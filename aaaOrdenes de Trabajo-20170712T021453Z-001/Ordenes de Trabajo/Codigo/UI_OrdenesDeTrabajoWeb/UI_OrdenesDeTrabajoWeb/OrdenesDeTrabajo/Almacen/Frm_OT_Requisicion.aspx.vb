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
Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_Requisicion
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' Propiedad para la Categoria del material
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>14/6/2016</creationDate>
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
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>6/6/2016</creationDate>
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

    Public Property HistApropSuperv As String
        Get
            If ViewState("HistApropSuperv") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("HistApropSuperv"), String)
        End Get
        Set(value As String)
            ViewState("HistApropSuperv") = value
        End Set
    End Property


    ''' <summary>
    ''' Guarda la ubicación de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>9/6/2016</creationDate>
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
    ''' Diccionario de datos para mantener los seleccionados de la lista de almacenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/6/2016</creationDate>
    Private Property AlmacenesVias As System.Collections.Generic.Dictionary(Of String, Integer)
        Get
            Return CType(ViewState("AlmacenesVias"), System.Collections.Generic.Dictionary(Of String, Integer))
        End Get
        Set(value As System.Collections.Generic.Dictionary(Of String, Integer))
            ViewState("AlmacenesVias") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/6/2016</creationDate>
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
    ''' <creationDate>13/6/2016</creationDate>
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
    ''' Almacena el objeto de solicitud material para la ot actual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>28/6/2016</creationDate>
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
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>9/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs)
        Dim vlo_NuevaFila As Data.DataRow
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtfInventario As Wsr_OT_Catalogos.EntOtfInventario
        Dim vlo_almacen As Wsr_OT_Catalogos.EntOtmAlmacenBodega
        Dim vlc_monto As String()

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

                vlo_EntOtfInventario = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_almacen.IdAlmacenBodega,
                                  Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, Me.IdUbicacion,
                                  Modelo.OTF_INVENTARIO.ID_MATERIAL, Me.txtCodigo.Text))

                vlo_NuevaFila = DsMaterialesInsert.Tables(0).NewRow

                vlc_monto = Me.WucDatosMaterial.RetornaMontoPromedio.Split(" ")

                'If vlo_EntOtfInventario.CantidadDisponible >= CInt(Me.WucDatosMaterial.RetornaCantidad) Then
                AgregarDetalleMaterial()

                vlo_NuevaFila(Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL) = Me.txtCodigo.Text
                vlo_NuevaFila(Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION) = Me.WucDatosMaterial.RetornaDescripcion
                vlo_NuevaFila(Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE) = Me.WucDatosMaterial.RetornaDetalle
                vlo_NuevaFila(Modelo.V_OTT_DETALLE_MATERIALLST.DISPONIBLE_ALMACEN_SOLICITUD) = vlo_EntOtfInventario.CantidadDisponible
                vlo_NuevaFila(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA) = CType(Me.WucDatosMaterial.RetornaCantidad, Double)
                vlo_NuevaFila(Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO) = CInt(vlc_monto(0)) * CType(Me.WucDatosMaterial.RetornaCantidad, Double)

                DsMaterialesInsert.Tables(0).Rows.Add(vlo_NuevaFila)

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

                CargarLista()
                Me.upControlDatosMaterial.Visible = False
                'Else
                '  mostrarAlertSinCantidadDisponible()
                '  End If

            End If

            WebUtils.RegistrarScript(Me, "cargarLupaLupa", "cargarLupa();")
            WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();")
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
    ''' Carga los datos de la fila a modificar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>9/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibModificar_Click(sender As Object, e As EventArgs)
        Dim vlo_imgModificar As ImageButton
        Dim vlo_idDetalleMaterial As String
        Dim vlo_fila() As DataRow

        Try
            vlo_imgModificar = CType(sender, ImageButton)
            vlo_idDetalleMaterial = vlo_imgModificar.CommandArgument

            vlo_fila = Me.DsMaterialesInsert.Tables(0).Select(String.Format("{0} = {1}", Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL, vlo_idDetalleMaterial))
            If vlo_fila.Length > 0 Then
                Me.txtCodigo.Text = vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)

                Me.WucDatosMaterial.AsignaCantidad(vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA))
                Me.WucDatosMaterial.AsignaDetalle(vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE))

                txtIdSolicitante_TextChanged(sender, e)
                upTxtCodigo.Update()
                upControlDatosMaterial.Update()
                btnAgregar.Visible = False
                btnModificar.Visible = True
                btnModificar.CommandArgument = vlo_idDetalleMaterial
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
    ''' <creationDate>9/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnModificar_Click(sender As Object, e As EventArgs)
        Dim vln_idDetalleMaterial As Integer
        Dim vlo_fila() As DataRow
        Dim vlo_EntOtfInventario As Wsr_OT_Catalogos.EntOtfInventario
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial

        'instanciar y configurar objetos
        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vln_idDetalleMaterial = btnModificar.CommandArgument
            vlo_fila = Me.DsMaterialesInsert.Tables(0).Select(String.Format("{0} = {1}", Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL, vln_idDetalleMaterial))
            If vlo_fila.Length > 0 Then

                vlo_EntOttDetalleMaterial = CargarDetalleMaterial(vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL))
                vlo_EntOttDetalleMaterial.Detalle = Me.WucDatosMaterial.RetornaDetalle
                vlo_EntOttDetalleMaterial.CantidadSolicitada = CType(Me.WucDatosMaterial.RetornaCantidad, Double)
                vlo_EntOttDetalleMaterial.Usuario = Me.Usuario.UserName

                vlo_EntOtfInventario = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                       ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                       ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                       String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                                     Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOttDetalleMaterial.IdAlmacenBodega,
                                     Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_EntOttDetalleMaterial.IdUbicacionAdministra,
                                     Modelo.OTF_INVENTARIO.ID_MATERIAL, vlo_EntOttDetalleMaterial.IdMaterial))

                If vlo_EntOtfInventario.Existe Then
                    If (vlo_EntOtfInventario.CantidadDisponible - vlo_EntOtfInventario.CantidadReservada) >= CType(Me.WucDatosMaterial.RetornaCantidad, Double) Then

                        If vlo_EntOttDetalleMaterial.CantidadReservada > 0 Then
                            vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada - vlo_EntOttDetalleMaterial.CantidadReservada
                        End If

                        vlo_EntOttDetalleMaterial.CantidadReservada = CType(Me.WucDatosMaterial.RetornaCantidad, Double)
                        vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada + CType(Me.WucDatosMaterial.RetornaCantidad, Double)
                        vlo_EntOtfInventario.Usuario = Me.Usuario.UserName

                        ModificarInventario(vlo_EntOtfInventario)
                        ModificarDetalleMaterial(vlo_EntOttDetalleMaterial)

                    Else
                        mostrarAlertSinCantidadDisponible()
                    End If
                Else
                    ModificarDetalleMaterial(vlo_EntOttDetalleMaterial)
                End If

            End If

            CargarLista()

            WebUtils.RegistrarScript(Me.Page, "HabilitarCodigo", "javascript:HabilitarCodigo();cargarLupa();")
            Me.txtCodigo.ReadOnly = False

            btnCancelar_Click(sender, e)
            Me.btnModificar.Visible = False
            Me.btnCancelar.Visible = False
            Me.btnAgregar.Visible = True

            WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();")

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
    ''' <creationDate>9/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibAlmacen_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim vlo_IbAlmacen As DropDownList
        Dim vln_idMaterial As Integer
        Dim vln_IdUbicacion As Integer
        Dim vlo_EntOtfInventario As Wsr_OT_Catalogos.EntOtfInventario
        Dim vlo_EntOtmAlmacenBodega As Wsr_OT_Catalogos.EntOtmAlmacenBodega
        Dim vlo_result() As DataRow
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlc_idAlmacenViaCompra() As String
        Dim vlo_EntOtmMaterial As Wsr_OT_Catalogos.EntOtmMaterial
        Dim vln_monto As Integer

        Try

            'instanciar y configurar objetos
            vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
            vlo_Ws_OT_Catalogos.Timeout = -1
            vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

            'Se obtiene el id del material
            vlo_IbAlmacen = CType(sender, DropDownList)
            vln_idMaterial = vlo_IbAlmacen.Attributes("data-idMaterial")
            vln_IdUbicacion = vlo_IbAlmacen.Attributes("data-idUbicacion")

            vlo_EntOtmMaterial = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1} AND {2} = {3}",
                             Modelo.OTM_MATERIAL.ID_MATERIAL, vln_idMaterial,
                             Modelo.OTM_MATERIAL.ID_UBICACION_ADMINISTRA, vln_IdUbicacion))

            'Se busca en el dataset por el id del material obtenido
            vlo_result = Me.DsMaterialesInsert.Tables(0).Select(
                String.Format("{0} = {1}", Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL, vln_idMaterial))

            'Se obtiene el valor seleccionado del dropdownlist
            vlc_idAlmacenViaCompra = vlo_IbAlmacen.SelectedValue.Split("_")

            If AlmacenesVias.ContainsKey(vlo_IbAlmacen.UniqueID) Then
                AlmacenesVias.Item(vlo_IbAlmacen.UniqueID) = vlo_IbAlmacen.SelectedIndex
            Else
                AlmacenesVias.Add(vlo_IbAlmacen.UniqueID, vlo_IbAlmacen.SelectedIndex)
            End If

            'Se obtiene el almacen o via de compra
            'TODO: Filtrar inactivos,orden descripcion asc
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
                'TODO: Filtrar inactivos, orden descripcion asc
                vlo_EntOtfInventario = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                                  Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, Me.IdUbicacion,
                                  Modelo.OTF_INVENTARIO.ID_MATERIAL, vln_idMaterial,
                                  Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOtmAlmacenBodega.IdAlmacenBodega))

                'Si el almacen es de tipo via de compra
                If vlo_EntOtfInventario.Existe Then
                    vln_monto = vlo_EntOtmMaterial.CostoPromedio * CInt(vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA))
                    vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DISPONIBLE_ALMACEN_SOLICITUD) = vlo_EntOtfInventario.CantidadDisponible.ToString
                    vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DISP_ALMACEN_SOLICITUD_MEDIDA) = vlo_EntOtfInventario.CantidadDisponible.ToString
                    vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO) = vln_monto.ToString

                    ModificarDetalleMaterial(vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL),
                                             vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL),
                                             vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE),
                                             vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA),
                                             vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_RESERVADA),
                                             vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_ALMACEN_BODEGA),
                                             vlc_idAlmacenViaCompra, vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.TIME_STAMP), True)
                    CargarLista()
                Else
                    ''mostrarAlertSinCantidadDisponible()
                    vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DISPONIBLE_ALMACEN_SOLICITUD) = 0
                    vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DISP_ALMACEN_SOLICITUD_MEDIDA) = "0"
                    'vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO) = 0

                    Me.rpPedidos.DataSource = DsMaterialesInsert
                    Me.rpPedidos.DataMember = Me.DsMaterialesInsert.Tables(0).TableName
                    Me.rpPedidos.DataBind()


                End If
            Else
                'Se coloca cero ya que se va a hacer un pedido mediante via de compra
                vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DISPONIBLE_ALMACEN_SOLICITUD) = 0
                vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO) = 0

                ModificarDetalleMaterial(vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL),
                                         vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL),
                                         vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE),
                                         vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA),
                                         vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_RESERVADA),
                                         vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_ALMACEN_BODEGA),
                                         vlc_idAlmacenViaCompra, vlo_result(0)(Modelo.V_OTT_DETALLE_MATERIALLST.TIME_STAMP), False)
                CargarLista()

            End If

            WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();")
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
    ''' <creationDate>9/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As EventArgs)
        Dim vlc_IdDetalleMaterial As String
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlc_IdDetalleMaterial = CType(sender, ImageButton).CommandArgument

            vlo_EntOttDetalleMaterial = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL, vlc_IdDetalleMaterial))

            If vlo_EntOttDetalleMaterial.Existe Then
                vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_BorrarRegistro(
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                   vlo_EntOttDetalleMaterial)

                If vlo_EntOttDetalleMaterial.CantidadReservada > 0 Then
                    ActualizarReservadoInventario(vlo_EntOttDetalleMaterial.IdAlmacenBodega, vlo_EntOttDetalleMaterial.IdMaterial, vlo_EntOttDetalleMaterial.IdUbicacionAdministra, vlo_EntOttDetalleMaterial.CantidadReservada)
                End If

            End If

            CargarLista()

            WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();")

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
    ''' <creationDate>9/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpMateriales_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpPedidos.ItemDataBound
        Dim vlo_IdBorrar As ImageButton
        Dim vlo_Almacen As DropDownList
        Dim vlo_Modificar As ImageButton
        Dim vln_IdAlmacenBodega As Integer
        Dim vln_IdViaCompraContrato As Integer
        Dim vlc_IdViaCompra As String

        Dim vln_DispAlmacenBodega As Double
        Dim vlc_CadenaDisponible As String()

        Dim vln_CantidadSolicitada As Double
        Dim vlc_CadenCantidadSolicitada As String()

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_IdBorrar = e.Item.FindControl("ibBorrar")
            If vlo_IdBorrar IsNot Nothing Then
                vlo_IdBorrar.Attributes.Add("data-uniqueid", vlo_IdBorrar.UniqueID)
            End If

            vlo_Modificar = e.Item.FindControl("ibModificar")
            If vlo_Modificar IsNot Nothing Then
                vlo_Modificar.Attributes.Add("data-uniqueid", vlo_Modificar.UniqueID)
            End If

            vlo_Almacen = e.Item.FindControl("ibAlmacen")

            vlo_Almacen.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            If vlo_Almacen IsNot Nothing Then
                vlo_Almacen.Attributes.Add("data-uniqueid", vlo_Almacen.UniqueID)
                With vlo_Almacen
                    .DataSource = DsAlmacenViaCompra
                    .DataMember = DsAlmacenViaCompra.Tables(0).TableName
                    .DataTextField = "DESCRIPCION"
                    .DataValueField = "ID_AMBITO"
                    .DataBind()
                End With

                vln_IdAlmacenBodega = vlo_Almacen.Attributes("data-IdAlmacenBodegaCombo")
                vln_IdViaCompraContrato = vlo_Almacen.Attributes("data-IdViaCompraContratoCombo")
                vlc_IdViaCompra = vlo_Almacen.Attributes("data-IdViaCompraCombo")

                vlc_CadenaDisponible = vlo_Almacen.Attributes("data-DispAlmacenBodega").Split(" ")
                vlc_CadenCantidadSolicitada = vlo_Almacen.Attributes("data-CantidadSolicitadaMedida").Split(" ")

                vln_DispAlmacenBodega = vlc_CadenaDisponible(0)
                vln_CantidadSolicitada = vlc_CadenCantidadSolicitada(0)

                If vlc_IdViaCompra <> ViaDespacho.VIACOMPRA Then
                    If vln_DispAlmacenBodega >= vln_CantidadSolicitada Then
                        vlo_Almacen.SelectedValue = String.Format("{0}_{1}", vln_IdAlmacenBodega, vlc_IdViaCompra)
                    Else
                        vlo_Almacen.SelectedValue = String.Empty
                    End If
                Else
                    vlo_Almacen.SelectedValue = String.Format("{0}_{1}", vln_IdViaCompraContrato, vlc_IdViaCompra)
                End If

                If Me.AlmacenesVias.ContainsKey(vlo_Almacen.UniqueID) Then
                    AlmacenesVias.TryGetValue(vlo_Almacen.UniqueID, vlo_Almacen.SelectedIndex)
                End If

                vlo_Modificar.Attributes.Add("data-idAlmacen", vlo_Almacen.UniqueID)

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
    ''' <creationDate>13/6/2016</creationDate>
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
    ''' descarga el archivo adjunto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>13/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkSolicitudPresupuestaria_Command(sender As Object, e As CommandEventArgs) Handles lnkSolicitudPresupuestaria.Command
        Dim vlo_EntOttAdjuntoOrdenTrabajo As EntOttAdjuntoOrdenTrabajo
        Try
            Response.Clear()
            vlo_EntOttAdjuntoOrdenTrabajo = obtenerArchivo(Me.lnkSolicitudPresupuestaria.CommandArgument)
            Response.AppendHeader("content-disposition", "attachment; filename=" + vlo_EntOttAdjuntoOrdenTrabajo.NombreArchivo)
            Response.BinaryWrite(vlo_EntOttAdjuntoOrdenTrabajo.Archivo)
            Response.End()

            WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();")

        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' descarga el archivo adjunto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>13/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkSolicitudPrep_Command(sender As Object, e As CommandEventArgs) Handles lnkSolicitudPrep.Command
        Dim vlo_EntOttAdjuntoOrdenTrabajo As EntOttAdjuntoOrdenTrabajo
        Try
            Response.Clear()
            vlo_EntOttAdjuntoOrdenTrabajo = obtenerArchivo(Me.lnkSolicitudPrep.CommandArgument)
            Response.AppendHeader("content-disposition", "attachment; filename=" + vlo_EntOttAdjuntoOrdenTrabajo.NombreArchivo)
            Response.BinaryWrite(vlo_EntOttAdjuntoOrdenTrabajo.Archivo)
            Response.End()

            WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();")

        Catch ex_Descarga As System.Threading.ThreadAbortException
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
    ''' <creationDate>13/6/2016</creationDate>
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
        WebUtils.RegistrarScript(Me, "cargarTooltip", "cargarTooltip();cargarLupa();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>13/6/2016</creationDate>
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
    ''' <creationDate>13/2/2017</creationDate>
    ''' <changeLog></changeLog>
    Public Sub rbtRechazada_CheckedChanged(sender As Object, e As EventArgs) Handles rbtRechazada.CheckedChanged
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
    ''' <author>César Bermudez G</author>
    ''' <creationDate>15/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs)
        Try

            If SeleccionAlmacenViaCompra() Then

                If Aceptar() Then
                    mostrarAlertaGuardadoExitoso()
                Else
                    MostrarAlertaError("No ha sido posible la actualización de datos.")
                End If

            Else
                MostrarAlertaError("Todas las lineas deben llevar su respectivo Almacén/Bodega/Vía Compra.")
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

#End Region

#Region "Metodos"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvo_EntOttDetalleMaterial"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ActualizarReservadoInventario(pvo_EntOttDetalleMaterial As EntOttDetalleMaterial, pvn_CantidadSolicitada As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtfInventario As Wsr_OT_Catalogos.EntOtfInventario

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtfInventario = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                              Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, pvo_EntOttDetalleMaterial.IdAlmacenBodega,
                              Modelo.OTF_INVENTARIO.ID_MATERIAL, pvo_EntOttDetalleMaterial.IdMaterial,
                              Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, pvo_EntOttDetalleMaterial.IdUbicacionAdministra))

            vlo_EntOtfInventario.CantidadReservada = (vlo_EntOtfInventario.CantidadReservada - pvo_EntOttDetalleMaterial.CantidadReservada)
            vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada + pvn_CantidadSolicitada
            vlo_EntOtfInventario.Usuario = Me.Usuario.UserName

            vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtfInventario)

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
    ''' <param name="pvn_IdAlmacenBodega"></param>
    ''' <param name="pvn_IdMaterial"></param>
    ''' <param name="pvn_IdUbicacionAdministra"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ActualizarReservadoInventario(pvn_IdAlmacenBodega As Integer, pvn_IdMaterial As Integer, pvn_IdUbicacionAdministra As Integer, pvn_CantidadReservada As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtfInventario As Wsr_OT_Catalogos.EntOtfInventario

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtfInventario = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                              Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, pvn_IdAlmacenBodega,
                              Modelo.OTF_INVENTARIO.ID_MATERIAL, pvn_IdMaterial,
                              Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra))

            vlo_EntOtfInventario.CantidadReservada = (vlo_EntOtfInventario.CantidadReservada - pvn_CantidadReservada)
            vlo_EntOtfInventario.Usuario = Me.Usuario.UserName

            vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtfInventario)

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
    ''' <param name="pvc_Mensaje"></param>
    ''' <remarks></remarks>
    ''' <creationDate>6/6/2016</creationDate>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub


    Private Sub mostrarAlertaGuardadoExitoso()
        WebUtils.RegistrarScript(Me, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <creationDate>/6/2016</creationDate>
    Private Sub mostrarAlertSinCantidadDisponible()
        WebUtils.RegistrarScript(Me, "alertaError", "mostrarAlertSinCantidadDisponible();")
    End Sub

    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>6/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista()
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
                    Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION, Me.IdUbicacion), String.Format("{0} ASC", Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL), False, 0, 0)

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
    ''' <creationDate>6/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Usuario = New UsuarioActual()
        AlmacenesVias = New Dictionary(Of String, Integer)
        leerParametros()
        CargarCostoTotalOrden()
        CargarSolicitudMaterial()
        cargarAlmacenesVias()
        CargarLista()
        InicializarControlesUsuario()
        CargarArchivosOficio()
        Me.trObservaciones.Visible = False
        Me.rfvTxtObservaciones.Enabled = False

        If HistApropSuperv.Equals("1") Then
            Me.rbtDevuelta.Visible = False
            upObservaciones.Update()
        End If

    End Sub

    ''' <summary>
    ''' Se encarga de leer los parámetros provenientes del listado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>6/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub leerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        Me.IdSectorTaller = WebUtils.LeerParametro(Of Integer)("pvn_IdSectorTaller")
        Me.HistApropSuperv = WebUtils.LeerParametro(Of String)("pvc_HistApropSuperv")

        Me.Session.Add("pvn_IdUbicacion", IdUbicacion)
        Me.Session.Add("pvc_IdOrdenTrabajo", IdOrdenTrabajo)
        Me.Session.Add("pvn_Anno", Anno)
        Me.Session.Add("pvn_IdSectorTaller", IdSectorTaller)


    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario para mostrar la información general
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>6/6/2016</creationDate>
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
    ''' <creationDate>9/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub cargarAlmacenesVias()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            'TODO: Cambiar orden descripcion asc, Filtrar Inactivos
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
    ''' Carga la solicitud material en memoria y las observaciones generales en el campo de texto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>28/6/2016</creationDate>
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
                Me.txtObservacionesGenerales.Text = "No indica observaciones"
            Else
                Me.txtObservacionesGenerales.Text = SolicitudMaterial.Observaciones
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
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>13/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarArchivosOficio()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_dsDatos As DataSet
        Dim vlo_fila As DataRow

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            'Se listan los archivos que la solicitud traiga 
            '{0}: nombre de la columna id_adjunto
            '{1}: id adjunto respuesta de la solicitud de material
            '{2}: id adjunto solicita de la solicitud de material

            vlo_dsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarVOttAdjuntoLigerolst(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} OR {0} = {2}",
                        Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO,
                    SolicitudMaterial.IdAdjuntoRespuesta, SolicitudMaterial.IdAdjuntoSolicita), String.Empty, False, 0, 0)

            If vlo_dsDatos.Tables.Count > 0 AndAlso vlo_dsDatos.Tables(0).Rows.Count > 0 Then
                vlo_fila = vlo_dsDatos.Tables(0).Rows(0)

                Me.lnkSolicitudPresupuestaria.Text = vlo_fila(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)
                Me.lnkSolicitudPresupuestaria.CommandArgument = vlo_fila(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO)
                Me.tr1.Visible = True

                If vlo_dsDatos.Tables(0).Rows.Count >= 2 Then
                    vlo_fila = vlo_dsDatos.Tables(0).Rows(1)
                    Me.lnkSolicitudPrep.Text = vlo_fila(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)
                    Me.lnkSolicitudPrep.CommandArgument = vlo_fila(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO)
                    Me.tr2.Visible = True
                End If
            End If


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
    End Sub

    ''' <summary>
    ''' Carga el costo total de la orden de trabajo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>13/6/2016</creationDate>
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
    ''' <creationDate>13/6/2016</creationDate>
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
    ''' <creationDate>13/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ModificarDetalleMaterial(pvo_EntOttDetalleMaterial As EntOttDetalleMaterial)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo

        Try
            'instanciar y configurar objetos
            vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
            vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
            vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials


            vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvo_EntOttDetalleMaterial)
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub


    Private Sub ModificarInventario(pvo_EntOtfInventario As Wsr_OT_Catalogos.EntOtfInventario)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        'instanciar y configurar objetos
        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvo_EntOtfInventario)

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
    ''' <creationDate>13/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ModificarDetalleMaterial(pvn_idDetalleMaterial As Integer, pvn_idMaterial As Integer, pvc_detalle As String, pvn_cantidad As Integer, pvn_CantidadReservada As Integer, pvn_IdAlmacenBodegaViejo As Integer, pvc_idAlmacenViaCompra() As String, pvd_timestamp As Date, pvb_banderaReserva As Boolean)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial
        Dim vlc_tipoAmbito As String

        Try
            'instanciar y configurar objetos
            vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
            vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
            vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

            vlo_EntOttDetalleMaterial = New EntOttDetalleMaterial
            vlo_EntOttDetalleMaterial.IdDetalleMaterial = pvn_idDetalleMaterial
            vlo_EntOttDetalleMaterial.IdMaterial = pvn_idMaterial
            vlo_EntOttDetalleMaterial.IdUbicacion = Me.IdUbicacion
            vlo_EntOttDetalleMaterial.IdUbicacionAdministra = Me.IdUbicacion
            vlo_EntOttDetalleMaterial.Detalle = pvc_detalle
            vlo_EntOttDetalleMaterial.IdOrdenTrabajo = Me.IdOrdenTrabajo
            vlo_EntOttDetalleMaterial.CantidadSolicitada = pvn_cantidad
            If pvb_banderaReserva Then
                vlo_EntOttDetalleMaterial.CantidadReservada = pvn_cantidad
            End If
            vlo_EntOttDetalleMaterial.Estado = EstadoDetalle.PENDIENTE
            vlo_EntOttDetalleMaterial.Usuario = Me.Usuario.UserName
            vlo_EntOttDetalleMaterial.TimeStamp = pvd_timestamp

            vlc_tipoAmbito = pvc_idAlmacenViaCompra(1)
            If vlc_tipoAmbito = Tipo.ALMACEN Or vlc_tipoAmbito = Tipo.BODEGA Then
                vlo_EntOttDetalleMaterial.IdAlmacenBodega = pvc_idAlmacenViaCompra(0)
                vlo_EntOttDetalleMaterial.ViaDespacho = vlc_tipoAmbito

                vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ModificarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOttDetalleMaterial)

                ActualizarInventario(pvn_CantidadReservada, pvn_IdAlmacenBodegaViejo, pvc_idAlmacenViaCompra(0), Me.IdUbicacion, pvn_idMaterial)

            Else
                vlo_EntOttDetalleMaterial.IdViaCompraContrato = pvc_idAlmacenViaCompra(0)
                vlo_EntOttDetalleMaterial.ViaDespacho = vlc_tipoAmbito

                vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttDetalleMaterial)

                ActualizarInventario(pvn_CantidadReservada, pvn_IdAlmacenBodegaViejo, 0, Me.IdUbicacion, pvn_idMaterial)

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
    ''' Actualiza el inventario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ActualizarInventario(pvn_CantidadReservada As Integer, pvn_IdAlmacenBodegaViejo As Integer, pvn_idAlmacenBodegaNuevo As Integer, pvn_IdUbicacion As Integer, pvn_idMaterial As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_InventarioNuevo As Wsr_OT_Catalogos.EntOtfInventario
        Dim vlo_InventarioViejo As Wsr_OT_Catalogos.EntOtfInventario

        'instanciar y configurar objetos
        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
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

            vlo_InventarioNuevo = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                              Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, pvn_idAlmacenBodegaNuevo,
                              Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion,
                              Modelo.OTF_INVENTARIO.ID_MATERIAL, pvn_idMaterial))

            If vlo_InventarioNuevo.Existe Then
                vlo_InventarioNuevo.CantidadReservada = vlo_InventarioNuevo.CantidadReservada + pvn_CantidadReservada

                vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ModificarRegistro(
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                   vlo_InventarioNuevo)
            End If

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
    ''' <creationDate>13/6/2016</creationDate>
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

#End Region

#Region "Funciones"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvn_DetalleMaterial"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargarDetalleMaterial(pvn_DetalleMaterial As Integer) As EntOttDetalleMaterial
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL, pvn_DetalleMaterial))

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
    ''' <param name="pvc_idAdjunto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>13/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function obtenerArchivo(pvc_idAdjunto As String) As EntOttAdjuntoOrdenTrabajo
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_dsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO, pvc_idAdjunto))

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

    Private Function SeleccionAlmacenViaCompra() As Boolean
        Dim vlo_Almacen As DropDownList

        Try
            For Each item In rpPedidos.Items
                vlo_Almacen = item.FindControl("ibAlmacen")
                If vlo_Almacen.SelectedValue = String.Empty Then
                    Return False
                End If
            Next

            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>13/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Aceptar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo

        'instanciar y configurar objetos
        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_RespuestaRevisionRequisiciones(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.DsMaterialesInsert, Me.IdOrdenTrabajo, Me.IdUbicacion, Me.txtObservacion.Text,
                Me.Usuario.NumEmpleado, Me.rbtAprobada.Checked, Me.rbtDevuelta.Checked, Me.rbtRechazada.Checked) > 0

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
