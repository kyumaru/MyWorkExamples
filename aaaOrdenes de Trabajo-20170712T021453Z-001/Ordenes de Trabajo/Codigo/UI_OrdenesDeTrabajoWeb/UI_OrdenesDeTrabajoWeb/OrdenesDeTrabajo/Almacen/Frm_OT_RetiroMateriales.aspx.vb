Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports System.Data
Imports Wsr_OT_Catalogos
Imports AjaxControlToolkit


Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' Almacena la identificación del sector o taller 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' Almacena la identificación de la solicitud retiro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>7/7/2016</creationDate>
    Private Property IdSolicitudRetiro As Integer
        Get
            If ViewState("IdSolicitudRetiro") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdSolicitudRetiro"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdSolicitudRetiro") = value
        End Set
    End Property

    ''' <summary>
    ''' Es el identificador base de la Orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' <creationDate>1/7/2016</creationDate>
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

    Private Property ContadorListado As Integer
        Get
            If ViewState("ContadorListado") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("ContadorListado"), Integer)
        End Get
        Set(value As Integer)
            ViewState("ContadorListado") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de materiales a ingresar eliminar o modificar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>6/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property OrdenTrabajo As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
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
    ''' <creationDate>1/7/2016</creationDate>
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
    ''' Evento que por cada fila adjunta un identificador único para el textbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>21/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpPedidos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpPedidos.ItemDataBound
        Dim vlo_txtCantidad As TextBox
        Dim vlo_FilteredTextBoxExtender As FilteredTextBoxExtender

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_txtCantidad = e.Item.FindControl("txtCantidad")
            If vlo_txtCantidad IsNot Nothing Then
                vlo_txtCantidad.Attributes.Add("data-uniqueid", vlo_txtCantidad.UniqueID)

                vlo_txtCantidad.Text = CType(CType(vlo_txtCantidad.Attributes("data-CantidadSolicitada"), Double) - CType(vlo_txtCantidad.Attributes("data-CantidadRetirada"), Double), String)



                'vlo_FilteredTextBoxExtender = New FilteredTextBoxExtender
                'vlo_FilteredTextBoxExtender.FilterMode = AjaxControlToolkit.FilterModes.ValidChars
                'vlo_FilteredTextBoxExtender.ValidChars = "."
                'vlo_FilteredTextBoxExtender.FilterType = AjaxControlToolkit.FilterTypes.Numbers
                'vlo_FilteredTextBoxExtender.FilterType = vlo_FilteredTextBoxExtender.FilterType + AjaxControlToolkit.FilterTypes.Custom
                'vlo_FilteredTextBoxExtender.TargetControlID = String.Format("cphContenidoFormulario_cphFormulario_rpPedidos_txtCantidad_{0}", ContadorListado)
                'ContadorListado = ContadorListado + 1

            End If

        End If
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>1/07/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            If ValidarAcceso(Me.txtUsuario.Text.Trim, Me.txtClave.Text.Trim) Then
                TramitarSolicitudMateriales()
            End If
        End If
    End Sub

#End Region

