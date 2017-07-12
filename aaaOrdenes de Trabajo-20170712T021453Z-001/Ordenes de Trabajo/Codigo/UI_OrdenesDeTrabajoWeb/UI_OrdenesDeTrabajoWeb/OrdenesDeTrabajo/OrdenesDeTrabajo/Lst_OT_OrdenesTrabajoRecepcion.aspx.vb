Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports Wsr_SDP_ReportServer
Imports Utilerias.GeneradorDeReportes

Partial Class OrdenesDeTrabajo_Lst_OT_OrdenesTrabajoRecepcion
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la ultima condicion de busqueda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/12/2015</creationDate>
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
    ''' <creationDate>02/12/2015</creationDate>
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
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property NuevaSeccion As Integer
        Get
            If ViewState("NuevaSeccion") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("NuevaSeccion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("NuevaSeccion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
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
    ''' <creationDate>16/09/2015</creationDate>
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
    ''' Propiedad para la ultima columna secundaria de clasificacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Jairo Alfaro Magnan</author>
    ''' <creationDate>26/05/2017</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortColumnSecundario As String
        Get
            If ViewState("UltimoSortColumnSecundario") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortColumnSecundario"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortColumnSecundario") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima direccion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
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

    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para la ubicacion en la que esta autorizado el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/10/2015</creationDate>
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
    ''' Prpoiedad para cargar datos del empleado en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Empleado As WsrEU_Curriculo.EntEmpleados
        Get
            Return CType(ViewState("Empleado"), WsrEU_Curriculo.EntEmpleados)
        End Get
        Set(value As WsrEU_Curriculo.EntEmpleados)
            ViewState("Empleado") = value
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
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.Empleado = CargarFuncionario(Me.Usuario.UserName)
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If Me.AutorizadoUbicacion.Existe Then
                    LeerParametrosSession()
                    Buscar(ObtenerCondicionBusqueda, String.Format("{0} {1} , {2} {3}", Modelo.V_OT_UNION_ORDEN_PREORDEN.TIPO_ORDEN_TRABAJO, Ordenamiento.ASCENDENTE, Modelo.V_OT_UNION_ORDEN_PREORDEN.FECHA_HORA_SOLICITA, Ordenamiento.DESCENDENTE))
                    CargarComboEstado()
                    CargarComboCategorias()
                    CargarComboTipoOrden()
                    CargarComboSectorTaller()
                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no se encuentra autorizado en ninguna sede.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If

            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
        Me.pnRpOrdenTrabajo.Dibujar()
    End Sub

    ''' <summary>
    ''' lee y carga los parametros guardados en sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametrosSession()

        Try

            Me.NuevaSeccion = WebUtils.LeerParametro(Of Integer)("pvn_NuevaSeccion")

            If Me.NuevaSeccion <> 1 Then
                Me.Paginador = WebUtils.LeerParametro(Of Integer)("pvn_Paginador")
                Me.UltimaCondicionBusqueda = WebUtils.LeerParametro(Of String)("pvc_UltimaCondicionBusqueda")
            Else
                Me.Paginador = 0
                Me.UltimaCondicionBusqueda = String.Empty
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados, exceptuandeo el de Número de Orden de Trabajo,  de la 
    ''' tabla del listado, carga la lista de registros en orden relacionado a la columna seleccionada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog>
    ''' </changeLog>
    Protected Sub lnkRpOrdenTrabajo_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerCondicionBusqueda, ObtenerExpresionDeOrdenamiento(e.CommandName), pnRpOrdenTrabajo.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub


    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno el encabezado de Número de Orden de Trabajo
    ''' , carga la lista de registros en orden relacionado a la columna seleccionada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Jairo Alfaro Magnan</author>
    ''' <creationDate>26/05/2017</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpOrdenTrabajo_Sort_Numero_Orden(sender As Object, e As CommandEventArgs)
        Dim vlc_expresionRegular As String

        vlc_expresionRegular = String.Format("REGEXP_SUBSTR({0}, {1})", Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_ORDEN_TRABAJO, Utilerias.OrdenesDeTrabajo.Constantes.EXPRESION_REGULAR_NUMERO_ORDEN_TRABAJO)
        Try
            CargarLista(ObtenerCondicionBusqueda, ObtenerExpresionDeOrdenamiento(vlc_expresionRegular, e.CommandName), pnRpOrdenTrabajo.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/11/2015</creationDate>
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
            Me.Session.Add("pvc_PantallaRetorno", "OrdenesDeTrabajo/Lst_OT_OrdenesTrabajoRecepcion.aspx")
            Response.Redirect("../Frm_OT_ConsultaTrazabilidad.aspx", False)
        Else
            Me.Session.Add("pvc_PantallaRetorno", "Lst_OT_OrdenesTrabajoRecepcion.aspx")
            Response.Redirect("Frm_OT_ConsultaTrazabilidadPreOrden.aspx", False)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/12/2015</creationDate>
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
        Response.Redirect(String.Format("Frm_OT_OrdenesTrabajoRecepcion.aspx"), False)
    End Sub

    Protected Sub ibAsignarNumeroOt_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("Frm_OT_AsignaNumeroOT.aspx", False)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibConsultar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_Operacion", eOperacion.Consultar)
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_IdPreOrdenTrabajo", vlc_Llave(2))
            Me.Session.Add("pvn_EsPreOrden", vlc_Llave(3))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Paginador", Paginador)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect(String.Format("Frm_OT_OrdenesTrabajoRecepcion.aspx"), False)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los números del paginador
    ''' </summary>
    ''' <param name="pvn_PaginaSeleccionada"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
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
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpOrdenTrabajo_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpOrdenTrabajo.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        Dim vlo_IbEnviar As ImageButton
        Dim vlo_IbRevisar As HtmlTableRow
        Dim vlo_HiddenField As HiddenField

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

        If e.Item.FindControl("trTabla") IsNot Nothing Then
            vlo_IbRevisar = CType(e.Item.FindControl("trTabla"), HtmlTableRow)
            vlo_HiddenField = CType(e.Item.FindControl("hdfTipoOrden"), HiddenField)
            If vlo_HiddenField.Value = TipoOrden.EMERGENCIA Then
                vlo_IbRevisar.BgColor = "#F6D1C9"
                'FA5858
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
    ''' <creationDate>16/09/2015</creationDate>
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
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
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

            If Roles.IsUserInRole(Membership.GetUser.UserName, RolesSistema.OT_DIRECTOR_UNIDAD) Then
                vlc_Estado = EstadoOrden.ASIGNADA
            Else
                vlc_Estado = EstadoOrden.PENDIENTE_REVISION_DIRECTOR
            End If

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
    ''' Evento que se ejecuta cuando se da click sobre la figura de asignar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibImprimir_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_EntParametroReporte As EntParametroReporte
        Dim vlo_ListaEntParametroReporte As New List(Of EntParametroReporte)
        Dim vlc_Llave As String()
        Dim vlo_DsOrdenTrabajo As Data.DataSet

        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("_")

            'configuracion de los reporte
            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Usuario"
            vlo_EntParametroReporte.Valor = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB)
            vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

            'configuracion de los reporte
            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Clave"
            vlo_EntParametroReporte.Valor = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB)
            vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

            'configuracion de los reporte
            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Condicion"
            vlo_EntParametroReporte.Valor = String.Format("{0} = {1} AND {2} = '{3}'", Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_UBICACION, vlc_Llave(0), Utilerias.OrdenesDeTrabajo.Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_ORDEN_TRABAJO, vlc_Llave(1))
            vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Orden"
            vlo_EntParametroReporte.Valor = " "
            'Utilerias.OrdenesDeTrabajo.Ordenamiento.ASCENDENTE
            vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

            Me.Session.Add("pvo_ListaEntParametroReporte", vlo_ListaEntParametroReporte)

            vlo_DsOrdenTrabajo = CargarOrdenTrabajoVista(vlc_Llave(0), vlc_Llave(1))

            If vlo_DsOrdenTrabajo.Tables(0).Rows(0)(Modelo.V_OT_UNION_ORDEN_PREORDEN.CATEG_REQUIERE_FICHA_TECNICA).ToString = "1" Then
                'configuracion de los reporte
                vlo_EntParametroReporte = New EntParametroReporte
                vlo_EntParametroReporte.Nombre = "pvc_UsuarioEjecuta"
                vlo_EntParametroReporte.Valor = String.Format("{0} {1} {2}", Me.Empleado.NOMBRE, Me.Empleado.APELLIDO1, Me.Empleado.APELLIDO2)
                vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)
                ScriptManager.RegisterStartupScript(Me, GetType(String), "redirect", String.Format("window.open('../../Controles/Frm_OT_ManejoReportes.aspx?pvc_RutaBase={0}&pvc_NombreReporte={1}&pvc_FormatoReporte={2}', 'ticker', 'toolbar=no,menubar=no,location=no, scrollbars=YES,scroll=YES');", Utilerias.OrdenesDeTrabajo.Reportes.RUTA_BASE, Utilerias.OrdenesDeTrabajo.Reportes.RPT_OT_FICHA_TECNICA, FORMATO_REPORTE.PDF), True)
            Else
                ScriptManager.RegisterStartupScript(Me, GetType(String), "redirect", String.Format("window.open('../../Controles/Frm_OT_ManejoReportes.aspx?pvc_RutaBase={0}&pvc_NombreReporte={1}&pvc_FormatoReporte={2}', 'ticker', 'toolbar=no,menubar=no,location=no, scrollbars=YES,scroll=YES');", Utilerias.OrdenesDeTrabajo.Reportes.RUTA_BASE, Utilerias.OrdenesDeTrabajo.Reportes.RPT_OT_BOLETA_ORDEN_TRABAJO_CARTA, FORMATO_REPORTE.PDF), True)
            End If

            If vlc_Llave(2) = EstadoOrden.PARA_IMPRESION Then
                Modificar(vlc_Llave(0), vlc_Llave(1))
            End If
            Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)

        Catch ex As Exception
            Throw
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
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            UltimaCondicionBusqueda = String.Empty
            Me.Paginador = 0
            Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>17/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub HtmlAnchor_Click(sender As Object, e As EventArgs)
        Dim vlc_IdAnchor As String

        Try

            ''Tipo de orden = 0 es para emergencia
            ''Tipo de orden = 1 es para ordinaria

            If Me.rbtEmergencia.Checked Or Me.rbtEmergenciaFiltro.Checked Then
                Response.Redirect(String.Format("Frm_OT_OrdenesTrabajoRecepcion.aspx?pvn_Operacion={0}&pvn_TipoOrden={1}", 1, 0), False)
            End If

            If Me.rbtOrdinaria.Checked Or Me.rbtOrdinariaFiltro.Checked Then
                Response.Redirect(String.Format("Frm_OT_OrdenesTrabajoRecepcion.aspx?pvn_Operacion={0}&pvn_TipoOrden={1}", 1, 1), False)
            End If

            vlc_IdAnchor = CType(sender, HtmlAnchor).ID
            Select Case vlc_IdAnchor
                Case "lnkNuevoRegistro"
                    WebUtils.RegistrarScript(Me, "SeleccionDeTipo", "mostrarAlertaAdvertencia('Debe seleccionar el tipo de orden de trabajo (Emergencia/Ordinaria)');")
                Case "lnkNuevoRegistro2"
                    WebUtils.RegistrarScript(Me, "SeleccionDeTipo", "mostrarAlertaAdvertenciaFiltro('Debe seleccionar el tipo de orden de trabajo (Emergencia/Ordinaria)');")
            End Select

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
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaRegistroBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroBorrado", "mostrarAlertaRegistroBorrado();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaRegistroNoBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroNoBorrado", "mostrarAlertaRegistroNoBorrado();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
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
               String.Format("{0} LIKE '%{1}%'", Modelo.OTM_CATEGORIA_SERVICIO.ESTADO, Estado.ACTIVO),
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
    ''' Método CargarComboEstado
    ''' Se comunica con servicio web para obtener los diferentes estados
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboEstado()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlFiltroEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTC_ESTADO_ORDEN_TRABAJO_ListarRegistrosLista(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} <> '{1}'", Modelo.V_OTC_ESTADO_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO, EstadoOrden.BORRADA),
               String.Empty,
               False,
               0,
               0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlFiltroEstado
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTC_ESTADO_ORDEN_TRABAJO.DESCRIPCION
                    .DataValueField = Modelo.OTC_ESTADO_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO
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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboTipoOrden()
        Me.ddlTipoOrden.Items.Clear()
        Me.ddlTipoOrden.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
        Me.ddlTipoOrden.Items.Add(New ListItem("Ordinaria", TipoOrden.ORDINARIA))
        Me.ddlTipoOrden.Items.Add(New ListItem("Emergencia", TipoOrden.EMERGENCIA))
        Me.ddlTipoOrden.Items.Add(New ListItem("Preventiva", TipoOrden.PREVENTIVO))
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboSectorTaller()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlSectorTaller.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_SECTOR_TALLER_ListarRegistrosLista(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} LIKE '%{1}%'", Modelo.V_OTM_SECTOR_TALLERLST.ESTADO, Estado.ACTIVO),
               String.Empty,
               False,
               0,
               0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlSectorTaller
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE
                    .DataValueField = Modelo.V_OTM_SECTOR_TALLERLST.ID_SECTOR_TALLER
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
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvc_Condicion">parámetro con la condicion de búsqueda</param>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarVOtUnionOrdenPreorden(
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
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerExpresionDeOrdenamiento(Modelo.V_OT_UNION_ORDEN_PREORDEN.TIPO_ORDEN_TRABAJO)
        End If

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntDatosPaginacion =
                vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerDatosPaginacionVOtUnionOrdenPreorden(
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                     pvc_Condicion,
                     pvc_Orden,
                     CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpOrdenTrabajo.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas


                '                CargarLista(pvc_Condicion, pvc_Orden, 1)

                If Me.Paginador > 0 Then
                    CargarLista(pvc_Condicion, pvc_Orden, Me.Paginador)
                Else
                    CargarLista(pvc_Condicion, pvc_Orden, 1)
                End If

                Me.pnRpOrdenTrabajo.Dibujar()
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de ordenes {0}", vlo_EntDatosPaginacion.TotalRegistros)
            Else
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

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Función encargada de comunicarse con el  servicio web y proceder a borrar el registro
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvn_IdPreOrdenTrabajo"></param>
    ''' <returns>Si retorna un número mayor a 0 quiere decir que la operacion se realizo con éxito</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
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
    ''' obtiene la condicion de busqueda
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroEstado.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO, Me.ddlFiltroEstado.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO, Me.ddlFiltroEstado.SelectedValue)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlCategoriaServicio.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_CATEGORIA_SERVICIO, Me.ddlCategoriaServicio.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_CATEGORIA_SERVICIO, Me.ddlCategoriaServicio.SelectedValue)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtNumOrden.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_ORDEN_TRABAJO, Me.txtNumOrden.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_ORDEN_TRABAJO, Me.txtNumOrden.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtNumOrdenPdago.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_UNION_ORDEN_PREORDEN.NUMERO_ORDEN, Me.txtNumOrdenPdago.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_UNION_ORDEN_PREORDEN.NUMERO_ORDEN, Me.txtNumOrdenPdago.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlTipoOrden.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OT_UNION_ORDEN_PREORDEN.TIPO_ORDEN_TRABAJO, Me.ddlTipoOrden.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OT_UNION_ORDEN_PREORDEN.TIPO_ORDEN_TRABAJO, Me.ddlTipoOrden.SelectedValue)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlSectorTaller.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_SECTOR_TALLER, Me.ddlSectorTaller.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_SECTOR_TALLER, Me.ddlSectorTaller.SelectedValue)
            End If
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_UBICACION, Me.AutorizadoUbicacion.IdUbicacionAdministra)
        Else
            vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_UNION_ORDEN_PREORDEN.ID_UBICACION, Me.AutorizadoUbicacion.IdUbicacionAdministra)
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} <> '{1}'", Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO, EstadoOrden.BORRADA)
        Else
            vlc_Condicion = String.Format("{0} AND {1} <> '{2}'", vlc_Condicion, Modelo.V_OT_UNION_ORDEN_PREORDEN.ESTADO_ORDEN_TRABAJO, EstadoOrden.BORRADA)
        End If

        If UltimaCondicionBusqueda = String.Empty Then
            UltimaCondicionBusqueda = vlc_Condicion
        Else
            Return UltimaCondicionBusqueda
        End If

        Return vlc_Condicion
    End Function

    ''' <summary>
    ''' funcion encargada de realizar los oredenamientos segun los valores brindados
    ''' </summary>
    ''' <param name="pvc_Columna"> numero de la columna para el ordenamiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2015</creationDate>
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

    Private Function ObtenerExpresionDeOrdenamiento(pvc_ColumnaPrincipal As String, pvc_ColumnaSecundaria As String) As String
        If String.IsNullOrWhiteSpace(UltimoSortColumn) OrElse pvc_ColumnaPrincipal.CompareTo(UltimoSortColumn) <> 0 Then
            UltimoSortColumn = pvc_ColumnaPrincipal
            UltimoSortDirection = SortDirection.Ascending
        Else
            If UltimoSortDirection = SortDirection.Ascending Then
                UltimoSortDirection = SortDirection.Descending
            Else
                UltimoSortDirection = SortDirection.Ascending
            End If
        End If

        UltimoSortColumnSecundario = pvc_ColumnaSecundaria

        '{0}: Columna principal para ordenamiento
        '{1}: Dirección del ordenamiento
        '{2}: Columna secundaria para ordenamiento
        UltimoSortExpression = String.Format("{0} {1}, {2} {1}", UltimoSortColumn, IIf(UltimoSortDirection = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE), UltimoSortColumnSecundario)
        Return UltimoSortExpression
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>17/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Asignar(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttOrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo))

            If Roles.IsUserInRole(Membership.GetUser.UserName, RolesSistema.OT_DIRECTOR_UNIDAD) Then
                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.ASIGNADA
            Else
                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PENDIENTE_REVISION_DIRECTOR
            End If

            vlo_EntOttOrdenTrabajo.Usuario = Me.Usuario.UserName

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttOrdenTrabajo) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Administra el proceso para modificar el estado de la orden de trabajo
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>23/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOttOrdenTrabajo = ConstruirRegistro(pvn_IdUbicacion, pvc_IdOrdenTrabajo)

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ModificarRegistroPDAGO(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttOrdenTrabajo) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Funcion encargada de contruir el registro  de la orden de trabajo
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>23/09/2015</creationDate>
    ''' <changeLog>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>01/02/2016</creationDate>
    ''' <change>Cambio del estado a EN EVALUACION para las ordenes que ya fueron impresas</change>
    ''' </changeLog>
    Private Function ConstruirRegistro(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String) As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo
        Dim vlo_EntOttOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntOttOrdenTrabajo = New Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo
            vlo_EntOttOrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo))

            vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = Utilerias.OrdenesDeTrabajo.EstadoOrden.EN_EVALUACION
            vlo_EntOttOrdenTrabajo.Usuario = Me.Usuario.UserName

            Return vlo_EntOttOrdenTrabajo
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' retorna la ubicacion en la que sta autorizado el usuario 
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/10/2015</creationDate>
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


    ''' <summary>
    ''' carga la vista para lista
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gomez</author>
    ''' <creationDate>18/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarOrdenTrabajoVista(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String) As Data.DataSet
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo),
                String.Empty,
                False,
                0,
                0)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionario(pvn_IdPersonal As String) As WsrEU_Curriculo.EntEmpleados
        Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("ID_PERSONAL = '{0}'", pvn_IdPersonal))
        Catch ex As Exception
            Throw
        Finally
            If vlo_WsEU_Curriculo IsNot Nothing Then
                vlo_WsEU_Curriculo.Dispose()
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
    ''' <creationDate>25/04/2016</creationDate>
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
