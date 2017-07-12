Imports Utilerias.OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Almacen_Lst_OT_RetiroMateriales
    Inherits System.Web.UI.Page
#Region "Propiedades"
    ''' <summary>
    ''' Propiedad para la ultima condicion de busqueda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' parametro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' ubicacion del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' Evento que se ejecuta cuando se da click sobre el botón Buscar,
    ''' que se encuentra en el área de filtros.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            UltimaCondicionBusqueda = String.Empty
            Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Redirige con parametros a la página de retiro de materiales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub chkEntregado_CheckedChanged(sender As Object, e As EventArgs)
        Dim vlo_chkEntregado As CheckBox

        Try
            vlo_chkEntregado = CType(sender, CheckBox)


            Me.Session.Add("pvn_IdUbicacion", vlo_chkEntregado.Attributes("data-idUbicacion"))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlo_chkEntregado.Attributes("data-idOrdenTrabajo"))
            Me.Session.Add("pvn_Anno", vlo_chkEntregado.Attributes("data-anno"))
            Me.Session.Add("pvn_IdSectorTaller", vlo_chkEntregado.Attributes("data-idSectorTaller"))
            Me.Session.Add("pvn_IdSolicitudRetiro", vlo_chkEntregado.Attributes("data-idSolicitudRetiro"))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("Frm_OT_RetiroMateriales.aspx", False)
    End Sub

    ''' <summary>
    ''' Evento para mostrar información de la OT
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez</author>
    ''' <creationDate>1/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkNumOt_Click(sender As Object, e As EventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, LinkButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_Operacion", eOperacion.Consultar)
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Regresar", "Lst_OT_DespachoMateriales.aspx")

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
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' Evento que se ejecuta al carar el repeater de ordenes, asigna atributos a cada elemento del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez</author>
    ''' <creationDate>4/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpOrdenTrabajo_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpOrdenTrabajo.ItemDataBound
        Dim vlo_IbRevisar As HtmlTableRow
        Dim vlo_HiddenField2 As HiddenField

        If e.Item.FindControl("trOrden") IsNot Nothing Then
            vlo_IbRevisar = CType(e.Item.FindControl("trOrden"), HtmlTableRow)
            vlo_HiddenField2 = CType(e.Item.FindControl("hdfTipoOrden"), HiddenField)

            If vlg_ParametroRango.Existe And vlg_ParametroAtencion.Existe And vlg_ParametroAtraso.Existe Then

                If vlo_HiddenField2.Value = TipoOrden.EMERGENCIA Then
                    vlo_IbRevisar.BgColor = "#F6D1C9"
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez</author>
    ''' <creationDate>6/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlFiltroSectorTaller_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim vlc_condicion As String

        Try

            If ddlFiltroSectorTaller.SelectedValue <> String.Empty Then
                vlc_condicion = String.Format("{0} = '{1}' ", Modelo.V_OTT_SOLICITUD_RETIROLST.ID_SECTOR_TALLER, Me.ddlFiltroSectorTaller.SelectedValue)
                vlc_condicion = String.Format("{0} AND {1} = '{2}'", vlc_condicion, Modelo.V_OTT_SOLICITUD_RETIROLST.ESTADO_SOLICITUD_RETIRO, EstadoOrden.SOLICITUD_LISTA_RETIRO)
                BuscarSinFiltro(vlc_condicion, UltimoSortExpression)
            Else

                vlc_condicion = String.Format("{0} = '{1}'", Modelo.V_OTT_SOLICITUD_RETIROLST.ESTADO_SOLICITUD_RETIRO, EstadoOrden.SOLICITUD_LISTA_RETIRO)
                BuscarSinFiltro(vlc_condicion, UltimoSortExpression)
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inicializa el listado y envia a buscar con los valores especificados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarListado()
        Try
            Me.Usuario = New UsuarioActual
            Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)
            If AutorizadoUbicacion.Existe Then
                InicializarParametros()
                LeerParametrosSession()
                Buscar(ObtenerCondicionBusqueda, String.Empty)
                CargarListaTalleres()
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ninguna ubicación autorizada, contactar con el administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
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
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>7/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametrosSession()

        Try

            Me.UltimaCondicionBusqueda = WebUtils.LeerParametro(Of String)("pvc_UltimaCondicionBusqueda")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Carga e inicializa los parametros del sistema, relacionados a los rangos de tiempos para semaforo de OT's
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarParametros()
        Try
            vlg_ParametroRango = CargarParametro(Parametros.TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_EN_RANGO, Me.AutorizadoUbicacion.IdUbicacionAdministra)
            vlg_ParametroAtencion = CargarParametro(Parametros.TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_ATENCION, Me.AutorizadoUbicacion.IdUbicacionAdministra)
            vlg_ParametroAtraso = CargarParametro(Parametros.TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_ATRASO, Me.AutorizadoUbicacion.IdUbicacionAdministra)

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
    ''' <creationDate>1/7/2016</creationDate>
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
                pvc_Orden = String.Format("{0} {1}", Modelo.V_OTT_SOLICITUD_RETIROLST.TIPO_ORDEN_TRABAJO, Ordenamiento.ASCENDENTE)
            End If

            vlo_EntDatosPaginacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_SOLICITUD_RETIRO_ObtenerDatosPaginacionVOttSolicitudRetirolst(
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
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de solicitudes {0}", vlo_EntDatosPaginacion.TotalRegistros)
                Me.rpOrdenTrabajo.Visible = True
                Me.pnRpOrdenTrabajo.Visible = True
                Me.trTaller.Visible = True
            Else
                Me.ddlFiltroSectorTaller.SelectedValue = String.Empty
                Me.trTaller.Visible = False
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

    Private Sub BuscarSinFiltro(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If String.IsNullOrEmpty(pvc_Orden) Then
                pvc_Orden = String.Format("{0} {1}", Modelo.V_OTT_SOLICITUD_RETIROLST.TIPO_ORDEN_TRABAJO, Ordenamiento.ASCENDENTE)
            End If

            vlo_EntDatosPaginacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_SOLICITUD_RETIRO_ObtenerDatosPaginacionVOttSolicitudRetirolst(
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
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de solicitudes {0}", vlo_EntDatosPaginacion.TotalRegistros)
                Me.rpOrdenTrabajo.Visible = True
                Me.pnRpOrdenTrabajo.Visible = True
                Me.trTaller.Visible = True
            Else
                Me.ddlFiltroSectorTaller.SelectedValue = String.Empty
                Me.trTaller.Visible = True
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de solicitudes 0")
                Me.rpOrdenTrabajo.Visible = False
                Me.pnRpOrdenTrabajo.Visible = False
                MostrarAlertaNoHayDatosSinFiltro()
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
    ''' <creationDate>1/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "mostrarAlertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    Private Sub MostrarAlertaNoHayDatosSinFiltro()
        WebUtils.RegistrarScript(Me, "mostrarAlertaNoHayDatos", "mostrarAlertaNoHayDatosSinFiltro();")
    End Sub

    ''' <summary>
    ''' Ejecuta un llamado para obtener estados de orden de trabajo desde la base de datos y cargar la lista de filtros con ella.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaTalleres()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlfiltroTallerSector.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
            Me.ddlFiltroSectorTaller.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
            '{0}: Columna ESTADO
            '{1}: sectores y talleres activos

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_SECTOR_TALLER_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}'", Modelo.OTM_SECTOR_TALLER.ESTADO, Estado.ACTIVO),
                String.Format("{0} {1}", Modelo.OTM_SECTOR_TALLER.NOMBRE, Ordenamiento.ASCENDENTE),
                False, 0, 0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlfiltroTallerSector
                    .DataSource = vlo_DsDatos
                    .DataValueField = Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER
                    .DataTextField = Modelo.OTM_SECTOR_TALLER.NOMBRE
                    .DataBind()
                End With

                With Me.ddlFiltroSectorTaller
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
    ''' <creationDate>1/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_SOLICITUD_RETIRO_ListarRegistrosLista(
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

#End Region

#Region "Funciones"

    ''' <summary>
    ''' construye la condicion de busqueda
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroNumOrden.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OTT_SOLICITUD_RETIROLST.ID_ORDEN_TRABAJO, Me.txtFiltroNumOrden.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OTT_SOLICITUD_RETIROLST.ID_ORDEN_TRABAJO, Me.txtFiltroNumOrden.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroTipoOrden.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OTT_SOLICITUD_RETIROLST.DESC_TIPO_ORDEN, Me.txtFiltroTipoOrden.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OTT_SOLICITUD_RETIROLST.DESC_TIPO_ORDEN, Me.txtFiltroTipoOrden.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroSolcitante.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OTT_SOLICITUD_RETIROLST.NOMBRE_SOLICITANTE, Me.txtFiltroSolcitante.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OTT_SOLICITUD_RETIROLST.NOMBRE_SOLICITANTE, Me.txtFiltroSolcitante.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlfiltroTallerSector.SelectedValue) Then
            If Not String.IsNullOrWhiteSpace(Me.ddlfiltroTallerSector.SelectedValue) Then
                vlc_Condicion = String.Format("{0} = '{1}' ", Modelo.V_OTT_SOLICITUD_RETIROLST.ID_SECTOR_TALLER, Me.ddlfiltroTallerSector.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTT_SOLICITUD_RETIROLST.ID_SECTOR_TALLER, Me.ddlfiltroTallerSector.SelectedValue)
            End If
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} = '{1}' ", Modelo.V_OTT_SOLICITUD_RETIROLST.ESTADO_SOLICITUD_RETIRO, EstadoOrden.SOLICITUD_LISTA_RETIRO)
        Else
            vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTT_SOLICITUD_RETIROLST.ESTADO_SOLICITUD_RETIRO, EstadoOrden.SOLICITUD_LISTA_RETIRO)
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
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' carga parametros del sistema
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
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
