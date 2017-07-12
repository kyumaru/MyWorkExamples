Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos
Imports System.Data


''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
''' <author>César Bermúdez García</author>
''' <creationDate>28/01/2016</creationDate>
Partial Class Catalogos_Frm_OT_TipoDocumento
    Inherits System.Web.UI.Page
#Region "Propiedades"

    ''' <summary>
    ''' Operacion a efectuar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>28/01/2016</creationDate>
    Private Property operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Entidad de tipo documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>28/01/2016</creationDate>
    Private Property TipoDocumento As EntOtmTipoDocumento
        Get
            Return CType(ViewState("TipoDocumento"), EntOtmTipoDocumento)
        End Get
        Set(value As EntOtmTipoDocumento)
            ViewState("TipoDocumento") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad del dataset de oficios, pertenecientes al apoyo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsFormatosAdmitidos As DataTable
        Get
            Return CType(ViewState("DsFormatosAdmitidos"), DataTable)
        End Get
        Set(value As DataTable)
            ViewState("DsFormatosAdmitidos") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad del dataset de oficios, pertenecientes al apoyo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property MaxRequestLength As Integer
        Get
            Return CType(ViewState("MaxRequestLength"), Integer)
        End Get
        Set(value As Integer)
            ViewState("MaxRequestLength") = value
        End Set
    End Property

#End Region

