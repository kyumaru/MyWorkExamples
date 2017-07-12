Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Imports System.Data

Partial Class Catalogos_Lst_OT_Materiales
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
    ''' propiedad para la ubicacion autorizada del usuario que desea registrar ordenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/5/2016</creationDate>
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
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/5/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property

    Public Property FiltroBusquedaForm As String
        Get
            Return CType(ViewState("FiltroBusquedaForm"), String)
        End Get
        Set(value As String)
            ViewState("FiltroBusquedaForm") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el maximo id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>30/5/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property MaxId As Integer
        Get
            Return CType(ViewState("MaxId"), Integer)
        End Get
        Set(value As Integer)
            ViewState("MaxId") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Se ejecuta al cargar la página, inicializa los controles necesarios para el funcionamiento adecuado de la pantalla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)
                Dim vlc_CondicionFiltro As String = WebUtils.LeerParametro(Of String)("pvc_FiltroBusquedaForm")

                If Me.AutorizadoUbicacion.Existe Then
                    CargarEstados()
                    '{0}:ID_UBICACION_ADMINISTRA
                    '{1}:id de la ubicacion del usuario actual
                    '{2}:ESTADO
                    '{3}:Solo categorias con estado activo
                    CargarCategorias(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTM_CATEGORIA_MATERIAL.ID_UBICACION_ADMINISTRA, AutorizadoUbicacion.IdUbicacionAdministra,
                                                   Modelo.OTM_CATEGORIA_MATERIAL.ESTADO, Estado.ACTIVO))

                    '' vlc_CondicionFiltro = WebUtils.LeerParametro(Of String)("pvc_FiltroDatos")


                    If vlc_CondicionFiltro Is Nothing Or vlc_CondicionFiltro = String.Empty Then
                        FiltroBusquedaForm = String.Format("{0} = {1}", Modelo.OTM_MATERIAL.ID_UBICACION_ADMINISTRA, AutorizadoUbicacion.IdUbicacionAdministra)
                    Else
                        FiltroBusquedaForm = vlc_CondicionFiltro
                    End If

                    Buscar(FiltroBusquedaForm, String.Empty)
                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('No se pudo encontrar ninguna sede a su cargo por lo que no está autorizado a realizar acciones en esta pantalla. Para gestionar los permisos necesarios contacte al administrador del sistema.','../Genericos/Frm_MenuPrincipal.aspx');")
                End If
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
        Me.pnRpMateriales.Dibujar()
    End Sub

    ''' <summary>
    ''' Evento que por cada fila adjunta un identificador único para borrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/5/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpMateriales_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpMateriales.ItemDataBound
        Dim vlo_IdBorrar As ImageButton
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_IdBorrar = e.Item.FindControl("ibBorrar")
            If vlo_IdBorrar IsNot Nothing Then
                vlo_IdBorrar.Attributes.Add("data-uniqueid", vlo_IdBorrar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Maneja el evento para ordenar la información por columnas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRPMateriales_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerCondicionDeBusqueda, ObtenerSortExpression(e.CommandName), pnRpMateriales.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Maneja el evento de cuando el usuario desea cambiar de página
    ''' </summary>
    ''' <param name="pvn_PaginaSeleccionada"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub pnRpVias_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpMateriales.CambioDePagina
        Try
            CargarLista(ObtenerCondicionDeBusqueda, UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Ejecuta el buscar con los filtros adecuados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Buscar(ObtenerCondicionDeBusqueda, String.Empty)
            ''  Me.Session.Add("pvc_FiltroDatos", ObtenerCondicionDeBusqueda)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Ejecuta el borrar de un area profesional
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_IbBorrar As ImageButton
        Dim vlc_IdMaterial As String

        Try
            vlo_IbBorrar = CType(sender, ImageButton)
            vlc_IdMaterial = vlo_IbBorrar.CommandArgument

            If Borrar(vlc_IdMaterial) Then
                UltimoSortDireccion = SortDirection.Descending
                Buscar(ObtenerCondicionDeBusqueda, String.Empty)
                MostrarAlertaRegistroBorrado()
            Else
                Buscar(ObtenerCondicionDeBusqueda, String.Empty)
                MostrarAlertaRegistroNoBorrado()
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
    ''' Se ejecuta al cambiar el combo de ambitos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>08/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlFiltroCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFiltroCategoria.SelectedIndexChanged
        Try
            '{0}:ID_UBICACION_ADMINISTRA
            '{1}:id de la ubicacion del usuario actual
            '{2}:ESTADO
            '{3}:Solo subcategorias con estado activo
            If Not String.IsNullOrWhiteSpace(ddlFiltroCategoria.SelectedValue) Then
                CargarSubCategorias(String.Format("{0} = {1}", Modelo.OTM_SUBCATEGORIA_CATEGOR.ID_CATEGORIA_MATERIAL, ddlFiltroCategoria.SelectedValue))

                upSubcategoria.Update()

            End If
            WebUtils.RegistrarScript(Me, "visibilidadPaneles", "ocultarAreaDeListado();")

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
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>08/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnLimpiarFiltros_Click(sender As Object, e As EventArgs)
        Me.ddlFiltroCategoria.SelectedIndex = 0
        Me.ddlFiltroSubCategoria.SelectedIndex = 0
        Me.ddlFiltroEstado.SelectedIndex = 0
        FiltroBusquedaForm = String.Empty
        '' Me.Session.Add("pvc_FiltroDatos", String.Empty)
        WebUtils.RegistrarScript(Me, "visibilidadPaneles", "ocultarAreaDeListado();")
    End Sub

