Imports Utilerias.OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo.Modelo
Imports System.Data

Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_ConsultaAjusteInvExistencia
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

    Private Property DsMateriales As DataSet
        Get
            Return CType(ViewState("DsMateriales"), DataSet)
        End Get
        Set(value As DataSet)
            ViewState("DsMateriales") = value
        End Set
    End Property

    Private Property DsMaterialesBD As DataSet
        Get
            Return CType(ViewState("DsMaterialesBD"), DataSet)
        End Get
        Set(value As DataSet)
            ViewState("DsMaterialesBD") = value
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

    Public Property AjusteInventario As Wsr_OT_OrdenesDeTrabajo.EntOttAjusteInventario
        Get
            Return CType(ViewState("AjusteInventario"), Wsr_OT_OrdenesDeTrabajo.EntOttAjusteInventario)
        End Get
        Set(value As Wsr_OT_OrdenesDeTrabajo.EntOttAjusteInventario)
            ViewState("AjusteInventario") = value
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

    Public Property Anno As Integer
        Get
            Return CType(ViewState("Anno"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Anno") = value
        End Set
    End Property

    Public Property IdAlmacen As Integer
        Get
            Return CType(ViewState("IdAlmacen"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdAlmacen") = value
        End Set
    End Property

    Public Property ConsecutivoAjuste As Integer
        Get
            Return CType(ViewState("ConsecutivoAjuste"), Integer)
        End Get
        Set(value As Integer)
            ViewState("ConsecutivoAjuste") = value
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

    Public Property NoCargarHilera As Integer
        Get
            Return CType(ViewState("NoCargarHilera"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("NoCargarHilera") = value
        End Set
    End Property

    Private Property TipoAjuste As String
        Get
            Return CType(ViewState("TipoAjuste"), String)
        End Get
        Set(value As String)
            ViewState("TipoAjuste") = value
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
        If Not Page.IsPostBack Then
            Try

                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If AutorizadoUbicacion.Existe Then
                    LeerParametrosSession()

                    If Me.Modo = 1 Then 'Ingreso a la pantalla por la VIA 1 Ajuste inventario sin crear
                        Me.trMatGestionar.Visible = True
                        Me.btnCancelar.Text = "Regresar"
                        Me.AjusteInventario = New Wsr_OT_OrdenesDeTrabajo.EntOttAjusteInventario
                        Me.btnFinalizar.Visible = True

                        CargarNombreAlmacen()

                        'Se carga la lista de materiales
                        CargarLista(Me.HileraMateriales)
                    ElseIf Me.Modo = 2 Then 'Ingreso a la pantalla por la VIA 2 ajuste creado
                        Me.trMatGestionar.Visible = True


                        'Se obtiene el ajuste dE inventario
                        CargarAjusteInventario()

                        'Se obtiene el nombre del almacen para despliegue
                        CargarNombreAlmacen()

                        'Metodo para obtener los materiales ya almacenados en base de datos
                        ObtenerHileraMateriales()


                        If Me.AjusteInventario.EstadoAjuste = EstadoAjuste.CREADO Then
                            Me.btnCancelar.Text = "Regresar"
                        Else
                            Me.btnCancelar.Text = "Cancelar"
                        End If

                        Me.txtObservaciones.Text = Me.AjusteInventario.Observaciones


                        'Se carga la lista de materiales
                        CargarLista(Me.HileraMateriales)

                        CompletarDataSet()
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
        Dim vlo_IbBorrar As ImageButton

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_IbBorrar = e.Item.FindControl("ibBorrar")
            If vlo_IbBorrar IsNot Nothing Then
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Eliminar un material de la lista
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Dim vln_IdMaterial As String
            Dim vlo_Id() As Object

            vln_IdMaterial = CType(CType(sender, ImageButton).CommandArgument, String)

            vlo_Id = vln_IdMaterial.Split("%")

            Me.DsMateriales.Tables(0).Rows.Find(New Object() {vlo_Id(0), vlo_Id(1), vlo_Id(2)}).Delete()
            Me.DsMateriales.AcceptChanges()

            If Me.DsMateriales IsNot Nothing AndAlso Me.DsMateriales.Tables(0).Rows.Count > 0 Then

                With Me.rpMateriales
                    .DataSource = Me.DsMateriales
                    .DataMember = Me.DsMateriales.Tables(0).TableName
                    .DataBind()
                    .Visible = True
                End With
            Else
                With Me.rpMateriales
                    .DataSource = Nothing
                    .DataMember = Nothing
                    .DataBind()
                    .Visible = False
                End With
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
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


            If Me.Modo = 1 Then 'Ingreso a la pantalla desde la VIA 1 Ajuste inventario no creada
                Me.HileraMateriales = String.Empty

                For Each vlo_FIla In Me.DsMateriales.Tables(0).Rows
                    Me.HileraMateriales = String.Format("{0}{1},", Me.HileraMateriales, vlo_FIla(Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL))
                Next

                Me.HileraMateriales = Mid(Me.HileraMateriales, 1, Len(Me.HileraMateriales) - 1)

                Me.Session.Add("pvc_HileraSeleccionada", Me.HileraMateriales)
                Me.Session.Add("pvn_IdAlmacen", Me.IdAlmacen)
                Me.Session.Add("pvn_Modo", Me.Modo)
                Me.Session.Add("pvc_TipoAjuste", Me.TipoAjuste)

                Response.Redirect("Frm_OT_AjusteInvGlobal.aspx", False)
            ElseIf Me.Modo = 2 Then 'Ingreso a la pantalla desde la VIA 2  Ajuste inventario creada
                If Me.AjusteInventario.EstadoAjuste = EstadoAjuste.CREADO Then
                    Me.HileraMateriales = String.Empty
                    For Each vlo_FIla In Me.DsMateriales.Tables(0).Rows
                        Me.HileraMateriales = String.Format("{0}{1},", Me.HileraMateriales, vlo_FIla(Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL))
                    Next

                    Me.HileraMateriales = Mid(Me.HileraMateriales, 1, Len(Me.HileraMateriales) - 1)

                    Me.Session.Add("pvc_HileraSeleccionada", Me.HileraMateriales)
                    Me.Session.Add("pvn_Modo", Me.Modo)
                    Me.Session.Add("pvn_IdAlmacen", Me.IdAlmacen)
                    Me.Session.Add("pvn_IdUbicacion", Me.IdUbicacion)
                    Me.Session.Add("pvn_Anno", Me.Anno)
                    Me.Session.Add("pvn_ConsecutivoAjuste", Me.ConsecutivoAjuste)
                    Me.Session.Add("pvc_TipoAjuste", Me.TipoAjuste)

                    Response.Redirect("Frm_OT_AjusteInvGlobal.aspx", False)
                Else
                    Response.Redirect("Lst_OT_AjusteInventario.aspx", False)
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
            If Me.DsMateriales IsNot Nothing AndAlso Me.DsMateriales.Tables(0).Rows.Count > 0 Then
                If Not VerificarTipo() Then
                    If Not VerificarCantidad() Then
                        If Me.Modo = 1 Then 'Ingreso a la pantalla desde la VIA 1
                            If CrearAjusteInventario(False) Then
                                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
                            Else
                                MostrarAlertaError("No ha sido posible registrar el ajuste de inventario")
                            End If
                        ElseIf Me.Modo = 2 Then 'Ingreso a la pantalla desde la VIA 2
                            If ModificarAjusteInventario(False) Then
                                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
                            Else
                                MostrarAlertaError("No ha sido posible modificar el ajuste de inventario")
                            End If
                        End If
                    Else
                        MostrarAlertaError("Debe indicar la cantidad en todos los materiales para poder realizar el ajuste")
                    End If
                Else
                    MostrarAlertaError("Debe indicar el tipo de movimiento en todos los materiales para poder realizar el ajuste")
                End If

            Else
                MostrarAlertaError("Debe indicar al menos un material")
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
    Protected Sub btnFinalizar_Click(sender As Object, e As EventArgs) Handles btnFinalizar.Click
        Try
            If Me.DsMateriales IsNot Nothing AndAlso Me.DsMateriales.Tables(0).Rows.Count > 0 Then
                If Not VerificarTipo() Then
                    If Not VerificarCantidad() Then
                        If Me.Modo = 1 Then 'Ingreso a la pantalla desde la VIA 1
                            If CrearAjusteInventario(True) Then
                                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
                            Else
                                MostrarAlertaError("No ha sido posible registrar el ajuste de inventario")
                            End If
                        ElseIf Me.Modo = 2 Then 'Ingreso a la pantalla desde la VIA 2
                            If ModificarAjusteInventario(True) Then
                                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaRegistroExitoso", "mostrarAlertaRegistroExitoso();")
                            Else
                                MostrarAlertaError("No ha sido posible modificar el ajuste de inventario")
                            End If
                        End If
                    Else
                        MostrarAlertaError("Debe indicar la cantidad en todos los materiales para poder realizar el ajuste")
                    End If
                Else
                    MostrarAlertaError("Debe indicar el tipo de movimiento en todos los materiales para poder realizar el ajuste")
                End If

            Else
                MostrarAlertaError("Debe indicar al menos un material")
            End If


        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para agregar la cantidad digitada por el usuario al dataset que guarda en base de datos y ademas calcular el precio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub txtCantidad_OnTextChanged(sender As Object, e As EventArgs)
        Try
            Dim vlo_txtCantidad As TextBox = CType(sender, TextBox)
            Dim vlo_Row As DataRow
            Dim vlc_Cantidad As String

            vlc_Cantidad = vlo_txtCantidad.Text

            vlo_Row = Me.DsMateriales.Tables(0).Rows.Find(New Object() {vlo_txtCantidad.Attributes("data-inf").ToString(), Me.IdAlmacen, Me.AutorizadoUbicacion.IdUbicacionAdministra})

            If Not String.IsNullOrWhiteSpace(vlc_Cantidad) Then
                If IsNumeric(vlc_Cantidad) Then
                    vlo_Row("CANTIDAD") = vlc_Cantidad
                    If CType(vlc_Cantidad, Integer) > CType(vlo_Row(Modelo.V_OTF_INVENTARIOLST.CANTIDAD_DISPONIBLE), Double) Then
                        vlo_Row("TIPO") = "Incremento"
                    ElseIf CType(vlc_Cantidad, Integer) < CType(vlo_Row(Modelo.V_OTF_INVENTARIOLST.CANTIDAD_DISPONIBLE), Double) Then
                        vlo_Row("TIPO") = "Decremento"
                    Else
                        vlo_Row("TIPO") = String.Empty
                    End If

                Else
                    vlo_Row("CANTIDAD") = String.Empty
                    vlo_Row("TIPO") = String.Empty
                    MostrarAlertaError("Debe digitar un número válido")
                End If

            Else
                vlo_Row("CANTIDAD") = String.Empty
                vlo_Row("TIPO") = String.Empty
            End If



            If Me.DsMateriales IsNot Nothing AndAlso Me.DsMateriales.Tables(0).Rows.Count > 0 Then

                With Me.rpMateriales
                    .DataSource = Me.DsMateriales
                    .DataMember = Me.DsMateriales.Tables(0).TableName
                    .DataBind()
                End With
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

    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
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
            Me.Modo = WebUtils.LeerParametro(Of Integer)("pvn_Modo")
            Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
            Me.ConsecutivoAjuste = WebUtils.LeerParametro(Of Integer)("pvn_ConsecutivoAjuste")
            Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
            Me.NoCargarHilera = WebUtils.LeerParametro(Of Integer)("pvn_Formulario")
            Me.IdAlmacen = WebUtils.LeerParametro(Of Integer)("pvn_IdAlmacen")
            Me.TipoAjuste = WebUtils.LeerParametro(Of String)("pvc_TipoAjuste")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Cargar lista de materiales a partir de una hilera
    ''' </summary>
    ''' <param name="pvc_HileraMateriales">parámetro con la condicion de búsqueda</param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_HileraMateriales As String)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlc_Orden As String
        Dim vlc_Condicion As String

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlc_Orden = String.Format("{0} {1}", Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL, Ordenamiento.ASCENDENTE)

            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} IN ({5})", Modelo.V_OTF_INVENTARIOLST.ID_UBICACION_ADMINISTRA, Me.AutorizadoUbicacion.IdUbicacionAdministra, Modelo.V_OTF_INVENTARIOLST.ID_ALMACEN_BODEGA, Me.IdAlmacen, Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL, pvc_HileraMateriales)

            Me.DsMateriales = vlo_Ws_OT_Catalogos.OTF_INVENTARIO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlc_Condicion,
                vlc_Orden,
                False,
                0,
                0)

            Me.DsMateriales.Tables(0).Columns.Add("CANTIDAD", GetType(String))
            Me.DsMateriales.Tables(0).Columns.Add("TIPO", GetType(String))

            Me.DsMateriales.Tables(0).PrimaryKey = New Data.DataColumn() {Me.DsMateriales.Tables(0).Columns(Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL), Me.DsMateriales.Tables(0).Columns(Modelo.V_OTF_INVENTARIOLST.ID_ALMACEN_BODEGA), Me.DsMateriales.Tables(0).Columns(Modelo.V_OTF_INVENTARIOLST.ID_UBICACION_ADMINISTRA)}

            If Me.DsMateriales IsNot Nothing AndAlso Me.DsMateriales.Tables(0).Rows.Count > 0 Then

                With Me.rpMateriales
                    .DataSource = Me.DsMateriales
                    .DataMember = Me.DsMateriales.Tables(0).TableName
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

    ''' <summary>
    ''' Se obtiene el ajuste de inventario almacenado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CargarAjusteInventario()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.AjusteInventario = vlo_Ws_OT_OrdenesDeTrabajo.OTT_AJUSTE_INVENTARIO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Utilerias.OrdenesDeTrabajo.Modelo.OTT_AJUSTE_INVENTARIO.ID_UBICACION, Me.IdUbicacion, Utilerias.OrdenesDeTrabajo.Modelo.OTT_AJUSTE_INVENTARIO.ANNO, Me.Anno, Utilerias.OrdenesDeTrabajo.Modelo.OTT_AJUSTE_INVENTARIO.CONSECUTIVO_AJUSTE, Me.ConsecutivoAjuste))

            Me.IdAlmacen = Me.AjusteInventario.IdAlmacenBodega
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarNombreAlmacen()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlc_Condicion As String
        Dim vlo_EntOtmAlmacenBodega As Wsr_OT_Catalogos.EntOtmAlmacenBodega

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlc_Condicion = String.Format("{0} = {1}", Modelo.OTM_ALMACEN_BODEGA.ID_ALMACEN_BODEGA, Me.IdAlmacen)

            vlo_EntOtmAlmacenBodega = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlc_Condicion)

            Me.lblAlmacen.Text = vlo_EntOtmAlmacenBodega.Descripcion
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene los materiales almacenados en base de datos, ademas dependiendo del parametro NoCargarHilera refresca la hilera de materiales que se manejan en sesion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ObtenerHileraMateriales()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            DsMaterialesBD = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_AJUSTE_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Utilerias.OrdenesDeTrabajo.Modelo.OTT_DETALLE_AJUSTE.ID_UBICACION, Me.AjusteInventario.IdUbicacion, Utilerias.OrdenesDeTrabajo.Modelo.OTT_DETALLE_AJUSTE.ANNO, Me.AjusteInventario.Anno, Utilerias.OrdenesDeTrabajo.Modelo.OTT_DETALLE_AJUSTE.CONSECUTIVO_AJUSTE, Me.AjusteInventario.ConsecutivoAjuste),
                String.Empty,
                False,
                0,
                0)

            If Me.NoCargarHilera = 0 Then
                For Each vlo_FIla In DsMaterialesBD.Tables(0).Rows
                    Me.HileraMateriales = String.Format("{0}{1},", Me.HileraMateriales, vlo_FIla(Modelo.OTT_DETALLE_AJUSTE.ID_MATERIAL))
                Next

                Me.HileraMateriales = Mid(Me.HileraMateriales, 1, Len(Me.HileraMateriales) - 1)
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
    ''' Se completa el dataset que se muestra en pantallas con las cantidades y observaciones que ya se encuentran almacenadas en base de datos para correcto despliegue
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CompletarDataSet()

        Try

            For Each vlo_FilaDetalle In Me.DsMaterialesBD.Tables(0).Rows
                For Each vlo_FilaPantalla In Me.DsMateriales.Tables(0).Rows
                    If vlo_FilaPantalla(Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL) = vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.ID_MATERIAL) Then
                        If Not TypeOf vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE) Is DBNull Then
                            If CType(vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE), String) = Utilerias.OrdenesDeTrabajo.TipoMovimiento.INCREMENTO Then
                                vlo_FilaPantalla("TIPO") = "Incremento"
                                vlo_FilaPantalla("CANTIDAD") = CType(vlo_FilaPantalla(Modelo.V_OTF_INVENTARIOLST.CANTIDAD_DISPONIBLE), Double) + CType(vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.CANTIDAD), Integer)
                            ElseIf CType(vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE), String) = Utilerias.OrdenesDeTrabajo.TipoMovimiento.DECREMENTO Then
                                vlo_FilaPantalla("TIPO") = "Decremento"
                                vlo_FilaPantalla("CANTIDAD") = CType(vlo_FilaPantalla(Modelo.V_OTF_INVENTARIOLST.CANTIDAD_DISPONIBLE), Double) - CType(vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.CANTIDAD), Integer)
                            End If
                        End If
                    End If
                Next
            Next

            If Me.DsMateriales IsNot Nothing AndAlso Me.DsMateriales.Tables(0).Rows.Count > 0 Then

                With Me.rpMateriales
                    .DataSource = Me.DsMateriales
                    .DataMember = Me.DsMateriales.Tables(0).TableName
                    .DataBind()
                End With
            End If

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
    ''' Administra el proceso para crear una gestion de compra
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Mauricio Salas</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CrearAjusteInventario(pvb_Finalizar As Boolean) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_Resultado As String

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlc_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_AJUSTE_INVENTARIO_CrearAjusteExistencia(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.DsMateriales, Me.AutorizadoUbicacion.IdUbicacionAdministra, Me.IdAlmacen, Me.txtObservaciones.Text, Me.Usuario.UserName, pvb_Finalizar)

            Return vlc_Resultado <> String.Empty

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    Private Function ModificarAjusteInventario(pvb_Finalizar As Boolean) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_Resultado As String

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlc_Resultado = vlo_Ws_OT_OrdenesDeTrabajo.OTT_AJUSTE_INVENTARIO_ModificarAjusteExistencia(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.DsMateriales, Me.AjusteInventario, Me.txtObservaciones.Text, Me.Usuario.UserName, pvb_Finalizar)

            Return vlc_Resultado <> String.Empty

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Verifica que las cantidades vayan completas para evitar error en base de datos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function VerificarCantidad() As Boolean
        Dim vlb_Incompleto = False
        Try
            For Each vlo_fila In DsMateriales.Tables(0).Rows
                If TypeOf vlo_fila.Item("CANTIDAD") Is DBNull Then
                    vlb_Incompleto = True
                    Return vlb_Incompleto
                End If

            Next

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try

        Return vlb_Incompleto
    End Function

    ''' <summary>
    ''' Verifica que las cantidades vayan completas para evitar error en base de datos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function VerificarTipo() As Boolean
        Dim vlb_Incompleto = False
        Try
            For Each vlo_fila In DsMateriales.Tables(0).Rows
                If TypeOf vlo_fila.Item("TIPO") Is DBNull Then
                    vlb_Incompleto = True
                    Return vlb_Incompleto
                End If

            Next

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try

        Return vlb_Incompleto
    End Function

#End Region
End Class
