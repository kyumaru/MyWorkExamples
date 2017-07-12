Imports Utilerias.OrdenesDeTrabajo

Partial Class Seguridad_Frm_OT_AdministrarRoles
    Inherits System.Web.UI.Page

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta cuando se click sobre el botón de aceptar
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            If CrearRole() Then
                WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
            Else
                MostrarAlertaError("No ha sido posible crear el rol.")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
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

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Función que crea el rol.
    ''' </summary>
    ''' <returns>True: Caso exitoso. False: No se realizo la acción.</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CrearRole() As Boolean
        Dim vlb_OperacionExitosa As Boolean

        Try
            vlb_OperacionExitosa = False

            Roles.CreateRole(Me.txtRole.Text.Trim)
            Return True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Return False
    End Function

#End Region

End Class
