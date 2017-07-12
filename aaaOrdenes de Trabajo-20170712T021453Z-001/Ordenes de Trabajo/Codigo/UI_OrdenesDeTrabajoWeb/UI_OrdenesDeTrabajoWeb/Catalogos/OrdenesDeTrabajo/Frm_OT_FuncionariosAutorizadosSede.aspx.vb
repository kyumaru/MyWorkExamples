Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos

Partial Class Catalogos_Frm_OT_FuncionariosAutorizadosSede
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el Autorizado Ubicacion a trabajar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property AutorizadoUbicacion As EntOtmAutorizadoUbicacion
        Get
            Return CType(ViewState("AutorizadoUbicacion"), EntOtmAutorizadoUbicacion)
        End Get
        Set(value As EntOtmAutorizadoUbicacion)
            ViewState("AutorizadoUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
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
    ''' <creationDate>02/10/2015</creationDate>
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
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click en el boton de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try
                Me.Empleado = CargarFuncionario(Me.txtIdFuncionario.Text)

                If Me.Empleado.Existe Then
                    Select Case (Me.Operacion)
                        Case Is = eOperacion.Agregar
                            If Agregar() Then
                                WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
                            Else
                                MostrarAlertaError("No ha sido posible almacenar la información del nuevo registro")
                            End If

                        Case Is = eOperacion.Modificar
                            If Modificar() Then
                                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                            Else
                                MostrarAlertaError("No ha sido posible actualizar la información del registro")
                            End If
                    End Select
                Else
                    MostrarAlertaError("El ID del solicitante no corresponde a ningún funcionario.")
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
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnLimpiarFormulario_Click(sender As Object, e As EventArgs) Handles btnLimpiarFormulario.Click
        WebUtils.RegistrarScript(Me.Page, "CargarLupa", "javascript:cargarLupa();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkEjecutarBusquedaFuncionario_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusquedaFuncionario.Click
        Try
            Me.wuc_EmpleadosEU.Indicador = 1
            WebUtils.RegistrarScript(Me.Page, "mostrarPopUpBusquedaFuncionario", "javascript:mostrarPopUp('#PopUpBusquedaFuncionario');cargarLupa();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvc_NumeroDeEmpleado"></param>
    ''' <param name="pvc_Identificacion"></param>
    ''' <param name="pvc_NombreCompleto"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub wuc_EmpleadosEU_Aceptar(pvc_NumeroDeEmpleado As Integer, pvc_Identificacion As String, pvc_NombreCompleto As String) Handles wuc_EmpleadosEU.Aceptar
        If Me.wuc_EmpleadosEU.Indicador = 1 Then
            Me.txtIdFuncionario.Text = pvc_Identificacion
            Me.lblNombre.Text = pvc_NombreCompleto
            Me.upTxtIdFuncionario.Update()
            Me.upLblNombre.Update()
        End If

        WebUtils.RegistrarScript(Me.Page, "InicializarFormulario", "javascript:inicializarFormulario();ocultarFiltroFuncionario();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub txtIdFuncionario_TextChanged(sender As Object, e As EventArgs) Handles txtIdFuncionario.TextChanged
        Dim vlo_ServiciosCurriculos As WsrEU_Curriculo.wsEU_Curriculo
        Dim vlo_DsEmpleados As Data.DataSet
        Dim pvc_CondicionBusquedas As String

        Try
            'instanciar y configurar objetos
            vlo_ServiciosCurriculos = New WsrEU_Curriculo.wsEU_Curriculo
            vlo_ServiciosCurriculos.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_ServiciosCurriculos.Timeout = -1

            Me.lblNombre.Text = String.Empty

            If Me.txtIdFuncionario.Text <> String.Empty Then
                pvc_CondicionBusquedas = String.Format("{0} = '{1}'", "ID_PERSONAL", Me.txtIdFuncionario.Text)
                vlo_DsEmpleados = vlo_ServiciosCurriculos.Empleados_ListarRegistrosLista(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB), ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), pvc_CondicionBusquedas, "NOMBRE ASC, APELLIDO1 ASC, APELLIDO2 ASC")
                If vlo_DsEmpleados IsNot Nothing AndAlso vlo_DsEmpleados.Tables.Count > 0 AndAlso vlo_DsEmpleados.Tables(0).Rows.Count > 0 Then
                    Me.lblNombre.Text = String.Format("{0} {1} {2}", vlo_DsEmpleados.Tables(0).Rows(0)(4), vlo_DsEmpleados.Tables(0).Rows(0)(5), vlo_DsEmpleados.Tables(0).Rows(0)(6))
                Else
                    Me.lblNombre.Text = String.Empty
                    Me.txtIdFuncionario.Text = String.Empty
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoEncontrado();")

                End If
                WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar el formulario segun la operacion a realizar
    ''' en caso de ser agregar carga el texo con "Agregar", en caso de ser modificar 
    ''' cargar el texto con "Modificar" y llama al método que obtiene los datos del autorizado, segun el id 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        CargarComboSede()

        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Autorizado"
            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Autorizado"
                Try
                    CargarAutorizadoUbicacion(WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion"), WebUtils.LeerParametro(Of Integer)("pvn_NumEmpleado"))
                  
                Catch ex As Exception
                    Throw
                End Try
        End Select
    End Sub


    ''' <summary>
    ''' carga los registros de otm_ubicacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboSede()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlSede.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODAS, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_UBICACION_ListarRegistros(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} LIKE '%{1}%'", Modelo.OTM_UBICACION.ESTADO, Estado.ACTIVO),
               String.Format("{0} {1}", Modelo.OTM_UBICACION.DESCRIPCION, Ordenamiento.ASCENDENTE),
               False,
               0,
               0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlSede
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_UBICACION.DESCRIPCION
                    .DataValueField = Modelo.OTM_UBICACION.ID_UBICACION
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
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la autorizacion segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">identificacion ubicacion</param>
    ''' <param name="pvn_NumEmpleado">numero de empleado</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarAutorizadoUbicacion(pvn_IdUbicacion As Integer, pvn_NumEmpleado As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntEmpleado As WsrEU_Curriculo.EntEmpleados

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.AutorizadoUbicacion = vlo_Ws_OT_Catalogos.OTM_AUTORIZADO_UBICACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_AUTORIZADO_UBICACION.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion, Modelo.OTM_AUTORIZADO_UBICACION.NUM_EMPLEADO, pvn_NumEmpleado))

            vlo_EntEmpleado = CargarFuncionarioNumEmpleado(Me.AutorizadoUbicacion.NumEmpleado)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If Me.AutorizadoUbicacion.Existe Then
            With Me.AutorizadoUbicacion
                Me.txtIdFuncionario.Text = vlo_EntEmpleado.ID_PERSONAL
                Me.lblNombre.Text = String.Format("{0} {1} {2}", vlo_EntEmpleado.NOMBRE, vlo_EntEmpleado.APELLIDO1, vlo_EntEmpleado.APELLIDO2)
                Me.ddlSede.SelectedValue = .IdUbicacionAdministra
            End With
        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion encargada de contruir el registro  de la autorizacion
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Dim vlo_EntOtmAutorizadoUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion

        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtmAutorizadoUbicacion = New Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
            vlo_EntOtmAutorizadoUbicacion.NumEmpleado = Me.Empleado.NUM_EMPLEADO
        Else
            vlo_EntOtmAutorizadoUbicacion = Me.AutorizadoUbicacion
        End If

        With vlo_EntOtmAutorizadoUbicacion
            .IdUbicacionAdministra = Me.ddlSede.SelectedValue
            .Usuario = Me.Usuario.UserName
        End With

        Return vlo_EntOtmAutorizadoUbicacion
    End Function

    ''' <summary>
    ''' Administra el proceso para agregar un autorizado
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmAutorizadoUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmAutorizadoUbicacion = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_AUTORIZADO_UBICACION_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmAutorizadoUbicacion) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Administra el proceso para modificar una autorizado
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmAutorizadoUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmAutorizadoUbicacion = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_AUTORIZADO_UBICACION_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmAutorizadoUbicacion) > 0
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
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
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
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/10/2015</creationDate>
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

#End Region

End Class
