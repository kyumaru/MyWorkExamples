Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports System.IO

Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_RegistroIngresoMatAlmacen
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
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>09/02/2017</creationDate>
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
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>09/02/2017</creationDate>
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
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>09/02/2017</creationDate>
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

    Public Property GestionDespliegue As String
        Get
            Return CType(ViewState("GestionDespliegue"), String)
        End Get
        Set(value As String)
            ViewState("GestionDespliegue") = value
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
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>09/02/2017</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim vlc_Condicion As String
        Dim vlc_IdFondoTrabajo As String
        Dim vlc_UnidadEspecializada As String
        Dim vlo_EntOtpParametro As Wsr_OT_Catalogos.EntOtpParametroUbicacion

        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If AutorizadoUbicacion.Existe Then
                    vlo_EntOtpParametro = CargarParametro(Parametros.VALOR_SECUENCIA_FONDO_DE_TRABAJO)
                    vlc_IdFondoTrabajo = vlo_EntOtpParametro.Valor

                    vlo_EntOtpParametro = CargarParametro(Parametros.VALOR_SECUENCIA_UNIDAD_ESPECIALIZADA_COMPRAS)
                    vlc_UnidadEspecializada = vlo_EntOtpParametro.Valor

                    LeerParametrosSession()

                    If vlc_IdFondoTrabajo = Me.IdViaCompra Then
                        CargarComboProveedor()
                    ElseIf vlc_UnidadEspecializada = Me.IdViaCompra Then
                        CargarComboProveedor()
                    Else
                        Me.trProveedor.Visible = False
                    End If



                    CargarAdjuntos()

                    Me.Condicion = String.Empty

                    vlc_Condicion = String.Format("{0} = {1}  AND {2} = {3}  AND {4} = {5}  AND {6} = {7} AND {8} > 0", Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_UBICACION, Me.IdUbicacion, Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra, Modelo.V_OT_LINEA_GC_GROUP_FONDO.ANNO, Me.Anno, Modelo.V_OT_LINEA_GC_GROUP_FONDO.NUMERO_GESTION, Me.NumeroGestion, Modelo.V_OT_LINEA_GC_GROUP_FONDO.CANTIDAD_RESTANTE)

                    CargarEncabezado(vlc_Condicion, String.Format("{0} ASC", Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_MATERIAL))

                    CargarDetalle(vlc_Condicion, String.Format("{0} DESC, {1} ASC, {2} ASC", Modelo.V_OTT_LINEA_GESTION_COMPRALST.ES_EMERGENCIA, Modelo.V_OTT_LINEA_GESTION_COMPRALST.FECHA_SOLICITUD, Modelo.V_OTT_LINEA_GESTION_COMPRALST.FECHA_ASIGNACION))

                    If Me.Modo = 1 Then
                        If Me.Operacion = eOperacion.Modificar Then
                            CargarIngresoMaterial()

                            CargarDetalleIngresoMateriales() 'se carga el detalle almacenado en base de datos para refrescar las cantidades de material por cada OT

                            Me.ddlProveedor.Enabled = False
                        End If
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
    ''' Boton siguiente para ver como va a quedar el registro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>09/02/2017</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        If Page.IsValid Then
            Try
                If VerificarCantidadIng() Then
                    Me.Session.Add("pvn_IdUbicacion", Me.IdUbicacion)
                    Me.Session.Add("pvn_IdViaCompraContrato", Me.IdViaCompra)
                    Me.Session.Add("pvn_Anno", Me.Anno)
                    Me.Session.Add("pvn_NumeroGestion", Me.NumeroGestion)
                    Me.Session.Add("pvn_Consecutivo", Me.Consecutivo)
                    Me.Session.Add("pvn_Operacion", Me.Operacion)
                    Me.Session.Add("pvo_DsEnca", Me.DsEnca)
                    Me.Session.Add("pvo_DsDetalle", Me.DsDetalle)
                    Me.Session.Add("pvo_DsAdjuntos", Me.DsAdjuntos)
                    Me.Session.Add("pvc_IdProveedor", Me.ddlProveedor.SelectedValue)
                    Me.Session.Add("pvc_Observaciones", Me.txtObservaciones.Text)
                    Me.Session.Add("pvc_GestionDespliegue", Me.GestionDespliegue)
                    Me.Session.Add("pvn_Modo", Me.Modo)
                    Me.Session.Add("pvo_EntOttAdjuntoGestionIngr", Me.vlo_EntOttAdjuntoGestionIngr)

                    Response.Redirect("Frm_OT_ConsultaIngresoMatAlmacen.aspx", False)
                Else
                    MostrarAlertaError("Debe ingresar la cantidad de al menos un material")
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
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>09/02/2017</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Me.Session.Add("pvn_IdUbicacion", Me.IdUbicacion)
        Me.Session.Add("pvn_Anno", Me.Anno)
        Me.Session.Add("pvn_IdViaCompraContrato", Me.IdViaCompra)
        Me.Session.Add("pvn_NumeroGestion", Me.NumeroGestion)

        Response.Redirect("Lst_OT_GestionMaterialAlmacen.aspx", False)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
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
    ''' <author>Mauricio Salas Chaves</author>
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
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpAdjunto_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpAdjunto.ItemDataBound
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
    ''' primera columna de cada registro del listado de adjuntos, llama a la función borrar y muestra  un mensaje según 
    ''' la operacion efectuada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
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

    Protected Sub txtCantidadEnca_OnTextChanged(sender As Object, e As EventArgs)
        Try
            Dim vlo_txtCantidad As TextBox = CType(sender, TextBox)
            Dim vlo_Row As Data.DataRow
            Dim vlc_Cantidad As String
            Dim vln_CantidadAux As Integer

            vlc_Cantidad = vlo_txtCantidad.Text

            vlo_Row = Me.DsEnca.Tables(0).Rows.Find(New Object() {vlo_txtCantidad.Attributes("data-inf").ToString()})

            If Not String.IsNullOrWhiteSpace(vlc_Cantidad) Then
                If IsNumeric(vlc_Cantidad) Then
                    If vlc_Cantidad > vlo_Row.Item(Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_RESTANTE) Then
                        vlo_Row("CANTIDAD_ING") = String.Empty
                        MostrarAlertaError("La cantidad a ingresar no puede ser mayor a la solicitada")
                    Else
                        vlo_Row("CANTIDAD_ING") = vlc_Cantidad

                        vln_CantidadAux = CType(vlc_Cantidad, Integer)
                        'Buscar en el otro Ds para actualizar
                        For Each vlo_Fila As Data.DataRow In Me.DsDetalle.Tables(0).Rows
                            If vln_CantidadAux > 0 Then
                                If vlo_Row.Item(Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL) = vlo_Fila.Item(Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL) Then
                                    If vlo_Fila.Item(Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_RESTANTE) < vln_CantidadAux Then
                                        vlo_Fila("CANTIDAD_ING") = vlo_Fila.Item(Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_RESTANTE)
                                        vln_CantidadAux = vln_CantidadAux - vlo_Fila.Item(Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_RESTANTE)
                                    Else
                                        vlo_Fila("CANTIDAD_ING") = vln_CantidadAux
                                        vln_CantidadAux = vln_CantidadAux - vln_CantidadAux
                                    End If
                                End If
                            End If
                        Next
                    End If

                Else
                    vlo_Row("CANTIDAD_ING") = String.Empty
                    MostrarAlertaError("Debe digitar un número válido")
                End If

            Else
                If Not TypeOf vlo_Row.Item("CANTIDAD_ING") Is DBNull AndAlso vlo_Row.Item("CANTIDAD_ING") <> String.Empty Then
                    For Each vlo_Fila As Data.DataRow In Me.DsDetalle.Tables(0).Rows
                        If vlo_Row.Item(Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL) = vlo_Fila.Item(Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL) Then
                            vlo_Fila("CANTIDAD_ING") = String.Empty
                        End If
                    Next
                End If
                vlo_Row("CANTIDAD_ING") = String.Empty
            End If

            If Me.DsEnca IsNot Nothing AndAlso Me.DsEnca.Tables(0).Rows.Count > 0 Then
                With Me.rpEnca
                    .DataSource = Me.DsEnca
                    .DataMember = Me.DsEnca.Tables(0).TableName
                    .DataBind()
                End With
            End If

            If Me.Condicion = String.Empty Then
                Me.Condicion = String.Format("{0} = {1}  AND {2} = {3}  AND {4} = {5}  AND {6} = {7} AND {8} = {9}", Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ANNO, Me.Anno, Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_GESTION, Me.NumeroGestion, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL_TABLA, vlo_txtCantidad.Attributes("data-inf").ToString())
                CargarDetalleSelec(Me.Condicion)
            End If


        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub txtCantidadDet_OnTextChanged(sender As Object, e As EventArgs)
        Try
            Dim vlo_txtCantidad As TextBox = CType(sender, TextBox)
            Dim vlo_Row As Data.DataRow
            Dim vlo_RowEnca As Data.DataRow
            Dim vlc_Cantidad As String
            Dim vln_CantidadAux As Integer
            Dim vln_Aux As Integer

            vlc_Cantidad = vlo_txtCantidad.Text

            vlo_Row = Me.DsDetalle.Tables(0).Rows.Find(New Object() {vlo_txtCantidad.Attributes("data-inf").ToString()})

            If Not String.IsNullOrWhiteSpace(vlc_Cantidad) Then
                If IsNumeric(vlc_Cantidad) Then
                    vlo_RowEnca = Me.DsEnca.Tables(0).Rows.Find(New Object() {vlo_Row.Item(Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL)})

                    vln_CantidadAux = CType(vlc_Cantidad, Integer)

                    If Not TypeOf vlo_Row.Item("CANTIDAD_ING") Is DBNull AndAlso vlo_Row.Item("CANTIDAD_ING") <> String.Empty Then 'se verifica  si ya se ingreso una catidad anteriormente
                        If vlo_Row.Item("CANTIDAD_ING") > vln_CantidadAux Then 'si la cantidad ingresada es menor se realizan los ajustes
                            vln_Aux = vlo_Row.Item("CANTIDAD_ING") - vln_CantidadAux
                            vlo_Row.Item("CANTIDAD_ING") = vln_CantidadAux

                            vlo_RowEnca.Item("CANTIDAD_ING") = vlo_RowEnca.Item("CANTIDAD_ING") - vln_CantidadAux 'se ajusta el encabezado
                        ElseIf vlo_Row.Item("CANTIDAD_ING") < vln_CantidadAux Then 'si la cantidad es mayor se realizan los ajustes
                            vln_Aux = vln_CantidadAux - vlo_Row.Item("CANTIDAD_ING")

                            If vlo_RowEnca.Item("CANTIDAD_ING") + vln_Aux > vlo_Row.Item(Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_RESTANTE) Then 'se verifica que el nuevo total no sobrepase el solicitado
                                vlo_Row("CANTIDAD_ING") = String.Empty
                                MostrarAlertaError("La cantidad a ingresar no puede ser mayor a la solicitada total")
                            Else
                                If vln_CantidadAux > vlo_Row.Item(Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_RESTANTE) Then
                                    vlo_Row("CANTIDAD_ING") = String.Empty
                                    MostrarAlertaError("La cantidad a ingresar no puede ser mayor a la solicitada")
                                Else
                                    vlo_RowEnca.Item("CANTIDAD_ING") = vlo_RowEnca.Item("CANTIDAD_ING") + vln_Aux
                                    vlo_Row.Item("CANTIDAD_ING") = vln_CantidadAux
                                End If

                            End If

                        End If
                    Else
                        If vln_CantidadAux > vlo_Row.Item(Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_RESTANTE) Then
                            vlo_Row("CANTIDAD_ING") = String.Empty
                            MostrarAlertaError("La cantidad a ingresar no puede ser mayor a la solicitada")
                        Else
                            If Not TypeOf vlo_RowEnca.Item("CANTIDAD_ING") Is DBNull AndAlso vlo_RowEnca.Item("CANTIDAD_ING") <> String.Empty Then
                                vlo_RowEnca.Item("CANTIDAD_ING") = vlo_RowEnca.Item("CANTIDAD_ING") + vln_CantidadAux
                            Else
                                vlo_RowEnca.Item("CANTIDAD_ING") = vln_CantidadAux
                            End If

                            vlo_Row.Item("CANTIDAD_ING") = vln_CantidadAux
                        End If
                    End If

                Else
                    vlo_Row("CANTIDAD_ING") = String.Empty
                    MostrarAlertaError("Debe digitar un número válido")
                End If

            Else
                If Not TypeOf vlo_Row.Item("CANTIDAD_ING") Is DBNull AndAlso vlo_Row.Item("CANTIDAD_ING") <> String.Empty Then
                    vln_Aux = vlo_Row.Item("CANTIDAD_ING")

                    vlo_RowEnca = Me.DsEnca.Tables(0).Rows.Find(New Object() {vlo_Row.Item(Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL)})

                    If Not TypeOf vlo_RowEnca.Item("CANTIDAD_ING") Is DBNull AndAlso vlo_RowEnca.Item("CANTIDAD_ING") <> String.Empty Then
                        vlo_RowEnca.Item("CANTIDAD_ING") = vlo_RowEnca.Item("CANTIDAD_ING") - vln_Aux

                        If vlo_RowEnca.Item("CANTIDAD_ING") = 0 Then
                            vlo_RowEnca.Item("CANTIDAD_ING") = String.Empty
                        End If
                    End If
                End If
                vlo_Row("CANTIDAD_ING") = String.Empty
            End If



            If Me.DsEnca IsNot Nothing AndAlso Me.DsEnca.Tables(0).Rows.Count > 0 Then
                With Me.rpEnca
                    .DataSource = Me.DsEnca
                    .DataMember = Me.DsEnca.Tables(0).TableName
                    .DataBind()
                End With
            End If

            CargarDetalleSelec(Me.Condicion)

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
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>09/02/2017</creationDate>
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
            Me.Modo = WebUtils.LeerParametro(Of Integer)("pvn_Modo")
            Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
            Me.GestionDespliegue = WebUtils.LeerParametro(Of String)("pvc_GestionDespliegue")

            If Me.Modo = 2 Then
                Me.DsEnca = WebUtils.LeerParametro(Of Data.DataSet)("pvo_DsEnca")
                Me.DsDetalle = WebUtils.LeerParametro(Of Data.DataSet)("pvo_DsDetalle")
                Me.DsAdjuntos = WebUtils.LeerParametro(Of Data.DataSet)("pvo_DsAdjuntos")
                Me.ddlProveedor.SelectedValue = WebUtils.LeerParametro(Of String)("pvc_IdProveedor")
                Me.txtObservaciones.Text = WebUtils.LeerParametro(Of String)("pvc_Observaciones")
                Me.vlo_EntOttAdjuntoGestionIngr = WebUtils.LeerParametro(Of EntOttAdjuntoGestionIngr)("pvo_EntOttAdjuntoGestionIngr")
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Private Sub CargarComboProveedor()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlProveedor.Items.Clear()
            Me.ddlProveedor.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_PROVEEDOR_COTIZACION_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = 1", Modelo.OTT_PROVEEDOR_COTIZACION.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_PROVEEDOR_COTIZACION.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra, Modelo.OTT_PROVEEDOR_COTIZACION.ANNO, Me.Anno, Modelo.OTT_PROVEEDOR_COTIZACION.NUMERO_GESTION, Me.NumeroGestion, Modelo.OTT_PROVEEDOR_COTIZACION.ADJUDICADO),
                String.Empty,
                False,
                0,
                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlProveedor
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.NOMBRE_PROVEEDOR
                    .DataValueField = Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.IDENTIFICACION
                    .DataBind()
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

    Private Function VerificarCantidadIng() As Boolean
        Dim vlb_Completo = False
        Try
            For Each vlo_fila In DsEnca.Tables(0).Rows
                If Not TypeOf vlo_fila.Item("CANTIDAD_ING") Is DBNull AndAlso vlo_fila.Item("CANTIDAD_ING") <> String.Empty Then
                    vlb_Completo = True
                    Return vlb_Completo
                End If

            Next

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try

        Return vlb_Completo
    End Function

    Private Sub CargarEncabezado(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.Modo = 1 Then
                Me.DsEnca = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_GESTION_COMPRA_ListarVOtLineaGcGroupFondoPersonalizado(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

                Me.DsEnca.Tables(0).Columns.Add("CANTIDAD_ING", GetType(String))

                Me.DsEnca.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsEnca.Tables(0).Columns(Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_MATERIAL)}
            End If
            

            If Me.DsEnca IsNot Nothing AndAlso Me.DsEnca.Tables(0).Rows.Count > 0 Then
                With Me.rpEnca
                    .DataSource = Me.DsEnca
                    .DataMember = Me.DsEnca.Tables(0).TableName
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

    Private Sub CargarDetalle(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.Modo = 1 Then
                Me.DsDetalle = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_GESTION_COMPRA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

                Me.DsDetalle.Tables(0).Columns.Add("CANTIDAD_ING", GetType(String))

                Me.DsDetalle.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsDetalle.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_LINEA_GESTION_COMPRA)}
            End If
            


            If Me.DsDetalle IsNot Nothing AndAlso Me.DsDetalle.Tables(0).Rows.Count > 0 Then
                With Me.rpDetalle
                    .DataSource = Me.DsDetalle
                    .DataMember = Me.DsDetalle.Tables(0).TableName
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

            Dim vlo_DataViewFDetalles As New Data.DataView(Me.DsDetalle.Tables(0))
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarAdjuntos()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_Condicion As String

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.Modo = 1 Then
                If Me.Operacion = eOperacion.Modificar Then
                    vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_ADJUNTO_GESTION_INGR.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_ADJUNTO_GESTION_INGR.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra, Modelo.OTT_ADJUNTO_GESTION_INGR.ANNO, Me.Anno, Modelo.OTT_ADJUNTO_GESTION_INGR.NUMERO_GESTION, Me.NumeroGestion, Modelo.OTT_ADJUNTO_GESTION_INGR.CONSECUTIVO, Me.Consecutivo)
                Else
                    vlc_Condicion = "1 <> 1"
                End If

                Me.DsAdjuntos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_GESTION_INGR_ListarRegistrosLista(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlc_Condicion,
                    String.Format("{0} {1}", Modelo.OTT_ADJUNTO_GESTION_INGR.NOMBRE_ARCHIVO, Ordenamiento.ASCENDENTE),
                    False, 0, 0)

                Me.vlo_EntOttAdjuntoGestionIngr = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_GESTION_INGR_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlc_Condicion)
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

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
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
                    Me.txtObservaciones.Text = vlo_dsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_INGRESO_MATERLST.OBSERVACIONES)
                    Me.ddlProveedor.SelectedValue = vlo_dsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_INGRESO_MATERLST.IDENTIFICACION)
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
        Dim vlo_RowEnca As Data.DataRow

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

                        vlo_RowEnca = Me.DsEnca.Tables(0).Rows.Find(New Object() {vlo_FilaPantalla.Item(Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL)})

                        If Not TypeOf vlo_FilaDetalle(Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA) Is DBNull Then
                            vlo_FilaPantalla("CANTIDAD_ING") = vlo_FilaDetalle(Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA)
                        End If

                        If Not TypeOf vlo_RowEnca("CANTIDAD_ING") Is DBNull Then
                            vlo_RowEnca("CANTIDAD_ING") = CType(vlo_RowEnca("CANTIDAD_ING"), Double) + CType(vlo_FilaPantalla("CANTIDAD_ING"), Double)
                        Else
                            vlo_RowEnca("CANTIDAD_ING") = vlo_FilaPantalla("CANTIDAD_ING")
                        End If
                    End If
                Next
            Next

            If Me.DsEnca IsNot Nothing AndAlso Me.DsEnca.Tables(0).Rows.Count > 0 Then
                With Me.rpEnca
                    .DataSource = Me.DsEnca
                    .DataMember = Me.DsEnca.Tables(0).TableName
                    .DataBind()
                End With
            End If

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
    ''' <author>Mauricio Salas Chaves</author>
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

    ''' <summary>
    ''' carga la ubicacion favorita
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>09/02/2017</creationDate>
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
