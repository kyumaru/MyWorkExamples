Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo



Partial Class OrdenesDeTrabajo_Frm_OT_VbDenegadaRechazada
    Inherits System.Web.UI.Page
#Region "Propiedades"


    ''' <summary>
    ''' Operacion actual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>8/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property Operacion As String
        Get
            Return CType(ViewState("Operacion"), String)
        End Get
        Set(value As String)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la orden, pasado por parámetro desde la pantalla de listado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
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
    ''' Parámetro del número de empleado pasado desde la pantalla del listado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property NumEmpleado As String
        Get
            Return CType(ViewState("NumEmpleado"), String)
        End Get
        Set(value As String)
            ViewState("NumEmpleado") = value
        End Set
    End Property

    ''' <summary>
    ''' Parámetro del número de empleado coordinador pasado desde la pantalla del listado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property NumEmpleadoCoordinador As String
        Get
            Return CType(ViewState("NumEmpleadoCoordinador"), String)
        End Get
        Set(value As String)
            ViewState("NumEmpleadoCoordinador") = value
        End Set
    End Property

    ''' <summary>
    ''' Parámetro del número de empleado coordinador pasado desde la pantalla del listado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdUbicacion As String
        Get
            Return CType(ViewState("IdUbicacion"), String)
        End Get
        Set(value As String)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Orden de trabajo actual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
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
    ''' Propiedad para el usuario actual en sesion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
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
    ''' Propiedad para guardar el empleado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
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
    ''' Propiedad para cargar la ubiación 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property AutorizadoUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Get
            Return CType(ViewState("AutorizadoUbicacion"), Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
            ViewState("AutorizadoUbicacion") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Carga los datos de la orden de trabajo a Reasignar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try

                'configurar ID Ubicacion
                Me.Usuario = New UsuarioActual
                Me.Empleado = CargarFuncionario(Me.Usuario.UserName)
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)
                If Me.AutorizadoUbicacion.Existe Then
                    InicializarFormulario()
                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ninguna ubicación autorizada, contactar con el administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Cambia la lista de actividades deacuerdo a la categoría seleccionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategoria.SelectedIndexChanged
        Dim vlc_Condicion As String

        Try
            If CalcularSectorTaller() Then
                Me.lblNombreTallerSector.Text = ObtenerNombreTaller(Me.ddlCategoria.SelectedValue, Me.ddlActividad.SelectedValue, Me.ddlLugarTrabajo.SelectedValue)
            End If
            If Me.ddlCategoria.SelectedValue <> String.Empty Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_CATEGORIA_SERVICIO, Me.ddlCategoria.SelectedValue)
                CargarActividad(vlc_Condicion)
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Pregunta si debe ejecutar el metodo para obtener el nombre del taller
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlActividad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlActividad.SelectedIndexChanged

        Try
            If CalcularSectorTaller() Then
                Me.lblNombreTallerSector.Text = ObtenerNombreTaller(Me.ddlCategoria.SelectedValue, Me.ddlActividad.SelectedValue, Me.ddlLugarTrabajo.SelectedValue)
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Pregunta si debe ejecutar el metodo para obtener el nombre del taller
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlLugarTrabajo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLugarTrabajo.SelectedIndexChanged

        Try
            If CalcularSectorTaller() Then
                Me.lblNombreTallerSector.Text = ObtenerNombreTaller(Me.ddlCategoria.SelectedValue, Me.ddlActividad.SelectedValue, Me.ddlLugarTrabajo.SelectedValue)

            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al dar click en aceptar, 
    ''' la acción a tomar dependerá de qué valor se halla seleccionado en el radio button.
    ''' Se muestra un mensaje al final indicando si tuvo exito
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>8/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            Select Case Me.Operacion
                Case Is = "VB"
                    VistoBueno(Me.IdOrdenTrabajo, Me.IdUbicacion, Me.NumEmpleado)
                    WebUtils.RegistrarScript(Me.Page, "MensajePopup", "MensajePopup('Operación de Visto Bueno realizada con éxito','Lst_OT_Rechazadas.aspx');")
                Case Is = "D"
                    Denegar(Me.IdOrdenTrabajo, Me.IdUbicacion, IIf(String.IsNullOrWhiteSpace(Me.NumEmpleadoCoordinador), Me.NumEmpleado, Me.NumEmpleadoCoordinador))
                    WebUtils.RegistrarScript(Me.Page, "MensajePopup", "MensajePopup('Operación de denegar realizada con éxito','Lst_OT_Rechazadas.aspx');")
                Case Is = "R"
                    If Modificar() Then
                        WebUtils.RegistrarScript(Me.Page, "MensajePopup", "MensajePopup('Reasignación exitosa','Lst_OT_Rechazadas.aspx');")
                    End If

            End Select
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Cambia el comportamiento del formulario deacuerdo a lo seleccionado en el radiobutton
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Condicion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtVistoBueno.CheckedChanged, rbtDenegada.CheckedChanged, rbtReasignada.CheckedChanged
        Try

            If Me.rbtVistoBueno.Checked Then
                Me.ddlCategoria.Enabled = False
                Me.ddlActividad.Enabled = False
                Me.ddlLugarTrabajo.Enabled = False
                Me.rvftxtMotivo.Enabled = False
                Me.rfvDdlCategoria.Enabled = False
                Me.rfvDdlActividad.Enabled = False
                Me.rfvddlLugarTrabajo.Enabled = False
                Me.Operacion = "VB"
                Me.lblAccion.Text = "Visto Bueno al motivo de rechazo"
            End If

            If Me.rbtDenegada.Checked Then
                Me.ddlCategoria.Enabled = False
                Me.ddlActividad.Enabled = False
                Me.ddlLugarTrabajo.Enabled = False
                Me.rvftxtMotivo.Enabled = True
                Me.rfvDdlCategoria.Enabled = False
                Me.rfvDdlActividad.Enabled = False
                Me.rfvddlLugarTrabajo.Enabled = False
                Me.Operacion = "D"
                Me.lblAccion.Text = "Denegación al motivo de rechazo"
            End If

            If Me.rbtReasignada.Checked Then
                Me.ddlCategoria.Enabled = True
                Me.ddlActividad.Enabled = True
                Me.ddlLugarTrabajo.Enabled = True
                Me.rvftxtMotivo.Enabled = False
                Me.rfvDdlCategoria.Enabled = True
                Me.rfvDdlActividad.Enabled = True
                Me.rfvddlLugarTrabajo.Enabled = True
                Me.Operacion = "R"
                Me.lblAccion.Text = "Reasignar órden de trabajo"
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub


