Imports System.Data
Imports Wsr_OT_OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo.Modelo
Imports Utilerias.OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_AjusteInventarioAprobJefatura
    Inherits System.Web.UI.Page
#Region "Propiedades"
    ''' <summary>
    ''' Propiedad para la ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortExpressionArchivo As String
        Get
            If ViewState("UltimoSortExpressionArchivo") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortExpressionArchivo"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortExpressionArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima columna de clasificacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortColumnArchivo As String
        Get
            If ViewState("UltimoSortColumnArchivo") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortColumnArchivo"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortColumnArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima direccion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortDirectionArchivo As SortDirection
        Get
            If ViewState("UltimoSortDirectionArchivo") Is Nothing Then
                Return SortDirection.Ascending
            End If
            Return CType(ViewState("UltimoSortDirectionArchivo"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("UltimoSortDirectionArchivo") = value
        End Set
    End Property

    Private Property UltimaCondicionBusquedaArchivo As String
        Get
            If ViewState("UltimaCondicionBusquedaArchivo") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimaCondicionBusquedaArchivo"), String)
        End Get
        Set(value As String)
            ViewState("UltimaCondicionBusquedaArchivo") = value
        End Set
    End Property

    Public Property IdUbicacion As Integer
        Get
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    Public Property Anno As Integer
        Get
            Return CType(ViewState("Anno"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("Anno") = value
        End Set
    End Property

    Public Property ConsecutivoAjuste As Integer
        Get
            Return CType(ViewState("ConsecutivoAjuste"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("ConsecutivoAjuste") = value
        End Set
    End Property

    Private Property DsAdjuntos As Data.DataSet
        Get
            Return CType(ViewState("DsAdjuntos"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsAdjuntos") = value
        End Set
    End Property
#End Region

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try

                Me.IdUbicacion = CType(Session("pvn_IdUbicacion"), Integer)
                Me.Anno = CType(Session("pvn_Anno"), Integer)
                Me.ConsecutivoAjuste = CType(Session("pvn_ConsecutivoAjuste"), Integer)

                'Se cargan los archivos adjuntos
                Me.UltimaCondicionBusquedaArchivo = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.V_OTT_DETALLE_AJUSTELST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_DETALLE_AJUSTELST.ANNO, Me.Anno, Modelo.V_OTT_DETALLE_AJUSTELST.CONSECUTIVO_AJUSTE, Me.ConsecutivoAjuste)
                BuscarArchivo(Me.UltimaCondicionBusquedaArchivo, String.Empty)

                Me.trObservaciones.Visible = False
                Me.txtObservaciones.MaxLength = Modelo.OTT_AJUSTE_INVENTARIO.OBSERVACIONES_BD_TAMANO

                CargarObservaciones()

            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
            Me.pnRpArchivo.Dibujar()
        End If
    End Sub

    Protected Sub rdbAprobado_CheckedChanged(sender As Object, e As EventArgs) Handles rdbAprobado.CheckedChanged
        If Me.rdbAprobado.Checked Then
            Me.trObservaciones.Visible = False
        End If

    End Sub

    Protected Sub rdbDevuelto_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDevuelto.CheckedChanged
        If Me.rdbDevuelto.Checked Then
            Me.trObservaciones.Visible = True
            Me.txtObservaciones.Text = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Evento que permite cambiar de pagina en la lista de evaluaciones entrevista
    ''' </summary>
    ''' <param name="pvn_PaginaSeleccionada"></param>
    ''' <remarks></remarks>
    Protected Sub pnRpArchivo_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpArchivo.CambioDePagina
        Try
            CargarListaArchivo(UltimaCondicionBusquedaArchivo, UltimoSortExpressionArchivo, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub lnkArchivo_Command(sender As Object, e As CommandEventArgs)
        Try

            CargarListaArchivo(UltimaCondicionBusquedaArchivo, ObtenerExpresionDeOrdenamientoArchivo(e.CommandName), pnRpArchivo.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento del botón aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <author>Mauricio Salas</author>
    ''' <remarks></remarks>
    Protected Sub btnTramitar_Click(sender As Object, e As EventArgs) Handles btnTramitar.Click
        If Page.IsValid Then
            Try
                If Me.rdbDevuelto.Checked AndAlso Me.txtObservaciones.Text = String.Empty Then
                    MostrarAlertaError("Las observaciones son requeridas")
                Else
                    GuardarRevision()
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

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub


    ''' <summary>
    ''' Guarda la revisión de la solicitud
    ''' </summary>
    ''' <author>Mauricio Salas</author>
    ''' <remarks></remarks>
    Private Sub GuardarRevision()
        Dim vlo_Wsr_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlc_Condicion As String
        Dim vlo_Usuario As UsuarioActual
        Dim vln_Resultado As Integer
        Dim vlo_EntOttAjusteInventario As EntOttAjusteInventario
        Dim vlo_EntOtlTrazabilidadAjuste As EntOtlTrazabilidadAjuste

        Try

            vlo_Wsr_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
            vlo_Wsr_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Wsr_OT_OrdenesDeTrabajo.Timeout = -1

            vlo_Usuario = New UsuarioActual
            vlo_EntOtlTrazabilidadAjuste = New EntOtlTrazabilidadAjuste

            'se construye la entidad a guardar
            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTT_AJUSTE_INVENTARIO.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_AJUSTE_INVENTARIO.ANNO, Me.Anno, Modelo.OTT_AJUSTE_INVENTARIO.CONSECUTIVO_AJUSTE, Me.ConsecutivoAjuste)
            vlo_EntOttAjusteInventario = vlo_Wsr_OT_OrdenesDeTrabajo.OTT_AJUSTE_INVENTARIO_ObtenerRegistro(System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB).ToString, System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB).ToString, vlc_Condicion)

            If vlo_EntOttAjusteInventario.Existe Then
                vlo_EntOttAjusteInventario.EstadoAjuste = IIf(Me.rdbAprobado.Checked, Utilerias.OrdenesDeTrabajo.EstadoAjuste.APROBADO, Utilerias.OrdenesDeTrabajo.EstadoAjuste.DEVUELTO_JEFATURA)
            End If

            With vlo_EntOtlTrazabilidadAjuste
                .EstadoAjuste = IIf(Me.rdbAprobado.Checked, Utilerias.OrdenesDeTrabajo.EstadoAjuste.APROBADO, Utilerias.OrdenesDeTrabajo.EstadoAjuste.DEVUELTO_JEFATURA)
                .Observaciones = Me.txtObservaciones.Text
                .Usuario = vlo_Usuario.UserName
            End With


            vln_Resultado = vlo_Wsr_OT_OrdenesDeTrabajo.OTT_AJUSTE_INVENTARIO_AprobacionJefatura(System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB).ToString, System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB).ToString, vlo_EntOttAjusteInventario, vlo_EntOtlTrazabilidadAjuste)

            If vln_Resultado > 0 Then
                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
            Else
                MostrarAlertaError("No ha sido posible tramitar el ajuste de inventario")
            End If

        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub CargarObservaciones()
        Dim vlo_Wsr_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlc_Condicion As String
        Dim vlo_EntOttAjusteInventario As EntOttAjusteInventario

        Try

            vlo_Wsr_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
            vlo_Wsr_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Wsr_OT_OrdenesDeTrabajo.Timeout = -1

            'se construye la entidad a guardar
            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTT_AJUSTE_INVENTARIO.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_AJUSTE_INVENTARIO.ANNO, Me.Anno, Modelo.OTT_AJUSTE_INVENTARIO.CONSECUTIVO_AJUSTE, Me.ConsecutivoAjuste)
            vlo_EntOttAjusteInventario = vlo_Wsr_OT_OrdenesDeTrabajo.OTT_AJUSTE_INVENTARIO_ObtenerRegistro(System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB).ToString, System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB).ToString, vlc_Condicion)

            If vlo_EntOttAjusteInventario.Existe Then
                Me.txtObs.Text = vlo_EntOttAjusteInventario.Observaciones
            End If

        Catch ex As Exception
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' Método encargado de realizar la búsqueda de registros según los datos de 
    ''' condición y orden de búsqueda
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    Private Sub BuscarArchivo(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = String.Format("{0} {1}", V_OTT_DETALLE_AJUSTELST.ID_MATERIAL, Ordenamiento.ASCENDENTE)
        End If
        Me.UltimoSortExpressionArchivo = pvc_Orden

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntDatosPaginacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_AJUSTE_ObtenerDatosPaginacionVOttDetalleAjustelst(
                                             ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                             ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                             pvc_Condicion,
                                             pvc_Orden,
                                             CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer)
                                            )

            Me.pnRpArchivo.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.lblCantidadDeRegistrosArchivo.Visible = True
                Me.lblCantidadDeRegistrosArchivo.Text = String.Format("Cantidad de registros {0}", vlo_EntDatosPaginacion.TotalRegistros)
                Me.lblNoHayDAtosArchivo.Visible = False
                CargarListaArchivo(pvc_Condicion, pvc_Orden, 1)
            Else
                Me.lblCantidadDeRegistrosArchivo.Visible = False
                Me.lblCantidadDeRegistrosArchivo.Text = String.Empty
                Me.lblNoHayDAtosArchivo.Visible = True
                Me.rpArchivo.Visible = False
            End If

            Me.pnRpArchivo.Dibujar()

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
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvc_Condicion">parámetro con la condicion de búsqueda</param>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>01/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaArchivo(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            DsAdjuntos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_AJUSTE_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If DsAdjuntos IsNot Nothing AndAlso DsAdjuntos.Tables(0).Rows.Count > 0 Then
                With Me.rpArchivo
                    .DataSource = DsAdjuntos
                    .DataMember = DsAdjuntos.Tables(0).TableName
                    .DataBind()
                    .Visible = True
                End With
                Me.lblNoHayDAtosArchivo.Visible = False

            Else
                With Me.rpArchivo
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With
                Me.lblNoHayDAtosArchivo.Visible = True
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If

            If DsAdjuntos IsNot Nothing Then
                DsAdjuntos.Dispose()
            End If
        End Try
    End Sub


#End Region

#Region "Funciones"
    Private Function ObtenerExpresionDeOrdenamientoArchivo(pvc_Columna As String) As String
        If String.IsNullOrWhiteSpace(UltimoSortColumnArchivo) OrElse pvc_Columna.CompareTo(UltimoSortColumnArchivo) <> 0 Then
            UltimoSortColumnArchivo = pvc_Columna
            UltimoSortDirectionArchivo = SortDirection.Ascending
        Else
            If UltimoSortDirectionArchivo = SortDirection.Ascending Then
                UltimoSortDirectionArchivo = SortDirection.Descending
            Else
                UltimoSortDirectionArchivo = SortDirection.Ascending
            End If
        End If

        UltimoSortExpressionArchivo = String.Format("{0} {1}", UltimoSortColumnArchivo, IIf(UltimoSortDirectionArchivo = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))
        Return UltimoSortExpressionArchivo
    End Function
#End Region
End Class
