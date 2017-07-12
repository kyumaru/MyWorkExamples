Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOtfRevisionPreOrdenTra

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
        ''' Permite agregar un registro en la tabla OTF_REVISION_PRE_ORDEN_TRA, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>19/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtfRevisionPreOrdenTra) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfRevisionPreOrdenTra As DalOtfRevisionPreOrdenTra
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdRevisionPreOrdenTra).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
                End If

                vlo_DalOtfRevisionPreOrdenTra = New DalOtfRevisionPreOrdenTra(vlo_Conexion)
                vln_Resultado = vlo_DalOtfRevisionPreOrdenTra.InsertarRegistro(pvo_Registro)
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
        ''' 
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>19/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRevisionEnvioCorreo(pvo_Registro As EntOtfRevisionPreOrdenTra, pvn_NumeroEmpleado As Integer, pvc_Descripcion As String, pvc_Motivo As String, pvo_FechaHoraSolicita As Date) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlc_Cuerpo As String
            vlc_Cuerpo = String.Empty
            Dim vlo_DalOtfRevisionPreOrdenTra As DalOtfRevisionPreOrdenTra
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOtfPreOrdenTrabajo As DalOtfPreOrdenTrabajo
            Dim vlo_DalOtmCategoriaServicio As DalOtmCategoriaServicio
            Dim vlo_DalOtpParametro As DalOtpParametroUbicacion
            Dim vlo_EntOtpParametro As EntOtpParametroUbicacion
            Dim vlo_EntOtfPreOrdenTrabajo As EntOtfPreOrdenTrabajo
            Dim vln_Resultado As Integer
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
            Dim vlo_EntOtmCategoriaServicio As EntOtmCategoriaServicio

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdRevisionPreOrdenTra).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente.")
                End If

                vlo_EntEmpleados = CargarFuncionario(pvn_NumeroEmpleado)

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOtfPreOrdenTrabajo = New DalOtfPreOrdenTrabajo(vlo_Conexion)
                vlo_EntOtfPreOrdenTrabajo = vlo_DalOtfPreOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_PRE_ORDEN_TRABAJO.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTF_PRE_ORDEN_TRABAJO.ID_PRE_ORDEN_TRABAJO, pvo_Registro.IdPreOrdenTrabajo))

                vlo_DalOtmCategoriaServicio = New DalOtmCategoriaServicio(vlo_Conexion)
                vlo_EntOtmCategoriaServicio = vlo_DalOtmCategoriaServicio.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO, vlo_EntOtfPreOrdenTrabajo.IdCategoriaServicio))

                If pvo_Registro.Estado = EstadoOrden.APROBADA Then
                    If vlo_EntOtmCategoriaServicio.RequiereFichaTecnica = 0 Then
                        vlo_DalOttOrdenTrabajo.EjecutarPrOtAsigOtMante(vlo_EntOtfPreOrdenTrabajo.IdUbicacion, vlo_EntOtfPreOrdenTrabajo.IdPreOrdenTrabajo, vlo_EntOtfPreOrdenTrabajo.CodUnidadSirh, vlo_EntOtfPreOrdenTrabajo.NombrePersonaContacto, vlo_EntOtfPreOrdenTrabajo.Telefono, vlo_EntOtfPreOrdenTrabajo.SennasExactas, vlo_EntOtfPreOrdenTrabajo.DescripcionTrabajo, vlo_EntOtfPreOrdenTrabajo.Usuario, CType(vlo_EntOtfPreOrdenTrabajo.NumEmpleado, Integer), vlo_EntOtfPreOrdenTrabajo.IdCategoriaServicio, vlo_EntOtfPreOrdenTrabajo.IdActividad, vlo_EntOtfPreOrdenTrabajo.IdLugarTrabajo, vlo_EntOtfPreOrdenTrabajo.IncluidaEnRecepcion, vlo_EntOtfPreOrdenTrabajo.IdUbicacionOrigen)
                    Else
                        vlo_DalOttOrdenTrabajo.EjecutarPrOtAsigOtDisenio(vlo_EntOtfPreOrdenTrabajo.IdUbicacion, vlo_EntOtfPreOrdenTrabajo.IdPreOrdenTrabajo, vlo_EntOtfPreOrdenTrabajo.CodUnidadSirh, vlo_EntOtfPreOrdenTrabajo.NombrePersonaContacto, vlo_EntOtfPreOrdenTrabajo.Telefono, vlo_EntOtfPreOrdenTrabajo.SennasExactas, vlo_EntOtfPreOrdenTrabajo.DescripcionTrabajo, vlo_EntOtfPreOrdenTrabajo.Usuario, CType(vlo_EntOtfPreOrdenTrabajo.NumEmpleado, Integer), vlo_EntOtfPreOrdenTrabajo.IdCategoriaServicio, vlo_EntOtfPreOrdenTrabajo.IdActividad, vlo_EntOtfPreOrdenTrabajo.IdLugarTrabajo, vlo_EntOtfPreOrdenTrabajo.IncluidaEnRecepcion, vlo_EntOtfPreOrdenTrabajo.IdUbicacionOrigen)
                    End If
                Else

                    vlo_DalOtfRevisionPreOrdenTra = New DalOtfRevisionPreOrdenTra(vlo_Conexion)
                    vlo_DalOtfRevisionPreOrdenTra.InsertarRegistro(pvo_Registro)

                    vlo_DalOtpParametro = New DalOtpParametroUbicacion(vlo_Conexion)
                    vlo_EntOtpParametro = vlo_DalOtpParametro.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR, Modelo.OTP_PARAMETRO_UBICACION.ID_UBICACION_ADMINISTRA, pvo_Registro.IdUbicacion))

                    If (Not String.IsNullOrWhiteSpace(vlo_EntEmpleados.CORREO_INSTITUCIONAL)) Then
                        Select Case pvo_Registro.Estado
                            Case EstadoRevision.DEVUELTA
                                vlc_Cuerpo = String.Format("Estimado(a): {0} {1} {2},   Su Orden de trabajo de mantenimiento y construcción de fecha {3} con la descripción: {4}, fue devuelta para corrección por el siguiente motivo: {5}. Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema, refiérase a la dirección {6}.", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2, pvo_FechaHoraSolicita.ToString("dd/MM/yyyy"), pvc_Descripcion, pvc_Motivo, vlo_EntOtpParametro.Valor)
                            Case EstadoRevision.DENEGADA
                                vlc_Cuerpo = String.Format("Estimado(a): {0} {1} {2},   Se deniega el trámite de su Orden de trabajo de mantenimiento y construcción de fecha {3} con la descripción: {4}, fue devuelta para corrección por el siguiente motivo: {5}. Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema, refiérase a la dirección {6}.", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2, pvo_FechaHoraSolicita.ToString("dd/MM/yyyy"), pvc_Descripcion, pvc_Motivo, vlo_EntOtpParametro.Valor)
                        End Select
                        NotificacionRevisionOrdenTrabajo(vlo_EntEmpleados.CORREO_INSTITUCIONAL, vlc_Cuerpo)
                    End If
                End If

                vlo_Conexion.TransaccionCommit()
                vln_Resultado = 1
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
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>07/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Sub NotificacionRevisionOrdenTrabajo(pvc_CorreoInstitucional As String, pvc_Cuerpo As String)
            'Dim vlo_WsGestorNotificaciones As WsrGestorNotificaciones.wsGestorNotificaciones
            'Dim vlo_Sistema As WsrGestorNotificaciones.EntGNM_SISTEMA
            'Dim vlo_ListaAdjunto As List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)
            'Dim vlo_ListaDestinatario As List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
            'Dim vlo_Notificacion As WsrGestorNotificaciones.EntGNT_NOTIFICACION
            'Dim vlo_EntGNT_DESTINATARIO As WsrGestorNotificaciones.EntGNT_DESTINATARIO

            'vlo_WsGestorNotificaciones = New WsrGestorNotificaciones.wsGestorNotificaciones
            'vlo_WsGestorNotificaciones.Timeout = -1
            'vlo_WsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
            'vlo_WsGestorNotificaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_GESTOR_NOTIFICACIONES)

            'Try

            '    vlo_Sistema = vlo_WsGestorNotificaciones.GNM_SISTEMA_ObtenerRegistro(
            '        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
            '        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
            '        String.Format("NOMBRE_SISTEMA = '{0}'", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_GN)))

            '    If vlo_Sistema IsNot Nothing AndAlso vlo_Sistema.Existe Then
            '        vlo_Notificacion = New WsrGestorNotificaciones.EntGNT_NOTIFICACION()

            '        vlo_Notificacion.ASUNTO = "Notificación de Revisión de Orden de Trabajo y Mantenimiento"
            '        vlo_Notificacion.CUERPO = pvc_Cuerpo
            '        vlo_Notificacion.ES_HTML = 0
            '        vlo_Notificacion.USUARIO_CREA = System.Environment.UserName.Trim()

            '        vlo_ListaDestinatario = New List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
            '        vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
            '        vlo_EntGNT_DESTINATARIO.DESTINATARIO = pvc_CorreoInstitucional
            '        vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)

            '        vlo_ListaAdjunto = New List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)

            '        vlo_WsGestorNotificaciones.GNT_NOTIFICACION_Registrar(
            '            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
            '            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
            '            vlo_Sistema,
            '            vlo_Notificacion,
            '            vlo_ListaAdjunto.ToArray,
            '            vlo_ListaDestinatario.ToArray)

            '    End If
            'Catch ex As Exception
            '    Throw New OrdenesDeTrabajoException("Error en el envío de correos.")
            'End Try
        End Sub

        ''' <summary>
        ''' Permite obtener un registro según su llave primaria
        ''' </summary>
        ''' <param name="pvn_IdRevisionPreOrdenTra">Llave primaria de la tabla otf_revision_orden_trabaj que se asocia con la secuencia sq_id_revision_orden_trabaj</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>07/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdRevisionPreOrdenTra As Integer) As EntOtfRevisionPreOrdenTra
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfRevisionpreOrdenTra As DalOtfRevisionPreOrdenTra

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfRevisionpreOrdenTra = New DalOtfRevisionPreOrdenTra(vlo_Conexion)
                Return vlo_DalOtfRevisionpreOrdenTra.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTF_REVISION_PRE_ORDEN_TRA.ID_REVISION_PRE_ORDEN_TRA, pvn_IdRevisionPreOrdenTra))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Carga el empleado, segun la el numero de empleado  que obtenga or parametro
        ''' </summary>
        ''' <param name="pvn_NumEmpleado"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>07/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function CargarFuncionario(pvn_NumEmpleado As String) As WsrEU_Curriculo.EntEmpleados
            Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

            vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
            vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsEU_Curriculo.Timeout = -1
            vlo_WsEU_Curriculo.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_EU_CURRICULO)

            Try
                Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    String.Format("NUM_EMPLEADO = '{0}'", pvn_NumEmpleado))
            Catch ex As Exception
                Throw
            Finally
                If vlo_WsEU_Curriculo IsNot Nothing Then
                    vlo_WsEU_Curriculo.Dispose()
                End If
            End Try
        End Function


        ''' <summary>
        ''' carga los datos de la vista para lista de trazabilidad con responsable
        ''' </summary>
        ''' <param name="pvc_Condicion"></param>
        ''' <param name="pvc_Orden"></param>
        ''' <param name="pvb_Paginar"></param>
        ''' <param name="pvn_NumeroPagina"></param>
        ''' <param name="pvn_TamanoPagina"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>11/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarRegistrosListaPersonalizado(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As Data.DataSet
            Dim vlo_WsCatalogoVacaciones As WsrCatalogosVacaciones.WsCatalogosVacaciones
            Dim vlo_Estructura As WsrCatalogosVacaciones.PLM_ESTRUCTURA_ORG
            Dim vlc_EncargadoTramite As String = String.Empty
            Dim vlc_NombreJefe As String
            Dim vlc_Condicion As String
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsRegistros As Data.DataSet
            Dim vlo_DalOtfRevisionPreOrdenTra As DalOtfRevisionPreOrdenTra
            Dim vlc_EstadoOrden As String
            Dim vlc_CodigoSIRH As String
            Dim vlc_NombreSolicitante As String

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfRevisionPreOrdenTra = New DalOtfRevisionPreOrdenTra(vlo_Conexion)

                vlo_DsRegistros = vlo_DalOtfRevisionPreOrdenTra.ListarRegistrosLista(
                    pvc_Condicion,
                    pvc_Orden,
                    pvb_Paginar,
                    pvn_NumeroPagina,
                    pvn_TamanoPagina)

                For Each vlo_Fila In vlo_DsRegistros.Tables(0).Rows

                    vlc_EstadoOrden = vlo_Fila(Modelo.V_OTF_REVISION_PRE_ORDENLST.ESTADO).ToString
                    vlc_CodigoSIRH = vlo_Fila(Modelo.V_OTF_REVISION_PRE_ORDENLST.COD_UNIDAD_SIRH).ToString
                    vlc_NombreSolicitante = vlo_Fila(Modelo.V_OTF_REVISION_PRE_ORDENLST.NOMBRE_SOLICITANTE).ToString

                    If vlc_EstadoOrden = EstadoOrden.PENDIENTE_REVISION_DIRECTOR Then

                        vlo_WsCatalogoVacaciones = New WsrCatalogosVacaciones.WsCatalogosVacaciones
                        vlo_WsCatalogoVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
                        vlo_WsCatalogoVacaciones.Timeout = -1
                        vlo_WsCatalogoVacaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_CATALOGOS_VACACIONES)

                        vlc_NombreJefe = vlo_WsCatalogoVacaciones.PLM_ESTRUCTURA_ORG_ObtenerNombreJefeUnidad(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                        CType(vlc_CodigoSIRH, Integer))

                        vlc_Condicion = String.Format("COD_UNIDAD_SIRH = '{0}' AND TIPO = '{1}' AND ESTADO = '{2}'", vlc_CodigoSIRH, "UBC", Utilerias.OrdenesDeTrabajo.Estado.ACTIVO)

                        vlo_Estructura = vlo_WsCatalogoVacaciones.PLM_ESTRUCTURA_ORG_ObtenerRegistro(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                        vlc_Condicion)

                        vlc_EncargadoTramite = String.Format("{0}({1})", vlc_NombreJefe, vlo_Estructura.DESCRIPCION)

                    ElseIf vlc_EstadoOrden = EstadoOrden.PENDIENTE_DE_ENVIO Or vlc_EstadoOrden = EstadoOrden.DEVUELTA Then
                        vlc_EncargadoTramite = vlc_NombreSolicitante
                    Else
                        vlc_EncargadoTramite = String.Empty
                    End If

                    vlo_Fila(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.RESPONSABLE) = vlc_EncargadoTramite
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
            End Try
        End Function

#End Region

    End Class
End Namespace