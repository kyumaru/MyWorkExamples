Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Frm_OT_VistoBuenoAnalisisViabilidadTecnica
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' etapa para la orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property EtapaOrdenTrabajo As Wsr_OT_Catalogos.EntOtmEtapaOrdenTrabajo
        Get
            Return CType(ViewState("EtapaOrdenTrabajo"), Wsr_OT_Catalogos.EntOtmEtapaOrdenTrabajo)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmEtapaOrdenTrabajo)
            ViewState("EtapaOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
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
    ''' Propiedad para el determinar si se desea cambiar el estado y finalizar 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property CambiaEstado As Boolean
        Get
            Return CType(ViewState("CambiaEstado"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("CambiaEstado") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdUbicacion As Integer
        Get
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdOrdenTrabajo As String
        Get
            Return CType(ViewState("IdOrdenTrabajo"), String)
        End Get
        Set(value As String)
            ViewState("IdOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property OrdenTrabajo As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ViabilidadTecnica As EntOttViabilidadTecnica
        Get
            Return CType(ViewState("ViabilidadTecnica"), EntOttViabilidadTecnica)
        End Get
        Set(value As EntOttViabilidadTecnica)
            ViewState("ViabilidadTecnica") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de archivos adjuntos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsAdjuntosInsert As Data.DataSet
        Get
            Return CType(ViewState("DsAdjuntosInsert"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsAdjuntosInsert") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
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
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' descarga la imagen adjunta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivo_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CType(Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO), Byte()))
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se cambia el valor del check selecionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Condicion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtAprobada.CheckedChanged, rbtDevuelta.CheckedChanged
        Try

            If Me.rbtAprobada.Checked Then
                Me.rfvTxtJustificacion.Enabled = False
            End If

            If Me.rbtDevuelta.Checked Then
                Me.rfvTxtJustificacion.Enabled = True
            End If

            WebUtils.RegistrarScript(Me.Page, "GoDown", "GoDown();")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el boton de guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Page.IsValid Then
            Try
                Me.CambiaEstado = False

                If Me.hdnNicEdit.Value.Trim <> String.Empty Then
                    Select Case (Me.Operacion)
                        Case Is = eOperacion.Agregar
                            If Agregar() Then
                                Me.Operacion = eOperacion.Modificar
                                CargarViabilidadTecnica()
                                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa2();")
                            Else
                                MostrarAlertaError("No ha sido posible almacenar la información del nuevo registro")
                            End If

                        Case Is = eOperacion.Modificar
                            If Modificar() Then
                                Me.Operacion = eOperacion.Modificar
                                CargarViabilidadTecnica()
                                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa2();")
                            Else
                                MostrarAlertaError("No ha sido posible actualizar la información del registro")
                            End If
                    End Select
                Else
                    MostrarAlertaError("El Detalle, del campo de texto enriquecido no puede ser vacío.")
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
    ''' Evento que se ejecuta cuando se da click sobre el boton de guardar y enviar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardarEnviar_Click(sender As Object, e As EventArgs) Handles btnGuardarEnviar.Click
        If Page.IsValid Then
            Try
                Me.CambiaEstado = True
                If Me.hdnNicEdit.Value.Trim <> String.Empty Then
                    Select Case (Me.Operacion)
                        Case Is = eOperacion.Agregar
                            If Agregar() Then
                                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
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
                    MostrarAlertaError("El Detalle, del campo de texto enriquesido no puede ser vacío.")
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

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar los diferentess componentes del formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        LeerParametros()
        InicializarControl()
        CargarEncargadoProyecto()
        CargarListaColaborador()

        CargarViabilidadTecnica()
        Me.EtapaOrdenTrabajo = CargarEtapaOrdenTrabajo()
        CargaDsAdjuntos()

        If Me.ViabilidadTecnica.Existe Then
            Me.Operacion = eOperacion.Modificar

            If Me.ViabilidadTecnica.Viabilidad = 1 Then
                Me.rbSiViabilidad.Checked = True
                Me.rfvTxtJustificacion.Enabled = True
            Else
                Me.rbNoViabilidad.Checked = True
                Me.rfvTxtJustificacion.Enabled = False
            End If

            Me.txtEstimacionPresup.Text = Me.ViabilidadTecnica.EstimacionPresupuestaria.ToString("N2")
            Me.hdnNicEdit.Value = HttpUtility.HtmlDecode(Me.ViabilidadTecnica.Detalle)

            Me.rbtDevuelta.Checked = True
            Me.rfvTxtJustificacion.Enabled = True

        Else
            Me.Operacion = eOperacion.Agregar
            Me.rbtDevuelta.Checked = True
            Me.rfvTxtJustificacion.Enabled = True
            Me.rbNoViabilidad.Checked = True
        End If

    End Sub

    ''' <summary>
    ''' Carga los Archivos actuales existente de adjuntos nde viabilidad
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargaDsAdjuntos()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsAdjuntosInsert = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}'",
                              Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ETAPA_ORDEN_TRABAJO, Me.EtapaOrdenTrabajo.IdEtapaOrdenTrabajo,
                              Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_UBICACION, Me.IdUbicacion,
                              Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo),
                String.Empty,
                False,
                0,
                0)

            Me.rpAdjunto.DataSource = Me.DsAdjuntosInsert
            Me.rpAdjunto.DataMember = Me.DsAdjuntosInsert.Tables(0).TableName
            Me.rpAdjunto.DataBind()

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga la estructura de la tabla de adjuntos OTT_ADJUNTO_ORDEN_TRABAJO
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEstructuraDsAdjunto()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsAdjuntosInsert = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarRegistros(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                String.Format("1 = 0"),
                                String.Empty,
                                False,
                                0,
                                0)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub


    ''' <summary>
    ''' Lee los parametros 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub InicializarControl()
        'Asignación de Datos para generar el web user control de la información general
        ctrl_InfoGeneral.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.IdUbicacion.ToString
        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()
    End Sub

    ''' <summary>
    ''' Carga la entidad de la viabilidad tecnica, en caso de existir
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarViabilidadTecnica()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ViabilidadTecnica = vlo_Ws_OT_OrdenesDeTrabajo.OTT_VIABILIDAD_TECNICA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OTT_VIABILIDAD_TECNICA.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_VIABILIDAD_TECNICA.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Se encarga de cargar el encargado actual del proyecto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEncargadoProyecto()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO, Cargo.ENCARGADO),
                String.Format("{0} {1}", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA, Ordenamiento.DESCENDENTE),
                False,
                0,
                0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                Me.lblEncargadoProyecto.Text = vlo_DsDatos.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)
            Else
                Me.lblEncargadoProyecto.Text = "-"
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

    ''' <summary>
    ''' Carga la lista de funcionario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaColaborador()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}' AND {6} <> 0", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION,
                              Me.IdUbicacion, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo,
                              Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO, Cargo.COLABORADOR, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_REAL),
                String.Empty, False, 0, 0)

            With Me.rpColaboradores
                .DataSource = vlo_DsDatos
                .DataMember = vlo_DsDatos.Tables(0).TableName
                .DataBind()
            End With
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
    ''' carga la etapa de la orden
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarEtapaOrdenTrabajo() As Wsr_OT_Catalogos.EntOtmEtapaOrdenTrabajo
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_ETAPA_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_ETAPA_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA))
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Funcion encargada de contruir el registro  de la viabilidad
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_OrdenesDeTrabajo.EntOttViabilidadTecnica
        Dim vlo_EntOttViabilidadTecnica As Wsr_OT_OrdenesDeTrabajo.EntOttViabilidadTecnica

        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOttViabilidadTecnica = New Wsr_OT_OrdenesDeTrabajo.EntOttViabilidadTecnica
            vlo_EntOttViabilidadTecnica.IdUbicacion = Me.IdUbicacion
            vlo_EntOttViabilidadTecnica.IdOrdenTrabajo = Me.IdOrdenTrabajo
        Else
            vlo_EntOttViabilidadTecnica = Me.ViabilidadTecnica
        End If

        With vlo_EntOttViabilidadTecnica
            .Viabilidad = IIf(Me.rbSiViabilidad.Checked, 1, 0)
            .EstimacionPresupuestaria = CType(Me.txtEstimacionPresup.Text.Trim, Double)
            .Detalle = HttpUtility.HtmlEncode(Me.hdnNicEdit.Value)
            .Usuario = Me.Usuario.UserName
        End With

        Return vlo_EntOttViabilidadTecnica
    End Function

    ''' <summary>
    ''' Administra el proceso para agregar una viabilidad tecnica
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttViabilidadTecnica As Wsr_OT_OrdenesDeTrabajo.EntOttViabilidadTecnica

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOttViabilidadTecnica = ConstruirRegistro()

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_VIABILIDAD_TECNICA_InsertarViabilidad(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttViabilidadTecnica, CambiaEstado, IIf(Me.rbSiViabilidad.Checked, True, False), Me.txtJustificacion.Text, Me.Usuario.NumEmpleado) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Administra el proceso para modificar una viabilidad tecnica
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttViabilidadTecnica As Wsr_OT_OrdenesDeTrabajo.EntOttViabilidadTecnica

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOttViabilidadTecnica = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_VIABILIDAD_TECNICA_ModificarViabilidad(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
              vlo_EntOttViabilidadTecnica, CambiaEstado, IIf(Me.rbtAprobada.Checked, True, False), Me.txtJustificacion.Text, Me.Usuario.NumEmpleado) > 0
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
