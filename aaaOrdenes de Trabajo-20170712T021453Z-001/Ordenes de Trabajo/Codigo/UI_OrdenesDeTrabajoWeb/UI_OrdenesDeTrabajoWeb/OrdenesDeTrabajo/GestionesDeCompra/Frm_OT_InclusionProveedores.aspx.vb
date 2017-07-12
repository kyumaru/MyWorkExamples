Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_InclusionProveedores
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property PantallaRetorno As String
        Get
            Return CType(ViewState("PantallaRetorno"), String)
        End Get
        Set(value As String)
            ViewState("PantallaRetorno") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
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
    ''' <creationDate>31/08/2016</creationDate>
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
    ''' <creationDate>31/08/2016</creationDate>
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
    ''' data set para materiales iniciales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsProveedorCotizacion As Data.DataSet
        Get
            Return CType(ViewState("DsProveedorCotizacion"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsProveedorCotizacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la gestión de compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property GestionCompra As EntOttGestionCompra
        Get
            Return CType(ViewState("GestionCompra"), EntOttGestionCompra)
        End Get
        Set(value As EntOttGestionCompra)
            ViewState("GestionCompra") = value
        End Set
    End Property

    ''' <summary>
    ''' usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
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
    ''' ubicacion del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property AutorizadoUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Get
            Return CType(ViewState("AutorizadoUbicacion"), Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
            ViewState("AutorizadoUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdUbicacion As Integer
        Get
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdViaCompraContrato As Integer
        Get
            Return CType(ViewState("IdViaCompraContrato"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdViaCompraContrato") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Annio As Integer
        Get
            Return CType(ViewState("Annio"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Annio") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property NumeroGestion As Integer
        Get
            Return CType(ViewState("NumeroGestion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("NumeroGestion") = value
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
    ''' <creationDate>31/08/2016</creationDate>
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
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados de la tabla del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpProveedoresCotizacion_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerCondicionBusqueda(), ObtenerExpresionDeOrdenamiento(e.CommandName))
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click al avento del listado de  borrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            If BorrarProveedor(CType(sender, ImageButton).CommandArgument) Then
                CargarLista(ObtenerCondicionBusqueda(), UltimoSortExpression)
            Else
                MostrarAlertaError("No ha sido posible borrar el registro.")
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

    ''' <summary>
    ''' evento que se ejecuta cuando se da click al avento del listado de enviar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibEnviar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            If EnviarProveedor(CType(sender, ImageButton).CommandArgument) Then
                CargarLista(ObtenerCondicionBusqueda(), UltimoSortExpression)
            Else
                MostrarAlertaError("No ha sido posible enviar la cotización.")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click sobre el boton de ejecutar busqueda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkEjecutarBusqueda_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusqueda.Click
        Try
            CargarComboProveedores(String.Format("{0} = '{1}' AND UPPER({2}) LIKE '%{3}%'", Modelo.OTM_PROVEEDOR.ESTADO, Estado.ACTIVO, Modelo.OTM_PROVEEDOR.NOMBRE, Me.txtFiltroDescripcion.Text.ToUpper))
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click sobre el boton de agregar proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try

            If Me.DsProveedorCotizacion.Tables(0).Rows.Find(New Object() {Me.ddlProveedores.SelectedValue}) Is Nothing Then
                If AgregarProveedor() Then
                    CargarLista(ObtenerCondicionBusqueda(), UltimoSortExpression)
                    Me.ddlProveedores.SelectedValue = String.Empty
                Else
                    MostrarAlertaError("No ha sido posible agregar el registro.")
                End If
            Else
                MostrarAlertaError("El registro ya existe para la gestión de compra.")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' limpia el filtro de proveedores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnLimpiarFiltro_Click(sender As Object, e As EventArgs) Handles btnLimpiarFiltro.Click
        Try
            Me.txtFiltroDescripcion.Text = String.Empty
            CargarComboProveedores(String.Format("{0} = '{1}'", Modelo.OTM_PROVEEDOR.ESTADO, Estado.ACTIVO))
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' redirecciona la pantalla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            Me.Session.Add("pvn_IdUbicacion", Me.GestionCompra.IdUbicacion)
            Me.Session.Add("pvn_IdViaCompraContrato", Me.GestionCompra.IdViaCompraContrato)
            Me.Session.Add("pvn_Annio", Me.GestionCompra.Anno)
            Me.Session.Add("pvn_NumeroGestion", Me.GestionCompra.NumeroGestion)
        Catch ex As Exception
            Throw
        End Try
        Response.Redirect(Me.PantallaRetorno, False)
    End Sub

    ''' <summary>
    ''' procesa la inclusion de proveedores 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnSolicitarCotizacion_Click(sender As Object, e As EventArgs) Handles btnSolicitarCotizacion.Click
        Try
            If Me.DsProveedorCotizacion IsNot Nothing AndAlso Me.DsProveedorCotizacion.Tables(0).Rows.Count > 0 Then
                If ProcesarInclusion() Then
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                Else
                    MostrarAlertaError("No ha sido actualizar la información.")
                End If
            Else
                MostrarAlertaError("Debe de incluir al menos un proveedor.")
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
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' inicializa componentes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarListado()
        Try

            If Roles.IsUserInRole(Membership.GetUser.UserName, RolesSistema.OT_GESTOR_DE_INVENTARIO) Then

                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If Me.AutorizadoUbicacion.Existe Then

                    LeerParametrosSession()
                    CargarGestionCompra()

                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ninguna ubicación autorizada, contactar con el administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee el rol necesario para ingresar a esta página.','../../Genericos/Frm_MenuPrincipal.aspx');")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' lee y carga los parametros guardados en sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametrosSession()
        Try

            Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
            Me.IdViaCompraContrato = WebUtils.LeerParametro(Of Integer)("pvn_IdViaCompraContrato")
            Me.Annio = WebUtils.LeerParametro(Of Integer)("pvn_Annio")
            Me.NumeroGestion = WebUtils.LeerParametro(Of Integer)("pvn_NumeroGestion")
            Me.PantallaRetorno = WebUtils.LeerParametro(Of String)("pvc_PantallaRetorno")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' carga la gestion, segun parametros obtenidos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarGestionCompra()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.GestionCompra = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_UBICACION, Me.IdUbicacion, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ANNO, Me.Annio, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_GESTION, Me.NumeroGestion))

            If Me.GestionCompra.Existe Then
                CargarLista(ObtenerCondicionBusqueda(), String.Empty)
                CargarComboProveedores(String.Format("{0} = '{1}'", Modelo.OTM_PROVEEDOR.ESTADO, Estado.ACTIVO))
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('El identificador provisto no pertenece a ningun registro del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
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
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvc_Condicion">parámetro con la condicion de búsqueda</param>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsProveedorCotizacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_PROVEEDOR_COTIZACION_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

            Me.DsProveedorCotizacion.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsProveedorCotizacion.Tables(0).Columns(Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.IDENTIFICACION)}

            If Me.DsProveedorCotizacion IsNot Nothing AndAlso Me.DsProveedorCotizacion.Tables(0).Rows.Count > 0 Then
                With Me.rpProveedoresCotizacion
                    .DataSource = Me.DsProveedorCotizacion
                    .DataMember = Me.DsProveedorCotizacion.Tables(0).TableName
                    .DataBind()
                End With
                Me.btnSolicitarCotizacion.Visible = True
            Else
                Me.btnSolicitarCotizacion.Visible = False
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
    ''' carga al combo de seleccion de proveedores
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboProveedores(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlProveedores.Items.Clear()
            Me.ddlProveedores.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_PROVEEDOR_ListarRegistrosLista(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               pvc_Condicion,
               String.Empty,
               False,
               0,
               0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlProveedores
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_PROVEEDOR.NOMBRE
                    .DataValueField = Modelo.OTM_PROVEEDOR.IDENTIFICACION
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

#End Region

#Region "Funciones"

    ''' <summary>
    ''' realiza el proceso de solicitud general de la cotizacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ProcesarInclusion() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_PROVEEDOR_COTIZACION_SolicitudCotizacion(
             ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
             ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
             Me.Usuario.UserName, Me.GestionCompra) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' cantruya la entidad a insertar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As EntOttProveedorCotizacion
        Dim vlo_EntOttProveedorCotizacion As EntOttProveedorCotizacion

        Try

            vlo_EntOttProveedorCotizacion = New EntOttProveedorCotizacion

            vlo_EntOttProveedorCotizacion.Identificacion = Me.ddlProveedores.SelectedValue
            vlo_EntOttProveedorCotizacion.IdUbicacion = Me.IdUbicacion
            vlo_EntOttProveedorCotizacion.IdViaCompraContrato = Me.IdViaCompraContrato
            vlo_EntOttProveedorCotizacion.Anno = Me.Annio
            vlo_EntOttProveedorCotizacion.NumeroGestion = Me.NumeroGestion
            vlo_EntOttProveedorCotizacion.Estado = EstadoProveedorCotizacion.PENDIENTE_DE_ENVIO
            vlo_EntOttProveedorCotizacion.Usuario = Me.Usuario.UserName

            Return vlo_EntOttProveedorCotizacion

        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function AgregarProveedor() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttProveedorCotizacion As EntOttProveedorCotizacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttProveedorCotizacion = ConstruirRegistro()

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_PROVEEDOR_COTIZACION_InsertarRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               vlo_EntOttProveedorCotizacion) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' borrar el registro de cotizacion proveedor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function BorrarProveedor(pvc_Identificacion As String) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttProveedorCotizacion As EntOttProveedorCotizacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttProveedorCotizacion = New EntOttProveedorCotizacion
            vlo_EntOttProveedorCotizacion.Identificacion = pvc_Identificacion
            vlo_EntOttProveedorCotizacion.IdUbicacion = Me.IdUbicacion
            vlo_EntOttProveedorCotizacion.IdViaCompraContrato = Me.IdViaCompraContrato
            vlo_EntOttProveedorCotizacion.Anno = Me.Annio
            vlo_EntOttProveedorCotizacion.NumeroGestion = Me.NumeroGestion

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_PROVEEDOR_COTIZACION_BorrarRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               vlo_EntOttProveedorCotizacion) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvc_Identificacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function EnviarProveedor(pvc_Identificacion As String) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_PROVEEDOR_COTIZACION_EnviarProveedor(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.Usuario.UserName, Me.GestionCompra, pvc_Identificacion) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga la ubicacion 
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarAutorizadoUbicacion(pvn_NumEmpleado As Integer) As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Dim Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        Ws_OT_Catalogos.Timeout = -1
        Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return Ws_OT_Catalogos.OTM_AUTORIZADO_UBICACION_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTM_AUTORIZADO_UBICACION.NUM_EMPLEADO, pvn_NumEmpleado))
        Catch ex As Exception
            Throw
        Finally
            If Ws_OT_Catalogos IsNot Nothing Then
                Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' arma la condicion de busqueda
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String
        vlc_Condicion = String.Empty

        vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.ID_UBICACION, Me.IdUbicacion)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.ANNO, Me.Annio)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.NUMERO_GESTION, Me.NumeroGestion)

        Return vlc_Condicion
    End Function

    ''' <summary>
    ''' funcion encargada de realizar los oredenamientos segun los valores brindados
    ''' </summary>
    ''' <param name="pvc_Columna"> numero de la columna para el ordenamiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/08/2016</creationDate>
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
