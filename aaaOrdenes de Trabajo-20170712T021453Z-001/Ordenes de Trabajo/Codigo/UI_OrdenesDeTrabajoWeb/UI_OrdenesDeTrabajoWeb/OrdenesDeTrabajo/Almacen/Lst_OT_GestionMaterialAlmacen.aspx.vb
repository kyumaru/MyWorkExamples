Imports Utilerias.OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo.Modelo
Imports System.Data

Partial Class OrdenesDeTrabajo_Almacen_Lst_OT_GestionMaterialAlmacen
    Inherits System.Web.UI.Page
#Region "Propiedades"
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

    ''' <summary>l
    ''' Propiedad para la ultima columna de clasificacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
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
    ''' <creationDate>23/05/2016</creationDate>
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

    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property

    Public Property AdjuntoCotizacion As Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoCotizacion
        Get
            Return CType(ViewState("AdjuntoCotizacion"), Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoCotizacion)
        End Get
        Set(value As Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoCotizacion)
            ViewState("AdjuntoCotizacion") = value
        End Set
    End Property

    Public Property IdUbicacion As Integer
        Get
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    Public Property Anno As Integer
        Get
            Return CType(ViewState("Anno"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Anno") = value
        End Set
    End Property

    Public Property IdViaCompra As Integer
        Get
            Return CType(ViewState("IdViaCompra"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdViaCompra") = value
        End Set
    End Property

    Public Property NumeroGestion As Integer
        Get
            Return CType(ViewState("NumeroGestion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("NumeroGestion") = value
        End Set
    End Property

    Public Property Identificacion As String
        Get
            Return CType(ViewState("Identificacion"), String)
        End Get
        Set(value As String)
            ViewState("Identificacion") = value
        End Set
    End Property

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
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim vlc_Condicion As String

        If Not Page.IsPostBack Then
            Try

                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If AutorizadoUbicacion.Existe Then
                    LeerParametrosSession()

                    CargarGestionCompra()

                    CargarArchivoProveedor()


                    vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ID_UBICACION, Me.IdUbicacion, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ANNO, Me.Anno, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.NUMERO_GESTION, Me.NumeroGestion, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra)

                    Buscar(vlc_Condicion, String.Empty)



                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ninguna ubicación autorizada, contactar con el administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If



            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    Protected Sub lnkRpSuministros_Command(sender As Object, e As CommandEventArgs)
        Dim vlc_Condicion As String

        Try
            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ID_UBICACION, Me.IdUbicacion, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ANNO, Me.Anno, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.NUMERO_GESTION, Me.NumeroGestion, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra)

            CargarLista(vlc_Condicion, ObtenerExpresionDeOrdenamiento(e.CommandName), Me.pnRpSuministros.PaginaActualLista)
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
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub pnRpSuministros_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpSuministros.CambioDePagina
        Dim vlc_Condicion As String

        Try
            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ID_UBICACION, Me.IdUbicacion, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ANNO, Me.Anno, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.NUMERO_GESTION, Me.NumeroGestion, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_GESTION_INGRESO_MATERLST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra)

            CargarLista(vlc_Condicion, UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub lnkTrazabilidadGestion(sender As Object, e As CommandEventArgs)
        Dim vlc_CommandArgument As String
        Dim vlc_Llave As String()

        vlc_CommandArgument = CType(CType(sender, LinkButton).CommandArgument, String)
        vlc_Llave = vlc_CommandArgument.Split("%")

        Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
        Me.Session.Add("pvn_IdViaCompraContrato", vlc_Llave(1))
        Me.Session.Add("pvn_Anno", vlc_Llave(2))
        Me.Session.Add("pvn_NumeroGestion", vlc_Llave(3))
        Me.Session.Add("pvn_Consecutivo", vlc_Llave(4))
        Me.Session.Add("pvc_PaginaRegreso", "Lst_OT_ValidarMontos.aspx")

        Response.Redirect("Lst_OT_TrazabilidadGestionIngresoMat.aspx", False)

    End Sub

    Protected Sub ibConsultarGestion_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_CommandArgument As String
        Dim vlc_Llave As String()

        vlc_CommandArgument = CType(CType(sender, ImageButton).CommandArgument, String)
        vlc_Llave = vlc_CommandArgument.Split("%")

        Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
        Me.Session.Add("pvn_Anno", vlc_Llave(1))
        Me.Session.Add("pvn_IdViaCompraContrato", vlc_Llave(2))
        Me.Session.Add("pvn_NumeroGestion", vlc_Llave(3))
        Me.Session.Add("pvn_Consecutivo", vlc_Llave(4))
        Me.Session.Add("pvn_Operacion", eOperacion.Modificar)
        Me.Session.Add("pvn_Modo", 1)
        Me.Session.Add("pvc_GestionDespliegue", Me.lblNumeroGestion.Text)

        Response.Redirect("Frm_OT_RegistroIngresoMatAlmacen.aspx", False)

    End Sub

    Protected Sub ibAgregar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Me.Session.Add("pvn_IdUbicacion", Me.IdUbicacion)
            Me.Session.Add("pvn_IdViaCompraContrato", Me.IdViaCompra)
            Me.Session.Add("pvn_Anno", Me.Anno)
            Me.Session.Add("pvn_NumeroGestion", Me.NumeroGestion)
            Me.Session.Add("pvn_Operacion", eOperacion.Agregar)
            Me.Session.Add("pvn_Modo", 1)
            Me.Session.Add("pvc_GestionDespliegue", Me.lblNumeroGestion.Text)

            Response.Redirect("Frm_OT_RegistroIngresoMatAlmacen.aspx", False)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub lnkArchivo_Click(sender As Object, e As EventArgs) Handles lnkArchivo.Click
        DescargaArchivo(Me.AdjuntoCotizacion.Archivo, Me.AdjuntoCotizacion.NombreArchivo)
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    ''' <summary>
    ''' lee y carga los parametros guardados en sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>29/08/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametrosSession()
        Try
            Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
            Me.NumeroGestion = WebUtils.LeerParametro(Of Integer)("pvn_NumeroGestion")
            Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
            Me.IdViaCompra = WebUtils.LeerParametro(Of Integer)("pvn_IdViaCompraContrato")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Private Sub CargarArchivoProveedor()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.AdjuntoCotizacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_COTIZACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = '{9}'",
                              Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_COTIZACION.ID_UBICACION, Me.IdUbicacion,
                              Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_COTIZACION.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra,
                              Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_COTIZACION.ANNO, Me.Anno,
                              Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_COTIZACION.NUMERO_GESTION, Me.NumeroGestion,
                              Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_COTIZACION.IDENTIFICACION, Me.Identificacion))

            Me.lnkArchivo.Text = Me.AdjuntoCotizacion.NombreArchivo

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

    End Sub

    Private Sub DescargaArchivo(pvo_Archivo As Byte(), pvc_NombreArchivo As String)
        pvc_NombreArchivo = pvc_NombreArchivo.Replace(" ", "")
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + pvc_NombreArchivo)
            Response.BinaryWrite(pvo_Archivo)
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_INGRESO_MATER_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.RpSuministros
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End With
            Else
                MostrarAlertaNoHayDatos()

                With Me.RpSuministros
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With


                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "ocultarAreaDeListado(); mostrarAreaFiltrosDeBusqueda();")
                Me.RpSuministros.Visible = False
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
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerExpresionDeOrdenamiento(Modelo.V_OTT_GESTION_INGRESO_MATERLST.NUMERO_GESTION_COMPRA)
        End If

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials



        Try

            vlo_EntDatosPaginacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_INGRESO_MATER_ObtenerDatosPaginacionVOttGestionIngresoMaterlst(
                         ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                         ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                         pvc_Condicion,
                         pvc_Orden,
                         CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpSuministros.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                Me.pnRpSuministros.Dibujar()
                Me.lblCantidadDeRegistrosSM.Visible = True
                Me.lblCantidadDeRegistrosSM.Text = String.Format("Cantidad de Registros {0}", vlo_EntDatosPaginacion.TotalRegistros)
                Me.RpSuministros.Visible = True
                CargarLista(pvc_Condicion, pvc_Orden, 1)
            Else

                Me.lblCantidadDeRegistrosSM.Visible = False
                Me.lblCantidadDeRegistrosSM.Text = String.Empty
                MostrarAlertaNoHayDatos()
                Me.RpSuministros.Visible = False
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
    ''' Se obtiene el ajuste de inventario almacenado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarGestionCompra()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Utilerias.OrdenesDeTrabajo.Modelo.OTT_GESTION_COMPRA.ID_UBICACION, Me.IdUbicacion, Utilerias.OrdenesDeTrabajo.Modelo.OTT_GESTION_COMPRA.ANNO, Me.Anno, Utilerias.OrdenesDeTrabajo.Modelo.OTT_GESTION_COMPRA.NUMERO_GESTION, Me.NumeroGestion, Utilerias.OrdenesDeTrabajo.Modelo.OTT_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra),
                String.Empty,
                False,
                0, 0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                Me.lblViaCompra.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_COMPRALST.DESC_VIA_COMPRA)
                Me.lblNumeroGestion.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION_COMPRA)
                Me.txtObservaciones.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES)
                Me.Identificacion = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_COMPRALST.ID_PROVEEDOR_ADJ)
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
    ''' <creationDate>23/05/2016</creationDate>
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
