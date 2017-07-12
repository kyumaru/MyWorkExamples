Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Imports System.Data

''' <summary>
''' Listado de Catálogos de espacio
''' </summary>
''' <remarks></remarks>
''' <author>César Bermúdez García</author>
''' <creationDate>10/11/2015</creationDate>
Partial Class Catalogos_LST_OT_Espacios
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
#End Region

#Region "Eventos"

    Protected Sub rpEspacios_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpEspacios.ItemDataBound
        Dim vlo_IdBorrar As ImageButton
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_IdBorrar = e.Item.FindControl("ibBorrar")
            If vlo_IdBorrar IsNot Nothing Then
                vlo_IdBorrar.Attributes.Add("data-uniqueid", vlo_IdBorrar.UniqueID)
            End If
        End If
    End Sub

    Protected Sub lnkRPEspacios_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerCondicionDeBusqueda, ObtenerSortExpression(e.CommandName), pnRpEspacios.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub pnRpEspacios_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpEspacios.CambioDePagina
        Try
            CargarLista(ObtenerCondicionDeBusqueda, UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Dim vlo_Usuario = New UsuarioActual
                Dim vlo_AutorizadoUbicacion = CargarAutorizadoUbicacion(vlo_Usuario.NumEmpleado)
                If vlo_AutorizadoUbicacion.Existe Then
                    CargarEstadoEspacio()
                    Buscar(String.Empty, String.Empty)
                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('No se pudo encontrar ninguna sede a su cargo por lo que no está autorizado a realizar acciones en esta pantalla. Para gestionar los permisos necesarios contacte al administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
        Me.pnRpEspacios.Dibujar()
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Buscar(ObtenerCondicionDeBusqueda, String.Empty)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_IbBorrar As ImageButton
        Dim vlc_IdEspacio As String

        Try
            vlo_IbBorrar = CType(sender, ImageButton)
            vlc_IdEspacio = vlo_IbBorrar.CommandArgument

            If Borrar(vlc_IdEspacio) Then
                UltimoSortDireccion = SortDirection.Descending
                Buscar(ObtenerCondicionDeBusqueda, String.Empty)
                MostrarAlertaRegistroBorrado()
            Else
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

    Protected Sub imgSubirNuevo_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_imgSubirNuevo As ImageButton

        Try

            vlo_imgSubirNuevo = CType(sender, ImageButton)
            Dim vlo_IdEspacio = vlo_imgSubirNuevo.CommandArgument.ToString().Split(",")

            If vlo_IdEspacio(1) <> "1" Then
                SubirEspacio(vlo_IdEspacio(0))
            End If

            UltimoSortDireccion = SortDirection.Descending
            Buscar(String.Empty, String.Empty)
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

    Protected Sub imgBajarNuevo_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_imgSubirNuevo As ImageButton

        Try

            vlo_imgSubirNuevo = CType(sender, ImageButton)
            Dim vlo_IdEspacio = vlo_imgSubirNuevo.CommandArgument.ToString().Split(",")

            If OrdenValido(vlo_IdEspacio(1)) Then
                BajarEspacio(vlo_IdEspacio(0))
            End If

            UltimoSortDireccion = SortDirection.Descending
            Buscar(String.Empty, String.Empty)

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
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("MostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub CargarEstadoEspacio()
        Try
            Me.ddlFiltroEstado.Items.Clear()
            Me.ddlFiltroEstado.Items.Add(New ListItem("[Todos...]", String.Empty))
            Me.ddlFiltroEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
            Me.ddlFiltroEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
        Catch ex As Exception
            Throw
        Finally
        End Try
    End Sub

    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_ESPACIO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden, True, pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpEspacios
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            Else
                With Me.rpEspacios
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

    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EndDatosPaginacion As EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerSortExpression(Modelo.OTM_ESPACIO.ORDEN)
        End If

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EndDatosPaginacion = vlo_Ws_OT_Catalogos.OTM_ESPACIO_ObtenerDatosPaginacionVOtmEspaciolst(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EndDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpEspacios.TotalPaginasLista = vlo_EndDatosPaginacion.TotalPaginas
                Me.pnRpEspacios.Dibujar()
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de Espacios: {0}", vlo_EndDatosPaginacion.TotalRegistros)
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

    Private Sub SubirEspacio(vlo_IdEspacio As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_Ws_OT_Catalogos.OTM_ESPACIO_EjecutarPrOtSubirespacio(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                CType(vlo_IdEspacio, Double))
        Catch
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

    End Sub

    Private Sub BajarEspacio(vlo_IdEspacio As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_Ws_OT_Catalogos.OTM_ESPACIO_EjecutarPrOtBajarespacio(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                CType(vlo_IdEspacio, Double))
        Catch
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

    End Sub

#End Region

#Region "Funciones"

    Private Function Borrar(pvc_Id As String) As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmEspacio As EntOtmEspacio

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_EntOtmEspacio = New EntOtmEspacio

        Try
            vlo_EntOtmEspacio.IdEspacio = pvc_Id

            Return vlo_Ws_OT_Catalogos.OTM_ESPACIO_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmEspacio) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

    End Function

    Private Function ObtenerCondicionDeBusqueda() As String
        Dim vlc_Condicion As String = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroDescripcion.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor de búsqueda
                vlc_Condicion = String.Format("UPPER({0}) LIKE '%{1}%'",
                                            Modelo.OTM_ESPACIO.DESCRIPCION,
                                            Me.txtFiltroDescripcion.Text.Trim.ToUpper)
            Else
                '{0} = Valor original de vlc_Condicion
                '{1} = Nombre de la columna
                '{2} = Valor a buscar
                vlc_Condicion = String.Format("{0} AND UPPER({1}) LIKE '%{2}%'",
                                            vlc_Condicion,
                                            Modelo.OTM_ESPACIO.DESCRIPCION,
                                            Me.txtFiltroDescripcion.Text.Trim.ToUpper)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroEstado.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor a buscar
                vlc_Condicion = String.Format("{0} = '{1}'",
                                            Modelo.OTM_ESPACIO.ESTADO,
                                            Me.ddlFiltroEstado.SelectedValue)
            Else
                '{0} = Contenido de vlc_Condicion
                '{1} = Nombre de la columna a filtrar
                '{2} = valor de Búsqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'",
                                            vlc_Condicion,
                                            Modelo.OTM_ESPACIO.ESTADO,
                                            Me.ddlFiltroEstado.SelectedValue)

            End If
        End If

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
    ''' Valida que el ultimo espacio de la lista no ejecute un bajar para evitar un error
    ''' </summary>
    ''' <param name="pvs_IdOrden"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/12/2015</creationDate>
    Private Function OrdenValido(pvs_IdOrden As String) As Boolean

        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Dim vlo_ultimo = vlo_Ws_OT_Catalogos.OTM_ESPACIO_ObtenerFnOtObtenerUltimoOrden(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB))

            If vlo_ultimo = CType(pvs_IdOrden, Double) Then
                Return False
            End If


        Catch
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        Return True
    End Function

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la autorizacion segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_NumEmpleado">numero de empleado</param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>16/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarAutorizadoUbicacion(pvn_NumEmpleado As Integer) As EntOtmAutorizadoUbicacion
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
