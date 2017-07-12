Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttSolicitudRetiro
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
        ''' Permite agregar un registro en la tabla OTT_SOLICITUD_RETIRO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttSolicitudRetiro) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttSolicitudRetiro As DalOttSolicitudRetiro
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.Anno, pvo_Registro.IdSolicitudRetiro, pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Ya existe un registro con el mismo año y con el mismo número de solicitud")
                End If

                vlo_DalOttSolicitudRetiro = New DalOttSolicitudRetiro(vlo_Conexion)
                vln_Resultado = vlo_DalOttSolicitudRetiro.InsertarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTT_SOLICITUD_RETIRO, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOttSolicitudRetiro) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttSolicitudRetiro As DalOttSolicitudRetiro
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.Anno, pvo_Registro.IdSolicitudRetiro) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("La solicitud posee detalles de solicitud asociados")
                End If

                vlo_DalOttSolicitudRetiro = New DalOttSolicitudRetiro(vlo_Conexion)
                vln_Resultado = vlo_DalOttSolicitudRetiro.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvn_Anno">Llave primaria de la tabla ott_solicitud_retiro</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_Anno As Integer, pvn_consecutivo As Integer, pvn_idUbicacion As Integer, pvc_idOrdenTrabajo As String) As EntOttSolicitudRetiro
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttSolicitudRetiro As DalOttSolicitudRetiro

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttSolicitudRetiro = New DalOttSolicitudRetiro(vlo_Conexion)
                Return vlo_DalOttSolicitudRetiro.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = '{7}'",
                            Modelo.OTT_SOLICITUD_RETIRO.ANNO, pvn_Anno, Modelo.OTT_SOLICITUD_RETIRO.ID_SOLICITUD_RETIRO, pvn_consecutivo,
                            Modelo.OTT_SOLICITUD_RETIRO.ID_UBICACION, pvn_idUbicacion, Modelo.OTT_SOLICITUD_RETIRO.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Verifica si un registro posee datos asociados en las tablas hijas
        ''' </summary>
        ''' <param name="pvn_Anno">Llave primaria de la tabla ott_solicitud_retiro</param>
        ''' <param name="pvn_IdSolicitudRetiro">Llave primaria de la tabla ott_solicitud_retiro</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_Anno As Integer, pvn_IdSolicitudRetiro As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOttDetalleRetiro As DalOttDetalleRetiro

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                'valor inicial de retorno
                vlo_PoseeRegistrosAsociados = False

                'Determinar la existencia de registros asociados en la tabla OTT_DETALLE_RETIRO
                vlo_DalOttDetalleRetiro = New DalOttDetalleRetiro(vlo_Conexion)
                If vlo_DalOttDetalleRetiro.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTT_DETALLE_RETIRO.ANNO, pvn_Anno, Modelo.OTT_DETALLE_RETIRO.ID_SOLICITUD_RETIRO, pvn_IdSolicitudRetiro)).Existe Then
                    Return True
                End If

                Return False
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
