Imports Utilerias.OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_ConsultaGestiónCompraFondoTrabajo
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property GestionCompra As Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra
        Get
            Return CType(ViewState("GestionCompra"), Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra)
        End Get
        Set(value As Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra)
            ViewState("GestionCompra") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en sesion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/08/2016</creationDate>
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
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/08/2016</creationDate>
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
    ''' <creationDate>29/08/2016</creationDate>
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
    ''' <creationDate>29/08/2016</creationDate>
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
    ''' <creationDate>25/08/2016</creationDate>
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
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpMateriales_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpMateriales.ItemDataBound
        Dim vlo_HtmlAnchor As HtmlAnchor
        Dim vlo_HtmlGenericControl As HtmlGenericControl
        Dim vlo_WebUserControl As Controles_wuc_OT_Lineas_Material_Gestion_Compra

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_HtmlAnchor = CType(e.Item.FindControl("ancorAcordeon"), HtmlAnchor)
            vlo_HtmlGenericControl = e.Item.FindControl("cuerpoAcordeon1")
            vlo_HtmlAnchor.HRef = "#" + vlo_HtmlGenericControl.ClientID

            vlo_WebUserControl = CType(e.Item.FindControl("wucLineasMaterialGestionCompra"), Controles_wuc_OT_Lineas_Material_Gestion_Compra)
            vlo_WebUserControl.Inicializar()

        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cargar la página, inicializa los componentes necesarios
    ''' para el funcionamiento de la página
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim vlo_Wuc_OT_Lineas_Material_Gestion_Compra As Controles_wuc_OT_Lineas_Material_Gestion_Compra

        For Each vlo_Item In Me.rpMateriales.Items
            vlo_Wuc_OT_Lineas_Material_Gestion_Compra = CType(vlo_Item.FindControl("wucLineasMaterialGestionCompra"), Controles_wuc_OT_Lineas_Material_Gestion_Compra)
            AddHandler vlo_Wuc_OT_Lineas_Material_Gestion_Compra.Recargar, AddressOf RecargarFormulario
        Next

        If Not Page.IsPostBack Then
            Try

                Me.Usuario = New UsuarioActual
                LeerParametrosSession()
                CargarGestionCompra()
                CargarLista(ObtenerCondicionBusqueda(), String.Empty)

            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            If Me.GestionCompra.Estado = EstadoGestionCompra.CREADA Then
                Me.Session.Add("pvn_Operacion", eOperacion.Modificar)
                Me.Session.Add("pvn_IdUbicacion", Me.GestionCompra.IdUbicacion)
                Me.Session.Add("pvn_IdViaCompraContrato", Me.GestionCompra.IdViaCompraContrato)
                Me.Session.Add("pvn_Annio", Me.GestionCompra.Anno)
                Me.Session.Add("pvn_NumeroGestion", Me.GestionCompra.NumeroGestion)
                WebUtils.RegistrarScript(Me.Page, "irASeleccion", "irASeleccion();")
            Else
                WebUtils.RegistrarScript(Me.Page, "irAListado", "irAListado();")
            End If
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
    ''' <creationDate>29/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Try
            If ModificarGestionCompra() Then
                Me.Session.Add("pvn_IdUbicacion", Me.GestionCompra.IdUbicacion)
                Me.Session.Add("pvn_IdViaCompraContrato", Me.GestionCompra.IdViaCompraContrato)
                Me.Session.Add("pvn_Annio", Me.GestionCompra.Anno)
                Me.Session.Add("pvn_NumeroGestion", Me.GestionCompra.NumeroGestion)
                Me.Session.Add("pvc_PantallaRetorno", "Frm_OT_ConsultaGestiónCompraFondoTrabajo.aspx")
                WebUtils.RegistrarScript(Me.Page, "irAInclusionProveedores", "irAInclusionProveedores();")
            Else
                MostrarAlertaError("No ha sido posible actualizar la información del registro")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Se recarga el formulario despues de haber eliminado un material del acordeon
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>19/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub RecargarFormulario()

        Try
            CargarLista(ObtenerCondicionBusqueda(), String.Empty)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' lee y carga los parametros guardados en sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/08/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametrosSession()
        Try

            Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
            Me.IdViaCompraContrato = WebUtils.LeerParametro(Of Integer)("pvn_IdViaCompraContrato")
            Me.Annio = WebUtils.LeerParametro(Of Integer)("pvn_Annio")
            Me.NumeroGestion = WebUtils.LeerParametro(Of Integer)("pvn_NumeroGestion")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvc_Condicion">parámetro con la condicion de búsqueda</param>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_GESTION_COMPRA_ListarVOtLineaGcGroupFondoPersonalizado(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpMateriales
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With
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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/08/2016</creationDate>
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
                Me.txtObservaciones.Text = Me.GestionCompra.Observaciones
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('El identificador provisto no pertenece a ningun registro del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
            End If

            If Me.GestionCompra.Estado = EstadoGestionCompra.CREADA Then
                Me.btnSiguiente.Visible = True
                Me.btnCancelar.Text = "Regresar"
            Else
                Me.btnSiguiente.Visible = False
                Me.btnCancelar.Text = "Cancelar"
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ModificarGestionCompra() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.GestionCompra.Estado = EstadoGestionCompra.SOLICITUD_DE_COTIZACONES
            Me.GestionCompra.Usuario = Me.Usuario.UserName

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_ModificarGestionAgregarGrupos(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.GestionCompra) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' arma la condicion de busqueda
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String
        vlc_Condicion = String.Empty

        vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_UBICACION, Me.IdUbicacion)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_LINEA_GC_GROUP_FONDO.ANNO, Me.Annio)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_LINEA_GC_GROUP_FONDO.NUMERO_GESTION, Me.NumeroGestion)

        Return vlc_Condicion
    End Function

#End Region

End Class
