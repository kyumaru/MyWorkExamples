Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports Wsr_OT_Catalogos
Imports Wsr_SDP_ReportServer
Imports Utilerias.GeneradorDeReportes

Partial Class OrdenesDeTrabajo_OrdenesDeTrabajo_Lst_OT_GestionOrdenTrabajoSupervisor
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la ultima condicion de busqueda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/12/2015</creationDate>
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
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property ValorSeleccionadoCombo As Integer
        Get
            If ViewState("ValorSeleccionadoCombo") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("ValorSeleccionadoCombo"), Integer)
        End Get
        Set(value As Integer)
            ViewState("ValorSeleccionadoCombo") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property Paginador As Integer
        Get
            If ViewState("Paginador") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("Paginador"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Paginador") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
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
    ''' <creationDate>28/09/2015</creationDate>
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
    ''' <creationDate>28/09/2015</creationDate>
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
    ''' Orden de trabajo 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property OrdenTrabajo As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2015</creationDate>
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/09/2015</creationDate>
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>13/10/2015</creationDate>
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
    ''' parametro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property vlg_ParametroRango As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Get
            Return CType(ViewState("vlg_Parametro1"), Wsr_OT_Catalogos.EntOtpParametroUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtpParametroUbicacion)
            ViewState("vlg_Parametro1") = value
        End Set
    End Property

    ''' <summary>
    ''' parametro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property vlg_ParametroAtencion As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Get
            Return CType(ViewState("vlg_Parametro2"), Wsr_OT_Catalogos.EntOtpParametroUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtpParametroUbicacion)
            ViewState("vlg_Parametro2") = value
        End Set
    End Property

    ''' <summary>
    ''' parametro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property vlg_ParametroAtraso As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Get
            Return CType(ViewState("vlg_Parametro3"), Wsr_OT_Catalogos.EntOtpParametroUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtpParametroUbicacion)
            ViewState("vlg_Parametro3") = value
        End Set
    End Property

    ''' <summary>
    ''' parametro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property FechaRango As DateTime
        Get
            Return CType(ViewState("FechaRango"), DateTime)
        End Get
        Set(value As DateTime)
            ViewState("FechaRango") = value
        End Set
    End Property

    ''' <summary>
    ''' parametro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property FechaAtencion As DateTime
        Get
            Return CType(ViewState("FechaAtencion"), DateTime)
        End Get
        Set(value As DateTime)
            ViewState("FechaAtencion") = value
        End Set
    End Property

    ''' <summary>
    ''' parametro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property FechaAtraso As DateTime
        Get
            Return CType(ViewState("FechaAtraso"), DateTime)
        End Get
        Set(value As DateTime)
            ViewState("FechaAtraso") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property NuevaSeccion As Integer
        Get
            If ViewState("NuevaSeccion") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("NuevaSeccion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("NuevaSeccion") = value
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
    ''' <creationDate>28/09/2015</creationDate>
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
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>09/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkTrazabilidad_Command(sender As Object, e As CommandEventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = e.CommandArgument.Split("¬")

            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)
            Me.Session.Add("pvc_PantallaRetorno", "OrdenesDeTrabajo/Lst_OT_GestionOrdenTrabajoSupervisor.aspx")
            Me.Session.Add("pvn_SelectedValue", Me.ddlSectorYTaller.SelectedValue)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("../Frm_OT_ConsultaTrazabilidad.aspx", False)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados de la 
    ''' tabla del listado, carga la lista de registros en orden relacionado a la columna seleccionada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpOrdenTrabajo_Command(sender As Object, e As CommandEventArgs)
        Try

            If Me.ddlSectorYTaller.SelectedValue <> String.Empty Then
                CargarLista(ObtenerCondicionBusquedaCombo, ObtenerExpresionDeOrdenamiento(e.CommandName), pnRpOrdenTrabajo.PaginaActualLista)
            End If
            WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")

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
    ''' <creationDate>28/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub pnRpOrdenTrabajo_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpOrdenTrabajo.CambioDePagina
        Try
            Paginador = pvn_PaginaSeleccionada

            If Me.ddlSectorYTaller.SelectedValue <> String.Empty Then
                CargarLista(ObtenerCondicionBusquedaCombo, UltimoSortExpression, pvn_PaginaSeleccionada)
            End If
            WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al carar el repeater de ordenes, asigna atributos a cada elemento del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpOrdenTrabajo_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpOrdenTrabajo.ItemDataBound
        Dim vlo_IbRevisar As HtmlTableRow
        Dim vlo_HiddenField As HiddenField
        Dim vlo_HiddenField2 As HiddenField


        If e.Item.FindControl("trTabla") IsNot Nothing Then
            vlo_IbRevisar = CType(e.Item.FindControl("trTabla"), HtmlTableRow)
            vlo_HiddenField = CType(e.Item.FindControl("hdFechaAsignacion"), HiddenField)
            vlo_HiddenField2 = CType(e.Item.FindControl("hdfTipoOrden"), HiddenField)

            If vlg_ParametroRango.Existe And vlg_ParametroAtencion.Existe And vlg_ParametroAtraso.Existe Then

                If CType(vlo_HiddenField.Value, DateTime) > FechaRango And CType(vlo_HiddenField.Value, DateTime) <= DateTime.Today Then
                    vlo_IbRevisar.BgColor = "#c8e3c5"
                ElseIf CType(vlo_HiddenField.Value, DateTime) > FechaAtencion And CType(vlo_HiddenField.Value, DateTime) <= FechaRango Then
                    vlo_IbRevisar.BgColor = "#f7f7c6"
                Else
                    vlo_IbRevisar.BgColor = "#F6D1C9"
                End If

                If vlo_HiddenField2.Value = TipoOrden.EMERGENCIA Then
                    vlo_IbRevisar.BgColor = "#F6D1C9"
                End If
            End If
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
    ''' <creationDate>28/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try

            If Me.ddlSectorYTaller.SelectedValue <> String.Empty Then
                UltimaCondicionBusqueda = String.Empty
                Me.Paginador = 0
                Buscar(ObtenerCondicionBusquedaCombo, UltimoSortExpression)
                Me.txtNumOrden.Text = String.Empty
                Me.ddlEstado.SelectedValue = String.Empty
                Me.ddlEdificio.SelectedValue = String.Empty
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
    ''' <creationDate>14/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlSectorYTaller_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSectorYTaller.SelectedIndexChanged
        Try
            If Me.ddlSectorYTaller.SelectedValue <> String.Empty Then
                UltimaCondicionBusqueda = String.Empty
                UltimoSortExpression = String.Format("{0} {1} , {2} {3}", Modelo.V_OT_REVISION_SUPERVISOR.ES_EMERGENCIA, Ordenamiento.ASCENDENTE, Modelo.V_OT_REVISION_SUPERVISOR.FECHA_HORA_SOLICITA, Ordenamiento.DESCENDENTE)
                Buscar(ObtenerCondicionBusquedaCombo, String.Format("{0} {1} , {2} {3}", Modelo.V_OT_REVISION_SUPERVISOR.ES_EMERGENCIA, Ordenamiento.ASCENDENTE, Modelo.V_OT_REVISION_SUPERVISOR.FECHA_HORA_SOLICITA, Ordenamiento.DESCENDENTE))
            Else
                UltimaCondicionBusqueda = String.Empty
                Me.Paginador = 0
                Me.lblCantidadDeRegistros.Visible = False
                Me.lblCantidadDeRegistros.Text = String.Empty
                Me.rpOrdenTrabajo.Visible = False
                Me.pnRpOrdenTrabajo.Visible = False
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            End If
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
    ''' <creationDate>27/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkNumOt_Click(sender As Object, e As EventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, LinkButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_Operacion", eOperacion.Consultar)
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)
            Me.Session.Add("pvn_Regresar", "Lst_OT_GestionOrdenTrabajoSupervisor.aspx")
            Me.Session.Add("pvn_SelectedValue", Me.ddlSectorYTaller.SelectedValue)

            Response.Redirect(String.Format("Frm_OT_OrdenTrabajo.aspx"), False)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarListado()
        Try

            If Roles.IsUserInRole(Membership.GetUser.UserName, RolesSistema.OT_SUPERVISOR) Then

                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If Me.AutorizadoUbicacion.Existe Then

                    InicializarParametros()
                    LeerParametrosSession()
                    CargarInformacionSupervisor()
                    CargarComboEstado()
                    CargarComboLugarTrabajo()
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")

                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted debe de indicar la sede en la cual presentará las ordenes de trabajo.','Frm_OT_SelecciónSedeTrabajo.aspx');")
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
    ''' <creationDate>02/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametrosSession()

        Try

            Me.NuevaSeccion = WebUtils.LeerParametro(Of Integer)("pvn_NuevaSeccion")

            If Me.NuevaSeccion <> 1 Then
                Me.Paginador = WebUtils.LeerParametro(Of Integer)("pvn_Paginador")
                Me.UltimaCondicionBusqueda = WebUtils.LeerParametro(Of String)("pvc_UltimaCondicionBusqueda")
                Me.ValorSeleccionadoCombo = WebUtils.LeerParametro(Of Integer)("pvn_SelectedValue")
            Else
                Me.Paginador = 0
                Me.UltimaCondicionBusqueda = String.Empty
                Me.ValorSeleccionadoCombo = 0
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Carga e inicializa los parametros del sistema, relacionados a los rangos de tiempos para semaforo de OT's
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarParametros()
        Try
            vlg_ParametroRango = CargarParametro(Parametros.TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_EN_RANGO, Me.AutorizadoUbicacion.IdUbicacionAdministra)
            vlg_ParametroAtencion = CargarParametro(Parametros.TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_ATENCION, Me.AutorizadoUbicacion.IdUbicacionAdministra)
            vlg_ParametroAtraso = CargarParametro(Parametros.TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_ATRASO, Me.AutorizadoUbicacion.IdUbicacionAdministra)

            If vlg_ParametroRango.Existe And vlg_ParametroAtencion.Existe And vlg_ParametroAtraso.Existe Then
                FechaRango = DateTime.Now.AddDays(-CType(vlg_ParametroRango.Valor, Integer))
                FechaAtencion = DateTime.Now.AddDays(-CType(vlg_ParametroAtencion.Valor, Integer))
                FechaAtraso = DateTime.Now.AddDays(-CType(vlg_ParametroAtraso.Valor, Integer))
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboEstado()
        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
        Me.ddlEstado.Items.Add(New ListItem("Asignada", EstadoOrden.ASIGNADA))
        Me.ddlEstado.Items.Add(New ListItem("No Conforme", EstadoOrden.NO_CONFORME))
        Me.ddlEstado.Items.Add(New ListItem("Recibido Conforme del Solicitante", EstadoOrden.RECIBIDO_CONFORME_SOLICITANTE))
        Me.ddlEstado.Items.Add(New ListItem("En Estudio", EstadoOrden.EN_ESTUDIO))
        Me.ddlEstado.Items.Add(New ListItem("En Ejecución", EstadoOrden.EN_PROCESO))
        Me.ddlEstado.Items.Add(New ListItem("En Evaluación", EstadoOrden.EN_EVALUACION))
        Me.ddlEstado.Items.Add(New ListItem("Para Impresión", EstadoOrden.PARA_IMPRESION))
        Me.ddlEstado.Items.Add(New ListItem("Para Retiro de Material", EstadoOrden.PARA_RETIRO_MATERIAL))
        Me.ddlEstado.Items.Add(New ListItem("Material Pendiente de Compra", EstadoOrden.MATERIAL_PENDIENTE_COMPRA))
        Me.ddlEstado.Items.Add(New ListItem("Liquidada", EstadoOrden.LIQUIDADA))
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboLugarTrabajo()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlEdificio.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_LUGAR_TRABAJO_ListarRegistros(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} LIKE '%{1}%' AND {2} = {3}", Modelo.V_OTM_LUGAR_TRABAJO.ESTADO, Estado.ACTIVO, Modelo.V_OTM_LUGAR_TRABAJO.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra),
               String.Empty,
               False,
               0,
               0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlEdificio
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_LUGAR_TRABAJO.NOMBRE
                    .DataValueField = Modelo.V_OTM_LUGAR_TRABAJO.ID_LUGAR_TRABAJO
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarVOtRevisionSupervisor(
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
    ''' Método encargado de realizar la busqueda de registros segun los datos de 
    ''' condicion y orden de búsqueda
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerExpresionDeOrdenamiento(Modelo.V_OT_REVISION_SUPERVISOR.ES_EMERGENCIA)
        End If

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntDatosPaginacion =
                vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerDatosPaginacionVOtRevisionSupervisor(
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                     pvc_Condicion,
                     pvc_Orden,
                     CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpOrdenTrabajo.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas


                'CargarLista(pvc_Condicion, pvc_Orden, 1)

                If Me.Paginador > 0 Then
                    CargarLista(pvc_Condicion, pvc_Orden, Me.Paginador)
                Else
                    CargarLista(pvc_Condicion, pvc_Orden, 1)
                End If


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
    ''' carga oos sector y talleres del supervisor en session
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarInformacionSupervisor()
        Dim vlo_DsCategoriaServicio As Data.DataSet
        Dim vlo_DsActividad As Data.DataSet
        Dim vlo_DsSectorTaller As Data.DataSet
        Dim vlc_Condicion As String
        vlc_Condicion = String.Empty

        Try

            vlo_DsCategoriaServicio = CargarCategoriasServicio(Me.Usuario.NumEmpleado)

            For Each vlo_Fila In vlo_DsCategoriaServicio.Tables(0).Rows
                If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                    vlc_Condicion = String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_CATEGORIA_SERVICIO, vlo_Fila(Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO).ToString)
                Else
                    vlc_Condicion = String.Format("{0} OR {1} = {2}", vlc_Condicion, Modelo.OTM_ACTIVIDAD.ID_CATEGORIA_SERVICIO, vlo_Fila(Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO).ToString)
                End If
            Next

            If Not String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlo_DsActividad = CargarActividades(vlc_Condicion)
            Else
                vlo_DsActividad = CargarActividades("1 = 0")
            End If

            vlc_Condicion = String.Empty

            For Each vlo_Fila In vlo_DsCategoriaServicio.Tables(0).Rows
                If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                    vlc_Condicion = String.Format("{0} = {1}", Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, vlo_Fila(Modelo.OTM_CATEGORIA_SERVICIO.ID_SECTOR_TALLER).ToString)
                Else
                    vlc_Condicion = String.Format("{0} OR {1} = {2}", vlc_Condicion, Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, vlo_Fila(Modelo.OTM_CATEGORIA_SERVICIO.ID_SECTOR_TALLER).ToString)
                End If
            Next

            For Each vlo_Fila In vlo_DsActividad.Tables(0).Rows
                If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                    vlc_Condicion = String.Format("{0} = {1}", Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, vlo_Fila(Modelo.OTM_ACTIVIDAD.ID_SECTOR_TALLER).ToString)
                Else
                    vlc_Condicion = String.Format("{0} OR {1} = {2}", vlc_Condicion, Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, vlo_Fila(Modelo.OTM_ACTIVIDAD.ID_SECTOR_TALLER).ToString)
                End If
            Next

            If Not String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlo_DsSectorTaller = CargarSectoresTalleres(vlc_Condicion)

                If vlo_DsSectorTaller IsNot Nothing AndAlso vlo_DsSectorTaller.Tables(0).Rows.Count > 0 Then
                    Me.ddlSectorYTaller.Items.Add(New ListItem("Seleccione", String.Empty))

                    With Me.ddlSectorYTaller
                        .DataSource = vlo_DsSectorTaller
                        .DataMember = vlo_DsSectorTaller.Tables(0).TableName
                        .DataTextField = Modelo.OTM_SECTOR_TALLER.NOMBRE
                        .DataValueField = Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER
                        .DataBind()
                    End With

                    If Me.ValorSeleccionadoCombo > 0 Then
                        Me.ddlSectorYTaller.SelectedValue = Me.ValorSeleccionadoCombo
                        Buscar(ObtenerCondicionBusquedaCombo, String.Format("{0} {1} , {2} {3}", Modelo.V_OT_REVISION_SUPERVISOR.ES_EMERGENCIA, Ordenamiento.DESCENDENTE, Modelo.V_OT_REVISION_SUPERVISOR.FECHA_HORA_SOLICITA, Ordenamiento.DESCENDENTE))
                    End If

                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ningun taller o sector en supervisión.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ningun taller o sector en supervisión.','../../Genericos/Frm_MenuPrincipal.aspx');")
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' construye la condicion de busqueda
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusquedaCombo() As String
        Dim vlc_Condicion As String = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtNumOrden.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_REVISION_SUPERVISOR.NUMERO_ORDEN, Me.txtNumOrden.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_REVISION_SUPERVISOR.NUMERO_ORDEN, Me.txtNumOrden.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlEdificio.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OT_REVISION_SUPERVISOR.ID_LUGAR_TRABAJO, Me.ddlEdificio.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OT_REVISION_SUPERVISOR.ID_LUGAR_TRABAJO, Me.ddlEdificio.SelectedValue)
            End If
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_REVISION_SUPERVISOR.ID_SECTOR_TALLER, Me.ddlSectorYTaller.SelectedValue)
        Else
            vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_REVISION_SUPERVISOR.ID_SECTOR_TALLER, Me.ddlSectorYTaller.SelectedValue)
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_REVISION_SUPERVISOR.ID_UBICACION, Me.AutorizadoUbicacion.IdUbicacionAdministra)
        Else
            vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_REVISION_SUPERVISOR.ID_UBICACION, Me.AutorizadoUbicacion.IdUbicacionAdministra)
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlEstado.SelectedValue) Then
            vlc_Condicion = String.Format("{0} AND {1} = '{2}' ", vlc_Condicion, Modelo.V_OT_REVISION_SUPERVISOR.ESTADO_ORDEN_TRABAJO, Me.ddlEstado.SelectedValue)
        Else
            vlc_Condicion = String.Format("{4} AND ({0} = '{1}' OR {0} = '{2}' OR {0} = '{3}' OR {0} ='{5}')", Modelo.V_OT_REVISION_SUPERVISOR.ESTADO_ORDEN_TRABAJO, EstadoOrden.NO_CONFORME, EstadoOrden.PARA_EVALUACION_SOLICITANTE, EstadoOrden.ASIGNADA, vlc_Condicion, EstadoOrden.EN_PROCESO)
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2015</creationDate>
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
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/07/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionario(pvn_IdPersonal As String) As WsrEU_Curriculo.EntEmpleados
        Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("ID_PERSONAL = '{0}'", pvn_IdPersonal))
        Catch ex As Exception
            Throw
        Finally
            If vlo_WsEU_Curriculo IsNot Nothing Then
                vlo_WsEU_Curriculo.Dispose()
            End If
        End Try
    End Function


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

    ''' <summary>
    ''' carga las categorias de servicio del usuario en sesion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>13/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarCategoriasServicio(pvn_NumEmpleado As Integer) As Data.DataSet
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_SERVICIO.NUM_EMPLEADO_SUPERVISOR, pvn_NumEmpleado),
                String.Empty,
                False,
                0,
                0)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga las actividades segun la condicion de busqueda
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>13/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarActividades(pvc_Condicion As String) As Data.DataSet
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_Catalogos.OTM_ACTIVIDAD_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                String.Empty,
                False,
                0,
                0)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga los sectores talleres segun la condicion que obtiene por parametro
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarSectoresTalleres(pvc_Condicion As String) As Data.DataSet
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_SECTOR_TALLER_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                String.Empty,
                False,
                0,
                0)
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga parametros del sistema
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarParametro(pvn_IdParametro As Integer, pvn_IdUbicacion As Integer) As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ObtenerRegistro(
                 ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, pvn_IdParametro, Modelo.OTP_PARAMETRO_UBICACION.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion))

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
