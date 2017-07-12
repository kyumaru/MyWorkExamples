Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_ReingresoMateriales
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
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
    ''' <creationDate>28/07/2016</creationDate>
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
    ''' <creationDate>28/07/2016</creationDate>
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
    ''' Propiedad para la ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortExpression2 As String
        Get
            If ViewState("UltimoSortExpression2") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortExpression2"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortExpression2") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima columna de clasificacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortColumn2 As String
        Get
            If ViewState("UltimoSortColumn2") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortColumn2"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortColumn2") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima direccion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortDirection2 As SortDirection
        Get
            If ViewState("UltimoSortDirection2") Is Nothing Then
                Return SortDirection.Ascending
            End If
            Return CType(ViewState("UltimoSortDirection2"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("UltimoSortDirection2") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de materiales a ingresar eliminar o modificar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
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
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
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
    ''' <creationDate>28/07/2016</creationDate>
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
    ''' <creationDate>28/07/2016</creationDate>
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
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
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
    ''' Propiedad para el dataset de solicitud de reingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsSolicitudReingreso As Data.DataSet
        Get
            Return CType(ViewState("DsSolicitudReingreso"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsSolicitudReingreso") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta al cargar la página
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
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
        Me.pnRpSolicitudReingreso.Dibujar()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados de la 
    ''' tabla del listado de aprobados, carga la lista de registros en orden relacionado a la columna seleccionada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpSolicitudReingreso_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarListaSolicitudReingreso(ObtenerExpresionDeOrdenamiento2(e.CommandName), pnRpSolicitudReingreso.PaginaActualLista)
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
    ''' <creationDate>24/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub pnRpSolicitudReingreso_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpSolicitudReingreso.CambioDePagina
        Try
            CargarListaSolicitudReingreso(UltimoSortExpression2, pvn_PaginaSeleccionada)
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
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkEjecutarBusquedaOrden_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusquedaOrden.Click
        Try
            BuscarOrdenTrabajo()
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
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub txtIdOrdenTrabajo_TextChanged(sender As Object, e As EventArgs) Handles txtIdOrdenTrabajo.TextChanged
        Try
            BuscarOrdenTrabajo()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados de la 
    ''' tabla del listado de aprobados, carga la lista de registros en orden relacionado a la columna seleccionada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpMateriales_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarListaMateriales(ObtenerExpresionDeOrdenamiento(e.CommandName))
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta al dar click en el boton de cancelar 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect("../../Genericos/Frm_MenuPrincipal.aspx", False)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnTramitarReingreso_Click(sender As Object, e As EventArgs) Handles btnTramitarReingreso.Click
        Try
            If Tramitar() Then
                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
            Else
                MostrarAlertaError("Se ha producido un error, no se ha enviado la notificación ni la actualización de la orden.")
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
    ''' <creationDate>01/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' inicializa los componentes necesarios para el funcionamiento de la página
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Try

            If Roles.IsUserInRole(Membership.GetUser.UserName, RolesSistema.OT_COORDINADOR_MANTENIMIENTO) Then

                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If Me.AutorizadoUbicacion.Existe Then

                    CargarSectorTaller()

                    If Not Me.SectorTaller.Existe Then
                        WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ningun taller o sector en coordinación.','../../Genericos/Frm_MenuPrincipal.aspx');")
                    Else
                        CargarEstructuraSolicitudReingreso()
                    End If
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
    ''' Carga el sector del uausrio en session
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarSectorTaller()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.SectorTaller = vlo_Ws_OT_Catalogos.OTM_SECTOR_TALLER_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("({0} = {2} OR {1} = {2}) AND {3} = {4}",
                             Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_COORDINADOR,
                             Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_SUSTITUTO,
                             Me.Usuario.NumEmpleado,
                             Modelo.OTM_SECTOR_TALLER.ID_UBICACION_ADMINISTRA,
                             Me.AutorizadoUbicacion.IdUbicacionAdministra))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' carga la OT, segun el dato del campo para id orden trabajo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BuscarOrdenTrabajo()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        'instanciar y configurar objetos
        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If Me.txtIdOrdenTrabajo.Text <> String.Empty Then

                Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, Me.AutorizadoUbicacion.IdUbicacionAdministra, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, Me.txtIdOrdenTrabajo.Text, Modelo.OTT_ORDEN_TRABAJO.ID_SECTOR_TALLER, Me.SectorTaller.IdSectorTaller))

                If Me.OrdenTrabajo IsNot Nothing AndAlso Me.OrdenTrabajo.Existe Then

                    Me.txtIdOrdenTrabajo.Text = Me.OrdenTrabajo.IdOrdenTrabajo
                    InicializarControl()
                    CargarListaMateriales(String.Format("{0} {1}", Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL, Ordenamiento.ASCENDENTE))
                    BuscarSolicitudReingreso(String.Format("{0} {1}", Modelo.V_OTT_SOLICITUD_REINGRESOLST.DESCRIPCION, Ordenamiento.ASCENDENTE))
                    upControlDatosOrden.Visible = True

                Else
                    Me.txtIdOrdenTrabajo.Text = String.Empty
                    upControlDatosOrden.Visible = False
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoEncontrado();")
                End If
            Else
                upControlDatosOrden.Visible = False
            End If

            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub InicializarControl()
        'Asignación de Datos para generar el web user control de la información general
        ctrl_InfoGeneral.Anno = Me.OrdenTrabajo.Anno
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.OrdenTrabajo.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.OrdenTrabajo.IdUbicacion
        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()
    End Sub

    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaMateriales(pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.DsMateriales = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}' AND {2} = {3} AND ({4} = '{5}' OR {4} = '{6}' OR {4} = '{7}')", Modelo.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, Me.OrdenTrabajo.IdOrdenTrabajo, Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION, Me.OrdenTrabajo.IdUbicacion, Modelo.OTT_DETALLE_MATERIAL.ESTADO, EstadoRegistro.PENDIENTE_APROBACION, EstadoRegistro.APROBADA, EstadoRegistro.DENEGADA),
                pvc_Orden,
                False,
                0,
                0)

            If Me.DsMateriales IsNot Nothing AndAlso Me.DsMateriales.Tables(0).Rows.Count > 0 Then
                Me.rpMateriales.DataSource = Me.DsMateriales
                Me.rpMateriales.DataMember = Me.DsMateriales.Tables(0).TableName
                Me.rpMateriales.DataBind()
                Me.rpMateriales.Visible = True

                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de registros {0}", Me.DsMateriales.Tables(0).Rows.Count)
            Else
                With Me.rpMateriales
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = True
                End With

                Me.lblCantidadDeRegistros.Visible = False
                Me.lblCantidadDeRegistros.Text = String.Empty
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
    ''' Método encargado de realizar la busqueda de registros segun los datos de 
    ''' condicion y orden de búsqueda
    ''' </summary>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BuscarSolicitudReingreso(pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerExpresionDeOrdenamiento2(Modelo.V_OTT_SOLICITUD_REINGRESOLST.ID_SOLICITUD_REINGRESO)
        End If

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntDatosPaginacion =
                vlo_Ws_OT_OrdenesDeTrabajo.OTT_SOLICITUD_REINGRESO_ObtenerDatosPaginacionVOttSolicitudReingresolst(
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                     String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OTT_SOLICITUD_REINGRESOLST.ID_UBICACION, Me.OrdenTrabajo.IdUbicacion, Modelo.V_OTT_SOLICITUD_REINGRESOLST.ID_ORDEN_TRABAJO, Me.OrdenTrabajo.IdOrdenTrabajo),
                     pvc_Orden,
                     CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpSolicitudReingreso.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                CargarListaSolicitudReingreso(pvc_Orden, 1)
                Me.pnRpSolicitudReingreso.Dibujar()
                Me.lblCantidadDeRegistros2.Visible = True
                Me.lblCantidadDeRegistros2.Text = String.Format("Cantidad de registros {0}", vlo_EntDatosPaginacion.TotalRegistros)
            Else
                Me.lblCantidadDeRegistros2.Visible = False
                Me.lblCantidadDeRegistros2.Text = String.Empty
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
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaSolicitudReingreso(pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_SOLICITUD_REINGRESO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OTT_SOLICITUD_REINGRESOLST.ID_UBICACION, Me.OrdenTrabajo.IdUbicacion, Modelo.V_OTT_SOLICITUD_REINGRESOLST.ID_ORDEN_TRABAJO, Me.OrdenTrabajo.IdOrdenTrabajo),
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpSolicitudReingreso
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With

            Else
                With Me.rpSolicitudReingreso
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With
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
    ''' carga la estructura del dataset de solicitudes de reingreso
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEstructuraSolicitudReingreso()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsSolicitudReingreso = vlo_Ws_OT_OrdenesDeTrabajo.OTT_SOLICITUD_REINGRESO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("1 = 0"), String.Empty, False, 0, 0)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' recorre el repeater y 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargaSolicitudesReingresoRepeater()
        Dim vlo_DropDownList As DropDownList
        Dim vlo_TextBox As TextBox
        Dim vlo_HiddenField As HiddenField
        Dim vlo_DrFila As Data.DataRow

        Try

            For Each vlo_item In Me.rpMateriales.Items

                vlo_DropDownList = vlo_item.FindControl("ddltipoSolicitud")
                vlo_TextBox = vlo_item.FindControl("txtCantReingresar")
                vlo_HiddenField = vlo_item.FindControl("hdfIdDetalleMaterial")

                If vlo_DropDownList.SelectedValue <> String.Empty AndAlso vlo_TextBox.Text <> String.Empty Then

                    vlo_DrFila = Me.DsSolicitudReingreso.Tables(0).NewRow
                    vlo_DrFila.Item(Me.DsSolicitudReingreso.Tables(0).Columns(Modelo.V_OTT_SOLICITUD_REINGRESOLST.ANNO)) = DateTime.Now.Year
                    vlo_DrFila.Item(Me.DsSolicitudReingreso.Tables(0).Columns(Modelo.V_OTT_SOLICITUD_REINGRESOLST.ID_UBICACION_ADMINISTRA)) = Me.AutorizadoUbicacion.IdUbicacionAdministra
                    vlo_DrFila.Item(Me.DsSolicitudReingreso.Tables(0).Columns(Modelo.V_OTT_SOLICITUD_REINGRESOLST.ID_DETALLE_MATERIAL)) = vlo_HiddenField.Value
                    vlo_DrFila.Item(Me.DsSolicitudReingreso.Tables(0).Columns(Modelo.V_OTT_SOLICITUD_REINGRESOLST.CANTIDAD_REINGRESO)) = vlo_TextBox.Text
                    vlo_DrFila.Item(Me.DsSolicitudReingreso.Tables(0).Columns(Modelo.V_OTT_SOLICITUD_REINGRESOLST.CANTIDAD_RECIBIDA)) = 0
                    vlo_DrFila.Item(Me.DsSolicitudReingreso.Tables(0).Columns(Modelo.V_OTT_SOLICITUD_REINGRESOLST.COSTO_CALCULADO)) = 0
                    vlo_DrFila.Item(Me.DsSolicitudReingreso.Tables(0).Columns(Modelo.V_OTT_SOLICITUD_REINGRESOLST.TIPO_SOLICITUD_REINGRESO)) = vlo_DropDownList.SelectedValue
                    vlo_DrFila.Item(Me.DsSolicitudReingreso.Tables(0).Columns(Modelo.V_OTT_SOLICITUD_REINGRESOLST.ESTADO)) = EstadoSolicitudReingreso.PENDIENTE
                    vlo_DrFila.Item(Me.DsSolicitudReingreso.Tables(0).Columns(Modelo.V_OTT_SOLICITUD_REINGRESOLST.USUARIO)) = Me.Usuario.UserName

                    Me.DsSolicitudReingreso.Tables(0).Rows.Add(vlo_DrFila)

                End If
            Next

        Catch ex As Exception
            Throw
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
    ''' <creationDate>01/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Tramitar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            CargaSolicitudesReingresoRepeater()

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_SOLICITUD_REINGRESO_InsertarSolicitudesReingreso(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.DsSolicitudReingreso, Me.AutorizadoUbicacion.IdUbicacionAdministra, DateTime.Now.Year) > 0

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
    ''' <creationDate>28/07/2016</creationDate>
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
    ''' funcion encargada de realizar los oredenamientos segun los valores brindados
    ''' </summary>
    ''' <param name="pvc_Columna"> numero de la columna para el ordenamiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
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
    ''' funcion encargada de realizar los oredenamientos segun los valores brindados
    ''' </summary>
    ''' <param name="pvc_Columna"> numero de la columna para el ordenamiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerExpresionDeOrdenamiento2(pvc_Columna As String) As String
        If String.IsNullOrWhiteSpace(UltimoSortColumn2) OrElse pvc_Columna.CompareTo(UltimoSortColumn2) <> 0 Then
            UltimoSortColumn2 = pvc_Columna
            UltimoSortDirection2 = SortDirection.Ascending
        Else
            If UltimoSortDirection2 = SortDirection.Ascending Then
                UltimoSortDirection2 = SortDirection.Descending
            Else
                UltimoSortDirection2 = SortDirection.Ascending
            End If
        End If

        UltimoSortExpression2 = String.Format("{0} {1}", UltimoSortColumn2, IIf(UltimoSortDirection2 = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))
        Return UltimoSortExpression2
    End Function

#End Region

End Class
