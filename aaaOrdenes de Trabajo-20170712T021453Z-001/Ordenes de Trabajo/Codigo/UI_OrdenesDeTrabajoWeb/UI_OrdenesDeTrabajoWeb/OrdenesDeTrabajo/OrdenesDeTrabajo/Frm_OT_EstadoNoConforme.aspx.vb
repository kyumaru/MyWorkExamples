Imports System.Data  'para utilizar data set
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos

Partial Class OrdenesDeTrabajo_Frm_OT_EstadoNoConforme
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/01/2016</creationDate>
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
    ''' propiedad para el id de ubicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/01/2016</creationDate>
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
    ''' propiedad para el id orden trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/01/2016</creationDate>
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
    ''' propiedad para la orden de trabajo 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property OrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo)
        End Get
        Set(value As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de trazabilidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsTrazabilidad As Data.DataSet
        Get
            Return CType(ViewState("DsTrazabilidad"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsTrazabilidad") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' evento que se ejecuta cuando se da carga la pagina, inicializa los didferentes componentes de la pagina
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
                IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
                CargarOrden(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OTT_ORDEN_TRABAJOLST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo))
                CargarHistorialDesconformidad(String.Format("{0} = {1} AND {2} = '{3}' AND ({4} = '{5}' OR {4} = '{6}')", Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO, EstadoOrden.NO_CONFORME, EstadoOrden.RECIBIDO_CONFORME_SOLICITANTE), String.Format("{0} {1}", Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.FECHA_HORA_EJECUCION, Ordenamiento.ASCENDENTE))
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' evento que se ejecut a cuando se da click sobre el boton de enviar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        Try
            If EnviarParaRecibidoConforme() Then
                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
            Else
                MostrarAlertaError("No ha sido posible actualizar la información del registro")
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
    ''' <creationDate>15/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' carga la orden de trabajo respectiva, desde la vista para lista
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrden(pvc_Condicion As String)
        Dim vlo_DsOrden As System.Data.DataSet
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        Try
            vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
            vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
            vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

            vlo_DsOrden = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarRegistrosLista(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            pvc_Condicion,
            String.Empty,
            False,
            0,
            0)

            Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion)

            If vlo_DsOrden.Tables(0) IsNot Nothing AndAlso vlo_DsOrden.Tables(0).Rows.Count > 0 Then
                Me.lblNumeroOrden.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)
                Me.lblPdago.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.NUMERO_ORDEN)
                Me.lblEdificio.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_LUGAR_TRABAJO)
                Me.lblDescripcion.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.DESCRIPCION_TRABAJO)
                Me.lblCategoria.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_CATEGORIA_SERVICIO)
                Me.lblActivididad.Text = vlo_DsOrden.Tables(0).Rows(0).Item(Modelo.V_OTT_ORDEN_TRABAJOLST.DESC_ACTIVIDAD)
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_DsOrden IsNot Nothing Then
                vlo_DsOrden.Dispose()
            End If

            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' carga el data set con los registros de trazabilidad de la orden
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarHistorialDesconformidad(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.DsTrazabilidad = vlo_Ws_OT_OrdenesDeTrabajo.OTT_TRAZABILIDAD_PROCESO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

            If Me.DsTrazabilidad.Tables.Count > 0 AndAlso Me.DsTrazabilidad.Tables(0).Rows.Count > 0 Then
                For Each vlo_FilaTrazabilidad In Me.DsTrazabilidad.Tables(0).Rows
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
    ''' se comunica con el servicio web
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function EnviarParaRecibidoConforme() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.OrdenTrabajo.Usuario = Me.Usuario.UserName

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_EnviarParaRecibidoConforme(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.OrdenTrabajo, Me.txtObservaciones.Text, Me.Usuario.NumEmpleado) > 0

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
