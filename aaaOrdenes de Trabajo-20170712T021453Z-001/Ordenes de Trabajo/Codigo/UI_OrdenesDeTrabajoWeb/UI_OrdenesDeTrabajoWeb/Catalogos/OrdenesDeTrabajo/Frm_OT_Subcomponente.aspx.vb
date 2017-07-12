Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo

Partial Class Catalogos_Frm_OT_Subcomponente
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Almacena la operación a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/11/2015</creationDate>
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
    ''' <creationDate>16/11/2015</creationDate>
    Private Property Subcomponente As EntOtmSubcomponente
        Get
            Return CType(ViewState("Espacio"), EntOtmSubcomponente)
        End Get
        Set(value As EntOtmSubcomponente)
            ViewState("Espacio") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el espacio del subcomponente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/03/2016</creationDate>
    Public Property IdEspacio As Integer
        Get
            If ViewState("IdEspacio") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdEspacio"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdEspacio") = value
        End Set
    End Property


#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que carga los datps del subcomponente en pantalla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/11/2015</creationDate>
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
    ''' <creationDate>16/11/2015</creationDate>
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
    ''' Obtiene un registro desde la tabla de subcomponentes y lo carga en memoria
    ''' </summary>
    ''' <param name="pvc_IdSubcomponente"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/11/2015</creationDate>
    Private Sub CargarSubcomponente(pvc_IdSubcomponente As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0} Nombre de la columna 
            '{1} numero de ID_SUBCOMPONENTE

            Me.Subcomponente = vlo_Ws_OT_Catalogos.OTM_SUBCOMPONENTE_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0})={1}", Modelo.OTM_SUBCOMPONENTE.ID_SUBCOMPONENTE, pvc_IdSubcomponente.Trim.ToUpper))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If Me.Subcomponente.Existe Then
            With Me.Subcomponente
                Me.txtDescripcion.Text = .Descripcion
                Me.ddlEstado.SelectedValue = .Estado
                Me.IdEspacio = .IdEspacio
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
    ''' <creationDate>16/11/2015</creationDate>
    Private Sub InicializarFormulario()
        CargarEstado()
        Me.IdEspacio = WebUtils.LeerParametro(Of Integer)("pvc_IdEspacio")
        Me.operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")

        Select Case Me.operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Subcomponente"
                Me.ddlEstado.Enabled = False
            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Subcomponente"
                Me.ddlEstado.Enabled = True
                Try
                    CargarSubcomponente(WebUtils.LeerParametro(Of String)("pvc_IdSubcomponente"))
                Catch ex As Exception
                    Throw
                End Try

        End Select

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
    ''' <creationDate>16/11/2015</creationDate>
    Private Function ConstruirRegistro() As EntOtmSubcomponente
        Dim vlo_EntOtmSubcomponente As EntOtmSubcomponente

        If Me.operacion = eOperacion.Agregar Then
            vlo_EntOtmSubcomponente = New EntOtmSubcomponente
            vlo_EntOtmSubcomponente.Estado = Estado.ACTIVO
        ElseIf Me.operacion = eOperacion.Modificar Then
            vlo_EntOtmSubcomponente = Me.Subcomponente
        End If

        Dim vlo_UsuarioActual = New UsuarioActual()
        With vlo_EntOtmSubcomponente
            .IdSubcomponente = IIf(Me.operacion = eOperacion.Modificar, vlo_EntOtmSubcomponente.IdSubcomponente, 0)
            .IdEspacio = Me.IdEspacio
            .Descripcion = Me.txtDescripcion.Text.Trim.ToUpper
            .Estado = IIf(Me.operacion = eOperacion.Modificar, Me.ddlEstado.SelectedValue, Estado.ACTIVO)
            .Usuario = vlo_UsuarioActual.UserName
            .Orden = IIf(Me.operacion = eOperacion.Agregar, CargarOrden(.IdEspacio), .Orden)

        End With

        Return vlo_EntOtmSubcomponente

    End Function

    ''' <summary>
    '''  Agrega un subcomponente nuevo a la tabla de subcomponentes devuelve false en caso de una descripción repetida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/11/2015</creationDate>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmSubcomponente As EntOtmSubcomponente
        Dim vlo_EntidadOtmSubcomponente As EntOtmSubcomponente

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmSubcomponente = ConstruirRegistro()
        Try

                Return vlo_Ws_OT_Catalogos.OTM_SUBCOMPONENTE_InsertarRegistro(
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


    ''' <summary>
    ''' Modifica un registro en la tabla de subcomponentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/11/2015</creationDate>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmSubcomponente As EntOtmSubcomponente
        Dim vlo_EntidadOtmSubcomponente As EntOtmSubcomponente


        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmSubcomponente = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_SUBCOMPONENTE_ModificarRegistro(
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

    ''' <summary>
    ''' Carga el valor del Último orden para agregar el subcomponente a agregar al final del listado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/11/2015</creationDate>
    Private Function CargarOrden(pvn_idEspacio As Integer) As Integer
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_SUBCOMPONENTE_ObtenerFnOtUltimoordensubcomponente(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_idEspacio)
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
