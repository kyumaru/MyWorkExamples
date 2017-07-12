Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Imports Utilerias.Genericos.Extensiones
Partial Class Catalogos_Frm_OT_SectorTaller
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

    Private Property Lugar As EntOtmSectorTaller
        Get
            Return CType(ViewState("Lugar"), EntOtmSectorTaller)
        End Get
        Set(value As EntOtmSectorTaller)
            ViewState("Lugar") = value
        End Set
    End Property

    Private Property NumCoordinador As Integer
        Get
            Return CType(ViewState("NumCoordinador"), Integer)
        End Get
        Set(value As Integer)
            ViewState("NumCoordinador") = value
        End Set
    End Property

    Private Property NumSustituto As Integer
        Get
            Return CType(ViewState("NumSustituto"), Integer)
        End Get
        Set(value As Integer)
            ViewState("NumSustituto") = value
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

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>1/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property


#End Region

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Try
                Me.UnidadAdministra = CType(Session("pvo_UnidadAdministra"), Integer)
                Me.Usuario = New UsuarioActual
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try

        End If
    End Sub

    Protected Sub wuc_EmpleadosEU_Aceptar(pvc_NumeroDeEmpleado As Integer, pvc_Identificacion As String, pvc_NombreCompleto As String) Handles wuc_EmpleadosEU.Aceptar

        Dim vlo_entEmpleados As WsrEU_Curriculo.EntEmpleados


        If Me.wuc_EmpleadosEU.Indicador = 1 Then
            If NumCoordinador <> pvc_NumeroDeEmpleado Then
                vlo_entEmpleados = CargarFuncionario(NumCoordinador)
                Roles.RemoveUserFromRole(vlo_entEmpleados.ID_PERSONAL, RolesSistema.OT_COORDINADOR_MANTENIMIENTO)
                Roles.AddUserToRole(pvc_Identificacion, RolesSistema.OT_COORDINADOR_MANTENIMIENTO)
            End If
            Me.txtIdCoordinador.Text = pvc_Identificacion
            Me.lblNombreCoordinador.Text = pvc_NombreCompleto
            Me.NumCoordinador = pvc_NumeroDeEmpleado
            Me.upTxtIdCoordinador.Update()
            Me.upTxtNombreCoordinador.Update()
        ElseIf Me.wuc_EmpleadosEU.Indicador = 2 Then
            If NumSustituto <> pvc_NumeroDeEmpleado Then
                vlo_entEmpleados = CargarFuncionario(pvc_NumeroDeEmpleado)
                Roles.RemoveUserFromRole(vlo_entEmpleados.ID_PERSONAL, RolesSistema.OT_COORDINADOR_MANTENIMIENTO)
                Roles.AddUserToRole(pvc_Identificacion, RolesSistema.OT_COORDINADOR_MANTENIMIENTO)
            End If
            Me.txtIdSustituto.Text = pvc_Identificacion
            Me.lblNombreSustituto.Text = pvc_NombreCompleto
            Me.NumSustituto = pvc_NumeroDeEmpleado
            Me.upTxtIdSustituto.Update()
            Me.upTxtNombreSustituto.Update()
        End If

        WebUtils.RegistrarScript(Me.Page, "InicializarFormulario", "javascript:inicializarFormulario();ocultarFiltroFuncionario();")
    End Sub

    Protected Sub lnkEjecutarBusquedaCoordinador_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusquedaCoordinador.Click
        Try
            Me.wuc_EmpleadosEU.Indicador = 1
            WebUtils.RegistrarScript(Me.Page, "mostrarPopUpBusquedaFuncionario", "javascript:mostrarPopUp('#PopUpBusquedaFuncionario');cargarLupa();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub lnkEjecutarBusquedaSustituto_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusquedaSustituto.Click
        Try
            Me.wuc_EmpleadosEU.Indicador = 2
            WebUtils.RegistrarScript(Me.Page, "mostrarPopUpBusquedaFuncionario", "javascript:mostrarPopUp('#PopUpBusquedaFuncionario');cargarLupa();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub txtIdCoordinador_TextChanged(sender As Object, e As EventArgs) Handles txtIdCoordinador.TextChanged
        Dim vlo_ServiciosCurriculos As WsrEU_Curriculo.wsEU_Curriculo
        Dim vlo_DsEmpleados As Data.DataSet
        Dim pvc_CondicionBusquedas As String

        Try
            'instanciar y configurar objetos
            vlo_ServiciosCurriculos = New WsrEU_Curriculo.wsEU_Curriculo
            vlo_ServiciosCurriculos.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_ServiciosCurriculos.Timeout = -1

            Me.lblNombreCoordinador.Text = ""

            If Me.txtIdCoordinador.Text <> "" Then
                pvc_CondicionBusquedas = String.Format("{0} = '{1}'", "ID_PERSONAL", Me.txtIdCoordinador.Text)
                vlo_DsEmpleados = vlo_ServiciosCurriculos.Empleados_ListarRegistrosLista(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB), ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), pvc_CondicionBusquedas, "NOMBRE ASC, APELLIDO1 ASC, APELLIDO2 ASC")
                If vlo_DsEmpleados IsNot Nothing AndAlso vlo_DsEmpleados.Tables.Count > 0 AndAlso vlo_DsEmpleados.Tables(0).Rows.Count > 0 Then
                    Me.lblNombreCoordinador.Text = String.Format("{0} {1} {2}", vlo_DsEmpleados.Tables(0).Rows(0)(4), vlo_DsEmpleados.Tables(0).Rows(0)(5), vlo_DsEmpleados.Tables(0).Rows(0)(6))
                    Me.NumCoordinador = vlo_DsEmpleados.Tables(0).Rows(0)(0)
                Else
                    Me.lblNombreCoordinador.Text = ""
                    Me.txtIdCoordinador.Text = ""
                    Me.NumCoordinador = 0
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoEncontrado();")

                End If
                WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:establecerControles();")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub txtIdSustituto_TextChanged(sender As Object, e As EventArgs) Handles txtIdSustituto.TextChanged
        Dim vlo_ServiciosCurriculos As WsrEU_Curriculo.wsEU_Curriculo
        Dim vlo_DsEmpleados As Data.DataSet
        Dim pvc_CondicionBusquedas As String

        Try
            'instanciar y configurar objetos
            vlo_ServiciosCurriculos = New WsrEU_Curriculo.wsEU_Curriculo
            vlo_ServiciosCurriculos.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_ServiciosCurriculos.Timeout = -1

            Me.lblNombreSustituto.Text = ""

            If Me.txtIdSustituto.Text <> "" Then
                pvc_CondicionBusquedas = String.Format("{0} = '{1}'", "ID_PERSONAL", Me.txtIdSustituto.Text)
                vlo_DsEmpleados = vlo_ServiciosCurriculos.Empleados_ListarRegistrosLista(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB), ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), pvc_CondicionBusquedas, "NOMBRE ASC, APELLIDO1 ASC, APELLIDO2 ASC")
                If vlo_DsEmpleados IsNot Nothing AndAlso vlo_DsEmpleados.Tables.Count > 0 AndAlso vlo_DsEmpleados.Tables(0).Rows.Count > 0 Then
                    Me.lblNombreSustituto.Text = String.Format("{0} {1} {2}", vlo_DsEmpleados.Tables(0).Rows(0)(4), vlo_DsEmpleados.Tables(0).Rows(0)(5), vlo_DsEmpleados.Tables(0).Rows(0)(6))
                    Me.NumSustituto = vlo_DsEmpleados.Tables(0).Rows(0)(0)
                Else
                    Me.lblNombreSustituto.Text = ""
                    Me.txtIdSustituto.Text = ""
                    Me.NumSustituto = 0
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoEncontrado();")

                End If
            Else
                Me.NumSustituto = 0
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
                            AgregarCoordinadores()
                            WebUtils.RegistrarScript(Me, "MostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("No a sido posible registrar el nuevo taller o sector.")
                        End If
                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            AgregarCoordinadores()
                            WebUtils.RegistrarScript(Me, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No a sido posible actualizar la información del nuevo taller o sector.")
                        End If
                End Select

                Roles.AddUserToRole(Me.txtIdCoordinador.Text, Utilerias.OrdenesDeTrabajo.Constantes.ROL_COORDINADOR)

                If Me.txtIdSustituto.Text <> "" Then
                    Roles.AddUserToRole(Me.txtIdSustituto.Text, Utilerias.OrdenesDeTrabajo.Constantes.ROL_COORDINADOR)
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

    Protected Sub btnLimpiarFormulario_Click(sender As Object, e As EventArgs) Handles btnLimpiarFormulario.Click
        WebUtils.RegistrarScript(Me.Page, "CargarLupa", "javascript:cargarLupa();")
    End Sub
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Carga combo de Estado y Pertenece a sede, ademas oculta Estado cuando esta en modo insertar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InicializarFormulario()
        CargarEstado()
        Me.rdbSector.Checked = True
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Sector o Taller"
                'Me.ddlEstado.Attributes.Add("style", "display:none;") 'ocultar etiqueta
                'Me.trEstado.Attributes.Add("style", "display:none;")
                Me.trEstado.Visible = False
                Me.Lugar = New EntOtmSectorTaller

            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Sector o Taller"
                Me.trEstado.Visible = True
                ' Me.trEstado.Attributes.Add("style", "display:block;")
                'Me.ddlEstado.Attributes.Add("style", "display:block;") 'mostrar texto
                Try
                    CargarLugar(WebUtils.LeerParametro(Of String)("pvc_IdLugar"))
                Catch ex As Exception
                    Throw
                End Try
        End Select
    End Sub

    ''' <summary>
    ''' Obtiene el registro por medio del parametro pvc_IdUbicacion
    ''' </summary>
    ''' <param name="pvc_IdLugar"></param>
    ''' <remarks></remarks>
    Private Sub CargarLugar(pvc_IdLugar As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsCategoria As System.Data.DataSet
        Dim pvc_Condicion As String

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        '0 columna, 1 valor busqueda
        Try

            pvc_Condicion = String.Format("UPPER({0}) = '{1}'", Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, pvc_IdLugar.Trim.ToUpper)

            vlo_DsCategoria = vlo_Ws_OT_Catalogos.OTM_SECTOR_TALLER_ListarRegistrosLista(
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
            Me.txtNombre.Text = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE)
            Me.txtIdCoordinador.Text = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.CEDULA_COORDINADOR)
            Me.lblNombreCoordinador.Text = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE_COORDINADOR)
            If vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.CEDULA_SUSTITUTO) IsNot System.DBNull.Value Then
                Me.txtIdSustituto.Text = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.CEDULA_SUSTITUTO)
            End If
            If vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE_SUSTITUTO) IsNot System.DBNull.Value Then
                Me.lblNombreSustituto.Text = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE_SUSTITUTO)
            End If
            If vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.TIPO_AREA) = Constantes.TIPO_AREA_SEC Then
                Me.rdbSector.Checked = True
            ElseIf vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.TIPO_AREA) = Constantes.TIPO_AREA_TAL Then
                Me.rdbTaller.Checked = True
            End If
            Me.ddlEstado.SelectedValue = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.ESTADO)
            Me.NumCoordinador = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NUM_EMPLEADO_COORDINADOR)
            Me.NumSustituto = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NUM_EMPLEADO_SUSTITUTO)
            Me.UnidadAdministra = vlo_DsCategoria.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.ID_UBICACION_ADMINISTRA)

            Me.Lugar = vlo_DsCategoria.Tables(0).Rows(0).ToEntity(Of EntOtmSectorTaller)()

        Else
            WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub CargarEstado() 'CondicionUbicacion

        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
        Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
        Me.ddlEstado.SelectedValue = Estado.ACTIVO
    End Sub

    ''' <summary>
    ''' Agrega los coordinadores a la lista de no existir
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregarCoordinadores()
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtfOperarioArea As EntOtfOperarioArea
        Dim vlo_entEmpleados As WsrEU_Curriculo.EntEmpleados

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            'Si el funcionario existe
            vlo_entEmpleados = CargarFuncionario(Me.NumCoordinador)
            If vlo_entEmpleados IsNot Nothing Then
                'Trata de buscarlo en la tabla
                vlo_EntOtfOperarioArea = vlo_Ws_OT_Catalogos.OTF_OPERARIO_AREA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTF_OPERARIO_AREA.NUM_EMPLEADO, vlo_entEmpleados.NUM_EMPLEADO))

                'Si no está presente se procede a agregar al coordinador
                If Not vlo_EntOtfOperarioArea.Existe Then
                    vlo_EntOtfOperarioArea = New EntOtfOperarioArea
                    vlo_EntOtfOperarioArea.NumEmpleado = vlo_entEmpleados.NUM_EMPLEADO
                    vlo_EntOtfOperarioArea.IdSectorTaller = Me.Lugar.IdSectorTaller
                    vlo_EntOtfOperarioArea.CategoriaLaboral = Area.OPERARIO
                    vlo_EntOtfOperarioArea.Estado = Estado.ACTIVO
                    vlo_EntOtfOperarioArea.Usuario = Me.Usuario.UserName

                    vlo_Ws_OT_Catalogos.OTF_OPERARIO_AREA_InsertarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOtfOperarioArea)
                End If
            End If


            'Mismo proceso para agregar al sustituto, si existe

            vlo_entEmpleados = CargarFuncionario(Me.NumSustituto)

            If vlo_entEmpleados.Existe Then
                vlo_EntOtfOperarioArea = vlo_Ws_OT_Catalogos.OTF_OPERARIO_AREA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTF_OPERARIO_AREA.NUM_EMPLEADO, vlo_entEmpleados.NUM_EMPLEADO))

                If Not vlo_EntOtfOperarioArea.Existe Then
                    vlo_EntOtfOperarioArea = New EntOtfOperarioArea
                    vlo_EntOtfOperarioArea.NumEmpleado = vlo_entEmpleados.NUM_EMPLEADO
                    vlo_EntOtfOperarioArea.IdSectorTaller = Me.Lugar.IdSectorTaller
                    vlo_EntOtfOperarioArea.CategoriaLaboral = Area.OPERARIO
                    vlo_EntOtfOperarioArea.Estado = Estado.ACTIVO
                    vlo_EntOtfOperarioArea.Usuario = Me.Usuario.UserName

                    vlo_Ws_OT_Catalogos.OTF_OPERARIO_AREA_InsertarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOtfOperarioArea)
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
    Private Function ConstruirRegistro() As EntOtmSectorTaller
        Dim vlo_EntOtmSectorTaller As EntOtmSectorTaller


        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtmSectorTaller = New EntOtmSectorTaller
        ElseIf Me.Operacion = eOperacion.Modificar Then
            vlo_EntOtmSectorTaller = Me.Lugar
        End If

        With vlo_EntOtmSectorTaller


            .Nombre = Me.txtNombre.Text.Trim
            .NumEmpleadoCoordinador = Me.NumCoordinador
            .NumEmpleadoSustituto = Me.NumSustituto
            If rdbSector.Checked = True Then
                .TipoArea = Constantes.TIPO_AREA_SEC
            ElseIf rdbTaller.Checked = True Then
                .TipoArea = Constantes.TIPO_AREA_TAL
            End If
            .Estado = Me.ddlEstado.SelectedValue
            .IdUbicacionAdministra = Me.UnidadAdministra
            .UsuarioResponsable = Me.Usuario.UserName

        End With
        Return vlo_EntOtmSectorTaller
    End Function

    ''' <summary>
    ''' Ingresa nuevo registro en la DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <changelog>
    '''    <autor>Cesar Bermudez Garcia</autor>
    '''    <fecha>03/02/2016</fecha>
    '''    <cambios>Verificar si el coordinador o el sustituto ya están asignados a un taller o sector</cambios>
    ''' </changelog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmSectorTaller As EntOtmSectorTaller

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmSectorTaller = ConstruirRegistro()

        Try
            Me.Lugar.IdSectorTaller = vlo_Ws_OT_Catalogos.OTM_SECTOR_TALLER_InsertarRegistro(
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                   vlo_EntOtmSectorTaller)

            Return Me.Lugar.IdSectorTaller > 0

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
    '''    <changelog>
    '''    <autor>Cesar Bermudez Garcia</autor>
    '''    <fecha>03/02/2016</fecha>
    '''    <cambios>Se verifica en el BllOtmSectorTaller si el coordinador o el sustituto ya están asignados a un taller o sector</cambios>
    ''' </changelog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmSectorTaller As EntOtmSectorTaller

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmSectorTaller = ConstruirRegistro()

        Try

            Return vlo_Ws_OT_Catalogos.OTM_SECTOR_TALLER_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmSectorTaller) > 0


        Catch ex As Exception
            Dim excep = OrdenesDeTrabajoException.GetFromSoapException(ex)
            MostrarAlertaError(excep.Message.ToString)
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvc_idPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>17/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionario(pvc_idPersonal As String) As WsrEU_Curriculo.EntEmpleados
        Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("ID_PERSONAL = '{0}' OR NUM_EMPLEADO = {0}", pvc_idPersonal))
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