#Region "Metodos"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvc_Mensaje"></param>
    ''' <remarks></remarks>
    ''' <creationDate>1/07/2016</creationDate>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub mostrarAlertaGuardadoExitoso()
        WebUtils.RegistrarScript(Me, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <creationDate>1/07/2016</creationDate>
    Private Sub mostrarAlertSinCantidadDisponible()
        WebUtils.RegistrarScript(Me, "alertaError", "mostrarAlertSinCantidadDisponible();")
    End Sub


    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.DsMaterialesInsert = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_RETIRO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTT_DETALLE_RETIRO.ID_SOLICITUD_RETIRO, Me.IdSolicitudRetiro), String.Empty, False, 0, 0)

            If Me.DsMaterialesInsert IsNot Nothing AndAlso Me.DsMaterialesInsert.Tables(0).Rows.Count > 0 Then
                Me.rpPedidos.DataSource = DsMaterialesInsert
                Me.rpPedidos.DataMember = Me.DsMaterialesInsert.Tables(0).TableName
                Me.rpPedidos.DataBind()
                Me.rpPedidos.Visible = True
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
    ''' <creationDate>1/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Usuario = New UsuarioActual()
        leerParametros()
        CargarLista()
        CargarOrdenTrabajo(Me.IdUbicacion, Me.IdOrdenTrabajo)
        InicializarControlesUsuario()
    End Sub

    ''' <summary>
    ''' Se encarga de leer los parámetros provenientes del listado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub leerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        Me.IdSectorTaller = WebUtils.LeerParametro(Of Integer)("pvn_IdSectorTaller")
        Me.IdSolicitudRetiro = WebUtils.LeerParametro(Of Integer)("pvn_IdSolicitudRetiro")
    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario para mostrar la información general
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarControlesUsuario()

        'Asignación de Datos para generar el web user control de la información general
        ctrl_InfoGeneral.Anno = Me.OrdenTrabajo.Anno
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.IdUbicacion

        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()


    End Sub

    ''' <summary>
    ''' Obtiene los datos ingresados en la lista de materiales solicitados e ingresa las solicitudes de salida del material
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez</author>
    ''' <creationDate>26/03/2017</creationDate>
    ''' <changeLog></changeLog>
    Private Sub TramitarSolicitudMateriales()
        Dim vln_Result As Integer
        Dim vlo_txtCantidad As TextBox
        Dim vln_Cantidad As Double
        Dim vln_CantidadSolicitada As Double
        Dim vln_CantidadRetirada As Double
        Dim vln_idMaterial As Integer
        Dim vln_idDetalleMaterial As Integer
        Dim vln_Anno As Integer
        Dim vln_idSolicitudRetiro As Integer
        Dim vln_idAlmacenBodega As Integer
        Dim vln_idUbicacion As Integer
        Dim vlb_bandera As Boolean = True
        Dim vlo_DsInsertar As Data.DataSet
        'Dim vlo_EntOtfInventario As Wsr_OT_Catalogos.EntOtfInventario

        'Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        'vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        'vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        'vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        ''instanciar y configurar objetos
        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            'vlo_almacen = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ObtenerRegistro(
            '   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            '   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            '   String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} ",
            '                 Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, Me.IdUbicacion,
            '                 Modelo.OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))

            'Hay que recorrer el repeater para obtener el text box y validarlo
            For Each vlo_filarepeater As RepeaterItem In rpPedidos.Items
                vlo_txtCantidad = CType(vlo_filarepeater.FindControl("txtCantidad"), TextBox)

                If Not String.IsNullOrWhiteSpace(vlo_txtCantidad.Text) Then
                    'Se obtiene la cantidad a solicitar y la cantidad disponible en inventario para validar que esté correcto el rebajo que se hará
                    vln_Cantidad = vlo_txtCantidad.Text

                    vln_idMaterial = vlo_txtCantidad.Attributes("data-idMaterial")
                    vln_idAlmacenBodega = vlo_txtCantidad.Attributes("data-idAlmacenBodega")
                    vln_idUbicacion = vlo_txtCantidad.Attributes("data-idUbicacion")
                    vln_CantidadSolicitada = vlo_txtCantidad.Attributes("data-CantidadSolicitada")
                    vln_CantidadRetirada = vlo_txtCantidad.Attributes("data-CantidadRetirada")
                    vln_idDetalleMaterial = vlo_txtCantidad.Attributes("data-idDetalleMaterial")
                    vln_Anno = vlo_txtCantidad.Attributes("data-Anno")
                    vln_idSolicitudRetiro = vlo_txtCantidad.Attributes("data-SolicitudRetiro")

                    If vln_Cantidad > 0 Then

                        

                        'vlo_EntOtfInventario = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                        '          ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        '          ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        '          String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                        '           Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vln_idAlmacenBodega,
                        '           Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vln_idUbicacion,
                        '           Modelo.OTF_INVENTARIO.ID_MATERIAL, vln_idMaterial))

                        If ((vln_CantidadSolicitada - vln_CantidadRetirada) >= vln_Cantidad) And vlo_txtCantidad.Enabled Then

                            'If vlo_EntOtfInventario IsNot Nothing Then

                            'If (vlo_EntOtfInventario.CantidadDisponible) >= vln_Cantidad Then

                            vln_Result = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ProcesarRetiroMaterial(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                IdOrdenTrabajo,
                                vlo_txtCantidad.Attributes("data-idDetalleMaterial"),
                                vln_idAlmacenBodega,
                                vln_idUbicacion,
                                vln_idMaterial,
                                vln_Cantidad,
                                Me.Usuario.UserName,
                                vln_Anno,
                                vln_idSolicitudRetiro)

                            'End If

                            'End If

                        End If

                    End If

                End If

            Next

            vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ActualizaSolicitudRetiroMaterial(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                Me.Usuario.UserName,
                                vln_Anno,
                                vln_idSolicitudRetiro)

            CargarLista()
            mostrarAlertaGuardadoExitoso()
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
        Finally
            If vlo_DsInsertar IsNot Nothing Then
                vlo_DsInsertar.Dispose()
            End If
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
    ''' <creationDate>6/7/2016</creationDate>
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


