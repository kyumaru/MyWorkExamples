Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Frm_OT_OrdenTrabajoHijaProfesional
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
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
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
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
    ''' Prpoiedad para cargar datos del empleado en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
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
    ''' propiedad para la categoria de servicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property CategoriaServicio As Wsr_OT_Catalogos.EntOtmCategoriaServicio
        Get
            Return CType(ViewState("CategoriaServicio"), Wsr_OT_Catalogos.EntOtmCategoriaServicio)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmCategoriaServicio)
            ViewState("CategoriaServicio") = value
        End Set
    End Property

    ''' <summary>
    ''' ubicaion favorita del usuario que incluyo la orden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UbicacionFavorita As Wsr_OT_OrdenesDeTrabajo.EntOtfUbicacionFavorita
        Get
            Return CType(ViewState("UbicacionFavorita"), Wsr_OT_OrdenesDeTrabajo.EntOtfUbicacionFavorita)
        End Get
        Set(value As Wsr_OT_OrdenesDeTrabajo.EntOtfUbicacionFavorita)
            ViewState("UbicacionFavorita") = value
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
    ''' <creationDate>15/03/2016</creationDate>
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
    ''' evento que se ejecuta cuando se da click al boton de aceptar y enviar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptarEnviarOculto_Click(sender As Object, e As EventArgs) Handles btnAceptarEnviarOculto.Click
        If Page.IsValid Then
            Try

                If (Me.ddlCategoria.SelectedValue <> Me.OrdenTrabajo.IdCategoriaServicio) Or (Me.ddlActividad.SelectedValue <> Me.OrdenTrabajo.IdActividad) Then
                    If Agregar() Then
                        WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Se ha registrado la información correctamente','Lst_OT_GestionProfesionalesDisenio.aspx');")
                    Else
                        MostrarAlertaError("No ha sido posible almacenar la información del nuevo registro")
                    End If
                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Para crear una orden de trabajo hija debe indicar una nueva categoría y/o actividad.','');")
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

    ''' <summary>
    ''' evento que se ejecuta cuando se cambia el valor del combo de categorias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategoria.SelectedIndexChanged
        Dim vlc_Condicion As String

        Try
            If Me.ddlCategoria.SelectedValue <> String.Empty Then
                vlc_Condicion = String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTM_ACTIVIDAD.ID_CATEGORIA_SERVICIO, Me.ddlCategoria.SelectedValue, Modelo.OTM_ACTIVIDAD.ESTADO, Estado.ACTIVO)
                CargarActividad(vlc_Condicion)
                CategoriaServicio = CargarCategoriaServicio(CType(Me.ddlCategoria.SelectedValue, Integer))
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se cambia el valor seleccionado del combo de actividades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlActividad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlActividad.SelectedIndexChanged
        Try
            If Me.ddlActividad.SelectedValue <> String.Empty Then
                CargarDescripcionAmpliadaActividad()
            Else
                Me.txtDescripcionActividad.Text = String.Empty
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
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
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
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Try
            CargarUnidades()
            CargarLugarTrabajo(String.Format("{0} = '{1}'", Modelo.OTM_LUGAR_TRABAJO.ESTADO, Estado.ACTIVO))
            Me.lblAccion.Text = "Agregar Orden  de Trabajo Hija"
            CargarOrdenTrabajo(WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion"), WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo"))
            'WebUtils.RegistrarScript(Me, "deshabilitar", "deshabilitar();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Carga las unidades 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarUnidades()
        Dim vlo_DsUnidades As New Data.DataSet

        Try
            'Limpia el combo de beneficiarios
            Me.ddlUnidad.DataSource = Nothing
            Me.ddlUnidad.Items.Clear()
            Me.ddlUnidad.DataBind()

            FuncionesUtils.CargarUbicaciones(DateTime.Now, DateTime.Now, -1, 1, vlo_DsUnidades)

            'Verifica que el data set contenga tablas
            If vlo_DsUnidades.Tables.Count > 0 Then
                'Verifica la existencia de registros en la tabla
                If vlo_DsUnidades.Tables(0).Rows.Count > 0 Then
                    With Me.ddlUnidad
                        .Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
                        .DataSource = vlo_DsUnidades
                        .DataMember = vlo_DsUnidades.Tables(0).TableName
                        .DataTextField = "COD_DESC"
                        .DataValueField = "CODIGO_UBICA"
                        .DataBind()
                    End With
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Carga las catagorias, segun la condicion de busqueda que obtiene por parametro
    ''' </summary>
    ''' <param name="pvc_Condicion">consecutivo de la orden</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarCategoriaServicio(pvc_Condicion As String)
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlCategoria.Items.Clear()
            Me.ddlCategoria.Items.Add(New ListItem("[Seleccione el Valor]", String.Empty))

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
    ''' Carga las actividades, segun la condicion de busqueda que obtiene por parametro
    ''' </summary>
    ''' <param name="pvc_Condicion">consecutivo de la orden</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarActividad(pvc_Condicion As String)
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlActividad.Items.Clear()
            Me.ddlActividad.Items.Add(New ListItem("[Seleccione el Valor]", String.Empty))

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
    ''' carga el combo de lugares de trabajo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLugarTrabajo(pvc_Condicion As String)
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlLugarTrabajo.Items.Clear()
            Me.ddlLugarTrabajo.Items.Add(New ListItem("[Seleccione el Valor]", String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_LUGAR_TRABAJO_ListarRegistros(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                pvc_Condicion,
                                String.Format("{0} ASC", Modelo.OTM_LUGAR_TRABAJO.NOMBRE),
                                False,
                                0,
                                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlLugarTrabajo
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_LUGAR_TRABAJO.NOMBRE
                    .DataValueField = Modelo.OTM_LUGAR_TRABAJO.ID_LUGAR_TRABAJO
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
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">id de la ubicacion</param>
    ''' <param name="pvc_IdOrdenTrabajo">id de la orden</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenTrabajo(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntEmpleadoSolicita As WsrEU_Curriculo.EntEmpleados

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo))

            vlo_EntEmpleadoSolicita = CargarFuncionarioNumEmpleado(Me.OrdenTrabajo.NumEmpleado)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

        If Me.OrdenTrabajo.Existe Then
            With Me.OrdenTrabajo
                Me.txtPersonaContacto.Text = .NombrePersonaContacto
                Me.lblSolicitante.Text = String.Format("{0} {1} {2}", vlo_EntEmpleadoSolicita.NOMBRE, vlo_EntEmpleadoSolicita.APELLIDO1, vlo_EntEmpleadoSolicita.APELLIDO2)
                Me.txtTelefono.Text = .Telefono
                Me.ddlUnidad.SelectedValue = .CodUnidadSirh
                Me.ddlLugarTrabajo.SelectedValue = .IdLugarTrabajo
                Me.txtLugarExacto.Text = .SennasExactas
                Me.UbicacionFavorita = CargarUbicacionFavorita(.NumEmpleado)
                CargarCategoriaServicio(String.Format("{0} LIKE '%{1}%' AND {2} = {3}", Modelo.OTM_CATEGORIA_SERVICIO.ESTADO, Estado.ACTIVO, Modelo.OTM_CATEGORIA_SERVICIO.ID_UBICACION_ADMINISTRA, Me.UbicacionFavorita.IdUbicacion))
                Me.ddlCategoria.SelectedValue = .IdCategoriaServicio
                CargarActividad(String.Format("{0} LIKE '%{1}%' AND {2} = {3}", Modelo.OTM_ACTIVIDAD.ESTADO, Estado.ACTIVO, Modelo.OTM_ACTIVIDAD.ID_CATEGORIA_SERVICIO, .IdCategoriaServicio))
                Me.ddlActividad.SelectedValue = .IdActividad
                CargarDescripcionAmpliadaActividad()
                Me.txtDescTrabajo.Text = .DescripcionTrabajo

            End With
        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    ''' <summary>
    ''' carga la descripcion ampliada de la actividad
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarDescripcionAmpliadaActividad()
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmActividad As Wsr_OT_Catalogos.EntOtmActividad

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtmActividad = vlo_Ws_OT_Ws_OT_Catalogos.OTM_ACTIVIDAD_ObtenerRegistro(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_ACTIVIDAD, Me.ddlActividad.SelectedValue))

            Me.txtDescripcionActividad.Text = vlo_EntOtmActividad.DescripcionAmpliada

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion encargada de contruir el registro  de la orden de trabajo
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo
        Dim vlo_EntOttOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo
        vlo_EntOttOrdenTrabajo = Me.OrdenTrabajo

        With vlo_EntOttOrdenTrabajo
            .IdUbicacionMadre = .IdUbicacion
            .IdOrdenTrabajoMadre = .IdOrdenTrabajo
            .EstadoOrdenTrabajo = EstadoOrden.PENDIENTE_REVISION_SUPERVISOR
            .IdCategoriaServicio = Me.ddlCategoria.SelectedValue
            .IdActividad = Me.ddlActividad.SelectedValue
            .IdSectorTaller = 0
            .NumEmpleado = Me.Usuario.NumEmpleado
            .FechaHoraSolicita = DateTime.Now
            .DescripcionTrabajo = Me.txtDescTrabajo.Text
            .IncluidaEnRecepcion = 0
            .Parentesco = "HIJ"
            .Usuario = Me.Usuario.UserName
        End With

        Return vlo_EntOttOrdenTrabajo
    End Function

    ''' <summary>
    ''' carga un entidad de lugar de trabajo, segun identificacion obtenida por parametro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
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
    ''' Administra el proceso para agregar una orden de trabajo
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOttOrdenTrabajo = ConstruirRegistro()

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_InsertarOrdenTrabajoHija(
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
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
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
    ''' carga la categoria de servicio
    ''' </summary>
    ''' <param name="pvn_IdCategoriaServicio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
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
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionarioNumEmpleado(pvn_NumEmpleado As String) As WsrEU_Curriculo.EntEmpleados
        Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("NUM_EMPLEADO = '{0}'", pvn_NumEmpleado))
        Catch ex As Exception
            Throw
        Finally
            If vlo_WsEU_Curriculo IsNot Nothing Then
                vlo_WsEU_Curriculo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga la ubicacion favorita
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarUbicacionFavorita(pvn_NumEmpleado As Integer) As Wsr_OT_OrdenesDeTrabajo.EntOtfUbicacionFavorita
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTF_UBICACION_FAVORITA_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTF_UBICACION_FAVORITA.NUM_EMPLEADO, pvn_NumEmpleado))
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
