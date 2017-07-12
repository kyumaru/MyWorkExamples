Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports System.Data

Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_AjusteInvIndividual
    Inherits System.Web.UI.Page
#Region "Propiedades"
   
    Public Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property

    Public Property IdUbicacion As Integer
        Get
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    Public Property ConsecutivoAjuste As Integer
        Get
            Return CType(ViewState("ConsecutivoAjuste"), Integer)
        End Get
        Set(value As Integer)
            ViewState("ConsecutivoAjuste") = value
        End Set
    End Property

    Private Property Anio As Integer
        Get
            Return CType(ViewState("Anio"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Anio") = value
        End Set
    End Property

    Public Property IdMaterial As Integer
        Get
            Return CType(ViewState("IdMaterial"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdMaterial") = value
        End Set
    End Property

    Private Property EntOtfInventario As Wsr_OT_Catalogos.EntOtfInventario
        Get
            Return CType(ViewState("EntOtfInventario"), Wsr_OT_Catalogos.EntOtfInventario)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtfInventario)
            ViewState("EntOtfInventario") = value
        End Set
    End Property

    Private Property EntOttAjusteInventario As EntOttAjusteInventario
        Get
            Return CType(ViewState("EntOttAjusteInventario"), EntOttAjusteInventario)
        End Get
        Set(value As EntOttAjusteInventario)
            ViewState("EntOttAjusteInventario") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>26/01/2017</creationDate>
    ''' <changeLog></changeLog>
    Private Property Estado As String
        Get
            Return CType(ViewState("Estado"), String)
        End Get
        Set(value As String)
            ViewState("Estado") = value
        End Set
    End Property

    Private Property AutorizadoUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Get
            Return CType(ViewState("AutorizadoUbicacion"), Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
            ViewState("AutorizadoUbicacion") = value
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
    ''' <creationDate>26/01/2017</creationDate>
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
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>26/01/2017</creationDate>
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
    ''' Metodo para seleccionar el material del filtro de busqueda
    ''' </summary>
    ''' <param name="pvc_IdMaterial"></param>
    ''' <param name="pvc_Descripcion"></param>
    ''' <param name="pvn_IdCategoria"></param>
    ''' <param name="pvn_idSubcategoria"></param>
    ''' <param name="pvn_CostoPromedio"></param>
    ''' <param name="pvn_UnidadMedida"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ctrl_Materiales_Aceptar(pvc_IdMaterial As Integer, pvc_Descripcion As String, pvn_IdCategoria As Integer, pvn_idSubcategoria As Integer, pvn_CostoPromedio As Integer, pvn_UnidadMedida As Integer) Handles ctrl_Materiales.Aceptar
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlc_Condicion As String
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try


            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3}", Modelo.V_OTM_MATERIALLST.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra, Modelo.V_OTM_MATERIALLST.ID_MATERIAL, pvc_IdMaterial)

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlc_Condicion,
                String.Empty,
                False,
                0,
                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                Me.lblDescripcion.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.DESCRIPCION)
                Me.lblCategoria.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.DESC_CATEGORIA)
                Me.lblSubCategoria.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.DESC_SUBCATEGORIA)
                Me.lblUnidadMedida.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.DESC_UNIDAD_MEDIDA)
                Me.lblExistencia.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.CANTIDAD_EXISTENCIA)
                Me.lblDisponible.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.CANTIDAD_DISPONIBLE)
                Me.lblReservado.Text = CType(vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.CANTIDAD_EXISTENCIA), Integer) - CType(vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.CANTIDAD_DISPONIBLE), Integer)
                Me.txtCodigo.Text = pvc_IdMaterial.ToString
            End If

            arDatosMat.Visible = True
            'arAjuste.Visible = True
            arDatosAjuste.Visible = True
            upTxtCodigo.Update()
            upControlDatosMaterial.Update()
            WebUtils.RegistrarScript(Me.Page, "InicializarFormulario", "javascript:inicializarFormulario();ocultarFiltroMateriales();")
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
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub txtCodigo_TextChanged(sender As Object, e As EventArgs) Handles txtCodigo.TextChanged
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlc_Condicion As String
        Dim vlo_DsDatos As DataSet

        Try
            'instanciar y configurar objetos
            vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
            vlo_Ws_OT_Catalogos.Timeout = -1
            vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials



            If Me.txtCodigo.Text <> "" Then
                vlc_Condicion = String.Format("{0} = {1} AND {2} = {3}", Modelo.V_OTM_MATERIALLST.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra, Modelo.V_OTM_MATERIALLST.ID_MATERIAL, Me.txtCodigo.Text)

                vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ListarRegistrosLista(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlc_Condicion,
                    String.Empty,
                    False,
                    0,
                    0)
                If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    Me.lblDescripcion.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.DESCRIPCION)
                    Me.lblCategoria.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.DESC_CATEGORIA)
                    Me.lblSubCategoria.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.DESC_SUBCATEGORIA)
                    Me.lblUnidadMedida.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.DESC_UNIDAD_MEDIDA)
                    Me.lblExistencia.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.CANTIDAD_EXISTENCIA)
                    Me.lblDisponible.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.CANTIDAD_DISPONIBLE)
                    Me.lblReservado.Text = CType(vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.CANTIDAD_EXISTENCIA), Integer) - CType(vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.CANTIDAD_DISPONIBLE), Integer)
                    arDatosMat.Visible = True
                    'arAjuste.Visible = True
                    arDatosAjuste.Visible = True
                Else
                    Me.lblDescripcion.Text = String.Empty
                    Me.lblCategoria.Text = String.Empty
                    Me.lblSubCategoria.Text = String.Empty
                    Me.lblUnidadMedida.Text = String.Empty
                    Me.lblExistencia.Text = String.Empty
                    Me.lblDisponible.Text = String.Empty
                    Me.lblReservado.Text = String.Empty
                    Me.txtCodigo.Text = String.Empty
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoHayDatos();")
                    arDatosMat.Visible = False
                    'arAjuste.Visible = False
                    arDatosAjuste.Visible = False
                End If

            Else
                Me.lblDescripcion.Text = String.Empty
                Me.lblCategoria.Text = String.Empty
                Me.lblSubCategoria.Text = String.Empty
                Me.lblUnidadMedida.Text = String.Empty
                Me.lblExistencia.Text = String.Empty
                Me.lblDisponible.Text = String.Empty
                Me.lblReservado.Text = String.Empty
                Me.txtCodigo.Text = String.Empty
                arDatosMat.Visible = False
                'arAjuste.Visible = False
                arDatosAjuste.Visible = False
            End If
            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub ddlBodegaAlmacen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBodegaAlmacen.SelectedIndexChanged
        Try
            CalcularAjuste()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub txtCantidad_TextChanged(sender As Object, e As EventArgs) Handles txtCantidad.TextChanged
        Try
            CalcularAjuste()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub rblTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblTipo.SelectedIndexChanged
        Try
            CalcularAjuste()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Page.IsValid Then
            Try
                If Me.Operacion = eOperacion.Agregar Then
                    If CrearAjusteInventario(False) Then
                        WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaGuardarExitoso();")
                    Else
                        MostrarAlertaError("No ha sido posible registrar el ajuste de inventario")
                    End If
                ElseIf Me.Operacion = eOperacion.Modificar Then
                    If ModificarAjusteInventario(False) Then
                        WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaGuardarExitoso();")
                    Else
                        MostrarAlertaError("No ha sido posible actualizar el ajuste de inventario")
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

    Protected Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click
        If Page.IsValid Then
            Try
                If Me.Operacion = eOperacion.Agregar Then
                    If CrearAjusteInventario(True) Then
                        WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaGuardarExitoso();")
                    Else
                        MostrarAlertaError("No ha sido posible registrar el ajuste de inventario")
                    End If
                ElseIf Me.Operacion = eOperacion.Modificar Then
                    If ModificarAjusteInventario(True) Then
                        WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaGuardarExitoso();")
                    Else
                        MostrarAlertaError("No ha sido posible actualizar el ajuste de inventario")
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
#End Region

