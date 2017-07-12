Imports System.Data  'para utilizar data set
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos
Partial Class Catalogos_Lst_OT_ActividadesPorCategoriaServicio
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

    Private Property IdCategoria As String
        Get
            If ViewState("IdCategoria") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdCategoria"), String)
        End Get
        Set(value As String)
            ViewState("IdCategoria") = value
        End Set
    End Property

    Private Property Condicion As String 'propiedad utilizada para mantener el ordenamiento correcto a la hora de cambiar de pagina o al darle click a algun encabezado del listado
        Get
            If ViewState("Condicion") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("Condicion"), String)
        End Get
        Set(value As String)
            ViewState("Condicion") = value
        End Set
    End Property

    Private Property Categoria As EntOtmCategoriaServicio
        Get
            Return CType(ViewState("Categoria"), EntOtmCategoriaServicio)
        End Get
        Set(value As EntOtmCategoriaServicio)
            ViewState("Categoria") = value
        End Set
    End Property

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
        Dim pvc_Condicion As String
        Me.UnidadAdministra = CType(Session("pvo_UnidadAdministra"), Integer)
        IdCategoria = WebUtils.LeerParametro(Of String)("pvc_IdCategoria")
        If Not IsPostBack Then
            CargarEstadoActividad()
            CargarSector()
            CargarCategoria()

            pvc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTM_ACTIVIDAD.ID_CATEGORIA_SERVICIO, IdCategoria)
            Me.Condicion = pvc_Condicion
            Buscar(pvc_Condicion, String.Empty)
        End If

        Me.pnRpActividades.Dibujar()
    End Sub

    Protected Sub rpActividades_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpActividades.ItemDataBound
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
                Buscar(Me.Condicion, UltimoSortExpression)
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

    Protected Sub btnNuevoRegistro_Click(sender As Object, e As EventArgs) Handles ibNuevoRegistro.Click
        Dim vlc_direccion As String
        Try
            vlc_direccion = String.Format("Frm_OT_ActividadesPorCategoriaServicio.aspx?pvn_Operacion={0}&pvc_IdCategoria={1}", CType(Utilerias.OrdenesDeTrabajo.eOperacion.Agregar, Integer), Me.IdCategoria)
            Response.Redirect(vlc_direccion, False)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles ibNuevo.Click
        Dim vlc_direccion As String
        Try
            vlc_direccion = String.Format("Frm_OT_ActividadesPorCategoriaServicio.aspx?pvn_Operacion={0}&pvc_IdCategoria={1}", CType(Utilerias.OrdenesDeTrabajo.eOperacion.Agregar, Integer), Me.IdCategoria)
            Response.Redirect(vlc_direccion, False)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub


    Protected Sub pnRpActividades_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpActividades.CambioDePagina
        Try
            CargarLista(Me.Condicion, UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub lnkRpActividades_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(Me.Condicion, ObtenerSortExpression(e.CommandName), pnRpActividades.PaginaActualLista)
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

    Private Sub CargarEstadoActividad() 'CondicionActividad

        Me.ddlFiltroEstado.Items.Clear()
        Me.ddlFiltroEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
        Me.ddlFiltroEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlFiltroEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
    End Sub

    Private Sub CargarSector()
        Dim vlo_DsSector As System.Data.DataSet
        Dim vlo_Wsr_OT_Catalogos As Ws_OT_Catalogos
        Dim pvc_Condicion As String

        Try
            vlo_Wsr_OT_Catalogos = New Ws_OT_Catalogos
            vlo_Wsr_OT_Catalogos.Timeout = -1
            vlo_Wsr_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
            'vlo_Wsr_TC_Catalogos.Url = ConfigurationManager.AppSettings(<ConstanteInterna.MI_SERVICIO_WEB>)

            pvc_Condicion = String.Format("{0} = '{1}' AND {2} = {3}", Modelo.V_OTM_SECTOR_TALLER.TIPO_AREA, Constantes.TIPO_AREA_SEC, Modelo.V_OTM_CATEGORIA_SERVICIO.ID_UBICACION_ADMINISTRA, Me.UnidadAdministra)

            vlo_DsSector = vlo_Wsr_OT_Catalogos.OTM_SECTOR_TALLER_ListarRegistrosLista(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            pvc_Condicion,
            String.Empty,
            False,
            0,
            0)
            'primer string.empty es la condicion
            Me.ddlFiltroSector.Items.Clear()
            Me.ddlFiltroSector.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))

            If vlo_DsSector.Tables(0) IsNot Nothing AndAlso vlo_DsSector.Tables(0).Rows.Count > 0 Then
                With Me.ddlFiltroSector
                    .DataSource = vlo_DsSector
                    .DataMember = vlo_DsSector.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_SECTOR_TALLER.NOMBRE
                    .DataValueField = Modelo.V_OTM_SECTOR_TALLER.ID_SECTOR_TALLER
                    .DataBind()
                End With
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_DsSector IsNot Nothing Then
                vlo_DsSector.Dispose()

            End If

            If vlo_Wsr_OT_Catalogos IsNot Nothing Then
                vlo_Wsr_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarCategoria()
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        '0 columna, 1 valor busqueda
        Try
            Me.Categoria = vlo_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0}) = '{1}'", Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO, Me.IdCategoria))   'verificar el nombre tambien?
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        With Me.Categoria

            Me.lblCategoria.Text = .Descripcion

        End With

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
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_ACTIVIDAD_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpActividades
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()

                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            Else
                With Me.rpActividades
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
            pvc_Orden = ObtenerSortExpression(Modelo.V_OTM_ACTIVIDADLST.DESCRIPCION)
        End If

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntDatosPaginacion = vlo_Ws_OT_Catalogos.OTM_ACTIVIDAD_ObtenerDatosPaginacionVOtmActividadlst(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer)
                )

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpActividades.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                Me.pnRpActividades.Dibujar()
                lblCantidadRegistro.Visible = True
                lblCantidadRegistro.Text = String.Format("Cantidad de Actividades: {0}", vlo_EntDatosPaginacion.TotalRegistros)
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
    ''' <param name="pvc_Id_Actividad"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Borrar(pvc_Id_Actividad As String) As Boolean 'pvc_Id_Categoria As String, pvc_IdActividad As String
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmActividad As EntOtmActividad
        Dim vlo_Clave() As Object

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_EntOtmActividad = New EntOtmActividad
        Try
            vlo_Clave = pvc_Id_Actividad.Split("_")

            vlo_EntOtmActividad.IdCategoriaServicio = vlo_Clave(0)
            vlo_EntOtmActividad.IdActividad = vlo_Clave(1)

            'vlc_condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = '{9}'", EMF_IDIOMA.COD_IDIOMA, vlo_Clave(0), EMF_IDIOMA.ID_CENTRO_IDIOMAS, vlo_Clave(1), EMF_IDIOMA.ID_NIVEL_IDIOMA, vlo_Clave(2), EMF_IDIOMA.ID_TIPO_IDENTIFICACION, vlo_Clave(3), EMF_IDIOMA.ID_PERSONAL, vlo_Clave(4))
            'vlo_EntTcmLibro.Usuario = New UsuarioActual().UserName 
            Return vlo_Ws_OT_Catalogos.OTM_ACTIVIDAD_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmActividad) > 0
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

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroDescripcion.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '0 columna bd y 1 valor busqueda
                vlc_Condicion = String.Format("UPPER({0}) LIKE '%{1}%'", Modelo.V_OTM_ACTIVIDAD.DESCRIPCION, Me.txtFiltroDescripcion.Text.Trim.ToUpper)
            Else
                '0 condicion original, 1 columna db y 2 valor busqueda
                vlc_Condicion = String.Format("{0} AND UPPER({1}) LIKE '%{2}%'", vlc_Condicion, Modelo.V_OTM_ACTIVIDAD.DESCRIPCION, Me.txtFiltroDescripcion.Text.Trim.ToUpper)

            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroSector.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '0 columna bd y 1 valor busqueda
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OTM_ACTIVIDAD.ID_SECTOR_TALLER, Me.ddlFiltroSector.SelectedValue)
            Else
                '0 condicion original, 1 columna db y 2 valor busqueda
                vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTM_ACTIVIDAD.ID_SECTOR_TALLER, Me.ddlFiltroSector.SelectedValue)

            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroEstado.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '0 columna bd y 1 valor busqueda
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTM_ACTIVIDAD.ESTADO, Me.ddlFiltroEstado.SelectedValue)
            Else
                '0 condicion original, 1 columna db y 2 valor busqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTM_ACTIVIDAD.ESTADO, Me.ddlFiltroEstado.SelectedValue)

            End If
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
#End Region
End Class
