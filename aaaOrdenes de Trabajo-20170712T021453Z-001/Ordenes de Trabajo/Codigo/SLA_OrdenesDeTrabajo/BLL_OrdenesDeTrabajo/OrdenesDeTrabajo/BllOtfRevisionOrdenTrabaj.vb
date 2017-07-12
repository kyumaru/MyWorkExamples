Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOtfRevisionOrdenTrabaj
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
		''' Permite agregar un registro en la tabla OTF_REVISION_ORDEN_TRABAJ, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>31/08/2015 08:25:27 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtfRevisionOrdenTrabaj) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtfRevisionOrdenTrabaj As DalOtfRevisionOrdenTrabaj
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdRevisionOrdenTrabaj).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
				End If

				vlo_DalOtfRevisionOrdenTrabaj = New DalOtfRevisionOrdenTrabaj(vlo_Conexion)
				vln_Resultado = vlo_DalOtfRevisionOrdenTrabaj.InsertarRegistro(pvo_Registro)
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
        ''' Permite agregar un registro en la tabla OTF_REVISION_ORDEN_TRABAJ, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>08/09/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRevisionEnvioCorreo(pvo_Registro As EntOtfRevisionOrdenTrabaj, pvn_NumeroEmpleado As Integer, pvc_Descripcion As String, pvc_Motivo As String, pvo_FechaHoraSolicita As Date) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlc_Cuerpo As String
            vlc_Cuerpo = String.Empty
            Dim vlo_DalOtfRevisionOrdenTrabaj As DalOtfRevisionOrdenTrabaj
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOtlTrazabilidadProceso As DalOtlTrazabilidadProceso
            Dim vlo_DalOtpParametro As DalOtpParametroUbicacion
            Dim vlo_EntOtpParametro As EntOtpParametroUbicacion
            Dim vlo_EntOtlTrazabilidadProceso As EntOtlTrazabilidadProceso
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vln_Resultado As Integer
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdRevisionOrdenTrabaj).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente.")
                End If

                vlo_EntEmpleados = CargarFuncionario(pvn_NumeroEmpleado)

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTT_ORDEN_TRABAJO.ANNO, pvo_Registro.Anno, Modelo.OTT_ORDEN_TRABAJO.CONSECUTIVO, pvo_Registro.Consecutivo, Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvo_Registro.IdUbicacion))

                Select Case pvo_Registro.Estado
                    Case EstadoRevision.APROBADA
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.ASIGNADA
                    Case EstadoRevision.DEVUELTA
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.DEVUELTA
                    Case EstadoRevision.DENEGADA
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.DENEGADA
                End Select

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                Select Case pvo_Registro.Estado
                    Case EstadoRevision.DEVUELTA, EstadoRevision.DENEGADA
                        vlo_DalOtlTrazabilidadProceso = New DalOtlTrazabilidadProceso(vlo_Conexion)
                        vlo_EntOtlTrazabilidadProceso = New EntOtlTrazabilidadProceso

                        vlo_EntOtlTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                        vlo_EntOtlTrazabilidadProceso.Anno = pvo_Registro.Anno
                        vlo_EntOtlTrazabilidadProceso.Consecutivo = pvo_Registro.Consecutivo
                        vlo_EntOtlTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumeroEmpleado
                        vlo_EntOtlTrazabilidadProceso.EstadoOrdenTrabajo = pvo_Registro.Estado
                        vlo_EntOtlTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                        vlo_EntOtlTrazabilidadProceso.Observaciones = pvo_Registro.Observaciones
                        vlo_EntOtlTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                        vlo_DalOtlTrazabilidadProceso = New DalOtlTrazabilidadProceso(vlo_Conexion)
                        vlo_DalOtlTrazabilidadProceso.InsertarRegistro(vlo_EntOtlTrazabilidadProceso)
                End Select

                vlo_DalOtfRevisionOrdenTrabaj = New DalOtfRevisionOrdenTrabaj(vlo_Conexion)
                vlo_DalOtfRevisionOrdenTrabaj.InsertarRegistro(pvo_Registro)

                vlo_DalOtpParametro = New DalOtpParametroUbicacion(vlo_Conexion)
                vlo_EntOtpParametro = vlo_DalOtpParametro.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR, Modelo.OTP_PARAMETRO_UBICACION.ID_UBICACION_ADMINISTRA, pvo_Registro.IdUbicacion))

                If (Not String.IsNullOrWhiteSpace(vlo_EntEmpleados.CORREO_INSTITUCIONAL)) And (pvo_Registro.Estado <> EstadoRevision.APROBADA) Then
                    Select Case pvo_Registro.Estado
                        Case EstadoRevision.DEVUELTA
                            vlc_Cuerpo = String.Format("Estimado(a): {0} {1} {2},   Su Orden de trabajo de mantenimiento y construcción de fecha {3} con la descripción: {4}, fue devuelta para corrección por el siguiente motivo: {5}. Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema, refiérase a la dirección {6}.", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2, pvo_FechaHoraSolicita.ToString("dd/MM/yyyy"), pvc_Descripcion, pvc_Motivo, vlo_EntOtpParametro.Valor)
                        Case EstadoRevision.DENEGADA
                            vlc_Cuerpo = String.Format("Estimado(a): {0} {1} {2},   Se deniega el trámite de su Orden de trabajo de mantenimiento y construcción de fecha {3} con la descripción: {4}, fue devuelta para corrección por el siguiente motivo: {5}. Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema, refiérase a la dirección {6}.", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2, pvo_FechaHoraSolicita.ToString("dd/MM/yyyy"), pvc_Descripcion, pvc_Motivo, vlo_EntOtpParametro.Valor)
                    End Select
                    NotificacionRevisionOrdenTrabajo(vlo_EntEmpleados.CORREO_INSTITUCIONAL, vlc_Cuerpo)
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
        ''' <creationDate>08/09/2014</creationDate>
        ''' <changeLog></changeLog>
        Private Sub NotificacionRevisionOrdenTrabajo(pvc_CorreoInstitucional As String, pvc_Cuerpo As String)
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

                    vlo_Notificacion.ASUNTO = "Notificación de Revisión de Orden de Trabajo y Mantenimiento"
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

        ''' <summary>
        ''' Permite obtener un registro según su llave primaria
        ''' </summary>
        ''' <param name="pvn_IdRevisionOrdenTrabaj">Llave primaria de la tabla otf_revision_orden_trabaj que se asocia con la secuencia sq_id_revision_orden_trabaj</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:25:27 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdRevisionOrdenTrabaj As Integer) As EntOtfRevisionOrdenTrabaj
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfRevisionOrdenTrabaj As DalOtfRevisionOrdenTrabaj

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfRevisionOrdenTrabaj = New DalOtfRevisionOrdenTrabaj(vlo_Conexion)
                Return vlo_DalOtfRevisionOrdenTrabaj.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTF_REVISION_ORDEN_TRABAJ.ID_REVISION_ORDEN_TRABAJ, pvn_IdRevisionOrdenTrabaj))
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
        ''' <creationDate>07/09/2015</creationDate>
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


#End Region

    End Class
End Namespace