#End Region

#Region "Funciones"
    ''' <summary>
    ''' Valida el acceso del usuario con el LDAP
    ''' </summary>
    ''' <param name="pvc_Identificacion"></param>
    ''' <param name="pvc_Carne"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>1/7/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ValidarAcceso(pvc_Identificacion As String, pvc_Carne As String) As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmSectorTaller As EntOtmSectorTaller
        Dim vlb_resultado As Boolean

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Membership.ValidateUser(Me.txtUsuario.Text.Trim, Me.txtClave.Text.Trim) Then
                Dim vlo_User As MembershipUser = Membership.GetUser(CType(Me.txtUsuario.Text, Object))
                Dim vlo_UsuarioActual As New UsuarioActual(vlo_User)

                vlo_EntOtmSectorTaller = vlo_Ws_OT_Catalogos.OTM_SECTOR_TALLER_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1} OR {2} = {1}",
                                  Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_COORDINADOR,
                                  vlo_UsuarioActual.NumEmpleado,
                                  Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_SUSTITUTO))

                If Not vlo_EntOtmSectorTaller.Existe Then
                    MostrarAlertaError("El usuario no posee ningun taller o sector asociado.")
                End If

                vlb_resultado = vlo_EntOtmSectorTaller.IdSectorTaller = OrdenTrabajo.IdSectorTaller

                If Not vlb_resultado Then
                    MostrarAlertaError("La orden de trabajo no está asociada a el usuario provisto.")
                End If

                'authCookie.Expires = vlo_AuthTicket.Expiration
                'Response.Cookies.Add(authCookie)

                'Session.Remove(String.Format("{0}_NombreDeUsuario", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DEL_SITIO_WEB)))
                'Session.Add(String.Format("{0}_NombreDeUsuario", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_DEL_SITIO_WEB)), vlo_UsuarioActual.NombreCompleto)

            Else
                Me.blError.Items.Clear()
                MostrarAlertaError("Usuario o Contraseña incorrectos por favor intente de nuevo.")
                vlb_resultado = False
            End If

            Return vlb_resultado
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try

    End Function

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionario(pvn_IdPersonal As String) As WsrEU_Curriculo.EntEmpleados
        Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("ID_PERSONAL = '{0}' OR NUM_EMPLEADO = {0}", pvn_IdPersonal))
        Catch ex As Exception
            Throw
        Finally
            If vlo_WsEU_Curriculo IsNot Nothing Then
                vlo_WsEU_Curriculo.Dispose()
            End If
        End Try
    End Function
#End Region
End Class
