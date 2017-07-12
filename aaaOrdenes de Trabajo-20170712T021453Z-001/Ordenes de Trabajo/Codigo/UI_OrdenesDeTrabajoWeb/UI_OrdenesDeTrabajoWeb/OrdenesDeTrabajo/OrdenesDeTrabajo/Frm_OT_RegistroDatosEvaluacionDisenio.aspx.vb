Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Frm_OT_RegistroDatosEvaluacionDisenio
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortExpressionFuncionario As String
        Get
            If ViewState("UltimoSortExpressionFuncionario") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortExpressionFuncionario"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortExpressionFuncionario") = value
        End Set
    End Property

    ''' <summary>
    ''' Ultima columna de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortColumnFuncionario As String
        Get
            If ViewState("UltimoSortColumnFuncionario") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortColumnFuncionario"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortColumnFuncionario") = value
        End Set
    End Property

    ''' <summary>
    ''' Ultima direccion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortDirectionFuncionario As SortDirection
        Get
            If ViewState("UltimoSortDirectionFuncionario") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return SortDirection.Ascending
            End If
            Return CType(ViewState("UltimoSortDirectionFuncionario"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("UltimoSortDirectionFuncionario") = value
        End Set
    End Property

    ''' <summary>
    ''' Ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortExpressionEncargado As String
        Get
            If ViewState("UltimoSortExpressionEncargado") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortExpressionEncargado"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortExpressionEncargado") = value
        End Set
    End Property

    ''' <summary>
    ''' Ultima columna de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortColumnEncargado As String
        Get
            If ViewState("UltimoSortColumnEncargado") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortColumnEncargado"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortColumnEncargado") = value
        End Set
    End Property

    ''' <summary>
    ''' Ultima direccion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortDirectionEncargado As SortDirection
        Get
            If ViewState("UltimoSortDirectionEncargado") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return SortDirection.Ascending
            End If
            Return CType(ViewState("UltimoSortDirectionEncargado"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("UltimoSortDirectionEncargado") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/02/2016</creationDate>
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
    ''' Propiedad para determinar si se debe cambiar el estado de la orden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property GuardarEnviar As Boolean
        Get
            Return CType(ViewState("GuardarEnviar"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("GuardarEnviar") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el empleado  seleccionado en los combos de funcionarios y encargados
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Empleado As WsrEU_Curriculo.EntEmpleados
        Get
            Return CType(ViewState("Empleado"), WsrEU_Curriculo.EntEmpleados)
        End Get
        Set(value As WsrEU_Curriculo.EntEmpleados)
            ViewState("Empleado") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/02/2016</creationDate>
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
    ''' Propiedad para  determinar el año de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>17/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Anio As Integer
        Get
            Return CType(ViewState("Anio"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Anio") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para  determinar si se ha modificado algo en la aplicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>17/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property BanderaCambios As Boolean
        Get
            Return CType(ViewState("BanderaCambios"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("BanderaCambios") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el id ubicacion de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/02/2016</creationDate>
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
    ''' <creationDate>08/02/2016</creationDate>
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
    ''' Propiedad para el dataset de operarios orden trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsOperarioOrdenTrabajoVistaEncargado As Data.DataSet
        Get
            Return CType(ViewState("DsOperarioOrdenTrabajoVistaEncargado"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsOperarioOrdenTrabajoVistaEncargado") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de operarios orden trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsOperarioOrdenTrabajoEncargado As Data.DataSet
        Get
            Return CType(ViewState("DsOperarioOrdenTrabajoEncargado"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsOperarioOrdenTrabajoEncargado") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de operarios tiempo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsTiempoOperarioVistaColaborador As Data.DataSet
        Get
            Return CType(ViewState("DsTiempoOperarioVistaColaborador"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsTiempoOperarioVistaColaborador") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de operarios tiempo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsOperarioOrdenTrabajoColaborador As Data.DataSet
        Get
            Return CType(ViewState("DsOperarioOrdenTrabajoColaborador"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsOperarioOrdenTrabajoColaborador") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de  tiempos de operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsTiempoOperarioColaborador As Data.DataSet
        Get
            Return CType(ViewState("DsTiempoOperarioColaborador"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsTiempoOperarioColaborador") = value
        End Set
    End Property

    ''' <summary>
    ''' Condicion de búsqueda para el operario encargado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property CondicionOperarioEncargado As String
        Get
            Return CType(ViewState("CondicionOperarioEncargado"), String)
        End Get
        Set(value As String)
            ViewState("CondicionOperarioEncargado") = value
        End Set
    End Property

    ''' <summary>
    ''' Condicion de búsqueda para el operario encargado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property CondicionOperarioColaborador As String
        Get
            Return CType(ViewState("CondicionOperarioColaborador"), String)
        End Get
        Set(value As String)
            ViewState("CondicionOperarioColaborador") = value
        End Set
    End Property

    ''' <summary>
    ''' Condicion de búsqueda para el operario encargado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property CondicionTiempoColaborador As String
        Get
            Return CType(ViewState("CondicionTiempoColaborador"), String)
        End Get
        Set(value As String)
            ViewState("CondicionTiempoColaborador") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para almacenar el numero de empleado a modicar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property NumEmpleadoMod As Integer
        Get
            Return CType(ViewState("NumEmpleadoMod"), Integer)
        End Get
        Set(value As Integer)
            ViewState("NumEmpleadoMod") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para almacenar id_sector taller
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdSectorTaller As Integer
        Get
            Return CType(ViewState("IdSectorTaller"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdSectorTaller") = value
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
    ''' <creationDate>05/02/2016</creationDate>
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
    ''' Evento que se ejecuta cuando se cambio el vaor del combo de funcionarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlFuncionario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFuncionario.SelectedIndexChanged
        Try
            If Me.ddlFuncionario.SelectedValue <> String.Empty Then
                CargarDescripcionAreaFuncionario()
            Else
                Me.lblAreaFuncionario.Text = String.Empty
            End If
            WebUtils.RegistrarScript(Me.Page, "establecerRangosFecha", "establecerRangosFecha();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el boton de actualizar (nombre del proyecto)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Try
            If ModificarNombreProyecto() Then
                InicializarControl()
                WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarAlertaNombreProyectoExitoso();")
            Else
                MostrarAlertaError("No ha sido posible actualizar la información del nombre del proyecto.")
            End If
            WebUtils.RegistrarScript(Me.Page, "establecerRangosFecha", "establecerRangosFecha();")
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
    ''' Evento que se ejecuta cuando  se da click sobre el boton de de agregar 'btnAgregar'
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>09/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            AgregarFuncionarioEncargado()
            Me.BanderaCambios = True
            WebUtils.RegistrarScript(Me.Page, "ClickCancelas", "ActivarMensajeClick();")
            WebUtils.RegistrarScript(Me.Page, "establecerRangosFecha", "establecerRangosFecha();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejeciuta cuando se da click sobre el boton de agregar funcionario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarFuncionario_Click(sender As Object, e As EventArgs) Handles btnAgregarFuncionario.Click
        Try
            AgregarFuncionarioColaborador()
            Me.BanderaCambios = True
            WebUtils.RegistrarScript(Me.Page, "ClickCancelas", "ActivarMensajeClick();")
            WebUtils.RegistrarScript(Me.Page, "establecerRangosFecha", "establecerRangosFecha();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el boton de modificar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Dim vlo_DrFila As Data.DataRow
        Dim vlo_DrFilaVista As Data.DataRow

        Try
            vlo_DrFila = Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Rows.Find(New Object() {
                                                                                NumEmpleadoMod,
                                                                                Me.IdSectorTaller,
                                                                                Me.IdUbicacion,
                                                                                Me.IdOrdenTrabajo,
                                                                                EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA})


            Dim vlo_DataViewFechas As New Data.DataView(Me.DsOperarioOrdenTrabajoEncargado.Tables(0))
            Dim vlc_CondicionVista = String.Format("('{0}' >= {1} AND '{0}' <= {2})", Me.txtFechaHasta.Text.Trim, Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_DESDE, Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_HASTA)
            vlo_DataViewFechas.RowFilter = vlc_CondicionVista

            If vlo_DataViewFechas.Count = 0 Or vlo_DataViewFechas.Count = 1 Then

                vlo_DrFila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_HASTA) = CType(Me.txtFechaHasta.Text, DateTime)

                vlo_DrFilaVista = Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Rows.Find(New Object() {NumEmpleadoMod})

                vlo_DrFilaVista(Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA) = CType(Me.txtFechaHasta.Text, DateTime)

                Dim vlo_DataView As New Data.DataView(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0))
                vlo_DataView.Sort = String.Format("{0} {1}", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA, Ordenamiento.ASCENDENTE)

                With Me.rpEncargado
                    .DataSource = vlo_DataView
                    .DataMember = Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).TableName
                    .DataBind()
                    .Visible = True
                End With

                Me.btnModificar.Visible = False
                Me.btnAgregar.Visible = True

                Me.ddlEncargado.SelectedValue = String.Empty

                Me.txtFechaDesde.Text = String.Empty

                Me.txtFechaHasta.Text = String.Empty

                Me.ddlEncargado.Enabled = True
                Me.txtFechaDesde.Enabled = True

            Else
                MostrarAlertaError("No puede existir traslape de fechas entre encargados.")
            End If
            Me.BanderaCambios = True
            WebUtils.RegistrarScript(Me.Page, "ClickCancelas", "ActivarMensajeClick();")
            WebUtils.RegistrarScript(Me.Page, "establecerRangosFecha", "establecerRangosFecha();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el boton modificar del listato de encargados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibModificar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_DrFila As Data.DataRow

        Try

            NumEmpleadoMod = CType(CType(sender, ImageButton).CommandArgument.Split(",").GetValue(0), Integer)
            IdSectorTaller = CType(CType(sender, ImageButton).CommandArgument.Split(",").GetValue(1), Integer)

            vlo_DrFila = Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Rows.Find(New Object() {
                                                                                Me.NumEmpleadoMod,
                                                                                Me.IdSectorTaller,
                                                                                Me.IdUbicacion,
                                                                                Me.IdOrdenTrabajo,
                                                                                EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA})

            Me.ddlEncargado.SelectedValue = String.Format("{0},{1}", vlo_DrFila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO), vlo_DrFila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER))

            Me.txtFechaDesde.Text = CType(vlo_DrFila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_DESDE), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)

            Me.txtFechaHasta.Text = CType(vlo_DrFila(Utilerias.OrdenesDeTrabajo.Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_HASTA), DateTime).ToString(Utilerias.OrdenesDeTrabajo.Constantes.FORMATO_FECHA_UI)

            Me.btnModificar.Visible = True
            Me.btnAgregar.Visible = False

            Me.ddlEncargado.Enabled = False
            Me.txtFechaDesde.Enabled = False

            WebUtils.RegistrarScript(Me.Page, "ConfigurarFechas", "establecerFechaMinima();")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vln_NumEmpleado As Integer
        Dim vln_IdSectorTaller As Integer

        Try

            vln_NumEmpleado = CType(CType(sender, ImageButton).CommandArgument.Split(",").GetValue(0), Integer)
            vln_IdSectorTaller = CType(CType(sender, ImageButton).CommandArgument.Split(",").GetValue(1), Integer)

            BorrarEncargado(vln_NumEmpleado, vln_IdSectorTaller)
            Me.BanderaCambios = True
            WebUtils.RegistrarScript(Me.Page, "ClickCancelas", "ActivarMensajeClick();")
            WebUtils.RegistrarScript(Me.Page, "establecerRangosFecha", "establecerRangosFecha();")
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
    ''' Evento que se ejecuta cuando se da click sobre el boton borrar del listado de colaboradores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrarColaborador_Click(sender As Object, e As ImageClickEventArgs)
        Dim vln_NumEmpleado As Integer
        Dim vln_IdSectorTaller As Integer

        Try

            vln_NumEmpleado = CType(CType(sender, ImageButton).CommandArgument.Split(",").GetValue(0), Integer)
            vln_IdSectorTaller = CType(CType(sender, ImageButton).CommandArgument.Split(",").GetValue(1), Integer)

            BorrarColaborador(vln_NumEmpleado, vln_IdSectorTaller)
            Me.BanderaCambios = True
            WebUtils.RegistrarScript(Me.Page, "ClickCancelas", "ActivarMensajeClick();")
            WebUtils.RegistrarScript(Me.Page, "establecerRangosFecha", "establecerRangosFecha();")
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
    ''' Evento que se ejecuta al carar el repeater de encargados, asigna atributos a cada elemento del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpEncargado_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpEncargado.ItemDataBound
        Dim vlo_IbBorrar As ImageButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibBorrarEncargado") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrarEncargado"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el boton de Guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            Me.GuardarEnviar = False
            If GuardarEvaluacion() Then
                WebUtils.RegistrarScript(Me, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
            Else
                MostrarAlertaError("No ha sido posible almacenar la información.")
            End If
            Me.BanderaCambios = True
            WebUtils.RegistrarScript(Me.Page, "ClickCancelas", "ActivarMensajeClick();")
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
    ''' Evento que se ejecuta cuando se da click sobre el boton de Guardar y Enviar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardarEnviar_Click(sender As Object, e As EventArgs) Handles btnGuardarEnviar.Click
        Try
            Me.GuardarEnviar = True
            If GuardarEvaluacion() Then
                WebUtils.RegistrarScript(Me, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
            Else
                MostrarAlertaError("No ha sido posible almacenar la información.")
            End If
            Me.BanderaCambios = True
            WebUtils.RegistrarScript(Me.Page, "ClickCancelas", "ActivarMensajeClick();")
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
    ''' Evento que se ejecuta cuando se da click sobre el boton de cancelar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("Lst_OT_GestionOrdenTrabajoCordinadorSectorTaller.aspx", False)
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar los diferentess componentes del formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        LeerParametros()
        InicializarControl()
        CargarComboUnidadesTiempo()
        CargarOrdenTrabajo(Me.IdUbicacion, Me.IdOrdenTrabajo)
        CargarListaEncargado(String.Empty)
        CargaComboEncargadosFuncionarios()
        CargarListaColaborador(String.Empty)
        Me.txtNombreProyecto.Text = IIf(Me.OrdenTrabajo.NombreProyecto = "-", String.Empty, Me.OrdenTrabajo.NombreProyecto)
        CargarFuncionariosEncargados()
        CargarFuncionariosColaboradores()
        If Me.OrdenTrabajo.EstadoOrdenTrabajo <> EstadoOrden.ASIGNADA Then
            ' Me.ddlEncargado.Enabled = False
            'Me.txtFechaDesde.Enabled = False
            '  Me.btnAgregar.Visible = False
            Me.ddlFuncionario.Enabled = False
            Me.ddlUnidadTiempo.Enabled = False
            Me.txtTiempo.Enabled = False
            Me.btnAgregarFuncionario.Visible = False
            Me.btnGuardarEnviar.Visible = False
        Else
            WebUtils.RegistrarScript(Me.Page, "establecerRangosFecha", "establecerRangosFecha();")
        End If
        Me.BanderaCambios = False
    End Sub

    ''' <summary>
    ''' Lee los parametros 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.Anio = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub InicializarControl()
        'Asignación de Datos para generar el web user control de la información general
        ctrl_InfoGeneral.Anno = Me.Anio
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.IdUbicacion.ToString
        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()
        Me.upControlOrdenTrabajo.Update()
    End Sub

    ''' <summary>
    ''' Cara el combo de unidades de tiempo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboUnidadesTiempo()
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlUnidadTiempo.Items.Clear()

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_UNIDAD_TIEMPO_ListarRegistros(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                String.Format("{0} = '{1}'", Modelo.OTM_UNIDAD_TIEMPO.ESTADO, Estado.ACTIVO),
                                String.Format("{0} ASC", Modelo.OTM_UNIDAD_TIEMPO.DESCRIPCION),
                                False,
                                0,
                                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlUnidadTiempo
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_UNIDAD_TIEMPO.DESCRIPCION
                    .DataValueField = Modelo.OTM_UNIDAD_TIEMPO.ID_UNIDAD_TIEMPO
                    .DataBind()
                End With
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' El combo se carga con todos funcionarios a los cuales se les ha asignado un área profesional, únicamente profesionales
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargaComboEncargadosFuncionarios()
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlEncargado.Items.Clear()
            Me.ddlEncargado.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            Me.ddlFuncionario.Items.Clear()
            Me.ddlFuncionario.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTF_OPERARIO_AREA_ListarRegistrosLista(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                String.Format("{0} = '{1}' AND {2} = '{3}' AND {4} <> 0", Modelo.V_OTF_OPERARIO_AREALST.CATEGORIA_LABORAL, Area.PROFESIONAL, Modelo.V_OTF_OPERARIO_AREALST.ESTADO, Estado.ACTIVO, Modelo.V_OTF_OPERARIO_AREALST.ID_AREA_PROFESIONAL),
                                String.Format("{0} ASC", Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO),
                                False,
                                0,
                                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlEncargado
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO
                    .DataValueField = Modelo.V_OTF_OPERARIO_AREALST.NUM_EMPLEADO_SECTOR
                    .DataBind()
                End With

                With Me.ddlFuncionario
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO
                    .DataValueField = Modelo.V_OTF_OPERARIO_AREALST.NUM_EMPLEADO_SECTOR
                    .DataBind()
                End With
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga la lista de encargados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaEncargado(pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerSortExpressionEncargado(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)
        End If

        Try
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO, Cargo.ENCARGADO),
                pvc_Orden,
                False,
                0,
                0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpEncargado
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With
            Else
                With Me.rpEncargado
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
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaColaborador(pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerSortExpressionFuncionario(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO) 'lo pide el programa ordenar x isbn
        End If

        Try
            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO, Cargo.COLABORADOR),
                pvc_Orden,
                False,
                0,
                0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpFuncionarios
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                End With
            Else
                With Me.rpFuncionarios
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
            If vlo_DsDatos IsNot Nothing Then
                vlo_DsDatos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga datos del funcionario, para poder ver cual es el área profesional
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarDescripcionAreaFuncionario()
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTF_OPERARIO_AREA_ListarRegistrosLista(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                String.Format("{0} = {1}", Modelo.V_OTF_OPERARIO_AREALST.NUM_EMPLEADO, Me.ddlFuncionario.SelectedValue.Split(",").GetValue(0)),
                                String.Empty,
                                False,
                                0,
                                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                Me.lblAreaFuncionario.Text = vlo_DsDatos.Tables(0).Rows(0)(Modelo.V_OTF_OPERARIO_AREALST.AREA).ToString
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/02/2016</creationDate>
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

        If Me.OrdenTrabajo.Existe Then

        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    ''' <summary>
    ''' Carga los encargados de la OT, de la tabla OTT_OPERARIO_ORDEN_TRAB
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarFuncionariosEncargados()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsOperarioOrdenTrabajoVistaEncargado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}' AND {6} = '{7}'", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO, Cargo.ENCARGADO, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA),
                String.Format("{0} {1}", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA, Ordenamiento.ASCENDENTE),
                False,
                0,
                0)

            Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Columns(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NUM_EMPLEADO)}

            Me.CondicionOperarioEncargado = String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}' AND {6} = '{7}'", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.OTT_OPERARIO_ORDEN_TRAB.CARGO, Cargo.ENCARGADO, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA)

            Me.DsOperarioOrdenTrabajoEncargado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.CondicionOperarioEncargado,
                String.Format("{0} {1}", Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_HASTA, Ordenamiento.ASCENDENTE),
                False,
                0,
                0)

            Me.DsOperarioOrdenTrabajoEncargado.Tables(0).PrimaryKey = New Data.DataColumn() {
                   Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO),
                   Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER),
                   Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION),
                   Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO),
                   Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO)}

            If Me.DsOperarioOrdenTrabajoVistaEncargado IsNot Nothing AndAlso Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Rows.Count > 0 Then
                With Me.rpEncargado
                    .DataSource = Me.DsOperarioOrdenTrabajoVistaEncargado
                    .DataMember = Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).TableName
                    .DataBind()
                    .Visible = True
                End With
            Else
                With Me.rpEncargado
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
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
    ''' Carga los colaboradores de la OT, de la tabla OTT_OPERARIO_ORDEN_TRAB y OTT_TIEMPO_OPERARIO
    ''' </summary>
    ''' <remarks></remarks>
    '''  <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarFuncionariosColaboradores()

        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsTiempoOperarioVistaColaborador = vlo_Ws_OT_OrdenesDeTrabajo.OTT_TIEMPO_OPERARIO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}' AND {6} = '{7}'", Modelo.V_OTT_TIEMPO_OPERARIOLST.ID_UBICACION, Me.IdUbicacion, Modelo.V_OTT_TIEMPO_OPERARIOLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.V_OTT_TIEMPO_OPERARIOLST.CLASIFICACION, Clasificacion.ESTIMADO, Modelo.V_OTT_TIEMPO_OPERARIOLST.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA),
                String.Empty,
                False,
                0,
                0)

            Me.DsTiempoOperarioVistaColaborador.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.NUM_EMPLEADO)}

            Me.CondicionOperarioColaborador = String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}' AND {6} = '{7}'", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.OTT_OPERARIO_ORDEN_TRAB.CARGO, Cargo.COLABORADOR, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA)

            Me.DsOperarioOrdenTrabajoColaborador = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.CondicionOperarioColaborador,
                String.Empty,
                False,
                0,
                0)

            Me.DsOperarioOrdenTrabajoColaborador.Tables(0).PrimaryKey = New Data.DataColumn() {
                   Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO),
                   Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER),
                   Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION),
                   Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO),
                   Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO)}

            Me.CondicionTiempoColaborador = String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}' AND {6} = '{7}'", Modelo.OTT_TIEMPO_OPERARIO.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo, Modelo.OTT_TIEMPO_OPERARIO.CLASIFICACION, Clasificacion.ESTIMADO, Modelo.OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA)

            Me.DsTiempoOperarioColaborador = vlo_Ws_OT_OrdenesDeTrabajo.OTT_TIEMPO_OPERARIO_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.CondicionTiempoColaborador,
                String.Empty,
                False,
                0,
                0)

            Me.DsTiempoOperarioColaborador.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.NUM_EMPLEADO)}


            If Me.DsTiempoOperarioVistaColaborador IsNot Nothing AndAlso Me.DsTiempoOperarioVistaColaborador.Tables(0).Rows.Count > 0 Then
                With Me.rpFuncionarios
                    .DataSource = Me.DsTiempoOperarioVistaColaborador
                    .DataMember = Me.DsTiempoOperarioVistaColaborador.Tables(0).TableName
                    .DataBind()
                    .Visible = True
                End With
            Else
                With Me.rpFuncionarios
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
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
    ''' Agregar un registro en el dataset de OTT_OPERARIO_ORDEN_TRAB, funcionario encargado del proyecto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>09/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregarFuncionarioEncargado()
        Dim vlo_DrFila As Data.DataRow
        Dim vlo_DrFilaFindColaborador As Data.DataRow
        Dim vlo_DrFilaVista As Data.DataRow
        Dim vlo_DrFilaFind As Data.DataRow

        Try

            vlo_DrFilaFind = Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Rows.Find(New Object() {
                                                          Me.ddlEncargado.SelectedValue.Split(",").GetValue(0),
                                                          Me.ddlEncargado.SelectedValue.Split(",").GetValue(1),
                                                          Me.IdUbicacion,
                                                          Me.IdOrdenTrabajo,
                                                          EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA})

            If vlo_DrFilaFind Is Nothing Then

                vlo_DrFilaFindColaborador = Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Rows.Find(New Object() {
                                                         Me.ddlEncargado.SelectedValue.Split(",").GetValue(0),
                                                         Me.ddlEncargado.SelectedValue.Split(",").GetValue(1),
                                                         Me.IdUbicacion,
                                                         Me.IdOrdenTrabajo,
                                                         EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA})

                If vlo_DrFilaFindColaborador Is Nothing Then

                    Dim vlo_DataViewFechas As New Data.DataView(Me.DsOperarioOrdenTrabajoEncargado.Tables(0))
                    Dim vlc_CondicionVista = String.Format("(('{0}' >= {2} AND '{0}' <= {3}) OR ('{1}' >= {2} AND '{1}' <= {3})) AND (('{0}'< {3}) OR ('{1}' > {2}))", Me.txtFechaDesde.Text.Trim, Me.txtFechaHasta.Text.Trim, Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_DESDE, Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_HASTA)
                    vlo_DataViewFechas.RowFilter = vlc_CondicionVista

                    If vlo_DataViewFechas.Count = 0 Then

                        vlo_DrFila = Me.DsOperarioOrdenTrabajoEncargado.Tables(0).NewRow
                        vlo_DrFila.Item(Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO)) = Me.ddlEncargado.SelectedValue.Split(",").GetValue(0)
                        vlo_DrFila.Item(Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER)) = Me.ddlEncargado.SelectedValue.Split(",").GetValue(1)
                        vlo_DrFila.Item(Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION)) = Me.IdUbicacion
                        vlo_DrFila.Item(Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO)) = Me.IdOrdenTrabajo
                        vlo_DrFila.Item(Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO)) = EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA
                        vlo_DrFila.Item(Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.CARGO)) = Cargo.ENCARGADO
                        vlo_DrFila.Item(Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_DESDE)) = CType(Me.txtFechaDesde.Text, DateTime)
                        vlo_DrFila.Item(Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_HASTA)) = CType(Me.txtFechaHasta.Text, DateTime)
                        vlo_DrFila.Item(Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.USUARIO)) = Me.Usuario.UserName

                        Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Rows.Add(vlo_DrFila)

                        CargarFuncionario(Me.ddlEncargado.SelectedValue.Split(",").GetValue(0))

                        vlo_DrFilaVista = Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).NewRow
                        vlo_DrFilaVista.Item(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Columns(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)) = Me.Empleado.ID_PERSONAL
                        vlo_DrFilaVista.Item(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Columns(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)) = String.Format("{0} {1} {2}", Me.Empleado.NOMBRE, Me.Empleado.APELLIDO1, Me.Empleado.APELLIDO2)
                        vlo_DrFilaVista.Item(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Columns(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA)) = CargaDescripcionArea()
                        vlo_DrFilaVista.Item(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Columns(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NUM_EMPLEADO)) = Me.ddlEncargado.SelectedValue.Split(",").GetValue(0)
                        vlo_DrFilaVista.Item(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Columns(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_SECTOR_TALLER)) = Me.ddlEncargado.SelectedValue.Split(",").GetValue(1)
                        vlo_DrFilaVista.Item(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Columns(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION)) = Me.IdUbicacion
                        vlo_DrFilaVista.Item(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Columns(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO)) = Me.IdOrdenTrabajo
                        vlo_DrFilaVista.Item(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Columns(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ETAPA_ORDEN_TRABAJO)) = EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA
                        vlo_DrFilaVista.Item(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Columns(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO)) = Cargo.ENCARGADO
                        vlo_DrFilaVista.Item(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Columns(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_DESDE)) = CType(Me.txtFechaDesde.Text, DateTime)
                        vlo_DrFilaVista.Item(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Columns(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA)) = CType(Me.txtFechaHasta.Text, DateTime)
                        vlo_DrFilaVista.Item(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Columns(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.USUARIO)) = Me.Usuario.UserName

                        Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Rows.Add(vlo_DrFilaVista)

                        If Me.DsOperarioOrdenTrabajoVistaEncargado IsNot Nothing AndAlso Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Rows.Count > 0 Then

                            Dim vlo_DataView As New Data.DataView(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0))
                            vlo_DataView.Sort = String.Format("{0} {1}", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA, Ordenamiento.ASCENDENTE)

                            With Me.rpEncargado
                                .DataSource = vlo_DataView
                                .DataMember = Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).TableName
                                .DataBind()
                                .Visible = True
                            End With
                        Else
                            With Me.rpEncargado
                                .DataSource = Nothing
                                .DataBind()
                                .Visible = False
                            End With
                        End If

                        Me.ddlEncargado.SelectedValue = String.Empty
                        Me.txtFechaDesde.Text = String.Empty
                        Me.txtFechaHasta.Text = String.Empty

                    Else
                        MostrarAlertaError("No puede existir traslape de fechas entre encargados.")
                    End If
                Else
                    MostrarAlertaError("El Funcionario ya esta registrado como colaborador.")
                End If
            Else
                MostrarAlertaError("El registro ya existe.")
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Agregar un registro en el dataset de OTT_OPERARIO_ORDEN_TRAB, colaborador del proyecto
    ''' Ademas en el dataset de OTT_TIEMPO_OPERARIO
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregarFuncionarioColaborador()
        Dim vlo_DrFilaOperario As Data.DataRow
        Dim vlo_DrFilaTiempo As Data.DataRow
        Dim vlo_DrFilaTiempoVista As Data.DataRow
        Dim vlo_DrFilaFindEncargado As Data.DataRow
        Dim vlo_DrFilaFindColaborador As Data.DataRow

        Try

            vlo_DrFilaFindEncargado = Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Rows.Find(New Object() {
                                                          Me.ddlFuncionario.SelectedValue.Split(",").GetValue(0),
                                                          Me.ddlFuncionario.SelectedValue.Split(",").GetValue(1),
                                                          Me.IdUbicacion,
                                                          Me.IdOrdenTrabajo,
                                                          EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA})

            If vlo_DrFilaFindEncargado Is Nothing Then

                vlo_DrFilaFindColaborador = Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Rows.Find(New Object() {
                                                          Me.ddlFuncionario.SelectedValue.Split(",").GetValue(0),
                                                          Me.ddlFuncionario.SelectedValue.Split(",").GetValue(1),
                                                          Me.IdUbicacion,
                                                          Me.IdOrdenTrabajo,
                                                          EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA})

                If vlo_DrFilaFindColaborador Is Nothing Then

                    vlo_DrFilaOperario = Me.DsOperarioOrdenTrabajoColaborador.Tables(0).NewRow
                    vlo_DrFilaOperario.Item(Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO)) = Me.ddlFuncionario.SelectedValue.Split(",").GetValue(0)
                    vlo_DrFilaOperario.Item(Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER)) = Me.ddlFuncionario.SelectedValue.Split(",").GetValue(1)
                    vlo_DrFilaOperario.Item(Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION)) = Me.IdUbicacion
                    vlo_DrFilaOperario.Item(Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO)) = Me.IdOrdenTrabajo
                    vlo_DrFilaOperario.Item(Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO)) = EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA
                    vlo_DrFilaOperario.Item(Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.CARGO)) = Cargo.COLABORADOR
                    vlo_DrFilaOperario.Item(Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.USUARIO)) = Me.Usuario.UserName

                    Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Rows.Add(vlo_DrFilaOperario)


                    vlo_DrFilaTiempo = Me.DsTiempoOperarioColaborador.Tables(0).NewRow

                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.NUM_EMPLEADO)) = Me.ddlFuncionario.SelectedValue.Split(",").GetValue(0)
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER)) = Me.ddlFuncionario.SelectedValue.Split(",").GetValue(1)
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_UBICACION)) = Me.IdUbicacion
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO)) = Me.IdOrdenTrabajo
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO)) = EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.TIEMPO)) = Me.txtTiempo.Text
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO)) = Me.ddlUnidadTiempo.SelectedValue
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.CLASIFICACION)) = Clasificacion.ESTIMADO
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.USUARIO)) = Me.Usuario.UserName

                    Me.DsTiempoOperarioColaborador.Tables(0).Rows.Add(vlo_DrFilaTiempo)

                    CargarFuncionario(Me.ddlFuncionario.SelectedValue.Split(",").GetValue(0))

                    vlo_DrFilaTiempoVista = Me.DsTiempoOperarioVistaColaborador.Tables(0).NewRow

                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.NUM_EMPLEADO)) = Me.ddlFuncionario.SelectedValue.Split(",").GetValue(0)
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.ID_SECTOR_TALLER)) = Me.ddlFuncionario.SelectedValue.Split(",").GetValue(1)
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.ID_UBICACION)) = Me.IdUbicacion
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.ID_ORDEN_TRABAJO)) = Me.IdOrdenTrabajo
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.ID_ETAPA_ORDEN_TRABAJO)) = EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.TIEMPO)) = Me.txtTiempo.Text
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.ID_UNIDAD_TIEMPO)) = Me.ddlUnidadTiempo.SelectedValue
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.CLASIFICACION)) = Clasificacion.ESTIMADO
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.USUARIO)) = Me.Usuario.UserName
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.CEDULA)) = Me.Empleado.ID_PERSONAL
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.NOMBRE_EMPLEADO)) = String.Format("{0} {1} {2}", Me.Empleado.NOMBRE, Me.Empleado.APELLIDO1, Me.Empleado.APELLIDO2)
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.DESC_AREA)) = RetornaDescripcionAreaFuncionario()
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.DESC_TIEMPO_REAL)) = String.Format("{0} {1}", Me.txtTiempo.Text, Me.ddlUnidadTiempo.SelectedItem.ToString)

                    Me.DsTiempoOperarioVistaColaborador.Tables(0).Rows.Add(vlo_DrFilaTiempoVista)

                    If Me.DsTiempoOperarioVistaColaborador IsNot Nothing AndAlso Me.DsTiempoOperarioVistaColaborador.Tables(0).Rows.Count > 0 Then

                        With Me.rpFuncionarios
                            .DataSource = Me.DsTiempoOperarioVistaColaborador
                            .DataMember = Me.DsTiempoOperarioVistaColaborador.Tables(0).TableName
                            .DataBind()
                            .Visible = True
                        End With
                    Else
                        With Me.rpFuncionarios
                            .DataSource = Nothing
                            .DataBind()
                            .Visible = False
                        End With
                    End If

                    Me.ddlFuncionario.SelectedValue = String.Empty
                    Me.txtTiempo.Text = String.Empty
                    Me.lblAreaFuncionario.Text = String.Empty

                Else
                    MostrarAlertaError("El registro ya existe.")
                End If
            Else

                vlo_DrFilaFindColaborador = Me.DsTiempoOperarioColaborador.Tables(0).Rows.Find(New Object() {Me.ddlFuncionario.SelectedValue.Split(",").GetValue(0)})

                If vlo_DrFilaFindColaborador Is Nothing Then

                    vlo_DrFilaTiempo = Me.DsTiempoOperarioColaborador.Tables(0).NewRow

                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.NUM_EMPLEADO)) = Me.ddlFuncionario.SelectedValue.Split(",").GetValue(0)
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER)) = Me.ddlFuncionario.SelectedValue.Split(",").GetValue(1)
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_UBICACION)) = Me.IdUbicacion
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO)) = Me.IdOrdenTrabajo
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO)) = EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.TIEMPO)) = Me.txtTiempo.Text
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO)) = Me.ddlUnidadTiempo.SelectedValue
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.CLASIFICACION)) = Clasificacion.ESTIMADO
                    vlo_DrFilaTiempo.Item(Me.DsTiempoOperarioColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.USUARIO)) = Me.Usuario.UserName

                    Me.DsTiempoOperarioColaborador.Tables(0).Rows.Add(vlo_DrFilaTiempo)

                    CargarFuncionario(Me.ddlFuncionario.SelectedValue.Split(",").GetValue(0))

                    vlo_DrFilaTiempoVista = Me.DsTiempoOperarioVistaColaborador.Tables(0).NewRow

                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.NUM_EMPLEADO)) = Me.ddlFuncionario.SelectedValue.Split(",").GetValue(0)
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.ID_SECTOR_TALLER)) = Me.ddlFuncionario.SelectedValue.Split(",").GetValue(1)
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.ID_UBICACION)) = Me.IdUbicacion
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.ID_ORDEN_TRABAJO)) = Me.IdOrdenTrabajo
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.ID_ETAPA_ORDEN_TRABAJO)) = EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.TIEMPO)) = Me.txtTiempo.Text
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.ID_UNIDAD_TIEMPO)) = Me.ddlUnidadTiempo.SelectedValue
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.CLASIFICACION)) = Clasificacion.ESTIMADO
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.USUARIO)) = Me.Usuario.UserName
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.CEDULA)) = Me.Empleado.ID_PERSONAL
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.NOMBRE_EMPLEADO)) = String.Format("{0} {1} {2}", Me.Empleado.NOMBRE, Me.Empleado.APELLIDO1, Me.Empleado.APELLIDO2)
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.DESC_AREA)) = RetornaDescripcionAreaFuncionario()
                    vlo_DrFilaTiempoVista.Item(Me.DsTiempoOperarioVistaColaborador.Tables(0).Columns(Modelo.V_OTT_TIEMPO_OPERARIOLST.DESC_TIEMPO_REAL)) = String.Format("{0} {1}", Me.txtTiempo.Text, Me.ddlUnidadTiempo.SelectedItem.ToString)

                    Me.DsTiempoOperarioVistaColaborador.Tables(0).Rows.Add(vlo_DrFilaTiempoVista)

                    If Me.DsTiempoOperarioVistaColaborador IsNot Nothing AndAlso Me.DsTiempoOperarioVistaColaborador.Tables(0).Rows.Count > 0 Then

                        With Me.rpFuncionarios
                            .DataSource = Me.DsTiempoOperarioVistaColaborador
                            .DataMember = Me.DsTiempoOperarioVistaColaborador.Tables(0).TableName
                            .DataBind()
                            .Visible = True
                        End With
                    Else
                        With Me.rpFuncionarios
                            .DataSource = Nothing
                            .DataBind()
                            .Visible = False
                        End With
                    End If

                    Me.ddlFuncionario.SelectedValue = String.Empty
                    Me.txtTiempo.Text = String.Empty
                    Me.lblAreaFuncionario.Text = String.Empty

                Else
                    MostrarAlertaError("El encargado ya posee un tiempo asignado.")
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Borra un elemento de la vista temporal de encargado
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BorrarEncargado(pvn_NumEmpleado As Integer, pvn_IdSectorTaller As Integer)
        Dim vlo_DrFila As Data.DataRow

        Try

            vlo_DrFila = Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Rows.Find(New Object() {pvn_NumEmpleado})

            If vlo_DrFila IsNot Nothing Then
                vlo_DrFila.Delete()
                Me.DsOperarioOrdenTrabajoEncargado.Tables(0).Rows.Find(New Object() {
                                                         pvn_NumEmpleado,
                                                         pvn_IdSectorTaller,
                                                         Me.IdUbicacion,
                                                         Me.IdOrdenTrabajo,
                                                         EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA}).Delete()

                If Me.DsOperarioOrdenTrabajoVistaEncargado IsNot Nothing AndAlso Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).Rows.Count > 0 Then

                    Dim vlo_DataView As New Data.DataView(Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0))
                    vlo_DataView.Sort = String.Format("{0} {1}", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA, Ordenamiento.ASCENDENTE)

                    With Me.rpEncargado
                        .DataSource = vlo_DataView
                        .DataMember = Me.DsOperarioOrdenTrabajoVistaEncargado.Tables(0).TableName
                        .DataBind()
                        .Visible = True
                    End With
                Else
                    With Me.rpEncargado
                        .DataSource = Nothing
                        .DataBind()
                        .Visible = False
                    End With
                End If

            Else
                MostrarAlertaError("No ha sido posible borrar el registro.")
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Borra un elemento de la vista temporal de colaboradores
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BorrarColaborador(pvn_NumEmpleado As Integer, pvn_IdSectorTaller As Integer)
        Dim vlo_DrFila As Data.DataRow

        Try

            vlo_DrFila = Me.DsTiempoOperarioVistaColaborador.Tables(0).Rows.Find(New Object() {pvn_NumEmpleado})

            If vlo_DrFila IsNot Nothing Then
                vlo_DrFila.Delete()
                Me.DsOperarioOrdenTrabajoColaborador.Tables(0).Rows.Find(New Object() {
                                                         pvn_NumEmpleado,
                                                        pvn_IdSectorTaller,
                                                         Me.IdUbicacion,
                                                         Me.IdOrdenTrabajo,
                                                         EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA}).Delete()

                Me.DsTiempoOperarioColaborador.Tables(0).Rows.Find(New Object() {pvn_NumEmpleado}).Delete()

                If Me.DsTiempoOperarioVistaColaborador IsNot Nothing AndAlso Me.DsTiempoOperarioVistaColaborador.Tables(0).Rows.Count > 0 Then

                    With Me.rpFuncionarios
                        .DataSource = Me.DsTiempoOperarioVistaColaborador
                        .DataMember = Me.DsTiempoOperarioVistaColaborador.Tables(0).TableName
                        .DataBind()
                        .Visible = True
                    End With
                Else
                    With Me.rpFuncionarios
                        .DataSource = Nothing
                        .DataBind()
                        .Visible = False
                    End With
                End If

            Else
                MostrarAlertaError("No ha sido posible borrar el registro.")
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Carga el empleado, segun el numeor de emplaedo que obtenga por parametro
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Sub CargarFuncionario(pvn_NumEmpleado As Integer)
        Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            Me.Empleado = vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
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
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Se comunica  con el web servise para modificar el nombre del proyecto de la OT
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ModificarNombreProyecto() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
        Dim vlo_result As Integer

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOttOrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, Me.IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo))

            vlo_EntOttOrdenTrabajo.NombreProyecto = Me.txtNombreProyecto.Text

            vlo_result = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttOrdenTrabajo)

            If vlo_result > 0 Then
                CargarOrdenTrabajo(Me.IdUbicacion, Me.IdOrdenTrabajo)
            End If

            Return vlo_result > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Retorna la descripcion del area profesional del encargado seleccionado en el combo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargaDescripcionArea() As String
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_Catalogos.OTM_AREA_PROFESIONAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_AREA_PROFESIONAL.ID_AREA_PROFESIONAL,
                    vlo_Ws_OT_Catalogos.OTF_OPERARIO_AREA_ObtenerRegistro(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_OPERARIO_AREA.NUM_EMPLEADO,
                                      Me.ddlEncargado.SelectedValue.Split(",").GetValue(0), Modelo.OTF_OPERARIO_AREA.ID_SECTOR_TALLER,
                                      Me.ddlEncargado.SelectedValue.Split(",").GetValue(1))).IdAreaProfesional)).Descripcion
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga datos del funcionario, para poder ver cual es el área profesional
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function RetornaDescripcionAreaFuncionario() As String
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTF_OPERARIO_AREA_ListarRegistrosLista(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                String.Format("{0} = {1} AND {2} = {3}", Modelo.V_OTF_OPERARIO_AREALST.NUM_EMPLEADO, Me.ddlFuncionario.SelectedValue.Split(",").GetValue(0), Modelo.V_OTF_OPERARIO_AREALST.ID_SECTOR_TALLER, Me.ddlFuncionario.SelectedValue.Split(",").GetValue(1)),
                                String.Empty,
                                False,
                                0,
                                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                Return vlo_DsDatos.Tables(0).Rows(0)(Modelo.V_OTF_OPERARIO_AREALST.AREA).ToString
            Else
                Return "-"
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Se comunica con el servicio web para almacenar los datos de la evaluacion, solamente guardar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function GuardarEvaluacion() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.OrdenTrabajo.NombreProyecto = Me.txtNombreProyecto.Text
            Me.OrdenTrabajo.Usuario = Me.Usuario.UserName

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ProcesarEvaluacion(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        Me.OrdenTrabajo, Me.DsOperarioOrdenTrabajoEncargado, Me.DsOperarioOrdenTrabajoColaborador, Me.DsTiempoOperarioColaborador, Me.GuardarEnviar,
                        Me.CondicionOperarioEncargado, Me.CondicionOperarioColaborador, Me.CondicionTiempoColaborador) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Ordenar ascendente o descendente la lista
    ''' </summary>
    ''' <param name="pvc_NombreColumna"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerSortExpressionEncargado(pvc_NombreColumna As String) As String
        If String.IsNullOrWhiteSpace(UltimoSortColumnEncargado) OrElse pvc_NombreColumna.CompareTo(UltimoSortColumnEncargado) <> 0 Then
            UltimoSortColumnEncargado = pvc_NombreColumna
            UltimoSortDirectionEncargado = SortDirection.Ascending
        Else
            If UltimoSortDirectionEncargado = SortDirection.Ascending Then
                UltimoSortDirectionEncargado = SortDirection.Descending
            Else
                UltimoSortDirectionEncargado = SortDirection.Ascending

            End If
        End If
        '0 nombre de la columna y 1 direccion de ordenamiento
        UltimoSortExpressionEncargado = String.Format("{0} {1}", UltimoSortColumnEncargado, IIf(UltimoSortDirectionEncargado = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))
        Return UltimoSortExpressionEncargado
    End Function

    ''' <summary>
    ''' Ordenar ascendente o descendente la lista
    ''' </summary>
    ''' <param name="pvc_NombreColumna"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerSortExpressionFuncionario(pvc_NombreColumna As String) As String
        If String.IsNullOrWhiteSpace(UltimoSortColumnFuncionario) OrElse pvc_NombreColumna.CompareTo(UltimoSortColumnFuncionario) <> 0 Then
            UltimoSortColumnFuncionario = pvc_NombreColumna
            UltimoSortDirectionFuncionario = SortDirection.Ascending
        Else
            If UltimoSortDirectionFuncionario = SortDirection.Ascending Then
                UltimoSortDirectionFuncionario = SortDirection.Descending
            Else
                UltimoSortDirectionFuncionario = SortDirection.Ascending

            End If
        End If
        '0 nombre de la columna y 1 direccion de ordenamiento
        UltimoSortExpressionFuncionario = String.Format("{0} {1}", UltimoSortColumnFuncionario, IIf(UltimoSortDirectionFuncionario = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))
        Return UltimoSortExpressionFuncionario
    End Function

#End Region

End Class
