Imports Wsr_GN_GestorNotificaciones
Imports System.Data
Imports Utilerias.OrdenesDeTrabajo

Partial Class Notificaciones_Lst_OT_ConsultaCorreos
    Inherits System.Web.UI.Page
#Region "Propiedades"
    Private Property FiltrarPorFecha As Boolean
        Get
            Return CType(ViewState("FiltrarPorFecha"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("FiltrarPorFecha") = value
        End Set
    End Property

    Private Property EntGNM_SISTEMA As EntGNM_SISTEMA
        Get
            Return CType(ViewState("EntGNM_SISTEMA"), EntGNM_SISTEMA)
        End Get
        Set(value As EntGNM_SISTEMA)
            ViewState("EntGNM_SISTEMA") = value
        End Set
    End Property

    Private Property UsuarioActualConectado As UsuarioActual
        Get
            Return CType(ViewState("UsuarioActualConectado"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("UsuarioActualConectado") = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad para la ultima condicion de busqueda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    Public Property NuevaSeccion As String
        Get
            Return CType(ViewState("NuevaSeccion"), String)
        End Get
        Set(ByVal value As String)
            ViewState("NuevaSeccion") = value
        End Set
    End Property

    Public Property CargarUltimaPaginaSeleccionada As Boolean
        Get
            Return CType(ViewState("CargarUltimaPaginaSeleccionada"), Boolean)
        End Get
        Set(ByVal value As Boolean)
            ViewState("CargarUltimaPaginaSeleccionada") = value
        End Set
    End Property

#End Region

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try

                
                UsuarioActualConectado = New UsuarioActual()
                Me.FiltrarPorFecha = False
                Me.arTitulo.Visible = False
                Me.arListado.Visible = False
                Me.arPaginador.Visible = False
                Me.EntGNM_SISTEMA = New EntGNM_SISTEMA

                If Me.txtCuentaCorreo.Text <> String.Empty Or Me.txtFecha.Text <> String.Empty Then
                    Buscar(String.Empty, String.Empty)
                End If

            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If

        Me.pnRpCorreos.Dibujar()

    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        Try
            Session("pvcc_sCuentaCorreo") = Me.txtCuentaCorreo.Text.Trim
            Session("pvcc_sAsunto") = Me.txtAsunto.Text.Trim
            Session("pvcc_sFecha") = Me.txtFecha.Text.Trim

            Session("pvcc_rCondicion") = "1"

            Buscar(String.Empty, String.Empty)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub rpCorreos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpCorreos.ItemDataBound
        Dim vlo_IbBorrar As ImageButton

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_IbBorrar = e.Item.FindControl("ibConsultar")
            If vlo_IbBorrar IsNot Nothing Then
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub

    Protected Sub ibConsultar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llaves() As String

        Try
            vlc_Llaves = Split(CType(CType(sender, ImageButton).CommandArgument, String), ",")

            Session.Add("pvn_IdNotificacion", vlc_Llaves(0))
            Session.Add("pvc_Origen", vlc_Llaves(1))

            Response.Redirect("Frm_OT_VerCorreos.aspx", False)

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


    Protected Sub pnRpUbicaciones_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpCorreos.CambioDePagina
        Try
            CargarLista(String.Empty, String.Empty, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub lnkRpUbicaciones_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(String.Empty, String.Empty, pnRpCorreos.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub
#End Region

#Region "Metodos"

    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de reconstruir los filtros utilizados si se esta accediendo al listado (Recuperación de criterios de búsqueda y ordenamiento )
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarFiltrosEnSeccion()
        'Criterio de búsqueda

        Me.txtCuentaCorreo.Text = Session("pvcc_sCuentaCorreo").ToString()
        Me.txtAsunto.Text = Session("pvcc_sAsunto").ToString()
        Me.txtFecha.Text = Session("pvcc_sFecha").ToString()


        'Cargar Ultimo número de página seleccionado
        Me.CargarUltimaPaginaSeleccionada = IIf(Session("pvcn_sUltimaSeleccionPaginaActualLista" + Me.GetType().Name) Is Nothing, False, True)

        'Criterio de Ordenamiento
        UltimoSortExpression = IIf(Session("pvcc_sUltimoSortExpression" + Me.GetType().Name) Is Nothing, String.Empty, Session("pvcc_sUltimoSortExpression" + Me.GetType().Name).ToString())

    End Sub

    ''' <summary>
    ''' Guarda el orden y la ultima pagina seleccionada en variables de seccion
    ''' </summary>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    Private Sub GuardarOrdenyUltimaPaginaSeleccionada(pvc_Orden As String)
        Session("pvcc_sUltimoSortExpression" + Me.GetType().Name) = pvc_Orden
        Session("pvcn_sUltimaSeleccionPaginaActualLista" + Me.GetType().Name) = Me.pnRpCorreos.PaginaActualLista
        Session("pvcn_sUltimaSeleccionPaginaActualPaginador" + Me.GetType().Name) = Me.pnRpCorreos.PaginaActualPaginadorNumerico
    End Sub

    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_wsGestorNotificaciones As wsGestorNotificaciones
        Dim vlo_EntDatosPaginacion As EntDatosPaginacion

        Try

            'If String.IsNullOrWhiteSpace(pvc_Orden) Then
            '    pvc_Orden = String.Format("{0} {1}", Utilerias.OrdenesDeTrabajo.Modelo.GNT_NOTIFICACION.ID_NOTIFICACION, Ordenamiento.DESCENDENTE)
            'End If

            UltimoSortExpression = pvc_Orden

            vlo_wsGestorNotificaciones = New wsGestorNotificaciones
            vlo_wsGestorNotificaciones.Timeout = -1
            vlo_wsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials

            vlo_EntDatosPaginacion = New EntDatosPaginacion

            If Me.EntGNM_SISTEMA.Existe = False Then
                Me.EntGNM_SISTEMA = vlo_wsGestorNotificaciones.GNM_SISTEMA_ObtenerPorNombre(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_EN_GESTOR_DE_NOTIFICACIONES))
            End If

            If Me.EntGNM_SISTEMA.Existe Then
                Me.FiltrarPorFecha = Me.txtFecha.Text <> String.Empty

                vlo_EntDatosPaginacion = vlo_wsGestorNotificaciones.GNT_NOTIFICACION_ObtenerDatosPaginacionFnGnListarnotificaciones(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Empty,
                pvc_Orden,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer),
                Me.EntGNM_SISTEMA.ID_SISTEMA,
                Me.txtAsunto.Text.Trim,
                Me.FiltrarPorFecha,
                IIf(Me.FiltrarPorFecha, Me.txtFecha.Text, Utilerias.OrdenesDeTrabajo.Constantes.fechaNula),
                Me.txtCuentaCorreo.Text.Trim
                )



            End If

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                lblCantidadRegistro.Visible = True
                lblCantidadRegistro.Text = String.Format("Cantidad de notificaciones: {0}", vlo_EntDatosPaginacion.TotalRegistros)
                CargarLista(pvc_Condicion, pvc_Orden, ObtenerNumeroDePagina()) '1 xq la lista siempre carga en esa posicion
                Me.arTitulo.Visible = True
                Me.arListado.Visible = True
                Me.arPaginador.Visible = True
                Me.pnRpCorreos.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                Me.pnRpCorreos.Dibujar()
            Else
                Me.lblCantidadRegistro.Visible = False
                Me.lblCantidadRegistro.Text = String.Empty
                Me.arTitulo.Visible = False
                Me.arListado.Visible = False
                Me.arPaginador.Visible = False
                MostrarAlertaNoHayDatos()
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_wsGestorNotificaciones IsNot Nothing Then
                vlo_wsGestorNotificaciones.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_wsGestorNotificaciones As wsGestorNotificaciones
        Dim vlo_DsDatos As DataSet

        vlo_wsGestorNotificaciones = New wsGestorNotificaciones
        vlo_wsGestorNotificaciones.Timeout = -1
        vlo_wsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            GuardarOrdenyUltimaPaginaSeleccionada(pvc_Orden)

            vlo_DsDatos = vlo_wsGestorNotificaciones.GNT_NOTIFICACION_ListarFnGnListarnotificaciones(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), String.Empty, pvc_Orden,
                                                                                     True, pvn_NumeroDePagina, CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer), Me.EntGNM_SISTEMA.ID_SISTEMA,
                                                                                    Me.txtAsunto.Text.Trim, Me.FiltrarPorFecha, IIf(Me.FiltrarPorFecha, Me.txtFecha.Text, Utilerias.OrdenesDeTrabajo.Constantes.fechaNula), Me.txtCuentaCorreo.Text.Trim)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpCorreos
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()

                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado();")
            Else
                With Me.rpCorreos
                    .DataSource = Nothing
                    .DataBind()
                End With
                MostrarAlertaNoHayDatos()
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_wsGestorNotificaciones IsNot Nothing Then
                vlo_wsGestorNotificaciones.Dispose()
            End If
            If vlo_DsDatos IsNot Nothing Then
                vlo_DsDatos.Dispose()

            End If
        End Try
    End Sub

#End Region

#Region "Funciones"
    ''' <summary>
    ''' Obtiene el numero de pagina a mostrar, dependiendo si tiene que recuperar la ultima pagina seleccionada o simplemente mostrar la primera
    ''' </summary>
    ''' <returns>El numero de pagina a mostrar</returns>
    ''' <remarks></remarks>
    Private Function ObtenerNumeroDePagina() As Integer
        If Me.CargarUltimaPaginaSeleccionada Then
            '1.Se cambia la bandera y se carga de la seccion la ultima Pagina seleccionada 
            Me.CargarUltimaPaginaSeleccionada = False
            Me.pnRpCorreos.PaginaActualPaginadorNumerico = CType(Session("pvcn_sUltimaSeleccionPaginaActualPaginador" + Me.GetType().Name), Integer)
            Me.pnRpCorreos.PaginaActualLista = CType(Session("pvcn_sUltimaSeleccionPaginaActualLista" + Me.GetType().Name), Integer)
            Return Me.pnRpCorreos.PaginaActualLista
        Else
            '2.Si no es necesario cargar la ultima pagina seleccionada, se carga la primera
            Return 1
        End If
    End Function
#End Region
End Class
