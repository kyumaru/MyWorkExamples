Imports Wsr_OT_OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo


Partial Class OrdenesDeTrabajo_Frm_OT_HijasRechazadas
    Inherits System.Web.UI.Page
#Region "Propiedades"


    ''' <summary>
    ''' Operacion actual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>8/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property Operacion As String
        Get
            Return CType(ViewState("Operacion"), String)
        End Get
        Set(value As String)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la orden, pasado por parámetro desde la pantalla de listado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdOrdenTrabajo As String
        Get
            Return CType(ViewState("IdOrdenTrabajo"), String)
        End Get
        Set(value As String)
            ViewState("IdOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Parámetro del número de empleado pasado desde la pantalla del listado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property NumEmpleado As String
        Get
            Return CType(ViewState("NumEmpleado"), String)
        End Get
        Set(value As String)
            ViewState("NumEmpleado") = value
        End Set
    End Property

    ''' <summary>
    ''' Parámetro del número de empleado coordinador pasado desde la pantalla del listado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property NumEmpleadoCoordinador As String
        Get
            Return CType(ViewState("NumEmpleadoCoordinador"), String)
        End Get
        Set(value As String)
            ViewState("NumEmpleadoCoordinador") = value
        End Set
    End Property

    ''' <summary>
    ''' Id ubicacion de la orden de trabajo hija
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdUbicacion As String
        Get
            Return CType(ViewState("IdUbicacion"), String)
        End Get
        Set(value As String)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Orden de trabajo actual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property OrdenTrabajo As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario actual en sesion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para guardar el empleado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property Empleado As WsrEU_Curriculo.EntEmpleados
        Get
            Return CType(ViewState("Empleado"), WsrEU_Curriculo.EntEmpleados)
        End Get
        Set(value As WsrEU_Curriculo.EntEmpleados)
            ViewState("Empleado") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para cargar la ubiación favorita
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property AutorizadoUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Get
            Return CType(ViewState("AutorizadoUbicacion"), Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
            ViewState("AutorizadoUbicacion") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Carga los datos de la orden de trabajo a Reasignar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.Empleado = CargarFuncionario(Me.Usuario.UserName)
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)
                If Me.AutorizadoUbicacion.Existe Then
                    InicializarFormulario()
                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ninguna ubicación autorizada, contactar con el administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al presionar el boton aceptar, 
    ''' Las acciones son VB: Visto bueno o D: Denegada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>8/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            Select Case Me.Operacion
                Case Is = "VB"
                    VistoBueno(Me.IdOrdenTrabajo, Me.NumEmpleado)
                    WebUtils.RegistrarScript(Me.Page, "MensajePopup", "MensajePopup('Operación de Visto Bueno realizada con éxito','Lst_OT_HijasRechazadas.aspx');")
                Case Is = "D"
                    Denegar(Me.IdOrdenTrabajo, IIf(String.IsNullOrWhiteSpace(Me.NumEmpleadoCoordinador), Me.NumEmpleado, Me.NumEmpleadoCoordinador))
                    WebUtils.RegistrarScript(Me.Page, "MensajePopup", "MensajePopup('Operación de denegar realizada con éxito','Lst_OT_HijasRechazadas.aspx');")

            End Select
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Modifica el comportamiento del formulario cuando se escoge una de las opciones del radiobutton
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Condicion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtVistoBueno.CheckedChanged, rbtDenegada.CheckedChanged
        Try

            If Me.rbtVistoBueno.Checked Then
                Me.rvftxtMotivo.Enabled = False
                Me.Operacion = "VB"
                Me.lblAccion.Text = "Visto Bueno al motivo de rechazo"
            End If

            If Me.rbtDenegada.Checked Then
                Me.rvftxtMotivo.Enabled = True
                Me.Operacion = "D"
                Me.lblAccion.Text = "Denegación al motivo de rechazo"
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub


#End Region

#Region "Metodos"

    Private Sub MostrarAlertaOperacionVistoBuenoExitosa()
        WebUtils.RegistrarScript(Me, "exitoVistoBueno", "mostrarAlertaOperacionVistoBuenoExitosa();")
    End Sub

    Private Sub MostrarAlertaOperacionDenegacionExitosa()
        WebUtils.RegistrarScript(Me, "exitoDenegacion", "mostrarAlertaOperacionDenegacionExitosa();")
    End Sub

    ''' <summary>
    ''' Inicializa el formulario, además deshabilita campos y validadores según corresponda
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()

        Try
            Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
            Me.NumEmpleado = WebUtils.LeerParametro(Of String)("pvc_NumEmpleado")
            Me.NumEmpleadoCoordinador = WebUtils.LeerParametro(Of String)("pvc_NumEmpleadoCoordinador")
            Me.IdUbicacion = WebUtils.LeerParametro(Of String)("pvc_IdUbicacion")

            Me.Operacion = WebUtils.LeerParametro(Of String)("pvc_OP")
            CargarOrdenTrabajo()

            Select Case Me.Operacion
                Case Is = "VB"
                    Me.rbtVistoBueno.Checked = True
                    Condicion_CheckedChanged("", New EventArgs)
                Case Is = "D"
                    Me.rbtDenegada.Checked = True
                    Condicion_CheckedChanged("", New EventArgs)
            End Select

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Genera un llamado para ejecutar la accion de visto bueno
    ''' </summary>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <param name="pvc_NumEmpleado"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub VistoBueno(pvc_IdOrdenTrabajo As String, pvc_NumEmpleado As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        Try

            'Configurar el servicio de Ordenes de Trabajo
            vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
            vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
            vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

            vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_VistoBuenoOrdenTrabajoHija(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_IdOrdenTrabajo, pvc_NumEmpleado, Me.txtMotivo.Text)

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Llama a un metodo que ejecuta la accion de denegar la orden de trabajo hija
    ''' </summary>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <param name="pvc_NumEmpleado"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Denegar(pvc_IdOrdenTrabajo As String, pvc_NumEmpleado As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        Try
            'Configurar el servicio de Ordenes de Trabajo
            vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
            vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
            vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

            vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_DenegacionOrdenTrabajoHija(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_IdOrdenTrabajo, pvc_NumEmpleado, Me.txtMotivo.Text)

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Permite Cargar una orden de trabajo por el Id
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenTrabajo()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_lugarTrabajo As String
        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{2}' AND {1} = {3}", Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, Me.IdOrdenTrabajo, Me.IdUbicacion))

            vlc_lugarTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerFnOtConsultaLugarTrabajo(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.OrdenTrabajo.IdCategoriaServicio, Me.OrdenTrabajo.IdActividad, Me.OrdenTrabajo.IdLugarTrabajo)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

        If Me.OrdenTrabajo.Existe Then
            With Me.OrdenTrabajo
                Me.lblFechaSolicitud.Text = .FechaHoraSolicita.ToString
                Me.lblSolicitante.Text = String.Format("{0} {1} {2}", Me.Empleado.NOMBRE, Me.Empleado.APELLIDO1, Me.Empleado.APELLIDO2)
                Me.lblPersonaContacto.Text = .NombrePersonaContacto
                Me.lblTelefono.Text = .Telefono
                Me.lblLugarExacto.Text = .SennasExactas
                Me.lblEdificio.Text = CargarLugarTrabajo(.IdLugarTrabajo).Nombre
                Me.lblCategServ.Text = CargarCategoriaServicio(.IdCategoriaServicio).Descripcion
                Me.lblActividad.Text = CargarActividad(.IdActividad).Descripcion
                Me.lblDescTrabajo.Text = .DescripcionTrabajo
                Me.lblSectorTaller.Text = vlc_lugarTrabajo
            End With
        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Carga el funcionario desde el servicio EUCurriculo
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionario(pvn_IdPersonal As String) As WsrEU_Curriculo.EntEmpleados
        Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("ID_PERSONAL = '{0}'", pvn_IdPersonal))
        Catch ex As Exception
            Throw
        Finally
            If vlo_WsEU_Curriculo IsNot Nothing Then
                vlo_WsEU_Curriculo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga la ubicacion favorita de un funcionario por numero de empleado
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarAutorizadoUbicacion(pvn_NumEmpleado As Integer) As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Dim Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        Ws_OT_Catalogos.Timeout = -1
        Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return Ws_OT_Catalogos.OTM_AUTORIZADO_UBICACION_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTM_AUTORIZADO_UBICACION.NUM_EMPLEADO, pvn_NumEmpleado))
        Catch ex As Exception
            Throw
        Finally
            If Ws_OT_Catalogos IsNot Nothing Then
                Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Retorna una entidad de tipo lugar trabajo, segun el parámetro obtenido
    ''' </summary>
    ''' <param name="pvn_IdLugarTrabajo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarLugarTrabajo(pvn_IdLugarTrabajo As Integer) As Wsr_OT_Catalogos.EntOtmLugarTrabajo
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_Catalogos.OTM_LUGAR_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_LUGAR_TRABAJO.ID_LUGAR_TRABAJO, pvn_IdLugarTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Retorna una entidad de tipo categoria servicio, segun el parámetro obtenido
    ''' </summary>
    ''' <param name="pvn_IdCategoriaServicio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarCategoriaServicio(pvn_IdCategoriaServicio As Integer) As Wsr_OT_Catalogos.EntOtmCategoriaServicio
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO, pvn_IdCategoriaServicio))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function


    ''' <summary>
    ''' Retorna una entidad de tipo actividad, segun el parámetro obtenido
    ''' </summary>
    ''' <param name="pvn_IdActividad"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarActividad(pvn_IdActividad As Integer) As Wsr_OT_Catalogos.EntOtmActividad
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_Catalogos.OTM_ACTIVIDAD_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_ACTIVIDAD, pvn_IdActividad))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

#End Region
End Class
