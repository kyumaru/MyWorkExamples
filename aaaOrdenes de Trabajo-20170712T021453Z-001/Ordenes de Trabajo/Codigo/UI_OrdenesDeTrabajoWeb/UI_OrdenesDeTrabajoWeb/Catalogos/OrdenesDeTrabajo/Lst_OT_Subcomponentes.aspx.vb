Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Imports System.Data

''' <summary>
''' Listado del catálogo de subcomponentes
''' </summary>
''' <remarks></remarks>
''' <author>César Bermúdez García</author>
''' <creationDate>16/11/2015</creationDate>
Partial Class Catalogos_LST_OT_Subcomponentes
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

    Private Property Espacio As EntOtmEspacio
        Get
            If ViewState("Espacio") Is Nothing Then
                Return New EntOtmEspacio
            End If
            Return CType(ViewState("Espacio"), EntOtmEspacio)
        End Get
        Set(value As EntOtmEspacio)
            ViewState("Espacio") = value
        End Set
    End Property
#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta cuando se carga el repeater de subcomponentes
    ''' Esto permite ejecutar correctamente la opción de borrar.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/11/2015</creationDate>
    Protected Sub rpSubcomponentes_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpSubcomponentes.ItemDataBound
        Dim vlo_IdBorrar As ImageButton
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_IdBorrar = e.Item.FindControl("ibBorrar")
            If vlo_IdBorrar IsNot Nothing Then
                vlo_IdBorrar.Attributes.Add("data-uniqueid", vlo_IdBorrar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Este evento se ejecutará al cargar la página
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/11/2015</creationDate>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                CargarEstado()
                CargarEspacio(WebUtils.LeerParametro(Of String)("pvc_IdEspacio"))
                Buscar(String.Format("{0} = {1}",
                                Modelo.OTM_SUBCOMPONENTE.ID_ESPACIO,
                                Me.Espacio.IdEspacio),
                        String.Empty)
                Me.NombreEspacio.Text = String.Format("Espacio: {0}", Me.Espacio.Descripcion)
                Dim agregar = CType(Utilerias.OrdenesDeTrabajo.eOperacion.Agregar, Integer)
                Dim href = String.Format("{0}{1}{2}{3}", Me.agregarSub.HRef, agregar, "&pvc_IdEspacio=", Me.Espacio.IdEspacio)
                Me.agregarSub.HRef = href
                Me.agregarSubcomp.HRef = href
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
        Me.pnRpSubcomponentes.Dibujar()
    End Sub

    ''' <summary>
    ''' Carga la lista deacuerdo al orden deseado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/11/2015</creationDate>
    Protected Sub lnkRPSubcomponentes_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerCondicionDeBusqueda, ObtenerSortExpression(e.CommandName), pnRpSubcomponentes.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub


    ''' <summary>
    ''' Altera el orden de el subcomponente, en este caso sube el subcomponente clickeado y baja el anterior,
    ''' Eso si no es el primero.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/11/2015</creationDate>
    Protected Sub imgSubirNuevo_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_imgSubirNuevo As ImageButton

        Try
            vlo_imgSubirNuevo = CType(sender, ImageButton)
            Dim vlo_IdEspacio = vlo_imgSubirNuevo.CommandArgument.ToString().Split(",")

            If vlo_IdEspacio(1) <> "1" Then
                SubirSubcomponente(vlo_IdEspacio(2), vlo_IdEspacio(0))
            End If

            UltimoSortDireccion = SortDirection.Descending
            Buscar(ObtenerCondicionDeBusqueda, String.Empty)


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
    ''' Altera el orden de el subcomponente, en este caso baja el subcomponente clickeado y sube el siguiente,
    ''' Eso si no es el último.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/11/2015</creationDate>
    Protected Sub imgBajarNuevo_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_imgSubirNuevo As ImageButton

        Try
            vlo_imgSubirNuevo = CType(sender, ImageButton)
            Dim vlo_IdEspacio = vlo_imgSubirNuevo.CommandArgument.ToString().Split(",")

            If OrdenValido(Me.Espacio.IdEspacio, vlo_IdEspacio(1)) Then
                BajarSubcomponente(vlo_IdEspacio(2), vlo_IdEspacio(0))
            End If

            UltimoSortDireccion = SortDirection.Descending
            Buscar(ObtenerCondicionDeBusqueda, String.Empty)

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
    ''' Ejecuta el método para borrar un subcomponente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/11/2015</creationDate>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_IbBorrar As ImageButton
        Dim vlc_IdSubcomponente() As String

        Try
            vlo_IbBorrar = CType(sender, ImageButton)
            vlc_IdSubcomponente = vlo_IbBorrar.CommandArgument.Split(",")

            If Borrar(vlc_IdSubcomponente(0), vlc_IdSubcomponente(1)) Then
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

    ''' <summary>
    ''' Ejecuta la función para buscar deacuerdo a los filtros seleccionados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/11/2015</creationDate>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Buscar(ObtenerCondicionDeBusqueda, String.Empty)
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
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("MostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub CargarEstado()
        Try
            Me.ddlFiltroEstado.Items.Clear()
            Me.ddlFiltroEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
            Me.ddlFiltroEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
            Me.ddlFiltroEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
        Catch ex As Exception
            Throw
        Finally
        End Try
    End Sub

    ''' <summary>
    ''' Método para cargar la lista de subcomponentes
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/11/2015</creationDate>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_SUBCOMPONENTE_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden, True, pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpSubcomponentes
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            Else
                With Me.rpSubcomponentes
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
    ''' Método para subir un componente de orden
    ''' </summary>
    ''' <param name="vlo_IdEspacio"></param>
    ''' <param name="vlo_IdSubcomponente"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/11/2015</creationDate>
    Private Sub SubirSubcomponente(vlo_IdEspacio As String, vlo_IdSubcomponente As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_Ws_OT_Catalogos.OTM_SUBCOMPONENTE_EjecutarPrOtSubirsubcomponente(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                CType(vlo_IdEspacio, Double),
                CType(vlo_IdSubcomponente, Double))
        Catch
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Método para bajar el orden de un subcomponente
    ''' </summary>
    ''' <param name="vlo_IdEspacio"></param>
    ''' <param name="vlo_IdSubcomponente"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/11/2015</creationDate>
    Private Sub BajarSubcomponente(vlo_IdEspacio As String, vlo_IdSubcomponente As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_Ws_OT_Catalogos.OTM_SUBCOMPONENTE_EjecutarPrOtBajarsubcomponente(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                CType(vlo_IdEspacio, Double),
                CType(vlo_IdSubcomponente, Double))
        Catch
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Permite actualizar el listado de subcomponentes
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/10/2015</creationDate>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EndDatosPaginacion As EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerSortExpression(Modelo.OTM_SUBCOMPONENTE.ORDEN)
        End If

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EndDatosPaginacion = vlo_Ws_OT_Catalogos.OTM_SUBCOMPONENTE_ObtenerDatosPaginacionVOtmSubcomponentelst(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EndDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpSubcomponentes.TotalPaginasLista = vlo_EndDatosPaginacion.TotalPaginas
                Me.pnRpSubcomponentes.Dibujar()
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de Subcomponentes: {0}", vlo_EndDatosPaginacion.TotalRegistros)
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
    ''' Obtiene el registro de espacio relacionado a este subcomponente desde la base de datos y lo carga en memoria
    ''' </summary>
    ''' <param name="pvc_IdEspacio"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/10/2015</creationDate>
    Private Sub CargarEspacio(pvc_IdEspacio As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0} Nombre de la columna 
            '{1} numero de ID_ESPACIO

            Me.Espacio = vlo_Ws_OT_Catalogos.OTM_ESPACIO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0})={1}", Modelo.OTM_ESPACIO.ID_ESPACIO, pvc_IdEspacio.Trim.ToUpper))

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
    ''' Valida que el ultimo espacio de la lista no ejecute un bajar para evitar un error
    ''' </summary>
    ''' <param name="pvs_IdOrden"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/10/2015</creationDate>
    Private Function OrdenValido(pvn_IdEspacio As Integer, pvs_IdOrden As String) As Boolean

        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Dim vlo_ultimo = vlo_Ws_OT_Catalogos.OTM_SUBCOMPONENTE_ObtenerFnOtUltimoordensubcomponente(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_IdEspacio)

            If vlo_ultimo - 1 = CType(pvs_IdOrden, Double) Then
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
    ''' Permite borrar un subcomponente de la fuente de datos
    ''' </summary>
    ''' <param name="pvc_IdEspacio"></param>
    ''' <param name="pvc_IdSubcomponente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/10/2015</creationDate>
    Private Function Borrar(pvc_IdEspacio As String, pvc_IdSubcomponente As String) As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmSubcomponente As EntOtmSubcomponente

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_EntOtmSubcomponente = New EntOtmSubcomponente

        Try
            vlo_EntOtmSubcomponente.IdSubcomponente = pvc_IdSubcomponente
            vlo_EntOtmSubcomponente.IdEspacio = pvc_IdEspacio

            Return vlo_Ws_OT_Catalogos.OTM_SUBCOMPONENTE_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmSubcomponente) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' Da formato a la condición de búsqueda, esta condicion debe tener como filtro el IdEspacio, ya que los subcomponentes estan limitados a un espacio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/10/2015</creationDate>
    Private Function ObtenerCondicionDeBusqueda() As String
        Dim vlc_Condicion As String = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroDescripcion.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor de búsqueda
                vlc_Condicion = String.Format("UPPER({0}) LIKE '%{1}%'",
                                            Modelo.OTM_SUBCOMPONENTE.DESCRIPCION,
                                            Me.txtFiltroDescripcion.Text.Trim.ToUpper)
            Else
                '{0} = Valor original de vlc_Condicion
                '{1} = Nombre de la columna
                '{2} = Valor a buscar
                vlc_Condicion = String.Format("{0} AND UPPER({1}) LIKE '%{2}%'",
                                            vlc_Condicion,
                                            Modelo.OTM_SUBCOMPONENTE.DESCRIPCION,
                                            Me.txtFiltroDescripcion.Text.Trim.ToUpper)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.Espacio.IdEspacio) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor de búsqueda
                vlc_Condicion = String.Format("{0} = {1}",
                                            Modelo.OTM_SUBCOMPONENTE.ID_ESPACIO,
                                            Me.Espacio.IdEspacio)
            Else
                '{0} = Valor original de vlc_Condicion
                '{1} = Nombre de la columna
                '{2} = Valor a buscar
                vlc_Condicion = String.Format("{0} AND {1} = {2}",
                                            vlc_Condicion,
                                            Modelo.OTM_SUBCOMPONENTE.ID_ESPACIO,
                                            Me.Espacio.IdEspacio)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroEstado.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor a buscar
                vlc_Condicion = String.Format("{0} = '{1}'",
                                            Modelo.OTM_SUBCOMPONENTE.ESTADO,
                                            Me.ddlFiltroEstado.SelectedValue)
            Else
                '{0} = Contenido de vlc_Condicion
                '{1} = Nombre de la columna a filtrar
                '{2} = valor de Búsqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'",
                                            vlc_Condicion,
                                            Modelo.OTM_SUBCOMPONENTE.ESTADO,
                                            Me.ddlFiltroEstado.SelectedValue)

            End If
        End If

        Return vlc_Condicion

    End Function

    ''' <summary>
    ''' Obtiene la expresion de ordenamiento 
    ''' </summary>
    ''' <param name="pvc_NombreColumna"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/10/2015</creationDate>
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

#End Region


End Class
