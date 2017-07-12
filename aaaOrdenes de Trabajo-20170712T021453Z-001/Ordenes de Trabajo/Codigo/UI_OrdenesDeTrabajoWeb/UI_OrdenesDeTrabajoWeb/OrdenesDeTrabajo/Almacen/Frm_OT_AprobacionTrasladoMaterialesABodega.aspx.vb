Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports Wsr_OT_Catalogos
Imports System.Data

Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_AprobacionTrasladoMaterialesABodega
    Inherits System.Web.UI.Page

#Region "Propiedades"
    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de materiales a ingresar eliminar o modificar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/06/2016</creationDate>
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
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/06/2016</creationDate>
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
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property CantidadMateriales As Integer
        Get
            Return CType(ViewState("CantidadMateriales"), Integer)
        End Get
        Set(value As Integer)
            ViewState("CantidadMateriales") = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/06/2016</creationDate>
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
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdSolicitudTraslado As Integer
        Get
            Return CType(ViewState("IdSolicitudTraslado"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdSolicitudTraslado") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property IdAlmacen As Integer
        Get
            Return CType(ViewState("IdAlmacen"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdAlmacen") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el año de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Anio As Integer
        Get
            Return CType(ViewState("Anio"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Anio") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property IdBodega As Integer
        Get
            Return CType(ViewState("IdBodega"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdBodega") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/06/2016</creationDate>
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
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property SolicitudTraslado As EntOttSolicitudTraslado
        Get
            Return CType(ViewState("SolicitudTraslado"), EntOttSolicitudTraslado)
        End Get
        Set(value As EntOttSolicitudTraslado)
            ViewState("SolicitudTraslado") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la Categoria del material
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/6/2016</creationDate>
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/6/2016</creationDate>
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/6/2016</creationDate>
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
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Estado As String
        Get
            Return CType(ViewState("Estado"), String)
        End Get
        Set(value As String)
            ViewState("Estado") = value
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
    ''' <creationDate>24/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                InicializarFormulario()
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
    ''' <creationDate>24/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkEjecutarBusquedaMaterial_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusquedaMaterial.Click
        Try
            Me.ctrl_Materiales.mostrarAlmacenPartida = False
            WebUtils.RegistrarScript(Me.Page, "mostrarPopUpBusquedaMaterial", "javascript:mostrarPopUp('#PopUpBusquedaMateriales');cargarLupa();")
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
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
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
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub txtCodigo_TextChanged(sender As Object, e As EventArgs) Handles txtCodigo.TextChanged
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
                    IdMaterial = vlo_EntOtmMaterial.IdMaterial.ToString
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
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoEncontrado();")
                    upControlDatosMaterial.Visible = False
                End If
            Else
                upControlDatosMaterial.Visible = False
            End If
            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarMaterial_Click(sender As Object, e As EventArgs) Handles btnAgregarMaterial.Click
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
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, Me.IdUbicacion, Modelo.OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))

            If vlo_almacen.Existe Then

                vlo_EntOtfInventario = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, Me.IdBodega,
                                  Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, Me.IdUbicacion,
                                  Modelo.OTF_INVENTARIO.ID_MATERIAL, Me.txtCodigo.Text))

                If vlo_EntOtfInventario.Existe Then
                    If vlo_EntOtfInventario.CantidadDisponible >= CType(Me.WucDatosMaterial.RetornaCantidad, Double) Then



                        'vlo_NuevaFila = DsMaterialesInsert.Tables(0).NewRow
                        vlc_monto = Me.WucDatosMaterial.RetornaMontoPromedio.Split(" ")
                        If (Me.WucDatosMaterial.RetornaCantidad <> String.Empty) AndAlso (CType(Me.WucDatosMaterial.RetornaCantidad, Double) > 0) Then
                            If Operacion = eOperacion.Agregar Then
                                If Not String.IsNullOrWhiteSpace(Me.txtCodigo.Text) Then

                                    If AgregarDsMateriales(CType(Me.WucDatosMaterial.RetornaCantidad, Double), 0, Me.WucDatosMaterial.RetornaDetalle, CType(vlc_monto(0), Double), vlo_EntOtfInventario.CantidadDisponible, Me.WucDatosMaterial.RetornaDescripcion) Then
                                        'WebUtils.RegistrarScript(Me.Page, "OcultarPopUpUnidad", "javascript:ocultarPopUpFondo();AgregarImagenBorrar();")
                                        LlenarRepeaterPedidos(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_TRASLADOLST.ID_MATERIAL)

                                    Else
                                        MostrarAlertaError("El material no se pudo agregar")
                                    End If
                                Else
                                    MostrarAlertaError("Debe Agregar el código")
                                End If
                            Else
                                If AgregarDsMateriales(CType(Me.WucDatosMaterial.RetornaCantidad, Double), 0, Me.WucDatosMaterial.RetornaDetalle, CType(vlc_monto(0), Double), vlo_EntOtfInventario.CantidadDisponible, Me.WucDatosMaterial.RetornaDescripcion) Then
                                    'WebUtils.RegistrarScript(Me.Page, "OcultarPopUpUnidad", "javascript:ocultarPopUpFondo();AgregarImagenBorrar();")
                                    LlenarRepeaterPedidos(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_TRASLADOLST.ID_MATERIAL)
                                Else
                                    MostrarAlertaError("El material no se pudo modificar")
                                End If
                            End If

                            Me.txtCodigo.Text = String.Empty
                            Me.WucDatosMaterial.AsignaDescripcion(String.Empty)
                            Me.WucDatosMaterial.AsignaCategoria(String.Empty)
                            Me.WucDatosMaterial.AsignaMontoPromedio(String.Empty)
                            Me.WucDatosMaterial.AsignaSubCategoria(String.Empty)
                            Me.WucDatosMaterial.AsignaUnidadMedida(String.Empty)
                            Me.WucDatosMaterial.AsignaDetalle(String.Empty)
                            Me.WucDatosMaterial.AsignaCantidad(String.Empty)
                            Me.WucDatosMaterial.AsignaMontoPromedio(String.Empty)
                            upControlDatosMaterial.Visible = False
                            upTxtCodigo.Update()
                            upControlDatosMaterial.Update()
                        Else
                            WebUtils.RegistrarScript(Me, "mostrarAlertCantidadCero", "mostrarAlertCantidadCero();")
                        End If
                    Else
                        mostrarAlertSinCantidadDisponible()
                    End If
                Else
                    MostrarAlertaError("El material no se encuentra en el inventario")
                End If
            End If

            WebUtils.RegistrarScript(Me, "cargarLupaLupa", "cargarLupa();")
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnModificarMaterial_Click(sender As Object, e As EventArgs) Handles btnModificarMaterial.Click
        Dim vlo_EntOtfInventario As Wsr_OT_Catalogos.EntOtfInventario
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_almacen As Wsr_OT_Catalogos.EntOtmAlmacenBodega
        Dim vlc_monto As String()

        'instanciar y configurar objetos
        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

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
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, Me.IdBodega,
                                  Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, Me.IdUbicacion,
                                  Modelo.OTF_INVENTARIO.ID_MATERIAL, Me.txtCodigo.Text))
                If vlo_EntOtfInventario.Existe Then
                    If vlo_EntOtfInventario.CantidadDisponible >= CInt(Me.WucDatosMaterial.RetornaCantidad) Then
                        vlc_monto = Me.WucDatosMaterial.RetornaMontoPromedio.Split(" ")
                        If Not String.IsNullOrWhiteSpace(txtCodigo.Text) Then
                            If BorrarDetalleMaterial(IdMaterial) Then
                                If AgregarDsMateriales(CType(Me.WucDatosMaterial.RetornaCantidad, Double), 0, Me.WucDatosMaterial.RetornaDetalle, CType(vlc_monto(0), Double), CType(Me.WucDatosMaterial.RetornaCantidad, Double), Me.WucDatosMaterial.RetornaDescripcion) Then
                                    LlenarRepeaterPedidos(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_TRASLADOLST.ID_MATERIAL)
                                    Me.txtCodigo.Text = String.Empty
                                    Me.WucDatosMaterial.AsignaDescripcion(String.Empty)
                                    Me.WucDatosMaterial.AsignaCategoria(String.Empty)
                                    Me.WucDatosMaterial.AsignaMontoPromedio(String.Empty)
                                    Me.WucDatosMaterial.AsignaSubCategoria(String.Empty)
                                    Me.WucDatosMaterial.AsignaUnidadMedida(String.Empty)
                                    Me.WucDatosMaterial.AsignaDetalle(String.Empty)
                                    Me.WucDatosMaterial.AsignaCantidad(String.Empty)
                                    Me.WucDatosMaterial.AsignaMontoPromedio(String.Empty)
                                    upControlDatosMaterial.Visible = False
                                    upTxtCodigo.Update()
                                    upControlDatosMaterial.Update()
                                Else
                                    MostrarAlertaError("El material no se pudo modificar")
                                End If
                            Else
                                MostrarAlertaError("El material no se pudo modificar")
                            End If
                        Else
                            MostrarAlertaError("Los datos no se cargaron correctamente, por favor intentelo nuevamente")
                        End If
                        WebUtils.RegistrarScript(Me.Page, "HabilitarCodigo", "javascript:HabilitarCodigo();cargarLupa();")
                        Me.txtCodigo.ReadOnly = False

                        btnCancelarMaterial_Click(sender, e)
                        Me.btnModificarMaterial.Visible = False
                        Me.btnCancelarMaterial.Visible = False
                        Me.btnAgregarMaterial.Visible = True
                    Else
                        mostrarAlertSinCantidadDisponible()
                    End If
                Else
                    MostrarAlertaError("El material no se encuentra en el inventario")
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar, ubicada en la 
    ''' segunda columna de cada registro del listado, llama a la función borrar y muestra  un mensaje según 
    ''' la operacion efectuada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrarMaterial_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_IbBorrar As ImageButton
        Dim vlc_IdMaterial As String

        Try
            If CantidadMateriales = 1 Then
                MostrarAlertaError("El proveedor debe tener al menos un material")
            Else

                vlo_IbBorrar = CType(sender, ImageButton)
                vlc_IdMaterial = vlo_IbBorrar.CommandArgument
                If BorrarDetalleMaterial(vlc_IdMaterial) Then
                    LlenarRepeaterPedidos(Utilerias.OrdenesDeTrabajo.Modelo.OTT_LINEA_TRASLADO.ID_MATERIAL)
                Else
                    MostrarAlertaError("El material no fue borrado")
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
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el boton modificar del listato de encargados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibModificarMaterial_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()
        Try

            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            If Not String.IsNullOrWhiteSpace(vlc_Llave(0)) Then
                Me.txtCodigo.Text = vlc_Llave(0)
                Me.IdMaterial = vlc_Llave(0)

                Me.ctrl_Materiales.mostrarAlmacenPartida = False
                Me.ctrl_Materiales.Inicializar()
                Me.WucDatosMaterial.AsignaCantidad(vlc_Llave(1))
                Me.WucDatosMaterial.AsignaDescripcion(vlc_Llave(2))


                txtCodigo_TextChanged(sender, e)
                Me.txtCodigo.Enabled = False
                upTxtCodigo.Update()
                btnAgregarMaterial.Visible = False
                btnModificarMaterial.Visible = True
                btnModificarMaterial.CommandArgument = CType(sender, ImageButton).CommandArgument
                btnCancelarMaterial.Visible = True
                WebUtils.RegistrarScript(Me.Page, "InhabilitarCodigo", "javascript:InhabilitarCodigo();")
                Me.txtCodigo.ReadOnly = True
            Else
                MostrarAlertaError("Los datos no se cargaron correctamente, por favor intentelo nuevamente")
            End If
            LlenarRepeaterPedidos(Utilerias.OrdenesDeTrabajo.Modelo.OTT_LINEA_TRASLADO.ID_MATERIAL)
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
    ''' <creationDate>21/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnCancelarMaterial_Click(sender As Object, e As EventArgs) Handles btnCancelarMaterial.Click
        Me.txtCodigo.Text = String.Empty
        Me.txtCodigo.Enabled = True
        Me.WucDatosMaterial.AsignaDetalle(String.Empty)
        Me.WucDatosMaterial.AsignaCantidad(String.Empty)

        txtCodigo_TextChanged(sender, e)
        upTxtCodigo.Update()
        upControlDatosMaterial.Update()
        Me.btnAgregarMaterial.Visible = True
        Me.btnCancelarMaterial.Visible = False
        Me.btnModificarMaterial.Visible = False
        WebUtils.RegistrarScript(Me.Page, "HabilitarCodigo", "javascript:HabilitarCodigo();cargarLupa();")
        Me.txtCodigo.ReadOnly = False

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpPedidos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpPedidos.ItemDataBound
        Dim vlo_IdBorrar As ImageButton
        Dim vlo_Modificar As ImageButton

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_IdBorrar = e.Item.FindControl("ibBorrarMaterial")
            If vlo_IdBorrar IsNot Nothing Then
                vlo_IdBorrar.Attributes.Add("data-uniqueid", vlo_IdBorrar.UniqueID)
            End If

            vlo_Modificar = e.Item.FindControl("ibModificarMaterial")
            If vlo_Modificar IsNot Nothing Then
                vlo_Modificar.Attributes.Add("data-uniqueid", vlo_Modificar.UniqueID)
            End If

        End If
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try
                If Me.CantidadMateriales > 0 Then
                    If Aceptar() Then
                        WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarAlertaActualizacionExitosa();")
                    Else
                        MostrarAlertaError("No ha sido posible actualizar la información.")
                    End If
                Else
                    MostrarAlertaError("La soliciud debe tener materiales.")
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
#End Region

#Region "Métodos"

    Private Sub mostrarAlertSinCantidadDisponible()
        WebUtils.RegistrarScript(Me, "alertaError", "mostrarAlertSinCantidadDisponible();")
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
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
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Lee los parametros 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametros()
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.Anio = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        Me.IdBodega = WebUtils.LeerParametro(Of Integer)("pvn_IdBodega")
        Me.IdAlmacen = WebUtils.LeerParametro(Of String)("pvn_IdAlmacen")
        Me.IdSolicitudTraslado = WebUtils.LeerParametro(Of Integer)("pvc_IdSolicitudTraslado")
        Me.lblNombreBodega.Text = WebUtils.LeerParametro(Of String)("pvc_NombreAlmacen")
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar los diferentess componentes del formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        Me.Usuario = New UsuarioActual
        Me.SolicitudTraslado = New EntOttSolicitudTraslado
        LeerParametros()
        Me.ctrl_Materiales.mostrarAlmacenPartida = False
        Me.ctrl_Materiales.Inicializar()
        Me.WucDatosMaterial.AsignaDescripcion(String.Empty)
        Me.WucDatosMaterial.AsignaCategoria(String.Empty)
        Me.WucDatosMaterial.AsignaMontoPromedio(String.Empty)
        Me.WucDatosMaterial.AsignaSubCategoria(String.Empty)
        Me.WucDatosMaterial.AsignaUnidadMedida(String.Empty)
        upControlDatosMaterial.Visible = False

        Select Case Me.Operacion
            Case Is = eOperacion.Modificar
                Try
                    If Not String.IsNullOrWhiteSpace(Me.Anio) And Not String.IsNullOrWhiteSpace(Me.IdSolicitudTraslado) And Not String.IsNullOrWhiteSpace(Me.IdUbicacion) Then
                        CargarListaMateriales(String.Empty, 1)
                        CargarSolucionTraslado()
                    Else
                        MostrarAlertaError("Disculpe, No se cargo los datos correctamente")
                    End If
                Catch ex As Exception
                    Throw
                End Try
        End Select
    End Sub

    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaMateriales(pvc_Orden As String, pvn_NumeroPaginacion As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vln_MontoTotal As Double

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsMaterialesInsert = vlo_Ws_OT_Catalogos.OTT_LINEA_TRASLADO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}' AND {2} = {3} AND {4} = '{5}' AND {6} = {7}", Modelo.V_OTT_LINEA_TRASLADOLST.ID_ALMACEN, Me.IdBodega, Modelo.V_OTT_LINEA_TRASLADOLST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_LINEA_TRASLADOLST.ESTADO, EstadoDetalle.PENDIENTE_APROBACION, Modelo.V_OTT_LINEA_TRASLADOLST.ID_SOLICITUD_TRASLADO, Me.IdSolicitudTraslado),
                pvc_Orden,
                True,
                pvn_NumeroPaginacion,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))
            Me.DsMaterialesInsert.Tables(0).PrimaryKey = New DataColumn() {Me.DsMaterialesInsert.Tables(0).Columns(Modelo.OTT_LINEA_TRASLADO.ID_MATERIAL)}
            If Me.DsMaterialesInsert IsNot Nothing AndAlso Me.DsMaterialesInsert.Tables(0).Rows.Count > 0 Then

                Me.rpPedidos.DataSource = Me.DsMaterialesInsert
                Me.rpPedidos.DataMember = Me.DsMaterialesInsert.Tables(0).TableName
                Me.rpPedidos.DataBind()
                Me.rpPedidos.Visible = True

                vln_MontoTotal = 0
                For Each vlo_fila In DsMaterialesInsert.Tables(0).Rows
                    vln_MontoTotal = vln_MontoTotal + vlo_fila(Modelo.V_OTT_LINEA_TRASLADOLST.COSTO_PROMEDIO)
                Next
                Me.lblMontoTotal.Text = String.Format("Total: ₡{0}", vln_MontoTotal.ToString("N2"))
                Me.CantidadMateriales = Me.DsMaterialesInsert.Tables(0).Rows.Count
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de Materiales: {0}", Me.CantidadMateriales)

            Else
                With Me.rpPedidos
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpPedidos.Visible = False
                Me.lblCantidadDeRegistros.Visible = False
            End If
            If Me.CantidadMateriales = 0 Then
                Me.txtMaterialValidacion.Text = String.Empty
            Else
                Me.txtMaterialValidacion.Text = Me.CantidadMateriales
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
    '''Método que carga un cliente temporal en la entidad y en los campos de texto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Jeannette Chavarría Rojas</author>
    ''' <creationDate>17/12/2015</creationDate>
    ''' <changeLog></changeLog> 
    Private Sub CargarSolucionTraslado()
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_pais As String = String.Empty

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials


        Try
            Me.SolicitudTraslado = vlo_Ws_OT_Catalogos.OTT_SOLICITUD_TRASLADO_ObtenerRegistroLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0}) = {1} AND UPPER({2}) = {3} AND UPPER({4}) = {5}", Modelo.OTT_SOLICITUD_TRASLADO.ANNO, Me.Anio, Modelo.OTT_SOLICITUD_TRASLADO.ID_SOLICITUD_TRASLADO, Me.IdSolicitudTraslado, Modelo.OTT_SOLICITUD_TRASLADO.ID_UBICACION, Me.IdUbicacion))

            If Me.SolicitudTraslado.Existe Then
                With Me.SolicitudTraslado
                    Me.txtFecha.Text = .FechaRegistroSolicitud
                    Me.rblJornada.SelectedValue = .JornadaRetiro
                    If String.IsNullOrWhiteSpace(.Observaciones) Or .Observaciones.Equals("-") Then
                        Me.txtObservaciones.Text = .Observaciones
                    Else
                        Me.txtObservaciones.Text = .Observaciones
                    End If
                    Me.lblNombreEncargado.Text = .Encargado
                End With
                rblDesicion.SelectedValue = "1"
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
    ''' Método que llena el reapeter de Fondos con los datos del dataset DsFondo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Jeannette Chavarría Rojas</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LlenarRepeaterPedidos(pvc_campoOrden As String)
        Dim vln_MontoTotal As Integer
        Dim vln_Monto As Integer
        Try

            If Me.DsMaterialesInsert.Tables.Count > 0 AndAlso DsMaterialesInsert.Tables(0).Rows.Count > 0 Then
                With Me.rpPedidos
                    .DataSource = Me.DsMaterialesInsert
                    .DataMember = Me.DsMaterialesInsert.Tables(0).TableName
                    .DataBind()
                End With
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de Materiales: {0}", Me.CantidadMateriales)
                If Not String.IsNullOrWhiteSpace(pvc_campoOrden) Then
                    DsMaterialesInsert.Tables(0).DefaultView.Sort = String.Format("{0} {1}", pvc_campoOrden, Ordenamiento.ASCENDENTE)
                End If

                vln_MontoTotal = 0
                For Each vlo_fila In DsMaterialesInsert.Tables(0).Rows
                    vln_Monto = 0
                    vln_Monto = vlo_fila(Modelo.V_OTT_LINEA_TRASLADOLST.COSTO_PROMEDIO) * vlo_fila(Modelo.V_OTT_LINEA_TRASLADOLST.CANTIDAD_REQUERIDA)
                    vln_MontoTotal = vln_MontoTotal + vln_Monto
                Next
                Me.lblMontoTotal.Text = String.Format("Total: ₡{0}", vln_MontoTotal.ToString("N2"))
            Else
                With Me.rpPedidos
                    .DataSource = Nothing
                    .DataBind()
                End With
                'MostrarAlertaNoHayDatos()
                Me.lblCantidadDeRegistros.Visible = False
            End If
            If Me.CantidadMateriales = 0 Then
                Me.txtMaterialValidacion.Text = String.Empty
            Else
                Me.txtMaterialValidacion.Text = Me.CantidadMateriales
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

#Region "Funciones"


    ''' <summary>
    ''' Permite agregar al DsFondos los datos del Fondo Unidad 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Jeannette Chavarría Rojas</author>
    ''' <creationDate>28/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function AgregarDsMateriales(pvn_cantidadRequerida As Double, pvn_cantidadRetirada As Integer, pvc_detalle As String, pvn_costoPromedio As Double, pvn_cantidadDisponible As Double, pvc_descipcion As String) As Boolean
        Dim vlo_DrFila As Data.DataRow
        Dim vlo_fila() As Data.DataRow
        Dim vlc_condicion As String
        Dim vlo_Usuario As New UsuarioActual()

        Try

            vlc_condicion = String.Format("{0}={1}", Modelo.OTT_LINEA_TRASLADO.ID_MATERIAL, Me.txtCodigo.Text)
            vlo_fila = Me.DsMaterialesInsert.Tables(0).Select(vlc_condicion)
            If vlo_fila.Length > 0 Then
                MostrarAlertaError("El material ya existe en la solicitud")
                Return False
            Else
                If Me.DsMaterialesInsert.Tables(0).Rows.Count >= 0 Then
                    vlo_DrFila = Me.DsMaterialesInsert.Tables(0).NewRow
                    vlo_DrFila.Item(Me.DsMaterialesInsert.Tables(0).Columns(Modelo.V_OTT_LINEA_TRASLADOLST.ANNO)) = Year(Now)
                    vlo_DrFila.Item(Me.DsMaterialesInsert.Tables(0).Columns(Modelo.V_OTT_LINEA_TRASLADOLST.ID_UBICACION)) = Me.IdUbicacion
                    vlo_DrFila.Item(Me.DsMaterialesInsert.Tables(0).Columns(Modelo.V_OTT_LINEA_TRASLADOLST.ID_ALMACEN)) = Me.IdBodega
                    vlo_DrFila.Item(Me.DsMaterialesInsert.Tables(0).Columns(Modelo.V_OTT_LINEA_TRASLADOLST.ID_MATERIAL)) = Me.txtCodigo.Text
                    vlo_DrFila.Item(Me.DsMaterialesInsert.Tables(0).Columns(Modelo.V_OTT_LINEA_TRASLADOLST.CANTIDAD_REQUERIDA)) = pvn_cantidadRequerida
                    vlo_DrFila.Item(Me.DsMaterialesInsert.Tables(0).Columns(Modelo.V_OTT_LINEA_TRASLADOLST.CANTIDAD_RETIRADA)) = pvn_cantidadRetirada
                    vlo_DrFila.Item(Me.DsMaterialesInsert.Tables(0).Columns(Modelo.V_OTT_LINEA_TRASLADOLST.DETALLE)) = pvc_detalle
                    vlo_DrFila.Item(Me.DsMaterialesInsert.Tables(0).Columns(Modelo.V_OTT_LINEA_TRASLADOLST.USUARIO)) = Usuario.UserId
                    vlo_DrFila.Item(Me.DsMaterialesInsert.Tables(0).Columns(Modelo.V_OTT_LINEA_TRASLADOLST.TIME_STAMP)) = Date.Now

                    vlo_DrFila.Item(Me.DsMaterialesInsert.Tables(0).Columns(Modelo.V_OTT_LINEA_TRASLADOLST.DESCRIPCION)) = pvc_descipcion
                    vlo_DrFila.Item(Me.DsMaterialesInsert.Tables(0).Columns(Modelo.V_OTT_LINEA_TRASLADOLST.COSTO_PROMEDIO)) = pvn_costoPromedio
                    vlo_DrFila.Item(Me.DsMaterialesInsert.Tables(0).Columns(Modelo.V_OTT_LINEA_TRASLADOLST.CANTIDAD_DISPONIBLE)) = pvn_cantidadDisponible
                    Me.DsMaterialesInsert.Tables(0).Rows.Add(vlo_DrFila)
                End If
                Me.CantidadMateriales += 1
                Return True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function BorrarDetalleMaterial(pvn_IdMaterial As Integer) As Boolean
        Dim vlo_FilaAuxiliar As DataRow

        vlo_FilaAuxiliar = Me.DsMaterialesInsert.Tables(0).Rows.Find(pvn_IdMaterial)

        If Me.DsMaterialesInsert.Tables(0).Rows.Count > 0 Then

            vlo_FilaAuxiliar.Delete()
            Me.CantidadMateriales -= 1
            Return True
        End If

        Return False
    End Function

    ''' <summary>
    ''' Administra el proceso de envio a aprobacion de los materiales
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/06/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Aceptar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOttSolicitudTraslado As Wsr_OT_Catalogos.EntOttSolicitudTraslado
        Dim vlo_EntOtlTrazabilSolTraslado As Wsr_OT_Catalogos.EntOtlTrazabilSolTraslado
        Dim vlb_esAprobar As Boolean

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        If rblDesicion.SelectedValue = "1" Then
            vlb_esAprobar = True
        Else
            vlb_esAprobar = False
        End If

        vlo_EntOttSolicitudTraslado = ConstruirRegistro()
        vlo_EntOtlTrazabilSolTraslado = ConstruirRegistroTrazabilidad()
        Try
            Return vlo_Ws_OT_Catalogos.OTT_SOLICITUD_TRASLADO_ModificarRegistroConDesicion(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               vlo_EntOttSolicitudTraslado, vlo_EntOtlTrazabilSolTraslado, DsMaterialesInsert, vlb_esAprobar) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Funcion encargada de contruir el registro
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_Catalogos.EntOttSolicitudTraslado
        Dim vlo_EntOttSolicitudTraslado As Wsr_OT_Catalogos.EntOttSolicitudTraslado

        vlo_EntOttSolicitudTraslado = Me.SolicitudTraslado

        With vlo_EntOttSolicitudTraslado
            If rblDesicion.SelectedValue = "1" Then
                .EstadoTraslado = EstadoTraslado.APROBADA
            Else
                .EstadoTraslado = EstadoTraslado.DEVUELTA
            End If
            .FechaPropuestaSalida = Date.Now
            .FechaRegistroSolicitud = Date.Now
            .JornadaRetiro = rblJornada.SelectedValue
            .Observaciones = Me.txtObservaciones.Text
        End With

        Return vlo_EntOttSolicitudTraslado
    End Function

    ''' <summary>
    ''' Funcion encargada de contruir el registro
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistroTrazabilidad() As Wsr_OT_Catalogos.EntOtlTrazabilSolTraslado
        Dim vlo_EntOtlTrazabilSolTraslado As Wsr_OT_Catalogos.EntOtlTrazabilSolTraslado
        vlo_EntOtlTrazabilSolTraslado = New EntOtlTrazabilSolTraslado
        With vlo_EntOtlTrazabilSolTraslado
            If rblDesicion.SelectedValue = "1" Then
                .EstadoTraslado = EstadoTraslado.APROBADA
            Else
                .EstadoTraslado = EstadoTraslado.DEVUELTA
            End If
            .IdUbicacion = Me.IdUbicacion
            .Anno = Me.Anio
            .IdSolicitudTraslado = Me.IdSolicitudTraslado
            .Usuario = Usuario.UserName
            .TimeStamp = Date.Now
        End With

        Return vlo_EntOtlTrazabilSolTraslado
    End Function
#End Region

End Class
