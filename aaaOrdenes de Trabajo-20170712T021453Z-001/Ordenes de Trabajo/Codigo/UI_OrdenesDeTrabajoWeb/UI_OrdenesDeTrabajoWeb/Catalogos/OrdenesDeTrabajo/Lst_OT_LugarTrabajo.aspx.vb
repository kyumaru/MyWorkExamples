Imports System.Data  'para utilizar data set
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos
Imports WsrEU_Curriculo
Partial Class Catalogos_Lst_OT_LugarTrabajo
    Inherits System.Web.UI.Page
#Region "Propiedades"

    ''' <summary>
    ''' Ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortExpression As String
        Get
            If ViewState("UltimoSortExpression") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortExpression"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortExpression") = value
        End Set
    End Property

    ''' <summary>
    ''' Ultima columna de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortColumn As String
        Get
            If ViewState("UltimoSortColumn") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortColumn"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortColumn") = value
        End Set
    End Property

    ''' <summary>
    ''' Ultima direccion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortDirection As SortDirection
        Get
            If ViewState("UltimoSortDirection") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return SortDirection.Ascending
            End If
            Return CType(ViewState("UltimoSortDirection"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("UltimoSortDirection") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en sesion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en sesion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property EntEmpleados As EntEmpleados
        Get
            Return CType(ViewState("EntEmpleados"), EntEmpleados)
        End Get
        Set(value As EntEmpleados)
            ViewState("EntEmpleados") = value
        End Set
    End Property

    ''' <summary>
    ''' Condicion de busqueda para el listado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property Condicion As String
        Get
            Return CType(ViewState("Condicion"), String)
        End Get
        Set(value As String)
            ViewState("Condicion") = value
        End Set
    End Property

    ''' <summary>
    ''' Condicion de busqueda para el filtro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UnidadAdministra As Integer
        Get
            Return CType(ViewState("UnidadAdministra"), Integer)
        End Get
        Set(value As Integer)
            ViewState("UnidadAdministra") = value
        End Set
    End Property
#End Region

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim vlo_DsSedeCargo As System.Data.DataSet

        If Not IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.EntEmpleados = CargarFuncionario(Me.Usuario.UserName)
                vlo_DsSedeCargo = VerificaSedes(Me.EntEmpleados.NUM_EMPLEADO)

                If vlo_DsSedeCargo IsNot Nothing AndAlso vlo_DsSedeCargo.Tables(0).Rows.Count > 0 Then
                    Me.Condicion = String.Format("ID_UBICACION_ADMINISTRA = {0}", vlo_DsSedeCargo.Tables(0).Rows(0).Item(Modelo.V_OTM_AUTORIZADO_UBICACION.ID_UBICACION_ADMINISTRA))
                    Me.UnidadAdministra = vlo_DsSedeCargo.Tables(0).Rows(0).Item(Modelo.V_OTM_AUTORIZADO_UBICACION.ID_UBICACION_ADMINISTRA)
                    Me.Session.Add("pvo_UnidadAdministra", vlo_DsSedeCargo.Tables(0).Rows(0).Item(Modelo.V_OTM_AUTORIZADO_UBICACION.ID_UBICACION_ADMINISTRA))
                    CargarEstadoLugar()
                    CargarUnidad()
                    CargarUbicacion()
                    CargarClasificacion()
                    Buscar(Me.Condicion, String.Empty)
                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "mensajePopup('No se pudo encontrar ninguna sede a su cargo por lo que no está autorizado a realizar acciones en esta pantalla. Para gestionar los permisos necesarios contacte al administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If

            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If

        Me.pnRpLugares.Dibujar()
    End Sub

    Protected Sub rpLugares_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpLugares.ItemDataBound
        Dim vlo_IbBorrar As ImageButton

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_IbBorrar = e.Item.FindControl("ibBorrar")
            If vlo_IbBorrar IsNot Nothing Then
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub

    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            If Borrar(CType(sender, ImageButton).CommandArgument) Then
                Buscar(ObtenerCondicionDeBusqueda, UltimoSortExpression)
                MostrarAlertaRegistroBorrado()

            Else
                MostrarAlertaRegistroNoBorrado()
            End If
        Catch ex As Exception
            If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                Dim vlo_OrdenesDeTrabajoException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                MostrarAlertaError(vlo_OrdenesDeTrabajoException.Message)
                WebUtils.RegistrarScript(Me, "OcultarAreaFiltrosDeBusqueda", "ocultarAreaFiltrosDeBusqueda();")

            Else
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End If

        End Try
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Buscar(ObtenerCondicionDeBusqueda, String.Empty)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub pnRpLugares_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpLugares.CambioDePagina   'REVISAR CONDICION
        Try
            CargarLista(Me.Condicion, UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub lnkRpLugares_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(Me.Condicion, ObtenerSortExpression(e.CommandName), pnRpLugares.PaginaActualLista)    'REVISAR CONDICION
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
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

    Private Sub CargarEstadoLugar()

        Me.ddlFiltroEstado.Items.Clear()
        Me.ddlFiltroEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
        Me.ddlFiltroEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlFiltroEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))

    End Sub

    Private Sub CargarClasificacion()

        Me.ddlFiltroClasificacion.Items.Clear()
        Me.ddlFiltroClasificacion.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODAS, String.Empty))
        Me.ddlFiltroClasificacion.Items.Add(New ListItem("Edificio", Constantes.CLASIFICACION_EDI))
        Me.ddlFiltroClasificacion.Items.Add(New ListItem("Sitio", Constantes.CLASIFICACION_SIT))

    End Sub

    Private Sub CargarUbicacion()
        Dim vlo_DsUbicacion As System.Data.DataSet
        Dim vlo_Wsr_OT_Catalogos As Ws_OT_Catalogos
        Dim pvc_Condicion As String

        Try
            vlo_Wsr_OT_Catalogos = New Ws_OT_Catalogos
            vlo_Wsr_OT_Catalogos.Timeout = -1
            vlo_Wsr_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
            'vlo_Wsr_TC_Catalogos.Url = ConfigurationManager.AppSettings(<ConstanteInterna.MI_SERVICIO_WEB>)

            'pvc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTM_UBICACION.TIPO_AREA, Constantes.TIPO_AREA_TAL)

            vlo_DsUbicacion = vlo_Wsr_OT_Catalogos.OTM_UBICACION_ListarRegistrosLista(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            String.Empty,
            String.Empty,
            False,
            0,
            0)
            'primer string.empty es la condicion
            Me.ddlFiltroUbicacion.Items.Clear()
            Me.ddlFiltroUbicacion.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODAS, String.Empty))

            If vlo_DsUbicacion.Tables(0) IsNot Nothing AndAlso vlo_DsUbicacion.Tables(0).Rows.Count > 0 Then
                With Me.ddlFiltroUbicacion
                    .DataSource = vlo_DsUbicacion
                    .DataMember = vlo_DsUbicacion.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_UBICACION.DESCRIPCION
                    .DataValueField = Modelo.V_OTM_UBICACION.ID_UBICACION
                    .DataBind()
                End With
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_DsUbicacion IsNot Nothing Then
                vlo_DsUbicacion.Dispose()

            End If

            If vlo_Wsr_OT_Catalogos IsNot Nothing Then
                vlo_Wsr_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarUnidad()
        Dim vlo_DsUnidades As New Data.DataSet

        Try
            'Limpia el combo de beneficiarios
            Me.ddlFiltroUnidadAdministra.DataSource = Nothing
            Me.ddlFiltroUnidadAdministra.Items.Clear()
            Me.ddlFiltroUnidadAdministra.DataBind()

            FuncionesUtils.CargarUbicaciones(DateTime.Now, DateTime.Now, -1, 1, vlo_DsUnidades)

            'Verifica que el data set contenga tablas
            If vlo_DsUnidades.Tables.Count > 0 AndAlso vlo_DsUnidades.Tables(0).Rows.Count > 0 Then
                With Me.ddlFiltroUnidadAdministra
                    .Items.Add(New ListItem(Constantes.FORMATO_DDL_TODAS, String.Empty))
                    .DataSource = vlo_DsUnidades
                    .DataMember = vlo_DsUnidades.Tables(0).TableName
                    .DataTextField = "COD_DESC"
                    .DataValueField = "CODIGO_UBICA"
                    .DataBind()
                End With
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' Carga la lista del catalogo
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_LUGAR_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpLugares
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()

                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            Else
                With Me.rpLugares
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
    ''' Metodo para buscar con una condicion enviada por parametro
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntDatosPaginacion As EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerSortExpression(Modelo.V_OTM_LUGAR_TRABAJOLST.NOMBRE) 'lo pide el programa ordenar x isbn
        End If

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntDatosPaginacion = vlo_Ws_OT_Catalogos.OTM_LUGAR_TRABAJO_ObtenerDatosPaginacionVOtmLugarTrabajolst(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer)
                )

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpLugares.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                Me.pnRpLugares.Dibujar()
                lblCantidadRegistro.Visible = True
                lblCantidadRegistro.Text = String.Format("Cantidad de lugares de trabajo: {0}", vlo_EntDatosPaginacion.TotalRegistros)
                CargarLista(pvc_Condicion, pvc_Orden, 1) '1 xq la lista siempre carga en esa posicion
            Else
                Me.lblCantidadRegistro.Visible = False
                Me.lblCantidadRegistro.Text = String.Empty
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
#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion para eliminar un registro
    ''' </summary>
    ''' <param name="pvc_IdLugarTrabajo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Borrar(pvc_IdLugarTrabajo As String) As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmLugarTrabajo As EntOtmLugarTrabajo

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_EntOtmLugarTrabajo = New EntOtmLugarTrabajo
        Try
            vlo_EntOtmLugarTrabajo.IdLugarTrabajo = pvc_IdLugarTrabajo
            'vlo_EntTcmLibro.Usuario = New UsuarioActual().UserName 
            Return vlo_Ws_OT_Catalogos.OTM_LUGAR_TRABAJO_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmLugarTrabajo) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()

            End If
        End Try
    End Function

    ''' <summary>
    ''' Obtener condicion de busqueda para enviar al metodo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ObtenerCondicionDeBusqueda() As String
        Dim vlc_Condicion As String = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroNombre.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '0 columna bd y 1 valor busqueda
                vlc_Condicion = String.Format("UPPER({0}) LIKE '%{1}%' AND {2} = {3}", Modelo.V_OTM_LUGAR_TRABAJO.NOMBRE, Me.txtFiltroNombre.Text.Trim.ToUpper, Modelo.V_OTM_LUGAR_TRABAJO.ID_UBICACION_ADMINISTRA, Me.UnidadAdministra)
            Else
                '0 condicion original, 1 columna db y 2 valor busqueda
                vlc_Condicion = String.Format("{0} AND UPPER({1}) LIKE '{2}' AND {3} = {4}", vlc_Condicion, Modelo.V_OTM_LUGAR_TRABAJO.NOMBRE, Me.txtFiltroNombre.Text.Trim.ToUpper, Modelo.V_OTM_LUGAR_TRABAJO.ID_UBICACION_ADMINISTRA, Me.UnidadAdministra)

            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroClasificacion.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '0 columna bd y 1 valor busqueda
                vlc_Condicion = String.Format("{0} = '{1}' AND {2} = {3}", Modelo.V_OTM_LUGAR_TRABAJO.CLASIFICACION, Me.ddlFiltroClasificacion.SelectedValue, Modelo.V_OTM_LUGAR_TRABAJO.ID_UBICACION_ADMINISTRA, Me.UnidadAdministra)
            Else
                '0 condicion original, 1 columna db y 2 valor busqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}' AND {3} = {4}", vlc_Condicion, Modelo.V_OTM_LUGAR_TRABAJO.CLASIFICACION, Me.ddlFiltroClasificacion.SelectedValue, Modelo.V_OTM_LUGAR_TRABAJO.ID_UBICACION_ADMINISTRA, Me.UnidadAdministra)

            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroUnidadAdministra.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '0 columna bd y 1 valor busqueda
                vlc_Condicion = String.Format("{0} = '{1}' AND {2} = {3}", Modelo.V_OTM_UNIDAD_ENCARGADALST.COD_UNIDAD_SIRH, Me.ddlFiltroUnidadAdministra.SelectedValue, Modelo.V_OTM_LUGAR_TRABAJO.ID_UBICACION_ADMINISTRA, Me.UnidadAdministra)
            Else
                '0 condicion original, 1 columna db y 2 valor busqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}' AND {3} = {4}", vlc_Condicion, Modelo.V_OTM_UNIDAD_ENCARGADALST.COD_UNIDAD_SIRH, Me.ddlFiltroUnidadAdministra.SelectedValue, Modelo.V_OTM_LUGAR_TRABAJO.ID_UBICACION_ADMINISTRA, Me.UnidadAdministra)

            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroUbicacion.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '0 columna bd y 1 valor busqueda
                vlc_Condicion = String.Format("{0} = '{1}' AND {2} = {3}", Modelo.V_OTM_LUGAR_TRABAJO.ID_UBICACION_PERTENECE, Me.ddlFiltroUbicacion.SelectedValue, Modelo.V_OTM_LUGAR_TRABAJO.ID_UBICACION_ADMINISTRA, Me.UnidadAdministra)
            Else
                '0 condicion original, 1 columna db y 2 valor busqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}' AND {3} = {4}", vlc_Condicion, Modelo.V_OTM_LUGAR_TRABAJO.ID_UBICACION_PERTENECE, Me.ddlFiltroUbicacion.SelectedValue, Modelo.V_OTM_LUGAR_TRABAJO.ID_UBICACION_ADMINISTRA, Me.UnidadAdministra)

            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroEstado.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '0 columna bd y 1 valor busqueda
                vlc_Condicion = String.Format("{0} = '{1}' AND {2} = {3}", Modelo.V_OTM_LUGAR_TRABAJO.ESTADO, Me.ddlFiltroEstado.SelectedValue, Modelo.V_OTM_LUGAR_TRABAJO.ID_UBICACION_ADMINISTRA, Me.UnidadAdministra)
            Else
                '0 condicion original, 1 columna db y 2 valor busqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}' AND {3} = {4}", vlc_Condicion, Modelo.V_OTM_LUGAR_TRABAJO.ESTADO, Me.ddlFiltroEstado.SelectedValue, Modelo.V_OTM_LUGAR_TRABAJO.ID_UBICACION_ADMINISTRA, Me.UnidadAdministra)

            End If
        End If

        If vlc_Condicion = "" Then
            vlc_Condicion = Me.Condicion
        End If

        Return vlc_Condicion

    End Function

    ''' <summary>
    ''' Ordenar ascendente o descendente la lista
    ''' </summary>
    ''' <param name="pvc_NombreColumna"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ObtenerSortExpression(pvc_NombreColumna As String) As String
        If String.IsNullOrWhiteSpace(UltimoSortColumn) OrElse pvc_NombreColumna.CompareTo(UltimoSortColumn) <> 0 Then
            UltimoSortColumn = pvc_NombreColumna
            UltimoSortDirection = SortDirection.Ascending
        Else
            If UltimoSortDirection = SortDirection.Ascending Then
                UltimoSortDirection = SortDirection.Descending
            Else
                UltimoSortDirection = SortDirection.Ascending

            End If
        End If
        '0 nombre de la columna y 1 direccion de ordenamiento
        UltimoSortExpression = String.Format("{0} {1}", UltimoSortColumn, IIf(UltimoSortDirection = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))
        Return UltimoSortExpression
    End Function

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionario(pvn_IdPersonal As String) As EntEmpleados
        Dim vlo_WsEU_Curriculo As wsEU_Curriculo

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("ID_PERSONAL = '{0}'", pvn_IdPersonal))
        Catch ex As Exception
            Throw
        Finally
            If vlo_WsEU_Curriculo IsNot Nothing Then
                vlo_WsEU_Curriculo.Dispose()
            End If
        End Try
    End Function

    Private Function VerificaSedes(pvc_NumEmpleado As String) As DataSet
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet
        Dim pvc_Condicion As String

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            pvc_Condicion = String.Format("NUM_EMPLEADO = {0}", pvc_NumEmpleado)

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_AUTORIZADO_UBICACION_ListarRegistrosLista(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            pvc_Condicion,
            String.Empty,
            False,
            0,
            0)

            Return vlo_DsDatos
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
