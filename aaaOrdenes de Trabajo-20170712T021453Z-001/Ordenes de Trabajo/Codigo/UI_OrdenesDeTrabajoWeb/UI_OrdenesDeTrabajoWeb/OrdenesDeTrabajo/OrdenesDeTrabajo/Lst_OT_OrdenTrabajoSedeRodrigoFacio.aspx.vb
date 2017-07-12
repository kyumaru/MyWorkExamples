Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports WsrOrhEuCatalogosPlanilla.WsOrhEuCatalogosPlanilla

Partial Class OrdenesDeTrabajo_Lst_OT_OrdenTrabajoSedeRodrigoFacio
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la ultima condicion de busqueda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimaCondicionBusqueda As String
        Get
            If ViewState("UltimaCondicionBusqueda") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimaCondicionBusqueda"), String)
        End Get
        Set(value As String)
            ViewState("UltimaCondicionBusqueda") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property Paginador As Integer
        Get
            If ViewState("Paginador") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("Paginador"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Paginador") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/09/2015</creationDate>
    ''' <changeLog></changeLog>
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

    ''' <summary>
    ''' Propiedad para la ultima columna de clasificacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/09/2015</creationDate>
    ''' <changeLog></changeLog>
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

    ''' <summary>
    ''' Propiedad para la ultima direccion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/09/2015</creationDate>
    ''' <changeLog></changeLog>
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

    ''' <summary>
    ''' usuariuo en sesiion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property Parametro As Wsr_OT_Catalogos.EntOtpParametroGlobal
        Get
            Return CType(ViewState("Parametro"), Wsr_OT_Catalogos.EntOtpParametroGlobal)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtpParametroGlobal)
            ViewState("Parametro") = value
        End Set
    End Property

    ''' <summary>
    ''' otm_unidad_ubicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>09/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property UnidadUbicacion As Wsr_OT_Catalogos.EntOtmUnidadUbicacion
        Get
            Return CType(ViewState("UnidadUbicacion"), Wsr_OT_Catalogos.EntOtmUnidadUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmUnidadUbicacion)
            ViewState("UnidadUbicacion") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta al cargar la página, inicializa los componentes necesarios
    ''' para el funcionamiento de la página
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                LeerParametrosSession()
                CargarUnidades()
                Parametro = CargarParametro(Utilerias.OrdenesDeTrabajo.Parametros.SEDE_RODRIGO_FACIO)
                CargarComboCategorias()
                CargarComboCondicionEstado()
                If Not Me.trUnidad.Visible Then
                    UnidadUbicacion = CargarUnidadUbicacion(Me.ddlUnidad.SelectedValue)
                    If Me.UnidadUbicacion.Existe Then
                        Buscar(UltimaCondicionBusqueda, String.Empty)
                    Else
                        WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Su unidad aún no posee ningún registro asociado en el sistema de órdenes de trabajo. Para solventar esta situación contacte al administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                    End If
                Else
                    WebUtils.RegistrarScript(Me, "ocultaFiltrosCombo", "ocultaFiltrosCombo();")
                End If
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados de la 
    ''' tabla del listado, carga la lista de registros en orden relacionado a la columna seleccionada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpOrdenTrabajo_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerCondicionBusqueda, ObtenerExpresionDeOrdenamiento(e.CommandName), pnRpOrdenTrabajo.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' guarda variables de sesion y redirecciona el sistema la pantalla para observar la trazabildad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkTrazabilidad_Command(sender As Object, e As CommandEventArgs)
        Dim vlc_Llave As String() = e.CommandArgument.Split("¬")

        Try
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_IdPreOrdenTrabajo", vlc_Llave(2))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)


        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        If CType(vlc_Llave(3), Integer) = 0 Then
            Me.Session.Add("pvc_PantallaRetorno", "OrdenesDeTrabajo/Lst_OT_OrdenTrabajoSedeRodrigoFacio.aspx")
            Response.Redirect("../Frm_OT_ConsultaTrazabilidad.aspx", False)
        Else
            Me.Session.Add("pvc_PantallaRetorno", "Lst_OT_OrdenTrabajoSedeRodrigoFacio.aspx")
            Response.Redirect("Frm_OT_ConsultaTrazabilidadPreOrden.aspx", False)
        End If
    End Sub

    ''' <summary>
    '''  guarda variables de sesion y redirecciona el sistema la pantalla para realizar modificaciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibModificar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_Operacion", eOperacion.Modificar)
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvn_IdPreOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect(String.Format("Frm_OT_OrdenTrabajoSedeRodrigoFacio.aspx"), False)
    End Sub

    ''' <summary>
    ''' guarda variables de sesion y redirecciona el sistema la pantalla para realizar consulta de la ot
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibConsultar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_Operacion", eOperacion.Consultar)
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect(String.Format("Frm_OT_OrdenTrabajoSedeRodrigoFacio.aspx"), False)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los números del paginador
    ''' </summary>
    ''' <param name="pvn_PaginaSeleccionada"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub pnRpOrdenTrabajo_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpOrdenTrabajo.CambioDePagina
        Try
            Paginador = pvn_PaginaSeleccionada
            CargarLista(ObtenerCondicionBusqueda, UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al carar el repeater de ordenes, asigna atributos a cada elemento del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpOrdenTrabajo_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpOrdenTrabajo.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        Dim vlo_IbEnviar As ImageButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibEnviar") IsNot Nothing Then
                vlo_IbEnviar = CType(e.Item.FindControl("ibEnviar"), ImageButton)
                vlo_IbEnviar.Attributes.Add("data-uniqueid", vlo_IbEnviar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar, ubicada en la 
    ''' segunda columna de cada registro del listado, llama a la función borrar y muestra  un mensaje según 
    ''' la operacion efectuada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()

        Try

            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            If Borrar(CType(vlc_Llave(0), Integer), CType(vlc_Llave(1), Integer)) Then
                MostrarAlertaRegistroBorrado()
            Else
                MostrarAlertaRegistroNoBorrado()
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
        Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)
    End Sub

    ''' <summary>
    ''' evento que se ejecuta al dar click sobre el boton de enviar orden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibEnviar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()
        Dim vlo_EntOtfPreOrdenTrabajo As EntOtfPreOrdenTrabajo
        Dim vlo_EntOtmCategoriaServicio As Wsr_OT_Catalogos.EntOtmCategoriaServicio
        Dim vlo_DsDetallesMatriz As Data.DataSet
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_Estado As String
        Dim vln_Resultado As Integer = 0

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            vlo_EntOtfPreOrdenTrabajo = CargarPreOrdenTrabajo(vlc_Llave(0), vlc_Llave(1))
            vlo_EntOtfPreOrdenTrabajo.Usuario = Usuario.UserName
            vlo_EntOtmCategoriaServicio = CargarCategoriaServicio(vlo_EntOtfPreOrdenTrabajo.IdCategoriaServicio)

            vlc_Estado = EstadoOrden.ASIGNADA

            If vlo_EntOtmCategoriaServicio.RequiereFichaTecnica = 1 Then

                vlo_DsDetallesMatriz = CargarDetallesMatriz(vlc_Llave(0), vlc_Llave(1))

                If vlo_DsDetallesMatriz IsNot Nothing AndAlso vlo_DsDetallesMatriz.Tables(0).Rows.Count > 0 Then

                    vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_EnviarPreOrden(
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                    vlo_EntOtfPreOrdenTrabajo, vlc_Estado, vlo_EntOtmCategoriaServicio.RequiereFichaTecnica)

                    If vln_Resultado > 0 Then
                        WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)
                    Else
                        MostrarAlertaError("No ha sido posible actualizar la información del registro")
                    End If
                Else
                    MostrarAlertaError("El usuario que creó la orden debe crear la ficha técnica y sus detalles, para poder ser enviada.")
                End If
            Else

                vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_EnviarPreOrden(
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                    vlo_EntOtfPreOrdenTrabajo, vlc_Estado, vlo_EntOtmCategoriaServicio.RequiereFichaTecnica)

                If vln_Resultado > 0 Then
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                    Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)
                Else
                    MostrarAlertaError("No ha sido posible actualizar la información del registro")
                End If
            End If

            WebUtils.RegistrarScript(Me, "OcultarAreaFiltrosDeBusqueda", "ocultarAreaFiltrosDeBusqueda();")
        Catch ex As Exception
            If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                Dim vlo_OrdenesDeTrabajoException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                MostrarAlertaError(vlo_OrdenesDeTrabajoException.Message)
            Else
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el botón Buscar,
    ''' que se encuentra en el área de filtros.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            UltimaCondicionBusqueda = String.Empty
            Me.Paginador = 0

            If Not Me.trUnidad.Visible Then
                Me.ddlUnidad.SelectedIndex = 1
                UnidadUbicacion = CargarUnidadUbicacion(Me.ddlUnidad.SelectedValue)
                Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)
            Else

                If Me.ddlUnidad.SelectedValue <> String.Empty Then
                    BuscarCombo(ObtenerCondicionBusqueda, UltimoSortExpression)
                Else
                    Me.rpOrdenTrabajo.Visible = False
                    Me.pnRpOrdenTrabajo.Visible = False
                    Me.lblCantidadDeRegistros.Visible = False
                    WebUtils.RegistrarScript(Me, "ocultaFiltrosCombo", "ocultaFiltrosCombo();")
                End If
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cunado se cambia el valor del combo de unidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlUnidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUnidad.SelectedIndexChanged
        Try
            If Me.ddlUnidad.SelectedValue <> String.Empty Then

                UnidadUbicacion = CargarUnidadUbicacion(Me.ddlUnidad.SelectedValue)
                If Me.UnidadUbicacion.Existe Then
                    BuscarCombo(UltimaCondicionBusqueda, String.Empty)
                Else
                    Me.rpOrdenTrabajo.Visible = False
                    Me.pnRpOrdenTrabajo.Visible = False
                    Me.lblCantidadDeRegistros.Visible = False
                    WebUtils.RegistrarScript(Me, "Mensaje", "sinUnidadesAsociadas();")
                End If
            Else
                Me.rpOrdenTrabajo.Visible = False
                Me.pnRpOrdenTrabajo.Visible = False
                Me.lblCantidadDeRegistros.Visible = False
                WebUtils.RegistrarScript(Me, "ocultaFiltrosCombo", "ocultaFiltrosCombo();")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatosCombo()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatosCombo();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaRegistroBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroBorrado", "mostrarAlertaRegistroBorrado();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaRegistroNoBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroNoBorrado", "mostrarAlertaRegistroNoBorrado();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' lee y carga los parametros guardados en sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametrosSession()
        Try
            Me.Paginador = WebUtils.LeerParametro(Of Integer)("pvn_Paginador")
            Me.UltimaCondicionBusqueda = WebUtils.LeerParametro(Of String)("pvc_UltimaCondicionBusqueda")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Carga las unidades a las cuales esta relacionado el usuario en session, segun las jefaturas
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarUnidades()
        Dim vlo_DsUnidades As Data.DataSet

        Try

            vlo_DsUnidades = CargarUnidadesPorNombreUsuario(Me.Usuario.UserId)

            If vlo_DsUnidades IsNot Nothing AndAlso vlo_DsUnidades.Tables(0).Rows.Count > 0 Then

                With Me.ddlUnidad
                    .Items.Add(New ListItem("[Seleccione la unidad]", String.Empty))
                    .DataSource = vlo_DsUnidades
                    .DataMember = vlo_DsUnidades.Tables(0).TableName
                    .DataTextField = "DESCRIPCION"
                    .DataValueField = "CODIGO_UBICA"
                    .DataBind()
                End With

                If vlo_DsUnidades.Tables(0).Rows.Count = 1 Then
                    Me.ddlUnidad.SelectedIndex = 1
                    Me.trUnidad.Visible = False
                    Me.trLblUnidad.Visible = True
                    Me.lblNombreUnidad.Text = Me.ddlUnidad.SelectedItem.ToString
                Else
                    Me.trUnidad.Visible = True
                    Me.trLblUnidad.Visible = False
                End If
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no tiene asignada ninguna unidad en el sistema de Acciones de Personal por lo que no está autorizado a tramitar órdenes de trabajo mediante esta opción. Para solventar esta situación contacte al administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvc_Condicion">parámetro con la condicion de búsqueda</param>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarVOtUnionPreTransHist(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpOrdenTrabajo
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End With
            Else
                With Me.rpOrdenTrabajo
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
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
    ''' Método encargado de realizar la busqueda de registros segun los datos de 
    ''' condicion y orden de búsqueda
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerExpresionDeOrdenamiento(Modelo.V_OT_UNION_HISTORIC_TRANSAC.FECHA_HORA_SOLICITA)
        End If

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If String.IsNullOrWhiteSpace(pvc_Condicion) Then
                pvc_Condicion = String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_UBICACION_ORIGEN, Me.UnidadUbicacion.IdUbicacion, Modelo.V_OT_UNION_PRE_TRANS_HIST.DESC_CONDICION_ESTADO, CondicionEstado.EN_TRAMITE)
                UltimaCondicionBusqueda = pvc_Condicion
            Else
                pvc_Condicion = String.Format("{0} AND {1} = {2}", pvc_Condicion, Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_UBICACION_ORIGEN, Me.UnidadUbicacion.IdUbicacion)
                UltimaCondicionBusqueda = pvc_Condicion
            End If

            vlo_EntDatosPaginacion =
                vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerDatosPaginacionVOtUnionPreTransHist(
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                     pvc_Condicion,
                     pvc_Orden,
                     CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpOrdenTrabajo.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas

                If Me.Paginador > 0 Then
                    CargarLista(pvc_Condicion, pvc_Orden, Me.Paginador)
                Else
                    CargarLista(pvc_Condicion, pvc_Orden, 1)
                End If

                Me.pnRpOrdenTrabajo.Dibujar()
                Me.rpOrdenTrabajo.Visible = True
                Me.pnRpOrdenTrabajo.Visible = True
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de ordenes {0}", vlo_EntDatosPaginacion.TotalRegistros)
            Else
                Me.rpOrdenTrabajo.Visible = False
                Me.pnRpOrdenTrabajo.Visible = False
                Me.lblCantidadDeRegistros.Visible = False
                Me.lblCantidadDeRegistros.Text = String.Empty
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
    ''' Método encargado de realizar la busqueda de registros segun los datos de 
    ''' condicion y orden de búsqueda
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BuscarCombo(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerExpresionDeOrdenamiento(Modelo.V_OT_UNION_PRE_TRANS_HIST.FECHA_HORA_SOLICITA)
        End If

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If String.IsNullOrWhiteSpace(pvc_Condicion) Then
                pvc_Condicion = String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_UBICACION_ORIGEN, Me.UnidadUbicacion.IdUbicacion, Modelo.V_OT_UNION_PRE_TRANS_HIST.DESC_CONDICION_ESTADO, CondicionEstado.EN_TRAMITE)
                UltimaCondicionBusqueda = pvc_Condicion
            Else
                pvc_Condicion = String.Format("{0} AND {1} = {2}", pvc_Condicion, Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_UBICACION_ORIGEN, Me.UnidadUbicacion.IdUbicacion)
                UltimaCondicionBusqueda = pvc_Condicion
            End If

            vlo_EntDatosPaginacion =
                vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerDatosPaginacionVOtUnionPreTransHist(
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                     pvc_Condicion,
                     pvc_Orden,
                     CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpOrdenTrabajo.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas

                If Me.Paginador > 0 Then
                    CargarLista(pvc_Condicion, pvc_Orden, Me.Paginador)
                Else
                    CargarLista(pvc_Condicion, pvc_Orden, 1)
                End If

                Me.pnRpOrdenTrabajo.Dibujar()
                Me.rpOrdenTrabajo.Visible = True
                Me.pnRpOrdenTrabajo.Visible = True
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de ordenes {0}", vlo_EntDatosPaginacion.TotalRegistros)
            Else
                Me.rpOrdenTrabajo.Visible = False
                Me.pnRpOrdenTrabajo.Visible = False
                Me.lblCantidadDeRegistros.Visible = False
                Me.lblCantidadDeRegistros.Text = String.Empty
                MostrarAlertaNoHayDatosCombo()
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
    ''' carga el combo de categorias de la sede rodrigo facio
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboCategorias()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlCategoriaServicio.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODAS, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ListarRegistros(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} LIKE '%{1}%' AND {2} = {3}", Modelo.OTM_CATEGORIA_SERVICIO.ESTADO, Estado.ACTIVO, Modelo.OTM_CATEGORIA_SERVICIO.ID_UBICACION_ADMINISTRA, Me.Parametro.Valor),
               String.Empty,
               False,
               0,
               0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlCategoriaServicio
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_CATEGORIA_SERVICIO.DESCRIPCION
                    .DataValueField = Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO
                    .DataBind()
                End With
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
    ''' carga el combo con los tipos de condicion para el estado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboCondicionEstado()
        Me.ddlCondiconOrden.Items.Clear()
        Me.ddlCondiconOrden.Items.Add(New ListItem("En Trámite", CondicionEstado.EN_TRAMITE))
        Me.ddlCondiconOrden.Items.Add(New ListItem("Tramitadas", CondicionEstado.TRAMITADA))
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Carga un DataSet con la información de las ubicaciones a las cuales tiene acceso el usuario conectado
    ''' </summary>
    ''' <returns>DataSet con información de la tabla PLM_UBICACION_POR_USUARIO</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarUnidadesPorNombreUsuario(pvc_UserId As String) As Data.DataSet
        Dim vlo_WsOrhEuCatalogosPlanilla As WsrOrhEuCatalogosPlanilla.WsOrhEuCatalogosPlanilla 'para acceder a los métodos del BLL
        Dim vlo_DsDatos As Data.DataSet 'para almacenar la información de la base de datos

        'instanciar y configurar objetos
        vlo_WsOrhEuCatalogosPlanilla = New WsrOrhEuCatalogosPlanilla.WsOrhEuCatalogosPlanilla
        vlo_WsOrhEuCatalogosPlanilla.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsOrhEuCatalogosPlanilla.Timeout = -1

        Try

            'ejecutar proceso
            vlo_DsDatos = vlo_WsOrhEuCatalogosPlanilla.PLM_UBICACION_POR_USUARIO_ObtenerUbicacionesPorNombreUsuario(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_UserId,
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_PLANILLAS))
        Catch ex As Exception
            Throw
        Finally
            If vlo_WsOrhEuCatalogosPlanilla IsNot Nothing Then
                vlo_WsOrhEuCatalogosPlanilla.Dispose()
            End If
        End Try

        Return vlo_DsDatos
    End Function

    ''' <summary>
    ''' funcion encargada de realizar los oredenamientos segun los valores brindados
    ''' </summary>
    ''' <param name="pvc_Columna"> numero de la columna para el ordenamiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerExpresionDeOrdenamiento(pvc_Columna As String) As String
        If String.IsNullOrWhiteSpace(UltimoSortColumn) OrElse pvc_Columna.CompareTo(UltimoSortColumn) <> 0 Then
            UltimoSortColumn = pvc_Columna
            UltimoSortDirection = SortDirection.Ascending
        Else
            If UltimoSortDirection = SortDirection.Ascending Then
                UltimoSortDirection = SortDirection.Descending
            Else
                UltimoSortDirection = SortDirection.Ascending
            End If
        End If

        UltimoSortExpression = String.Format("{0} {1}", UltimoSortColumn, IIf(UltimoSortDirection = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))
        Return UltimoSortExpression
    End Function

    ''' <summary>
    ''' Crea y retorna la condicion de busqueda para la la visat de ot´s
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.ddlCategoriaServicio.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_CATEGORIA_SERVICIO, Me.ddlCategoriaServicio.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_CATEGORIA_SERVICIO, Me.ddlCategoriaServicio.SelectedValue)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlCondiconOrden.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OT_UNION_PRE_TRANS_HIST.DESC_CONDICION_ESTADO, Me.ddlCondiconOrden.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OT_UNION_PRE_TRANS_HIST.DESC_CONDICION_ESTADO, Me.ddlCondiconOrden.SelectedValue)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtNumOrden.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_ORDEN_TRABAJO, Me.txtNumOrden.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OT_UNION_PRE_TRANS_HIST.ID_ORDEN_TRABAJO, Me.txtNumOrden.Text.Trim)
            End If
        End If

        If UltimaCondicionBusqueda = String.Empty Then
            UltimaCondicionBusqueda = vlc_Condicion
        Else
            Return UltimaCondicionBusqueda
        End If

        Return vlc_Condicion
    End Function

    ''' <summary>
    ''' Función encargada de comunicarse con el  servicio web y proceder a borrar el registro
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvn_IdPreOrdenTrabajo"></param>
    ''' <returns>Si retorna un número mayor a 0 quiere decir que la operacion se realizo con éxito</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Borrar(pvn_IdUbicacion As Integer, pvn_IdPreOrdenTrabajo As Integer) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_BorradoFisico(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_IdUbicacion, pvn_IdPreOrdenTrabajo) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga un parametro del sistema, segun parametros
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarParametro(pvn_Valor As Integer) As Wsr_OT_Catalogos.EntOtpParametroGlobal
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_Ws_OT_Catalogos.OTP_PARAMETRO_GLOBAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_GLOBAL.ID_PARAMETRO, pvn_Valor))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga una unidad ubicacion de la tabla otm_unidad_ubicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>09/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarUnidadUbicacion(pvn_CodigoUnidadSIRH As Integer) As Wsr_OT_Catalogos.EntOtmUnidadUbicacion
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Ws_OT_Catalogos.OTM_UNIDAD_UBICACION_ObtenerRegistro(
              ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
              ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
              String.Format("{0} = {1}", Modelo.OTM_UNIDAD_UBICACION.COD_UNIDAD_SIRH, pvn_CodigoUnidadSIRH))
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga la entidad de la PRE orden de trabajo
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvn_IdPreOrdenTrabajo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarPreOrdenTrabajo(pvn_IdUbicacion As Integer, pvn_IdPreOrdenTrabajo As Integer) As Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_PRE_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTF_PRE_ORDEN_TRABAJO.ID_PRE_ORDEN_TRABAJO, pvn_IdPreOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga la entidad de la cateoria de servicio
    ''' </summary>
    ''' <param name="pvn_IdCategoriaServicio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarCategoriaServicio(pvn_IdCategoriaServicio As Integer) As Wsr_OT_Catalogos.EntOtmCategoriaServicio
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO, pvn_IdCategoriaServicio))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga un data set con los detalles de la OTF_FICHA_TECNICA_DETALLE
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvn_IdPreOrdenTrabajo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>27/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarDetallesMatriz(pvn_IdUbicacion As Integer, pvn_IdPreOrdenTrabajo As Integer) As Data.DataSet
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTF_FICHA_TECNICA_DETALLE_ListarRegistros(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_FICHA_TECNICA_DETALLE.ID_UBICACION, pvn_IdUbicacion, Modelo.OTF_FICHA_TECNICA_DETALLE.ID_PRE_ORDEN_TRABAJO, pvn_IdPreOrdenTrabajo),
                String.Empty,
                False,
                0, 0)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

#End Region

End Class
