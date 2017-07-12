Imports Utilerias.OrdenesDeTrabajo

Partial Class Controles_wuc_OT_DetalleGestionCompraFondo
    Inherits System.Web.UI.UserControl
#Region "Propiedades"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>25/08/2016</creationDate>
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
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>01/09/2016</creationDate>
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
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>01/09/2016</creationDate>
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
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>01/09/2016</creationDate>
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
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>01/09/2016</creationDate>
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
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>01/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property IdMaterial As Integer
        Get
            Return CType(ViewState("IdMaterial"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdMaterial") = value
        End Set
    End Property

#End Region

#Region "Eventos"

#End Region

#Region "Métodos"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>25/08/2016</creationDate>
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
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>01/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Sub Inicializar()
        CargarGestionCompra()
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
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>01/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaLineasMaterialGestion(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_IdAnnio As Integer, pvn_IdNumeroGestion As Integer, pvn_IdMaterial As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet
        Dim vlo_DsDatosOfertaProv As Data.DataSet
        Dim vlo_DsDatosAcordeonPri As Data.DataSet
        Dim vlo_EntOttGrupoGestionCompra As Wsr_OT_OrdenesDeTrabajo.EntOttGrupoGestionCompra
        Dim vlo_Row As Data.DataRow
        Dim vlo_Fila As Data.DataRow
        Dim vlc_Proveedores As String

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

            
            vlo_DsDatosAcordeonPri = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_GESTION_COMPRA_ListarVOtLineaGestCompGroupPersonalizado(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.V_OTT_OFERTA_PROVEEDORLST.ID_UBICACION, pvn_IdUbicacion, Modelo.V_OTT_OFERTA_PROVEEDORLST.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.V_OTT_OFERTA_PROVEEDORLST.ANNO, pvn_IdAnnio, Modelo.V_OTT_OFERTA_PROVEEDORLST.NUMERO_GESTION, pvn_IdNumeroGestion),
                String.Empty,
                False,
                0,
                0)

            
            vlo_DsDatos.Tables(0).Columns.Add("PROVEEDORES")

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then

                For Each vlo_Row In vlo_DsDatos.Tables(0).Rows
                    'Se obtiene el ID_GRUPO_GESTION_COMPRA para obtener la lista de ofertas para esa linea de la gestion de compra
                    vlo_EntOttGrupoGestionCompra = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GRUPO_GESTION_COMPRA_ObtenerRegistro(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.V_OTT_GRUPO_GESTION_COMPRA.ID_UBICACION, pvn_IdUbicacion, Modelo.V_OTT_GRUPO_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.V_OTT_GRUPO_GESTION_COMPRA.ANNO, pvn_IdAnnio, Modelo.V_OTT_GRUPO_GESTION_COMPRA.NUMERO_GESTION, pvn_IdNumeroGestion, Modelo.V_OTT_GRUPO_GESTION_COMPRA.ID_MATERIAL, vlo_Row(Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL_TABLA)))

                    'Se obtiene las ofertas para la linea de gestion de compra
                    vlo_DsDatosOfertaProv = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OFERTA_PROVEEDOR_ListarRegistrosLista(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.V_OTT_OFERTA_PROVEEDORLST.ID_UBICACION, pvn_IdUbicacion, Modelo.V_OTT_OFERTA_PROVEEDORLST.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.V_OTT_OFERTA_PROVEEDORLST.ANNO, pvn_IdAnnio, Modelo.V_OTT_OFERTA_PROVEEDORLST.NUMERO_GESTION, pvn_IdNumeroGestion, Modelo.V_OTT_OFERTA_PROVEEDORLST.ID_GRUPO_GESTION_COMPRA, vlo_EntOttGrupoGestionCompra.IdGrupoGestionCompra),
                        String.Empty,
                        False,
                        0,
                        0)

                    If vlo_DsDatosOfertaProv IsNot Nothing AndAlso vlo_DsDatosOfertaProv.Tables(0).Rows.Count > 0 Then
                        For Each vlo_Fila In vlo_DsDatosOfertaProv.Tables(0).Rows
                            vlc_Proveedores = String.Format("{0}{1}: {2}&nbsp;&nbsp;&nbsp;&nbsp;", vlc_Proveedores, vlo_Fila(Modelo.V_OTT_OFERTA_PROVEEDORLST.NOMBRE_PROVEEDOR), vlo_Fila(Modelo.V_OTT_OFERTA_PROVEEDORLST.MONTO) / vlo_EntOttGrupoGestionCompra.CantidadSolicitada * vlo_Row(Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_SOLICITADA))
                        Next
                        vlo_Row("PROVEEDORES") = vlc_Proveedores
                    End If

                Next

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

#End Region
End Class
