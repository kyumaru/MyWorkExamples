Imports Wsr_OT_Catalogos
Imports System.Data
Imports Utilerias.OrdenesDeTrabajo


''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Partial Class Catalogos_Lst_OT_AsignacionRolDelegado
    Inherits System.Web.UI.Page

#Region "Propiedades"
    ''' <summary>
    ''' Profesionales ingresados por el usuario a la tabla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>25/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property DsUsuarios As DataTable
        Get
            Return CType(ViewState("DsUsuarios"), DataTable)
        End Get
        Set(value As DataTable)
            ViewState("DsUsuarios") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para la ubicacion autorizada del usuario que desea registrar ordenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>20/04/2016</creationDate>
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
    ''' Propiedad para el usuario en sesion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>20/04/2016</creationDate>
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
            Me.Usuario = New UsuarioActual
            Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)
            If Me.AutorizadoUbicacion.Existe Then
                inicializarSetDatos()
                Buscar()
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('No se pudo encontrar ninguna sede a su cargo por lo que no está autorizado a realizar acciones en esta pantalla. Para gestionar los permisos necesarios contacte al administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
            End If
        End If
    End Sub

    Protected Sub lnkEjecutarBusquedaFuncionario_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusquedaFuncionario.Click
        Try
            WebUtils.RegistrarScript(Me.Page, "mostrarPopUpBusquedaFuncionario", "javascript:mostrarPopUp('#PopUpBusquedaFuncionario');cargarLupa();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
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
        Me.upTxtIdentificacion.Update()
        Me.upTxtNombre.Update()
        WebUtils.RegistrarScript(Me.Page, "InicializarFormulario", "javascript:inicializarFormulario();ocultarFiltroFuncionario();")
    End Sub

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

            If Me.txtIdentificacion.Text <> "" Then
                pvc_CondicionBusquedas = String.Format("{0} = '{1}'", "ID_PERSONAL", Me.txtIdentificacion.Text)
                vlo_DsEmpleados = vlo_ServiciosCurriculos.Empleados_ListarRegistrosLista(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB), ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), pvc_CondicionBusquedas, "NOMBRE ASC, APELLIDO1 ASC, APELLIDO2 ASC")
                If vlo_DsEmpleados IsNot Nothing AndAlso vlo_DsEmpleados.Tables.Count > 0 AndAlso vlo_DsEmpleados.Tables(0).Rows.Count > 0 Then
                    Me.lblNombre.Text = String.Format("{0} {1} {2}", vlo_DsEmpleados.Tables(0).Rows(0)(4), vlo_DsEmpleados.Tables(0).Rows(0)(5), vlo_DsEmpleados.Tables(0).Rows(0)(6))
                Else
                    Me.lblNombre.Text = ""
                    Me.txtIdentificacion.Text = ""
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

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("../../Genericos/Frm_MenuPrincipal.aspx")
    End Sub

    Protected Sub rpLugares_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpUsuarios.ItemDataBound
        Dim vlo_IbBorrar As ImageButton

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_IbBorrar = e.Item.FindControl("ibBorrar")
            If vlo_IbBorrar IsNot Nothing Then
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub

    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            If Borrar(CType(sender, ImageButton).CommandArgument) Then
                Buscar()
                MostrarAlertaRegistroBorrado()

            Else
                MostrarAlertaRegistroNoBorrado()
            End If
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click en el boton de agregar para la evaluacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarFuncionario_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            AgregaFuncionariosDataTable()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub MostrarAlertaRegistroBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroBorrado", "mostrarAlertaRegistroBorrado();")
    End Sub

    Private Sub MostrarAlertaRegistroNoBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroNoBorrado", "mostrarAlertaRegistroNoBorrado();")
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarSetDatos()
        Dim vlo_columna As DataColumn
        Dim vlo_llaves(1) As DataColumn


        'Se crea un nuevo datatabla 
        Me.DsUsuarios = New DataTable

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = "CEDULA"
        'Se agrega la columna configurada al set de datos
        DsUsuarios.Columns.Add(vlo_columna)
        'Se agrega al arreglo de llaves primarias la columna
        vlo_llaves(0) = vlo_columna
        'Y se configura el set de datos para que busque por formatos admitidos como llave primaria.
        Me.DsUsuarios.PrimaryKey = vlo_llaves

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = "NOMBRE_EMPLEADO"
        'Se agrega la columna configurada al set de datos
        DsUsuarios.Columns.Add(vlo_columna)

    End Sub

    ''' <summary>
    ''' Metodo para buscar con una condicion enviada por parametro
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Buscar()
        Dim vlo_UsuariosConRole() As String


        Try
            vlo_UsuariosConRole = Roles.GetUsersInRole(Utilerias.OrdenesDeTrabajo.RolesSistema.OT_DELEGADO_JEFE_SECCION)

            If vlo_UsuariosConRole.Length > 0 Then
                Me.pnRpLugares.TotalPaginasLista = 1
                Me.pnRpLugares.Dibujar()
                lblCantidadRegistro.Visible = True
                lblCantidadRegistro.Text = String.Format("Cantidad de Usuarios con Rol de jefe delegado: {0}", vlo_UsuariosConRole.Length)
                CargarLista(vlo_UsuariosConRole)
            Else
                Me.lblCantidadRegistro.Visible = False
                Me.lblCantidadRegistro.Text = String.Empty
                'MostrarAlertaNoHayDatos()
            End If
        Catch ex As Exception
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' agrega un funcionario al dataset, estos se encuentran en memoria, y son insertados en la 
    ''' base de datos hasta el final, es decir un vez que se da click sobre el boton de aceptar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregaFuncionariosDataTable()
        Dim vlo_DrNuevaFila As DataRow
        Dim vlo_empleado As WsrEU_Curriculo.EntEmpleados
        Try

            Dim vln_Cedula As Integer
            vln_Cedula = CType(Me.txtIdentificacion.Text, Integer)


            If Me.DsUsuarios.Rows.Find(New Object() {vln_Cedula}) Is Nothing Then

                vlo_empleado = CargarFuncionario(vln_Cedula)

                'Se recorre la lista de todos los profesionales disponibles para agregar el correspondiente
                vlo_DrNuevaFila = Me.DsUsuarios.NewRow
                vlo_DrNuevaFila.Item("CEDULA") = vlo_empleado.ID_PERSONAL
                vlo_DrNuevaFila.Item("NOMBRE_EMPLEADO") = String.Format("{0} {1} {2}", vlo_empleado.NOMBRE, vlo_empleado.APELLIDO1, vlo_empleado.APELLIDO2)
                Me.DsUsuarios.Rows.Add(vlo_DrNuevaFila)

                Roles.AddUserToRole(vlo_empleado.ID_PERSONAL, RolesSistema.OT_DELEGADO_JEFE_SECCION)

                lblCantidadRegistro.Text = String.Format("Cantidad de Usuarios con Rol de jefe delegado: {0}", DsUsuarios.Rows.Count)

                If Me.DsUsuarios.Rows.Count > 0 Then
                    With Me.rpUsuarios
                        .DataSource = Me.DsUsuarios
                        .DataMember = Me.DsUsuarios.TableName
                        .DataBind()
                        Me.rpUsuarios.Visible = True
                    End With

                Else
                    With Me.rpUsuarios
                        .DataSource = Nothing
                        .DataBind()
                    End With
                    Me.rpUsuarios.Visible = False
                End If

            Else
                MostrarAlertaError("Usuario repetido en la lista")
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Private Sub CargarLista(pvo_UsuariosConRole As String())
        Dim vlo_empleado As WsrEU_Curriculo.EntEmpleados
        Dim vlo_NuevaFila As DataRow
        For Each vlc_Cedula As String In pvo_UsuariosConRole
            vlo_empleado = CargarFuncionario(vlc_Cedula)
            vlo_NuevaFila = Me.DsUsuarios.NewRow
            vlo_NuevaFila.Item("CEDULA") = vlo_empleado.ID_PERSONAL
            vlo_NuevaFila.Item("NOMBRE_EMPLEADO") = String.Format("{0} {1} {2}", vlo_empleado.NOMBRE, vlo_empleado.APELLIDO1, vlo_empleado.APELLIDO2)
            Me.DsUsuarios.Rows.Add(vlo_NuevaFila)

        Next

        If Me.DsUsuarios.Rows.Count > 0 Then
            With Me.rpUsuarios
                .DataSource = Me.DsUsuarios
                .DataMember = Me.DsUsuarios.TableName
                .DataBind()
                Me.rpUsuarios.Visible = True
            End With

        Else
            With Me.rpUsuarios
                .DataSource = Nothing
                .DataBind()
            End With
            Me.rpUsuarios.Visible = False
        End If

    End Sub

#End Region

#Region "Funciones"
    ''' <summary>
    ''' Funcion para quitar el rol a un usuario
    ''' </summary>
    ''' <param name="pvc_Cedula"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Borrar(pvc_Cedula As String) As Boolean
        Try

            Me.DsUsuarios.Rows.Find(New Object() {pvc_Cedula}).Delete()

            Roles.RemoveUserFromRole(pvc_Cedula, RolesSistema.OT_DELEGADO_JEFE_SECCION)

            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>25/2/2016</creationDate>
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
    ''' Carga una entidad de tipo autorizado ubicacion
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>20/04/2016</creationDate>
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

#End Region

End Class
