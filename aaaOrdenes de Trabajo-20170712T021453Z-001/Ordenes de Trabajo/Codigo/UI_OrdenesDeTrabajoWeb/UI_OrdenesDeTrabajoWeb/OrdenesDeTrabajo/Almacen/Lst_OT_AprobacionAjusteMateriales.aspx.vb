Imports Utilerias.OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Almacen_Lst_OT_AprobacionAjusteMateriales
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la ultima condicion de busqueda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimaCondicionBusqueda As String
        Get
            If ViewState("UltimaCondicionBusqueda") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimaCondicionBusqueda"), String)
        End Get
        Set(value As String)
            ViewState("UltimaCondicionBusqueda") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
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
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
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
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
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
    ''' usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
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
    ''' propiedad para el sector taller a trabajar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property SectorTaller As Wsr_OT_Catalogos.EntOtmSectorTaller
        Get
            Return CType(ViewState("SectorTaller"), Wsr_OT_Catalogos.EntOtmSectorTaller)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmSectorTaller)
            ViewState("SectorTaller") = value
        End Set
    End Property

    ''' <summary>
    ''' ubicacion favorita del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UbicacionAutorizada As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Get
            Return CType(ViewState("UbicacionAutorizada"), Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
            ViewState("UbicacionAutorizada") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el costo total de la orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/7/2016</creationDate>
    Public Property CostoTotalOT As Integer
        Get
            If ViewState("CostoTotalOT") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("CostoTotalOT"), Integer)
        End Get
        Set(value As Integer)
            ViewState("CostoTotalOT") = value
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
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                InicializarListado()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
        Me.pnRpOrdenTrabajo.Dibujar()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el botón Buscar,
    ''' que se encuentra en el área de filtros.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento para mostrar información de la OT
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkNumOt_Click(sender As Object, e As EventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, LinkButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_Operacion", eOperacion.Consultar)
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Regresar", "../Almacen/Lst_OT_AprobacionAjusteMateriales.aspx")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect(String.Format("../OrdenesDeTrabajo/Frm_OT_OrdenTrabajo.aspx"), False)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados de la 
    ''' tabla del listado, carga la lista de registros en orden relacionado a la columna seleccionada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpOrdenTrabajo_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerCondicionBusqueda, ObtenerExpresionDeOrdenamiento(e.CommandName), pnRpOrdenTrabajo.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Redirige a otra página para registrar el análisis de la viabilidad técnica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnRequisicion_Click(sender As Object, e As EventArgs)
        Dim vlc_Llave As String()
        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_Anno", vlc_Llave(2))
            Me.Session.Add("pvn_IdSectorTaller", vlc_Llave(3))
            Response.Redirect(String.Format("Frm_OT_Requisicion.aspx"), False)

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
    ''' <author>César Bermúdez</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnRevisionRequisicion_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()
        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_Anno", vlc_Llave(2))

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect(String.Format("Frm_OT_AprobacionAjusteMateriales.aspx"), False)
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inicializa el listado y envia a buscar con los valores especificados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarListado()
        Try
            If Roles.IsUserInRole(Membership.GetUser.UserName, RolesSistema.OT_REVISOR_REQUISICIONES) Then
                Me.Usuario = New UsuarioActual
                Me.UbicacionAutorizada = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If Me.UbicacionAutorizada.Existe Then
                    CargarCostoTotalOrden()
                    Buscar(ObtenerCondicionBusqueda, String.Empty)
                    CargarListaTalleres()
                    CargarComboTipoOrden()
                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ninguna ubicación autorizada, contactar con el administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee el rol necesario para ingresar a est página.','../../Genericos/Frm_MenuPrincipal.aspx');")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboTipoOrden()
        Me.ddlTipoOrden.Items.Clear()
        Me.ddlTipoOrden.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
        Me.ddlTipoOrden.Items.Add(New ListItem("Ordinaria", TipoOrden.ORDINARIA))
        Me.ddlTipoOrden.Items.Add(New ListItem("Emergencia", TipoOrden.EMERGENCIA))
        Me.ddlTipoOrden.Items.Add(New ListItem("Preventiva", TipoOrden.PREVENTIVO))
    End Sub

    ''' <summary>
    ''' Método encargado de realizar la busqueda de registros segun los datos de 
    ''' condicion y orden de búsqueda
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntDatosPaginacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerDatosPaginacionVOttOrdenTrabajolst(
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                     pvc_Condicion,
                     pvc_Orden,
                     CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpOrdenTrabajo.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas

                CargarLista(pvc_Condicion, pvc_Orden, 1)

                Me.pnRpOrdenTrabajo.Dibujar()
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de ordenes {0}", vlo_EntDatosPaginacion.TotalRegistros)
                Me.rpOrdenTrabajo.Visible = True
                Me.pnRpOrdenTrabajo.Visible = True
            Else
                Me.lblCantidadDeRegistros.Visible = False
                Me.lblCantidadDeRegistros.Text = String.Empty
                Me.rpOrdenTrabajo.Visible = False
                Me.pnRpOrdenTrabajo.Visible = False
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
    ''' Ejecuta eljavascript para mostrar una alerta cuando no existen datos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "mostrarAlertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    ''' <summary>
    ''' Ejecuta un llamado para obtener estados de orden de trabajo desde la base de datos y cargar la lista de filtros con ella.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaTalleres()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlfiltroTallerSector.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
            '{0}: Columna ESTADO
            '{1}: sectores y talleres activos

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_SECTOR_TALLER_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}'", Modelo.OTM_SECTOR_TALLER.ESTADO, Estado.ACTIVO), String.Empty, False, 1, 0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlfiltroTallerSector
                    .DataSource = vlo_DsDatos
                    .DataValueField = Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER
                    .DataTextField = Modelo.OTM_SECTOR_TALLER.NOMBRE
                    .DataBind()
                End With
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
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpOrdenTrabajo
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End With
                Me.rpOrdenTrabajo.Visible = True
                Me.pnRpOrdenTrabajo.Visible = True
            Else
                With Me.rpOrdenTrabajo
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With
                Me.rpOrdenTrabajo.Visible = False
                Me.pnRpOrdenTrabajo.Visible = False
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
    ''' Carga em la session el usuario encargado de este proyecto si existe
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEncargado(pvn_idUbicacion As Integer, pvc_idOrdenTrabajo As String, pvn_idSectorTaller As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttOperarioOrdenTrab As Wsr_OT_OrdenesDeTrabajo.EntOttOperarioOrdenTrab

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0}: Columna ID_UBICACION
            '{1}: numero de la ubicacion de la OT
            '{2}: Columna ID_SECTOR_TALLER
            '{3}: numero de sector taller de la OT
            '{4}: Columna ID_ORDEN_TRABAJO
            '{5}: id de la orden de trabajo
            '{6}: Columnna CARGO
            '{7}: cargo de encargado

            vlo_EntOttOperarioOrdenTrab = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}' AND {6} = '{7}'",
                              Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, pvn_idUbicacion,
                              Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER, pvn_idSectorTaller,
                              Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo,
                              Modelo.OTT_OPERARIO_ORDEN_TRAB.CARGO, Cargo.ENCARGADO))

            If vlo_EntOttOperarioOrdenTrab.Existe Then
                Me.Session.Add("pvn_IdEncargado", vlo_EntOttOperarioOrdenTrab.NumEmpleado)
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga el costo total de la orden de trabajo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarCostoTotalOrden()
        Dim vlo_EntOtpParametroUbicacion As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtpParametroUbicacion = vlo_Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.MAXIMO_EJECUCION_OBRAS))

            CostoTotalOT = vlo_EntOtpParametroUbicacion.Valor

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub
#End Region

