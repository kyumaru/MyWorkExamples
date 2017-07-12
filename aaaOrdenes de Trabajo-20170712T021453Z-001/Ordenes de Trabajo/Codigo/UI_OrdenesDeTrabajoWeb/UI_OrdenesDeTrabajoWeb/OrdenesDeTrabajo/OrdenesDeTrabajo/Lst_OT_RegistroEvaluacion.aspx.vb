Imports System.Data
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Lst_OT_RegistroEvaluacion
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Parámetro del sistema, para habilitar tercera etapa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>28/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Property ParametroUbicacionTerceraEtapa As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Get
            Return CType(ViewState("ParametroUbicacionTerceraEtapa"), Wsr_OT_Catalogos.EntOtpParametroUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtpParametroUbicacion)
            ViewState("ParametroUbicacionTerceraEtapa") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad del dataset de funcionarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property DsFuncionarios As DataTable
        Get
            Return CType(ViewState("DsFuncionarios"), DataTable)
        End Get
        Set(value As DataTable)
            ViewState("DsFuncionarios") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad del dataset de funcionarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>30/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property DsFuncionariosReal As DataTable
        Get
            Return CType(ViewState("DsFuncionariosReal"), DataTable)
        End Get
        Set(value As DataTable)
            ViewState("DsFuncionariosReal") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima direccion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortDirection As SortDirection
        Get
            If ViewState("UltimoSortDirection") Is Nothing Then
                Return SortDirection.Ascending
            End If
            Return CType(ViewState("UltimoSortDirection"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("UltimoSortDirection") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el valor de los funcionarios para recursos ejecución
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsFuncionariosEjecucion As DataTable
        Get
            Return CType(ViewState("DsFuncionariosEjecucion"), DataTable)
        End Get
        Set(value As DataTable)
            ViewState("DsFuncionariosEjecucion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>01/02/2016</creationDate>
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
    ''' Propiedad para la solicitud de materiales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property SolicitudMaterial As EntOttSolicitudMaterial
        Get
            Return CType(ViewState("SolicitudMaterial"), EntOttSolicitudMaterial)
        End Get
        Set(value As EntOttSolicitudMaterial)
            ViewState("SolicitudMaterial") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la lista de funcionarios de este sector o taller
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsFuncionariosTaller As DataSet
        Get
            Return CType(ViewState("DsFuncionariosTaller"), DataSet)
        End Get
        Set(value As DataSet)
            ViewState("DsFuncionariosTaller") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la lista de funcionarios de este sector o taller
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsFuncionariosTallerEjecucion As DataSet
        Get
            Return CType(ViewState("DsFuncionariosTallerEjecucion"), DataSet)
        End Get
        Set(value As DataSet)
            ViewState("DsFuncionariosTallerEjecucion") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>27/01/2016</creationDate>
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
    ''' <creationDate>27/01/2016</creationDate>
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
    ''' <creationDate>27/01/2016</creationDate>
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
    ''' Almacena el año de la orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>27/01/2016</creationDate>
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
    ''' Determina cuando se desea guardar datos para la ejecucion, 
    ''' ademas de controlar cuando se deshabilitarán botones y validadores para la pestaña de evaluación.
    ''' Esto depende del estado en que se encuentre la orden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>27/01/2016</creationDate>
    Public Property ParaEjecucion As Boolean
        Get
            If ViewState("ParaEjecucion") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("ParaEjecucion"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("ParaEjecucion") = value
        End Set
    End Property

    ''' <summary>
    ''' Determina si se debe modificar la ot
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/03/2016</creationDate>
    Private Property ModificarOrdenTrabajo As Boolean
        Get
            Return CType(ViewState("ModificarOrdenTrabajo"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("ModificarOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez García</author>
    ''' <creationDate>24/2/2016</creationDate>
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
    ''' Propiedad para la Categoria del material
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property Categoria As Wsr_OT_Catalogos.EntOtmCategoriaMaterial
        Get
            Return CType(ViewState("Categoria"), Wsr_OT_Catalogos.EntOtmCategoriaMaterial)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmCategoriaMaterial)
            ViewState("Categoria") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de materiales a ingresar eliminar o modificar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsMaterialesInsert As Data.DataSet
        Get
            Return CType(ViewState("DsMaterialesInsert"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsMaterialesInsert") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la Categoria del material
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property SubCategoria As Wsr_OT_Catalogos.EntOtmSubcategoriaMaterial
        Get
            Return CType(ViewState("SubCategoria"), Wsr_OT_Catalogos.EntOtmSubcategoriaMaterial)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmSubcategoriaMaterial)
            ViewState("SubCategoria") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la unidad de medida del material
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property UnidadMedida As Wsr_OT_Catalogos.EntOtmUnidadMedida
        Get
            Return CType(ViewState("UnidadMedida"), Wsr_OT_Catalogos.EntOtmUnidadMedida)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmUnidadMedida)
            ViewState("UnidadMedida") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property MontoTotal As Double
        Get
            If ViewState("MontoTotal") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("MontoTotal"), Double)
        End Get
        Set(value As Double)
            ViewState("MontoTotal") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el costo total de la orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property CostoTotalOT As Integer
        Get
            If ViewState("CostoTotalOT") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("CostoTotalOT"), Integer)
        End Get
        Set(value As Integer)
            ViewState("CostoTotalOT") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual()
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click en el boton de agregar para la evaluacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarFuncionario_Click(sender As Object, e As EventArgs) Handles btnAgregarFuncionario.Click
        Try
            AgregaFuncionariosDataTable()
            '  Me.liTiempo.Visible = True
            ActivarEvaluacionRecursoHumano()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click en el boton de agregar para la ejecución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarFuncionarioEjecucion_Click(sender As Object, e As EventArgs) Handles btnAgregarFuncionarioEjecucion.Click
        Try
            AgregaFuncionariosEjecucion()
            Me.liTiempoEjecucion.Visible = True
            ActivarEvaluacionRecursoHumanoEjecucion()
            'Me.ddlFuncionarioEjecucion.SelectedValue = String.Empty
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
    ''' <creationDate>31/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            BorrarFuncionario(CType(sender, ImageButton).CommandName)
            ActivarEvaluacionRecursoHumano()
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
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrarEjecución_Click(sender As Object, e As ImageClickEventArgs)
        Try
            BorrarFuncionarioEjecucion(CType(sender, ImageButton).CommandName)
            ActivarEvaluacionRecursoHumanoEjecucion()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta se carga la lista de funcionarios, por cada registro del
    ''' repeater se asigna un identificador unico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>31/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpFuncionarios_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpFuncionarios.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        If Not Me.ParaEjecucion AndAlso Me.OrdenTrabajo.EstadoOrdenTrabajo <> EstadoOrden.PARA_IMPRESION Then
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
    ''' evento que se ejecuta se carga la lista de funcionarios, por cada registro del
    ''' repeater se asigna un identificador unico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpFuncionariosEjecucion_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpFuncionariosEjecucion.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        If Me.OrdenTrabajo.EstadoOrdenTrabajo <> EstadoOrden.EN_PROCESO AndAlso Me.OrdenTrabajo.EstadoOrdenTrabajo <> EstadoOrden.PARA_IMPRESION Then
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
    ''' Al dar click en el botón guardar se almacenaran los datos indicados por el coordinador
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    ''' <remarks></remarks>
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            'Se cambia el estado de la orden para que quede registrada como evaluacion
            'De esta forma se asegura que la proxima vez que entre tendrá que registrar los recursos para la ejecución
            If IsValid Then
                Me.ModificarOrdenTrabajo = False
                If Guardar() Then
                    WebUtils.RegistrarScript(Me.Page, "mostrarAlertaEvaluacionExitosa", "mostrarAlertaEvaluacionExitosa();")
                Else
                    MostrarAlertaError("No ha sido posible guardar la información del registro")
                End If
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
    End Sub

    ''' <summary>
    ''' Al dar click en Guardar y Finalizar se guardaran los datos y se cambiara el estado para que la OT quede lista para ejecutarse. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>01/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardarYFinalizar_Click(sender As Object, e As EventArgs) Handles btnGuardarYFinalizar.Click
        Try

            If IsValid Then

                If Not Me.ParaEjecucion Then
                    Me.OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PARA_IMPRESION
                Else
                    Me.OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PENDIENTE_APROBACION_REQUISICION
                End If

                Me.ModificarOrdenTrabajo = True

                If Me.ParaEjecucion Then

                    If Me.SolicitudMaterial.Existe Then

                        If Me.SolicitudMaterial.NoRequiereMaterial = 1 Or Me.chkRequierMaterial.Checked Then
                            Me.OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EN_PROCESO
                            If Guardar() Then
                                WebUtils.RegistrarScript(Me.Page, "mostrarAlertaEvaluacionExitosa", "mostrarAlertaEvaluacionExitosa();")
                            Else
                                MostrarAlertaError("No ha sido posible actualizar la información")
                            End If
                        Else

                            If Me.DsMaterialesInsert IsNot Nothing AndAlso Me.DsMaterialesInsert.Tables(0).Rows.Count > 0 Then
                                If Guardar() Then
                                    WebUtils.RegistrarScript(Me.Page, "mostrarAlertaEvaluacionExitosa", "mostrarAlertaEvaluacionExitosa();")
                                Else
                                    MostrarAlertaError("No ha sido posible actualizar la información")
                                End If
                            Else
                                MostrarAlertaError("Debe ingresar al menos un material a la solicitud.")
                            End If

                        End If

                    Else
                        MostrarAlertaError("Debe completar los datos de la solicitud de materiales.")
                    End If
                Else

                    If Guardar() Then
                        WebUtils.RegistrarScript(Me.Page, "mostrarAlertaEvaluacionExitosa", "mostrarAlertaEvaluacionExitosa();")
                    Else
                        MostrarAlertaError("No ha sido posible actualizar la información")
                    End If
                End If

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
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs)
        Try
            Response.Redirect("Lst_OT_GestionOrdenTrabajoCordinadorSectorTaller.aspx")

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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkEjecutarBusquedaMaterial_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusquedaMaterial.Click
        Try
            Me.ctrl_Materiales.mostrarAlmacenPartida = False
            WebUtils.RegistrarScript(Me.Page, "mostrarPopUpBusquedaMaterial", "javascript:mostrarPopUp('#PopUpBusquedaMateriales');cargarLupa();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvc_IdMaterial"></param>
    ''' <param name="pvc_Descripcion"></param>
    ''' <param name="pvn_IdCategoria"></param>
    ''' <param name="pvn_idSubcategoria"></param>
    ''' <param name="pvn_CostoPromedio"></param>
    ''' <param name="pvn_UnidadMedida"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ctrl_Materiales_Aceptar(pvc_IdMaterial As Integer, pvc_Descripcion As String, pvn_IdCategoria As Integer, pvn_idSubcategoria As Integer, pvn_CostoPromedio As Integer, pvn_UnidadMedida As Integer) Handles ctrl_Materiales.Aceptar
        CargarCategoria(String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_MATERIAL.ID_CATEGORIA_MATERIAL, pvn_IdCategoria))
        CargarSubCategoria(String.Format("{0} = {1}", Modelo.OTM_SUBCATEGORIA_MATERIAL.ID_SUBCATEGORIA_MATERIAL, pvn_idSubcategoria))
        CargarUnidadMedida(String.Format("{0} = {1}", Modelo.OTM_UNIDAD_MEDIDA.ID_UNIDAD_MEDIDA, pvn_UnidadMedida))
        Me.txtCodigo.Text = pvc_IdMaterial.ToString

        Me.WucDatosMaterial.AsignaDescripcion(pvc_Descripcion)
        Me.WucDatosMaterial.AsignaCategoria(Me.Categoria.Descripcion)
        Me.WucDatosMaterial.AsignaMontoPromedio(String.Format("{0} {1}", pvn_CostoPromedio.ToString("N2"), "Colones"))
        Me.WucDatosMaterial.AsignaSubCategoria(Me.SubCategoria.Descripcion)
        Me.WucDatosMaterial.AsignaUnidadMedida(Me.UnidadMedida.Descripcion)

        upControlDatosMaterial.Visible = True
        upTxtCodigo.Update()
        upControlDatosMaterial.Update()

        WebUtils.RegistrarScript(Me.Page, "InicializarFormulario", "javascript:inicializarFormulario();ocultarFiltroMateriales();")
        WebUtils.RegistrarScript(Me, "cargarTootipDetalles", "cargarTootipDetalles();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub txtCodigo_TextChanged(sender As Object, e As EventArgs) Handles txtCodigo.TextChanged
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmMaterial As Wsr_OT_Catalogos.EntOtmMaterial
        Dim pvc_CondicionBusquedas As String

        Try
            'instanciar y configurar objetos
            vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
            vlo_Ws_OT_Catalogos.Timeout = -1
            vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

            Me.WucDatosMaterial.AsignaDescripcion(String.Empty)
            Me.WucDatosMaterial.AsignaCategoria(String.Empty)
            Me.WucDatosMaterial.AsignaMontoPromedio(String.Empty)
            Me.WucDatosMaterial.AsignaSubCategoria(String.Empty)
            Me.WucDatosMaterial.AsignaUnidadMedida(String.Empty)

            If Me.txtCodigo.Text <> "" Then
                pvc_CondicionBusquedas = String.Format("{0} = '{1}'", Modelo.OTM_MATERIAL.ID_MATERIAL, Me.txtCodigo.Text)
                vlo_EntOtmMaterial = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ObtenerRegistro(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                                                                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                                                                   pvc_CondicionBusquedas)
                If vlo_EntOtmMaterial IsNot Nothing AndAlso vlo_EntOtmMaterial.Existe Then

                    CargarCategoria(String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_MATERIAL.ID_CATEGORIA_MATERIAL, vlo_EntOtmMaterial.IdCategoriaMaterial))
                    CargarSubCategoria(String.Format("{0} = {1}", Modelo.OTM_SUBCATEGORIA_MATERIAL.ID_SUBCATEGORIA_MATERIAL, vlo_EntOtmMaterial.IdSubcategoriaMaterial))
                    CargarUnidadMedida(String.Format("{0} = {1}", Modelo.OTM_UNIDAD_MEDIDA.ID_UNIDAD_MEDIDA, vlo_EntOtmMaterial.IdUnidadMedida))
                    Me.txtCodigo.Text = vlo_EntOtmMaterial.IdMaterial.ToString

                    Me.WucDatosMaterial.AsignaDescripcion(vlo_EntOtmMaterial.Descripcion)
                    Me.WucDatosMaterial.AsignaCategoria(Me.Categoria.Descripcion)
                    Me.WucDatosMaterial.AsignaMontoPromedio(String.Format("{0} {1}", vlo_EntOtmMaterial.CostoPromedio.ToString("N2"), "Colones"))
                    Me.WucDatosMaterial.AsignaSubCategoria(Me.SubCategoria.Descripcion)
                    Me.WucDatosMaterial.AsignaUnidadMedida(Me.UnidadMedida.Descripcion)
                    upControlDatosMaterial.Visible = True
                Else
                    Me.WucDatosMaterial.AsignaDescripcion(String.Empty)
                    Me.WucDatosMaterial.AsignaCategoria(String.Empty)
                    Me.WucDatosMaterial.AsignaMontoPromedio(String.Empty)
                    Me.WucDatosMaterial.AsignaSubCategoria(String.Empty)
                    Me.WucDatosMaterial.AsignaUnidadMedida(String.Empty)
                    upControlDatosMaterial.Visible = False
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoEncontrado();")
                End If

            Else
                upControlDatosMaterial.Visible = False

            End If
            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")
            WebUtils.RegistrarScript(Me, "cargarTootipDetalles", "cargarTootipDetalles();")
        Catch ex As Exception
            Throw
        End Try
    End Sub


    ''''''''''''''''''''''''''''''''''''''''-----------------------------------------'''''''''''''''''''''''''''''''''''''''

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''MATERIALES'''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''-----------------------------------------'''''''''''''''''''''''''''''''''''''''

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el boton modificar del listato de encargados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibModificarMaterial_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_fila() As DataRow

        Try
            vlo_fila = Me.DsMaterialesInsert.Tables(0).Select(String.Format("{0} = {1}", Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL, CType(sender, ImageButton).CommandArgument))
            If vlo_fila.Length > 0 Then
                Me.txtCodigo.Text = vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL)

                Me.WucDatosMaterial.AsignaCantidad(vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA))
                Me.WucDatosMaterial.AsignaDetalle(vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE))

                txtCodigo_TextChanged(sender, e)
                upTxtCodigo.Update()
                upControlDatosMaterial.Update()
                btnAgregarMaterial.Visible = False
                btnModificarMaterial.Visible = True
                btnModificarMaterial.CommandArgument = CType(sender, ImageButton).CommandArgument
                btnCancelarMaterial.Visible = True
                WebUtils.RegistrarScript(Me.Page, "InhabilitarCodigo", "javascript:InhabilitarCodigo();")
                Me.txtCodigo.ReadOnly = True
            End If
            WebUtils.RegistrarScript(Me, "cargarTootipDetalles", "cargarTootipDetalles();")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrarMaterial_Click(sender As Object, e As ImageClickEventArgs)

        Try

            If Not BorrarDetalleMaterial(CType(CType(sender, ImageButton).CommandArgument, Integer)) Then
                MostrarAlertaError("No ha sido posible borrar el material seleccionado.")
            End If

            CargarListaMateriales(String.Format("{0} {1}", Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL, Ordenamiento.ASCENDENTE))
            WebUtils.RegistrarScript(Me.Page, "cargarLupa2", "javascript:cargarLupa();")
            WebUtils.RegistrarScript(Me, "cargarTootipDetalles", "cargarTootipDetalles();")
        Catch ex As Exception
            If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso
                CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                Dim vlo_TallerCapacitacionException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                MostrarAlertaError(vlo_TallerCapacitacionException.Message)
            Else
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se d click sobre el checkbox de requerie materiales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub chkRequierMaterial_CheckedChanged(sender As Object, e As EventArgs) Handles chkRequierMaterial.CheckedChanged
        Try
            If Me.chkRequierMaterial.Checked Then
                Me.panelMateriales.Visible = False
                WebUtils.RegistrarScript(Me.Page, "mostrarAlertNoRequiereMaterial", "javascript:mostrarAlertNoRequiereMaterial();")
            Else
                Me.panelMateriales.Visible = True
            End If
            WebUtils.RegistrarScript(Me.Page, "cargarLupa2", "javascript:cargarLupa();")
            WebUtils.RegistrarScript(Me, "cargarTootipDetalles", "cargarTootipDetalles();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados de la 
    ''' tabla del listado, carga la lista de registros en orden relacionado a la columna seleccionada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpPedidos_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarListaMateriales(ObtenerExpresionDeOrdenamiento(e.CommandName))
            WebUtils.RegistrarScript(Me, "cargarTootipDetalles", "cargarTootipDetalles();")
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarMaterial_Click(sender As Object, e As EventArgs) Handles btnAgregarMaterial.Click
        Dim vlo_NuevaFila As Data.DataRow
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtfInventario As Wsr_OT_Catalogos.EntOtfInventario
        Dim vlo_almacen As Wsr_OT_Catalogos.EntOtmAlmacenBodega
        Dim vlc_monto As String()

        Try

            'instanciar y configurar objetos
            vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
            vlo_Ws_OT_Catalogos.Timeout = -1
            vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

            vlo_almacen = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'",
                              Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, Me.IdUbicacion,
                              Modelo.OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))

            If vlo_almacen.Existe Then

                vlo_EntOtfInventario = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_almacen.IdAlmacenBodega,
                                  Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, Me.IdUbicacion,
                                  Modelo.OTF_INVENTARIO.ID_MATERIAL, Me.txtCodigo.Text))

                vlo_NuevaFila = DsMaterialesInsert.Tables(0).NewRow

                vlc_monto = Me.WucDatosMaterial.RetornaMontoPromedio.Split(" ")

                If (Me.WucDatosMaterial.RetornaCantidad <> String.Empty) AndAlso (CType(Me.WucDatosMaterial.RetornaCantidad, Double) > 0) Then
                    '  If vlo_EntOtfInventario.CantidadDisponible >= CInt(Me.WucDatosMaterial.RetornaCantidad) Then
                    AgregarDetalleMaterial(vlo_EntOtfInventario.IdAlmacenBodega)

                    vlo_NuevaFila(Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL) = Me.txtCodigo.Text
                    vlo_NuevaFila(Modelo.V_OTT_DETALLE_MATERIALLST.DESCRIPCION) = Me.WucDatosMaterial.RetornaDescripcion
                    vlo_NuevaFila(Modelo.V_OTT_DETALLE_MATERIALLST.DETALLE) = Me.WucDatosMaterial.RetornaDetalle
                    vlo_NuevaFila(Modelo.V_OTT_DETALLE_MATERIALLST.DISPONIBLE_ALMACEN_SOLICITUD) = vlo_EntOtfInventario.CantidadDisponible
                    vlo_NuevaFila(Modelo.V_OTT_DETALLE_MATERIALLST.CANTIDAD_SOLICITADA) = CType(Me.WucDatosMaterial.RetornaCantidad, Double)
                    vlo_NuevaFila(Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO) = CType(vlc_monto(0), Double) * CType(Me.WucDatosMaterial.RetornaCantidad, Double)

                    DsMaterialesInsert.Tables(0).Rows.Add(vlo_NuevaFila)

                    Me.txtCodigo.Text = String.Empty
                    Me.WucDatosMaterial.AsignaDescripcion(String.Empty)
                    Me.WucDatosMaterial.AsignaCategoria(String.Empty)
                    Me.WucDatosMaterial.AsignaMontoPromedio(String.Empty)
                    Me.WucDatosMaterial.AsignaSubCategoria(String.Empty)
                    Me.WucDatosMaterial.AsignaUnidadMedida(String.Empty)
                    Me.WucDatosMaterial.AsignaDetalle(String.Empty)
                    Me.WucDatosMaterial.AsignaCantidad(String.Empty)

                    upTxtCodigo.Update()
                    upControlDatosMaterial.Update()

                    CargarListaMateriales(String.Format("{0} {1}", Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL, Ordenamiento.ASCENDENTE))

                    Me.upControlDatosMaterial.Visible = False
                    'Else
                    '  mostrarAlertSinCantidadDisponible()
                    '  End If

                Else
                    WebUtils.RegistrarScript(Me, "mostrarAlertCantidadCero", "mostrarAlertCantidadCero();")
                End If
            End If

            WebUtils.RegistrarScript(Me, "cargarLupaLupa", "cargarLupa();")
            WebUtils.RegistrarScript(Me, "cargarTootipDetalles", "cargarTootipDetalles();")
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnModificarMaterial_Click(sender As Object, e As EventArgs) Handles btnModificarMaterial.Click
        Dim vln_IdDetalleMaterial As Integer
        Dim vlo_fila() As DataRow
        Dim vlo_EntOtfInventario As Wsr_OT_Catalogos.EntOtfInventario
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_almacen As Wsr_OT_Catalogos.EntOtmAlmacenBodega
        Dim vlc_monto As String()

        'instanciar y configurar objetos
        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_almacen = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'",
                              Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, Me.IdUbicacion,
                              Modelo.OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))

            'instanciar y configurar objetos
            vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
            vlo_Ws_OT_Catalogos.Timeout = -1
            vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

            vlo_EntOtfInventario = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_almacen.IdAlmacenBodega,
                                  Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, Me.IdUbicacion,
                                  Modelo.OTF_INVENTARIO.ID_MATERIAL, Me.txtCodigo.Text))

            '  If vlo_EntOtfInventario.CantidadDisponible >= CInt(Me.WucDatosMaterial.RetornaCantidad) Then
            vlc_monto = Me.WucDatosMaterial.RetornaMontoPromedio.Split(" ")

            vln_IdDetalleMaterial = btnModificarMaterial.CommandArgument
            vlo_fila = Me.DsMaterialesInsert.Tables(0).Select(String.Format("{0} = {1}", Modelo.V_OTT_DETALLE_MATERIALLST.ID_DETALLE_MATERIAL, vln_IdDetalleMaterial))
            If vlo_fila.Length > 0 Then

                If (Me.WucDatosMaterial.RetornaCantidad <> String.Empty) AndAlso (CType(Me.WucDatosMaterial.RetornaCantidad, Double) > 0) Then
                    ModificarDetalleMaterial(vln_IdDetalleMaterial, Me.txtCodigo.Text, Me.WucDatosMaterial.RetornaDetalle, CType(Me.WucDatosMaterial.RetornaCantidad, Double), vlo_fila(0)(Modelo.V_OTT_DETALLE_MATERIALLST.TIME_STAMP), vlo_EntOtfInventario.IdAlmacenBodega)
                    CargarListaMateriales(String.Format("{0} {1}", Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL, Ordenamiento.ASCENDENTE))
                    Me.txtCodigo.ReadOnly = False
                    btnCancelarMaterial_Click(sender, e)
                    Me.btnModificarMaterial.Visible = False
                    Me.btnCancelarMaterial.Visible = False
                    Me.btnAgregarMaterial.Visible = True
                Else
                    WebUtils.RegistrarScript(Me, "mostrarAlertCantidadCero", "mostrarAlertCantidadCero();")
                End If
            End If

            WebUtils.RegistrarScript(Me.Page, "HabilitarCodigo", "javascript:HabilitarCodigo();cargarLupa();")
            WebUtils.RegistrarScript(Me, "cargarTootipDetalles", "cargarTootipDetalles();")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnCancelarMaterial_Click(sender As Object, e As EventArgs) Handles btnCancelarMaterial.Click
        Me.txtCodigo.Text = String.Empty
        Me.WucDatosMaterial.AsignaDetalle(String.Empty)
        Me.WucDatosMaterial.AsignaCantidad(String.Empty)

        txtCodigo_TextChanged(sender, e)
        upTxtCodigo.Update()
        upControlDatosMaterial.Update()
        Me.btnAgregarMaterial.Visible = True
        Me.btnCancelarMaterial.Visible = False
        Me.btnModificarMaterial.Visible = False
        WebUtils.RegistrarScript(Me.Page, "HabilitarCodigo", "javascript:HabilitarCodigo();cargarLupa();")
        WebUtils.RegistrarScript(Me, "cargarTootipDetalles", "cargarTootipDetalles();")
        Me.txtCodigo.ReadOnly = False
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpPedidos_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpPedidos.ItemDataBound
        Dim vlo_IdBorrar As ImageButton
        Dim vlo_Modificar As ImageButton

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_IdBorrar = e.Item.FindControl("ibBorrarMaterial")
            If vlo_IdBorrar IsNot Nothing Then
                vlo_IdBorrar.Attributes.Add("data-uniqueid", vlo_IdBorrar.UniqueID)
            End If

            vlo_Modificar = e.Item.FindControl("ibModificarMaterial")
            If vlo_Modificar IsNot Nothing Then
                vlo_Modificar.Attributes.Add("data-uniqueid", vlo_Modificar.UniqueID)
            End If

        End If
    End Sub

    ''''''''''''''''''''''''''''''''''''''''-----------------------------------------'''''''''''''''''''''''''''''''''''''''

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''FIN MATERIALES'''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''''''''''''''''''''''''''''''''''''''''-----------------------------------------'''''''''''''''''''''''''''''''''''''''

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Carga las categorias deacuerdo a la condición indicada
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>6/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarCategoria(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.Categoria = vlo_Ws_OT_Catalogos.OTM_CATEGORIA_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga las categorias deacuerdo a la condición indicada
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarSubCategoria(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.SubCategoria = vlo_Ws_OT_Catalogos.OTM_SUBCATEGORIA_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga la unidad de medida del material específicado
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarUnidadMedida(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            UnidadMedida = vlo_Ws_OT_Catalogos.OTM_UNIDAD_MEDIDA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Inicializa el formulario 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>01/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Try

            InicializarControl()
            inicializarSetDatos()
            CargarUnidades(String.Format("{0} = '{1}'", Modelo.V_OTM_UNIDAD_TIEMPOLST.ESTADO, Estado.ACTIVO), String.Empty, 1)
            'Cargar la lista de funcionarios por sector o taller
            CargarListaFuncionarios(String.Format("{0} = {1} AND {2} = '{3}'",
                            Modelo.OTF_OPERARIO_AREA.ID_SECTOR_TALLER,
                            IdSectorTaller,
                            Modelo.V_OTF_OPERARIO_AREALST.CATEGORIA_LABORAL,
                            Area.OPERARIO), String.Empty, 1)
            CargarOrdenTrabajo(Me.IdUbicacion, Me.IdOrdenTrabajo)
            ConfigurarControles()
            Me.ctrl_Materiales.mostrarAlmacenPartida = False
            Me.ctrl_Materiales.Inicializar()
            CargarListaMateriales(String.Format("{0} {1}", Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL, Ordenamiento.ASCENDENTE))
            Me.ParametroUbicacionTerceraEtapa = CargarParametro(Parametros.VALOR_PARA_HABILITAR_TERCERA_ETAPA_SISTEMA, Me.IdUbicacion)

            If CType(Me.ParametroUbicacionTerceraEtapa.Valor, Integer) = 0 Then
                Me.SolicitudMaterial = New EntOttSolicitudMaterial
                Me.SolicitudMaterial.IdUbicacion = Me.IdUbicacion
                Me.SolicitudMaterial.IdOrdenTrabajo = Me.IdOrdenTrabajo
                Me.SolicitudMaterial.NoRequiereMaterial = 1
                Me.SolicitudMaterial.Usuario = Me.Usuario.UserName
                Me.SolicitudMaterial.Existe = True
                Me.SolicitudMaterial.EstadoSolMaterial = EstadoSolicitudMaterial.INGRESADO_POR_COORDINADOR
                Me.txtObservacionesMaterial.Text = String.Empty
                Me.chkRequierMaterial.Checked = True
                Me.liMEEjecucion.Visible = False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>01/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Ejecuta el script para activar la pestaña de tiempo en pantalla
    ''' </summary>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>01/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ActivarEvaluacionTiempo()
        WebUtils.RegistrarScript(Me.Page, "ActivarTiempoEvaluacion", "ActivarTiempoEvaluacion();")
    End Sub

    ''' <summary>
    ''' Registra el script para activar la pestaña de recurso humano
    ''' </summary>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>01/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ActivarEvaluacionRecursoHumano()
        WebUtils.RegistrarScript(Me.Page, "ActivarRHEvaluacion", "ActivarRHEvaluacion();")
    End Sub

    ''' <summary>
    ''' Registra el script para activar la pestaña de recurso humano en ejecucion
    ''' </summary>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>01/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ActivarEvaluacionRecursoHumanoEjecucion()
        WebUtils.RegistrarScript(Me.Page, "ActivarRHEjecucion", "ActivarRHEjecucion();")
    End Sub

    ''' <summary>
    ''' Registra el script para activar la pestaña de recursos para ejecucion
    ''' </summary>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>04/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ActivarRHEjecucion()
        WebUtils.RegistrarScript(Me.Page, "ActivarRHEjecucion", "ActivarRHEjecucion();")
    End Sub

    ''' <summary>
    ''' Inicializa configuraciones para validadores y opciones del menú
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>01/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ConfigurarControles()
        Dim vlc_CondicionEvaluacion As String
        Dim vlc_CondicionEjecucion As String
        'Dependiendo del estado de la orden se muestran algunos botones EN_EVALUACION y en caso de estar EN_PROCESO no se muestra ninguno
        'Si la orden de trabajo está apenas ASIGNADA se muestra primero el listado de funcionarios para que agregue la evaluación, la ejecución no se muestra.


        'Condicion para cargar los funcionarios anteriormente ingresados
        '{0}:Columna ID_SECTOR_TALLER
        '{1}:Valor del Id sector taller
        '{2}:Columna ID_UBICACION
        '{3}:Número de ubicación deseada
        '{4}:Columna ID_ORDEN_TRABAJO
        '{5}:Id de la orden de trabajo cargada por parámetro
        '{6}:Columna ID_ETAPA_ORDEN_TRABAJO
        '{7}:Cargar solo las de EVALUACION
        vlc_CondicionEvaluacion = String.Format("{0}={1} AND {2}={3} AND {4} = '{5}' AND {6} = {7}", Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER,
                                                      Me.IdSectorTaller, Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, Me.IdUbicacion,
                                                      Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo,
                                                      Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.EVALUACION)

        Select Case Me.OrdenTrabajo.EstadoOrdenTrabajo
            Case EstadoOrden.ASIGNADA
                'Se activa la pestaña recursos para evaluacion

                Me.ParaEjecucion = False
                'Carga la pestaña de RH por defecto
                ActivarEvaluacionRecursoHumano()
                'Si ya existian datos previamente cargados
                If CargarOperariosActuales(vlc_CondicionEvaluacion) Then
                    ' CargarUnidadesTiempo(vlc_CondicionEvaluacion)
                    'Se activa la pestaña de ejecucion si existen datos guardados en la tabla
                    Me.liRecursosEjecucion.Visible = False
                    'Habilita la pestaña de tiempo
                    '  Me.liTiempo.Visible = True
                    hdfHayDatos.Value = Me.DsFuncionarios.Rows.Count
                Else
                    'La pestaña recursos para ejecución no se mostrará
                    Me.liRecursosEjecucion.Visible = False
                    '   Me.liTiempo.Visible = False
                    'Como no hay registros activa el validador

                End If

                Me.rfvFechaEfectuo.Enabled = False
                '  Me.rfvTxtTiempoReal.Enabled = False
                ' Me.rfvDdlUnidadesDeTiempo.Enabled = False
                Me.rfvTxtdpFechaPropuestaInicio.Enabled = False
                Me.rfvtxtTiempoEstimadoFinaliza.Enabled = False
                Me.rfvddlUnidadTiempoEstimado.Enabled = False
                Me.rfvddlFuncionarioEjecucion.Enabled = False
                '  Me.trFuncionariosReal.Visible = False

            Case EstadoOrden.PARA_IMPRESION

                Me.liRecursosEjecucion.Visible = False
                Me.btnAgregarFuncionario.Visible = False
                'True por que en éste punto se pueden guardar datos para la ejecución
                Me.ParaEjecucion = False
                'Se cargan ambas listas tanto las que están en evaluacion como las de ejecucion,
                'Las de evaluación serán cargadas solo para consulta
                'Me.tdBorrar.visible = False

                '     Me.txtTiempoEstimado.Enabled = False
                '   Me.ddlUnidad.Enabled = False
                '  Me.txtDPFechaPropuesta.Enabled = False


                Me.trFuncionarioEvaluacion.Visible = False
                Me.btnAgregarFuncionario.Visible = False
                Me.btnGuardar.Visible = False
                Me.btnGuardarYFinalizar.Visible = False
                'Me.trFuncionariosReal.Visible = True

                If CargarOperariosActuales(vlc_CondicionEvaluacion) Then
                    '  CargarUnidadesTiempo(vlc_CondicionEvaluacion)
                End If

                ActivarEvaluacionRecursoHumano()


            Case EstadoOrden.EN_EVALUACION


                Me.liRecursosEjecucion.Visible = True
                Me.btnAgregarFuncionario.Visible = False
                'True por que en éste punto se pueden guardar datos para la ejecución
                Me.ParaEjecucion = True
                'Se cargan ambas listas tanto las que están en evaluacion como las de ejecucion,
                'Las de evaluación serán cargadas solo para consulta
                'Me.tdBorrar.visible = False


                ' Me.txtTiempoEstimado.Enabled = False
                'Me.ddlUnidad.Enabled = False
                'Me.txtDPFechaPropuesta.Enabled = False


                Me.trFuncionarioEvaluacion.Visible = False
                ' Me.trFuncionariosReal.Visible = True

                If CargarOperariosActuales(String.Format("{0} AND {1} = '{2}'", vlc_CondicionEvaluacion, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_EST_CLAS, "0")) Then
                    'CargarUnidadesTiempo(String.Format("{0} AND {1} = '{2}'", vlc_CondicionEvaluacion, Modelo.OTT_TIEMPO_OPERARIO.CLASIFICACION, Clasificacion.ESTIMADO))
                End If

                inicializarSetDatosEjecucion()
                inicializarSetDatosReal()

                'Condicion para cargar los funcionarios anteriormente ingresados de ejecucion
                '{0}:Columna ID_SECTOR_TALLER
                '{1}:Valor del Id sector taller
                '{2}:Columna ID_UBICACION
                '{3}:Número de ubicación deseada
                '{4}:Columna ID_ORDEN_TRABAJO
                '{5}:Id de la orden de trabajo cargada por parámetro
                '{6}:Columna ID_ETAPA_ORDEN_TRABAJO
                '{7}:Cargar solo las de EVALUACION
                vlc_CondicionEjecucion = String.Format("{0}={1} AND {2}={3} AND {4} = '{5}' AND {6} = {7}", Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER,
                                                              Me.IdSectorTaller, Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, Me.IdUbicacion,
                                                              Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo,
                                                              Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.EJECUCION)

                If CargarOperariosEnEjecucion(vlc_CondicionEjecucion, False) Then
                    ' CargarUnidadesTiempoEjecucion(vlc_CondicionEvaluacion)
                    '   CargarOperariosTiempoReal()
                Else
                    Me.liTiempoEjecucion.Visible = False

                End If
                'Inicializa tabla de funcionarios en memoria

                hdfHayDatos.Value = Me.DsFuncionariosEjecucion.Rows.Count

                ActivarRHEjecucion()

                CargarDatosMaterialesEjecucion()

            Case EstadoOrden.EN_PROCESO, EstadoOrden.PENDIENTE_APROBACION_REQUISICION, EstadoOrden.PARA_RETIRO_MATERIAL, EstadoOrden.MATERIAL_PENDIENTE_COMPRA, EstadoOrden.REVISION_PRESUPUESTO_SUPERVISOR, EstadoOrden.LIQUIDADA

                Me.liRecursosEjecucion.Visible = True
                'True por que en éste punto se pueden guardar datos para la ejecución
                Me.ParaEjecucion = True
                'Se cargan ambas listas tanto las que están en evaluacion como las de ejecucion,
                'Ambas serán cargadas solo para consulta
                'Me.tdBorrar.visible = False

                If CargarOperariosActuales(vlc_CondicionEvaluacion) Then
                    ' CargarUnidadesTiempo(vlc_CondicionEvaluacion)
                End If

                inicializarSetDatosEjecucion()
                inicializarSetDatosReal()

                'Condicion para cargar los funcionarios anteriormente ingresados de ejecucion
                '{0}:Columna ID_SECTOR_TALLER
                '{1}:Valor del Id sector taller
                '{2}:Columna ID_UBICACION
                '{3}:Número de ubicación deseada
                '{4}:Columna ID_ORDEN_TRABAJO
                '{5}:Id de la orden de trabajo cargada por parámetro
                '{6}:Columna ID_ETAPA_ORDEN_TRABAJO
                '{7}:Cargar solo las de EVALUACION
                vlc_CondicionEjecucion = String.Format("{0}={1} AND {2}={3} AND {4} = '{5}' AND {6} = {7}", Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER,
                                                              Me.IdSectorTaller, Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, Me.IdUbicacion,
                                                              Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo,
                                                              Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.EJECUCION)

                If CargarOperariosEnEjecucion(vlc_CondicionEjecucion, True) Then
                    ' CargarUnidadesTiempoEjecucion(vlc_CondicionEvaluacion)
                    '   CargarOperariosTiempoReal()
                Else
                    Me.liTiempoEjecucion.Visible = False

                End If
                'Inicializa tabla de funcionarios en memoria

                'Me.txtTiempoEstimado.Enabled = False
                'Me.ddlUnidad.Enabled = False
                ' Me.txtDPFechaPropuesta.Enabled = False


                Me.trFuncionarioEvaluacion.Visible = False
                Me.trFuncionarioEjecucion.Visible = False
                'Me.trFuncionariosReal.Visible = False
                Me.btnAgregarFuncionario.Visible = False
                Me.btnGuardar.Visible = False
                Me.btnGuardarYFinalizar.Visible = False
                Me.txtDPFechaEfectuo.Enabled = False
                ' Me.ddlUnidadesDeTiempo.Enabled = False
                ' Me.txtTiempoReal.Enabled = False
                Me.txtdpFechaPropuestaInicio.Enabled = False
                Me.txtTiempoEstimadoFinaliza.Enabled = False
                Me.ddlUnidadTiempoEstimado.Enabled = False
                ' Me.ddlFuncionariosReal.Enabled = False
                ' Me.btnAgregarFuncionarioTR.Visible = False


                ActivarEvaluacionRecursoHumano()

                CargarDatosMaterialesEjecucion()
        End Select
    End Sub


    ''' <summary>
    ''' Inicializa el control de usuario para mostrar la información general
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InicializarControl()
        'Asignación de Datos para generar el web user control de la información general

        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")

        Me.IdSectorTaller = WebUtils.LeerParametro(Of Integer)("pvn_IdSectorTaller")

        ctrl_InfoGeneral.Anno = Me.Anno
        ctrl_InfoGeneral.IdOrdenTrabajo = IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = IdUbicacion

        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()

    End Sub

    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>31/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarSetDatos()
        Dim vlo_columna As DataColumn
        Dim vlo_llaves(1) As DataColumn


        'Se crea un nuevo datatabla 
        Me.DsFuncionarios = New DataTable

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTF_OPERARIO_AREALST.CEDULA
        'Se agrega la columna configurada al set de datos
        DsFuncionarios.Columns.Add(vlo_columna)
        'Se agrega al arreglo de llaves primarias la columna
        vlo_llaves(0) = vlo_columna
        'Y se configura el set de datos para que busque por formatos admitidos como llave primaria.
        Me.DsFuncionarios.PrimaryKey = vlo_llaves

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO
        'Se agrega la columna configurada al set de datos
        DsFuncionarios.Columns.Add(vlo_columna)

    End Sub

    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>31/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarSetDatosEjecucion()
        Dim vlo_columna As DataColumn
        Dim vlo_llaves(1) As DataColumn


        'Se crea un nuevo datatabla 
        Me.DsFuncionariosEjecucion = New DataTable

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTF_OPERARIO_AREALST.CEDULA
        'Se agrega la columna configurada al set de datos
        DsFuncionariosEjecucion.Columns.Add(vlo_columna)
        'Se agrega al arreglo de llaves primarias la columna
        vlo_llaves(0) = vlo_columna
        'Y se configura el set de datos para que busque por formatos admitidos como llave primaria.
        Me.DsFuncionariosEjecucion.PrimaryKey = vlo_llaves

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO
        'Se agrega la columna configurada al set de datos
        DsFuncionariosEjecucion.Columns.Add(vlo_columna)

    End Sub

    ''' <summary>
    ''' Inicializa la tabla de dato de funcionarios para el tiempo real
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>30/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarSetDatosReal()
        Dim vlo_columna As DataColumn
        Dim vlo_llaves(1) As DataColumn


        'Se crea un nuevo datatabla 
        Me.DsFuncionariosReal = New DataTable

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTF_OPERARIO_AREALST.CEDULA
        'Se agrega la columna configurada al set de datos
        DsFuncionariosReal.Columns.Add(vlo_columna)
        'Se agrega al arreglo de llaves primarias la columna
        vlo_llaves(0) = vlo_columna
        'Y se configura el set de datos para que busque por formatos admitidos como llave primaria.
        Me.DsFuncionariosReal.PrimaryKey = vlo_llaves

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO
        'Se agrega la columna configurada al set de datos
        DsFuncionariosReal.Columns.Add(vlo_columna)

    End Sub

    ''' <summary>
    ''' Carga la lista de unidades de tiempo
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>03/02/2016</creationDate>
    Private Sub CargarUnidades(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_UNIDAD_TIEMPO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden, True, pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                'ddl de ejecucion
                With Me.ddlUnidadTiempoEstimado
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
    ''' agrega un funcionario al dataset, estos se encuentran en memoria, y son insertados en la 
    ''' base de datos hasta el final, es decir un vez que se da click sobre el boton de aceptar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>31/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregaFuncionariosDataTable()
        Dim vlo_DrNuevaFila As DataRow

        Try
            If Me.DsFuncionarios.Rows.Find(New Object() {Me.ddlFuncionario.SelectedValue}) Is Nothing Then

                For Each vlo_DrFuncionario As DataRow In DsFuncionariosTaller.Tables(0).Rows
                    If String.Compare(vlo_DrFuncionario(Modelo.V_OTF_OPERARIO_AREALST.CEDULA), Me.ddlFuncionario.SelectedValue) = 0 Then
                        vlo_DrNuevaFila = Me.DsFuncionarios.NewRow
                        vlo_DrNuevaFila.Item(Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO) = Me.ddlFuncionario.SelectedItem
                        vlo_DrNuevaFila.Item(Modelo.V_OTF_OPERARIO_AREALST.CEDULA) = Me.ddlFuncionario.SelectedValue
                        Me.DsFuncionarios.Rows.Add(vlo_DrNuevaFila)
                    End If
                Next

                If Me.DsFuncionarios IsNot Nothing AndAlso Me.DsFuncionarios.Rows.Count > 0 Then
                    Me.rpFuncionarios.DataSource = Me.DsFuncionarios
                    Me.rpFuncionarios.DataMember = Me.DsFuncionarios.TableName
                    Me.rpFuncionarios.DataBind()
                    Me.rpFuncionarios.Visible = True
                    Me.hdfHayDatos.Value = Me.DsFuncionarios.Rows.Count
                Else
                    With Me.rpFuncionarios
                        .DataSource = Nothing
                        .DataBind()
                    End With
                    Me.rpFuncionarios.Visible = False
                End If

            Else
                MostrarAlertaError("Usuario repetido en la lista")
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Agrega funcionarios al listado de recurso humano para la ejecución
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregaFuncionariosEjecucion()
        Dim vlo_DrNuevaFila As DataRow

        Try
            If Me.DsFuncionariosEjecucion.Rows.Find(New Object() {Me.ddlFuncionarioEjecucion.SelectedValue}) Is Nothing Then

                For Each vlo_DrFuncionario As DataRow In DsFuncionariosTallerEjecucion.Tables(0).Rows
                    If String.Compare(vlo_DrFuncionario(Modelo.V_OTF_OPERARIO_AREALST.CEDULA), Me.ddlFuncionarioEjecucion.SelectedValue) = 0 Then
                        vlo_DrNuevaFila = Me.DsFuncionariosEjecucion.NewRow
                        vlo_DrNuevaFila.Item(Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO) = Me.ddlFuncionarioEjecucion.SelectedItem
                        vlo_DrNuevaFila.Item(Modelo.V_OTF_OPERARIO_AREALST.CEDULA) = Me.ddlFuncionarioEjecucion.SelectedValue
                        Me.DsFuncionariosEjecucion.Rows.Add(vlo_DrNuevaFila)
                    End If
                Next

                If Me.DsFuncionariosEjecucion IsNot Nothing AndAlso Me.DsFuncionariosEjecucion.Rows.Count > 0 Then
                    Me.rpFuncionariosEjecucion.DataSource = Me.DsFuncionariosEjecucion
                    Me.rpFuncionariosEjecucion.DataMember = Me.DsFuncionariosEjecucion.TableName
                    Me.rpFuncionariosEjecucion.DataBind()
                    Me.rpFuncionariosEjecucion.Visible = True
                    Me.hdfHayDatos.Value = Me.DsFuncionarios.Rows.Count
                Else
                    With Me.rpFuncionariosEjecucion
                        .DataSource = Nothing
                        .DataBind()
                    End With
                    Me.rpFuncionariosEjecucion.Visible = False
                End If

            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' borra un registro del dataset temporal de funcionarios
    ''' </summary>
    ''' <param name="pvc_CommandName"></param>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>28/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BorrarFuncionario(pvc_CommandName As String)
        Try
            'Este if en caso de que se borren funcionarios y la tabla quede vacia
            If DsFuncionarios.Rows.Count <= 0 Then
                Me.rfvddlFuncionario.Enabled = True
                ' Me.rfvDdlUnidad.Enabled = False
                ' Me.rfvDate.Enabled = False
                Me.rfvddlFuncionario.Enabled = False
                ' Me.rvfTxtTiempoEstimado.Enabled = False
            End If

            Me.DsFuncionarios.Rows.Find(New Object() {pvc_CommandName}).Delete()


            If Me.DsFuncionarios IsNot Nothing AndAlso Me.DsFuncionarios.Rows.Count > 0 Then
                Me.rpFuncionarios.DataSource = Me.DsFuncionarios
                Me.rpFuncionarios.DataMember = Me.DsFuncionarios.TableName
                Me.rpFuncionarios.DataBind()
                Me.rpFuncionarios.Visible = True
                Me.hdfHayDatos.Value = Me.DsFuncionarios.Rows.Count
            Else
                With Me.rpFuncionarios
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpFuncionarios.Visible = False
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' borra un registro del dataset temporal de oficios
    ''' </summary>
    ''' <param name="pvc_CommandName"></param>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BorrarFuncionarioEjecucion(pvc_CommandName As String)
        Try
            'Este if en caso de que se borren funcionarios y la tabla quede vacia
            If DsFuncionariosEjecucion.Rows.Count <= 0 Then
                Me.rfvddlFuncionario.Enabled = True
                ' Me.rfvDdlUnidad.Enabled = False
                ' Me.rfvDate.Enabled = False
                Me.rfvddlFuncionario.Enabled = False
                ' Me.rvfTxtTiempoEstimado.Enabled = False
            End If

            Me.DsFuncionariosEjecucion.Rows.Find(New Object() {pvc_CommandName}).Delete()

            If Me.DsFuncionariosEjecucion IsNot Nothing AndAlso Me.DsFuncionariosEjecucion.Rows.Count > 0 Then
                Me.rpFuncionariosEjecucion.DataSource = Me.DsFuncionariosEjecucion
                Me.rpFuncionariosEjecucion.DataMember = Me.DsFuncionariosEjecucion.TableName
                Me.rpFuncionariosEjecucion.DataBind()
                Me.rpFuncionariosEjecucion.Visible = True
                Me.hdfHayDatos.Value = Me.DsFuncionariosEjecucion.Rows.Count
            Else
                With Me.rpFuncionariosEjecucion
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpFuncionariosEjecucion.Visible = False
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Carga la lista de operarios junto con el coordinador principal y el sustituto(si existe)
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>28/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaFuncionarios(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlFuncionario.Items.Clear()
            Me.ddlFuncionario.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
            '   Me.ddlFuncionariosReal.Items.Clear()
            '  Me.ddlFuncionariosReal.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
            Me.ddlFuncionarioEjecucion.Items.Clear()
            Me.ddlFuncionarioEjecucion.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            'Se carga la lista de operarios que posee el actual sector o taller
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTF_OPERARIO_AREA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then

                For Each vlo_fila As DataRow In vlo_DsDatos.Tables(0).Rows
                    Me.ddlFuncionario.Items.Add(New ListItem(vlo_fila(Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO), vlo_fila(Modelo.V_OTF_OPERARIO_AREALST.CEDULA)))
                    ' Me.ddlFuncionariosReal.Items.Add(New ListItem(vlo_fila(Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO), vlo_fila(Modelo.V_OTF_OPERARIO_AREALST.CEDULA)))
                    Me.ddlFuncionarioEjecucion.Items.Add(New ListItem(vlo_fila(Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO), vlo_fila(Modelo.V_OTF_OPERARIO_AREALST.CEDULA)))
                Next

                Me.ddlFuncionario.DataBind()
                ' Me.ddlFuncionariosReal.DataBind()
                Me.ddlFuncionarioEjecucion.DataBind()
            Else
                With Me.ddlFuncionario
                    .DataSource = Nothing
                    .DataBind()
                End With

            End If
            Me.DsFuncionariosTaller = vlo_DsDatos
            Me.DsFuncionariosTallerEjecucion = vlo_DsDatos
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
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">id de la ubicacion</param>
    ''' <param name="pvn_IdOrdenTrabajo">id de la orden</param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>01/02/2016</creationDate>
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
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvn_IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga los datos de los materiales de la orden
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarSolicitudMaterialModificar()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.SolicitudMaterial = vlo_Ws_OT_OrdenesDeTrabajo.OTT_SOLICITUD_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_SOLICITUD_MATERIAL.ID_UBICACION, Me.OrdenTrabajo.IdUbicacion, Modelo.OTT_SOLICITUD_MATERIAL.ID_ORDEN_TRABAJO, Me.OrdenTrabajo.IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga los datos de los materiales de la orden
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarDatosMaterialesEjecucion()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.SolicitudMaterial = vlo_Ws_OT_OrdenesDeTrabajo.OTT_SOLICITUD_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_SOLICITUD_MATERIAL.ID_UBICACION, Me.OrdenTrabajo.IdUbicacion, Modelo.OTT_SOLICITUD_MATERIAL.ID_ORDEN_TRABAJO, Me.OrdenTrabajo.IdOrdenTrabajo))

            If Me.SolicitudMaterial.Existe Then
                If Me.SolicitudMaterial.NoRequiereMaterial = 1 Then
                    Me.chkRequierMaterial.Checked = True
                    Me.panelMateriales.Visible = False
                Else
                    Me.chkRequierMaterial.Checked = False
                    Me.panelMateriales.Visible = True
                End If
                Me.txtObservacionesMaterial.Text = IIf(Me.SolicitudMaterial.Observaciones = "-", String.Empty, Me.SolicitudMaterial.Observaciones)
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
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaMateriales(pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_Condicion As String

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If Me.OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EN_EVALUACION Then
                vlc_Condicion = String.Format("{0} = '{1}' AND {2} = {3} AND {4} = '{5}'", Modelo.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_DETALLE_MATERIAL.ESTADO, EstadoRegistro.PENDIENTE_ENVIO)
            Else
                vlc_Condicion = String.Format("{0} = '{1}' AND {2} = {3}", Modelo.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION, Me.IdUbicacion)
                Me.trCodigo.Visible = False
                Me.btnAgregarMaterial.Visible = False
                Me.txtObservacionesMaterial.ReadOnly = True
                Me.chkRequierMaterial.Enabled = False
            End If

            Me.DsMaterialesInsert = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

            If Me.DsMaterialesInsert IsNot Nothing AndAlso Me.DsMaterialesInsert.Tables(0).Rows.Count > 0 Then
                Me.rpPedidos.DataSource = DsMaterialesInsert
                Me.rpPedidos.DataMember = Me.DsMaterialesInsert.Tables(0).TableName
                Me.rpPedidos.DataBind()
                Me.rpPedidos.Visible = True

                MontoTotal = 0
                For Each vlo_fila In DsMaterialesInsert.Tables(0).Rows
                    MontoTotal = MontoTotal + vlo_fila(Modelo.V_OTT_DETALLE_MATERIALLST.COSTO_PROMEDIO)
                Next

                Me.lblMontoTotal.Text = String.Format("Total: ₡{0}", MontoTotal.ToString("N2"))

                If Me.OrdenTrabajo.EstadoOrdenTrabajo <> EstadoOrden.EN_EVALUACION Then
                    Me.trNoMaterial.Visible = False
                End If
            Else
                With Me.rpPedidos
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpPedidos.Visible = False
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
    ''' Agrega un detalle material a la base de datos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregarDetalleMaterial(pvn_IdAlmacenBodega As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial
        Dim vlo_fila() As DataRow

        'instanciar y configurar objetos
        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntOttDetalleMaterial = New EntOttDetalleMaterial
            vlo_EntOttDetalleMaterial.IdMaterial = Me.txtCodigo.Text
            vlo_EntOttDetalleMaterial.IdUbicacion = Me.IdUbicacion
            vlo_EntOttDetalleMaterial.IdUbicacionAdministra = Me.IdUbicacion
            vlo_EntOttDetalleMaterial.Detalle = Me.WucDatosMaterial.RetornaDetalle
            vlo_EntOttDetalleMaterial.IdOrdenTrabajo = Me.IdOrdenTrabajo
            vlo_EntOttDetalleMaterial.CantidadSolicitada = CType(Me.WucDatosMaterial.RetornaCantidad, Double)
            vlo_EntOttDetalleMaterial.Estado = EstadoRegistro.PENDIENTE_ENVIO
            vlo_EntOttDetalleMaterial.ViaDespacho = ViaDespacho.ALMACEN
            vlo_EntOttDetalleMaterial.IdAlmacenBodega = pvn_IdAlmacenBodega
            vlo_EntOttDetalleMaterial.Usuario = Me.Usuario.UserName

            If Me.SolicitudMaterial.Existe Then

                vlo_fila = Me.DsMaterialesInsert.Tables(0).Select(String.Format("{0} = {1}", Modelo.V_OTT_DETALLE_MATERIALLST.ID_MATERIAL, Me.txtCodigo.Text))

                If vlo_fila.Length > 0 Then
                    MostrarAlertaError("Ya existe un registro con el material solicitado, proceda a modificarlo.")
                Else

                    vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_InsertarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOttDetalleMaterial)
                End If
            Else

                Me.SolicitudMaterial.IdOrdenTrabajo = Me.OrdenTrabajo.IdOrdenTrabajo
                Me.SolicitudMaterial.IdUbicacion = Me.OrdenTrabajo.IdUbicacion
                Me.SolicitudMaterial.NoRequiereMaterial = 0
                Me.SolicitudMaterial.Observaciones = Me.txtObservacionesMaterial.Text
                Me.SolicitudMaterial.Usuario = Usuario.UserName
                Me.SolicitudMaterial.EstadoSolMaterial = EstadoSolicitudMaterial.INGRESADO_POR_COORDINADOR

                vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_InsertarSolicitudDetalle(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.SolicitudMaterial, vlo_EntOttDetalleMaterial)

            End If

            If Me.SolicitudMaterial.NoRequiereMaterial = 1 Then

                CargarSolicitudMaterialModificar()

                Me.SolicitudMaterial.NoRequiereMaterial = 0
                Me.SolicitudMaterial.Usuario = Usuario.UserName
                Me.SolicitudMaterial.EstadoSolMaterial = EstadoSolicitudMaterial.INGRESADO_POR_COORDINADOR

                vlo_Ws_OT_OrdenesDeTrabajo.OTT_SOLICITUD_MATERIAL_ModificarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    Me.SolicitudMaterial)

            End If

            CargarSolicitudMaterialModificar()


        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Agrega un detalle material a la base de datos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ModificarDetalleMaterial(pvn_idDetalleMaterial As Integer, pvn_idMaterial As Integer, pvc_detalle As String, pvn_cantidad As Double, pvd_timestamp As Date, pvn_IdAlmacenBodega As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial

        'instanciar y configurar objetos
        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttDetalleMaterial = New EntOttDetalleMaterial
            vlo_EntOttDetalleMaterial.IdDetalleMaterial = pvn_idDetalleMaterial
            vlo_EntOttDetalleMaterial.IdMaterial = pvn_idMaterial
            vlo_EntOttDetalleMaterial.IdUbicacion = Me.IdUbicacion
            vlo_EntOttDetalleMaterial.IdUbicacionAdministra = Me.IdUbicacion
            vlo_EntOttDetalleMaterial.Detalle = pvc_detalle
            vlo_EntOttDetalleMaterial.IdOrdenTrabajo = Me.IdOrdenTrabajo
            vlo_EntOttDetalleMaterial.CantidadSolicitada = pvn_cantidad
            vlo_EntOttDetalleMaterial.Usuario = Me.Usuario.UserName
            vlo_EntOttDetalleMaterial.Estado = EstadoRegistro.PENDIENTE_ENVIO
            vlo_EntOttDetalleMaterial.TimeStamp = pvd_timestamp
            vlo_EntOttDetalleMaterial.ViaDespacho = ViaDespacho.ALMACEN
            vlo_EntOttDetalleMaterial.IdAlmacenBodega = pvn_IdAlmacenBodega

            vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttDetalleMaterial)
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' carga parametros del sistema
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/11/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarParametro(pvn_IdParametro As Integer, pvn_IdUbicacion As Integer) As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ObtenerRegistro(
                 ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, pvn_IdParametro, Modelo.OTP_PARAMETRO_UBICACION.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga la tabla de operarios que están actualmente para hacer la evaluación
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarOperariosActuales(pvc_Condicion As String) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet
        Dim vlo_filaNueva As DataRow

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            'Carga la lista de funcionarios actuales en el repeater
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, String.Empty, False, 1, 0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                'Se actualiza la tabla en memoria con los datos obtenidos


                '   If DsFuncionarios.Rows.Count < 0 Then


                For Each vlo_fila As DataRow In vlo_DsDatos.Tables(0).Rows
                    vlo_filaNueva = Me.DsFuncionarios.NewRow()
                    vlo_filaNueva(Modelo.V_OTF_OPERARIO_AREALST.CEDULA) = vlo_fila(Modelo.V_OTF_OPERARIO_AREALST.CEDULA)
                    vlo_filaNueva(Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO) = vlo_fila(Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO)
                    Me.DsFuncionarios.Rows.Add(vlo_filaNueva)
                Next

                ' End If
                Me.rpFuncionarios.DataSource = vlo_DsDatos
                Me.rpFuncionarios.DataMember = vlo_DsDatos.Tables(0).TableName
                Me.rpFuncionarios.DataBind()
                Me.rpFuncionarios.Visible = True

                'Me.txtDPFechaPropuesta.Text = vlo_DsDatos.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_PROPUESTA).ToString
                Return True

            Else
                With Me.rpFuncionarios
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpFuncionarios.Visible = False
            End If

            Return False

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
    End Function

    ''' <summary>
    ''' Carga la tabla de operarios que están actualmente para hacer la ejecucion
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarOperariosEnEjecucion(pvc_Condicion As String, pvb_SoloLectura As Boolean) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet
        Dim vlo_filaNueva As DataRow

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            'Carga la lista de funcionarios actuales en el repeater
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, String.Empty, False, 1, 0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                'Se actualiza la tabla en memoria con los datos obtenidos
                If Not pvb_SoloLectura Then
                    For Each vlo_fila As DataRow In vlo_DsDatos.Tables(0).Rows
                        If vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_EST_CLAS) = Clasificacion.ESTIMADO Then
                            vlo_filaNueva = Me.DsFuncionariosEjecucion.NewRow()
                            vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)
                            vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)
                            Me.DsFuncionariosEjecucion.Rows.Add(vlo_filaNueva)
                        Else
                            vlo_filaNueva = Me.DsFuncionariosReal.NewRow()
                            vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)
                            vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)
                            Me.DsFuncionariosReal.Rows.Add(vlo_filaNueva)
                        End If


                    Next
                End If

                Me.rpFuncionariosEjecucion.DataSource = vlo_DsDatos
                Me.rpFuncionariosEjecucion.DataMember = vlo_DsDatos.Tables(0).TableName
                Me.rpFuncionariosEjecucion.DataBind()
                Me.rpFuncionariosEjecucion.Visible = True

                If Me.ParaEjecucion Then
                    Me.txtdpFechaPropuestaInicio.Text = CDate(vlo_DsDatos.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_PROPUESTA).ToString)
                    Me.txtDPFechaEfectuo.Text = CDate(vlo_DsDatos.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_EJECUTA).ToString)
                    Me.txtTiempoEstimadoFinaliza.Text = vlo_DsDatos.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_EST).ToString
                    Me.ddlUnidadTiempoEstimado.Text = vlo_DsDatos.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_EST).ToString

                End If
                Return True

            Else
                With Me.rpFuncionariosEjecucion
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpFuncionariosEjecucion.Visible = False
            End If

            Return False

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
    End Function

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>01/02016</creationDate>
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
                String.Format("ID_PERSONAL = '{0}'", pvn_IdPersonal))
        Catch ex As Exception
            Throw
        Finally
            If vlo_WsEU_Curriculo IsNot Nothing Then
                vlo_WsEU_Curriculo.Dispose()
            End If
        End Try
    End Function

    Private Function Guardar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_fechaefectuo As Date
        Dim vlo_fechaPropuestaEjecuta As Date
        Dim vlo_fechaPropuesta As Date
        Dim vlo_tiempoRealInvertido As Integer
        Dim vlo_tiempoEstimado As Integer
        Dim vlo_UnidadtiempoEstimado As Integer

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        If String.IsNullOrWhiteSpace(Me.txtDPFechaEfectuo.Text) Then
            vlo_fechaefectuo = Nothing
        Else
            vlo_fechaefectuo = CDate(Me.txtDPFechaEfectuo.Text)
        End If

        If String.IsNullOrWhiteSpace(Me.txtdpFechaPropuestaInicio.Text) Then
            vlo_fechaPropuestaEjecuta = Nothing
        Else
            vlo_fechaPropuestaEjecuta = CDate(Me.txtdpFechaPropuestaInicio.Text)
        End If

        Try

            'Para ver mas detalle de cada uno de los parámetros revise el SLA 
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_GuardarEvaluacion(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.DsFuncionarios, Me.DsFuncionariosReal, Me.DsFuncionariosEjecucion, Me.IdSectorTaller, Me.IdUbicacion, Me.IdOrdenTrabajo,
                vlo_fechaPropuesta, vlo_fechaefectuo, vlo_fechaPropuestaEjecuta,
                vlo_UnidadtiempoEstimado, vlo_tiempoEstimado, vlo_tiempoRealInvertido,
                 Nothing,
                IIf(String.IsNullOrWhiteSpace(Me.ddlUnidadTiempoEstimado.SelectedValue), Nothing, Me.ddlUnidadTiempoEstimado.SelectedValue),
                IIf(String.IsNullOrWhiteSpace(Me.txtTiempoEstimadoFinaliza.Text), Nothing, Me.txtTiempoEstimadoFinaliza.Text),
                Me.Usuario.UserName, Me.ParaEjecucion, Me.OrdenTrabajo, Me.ModificarOrdenTrabajo, Me.chkRequierMaterial.Checked, Me.txtObservacionesMaterial.Text) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function BorrarDetalleMaterial(pvn_IdDetalleMaterial As Integer) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial

        'instanciar y configurar objetos
        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttDetalleMaterial = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL, pvn_IdDetalleMaterial))


            If vlo_EntOttDetalleMaterial.Existe Then
                Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_BorrarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOttDetalleMaterial) > 0
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
        Return False
    End Function

    ''' <summary>
    ''' funcion encargada de realizar los oredenamientos segun los valores brindados
    ''' </summary>
    ''' <param name="pvc_Columna"> numero de la columna para el ordenamiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>30/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerExpresionDeOrdenamiento(pvc_Columna As String) As String
        If UltimoSortDirection = SortDirection.Ascending Then
            UltimoSortDirection = SortDirection.Descending
        Else
            UltimoSortDirection = SortDirection.Ascending
        End If

        Return String.Format("{0} {1}", pvc_Columna, IIf(UltimoSortDirection = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))
    End Function

#End Region

End Class
