Imports Utilerias.OrdenesDeTrabajo

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
''' <author>César Bermudez Garcia</author>
''' <creationDate>06/04/2016</creationDate>
''' <changeLog></changeLog>
Partial Class OrdenesDeTrabajo_Lst_OT_RevisionGeneral
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la ultima condicion de busqueda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
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
    ''' Propiedad para la ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
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
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
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
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
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
    ''' usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
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
    ''' ubicacion del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
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
    ''' Sector o taller donde el usuario actual está asignado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property SectorTaller As Wsr_OT_Catalogos.EntOtmSectorTaller
        Get
            Return CType(ViewState("SectorTaller"), Wsr_OT_Catalogos.EntOtmSectorTaller)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmSectorTaller)
            ViewState("SectorTaller") = value
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
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                InicializarListado()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
        Me.pnRpOrdenTrabajo.Dibujar()
    End Sub

    ''' <summary>
    ''' Se ejecuta para mostrar la trazabilidad con los parámetros especificados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkTrazabilidad_Command(sender As Object, e As CommandEventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = e.CommandArgument.Split("¬")

            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvc_PantallaRetorno", "Lst_OT_RevisionGeneral.aspx")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("Frm_OT_ConsultaTrazabilidad.aspx", False)
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados de la 
    ''' tabla del listado, carga la lista de registros en orden relacionado a la columna seleccionada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez</author>
    ''' <creationDate>06/04/2016</creationDate>
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
    ''' Redirige a la pantalla de Ante Proyecto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAnteproyecto_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()
        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_IdSectorTaller", vlc_Llave(2))
            Me.Session.Add("pvn_Anno", vlc_Llave(3))
            Me.Session.Add("pvn_Operacion", eOperacion.Consultar)
            Me.Session.Add("pvn_Regresar", "Lst_OT_RevisionGeneral.aspx")
            CargarEncargado(vlc_Llave(0), vlc_Llave(1), vlc_Llave(2))

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect(String.Format("Frm_OT_Anteproyecto.aspx"), False)
    End Sub

    ''' <summary>
    ''' Redirige a otra página para registrar el análisis de la viabilidad técnica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnVT_Click(sender As Object, e As EventArgs)
        Dim vlc_Llave As String()
        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_Anno", vlc_Llave(2))
            Me.Session.Add("pvn_IdSectorTaller", vlc_Llave(3))
            Me.Session.Add("pvn_Operacion", eOperacion.Consultar)
            Me.Session.Add("pvn_Regresar", "Lst_OT_RevisionGeneral.aspx")
            CargarEncargado(vlc_Llave(0), vlc_Llave(1), vlc_Llave(3))
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect(String.Format("Frm_OT_AnalisisViabilidadTecnica.aspx"), False)
    End Sub

    ''' <summary>
    ''' Evento para mostrar información de la OT
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkNumOt_Click(sender As Object, e As EventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, LinkButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_Operacion", eOperacion.Consultar)
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvc_UltimaCondicionBusqueda", UltimaCondicionBusqueda)
            Me.Session.Add("pvn_Regresar", "Lst_OT_RevisionGeneral.aspx")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect(String.Format("Frm_OT_OrdenTrabajo.aspx"), False)
    End Sub

    ''' <summary>
    ''' Redirecciona al formulario de elaboración de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub imgElaboracionPresupuesto_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_Anno", vlc_Llave(2))
            Me.Session.Add("pvn_Operacion", eOperacion.Consultar)
            Me.Session.Add("pvn_Regresar", "Lst_OT_RevisionGeneral.aspx")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("Frm_OT_ElaboracionPresupuesto.aspx", False)
    End Sub

    ''' <summary>
    ''' Manda a llamar al formulario de planos para la elaboracion de esta etapa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub imgPlanos_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave() As String
        Try

            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")
            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_IdSectorTaller", vlc_Llave(2))
            Me.Session.Add("pvn_Anno", vlc_Llave(3))
            Me.Session.Add("pvn_Operacion", eOperacion.Consultar)
            Me.Session.Add("pvn_Regresar", "Lst_OT_RevisionGeneral.aspx")
            CargarEncargado(vlc_Llave(0), vlc_Llave(1), vlc_Llave(2))

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("Frm_OT_Planos.aspx", False)
    End Sub

    ''' <summary>
    ''' Redirecciona al formulario de expediente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibExpediente_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlc_Llave As String()

        Try
            vlc_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            Me.Session.Add("pvn_IdUbicacion", vlc_Llave(0))
            Me.Session.Add("pvc_IdOrdenTrabajo", vlc_Llave(1))
            Me.Session.Add("pvn_Anno", vlc_Llave(2))
            Me.Session.Add("pvn_Operacion", eOperacion.Consultar)
            Me.Session.Add("pvn_Regresar", "Lst_OT_RevisionGeneral.aspx")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect("Frm_OT_Expediente.aspx", False)
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inicializa el listado 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarListado()
        Try
            Me.Usuario = New UsuarioActual
            Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

            If Me.AutorizadoUbicacion.Existe Then
                If Roles.IsUserInRole(Membership.GetUser.UserName, RolesSistema.OT_COORDINADOR_DISENIO) Then
                    'Obtener el sector o taller en que el usuario actual está asignado
                    CargarTallerSector()
                    CargarListaEstados()
                    Buscar(String.Format("{0} = {1} AND {2} = {3}",
                                         Modelo.V_OTT_ORDEN_TRABAJOLST.POSEE_FICHA_TECNICA,
                                         FichaTecnica.REQUIERE_FICHA_TECNICA,
                                         Modelo.V_OTT_ORDEN_TRABAJOLST.VIABILIDAD_TECNICA, 1), String.Empty)

                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee el rol necesario para ingresar a esta página.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ninguna ubicación autorizada, contactar con el administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de realizar la busqueda de registros segun los datos de 
    ''' condicion y orden de búsqueda
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntDatosPaginacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerDatosPaginacionVOttOrdenTrabajolst(
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                     pvc_Condicion,
                     pvc_Orden,
                     CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpOrdenTrabajo.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas


                CargarLista(pvc_Condicion, pvc_Orden, 1)


                Me.pnRpOrdenTrabajo.Dibujar()
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de ordenes {0}", vlo_EntDatosPaginacion.TotalRegistros)
                Me.rpOrdenTrabajo.Visible = True
                Me.pnRpOrdenTrabajo.Visible = True
            Else


                Me.lblCantidadDeRegistros.Visible = False
                Me.lblCantidadDeRegistros.Text = String.Empty
                Me.rpOrdenTrabajo.Visible = False
                Me.pnRpOrdenTrabajo.Visible = False
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
    ''' Ejecuta eljavascript para mostrar una alerta cuando no existen datos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "mostrarAlertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    ''' <summary>
    ''' Ejecuta un llamado para obtener estados de orden de trabajo desde la base de datos y cargar la lista de filtros con ella.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaEstados()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlFiltroEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
            '{0}: Columna ESTADO_ORDEN_TRABAJO
            '{1}: Evaluación Preliminar Pendiente
            '{2}: Evaluación Preliminar Evaluación
            '{3}: Evaluación Preliminar Revisión Coordinador
            '{4}: Evaluación Preliminar Devuelta por Coordinador
            '{5}: Evaluación Preliminar Aprobada por Coordinador
            '{6}: Evaluación Preliminar Revisión Jefatura
            '{7}: Evaluación Preliminar Aprobada Jefatura
            '{8}: Evaluación Preliminar Devuelta por Jefatura
            '{9}: Pendiente Respuesta del Solicitante
            '{10}: En Anteproyecto 
            '{11}: Anteproyecto Pendiente Revisión del solicitante
            '{12}: Anteproyecto Devuelto por el solicitante
            '{13}: Anteproyecto Aprobado por el solicitante


            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTC_ESTADO_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}' OR {0} = '{2}' OR {0} = '{3}' OR {0} = '{4}' OR {0} = '{5}' OR {0} = '{6}' OR {0} = '{7}' OR {0} = '{8}' OR {0} = '{9}' OR {0} = '{10}' OR {0} = '{11}' OR {0} = '{12}' OR {0} = '{13}' OR {0} = '{14}' OR {0} = '{15}'",
                        Modelo.OTC_ESTADO_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO, EstadoOrden.EVALUACION_PRELIMINAR_PENDIENTE,
                        EstadoOrden.EVALUACION_PRELIMINAR_EVALUACION, EstadoOrden.EVALUACION_PRELIMINAR_REVISION_COORD, EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_COORD,
                        EstadoOrden.EVALUACION_PRELIMINAR_APROBADA_COORD, EstadoOrden.EVALUACION_PRELIMINAR_REVISION_JEFATURA, EstadoOrden.EVALUACION_PRELIMINAR_APROBADA_JEFATURA,
                        EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_JEFATURA, EstadoOrden.PENDIENTE_RESPUESTA_SOLICITANTE, EstadoOrden.EN_ANTEPROYECTO,
                        EstadoOrden.ANTEPROYECTO_PENDIENTE_REVISION_SOLICITANTE, EstadoOrden.ANTEPROYECTO_DEVUELTO_SOLICITANTE,
                        EstadoOrden.ANTEPROYECTO_APROBADO_SOLICITANTE, EstadoOrden.ELABORACION_DE_PLANOS, EstadoOrden.ELABORACION_PRESUPUESTO), String.Empty, False, 1, 0)


            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlFiltroEstado
                    .DataSource = vlo_DsDatos
                    .DataValueField = Modelo.OTC_ESTADO_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO
                    .DataTextField = Modelo.OTC_ESTADO_ORDEN_TRABAJO.DESCRIPCION
                    .DataBind()
                End With
            End If

        Catch ex As Exception

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
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarRegistrosLista(
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
                Me.rpOrdenTrabajo.Visible = True
                Me.pnRpOrdenTrabajo.Visible = True
            Else
                With Me.rpOrdenTrabajo
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With
                Me.rpOrdenTrabajo.Visible = False
                Me.pnRpOrdenTrabajo.Visible = False
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
    ''' Carga em la session el usuario encargado de este proyecto si existe
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEncargado(pvn_idUbicacion As Integer, pvc_idOrdenTrabajo As String, pvn_idSectorTaller As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttOperarioOrdenTrab As Wsr_OT_OrdenesDeTrabajo.EntOttOperarioOrdenTrab


        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0}: Columna ID_UBICACION
            '{1}: numero de la ubicacion de la OT
            '{2}: Columna ID_SECTOR_TALLER
            '{3}: numero de sector taller de la OT
            '{4}: Columna ID_ORDEN_TRABAJO
            '{5}: id de la orden de trabajo
            '{6}: Columnna CARGO
            '{7}: cargo de encargado

            vlo_EntOttOperarioOrdenTrab = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}' AND {6} = '{7}'",
                              Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, pvn_idUbicacion,
                              Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER, pvn_idSectorTaller,
                              Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo,
                              Modelo.OTT_OPERARIO_ORDEN_TRAB.CARGO, Cargo.ENCARGADO))

            If vlo_EntOttOperarioOrdenTrab.Existe Then
                Me.Session.Add("pvn_IdEncargado", vlo_EntOttOperarioOrdenTrab.NumEmpleado)
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
    ''' carga la ubicacion 
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarAutorizadoUbicacion(pvn_NumEmpleado As Integer) As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Dim Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        Ws_OT_Catalogos.Timeout = -1
        Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return Ws_OT_Catalogos.OTM_AUTORIZADO_UBICACION_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTM_AUTORIZADO_UBICACION.NUM_EMPLEADO, pvn_NumEmpleado))
        Catch ex As Exception
            Throw
        Finally
            If Ws_OT_Catalogos IsNot Nothing Then
                Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga el taller o sector del usuario actualmente en sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarTallerSector()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            SectorTaller = vlo_Ws_OT_Catalogos.OTM_SECTOR_TALLER_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {2} OR {1} = {2}", Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_COORDINADOR,
                    Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_SUSTITUTO, Me.Usuario.NumEmpleado))

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' construye la condicion de busqueda
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroNumOrden.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO, Me.txtFiltroNumOrden.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO, Me.txtFiltroNumOrden.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroNombreProyecto.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OTT_ORDEN_TRABAJOLST.NOMBRE_PROYECTO, Me.txtFiltroNombreProyecto.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OTT_ORDEN_TRABAJOLST.NOMBRE_PROYECTO, Me.txtFiltroNombreProyecto.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroEncargado.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OTT_ORDEN_TRABAJOLST.FECHA_HORA_SOLICITA, Me.txtFiltroEncargado.Text)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OTT_ORDEN_TRABAJOLST.FECHA_HORA_SOLICITA, Me.txtFiltroEncargado.Text)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroEstado.SelectedValue) Then
            If Not String.IsNullOrWhiteSpace(Me.ddlFiltroEstado.SelectedValue) Then
                vlc_Condicion = String.Format("{0} = '{1}' ", Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO, Me.ddlFiltroEstado.SelectedValue)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}')", vlc_Condicion, Modelo.V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO, Me.ddlFiltroEstado.SelectedValue)
            End If
        End If

        Return vlc_Condicion
    End Function

    ''' <summary>
    ''' funcion encargada de realizar los oredenamientos segun los valores brindados
    ''' </summary>
    ''' <param name="pvc_Columna"> numero de la columna para el ordenamiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
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

#End Region

End Class
