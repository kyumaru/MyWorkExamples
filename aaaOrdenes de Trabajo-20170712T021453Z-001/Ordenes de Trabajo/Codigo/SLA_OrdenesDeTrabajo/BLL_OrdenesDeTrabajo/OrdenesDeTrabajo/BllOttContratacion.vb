Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttContratacion
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

#Region "Metodos"
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        ''' <author>César Bermudez G</author>
        ''' <creationDate>28/4/2014</creationDate>
        ''' <changeLog></changeLog>
        Private Sub Notificacion(pvc_CorreoInstitucional As String, pvc_Cuerpo As String, pvc_Asunto As String)
            Dim vlo_WsGestorNotificaciones As WsrGestorNotificaciones.wsGestorNotificaciones
            Dim vlo_Sistema As WsrGestorNotificaciones.EntGNM_SISTEMA
            Dim vlo_ListaAdjunto As List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)
            Dim vlo_ListaDestinatario As List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
            Dim vlo_Notificacion As WsrGestorNotificaciones.EntGNT_NOTIFICACION
            Dim vlo_EntGNT_DESTINATARIO As WsrGestorNotificaciones.EntGNT_DESTINATARIO

            vlo_WsGestorNotificaciones = New WsrGestorNotificaciones.wsGestorNotificaciones
            vlo_WsGestorNotificaciones.Timeout = -1
            vlo_WsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsGestorNotificaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_GESTOR_NOTIFICACIONES)

            Try

                vlo_Sistema = vlo_WsGestorNotificaciones.GNM_SISTEMA_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    String.Format("NOMBRE_SISTEMA = '{0}'", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_GN)))

                If vlo_Sistema IsNot Nothing AndAlso vlo_Sistema.Existe Then
                    vlo_Notificacion = New WsrGestorNotificaciones.EntGNT_NOTIFICACION()

                    vlo_Notificacion.ASUNTO = pvc_Asunto
                    vlo_Notificacion.CUERPO = pvc_Cuerpo
                    vlo_Notificacion.ES_HTML = 0
                    vlo_Notificacion.USUARIO_CREA = System.Environment.UserName.Trim()

                    vlo_ListaDestinatario = New List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
                    vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
                    vlo_EntGNT_DESTINATARIO.DESTINATARIO = pvc_CorreoInstitucional
                    vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)

                    vlo_ListaAdjunto = New List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)

                    vlo_WsGestorNotificaciones.GNT_NOTIFICACION_Registrar(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                        vlo_Sistema,
                        vlo_Notificacion,
                        vlo_ListaAdjunto.ToArray,
                        vlo_ListaDestinatario.ToArray)

                End If
            Catch ex As Exception
                Throw New OrdenesDeTrabajoException("Error en el envío de correos.")
            End Try
        End Sub
#End Region

#Region "Funciones"

        ''' <summary>
        ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
        ''' </summary>
        ''' <param name="pvc_idPersonal"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>28/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function CargarFuncionario(pvc_idPersonal As String) As WsrEU_Curriculo.EntEmpleados
            Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

            vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
            vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsEU_Curriculo.Timeout = -1
            vlo_WsEU_Curriculo.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_EU_CURRICULO)

            Try
                Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    String.Format("ID_PERSONAL = '{0}' OR NUM_EMPLEADO = {0}", pvc_idPersonal))
            Catch ex As Exception
                Throw
            Finally
                If vlo_WsEU_Curriculo IsNot Nothing Then
                    vlo_WsEU_Curriculo.Dispose()
                End If
            End Try
        End Function


        ''' <summary>
        ''' Permite agregar un registro en la tabla OTT_CONTRATACION, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>13/05/2016 10:03:34 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttContratacion) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttContratacion As DalOttContratacion
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo, pvo_Registro.Version).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
                End If

                vlo_DalOttContratacion = New DalOttContratacion(vlo_Conexion)
                vln_Resultado = vlo_DalOttContratacion.InsertarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTT_CONTRATACION, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOttContratacion) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttContratacion As DalOttContratacion
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo, pvo_Registro.Version) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Hay registro asociados")
                End If

                vlo_DalOttContratacion = New DalOttContratacion(vlo_Conexion)
                vln_Resultado = vlo_DalOttContratacion.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvn_Version">Numero de version del proceso de contratación asociado a una orden de trabajo</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_Version As Integer) As EntOttContratacion
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttContratacion As DalOttContratacion

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttContratacion = New DalOttContratacion(vlo_Conexion)
                Return vlo_DalOttContratacion.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}' AND {4} = {5}", Modelo.OTT_CONTRATACION.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_CONTRATACION.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper(), Modelo.OTT_CONTRATACION.VERSION, pvn_Version))
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
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvc_IdOrdenTrabajo">Identificador único alfanumérico de la orden de trabajo</param>
        ''' <param name="pvn_Version">Numero de version del proceso de contratación asociado a una orden de trabajo</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>13/05/2016 10:03:34 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_Version As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOttDocumentoContratacion As DalOttDocumentoContratacion
            Dim vlo_DalOttLineaAdjudicacion As DalOttLineaAdjudicacion

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

                'Determinar la existencia de registros asociados en la tabla OTT_DOCUMENTO_CONTRATACION
                vlo_DalOttDocumentoContratacion = New DalOttDocumentoContratacion(vlo_Conexion)
                If vlo_DalOttDocumentoContratacion.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}' AND {4} = {5}", Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper(), Modelo.OTT_DOCUMENTO_CONTRATACION.VERSION, pvn_Version)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTT_LINEA_ADJUDICACION
                vlo_DalOttLineaAdjudicacion = New DalOttLineaAdjudicacion(vlo_Conexion)
                If vlo_DalOttLineaAdjudicacion.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}' AND {4} = {5}", Modelo.OTT_LINEA_ADJUDICACION.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_LINEA_ADJUDICACION.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper(), Modelo.OTT_LINEA_ADJUDICACION.VERSION, pvn_Version)).Existe Then
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

        ''' <summary>
        ''' Guarda la version y cambia el estado de la orden con lo enviado por parámetro
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <param name="pvc_estado"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>15/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Function GuardarNuevaVersion(pvo_Registro As EntOttContratacion, pvc_estado As String, pvn_etapaActual As Integer, pvc_etapaActual As String, pvn_NumEmpleado As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttContratacion As DalOttContratacion
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_EntOttContratacion As EntOttContratacion
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_DalOttDocumentoContratacion As DalOttDocumentoContratacion
            Dim vlo_DsDatos As Data.DataSet
            Dim vlo_nuevaFila As Data.DataRow
            Dim vlo_DsNuevosDatos As Data.DataSet
            Dim vlo_resultado = -1

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttContratacion = New DalOttContratacion(vlo_Conexion)
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_DalOttDocumentoContratacion = New DalOttDocumentoContratacion(vlo_Conexion)

                If pvo_Registro.Version = 1 AndAlso Not pvo_Registro.Existe Then
                    'Se inserta la nueva version de la contratación
                    vlo_resultado = vlo_DalOttContratacion.InsertarRegistro(pvo_Registro)
                Else
                    'Se modifica el valor de la version anterior a NO_EDITABLE
                    vlo_EntOttContratacion = vlo_DalOttContratacion.ObtenerRegistro(String.Format("{0} = '{1}' AND {2} = {3} AND {4} = {5}",
                                                                                    Modelo.OTT_CONTRATACION.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo,
                                                                                    Modelo.OTT_CONTRATACION.ID_UBICACION, pvo_Registro.IdUbicacion,
                                                                                    Modelo.OTT_CONTRATACION.VERSION, pvo_Registro.Version))

                    If vlo_EntOttContratacion.Existe Then
                        vlo_EntOttContratacion.Editable = Version.NO_EDITABLE
                        vlo_EntOttContratacion.IdEtapaContratacion = pvn_etapaActual
                        vlo_resultado = vlo_DalOttContratacion.ModificarRegistro(vlo_EntOttContratacion)
                    End If

                    vlo_EntOttContratacion = New EntOttContratacion

                    vlo_EntOttContratacion.Version = pvo_Registro.Version + 1
                    vlo_EntOttContratacion.Observaciones = pvo_Registro.Observaciones
                    vlo_EntOttContratacion.IdUbicacion = pvo_Registro.IdUbicacion
                    vlo_EntOttContratacion.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                    vlo_EntOttContratacion.IdEtapaContratacion = pvo_Registro.IdEtapaContratacion
                    vlo_EntOttContratacion.Editable = Version.EDITABLE
                    vlo_EntOttContratacion.IdViaContrato = pvo_Registro.IdViaContrato
                    vlo_EntOttContratacion.NumeroContrato = pvo_Registro.NumeroContrato
                    vlo_EntOttContratacion.NombreContrato = pvo_Registro.NombreContrato
                    vlo_EntOttContratacion.NumeroDecisionInicial = pvo_Registro.NumeroDecisionInicial
                    vlo_EntOttContratacion.NumeroSolicitud = pvo_Registro.NumeroSolicitud
                    vlo_EntOttContratacion.Usuario = pvo_Registro.Usuario

                    'Se inserta la nueva version de la contratación
                    vlo_resultado = vlo_DalOttContratacion.InsertarRegistro(vlo_EntOttContratacion)
                End If

                If vlo_resultado > 0 Then
                    vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = '{1}' AND {2} = {3}",
                                Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo, Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvo_Registro.IdUbicacion))
                    If vlo_EntOttOrdenTrabajo.Existe Then
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = pvc_estado
                        vlo_resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                        If vlo_resultado > 0 Then
                            vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                            vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                            vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                            vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                            vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo
                            'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                            vlo_EntOttTrazabilidadProceso.Observaciones = String.Format("Creación de una nueva version apartir de etapa {0}", pvc_etapaActual)
                            vlo_EntOttTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                            vlo_resultado = vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                        End If


                    End If
                End If

                vlo_DsDatos = vlo_DalOttDocumentoContratacion.ListarRegistrosLista(
                    String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5} AND {6} < {7}",
                                Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION,
                                pvo_Registro.IdUbicacion,
                                Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO,
                                pvo_Registro.IdOrdenTrabajo,
                                Modelo.OTT_DOCUMENTO_CONTRATACION.VERSION,
                                pvo_Registro.Version,
                                Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ETAPA_CONTRATACION,
                                pvn_etapaActual), String.Empty, False, 0, 0)

                vlo_DsNuevosDatos = vlo_DalOttDocumentoContratacion.ListarRegistros("1=0", String.Empty, False, 0, 0)

                If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila As Data.DataRow In vlo_DsDatos.Tables(0).Rows
                        Dim vlc_Origen = vlo_fila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ORIGEN)
                        Dim vln_Numlinea = vlo_fila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.NUMERO_LINEA)
                        vlo_nuevaFila = vlo_DsNuevosDatos.Tables(0).NewRow
                        vlo_nuevaFila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_UBICACION) = vlo_fila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_UBICACION)
                        vlo_nuevaFila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ORDEN_TRABAJO) = vlo_fila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ORDEN_TRABAJO)
                        vlo_nuevaFila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_TIPO_DOCUMENTO) = vlo_fila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_TIPO_DOCUMENTO)
                        vlo_nuevaFila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ETAPA_ORDEN_TRABAJO) = vlo_fila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ETAPA_ORDEN_TRABAJO)
                        vlo_nuevaFila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ADJUNTO_ORDEN_TRABAJO) = vlo_fila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ADJUNTO_ORDEN_TRABAJO)
                        vlo_nuevaFila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ETAPA_CONTRATACION) = vlo_fila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ETAPA_CONTRATACION)
                        vlo_nuevaFila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.DOCUMENTO_TRAMITADO) = vlo_fila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.DOCUMENTO_TRAMITADO)
                        vlo_nuevaFila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.FECHA_HORA_REGISTRO) = vlo_fila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.FECHA_HORA_REGISTRO)
                        vlo_nuevaFila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ORIGEN) = IIf(vlc_Origen = "-", DBNull.Value, vlc_Origen)
                        vlo_nuevaFila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.NUMERO_LINEA) = IIf(vln_Numlinea = 0, DBNull.Value, vln_Numlinea)
                        vlo_nuevaFila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.USUARIO) = pvo_Registro.Usuario
                        vlo_nuevaFila(Modelo.V_OTT_DOCUMENTO_CONTRATLST.VERSION) = vlo_EntOttContratacion.Version
                        vlo_DsNuevosDatos.Tables(0).Rows.Add(vlo_nuevaFila)

                    Next
                End If
                vlo_DalOttDocumentoContratacion.AdapterOtTAdjunto(vlo_DsNuevosDatos)

                vlo_Conexion.TransaccionCommit()

                Return vlo_resultado

            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

        End Function

        ''' <summary>
        ''' Se encarga de cambiar el estado de la orden a GCT: Gestion de contratación y de registrar ese cambio en la trazabilidad
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>19/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Function DevolverExpediente(pvo_Registro As EntOttOrdenTrabajo, pvo_Contratacion As EntOttContratacion, pvn_NumEmpleado As Integer, pvc_Motivo As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttContratacion As DalOttContratacion
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_resultado = -1

            If vgo_Conexion Is Nothing Then
                vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                vlb_LiberarConexion = True
            Else
                vlo_Conexion = vgo_Conexion
                vlb_LiberarConexion = False
            End If

            Try

                vlo_Conexion.TransaccionBegin()


                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_DalOttContratacion = New DalOttContratacion(vlo_Conexion)

                pvo_Contratacion.Editable = Version.NO_EDITABLE

                vlo_resultado = vlo_DalOttContratacion.ModificarRegistro(pvo_Contratacion)


                pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.GESTION_CONTRATACION
                vlo_resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)

                If vlo_resultado > 0 Then
                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_Registro.EstadoOrdenTrabajo
                    ' vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                    vlo_EntOttTrazabilidadProceso.Observaciones = pvc_Motivo
                    vlo_EntOttTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                    vlo_resultado = vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                End If

                vlo_Conexion.TransaccionCommit()

                Return vlo_resultado
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

        End Function

        ''' <summary>
        ''' Cierra el expediente tecnico
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>19/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Function CerrarExpediente(pvo_RegistroOrden As EntOttOrdenTrabajo, pvo_Contratacion As EntOttContratacion, pvn_NumEmpleado As Integer, pvn_idEtapaActual As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttContratacion As DalOttContratacion
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_resultado = -1
            Dim vln_SiguienteEtapa As Integer

            If vgo_Conexion Is Nothing Then
                vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                vlb_LiberarConexion = True
            Else
                vlo_Conexion = vgo_Conexion
                vlb_LiberarConexion = False
            End If

            Try

                vlo_DalOttContratacion = New DalOttContratacion(vlo_Conexion)
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)

                vln_SiguienteEtapa = vlo_DalOttContratacion.SiguienteEstado(pvn_idEtapaActual)

                Select Case vln_SiguienteEtapa
                    Case EtapaContratacion.EXPEDIENTE_TECNICO
                        pvo_RegistroOrden.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE
                    Case EtapaContratacion.INICIO
                        pvo_RegistroOrden.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_INICIO
                    Case EtapaContratacion.PUBLICACION_CARTEL
                        pvo_RegistroOrden.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_PUBLICACION_CARTEL
                    Case EtapaContratacion.VISITA_TECNICA
                        pvo_RegistroOrden.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_VISITA_TECNICA
                    Case EtapaContratacion.ACLARACIONES
                        pvo_RegistroOrden.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_ACLARACIONES
                    Case EtapaContratacion.OFERTAS
                        pvo_RegistroOrden.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_OFERTAS
                    Case EtapaContratacion.RECOMENDACION_TECNICA
                        pvo_RegistroOrden.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_RECOMENDACION_TECNICA
                    Case EtapaContratacion.ADJUDICACION
                        pvo_RegistroOrden.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_ADJUDICACION
                End Select

                vlo_Conexion.TransaccionBegin()

                pvo_Contratacion.IdEtapaContratacion = pvn_idEtapaActual
                pvo_Contratacion.Usuario = pvo_RegistroOrden.Usuario
                vlo_resultado = vlo_DalOttContratacion.ModificarRegistro(pvo_Contratacion)

                vlo_resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_RegistroOrden)

                If vlo_resultado > 0 Then
                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_RegistroOrden.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_RegistroOrden.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_RegistroOrden.EstadoOrdenTrabajo
                    'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                    vlo_EntOttTrazabilidadProceso.Observaciones = String.Empty
                    vlo_EntOttTrazabilidadProceso.Usuario = pvo_RegistroOrden.Usuario

                    vlo_resultado = vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                End If
                vlo_Conexion.TransaccionCommit()

                Return vlo_resultado
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

        End Function

        ''' <summary>
        ''' Almacena los datos de la etapa de inicio
        ''' </summary>
        ''' <param name="pvo_Documentos"></param>
        ''' <param name="pvo_Contratacion"></param>
        ''' <param name="pvo_ordenTrabajo"></param>
        ''' <param name="pvn_numEmpleado"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>21/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Function GuardarInicio(pvo_Contratacion As EntOttContratacion, pvo_ordenTrabajo As EntOttOrdenTrabajo, pvn_numEmpleado As Integer, pvn_Etapa As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttContratacion As DalOttContratacion
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_DalOttDocumentoContratacion As DalOttDocumentoContratacion
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_DsDatos As Data.DataSet
            Dim vlo_DsDatosDocumentos As Data.DataSet
            Dim vlo_resultado = -1
            Dim vln_SiguienteEtapa As Integer
            Dim vlo_NuevaFila As Data.DataRow

            Dim vlb_transaccion As Boolean

            If vgo_Conexion Is Nothing Then
                vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                vlb_LiberarConexion = True
            Else
                vlo_Conexion = vgo_Conexion
                vlb_LiberarConexion = False
            End If

            Try

                vlo_DalOttContratacion = New DalOttContratacion(vlo_Conexion)
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DalOttDocumentoContratacion = New DalOttDocumentoContratacion(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()
                vlb_transaccion = True
                'Agrega a la contratacion los numero de solicitud y decisión inicial
                pvo_Contratacion.IdEtapaContratacion = EtapaContratacion.INICIO
                vlo_resultado = vlo_DalOttContratacion.ModificarRegistro(pvo_Contratacion)

                vln_SiguienteEtapa = vlo_DalOttContratacion.SiguienteEstado(pvn_Etapa)

                Select Case vln_SiguienteEtapa
                    Case EtapaContratacion.EXPEDIENTE_TECNICO
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE
                    Case EtapaContratacion.INICIO
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_INICIO
                    Case EtapaContratacion.PUBLICACION_CARTEL
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_PUBLICACION_CARTEL
                    Case EtapaContratacion.VISITA_TECNICA
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_VISITA_TECNICA
                    Case EtapaContratacion.ACLARACIONES
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_ACLARACIONES
                    Case EtapaContratacion.OFERTAS
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_OFERTAS
                    Case EtapaContratacion.RECOMENDACION_TECNICA
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_RECOMENDACION_TECNICA
                    Case EtapaContratacion.ADJUDICACION
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_ADJUDICACION
                End Select

                vlo_resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_ordenTrabajo)

                If vlo_resultado > 0 Then
                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_ordenTrabajo.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_ordenTrabajo.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_numEmpleado
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_ordenTrabajo.EstadoOrdenTrabajo
                    'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                    vlo_EntOttTrazabilidadProceso.Observaciones = String.Empty
                    vlo_EntOttTrazabilidadProceso.Usuario = pvo_ordenTrabajo.Usuario

                    vlo_resultado = vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                End If

                vlo_Conexion.TransaccionCommit()

                Return vlo_resultado
            Catch vlo_Excepcion As Exception
                If vlb_transaccion Then
                    vlo_Conexion.TransaccionRollback()
                End If

                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Guarda la informacion del cartel en ajuntos, actualiza el estado de la orden de trabajo y guarda el cambio en la trazabilidad
        ''' </summary>
        ''' <param name="pvo_ArchivoCartel"></param>
        ''' <param name="pvo_Contratacion"></param>
        ''' <param name="pvo_ordenTrabajo"></param>
        ''' <param name="pvn_numEmpleado"></param>
        ''' <param name="pvn_Etapa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>21/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Function GuardarCartel(pvo_ArchivoCartel As EntOttAdjuntoOrdenTrabajo, pvo_Contratacion As EntOttContratacion, pvo_ordenTrabajo As EntOttOrdenTrabajo, pvn_numEmpleado As Integer, pvn_Etapa As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttContratacion As DalOttContratacion
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_DalOttDocumentoContratacion As DalOttDocumentoContratacion
            Dim vlo_EntOttDocumentoContratacion As EntOttDocumentoContratacion
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_resultado = -1
            Dim vln_SiguienteEtapa As Integer

            Dim vlb_transaccion As Boolean

            If vgo_Conexion Is Nothing Then
                vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                vlb_LiberarConexion = True
            Else
                vlo_Conexion = vgo_Conexion
                vlb_LiberarConexion = False
            End If

            Try


                vlo_DalOttContratacion = New DalOttContratacion(vlo_Conexion)
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DalOttDocumentoContratacion = New DalOttDocumentoContratacion(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()
                vlb_transaccion = True
                pvo_Contratacion.IdEtapaContratacion = EtapaContratacion.PUBLICACION_CARTEL
                'Agrega a la contratacion los numero de solicitud y decisión inicial
                vlo_resultado = vlo_DalOttContratacion.ModificarRegistro(pvo_Contratacion)
                'Ingresa los adjuntos
                vlo_resultado = vlo_DalOttAdjuntoOrdenTrabajo.InsertarRegistro(pvo_ArchivoCartel)

                vlo_EntOttDocumentoContratacion = New EntOttDocumentoContratacion

                vlo_EntOttDocumentoContratacion.IdOrdenTrabajo = pvo_ordenTrabajo.IdOrdenTrabajo
                vlo_EntOttDocumentoContratacion.IdUbicacion = pvo_ordenTrabajo.IdUbicacion
                vlo_EntOttDocumentoContratacion.IdEtapaContratacion = EtapaContratacion.PUBLICACION_CARTEL
                vlo_EntOttDocumentoContratacion.FechaHoraRegistro = Now
                vlo_EntOttDocumentoContratacion.IdEtapaOrdenTrabajo = EtapasOrdenTrabajo.CONTRATACIONES
                vlo_EntOttDocumentoContratacion.IdTipoDocumento = TipoDocumento.CARTEL
                vlo_EntOttDocumentoContratacion.Usuario = pvn_numEmpleado
                vlo_EntOttDocumentoContratacion.Version = pvo_Contratacion.Version
                vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo = vlo_resultado
                vlo_EntOttDocumentoContratacion.DocumentoTramitado = Documento.NO_TRAMITADO

                vlo_resultado = vlo_DalOttDocumentoContratacion.InsertarRegistro(vlo_EntOttDocumentoContratacion)


                vln_SiguienteEtapa = vlo_DalOttContratacion.SiguienteEstado(pvn_Etapa)

                Select Case vln_SiguienteEtapa
                    Case EtapaContratacion.EXPEDIENTE_TECNICO
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE
                    Case EtapaContratacion.INICIO
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_INICIO
                    Case EtapaContratacion.PUBLICACION_CARTEL
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_PUBLICACION_CARTEL
                    Case EtapaContratacion.VISITA_TECNICA
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_VISITA_TECNICA
                    Case EtapaContratacion.ACLARACIONES
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_ACLARACIONES
                    Case EtapaContratacion.OFERTAS
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_OFERTAS
                    Case EtapaContratacion.RECOMENDACION_TECNICA
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_RECOMENDACION_TECNICA
                    Case EtapaContratacion.ADJUDICACION
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_ADJUDICACION
                End Select

                vlo_resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_ordenTrabajo)

                If vlo_resultado > 0 Then
                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_ordenTrabajo.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_ordenTrabajo.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_numEmpleado
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_ordenTrabajo.EstadoOrdenTrabajo
                    ' vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                    vlo_EntOttTrazabilidadProceso.Observaciones = String.Empty
                    vlo_EntOttTrazabilidadProceso.Usuario = pvo_ordenTrabajo.Usuario

                    vlo_resultado = vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                End If

                vlo_Conexion.TransaccionCommit()

                Return vlo_resultado
            Catch vlo_Excepcion As Exception
                If vlb_transaccion Then
                    vlo_Conexion.TransaccionRollback()
                End If

                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Acepta el acta de la visita técnica
        ''' </summary>
        ''' <param name="pvo_Archivo"></param>
        ''' <param name="pvo_Contratacion"></param>
        ''' <param name="pvo_ordenTrabajo"></param>
        ''' <param name="pvn_numEmpleado"></param>
        ''' <param name="pvn_Etapa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>21/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Function AceptarVisitaTecnica(pvo_Archivo As EntOttAdjuntoOrdenTrabajo, pvo_Contratacion As EntOttContratacion, pvo_ordenTrabajo As EntOttOrdenTrabajo, pvn_numEmpleado As Integer, pvn_Etapa As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttContratacion As DalOttContratacion
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_DalOttDocumentoContratacion As DalOttDocumentoContratacion
            Dim vlo_EntOttDocumentoContratacion As EntOttDocumentoContratacion
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_resultado = -1
            Dim vln_SiguienteEtapa As Integer

            Dim vlb_transaccion As Boolean

            If vgo_Conexion Is Nothing Then
                vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                vlb_LiberarConexion = True
            Else
                vlo_Conexion = vgo_Conexion
                vlb_LiberarConexion = False
            End If

            Try


                vlo_DalOttContratacion = New DalOttContratacion(vlo_Conexion)
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DalOttDocumentoContratacion = New DalOttDocumentoContratacion(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()
                vlb_transaccion = True

                'Modifica la etapa actual de la contratación
                pvo_Contratacion.IdEtapaContratacion = EtapaContratacion.VISITA_TECNICA
                vlo_resultado = vlo_DalOttContratacion.ModificarRegistro(pvo_Contratacion)


                'Ingresa el archivo adjunto 
                vlo_resultado = vlo_DalOttAdjuntoOrdenTrabajo.InsertarRegistro(pvo_Archivo)


                vlo_EntOttDocumentoContratacion = New EntOttDocumentoContratacion
                'Se crea la instancia de la tabla intermedia con el id del adjunto devuelto en el resultado

                vlo_EntOttDocumentoContratacion.IdOrdenTrabajo = pvo_ordenTrabajo.IdOrdenTrabajo
                vlo_EntOttDocumentoContratacion.IdUbicacion = pvo_ordenTrabajo.IdUbicacion
                vlo_EntOttDocumentoContratacion.IdEtapaContratacion = EtapaContratacion.VISITA_TECNICA
                vlo_EntOttDocumentoContratacion.FechaHoraRegistro = Now
                vlo_EntOttDocumentoContratacion.IdEtapaOrdenTrabajo = EtapasOrdenTrabajo.CONTRATACIONES
                vlo_EntOttDocumentoContratacion.IdTipoDocumento = TipoDocumento.OFICIO
                vlo_EntOttDocumentoContratacion.Usuario = pvn_numEmpleado
                vlo_EntOttDocumentoContratacion.Version = pvo_Contratacion.Version
                vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo = vlo_resultado
                vlo_EntOttDocumentoContratacion.DocumentoTramitado = Documento.NO_TRAMITADO

                vlo_resultado = vlo_DalOttDocumentoContratacion.InsertarRegistro(vlo_EntOttDocumentoContratacion)

                'Se solicita el numero de la siguiente etapa
                vln_SiguienteEtapa = vlo_DalOttContratacion.SiguienteEstado(pvn_Etapa)
                'Se le asigna a la orden el estado correspondiente a esta etapa de contratación
                Select Case vln_SiguienteEtapa
                    Case EtapaContratacion.EXPEDIENTE_TECNICO
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE
                    Case EtapaContratacion.INICIO
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_INICIO
                    Case EtapaContratacion.PUBLICACION_CARTEL
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_PUBLICACION_CARTEL
                    Case EtapaContratacion.VISITA_TECNICA
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_VISITA_TECNICA
                    Case EtapaContratacion.ACLARACIONES
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_ACLARACIONES
                    Case EtapaContratacion.OFERTAS
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_OFERTAS
                    Case EtapaContratacion.RECOMENDACION_TECNICA
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_RECOMENDACION_TECNICA
                    Case EtapaContratacion.ADJUDICACION
                        pvo_ordenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_ADJUDICACION
                End Select
                'Modificar
                vlo_resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_ordenTrabajo)
                'Trazabilidad
                If vlo_resultado > 0 Then
                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_ordenTrabajo.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_ordenTrabajo.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_numEmpleado
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_ordenTrabajo.EstadoOrdenTrabajo
                    'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                    vlo_EntOttTrazabilidadProceso.Observaciones = String.Empty
                    vlo_EntOttTrazabilidadProceso.Usuario = pvo_ordenTrabajo.Usuario

                    vlo_resultado = vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                End If

                vlo_Conexion.TransaccionCommit()

                Return vlo_resultado
            Catch vlo_Excepcion As Exception
                If vlb_transaccion Then
                    vlo_Conexion.TransaccionRollback()
                End If

                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Cierra la etapa de aclaraciones
        ''' </summary>
        ''' <param name="pvn_numEmpleado"></param>
        ''' <param name="pvn_Etapa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>22/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Function CerrarAclaraciones(pvc_idOrdenTrabajo As String, pvn_idUbicacion As Integer, pvn_numEmpleado As Integer, pvn_Etapa As Integer, pvn_Version As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttContratacion As DalOttContratacion
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_EntOttContratacion As EntOttContratacion
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_resultado = -1
            Dim vln_SiguienteEtapa As Integer

            Dim vlb_transaccion As Boolean

            If vgo_Conexion Is Nothing Then
                vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                vlb_LiberarConexion = True
            Else
                vlo_Conexion = vgo_Conexion
                vlb_LiberarConexion = False
            End If

            Try


                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_DalOttContratacion = New DalOttContratacion(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()
                vlb_transaccion = True

                vlo_EntOttContratacion = vlo_DalOttContratacion.ObtenerRegistro(
                    String.Format("{0} = '{1}' AND {2} = {3} AND {4} = {5}",
                                Modelo.OTT_CONTRATACION.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo,
                                Modelo.OTT_CONTRATACION.ID_UBICACION, pvn_idUbicacion,
                                Modelo.OTT_CONTRATACION.VERSION, pvn_Version))

                If vlo_EntOttContratacion.Existe Then
                    vlo_EntOttContratacion.IdEtapaContratacion = EtapaContratacion.ACLARACIONES
                    vlo_resultado = vlo_DalOttContratacion.ModificarRegistro(vlo_EntOttContratacion)
                End If

                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = '{1}' AND {2} = {3}",
                            Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo, Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_idUbicacion))
                If vlo_EntOttOrdenTrabajo.Existe Then

                    vln_SiguienteEtapa = vlo_DalOttContratacion.SiguienteEstado(pvn_Etapa)

                    Select Case vln_SiguienteEtapa
                        Case EtapaContratacion.EXPEDIENTE_TECNICO
                            vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE
                        Case EtapaContratacion.INICIO
                            vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_INICIO
                        Case EtapaContratacion.PUBLICACION_CARTEL
                            vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_PUBLICACION_CARTEL
                        Case EtapaContratacion.VISITA_TECNICA
                            vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_VISITA_TECNICA
                        Case EtapaContratacion.ACLARACIONES
                            vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_ACLARACIONES
                        Case EtapaContratacion.OFERTAS
                            vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_OFERTAS
                        Case EtapaContratacion.RECOMENDACION_TECNICA
                            vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_RECOMENDACION_TECNICA
                        Case EtapaContratacion.ADJUDICACION
                            vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_ADJUDICACION
                    End Select

                    vlo_resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                    If vlo_resultado > 0 Then
                        vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                        vlo_EntOttTrazabilidadProceso.IdUbicacion = vlo_EntOttOrdenTrabajo.IdUbicacion
                        vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = vlo_EntOttOrdenTrabajo.IdOrdenTrabajo
                        vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_numEmpleado
                        vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo
                        ' vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                        vlo_EntOttTrazabilidadProceso.Observaciones = String.Empty
                        vlo_EntOttTrazabilidadProceso.Usuario = vlo_EntOttOrdenTrabajo.Usuario

                        vlo_resultado = vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                    End If
                End If

                vlo_Conexion.TransaccionCommit()

                Return vlo_resultado
            Catch vlo_Excepcion As Exception
                If vlb_transaccion Then
                    vlo_Conexion.TransaccionRollback()
                End If

                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

        End Function

        ''' <summary>
        ''' Cierra la etapa de adjudicacion y por lo tanto de contrataciones
        ''' </summary>
        ''' <param name="pvn_numEmpleado"></param>
        ''' <param name="pvn_Etapa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>09/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Function CerrarAdjudicacion(pvo_Registro As EntOttOrdenTrabajo, pvn_numEmpleado As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_resultado = -1
            Dim vln_SiguienteEtapa As Integer

            Dim vlb_transaccion As Boolean

            If vgo_Conexion Is Nothing Then
                vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                vlb_LiberarConexion = True
            Else
                vlo_Conexion = vgo_Conexion
                vlb_LiberarConexion = False
            End If

            Try

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()
                vlb_transaccion = True

                If pvo_Registro.Existe Then

                    pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.SUPERVISION_OBRA

                    vlo_resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)

                    If vlo_resultado > 0 Then
                        vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                        vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                        vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                        vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_numEmpleado
                        vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_Registro.EstadoOrdenTrabajo
                        'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                        vlo_EntOttTrazabilidadProceso.Observaciones = String.Empty
                        vlo_EntOttTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                        vlo_resultado = vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                    End If
                End If

                vlo_Conexion.TransaccionCommit()

                Return vlo_resultado
            Catch vlo_Excepcion As Exception
                If vlb_transaccion Then
                    vlo_Conexion.TransaccionRollback()
                End If

                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="pvc_idOrdenTrabajo"></param>
        ''' <param name="pvn_idUbicacion"></param>
        ''' <param name="pvn_numEmpleado"></param>
        ''' <param name="pvn_Etapa"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>27/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Function CerrarOfertas(pvc_idOrdenTrabajo As String, pvn_idUbicacion As Integer, pvn_numEmpleado As Integer, pvn_Etapa As Integer, pvb_Desierta As Boolean, pvn_Version As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttContratacion As DalOttContratacion
            Dim vlo_EntOttContratacion As EntOttContratacion
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_resultado = -1
            Dim vln_SiguienteEtapa As Integer

            Dim vlb_transaccion As Boolean

            If vgo_Conexion Is Nothing Then
                vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                vlb_LiberarConexion = True
            Else
                vlo_Conexion = vgo_Conexion
                vlb_LiberarConexion = False
            End If

            Try


                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_DalOttContratacion = New DalOttContratacion(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()
                vlb_transaccion = True


                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = '{1}' AND {2} = {3}",
                                                    Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo, Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_idUbicacion))

                vlo_EntOttContratacion = vlo_DalOttContratacion.ObtenerRegistro(
                    String.Format("{0} = '{1}' AND {2} = {3} AND {4} = {5}",
                                Modelo.OTT_CONTRATACION.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo,
                                Modelo.OTT_CONTRATACION.ID_UBICACION, pvn_idUbicacion,
                                Modelo.OTT_CONTRATACION.VERSION, pvn_Version))

                If vlo_EntOttContratacion.Existe Then
                    vlo_EntOttContratacion.IdEtapaContratacion = EtapaContratacion.ACLARACIONES
                    vlo_resultado = vlo_DalOttContratacion.ModificarRegistro(vlo_EntOttContratacion)
                End If

                If vlo_EntOttOrdenTrabajo.Existe Then

                    If pvb_Desierta Then
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.GESTION_CONTRATACION

                        vlo_EntOttContratacion = vlo_DalOttContratacion.ObtenerRegistro(
                        String.Format("{0} = '{1}' AND {2} = {3} AND {4} = {5}",
                                Modelo.OTT_CONTRATACION.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo,
                                Modelo.OTT_CONTRATACION.ID_UBICACION, pvn_idUbicacion,
                                Modelo.OTT_CONTRATACION.VERSION, pvn_Version))

                        vlo_EntOttContratacion.Editable = Version.NO_EDITABLE

                        vlo_resultado = vlo_DalOttContratacion.ModificarRegistro(vlo_EntOttContratacion)

                        Dim vlo_ordenTrabLista = vlo_DalOttOrdenTrabajo.ListarRegistrosLista(String.Format("{0} = {1} AND {2} = '{3}'",
                                                                    Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION, pvn_idUbicacion,
                                                                    Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo), String.Empty, False, 0, 0)

                        If vlo_ordenTrabLista.Tables.Count > 0 AndAlso vlo_ordenTrabLista.Tables(0).Rows.Count Then
                            Dim vln_NumProfesional = vlo_ordenTrabLista.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.NUM_PROF_ENCARGADO)
                            Dim vln_NumCoordinador = vlo_ordenTrabLista.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.NUM_COORD_ENCARGADO)

                            '{0}: Dia
                            '{1}: Mes
                            '{2}: Año
                            '{3}: Hora
                            '{4}: Minutos
                            '{5}:Nombre del proyecto
                            '{6}:Número de orden de trabajo

                            Dim vlc_Cuerpo = String.Format("<table><tr><th><b style='float: left;'>Universidad de Costa Rica</b></th><td style='float: right;'><b>Fecha:{0}/{1}/{2}</b></td></tr><tr><th><b style='float: left;'>Sistema para el control y seguimiento de Órdenes de Trabajo</b></th><td style='float: right;'><b>Hora:{3}:{4}</b></td></tr></table><p>Se le(s) informa que la contratación para el proyecto {5}, orden de trabajo n° {6} fue declarada desierta por lo que se solicita:</p><p>1. Si es necesario realizar una revisión al presupuesto y los documentos contractuales para realizar mejoras o actualización (Queda en diseño).</p><p>2. Si los documentos y presupuesto son correctos, indicar la fecha y hora de la nueva visita técnica para iniciar un nuevo proceso de contratación.</p><hr><p><i><b>Proceso automatizado de envío de notificaciones. Por favor no responder a este correo</b></i>",
                                                           Date.Now.Day, Date.Now.Month, Date.Now.Year, Date.Now.Hour, Date.Now.Minute, vlo_ordenTrabLista.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.NOMBRE_PROYECTO), vlo_ordenTrabLista.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO))

                            Notificacion(CargarFuncionario(vln_NumProfesional).CORREO_INSTITUCIONAL, vlc_Cuerpo, "Notificación de contratación desierta.")
                            Notificacion(CargarFuncionario(vln_NumCoordinador).CORREO_INSTITUCIONAL, vlc_Cuerpo, "Notificación de contratación desierta.")

                        End If



                    Else
                        vln_SiguienteEtapa = vlo_DalOttContratacion.SiguienteEstado(pvn_Etapa)

                        Select Case vln_SiguienteEtapa
                            Case EtapaContratacion.EXPEDIENTE_TECNICO
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE
                            Case EtapaContratacion.INICIO
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_INICIO
                            Case EtapaContratacion.PUBLICACION_CARTEL
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_PUBLICACION_CARTEL
                            Case EtapaContratacion.VISITA_TECNICA
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_VISITA_TECNICA
                            Case EtapaContratacion.ACLARACIONES
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_ACLARACIONES
                            Case EtapaContratacion.OFERTAS
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_OFERTAS
                            Case EtapaContratacion.RECOMENDACION_TECNICA
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_RECOMENDACION_TECNICA
                            Case EtapaContratacion.ADJUDICACION
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_ADJUDICACION
                        End Select
                    End If


                    vlo_resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                    If vlo_resultado > 0 Then
                        vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                        vlo_EntOttTrazabilidadProceso.IdUbicacion = vlo_EntOttOrdenTrabajo.IdUbicacion
                        vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = vlo_EntOttOrdenTrabajo.IdOrdenTrabajo
                        vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_numEmpleado
                        vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo
                        'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                        vlo_EntOttTrazabilidadProceso.Observaciones = String.Empty
                        vlo_EntOttTrazabilidadProceso.Usuario = vlo_EntOttOrdenTrabajo.Usuario

                        vlo_resultado = vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                    End If
                End If

                vlo_Conexion.TransaccionCommit()

                Return vlo_resultado
            Catch vlo_Excepcion As Exception
                If vlb_transaccion Then
                    vlo_Conexion.TransaccionRollback()
                End If

                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Cierra la etapa de recomendacion tecnica
        ''' </summary>
        ''' <param name="pvo_ArchivoRecomendacion"></param>
        ''' <param name="pvc_idOrdenTrabajo"></param>
        ''' <param name="pvn_idUbicacion"></param>
        ''' <param name="pvn_numEmpleado"></param>
        ''' <param name="pvn_Etapa"></param>
        ''' <param name="pvb_Infructuosa"></param>
        ''' <param name="pvn_Version"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function CerrarRecomendacionTecnica(pvo_ArchivoRecomendacion As EntOttAdjuntoOrdenTrabajo, pvc_idOrdenTrabajo As String, pvn_idUbicacion As Integer, pvn_numEmpleado As Integer, pvn_Etapa As Integer, pvb_Infructuosa As Boolean, pvn_Version As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttContratacion As DalOttContratacion
            Dim vlo_EntOttContratacion As EntOttContratacion
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_DalOttDocumentoContratacion As DalOttDocumentoContratacion
            Dim vlo_EntOttDocumentoContratacion As EntOttDocumentoContratacion
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_resultado = -1
            Dim vln_SiguienteEtapa As Integer

            Dim vlb_transaccion As Boolean

            If vgo_Conexion Is Nothing Then
                vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                vlb_LiberarConexion = True
            Else
                vlo_Conexion = vgo_Conexion
                vlb_LiberarConexion = False
            End If

            Try


                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_DalOttContratacion = New DalOttContratacion(vlo_Conexion)
                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DalOttDocumentoContratacion = New DalOttDocumentoContratacion(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()
                vlb_transaccion = True


                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = '{1}' AND {2} = {3}",
                                                    Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo, Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_idUbicacion))


                If vlo_EntOttOrdenTrabajo.Existe Then

                    If pvb_Infructuosa Then
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.GESTION_CONTRATACION
                        vlo_EntOttContratacion = vlo_DalOttContratacion.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}",
                                                    Modelo.OTT_CONTRATACION.ID_UBICACION, pvn_idUbicacion, Modelo.OTT_CONTRATACION.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo,
                                                    Modelo.OTT_CONTRATACION.VERSION, pvn_Version))

                        vlo_EntOttContratacion.Editable = Version.NO_EDITABLE
                        vlo_EntOttContratacion.IdEtapaContratacion = EtapaContratacion.RECOMENDACION_TECNICA
                        vlo_resultado = vlo_DalOttContratacion.ModificarRegistro(vlo_EntOttContratacion)

                        Dim vlo_ordenTrabLista = vlo_DalOttOrdenTrabajo.ListarRegistrosLista(String.Format("{0} = {1} AND {2} = '{3}'",
                                                                    Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION, pvn_idUbicacion,
                                                                    Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo), String.Empty, False, 0, 0)

                        If vlo_ordenTrabLista.Tables.Count > 0 AndAlso vlo_ordenTrabLista.Tables(0).Rows.Count Then
                            Dim vln_NumProfesional = vlo_ordenTrabLista.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.NUM_PROF_ENCARGADO)
                            Dim vln_NumCoordinador = vlo_ordenTrabLista.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.NUM_COORD_ENCARGADO)

                            '{0}: Dia
                            '{1}: Mes
                            '{2}: Año
                            '{3}: Hora
                            '{4}: Minutos
                            '{5}:Nombre del proyecto
                            '{6}:Número de orden de trabajo

                            Dim vlc_Cuerpo = String.Format("<table><tr><th><b style='float: left;'>Universidad de Costa Rica</b></th><td style='float: right;'><b>Fecha:{0}/{1}/{2}</b></td></tr><tr><th><b style='float: left;'>Sistema para el control y seguimiento de Órdenes de Trabajo</b></th><td style='float: right;'><b>Hora:{3}:{4}</b></td></tr></table><p>Se le(s) informa que la contratación para el proyecto {5}, orden de trabajo n° {6} fue declarada infructuosa por lo que se solicita:</p><p>1. Si es necesario realizar una revisión al presupuesto y los documentos contractuales para realizar mejoras o actualización (Queda en diseño).</p><p>2. Si los documentos y presupuesto son correctos, indicar la fecha y hora de la nueva visita técnica para iniciar un nuevo proceso de contratación.</p><hr><p><i><b>Proceso automatizado de envío de notificaciones. Por favor no responder a este correo</b></i>",
                                                           Date.Now.Day, Date.Now.Month, Date.Now.Year, Date.Now.Hour, Date.Now.Minute, vlo_ordenTrabLista.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.NOMBRE_PROYECTO), vlo_ordenTrabLista.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO))

                            Notificacion(CargarFuncionario(vln_NumProfesional).CORREO_INSTITUCIONAL, vlc_Cuerpo, "Notificación de contratación infructuosa")
                            Notificacion(CargarFuncionario(vln_NumCoordinador).CORREO_INSTITUCIONAL, vlc_Cuerpo, "Notificación de contratación infructuosa")

                        End If

                    Else

                        'Ingresa el archivo adjunto 
                        vlo_resultado = vlo_DalOttAdjuntoOrdenTrabajo.InsertarRegistro(pvo_ArchivoRecomendacion)

                        vlo_EntOttDocumentoContratacion = New EntOttDocumentoContratacion
                        'Se crea la instancia de la tabla intermedia con el id del adjunto devuelto en el resultado

                        vlo_EntOttDocumentoContratacion.IdOrdenTrabajo = vlo_EntOttOrdenTrabajo.IdOrdenTrabajo
                        vlo_EntOttDocumentoContratacion.IdUbicacion = vlo_EntOttOrdenTrabajo.IdUbicacion
                        vlo_EntOttDocumentoContratacion.IdEtapaContratacion = EtapaContratacion.RECOMENDACION_TECNICA
                        vlo_EntOttDocumentoContratacion.FechaHoraRegistro = Now
                        vlo_EntOttDocumentoContratacion.IdEtapaOrdenTrabajo = EtapasOrdenTrabajo.CONTRATACIONES
                        vlo_EntOttDocumentoContratacion.IdTipoDocumento = TipoDocumento.OFICIO
                        vlo_EntOttDocumentoContratacion.Usuario = pvn_numEmpleado
                        vlo_EntOttDocumentoContratacion.Version = pvn_Version
                        vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo = vlo_resultado
                        vlo_EntOttDocumentoContratacion.DocumentoTramitado = Documento.NO_TRAMITADO

                        vlo_resultado = vlo_DalOttDocumentoContratacion.InsertarRegistro(vlo_EntOttDocumentoContratacion)

                        vln_SiguienteEtapa = vlo_DalOttContratacion.SiguienteEstado(pvn_Etapa)

                        Select Case vln_SiguienteEtapa
                            Case EtapaContratacion.EXPEDIENTE_TECNICO
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE
                            Case EtapaContratacion.INICIO
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_INICIO
                            Case EtapaContratacion.PUBLICACION_CARTEL
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_PUBLICACION_CARTEL
                            Case EtapaContratacion.VISITA_TECNICA
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_VISITA_TECNICA
                            Case EtapaContratacion.ACLARACIONES
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_ACLARACIONES
                            Case EtapaContratacion.OFERTAS
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_OFERTAS
                            Case EtapaContratacion.RECOMENDACION_TECNICA
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_RECOMENDACION_TECNICA
                            Case EtapaContratacion.ADJUDICACION
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_ADJUDICACION
                        End Select
                    End If


                    vlo_resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                    If vlo_resultado > 0 Then
                        vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                        vlo_EntOttTrazabilidadProceso.IdUbicacion = vlo_EntOttOrdenTrabajo.IdUbicacion
                        vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = vlo_EntOttOrdenTrabajo.IdOrdenTrabajo
                        vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_numEmpleado
                        vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo
                        'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                        vlo_EntOttTrazabilidadProceso.Observaciones = String.Empty
                        vlo_EntOttTrazabilidadProceso.Usuario = vlo_EntOttOrdenTrabajo.Usuario

                        vlo_resultado = vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                    End If
                End If

                vlo_Conexion.TransaccionCommit()

                Return vlo_resultado
            Catch vlo_Excepcion As Exception
                If vlb_transaccion Then
                    vlo_Conexion.TransaccionRollback()
                End If

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
