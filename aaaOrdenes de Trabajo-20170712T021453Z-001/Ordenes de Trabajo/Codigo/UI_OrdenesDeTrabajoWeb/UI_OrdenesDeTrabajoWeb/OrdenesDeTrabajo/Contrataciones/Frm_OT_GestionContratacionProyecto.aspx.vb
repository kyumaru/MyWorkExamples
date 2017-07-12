Imports Utilerias.OrdenesDeTrabajo
Imports System.Data
Imports Wsr_OT_OrdenesDeTrabajo
Imports Wsr_OT_Catalogos


''' <summary>
''' Clase que controla el flujo de la página para la gestion de contrataciones
''' </summary>
''' <remarks></remarks>
''' <author>César Bermudez Garcia</author>
''' <creationDate>15/04/2016</creationDate>
''' <changeLog></changeLog>
Partial Class OrdenesDeTrabajo_Frm_OT_GestionContratacionProyecto
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Contador para tabs
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>18/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property ContadorTaps As Integer
        Get
            Return CType(ViewState("ContadorTaps"), Integer)
        End Get
        Set(value As Integer)
            ViewState("ContadorTaps") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la última version del anteproyecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/04/2016</creationDate>
    Private Property VersionActual As Integer
        Get
            If ViewState("VersionActual") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("VersionActual"), Integer)
        End Get
        Set(value As Integer)
            ViewState("VersionActual") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/04/2016</creationDate>
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
    ''' <creationDate>15/04/2016</creationDate>
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
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/04/2016</creationDate>
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
    ''' Es el identificador base de la Orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/04/2016</creationDate>
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
    ''' <creationDate>15/04/2016</creationDate>
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
    ''' <creationDate>15/04/2016</creationDate>
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
    ''' Guarda etapa actual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>19/04/2016</creationDate>
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
    ''' Es el identificador base de la Orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>19/04/2016</creationDate>
    Public Property EtapaActualDescripcion As String
        Get
            If ViewState("EtapaActualDescripcion") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("EtapaActualDescripcion"), String)
        End Get
        Set(value As String)
            ViewState("EtapaActualDescripcion") = value
        End Set
    End Property


    ''' <summary>
    ''' entidad para la contratación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/04/2016</creationDate>
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
    ''' Tamaño maximo para el archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property TamanoArchivoInicio As Integer
        Get
            Return CType(ViewState("TamanoArchivoInicio"), Integer)
        End Get
        Set(value As Integer)
            ViewState("TamanoArchivoInicio") = value
        End Set
    End Property

    ''' <summary>
    ''' Extensiones para archivo de inicio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ExtensionesPermitidasInicio As String
        Get
            Return CType(ViewState("ExtensionesPermitidasInicio"), String)
        End Get
        Set(value As String)
            ViewState("ExtensionesPermitidasInicio") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la lista de archivos a agregar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsArchivosInicio As DataSet
        Get
            Return CType(ViewState("DsArchivosInicio"), DataSet)
        End Get
        Set(value As DataSet)
            ViewState("DsArchivosInicio") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la lista de lineas de contratacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsLineas As DataSet
        Get
            Return CType(ViewState("DsLineas"), DataSet)
        End Get
        Set(value As DataSet)
            ViewState("DsLineas") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la lista de archivos de la etapa adjudicación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>6/5/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsArchivosAdjudicacion As DataSet
        Get
            Return CType(ViewState("DsArchivosAdjudicacion"), DataSet)
        End Get
        Set(value As DataSet)
            ViewState("DsArchivosAdjudicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Extensiones para archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ExtensionesPermitidasCartel As String
        Get
            Return CType(ViewState("ExtensionesPermitidasCartel"), String)
        End Get
        Set(value As String)
            ViewState("ExtensionesPermitidasCartel") = value
        End Set
    End Property

    ''' <summary>
    ''' Tamaño maximo para el archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property TamanoArchivoCartel As Integer
        Get
            Return CType(ViewState("TamanoArchivoCartel"), Integer)
        End Get
        Set(value As Integer)
            ViewState("TamanoArchivoCartel") = value
        End Set
    End Property

    ''' <summary>
    ''' Extensiones para archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ExtensionesPermitidasOficio As String
        Get
            Return CType(ViewState("ExtensionesPermitidasRecomendacion"), String)
        End Get
        Set(value As String)
            ViewState("ExtensionesPermitidasRecomendacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Tamaño maximo para el archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property TamanoArchivoOficio As Integer
        Get
            Return CType(ViewState("TamanoArchivoRecomendacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("TamanoArchivoRecomendacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el archivo de clausula penal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property ArchivoCartel As EntOttAdjuntoOrdenTrabajo
        Get
            Return CType(ViewState("ArchivoCartel"), EntOttAdjuntoOrdenTrabajo)
        End Get
        Set(value As EntOttAdjuntoOrdenTrabajo)
            ViewState("ArchivoCartel") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el archivo de recomendacion tecnica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property ArchivoRecomendacion As EntOttAdjuntoOrdenTrabajo
        Get
            Return CType(ViewState("ArchivoRecomendacion"), EntOttAdjuntoOrdenTrabajo)
        End Get
        Set(value As EntOttAdjuntoOrdenTrabajo)
            ViewState("ArchivoRecomendacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el archivo de la linea de adjudicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property ArchivoLinea As EntOttAdjuntoOrdenTrabajo
        Get
            Return CType(ViewState("ArchivoRecomendacion"), EntOttAdjuntoOrdenTrabajo)
        End Get
        Set(value As EntOttAdjuntoOrdenTrabajo)
            ViewState("ArchivoRecomendacion") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta al cargar la pagina e inicializa componentes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/04/2016</creationDate>
    ''' <changeLog></changeLog>
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
    ''' Maneja el evento de devolver el expediente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>18/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub DevolverExpediente_Click(sender As Object, e As EventArgs)
        If Page.IsValid Then
            Try
                If Devolver() Then
                    MostrarExitoAlListado()
                Else
                    MostrarAlertaError("No ha sido posible devolver el expediente")
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
        End If
    End Sub

    ''' <summary>
    ''' Se encarga del evento para cerrar la etapa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>18/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnCerrarEtapa_Click(sender As Object, e As EventArgs)
        Try
            If CerrarEtapa() Then
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

    ''' <summary>
    ''' Se ejecuta al dar click en cerrar adjudicacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnCerrarAdjudicacion_Click(sender As Object, e As EventArgs)
        Try
            If CerrarEtapaAdjudicacion() Then
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardarInicio_Click(sender As Object, e As EventArgs)
        Try
            If GuardarInicio() Then
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


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardarCartel_Click(sender As Object, e As EventArgs)
        Try
            If GuardarCartel() Then
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>29/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnCerrarRecomendacion_Click(sender As Object, e As EventArgs)
        Try
            If GuardarRecomendacion() Then
                If Me.chkContratacionInfructuosa.Checked Then
                    MostrarExitoAlListado()

                Else
                    WebUtils.RegistrarScript(Me, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
                End If

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


    ''' <summary>
    ''' Evento que se ejecuta cuando se da carga cada uno de los elementos del repeater de titulos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>18/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpListaTapsTitulos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpListaTapsTitulos.ItemDataBound
        Dim vlo_HtmlAnchor As HtmlAnchor

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_HtmlAnchor = CType(e.Item.FindControl("cuerpoTabPanel"), HtmlAnchor)
            vlo_HtmlAnchor.HRef = String.Format("#{0}_{1}", "cphContenidoFormulario_cphFormulario_rpListaTapsContenidos_cuerpoTabPanel", Me.ContadorTaps.ToString)

            Me.ContadorTaps = Me.ContadorTaps + 1
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da carga cada uno de los elementos del repeater de contenidos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>18/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpListaTapsContenidos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpListaTapsContenidos.ItemDataBound
        Dim vlo_WebUserControl As Controles_wuc_OT_ExpedienteTecContrataciones

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_WebUserControl = CType(e.Item.FindControl("wucExpedienteTecnico"), Controles_wuc_OT_ExpedienteTecContrataciones)
            vlo_WebUserControl.IdOrdenTrabajo = Me.IdOrdenTrabajo
            vlo_WebUserControl.IdUbicacion = Me.IdUbicacion
            vlo_WebUserControl.Inicializar()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da carga cada uno de los elementos del repeater de contenidos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>9/5/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpLineas_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpLineas.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        Dim vlo_IbConsultar As ImageButton
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibConsultar") IsNot Nothing Then
                vlo_IbConsultar = CType(e.Item.FindControl("ibConsultar"), ImageButton)
                vlo_IbConsultar.Attributes.Add("data-uniqueid", vlo_IbConsultar.UniqueID)
                If Me.Contratacion.Editable = Version.NO_EDITABLE Then
                    vlo_IbConsultar.Visible = False
                End If
            End If
            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
                If Me.Contratacion.Editable = Version.NO_EDITABLE Then
                    vlo_IbBorrar.Visible = False
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' evento que se ejecuta para adjuntar un identificador unico al set de archivos de inicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpAdjuntosInicio_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpAdjuntosInicio.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' evento que se ejecuta para adjuntar un identificador unico al set de archivos de inicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpDocumentosAdjudicacion_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpDocumentosAdjudicacion.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Genera la nueva versión editable del ante proyecto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>19/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnNuevaVersion_Click(sender As Object, e As EventArgs) Handles btnAceptarVersion.Click
        Try
            If Not String.IsNullOrWhiteSpace(Me.ddlEtapaVersion.SelectedValue) Then
                If Me.ddlEtapaVersion.SelectedValue <> 0 Then
                    If GenerarNuevaVersion() Then
                        WebUtils.RegistrarScript(Me, "nuevaVersion", "mostrarAlertaNuevaVersion();")
                    End If

                Else
                    WebUtils.RegistrarScript(Me, "mostrarAlertaError", "mostrarAlertaError('Para cambiar la versión debe seleccionar una etapa válida');")
                End If

            Else
                If Me.VersionActual = 1 Then
                    If Not String.IsNullOrWhiteSpace(ddlVersion.SelectedValue) Then
                        If GenerarNuevaVersion() Then
                            WebUtils.RegistrarScript(Me, "nuevaVersion", "mostrarAlertaNuevaVersion();")
                        End If
                    Else
                        WebUtils.RegistrarScript(Me, "mostrarAlertaError", "mostrarAlertaError('Para cambiar la versión debe seleccionar una etapa válida');")
                    End If
                Else
                    If Me.OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PENDIENTE_REVISION_CONTRATACIONES Then
                        Me.ddlEtapaVersion.SelectedIndex = EtapaContratacion.EXPEDIENTE_TECNICO
                        If GenerarNuevaVersion() Then
                            WebUtils.RegistrarScript(Me, "nuevaVersion", "mostrarAlertaNuevaVersion();")
                        End If
                    Else
                        WebUtils.RegistrarScript(Me, "mostrarAlertaError", "mostrarAlertaError('Para cambiar la versión debe seleccionar una etapa válida');")
                    End If
                End If
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>20/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlVersion_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If Not String.IsNullOrEmpty(Me.ddlVersion.SelectedValue) Then
                CargarContratacion()
                If Me.Contratacion.Existe Then
                    Me.EtapaActual = Contratacion.IdEtapaContratacion
                    ConfigurarVisualizacion()
                    mostrarTabs()
                    ConfigurarTabInicio()
                    ConfigurarTabPublicacion()
                    ConfigurarTabRecomendacionTecnica()
                    ConfigurarTabAdjudicacion()
                    InicializarControlesUsuario()
                Else
                    ocultarTabs()
                End If
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

    ''' <summary>
    ''' Evento que manda a eliminar una linea de contratacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>9/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrarLinea_Click(sender As Object, e As ImageClickEventArgs)
        Dim vln_Indice As Integer
        Dim vln_IdNumLinea As Integer

        Try
            vln_Indice = Convert.ToInt32(CType(sender, ImageButton).CommandName)
            vln_IdNumLinea = Me.DsLineas.Tables(0).Rows(vln_Indice)(Modelo.OTT_LINEA_ADJUDICACION.NUMERO_LINEA).ToString

            If BorrarLinea(vln_IdNumLinea) Then

                Me.DsLineas.Tables(0).Rows(vln_Indice).Delete()

                If Me.DsLineas IsNot Nothing AndAlso Me.DsLineas.Tables(0).Rows.Count > 0 Then
                    Me.rpLineas.DataSource = Me.DsLineas
                    Me.rpLineas.DataMember = Me.DsLineas.Tables(0).TableName
                    Me.rpLineas.DataBind()
                Else
                    With Me.rpLineas
                        .DataSource = Nothing
                        .DataBind()
                    End With
                    rpLineas.Visible = False
                End If

            End If
            activarAdjudicacionLineas()


        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que manda a eliminar una linea de contratacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>9/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibConsultar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vln_Indice As Integer

        Try
            If EtapaActual = EtapaContratacion.ADJUDICACION Then
                btnAgregarLinea.Visible = False
                btnModificarLinea.Visible = True
                btnCancelar.Visible = True
            End If

            vln_Indice = Convert.ToInt32(CType(sender, ImageButton).CommandName)
            txtNumLinea.Text = Me.DsLineas.Tables(0).Rows(vln_Indice)(Modelo.OTT_LINEA_ADJUDICACION.NUMERO_LINEA).ToString
            txtMonto.Text = Me.DsLineas.Tables(0).Rows(vln_Indice)(Modelo.OTT_LINEA_ADJUDICACION.MONTO_ADJUDICADO).ToString
            txtAdjudicatario.Text = Me.DsLineas.Tables(0).Rows(vln_Indice)(Modelo.OTT_LINEA_ADJUDICACION.ADJUDICATARIO).ToString
            txtFechaInicio.Text = CDate(Me.DsLineas.Tables(0).Rows(vln_Indice)(Modelo.OTT_LINEA_ADJUDICACION.FECHA_INICIO_OBRA)).ToString(Constantes.FORMATO_FECHA_UI)
            txtPlazo.Text = Me.DsLineas.Tables(0).Rows(vln_Indice)(Modelo.OTT_LINEA_ADJUDICACION.PLAZO_EN_DIAS).ToString
            ddlDias.SelectedValue = IIf(Me.DsLineas.Tables(0).Rows(vln_Indice)(Modelo.OTT_LINEA_ADJUDICACION.FORMA_CALCULO_DIAS).ToString = FormaDias.HABILES, Dias.HABILES, Dias.NATURALES)
            lblFinEstimada.Text = CDate(Me.DsLineas.Tables(0).Rows(vln_Indice)(Modelo.OTT_LINEA_ADJUDICACION.FECHA_FIN_ESTIMADA)).ToString(Constantes.FORMATO_FECHA_UI)
            cargarArchivoLinea(txtNumLinea.Text)
            activarAdjudicacionLineas()
        Catch ex As Exception
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
    ''' <creationDate>04/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrarAdjunto_Click(sender As Object, e As ImageClickEventArgs)
        Dim vln_Indice As Integer
        Dim vln_IdAdjunto As Integer
        Dim vlc_Indicador As String

        Try
            vln_Indice = Convert.ToInt32(CType(sender, ImageButton).CommandName)
            vlc_Indicador = Convert.ToString(CType(sender, ImageButton).CommandArgument)
            If String.Compare(vlc_Indicador, "A") = 0 Then
                vln_IdAdjunto = Me.DsArchivosAdjudicacion.Tables(0).Rows(vln_Indice)(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ADJUNTO_ORDEN_TRABAJO).ToString
            Else
                vln_IdAdjunto = Me.DsArchivosInicio.Tables(0).Rows(vln_Indice)(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ADJUNTO_ORDEN_TRABAJO).ToString
            End If


            If Borrar(vln_IdAdjunto) Then


                If String.Compare(vlc_Indicador, "A") = 0 Then
                    Me.DsArchivosAdjudicacion.Tables(0).Rows(vln_Indice).Delete()

                    If Me.DsArchivosAdjudicacion IsNot Nothing AndAlso Me.DsArchivosAdjudicacion.Tables(0).Rows.Count > 0 Then
                        Me.rpDocumentosAdjudicacion.DataSource = Me.DsArchivosAdjudicacion
                        Me.rpDocumentosAdjudicacion.DataMember = Me.DsArchivosAdjudicacion.Tables(0).TableName
                        Me.rpDocumentosAdjudicacion.DataBind()
                    Else
                        With Me.rpDocumentosAdjudicacion
                            .DataSource = Nothing
                            .DataBind()
                        End With
                    End If
                    activarAdjudicacionDocumentos()
                Else
                    Me.DsArchivosInicio.Tables(0).Rows(vln_Indice).Delete()

                    If Me.DsArchivosInicio IsNot Nothing AndAlso Me.DsArchivosInicio.Tables(0).Rows.Count > 0 Then
                        Me.rpAdjuntosInicio.DataSource = Me.DsArchivosInicio
                        Me.rpAdjuntosInicio.DataMember = Me.DsArchivosInicio.Tables(0).TableName
                        Me.rpAdjuntosInicio.DataBind()
                    Else
                        With Me.rpAdjuntosInicio
                            .DataSource = Nothing
                            .DataBind()
                        End With
                    End If

                End If


            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' opcion de descargar un archivo seleccionado de la lista
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivo_Command(sender As Object, e As CommandEventArgs)
        Try
            Dim vln_IdAdjunto As Integer = Me.DsArchivosInicio.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ADJUNTO_ORDEN_TRABAJO).ToString

            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsArchivosInicio.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.V_OTT_DOCUMENTO_CONTRATLST.NOMBRE_ARCHIVO).ToString)
            Dim vlo = ObtenerArchivo(vln_IdAdjunto)
            Response.BinaryWrite(vlo.Archivo)
            Response.End()

        Catch ex As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' opcion de descargar un archivo seleccionado de la lista
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivoAdjudicacion_Command(sender As Object, e As CommandEventArgs)
        Try
            Dim vln_IdAdjunto As Integer = Me.DsArchivosAdjudicacion.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ADJUNTO_ORDEN_TRABAJO).ToString

            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsArchivosAdjudicacion.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.V_OTT_DOCUMENTO_CONTRATLST.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CType(ObtenerArchivo(vln_IdAdjunto).Archivo, Byte()))
            Response.End()

        Catch ex As System.Threading.ThreadAbortException
            Throw
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
    Protected Sub btnAgregardjudicacion_Click(sender As Object, e As EventArgs) Handles btnAgregardjudicacion.Click
        Try
            AgregarArchivoAdjudicacion()
            activarAdjudicacionDocumentos()
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
            activarInicio()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Modifica el comportamiento de la pagina y agrega el archivo a una variable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarCartel_Click(sender As Object, e As EventArgs)
        Try
            Me.ArchivoCartel.NombreArchivo = Me.fuCartel.FileName
            Me.ArchivoCartel.Archivo = Me.fuCartel.FileBytes
            fuCartel.Visible = False
            Me.lnkArchivoCartel.Text = Me.fuCartel.FileName
            btnAgregarCartel.Visible = False
            lnkArchivoCartel.Visible = True
            btnEliminarCartel.Visible = True
            activarCartel()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' agrega el archivo tipo oficio para la linea de contratacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarDocAdjudicacion_Click(sender As Object, e As EventArgs)
        Try
            Me.ArchivoLinea.NombreArchivo = Me.fuDocAdjudicacion.FileName
            Me.ArchivoLinea.Archivo = Me.fuDocAdjudicacion.FileBytes
            Me.lnkAdjudicacion.Attributes.Add("data-idAdjunto", 0)
            fuDocAdjudicacion.Visible = False
            Me.lnkAdjudicacion.Text = Me.fuDocAdjudicacion.FileName
            btnAgregarDocAdjudicacion.Visible = False
            lnkAdjudicacion.Visible = True
            btnEliminarArchivoLinea.Visible = True
            activarAdjudicacionLineas()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' elimina el archivo cartel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnEliminarArchivoLinea_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminarArchivoLinea.Click
        Try
            Me.ArchivoLinea.Archivo = Nothing
            Me.ArchivoLinea.NombreArchivo = String.Empty
            Me.fuDocAdjudicacion.Visible = True
            Me.lnkAdjudicacion.Visible = False
            Me.btnEliminarArchivoLinea.Visible = False
            btnAgregarDocAdjudicacion.Visible = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        activarAdjudicacionLineas()
    End Sub

    ''' <summary>
    ''' descarga el archivo adjunto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivoCartel_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.ArchivoCartel.NombreArchivo)
            Response.BinaryWrite(Me.ArchivoCartel.Archivo)
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' elimina el archivo cartel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnEliminarCartela_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminarCartel.Click
        Try
            Me.ArchivoCartel.Archivo = Nothing
            Me.ArchivoCartel.NombreArchivo = String.Empty
            Me.fuCartel.Visible = True
            Me.lnkArchivoCartel.Visible = False
            Me.btnEliminarCartel.Visible = False
            btnAgregarCartel.Visible = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        activarCartel()
    End Sub

    ''' <summary>
    ''' Descarga el archivo recomendacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>29/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRecomendacion_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.ArchivoRecomendacion.NombreArchivo)
            Response.BinaryWrite(Me.ArchivoRecomendacion.Archivo)
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Descarga el archivo recomendacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkAdjudicacion_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.ArchivoLinea.NombreArchivo)
            Response.BinaryWrite(Me.ArchivoLinea.Archivo)
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Agrega el archivo recomendacion seleccionado por el usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>29/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarRecomendacion_Click(sender As Object, e As EventArgs)
        Try
            Me.ArchivoRecomendacion.NombreArchivo = Me.fuRecomendacion.FileName
            Me.ArchivoRecomendacion.Archivo = Me.fuRecomendacion.FileBytes
            fuRecomendacion.Visible = False
            Me.lnkRecomendacion.Text = Me.fuRecomendacion.FileName
            btnAgregarRecomendacion.Visible = False
            lnkRecomendacion.Visible = True
            btnEliminarRecomendacion.Visible = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        WebUtils.RegistrarScript(Me, "activarRecTecnica", "activarRecTecnica();")
    End Sub

    ''' <summary>
    ''' Elimina el archivo de recomendacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>29/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnEliminarRecomendacion_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminarRecomendacion.Click
        Try
            Me.ArchivoRecomendacion.Archivo = Nothing
            Me.ArchivoRecomendacion.NombreArchivo = String.Empty
            Me.fuRecomendacion.Visible = True
            Me.lnkRecomendacion.Visible = False
            Me.btnEliminarRecomendacion.Visible = False
            btnAgregarRecomendacion.Visible = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        WebUtils.RegistrarScript(Me, "activarRecTecnica", "activarRecTecnica();")
    End Sub

    ''' <summary>
    ''' Calcula el fin de fecha estimada y lo coloca en el label
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub txtFechaInicio_TextChanged(sender As Object, e As EventArgs) Handles txtFechaInicio.TextChanged
        Dim vld_FechaInicio As Date
        Dim vln_Plazo As Integer
        Dim vln_Dias As Integer
        Dim vld_Resultado As Date
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Not String.IsNullOrEmpty(Me.txtFechaInicio.Text) AndAlso Not String.IsNullOrEmpty(Me.txtPlazo.Text) AndAlso Not String.IsNullOrEmpty(ddlDias.SelectedValue) Then
                vld_FechaInicio = CType(Me.txtFechaInicio.Text, Date)
                vln_Plazo = CInt(Me.txtPlazo.Text)
                vln_Dias = ddlDias.SelectedValue

                If vln_Dias = Dias.HABILES Then
                    vld_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_ADJUDICACION_SumaDiasHabiles_CierreUCR(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        vld_FechaInicio, vln_Plazo)
                Else
                    vld_Resultado = DateAdd(DateInterval.Day, vln_Plazo, vld_FechaInicio)
                End If

                Me.lblFinEstimada.Text = vld_Resultado.ToString(Constantes.FORMATO_FECHA_UI)
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
        activarAdjudicacionLineas()
    End Sub

    ''' <summary>
    ''' Calcula el fin de fecha estimada y lo coloca en el label
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub FinFechaEstimada(sender As Object, e As EventArgs) Handles txtPlazo.TextChanged
        Dim vld_FechaInicio As Date
        Dim vln_Plazo As Integer
        Dim vln_Dias As Integer
        Dim vld_Resultado As Date
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Not String.IsNullOrEmpty(Me.txtFechaInicio.Text) AndAlso Not String.IsNullOrEmpty(Me.txtPlazo.Text) AndAlso Not String.IsNullOrEmpty(ddlDias.SelectedValue) Then
                vld_FechaInicio = CType(Me.txtFechaInicio.Text, Date)
                vln_Plazo = CInt(Me.txtPlazo.Text)
                vln_Dias = ddlDias.SelectedValue

                If vln_Dias = Dias.HABILES Then
                    vld_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_ADJUDICACION_SumaDiasHabiles_CierreUCR(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        vld_FechaInicio, vln_Plazo)
                Else
                    vld_Resultado = DateAdd(DateInterval.Day, vln_Plazo, vld_FechaInicio)
                End If

                Me.lblFinEstimada.Text = vld_Resultado.ToString(Constantes.FORMATO_FECHA_UI)
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
        activarAdjudicacionLineas()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarLinea_Click(sender As Object, e As EventArgs)
        Try
            If agregarLinea() Then
                activarAdjudicacionLineas()
                WebUtils.RegistrarScript(Me, "mostrarNuevaLineaGuardada", "mostrarNuevaLineaGuardada();")
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>09/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnModificarLinea_Click(sender As Object, e As EventArgs)
        Try
            If modificarLinea() Then
                activarAdjudicacionLineas()
                WebUtils.RegistrarScript(Me, "mostrarNuevaLineaGuardada", "mostrarNuevaLineaGuardada();")
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

    ''' <summary>
    ''' Cancela la operación de modificar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>11/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        Me.txtNumLinea.Text = String.Empty
        Me.txtMonto.Text = String.Empty
        Me.txtAdjudicatario.Text = String.Empty
        Me.txtFechaInicio.Text = String.Empty
        Me.txtPlazo.Text = String.Empty
        Me.lblFinEstimada.Text = String.Empty
        Me.ddlDias.SelectedIndex = 0
        Me.ArchivoLinea = New EntOttAdjuntoOrdenTrabajo

        Me.ArchivoLinea.Archivo = Nothing
        Me.ArchivoLinea.NombreArchivo = String.Empty
        Me.fuDocAdjudicacion.Visible = True
        Me.lnkAdjudicacion.Visible = False
        Me.btnEliminarArchivoLinea.Visible = False
        btnAgregarDocAdjudicacion.Visible = True

        Me.btnModificarLinea.Visible = False
        Me.btnAgregarLinea.Visible = True
        Me.btnCancelar.Visible = False
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>18/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenTrabajoMadre(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String)
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
        Else
        End If
    End Sub

    ''' <summary>
    ''' Carga las variables para validar el archivo 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>21/04/2016</creationDate>
    Private Sub CargarTiposDocumento()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As Wsr_OT_Catalogos.EntOtmTipoDocumento

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.CLAUSULA_PENAL))

            If vlo_EntOtmTipoDocumento.Existe Then
                Me.ExtensionesPermitidasInicio = vlo_EntOtmTipoDocumento.FormatosAdmitidos
                imgExtensionesInicio.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidasInicio.ToLower))
                Me.TamanoArchivoInicio = vlo_EntOtmTipoDocumento.TamanioMaximo
            End If

            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.CARTEL))

            If vlo_EntOtmTipoDocumento.Existe Then
                Me.ExtensionesPermitidasCartel = vlo_EntOtmTipoDocumento.FormatosAdmitidos
                imgExtensionesCartel.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidasCartel.ToLower))
                Me.TamanoArchivoCartel = vlo_EntOtmTipoDocumento.TamanioMaximo
            End If

            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.OFICIO))

            If vlo_EntOtmTipoDocumento.Existe Then
                Me.ExtensionesPermitidasOficio = vlo_EntOtmTipoDocumento.FormatosAdmitidos
                imgExtensionesRecomendacion.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidasOficio.ToLower))
                imgExtensionesAdjudicacion.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidasOficio.ToLower))
                imgExtensionesInicioObra.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidasOficio.ToLower))
                Me.TamanoArchivoOficio = vlo_EntOtmTipoDocumento.TamanioMaximo
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
    ''' Carga y conturye los tabs de etapas
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>18/04/2016</creationDate>
    ''' <changeLog> Carlos Gómez -- Solamente se cargan los taps con documentos adjuntos</changeLog>
    Private Sub CargarTapsEtapas()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet
        Dim vln_CantidadAdjuntos As Integer = 0

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_ETAPA_ORDEN_TRABAJO_ListarRegistros(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                            String.Format("{0} LIKE '%{1}%'", Modelo.OTM_ETAPA_ORDEN_TRABAJO.ESTADO, Estado.ACTIVO),
                            String.Format("{0} {1}", Modelo.OTM_ETAPA_ORDEN_TRABAJO.ORDEN, Ordenamiento.ASCENDENTE),
                            False,
                            0,
                            0)

            For Each vlo_Fila In vlo_DsDatos.Tables(0).Rows
                vln_CantidadAdjuntos = CargarCantidadAdjuntosOrdenTrabajoaEtapa(Me.IdUbicacion, Me.IdOrdenTrabajo, CType(vlo_Fila(Modelo.OTM_ETAPA_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO), Integer))
                If Not (vln_CantidadAdjuntos > 0) Then
                    vlo_Fila.Delete()
                End If
                vln_CantidadAdjuntos = 0
            Next

            vlo_DsDatos.AcceptChanges()

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpListaTapsTitulos
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With

                With Me.rpListaTapsContenidos
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    Private Sub activarInicio()
        WebUtils.RegistrarScript(Me, "activarInicio", "activarInicio();")
    End Sub

    Private Sub activarAdjudicacion()
        WebUtils.RegistrarScript(Me, "activarAdjudicacion", "activarAdjudicacion();")
    End Sub

    Private Sub activarAdjudicacionDocumentos()
        WebUtils.RegistrarScript(Me, "activarAdjudicacionDocumentos", "activarAdjudicacionDocumentos();")
    End Sub

    Private Sub activarAdjudicacionLineas()
        WebUtils.RegistrarScript(Me, "activarAdjudicacionLineas", "activarAdjudicacionLineas();")
    End Sub

    Private Sub activarCartel()
        WebUtils.RegistrarScript(Me, "activarCartel", "activarCartel();")
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub MostrarExitoAlListado()
        WebUtils.RegistrarScript(Me, "MostrarExitoAlListado", "MostrarExitoAlListado();")
    End Sub


    ''' <summary>
    ''' Inicializa el formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Usuario = New UsuarioActual()
        leerParametros()
        CargarOrdenTrabajo(Me.IdUbicacion, Me.IdOrdenTrabajo)
        inicializarListaDias()
        CargarContrataciones()
        CargarListaViaContrato(String.Format("{0} in ('{1}','{2}')", Modelo.V_OTM_VIA_COMPRA_CONTRATO.AMBITO, Utilerias.OrdenesDeTrabajo.Ambito.AMBOS, Utilerias.OrdenesDeTrabajo.Ambito.CONTRATACIONES))
        ConfigurarVisualizacion()
        CargarTiposDocumento()
        ConfigurarTabInicio()
        ConfigurarTabPublicacion()
        ConfigurarTabRecomendacionTecnica()
        ConfigurarTabAdjudicacion()
        InicializarControlesUsuario()
    End Sub

    ''' <summary>
    ''' Se encarga de leer los parámetros provenientes del listado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub leerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        Me.IdSectorTaller = WebUtils.LeerParametro(Of Integer)("pvn_IdSectorTaller")

        Me.Session.Add("pvn_IdUbicacion", IdUbicacion)
        Me.Session.Add("pvc_IdOrdenTrabajo", IdOrdenTrabajo)
        Me.Session.Add("pvn_Anno", Anno)
        Me.Session.Add("pvn_IdSectorTaller", IdSectorTaller)


    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario para mostrar la información general
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarControlesUsuario()

        Me.btnEliminarRecomendacion.Attributes.Add("data-uniqueid", btnEliminarRecomendacion.UniqueID)
        Me.btnEliminarArchivoLinea.Attributes.Add("data-uniqueid", btnEliminarArchivoLinea.UniqueID)
        Me.btnEliminarCartel.Attributes.Add("data-uniqueid", btnEliminarCartel.UniqueID)
        Me.btnAceptarVersion.Attributes.Add("data-uniqueid", btnAceptarVersion.UniqueID)

        'Asignación de Datos para generar el web user control de la información general
        ctrl_InfoGeneral.Anno = Me.Anno
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.IdUbicacion

        ctrl_InfoGeneral.NumContrato = Contratacion.NumeroContrato
        ctrl_InfoGeneral.NombreContrato = Contratacion.NombreContrato

        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()

        If OrdenTrabajo.EstadoOrdenTrabajo <> EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE AndAlso
            OrdenTrabajo.EstadoOrdenTrabajo <> EstadoOrden.CONTRATACION_INICIO AndAlso
            OrdenTrabajo.EstadoOrdenTrabajo <> EstadoOrden.CONTRATACION_PUBLICACION_CARTEL Then
            ctrl_Aclaraciones.IdOrdenTrabajo = IdOrdenTrabajo
            ctrl_Aclaraciones.IdUbicacion = IdUbicacion
            ctrl_Aclaraciones.EtapaActual = Me.EtapaActual
            ctrl_Aclaraciones.Version = Me.Contratacion.Version
            ctrl_Aclaraciones.Editable = Contratacion.Editable
            ctrl_Aclaraciones.Cargo = Cargo.ENCARGADO
            Me.ctrl_Aclaraciones.Inicializar()

            If OrdenTrabajo.EstadoOrdenTrabajo <> EstadoOrden.CONTRATACION_ACLARACIONES Then
                ctrl_Ofertas.IdOrdenTrabajo = IdOrdenTrabajo
                ctrl_Ofertas.IdUbicacion = IdUbicacion
                ctrl_Ofertas.EtapaActual = Me.EtapaActual
                ctrl_Ofertas.Version = Me.Contratacion.Version
                ctrl_Ofertas.Editable = Contratacion.Editable
                ctrl_Ofertas.Cargo = Cargo.ENCARGADO
                Me.ctrl_Ofertas.Inicializar()
            End If


        End If




    End Sub

    ''' <summary>
    ''' Se encarga de mostrar los campos de la publicacion cartel para consulta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub documentoAMostrar()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDocumentoContratacion As EntOttDocumentoContratacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.txtNumContrato.Enabled = False
            txtNumContrato.Text = Contratacion.NumeroContrato
            txtNombreContrato.Text = Contratacion.NombreContrato
            txtNombreContrato.Enabled = False
            fuCartel.Visible = False
            btnAgregarCartel.Visible = False
            btnEliminarCartel.Visible = False


            vlo_EntOttDocumentoContratacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}' AND {6} = {7}",
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ETAPA_CONTRATACION,
                              EtapaContratacion.PUBLICACION_CARTEL,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION,
                              IdUbicacion, Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO,
                              IdOrdenTrabajo, Modelo.OTT_DOCUMENTO_CONTRATACION.VERSION, Contratacion.Version))

            If vlo_EntOttDocumentoContratacion.Existe Then
                Me.ArchivoCartel = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}",
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO,
                              vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo))
                If ArchivoCartel.Existe Then
                    lnkArchivoCartel.Text = ArchivoCartel.NombreArchivo
                    lnkArchivoCartel.Visible = True
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Carga el archivo de la recomendacion tecnica
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub documentoRecomendacion()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDocumentoContratacion As EntOttDocumentoContratacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.txtNumContrato.Enabled = False
            txtNumContrato.Text = Contratacion.NumeroContrato
            txtNombreContrato.Text = Contratacion.NombreContrato
            txtNombreContrato.Enabled = False
            fuCartel.Visible = False
            btnAgregarCartel.Visible = False
            btnEliminarCartel.Visible = False


            vlo_EntOttDocumentoContratacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}' AND {6} = {7}",
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ETAPA_CONTRATACION,
                              EtapaContratacion.RECOMENDACION_TECNICA,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION,
                              IdUbicacion, Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO,
                              IdOrdenTrabajo, Modelo.OTT_DOCUMENTO_CONTRATACION.VERSION, Contratacion.Version))

            If vlo_EntOttDocumentoContratacion.Existe Then
                Me.ArchivoRecomendacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}",
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO,
                              vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo))
                If ArchivoCartel.Existe Then
                    lnkRecomendacion.Text = ArchivoRecomendacion.NombreArchivo
                    lnkRecomendacion.Visible = True
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Carga el archivo de la linea de adjudicacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarDocumentoAdjudicacion()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDocumentoContratacion As EntOttDocumentoContratacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.txtNumContrato.Enabled = False
            txtNumContrato.Text = Contratacion.NumeroContrato
            txtNombreContrato.Text = Contratacion.NombreContrato
            txtNombreContrato.Enabled = False
            fuCartel.Visible = False
            btnAgregarCartel.Visible = False
            btnEliminarCartel.Visible = False


            vlo_EntOttDocumentoContratacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}' AND {6} = {7} AND {8} = {9}",
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ETAPA_CONTRATACION,
                              EtapaContratacion.ADJUDICACION,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION,
                              IdUbicacion, Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO,
                              IdOrdenTrabajo, Modelo.OTT_DOCUMENTO_CONTRATACION.VERSION, Contratacion.Version,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.DOCUMENTO_TRAMITADO, Documento.TRAMITADO))

            If vlo_EntOttDocumentoContratacion.Existe Then
                Me.ArchivoLinea = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}",
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO,
                              vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo))
                If ArchivoCartel.Existe Then
                    lnkAdjudicacion.Text = ArchivoLinea.NombreArchivo
                    lnkAdjudicacion.Visible = True
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Configura el dataset de archivos para su inicializacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ConfigurarTabInicio()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            DsArchivosInicio = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5} AND {6} = {7}",
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION,
                              IdUbicacion,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO,
                              IdOrdenTrabajo,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.VERSION,
                              Contratacion.Version,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ETAPA_CONTRATACION,
                              EtapaContratacion.INICIO), String.Empty, False, 0, 0)



            If DsArchivosInicio IsNot Nothing AndAlso DsArchivosInicio.Tables(0).Rows.Count > 0 Then
                Me.rpAdjuntosInicio.DataSource = DsArchivosInicio
                Me.rpAdjuntosInicio.DataMember = DsArchivosInicio.Tables(0).TableName
                Me.rpAdjuntosInicio.DataBind()
            Else
                With Me.rpAdjuntosInicio
                    .DataSource = Nothing
                    .DataBind()
                End With
            End If

            If OrdenTrabajo.EstadoOrdenTrabajo <> EstadoOrden.CONTRATACION_INICIO Then
                Me.txtDescicionInicial.Text = Contratacion.NumeroDecisionInicial
                Me.txtNSolicitud.Text = Contratacion.NumeroSolicitud
                txtDescicionInicial.Enabled = False
                txtNSolicitud.Enabled = False

                fuDocumento.Visible = False
                btnAgregarDocumento.Visible = False
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
    ''' Configura y carga el data set de archivos de adjudicación
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ConfigurarTabAdjudicacion()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If Me.Contratacion.Editable = Version.NO_EDITABLE Then
                Me.btnAgregarLinea.Visible = False
                Me.btnAgregarDocAdjudicacion.Visible = False
                btnCerrarAdjudicacion.Enabled = False
                btnAgregardjudicacion.Enabled = False
            End If


            Me.DsArchivosAdjudicacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5} AND {6} = {7} AND {8} = 0",
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION,
                              IdUbicacion,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO,
                              IdOrdenTrabajo,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.VERSION,
                              Contratacion.Version,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ETAPA_CONTRATACION,
                              EtapaContratacion.ADJUDICACION,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.NUMERO_LINEA), String.Empty, False, 0, 0)

            Me.DsLineas = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_ADJUDICACION_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}",
                            Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION,
                            IdUbicacion,
                            Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO,
                            IdOrdenTrabajo,
                            Modelo.OTT_DOCUMENTO_CONTRATACION.VERSION,
                            Contratacion.Version), String.Format("{0} ASC", Modelo.OTT_LINEA_ADJUDICACION.NUMERO_LINEA), False, 0, 0)



            If DsArchivosAdjudicacion IsNot Nothing AndAlso DsArchivosAdjudicacion.Tables(0).Rows.Count > 0 Then
                Me.rpDocumentosAdjudicacion.DataSource = DsArchivosAdjudicacion
                Me.rpDocumentosAdjudicacion.DataMember = DsArchivosAdjudicacion.Tables(0).TableName
                Me.rpDocumentosAdjudicacion.DataBind()

            Else
                With Me.rpDocumentosAdjudicacion
                    .DataSource = Nothing
                    .DataBind()
                End With
            End If

            If DsLineas IsNot Nothing AndAlso DsLineas.Tables(0).Rows.Count > 0 Then
                Me.rpLineas.DataSource = DsLineas
                Me.rpLineas.DataMember = DsLineas.Tables(0).TableName
                Me.rpLineas.DataBind()

            Else
                With Me.rpLineas
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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ConfigurarTabPublicacion()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDocumentoContratacion As EntOttDocumentoContratacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttDocumentoContratacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5} AND {6} = {7}",
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION,
                              IdUbicacion,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO,
                              IdOrdenTrabajo,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ETAPA_CONTRATACION,
                              EtapaContratacion.PUBLICACION_CARTEL,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.VERSION,
                              Contratacion.Version))

            If vlo_EntOttDocumentoContratacion.Existe Then
                Me.txtNumContrato.Text = Contratacion.NumeroContrato
                Me.txtNombreContrato.Text = IIf(String.IsNullOrWhiteSpace(Contratacion.NombreContrato), Me.OrdenTrabajo.NombreProyecto, Contratacion.NombreContrato)

                Me.ArchivoCartel = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO,
                              vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo))

                Me.fuCartel.Visible = False
                Me.btnAgregarCartel.Visible = False
                Me.lnkArchivoCartel.Text = ArchivoCartel.NombreArchivo
                lnkArchivoCartel.Visible = True
                btnEliminarCartel.Visible = False

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
    ''' Carga los datos de la recomendacion tecnica si existen
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>2/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ConfigurarTabRecomendacionTecnica()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDocumentoContratacion As EntOttDocumentoContratacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttDocumentoContratacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5} AND {6} = {7}",
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION,
                              IdUbicacion,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO,
                              IdOrdenTrabajo,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ETAPA_CONTRATACION,
                              EtapaContratacion.RECOMENDACION_TECNICA,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.VERSION,
                              Contratacion.Version))

            If vlo_EntOttDocumentoContratacion.Existe Then


                Me.ArchivoRecomendacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO,
                              vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo))

                Me.fuRecomendacion.Visible = False
                Me.btnAgregarRecomendacion.Visible = False
                Me.lnkRecomendacion.Visible = True
                Me.btnEliminarRecomendacion.Visible = False
                chkContratacionInfructuosa.Visible = False

                Me.lnkRecomendacion.Text = ArchivoRecomendacion.NombreArchivo
            Else
                Me.lnkRecomendacion.Text = String.Empty
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
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">id de la ubicacion</param>
    ''' <param name="pvn_IdOrdenTrabajo">id de la orden</param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/04/2016</creationDate>
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
    ''' Carga el repeater de datos con lo obtenido desde la base
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaViaContrato(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet
        Dim vlo_EntOttViabilidadTecnica As EntOttViabilidadTecnica



        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlViaContrato.Items.Clear()
            Me.ddlViaContrato.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, 0))


            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_VIA_COMPRA_CONTRATO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, String.Format("{0} {1}", Modelo.V_OTM_VIA_COMPRA_CONTRATO.TOPE_ECONOMICO, Ordenamiento.ASCENDENTE), False, 0, 0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlViaContrato
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_VIA_COMPRA_CONTRATO.DESCRIPCION
                    .DataValueField = Modelo.V_OTM_VIA_COMPRA_CONTRATO.ID_VIA_COMPRA_CONTRATO
                    .DataBind()
                End With
            End If

            vlo_EntOttViabilidadTecnica = vlo_Ws_OT_OrdenesDeTrabajo.OTT_VIABILIDAD_TECNICA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}' AND {2} = {3}", Modelo.OTT_VIABILIDAD_TECNICA.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.OTT_VIABILIDAD_TECNICA.ID_UBICACION, Me.IdUbicacion))

            If vlo_EntOttViabilidadTecnica.Existe Then
                If Not Contratacion.Existe Then
                    Me.Contratacion.IdViaContrato = vlo_Ws_OT_OrdenesDeTrabajo.OTT_CONTRATACION_ObtenerIdViaContratacion(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), vlo_EntOttViabilidadTecnica.EstimacionPresupuestaria)
                End If
                Me.lblMonto.Text = String.Format("₡{0}", vlo_EntOttViabilidadTecnica.EstimacionPresupuestaria.ToString("N2"))
            End If
            If Me.Contratacion.IdViaContrato <> 0 Then
                Me.ddlViaContrato.SelectedValue = Me.Contratacion.IdViaContrato
            End If
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
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ConfigurarVisualizacion()
        If Me.OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PENDIENTE_REVISION_CONTRATACIONES Then
            Me.lblEtapaActual.Visible = False
        End If

        If Me.OrdenTrabajo.EstadoOrdenTrabajo <> EstadoOrden.PENDIENTE_REVISION_CONTRATACIONES Then
            mostrarTabs()
        End If

        If EtapaActual = 0 Then
            Me.lblEtapaActual.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Carga la version del contrato actual seleccionada desde la lista de versiones
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarContratacion()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.Contratacion = vlo_Ws_OT_OrdenesDeTrabajo.OTT_CONTRATACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}",
                    Modelo.V_OTT_CONTRATACIONLST.ID_UBICACION,
                    Me.IdUbicacion,
                    Modelo.V_OTT_CONTRATACIONLST.ID_ORDEN_TRABAJO,
                    Me.IdOrdenTrabajo,
                    Modelo.V_OTT_CONTRATACIONLST.VERSION,
                    Me.ddlVersion.SelectedValue))

            If Me.Contratacion.Existe Then
                Me.ddlViaContrato.SelectedValue = Me.Contratacion.IdViaContrato
            Else
                Me.Contratacion.IdOrdenTrabajo = IdOrdenTrabajo
                Me.Contratacion.IdUbicacion = IdUbicacion
                Me.Contratacion.IdEtapaContratacion = EtapaContratacion.EXPEDIENTE_TECNICO
                Me.Contratacion.Editable = Version.NO_EDITABLE
                Me.Contratacion.Usuario = Me.Usuario.UserName
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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarContrataciones()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_UltimaVersion As DataRow
        Dim vlo_DsDatos As DataSet
        Dim vlo_Version As Integer

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
                String.Format("{0} {1}", Modelo.V_OTT_CONTRATACIONLST.VERSION, Ordenamiento.ASCENDENTE), False, 0, 0)

            Me.Contratacion = New EntOttContratacion

            Me.ddlVersion.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
            For Each vlo_fila As DataRow In vlo_DsDatos.Tables(0).Rows
                vlo_Version = vlo_fila.Item(Modelo.V_OTT_CONTRATACIONLST.VERSION)
                If vlo_fila.Item(Modelo.V_OTT_CONTRATACIONLST.EDITABLE) = Version.EDITABLE Then
                    Me.ddlVersion.Items.Add(New ListItem(vlo_Version, vlo_Version))
                    Me.ddlViaContrato.Enabled = False
                Else
                    Me.ddlVersion.Items.Add(New ListItem(String.Format("{0} (No Editable)", vlo_Version), vlo_Version))
                    Me.ddlViaContrato.Enabled = True
                End If
            Next

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then

                'Se carga la última contratación 
                vlo_UltimaVersion = vlo_DsDatos.Tables(0).Rows(vlo_DsDatos.Tables(0).Rows.Count - 1)
                'Se agrega un identificador de la version en la lista para que seleccione cual desea consultar

                If vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.EDITABLE) = Version.EDITABLE Then
                    Contratacion.Editable = Version.EDITABLE
                    'Mostrar Datos con la fila editable
                Else
                    Contratacion.Editable = Version.NO_EDITABLE
                End If

                'Se cargan los datos del anteproyecto en el objeto en memoria

                Contratacion.IdViaContrato = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.ID_VIA_CONTRATO)
                Contratacion.IdEtapaContratacion = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.ID_ETAPA_CONTRATACION)
                Contratacion.Observaciones = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.OBSERVACIONES)
                Contratacion.IdOrdenTrabajo = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.ID_ORDEN_TRABAJO)
                Contratacion.IdUbicacion = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.ID_UBICACION)
                Contratacion.NombreContrato = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.NOMBRE_CONTRATO)
                Contratacion.NumeroContrato = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.NUMERO_CONTRATO)
                Contratacion.NumeroDecisionInicial = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.NUMERO_DECISION_INICIAL)
                Contratacion.NumeroSolicitud = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.NUMERO_SOLICITUD)
                Contratacion.Version = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.VERSION)
                Contratacion.TimeStamp = vlo_UltimaVersion.Item(Modelo.V_OTT_CONTRATACIONLST.TIME_STAMP)
                Contratacion.Existe = True


                Me.VersionActual = Contratacion.Version
                If OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PENDIENTE_REVISION_CONTRATACIONES Then
                    Me.ddlVersion.Items.Add(New ListItem(Contratacion.Version + 1, Contratacion.Version + 1))
                    Me.ddlVersion.SelectedValue = Contratacion.Version + 1
                Else
                    Me.ddlVersion.SelectedValue = Contratacion.Version
                End If
                If Contratacion.Observaciones <> "-" Then
                    Me.txtObservaciones.Text = Contratacion.Observaciones
                End If

                AsignarEtapaActual()
            Else
                'Si no hay versiones registradas se crea la version uno por default
                Me.Contratacion = New EntOttContratacion
                Contratacion.IdOrdenTrabajo = Me.IdOrdenTrabajo
                Contratacion.IdEtapaContratacion = EtapaContratacion.EXPEDIENTE_TECNICO
                Contratacion.IdUbicacion = Me.IdUbicacion
                Contratacion.Editable = Version.NO_EDITABLE
                Contratacion.Version = 1
                Me.VersionActual = Version.EDITABLE
                Me.ddlVersion.Items.Add(New ListItem("1", "1"))
                Me.ddlVersion.SelectedValue = 1
                Me.EtapaActual = EtapaContratacion.EXPEDIENTE_TECNICO
                Me.ddlViaContrato.Enabled = True

            End If
            AsignarEtapaDescripcion()
            Me.lblEtapaActual.Text = String.Format("Etapa Actual: {0}", Me.EtapaActualDescripcion)
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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/04/2016</creationDate>
    ''' <changeLog>
    ''' Se incluye el campo de observaciones
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>7/7/2016</creationDate>
    ''' </changeLog>
    Private Sub construirRegistro()
        Me.Contratacion.Usuario = Me.Usuario.UserName
        Me.Contratacion.Observaciones = Me.txtObservaciones.Text
        Me.Contratacion.IdViaContrato = Me.ddlViaContrato.SelectedValue
        Me.Contratacion.Editable = Version.EDITABLE
    End Sub

    ''' <summary>
    ''' Inicializa la lista con dias hábiles y dias naturales
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>6/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarListaDias()
        Me.ddlDias.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
        Me.ddlDias.Items.Add(New ListItem("Naturales", Dias.NATURALES))
        Me.ddlDias.Items.Add(New ListItem("Habiles", Dias.HABILES))
    End Sub

    ''' <summary>
    ''' Asigna la descripción de la etapa actual
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>20/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AsignarEtapaDescripcion()
        Select Case Me.EtapaActual

            Case EtapaContratacion.EXPEDIENTE_TECNICO
                Me.EtapaActualDescripcion = "Expediente Técnico"
            Case EtapaContratacion.INICIO
                Me.EtapaActualDescripcion = "Inicio"
                WebUtils.RegistrarScript(Me, "activarInicio", "activarInicio();")
            Case EtapaContratacion.PUBLICACION_CARTEL
                Me.EtapaActualDescripcion = "Publicación del Cartel"
                WebUtils.RegistrarScript(Me, "activarCartel", "activarCartel();")
            Case EtapaContratacion.VISITA_TECNICA
                Me.EtapaActualDescripcion = "Visita Técnica"
            Case EtapaContratacion.ACLARACIONES
                Me.EtapaActualDescripcion = "Aclaraciones"
                WebUtils.RegistrarScript(Me, "activarAclaraciones", "activarAclaraciones();")
            Case EtapaContratacion.OFERTAS
                Me.EtapaActualDescripcion = "Ofertas"
                WebUtils.RegistrarScript(Me, "activarOfertas", "activarOfertas();")
            Case EtapaContratacion.RECOMENDACION_TECNICA
                Me.EtapaActualDescripcion = "Recomendación Técnica"
                WebUtils.RegistrarScript(Me, "activarRecTecnica", "activarRecTecnica();")
            Case EtapaContratacion.ADJUDICACION
                Me.EtapaActualDescripcion = "Adjudicación"
                WebUtils.RegistrarScript(Me, "activarAdjudicacionDocumentos", "activarAdjudicacionDocumentos();")

        End Select
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>13/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function asignarEstadoEtapaContratacion() As String
        Dim vlo_resultado As String
        Select Case Me.ddlEtapaVersion.SelectedValue

            Case EtapaContratacion.EXPEDIENTE_TECNICO
                vlo_resultado = EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE
                Me.Contratacion.IdEtapaContratacion = EtapaContratacion.EXPEDIENTE_TECNICO
            Case EtapaContratacion.INICIO
                vlo_resultado = EstadoOrden.CONTRATACION_INICIO
                Me.Contratacion.IdEtapaContratacion = EtapaContratacion.INICIO
            Case EtapaContratacion.PUBLICACION_CARTEL
                vlo_resultado = EstadoOrden.CONTRATACION_PUBLICACION_CARTEL
                Me.Contratacion.IdEtapaContratacion = EtapaContratacion.PUBLICACION_CARTEL
            Case EtapaContratacion.VISITA_TECNICA
                vlo_resultado = EstadoOrden.CONTRATACION_VISITA_TECNICA
                Me.Contratacion.IdEtapaContratacion = EtapaContratacion.VISITA_TECNICA
            Case EtapaContratacion.ACLARACIONES
                vlo_resultado = EstadoOrden.CONTRATACION_ACLARACIONES
                Me.Contratacion.IdEtapaContratacion = EtapaContratacion.ACLARACIONES
            Case EtapaContratacion.OFERTAS
                vlo_resultado = EstadoOrden.CONTRATACION_OFERTAS
                Me.Contratacion.IdEtapaContratacion = EtapaContratacion.OFERTAS
            Case EtapaContratacion.RECOMENDACION_TECNICA
                vlo_resultado = EstadoOrden.CONTRATACION_RECOMENDACION_TECNICA
                Me.Contratacion.IdEtapaContratacion = EtapaContratacion.RECOMENDACION_TECNICA
            Case EtapaContratacion.ADJUDICACION
                Me.Contratacion.IdEtapaContratacion = EtapaContratacion.ADJUDICACION
                vlo_resultado = EstadoOrden.CONTRATACION_ADJUDICACION
            Case Else
                vlo_resultado = EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE
                Me.Contratacion.IdEtapaContratacion = EtapaContratacion.EXPEDIENTE_TECNICO
                Me.EtapaActual = EtapaContratacion.EXPEDIENTE_TECNICO
                AsignarEtapaDescripcion()
        End Select

        Return vlo_resultado
    End Function

    ''' <summary>
    ''' Asigna la etapa actual basandose en el estado de la orden de trabajo y controla la visualización de los botones
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>20/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AsignarEtapaActual()
        If Me.Contratacion.Editable = Version.EDITABLE Then
            Select Case Me.OrdenTrabajo.EstadoOrdenTrabajo

                Case EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE
                    Me.EtapaActual = EtapaContratacion.EXPEDIENTE_TECNICO
                    Me.DevolverExpediente.Visible = True
                    Me.btnCerrarEtapa.Visible = True
                    btnNuevaVersion.Visible = False
                Case EstadoOrden.CONTRATACION_INICIO
                    Me.EtapaActual = EtapaContratacion.INICIO
                    Me.btnGuardarInicio.Visible = True
                    activarInicio()
                    btnNuevaVersion.Visible = False
                Case EstadoOrden.CONTRATACION_PUBLICACION_CARTEL
                    Me.EtapaActual = EtapaContratacion.PUBLICACION_CARTEL
                    btnGuardarCartel.Visible = True
                    txtNumContrato.Enabled = True
                    txtNombreContrato.Enabled = True
                    txtNombreContrato.Text = OrdenTrabajo.NombreProyecto
                    activarCartel()
                Case EstadoOrden.CONTRATACION_VISITA_TECNICA
                    documentoAMostrar()
                    Me.EtapaActual = EtapaContratacion.VISITA_TECNICA
                Case EstadoOrden.CONTRATACION_ACLARACIONES
                    Me.EtapaActual = EtapaContratacion.ACLARACIONES
                Case EstadoOrden.CONTRATACION_OFERTAS
                    Me.EtapaActual = EtapaContratacion.OFERTAS
                Case EstadoOrden.CONTRATACION_RECOMENDACION_TECNICA
                    documentoRecomendacion()
                    btnAgregarRecomendacion.Visible = True
                    btnCerrarRecomendacion.Visible = True
                    Me.EtapaActual = EtapaContratacion.RECOMENDACION_TECNICA
                Case EstadoOrden.CONTRATACION_ADJUDICACION
                    inicializarDocumentoAdjudicacion()
                    btnAgregarLinea.Visible = True
                    btnCerrarAdjudicacion.Visible = True
                    btnAgregarDocAdjudicacion.Enabled = True
                    btnAgregardjudicacion.Enabled = True
                    Me.EtapaActual = EtapaContratacion.ADJUDICACION

                Case Else
                    inicializarDocumentoAdjudicacion()
                    btnAgregarLinea.Visible = True
                    btnCerrarAdjudicacion.Visible = True
                    btnAgregarDocAdjudicacion.Enabled = True
                    btnAgregardjudicacion.Enabled = True
                    Me.EtapaActual = EtapaContratacion.ADJUDICACION

            End Select
        Else
            Me.DevolverExpediente.Visible = False
            Me.btnCerrarEtapa.Visible = False
            Me.btnGuardarInicio.Visible = False
            btnGuardarCartel.Visible = False
            btnAgregarRecomendacion.Visible = False
            btnCerrarRecomendacion.Visible = False
            btnAgregarLinea.Visible = False
            btnCerrarAdjudicacion.Visible = False
            btnAgregarDocAdjudicacion.Enabled = False
            btnAgregardjudicacion.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Asigna la etapa actual en modo consulta basandose en el estado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>5/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AsignarEtapaActualConsulta()
        Select Case Me.OrdenTrabajo.EstadoOrdenTrabajo


            Case EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE
                Me.EtapaActual = EtapaContratacion.EXPEDIENTE_TECNICO
                Me.DevolverExpediente.Visible = True
                Me.btnCerrarEtapa.Visible = True
            Case EstadoOrden.CONTRATACION_INICIO
                Me.EtapaActual = EtapaContratacion.INICIO
                Me.btnGuardarInicio.Visible = True
                activarInicio()
            Case EstadoOrden.CONTRATACION_PUBLICACION_CARTEL
                Me.EtapaActual = EtapaContratacion.PUBLICACION_CARTEL
                btnGuardarCartel.Visible = True
                activarCartel()
            Case EstadoOrden.CONTRATACION_VISITA_TECNICA
                documentoAMostrar()
                Me.EtapaActual = EtapaContratacion.VISITA_TECNICA
            Case EstadoOrden.CONTRATACION_ACLARACIONES
                Me.EtapaActual = EtapaContratacion.ACLARACIONES
            Case EstadoOrden.CONTRATACION_OFERTAS
                Me.EtapaActual = EtapaContratacion.OFERTAS
            Case EstadoOrden.CONTRATACION_RECOMENDACION_TECNICA
                documentoRecomendacion()
                btnAgregarRecomendacion.Visible = True
                btnCerrarRecomendacion.Visible = True
                Me.EtapaActual = EtapaContratacion.RECOMENDACION_TECNICA
            Case EstadoOrden.CONTRATACION_ADJUDICACION
                inicializarDocumentoAdjudicacion()
                btnAgregarLinea.Visible = True
                btnCerrarAdjudicacion.Visible = True
                btnAgregarDocAdjudicacion.Enabled = True
                btnAgregardjudicacion.Enabled = True
                Me.EtapaActual = EtapaContratacion.ADJUDICACION


        End Select
    End Sub

    ''' <summary>
    ''' Muestra los tabs deacuerdo a la via de contratacion calculada
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>18/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub mostrarTabs()
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.liInicio.Visible = False
            Me.liPublicacionCartel.Visible = False
            Me.liAclaraciones.Visible = False
            Me.liOfertas.Visible = False
            Me.liRecomendaciónTécnica.Visible = False
            Me.liAdjudicación.Visible = False

            Me.ddlEtapaVersion.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, 0))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_ETAPA_VIA_CONTRATO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OTM_ETAPA_VIA_CONTRATOLST.ID_VIA_CONTRATO, Me.ddlViaContrato.SelectedValue,
                              Modelo.V_OTM_ETAPA_VIA_CONTRATOLST.ESTADO, Estado.ACTIVO), String.Empty, False, 0, 0)

            For Each vlo_fila As Data.DataRow In vlo_DsDatos.Tables(0).Rows
                'Se inserta un registro en la lista de etapas para generar una versión apartir de una etapa
                Me.ddlEtapaVersion.Items.Add(New ListItem(vlo_fila(Modelo.V_OTM_ETAPA_VIA_CONTRATOLST.DESC_ETAPA_CONTRAT), vlo_fila(Modelo.V_OTM_ETAPA_VIA_CONTRATOLST.ID_ETAPA_CONTRATACION)))

                Select Case vlo_fila(Modelo.V_OTM_ETAPA_VIA_CONTRATOLST.ID_ETAPA_CONTRATACION)
                    Case EtapaContratacion.EXPEDIENTE_TECNICO
                        Me.liExpediente.Visible = True
                        inicializarControlRevisionExp()
                    Case EtapaContratacion.INICIO
                        Me.liInicio.Visible = True
                    Case EtapaContratacion.PUBLICACION_CARTEL
                        Me.ArchivoCartel = New EntOttAdjuntoOrdenTrabajo
                        Me.ArchivoCartel.IdTipoDocumento = TipoDocumento.CARTEL
                        Me.liPublicacionCartel.Visible = True
                    Case EtapaContratacion.ACLARACIONES
                        Me.liAclaraciones.Visible = True
                    Case EtapaContratacion.OFERTAS
                        Me.liOfertas.Visible = True
                    Case EtapaContratacion.RECOMENDACION_TECNICA
                        Me.ArchivoRecomendacion = New EntOttAdjuntoOrdenTrabajo
                        Me.ArchivoRecomendacion.IdTipoDocumento = TipoDocumento.OFICIO
                        Me.liRecomendaciónTécnica.Visible = True
                    Case EtapaContratacion.ADJUDICACION
                        Me.ArchivoLinea = New EntOttAdjuntoOrdenTrabajo
                        Me.ArchivoLinea.IdTipoDocumento = TipoDocumento.OFICIO
                        Me.liAdjudicación.Visible = True
                End Select

                If vlo_fila(Modelo.OTM_ETAPA_VIA_CONTRATO.ID_ETAPA_CONTRATACION) = Me.EtapaActual Then
                    Exit For
                End If

            Next

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Muestra los tabs deacuerdo a la via de contratacion calculada
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>5/5/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub mostrarTabsConsulta()
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.liInicio.Visible = True
            Me.liPublicacionCartel.Visible = True
            Me.liAclaraciones.Visible = True
            Me.liOfertas.Visible = True
            Me.liRecomendaciónTécnica.Visible = True
            Me.liAdjudicación.Visible = True

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_ETAPA_VIA_CONTRATO_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_ETAPA_VIA_CONTRATO.ID_VIA_CONTRATO, Me.ddlViaContrato.SelectedValue,
                              Modelo.OTM_ETAPA_VIA_CONTRATO.ESTADO, Estado.ACTIVO),
                String.Empty, False, 0, 0)

            For Each vlo_fila As Data.DataRow In vlo_DsDatos.Tables(0).Rows

                Select Case vlo_fila(Modelo.OTM_ETAPA_VIA_CONTRATO.ID_ETAPA_CONTRATACION)

                    Case EtapaContratacion.EXPEDIENTE_TECNICO
                        inicializarControlRevisionExp()
                        DevolverExpediente.Visible = False
                        btnCerrarEtapa.Visible = False
                    Case EtapaContratacion.INICIO
                        btnAgregarDocumento.Visible = False
                        btnGuardarInicio.Visible = False
                    Case EtapaContratacion.PUBLICACION_CARTEL
                        btnGuardarCartel.Visible = False
                    Case EtapaContratacion.ACLARACIONES
                    Case EtapaContratacion.OFERTAS
                    Case EtapaContratacion.RECOMENDACION_TECNICA
                        btnAgregarRecomendacion.Visible = False
                        btnEliminarRecomendacion.Visible = False
                        btnCerrarRecomendacion.Visible = False
                    Case EtapaContratacion.ADJUDICACION
                End Select

                If vlo_fila(Modelo.OTM_ETAPA_VIA_CONTRATO.ID_ETAPA_CONTRATACION) = Me.EtapaActual Then
                    Exit For
                End If

            Next

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Oculta los tabs de la lista
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ocultarTabs()
        Me.liExpediente.Visible = False
        Me.liInicio.Visible = False
        Me.liPublicacionCartel.Visible = False
        Me.liAclaraciones.Visible = False
        Me.liOfertas.Visible = False
        Me.liRecomendaciónTécnica.Visible = False
        Me.liAdjudicación.Visible = False
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>18/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarControlRevisionExp()
        CargarOrdenTrabajoMadre(Me.IdUbicacion, Me.IdOrdenTrabajo)
        Me.ContadorTaps = 0
        CargarTapsEtapas()
    End Sub

    ''' <summary>
    ''' Agrega un nuevo adjunto al dataset 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>28/04/2016</creationDate>
    ''' <changeLog></changeLog>
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
            vlo_NuevoArchivo.Descripcion = "Oficio de la etapa de inicio de la contratación."
            vlo_NuevoArchivo.Usuario = Me.Usuario.UserName
            vlo_NuevoArchivo.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.CONTRATACIONES
            vlo_NuevoArchivo.IdTipoDocumento = TipoDocumento.OFICIO

            vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_NuevoArchivo)

            If vln_Resultado > 0 Then
                vlo_EntOttDocumentoContratacion = New Wsr_OT_OrdenesDeTrabajo.EntOttDocumentoContratacion

                vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo = vln_Resultado
                vlo_EntOttDocumentoContratacion.IdEtapaContratacion = EtapaContratacion.INICIO
                vlo_EntOttDocumentoContratacion.IdEtapaOrdenTrabajo = EtapasOrdenTrabajo.CONTRATACIONES
                vlo_EntOttDocumentoContratacion.IdOrdenTrabajo = IdOrdenTrabajo
                vlo_EntOttDocumentoContratacion.IdTipoDocumento = TipoDocumento.OFICIO
                vlo_EntOttDocumentoContratacion.IdUbicacion = IdUbicacion
                vlo_EntOttDocumentoContratacion.DocumentoTramitado = Documento.NO_TRAMITADO
                vlo_EntOttDocumentoContratacion.Usuario = Me.Usuario.UserName
                vlo_EntOttDocumentoContratacion.Version = Contratacion.Version
                vlo_EntOttDocumentoContratacion.FechaHoraRegistro = Now

                vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_InsertarRegistro(
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                    vlo_EntOttDocumentoContratacion)

            End If

            If vln_Resultado > 0 Then

                ConfigurarTabInicio()

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
    ''' Agrega archivos al listado de documentos de adjudicacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregarArchivoAdjudicacion()
        Dim vln_Resultado As Integer
        Dim vlo_NuevoArchivo As Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoOrdenTrabajo
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDocumentoContratacion As Wsr_OT_OrdenesDeTrabajo.EntOttDocumentoContratacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_NuevoArchivo = New Wsr_OT_OrdenesDeTrabajo.EntOttAdjuntoOrdenTrabajo

            vlo_NuevoArchivo.NombreArchivo = Me.fuDocumentosAdjudicacion.FileName
            vlo_NuevoArchivo.Archivo = Me.fuDocumentosAdjudicacion.FileBytes
            vlo_NuevoArchivo.IdUbicacion = IdUbicacion
            vlo_NuevoArchivo.IdOrdenTrabajo = IdOrdenTrabajo
            vlo_NuevoArchivo.Descripcion = "Documento de Adjudicación."
            vlo_NuevoArchivo.Usuario = Me.Usuario.UserName
            vlo_NuevoArchivo.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.CONTRATACIONES
            vlo_NuevoArchivo.IdTipoDocumento = TipoDocumento.OFICIO

            vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_NuevoArchivo)

            If vln_Resultado > 0 Then
                vlo_EntOttDocumentoContratacion = New Wsr_OT_OrdenesDeTrabajo.EntOttDocumentoContratacion

                vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo = vln_Resultado
                vlo_EntOttDocumentoContratacion.IdEtapaContratacion = EtapaContratacion.ADJUDICACION
                vlo_EntOttDocumentoContratacion.IdEtapaOrdenTrabajo = EtapasOrdenTrabajo.CONTRATACIONES
                vlo_EntOttDocumentoContratacion.IdOrdenTrabajo = IdOrdenTrabajo
                vlo_EntOttDocumentoContratacion.IdTipoDocumento = TipoDocumento.OFICIO
                vlo_EntOttDocumentoContratacion.IdUbicacion = IdUbicacion
                vlo_EntOttDocumentoContratacion.DocumentoTramitado = Documento.NO_TRAMITADO
                vlo_EntOttDocumentoContratacion.Usuario = Me.Usuario.UserName
                vlo_EntOttDocumentoContratacion.Version = Contratacion.Version
                vlo_EntOttDocumentoContratacion.FechaHoraRegistro = Now

                vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_InsertarRegistro(
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                    vlo_EntOttDocumentoContratacion)

            End If

            If vln_Resultado > 0 Then

                ConfigurarTabAdjudicacion()

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
    ''' Carga el archivo de la linea de contratación específica
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>9/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub cargarArchivoLinea(pvn_NumLinea As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_DataSet As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DataSet = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}",
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION, IdUbicacion,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO, IdOrdenTrabajo,
                              Modelo.OTT_DOCUMENTO_CONTRATACION.NUMERO_LINEA, pvn_NumLinea), String.Empty, False, 0, 0)

            If vlo_DataSet.Tables.Count > 0 AndAlso vlo_DataSet.Tables(0).Rows.Count > 0 Then
                Me.fuDocAdjudicacion.Visible = False
                Me.btnAgregarDocAdjudicacion.Visible = False
                Me.lnkAdjudicacion.Text = vlo_DataSet.Tables(0).Rows(0).Item(Modelo.V_OTT_DOCUMENTO_CONTRATLST.NOMBRE_ARCHIVO).ToString
                cargarArchivoLinea(CInt(vlo_DataSet.Tables(0).Rows(0).Item(Modelo.V_OTT_DOCUMENTO_CONTRATLST.ID_ADJUNTO_ORDEN_TRABAJO)))
                btnEliminarArchivoLinea.Visible = True
                lnkAdjudicacion.Visible = True
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
    ''' Carga el archivo de la linea de contratación específica
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>9/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub cargarArchivoLinea(pvn_idAdjunto As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ArchivoLinea = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerRegistro(
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
    End Sub

    ''' <summary>
    ''' Carga la lista de lineas de adjudicacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub cargarLineasAdjudicacion()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.DsLineas = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_ADJUDICACION_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}",
                Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION,
                IdUbicacion,
                Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO,
                IdOrdenTrabajo,
                Modelo.OTT_DOCUMENTO_CONTRATACION.VERSION,
                Contratacion.Version), String.Format("{0} ASC", Modelo.OTT_LINEA_ADJUDICACION.NUMERO_LINEA), False, 0, 0)

            If DsLineas IsNot Nothing AndAlso DsLineas.Tables(0).Rows.Count > 0 Then
                Me.rpLineas.DataSource = DsLineas
                Me.rpLineas.DataMember = DsLineas.Tables(0).TableName
                Me.rpLineas.DataBind()

            Else
                With Me.rpLineas
                    .DataSource = Nothing
                    .DataBind()
                End With
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Retorna la cantida de ajunto que posee una orden especifica para una etapa determinada
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <param name="pvn_IdEtapaOrdenTrabajo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Function CargarCantidadAdjuntosOrdenTrabajoaEtapa(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_IdEtapaOrdenTrabajo As Integer) As Integer
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return CType(vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ObtenerFnOtCantidadAdjuntosEtapa(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        pvn_IdUbicacion, pvc_IdOrdenTrabajo, pvn_IdEtapaOrdenTrabajo), Integer)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Genera una nueva versión del anteproyecto copiando todos los elementos de la ultima versión no editable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function GenerarNuevaVersion() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_EstadoOrden As String

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            construirRegistro()

            vlc_EstadoOrden = asignarEstadoEtapaContratacion()
            If vlc_EstadoOrden Is Nothing Then
                vlc_EstadoOrden = EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE
            End If
            Me.Contratacion.Usuario = Me.Usuario.UserName
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_CONTRATACION_NuevaVersion(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.Contratacion, vlc_EstadoOrden, Me.EtapaActual, Me.EtapaActualDescripcion, Me.Usuario.NumEmpleado) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Ejecuta la funcion de delvover el expediente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>19/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Devolver() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.Contratacion.Usuario = Me.Usuario.UserName

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_CONTRATACION_DevolverExpediente(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.OrdenTrabajo, Me.Contratacion, Me.Usuario.NumEmpleado, Me.txtMotivo.Text) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Cierra la etapa y hace que se pase a la siguiente etapa de la contratacion correspondiente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>19/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CerrarEtapa() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_CONTRATACION_CerrarEtapaExpedienteTecnico(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.OrdenTrabajo, Me.Contratacion, Me.Usuario.NumEmpleado, Me.EtapaActual) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Cierra la etapa y hace que se pase a la siguiente etapa de la contratacion correspondiente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>19/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function GuardarInicio() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.DsArchivosInicio.Tables(0).Rows.Count > 0 Then
                Me.Contratacion.NumeroSolicitud = Me.txtNSolicitud.Text.Trim
                Me.Contratacion.NumeroDecisionInicial = Me.txtDescicionInicial.Text.Trim
                Me.Contratacion.Usuario = Me.Usuario.UserName

                Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_CONTRATACION_GuardarYCerrarEtapaInicio(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    Me.Contratacion, Me.OrdenTrabajo, Me.Usuario.NumEmpleado, Me.EtapaActual) > 0
            Else
                MostrarAlertaError("La lista de archivos está vacia, por favor adjunte documentos.")
            End If
            Return False
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Cierra la etapa y hace que se pase a la siguiente etapa de la contratacion correspondiente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function GuardarCartel() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.ArchivoCartel.Archivo IsNot Nothing Then
                ArchivoCartel.IdOrdenTrabajo = IdOrdenTrabajo
                ArchivoCartel.IdUbicacion = IdUbicacion
                ArchivoCartel.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.CONTRATACIONES
                ArchivoCartel.Descripcion = "Cartel de la contratación"
                ArchivoCartel.Usuario = Me.Usuario.UserName

                Me.Contratacion.NumeroContrato = Me.txtNumContrato.Text.Trim
                Me.Contratacion.NombreContrato = Me.txtNombreContrato.Text.Trim
                Me.Contratacion.Usuario = Me.Usuario.UserName

                Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_CONTRATACION_GuardarYCerrarEtapaCartel(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    Me.ArchivoCartel, Me.Contratacion, Me.OrdenTrabajo, Me.Usuario.NumEmpleado, Me.EtapaActual) > 0
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

    ''' <summary>
    ''' Agrega una linea al contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function agregarLinea() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttLineaAdjudicacion As EntOttLineaAdjudicacion
        Dim vln_Resultado As Integer

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.ArchivoLinea.Archivo IsNot Nothing Then
                vlo_EntOttLineaAdjudicacion = New EntOttLineaAdjudicacion
                vlo_EntOttLineaAdjudicacion.IdOrdenTrabajo = IdOrdenTrabajo
                vlo_EntOttLineaAdjudicacion.IdUbicacion = IdUbicacion
                vlo_EntOttLineaAdjudicacion.Adjudicatario = Me.txtAdjudicatario.Text
                vlo_EntOttLineaAdjudicacion.FechaFinEstimada = CDate(Me.lblFinEstimada.Text)
                vlo_EntOttLineaAdjudicacion.FechaInicioObra = CDate(Me.txtFechaInicio.Text)
                vlo_EntOttLineaAdjudicacion.FormaCalculoDias = IIf(Me.ddlDias.SelectedValue = Dias.HABILES, FormaDias.HABILES, FormaDias.NATURALES)
                vlo_EntOttLineaAdjudicacion.MontoAdjudicado = Me.txtMonto.Text
                vlo_EntOttLineaAdjudicacion.NumeroLinea = Me.txtNumLinea.Text
                vlo_EntOttLineaAdjudicacion.PlazoEnDias = Me.txtPlazo.Text
                vlo_EntOttLineaAdjudicacion.Usuario = Usuario.UserName
                vlo_EntOttLineaAdjudicacion.Version = Contratacion.Version

                ArchivoLinea.IdUbicacion = IdUbicacion
                ArchivoLinea.IdOrdenTrabajo = IdOrdenTrabajo
                ArchivoLinea.Descripcion = "Documento de Adjudicación."
                ArchivoLinea.Usuario = Me.Usuario.UserName
                ArchivoLinea.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.CONTRATACIONES
                ArchivoLinea.IdTipoDocumento = TipoDocumento.OFICIO

                vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_ADJUDICACION_InsertarRegistro(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        vlo_EntOttLineaAdjudicacion, ArchivoLinea)

            Else
                MostrarAlertaError("Debe ingresar un documento para el inicio de la obra.")
            End If

            cargarLineasAdjudicacion()

            Return vln_Resultado > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Modifica una linea al contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>9/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function modificarLinea() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttLineaAdjudicacion As EntOttLineaAdjudicacion
        Dim vln_Resultado As Integer

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.ArchivoLinea.Archivo IsNot Nothing Then
                vlo_EntOttLineaAdjudicacion = New EntOttLineaAdjudicacion
                vlo_EntOttLineaAdjudicacion.IdOrdenTrabajo = IdOrdenTrabajo
                vlo_EntOttLineaAdjudicacion.IdUbicacion = IdUbicacion
                vlo_EntOttLineaAdjudicacion.Adjudicatario = Me.txtAdjudicatario.Text
                vlo_EntOttLineaAdjudicacion.FechaFinEstimada = CDate(Me.lblFinEstimada.Text)
                vlo_EntOttLineaAdjudicacion.FechaInicioObra = CDate(Me.txtFechaInicio.Text)
                vlo_EntOttLineaAdjudicacion.FormaCalculoDias = IIf(Me.ddlDias.SelectedValue = Dias.HABILES, FormaDias.HABILES, FormaDias.NATURALES)
                vlo_EntOttLineaAdjudicacion.MontoAdjudicado = Me.txtMonto.Text
                vlo_EntOttLineaAdjudicacion.NumeroLinea = Me.txtNumLinea.Text
                vlo_EntOttLineaAdjudicacion.PlazoEnDias = Me.txtPlazo.Text
                vlo_EntOttLineaAdjudicacion.Usuario = Usuario.UserName
                vlo_EntOttLineaAdjudicacion.Version = Contratacion.Version

                ArchivoLinea.IdUbicacion = IdUbicacion
                ArchivoLinea.IdOrdenTrabajo = IdOrdenTrabajo
                ArchivoLinea.Descripcion = "Documento de Adjudicación."
                ArchivoLinea.Usuario = Me.Usuario.UserName
                ArchivoLinea.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.CONTRATACIONES
                ArchivoLinea.IdTipoDocumento = TipoDocumento.OFICIO

                vln_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_ADJUDICACION_ModificarRegistro(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        vlo_EntOttLineaAdjudicacion, ArchivoLinea)

            Else
                MostrarAlertaError("Debe ingresar un documento para el inicio de la obra.")

            End If

            cargarLineasAdjudicacion()


            activarAdjudicacionLineas()

            Return vln_Resultado > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Cierra la etapa de adjudicacion y cambia la OT a estado supervision de obra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function GuardarAdjudicacion() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttLineaAdjudicacion As EntOttLineaAdjudicacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.ArchivoLinea.Archivo IsNot Nothing Then

                vlo_EntOttLineaAdjudicacion = New EntOttLineaAdjudicacion
                vlo_EntOttLineaAdjudicacion.IdOrdenTrabajo = IdOrdenTrabajo
                vlo_EntOttLineaAdjudicacion.IdUbicacion = IdUbicacion
                vlo_EntOttLineaAdjudicacion.Adjudicatario = Me.txtAdjudicatario.Text
                vlo_EntOttLineaAdjudicacion.FechaFinEstimada = CDate(Me.lblFinEstimada.Text)
                vlo_EntOttLineaAdjudicacion.FechaInicioObra = CDate(Me.txtFechaInicio.Text)
                vlo_EntOttLineaAdjudicacion.FormaCalculoDias = IIf(Me.ddlDias.SelectedValue = Dias.HABILES, FormaDias.HABILES, FormaDias.NATURALES)
                vlo_EntOttLineaAdjudicacion.MontoAdjudicado = Me.txtMonto.Text
                vlo_EntOttLineaAdjudicacion.NumeroLinea = Me.txtNumLinea.Text
                vlo_EntOttLineaAdjudicacion.PlazoEnDias = Me.txtPlazo.Text
                vlo_EntOttLineaAdjudicacion.Usuario = Usuario.UserName
                vlo_EntOttLineaAdjudicacion.Version = Contratacion.Version


                ArchivoLinea.IdOrdenTrabajo = IdOrdenTrabajo
                ArchivoLinea.IdUbicacion = IdUbicacion
                ArchivoLinea.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.CONTRATACIONES
                ArchivoLinea.Descripcion = "Documento de inicio de obra."
                ArchivoLinea.Usuario = Me.Usuario.UserName

                Me.Contratacion.NumeroContrato = Me.txtNumContrato.Text.Trim
                Me.Contratacion.NombreContrato = Me.txtNombreContrato.Text.Trim
                Me.Contratacion.Usuario = Me.Usuario.UserName

                Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_ADJUDICACION_InsertarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOttLineaAdjudicacion, Me.ArchivoLinea) > 0
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

    ''' <summary>
    ''' Cierra la etapa de adjudicacion y de esta contratación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CerrarEtapaAdjudicacion() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.DsLineas.Tables(0).Rows.Count > 0 Then
                If Me.DsArchivosAdjudicacion.Tables(0).Rows.Count > 0 Then
                    Me.OrdenTrabajo.Usuario = Me.Usuario.UserName

                    Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_CONTRATACION_GuardarAdjudicacion(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        Me.OrdenTrabajo, Me.Usuario.NumEmpleado) > 0
                Else
                    MostrarAlertaError("Debe agregarse al menos un documento en la viñeta de documentos para cerrar la etapa.")
                End If
                MostrarAlertaError("Debe ingresar al menos una linea de contratación.")
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
    ''' Elimina el elemento de la lista de lineas de contratacion
    ''' </summary>
    ''' <param name="pvn_NumeroLinea"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>9/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function BorrarLinea(pvn_NumeroLinea As Integer) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttLineaAdjudicacion As EntOttLineaAdjudicacion
        Dim vlo_EntOttDocumentoContratacion As EntOttDocumentoContratacion


        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttLineaAdjudicacion = New EntOttLineaAdjudicacion
            vlo_EntOttLineaAdjudicacion.Version = Contratacion.Version
            vlo_EntOttLineaAdjudicacion.IdUbicacion = IdUbicacion
            vlo_EntOttLineaAdjudicacion.IdOrdenTrabajo = IdOrdenTrabajo
            vlo_EntOttLineaAdjudicacion.NumeroLinea = pvn_NumeroLinea

            cargarArchivoLinea(vlo_EntOttLineaAdjudicacion.NumeroLinea.ToString)



            vlo_EntOttDocumentoContratacion = New EntOttDocumentoContratacion

            vlo_EntOttDocumentoContratacion.IdOrdenTrabajo = IdOrdenTrabajo
            vlo_EntOttDocumentoContratacion.IdUbicacion = IdUbicacion
            vlo_EntOttDocumentoContratacion.Version = Contratacion.Version
            vlo_EntOttDocumentoContratacion.IdTipoDocumento = Me.ArchivoLinea.IdTipoDocumento
            vlo_EntOttDocumentoContratacion.IdEtapaOrdenTrabajo = EtapasOrdenTrabajo.CONTRATACIONES
            vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo = Me.ArchivoLinea.IdAdjuntoOrdenTrabajo

            vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_CONTRATACION_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttDocumentoContratacion)

            vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.ArchivoLinea)

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_ADJUDICACION_BorrarRegistro(
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                    vlo_EntOttLineaAdjudicacion) > 0

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
    ''' <creationDate>28/04/2016</creationDate>
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
            vlo_EntOttDocumentoContratacion.Version = Contratacion.Version
            vlo_EntOttDocumentoContratacion.IdUbicacion = IdUbicacion
            vlo_EntOttDocumentoContratacion.IdOrdenTrabajo = IdOrdenTrabajo
            vlo_EntOttDocumentoContratacion.IdTipoDocumento = TipoDocumento.OFICIO
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
    ''' Cierra la etapa de recomendacion tecnica
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function GuardarRecomendacion() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If chkContratacionInfructuosa.Checked Then
                Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_CONTRATACION_GuardarYCerrarEtapaRecomendacion(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    Me.ArchivoRecomendacion, IdOrdenTrabajo, IdUbicacion, Me.Usuario.NumEmpleado, Me.EtapaActual,
                    Me.chkContratacionInfructuosa.Checked, Contratacion.Version) > 0

                Return True

            Else

                If Me.ArchivoRecomendacion.Archivo IsNot Nothing Then
                    ArchivoRecomendacion.IdOrdenTrabajo = IdOrdenTrabajo
                    ArchivoRecomendacion.IdUbicacion = IdUbicacion
                    ArchivoRecomendacion.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.CONTRATACIONES
                    ArchivoRecomendacion.Descripcion = "Oficio de recomendación técnica final."
                    ArchivoRecomendacion.Usuario = Me.Usuario.UserName

                    Me.Contratacion.Usuario = Me.Usuario.UserName

                    Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_CONTRATACION_GuardarYCerrarEtapaRecomendacion(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        Me.ArchivoRecomendacion, IdOrdenTrabajo, IdUbicacion, Me.Usuario.NumEmpleado, Me.EtapaActual,
                        Me.chkContratacionInfructuosa.Checked, Contratacion.Version) > 0
                Else
                    MostrarAlertaError("El archivo está vacio, por favor ingrese un archivo.")
                End If
            End If
            Return False
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
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
    ''' <creationDate>5/5/2016</creationDate>
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

#End Region

End Class
