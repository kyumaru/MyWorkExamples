Imports Wsr_TC_Catalogos
Imports Utilerias.TallerCapacitacion

Partial Class Catalogos_Frm_TC_Libro
    Inherits System.Web.UI.Page

#Region "Propiedades"
    Private Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    Private Property Libro As EntTcmLibro
        Get
            Return CType(ViewState("Libro"), EntTcmLibro)
        End Get
        Set(value As EntTcmLibro)
            ViewState("Libro") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    Protected Sub Page_Load(
        sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Try
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)

            End Try
        End If
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        ' hay una serie de validadores que se lanzan desde el cliente pero tambien se ejecutan en el servidor

        If Page.IsValid Then ' si algo falla en la validacion
            Try
                Select Case Me.Operacion
                    Case Is = eOperacion.Agregar
                        If Agregar() Then
                            WebUtils.RegistrarScript(Me, "popupExito", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("No ha sido posible registrar el nuevo libro.")
                        End If
                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me, "popupExito", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No ha sido posible modificar la info del libro.")
                        End If

                End Select
            Catch ex As Exception
                If TypeOf (ex) Is System.Web.Services.Protocols.SoapException _
                    AndAlso CType(ex, System.Web.Services.Protocols.SoapException).Actor = TallerCapacitacionException.NOMBRE_CLASE Then
                    Dim vlo_TallerCapacitacionException As TallerCapacitacionException = TallerCapacitacionException.GetFromSoapException(ex)
                    MostrarAlertaError(vlo_TallerCapacitacionException.Message) ' registra un script para mostar un error
                Else
                    Dim vlo_ControlDeErrores As New ControlDeErrores
                    vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
                End If
            End Try
        End If
    End Sub

#End Region

#Region "Métodos"
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub CargarCondicionLibro()
        Dim vlo_Ws_TC_Catalogos As Ws_TC_Catalogos
        Dim vlo_DsCondicionLibro As System.Data.DataSet
        vlo_Ws_TC_Catalogos = New Ws_TC_Catalogos
        vlo_Ws_TC_Catalogos.Timeout = -1
        vlo_Ws_TC_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsCondicionLibro = vlo_Ws_TC_Catalogos.TCC_CONDICION_LIBRO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Empty,
                String.Empty,
                False,
                0,
                0)
            Me.ddlCondicionLibro.Items.Clear()
            Me.ddlCondicionLibro.Items.Add(New ListItem("[seleccione...]", String.Empty))

            If vlo_DsCondicionLibro.Tables.Count > 0 AndAlso vlo_DsCondicionLibro.Tables(0).Rows.Count > 0 Then
                With Me.ddlCondicionLibro
                    .DataSource = vlo_DsCondicionLibro
                    .DataMember = vlo_DsCondicionLibro.Tables(0).TableName
                    .DataTextField = Modelo.TCC_CONDICION_LIBRO.DESCRIPCION
                    .DataValueField = Modelo.TCC_CONDICION_LIBRO.CONDICION_LIBRO
                    .DataBind()
                End With
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_TC_Catalogos IsNot Nothing Then
                vlo_Ws_TC_Catalogos.Dispose()
            End If
            If vlo_DsCondicionLibro IsNot Nothing Then
                vlo_DsCondicionLibro.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarLibro(pvc_Isbn As String)
        ' busca registro que haga match y carga info en la pantalla
        'creamos var de servicio web ws e instanciamos

        Dim vlo_Ws_TC_Catalogos As Ws_TC_Catalogos

        vlo_Ws_TC_Catalogos = New Ws_TC_Catalogos
        vlo_Ws_TC_Catalogos.Timeout = -1
        vlo_Ws_TC_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            ' 0 columna, 1 valor de busqueda
            Me.Libro = vlo_Ws_TC_Catalogos.TCM_LIBRO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0})='{1}'", Modelo.TCM_LIBRO.ISBN, pvc_Isbn.Trim.ToUpper))
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_TC_Catalogos IsNot Nothing Then
                vlo_Ws_TC_Catalogos.Dispose()
            End If
        End Try

        If Me.Libro.Existe Then
            With Me.Libro
                Me.txtIsbn.Text = .Isbn
                Me.lblIsbn.Text = .Isbn
                Me.txtTitulo.Text = .Titulo
                Me.txtResumen.Text = .Resumen
                Me.txtTotalPaginas.Text = .TotalPaginas
                Me.txtFechaImpresion.Text = .FechaHoraImpresion.ToString(Constantes.FORMATO_FECHA_UI)
                Me.txtHoraImpresion.Text = .FechaHoraImpresion.Hour
                Me.txtMinutosImpresion.Text = .FechaHoraImpresion.Minute
                Me.ddlCondicionLibro.SelectedValue = .CondicionLibro
            End With
        Else
            WebUtils.RegistrarScript(Me, "llaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    Private Sub InicializarFormulario() ' setea los valores iniciales del form
        CargarCondicionLibro()
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion") ' busca en el querystring o en el ...
        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "agregar libro"
                Me.txtIsbn.Attributes.Add("style", "display:block;") ' se muestra
                Me.lblIsbn.Attributes.Add("style", "display:none;")' no se muestra, no usar visible false, el tag html no se hace en el dom

            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "modificar libro"
                Me.txtIsbn.Attributes.Add("style", "display:none;") ' se muestra
                Me.lblIsbn.Attributes.Add("style", "display:block;")

                ' si va a la base de datos puede haber error, try

                Try
                    CargarLibro(WebUtils.LeerParametro(Of String)("pvc_Isbn"))
                Catch ex As Exception
                    Throw
                End Try

        End Select
    End Sub
#End Region

#Region "Funciones"
    Private Function ConstruirRegistro() As EntTcmLibro
        Dim vlo_EntTcmLibro As EntTcmLibro
        Dim vld_FechaHoraImpresion As DateTime

        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntTcmLibro = New EntTcmLibro
        Else
            vlo_EntTcmLibro = Me.Libro
        End If

        With vlo_EntTcmLibro
            If Me.Operacion = eOperacion.Agregar Then
                .Isbn = Me.txtIsbn.Text.Trim

            End If
            .Titulo = Me.txtTitulo.Text.Trim
            .Resumen = Me.txtResumen.Text.Trim
            .TotalPaginas = CType(Me.txtTotalPaginas.Text, Integer)
            .CondicionLibro = Me.ddlCondicionLibro.SelectedValue

            vld_FechaHoraImpresion = CType(Me.txtFechaImpresion.Text, DateTime)
            .FechaHoraImpresion = New DateTime(
                vld_FechaHoraImpresion.Year,
                vld_FechaHoraImpresion.Month,
                vld_FechaHoraImpresion.Day,
                CType(Me.txtHoraImpresion.Text, Integer),
                CType(Me.txtMinutosImpresion.Text, Integer),
                0)
            .Usuario = "RGS"
            '.Usuario=New UsuarioActual().UserName 'esta es la que se usa en produccion
        End With
        Return vlo_EntTcmLibro
    End Function

    Private Function Agregar() As Boolean
        Dim vlo_Ws_TC_Catalogos As Ws_TC_Catalogos
        Dim vlo_EntTcmLibro As EntTcmLibro

        vlo_Ws_TC_Catalogos = New Ws_TC_Catalogos
        vlo_Ws_TC_Catalogos.Timeout = -1
        vlo_Ws_TC_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntTcmLibro = ConstruirRegistro()

        Try
            Return vlo_Ws_TC_Catalogos.TCM_LIBRO_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntTcmLibro) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_TC_Catalogos IsNot Nothing Then
                vlo_Ws_TC_Catalogos.Dispose()
            End If
        End Try

    End Function

    Private Function Modificar() As Boolean
        Dim vlo_Ws_TC_Catalogos As Ws_TC_Catalogos
        Dim vlo_EntTcmLibro As EntTcmLibro

        vlo_Ws_TC_Catalogos = New Ws_TC_Catalogos
        vlo_Ws_TC_Catalogos.Timeout = -1
        vlo_Ws_TC_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntTcmLibro = ConstruirRegistro()

        Try
            Return vlo_Ws_TC_Catalogos.TCM_LIBRO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntTcmLibro) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_TC_Catalogos IsNot Nothing Then
                vlo_Ws_TC_Catalogos.Dispose()
            End If
        End Try

    End Function
#End Region

End Class
