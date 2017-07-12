Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

''' <summary>
''' Clase que administra la inclusión y modificación de vioas de contrato 
''' </summary>
''' <remarks></remarks>
''' <author>César Bermúdez García</author>
''' <creationDate>14/04/2016</creationDate>
Partial Class Catalogos_Frm_OT_ViasContrato
    Inherits System.Web.UI.Page
#Region "Propiedades"
    ''' <summary>
    ''' Almacena la operación que el usuario desea efectuar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    Private Property operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el valor actual del objeto a insertar/modificar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property ViaContrato As EntOtmViaCompraContrato
        Get
            Return CType(ViewState("ViaContrato"), EntOtmViaCompraContrato)
        End Get
        Set(value As EntOtmViaCompraContrato)
            ViewState("ViaContrato") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para la ubicacion autorizada del usuario que desea registrar ordenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
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
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
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
    ''' <summary>
    ''' Inicializa los componentes del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
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

    ''' <summary>
    ''' Este método se ejecutará al presionar el botón aceptar, dependiendo de la acción modificará o agregará y al final presentará un mensaje
    ''' Si la operación fue exitosa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try
                Select Case Me.operacion
                    Case Is = eOperacion.Agregar

                        If Agregar() Then
                            WebUtils.RegistrarScript(Me, "RegistroExitoso", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("La Descripción o sufijo ingresados ya existen.")
                        End If
                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me, "ActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No ha sido posible actualizar la información del Area Profesional.")
                        End If

                End Select
            Catch ex As Exception
                If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso
                CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                    Dim vlo_TallerCapacitacionException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                    MostrarAlertaError(vlo_TallerCapacitacionException.Message)
                    WebUtils.RegistrarScript(Me, "OcultarAreaDeFiltros", "ocultarAreaFiltrosDeBusqueda();")
                Else
                    Dim vlo_ControlDeErrores As New ControlDeErrores
                    vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
                End If
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta al cambiar el combo de ambitos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlAmbito_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAmbito.SelectedIndexChanged
        Try
            If ddlAmbito.SelectedValue = Ambito.CONTRATACIONES Or ddlAmbito.SelectedValue = Ambito.AMBOS Then
                Me.rfvTxtTopeEconomico.Enabled = True
            Else
                Me.rfvTxtTopeEconomico.Enabled = False
            End If
            upAmbito.Update()
            UpTope.Update()
        Catch ex As Exception
            If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso
                CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                Dim vlo_TallerCapacitacionException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                MostrarAlertaError(vlo_TallerCapacitacionException.Message)
                WebUtils.RegistrarScript(Me, "OcultarAreaDeFiltros", "ocultarAreaFiltrosDeBusqueda();")
            Else
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End If
        End Try
    End Sub


#End Region

#Region "Metodos"

    ''' <summary>
    ''' Carga la lista de estados admisibles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEstado()
        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
    End Sub

    ''' <summary>
    ''' Carga la lista de ambito
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>27/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarAmbito()
        Try
            Me.ddlAmbito.Items.Clear()
            Me.ddlAmbito.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
            Me.ddlAmbito.Items.Add(New ListItem("Contrataciones", Ambito.CONTRATACIONES))
            Me.ddlAmbito.Items.Add(New ListItem("Compras", Ambito.COMPRAS))
            Me.ddlAmbito.Items.Add(New ListItem("Ambos", Ambito.AMBOS))
        Catch ex As Exception
            Throw
        Finally
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene el registro desde la base de datos y lo carga en memoria
    ''' </summary>
    ''' <param name="pvc_IdViaContrato"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarViaContrato(pvc_IdViaContrato As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0} Nombre de la columna 
            '{1} numero de ID_VIA_CONTRATO

            Me.ViaContrato = vlo_Ws_OT_Catalogos.OTM_VIA_COMPRA_CONTRATO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0}={1}", Modelo.OTM_VIA_COMPRA_CONTRATO.ID_VIA_COMPRA_CONTRATO, pvc_IdViaContrato))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If Me.ViaContrato.Existe Then
            With Me.ViaContrato
                Me.txtDescripcion.Text = .Descripcion
                Me.ddlEstado.SelectedValue = .Estado
                Me.txtTopeEconomico.Text = IIf(.TopeEconomico = 0, String.Empty, .TopeEconomico)
                Me.ddlAmbito.SelectedValue = .Ambito
            End With
        Else
            WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrrecta", "mostrarAlertaLlaveIncorrrecta();")
        End If


    End Sub

    ''' <summary>
    ''' Inicializa el formulario dependiendo de la operación recibida por parámetro
    ''' Tambien carga el drop down list de estados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Try
            CargarEstado()
            CargarAmbito()

            Me.Usuario = New UsuarioActual
            Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)
            Me.operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")

            Select Case Me.operacion
                Case Is = eOperacion.Agregar
                    Me.lblAccion.Text = "Agregar Vía de compra"
                    Me.ddlEstado.Enabled = False
                    Me.trEstado.Visible = False
                Case Is = eOperacion.Modificar
                    Me.lblAccion.Text = "Modificar Vía de compra"
                    Me.ddlEstado.Enabled = True

                    Try
                        CargarViaContrato(WebUtils.LeerParametro(Of Integer)("pvc_IdViaContrato"))
                    Catch ex As Exception
                        Throw
                    End Try
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    '''  Construye el registro para ser enviado a la base de datos
    ''' </summary>
    ''' <returns>Entidad de area profesional</returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As EntOtmViaCompraContrato
        Dim vlo_EntOtmViaCompraContrato As EntOtmViaCompraContrato
        Dim vlc_tope As String

        If Me.operacion = eOperacion.Agregar Then
            vlo_EntOtmViaCompraContrato = New EntOtmViaCompraContrato
        ElseIf Me.operacion = eOperacion.Modificar Then
            vlo_EntOtmViaCompraContrato = Me.ViaContrato
        End If
        vlc_tope = Me.txtTopeEconomico.Text
        With vlo_EntOtmViaCompraContrato
            .Ambito = ddlAmbito.SelectedValue
            .Descripcion = Me.txtDescripcion.Text.Trim
            .Estado = IIf(Me.operacion = eOperacion.Modificar, Me.ddlEstado.SelectedValue, Estado.ACTIVO)
            If Not String.IsNullOrWhiteSpace(vlc_tope) Then
                .TopeEconomico = CType(Me.txtTopeEconomico.Text, Double)
            End If
            .Usuario = Me.Usuario.UserName
            .IdUbicacion = Me.AutorizadoUbicacion.IdUbicacionAdministra
        End With

        Return vlo_EntOtmViaCompraContrato

    End Function

    ''' <summary>
    '''  Agrega un area profesional nueva a la tabla de areas profesionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmViaCompraContrato As EntOtmViaCompraContrato

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmViaCompraContrato = ConstruirRegistro()

        Try
            
            If (Me.ddlAmbito.SelectedValue = Ambito.CONTRATACIONES Or Me.ddlAmbito.SelectedValue = Ambito.AMBOS) AndAlso vlo_EntOtmViaCompraContrato.TopeEconomico <= 0 Then
                MostrarAlertaError("El tope económico debe ser mayor a Cero")
            Else
                Return vlo_Ws_OT_Catalogos.OTM_VIA_COMPRA_CONTRATO_InsertarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOtmViaCompraContrato) > 0
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        Return False
    End Function


    ''' <summary>
    ''' Modifica un registro en la tabla de espacios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmViaCompraContrato As EntOtmViaCompraContrato

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmViaCompraContrato = ConstruirRegistro()

        Try

            If (Me.ddlAmbito.SelectedValue = Ambito.CONTRATACIONES Or Me.ddlAmbito.SelectedValue = Ambito.AMBOS) AndAlso vlo_EntOtmViaCompraContrato.TopeEconomico <= 0 Then
                MostrarAlertaError("El tope económico debe ser mayor a Cero")
            Else
                Return vlo_Ws_OT_Catalogos.OTM_VIA_COMPRA_CONTRATO_ModificarRegistro(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        vlo_EntOtmViaCompraContrato)
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
        Return False
    End Function

    ''' <summary>
    ''' Carga una entidad de tipo autorizado ubicacion
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
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
