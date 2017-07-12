Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOtfFichaTecnicaDetalle
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
        ''' 
        ''' </summary>
        ''' <param name="pvo_DsFichaTecnicaEspacio"></param>
        ''' <param name="pvo_DsFichaTecnicaSubComponente"></param>
        ''' <param name="pvo_DsFichaTecnicaDetalle"></param>
        ''' <param name="pvo_OrdenTrabajo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>14/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarDescripcionesRequerimientosEspaciosPrincipales(pvo_DsFichaTecnicaEspacio As Data.DataSet, pvo_DsFichaTecnicaSubComponente As Data.DataSet, pvo_DsFichaTecnicaDetalle As Data.DataSet, pvo_OrdenTrabajo As EntOtfPreOrdenTrabajo, pvc_Estado As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsEspacio As Data.DataSet
            Dim vlo_DsSubComp As Data.DataSet
            Dim vlo_DsDetalle As Data.DataSet
            Dim vlo_DalOtfPreOrdenTrabajo As DalOtfPreOrdenTrabajo
            Dim vlo_DalOtfFichaTecnicaEspacio As DalOtfFichaTecnicaEspacio
            Dim vlo_DalOtfFichaTecnicaSubComp As DalOtfFichaTecnicaSubcomp
            Dim vlo_DalOtfFichaTecnicaDetalle As DalOtfFichaTecnicaDetalle
            Dim vlo_EntOtfRevisionPreOrdenTra As EntOtfRevisionPreOrdenTra
            Dim vlo_DalOtfRevisionPreOrdenTra As DalOtfRevisionPreOrdenTra
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vln_Resultado As Integer
            Dim vln_EntOtfFichaTecnicaDetalle As EntOtfFichaTecnicaDetalle
            Dim vlo_EntOtfFichaTecnicaSubcomp As EntOtfFichaTecnicaSubcomp
            Dim vlo_EntOtfFichaTecnicaEspacio As EntOtfFichaTecnicaEspacio

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOtfPreOrdenTrabajo = New DalOtfPreOrdenTrabajo(vlo_Conexion)
                vlo_DalOtfFichaTecnicaEspacio = New DalOtfFichaTecnicaEspacio(vlo_Conexion)
                vlo_DalOtfFichaTecnicaSubComp = New DalOtfFichaTecnicaSubcomp(vlo_Conexion)
                vlo_DalOtfFichaTecnicaDetalle = New DalOtfFichaTecnicaDetalle(vlo_Conexion)
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)

                vlo_DsDetalle = vlo_DalOtfFichaTecnicaDetalle.ListarRegistros(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_FICHA_TECNICA_DETALLE.ID_UBICACION, pvo_OrdenTrabajo.IdUbicacion, Modelo.OTF_FICHA_TECNICA_DETALLE.ID_PRE_ORDEN_TRABAJO, pvo_OrdenTrabajo.IdPreOrdenTrabajo), String.Empty, False, 0, 0)
                vlo_DsSubComp = vlo_DalOtfFichaTecnicaSubComp.ListarRegistros(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_FICHA_TECNICA_SUBCOMP.ID_UBICACION, pvo_OrdenTrabajo.IdUbicacion, Modelo.OTF_FICHA_TECNICA_SUBCOMP.ID_PRE_ORDEN_TRABAJO, pvo_OrdenTrabajo.IdPreOrdenTrabajo), String.Empty, False, 0, 0)
                vlo_DsEspacio = vlo_DalOtfFichaTecnicaEspacio.ListarRegistros(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_FICHA_TECNICA_ESPACIO.ID_UBICACION, pvo_OrdenTrabajo.IdUbicacion, Modelo.OTF_FICHA_TECNICA_ESPACIO.ID_PRE_ORDEN_TRABAJO, pvo_OrdenTrabajo.IdPreOrdenTrabajo), String.Empty, False, 0, 0)

                For Each vlo_FilaDetalle In vlo_DsDetalle.Tables(0).Rows
                    vln_EntOtfFichaTecnicaDetalle = vlo_DalOtfFichaTecnicaDetalle.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}",
                            Modelo.OTF_FICHA_TECNICA_DETALLE.ID_UBICACION, vlo_FilaDetalle(Modelo.OTF_FICHA_TECNICA_DETALLE.ID_UBICACION),
                            Modelo.OTF_FICHA_TECNICA_DETALLE.ID_PRE_ORDEN_TRABAJO, vlo_FilaDetalle(Modelo.OTF_FICHA_TECNICA_DETALLE.ID_PRE_ORDEN_TRABAJO),
                            Modelo.OTF_FICHA_TECNICA_DETALLE.ID_ESPACIO, vlo_FilaDetalle(Modelo.OTF_FICHA_TECNICA_DETALLE.ID_ESPACIO),
                            Modelo.OTF_FICHA_TECNICA_DETALLE.ID_SUBCOMPONENTE, vlo_FilaDetalle(Modelo.OTF_FICHA_TECNICA_DETALLE.ID_SUBCOMPONENTE),
                            Modelo.OTF_FICHA_TECNICA_DETALLE.ID_REQUERIMIENTO, vlo_FilaDetalle(Modelo.OTF_FICHA_TECNICA_DETALLE.ID_REQUERIMIENTO)))

                    vlo_DalOtfFichaTecnicaDetalle.BorrarRegistro(vln_EntOtfFichaTecnicaDetalle)

                Next

                For Each vlo_FilaSubComp In vlo_DsSubComp.Tables(0).Rows
                    vlo_EntOtfFichaTecnicaSubcomp = vlo_DalOtfFichaTecnicaSubComp.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}",
                         Modelo.OTF_FICHA_TECNICA_SUBCOMP.ID_UBICACION, vlo_FilaSubComp(Modelo.OTF_FICHA_TECNICA_SUBCOMP.ID_UBICACION),
                         Modelo.OTF_FICHA_TECNICA_SUBCOMP.ID_PRE_ORDEN_TRABAJO, vlo_FilaSubComp(Modelo.OTF_FICHA_TECNICA_SUBCOMP.ID_PRE_ORDEN_TRABAJO),
                         Modelo.OTF_FICHA_TECNICA_SUBCOMP.ID_ESPACIO, vlo_FilaSubComp(Modelo.OTF_FICHA_TECNICA_SUBCOMP.ID_ESPACIO),
                         Modelo.OTF_FICHA_TECNICA_SUBCOMP.ID_SUBCOMPONENTE, vlo_FilaSubComp(Modelo.OTF_FICHA_TECNICA_SUBCOMP.ID_SUBCOMPONENTE)))

                    vlo_DalOtfFichaTecnicaSubComp.BorrarRegistro(vlo_EntOtfFichaTecnicaSubcomp)
                Next

                For Each vlo_FilaEspacio In vlo_DsEspacio.Tables(0).Rows
                    vlo_EntOtfFichaTecnicaEspacio = vlo_DalOtfFichaTecnicaEspacio.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                            Modelo.OTF_FICHA_TECNICA_ESPACIO.ID_UBICACION, vlo_FilaEspacio(Modelo.OTF_FICHA_TECNICA_ESPACIO.ID_UBICACION),
                            Modelo.OTF_FICHA_TECNICA_ESPACIO.ID_PRE_ORDEN_TRABAJO, vlo_FilaEspacio(Modelo.OTF_FICHA_TECNICA_ESPACIO.ID_PRE_ORDEN_TRABAJO),
                            Modelo.OTF_FICHA_TECNICA_ESPACIO.ID_ESPACIO, vlo_FilaEspacio(Modelo.OTF_FICHA_TECNICA_ESPACIO.ID_ESPACIO)))

                    vlo_DalOtfFichaTecnicaEspacio.BorrarRegistro(vlo_EntOtfFichaTecnicaEspacio)
                Next

                'vlo_DalOtfFichaTecnicaDetalle.AdapterOtfFichaTecnicaDetalle(vlo_DsDetalle)
                'vlo_DalOtfFichaTecnicaSubComp.AdapterOtfFichaTecnicaSubComp(vlo_DsSubComp)
                'vlo_DalOtfFichaTecnicaEspacio.AdapterOtfFichaTecnicaEspacio(vlo_DsEspacio)

                vlo_DalOtfFichaTecnicaEspacio.AdapterOtfFichaTecnicaEspacio(pvo_DsFichaTecnicaEspacio)
                vlo_DalOtfFichaTecnicaSubComp.AdapterOtfFichaTecnicaSubComp(pvo_DsFichaTecnicaSubComponente)
                vlo_DalOtfFichaTecnicaDetalle.AdapterOtfFichaTecnicaDetalle(pvo_DsFichaTecnicaDetalle)

                If pvc_Estado <> String.Empty Then
                    'vlo_DalOtfPreOrdenTrabajo.ModificarRegistro(pvo_OrdenTrabajo)

                    'si es PENDIENTE_REVISION_DIRECTOR se registra un nuevo dato en revision
                    If EstadoOrden.PENDIENTE_REVISION_DIRECTOR = pvc_Estado Then
                        vlo_EntOtfRevisionPreOrdenTra = New EntOtfRevisionPreOrdenTra
                        vlo_EntOtfRevisionPreOrdenTra.IdUbicacion = pvo_OrdenTrabajo.IdUbicacion
                        vlo_EntOtfRevisionPreOrdenTra.IdPreOrdenTrabajo = pvo_OrdenTrabajo.IdPreOrdenTrabajo
                        vlo_EntOtfRevisionPreOrdenTra.Estado = EstadoOrden.PENDIENTE_REVISION_DIRECTOR
                        vlo_EntOtfRevisionPreOrdenTra.Usuario = pvo_OrdenTrabajo.Usuario

                        vlo_DalOtfRevisionPreOrdenTra = New DalOtfRevisionPreOrdenTra(vlo_Conexion)
                        vlo_DalOtfRevisionPreOrdenTra.InsertarRegistro(vlo_EntOtfRevisionPreOrdenTra)

                    Else 'si es ASIGNADA se llama el procedimiento
                        vlo_DalOttOrdenTrabajo.EjecutarPrOtAsigOtDisenio(pvo_OrdenTrabajo.IdUbicacion, pvo_OrdenTrabajo.IdPreOrdenTrabajo, pvo_OrdenTrabajo.CodUnidadSirh, pvo_OrdenTrabajo.NombrePersonaContacto, pvo_OrdenTrabajo.Telefono, pvo_OrdenTrabajo.SennasExactas, pvo_OrdenTrabajo.DescripcionTrabajo, pvo_OrdenTrabajo.Usuario, CType(pvo_OrdenTrabajo.NumEmpleado, Integer), pvo_OrdenTrabajo.IdCategoriaServicio, pvo_OrdenTrabajo.IdActividad, pvo_OrdenTrabajo.IdLugarTrabajo, pvo_OrdenTrabajo.IncluidaEnRecepcion, pvo_OrdenTrabajo.IdUbicacionOrigen)
                    End If
                End If

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
        ''' Permite agregar un registro en la tabla OTF_FICHA_TECNICA_DETALLE, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtfFichaTecnicaDetalle) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfFichaTecnicaDetalle As DalOtfFichaTecnicaDetalle
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdPreOrdenTrabajo, pvo_Registro.IdEspacio, pvo_Registro.IdSubcomponente, pvo_Registro.IdRequerimiento).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
                End If

                vlo_DalOtfFichaTecnicaDetalle = New DalOtfFichaTecnicaDetalle(vlo_Conexion)
                vln_Resultado = vlo_DalOtfFichaTecnicaDetalle.InsertarRegistro(pvo_Registro)
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
        ''' <param name="pvn_IdPreOrdenTrabajo">Llave primaria de la tabla otf_pre_orden_trabajo que se asocia con la secuencia sq_id_pre_orden_trabajo</param>
        ''' <param name="pvn_IdEspacio">Llave primaria de la tabla otm_espacio que se asocia con la secuencia sq_id_espacio</param>
        ''' <param name="pvn_IdSubcomponente">Llave primaria de la tabla otm_subcomponente que se asocia con la secuencia sq_id_subcomponente</param>
        ''' <param name="pvn_IdRequerimiento">Llave primaria de la tabla otm_requerimiento que se asocia con la secuencia sq_id_requerimiento</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvn_IdPreOrdenTrabajo As Integer, pvn_IdEspacio As Integer, pvn_IdSubcomponente As Integer, pvn_IdRequerimiento As Integer) As EntOtfFichaTecnicaDetalle
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfFichaTecnicaDetalle As DalOtfFichaTecnicaDetalle

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfFichaTecnicaDetalle = New DalOtfFichaTecnicaDetalle(vlo_Conexion)
                Return vlo_DalOtfFichaTecnicaDetalle.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTF_FICHA_TECNICA_DETALLE.ID_UBICACION, pvn_IdUbicacion, Modelo.OTF_FICHA_TECNICA_DETALLE.ID_PRE_ORDEN_TRABAJO, pvn_IdPreOrdenTrabajo, Modelo.OTF_FICHA_TECNICA_DETALLE.ID_ESPACIO, pvn_IdEspacio, Modelo.OTF_FICHA_TECNICA_DETALLE.ID_SUBCOMPONENTE, pvn_IdSubcomponente, Modelo.OTF_FICHA_TECNICA_DETALLE.ID_REQUERIMIENTO, pvn_IdRequerimiento))
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
