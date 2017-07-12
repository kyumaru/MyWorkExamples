Imports System.Data  'para utilizar data set
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos
Partial Class Catalogos_Lst_OT_Parametros
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
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>13/10/2015</creationDate>
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
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.Usuario = New UsuarioActual
            Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

            If Me.AutorizadoUbicacion.Existe Then
                Buscar(String.Format("{0} = {1} AND {2} = 0", Modelo.V_OTP_PARAMETRO_UBICACIONLST.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra, Modelo.V_OTP_PARAMETRO_UBICACIONLST.PROTEGIDO), String.Empty)
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no se encuentra autorizado para registrar ordenes de trabajo en ninguna sede.','../../Genericos/Frm_MenuPrincipal.aspx');")
            End If

        End If

        Me.pnRpParametros.Dibujar()
    End Sub

    Protected Sub pnRpParametros_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpParametros.CambioDePagina
        Try
            CargarLista(String.Format("{0} = {1} AND {2} = 0", Modelo.V_OTP_PARAMETRO_UBICACIONLST.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra, Modelo.V_OTP_PARAMETRO_UBICACIONLST.PROTEGIDO), UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub lnkRpParametros_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(String.Format("{0} = {1} AND {2} = 0", Modelo.V_OTP_PARAMETRO_UBICACIONLST.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra, Modelo.V_OTP_PARAMETRO_UBICACIONLST.PROTEGIDO), ObtenerSortExpression(e.CommandName), pnRpParametros.PaginaActualLista)
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

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
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
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpParametros
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()

                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            Else
                With Me.rpParametros
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
            pvc_Orden = ObtenerSortExpression(Modelo.V_OTP_PARAMETRO_UBICACIONLST.DESCRIPCION) 'lo pide el programa ordenar x isbn
        End If

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntDatosPaginacion = vlo_Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ObtenerDatosPaginacionVOtpParametroUbicacionlst(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer)
                )

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpParametros.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                Me.pnRpParametros.Dibujar()
                lblCantidadRegistro.Visible = True
                lblCantidadRegistro.Text = String.Format("Cantidad de parámetros: {0}", vlo_EntDatosPaginacion.TotalRegistros)
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

    ' ''' <summary>
    ' ''' Funcion para eliminar un registro
    ' ''' </summary>
    ' ''' <param name="pvc_Id_Ubicacion"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function Borrar(pvc_Id_Ubicacion As String) As Boolean
    '    Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
    '    Dim vlo_EntOtmUbicacion As EntOtmUbicacion

    '    vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
    '    vlo_Ws_OT_Catalogos.Timeout = -1
    '    vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
    '    vlo_EntOtmUbicacion = New EntOtmUbicacion
    '    Try
    '        vlo_EntOtmUbicacion.IdUbicacion = pvc_Id_Ubicacion
    '        'vlo_EntTcmLibro.Usuario = New UsuarioActual().UserName 
    '        Return vlo_Ws_OT_Catalogos.OTM_UBICACION_BorrarRegistro(
    '            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
    '            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
    '            vlo_EntOtmUbicacion) > 0
    '    Catch ex As Exception
    '        Throw
    '    Finally
    '        If vlo_Ws_OT_Catalogos IsNot Nothing Then
    '            vlo_Ws_OT_Catalogos.Dispose()

    '        End If
    '    End Try
    'End Function

    ' ''' <summary>
    ' ''' Obtener condicion de busqueda para enviar al metodo
    ' ''' </summary>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function ObtenerCondicionDeBusqueda() As String
    '    Dim vlc_Condicion As String = String.Empty

    '    'If Not String.IsNullOrWhiteSpace(Me.txtFiltroNombre.Text) Then
    '    '    If String.IsNullOrWhiteSpace(vlc_Condicion) Then
    '    '        '0 columna bd y 1 valor busqueda
    '    '        vlc_Condicion = String.Format("UPPER({0}) LIKE '%{1}%'", Modelo.V_OTM_UBICACION.DESCRIPCION, Me.txtFiltroNombre.Text.Trim.ToUpper)
    '    '    Else
    '    '        '0 condicion original, 1 columna db y 2 valor busqueda
    '    '        vlc_Condicion = String.Format("{0} AND UPPER({1}) LIKE '{2}'", vlc_Condicion, Modelo.V_OTM_UBICACION.DESCRIPCION, Me.txtFiltroNombre.Text.Trim.ToUpper)

    '    '    End If
    '    'End If

    '    'If Not String.IsNullOrWhiteSpace(Me.ddlFiltroPerteneceSede.SelectedValue) Then
    '    '    If String.IsNullOrWhiteSpace(vlc_Condicion) Then
    '    '        '0 columna bd y 1 valor busqueda
    '    '        vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OTM_UBICACION.PERTENECE_A_SEDE, Me.ddlFiltroPerteneceSede.SelectedValue)
    '    '    Else
    '    '        '0 condicion original, 1 columna db y 2 valor busqueda
    '    '        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTM_UBICACION.PERTENECE_A_SEDE, Me.ddlFiltroEstado.SelectedValue)

    '    '    End If
    '    'End If

    '    'If Not String.IsNullOrWhiteSpace(Me.ddlFiltroEstado.SelectedValue) Then
    '    '    If String.IsNullOrWhiteSpace(vlc_Condicion) Then
    '    '        '0 columna bd y 1 valor busqueda
    '    '        vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTM_UBICACION.ESTADO, Me.ddlFiltroEstado.SelectedValue)
    '    '    Else
    '    '        '0 condicion original, 1 columna db y 2 valor busqueda
    '    '        vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTM_UBICACION.ESTADO, Me.ddlFiltroEstado.SelectedValue)

    '    '    End If
    '    'End If

    '    Return vlc_Condicion

    'End Function

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
    ''' 
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>13/10/2015</creationDate>
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
