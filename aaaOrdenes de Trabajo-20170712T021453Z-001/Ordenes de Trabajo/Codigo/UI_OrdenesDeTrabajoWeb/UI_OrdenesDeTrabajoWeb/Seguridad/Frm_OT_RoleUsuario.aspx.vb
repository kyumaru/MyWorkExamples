Imports Utilerias.OrdenesDeTrabajo
Imports System.Data

Partial Class Seguridad_Frm_OT_RoleUsuario
    Inherits System.Web.UI.Page

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el botón de aceptar
    ''' </summary>
    ''' <param name="sender">parámetro propio del evento</param>
    ''' <param name="e">parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkAceptar_Click(sender As Object, e As EventArgs) Handles lnkAceptar.Click
        Try
            Me.AsignarRoles()
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
    ''' <creationDate>21/09/2015</creationDate>
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub wuc_EmpleadosEU_Aceptar(pvc_NumeroDeEmpleado As Integer, pvc_Identificacion As String, pvc_NombreCompleto As String) Handles wuc_EmpleadosEU.Aceptar
        If Me.wuc_EmpleadosEU.Indicador = 1 Then
            Me.txtIdSolicitante.Text = pvc_Identificacion
            Me.lblNombre.Text = pvc_NombreCompleto
            Me.lblCorreo.Text = pvc_NombreCompleto
            Me.lblCodigoUsuario.Text = pvc_Identificacion
            Me.upTxtIdSolicitante.Update()
            Me.upLblNombre.Update()
            Me.upLblCorreo.Update()
            Me.upLblCodogoUsuario.Update()
        End If

        WebUtils.RegistrarScript(Me.Page, "InicializarFormulario", "javascript:inicializarFormulario();ocultarFiltroFuncionario();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/09/2015</creationDate>
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
            Me.lblCorreo.Text = ""
            Me.lblCodigoUsuario.Text = ""

            If Me.txtIdSolicitante.Text <> "" Then
                pvc_CondicionBusquedas = String.Format("{0} = '{1}'", "ID_PERSONAL", Me.txtIdSolicitante.Text)
                vlo_DsEmpleados = vlo_ServiciosCurriculos.Empleados_ListarRegistrosLista(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB), ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), pvc_CondicionBusquedas, "NOMBRE ASC, APELLIDO1 ASC, APELLIDO2 ASC")
                If vlo_DsEmpleados IsNot Nothing AndAlso vlo_DsEmpleados.Tables.Count > 0 AndAlso vlo_DsEmpleados.Tables(0).Rows.Count > 0 Then
                    Me.lblNombre.Text = String.Format("{0} {1} {2}", vlo_DsEmpleados.Tables(0).Rows(0)(4), vlo_DsEmpleados.Tables(0).Rows(0)(5), vlo_DsEmpleados.Tables(0).Rows(0)(6))
                    Me.lblCorreo.Text = String.Format("{0}", vlo_DsEmpleados.Tables(0).Rows(0)(13))
                    Me.lblCodigoUsuario.Text = Me.txtIdSolicitante.Text.Trim
                Else
                    Me.lblNombre.Text = ""
                    Me.lblCorreo.Text = ""
                    Me.lblCodigoUsuario.Text = ""
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
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método para asignación de roles, guarda una variable de sessión y redirecciona la página.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AsignarRoles()
        Try
            Me.Session.Add("pvc_Username", Me.lblCodigoUsuario.Text.Trim)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("Frm_OT_AsignarRoles.aspx", False)
    End Sub

#End Region

End Class
