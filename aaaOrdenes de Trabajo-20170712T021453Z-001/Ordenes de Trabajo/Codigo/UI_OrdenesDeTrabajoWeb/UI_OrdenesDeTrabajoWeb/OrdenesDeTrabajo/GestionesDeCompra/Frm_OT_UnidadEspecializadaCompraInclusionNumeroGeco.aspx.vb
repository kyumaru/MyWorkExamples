Imports Utilerias.OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_UnidadEspecializadaCompraInclusionNumeroGeco
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' tamaño en megas del archivo a cargar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property TamanoArchivo As Integer
        Get
            Return CType(ViewState("TamanoArchivo"), Integer)
        End Get
        Set(value As Integer)
            ViewState("TamanoArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property PoseeArchivo As Boolean
        Get
            Return CType(ViewState("PoseeArchivo"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("PoseeArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' Extensiones para archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ExtensionesArchivo As String
        Get
            Return CType(ViewState("ExtensionesArchivo"), String)
        End Get
        Set(value As String)
            ViewState("ExtensionesArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property GestionCompra As Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra
        Get
            Return CType(ViewState("GestionCompra"), Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra)
        End Get
        Set(value As Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra)
            ViewState("GestionCompra") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property AdjuntoGestionCompra As Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoGestionCompr
        Get
            Return CType(ViewState("AdjuntoGestionCompra"), Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoGestionCompr)
        End Get
        Set(value As Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoGestionCompr)
            ViewState("AdjuntoGestionCompra") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en sesion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
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
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdUbicacion As Integer
        Get
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdViaCompraContrato As Integer
        Get
            Return CType(ViewState("IdViaCompraContrato"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdViaCompraContrato") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Annio As Integer
        Get
            Return CType(ViewState("Annio"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Annio") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property NumeroGestion As Integer
        Get
            Return CType(ViewState("NumeroGestion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("NumeroGestion") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property MaximoLinea As Integer
        Get
            Return CType(ViewState("MaximoLinea"), Integer)
        End Get
        Set(value As Integer)
            ViewState("MaximoLinea") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsDatos As Data.DataSet
        Get
            Return CType(ViewState("DsDatos"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsDatos") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' elimina el archivo del registro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnEliminarArchivo_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminarArchivo.Click
        Try
            Me.AdjuntoGestionCompra.Archivo = Nothing
            Me.AdjuntoGestionCompra.NombreArchivo = ""
            Me.ifArchivo.Visible = True
            Me.lnkArchivo.Visible = False
            Me.btnEliminarArchivo.Visible = False
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' descargar archivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivo_Click(sender As Object, e As EventArgs) Handles lnkArchivo.Click
        DescargaArchivo(Me.AdjuntoGestionCompra.Archivo, Me.AdjuntoGestionCompra.NombreArchivo)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibSubir_Click(sender As Object, e As ImageClickEventArgs)
        Try
            If Subir(CType(sender, ImageButton).CommandArgument) Then
                CargarLista(ObtenerCondicionBusqueda(), String.Empty)
            Else
                MostrarAlertaError("No ha sido posible actualizar la información del registro")
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
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBajar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            If Bajar(CType(sender, ImageButton).CommandArgument) Then
                CargarLista(ObtenerCondicionBusqueda(), String.Empty)
            Else
                MostrarAlertaError("No ha sido posible actualizar la información del registro")
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
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpMateriales_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpMateriales.ItemDataBound
        Dim vlo_HtmlAnchor As HtmlAnchor
        Dim vlo_HtmlGenericControl As HtmlGenericControl
        Dim vlo_WebUserControl As Controles_wuc_OT_Lineas_Material_Gestion_Compra

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_HtmlAnchor = CType(e.Item.FindControl("ancorAcordeon"), HtmlAnchor)
            vlo_HtmlGenericControl = e.Item.FindControl("cuerpoAcordeon1")
            vlo_HtmlAnchor.HRef = "#" + vlo_HtmlGenericControl.ClientID

            vlo_WebUserControl = CType(e.Item.FindControl("wucLineasMaterialGestionCompra"), Controles_wuc_OT_Lineas_Material_Gestion_Compra)
            vlo_WebUserControl.Inicializar()

        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al cargar la página, inicializa los componentes necesarios
    ''' para el funcionamiento de la página
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim vlo_Wuc_OT_Lineas_Material_Gestion_Compra As Controles_wuc_OT_Lineas_Material_Gestion_Compra

        For Each vlo_Item In Me.rpMateriales.Items
            vlo_Wuc_OT_Lineas_Material_Gestion_Compra = CType(vlo_Item.FindControl("wucLineasMaterialGestionCompra"), Controles_wuc_OT_Lineas_Material_Gestion_Compra)
            AddHandler vlo_Wuc_OT_Lineas_Material_Gestion_Compra.Recargar, AddressOf RecargarFormulario
        Next

        If Not Page.IsPostBack Then
            Try

                Me.Usuario = New UsuarioActual
                LeerParametrosSession()
                CargarGestionCompra()
                CargarLista(ObtenerCondicionBusqueda(), String.Empty)
                CargarDatosDeTipoArchivo()
                CargarArchivoSolicitudGeco()
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
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            WebUtils.RegistrarScript(Me.Page, "irAListado", "irAListado();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If GuardarGestionCompra() Then
            WebUtils.RegistrarScript(Me.Page, "irAListado", "irAListado();")
        Else
            MostrarAlertaError("No ha sido posible actualizar la información del registro")
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnTramitar_Click(sender As Object, e As EventArgs) Handles btnTramitar.Click

        If (Me.AdjuntoGestionCompra.Archivo IsNot Nothing) Or (Me.ifArchivo.Visible And (Me.ifArchivo.FileName = String.Empty)) Then
            If TramitarGestionCompra() Then
                WebUtils.RegistrarScript(Me.Page, "irAListado", "irAListado();")
            Else
                MostrarAlertaError("No ha sido posible actualizar la información del registro")
            End If
        Else
            MostrarAlertaError("Debe de agregar el archivo.")
        End If

    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Método encargado de descargar archivos
    ''' </summary>
    ''' <param name="pvo_Archivo">bytes del archivo a descargar</param>
    ''' <param name="pvc_NombreArchivo">nombre del archivo a descargar</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
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
    ''' Se recarga el formulario despues de haber eliminado un material del acordeon
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub RecargarFormulario()

        Try
            CargarLista(ObtenerCondicionBusqueda(), String.Empty)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' lee y carga los parametros guardados en sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametrosSession()
        Try

            Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
            Me.IdViaCompraContrato = WebUtils.LeerParametro(Of Integer)("pvn_IdViaCompraContrato")
            Me.Annio = WebUtils.LeerParametro(Of Integer)("pvn_Annio")
            Me.NumeroGestion = WebUtils.LeerParametro(Of Integer)("pvn_NumeroGestion")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvc_Condicion">parámetro con la condicion de búsqueda</param>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If String.IsNullOrWhiteSpace(pvc_Orden) Then
                pvc_Orden = String.Format("{0} {1}", Modelo.V_OTT_GRUPO_GESTION_COMPRALST.NUMERO_LINEA, Ordenamiento.ASCENDENTE)
            End If

            Me.DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GRUPO_GESTION_COMPRA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

            If Me.DsDatos IsNot Nothing AndAlso Me.DsDatos.Tables(0).Rows.Count > 0 Then

                Me.MaximoLinea = Me.DsDatos.Tables(0).Rows.Count

                With Me.rpMateriales
                    .DataSource = Me.DsDatos
                    .DataMember = Me.DsDatos.Tables(0).TableName
                    .DataBind()
                End With
            Else
                Me.MaximoLinea = 0
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
    ''' <creationDate>24/10/2016</creationDate>
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
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}",
                              Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_UBICACION, Me.IdUbicacion,
                              Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato,
                              Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ANNO, Me.Annio,
                              Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_GESTION, Me.NumeroGestion))

            If Me.GestionCompra.Existe Then
                Me.txtObservaciones.Text = Me.GestionCompra.Observaciones
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('El identificador provisto no pertenece a ningun registro del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
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
    ''' Carga el los datos del tipo de archivo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarDatosDeTipoArchivo()
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As Wsr_OT_Catalogos.EntOtmTipoDocumento
        Dim vlo_builder = New StringBuilder()

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.SOLICITUD_GECO))

            Me.TamanoArchivo = vlo_EntOtmTipoDocumento.TamanioMaximo
            Me.ExtensionesArchivo = vlo_EntOtmTipoDocumento.FormatosAdmitidos

            vlo_builder.AppendLine(String.Format("Extensiones permitidas:{0}", ExtensionesArchivo.ToLower))
            Me.imgExtensiones.Attributes.Add("title", vlo_builder.ToString)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarArchivoSolicitudGeco()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.AdjuntoGestionCompra = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_GESTION_COMPR_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}",
                              Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_GESTION_COMPR.ID_UBICACION, Me.IdUbicacion,
                              Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_GESTION_COMPR.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato,
                              Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_GESTION_COMPR.ANNO, Me.Annio,
                              Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_GESTION_COMPR.NUMERO_GESTION, Me.NumeroGestion,
                              Utilerias.OrdenesDeTrabajo.Modelo.OTT_ADJUNTO_GESTION_COMPR.ID_TIPO_DOCUMENTO, TipoDocumento.SOLICITUD_GECO))

            If Me.AdjuntoGestionCompra.Existe Then
                Me.lnkArchivo.Text = Me.AdjuntoGestionCompra.NombreArchivo
                Me.ifArchivo.Visible = False
                Me.lnkArchivo.Visible = True
                Me.btnEliminarArchivo.Visible = True
            Else
                Me.ifArchivo.Visible = True
                Me.lnkArchivo.Visible = False
                Me.btnEliminarArchivo.Visible = False
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
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function GuardarGestionCompra() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If Me.ifArchivo.Visible Then
                If Me.ifArchivo.FileName <> String.Empty Then
                    Me.AdjuntoGestionCompra = New Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoGestionCompr
                    Me.AdjuntoGestionCompra.NombreArchivo = Me.ifArchivo.FileName
                    Me.AdjuntoGestionCompra.Archivo = Me.ifArchivo.FileBytes
                    Me.AdjuntoGestionCompra.Usuario = Me.Usuario.UserName
                    Me.PoseeArchivo = True
                Else
                    Me.PoseeArchivo = False
                End If
            Else
                Me.PoseeArchivo = False
            End If

            Me.GestionCompra.NumeroGestionGeco = Me.txtNumeroGECO.Text
            Me.GestionCompra.Usuario = Me.Usuario.UserName

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_GuardarGestionCompraUnidadEspecializadaGECO(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.GestionCompra, Me.AdjuntoGestionCompra, Me.PoseeArchivo) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function TramitarGestionCompra() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If Me.ifArchivo.Visible Then
                If (Me.ifArchivo.FileName <> String.Empty) Then
                    Me.AdjuntoGestionCompra = New Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoGestionCompr
                    Me.AdjuntoGestionCompra.NombreArchivo = Me.ifArchivo.FileName
                    Me.AdjuntoGestionCompra.Archivo = Me.ifArchivo.FileBytes
                    Me.AdjuntoGestionCompra.Usuario = Me.Usuario.UserName
                End If
            End If

            Me.GestionCompra.NumeroGestionGeco = Me.txtNumeroGECO.Text
            Me.GestionCompra.Estado = EstadoGestionCompra.GESTION_UNIDAD_ESPECIALIZADA_COTIZACION
            Me.GestionCompra.Usuario = Me.Usuario.UserName

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_TramitarGestionCompraUnidadEspecializadaGECO(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.GestionCompra, Me.AdjuntoGestionCompra) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' arma la condicion de busqueda
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String
        vlc_Condicion = String.Empty

        vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ID_UBICACION, Me.IdUbicacion)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTT_GRUPO_GESTION_COMPRALST.ANNO, Me.Annio)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTT_GRUPO_GESTION_COMPRALST.NUMERO_GESTION, Me.NumeroGestion)

        Return vlc_Condicion
    End Function

    ''' <summary>
    ''' se comunica con el servicio web, para modificar el regsitro
    ''' </summary>
    ''' <param name="pvn_NumeroLinea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Subir(pvn_NumeroLinea As Integer) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_GRUPO_GESTION_COMPRA_SubirNumeroLinea(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.GestionCompra, pvn_NumeroLinea, Me.Usuario.UserName) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' se comunica con el servicio web, para modificar el regsitro
    ''' </summary>
    ''' <param name="pvn_NumeroLinea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Bajar(pvn_NumeroLinea As Integer) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_GRUPO_GESTION_COMPRA_BajarNumeroLinea(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.GestionCompra, pvn_NumeroLinea, Me.Usuario.UserName) > 0

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
