Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos

''' <summary>
''' Clase para presentar la información de los periodos de cierre
''' </summary>
''' <remarks></remarks>
''' <author>César Bermúdez García</author>
''' <creationDate>18/1/2016</creationDate>
Partial Class Catalogos_Frm_OT_PeriodoCierre
    Inherits System.Web.UI.Page
#Region "Propiedades"
    ''' <summary>
    ''' Propiedad que define la operación a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>18/1/2016</creationDate>
    Private Property operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que almacena el valor actual del periodo de cierre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>18/1/2016</creationDate>
    Private Property PeriodoCierre As EntOtfPeriodoCierre
        Get
            Return CType(ViewState("PeriodoCierre"), EntOtfPeriodoCierre)
        End Get
        Set(value As EntOtfPeriodoCierre)
            ViewState("PeriodoCierre") = value
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
    ''' <creationDate>18/1/2016</creationDate>
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
    ''' <creationDate>18/1/2016</creationDate>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try
                Select Case Me.operacion
                    Case Is = eOperacion.Agregar

                        If Agregar() Then
                            WebUtils.RegistrarScript(Me, "RegistroExitoso", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("La Unidad interna ingresada ya existe.")
                        End If
                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me, "ActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No ha sido posible actualizar la información del periodo de cierre")
                        End If

                End Select
            Catch ex As Exception
                MostrarAlertaError("La Unidad interna ingresada ya existe.")
                'If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso
                'CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                '    Dim vlo_TallerCapacitacionException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                '    MostrarAlertaError(vlo_TallerCapacitacionException.Message)
                '    WebUtils.RegistrarScript(Me, "OcultarAreaDeFiltros", "ocultarAreaFiltrosDeBusqueda();")
                'Else
                '    Dim vlo_ControlDeErrores As New ControlDeErrores
                '    vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
                'End If
            End Try
        End If
    End Sub

#End Region

#Region "Metodos"

    Private Sub cargarUnidadInterna()
        Me.ddlUnidadInterna.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODAS, String.Empty))
        Me.ddlUnidadInterna.Items.Add(New ListItem("Mantenimiento", UnidadCierre.MANTENIMIENTO))
        Me.ddlUnidadInterna.Items.Add(New ListItem("Diseño", UnidadCierre.DISENIO))
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub


    ''' <summary>
    ''' Inicializa el formulario dependiendo de la operación recibida por parámetro
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>18/1/2016</creationDate>
    Private Sub InicializarFormulario()
        cargarUnidadInterna()

        Me.operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")

        Select Case Me.operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Periodo de cierre"
            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Periodo de cierre"
                Try
                    CargarPeriodo(WebUtils.LeerParametro(Of String)("pvc_IdPeriodo"))
                Catch ex As Exception
                    Throw
                End Try

        End Select

    End Sub

    ''' <summary>
    ''' Obtiene el registro desde la base de datos y lo carga en memoria
    ''' </summary>
    ''' <param name="pvc_IdPeriodoCierre"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>18/1/2016</creationDate>
    Private Sub CargarPeriodo(pvc_IdPeriodoCierre As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0} Nombre de la columna 
            '{1} numero de ID_PERIODO_CIERRE

            Me.PeriodoCierre = vlo_Ws_OT_Catalogos.OTF_PERIODO_CIERRE_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0}={1}", Modelo.OTF_PERIODO_CIERRE.ID_PERIODO_CIERRE, pvc_IdPeriodoCierre.Trim.ToUpper))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If Me.PeriodoCierre.Existe Then
            With Me.PeriodoCierre
                Me.ddlUnidadInterna.SelectedValue = .UnidadCierre
                Me.txtFiltroFechaDesde.Text = .FechaInicioCierre
                Me.txtFiltroFechaHasta.Text = .FechaFinCierre

            End With
        Else
            WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrrecta", "mostrarAlertaLlaveIncorrrecta();")
        End If


    End Sub

#End Region

#Region "Funciones"
    ''' <summary>
    '''  Construye el registro para ser enviado a la base de datos
    ''' </summary>
    ''' <returns>Entidad de Periodo de cierre</returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>18/1/2016</creationDate>
    Private Function ConstruirRegistro() As EntOtfPeriodoCierre
        Dim vlo_EntOtfPeriodoCierre As EntOtfPeriodoCierre

        If Me.operacion = eOperacion.Agregar Then
            vlo_EntOtfPeriodoCierre = New EntOtfPeriodoCierre

        ElseIf Me.operacion = eOperacion.Modificar Then
            vlo_EntOtfPeriodoCierre = Me.PeriodoCierre

        End If

        Dim vlo_UsuarioActual = New UsuarioActual()
        Dim vlo_Ubicacion = CargarAutorizadoUbicacion(vlo_UsuarioActual.NumEmpleado)

        With vlo_EntOtfPeriodoCierre
            .FechaFinCierre = CType(Me.txtFiltroFechaHasta.Text, Date)
            .FechaInicioCierre = CType(Me.txtFiltroFechaDesde.Text, Date)
            .IdUbicacion = vlo_Ubicacion.IdUbicacionAdministra
            .Usuario = vlo_UsuarioActual.UserName
            .UnidadCierre = Me.ddlUnidadInterna.SelectedValue
        End With

        Return vlo_EntOtfPeriodoCierre

    End Function

    ''' <summary>
    '''  Agrega una unidad de tiempo nueva a la tabla de periodo de cierre
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>18/1/2016</creationDate>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtfPeriodoCierre As EntOtfPeriodoCierre

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtfPeriodoCierre = ConstruirRegistro()
            Return vlo_Ws_OT_Catalogos.OTF_PERIODO_CIERRE_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtfPeriodoCierre) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try


    End Function


    ''' <summary>
    ''' Modifica un registro en la tabla de periodo de cierre
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>18/1/2016</creationDate>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtfPeriodoCierre As EntOtfPeriodoCierre

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtfPeriodoCierre = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTF_PERIODO_CIERRE_ModificarRegistro(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            vlo_EntOtfPeriodoCierre) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

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
