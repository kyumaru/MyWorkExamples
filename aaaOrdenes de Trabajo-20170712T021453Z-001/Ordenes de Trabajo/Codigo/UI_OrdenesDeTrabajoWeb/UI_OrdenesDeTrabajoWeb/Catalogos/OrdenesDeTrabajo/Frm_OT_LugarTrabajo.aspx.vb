Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Imports System.Data

Partial Class Catalogos_Frm_OT_LugarTrabajo
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

    Private Property Lugar As EntOtmLugarTrabajo
        Get
            Return CType(ViewState("Lugar"), EntOtmLugarTrabajo)
        End Get
        Set(value As EntOtmLugarTrabajo)
            ViewState("Lugar") = value
        End Set
    End Property

    Private Property UnidadAdministra As Integer
        Get
            Return CType(ViewState("UnidadAdministra"), Integer)
        End Get
        Set(value As Integer)
            ViewState("UnidadAdministra") = value
        End Set
    End Property

    ''' <summary>
    ''' Maneja un set de unidades que administra este edificio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsUnidades As DataTable
        Get
            Return CType(ViewState("DsUnidades"), DataTable)
        End Get
        Set(value As DataTable)
            ViewState("DsUnidades") = value
        End Set
    End Property
#End Region

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Me.UnidadAdministra = CType(Session("pvo_UnidadAdministra"), Integer)
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try

        End If
    End Sub

    ''' <summary>
    ''' Evento click del boton aceptar, ingresa o modifica dependiendo de la opcion seleccionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then 'preguntar cuando hay validadores en el codigo
            Try
                Select Case Me.Operacion
                    Case Is = eOperacion.Agregar
                        If Agregar() Then
                            WebUtils.RegistrarScript(Me, "MostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("No a sido posible registrar la nueva ubicación.")
                        End If
                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No a sido posible actualizar la información de la ubicación.")
                        End If
                End Select
            Catch ex As Exception
                If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                    Dim vlo_OrdenesDeTrabajoException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                    MostrarAlertaError(vlo_OrdenesDeTrabajoException.Message)
                Else
                    Dim vlo_ControlDeErrores As New ControlDeErrores
                    vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
                End If
            End Try
        End If
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click sobre el boton de eliminar
    ''' del listado de funcionarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            BorrarUnidad(CType(sender, ImageButton).CommandName)
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
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            AgregaUnidadesDataTable()
            Me.upUnidades.Update()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' se ejecuta cuando se da click en el link de popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkGrid_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Try
            LimpiarFormulario()
            CargarDatosUnidad(e.CommandArgument.ToString)
            WebUtils.RegistrarScript(Me.Page, "Recargar", "inicializarScript();mostrarPopUp('#CerrarPopUpBusquedaUnidad');")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Método para cargar la lupa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnLimpiarFormulario_Click(sender As Object, e As EventArgs) Handles btnLimpiarFormulario.Click
        WebUtils.RegistrarScript(Me.Page, "CargarLupa", "javascript:cargarLupa();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ibBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibBuscar.Click
        Try
            BuscarUnidades(ObtenerCondicionDeBusqueda)
            WebUtils.RegistrarScript(Me.Page, "Recargar", "inicializarScript();")
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
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ibLimpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibLimpiar.Click
        LimpiarFormulario()
        WebUtils.RegistrarScript(Me.Page, "Recargar", "inicializarScript();")
    End Sub

    ''' <summary>
    ''' cambio de paginas del grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub grdUnidades_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdUnidades.PageIndexChanging
        Try
            Me.grdUnidades.PageIndex = e.NewPageIndex
            BuscarUnidades(ObtenerCondicionDeBusqueda)
            WebUtils.RegistrarScript(Me.Page, "Recargar", "inicializarScript();")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' muestra el popup de unidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkEjecutarBusquedaUnidad_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusquedaUnidad.Click
        Try
            WebUtils.RegistrarScript(Me.Page, "mostrarPopUpBusquedaUnidad", "javascript:mostrarPopUp('#PopUpBusquedaUnidad');inicializarScript();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' se ejecuta cuando se cambia el valor del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub txtUnidad_TextChanged(sender As Object, e As EventArgs) Handles txtUnidad.TextChanged
        Dim vlo_DsUnidad As Data.DataSet
        Dim pvc_CondicionBusquedas As String

        Try

            Me.lblNombreUnidad.Text = String.Empty

            If Me.txtUnidad.Text <> String.Empty Then
                pvc_CondicionBusquedas = String.Format("{0} = {1}", "COD_UNIDAD_SIRH", Me.txtUnidad.Text)
                vlo_DsUnidad = CargarDataSetUnidades(pvc_CondicionBusquedas)
                If vlo_DsUnidad IsNot Nothing AndAlso vlo_DsUnidad.Tables.Count > 0 AndAlso vlo_DsUnidad.Tables(0).Rows.Count > 0 Then
                    Me.lblNombreUnidad.Text = vlo_DsUnidad.Tables(0).Rows(0)(4)
                Else
                    Me.lblNombreUnidad.Text = String.Empty
                    Me.txtUnidad.Text = String.Empty
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoEncontrado();")

                End If

            End If
            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarScript();")
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' carga los datos de la unidad que obtiene por parametro
    ''' </summary>
    ''' <param name="pvn_CodUnidadSirh"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarDatosUnidad(pvn_CodUnidadSirh As Integer)
        Dim vlo_Unidad As WsrCatalogosVacaciones.PLM_ESTRUCTURA_ORG

        Try

            vlo_Unidad = CargarUnidad(pvn_CodUnidadSirh)

            Me.txtUnidad.Text = vlo_Unidad.COD_UNIDAD_SIRH
            Me.lblNombreUnidad.Text = vlo_Unidad.DESCRIPCION

            Me.upTxtUnidad.Update()
            Me.upLblNombreUnidad.Update()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' limpia los campos de búsqueda y datos del popup
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LimpiarFormulario()
        Me.txtCodigo.Text = String.Empty
        Me.txtDescripcion.Text = String.Empty
        Me.grdUnidades.DataSource = Nothing
        Me.grdUnidades.DataBind()
        Me.txtDescripcion.Focus()
    End Sub

    ''' <summary>
    ''' Carga combo de Estado y Pertenece a sede, ademas oculta Estado cuando esta en modo insertar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InicializarFormulario()
        CargarEstado()
        inicializarSetDatos()
        CargarUbicacion()
        CargarTipoLugarUbicacion()
        CargarSector()
        'CargarUnidades()
        Me.rdbEdificio.Checked = True
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Edificio o Sitio Universitario"
                'Me.ddlEstado.Attributes.Add("style", "display:none;") 'ocultar etiqueta
                'Me.trEstado.Attributes.Add("style", "display:none;")
                Me.ddlEstado.Enabled = False

            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Edificio o Sitio Universitario"
                Me.ddlEstado.Enabled = True
                ' Me.trEstado.Attributes.Add("style", "display:block;")
                'Me.ddlEstado.Attributes.Add("style", "display:block;") 'mostrar texto
                Try
                    CargarLugar(WebUtils.LeerParametro(Of String)("pvc_IdLugarTrabajo"))
                Catch ex As Exception
                    Throw
                End Try
        End Select
    End Sub

    ''' <summary>
    ''' borra un registro del dataset temporal de unidades
    ''' </summary>
    ''' <param name="pvc_CommandName"></param>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BorrarUnidad(pvc_CommandName As String)
        Try

            Me.DsUnidades.Rows.Find(New Object() {pvc_CommandName}).Delete()


            If Me.DsUnidades IsNot Nothing AndAlso Me.DsUnidades.Rows.Count > 0 Then
                Me.rpUnidades.DataSource = Me.DsUnidades
                Me.rpUnidades.DataMember = Me.DsUnidades.TableName
                Me.rpUnidades.DataBind()
                Me.rpUnidades.Visible = True
            Else
                With Me.rpUnidades
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpUnidades.Visible = False
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' agrega una unidad al dataset
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregaUnidadesDataTable()
        Dim vlo_DrNuevaFila As DataRow

        Try

            Dim vlc_CodUbica As Integer
            'vlc_CodUbica = CType(Me.ddlUnidad.SelectedValue, Integer)
            vlc_CodUbica = CType(Me.txtUnidad.Text, Integer)

            If Me.DsUnidades.Rows.Find(New Object() {vlc_CodUbica}) Is Nothing Then

                vlo_DrNuevaFila = Me.DsUnidades.NewRow

                vlo_DrNuevaFila.Item("CODIGO_UBICA") = vlc_CodUbica
                'vlo_DrNuevaFila.Item("COD_DESC") = Me.ddlUnidad.SelectedItem
                vlo_DrNuevaFila.Item("COD_DESC") = Me.lblNombreUnidad.Text
                Me.DsUnidades.Rows.Add(vlo_DrNuevaFila)

                If Me.DsUnidades IsNot Nothing AndAlso Me.DsUnidades.Rows.Count > 0 Then
                    Me.rpUnidades.DataSource = DsUnidades
                    Me.rpUnidades.DataMember = Me.DsUnidades.TableName
                    Me.rpUnidades.DataBind()
                    Me.rpUnidades.Visible = True
                Else
                    With Me.rpUnidades
                        .DataSource = Nothing
                        .DataBind()
                    End With
                    Me.rpUnidades.Visible = False
                End If
            Else
                MostrarAlertaError("La unidad ya está presente en la lista")
            End If

            Me.txtUnidad.Text = String.Empty
            Me.lblNombreUnidad.Text = String.Empty
            WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarScript();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub


    ''' <summary>
    ''' Obtiene el registro por medio del parametro pvc_IdUbicacion
    ''' </summary>
    ''' <param name="pvc_IdLugar"></param>
    ''' <remarks></remarks>
    Private Sub CargarLugar(pvc_IdLugar As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet
        Dim vlo_DsUnidades As DataSet
        Dim vlo_nuevaFila As DataRow
        Dim vlo_FilaEncontrada() As DataRow


        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        '0 columna, 1 valor busqueda
        Try
            Me.Lugar = vlo_Ws_OT_Catalogos.OTM_LUGAR_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0}) = '{1}'", Modelo.OTM_LUGAR_TRABAJO.ID_LUGAR_TRABAJO, pvc_IdLugar.Trim.ToUpper))   'verificar el nombre tambien?

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_UNIDAD_ENCARGADA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_UNIDAD_ENCARGADA.ID_LUGAR_TRABAJO, Me.Lugar.IdLugarTrabajo),
                String.Empty, False, 0, 0)

            FuncionesUtils.CargarUbicaciones(DateTime.Now, DateTime.Now, -1, 1, vlo_DsUnidades)


            If Me.Lugar.Existe Then
                With Me.Lugar

                    Me.txtNombre.Text = .Nombre
                    If .Clasificacion = Constantes.CLASIFICACION_EDI Then
                        Me.rdbEdificio.Checked = True
                        Me.rdbSitio.Checked = False
                    ElseIf .Clasificacion = Constantes.CLASIFICACION_SIT Then
                        Me.rdbEdificio.Checked = False
                        Me.rdbSitio.Checked = True
                    End If
                    Me.ddlUbicacion.SelectedValue = .IdUbicacionPertenece
                    Me.ddlLugar.SelectedValue = .IdTipoLugarUbicacion
                    Me.ddlSector.SelectedValue = .IdSectorTaller
                    Me.ddlEstado.SelectedValue = .Estado
                    Me.UnidadAdministra = .IdUbicacionAdministra

                End With
            Else
                WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
            End If

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                If vlo_DsUnidades.Tables.Count > 0 AndAlso vlo_DsUnidades.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila As Data.DataRow In vlo_DsDatos.Tables(0).Rows
                        vlo_FilaEncontrada = vlo_DsUnidades.Tables(0).Select(String.Format("CODIGO_UBICA = {0}", vlo_fila.Item(Modelo.V_OTM_UNIDAD_ENCARGADALST.COD_UNIDAD_SIRH)))
                        If vlo_FilaEncontrada.Length > 0 Then
                            vlo_nuevaFila = Me.DsUnidades.NewRow
                            vlo_nuevaFila.Item("CODIGO_UBICA") = vlo_fila.Item(Modelo.V_OTM_UNIDAD_ENCARGADALST.COD_UNIDAD_SIRH)
                            vlo_nuevaFila.Item("COD_DESC") = vlo_FilaEncontrada(0).Item("COD_DESC")
                            Me.DsUnidades.Rows.Add(vlo_nuevaFila)
                        End If
                    Next

                    With Me.rpUnidades
                        .DataSource = DsUnidades
                        .DataMember = DsUnidades.TableName
                        .DataBind()
                        .Visible = True
                    End With
                End If

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
            If vlo_DsUnidades IsNot Nothing Then
                vlo_DsUnidades.Dispose()
            End If
        End Try

    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub CargarEstado()

        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
        Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
        Me.ddlEstado.SelectedValue = Estado.ACTIVO
    End Sub

    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>14/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarSetDatos()
        Dim vlo_columna As DataColumn
        Dim vlo_llaves(1) As DataColumn


        'Se crea un nuevo datatabla 
        Me.DsUnidades = New DataTable

        vlo_columna = New DataColumn()

        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.Int32")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = "CODIGO_UBICA"
        'Se agrega la columna configurada al set de datos
        DsUnidades.Columns.Add(vlo_columna)
        'Se agrega al arreglo de llaves primarias la columna
        vlo_llaves(0) = vlo_columna
        'Y se configura el set de datos para que busque por formatos admitidos como llave primaria.
        Me.DsUnidades.PrimaryKey = vlo_llaves

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = "COD_DESC"
        'Se agrega la columna configurada al set de datos
        DsUnidades.Columns.Add(vlo_columna)

    End Sub

    Private Sub CargarUbicacion()
        Dim vlo_DsUbicacion As System.Data.DataSet
        Dim vlo_Wsr_OT_Catalogos As Ws_OT_Catalogos
        Dim pvc_Condicion As String

        Try
            vlo_Wsr_OT_Catalogos = New Ws_OT_Catalogos
            vlo_Wsr_OT_Catalogos.Timeout = -1
            vlo_Wsr_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
            'vlo_Wsr_TC_Catalogos.Url = ConfigurationManager.AppSettings(<ConstanteInterna.MI_SERVICIO_WEB>)

            pvc_Condicion = String.Format("{0} = '{1}'", Modelo.OTM_UBICACION.ESTADO, Estado.ACTIVO)

            vlo_DsUbicacion = vlo_Wsr_OT_Catalogos.OTM_UBICACION_ListarRegistros(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            pvc_Condicion,
            String.Format("{0} {1}", Modelo.OTM_UBICACION.DESCRIPCION, Ordenamiento.ASCENDENTE),
            False,
            0,
            0)
            'primer string.empty es la condicion
            Me.ddlUbicacion.Items.Clear()
            Me.ddlUbicacion.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            If vlo_DsUbicacion.Tables(0) IsNot Nothing AndAlso vlo_DsUbicacion.Tables(0).Rows.Count > 0 Then
                With Me.ddlUbicacion
                    .DataSource = vlo_DsUbicacion
                    .DataMember = vlo_DsUbicacion.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_UBICACION.DESCRIPCION
                    .DataValueField = Modelo.V_OTM_UBICACION.ID_UBICACION
                    .DataBind()
                End With
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_DsUbicacion IsNot Nothing Then
                vlo_DsUbicacion.Dispose()

            End If

            If vlo_Wsr_OT_Catalogos IsNot Nothing Then
                vlo_Wsr_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarTipoLugarUbicacion()
        Dim vlo_DsUbicacion As System.Data.DataSet
        Dim vlo_Wsr_OT_Catalogos As Ws_OT_Catalogos
        Dim pvc_Condicion As String

        Try
            vlo_Wsr_OT_Catalogos = New Ws_OT_Catalogos
            vlo_Wsr_OT_Catalogos.Timeout = -1
            vlo_Wsr_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
            'vlo_Wsr_TC_Catalogos.Url = ConfigurationManager.AppSettings(<ConstanteInterna.MI_SERVICIO_WEB>)

            pvc_Condicion = String.Format("{0} = '{1}'", Modelo.OTM_UBICACION.ESTADO, Estado.ACTIVO)

            vlo_DsUbicacion = vlo_Wsr_OT_Catalogos.OTM_TIPO_LUGAR_UBICACION_ListarRegistros(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            pvc_Condicion,
            String.Format("{0} {1}", Modelo.OTM_TIPO_LUGAR_UBICACION.DESCRIPCION, Ordenamiento.ASCENDENTE),
            False,
            0,
            0)
            'primer string.empty es la condicion
            Me.ddlLugar.Items.Clear()
            Me.ddlLugar.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            If vlo_DsUbicacion.Tables(0) IsNot Nothing AndAlso vlo_DsUbicacion.Tables(0).Rows.Count > 0 Then
                With Me.ddlLugar
                    .DataSource = vlo_DsUbicacion
                    .DataMember = vlo_DsUbicacion.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_TIPO_LUGAR_UBICACION.DESCRIPCION
                    .DataValueField = Modelo.V_OTM_TIPO_LUGAR_UBICACION.ID_TIPO_LUGAR_UBICACION
                    .DataBind()
                End With
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_DsUbicacion IsNot Nothing Then
                vlo_DsUbicacion.Dispose()

            End If

            If vlo_Wsr_OT_Catalogos IsNot Nothing Then
                vlo_Wsr_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    Private Sub CargarSector()
        Dim vlo_DsSector As System.Data.DataSet
        Dim vlo_Wsr_OT_Catalogos As Ws_OT_Catalogos
        Dim pvc_Condicion As String

        Try
            vlo_Wsr_OT_Catalogos = New Ws_OT_Catalogos
            vlo_Wsr_OT_Catalogos.Timeout = -1
            vlo_Wsr_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
            'vlo_Wsr_TC_Catalogos.Url = ConfigurationManager.AppSettings(<ConstanteInterna.MI_SERVICIO_WEB>)

            pvc_Condicion = String.Format("{0} = '{1}' AND {2} = {3} AND {4} = '{5}'", Modelo.OTM_SECTOR_TALLER.TIPO_AREA, Constantes.TIPO_AREA_SEC, Modelo.OTM_SECTOR_TALLER.ID_UBICACION_ADMINISTRA, Me.UnidadAdministra, Modelo.OTM_SECTOR_TALLER.ESTADO, Estado.ACTIVO)

            vlo_DsSector = vlo_Wsr_OT_Catalogos.OTM_SECTOR_TALLER_ListarRegistros(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            pvc_Condicion,
            String.Format("{0} {1}", Modelo.OTM_SECTOR_TALLER.NOMBRE, Ordenamiento.ASCENDENTE),
            False,
            0,
            0)
            'primer string.empty es la condicion
            Me.ddlSector.Items.Clear()
            Me.ddlSector.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            If vlo_DsSector.Tables(0) IsNot Nothing AndAlso vlo_DsSector.Tables(0).Rows.Count > 0 Then
                With Me.ddlSector
                    .DataSource = vlo_DsSector
                    .DataMember = vlo_DsSector.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_SECTOR_TALLER.NOMBRE
                    .DataValueField = Modelo.V_OTM_SECTOR_TALLER.ID_SECTOR_TALLER
                    .DataBind()
                End With
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_DsSector IsNot Nothing Then
                vlo_DsSector.Dispose()

            End If

            If vlo_Wsr_OT_Catalogos IsNot Nothing Then
                vlo_Wsr_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    'Private Sub CargarUnidades()
    '    Dim vlo_dsDatos As DataSet
    '    Try
    '        'Limpia el combo de beneficiarios
    '        Me.ddlUnidad.DataSource = Nothing
    '        Me.ddlUnidad.Items.Clear()
    '        Me.ddlUnidad.DataBind()

    '        FuncionesUtils.CargarUbicaciones(DateTime.Now, DateTime.Now, -1, 1, vlo_dsDatos)

    '        'Verifica que el data set contenga tablas
    '        If vlo_dsDatos.Tables.Count > 0 AndAlso vlo_dsDatos.Tables(0).Rows.Count > 0 Then
    '            With Me.ddlUnidad
    '                .Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
    '                .DataSource = vlo_dsDatos
    '                .DataMember = vlo_dsDatos.Tables(0).TableName
    '                .DataTextField = "COD_DESC"
    '                .DataValueField = "CODIGO_UBICA"
    '                .DataBind()
    '            End With
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    Finally
    '        If vlo_dsDatos IsNot Nothing Then
    '            vlo_dsDatos.Dispose()
    '        End If
    '    End Try
    'End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Busca unidades segun el criterio de búsqueda que obtiene por parametro
    ''' </summary>
    ''' <param name="pvc_CondicionBusquedas"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BuscarUnidades(pvc_CondicionBusquedas As String)
        Dim vlo_DsUnidades As Data.DataSet

        Try

            If String.IsNullOrWhiteSpace(pvc_CondicionBusquedas) Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "MensajeRetorno", "javascript:alert('Debe indicar algún criterio de búsqueda.');", True)
            Else
                vlo_DsUnidades = CargarDataSetUnidades(pvc_CondicionBusquedas)

                If vlo_DsUnidades IsNot Nothing AndAlso vlo_DsUnidades.Tables(0).Rows.Count > 0 Then
                    With Me.grdUnidades
                        .DataSource = vlo_DsUnidades
                        .DataMember = vlo_DsUnidades.Tables(0).TableName
                        .DataBind()
                    End With
                Else
                    grdUnidades.DataSource = Nothing
                    grdUnidades.DataBind()
                End If
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' retorna la condicion de busqueda de unidades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionDeBusqueda() As String
        Dim vlc_CondicionBusqueda As String

        vlc_CondicionBusqueda = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtCodigo.Text) Then
            vlc_CondicionBusqueda = String.Format("DESCRIPCION LIKE ('%{0}%')", Me.txtCodigo.Text.Trim)
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtDescripcion.Text) Then
            If String.IsNullOrWhiteSpace(vlc_CondicionBusqueda) Then
                vlc_CondicionBusqueda = String.Format("DESCRIPCION LIKE '%{0}%'", Me.txtDescripcion.Text.Trim)
            Else
                vlc_CondicionBusqueda = String.Format("{0} AND DESCRIPCION LIKE '%{1}%'", vlc_CondicionBusqueda, Me.txtDescripcion.Text.Trim)
            End If
        End If

        Return vlc_CondicionBusqueda
    End Function

    ''' <summary>
    ''' Retorna una entidad de tipo unidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarUnidad(pvn_CodUnidadSirh As Integer) As WsrCatalogosVacaciones.PLM_ESTRUCTURA_ORG
        Dim vlo_WsCatalogosVacaciones As WsrCatalogosVacaciones.WsCatalogosVacaciones

        vlo_WsCatalogosVacaciones = New WsrCatalogosVacaciones.WsCatalogosVacaciones
        vlo_WsCatalogosVacaciones.Timeout = -1
        vlo_WsCatalogosVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_WsCatalogosVacaciones.PLM_ESTRUCTURA_ORG_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("COD_UNIDAD_SIRH = {0} AND ESTADO = 'ACT' AND TIPO = 'UBC'", pvn_CodUnidadSirh))
        Catch ex As Exception
            If vlo_WsCatalogosVacaciones IsNot Nothing Then
                vlo_WsCatalogosVacaciones.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Construye registro para insertar a la base de datos con los datos del usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConstruirRegistro() As EntOtmLugarTrabajo
        Dim vlo_EntOtmLugarTrabajo As EntOtmLugarTrabajo


        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtmLugarTrabajo = New EntOtmLugarTrabajo
        ElseIf Me.Operacion = eOperacion.Modificar Then
            vlo_EntOtmLugarTrabajo = Me.Lugar
        End If

        With vlo_EntOtmLugarTrabajo


            .Nombre = Me.txtNombre.Text.Trim
            If Me.rdbEdificio.Checked = True Then
                .Clasificacion = Constantes.CLASIFICACION_EDI
            ElseIf Me.rdbSitio.Checked = True Then
                .Clasificacion = Constantes.CLASIFICACION_SIT
            End If
            .Estado = Me.ddlEstado.SelectedValue
            .IdUbicacionPertenece = Me.ddlUbicacion.SelectedValue
            .IdTipoLugarUbicacion = Me.ddlLugar.SelectedValue
            'If Me.ddlSector.SelectedValue = "" Then
            '.IdSectorTaller = 0
            'Else
            .IdSectorTaller = Me.ddlSector.SelectedValue
            .IdUbicacionAdministra = Me.UnidadAdministra
            'End If
            .Usuario = New UsuarioActual().UserName

        End With
        Return vlo_EntOtmLugarTrabajo
    End Function

    ''' <summary>
    ''' Ingresa nuevo registro en la DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmLugarTrabajo As EntOtmLugarTrabajo

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmLugarTrabajo = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_LUGAR_TRABAJO_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmLugarTrabajo, Me.DsUnidades) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Modifica registro en la DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmLugarTrabajo As EntOtmLugarTrabajo

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmLugarTrabajo = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_LUGAR_TRABAJO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmLugarTrabajo, Me.DsUnidades) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' retorna el dataset de unidades, segun criterio de busqueda
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarDataSetUnidades(pvc_Condicion As String) As Data.DataSet
        Dim vlo_WsCatalogosVacaciones As WsrCatalogosVacaciones.WsCatalogosVacaciones

        vlo_WsCatalogosVacaciones = New WsrCatalogosVacaciones.WsCatalogosVacaciones
        vlo_WsCatalogosVacaciones.Timeout = -1
        vlo_WsCatalogosVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If String.IsNullOrWhiteSpace(pvc_Condicion) Then
                pvc_Condicion = String.Format("TIPO = 'UBC' AND ESTADO = 'ACT'")
            Else
                pvc_Condicion = String.Format("{0} AND TIPO = 'UBC' AND ESTADO = 'ACT'", pvc_Condicion)
            End If

            Return vlo_WsCatalogosVacaciones.PLM_ESTRUCTURA_ORG_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                String.Empty)

        Catch ex As Exception
            If vlo_WsCatalogosVacaciones IsNot Nothing Then
                vlo_WsCatalogosVacaciones.Dispose()
            End If
        End Try
    End Function

#End Region


End Class
