Imports Wsr_OT_OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo
Imports Utilerias.GeneradorDeReportes
Imports System.Data

Partial Class OrdenesDeTrabajo_Frm_OT_Anteproyecto
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
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
    ''' Marca cuando es editable o no
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>18/02/2016</creationDate>
    Public Property Editable As Boolean
        Get
            If ViewState("Editable") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("Editable"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("Editable") = value
        End Set
    End Property

    ''' <summary>
    ''' manejo del evento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/02/2016</creationDate>
    Public Property ManejoEventoCheck As Boolean
        Get
            If ViewState("ManejoEventoCheck") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("ManejoEventoCheck"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("ManejoEventoCheck") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
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
    ''' entidad para el anteproyecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/02/2016</creationDate>
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
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/02/2016</creationDate>
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
    ''' Almacena la última version del anteproyecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/02/2016</creationDate>
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
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/02/2016</creationDate>
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
    ''' <creationDate>29/02/2016</creationDate>
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
    ''' <creationDate>29/02/2016</creationDate>
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
    ''' Almacena el profesional encargado de la orden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>02/03/2016</creationDate>
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
    ''' Extensiones para archivo de aval foresta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>02/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ExtensionesPermitidasForesta As String
        Get
            Return CType(ViewState("ExtensionesPermitidasForesta"), String)
        End Get
        Set(value As String)
            ViewState("ExtensionesPermitidasForesta") = value
        End Set
    End Property

    ''' <summary>
    ''' Tamaño maximo del archivo para aval foresta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>02/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property TamanoArchivoForesta As Integer
        Get
            Return CType(ViewState("TamanoArchivoForesta"), Integer)
        End Get
        Set(value As Integer)
            ViewState("TamanoArchivoForesta") = value
        End Set
    End Property

    ''' <summary>
    ''' Extensiones para archivo aval planta fisica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>02/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ExtensionesPermitidasPlanta As String
        Get
            Return CType(ViewState("ExtensionesPermitidasPlanta"), String)
        End Get
        Set(value As String)
            ViewState("ExtensionesPermitidasPlanta") = value
        End Set
    End Property

    ''' <summary>
    ''' Tamaño maximo del archivo para aval planta fisica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>02/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property TamanoArchivoPlanta As Integer
        Get
            Return CType(ViewState("TamanoArchivoPlanta"), Integer)
        End Get
        Set(value As Integer)
            ViewState("TamanoArchivoPlanta") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de archivos adjuntos que se mostrará al usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>02/03/2016</creationDate>
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
    ''' Propiedad para el dataset de archivos adjuntos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>02/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsActividades As Data.DataTable
        Get
            Return CType(ViewState("DsActividades"), Data.DataTable)
        End Get
        Set(value As Data.DataTable)
            ViewState("DsActividades") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de tipos de documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
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

    ''' <summary>
    ''' Extensiones para archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>4/03/2016</creationDate>
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
    ''' Variable global para actividades a modificar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>4/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property Actividad As String
        Get
            Return CType(ViewState("Actividad"), String)
        End Get
        Set(value As String)
            ViewState("Actividad") = value
        End Set
    End Property

    ''' <summary>
    ''' Tamaño maximo para el archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>4/03/2016</creationDate>
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
    ''' Tamaño maximo de caracteres para las actividades
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>28/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ActividadCaracteresRestantes As Integer
        Get
            Return CType(ViewState("ActividadCaracteresRestantes"), Integer)
        End Get
        Set(value As Integer)
            ViewState("ActividadCaracteresRestantes") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el archivo de planta fisica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>09/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property ArchivoPlantaFisica As EntOttAdjuntoOrdenTrabajo
        Get
            Return CType(ViewState("ArchivoPlantaFisica"), EntOttAdjuntoOrdenTrabajo)
        End Get
        Set(value As EntOttAdjuntoOrdenTrabajo)
            ViewState("ArchivoPlantaFisica") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la propiedad para el archivo foresta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>09/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property ArchivoForesta As EntOttAdjuntoOrdenTrabajo
        Get
            Return CType(ViewState("ArchivoForesta"), EntOttAdjuntoOrdenTrabajo)
        End Get
        Set(value As EntOttAdjuntoOrdenTrabajo)
            ViewState("ArchivoForesta") = value
        End Set
    End Property


    ''' <summary>
    ''' Prpoiedad para cargar datos del empleado en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/03/32016</creationDate>
    ''' <changeLog></changeLog>
    Private Property EmpleadoEjecuta As WsrEU_Curriculo.EntEmpleados
        Get
            Return CType(ViewState("EmpleadoEjecuta"), WsrEU_Curriculo.EntEmpleados)
        End Get
        Set(value As WsrEU_Curriculo.EntEmpleados)
            ViewState("EmpleadoEjecuta") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena cuando se efectuan cambios o no
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>28/03/2016</creationDate>
    Public Property BanderaCambios As Boolean
        Get
            If ViewState("BanderaCambios") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("BanderaCambios"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("BanderaCambios") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta al cargar la página
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/02/2016</creationDate>
    ''' <changeLog></changeLog>
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
    ''' Genera la nueva versión editable del ante proyecto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/02/2016</creationDate>
    ''' <changeLog>Carlos Gómez  -- modificacion total de comportamiento</changeLog>
    Protected Sub btnNuevaVersion_Click(sender As Object, e As EventArgs)
        Try
            If Not Me.Anteproyecto.Editable Then
                If GenerarNuevaVersion() Then
                    WebUtils.RegistrarScript(Me.Page, "mostrarAlertaNuevaVersion", "mostrarAlertaNuevaVersion();")
                    Me.btnGuardar.Visible = True
                    Me.btnGuardarYEnviar.Visible = True
                    Me.btnGuardar.Enabled = True
                    btnGuardarYEnviar.Enabled = True
                    btnNuevaVersion.Visible = False
                    Me.txtDescripcion.Text = Me.Anteproyecto.Descripcion
                    Me.txtCantidad.Text = Me.Anteproyecto.Cantidad
                    Me.ddlUnidadMedida.SelectedValue = Me.Anteproyecto.UnidadMedida
                    Me.txtTiempoRespuesta.Text = Me.Anteproyecto.TiempoRespuesta
                    Me.ddlUnidadTiempoRespuesta.SelectedValue = Me.Anteproyecto.IdUnidadTiempo
                    ActualizarListadoVersionesAnteproyecto()
                End If
            Else
                MostrarAlertaError("No se puede crear una nueva versión del anteproyecto hasta que la versión actual sea no editable")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Agrega la actividad al repeater
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>02/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarActividad_Click(sender As Object, e As EventArgs)
        Try
            AgregaActividadesDataTable()
            activarContempladas()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Ademas de mostrar/ocultar el campo para incluir un archivo Activa/desactiva el validador del respectivo control y actualiza el update panel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>02/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub chkPlantaFisica_CheckedChanged(sender As Object, e As EventArgs) Handles chkPlantaFisica.CheckedChanged
        Try
            If Me.ManejoEventoCheck Then
                If Me.chkPlantaFisica.Checked Then
                    Me.fuPlantaFisica.Visible = True
                    Me.imgExtensionesPlanta.Visible = True
                    Me.rfvFuPlantaFisica.Enabled = True
                    Me.btnAgregarPlanta.Visible = True

                Else
                    Me.rfvFuPlantaFisica.Enabled = False
                    Me.imgExtensionesPlanta.Visible = False
                    btnAgregarPlanta.Visible = False
                    Me.ArchivoPlantaFisica.Archivo = Nothing
                    Me.ArchivoPlantaFisica.NombreArchivo = String.Empty
                    Me.fuPlantaFisica.Visible = False
                    Me.lnkArchivoPlantaFisica.Visible = False
                    Me.btnEliminarArchivoPlanta.Visible = False
                    btnAgregarPlanta.Visible = False

                End If
                BanderaCambios = True
                WebUtils.RegistrarScript(Me.Page, "regresarNormal", "regresarNormal();")
            End If

            Me.ManejoEventoCheck = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        activarContempladas()
    End Sub

    ''' <summary>
    ''' Ademas de mostrar/ocultar el campo para incluir un archivo Activa/desactiva el validador del respectivo control y actualiza el update panel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>02/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub chkForesta_CheckedChanged(sender As Object, e As EventArgs) Handles chkForesta.CheckedChanged
        Try

            If Me.ManejoEventoCheck Then

                If Me.chkForesta.Checked Then
                    Me.fuForesta.Visible = True
                    Me.rfvFuForesta.Enabled = True
                    Me.imgExtensionesForesta.Visible = True
                    btnAgregarForesta.Visible = True

                Else
                    Me.fuForesta.Visible = False
                    Me.imgExtensionesForesta.Visible = False
                    Me.rfvFuForesta.Enabled = False
                    Me.ArchivoForesta.Archivo = Nothing
                    Me.ArchivoForesta.NombreArchivo = String.Empty
                    Me.fuForesta.Visible = False
                    Me.lnkArchivoForesta.Visible = False
                    Me.btnEliminarArchivoForesta.Visible = False
                    btnAgregarForesta.Visible = False

                End If
                BanderaCambios = True
                WebUtils.RegistrarScript(Me.Page, "regresarNormal", "regresarNormal();")
            End If

            Me.ManejoEventoCheck = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        activarContempladas()
    End Sub

    ''' <summary>
    ''' evento que se ejecuta se carga la lista de actividades, por cada registro del
    ''' repeater se asigna un identificador unico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>4/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpActividadesEjecucion_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpActividades.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        Dim vlo_IbModificar As ImageButton
        If Editable Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                    vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                    vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
                End If
                If e.Item.FindControl("ibmodificar") IsNot Nothing Then
                    vlo_IbModificar = CType(e.Item.FindControl("ibmodificar"), ImageButton)
                    vlo_IbModificar.Attributes.Add("data-uniqueid", vlo_IbModificar.UniqueID)
                End If
            End If
        Else
            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                vlo_IbBorrar.Visible = False
            End If
        End If
        activarContempladas()
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click sobre el boton de eliminar
    ''' del listado de funcionarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>4/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            BorrarActividad(CType(sender, ImageButton).CommandName)
            activarContempladas()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se desea modificar una actividad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>4/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibmodificar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Me.txtActividad.Text = CType(sender, ImageButton).CommandName
            Actividad = CType(sender, ImageButton).CommandName
            Me.btnAgregarActividad.Visible = False
            Me.btnModificarActividad.Visible = True
            activarContempladas()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click sobre el boton de eliminar
    ''' del listado de funcionarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>4/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnModificarActividad_Click(sender As Object, e As EventArgs)
        Try
            ModificarActividad()
            Me.btnAgregarActividad.Visible = True
            Me.btnModificarActividad.Visible = False
            activarContempladas()
        Catch ex As Exception
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
    ''' <creationDate>04/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlTipoArchivo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoArchivo.SelectedIndexChanged
        Dim vlo_TipoDocumento() As DataRow
        Try
            If Me.ddlTipoArchivo.SelectedValue <> String.Empty Then
                vlo_TipoDocumento = DsTipoArchivo.Tables(0).Select(String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, Me.ddlTipoArchivo.SelectedValue))
                If DsTipoArchivo.Tables.Count > 0 AndAlso vlo_TipoDocumento IsNot Nothing Then
                    Me.ExtensionesPermitidas = vlo_TipoDocumento(0).Item(Modelo.OTM_TIPO_DOCUMENTO.FORMATOS_ADMITIDOS)
                    imgExtensiones.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidas.ToLower))
                    Me.TamanoArchivo = vlo_TipoDocumento(0).Item(Modelo.OTM_TIPO_DOCUMENTO.TAMANIO_MAXIMO)

                End If

                Me.trArchivosTipo.Visible = True
            Else
                Me.trArchivosTipo.Visible = False
            End If

            activarDocumentos()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al carar el repeater de adjuntos, asigna atributos a cada elemento del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>04/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpAdjunto_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpAdjunto.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        If Editable Then
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
        activarDocumentos()
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
            activarDocumentos()
            Me.trArchivosTipo.Visible = False
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' descarga el archivo adjunto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>04/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnDescargarArchivo_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(CType(sender, ImageButton).CommandArgument))(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CType(Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(CType(sender, ImageButton).CommandArgument))(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ARCHIVO), Byte()))
            Response.End()
            activarDocumentos()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
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
    ''' <creationDate>04/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrarAdjunto_Click(sender As Object, e As ImageClickEventArgs)
        Dim vln_Indice As Integer
        Dim vlo_filanueva As DataRow

        Try
            vlo_filanueva = DsArchivosBorrados.Tables(0).NewRow()
            vlo_filanueva.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ADJUNTO_ORDEN_TRABAJO) = Convert.ToInt32(CType(sender, ImageButton).CommandArgument)
            DsArchivosBorrados.Tables(0).Rows.Add(vlo_filanueva)

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
        activarDocumentos()
    End Sub

    ''' <summary>
    ''' opcion de descargar un archivo seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivo_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Dim vln_IdAdjunto As Integer = Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ADJUNTO_ORDEN_TRABAJO).ToString

            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CType(ObtenerArchivo(vln_IdAdjunto).Archivo, Byte()))
            Response.End()
            activarDocumentos()
        Catch ex As System.Threading.ThreadAbortException
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
    ''' <creationDate>09/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try

            If (((Me.chkPlantaFisica.Checked) And (Me.ArchivoPlantaFisica.Archivo IsNot Nothing)) Or ((Not Me.chkPlantaFisica.Checked) And (Me.ArchivoPlantaFisica.Archivo Is Nothing))) Or (Me.lnkArchivoPlantaFisica.Visible = True) Then

                If (((Me.chkForesta.Checked) And (Me.ArchivoForesta.Archivo IsNot Nothing)) Or ((Not Me.chkForesta.Checked) And (Me.ArchivoForesta.Archivo Is Nothing))) Or (Me.lnkArchivoForesta.Visible = True) Then

                    If GuardarAnteproyecto() Then
                        WebUtils.RegistrarScript(Me.Page, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
                    Else
                        MostrarAlertaError("No ha sido posible guardar la información del registro")
                    End If
                Else
                    MostrarAlertaError("Debe de agregar el archivo de foresta")
                End If
            Else
                MostrarAlertaError("Debe de agregar el archivo de planta física")
            End If

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
    ''' <creationDate>10/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardarYEnviar_Click(sender As Object, e As EventArgs) Handles btnGuardarYEnviar.Click
        Try
            If (((Me.chkPlantaFisica.Checked) And (Me.ArchivoPlantaFisica.Archivo IsNot Nothing)) Or ((Not Me.chkPlantaFisica.Checked) And (Me.ArchivoPlantaFisica.Archivo Is Nothing))) Or (Me.lnkArchivoPlantaFisica.Visible = True) Then

                If (((Me.chkForesta.Checked) And (Me.ArchivoForesta.Archivo IsNot Nothing)) Or ((Not Me.chkForesta.Checked) And (Me.ArchivoForesta.Archivo Is Nothing))) Or (Me.lnkArchivoForesta.Visible = True) Then

                    Me.Editable = 0

                    If GuardarAnteproyecto() Then
                        WebUtils.RegistrarScript(Me.Page, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
                    Else
                        MostrarAlertaError("No ha sido posible guardar la información del registro")
                    End If
                Else
                    MostrarAlertaError("Debe de agregar el archivo de foresta")
                End If
            Else
                MostrarAlertaError("Debe de agregar el archivo de planta física")
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' elimina el archivo aval planta fisica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>09/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnEliminarArchivoPlanta_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminarArchivoPlanta.Click
        Try
            Me.ArchivoPlantaFisica.Archivo = Nothing
            Me.ArchivoPlantaFisica.NombreArchivo = String.Empty
            Me.fuPlantaFisica.Visible = True
            Me.imgExtensionesPlanta.Visible = True
            Me.lnkArchivoPlantaFisica.Visible = False
            Me.btnEliminarArchivoPlanta.Visible = False
            btnAgregarPlanta.Visible = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        activarContempladas()
    End Sub

    ''' <summary>
    ''' elimina el archivo aval foresta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>10/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnEliminarArchivoForesta_Click(sender As Object, e As ImageClickEventArgs) Handles btnEliminarArchivoForesta.Click
        Try

            Me.ArchivoForesta.Archivo = Nothing
            Me.ArchivoForesta.NombreArchivo = String.Empty
            Me.fuForesta.Visible = True
            Me.imgExtensionesForesta.Visible = True
            Me.lnkArchivoForesta.Visible = False
            Me.btnEliminarArchivoForesta.Visible = False
            btnAgregarForesta.Visible = True
            activarContempladas()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' descargar archivo planta fisica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>09/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivoPlanta_Click(sender As Object, e As EventArgs) Handles lnkArchivoPlantaFisica.Click
        DescargaArchivo(Me.ArchivoPlantaFisica.IdAdjuntoOrdenTrabajo, Me.ArchivoPlantaFisica.NombreArchivo)
        activarContempladas()
    End Sub

    ''' <summary>
    ''' descargar archivo foresta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>10/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivoForesta_Click(sender As Object, e As EventArgs) Handles lnkArchivoForesta.Click
        DescargaArchivo(Me.ArchivoForesta.IdAdjuntoOrdenTrabajo, Me.ArchivoForesta.NombreArchivo)
        activarContempladas()
    End Sub

    Protected Sub btnAgregarPlanta_Click(sender As Object, e As EventArgs) Handles btnAgregarPlanta.Click
        Try
            Me.ArchivoPlantaFisica.NombreArchivo = Me.fuPlantaFisica.FileName
            Me.ArchivoPlantaFisica.Archivo = Me.fuPlantaFisica.FileBytes
            fuPlantaFisica.Visible = False
            imgExtensionesPlanta.Visible = False
            btnAgregarPlanta.Visible = False
            lnkArchivoPlantaFisica.Text = Me.fuPlantaFisica.FileName
            lnkArchivoPlantaFisica.Visible = True
            btnEliminarArchivoPlanta.Visible = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        activarContempladas()
    End Sub

    Protected Sub btnAgregarForesta_Click(sender As Object, e As EventArgs) Handles btnAgregarForesta.Click
        Try
            Me.ArchivoForesta.NombreArchivo = Me.fuForesta.FileName
            Me.ArchivoForesta.Archivo = Me.fuForesta.FileBytes
            fuForesta.Visible = False
            imgExtensionesForesta.Visible = False
            lnkArchivoForesta.Text = Me.fuForesta.FileName
            btnAgregarForesta.Visible = False
            lnkArchivoForesta.Visible = True
            btnEliminarArchivoForesta.Visible = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        activarContempladas()
    End Sub

    ''' <summary>
    ''' Genera pdf
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try

            Me.EmpleadoEjecuta = CargarFuncionarioNumEmpleado(Me.Usuario.NumEmpleado)

            Me.Session.Add("pvo_EntOttAnteproyecto", Me.Anteproyecto)
            Me.Session.Add("pvc_Condicion", String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}", Modelo.OTT_ANTEPROYECTO.ID_UBICACION, Me.Anteproyecto.IdUbicacion, Modelo.OTT_ANTEPROYECTO.ID_ORDEN_TRABAJO, Me.Anteproyecto.IdOrdenTrabajo, Modelo.OTT_ANTEPROYECTO.VERSION, Me.Anteproyecto.Version))
            Me.Session.Add("pvc_EmpleadoEjecuta", String.Format("{0} {1} {2}", Me.EmpleadoEjecuta.NOMBRE, Me.EmpleadoEjecuta.APELLIDO1, Me.EmpleadoEjecuta.APELLIDO2))

            ScriptManager.RegisterStartupScript(Me, GetType(String), "redirect", String.Format("window.open('../../Controles/Frm_OT_ManejoReportes.aspx?pvc_RutaBase={0}&pvc_NombreReporte={1}&pvc_FormatoReporte={2}&pvn_Concatenar={3}', 'ticker', 'toolbar=no,menubar=no,location=no, scrollbars=YES,scroll=YES');", Utilerias.OrdenesDeTrabajo.Reportes.RUTA_BASE, Utilerias.OrdenesDeTrabajo.Reportes.RPT_OT_ANTE_PROYECTO, FORMATO_REPORTE.PDF, 1), True)
            activarContempladas()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Controla la consulta de versiones del anteproyecto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlVersion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlVersion.SelectedIndexChanged
        Try
            cargarAnteProyecto(Me.ddlVersion.SelectedValue)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Método encargado de descargar archivos
    ''' </summary>
    ''' <param name="pvc_NombreArchivo">nombre del archivo a descargar</param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>10/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub DescargaArchivo(pvn_idAdjunto As Integer, pvc_NombreArchivo As String)
        pvc_NombreArchivo = pvc_NombreArchivo.Replace(" ", "")
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + pvc_NombreArchivo)
            Response.BinaryWrite(ObtenerArchivo(pvn_idAdjunto).Archivo)
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>4/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>4/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub activarContempladas()
        WebUtils.RegistrarScript(Me, "activarContempladas", "activarContempladas();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>4/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub activarDocumentos()
        WebUtils.RegistrarScript(Me, "activarDocumentos", "activarDocumentos();")
    End Sub

    ''' <summary>
    ''' Inicializa el comportamiento que tendrá la pantalla
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarFormulario()
        Me.PaginaRegresar = "Lst_OT_GestionProfesionalesDisenio.aspx"
        LeerParametros()
        CargarUnidadMedida()
        inicializarSetDatos()
        InicializarControlUsuario()
        cargarAnteProyecto()
        CargaDsAdjuntos()
        inicializarComponentes()
        cargarAvalOpcionesPermitidas()
        CargarUnidades(String.Format("{0} = '{1}'", Modelo.V_OTM_UNIDAD_TIEMPOLST.ESTADO, Estado.ACTIVO), String.Empty, 1)
        CargarListaTipoArchivo(String.Format("{0} = '{1}' AND {2} = {3}", Modelo.OTM_TIPO_DOCUMENTO.ESTADO, Estado.ACTIVO, Modelo.OTM_TIPO_DOCUMENTO.PROTEGIDO, Proteccion.NO_PROTEGIDO))
        activarContempladas()
        Me.BanderaCambios = False
        If Me.Operacion = eOperacion.Consultar Then
            Me.btnEliminarArchivoPlanta.Visible = False
            Me.btnEliminarArchivoForesta.Visible = False
            Me.PaginaRegresar = WebUtils.LeerParametro(Of String)("pvn_Regresar")
            WebUtils.RegistrarScript(Me, "regresarConsulta", "regresarConsulta();")
            WebUtils.RegistrarScript(Me, "deshabilitar", "deshabilitar();")
        Else
            WebUtils.RegistrarScript(Me, "regresar", "regresar();")
        End If
        Me.ManejoEventoCheck = True
    End Sub

    ''' <summary>
    ''' Lee los parametros de la sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>4/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.IdSectorTaller = WebUtils.LeerParametro(Of Integer)("pvn_IdSectorTaller")
        Me.ProfesionalEncargado = WebUtils.LeerParametro(Of Integer)("pvn_IdEncargado")
        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
    End Sub

    ''' <summary>
    ''' Carga las medidas de metro, metro cuadrado y metro cúbico en el combo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>4/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarUnidadMedida()
        Me.ddlUnidadMedida.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
        Me.ddlUnidadMedida.Items.Add(New ListItem("Metro", Cantidad.METRO))
        Me.ddlUnidadMedida.Items.Add(New ListItem("Metro Cuadrado", Cantidad.METRO_CUADRADO))
        Me.ddlUnidadMedida.Items.Add(New ListItem("Metro Cúbico", Cantidad.METRO_CUBICO))
    End Sub

    ''' <summary>
    ''' Inicializa la visibilidad de los componentes y algunas propiedades
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarComponentes()
        If Editable Then
            Me.btnNuevaVersion.Visible = False
            Me.btnGuardar.Visible = True
            Me.btnGuardarYEnviar.Visible = True
        Else
            Me.trRevisionUsuario.Visible = False
            Me.btnGuardar.Visible = False
            Me.btnGuardarYEnviar.Visible = False
            WebUtils.RegistrarScript(Me, "deshabilitarVersion", "deshabilitarVersion();")
        End If
        Me.fuPlantaFisica.Visible = False
        Me.fuForesta.Visible = False
        Me.imgExtensionesPlanta.Visible = False
        Me.imgExtensionesForesta.Visible = False
        Me.Usuario = New UsuarioActual()
    End Sub

    ''' <summary>
    ''' Inicializa la visibilidad de los componentes y algunas propiedades
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarComponentesVersion()
        If Editable Then
            Me.btnNuevaVersion.Visible = False
            Me.btnGuardar.Visible = True
            Me.btnGuardarYEnviar.Visible = True
            'habilitarVersion
            WebUtils.RegistrarScript(Me, "habilitarVersion", "habilitarVersion();")
        Else
            Me.trRevisionUsuario.Visible = False
            Me.btnGuardar.Visible = False
            Me.btnGuardarYEnviar.Visible = False
            WebUtils.RegistrarScript(Me, "deshabilitarVersion", "deshabilitarVersion();")
        End If

        Me.Usuario = New UsuarioActual()
    End Sub

    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarSetDatos()
        Dim vlo_columna As DataColumn
        Dim vlo_llaves(1) As DataColumn


        'Se crea un nuevo datatabla 
        Me.DsActividades = New DataTable

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = "ACTIVIDAD"
        'Se agrega la columna configurada al set de datos
        DsActividades.Columns.Add(vlo_columna)
        'Se agrega al arreglo de llaves primarias la columna
        vlo_llaves(0) = vlo_columna
        'Y se configura el set de datos para que busque por formatos admitidos como llave primaria.
        Me.DsActividades.PrimaryKey = vlo_llaves

    End Sub


    ''' <summary>
    ''' Inicializa el control de usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>4/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarControlUsuario()
        'Asignación de Datos para generar el web user control de la información general
        ctrl_InfoGeneral.Anno = Me.Anno
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.IdUbicacion
        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ActualizarListadoVersionesAnteproyecto()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ANTEPROYECTO_ListarRegistrosLista(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            String.Format("{0} = {1} AND {2} = '{3}'",
                        Modelo.V_OTT_ANTEPROYECTOLST.ID_UBICACION,
                        Me.IdUbicacion,
                        Modelo.V_OTT_ANTEPROYECTOLST.ID_ORDEN_TRABAJO,
                        Me.IdOrdenTrabajo), String.Format("{0} {1}", Modelo.V_OTT_ANTEPROYECTOLST.VERSION, Ordenamiento.DESCENDENTE), False, 0, 0)

            Me.ddlVersion.Items.Clear()
            'Me.ddlVersion.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
            For Each vlo_fila As DataRow In vlo_DsDatos.Tables(0).Rows
                Me.ddlVersion.Items.Add(New ListItem(vlo_fila.Item(Modelo.V_OTT_ANTEPROYECTOLST.VERSION), vlo_fila.Item(Modelo.V_OTT_ANTEPROYECTOLST.VERSION)))
            Next

            Me.ddlVersion.SelectedValue = vlo_DsDatos.Tables(0).Rows(0)(Modelo.V_OTT_ANTEPROYECTOLST.VERSION).ToString

            ActualizarCargaDeAnteProyecto(Me.ddlVersion.SelectedValue)

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
    ''' Carga el anteproyecto editable si existe,
    ''' Si no se deberá crear la version uno del anteproyecto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>02/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub cargarAnteProyecto()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet
        Dim vlo_Encargado As WsrEU_Curriculo.EntEmpleados
        Dim vlo_UltimoAnteproyecto As DataRow

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0}: nombre columna
            '{1}: id ubicacion
            '{2}: Nombre de columna
            '{3}: Id orden trabajo
            '{4}: nombre de columna
            '{5}: traer la última version editable

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ANTEPROYECTO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'",
                            Modelo.V_OTT_ANTEPROYECTOLST.ID_UBICACION,
                            Me.IdUbicacion,
                            Modelo.V_OTT_ANTEPROYECTOLST.ID_ORDEN_TRABAJO,
                            Me.IdOrdenTrabajo), String.Format("{0} {1}", Modelo.V_OTT_ANTEPROYECTOLST.VERSION, Ordenamiento.DESCENDENTE), False, 0, 0)

            ' Me.ddlVersion.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
            For Each vlo_fila As DataRow In vlo_DsDatos.Tables(0).Rows
                Me.ddlVersion.Items.Add(New ListItem(vlo_fila.Item(Modelo.V_OTT_ANTEPROYECTOLST.VERSION), vlo_fila.Item(Modelo.V_OTT_ANTEPROYECTOLST.VERSION)))
            Next
            Me.Anteproyecto = New EntOttAnteproyecto

            'Carga el profesional encargado y coloca el nombre completo en el encabezado correspondiente
            vlo_Encargado = CargarFuncionario(Me.ProfesionalEncargado)
            Me.lblEncargado.Text = String.Format("{0} {1} {2}", vlo_Encargado.NOMBRE, vlo_Encargado.APELLIDO1, vlo_Encargado.APELLIDO2)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then

                'Se carga el ultimo anteproyecto 
                vlo_UltimoAnteproyecto = vlo_DsDatos.Tables(0).Rows(0)
                'Se agrega un identificador de la version en la lista para que seleccione cual desea consultar


                If vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.EDITABLE) = Version.EDITABLE Then
                    Anteproyecto.Editable = Version.EDITABLE
                    'Mostrar Datos con la fila editable
                    Me.Editable = True
                Else
                    Anteproyecto.Editable = Version.NO_EDITABLE
                    Me.Editable = False
                End If

                'Se cargan los datos del anteproyecto en el objeto en memoria
                Anteproyecto.Version = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.VERSION)
                Anteproyecto.FechaEnvia = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.FECHA_ENVIA)
                Anteproyecto.FechaResponde = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.FECHA_RESPONDE)
                Anteproyecto.Descripcion = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.DESCRIPCION)
                Anteproyecto.Cantidad = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.CANTIDAD)
                Anteproyecto.UnidadMedida = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.UNIDAD_MEDIDA)
                Anteproyecto.IdUnidadTiempo = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.ID_UNIDAD_TIEMPO)
                Anteproyecto.AvalForesta = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.AVAL_FORESTA)
                Anteproyecto.AvalPlantaFisica = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.AVAL_PLANTA_FISICA)
                Anteproyecto.ActividadesContempladas = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.ACTIVIDADES_CONTEMPLADAS)
                Anteproyecto.TiempoRespuesta = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.TIEMPO_RESPUESTA)
                Anteproyecto.IdOrdenTrabajo = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.ID_ORDEN_TRABAJO)
                Anteproyecto.IdUbicacion = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.ID_UBICACION)
                Anteproyecto.TimeStamp = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.TIME_STAMP)
                Anteproyecto.Existe = True


                Me.ddlVersion.SelectedValue = Anteproyecto.Version
                Me.VersionActual = Anteproyecto.Version

                Me.lblFechaEnvia.Text = CType(Anteproyecto.FechaEnvia, DateTime).ToString(Constantes.FORMATO_FECHA_UI)

                Me.lblFechaRespuesta.Text = IIf(String.IsNullOrWhiteSpace(Anteproyecto.FechaEnvia),
                                                CType(Anteproyecto.FechaResponde, DateTime).ToString(Constantes.FORMATO_FECHA_UI),
                                                String.Empty)

                If String.IsNullOrWhiteSpace(vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.OBSERVACIONES).ToString) Then
                    Me.lblObservaciones.Text = "Sin Observaciones"
                    Me.imgObservaciones.Visible = False
                Else
                    Me.lblObservaciones.Text = "Con Observaciones"
                    Me.imgObservaciones.Attributes.Add("title", vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.OBSERVACIONES))
                End If

                Me.chkPlantaFisica.Checked = Me.Anteproyecto.AvalPlantaFisica
                Me.chkForesta.Checked = Me.Anteproyecto.AvalForesta

                Me.txtDescripcion.Text = Anteproyecto.Descripcion
                Me.txtCantidad.Text = Anteproyecto.Cantidad
                Me.ddlUnidadMedida.SelectedValue = Anteproyecto.UnidadMedida
                Me.trRevisionUsuario.Visible = True

                Me.txtTiempoRespuesta.Text = Anteproyecto.TiempoRespuesta
                Me.ddlUnidadTiempoRespuesta.SelectedValue = Anteproyecto.IdUnidadTiempo
                If Anteproyecto.ActividadesContempladas <> "-" Then
                    cargarActividades(Anteproyecto.ActividadesContempladas)
                End If

                Me.ArchivoPlantaFisica = New EntOttAdjuntoOrdenTrabajo
                Me.ArchivoForesta = New EntOttAdjuntoOrdenTrabajo
                Me.btnPDF.Visible = True
            Else
                'Si no hay versiones registradas se crea la version uno por default
                Me.Editable = True
                Me.trRevisionUsuario.Visible = False
                Me.Anteproyecto = New EntOttAnteproyecto

                Me.ArchivoPlantaFisica = New EntOttAdjuntoOrdenTrabajo
                Me.ArchivoPlantaFisica.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.ANTEPROYECTO
                Me.ArchivoPlantaFisica.IdOrdenTrabajo = Me.IdOrdenTrabajo
                Me.ArchivoPlantaFisica.IdTipoDocumento = TipoDocumento.AVAL_PLANTA_FISICA
                Me.ArchivoPlantaFisica.IdUbicacion = Me.IdUbicacion
                Me.ArchivoPlantaFisica.Usuario = Me.Usuario.UserName
                Me.ArchivoPlantaFisica.Descripcion = "Nota de solicitud para Aval de Planta Física"


                Me.ArchivoForesta = New EntOttAdjuntoOrdenTrabajo
                Me.ArchivoForesta.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.ANTEPROYECTO
                Me.ArchivoForesta.IdOrdenTrabajo = Me.IdOrdenTrabajo
                Me.ArchivoForesta.IdTipoDocumento = TipoDocumento.AVAL_FORESTA
                Me.ArchivoForesta.IdUbicacion = Me.IdUbicacion
                Me.ArchivoForesta.Usuario = Me.Usuario.UserName
                Me.ArchivoForesta.Descripcion = "Nota de solicitud para Aval de Foresta"

                Me.VersionActual = Version.EDITABLE
                Me.ddlVersion.Items.Add(New ListItem("1", "1"))
                Me.ddlVersion.SelectedValue = 1
                Me.btnGuardarYEnviar.Visible = True
                Me.btnGuardar.Visible = False
                Me.btnPDF.Visible = False
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
        activarContempladas()
    End Sub

    ''' <summary>
    ''' Carga el anteproyecto de una versión específica
    ''' </summary>
    ''' <param name="pvc_Version"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez ondoy</author>
    ''' <creationDate>16/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ActualizarCargaDeAnteProyecto(pvc_Version As String)
        Dim pvn_Version As Integer
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet
        Dim vlo_UltimoAnteproyecto As DataRow

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Not String.IsNullOrWhiteSpace(pvc_Version) Then
                pvn_Version = CInt(pvc_Version)
            End If
            'Se obtienen los datos del anteproyecto específico
            '{0}: nombre columna
            '{1}: id ubicacion
            '{2}: Nombre de columna
            '{3}: Id orden trabajo
            '{4}: nombre de columna
            '{5}: traer la última version editable

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ANTEPROYECTO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}",
                            Modelo.V_OTT_ANTEPROYECTOLST.ID_UBICACION,
                            Me.IdUbicacion,
                            Modelo.V_OTT_ANTEPROYECTOLST.ID_ORDEN_TRABAJO,
                            Me.IdOrdenTrabajo,
                            Modelo.V_OTT_ANTEPROYECTOLST.VERSION,
                            pvn_Version), String.Empty, False, 0, 0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                'Se carga la fila existente anteproyecto 
                vlo_UltimoAnteproyecto = vlo_DsDatos.Tables(0).Rows(0)
                'Se agrega un identificador de la version en la lista para que seleccione cual desea consultar


                If vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.EDITABLE) = Version.EDITABLE Then
                    Anteproyecto.Editable = Version.EDITABLE
                    'Mostrar Datos con la fila editable
                    Me.Editable = True
                Else
                    Anteproyecto.Editable = Version.NO_EDITABLE
                    Me.Editable = False
                End If

                'Se cargan los datos del anteproyecto en el objeto en memoria
                Anteproyecto.Version = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.VERSION)
                Anteproyecto.FechaEnvia = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.FECHA_ENVIA)
                Anteproyecto.FechaResponde = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.FECHA_RESPONDE)
                Anteproyecto.Descripcion = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.DESCRIPCION)
                Anteproyecto.Cantidad = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.CANTIDAD)
                Anteproyecto.UnidadMedida = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.UNIDAD_MEDIDA)
                Anteproyecto.IdUnidadTiempo = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.ID_UNIDAD_TIEMPO)
                Anteproyecto.AvalForesta = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.AVAL_FORESTA)
                Anteproyecto.AvalPlantaFisica = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.AVAL_PLANTA_FISICA)
                Anteproyecto.ActividadesContempladas = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.ACTIVIDADES_CONTEMPLADAS)
                Anteproyecto.TiempoRespuesta = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.TIEMPO_RESPUESTA)
                Anteproyecto.IdOrdenTrabajo = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.ID_ORDEN_TRABAJO)
                Anteproyecto.IdUbicacion = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.ID_UBICACION)
                Anteproyecto.TimeStamp = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.TIME_STAMP)
                Anteproyecto.Existe = True

                Me.VersionActual = Anteproyecto.Version

                Me.lblFechaEnvia.Text = CType(Anteproyecto.FechaEnvia, DateTime).ToString(Constantes.FORMATO_FECHA_UI)

                Me.lblFechaRespuesta.Text = IIf(String.IsNullOrWhiteSpace(Anteproyecto.FechaEnvia),
                                                CType(Anteproyecto.FechaResponde, DateTime).ToString(Constantes.FORMATO_FECHA_UI),
                                                String.Empty)

                If String.IsNullOrWhiteSpace(vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.OBSERVACIONES).ToString) Then
                    Me.lblObservaciones.Text = "Sin Observaciones"
                    Me.imgObservaciones.Visible = False
                Else
                    Me.lblObservaciones.Text = "Con Observaciones"
                    Me.imgObservaciones.Attributes.Add("title", vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.OBSERVACIONES))
                End If

                Me.chkPlantaFisica.Checked = Me.Anteproyecto.AvalPlantaFisica
                Me.chkForesta.Checked = Me.Anteproyecto.AvalForesta

                Me.txtDescripcion.Text = Anteproyecto.Descripcion
                Me.txtCantidad.Text = Anteproyecto.Cantidad
                Me.ddlUnidadMedida.SelectedValue = Anteproyecto.UnidadMedida
                Me.trRevisionUsuario.Visible = True

                Me.txtTiempoRespuesta.Text = Anteproyecto.TiempoRespuesta
                Me.ddlUnidadTiempoRespuesta.SelectedValue = Anteproyecto.IdUnidadTiempo
                ' cargarActividades(Anteproyecto.ActividadesContempladas)
                CargaDsAdjuntos()
                ' inicializarComponentes()
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
        activarContempladas()
    End Sub


    ''' <summary>
    ''' Carga el anteproyecto de una versión específica
    ''' </summary>
    ''' <param name="pvc_Version"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub cargarAnteProyecto(pvc_Version As String)
        Dim pvn_Version As Integer
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet
        Dim vlo_UltimoAnteproyecto As DataRow

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Not String.IsNullOrWhiteSpace(pvc_Version) Then
                pvn_Version = CInt(pvc_Version)
            End If
            'Se obtienen los datos del anteproyecto específico
            '{0}: nombre columna
            '{1}: id ubicacion
            '{2}: Nombre de columna
            '{3}: Id orden trabajo
            '{4}: nombre de columna
            '{5}: traer la última version editable

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ANTEPROYECTO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}",
                            Modelo.V_OTT_ANTEPROYECTOLST.ID_UBICACION,
                            Me.IdUbicacion,
                            Modelo.V_OTT_ANTEPROYECTOLST.ID_ORDEN_TRABAJO,
                            Me.IdOrdenTrabajo,
                            Modelo.V_OTT_ANTEPROYECTOLST.VERSION,
                            pvn_Version), String.Empty, False, 0, 0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                'Se carga la fila existente anteproyecto 
                vlo_UltimoAnteproyecto = vlo_DsDatos.Tables(0).Rows(0)
                'Se agrega un identificador de la version en la lista para que seleccione cual desea consultar

                If vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.EDITABLE) = Version.EDITABLE Then
                    Anteproyecto.Editable = Version.EDITABLE
                    'Mostrar Datos con la fila editable
                    Me.Editable = True
                Else
                    Anteproyecto.Editable = Version.NO_EDITABLE
                    Me.Editable = False
                End If

                'Se cargan los datos del anteproyecto en el objeto en memoria
                Anteproyecto.Version = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.VERSION)
                Anteproyecto.FechaEnvia = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.FECHA_ENVIA)
                Anteproyecto.FechaResponde = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.FECHA_RESPONDE)
                Anteproyecto.Descripcion = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.DESCRIPCION)
                Anteproyecto.Cantidad = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.CANTIDAD)
                Anteproyecto.UnidadMedida = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.UNIDAD_MEDIDA)
                Anteproyecto.IdUnidadTiempo = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.ID_UNIDAD_TIEMPO)
                Anteproyecto.AvalForesta = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.AVAL_FORESTA)
                Anteproyecto.AvalPlantaFisica = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.AVAL_PLANTA_FISICA)
                Anteproyecto.ActividadesContempladas = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.ACTIVIDADES_CONTEMPLADAS)
                Anteproyecto.TiempoRespuesta = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.TIEMPO_RESPUESTA)
                Anteproyecto.IdOrdenTrabajo = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.ID_ORDEN_TRABAJO)
                Anteproyecto.IdUbicacion = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.ID_UBICACION)
                Anteproyecto.TimeStamp = vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.TIME_STAMP)
                Anteproyecto.Existe = True


                Me.ddlVersion.SelectedValue = Anteproyecto.Version
                Me.VersionActual = Anteproyecto.Version

                Me.lblFechaEnvia.Text = CType(Anteproyecto.FechaEnvia, DateTime).ToString(Constantes.FORMATO_FECHA_UI)

                Me.lblFechaRespuesta.Text = IIf(String.IsNullOrWhiteSpace(Anteproyecto.FechaEnvia),
                                                CType(Anteproyecto.FechaResponde, DateTime).ToString(Constantes.FORMATO_FECHA_UI),
                                                String.Empty)

                If String.IsNullOrWhiteSpace(vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.OBSERVACIONES).ToString) Then
                    Me.lblObservaciones.Text = "Sin Observaciones"
                    Me.imgObservaciones.Visible = False
                Else
                    Me.lblObservaciones.Text = "Con Observaciones"
                    Me.imgObservaciones.Attributes.Add("title", vlo_UltimoAnteproyecto.Item(Modelo.V_OTT_ANTEPROYECTOLST.OBSERVACIONES))
                End If

                Me.ManejoEventoCheck = False
                Me.chkPlantaFisica.Checked = Me.Anteproyecto.AvalPlantaFisica
                Me.ManejoEventoCheck = False
                Me.chkForesta.Checked = Me.Anteproyecto.AvalForesta

                Me.txtDescripcion.Text = Anteproyecto.Descripcion
                Me.txtCantidad.Text = Anteproyecto.Cantidad
                Me.ddlUnidadMedida.SelectedValue = Anteproyecto.UnidadMedida
                Me.trRevisionUsuario.Visible = True

                Me.txtTiempoRespuesta.Text = Anteproyecto.TiempoRespuesta
                Me.ddlUnidadTiempoRespuesta.SelectedValue = Anteproyecto.IdUnidadTiempo
                Me.DsActividades.Rows.Clear()
                cargarActividades(Anteproyecto.ActividadesContempladas)
                CargaDsAdjuntos()
                inicializarComponentesVersion()

                If Me.chkPlantaFisica.Checked Then
                    Me.ArchivoPlantaFisica.Existe = True
                    Me.fuPlantaFisica.Visible = False
                    Me.btnAgregarPlanta.Visible = False
                    Me.rfvFuPlantaFisica.Enabled = False
                    Me.btnEliminarArchivoPlanta.Visible = True
                    Me.lnkArchivoPlantaFisica.Visible = True
                Else
                    Me.ArchivoPlantaFisica.Existe = False
                    Me.fuPlantaFisica.Visible = False
                    Me.btnAgregarPlanta.Visible = False
                    Me.rfvFuPlantaFisica.Enabled = False
                    Me.btnEliminarArchivoPlanta.Visible = False
                    Me.lnkArchivoPlantaFisica.Visible = False
                End If

                If Me.chkForesta.Checked Then
                    Me.ArchivoForesta.Existe = True
                    Me.fuForesta.Visible = False
                    Me.btnAgregarForesta.Visible = False
                    Me.rfvFuForesta.Enabled = False
                    Me.lnkArchivoForesta.Visible = True
                    Me.btnEliminarArchivoForesta.Visible = True
                Else
                    Me.ArchivoForesta.Existe = False
                    Me.fuForesta.Visible = False
                    Me.btnAgregarForesta.Visible = False
                    Me.rfvFuForesta.Enabled = False
                    Me.lnkArchivoForesta.Visible = False
                    Me.btnEliminarArchivoForesta.Visible = False
                End If

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
        activarContempladas()
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de separar las actividades y de insertarlas a la lista
    ''' </summary>
    ''' <param name="pvc_Actividades"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>02/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub cargarActividades(pvc_Actividades As String)
        Dim vlo_Actividades() As String
        Dim vlo_DrNuevaFila As DataRow

        Me.ActividadCaracteresRestantes = pvc_Actividades.Length

        vlo_Actividades = pvc_Actividades.Split("¬")

        For Each pvc_Actividad As String In vlo_Actividades
            vlo_DrNuevaFila = Me.DsActividades.NewRow

            vlo_DrNuevaFila.Item("ACTIVIDAD") = pvc_Actividad.ToUpper
            Me.DsActividades.Rows.Add(vlo_DrNuevaFila)
        Next

        If Me.DsActividades IsNot Nothing AndAlso Me.DsActividades.Rows.Count > 0 Then
            Me.rpActividades.DataSource = DsActividades
            Me.rpActividades.DataMember = Me.DsActividades.TableName
            Me.rpActividades.DataBind()
            Me.rpActividades.Visible = True
        Else
            With Me.rpActividades
                .DataSource = Nothing
                .DataBind()
            End With
            Me.rpActividades.Visible = False
        End If


    End Sub

    ''' <summary>
    ''' Carga las variables para validar archivos aval planta fisica y aval foresta
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>02/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub cargarAvalOpcionesPermitidas()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As Wsr_OT_Catalogos.EntOtmTipoDocumento


        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0}: ID_TIPO_DOCUMENTO
            '{1}: AVAL_FORESTA
            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Utilerias.OrdenesDeTrabajo.Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.AVAL_FORESTA))

            'Se cargan los parámetros para validar los archivos que sean para aval foresta
            Me.ExtensionesPermitidasForesta = vlo_EntOtmTipoDocumento.FormatosAdmitidos
            imgExtensionesForesta.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidasForesta.ToLower))
            Me.TamanoArchivoForesta = vlo_EntOtmTipoDocumento.TamanioMaximo

            '{0}: ID_TIPO_DOCUMENTO
            '{1}: AVAL_PLANTA_FISICA
            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Utilerias.OrdenesDeTrabajo.Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.AVAL_PLANTA_FISICA))

            'Se cargan los parámetros para validar los archivos que sean para aval planta fisica
            Me.ExtensionesPermitidasPlanta = vlo_EntOtmTipoDocumento.FormatosAdmitidos
            imgExtensionesPlanta.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidasPlanta.ToLower))
            Me.TamanoArchivoPlanta = vlo_EntOtmTipoDocumento.TamanioMaximo

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Carga la lista de unidades de tiempo
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>04/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarUnidades(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_UNIDAD_TIEMPO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden, True, pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                'ddl de unidad para valoración
                With Me.ddlUnidadTiempoRespuesta
                    .Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_UNIDAD_TIEMPOLST.DESCRIPCION
                    .DataValueField = Modelo.V_OTM_UNIDAD_TIEMPOLST.ID_UNIDAD_TIEMPO
                    .DataBind()
                End With


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
    ''' agrega una actividad al dataset
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>04/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregaActividadesDataTable()
        Dim vlo_DrNuevaFila As DataRow
        Dim vlc_Actividad As String

        Try
            vlc_Actividad = CType(Me.txtActividad.Text, String).ToUpper.Trim

            If Me.DsActividades.Rows.Find(New Object() {vlc_Actividad}) Is Nothing Then

                vlo_DrNuevaFila = Me.DsActividades.NewRow

                vlo_DrNuevaFila.Item("ACTIVIDAD") = vlc_Actividad.ToUpper
                Me.DsActividades.Rows.Add(vlo_DrNuevaFila)

                Me.ActividadCaracteresRestantes = ActividadCaracteresRestantes + vlc_Actividad.Length + 1

                If Me.DsActividades IsNot Nothing AndAlso Me.DsActividades.Rows.Count > 0 Then
                    Me.rpActividades.DataSource = DsActividades
                    Me.rpActividades.DataMember = Me.DsActividades.TableName
                    Me.rpActividades.DataBind()
                    Me.rpActividades.Visible = True
                Else
                    With Me.rpActividades
                        .DataSource = Nothing
                        .DataBind()
                    End With
                    Me.rpActividades.Visible = False
                End If

                txtActividad.Text = String.Empty
            Else
                MostrarAlertaError("La actividad ya está presente en la lista")
            End If

            Me.BanderaCambios = True
            WebUtils.RegistrarScript(Me.Page, "regresarNormal", "regresarNormal();")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' borra un registro del dataset temporal de actividades
    ''' </summary>
    ''' <param name="pvc_CommandName"></param>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>04/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BorrarActividad(pvc_CommandName As String)
        Try

            Me.DsActividades.Rows.Find(New Object() {pvc_CommandName}).Delete()


            If Me.DsActividades IsNot Nothing AndAlso Me.DsActividades.Rows.Count > 0 Then
                Me.rpActividades.DataSource = Me.DsActividades
                Me.rpActividades.DataMember = Me.DsActividades.TableName
                Me.rpActividades.DataBind()
                Me.rpActividades.Visible = True
            Else
                With Me.rpActividades
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpActividades.Visible = False
            End If
            BanderaCambios = True
            WebUtils.RegistrarScript(Me.Page, "regresarNormal", "regresarNormal();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' borra un registro del dataset temporal de actividades
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>04/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ModificarActividad()
        Dim vlo_DrNuevaFila As DataRow

        Try
            'Obtiene los datos del campo de texto
            Dim vlc_Actividad As String
            vlc_Actividad = CType(Me.txtActividad.Text, String).ToUpper

            'Si La actividad a la que se le dio click es igual a la ingresada muestra mensaje de error
            If Me.Actividad <> vlc_Actividad Then
                vlo_DrNuevaFila = Me.DsActividades.NewRow

                vlo_DrNuevaFila.Item("ACTIVIDAD") = vlc_Actividad.ToUpper
                Me.DsActividades.Rows.Add(vlo_DrNuevaFila)

                'Borra la actividad anterior
                Me.DsActividades.Rows.Find(New Object() {Actividad}).Delete()

                If Me.DsActividades IsNot Nothing AndAlso Me.DsActividades.Rows.Count > 0 Then
                    Me.rpActividades.DataSource = Me.DsActividades
                    Me.rpActividades.DataMember = Me.DsActividades.TableName
                    Me.rpActividades.DataBind()
                    Me.rpActividades.Visible = True
                Else
                    With Me.rpActividades
                        .DataSource = Nothing
                        .DataBind()
                    End With
                    Me.rpActividades.Visible = False
                End If

                txtActividad.Text = String.Empty
            Else
                MostrarAlertaError("La actividad no ha sido modificada.")
            End If

            BanderaCambios = True
            WebUtils.RegistrarScript(Me.Page, "regresarNormal", "regresarNormal();")

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
    ''' <creationDate>04/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaTipoArchivo(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlTipoArchivo.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, String.Empty, True, 1,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                'drop down list de tipos de documento
                With Me.ddlTipoArchivo
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_TIPO_DOCUMENTO.DESCRIPCION
                    .DataValueField = Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO
                    .DataBind()
                End With

                Me.DsTipoArchivo = vlo_DsDatos
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
    ''' Agrega un nuevo adjunto al dataset 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>04/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregarArchivo()
        Dim vlo_DrFila As Data.DataRow


        Try
            If ifInfo.HasFile Then
                Dim vlo_buf(ifInfo.FileBytes.Length) As Byte
                ifInfo.FileContent.Read(vlo_buf, 0, ifInfo.FileBytes.Length)
                vlo_DrFila = Me.DsAdjuntosInsert.Tables(0).NewRow
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.NOMBRE_ARCHIVO)) = ifInfo.FileName
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESCRIPCION)) = Me.txtDescripcionArchivo.Text
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ARCHIVO)) = vlo_buf
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.USUARIO)) = Me.Usuario.UserName
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_TIPO_DOCUMENTO)) = Me.ddlTipoArchivo.SelectedValue
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESC_TIPO_DOCUMENTO)) = Me.ddlTipoArchivo.SelectedItem
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)) = Me.IdOrdenTrabajo
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_UBICACION)) = Me.IdUbicacion
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ETAPA_ORDEN_TRABAJO)) = EtapasOrdenTrabajo.ANTEPROYECTO
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

                BanderaCambios = True
                WebUtils.RegistrarScript(Me.Page, "regresarNormal", "regresarNormal();")
            End If
            ifInfo.FileContent.Close()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Carga los archivos adjuntos insertados por el usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargaDsAdjuntos()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsAdjuntos As Data.DataSet
        Dim vlo_NuevaFila As DataRow

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsAdjuntos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DOCUMENTO_ANTEPROYECT_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}'",
                Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.VERSION, Me.VersionActual,
                Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_UBICACION, Me.IdUbicacion,
                Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo),
                String.Empty,
                False,
                0,
                0)

            Me.DsAdjuntosInsert = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("1 = 0"),
                String.Empty,
                False,
                0,
                0)

            Me.DsArchivosBorrados = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("1 = 0"),
                String.Empty,
                False,
                0,
                0)


            If vlo_DsAdjuntos.Tables.Count > 0 AndAlso vlo_DsAdjuntos.Tables(0).Rows.Count > 0 Then
                For Each vlo_fila As DataRow In vlo_DsAdjuntos.Tables(0).Rows
                    Select Case vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_TIPO_DOCUMENTO)
                        Case TipoDocumento.AVAL_PLANTA_FISICA
                            Me.ArchivoPlantaFisica = New EntOttAdjuntoOrdenTrabajo
                            Me.ArchivoPlantaFisica.IdAdjuntoOrdenTrabajo = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ADJUNTO_ORDEN_TRABAJO)
                            Me.ArchivoPlantaFisica.IdEtapaOrdentrabajo = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ETAPA_ORDEN_TRABAJO)
                            Me.ArchivoPlantaFisica.IdOrdenTrabajo = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ORDEN_TRABAJO)
                            Me.ArchivoPlantaFisica.IdTipoDocumento = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_TIPO_DOCUMENTO)
                            Me.ArchivoPlantaFisica.IdUbicacion = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_UBICACION)
                            Me.ArchivoPlantaFisica.NombreArchivo = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.NOMBRE_ARCHIVO)
                            Me.ArchivoPlantaFisica.Existe = True
                            Me.lnkArchivoPlantaFisica.Text = Me.ArchivoPlantaFisica.NombreArchivo
                            Me.fuPlantaFisica.Visible = False
                            Me.rfvFuPlantaFisica.Enabled = False
                            Me.btnEliminarArchivoPlanta.Visible = True
                            Me.lnkArchivoPlantaFisica.Visible = True
                        Case TipoDocumento.AVAL_FORESTA
                            Me.ArchivoForesta = New EntOttAdjuntoOrdenTrabajo
                            Me.ArchivoForesta.IdAdjuntoOrdenTrabajo = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ADJUNTO_ORDEN_TRABAJO)
                            Me.ArchivoForesta.IdEtapaOrdentrabajo = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ETAPA_ORDEN_TRABAJO)
                            Me.ArchivoForesta.IdOrdenTrabajo = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ORDEN_TRABAJO)
                            Me.ArchivoForesta.IdTipoDocumento = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_TIPO_DOCUMENTO)
                            Me.ArchivoForesta.IdUbicacion = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_UBICACION)
                            Me.ArchivoForesta.NombreArchivo = vlo_fila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.NOMBRE_ARCHIVO)
                            Me.ArchivoForesta.Existe = True
                            Me.lnkArchivoForesta.Text = Me.ArchivoForesta.NombreArchivo
                            Me.fuForesta.Visible = False
                            Me.rfvFuForesta.Enabled = False
                            Me.lnkArchivoForesta.Visible = True
                            Me.btnEliminarArchivoForesta.Visible = True
                        Case Else
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

                    End Select
                Next

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
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
            If vlo_DsAdjuntos IsNot Nothing Then
                vlo_DsAdjuntos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Se encarga de cargar la información de la variable actual del anteproyecto antes de ser enviada a guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ContruirRegistro()
        Dim vln_cantidad As Integer
        Dim vln_TiempoRespuesta As Integer
        Dim vln_UnidadTiempo As Integer
        Dim vlc_Actividades As StringBuilder = New StringBuilder

        For Each vlo_fila As DataRow In Me.DsActividades.Rows
            vlc_Actividades.Append(vlo_fila.Item("ACTIVIDAD"))
            vlc_Actividades.Append("¬")
        Next

        If String.IsNullOrWhiteSpace(Me.txtCantidad.Text) Then
            vln_cantidad = 0
        Else
            vln_cantidad = CInt(Me.txtCantidad.Text)
        End If

        If String.IsNullOrWhiteSpace(Me.txtTiempoRespuesta.Text) Then
            vln_TiempoRespuesta = 0
        Else
            vln_TiempoRespuesta = CInt(Me.txtTiempoRespuesta.Text)
        End If

        If String.IsNullOrWhiteSpace(Me.ddlUnidadTiempoRespuesta.SelectedValue) Then
            vln_UnidadTiempo = 0
        Else
            vln_UnidadTiempo = CInt(Me.ddlUnidadTiempoRespuesta.SelectedValue)
        End If

        With Me.Anteproyecto
            .Descripcion = Me.txtDescripcion.Text
            .Cantidad = vln_cantidad
            .UnidadMedida = Me.ddlUnidadMedida.SelectedValue
            .AvalPlantaFisica = IIf(Me.chkPlantaFisica.Checked, 1, 0)
            .AvalForesta = IIf(Me.chkForesta.Checked, 1, 0)
            .TiempoRespuesta = vln_TiempoRespuesta
            .IdUnidadTiempo = Me.ddlUnidadTiempoRespuesta.SelectedValue
            If vlc_Actividades.ToString <> String.Empty Then
                .ActividadesContempladas = vlc_Actividades.ToString.Substring(0, vlc_Actividades.Length - 1)
            Else
                .ActividadesContempladas = String.Empty
            End If

            .Editable = IIf(Me.Editable, 1, 0)
            .FechaEnvia = Now
            .IdOrdenTrabajo = Me.IdOrdenTrabajo
            .IdUbicacion = Me.IdUbicacion
            .Version = Me.VersionActual
            .Usuario = Me.Usuario.UserName
        End With
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


    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal o el numero de empleado que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>02/03/2016</creationDate>
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
    ''' Almacena la información obtenida del usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>07/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function GuardarAnteproyecto() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            'Si hay cambios nuevos que guardar 
            '           If BanderaCambios Then
            ContruirRegistro()

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ANTEPROYECTO_GuardarVersion(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                 Me.DsAdjuntosInsert, Me.Anteproyecto, Me.ArchivoPlantaFisica, Me.ArchivoForesta, DsArchivosBorrados) > 0
            '            End If

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
    ''' <creationDate>11/03/2016</creationDate>
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
    ''' Genera una nueva versión del anteproyecto copiando todos los elementos de la ultima versión no editable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function GenerarNuevaVersion() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.Anteproyecto.Usuario = Me.Usuario.UserName
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ANTEPROYECTO_NuevaVersion(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.Anteproyecto) > 0

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
