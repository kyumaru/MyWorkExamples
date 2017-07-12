Imports System.Data
Imports Wsr_OT_OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo.Modelo
Imports Utilerias.OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_FondoTrabajoAprobJefatura
    Inherits System.Web.UI.Page
#Region "Propiedades"
    ''' <summary>
    ''' Propiedad para la ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortExpressionArchivo As String
        Get
            If ViewState("UltimoSortExpressionArchivo") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortExpressionArchivo"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortExpressionArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima columna de clasificacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortColumnArchivo As String
        Get
            If ViewState("UltimoSortColumnArchivo") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortColumnArchivo"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortColumnArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima direccion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortDirectionArchivo As SortDirection
        Get
            If ViewState("UltimoSortDirectionArchivo") Is Nothing Then
                Return SortDirection.Ascending
            End If
            Return CType(ViewState("UltimoSortDirectionArchivo"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("UltimoSortDirectionArchivo") = value
        End Set
    End Property

    Private Property UltimaCondicionBusquedaArchivo As String
        Get
            If ViewState("UltimaCondicionBusquedaArchivo") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimaCondicionBusquedaArchivo"), String)
        End Get
        Set(value As String)
            ViewState("UltimaCondicionBusquedaArchivo") = value
        End Set
    End Property

    Public Property IdUbicacion As Integer
        Get
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    Public Property IdViaCompraContrato As Integer
        Get
            Return CType(ViewState("IdViaCompraContrato"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("IdViaCompraContrato") = value
        End Set
    End Property

    Public Property Anno As Integer
        Get
            Return CType(ViewState("Anno"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("Anno") = value
        End Set
    End Property

    Public Property NumeroGestion As Integer
        Get
            Return CType(ViewState("NumeroGestion"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("NumeroGestion") = value
        End Set
    End Property

    Private Property DsAdjuntos As Data.DataSet
        Get
            Return CType(ViewState("DsAdjuntos"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsAdjuntos") = value
        End Set
    End Property

    Public Property UniqueIDBotonAceptar As String
        Get
            If ViewState("UniqueIDBotonAceptar") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UniqueIDBotonAceptar"), String)
        End Get
        Set(value As String)
            ViewState("UniqueIDBotonAceptar") = value
        End Set
    End Property
#End Region

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load       
        If Not IsPostBack Then
            
            Try
                InicializarFormulario()
                

            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
            Me.pnRpArchivo.Dibujar()
        End If
    End Sub

    Protected Sub rpMateriales_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpMateriales.ItemDataBound
        Dim vlo_HtmlAnchor As HtmlAnchor
        Dim vlo_HtmlGenericControl As HtmlGenericControl
        Dim vlo_WebUserControl As Controles_wuc_OT_DetalleGestionCompraFondo

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_HtmlAnchor = CType(e.Item.FindControl("ancorAcordeon"), HtmlAnchor)
            vlo_HtmlGenericControl = e.Item.FindControl("cuerpoAcordeon1")
            vlo_HtmlAnchor.HRef = "#" + vlo_HtmlGenericControl.ClientID

            vlo_WebUserControl = CType(e.Item.FindControl("wucDetalleGestionCompraFondo"), Controles_wuc_OT_DetalleGestionCompraFondo)
            vlo_WebUserControl.Inicializar()

        End If
    End Sub

    ''' <summary>
    ''' Evento que permite cambiar de pagina en la lista de evaluaciones entrevista
    ''' </summary>
    ''' <param name="pvn_PaginaSeleccionada"></param>
    ''' <remarks></remarks>
    Protected Sub pnRpArchivo_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpArchivo.CambioDePagina
        Try
            CargarListaArchivo(UltimaCondicionBusquedaArchivo, UltimoSortExpressionArchivo, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub lnkArchivo_Command(sender As Object, e As CommandEventArgs)
        Try

            CargarListaArchivo(UltimaCondicionBusquedaArchivo, ObtenerExpresionDeOrdenamientoArchivo(e.CommandName), pnRpArchivo.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    'Protected Sub lnkDescargarArchivo_Command(sender As Object, e As CommandEventArgs)
    '    Dim vlb_BanderaDescargaArchivo As Boolean
    '    Try
    '        DescargarArchivoBaseDatos(e.CommandName, Utilerias.OrdenesDeTrabajo.TipoProcedimiento.ProcesarArchivosDocumentoAdjunto)

    '    Catch ex_Descarga As System.Threading.ThreadAbortException
    '        vlb_BanderaDescargaArchivo = True
    '    Catch ex As Exception
    '        Dim vlo_ControlDeErrores As New ControlDeErrores
    '        vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
    '    End Try
    'End Sub

    Protected Sub lnkDescargarArchivo_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsAdjuntos.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CType(Me.DsAdjuntos.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.V_OTT_ADJUNTO_COTIZACIONLST.ARCHIVO), Byte()))
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Evento del botón aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <author>Mauricio Salas</author>
    ''' <remarks></remarks>
    Protected Sub btnTramitar_Click(sender As Object, e As EventArgs) Handles lnkAceptarAux.Click
        If Page.IsValid Then
            Try
                GuardarRevision()

            Catch ex As Exception
                If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                    Dim vlo_OrdenesDeTrabajoException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                    MostrarAlertaError(vlo_OrdenesDeTrabajoException.Message)
                Else
                    Dim vlo_ControlDeErrores As New ControlDeErrores
                    vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
                End If
            End Try
        End If
    End Sub

#End Region

#Region "Métodos"
    Private Sub InicializarFormulario()
        Dim vlc_Condicion As String
        Dim vlo_DsAdjudicado As Data.DataSet
        Dim vlo_Wsr_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo

        Try
            vlo_Wsr_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
            vlo_Wsr_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Wsr_OT_OrdenesDeTrabajo.Timeout = -1

            Me.IdUbicacion = CType(Session("pvn_IdUbicacion"), Integer)
            Me.IdViaCompraContrato = CType(Session("pvn_IdViaCompraContrato"), Integer)
            Me.Anno = CType(Session("pvn_Anno"), Integer)
            Me.NumeroGestion = CType(Session("pvn_NumeroGestion"), Integer)

            Me.UniqueIDBotonAceptar = Me.btnTramitar.UniqueID

            'Se carga la informacion del acordeon
            CargarListaAcordeon(ObtenerCondicionBusquedaAcordeon(), String.Empty)

            'Se obtiene el proveedor adjudicado
            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = 1", Modelo.V_OTT_PROVEEDOR_COTIZACION.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_PROVEEDOR_COTIZACION.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato, Modelo.V_OTT_PROVEEDOR_COTIZACION.ANNO, Me.Anno, Modelo.V_OTT_PROVEEDOR_COTIZACION.NUMERO_GESTION, Me.NumeroGestion, Modelo.V_OTT_PROVEEDOR_COTIZACION.ADJUDICADO)
            vlo_DsAdjudicado = vlo_Wsr_OT_OrdenesDeTrabajo.OTT_PROVEEDOR_COTIZACION_ListarRegistros(System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB).ToString,
                                                                                                    System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB).ToString,
                                                                                                    vlc_Condicion, String.Empty, False, 0, 0)

            If vlo_DsAdjudicado IsNot Nothing AndAlso vlo_DsAdjudicado.Tables(0).Rows.Count > 0 Then
                'Se cargan los archivos adjuntos
                Me.UltimaCondicionBusquedaArchivo = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.V_OTT_ADJUNTO_COTIZACIONLST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_ADJUNTO_COTIZACIONLST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato, Modelo.V_OTT_ADJUNTO_COTIZACIONLST.ANNO, Me.Anno, Modelo.V_OTT_ADJUNTO_COTIZACIONLST.NUMERO_GESTION, Me.NumeroGestion, Modelo.V_OTT_ADJUNTO_COTIZACIONLST.IDENTIFICACION, vlo_DsAdjudicado.Tables(0).Rows(0).Item(Modelo.V_OTT_PROVEEDOR_COTIZACION.IDENTIFICACION))
                BuscarArchivo(Me.UltimaCondicionBusquedaArchivo, String.Empty)
                Me.btnTramitar.Enabled = True
            Else
                MostrarAlertaError("No ha sido posible encontrar el proveedor adjudicado")
                Me.btnTramitar.Enabled = False
            End If
            
        Catch ex As Exception
            Throw
        End Try
        
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Guarda la revisión de la solicitud
    ''' </summary>
    ''' <author>Mauricio Salas</author>
    ''' <remarks></remarks>
    Private Sub GuardarRevision()
        Dim vlo_Wsr_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlc_Condicion As String
        Dim vlo_Usuario As UsuarioActual
        Dim vln_Resultado As Integer
        Dim vlo_EntOttGestionCompra As EntOttGestionCompra
        Dim vlo_EntOtlTrazabilGestionComp As EntOtlTrazabilGestionComp
        Dim vlb_EnviarCorreo As Boolean

        Try

            vlo_Wsr_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
            vlo_Wsr_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Wsr_OT_OrdenesDeTrabajo.Timeout = -1

            vlo_Usuario = New UsuarioActual
            vlo_EntOtlTrazabilGestionComp = New EntOtlTrazabilGestionComp

            'se construye la entidad a guardar
            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_GESTION_COMPRA.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato, Modelo.OTT_GESTION_COMPRA.ANNO, Me.Anno, Modelo.OTT_GESTION_COMPRA.NUMERO_GESTION, Me.NumeroGestion)
            vlo_EntOttGestionCompra = vlo_Wsr_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_ObtenerRegistro(System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB).ToString, System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB).ToString, vlc_Condicion)

            If vlo_EntOttGestionCompra.Existe Then
                vlo_EntOttGestionCompra.Estado = IIf(Me.rdbAprobado.Checked, Utilerias.OrdenesDeTrabajo.EstadoGestionCompra.GESTION_DE_CHEQUE, Utilerias.OrdenesDeTrabajo.EstadoGestionCompra.APROBACION_DEL_SUPERVISOR)
            End If

            With vlo_EntOtlTrazabilGestionComp
                .IdUbicacion = Me.IdUbicacion
                .IdViaCompraContrato = Me.IdViaCompraContrato
                .Anno = Me.Anno
                .NumeroGestion = Me.NumeroGestion
                .Estado = Utilerias.OrdenesDeTrabajo.EstadoGestionCompra.APROBACION_DE_JEFATURA
                .Observaciones = Me.txtObservaciones.Text
                .Usuario = vlo_Usuario.UserName
            End With

            'Enviar notificacion si se decide aprobar la Gestion de Compra
            If Me.rdbAprobado.Checked Then
                vlb_EnviarCorreo = True
            Else
                vlb_EnviarCorreo = False
            End If

            vln_Resultado = vlo_Wsr_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_FondoTrabajoAprobacionJefatura(System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB).ToString, System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB).ToString, vlo_EntOttGestionCompra, vlo_EntOtlTrazabilGestionComp, vlb_EnviarCorreo)


            If vln_Resultado > 0 Then
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
                Else
                    MostrarAlertaError("No ha sido posible tramitar la gestión de compra")
                End If

        Catch ex As Exception
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' Método encargado de realizar la búsqueda de registros según los datos de 
    ''' condición y orden de búsqueda
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    Private Sub BuscarArchivo(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = String.Format("{0} {1}", V_OTT_ADJUNTO_COTIZACIONLST.NOMBRE_ARCHIVO, Ordenamiento.ASCENDENTE)
        End If
        Me.UltimoSortExpressionArchivo = pvc_Orden

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntDatosPaginacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_COTIZACION_ObtenerDatosPaginacionVOttAdjuntoCotizacionlst(
                                             ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                             ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                             pvc_Condicion,
                                             pvc_Orden,
                                             CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer)
                                            )

            Me.pnRpArchivo.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.lblCantidadDeRegistrosArchivo.Visible = True
                Me.lblCantidadDeRegistrosArchivo.Text = String.Format("Cantidad de registros {0}", vlo_EntDatosPaginacion.TotalRegistros)
                Me.lblNoHayDAtosArchivo.Visible = False
                CargarListaArchivo(pvc_Condicion, pvc_Orden, 1)
            Else
                Me.lblCantidadDeRegistrosArchivo.Visible = False
                Me.lblCantidadDeRegistrosArchivo.Text = String.Empty
                Me.lblNoHayDAtosArchivo.Visible = True
                Me.rpArchivo.Visible = False
            End If

            Me.pnRpArchivo.Dibujar()

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
    ''' <param name="pvc_Condicion">parámetro con la condicion de búsqueda</param>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>01/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaArchivo(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            DsAdjuntos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_COTIZACION_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If DsAdjuntos IsNot Nothing AndAlso DsAdjuntos.Tables(0).Rows.Count > 0 Then
                With Me.rpArchivo
                    .DataSource = DsAdjuntos
                    .DataMember = DsAdjuntos.Tables(0).TableName
                    .DataBind()
                    .Visible = True
                End With
                Me.lblNoHayDAtosArchivo.Visible = False

            Else
                With Me.rpArchivo
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With
                Me.lblNoHayDAtosArchivo.Visible = True
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If

            If DsAdjuntos IsNot Nothing Then
                DsAdjuntos.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarListaAcordeon(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_GESTION_COMPRA_ListarVOtLineaGestCompGroupPersonalizado(
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


#End Region

#Region "Funciones"
    Private Function ObtenerExpresionDeOrdenamientoArchivo(pvc_Columna As String) As String
        If String.IsNullOrWhiteSpace(UltimoSortColumnArchivo) OrElse pvc_Columna.CompareTo(UltimoSortColumnArchivo) <> 0 Then
            UltimoSortColumnArchivo = pvc_Columna
            UltimoSortDirectionArchivo = SortDirection.Ascending
        Else
            If UltimoSortDirectionArchivo = SortDirection.Ascending Then
                UltimoSortDirectionArchivo = SortDirection.Descending
            Else
                UltimoSortDirectionArchivo = SortDirection.Ascending
            End If
        End If

        UltimoSortExpressionArchivo = String.Format("{0} {1}", UltimoSortColumnArchivo, IIf(UltimoSortDirectionArchivo = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))
        Return UltimoSortExpressionArchivo
    End Function

    Private Function ObtenerCondicionBusquedaAcordeon() As String
        Dim vlc_Condicion As String
        vlc_Condicion = String.Empty

        vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_UBICACION, Me.IdUbicacion)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_LINEA_GEST_COMP_GROUP.ANNO, Me.Anno)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_LINEA_GEST_COMP_GROUP.NUMERO_GESTION, Me.NumeroGestion)

        Return vlc_Condicion
    End Function
#End Region
End Class
