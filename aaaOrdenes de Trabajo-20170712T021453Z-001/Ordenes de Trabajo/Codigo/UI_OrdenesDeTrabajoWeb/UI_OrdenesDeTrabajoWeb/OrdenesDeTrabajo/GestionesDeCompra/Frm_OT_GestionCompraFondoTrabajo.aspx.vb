Imports Utilerias.OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_GestionCompraFondoTrabajo
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' ubicacion del usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property ParametroFondoTrabajo As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Get
            Return CType(ViewState("ParametroFondoTrabajo"), Wsr_OT_Catalogos.EntOtpParametroUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtpParametroUbicacion)
            ViewState("ParametroFondoTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
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
    ''' data set para materiales iniciales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsMateriales As Data.DataSet
        Get
            Return CType(ViewState("DsMateriales"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsMateriales") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en sesion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
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
    ''' ubicacion del usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property LlaveGestionCompra As String
        Get
            Return CType(ViewState("LlaveGestionCompra"), String)
        End Get
        Set(value As String)
            ViewState("LlaveGestionCompra") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
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
    ''' <creationDate>25/08/2016</creationDate>
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
    ''' <creationDate>25/08/2016</creationDate>
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
    ''' Evento que se ejecuta al cargar la página, inicializa los componentes necesarios
    ''' para el funcionamiento de la página
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try

                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If AutorizadoUbicacion.Existe Then
                    Me.ParametroFondoTrabajo = CargarParametro(Parametros.VALOR_SECUENCIA_FONDO_DE_TRABAJO)


                    Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")

                    If Me.Operacion = eOperacion.Modificar Then
                        LeerParametrosSession()
                        CargarGestionCompra()
                        CargarListaUnion(ObtenerCondicionBusqueda(), String.Format("{0} {1}, {2} {3}", Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL, Ordenamiento.ASCENDENTE, Modelo.V_OT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, Ordenamiento.ASCENDENTE))
                    Else
                        CargarLista(ObtenerCondicionBusqueda(), String.Format("{0} {1}, {2} {3}", Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL, Ordenamiento.ASCENDENTE, Modelo.V_OT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, Ordenamiento.ASCENDENTE))
                    End If


                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ninguna ubicación autorizada, contactar con el administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If

            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el botón Buscar,
    ''' que se encuentra en el área de filtros.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            CargarListaBusqueda()
            'chkSeleccionarTodos.AutoPostBack = False
            'chkSeleccionarTodos.Checked = False
            'chkSeleccionarTodos.AutoPostBack = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el check box de seleccionar todos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub chkSeleccionarTodos_CheckedChanged(sender As Object, e As EventArgs) Handles chkSeleccionarTodos.CheckedChanged
        Try
            ProcesarSeleccion(Me.chkSeleccionarTodos.Checked)
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
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub chkDetalle_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim vlo_CheckBox As CheckBox = CType(sender, CheckBox)

            If vlo_CheckBox.Checked Then
                Me.DsMateriales.Tables(0).Rows.Find(New Object() {vlo_CheckBox.Attributes("CommandArgument").ToString()})(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO) = "1"
            Else
                Me.DsMateriales.Tables(0).Rows.Find(New Object() {vlo_CheckBox.Attributes("CommandArgument").ToString()})(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO) = "0"
            End If
            WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
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
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim vlc_Llave As String()

        Try

            If ValidaGestion() Then
                Select Case Me.Operacion
                    Case eOperacion.Agregar
                        If GestionarCompraAgregar() Then

                            vlc_Llave = Me.LlaveGestionCompra.Split(",")
                            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
                            Me.Session.Add("pvn_IdViaCompraContrato", vlc_Llave(1))
                            Me.Session.Add("pvn_Annio", vlc_Llave(2))
                            Me.Session.Add("pvn_NumeroGestion", vlc_Llave(3))
                            WebUtils.RegistrarScript(Me.Page, "irAConsulta", "irAConsulta();")
                        Else
                            MostrarAlertaError("No ha sido posible actualizar la información del registro")
                        End If
                    Case eOperacion.Modificar
                        If GestionarCompraModificar() Then

                            Me.Session.Add("pvn_IdUbicacion", Me.GestionCompra.IdUbicacion)
                            Me.Session.Add("pvn_IdViaCompraContrato", Me.GestionCompra.IdViaCompraContrato)
                            Me.Session.Add("pvn_Annio", Me.GestionCompra.Anno)
                            Me.Session.Add("pvn_NumeroGestion", Me.GestionCompra.NumeroGestion)
                            WebUtils.RegistrarScript(Me.Page, "irAConsulta", "irAConsulta();")
                        Else
                            MostrarAlertaError("No ha sido posible actualizar la información del registro")
                        End If
                End Select

            Else
                MostrarAlertaError("Debe seleccionar al menos un registro")
            End If
            WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")

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
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' lee y carga los parametros guardados en sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2015</creationDate>
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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
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
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsMateriales = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ListarVOtDetalleMaterial(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

            Me.DsMateriales.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsMateriales.Tables(0).Columns(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL)}

            If Me.DsMateriales IsNot Nothing AndAlso Me.DsMateriales.Tables(0).Rows.Count > 0 Then
                With Me.rpDetalles
                    .DataSource = Me.DsMateriales
                    .DataMember = Me.DsMateriales.Tables(0).TableName
                    .DataBind()
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End With
            Else
                With Me.rpDetalles
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
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvc_Condicion">parámetro con la condicion de búsqueda</param>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaUnion(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsMateriales = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ListarVOtDetalleMaterialUnion(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                Me.GestionCompra)

            Me.DsMateriales.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsMateriales.Tables(0).Columns(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL)}

            If Me.DsMateriales IsNot Nothing AndAlso Me.DsMateriales.Tables(0).Rows.Count > 0 Then
                With Me.rpDetalles
                    .DataSource = Me.DsMateriales
                    .DataMember = Me.DsMateriales.Tables(0).TableName
                    .DataBind()
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End With
            Else
                With Me.rpDetalles
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
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaBusqueda()

        Try

            Dim vlo_DataViewFDetalles As New Data.DataView(Me.DsMateriales.Tables(0))
            vlo_DataViewFDetalles.RowFilter = ObtenerCondicionBusqueda()

            If vlo_DataViewFDetalles IsNot Nothing AndAlso vlo_DataViewFDetalles.Count > 0 Then
                With Me.rpDetalles
                    .DataSource = vlo_DataViewFDetalles
                    .DataMember = Me.DsMateriales.Tables(0).TableName
                    .DataBind()
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End With
            Else
                With Me.rpDetalles
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With
                MostrarAlertaNoHayDatos()
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Determina cual accion sera la que se debe realizar, seleccionar todos o desseleccionar ntodos
    ''' </summary>
    ''' <param name="pvb_SeleccionarTodos"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ProcesarSeleccion(pvb_SeleccionarTodos As Boolean)
        Dim vlo_CheckBox As CheckBox
        Dim vlo_HiddenField As HiddenField

        Try
            For Each item In Me.rpDetalles.Items
                vlo_CheckBox = CType(item.FindControl("chkDetalle"), CheckBox)
                vlo_CheckBox.Checked = pvb_SeleccionarTodos
                vlo_HiddenField = CType(item.FindControl("hdfIdDetalleMaterial"), HiddenField)

                If vlo_CheckBox.Checked Then
                    Me.DsMateriales.Tables(0).Rows.Find(New Object() {vlo_HiddenField.Value})(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO) = "1"
                Else
                    Me.DsMateriales.Tables(0).Rows.Find(New Object() {vlo_HiddenField.Value})(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO) = "0"
                End If

            Next
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
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
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String
        vlc_Condicion = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtNumOT.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, Me.txtNumOT.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, Me.txtNumOT.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtNumeroMaterial.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL, Me.txtNumeroMaterial.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL, Me.txtNumeroMaterial.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtDescMaterial.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OT_DETALLE_MATERIAL.DESCRIPCION, Me.txtDescMaterial.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OT_DETALLE_MATERIAL.DESCRIPCION, Me.txtDescMaterial.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroFechaDesde.Text) And Not String.IsNullOrWhiteSpace(Me.txtFiltroFechaHasta.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} BETWEEN TO_DATE('{1}', 'dd/mm/yyyy') AND TO_DATE('{2}', 'dd/mm/yyyy')", Modelo.V_OT_DETALLE_MATERIAL.FECHA_ASIGNACION, Me.txtFiltroFechaDesde.Text, Me.txtFiltroFechaHasta.Text)
            Else
                vlc_Condicion = String.Format("{0} AND {1} BETWEEN TO_DATE('{2}', 'dd/mm/yyyy') AND TO_DATE('{3}', 'dd/mm/yyyy')", vlc_Condicion, Modelo.V_OT_DETALLE_MATERIAL.FECHA_ASIGNACION, Me.txtFiltroFechaDesde.Text, Me.txtFiltroFechaHasta.Text)
            End If
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} LIKE '{1}'", Modelo.V_OT_DETALLE_MATERIAL.ESTADO, EstadoRegistro.APROBADA)
        Else
            vlc_Condicion = String.Format("{0} AND {1} LIKE '{2}'", vlc_Condicion, Modelo.V_OT_DETALLE_MATERIAL.ESTADO, EstadoRegistro.APROBADA)
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra)
        Else
            vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra)
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_DETALLE_MATERIAL.ID_VIA_COMPRA_CONTRATO, Me.ParametroFondoTrabajo.Valor)
        Else
            vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_DETALLE_MATERIAL.ID_VIA_COMPRA_CONTRATO, Me.ParametroFondoTrabajo.Valor)
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OT_DETALLE_MATERIAL.VIA_DESPACHO, ViaDespacho.VIACOMPRA)
        Else
            vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OT_DETALLE_MATERIAL.VIA_DESPACHO, ViaDespacho.VIACOMPRA)
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} = 0", Modelo.V_OT_DETALLE_MATERIAL.POSEE_GESTION_COMPRA)
        Else
            vlc_Condicion = String.Format("{0} AND {1} = 0", vlc_Condicion, Modelo.V_OT_DETALLE_MATERIAL.POSEE_GESTION_COMPRA)
        End If

        Return vlc_Condicion
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ValidaGestion() As Boolean
        Try

            For Each vlo_FIla In Me.DsMateriales.Tables(0).Rows
                If vlo_FIla(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO).ToString = "1" Then
                    Return True
                End If
            Next

            Return False

        Catch ex As Exception
            Throw
        End Try
    End Function


    ''' <summary>
    ''' Administra el proceso para modificar la gestion
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function GestionarCompraModificar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.GestionCompra.Observaciones = Me.txtObservaciones.Text

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_ModificarGestionCompraFondoTrabajo(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.DsMateriales, Me.Usuario.UserName, Me.AutorizadoUbicacion.IdUbicacionAdministra, Me.GestionCompra) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Administra el proceso para agregar la gestion
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function GestionarCompraAgregar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.LlaveGestionCompra = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_InsertarGestionCompraFondoTrabajo(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.DsMateriales, Me.Usuario.UserName, Me.AutorizadoUbicacion.IdUbicacionAdministra, Me.txtObservaciones.Text)

            Return Me.LlaveGestionCompra <> String.Empty

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
    ''' <creationDate>25/08/2016</creationDate>
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
    ''' 
    ''' </summary>
    ''' <param name="pvn_IdParametro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargarParametro(pvn_IdParametro As Integer) As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Dim Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        Ws_OT_Catalogos.Timeout = -1
        Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, pvn_IdParametro))
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
