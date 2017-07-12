Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos

Partial Class Catalogos_Frm_OT_AlmacenesBodegas
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/05/2016</creationDate>
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
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/05/2016</creationDate>
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
    ''' Propiedad para la el almacen/bodega a trabajar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property AlmacenBodega As Wsr_OT_Catalogos.EntOtmAlmacenBodega
        Get
            Return CType(ViewState("AlmacenBodega"), Wsr_OT_Catalogos.EntOtmAlmacenBodega)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAlmacenBodega)
            ViewState("AlmacenBodega") = value
        End Set
    End Property

    ''' <summary>
    ''' Autorizado ubicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/05/2016</creationDate>
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
    ''' <creationDate>24/05/2016</creationDate>
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
    ''' Evento que se ejecuta cuando se da click en el boton aceptar para agregar un nuevo registro
    ''' llama a la funcion procesar y muestra un mensaje segun la operacion realizada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try

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
    ''' evento que se ejecuta al dar cambiar el valor del combo de tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipo.SelectedIndexChanged
        Try
            If Me.ddlTipo.SelectedValue <> String.Empty AndAlso Me.ddlTipo.SelectedValue = Tipo.BODEGA Then
                Me.trTaller.Visible = True
            Else
                Me.ddlTaller.SelectedValue = String.Empty
                Me.trTaller.Visible = False
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
    ''' <creationDate>24/05/2016</creationDate>
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
    ''' <creationDate>24/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        Dim vlo_AlmacenBodegaCombo As Wsr_OT_Catalogos.EntOtmAlmacenBodega
        CargarEstado()
        CargarTipo()
        CargarComboSectorTaller()
        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Registro"
                Me.trEstado.Visible = False

                vlo_AlmacenBodegaCombo = CargarAlmacenUbicacionActual()

                If vlo_AlmacenBodegaCombo.Existe Then
                    Me.trTaller.Visible = True
                End If

            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Registro"
                Me.trEstado.Visible = True
                Try
                    CargarAlmacenBodega(WebUtils.LeerParametro(Of Integer)("pvn_IdAlmacenBodega"))
                Catch ex As Exception
                    Throw
                End Try
        End Select
    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos del registro segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdAlmacenBodega">identificacion del registro</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarAlmacenBodega(pvn_IdAlmacenBodega As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.AlmacenBodega = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_ALMACEN_BODEGA.ID_ALMACEN_BODEGA, pvn_IdAlmacenBodega))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If Me.AlmacenBodega.Existe Then

            If (Me.Operacion = eOperacion.Modificar) And (Me.AlmacenBodega.Tipo = Tipo.ALMACEN) Then
                Me.ddlTipo.Items.Add(New ListItem("Almacén", Tipo.ALMACEN))
            End If

            With Me.AlmacenBodega
                Me.txtDescripcion.Text = .Descripcion
                Me.ddlTipo.SelectedValue = .Tipo
                If .Tipo = Tipo.BODEGA Then
                    Me.ddlTaller.SelectedValue = .IdSectorTaller
                    Me.trTaller.Visible = True
                End If
                Me.ddlEstado.SelectedValue = .Estado

                If .Tipo = Tipo.ALMACEN Then
                    Me.ddlTipo.Enabled = False
                    Me.ddlEstado.Enabled = False
                End If

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
    ''' <creationDate>24/05/2016</creationDate>
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

    ''' <summary>
    ''' carga el combo de estados permitidos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarTipo()
        Dim vlo_EntOtmAlmacenBodega As EntOtmAlmacenBodega
        Try
            Me.ddlTipo.Items.Clear()

            vlo_EntOtmAlmacenBodega = CargarAlmacenUbicacionActual()

            If (Not vlo_EntOtmAlmacenBodega.Existe) Then
                Me.ddlTipo.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
                Me.ddlTipo.Items.Add(New ListItem("Almacén", Tipo.ALMACEN))
           
            End If

            Me.ddlTipo.Items.Add(New ListItem("Bodega", Tipo.BODEGA))
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' carga el combo de talleres
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboSectorTaller()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlTaller.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_SECTOR_TALLER_ListarRegistros(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = '{1}' AND {2} = {3}",
                             Modelo.OTM_SECTOR_TALLER.ESTADO, Estado.ACTIVO,
                             Modelo.OTM_SECTOR_TALLER.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra),
               String.Format("{0} {1}", Modelo.OTM_SECTOR_TALLER.NOMBRE, Ordenamiento.ASCENDENTE),
               False,
               0,
               0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlTaller
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_SECTOR_TALLER.NOMBRE
                    .DataValueField = Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER
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

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion encargada de contruir el registro
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_Catalogos.EntOtmAlmacenBodega
        Dim vlo_EntOtmAlmacenBodega As Wsr_OT_Catalogos.EntOtmAlmacenBodega

        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtmAlmacenBodega = New Wsr_OT_Catalogos.EntOtmAlmacenBodega
            vlo_EntOtmAlmacenBodega.IdUbicacionAdministra = Me.AutorizadoUbicacion.IdUbicacionAdministra
        Else
            vlo_EntOtmAlmacenBodega = Me.AlmacenBodega
        End If

        With vlo_EntOtmAlmacenBodega
            .Descripcion = Me.txtDescripcion.Text
            .Tipo = Me.ddlTipo.SelectedValue
            .IdSectorTaller = IIf(Me.ddlTaller.SelectedValue <> String.Empty, Me.ddlTaller.SelectedValue, 0)
            .Estado = Me.ddlEstado.SelectedValue
            .Usuario = Me.Usuario.UserName
        End With

        Return vlo_EntOtmAlmacenBodega
    End Function

    ''' <summary>
    ''' Administra el proceso para agregar un registro
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmAlmacenBodega As Wsr_OT_Catalogos.EntOtmAlmacenBodega

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmAlmacenBodega = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmAlmacenBodega) > 0
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
    ''' <creationDate>24/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmAlmacenBodega As Wsr_OT_Catalogos.EntOtmAlmacenBodega

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmAlmacenBodega = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmAlmacenBodega) > 0
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
    ''' <creationDate>24/05/2016</creationDate>
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>31/10/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarAlmacenUbicacionActual() As Wsr_OT_Catalogos.EntOtmAlmacenBodega
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'",
                                Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra,
                                Modelo.OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))

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
