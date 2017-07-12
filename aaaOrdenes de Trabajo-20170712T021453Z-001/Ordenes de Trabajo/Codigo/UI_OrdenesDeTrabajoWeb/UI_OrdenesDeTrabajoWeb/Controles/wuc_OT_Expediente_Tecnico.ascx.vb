Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

Partial Class Controles_wuc_OT_Expediente_Tecnico
    Inherits System.Web.UI.UserControl

#Region "Propiedades"

    ''' <summary>
    ''' id de la ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property IdUbicacion As Integer
        Get
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' id orden trabajo de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property IdOrdenTrabajo As String
        Get
            Return CType(ViewState("IdOrdenTrabajo"), String)
        End Get
        Set(value As String)
            ViewState("IdOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' id de la etapa  OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property IdEtapaOrdenTrabajo As Integer
        Get
            Return CType(ViewState("IdEtapaOrdenTrabajo"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdEtapaOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' data set para almacer los adjuntos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property DsAdjuntos As Data.DataSet
        Get
            Return CType(ViewState("DsAdjuntos"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsAdjuntos") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' descarga la imagen adjunta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivo_Command(sender As Object, e As CommandEventArgs)
        Try
            DescargarArchivo(Convert.ToInt32(e.CommandArgument))
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el check box de seleccionar todos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub chkSeleccionarTodos_CheckedChanged(sender As Object, e As EventArgs) Handles chkSeleccionarTodos.CheckedChanged
        Try
            ProcesarSeleccion(Me.chkSeleccionarTodos.Checked)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Llama los métodos que inicializan el formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Sub Inicializar()
        CargarListaAdjuntos(Me.IdUbicacion, Me.IdOrdenTrabajo, Me.IdEtapaOrdenTrabajo)
    End Sub

    ''' <summary>
    ''' Se comunica con el web service para cargar los datos de adjuntos de la OT, etapa especifica
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <param name="pvn_IdEtapaOrdenTrabajo"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaAdjuntos(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_IdEtapaOrdenTrabajo As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_Condicion As String = String.Empty
        Dim vlo_CantidadVersiones As Integer
        Dim vlc_CondicionBusqueda As String
        Dim vlo_DsDocumentoAnteProyecto As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If Me.IdEtapaOrdenTrabajo = EtapasOrdenTrabajo.ANTEPROYECTO Then

                vlo_CantidadVersiones = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ANTEPROYECTO_ObtenerFnOtVersionAnteproyecto(
                       ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                       ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                       Me.IdUbicacion,
                       Me.IdOrdenTrabajo)

                vlc_CondicionBusqueda = String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.OTT_DOCUMENTO_ANTEPROYECT.VERSION, vlo_CantidadVersiones)

                vlo_DsDocumentoAnteProyecto = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_ANTEPROYECT_ListarRegistrosLista(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlc_CondicionBusqueda,
                    String.Empty,
                    False,
                    0,
                    0)

                If vlo_DsDocumentoAnteProyecto IsNot Nothing AndAlso vlo_DsDocumentoAnteProyecto.Tables(0).Rows.Count > 0 Then
                    For Each vlo_FilaDocumentoAnteProyecto In vlo_DsDocumentoAnteProyecto.Tables(0).Rows
                        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                            vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_ADJUNTO.ID_ADJUNTO_ORDEN_TRABAJO, vlo_FilaDocumentoAnteProyecto(Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO).ToString)
                        Else
                            vlc_Condicion = String.Format("{0} OR {1} = {2}", vlc_Condicion, Modelo.V_OT_ADJUNTO.ID_ADJUNTO_ORDEN_TRABAJO, vlo_FilaDocumentoAnteProyecto(Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO).ToString)
                        End If
                    Next

                    Me.DsAdjuntos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarVOtAdjunto(
                                             ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                             ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                             vlc_Condicion,
                                             String.Format("{0} {1}", Modelo.V_OT_ADJUNTO.DESC_TIPO_DOCUMENTO, Ordenamiento.ASCENDENTE),
                                             False,
                                             0,
                                             0)

                    If Me.DsAdjuntos IsNot Nothing AndAlso Me.DsAdjuntos.Tables(0).Rows.Count > 0 Then
                        Me.rpAdjunto.DataSource = Me.DsAdjuntos
                        Me.rpAdjunto.DataMember = Me.DsAdjuntos.Tables(0).TableName
                        Me.rpAdjunto.DataBind()
                    End If

                End If
            Else
                vlc_Condicion = String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}", Modelo.V_OT_ADJUNTO.ID_UBICACION, pvn_IdUbicacion, Modelo.V_OT_ADJUNTO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo, Modelo.V_OT_ADJUNTO.ID_ETAPA_ORDEN_TRABAJO, pvn_IdEtapaOrdenTrabajo)

                Me.DsAdjuntos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarVOtAdjunto(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               vlc_Condicion,
               String.Format("{0} {1}", Modelo.V_OT_ADJUNTO.DESC_TIPO_DOCUMENTO, Ordenamiento.ASCENDENTE),
               False,
               0,
               0)

                If Me.DsAdjuntos IsNot Nothing AndAlso Me.DsAdjuntos.Tables(0).Rows.Count > 0 Then
                    Me.rpAdjunto.DataSource = Me.DsAdjuntos
                    Me.rpAdjunto.DataMember = Me.DsAdjuntos.Tables(0).TableName
                    Me.rpAdjunto.DataBind()
                End If

            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Descarga un archivo, de la tabla de OTT_ADJUNTO_ORDEN_TRABAJO
    ''' </summary>
    ''' <param name="pvn_IdAdjuntoOrdenTrabajo"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub DescargarArchivo(pvn_IdAdjuntoOrdenTrabajo As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttAdjuntoOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoOrdenTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttAdjuntoOrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO, pvn_IdAdjuntoOrdenTrabajo))

            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + vlo_EntOttAdjuntoOrdenTrabajo.NombreArchivo)
            Response.BinaryWrite(vlo_EntOttAdjuntoOrdenTrabajo.Archivo)
            Response.End()

        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Determina cual accion sera la que se debe realizar, seleccionar todos o desseleccionar ntodos
    ''' </summary>
    ''' <param name="pvb_SeleccionarTodos"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ProcesarSeleccion(pvb_SeleccionarTodos As Boolean)
        Dim vlo_CheckBox As CheckBox

        Try
            For Each item In Me.rpAdjunto.Items
                vlo_CheckBox = CType(item.FindControl("chkArchivo"), CheckBox)
                vlo_CheckBox.Checked = pvb_SeleccionarTodos
            Next
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region


#Region "Funciones"

    ''' <summary>
    ''' retorna el repeater
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Function RetornaRepeater() As Repeater
        Try
            Return rpAdjunto
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Function

#End Region

End Class
