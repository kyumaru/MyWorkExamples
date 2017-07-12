Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports Wsr_SDP_ReportServer
Imports Utilerias.GeneradorDeReportes

Partial Class OrdenesDeTrabajo_Frm_OT_DesicionInicial
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Prpoiedad para cargar datos del empleado en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Empleado As WsrEU_Curriculo.EntEmpleados
        Get
            Return CType(ViewState("Empleado"), WsrEU_Curriculo.EntEmpleados)
        End Get
        Set(value As WsrEU_Curriculo.EntEmpleados)
            ViewState("Empleado") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
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
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
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
    ''' <creationDate>31/03/2016</creationDate>
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
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property OrdenTrabajo As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DesicionInicial As EntOttDesicionInicial
        Get
            Return CType(ViewState("DesicionInicial"), EntOttDesicionInicial)
        End Get
        Set(value As EntOttDesicionInicial)
            ViewState("DesicionInicial") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de rubros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsRubros As Data.DataSet
        Get
            Return CType(ViewState("DsRubros"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsRubros") = value
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
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.Empleado = CargarFuncionario(Me.Usuario.UserName)
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el boton de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
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

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el boton de imprimir reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        Dim vlo_EntParametroReporte As EntParametroReporte
        Dim vlo_ListaEntParametroReporte As New List(Of EntParametroReporte)

        Try

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
            vlo_EntParametroReporte.Valor = String.Format("{0} = {1} AND {2} = '{3}'", Utilerias.OrdenesDeTrabajo.Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_UBICACION, Me.IdUbicacion, Utilerias.OrdenesDeTrabajo.Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo)
            vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_Orden"
            vlo_EntParametroReporte.Valor = " "
            vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

            vlo_EntParametroReporte = New EntParametroReporte
            vlo_EntParametroReporte.Nombre = "pvc_UsuarioEjecuta"
            vlo_EntParametroReporte.Valor = String.Format("{0} {1} {2}", Me.Empleado.NOMBRE, Me.Empleado.APELLIDO1, Me.Empleado.APELLIDO2)
            vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

            Me.Session.Add("pvo_ListaEntParametroReporte", vlo_ListaEntParametroReporte)
           
            ScriptManager.RegisterStartupScript(Me, GetType(String), "redirect", String.Format("window.open('../../Controles/Frm_OT_ManejoReportes.aspx?pvc_RutaBase={0}&pvc_NombreReporte={1}&pvc_FormatoReporte={2}', 'ticker', 'toolbar=no,menubar=no,location=no, scrollbars=YES,scroll=YES');", Utilerias.OrdenesDeTrabajo.Reportes.RUTA_BASE, Utilerias.OrdenesDeTrabajo.Reportes.RPT_OT_DESICION_INICIAL, FORMATO_REPORTE.PDF), True)

        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar el formulario segun la operacion a realizar
    ''' en caso de ser agregar carga el texo con "Agregar", en caso de ser modificar 
    ''' cargar el texto con "Modificar" y llama al método que obtiene los datos de la orden seleccionada segun el id 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        LeerParametros()
        CargarOrdenTrabajo(Me.IdUbicacion, Me.IdOrdenTrabajo)

        If Me.OrdenTrabajo.Existe Then
            CargaDatosEncabezado()
            CargarDesicionInicial(Me.IdUbicacion, Me.IdOrdenTrabajo)

            If Me.DesicionInicial.Existe Then
                Me.btnReporte.Enabled = True
                Me.ddlTipoObra.SelectedValue = Me.DesicionInicial.IdTipoObra
                Me.lblFecha.Text = Me.DesicionInicial.Fecha.ToString(Constantes.FORMATO_FECHA_UI)
                CombinarListaRubros()
            Else
                Me.btnReporte.Enabled = False
                CargarListaRubros()
            End If

        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    ''' <summary>
    ''' Lee los parametros guardados en la sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.Session.Add("pvn_IdUbicacion", Me.IdUbicacion)
        Me.Session.Add("pvc_IdOrdenTrabajo", Me.IdOrdenTrabajo)
    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">id de la ubicacion</param>
    ''' <param name="pvc_IdOrdenTrabajo">id de la orden</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenTrabajo(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
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
    End Sub

    ''' <summary>
    ''' Carga los datos del encabezado del formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargaDatosEncabezado()
        Try
            With Me.OrdenTrabajo
                Me.lblNombreProyecto.Text = .NombreProyecto
                Me.lblNumOT.Text = .IdOrdenTrabajo
                Me.lblFecha.Text = DateTime.Now.ToString(Constantes.FORMATO_FECHA_UI)
            End With

            CargarTiposObra()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Se carga el combo de  tipo de obra
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarTiposObra()
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlTipoObra.Items.Clear()
            Me.ddlTipoObra.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_TIPO_OBRA_ListarRegistros(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                String.Format("{0} = '{1}'", Modelo.V_OTM_TIPO_OBRA.ESTADO, Estado.ACTIVO),
                                String.Format("{0} ASC", Modelo.V_OTM_TIPO_OBRA.DESCRIPCION),
                                False,
                                0,
                                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlTipoObra
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_TIPO_OBRA.DESCRIPCION
                    .DataValueField = Modelo.V_OTM_TIPO_OBRA.ID_TIPO_OBRA
                    .DataBind()
                End With
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Se carga la desicion inicial del proyecto, determina si existe o no
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">id de la ubicacion</param>
    ''' <param name="pvc_IdOrdenTrabajo">id de la orden</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarDesicionInicial(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DesicionInicial = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DESICION_INICIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_DESICION_INICIAL.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_DESICION_INICIAL.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carla la lista de OTT_ORDN_TRAB_DEC_INICIAL, para verificar cuales son los rubros ya existente y añadirlos a la lista de rubroos en caso de que alguno este en estado inactivo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CombinarListaRubros()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsOrdenTrabDecInic As System.Data.DataSet
        Dim vlc_SubCondicion As String = String.Empty
        Dim vlc_Condicion As String = String.Empty
        Dim vlo_HiddenField As HiddenField
        Dim vlo_TextBox As TextBox
        Dim vlo_DrFilaFind As Data.DataRow

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsOrdenTrabDecInic = CargarOrdenTrabDecInicial()

            For Each vlo_Existente In vlo_DsOrdenTrabDecInic.Tables(0).Rows
                If String.IsNullOrWhiteSpace(vlc_SubCondicion) Then
                    vlc_SubCondicion = String.Format("{0}", vlo_Existente(Modelo.V_OTM_RUBRO_DECISION_INICIA.ID_RUBRO_DECISION_INICIA))
                Else
                    vlc_SubCondicion = String.Format("{0}, {1}", vlc_SubCondicion, vlo_Existente(Modelo.V_OTM_RUBRO_DECISION_INICIA.ID_RUBRO_DECISION_INICIA))
                End If
            Next

            vlo_DsOrdenTrabDecInic.Tables(0).PrimaryKey = New Data.DataColumn() {vlo_DsOrdenTrabDecInic.Tables(0).Columns(Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_RUBRO_DECISION_INICIA)}

            vlc_Condicion = String.Format("{0} = '{1}' OR {2} IN ({3})", Modelo.V_OTM_RUBRO_DECISION_INICIA.ESTADO, Estado.ACTIVO, Modelo.V_OTM_RUBRO_DECISION_INICIA.ID_RUBRO_DECISION_INICIA, vlc_SubCondicion)

            DsRubros = vlo_Ws_OT_Catalogos.OTM_RUBRO_DECISION_INICIA_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlc_Condicion,
                String.Format("{0} {1}", Modelo.OTM_RUBRO_DECISION_INICIA.ORDEN, Ordenamiento.ASCENDENTE),
                False,
                0,
                0)

            DsRubros.Tables(0).PrimaryKey = New Data.DataColumn() {DsRubros.Tables(0).Columns(Modelo.OTM_RUBRO_DECISION_INICIA.ID_RUBRO_DECISION_INICIA)}

            If DsRubros IsNot Nothing AndAlso DsRubros.Tables(0).Rows.Count > 0 Then
                With Me.rpRubro
                    .DataSource = DsRubros
                    .DataMember = DsRubros.Tables(0).TableName
                    .DataBind()
                End With
            End If

            For Each item In Me.rpRubro.Items
                vlo_HiddenField = CType(item.FindControl("hdfIdRubroDecInic"), HiddenField)
                vlo_DrFilaFind = vlo_DsOrdenTrabDecInic.Tables(0).Rows.Find(New Object() {vlo_HiddenField.Value})

                If Not vlo_DrFilaFind Is Nothing Then
                    vlo_TextBox = CType(item.FindControl("txtDescripcionRubro"), TextBox)
                    vlo_TextBox.Text = vlo_DrFilaFind(Modelo.OTT_ORDN_TRAB_DEC_INICIAL.VALOR)
                End If
            Next

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga la lista de rubros activos 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaRubros()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_RUBRO_DECISION_INICIA_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}'", Modelo.V_OTM_RUBRO_DECISION_INICIA.ESTADO, Estado.ACTIVO),
                String.Format("{0} {1}", Modelo.OTM_RUBRO_DECISION_INICIA.ORDEN, Ordenamiento.ASCENDENTE),
                False,
                0,
                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpRubro
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

            If vlo_DsDatos IsNot Nothing Then
                vlo_DsDatos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Procesa los datos para la creacion de la desicion inicial
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Procesar()
        Dim vlo_TextBox As TextBox
        Dim vlo_HiddenField As HiddenField
        Dim vlo_DescripcionRubroFaltante As Boolean = False
        Dim vlo_DsOrdenTrabDecInicial As Data.DataSet
        Dim vlo_DrFila As Data.DataRow
        Dim vlo_DrFilaRubro As Data.DataRow
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vln_Resultado As Integer

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.DesicionInicial.Existe Then

                vlo_DsOrdenTrabDecInicial = CargarEstruturaOrdenTrabDecInicial()

                Me.DesicionInicial.IdTipoObra = Me.ddlTipoObra.SelectedValue
                Me.DesicionInicial.Fecha = DateTime.Now

                For Each item In Me.rpRubro.Items
                    vlo_TextBox = CType(item.FindControl("txtDescripcionRubro"), TextBox)
                    vlo_HiddenField = CType(item.FindControl("hdfIdRubroDecInic"), HiddenField)
                    If vlo_TextBox.Text.Trim = String.Empty Then

                        vlo_DrFilaRubro = DsRubros.Tables(0).Rows.Find(New Object() {vlo_HiddenField.Value})

                        If vlo_DrFilaRubro(Modelo.OTM_RUBRO_DECISION_INICIA.ESTADO) = Estado.ACTIVO Then
                            vlo_DescripcionRubroFaltante = True
                            MostrarAlertaError(String.Format("El Rubro {0} no puede ser un valor vacio.", vlo_DrFilaRubro(Modelo.OTM_RUBRO_DECISION_INICIA.DESCRIPCION)))
                            Exit For
                        End If
                    End If

                    If vlo_TextBox.Text.Trim <> String.Empty Then
                        vlo_DrFila = vlo_DsOrdenTrabDecInicial.Tables(0).NewRow
                        vlo_DrFila.Item(vlo_DsOrdenTrabDecInicial.Tables(0).Columns(Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_UBICACION)) = Me.IdUbicacion
                        vlo_DrFila.Item(vlo_DsOrdenTrabDecInicial.Tables(0).Columns(Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_ORDEN_TRABAJO)) = Me.IdOrdenTrabajo
                        vlo_DrFila.Item(vlo_DsOrdenTrabDecInicial.Tables(0).Columns(Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_TIPO_OBRA)) = Me.ddlTipoObra.SelectedValue
                        vlo_DrFila.Item(vlo_DsOrdenTrabDecInicial.Tables(0).Columns(Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_RUBRO_DECISION_INICIA)) = vlo_HiddenField.Value
                        vlo_DrFila.Item(vlo_DsOrdenTrabDecInicial.Tables(0).Columns(Modelo.OTT_ORDN_TRAB_DEC_INICIAL.USUARIO)) = Me.Usuario.UserName
                        vlo_DrFila.Item(vlo_DsOrdenTrabDecInicial.Tables(0).Columns(Modelo.OTT_ORDN_TRAB_DEC_INICIAL.VALOR)) = vlo_TextBox.Text

                        vlo_DsOrdenTrabDecInicial.Tables(0).Rows.Add(vlo_DrFila)
                    End If
                Next

                If Not vlo_DescripcionRubroFaltante Then

                    vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDN_TRAB_DEC_INICIAL_ModificarDesicionInicialConDetalles(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        Me.DesicionInicial,
                        vlo_DsOrdenTrabDecInicial)

                    If vln_Resultado > 0 Then
                        WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                    Else
                        MostrarAlertaError("No ha sido posible almacenar la información de los registros")
                    End If
                End If

            Else

                vlo_DsOrdenTrabDecInicial = CargarEstruturaOrdenTrabDecInicial()

                Me.DesicionInicial = New EntOttDesicionInicial
                With Me.DesicionInicial
                    .IdUbicacion = Me.IdUbicacion
                    .IdOrdenTrabajo = Me.IdOrdenTrabajo
                    .IdTipoObra = Me.ddlTipoObra.SelectedValue
                    .Fecha = DateTime.Now
                    .Usuario = Me.Usuario.UserName
                End With

                For Each item In Me.rpRubro.Items
                    vlo_TextBox = CType(item.FindControl("txtDescripcionRubro"), TextBox)
                    vlo_HiddenField = CType(item.FindControl("hdfIdRubroDecInic"), HiddenField)
                    If vlo_TextBox.Text.Trim = String.Empty Then
                        vlo_DescripcionRubroFaltante = True
                        MostrarAlertaError("Todas las descripciones de los rubros son requeridas.")
                        Exit For
                    End If

                    vlo_DrFila = vlo_DsOrdenTrabDecInicial.Tables(0).NewRow
                    vlo_DrFila.Item(vlo_DsOrdenTrabDecInicial.Tables(0).Columns(Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_UBICACION)) = Me.IdUbicacion
                    vlo_DrFila.Item(vlo_DsOrdenTrabDecInicial.Tables(0).Columns(Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_ORDEN_TRABAJO)) = Me.IdOrdenTrabajo
                    vlo_DrFila.Item(vlo_DsOrdenTrabDecInicial.Tables(0).Columns(Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_TIPO_OBRA)) = Me.ddlTipoObra.SelectedValue
                    vlo_DrFila.Item(vlo_DsOrdenTrabDecInicial.Tables(0).Columns(Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_RUBRO_DECISION_INICIA)) = vlo_HiddenField.Value
                    vlo_DrFila.Item(vlo_DsOrdenTrabDecInicial.Tables(0).Columns(Modelo.OTT_ORDN_TRAB_DEC_INICIAL.USUARIO)) = Me.Usuario.UserName
                    vlo_DrFila.Item(vlo_DsOrdenTrabDecInicial.Tables(0).Columns(Modelo.OTT_ORDN_TRAB_DEC_INICIAL.VALOR)) = vlo_TextBox.Text

                    vlo_DsOrdenTrabDecInicial.Tables(0).Rows.Add(vlo_DrFila)
                Next

                If Not vlo_DescripcionRubroFaltante Then

                    vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDN_TRAB_DEC_INICIAL_InsertarDesicionInicialConDetalles(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        Me.DesicionInicial,
                        vlo_DsOrdenTrabDecInicial)

                    If vln_Resultado > 0 Then
                        WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                    Else
                        MostrarAlertaError("No ha sido posible almacenar la información de los registros")
                    End If
                End If
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
    ''' Devuelve un data set con la estructura de la tabla OTT_ORDN_TRAB_DEC_INICIAL
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarEstruturaOrdenTrabDecInicial() As Data.DataSet
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDN_TRAB_DEC_INICIAL_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("1 = 0"),
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
    ''' Devuelve un data set con los datos de la tabla OTT_ORDN_TRAB_DEC_INICIAL, para una OT especifica
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarOrdenTrabDecInicial() As Data.DataSet
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDN_TRAB_DEC_INICIAL_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo),
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
    ''' <creationDate>01/04/2016</creationDate>
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

#End Region

End Class
