Imports Wsr_OT_OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo
Imports System.Data



Partial Class OrdenesDeTrabajo_Frm_OT_GestionContratacionProfesional
    Inherits System.Web.UI.Page

#Region "Propiedades"
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
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>22/04/2016</creationDate>
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
    ''' Guarda la ubicación de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>22/04/2016</creationDate>
    Public Property Anno As Integer
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
    ''' Almacena el archivo del acta que se levantó en la visita técnica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Acta As EntOttAdjuntoOrdenTrabajo
        Get
            Return CType(ViewState("Acta"), EntOttAdjuntoOrdenTrabajo)
        End Get
        Set(value As EntOttAdjuntoOrdenTrabajo)
            ViewState("Acta") = value
        End Set
    End Property

    ''' <summary>
    ''' Extensiones para archivo
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
            Return CType(ViewState("TamanoArchivoCartel"), Integer)
        End Get
        Set(value As Integer)
            ViewState("TamanoArchivoCartel") = value
        End Set
    End Property

    ''' <summary>
    ''' entidad para la contratación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property Contratacion As EntOttContratacion
        Get
            Return CType(ViewState("Contratacion"), EntOttContratacion)
        End Get
        Set(value As EntOttContratacion)
            ViewState("Contratacion") = value
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

#End Region

