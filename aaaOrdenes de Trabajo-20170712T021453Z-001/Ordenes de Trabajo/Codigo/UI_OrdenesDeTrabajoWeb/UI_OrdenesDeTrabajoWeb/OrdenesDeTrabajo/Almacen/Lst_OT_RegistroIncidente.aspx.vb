Imports Utilerias.OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Almacen_Lst_OT_RegistroIncidente
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
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
    ''' <creationDate>05/08/2016</creationDate>
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
    ''' <creationDate>05/08/2016</creationDate>
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
    ''' <creationDate>05/08/2016</creationDate>
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
    ''' <creationDate>05/08/2016</creationDate>
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                If Roles.IsUserInRole(Membership.GetUser.UserName, RolesSistema.OT_ENCARGADO_ALMACEN) Then
                    Me.Usuario = New UsuarioActual
                    Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                    If Me.AutorizadoUbicacion.Existe Then

                        Buscar(ObtenerCondicionBusqueda(), String.Empty)
                        CargarTipoIncidente()

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
        End If
        Me.pnRpIncidentes.Dibujar()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados de la tabla del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpIncidentes_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerCondicionBusqueda, ObtenerExpresionDeOrdenamiento(e.CommandName), Me.pnRpIncidentes.PaginaActualLista)
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
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub pnRpIncidentes_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpIncidentes.CambioDePagina
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
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpIncidentes_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpIncidentes.ItemDataBound
        Dim vlo_IbBorrar As ImageButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
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
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)

        Try
            If Borrar(CType(sender, ImageButton).CommandArgument) Then
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

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el botón Buscar,
    ''' que se encuentra en el área de filtros.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaRegistroBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroBorrado", "mostrarAlertaRegistroBorrado();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaRegistroNoBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroNoBorrado", "mostrarAlertaRegistroNoBorrado();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ' ''' <summary>
    ' ''' carga el combo de estados
    ' ''' </summary>
    ' ''' <remarks></remarks>
    ' ''' <author>Carlos Gómez Ondoy</author>
    ' ''' <creationDate>04/08/2016</creationDate>
    ' ''' <changeLog></changeLog>
    'Private Sub CargarEstado() 'CondicionUbicacion

    '    Me.ddlFiltroEstado.Items.Clear()
    '    Me.ddlFiltroEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
    '    Me.ddlFiltroEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
    '    Me.ddlFiltroEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
    'End Sub

    ''' <summary>
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvc_Condicion">parámetro con la condicion de búsqueda</param>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTF_INCIDENTE_ALMACEN_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpIncidentes
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End With
            Else
                With Me.rpIncidentes
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

    ''' <summary>
    ''' Método encargado de realizar la busqueda de registros segun los datos de 
    ''' condicion y orden de búsqueda
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerExpresionDeOrdenamiento(Modelo.V_OTF_INCIDENTE_ALMACENLST.FECHA_INCLUSION)
        End If

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntDatosPaginacion =
                vlo_Ws_OT_OrdenesDeTrabajo.OTF_INCIDENTE_ALMACEN_ObtenerDatosPaginacionVOtfIncidenteAlmacenlst(
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                     pvc_Condicion,
                     pvc_Orden,
                     CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpIncidentes.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                CargarLista(pvc_Condicion, pvc_Orden, 1)
                Me.pnRpIncidentes.Dibujar()
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de registros {0}", vlo_EntDatosPaginacion.TotalRegistros)
            Else
                Me.lblCantidadDeRegistros.Visible = False
                Me.lblCantidadDeRegistros.Text = String.Empty
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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarTipoIncidente()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlTipo.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_TIPO_INCIDENTE_ListarRegistros(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} LIKE '%{1}%'", Modelo.V_OTM_LUGAR_TRABAJO.ESTADO, Estado.ACTIVO),
               String.Empty,
               False,
               0,
               0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlTipo
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_TIPO_INCIDENTE.DESCRIPCION
                    .DataValueField = Modelo.V_OTM_TIPO_INCIDENTE.ID_TIPO_INCIDENTE
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
    ''' Función encargada de comunicarse con el  servicio web y proceder a borrar el registro
    ''' </summary>
    ''' <param name="pvn_IdIncidenteAlmacen"></param>
    ''' <returns>Si retorna un número mayor a 0 quiere decir que la operacion se realizo con éxito</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Borrar(pvn_IdIncidenteAlmacen As Integer) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOtfIncidenteAlmacen As Wsr_OT_OrdenesDeTrabajo.EntOtfIncidenteAlmacen

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtfIncidenteAlmacen = New Wsr_OT_OrdenesDeTrabajo.EntOtfIncidenteAlmacen
        vlo_EntOtfIncidenteAlmacen.IdIncidenteAlmacen = pvn_IdIncidenteAlmacen

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTF_INCIDENTE_ALMACEN_BorrarRegistroConAdjuntos(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtfIncidenteAlmacen) > 0
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
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String
        vlc_Condicion = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtDetalle.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("UPPER({0}) LIKE '%{1}%'", Modelo.V_OTF_INCIDENTE_ALMACENLST.DETALLE, Me.txtDetalle.Text.Trim.ToUpper)
            Else
                vlc_Condicion = String.Format("{0} AND UPPER({1}) LIKE '%{2}%'", vlc_Condicion, Modelo.V_OTF_INCIDENTE_ALMACENLST.DETALLE, Me.txtDetalle.Text.Trim.ToUpper)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlTipo.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTF_INCIDENTE_ALMACENLST.ID_TIPO_INCIDENTE, Me.ddlTipo.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTF_INCIDENTE_ALMACENLST.ID_TIPO_INCIDENTE, Me.ddlTipo.SelectedValue)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroFecha.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = TO_DATE('{1}', 'dd/mm/yyyy')", Modelo.V_OTF_INCIDENTE_ALMACENLST.FECHA_INCLUSION, Me.txtFiltroFecha.Text)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = TO_DATE('{2}', 'dd/mm/yyyy')", vlc_Condicion, Modelo.V_OTF_INCIDENTE_ALMACENLST.FECHA_INCLUSION, Me.txtFiltroFecha.Text)
            End If
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OTF_INCIDENTE_ALMACENLST.ID_UBICACION, Me.AutorizadoUbicacion.IdUbicacionAdministra)
        Else
            vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTF_INCIDENTE_ALMACENLST.ID_UBICACION, Me.AutorizadoUbicacion.IdUbicacionAdministra)
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTF_INCIDENTE_ALMACENLST.ESTADO, EstadoIncidente.CREADO)
        Else
            vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTF_INCIDENTE_ALMACENLST.ESTADO, EstadoIncidente.CREADO)
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
    ''' <creationDate>05/08/2016</creationDate>
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
    ''' carga la ubicacion 
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
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

#End Region

End Class
