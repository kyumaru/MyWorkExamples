Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Imports System.Data


Partial Class Catalogos_Lst_OT_Rubros
    Inherits System.Web.UI.Page
#Region "Propiedades"
    Private Property UltimoSortExpression As String
        Get
            If ViewState("UltimoSortExpression") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortExpression"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortExpression") = value
        End Set
    End Property

    Private Property UltimoSortColumn As String
        Get
            If ViewState("UltimoSortColumn") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortColumn"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortColumn") = value
        End Set
    End Property

    Private Property UltimoSortDireccion As SortDirection
        Get
            If ViewState("UltimoSortDireccion") Is Nothing Then
                Return SortDirection.Ascending
            End If
            Return CType(ViewState("UltimoSortDireccion"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("UltimoSortDireccion") = value
        End Set
    End Property

    ''' <summary>
    ''' Determina cuando se muestra información filtrada, en tal caso no se mostrarán los botones para borrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Public Property FiltroConsulta As Boolean
        Get
            If ViewState("FiltroConsulta") Is Nothing Then
                Return False
            End If
            Return CType(ViewState("FiltroConsulta"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("FiltroConsulta") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el último elemento de la lista
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Public Property UltimoOrden As Integer
        Get
            If ViewState("UltimoOrden") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("UltimoOrden"), Integer)
        End Get
        Set(value As Integer)
            ViewState("UltimoOrden") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para la ubicacion autorizada del usuario que desea registrar ordenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>20/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property AutorizadoUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Get
            Return CType(ViewState("AutorizadoUbicacion"), Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
            ViewState("AutorizadoUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en sesion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>20/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property


#End Region

#Region "Eventos"
    ''' <summary>
    ''' Metodo que se encarga de inicializar el listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)
                If Me.AutorizadoUbicacion.Existe Then
                    inicializarListado()
                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('No se pudo encontrar ninguna sede a su cargo por lo que no está autorizado a realizar acciones en esta pantalla. Para gestionar los permisos necesarios contacte al administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se carga el repeater de subcomponentes
    ''' Esto permite ejecutar correctamente la opción de borrar.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/11/2015</creationDate>
    Protected Sub rpSubcomponentes_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpRubros.ItemDataBound
        Dim vlo_IdBorrar As ImageButton
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_IdBorrar = e.Item.FindControl("ibBorrar")
            If vlo_IdBorrar IsNot Nothing Then
                vlo_IdBorrar.Attributes.Add("data-uniqueid", vlo_IdBorrar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista deacuerdo al orden deseado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Protected Sub lnkRPRubros_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerCondicionDeBusqueda, ObtenerSortExpression(e.CommandName), pnRpRubros.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub


    ''' <summary>
    ''' Altera el orden de el rubro, en este caso sube el rubro clickeado y baja el anterior,
    ''' Eso si no es el primero.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Protected Sub imgSubirNuevo_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_imgSubirNuevo As ImageButton

        Try
            vlo_imgSubirNuevo = CType(sender, ImageButton)
            Dim vln_Orden = vlo_imgSubirNuevo.CommandArgument.ToString().Split(",")

            If vln_Orden(1) <> "1" Then
                SubirRubro(vln_Orden(0))
            End If

            UltimoSortDireccion = SortDirection.Descending
            Buscar(ObtenerCondicionDeBusqueda, String.Format("{0} {1}", Modelo.OTM_RUBRO_DECISION_INICIA.ORDEN, Ordenamiento.ASCENDENTE))


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
    ''' Altera el orden de el rubro, en este caso baja el rubro clickeado y sube el siguiente,
    ''' Eso si no es el último.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Protected Sub imgBajarNuevo_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_imgSubirNuevo As ImageButton

        Try
            vlo_imgSubirNuevo = CType(sender, ImageButton)
            Dim vln_Orden = vlo_imgSubirNuevo.CommandArgument.ToString().Split(",")

            If OrdenValido(CInt(vln_Orden(1))) Then
                BajarRubro(vln_Orden(0))
            End If

            UltimoSortDireccion = SortDirection.Descending
            Buscar(ObtenerCondicionDeBusqueda, String.Format("{0} {1}", Modelo.OTM_RUBRO_DECISION_INICIA.ORDEN, Ordenamiento.ASCENDENTE))

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
    ''' Ejecuta el método para borrar un subcomponente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_IbBorrar As ImageButton
        Dim vln_IdSubcomponente As Integer

        Try
            vlo_IbBorrar = CType(sender, ImageButton)
            If Not String.IsNullOrWhiteSpace(vlo_IbBorrar.CommandArgument) Then
                vln_IdSubcomponente = CInt(vlo_IbBorrar.CommandArgument)
                If Borrar(vln_IdSubcomponente) Then
                    UltimoSortDireccion = SortDirection.Descending
                    Buscar(ObtenerCondicionDeBusqueda, String.Format("{0} {1}", Modelo.OTM_RUBRO_DECISION_INICIA.ORDEN, Ordenamiento.ASCENDENTE))
                    MostrarAlertaRegistroBorrado()
                Else
                    MostrarAlertaRegistroNoBorrado()
                End If

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
    ''' Ejecuta la función para buscar deacuerdo a los filtros seleccionados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim vlc_Condicion As String
        Try
            vlc_Condicion = ObtenerCondicionDeBusqueda()
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                Me.FiltroConsulta = False
                Buscar(vlc_Condicion, String.Empty)
            Else
                Me.FiltroConsulta = True
                Buscar(vlc_Condicion, String.Empty)
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Metodos"
    ''' <summary>
    ''' Muestra la alerta de no hay datos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    ''' <summary>
    ''' indica si el registro fue borrado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Sub MostrarAlertaRegistroBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroBorrado", "mostrarAlertaRegistroBorrado();")
    End Sub

    ''' <summary>
    ''' Indica si el registro no fue borrado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Sub MostrarAlertaRegistroNoBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroNoBorrado", "mostrarAlertaRegistroNoBorrado();")
    End Sub

    ''' <summary>
    ''' Mostrar error generico
    ''' </summary>
    ''' <param name="pvc_Mensaje"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("MostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Inicializa el listado y carga el repeater de rubros
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Sub inicializarListado()
        CargarEstado()
        Me.FiltroConsulta = False
        Buscar(String.Empty, String.Format("{0} {1}", Modelo.OTM_RUBRO_DECISION_INICIA.ORDEN, Ordenamiento.ASCENDENTE))
    End Sub

    ''' <summary>
    ''' Carga la lista de estados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Sub CargarEstado()
        Try
            Me.ddlFiltroEstado.Items.Clear()
            Me.ddlFiltroEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
            Me.ddlFiltroEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
            Me.ddlFiltroEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
        Catch ex As Exception
            Throw
        Finally
        End Try
    End Sub

    ''' <summary>
    ''' Método para cargar la lista de rubros
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet
        Dim vln_cantidad As Integer

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_RUBRO_DECISION_INICIA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden, True, pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                vln_cantidad = vlo_DsDatos.Tables(0).Rows.Count
                CargarUltimoOrden()
                With Me.rpRubros
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            Else
                With Me.rpRubros
                    .DataSource = Nothing
                    .DataBind()
                End With
                MostrarAlertaNoHayDatos()
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
            If vlo_DsDatos IsNot Nothing Then
                vlo_DsDatos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Permite actualizar el listado de rubros
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EndDatosPaginacion As EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerSortExpression(Modelo.OTM_SUBCOMPONENTE.ORDEN)
        End If

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EndDatosPaginacion = vlo_Ws_OT_Catalogos.OTM_RUBRO_DECISION_INICIA_ObtenerDatosPaginacionVOtmRubroDecisionInicia(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EndDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpRubros.TotalPaginasLista = vlo_EndDatosPaginacion.TotalPaginas
                Me.pnRpRubros.Dibujar()
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de Rubros: {0}", vlo_EndDatosPaginacion.TotalRegistros)
                CargarLista(pvc_Condicion, pvc_Orden, 1)
            Else
                Me.lblCantidadDeRegistros.Visible = False
                Me.lblCantidadDeRegistros.Text = String.Empty
                MostrarAlertaNoHayDatos()
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
    ''' Método para subir un rubro
    ''' </summary>
    ''' <param name="pvn_IdRubro"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Sub SubirRubro(pvn_IdRubro As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_Ws_OT_Catalogos.OTM_RUBRO_DECISION_INICIA_EjecutarPrOtSubirRubro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_IdRubro)
        Catch
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Método para bajar el rubro
    ''' </summary>
    ''' <param name="pvn_IdRubro"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Sub BajarRubro(pvn_IdRubro As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_Ws_OT_Catalogos.OTM_RUBRO_DECISION_INICIA_EjecutarPrOtBajarRubro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_IdRubro)
        Catch
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Carga el valor del Último orden para agregar el rubro al final del listado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Sub CargarUltimoOrden()
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.UltimoOrden = vlo_Ws_OT_Catalogos.OTM_RUBRO_DECISION_INICIA_ObtenerFnOtObtenerUltimoOrden(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Permite borrar un rubro de la fuente de datos
    ''' </summary>
    ''' <param name="pvn_IdRubro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Function Borrar(pvn_IdRubro As Integer) As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmRubroDecisionInicia As EntOtmRubroDecisionInicia

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_EntOtmRubroDecisionInicia = New EntOtmRubroDecisionInicia

        Try
            vlo_EntOtmRubroDecisionInicia.IdRubroDecisionInicia = pvn_IdRubro

            Return vlo_Ws_OT_Catalogos.OTM_RUBRO_DECISION_INICIA_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmRubroDecisionInicia) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' Valida que el ultimo rubro de la lista no ejecute un bajar para evitar un error
    ''' </summary>
    ''' <param name="pvn_Orden"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Function OrdenValido(pvn_Orden As Integer) As Boolean
        Return UltimoOrden - 1 <> pvn_Orden
    End Function


    ''' <summary>
    ''' Obtiene la expresion de ordenamiento 
    ''' </summary>
    ''' <param name="pvc_NombreColumna"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Function ObtenerSortExpression(pvc_NombreColumna As String) As String
        If String.IsNullOrWhiteSpace(UltimoSortColumn) OrElse
            pvc_NombreColumna.CompareTo(UltimoSortColumn) <> 0 Then
            UltimoSortColumn = pvc_NombreColumna
            UltimoSortDireccion = SortDirection.Ascending
        Else
            If UltimoSortDireccion = SortDirection.Ascending Then
                UltimoSortDireccion = SortDirection.Descending
            Else
                UltimoSortDireccion = SortDirection.Ascending
            End If
        End If

        UltimoSortExpression = String.Format("{0} {1}", UltimoSortColumn, IIf(UltimoSortDireccion = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))

        Return UltimoSortExpression

    End Function

    ''' <summary>
    ''' Da formato a la condición de búsqueda deacuerdo a los filtros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Function ObtenerCondicionDeBusqueda() As String
        Dim vlc_Condicion As String = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroDescripcion.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor de búsqueda
                vlc_Condicion = String.Format("UPPER({0}) LIKE '%{1}%'",
                                            Modelo.OTM_SUBCOMPONENTE.DESCRIPCION,
                                            Me.txtFiltroDescripcion.Text.Trim.ToUpper)
            Else
                '{0} = Valor original de vlc_Condicion
                '{1} = Nombre de la columna
                '{2} = Valor a buscar
                vlc_Condicion = String.Format("{0} AND UPPER({1}) LIKE '%{2}%'",
                                            vlc_Condicion,
                                            Modelo.OTM_SUBCOMPONENTE.DESCRIPCION,
                                            Me.txtFiltroDescripcion.Text.Trim.ToUpper)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroEstado.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor a buscar
                vlc_Condicion = String.Format("{0} = '{1}'",
                                            Modelo.OTM_SUBCOMPONENTE.ESTADO,
                                            Me.ddlFiltroEstado.SelectedValue)
            Else
                '{0} = Contenido de vlc_Condicion
                '{1} = Nombre de la columna a filtrar
                '{2} = valor de Búsqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'",
                                            vlc_Condicion,
                                            Modelo.OTM_SUBCOMPONENTE.ESTADO,
                                            Me.ddlFiltroEstado.SelectedValue)

            End If
        End If

        Return vlc_Condicion

    End Function

    ''' <summary>
    ''' Carga una entidad de tipo autorizado ubicacion
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>20/04/2016</creationDate>
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
#End Region


End Class
