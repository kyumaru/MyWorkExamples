Imports Utilerias.OrdenesDeTrabajo
Imports System.Data

Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_Aprovisionamiento
    Inherits System.Web.UI.Page
#Region "Propiedades"

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

                    Me.HileraSeleccionada = WebUtils.LeerParametro(Of String)("pvc_HileraSeleccionada")
                    Me.Modo = WebUtils.LeerParametro(Of String)("pvn_Modo")

                    CargarComboFiltrarPor()
                    CargarComboCategoria()
                    CargarComboPartida()

                    Me.trCategoria.Visible = False
                    Me.trSubCategoria.Visible = False
                    Me.trPartida.Visible = False

                    CargarLista(ObtenerCondicionBusqueda(), String.Format("{0} {1}", Modelo.V_OTM_MATERIALLST.ID_MATERIAL, Ordenamiento.ASCENDENTE))

                    If Me.Modo = 2 Then
                        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
                        Me.NumeroGestion = WebUtils.LeerParametro(Of Integer)("pvn_NumeroGestion")
                        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
                    End If

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
                    Me.Session.Add("pvn_Modo", 1)
                ElseIf Me.Modo = 2 Then
                    Me.HileraMateriales = String.Format("{0},{1}", Me.HileraMateriales, Me.HileraSeleccionada)
                    Me.Session.Add("pvc_HileraMateriales", Me.HileraMateriales)
                    Me.Session.Add("pvn_Modo", 2)
                    Me.Session.Add("pvn_IdUbicacion", Me.IdUbicacion)
                    Me.Session.Add("pvn_Anno", Me.Anno)
                    Me.Session.Add("pvn_NumeroGestion", Me.NumeroGestion)
                    Me.Session.Add("pvn_Formulario", 1) 'variable utilizada cuando no necesito obtener la hilera de materiales en la pantalla de consulta gestion por aprovisionamiento
                End If


                Response.Redirect("Frm_OT_ConsultaAprovisionamiento.aspx", False)
            Else
                MostrarAlertaError("Debe seleccionar al menos un registro")
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
            Response.Redirect("Lst_OT_Aprovisionamiento.aspx", False)

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub ddlFiltrarPor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFiltrarPor.SelectedIndexChanged
        Try
            Select Case ddlFiltrarPor.SelectedValue
                Case Is = String.Empty
                    trCategoria.Visible = False
                    trSubCategoria.Visible = False
                    trPartida.Visible = False

                Case Is = FiltroPor.ARTICULOS_CERO
                    trCategoria.Visible = False
                    trSubCategoria.Visible = False
                    trPartida.Visible = False

                Case Is = FiltroPor.PUNTO_REORDEN
                    trCategoria.Visible = False
                    trSubCategoria.Visible = False
                    trPartida.Visible = False

                Case Is = FiltroPor.CATEGORIA
                    trCategoria.Visible = True
                    trSubCategoria.Visible = True
                    trPartida.Visible = False

                Case Is = FiltroPor.PARTIDA
                    trCategoria.Visible = False
                    trSubCategoria.Visible = False
                    trPartida.Visible = True
            End Select

            ddlFiltroCategoria.SelectedValue = String.Empty
            ddlFiltroSubCategoria.SelectedValue = String.Empty
            ddlFiltroPartida.SelectedValue = String.Empty
            chkSeleccionarTodos.Checked = False

            CargarListaBusqueda()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub ddlFiltroSubCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFiltroSubCategoria.SelectedIndexChanged
        Try
            chkSeleccionarTodos.Checked = False
            CargarListaBusqueda()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub ddlFiltroCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFiltroCategoria.SelectedIndexChanged
        Try
            chkSeleccionarTodos.Checked = False
            CargarComboSubCategoria()
            CargarListaBusqueda()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub ddlFiltroPartida_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFiltroPartida.SelectedIndexChanged
        Try
            chkSeleccionarTodos.Checked = False
            CargarListaBusqueda()

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

    Private Sub CargarComboFiltrarPor()
        Try

            Me.ddlFiltrarPor.Items.Clear()
            Me.ddlFiltrarPor.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
            Me.ddlFiltrarPor.Items.Add(New ListItem(FiltroPor.ARTICULOS_CERO_STR, FiltroPor.ARTICULOS_CERO))
            Me.ddlFiltrarPor.Items.Add(New ListItem(FiltroPor.PUNTO_REORDEN_STR, FiltroPor.PUNTO_REORDEN))
            Me.ddlFiltrarPor.Items.Add(New ListItem(FiltroPor.CATEGORIA_STR, FiltroPor.CATEGORIA))
            Me.ddlFiltrarPor.Items.Add(New ListItem(FiltroPor.PARTIDA_STR, FiltroPor.PARTIDA))

            ddlFiltrarPor.SelectedValue = String.Empty

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub CargarComboPartida()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_dsDatos As Data.DataSet
        Dim vlo_PeriodoActual As Integer

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_PeriodoActual = Date.Now.Year

            Me.ddlFiltroPartida.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_dsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarVOttPartidasPresup(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.V_OTT_PARTIDAS_PRESUP.NUM_PERIODO, vlo_PeriodoActual), String.Empty, False, 0, 0)

            If vlo_dsDatos.Tables.Count > 0 AndAlso vlo_dsDatos.Tables(0).Rows.Count <= 0 Then

                vlo_PeriodoActual = vlo_PeriodoActual - 1
                'Se cargan los del periodo anterior en caso de estar vacío
                vlo_dsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarVOttPartidasPresup(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.V_OTT_PARTIDAS_PRESUP.NUM_PERIODO, vlo_PeriodoActual), String.Empty, False, 0, 0)
            End If

            For Each vlo_fila As Data.DataRow In vlo_dsDatos.Tables(0).Rows
                Me.ddlFiltroPartida.Items.Add(New ListItem(String.Format("({0}) {1}", vlo_fila(Modelo.V_OTT_PARTIDAS_PRESUP.ID_EGRESO), vlo_fila(Modelo.V_OTT_PARTIDAS_PRESUP.NOM_EGRESO)), vlo_fila(Modelo.V_OTT_PARTIDAS_PRESUP.ID_EGRESO)))
            Next


        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarComboCategoria()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        Dim vlc_Condicion As String = String.Empty
        Dim vlc_Orden As String = String.Empty

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials


        Try
            vlc_Condicion = String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_MATERIAL.ID_UBICACION_ADMINISTRA, AutorizadoUbicacion.IdUbicacionAdministra)


            vlc_Orden = String.Format("{0} {1}", Modelo.OTM_CATEGORIA_MATERIAL.DESCRIPCION, Ordenamiento.ASCENDENTE)
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_CATEGORIA_MATERIAL_ListarRegistros(System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB).ToString,
                                                                                                   System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB).ToString,
                                                                                                   vlc_Condicion,
                                                                                                   vlc_Orden,
                                                                                                   False,
                                                                                                   0,
                                                                                                   0)

            Me.ddlFiltroCategoria.Items.Clear()
            Me.ddlFiltroCategoria.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
            If Not vlo_DsDatos.Tables(0) Is Nothing Then
                If vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    With Me.ddlFiltroCategoria
                        .DataSource = vlo_DsDatos
                        .DataTextField = Modelo.OTM_CATEGORIA_MATERIAL.DESCRIPCION
                        .DataValueField = Modelo.OTM_CATEGORIA_MATERIAL.ID_CATEGORIA_MATERIAL
                        .DataBind()
                    End With
                End If
            End If


            CargarComboSubCategoria()


        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Carga el combo de especialidades
    ''' </summary>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>12/01/2016</creationDate>
    ''' <remarks></remarks>
    Private Sub CargarComboSubCategoria()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsCursoCatedra As DataSet

        Dim vlc_Condicion As String = String.Empty
        Dim vlc_Orden As String = String.Empty

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials


        Try

            If String.IsNullOrEmpty(Me.ddlFiltroCategoria.SelectedValue) Then
                LimpiarComboSubCategoria()
            Else

                vlc_Condicion = String.Format("{0} = {1}", Modelo.OTM_SUBCATEGORIA_CATEGOR.ID_CATEGORIA_MATERIAL, Me.ddlFiltroCategoria.SelectedValue)
                vlc_Orden = String.Format("{0} {1}", Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.DESCRIPCION_SUBCATEG_MATE, Ordenamiento.ASCENDENTE)
                vlo_DsCursoCatedra = vlo_Ws_OT_Catalogos.OTM_SUBCATEGORIA_CATEGOR_ListarRegistrosLista(System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB).ToString,
                                                                                                       System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB).ToString,
                                                                                                       vlc_Condicion,
                                                                                                       vlc_Orden,
                                                                                                       False,
                                                                                                       0,
                                                                                                       0)

                Me.ddlFiltroSubCategoria.Items.Clear()
                Me.ddlFiltroSubCategoria.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
                If Not vlo_DsCursoCatedra.Tables(0) Is Nothing Then
                    If vlo_DsCursoCatedra.Tables(0).Rows.Count > 0 Then
                        With Me.ddlFiltroSubCategoria
                            .DataSource = vlo_DsCursoCatedra
                            .DataTextField = Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.DESCRIPCION_SUBCATEG_MATE
                            .DataValueField = Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.ID_SUBCATEGORIA_MATERIAL
                            .DataBind()
                        End With
                    End If
                End If

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub LimpiarComboSubCategoria()
        Me.ddlFiltroSubCategoria.Items.Clear()
        Me.ddlFiltroSubCategoria.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
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
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsMateriales = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                False,
                0,
                0)

            Me.DsMateriales.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsMateriales.Tables(0).Columns(Modelo.V_OTM_MATERIALLST.ID_MATERIAL)}

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
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
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
                    .Visible = True
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado();")
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
                    If vln_Registro = vlo_Row(Modelo.V_OTM_MATERIALLST.ID_MATERIAL) Then
                        vlo_Row(Modelo.V_OTM_MATERIALLST.SELECCIONADO) = "1"
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

        vlc_Condicion = String.Format("{0} = '{1}' AND {2} = {3}", Modelo.V_OTM_MATERIALLST.ESTADO, Estado.ACTIVO, Modelo.V_OTM_MATERIALLST.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra)

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltrarPor.SelectedValue) Then
            Select Case Me.ddlFiltrarPor.SelectedValue
                Case Is = FiltroPor.ARTICULOS_CERO
                    If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                        vlc_Condicion = String.Format("{0} = 0", Modelo.V_OTM_MATERIALLST.CANTIDAD_DISPONIBLE)
                    Else
                        vlc_Condicion = String.Format("{0} AND {1} = 0", vlc_Condicion, Modelo.V_OTM_MATERIALLST.CANTIDAD_DISPONIBLE)
                    End If

                Case Is = FiltroPor.PUNTO_REORDEN
                    If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                        vlc_Condicion = String.Format("{0} >= {1}", Modelo.V_OTM_MATERIALLST.CANTIDAD_DISPONIBLE, Modelo.V_OTM_MATERIALLST.PUNTO_REORDEN)
                    Else
                        vlc_Condicion = String.Format("{0} AND {1} >= {2}", vlc_Condicion, Modelo.V_OTM_MATERIALLST.CANTIDAD_DISPONIBLE, Modelo.V_OTM_MATERIALLST.PUNTO_REORDEN)
                    End If

                Case Is = FiltroPor.CATEGORIA
                    If Not String.IsNullOrWhiteSpace(Me.ddlFiltroCategoria.SelectedValue) Then
                        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                            vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTM_MATERIALLST.ID_CATEGORIA_MATERIAL, Me.ddlFiltroCategoria.SelectedValue)
                        Else
                            vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTM_MATERIALLST.ID_CATEGORIA_MATERIAL, Me.ddlFiltroCategoria.SelectedValue)
                        End If
                    End If

                    If Not String.IsNullOrWhiteSpace(Me.ddlFiltroSubCategoria.SelectedValue) Then
                        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                            vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTM_MATERIALLST.ID_SUBCATEGORIA_MATERIAL, Me.ddlFiltroSubCategoria.SelectedValue)
                        Else
                            vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTM_MATERIALLST.ID_SUBCATEGORIA_MATERIAL, Me.ddlFiltroSubCategoria.SelectedValue)
                        End If
                    End If

                Case Is = FiltroPor.PARTIDA
                    If Not String.IsNullOrWhiteSpace(Me.ddlFiltroPartida.SelectedValue) Then
                        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                            vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTM_MATERIALLST.PARTIDA_PRESUPUESTARIA, Me.ddlFiltroPartida.SelectedValue)
                        Else
                            vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTM_MATERIALLST.PARTIDA_PRESUPUESTARIA, Me.ddlFiltroPartida.SelectedValue)
                        End If
                    End If
            End Select
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
                If vlo_FIla(Modelo.V_OTM_MATERIALLST.SELECCIONADO).ToString = "1" Then
                    vlb_PoseeSeleccionados = True
                    Me.HileraMateriales = String.Format("{0}{1},", Me.HileraMateriales, vlo_FIla(Modelo.V_OTM_MATERIALLST.ID_MATERIAL))
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
