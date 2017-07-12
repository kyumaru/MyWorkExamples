Imports Utilerias.OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo.Modelo
Imports System.Data

Partial Class OrdenesDeTrabajo_GestionesDeCompra_Frm_OT_ConsultaGestionCompraSuministros
    Inherits System.Web.UI.Page

#Region "Propiedades"

    Private Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    Public Property HileraMateriales As String
        Get
            Return CType(ViewState("HileraMateriales"), String)
        End Get
        Set(value As String)
            ViewState("HileraMateriales") = value
        End Set
    End Property

    Public Property Observaciones As String
        Get
            Return CType(ViewState("Observaciones"), String)
        End Get
        Set(value As String)
            ViewState("Observaciones") = value
        End Set
    End Property

    Private Property DsMateriales As DataSet
        Get
            Return CType(ViewState("DsMateriales"), DataSet)
        End Get
        Set(value As DataSet)
            ViewState("DsMateriales") = value
        End Set
    End Property

    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property

    Public Property GestionCompra As Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra
        Get
            Return CType(ViewState("GestionCompra"), Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra)
        End Get
        Set(value As Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra)
            ViewState("GestionCompra") = value
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

    Public Property Modo As Integer
        Get
            Return CType(ViewState("Modo"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Modo") = value
        End Set
    End Property

    Private Property AutorizadoUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Get
            Return CType(ViewState("AutorizadoUbicacion"), Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
            ViewState("AutorizadoUbicacion") = value
        End Set
    End Property

    Public Property ViaCompraSuministros As Integer
        Get
            Return CType(ViewState("ViaCompraSuministros"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("ViaCompraSuministros") = value
        End Set
    End Property

    Public Property NoCargarHilera As Integer
        Get
            Return CType(ViewState("NoCargarHilera"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("NoCargarHilera") = value
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
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim vlo_Wuc As Controles_wuc_OT_Lineas_Material_Detalle_Producto

        'Se agrega el evento de recargar del control para que a la hora de eliminar un item se refresque el formulario
        For Each vlo_Item In Me.rpMateriales.Items
            vlo_Wuc = CType(vlo_Item.FindControl("wuc_OT_Lineas_Material_Detalle_Producto"), Controles_wuc_OT_Lineas_Material_Detalle_Producto)
            AddHandler vlo_Wuc.Recargar, AddressOf RecargarFormulario
        Next

        If Not Page.IsPostBack Then
            Try

                Me.Usuario = New UsuarioActual
                Me.ViaCompraSuministros = CargarParametro(Parametros.VALOR_SECUENCIA_SUMINISTROS)
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If AutorizadoUbicacion.Existe Then
                    LeerParametrosSession()

                    If Me.Modo = 1 Then 'Ingreso a la pantalla por la VIA 1
                        Me.trTitulo.Visible = True
                        Me.trMatGestionar.Visible = False
                        Me.trNumGestionGeco.Visible = False
                        Me.btnCancelar.Text = "Regresar"
                        Me.GestionCompra = New Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra
                        Me.btnFinalizar.Visible = True

                        'Se carga el acordeon
                        CargarLista(Me.HileraMateriales)
                    ElseIf Me.Modo = 2 Then 'Ingreso a la pantalla por la VIA 2
                        Me.trTitulo.Visible = False
                        Me.trMatGestionar.Visible = True
                        Me.trNumGestionGeco.Visible = True


                        'Se obtiene la gestion de compra
                        CargarGestionCompra()

                        If Me.NoCargarHilera = 0 Then
                            ObtenerHileraMateriales()
                        End If


                        If Me.GestionCompra.Estado = EstadoGestionCompra.CREADA Then
                            Me.btnCancelar.Text = "Regresar"
                        Else
                            Me.btnCancelar.Text = "Cancelar"
                        End If

                        Me.Observaciones = Me.GestionCompra.Observaciones
                        Me.txtNumGestionGECO.Text = Me.GestionCompra.NumeroGestionGeco

                        If Me.GestionCompra.Estado = EstadoGestionCompra.INGRESO_GESTION_GECO Or Me.GestionCompra.Estado = EstadoGestionCompra.CREADA Then
                            Me.btnFinalizar.Visible = True
                        End If

                        'Se carga el acordeon
                        CargarLista(Me.HileraMateriales)
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
    ''' Databound del acordeon cuando se ingresa por la VIA 1
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpMateriales_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpMateriales.ItemDataBound
        Dim vlo_HtmlAnchor As HtmlAnchor
        Dim vlo_HtmlGenericControl As HtmlGenericControl
        Dim vlo_WebUserControl As Controles_wuc_OT_Lineas_Material_Detalle_Producto

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_HtmlAnchor = CType(e.Item.FindControl("ancorAcordeon"), HtmlAnchor)
            vlo_HtmlGenericControl = e.Item.FindControl("cuerpoAcordeon1")
            vlo_HtmlAnchor.HRef = "#" + vlo_HtmlGenericControl.ClientID

            vlo_WebUserControl = CType(e.Item.FindControl("wuc_OT_Lineas_Material_Detalle_Producto"), Controles_wuc_OT_Lineas_Material_Detalle_Producto)
            vlo_WebUserControl.Inicializar()

        End If
    End Sub

    ''' <summary>
    ''' Boton de Cancelar/Regresar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Try
            If Me.Modo = 1 Then 'Ingreso a la pantalla desde la VIA 1
                Me.Session.Add("pvc_HileraSeleccionada", Me.HileraMateriales)
                Me.Session.Add("pvn_Modo", Me.Modo)
                Me.Session.Add("pvc_Observaciones", Me.Observaciones)
                Response.Redirect("Frm_OT_GestionCompraSuministros.aspx", False)
            ElseIf Me.Modo = 2 Then 'Ingreso a la pantalla desde la VIA 2
                If Me.GestionCompra.Estado = EstadoGestionCompra.CREADA Then
                    Me.Session.Add("pvc_HileraSeleccionada", Me.HileraMateriales)
                    Me.Session.Add("pvn_Modo", Me.Modo)
                    Me.Session.Add("pvc_Observaciones", Me.Observaciones)
                    Me.Session.Add("pvn_IdUbicacion", Me.IdUbicacion)
                    Me.Session.Add("pvn_IdViaCompraContrato", Me.IdViaCompraContrato)
                    Me.Session.Add("pvn_Anno", Me.Anno)
                    Me.Session.Add("pvn_NumeroGestion", Me.NumeroGestion)
                    Response.Redirect("Frm_OT_GestionCompraSuministros.aspx", False)
                Else
                    Me.Session.Add("pvn_IdViaCompraSelec", Me.ViaCompraSuministros)
                    Response.Redirect("Lst_OT_GestiónCompraListado.aspx", False)
                End If
                
            End If
            
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Boton de guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Try
            If Me.Modo = 1 Then 'Ingreso a la pantalla desde la VIA 1
                If CrearGestionCompra() Then
                    Me.Session.Add("pvn_IdViaCompraSelec", Me.ViaCompraSuministros)
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
                Else
                    MostrarAlertaError("No ha sido posible registrar la gestión de compra")
                End If
            ElseIf Me.Modo = 2 Then 'Ingreso a la pantalla desde la VIA 2
                If Me.GestionCompra.Estado <> EstadoGestionCompra.CREADA Then
                    Me.GestionCompra.NumeroGestionGeco = Me.txtNumGestionGECO.Text

                    If ModificarGestionCompra(Me.GestionCompra) Then
                        Me.Session.Add("pvn_IdViaCompraSelec", Me.ViaCompraSuministros)
                        WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
                    Else
                        MostrarAlertaError("No ha sido posible registrar la gestión de compra")
                    End If
                Else
                    If GuardarMateriales() Then
                        Me.Session.Add("pvn_IdViaCompraSelec", Me.ViaCompraSuministros)
                        WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
                    Else
                        MostrarAlertaError("No ha sido posible modificar la gestión de compra")
                    End If

                End If

            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Boton de finalizar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles lnkAceptarAux.Click
        Try
            If Me.Modo = 1 Then 'Ingreso a la pantalla desde la VIA 1

                If CrearGestionCompraFinalizar() Then
                    Me.Session.Add("pvn_IdViaCompraSelec", Me.ViaCompraSuministros)
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
                Else
                    MostrarAlertaError("No ha sido posible registrar la gestión de compra")
                End If
            ElseIf Me.Modo = 2 Then 'Ingreso a la pantalla desde la VIA 2

                If Me.GestionCompra.Estado = EstadoGestionCompra.INGRESO_GESTION_GECO Then
                    Me.GestionCompra.Estado = EstadoGestionCompra.PENDIENTE_INGRESO_ALMACEN
                ElseIf Me.GestionCompra.Estado = EstadoGestionCompra.CREADA Then
                    Me.GestionCompra.Estado = EstadoGestionCompra.INGRESO_GESTION_GECO
                End If

                If ModificarGestionCompra(Me.GestionCompra) Then
                    Me.Session.Add("pvn_IdViaCompraSelec", Me.ViaCompraSuministros)
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
                Else
                    MostrarAlertaError("No ha sido posible registrar la gestión de compra")
                End If
            End If
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
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' lee y carga los parametros guardados en sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>29/08/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametrosSession()
        Try
            Me.HileraMateriales = WebUtils.LeerParametro(Of String)("pvc_HileraMateriales")
            Me.Observaciones = WebUtils.LeerParametro(Of String)("pvc_Observaciones")
            Me.Modo = WebUtils.LeerParametro(Of Integer)("pvn_Modo")
            Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
            Me.IdViaCompraContrato = WebUtils.LeerParametro(Of Integer)("pvn_IdViaCompraContrato")
            Me.NumeroGestion = WebUtils.LeerParametro(Of Integer)("pvn_NumeroGestion")
            Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
            Me.NoCargarHilera = WebUtils.LeerParametro(Of Integer)("pvn_Formulario")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Cargar lista para el padre del acordeon por la VIA 1
    ''' </summary>
    ''' <param name="pvc_HileraMateriales">parámetro con la condicion de búsqueda</param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_HileraMateriales As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ListarFcOtMatAgrupadosSum(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Empty,
                String.Empty,
                False,
                0,
                0,
                pvc_HileraMateriales)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpMateriales
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
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
    ''' Se cargan los materiales seleccionados para obtener el dataset que se va a enviar al momento de crear la gestion de compra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargarMaterialesSeleccionados() As DataSet
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_Condicion As String


        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlc_Condicion = String.Format("{0} IN ({1})", Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL, Me.HileraMateriales)

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ListarVOtDetalleMaterial(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlc_Condicion,
                String.Empty,
                False,
                0,
                0)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Se obtiene la gestion de compra almacenada
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarGestionCompra()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.GestionCompra = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_UBICACION, Me.IdUbicacion, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ANNO, Me.Anno, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_GESTION, Me.NumeroGestion))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    Private Sub ObtenerHileraMateriales()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_GESTION_COMPRA_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_UBICACION, Me.GestionCompra.IdUbicacion, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_VIA_COMPRA_CONTRATO, Me.GestionCompra.IdViaCompraContrato, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.ANNO, Me.GestionCompra.Anno, Utilerias.OrdenesDeTrabajo.Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_GESTION, Me.GestionCompra.NumeroGestion),
                String.Empty,
                False,
                0,
                0)

            For Each vlo_FIla In vlo_DsDatos.Tables(0).Rows
                Me.HileraMateriales = String.Format("{0}{1},", Me.HileraMateriales, vlo_FIla(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL))
            Next

            Me.HileraMateriales = Mid(Me.HileraMateriales, 1, Len(Me.HileraMateriales) - 1)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Se recarga el formulario despues de haber eliminado un material del acordeon ingresando por la VIA 1
    ''' </summary>
    ''' <param name="pvc_HileraMateriales"></param>
    ''' <remarks></remarks>
    Private Sub RecargarFormulario(ByVal pvc_HileraMateriales As String)

        Try
            Me.HileraMateriales = pvc_HileraMateriales
            CargarLista(pvc_HileraMateriales)

        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Funciones"
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
    ''' Se obtiene el valor de un parametro
    ''' </summary>
    ''' <param name="pvn_IdParametro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargarParametro(pvn_IdParametro As Integer) As Integer
        Dim Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtpParametroUbicacion As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Dim vlc_IdViaCompraContrato As Integer

        Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        Ws_OT_Catalogos.Timeout = -1
        Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntOtpParametroUbicacion = Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, pvn_IdParametro))

            vlc_IdViaCompraContrato = vlo_EntOtpParametroUbicacion.Valor

            Return vlc_IdViaCompraContrato

        Catch ex As Exception
            Throw
        Finally
            If Ws_OT_Catalogos IsNot Nothing Then
                Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Administra el proceso para crear una gestion de compra
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CrearGestionCompra() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_LlaveGestionCompra As String
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = CargarMaterialesSeleccionados()

            vlc_LlaveGestionCompra = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_InsertarGestionCompraSuministros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_DsDatos, Me.Usuario.UserName, Me.AutorizadoUbicacion.IdUbicacionAdministra, Me.Observaciones)

            Return vlc_LlaveGestionCompra <> String.Empty

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Administra el proceso para crear una gestion de compra con el estado INGRESO_GESTION_GECO
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CrearGestionCompraFinalizar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_LlaveGestionCompra As String
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = CargarMaterialesSeleccionados()

            vlc_LlaveGestionCompra = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_InsertarGestionCompraFinSuministros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_DsDatos, Me.Usuario.UserName, Me.AutorizadoUbicacion.IdUbicacionAdministra, Me.Observaciones)

            Return vlc_LlaveGestionCompra <> String.Empty

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    Private Function GuardarMateriales() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_LlaveGestionCompra As String
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = CargarMaterialesSeleccionados()

            vlc_LlaveGestionCompra = vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_ModificarGestionCompraSuministros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_DsDatos, Me.GestionCompra, Me.Observaciones, Me.Usuario.UserName)

            Return vlc_LlaveGestionCompra <> String.Empty

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String
        vlc_Condicion = String.Empty

        vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_UBICACION, Me.IdUbicacion)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_VIA_COMPRA_CONTRATO, Me.IdViaCompraContrato)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_LINEA_GEST_COMP_GROUP.ANNO, Me.Anno)
        vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_LINEA_GEST_COMP_GROUP.NUMERO_GESTION, Me.NumeroGestion)

        Return vlc_Condicion
    End Function

    ''' <summary>
    ''' Se modifica la gestion de compra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ModificarGestionCompra(vlo_EntOttGestionCompra As Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_GESTION_COMPRA_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttGestionCompra) > 0
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
