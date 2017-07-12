Imports Utilerias.OrdenesDeTrabajo
Imports System.Data

Partial Class OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_GestionCompraSuministros
    Inherits System.Web.UI.Page
#Region "Propiedades"

    ''' <summary>
    ''' ubicacion del usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>23/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property ParametroViaCompraContrato As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Get
            Return CType(ViewState("ParametroViaCompraContrato"), Wsr_OT_Catalogos.EntOtpParametroUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtpParametroUbicacion)
            ViewState("ParametroViaCompraContrato") = value
        End Set
    End Property

    ''' <summary>
    ''' data set para materiales iniciales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsMateriales As Data.DataSet
        Get
            Return CType(ViewState("DsMateriales"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsMateriales") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en sesion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>06/09/2016</creationDate>
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
    ''' ubicacion del usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>23/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property AutorizadoUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Get
            Return CType(ViewState("AutorizadoUbicacion"), Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
            ViewState("AutorizadoUbicacion") = value
        End Set
    End Property

    Private Property HileraMateriales As String
        Get
            Return CType(ViewState("HileraMateriales"), String)
        End Get
        Set(value As String)
            ViewState("HileraMateriales") = value
        End Set
    End Property

    Private Property HileraSeleccionada As String
        Get
            Return CType(ViewState("HileraSeleccionada"), String)
        End Get
        Set(value As String)
            ViewState("HileraSeleccionada") = value
        End Set
    End Property

    Private Property Modo As Integer
        Get
            Return CType(ViewState("Modo"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Modo") = value
        End Set
    End Property

    Public Property IdUbicacion As Integer
        Get
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    Public Property IdViaCompraContrato As Integer
        Get
            Return CType(ViewState("IdViaCompraContrato"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdViaCompraContrato") = value
        End Set
    End Property

    Public Property Anno As Integer
        Get
            Return CType(ViewState("Anno"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Anno") = value
        End Set
    End Property

    Public Property NumeroGestion As Integer
        Get
            Return CType(ViewState("NumeroGestion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("NumeroGestion") = value
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
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try

                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If AutorizadoUbicacion.Existe Then
                    Me.ParametroViaCompraContrato = CargarParametro(Parametros.VALOR_SECUENCIA_SUMINISTROS)

                    Me.HileraSeleccionada = WebUtils.LeerParametro(Of String)("pvc_HileraSeleccionada")
                    Me.Modo = WebUtils.LeerParametro(Of String)("pvn_Modo")
                    Me.txtObservaciones.Text = WebUtils.LeerParametro(Of String)("pvc_Observaciones")

                    If Me.Modo = 2 Then
                        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
                        Me.IdViaCompraContrato = WebUtils.LeerParametro(Of Integer)("pvn_IdViaCompraContrato")
                        Me.NumeroGestion = WebUtils.LeerParametro(Of Integer)("pvn_NumeroGestion")
                        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
                    End If

                    CargarLista(ObtenerCondicionBusqueda(), String.Format("{0} {1}", Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL, Ordenamiento.ASCENDENTE))

                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ninguna ubicación autorizada, contactar con el administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If

            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el botón Buscar,
    ''' que se encuentra en el área de filtros.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            CargarListaBusqueda()
            'chkSeleccionarTodos.AutoPostBack = False
            'chkSeleccionarTodos.Checked = False
            'chkSeleccionarTodos.AutoPostBack = True
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub chkDetalle_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim vlo_CheckBox As CheckBox = CType(sender, CheckBox)

            If vlo_CheckBox.Checked Then
                Me.DsMateriales.Tables(0).Rows.Find(New Object() {vlo_CheckBox.Attributes("CommandArgument").ToString()})(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO) = "1"
            Else
                Me.DsMateriales.Tables(0).Rows.Find(New Object() {vlo_CheckBox.Attributes("CommandArgument").ToString()})(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO) = "0"
            End If
            WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
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
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click

        Try

            If ValidaGestion() Then
                
                If Me.Modo = 1 Then
                    Me.Session.Add("pvc_HileraMateriales", Me.HileraMateriales)
                    Me.Session.Add("pvc_Observaciones", Me.txtObservaciones.Text)
                    Me.Session.Add("pvn_Modo", 1)
                ElseIf Me.Modo = 2 Then
                    Me.HileraMateriales = String.Format("{0},{1}", Me.HileraMateriales, Me.HileraSeleccionada)
                    Me.Session.Add("pvc_HileraMateriales", Me.HileraMateriales)
                    Me.Session.Add("pvc_Observaciones", Me.txtObservaciones.Text)
                    Me.Session.Add("pvn_Modo", 2)
                    Me.Session.Add("pvn_IdUbicacion", Me.IdUbicacion)
                    Me.Session.Add("pvn_IdViaCompraContrato", Me.IdViaCompraContrato)
                    Me.Session.Add("pvn_Anno", Me.Anno)
                    Me.Session.Add("pvn_NumeroGestion", Me.NumeroGestion)
                    Me.Session.Add("pvn_Formulario", 1) 'variable utilizada cuando no necesito obtener la hilera de materiales en la pantalla de consulta gestion por suministros
                End If
                

                Response.Redirect("Frm_OT_ConsultaGestionCompraSuministros.aspx", False)
            Else
                MostrarAlertaError("Debe seleccionar al menos un registro")
                WebUtils.RegistrarScript(Me, "OcultarAreaFiltrosDeBusqueda", "ocultarAreaFiltrosDeBusqueda();")
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

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try

            Me.Session.Add("pvn_IdViaCompraSelec", Me.ParametroViaCompraContrato.Valor)
            Response.Redirect("Lst_OT_GestiónCompraListado.aspx", False)

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
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvc_Condicion">parámetro con la condicion de búsqueda</param>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsMateriales = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ListarVOtDetalleMaterial(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

            Me.DsMateriales.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsMateriales.Tables(0).Columns(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL)}

            If Me.HileraSeleccionada IsNot Nothing AndAlso Me.HileraSeleccionada <> String.Empty Then
                ProcesarMarcados()
            End If

            If Me.DsMateriales IsNot Nothing AndAlso Me.DsMateriales.Tables(0).Rows.Count > 0 Then
                With Me.rpDetalles
                    .DataSource = Me.DsMateriales
                    .DataMember = Me.DsMateriales.Tables(0).TableName
                    .DataBind()
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End With
            Else
                With Me.rpDetalles
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With
                MostrarAlertaNoHayDatos()
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
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaBusqueda()

        Try

            Dim vlo_DataViewFDetalles As New Data.DataView(Me.DsMateriales.Tables(0))
            vlo_DataViewFDetalles.RowFilter = ObtenerCondicionBusqueda()

            If vlo_DataViewFDetalles IsNot Nothing AndAlso vlo_DataViewFDetalles.Count > 0 Then
                With Me.rpDetalles
                    .DataSource = vlo_DataViewFDetalles
                    .DataMember = Me.DsMateriales.Tables(0).TableName
                    .DataBind()
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End With
            Else
                With Me.rpDetalles
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With
                MostrarAlertaNoHayDatos()
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Determina cual accion sera la que se debe realizar, seleccionar todos o desseleccionar ntodos
    ''' </summary>
    ''' <param name="pvb_SeleccionarTodos"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ProcesarSeleccion(pvb_SeleccionarTodos As Boolean)
        Dim vlo_CheckBox As CheckBox
        Dim vlo_HiddenField As HiddenField

        Try
            For Each item In Me.rpDetalles.Items
                vlo_CheckBox = CType(item.FindControl("chkDetalle"), CheckBox)
                vlo_CheckBox.Checked = pvb_SeleccionarTodos
                vlo_HiddenField = CType(item.FindControl("hdfIdDetalleMaterial"), HiddenField)

                If vlo_CheckBox.Checked Then
                    Me.DsMateriales.Tables(0).Rows.Find(New Object() {vlo_HiddenField.Value})(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO) = "1"
                Else
                    Me.DsMateriales.Tables(0).Rows.Find(New Object() {vlo_HiddenField.Value})(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO) = "0"
                End If

            Next
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Private Sub ProcesarMarcados()
        Dim vlo_Row As DataRow
        Dim vlo_Clave() As Object


        vlo_Row = Me.DsMateriales.Tables(0).NewRow

        vlo_Clave = Me.HileraSeleccionada.Split(",")

        
        Try
            For Each vlo_Row In Me.DsMateriales.Tables(0).Rows
                For Each vln_Registro In vlo_Clave
                    If vln_Registro = vlo_Row(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL) Then
                        vlo_Row(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO) = "1"
                    End If
                Next
            Next

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' arma la condicion de busqueda
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String

        vlc_Condicion = String.Format("{0} = '{1}' AND {2} = {3} AND {4} = {5} AND {6} = '{7}'", Modelo.V_OT_DETALLE_MATERIAL.ESTADO, EstadoRegistro.APROBADA, Modelo.V_OT_DETALLE_MATERIAL.ID_VIA_COMPRA_CONTRATO, Me.ParametroViaCompraContrato.Valor, Modelo.V_OT_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra, Modelo.V_OT_DETALLE_MATERIAL.VIA_DESPACHO, ViaDespacho.VIACOMPRA)

        If Not String.IsNullOrWhiteSpace(Me.txtNumOT.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, Me.txtNumOT.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, Me.txtNumOT.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtNumeroMaterial.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL, Me.txtNumeroMaterial.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL, Me.txtNumeroMaterial.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtDescMaterial.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OT_DETALLE_MATERIAL.DESCRIPCION, Me.txtDescMaterial.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OT_DETALLE_MATERIAL.DESCRIPCION, Me.txtDescMaterial.Text.Trim)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroFechaDesde.Text) And Not String.IsNullOrWhiteSpace(Me.txtFiltroFechaHasta.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} BETWEEN TO_DATE('{1}', 'dd/mm/yyyy') AND TO_DATE('{2}', 'dd/mm/yyyy')", Modelo.V_OT_DETALLE_MATERIAL.FECHA_ASIGNACION, Me.txtFiltroFechaDesde.Text, Me.txtFiltroFechaHasta.Text)
            Else
                vlc_Condicion = String.Format("{0} AND {1} BETWEEN TO_DATE('{2}', 'dd/mm/yyyy') AND TO_DATE('{3}', 'dd/mm/yyyy')", vlc_Condicion, Modelo.V_OT_DETALLE_MATERIAL.FECHA_ASIGNACION, Me.txtFiltroFechaDesde.Text, Me.txtFiltroFechaHasta.Text)
            End If
        End If

        Return vlc_Condicion
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>06/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ValidaGestion() As Boolean
        Dim vlb_PoseeSeleccionados As Boolean

        Try
            Me.HileraMateriales = String.Empty
            vlb_PoseeSeleccionados = False

            For Each vlo_FIla In Me.DsMateriales.Tables(0).Rows
                If vlo_FIla(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO).ToString = "1" Then
                    vlb_PoseeSeleccionados = True
                    Me.HileraMateriales = String.Format("{0}{1},", Me.HileraMateriales, vlo_FIla(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL))
                End If
            Next
            'Se remueve el ultimo caracter el cual es una "," al conformar el string
            Me.HileraMateriales = Mid(Me.HileraMateriales, 1, Len(Me.HileraMateriales) - 1)

            Return vlb_PoseeSeleccionados

        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' carga la ubicacion 
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>23/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarAutorizadoUbicacion(pvn_NumEmpleado As Integer) As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Dim Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        Ws_OT_Catalogos.Timeout = -1
        Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return Ws_OT_Catalogos.OTM_AUTORIZADO_UBICACION_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTM_AUTORIZADO_UBICACION.NUM_EMPLEADO, pvn_NumEmpleado))
        Catch ex As Exception
            Throw
        Finally
            If Ws_OT_Catalogos IsNot Nothing Then
                Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvn_IdParametro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargarParametro(pvn_IdParametro As Integer) As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Dim Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        Ws_OT_Catalogos.Timeout = -1
        Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, pvn_IdParametro))
        Catch ex As Exception
            Throw
        Finally
            If Ws_OT_Catalogos IsNot Nothing Then
                Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

#End Region
End Class
