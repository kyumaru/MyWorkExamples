Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration
Imports System.Data

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOttFichaTecnicaDetalle
#Region "Atributos"
		Private vgc_CadenaConexion As String
		Private vgo_Conexion As ConexionOracle
#End Region

#Region "Constructores"
		Public Sub New(pvc_CadenaConexion As String)
			vgc_CadenaConexion = pvc_CadenaConexion
		End Sub

		Public Sub New(pvo_Conexion As ConexionOracle)
			vgo_Conexion = pvo_Conexion
		End Sub
#End Region

#Region "Funciones"
		''' <summary>
		''' Permite agregar un registro en la tabla OTT_FICHA_TECNICA_DETALLE, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>17/12/2015 02:43:46 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOttFichaTecnicaDetalle) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttFichaTecnicaDetalle As DalOttFichaTecnicaDetalle
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo, pvo_Registro.IdEspacio, pvo_Registro.IdSubcomponente, pvo_Registro.IdRequerimiento).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente.")
				End If

				vlo_DalOttFichaTecnicaDetalle = New DalOttFichaTecnicaDetalle(vlo_Conexion)
				vln_Resultado = vlo_DalOttFichaTecnicaDetalle.InsertarRegistro(pvo_Registro)
			Catch vlo_Excepcion As Exception
				Throw
			Finally
				If vlb_LiberarConexion Then
					vlo_Conexion.Dispose()
				End If
			End Try

			Return vln_Resultado
        End Function

        ''' <summary>
        ''' inserta los registros de detalles, espacios y sub componentes
        ''' </summary>
        ''' <param name="pvo_DsFichaTecnicaEspacio">data set de espacios</param>
        ''' <param name="pvo_DsFichaTecnicaSubComponente">data set de sub componenrtes</param>
        ''' <param name="pvo_DsFichaTecnicaDetalle">data set de detalles</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>18/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarDescripcionesRequerimientosEspaciosPrincipales(pvo_DsFichaTecnicaEspacio As Data.DataSet, pvo_DsFichaTecnicaSubComponente As Data.DataSet, pvo_DsFichaTecnicaDetalle As Data.DataSet, pvo_OrdenTrabajo As EntOttOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsEspacio As Data.DataSet
            Dim vlo_DsSubComp As Data.DataSet
            Dim vlo_DsDetalle As Data.DataSet
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttFichaTecnicaEspacio As DalOttFichaTecnicaEspacio
            Dim vlo_DalOttFichaTecnicaSubComp As DalOttFichaTecnicaSubcomp
            Dim vlo_DalOttFichaTecnicaDetalle As DalOttFichaTecnicaDetalle
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttFichaTecnicaEspacio = New DalOttFichaTecnicaEspacio(vlo_Conexion)
                vlo_DalOttFichaTecnicaSubComp = New DalOttFichaTecnicaSubcomp(vlo_Conexion)
                vlo_DalOttFichaTecnicaDetalle = New DalOttFichaTecnicaDetalle(vlo_Conexion)

                vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_OrdenTrabajo)

                vlo_DsDetalle = vlo_DalOttFichaTecnicaDetalle.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_FICHA_TECNICA_DETALLE.ID_UBICACION, pvo_OrdenTrabajo.IdUbicacion, Modelo.OTT_FICHA_TECNICA_DETALLE.ID_ORDEN_TRABAJO, pvo_OrdenTrabajo.IdOrdenTrabajo), String.Empty, False, 0, 0)
                vlo_DsSubComp = vlo_DalOttFichaTecnicaSubComp.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_FICHA_TECNICA_SUBCOMP.ID_UBICACION, pvo_OrdenTrabajo.IdUbicacion, Modelo.OTT_FICHA_TECNICA_SUBCOMP.ID_ORDEN_TRABAJO, pvo_OrdenTrabajo.IdOrdenTrabajo), String.Empty, False, 0, 0)
                vlo_DsEspacio = vlo_DalOttFichaTecnicaEspacio.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_FICHA_TECNICA_ESPACIO.ID_UBICACION, pvo_OrdenTrabajo.IdUbicacion, Modelo.OTT_FICHA_TECNICA_ESPACIO.ID_ORDEN_TRABAJO, pvo_OrdenTrabajo.IdOrdenTrabajo), String.Empty, False, 0, 0)

                For Each vlo_FilaDetalle In vlo_DsDetalle.Tables(0).Rows
                    vlo_FilaDetalle.Delete()
                Next

                For Each vlo_FilaSubComp In vlo_DsSubComp.Tables(0).Rows
                    vlo_FilaSubComp.Delete()
                Next

                For Each vlo_FilaEspacio In vlo_DsEspacio.Tables(0).Rows
                    vlo_FilaEspacio.Delete()
                Next

                vlo_DalOttFichaTecnicaDetalle.AdapterOttFichaTecnicaDetalle(vlo_DsDetalle)
                vlo_DalOttFichaTecnicaSubComp.AdapterOttFichaTecnicaSubComp(vlo_DsSubComp)
                vlo_DalOttFichaTecnicaEspacio.AdapterOttFichaTecnicaEspacio(vlo_DsEspacio)

                vlo_DalOttFichaTecnicaEspacio.AdapterOttFichaTecnicaEspacio(pvo_DsFichaTecnicaEspacio)
                vlo_DalOttFichaTecnicaSubComp.AdapterOttFichaTecnicaSubComp(pvo_DsFichaTecnicaSubComponente)
                vlo_DalOttFichaTecnicaDetalle.AdapterOttFichaTecnicaDetalle(pvo_DsFichaTecnicaDetalle)

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

		''' <summary>
		''' Permite obtener un registro según su llave primaria
		''' </summary>
		''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
		''' <param name="pvc_IdOrdenTrabajo">Identificador único alfanumérico de la orden de trabajo</param>
		''' <param name="pvn_IdEspacio">Llave primaria de la tabla otm_espacio que se asocia con la secuencia sq_id_espacio</param>
		''' <param name="pvn_IdSubcomponente">Llave primaria de la tabla otm_subcomponente que se asocia con la secuencia sq_id_subcomponente</param>
		''' <param name="pvn_IdRequerimiento">Llave primaria de la tabla otm_requerimiento que se asocia con la secuencia sq_id_requerimiento</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>17/12/2015 02:43:46 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_IdEspacio As Integer, pvn_IdSubcomponente As Integer, pvn_IdRequerimiento As Integer) As EntOttFichaTecnicaDetalle
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttFichaTecnicaDetalle As DalOttFichaTecnicaDetalle

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOttFichaTecnicaDetalle = New DalOttFichaTecnicaDetalle(vlo_Conexion)
				Return vlo_DalOttFichaTecnicaDetalle.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}' AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_FICHA_TECNICA_DETALLE.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_FICHA_TECNICA_DETALLE.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper(), Modelo.OTT_FICHA_TECNICA_DETALLE.ID_ESPACIO, pvn_IdEspacio, Modelo.OTT_FICHA_TECNICA_DETALLE.ID_SUBCOMPONENTE, pvn_IdSubcomponente, Modelo.OTT_FICHA_TECNICA_DETALLE.ID_REQUERIMIENTO, pvn_IdRequerimiento))
			Catch vlo_Excepcion As Exception
				Throw
			Finally
				If vlb_LiberarConexion Then
					vlo_Conexion.Dispose()
				End If
			End Try
        End Function

        ''' <summary>
        ''' carga los datos de la vista para lista de detalles
        ''' </summary>
        ''' <param name="pvc_Condicion"></param>
        ''' <param name="pvc_Orden"></param>
        ''' <param name="pvb_Paginar"></param>
        ''' <param name="pvn_NumeroPagina"></param>
        ''' <param name="pvn_TamanoPagina"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>15/01/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarRegistrosListaPersonalizado(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As Data.DataSet
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsRegistros As Data.DataSet
            Dim vlo_DalOttFichaTecnicaDetalle As DalOttFichaTecnicaDetalle
            Dim vlo_WsSIRH As WsrSIRH.WsSIRH
            Dim vlo_DsDatos As Data.DataSet
            Dim vlo_DrFilaUnidad As Data.DataRow

            vlo_WsSIRH = New WsrSIRH.WsSIRH
            vlo_WsSIRH.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsSIRH.Timeout = -1
            vlo_WsSIRH.Url = System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_SIRH).ToString

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DsDatos = vlo_WsSIRH.UBICACION_CargarRegistros(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    DateTime.Now,
                    DateTime.Now,
                    0,
                    1,
                    -1,
                    True)

                vlo_DalOttFichaTecnicaDetalle = New DalOttFichaTecnicaDetalle(vlo_Conexion)

                vlo_DsRegistros = vlo_DalOttFichaTecnicaDetalle.ListarRegistrosLista(
                    pvc_Condicion,
                    pvc_Orden,
                    pvb_Paginar,
                    pvn_NumeroPagina,
                    pvn_TamanoPagina)

                vlo_DsDatos.Tables(0).PrimaryKey = New DataColumn() {vlo_DsDatos.Tables(0).Columns("Codigo_Ubica")}

                For Each vlo_DrFilaDetalle In vlo_DsRegistros.Tables(0).Rows

                    vlo_DrFilaUnidad = vlo_DsDatos.Tables(0).Rows.Find(New Object() {vlo_DrFilaDetalle(Modelo.V_OTT_FICHA_TECNICA_DETALLELST.COD_UNIDAD_SIRH).ToString})

                    vlo_DrFilaDetalle(Modelo.V_OTT_FICHA_TECNICA_DETALLELST.NOMBRE_UNIDAD) = vlo_DrFilaUnidad("Cod_Desc")

                Next

                Return vlo_DsRegistros

            Catch vlo_Exc As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Exc, Utilerias.OrdenesDeTrabajo.Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw
                End If
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
                If vlo_WsSIRH IsNot Nothing Then
                    vlo_WsSIRH.Dispose()
                End If
            End Try

        End Function

#End Region

	End Class
End Namespace
