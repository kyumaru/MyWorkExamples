Imports Utilerias.OrdenesDeTrabajo
Imports Utilerias.GeneradorDeReportes
Imports Wsr_OT_OrdenesDeTrabajo
Imports System.IO
Imports Ionic.Zip

Partial Class OrdenesDeTrabajo_Frm_OT_RevisionAnteProyectoUsuario
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
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
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
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
    ''' <creationDate>11/03/2016</creationDate>
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
    ''' Guarda la condicion para obtener los adjuntos en caso de descargar todos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>19/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property CondicionAdjuntos As String
        Get
            Return CType(ViewState("CondicionAdjuntos"), String)
        End Get
        Set(value As String)
            ViewState("CondicionAdjuntos") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
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
    ''' Propiedad para el Ante Proyecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property Anteproyecto As EntOttAnteproyecto
        Get
            Return CType(ViewState("Anteproyecto"), EntOttAnteproyecto)
        End Get
        Set(value As EntOttAnteproyecto)
            ViewState("Anteproyecto") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de encargado de proyecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property DsEncargadoProyecto As Data.DataSet
        Get
            Return CType(ViewState("DsEncargadoProyecto"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsEncargadoProyecto") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de adjuntos de ante proyecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property DsAdjuntos As Data.DataSet
        Get
            Return CType(ViewState("DsAdjuntos"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsAdjuntos") = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad para el dataset de adjuntos de ante proyecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property DsAdjuntosDescarga As Data.DataSet
        Get
            Return CType(ViewState("DsAdjuntosDescarga"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsAdjuntosDescarga") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property PoseePlantaFisica As Boolean
        Get
            Return CType(ViewState("PoseePlantaFisica"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("PoseePlantaFisica") = value
        End Set
    End Property

    ''' <summary>
    ''' Prpoiedad para cargar datos del empleado en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property EmpleadoEjecuta As WsrEU_Curriculo.EntEmpleados
        Get
            Return CType(ViewState("EmpleadoEjecuta"), WsrEU_Curriculo.EntEmpleados)
        End Get
        Set(value As WsrEU_Curriculo.EntEmpleados)
            ViewState("EmpleadoEjecuta") = value
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
    ''' <creationDate>11/03/2016</creationDate>
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
    ''' evento que se ejecuta cuando se da click en el boton de aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        Try
            Aceptar()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuata cuando se da click en alguno  de  los valores de la condicion de revision
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Condicion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtVistoBueno.CheckedChanged, rbtIndicarObservaciones.CheckedChanged
        Try

            If Me.rbtVistoBueno.Checked Then
                Me.rfvTxtJustificacion.Enabled = False
                Me.trJustificacion.Visible = False
            End If

            If Me.rbtIndicarObservaciones.Checked Then
                Me.rfvTxtJustificacion.Enabled = True
                Me.trJustificacion.Visible = True
            End If

            WebUtils.RegistrarScript(Me.Page, "GoDown", "GoDown();")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' descargar archivo panta fisica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivoPlantaFisica_Click(sender As Object, e As EventArgs) Handles lnkArchivoPlantaFisica.Click
        DescargaArchivo(CType(DsAdjuntos.Tables(0).Rows(0)(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO), Byte()), DsAdjuntos.Tables(0).Rows(0)(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO).ToString)
    End Sub

    ''' <summary>
    ''' descargar archivo foresta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivoForesta_Click(sender As Object, e As EventArgs) Handles lnkArchivoForesta.Click
        If PoseePlantaFisica Then
            DescargaArchivo(CType(DsAdjuntos.Tables(0).Rows(1)(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO), Byte()), DsAdjuntos.Tables(0).Rows(1)(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO).ToString)
        Else
            DescargaArchivo(CType(DsAdjuntos.Tables(0).Rows(0)(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO), Byte()), DsAdjuntos.Tables(0).Rows(0)(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO).ToString)
        End If
    End Sub

    ''' <summary>
    ''' descarga la imagen adjunta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivo_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsAdjuntosDescarga.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CargarEntidadArchivo(Convert.ToInt32(e.CommandName)).Archivo)
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' genera reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGenerarArchivoPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarArchivoPDF.Click
        Try
            MostrarReporte()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al dar click sobre el boton de descargar todos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnDescargarTodos_Click(sender As Object, e As EventArgs) Handles btnDescargarTodos.Click
        Dim vlc_RutaTemporal As String
        Dim vlb_BanderaDescargaArchivo As Boolean
        Dim vlo_Adjuntos As Data.DataSet
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Response.Clear()
            Response.ContentType = "application/zip"
            Response.AppendHeader("content-disposition", "attachment; filename=RevisionAnteproyecto.zip")

            vlc_RutaTemporal = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_RUTA_ESCRIBIR_ARCHIVOS)

            vlo_Adjuntos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.CondicionAdjuntos, String.Empty, False, 0, 0)

            Using zip As New ZipFile
                zip.ParallelDeflateThreshold = -1
                For Each vlo_FilaAdjunto In vlo_Adjuntos.Tables(0).Rows
                    If File.Exists(vlc_RutaTemporal + vlo_FilaAdjunto(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)) Then
                        File.Delete(vlc_RutaTemporal + vlo_FilaAdjunto(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO))
                    End If
                    File.WriteAllBytes(vlc_RutaTemporal + String.Format("{1}-{0}", vlo_FilaAdjunto(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO), vlo_FilaAdjunto(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO)), CType(vlo_FilaAdjunto(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO), Byte()))
                Next
                zip.AddFiles(System.IO.Directory.GetFiles(vlc_RutaTemporal))
                zip.Save(Response.OutputStream)
            End Using

            'Borrar los archivos antes creados
            For Each file As IO.FileInfo In New IO.DirectoryInfo(vlc_RutaTemporal).GetFiles("*.*")
                file.Delete()
            Next


            Response.End()



        Catch ex_Descarga As System.Threading.ThreadAbortException
            vlb_BanderaDescargaArchivo = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Métodos"


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarReporte()

        Try

            Me.EmpleadoEjecuta = CargarFuncionarioNumEmpleado(Me.Usuario.NumEmpleado)

            Me.Session.Add("pvo_EntOttAnteproyecto", Me.Anteproyecto)
            Me.Session.Add("pvc_Condicion", String.Format("{0} = {1} AND {2} = '{3}' AND {4} = 0 AND {5} = (SELECT MAX({5}) FROM {6} WHERE {0} = {1} AND {2} = '{3}' AND {4} = 0)", Modelo.OTT_ANTEPROYECTO.ID_UBICACION, Me.Anteproyecto.IdUbicacion, Modelo.OTT_ANTEPROYECTO.ID_ORDEN_TRABAJO, Me.Anteproyecto.IdOrdenTrabajo, Modelo.OTT_ANTEPROYECTO.EDITABLE, Modelo.OTT_ANTEPROYECTO.VERSION, Modelo.OTT_ANTEPROYECTO.Name))
            Me.Session.Add("pvc_EmpleadoEjecuta", String.Format("{0} {1} {2}", Me.EmpleadoEjecuta.NOMBRE, Me.EmpleadoEjecuta.APELLIDO1, Me.EmpleadoEjecuta.APELLIDO2))

            ScriptManager.RegisterStartupScript(Me, GetType(String), "redirect", String.Format("window.open('../../Controles/Frm_OT_ManejoReportes.aspx?pvc_RutaBase={0}&pvc_NombreReporte={1}&pvc_FormatoReporte={2}&pvn_Concatenar={3}', 'ticker', 'toolbar=no,menubar=no,location=no, scrollbars=YES,scroll=YES');", Utilerias.OrdenesDeTrabajo.Reportes.RUTA_BASE, Utilerias.OrdenesDeTrabajo.Reportes.RPT_OT_ANTE_PROYECTO, FORMATO_REPORTE.PDF, 1), True)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de descargar archivos
    ''' </summary>
    ''' <param name="pvo_Archivo">bytes del archivo a descargar</param>
    ''' <param name="pvc_NombreArchivo">nombre del archivo a descargar</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub DescargaArchivo(pvo_Archivo As Byte(), pvc_NombreArchivo As String)
        pvc_NombreArchivo = pvc_NombreArchivo.Replace(" ", "")
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + pvc_NombreArchivo)
            Response.BinaryWrite(pvo_Archivo)
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar los diferentess componentes del formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        LeerParametros()
        InicializarControl()

        CargarOrdenTrabajo(Me.IdUbicacion, Me.IdOrdenTrabajo)
        CargarAnteProyecto(Me.IdUbicacion, Me.IdOrdenTrabajo)
        CargarEncargadoProyecto(Me.IdUbicacion, Me.IdOrdenTrabajo)
        CargarArchivosAnteProyecto(Me.IdUbicacion, Me.IdOrdenTrabajo)
        CargarArchivosPlantaFisicaForesta(Me.IdUbicacion, Me.IdOrdenTrabajo)
        CargarActividadesContempladas()

        Me.rbtIndicarObservaciones.Checked = True
        Me.rfvTxtJustificacion.Enabled = True
        Me.trJustificacion.Visible = True

    End Sub

    ''' <summary>
    ''' Lee los parametros 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
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
    ''' <creationDate>11/03/2016</creationDate>
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
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
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

        If Not Me.OrdenTrabajo.Existe Then
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la ultima version del anteproyecto segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarAnteProyecto(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_Condicion As String
        Dim vlc_UnidadMedida As String = String.Empty

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_Condicion = String.Format("{0} = {1} AND {2} = '{3}' AND {4} = 0 AND {5} = (SELECT MAX({5}) FROM {6} WHERE {0} = {1} AND {2} = '{3}' AND {4} = 0)", Modelo.OTT_ANTEPROYECTO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ANTEPROYECTO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo, Modelo.OTT_ANTEPROYECTO.EDITABLE, Modelo.OTT_ANTEPROYECTO.VERSION, Modelo.OTT_ANTEPROYECTO.Name)

            Me.Anteproyecto = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ANTEPROYECTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               vlo_Condicion)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

        If Me.Anteproyecto.Existe Then
            With Me.Anteproyecto

                If .UnidadMedida = Cantidad.METRO Then
                    vlc_UnidadMedida = "Metro"
                ElseIf .UnidadMedida = Cantidad.METRO_CUADRADO Then
                    vlc_UnidadMedida = "Metro Cuadrado"
                ElseIf .UnidadMedida = Cantidad.METRO_CUBICO Then
                    vlc_UnidadMedida = "Metro Cúbico"
                End If

                Me.txtDescripción.Text = .Descripcion
                Me.lblCantidad.Text = String.Format("{0} {1}", .Cantidad.ToString, vlc_UnidadMedida)
                Me.lblVersion.Text = .Version
            End With
        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrectaAnteProyecto();")
        End If
    End Sub

    ''' <summary>
    ''' Carga un data set con los encargados del proyecto
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEncargadoProyecto(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.DsEncargadoProyecto = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION, pvn_IdUbicacion, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO, Cargo.ENCARGADO),
                String.Format("{0} {1}", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA, Ordenamiento.DESCENDENTE),
                False,
                0,
                0)

            If Me.DsEncargadoProyecto IsNot Nothing AndAlso Me.DsEncargadoProyecto.Tables(0).Rows.Count > 0 Then
                Me.lblEncargadoProyecto.Text = Me.DsEncargadoProyecto.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)
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
    ''' Carga los archivos ante proyecto de la ot, excluyendo aval planta fisica, y aval de foresta
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarArchivosAnteProyecto(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDocumentoAnteProyecto As Data.DataSet
        Dim vlc_CondicionAdjuntos As String
        vlc_CondicionAdjuntos = String.Empty
        Dim vlc_CondicionBusqueda As String
        Dim vln_Version As Integer

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try


            vln_Version = Me.Anteproyecto.Version
            vlc_CondicionBusqueda = String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5} AND {6} <> {7} AND {8} <> {9}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo, Modelo.OTT_DOCUMENTO_ANTEPROYECT.VERSION, vln_Version, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_TIPO_DOCUMENTO, TipoDocumento.AVAL_FORESTA, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_TIPO_DOCUMENTO, TipoDocumento.AVAL_PLANTA_FISICA)

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
                    If String.IsNullOrWhiteSpace(vlc_CondicionAdjuntos) Then
                        vlc_CondicionAdjuntos = String.Format("{0} = {1}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO, vlo_FilaDocumentoAnteProyecto(Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO).ToString)
                    Else
                        vlc_CondicionAdjuntos = String.Format("{0} OR {1} = {2}", vlc_CondicionAdjuntos, Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO, vlo_FilaDocumentoAnteProyecto(Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO).ToString)
                    End If
                Next

                Me.DsAdjuntos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarVOttAdjuntoLigerolst(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlc_CondicionAdjuntos,
                    String.Empty,
                    False,
                    0,
                    0)

                Me.CondicionAdjuntos = vlc_CondicionAdjuntos
                Me.DsAdjuntosDescarga = Me.DsAdjuntos

                Me.rpAdjunto.DataSource = Me.DsAdjuntos
                Me.rpAdjunto.DataMember = Me.DsAdjuntos.Tables(0).TableName
                Me.rpAdjunto.DataBind()

            Else
                Me.btnDescargarTodos.Visible = False

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
    ''' Carga los archivos de planta fisica y foresta
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarArchivosPlantaFisicaForesta(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDocumentoAnteProyecto As Data.DataSet
        Dim vlc_CondicionAdjuntos As String
        vlc_CondicionAdjuntos = String.Empty

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDocumentoAnteProyecto = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_ANTEPROYECT_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5} AND ({6} = {7} OR {8} = {9})", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo, Modelo.OTT_DOCUMENTO_ANTEPROYECT.VERSION, Me.Anteproyecto.Version, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_TIPO_DOCUMENTO, TipoDocumento.AVAL_FORESTA, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_TIPO_DOCUMENTO, TipoDocumento.AVAL_PLANTA_FISICA),
                String.Empty,
                False,
                0,
                0)

            For Each vlo_FilaDocumentoAnteProyecto In vlo_DsDocumentoAnteProyecto.Tables(0).Rows
                If String.IsNullOrWhiteSpace(vlc_CondicionAdjuntos) Then
                    vlc_CondicionAdjuntos = String.Format("{0} = {1}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO, vlo_FilaDocumentoAnteProyecto(Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO).ToString)
                Else
                    vlc_CondicionAdjuntos = String.Format("{0} OR {1} = {2}", vlc_CondicionAdjuntos, Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO, vlo_FilaDocumentoAnteProyecto(Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO).ToString)
                End If
            Next
            If Not String.IsNullOrWhiteSpace(vlc_CondicionAdjuntos) Then
                Me.DsAdjuntos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarRegistros(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlc_CondicionAdjuntos,
                    String.Format("{0} {1}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO, Ordenamiento.DESCENDENTE),
                    False,
                    0,
                    0)

                If Me.DsAdjuntos IsNot Nothing Then
                    If Me.DsAdjuntos.Tables(0).Rows.Count > 0 Then

                        If Me.DsAdjuntos.Tables(0).Rows(0)(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO).ToString = TipoDocumento.AVAL_PLANTA_FISICA Then
                            Me.trPlantaFisica.Visible = True
                            Me.lnkArchivoPlantaFisica.Text = Me.DsAdjuntos.Tables(0).Rows(0)(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)
                            PoseePlantaFisica = True
                        Else
                            Me.trForesta.Visible = True
                            Me.lnkArchivoForesta.Text = Me.DsAdjuntos.Tables(0).Rows(0)(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)
                            PoseePlantaFisica = False
                        End If
                        If Me.DsAdjuntos.Tables(0).Rows.Count > 1 Then
                            Me.trForesta.Visible = True
                            Me.lnkArchivoForesta.Text = Me.DsAdjuntos.Tables(0).Rows(1)(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)
                        End If

                    End If
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
    ''' Crea y carga un dataset con la información de las actividades contempladas del ante proyecto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarActividadesContempladas()
        Dim vlo_ActividadesContempladas As String()
        Dim vlo_row As Data.DataRow
        Dim vlo_Tabla As Data.DataTable
        Dim vlo_DataSet As Data.DataSet

        Try
            vlo_ActividadesContempladas = Me.Anteproyecto.ActividadesContempladas.Split("¬")

            vlo_Tabla = New Data.DataTable
            vlo_Tabla.Columns.Add("ACTIVIDAD", Type.GetType("System.String"))
            vlo_DataSet = New Data.DataSet
            vlo_DataSet.Tables.Add(vlo_Tabla)

            For i = 0 To vlo_ActividadesContempladas.Length - 1
                vlo_row = vlo_DataSet.Tables(0).NewRow
                vlo_row("ACTIVIDAD") = vlo_ActividadesContempladas(i)
                vlo_DataSet.Tables(0).Rows.Add(vlo_row)
            Next

            If vlo_DataSet IsNot Nothing Then
                If vlo_DataSet.Tables(0).Rows.Count > 0 Then
                    Me.rpActividadesContempladas.DataSource = vlo_DataSet
                    Me.rpActividadesContempladas.DataMember = vlo_DataSet.Tables(0).TableName
                    Me.rpActividadesContempladas.DataBind()
                End If
            End If

        Catch ex As Exception
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' Evento que se comunica con el servicio web, para realizar la accion revision
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Aceptar()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vln_Resultado As Integer

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If Me.rbtVistoBueno.Checked Then

                vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ModificarRegistroRevisionAnteProyectoUsuarioAprobacion(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    Me.OrdenTrabajo, Me.Usuario.UserName)

            Else

                Me.OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.ANTEPROYECTO_DEVUELTO_SOLICITANTE
                Me.OrdenTrabajo.Usuario = Me.Usuario.UserName

                vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_RevisionAnteProyectoUsuario(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    Me.OrdenTrabajo,
                    Me.txtJustificacion.Text,
                    Me.Usuario.NumEmpleado)

            End If

            If vln_Resultado > 0 Then
                If Not Me.rbtVistoBueno.Checked Then
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa('Se ha actualizado la información de la orden.');")
                Else
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa('Se ha actualizado la información de la orden, ha sido aceptada');")
                End If
            Else
                MostrarAlertaError("Se ha producido un error,no se pudo completar la actualización de la orden.")
            End If

        Catch ex As Exception
            If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                Dim vlo_OrdenesDeTrabajoException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                MostrarAlertaError(vlo_OrdenesDeTrabajoException.Message)
            Else
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End If
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionarioNumEmpleado(pvn_NumEmpleado As String) As WsrEU_Curriculo.EntEmpleados
        Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("NUM_EMPLEADO = '{0}'", pvn_NumEmpleado))
        Catch ex As Exception
            Throw
        Finally
            If vlo_WsEU_Curriculo IsNot Nothing Then
                vlo_WsEU_Curriculo.Dispose()
            End If
        End Try
    End Function

    Public Function CargarEntidadArchivo(pvn_IdAdjunto As String) As Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoOrdenTrabajo
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO, pvn_IdAdjunto))
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
