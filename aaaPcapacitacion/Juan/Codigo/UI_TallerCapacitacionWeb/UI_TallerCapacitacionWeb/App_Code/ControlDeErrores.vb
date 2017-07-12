Imports Microsoft.VisualBasic

Public Class ControlDeErrores
#Region "Métodos"
    Public Sub RegistrarExcepcion(ByVal pvo_ExcepcionLanzada As Exception, ByVal pvc_InformacionAdicional As String)
        Dim vlc_NombreLlaveExcepcion As String = String.Format("{0}_ExcepcionLanzada", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DEL_SITIO_WEB))
        Dim vlc_NombreLlaveInformacionAdicional As String = String.Format("{0}_InformacionAdicional", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DEL_SITIO_WEB))

        If HttpContext.Current.Session(vlc_NombreLlaveExcepcion) IsNot Nothing Then
            HttpContext.Current.Session.Remove(vlc_NombreLlaveExcepcion)
        End If

        If HttpContext.Current.Session(vlc_NombreLlaveInformacionAdicional) IsNot Nothing Then
            HttpContext.Current.Session.Remove(vlc_NombreLlaveInformacionAdicional)
        End If

        HttpContext.Current.Session.Add(vlc_NombreLlaveExcepcion, pvo_ExcepcionLanzada)
        HttpContext.Current.Session.Add(vlc_NombreLlaveInformacionAdicional, pvc_InformacionAdicional)

        HttpContext.Current.Server.Transfer(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_RUTA_PAGINA_DE_ERRORES))
    End Sub
#End Region

