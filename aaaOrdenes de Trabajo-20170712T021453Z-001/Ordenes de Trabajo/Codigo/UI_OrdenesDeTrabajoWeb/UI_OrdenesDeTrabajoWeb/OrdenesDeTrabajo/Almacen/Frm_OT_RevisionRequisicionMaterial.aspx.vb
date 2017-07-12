Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports System.Data

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
''' <author>Carlos Gómez Ondoy</author>
''' <creationDate>29/09/2016</creationDate>
''' <changeLog>Se realizo un cambio total de la funcionalidad de la pantalla, ajustandola a lo que realmente solicitaba el requerimiento</changeLog>
Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_RevisionRequisicionMaterial
    Inherits System.Web.UI.Page

#Region "Propiedades"


    Private Property UltimoSortDirection As SortDirection
        Get
            If ViewState("UltimoSortDirection") Is Nothing Then
                Return SortDirection.Ascending
            End If
            Return CType(ViewState("UltimoSortDirection"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("UltimoSortDirection") = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' Almacena el archivo de solicitud de presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property ArchivoSolicitud As EntOttAdjuntoOrdenTrabajo
        Get
            Return CType(ViewState("ArchivoSolicitud"), EntOttAdjuntoOrdenTrabajo)
        End Get
        Set(value As EntOttAdjuntoOrdenTrabajo)
            ViewState("ArchivoSolicitud") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la propiedad para el archivo de respuesta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property ArchivoRespuesta As EntOttAdjuntoOrdenTrabajo
        Get
            Return CType(ViewState("ArchivoRespuesta"), EntOttAdjuntoOrdenTrabajo)
        End Get
        Set(value As EntOttAdjuntoOrdenTrabajo)
            ViewState("ArchivoRespuesta") = value
        End Set
    End Property

    ''' <summary>
    ''' Tamaño maximo del archivo para solicitud oficio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property TamanoArchivoOficio As Integer
        Get
            Return CType(ViewState("TamanoArchivoOficio"), Integer)
        End Get
        Set(value As Integer)
            ViewState("TamanoArchivoOficio") = value
        End Set
    End Property

    ''' <summary>
    ''' Extensiones para archivo solicitud oficio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ExtensionesPermitidasOficio As String
        Get
            Return CType(ViewState("ExtensionesPermitidasOficio"), String)
        End Get
        Set(value As String)
            ViewState("ExtensionesPermitidasOficio") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el objeto de solicitud material para la ot actual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/6/2016</creationDate>
    Private Property SolicitudMaterial As EntOttSolicitudMaterial
        Get
            Return CType(ViewState("SolicitudMaterial"), EntOttSolicitudMaterial)
        End Get
        Set(value As EntOttSolicitudMaterial)
            ViewState("SolicitudMaterial") = value
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ctrl_Materiales_Aceptar(pvc_IdMaterial As Integer, pvc_Descripcion As String, pvn_IdCategoria As Integer, pvn_idSubcategoria As Integer, pvn_CostoPromedio As Integer, pvn_UnidadMedida As Integer) Handles ctrl_Materiales.Aceptar
        CargarCategoria(String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_MATERIAL.ID_CATEGORIA_MATERIAL, pvn_IdCategoria))
        CargarSubCategoria(String.Format("{0} = {1}", Modelo.OTM_SUBCATEGORIA_MATERIAL.ID_SUBCATEGORIA_MATERIAL, pvn_idSubcategoria))
        CargarUnidadMedida(String.Format("{0} = {1}", Modelo.OTM_UNIDAD_MEDIDA.ID_UNIDAD_MEDIDA, pvn_UnidadMedida))
        Me.txtCodigo.Text = pvc_IdMaterial.ToString

        Me.WucDatosMaterial.AsignaDescripcion(pvc_Descripcion)
        Me.WucDatosMaterial.AsignaCategoria(Me.Categoria.Descripcion)
        Me.WucDatosMaterial.AsignaMontoPromedio(String.Format("{0} {1}", pvn_CostoPromedio.ToString(), "Colones"))
        Me.WucDatosMaterial.AsignaSubCategoria(Me.SubCategoria.Descripcion)
        Me.WucDatosMaterial.AsignaUnidadMedida(Me.UnidadMedida.Descripcion)

        upControlDatosMaterial.Visible = True
        upTxtCodigo.Update()
        upControlDatosMaterial.Update()
        WebUtils.RegistrarScript(Me.Page, "InicializarFormulario", "javascript:inicializarFormulario();ocultarFiltroMateriales();")
        WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub txtIdSolicitante_TextChanged(sender As Object, e As EventArgs) Handles txtCodigo.TextChanged
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmMaterial As Wsr_OT_Catalogos.EntOtmMaterial
        Dim pvc_CondicionBusquedas As String

        Try
            'instanciar y configurar objetos
            vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
            vlo_Ws_OT_Catalogos.Timeout = -1
            vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

            Me.WucDatosMaterial.AsignaDescripcion(String.Empty)
            Me.WucDatosMaterial.AsignaCategoria(String.Empty)
            Me.WucDatosMaterial.AsignaMontoPromedio(String.Empty)
            Me.WucDatosMaterial.AsignaSubCategoria(String.Empty)
            Me.WucDatosMaterial.AsignaUnidadMedida(String.Empty)

            If Me.txtCodigo.Text <> "" Then
                pvc_CondicionBusquedas = String.Format("{0} = '{1}'", Modelo.OTM_MATERIAL.ID_MATERIAL, Me.txtCodigo.Text)
                vlo_EntOtmMaterial = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ObtenerRegistro(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                                                                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                                                                   pvc_CondicionBusquedas)
                If vlo_EntOtmMaterial IsNot Nothing AndAlso vlo_EntOtmMaterial.Existe Then

                    CargarCategoria(String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_MATERIAL.ID_CATEGORIA_MATERIAL, vlo_EntOtmMaterial.IdCategoriaMaterial))
                    CargarSubCategoria(String.Format("{0} = {1}", Modelo.OTM_SUBCATEGORIA_MATERIAL.ID_SUBCATEGORIA_MATERIAL, vlo_EntOtmMaterial.IdSubcategoriaMaterial))
                    CargarUnidadMedida(String.Format("{0} = {1}", Modelo.OTM_UNIDAD_MEDIDA.ID_UNIDAD_MEDIDA, vlo_EntOtmMaterial.IdUnidadMedida))
                    Me.txtCodigo.Text = vlo_EntOtmMaterial.IdMaterial.ToString

                    Me.WucDatosMaterial.AsignaDescripcion(vlo_EntOtmMaterial.Descripcion)
                    Me.WucDatosMaterial.AsignaCategoria(Me.Categoria.Descripcion)
                    Me.WucDatosMaterial.AsignaMontoPromedio(String.Format("{0} {1}", vlo_EntOtmMaterial.CostoPromedio.ToString(), "Colones"))
                    Me.WucDatosMaterial.AsignaSubCategoria(Me.SubCategoria.Descripcion)
                    Me.WucDatosMaterial.AsignaUnidadMedida(Me.UnidadMedida.Descripcion)
                    upControlDatosMaterial.Visible = True

                Else
                    Me.WucDatosMaterial.AsignaDescripcion(String.Empty)
                    Me.WucDatosMaterial.AsignaCategoria(String.Empty)
                    Me.WucDatosMaterial.AsignaMontoPromedio(String.Empty)
                    Me.WucDatosMaterial.AsignaSubCategoria(String.Empty)
                    Me.WucDatosMaterial.AsignaUnidadMedida(String.Empty)
                    upControlDatosMaterial.Visible = False

                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoEncontrado();")
                End If
            Else
                upControlDatosMaterial.Visible = False
            End If
            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")
            WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();")
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
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        Dim vlo_NuevaFila As Data.DataRow
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
                String.Format("{0} = {1} AND {2} = '{3}'",
                              Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, Me.IdUbicacion,
                              Modelo.OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))

            If vlo_almacen.Existe Then

                vlo_NuevaFila = DsMaterialesInsert.Tables(0).NewRow

                AgregarDetalleMaterial()

                Me.txtCodigo.Text = String.Empty
                Me.WucDatosMaterial.AsignaDescripcion(String.Empty)
                Me.WucDatosMaterial.AsignaCategoria(String.Empty)
                Me.WucDatosMaterial.AsignaMontoPromedio(String.Empty)
                Me.WucDatosMaterial.AsignaSubCategoria(String.Empty)
                Me.WucDatosMaterial.AsignaUnidadMedida(String.Empty)
                Me.WucDatosMaterial.AsignaDetalle(String.Empty)
                Me.WucDatosMaterial.AsignaCantidad(String.Empty)

                upTxtCodigo.Update()
                upControlDatosMaterial.Update()

                CargarLista(String.Empty)
                Me.upControlDatosMaterial.Visible = False
            End If

            WebUtils.RegistrarScript(Me, "cargarLupaLupa", "cargarLupa();")
            WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();")
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
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibModificar_Click(sender As Object, e As EventArgs)
        Dim vlo_imgModificar As ImageButton
        Dim vlo_idMaterial As String
        Dim vlo_fila() As DataRow

        Try

            vlo_imgModificar = CType(sender, ImageButton)
            vlo_idMaterial = vlo_imgModificar.CommandArgument

            vlo_fila = Me.DsMaterialesInsert.Tables(0).Select(String.Format("{0} = {1}", Modelo.OTM_MATERIAL.ID_MATERIAL, vlo_idMaterial))
            If vlo_fila.Length > 0 Then
                Me.txtCodigo.Text = vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)

                Me.WucDatosMaterial.AsignaCantidad(vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA))
                Me.WucDatosMaterial.AsignaDetalle(vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE))

                txtIdSolicitante_TextChanged(sender, e)
                upTxtCodigo.Update()
                upControlDatosMaterial.Update()
                btnAgregar.Visible = False
                btnModificar.Visible = True
                btnModificar.CommandArgument = vlo_idMaterial
                btnCancelar.Visible = True
                WebUtils.RegistrarScript(Me.Page, "InhabilitarCodigo", "javascript:InhabilitarCodigo();")
                Me.txtCodigo.ReadOnly = True
            End If
            WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();")
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
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnModificar_Click(sender As Object, e As EventArgs)
        Dim vln_idMaterial As Integer
        Dim vlo_fila() As DataRow
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        'instanciar y configurar objetos
        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vln_idMaterial = btnModificar.CommandArgument
            vlo_fila = Me.DsMaterialesInsert.Tables(0).Select(String.Format("{0} = {1}", Modelo.OTM_MATERIAL.ID_MATERIAL, vln_idMaterial))
            If vlo_fila.Length > 0 Then

                ModificarDetalleMaterial(vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL),
                                         Me.txtCodigo.Text, Me.WucDatosMaterial.RetornaDetalle, CType(Me.WucDatosMaterial.RetornaCantidad, Double),
                                         vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.TIME_STAMP),
                                         vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_ALMACEN_BODEGA),
                                         vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_VIA_COMPRA_CONTRATO),
                                         vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.VIA_DESPACHO))
            End If

            CargarLista(String.Empty)

            btnCancelar_Click(sender, e)
            Me.btnModificar.Visible = False
            Me.btnCancelar.Visible = False
            Me.btnAgregar.Visible = True
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As EventArgs)
        Dim vlo_IbBorrar As ImageButton
        Dim vlc_IdDetalleMaterial As String
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_IbBorrar = CType(sender, ImageButton)
            vlc_IdDetalleMaterial = vlo_IbBorrar.CommandArgument

            vlo_EntOttDetalleMaterial = New EntOttDetalleMaterial
            vlo_EntOttDetalleMaterial.IdDetalleMaterial = vlc_IdDetalleMaterial
            vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttDetalleMaterial)

            CargarLista(String.Empty)

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

    ''' <summary>
    ''' Evento que por cada fila adjunta un identificador único para borrar, modificar y para la lista del almacen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpMateriales_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpPedidos.ItemDataBound
        Dim vlo_IdBorrar As ImageButton
        Dim vlo_Modificar As ImageButton

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_IdBorrar = e.Item.FindControl("ibBorrar")
            If vlo_IdBorrar IsNot Nothing Then
                vlo_IdBorrar.Attributes.Add("data-uniqueid", vlo_IdBorrar.UniqueID)
            End If

            vlo_Modificar = e.Item.FindControl("ibModificar")
            If vlo_Modificar IsNot Nothing Then
                vlo_Modificar.Attributes.Add("data-uniqueid", vlo_Modificar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        Me.txtCodigo.Text = String.Empty
        Me.WucDatosMaterial.AsignaDetalle(String.Empty)
        Me.WucDatosMaterial.AsignaCantidad(String.Empty)

        txtIdSolicitante_TextChanged(sender, e)
        upTxtCodigo.Update()
        upControlDatosMaterial.Update()
        Me.btnAgregar.Visible = True
        Me.btnCancelar.Visible = False
        Me.btnModificar.Visible = False
        WebUtils.RegistrarScript(Me.Page, "HabilitarCodigo", "javascript:HabilitarCodigo();cargarLupa();cargarTooltip();")

        Me.txtCodigo.ReadOnly = False
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs)
        Try
            If ((Me.DsMaterialesInsert.Tables(0).Rows.Count > 0) And (Me.rbtAprobada.Checked)) Or (Me.rbtLiquidar.Checked) Or (Me.rbtDevuelta.Checked) Then
                If ((Me.chkSolicitarPresupuesto.Checked) And (Me.ArchivoRespuesta.Archivo IsNot Nothing) And (Me.ArchivoSolicitud.Archivo IsNot Nothing)) Or (Not Me.chkSolicitarPresupuesto.Checked) Then
                    If Aceptar() Then
                        ModificaUltimoRegistroTrazabilidad()
                        mostrarAlertaGuardadoExitoso()
                    Else
                        MostrarAlertaError("No ha sido posible la actualización de datos.")
                    End If
                Else
                    MostrarAlertaError("Al solicitar presupuesto al solicitante se deben cargar ambos archivos.")
                End If
            Else
                MostrarAlertaError("Se debe indicar al menos un material.")
            End If
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub chkSolicitarPresupuesto_CheckedChanged(sender As Object, e As EventArgs)
        Try
            If chkSolicitarPresupuesto.Checked Then
                tblArchivos.Visible = True
                Me.fuSolicitud.Visible = True
                Me.fuRespuesta.Visible = True
                Me.rfvFuSolicitud.Enabled = True
                Me.rfvFuRespuesta.Enabled = True
                Me.btnAgregarSolicitud.Visible = True
                Me.btnAgregarRespuesta.Visible = True
                Me.imgExtensionesSolicitud.Visible = True
                Me.imgExtensionesRespuesta.Visible = True
            Else
                tblArchivos.Visible = False
                Me.rfvFuSolicitud.Enabled = False
                Me.rfvFuRespuesta.Enabled = False
                btnAgregarSolicitud.Visible = False
                Me.btnAgregarRespuesta.Visible = False
                Me.ArchivoSolicitud.Archivo = Nothing
                Me.ArchivoSolicitud.NombreArchivo = String.Empty
                Me.ArchivoRespuesta.Archivo = Nothing
                Me.ArchivoRespuesta.NombreArchivo = String.Empty
                Me.fuSolicitud.Visible = False
                Me.fuRespuesta.Visible = False
                Me.lnkArchivoSolicitud.Visible = False
                Me.lnkArchivoRespuesta.Visible = False
                Me.btnEliminarArchivoSolicitud.Visible = False
                Me.btnEliminarArchivoRespuesta.Visible = False
                Me.btnAgregarSolicitud.Visible = False
                Me.btnAgregarRespuesta.Visible = False
                Me.imgExtensionesSolicitud.Visible = False
                Me.imgExtensionesRespuesta.Visible = False
            End If

            WebUtils.RegistrarScript(Me, "cargarLupaLupa", "cargarLupa();")
            WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' elimina el archivo solicitud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>17/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnEliminarArchivoSolicitud_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminarArchivoSolicitud.Click
        Try
            EliminarArchivo(ArchivoSolicitud)
            Me.ArchivoSolicitud.Archivo = Nothing
            Me.ArchivoSolicitud.NombreArchivo = String.Empty
            Me.fuSolicitud.Visible = True
            Me.lnkArchivoSolicitud.Visible = False
            Me.btnEliminarArchivoSolicitud.Visible = False
            btnAgregarSolicitud.Visible = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' elimina el archivo respuesta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>17/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnEliminarArchivoRespuesta_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminarArchivoRespuesta.Click
        Try
            'EliminarArchivo(ArchivoRespuesta)
            Me.ArchivoRespuesta.Archivo = Nothing
            Me.ArchivoRespuesta.NombreArchivo = String.Empty
            Me.fuRespuesta.Visible = True
            Me.lnkArchivoRespuesta.Visible = False
            Me.btnEliminarArchivoRespuesta.Visible = False
            btnAgregarRespuesta.Visible = True

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
    ''' <creationDate>29/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Sub rbtAprobada_CheckedChanged(sender As Object, e As EventArgs) Handles rbtAprobada.CheckedChanged
        If rbtAprobada.Checked Then
            Me.trObservaciones.Visible = False
            Me.rfvTxtObservaciones.Enabled = False
        Else
            Me.trObservaciones.Visible = True
            Me.rfvTxtObservaciones.Enabled = True
        End If
        upObservaciones.Update()
        WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();cargarLupa() ;")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Sub rbtDevuelta_CheckedChanged(sender As Object, e As EventArgs) Handles rbtDevuelta.CheckedChanged
        If rbtAprobada.Checked Then
            Me.trObservaciones.Visible = False
            Me.rfvTxtObservaciones.Enabled = False
        Else
            Me.trObservaciones.Visible = True
            Me.rfvTxtObservaciones.Enabled = True
        End If
        upObservaciones.Update()
        WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();cargarLupa();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>29/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Sub rbtLiquidar_CheckedChanged(sender As Object, e As EventArgs) Handles rbtLiquidar.CheckedChanged
        If rbtAprobada.Checked Then
            Me.trObservaciones.Visible = False
            Me.rfvTxtObservaciones.Enabled = False
        Else
            Me.trObservaciones.Visible = True
            Me.rfvTxtObservaciones.Enabled = True
        End If
        upObservaciones.Update()
        WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();cargarLupa();")
    End Sub

    ''' <summary>
    ''' Agrega el archivo de solicitud de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarSolicitud_Click(sender As Object, e As EventArgs) Handles btnAgregarSolicitud.Click
        Try
            Me.ArchivoSolicitud.NombreArchivo = Me.fuSolicitud.FileName
            Me.ArchivoSolicitud.Archivo = Me.fuSolicitud.FileBytes
            fuSolicitud.Visible = False
            btnAgregarSolicitud.Visible = False
            lnkArchivoSolicitud.Text = Me.fuSolicitud.FileName
            lnkArchivoSolicitud.Visible = True
            btnEliminarArchivoSolicitud.Visible = True
            ArchivoSolicitud.IdAdjuntoOrdenTrabajo = InsertarArchivo(ArchivoSolicitud)
            Me.SolicitudMaterial.IdAdjuntoSolicita = ArchivoSolicitud.IdAdjuntoOrdenTrabajo
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try

    End Sub

    ''' <summary>
    ''' Agrega el archivo de respuesta 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarRespuesta_Click(sender As Object, e As EventArgs) Handles btnAgregarRespuesta.Click
        Try
            Me.ArchivoRespuesta.NombreArchivo = Me.fuRespuesta.FileName
            Me.ArchivoRespuesta.Archivo = Me.fuRespuesta.FileBytes
            fuRespuesta.Visible = False
            lnkArchivoRespuesta.Text = Me.fuRespuesta.FileName
            btnAgregarRespuesta.Visible = False
            lnkArchivoRespuesta.Visible = True
            btnEliminarArchivoRespuesta.Visible = True
            'ArchivoRespuesta.IdAdjuntoOrdenTrabajo = InsertarArchivo(ArchivoRespuesta)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub lnkRpPedidos_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerExpresionDeOrdenamiento(e.CommandName))
            WebUtils.RegistrarScript(Me, "cargarTootipDetalles", "cargarTootipDetalles();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' descargar archivo planta fisica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivoSolicitud_Click(sender As Object, e As EventArgs) Handles lnkArchivoSolicitud.Click
        DescargaArchivo(Me.ArchivoSolicitud.Archivo, Me.ArchivoSolicitud.NombreArchivo)
    End Sub

    ''' <summary>
    ''' descargar archivo foresta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivoRespuesta_Click(sender As Object, e As EventArgs) Handles lnkArchivoRespuesta.Click
        DescargaArchivo(Me.ArchivoRespuesta.Archivo, Me.ArchivoRespuesta.NombreArchivo)
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Método encargado de descargar archivos
    ''' </summary>
    ''' <param name="pvc_NombreArchivo">nombre del archivo a descargar</param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' 
    ''' </summary>
    ''' <param name="pvc_Mensaje"></param>
    ''' <remarks></remarks>
    ''' <creationDate>16/6/2016</creationDate>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <creationDate>16/6/2016</creationDate>
    Private Sub mostrarAlertaGuardadoExitoso()
        WebUtils.RegistrarScript(Me, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <creationDate>16/6/2016</creationDate>
    Private Sub mostrarAlertSinCantidadDisponible()
        WebUtils.RegistrarScript(Me, "alertaError", "mostrarAlertSinCantidadDisponible();")
    End Sub

    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Orden As String)
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
                String.Format("{0} = '{1}' AND {2} = {3}", Modelo.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo,
                    Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION, Me.IdUbicacion), pvc_Orden, False, 0, 0)

            If Me.DsMaterialesInsert IsNot Nothing AndAlso Me.DsMaterialesInsert.Tables(0).Rows.Count > 0 Then
                Me.rpPedidos.DataSource = DsMaterialesInsert
                Me.rpPedidos.DataMember = Me.DsMaterialesInsert.Tables(0).TableName
                Me.rpPedidos.DataBind()
                Me.rpPedidos.Visible = True
                tituloListado.Visible = True
                For Each vlo_fila In DsMaterialesInsert.Tables(0).Rows
                    vln_costoPromedio = vlo_fila(Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO)
                    MontoTotal = MontoTotal + vln_costoPromedio
                Next
                If MontoTotal < CostoTotalOT Then
                    lblMontoTotal.Attributes.Add("style", "color:black;")
                Else
                    lblMontoTotal.Attributes.Add("style", "color:red;")
                End If

                Me.lblMontoTotal.Text = String.Format("Total: ₡{0}", MontoTotal.ToString("N2"))
            Else
                With Me.rpPedidos
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpPedidos.Visible = False
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
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Usuario = New UsuarioActual()
        leerParametros()
        CargarCostoTotalOrden()
        CargarSolicitudMaterial()
        inicializarArchivos()
        cargarOpcionesArchivos()
        cargarObservaciones()
        cargarAlmacenesVias()
        CargarLista(String.Empty)
        CargarOrdenTrabajo(Me.IdUbicacion, Me.IdOrdenTrabajo)
        InicializarControlesUsuario()
        Me.trObservaciones.Visible = False
        Me.rfvTxtObservaciones.Enabled = False
    End Sub

    ''' <summary>
    ''' Se encarga de leer los parámetros provenientes del listado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' Inicializa los archivos para su posterior insersion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarArchivos()

        Me.ArchivoSolicitud = CargarArchivoSolicitud()
        Me.ArchivoRespuesta = CargarArchivoRespuesta()

        If Not ArchivoSolicitud.Existe Then

            ArchivoSolicitud.IdOrdenTrabajo = IdOrdenTrabajo
            ArchivoSolicitud.IdUbicacion = IdUbicacion
            ArchivoSolicitud.IdTipoDocumento = TipoDocumento.OFICIO
            ArchivoSolicitud.Usuario = Usuario.UserName
            ArchivoSolicitud.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.PRESUPUESTO
            ArchivoSolicitud.Descripcion = "Archivo adjunto de oficio para solicitud de presupuesto"
        Else
            lnkArchivoSolicitud.Text = ArchivoSolicitud.NombreArchivo
            tblArchivos.Visible = True
            fuSolicitud.Visible = False
            btnAgregarSolicitud.Visible = False
            lnkArchivoSolicitud.Visible = True
            btnEliminarArchivoSolicitud.Visible = True
            chkSolicitarPresupuesto.Checked = True
        End If

        If Not ArchivoRespuesta.Existe Then

            ArchivoRespuesta.IdOrdenTrabajo = IdOrdenTrabajo
            ArchivoRespuesta.IdUbicacion = IdUbicacion
            ArchivoRespuesta.IdTipoDocumento = TipoDocumento.OFICIO
            ArchivoRespuesta.Usuario = Usuario.UserName
            ArchivoRespuesta.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.PRESUPUESTO
            ArchivoRespuesta.Descripcion = "Archivo adjunto de respuesta a la solicitud de presupuesto"
        Else
            lnkArchivoRespuesta.Text = ArchivoRespuesta.NombreArchivo
            tblArchivos.Visible = True
            fuRespuesta.Visible = False
            btnAgregarRespuesta.Visible = False
            lnkArchivoRespuesta.Visible = True
            btnEliminarArchivoRespuesta.Visible = True
            chkSolicitarPresupuesto.Checked = True
        End If

        If chkSolicitarPresupuesto.Checked Then
            tblArchivos.Visible = True
            If Not ArchivoSolicitud.Existe Then

                Me.fuSolicitud.Visible = True
                Me.rfvFuSolicitud.Enabled = True
                Me.btnAgregarSolicitud.Visible = True
                Me.imgExtensionesSolicitud.Visible = True
            End If

            If Not ArchivoRespuesta.Existe Then

                Me.fuRespuesta.Visible = True
                Me.rfvFuRespuesta.Enabled = True
                Me.btnAgregarRespuesta.Visible = True
                Me.imgExtensionesRespuesta.Visible = True
            End If
        End If

    End Sub

    ''' <summary>
    ''' Carga las extensiones permitidas y el tamaño maximo de archivos para validaciones
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub cargarOpcionesArchivos()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As Wsr_OT_Catalogos.EntOtmTipoDocumento


        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0}: ID_TIPO_DOCUMENTO
            '{1}: OFICIO
            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Utilerias.OrdenesDeTrabajo.Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.OFICIO))

            'Se cargan los parámetros para validar los archivos que sean tipo oficio
            Me.ExtensionesPermitidasOficio = vlo_EntOtmTipoDocumento.FormatosAdmitidos
            imgExtensionesSolicitud.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidasOficio.ToLower))
            imgExtensionesRespuesta.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidasOficio.ToLower))
            Me.TamanoArchivoOficio = vlo_EntOtmTipoDocumento.TamanioMaximo
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
    '''  <author>César Bermudez Garcia</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub cargarObservaciones()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntOttTrazabilidadProceso = vlo_Ws_OT_OrdenesDeTrabajo.OTT_TRAZABILIDAD_PROCESO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}' AND {2} = '{3}' ORDER BY TIME_STAMP DESC",
                              Modelo.OTT_TRAZABILIDAD_PROCESO.ID_ORDEN_TRABAJO, IdOrdenTrabajo,
                              Modelo.OTT_TRAZABILIDAD_PROCESO.ESTADO_ORDEN_TRABAJO, EstadoOrden.REVISION_PRESUPUESTO_SUPERVISOR))

            If vlo_EntOttTrazabilidadProceso.Existe Then
                Me.txtObservacionesEncargado.Value = vlo_EntOttTrazabilidadProceso.ObservacionesInternas

                If (Me.txtObservacionesEncargado.Value = String.Empty) Or (Me.txtObservacionesEncargado.Value = "-") Then
                    Me.txtObservacionesEncargado.Value = "No indica observaciones"
                End If

            Else
                Me.txtObservacionesEncargado.Value = "No indica observaciones"
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">id de la ubicacion</param>
    ''' <param name="pvn_IdOrdenTrabajo">id de la orden</param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
                String.Empty, String.Empty, False, 0, 0)

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
    ''' <creationDate>16/6/2016</creationDate>
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
    ''' <creationDate>16/6/2016</creationDate>
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
            vlo_EntOttDetalleMaterial.Detalle = Me.WucDatosMaterial.RetornaDetalle
            vlo_EntOttDetalleMaterial.IdOrdenTrabajo = Me.IdOrdenTrabajo
            vlo_EntOttDetalleMaterial.CantidadSolicitada = CType(Me.WucDatosMaterial.RetornaCantidad, Double)
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
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ModificarDetalleMaterial(pvn_idDetalleMaterial As Integer, pvn_idMaterial As Integer, pvc_detalle As String, pvn_cantidad As Double, pvd_timestamp As Date, pvc_IdAlmacenBodega As String, pvc_IdViaCompraContrato As String, pvc_ViaDespacho As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial

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
            vlo_EntOttDetalleMaterial.Usuario = Me.Usuario.UserName
            vlo_EntOttDetalleMaterial.TimeStamp = pvd_timestamp
            vlo_EntOttDetalleMaterial.Estado = EstadoRegistro.PENDIENTE_APROBACION

            If pvc_ViaDespacho <> ViaDespacho.VIACOMPRA Then
                vlo_EntOttDetalleMaterial.IdAlmacenBodega = pvc_IdAlmacenBodega
                vlo_EntOttDetalleMaterial.ViaDespacho = pvc_ViaDespacho
            Else
                vlo_EntOttDetalleMaterial.IdViaCompraContrato = pvc_IdViaCompraContrato
                vlo_EntOttDetalleMaterial.ViaDespacho = pvc_ViaDespacho
            End If

            vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ModificarRegistro(
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
    ''' <creationDate>16/6/2016</creationDate>
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
            vlo_EntOttDetalleMaterial.Detalle = Me.WucDatosMaterial.RetornaDetalle
            vlo_EntOttDetalleMaterial.IdOrdenTrabajo = Me.IdOrdenTrabajo
            vlo_EntOttDetalleMaterial.CantidadSolicitada = CType(Me.WucDatosMaterial.RetornaCantidad, Double)
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
    ''' Carga la solicitud material en memoria y las observaciones generales en el campo de texto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarSolicitudMaterial()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.SolicitudMaterial = vlo_Ws_OT_OrdenesDeTrabajo.OTT_SOLICITUD_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_SOLICITUD_MATERIAL.ID_UBICACION,
                        IdUbicacion, Modelo.OTT_SOLICITUD_MATERIAL.ID_ORDEN_TRABAJO, IdOrdenTrabajo))

            If String.IsNullOrWhiteSpace(SolicitudMaterial.Observaciones) Or SolicitudMaterial.Observaciones = "-" Then
                Me.txtObservacionesGenerales.Value = "No indica observaciones"
            Else
                Me.txtObservacionesGenerales.Value = SolicitudMaterial.Observaciones
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

    '    ArchivoSolicitud.IdOrdenTrabajo = IdOrdenTrabajo
    '    ArchivoSolicitud.IdUbicacion = IdUbicacion
    '    ArchivoSolicitud.IdTipoDocumento = TipoDocumento.OFICIO
    '    ArchivoSolicitud.Usuario = Usuario.UserName
    '    ArchivoSolicitud.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.PRESUPUESTO
    '    ArchivoSolicitud.Descripcion = "Archivo adjunto de oficio para solicitud de presupuesto"

    '    ArchivoRespuesta.IdOrdenTrabajo = IdOrdenTrabajo
    '    ArchivoRespuesta.IdUbicacion = IdUbicacion
    '    ArchivoRespuesta.IdTipoDocumento = TipoDocumento.OFICIO
    '    ArchivoRespuesta.Usuario = Usuario.UserName
    '    ArchivoRespuesta.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.PRESUPUESTO
    '    ArchivoRespuesta.Descripcion = "Archivo adjunto de respuesta a la solicitud de presupuesto"


    Private Function CargarArchivoSolicitud() As EntOttAdjuntoOrdenTrabajo
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_dsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}' AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = 'Archivo adjunto de oficio para solicitud de presupuesto'",
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, IdOrdenTrabajo,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION, IdUbicacion,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO, TipoDocumento.OFICIO,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.PRESUPUESTO,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.DESCRIPCION))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
            If vlo_dsDatos IsNot Nothing Then
                vlo_dsDatos.Dispose()
            End If
        End Try
    End Function

    Private Function CargarArchivoRespuesta() As EntOttAdjuntoOrdenTrabajo
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_dsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}' AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = 'Archivo adjunto de respuesta a la solicitud de presupuesto'",
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, IdOrdenTrabajo,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION, IdUbicacion,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO, TipoDocumento.OFICIO,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.PRESUPUESTO,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.DESCRIPCION))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
            If vlo_dsDatos IsNot Nothing Then
                vlo_dsDatos.Dispose()
            End If
        End Try
    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvo_Adjunto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function InsertarArchivo(pvo_Adjunto As EntOttAdjuntoOrdenTrabajo) As Integer
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_dsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvo_Adjunto)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
            If vlo_dsDatos IsNot Nothing Then
                vlo_dsDatos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvo_Adjunto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function EliminarArchivo(pvo_Adjunto As EntOttAdjuntoOrdenTrabajo) As Integer
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_dsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvo_Adjunto)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
            If vlo_dsDatos IsNot Nothing Then
                vlo_dsDatos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Aceptar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlc_Accion As String = String.Empty

        'instanciar y configurar objetos
        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If rbtLiquidar.Checked Then
                vlc_Accion = "0"
            End If

            If rbtAprobada.Checked Then
                vlc_Accion = "1"
            End If

            If rbtDevuelta.Checked Then
                vlc_Accion = "2"
            End If

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_RespuestaRevisionRequisicionesSupervisor(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.IdOrdenTrabajo, Me.IdUbicacion, Me.txtObservaciones.Text, Me.Usuario.NumEmpleado,
                vlc_Accion, chkSolicitarPresupuesto.Checked, Me.ArchivoRespuesta, Me.SolicitudMaterial.IdAdjuntoSolicita) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    Private Function ModificaUltimoRegistroTrazabilidad() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso

        'instanciar y configurar objetos
        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttTrazabilidadProceso = vlo_Ws_OT_OrdenesDeTrabajo.OTT_TRAZABILIDAD_PROCESO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}' ORDER BY TIME_STAMP DESC",
                              Modelo.OTT_TRAZABILIDAD_PROCESO.ID_UBICACION, Me.IdUbicacion,
                              Modelo.OTT_TRAZABILIDAD_PROCESO.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo,
                              Modelo.OTT_TRAZABILIDAD_PROCESO.ESTADO_ORDEN_TRABAJO, EstadoOrden.EN_EVALUACION))

            vlo_EntOttTrazabilidadProceso.ObservacionesInternas = Me.txtObservaciones.Text

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_TRAZABILIDAD_PROCESO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttTrazabilidadProceso) > 0

            'Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_RespuestaRevisionRequisicionesSupervisor(
            '    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            '    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            '    Me.IdOrdenTrabajo, Me.IdUbicacion, Me.txtObservaciones.Text, Me.Usuario.NumEmpleado,
            '    vlc_Accion, chkSolicitarPresupuesto.Checked, Me.ArchivoRespuesta, Me.SolicitudMaterial.IdAdjuntoSolicita) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    Private Function ObtenerExpresionDeOrdenamiento(pvc_Columna As String) As String
        If UltimoSortDirection = SortDirection.Ascending Then
            UltimoSortDirection = SortDirection.Descending
        Else
            UltimoSortDirection = SortDirection.Ascending
        End If

        Return String.Format("{0} {1}", pvc_Columna, IIf(UltimoSortDirection = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))
    End Function

#End Region

End Class
