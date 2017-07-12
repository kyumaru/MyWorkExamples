Imports System.Data  'para utilizar data set
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_GestionesDeCompra_Lst_OT_TrazabilidadGestionCompra
    Inherits System.Web.UI.Page
#Region "Propiedades"

    ''' <summary>
    ''' Ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortExpressionAcuerdo As String
        Get
            If ViewState("UltimoSortExpressionAcuerdo") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortExpressionAcuerdo"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortExpressionAcuerdo") = value
        End Set
    End Property

    ''' <summary>
    ''' Ultima columna de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortColumnAcuerdo As String
        Get
            If ViewState("UltimoSortColumnAcuerdo") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortColumnAcuerdo"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortColumnAcuerdo") = value
        End Set
    End Property

    ''' <summary>
    ''' Ultima direccion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortDirectionAcuerdo As SortDirection
        Get
            If ViewState("UltimoSortDirectionAcuerdo") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return SortDirection.Ascending
            End If
            Return CType(ViewState("UltimoSortDirectionAcuerdo"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("UltimoSortDirectionAcuerdo") = value
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

    Public Property Condicion As String
        Get
            Return CType(ViewState("Condicion"), String)
        End Get
        Set(ByVal value As String)
            ViewState("Condicion") = value
        End Set
    End Property

#End Region

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                InicializarFormulario()

            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try

        Me.pnRpTramite.Dibujar()
    End Sub

    Protected Sub pnRpAcuerdos_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpTramite.CambioDePagina
        Try
            CargarListaTramites(Me.Condicion, UltimoSortExpressionAcuerdo, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub lnkRpTramites_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarListaTramites(Me.Condicion, ObtenerSortExpressionAcuerdo(e.CommandName), pnRpTramite.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Metodos"
    Private Sub InicializarFormulario()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
        Try
            Me.IdUbicacion = CType(Session("pvn_IdUbicacion"), Integer)
            Me.IdViaCompraContrato = CType(Session("pvn_IdViaCompraContrato"), Integer)
            Me.Anno = CType(Session("pvn_Anno"), Integer)
            Me.NumeroGestion = CType(Session("pvn_NumeroGestion"), Integer)
            Me.txtPaginaRegreso.Text = CType(Session("pvc_PaginaRegreso"), String)

            Me.txtObservaciones.Enabled = False

            Me.Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.V_OTL_TRAZABIL_GESTION_COMPLST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTL_TRAZABIL_GESTION_COMPLST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato, Modelo.V_OTL_TRAZABIL_GESTION_COMPLST.ANNO, Me.Anno, Modelo.V_OTL_TRAZABIL_GESTION_COMPLST.NUMERO_GESTION, Me.NumeroGestion)

            'Se obtiene la Gestion de compra
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.Condicion,
                String.Empty,
                False,
                0,
                0)

            Me.lblTitulo.Text = String.Format("Seguimiento a Gestiones de Compra por {0}", vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_COMPRALST.DESC_VIA_COMPRA))
            Me.lblNumeroGestion.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_COMPRALST.NUMERO_GESTION_COMPRA)
            Me.txtObservaciones.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_COMPRALST.OBSERVACIONES)

            If Me.txtObservaciones.Text = String.Empty Then
                Me.trObservaciones.Visible = False
            Else
                Me.trObservaciones.Visible = True
            End If
            'Se obtiene la lista de tramites
            BuscarTramites(Me.Condicion, String.Format("{0} {1}", Modelo.V_OTT_GESTION_COMPRALST.TIME_STAMP, Ordenamiento.DESCENDENTE))

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Carga la lista de acuerdos
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    Private Sub CargarListaTramites(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTL_TRAZABIL_GESTION_COMP_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpTramites
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()

                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado();")
            Else
                With Me.rpTramites
                    .DataSource = Nothing
                    .DataBind()
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
    ''' Metodo para buscar con una condicion enviada por parametro
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    Private Sub BuscarTramites(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntDatosPaginacion As EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerSortExpressionAcuerdo(Modelo.V_OTL_TRAZABIL_GESTION_COMPLST.TIME_STAMP)
        End If

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntDatosPaginacion = vlo_Ws_OT_OrdenesDeTrabajo.OTL_TRAZABIL_GESTION_COMP_ObtenerDatosPaginacionVOtlTrazabilGestionComplst(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer)
                )

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpTramite.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                Me.pnRpTramite.Dibujar()
                lblCantidadRegistroAcuerdos.Visible = True
                lblCantidadRegistroAcuerdos.Text = String.Format("Cantidad de Trámites: {0}", vlo_EntDatosPaginacion.TotalRegistros)
                CargarListaTramites(pvc_Condicion, pvc_Orden, 1) '1 xq la lista siempre carga en esa posicion
                Me.lblNoHayDAtos.Visible = False
            Else
                Me.lblCantidadRegistroAcuerdos.Visible = False
                Me.lblCantidadRegistroAcuerdos.Text = String.Empty
                Me.lblNoHayDAtos.Visible = True
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

    ''' <summary>
    ''' Ordenar ascendente o descendente la lista
    ''' </summary>
    ''' <param name="pvc_NombreColumna"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ObtenerSortExpressionAcuerdo(pvc_NombreColumna As String) As String
        If String.IsNullOrWhiteSpace(UltimoSortColumnAcuerdo) OrElse pvc_NombreColumna.CompareTo(UltimoSortColumnAcuerdo) <> 0 Then
            UltimoSortColumnAcuerdo = pvc_NombreColumna
            UltimoSortDirectionAcuerdo = SortDirection.Ascending
        Else
            If UltimoSortDirectionAcuerdo = SortDirection.Ascending Then
                UltimoSortDirectionAcuerdo = SortDirection.Descending
            Else
                UltimoSortDirectionAcuerdo = SortDirection.Ascending

            End If
        End If
        '0 nombre de la columna y 1 direccion de ordenamiento
        UltimoSortExpressionAcuerdo = String.Format("{0} {1}", UltimoSortColumnAcuerdo, IIf(UltimoSortDirectionAcuerdo = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))
        Return UltimoSortExpressionAcuerdo
    End Function
#End Region
End Class
