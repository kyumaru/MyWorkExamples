Imports System.Data
Imports Utilerias.TallerCapacitacion
Imports Wsr_TC_Catalogos

Partial Class Catalogos_Lst_TC_Libro
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

#End Region

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarCondicionLibro()
            Buscar(String.Empty, String.Empty)
        End If

        Me.pnRpLibros.Dibujar() 'siempre se ejecuta post back o no
    End Sub

    Protected Sub lnkRpLibros_Command(sender As Object, e As CommandEventArgs)
        ' no lleva handles xq handles solo se asocia a un control, este ocupa manejar varios, se hace desde el aspx, con el property oncommand en cada control
        Try
            CargarLista(ObtenerCondicionDeBusqueda, ObtenerSortExpression(e.CommandName), pnRpLibros.PaginaActualLista) ' lleva condicion, orden, numero de pag, commandname esta en el aspx como property

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub pnRpLibros_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpLibros.CambioDePagina
        Try
            CargarLista(ObtenerCondicionDeBusqueda, UltimoSortExpression, pvn_PaginaSeleccionada) ' lleva condicion, orden, numero de pag, commandname esta en el aspx como property

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            '
            Buscar(ObtenerCondicionDeBusqueda, String.Empty)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    'logica para borrar los ids a borrar se crea en tiempo de exe,
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_IbBorrar As ImageButton
        Dim vlc_Isbn As String

        Try
            vlo_IbBorrar = CType(sender, ImageButton)
            vlc_Isbn = vlo_IbBorrar.CommandArgument

            If Borrar(vlc_Isbn) Then
                Buscar(ObtenerCondicionDeBusqueda, String.Empty)
                MostrarAlertaRegistroBorrado()
            Else
                MostrarAlertaRegistroNoBorrado()
            End If
        Catch ex As Exception
            ' si hay autores asociados genera error, pero como viene por soap se encapsula como soap exepcion
            If TypeOf (ex) Is System.Web.Services.Protocols.SoapException _
                AndAlso CType(ex, System.Web.Services.Protocols.SoapException).Actor = TallerCapacitacionException.NOMBRE_CLASE Then
                Dim vlo_TallerCapacitacionException As TallerCapacitacionException = TallerCapacitacionException.GetFromSoapException(ex)
                MostrarAlertaError(vlo_TallerCapacitacionException.Message) ' registra un script para mostar un error
                WebUtils.RegistrarScript(Me, "ocultarAreaFiltros", "ocultarAreaFiltrosDeBusqueda();") ' mantiene visible la lista
            Else
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End If

        End Try
    End Sub

    'evento para confirmacion antes de borrar registro, se liga a cada linea del repeater
    Protected Sub rpLibros_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpLibros.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        ' antes de enviar el html se puede hacerle cambios
        If e.Item.ItemType = ListItemType.Item OrElse
                e.Item.ItemType = ListItemType.AlternatingItem Then
            'item tiene varios controles hijos
            vlo_IbBorrar = e.Item.FindControl("ibBorrar") ' si no lo encuentra devuelve null
            If vlo_IbBorrar IsNot Nothing Then
                'agregamos una propiedad mas al html data unique id
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)

            End If
        End If

    End Sub

#End Region

