Imports System.Data
Imports Wsr_OT_OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo.Modelo
Imports Utilerias.OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_UnidadEspAprobSupervisor
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

    Public Property GestionCompra As EntOttGestionCompra
        Get
            Return CType(ViewState("GestionCompra"), EntOttGestionCompra)
        End Get
        Set(ByVal value As EntOttGestionCompra)
            ViewState("GestionCompra") = value
        End Set
    End Property

#End Region

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try

                Me.IdUbicacion = CType(Session("pvn_IdUbicacion"), Integer)
                Me.IdViaCompraContrato = CType(Session("pvn_IdViaCompraContrato"), Integer)
                Me.Anno = CType(Session("pvn_Anno"), Integer)
                Me.NumeroGestion = CType(Session("pvn_NumeroGestion"), Integer)

                CargarGestionCompra()
                If Me.GestionCompra.Observaciones <> String.Empty Or Me.GestionCompra.Observaciones <> "-" Then
                    trObservaciones.Visible = True
                    Me.txtObs.Text = Me.GestionCompra.Observaciones
                Else
                    trObservaciones.Visible = False
                End If

                Me.lblNumGestion.Text = Me.GestionCompra.NumeroGestion

                'Se carga la informacion del acordeon
                CargarListaAcordeon(ObtenerCondicionBusquedaAcordeon(), String.Empty)


            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    Protected Sub rpMateriales_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpMateriales.ItemDataBound
        Dim vlo_HtmlAnchor As HtmlAnchor
        Dim vlo_HtmlGenericControl As HtmlGenericControl
        Dim vlo_WebUserControl As Controles_wuc_OT_Lineas_Material_Gestion_Compra

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_HtmlAnchor = CType(e.Item.FindControl("ancorAcordeon"), HtmlAnchor)
            vlo_HtmlGenericControl = e.Item.FindControl("cuerpoAcordeon1")
            vlo_HtmlAnchor.HRef = "#" + vlo_HtmlGenericControl.ClientID

            vlo_WebUserControl = CType(e.Item.FindControl("wuc_OT_Lineas_Material_Gestion_Compra"), Controles_wuc_OT_Lineas_Material_Gestion_Compra)
            vlo_WebUserControl.Inicializar()

        End If
    End Sub

    ''' <summary>
    ''' Evento del botón aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <author>Mauricio Salas</author>
    ''' <remarks></remarks>
    Protected Sub btnTramitar_Click(sender As Object, e As EventArgs) Handles btnTramitar.Click
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

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub CargarGestionCompra()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.GestionCompra = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_UBICACION, Me.IdUbicacion, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ANNO, Me.Anno, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_GESTION, Me.NumeroGestion))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
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
                vlo_EntOttGestionCompra.Estado = IIf(Me.rdbAprobado.Checked, Utilerias.OrdenesDeTrabajo.EstadoGestionCompra.APROBACION_JEFE_ADMINISTRATIVO, Utilerias.OrdenesDeTrabajo.EstadoGestionCompra.DEVUELTA_GESTOR_INVENTARIO)
            End If

            With vlo_EntOtlTrazabilGestionComp
                .IdUbicacion = Me.IdUbicacion
                .IdViaCompraContrato = Me.IdViaCompraContrato
                .Anno = Me.Anno
                .NumeroGestion = Me.NumeroGestion
                .Estado = Utilerias.OrdenesDeTrabajo.EstadoGestionCompra.APROBACION_DEL_SUPERVISOR
                .Observaciones = Me.txtObservaciones.Text
                .Usuario = vlo_Usuario.UserName
            End With


            vln_Resultado = vlo_Wsr_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_UnidadEspAprobSupervisor(System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB).ToString, System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB).ToString, vlo_EntOttGestionCompra, vlo_EntOtlTrazabilGestionComp)

            If vln_Resultado > 0 Then
                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
            Else
                MostrarAlertaError("No ha sido posible tramitar la gestión de compra")
            End If

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub CargarListaAcordeon(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GRUPO_GESTION_COMPRA_ListarRegistrosLista(
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

        vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ID_UBICACION, Me.IdUbicacion)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ANNO, Me.Anno)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTT_GRUPO_GESTION_COMPRALST.NUMERO_GESTION, Me.NumeroGestion)

        Return vlc_Condicion
    End Function
#End Region
End Class
