Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports System.IO

Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_ConsultaIngresoMatAlmacen
    Inherits System.Web.UI.Page
#Region "Propiedades"
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

    Public Property Consecutivo As Integer
        Get
            Return CType(ViewState("Consecutivo"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Consecutivo") = value
        End Set
    End Property

    Public Property Condicion As String
        Get
            Return CType(ViewState("Condicion"), String)
        End Get
        Set(value As String)
            ViewState("Condicion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de archivos adjuntos que se mostrará al usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsAdjuntos As Data.DataSet
        Get
            Return CType(ViewState("DsAdjuntos"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsAdjuntos") = value
        End Set
    End Property

    Private Property DsDetalle As Data.DataSet
        Get
            Return CType(ViewState("DsDetalle"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsDetalle") = value
        End Set
    End Property

    Private Property DsDetalleAux As Data.DataSet
        Get
            Return CType(ViewState("DsDetalleAux"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsDetalleAux") = value
        End Set
    End Property

    Private Property DsEnca As Data.DataSet
        Get
            Return CType(ViewState("DsEnca"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsEnca") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
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

    Public Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    Public Property Modo As Integer
        Get
            Return CType(ViewState("Modo"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Modo") = value
        End Set
    End Property

    Public Property IdProveedor As String
        Get
            Return CType(ViewState("IdProveedor"), String)
        End Get
        Set(value As String)
            ViewState("IdProveedor") = value
        End Set
    End Property

    Public Property vlo_EntOttAdjuntoGestionIngr As EntOttAdjuntoGestionIngr
        Get
            Return CType(ViewState("vlo_EntOttAdjuntoGestionIngr"), EntOttAdjuntoGestionIngr)
        End Get
        Set(value As EntOttAdjuntoGestionIngr)
            ViewState("vlo_EntOttAdjuntoGestionIngr") = value
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
        Dim vlc_IdFondoTrabajo As String
        Dim vlc_UnidadEspecializada As String
        Dim vlo_EntOtpParametro As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Dim vlo_DsEnca As Data.DataSet

        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If AutorizadoUbicacion.Existe Then
                    LeerParametrosSession()

                    CargarViaCompra()

                    If Me.Modo = 1 Then 'si el modo es 1 aun no se ha concatenado el consecutivo para la gestion de ingreso de mat
                        ObtenerConsecutivo()
                    End If

                    Me.lblFecha.Text = DateTime.Now

                    vlo_EntOtpParametro = CargarParametro(Parametros.VALOR_SECUENCIA_FONDO_DE_TRABAJO)
                    vlc_IdFondoTrabajo = vlo_EntOtpParametro.Valor

                    vlo_EntOtpParametro = CargarParametro(Parametros.VALOR_SECUENCIA_UNIDAD_ESPECIALIZADA_COMPRAS)
                    vlc_UnidadEspecializada = vlo_EntOtpParametro.Valor

                    If vlc_IdFondoTrabajo = Me.IdViaCompra Then
                        Me.lblProveedor.Visible = True
                        Me.thProveedor.Visible = True
                        CargarProveedor()
                    ElseIf vlc_UnidadEspecializada = Me.IdViaCompra Then
                        Me.lblProveedor.Visible = True
                        Me.thProveedor.Visible = True
                        CargarProveedor()
                    Else
                        Me.lblProveedor.Visible = False
                        Me.thProveedor.Visible = False
                    End If

                    If Me.DsAdjuntos.Tables.Count > 0 AndAlso Me.DsAdjuntos.Tables(0).Rows.Count > 0 Then
                        With Me.rpAdjunto
                            .DataSource = Me.DsAdjuntos
                            .DataMember = Me.DsAdjuntos.Tables(0).TableName
                            .DataBind()
                            .Visible = True
                        End With
                    Else
                        With Me.rpAdjunto
                            .DataSource = Nothing
                            .DataBind()
                            .Visible = False
                        End With
                    End If

                    vlo_DsEnca = Me.DsEnca.Clone()

                    For Each vlo_Fila As Data.DataRow In Me.DsEnca.Tables(0).Rows
                        If Not TypeOf vlo_Fila.Item("CANTIDAD_ING") Is DBNull AndAlso vlo_Fila.Item("CANTIDAD_ING") <> String.Empty Then
                            vlo_DsEnca.Tables(0).ImportRow(vlo_Fila)
                        End If
                    Next

                    If vlo_DsEnca IsNot Nothing AndAlso vlo_DsEnca.Tables(0).Rows.Count > 0 Then
                        With Me.rpEnca
                            .DataSource = vlo_DsEnca
                            .DataMember = vlo_DsEnca.Tables(0).TableName
                            .DataBind()
                        End With
                    Else
                        With Me.rpEnca
                            .DataSource = Nothing
                            .DataBind()
                            .Visible = False
                        End With
                    End If

                    Me.DsDetalleAux = Me.DsDetalle.Clone()

                    For Each vlo_Fila As Data.DataRow In Me.DsDetalle.Tables(0).Rows
                        If Not TypeOf vlo_Fila.Item("CANTIDAD_ING") Is DBNull AndAlso vlo_Fila.Item("CANTIDAD_ING") <> String.Empty Then
                            Me.DsDetalleAux.Tables(0).ImportRow(vlo_Fila)
                        End If
                    Next

                    If Me.DsDetalleAux IsNot Nothing AndAlso Me.DsDetalleAux.Tables(0).Rows.Count > 0 Then
                        With Me.rpDetalle
                            .DataSource = Me.DsDetalleAux
                            .DataMember = Me.DsDetalleAux.Tables(0).TableName
                            .DataBind()
                            .Visible = False
                        End With
                    Else
                        With Me.rpDetalle
                            .DataSource = Nothing
                            .DataBind()
                            .Visible = False
                        End With
                    End If

                    Me.Condicion = String.Empty
                    
                    If Me.Operacion = eOperacion.Modificar Then
                        CargarIngresoMaterial()

                        CargarDetalleIngresoMateriales() 'se carga el detalle almacenado en base de datos para refrescar las cantidades de material por cada OT
                    Else

                    End If

                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ninguna ubicación autorizada, contactar con el administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If

            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnTramitar_Click(sender As Object, e As EventArgs) Handles btnTramitar.Click
        If Page.IsValid Then
            Try
                If Me.DsAdjuntos IsNot Nothing AndAlso Me.DsAdjuntos.Tables(0).Rows.Count > 0 Then
                    If Me.Operacion = eOperacion.Agregar Then
                        If CrearIngresoMaterial(True) Then
                            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
                        Else
                            MostrarAlertaError("No ha sido posible registrar el ajuste de inventario")
                        End If
                    ElseIf Me.Operacion = eOperacion.Modificar Then
                        If ModificarIngresoMaterial(True) Then
                            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
                        Else
                            MostrarAlertaError("No ha sido posible modificar el ajuste de inventario")
                        End If
                    End If
                Else
                    MostrarAlertaError("Debe ingresar al menos un adjunto")
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
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Page.IsValid Then
            Try
                If Me.DsAdjuntos IsNot Nothing AndAlso Me.DsAdjuntos.Tables(0).Rows.Count > 0 Then
                    If Me.Operacion = eOperacion.Agregar Then
                        If CrearIngresoMaterial(False) Then
                            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
                        Else
                            MostrarAlertaError("No ha sido posible registrar el ajuste de inventario")
                        End If
                    ElseIf Me.Operacion = eOperacion.Modificar Then
                        If ModificarIngresoMaterial(False) Then
                            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
                        Else
                            MostrarAlertaError("No ha sido posible modificar el ajuste de inventario")
                        End If
                    End If
                Else
                    MostrarAlertaError("Debe ingresar al menos un adjunto")
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
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Me.Session.Add("pvn_IdUbicacion", Me.IdUbicacion)
        Me.Session.Add("pvn_IdViaCompraContrato", Me.IdViaCompra)
        Me.Session.Add("pvn_Anno", Me.Anno)
        Me.Session.Add("pvn_NumeroGestion", Me.NumeroGestion)
        Me.Session.Add("pvn_Consecutivo", Me.Consecutivo)
        Me.Session.Add("pvn_Operacion", Me.Operacion)
        Me.Session.Add("pvn_Modo", 2)
        Me.Session.Add("pvo_DsEnca", Me.DsEnca)
        Me.Session.Add("pvo_DsDetalle", Me.DsDetalle)
        Me.Session.Add("pvo_DsAdjuntos", Me.DsAdjuntos)
        Me.Session.Add("pvc_IdProveedor", Me.IdProveedor)
        Me.Session.Add("pvc_Observaciones", Me.txtObservaciones.Text)
        Me.Session.Add("pvc_GestionDespliegue", Me.lblNumeroGestion.Text)
        Me.Session.Add("pvo_EntOttAdjuntoGestionIngr", Me.vlo_EntOttAdjuntoGestionIngr)

        Response.Redirect("Frm_OT_RegistroIngresoMatAlmacen.aspx", False)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            AgregarArchivo()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' descarga la imagen adjunta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivo_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsAdjuntos.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.OTT_ADJUNTO_GESTION_INGR.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CType(Me.DsAdjuntos.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.OTT_ADJUNTO_GESTION_INGR.ARCHIVO), Byte()))
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    Protected Sub ibConsultarGestion_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_IdMaterial As String
        Dim vlc_Condicion As String

        vlc_IdMaterial = CType(CType(sender, ImageButton).CommandArgument, String)

        vlc_Condicion = String.Format("{0} = {1}  AND {2} = {3}  AND {4} = {5}  AND {6} = {7} AND {8} = {9}", Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ANNO, Me.Anno, Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_GESTION, Me.NumeroGestion, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL_TABLA, vlc_IdMaterial)

        Me.Condicion = vlc_Condicion

        CargarDetalleSelec(vlc_Condicion)

    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al carar el repeater de adjuntos, asigna atributos a cada elemento del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpAdjunto_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpAdjunto.ItemDataBound
        Dim vlo_IbBorrar As ImageButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
            'If e.Item.FindControl("ibModificar") IsNot Nothing Then
            '    vlo_IbBorrar = CType(e.Item.FindControl("ibModificar"), ImageButton)
            '    vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            'End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar, ubicada en la 
    ''' primera columna de cada registro del listado de adjuntos, llama a la función borrar y muestra  un mensaje según 
    ''' la operacion efectuada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vln_Indice As Integer

        Try

            vln_Indice = Convert.ToInt32(CType(sender, ImageButton).CommandName)

            Me.DsAdjuntos.Tables(0).Rows(vln_Indice).Delete()
            Me.DsAdjuntos.Tables(0).AcceptChanges()

            If Me.DsAdjuntos IsNot Nothing AndAlso Me.DsAdjuntos.Tables(0).Rows.Count > 0 Then
                Me.rpAdjunto.DataSource = Me.DsAdjuntos
                Me.rpAdjunto.DataMember = Me.DsAdjuntos.Tables(0).TableName
                Me.rpAdjunto.DataBind()
                Me.rpAdjunto.Visible = True
            Else
                With Me.rpAdjunto
                    .DataSource = Nothing
                    .DataBind()
                    Me.rpAdjunto.Visible = False
                End With
            End If

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
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub LeerParametrosSession()
        Try
            Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
            Me.NumeroGestion = WebUtils.LeerParametro(Of Integer)("pvn_NumeroGestion")
            Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
            Me.IdViaCompra = WebUtils.LeerParametro(Of Integer)("pvn_IdViaCompraContrato")
            Me.Consecutivo = WebUtils.LeerParametro(Of Integer)("pvn_Consecutivo")
            Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
            Me.DsEnca = WebUtils.LeerParametro(Of Data.DataSet)("pvo_DsEnca")
            Me.DsDetalle = WebUtils.LeerParametro(Of Data.DataSet)("pvo_DsDetalle")
            Me.DsAdjuntos = WebUtils.LeerParametro(Of Data.DataSet)("pvo_DsAdjuntos")
            Me.IdProveedor = WebUtils.LeerParametro(Of String)("pvc_IdProveedor")
            Me.txtObservaciones.Text = WebUtils.LeerParametro(Of String)("pvc_Observaciones")
            Me.lblNumeroGestion.Text = WebUtils.LeerParametro(Of String)("pvc_GestionDespliegue")
            Me.Modo = WebUtils.LeerParametro(Of Integer)("pvn_Modo")
            Me.vlo_EntOttAdjuntoGestionIngr = WebUtils.LeerParametro(Of EntOttAdjuntoGestionIngr)("pvo_EntOttAdjuntoGestionIngr")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Private Sub CargarViaCompra()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlc_Condicion As String = String.Empty
        Dim vlo_EntOtmViaCompra As Wsr_OT_Catalogos.EntOtmViaCompraContrato

        vlc_Condicion = String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_VIA_COMPRA_CONTRATO.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra, Modelo.OTM_VIA_COMPRA_CONTRATO.ID_UBICACION, Me.IdUbicacion)

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtmViaCompra = vlo_Ws_OT_Catalogos.OTM_VIA_COMPRA_CONTRATO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlc_Condicion)

            If vlo_EntOtmViaCompra.Existe Then
                Me.lblViaCompra.Text = vlo_EntOtmViaCompra.Descripcion
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarProveedor()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlc_Condicion As String = String.Empty
        Dim vlo_EntOtmProveedor As Wsr_OT_Catalogos.EntOtmProveedor

        vlc_Condicion = String.Format("{0} = '{1}'", Modelo.OTM_PROVEEDOR.IDENTIFICACION, Me.IdProveedor)

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtmProveedor = vlo_Ws_OT_Catalogos.OTM_PROVEEDOR_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlc_Condicion)

            If vlo_EntOtmProveedor.Existe Then
                Me.lblProveedor.Text = vlo_EntOtmProveedor.Nombre
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    Private Sub ObtenerConsecutivo()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_Condicion As String = String.Empty
        Dim vln_Consecutivo As Integer

        vlc_Condicion = String.Format("{0} = '{1}'", Modelo.OTM_PROVEEDOR.IDENTIFICACION, Me.IdProveedor)

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vln_Consecutivo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_INGRESO_MATER_ObtenerFcOtConsecutivoIngresoMat(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.Anno, Me.IdUbicacion, Me.NumeroGestion, Me.IdViaCompra)

            Me.lblNumeroGestion.Text = String.Format("{0}-{1}", Me.lblNumeroGestion.Text, vln_Consecutivo + 1)
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarDetalleSelec(pvc_Condicion As String)

        Try

            Dim vlo_DataViewFDetalles As New Data.DataView(Me.DsDetalleAux.Tables(0))
            vlo_DataViewFDetalles.RowFilter = pvc_Condicion

            If vlo_DataViewFDetalles IsNot Nothing AndAlso vlo_DataViewFDetalles.Count > 0 Then
                With Me.rpDetalle
                    .DataSource = vlo_DataViewFDetalles
                    .DataMember = Me.DsDetalle.Tables(0).TableName
                    .DataBind()
                    .Visible = True
                    WebUtils.RegistrarScript(Me, "visibilidadPanel", "mostrarAreaDeListado();")
                End With
            Else
                With Me.rpDetalle
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                    WebUtils.RegistrarScript(Me, "visibilidadPanel", "mostrarAreaDeListado();")
                End With
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub CargarIngresoMaterial()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_dsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_dsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_INGRESO_MATER_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_GESTION_INGRESO_MATER.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_GESTION_INGRESO_MATER.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra, Modelo.OTT_GESTION_INGRESO_MATER.ANNO, Me.Anno, Modelo.OTT_GESTION_INGRESO_MATER.NUMERO_GESTION, Me.NumeroGestion, Modelo.OTT_GESTION_INGRESO_MATER.CONSECUTIVO, Me.Consecutivo),
                String.Empty,
                False, 0, 0)

            If vlo_dsDatos.Tables.Count > 0 AndAlso vlo_dsDatos.Tables(0).Rows.Count > 0 Then
                Me.lblViaCompra.Text = vlo_dsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_INGRESO_MATERLST.DESC_VIA_COMPRA)
                Me.lblNumeroGestion.Text = vlo_dsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_INGRESO_MATERLST.NUMERO_GESTION_COMPRA)
                Me.lblFecha.Text = vlo_dsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_INGRESO_MATERLST.FECHA_INGRESO_REGISTRO)
                If Not TypeOf vlo_dsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_INGRESO_MATERLST.OBSERVACIONES) Is DBNull AndAlso vlo_dsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_INGRESO_MATERLST.OBSERVACIONES) <> String.Empty Then
                    Me.txtObservaciones.Text = vlo_dsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_INGRESO_MATERLST.OBSERVACIONES)
                End If
                If Not TypeOf vlo_dsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_INGRESO_MATERLST.NOMBRE_PROVEEDOR) Is DBNull AndAlso vlo_dsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_INGRESO_MATERLST.NOMBRE_PROVEEDOR) <> String.Empty Then
                    Me.lblProveedor.Text = vlo_dsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_INGRESO_MATERLST.NOMBRE_PROVEEDOR)
                End If

            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarDetalleIngresoMateriales()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_GESTION_INGR_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Utilerias.OrdenesDeTrabajo.Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION, Me.IdUbicacion, Utilerias.OrdenesDeTrabajo.Modelo.OTT_DETALLE_GESTION_INGR.ANNO, Me.Anno, Utilerias.OrdenesDeTrabajo.Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra, Modelo.OTT_DETALLE_GESTION_INGR.NUMERO_GESTION, Me.NumeroGestion),
                String.Empty,
                False,
                0,
                0)

            For Each vlo_FilaDetalle In vlo_DsDatos.Tables(0).Rows
                For Each vlo_FilaPantalla In Me.DsDetalle.Tables(0).Rows
                    If vlo_FilaPantalla(Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_LINEA_GESTION_COMPRA) = vlo_FilaDetalle(Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA) Then
                        If Not TypeOf vlo_FilaDetalle(Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA) Is DBNull Then
                            vlo_FilaPantalla("CANTIDAD_ING") = vlo_FilaDetalle(Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA)
                        End If
                    End If
                Next
            Next

            If Me.DsDetalle IsNot Nothing AndAlso Me.DsDetalle.Tables(0).Rows.Count > 0 Then

                With Me.rpDetalle
                    .DataSource = Me.DsDetalle
                    .DataMember = Me.DsDetalle.Tables(0).TableName
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregarArchivo()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As Wsr_OT_Catalogos.EntOtmTipoDocumento
        Dim vlc_NombreArchivo As String
        Dim vlc_ExtensionArchivo As String
        Dim vlc_ExtensionesTipo As String()
        Dim vln_TamanoNombre As Integer
        Dim vln_TamArchivo As Integer
        Dim vln_limiteTamArchivo As Integer
        Dim vln_Resultado As Integer = 0
        Dim vlo_DrFila As Data.DataRow

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.GENERICO))


            vlc_ExtensionesTipo = vlo_EntOtmTipoDocumento.FormatosAdmitidos.Split(",")
            vlc_NombreArchivo = Me.ifInfo.FileName
            vlc_ExtensionArchivo = Path.GetExtension(vlc_NombreArchivo).Replace(".", "")
            vln_TamanoNombre = Modelo.OTT_ADJUNTO_GESTION_INGR.NOMBRE_ARCHIVO_BD_TAMANO
            vln_TamArchivo = Me.ifInfo.FileBytes.Length
            vln_limiteTamArchivo = vlo_EntOtmTipoDocumento.TamanioMaximo * 1048576

            If Me.DsAdjuntos IsNot Nothing AndAlso Me.DsAdjuntos.Tables(0).Rows.Count > 0 Then
                MostrarAlertaError("Solo se puede agregar una factura por cada gestión de ingreso de material")
            Else
                If (vlc_ExtensionesTipo.Contains(vlc_ExtensionArchivo.ToUpper)) Then

                    If vlc_NombreArchivo.Length < vln_TamanoNombre Then

                        If (vln_TamArchivo < vln_limiteTamArchivo) Then

                            vlo_EntOttAdjuntoGestionIngr.NumeroGestion = Me.NumeroGestion
                            vlo_EntOttAdjuntoGestionIngr.NombreArchivo = Me.ifInfo.FileName
                            vlo_EntOttAdjuntoGestionIngr.Archivo = Me.ifInfo.FileBytes
                            vlo_EntOttAdjuntoGestionIngr.IdViaCompraContrato = Me.IdViaCompra
                            vlo_EntOttAdjuntoGestionIngr.IdUbicacion = Me.IdUbicacion
                            vlo_EntOttAdjuntoGestionIngr.Usuario = Me.Usuario.UserName
                            vlo_EntOttAdjuntoGestionIngr.NumeroDocumento = Me.txtNumDocumento.Text
                            vlo_EntOttAdjuntoGestionIngr.IdTipoDocumento = vlo_EntOtmTipoDocumento.IdTipoDocumento
                            vlo_EntOttAdjuntoGestionIngr.Anno = Me.Anno

                            vlo_DrFila = Me.DsAdjuntos.Tables(0).NewRow
                            vlo_DrFila.Item(Modelo.OTT_ADJUNTO_GESTION_INGR.NUMERO_GESTION) = Me.NumeroGestion
                            vlo_DrFila.Item(Modelo.OTT_ADJUNTO_GESTION_INGR.NOMBRE_ARCHIVO) = Me.ifInfo.FileName
                            vlo_DrFila.Item(Modelo.OTT_ADJUNTO_GESTION_INGR.ARCHIVO) = Me.ifInfo.FileBytes
                            vlo_DrFila.Item(Modelo.OTT_ADJUNTO_GESTION_INGR.ID_VIA_COMPRA_CONTRATO) = Me.IdViaCompra
                            vlo_DrFila.Item(Modelo.OTT_ADJUNTO_GESTION_INGR.ID_UBICACION) = Me.IdUbicacion
                            vlo_DrFila.Item(Modelo.OTT_ADJUNTO_GESTION_INGR.USUARIO) = Me.Usuario.UserName
                            vlo_DrFila.Item(Modelo.OTT_ADJUNTO_GESTION_INGR.NUMERO_DOCUMENTO) = Me.txtNumDocumento.Text
                            vlo_DrFila.Item(Modelo.OTT_ADJUNTO_GESTION_INGR.ID_TIPO_DOCUMENTO) = vlo_EntOtmTipoDocumento.IdTipoDocumento
                            vlo_DrFila.Item(Modelo.OTT_ADJUNTO_GESTION_INGR.ANNO) = Me.Anno

                            Me.DsAdjuntos.Tables(0).Rows.Add(vlo_DrFila)

                            If Me.DsAdjuntos IsNot Nothing AndAlso Me.DsAdjuntos.Tables(0).Rows.Count > 0 Then
                                Me.rpAdjunto.DataSource = Me.DsAdjuntos
                                Me.rpAdjunto.DataMember = Me.DsAdjuntos.Tables(0).TableName
                                Me.rpAdjunto.DataBind()
                                Me.rpAdjunto.Visible = True
                            Else
                                With Me.rpAdjunto
                                    .DataSource = Nothing
                                    .DataBind()
                                    Me.rpAdjunto.Visible = False
                                End With
                            End If

                            Me.txtNumDocumento.Text = String.Empty
                        Else
                            MostrarAlertaError("El tamaño del archivo excede el máximo permitido.")
                        End If
                    Else
                        MostrarAlertaError("El nombre del archivo es demasiado largo.")
                    End If
                Else
                    MostrarAlertaError("No es una extensión permitida para el tipo de documento seleccionado.")
                End If
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
    Private Function CrearIngresoMaterial(pvb_Finalizar As Boolean) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_Resultado As String


        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlc_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_INGRESO_MATER_CrearIngresoMaterial(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.vlo_EntOttAdjuntoGestionIngr, Me.DsEnca, Me.DsDetalle, Me.IdUbicacion, Me.IdViaCompra, Me.Anno, Me.NumeroGestion, Me.IdProveedor, Me.txtObservaciones.Text, Me.Usuario.UserName, pvb_Finalizar)

            Return vlc_Resultado <> String.Empty

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    Private Function ModificarIngresoMaterial(pvb_Finalizar As Boolean) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_Resultado As String

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlc_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_INGRESO_MATER_ModificarIngresoMaterial(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.vlo_EntOttAdjuntoGestionIngr, Me.DsEnca, Me.DsDetalle, Me.IdUbicacion, Me.IdViaCompra, Me.Anno, Me.NumeroGestion, Me.Consecutivo, Me.IdProveedor, Me.txtObservaciones.Text, Me.Usuario.UserName, pvb_Finalizar)

            Return vlc_Resultado <> String.Empty

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga la ubicacion favorita
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarAutorizadoUbicacion(pvn_NumEmpleado As Integer) As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_AUTORIZADO_UBICACION_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTM_AUTORIZADO_UBICACION.NUM_EMPLEADO, pvn_NumEmpleado))
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    Private Function CargarParametro(pvn_IdParametro As Integer) As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Dim Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        Ws_OT_Catalogos.Timeout = -1
        Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, pvn_IdParametro))
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
