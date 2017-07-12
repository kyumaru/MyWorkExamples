Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOttOrdnTrabDecInicial
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
		''' Permite agregar un registro en la tabla OTT_ORDN_TRAB_DEC_INICIAL, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>31/03/2016 08:44:56 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOttOrdnTrabDecInicial) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttOrdnTrabDecInicial As DalOttOrdnTrabDecInicial
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo, pvo_Registro.IdTipoObra, pvo_Registro.IdRubroDecisionInicia).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
				End If

				vlo_DalOttOrdnTrabDecInicial = New DalOttOrdnTrabDecInicial(vlo_Conexion)
				vln_Resultado = vlo_DalOttOrdnTrabDecInicial.InsertarRegistro(pvo_Registro)
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
        ''' Permite agregar un registro en la tabla OTT_ORDN_TRAB_DEC_INICIAL, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>31/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarDesicionInicialConDetalles(ByVal pvo_Registro As EntOttDesicionInicial, pvo_DsOrdenTrabDecInic As Data.DataSet) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttDesicionInicial As DalOttDesicionInicial
            Dim vlo_DalOttOrdnTrabDecInicial As DalOttOrdnTrabDecInicial
            Dim vln_Resultado As Integer
            vln_Resultado = -1

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttDesicionInicial = New DalOttDesicionInicial(vlo_Conexion)
                vlo_DalOttOrdnTrabDecInicial = New DalOttOrdnTrabDecInicial(vlo_Conexion)

                vlo_DalOttDesicionInicial.InsertarRegistro(pvo_Registro)
                vlo_DalOttOrdnTrabDecInicial.AdapterOttOrdenTrabDecInic(pvo_DsOrdenTrabDecInic)

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

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite modificar registros en la tabla OTT_ORDN_TRAB_DEC_INICIAL y modificar una desicion inicial
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>31/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarDesicionInicialConDetalles(ByVal pvo_Registro As EntOttDesicionInicial, pvo_DsOrdenTrabDecInic As Data.DataSet) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttDesicionInicial As DalOttDesicionInicial
            Dim vlo_DalOttOrdnTrabDecInicial As DalOttOrdnTrabDecInicial
            Dim vlo_DsOrdnTrabDecInicial As Data.DataSet
            Dim vln_Resultado As Integer
            vln_Resultado = -1

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttDesicionInicial = New DalOttDesicionInicial(vlo_Conexion)
                vlo_DalOttOrdnTrabDecInicial = New DalOttOrdnTrabDecInicial(vlo_Conexion)

                vlo_DsOrdnTrabDecInicial = vlo_DalOttOrdnTrabDecInicial.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo), String.Empty, False, 0, 0)

                For Each vlo_Fila In vlo_DsOrdnTrabDecInicial.Tables(0).Rows
                    vlo_Fila.Delete()
                Next
                vlo_DalOttOrdnTrabDecInicial = New DalOttOrdnTrabDecInicial(vlo_Conexion)
                vlo_DalOttOrdnTrabDecInicial.AdapterOttOrdenTrabDecInic(vlo_DsOrdnTrabDecInicial)

                vlo_DalOttDesicionInicial.ModificarRegistro(pvo_Registro)
                vlo_DalOttOrdnTrabDecInicial = New DalOttOrdnTrabDecInicial(vlo_Conexion)
                vlo_DalOttOrdnTrabDecInicial.AdapterOttOrdenTrabDecInic(pvo_DsOrdenTrabDecInic)

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

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite obtener un registro según su llave primaria
        ''' </summary>
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvc_IdOrdenTrabajo">Identificador único alfanumérico de la orden de trabajo</param>
        ''' <param name="pvn_IdTipoObra">Llave primaria de la tabla otm_rubro_desicion_inicia que se asocia con la secuencia sq_rubro_desicion_inicia</param>
        ''' <param name="pvn_IdRubroDecisionInicia">Llave primaria de la tabla otm_rubro_desicion_inicia que se asocia con la secuencia sq_rubro_desicion_inicia</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/03/2016 08:44:56 a.m.</creationDate>
        ''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_IdTipoObra As Integer, pvn_IdRubroDecisionInicia As Integer) As EntOttOrdnTrabDecInicial
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttOrdnTrabDecInicial As DalOttOrdnTrabDecInicial

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOttOrdnTrabDecInicial = New DalOttOrdnTrabDecInicial(vlo_Conexion)
				Return vlo_DalOttOrdnTrabDecInicial.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}' AND {4} = {5} AND {6} = {7}", Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper(), Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_TIPO_OBRA, pvn_IdTipoObra, Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_RUBRO_DECISION_INICIA, pvn_IdRubroDecisionInicia))
			Catch vlo_Excepcion As Exception
				Throw
			Finally
				If vlb_LiberarConexion Then
					vlo_Conexion.Dispose()
				End If
			End Try
		End Function

#End Region

	End Class
End Namespace
