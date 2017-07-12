Partial Class Controles_wucEmpleadosEU
    Inherits System.Web.UI.UserControl



#Region "Atributos del Objeto"
    Private vlc_NuevoIngreso As String
#End Region

#Region "Propiedades"
    Public Property NuevoIngreso() As String
        Get
            Return vlc_NuevoIngreso
        End Get
        Set(ByVal Value As String)
            vlc_NuevoIngreso = Value
        End Set
    End Property

    Public Property FuncionJavaMostrarPopUp() As String
        Get
            If ViewState("FuncionJavaMostrarPopUp") IsNot Nothing Then
                Return ViewState("FuncionJavaMostrarPopUp").ToString.Trim
            Else
                Return String.Empty
            End If
        End Get
        Set(ByVal value As String)
            ViewState("FuncionJavaMostrarPopUp") = value
        End Set
    End Property

    Public Property Indicador As Integer
        Get
            Return CType(ViewState("Indicador"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Indicador") = value
        End Set
    End Property
#End Region

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        WebUtils.RegistrarScript(Me, "InicializarControl", "javascript:inicializarControl();")
    End Sub

    Private Sub RegistrarScriptMostrarPopUp()
        If String.IsNullOrEmpty(Me.FuncionJavaMostrarPopUp) Then
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "MostrarPopUp", "javascript:MostrarPopup();", True)
        Else
            ScriptManager.RegisterStartupScript(Me.Page, GetType(String), "MostrarPopUp", String.Format("javascript:{0}();", Me.FuncionJavaMostrarPopUp), True)
        End If
    End Sub

    Private Sub ibBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibBuscar.Click
        Try
            BuscarEmpleados(ObtenerCondicionDeBusqueda)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ibLimpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibLimpiar.Click
        LimpiarFormulario()
        '  RegistrarScriptMostrarPopUp()
    End Sub

    Protected Sub grdEmpleados_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdEmpleados.PageIndexChanging
        Try
            Me.grdEmpleados.PageIndex = e.NewPageIndex
            BuscarEmpleados(ObtenerCondicionDeBusqueda)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub lnkGrid_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Dim vlc_Llaves() As String
        Try
            'limpiar formulario
            LimpiarFormulario()

            'leer command argument
            vlc_Llaves = e.CommandArgument.ToString.Split("_")

            RaiseEvent Aceptar(CType(vlc_Llaves(0), Integer), vlc_Llaves(1), String.Format("{0} {1} {2}", vlc_Llaves(2).Trim, vlc_Llaves(3).Trim, vlc_Llaves(4).Trim))
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Event Aceptar(pvc_NumeroDeEmpleado As Integer, pvc_Identificacion As String, pvc_NombreCompleto As String)
    Dim vlb_Consultar As Boolean = False


#End Region

#Region " Metodos "
    Private Sub BuscarEmpleados(pvc_CondicionBusquedas As String)
        Dim vlo_ServiciosCurriculos As WsrEU_Curriculo.wsEU_Curriculo
        Dim vlo_DsEmpleados As Data.DataSet

        Try
            'instanciar y configurar objetos
            vlo_ServiciosCurriculos = New WsrEU_Curriculo.wsEU_Curriculo
            vlo_ServiciosCurriculos.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_ServiciosCurriculos.Timeout = -1

            If String.IsNullOrWhiteSpace(pvc_CondicionBusquedas) Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "MensajeRetorno", "javascript:alert('Debe indicar algún criterio de búsqueda.');", True)
            Else
                vlo_DsEmpleados = vlo_ServiciosCurriculos.Empleados_ListarRegistrosLista(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB), ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), pvc_CondicionBusquedas, "NOMBRE ASC, APELLIDO1 ASC, APELLIDO2 ASC")
                If vlo_DsEmpleados IsNot Nothing AndAlso vlo_DsEmpleados.Tables.Count > 0 AndAlso vlo_DsEmpleados.Tables(0).Rows.Count > 0 Then
                    With Me.grdEmpleados
                        .DataSource = vlo_DsEmpleados
                        .DataMember = vlo_DsEmpleados.Tables(0).TableName
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

    Public Sub Inicializar()
        Me.grdEmpleados.PageSize = CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer)
        LimpiarFormulario()
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
#End Region

#Region "Funciones"
    Private Function ObtenerCondicionDeBusqueda() As String
        Dim vlc_CondicionBusqueda As String

        vlc_CondicionBusqueda = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtIdentificacion.Text) Then
            vlc_CondicionBusqueda = String.Format("UPPER(ID_PERSONAL) LIKE('%{0}%')", Me.txtIdentificacion.Text.Trim.ToUpper)
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtNombre.Text) Then
            If String.IsNullOrWhiteSpace(vlc_CondicionBusqueda) Then
                vlc_CondicionBusqueda = String.Format("UPPER(NOMBRE) LIKE '%{0}%'", Me.txtNombre.Text.Trim.ToUpper)
            Else
                vlc_CondicionBusqueda = String.Format("{0} AND UPPER(NOMBRE) LIKE '%{1}%'", vlc_CondicionBusqueda, Me.txtNombre.Text.Trim.ToUpper)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtApellido1.Text) Then
            If String.IsNullOrWhiteSpace(vlc_CondicionBusqueda) Then
                vlc_CondicionBusqueda = String.Format("UPPER(APELLIDO1) LIKE '%{0}%'", Me.txtApellido1.Text.Trim.ToUpper)
            Else
                vlc_CondicionBusqueda = String.Format("{0} AND UPPER(APELLIDO1) LIKE '%{1}%'", vlc_CondicionBusqueda, Me.txtApellido1.Text.Trim.ToUpper)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtApellido2.Text) Then
            If String.IsNullOrWhiteSpace(vlc_CondicionBusqueda) Then
                vlc_CondicionBusqueda = String.Format("UPPER(APELLIDO2) LIKE '%{0}%'", Me.txtApellido2.Text.Trim.ToUpper)
            Else
                vlc_CondicionBusqueda = String.Format("{0} AND UPPER(APELLIDO2) LIKE '%{1}%'", vlc_CondicionBusqueda, Me.txtApellido2.Text.Trim.ToUpper)
            End If
        End If

        Return vlc_CondicionBusqueda
    End Function
#End Region

End Class