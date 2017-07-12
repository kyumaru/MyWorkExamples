Imports Utilerias.OrdenesDeTrabajo
Imports System.Data
Imports Wsr_OT_Catalogos

Partial Class OrdenesDeTrabajo_Almacen_Lst_OT_TrasladoDeMaterialABodega
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

    Public Property EsCordinador As Boolean
        Get
            Return CType(ViewState("EsCordinador"), Boolean)
        End Get
        Set(ByVal value As Boolean)
            ViewState("EsCordinador") = value
        End Set
    End Property

    Public Property TieneBodegas As Boolean
        Get
            Return CType(ViewState("TieneBodegas"), Boolean)
        End Get
        Set(ByVal value As Boolean)
            ViewState("TieneBodegas") = value
        End Set
    End Property

    Public Property IdBodega As Integer
        Get
            Return CType(ViewState("IdBodega"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("IdBodega") = value
        End Set
    End Property

    Public Property NombreBodega As String
        Get
            Return CType(ViewState("NombreBodega"), String)
        End Get
        Set(ByVal value As String)
            ViewState("NombreBodega") = value
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
        Me.pnRpGesionSolicitud.Dibujar()
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
    Protected Sub lnkRpProveedores_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerCondicionBusqueda, ObtenerExpresionDeOrdenamiento(e.CommandName), Me.pnRpGesionSolicitud.PaginaActualLista)
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
    Protected Sub pnRpProveedores_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpGesionSolicitud.CambioDePagina
        Try
            CargarLista(ObtenerCondicionBusqueda, UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al carar el repeater del listado, asigna atributos a cada elemento del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpGestionSolicitud_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpGestionSolicitud.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        Dim vlo_IbGestion As ImageButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibGestion") IsNot Nothing Then
                vlo_IbGestion = CType(e.Item.FindControl("ibGestion"), ImageButton)
                vlo_IbGestion.Attributes.Add("data-uniqueid", vlo_IbGestion.UniqueID)
            End If

            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar, ubicada en la 
    ''' segunda columna de cada registro del listado, llama a la función borrar y muestra  un mensaje según 
    ''' la operacion efectuada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibGestionSolicitud_Click(sender As Object, e As ImageClickEventArgs)

        Dim vlc_Llave As String()
        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdSolicitudTraslado", vlc_Llave(1))
            Me.Session.Add("pvn_Anno", vlc_Llave(2))
            Me.Session.Add("pvn_IdBodega", vlc_Llave(3))
            Me.Session.Add("pvc_IdEstado", vlc_Llave(4))
            Me.Session.Add("pvc_NombreAlmacen", NombreBodega)
            Me.Session.Add("pvn_Operacion", eOperacion.Modificar)
            Response.Redirect(String.Format("Frm_OT_TrasladoDeMaterialABodega.aspx"), False)

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
            Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub ddlBodega_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBodega.SelectedIndexChanged
        Me.NombreBodega = Me.ddlBodega.SelectedItem.Text
        Me.IdBodega = Me.ddlBodega.SelectedValue
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar, ubicada en la 
    ''' segunda columna de cada registro del listado, llama a la función borrar y muestra  un mensaje según 
    ''' la operacion efectuada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()
        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            If Borrar(vlc_Llave(1), vlc_Llave(2), vlc_Llave(0), vlc_Llave(3)) Then
                MostrarAlertaRegistroBorrado()
                Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)
            Else
                MostrarAlertaRegistroNoBorrado()
            End If
        Catch ex As Exception
            If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                Dim vlo_OrdenesDeTrabajoException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                MostrarAlertaError(vlo_OrdenesDeTrabajoException.Message)
            Else
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End If
        End Try
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaRegistroBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroBorrado", "mostrarAlertaRegistroBorrado();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaRegistroNoBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroNoBorrado", "mostrarAlertaRegistroNoBorrado();")
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
    ''' carga el combo de estados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEstado()
        Me.ddlFiltroEstado.Items.Clear()
        Me.ddlFiltroEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
        Me.ddlFiltroEstado.Items.Add(New ListItem(EstadoTraslado.CREADA_STR, EstadoTraslado.CREADA))
        Me.ddlFiltroEstado.Items.Add(New ListItem(EstadoTraslado.PENDIENTE_APROBACION_STR, EstadoTraslado.PENDIENTE_APROBACION))
        Me.ddlFiltroEstado.Items.Add(New ListItem(EstadoTraslado.APROBADA_STR, EstadoTraslado.APROBADA))
        Me.ddlFiltroEstado.Items.Add(New ListItem(EstadoTraslado.PREPARACION_MATERIAL_STR, EstadoTraslado.PREPARACION_MATERIAL))
        Me.ddlFiltroEstado.Items.Add(New ListItem(EstadoTraslado.SOLICITUD_RETIRO_STR, EstadoTraslado.SOLICITUD_RETIRO))
        Me.ddlFiltroEstado.Items.Add(New ListItem(EstadoTraslado.SOLICITUD_ENTREGA_STR, EstadoTraslado.SOLICITUD_ENTREGA))
        Me.ddlFiltroEstado.Items.Add(New ListItem(EstadoTraslado.DEVUELTA_STR, EstadoTraslado.DEVUELTA))

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
                CargarBodega()
                If TieneBodegas = True Then
                    Me.ddlBodega.Enabled = True
                    CargarEstado()
                    Buscar(Me.ObtenerCondicionBusqueda, Me.UltimoSortExpression)
                Else
                    WebUtils.RegistrarScript(Me, "alertaError3", String.Format("javascript:mostrarAlertaMensajeRedirecionar('{0}');", "Actualmente no hay bodegas disponibles"))
                    Me.ddlBodega.Enabled = False
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "ocultarAreaDeListado(); mostrarAreaFiltrosDeBusqueda();")
                End If
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted debe de indicar la sede en la cual presentará las ordenes de trabajo.','Frm_OT_SelecciónSedeTrabajo.aspx');")
            End If
        Else
            Me.lblCantidadDeRegistros.Visible = False
            Me.lblCantidadDeRegistros.Text = String.Empty
            Me.rpGestionSolicitud.Visible = False
            WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee el rol necesario para ingresar a esta página.','../../Genericos/Frm_MenuPrincipal.aspx');")
        End If
    End Sub

    '' <summary>
    ''' Carga el combo de Periodos
    ''' </summary>
    ''' <author>Junior Castillo</author>
    ''' <creationDate>10/05/2015</creationDate>
    ''' <remarks></remarks>
    Private Sub CargarBodega()
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsBodega As DataSet

        Dim vlc_Condicion As String = String.Empty
        Dim vlc_Orden As String = String.Empty

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_Ws_OT_Catalogos.Timeout = -1

        Try

            If Me.EsCordinador Then
                vlc_Condicion = String.Format("{0} = '{1}' AND {2} = {3} AND {4} = 1", Modelo.V_OTM_ALMACEN_BODEGALST.ESTADO, Estado.ACTIVO, Modelo.V_OTM_ALMACEN_BODEGALST.COORDINADOR_SECTOR_TALLER, Usuario.NumEmpleado, Modelo.V_OTM_ALMACEN_BODEGALST.PERTENECE_INVENTARIO)




                vlc_Orden = String.Format("{0} {1}", Modelo.V_OTM_ALMACEN_BODEGALST.DESCRIPCION, Ordenamiento.ASCENDENTE)

                vlo_DsBodega = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ListarRegistrosLista(System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB).ToString,
                                                                                                        System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB).ToString,
                                                                                                        vlc_Condicion,
                                                                                                        vlc_Orden,
                                                                                                        False,
                                                                                                        0,
                                                                                                        0)
                Me.ddlBodega.Items.Clear()
                If vlo_DsBodega.Tables(0) IsNot Nothing AndAlso vlo_DsBodega.Tables(0).Rows.Count > 0 Then
                    With Me.ddlBodega
                        .DataSource = vlo_DsBodega
                        .DataTextField = Modelo.V_OTM_ALMACEN_BODEGALST.DESCRIPCION
                        .DataValueField = Modelo.V_OTM_ALMACEN_BODEGALST.ID_ALMACEN_BODEGA
                        .DataBind()
                    End With
                    ddlBodega.SelectedIndex = 0
                    IdBodega = CType(ddlBodega.SelectedValue, Integer)
                    NombreBodega = ddlBodega.SelectedItem.Text
                    TieneBodegas = True
                Else
                    TieneBodegas = False
                End If
            End If
        Catch ex As Exception
            Throw
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
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTT_SOLICITUD_TRASLADO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpGestionSolicitud
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End With
            Else
                With Me.rpGestionSolicitud
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With
                MostrarAlertaNoHayDatos()
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
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_Catalogos.EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerExpresionDeOrdenamiento(Modelo.V_OTT_SOLICITUD_TRASLADOLST.NUMERO_SOLICITUD)
        End If

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntDatosPaginacion = vlo_Ws_OT_Catalogos.OTT_SOLICITUD_TRASLADO_ObtenerDatosPaginacionVOttSolicitudTrasladolst(
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                     pvc_Condicion,
                     pvc_Orden,
                     CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpGesionSolicitud.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                CargarLista(pvc_Condicion, pvc_Orden, 1)
                Me.pnRpGesionSolicitud.Dibujar()
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de Solicitudes {0}", vlo_EntDatosPaginacion.TotalRegistros)
            Else
                Me.lblCantidadDeRegistros.Visible = False
                Me.lblCantidadDeRegistros.Text = String.Empty
                MostrarAlertaNoHayDatos()
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

        vlc_Condicion = String.Format("UPPER({0}) = {1}", Modelo.V_OTT_SOLICITUD_TRASLADOLST.ID_BODEGA, Me.ddlBodega.SelectedValue)
        IdBodega = CType(Me.ddlBodega.SelectedValue, Integer)
        If Not String.IsNullOrWhiteSpace(Me.txtFiltroSolicitud.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("UPPER({0}) like '%{1}%'", Modelo.V_OTT_SOLICITUD_TRASLADOLST.NUMERO_SOLICITUD, Me.txtFiltroSolicitud.Text.Trim.ToUpper)
            Else
                vlc_Condicion = String.Format("{0} AND UPPER({1}) like '%{2}%'", vlc_Condicion, Modelo.V_OTT_SOLICITUD_TRASLADOLST.NUMERO_SOLICITUD, Me.txtFiltroSolicitud.Text.Trim.ToUpper)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroEstado.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTT_SOLICITUD_TRASLADOLST.ESTADO_TRASLADO, Me.ddlFiltroEstado.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTT_SOLICITUD_TRASLADOLST.ESTADO_TRASLADO, Me.ddlFiltroEstado.SelectedValue)
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

            If Roles.IsUserInRole(Membership.GetUser.UserName, RolesSistema.OT_COORDINADOR_MANTENIMIENTO) Then
                Me.AccesoConcedido = True
                Me.EsCordinador = True
            Else
                Me.AccesoConcedido = False
                Me.EsCordinador = False
                WebUtils.RegistrarScript(Me, "alertaError", "mostrarAlertaSinRoleTramitadorOAF();")
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

    '' <summary>
    ''' Función encargada de comunicarse con el  servicio web y proceder a borrar el registro
    ''' </summary>
    ''' <param name="pvn_Identificacion"></param>
    ''' <returns>Si retorna un número mayor a 0 quiere decir que la operacion se realizo con éxito</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Borrar(pvn_IdSolicitud As Integer, pvn_IdAnio As Integer, pvn_IdUbicacion As Integer, pvn_IdAlmacen As Integer) As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOttSolicitudTraslado As Wsr_OT_Catalogos.EntOttSolicitudTraslado

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOttSolicitudTraslado = New Wsr_OT_Catalogos.EntOttSolicitudTraslado
        vlo_EntOttSolicitudTraslado.IdSolicitudTraslado = pvn_IdSolicitud
        vlo_EntOttSolicitudTraslado.Anno = pvn_IdAnio
        vlo_EntOttSolicitudTraslado.IdUbicacion = pvn_IdUbicacion
        vlo_EntOttSolicitudTraslado.IdAlmacen = pvn_IdAlmacen

        Try
            Return vlo_Ws_OT_Catalogos.OTT_SOLICITUD_TRASLADO_BorrarRegistrosConjunto(
          ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
          ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
          vlo_EntOttSolicitudTraslado) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function
#End Region


End Class