#Region "Eventos"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>22/04/2016</creationDate>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                inicializarFormulario()
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
    ''' descarga el archivo adjunto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivoActa_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.Acta.NombreArchivo)
            Response.BinaryWrite(Me.Acta.Archivo)
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarActa_Click(sender As Object, e As EventArgs)
        Me.Acta.NombreArchivo = Me.fuActaVisita.FileName
        Me.Acta.Archivo = Me.fuActaVisita.FileBytes
        fuActaVisita.Visible = False
        Me.lnkArchivoActa.Text = Me.fuActaVisita.FileName
        btnAgregarActa.Visible = False
        lnkArchivoActa.Visible = True
        btnEliminarActa.Visible = True
    End Sub

    ''' <summary>
    ''' elimina el archivo cartel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnEliminarCartela_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminarActa.Click
        Try
            Me.Acta.Archivo = Nothing
            Me.Acta.NombreArchivo = String.Empty
            Me.fuActaVisita.Visible = True
            Me.lnkArchivoActa.Visible = False
            Me.btnEliminarActa.Visible = False
            btnAgregarActa.Visible = True
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
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs)
        Try
            If Aceptar() Then
                WebUtils.RegistrarScript(Me, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
            Else
                MostrarAlertaError("No ha sido posible cerrar la etapa")
            End If
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
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>22/04/2016</creationDate>
    Private Sub inicializarFormulario()
        Me.Usuario = New UsuarioActual()
        btnEliminarActa.Attributes.Add("data-uniqueid", btnEliminarActa.UniqueID)
        leerParametros()
        CargarOrdenTrabajo(IdUbicacion, IdOrdenTrabajo)
        cargarContratacion()
        CargarTiposDocumento()
        InicializarArchivo()
        InicializarControlUsuario()
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Se encarga de leer los parámetros provenientes del listado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub leerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario para mostrar la información general
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>26/04/2016</creationDate>
    ''' </changeLog>
    Private Sub InicializarControlUsuario()
        'Asignación de Datos para generar el web user control de la información general
        ctrl_InfoGeneral.Anno = Me.Anno
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.IdUbicacion
        ctrl_InfoGeneral.NumContrato = Contratacion.NumeroContrato
        ctrl_InfoGeneral.NombreContrato = Contratacion.NombreContrato

        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()
        Me.liRecomendacion.Visible = False
        Me.liAclaraciones.Visible = False

        If Me.OrdenTrabajo.EstadoOrdenTrabajo <> EstadoOrden.CONTRATACION_VISITA_TECNICA Then

            Me.liAclaraciones.Visible = True

            ctrl_Aclaraciones.IdOrdenTrabajo = IdOrdenTrabajo
            ctrl_Aclaraciones.IdUbicacion = IdUbicacion
            ctrl_Aclaraciones.EtapaActual = Me.EtapaActual
            ctrl_Aclaraciones.Version = Me.Contratacion.Version
            ctrl_Aclaraciones.Editable = Contratacion.Editable
            ctrl_Aclaraciones.Cargo = Cargo.PROFESIONAL
            Me.ctrl_Aclaraciones.Inicializar()

        End If

        If Me.OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_OFERTAS Then
            Me.liAclaraciones.Visible = True
            Me.liRecomendacion.Visible = True
            ctrl_OfertasProfesional.IdOrdenTrabajo = IdOrdenTrabajo
            ctrl_OfertasProfesional.IdUbicacion = IdUbicacion
            ctrl_OfertasProfesional.EtapaActual = Me.EtapaActual
            ctrl_OfertasProfesional.Version = Me.Contratacion.Version
            ctrl_OfertasProfesional.Editable = Contratacion.Editable
            ctrl_OfertasProfesional.Cargo = Cargo.PROFESIONAL
            ctrl_OfertasProfesional.Inicializar()

        End If


    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarArchivo()

        If OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.CONTRATACION_VISITA_TECNICA Then
            fuActaVisita.Visible = True
            btnAgregarActa.Visible = True
            btnAceptar.Visible = True
            Acta = New EntOttAdjuntoOrdenTrabajo
            Acta.IdTipoDocumento = TipoDocumento.OFICIO
            Acta.Descripcion = "Acta de Visita Técnica"
            Acta.IdOrdenTrabajo = IdOrdenTrabajo
            Acta.IdUbicacion = IdUbicacion
            Acta.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.CONTRATACIONES
            Acta.Usuario = Me.Usuario.UserName

        Else
            fuActaVisita.Visible = False
            btnAgregarActa.Visible = False
            btnEliminarActa.Visible = False
            btnAceptar.Visible = False
            cargarDocumento()
        End If

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
                String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.OFICIO))

            If vlo_EntOtmTipoDocumento.Existe Then
                Me.ExtensionesPermitidas = vlo_EntOtmTipoDocumento.FormatosAdmitidos
                imgExtensiones.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidas.ToLower))
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
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">id de la ubicacion</param>
    ''' <param name="pvn_IdOrdenTrabajo">id de la orden</param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenTrabajo(pvn_IdUbicacion As Integer, pvn_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION,
                              pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvn_IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub cargarDocumento()

        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDocumentoContratacion As EntOttDocumentoContratacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttDocumentoContratacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}' AND {6} = {7}",
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ETAPA_CONTRATACION,
                              EtapaContratacion.VISITA_TECNICA,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION,
                              IdUbicacion, Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO,
                              IdOrdenTrabajo, Modelo.OTT_DOCUMENTO_CONTRATACION.VERSION, Contratacion.Version))


            If vlo_EntOttDocumentoContratacion.Existe Then
                Me.Acta = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}",
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO,
                              vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo))
                If Acta.Existe Then
                    lnkArchivoActa.Text = Acta.NombreArchivo
                    lnkArchivoActa.Visible = True
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
    ''' Carga la ultima version de la contratación
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub cargarContratacion()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet
        Dim vlo_UltimaVersion As DataRow

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_CONTRATACION_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'",
                    Modelo.V_OTT_CONTRATACIONLST.ID_UBICACION,
                    Me.IdUbicacion,
                    Modelo.V_OTT_CONTRATACIONLST.ID_ORDEN_TRABAJO,
                    Me.IdOrdenTrabajo),
                String.Format("{0} {1}", Modelo.V_OTT_CONTRATACIONLST.VERSION, Ordenamiento.DESCENDENTE), False, 0, 0)

            Me.Contratacion = New EntOttContratacion

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then

                'Se carga la ultima contratacion 
                vlo_UltimaVersion = vlo_DsDatos.Tables(0).Rows(0)
                'Se agrega un identificador de la version en la lista para que seleccione cual desea consultar

                If vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.EDITABLE) = Version.EDITABLE Then
                    Contratacion.Editable = Version.EDITABLE
                    'Mostrar Datos con la fila editable
                Else
                    Contratacion.Editable = Version.NO_EDITABLE
                End If

                'Se cargan los datos del anteproyecto en el objeto en memoria

                Contratacion.IdViaContrato = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.ID_VIA_CONTRATO)
                Contratacion.IdOrdenTrabajo = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.ID_ORDEN_TRABAJO)
                Contratacion.IdUbicacion = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.ID_UBICACION)
                Contratacion.NombreContrato = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.NOMBRE_CONTRATO)
                Contratacion.NumeroContrato = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.NUMERO_CONTRATO)
                Contratacion.NumeroDecisionInicial = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.NUMERO_DECISION_INICIAL)
                Contratacion.NumeroSolicitud = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.NUMERO_SOLICITUD)
                Contratacion.Version = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.VERSION)
                Contratacion.TimeStamp = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.TIME_STAMP)
                Contratacion.Existe = True

            End If

            AsignarEtapaActual()

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Asigna la etapa actual basandose en el estado de la orden de trabajo y controla la visualización de los botones
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>22/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AsignarEtapaActual()
        Select Case Me.OrdenTrabajo.EstadoOrdenTrabajo

            Case EstadoOrden.CONTRATACION_VISITA_TECNICA
                Me.EtapaActual = EtapaContratacion.VISITA_TECNICA
                activarVisitaTecnica()
            Case EstadoOrden.CONTRATACION_ACLARACIONES
                Me.EtapaActual = EtapaContratacion.ACLARACIONES
                activarAclaraciones()
            Case EstadoOrden.CONTRATACION_OFERTAS
                Me.EtapaActual = EtapaContratacion.OFERTAS
                activarRecomendacion()

        End Select
    End Sub

    Private Sub activarVisitaTecnica()
        WebUtils.RegistrarScript(Me, "activarVisitaTecnica", "activarVisitaTecnica();")
    End Sub

    Private Sub activarAclaraciones()
        WebUtils.RegistrarScript(Me, "activarAclaraciones", "activarAclaraciones();")
    End Sub

    Private Sub activarRecomendacion()
        WebUtils.RegistrarScript(Me, "activarRecomendacion", "activarRecomendacion();")
    End Sub

#End Region

#Region "Funciones"

    Private Function Aceptar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.Acta.Archivo IsNot Nothing Then

                Me.Contratacion.Usuario = Usuario.UserName

                Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_CONTRATACION_AceptarVisitaTecnica(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    Me.Acta, Me.Contratacion, Me.OrdenTrabajo, Me.Usuario.NumEmpleado, Me.EtapaActual) > 0
            Else
                MostrarAlertaError("El archivo está vacio, por favor ingrese un archivo.")
            End If
            Return -1
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