#Region "Eventos"
    ''' <summary>
    ''' Inicializa los componentes del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>28/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Este método se ejecutará al presionar el botón aceptar, dependiendo de la acción modificará o agregará y al final presentará un mensaje
    ''' Si la operación fue exitosa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>28/01/2016</creationDate>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try

                If (Me.MaxRequestLength / 1024) >= CType(Me.txtTamanioMaximo.Text.Trim, Integer) Then

                    Select Case Me.operacion
                        Case Is = eOperacion.Agregar

                            If Agregar() Then
                                WebUtils.RegistrarScript(Me, "RegistroExitoso", "mostrarPopupRegistroExitoso();")
                            Else
                                MostrarAlertaError("No ha sido posible agregar la información del nuevo registro.")
                            End If
                        Case Is = eOperacion.Modificar
                            If Modificar() Then
                                WebUtils.RegistrarScript(Me, "ActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                            Else
                                MostrarAlertaError("No ha sido posible actualizar la información del Tipo de Documento.")
                            End If

                    End Select
                Else
                    MostrarAlertaError("El Tamaño máximo es muy grande, debe ser menor a 10mb.")
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
        End If
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click sobre el boton de eliminar
    ''' del listado   de oficios del apoyo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            BorrarFormatoDataTable(CType(sender, ImageButton).CommandName)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta se carga la lista de oficios, por cada registros del
    ''' repeater se asigna una propiedad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpOficioApoyo_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpFormatos.ItemDataBound
        Dim vlo_IbBorrar As ImageButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click en agregar formato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkAgregarFormato_Click(sender As Object, e As EventArgs) Handles lnkAgregarFormato.Click
        Try
            AgregaFormatosDataTable()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Metodos"
    Private Sub CargarEstado()
        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))

    End Sub

    ''' <summary>
    ''' agrega un formato de documentos admitidos al dataset, estos se encuentran en memoria, y son insertados en la 
    ''' base de tos hasta el final, es decir un vez que se da click sobre el boton de aceptar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>28/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregaFormatosDataTable()
        Dim vlo_DrNuevaFila As DataRow

        Try
            If Me.txtFormatoDescripcion.Text <> String.Empty Then

                If Me.DsFormatosAdmitidos.Rows.Find(New Object() {Me.txtFormatoDescripcion.Text.ToUpper}) Is Nothing Then

                    vlo_DrNuevaFila = Me.DsFormatosAdmitidos.NewRow

                    vlo_DrNuevaFila.Item(Me.DsFormatosAdmitidos.Columns(Modelo.OTM_TIPO_DOCUMENTO.FORMATOS_ADMITIDOS)) = Me.txtFormatoDescripcion.Text.ToUpper
                    vlo_DrNuevaFila.Item(Me.DsFormatosAdmitidos.Columns(Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO)) = 0

                    Me.DsFormatosAdmitidos.Rows.Add(vlo_DrNuevaFila)

                    If Me.DsFormatosAdmitidos IsNot Nothing AndAlso Me.DsFormatosAdmitidos.Rows.Count > 0 Then
                        Me.rpFormatos.DataSource = Me.DsFormatosAdmitidos
                        Me.rpFormatos.DataMember = Me.DsFormatosAdmitidos.TableName
                        Me.rpFormatos.DataBind()
                        Me.rpFormatos.Visible = True
                    Else
                        With Me.rpFormatos
                            .DataSource = Nothing
                            .DataBind()
                        End With
                        Me.rpFormatos.Visible = False
                    End If

                End If

                Me.txtFormatoDescripcion.Text = String.Empty

            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene el registro desde la base de datos y lo carga en memoria
    ''' </summary>
    ''' <param name="pvc_IdTipoDocumento"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>28/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarTipoDocumento(pvc_IdTipoDocumento As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0} Nombre de la columna 
            '{1} numero de ID_TIPO_DOCUMENTO

            Me.TipoDocumento = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0})={1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, pvc_IdTipoDocumento.Trim.ToUpper))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If Me.TipoDocumento.Existe Then
            With Me.TipoDocumento
                Me.txtDescripcion.Text = .Descripcion
                Me.txtTamanioMaximo.Text = .TamanioMaximo
                Me.ddlEstado.SelectedValue = .Estado
            End With
        Else
            WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrrecta", "mostrarAlertaLlaveIncorrrecta();")
        End If


    End Sub

    ''' <summary>
    ''' Inicializa el formulario dependiendo de la operación recibida por parámetro
    ''' Tambien carga el drop down list de estados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>28/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        CargarEstado()
        Me.MaxRequestLength = CType(ConfigurationManager.GetSection("system.web/httpRuntime"), System.Web.Configuration.HttpRuntimeSection).MaxRequestLength

        'Se agregan columnas al datatable vacio
        inicializarSetDatos()

        Me.operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")

        Select Case Me.operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Tipo de Documento"
                Me.btnAceptar.Text = "Agregar"
            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Tipo de Documento"
                Me.btnAceptar.Text = "Modificar"
                Me.trEstado.Visible = True
                Try
                    CargarTipoDocumento(WebUtils.LeerParametro(Of String)("pvc_IdTipoDocumento"))
                    CargarFormatosAdmitidos()
                Catch ex As Exception
                    Throw
                End Try

        End Select

    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' carga los formatos actuales del tipo de archivo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez Garcia</author>
    ''' <creationDate>28/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarFormatosAdmitidos()
        Dim vlo_dtFormatos As DataTable
        Dim vlo_drFormato As DataRow
        Dim vlc_Formatos() As String
        Dim vlo_llaves(1) As DataColumn

        Try
            vlo_dtFormatos = Me.DsFormatosAdmitidos
            'Se obtienen los datos del objeto actual cargado en memoria.
            vlc_Formatos = Me.TipoDocumento.FormatosAdmitidos.Split(",")

            'Por cada uno de los formatos separados por coma se creará una nueva fila,
            'Se le cargarán los datos y se agregará a la tabla
            For Each formato As String In vlc_Formatos
                vlo_drFormato = vlo_dtFormatos.NewRow()
                vlo_drFormato(Modelo.OTM_TIPO_DOCUMENTO.FORMATOS_ADMITIDOS) = formato
                vlo_drFormato(Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO) = Me.TipoDocumento.IdTipoDocumento
                vlo_dtFormatos.Rows.Add(vlo_drFormato)
            Next

            'Se agrega el set de datos al repeater
            If vlo_dtFormatos IsNot Nothing AndAlso vlo_dtFormatos.Rows.Count > 0 Then
                Me.rpFormatos.DataSource = vlo_dtFormatos
                Me.rpFormatos.DataMember = vlo_dtFormatos.TableName
                Me.rpFormatos.DataBind()
                Me.rpFormatos.Visible = True
            Else
                With Me.rpFormatos
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpFormatos.Visible = False
            End If
            'Por último se almacena en la propiedad los datos mostrados 
            Me.DsFormatosAdmitidos = vlo_dtFormatos
        Catch ex As Exception
            Throw
        Finally
            If vlo_dtFormatos IsNot Nothing Then
                vlo_dtFormatos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarSetDatos()
        Dim vlo_columna As DataColumn
        Dim vlo_llaves(1) As DataColumn


        'Se crea un nuevo datatabla 
        Me.DsFormatosAdmitidos = New DataTable
        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.OTM_TIPO_DOCUMENTO.FORMATOS_ADMITIDOS
        'Se agrega la columna configurada al set de datos
        DsFormatosAdmitidos.Columns.Add(vlo_columna)
        'Se agrega al arreglo de llaves primarias la columna
        vlo_llaves(0) = vlo_columna


        vlo_columna = New DataColumn()
        vlo_columna.DataType = System.Type.GetType("System.String")
        vlo_columna.ColumnName = Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO
        DsFormatosAdmitidos.Columns.Add(vlo_columna)

        'Y se configura el set de datos para que busque por formatos admitidos como llave primaria.
        Me.DsFormatosAdmitidos.PrimaryKey = vlo_llaves

    End Sub

    ''' <summary>
    ''' borra un registro del dataset temporal de oficios
    ''' </summary>
    ''' <param name="pvc_CommandName"></param>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>28/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BorrarFormatoDataTable(pvc_CommandName As String)
        Try
            Me.DsFormatosAdmitidos.Rows.Find(New Object() {pvc_CommandName}).Delete()

            If Me.DsFormatosAdmitidos IsNot Nothing AndAlso Me.DsFormatosAdmitidos.Rows.Count > 0 Then
                Me.rpFormatos.DataSource = Me.DsFormatosAdmitidos
                Me.rpFormatos.DataMember = Me.DsFormatosAdmitidos.TableName
                Me.rpFormatos.DataBind()
                Me.rpFormatos.Visible = True
            Else
                With Me.rpFormatos
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpFormatos.Visible = False
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    '''  Construye el registro para ser enviado a la base de datos
    ''' </summary>
    ''' <returns>Entidad de Tipo de documento</returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>28/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As EntOtmTipoDocumento
        Dim vlo_EntOtmTipoDocumento As EntOtmTipoDocumento

        If Me.operacion = eOperacion.Agregar Then
            vlo_EntOtmTipoDocumento = New EntOtmTipoDocumento
        ElseIf Me.operacion = eOperacion.Modificar Then
            vlo_EntOtmTipoDocumento = Me.TipoDocumento
        End If

        Dim vlo_UsuarioActual = New UsuarioActual()
        With vlo_EntOtmTipoDocumento
            .Descripcion = Me.txtDescripcion.Text.ToUpper.Trim
            .TamanioMaximo = Me.txtTamanioMaximo.Text.Trim
            .Estado = IIf(Me.operacion = eOperacion.Modificar, Me.ddlEstado.SelectedValue, Estado.ACTIVO)
            .FormatosAdmitidos = ObtenerFormatosDeseados()
            .Usuario = vlo_UsuarioActual.UserName
            .Protegido = Proteccion.NO_PROTEGIDO

        End With

        Return vlo_EntOtmTipoDocumento

    End Function

    ''' <summary>
    '''  Agrega un tipo de documento nuevo a la tabla OTM_TIPO_DOCUMENTO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>28/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As EntOtmTipoDocumento
        Dim vlo_EntidadOtmTipoDocumento As EntOtmTipoDocumento

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmTipoDocumento = ConstruirRegistro()

        vlo_EntidadOtmTipoDocumento = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = '{1}'", Modelo.OTM_TIPO_DOCUMENTO.DESCRIPCION, vlo_EntOtmTipoDocumento.Descripcion.ToUpper()))

        If Not vlo_EntidadOtmTipoDocumento.Existe Then
            Try
                Return vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_InsertarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOtmTipoDocumento) > 0
            Catch ex As Exception
                Throw
            Finally
                If vlo_Ws_OT_Catalogos IsNot Nothing Then
                    vlo_Ws_OT_Catalogos.Dispose()
                End If
            End Try
        End If


    End Function


    ''' <summary>
    ''' Modifica un registro en la tabla OTM_TIPO_DOCUMENTO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>28/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As EntOtmTipoDocumento

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmTipoDocumento = ConstruirRegistro()

        Try

            Return vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ModificarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOtmTipoDocumento) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Recorre la lista y crea un texto separado por comas con los formatos deseados 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/01/2016</creationDate>
    ''' <changeLog>Carlos Gómez Ondoy   23/02/2016    Se agrega la validación  IF</changeLog>
    Private Function ObtenerFormatosDeseados() As String
        Dim vlc_StringBuiler As New System.Text.StringBuilder

        For Each vlo_row As DataRow In Me.DsFormatosAdmitidos.Rows
            vlc_StringBuiler.Append(vlo_row(Modelo.OTM_TIPO_DOCUMENTO.FORMATOS_ADMITIDOS))
            vlc_StringBuiler.Append(",")
        Next

        If vlc_StringBuiler.ToString <> String.Empty Then
            Return vlc_StringBuiler.ToString.Substring(0, vlc_StringBuiler.ToString.LastIndexOf(","))
        Else
            Return String.Empty
        End If
    End Function

#End Region

End Class