#End Region

#Region "Metodos"
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    Private Sub MostrarAlertaRegistroBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroBorrado", "mostrarAlertaRegistroBorrado();")
    End Sub

    Private Sub MostrarAlertaRegistroNoBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroNoBorrado", "mostrarAlertaRegistroNoBorrado();")
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Carga la lista de estados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEstados()
        Try
            Me.ddlFiltroEstado.Items.Clear()
            Me.ddlFiltroEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
            Me.ddlFiltroEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
            Me.ddlFiltroEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
        Catch ex As Exception
            Throw
        Finally
        End Try
    End Sub

    ''' <summary>
    ''' Carga el repeater de datos con lo obtenido desde la base
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden, True, pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                MaxId = vlo_DsDatos.Tables(0).Rows(vlo_DsDatos.Tables(0).Rows.Count - 1).Item(Modelo.OTM_MATERIAL.ID_MATERIAL)
                '' Me.Session.Add("pvn_MaxId", MaxId + 1)
                With Me.rpMateriales
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            Else
                ''Me.Session.Add("pvn_MaxId", 1)
                With Me.rpMateriales
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
    ''' Ejecuta la acción para listar materiales
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EndDatosPaginacion As EntDatosPaginacion

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If pvc_Condicion = String.Empty Then
                pvc_Condicion = String.Format("{0} = {1}", Modelo.OTM_MATERIAL.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra)
            End If

            If pvc_Orden = String.Empty Then
                pvc_Orden = String.Format("{0} ASC", Modelo.OTM_MATERIAL.ID_MATERIAL)
            End If

            vlo_EndDatosPaginacion = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ObtenerDatosPaginacionVOtmMateriallst(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EndDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpMateriales.TotalPaginasLista = vlo_EndDatosPaginacion.TotalPaginas
                Me.pnRpMateriales.Dibujar()
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de materiales: {0}", vlo_EndDatosPaginacion.TotalRegistros)
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
    ''' Carga las Subcategorias deacuerdo a la condición indicada
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>08/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarSubCategorias(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_dsDatos As Data.DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlFiltroSubCategoria.Items.Clear()
            Me.ddlFiltroSubCategoria.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_dsDatos = vlo_Ws_OT_Catalogos.OTM_SUBCATEGORIA_CATEGOR_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, String.Empty, False, 0, 0)

            With Me.ddlFiltroSubCategoria
                .DataSource = vlo_dsDatos
                .DataMember = vlo_dsDatos.Tables(0).TableName
                .DataTextField = Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.DESCRIPCION_SUBCATEG_MATE
                .DataValueField = Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.ID_SUBCATEGORIA_MATERIAL
                .DataBind()
            End With

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
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarCategorias(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_dsDatos As Data.DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlFiltroCategoria.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_dsDatos = vlo_Ws_OT_Catalogos.OTM_CATEGORIA_MATERIAL_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, String.Empty, False, 0, 0)

            With Me.ddlFiltroCategoria
                .DataSource = vlo_dsDatos
                .DataMember = vlo_dsDatos.Tables(0).TableName
                .DataTextField = Modelo.OTM_CATEGORIA_MATERIAL.DESCRIPCION
                .DataValueField = Modelo.OTM_CATEGORIA_MATERIAL.ID_CATEGORIA_MATERIAL
                .DataBind()
            End With

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
    ''' Ejecuta el borrar de una via de contrato específica
    ''' </summary>
    ''' <param name="pvc_IdMaterial"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Borrar(pvc_IdMaterial As String) As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmMaterial As EntOtmMaterial

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_EntOtmMaterial = New EntOtmMaterial

        Try
            vlo_EntOtmMaterial.IdMaterial = pvc_IdMaterial
            vlo_EntOtmMaterial.IdUbicacionAdministra = AutorizadoUbicacion.IdUbicacionAdministra

            Return vlo_Ws_OT_Catalogos.OTM_MATERIAL_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmMaterial) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' Funcion que retorna la condicion de busqueda actual
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionDeBusqueda() As String
        Dim vlc_Condicion As String = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroDescripcion.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor de búsqueda
                vlc_Condicion = String.Format("UPPER({0}) LIKE '%{1}%'",
                                            Modelo.OTM_MATERIAL.DESCRIPCION,
                                            Me.txtFiltroDescripcion.Text.Trim.ToUpper)
            Else
                '{0} = Valor original de vlc_Condicion
                '{1} = Nombre de la columna
                '{2} = Valor a buscar
                vlc_Condicion = String.Format("{0} AND UPPER({1}) LIKE '%{2}%'",
                                            vlc_Condicion,
                                            Modelo.OTM_MATERIAL.DESCRIPCION,
                                            Me.txtFiltroDescripcion.Text.Trim.ToUpper)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroEstado.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor a buscar
                vlc_Condicion = String.Format("{0} = '{1}'",
                                            Modelo.OTM_MATERIAL.ESTADO,
                                            Me.ddlFiltroEstado.SelectedValue)
            Else
                '{0} = Contenido de vlc_Condicion
                '{1} = Nombre de la columna a filtrar
                '{2} = valor de Búsqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'",
                                            vlc_Condicion,
                                            Modelo.OTM_MATERIAL.ESTADO,
                                            Me.ddlFiltroEstado.SelectedValue)

            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroCategoria.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor a buscar
                vlc_Condicion = String.Format("{0} = '{1}'",
                                            Modelo.OTM_MATERIAL.ID_CATEGORIA_MATERIAL,
                                            Me.ddlFiltroCategoria.SelectedValue)
            Else
                '{0} = Contenido de vlc_Condicion
                '{1} = Nombre de la columna a filtrar
                '{2} = valor de Búsqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'",
                                            vlc_Condicion,
                                            Modelo.OTM_MATERIAL.ID_CATEGORIA_MATERIAL,
                                            Me.ddlFiltroCategoria.SelectedValue)

            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroSubCategoria.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor a buscar
                vlc_Condicion = String.Format("{0} = '{1}'",
                                            Modelo.OTM_MATERIAL.ID_SUBCATEGORIA_MATERIAL,
                                            Me.ddlFiltroSubCategoria.SelectedValue)
            Else
                '{0} = Contenido de vlc_Condicion
                '{1} = Nombre de la columna a filtrar
                '{2} = valor de Búsqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'",
                                            vlc_Condicion,
                                            Modelo.OTM_MATERIAL.ID_SUBCATEGORIA_MATERIAL,
                                            Me.ddlFiltroSubCategoria.SelectedValue)

            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroCodigo.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor a buscar
                vlc_Condicion = String.Format("{0} = '{1}'",
                                            Modelo.OTM_MATERIAL.ID_MATERIAL,
                                            Me.txtFiltroCodigo.Text)
            Else
                '{0} = Contenido de vlc_Condicion
                '{1} = Nombre de la columna a filtrar
                '{2} = valor de Búsqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'",
                                            vlc_Condicion,
                                            Modelo.OTM_MATERIAL.ID_MATERIAL,
                                            Me.txtFiltroCodigo.Text)

            End If
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} = {1}", Modelo.OTM_MATERIAL.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra)
        Else
            vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.OTM_MATERIAL.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra)
        End If


        FiltroBusquedaForm = vlc_Condicion
        Return vlc_Condicion

    End Function

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
    ''' Carga una entidad de tipo autorizado ubicacion
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
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

