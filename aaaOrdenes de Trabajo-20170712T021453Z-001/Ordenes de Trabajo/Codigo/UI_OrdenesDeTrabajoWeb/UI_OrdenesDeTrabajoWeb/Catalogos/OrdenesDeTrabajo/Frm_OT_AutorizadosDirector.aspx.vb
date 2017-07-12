Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos

Partial Class Catalogos_Frm_OT_AutorizadosDirector
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
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
    ''' Prpoiedad para cargar datos del empleado en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
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
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsFuncionariosUnidad As Data.DataSet
        Get
            Return CType(ViewState("DsFuncionariosUnidad"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsFuncionariosUnidad") = value
        End Set
    End Property

#End Region

#Region "Eventos"


    ''' <summary>
    ''' Evento que se ejecuta al cargar la página, inicializa los componentes necesarios
    ''' para el funcionamiento de la página
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.Empleado = CargarFuncionario(Me.Usuario.UserName)
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click en el boton aceptar para agregar un nuevo registro
    ''' llama a la funcion procesar y muestra un mensaje segun la operacion realizada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try
                Select Case (Me.Operacion)
                    Case Is = eOperacion.Agregar
                        If Agregar() Then
                            AsignarRole(Me.txtIdentificacion.Text)
                            WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("No ha sido posible almacenar la información del nuevo registro")
                        End If

                    Case Is = eOperacion.Modificar
                        'TODO
                End Select
            Catch ex As Exception
                If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                    Dim vlo_OrdenesDeTrabajoException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                    MostrarAlertaError(vlo_OrdenesDeTrabajoException.Message)
                Else
                    Dim vlo_ControlDeErrores As New ControlDeErrores
                    vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
                End If
            End Try
        End If
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se cambia el valor del combo de unidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlUnidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUnidad.SelectedIndexChanged
        Try
            If Me.ddlUnidad.SelectedValue <> String.Empty Then
                Me.DsFuncionariosUnidad = CargarFuncionariosUbicacion(Me.ddlUnidad.SelectedValue)
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkGrid_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Try
            LimpiarFormulario()
            CargarDatosEmpleado(e.CommandArgument.ToString)
            WebUtils.RegistrarScript(Me.Page, "Recargar", "inicializarScript();mostrarPopUp('#CerrarPopUpBusquedaFuncionario');")
        Catch ex As Exception
            Throw
        End Try
    End Sub


    Private Sub ibBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibBuscar.Click
        Try
            BuscarEmpleados(ObtenerCondicionDeBusqueda)
            WebUtils.RegistrarScript(Me.Page, "Recargar", "inicializarScript();")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ibLimpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibLimpiar.Click
        LimpiarFormulario()
        WebUtils.RegistrarScript(Me.Page, "Recargar", "inicializarScript();")
    End Sub

    Protected Sub grdEmpleados_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdEmpleados.PageIndexChanging
        Try
            Me.grdEmpleados.PageIndex = e.NewPageIndex
            BuscarEmpleados(ObtenerCondicionDeBusqueda)
            WebUtils.RegistrarScript(Me.Page, "Recargar", "inicializarScript();")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub lnkEjecutarBusqueda_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusqueda.Click
        Try
            If Me.ddlUnidad.SelectedValue <> String.Empty Then
                WebUtils.RegistrarScript(Me.Page, "mostrarPopUpBusquedaFuncionario", "javascript:mostrarPopUp('#PopUpBusquedaFuncionario');inicializarScript();")
            Else
                WebUtils.RegistrarScript(Me.Page, "SeleccioneUnidad", "mostrarAlertaError('Seleccione la Unidad.');inicializarScript();")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarDatosEmpleado(pvn_NumeroEmpleado As Integer)
        Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados

        Try

            vlo_EntEmpleados = CargarFuncionarioNumEmpleado(pvn_NumeroEmpleado)

            Me.txtIdentificacion.Text = vlo_EntEmpleados.ID_PERSONAL
            Me.lblNombre.Text = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)

            Me.upIdPersonal.Update()
            Me.uplblNombre.Update()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Private Sub BuscarEmpleados(pvc_CondicionBusquedas As String)
        Dim vlo_DsEmpleados As Data.DataSet
        Dim vlo_DataView As Data.DataView

        Try

            vlo_DsEmpleados = Me.DsFuncionariosUnidad

            If String.IsNullOrWhiteSpace(pvc_CondicionBusquedas) Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "MensajeRetorno", "javascript:alert('Debe indicar algún criterio de búsqueda.');", True)
            Else

                vlo_DataView = New Data.DataView(vlo_DsEmpleados.Tables(0))
                vlo_DataView.RowFilter = pvc_CondicionBusquedas

                If vlo_DataView IsNot Nothing AndAlso vlo_DataView.Count > 0 Then
                    With Me.grdEmpleados
                        .DataSource = vlo_DataView
                        .DataMember = vlo_DataView.Table.TableName
                        .DataBind()
                    End With
                Else
                    grdEmpleados.DataSource = Nothing
                    grdEmpleados.DataBind()
                End If
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub LimpiarFormulario()
        Me.txtNombre.Text = String.Empty
        Me.txtApellido1.Text = String.Empty
        Me.txtApellido2.Text = String.Empty
        Me.txtIdentificacion.Text = String.Empty
        Me.grdEmpleados.DataSource = Nothing
        Me.grdEmpleados.DataBind()
        Me.txtApellido1.Focus()
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar el formulario segun la operacion a realizar
    ''' en caso de ser agregar carga el texo con "Agregar", en caso de ser modificar 
    ''' cargar el texto con "Modificar" y llama al método que obtiene los datos de la orden seleccionada segun el id 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        CargarUnidades()

        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Autorizado"
            Case Is = eOperacion.Modificar
                'TODO

        End Select

    End Sub

    ''' <summary>
    ''' Carga las unidades a las cuales esta relacionado el usuario en session
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarUnidades()
        Dim vlo_DsUnidades As Data.DataSet

        Try
            vlo_DsUnidades = ObtenerUnidadesAcargoDeFuncionario(Me.Empleado.NUM_EMPLEADO)

            If vlo_DsUnidades IsNot Nothing AndAlso vlo_DsUnidades.Tables(0).Rows.Count > 0 Then
                With Me.ddlUnidad
                    .Items.Add(New ListItem("[Seleccione la unidad]", String.Empty))
                    .DataSource = vlo_DsUnidades
                    .DataMember = vlo_DsUnidades.Tables(0).TableName
                    .DataTextField = "DESCRIPCION"
                    .DataValueField = "COD_UNIDAD_SIRH"
                    .DataBind()
                End With

                If vlo_DsUnidades.Tables(0).Rows.Count = 1 Then
                    Me.ddlUnidad.SelectedIndex = 1
                    Me.trUnidad.Visible = False

                    Me.DsFuncionariosUnidad = CargarFuncionariosUbicacion(Me.ddlUnidad.SelectedValue)

                Else
                    Me.trUnidad.Visible = True
                End If
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no se encuentra asociado a ninguna unidad.','../../Genericos/Frm_MenuPrincipal.aspx');")
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Asiga el role al usuario autorizado
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AsignarRole(ByVal pvc_NumIdentificacion As String)

        Try
            Dim vlc_RoleName As String
            vlc_RoleName = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ROL_OT_AUTORIZADOR_SOLICITUD)
            Roles.AddUserToRole(pvc_NumIdentificacion, vlc_RoleName)
            '  Roles.AddUsersToRoles(New String() {pvc_NumIdentificacion.Trim}, New String() {vlc_RoleName})
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion encargada de contruir el registro  de la orden de trabajo
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_Catalogos.EntOtmAutorizadoDirector
        Dim vlo_EntOtmAutorizadoDirector As Wsr_OT_Catalogos.EntOtmAutorizadoDirector

        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtmAutorizadoDirector = New Wsr_OT_Catalogos.EntOtmAutorizadoDirector
        Else
            'TODO
        End If

        With vlo_EntOtmAutorizadoDirector
            .CodUnidadSirh = CType(Me.ddlUnidad.SelectedValue, Integer)
            .NumEmpleado = CargarFuncionario(Me.txtIdentificacion.Text).NUM_EMPLEADO
            .Usuario = Me.Usuario.UserName
        End With

        Return vlo_EntOtmAutorizadoDirector
    End Function

    ''' <summary>
    ''' Administra el proceso para agregar un autorizado por director
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmAutorizadoDirector As Wsr_OT_Catalogos.EntOtmAutorizadoDirector

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmAutorizadoDirector = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_AUTORIZADO_DIRECTOR_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmAutorizadoDirector) > 0
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/07/2015</creationDate>
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
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/07/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionarioNumEmpleado(pvn_NumEmpleado As Integer) As WsrEU_Curriculo.EntEmpleados
        Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("NUM_EMPLEADO = {0}", pvn_NumEmpleado))
        Catch ex As Exception
            Throw
        Finally
            If vlo_WsEU_Curriculo IsNot Nothing Then
                vlo_WsEU_Curriculo.Dispose()
            End If
        End Try
    End Function


    ''' <summary>
    ''' Obtiene el listado de unidades que estan a cargo del funcionario parametrizado
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Function ObtenerUnidadesAcargoDeFuncionario(ByVal pvn_NumEmpleado As Integer) As Data.DataSet
        Dim vlo_GestorVacaciones As WsrCatalogosVacaciones.WsCatalogosVacaciones

        Try
            'Inicializa controles
            vlo_GestorVacaciones = New WsrCatalogosVacaciones.WsCatalogosVacaciones
            vlo_GestorVacaciones.Timeout = -1
            vlo_GestorVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials

            Return vlo_GestorVacaciones.PLM_ESTRUCTURA_ORG_ObtenerUbicacionesPorJefesSustitutos(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_NumEmpleado)

        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvn_CodigoUnidad"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarFuncionariosUbicacion(pvn_CodigoUnidad As Integer) As Data.DataSet
        Dim vlo_GestorVacaciones As WsrCatalogosVacaciones.WsCatalogosVacaciones
        Dim vlo_DsFuncionariosUbicacion As Data.DataSet

        Try
            'Inicializa controles
            vlo_GestorVacaciones = New WsrCatalogosVacaciones.WsCatalogosVacaciones
            vlo_GestorVacaciones.Timeout = -1
            vlo_GestorVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials

            vlo_DsFuncionariosUbicacion = vlo_GestorVacaciones.PLM_ESTRUCTURA_ORG_ObtenerEmpleadosPorUbicacion(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_CodigoUnidad)

            vlo_DsFuncionariosUbicacion.Tables(0).Columns.Add("ID_PERSONAL", Type.GetType("System.String"))

            For Each vlo_Fila In vlo_DsFuncionariosUbicacion.Tables(0).Rows
                vlo_Fila("ID_PERSONAL") = CargarFuncionarioNumEmpleado(CType(vlo_Fila("NUM_EMPLEADO"), Integer)).ID_PERSONAL
            Next

            Return vlo_DsFuncionariosUbicacion

        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function ObtenerCondicionDeBusqueda() As String
        Dim vlc_CondicionBusqueda As String

        vlc_CondicionBusqueda = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtIdPersonal.Text) Then
            vlc_CondicionBusqueda = String.Format("ID_PERSONAL LIKE ('%{0}%')", Me.txtIdPersonal.Text.Trim.ToUpper)
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtNombre.Text) Then
            If String.IsNullOrWhiteSpace(vlc_CondicionBusqueda) Then
                vlc_CondicionBusqueda = String.Format("NOMBRE LIKE '%{0}%'", Me.txtNombre.Text.Trim)
            Else
                vlc_CondicionBusqueda = String.Format("{0} AND NOMBRE LIKE '%{1}%'", vlc_CondicionBusqueda, Me.txtNombre.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtApellido1.Text) Then
            If String.IsNullOrWhiteSpace(vlc_CondicionBusqueda) Then
                vlc_CondicionBusqueda = String.Format("NOMBRE LIKE '%{0}%'", Me.txtApellido1.Text.Trim)
            Else
                vlc_CondicionBusqueda = String.Format("{0} AND NOMBRE LIKE '%{1}%'", vlc_CondicionBusqueda, Me.txtApellido1.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtApellido2.Text) Then
            If String.IsNullOrWhiteSpace(vlc_CondicionBusqueda) Then
                vlc_CondicionBusqueda = String.Format("NOMBRE LIKE '%{0}%'", Me.txtApellido2.Text.Trim)
            Else
                vlc_CondicionBusqueda = String.Format("{0} AND NOMBRE LIKE '%{1}%'", vlc_CondicionBusqueda, Me.txtApellido2.Text.Trim)
            End If
        End If

        Return vlc_CondicionBusqueda
    End Function

#End Region

End Class
