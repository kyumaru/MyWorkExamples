Imports System.Data
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class Controles_wuc_OT_Aclaraciones
    Inherits System.Web.UI.UserControl

#Region "Propiedades"

    ''' <summary>
    ''' Almacena la lista de archivos a agregar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsArchivos As DataSet
        Get
            Return CType(ViewState("DsArchivos"), DataSet)
        End Get
        Set(value As DataSet)
            ViewState("DsArchivos") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
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
    ''' Guarda la información del funcionario actualmente realizando la operación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property Funcionario As WsrEU_Curriculo.EntEmpleados
        Get
            Return CType(ViewState("Funcionario"), WsrEU_Curriculo.EntEmpleados)
        End Get
        Set(value As WsrEU_Curriculo.EntEmpleados)
            ViewState("Funcionario") = value
        End Set
    End Property


    ''' <summary>
    ''' Es el identificador base de la Orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>22/04/2016</creationDate>
    Public Property IdOrdenTrabajo As String
        Get
            If ViewState("IdOrdenTrabajo") Is Nothing Then
                Return 0
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
    ''' <creationDate>22/04/2016</creationDate>
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
    ''' Guarda etapa actual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>22/04/2016</creationDate>
    Public Property EtapaActual As Integer
        Get
            If ViewState("EtapaActual") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("EtapaActual"), Integer)
        End Get
        Set(value As Integer)
            ViewState("EtapaActual") = value
        End Set
    End Property

    ''' <summary>
    ''' Extensiones para archivo de inicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ExtensionesPermitidas As String
        Get
            Return CType(ViewState("ExtensionesPermitidas"), String)
        End Get
        Set(value As String)
            ViewState("ExtensionesPermitidas") = value
        End Set
    End Property

    ''' <summary>
    ''' Cargo del tipo de origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>27/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property Cargo As String
        Get
            Return CType(ViewState("Cargo"), String)
        End Get
        Set(value As String)
            ViewState("Cargo") = value
        End Set
    End Property
    ''' <summary>
    ''' Tamaño maximo para el archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property TamanoArchivo As Integer
        Get
            Return CType(ViewState("TamanoArchivo"), Integer)
        End Get
        Set(value As Integer)
            ViewState("TamanoArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' Numero de la ultima version de la contratacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property Version As Integer
        Get
            Return CType(ViewState("Version"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Version") = value
        End Set
    End Property

    ''' <summary>
    ''' Define cuando la version actual es editable o no
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property Editable As Integer
        Get
            Return CType(ViewState("Editable"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Editable") = value
        End Set
    End Property


#End Region

#Region "Eventos"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                InicializarFormulario()
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
    ''' evento que se ejecuta se carga la lista de actividades, por cada registro del
    ''' repeater se asigna un identificador unico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>25/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpAdjuntos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpAdjuntos.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        If Editable = Utilerias.OrdenesDeTrabajo.Version.EDITABLE Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                    vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                    vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
                End If
            End If
        Else
            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                vlo_IbBorrar.Visible = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' opcion de descargar un archivo seleccionado de la lista
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivo_Command(sender As Object, e As CommandEventArgs)
        Try
            Dim vln_IdAdjunto As Integer = Me.DsArchivos.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ADJUNTO_ORDEN_TRABAJO).ToString

            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsArchivos.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.V_OTT_DOCUMENTO_CONTRATLST.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CType(ObtenerArchivo(vln_IdAdjunto).Archivo, Byte()))
            Response.End()

        Catch ex As System.Threading.ThreadAbortException
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar, ubicada en la 
    ''' primera columna de cada registro del listado de adjuntos y borra el elemento del set de datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrarAdjuntoAclaraciones_Click(sender As Object, e As ImageClickEventArgs)
        Dim vln_Indice As Integer
        Dim vln_IdAdjunto As Integer

        Try

            vln_Indice = Convert.ToInt32(CType(sender, ImageButton).CommandName)
            vln_IdAdjunto = Me.DsArchivos.Tables(0).Rows(vln_Indice)(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ADJUNTO_ORDEN_TRABAJO).ToString

            If Borrar(vln_IdAdjunto) Then
                Me.DsArchivos.Tables(0).Rows(vln_Indice).Delete()
                If Me.DsArchivos.Tables(0).Rows.Count = 1 Then
                    Me.DsArchivos = New DataSet
                End If
                inicializarSetDatos()
                If DsArchivos.Tables.Count <= 0 Then
                    articleTitulo.Visible = False
                End If
            End If
            WebUtils.RegistrarScript(Me, "activarAclaraciones", "activarAclaraciones();")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al agregar un archivo adjunto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarArchivo_Click(sender As Object, e As EventArgs) Handles btnAgregarDocumento.Click
        Try
            AgregarArchivo()
            articleTitulo.Visible = True
            Me.txtResumen.Text = String.Empty
            WebUtils.RegistrarScript(Me, "activarAclaraciones", "activarAclaraciones();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>25/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnCerrarEtapa_Click(sender As Object, e As EventArgs)
        Try
            If Editable = Utilerias.OrdenesDeTrabajo.Version.EDITABLE AndAlso ValidaTramitados() Then
                If CerrarAclaraciones() Then
                    WebUtils.RegistrarScript(Me.Page, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
                End If
            End If
            WebUtils.RegistrarScript(Me, "activarAclaraciones", "activarAclaraciones();")
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub chkTramitado_CheckedChanged(sender As Object, e As EventArgs)
        Dim vlo_IbTramitado As CheckBox
        Dim vlc_IdAdjunto As String

        Try
            vlo_IbTramitado = CType(sender, CheckBox)
            vlc_IdAdjunto = vlo_IbTramitado.Attributes("data-idAdjunto")

            If Modificar(vlc_IdAdjunto, vlo_IbTramitado.Checked) Then

            Else

            End If
            WebUtils.RegistrarScript(Me, "activarAclaraciones", "activarAclaraciones();")

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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Sub Inicializar()
        InicializarFormulario()
    End Sub


    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Inicializa el formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Usuario = New UsuarioActual()
        Me.Funcionario = CargarFuncionario(Usuario.UserName)
        inicializarSetDatos()
        CargarTiposDocumento()
        If Me.EtapaActual = EtapaContratacion.ACLARACIONES AndAlso Editable = Utilerias.OrdenesDeTrabajo.Version.EDITABLE Then
            If Me.Cargo = Utilerias.OrdenesDeTrabajo.Cargo.ENCARGADO Then
                btnCerrarEtapa.Visible = True
            Else
                btnCerrarEtapa.Visible = False
            End If
            btnAgregarDocumento.Enabled = True
        Else
            btnAgregarDocumento.Enabled = False
            btnCerrarEtapa.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Inicializa los datos de las columnas de el listado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarSetDatos()

        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            'Carga los adjuntos de la tabla intermedia
            Me.DsArchivos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}' AND {2} = {3} AND {4} = {5} AND {6} = {7}",
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO,
                              IdOrdenTrabajo, Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION,
                              IdUbicacion, Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ETAPA_CONTRATACION,
                              EtapaContratacion.ACLARACIONES, Modelo.OTT_DOCUMENTO_CONTRATACION.VERSION,
                              Me.Version), String.Format("{0} DESC", Modelo.V_OTT_DOCUMENTO_CONTRATLST.FECHA_HORA_REGISTRO), False, 0, 0)


            If Me.DsArchivos IsNot Nothing AndAlso Me.DsArchivos.Tables(0).Rows.Count > 0 Then
                Me.rpAdjuntos.DataSource = Me.DsArchivos
                Me.rpAdjuntos.DataMember = Me.DsArchivos.Tables(0).TableName
                Me.rpAdjuntos.DataBind()
            Else
                With Me.rpAdjuntos
                    .DataSource = Nothing
                    .DataBind()
                End With
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
    ''' Carga las variables para validar el archivo 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    Private Sub CargarTiposDocumento()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As Wsr_OT_Catalogos.EntOtmTipoDocumento

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.GENERICO))

            If vlo_EntOtmTipoDocumento.Existe Then
                Me.ExtensionesPermitidas = vlo_EntOtmTipoDocumento.FormatosAdmitidos
                imgExtensionesAclaraciones.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidas.ToLower))
                Me.TamanoArchivo = vlo_EntOtmTipoDocumento.TamanioMaximo
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    Private Sub AgregarArchivo()
        Dim vln_Resultado As Integer
        Dim vlo_NuevoArchivo As Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoOrdenTrabajo
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDocumentoContratacion As Wsr_OT_OrdenesDeTrabajo.EntOttDocumentoContratacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_NuevoArchivo = New Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoOrdenTrabajo

            vlo_NuevoArchivo.NombreArchivo = Me.fuDocumento.FileName
            vlo_NuevoArchivo.Archivo = Me.fuDocumento.FileBytes
            vlo_NuevoArchivo.IdUbicacion = IdUbicacion
            vlo_NuevoArchivo.IdOrdenTrabajo = IdOrdenTrabajo
            vlo_NuevoArchivo.Descripcion = Me.txtResumen.Text
            vlo_NuevoArchivo.Usuario = Funcionario.ID_PERSONAL
            vlo_NuevoArchivo.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.CONTRATACIONES
            vlo_NuevoArchivo.IdTipoDocumento = TipoDocumento.GENERICO

            vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_NuevoArchivo)

            If vln_Resultado > 0 Then
                vlo_EntOttDocumentoContratacion = New Wsr_OT_OrdenesDeTrabajo.EntOttDocumentoContratacion

                vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo = vln_Resultado
                vlo_EntOttDocumentoContratacion.IdEtapaContratacion = EtapaContratacion.ACLARACIONES
                vlo_EntOttDocumentoContratacion.IdEtapaOrdenTrabajo = EtapasOrdenTrabajo.CONTRATACIONES
                vlo_EntOttDocumentoContratacion.IdOrdenTrabajo = IdOrdenTrabajo
                vlo_EntOttDocumentoContratacion.IdTipoDocumento = TipoDocumento.GENERICO
                vlo_EntOttDocumentoContratacion.IdUbicacion = IdUbicacion
                vlo_EntOttDocumentoContratacion.DocumentoTramitado = 0
                vlo_EntOttDocumentoContratacion.Origen = Me.Cargo
                vlo_EntOttDocumentoContratacion.Usuario = Funcionario.ID_PERSONAL
                vlo_EntOttDocumentoContratacion.Version = Version
                vlo_EntOttDocumentoContratacion.FechaHoraRegistro = Now

                vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_InsertarRegistro(
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                    vlo_EntOttDocumentoContratacion)

            End If

            If vln_Resultado > 0 Then

                inicializarSetDatos()

            Else
                MostrarAlertaError("No ha sido posible ingresar la información")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Modifica el estado del documento a tramitado
    ''' </summary>
    ''' <param name="pvc_IdAdjunto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Modificar(pvc_IdAdjunto As String, pvb_Tramitado As Boolean) As Boolean
        Dim vln_Resultado As Integer
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDocumentoContratacion As Wsr_OT_OrdenesDeTrabajo.EntOttDocumentoContratacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntOttDocumentoContratacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_ObtenerRegistro(
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                    String.Format("{0} = {1}", Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ADJUNTO_ORDEN_TRABAJO, pvc_IdAdjunto))

            If vlo_EntOttDocumentoContratacion.Existe Then
                If pvb_Tramitado Then
                    vlo_EntOttDocumentoContratacion.DocumentoTramitado = Documento.TRAMITADO
                Else
                    vlo_EntOttDocumentoContratacion.DocumentoTramitado = Documento.NO_TRAMITADO
                End If

                vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_ModificarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOttDocumentoContratacion)
            End If

            inicializarSetDatos()

            Return vln_Resultado > 0
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal o el numero de empleado que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>25/04/2016</creationDate>
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
    ''' <creationDate>25/04/2016</creationDate>
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
    ''' Elimina el elemento de la lista de adjuntos y de la tabla intermedia
    ''' </summary>
    ''' <param name="pvn_IdAdjunto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>25/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Borrar(pvn_IdAdjunto As Integer) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vln_resultado As Integer
        Dim vlo_EntOttDocumentoContratacion As EntOttDocumentoContratacion
        Dim vlo_EntOttAdjuntoOrdenTrabajo As EntOttAdjuntoOrdenTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttDocumentoContratacion = New EntOttDocumentoContratacion
            vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo = pvn_IdAdjunto
            vlo_EntOttDocumentoContratacion.Version = Version
            vlo_EntOttDocumentoContratacion.IdUbicacion = IdUbicacion
            vlo_EntOttDocumentoContratacion.IdOrdenTrabajo = IdOrdenTrabajo
            vlo_EntOttDocumentoContratacion.IdTipoDocumento = TipoDocumento.GENERICO
            vlo_EntOttDocumentoContratacion.IdEtapaOrdenTrabajo = EtapasOrdenTrabajo.CONTRATACIONES

            vln_resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_BorrarRegistro(
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                    vlo_EntOttDocumentoContratacion)

            If vln_resultado > 0 Then

                vlo_EntOttAdjuntoOrdenTrabajo = New EntOttAdjuntoOrdenTrabajo

                vlo_EntOttAdjuntoOrdenTrabajo.IdAdjuntoOrdenTrabajo = pvn_IdAdjunto

                vln_resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttAdjuntoOrdenTrabajo)

            End If

            Return vln_resultado > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' Cierra la etapa de aclaraciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>25/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CerrarAclaraciones() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.DsArchivos.Tables.Count > 0 AndAlso Me.DsArchivos.Tables(0).Rows.Count > 0 Then
                Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_CONTRATACION_CerrarEtapaAclaraciones(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    Me.IdOrdenTrabajo, IdUbicacion, Me.Usuario.NumEmpleado, Me.EtapaActual, Me.Version) > 0
            Else
                MostrarAlertaError("Debe adjuntar al menos un documento para las aclaraciones.")
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Validar los documentos del profesional, en teoria todos deben estar tramitados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>25/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ValidaTramitados() As Boolean
        Dim vlo_resultado As Boolean
        Dim vlo_filas() As Data.DataRow
        Dim vlo_PrimerItem As Data.DataRow
        Try
            vlo_resultado = False
            If DsArchivos.Tables.Count > 0 Then
                vlo_filas = DsArchivos.Tables(0).Select(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_DOCUMENTO_CONTRATACION.DOCUMENTO_TRAMITADO, Documento.NO_TRAMITADO,
                                                                          Modelo.OTT_DOCUMENTO_CONTRATACION.ORIGEN, Utilerias.OrdenesDeTrabajo.Cargo.PROFESIONAL))
                If vlo_filas.Length <= 0 Then
                    vlo_PrimerItem = DsArchivos.Tables(0).Rows(0)
                    If vlo_PrimerItem(Modelo.OTT_DOCUMENTO_CONTRATACION.ORIGEN).ToString() = Utilerias.OrdenesDeTrabajo.Cargo.PROFESIONAL Then
                        vlo_resultado = True
                    Else
                        MostrarAlertaError("No se puede cerrar esta etapa hasta que el profesional responda a la solicitud creada.")
                    End If
                Else
                    MostrarAlertaError("No se puede cerrar esta etapa hasta que todas las respuestas del profesional esten tramitadas.")
                End If
            End If

            Return vlo_resultado
        Catch ex As Exception
            Throw
        End Try
    End Function


#End Region

End Class