#Region "Métodos"
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

    Private Sub CargarCondicionLibro()
        Dim vlo_Ws_TC_Catalogos As Ws_TC_Catalogos
        Dim vlo_DsCondicionLibro As DataSet
        vlo_Ws_TC_Catalogos = New Ws_TC_Catalogos
        vlo_Ws_TC_Catalogos.Timeout = -1
        vlo_Ws_TC_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsCondicionLibro = vlo_Ws_TC_Catalogos.TCC_CONDICION_LIBRO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Empty,
                String.Empty,
                False,
                0,
                0)
            Me.ddlFiltroCondicionLibro.Items.Clear()
            Me.ddlFiltroCondicionLibro.Items.Add(New ListItem("[Todos...]", String.Empty))

            If vlo_DsCondicionLibro.Tables.Count > 0 AndAlso vlo_DsCondicionLibro.Tables(0).Rows.Count > 0 Then
                With Me.ddlFiltroCondicionLibro
                    .DataSource = vlo_DsCondicionLibro
                    .DataMember = vlo_DsCondicionLibro.Tables(0).TableName
                    .DataTextField = Modelo.TCC_CONDICION_LIBRO.DESCRIPCION
                    .DataValueField = Modelo.TCC_CONDICION_LIBRO.CONDICION_LIBRO
                    .DataBind()
                End With
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_TC_Catalogos IsNot Nothing Then
                vlo_Ws_TC_Catalogos.Dispose()
            End If
            If vlo_DsCondicionLibro IsNot Nothing Then
                vlo_DsCondicionLibro.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_TC_Catalogos As Ws_TC_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_TC_Catalogos = New Ws_TC_Catalogos
        vlo_Ws_TC_Catalogos.Timeout = -1
        vlo_Ws_TC_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_TC_Catalogos.TCM_LIBRO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer)
                )
            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpLibros
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            Else
                With Me.rpLibros
                    .DataSource = Nothing
                    .DataBind()
                End With
                MostrarAlertaNoHayDatos()
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_TC_Catalogos IsNot Nothing Then
                vlo_Ws_TC_Catalogos.Dispose()
            End If
            If vlo_DsDatos IsNot Nothing Then
                vlo_DsDatos.Dispose()
            End If
        End Try
    End Sub

    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_TC_Catalogos As Ws_TC_Catalogos
        Dim vlo_EntDatosPaginacion As EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerSortExpression(Modelo.V_TCM_LIBROLST.ISBN)
        End If

        vlo_Ws_TC_Catalogos = New Ws_TC_Catalogos
        vlo_Ws_TC_Catalogos.Timeout = -1
        vlo_Ws_TC_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntDatosPaginacion = vlo_Ws_TC_Catalogos.TCM_LIBRO_ObtenerDatosPaginacionVTcmLibrolst(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer)
            )

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpLibros.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                Me.pnRpLibros.Dibujar()
                Me.lblCantidadRegistro.Visible = True
                Me.lblCantidadRegistro.Text = String.Format("Cantidad de libros: {0}", vlo_EntDatosPaginacion.TotalRegistros)
                CargarLista(pvc_Condicion, pvc_Orden, 1)
            Else
                Me.lblCantidadRegistro.Visible = False
                Me.lblCantidadRegistro.Text = String.Empty
                MostrarAlertaNoHayDatos()
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_TC_Catalogos IsNot Nothing Then
                vlo_Ws_TC_Catalogos.Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"
    Private Function Borrar(pvc_Isbn As String) As Boolean
        Dim vlo_Ws_TC_Catalogos As Ws_TC_Catalogos
        Dim vlo_EntTcmLibro As EntTcmLibro

        vlo_Ws_TC_Catalogos = New Ws_TC_Catalogos
        With vlo_Ws_TC_Catalogos
            .Timeout = -1
            .Credentials = System.Net.CredentialCache.DefaultCredentials
        End With
        vlo_EntTcmLibro = New EntTcmLibro

        Try
            vlo_EntTcmLibro.Isbn = pvc_Isbn
            'vlo_EntTcmLibro.Usuario = New UsuarioActual().UserName

            Return vlo_Ws_TC_Catalogos.TCM_LIBRO_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntTcmLibro) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_TC_Catalogos IsNot Nothing Then
                vlo_Ws_TC_Catalogos.Dispose()
            End If
        End Try
    End Function

    Private Function ObtenerSortExpression(pvc_NombreColumna As String) As String
        If String.IsNullOrWhiteSpace(UltimoSortColumn) OrElse pvc_NombreColumna.CompareTo(UltimoSortColumn) Then
            UltimoSortColumn = pvc_NombreColumna
            UltimoSortDirection = SortDirection.Ascending
        Else
            If UltimoSortDirection = SortDirection.Ascending Then
                UltimoSortDirection = SortDirection.Descending
            Else
                UltimoSortDirection = SortDirection.Ascending
            End If
        End If
        '{0}: Nombre de la columna
        '{1}: Dirección de ordenamiento
        UltimoSortExpression = String.Format("{0} {1}", UltimoSortColumn, IIf(UltimoSortDirection = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))
        Return UltimoSortExpression
    End Function

    Private Function ObtenerCondicionDeBusqueda() As String
        Dim vlc_Condicion As String = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroIsbn.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0}: Columna BD
                '{1}: Valor de búsqueda
                vlc_Condicion = String.Format("UPPER({0}) LIKE '%{1}%'", Modelo.V_TCM_LIBROLST.ISBN, Me.txtFiltroIsbn.Text.Trim.ToUpper)
            Else
                '{0}: Condición original
                '{1}: Columna BD
                '{2}: Valor de búsqueda
                vlc_Condicion = String.Format("{0} AND UPPER({1}) LIKE '%{2}%'", vlc_Condicion, Modelo.V_TCM_LIBROLST.ISBN, Me.txtFiltroIsbn.Text.Trim.ToUpper)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroTitulo.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0}: Columna BD
                '{1}: Valor de búsqueda
                vlc_Condicion = String.Format("UPPER({0}) LIKE '%{1}%'", Modelo.V_TCM_LIBROLST.TITULO, Me.txtFiltroTitulo.Text.Trim.ToUpper)
            Else
                '{0}: Condición original
                '{1}: Columna BD
                '{2}: Valor de búsqueda
                vlc_Condicion = String.Format("{0} AND UPPER({1}) LIKE '%{2}%'", vlc_Condicion, Modelo.V_TCM_LIBROLST.TITULO, Me.txtFiltroTitulo.Text.Trim.ToUpper)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroCondicionLibro.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0}: Columna BD
                '{1}: Valor de búsqueda
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_TCM_LIBROLST.CONDICION_LIBRO, Me.ddlFiltroCondicionLibro.SelectedValue)
            Else
                '{0}: Condición original
                '{1}: Columna BD
                '{2}: Valor de búsqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_TCM_LIBROLST.CONDICION_LIBRO, Me.ddlFiltroCondicionLibro.SelectedValue)
            End If
        End If
        Return vlc_Condicion
    End Function

#End Region

End Class