#Region "Funciones"
    Public Function ObtenerExcepcionLanzada() As Exception
        Dim vlc_NombreLlaveExcepcion As String = String.Format("{0}_ExcepcionLanzada", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DEL_SITIO_WEB))
        Dim vlo_ExcepcionLanzada As Exception

        If HttpContext.Current.Session(vlc_NombreLlaveExcepcion) Is Nothing Then
            Return Nothing
        End If

        vlo_ExcepcionLanzada = CType(HttpContext.Current.Session(vlc_NombreLlaveExcepcion), Exception)
        HttpContext.Current.Session.Remove(vlc_NombreLlaveExcepcion)

        Return vlo_ExcepcionLanzada
    End Function

    Public Function ObtenerInformacionAdicional() As String
        Dim vlc_NombreLlaveInformacionAdicional As String = String.Format("{0}_InformacionAdicional", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DEL_SITIO_WEB))
        Dim vlc_InformacionAdicional As String

        If HttpContext.Current.Session(vlc_NombreLlaveInformacionAdicional) Is Nothing Then
            Return String.Empty
        End If

        vlc_InformacionAdicional = CType(HttpContext.Current.Session(vlc_NombreLlaveInformacionAdicional), String)
        HttpContext.Current.Session.Remove(vlc_NombreLlaveInformacionAdicional)

        Return vlc_InformacionAdicional
    End Function

    Public Function ObtenerDetalleDelError() As String
        Dim vlo_ExcepcionLanzada As Exception
        Dim vlc_InformacionAdicional As String
        Dim vlo_UsuarioActual As New UsuarioActual
        Dim vlc_DetalleError As StringBuilder
        Dim vlc_DetalleErrorAnidado As StringBuilder
        Dim vlo_ExcepcionInterna As Exception

        vlo_ExcepcionLanzada = ObtenerExcepcionLanzada()
        vlc_InformacionAdicional = ObtenerInformacionAdicional()

        vlc_DetalleError = New StringBuilder()
        vlc_DetalleErrorAnidado = New StringBuilder()
        vlc_DetalleError.AppendLine("<hr />")
        vlc_DetalleError.Append("Información de la Excepción:")
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Excepción: {0}", vlo_ExcepcionLanzada.GetType().ToString())
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.Append("--> " + vlo_ExcepcionLanzada.Message.Trim())
        vlc_DetalleError.AppendLine("<br />")

        If Not String.IsNullOrEmpty(vlo_ExcepcionLanzada.StackTrace) Then
            vlc_DetalleError.AppendFormat("Trace: {0}", vlo_ExcepcionLanzada.StackTrace.Trim())
        Else
            vlc_DetalleError.AppendFormat("Trace: ")
        End If

        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendLine("<br />")

        'procesar excepsiones anidades
        If vlo_ExcepcionLanzada.InnerException IsNot Nothing Then
            vlo_ExcepcionInterna = vlo_ExcepcionLanzada.InnerException
            While (vlo_ExcepcionInterna IsNot Nothing)
                vlc_DetalleErrorAnidado.AppendLine("<hr />")
                vlc_DetalleErrorAnidado.AppendLine("Excepción Interna:")
                vlc_DetalleErrorAnidado.AppendLine("<br />")
                vlc_DetalleErrorAnidado.AppendFormat("Excepción: {0}", vlo_ExcepcionInterna.GetType().ToString())
                vlc_DetalleErrorAnidado.AppendLine("<br />")
                vlc_DetalleErrorAnidado.Append("--> " + vlo_ExcepcionInterna.Message.Trim())
                vlc_DetalleErrorAnidado.AppendLine("<br />")
                vlc_DetalleErrorAnidado.AppendFormat("Trace: {0}", vlo_ExcepcionInterna.StackTrace.Trim())
                vlc_DetalleErrorAnidado.AppendLine("<br />")
                vlo_ExcepcionInterna = vlo_ExcepcionInterna.InnerException
            End While
            vlc_DetalleError.Append(vlc_DetalleErrorAnidado.ToString())
        End If

        vlc_DetalleError.AppendLine("<hr />")
        vlc_DetalleError.AppendLine("Información Adicional")
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Aplicación: {0}", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DE_LA_APLICACION).ToString)
        vlc_DetalleError.AppendLine("<br />")
        If vlo_UsuarioActual.Existe Then
            vlc_DetalleError.AppendFormat("Usuario ID: {0}", vlo_UsuarioActual.UserId)
            vlc_DetalleError.AppendLine("<br />")
            vlc_DetalleError.AppendFormat("Usuario Name: {0}", vlo_UsuarioActual.UserName)
            vlc_DetalleError.AppendLine("<br />")
            vlc_DetalleError.AppendFormat("Usuario Correo Electrónico: {0}", vlo_UsuarioActual.CorreoElectronico)
            vlc_DetalleError.AppendLine("<br />")
        Else
            vlc_DetalleError.AppendLine("<strong>No se cuenta con información del usuario en sesión.</strong>")
            vlc_DetalleError.AppendLine("<br />")
        End If
        vlc_DetalleError.AppendFormat("Fecha y hora: {0}", DateTime.Now.ToString())
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Servidor: {0}", System.Environment.MachineName)
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Sistema Operativo del Servidor: {0}", System.Environment.OSVersion)
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Dominio del Servidor: {0}", System.Environment.UserDomainName)
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Usuario Sistema Operativo del Servidor: {0}", System.Environment.UserName)
        vlc_DetalleError.AppendLine("<hr />")
        vlc_DetalleError.AppendLine("Información Adicional del Cliente")
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Navegador: {0}", HttpContext.Current.Request.Browser.Browser)
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Navegador Versión: {0}", HttpContext.Current.Request.Browser.Version)
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Navegador Plataforma: {0}", HttpContext.Current.Request.Browser.Platform)
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Request Tipo: {0}", HttpContext.Current.Request.RequestType)
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Request Url: {0}", HttpContext.Current.Request.Url.ToString)
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Request Url Anterior: {0}", HttpContext.Current.Request.UrlReferrer.ToString)
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Equipo Cliente Dirección IP: {0}", HttpContext.Current.Request.UserHostAddress)
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Equipo Cliente Nombre: {0}", HttpContext.Current.Request.UserHostName)
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendFormat("Equipo Cliente Lenguaje: {0}", IIf(HttpContext.Current.Request.UserLanguages.Count > 0, HttpContext.Current.Request.UserLanguages(0), "--"))
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendLine("<hr />")
        vlc_DetalleError.AppendLine("Información Adicional del programador")
        vlc_DetalleError.AppendLine("<br />")
        vlc_DetalleError.AppendLine(IIf(String.IsNullOrWhiteSpace(vlc_InformacionAdicional), "No indica.", vlc_InformacionAdicional))
        vlc_DetalleError.AppendLine("<hr />")

        Return vlc_DetalleError.ToString()
    End Function
#End Region
End Class