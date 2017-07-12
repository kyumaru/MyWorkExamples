Imports System.Data  'para utilizar data set
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos
Partial Class OrdenesDeTrabajo_Frm_OT_ConsultaTrazabilidad
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

    Private Property IdUbicacion As Integer
        Get
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    Private Property IdOrdenTrabajo As String
        Get
            Return CType(ViewState("IdOrdenTrabajo"), String)
        End Get
        Set(value As String)
            ViewState("IdOrdenTrabajo") = value
        End Set
    End Property

    Private Property Condicion As String
        Get
            Return CType(ViewState("Condicion"), String)
        End Get
        Set(value As String)
            ViewState("Condicion") = value
        End Set
    End Property

    Public Property PantallaRetorno As String
        Get
            Return CType(ViewState("PantallaRetorno"), String)
        End Get
        Set(value As String)
            ViewState("PantallaRetorno") = value
        End Set
    End Property

    Private Property OrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo)
        End Get
        Set(value As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

#End Region

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim pvc_Condicion As String

        If Not IsPostBack Then
            IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
            IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
            PantallaRetorno = WebUtils.LeerParametro(Of String)("pvc_PantallaRetorno")

            pvc_Condicion = String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo)
            Me.Condicion = pvc_Condicion
            CargarOrden(pvc_Condicion)
            Buscar(pvc_Condicion)
        End If

        Me.pnRpAcciones.Dibujar()
    End Sub

    Protected Sub pnRpAcciones_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpAcciones.CambioDePagina
        Try
            CargarLista(Me.Condicion, UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

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

    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub CargarOrden(pvc_Condicion As String)
        Dim vlo_DsOrden As System.Data.DataSet
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        Try
            vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
            vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
            vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
            'vlo_Wsr_TC_Catalogos.Url = ConfigurationManager.AppSettings(<ConstanteInterna.MI_SERVICIO_WEB>)

            'pvc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTM_SECTOR_TALLER.TIPO_AREA, Constantes.TIPO_AREA_SEC)

            vlo_DsOrden = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarRegistrosLista(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            pvc_Condicion,
            String.Empty,
            False,
            0,
            0)
            'primer string.empty es la condicion


            If vlo_DsOrden.Tables(0) IsNot Nothing AndAlso vlo_DsOrden.Tables(0).Rows.Count > 0 Then
                Me.lblNumeroOrden.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)
                Me.lblPdago.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.NUMERO_ORDEN)
                Me.lblEstado.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_ESTADO_ORDEN)
                Me.lblEdificio.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_LUGAR_TRABAJO)
                Me.lblDescripcion.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.DESCRIPCION_TRABAJO)
                Me.lblCategoria.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_CATEGORIA_SERVICIO)
                Me.lblActivididad.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_ACTIVIDAD)
                Me.lblEncargado.Text = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_CargarEncargadoTramite(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO),
                vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.COD_UNIDAD_SIRH),
                vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.ID_SECTOR_TALLER),
                vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.NOMBRE_SOLICITANTE))
            End If

            If Me.lblEncargado.Text = "" Then
                Me.trEncargado.Visible = False
            Else
                'Me.trEncargado.Visible = True
                Me.trEncargado.Visible = False
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
    ''' Carga la lista del catalogo
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_TRAZABILIDAD_PROCESO_ListarRegistrosListaPersonalizado(
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
    ''' <remarks></remarks>
    Private Sub Buscar(pvc_Condicion As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion


        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntDatosPaginacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_TRAZABILIDAD_PROCESO_ObtenerDatosPaginacionVOttTrazabilidadProcesolst(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                String.Format("{0} {1}", Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.FECHA_HORA_EJECUCION, Ordenamiento.DESCENDENTE),
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer)
                )

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpAcciones.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                Me.pnRpAcciones.Dibujar()
                lblCantidadRegistro.Visible = True
                lblCantidadRegistro.Text = String.Format("Cantidad de Trámites: {0}", vlo_EntDatosPaginacion.TotalRegistros)
                CargarLista(pvc_Condicion, String.Format("{0} {1}", Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.FECHA_HORA_EJECUCION, Ordenamiento.DESCENDENTE), 1) '1 xq la lista siempre carga en esa posicion
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
