Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports System.Data

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
''' <changeLog>Carlos Gómez Ondoy - Cambio total de la funcionalidad de la pantalla - 04/10/2016</changeLog>
Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_AprobacionAjusteMateriales
    Inherits System.Web.UI.Page

#Region "Propiedades"

    Private Property SolicitudMaterial As EntOttSolicitudMaterial
        Get
            Return CType(ViewState("SolicitudMaterial"), EntOttSolicitudMaterial)
        End Get
        Set(value As EntOttSolicitudMaterial)
            ViewState("SolicitudMaterial") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
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
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property OrdenTrabajo As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la Categoria del material
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property Categoria As Wsr_OT_Catalogos.EntOtmCategoriaMaterial
        Get
            Return CType(ViewState("Categoria"), Wsr_OT_Catalogos.EntOtmCategoriaMaterial)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmCategoriaMaterial)
            ViewState("Categoria") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la Categoria del material
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property SubCategoria As Wsr_OT_Catalogos.EntOtmSubcategoriaMaterial
        Get
            Return CType(ViewState("SubCategoria"), Wsr_OT_Catalogos.EntOtmSubcategoriaMaterial)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmSubcategoriaMaterial)
            ViewState("SubCategoria") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la unidad de medida del material
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property UnidadMedida As Wsr_OT_Catalogos.EntOtmUnidadMedida
        Get
            Return CType(ViewState("UnidadMedida"), Wsr_OT_Catalogos.EntOtmUnidadMedida)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmUnidadMedida)
            ViewState("UnidadMedida") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la unidad de medida del material
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property AlmacenPrincipal As Wsr_OT_Catalogos.EntOtmAlmacenBodega
        Get
            Return CType(ViewState("AlmacenPrincipal"), Wsr_OT_Catalogos.EntOtmAlmacenBodega)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAlmacenBodega)
            ViewState("AlmacenPrincipal") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    Private Property IdSectorTaller As Integer
        Get
            If ViewState("IdSectorTaller") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdSectorTaller"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdSectorTaller") = value
        End Set
    End Property

    ''' <summary>
    ''' Es el identificador base de la Orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    Public Property IdOrdenTrabajo As String
        Get
            If ViewState("IdOrdenTrabajo") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdOrdenTrabajo"), String)
        End Get
        Set(value As String)
            ViewState("IdOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Guarda la ubicación de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    Public Property IdUbicacion As Integer
        Get
            If ViewState("IdUbicacion") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Guarda la ubicación de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    Public Property Anno As Integer
        Get
            If ViewState("Anno") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("Anno"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Anno") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de materiales a ingresar eliminar o modificar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsMaterialesAprobados As Data.DataSet
        Get
            Return CType(ViewState("DsMaterialesAprobados"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsMaterialesAprobados") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de materiales a ingresar eliminar o modificar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsMaterialesInsert As Data.DataSet
        Get
            Return CType(ViewState("DsMaterialesInsert"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsMaterialesInsert") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de vias de compra y de almacenes y bodegas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsAlmacenViaCompra As Data.DataSet
        Get
            Return CType(ViewState("DsAlmacenViaCompra"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsAlmacenViaCompra") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    Private Property MontoTotal As Double
        Get
            If ViewState("MontoTotal") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("MontoTotal"), Double)
        End Get
        Set(value As Double)
            ViewState("MontoTotal") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el costo total de la orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    Private Property CostoTotalOT As Integer
        Get
            If ViewState("CostoTotalOT") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("CostoTotalOT"), Integer)
        End Get
        Set(value As Integer)
            ViewState("CostoTotalOT") = value
        End Set
    End Property

    ''' <summary>
    ''' Diccionario de datos para mantener los seleccionados de la lista de almacenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    Private Property AlmacenesVias As System.Collections.Generic.Dictionary(Of String, Integer)
        Get
            Return CType(ViewState("AlmacenesVias"), System.Collections.Generic.Dictionary(Of String, Integer))
        End Get
        Set(value As System.Collections.Generic.Dictionary(Of String, Integer))
            ViewState("AlmacenesVias") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta al cargar la pagina e inicializa componentes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                InicializarFormulario()
            Catch ex As Exception
                If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso
                 CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                    Dim vlo_TallerCapacitacionException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                    MostrarAlertaError(vlo_TallerCapacitacionException.Message)
                    WebUtils.RegistrarScript(Me, "OcultarAreaDeFiltros", "ocultarAreaFiltrosDeBusqueda();")
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
    ''' <author>César Bermudez G</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkEjecutarBusquedaMaterial_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusquedaMaterial.Click
        Try
            Me.ctrl_Materiales.mostrarAlmacenPartida = True
            WebUtils.RegistrarScript(Me.Page, "mostrarPopUpBusquedaFuncionario", "javascript:mostrarPopUp('#PopUpBusquedaMateriales');cargarLupa();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvc_IdMaterial"></param>
    ''' <param name="pvc_Descripcion"></param>
    ''' <param name="pvn_IdCategoria"></param>
    ''' <param name="pvn_idSubcategoria"></param>
    ''' <param name="pvn_CostoPromedio"></param>
    ''' <param name="pvn_UnidadMedida"></param>
    ''' <remarks></remarks
    ''' <author>César Bermudez G</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ctrl_Materiales_Aceptar(pvc_IdMaterial As Integer, pvc_Descripcion As String, pvn_IdCategoria As Integer, pvn_idSubcategoria As Integer, pvn_CostoPromedio As Integer, pvn_UnidadMedida As Integer) Handles ctrl_Materiales.Aceptar
        CargarCategoria(String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_MATERIAL.ID_CATEGORIA_MATERIAL, pvn_IdCategoria))
        CargarSubCategoria(String.Format("{0} = {1}", Modelo.OTM_SUBCATEGORIA_MATERIAL.ID_SUBCATEGORIA_MATERIAL, pvn_idSubcategoria))
        CargarUnidadMedida(String.Format("{0} = {1}", Modelo.OTM_UNIDAD_MEDIDA.ID_UNIDAD_MEDIDA, pvn_UnidadMedida))
        Me.txtCodigo.Text = pvc_IdMaterial.ToString

        WucDatosMaterial.AsignaDescripcion(pvc_Descripcion)
        WucDatosMaterial.AsignaCategoria(Me.Categoria.Descripcion)
        WucDatosMaterial.AsignaMontoPromedio(pvn_CostoPromedio.ToString())
        WucDatosMaterial.AsignaUnidadMedida(Me.UnidadMedida.Descripcion)
        WucDatosMaterial.AsignaSubCategoria(Me.SubCategoria.Descripcion)

        WucDatosMaterial.Visible = True
        upControlDatosMaterial.Visible = True
        upTxtCodigo.Update()
        upControlDatosMaterial.Update()

        WebUtils.RegistrarScript(Me.Page, "InicializarFormulario", "javascript:inicializarFormulario();ocultarFiltroMateriales();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub txtCodigo_TextChanged(sender As Object, e As EventArgs) Handles txtCodigo.TextChanged
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmMaterial As Wsr_OT_Catalogos.EntOtmMaterial
        Dim pvc_CondicionBusquedas As String

        'instanciar y configurar objetos
        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            WucDatosMaterial.AsignaDescripcion("")
            WucDatosMaterial.AsignaCategoria("")
            WucDatosMaterial.AsignaMontoPromedio("")
            WucDatosMaterial.AsignaUnidadMedida("")
            WucDatosMaterial.AsignaSubCategoria("")

            If Me.txtCodigo.Text <> "" Then
                pvc_CondicionBusquedas = String.Format("{0} = '{1}'", Modelo.OTM_MATERIAL.ID_MATERIAL, Me.txtCodigo.Text)
                vlo_EntOtmMaterial = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ObtenerRegistro(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                                                                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                                                                   pvc_CondicionBusquedas)
                If vlo_EntOtmMaterial IsNot Nothing AndAlso vlo_EntOtmMaterial.Existe Then

                    WucDatosMaterial.Visible = True


                    CargarCategoria(String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_MATERIAL.ID_CATEGORIA_MATERIAL, vlo_EntOtmMaterial.IdCategoriaMaterial))
                    CargarSubCategoria(String.Format("{0} = {1}", Modelo.OTM_SUBCATEGORIA_MATERIAL.ID_SUBCATEGORIA_MATERIAL, vlo_EntOtmMaterial.IdSubcategoriaMaterial))
                    CargarUnidadMedida(String.Format("{0} = {1}", Modelo.OTM_UNIDAD_MEDIDA.ID_UNIDAD_MEDIDA, vlo_EntOtmMaterial.IdUnidadMedida))
                    Me.txtCodigo.Text = vlo_EntOtmMaterial.IdMaterial.ToString

                    WucDatosMaterial.AsignaDescripcion(vlo_EntOtmMaterial.Descripcion)
                    WucDatosMaterial.AsignaCategoria(Me.Categoria.Descripcion)
                    WucDatosMaterial.AsignaMontoPromedio(vlo_EntOtmMaterial.CostoPromedio.ToString())
                    WucDatosMaterial.AsignaUnidadMedida(Me.UnidadMedida.Descripcion)
                    WucDatosMaterial.AsignaSubCategoria(Me.SubCategoria.Descripcion)
                    upControlDatosMaterial.Visible = True


                Else
                    WucDatosMaterial.Visible = False
                    WucDatosMaterial.AsignaDescripcion("")
                    WucDatosMaterial.AsignaCategoria("")
                    WucDatosMaterial.AsignaMontoPromedio("")
                    WucDatosMaterial.AsignaUnidadMedida("")
                    WucDatosMaterial.AsignaSubCategoria("")
                    upControlDatosMaterial.Visible = False

                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoEncontrado();")
                End If
            Else
                upControlDatosMaterial.Visible = False


            End If
            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")
            WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();")
            ' upPanelMateriales.Update()

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Evento que por cada fila adjunta un identificador único para borrar, modificar y para la lista del almacen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpIngresados_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpIngresados.ItemDataBound
        Dim vlo_IdBorrar As ImageButton
        Dim vlo_Almacen As DropDownList
        Dim vlo_Modificar As ImageButton
        Dim vlo_Denegar As CheckBox
        Dim vlc_estado As String
        'Dim vlo_HiddenField As HiddenField
        'Dim vlo_HiddenField2 As HiddenField
        Dim vln_IdAlmacenBodega As Integer
        Dim vln_IdViaCompraContrato As Integer
        Dim vlc_IdViaCompra As String

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then

            vlo_Denegar = e.Item.FindControl("ibDenegar")
            If vlo_Denegar IsNot Nothing Then
                vlo_Denegar.Attributes.Add("data-uniqueid", vlo_Denegar.UniqueID)
                vlc_estado = vlo_Denegar.Attributes("data-Estado")
                If vlc_estado = EstadoRegistro.DENEGADA Then
                    vlo_Denegar.Checked = True
                End If
            End If

            vlo_IdBorrar = e.Item.FindControl("ibBorrar")
            If vlo_IdBorrar IsNot Nothing Then
                vlo_IdBorrar.Attributes.Add("data-uniqueid", vlo_IdBorrar.UniqueID)
            End If

            vlo_Modificar = e.Item.FindControl("ibModificar")
            If vlo_Modificar IsNot Nothing Then
                vlo_Modificar.Attributes.Add("data-uniqueid", vlo_Modificar.UniqueID)
            End If

            vlo_Almacen = e.Item.FindControl("ibAlmacen")

            If vlo_Almacen IsNot Nothing Then
                vlo_Almacen.Attributes.Add("data-uniqueid", vlo_Almacen.UniqueID)
                With vlo_Almacen
                    .DataSource = DsAlmacenViaCompra
                    .DataMember = DsAlmacenViaCompra.Tables(0).TableName
                    .DataTextField = "DESCRIPCION"
                    .DataValueField = "ID_AMBITO"
                    .DataBind()
                End With

                'vlo_HiddenField = CType(e.Item.FindControl("hdfAlmacen"), HiddenField)
                'vlo_HiddenField2 = CType(e.Item.FindControl("hdfDetalleMaterial"), HiddenField)

                'If vlo_HiddenField.Value.First <> "0" Then
                '    vlo_Almacen.SelectedValue = vlo_HiddenField.Value
                'End If

                'If vlo_HiddenField2.Value.First <> "0" Then
                '    vlo_Almacen.SelectedValue = vlo_HiddenField2.Value
                'End If

                vln_IdAlmacenBodega = vlo_Almacen.Attributes("data-IdAlmacenBodegaCombo")
                vln_IdViaCompraContrato = vlo_Almacen.Attributes("data-IdViaCompraContratoCombo")
                vlc_IdViaCompra = vlo_Almacen.Attributes("data-IdViaCompraCombo")

                If vlc_IdViaCompra <> ViaDespacho.VIACOMPRA Then
                    vlo_Almacen.SelectedValue = String.Format("{0}_{1}", vln_IdAlmacenBodega, vlc_IdViaCompra)
                Else
                    vlo_Almacen.SelectedValue = String.Format("{0}_{1}", vln_IdViaCompraContrato, vlc_IdViaCompra)
                End If

                If Me.AlmacenesVias.ContainsKey(vlo_Almacen.UniqueID) Then
                    AlmacenesVias.TryGetValue(vlo_Almacen.UniqueID, vlo_Almacen.SelectedIndex)
                End If

                vlo_Modificar.Attributes.Add("data-idAlmacen", vlo_Almacen.UniqueID)

                If vlo_Denegar.Checked Then
                    vlo_Almacen.Enabled = False
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Habilita o inhabilita determinado control dentro del repeater
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>27/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub chkDenegar_CheckedChanged(sender As Object, e As EventArgs)
        Dim vlo_check As CheckBox
        Dim vlo_fila As Integer
        Dim vlo_txtCantidad As DropDownList

        Try
            vlo_check = CType(sender, CheckBox)
            vlo_fila = vlo_check.Attributes("data-fila")

            For Each vlo_filarepeater In rpIngresados.Items(vlo_fila).Controls
                vlo_txtCantidad = CType(vlo_filarepeater.FindControl("ibAlmacen"), DropDownList)
                If vlo_txtCantidad IsNot Nothing Then
                    If vlo_check.Checked Then
                        vlo_txtCantidad.Enabled = False
                        modificarLineaMaterial(vlo_check.Attributes("data-idDetalleMaterial"), EstadoRegistro.DENEGADA)
                    Else
                        vlo_txtCantidad.Enabled = True
                        modificarLineaMaterial(vlo_check.Attributes("data-idDetalleMaterial"), EstadoRegistro.PENDIENTE_APROBACION)
                    End If
                End If
            Next
            CargarListaNuevos()

            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")
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
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        Me.txtCodigo.Text = String.Empty
        WucDatosMaterial.AsignaCantidad(String.Empty)
        WucDatosMaterial.AsignaDetalle(String.Empty)

        txtCodigo_TextChanged(sender, e)
        upTxtCodigo.Update()
        upControlDatosMaterial.Update()
        Me.btnAgregar.Visible = True
        Me.btnCancelar.Visible = False
        Me.btnModificar.Visible = False
        Me.txtCodigo.ReadOnly = False
        WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();HabilitarCodigo();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs)
        Try
            If Aceptar() Then
                mostrarAlertaGuardadoExitoso()
            Else
                MostrarAlertaError("No ha sido posible la actualización de datos.")
            End If
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
    ''' <author>César Bermudez G</author>
    ''' <creationDate>9/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibAlmacen_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim vlo_IbAlmacen As DropDownList
        Dim vln_idMaterial As Integer
        Dim vln_idDetalleMaterial As Integer
        Dim vln_IdUbicacion As Integer
        Dim vlo_result() As Data.DataRow
        Dim vlo_EntOtfInventario As Wsr_OT_Catalogos.EntOtfInventario
        Dim vlo_EntOtmMaterial As Wsr_OT_Catalogos.EntOtmMaterial
        Dim vlo_EntOtmAlmacenBodega As Wsr_OT_Catalogos.EntOtmAlmacenBodega
        Dim vln_monto As Integer
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlc_idAlmacenViaCompra() As String

        Try

            'instanciar y configurar objetos
            vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
            vlo_Ws_OT_Catalogos.Timeout = -1
            vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

            'Se obtiene el id del material
            vlo_IbAlmacen = CType(sender, DropDownList)
            vln_idMaterial = vlo_IbAlmacen.Attributes("data-idMaterial")
            vln_IdUbicacion = vlo_IbAlmacen.Attributes("data-idUbicacion")
            vln_idDetalleMaterial = vlo_IbAlmacen.Attributes("data-idDetalleMaterial")

            vlo_EntOtmMaterial = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ObtenerRegistro(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1} AND {2} = {3}",
                             Modelo.OTM_MATERIAL.ID_MATERIAL, vln_idMaterial,
                             Modelo.OTM_MATERIAL.ID_UBICACION_ADMINISTRA, vln_IdUbicacion))

            'Se busca en el dataset por el id del material obtenido
            vlo_result = Me.DsMaterialesInsert.Tables(0).Select(
                String.Format("{0} = {1} AND {2} = {3}",
                              Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL, vln_idMaterial,
                              Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL, vln_idDetalleMaterial))

            'Se obtiene el valor seleccionado del dropdownlist
            vlc_idAlmacenViaCompra = vlo_IbAlmacen.SelectedValue.Split("_")

            If AlmacenesVias.ContainsKey(vlo_IbAlmacen.UniqueID) Then
                AlmacenesVias.Item(vlo_IbAlmacen.UniqueID) = vlo_IbAlmacen.SelectedIndex
            Else
                AlmacenesVias.Add(vlo_IbAlmacen.UniqueID, vlo_IbAlmacen.SelectedIndex)
            End If

            ''Se obtiene el almacen o via de compra
            'vlo_EntOtmAlmacenBodega = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ObtenerRegistro(
            '   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            '   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            '   String.Format("{0} = {1}", Modelo.OTM_ALMACEN_BODEGA.ID_ALMACEN_BODEGA, vlc_idAlmacenViaCompra(0)))

            If vlc_idAlmacenViaCompra(1).Equals(ViaDespacho.VIACOMPRA) Then
                vlo_EntOtmAlmacenBodega = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ObtenerRegistro(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            String.Format("{0} = {1} AND {2} = 0",
                          Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, Me.IdUbicacion,
                          Modelo.OTM_ALMACEN_BODEGA.ID_ALMACEN_BODEGA))
            Else
                vlo_EntOtmAlmacenBodega = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ObtenerRegistro(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            String.Format("{0} = {1} AND {2} = {3}",
                          Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, Me.IdUbicacion,
                          Modelo.OTM_ALMACEN_BODEGA.ID_ALMACEN_BODEGA, vlc_idAlmacenViaCompra(0)))
            End If

            If vlo_EntOtmAlmacenBodega.Existe Then
                'Se obtiene la información del inventario
                vlo_EntOtfInventario = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                                  Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, Me.IdUbicacion,
                                  Modelo.OTF_INVENTARIO.ID_MATERIAL, vln_idMaterial,
                                  Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOtmAlmacenBodega.IdAlmacenBodega))

                'Si el almacen NO es de tipo via de compra
                If vlo_EntOtfInventario.Existe Then

                    If Not ModificarDetalleMaterialConInventario(vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL),
                                           vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL),
                                           vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE),
                                           CType(vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA), Double),
                                           CType(vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_RESERVADA), Double),
                                           vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_ALMACEN_BODEGA),
                                           vlc_idAlmacenViaCompra, vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.TIME_STAMP)) Then

                        mostrarAlertSinCantidadDisponible()
                    Else
                        vln_monto = vlo_EntOtmMaterial.CostoPromedio * CInt(vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA))

                        'vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DISPONIBLE_ALMACEN_SOLICITUD) = vlo_EntOtfInventario.CantidadDisponible.ToString
                        'vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DISP_ALMACEN_SOLICITUD_MEDIDA) = vlo_EntOtfInventario.CantidadDisponible.ToString
                        'vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO) = vln_monto.ToString
                    End If

                    ' CargarListaNuevos()
                Else
                    'CargarListaNuevos()

                    mostrarAlertSinCantidadDisponible()

                End If

            Else

                vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DISPONIBLE_ALMACEN_SOLICITUD) = 0
                vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_DISPONIBLE_INVENTARIO) = 0
                vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO) = 0

                ModificarDetalleMaterialConInventario(vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL),
                                         vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL),
                                         vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE),
                                          CType(vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA), Double),
                                           CType(vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_RESERVADA), Double),
                                         vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_ALMACEN_BODEGA),
                                         vlc_idAlmacenViaCompra, vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.TIME_STAMP))
                ' CargarListaNuevos()

            End If

            CargarListaNuevos()
            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Agrega el detalle material a la base de datos y actualiza la tabla vista por el usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtfInventario As Wsr_OT_Catalogos.EntOtfInventario
        Dim vlo_almacen As Wsr_OT_Catalogos.EntOtmAlmacenBodega
        Dim vln_monto As Double

        Try
            MontoTotal = 0
            'instanciar y configurar objetos
            vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
            vlo_Ws_OT_Catalogos.Timeout = -1
            vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

            vlo_almacen = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, Me.IdUbicacion, Modelo.OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))

            If vlo_almacen.Existe Then

                vlo_EntOtfInventario = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_almacen.IdAlmacenBodega,
                                  Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, Me.IdUbicacion,
                                  Modelo.OTF_INVENTARIO.ID_MATERIAL, Me.txtCodigo.Text))

                vln_monto = WucDatosMaterial.RetornaMontoPromedio

                'If vlo_EntOtfInventario.CantidadDisponible >= CInt(WucDatosMaterial.RetornaCantidad) Then
                AgregarDetalleMaterial()

                MontoTotal = MontoTotal + (vln_monto * CInt(WucDatosMaterial.RetornaCantidad))

                If MontoTotal < CostoTotalOT Then
                    lblMontoTotalPendientes.Attributes.Add("style", "color:black;")
                Else
                    lblMontoTotalPendientes.Attributes.Add("style", "color:red;")
                End If

                Me.lblMontoTotalPendientes.Text = String.Format("Total: ₡{0:n2}", MontoTotal)

                CargarListaNuevos()

                Me.txtCodigo.Text = String.Empty
                Me.WucDatosMaterial.AsignaDescripcion(String.Empty)
                Me.WucDatosMaterial.AsignaCategoria(String.Empty)
                Me.WucDatosMaterial.AsignaMontoPromedio(String.Empty)
                Me.WucDatosMaterial.AsignaSubCategoria(String.Empty)
                Me.WucDatosMaterial.AsignaUnidadMedida(String.Empty)
                Me.WucDatosMaterial.AsignaDetalle(String.Empty)
                Me.WucDatosMaterial.AsignaCantidad(String.Empty)

                WucDatosMaterial.Visible = False
                upControlDatosMaterial.Visible = False
                upTxtCodigo.Update()
                upControlDatosMaterial.Update()
                'Else
                ' mostrarAlertSinCantidadDisponible()
                ' End If

            End If
            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Carga los datos de la fila a modificar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibModificar_Click(sender As Object, e As EventArgs)
        Dim vlo_imgModificar As ImageButton
        Dim vlo_idMaterial As String
        Dim vlo_idDetalleMaterial As String
        Dim vlo_fila() As DataRow
        Dim vlc_Llave As String()

        Try
            vlo_imgModificar = CType(sender, ImageButton)

            vlc_Llave = vlo_imgModificar.CommandArgument.Split("¬")

            vlo_idMaterial = vlc_Llave(0)
            vlo_idDetalleMaterial = vlc_Llave(1)

            vlo_fila = Me.DsMaterialesInsert.Tables(0).Select(String.Format("{0} = {1} AND {2} = {3}",
                                                                            Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL, vlo_idMaterial,
                                                                            Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL, vlo_idDetalleMaterial))
            If vlo_fila.Length > 0 Then
                Me.txtCodigo.Text = vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)

                WucDatosMaterial.AsignaCantidad(vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA))
                WucDatosMaterial.AsignaDetalle(vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE))

                txtCodigo_TextChanged(sender, e)
                upTxtCodigo.Update()
                upControlDatosMaterial.Update()
                btnAgregar.Visible = False
                btnModificar.Visible = True
                WebUtils.RegistrarScript(Me.Page, "InhabilitarCodigo", "javascript:InhabilitarCodigo();")
                btnModificar.CommandArgument = String.Format("{0}¬{1}", vlo_idMaterial, vlo_idDetalleMaterial)
                btnCancelar.Visible = True
                Me.txtCodigo.ReadOnly = True
            End If
            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Evento que almacena los cambios realizados a un elemento del listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnModificar_Click(sender As Object, e As EventArgs)
        Dim vln_idMaterial As Integer
        Dim vln_idDetalleMaterial As Integer
        Dim vlc_Llave As String()
        Dim vlo_fila() As DataRow
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_almacen As Wsr_OT_Catalogos.EntOtmAlmacenBodega

        Try

            'instanciar y configurar objetos
            vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
            vlo_Ws_OT_Catalogos.Timeout = -1
            vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

            vlo_almacen = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, Me.IdUbicacion, Modelo.OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))

            'instanciar y configurar objetos
            vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
            vlo_Ws_OT_Catalogos.Timeout = -1
            vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

            vlc_Llave = btnModificar.CommandArgument.Split("¬")

            vln_idMaterial = vlc_Llave(0)
            vln_idDetalleMaterial = vlc_Llave(1)
            vlo_fila = Me.DsMaterialesInsert.Tables(0).Select(String.Format("{0} = {1} AND {2} = {3}",
                                                                            Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL, vln_idMaterial,
                                                                            Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL, vln_idDetalleMaterial))
            If vlo_fila.Length > 0 Then

                If Not ModificarDetalleMaterial(vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL),
                                         CInt(Me.txtCodigo.Text),
                                         WucDatosMaterial.RetornaDetalle,
                                         CType(WucDatosMaterial.RetornaCantidad, Double)) Then

                    mostrarAlertSinCantidadDisponible()
                End If
            End If

            WebUtils.RegistrarScript(Me.Page, "HabilitarCodigo", "javascript:HabilitarCodigo();cargarLupa();")

            Me.txtCodigo.ReadOnly = False
            btnCancelar_Click(sender, e)
            Me.btnModificar.Visible = False
            Me.btnCancelar.Visible = False
            Me.btnAgregar.Visible = True
            Me.txtCodigo.ReadOnly = False

            CargarListaNuevos()
            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")

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
    ''' <author>César Bermudez G</author>
    ''' <creationDate>9/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As EventArgs)
        Dim vlo_IbBorrar As ImageButton
        Dim vlc_IdMaterial As String

        Try
            vlo_IbBorrar = CType(sender, ImageButton)
            vlc_IdMaterial = vlo_IbBorrar.CommandArgument

            If Borrar(vlc_IdMaterial) Then
                MostrarAlertaRegistroBorrado()
                CargarListaNuevos()
            Else
                MostrarAlertaRegistroNoBorrado()
            End If
            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")
        Catch ex As Exception
            If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso
                CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                Dim vlo_TallerCapacitacionException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                MostrarAlertaError(vlo_TallerCapacitacionException.Message)
                WebUtils.RegistrarScript(Me, "OcultarAreaDeFiltros", "ocultarAreaFiltrosDeBusqueda();")
            Else
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End If
        End Try
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>27/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaRegistroBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroBorrado", "mostrarAlertaRegistroBorrado();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>27/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaRegistroNoBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroNoBorrado", "mostrarAlertaRegistroNoBorrado();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvc_Mensaje"></param>
    ''' <remarks></remarks>
    ''' <creationDate>24/6/2016</creationDate>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <creationDate>24/6/2016</creationDate>
    Private Sub mostrarAlertaGuardadoExitoso()
        WebUtils.RegistrarScript(Me, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <creationDate>24/6/2016</creationDate>
    Private Sub mostrarAlertSinCantidadDisponible()
        WebUtils.RegistrarScript(Me, "alertaError", "mostrarAlertSinCantidadDisponible();")
    End Sub

    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vln_costoPromedio As Double

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            MontoTotal = 0
            Me.DsMaterialesAprobados = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}' AND {2} = {3} AND {4} = '{5}'", Modelo.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo,
                    Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_DETALLE_MATERIAL.ESTADO, EstadoRegistro.APROBADA),
                String.Format("{0} {1}", Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL, Ordenamiento.ASCENDENTE), False, 0, 0)

            If Me.DsMaterialesAprobados IsNot Nothing AndAlso Me.DsMaterialesAprobados.Tables(0).Rows.Count > 0 Then
                Me.rpAprobados.DataSource = DsMaterialesAprobados
                Me.rpAprobados.DataMember = Me.DsMaterialesAprobados.Tables(0).TableName
                Me.rpAprobados.DataBind()
                Me.rpAprobados.Visible = True
                For Each vlo_fila In DsMaterialesAprobados.Tables(0).Rows
                    vln_costoPromedio = vlo_fila(Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO)
                    MontoTotal = MontoTotal + vln_costoPromedio
                Next
                If MontoTotal < CostoTotalOT Then
                    lblMontoTotal.Attributes.Add("style", "color:black;")
                Else
                    lblMontoTotal.Attributes.Add("style", "color:red;")
                End If

                Me.lblMontoTotal.Text = String.Format("Total: ₡{0:n2}", MontoTotal)
            Else
                With Me.rpAprobados
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpAprobados.Visible = False
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
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaNuevos()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vln_costoPromedio As Double

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            MontoTotal = 0
            Me.DsMaterialesInsert = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}' AND {2} = {3} AND ({4} = '{5}' OR {4} = '{6}')",
                    Modelo.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo,
                    Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION, Me.IdUbicacion,
                    Modelo.OTT_DETALLE_MATERIAL.ESTADO, EstadoRegistro.PENDIENTE_APROBACION,
                    EstadoRegistro.DENEGADA),
                String.Format("{0} {1}", Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL, Ordenamiento.ASCENDENTE), False, 0, 0)

            If Me.DsMaterialesInsert IsNot Nothing AndAlso Me.DsMaterialesInsert.Tables(0).Rows.Count > 0 Then
                Me.rpIngresados.DataSource = DsMaterialesInsert
                Me.rpIngresados.DataMember = Me.DsMaterialesInsert.Tables(0).TableName
                Me.rpIngresados.DataBind()
                Me.rpIngresados.Visible = True
                'tituloListado.Visible = True
                For Each vlo_fila In DsMaterialesInsert.Tables(0).Rows
                    vln_costoPromedio = vlo_fila(Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO)
                    MontoTotal = MontoTotal + vln_costoPromedio
                Next
                If MontoTotal < CostoTotalOT Then
                    lblMontoTotalPendientes.Attributes.Add("style", "color:black;")
                Else
                    lblMontoTotalPendientes.Attributes.Add("style", "color:red;")
                End If

                Me.lblMontoTotalPendientes.Text = String.Format("Total: ₡{0:n2}", MontoTotal)
            Else
                With Me.rpIngresados
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpIngresados.Visible = False
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
    ''' Inicializa el formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Usuario = New UsuarioActual()
        AlmacenesVias = New Dictionary(Of String, Integer)
        CargarCostoTotalOrden()
        leerParametros()
        cargarAlmacenesVias()
        CargarLista()
        CargarListaNuevos()
        CargarOrdenTrabajo(Me.IdUbicacion, Me.IdOrdenTrabajo)
        CargarDatosMateriales()
        InicializarControlesUsuario()
    End Sub

    Private Sub CargarDatosMateriales()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_Historial As String()

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.SolicitudMaterial = vlo_Ws_OT_OrdenesDeTrabajo.OTT_SOLICITUD_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_SOLICITUD_MATERIAL.ID_UBICACION, Me.OrdenTrabajo.IdUbicacion, Modelo.OTT_SOLICITUD_MATERIAL.ID_ORDEN_TRABAJO, Me.OrdenTrabajo.IdOrdenTrabajo))

            If Me.SolicitudMaterial.Existe Then
                vlo_Historial = Me.SolicitudMaterial.HistorialJustificacion.Split("¬")

                For Each vlo_Dato As String In vlo_Historial
                    Me.lblJustficacionMaterial.Text = Me.lblJustficacionMaterial.Text + vlo_Dato + vbNewLine + vbNewLine
                Next
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
    ''' Se encarga de leer los parámetros provenientes del listado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub leerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        Me.IdSectorTaller = WebUtils.LeerParametro(Of Integer)("pvn_IdSectorTaller")

        Me.Session.Add("pvn_IdUbicacion", IdUbicacion)
        Me.Session.Add("pvc_IdOrdenTrabajo", IdOrdenTrabajo)
        Me.Session.Add("pvn_Anno", Anno)
        Me.Session.Add("pvn_IdSectorTaller", IdSectorTaller)

    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">id de la ubicacion</param>
    ''' <param name="pvn_IdOrdenTrabajo">id de la orden</param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenTrabajo(pvn_IdUbicacion As Integer, pvn_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION,
                              pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvn_IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario para mostrar la información general
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarControlesUsuario()

        'Asignación de Datos para generar el web user control de la información general
        ctrl_InfoGeneral.Anno = Me.Anno
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.IdUbicacion

        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()

        Me.ctrl_Materiales.mostrarAlmacenPartida = True
        Me.ctrl_Materiales.Inicializar()

    End Sub

    ''' <summary>
    ''' Carga las categorias deacuerdo a la condición indicada
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarCategoria(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.Categoria = vlo_Ws_OT_Catalogos.OTM_CATEGORIA_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga las categorias deacuerdo a la condición indicada
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarSubCategoria(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.SubCategoria = vlo_Ws_OT_Catalogos.OTM_SUBCATEGORIA_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga la unidad de medida del material específicado
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarUnidadMedida(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            UnidadMedida = vlo_Ws_OT_Catalogos.OTM_UNIDAD_MEDIDA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub cargarAlmacenesVias()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            DsAlmacenViaCompra = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ListarVOttViaCompraAlmacen(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}'", Modelo.OTM_ALMACEN_BODEGA.ESTADO, Estado.ACTIVO),
                String.Format("{0} {1}", Modelo.OTM_ALMACEN_BODEGA.DESCRIPCION, Ordenamiento.ASCENDENTE), False, 0, 0)

            AlmacenPrincipal = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'",
                Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, Me.IdUbicacion, Modelo.OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga el costo total de la orden de trabajo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarCostoTotalOrden()
        Dim vlo_EntOtpParametroUbicacion As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtpParametroUbicacion = vlo_Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.MAXIMO_EJECUCION_OBRAS))

            CostoTotalOT = vlo_EntOtpParametroUbicacion.Valor

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Agrega un detalle material a la base de datos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregarDetalleMaterial()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial

        'instanciar y configurar objetos
        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttDetalleMaterial = New EntOttDetalleMaterial
            vlo_EntOttDetalleMaterial.IdMaterial = Me.txtCodigo.Text
            vlo_EntOttDetalleMaterial.IdUbicacion = Me.IdUbicacion
            vlo_EntOttDetalleMaterial.IdUbicacionAdministra = Me.IdUbicacion
            vlo_EntOttDetalleMaterial.Detalle = WucDatosMaterial.RetornaDetalle
            vlo_EntOttDetalleMaterial.IdOrdenTrabajo = Me.IdOrdenTrabajo
            vlo_EntOttDetalleMaterial.CantidadSolicitada = CType(WucDatosMaterial.RetornaCantidad, Double)
            vlo_EntOttDetalleMaterial.IdAlmacenBodega = AlmacenPrincipal.IdAlmacenBodega
            vlo_EntOttDetalleMaterial.ViaDespacho = AlmacenPrincipal.Tipo
            vlo_EntOttDetalleMaterial.Estado = EstadoRegistro.PENDIENTE_APROBACION
            vlo_EntOttDetalleMaterial.Usuario = Me.Usuario.UserName

            vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttDetalleMaterial)
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Agrega un detalle material a la base de datos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Function ModificarDetalleMaterial(pvn_idDetalleMaterial As Integer, pvn_idMaterial As Integer, pvc_detalle As String, pvn_cantidad As Double) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial
        Dim vlo_EntOtfInventario As Wsr_OT_Catalogos.EntOtfInventario
        Dim pvn_ReservadoTemporal As Double
        Dim vlb_Bandera As Boolean = False

        'instanciar y configurar objetos
        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttDetalleMaterial = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL, pvn_idDetalleMaterial))

            vlo_EntOtfInventario = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                              Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOttDetalleMaterial.IdAlmacenBodega,
                              Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_EntOttDetalleMaterial.IdUbicacionAdministra,
                              Modelo.OTF_INVENTARIO.ID_MATERIAL, vlo_EntOttDetalleMaterial.IdMaterial))

            If vlo_EntOtfInventario.Existe Then

                If vlo_EntOttDetalleMaterial.CantidadReservada > 0 Then

                    pvn_ReservadoTemporal = vlo_EntOtfInventario.CantidadReservada - vlo_EntOttDetalleMaterial.CantidadReservada

                    If pvn_ReservadoTemporal >= pvn_cantidad Then

                        vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada - vlo_EntOttDetalleMaterial.CantidadReservada
                        'vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada + pvn_cantidad
                        vlo_EntOtfInventario.Usuario = Me.Usuario.UserName
                        vlb_Bandera = True
                    Else
                        Return False
                    End If

                End If

            End If

            vlo_EntOttDetalleMaterial.CantidadReservada = 0
            vlo_EntOttDetalleMaterial.CantidadSolicitada = pvn_cantidad
            vlo_EntOttDetalleMaterial.Detalle = pvc_detalle
            vlo_EntOttDetalleMaterial.Usuario = Me.Usuario.UserName

            If vlb_Bandera Then
                vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ModificarRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               vlo_EntOtfInventario)
            End If

            vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttDetalleMaterial)

            Return True

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    Private Function ModificarDetalleMaterialConInventario(pvn_idDetalleMaterial As Integer, pvn_idMaterial As Integer, pvc_detalle As String, pvn_cantidad As Double, pvn_CantidadReservada As Double, pvn_IdAlmacenBodegaViejo As Integer, pvc_idAlmacenViaCompra() As String, pvd_timestamp As Date) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial
        Dim vlc_tipoAmbito As String

        'instanciar y configurar objetos
        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttDetalleMaterial = New EntOttDetalleMaterial
            vlo_EntOttDetalleMaterial.IdDetalleMaterial = pvn_idDetalleMaterial
            vlo_EntOttDetalleMaterial.IdMaterial = pvn_idMaterial
            vlo_EntOttDetalleMaterial.IdUbicacion = Me.IdUbicacion
            vlo_EntOttDetalleMaterial.IdUbicacionAdministra = Me.IdUbicacion
            vlo_EntOttDetalleMaterial.Detalle = pvc_detalle
            vlo_EntOttDetalleMaterial.IdOrdenTrabajo = Me.IdOrdenTrabajo
            vlo_EntOttDetalleMaterial.CantidadSolicitada = pvn_cantidad
            'If pvb_banderaReserva Then
            vlo_EntOttDetalleMaterial.CantidadReservada = 0
            'End If
            vlo_EntOttDetalleMaterial.Estado = EstadoDetalle.PENDIENTE
            vlo_EntOttDetalleMaterial.Usuario = Me.Usuario.UserName
            vlo_EntOttDetalleMaterial.TimeStamp = pvd_timestamp

            vlc_tipoAmbito = pvc_idAlmacenViaCompra(1)
            If vlc_tipoAmbito = Tipo.ALMACEN Or vlc_tipoAmbito = Tipo.BODEGA Then

                If ActualizarInventario(pvn_CantidadReservada, pvn_IdAlmacenBodegaViejo, pvc_idAlmacenViaCompra(0), Me.IdUbicacion, pvn_idMaterial) Then

                    vlo_EntOttDetalleMaterial.IdAlmacenBodega = pvc_idAlmacenViaCompra(0)
                    vlo_EntOttDetalleMaterial.ViaDespacho = vlc_tipoAmbito

                    vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ModificarRegistro(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        vlo_EntOttDetalleMaterial)
                    Return True
                Else
                    Return False
                End If

            Else

                If ActualizarInventario(pvn_CantidadReservada, pvn_IdAlmacenBodegaViejo, 0, Me.IdUbicacion, pvn_idMaterial) Then

                    vlo_EntOttDetalleMaterial.IdViaCompraContrato = pvc_idAlmacenViaCompra(0)
                    vlo_EntOttDetalleMaterial.ViaDespacho = vlc_tipoAmbito

                    vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ModificarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOttDetalleMaterial)

                    Return True
                Else
                    Return False
                End If

            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Agrega un detalle material a la base de datos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BorrarDetalleMaterial()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial

        'instanciar y configurar objetos
        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttDetalleMaterial = New EntOttDetalleMaterial
            vlo_EntOttDetalleMaterial.IdMaterial = Me.txtCodigo.Text
            vlo_EntOttDetalleMaterial.IdUbicacion = Me.IdUbicacion
            vlo_EntOttDetalleMaterial.IdUbicacionAdministra = Me.IdUbicacion
            vlo_EntOttDetalleMaterial.Detalle = WucDatosMaterial.RetornaDetalle
            vlo_EntOttDetalleMaterial.IdOrdenTrabajo = Me.IdOrdenTrabajo
            vlo_EntOttDetalleMaterial.CantidadSolicitada = WucDatosMaterial.RetornaCantidad
            vlo_EntOttDetalleMaterial.Usuario = Me.Usuario.UserName

            vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttDetalleMaterial)
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
    ''' <param name="pvc_idDetalleMaterial"></param>
    ''' <param name="pvc_estado"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>12/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub modificarLineaMaterial(pvc_idDetalleMaterial As String, pvc_estado As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntOttDetalleMaterial = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL, pvc_idDetalleMaterial))

            If vlo_EntOttDetalleMaterial.Existe Then
                vlo_EntOttDetalleMaterial.Estado = pvc_estado
                vlo_EntOttDetalleMaterial.Usuario = Me.Usuario.UserName

                vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ModificarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOttDetalleMaterial)
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
    ''' Metodo encargado de borrar un detalle material
    ''' </summary>
    ''' <param name="pvc_Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>27/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Borrar(pvc_Id As String) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntOttDetalleMaterial = New EntOttDetalleMaterial
            vlo_EntOttDetalleMaterial.IdDetalleMaterial = pvc_Id

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttDetalleMaterial) > 0

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
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Aceptar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_AjusteMateriales(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    Me.Usuario.UserName, Me.IdUbicacion, Me.IdOrdenTrabajo) > 0

        Catch ex As Exception
            If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                Dim vlo_OrdenesDeTrabajoException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                MostrarAlertaError(vlo_OrdenesDeTrabajoException.Message)
            Else
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End If
        End Try
    End Function

    Private Function ActualizarInventario(pvn_CantidadReservada As Integer, pvn_IdAlmacenBodegaViejo As Integer, pvn_idAlmacenBodegaNuevo As Integer, pvn_IdUbicacion As Integer, pvn_idMaterial As Integer) As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_InventarioNuevo As Wsr_OT_Catalogos.EntOtfInventario
        Dim vlo_InventarioViejo As Wsr_OT_Catalogos.EntOtfInventario

        'instanciar y configurar objetos
        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_InventarioNuevo = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
              ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
              ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
              String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                            Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, pvn_idAlmacenBodegaNuevo,
                            Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion,
                            Modelo.OTF_INVENTARIO.ID_MATERIAL, pvn_idMaterial))

            If vlo_InventarioNuevo.Existe Then

                If (vlo_InventarioNuevo.CantidadDisponible - vlo_InventarioNuevo.CantidadReservada) >= pvn_CantidadReservada Then
                    vlo_InventarioNuevo.CantidadReservada = vlo_InventarioNuevo.CantidadReservada + pvn_CantidadReservada

                    'vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ModificarRegistro(
                    '   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    '   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    '   vlo_InventarioNuevo)
                Else
                    Return False
                End If
            End If

            vlo_InventarioViejo = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                                      Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, pvn_IdAlmacenBodegaViejo,
                                      Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion,
                                      Modelo.OTF_INVENTARIO.ID_MATERIAL, pvn_idMaterial))

            If vlo_InventarioViejo.Existe Then
                vlo_InventarioViejo.CantidadReservada = vlo_InventarioViejo.CantidadReservada - pvn_CantidadReservada

                vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ModificarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_InventarioViejo)
            End If

            Return True

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

#End Region

End Class
