Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos
Imports System.Data
Imports Wsr_OT_OrdenesDeTrabajo

''' <summary>
''' Mantiene la información del operario o profesional que se agregará al sector o taller correspondiente
''' </summary>
''' <remarks></remarks>
''' <author>César Bermúdez García</author>
''' <creationDate>27/01/2016</creationDate>
Partial Class Catalogos_Frm_OT_PersonalSectorTaller
    Inherits System.Web.UI.Page
#Region "Propiedades"
    ''' <summary>
    ''' Almacena la operación que el usuario desea efectuar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>27/01/2016</creationDate>
    Private Property operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>27/01/2016</creationDate>
    Private Property IdSectorTaller As String
        Get
            If ViewState("IdSectorTaller") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdSectorTaller"), String)
        End Get
        Set(value As String)
            ViewState("IdSectorTaller") = value
        End Set
    End Property

    ''' <summary>
    ''' Datos del operario a agregar o modificar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>27/01/2016</creationDate>
    Private Property Persona As EntOtfOperarioArea
        Get
            Return CType(ViewState("Persona"), EntOtfOperarioArea)
        End Get
        Set(value As EntOtfOperarioArea)
            ViewState("Persona") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el numero de empleado del operario a agregarse
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>27/01/2016</creationDate>
    Private Property NumEmpleado As Integer
        Get
            Return CType(ViewState("NumEmpleado"), Integer)
        End Get
        Set(value As Integer)
            ViewState("NumEmpleado") = value
        End Set
    End Property


#End Region

