Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Imports Utilerias.Genericos.Extensiones


Partial Class Catalogos_Frm_OT_CategoriaServicio
    Inherits System.Web.UI.Page
#Region "Propiedades"
    Private Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    Private Property Categoria As EntOtmCategoriaServicio
        Get
            Return CType(ViewState("Categoria"), EntOtmCategoriaServicio)
        End Get
        Set(value As EntOtmCategoriaServicio)
            ViewState("Categoria") = value
        End Set
    End Property

    Private Property NumEmpleado As Integer
        Get
            Return CType(ViewState("NumEmpleado"), Integer)
        End Get
        Set(value As Integer)
            ViewState("NumEmpleado") = value
        End Set
    End Property

    Private Property UnidadAdministra As Integer
        Get
            Return CType(ViewState("UnidadAdministra"), Integer)
        End Get
        Set(value As Integer)
            ViewState("UnidadAdministra") = value
        End Set
    End Property
#End Region

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'AddHandler wuc_EmpleadosEU.Aceptar, AddressOf CargarNombreDeFuncionario
        If Not IsPostBack Then
            Try
                Me.UnidadAdministra = CType(Session("pvo_UnidadAdministra"), Integer)
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try

        End If
    End Sub

    Protected Sub wuc_EmpleadosEU_Aceptar(pvc_NumeroDeEmpleado As Integer, pvc_Identificacion As String, pvc_NombreCompleto As String) Handles wuc_EmpleadosEU.Aceptar
        Me.txtSupervisor.Text = pvc_Identificacion
        Me.lblNombre.Text = pvc_NombreCompleto
        Me.NumEmpleado = pvc_NumeroDeEmpleado
        Me.upTxtSupervisor.Update()
        Me.upTxtNombre.Update()
        WebUtils.RegistrarScript(Me.Page, "InicializarFormulario", "javascript:inicializarFormulario();ocultarFiltroFuncionario();")
    End Sub

    Protected Sub btnLimpiarFormulario_Click(sender As Object, e As EventArgs) Handles btnLimpiarFormulario.Click
        WebUtils.RegistrarScript(Me.Page, "CargarLupa", "javascript:cargarLupa();")
    End Sub

    Protected Sub txtSupervisor_TextChanged(sender As Object, e As EventArgs) Handles txtSupervisor.TextChanged
        Dim vlo_ServiciosCurriculos As WsrEU_Curriculo.wsEU_Curriculo
        Dim vlo_DsEmpleados As Data.DataSet
        Dim pvc_CondicionBusquedas As String

        Try
            'instanciar y configurar objetos
            vlo_ServiciosCurriculos = New WsrEU_Curriculo.wsEU_Curriculo
            vlo_ServiciosCurriculos.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_ServiciosCurriculos.Timeout = -1

            Me.lblNombre.Text = ""

            If Me.txtSupervisor.Text <> "" Then
                pvc_CondicionBusquedas = String.Format("{0} = '{1}'", "ID_PERSONAL", Me.txtSupervisor.Text)
                vlo_DsEmpleados = vlo_ServiciosCurriculos.Empleados_ListarRegistrosLista(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB), ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), pvc_CondicionBusquedas, "NOMBRE ASC, APELLIDO1 ASC, APELLIDO2 ASC")
                If vlo_DsEmpleados IsNot Nothing AndAlso vlo_DsEmpleados.Tables.Count > 0 AndAlso vlo_DsEmpleados.Tables(0).Rows.Count > 0 Then
                    Me.lblNombre.Text = String.Format("{0} {1} {2}", vlo_DsEmpleados.Tables(0).Rows(0)(4), vlo_DsEmpleados.Tables(0).Rows(0)(5), vlo_DsEmpleados.Tables(0).Rows(0)(6))
                    Me.NumEmpleado = vlo_DsEmpleados.Tables(0).Rows(0)(0)
                Else
                    Me.lblNombre.Text = ""
                    Me.txtSupervisor.Text = ""
                    Me.NumEmpleado = 0
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoEncontrado();")
                End If
            End If
            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:establecerControles();")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Evento click del boton aceptar, ingresa o modifica dependiendo de la opcion seleccionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then 'preguntar cuando hay validadores en el codigo
            Try
                Select Case Me.Operacion
                    Case Is = eOperacion.Agregar
                        If Agregar() Then
                            AsignarRole(Me.txtSupervisor.Text)
                            WebUtils.RegistrarScript(Me, "MostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("No a sido posible registrar la nueva categoría.")
                        End If
                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No a sido posible actualizar la información de la categoría.")
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
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Habilita la opción para agregar categorias ocultas, por defecto se agregan con estado visible
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarCategoriasOcultas()
        Me.ddlCategoriasOcultas.Items.Clear()
        Me.ddlCategoriasOcultas.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
        Me.ddlCategoriasOcultas.Items.Add(New ListItem("Ocultar", Constantes.OCULTO))
        Me.ddlCategoriasOcultas.Items.Add(New ListItem("Mostrar", Constantes.VISIBLE))
    End Sub

    ''' <summary>
    ''' Carga combo de Estado y Pertenece a sede, ademas oculta Estado cuando esta en modo insertar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InicializarFormulario()
        CargarEstadoCategoria()
        CargarTaller()
        CargarCategoriasOcultas()
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Categoría"
                'Me.ddlEstado.Attributes.Add("style", "display:none;") 'ocultar etiqueta
                'Me.trEstado.Attributes.Add("style", "display:none;")
                Me.trEstado.Visible = False

            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Categoría"
                Me.trEstado.Visible = True
                ' Me.trEstado.Attributes.Add("style", "display:block;")
                'Me.ddlEstado.Attributes.Add("style", "display:block;") 'mostrar texto
                Try
                    CargarCategoria(WebUtils.LeerParametro(Of String)("pvc_IdCategoria"))
                Catch ex As Exception
                    Throw
                End Try
        End Select
    End Sub


    ''' <summary>
    ''' Obtiene el registro por medio del parametro pvc_IdUbicacion
    ''' </summary>
    ''' <param name="pvc_IdCategoria"></param>
    ''' <remarks></remarks>
    Private Sub CargarCategoria(pvc_IdCategoria As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsCategoria As System.Data.DataSet
        Dim pvc_Condicion As String
        Dim vlo_Row As System.Data.DataRow
        Dim vlo_EntOtmCategoriaServicio As EntOtmCategoriaServicio

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        '0 columna, 1 valor busqueda
        Try

            pvc_Condicion = String.Format("UPPER({0}) = '{1}'", Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO, pvc_IdCategoria.Trim.ToUpper)

            vlo_DsCategoria = vlo_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ListarRegistrosLista(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                pvc_Condicion,
                                String.Empty,
                                False,
                                0,
                                0)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If vlo_DsCategoria.Tables(0) IsNot Nothing AndAlso vlo_DsCategoria.Tables(0).Rows.Count > 0 Then
            Me.txtDescripcion.Text = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_CATEGORIA_SERVICIOLST.DESCRIPCION)
            Me.txtSupervisor.Text = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_CATEGORIA_SERVICIOLST.CEDULA)
            Me.lblNombre.Text = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_CATEGORIA_SERVICIOLST.NOMBRE_EMPLEADO)
            If vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_CATEGORIA_SERVICIOLST.REQUIERE_FICHA_TECNICA) = 1 Then
                Me.chkFicha.Checked = True
            Else
                Me.chkFicha.Checked = False
            End If
            Me.ddlTaller.SelectedValue = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_CATEGORIA_SERVICIOLST.ID_SECTOR_TALLER)
            Me.ddlEstado.SelectedValue = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_CATEGORIA_SERVICIOLST.ESTADO)
            Me.NumEmpleado = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_CATEGORIA_SERVICIOLST.NUM_EMPLEADO_SUPERVISOR)
            Me.UnidadAdministra = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_CATEGORIA_SERVICIOLST.ID_UBICACION_ADMINISTRA)
            Me.ddlCategoriasOcultas.SelectedValue = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_CATEGORIA_SERVICIOLST.OCULTAR_CATEGORIA)
            Me.Categoria = vlo_DsCategoria.Tables(0).Rows(0).ToEntity(Of EntOtmCategoriaServicio)()

            Me.txtSiglas.Text = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_CATEGORIA_SERVICIOLST.SIGLAS_ORDEN_TRABAJO_HIJA)


        Else
            WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub CargarEstadoCategoria() 'CondicionUbicacion

        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
        Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
        Me.ddlEstado.SelectedValue = Estado.ACTIVO
    End Sub

    Private Sub CargarTaller()
        Dim vlo_DsTaller As System.Data.DataSet
        Dim vlo_Wsr_OT_Catalogos As Ws_OT_Catalogos
        Dim pvc_Condicion As String

        Try
            vlo_Wsr_OT_Catalogos = New Ws_OT_Catalogos
            vlo_Wsr_OT_Catalogos.Timeout = -1
            vlo_Wsr_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
            'vlo_Wsr_TC_Catalogos.Url = ConfigurationManager.AppSettings(<ConstanteInterna.MI_SERVICIO_WEB>)

            pvc_Condicion = String.Format("{0} = '{1}' AND {2} = {3}", Modelo.V_OTM_SECTOR_TALLER.TIPO_AREA, Constantes.TIPO_AREA_TAL, Modelo.V_OTM_CATEGORIA_SERVICIO.ID_UBICACION_ADMINISTRA, Me.UnidadAdministra)

            vlo_DsTaller = vlo_Wsr_OT_Catalogos.OTM_SECTOR_TALLER_ListarRegistrosLista(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            pvc_Condicion,
            String.Format("{0} {1}", Modelo.OTM_SECTOR_TALLER.NOMBRE, Ordenamiento.ASCENDENTE),
            False,
            0,
            0)
            'primer string.empty es la condicion
            Me.ddlTaller.Items.Clear()
            Me.ddlTaller.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            If vlo_DsTaller.Tables(0) IsNot Nothing AndAlso vlo_DsTaller.Tables(0).Rows.Count > 0 Then
                With Me.ddlTaller
                    .DataSource = vlo_DsTaller
                    .DataMember = vlo_DsTaller.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_SECTOR_TALLER.NOMBRE
                    .DataValueField = Modelo.V_OTM_SECTOR_TALLER.ID_SECTOR_TALLER
                    .DataBind()
                End With
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_DsTaller IsNot Nothing Then
                vlo_DsTaller.Dispose()

            End If

            If vlo_Wsr_OT_Catalogos IsNot Nothing Then
                vlo_Wsr_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Asiga el role de supervisor
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AsignarRole(ByVal pvc_NumIdentificacion As String)

        Try
            Roles.AddUserToRole(pvc_NumIdentificacion, RolesSistema.OT_SUPERVISOR)
            '  Roles.AddUsersToRoles(New String() {pvc_NumIdentificacion.Trim}, New String() {vlc_RoleName})
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' quita el Role OT_COORDINADOR_MANTENIMIENTO y asigna el OT_COORDINADOR_DISENIO
    ''' </summary>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub QuitarRol(ByVal pvc_NumIdentificacion As String)
        Try
            Roles.RemoveUserFromRole(pvc_NumIdentificacion.Trim, RolesSistema.OT_COORDINADOR_MANTENIMIENTO)
            Roles.AddUserToRole(pvc_NumIdentificacion.Trim, RolesSistema.OT_COORDINADOR_DISENIO)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Asigna el Role OT_COORDINADOR_MANTENIMIENTO y quita el OT_COORDINADOR_DISENIO
    ''' </summary>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AsignarRol(ByVal pvc_NumIdentificacion As String)
        Try
            Roles.AddUserToRole(pvc_NumIdentificacion.Trim, RolesSistema.OT_COORDINADOR_MANTENIMIENTO)
            Roles.RemoveUserFromRole(pvc_NumIdentificacion.Trim, RolesSistema.OT_COORDINADOR_DISENIO)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Asigna los roles de los coordinadores OT_COORDINADOR_MANTENIMIENTO, OT_COORDINADOR_DISENIO dependiendo si posee o no ficha técnica
    ''' </summary>
    ''' <param name="pvn_idSectortaller"></param>
    ''' <param name="pvn_requiereFicha"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AsignarRoles(pvn_idSectortaller As Integer, pvn_requiereFicha As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmSectorTaller As EntOtmSectorTaller
        Dim vlo_Encargado As WsrEU_Curriculo.EntEmpleados

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1

        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntOtmSectorTaller = vlo_Ws_OT_Catalogos.OTM_SECTOR_TALLER_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, pvn_idSectortaller))
            If vlo_EntOtmSectorTaller.Existe Then
                If pvn_requiereFicha = 1 Then
                    vlo_Encargado = CargarFuncionario(vlo_EntOtmSectorTaller.NumEmpleadoCoordinador)
                    If vlo_Encargado.Existe Then
                        QuitarRol(vlo_Encargado.ID_PERSONAL.ToString)
                    End If
                    vlo_Encargado = CargarFuncionario(vlo_EntOtmSectorTaller.NumEmpleadoSustituto)
                    If vlo_Encargado.Existe Then
                        QuitarRol(vlo_Encargado.ID_PERSONAL.ToString)
                    End If
                Else
                    vlo_Encargado = CargarFuncionario(vlo_EntOtmSectorTaller.NumEmpleadoCoordinador)
                    If vlo_Encargado.Existe Then
                        AsignarRol(vlo_Encargado.ID_PERSONAL.ToString)
                    End If
                    vlo_Encargado = CargarFuncionario(vlo_EntOtmSectorTaller.NumEmpleadoSustituto)
                    If vlo_Encargado.Existe Then
                        AsignarRol(vlo_Encargado.ID_PERSONAL.ToString)
                    End If
                End If
            End If

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
    ''' Construye registro para insertar a la base de datos con los datos del usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConstruirRegistro() As EntOtmCategoriaServicio
        Dim vlo_EntOtmCategoriaServicio As EntOtmCategoriaServicio


        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtmCategoriaServicio = New EntOtmCategoriaServicio
        ElseIf Me.Operacion = eOperacion.Modificar Then
            vlo_EntOtmCategoriaServicio = Me.Categoria
        End If

        With vlo_EntOtmCategoriaServicio


            .Descripcion = Me.txtDescripcion.Text.Trim
            .NumEmpleadoSupervisor = Me.NumEmpleado
            If Me.chkFicha.Checked = True Then
                .RequiereFichaTecnica = 1
            Else
                .RequiereFichaTecnica = 0
            End If
            If Me.ddlTaller.SelectedValue = "" Then
                .IdSectorTaller = 0
            Else
                .IdSectorTaller = Me.ddlTaller.SelectedValue
            End If

            .Siglas = Me.txtSiglas.Text.Trim.ToUpper
            .OcultarCategoria = Me.ddlCategoriasOcultas.SelectedValue
            .IdUbicacionAdministra = Me.UnidadAdministra
            .Estado = Me.ddlEstado.SelectedValue
            '.Usuario = New UsuarioActual().UserName 

            AsignarRoles(vlo_EntOtmCategoriaServicio.IdSectorTaller, vlo_EntOtmCategoriaServicio.RequiereFichaTecnica)

        End With
        Return vlo_EntOtmCategoriaServicio
    End Function

    ''' <summary>
    ''' Ingresa nuevo registro en la DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmCategoriaServicio As EntOtmCategoriaServicio

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmCategoriaServicio = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmCategoriaServicio) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Modifica registro en la DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmCategoriaServicio As EntOtmCategoriaServicio

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmCategoriaServicio = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmCategoriaServicio) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal o el numero de empleado que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
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
                String.Format("ID_PERSONAL = '{0}' OR NUM_EMPLEADO = {0}", pvn_IdPersonal))
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
