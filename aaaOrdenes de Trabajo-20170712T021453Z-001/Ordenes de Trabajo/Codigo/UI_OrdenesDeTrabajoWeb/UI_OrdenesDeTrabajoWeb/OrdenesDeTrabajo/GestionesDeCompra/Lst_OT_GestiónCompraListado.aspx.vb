Imports Utilerias.OrdenesDeTrabajo
Imports System.Data
Imports Wsr_OT_OrdenesDeTrabajo
Imports Wsr_OT_Catalogos
Partial Class OrdenesDeTrabajo_Almacen_Lst_OT_GestiónCompraListado
    Inherits System.Web.UI.Page
#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortExpression As String
        Get
            If ViewState("UltimoSortExpression") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortExpression"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortExpression") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima columna de clasificacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortColumn As String
        Get
            If ViewState("UltimoSortColumn") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortColumn"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortColumn") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima direccion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortDirection As SortDirection
        Get
            If ViewState("UltimoSortDirection") Is Nothing Then
                Return SortDirection.Ascending
            End If
            Return CType(ViewState("UltimoSortDirection"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("UltimoSortDirection") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en sesion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property


    Public Property UniqueIDBotonBuscar As String
        Get
            If ViewState("UniqueIDBotonBuscar") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UniqueIDBotonBuscar"), String)
        End Get
        Set(value As String)
            ViewState("UniqueIDBotonBuscar") = value
        End Set
    End Property

    Public Property AccesoConcedido As Boolean
        Get
            Return CType(ViewState("AccesoConcedido"), Boolean)
        End Get
        Set(ByVal value As Boolean)
            ViewState("AccesoConcedido") = value
        End Set
    End Property

    ''' <summary>
    ''' ubicacion favorita del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>13/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Property AutorizadaUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Get
            Return CType(ViewState("AutorizadaUbicacion"), Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
            ViewState("AutorizadaUbicacion") = value
        End Set
    End Property

    Public Property ViaCompraSuministros As Integer
        Get
            Return CType(ViewState("ViaCompraSuministros"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("ViaCompraSuministros") = value
        End Set
    End Property

    Public Property ViaCompraUnidadEspec As Integer
        Get
            Return CType(ViewState("ViaCompraUnidadEspec"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("ViaCompraUnidadEspec") = value
        End Set
    End Property

    Public Property ViaCompraFondoTrabajo As Integer
        Get
            Return CType(ViewState("ViaCompraFondoTrabajo"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("ViaCompraFondoTrabajo") = value
        End Set
    End Property

    Public Property IdViaCompraSelec As Integer
        Get
            Return CType(ViewState("IdViaCompraSelec"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("IdViaCompraSelec") = value
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
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
        Me.pnRpFondoTrabajo.Dibujar()
        Me.pnRpSuministros.Dibujar()
        Me.pnRpUnidadCompra.Dibujar()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados de la tabla del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpFondoTrabajo_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerCondicionBusqueda, ObtenerExpresionDeOrdenamiento(e.CommandName), Me.pnRpFondoTrabajo.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los números del paginador
    ''' </summary>
    ''' <param name="pvn_PaginaSeleccionada"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub pnRpFondoTrabajo_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpFondoTrabajo.CambioDePagina
        Try
            CargarLista(ObtenerCondicionBusqueda, UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados de la tabla del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpUnidadCompra_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerCondicionBusqueda, ObtenerExpresionDeOrdenamiento(e.CommandName), Me.pnRpFondoTrabajo.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los números del paginador
    ''' </summary>
    ''' <param name="pvn_PaginaSeleccionada"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub pnRpUnidadCompra_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpUnidadCompra.CambioDePagina
        Try
            CargarLista(ObtenerCondicionBusqueda, UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados de la tabla del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpSuministros_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerCondicionBusqueda, ObtenerExpresionDeOrdenamiento(e.CommandName), Me.pnRpFondoTrabajo.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los números del paginador
    ''' </summary>
    ''' <param name="pvn_PaginaSeleccionada"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub pnRpSuministros_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpSuministros.CambioDePagina
        Try
            CargarLista(ObtenerCondicionBusqueda, UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sredirecciona a la pagina correspondiente segun la seleccion del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibAgregar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Select Case Me.ddlViaCompra.SelectedValue
                'Suministros
                Case Me.ViaCompraSuministros
                    Me.Session.Add("pvn_Modo", 1)
                    Me.Session.Add("pvc_Observaciones", String.Empty)
                    Response.Redirect(String.Format("Frm_OT_GestionCompraSuministros.aspx"), False)

                    'Unidad Especializada de Compras
                Case Me.ViaCompraUnidadEspec
                    Me.Session.Add("pvn_Operacion", eOperacion.Agregar)
                    Response.Redirect(String.Format("Frm_OT_GestionCompraUnidadEspecializada.aspx"), False)

                    'Fondo de Trabajo
                Case Me.ViaCompraFondoTrabajo
                    Me.Session.Add("pvn_Operacion", eOperacion.Agregar)
                    Response.Redirect(String.Format("Frm_OT_GestionCompraFondoTrabajo.aspx"), False)
                Case Else
                    MostrarAlertaError("Debe seleccionar una via de Compra")
            End Select
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el botón Buscar,
    ''' que se encuentra en el área de filtros.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Select Case ddlViaCompra.SelectedValue
                'Suministros
                Case Me.ViaCompraSuministros
                    Me.artFondoTrabajo.Visible = False
                    Me.artSuministros.Visible = True
                    Me.artUnidadDeCompra.Visible = False
                    Buscar(ObtenerCondicionBusqueda, String.Empty)

                    'Unidad Especializada de Compras
                Case Me.ViaCompraUnidadEspec
                    Me.artFondoTrabajo.Visible = False
                    Me.artSuministros.Visible = False
                    Me.artUnidadDeCompra.Visible = True
                    Buscar(ObtenerCondicionBusqueda, String.Empty)

                    'Fondo de Trabajo
                Case Me.ViaCompraFondoTrabajo
                    Me.artFondoTrabajo.Visible = True
                    Me.artSuministros.Visible = False
                    Me.artUnidadDeCompra.Visible = False
                    Buscar(ObtenerCondicionBusqueda, String.Empty)
                Case Else
                    Me.artFondoTrabajo.Visible = False
                    Me.artSuministros.Visible = False
                    Me.artUnidadDeCompra.Visible = False
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "ocultarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            End Select

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub lnkTrazabilidadGestion(sender As Object, e As CommandEventArgs)
        Dim vlc_CommandArgument As String
        Dim vlc_Llave As String()

        vlc_CommandArgument = CType(CType(sender, LinkButton).CommandArgument, String)
        vlc_Llave = vlc_CommandArgument.Split("%")

        Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
        Me.Session.Add("pvn_IdViaCompraContrato", vlc_Llave(1))
        Me.Session.Add("pvn_Anno", vlc_Llave(2))
        Me.Session.Add("pvn_NumeroGestion", vlc_Llave(3))
        Me.Session.Add("pvc_PaginaRegreso", "Lst_OT_GestiónCompraListado.aspx")

        Response.Redirect("Lst_OT_TrazabilidadGestionCompra.aspx", False)

    End Sub

    Protected Sub ibConsultarGestion_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_CommandArgument As String
        Dim vlc_Llave As String()

        vlc_CommandArgument = CType(CType(sender, ImageButton).CommandArgument, String)
        vlc_Llave = vlc_CommandArgument.Split("%")

        Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
        Me.Session.Add("pvn_IdViaCompraContrato", vlc_Llave(1))
        Me.Session.Add("pvn_Anno", vlc_Llave(2))
        Me.Session.Add("pvn_NumeroGestion", vlc_Llave(3))
        Me.Session.Add("pvn_Modo", 2)

        Response.Redirect("Frm_OT_ConsultaGestionCompraSuministros.aspx", False)

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibIngresoGestionGeco_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvn_IdViaCompraContrato", vlc_Llave(1))
            Me.Session.Add("pvn_NumeroGestion", vlc_Llave(2))
            Me.Session.Add("pvn_Annio", vlc_Llave(3))

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect(String.Format("Frm_OT_UnidadEspecializadaCompraInclusionNumeroGeco.aspx"), False)
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>19/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' InicializarFormulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InicializarFormulario()
        Usuario = New UsuarioActual
        ValidarTipoDeRol()
        If Me.AccesoConcedido Then
            Me.AutorizadaUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)
            If Me.AutorizadaUbicacion.Existe Then
                Me.UniqueIDBotonBuscar = Me.btnBuscar.UniqueID

                Me.IdViaCompraSelec = WebUtils.LeerParametro(Of Integer)("pvn_IdViaCompraSelec")

                If Me.IdViaCompraSelec = 0 Then
                    Me.artFondoTrabajo.Visible = False
                    Me.artSuministros.Visible = False
                    Me.artUnidadDeCompra.Visible = False
                    Me.rpFondoTrabajo.Visible = False
                    Me.RpSuministros.Visible = False
                    Me.RpUnidadCompra.Visible = False
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "ocultarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End If
                
                Me.ViaCompraSuministros = CargarParametro(Parametros.VALOR_SECUENCIA_SUMINISTROS)
                Me.ViaCompraUnidadEspec = CargarParametro(Parametros.VALOR_SECUENCIA_UNIDAD_ESPECIALIZADA_COMPRAS)
                Me.ViaCompraFondoTrabajo = CargarParametro(Parametros.VALOR_SECUENCIA_FONDO_DE_TRABAJO)

                CargarEstado()
                CargarFlujoGestionCompra()
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee autorización para acceder a esta página.','../../Genericos/Frm_MenuPrincipal.aspx');")
            End If
        Else

            Me.artFondoTrabajo.Visible = False
            Me.artSuministros.Visible = False
            Me.artUnidadDeCompra.Visible = False
            Me.rpFondoTrabajo.Visible = False
            Me.RpSuministros.Visible = False
            Me.RpUnidadCompra.Visible = False
            WebUtils.RegistrarScript(Me, "visibilidadPaneles", "ocultarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            

            WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee el rol necesario para ingresar a esta página.','../../Genericos/Frm_MenuPrincipal.aspx');")
        End If
    End Sub

    Private Sub CargarEstado()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlc_Orden As String = String.Empty

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        If String.IsNullOrWhiteSpace(vlc_Orden) Then
            vlc_Orden = ObtenerExpresionDeOrdenamiento(Modelo.V_OTC_ESTADO_GESTION_COMPRA.DESCRIPCION)
        End If

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTC_ESTADO_GESTION_COMPRA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Empty,
                vlc_Orden,
                False,
                0,
                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                Me.ddlFiltroEstado.Items.Clear()
                Me.ddlFiltroEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
                With Me.ddlFiltroEstado
                    .DataSource = vlo_DsDatos
                    .DataValueField = Modelo.V_OTC_ESTADO_GESTION_COMPRA.ESTADO
                    .DataTextField = Modelo.V_OTC_ESTADO_GESTION_COMPRA.DESCRIPCION
                    .DataBind()
                End With
                ddlFiltroEstado.SelectedValue = String.Empty
            Else
                With Me.ddlFiltroEstado
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With
                MostrarAlertaNoHayDatos()
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If

            If vlo_DsDatos IsNot Nothing Then
                vlo_DsDatos.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarFlujoGestionCompra()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlc_Orden As String = String.Empty
        Dim vlc_Condicion As String = String.Empty
        Dim vlc_idParametro As String = String.Empty
        Dim vlo_EntOtpParametroUbicacion As EntOtpParametroUbicacion

        vlo_EntOtpParametroUbicacion = New EntOtpParametroUbicacion
        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        If String.IsNullOrWhiteSpace(vlc_Orden) Then
            vlc_Orden = ObtenerExpresionDeOrdenamiento(Modelo.V_OTM_VIA_COMPRA_CONTRATO.DESCRIPCION)
        End If

        Try

            vlc_idParametro = Utilerias.OrdenesDeTrabajo.Parametros.GESTIONES_COMPRA_FINALIZADAS

            vlo_EntOtpParametroUbicacion = vlo_Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, vlc_idParametro))

            If vlo_EntOtpParametroUbicacion.Existe Then

                vlc_Condicion = String.Format("{0} IN({1})", Modelo.OTM_VIA_COMPRA_CONTRATO.ID_VIA_COMPRA_CONTRATO, vlo_EntOtpParametroUbicacion.Valor)

                vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_VIA_COMPRA_CONTRATO_ListarRegistrosLista(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlc_Condicion,
                    vlc_Orden,
                    False,
                    0,
                    0)

                If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    Me.ddlViaCompra.Items.Clear()
                    Me.ddlViaCompra.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
                    With Me.ddlViaCompra
                        .DataSource = vlo_DsDatos
                        .DataValueField = Modelo.V_OTM_VIA_COMPRA_CONTRATO.ID_VIA_COMPRA_CONTRATO
                        .DataTextField = Modelo.V_OTM_VIA_COMPRA_CONTRATO.DESCRIPCION
                        .DataBind()
                    End With

                    If Me.IdViaCompraSelec <> 0 Then
                        ddlViaCompra.SelectedValue = Me.IdViaCompraSelec
                        Me.artFondoTrabajo.Visible = False
                        Me.artSuministros.Visible = True
                        Me.artUnidadDeCompra.Visible = False
                        Buscar(ObtenerCondicionBusqueda, String.Empty)
                    Else
                        ddlViaCompra.SelectedValue = String.Empty
                    End If

                Else
                    With Me.ddlFiltroEstado
                        .DataSource = Nothing
                        .DataBind()
                        .Visible = False
                    End With
                    MostrarAlertaNoHayDatos()
                End If
            Else
                MostrarAlertaNoHayDatos()
                Me.ddlViaCompra.Enabled = False
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If

            If vlo_DsDatos IsNot Nothing Then
                vlo_DsDatos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvc_Condicion">parámetro con la condicion de búsqueda</param>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                Select Case ddlViaCompra.SelectedValue
                    'Suministros
                    Case Me.ViaCompraSuministros
                        With Me.RpSuministros
                            .DataSource = vlo_DsDatos
                            .DataMember = vlo_DsDatos.Tables(0).TableName
                            .DataBind()
                            WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                        End With
                        Me.RpSuministros.Visible = True
                        Me.rpFondoTrabajo.Visible = False
                        Me.RpUnidadCompra.Visible = False

                        'Unidad Especializada de Compras
                    Case Me.ViaCompraUnidadEspec
                        With Me.RpUnidadCompra
                            .DataSource = vlo_DsDatos
                            .DataMember = vlo_DsDatos.Tables(0).TableName
                            .DataBind()
                            WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                        End With
                        Me.RpSuministros.Visible = False
                        Me.rpFondoTrabajo.Visible = False
                        Me.RpUnidadCompra.Visible = True

                        'Fondo de Trabajo
                    Case Me.ViaCompraFondoTrabajo
                        With Me.rpFondoTrabajo
                            .DataSource = vlo_DsDatos
                            .DataMember = vlo_DsDatos.Tables(0).TableName
                            .DataBind()
                            WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                        End With
                        Me.RpSuministros.Visible = False
                        Me.rpFondoTrabajo.Visible = True
                        Me.RpUnidadCompra.Visible = False
                End Select
            Else
                With Me.rpFondoTrabajo
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With

                With Me.RpSuministros
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With

                With Me.RpUnidadCompra
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "ocultarAreaDeListado(); mostrarAreaFiltrosDeBusqueda();")
                Me.RpSuministros.Visible = False
                Me.rpFondoTrabajo.Visible = False
                Me.RpUnidadCompra.Visible = False
                MostrarAlertaNoHayDatos()
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If

            If vlo_DsDatos IsNot Nothing Then
                vlo_DsDatos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de realizar la busqueda de registros segun los datos de 
    ''' condicion y orden de búsqueda
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerExpresionDeOrdenamiento(Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION)
        End If

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials



        Try

            vlo_EntDatosPaginacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_ObtenerDatosPaginacionVOttGestionCompralst(
                         ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                         ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                         pvc_Condicion,
                         pvc_Orden,
                         CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Select Case ddlViaCompra.SelectedValue
                    'Suministros
                    Case Me.ViaCompraSuministros
                        Me.pnRpSuministros.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                        Me.pnRpSuministros.Dibujar()
                        Me.lblCantidadDeRegistrosSM.Visible = True
                        Me.lblCantidadDeRegistrosSM.Text = String.Format("Cantidad de Gestiones {0}", vlo_EntDatosPaginacion.TotalRegistros)

                        'Unidad Especializada de Compras
                    Case Me.ViaCompraUnidadEspec
                        Me.pnRpUnidadCompra.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                        Me.pnRpUnidadCompra.Dibujar()
                        Me.lblCantidadDeRegistrosUC.Visible = True
                        Me.lblCantidadDeRegistrosUC.Text = String.Format("Cantidad de Gestiones {0}", vlo_EntDatosPaginacion.TotalRegistros)

                        'Fondo de Trabajo
                    Case Me.ViaCompraFondoTrabajo
                        Me.pnRpFondoTrabajo.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                        Me.pnRpFondoTrabajo.Dibujar()
                        Me.lblCantidadDeRegistrosFT.Visible = True
                        Me.lblCantidadDeRegistrosFT.Text = String.Format("Cantidad de Gestiones {0}", vlo_EntDatosPaginacion.TotalRegistros)
                End Select
                CargarLista(pvc_Condicion, pvc_Orden, 1)
            Else
                Me.lblCantidadDeRegistrosFT.Visible = False
                Me.lblCantidadDeRegistrosFT.Text = String.Empty

                Me.lblCantidadDeRegistrosSM.Visible = False
                Me.lblCantidadDeRegistrosSM.Text = String.Empty

                Me.lblCantidadDeRegistrosUC.Visible = False
                Me.lblCantidadDeRegistrosUC.Text = String.Empty
                MostrarAlertaNoHayDatos()
                Me.RpSuministros.Visible = False
                Me.rpFondoTrabajo.Visible = False
                Me.RpUnidadCompra.Visible = False
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If

            If vlo_DsDatos IsNot Nothing Then
                vlo_DsDatos.Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"
    ''' <summary>
    ''' arma la condicion de busqueda
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String
        vlc_Condicion = String.Empty

        vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OTT_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO, Me.ddlViaCompra.SelectedValue)

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroNumeroGestion.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("UPPER({0}) like '%{1}%'", Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION_COMPRA, Me.txtFiltroNumeroGestion.Text.Trim.ToUpper)
            Else
                vlc_Condicion = String.Format("{0} AND UPPER({1}) like '%{2}%'", vlc_Condicion, Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION_COMPRA, Me.txtFiltroNumeroGestion.Text.Trim.ToUpper)
            End If
        End If


        If (Not String.IsNullOrWhiteSpace(Me.txtFiltroFechaInicio.Text)) And (Not String.IsNullOrWhiteSpace(Me.txtFiltroFechaFin.Text)) Then
            If Not String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} AND TRUNC({1}) >= TO_DATE('{2}','DD/MM/YYYY')  AND TRUNC({1}) <= TO_DATE('{3}','DD/MM/YYYY') ", vlc_Condicion, Modelo.V_OTT_GESTION_COMPRALST.FECHA_REGISTRO_SOLICITUD, Me.txtFiltroFechaInicio.Text, Me.txtFiltroFechaFin.Text)
            Else
                vlc_Condicion = String.Format("TRUNC({0}) >= TO_DATE('{1}','DD/MM/YYYY')  AND  TRUNC({0}) <=  TO_DATE('{2}','DD/MM/YYYY') ", Modelo.V_OTT_GESTION_COMPRALST.FECHA_REGISTRO_SOLICITUD, Me.txtFiltroFechaInicio.Text, Me.txtFiltroFechaFin.Text)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroEstado.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTT_GESTION_COMPRALST.ESTADO, Me.ddlFiltroEstado.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTT_GESTION_COMPRALST.ESTADO, Me.ddlFiltroEstado.SelectedValue)
            End If
        End If

        Return vlc_Condicion
    End Function

    ''' <summary>
    ''' funcion encargada de realizar los oredenamientos segun los valores brindados
    ''' </summary>
    ''' <param name="pvc_Columna"> numero de la columna para el ordenamiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerExpresionDeOrdenamiento(pvc_Columna As String) As String
        If String.IsNullOrWhiteSpace(UltimoSortColumn) OrElse pvc_Columna.CompareTo(UltimoSortColumn) <> 0 Then
            UltimoSortColumn = pvc_Columna
            UltimoSortDirection = SortDirection.Ascending
        Else
            If UltimoSortDirection = SortDirection.Ascending Then
                UltimoSortDirection = SortDirection.Descending
            Else
                UltimoSortDirection = SortDirection.Ascending
            End If
        End If

        UltimoSortExpression = String.Format("{0} {1}", UltimoSortColumn, IIf(UltimoSortDirection = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))
        Return UltimoSortExpression
    End Function

    ''' <summary>
    ''' Inicializa la condicion para la consulta a la vista
    ''' </summary>
    ''' <author>Luis Murillo</author>
    ''' <creation>06/05/2015</creation>
    ''' <remarks></remarks>
    Private Sub ValidarTipoDeRol()
        Try

            If Roles.IsUserInRole(Membership.GetUser.UserName, RolesSistema.OT_GESTOR_DE_INVENTARIO) Then
                Me.AccesoConcedido = True
            Else
                Me.AccesoConcedido = False
                WebUtils.RegistrarScript(Me, "alertaError", "mostrarAlertaSinRol();")
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' carga la ubicacion favorita
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>13/10/2015</creationDate>
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

    Private Function CargarParametro(pvn_IdParametro As Integer) As Integer
        Dim Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtpParametroUbicacion As EntOtpParametroUbicacion
        Dim vlc_IdViaCompraContrato As Integer

        Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        Ws_OT_Catalogos.Timeout = -1
        Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntOtpParametroUbicacion = Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, pvn_IdParametro))

            vlc_IdViaCompraContrato = vlo_EntOtpParametroUbicacion.Valor

            Return vlc_IdViaCompraContrato

        Catch ex As Exception
            Throw
        Finally
            If Ws_OT_Catalogos IsNot Nothing Then
                Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

#End Region
End Class
