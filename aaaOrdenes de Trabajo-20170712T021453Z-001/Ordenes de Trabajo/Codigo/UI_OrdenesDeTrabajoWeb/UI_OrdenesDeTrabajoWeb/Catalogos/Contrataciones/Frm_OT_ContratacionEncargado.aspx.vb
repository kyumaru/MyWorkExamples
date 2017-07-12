Imports Utilerias.OrdenesDeTrabajo
Imports System.Data
Imports Wsr_OT_Catalogos


Partial Class Catalogos_Frm_OT_ContratacionEncargado
    Inherits System.Web.UI.Page

#Region "Propiedades"
    Private Property operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Encargado As EntOtmEncargadoContrato
        Get
            Return CType(ViewState("Encargado"), EntOtmEncargadoContrato)
        End Get
        Set(value As EntOtmEncargadoContrato)
            ViewState("Encargado") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para la ubicacion autorizada del usuario actual en sesion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>31/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property AutorizadoUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Get
            Return CType(ViewState("AutorizadoUbicacion"), Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
            ViewState("AutorizadoUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en sesion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>31/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property
#End Region

#Region "Eventos"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                inicializarFormulario()
            Catch ex As Exception
                If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso
            CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                    Dim vlo_TallerCapacitacionException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                    MostrarAlertaError(vlo_TallerCapacitacionException.Message)
                    WebUtils.RegistrarScript(Me, "OcultarAreaDeFiltros", "ocultarAreaFiltrosDeBusqueda();")
                Else
                    Dim vlo_ControlDeErrores As New ControlDeErrores
                    vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
                End If
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el botón de aceptar
    ''' </summary>
    ''' <param name="sender">parámetro propio del evento</param>
    ''' <param name="e">parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            If Guardar() Then
                If Me.operacion = eOperacion.Agregar Then
                    WebUtils.RegistrarScript(Me, "RegistroExitoso", "mostrarPopupRegistroExitoso();")
                Else
                    WebUtils.RegistrarScript(Me, "RegistroExitoso", "mostrarAlertaActualizacionExitosa();")
                End If
            End If
        Catch ex As Exception
            If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso
            CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                Dim vlo_TallerCapacitacionException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                MostrarAlertaError(vlo_TallerCapacitacionException.Message)
                WebUtils.RegistrarScript(Me, "OcultarAreaDeFiltros", "ocultarAreaFiltrosDeBusqueda();")
            Else
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkEjecutarBusquedaSolicitante_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusquedaSolicitante.Click
        Try
            Me.wuc_EmpleadosEU.Indicador = 1
            WebUtils.RegistrarScript(Me.Page, "mostrarPopUpBusquedaFuncionario", "javascript:mostrarPopUp('#PopUpBusquedaFuncionario');cargarLupa();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvc_NumeroDeEmpleado"></param>
    ''' <param name="pvc_Identificacion"></param>
    ''' <param name="pvc_NombreCompleto"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub wuc_EmpleadosEU_Aceptar(pvc_NumeroDeEmpleado As Integer, pvc_Identificacion As String, pvc_NombreCompleto As String) Handles wuc_EmpleadosEU.Aceptar
        If Me.wuc_EmpleadosEU.Indicador = 1 Then
            Me.txtIdSolicitante.Text = pvc_Identificacion
            Me.lblNombre.Text = pvc_NombreCompleto
            Me.upTxtIdSolicitante.Update()
            Me.upLblNombre.Update()
        End If

        WebUtils.RegistrarScript(Me.Page, "InicializarFormulario", "javascript:inicializarFormulario();ocultarFiltroFuncionario();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub txtIdSolicitante_TextChanged(sender As Object, e As EventArgs) Handles txtIdSolicitante.TextChanged
        Dim vlo_ServiciosCurriculos As WsrEU_Curriculo.wsEU_Curriculo
        Dim vlo_DsEmpleados As Data.DataSet
        Dim pvc_CondicionBusquedas As String

        Try
            'instanciar y configurar objetos
            vlo_ServiciosCurriculos = New WsrEU_Curriculo.wsEU_Curriculo
            vlo_ServiciosCurriculos.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_ServiciosCurriculos.Timeout = -1

            Me.lblNombre.Text = ""

            If Me.txtIdSolicitante.Text <> "" Then
                pvc_CondicionBusquedas = String.Format("{0} = '{1}'", "ID_PERSONAL", Me.txtIdSolicitante.Text)
                vlo_DsEmpleados = vlo_ServiciosCurriculos.Empleados_ListarRegistrosLista(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB), ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), pvc_CondicionBusquedas, "NOMBRE ASC, APELLIDO1 ASC, APELLIDO2 ASC")
                If vlo_DsEmpleados IsNot Nothing AndAlso vlo_DsEmpleados.Tables.Count > 0 AndAlso vlo_DsEmpleados.Tables(0).Rows.Count > 0 Then
                    Me.lblNombre.Text = String.Format("{0} {1} {2}", vlo_DsEmpleados.Tables(0).Rows(0)(4), vlo_DsEmpleados.Tables(0).Rows(0)(5), vlo_DsEmpleados.Tables(0).Rows(0)(6))
                Else
                    Me.lblNombre.Text = ""
                    Me.txtIdSolicitante.Text = ""
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoEncontrado();")
                End If

            End If
            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Inicializa el formulario dependiendo de la operación recibida por parámetro
    ''' Tambien carga el drop down list de estados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/10/2015</creationDate>
    Private Sub InicializarFormulario()
        CargarEstado()

        Me.Usuario = New UsuarioActual
        Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

        Me.operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")

        Select Case Me.operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Autorizado"
                Me.trEstado.Visible = False
            Case Is = eOperacion.Modificar
                lnkEjecutarBusquedaSolicitante.Visible = False
                txtIdSolicitante.Enabled = False
                Me.lblAccion.Text = "Modificar Autorizado"
                Me.trEstado.Visible = True
                Try
                    CargarAutorizado(WebUtils.LeerParametro(Of String)("pvc_IdAutorizado"))
                Catch ex As Exception
                    Throw
                End Try

        End Select

    End Sub

    Private Sub CargarAutorizado(pvn_idAutorizado As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_Empleado As WsrEU_Curriculo.EntEmpleados

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_Empleado = CargarFuncionario(pvn_idAutorizado)

            Me.Encargado = vlo_Ws_OT_Catalogos.OTM_ENCARGADO_CONTRATO_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1}", Modelo.OTM_ENCARGADO_CONTRATO.NUM_EMPLEADO, vlo_Empleado.NUM_EMPLEADO))

            If Encargado.Existe Then
                Me.txtIdSolicitante.Text = vlo_Empleado.ID_PERSONAL
                Me.lblNombre.Text = vlo_Empleado.NOMBRE & " " & vlo_Empleado.APELLIDO1 & " " & vlo_Empleado.APELLIDO2
                Me.ddlEstado.SelectedValue = Encargado.Estado
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try


    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub CargarEstado()
        Try
            Me.ddlEstado.Items.Clear()
            Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
            Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
        Catch ex As Exception
            Throw
        Finally
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Método para Guardar los datos respectivos en la tabla y asignar el rol OT_ENCARGADO_CONTRATACION
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Guardar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_Empleado As WsrEU_Curriculo.EntEmpleados
        Dim vlo_Resultado As Integer = -1
        Dim vlo_usuarioActual = New UsuarioActual()

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_Empleado = CargarFuncionario(txtIdSolicitante.Text)

                If Not Encargado IsNot Nothing Then
                    Encargado = New EntOtmEncargadoContrato
            End If

            Encargado.IdUbicacion = AutorizadoUbicacion.IdUbicacionAdministra
            Encargado.NumEmpleado = vlo_Empleado.NUM_EMPLEADO
            Encargado.Usuario = vlo_usuarioActual.UserName

            If eOperacion.Agregar = Me.operacion Then
                Encargado.Estado = Estado.ACTIVO
                vlo_Resultado = vlo_Ws_OT_Catalogos.OTM_ENCARGADO_CONTRATO_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Encargado)

                Roles.AddUserToRole(vlo_Empleado.ID_PERSONAL.Trim, RolesSistema.OT_ENCARGADO_CONTRATACION)
            Else
                Encargado.Estado = Me.ddlEstado.SelectedValue
                vlo_Resultado = vlo_Ws_OT_Catalogos.OTM_ENCARGADO_CONTRATO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Encargado)
                If Encargado.Estado = Estado.INACTIVO Then
                    Roles.RemoveUserFromRole(vlo_Empleado.ID_PERSONAL.Trim, RolesSistema.OT_ENCARGADO_CONTRATACION)
                Else
                    Roles.AddUserToRole(vlo_Empleado.ID_PERSONAL.Trim, RolesSistema.OT_ENCARGADO_CONTRATACION)
                End If
            End If

            Return vlo_Resultado > 0
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Return vlo_Resultado > 0
    End Function


    ''' <summary>
    ''' Carga una entidad de tipo autorizado ubicacion
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarAutorizadoUbicacion(pvn_NumEmpleado As Integer) As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_AUTORIZADO_UBICACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_AUTORIZADO_UBICACION.NUM_EMPLEADO, pvn_NumEmpleado))
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
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


#End Region

End Class
