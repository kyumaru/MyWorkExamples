Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports System.IO

Partial Class OrdenesDeTrabajo_Almacen_Frm_OT_AtencionIncidentes
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para el dataset de archivos adjuntos que se mostrará al usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsAdjuntosPrevios As Data.DataSet
        Get
            Return CType(ViewState("DsAdjuntosPrevios"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsAdjuntosPrevios") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de tipos de documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
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
    ''' Propiedad para el dataset de archivos adjuntos que se mostrará al usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsAdjuntos As Data.DataSet
        Get
            Return CType(ViewState("DsAdjuntos"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsAdjuntos") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el bandera de estado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property BanderaEstado As Boolean
        Get
            Return CType(ViewState("BanderaEstado"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("BanderaEstado") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
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
    ''' Propiedad para la solicitud de materiales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property IncidenteAlmacen As EntOtfIncidenteAlmacen
        Get
            Return CType(ViewState("IncidenteAlmacen"), EntOtfIncidenteAlmacen)
        End Get
        Set(value As EntOtfIncidenteAlmacen)
            ViewState("IncidenteAlmacen") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la Categoria del material
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
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
    ''' Propiedad para la Categoria del material
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
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
    ''' ubicacion del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property AutorizadoUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Get
            Return CType(ViewState("AutorizadoUbicacion"), Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
            ViewState("AutorizadoUbicacion") = value
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
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
        Me.pnRpArchivosPrevios.Dibujar()
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardarYFinalizar_Click(sender As Object, e As EventArgs) Handles btnGuardarYFinalizar.Click
        If Page.IsValid Then
            Try
                Me.BanderaEstado = True

                If Modificar() Then
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                Else
                    MostrarAlertaError("No ha sido posible actualizar la información del registro")
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
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Page.IsValid Then
            Try
                Me.BanderaEstado = False
                If Modificar() Then
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                Else
                    MostrarAlertaError("No ha sido posible actualizar la información del registro")
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
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect("Lst_OT_AtencionIncidentes.aspx", False)
    End Sub


    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los números del paginador
    ''' </summary>
    ''' <param name="pvn_PaginaSeleccionada"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub pnRpArchivosPrevios_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpArchivosPrevios.CambioDePagina
        Try
            CargarListaArchivosPrevios(pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' descarga la imagen adjunta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivosPrevios_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsAdjuntosPrevios.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.OTF_ADJUNTO_INCIDENTE.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CType(Me.DsAdjuntosPrevios.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.OTF_ADJUNTO_INCIDENTE.ARCHIVO), Byte()))
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            AgregarArchivo()
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' descarga la imagen adjunta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivo_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsAdjuntos.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.OTF_ADJUNTO_INCIDENTE.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CType(Me.DsAdjuntos.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.OTF_ADJUNTO_INCIDENTE.ARCHIVO), Byte()))
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al carar el repeater de adjuntos, asigna atributos a cada elemento del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpAdjunto_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpAdjunto.ItemDataBound
        Dim vlo_IbBorrar As ImageButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar, ubicada en la 
    ''' primera columna de cada registro del listado de adjuntos, llama a la función borrar y muestra  un mensaje según 
    ''' la operacion efectuada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vln_Indice As Integer

        Try

            vln_Indice = Convert.ToInt32(CType(sender, ImageButton).CommandName)

            Me.DsAdjuntos.Tables(0).Rows(vln_Indice).Delete()
            Me.DsAdjuntos.Tables(0).AcceptChanges()

            If Me.DsAdjuntos IsNot Nothing AndAlso Me.DsAdjuntos.Tables(0).Rows.Count > 0 Then
                Me.rpAdjunto.DataSource = Me.DsAdjuntos
                Me.rpAdjunto.DataMember = Me.DsAdjuntos.Tables(0).TableName
                Me.rpAdjunto.DataBind()
                Me.rpAdjunto.Visible = True
            Else
                With Me.rpAdjunto
                    .DataSource = Nothing
                    .DataBind()
                    Me.rpAdjunto.Visible = False
                End With
            End If

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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlTipoArchivo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoArchivo.SelectedIndexChanged
        Dim vlo_TipoDocumento() As Data.DataRow
        Try
            If Me.ddlTipoArchivo.SelectedValue <> String.Empty Then
                vlo_TipoDocumento = Me.DsTipoArchivo.Tables(0).Select(String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, Me.ddlTipoArchivo.SelectedValue))
                If DsTipoArchivo.Tables.Count > 0 AndAlso vlo_TipoDocumento IsNot Nothing Then
                    imgExtensiones.Attributes.Add("title", String.Format("Extensiones permitidas: {0}", vlo_TipoDocumento(0).Item(Modelo.OTM_TIPO_DOCUMENTO.FORMATOS_ADMITIDOS).ToString.ToLower))
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar los diferentess componentes del formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        CargarComboTipoIncidente()
        CargarComboTipoArchivo()
        Me.lblAccion.Text = "Atención de Incidente"
        Try
            CargarIncidenteAlmacen(WebUtils.LeerParametro(Of Integer)("pvn_IdIncidenteAlmacen"))

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarAdjuntos()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsAdjuntos = vlo_Ws_OT_OrdenesDeTrabajo.OTF_ADJUNTO_INCIDENTE_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OTF_ADJUNTO_INCIDENTELST.ID_INCIDENTE_ALMACEN, Me.IncidenteAlmacen.IdIncidenteAlmacen, Modelo.V_OTF_ADJUNTO_INCIDENTELST.ORIGEN, OrigenAdjunto.REVISOR),
                String.Format("{0} {1}", Modelo.V_OTF_ADJUNTO_INCIDENTELST.NOMBRE_ARCHIVO, Ordenamiento.ASCENDENTE),
                False, 0, 0)

            If Me.DsAdjuntos.Tables.Count > 0 AndAlso Me.DsAdjuntos.Tables(0).Rows.Count > 0 Then
                With Me.rpAdjunto
                    .DataSource = Me.DsAdjuntos
                    .DataMember = Me.DsAdjuntos.Tables(0).TableName
                    .DataBind()
                    .Visible = True
                End With
            Else
                With Me.rpAdjunto
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
    ''' Carga la lista de tipos de documento con la condicion especificada.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboTipoArchivo()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlTipoArchivo.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} LIKE '%{1}%'", Modelo.OTM_TIPO_DOCUMENTO.ESTADO, Estado.ACTIVO), String.Empty, False, 0, 0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlTipoArchivo
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_TIPO_DOCUMENTO.DESCRIPCION
                    .DataValueField = Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO
                    .DataBind()
                End With
            End If

            Me.DsTipoArchivo = vlo_DsDatos
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
    ''' Método encargado de realizar la busqueda de registros segun los datos de 
    ''' condicion y orden de búsqueda
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BuscarArchivosPrevios()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_OrdenesDeTrabajo.EntDatosPaginacion

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntDatosPaginacion =
                vlo_Ws_OT_OrdenesDeTrabajo.OTF_ADJUNTO_INCIDENTE_ObtenerDatosPaginacionVOtfAdjuntoIncidentelst(
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                     String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OTF_ADJUNTO_INCIDENTELST.ID_INCIDENTE_ALMACEN, Me.IncidenteAlmacen.IdIncidenteAlmacen, Modelo.V_OTF_ADJUNTO_INCIDENTELST.ORIGEN, OrigenAdjunto.REGISTRADOR),
                     String.Format("{0} {1}", Modelo.V_OTF_ADJUNTO_INCIDENTELST.NOMBRE_ARCHIVO, Ordenamiento.ASCENDENTE),
                     CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpArchivosPrevios.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                CargarListaArchivosPrevios(1)
                Me.pnRpArchivosPrevios.Dibujar()
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de registros {0}", vlo_EntDatosPaginacion.TotalRegistros)
            Else
                Me.lblCantidadDeRegistros.Visible = False
                Me.lblCantidadDeRegistros.Text = String.Empty
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
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaArchivosPrevios(pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsAdjuntosPrevios = vlo_Ws_OT_OrdenesDeTrabajo.OTF_ADJUNTO_INCIDENTE_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OTF_ADJUNTO_INCIDENTELST.ID_INCIDENTE_ALMACEN, Me.IncidenteAlmacen.IdIncidenteAlmacen, Modelo.V_OTF_ADJUNTO_INCIDENTELST.ORIGEN, OrigenAdjunto.REGISTRADOR),
                String.Format("{0} {1}", Modelo.V_OTF_ADJUNTO_INCIDENTELST.NOMBRE_ARCHIVO, Ordenamiento.ASCENDENTE),
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If Me.DsAdjuntosPrevios IsNot Nothing AndAlso Me.DsAdjuntosPrevios.Tables(0).Rows.Count > 0 Then
                With Me.rpArchivosPrevios
                    .DataSource = Me.DsAdjuntosPrevios
                    .DataMember = Me.DsAdjuntosPrevios.Tables(0).TableName
                    .DataBind()
                End With
            Else
                With Me.rpArchivosPrevios
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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboTipoIncidente()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlTipoIncidente.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_TIPO_INCIDENTE_ListarRegistros(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Empty,
               String.Empty,
               False,
               0,
               0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlTipoIncidente
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_TIPO_INCIDENTE.DESCRIPCION
                    .DataValueField = Modelo.V_OTM_TIPO_INCIDENTE.ID_TIPO_INCIDENTE
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
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos del registro segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdIncidenteAlmacen">identificacion del registro</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarIncidenteAlmacen(pvn_IdIncidenteAlmacen As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOtmMaterial As Wsr_OT_Catalogos.EntOtmMaterial

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.IncidenteAlmacen = vlo_Ws_OT_OrdenesDeTrabajo.OTF_INCIDENTE_ALMACEN_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTF_INCIDENTE_ALMACEN.ID_INCIDENTE_ALMACEN, pvn_IdIncidenteAlmacen))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

        If Me.IncidenteAlmacen.Existe Then
            With Me.IncidenteAlmacen
                Me.ddlTipoIncidente.SelectedValue = .IdTipoIncidente
                Me.txtCodigo.Text = .IdMaterial

                vlo_EntOtmMaterial = CargarMaterial(Me.txtCodigo.Text, Me.AutorizadoUbicacion.IdUbicacionAdministra)
                CargarCategoria(String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_MATERIAL.ID_CATEGORIA_MATERIAL, vlo_EntOtmMaterial.IdCategoriaMaterial))
                CargarSubCategoria(String.Format("{0} = {1}", Modelo.OTM_SUBCATEGORIA_MATERIAL.ID_SUBCATEGORIA_MATERIAL, vlo_EntOtmMaterial.IdSubcategoriaMaterial))
                Me.txtCodigo.Text = vlo_EntOtmMaterial.IdMaterial.ToString

                Me.lblDescripcion.Text = vlo_EntOtmMaterial.Descripcion
                Me.lblCategoria.Text = Me.Categoria.Descripcion
                Me.lblSubCategoria.Text = Me.SubCategoria.Descripcion
                Me.txtDetalle.Text = .Detalle
                Me.txtObservacionesRevisor.Text = IIf(.ObservacionesRevisor = "-", String.Empty, .ObservacionesRevisor)
            End With

            Me.ddlTipoIncidente.Enabled = False
            Me.txtCodigo.Enabled = False
            Me.txtDetalle.Enabled = False

            upControlDatosMaterial.Visible = True

            BuscarArchivosPrevios()
            CargarAdjuntos()

        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    ''' <summary>
    ''' Carga las categorias deacuerdo a la condición indicada
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
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
    ''' <creationDate>05/08/2016</creationDate>
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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregarArchivo()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As Wsr_OT_Catalogos.EntOtmTipoDocumento
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_NombreArchivo As String
        Dim vlc_ExtensionArchivo As String
        Dim vlc_ExtensionesTipo As String()
        Dim vln_TamanoNombre As Integer
        Dim vln_TamArchivo As Integer
        Dim vln_limiteTamArchivo As Integer
        Dim vln_Resultado As Integer = 0
        Dim vlo_DrFila As Data.DataRow

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, Me.ddlTipoArchivo.SelectedValue))

            vlc_ExtensionesTipo = vlo_EntOtmTipoDocumento.FormatosAdmitidos.Split(",")
            vlc_NombreArchivo = Me.ifInfo.FileName
            vlc_ExtensionArchivo = Path.GetExtension(vlc_NombreArchivo).Replace(".", "")
            vln_TamanoNombre = Modelo.OTF_ADJUNTO_INCIDENTE.NOMBRE_ARCHIVO_BD_TAMANO
            vln_TamArchivo = Me.ifInfo.FileBytes.Length
            vln_limiteTamArchivo = vlo_EntOtmTipoDocumento.TamanioMaximo * 1048576

            If (vlc_ExtensionesTipo.Contains(vlc_ExtensionArchivo.ToUpper)) Then

                If vlc_NombreArchivo.Length < vln_TamanoNombre Then

                    If (vln_TamArchivo < vln_limiteTamArchivo) Then

                        vlo_DrFila = Me.DsAdjuntos.Tables(0).NewRow
                        vlo_DrFila.Item(Modelo.V_OTF_ADJUNTO_INCIDENTELST.ID_TIPO_DOCUMENTO) = Me.ddlTipoArchivo.SelectedValue
                        vlo_DrFila.Item(Modelo.V_OTF_ADJUNTO_INCIDENTELST.NOMBRE_ARCHIVO) = Me.ifInfo.FileName
                        vlo_DrFila.Item(Modelo.V_OTF_ADJUNTO_INCIDENTELST.ARCHIVO) = Me.ifInfo.FileBytes
                        vlo_DrFila.Item(Modelo.V_OTF_ADJUNTO_INCIDENTELST.DESCRIPCION) = Me.txtDescripcion.Text
                        vlo_DrFila.Item(Modelo.V_OTF_ADJUNTO_INCIDENTELST.ORIGEN) = OrigenAdjunto.REVISOR
                        vlo_DrFila.Item(Modelo.V_OTF_ADJUNTO_INCIDENTELST.USUARIO) = Me.Usuario.UserName
                        vlo_DrFila.Item(Modelo.V_OTF_ADJUNTO_INCIDENTELST.DESC_TIPO_DOCUMENTO) = Me.ddlTipoArchivo.SelectedItem

                        Me.DsAdjuntos.Tables(0).Rows.Add(vlo_DrFila)

                        If Me.DsAdjuntos IsNot Nothing AndAlso Me.DsAdjuntos.Tables(0).Rows.Count > 0 Then
                            Me.rpAdjunto.DataSource = Me.DsAdjuntos
                            Me.rpAdjunto.DataMember = Me.DsAdjuntos.Tables(0).TableName
                            Me.rpAdjunto.DataBind()
                            Me.rpAdjunto.Visible = True
                        Else
                            With Me.rpAdjunto
                                .DataSource = Nothing
                                .DataBind()
                                Me.rpAdjunto.Visible = False
                            End With
                        End If

                        Me.ddlTipoArchivo.SelectedValue = String.Empty
                        Me.txtDescripcion.Text = String.Empty

                    Else
                        MostrarAlertaError("El tamaño del archivo excede el máximo permitido.")
                    End If
                Else
                    MostrarAlertaError("El nombre del archivo es demasiado largo.")
                End If
            Else
                MostrarAlertaError("No es una extensión permitida para el tipo de documento seleccionado.")
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' carga la ubicacion favorita
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarAutorizadoUbicacion(pvn_NumEmpleado As Integer) As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_AUTORIZADO_UBICACION_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTM_AUTORIZADO_UBICACION.NUM_EMPLEADO, pvn_NumEmpleado))
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga el material
    ''' </summary>
    ''' <param name="pvn_IdMaterial"></param>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarMaterial(pvn_IdMaterial As Integer, pvn_IdUbicacion As Integer) As Wsr_OT_Catalogos.EntOtmMaterial
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_MATERIAL_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_MATERIAL.ID_MATERIAL, pvn_IdMaterial, Modelo.OTM_MATERIAL.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion))
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga el almacen
    ''' </summary>
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarAlmacen(pvn_IdUbicacion As Integer) As Wsr_OT_Catalogos.EntOtmAlmacenBodega
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion, Modelo.OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOtfIncidenteAlmacen As Wsr_OT_OrdenesDeTrabajo.EntOtfIncidenteAlmacen

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtfIncidenteAlmacen = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTF_INCIDENTE_ALMACEN_ModificarRegistroConAdjuntosRevisor(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtfIncidenteAlmacen, Me.DsAdjuntos) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Funcion encargada de contruir el registro
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_OrdenesDeTrabajo.EntOtfIncidenteAlmacen
        Dim vlo_EntOtfIncidenteAlmacen As Wsr_OT_OrdenesDeTrabajo.EntOtfIncidenteAlmacen
        vlo_EntOtfIncidenteAlmacen = Me.IncidenteAlmacen

        With vlo_EntOtfIncidenteAlmacen
            .Estado = IIf(Me.BanderaEstado = True, EstadoIncidente.ATENDIDO, EstadoIncidente.PENDIENTE)
            .ObservacionesRevisor = Me.txtObservacionesRevisor.Text
            .Usuario = Me.Usuario.UserName
        End With

        Return vlo_EntOtfIncidenteAlmacen
    End Function

#End Region

End Class
