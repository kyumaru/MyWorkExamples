Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_SDP_ReportServer
Imports Utilerias.GeneradorDeReportes

''' <summary>
''' Maneja el comportamiento de la página de gestion por profesionales de diseño
''' </summary>
''' <remarks></remarks>
''' <author>César Bermudez Garcia</author>
''' <creationDate>09/02/2016</creationDate>
''' <changeLog></changeLog>
Partial Class OrdenesDeTrabajo_Lst_OT_GestionProfesionalesDisenio
    Inherits System.Web.UI.Page

#Region "Propiedades"

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


    ''' <summary>
    ''' Propiedad para la ultima condicion de busqueda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>09/02/2016</creationDate>
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
    ''' <creationDate>09/02/2016</creationDate>
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
    ''' <creationDate>09/02/2016</creationDate>
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
    ''' <creationDate>09/02/2016</creationDate>
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
    ''' ubicacion del usuario
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
    ''' <creationDate>09/02/2016</creationDate>
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
    ''' Se ejecuta para mostrar la trazabilidad con los parámetros especificados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>09/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkTrazabilidad_Command(sender As Object, e As CommandEventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = e.CommandArgument.Split("¬")

            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvc_PantallaRetorno", "OrdenesDeTrabajo/Lst_OT_GestionProfesionalesDisenio.aspx")
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("../Frm_OT_ConsultaTrazabilidad.aspx", False)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>7/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpOrdenTrabajo_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpOrdenTrabajo.ItemDataBound
        Dim vlo_btnAnteproyecto As ImageButton
        Dim vlo_ibOrdenHija As ImageButton
        Dim vlo_hdtEstado As HiddenField
        Dim vlo_hdfParentesco As HiddenField
        Dim vlo_HiddenFieldIdUbicacion As HiddenField
        Dim vlo_HiddenFieldIdOrdenTrabajo As HiddenField
        Dim vlo_ImageButtonOrdenHija As ImageButton
        Dim vlc_CantidaHijas As String

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("imgOrdenHija") IsNot Nothing Then
                vlo_HiddenFieldIdUbicacion = CType(e.Item.FindControl("hdfIdUbicacion"), HiddenField)
                vlo_HiddenFieldIdOrdenTrabajo = CType(e.Item.FindControl("hdfIdOrdenTrabajo"), HiddenField)
                vlo_ImageButtonOrdenHija = CType(e.Item.FindControl("imgOrdenHija"), ImageButton)
                vlc_CantidaHijas = ConsultarCantidadOrdenesHijas(vlo_HiddenFieldIdUbicacion.Value, vlo_HiddenFieldIdOrdenTrabajo.Value)
                vlo_ImageButtonOrdenHija.ToolTip = "Agregar Orden Hija / Cantidad de Hijas: " + vlc_CantidaHijas
                vlo_ImageButtonOrdenHija.AlternateText = "Agregar Orden Hija / Cantidad de Hijas: " + vlc_CantidaHijas
            End If
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("btnAnteproyecto") IsNot Nothing Then
                vlo_hdtEstado = CType(e.Item.FindControl("hdfEstado"), HiddenField)
                vlo_hdfParentesco = CType(e.Item.FindControl("hdfParentesco"), HiddenField)
                vlo_btnAnteproyecto = CType(e.Item.FindControl("btnAnteproyecto"), ImageButton)

                If vlo_hdtEstado.Value = EstadoOrden.EN_ANTEPROYECTO Or
                    vlo_hdtEstado.Value = EstadoOrden.ANTEPROYECTO_DEVUELTO_SOLICITANTE Or
                    vlo_hdtEstado.Value = EstadoOrden.ELABORACION_DE_PLANOS Or
                    vlo_hdtEstado.Value = EstadoOrden.ELABORACION_PRESUPUESTO Or
                    vlo_hdtEstado.Value = EstadoOrden.PRESUPUESTO_APROBADO_COORDINADOR Or
                    vlo_hdtEstado.Value = EstadoOrden.PRESUPUESTO_APROBADO_JEFATURA Or
                    vlo_hdtEstado.Value = EstadoOrden.PRESUPUESTO_APROBADO_SOLICITANTE Or
                    vlo_hdtEstado.Value = EstadoOrden.PRESUPUESTO_DEVUELTO_COORDINADOR Or
                    vlo_hdtEstado.Value = EstadoOrden.PRESUPUESTO_DEVUELTO_JEFATURA Or
                    vlo_hdtEstado.Value = EstadoOrden.PRESUPUESTO_PENDIENTE_RESPUESTA_SOLICITANTE Or
                    vlo_hdtEstado.Value = EstadoOrden.PRESUPUESTO_REVISION_COORDINADOR Or
                    vlo_hdtEstado.Value = EstadoOrden.PRESUPUESTO_REVISION_JEFATURA Then

                    vlo_btnAnteproyecto.Visible = True
                Else
                    vlo_btnAnteproyecto.Visible = False

                End If

            End If

            If e.Item.FindControl("imgOrdenHija") IsNot Nothing Then
                vlo_hdtEstado = CType(e.Item.FindControl("hdfEstado"), HiddenField)
                vlo_hdfParentesco = CType(e.Item.FindControl("hdfParentesco"), HiddenField)
                vlo_ibOrdenHija = CType(e.Item.FindControl("imgOrdenHija"), ImageButton)

                If vlo_hdfParentesco.Value <> "HIJ" Then
                    vlo_ibOrdenHija.Visible = True
                Else
                    vlo_ibOrdenHija.Visible = False
                End If
            End If
        End If

    End Sub
    ''' <summary>
    ''' Evento para mostrar información de la OT
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkNumOt_Click(sender As Object, e As EventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, LinkButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_Operacion", eOperacion.Consultar)
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Regresar", "Lst_OT_GestionProfesionalesDisenio.aspx")
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)

            Response.Redirect(String.Format("Frm_OT_OrdenTrabajo.aspx"), False)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados de la 
    ''' tabla del listado, carga la lista de registros en orden relacionado a la columna seleccionada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez</author>
    ''' <creationDate>10/02/2016</creationDate>
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
    ''' 
    ''' </summary>
    ''' <param name="pvn_PaginaSeleccionada"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>12/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub pnRpOrdenTrabajo_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpOrdenTrabajo.CambioDePagina
        Try
            Paginador = pvn_PaginaSeleccionada
            CargarLista(ObtenerCondicionBusqueda, UltimoSortExpression, pvn_PaginaSeleccionada)
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
    ''' <author>César Bermúdez</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Me.Paginador = 0
            Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)
            WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Redirige a otra página para asignar profesionales 
    ''' Para acompañamientos en la valoracion de la viabilidad técnica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnRH_Click(sender As Object, e As EventArgs)
        Dim vlc_Llave As String()
        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_Anno", vlc_Llave(2))
            Me.Session.Add("pvn_IdSectorTaller", vlc_Llave(3))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)
            CargarEncargado(vlc_Llave(0), vlc_Llave(1), vlc_Llave(3))
            Response.Redirect(String.Format("Frm_OT_AsignacionProfViabilidadTecnica.aspx"), False)

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
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnVT_Click(sender As Object, e As EventArgs)
        Dim vlc_Llave As String()
        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_Anno", vlc_Llave(2))
            Me.Session.Add("pvn_IdSectorTaller", vlc_Llave(3))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)
            CargarEncargado(vlc_Llave(0), vlc_Llave(1), vlc_Llave(3))
            Response.Redirect(String.Format("Frm_OT_AnalisisViabilidadTecnica.aspx"), False)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Redirige a la pantalla de Ante Proyecto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAnteproyecto_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()
        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_IdSectorTaller", vlc_Llave(2))
            Me.Session.Add("pvn_Anno", vlc_Llave(3))

            If (vlc_Llave(4) <> EstadoOrden.EN_ANTEPROYECTO) And (vlc_Llave(4) <> EstadoOrden.ANTEPROYECTO_DEVUELTO_SOLICITANTE) Then
                Me.Session.Add("pvn_Operacion", eOperacion.Consultar)
                Me.Session.Add("pvn_Regresar", "Lst_OT_GestionProfesionalesDisenio.aspx")
            End If

            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)
            CargarEncargado(vlc_Llave(0), vlc_Llave(1), vlc_Llave(2))
            Response.Redirect(String.Format("Frm_OT_Anteproyecto.aspx"), False)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Redirecciona al listado de creaciopn de ordenes de trabajo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibOrdenHija_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("Lst_OT_OrdenTrabajoHijaProfesional.aspx", False)
    End Sub

    ''' <summary>
    ''' Redirecciona al formulario de expediente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibExpediente_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_Anno", vlc_Llave(2))
            Me.Session.Add("pvn_Regresar", "Lst_OT_GestionProfesionalesDisenio.aspx")
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("Frm_OT_Expediente.aspx", False)
    End Sub

    ''' <summary>
    ''' Redirecciona al formulario de elaboración de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub imgElaboracionPresupuesto_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_Anno", vlc_Llave(2))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("Frm_OT_ElaboracionPresupuesto.aspx", False)
    End Sub

    ''' <summary>
    ''' Manda a llamar al formulario de planos para la elaboracion de esta etapa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub imgPlanos_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave() As String
        Try

            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_IdSectorTaller", vlc_Llave(2))
            Me.Session.Add("pvn_Anno", vlc_Llave(3))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)
            CargarEncargado(vlc_Llave(0), vlc_Llave(1), vlc_Llave(2))
            Response.Redirect("Frm_OT_Planos.aspx", False)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try

    End Sub

    ''' <summary>
    ''' Manda a llamar al formulario que finalizará la entrega del diseño
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub imgbtnEntrega_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave() As String
        Try

            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_IdSectorTaller", vlc_Llave(2))
            Me.Session.Add("pvn_Anno", vlc_Llave(3))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)
            Response.Redirect("Frm_OT_FinalizacionEntregaDis.aspx", False)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Redirige a otra página para realizar la gestion de la contratacion del proyecto por parte del profesional 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGestionProfesional_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()
        Try
           


        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre la figura de asignar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibImprimir_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_EntParametroReporte As EntParametroReporte
        Dim vlo_ListaEntParametroReporte As New List(Of EntParametroReporte)
        Dim vlo_Empleado As New WsrEU_Curriculo.EntEmpleados
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("_")

            'configuracion de los reporte
            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Usuario"
            vlo_EntParametroReporte.Valor = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB)
            vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

            'configuracion de los reporte
            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Clave"
            vlo_EntParametroReporte.Valor = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB)
            vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

            'configuracion de los reporte
            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Condicion"
            vlo_EntParametroReporte.Valor = String.Format("{0} = {1} AND {2} = '{3}'", Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_UBICACION, vlc_Llave(0), Utilerias.OrdenesDeTrabajo.Modelo.V_OT_GESTION_SECTOR_TALLER.ID_ORDEN_TRABAJO, vlc_Llave(1))
            vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Orden"
            vlo_EntParametroReporte.Valor = " "
            'Utilerias.OrdenesDeTrabajo.Ordenamiento.ASCENDENTE
            vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

            Me.Session.Add("pvo_ListaEntParametroReporte", vlo_ListaEntParametroReporte)

            vlo_Empleado = CargarFuncionario(Me.Usuario.UserName)
            'configuracion de los reporte
            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_UsuarioEjecuta"
            vlo_EntParametroReporte.Valor = String.Format("{0} {1} {2}", vlo_Empleado.NOMBRE, vlo_Empleado.APELLIDO1, vlo_Empleado.APELLIDO2)
            vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

            ScriptManager.RegisterStartupScript(Me, GetType(String), "redirect", String.Format("window.open('../../Controles/Frm_OT_ManejoReportes.aspx?pvc_RutaBase={0}&pvc_NombreReporte={1}&pvc_FormatoReporte={2}', 'ticker', 'toolbar=no,menubar=no,location=no, scrollbars=YES,scroll=YES');", Utilerias.OrdenesDeTrabajo.Reportes.RUTA_BASE, Utilerias.OrdenesDeTrabajo.Reportes.RPT_OT_FICHA_TECNICA, FORMATO_REPORTE.PDF), True)

            Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)

        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inicializa el listado y envia a buscar con los valores especificados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>09/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarListado()
        Try

            If Roles.IsUserInRole(Membership.GetUser.UserName, RolesSistema.OT_PROFESIONAL_DISENIO) Then
                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If Me.AutorizadoUbicacion.Existe Then
                    LeerParametrosSession()
                    Buscar(ObtenerCondicionBusqueda, String.Empty)

                    configurarVisualizacionBotones()
                    CargarListaEstados()

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
    ''' <creationDate>02/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametrosSession()

        Try

            Me.NuevaSeccion = WebUtils.LeerParametro(Of Integer)("pvn_NuevaSeccion")

            If Me.NuevaSeccion <> 1 Then
                Me.Paginador = WebUtils.LeerParametro(Of Integer)("pvn_Paginador")
                Me.UltimaCondicionBusqueda = WebUtils.LeerParametro(Of String)("pvc_UltimaCondicionBusqueda")
            Else
                Me.Paginador = 0
                Me.UltimaCondicionBusqueda = String.Empty
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de realizar la busqueda de registros segun los datos de 
    ''' condicion y orden de búsqueda
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If String.IsNullOrEmpty(pvc_Orden) Then
                'Las ordenes de trabajo con cualquiera de los siguientes estados se deben mostrar de ultimas en el listado y 
                'únicamente se mostrara el botón para Consulta: 
                'ERC	Evaluación Preliminar Revisión Coordinador 
                'EAC	Evaluación Preliminar Aprobada por Coordinador
                'ERJ	Evaluación Preliminar Revisión Jefatura
                'EAJ	Evaluación Preliminar Aprobada Jefatura
                'EDJ	Evaluación Preliminar Devuelta por Jefatura

                '{0}: Nombre de la columna del estado de la orden
                '{1}: EPE: Evaluación Preliminar Pendiente 
                '{2}: EPV: Evaluación Preliminar Evaluación
                '{3}: EDC: Evaluación Preliminar Devuelta por Coordinador.

                pvc_Orden = String.Format("CASE WHEN {0} = '{1}' THEN 1 WHEN {0} = '{2}' THEN 2 WHEN {0} = '{3}' THEN 3 ELSE 5 END, {4} {5}",
                                          Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO,
                                          EstadoOrden.EVALUACION_PRELIMINAR_PENDIENTE,
                                          EstadoOrden.EVALUACION_PRELIMINAR_EVALUACION,
                                          EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_COORD,
                                          Modelo.V_OTT_ORDEN_TRABAJOLST.CONSECUTIVO, Ordenamiento.DESCENDENTE)
            End If

            vlo_EntDatosPaginacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerDatosPaginacionVOttOrdenTrabajolst(
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                     pvc_Condicion,
                     pvc_Orden,
                     CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpOrdenTrabajo.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas

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
    ''' Ejecuta eljavascript para mostrar una alerta cuando no existen datos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "mostrarAlertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    ''' <summary>
    ''' Ejecuta un llamado para obtener estados de orden de trabajo desde la base de datos y cargar la lista de filtros con ella.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/02/2016</creationDate>
    ''' <change>Se agregan estados para anteproyecto</change>
    ''' </changeLog>
    Private Sub CargarListaEstados()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlfiltroEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
            '{0}: Columna ESTADO_ORDEN_TRABAJO
            '{1}: Evaluación Preliminar Pendiente
            '{2}: Evaluación Preliminar Evaluación
            '{3}: Evaluación Preliminar Revisión Coordinador
            '{4}: Evaluación Preliminar Devuelta por Coordinador
            '{5}: Evaluación Preliminar Aprobada por Coordinador
            '{6}: Evaluación Preliminar Revisión Jefatura
            '{7}: Evaluación Preliminar Aprobada Jefatura
            '{8}: Evaluación Preliminar Devuelta por Jefatura
            '{9}: Pendiente Respuesta del Solicitante
            '{10}: En Anteproyecto 
            '{11}: Anteproyecto Pendiente Revisión del solicitante
            '{12}: Anteproyecto Devuelto por el solicitante
            '{13}: Anteproyecto Aprobado por el solicitante


            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTC_ESTADO_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}' OR {0} = '{2}' OR {0} = '{3}' OR {0} = '{4}' OR {0} = '{5}' OR {0} = '{6}' OR {0} = '{7}' OR {0} = '{8}' OR {0} = '{9}' OR {0} = '{10}' OR {0} = '{11}' OR {0} = '{12}' OR {0} = '{13}' OR {0} = '{14}' OR {0} = '{15}'",
                        Modelo.OTC_ESTADO_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO, EstadoOrden.EVALUACION_PRELIMINAR_PENDIENTE,
                        EstadoOrden.EVALUACION_PRELIMINAR_EVALUACION, EstadoOrden.EVALUACION_PRELIMINAR_REVISION_COORD, EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_COORD,
                        EstadoOrden.EVALUACION_PRELIMINAR_APROBADA_COORD, EstadoOrden.EVALUACION_PRELIMINAR_REVISION_JEFATURA, EstadoOrden.EVALUACION_PRELIMINAR_APROBADA_JEFATURA,
                        EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_JEFATURA, EstadoOrden.PENDIENTE_RESPUESTA_SOLICITANTE, EstadoOrden.EN_ANTEPROYECTO,
                        EstadoOrden.ANTEPROYECTO_PENDIENTE_REVISION_SOLICITANTE, EstadoOrden.ANTEPROYECTO_DEVUELTO_SOLICITANTE,
                        EstadoOrden.ANTEPROYECTO_APROBADO_SOLICITANTE, EstadoOrden.ELABORACION_PRESUPUESTO, EstadoOrden.PRESUPUESTO_DEVUELTO_COORDINADOR
                        ), String.Empty, False, 1, 0)


            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlfiltroEstado
                    .DataSource = vlo_DsDatos
                    .DataValueField = Modelo.OTC_ESTADO_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO
                    .DataTextField = Modelo.OTC_ESTADO_ORDEN_TRABAJO.DESCRIPCION
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
    ''' <creationDate>10/02/2016</creationDate>
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
    ''' <creationDate>10/02/2016</creationDate>
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
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'",
                              Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, pvn_idUbicacion,
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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub configurarVisualizacionBotones()

    End Sub
#End Region

#Region "Funciones"

    ''' <summary>
    ''' carga la ubicacion 
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>09/02/2016</creationDate>
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
    ''' construye la condicion de busqueda
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String = String.Empty

        'Se listarán todas las ordenes de trabajo de diseño, donde el usuario en sesión sea encargado, este vigente para el proyecto y cuyo estado sea:
        'EPE: Evaluación Preliminar Pendiente 
        'EPV: Evaluación Preliminar Evaluación y EDC: Evaluación Preliminar Devuelta por Coordinador.

        '{0}: Columna del numero profesional encargado de la orden
        '{1}: Numero de empleado actualmente en el sistema
        '{2}: Columna para verificar si posee ficha técnica
        '{3}: Constante para ordenes de trabajo de diseño
        '{4}: Columna para validar que está en uno de los estados propuestos
        '{5}: es boleano asi que se envia un uno.
        '{6}: año actual
        '{7}: mes actual
        '{8}: dia actual
        '{9}: Constante del formato de fecha para la base de datos
        '{10}: columna de fecha desde
        '{11}: columna de fecha hasta


        vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND TO_DATE('{6}/{7}/{8}','{9}') BETWEEN {10} AND {11}",
                                         Modelo.V_OTT_ORDEN_TRABAJOLST.NUM_PROF_ENCARGADO,
                                         Me.Usuario.NumEmpleado,
                                         Modelo.V_OTT_ORDEN_TRABAJOLST.POSEE_FICHA_TECNICA,
                                         FichaTecnica.REQUIERE_FICHA_TECNICA,
                                         Modelo.V_OTT_ORDEN_TRABAJOLST.VIABILIDAD_TECNICA, 1,
                                         DateTime.Now.Year,
                                         DateTime.Now.Month,
                                         DateTime.Now.Day,
                                         Constantes.FORMATO_FECHA_BD,
                                         Modelo.V_OTT_ORDEN_TRABAJOLST.FECHA_DESDE,
                                         Modelo.V_OTT_ORDEN_TRABAJOLST.FECHA_HASTA)

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroNumOrden.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO, Me.txtFiltroNumOrden.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO, Me.txtFiltroNumOrden.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroNombreProyecto.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OTT_ORDEN_TRABAJOLST.NOMBRE_PROYECTO, Me.txtFiltroNombreProyecto.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OTT_ORDEN_TRABAJOLST.NOMBRE_PROYECTO, Me.txtFiltroNombreProyecto.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroFecha.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OTT_ORDEN_TRABAJOLST.FECHA_HORA_SOLICITA, Me.txtFiltroFecha.Text)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTT_ORDEN_TRABAJOLST.FECHA_HORA_SOLICITA, Me.txtFiltroFecha.Text)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlfiltroEstado.SelectedValue) Then
            If Not String.IsNullOrWhiteSpace(Me.ddlfiltroEstado.SelectedValue) Then
                vlc_Condicion = String.Format("{0} = '{1}' ", Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO, Me.ddlfiltroEstado.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}')", vlc_Condicion, Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO, Me.ddlfiltroEstado.SelectedValue)
            End If
        End If

        UltimaCondicionBusqueda = vlc_Condicion

        Return vlc_Condicion
    End Function

    ''' <summary>
    ''' funcion encargada de realizar los oredenamientos segun los valores brindados
    ''' </summary>
    ''' <param name="pvc_Columna"> numero de la columna para el ordenamiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
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
    ''' Retorna la cantidad de OTs hijas de una OT madre
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConsultarCantidadOrdenesHijas(pvn_IdUbicacion As String, pvc_IdOrdenTrabajo As String) As String
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION_MADRE, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO_MADRE, pvc_IdOrdenTrabajo),
                String.Empty,
                False,
                0, 0)

            Return CType(vlo_DsDatos.Tables(0).Rows.Count, String)
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/09/2016</creationDate>
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

#End Region

End Class
