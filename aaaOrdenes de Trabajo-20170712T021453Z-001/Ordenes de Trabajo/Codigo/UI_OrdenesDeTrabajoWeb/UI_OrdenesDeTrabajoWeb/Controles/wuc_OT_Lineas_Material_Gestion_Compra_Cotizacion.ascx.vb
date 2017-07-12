Imports Utilerias.OrdenesDeTrabajo

Partial Class Controles_wuc_OT_Lineas_Material_Gestion_Compra_Cotizacion
    Inherits System.Web.UI.UserControl

#Region "Propiedades"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property GestionCompra As Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra
        Get
            Return CType(ViewState("GestionCompra"), Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra)
        End Get
        Set(value As Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra)
            ViewState("GestionCompra") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para IdUbicacion de la linea
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property IdUbicacion As Integer
        Get
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para IdViaCompraContrato de la linea
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property IdViaCompraContrato As Integer
        Get
            Return CType(ViewState("IdViaCompraContrato"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdViaCompraContrato") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para IdAnnio de la linea
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property IdAnnio As Integer
        Get
            Return CType(ViewState("IdAnnio"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdAnnio") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para IdNumeroGestion de la linea
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property IdNumeroGestion As Integer
        Get
            Return CType(ViewState("IdNumeroGestion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdNumeroGestion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para IdMaterial de la linea
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property IdMaterial As Integer
        Get
            Return CType(ViewState("IdMaterial"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdMaterial") = value
        End Set
    End Property

    ''' <summary>
    ''' data set para materiales iniciales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsProveedorCotizacion As Data.DataSet
        Get
            Return CType(ViewState("DsProveedorCotizacion"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsProveedorCotizacion") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpProveedores_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpProveedores.ItemDataBound
        Dim vlo_TextBox As TextBox
        Dim vlo_HiddenField As HiddenField

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_TextBox = CType(e.Item.FindControl("txtCantidadProveedor"), TextBox)
            vlo_HiddenField = CType(e.Item.FindControl("hdfIdentificacion"), HiddenField)
            vlo_TextBox.Attributes.Add("data-identificacion", vlo_HiddenField.Value)
        End If
    End Sub

    Protected Sub txtCantidadProveedor_TextChanged(sender As Object, e As EventArgs)
        Dim vlo_TextBox As TextBox
        Dim vln_IdentificacionProveedor As Integer

        Try

            vlo_TextBox = CType(sender, TextBox)
            vln_IdentificacionProveedor = CType(vlo_TextBox.Attributes.Item("data-identificacion"), Integer)





        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarGestionCompra()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.GestionCompra = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_UBICACION, Me.IdUbicacion, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ANNO, Me.IdAnnio, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_GESTION, Me.IdNumeroGestion))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Llama los métodos que inicializan el formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Sub Inicializar()
        CargarGestionCompra()
        CargarListaProveedorCotizacion(Me.IdUbicacion, Me.IdViaCompraContrato, Me.IdAnnio, Me.IdNumeroGestion)
        CargarListaLineasMaterialGestion(Me.IdUbicacion, Me.IdViaCompraContrato, Me.IdAnnio, Me.IdNumeroGestion, Me.IdMaterial)
    End Sub

    ''' <summary>
    ''' carga la lista de lineas de material, de la gestion
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvn_IdViaCompraContrato"></param>
    ''' <param name="pvn_IdAnnio"></param>
    ''' <param name="pvn_IdNumeroGestion"></param>
    ''' <param name="pvn_IdMaterial"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaLineasMaterialGestion(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_IdAnnio As Integer, pvn_IdNumeroGestion As Integer, pvn_IdMaterial As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1

        Try
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_GESTION_COMPRA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_UBICACION, pvn_IdUbicacion, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ANNO, pvn_IdAnnio, Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_GESTION, pvn_IdNumeroGestion, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL, pvn_IdMaterial),
                String.Empty,
                False,
                0,
                0)

            vlo_DsDatos.Tables(0).Columns.Add("PROVEEDORES")

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then

                Me.rpLineas.DataSource = vlo_DsDatos
                Me.rpLineas.DataMember = vlo_DsDatos.Tables(0).TableName
                Me.rpLineas.DataBind()
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
    ''' carga la lista de lineas de material, de la gestion
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvn_IdViaCompraContrato"></param>
    ''' <param name="pvn_IdAnnio"></param>
    ''' <param name="pvn_IdNumeroGestion"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaProveedorCotizacion(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_IdAnnio As Integer, pvn_IdNumeroGestion As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1

        Try
            Me.DsProveedorCotizacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_PROVEEDOR_COTIZACION_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.ID_UBICACION, pvn_IdUbicacion, Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.ANNO, pvn_IdAnnio, Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.NUMERO_GESTION, pvn_IdNumeroGestion),
                String.Empty,
                False,
                0,
                0)

            If Me.DsProveedorCotizacion IsNot Nothing AndAlso Me.DsProveedorCotizacion.Tables(0).Rows.Count > 0 Then

                Me.rpProveedores.DataSource = Me.DsProveedorCotizacion
                Me.rpProveedores.DataMember = Me.DsProveedorCotizacion.Tables(0).TableName
                Me.rpProveedores.DataBind()
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


#End Region

End Class
