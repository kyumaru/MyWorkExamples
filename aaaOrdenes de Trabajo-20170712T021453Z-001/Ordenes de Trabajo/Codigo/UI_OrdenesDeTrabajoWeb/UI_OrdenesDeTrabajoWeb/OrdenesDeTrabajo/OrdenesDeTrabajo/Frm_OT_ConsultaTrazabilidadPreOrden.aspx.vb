Imports System.Data  'para utilizar data set
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos

Partial Class OrdenesDeTrabajo_Frm_OT_ConsultaTrazabilidadPreOrden
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
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
    ''' propiedad para la ubicacion de la preorden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdUbicacion As Integer
        Get
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para el id de la preorden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdPreOrdenTrabajo As Integer
        Get
            Return CType(ViewState("IdPreOrdenTrabajo"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdPreOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para la condicion de busqueda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Condicion As String
        Get
            Return CType(ViewState("Condicion"), String)
        End Get
        Set(value As String)
            ViewState("Condicion") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para almacenar la pantalla   a la que debe retorna al dar regresar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property PantallaRetorno As String
        Get
            Return CType(ViewState("PantallaRetorno"), String)
        End Get
        Set(value As String)
            ViewState("PantallaRetorno") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la entidad de la pre orden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property PreOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo
        Get
            Return CType(ViewState("PreOrdenTrabajo"), Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo)
        End Get
        Set(value As Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo)
            ViewState("PreOrdenTrabajo") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' evento que se ejecuta al dar inicializar la pagina
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim pvc_Condicion As String

        If Not IsPostBack Then
            Me.IdPreOrdenTrabajo = WebUtils.LeerParametro(Of Integer)("pvn_IdPreOrdenTrabajo")
            Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
            Me.PantallaRetorno = WebUtils.LeerParametro(Of String)("pvc_PantallaRetorno")

            pvc_Condicion = String.Format("{0} = {1} AND {2} = {3}", Modelo.V_OTF_REVISION_PRE_ORDENLST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTF_REVISION_PRE_ORDENLST.ID_PRE_ORDEN_TRABAJO, Me.IdPreOrdenTrabajo)
            Me.Condicion = pvc_Condicion
            CargarOrden(pvc_Condicion)
            Buscar(pvc_Condicion, String.Empty)
        End If

        Me.pnRpAcciones.Dibujar()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al dar click en algun elemento del paginador
    ''' </summary>
    ''' <param name="pvn_PaginaSeleccionada"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub pnRpAcciones_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpAcciones.CambioDePagina
        Try
            CargarLista(Me.Condicion, UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento de ordenamiento del listaod
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpAcciones_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(Me.Condicion, ObtenerSortExpression(e.CommandName), pnRpAcciones.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub
#End Region

#Region "Metodos"

    ''' <summary>
    ''' ejecuta js de no hay datos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    ''' <summary>
    ''' ejecuta js de error
    ''' </summary>
    ''' <param name="pvc_Mensaje"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' carga la pre orden de trabajo
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrden(pvc_Condicion As String)
        Dim vlo_DsOrden As System.Data.DataSet
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsOrden = vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                String.Empty,
                False,
                0,
                0)

            If vlo_DsOrden.Tables(0) IsNot Nothing AndAlso vlo_DsOrden.Tables(0).Rows.Count > 0 Then
                Me.lblEstado.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTF_PRE_ORDEN_TRABAJOLST.DESC_ESTADO_ORDEN)
                Me.lblEdificio.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTF_PRE_ORDEN_TRABAJOLST.DESC_LUGAR_TRABAJO)
                Me.lblDescripcion.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTF_PRE_ORDEN_TRABAJOLST.DESCRIPCION_TRABAJO)
                Me.lblCategoria.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTF_PRE_ORDEN_TRABAJOLST.DESC_CATEGORIA_SERVICIO)
                Me.lblActivididad.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTF_PRE_ORDEN_TRABAJOLST.DESC_ACTIVIDAD)
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_DsOrden IsNot Nothing Then
                vlo_DsOrden.Dispose()
            End If
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga la lista
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTF_REVISION_PRE_ORDEN_TRA_ListarRegistrosListaPersonalizado(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpAccion
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()

                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            Else
                With Me.rpAccion
                    .DataSource = Nothing
                    .DataBind()
                End With
                MostrarAlertaNoHayDatos()
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerSortExpression(Modelo.V_OTF_REVISION_PRE_ORDENLST.TIME_STAMP)
        End If

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntDatosPaginacion = vlo_Ws_OT_OrdenesDeTrabajo.OTF_REVISION_PRE_ORDEN_TRA_ObtenerDatosPaginacionVOtfRevisionPreOrdenTralst(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer)
                )

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpAcciones.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                Me.pnRpAcciones.Dibujar()
                lblCantidadRegistro.Visible = True
                lblCantidadRegistro.Text = String.Format("Cantidad de Trámites: {0}", vlo_EntDatosPaginacion.TotalRegistros)
                CargarLista(pvc_Condicion, pvc_Orden, 1)
            Else
                Me.lblCantidadRegistro.Visible = False
                Me.lblCantidadRegistro.Text = String.Empty
                MostrarAlertaNoHayDatos()
            End If
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
    ''' Ordenar ascendente o descendente la lista
    ''' </summary>
    ''' <param name="pvc_NombreColumna"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
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