#Region "Funciones"

    ''' <summary>
    ''' Carga una entidad de tipo autorizado ubicacion
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
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

    ''' <summary>
    ''' construye la condicion de busqueda
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroNumOrden.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO, Me.txtFiltroNumOrden.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO, Me.txtFiltroNumOrden.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlTipoOrden.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTT_ORDEN_TRABAJOLST.TIPO_ORDEN_TRABAJO, Me.ddlTipoOrden.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTT_ORDEN_TRABAJOLST.TIPO_ORDEN_TRABAJO, Me.ddlTipoOrden.SelectedValue)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlfiltroTallerSector.SelectedValue) Then
            If Not String.IsNullOrWhiteSpace(Me.ddlfiltroTallerSector.SelectedValue) Then
                vlc_Condicion = String.Format("{0} = '{1}' ", Modelo.V_OTT_ORDEN_TRABAJOLST.ID_SECTOR_TALLER, Me.ddlfiltroTallerSector.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTT_ORDEN_TRABAJOLST.ID_SECTOR_TALLER, Me.ddlfiltroTallerSector.SelectedValue)
            End If
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} = {1} ", Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION, Me.UbicacionAutorizada.IdUbicacionAdministra)
        Else
            vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION, Me.UbicacionAutorizada.IdUbicacionAdministra)
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("({0} = '{1}' OR {0} = '{2}' OR {0} = '{3}') AND {4} > 0", Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO, EstadoOrden.PARA_RETIRO_MATERIAL, EstadoOrden.MATERIAL_PENDIENTE_COMPRA, EstadoOrden.EN_PROCESO, Modelo.V_OTT_ORDEN_TRABAJOLST.POSEE_DETALLE_SOLICITUD)
        Else
            vlc_Condicion = String.Format("{0} AND ({1} = '{2}' OR {1} = '{3}' OR {1} = '{4}') AND {5} > 0", vlc_Condicion, Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO, EstadoOrden.PARA_RETIRO_MATERIAL, EstadoOrden.MATERIAL_PENDIENTE_COMPRA, EstadoOrden.EN_PROCESO, Modelo.V_OTT_ORDEN_TRABAJOLST.POSEE_DETALLE_SOLICITUD)
        End If

        If UltimaCondicionBusqueda = String.Empty Then
            UltimaCondicionBusqueda = vlc_Condicion
        Else
            Return UltimaCondicionBusqueda
        End If

        Return vlc_Condicion
    End Function

    ''' <summary>
    ''' funcion encargada de realizar los oredenamientos segun los valores brindados
    ''' </summary>
    ''' <param name="pvc_Columna"> numero de la columna para el ordenamiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
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

#End Region

End Class
