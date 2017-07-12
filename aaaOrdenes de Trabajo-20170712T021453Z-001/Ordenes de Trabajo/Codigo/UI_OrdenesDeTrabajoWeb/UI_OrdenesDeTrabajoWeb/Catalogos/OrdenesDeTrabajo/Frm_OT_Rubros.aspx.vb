Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo

Partial Class Catalogos_Frm_OT_Rubros
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Almacena la operación a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Property operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto entidad para almacenar datos a mostrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Property Rubro As EntOtmRubroDecisionInicia
        Get
            Return CType(ViewState("Rubro"), EntOtmRubroDecisionInicia)
        End Get
        Set(value As EntOtmRubroDecisionInicia)
            ViewState("Rubro") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el último elemento de la lista
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Public Property UltimoOrden As Integer
        Get
            If ViewState("UltimoOrden") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("UltimoOrden"), Integer)
        End Get
        Set(value As Integer)
            ViewState("UltimoOrden") = value
        End Set
    End Property


#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que carga los datos del rubro en pantalla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
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
    ''' Se ejecuta este evento al dar click en aceptar, segun la operación a realizar se agrega o modifica un registro.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try
                Select Case Me.operacion
                    Case Is = eOperacion.Agregar

                        If Agregar() Then
                            WebUtils.RegistrarScript(Me, "RegistroExitoso", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("La Descripción ingresada ya existe.")
                        End If
                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me, "ActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No ha sido posible actualizar la información del subcomponente.La descripción ingresada ya existe.")
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

#End Region

#Region "Metodos"
    Private Sub CargarEstado()
        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))

    End Sub

    ''' <summary>
    ''' Obtiene un registro desde la tabla de rubros y lo carga en memoria
    ''' </summary>
    ''' <param name="pvn_idRubro"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Sub CargarRubro(pvn_idRubro As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0} Nombre de la columna 
            '{1} numero de ID_RUBRO_DECISION_INICIA

            Me.Rubro = vlo_Ws_OT_Catalogos.OTM_RUBRO_DECISION_INICIA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0}={1}", Modelo.OTM_RUBRO_DECISION_INICIA.ID_RUBRO_DECISION_INICIA, pvn_idRubro))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If Me.Rubro.Existe Then
            With Me.Rubro
                Me.txtDescripcion.Text = .Descripcion
                Me.ddlEstado.SelectedValue = .Estado
            End With
        Else
            WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrrecta", "mostrarAlertaLlaveIncorrrecta();")
        End If


    End Sub

    ''' <summary>
    ''' Inicializa el formulario dependiendo de la operación a realizar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Sub InicializarFormulario()
        CargarEstado()
        Me.operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        Me.UltimoOrden = WebUtils.LeerParametro(Of Integer)("pvn_IdOrdenamiento")
        Select Case Me.operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Rubro para Decisión Inicial"
                Me.ddlEstado.Enabled = False
            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Rubro para Decisión Inicial"
                Me.ddlEstado.Enabled = True
                Try
                    CargarRubro(WebUtils.LeerParametro(Of Integer)("pvc_IdRubro"))
                Catch ex As Exception
                    Throw
                End Try

        End Select

    End Sub

    ''' <summary>
    ''' Carga el valor del Último orden para agregar el rubro al final del listado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Sub CargarUltimoOrden()
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.UltimoOrden = vlo_Ws_OT_Catalogos.OTM_RUBRO_DECISION_INICIA_ObtenerFnOtObtenerUltimoOrden(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
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
    ''' <returns>Entidad de Subcomponente</returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Function ConstruirRegistro() As EntOtmRubroDecisionInicia
        Dim vlo_EntOtmRubroDecisionInicia As EntOtmRubroDecisionInicia

        If Me.operacion = eOperacion.Agregar Then
            vlo_EntOtmRubroDecisionInicia = New EntOtmRubroDecisionInicia
            vlo_EntOtmRubroDecisionInicia.Estado = Estado.ACTIVO
        ElseIf Me.operacion = eOperacion.Modificar Then
            vlo_EntOtmRubroDecisionInicia = Me.Rubro
        End If

        Dim vlo_UsuarioActual = New UsuarioActual()
        With vlo_EntOtmRubroDecisionInicia
            .IdRubroDecisionInicia = IIf(Me.operacion = eOperacion.Modificar, vlo_EntOtmRubroDecisionInicia.IdRubroDecisionInicia, 0)
            .Descripcion = Me.txtDescripcion.Text.Trim.ToUpper
            .Estado = IIf(Me.operacion = eOperacion.Modificar, Me.ddlEstado.SelectedValue, Estado.ACTIVO)
            .Usuario = vlo_UsuarioActual.UserName
            .Orden = IIf(Me.operacion = eOperacion.Agregar, UltimoOrden, .Orden)

        End With

        Return vlo_EntOtmRubroDecisionInicia

    End Function

    ''' <summary>
    '''  Agrega un subcomponente nuevo a la tabla de subcomponentes devuelve false en caso de una descripción repetida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmRubroDecisionInicia As EntOtmRubroDecisionInicia

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            CargarUltimoOrden()
            vlo_EntOtmRubroDecisionInicia = ConstruirRegistro()

            Return vlo_Ws_OT_Catalogos.OTM_RUBRO_DECISION_INICIA_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmRubroDecisionInicia) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try


    End Function


    ''' <summary>
    ''' Modifica un registro en la tabla de subcomponentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmSubcomponente As EntOtmRubroDecisionInicia

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtmSubcomponente = ConstruirRegistro()

            Return vlo_Ws_OT_Catalogos.OTM_RUBRO_DECISION_INICIA_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmSubcomponente) > 0

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
