Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Frm_ExpedienteTecnico
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
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
    ''' Propiedad para el año de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Anio As Integer
        Get
            Return CType(ViewState("Anio"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Anio") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
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
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdOrdenTrabajo As String
        Get
            Return CType(ViewState("IdOrdenTrabajo"), String)
        End Get
        Set(value As String)
            ViewState("IdOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property OrdenTrabajoMadre As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajoMadre"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajoMadre") = value
        End Set
    End Property

    ''' <summary>
    ''' Contador para tabs
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property ContadorTaps As Integer
        Get
            Return CType(ViewState("ContadorTaps"), Integer)
        End Get
        Set(value As Integer)
            ViewState("ContadorTaps") = value
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
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                LeerParametros()
                CargarOrdenTrabajoMadre(Me.IdUbicacion, Me.IdOrdenTrabajo)
                CargarOrdenesTrabajoHijas()
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click en el link button de ordenes de madre
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkOrdenMadre_Click(sender As Object, e As EventArgs) Handles lnkOrdenMadre.Click
        Try
            Me.IdUbicacion = Me.OrdenTrabajoMadre.IdUbicacion
            Me.IdOrdenTrabajo = Me.OrdenTrabajoMadre.IdOrdenTrabajo
            Me.Anio = Me.OrdenTrabajoMadre.Anno
            InicializarFormulario()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    '''evento que se ejecuta cuando se da click sobre alguno de los registros de ordenes hijas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpOrdenTrabajoHija_Command(sender As Object, e As CommandEventArgs)
        Dim vlc_LLave As String()

        Try
            vlc_LLave = e.CommandArgument.ToString.Split("¬")

            Me.IdUbicacion = vlc_LLave(0)
            Me.IdOrdenTrabajo = vlc_LLave(1)
            Me.Anio = vlc_LLave(2)
            InicializarFormulario()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da carga cada uno de los elementos del repeater de titulos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpListaTapsTitulos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpListaTapsTitulos.ItemDataBound
        Dim vlo_HtmlAnchor As HtmlAnchor

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_HtmlAnchor = CType(e.Item.FindControl("cuerpoTabPanel"), HtmlAnchor)
            vlo_HtmlAnchor.HRef = String.Format("#cphContenidoFormulario_cphFormulario_rpListaTapsContenidos_cuerpoTabPanel_{0}", Me.ContadorTaps.ToString)
            Me.ContadorTaps = Me.ContadorTaps + 1
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da carga cada uno de los elementos del repeater de contenidos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpListaTapsContenidos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpListaTapsContenidos.ItemDataBound
        Dim vlo_WebUserControl As Controles_wuc_OT_Expediente_Tecnico

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_WebUserControl = CType(e.Item.FindControl("wucExpedienteTecnico"), Controles_wuc_OT_Expediente_Tecnico)
            vlo_WebUserControl.IdUbicacion = Me.IdUbicacion
            vlo_WebUserControl.IdOrdenTrabajo = Me.IdOrdenTrabajo
            vlo_WebUserControl.Inicializar()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click en el boton de guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            Procesar()
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

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar los diferentess componentes del formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.ContadorTaps = 0
        InicializarControl()
        CargarTapsEtapas()
    End Sub

    ''' <summary>
    ''' Lee los parametros 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.Anio = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        Me.Session.Add("pvn_IdUbicacion", Me.IdUbicacion)
        Me.Session.Add("pvc_IdOrdenTrabajo", Me.IdOrdenTrabajo)
        Me.Session.Add("pvn_Anno", Me.Anio)
    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub InicializarControl()
        'Asignación de Datos para generar el web user control de la información general
        ctrl_InfoGeneral.Anno = Me.Anio
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.IdUbicacion.ToString
        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()
    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenTrabajoMadre(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.OrdenTrabajoMadre = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

        If Not Me.OrdenTrabajoMadre.Existe Then
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        Else
            Me.lnkOrdenMadre.Text = Me.OrdenTrabajoMadre.IdOrdenTrabajo
        End If
    End Sub

    ''' <summary>
    ''' Carga las ordenes de trabajo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenesTrabajoHijas()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlc_Condicion As String = String.Empty

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlc_Condicion = String.Format("{0} = {1} AND {2} = '{3}' AND {4} = 'HIJ'", Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION_MADRE, Me.IdUbicacion, Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO_MADRE, Me.IdOrdenTrabajo, Modelo.V_OTT_ORDEN_TRABAJOLST.PARENTESCO)

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlc_Condicion,
                String.Empty,
                False,
                0,
                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpOrdenTrabajoHija
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With
            End If

        Catch ex As Exception
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Carga y conturye los tabs de etapas
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarTapsEtapas()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet
        Dim vln_CantidadAdjuntos As Integer = 0

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials


        Try

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_ETAPA_ORDEN_TRABAJO_ListarRegistros(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                            String.Format("{0} LIKE '%{1}%'", Modelo.OTM_ETAPA_ORDEN_TRABAJO.ESTADO, Estado.ACTIVO),
                            String.Format("{0} {1}", Modelo.OTM_ETAPA_ORDEN_TRABAJO.ORDEN, Ordenamiento.ASCENDENTE),
                            False,
                            0,
                            0)

            For Each vlo_Fila In vlo_DsDatos.Tables(0).Rows
                vln_CantidadAdjuntos = CargarCantidadAdjuntosOrdenTrabajoaEtapa(Me.IdUbicacion, Me.IdOrdenTrabajo, CType(vlo_Fila(Modelo.OTM_ETAPA_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO), Integer))
                If Not (vln_CantidadAdjuntos > 0) Then
                    vlo_Fila.Delete()
                End If
                vln_CantidadAdjuntos = 0
            Next

            vlo_DsDatos.AcceptChanges()

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpListaTapsTitulos
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With

                With Me.rpListaTapsContenidos
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With
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
    ''' Procesa los datos para la actualización de expediente tecnico
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Procesar()
        Dim vlo_DsAdjuntos As Data.DataSet
        Dim vlo_WebUserControl As Controles_wuc_OT_Expediente_Tecnico
        Dim vlo_RepeaterAdjuntos As Repeater
        Dim vlo_CheckBox As CheckBox
        Dim vlo_HiddenField As HiddenField
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vln_Resultado As Integer

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsAdjuntos = CargarAdjuntosOrdenTrabajo()
            vlo_DsAdjuntos.Tables(0).PrimaryKey = New Data.DataColumn() {vlo_DsAdjuntos.Tables(0).Columns(Modelo.V_OT_ADJUNTO.ID_ADJUNTO_ORDEN_TRABAJO)}

            For Each item In Me.rpListaTapsContenidos.Items
                vlo_WebUserControl = CType(item.FindControl("wucExpedienteTecnico"), Controles_wuc_OT_Expediente_Tecnico)
                vlo_RepeaterAdjuntos = vlo_WebUserControl.RetornaRepeater()

                For Each itemAdjunto In vlo_RepeaterAdjuntos.Items
                    vlo_CheckBox = CType(itemAdjunto.FindControl("chkArchivo"), CheckBox)
                    vlo_HiddenField = CType(itemAdjunto.FindControl("hdfIdAdjunto"), HiddenField)

                    If vlo_CheckBox.Checked Then
                        vlo_DsAdjuntos.Tables(0).Rows.Find(New Object() {vlo_HiddenField.Value})(Modelo.V_OT_ADJUNTO.EXPEDIENTE_TECNICO) = "1"
                    Else
                        vlo_DsAdjuntos.Tables(0).Rows.Find(New Object() {vlo_HiddenField.Value})(Modelo.V_OT_ADJUNTO.EXPEDIENTE_TECNICO) = "0"
                    End If
                Next
            Next

            vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ActualizarExpedienteTecnico(
                       ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                       ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                       vlo_DsAdjuntos)

            If vln_Resultado > 0 Then
                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
            Else
                MostrarAlertaError("No ha sido posible almacenar la información del expediente Técnico")
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
    ''' Retorna la cantida de ajunto que posee una orden especifica para una etapa determinada
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <param name="pvn_IdEtapaOrdenTrabajo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Function CargarCantidadAdjuntosOrdenTrabajoaEtapa(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_IdEtapaOrdenTrabajo As Integer) As Integer
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return CType(vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerFnOtCantidadAdjuntosEtapa(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        pvn_IdUbicacion, pvc_IdOrdenTrabajo, pvn_IdEtapaOrdenTrabajo), Integer)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' retorna un data set con los documentos adjuntos de la OT, de todas las diferentes etapas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarAdjuntosOrdenTrabajo() As Data.DataSet
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarVOtAdjunto(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OT_ADJUNTO.ID_UBICACION, Me.IdUbicacion, Modelo.V_OT_ADJUNTO.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo),
                        String.Empty,
                        False, 0, 0)

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
