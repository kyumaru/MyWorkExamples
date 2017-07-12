Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports System.IO
Imports System.Data

Partial Class OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_ExclusionMaterialesGestion
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

    Public Property Condicion As String
        Get
            Return CType(ViewState("Condicion"), String)
        End Get
        Set(value As String)
            ViewState("Condicion") = value
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

    Private Property DsDetaExc As Data.DataSet
        Get
            Return CType(ViewState("DsDetaExc"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsDetaExc") = value
        End Set
    End Property

    Private Property DsEncaExc As Data.DataSet
        Get
            Return CType(ViewState("DsEncaExc"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsEncaExc") = value
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

    Public Property Identificacion As String
        Get
            Return CType(ViewState("Identificacion"), String)
        End Get
        Set(value As String)
            ViewState("Identificacion") = value
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

        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If AutorizadoUbicacion.Existe Then

                    LeerParametrosSession()

                    CargarGestionCompra()

                    CargarArchivoProveedor()

                    Me.Condicion = String.Empty

                    vlc_Condicion = String.Format("{0} = {1}  AND {2} = {3}  AND {4} = {5}  AND {6} = {7} AND {8} > 0", Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_UBICACION, Me.IdUbicacion, Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra, Modelo.V_OT_LINEA_GC_GROUP_FONDO.ANNO, Me.Anno, Modelo.V_OT_LINEA_GC_GROUP_FONDO.NUMERO_GESTION, Me.NumeroGestion, Modelo.V_OT_LINEA_GC_GROUP_FONDO.CANTIDAD_RESTANTE)

                    CargarEncabezado(vlc_Condicion, String.Format("{0} ASC", Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_MATERIAL))

                    CargarDetalle(vlc_Condicion, String.Format("{0} DESC, {1} ASC, {2} ASC", Modelo.V_OTT_LINEA_GESTION_COMPRALST.ES_EMERGENCIA, Modelo.V_OTT_LINEA_GESTION_COMPRALST.FECHA_SOLICITUD, Modelo.V_OTT_LINEA_GESTION_COMPRALST.FECHA_ASIGNACION))




                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ninguna ubicación autorizada, contactar con el administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If

            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    Protected Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click
        If Page.IsValid Then
            Dim vlo_Row As DataRow
            Dim vlo_RowEnca As DataRow

            Try
                Me.DsEncaExc = Me.DsEnca.Clone()

                Me.DsDetaExc = Me.DsDetalle.Clone()

                For Each vlo_Fila As Data.DataRow In Me.DsDetalle.Tables(0).Rows
                    If Not TypeOf vlo_Fila.Item("SELECCIONADO") Is DBNull AndAlso vlo_Fila.Item("SELECCIONADO") <> String.Empty Then
                        If vlo_Fila.Item("SELECCIONADO") = "1" Then
                            Me.DsDetaExc.Tables(0).ImportRow(vlo_Fila)


                            vlo_Row = Me.DsEncaExc.Tables(0).Rows.Find(New Object() {vlo_Fila.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL_TABLA)})



                            vlo_RowEnca = Me.DsEncaExc.Tables(0).NewRow()

                            If vlo_Row IsNot Nothing Then
                                vlo_Row.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.CANTIDAD_RESTANTE) = vlo_Row.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.CANTIDAD_RESTANTE) + vlo_Fila.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_RESTANTE)
                            Else
                                vlo_RowEnca.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_MATERIAL) = vlo_Fila.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL_TABLA)
                                vlo_RowEnca.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.DESCRIPCION) = vlo_Fila.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.DESCRIPCION)
                                vlo_RowEnca.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.CANTIDAD_RESTANTE) = vlo_Fila.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_RESTANTE)
                                vlo_RowEnca.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_UBICACION) = vlo_Fila.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_UBICACION)
                                vlo_RowEnca.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_VIA_COMPRA_CONTRATO) = vlo_Fila.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO)
                                vlo_RowEnca.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.ANNO) = vlo_Fila.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ANNO)
                                vlo_RowEnca.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.NUMERO_GESTION) = vlo_Fila.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_GESTION)
                                vlo_RowEnca.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.CANTIDAD_SOLICITADA) = vlo_Fila.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_SOLICITADA)
                                vlo_RowEnca.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.CANTIDAD_SOLICITADA_MEDIDA) = vlo_Fila.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_SOLICITADA)
                                vlo_RowEnca.Item(Utilerias.OrdenesDeTrabajo.Modelo.V_OT_LINEA_GC_GROUP_FONDO.PROVEEDORES) = String.Empty


                                Me.DsEncaExc.Tables(0).Rows.Add(vlo_RowEnca)
                            End If
                        End If
                    End If
                Next

                If Me.DsEncaExc IsNot Nothing AndAlso Me.DsEncaExc.Tables(0).Rows.Count > 0 Then
                    With Me.rpEncaExc
                        .DataSource = Me.DsEncaExc
                        .DataMember = Me.DsEncaExc.Tables(0).TableName
                        .DataBind()
                        .Visible = True
                    End With
                Else
                    With Me.rpEncaExc
                        .DataSource = Nothing
                        .DataBind()
                        .Visible = False
                    End With
                End If

                If Me.DsDetaExc IsNot Nothing AndAlso Me.DsDetaExc.Tables(0).Rows.Count > 0 Then
                    With Me.rpDetaExc
                        .DataSource = Me.DsDetaExc
                        .DataMember = Me.DsDetaExc.Tables(0).TableName
                        .DataBind()
                        .Visible = False
                    End With
                Else
                    With Me.rpDetaExc
                        .DataSource = Nothing
                        .DataBind()
                        .Visible = False
                    End With
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
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try
                If Me.DsDetaExc IsNot Nothing AndAlso Me.DsDetaExc.Tables(0).Rows.Count > 0 Then
                    If ExcluirMateriales() Then
                        WebUtils.RegistrarScript(Me, "alertaExitosa", "mostrarAlertaExitosa();")
                    End If
                Else
                    MostrarAlertaError("Debe seleccionar los materiales que desea excluir")
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

        Response.Redirect("Lst_OT_ExclusionMaterialesGestion.aspx", False)
    End Sub

    Protected Sub ibConsultarGestion_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_IdMaterial As String
        Dim vlc_Condicion As String

        vlc_IdMaterial = CType(CType(sender, ImageButton).CommandArgument, String)

        vlc_Condicion = String.Format("{0} = {1}  AND {2} = {3}  AND {4} = {5}  AND {6} = {7} AND {8} = {9}", Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ANNO, Me.Anno, Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_GESTION, Me.NumeroGestion, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL_TABLA, vlc_IdMaterial)

        Me.Condicion = vlc_Condicion

        CargarDetalleSelec(vlc_Condicion)

    End Sub

    Protected Sub ibConsultarGestionExc_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_IdMaterial As String
        Dim vlc_Condicion As String

        vlc_IdMaterial = CType(CType(sender, ImageButton).CommandArgument, String)

        vlc_Condicion = String.Format("{0} = {1}  AND {2} = {3}  AND {4} = {5}  AND {6} = {7} AND {8} = {9}", Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompra, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ANNO, Me.Anno, Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_GESTION, Me.NumeroGestion, Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL_TABLA, vlc_IdMaterial)

        Me.Condicion = vlc_Condicion

        CargarDetalleSelecExc(vlc_Condicion)

    End Sub

    Protected Sub chkDetalle_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim vlo_CheckBox As CheckBox = CType(sender, CheckBox)

            If vlo_CheckBox.Checked Then
                Me.DsDetalle.Tables(0).Rows.Find(New Object() {vlo_CheckBox.Attributes("CommandArgument").ToString()})("SELECCIONADO") = "1"
            Else
                Me.DsDetalle.Tables(0).Rows.Find(New Object() {vlo_CheckBox.Attributes("CommandArgument").ToString()})("SELECCIONADO") = "0"
            End If
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
                Me.lblFecha.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTT_GESTION_COMPRALST.FECHA_REGISTRO_SOLICITUD)
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

    Private Sub CargarEncabezado(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.DsEnca = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_GESTION_COMPRA_ListarVOtLineaGcGroupFondoPersonalizado(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

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

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.DsDetalle = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_GESTION_COMPRA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

            Me.DsDetalle.Tables(0).Columns.Add("SELECCIONADO", GetType(String))

            For Each vlo_Fila As Data.DataRow In Me.DsDetalle.Tables(0).Rows
                vlo_Fila.Item("SELECCIONADO") = "0"
            Next

            Me.DsDetalle.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsDetalle.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_LINEA_GESTION_COMPRA)}

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

    Private Sub CargarDetalleSelecExc(pvc_Condicion As String)

        Try

            Dim vlo_DataViewFDetalles As New Data.DataView(Me.DsDetaExc.Tables(0))
            vlo_DataViewFDetalles.RowFilter = pvc_Condicion

            If vlo_DataViewFDetalles IsNot Nothing AndAlso vlo_DataViewFDetalles.Count > 0 Then
                With Me.rpDetaExc
                    .DataSource = vlo_DataViewFDetalles
                    .DataMember = Me.DsDetaExc.Tables(0).TableName
                    .DataBind()
                    .Visible = True
                    WebUtils.RegistrarScript(Me, "visibilidadPanel", "mostrarAreaDeListado();")
                End With
            Else
                With Me.rpDetaExc
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

    Private Function ExcluirMateriales() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_INGRESO_MATER_ExcluirMateriales(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               Me.IdUbicacion, Me.IdViaCompra, Me.NumeroGestion, Me.Anno, Me.Usuario.UserName, Me.DsDetaExc, Me.AutorizadoUbicacion.IdUbicacionAdministra) > 0
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