#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inicializa el formulario con las categorias, edificios y actividades, ademas deshabilita los campos que no se deben modificar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()

        Try
            Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
            Me.NumEmpleado = WebUtils.LeerParametro(Of String)("pvc_NumEmpleado")
            Me.NumEmpleadoCoordinador = WebUtils.LeerParametro(Of String)("pvc_NumEmpleadoCoordinador")
            Me.IdUbicacion = WebUtils.LeerParametro(Of String)("pvc_IdUbicacion")

           ' Me.Operacion = WebUtils.LeerParametro(Of String)("pvc_OP")
            CargarOrdenTrabajo()
            CargarCategorias(String.Format("{0} = '{1}'", Modelo.OTM_CATEGORIA_SERVICIO.NUM_EMPLEADO_SUPERVISOR, Me.AutorizadoUbicacion.NumEmpleado))
            CargarEdificios()
            CargarActividad(String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_CATEGORIA_SERVICIO, Me.ddlCategoria.SelectedValue))

            'Select Case Me.Operacion
            '    Case Is = "VB"
            '        Me.rbtVistoBueno.Checked = True
            '        Condicion_CheckedChanged("", New EventArgs)
            '    Case Is = "D"
            '        Me.rbtDenegada.Checked = True
            '        Condicion_CheckedChanged("", New EventArgs)
            '    Case Is = "R"
            '        Me.rbtReasignada.Checked = True
            '        Condicion_CheckedChanged("", New EventArgs)
            'End Select

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Manda a llamar al metodo que ejecuta el visto bueno
    ''' </summary>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <param name="pvc_NumEmpleado"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    Private Sub VistoBueno(pvc_IdOrdenTrabajo As String, pvn_IdUbicacion As Integer, pvc_NumEmpleado As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        Try

            'Configurar el servicio de Ordenes de Trabajo
            vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
            vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
            vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

            vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_VistoBuenoAlMotivoDeRechazo(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_IdOrdenTrabajo, pvn_IdUbicacion, pvc_NumEmpleado, Me.txtMotivo.Text, Me.Usuario.UserName)
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Llamado al metodo de denegacion al motivo rechazo
    ''' </summary>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <param name="pvc_NumEmpleado"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    Private Sub Denegar(pvc_IdOrdenTrabajo As String, pvn_IdUbicacion As Integer, pvc_NumEmpleado As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        Try

            'Configurar el servicio de Ordenes de Trabajo
            vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
            vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
            vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

            vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_DenegacionAlMotivoDeRechazo(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_IdOrdenTrabajo, pvn_IdUbicacion, pvc_NumEmpleado, Me.txtMotivo.Text, Me.Usuario.UserName)


        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Metodo para cargar las categorias con la condición especificada
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarCategorias(pvc_Condicion As String)
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlCategoria.Items.Clear()
            Me.ddlCategoria.Items.Add(New ListItem("[Selecccione el Valor]", String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ListarRegistros(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                            pvc_Condicion,
                            String.Empty,
                            False,
                            0,
                            0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlCategoria

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
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub


    ''' <summary>
    ''' Carga una lista de actividades con la condición recibida
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarActividad(pvc_Condicion As String)
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlActividad.Items.Clear()
            Me.ddlActividad.Items.Add(New ListItem("[Selecccione el Valor]", String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_ACTIVIDAD_ListarRegistros(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                            pvc_Condicion,
                            String.Empty,
                            False,
                            0,
                            0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlActividad
                    '.Items.Add(New ListItem("[Selecccione el Valor]", String.Empty))
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_ACTIVIDAD.DESCRIPCION
                    .DataValueField = Modelo.OTM_ACTIVIDAD.ID_ACTIVIDAD
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
    ''' Carga los edificios con la condicion especificada por parametro
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEdificios()
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlLugarTrabajo.Items.Add(New ListItem("[Selecccione el Valor]", String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_LUGAR_TRABAJO_ListarRegistros(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                String.Empty,
                                String.Empty,
                                False,
                                0,
                                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlLugarTrabajo
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_LUGAR_TRABAJO.NOMBRE
                    .DataValueField = Modelo.OTM_LUGAR_TRABAJO.ID_LUGAR_TRABAJO
                    .SelectedValue = Me.OrdenTrabajo.IdLugarTrabajo
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
    ''' Permite Cargar una orden de trabajo por el Id
    ''' </summary>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenTrabajo()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        Dim vlc_lugarTrabajo As String

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0}: nombre del campo id orden trabajo
            '{1}: nombre del campo id ubicación
            '{2}: Id de la OT
            '{3}: Ubicación de la OT

            Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{2}' AND {1} = {3}", Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, Me.IdOrdenTrabajo, Me.IdUbicacion))

            vlc_lugarTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerFnOtConsultaLugarTrabajo(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.OrdenTrabajo.IdCategoriaServicio, Me.OrdenTrabajo.IdActividad, Me.OrdenTrabajo.IdLugarTrabajo)



        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

        If Me.OrdenTrabajo.Existe Then
            With Me.OrdenTrabajo
                Me.lblFechaSolicitud.Text = .FechaHoraSolicita.ToString
                Me.lblSolicitante.Text = String.Format("{0} {1} {2}", Me.Empleado.NOMBRE, Me.Empleado.APELLIDO1, Me.Empleado.APELLIDO2)
                Me.lblPersonaContacto.Text = .NombrePersonaContacto
                Me.lblTelefono.Text = .Telefono
                Me.lblLugarExacto.Text = .SennasExactas
                Me.lblEdificio.Text = CargarLugarTrabajo(.IdLugarTrabajo).Nombre
                Me.lblCategServ.Text = CargarCategoriaServicio(.IdCategoriaServicio).Descripcion
                Me.lblActividad.Text = CargarActividad(.IdActividad).Descripcion
                Me.ddlLugarTrabajo.SelectedValue = .IdLugarTrabajo
                Me.ddlCategoria.SelectedValue = .IdCategoriaServicio
                Me.ddlActividad.SelectedValue = .IdActividad
                Me.lblDescTrabajo.Text = .DescripcionTrabajo
                Me.lblSectorTaller.Text = vlc_lugarTrabajo
                Me.lblMotivoRechazo.Text = vlo_Ws_OT_Catalogos.OTM_MOTIVO_RECHAZO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Utilerias.OrdenesDeTrabajo.Modelo.OTM_MOTIVO_RECHAZO.ID_MOTIVO_RECHAZO, .IdMotivoRechazo)).Descripcion
            End With
        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Carga el funcionario desde el servicio EUCurriculo
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
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
    ''' Carga la ubicacion de un funcionario por numero de empleado
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
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
    ''' Permite construir el registro a ser modificado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo
        Dim vlo_EntOttOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo

        vlo_EntOttOrdenTrabajo = Me.OrdenTrabajo

        With vlo_EntOttOrdenTrabajo
            .IdCategoriaServicio = Me.ddlCategoria.SelectedValue
            .IdActividad = Me.ddlActividad.SelectedValue
            .IdLugarTrabajo = Me.ddlLugarTrabajo.SelectedValue
            .Usuario = Me.Usuario.UserName
            .EstadoOrdenTrabajo = Utilerias.OrdenesDeTrabajo.EstadoOrden.ASIGNADA
        End With

        Return vlo_EntOttOrdenTrabajo
    End Function

    ''' <summary>
    ''' Funcion que llama el servicio web de ordenes de trabajo para modificar un registro sin archivos adjuntos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOtfOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        If Me.ddlCategoria.SelectedValue <> Me.OrdenTrabajo.IdCategoriaServicio Or
            Me.ddlActividad.SelectedValue <> Me.OrdenTrabajo.IdActividad Or
            Me.ddlLugarTrabajo.SelectedValue <> Me.OrdenTrabajo.IdLugarTrabajo Then


            vlo_EntOtfOrdenTrabajo = ConstruirRegistro()


            Try
                Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ModificarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOtfOrdenTrabajo) > 0
            Catch ex As Exception
                Throw
            Finally
                If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                    vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
                End If
            End Try
        Else
            WebUtils.RegistrarScript(Me.Page, "mostrarAlertaRegistroIdentico", "mostrarAlertaRegistroIdentico();")
            Return False
        End If

    End Function

    ''' <summary>
    ''' Evalua cuando se debe llamar a la vista y calcular el sector o taller al que se va a reasignar la orden
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CalcularSectorTaller() As Boolean
        Dim vlo_Resultado As Boolean

        vlo_Resultado = False
        If Me.ddlCategoria.SelectedValue <> String.Empty Then
            If Me.ddlActividad.SelectedValue <> String.Empty Then
                If Me.ddlLugarTrabajo.SelectedValue <> String.Empty Then
                    vlo_Resultado = True
                End If
            End If
        End If

        Return vlo_Resultado
    End Function

    ''' <summary>
    ''' Funcion para obtener el nombre del sector o taller al que se reasignará la orden de trabajo
    ''' </summary>
    ''' <param name="pvn_PvnIdcategoria"></param>
    ''' <param name="pvn_PvnIdactividad"></param>
    ''' <param name="pvn_PvnIdlugartrabajo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>3/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerNombreTaller(pvn_PvnIdcategoria As String, pvn_PvnIdactividad As String, pvn_PvnIdlugartrabajo As String) As String
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerFnOtConsultaLugarTrabajo(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_PvnIdcategoria, pvn_PvnIdactividad, pvn_PvnIdlugartrabajo)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' Retorna una entidad de tipo lugar trabajo, segun el parámetro obtenido
    ''' </summary>
    ''' <param name="pvn_IdLugarTrabajo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarLugarTrabajo(pvn_IdLugarTrabajo As Integer) As Wsr_OT_Catalogos.EntOtmLugarTrabajo
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_Catalogos.OTM_LUGAR_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_LUGAR_TRABAJO.ID_LUGAR_TRABAJO, pvn_IdLugarTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Retorna una entidad de tipo categoria servicio, segun el parámetro obtenido
    ''' </summary>
    ''' <param name="pvn_IdCategoriaServicio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
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
    ''' Retorna una entidad de tipo actividad, segun el parámetro obtenido
    ''' </summary>
    ''' <param name="pvn_IdActividad"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>9/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarActividad(pvn_IdActividad As Integer) As Wsr_OT_Catalogos.EntOtmActividad
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_Catalogos.OTM_ACTIVIDAD_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_ACTIVIDAD, pvn_IdActividad))

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
