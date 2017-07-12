Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports System.IO

Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_RegistroValidacionMontos
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

    Public Property GestionDespliegue As String
        Get
            Return CType(ViewState("GestionDespliegue"), String)
        End Get
        Set(value As String)
            ViewState("GestionDespliegue") = value
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
                        Me.lblProveedor.Visible = True
                        Me.thProveedor.Visible = True
                    ElseIf vlc_UnidadEspecializada = Me.IdViaCompra Then
                        Me.lblProveedor.Visible = True
                        Me.thProveedor.Visible = True
                    Else
                        Me.thProveedor.Visible = False
                        Me.lblProveedor.Visible = False
                    End If

                    Me.btnDevolver.Visible = False

                    CargarAdjuntos()

                    Me.Condicion = String.Empty

                    vlc_Condicion = String.Format("{0} = {1}  AND {2} = {3}  AND {4} = {5}  AND {6} = {7}", Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_UBICACION, Me.IdUbicacion, Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra, Modelo.V_OT_LINEA_GC_GROUP_FONDO.ANNO, Me.Anno, Modelo.V_OT_LINEA_GC_GROUP_FONDO.NUMERO_GESTION, Me.NumeroGestion)

                    CargarEncabezado(vlc_Condicion, String.Format("{0} ASC", Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_MATERIAL))

                    CargarDetalle(vlc_Condicion, String.Format("{0} ASC", Modelo.V_OTT_DETALLE_GESTION_INGRLST.NUMERO_OT))


                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ninguna ubicación autorizada, contactar con el administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If

            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    Protected Sub chkAsignar_CheckedChanged(sender As Object, e As EventArgs) Handles chkAsignar.CheckedChanged
        If Me.chkAsignar.Checked Then
            Me.btnFinalizar.Visible = False
            Me.btnGuardar.Visible = False
            Me.btnDevolver.Visible = True
        Else
            Me.btnFinalizar.Visible = True
            Me.btnGuardar.Visible = True
            Me.btnDevolver.Visible = False
        End If
    End Sub

    Protected Sub btnDevolver_Click(sender As Object, e As EventArgs) Handles btnDevolver.Click
        If Page.IsValid Then
            Try
                If Me.txtObs.Text = String.Empty Then
                    MostrarAlertaError("Debe indicar el motivo de devolución")
                Else
                    If DevolverGestionIngreso() Then
                        WebUtils.RegistrarScript(Me, "alertaExitosa", "mostrarAlertaExitosa();")
                    Else
                        MostrarAlertaError("Se ha producido un error al tramitar el registro")
                    End If
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

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Page.IsValid Then
            Try
                If GuardarCostos() Then
                    WebUtils.RegistrarScript(Me, "alertaExito", "mostrarGuardadoExitoso();")
                Else
                    MostrarAlertaError("Se ha producido un error al tramitar el registro")
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
    ''' Boton siguiente para ver como va a quedar el registro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>09/02/2017</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click
        If Page.IsValid Then
            Try
                If VerificarCantidadIng() Then
                    If TramitarMontos() Then
                        WebUtils.RegistrarScript(Me, "alertaExitosa", "mostrarAlertaExitosa();")
                    Else
                        MostrarAlertaError("Se ha producido un error al tramitar el registro")
                    End If
                    
                Else
                    MostrarAlertaError("Debe indicar el costo de todos los materiales incluidos en la gestión de ingreso")
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

    Protected Sub txtCantidadEnca_OnTextChanged(sender As Object, e As EventArgs)
        Try
            Dim vlo_txtCantidad As TextBox = CType(sender, TextBox)
            Dim vlo_Row As Data.DataRow
            Dim vlc_Cantidad As String
            Dim vln_CantidadAux As Double

            vlc_Cantidad = vlo_txtCantidad.Text

            vlo_Row = Me.DsEnca.Tables(0).Rows.Find(New Object() {vlo_txtCantidad.Attributes("data-inf").ToString()})

            If Not String.IsNullOrWhiteSpace(vlc_Cantidad) Then
                If IsNumeric(vlc_Cantidad) Then
                    vlo_Row("COSTO_ING") = vlc_Cantidad

                    vln_CantidadAux = CType(vlc_Cantidad, Double) / vlo_Row(Modelo.V_OT_DET_GEST_ING_GROUP.CANTIDAD_INGRESADA)
                    'Buscar en el otro Ds para actualizar
                    For Each vlo_Fila As Data.DataRow In Me.DsDetalle.Tables(0).Rows
                        If vlo_Row.Item(Modelo.V_OT_DET_GEST_ING_GROUP.ID_MATERIAL) = vlo_Fila.Item(Modelo.V_OTT_DETALLE_GESTION_INGRLST.ID_MATERIAL_TABLA) Then
                            vlo_Fila.Item(Modelo.V_OTT_DETALLE_GESTION_INGRLST.COSTO_INDIVIDUAL) = vln_CantidadAux
                        End If
                    Next

                Else
                    vlo_Row("COSTO_ING") = String.Empty
                    MostrarAlertaError("Debe digitar un número válido")
                End If

            Else
                If Not TypeOf vlo_Row.Item("COSTO_ING") Is DBNull AndAlso vlo_Row.Item("COSTO_ING") <> String.Empty Then
                    For Each vlo_Fila As Data.DataRow In Me.DsDetalle.Tables(0).Rows
                        If vlo_Row.Item(Modelo.V_OT_DET_GEST_ING_GROUP.ID_MATERIAL) = vlo_Fila.Item(Modelo.V_OTT_DETALLE_GESTION_INGRLST.ID_MATERIAL_TABLA) Then
                            vlo_Fila.Item(Modelo.V_OTT_DETALLE_GESTION_INGRLST.COSTO_INDIVIDUAL) = 0
                        End If
                    Next
                End If
                vlo_Row("COSTO_ING") = String.Empty
            End If

            If Me.DsEnca IsNot Nothing AndAlso Me.DsEnca.Tables(0).Rows.Count > 0 Then
                With Me.rpEnca
                    .DataSource = Me.DsEnca
                    .DataMember = Me.DsEnca.Tables(0).TableName
                    .DataBind()
                End With
            End If

            If Me.Condicion = String.Empty Then
                Me.Condicion = String.Format("{0} = {1}  AND {2} = {3}  AND {4} = {5}  AND {6} = {7} AND {8} = {9}", Modelo.V_OTT_DETALLE_GESTION_INGRLST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_DETALLE_GESTION_INGRLST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra, Modelo.V_OTT_DETALLE_GESTION_INGRLST.ANNO, Me.Anno, Modelo.V_OTT_DETALLE_GESTION_INGRLST.NUMERO_GESTION, Me.NumeroGestion, Modelo.V_OTT_DETALLE_GESTION_INGRLST.ID_MATERIAL_TABLA, vlo_txtCantidad.Attributes("data-inf").ToString())
                CargarDetalleSelec(Me.Condicion)
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
            Me.lblNumeroGestion.Text = WebUtils.LeerParametro(Of String)("pvc_GestionDespliegue")
            Me.lblProveedor.Text = WebUtils.LeerParametro(Of String)("pvc_NombreProveedor")
            Me.lblFecha.Text = WebUtils.LeerParametro(Of String)("pvc_Fecha")
            Me.lblViaCompra.Text = WebUtils.LeerParametro(Of String)("pvc_DescViaCompra")
            Me.txtObservaciones.Text = WebUtils.LeerParametro(Of String)("pvc_Observaciones")
            Me.lblResponsable.Text = WebUtils.LeerParametro(Of String)("pvc_Responsable")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Private Function VerificarCantidadIng() As Boolean
        Dim vlb_Completo As Boolean
        Try
            For Each vlo_fila In DsEnca.Tables(0).Rows
                If Not TypeOf vlo_fila.Item("COSTO_ING") Is DBNull AndAlso vlo_fila.Item("COSTO_ING") <> String.Empty Then
                    vlb_Completo = True

                Else
                    vlb_Completo = False
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
            Me.DsEnca = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_GESTION_INGR_ListarVOtDetGestIngGroup(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

            Me.DsEnca.Tables(0).Columns.Add("COSTO_ING", GetType(String))

            Me.DsEnca.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsEnca.Tables(0).Columns(Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_MATERIAL)}


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
        Dim vlo_RowEnca As Data.DataRow

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.DsDetalle = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_GESTION_INGR_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

            Me.DsDetalle.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsDetalle.Tables(0).Columns(Modelo.V_OTT_DETALLE_GESTION_INGRLST.ID_LINEA_GESTION_COMPRA)}

            If Me.DsDetalle IsNot Nothing AndAlso Me.DsDetalle.Tables(0).Rows.Count > 0 Then
                For Each vlo_FilaPantalla In Me.DsDetalle.Tables(0).Rows

                    vlo_RowEnca = Me.DsEnca.Tables(0).Rows.Find(New Object() {vlo_FilaPantalla.Item(Modelo.V_OTT_DETALLE_GESTION_INGRLST.ID_MATERIAL_TABLA)})

                    If Not TypeOf vlo_FilaPantalla(Modelo.V_OTT_DETALLE_GESTION_INGRLST.COSTO_INDIVIDUAL) Is DBNull AndAlso vlo_FilaPantalla(Modelo.V_OTT_DETALLE_GESTION_INGRLST.COSTO_INDIVIDUAL) <> 0 Then
                        If Not TypeOf vlo_RowEnca("COSTO_ING") Is DBNull AndAlso vlo_RowEnca("COSTO_ING") <> String.Empty Then
                            vlo_RowEnca("COSTO_ING") = CType(vlo_RowEnca("COSTO_ING"), Double) + CType(vlo_FilaPantalla(Modelo.V_OTT_DETALLE_GESTION_INGRLST.COSTO_INDIVIDUAL), Double)
                        Else
                            vlo_RowEnca("COSTO_ING") = vlo_FilaPantalla(Modelo.V_OTT_DETALLE_GESTION_INGRLST.COSTO_INDIVIDUAL)
                        End If
                    End If


                Next

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
            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_ADJUNTO_GESTION_INGR.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_ADJUNTO_GESTION_INGR.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra, Modelo.OTT_ADJUNTO_GESTION_INGR.ANNO, Me.Anno, Modelo.OTT_ADJUNTO_GESTION_INGR.NUMERO_GESTION, Me.NumeroGestion, Modelo.OTT_ADJUNTO_GESTION_INGR.CONSECUTIVO, Me.Consecutivo)

            Me.DsAdjuntos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_GESTION_INGR_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlc_Condicion,
                String.Format("{0} {1}", Modelo.OTT_ADJUNTO_GESTION_INGR.NOMBRE_ARCHIVO, Ordenamiento.ASCENDENTE),
                False, 0, 0)


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

    Private Function DevolverGestionIngreso() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_INGRESO_MATER_DevolverGestionIngresoMatAlmacen(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.IdUbicacion, Me.IdViaCompra, Me.NumeroGestion, Me.Anno, Me.Consecutivo, Me.txtObs.Text, Me.Usuario.UserName) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    Private Function GuardarCostos() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_INGRESO_MATER_GuardarDetalle(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               Me.DsDetalle) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    Private Function TramitarMontos() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_INGRESO_MATER_TramitarValidacionMontos(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               Me.IdUbicacion, Me.IdViaCompra, Me.NumeroGestion, Me.Anno, Me.Consecutivo, Me.txtObs.Text, Me.Usuario.UserName, Me.DsDetalle, Me.AutorizadoUbicacion.IdUbicacionAdministra) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function
#End Region
End Class
