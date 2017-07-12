Imports Wsr_OT_OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo
Partial Class OrdenesDeTrabajo_Frm_OT_ReciboConformeSolicitante
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

    Private Property OrdenTrabajo As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

    Private Property Trazabilidad As EntOttTrazabilidadProceso
        Get
            Return CType(ViewState("Trazabilidad"), EntOttTrazabilidadProceso)
        End Get
        Set(value As EntOttTrazabilidadProceso)
            ViewState("Trazabilidad") = value
        End Set
    End Property
#End Region

#Region "Eventos"
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
    ''' Evento click del boton aceptar, ingresa o modifica dependiendo de la opcion seleccionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then 'preguntar cuando hay validadores en el codigo
            Try
                Select Case Me.Operacion
                    Case Is = eOperacion.NoAplica

                        If rdbNo.Checked = True Or rdbSi.Checked = True Then
                            If rdbNo.Checked = True And txtObservaciones.Text = "" Then
                                MostrarAlertaError("Las observaciones son obligatorias cuando no se realizó el trabajo correctamente.")
                            Else
                                If rdbSi.Checked = True Then
                                    Me.OrdenTrabajo.EstadoOrdenTrabajo = Utilerias.OrdenesDeTrabajo.EstadoOrden.LIQUIDADA
                                ElseIf rdbNo.Checked = True Then
                                    Me.OrdenTrabajo.EstadoOrdenTrabajo = Utilerias.OrdenesDeTrabajo.EstadoOrden.NO_CONFORME
                                End If
                                If Agregar() Then
                                    WebUtils.RegistrarScript(Me, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                                Else
                                    MostrarAlertaError("No a sido posible registrar el recibido conforme.")
                                End If
                            End If
                        Else
                            MostrarAlertaError("Debe de elegir si el trabajo fue realizado.")
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
    ''' Carga combo de Estado y Pertenece a sede, ademas oculta Estado cuando esta en modo insertar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InicializarFormulario()
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        Select Case Me.Operacion
            Case Is = eOperacion.NoAplica
                Me.lblAccion.Text = "Recibido Conforme"

                Try
                    CargarOrdenTrabajo(WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion"), WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo"))
                Catch ex As Exception
                    Throw
                End Try
        End Select
    End Sub

    Private Sub CargarOrdenTrabajo(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

        If Me.OrdenTrabajo.Existe Then
            With Me.OrdenTrabajo
                CargarHistorialDesconformidad(String.Format("{0} = {1} AND {2} = '{3}' AND ({4} = '{5}' OR {4} = '{6}')", Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_UBICACION, pvn_IdUbicacion, Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo, Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO, EstadoOrden.NO_CONFORME, EstadoOrden.RECIBIDO_CONFORME_SOLICITANTE), String.Format("{0} {1}", Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.FECHA_HORA_EJECUCION, Ordenamiento.ASCENDENTE))
                Me.lblDescripcion.Text = .DescripcionTrabajo
            End With
        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' carga el data set con los registros de trazabilidad de la orden
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>13/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarHistorialDesconformidad(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_TRAZABILIDAD_PROCESO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                For Each vlo_FilaTrazabilidad In vlo_DsDatos.Tables(0).Rows
                    Me.txtDescripciones.Text = Me.txtDescripciones.Text + vlo_FilaTrazabilidad(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.NOMBRE_EMPLEADO).ToString() + ": " + IIf(vlo_FilaTrazabilidad(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.OBSERVACIONES).ToString() = "-", "Coordinador envia para recibido conforme del solicitante.", vlo_FilaTrazabilidad(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.OBSERVACIONES).ToString()) + vbNewLine + vbNewLine
                Next
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
            If vlo_DsDatos IsNot Nothing Then
                vlo_DsDatos.Dispose()

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
    Private Function ConstruirRegistro() As EntOttTrazabilidadProceso
        Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso

        vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

        With vlo_EntOttTrazabilidadProceso

            .IdUbicacion = Me.OrdenTrabajo.IdUbicacion
            .IdOrdenTrabajo = Me.OrdenTrabajo.IdOrdenTrabajo
            .NumEmpleadoEjecuta = Me.OrdenTrabajo.NumEmpleado
            .EstadoOrdenTrabajo = Me.OrdenTrabajo.EstadoOrdenTrabajo
            '            .FechaHoraEjecucion = DateTime.Now
            .Observaciones = txtObservaciones.Text
            .Usuario = New UsuarioActual().UserName

        End With
        Return vlo_EntOttTrazabilidadProceso

    End Function

    ''' <summary>
    ''' Ingresa nuevo registro en la DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOttTrazabilidadProceso = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ActualizaEstadoConforme(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.OrdenTrabajo,
                vlo_EntOttTrazabilidadProceso) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

#End Region
End Class
