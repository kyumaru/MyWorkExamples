Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration
Imports Utilerias.Genericos
Imports Utilerias.Genericos.Extensiones
Imports OrdenesDeTrabajo.LogicaNegocio.Catalogos
Imports System.Data
Imports WsrGestorNotificaciones
Imports WsrOTServicio

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttOrdenTrabajo
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
        ''' <param name="pvo_Registro"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>02/11/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarRegistroPDAGO(ByVal pvo_Registro As EntOttOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vln_Resultado As Integer
            Dim vln_NumOTPDDAGO As Integer = 0
            Dim vlo_DalOtpParametroUbicacion As DalOtpParametroUbicacion
            Dim vlo_EntOtpParametroUbicacion As EntOtpParametroUbicacion

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOtpParametroUbicacion = New DalOtpParametroUbicacion(vlo_Conexion)
                vlo_EntOtpParametroUbicacion = vlo_DalOtpParametroUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.VALOR_PARA_HABILITAR_WS_PDAGO))

                If vlo_EntOtpParametroUbicacion.Existe Then
                    If vlo_EntOtpParametroUbicacion.Valor = 1 Then
                        ''  vln_NumOTPDDAGO = InsertarOrdenTrabajoEnPDAGO(pvo_Registro.DescripcionTrabajo, IIf(pvo_Registro.TipoOrdenTrabajo = TipoOrden.EMERGENCIA, 1, 0), 0)
                        pvo_Registro.NumeroOrden = vln_NumOTPDDAGO
                    End If
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vln_Resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)

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
        ''' 
        ''' </summary>
        ''' <param name="pvc_DesActividad"></param>
        ''' <param name="pvn_Categoria"></param>
        ''' <param name="pvn_OtMadre"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>19/10/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarOrdenTrabajoEnPDAGO(pvc_DesActividad As String, pvn_Categoria As Integer, pvn_OtMadre As Integer) As Integer
            Dim vlo_OrdenTrabajo As WsrOTServicio.OrdenTrabajo
            Dim vlo_OTServicio As WsrOTServicio.OTServicio
            Dim vlc_Mensaje As String = String.Empty
            Dim vln_Resultado As Integer
            Dim vlb_Resultado2 As Boolean = True

            Try

                vlo_OTServicio = New WsrOTServicio.OTServicio
                vlo_OTServicio.Credentials = System.Net.CredentialCache.DefaultCredentials
                vlo_OTServicio.Timeout = -1
                vlo_OTServicio.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_OT_SERVICIO)

                vlo_OrdenTrabajo = New WsrOTServicio.OrdenTrabajo
                vlo_OrdenTrabajo.otr_codigo = 0
                vlo_OrdenTrabajo.uso_codigo = "000001"
                vlo_OrdenTrabajo.act_codigo = "WEB002"
                vlo_OrdenTrabajo.sec_codigo = "WEB001"
                vlo_OrdenTrabajo.edi_codigo = "WEB001"
                vlo_OrdenTrabajo.otr_desc_actividad = pvc_DesActividad
                vlo_OrdenTrabajo.otr_categoria = pvn_Categoria
                vlo_OrdenTrabajo.otr_madre = pvn_OtMadre
                vlo_OrdenTrabajo.otr_Comentario_Recepcion = "Ninguno"

                vlo_OTServicio.insertarOT(vlo_OrdenTrabajo, vlc_Mensaje, vln_Resultado, vlb_Resultado2)

                Return vln_Resultado

            Catch ex As Exception
                Throw New OrdenesDeTrabajoException(vlc_Mensaje)
            End Try
        End Function

        ''' <summary>
        ''' Modifica el estado de la ot, validando los oficios
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <param name="pvb_Gestionar"></param>
        ''' <param name="pvc_justificacion"></param>
        ''' <param name="pvc_UserName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>29/02/2016</creationDate>
        ''' <changeLog>
        '''    <author>Carlos Gómez Ondoy</author>
        '''    <creationDate>29/02/2016</creationDate>
        '''    <change>Se altera el comportamiento del metodo para admitir el informe de valoración presupuestaria</change>
        ''' </changeLog>
        Public Function OTT_ORDEN_TRABAJO_ValidaOficio(pvo_Registro As EntOttOrdenTrabajo, pvb_Gestionar As Boolean, pvc_justificacion As String, pvc_UserName As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOtmSectorTaller As DalOtmSectorTaller
            Dim vlo_DalOtpParametroUbicacion As DalOtpParametroUbicacion
            Dim vlo_BllOttTrazabilidadProceso As BllOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
            Dim vln_Resultado As Integer
            Dim vlc_Coordinador As String
            Dim vlc_NombreSolicitante As String
            Dim vlc_CorreoSolicitante As String
            Dim vlc_CorreoAdministrador As String
            Dim vlc_CorreoProfesional As String
            Dim vlc_ProfEncargado As String
            Dim vlo_DsLugar As Data.DataSet
            Dim vlo_DsParametros As Data.DataSet
            Dim vlo_DsOperarioOrdenTrabajo As Data.DataSet
            Dim vlc_Condicion As String
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

                vlo_EntEmpleados = CargarFuncionario(pvc_UserName)
                vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New DalOtpParametroUbicacion(vlo_Conexion)
                vlo_DalOtmSectorTaller = New DalOtmSectorTaller(vlo_Conexion)
                vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)

                If pvb_Gestionar Then
                    If pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.PRESUPUESTO_APROBADO_SOLICITANTE Then
                        pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.GESTION_CONTRATACION
                    Else
                        pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.EN_ANTEPROYECTO
                    End If

                    vlo_EntOttTrazabilidadProceso.ObservacionesInternas = pvc_justificacion

                Else
                    pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.LIQUIDADA

                    vlc_Condicion = String.Format("UPPER({0}) = '{1}'", Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, pvo_Registro.IdSectorTaller)

                    vlo_DsLugar = vlo_DalOtmSectorTaller.ListarRegistrosLista(
                                            vlc_Condicion,
                                            String.Empty,
                                            False,
                                            0,
                                            0)

                    vlc_Coordinador = CargarFuncionario(vlo_DsLugar.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NUM_EMPLEADO_COORDINADOR).ToString).CORREO_INSTITUCIONAL

                    'Se obtiene el correo del profesional encargado
                    vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)

                    vlc_Condicion = String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION, pvo_Registro.IdUbicacion,
                                                  Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO, Cargo.ENCARGADO)

                    vlo_DsOperarioOrdenTrabajo = vlo_DalOttOperarioOrdenTrab.ListarRegistrosLista(vlc_Condicion, String.Format("{0} {1}", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA, Ordenamiento.DESCENDENTE), False, 0, 0)

                    If vlo_DsOperarioOrdenTrabajo.Tables(0) IsNot Nothing AndAlso vlo_DsOperarioOrdenTrabajo.Tables(0).Rows.Count > 0 Then
                        vlc_ProfEncargado = vlo_DsOperarioOrdenTrabajo.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA).ToString
                    End If
                    vlc_CorreoProfesional = CargarFuncionario(vlc_ProfEncargado).CORREO_INSTITUCIONAL

                    'Se obtiene el nombre y correo del solicitante
                    vlo_EntEmpleados = CargarFuncionario(pvo_Registro.NumEmpleado)

                    vlc_NombreSolicitante = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                    vlc_CorreoSolicitante = vlo_EntEmpleados.CORREO_INSTITUCIONAL

                    'Obtiene el correo del administrador del sistema
                    vlo_DsParametros = vlo_DalOtpParametroUbicacion.ListarRegistrosLista(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR),
                                                                                      String.Empty, False, 0, 0)

                    If vlo_DsParametros.Tables.Count > 0 AndAlso vlo_DsParametros.Tables(0).Rows.Count > 0 Then
                        vlc_CorreoAdministrador = vlo_DsParametros.Tables(0).Rows(0).Item(Modelo.OTP_PARAMETRO_UBICACION.VALOR)
                    End If


                    pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.LIQUIDADA
                    vlo_EntOttTrazabilidadProceso.Observaciones = pvc_justificacion
                    Dim vlc_asunto = String.Format("Liquidación de la orden de trabajo N° {0}", pvo_Registro.IdOrdenTrabajo)
                    Dim vlc_cuerpo = String.Format("<b>Señor(a):{0} {1} {2}</b><br /><br /><b>Estimado(a) señor(a)</b><br />Se le notifica que tras la revisión de su respuesta al Costo final del proyecto: {3}, se ha determinado que no es viable continuar con el mismo; motivo por el cual su solicitud ha sido liquidada.  <br /><br />Se indican las siguientes observaciones:{4}<hr><i>Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema refiérase a la dirección: {5}</i>",
                                            vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2, pvo_Registro.NombreProyecto, pvc_justificacion, vlc_CorreoAdministrador)
                    If Not String.IsNullOrWhiteSpace(vlc_Coordinador) Then
                        NotificacionLiquidada(vlc_Coordinador, vlc_cuerpo, vlc_asunto)
                    End If
                    If Not String.IsNullOrWhiteSpace(vlc_CorreoSolicitante) Then
                        NotificacionLiquidada(vlc_CorreoSolicitante, vlc_cuerpo, vlc_asunto)
                    End If
                    If Not String.IsNullOrWhiteSpace(vlc_CorreoProfesional) Then
                        NotificacionLiquidada(vlc_CorreoProfesional, vlc_cuerpo, vlc_asunto)
                    End If

                End If

                vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = vlo_EntEmpleados.NUM_EMPLEADO
                vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_Registro.EstadoOrdenTrabajo
                'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                vlo_EntOttTrazabilidadProceso.Usuario = pvc_UserName

                vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)

                vlo_BllOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1


                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Modifica el estado de la ot, segun la accion que determine pvb_Aprobar
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <param name="pvb_Gestionar"></param>
        ''' <param name="pvc_justificacion"></param>
        ''' <param name="pvc_UserName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>07/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function AprobacionPresupuestoCoordinador(pvo_Registro As EntOttOrdenTrabajo, pvb_Aprobar As Boolean, pvc_justificacion As String, pvc_UserName As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_BllOttTrazabilidadProceso As BllOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
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

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)

                If pvb_Aprobar Then
                    pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.PRESUPUESTO_APROBADO_COORDINADOR
                    pvo_Registro.Usuario = pvc_UserName
                    vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)
                    vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo))

                    vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PRESUPUESTO_REVISION_JEFATURA
                    vlo_EntOttOrdenTrabajo.Usuario = pvc_UserName
                    vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                Else
                    pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.PRESUPUESTO_DEVUELTO_COORDINADOR
                    pvo_Registro.Usuario = pvc_UserName

                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                    vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = CargarFuncionario(pvc_UserName).NUM_EMPLEADO
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_Registro.EstadoOrdenTrabajo
                    ' vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                    vlo_EntOttTrazabilidadProceso.ObservacionesInternas = pvc_justificacion
                    vlo_EntOttTrazabilidadProceso.Usuario = pvc_UserName

                    vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                    vlo_BllOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                End If

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Modifica el estado de la ot, segun la accion que determine pvb_Aprobar
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <param name="pvo_Adjunto"></param>
        ''' <param name="pvo_Informe"></param>
        ''' <param name="pvb_Aprobar"></param>
        ''' <param name="pvc_justificacion"></param>
        ''' <param name="pvc_UserName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>08/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function AprobacionPresupuestoJefatura(pvo_Registro As EntOttOrdenTrabajo, pvo_Adjunto As EntOttAdjuntoOrdenTrabajo, pvo_Informe As EntOttInformePresupuesto, pvb_Aprobar As Boolean, pvc_justificacion As String, pvc_UserName As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_DalOtpParametroUbicacion As AccesoDatos.Catalogos.DalOtpParametroUbicacion
            Dim vlo_DalOttInformePresupuesto As DalOttInformePresupuesto
            Dim vlo_BllOttTrazabilidadProceso As BllOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vln_Resultado As Integer
            Dim vlc_Asunto As String
            Dim vlo_DsParametros As Data.DataSet
            Dim vlc_Cuerpo As String
            Dim vlc_CorreoAdministrador As String
            Dim vlo_Empleado As WsrEU_Curriculo.EntEmpleados
            Dim vlc_TiempoRespuesta As String
            Dim vlo_EntOtmUnidadTiempo As EntidadNegocio.Catalogos.EntOtmUnidadTiempo
            Dim vlo_DalOtmUnidadTiempo As AccesoDatos.Catalogos.DalOtmUnidadTiempo
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

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOtmUnidadTiempo = New DalOtmUnidadTiempo(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New AccesoDatos.Catalogos.DalOtpParametroUbicacion(vlo_Conexion)

                If pvb_Aprobar Then
                    pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.PRESUPUESTO_APROBADO_JEFATURA
                    pvo_Registro.Usuario = pvc_UserName
                    vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)
                    vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo))

                    vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PRESUPUESTO_PENDIENTE_RESPUESTA_SOLICITANTE
                    vlo_EntOttOrdenTrabajo.Usuario = pvc_UserName
                    vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                    vlo_DalOttInformePresupuesto = New DalOttInformePresupuesto(vlo_Conexion)
                    vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)

                    vlo_DalOttInformePresupuesto.ModificarRegistro(pvo_Informe)
                    vlo_DalOttAdjuntoOrdenTrabajo.InsertarRegistro(pvo_Adjunto)

                    ''CORREO

                    vlo_Empleado = CargarFuncionario(pvo_Registro.NumEmpleado)

                    If Not String.IsNullOrWhiteSpace(vlo_Empleado.CORREO_INSTITUCIONAL) Then

                        vlo_DsParametros = vlo_DalOtpParametroUbicacion.ListarRegistrosLista(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR),
                                                                                              String.Empty, False, 0, 0)

                        'Obtiene el correo del administrador del sistema
                        If vlo_DsParametros.Tables.Count > 0 AndAlso vlo_DsParametros.Tables(0).Rows.Count > 0 Then
                            vlc_CorreoAdministrador = vlo_DsParametros.Tables(0).Rows(0).Item(Modelo.OTP_PARAMETRO_UBICACION.VALOR)
                        End If

                        'obtiene la descripcion para el restante de tiempo de respuesta del solicitante
                        vlo_EntOtmUnidadTiempo = vlo_DalOtmUnidadTiempo.ObtenerRegistro(
                            String.Format("{0} = {1}", Modelo.OTM_UNIDAD_TIEMPO.ID_UNIDAD_TIEMPO, pvo_Informe.IdUnidadTiempo))

                        If pvo_Informe.TiempoRespuesta = 1 Then
                            vlc_TiempoRespuesta = String.Format("{0} {1}", pvo_Informe.TiempoRespuesta, vlo_EntOtmUnidadTiempo.Descripcion)
                        Else
                            vlc_TiempoRespuesta = String.Format("{0} {1}S", pvo_Informe.TiempoRespuesta, vlo_EntOtmUnidadTiempo.Descripcion)
                        End If


                        '{0} : Id orden de trabajo
                        vlc_Asunto = String.Format("Costo final del proyecto para la orden de trabajo N° {0}", pvo_Registro.IdOrdenTrabajo)
                        vlc_Cuerpo = String.Format("<b>Señor(a):{0} {1} {2}</b><br /><br /><b>Estimado(a) señor(a)</b><br />Se le notifica que el Informe de costo final del proyecto para el proyecto: {3}, es de {4}.<br /><br />Le solicitamos verificar el oficio adjunto, el cual puede descargar también desde el sistema de Órdenes de Trabajo. Usted dispone de {5} para dar respuesta mediante sistema, vencido el plazo su solicitud será liquidada.<br /><br />  No debe omitirse el envío físico del oficio a la Unidad.<hr><i>Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema refiérase a la dirección: {6}</i>",
                                                  vlo_Empleado.NOMBRE, vlo_Empleado.APELLIDO1, vlo_Empleado.APELLIDO2, pvo_Registro.NombreProyecto, pvo_Informe.EstimacionPresupuestaria.ToString("C").Replace("$", "₡"), vlc_TiempoRespuesta, vlc_CorreoAdministrador)

                        Notificacion(vlc_Asunto, vlc_Cuerpo, vlo_Empleado.CORREO_INSTITUCIONAL)

                    End If


                Else
                    pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.PRESUPUESTO_DEVUELTO_JEFATURA
                    pvo_Registro.Usuario = pvc_UserName

                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                    vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = CargarFuncionario(pvc_UserName).NUM_EMPLEADO
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_Registro.EstadoOrdenTrabajo
                    'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                    vlo_EntOttTrazabilidadProceso.ObservacionesInternas = pvc_justificacion
                    vlo_EntOttTrazabilidadProceso.Usuario = pvc_UserName

                    vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                    vlo_BllOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                End If

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return vln_Resultado
        End Function

        ''' <summary>
        ''' metodo para Envio Recibido Conforme del solicitante
        ''' </summary>
        ''' <param name="pvo_OrdenTrabajo"></param>
        ''' <param name="pvo_Descripcion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>15/01/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function EnviarParaRecibidoConforme(ByVal pvo_OrdenTrabajo As EntOttOrdenTrabajo, ByVal pvo_Descripcion As String, pvn_NumEmpleado As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_BllOttTrazabilidadProceso As BllOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_DalOtpParametro As DalOtpParametroUbicacion
            Dim vlo_DalOtmActividad As DalOtmActividad
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
            Dim vlo_EntOtpParametroCorreo As EntOtpParametroUbicacion
            Dim vlo_EntOtmActividad As EntOtmActividad
            Dim vlo_EntOtpParametroRecibidoConforme As EntOtpParametroUbicacion
            Dim vln_Resultado As Integer
            vln_Resultado = -1
            Dim vlc_Cuerpo As String
            vlc_Cuerpo = String.Empty

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                pvo_OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.RECIBIDO_CONFORME_SOLICITANTE

                vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_OrdenTrabajo)

                vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_OrdenTrabajo.IdUbicacion
                vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_OrdenTrabajo.IdOrdenTrabajo
                vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_OrdenTrabajo.EstadoOrdenTrabajo
                'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                vlo_EntOttTrazabilidadProceso.Observaciones = pvo_Descripcion
                vlo_EntOttTrazabilidadProceso.Usuario = pvo_OrdenTrabajo.Usuario

                vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                vlo_BllOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                vlo_DalOtmActividad = New DalOtmActividad(vlo_Conexion)
                vlo_EntOtmActividad = vlo_DalOtmActividad.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_ACTIVIDAD, pvo_OrdenTrabajo.IdActividad))

                vlo_DalOtpParametro = New DalOtpParametroUbicacion(vlo_Conexion)
                vlo_EntOtpParametroCorreo = vlo_DalOtpParametro.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR, Modelo.OTP_PARAMETRO_UBICACION.ID_UBICACION_ADMINISTRA, pvo_OrdenTrabajo.IdUbicacion))
                vlo_EntOtpParametroRecibidoConforme = vlo_DalOtpParametro.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.PLAZO_LIMITE_RECIBIDO_CONFORME, Modelo.OTP_PARAMETRO_UBICACION.ID_UBICACION_ADMINISTRA, pvo_OrdenTrabajo.IdUbicacion))

                vlo_EntEmpleados = CargarFuncionarioNumEmpleado(pvo_OrdenTrabajo.NumEmpleado)

                If (Not String.IsNullOrWhiteSpace(vlo_EntEmpleados.CORREO_INSTITUCIONAL)) Then
                    vlc_Cuerpo = String.Format("Estimado(a): {0} {1} {2}, Se le comunica que su orden de trabajo de mantenimiento y construcción N° {3}, para la actividad {7}, ha sido atendida. Se le solicita ingresar al sistema a la dirección: {4} y llenar la solicitud de recibido conforme de la orden de trabajo.Cuenta con {5} día(s) hábil(es) para llenar la solicitud, transcurridos los cuales se asumirá como recibida a satisfacción. Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema, refiérase a la dirección {6}.", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2, pvo_OrdenTrabajo.NumeroOrden, ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_DIRECCION_WEB_PANTALLA), vlo_EntOtpParametroRecibidoConforme.Valor, vlo_EntOtpParametroCorreo.Valor, vlo_EntOtmActividad.Descripcion)
                    Notificacion(vlo_EntEmpleados.CORREO_INSTITUCIONAL, vlc_Cuerpo, pvo_OrdenTrabajo)
                End If

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Procedimiento que se encarga de actualizar el estado a LIQ o NCF a la hora de seleccionar si se rechaza o se acepta que el trabajo fue realizado
        ''' Autor: Mauricio Salas Chaves
        ''' Fecha: 24/09/2015
        ''' </summary>
        ''' <param name="pvo_OrdenTrabajo"></param>
        ''' <param name="pvo_Trazabilidad"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ActualizaEstadoConforme(ByVal pvo_OrdenTrabajo As EntOttOrdenTrabajo, ByVal pvo_Trazabilidad As EntOttTrazabilidadProceso) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_BllOttTrazabilidadProceso As BllOttTrazabilidadProceso
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

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_OrdenTrabajo)

                vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                vlo_BllOttTrazabilidadProceso.InsertarRegistro(pvo_Trazabilidad)

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
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
        ''' <param name="pvo_Registro"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function RecibidoConformeSolicitante(ByVal pvo_Registro As EntOttOrdenTrabajo, pvn_NumEmpleado As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOtpParametro As DalOtpParametroUbicacion
            Dim vlo_DalOtmActividad As DalOtmActividad
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
            Dim vlo_EntOtpParametroCorreo As EntOtpParametroUbicacion
            Dim vlo_EntOtmActividad As EntOtmActividad
            Dim vlo_EntOtpParametroRecibidoConforme As EntOtpParametroUbicacion
            Dim vlo_BllOttTrazabilidadProceso As BllOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlc_Cuerpo As String
            vlc_Cuerpo = String.Empty
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


                vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)

                vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_Registro.EstadoOrdenTrabajo
                ' vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                vlo_EntOttTrazabilidadProceso.Observaciones = String.Empty
                vlo_EntOttTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                vlo_BllOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                vlo_DalOtmActividad = New DalOtmActividad(vlo_Conexion)
                vlo_EntOtmActividad = vlo_DalOtmActividad.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_ACTIVIDAD, pvo_Registro.IdActividad))

                vlo_DalOtpParametro = New DalOtpParametroUbicacion(vlo_Conexion)
                vlo_EntOtpParametroCorreo = vlo_DalOtpParametro.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR, Modelo.OTP_PARAMETRO_UBICACION.ID_UBICACION_ADMINISTRA, pvo_Registro.IdUbicacion))
                vlo_EntOtpParametroRecibidoConforme = vlo_DalOtpParametro.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.PLAZO_LIMITE_RECIBIDO_CONFORME, Modelo.OTP_PARAMETRO_UBICACION.ID_UBICACION_ADMINISTRA, pvo_Registro.IdUbicacion))

                vlo_EntEmpleados = CargarFuncionarioNumEmpleado(pvo_Registro.NumEmpleado)

                If (Not String.IsNullOrWhiteSpace(vlo_EntEmpleados.CORREO_INSTITUCIONAL)) Then
                    vlc_Cuerpo = String.Format("Estimado(a): {0} {1} {2}, Se le comunica que su orden de trabajo de mantenimiento y construcción N° {3}, para la actividad {7}, ha sido atendida. Se le solicita ingresar al sistema a la dirección: {4} y llenar la solicitud de recibido conforme de la orden de trabajo.Cuenta con {5} día(s) hábil(es) para llenar la solicitud, transcurridos los cuales se asumirá como recibida a satisfacción. Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema, refiérase a la dirección {6}.", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2, pvo_Registro.IdOrdenTrabajo, ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_DIRECCION_WEB_PANTALLA), vlo_EntOtpParametroRecibidoConforme.Valor, vlo_EntOtpParametroCorreo.Valor, vlo_EntOtmActividad.Descripcion)
                    Notificacion(vlo_EntEmpleados.CORREO_INSTITUCIONAL, vlc_Cuerpo, pvo_Registro)
                End If

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Metodo para denegacion de la OT hija, registro de trazabilidad
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <param name="pvc_Descripcion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>04/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function DenegacionOrdenTrabajoHijaTrazabilidadInterna(pvo_Registro As EntOttOrdenTrabajo, pvc_Descripcion As String, pvn_NumEmpleado As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOtmSectorTaller As DalOtmSectorTaller
            Dim vlo_BllOttTrazabilidadProceso As BllOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_EntOtmSectorTaller As EntOtmSectorTaller
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
            Dim vln_Resultado As Integer
            Dim vlc_Cuerpo As String
            vlc_Cuerpo = String.Empty
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
                vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)

                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvo_Registro.IdUbicacionMadre, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajoMadre))

                vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_Registro.EstadoOrdenTrabajo
                ' vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                vlo_EntOttTrazabilidadProceso.ObservacionesInternas = pvc_Descripcion
                vlo_EntOttTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                vlo_BllOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                vlo_DalOtmSectorTaller = New DalOtmSectorTaller(vlo_Conexion)
                vlo_EntOtmSectorTaller = vlo_DalOtmSectorTaller.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, vlo_EntOttOrdenTrabajo.IdSectorTaller))

                vlo_EntEmpleados = CargarFuncionarioNumEmpleado(vlo_EntOtmSectorTaller.NumEmpleadoCoordinador)

                vlc_Cuerpo = String.Format("Estimado(a): {0} {1} {2},  Se le informa que no fue avalada la creación de la orden de trabajo de mantenimiento y construcción con consecutivo de orden de trabajo {3} solicitada para realizar el siguiente trabajo:{4} .La misma se deniega por el siguiente motivo: {5}. Favor no contestar este correo, esta cuenta se utiliza únicamente para notificaciones.", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2, pvo_Registro.IdOrdenTrabajo, pvo_Registro.DescripcionTrabajo, pvc_Descripcion)
                NotificacionCoordinadorDenegacion(vlo_EntEmpleados.CORREO_INSTITUCIONAL, vlc_Cuerpo)

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Metodo para denegacion de la OT , registro de trazabilidad
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <param name="pvc_Descripcion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>14/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function RevisionAnteProyectoUsuario(pvo_Registro As EntOttOrdenTrabajo, pvc_Descripcion As String, pvn_NumEmpleado As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOtmSectorTaller As DalOtmSectorTaller
            Dim vlo_BllOttTrazabilidadProceso As BllOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
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
                vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)

                vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_Registro.EstadoOrdenTrabajo
                'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                vlo_EntOttTrazabilidadProceso.ObservacionesInternas = pvc_Descripcion
                vlo_EntOttTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                vlo_BllOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Metodo para denegacion de la OT
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <param name="pvc_Descripcion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>15/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function RevisionAnteProyectoUsuarioAprobacion(pvo_Registro As EntOttOrdenTrabajo, pvc_UserName As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vln_Resultado As Integer
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
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

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)

                pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.ANTEPROYECTO_APROBADO_SOLICITANTE
                pvo_Registro.Usuario = pvc_UserName

                vln_Resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)

                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'",
                        Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvo_Registro.IdUbicacion,
                        Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo))

                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.ELABORACION_DE_PLANOS
                vlo_EntOttOrdenTrabajo.Usuario = pvc_UserName

                vln_Resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                vlo_Conexion.TransaccionCommit()

                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return vln_Resultado
        End Function

        Private Sub NotificacionCoordinadorDenegacion(pvc_CorreoInstitucional As String, pvc_Cuerpo As String)
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

                    vlo_Notificacion.ASUNTO = String.Format("Notificación de denegación creación de orden de trabajo hija de mantenimiento y construcción.")
                    vlo_Notificacion.CUERPO = pvc_Cuerpo
                    vlo_Notificacion.ES_HTML = 1
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
        ''' envio de correo
        ''' </summary>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>02/10/2014</creationDate>
        ''' <changeLog></changeLog>
        Private Sub Notificacion(pvc_CorreoInstitucional As String, pvc_Cuerpo As String, pvo_Registro As EntOttOrdenTrabajo)
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

                    vlo_Notificacion.ASUNTO = String.Format("Solicitud de Recibido Conforme de Orden de Trabajo de Mantenimiento y Construcción N° {0}", pvo_Registro.NumeroOrden)
                    vlo_Notificacion.CUERPO = pvc_Cuerpo
                    vlo_Notificacion.ES_HTML = 1
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
        ''' envio de correo
        ''' </summary>
        ''' <remarks></remarks>
        ''' <author>Cesar Bermudez G</author>
        ''' <creationDate>26/05/2014</creationDate>
        ''' <changeLog></changeLog>
        Private Sub NotificacionLiquidada(pvc_CorreoInstitucional As String, pvc_Cuerpo As String, pvo_Asunto As String)
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

                    vlo_Notificacion.ASUNTO = pvo_Asunto
                    vlo_Notificacion.CUERPO = pvc_Cuerpo
                    vlo_Notificacion.ES_HTML = 1
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
        ''' envio de correo con adjuntos
        ''' </summary>
        ''' <remarks></remarks>
        ''' <author>César Bermudez García</author>
        ''' <creationDate>02/10/2014</creationDate>
        ''' <changeLog></changeLog>
        Private Sub NotificacionConAdjuntos(pvc_CorreoInstitucional As String, pvc_Cuerpo As String, pvo_Registro As EntOttOrdenTrabajo, pvo_EntOttAdjuntoOrdenTrabajo As EntOttAdjuntoOrdenTrabajo)
            Dim vlo_WsGestorNotificaciones As WsrGestorNotificaciones.wsGestorNotificaciones
            Dim vlo_Sistema As WsrGestorNotificaciones.EntGNM_SISTEMA
            Dim vlo_ListaAdjunto As List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)
            Dim vlo_ListaDestinatario As List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
            Dim vlo_Notificacion As WsrGestorNotificaciones.EntGNT_NOTIFICACION
            Dim vlo_EntGNT_DESTINATARIO As WsrGestorNotificaciones.EntGNT_DESTINATARIO
            Dim vlo_adjunto As WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO

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

                    vlo_Notificacion.ASUNTO = String.Format("Solicitud de Recibido Conforme de Orden de Trabajo de Mantenimiento y Construcción N° {0}", pvo_Registro.NumeroOrden)
                    vlo_Notificacion.CUERPO = pvc_Cuerpo
                    vlo_Notificacion.ES_HTML = 1
                    vlo_Notificacion.USUARIO_CREA = System.Environment.UserName.Trim()

                    vlo_ListaDestinatario = New List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
                    vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
                    vlo_EntGNT_DESTINATARIO.DESTINATARIO = pvc_CorreoInstitucional
                    vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)

                    vlo_ListaAdjunto = New List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)
                    vlo_adjunto = New WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO
                    vlo_adjunto.ARCHIVO = pvo_EntOttAdjuntoOrdenTrabajo.Archivo
                    vlo_adjunto.Existe = pvo_EntOttAdjuntoOrdenTrabajo.Existe
                    vlo_adjunto.ID_ARCHIVO_ADJUNTO = pvo_EntOttAdjuntoOrdenTrabajo.IdAdjuntoOrdenTrabajo
                    vlo_adjunto.ID_NOTIFICACION = vlo_Notificacion.ID_NOTIFICACION
                    vlo_adjunto.NOMBRE_ARCHIVO = pvo_EntOttAdjuntoOrdenTrabajo.NombreArchivo
                    vlo_adjunto.UsuarioResponsable = pvo_EntOttAdjuntoOrdenTrabajo.UsuarioResponsable
                    vlo_adjunto.TIME_STAMP = pvo_EntOttAdjuntoOrdenTrabajo.TimeStamp

                    vlo_ListaAdjunto.Add(vlo_adjunto)

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
        ''' Carga el empleado, segun la NumEmpleado que obtenga or parametro
        ''' </summary>
        ''' <param name="pvc_NumEmpleado"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>01/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function CargarFuncionarioNumEmpleado(pvc_NumEmpleado As String) As WsrEU_Curriculo.EntEmpleados
            Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

            vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
            vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsEU_Curriculo.Timeout = -1
            vlo_WsEU_Curriculo.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_EU_CURRICULO)

            Try
                Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    String.Format("NUM_EMPLEADO = {0}", pvc_NumEmpleado))
            Catch ex As Exception
                Throw
            Finally
                If vlo_WsEU_Curriculo IsNot Nothing Then
                    vlo_WsEU_Curriculo.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Permite agregar un registro en la tabla OTT_ORDEN_TRABAJO
        ''' </summary>
        ''' <param name="pvc_UsuarioSesion"></param>
        ''' <param name="pvn_Anno"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>11/09/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarOrdenTrabajoPreventivo(pvc_UsuarioSesion As String, pvn_Anno As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOtfPlaneacionPreventivo As DalOtfPlaneacionPreventivo
            Dim vlo_DalOtpParametro As DalOtpParametroGlobal
            Dim vlo_EntOtpParametro As EntOtpParametroGlobal
            Dim vlo_DsPlanecionPreventivo As Data.DataSet
            Dim vlo_DsEstructuraOT As Data.DataSet
            Dim vlo_DrFilaNueva As Data.DataRow
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
            Dim vln_Resultado As Integer
            vln_Resultado = 0
            Dim vlc_MensajeDeError As String
            vlc_MensajeDeError = "Ha ocurrido un error."

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_EntEmpleados = CargarFuncionario(pvc_UsuarioSesion)

                vlo_DalOtfPlaneacionPreventivo = New DalOtfPlaneacionPreventivo(vlo_Conexion)
                vlo_DsPlanecionPreventivo = vlo_DalOtfPlaneacionPreventivo.ListarRegistros(String.Empty, String.Empty, False, 0, 0)

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DsEstructuraOT = vlo_DalOttOrdenTrabajo.ListarRegistros(String.Format("1 = 0"), String.Empty, False, 0, 0)

                For Each vlo_Fila In vlo_DsPlanecionPreventivo.Tables(0).Rows

                    If ObtenerRegistroPorConsecutivo(CType(vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTF_PLANEACION_PREVENTIVO.ID_UBICACION_ADMINISTRA).ToString, Integer), pvn_Anno, CType(vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTF_PLANEACION_PREVENTIVO.CONSECUTIVO_PROPUESTO).ToString, Integer)).Existe Then
                        vlc_MensajeDeError = String.Format("El No. de consecutivo {0} ya existe para el año {1}", vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTF_PLANEACION_PREVENTIVO.CONSECUTIVO_PROPUESTO).ToString, pvn_Anno)
                        Throw New OrdenesDeTrabajoException(vlc_MensajeDeError)
                    End If

                    vlo_DalOtpParametro = New DalOtpParametroGlobal(vlo_Conexion)
                    vlo_EntOtpParametro = vlo_DalOtpParametro.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_GLOBAL.ID_PARAMETRO, Parametros.CODIGO_UNIDAD_SERVICIOS_GENERALES))

                    If vlo_EntOtpParametro.Existe And vlo_EntEmpleados.Existe Then
                        vlo_DrFilaNueva = vlo_DsEstructuraOT.Tables(0).NewRow
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION)) = CType(vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTF_PLANEACION_PREVENTIVO.ID_UBICACION_ADMINISTRA).ToString, Integer)
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.ANNO)) = pvn_Anno
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.CONSECUTIVO)) = CType(vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTF_PLANEACION_PREVENTIVO.CONSECUTIVO_PROPUESTO).ToString, Integer)
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO)) = String.Format("{0}-{1}", Right(pvn_Anno.ToString, 2), vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTF_PLANEACION_PREVENTIVO.CONSECUTIVO_PROPUESTO).ToString)

                        ''vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTF_PLANEACION_PREVENTIVO.CONSECUTIVO_PROPUESTO).ToString

                        ''String.Format("{0}-{1}", Right(pvo_Registro.Anno.ToString, 2), pvo_Registro.Consecutivo)

                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.TIPO_ORDEN_TRABAJO)) = TipoOrden.PREVENTIVO
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO)) = EstadoOrden.ASIGNADA
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.NUM_EMPLEADO)) = vlo_EntEmpleados.NUM_EMPLEADO
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.ID_CATEGORIA_SERVICIO)) = CType(vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTF_PLANEACION_PREVENTIVO.ID_CATEGORIA_SERVICIO).ToString, Integer)
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.ID_ACTIVIDAD)) = CType(vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTF_PLANEACION_PREVENTIVO.ID_ACTIVIDAD).ToString, Integer)
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.ID_LUGAR_TRABAJO)) = CType(vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTF_PLANEACION_PREVENTIVO.ID_LUGAR_TRABAJO).ToString, Integer)
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.FECHA_HORA_SOLICITA)) = DateTime.Now
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.COD_UNIDAD_SIRH)) = vlo_EntOtpParametro.Valor
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.SENNAS_EXACTAS)) = Constantes.SENNAS_EXACTAS
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.DESCRIPCION_TRABAJO)) = Constantes.DESCRIPCION_TRABAJO
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.USUARIO)) = pvc_UsuarioSesion
                        vlo_DrFilaNueva.Item(vlo_DsEstructuraOT.Tables(0).Columns(Modelo.OTT_ORDEN_TRABAJO.PARENTESCO)) = "MAD"
                        vlo_DsEstructuraOT.Tables(0).Rows.Add(vlo_DrFilaNueva)

                        vln_Resultado = vln_Resultado + 1
                    Else
                        Return -1
                    End If
                Next

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo.AdapterOttOrdenesTrabajo(vlo_DsEstructuraOT)

                vlo_Conexion.TransaccionCommit()


                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException(vlc_MensajeDeError)
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return -1
        End Function


        ''' <summary>
        ''' Permite modificar el numeor de OT PDGAO de OTT_ORDEN_TRABAJO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>04/11/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarNumeroOT(ByVal pvo_Registro As EntOttOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
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

                vlo_EntOttOrdenTrabajo = ObtenerRegistroPorLlaveAlterna(pvo_Registro.NumeroOrden)
                If vlo_EntOttOrdenTrabajo.Existe And (vlo_EntOttOrdenTrabajo.IdUbicacion <> pvo_Registro.IdUbicacion Or vlo_EntOttOrdenTrabajo.IdOrdenTrabajo <> pvo_Registro.IdOrdenTrabajo) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede modificar el registro, ya existe una de orden de trabajo con el número de orden indicado.")
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vln_Resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)
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
        ''' obtien registros por llava alterna de numero de orden
        ''' </summary>
        ''' <param name="pvn_NumeroOrden"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>04/11/2015</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlaveAlterna(pvn_NumeroOrden As Integer) As EntOttOrdenTrabajo
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                Return vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_ORDEN_TRABAJO.NUMERO_ORDEN, pvn_NumeroOrden))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Se obtiene el Nombre del Encargado del Tramite
        ''' </summary>
        ''' <param name="pvc_EstadoOrden"></param>
        ''' <param name="pvc_CodigoSIRH"></param>
        ''' <param name="pvc_IdSectorTaller"></param>
        ''' <param name="pvc_NombreSolicitante"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Mauricio Salas Chaves</author>
        ''' <creationDate>22/09/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerEncargadoTramite(pvc_EstadoOrden As String, pvc_CodigoSIRH As String, pvc_IdSectorTaller As String, pvc_NombreSolicitante As String) As String
            Dim vlo_DsLugar As System.Data.DataSet
            Dim vlo_WsCatalogoVacaciones As WsrCatalogosVacaciones.WsCatalogosVacaciones
            Dim vlo_DalOtmSectorTaller As DalOtmSectorTaller
            Dim vlo_Estructura As WsrCatalogosVacaciones.PLM_ESTRUCTURA_ORG
            Dim vlc_EncargadoTramite As String
            Dim vlc_NombreJefe As String
            Dim vlc_Condicion As String
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean

            Try

                If pvc_EstadoOrden = EstadoOrden.PENDIENTE_REVISION_DIRECTOR Then

                    vlo_WsCatalogoVacaciones = New WsrCatalogosVacaciones.WsCatalogosVacaciones
                    vlo_WsCatalogoVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
                    vlo_WsCatalogoVacaciones.Timeout = -1
                    vlo_WsCatalogoVacaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_CATALOGOS_VACACIONES)

                    vlc_NombreJefe = vlo_WsCatalogoVacaciones.PLM_ESTRUCTURA_ORG_ObtenerNombreJefeUnidad(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    CType(pvc_CodigoSIRH, Integer))

                    vlc_Condicion = String.Format("COD_UNIDAD_SIRH = '{0}' AND TIPO = '{1}' AND ESTADO = '{2}'", pvc_CodigoSIRH, "UBC", Utilerias.OrdenesDeTrabajo.Estado.ACTIVO)

                    vlo_Estructura = vlo_WsCatalogoVacaciones.PLM_ESTRUCTURA_ORG_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    vlc_Condicion)

                    vlc_EncargadoTramite = String.Format("{0}({1})", vlc_NombreJefe, vlo_Estructura.DESCRIPCION)

                ElseIf pvc_EstadoOrden = EstadoOrden.ASIGNADA Or pvc_EstadoOrden = EstadoOrden.EN_PROCESO Then

                    If vgo_Conexion Is Nothing Then
                        vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                        vlb_LiberarConexion = True
                    Else
                        vlo_Conexion = vgo_Conexion
                        vlb_LiberarConexion = False
                    End If

                    vlc_Condicion = String.Format("UPPER({0}) = '{1}'", Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, pvc_IdSectorTaller)

                    vlo_DalOtmSectorTaller = New DalOtmSectorTaller(vlo_Conexion)

                    vlo_DsLugar = vlo_DalOtmSectorTaller.ListarRegistrosLista(
                                            vlc_Condicion,
                                            String.Empty,
                                            False,
                                            0,
                                            0)

                    vlc_EncargadoTramite = String.Format("{0}({1})", vlo_DsLugar.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE_COORDINADOR), vlo_DsLugar.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE))
                ElseIf pvc_EstadoOrden = EstadoOrden.RECIBIDO_CONFORME_SOLICITANTE Then
                    vlc_EncargadoTramite = pvc_NombreSolicitante
                Else
                    vlc_EncargadoTramite = ""
                End If

                Return vlc_EncargadoTramite

            Catch ex As Exception
                Throw
            Finally
                If vlo_WsCatalogoVacaciones IsNot Nothing Then
                    vlo_WsCatalogoVacaciones.Dispose()
                End If
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <param name="pvn_idMotivoRechazo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function RechazarOrdenTrabajo(ByVal pvo_Registro As EntOttOrdenTrabajo, ByVal pvn_idMotivoRechazo As Integer, ByVal pvn_NumEmpleado As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_DalOttTiempoOperario As DalOttTiempoOperario
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab
            Dim vlo_EntOttTiempoOperario As EntOttTiempoOperario
            Dim vlo_EntOttOperarioOrdenTrab As EntOttOperarioOrdenTrab
            Dim vlo_DsTiempos As Data.DataSet
            Dim vlo_DsOperarios As Data.DataSet
            Dim vln_Resultado As Integer
            vln_Resultado = -1

            Dim vlb_Bandera As Boolean = True

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                If pvo_Registro.Parentesco = "MAD" Then
                    pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.EN_ESTUDIO
                Else
                    pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.PENDIENTE_REVISION_SUPERVISOR
                End If

                pvo_Registro.IdMotivoRechazo = pvn_idMotivoRechazo
                pvo_Registro.Usuario = pvo_Registro.Usuario

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)

                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                vlo_EntOttTrazabilidadProceso.IdMotivoRechazo = pvn_idMotivoRechazo
                vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_Registro.EstadoOrdenTrabajo
                'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                vlo_EntOttTrazabilidadProceso.Observaciones = String.Empty
                vlo_EntOttTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
                vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)

                While vlb_Bandera
                    vlo_EntOttTiempoOperario = New EntOttTiempoOperario
                    vlo_EntOttTiempoOperario = vlo_DalOttTiempoOperario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_TIEMPO_OPERARIO.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo))

                    If vlo_EntOttTiempoOperario.Existe Then
                        vlo_DalOttTiempoOperario.BorrarRegistro(vlo_EntOttTiempoOperario)
                        vlb_Bandera = True
                    Else
                        vlb_Bandera = False
                    End If
                End While

                vlb_Bandera = True

                While vlb_Bandera
                    vlo_EntOttOperarioOrdenTrab = New EntOttOperarioOrdenTrab
                    vlo_EntOttOperarioOrdenTrab = vlo_DalOttOperarioOrdenTrab.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_TIEMPO_OPERARIO.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo))

                    If vlo_EntOttOperarioOrdenTrab.Existe Then
                        vlo_DalOttOperarioOrdenTrab.BorrarRegistro(vlo_EntOttOperarioOrdenTrab)
                        vlb_Bandera = True
                    Else
                        vlb_Bandera = False
                    End If
                End While

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite modificar un registro en la tabla OTT_ORDEN_TRABAJO, y sus respectivos adjuntos
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <param name="pvo_DsAdjuntos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos gómez Ondoy</author>
        ''' <creationDate>03/09/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarOrdenTrabajoConAdjuntos(ByVal pvo_Registro As EntOttOrdenTrabajo, ByVal pvo_DsAdjuntos As Data.DataSet, pvn_IdTipoDoc As Integer, pvn_IdEtapaOrdenTrabajo As Integer, pvc_Descripcion As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_DsActuales As Data.DataSet
            Dim vlo_DsNuevos As Data.DataSet
            Dim vln_Resultado As Integer
            Dim vlo_DrFilaNueva As Data.DataRow
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

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)

                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DsActuales = vlo_DalOttAdjuntoOrdenTrabajo.ListarRegistros(
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = '{7}'",
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO, pvn_IdTipoDoc,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO, pvn_IdEtapaOrdenTrabajo,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION, pvo_Registro.IdUbicacion,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo),
                    String.Empty,
                    False,
                    0,
                    0)

                For Each vlo_Fila In vlo_DsActuales.Tables(0).Rows
                    vlo_Fila.Delete()
                Next

                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DalOttAdjuntoOrdenTrabajo.AdapterOtTAdjunto(vlo_DsActuales)

                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DsNuevos = vlo_DalOttAdjuntoOrdenTrabajo.ListarRegistros(String.Format("1 = 0"), String.Empty, False, 0, 0)

                For Each vlo_Fila In pvo_DsAdjuntos.Tables(0).Rows
                    vlo_DrFilaNueva = vlo_DsNuevos.Tables(0).NewRow
                    vlo_DrFilaNueva.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION)) = pvo_Registro.IdUbicacion
                    vlo_DrFilaNueva.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO)) = pvo_Registro.IdOrdenTrabajo
                    vlo_DrFilaNueva.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO)) = pvn_IdTipoDoc
                    vlo_DrFilaNueva.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.DESCRIPCION)) = pvc_Descripcion
                    vlo_DrFilaNueva.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO)) = pvn_IdEtapaOrdenTrabajo
                    vlo_DrFilaNueva.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)) = vlo_Fila(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)
                    vlo_DrFilaNueva.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO)) = vlo_Fila(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO)
                    vlo_DrFilaNueva.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.USUARIO)) = vlo_Fila(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.USUARIO)
                    vlo_DsNuevos.Tables(0).Rows.Add(vlo_DrFilaNueva)
                Next

                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DalOttAdjuntoOrdenTrabajo.AdapterOtTAdjunto(vlo_DsNuevos)

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
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
        ''' <param name="pvo_Registro"></param>
        ''' <param name="pvo_DsAdjuntos"></param>
        ''' <param name="pvn_IdTipoDoc"></param>
        ''' <param name="pvn_IdEtapaOrdenTrabajo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function InsertarOrdenTrabajoConAdjuntosCadena(ByVal pvo_Registro As EntOttOrdenTrabajo, ByVal pvo_DsAdjuntos As Data.DataSet, pvn_IdTipoDoc As Integer, pvn_IdEtapaOrdenTrabajo As Integer, pvc_Descripcion As String) As String
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlc_Resultado As String
            vlc_Resultado = String.Empty

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                pvo_Registro.Consecutivo = CType(vlo_DalOttOrdenTrabajo.ObtenerFnOtConsecutivoOrden(pvo_Registro.Anno, pvo_Registro.IdUbicacion), Integer) + 1
                pvo_Registro.IdOrdenTrabajo = String.Format("{0}-{1}", Right(pvo_Registro.Anno.ToString, 2), pvo_Registro.Consecutivo)

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo).Existe Then
                    vlc_Resultado = String.Empty
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo.InsertarRegistro(pvo_Registro)

                For Each vlo_Fila In pvo_DsAdjuntos.Tables(0).Rows
                    vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION) = pvo_Registro.IdUbicacion
                    vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO) = pvo_Registro.IdOrdenTrabajo
                    vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO) = pvn_IdTipoDoc
                    vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO) = pvn_IdEtapaOrdenTrabajo
                    vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.DESCRIPCION) = pvc_Descripcion
                Next

                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DalOttAdjuntoOrdenTrabajo.AdapterOtTAdjunto(pvo_DsAdjuntos)

                vlo_Conexion.TransaccionCommit()

                vlc_Resultado = String.Format("{0}¬{1}", pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo)
                Return vlc_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return vlc_Resultado
        End Function


        ''' <summary>
        ''' Permite agregar un registro en la tabla OTT_ORDEN_TRABAJO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' ademas los archivos adjuntos al documento
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <param name="pvo_DsAdjuntos">Archivos adjuntos a la Orden</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos gómez Ondoy</author>
        ''' <creationDate>03/09/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarOrdenTrabajoConAdjuntos(ByVal pvo_Registro As EntOttOrdenTrabajo, ByVal pvo_DsAdjuntos As Data.DataSet, pvn_IdTipoDoc As Integer, pvn_IdEtapaOrdenTrabajo As Integer, pvc_Descripcion As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
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

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                pvo_Registro.Consecutivo = CType(vlo_DalOttOrdenTrabajo.ObtenerFnOtConsecutivoOrden(pvo_Registro.Anno, pvo_Registro.IdUbicacion), Integer) + 1
                pvo_Registro.IdOrdenTrabajo = String.Format("{0}-{1}", Right(pvo_Registro.Anno.ToString, 2), pvo_Registro.Consecutivo)

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo.InsertarRegistro(pvo_Registro)

                For Each vlo_Fila In pvo_DsAdjuntos.Tables(0).Rows
                    vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION) = pvo_Registro.IdUbicacion
                    vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO) = pvo_Registro.IdOrdenTrabajo
                    vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO) = pvn_IdTipoDoc
                    vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO) = pvn_IdEtapaOrdenTrabajo
                    vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.DESCRIPCION) = pvc_Descripcion
                Next

                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DalOttAdjuntoOrdenTrabajo.AdapterOtTAdjunto(pvo_DsAdjuntos)

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
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
        ''' <param name="pvo_Registro"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>23/09/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorradoLogico(ByVal pvo_Registro As EntOttOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If
                If pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.PENDIENTE_DE_ENVIO Or pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.DEVUELTA Then
                    vlo_Conexion.TransaccionBegin()

                    pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.BORRADA

                    vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                    vln_Resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)

                    vlo_Conexion.TransaccionCommit()

                Else
                    Throw New OrdenesDeTrabajoException("Esta orden no puede ser borrada, para poder borrarla debe de estar en estado pendiente o devuelta")
                End If
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
        ''' Permite agregar un registro en la tabla OTT_ORDEN_TRABAJO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>04/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarOrdenTrabajoHija(ByVal pvo_Registro As EntOttOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOtmCategoriaServicio As DalOtmCategoriaServicio
            Dim vlo_EntOtmCategoriaServicio As EntOtmCategoriaServicio
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If
                vlo_DalOtmCategoriaServicio = New DalOtmCategoriaServicio(vlo_Conexion)
                vlo_EntOtmCategoriaServicio = vlo_DalOtmCategoriaServicio.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO, pvo_Registro.IdCategoriaServicio))

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                pvo_Registro.ConsecutivoHija = CType(vlo_DalOttOrdenTrabajo.ObtenerFnOtConsecutivoOrdenHija(pvo_Registro.Anno, pvo_Registro.IdUbicacion, pvo_Registro.IdCategoriaServicio, pvo_Registro.IdOrdenTrabajo), Integer) + 1
                pvo_Registro.IdOrdenTrabajo = String.Format("{0}-{1}{2}", pvo_Registro.IdOrdenTrabajo, vlo_EntOtmCategoriaServicio.Siglas, pvo_Registro.ConsecutivoHija)

                If ObtenerRegistroPorCategoriaActividad(pvo_Registro.IdCategoriaServicio, pvo_Registro.IdActividad, pvo_Registro.IdUbicacionMadre, pvo_Registro.IdOrdenTrabajoMadre).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Ya existe una orden de trabajo hija con la categoría y actividad indicadas.")
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vln_Resultado = vlo_DalOttOrdenTrabajo.InsertarRegistro(pvo_Registro)
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
        ''' se encarga de actualizar el estado de la oreden, y actualizar los registros de operario OT, y y tiempo
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <param name="pvd_FechaFinalizacion"></param>
        ''' <param name="pvn_TiempoInvertido"></param>
        ''' <param name="pvn_UnidadTiempo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>03/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function EnviarParaRecibidoConformeActualizacionOperarios(pvo_Registro As EntOttOrdenTrabajo, pvd_FechaFinalizacion As DateTime, pvn_TiempoInvertido As Integer, pvn_UnidadTiempo As Integer, pvb_CambiaEstado As Boolean, pvc_UserName As String, pvn_NumEmpleado As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab
            Dim vlo_DalOttTiempoOperario As DalOttTiempoOperario
            Dim vlo_DsOperarioOrdenTrabajo As Data.DataSet
            Dim vlo_DsTiempoOperario As Data.DataSet
            Dim vlo_DsTiempoOperarionNuevo As Data.DataSet
            Dim vlo_DrFilaTiempoOperarioNuevo As DataRow
            Dim vlo_DalOtpParametro As DalOtpParametroUbicacion
            Dim vlo_DalOtmActividad As DalOtmActividad
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
            Dim vlo_EntOtpParametroCorreo As EntOtpParametroUbicacion
            Dim vlo_EntOtmActividad As EntOtmActividad
            Dim vlo_EntOtpParametroRecibidoConforme As EntOtpParametroUbicacion
            Dim vlo_BllOttTrazabilidadProceso As BllOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlc_Cuerpo As String
            vlc_Cuerpo = String.Empty
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

                ' Si pvb_CambiaEstado es TRUE entonces cambia el estado de la orden
                If pvb_CambiaEstado Then
                    vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                    pvo_Registro.EstadoOrdenTrabajo = EstadoOrden.RECIBIDO_CONFORME_SOLICITANTE
                    pvo_Registro.Usuario = pvc_UserName

                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_Registro)

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = pvo_Registro.EstadoOrdenTrabajo
                    ' vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                    vlo_EntOttTrazabilidadProceso.Observaciones = String.Empty
                    vlo_EntOttTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                    vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                    vlo_BllOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                    vlo_DalOtmActividad = New DalOtmActividad(vlo_Conexion)
                    vlo_EntOtmActividad = vlo_DalOtmActividad.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_ACTIVIDAD, pvo_Registro.IdActividad))

                    vlo_DalOtpParametro = New DalOtpParametroUbicacion(vlo_Conexion)
                    vlo_EntOtpParametroCorreo = vlo_DalOtpParametro.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR, Modelo.OTP_PARAMETRO_UBICACION.ID_UBICACION_ADMINISTRA, pvo_Registro.IdUbicacion))
                    vlo_EntOtpParametroRecibidoConforme = vlo_DalOtpParametro.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.PLAZO_LIMITE_RECIBIDO_CONFORME, Modelo.OTP_PARAMETRO_UBICACION.ID_UBICACION_ADMINISTRA, pvo_Registro.IdUbicacion))

                    vlo_EntEmpleados = CargarFuncionarioNumEmpleado(pvo_Registro.NumEmpleado)

                    If (Not String.IsNullOrWhiteSpace(vlo_EntEmpleados.CORREO_INSTITUCIONAL)) Then
                        vlc_Cuerpo = String.Format("Estimado(a): {0} {1} {2}, Se le comunica que su orden de trabajo de mantenimiento y construcción N° {3}, para la actividad {7}, ha sido atendida. Se le solicita ingresar al sistema a la dirección: {4} y llenar la solicitud de recibido conforme de la orden de trabajo.Cuenta con {5} día(s) hábil(es) para llenar la solicitud, transcurridos los cuales se asumirá como recibida a satisfacción. Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema, refiérase a la dirección {6}.", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2, pvo_Registro.IdOrdenTrabajo, ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_DIRECCION_WEB_PANTALLA), vlo_EntOtpParametroRecibidoConforme.Valor, vlo_EntOtpParametroCorreo.Valor, vlo_EntOtmActividad.Descripcion)
                        Notificacion(vlo_EntEmpleados.CORREO_INSTITUCIONAL, vlc_Cuerpo, pvo_Registro)
                    End If

                End If

                ' Carga los operios relacionados a la OT
                vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)
                vlo_DsOperarioOrdenTrabajo = vlo_DalOttOperarioOrdenTrab.ListarRegistros(
                    String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo),
                    String.Empty, False, 0, 0)

                'Carga la estructura de los tiempos para operario
                vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
                vlo_DsTiempoOperarionNuevo = vlo_DalOttTiempoOperario.ListarRegistros(String.Format("1 = 0"), String.Empty, False, 0, 0)

                'Asigna la nueva fecha de finalizacion (FECHA_EJECUTA), y Crea los nuevos registros de tiempos para operarios
                For Each vlo_FilaOperarioOrdenTrabajo In vlo_DsOperarioOrdenTrabajo.Tables(0).Rows
                    vlo_FilaOperarioOrdenTrabajo(Utilerias.OrdenesDeTrabajo.Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_EJECUTA) = pvd_FechaFinalizacion

                    vlo_DrFilaTiempoOperarioNuevo = vlo_DsTiempoOperarionNuevo.Tables(0).NewRow
                    vlo_DrFilaTiempoOperarioNuevo.Item(vlo_DsTiempoOperarionNuevo.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.NUM_EMPLEADO)) = vlo_FilaOperarioOrdenTrabajo(Utilerias.OrdenesDeTrabajo.Modelo.OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO).ToString
                    vlo_DrFilaTiempoOperarioNuevo.Item(vlo_DsTiempoOperarionNuevo.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER)) = vlo_FilaOperarioOrdenTrabajo(Utilerias.OrdenesDeTrabajo.Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER).ToString
                    vlo_DrFilaTiempoOperarioNuevo.Item(vlo_DsTiempoOperarionNuevo.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_UBICACION)) = vlo_FilaOperarioOrdenTrabajo(Utilerias.OrdenesDeTrabajo.Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION).ToString
                    vlo_DrFilaTiempoOperarioNuevo.Item(vlo_DsTiempoOperarionNuevo.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO)) = vlo_FilaOperarioOrdenTrabajo(Utilerias.OrdenesDeTrabajo.Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO).ToString
                    vlo_DrFilaTiempoOperarioNuevo.Item(vlo_DsTiempoOperarionNuevo.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO)) = vlo_FilaOperarioOrdenTrabajo(Utilerias.OrdenesDeTrabajo.Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO).ToString

                    vlo_DsTiempoOperarionNuevo.Tables(0).Rows.Add(vlo_DrFilaTiempoOperarioNuevo)

                Next

                'Actualiza los registros, por medio del adapter
                vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)
                vlo_DalOttOperarioOrdenTrab.AdapterOttOperarioOrdenTrabajo(vlo_DsOperarioOrdenTrabajo)

                'Carga los tiempos de operarios
                vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
                vlo_DsTiempoOperario = vlo_DalOttTiempoOperario.ListarRegistros(
                    String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo),
                    String.Empty,
                    False,
                    0,
                    0)

                'Elimina del Dataset cada uno de los registros
                For Each vlo_FilaTiempoOperario In vlo_DsTiempoOperario.Tables(0).Rows
                    vlo_FilaTiempoOperario.Delete()
                Next

                'Elimina los registros de tiempos de operarios, por medio del adapter
                vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
                vlo_DalOttTiempoOperario.AdapterOttTiempoOperario(vlo_DsTiempoOperario)

                For Each vlo_Fila In vlo_DsTiempoOperarionNuevo.Tables(0).Rows
                    vlo_Fila(Modelo.OTT_TIEMPO_OPERARIO.TIEMPO) = pvn_TiempoInvertido
                    vlo_Fila(Modelo.OTT_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO) = pvn_UnidadTiempo
                    vlo_Fila(Modelo.OTT_TIEMPO_OPERARIO.CLASIFICACION) = "RAL"
                    vlo_Fila(Modelo.OTT_TIEMPO_OPERARIO.USUARIO) = pvc_UserName
                Next

                'Inserta los nuevos registros de tiempos de operarios, por medio del adapter
                vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
                vlo_DalOttTiempoOperario.AdapterOttTiempoOperario(vlo_DsTiempoOperarionNuevo)

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite agregar un registro en la tabla OTT_ORDEN_TRABAJO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
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

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vln_Resultado = vlo_DalOttOrdenTrabajo.InsertarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTT_ORDEN_TRABAJO, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOttOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Hay registro asociados")
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vln_Resultado = vlo_DalOttOrdenTrabajo.BorrarRegistro(pvo_Registro)
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
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String) As EntOttOrdenTrabajo
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                Return vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper()))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Permite obtener un registro según su categoria y su actividad
        ''' </summary>
        ''' <param name="pvn_IdUbicacion"></param>
        ''' <param name="pvc_IdOrdenTrabajo"></param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>04/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorCategoriaActividad(pvn_IdCategoria As Integer, pvc_IdActividad As String, pvn_IdUbicacionMadre As Integer, pvc_IdOrdenTrabajoMadre As String) As EntOttOrdenTrabajo
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                Return vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = 'HIJ' AND {5} = {6} AND {7} = '{8}'", Modelo.OTT_ORDEN_TRABAJO.ID_CATEGORIA_SERVICIO, pvn_IdCategoria, Modelo.OTT_ORDEN_TRABAJO.ID_ACTIVIDAD, pvc_IdActividad, Modelo.OTT_ORDEN_TRABAJO.PARENTESCO, Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION_MADRE, pvn_IdUbicacionMadre, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO_MADRE, pvc_IdOrdenTrabajoMadre))
            Catch vlo_Excepcion As Exception
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
        ''' <param name="pvn_IdUbicacion"></param>
        ''' <param name="pvn_Annio"></param>
        ''' <param name="pvn_Consecutivo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ObtenerRegistroPorConsecutivo(pvn_IdUbicacion As Integer, pvn_Annio As Integer, pvn_Consecutivo As Integer) As EntOttOrdenTrabajo
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                Return vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ANNO, pvn_Annio, Modelo.OTT_ORDEN_TRABAJO.CONSECUTIVO, pvn_Consecutivo))
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
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            '     Dim vlo_DalOttFichaTecnicaGeneral As DalOttFichaTecnicaGeneral
            Dim vlo_DalOttRevisionOrdenTrabaj As DalOttRevisionOrdenTrabaj
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            '   Dim vlo_DalOttFichaTecnicaEspacio As DalOttFichaTecnicaEspacio

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

                'Determinar la existencia de registros asociados en la tabla OTT_ORDEN_TRABAJO
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                If vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper())).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTT_ADJUNTO_ORDEN_TRABAJO
                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                If vlo_DalOttAdjuntoOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper())).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTT_FICHA_TECNICA_GENERAL
                'vlo_DalOttFichaTecnicaGeneral = New DalOttFichaTecnicaGeneral(vlo_Conexion)
                'If vlo_DalOttFichaTecnicaGeneral.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTT_FICHA_TECNICA_GENERAL.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_FICHA_TECNICA_GENERAL.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper())).Existe Then
                '    Return True
                'End If

                'Determinar la existencia de registros asociados en la tabla OTT_REVISION_ORDEN_TRABAJ
                vlo_DalOttRevisionOrdenTrabaj = New DalOttRevisionOrdenTrabaj(vlo_Conexion)
                If vlo_DalOttRevisionOrdenTrabaj.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTT_REVISION_ORDEN_TRABAJ.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_REVISION_ORDEN_TRABAJ.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper())).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTT_TRAZABILIDAD_PROCESO
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                If vlo_DalOttTrazabilidadProceso.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTT_TRAZABILIDAD_PROCESO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_TRAZABILIDAD_PROCESO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper())).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTT_FICHA_TECNICA_ESPACIO
                'vlo_DalOttFichaTecnicaEspacio = New DalOttFichaTecnicaEspacio(vlo_Conexion)
                'If vlo_DalOttFichaTecnicaEspacio.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTT_FICHA_TECNICA_ESPACIO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_FICHA_TECNICA_ESPACIO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper())).Existe Then
                '    Return True
                'End If

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
        ''' Proceso para liquidar una orden de trabajo
        ''' </summary>
        ''' <returns>Número positivo si la operación tuvo éxito o un número negativo si fallo</returns>
        ''' <remarks></remarks>
        ''' <author>César Augusto Bermúdez García</author>
        ''' <creationDate>27/11/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function ProcesoLiquidacionOrdenDeTrabajo() As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_WsPlataformaDeServicios As WsrPlataformaDeServicios.WsPlataformaDeServicios
            Dim vln_Resultado As String
            Dim vlo_DsDatos As DataSet
            Dim vlo_DataTable As DataTable
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vln_DiferenciaDias As Integer

            vlo_WsPlataformaDeServicios = New WsrPlataformaDeServicios.WsPlataformaDeServicios
            vlo_WsPlataformaDeServicios.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsPlataformaDeServicios.Timeout = -1
            vlo_WsPlataformaDeServicios.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_PLATAFORMA_SERVICIOS)
            vln_Resultado = -1

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                'Obtiene los registros que no requieren ficha tecnica
                vlo_DsDatos = vlo_DalOttOrdenTrabajo.ListarVOttOstSinFichalst(String.Empty, String.Empty, False, 0, 1)

                If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    vlo_DataTable = vlo_DsDatos.Tables(0)
                    For Each fecha As DataRow In vlo_DataTable.Rows
                        'Para cada uno calcular la diferencia de días hábiles
                        vln_DiferenciaDias = vlo_WsPlataformaDeServicios.PST_SOLICITUD_CONSTANCIA_DiferenciaFechasHabiles_CierreUCR(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN), fecha(0), Date.Now)
                        fecha(3) = "LIQ"

                        'Si la diferencia es mayor que el especificado en el parámetro
                        '“Plazo límite en días hábiles para recibido conforme de órdenes de trabajo”
                        If vln_DiferenciaDias > CInt(fecha(6)) Then

                            'Cambiar el estado de las ordenes de trabajo a liquidado
                            vlo_DalOttOrdenTrabajo.AdapterOTEstadoUpdate(vlo_DsDatos)
                            'Agregar trazabilidad al proceso de liquidación
                            vlo_EntOttTrazabilidadProceso.IdUbicacion = fecha(1)
                            vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = fecha(2)
                            vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = fecha(3)
                            vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = fecha(4)
                            vlo_EntOttTrazabilidadProceso.Usuario = fecha(5)
                            vlo_EntOttTrazabilidadProceso.Observaciones = "Liquidación automática por vencimiento del plazo para recibido conforme del solicitante."
                            'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now

                            vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                        End If
                    Next
                End If


                vlo_DsDatos.Clear()

                'Hacer el mismo proceso para las órdenes de trabajo que requieren ficha técnica

                vlo_DsDatos = vlo_DalOttOrdenTrabajo.ListarVOttOstConFichalst(String.Empty, String.Empty, False, 0, 1)

                If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    vlo_DataTable = vlo_DsDatos.Tables(0)
                    For Each fecha As DataRow In vlo_DataTable.Rows
                        vln_DiferenciaDias = vlo_WsPlataformaDeServicios.PST_SOLICITUD_CONSTANCIA_DiferenciaFechasHabiles_CierreUCR(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN), fecha(0), Date.Now)
                        fecha(3) = "LIQ"
                        If vln_DiferenciaDias > CInt(fecha(6)) Then
                            vlo_DalOttOrdenTrabajo.AdapterOTEstadoUpdate(vlo_DsDatos)
                            vlo_EntOttTrazabilidadProceso.IdUbicacion = fecha(1)
                            vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = fecha(2)
                            vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = fecha(3)
                            vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = fecha(4)
                            vlo_EntOttTrazabilidadProceso.Usuario = fecha(5)
                            vlo_EntOttTrazabilidadProceso.Observaciones = "Liquidación automática por vencimiento del plazo para recibido conforme del solicitante."
                            ' vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now

                            vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                        End If
                    Next
                Else
                End If

                vln_Resultado = 1

            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Lista las Ot's Rechazadas y prepara el data set para ser mostrado en la vista
        ''' </summary>
        ''' <param name="pvc_Condicion"></param>
        ''' <param name="pvc_Orden"></param>
        ''' <param name="pvb_Paginar"></param>
        ''' <param name="pvn_NumeroPagina"></param>
        ''' <param name="pvn_TamanoPagina"></param>
        ''' <returns>Un data set con las Ot's rechazadas</returns>
        ''' <remarks></remarks>
        ''' <author>César Augusto Bermúdez García</author>
        ''' <creationDate>2/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtRechazadas(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
            Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_ORDENES_DE_TRABAJO).ConnectionString
            Dim vlo_DalOttOrdenTrabajo As New DalOttOrdenTrabajo(vlo_Conexion)
            Dim vln_DiferenciaDias As Integer
            Dim vlo_WsPlataformaDeServicios As WsrPlataformaDeServicios.WsPlataformaDeServicios
            Dim vlo_DsDatos As DataSet


            vlo_WsPlataformaDeServicios = New WsrPlataformaDeServicios.WsPlataformaDeServicios
            vlo_WsPlataformaDeServicios.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsPlataformaDeServicios.Timeout = -1
            vlo_WsPlataformaDeServicios.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_PLATAFORMA_SERVICIOS)

            Try


                vlo_DsDatos = vlo_DalOttOrdenTrabajo.ListarVOtRechazadas(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

                If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    vlo_DsDatos.Tables(0).Columns.Add("DiasHabiles")
                    For Each row As DataRow In vlo_DsDatos.Tables(0).Rows
                        Dim num = row.ItemArray.Length - 1
                        'Para cada uno calcular la diferencia de días hábiles
                        vln_DiferenciaDias = vlo_WsPlataformaDeServicios.PST_SOLICITUD_CONSTANCIA_DiferenciaFechasHabiles_CierreUCR(
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN), CType(row(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_RECHAZADAS.FECHA), Date), Date.Now)

                        row(num) = vln_DiferenciaDias

                    Next
                End If

                Return vlo_DsDatos
            Catch ex As Exception

            End Try
        End Function

        ''' <summary>
        ''' Ejecuta el proceso de Visto bueno al motivo de rechazo
        ''' </summary>
        ''' <param name="pvn_IdOrdenTrabajo"></param>
        ''' <param name="pvc_NumEmpleado"></param>
        ''' <param name="pvc_MotivoRechazo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Augusto Bermúdez García</author>
        ''' <creationDate>2/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function VistoBuenoRechazo(pvn_IdOrdenTrabajo As String, pvn_IdUbicacion As Integer, pvc_NumEmpleado As String, pvc_MotivoRechazo As String, pvc_UsuarioActual As String)
            Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_ORDENES_DE_TRABAJO).ConnectionString
            Dim vlo_DalOttOrdenTrabajo As New DalOttOrdenTrabajo(vlo_Conexion)
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados

            Try
                'Cargar la Orden de trabajo a modificar y cambiarle su estado
                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = '{1}' AND {2} = {3}", Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvn_IdOrdenTrabajo, Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion))
                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = Utilerias.OrdenesDeTrabajo.EstadoOrden.RECHAZADA
                vlo_EntOttOrdenTrabajo.Usuario = pvc_UsuarioActual

                vlo_EntEmpleados = CargarFuncionarioNumEmpleado(pvc_NumEmpleado)

                vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                Return Notificacion("Notificación de rechazo de orden de trabajo de mantenimiento y construcción ",
                             String.Format("<b>Estimado(a):{0} {1} {2}</b><br /><br />Se le informa que su orden de trabajo de mantenimiento y construcción para realizar el siguiente trabajo:{3} fue rechazada.<br /><br />El motivo de rechazo es el siguiente:{4}<br /><br />Favor no contestar este correo, esta cuenta se utiliza unicamente para notificaciones",
                                           vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2,
                                           vlo_EntOttOrdenTrabajo.DescripcionTrabajo, pvc_MotivoRechazo), vlo_EntEmpleados.CORREO_INSTITUCIONAL)
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally

            End Try
        End Function

        ''' <summary>
        '''  Ejecuta el proceso para denegación al motivo de rechazo de una orden de trabajo
        ''' </summary>
        ''' <param name="pvn_IdOrdenTrabajo"></param>
        ''' <param name="pvc_NumEmpleado"></param>
        ''' <param name="pvc_MotivoRechazo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Augusto Bermúdez García</author>
        ''' <creationDate>2/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function DenegacionMotivoRechazo(pvn_IdOrdenTrabajo As String, pvn_IdUbicacion As Integer, pvc_NumEmpleado As String, pvc_MotivoRechazo As String, pvc_UsuarioActual As String)
            Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_ORDENES_DE_TRABAJO).ConnectionString
            Dim vlo_DalOttOrdenTrabajo As New DalOttOrdenTrabajo(vlo_Conexion)
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso

            Try
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                'Cargar la Orden de trabajo a modificar y cambiarle su estado
                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = '{1}' AND {2} = {3}", Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvn_IdOrdenTrabajo, Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion))
                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = Utilerias.OrdenesDeTrabajo.EstadoOrden.ASIGNADA
                vlo_EntOttOrdenTrabajo.Usuario = pvc_UsuarioActual

                'Se carega el funcionario por numero de empleado
                vlo_EntEmpleados = CargarFuncionarioNumEmpleado(pvc_NumEmpleado)

                'Se modifica el registro
                vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                Dim vlo_OpExitosa = Notificacion("Notificación de denegación de rechazo de orden de trabajo de mantenimiento y construcción",
                             String.Format("<b>Estimado(a):{0} {1} {2}</b><br /><br />Se le informa que no fue avalado el rechazo a la orden de trabajo de mantenimiento y construcción con consecutivo de orden de trabajo {3} solicitada para realizar el siguiente trabajo: {4}<br />El motivo de rechazo indicado fue el siguiente: {5}<br /> el mismo se deniega por el siguiente motivo: {6}<br />Favor no contestar este correo, esta cuenta se utiliza únicamente para notificaciones",
                                           vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2,
                                           vlo_EntOttOrdenTrabajo.IdOrdenTrabajo, vlo_EntOttOrdenTrabajo.DescripcionTrabajo,
                                           pvc_MotivoRechazo, pvc_MotivoRechazo), vlo_EntEmpleados.CORREO_INSTITUCIONAL)

                'If vlo_OpExitosa Then
                '    'Agregar trazabilidad al proceso de denegación
                '    vlo_EntOttTrazabilidadProceso.IdUbicacion = vlo_EntOttOrdenTrabajo.IdUbicacion
                '    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = vlo_EntOttOrdenTrabajo.IdOrdenTrabajo
                '    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo
                '    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = vlo_EntOttOrdenTrabajo.NumEmpleado
                '    vlo_EntOttTrazabilidadProceso.Usuario = vlo_EntOttOrdenTrabajo.Usuario
                '    vlo_EntOttTrazabilidadProceso.Observaciones = pvc_MotivoRechazo
                '    vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                '    'vlo_EntOttTrazabilidadProceso.IdMotivoRechazo = pvc_MotivoRechazo

                '    vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                'End If

            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally

            End Try
        End Function

        ''' <summary>
        '''  Crea y envía la notificación al gestor de notificaciones con el asunto mensaje y destinatario asignados
        ''' </summary>
        ''' <param name="pcv_Asunto"></param>
        ''' <param name="pvc_Mensaje"></param>
        ''' <param name="pvc_Destinatario"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Augusto Bermúdez García</author>
        ''' <creationDate>2/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function Notificacion(pcv_Asunto As String, pvc_Mensaje As String, pvc_Destinatario As String)
            Dim vlo_Gestor As wsGestorNotificaciones
            Dim vlo_ListaDestinatarios As List(Of EntGNT_DESTINATARIO)
            Dim vlo_EntCorreo As EntGNT_DESTINATARIO
            Dim vlo_EntNotificacion As EntGNT_NOTIFICACION
            Dim vlo_EntSistema As EntGNM_SISTEMA
            Dim vlc_CorreoAdministrador As String
            Dim vlo_ListaCorreoAdministrador As String()

            Try
                'Configurar controles para el gestor de notificaciones 
                vlo_Gestor = New wsGestorNotificaciones
                vlo_Gestor.Url = ConfigurationManager.AppSettings("WsrGestorNotificaciones.wsGestorNotificaciones")
                vlo_Gestor.Credentials = System.Net.CredentialCache.DefaultCredentials
                vlo_Gestor.Timeout = -1

                vlo_EntSistema = vlo_Gestor.GNM_SISTEMA_ObtenerPorNombre(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                                                                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                                                                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_GN))

                If vlo_EntSistema IsNot Nothing AndAlso vlo_EntSistema.Existe Then
                    'configurar la notificación
                    vlo_EntNotificacion = New EntGNT_NOTIFICACION()
                    vlo_EntNotificacion.ASUNTO = pcv_Asunto
                    vlo_EntNotificacion.CUERPO = pvc_Mensaje
                    vlo_EntNotificacion.ES_HTML = 1
                    vlo_EntNotificacion.USUARIO_CREA = ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN

                    'Generar destinatario
                    vlo_ListaDestinatarios = New List(Of EntGNT_DESTINATARIO)
                    vlo_EntCorreo = New EntGNT_DESTINATARIO()
                    vlo_EntCorreo.DESTINATARIO = pvc_Destinatario
                    vlo_ListaDestinatarios.Add(vlo_EntCorreo)

                    Return vlo_Gestor.GNT_NOTIFICACION_Registrar(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                                                          ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                                                          vlo_EntSistema, vlo_EntNotificacion, (New List(Of EntGNT_ARCHIVO_ADJUNTO)).ToArray(),
                                                          vlo_ListaDestinatarios.ToArray()) > 0
                End If
            Catch ex As Exception
                If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso
                    CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                    Dim vlo_TallerCapacitacionException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                Else
                    Dim vlo_ControlDeErrores As New ControlDeErrores
                End If
            End Try
        End Function

        ''' <summary>
        ''' Da el Visto bueno a la orden de trabajo hija
        ''' </summary>
        ''' <param name="pvn_IdOrdenTrabajo"></param>
        ''' <param name="pvc_NumEmpleado"></param>
        ''' <param name="pvc_MotivoRechazo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Augusto Bermúdez García</author>
        ''' <creationDate>4/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function VistoBuenoRechazoOrdenTrabajoHija(pvn_IdOrdenTrabajo As String, pvc_NumEmpleado As String, pvc_MotivoRechazo As String)
            Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_ORDENES_DE_TRABAJO).ConnectionString
            Dim vlo_DalOttOrdenTrabajo As New DalOttOrdenTrabajo(vlo_Conexion)
            Dim vlo_BllOtmMotivoRechazo As New BllOtmMotivoRechazo(vlo_Conexion)
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
            Dim vlo_EntOtmMotivoRechazo As EntOtmMotivoRechazo
            Dim vlo_IdMotivoRechazo As Integer

            Try
                'Cargar la Orden de trabajo a modificar y cambiarle su estado
                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = '{1}'", Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvn_IdOrdenTrabajo))
                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = Utilerias.OrdenesDeTrabajo.EstadoOrden.ASIGNADA
                vlo_EntOtmMotivoRechazo = New EntOtmMotivoRechazo

                vlo_EntEmpleados = CargarFuncionarioNumEmpleado(pvc_NumEmpleado)

                'Si el motivo de rechazo digitado no está vacio se agregará a la Ot el motivo de rechazo
                If Not String.IsNullOrWhiteSpace(pvc_MotivoRechazo) Then
                    vlo_EntOtmMotivoRechazo.Descripcion = pvc_MotivoRechazo
                    vlo_EntOtmMotivoRechazo.Estado = Utilerias.OrdenesDeTrabajo.Estado.ACTIVO
                    vlo_EntOtmMotivoRechazo.Usuario = vlo_EntEmpleados.USUARIO_CREACION

                    'Comprueba que no exista un motivo de rechazo con la misma descripción
                    vlo_EntOtmMotivoRechazo = vlo_BllOtmMotivoRechazo.ObtenerRegistroPorLlaveAlterna(pvc_MotivoRechazo)

                    'Si no existe agrega un motivo de rechazo nuevo
                    If vlo_EntOtmMotivoRechazo.IdMotivoRechazo = 0 Then
                        vlo_EntOtmMotivoRechazo.Descripcion = pvc_MotivoRechazo
                        vlo_EntOtmMotivoRechazo.Estado = Utilerias.OrdenesDeTrabajo.Estado.ACTIVO
                        vlo_EntOtmMotivoRechazo.Usuario = vlo_EntEmpleados.USUARIO_CREACION
                        vlo_IdMotivoRechazo = vlo_BllOtmMotivoRechazo.InsertarRegistro(vlo_EntOtmMotivoRechazo)
                    Else
                        vlo_IdMotivoRechazo = vlo_EntOtmMotivoRechazo.IdMotivoRechazo
                    End If

                    'Se asigna el motivo de rechazo a la orden de trabajo antes de enviarla a modificar
                    vlo_EntOttOrdenTrabajo.IdMotivoRechazo = vlo_IdMotivoRechazo
                End If

                vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally

            End Try

        End Function

        ''' <summary>
        '''  Deniega la creación de la orden de trabajo hija
        ''' </summary>
        ''' <param name="pvn_IdOrdenTrabajo"></param>
        ''' <param name="pvc_NumEmpleado"></param>
        ''' <param name="pvc_MotivoRechazo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Augusto Bermúdez García</author>
        ''' <creationDate>4/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function DenegacionMotivoRechazoOrdenTrabajoHija(pvn_IdOrdenTrabajo As String, pvc_NumEmpleado As String, pvc_MotivoRechazo As String)
            Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_ORDENES_DE_TRABAJO).ConnectionString
            Dim vlo_DalOttOrdenTrabajo As New DalOttOrdenTrabajo(vlo_Conexion)
            Dim vlo_BllOtmMotivoRechazo As New BllOtmMotivoRechazo(vlo_Conexion)
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
            Dim vlo_EntOtmMotivoRechazo As EntOtmMotivoRechazo
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_IdMotivoRechazo As Integer

            Try
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso


                'Cargar la Orden de trabajo a modificar y cambiarle su estado
                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = '{1}'", Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvn_IdOrdenTrabajo))
                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = Utilerias.OrdenesDeTrabajo.EstadoOrden.DENEGADA


                'Carga al coordinador
                vlo_EntEmpleados = CargarFuncionarioNumEmpleado(pvc_NumEmpleado)

                'Se busca por llave primaria el motivo del rechazo
                vlo_EntOtmMotivoRechazo = vlo_BllOtmMotivoRechazo.ObtenerRegistroPorLlavePrimaria(vlo_EntOttOrdenTrabajo.IdMotivoRechazo)

                'Si no existe un motivo rechazo y lo digitado en la interfaz no es nulo, se crea un nuevo motivo rechazo.
                If vlo_EntOtmMotivoRechazo Is Nothing And
                    Not String.IsNullOrWhiteSpace(pvc_MotivoRechazo) Then
                    vlo_EntOtmMotivoRechazo.Descripcion = pvc_MotivoRechazo
                    vlo_EntOtmMotivoRechazo.Estado = Utilerias.OrdenesDeTrabajo.Estado.ACTIVO
                    vlo_EntOtmMotivoRechazo.Usuario = vlo_EntEmpleados.USUARIO_CREACION

                    'Comprueba si existe un motivo con la misma descripción
                    vlo_EntOtmMotivoRechazo = vlo_BllOtmMotivoRechazo.ObtenerRegistroPorLlaveAlterna(pvc_MotivoRechazo)

                    If vlo_EntOtmMotivoRechazo.IdMotivoRechazo = 0 Then
                        vlo_EntOtmMotivoRechazo.Descripcion = pvc_MotivoRechazo
                        vlo_EntOtmMotivoRechazo.Estado = Utilerias.OrdenesDeTrabajo.Estado.ACTIVO
                        vlo_EntOtmMotivoRechazo.Usuario = vlo_EntEmpleados.USUARIO_CREACION
                        vlo_IdMotivoRechazo = vlo_BllOtmMotivoRechazo.InsertarRegistro(vlo_EntOtmMotivoRechazo)
                    Else
                        vlo_IdMotivoRechazo = vlo_EntOtmMotivoRechazo.IdMotivoRechazo
                    End If

                    'Se asigna el motivo rechazo a la orden de trabajo antes de modificarla
                    vlo_EntOttOrdenTrabajo.IdMotivoRechazo = vlo_IdMotivoRechazo
                Else
                    vlo_EntOttOrdenTrabajo.IdMotivoRechazo = vlo_EntOtmMotivoRechazo.IdMotivoRechazo

                End If

                'Se modifica el registro
                vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                'se envía una notificación al coordinador
                Dim vlo_OpExitosa = Notificacion("Notificación de denegación de rechazo de orden de trabajo de mantenimiento y construcción",
                             String.Format("<b>Estimado(a):{0} {1} {2}</b><br /><br />Se le informa que no fue avalada la creacion de la orden de trabajo de mantenimiento y construcción con consecutivo de orden de trabajo {3} solicitada para realizar el siguiente trabajo: {4}<br />La misma se deniega por el siguiente motivo: {5}<br /> Favor no contestar este correo, esta cuenta se utiliza únicamente para notificaciones",
                                           vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2,
                                           vlo_EntOttOrdenTrabajo.IdOrdenTrabajo, vlo_EntOttOrdenTrabajo.DescripcionTrabajo,
                                           pvc_MotivoRechazo), vlo_EntEmpleados.CORREO_INSTITUCIONAL)

                If vlo_OpExitosa Then
                    'Agregar trazabilidad al proceso de denegación
                    vlo_EntOttTrazabilidadProceso.IdUbicacion = vlo_EntOttOrdenTrabajo.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = vlo_EntOttOrdenTrabajo.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = vlo_EntOttOrdenTrabajo.NumEmpleado
                    vlo_EntOttTrazabilidadProceso.Usuario = vlo_EntOttOrdenTrabajo.Usuario
                    vlo_EntOttTrazabilidadProceso.Observaciones = pvc_MotivoRechazo
                    'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                    vlo_EntOttTrazabilidadProceso.IdMotivoRechazo = vlo_EntOtmMotivoRechazo.IdMotivoRechazo

                    vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                End If

            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally

            End Try

        End Function


        ''' <summary>
        '''  Agrega una columna de color según los días hábiles de tiempo en espera
        ''' </summary>
        ''' <param name="pvn_IdOrdenTrabajo"></param>
        ''' <param name="pvc_NumEmpleado"></param>
        ''' <param name="pvc_MotivoRechazo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Augusto Bermúdez García</author>
        ''' <creationDate>15/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarAlertasTiempo(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer, pvc_ReqFichaTecnica As Boolean)
            Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_ORDENES_DE_TRABAJO).ConnectionString
            Dim vlo_DalOttOrdenTrabajo As New DalOttOrdenTrabajo(vlo_Conexion)
            Dim vlo_DalOtpParametroUbicacion As New DalOtpParametroUbicacion(vlo_Conexion)
            Dim vlo_WsPlataformaDeServicios As WsrPlataformaDeServicios.WsPlataformaDeServicios
            Dim vlo_DsDatos As DataSet
            Dim vln_DiferenciaDias As Integer
            Dim vlo_ParametroAmarillo As EntOtpParametroUbicacion
            Dim vlo_ParametroRojo As EntOtpParametroUbicacion
            Dim vlo_ParametroVerde As EntOtpParametroUbicacion
            Dim vln_valorAmarilla As Integer
            Dim vln_valorRoja As Integer


            vlo_WsPlataformaDeServicios = New WsrPlataformaDeServicios.WsPlataformaDeServicios
            vlo_WsPlataformaDeServicios.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsPlataformaDeServicios.Timeout = -1
            vlo_WsPlataformaDeServicios.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_PLATAFORMA_SERVICIOS)



            Try
                'Obtener datos desde la vista
                vlo_DsDatos = vlo_DalOttOrdenTrabajo.ListarVOtAlertasTiempo(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

                If pvc_ReqFichaTecnica Then
                    'Se obtiene el valor de días para los semáforos
                    vlo_ParametroAmarillo = vlo_DalOtpParametroUbicacion.ObtenerRegistro(
                        String.Format("{0}={1}", Utilerias.OrdenesDeTrabajo.Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO,
                                      Utilerias.OrdenesDeTrabajo.Parametros.TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_ATENCION_DISENNO))

                    vlo_ParametroRojo = vlo_DalOtpParametroUbicacion.ObtenerRegistro(
                        String.Format("{0}={1}", Utilerias.OrdenesDeTrabajo.Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO,
                                      Utilerias.OrdenesDeTrabajo.Parametros.TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_ATRASO_DISENNO))
                Else
                    'Se obtiene el valor de días para los semáforos
                    vlo_ParametroAmarillo = vlo_DalOtpParametroUbicacion.ObtenerRegistro(
                        String.Format("{0}={1}", Utilerias.OrdenesDeTrabajo.Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO,
                                      Utilerias.OrdenesDeTrabajo.Parametros.TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_ATENCION))

                    vlo_ParametroRojo = vlo_DalOtpParametroUbicacion.ObtenerRegistro(
                        String.Format("{0}={1}", Utilerias.OrdenesDeTrabajo.Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO,
                                      Utilerias.OrdenesDeTrabajo.Parametros.TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_ATRASO))
                End If


                If vlo_ParametroRojo.Existe And vlo_ParametroAmarillo.Existe Then
                    vln_valorAmarilla = CInt(vlo_ParametroAmarillo.Valor)
                    vln_valorRoja = CInt(vlo_ParametroRojo.Valor)


                    'Agregar columna de color, si existen registros que mostrar
                    'Esta columna permitirá agrupar por colores

                    If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                        vlo_DsDatos.Tables(0).Columns.Add("Color")
                        For Each row As DataRow In vlo_DsDatos.Tables(0).Rows
                            Dim num = row.ItemArray.Length - 1


                            'Calcular la diferencia de días hábiles
                            vln_DiferenciaDias = vlo_WsPlataformaDeServicios.PST_SOLICITUD_CONSTANCIA_DiferenciaFechasHabiles_CierreUCR(
                                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                                        row(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ALERTAS_TIEMPO.FECHA_DE_ASIGNACION), Date.Now)


                            If vln_DiferenciaDias < vln_valorAmarilla Then
                                row(num) = "3"
                            ElseIf vln_DiferenciaDias > vln_valorAmarilla And vln_DiferenciaDias < vln_valorRoja Then
                                row(num) = "2"
                            ElseIf vln_DiferenciaDias > vln_valorRoja Then
                                row(num) = "1"
                            End If

                        Next
                    End If
                End If

                Return vlo_DsDatos
            Catch ex As Exception

            End Try


        End Function

        ''' <summary>
        ''' Valida la inclusion de ordenes de trabajo tomando en cuenta los periodos de cierre de mantenimiento y de diseño, así como las excepciones para dichos periodos
        ''' </summary>
        ''' <param name="pvc_CondicionInicial">Condicion básica para filtrar categorias</param>
        ''' <param name="pvc_NumEmpleado">Numero de empleado actual, para buscar si posee excepciones</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Augusto Bermúdez García</author>
        ''' <creationDate>21/01/2016</creationDate>
        Public Function ValidarCategoriasPeriodoCierre(pvc_CondicionInicial As String, pvc_NumEmpleado As String) As String

            Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_ORDENES_DE_TRABAJO).ConnectionString
            Dim vlo_DalOtfPeriodoCierre As New DalOtfPeriodoCierre(vlo_Conexion)
            Dim vlo_DalOtfExcepcionPeriodo As New DalOtfExcepcionPeriodo(vlo_Conexion)
            Dim vlo_DalOtmCategoriaServicio As New DalOtmCategoriaServicio(vlo_Conexion)

            Dim vlo_DtDatosPeriodoCierre As DataTable
            Dim vlo_DsDatosExcepcion As DataTable
            Dim vlc_Condicion As String
            Dim vlo_rowsMan() As DataRow
            Dim vlo_rowsDis() As DataRow


            Try
                'Listar los periodos de cierre y verificar si la fecha actual está presente dentro de ése periodo
                '{0}: Fecha Actual
                '{1}: FECHA_INICIO_CIERRE
                '{2}: FECHA_FIN_CIERRE

                vlo_DtDatosPeriodoCierre = vlo_DalOtfPeriodoCierre.ListarRegistrosLista(
                    String.Format("TO_DATE('{0}/{1}/{2}','{3}') BETWEEN {4} AND {5}",
                                    DateTime.Now.Year,
                                    DateTime.Now.Month,
                                    DateTime.Now.Day,
                                    Constantes.FORMATO_FECHA_BD,
                                    Modelo.OTF_PERIODO_CIERRE.FECHA_INICIO_CIERRE,
                                    Modelo.OTF_PERIODO_CIERRE.FECHA_FIN_CIERRE), String.Empty, False, 1, 0).Tables(0)

                'Hace un select sobre el dataset para verificar si está en periodo de mantenimiento
                '{0}: UNIDAD_CIERRE
                '{1}: MANTENIMIENTO
                vlo_rowsMan = vlo_DtDatosPeriodoCierre.Select(String.Format("{0}='{1}'", Modelo.OTF_PERIODO_CIERRE.UNIDAD_CIERRE, UnidadCierre.MANTENIMIENTO))

                'Hace un select sobre el dataset para verificar si está en periodo de diseño
                '{0}: UNIDAD_CIERRE
                '{1}: DISENIO
                vlo_rowsDis = vlo_DtDatosPeriodoCierre.Select(String.Format("{0}='{1}'", Modelo.OTF_PERIODO_CIERRE.UNIDAD_CIERRE, UnidadCierre.DISENIO))

                If vlo_rowsMan.Length > 0 Then

                    If vlo_rowsDis.Length > 0 Then
                        'Si ambos periodos están en cierre, permitirá que se agreguen ordenes de cualquier tipo
                        Return String.Format("{0},{1}", pvc_CondicionInicial, String.Empty)
                    Else

                        'Si es de tipo mantenimiento se cargan solo las categorias que NO requieren ficha técnica
                        '{0}:   Condición actual
                        '{1}:   Campo de la vista REQUIERE_FICHA_TECNICA
                        '{2}:   NO_REQUIERE_FICHA_TECNICA

                        vlc_Condicion = String.Format("{0} AND {1} = {2}", pvc_CondicionInicial, Modelo.OTM_CATEGORIA_SERVICIO.REQUIERE_FICHA_TECNICA, FichaTecnica.NO_REQUIERE_FICHA_TECNICA)
                        Return String.Format("{0},{1}", vlc_Condicion, "El plazo para solicitudes de diseño ha finalizado. Sólo se permite la solicitud de órdenes de trabajo de mantenimiento.")
                    End If
                ElseIf vlo_rowsDis.Length > 0 Then

                    If vlo_rowsMan.Length > 0 Then
                        'Si ambos periodos están en cierre, permitirá que se agreguen ordenes de cualquier tipo
                        Return String.Format("{0},{1}", pvc_CondicionInicial, String.Empty)
                    Else

                        'Si es de tipo diseño se cargan solo las categorias que requieren ficha técnica
                        '{0}:   Condición actual
                        '{1}:   Campo de la vista REQUIERE_FICHA_TECNICA
                        '{2}:   REQUIERE_FICHA_TECNICA

                        vlc_Condicion = String.Format("{0} AND {1} = {2}", pvc_CondicionInicial, Modelo.OTM_CATEGORIA_SERVICIO.REQUIERE_FICHA_TECNICA, FichaTecnica.REQUIERE_FICHA_TECNICA)
                        Return String.Format("{0},{1}", vlc_Condicion, "El plazo para solicitudes de mantenimiento ha finalizado. Sólo se permite la solicitud de órdenes de trabajo de diseño.")
                    End If
                End If

                If vlo_rowsMan.Length = 0 And vlo_rowsDis.Length = 0 Then
                    'Obtener las excepciones
                    vlo_DsDatosExcepcion = vlo_DalOtfExcepcionPeriodo.ListarRegistros(String.Format("{0} = {1}", Modelo.OTF_EXCEPCION_PERIODO.NUM_EMPLEADO, pvc_NumEmpleado), String.Empty, False, 0, 0).Tables(0)

                    If vlo_DsDatosExcepcion.Rows.Count > 0 Then
                        vlo_rowsMan = vlo_DsDatosExcepcion.Select(String.Format("{0} = '{1}'", Modelo.OTF_EXCEPCION_PERIODO.UNIDAD_INTERNA, UnidadCierre.MANTENIMIENTO))


                        vlo_rowsDis = vlo_DsDatosExcepcion.Select(String.Format("{0} = '{1}'", Modelo.OTF_EXCEPCION_PERIODO.UNIDAD_INTERNA, UnidadCierre.DISENIO))

                        If vlo_rowsMan.Length > 0 Then

                            If vlo_rowsDis.Length > 0 Then
                                Return String.Format("{0},{1}", pvc_CondicionInicial, "Aunque sea periodo de cierre puede solicitar ordenes de trabajo de diseño y de mantenimiento.")
                            Else

                                'Si es de tipo mantenimiento se cargan solo las categorias que NO requieren ficha técnica
                                '{0}:   Condición actual
                                '{1}:   Campo de la vista REQUIERE_FICHA_TECNICA
                                '{2}:   NO_REQUIERE_FICHA_TECNICA

                                vlc_Condicion = String.Format("{0} AND {1} = {2}", pvc_CondicionInicial, Modelo.OTM_CATEGORIA_SERVICIO.REQUIERE_FICHA_TECNICA, FichaTecnica.NO_REQUIERE_FICHA_TECNICA)
                                Return String.Format("{0},{1}", vlc_Condicion, "El plazo para solicitudes de diseño ha finalizado. Sólo se permite la solicitud de órdenes de trabajo de mantenimiento.")

                            End If
                        ElseIf vlo_rowsDis.Length > 0 Then

                            If vlo_rowsMan.Length > 0 Then
                                'Si ambos periodos están en cierre, permitirá que se agreguen ordenes de cualquier tipo
                                Return String.Format("{0},{1}", pvc_CondicionInicial, String.Empty)
                            End If

                            'Si es de tipo diseño se cargan solo las categorias que requieren ficha técnica
                            '{0}:   Condición actual
                            '{1}:   Campo de la vista REQUIERE_FICHA_TECNICA
                            '{2}:   REQUIERE_FICHA_TECNICA

                            vlc_Condicion = String.Format("{0} AND {1} = {2}", pvc_CondicionInicial, Modelo.OTM_CATEGORIA_SERVICIO.REQUIERE_FICHA_TECNICA, FichaTecnica.REQUIERE_FICHA_TECNICA)
                            Return String.Format("{0},{1}", vlc_Condicion, "El plazo para solicitudes de mantenimiento ha finalizado. Sólo se permite la solicitud de órdenes de trabajo de diseño.")
                        End If

                    End If

                    Return String.Format("{0},{1}", String.Empty, String.Empty)
                End If


            Catch ex As Exception
                Throw
            End Try

        End Function


        ''' <summary>
        ''' Lista información general de las ordenes de trabajo transaccionales y de las históricas 
        ''' </summary>
        ''' <param name="pvc_Condicion"></param>
        ''' <param name="pvc_Orden"></param>
        ''' <param name="pvb_Paginar"></param>
        ''' <param name="pvn_NumeroPagina"></param>
        ''' <param name="pvn_TamanoPagina"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez García</author>
        ''' <creationDate>05/10/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarResumenOT(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As Data.DataSet
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsRegistros As Data.DataSet
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
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

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)

                vlo_DsRegistros = vlo_DalOttOrdenTrabajo.ListarVOtResumenOt(
                    pvc_Condicion,
                    pvc_Orden,
                    pvb_Paginar,
                    pvn_NumeroPagina,
                    pvn_TamanoPagina)

                vlo_DsDatos.Tables(0).PrimaryKey = New DataColumn() {vlo_DsDatos.Tables(0).Columns("CODIGO_UBICA")}

                For Each vlo_DrFilaUnidadUbicacion In vlo_DsRegistros.Tables(0).Rows

                    vlo_DrFilaUnidad = vlo_DsDatos.Tables(0).Rows.Find(New Object() {vlo_DrFilaUnidadUbicacion(Modelo.V_OT_RESUMEN_OT.COD_UNIDAD_SIRH)})

                    If vlo_DrFilaUnidad IsNot Nothing Then
                        vlo_DrFilaUnidadUbicacion(Modelo.V_OT_RESUMEN_OT.UNIDAD_SOLICITANTE) = vlo_DrFilaUnidad("COD_DESC")
                    End If

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

        ''' <summary>
        ''' En caso de vencimiento del plazo indicado para la aprobación de recibido conforme de solicitante  la orden de trabajo debe ser liquidada
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>04/08/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function LiquidarOrdenTrabajoFueraDePlazoRecibidoConformeSolicitante()
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsOrdenTrabajo As Data.DataSet
            Dim vlo_DsTrazabilidad As Data.DataSet
            Dim vlo_DrFilaNueva As Data.DataRow
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_WsPlataformaDeServicios As WsrPlataformaDeServicios.WsPlataformaDeServicios
            Dim vln_DiferenciaDias As Integer

            vlo_WsPlataformaDeServicios = New WsrPlataformaDeServicios.WsPlataformaDeServicios
            vlo_WsPlataformaDeServicios.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsPlataformaDeServicios.Timeout = -1
            vlo_WsPlataformaDeServicios.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_PLATAFORMA_SERVICIOS)

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                'inicia la transaccion
                vlo_Conexion.TransaccionBegin()

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_DsTrazabilidad = vlo_DalOttTrazabilidadProceso.ListarRegistros(String.Format("1 = 0"), String.Empty, False, 0, 0)
                vlo_DsOrdenTrabajo = vlo_DalOttOrdenTrabajo.ListarVOtOrdenLiquidacion(String.Format("{0} = '{1}'", Modelo.V_OT_ORDEN_LIQUIDACION.ESTADO_ORDEN_TRABAJO, EstadoOrden.RECIBIDO_CONFORME_SOLICITANTE), String.Empty, False, 0, 0)

                For Each vlo_Fila In vlo_DsOrdenTrabajo.Tables(0).Rows

                    vln_DiferenciaDias = vlo_WsPlataformaDeServicios.PST_SOLICITUD_CONSTANCIA_DiferenciaFechasHabiles_CierreUCR(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN), vlo_Fila(Modelo.V_OT_ORDEN_LIQUIDACION.FECHA_ENVIO_RECIBIDO_CONFORME), Date.Now)

                    If vln_DiferenciaDias > CType(vlo_Fila(Modelo.V_OT_ORDEN_LIQUIDACION.DIAS_HABILES), Integer) Then
                        vlo_Fila(Modelo.V_OT_ORDEN_LIQUIDACION.ESTADO_ORDEN_TRABAJO) = EstadoOrden.LIQUIDADA
                        vlo_Fila(Modelo.V_OT_ORDEN_LIQUIDACION.USUARIO) = "SISTEMA"

                        vlo_DrFilaNueva = vlo_DsTrazabilidad.Tables(0).NewRow
                        vlo_DrFilaNueva.Item(vlo_DsTrazabilidad.Tables(0).Columns(Modelo.OTT_TRAZABILIDAD_PROCESO.ID_UBICACION)) = CType(vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ORDEN_LIQUIDACION.ID_UBICACION).ToString, Integer)
                        vlo_DrFilaNueva.Item(vlo_DsTrazabilidad.Tables(0).Columns(Modelo.OTT_TRAZABILIDAD_PROCESO.ID_ORDEN_TRABAJO)) = vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ORDEN_LIQUIDACION.ID_ORDEN_TRABAJO).ToString
                        vlo_DrFilaNueva.Item(vlo_DsTrazabilidad.Tables(0).Columns(Modelo.OTT_TRAZABILIDAD_PROCESO.NUM_EMPLEADO_EJECUTA)) = vlo_Fila(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_ORDEN_LIQUIDACION.NUM_EMPLEADO).ToString
                        vlo_DrFilaNueva.Item(vlo_DsTrazabilidad.Tables(0).Columns(Modelo.OTT_TRAZABILIDAD_PROCESO.ESTADO_ORDEN_TRABAJO)) = EstadoOrden.LIQUIDADA
                        ' vlo_DrFilaNueva.Item(vlo_DsTrazabilidad.Tables(0).Columns(Modelo.OTT_TRAZABILIDAD_PROCESO.FECHA_HORA_EJECUCION)) = DateTime.Now
                        vlo_DrFilaNueva.Item(vlo_DsTrazabilidad.Tables(0).Columns(Modelo.OTT_TRAZABILIDAD_PROCESO.OBSERVACIONES)) = "Liquidación automática por vencimiento del plazo para recibido conforme del solicitante."
                        vlo_DrFilaNueva.Item(vlo_DsTrazabilidad.Tables(0).Columns(Modelo.OTT_TRAZABILIDAD_PROCESO.USUARIO)) = "SISTEMA"

                        vlo_DsTrazabilidad.Tables(0).Rows.Add(vlo_DrFilaNueva)

                    End If

                Next

                vlo_DalOttOrdenTrabajo.AdapterOttOrdenesTrabajoModificacion(vlo_DsOrdenTrabajo)
                vlo_DalOttTrazabilidadProceso.AdapterOttTrazabilidadProceso(vlo_DsTrazabilidad)

                vlo_Conexion.TransaccionCommit()

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

        ''' <summary>
        ''' En caso de vencimiento del plazo indicado para la revision de vibilidad tecnica la orden de trabajo debe ser liquidada
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>20/10/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function LiquidarOrdenTrabajoFueraDePlazoRevisionViabilidadTecnica()
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsOrdenTrabajo As Data.DataSet
            Dim vlo_DsViabilidadTecnica As Data.DataSet
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttViabilidadTecnica As DalOttViabilidadTecnica
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                'inicia la transaccion
                vlo_Conexion.TransaccionBegin()

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttViabilidadTecnica = New DalOttViabilidadTecnica(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)

                vlo_DsOrdenTrabajo = vlo_DalOttOrdenTrabajo.ListarRegistrosLista(String.Format("{0} = '{1}'", Modelo.OTT_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO, EstadoOrden.PENDIENTE_RESPUESTA_SOLICITANTE), String.Empty, False, 0, 0)

                For Each vlo_FilaOrdenTrabajo In vlo_DsOrdenTrabajo.Tables(0).Rows
                    Dim vln_CantidadDiasTotal As Long = 0
                    Dim vln_DiferenciaDias As Long = 0
                    vlo_DsViabilidadTecnica = vlo_DalOttViabilidadTecnica.ListarRegistrosLista(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_VIABILIDAD_TECNICA.ID_UBICACION, vlo_FilaOrdenTrabajo(Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION).ToString, Modelo.OTT_VIABILIDAD_TECNICA.ID_ORDEN_TRABAJO, vlo_FilaOrdenTrabajo(Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO).ToString), String.Empty, False, 0, 0)

                    Select Case CType(vlo_DsViabilidadTecnica.Tables(0).Rows(0)(Modelo.V_OTT_VIABILIDAD_TECNICALST.UNIDAD).ToString, Integer)
                        Case Unidades.MINUTOS
                            vln_CantidadDiasTotal = (CType(vlo_DsViabilidadTecnica.Tables(0).Rows(0)(Modelo.V_OTT_VIABILIDAD_TECNICALST.VALOR_UNIDAD_TIEMPO).ToString, Integer) / 1440) * CType(vlo_DsViabilidadTecnica.Tables(0).Rows(0)(Modelo.V_OTT_VIABILIDAD_TECNICALST.TIEMPO_RESPUESTA).ToString, Integer)
                        Case Unidades.HORAS
                            vln_CantidadDiasTotal = (CType(vlo_DsViabilidadTecnica.Tables(0).Rows(0)(Modelo.V_OTT_VIABILIDAD_TECNICALST.VALOR_UNIDAD_TIEMPO).ToString, Integer) / 24) * CType(vlo_DsViabilidadTecnica.Tables(0).Rows(0)(Modelo.V_OTT_VIABILIDAD_TECNICALST.TIEMPO_RESPUESTA).ToString, Integer)
                        Case Unidades.DIAS
                            vln_CantidadDiasTotal = (CType(vlo_DsViabilidadTecnica.Tables(0).Rows(0)(Modelo.V_OTT_VIABILIDAD_TECNICALST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * CType(vlo_DsViabilidadTecnica.Tables(0).Rows(0)(Modelo.V_OTT_VIABILIDAD_TECNICALST.TIEMPO_RESPUESTA).ToString, Integer)
                        Case Unidades.SEMANAS
                            vln_CantidadDiasTotal = (7 * CType(vlo_DsViabilidadTecnica.Tables(0).Rows(0)(Modelo.V_OTT_VIABILIDAD_TECNICALST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * CType(vlo_DsViabilidadTecnica.Tables(0).Rows(0)(Modelo.V_OTT_VIABILIDAD_TECNICALST.TIEMPO_RESPUESTA).ToString, Integer)
                        Case Unidades.MESES
                            vln_CantidadDiasTotal = (30 * CType(vlo_DsViabilidadTecnica.Tables(0).Rows(0)(Modelo.V_OTT_VIABILIDAD_TECNICALST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * CType(vlo_DsViabilidadTecnica.Tables(0).Rows(0)(Modelo.V_OTT_VIABILIDAD_TECNICALST.TIEMPO_RESPUESTA).ToString, Integer)
                        Case Unidades.ANIOS
                            vln_CantidadDiasTotal = (365 * CType(vlo_DsViabilidadTecnica.Tables(0).Rows(0)(Modelo.V_OTT_VIABILIDAD_TECNICALST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * CType(vlo_DsViabilidadTecnica.Tables(0).Rows(0)(Modelo.V_OTT_VIABILIDAD_TECNICALST.TIEMPO_RESPUESTA).ToString, Integer)
                    End Select

                    'calcula la cantidad de dias restantes de la excepcion
                    vln_DiferenciaDias = DateDiff(DateInterval.Day, DateTime.Now, CType(vlo_DsViabilidadTecnica.Tables(0).Rows(0)(Modelo.V_OTT_VIABILIDAD_TECNICALST.TIME_STAMP).ToString, DateTime).AddDays(vln_CantidadDiasTotal))

                    If vln_DiferenciaDias <= 0 Then
                        vlo_FilaOrdenTrabajo(Modelo.OTT_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO) = EstadoOrden.LIQUIDADA
                        vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                        vlo_EntOttTrazabilidadProceso.IdUbicacion = CType(vlo_FilaOrdenTrabajo(Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION).ToString, Integer)
                        vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = vlo_FilaOrdenTrabajo(Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO).ToString
                        vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = EstadoOrden.LIQUIDADA
                        vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = CType(vlo_FilaOrdenTrabajo(Modelo.OTT_ORDEN_TRABAJO.NUM_EMPLEADO).ToString, Integer)
                        vlo_EntOttTrazabilidadProceso.Usuario = "SISTEMA"
                        vlo_EntOttTrazabilidadProceso.Observaciones = "Liquidada por vencimiento de plazo para revisión de viabilidad técnica."
                        ' vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now

                        vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                    End If

                Next

                'Envia el dataset de ordenes  a el adapter respectivo, para la modificacion de los estados
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo.AdapterOttOrdenesTrabajoModificacion(vlo_DsOrdenTrabajo)

                'finaliza la transaccion
                vlo_Conexion.TransaccionCommit()

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

        ''' <summary>
        ''' En caso de vencimiento del plazo indicado para la aprobación en el ante proyecto la orden de trabajo debe ser liquidada
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>11/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function LiquidarOrdenTrabajoFueraDePlazoAprobacionAnteProyeto()
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsOrdenTrabajo As Data.DataSet
            Dim vlo_DsAnteProyecto As Data.DataSet
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttAnteproyecto As DalOttAnteproyecto
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                'inicia la transaccion
                vlo_Conexion.TransaccionBegin()

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttAnteproyecto = New DalOttAnteproyecto(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)

                vlo_DsOrdenTrabajo = vlo_DalOttOrdenTrabajo.ListarRegistrosLista(String.Format("{0} = '{1}'", Modelo.OTT_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO, EstadoOrden.ANTEPROYECTO_PENDIENTE_REVISION_SOLICITANTE), String.Empty, False, 0, 0)

                For Each vlo_FilaOrdenTrabajo In vlo_DsOrdenTrabajo.Tables(0).Rows
                    Dim vln_CantidadDiasTotal As Long = 0
                    Dim vln_DiferenciaDias As Long = 0
                    vlo_DsAnteProyecto = vlo_DalOttAnteproyecto.ListarRegistrosLista(String.Format("{0} = {1} AND {2} = '{3}' AND {4} = 1 AND {5} = (SELECT MAX({5}) FROM {6} WHERE {0} = {1} AND {2} = '{3}' AND {4} = 1)", Modelo.OTT_ANTEPROYECTO.ID_UBICACION, vlo_FilaOrdenTrabajo(Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION).ToString, Modelo.OTT_ANTEPROYECTO.ID_ORDEN_TRABAJO, vlo_FilaOrdenTrabajo(Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO).ToString, Modelo.OTT_ANTEPROYECTO.EDITABLE, Modelo.OTT_ANTEPROYECTO.VERSION, Modelo.OTT_ANTEPROYECTO.Name), String.Empty, False, 0, 0)

                    Select Case CType(vlo_DsAnteProyecto.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.ID_UNIDAD_TIEMPO).ToString, Integer)
                        Case Unidades.MINUTOS
                            vln_CantidadDiasTotal = (CType(vlo_DsAnteProyecto.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer) / 1440) * CType(vlo_DsAnteProyecto.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.TIEMPO_RESPUESTA).ToString, Integer)
                        Case Unidades.HORAS
                            vln_CantidadDiasTotal = (CType(vlo_DsAnteProyecto.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer) / 24) * CType(vlo_DsAnteProyecto.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.TIEMPO_RESPUESTA).ToString, Integer)
                        Case Unidades.DIAS
                            vln_CantidadDiasTotal = (CType(vlo_DsAnteProyecto.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * CType(vlo_DsAnteProyecto.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.TIEMPO_RESPUESTA).ToString, Integer)
                        Case Unidades.SEMANAS
                            vln_CantidadDiasTotal = (7 * CType(vlo_DsAnteProyecto.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * CType(vlo_DsAnteProyecto.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.TIEMPO_RESPUESTA).ToString, Integer)
                        Case Unidades.MESES
                            vln_CantidadDiasTotal = (30 * CType(vlo_DsAnteProyecto.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * CType(vlo_DsAnteProyecto.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.TIEMPO_RESPUESTA).ToString, Integer)
                        Case Unidades.ANIOS
                            vln_CantidadDiasTotal = (365 * CType(vlo_DsAnteProyecto.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * CType(vlo_DsAnteProyecto.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.TIEMPO_RESPUESTA).ToString, Integer)
                    End Select

                    'calcula la cantidad de dias restantes de la excepcion
                    vln_DiferenciaDias = DateDiff(DateInterval.Day, DateTime.Now, CType(vlo_DsAnteProyecto.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.TIME_STAMP).ToString, DateTime).AddDays(vln_CantidadDiasTotal))

                    If vln_DiferenciaDias <= 0 Then
                        vlo_FilaOrdenTrabajo(Modelo.OTT_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO) = EstadoOrden.LIQUIDADA
                        vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                        vlo_EntOttTrazabilidadProceso.IdUbicacion = CType(vlo_FilaOrdenTrabajo(Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION).ToString, Integer)
                        vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = vlo_FilaOrdenTrabajo(Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO).ToString
                        vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = EstadoOrden.LIQUIDADA
                        vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = CType(vlo_FilaOrdenTrabajo(Modelo.OTT_ORDEN_TRABAJO.NUM_EMPLEADO).ToString, Integer)
                        vlo_EntOttTrazabilidadProceso.Usuario = "SISTEMA"
                        vlo_EntOttTrazabilidadProceso.Observaciones = "Liquidada por vencimiento de plazo para aprobación por parte del solicitante."
                        ' vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now

                        vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                    End If

                Next

                'Envia el dataset de ordenes  a el adapter respectivo, para la modificacion de los estados
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo.AdapterOttOrdenesTrabajoModificacion(vlo_DsOrdenTrabajo)

                'finaliza la transaccion
                vlo_Conexion.TransaccionCommit()

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

        ''' <summary>
        ''' Si la OT llega a 0 Días como plazo es decir se vence el plazo la orden debe ser liquidada y notificar a la jefatura y al usuario 
        ''' Esto debe ejecutarse diariamente. 
        ''' </summary>
        ''' <param name="pvc_Usuario"></param>
        ''' <param name="pvc_Clave"></param>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>06/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function LiquidarOrdenTrabajoAprobacionPresupuestoSolicitante()
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsDatos As Data.DataSet
            Dim vlo_DsDatosPresupuesto As Data.DataSet
            Dim vlo_DsAnteProyecto As Data.DataSet
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_BllOttOrdenTrabajo As BllOttOrdenTrabajo
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vln_Unidad As Integer = 0
            Dim vln_TiempoRespuesta As Integer = 0
            Dim vln_CantidadDiasTotal As Long = 0
            Dim vln_DiasRestantes As Long = 0

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_BllOttOrdenTrabajo = New BllOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()

                'Calcula los dias restantes y los agrega a una columna
                vlo_DsDatosPresupuesto = ListarRespuestasSolicitante(String.Format("{0} = '{1}'",
                                Modelo.V_OT_RESPUESTAS_USUARIO.ESTADO_ORDEN_TRABAJO,
                                EstadoOrden.PRESUPUESTO_PENDIENTE_RESPUESTA_SOLICITANTE), String.Empty, False, 0, 0)

                For Each vlo_fila As Data.DataRow In vlo_DsDatosPresupuesto.Tables(0).Rows

                    If vlo_fila(Modelo.V_OT_RESPUESTAS_USUARIO.DIAS_FALTANTES) <= 0 Then

                        vlo_fila(Modelo.V_OT_RESPUESTAS_USUARIO.ESTADO_ORDEN_TRABAJO) = EstadoOrden.LIQUIDADA

                        vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                        vlo_EntOttTrazabilidadProceso.IdUbicacion = CType(vlo_fila(Modelo.V_OT_RESPUESTAS_USUARIO.ID_UBICACION).ToString, Integer)
                        vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = vlo_fila(Modelo.V_OT_RESPUESTAS_USUARIO.ID_ORDEN_TRABAJO).ToString
                        vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = EstadoOrden.LIQUIDADA
                        vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = CType(vlo_fila(Modelo.V_OT_RESPUESTAS_USUARIO.NUM_EMPLEADO).ToString, Integer)
                        vlo_EntOttTrazabilidadProceso.Usuario = "SISTEMA"
                        vlo_EntOttTrazabilidadProceso.Observaciones = "Liquidada por vencimiento de plazo para aprobación por parte del solicitante."
                        'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now

                        vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                    End If

                Next


                'Envia el dataset de ordenes  a el adapter respectivo, para la modificacion de los estados
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo.AdapterOttOrdenesTrabajoModificacion(vlo_DsDatosPresupuesto)


                vlo_Conexion.TransaccionCommit()

            Catch ex As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(ex, Utilerias.OrdenesDeTrabajo.Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw
                End If
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
                If vlo_DsDatos IsNot Nothing Then
                    vlo_DsDatos.Dispose()
                End If
            End Try

        End Function

        ''' <summary>
        ''' Funcion que almacena los archivos adjuntos de la elaboración de planos ademas de modificar el estado de la orden de trabajo
        ''' </summary>
        ''' <param name="pvo_EntOttOrdenTrabajo"></param>
        ''' <param name="pvo_DsAdjuntosInsertar"></param>
        ''' <param name="pvo_DsAdjuntosBorrar"></param>
        ''' <param name="pvb_Finalizar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez García</author>
        ''' <creationDate>15/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Function GuardarPlanos(pvo_EntOttOrdenTrabajo As EntOttOrdenTrabajo, pvo_DsAdjuntosInsertar As DataSet, pvo_DsAdjuntosBorrar As DataSet, pvb_Finalizar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_NuevaFila As Data.DataRow
            Dim vlo_DsArchivos As Data.DataSet
            Dim vlo_resultado As Integer = 1

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                'inicia la transaccion
                vlo_Conexion.TransaccionBegin()

                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)

                'Se borran los archivos que el usuario registro como borrados, si hay alguno
                If pvo_DsAdjuntosBorrar.Tables.Count > 0 AndAlso pvo_DsAdjuntosBorrar.Tables(0).Rows.Count > 0 Then
                    vlo_DalOttAdjuntoOrdenTrabajo.AdapterOtTAdjunto(pvo_DsAdjuntosBorrar)
                End If

                'Se carga la estructura basica del listado
                vlo_DsArchivos = vlo_DalOttAdjuntoOrdenTrabajo.ListarRegistros("0=1", String.Empty, False, 0, 0)

                For Each vlo_fila As Data.DataRow In pvo_DsAdjuntosInsertar.Tables(0).Rows
                    'Si el archivo NO es nulo se ingresa en la tabla
                    If Not TypeOf vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ARCHIVO) Is DBNull Then
                        vlo_NuevaFila = vlo_DsArchivos.Tables(0).NewRow
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESCRIPCION) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESCRIPCION)
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ETAPA_ORDEN_TRABAJO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ETAPA_ORDEN_TRABAJO)
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_TIPO_DOCUMENTO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_TIPO_DOCUMENTO)
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_UBICACION) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_UBICACION)
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.NOMBRE_ARCHIVO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.NOMBRE_ARCHIVO)
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ARCHIVO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ARCHIVO)
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.USUARIO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.USUARIO)

                        vlo_DsArchivos.Tables(0).Rows.Add(vlo_NuevaFila)
                    End If
                Next

                'Se insertan los archivos que no estaban presente en la lista antes
                If vlo_DsArchivos.Tables.Count > 0 AndAlso vlo_DsArchivos.Tables(0).Rows.Count > 0 Then
                    vlo_DalOttAdjuntoOrdenTrabajo.AdapterOtTAdjunto(vlo_DsArchivos)
                End If

                'Se modifica la orden de trabajo cuando se desea finalizar la etapa de planos
                If pvb_Finalizar AndAlso pvo_EntOttOrdenTrabajo.Existe Then
                    vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                    pvo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.ELABORACION_PRESUPUESTO
                    vlo_resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_EntOttOrdenTrabajo)
                End If


                vlo_Conexion.TransaccionCommit()

                Return vlo_resultado

            Catch ex As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(ex, Utilerias.OrdenesDeTrabajo.Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw
                End If
            End Try
        End Function

        ''' <summary>
        ''' Inserta datos del personal propuest para la evaluación
        ''' </summary>
        ''' <param name="pvc_Condicion"></param>
        ''' <param name="pvc_Orden"></param>
        ''' <param name="pvb_Paginar"></param>
        ''' <param name="pvn_NumeroPagina"></param>
        ''' <param name="pvn_TamanoPagina"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez García</author>
        ''' <creationDate>04/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Function ListarRegistrosLista(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DsDatos As Data.DataSet
            Dim vlo_resultado As Integer = 1
            Dim vlo_Empleado As WsrEU_Curriculo.EntEmpleados
            Dim vlo_personal() As String
            Dim vlo_Funcionarios As System.Text.StringBuilder
            Dim vlo_filaPersonal As String



            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If


                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_Funcionarios = New System.Text.StringBuilder
                vlo_DsDatos = vlo_DalOttOrdenTrabajo.ListarRegistrosLista(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

                If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila As Data.DataRow In vlo_DsDatos.Tables(0).Rows
                        vlo_filaPersonal = vlo_fila(Modelo.V_OTT_ORDEN_TRABAJOLST.PERSONAL_EVALUACION).ToString
                        If vlo_filaPersonal IsNot Nothing Then
                            If Not String.IsNullOrWhiteSpace(vlo_filaPersonal) Then
                                vlo_personal = vlo_filaPersonal.ToString.Split(",")
                                vlo_Funcionarios.Clear()
                                For Each vlo_numEmpleado As String In vlo_personal
                                    vlo_Empleado = CargarFuncionario(vlo_numEmpleado)
                                    vlo_Funcionarios.Append(vlo_Empleado.NOMBRE)
                                    vlo_Funcionarios.Append(" ")
                                    vlo_Funcionarios.Append(vlo_Empleado.APELLIDO1)
                                    vlo_Funcionarios.Append(" ")
                                    vlo_Funcionarios.Append(vlo_Empleado.APELLIDO2)
                                    vlo_Funcionarios.Append(", ")
                                Next
                                vlo_fila(Modelo.V_OTT_ORDEN_TRABAJOLST.PERSONAL) = vlo_Funcionarios.ToString.Substring(0, vlo_Funcionarios.Length - 2)
                            End If
                        End If

                    Next
                End If

                Return vlo_DsDatos

            Catch ex As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(ex, Utilerias.OrdenesDeTrabajo.Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw
                End If
            End Try
        End Function

        ''' <summary>
        ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
        ''' </summary>
        ''' <param name="pvc_idPersonal"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>04/04/2016</creationDate>
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
        ''' Obtiene ordenes de trabajo de diseño que deben ser respondidas por el usuario 
        ''' </summary>
        ''' <param name="pvc_Condicion"></param>
        ''' <param name="pvc_Orden"></param>
        ''' <param name="pvb_Paginar"></param>
        ''' <param name="pvn_NumeroPagina"></param>
        ''' <param name="pvn_TamanoPagina"></param>
        ''' <returns></returns>
        ''' <author>César Bermúdez García</author>
        ''' <creationDate>26/02/2016</creationDate>
        ''' <changeLog></changeLog>
        ''' <remarks></remarks>
        Function ListarRespuestasSolicitante(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As Data.DataSet
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_EntOttAdjuntoOrdenTrabajo As EntOttAdjuntoOrdenTrabajo
            Dim vlo_DsDatos As Data.DataSet
            Dim vld_fechaTrazabilidad As Date
            Dim vln_Unidad As Integer = 0
            Dim vln_TiempoRespuesta As Integer = 0
            Dim vln_CantidadDiasTotal As Long = 0
            Dim vln_DiasRestantes As Long = 0

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)

                vlo_DsDatos = vlo_DalOttOrdenTrabajo.ListarRespuestasSolicitante(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)
                If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then

                    '
                    '
                    'Se crean columnas nuevas para adjuntar el archivo y el nombre del archivo dependiendo del estado de la orden de trabajo


                    Dim colDecimal As Data.DataColumn = New Data.DataColumn(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO)
                    colDecimal.DataType = System.Type.GetType("System.Byte[]")
                    vlo_DsDatos.Tables(0).Columns.Add(colDecimal)

                    colDecimal = New Data.DataColumn(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)
                    colDecimal.DataType = System.Type.GetType("System.String")
                    vlo_DsDatos.Tables(0).Columns.Add(colDecimal)

                    For Each vlo_fila As Data.DataRow In vlo_DsDatos.Tables(0).Rows

                        'Basados en el estado de la orden se carga el adjunto de la respectiva etapa
                        'ademas de la unidad de tiempo y del tiempo de respuesta maximo para el cálculo de días hábiles

                        Select Case vlo_fila(Modelo.V_OT_RESPUESTAS_USUARIO.ESTADO_ORDEN_TRABAJO)

                            Case EstadoOrden.PRESUPUESTO_PENDIENTE_RESPUESTA_SOLICITANTE

                                vlo_EntOttAdjuntoOrdenTrabajo = vlo_DalOttAdjuntoOrdenTrabajo.ObtenerRegistro(
                                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}",
                                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION,
                                              vlo_fila.Item(Modelo.V_OT_RESPUESTAS_USUARIO.ID_UBICACION),
                                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO,
                                              vlo_fila.Item(Modelo.V_OT_RESPUESTAS_USUARIO.ID_ORDEN_TRABAJO),
                                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO,
                                              EtapasOrdenTrabajo.PRESUPUESTO))
                                vln_Unidad = CInt(vlo_fila.Item(Modelo.V_OT_RESPUESTAS_USUARIO.UNIDAD_TIEMPO_PRESUPUESTO))
                                vln_TiempoRespuesta = CInt(vlo_fila.Item(Modelo.V_OT_RESPUESTAS_USUARIO.TIEMPO_RESPUESTA_PRESUPUESTO))

                            Case EstadoOrden.PENDIENTE_RESPUESTA_SOLICITANTE
                                vlo_EntOttAdjuntoOrdenTrabajo = vlo_DalOttAdjuntoOrdenTrabajo.ObtenerRegistro(
                                String.Format("{0} = {1} AND {2} = '{3}'",
                                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION,
                                              vlo_fila.Item(Modelo.V_OT_RESPUESTAS_USUARIO.ID_UBICACION),
                                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO,
                                              vlo_fila.Item(Modelo.V_OT_RESPUESTAS_USUARIO.ID_ORDEN_TRABAJO)))

                                vln_Unidad = CInt(vlo_fila.Item(Modelo.V_OT_RESPUESTAS_USUARIO.UNIDAD_TIEMPO_VIABILIDAD))
                                vln_TiempoRespuesta = CInt(vlo_fila.Item(Modelo.V_OT_RESPUESTAS_USUARIO.TIEMPO_RESPUESTA_VIABILIDAD))

                            Case EstadoOrden.ANTEPROYECTO_PENDIENTE_REVISION_SOLICITANTE
                                vlo_EntOttAdjuntoOrdenTrabajo = vlo_DalOttAdjuntoOrdenTrabajo.ObtenerRegistro(
                                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}",
                                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION,
                                              vlo_fila.Item(Modelo.V_OT_RESPUESTAS_USUARIO.ID_UBICACION),
                                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO,
                                              vlo_fila.Item(Modelo.V_OT_RESPUESTAS_USUARIO.ID_ORDEN_TRABAJO),
                                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO,
                                              EtapasOrdenTrabajo.ANTEPROYECTO))

                                vln_Unidad = CInt(vlo_fila.Item(Modelo.V_OT_RESPUESTAS_USUARIO.UNIDAD_TIEMPO_ANTEPROYECTO))
                                vln_TiempoRespuesta = CInt(vlo_fila.Item(Modelo.V_OT_RESPUESTAS_USUARIO.TIEMPO_RESPUESTA_ANTEPROYECTO))

                        End Select

                        vld_fechaTrazabilidad = CDate(vlo_fila.Item(Modelo.V_OT_RESPUESTAS_USUARIO.FECHA_TRAZABILIDAD))

                        Select Case vln_Unidad
                            Case Unidades.MINUTOS
                                vln_CantidadDiasTotal = (CType(vlo_fila(Modelo.V_OT_RESPUESTAS_USUARIO.VALOR_UNIDAD_TIEMPO).ToString, Integer) / 1440) * vln_TiempoRespuesta
                            Case Unidades.HORAS
                                vln_CantidadDiasTotal = (CType(vlo_fila(Modelo.V_OT_RESPUESTAS_USUARIO.VALOR_UNIDAD_TIEMPO).ToString, Integer) / 24) * vln_TiempoRespuesta
                            Case Unidades.DIAS
                                vln_CantidadDiasTotal = (CType(vlo_fila(Modelo.V_OT_RESPUESTAS_USUARIO.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * vln_TiempoRespuesta
                            Case Unidades.SEMANAS
                                vln_CantidadDiasTotal = (7 * CType(vlo_fila(Modelo.V_OT_RESPUESTAS_USUARIO.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * vln_TiempoRespuesta
                            Case Unidades.MESES
                                vln_CantidadDiasTotal = (30 * CType(vlo_fila(Modelo.V_OT_RESPUESTAS_USUARIO.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * vln_TiempoRespuesta
                            Case Unidades.ANIOS
                                vln_CantidadDiasTotal = (365 * CType(vlo_fila(Modelo.V_OT_RESPUESTAS_USUARIO.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * vln_TiempoRespuesta
                        End Select

                        vln_DiasRestantes = DateDiff(DateInterval.Day, Now, vld_fechaTrazabilidad.AddDays(vln_CantidadDiasTotal))
                        vlo_fila.Item(Modelo.V_OT_RESPUESTAS_USUARIO.DIAS_FALTANTES) = vln_DiasRestantes

                        vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO) = vlo_EntOttAdjuntoOrdenTrabajo.Archivo

                        vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO) = vlo_EntOttAdjuntoOrdenTrabajo.NombreArchivo

                    Next
                End If

                Return vlo_DsDatos
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Guarda y finaliza la entrega de diseño, que guarda un archivo para la clausula penal y envia correos
        ''' </summary>
        ''' <param name="pvo_EntOttAdjuntoOrdenTrabajo"></param>
        ''' <param name="pvb_Enviar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Cesar Bermudez G</author>
        ''' <creationDate>17/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Function GuardarEntregaDis(pvo_EntOttAdjuntoOrdenTrabajo As EntOttAdjuntoOrdenTrabajo, pvb_Enviar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
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
                Else
                    If pvo_EntOttAdjuntoOrdenTrabajo.Archivo IsNot Nothing Then
                        vlo_Resultado = vlo_DalOttAdjuntoOrdenTrabajo.ModificarRegistro(pvo_EntOttAdjuntoOrdenTrabajo)
                    End If

                End If

                'Si se desea guardar y enviar
                If pvb_Enviar Then
                    vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'",
                                                Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION, pvo_EntOttAdjuntoOrdenTrabajo.IdUbicacion,
                                                Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_EntOttAdjuntoOrdenTrabajo.IdOrdenTrabajo))
                    'se obtiene la orden de trabajo y se le cambia el estado de la misma a PENDIENTE_REVISION_CONTRATACIONES
                    If vlo_EntOttOrdenTrabajo.Existe Then
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PENDIENTE_REVISION_CONTRATACIONES
                        vlo_Resultado = vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)
                        EnviarCorreoRevision(vlo_EntOttOrdenTrabajo)
                    End If
                End If

                vlo_Conexion.TransaccionCommit()

                Return vlo_Resultado

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
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
        Private Function EnviarCorreoRevision(pvo_EntOttOrdenTrabajo As EntOttOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_DalOtmSectorTaller As AccesoDatos.Catalogos.DalOtmSectorTaller
            Dim vlo_DalOtpParametroUbicacion As AccesoDatos.Catalogos.DalOtpParametroUbicacion
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab
            Dim vlc_Condicion As String
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

                'Se obtiene la lista de cedulas los encargados de la contratación
                vlc_ListaUsuariosRoL = CargarUsuariosRol(RolesSistema.OT_ENCARGADO_CONTRATACION)

                'Se obtiene el nombre y correo del solicitante
                vlo_EntEmpleados = CargarFuncionario(pvo_EntOttOrdenTrabajo.NumEmpleado)

                vlc_NombreSolicitante = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                vlc_CorreoSolicitante = vlo_EntEmpleados.CORREO_INSTITUCIONAL

                'Se obtiene el correo del profesional
                If vlo_DsOperarioOrdenTrabajo.Tables(0) IsNot Nothing AndAlso vlo_DsOperarioOrdenTrabajo.Tables(0).Rows.Count > 0 Then
                    vlc_ProfEncargado = vlo_DsOperarioOrdenTrabajo.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA).ToString
                End If

                vlo_EntEmpleados = CargarFuncionario(vlc_ProfEncargado)
                vlc_NombreProfEncargado = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)

                vlc_ProfEncargado = vlo_EntEmpleados.CORREO_INSTITUCIONAL

                'Obtiene el correo del administrador del sistema
                vlo_DsParametros = vlo_DalOtpParametroUbicacion.ListarRegistrosLista(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR),
                                                                                  String.Empty, False, 0, 0)

                If vlo_DsParametros.Tables.Count > 0 AndAlso vlo_DsParametros.Tables(0).Rows.Count > 0 Then
                    vlc_CorreoAdministrador = vlo_DsParametros.Tables(0).Rows(0).Item(Modelo.OTP_PARAMETRO_UBICACION.VALOR)
                End If


                Return NotificacionListaUsuarios(vlc_ListaUsuariosRoL, vlc_ProfEncargado, vlc_NombreProfEncargado, pvo_EntOttOrdenTrabajo.NombreProyecto, pvo_EntOttOrdenTrabajo.IdOrdenTrabajo, vlc_CorreoAdministrador)
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
        ''' Envia el correo electrónico y devuelve un valor mayor a cero si tuvo éxito o menor a cero si ocurrió un fallo
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez G</author>
        ''' <creationDate>29/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Function NotificacionListaUsuarios(pvc_ListaUsuariosRoL As String(), pvc_CorreoProfesional As String, pvc_NombreProfesional As String, pvc_NombreProyecto As String, pvc_idOrdenTrabajo As String, pvc_CorreoAdministrador As String) As Integer
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

                            '{0}: Numero de orden de trabajo
                            vlo_Notificacion.ASUNTO = String.Format("Tramite de Contratación para la Orden de Trabajo N°{0}", pvc_idOrdenTrabajo)

                            '{0}: Nombre del encargado de contrataciones
                            '{1}: Apellido 1 del encargado de contrataciones
                            '{2}: Apellido 2 del encargado de contrataciones
                            '{3}: Nombre del profesional
                            '{4}: Nombre del proyecto
                            '{5}: Correo del administrador del sistema

                            vlo_Notificacion.CUERPO = String.Format("<b>Señor(a):{0} {1} {2}</b><br /><br /><b>Estimado(a) señor(a)</b><br />Se le notifica que el señor(a):{3}, ha concluido con el expediente técnico requerido para proceder con el trámite de contratación para el proyecto: {4}; Sírvase revisar dicha notificacion en el sistema.<br /><hr><i>Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema refiérase a la dirección: {5}</i>",
                                                   vlo_Empleado.NOMBRE, vlo_Empleado.APELLIDO1, vlo_Empleado.APELLIDO2, pvc_NombreProfesional, pvc_NombreProyecto, pvc_CorreoAdministrador)


                            vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
                            vlo_EntGNT_DESTINATARIO.DESTINATARIO = vlo_Empleado.CORREO_INSTITUCIONAL
                            vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)

                            'Solo deben enviarse una vez estos correos, por ello se coloca la bandera
                            If vlb_bandera Then

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
        ''' <creationDate>18/07/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtReporteOrdenTrabPersonalizado(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As Data.DataSet
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsRegistros As Data.DataSet
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
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

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)

                vlo_DsRegistros = vlo_DalOttOrdenTrabajo.ListarVOtReporteOrdenTrab(
                    pvc_Condicion,
                    pvc_Orden,
                    pvb_Paginar,
                    pvn_NumeroPagina,
                    pvn_TamanoPagina)

                vlo_DsDatos.Tables(0).PrimaryKey = New DataColumn() {vlo_DsDatos.Tables(0).Columns("Codigo_Ubica")}

                For Each vlo_DrFilaOrdenTrabajo In vlo_DsRegistros.Tables(0).Rows

                    vlo_DrFilaUnidad = vlo_DsDatos.Tables(0).Rows.Find(New Object() {vlo_DrFilaOrdenTrabajo(Modelo.V_OT_REPORTE_ORDEN_TRAB.COD_UNIDAD_SIRH).ToString})

                    vlo_DrFilaOrdenTrabajo(Modelo.V_OT_REPORTE_ORDEN_TRAB.NOMBRE_UNIDAD) = vlo_DrFilaUnidad("Cod_Desc")

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