#Region "Eventos"

    ''' <summary>
    ''' Inicializa los componentes del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>20/01/2016</creationDate>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub


    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        Me.Session.Add("pvc_IdSectorTaller", IdSectorTaller)
        Response.Redirect("Lst_OT_PersonalSectorTaller.aspx")
    End Sub



    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then 'preguntar cuando hay validadores en el codigo
            Try
                Select Case Me.operacion
                    Case eOperacion.Agregar
                        If Agregar() Then
                            WebUtils.RegistrarScript(Me, "mostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("No a sido posible agregar el empleado.")
                        End If
                    Case eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me, "mostrarAlertaRegistroModificado", "mostrarAlertaRegistroModificado();")
                        Else
                            MostrarAlertaError("No a sido posible agregar el empleado.")
                        End If
                End Select
                

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
    ''' Se ejecuta cuando se da click en el link de la cédula del empleado
    ''' </summary>
    ''' <param name="pvc_NumeroDeEmpleado"></param>
    ''' <param name="pvc_Identificacion"></param>
    ''' <param name="pvc_NombreCompleto"></param>
    ''' <remarks></remarks>
    Protected Sub wuc_EmpleadosEU_Aceptar(pvc_NumeroDeEmpleado As Integer, pvc_Identificacion As String, pvc_NombreCompleto As String) Handles wuc_EmpleadosEU.Aceptar
        Me.txtIdentificacion.Text = pvc_Identificacion
        Me.lblNombre.Text = pvc_NombreCompleto
        Me.NumEmpleado = pvc_NumeroDeEmpleado
        CargarPuesto(Me.NumEmpleado)
        Me.upTxtIdentificacion.Update()
        Me.upTxtNombre.Update()
        Me.upTxtPuesto.Update()
        WebUtils.RegistrarScript(Me.Page, "InicializarFormulario", "javascript:inicializarFormulario();ocultarFiltroFuncionario();")
    End Sub

    ''' <summary>
    ''' Se ejecuta al darle click a la lupa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lnkEjecutarBusquedaFuncionario_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusquedaFuncionario.Click
        Try
            WebUtils.RegistrarScript(Me.Page, "mostrarPopUpBusquedaFuncionario", "javascript:mostrarPopUp('#PopUpBusquedaFuncionario');cargarLupa();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Se ejecuta cuando se cambia el texto de la identificación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub txtIdentificacion_TextChanged(sender As Object, e As EventArgs) Handles txtIdentificacion.TextChanged
        Dim vlo_ServiciosCurriculos As WsrEU_Curriculo.wsEU_Curriculo
        Dim vlo_DsEmpleados As Data.DataSet
        Dim pvc_CondicionBusquedas As String

        Try
            'instanciar y configurar objetos
            vlo_ServiciosCurriculos = New WsrEU_Curriculo.wsEU_Curriculo
            vlo_ServiciosCurriculos.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_ServiciosCurriculos.Timeout = -1

            Me.lblNombre.Text = ""
            Me.lblPuesto.Text = ""

            If Me.txtIdentificacion.Text <> "" Then
                pvc_CondicionBusquedas = String.Format("{0} = '{1}'", "ID_PERSONAL", Me.txtIdentificacion.Text)
                vlo_DsEmpleados = vlo_ServiciosCurriculos.Empleados_ListarRegistrosLista(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB), ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), pvc_CondicionBusquedas, "NOMBRE ASC, APELLIDO1 ASC, APELLIDO2 ASC")
                If vlo_DsEmpleados IsNot Nothing AndAlso vlo_DsEmpleados.Tables.Count > 0 AndAlso vlo_DsEmpleados.Tables(0).Rows.Count > 0 Then
                    Me.lblNombre.Text = String.Format("{0} {1} {2}", vlo_DsEmpleados.Tables(0).Rows(0)(4), vlo_DsEmpleados.Tables(0).Rows(0)(5), vlo_DsEmpleados.Tables(0).Rows(0)(6))
                    Me.NumEmpleado = vlo_DsEmpleados.Tables(0).Rows(0)(0)
                    CargarPuesto(vlo_DsEmpleados.Tables(0).Rows(0)(0))
                Else
                    Me.lblNombre.Text = ""
                    Me.txtIdentificacion.Text = ""
                    Me.NumEmpleado = 0
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoEncontrado();")

                End If
                WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:establecerControles();")
            Else
                WebUtils.RegistrarScript(Me.Page, "CargarLupa", "javascript:cargarLupa();")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Se ejecuta al cambiar la categoria laboral
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>20/01/2016</creationDate>
    Protected Sub ddlCategoriaLaboral_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategoriaLaboral.SelectedIndexChanged
        If Me.ddlCategoriaLaboral.SelectedValue = Area.PROFESIONAL Then
            'Si es área profesional se debe mostrar el combo con areas profesionales listo para escoger
            'Ademas se habilitan los validadores
            Me.trArea.Visible = True
            Me.ddlArea.Visible = True
            Me.rvfdllArea.Enabled = True
        Else
            Me.trArea.Visible = False
            Me.ddlArea.Visible = False
            Me.rvfdllArea.Enabled = False
        End If
    End Sub


#End Region

