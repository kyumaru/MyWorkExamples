Imports System.Data
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo


Partial Class OrdenesDeTrabajo_Frm_OT_Planos
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>06/04/2016</creationDate>
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
    ''' Indica a cual página se debe regresar.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property PaginaRegresar As String
        Get
            Return CType(ViewState("PaginaRegresar"), String)
        End Get
        Set(value As String)
            ViewState("PaginaRegresar") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de archivos adjuntos que se mostrará al usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/03/2016</creationDate>
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
    ''' Maneja un set de archivos borrados para actualizar la base de datos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsArchivosBorrados As Data.DataSet
        Get
            Return CType(ViewState("DsArchivosBorrados"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsArchivosBorrados") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/03/2016</creationDate>
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
    ''' Extensiones para archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/03/2016</creationDate>
    Public Property ExtensionesPermitidas As String
        Get
            Return CType(ViewState("ExtensionesPermitidas"), String)
        End Get
        Set(value As String)
            ViewState("ExtensionesPermitidas") = value
        End Set
    End Property

    ''' <summary>
    ''' Tamaño maximo para el archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/03/2016</creationDate>
    Public Property TamanoArchivo As Integer
        Get
            Return CType(ViewState("TamanoArchivo"), Integer)
        End Get
        Set(value As Integer)
            ViewState("TamanoArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el anno de la orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/03/2016</creationDate>
    Private Property Anno As Integer
        Get
            If ViewState("Anno") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("Anno"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Anno") = value
        End Set
    End Property

    ''' <summary>
    ''' Es el identificador base de la Orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/03/2016</creationDate>
    Public Property IdOrdenTrabajo As String
        Get
            If ViewState("IdOrdenTrabajo") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("IdOrdenTrabajo"), String)
        End Get
        Set(value As String)
            ViewState("IdOrdenTrabajo") = value
        End Set
    End Property


    ''' <summary>
    ''' Guarda la ubicación de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/03/2016</creationDate>
    Public Property IdUbicacion As Integer
        Get
            If ViewState("IdUbicacion") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/03/2016</creationDate>
    Private Property IdSectorTaller As Integer
        Get
            If ViewState("IdSectorTaller") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdSectorTaller"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdSectorTaller") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el profesional encargado de la orden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/03/2016</creationDate>
    Public Property ProfesionalEncargado As String
        Get
            If ViewState("ProfesionalEncargado") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("ProfesionalEncargado"), String)
        End Get
        Set(value As String)
            ViewState("ProfesionalEncargado") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property OrdenTrabajo As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>17/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property ParametroInicialesArchivos As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Get
            Return CType(ViewState("ParametroInicialesArchivos"), Wsr_OT_Catalogos.EntOtpParametroUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtpParametroUbicacion)
            ViewState("ParametroInicialesArchivos") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de tipos de documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <creationDate>4/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsTipoArchivo As Data.DataSet
        Get
            Return CType(ViewState("DsTipoArchivo"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsTipoArchivo") = value
        End Set
    End Property

#End Region

#Region "Eventos"
    ''' <summary>
    ''' Inicializa el evento al cargar la pagina
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/03/2016</creationDate>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                inicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Guarda los archivos en etapa: Elaboración de Planos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/03/2016</creationDate>
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            If Guardar(False) Then
                MostrarAlertaGuardadoExitoso()
            Else
                MostrarAlertaError("No ha sido posible guardar la información de los planos")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Envia a finalizar la etapa de elaboración de planos, cambia el estado de la OT 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/03/2016</creationDate>
    Protected Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click
        Try
            If Guardar(True) Then
                MostrarAlertaGuardadoExitoso()
            Else
                MostrarAlertaError("No ha sido posible guardar la información de los planos")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Redirige al listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect(Me.PaginaRegresar)
    End Sub



    ''' <summary>
    ''' evento que se ejecuta para cargar el boton de borrar en la lista de archivos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpFuncionariosEjecucion_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpAdjunto.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        If Me.OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.ELABORACION_DE_PLANOS Then
            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                'Si No es elaboracion de planos, coloca el boton de borrar como invisible
                vlo_IbBorrar.Visible = False
            End If
        Else
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                    vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                    vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al agregar un archivo adjunto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>04/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarArchivo_Click(sender As Object, e As EventArgs) Handles btnAgregarArchivo.Click
        Try
            AgregarArchivo()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' opcion de descargar un archivo seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivo_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Dim vln_IdAdjunto As Integer = Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ADJUNTO_ORDEN_TRABAJO).ToString
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CType(ObtenerArchivo(vln_IdAdjunto).Archivo, Byte()))
            Response.End()
        Catch ex As System.Threading.ThreadAbortException
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Cambia los formatos permitidos dependiendo del tipo de archivo seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlTipoArchivo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoArchivo.SelectedIndexChanged
        Dim vlo_TipoDocumento() As DataRow
        Try

            If Me.ddlTipoArchivo.SelectedValue <> String.Empty Then
                vlo_TipoDocumento = DsTipoArchivo.Tables(0).Select(String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, Me.ddlTipoArchivo.SelectedValue))
                If DsTipoArchivo.Tables.Count > 0 AndAlso vlo_TipoDocumento IsNot Nothing Then
                    Me.ExtensionesPermitidas = vlo_TipoDocumento(0).Item(Modelo.OTM_TIPO_DOCUMENTO.FORMATOS_ADMITIDOS)
                    imgExtensiones.Attributes.Add("title", String.Format("Extensiones permitidas: {0}", ExtensionesPermitidas.ToLower))
                    Me.TamanoArchivo = vlo_TipoDocumento(0).Item(Modelo.OTM_TIPO_DOCUMENTO.TAMANIO_MAXIMO)
                End If
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaGuardadoExitoso()
        WebUtils.RegistrarScript(Me, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
    End Sub


    ''' <summary>
    ''' Inicializa variables, control de usuario y dataset de archivos que se usarán en esta pagina
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/03/2016</creationDate>
    Private Sub inicializarFormulario()
        Me.PaginaRegresar = "Lst_OT_GestionProfesionalesDisenio.aspx"
        LeerParametros()
        CargarParametroSiglas()
        inicializarControl()
        CargarOrdenTrabajo()
        cargarArchivos()
        inicializarControles()
        CargarListaTipoArchivo(String.Format("{0} = '{1}' AND {2} = {3}",
                Modelo.OTM_TIPO_DOCUMENTO.ESTADO,
                Estado.ACTIVO,
                Modelo.OTM_TIPO_DOCUMENTO.PROTEGIDO,
                Proteccion.NO_PROTEGIDO))

        If Me.Operacion = eOperacion.Consultar Then
            Me.PaginaRegresar = WebUtils.LeerParametro(Of String)("pvn_Regresar")
            WebUtils.RegistrarScript(Me, "deshabilitar", "deshabilitar();")
        End If

    End Sub

    ''' <summary>
    ''' Metodo encargado de obtener los parametros en sesion y asignarlos a variables especificas
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/03/2016</creationDate>
    Private Sub LeerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.IdSectorTaller = WebUtils.LeerParametro(Of Integer)("pvn_IdSectorTaller")
        Me.ProfesionalEncargado = WebUtils.LeerParametro(Of Integer)("pvn_IdEncargado")
        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario para la informacón general de la OT
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/03/2016</creationDate>
    Private Sub inicializarControl()
        'Asignación de Datos para generar el web user control de la información general
        ctrl_InfoGeneral.Anno = Me.Anno
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.IdUbicacion
        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()
    End Sub

    ''' <summary>
    ''' Carga los datos de los archivos pertenecientes a la etapa de planos,
    ''' Tambien inicializa el repeater y le asigna valores de existir
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/03/2016</creationDate>
    Private Sub cargarArchivos()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsAdjuntos As Data.DataSet
        Dim vlo_NuevaFila As DataRow

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsAdjuntos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarVOtAdjunto(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION,
                            Me.IdUbicacion, Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO,
                            Me.IdOrdenTrabajo, Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO,
                            EtapasOrdenTrabajo.ELABORACION_PLANOS),
                            String.Empty, False, 0, 0)

            Me.DsAdjuntosInsert = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("1=0"),
                String.Empty,
                False,
                0,
                0)

            Me.DsArchivosBorrados = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("1=0"),
                String.Empty,
                False,
                0,
                0)


            If vlo_DsAdjuntos.Tables.Count > 0 AndAlso vlo_DsAdjuntos.Tables(0).Rows.Count > 0 Then
                With Me.rpAdjunto
                    .DataSource = vlo_DsAdjuntos
                    .DataMember = vlo_DsAdjuntos.Tables(0).TableName
                    .DataBind()
                    .Visible = True
                End With
            Else
                With Me.rpAdjunto
                    .DataSource = Nothing
                    .DataBind()
                End With
            End If

            For Each vlo_fila As DataRow In vlo_DsAdjuntos.Tables(0).Rows

                vlo_NuevaFila = Me.DsAdjuntosInsert.Tables(0).NewRow()
                vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ADJUNTO_ORDEN_TRABAJO) = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ADJUNTO_ORDEN_TRABAJO)
                vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ETAPA_ORDEN_TRABAJO) = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ETAPA_ORDEN_TRABAJO)
                vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ORDEN_TRABAJO) = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ORDEN_TRABAJO)
                vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_TIPO_DOCUMENTO) = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_TIPO_DOCUMENTO)
                vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_UBICACION) = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_UBICACION)
                vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.DESC_TIPO_DOCUMENTO) = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.DESC_TIPO_DOCUMENTO)
                vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.NOMBRE_ARCHIVO) = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.NOMBRE_ARCHIVO)
                vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.DESCRIPCION) = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.DESCRIPCION)
                vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.USUARIO) = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.USUARIO)

                Me.DsAdjuntosInsert.Tables(0).Rows.Add(vlo_NuevaFila)
            Next


        Catch ex As Exception
            Throw
        Finally
            If vlo_DsAdjuntos IsNot Nothing Then
                vlo_DsAdjuntos.Dispose()
            End If
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenTrabajo()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Inicializa los botones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/03/2016</creationDate>
    Private Sub inicializarControles()
        Dim vlo_Encargado As WsrEU_Curriculo.EntEmpleados

        If OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.ELABORACION_DE_PLANOS Then
            Me.btnGuardar.Visible = True
            Me.btnFinalizar.Visible = True
            Me.btnAgregarArchivo.Visible = True
        End If

        'Carga el profesional encargado y coloca el nombre completo en el encabezado correspondiente
        vlo_Encargado = CargarFuncionario(Me.ProfesionalEncargado)
        Me.lblEncargado.Text = String.Format("{0} {1} {2}", vlo_Encargado.NOMBRE, vlo_Encargado.APELLIDO1, vlo_Encargado.APELLIDO2)


    End Sub

    ''' <summary>
    ''' Agrega un nuevo adjunto al dataset 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregarArchivo()
        Dim vlo_DrFila As Data.DataRow
        Dim vlc_Nombre As String
        Dim vlb_Bandera As Boolean = False
        Dim vlc_Llave As String()

        Try
            vlc_Nombre = Me.ifInfo.FileName

            vlc_Llave = Me.ParametroInicialesArchivos.Valor.Split(";")

            For Each vlc_Valor As String In vlc_Llave
                If vlc_Nombre.StartsWith(vlc_Valor) Then
                    vlb_Bandera = True
                End If
            Next

            If vlb_Bandera Then

                vlo_DrFila = Me.DsAdjuntosInsert.Tables(0).NewRow
                vlo_DrFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.NOMBRE_ARCHIVO) = Me.ifInfo.FileName
                vlo_DrFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESCRIPCION) = Me.txtDescripcionArchivo.Text
                vlo_DrFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ARCHIVO) = Me.ifInfo.FileBytes
                vlo_DrFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.USUARIO) = Me.Usuario.UserName
                vlo_DrFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_TIPO_DOCUMENTO) = Me.ddlTipoArchivo.SelectedValue
                vlo_DrFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESC_TIPO_DOCUMENTO) = Me.ddlTipoArchivo.SelectedItem
                vlo_DrFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO) = Me.IdOrdenTrabajo
                vlo_DrFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_UBICACION) = Me.IdUbicacion
                vlo_DrFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ETAPA_ORDEN_TRABAJO) = EtapasOrdenTrabajo.ELABORACION_PLANOS
                Me.DsAdjuntosInsert.Tables(0).Rows.Add(vlo_DrFila)

                If Me.DsAdjuntosInsert IsNot Nothing AndAlso Me.DsAdjuntosInsert.Tables(0).Rows.Count > 0 Then
                    Me.rpAdjunto.DataSource = Me.DsAdjuntosInsert
                    Me.rpAdjunto.DataMember = Me.DsAdjuntosInsert.Tables(0).TableName
                    Me.rpAdjunto.DataBind()
                Else
                    With Me.rpAdjunto
                        .DataSource = Nothing
                        .DataBind()
                    End With
                End If

                Me.ddlTipoArchivo.SelectedValue = String.Empty
                Me.txtDescripcionArchivo.Text = String.Empty

            Else
                MostrarAlertaError("El archivo debe estar nombrado con las siglas correspondientes")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar, ubicada en la 
    ''' primera columna de cada registro del listado de adjuntos, llama a la función borrar y muestra  un mensaje según 
    ''' la operacion efectuada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrarAdjunto_Click(sender As Object, e As ImageClickEventArgs)
        Dim vln_Indice As Integer
        Dim vlo_filanueva As DataRow
        Dim vlc_IdDocumento As String
        Try

            vlc_IdDocumento = CType(sender, ImageButton).CommandArgument
            If Not String.IsNullOrWhiteSpace(vlc_IdDocumento) Then
                vlo_filanueva = DsArchivosBorrados.Tables(0).NewRow()
                vlo_filanueva.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ADJUNTO_ORDEN_TRABAJO) = Convert.ToInt32(vlc_IdDocumento)
                DsArchivosBorrados.Tables(0).Rows.Add(vlo_filanueva)
            End If


            vln_Indice = Convert.ToInt32(CType(sender, ImageButton).CommandName)
            Me.DsAdjuntosInsert.Tables(0).Rows(vln_Indice).Delete()

            If Me.DsAdjuntosInsert IsNot Nothing AndAlso Me.DsAdjuntosInsert.Tables(0).Rows.Count > 0 Then
                Me.rpAdjunto.DataSource = Me.DsAdjuntosInsert
                Me.rpAdjunto.DataMember = Me.DsAdjuntosInsert.Tables(0).TableName
                Me.rpAdjunto.DataBind()
            Else
                With Me.rpAdjunto
                    .DataSource = Nothing
                    .DataBind()
                End With
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Carga la lista de tipos de documento con la condicion especificada.
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaTipoArchivo(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlTipoArchivo.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, String.Empty, False, 0, 0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                'drop down list de tipos de documento
                With Me.ddlTipoArchivo
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_TIPO_DOCUMENTO.DESCRIPCION
                    .DataValueField = Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO
                    .DataBind()
                End With
            End If

            Me.DsTipoArchivo = vlo_DsDatos
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>17/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarParametroSiglas()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ParametroInicialesArchivos = vlo_Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.INICIALES_PERMITIDAS_ARCHIVOS_ELABORACION_PLANOS))


            imgSiglasPermitidas.Attributes.Add("title", String.Format("El nombre del archivo agregado debe iniciar con alguna de las siguientes letras: {0}", Me.ParametroInicialesArchivos.Valor.Replace(";", ",")))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal o el numero de empleado que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionario(pvn_IdPersonal As String) As WsrEU_Curriculo.EntEmpleados
        Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("ID_PERSONAL = '{0}' OR NUM_EMPLEADO = {0}", pvn_IdPersonal))
        Catch ex As Exception
            Throw
        Finally
            If vlo_WsEU_Curriculo IsNot Nothing Then
                vlo_WsEU_Curriculo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el archivo por el Id especificado
    ''' </summary>
    ''' <param name="pvn_idAdjunto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerArchivo(pvn_idAdjunto As Integer) As EntOttAdjuntoOrdenTrabajo
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO, pvn_idAdjunto))


        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Funcion encargada de llamar al metodo que bien almacena o finaliza la elaboración de planos
    ''' </summary>
    ''' <param name="pvb_Finalizar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Guardar(pvb_Finalizar As Boolean) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_GuardarFinalizarPlanos(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.OrdenTrabajo, Me.DsAdjuntosInsert, Me.DsArchivosBorrados, pvb_Finalizar) > 0

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
