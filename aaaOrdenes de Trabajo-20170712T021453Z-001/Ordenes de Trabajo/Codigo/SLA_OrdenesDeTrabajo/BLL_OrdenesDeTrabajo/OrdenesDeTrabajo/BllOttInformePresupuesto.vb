Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttInformePresupuesto
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
        ''' Permite agregar un registro en la tabla OTT_INFORME_PRESUPUESTO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>06/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarInforme(ByVal pvo_Registro As EntOttInformePresupuesto, pvb_CambiarEstado As Boolean, pvc_NombreUsuario As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttInformePresupuesto As DalOttInformePresupuesto
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente")
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo))

                If pvb_CambiarEstado Then
                    vlo_EntOttOrdenTrabajo.Usuario = pvc_NombreUsuario
                    vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PRESUPUESTO_REVISION_COORDINADOR
                    vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)
                End If

                vlo_DalOttInformePresupuesto = New DalOttInformePresupuesto(vlo_Conexion)
                vln_Resultado = vlo_DalOttInformePresupuesto.InsertarRegistro(pvo_Registro)

                vlo_Conexion.TransaccionCommit()

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
        ''' Permite modificar un registro en la tabla OTT_INFORME_PRESUPUESTO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>06/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarInforme(ByVal pvo_Registro As EntOttInformePresupuesto, pvb_CambiarEstado As Boolean, pvc_NombreUsuario As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttInformePresupuesto As DalOttInformePresupuesto
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
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
                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo))

                If pvb_CambiarEstado Then
                    vlo_EntOttOrdenTrabajo.Usuario = pvc_NombreUsuario
                    vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PRESUPUESTO_REVISION_COORDINADOR
                    vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)
                End If

                vlo_DalOttInformePresupuesto = New DalOttInformePresupuesto(vlo_Conexion)
                vln_Resultado = vlo_DalOttInformePresupuesto.ModificarRegistro(pvo_Registro)

                vlo_Conexion.TransaccionCommit()

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
        ''' Permite agregar un registro en la tabla OTT_INFORME_PRESUPUESTO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>06/04/2016 09:13:01 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttInformePresupuesto) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttInformePresupuesto As DalOttInformePresupuesto
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
                End If

                vlo_DalOttInformePresupuesto = New DalOttInformePresupuesto(vlo_Conexion)
                vln_Resultado = vlo_DalOttInformePresupuesto.InsertarRegistro(pvo_Registro)
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
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>06/04/2016 09:13:01 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String) As EntOttInformePresupuesto
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttInformePresupuesto As DalOttInformePresupuesto

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttInformePresupuesto = New DalOttInformePresupuesto(vlo_Conexion)
                Return vlo_DalOttInformePresupuesto.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTT_INFORME_PRESUPUESTO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_INFORME_PRESUPUESTO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper()))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function


        ''' <summary>
        ''' Guarda el archivo de oficio de la disponibilidad presupuestaria y cambia el estado de la orden, ademas envia un correo al coordinador y al jefe de la jefatura con la respuesta respectiva
        ''' </summary>
        ''' <param name="pvo_EntOttAdjuntoOrdenTrabajo"></param>
        ''' <param name="pvb_Enviar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Cesar Bermudez G</author>
        ''' <creationDate>07/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function GuardarAprobacionPresupuestaria(pvo_EntOttAdjuntoOrdenTrabajo As EntOttAdjuntoOrdenTrabajo, pvb_Enviar As Boolean, pvo_UsuariosConRole As String()) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_Resultado As Integer = 1


            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()

                'si el adjunto existe se modifica, si no se ingresa
                If Not pvo_EntOttAdjuntoOrdenTrabajo.Existe Then
                    vlo_Resultado = vlo_DalOttAdjuntoOrdenTrabajo.InsertarRegistro(pvo_EntOttAdjuntoOrdenTrabajo)
                    pvo_EntOttAdjuntoOrdenTrabajo.IdAdjuntoOrdenTrabajo = vlo_Resultado
                    pvo_EntOttAdjuntoOrdenTrabajo.Existe = True
                Else
                    If pvo_EntOttAdjuntoOrdenTrabajo.Archivo IsNot Nothing Then
                        vlo_Resultado = vlo_DalOttAdjuntoOrdenTrabajo.ModificarRegistro(pvo_EntOttAdjuntoOrdenTrabajo)
                    End If
                End If

                If pvb_Enviar Then
                    vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'",
                                                Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION, pvo_EntOttAdjuntoOrdenTrabajo.IdUbicacion,
                                                Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_EntOttAdjuntoOrdenTrabajo.IdOrdenTrabajo))
                    'se obtiene la orden de trabajo y se le cambia el estado de la misma a PRESUPUESTO_APROBADO_SOLICITANTE
                    If vlo_EntOttOrdenTrabajo.Existe Then
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PRESUPUESTO_APROBADO_SOLICITANTE
                        vlo_Resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                        EnviarCorreoRevision(vlo_EntOttOrdenTrabajo, pvb_Enviar)

                    End If
                Else
                    vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'",
                                                Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION, pvo_EntOttAdjuntoOrdenTrabajo.IdUbicacion,
                                                Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_EntOttAdjuntoOrdenTrabajo.IdOrdenTrabajo))
                    'se obtiene la orden de trabajo y se le cambia el estado de la misma a LIQUIDADA
                    If vlo_EntOttOrdenTrabajo.Existe Then
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.LIQUIDADA
                        vlo_Resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)
                        EnviarCorreoRevision(vlo_EntOttOrdenTrabajo, pvb_Enviar)
                    End If
                End If

                vlo_Conexion.TransaccionCommit()

                Return vlo_Resultado

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Envia el correo electrónico y devuelve un valor mayor a cero si tuvo éxito o menor a cero si ocurrió un fallo
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez G</author>
        ''' <creationDate>27/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Function EnviarCorreoRespuesta(pvc_ListaUsuariosRoL As String(), pvc_CorreoCoordinador As String, pvc_CorreoProfesional As String, pvc_NombreSolicitante As String, pvc_CorreoSolicitante As String, pvc_NombreProyecto As String, pvc_idOrdenTrabajo As String, pvb_Enviar As Boolean, pvc_CorreoAdministrador As String) As Integer
            Dim vlo_WsGestorNotificaciones As WsrGestorNotificaciones.wsGestorNotificaciones
            Dim vlo_Sistema As WsrGestorNotificaciones.EntGNM_SISTEMA
            Dim vlo_ListaAdjunto As List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)
            Dim vlo_ListaDestinatario As List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
            Dim vlo_Notificacion As WsrGestorNotificaciones.EntGNT_NOTIFICACION
            Dim vlo_EntGNT_DESTINATARIO As WsrGestorNotificaciones.EntGNT_DESTINATARIO
            Dim vlo_Empleado As WsrEU_Curriculo.EntEmpleados
            Dim vlb_bandera As Boolean
            Dim vln_Resultado As Integer


            vlo_WsGestorNotificaciones = New WsrGestorNotificaciones.wsGestorNotificaciones
            vlo_WsGestorNotificaciones.Timeout = -1
            vlo_WsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsGestorNotificaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_GESTOR_NOTIFICACIONES)

            Try
                vln_Resultado = -1
                vlb_bandera = True
                vlo_Sistema = vlo_WsGestorNotificaciones.GNM_SISTEMA_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    String.Format("NOMBRE_SISTEMA = '{0}'", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_GN)))


                vlo_ListaDestinatario = New List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)


                If vlo_Sistema IsNot Nothing AndAlso vlo_Sistema.Existe Then
                    vlo_Notificacion = New WsrGestorNotificaciones.EntGNT_NOTIFICACION()
                    For Each vlo_fila As String In pvc_ListaUsuariosRoL
                        'Obtiene la Cédula del funcionario actual
                        vlo_Empleado = CargarFuncionario(vlo_fila)
                        If Not String.IsNullOrWhiteSpace(vlo_Empleado.CORREO_INSTITUCIONAL) Then
                            vlo_Notificacion.ES_HTML = 1
                            vlo_Notificacion.USUARIO_CREA = System.Environment.UserName.Trim()

                            If pvb_Enviar Then
                                '{0}: Numero de orden de trabajo
                                vlo_Notificacion.ASUNTO = String.Format("Respuesta al Costo final del proyecto para la orden de trabajo N° {0}", pvc_idOrdenTrabajo)

                                '{0}: Nombre del jefe de mantenimiento
                                '{1}: Apellido 1 del jefe de mantenimiento
                                '{2}: Apellido 2 del jefe de mantenimiento
                                '{3}: Nombre del solicitante
                                '{4}: Nombre del proyecto
                                '{5}: Correo del administrador del sistema

                                vlo_Notificacion.CUERPO = String.Format("<b>Señor(a):{0} {1} {2}</b><br /><br /><b>Estimado(a) señor(a)</b><br />Se le notifica que el señor(a):{3}, ha efectuado la revisión del Costo final del proyecto {4}; Sírvase revisar dicha notificacion en el sistema.<br /><hr><i>Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema refiérase a la dirección: {5}</i>",
                                                   vlo_Empleado.NOMBRE, vlo_Empleado.APELLIDO1, vlo_Empleado.APELLIDO2, pvc_NombreSolicitante, pvc_NombreProyecto, pvc_CorreoAdministrador)

                            Else
                                '{0}: Numero de orden de trabajo
                                vlo_Notificacion.ASUNTO = String.Format("Liquidación de la orden de trabajo N°{0}", pvc_idOrdenTrabajo)
                                '{0}: Nombre del solicitante
                                '{1}: Nombre del proyecto
                                '{2}: Correo del administrador del sistema
                                vlo_Notificacion.CUERPO = String.Format("<b>Señor(a):{0}</b><br /><br /><b>Estimado(a) señor(a)</b><br />Se le notifica que el plazo de respuesta del cual disponía para la revisión del Costo final del proyecto: {1}, ha vencido; motivo por el cual su solicitud ha sido liquidada automáticamente. <br /><hr><i>Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema refiérase a la dirección: {2}</i>",
                                                pvc_NombreSolicitante, pvc_NombreProyecto, pvc_CorreoAdministrador)

                            End If

                            vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
                            vlo_EntGNT_DESTINATARIO.DESTINATARIO = vlo_Empleado.CORREO_INSTITUCIONAL
                            vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)

                            'Solo deben enviarse una vez estos correos, por ello se coloca la bandera
                            If vlb_bandera Then
                                'en el caso de que NO sea aprobación el solicitante debe estar en la lista
                                If Not pvb_Enviar Then
                                    If pvc_CorreoSolicitante <> pvc_CorreoCoordinador AndAlso pvc_CorreoSolicitante <> pvc_CorreoProfesional AndAlso pvc_CorreoSolicitante <> vlo_Empleado.CORREO_INSTITUCIONAL Then
                                        vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
                                        vlo_EntGNT_DESTINATARIO.DESTINATARIO = pvc_CorreoSolicitante
                                        vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)
                                    End If

                                End If
                                'Se agregan los correos del profesional y del coordinador del taller
                                If Not String.IsNullOrWhiteSpace(pvc_CorreoCoordinador) Then
                                    vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
                                    vlo_EntGNT_DESTINATARIO.DESTINATARIO = pvc_CorreoCoordinador
                                    vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)
                                End If

                                If Not String.IsNullOrWhiteSpace(pvc_CorreoProfesional) Then
                                    vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
                                    vlo_EntGNT_DESTINATARIO.DESTINATARIO = pvc_CorreoProfesional
                                    vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)
                                End If
                                vlb_bandera = False
                            End If

                            vlo_ListaAdjunto = New List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)

                            vln_Resultado = vlo_WsGestorNotificaciones.GNT_NOTIFICACION_Registrar(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                                vlo_Sistema,
                                vlo_Notificacion,
                                vlo_ListaAdjunto.ToArray,
                                vlo_ListaDestinatario.ToArray) > 0

                        End If
                    Next

                End If
                Return vln_Resultado
            Catch ex As Exception
                Throw New OrdenesDeTrabajoException("Error en el envío de correos.")
            End Try
        End Function

        ''' <summary>
        ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
        ''' </summary>
        ''' <param name="pvc_Valor"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Cesar bermudez garcia</author>
        ''' <creationDate>27/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function CargarFuncionario(pvc_Valor As String) As WsrEU_Curriculo.EntEmpleados
            Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

            vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
            vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsEU_Curriculo.Timeout = -1
            vlo_WsEU_Curriculo.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_EU_CURRICULO)

            Try
                Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    String.Format("ID_PERSONAL = '{0}' OR NUM_EMPLEADO = {0}", pvc_Valor))
            Catch ex As Exception
                Throw
            Finally
                If vlo_WsEU_Curriculo IsNot Nothing Then
                    vlo_WsEU_Curriculo.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Carga todos los usuarios con un rol especificado por parámetro
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Cesar bermudez garcia</author>
        ''' <creationDate>27/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function CargarUsuariosRol(pvc_RoleName As String) As String()
            Dim vlo_WsOracleRolesProvider As WsrOracleRolesProvider.WsOracleRolesProvider
            Dim vlc_ProviderName As String

            vlo_WsOracleRolesProvider = New WsrOracleRolesProvider.WsOracleRolesProvider
            vlo_WsOracleRolesProvider.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsOracleRolesProvider.Timeout = -1
            vlo_WsOracleRolesProvider.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_ORACLE_ROLES_PROVIDER)

            Try
                vlc_ProviderName = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ROL_PROVIDER_NAME)

                Return vlo_WsOracleRolesProvider.GetUsersInRole(vlc_ProviderName, pvc_RoleName)

            Catch ex As Exception
                Throw
            Finally
                If vlo_WsOracleRolesProvider IsNot Nothing Then
                    vlo_WsOracleRolesProvider.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Calcula los parámetros y los correos de los jefes de mantenimiento, el coordinador sector taller y profesional a cargo para enviarles notificaciones
        ''' </summary>
        ''' <param name="pvo_EntOttOrdenTrabajo"></param>
        ''' <param name="pvb_Enviar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Cesar bermudez garcia</author>
        ''' <creationDate>27/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Function EnviarCorreoRevision(pvo_EntOttOrdenTrabajo As EntOttOrdenTrabajo, pvb_Enviar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_DalOtmSectorTaller As AccesoDatos.Catalogos.DalOtmSectorTaller
            Dim vlo_DalOtpParametroUbicacion As AccesoDatos.Catalogos.DalOtpParametroUbicacion
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab
            Dim vlc_Condicion As String
            Dim vlc_Coordinador As String
            Dim vlc_NombreCoordinador As String
            Dim vlc_JefeSeccion As String
            Dim vlc_CorreoAdministrador As String
            Dim vlc_ProfEncargado As String
            Dim vlc_NombreProfEncargado As String
            Dim vlc_ListaUsuariosRoL As String()
            Dim vlc_NombreSolicitante As String
            Dim vlc_CorreoSolicitante As String
            Dim vlo_DsLugar As Data.DataSet
            Dim vlo_DsParametros As Data.DataSet
            Dim vlo_DsOperarioOrdenTrabajo As Data.DataSet
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                'Se obtiene el correo del profesional encargado
                vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)
                vlo_DalOtmSectorTaller = New AccesoDatos.Catalogos.DalOtmSectorTaller(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New AccesoDatos.Catalogos.DalOtpParametroUbicacion(vlo_Conexion)

                vlc_Condicion = String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION, pvo_EntOttOrdenTrabajo.IdUbicacion,
                                              Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO, pvo_EntOttOrdenTrabajo.IdOrdenTrabajo, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO, Cargo.ENCARGADO)

                vlo_DsOperarioOrdenTrabajo = vlo_DalOttOperarioOrdenTrab.ListarRegistrosLista(vlc_Condicion, String.Format("{0} {1}", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA, Ordenamiento.DESCENDENTE), False, 0, 0)

                If vlo_DsOperarioOrdenTrabajo.Tables(0) IsNot Nothing AndAlso vlo_DsOperarioOrdenTrabajo.Tables(0).Rows.Count > 0 Then
                    vlc_ProfEncargado = vlo_DsOperarioOrdenTrabajo.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA).ToString
                End If

                vlo_EntEmpleados = CargarFuncionario(vlc_ProfEncargado)

                vlc_ProfEncargado = vlo_EntEmpleados.CORREO_INSTITUCIONAL

                vlc_NombreProfEncargado = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)

                'Se obtiene la lista de cedulas de los jefes de mantenimiento y construcción
                vlc_ListaUsuariosRoL = CargarUsuariosRol(RolesSistema.OT_JEFE_SECCION)

                'Se obtiene el correo del coordinador
                vlc_Condicion = String.Format("UPPER({0}) = '{1}'", Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, pvo_EntOttOrdenTrabajo.IdSectorTaller)

                vlo_DsLugar = vlo_DalOtmSectorTaller.ListarRegistrosLista(
                                        vlc_Condicion,
                                        String.Empty,
                                        False,
                                        0,
                                        0)

                vlc_Coordinador = CargarFuncionario(vlo_DsLugar.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NUM_EMPLEADO_COORDINADOR).ToString).CORREO_INSTITUCIONAL

                'Se obtiene el nombre y correo del solicitante
                vlo_EntEmpleados = CargarFuncionario(pvo_EntOttOrdenTrabajo.NumEmpleado)

                vlc_NombreSolicitante = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                vlc_CorreoSolicitante = vlo_EntEmpleados.CORREO_INSTITUCIONAL

                'Se obtiene el correo del profesional
                If vlo_DsOperarioOrdenTrabajo.Tables(0) IsNot Nothing AndAlso vlo_DsOperarioOrdenTrabajo.Tables(0).Rows.Count > 0 Then
                    vlc_ProfEncargado = vlo_DsOperarioOrdenTrabajo.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA).ToString
                End If

                vlo_EntEmpleados = CargarFuncionario(vlc_ProfEncargado)

                vlc_ProfEncargado = vlo_EntEmpleados.CORREO_INSTITUCIONAL



                'Obtiene el correo del administrador del sistema
                vlo_DsParametros = vlo_DalOtpParametroUbicacion.ListarRegistrosLista(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR),
                                                                                  String.Empty, False, 0, 0)

                If vlo_DsParametros.Tables.Count > 0 AndAlso vlo_DsParametros.Tables(0).Rows.Count > 0 Then
                    vlc_CorreoAdministrador = vlo_DsParametros.Tables(0).Rows(0).Item(Modelo.OTP_PARAMETRO_UBICACION.VALOR)
                End If


                Return EnviarCorreoRespuesta(vlc_ListaUsuariosRoL, vlc_Coordinador, vlc_ProfEncargado, vlc_NombreSolicitante, vlc_CorreoSolicitante, pvo_EntOttOrdenTrabajo.NombreProyecto, pvo_EntOttOrdenTrabajo.IdOrdenTrabajo, pvb_Enviar, vlc_CorreoAdministrador)
            Catch ex As Exception
                Throw
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
                If vlo_DsLugar IsNot Nothing Then
                    vlo_DsLugar.Dispose()
                End If
                If vlo_DsParametros IsNot Nothing Then
                    vlo_DsParametros.Dispose()
                End If
                If vlo_DsOperarioOrdenTrabajo IsNot Nothing Then
                    vlo_DsOperarioOrdenTrabajo.Dispose()
                End If
            End Try
        End Function

#End Region

    End Class
End Namespace