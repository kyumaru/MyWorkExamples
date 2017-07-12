Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos

Partial Class Catalogos_Frm_OT_CategoriasMaterial
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para el dataset de SUB CATEGORIAS ASOCIADAS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsSubCategoriasAsociadas As Data.DataSet
        Get
            Return CType(ViewState("DsSubCategoriasAsociadas"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsSubCategoriasAsociadas") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
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
    ''' Propiedad para la categoria  a trabajar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property CategoriaMaterial As Wsr_OT_Catalogos.EntOtmCategoriaMaterial
        Get
            Return CType(ViewState("CategoriaMaterial"), Wsr_OT_Catalogos.EntOtmCategoriaMaterial)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmCategoriaMaterial)
            ViewState("CategoriaMaterial") = value
        End Set
    End Property

    ''' <summary>
    ''' Autorizado ubicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
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
    ''' Evento que se ejecuta al cargar la página, inicializa los componentes necesarios
    ''' para el funcionamiento de la página
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)
                If Me.AutorizadoUbicacion.Existe Then
                    InicializarFormulario()
                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no se encuentra autorizado para registrar ordenes de trabajo en ninguna sede.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar, ubicada en la 
    ''' primera columna de cada registro del listado de adjuntos, llama a la función borrar y muestra  un mensaje según 
    ''' la operacion efectuada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vln_Indice As Integer

        Try
            vln_Indice = Convert.ToInt32(CType(sender, ImageButton).CommandName)
            Select Case (Me.Operacion)
                Case Is = eOperacion.Agregar

                    Me.DsSubCategoriasAsociadas.Tables(0).Rows(vln_Indice).Delete()
                Case Is = eOperacion.Modificar
                    Me.DsSubCategoriasAsociadas.Tables(0).Rows(vln_Indice).Delete()
                    Me.DsSubCategoriasAsociadas.Tables(0).AcceptChanges()
            End Select

            If Me.DsSubCategoriasAsociadas IsNot Nothing AndAlso Me.DsSubCategoriasAsociadas.Tables(0).Rows.Count > 0 Then
                Me.rpSubCategorias.DataSource = Me.DsSubCategoriasAsociadas
                Me.rpSubCategorias.DataMember = Me.DsSubCategoriasAsociadas.Tables(0).TableName
                Me.rpSubCategorias.DataBind()
            Else
                With Me.rpSubCategorias
                    .DataSource = Nothing
                    .DataBind()
                End With
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click en el boton aceptar para agregar un nuevo registro
    ''' llama a la funcion procesar y muestra un mensaje segun la operacion realizada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try

                If Me.DsSubCategoriasAsociadas IsNot Nothing AndAlso Me.DsSubCategoriasAsociadas.Tables(0).Rows.Count > 0 Then

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
                    MostrarAlertaError("Debe agregar al menos una sub categoría")
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
    ''' agrega una nueva sub categoria asociada a las categoria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarSubCateg_Click(sender As Object, e As EventArgs) Handles btnAgregarSubCateg.Click
        Try
            AgregarSubCategoria()
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
    ''' <creationDate>20/05/2016</creationDate>
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
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        CargarEstado()
        CargarSubCategorias()
        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Registro"
                Me.trEstado.Visible = False
                CargarEstructuraDsSubCategorias()
            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Registro"
                Me.trEstado.Visible = True
                Try
                    CargarCategoriaMaterial(WebUtils.LeerParametro(Of Integer)("pvn_IdCategoria"))
                    CargaDsSubCategorias()
                Catch ex As Exception
                    Throw
                End Try
        End Select
        Me.DsSubCategoriasAsociadas.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsSubCategoriasAsociadas.Tables(0).Columns(Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.ID_SUBCATEGORIA_MATERIAL)}
    End Sub

    ''' <summary>
    ''' Carga las OTM_SUBCATEGORIA_MATERIAL, segun la condicion de busqueda que obtiene por parametro
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarSubCategorias()
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlSubCategoria.Items.Clear()
            Me.ddlSubCategoria.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_SUBCATEGORIA_MATERIAL_ListarRegistros(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                            String.Format("{0} = '{1}'", Modelo.OTM_SUBCATEGORIA_MATERIAL.ESTADO, Estado.ACTIVO),
                            String.Format("{0} {1}", Modelo.OTM_SUBCATEGORIA_MATERIAL.DESCRIPCION, Ordenamiento.ASCENDENTE),
                            False,
                            0,
                            0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlSubCategoria
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_SUBCATEGORIA_MATERIAL.DESCRIPCION
                    .DataValueField = Modelo.OTM_SUBCATEGORIA_MATERIAL.ID_SUBCATEGORIA_MATERIAL
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
    ''' Agrega una nueva sub categoria asociada
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregarSubCategoria()
        Dim vlo_DrFila As Data.DataRow

        Try

            If Me.DsSubCategoriasAsociadas.Tables(0).Rows.Find(New Object() {Me.ddlSubCategoria.SelectedValue}) Is Nothing Then
                vlo_DrFila = Me.DsSubCategoriasAsociadas.Tables(0).NewRow
                vlo_DrFila.Item(Me.DsSubCategoriasAsociadas.Tables(0).Columns(Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.ID_SUBCATEGORIA_MATERIAL)) = Me.ddlSubCategoria.SelectedValue
                vlo_DrFila.Item(Me.DsSubCategoriasAsociadas.Tables(0).Columns(Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.DESCRIPCION_SUBCATEG_MATE)) = Me.ddlSubCategoria.SelectedItem.ToString
                vlo_DrFila.Item(Me.DsSubCategoriasAsociadas.Tables(0).Columns(Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.USUARIO)) = Me.Usuario.UserName
                vlo_DrFila.Item(Me.DsSubCategoriasAsociadas.Tables(0).Columns(Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.POSEE_REGISTROS_ASOCIADOS)) = 0
                Me.DsSubCategoriasAsociadas.Tables(0).Rows.Add(vlo_DrFila)

                If Me.DsSubCategoriasAsociadas IsNot Nothing AndAlso Me.DsSubCategoriasAsociadas.Tables(0).Rows.Count > 0 Then
                    Me.rpSubCategorias.DataSource = Me.DsSubCategoriasAsociadas
                    Me.rpSubCategorias.DataMember = Me.DsSubCategoriasAsociadas.Tables(0).TableName
                    Me.rpSubCategorias.DataBind()
                Else
                    With Me.rpSubCategorias
                        .DataSource = Nothing
                        .DataBind()
                    End With
                End If

                Me.ddlSubCategoria.SelectedValue = String.Empty
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('La Sub Categoría seleccionada ya se encuentra relacionada.','');")
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' carga las sub categorias asociadas a la categoria actual
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargaDsSubCategorias()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsSubCategoriasAsociadas = vlo_Ws_OT_Catalogos.OTM_SUBCATEGORIA_CATEGOR_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.ID_CATEGORIA_MATERIAL, CategoriaMaterial.IdCategoriaMaterial),
                String.Empty,
                False,
                0,
                0)

            If Me.DsSubCategoriasAsociadas IsNot Nothing AndAlso Me.DsSubCategoriasAsociadas.Tables(0).Rows.Count > 0 Then
                Me.rpSubCategorias.DataSource = Me.DsSubCategoriasAsociadas
                Me.rpSubCategorias.DataMember = Me.DsSubCategoriasAsociadas.Tables(0).TableName
                Me.rpSubCategorias.DataBind()
            Else
                With Me.rpSubCategorias
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
        End Try
    End Sub

    ''' <summary>
    ''' Carga la estructura de la tabla OTM_SUBCATEGORIA_CATEGOR
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEstructuraDsSubCategorias()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsSubCategoriasAsociadas = vlo_Ws_OT_Catalogos.OTM_SUBCATEGORIA_CATEGOR_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("1 = 0"),
                String.Empty,
                False,
                0,
                0)

            If Me.DsSubCategoriasAsociadas IsNot Nothing AndAlso Me.DsSubCategoriasAsociadas.Tables(0).Rows.Count > 0 Then
                Me.rpSubCategorias.DataSource = Me.DsSubCategoriasAsociadas
                Me.rpSubCategorias.DataMember = Me.DsSubCategoriasAsociadas.Tables(0).TableName
                Me.rpSubCategorias.DataBind()
            Else
                With Me.rpSubCategorias
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
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos del registro segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdCategoria">identificacion del registro</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarCategoriaMaterial(pvn_IdCategoria As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.CategoriaMaterial = vlo_Ws_OT_Catalogos.OTM_CATEGORIA_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_MATERIAL.ID_CATEGORIA_MATERIAL, pvn_IdCategoria))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If Me.CategoriaMaterial.Existe Then
            With Me.CategoriaMaterial
                Me.txtDescripcion.Text = .Descripcion
                Me.ddlEstado.SelectedValue = .Estado
            End With
        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    ''' <summary>
    ''' carga el combo de estados permitidos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEstado()
        Try
            Me.ddlEstado.Items.Clear()
            Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
            Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion encargada de contruir el registro
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_Catalogos.EntOtmCategoriaMaterial
        Dim vlo_EntOtmCategoriaMaterial As Wsr_OT_Catalogos.EntOtmCategoriaMaterial

        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtmCategoriaMaterial = New Wsr_OT_Catalogos.EntOtmCategoriaMaterial
            vlo_EntOtmCategoriaMaterial.IdUbicacionAdministra = Me.AutorizadoUbicacion.IdUbicacionAdministra
        Else
            vlo_EntOtmCategoriaMaterial = Me.CategoriaMaterial
        End If

        With vlo_EntOtmCategoriaMaterial
            .Descripcion = Me.txtDescripcion.Text
            .Estado = Me.ddlEstado.SelectedValue
            .Usuario = Me.Usuario.UserName
        End With

        Return vlo_EntOtmCategoriaMaterial
    End Function

    ''' <summary>
    ''' Administra el proceso para agregar un registro
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmCategoriaMaterial As Wsr_OT_Catalogos.EntOtmCategoriaMaterial

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmCategoriaMaterial = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_CATEGORIA_MATERIAL_InsertarCategoriaAsociaciones(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmCategoriaMaterial, Me.DsSubCategoriasAsociadas) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Administra el proceso para modificar un registro
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmCategoriaMaterial As Wsr_OT_Catalogos.EntOtmCategoriaMaterial

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmCategoriaMaterial = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_CATEGORIA_MATERIAL_ModificarCategoriaAsociaciones(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmCategoriaMaterial, Me.DsSubCategoriasAsociadas) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' retorna la ubicacion en la que sta autorizado el usuario 
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/05/2016</creationDate>
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
