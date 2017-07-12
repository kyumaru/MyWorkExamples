Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Frm_OT_FichaTecnicaGeneral
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/11/2015</creationDate>
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
    ''' <creationDate>30/11/2015</creationDate>
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
    ''' propiedad para la ficha tecnica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property FichaTecnicaGeneral As EntOtfFichaTecnicaGeneral
        Get
            Return CType(ViewState("FichaTecnicaGeneral"), EntOtfFichaTecnicaGeneral)
        End Get
        Set(value As EntOtfFichaTecnicaGeneral)
            ViewState("FichaTecnicaGeneral") = value
        End Set
    End Property

    ''' <summary>
    ''' tamaño en megas del archivo a cargar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Property TamanoArchivo As Integer
        Get
            Return CType(ViewState("TamanoArchivo"), Integer)
        End Get
        Set(value As Integer)
            ViewState("TamanoArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' pantalla de retorno
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property PantallaRetorno As String
        Get
            Return CType(ViewState("PantallaRetorno"), String)
        End Get
        Set(value As String)
            ViewState("PantallaRetorno") = value
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
    ''' <creationDate>30/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    '''' <summary>
    ''' evento que se ejecuta cuando se da click en el boton de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptarOculto_Click(sender As Object, e As EventArgs) Handles btnAceptarOculto.Click
        If Page.IsValid Then
            Try
                'If Me.rbSiPresup.Checked Then
                Select Case (Me.Operacion)
                    Case Is = eOperacion.Agregar
                        If Agregar() Then
                            WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No ha sido posible almacenar la información del nuevo registro")
                        End If

                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No ha sido posible actualizar la información del registro")
                        End If
                End Select
                'Else
                'MostrarAlertaError("Debe contar con disponibilidad presupuestaria para solicitar la evaluacion de proyectos de diseño, de lo contrario no se podrá ejecutar.")
                'End If
            Catch ex As Exception
                If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                    Dim vlo_OrdenesDeTrabajoException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                    MostrarAlertaError(vlo_OrdenesDeTrabajoException.Message)
                Else

                End If
            End Try
        End If
    End Sub

    ''' <summary>
    ''' elimina el archivo del registro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnEliminarArchivo_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminarArchivo.Click
        Try
            Me.FichaTecnicaGeneral.Archivo = Nothing
            Me.FichaTecnicaGeneral.NombreArchivo = ""
            Me.ifArchivo.Visible = True
            Me.lnkArchivo.Visible = False
            Me.btnEliminarArchivo.Visible = False
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' descargar archivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivo_Click(sender As Object, e As EventArgs) Handles lnkArchivo.Click
        DescargaArchivo(Me.FichaTecnicaGeneral.Archivo, Me.FichaTecnicaGeneral.NombreArchivo)
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Método encargado de descargar archivos
    ''' </summary>
    ''' <param name="pvo_Archivo">bytes del archivo a descargar</param>
    ''' <param name="pvc_NombreArchivo">nombre del archivo a descargar</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub DescargaArchivo(pvo_Archivo As Byte(), pvc_NombreArchivo As String)
        pvc_NombreArchivo = pvc_NombreArchivo.Replace(" ", "")
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + pvc_NombreArchivo)
            Response.BinaryWrite(pvo_Archivo)
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar el formulario segun la operacion a realizar
    ''' en caso de ser agregar carga el texo con "Agregar", en caso de ser modificar 
    ''' cargar el texto con "Modificar" y llama al método que obtiene los datos del autorizado, segun el id 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")

        Me.TamanoArchivo = CargarTamañoMaximoArchivo()

        PantallaRetorno = WebUtils.LeerParametro(Of String)("pvc_PantallaRetorno")
        Me.Session.Add("pvc_PantallaRetorno", PantallaRetorno)

        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar " + Me.lblAccion.Text
                Me.rdbNoMe.Checked = True
                Me.rdbNoMo.Checked = True
                Me.rdbNoCa.Checked = True
                Me.rdbNoRa.Checked = True
                Me.rbNoPresup.Checked = True
                Me.ifArchivo.Visible = True
                Me.lnkArchivo.Visible = False
                Me.btnEliminarArchivo.Visible = False
            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar " + Me.lblAccion.Text
                Try
                    CargarFichaTecnicaGeneral(WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion"), WebUtils.LeerParametro(Of Integer)("pvc_IdOrdenTrabajo"))

                Catch ex As Exception
                    Throw
                End Try
        End Select
    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la ficha segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarFichaTecnicaGeneral(pvn_IdUbicacion As Integer, pvn_IdOrdenTrabajo As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.FichaTecnicaGeneral = vlo_Ws_OT_OrdenesDeTrabajo.OTF_FICHA_TECNICA_GENERAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_FICHA_TECNICA_GENERAL.ID_UBICACION, pvn_IdUbicacion, Modelo.OTF_FICHA_TECNICA_GENERAL.ID_PRE_ORDEN_TRABAJO, pvn_IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

        If Me.FichaTecnicaGeneral.Existe Then

            With Me.FichaTecnicaGeneral
                If .ConservaMobiliario = 1 Then
                    Me.rdbSiMe.Checked = True
                Else
                    Me.rdbNoMe.Checked = True
                End If

                If .CuentaConPresupuesto = 1 Then
                    Me.rbSiPresup.Checked = True
                Else
                    Me.rbNoPresup.Checked = True
                End If

                If .RequiereNuevoMobiliario = 1 Then
                    Me.rdbSiMo.Checked = True
                Else
                    Me.rdbNoMo.Checked = True
                End If

                '  Me.txtOtros.Text = IIf(.OtrosMobiliario = "-", String.Empty, .OtrosMobiliario)
                Me.txtOtrosReq.Text = IIf(.OtroTipoRequerimiento = "-", String.Empty, .OtroTipoRequerimiento)
                If .CuentaConAlarma = 1 Then
                    Me.rdbSiCa.Checked = True
                Else
                    Me.rdbNoCa.Checked = True
                End If

                If .RequiereAlarma = 1 Then
                    Me.rdbSiRa.Checked = True
                Else
                    Me.rdbNoRa.Checked = True
                End If

                If Not .Archivo Is Nothing Then
                    Me.lnkArchivo.Text = .NombreArchivo
                    Me.ifArchivo.Visible = False
                    Me.lnkArchivo.Visible = True
                    Me.btnEliminarArchivo.Visible = True
                Else
                    Me.ifArchivo.Visible = True
                    Me.lnkArchivo.Visible = False
                    Me.btnEliminarArchivo.Visible = False
                End If

            End With
        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion encargada de contruir el registro  de la ficha
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As EntOtfFichaTecnicaGeneral
        Dim vlo_EntOtfFichaTecnicaGeneral As EntOtfFichaTecnicaGeneral

        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtfFichaTecnicaGeneral = New EntOtfFichaTecnicaGeneral
            vlo_EntOtfFichaTecnicaGeneral.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
            vlo_EntOtfFichaTecnicaGeneral.IdPreOrdenTrabajo = WebUtils.LeerParametro(Of Integer)("pvc_IdOrdenTrabajo")
        Else
            vlo_EntOtfFichaTecnicaGeneral = Me.FichaTecnicaGeneral
        End If

        With vlo_EntOtfFichaTecnicaGeneral
            .ConservaMobiliario = IIf(Me.rdbSiMe.Checked, 1, 0)
            .RequiereNuevoMobiliario = IIf(Me.rdbSiMo.Checked, 1, 0)
            .CuentaConPresupuesto = IIf(Me.rbSiPresup.Checked, 1, 0)
            .OtroTipoRequerimiento = Me.txtOtrosReq.Text.Trim

            If Me.ifArchivo.Visible Then
                If Me.ifArchivo.FileName <> String.Empty Then
                    .NombreArchivo = Me.ifArchivo.FileName
                    .Archivo = Me.ifArchivo.FileBytes
                End If
            End If

            .CuentaConAlarma = IIf(Me.rdbSiCa.Checked, 1, 0)
            .RequiereAlarma = IIf(Me.rdbSiRa.Checked, 1, 0)
            .Usuario = Me.Usuario.UserName
        End With

        Me.Session.Add("pvn_IdUbicacion", vlo_EntOtfFichaTecnicaGeneral.IdUbicacion)
        Me.Session.Add("pvc_IdOrdenTrabajo", vlo_EntOtfFichaTecnicaGeneral.IdPreOrdenTrabajo)

        Return vlo_EntOtfFichaTecnicaGeneral
    End Function

    ''' <summary>
    ''' Administra el proceso para agregar una ficha
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOtfFichaTecnicaGeneral As EntOtfFichaTecnicaGeneral

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtfFichaTecnicaGeneral = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTF_FICHA_TECNICA_GENERAL_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtfFichaTecnicaGeneral) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Administra el proceso para modificar una ficha
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOtfFichaTecnicaGeneral As EntOtfFichaTecnicaGeneral

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtfFichaTecnicaGeneral = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTF_FICHA_TECNICA_GENERAL_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtfFichaTecnicaGeneral) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' megas del archivo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarTamañoMaximoArchivo() As Integer
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtpParametro As Wsr_OT_Catalogos.EntOtpParametroGlobal
        Dim vlc_Tamano As Integer
        Dim vln_TamanoBytesMega As Integer

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtpParametro = vlo_Ws_OT_Ws_OT_Catalogos.OTP_PARAMETRO_GLOBAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_GLOBAL.ID_PARAMETRO, Parametros.TAMAÑO_MAXIMO_ARCHIVOS))

            vln_TamanoBytesMega = 1048576
            vlc_Tamano = vlo_EntOtpParametro.Valor * vln_TamanoBytesMega
            Return vlc_Tamano
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

#End Region

End Class