#Region "Métodos"

    Private Sub mostrarAlertSinCantidadDisponible()
        WebUtils.RegistrarScript(Me, "alertaError", "mostrarAlertSinCantidadDisponible();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>26/01/2017</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Metodo para cargar el combo de almacenes y bodegas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarComboAlmacenesBodegas()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_dsDatos As Data.DataSet
        Dim vlc_Condicion As String

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials


        Try
            vlc_Condicion = String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, AutorizadoUbicacion.IdUbicacionAdministra,
                                              Modelo.OTM_ALMACEN_BODEGA.ESTADO, Utilerias.OrdenesDeTrabajo.Estado.ACTIVO)

            Me.ddlBodegaAlmacen.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_dsDatos = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlc_Condicion, String.Empty, False, 0, 0)

            For Each vlo_fila As Data.DataRow In vlo_dsDatos.Tables(0).Rows
                Me.ddlBodegaAlmacen.Items.Add(New ListItem(vlo_fila(Modelo.OTM_ALMACEN_BODEGA.DESCRIPCION), vlo_fila(Modelo.OTM_ALMACEN_BODEGA.ID_ALMACEN_BODEGA)))
            Next


        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar los diferentess componentes del formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>26/01/2017</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlc_Condicion As String
        Dim vlo_OttDetalleAjuste As EntOttDetalleAjuste
        Dim vlo_DsDatos As DataSet
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
            Me.Usuario = New UsuarioActual
            Me.ctrl_Materiales.mostrarAlmacenPartida = False
            Me.ctrl_Materiales.Inicializar()
            arDatosMat.Visible = False
            'arAjuste.Visible = False
            arDatosAjuste.Visible = False
            arTituloDetalle.Visible = False
            arDetalle.Visible = False

            Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

            If AutorizadoUbicacion.Existe Then
                CargarComboAlmacenesBodegas()

                Select Case Me.Operacion
                    Case Is = eOperacion.Modificar
                        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
                        Me.Anio = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
                        Me.ConsecutivoAjuste = WebUtils.LeerParametro(Of Integer)("pvn_ConsecutivoAjuste")

                        Me.ddlBodegaAlmacen.Enabled = False

                        'Se obtiene el ajuste de inventario
                        vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTT_AJUSTE_INVENTARIO.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_AJUSTE_INVENTARIO.ANNO, Me.Anio, Modelo.OTT_AJUSTE_INVENTARIO.CONSECUTIVO_AJUSTE, Me.ConsecutivoAjuste)

                        Me.EntOttAjusteInventario = vlo_Ws_OT_OrdenesDeTrabajo.OTT_AJUSTE_INVENTARIO_ObtenerRegistro(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                                    vlc_Condicion)
                        'Se obtiene el detalle del ajuste
                        vlo_OttDetalleAjuste = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_AJUSTE_ObtenerRegistro(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                                    vlc_Condicion)

                        Me.txtObs.Text = Me.EntOttAjusteInventario.Observaciones
                        Me.txtCantidad.Text = vlo_OttDetalleAjuste.Cantidad
                        Me.rblTipo.SelectedValue = vlo_OttDetalleAjuste.DireccionAjuste
                        Me.txtCodigo.Text = vlo_OttDetalleAjuste.IdMaterial
                        Me.ddlBodegaAlmacen.SelectedValue = EntOttAjusteInventario.IdAlmacenBodega


                        If Not Me.EntOttAjusteInventario.EstadoAjuste = EstadoAjuste.CREADO And Not Me.EntOttAjusteInventario.EstadoAjuste = EstadoAjuste.DEVUELTO_SUPERVISOR Then
                            WebUtils.RegistrarScript(Me.Page, "DeshabilitarFormulario", "javascript:deshabilitarFormulario();")
                        End If

                        'Se carga la informacion del material de inventario
                        vlc_Condicion = String.Format("{0} = {1} AND {2} = {3}", Modelo.V_OTM_MATERIALLST.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra, Modelo.V_OTM_MATERIALLST.ID_MATERIAL, Me.txtCodigo.Text)

                        vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ListarRegistrosLista(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                            vlc_Condicion,
                            String.Empty,
                            False,
                            0,
                            0)
                        If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                            Me.lblDescripcion.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.DESCRIPCION)
                            Me.lblCategoria.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.DESC_CATEGORIA)
                            Me.lblSubCategoria.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.DESC_SUBCATEGORIA)
                            Me.lblUnidadMedida.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.DESC_UNIDAD_MEDIDA)
                            Me.lblExistencia.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.CANTIDAD_EXISTENCIA)
                            Me.lblDisponible.Text = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.CANTIDAD_DISPONIBLE)
                            Me.lblReservado.Text = CType(vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.CANTIDAD_EXISTENCIA), Integer) - CType(vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_MATERIALLST.CANTIDAD_DISPONIBLE), Integer)
                            arDatosMat.Visible = True
                            'arAjuste.Visible = True
                            arDatosAjuste.Visible = True
                        Else
                            Me.lblDescripcion.Text = String.Empty
                            Me.lblCategoria.Text = String.Empty
                            Me.lblSubCategoria.Text = String.Empty
                            Me.lblUnidadMedida.Text = String.Empty
                            Me.lblExistencia.Text = String.Empty
                            Me.lblDisponible.Text = String.Empty
                            Me.lblReservado.Text = String.Empty
                            Me.txtCodigo.Text = String.Empty
                            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoHayDatos();")
                            arDatosMat.Visible = False
                            'arAjuste.Visible = False
                            arDatosAjuste.Visible = False
                        End If

                        'Se procede a calcular el ajuste segun la informacion que se guardo en base de datos
                        CalcularAjuste()
                End Select
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ninguna ubicación autorizada, contactar con el administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
            End If


        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para calcular como quedaria el inventario al efectuar el ajuste
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalcularAjuste()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlc_Condicion As String

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.ddlBodegaAlmacen.SelectedValue <> String.Empty AndAlso Me.txtCodigo.Text <> String.Empty AndAlso Me.rblTipo.SelectedValue <> String.Empty AndAlso Me.txtCantidad.Text <> String.Empty Then
                arTituloDetalle.Visible = True
                arDetalle.Visible = True

                vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra, Modelo.OTF_INVENTARIO.ID_MATERIAL, Me.txtCodigo.Text, Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, Me.ddlBodegaAlmacen.SelectedValue)

                Me.EntOtfInventario = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlc_Condicion)

                If Me.rblTipo.SelectedValue = "INC" Then
                    Me.lblExistenciaPost.Text = Me.EntOtfInventario.CantidadDisponible + Me.txtCantidad.Text
                    Me.lblReservadoPost.Text = Me.EntOtfInventario.CantidadReservada
                    Me.lblDisponiblePost.Text = Me.lblExistenciaPost.Text - Me.lblReservadoPost.Text
                Else
                    Me.lblExistenciaPost.Text = Me.EntOtfInventario.CantidadDisponible - Me.txtCantidad.Text
                    Me.lblReservadoPost.Text = Me.EntOtfInventario.CantidadReservada
                    Me.lblDisponiblePost.Text = Me.lblExistenciaPost.Text - Me.lblReservadoPost.Text
                End If
            Else
                arTituloDetalle.Visible = False
                arDetalle.Visible = False
                Me.lblExistenciaPost.Text = String.Empty
                Me.lblDisponiblePost.Text = String.Empty
                Me.lblReservadoPost.Text = String.Empty
                Me.EntOtfInventario = Nothing
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Funciones"
    Private Function CargarAutorizadoUbicacion(pvn_NumEmpleado As Integer) As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Dim Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        Ws_OT_Catalogos.Timeout = -1
        Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return Ws_OT_Catalogos.OTM_AUTORIZADO_UBICACION_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTM_AUTORIZADO_UBICACION.NUM_EMPLEADO, pvn_NumEmpleado))
        Catch ex As Exception
            Throw
        Finally
            If Ws_OT_Catalogos IsNot Nothing Then
                Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Administra el proceso de modificar un ajuste individual
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>26/01/2017</creationDate>
    ''' <changeLog></changeLog>
    Private Function ModificarAjusteInventario(pvb_Finalizar As Boolean) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_AJUSTE_INVENTARIO_ModificarAjusteIndividual(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               Me.EntOttAjusteInventario, Me.txtCantidad.Text, Me.txtObs.Text, Me.rblTipo.SelectedValue, Usuario.UserName, pvb_Finalizar) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Administra el proceso de crear un ajuste de inventario
    ''' </summary>
    ''' <param name="pvb_Finalizar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CrearAjusteInventario(pvb_Finalizar As Boolean) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_AJUSTE_INVENTARIO_CrearAjusteIndividual(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               Me.AutorizadoUbicacion.IdUbicacionAdministra, Me.ddlBodegaAlmacen.SelectedValue, Me.txtCodigo.Text, Me.txtCantidad.Text, Me.txtObs.Text, Me.rblTipo.SelectedValue, Usuario.UserName, pvb_Finalizar) > 0

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
