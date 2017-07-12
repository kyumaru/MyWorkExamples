Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Imports System.Data

Partial Class Catalogos_Frm_OT_Ordenar
    Inherits System.Web.UI.Page

#Region "Propiedades"
    Private Property idRequerimiento As String
        Get
            Return CType(ViewState("idRequerimiento"), String)
        End Get
        Set(value As String)
            ViewState("idRequerimiento") = value
        End Set
    End Property
#End Region

#Region "Eventos"
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        Response.Redirect("Lst_OT_Requerimientos.aspx")
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Me.idRequerimiento = WebUtils.LeerParametro(Of String)("pvc_IdRequerimiento")
                
                Buscar(ObtenerCondicionBusqueda(), ObtenerOrden())

            Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        End If
    End Sub

    Protected Sub imgSubirNuevo_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_imgSubirNuevo As ImageButton

        Try

            vlo_imgSubirNuevo = CType(sender, ImageButton)
            Dim vlo_IdRequerimiento = vlo_imgSubirNuevo.CommandArgument.ToString().Split(",")

            If vlo_IdRequerimiento(2) <> "1" Then
                SubirRequerimiento(vlo_IdRequerimiento(0))
            End If

            Buscar(ObtenerCondicionBusqueda(), ObtenerOrden())

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
            Dim vlo_IdRequerimiento = vlo_imgSubirNuevo.CommandArgument.ToString().Split(",")

            If OrdenValido(vlo_IdRequerimiento(1), vlo_IdRequerimiento(2)) Then
                BajarRequerimiento(vlo_IdRequerimiento(0))
            End If

            Buscar(ObtenerCondicionBusqueda(), ObtenerOrden())

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

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("MostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EndDatosPaginacion As EntDatosPaginacion

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EndDatosPaginacion = vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_ObtenerDatosPaginacionVOtmRequerimientolst(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EndDatosPaginacion.TotalRegistros > 0 Then
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de Requerimientos: {0}", vlo_EndDatosPaginacion.TotalRegistros)
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

    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden, False, pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpRequerimiento
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            Else
                With Me.rpRequerimiento
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

    Private Sub SubirRequerimiento(pvn_IdRequerimiento As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_EjecutarPrOtSubirrequerimiento(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_IdRequerimiento)
        Catch
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

    End Sub

    Private Sub BajarRequerimiento(pvn_IdRequerimiento As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_EjecutarPrOtBajarrequerimiento(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_IdRequerimiento)
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
    ''' <summary>
    ''' Valida que el último requerimiento de la lista no ejecute un bajar para evitar un error
    ''' </summary>
    ''' <param name="pvn_Nivel"></param>
    ''' <param name="pvn_Orden"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/11/2015</creationDate>
    Private Function OrdenValido(pvn_Nivel As Integer, pvn_Orden As Integer) As Boolean

        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Dim vlo_ultimo = vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_ObtenerFnOtUltimoordenrequerimiento(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), pvn_Nivel) - 1

            If vlo_ultimo = pvn_Orden Then
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

    Private Function ObtenerCondicionBusqueda() As String
        Dim result As String
        If Not String.IsNullOrEmpty(idRequerimiento) Then
            '{0} = Nombre de Columna
            '{1} = Id requerimiento 
            result = String.Format("{0} = {1} ", Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTO.ID_REQUERIMIENTO_PADRE, Me.idRequerimiento)
        Else
            '{0} = Nombre de Columna
            '{1} = Obtener los requerimientos de Nivel 1
            result = String.Format("{0} = {1} ", Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTO.NIVEL, 1)
        End If
        Return result
    End Function

    Private Function ObtenerOrden() As String
        '{0} = Nombre de Columna
        '{1} = Ordenar por NIVEL Ascendente
        '{2} = Nombre de Columna
        '{3} = Ordenar por ORDEN Ascendentemente
        Return String.Format("{0} {1},{2} {3}", Utilerias.OrdenesDeTrabajo.Modelo.OTM_REQUERIMIENTO.NIVEL, Utilerias.OrdenesDeTrabajo.Ordenamiento.ASCENDENTE, Utilerias.OrdenesDeTrabajo.Modelo.OTM_REQUERIMIENTO.ORDEN, Utilerias.OrdenesDeTrabajo.Ordenamiento.ASCENDENTE)
    End Function
#End Region


End Class