#Region "Metodos"

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub CargarEstado()
        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
    End Sub

    Private Sub CargarCategoria()
        Me.ddlCategoriaLaboral.Items.Clear()
        Me.ddlCategoriaLaboral.Items.Add(New ListItem("Operario", Area.OPERARIO))
        Me.ddlCategoriaLaboral.Items.Add(New ListItem("Profesional", Area.PROFESIONAL))
    End Sub

    Private Sub InicializarFormulario()

        Me.IdSectorTaller = WebUtils.LeerParametro(Of String)("pvc_IdSectorTaller")
        Me.Session.Add("pvc_IdSectorTaller", IdSectorTaller)
        Me.NumEmpleado = New UsuarioActual().NumEmpleado

        CargarEstado()
        CargarCategoria()
        CargarComboAreasProfesionales()
        CargarSector()

        Me.operacion = WebUtils.LeerParametro(Of Integer)("pvn_Operacion")
        Select Case Me.operacion
            Case eOperacion.Agregar
                Me.lblTitulo.Text = "Agregar Personal"
                Me.trArea.Visible = False
            Case eOperacion.Modificar
                Me.lblTitulo.Text = "Modificar Persona"
                CargarPersona(WebUtils.LeerParametro(Of String)("pvc_IdPersona"))
                CargarPuesto(Me.NumEmpleado)
                ddlCategoriaLaboral_SelectedIndexChanged("", New EventArgs)
        End Select

        
    End Sub

    Private Sub CargarComboAreasProfesionales()
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlArea.Items.Clear()
            Me.ddlArea.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))


            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_AREA_PROFESIONAL_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Empty, String.Empty, False, 1,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlArea
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_AREA_PROFESIONAL.DESCRIPCION
                    .DataValueField = Modelo.OTM_AREA_PROFESIONAL.ID_AREA_PROFESIONAL
                    .DataBind()
                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            Else
                With Me.ddlArea
                    .DataSource = Nothing
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

    Private Sub CargarSector()
        Dim vlo_DsSector As System.Data.DataSet
        Dim vlo_Wsr_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        Try
            vlo_Wsr_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
            vlo_Wsr_OT_Catalogos.Timeout = -1
            vlo_Wsr_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials


            vlo_DsSector = vlo_Wsr_OT_Catalogos.OTM_SECTOR_TALLER_ListarRegistrosLista(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            String.Format("{0} = '{1}'", Modelo.V_OTF_OPERARIO.ID_SECTOR_TALLER, IdSectorTaller),
            String.Empty,
            False,
            0,
            0)


            If vlo_DsSector.Tables(0) IsNot Nothing AndAlso vlo_DsSector.Tables(0).Rows.Count > 0 Then
                Me.lblSector.Text = vlo_DsSector.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE)
                Me.lblCoordinador.Text = vlo_DsSector.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE_COORDINADOR)
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_DsSector IsNot Nothing Then
                vlo_DsSector.Dispose()

            End If

            If vlo_Wsr_OT_Catalogos IsNot Nothing Then
                vlo_Wsr_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarPuesto(pvn_NumeroEmpleado As Integer)
        Dim vlo_DsPuesto As System.Data.DataSet
        Dim vlo_WsSolicitudVacaciones As WsrSolicitudVacaciones.WsSolicitudVacaciones
        Dim vlo_Fila As System.Data.DataRow

        Try
            vlo_WsSolicitudVacaciones = New WsrSolicitudVacaciones.WsSolicitudVacaciones
            vlo_WsSolicitudVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsSolicitudVacaciones.Timeout = -1


            vlo_DsPuesto = vlo_WsSolicitudVacaciones.VAH_PERIODOS_DEL_EMPLEADO_ObtenerDedicacionSIRHConUnidadTODOS(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_NumeroEmpleado,
                1)

            If vlo_DsPuesto.Tables(0) IsNot Nothing AndAlso vlo_DsPuesto.Tables(0).Rows.Count > 0 Then
                If vlo_DsPuesto.Tables(0).Rows.Count > 1 Then
                    For Each vlo_Fila In vlo_DsPuesto.Tables(0).Rows
                        Me.lblPuesto.Text = String.Format("{0} {1}", Me.lblPuesto.Text, vlo_Fila("DESC_PUESTO").ToString)
                    Next
                Else
                    Me.lblPuesto.Text = vlo_DsPuesto.Tables(0).Rows(0)(4)
                End If
            Else
                Me.lblPuesto.Text = "No tiene nombramiento actualmente"
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_DsPuesto IsNot Nothing Then
                vlo_DsPuesto.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga los datos del operario o Profesional asignado a este taller y los muestra en pantalla
    ''' </summary>
    ''' <param name="pvc_IdPersona"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez García</author>
    ''' <creationDate>27/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarPersona(pvc_IdPersona As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtfOperarioArea As EntOtfOperarioArea
        Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            'Se carga el empleado con su respectiva identificación
            vlo_EntEmpleados = CargarFuncionario(pvc_IdPersona)
            Me.NumEmpleado = vlo_EntEmpleados.NUM_EMPLEADO
            '{0}: Nombre de columna
            '{1}: Numero del empleado
            'Se obtiene el registro del operario o profesional asignado a este taller
            vlo_EntOtfOperarioArea = vlo_Ws_OT_Catalogos.OTF_OPERARIO_AREA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}'", Modelo.V_OTF_OPERARIO_AREA.NUM_EMPLEADO, vlo_EntEmpleados.NUM_EMPLEADO))

            'Se carga la información del usuario en pantalla
            '{0}: Nombre
            '{1}: Apellido 1
            '{2}: Apellido 2

            If vlo_EntOtfOperarioArea IsNot Nothing Then
                With vlo_EntOtfOperarioArea
                    Me.txtIdentificacion.Text = vlo_EntEmpleados.ID_PERSONAL
                    Me.lblNombre.Text = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                    Me.ddlCategoriaLaboral.SelectedValue = .CategoriaLaboral
                    Me.ddlEstado.SelectedValue = .Estado
                    Me.ddlArea.SelectedValue = .IdAreaProfesional

                End With
                Me.Persona = vlo_EntOtfOperarioArea
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
    ''' Des asigna el Role de autorizado por director
    ''' </summary>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>02/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub QuitarRol(ByVal pvc_NumIdentificacion As String)
        Try
            Dim vlc_RoleName As String
            vlc_RoleName = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ROL_OT_PROFESIONAL_DISENIO)
            Roles.RemoveUserFromRole(pvc_NumIdentificacion.Trim, vlc_RoleName)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Asigna el Role de autorizado por director
    ''' </summary>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>02/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AsignarRol(ByVal pvc_NumIdentificacion As String)
        Try
            Dim vlc_RoleName As String
            vlc_RoleName = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ROL_OT_PROFESIONAL_DISENIO)
            Roles.AddUserToRole(pvc_NumIdentificacion.Trim, vlc_RoleName)
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Construye registro para insertar a la base de datos con los datos del usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez García</author>
    ''' <creationDate>27/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As EntOtfOperarioArea
        Dim vlo_EntOtfOperarioArea As EntOtfOperarioArea


        If Me.operacion = eOperacion.Agregar Then
            vlo_EntOtfOperarioArea = New EntOtfOperarioArea
        ElseIf Me.operacion = eOperacion.Modificar Then
            vlo_EntOtfOperarioArea = Me.Persona
        End If


        With vlo_EntOtfOperarioArea
            .IdSectorTaller = Me.IdSectorTaller
            .NumEmpleado = Me.NumEmpleado
            .Usuario = New UsuarioActual().UserName
            .CategoriaLaboral = Me.ddlCategoriaLaboral.SelectedValue
            .Estado = Me.ddlEstado.SelectedValue
            .IdAreaProfesional = IIf(String.IsNullOrWhiteSpace(Me.ddlArea.SelectedValue), 0, Me.ddlArea.SelectedValue)
        End With

        'Se hace un llamado a un método para asociar o remover el rol OT_Profesional_Diseño.
        If vlo_EntOtfOperarioArea.Estado = Estado.ACTIVO AndAlso vlo_EntOtfOperarioArea.IdAreaProfesional <> 0 Then
            AsignarRol(Me.txtIdentificacion.Text.Trim)
        Else
            QuitarRol(Me.txtIdentificacion.Text.Trim)
        End If
        Return vlo_EntOtfOperarioArea
    End Function

    ''' <summary>
    ''' Ingresa nuevo registro en la DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez García</author>
    ''' <creationDate>27/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtfOperarioArea As EntOtfOperarioArea

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtfOperarioArea = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTF_OPERARIO_AREA_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtfOperarioArea) > 0
            
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Modifica un registro de la BD
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez García</author>
    ''' <creationDate>27/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtfOperarioArea As EntOtfOperarioArea

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtfOperarioArea = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTF_OPERARIO_AREA_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtfOperarioArea) > 0

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
    ''' <author>César Bermudez García</author>
    ''' <creationDate>27/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionario(pvn_NumEmpleado As String) As WsrEU_Curriculo.EntEmpleados
        Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("ID_PERSONAL = '{0}'", pvn_NumEmpleado))
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
