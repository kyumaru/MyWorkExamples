Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports System.Data

Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_GestionMateriales
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/6/2016</creationDate>
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
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property OrdenTrabajo As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la identificación del sector o taller 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/6/2016</creationDate>
    Private Property IdSectorTaller As Integer
        Get
            If ViewState("IdSectorTaller") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdSectorTaller"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdSectorTaller") = value
        End Set
    End Property

    ''' <summary>
    ''' Es el identificador base de la Orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/6/2016</creationDate>
    Public Property IdOrdenTrabajo As String
        Get
            If ViewState("IdOrdenTrabajo") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdOrdenTrabajo"), String)
        End Get
        Set(value As String)
            ViewState("IdOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Guarda la ubicación de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/6/2016</creationDate>
    Public Property IdUbicacion As Integer
        Get
            If ViewState("IdUbicacion") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Guarda la ubicación de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/6/2016</creationDate>
    Public Property Anno As Integer
        Get
            If ViewState("Anno") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("Anno"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Anno") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de materiales a ingresar eliminar o modificar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsMaterialesInsert As Data.DataSet
        Get
            Return CType(ViewState("DsMaterialesInsert"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsMaterialesInsert") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset detalle material
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsDetalle As Data.DataSet
        Get
            Return CType(ViewState("DsDetalle"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsDetalle") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/6/2016</creationDate>
    Private Property MontoTotal As Double
        Get
            If ViewState("MontoTotal") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("MontoTotal"), Double)
        End Get
        Set(value As Double)
            ViewState("MontoTotal") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el costo total de la orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/6/2016</creationDate>
    Private Property CostoTotalOT As Integer
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

    ''' <summary>
    ''' Contador para tabs
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>18/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ContadorTaps As Integer
        Get
            Return CType(ViewState("ContadorTaps"), Integer)
        End Get
        Set(value As Integer)
            ViewState("ContadorTaps") = value
        End Set
    End Property

    ''' <summary>
    ''' Guarda la ubicación de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/6/2016</creationDate>
    Public Property Consecutivo As Integer
        Get
            If ViewState("Consecutivo") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("Consecutivo"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Consecutivo") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta al cargar la pagina e inicializa componentes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                InicializarFormulario()
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
    ''' Evento que por cada fila adjunta un identificador único para el textbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>21/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpPedidos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpPedidos.ItemDataBound
        Dim vlo_txtCantidad As TextBox
        
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_txtCantidad = e.Item.FindControl("txtCantidad")
            If vlo_txtCantidad IsNot Nothing Then
                vlo_txtCantidad.Attributes.Add("data-uniqueid", vlo_txtCantidad.UniqueID)

                If Not vlo_txtCantidad.Visible Then
                    vlo_txtCantidad.Text = 0
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da carga cada uno de los elementos del repeater de contenidos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>18/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpListaTapsContenidos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpListaTapsContenidos.ItemDataBound
        Dim vlo_WebUserControl As Controles_wuc_OT_GestionMaterial

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_WebUserControl = CType(e.Item.FindControl("wuc_GestionMaterial"), Controles_wuc_OT_GestionMaterial)
            vlo_WebUserControl.Inicializar()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da carga cada uno de los elementos del repeater de titulos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>18/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpListaTapsTitulos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpListaTapsTitulos.ItemDataBound
        Dim vlo_HtmlAnchor As HtmlAnchor

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_HtmlAnchor = CType(e.Item.FindControl("cuerpoTabPanel"), HtmlAnchor)
            vlo_HtmlAnchor.HRef = String.Format("#{0}_{1}", "cphContenidoFormulario_cphFormulario_rpListaTapsContenidos_cuerpoTabPanel", Me.ContadorTaps.ToString)

            Me.ContadorTaps = Me.ContadorTaps + 1
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>20/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnTramitar_Click(sender As Object, e As EventArgs)
        Try
            TramitarSolicitudMateriales()
            CargarLista()
            CargarTabs()
            upTabs.Update()
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
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>23/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkMaterialesAdicionales_Click(sender As Object, e As EventArgs)
        Try
            Me.Session.Add("pvn_IdUbicacion", Me.OrdenTrabajo.IdUbicacion)
            Me.Session.Add("pvc_IdOrdenTrabajo", Me.OrdenTrabajo.IdOrdenTrabajo)
            Me.Session.Add("pvn_Anno", Me.OrdenTrabajo.Anno)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("Frm_OT_SolicitudAjusteMaterial.aspx", False)
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvc_Mensaje"></param>
    ''' <remarks></remarks>
    ''' <creationDate>17/6/2016</creationDate>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub mostrarAlertaGuardadoExitoso()
        WebUtils.RegistrarScript(Me, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <creationDate>17/6/2016</creationDate>
    Private Sub mostrarAlertSinCantidadDisponible()
        WebUtils.RegistrarScript(Me, "alertaError", "mostrarAlertSinCantidadDisponible();")
    End Sub

    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vln_costoPromedio As Double

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            MontoTotal = 0
            Me.DsMaterialesInsert = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ListarVOttGestionMaterial(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}' AND {2} = {3} AND {4} <> '{5}'",
                    Modelo.V_OTT_GESTION_MATERIAL.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo,
                    Modelo.V_OTT_GESTION_MATERIAL.ID_UBICACION, Me.IdUbicacion,
                    Modelo.V_OTT_GESTION_MATERIAL.ESTADO, EstadoRegistro.PENDIENTE_ENVIO),
                String.Format("{0} {1}", Modelo.V_OTT_GESTION_MATERIAL.ID_MATERIAL, Ordenamiento.ASCENDENTE), False, 0, 0)

            If Me.DsMaterialesInsert IsNot Nothing AndAlso Me.DsMaterialesInsert.Tables(0).Rows.Count > 0 Then
                Me.rpPedidos.DataSource = DsMaterialesInsert
                Me.rpPedidos.DataMember = Me.DsMaterialesInsert.Tables(0).TableName
                Me.rpPedidos.DataBind()
                Me.rpPedidos.Visible = True
                For Each vlo_fila In DsMaterialesInsert.Tables(0).Rows
                    vln_costoPromedio = vlo_fila(Modelo.V_OTT_GESTION_MATERIAL.COSTO_PROMEDIO_TOTAL)
                    MontoTotal = MontoTotal + vln_costoPromedio
                Next
                If MontoTotal < CostoTotalOT Then
                    lblMontoTotal.Attributes.Add("style", "color:black;")
                Else
                    lblMontoTotal.Attributes.Add("style", "color:red;")
                End If

                Me.lblMontoTotal.Text = String.Format("Total: ₡{0:n2}", MontoTotal)
            Else
                With Me.rpPedidos
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpPedidos.Visible = False
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
    ''' <creationDate>22/6/2016</creationDate>
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

    ''' <summary>
    ''' Inicializa el formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Usuario = New UsuarioActual()
        leerParametros()
        CargarCostoTotalOrden()
        CargarLista()
        CargarOrdenTrabajo(Me.IdUbicacion, Me.IdOrdenTrabajo)
        InicializarControlesUsuario()
        ContadorTaps = 0
        CargarTabs()
    End Sub

    ''' <summary>
    ''' Se encarga de leer los parámetros provenientes del listado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>6/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub leerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        Me.IdSectorTaller = WebUtils.LeerParametro(Of Integer)("pvn_IdSectorTaller")

        Me.Session.Add("pvn_IdUbicacion", IdUbicacion)
        Me.Session.Add("pvc_IdOrdenTrabajo", IdOrdenTrabajo)
        Me.Session.Add("pvn_Anno", Anno)
        Me.Session.Add("pvn_IdSectorTaller", IdSectorTaller)
    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">id de la ubicacion</param>
    ''' <param name="pvn_IdOrdenTrabajo">id de la orden</param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>6/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenTrabajo(pvn_IdUbicacion As Integer, pvn_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION,
                              pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvn_IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario para mostrar la información general
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>6/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarControlesUsuario()

        'Asignación de Datos para generar el web user control de la información general
        ctrl_InfoGeneral.Anno = Me.Anno
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.IdUbicacion

        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()

    End Sub

    Private Sub CargarTabs()

        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            DsDetalle = vlo_Ws_OT_OrdenesDeTrabajo.OTT_SOLICITUD_RETIRO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}' AND {2} = {3}", Modelo.OTT_SOLICITUD_RETIRO.ID_ORDEN_TRABAJO,
                              IdOrdenTrabajo, Modelo.OTT_SOLICITUD_RETIRO.ID_UBICACION, IdUbicacion),
                          String.Format("{0} ASC", Modelo.OTT_SOLICITUD_RETIRO.ID_SOLICITUD_RETIRO), False, 0, 0)

            If Me.DsDetalle.Tables(0).Rows.Count > 0 Then

                With Me.rpListaTapsTitulos
                    .DataSource = DsDetalle
                    .DataMember = DsDetalle.Tables(0).TableName
                    .DataBind()
                End With

                Me.rpListaTapsContenidos.DataSource = DsDetalle
                Me.rpListaTapsContenidos.DataMember = DsDetalle.Tables(0).TableName
                Me.rpListaTapsContenidos.DataBind()
                Me.rpListaTapsContenidos.Visible = True
            Else
                With Me.rpListaTapsContenidos
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpListaTapsContenidos.Visible = False
            End If

            ContadorTaps = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene los datos ingresados en la lista de materiales solicitados e ingresa las solicitudes de salida del material
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate></creationDate>
    ''' <changeLog></changeLog>
    Private Sub TramitarSolicitudMateriales()
        Dim vlb_BanderaSolicitud As Boolean = True
        Dim vlo_DetalleSolicitud As Data.DataRow
        Dim vlo_txtCantidad As TextBox
        Dim vln_Cantidad As Integer
        Dim vln_Disponible As Integer
        Dim vlc_estado As String
        Dim vlb_bandera As Boolean = True
        Dim vlo_DsInsertar As Data.DataSet
        Dim vlo_EntOttSolicitudRetiro As EntOttSolicitudRetiro
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            'Carga la estructura básica del listado a ingresar
            vlo_DsInsertar = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_RETIRO_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("1 = 0"), String.Empty, False, 0, 0)

            vlo_EntOttSolicitudRetiro = New EntOttSolicitudRetiro

            'Hay que recorrer el repeater para obtener el text box y validarlo
            For Each vlo_filarepeater As RepeaterItem In rpPedidos.Items
                vlo_txtCantidad = CType(vlo_filarepeater.FindControl("txtCantidad"), TextBox)

                If Not String.IsNullOrWhiteSpace(vlo_txtCantidad.Text) Then
                    'Se obtiene la cantidad a solicitar y la cantidad disponible en inventario para validar que esté correcto el rebajo que se hará
                    vln_Cantidad = vlo_txtCantidad.Text
                    vln_Disponible = vlo_txtCantidad.Attributes("data-disponible")
                    If vln_Cantidad > 0 Then

                        If vln_Cantidad <= vln_Disponible Then
                            'Solo se podran tramitar las que tengan estado diferente de pendiente de aprobación
                            vlc_estado = vlo_txtCantidad.Attributes("data-estado")

                            If vlc_estado = EstadoRegistro.APROBADA Or vlc_estado = EstadoRegistro.RECIBIDO_BODEGA Then

                                If vlb_BanderaSolicitud Then

                                    vlo_EntOttSolicitudRetiro.Anno = Now.Year
                                    vlo_EntOttSolicitudRetiro.EstadoSolicitudRetiro = EstadoSolicitudRetiro.SOLICITUD_MATERIAL_CREADA
                                    vlo_EntOttSolicitudRetiro.IdUbicacion = IdUbicacion
                                    vlo_EntOttSolicitudRetiro.IdOrdenTrabajo = IdOrdenTrabajo
                                    vlo_EntOttSolicitudRetiro.FechaHoraRetiro = txtFechaRetiro.Text
                                    vlo_EntOttSolicitudRetiro.JornadaRetiro = IIf(Me.rbtnTurno.SelectedValue = 1, Jornada.MANANA, Jornada.TARDE)
                                    vlo_EntOttSolicitudRetiro.Usuario = Usuario.UserName
                                    vlb_BanderaSolicitud = False
                                End If

                                vlo_DetalleSolicitud = vlo_DsInsertar.Tables(0).NewRow

                                vlo_DetalleSolicitud(Modelo.OTT_DETALLE_RETIRO.ANNO) = Now.Year
                                vlo_DetalleSolicitud(Modelo.OTT_DETALLE_RETIRO.ID_DETALLE_MATERIAL) = vlo_txtCantidad.Attributes("data-idDetalleMaterial")
                                vlo_DetalleSolicitud(Modelo.OTT_DETALLE_RETIRO.CANTIDAD_SOLICITADA) = vln_Cantidad
                                vlo_DetalleSolicitud(Modelo.OTT_DETALLE_RETIRO.ESTADO) = EstadoDetalle.PENDIENTE
                                vlo_DetalleSolicitud(Modelo.OTT_DETALLE_RETIRO.COSTO_CALCULADO) = String.Format("{0:n2}", (vlo_txtCantidad.Attributes("data-costoPromedio") * vln_Cantidad))
                                vlo_DetalleSolicitud(Modelo.OTT_DETALLE_RETIRO.USUARIO) = Usuario.UserName

                                vlo_DsInsertar.Tables(0).Rows.Add(vlo_DetalleSolicitud)

                            End If

                        Else
                            'Validacion de valores ingresados en la cantidad a solicitar excepto para los registros con estado pendiente
                            vlc_estado = vlo_txtCantidad.Attributes("data-estado")

                            If vlc_estado <> EstadoRegistro.PENDIENTE_APROBACION Then
                                vlb_bandera = False
                                MostrarAlertaError("La cantidad a solicitar debe ser menor que la cantidad disponible y que la solicitada.")
                                Exit For
                            End If

                        End If
                    End If
                Else

                    'validación de vacios en el campo cantidad a solicitar excepto para los registros con estado pendiente
                    vlc_estado = vlo_txtCantidad.Attributes("data-estado")

                    If vlc_estado <> EstadoRegistro.PENDIENTE_APROBACION Then
                        'La bandera representa cuando el usuario olvidó colocar un valor en la cantidad a solicitar cuando
                        'el estado del detalle material es diferente de pendiente de aprobación, en este caso no se le permite
                        'tramitar la solicitud, se ocupaba informar al ciclo sobre esa eventualidad para no insertar campos vacios en cantidad a solicitar.
                        vlb_bandera = False
                        MostrarAlertaError("Debe ingresar todas las cantidades.")
                        Exit For
                    End If
                End If

            Next

            If vlb_bandera Then
                If vlo_DsInsertar.Tables(0).Rows.Count > 0 Then

                    vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_RETIRO_SolicitudesMaterialCreadas(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        vlo_DsInsertar, vlo_EntOttSolicitudRetiro)

                Else
                    MostrarAlertaError("Como mínimo debe ingresar una cantidad a solicitar en uno de los materiales.")
                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
            If vlo_DsInsertar IsNot Nothing Then
                vlo_DsInsertar.Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"

#End Region

End Class
