Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Frm_OT_OrdenTrabajo
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la PRE ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property PreOrdenTrabajo As EntOtfPreOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOtfPreOrdenTrabajo)
        End Get
        Set(value As EntOtfPreOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property OrdenTrabajo As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
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
    ''' Prpoiedad para cargar datos del empleado en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/09/2015</creationDate>
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
    ''' Propiedad para el dataset de archivos adjuntos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/09/2015</creationDate>
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
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>09/09/2015</creationDate>
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
    ''' id para el tipo de documento permitido
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property IdTipoDocumento As Integer
        Get
            Return CType(ViewState("IdTipoDocumento"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdTipoDocumento") = value
        End Set
    End Property

    ''' <summary>
    ''' Extensiones para archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ExtensionesArchivo As String
        Get
            Return CType(ViewState("ExtensionesArchivo"), String)
        End Get
        Set(value As String)
            ViewState("ExtensionesArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property EventoGenerado As Integer
        Get
            Return CType(ViewState("EventoGenerado"), Integer)
        End Get
        Set(value As Integer)
            ViewState("EventoGenerado") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property UbicacionFavorita As Wsr_OT_OrdenesDeTrabajo.EntOtfUbicacionFavorita
        Get
            Return CType(ViewState("UbicacionFavorita"), Wsr_OT_OrdenesDeTrabajo.EntOtfUbicacionFavorita)
        End Get
        Set(value As Wsr_OT_OrdenesDeTrabajo.EntOtfUbicacionFavorita)
            ViewState("UbicacionFavorita") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property ParametroSedeRodrigoFacio As Wsr_OT_Catalogos.EntOtpParametroGlobal
        Get
            Return CType(ViewState("ParametroSedeRodrigoFacio"), Wsr_OT_Catalogos.EntOtpParametroGlobal)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtpParametroGlobal)
            ViewState("ParametroSedeRodrigoFacio") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property CategoriaServicio As Wsr_OT_Catalogos.EntOtmCategoriaServicio
        Get
            Return CType(ViewState("CategoriaServicio"), Wsr_OT_Catalogos.EntOtmCategoriaServicio)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmCategoriaServicio)
            ViewState("CategoriaServicio") = value
        End Set
    End Property

    ''' <summary>
    ''' etapa para la orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property EtapaOrdenTrabajo As Wsr_OT_Catalogos.EntOtmEtapaOrdenTrabajo
        Get
            Return CType(ViewState("EtapaOrdenTrabajo"), Wsr_OT_Catalogos.EntOtmEtapaOrdenTrabajo)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmEtapaOrdenTrabajo)
            ViewState("EtapaOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Wizard As Boolean
        Get
            Return CType(ViewState("Wizard"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("Wizard") = value
        End Set
    End Property


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property CadenaLlave As String
        Get
            Return CType(ViewState("CadenaLlave"), String)
        End Get
        Set(value As String)
            ViewState("CadenaLlave") = value
        End Set
    End Property

    ''' <summary>
    ''' Indica a cual página se debe regresar.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>27/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property PaginaRegresar As String
        Get
            Return CType(ViewState("PaginaRegresar"), String)
        End Get
        Set(value As String)
            ViewState("PaginaRegresar") = value
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
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.Empleado = CargarFuncionario(Me.Usuario.UserName)
                Me.UbicacionFavorita = CargarUbicacionFavorita(Me.Usuario.NumEmpleado)
                If Me.UbicacionFavorita.Existe Then
                    InicializarFormulario()
                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted debe de indicar la sede en la cual presentará las ordenes de trabajo.','Frm_OT_SelecciónSedeTrabajo.aspx');")
                End If
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click en el boton aceptar para agregar un nuevo registro
    ''' llama a la funcion procesar y muestra un mensaje segun la operacion realizada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptarOculto_Click(sender As Object, e As EventArgs) Handles btnAceptarOculto.Click
        If Page.IsValid Then
            Try
                Me.EventoGenerado = 1
                Select Case (Me.Operacion)
                    Case Is = eOperacion.Agregar
                        If Agregar() Then
                            If Wizard Then

                                Dim llavesOrden As String()
                                llavesOrden = Me.CadenaLlave.Split("¬")

                                Me.Session.Add("pvn_IdUbicacion", CType(llavesOrden(0), Integer))
                                Me.Session.Add("pvc_IdOrdenTrabajo", llavesOrden(1))
                                Me.Session.Add("pvc_PantallaRetorno", "Lst_OT_OrdenTrabajo.aspx")

                                WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarAlertaWizard('Se ha registrado la información correctamente');")
                            Else
                                WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
                            End If
                        Else
                            MostrarAlertaError("No ha sido posible almacenar la información del nuevo registro")
                        End If

                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            If Wizard Then
                                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                            Else
                                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                            End If
                        Else
                            MostrarAlertaError("No ha sido posible actualizar la información del registro")
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
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptarEnviarOculto_Click(sender As Object, e As EventArgs) Handles btnAceptarEnviarOculto.Click
        If Page.IsValid Then
            Try
                Me.EventoGenerado = 2
                If Agregar() Then
                    WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
                Else
                    MostrarAlertaError("No ha sido posible almacenar la información del nuevo registro")
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
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar, ubicada en la 
    ''' primera columna de cada registro del listado de adjuntos, llama a la función borrar y muestra  un mensaje según 
    ''' la operacion efectuada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vln_Indice As Integer

        Try
            vln_Indice = Convert.ToInt32(CType(sender, ImageButton).CommandName)
            Select Case (Me.Operacion)
                Case Is = eOperacion.Agregar

                    Me.DsAdjuntosInsert.Tables(0).Rows(vln_Indice).Delete()
                Case Is = eOperacion.Modificar
                    Me.DsAdjuntosInsert.Tables(0).Rows(vln_Indice).Delete()
                    Me.DsAdjuntosInsert.Tables(0).AcceptChanges()
            End Select

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

            If Me.DsAdjuntosInsert.Tables(0).Rows.Count < 2 Then
                Me.trArchivo.Visible = True
            End If

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
    ''' <creationDate>29/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivo_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CType(Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO), Byte()))
            Response.End()
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se cambia el valor del combo de unidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlUnidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUnidad.SelectedIndexChanged
        Try
            If Me.ddlUnidad.SelectedValue <> String.Empty Then
                CargarLugarTrabajo(String.Format("{2} = '{3}' AND ({0} = {1} OR {0} = 0)", Modelo.V_OTM_UNIDAD_ENCARGADALST.COD_UNIDAD_SIRH, Me.ddlUnidad.SelectedValue, Modelo.OTM_LUGAR_TRABAJO.ESTADO, Estado.ACTIVO))
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se cambia el valor del combo de categorias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategoria.SelectedIndexChanged

        Try
            If Me.ddlCategoria.SelectedValue <> String.Empty Then
                ActualizarComboActividades()
                CategoriaServicio = CargarCategoriaServicio(CType(Me.ddlCategoria.SelectedValue, Integer))
                Me.txtDescripcionActividad.Text = String.Empty

                If Me.CategoriaServicio.RequiereFichaTecnica = 1 Then
                    Me.btnAceptarEnviar.Visible = False
                    Me.btnAceptar.OnClientClick = "javascript:return validarGuardarWizard();"
                    Wizard = True
                Else
                    Me.btnAceptar.OnClientClick = "javascript:return validarGuardar();"
                    Wizard = False
                    Me.btnAceptarEnviar.Visible = True
                End If

            End If

            WebUtils.RegistrarScript(Me, "deshabilitar", "BotonCancelar();")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se cambia el valor seleccionado del combo de actividades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlActividad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlActividad.SelectedIndexChanged
        Try
            If Me.ddlActividad.SelectedValue <> String.Empty Then
                CargarDescripcionAmpliadaActividad()
            Else
                Me.txtDescripcionActividad.Text = String.Empty
            End If
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarArchivo_Click(sender As Object, e As EventArgs) Handles btnAgregarArchivo.Click
        Try
            AgregarArchivo()
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/09/2015</creationDate>
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

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        Response.Redirect(Me.PaginaRegresar, False)
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar el formulario segun la operacion a realizar
    ''' en caso de ser agregar carga el texo con "Agregar", en caso de ser modificar 
    ''' cargar el texto con "Modificar" y llama al método que obtiene los datos de la orden seleccionada segun el id 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Dim vlo_builder = New StringBuilder()

        Me.PaginaRegresar = "Lst_OT_OrdenTrabajo.aspx"
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")

        CargarUnidades()

        If EsPeriodoCierre() Then

            Me.TamanoArchivo = CargarTamañoMaximoArchivo()
            Me.ExtensionesArchivo = CargarExtensionesArchivo()
            Me.EtapaOrdenTrabajo = CargarEtapaOrdenTrabajo()
            vlo_builder.AppendLine("Documento que a juicio del solicitante y en combinación con la descripción del trabajo de mayor claridad a la solicitud.")
            vlo_builder.AppendLine(String.Format("Extensiones permitidas:{0}", ExtensionesArchivo.ToLower))
            Me.imgExtensiones.Attributes.Add("title", vlo_builder.ToString)
            Me.trTallerSector.Visible = False
            Me.trResponsable.Visible = False

            Select Case Me.Operacion
                Case Is = eOperacion.Agregar
                    Me.lblAccion.Text = "Agregar Orden de Trabajo"
                    CargarDatosGenerales()
                Case Is = eOperacion.Modificar
                    Me.lblAccion.Text = "Modificar Orden  de Trabajo"
                    Try
                        Me.btnAceptarEnviar.Visible = False
                        CargarActividad(String.Empty)
                        CargarOrdenTrabajo(WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion"), WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo"))
                        CargaDsAdjuntosModificar()
                    Catch ex As Exception
                        Throw
                    End Try
                Case eOperacion.Consultar
                    Me.lblAccion.Text = "Consultar Orden  de Trabajo"
                    Try
                        Me.lblAccion.Text = "Consultar Orden de Trabajo"
                        Me.btnCancelar.Text = "Regresar"
                        Me.btnAceptarEnviar.Visible = False
                        CargarCategoriaServicio(String.Empty)
                        CargarActividad(String.Empty)
                        CargarOrdenTrabajo(WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion"), WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo"))
                        Me.trArchivo.Visible = False
                        Dim vlc_regresar = WebUtils.LeerParametro(Of String)("pvn_Regresar")
                        If Not String.IsNullOrWhiteSpace(vlc_regresar) Then
                            Me.PaginaRegresar = vlc_regresar

                            If Me.PaginaRegresar = "Lst_OT_OrdenTrabajoHijaProfesional.aspx" Then
                                Me.Session.Add("pvn_IdUbicacion", Me.OrdenTrabajo.IdUbicacionMadre)
                                Me.Session.Add("pvc_IdOrdenTrabajo", Me.OrdenTrabajo.IdOrdenTrabajoMadre)
                            End If

                        End If
                        WebUtils.RegistrarScript(Me, "deshabilitar", "deshabilitar();")

                    Catch ex As Exception
                        Throw
                    End Try

            End Select

        Else
            WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('No es posible hacer solicitudes ya que el sistema no posee periodos de cierre','Frm_OT_SelecciónSedeTrabajo.aspx');")
        End If

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargaDsAdjuntos()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsAdjuntosInsert = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = '{7}'",
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO, Me.IdTipoDocumento,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO, Me.EtapaOrdenTrabajo.IdEtapaOrdenTrabajo,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION, Me.OrdenTrabajo.IdUbicacion,
                              Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, Me.OrdenTrabajo.IdOrdenTrabajo),
                String.Empty,
                False,
                0,
                0)

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

            If Me.DsAdjuntosInsert.Tables(0).Rows.Count < 2 Then
                Me.trArchivo.Visible = True
            Else
                Me.trArchivo.Visible = False
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
    ''' genera el data set de archivos, con respecto a los adjunto a la preorden
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargaDsAdjuntosModificar()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DrFila As Data.DataRow
        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsAdjuntosInsert = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarRegistros(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("1 = 0"),
                String.Empty,
                False,
                0,
                0)

            If Me.PreOrdenTrabajo.Imagen1 IsNot Nothing Then

                vlo_DrFila = Me.DsAdjuntosInsert.Tables(0).NewRow
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO)) = Me.PreOrdenTrabajo.Imagen1
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)) = Me.PreOrdenTrabajo.NombreImagen1
                Me.DsAdjuntosInsert.Tables(0).Rows.Add(vlo_DrFila)

                If Me.PreOrdenTrabajo.Imagen2 IsNot Nothing Then
                    vlo_DrFila = Me.DsAdjuntosInsert.Tables(0).NewRow
                    vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO)) = Me.PreOrdenTrabajo.Imagen2
                    vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)) = Me.PreOrdenTrabajo.NombreImagen2
                    Me.DsAdjuntosInsert.Tables(0).Rows.Add(vlo_DrFila)
                End If
            End If

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

            If Me.DsAdjuntosInsert.Tables(0).Rows.Count < 2 Then
                Me.trArchivo.Visible = True
            Else
                Me.trArchivo.Visible = False
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
    ''' Carga las catagorias, segun la condicion de busqueda que obtiene por parametro
    ''' </summary>
    ''' <param name="pvc_Condicion">consecutivo de la orden</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarCategoriaServicio(pvc_Condicion As String)
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlCategoria.Items.Clear()
            Me.ddlCategoria.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ListarRegistrosLista(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                            pvc_Condicion,
                            String.Format("{0} {1}", Modelo.V_OTM_CATEGORIA_SERVICIOLST.DESCRIPCION, Utilerias.OrdenesDeTrabajo.Ordenamiento.ASCENDENTE),
                            False,
                            0,
                            0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlCategoria

                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_CATEGORIA_SERVICIO.DESCRIPCION
                    .DataValueField = Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO
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
    ''' Carga las actividades, segun la condicion de busqueda que obtiene por parametro
    ''' </summary>
    ''' <param name="pvc_Condicion">consecutivo de la orden</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarActividad(pvc_Condicion As String)
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlActividad.Items.Clear()
            Me.ddlActividad.Items.Add(New ListItem("[Seleccione el Valor]", String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_ACTIVIDAD_ListarRegistros(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                            pvc_Condicion,
                            String.Format("{0} {1}", Modelo.V_OTM_ACTIVIDAD.DESCRIPCION, Utilerias.OrdenesDeTrabajo.Ordenamiento.ASCENDENTE),
                            False,
                            0,
                            0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlActividad
                    '.Items.Add(New ListItem("[Selecccione el Valor]", String.Empty))
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_ACTIVIDAD.DESCRIPCION
                    .DataValueField = Modelo.OTM_ACTIVIDAD.ID_ACTIVIDAD
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
    ''' Carga los datos generales del formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarDatosGenerales()
        Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
        Try
            vlo_EntEmpleados = CargarFuncionario(Me.Usuario.UserName)

            Me.lblSolicitante.Text = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
            Me.txtPersonaContacto.Text = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)

            CargarEstructuraDsAdjunto()

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Carga las unidades a las cuales esta relacionado el usuario en session
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog>
    ''' <author>Cesar bermudez garcia</author>
    ''' <creationDate>02/02/2016</creationDate>
    ''' <change>se comenta una condición</change>
    ''' </changeLog>
    Private Sub CargarUnidades()
        Dim vlo_DsUnidades As Data.DataSet

        Try

            Me.ParametroSedeRodrigoFacio = CargarParametro(Parametros.SEDE_RODRIGO_FACIO)

            vlo_DsUnidades = CargarUbicacionVacaciones(CargarFuncionario(Me.Usuario.UserName).NUM_EMPLEADO)


            If vlo_DsUnidades IsNot Nothing AndAlso vlo_DsUnidades.Tables(0).Rows.Count > 0 Then
                With Me.ddlUnidad
                    .Items.Add(New ListItem("[Seleccione la unidad]", String.Empty))
                    .DataSource = vlo_DsUnidades
                    .DataMember = vlo_DsUnidades.Tables(0).TableName
                    .DataTextField = "DESCRIPCION"
                    .DataValueField = "CODIGO_UBICA"
                    .DataBind()
                End With

                If vlo_DsUnidades.Tables(0).Rows.Count = 1 Then
                    Me.ddlUnidad.SelectedIndex = 1
                    Me.lblNombreUnidad.Text = Me.ddlUnidad.SelectedItem.ToString
                    trUnidadLabel.Visible = True
                    Me.trUnidad.Visible = False
                    CargarLugarTrabajo(String.Format("{2} = '{3}' AND ({0} = {1} OR {0} = 0)", Modelo.V_OTM_UNIDAD_ENCARGADALST.COD_UNIDAD_SIRH, Me.ddlUnidad.SelectedValue, Modelo.OTM_LUGAR_TRABAJO.ESTADO, Estado.ACTIVO))
                Else
                    ' CargarLugarTrabajo(String.Format("{0} = '{1}'", Modelo.OTM_LUGAR_TRABAJO.ESTADO, Estado.ACTIVO))
                    Me.trUnidad.Visible = True
                    Me.trUnidadLabel.Visible = False
                End If
            Else

                If CType(Me.ParametroSedeRodrigoFacio.Valor, Integer) = Me.UbicacionFavorita.IdUbicacion Then
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no se encuentra autorizado para realizar el tramite.','../../Genericos/Frm_MenuPrincipal.aspx');")
                Else
                    vlo_DsUnidades = CargarUnidadesUbicaciones(Me.UbicacionFavorita.IdUbicacion)

                    If vlo_DsUnidades IsNot Nothing AndAlso vlo_DsUnidades.Tables(0).Rows.Count > 0 Then
                        With Me.ddlUnidad
                            .Items.Add(New ListItem("[Seleccione la unidad]", String.Empty))
                            .DataSource = vlo_DsUnidades
                            .DataMember = vlo_DsUnidades.Tables(0).TableName
                            .DataTextField = Modelo.V_OTM_UNIDAD_UBICACIONLST.DESC_COD_UNIDAD_SIRH
                            .DataValueField = Modelo.V_OTM_UNIDAD_UBICACIONLST.COD_UNIDAD_SIRH
                            .DataBind()
                        End With
                    Else
                        WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('La sede favorita no posee ningúna unidad asociada.','../../Genericos/Frm_MenuPrincipal.aspx');")
                    End If
                End If
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLugarTrabajo(pvc_Condicion As String)
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlLugarTrabajo.Items.Clear()
            Me.ddlLugarTrabajo.Items.Add(New ListItem("[Seleccione el Valor]", String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_UNIDAD_ENCARGADA_ListarRegistrosLista(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                pvc_Condicion,
                                String.Format("{0} {1}", Modelo.V_OTM_UNIDAD_ENCARGADALST.DESCRIPCION, Utilerias.OrdenesDeTrabajo.Ordenamiento.ASCENDENTE),
                                False,
                                0,
                                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlLugarTrabajo
                    '.Items.Add(New ListItem("[Selecccione el Valor]", String.Empty))
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_UNIDAD_ENCARGADALST.DESCRIPCION
                    .DataValueField = Modelo.V_OTM_UNIDAD_ENCARGADALST.ID_LUGAR_TRABAJO
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
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">id de la ubicacion</param>
    ''' <param name="pvc_IdOrdenTrabajo">id de la orden</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>

    Private Sub CargarOrdenTrabajo(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.Operacion = eOperacion.Consultar Then

                If WebUtils.LeerParametro(Of Integer)("pvn_EsPreOrden") = 1 Then
                    Me.trTallerSector.Visible = False
                    Me.trResponsable.Visible = False

                    Me.PreOrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_ObtenerRegistro(
                          ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                          ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                          String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_PRE_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTF_PRE_ORDEN_TRABAJO.ID_PRE_ORDEN_TRABAJO, WebUtils.LeerParametro(Of Integer)("pvn_IdPreOrdenTrabajo")))

                    If Me.PreOrdenTrabajo.Existe Then
                        With Me.PreOrdenTrabajo
                            Me.lblSolicitante.Text = String.Format("{0} {1} {2}", Me.Empleado.NOMBRE, Me.Empleado.APELLIDO1, Me.Empleado.APELLIDO2)
                            Me.txtPersonaContacto.Text = .NombrePersonaContacto
                            Me.txtTelefono.Text = .Telefono
                            Me.ddlUnidad.SelectedValue = .CodUnidadSirh
                            Me.ddlLugarTrabajo.SelectedValue = .IdLugarTrabajo
                            Me.txtLugarExacto.Text = .SennasExactas
                            Me.ddlCategoria.SelectedValue = .IdCategoriaServicio
                            Me.ddlActividad.SelectedValue = .IdActividad
                            CargarDescripcionAmpliadaActividad()
                            Me.txtDescTrabajo.Text = .DescripcionTrabajo
                        End With
                        CargaDsAdjuntosModificar()
                    Else
                        WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
                    End If

                Else
                    Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistroConsulta(
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                   String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo))
                    If String.IsNullOrWhiteSpace(Me.OrdenTrabajo.NombreTaller) Or String.IsNullOrWhiteSpace(Me.OrdenTrabajo.CoordEncargado) Then
                        Me.trTallerSector.Visible = False
                        Me.trResponsable.Visible = False
                    Else
                        Me.trTallerSector.Visible = True
                        Me.trResponsable.Visible = True
                        Me.lblTallerSector.Text = Me.OrdenTrabajo.NombreTaller
                        Me.lblResponsable.Text = Me.OrdenTrabajo.CoordEncargado
                    End If

                    If Me.OrdenTrabajo.Existe Then
                        With Me.OrdenTrabajo
                            Me.lblSolicitante.Text = String.Format("{0} {1} {2}", Me.Empleado.NOMBRE, Me.Empleado.APELLIDO1, Me.Empleado.APELLIDO2)
                            Me.txtPersonaContacto.Text = .NombrePersonaContacto
                            Me.txtTelefono.Text = .Telefono
                            Me.ddlUnidad.SelectedValue = .CodUnidadSirh
                            Me.ddlLugarTrabajo.SelectedValue = .IdLugarTrabajo
                            Me.txtLugarExacto.Text = .SennasExactas
                            Me.ddlCategoria.SelectedValue = .IdCategoriaServicio
                            Me.ddlActividad.SelectedValue = .IdActividad
                            CargarDescripcionAmpliadaActividad()
                            Me.txtDescTrabajo.Text = .DescripcionTrabajo
                        End With
                        CargaDsAdjuntos()
                    Else
                        WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
                    End If
                End If
            Else
                Me.PreOrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_PRE_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTF_PRE_ORDEN_TRABAJO.ID_PRE_ORDEN_TRABAJO, pvc_IdOrdenTrabajo))

                If Me.PreOrdenTrabajo.Existe Then
                    With Me.PreOrdenTrabajo
                        Me.lblSolicitante.Text = String.Format("{0} {1} {2}", Me.Empleado.NOMBRE, Me.Empleado.APELLIDO1, Me.Empleado.APELLIDO2)
                        Me.txtPersonaContacto.Text = .NombrePersonaContacto
                        Me.txtTelefono.Text = .Telefono
                        Me.ddlUnidad.SelectedValue = .CodUnidadSirh
                        Me.ddlLugarTrabajo.SelectedValue = .IdLugarTrabajo
                        Me.txtLugarExacto.Text = .SennasExactas
                        Me.ddlCategoria.SelectedValue = .IdCategoriaServicio
                        ActualizarComboActividades()
                        Me.ddlActividad.SelectedValue = .IdActividad
                        CargarDescripcionAmpliadaActividad()
                        Me.txtDescTrabajo.Text = .DescripcionTrabajo
                    End With
                Else
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
                End If

                CategoriaServicio = CargarCategoriaServicio(CType(Me.ddlCategoria.SelectedValue, Integer))
                If Me.CategoriaServicio.RequiereFichaTecnica = 1 Then
                    Me.btnAceptar.OnClientClick = "javascript:return validarGuardarWizard();"
                    Wizard = True
                Else
                    Me.btnAceptar.OnClientClick = "javascript:return validarGuardarModificar();"
                    Wizard = False
                End If

                Me.btnAceptarEnviar.Visible = False

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
    ''' carga la descripcion ampliada de la actividad
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarDescripcionAmpliadaActividad()
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmActividad As Wsr_OT_Catalogos.EntOtmActividad

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtmActividad = vlo_Ws_OT_Ws_OT_Catalogos.OTM_ACTIVIDAD_ObtenerRegistro(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_ACTIVIDAD, Me.ddlActividad.SelectedValue))

            Me.txtDescripcionActividad.Text = vlo_EntOtmActividad.DescripcionAmpliada

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga la estructura de la tabla de adjuntos OTT_ADJUNTO_ORDEN_TRABAJO
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEstructuraDsAdjunto()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsAdjuntosInsert = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarRegistros(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                String.Format("1 = 0"),
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
    End Sub

    ''' <summary>
    ''' Agrega un nuevo adjunto al dataset 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregarArchivo()
        Dim vlo_DrFila As Data.DataRow

        Try
            If Me.DsAdjuntosInsert.Tables(0).Rows.Count < 2 Then
                vlo_DrFila = Me.DsAdjuntosInsert.Tables(0).NewRow
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)) = Me.ifInfo.FileName
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO)) = Me.ifInfo.FileBytes
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.USUARIO)) = Me.Usuario.UserName
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
                Me.trArchivo.Visible = True
            End If

            If Me.DsAdjuntosInsert.Tables(0).Rows.Count = 2 Then
                Me.trArchivo.Visible = False
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Carga las categorias dependiendo del periodo de cierre 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>19/01/2016</creationDate>
    ''' <changeLog>
    '''    <author>César Bermudez Garcia</author>
    '''    <creationDate>21/01/2016</creationDate>
    '''    <ChangeDetail>Se agrega la validación para considerar excepciones</ChangeDetail>
    '''</changeLog>
    Private Function EsPeriodoCierre() As Boolean

        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_Condicion As String
        Dim vlc_CondicionValida As String
        Dim vlc_result As String
        Dim vlo_result() As String

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        'Condicion base para las categorias en caso de que no se encuentre en periodo de cierre
        '{0}: OTM_CATEGORIA_SERVICIO.ESTADO
        '{1}: Activo
        '{2}: OTM_CATEGORIA_SERVICIO.OCULTAR_CATEGORIA
        '{3}: Visible

        vlc_Condicion = String.Format("{0} LIKE '%{1}%' AND {2} = {3}",
                                              Modelo.OTM_CATEGORIA_SERVICIO.ESTADO,
                                              Estado.ACTIVO,
                                              Modelo.OTM_CATEGORIA_SERVICIO.OCULTAR_CATEGORIA,
                                              Constantes.VISIBLE)

        If Operacion = eOperacion.Consultar Then
            Return True
        End If
        Try

            vlc_result = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_CategoriasPeriodoCierre(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlc_Condicion, Me.Usuario.NumEmpleado)

            If vlc_result IsNot Nothing Then

                vlo_result = vlc_result.Split(",")

                vlc_CondicionValida = vlo_result(0)

                If String.IsNullOrWhiteSpace(vlo_result(0)) Then
                    WebUtils.RegistrarScript(Me, "MensajePopup", String.Format("MensajePopup('El plazo para efectuar Ordenes de trabajo ha finalizado.','Lst_OT_OrdenTrabajo.aspx');"))
                    Return False
                Else
                    CargarCategoriaServicio(vlc_CondicionValida)
                    If Not String.IsNullOrWhiteSpace(vlo_result(1)) Then
                        WebUtils.RegistrarScript(Me, "MensajePopup", String.Format("MensajePopup('{0}','');", vlo_result(1)))
                    End If

                End If
            Else
                CargarCategoriaServicio(vlc_Condicion)
            End If


            Return True
        Catch ex As Exception

        End Try

    End Function

    ''' <summary>
    ''' Actualiza el combo de actividades respecto al indice seleccionado en el combo de categorías
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Jairo Alfaro Magnan</author>
    ''' <creationDate>26/05/2017</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ActualizarComboActividades()
        Dim vlc_Condicion As String
        vlc_Condicion = String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_CATEGORIA_SERVICIO, Me.ddlCategoria.SelectedValue)
        CargarActividad(vlc_Condicion)
    End Sub
#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion encargada de contruir el registro  de la orden de trabajo
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo
        Dim vlo_EntOttOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo

        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOttOrdenTrabajo = New Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo
            vlo_EntOttOrdenTrabajo.IdUbicacion = Me.UbicacionFavorita.IdUbicacion
            vlo_EntOttOrdenTrabajo.Anno = DateTime.Now.Year
            vlo_EntOttOrdenTrabajo.FechaHoraSolicita = DateTime.Now

            'If Wizard Then
            '    vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PENDIENTE_DE_ENVIO
            'Else
            'If ObtenerDirectorUnidad(CType(Me.ddlUnidad.SelectedValue, Integer)) = Me.Empleado.NUM_EMPLEADO Then

            '    vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = IIf(Me.EventoGenerado = 1, EstadoOrden.PENDIENTE_DE_ENVIO, EstadoOrden.ASIGNADA)

            'Else
            '    If Not Roles.IsUserInRole(Membership.GetUser.UserName, ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ROL_OT_AUTORIZADOR_SOLICITUD)) Then
            '        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = IIf(Me.EventoGenerado = 1, EstadoOrden.PENDIENTE_DE_ENVIO, EstadoOrden.PENDIENTE_REVISION_DIRECTOR)
            '    Else
            '        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = IIf(Me.EventoGenerado = 1, EstadoOrden.PENDIENTE_DE_ENVIO, EstadoOrden.ASIGNADA)
            '    End If
            'End If
            'End If

        Else
            vlo_EntOttOrdenTrabajo = Me.OrdenTrabajo
        End If

        With vlo_EntOttOrdenTrabajo
            .Parentesco = "MAD"
            .TipoOrdenTrabajo = TipoOrden.ORDINARIA
            .NumEmpleado = Me.Empleado.NUM_EMPLEADO
            .IdCategoriaServicio = Me.ddlCategoria.SelectedValue
            .IdActividad = Me.ddlActividad.SelectedValue
            .IdLugarTrabajo = Me.ddlLugarTrabajo.SelectedValue
            .CodUnidadSirh = Me.ddlUnidad.SelectedValue
            .NombrePersonaContacto = Me.txtPersonaContacto.Text
            .Telefono = Me.txtTelefono.Text
            .SennasExactas = Me.txtLugarExacto.Text
            .DescripcionTrabajo = Me.txtDescTrabajo.Text
            .Usuario = Me.Usuario.UserName
        End With

        Return vlo_EntOttOrdenTrabajo
    End Function

    ''' <summary>
    ''' Funcion encargada de el registro de la pre orden trabajo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistroPreOrden() As Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo
        Dim vlo_EntOtfPreOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo


        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtfPreOrdenTrabajo = New Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo
            vlo_EntOtfPreOrdenTrabajo.IdUbicacion = Me.UbicacionFavorita.IdUbicacion
            vlo_EntOtfPreOrdenTrabajo.Anno = DateTime.Now.Year
            vlo_EntOtfPreOrdenTrabajo.FechaHoraSolicita = DateTime.Now
        Else
            vlo_EntOtfPreOrdenTrabajo = Me.PreOrdenTrabajo
        End If

        With vlo_EntOtfPreOrdenTrabajo
            .NumEmpleado = Me.Empleado.NUM_EMPLEADO
            .IdCategoriaServicio = Me.ddlCategoria.SelectedValue
            .IdActividad = Me.ddlActividad.SelectedValue
            .IdLugarTrabajo = Me.ddlLugarTrabajo.SelectedValue
            .CodUnidadSirh = Me.ddlUnidad.SelectedValue
            .NombrePersonaContacto = Me.txtPersonaContacto.Text
            .Telefono = Me.txtTelefono.Text
            .SennasExactas = Me.txtLugarExacto.Text
            .DescripcionTrabajo = Me.txtDescTrabajo.Text
            .Usuario = Me.Usuario.UserName
            If Me.DsAdjuntosInsert IsNot Nothing AndAlso Me.DsAdjuntosInsert.Tables(0).Rows.Count > 0 Then
                .NombreImagen1 = Me.DsAdjuntosInsert.Tables(0).Rows.Item(0)(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO).ToString
                .Imagen1 = CType(Me.DsAdjuntosInsert.Tables(0).Rows.Item(0)(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO), Byte())
                If Me.DsAdjuntosInsert.Tables(0).Rows.Count > 1 Then
                    .NombreImagen2 = Me.DsAdjuntosInsert.Tables(0).Rows.Item(1)(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO).ToString
                    .Imagen2 = CType(Me.DsAdjuntosInsert.Tables(0).Rows.Item(1)(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO), Byte())
                End If
            End If
        End With
        Return vlo_EntOtfPreOrdenTrabajo
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarLugarTrabajo(pvn_IdLugarTrabajo As Integer) As Wsr_OT_Catalogos.EntOtmLugarTrabajo
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_Catalogos.OTM_LUGAR_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_LUGAR_TRABAJO.ID_LUGAR_TRABAJO, pvn_IdLugarTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Administra el proceso para agregar una orden de trabajo
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo
        Dim vlo_EntOtfPreOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If Wizard Then
                vlo_EntOtfPreOrdenTrabajo = ConstruirRegistroPreOrden()

                CadenaLlave = vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_InsertarPreOrdenCadena(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        vlo_EntOtfPreOrdenTrabajo,
                        EstadoOrden.PENDIENTE_DE_ENVIO)

            Else

                If ObtenerDirectorUnidad(CType(Me.ddlUnidad.SelectedValue, Integer)) = Me.Empleado.NUM_EMPLEADO Then

                    If Me.EventoGenerado = 1 Then
                        vlo_EntOtfPreOrdenTrabajo = ConstruirRegistroPreOrden()

                        CadenaLlave = vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_InsertarPreOrdenCadena(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                vlo_EntOtfPreOrdenTrabajo,
                                EstadoOrden.PENDIENTE_DE_ENVIO)

                    Else
                        vlo_EntOttOrdenTrabajo = ConstruirRegistro()
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.ASIGNADA

                        CadenaLlave = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_InsertarOrdenTrabajoConAdjuntosCadena(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                vlo_EntOttOrdenTrabajo, Me.DsAdjuntosInsert, Me.IdTipoDocumento, Me.EtapaOrdenTrabajo.IdEtapaOrdenTrabajo, DescripcionAdjuntos.ORDENES_USUARIO)

                    End If

                Else
                    If Not Roles.IsUserInRole(Membership.GetUser.UserName, ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ROL_OT_AUTORIZADOR_SOLICITUD)) Then

                        If Me.EventoGenerado = 1 Then
                            vlo_EntOtfPreOrdenTrabajo = ConstruirRegistroPreOrden()

                            CadenaLlave = vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_InsertarPreOrdenCadena(
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                    vlo_EntOtfPreOrdenTrabajo,
                                    EstadoOrden.PENDIENTE_DE_ENVIO)
                        Else
                            vlo_EntOtfPreOrdenTrabajo = ConstruirRegistroPreOrden()

                            CadenaLlave = vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_InsertarPreOrdenCadena(
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                    vlo_EntOtfPreOrdenTrabajo,
                                    EstadoOrden.PENDIENTE_REVISION_DIRECTOR)
                        End If

                    Else

                        If Me.EventoGenerado = 1 Then
                            vlo_EntOtfPreOrdenTrabajo = ConstruirRegistroPreOrden()

                            CadenaLlave = vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_InsertarPreOrdenCadena(
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                    vlo_EntOtfPreOrdenTrabajo,
                                    EstadoOrden.PENDIENTE_DE_ENVIO)
                        Else
                            vlo_EntOttOrdenTrabajo = ConstruirRegistro()
                            vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.ASIGNADA

                            CadenaLlave = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_InsertarOrdenTrabajoConAdjuntosCadena(
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                    vlo_EntOttOrdenTrabajo, Me.DsAdjuntosInsert, Me.IdTipoDocumento, Me.EtapaOrdenTrabajo.IdEtapaOrdenTrabajo, DescripcionAdjuntos.ORDENES_USUARIO)
                        End If
                    End If
                End If
            End If

            Return CadenaLlave <> String.Empty
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Administra el proceso para modificar una orden de trabajo
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOtfPreOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtfPreOrdenTrabajo = ConstruirRegistroPreOrden()

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtfPreOrdenTrabajo) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>17/07/2015</creationDate>
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

    ''' <summary>
    ''' carga las ubicaciones sugun el usuario conectado
    ''' </summary>
    ''' <param name="pvn_NumeroEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarUbicacionVacaciones(pvn_NumeroEmpleado As Integer) As Data.DataSet
        Dim vlo_WsSolicitudVacaciones As WsrSolicitudVacaciones.WsSolicitudVacaciones

        vlo_WsSolicitudVacaciones = New WsrSolicitudVacaciones.WsSolicitudVacaciones
        vlo_WsSolicitudVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsSolicitudVacaciones.Timeout = -1

        Try
            Return vlo_WsSolicitudVacaciones.VAT_SOLICITUD_VAC_ObtieneUnidadesActivasEmpleado(
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                   pvn_NumeroEmpleado)


        Catch ex As Exception
            Throw
        Finally
            If vlo_WsSolicitudVacaciones IsNot Nothing Then
                vlo_WsSolicitudVacaciones.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvn_CodUnidadSirh"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Function ObtenerDirectorUnidad(ByVal pvn_CodUnidadSirh As Integer) As Integer
        Dim vlo_Estructura As WsrCatalogosVacaciones.PLM_ESTRUCTURA_ORG
        Dim vlo_BLLPlanillas As WsrCatalogosVacaciones.WsCatalogosVacaciones
        Dim vlc_Condicion As String

        Try
            vlo_BLLPlanillas = New WsrCatalogosVacaciones.WsCatalogosVacaciones
            vlo_BLLPlanillas.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_BLLPlanillas.Timeout = -1

            vlc_Condicion = String.Format("COD_UNIDAD_SIRH = {0} AND TIPO = 'UBC' AND ESTADO = '{1}'", pvn_CodUnidadSirh, Estado.ACTIVO)

            vlo_Estructura = vlo_BLLPlanillas.PLM_ESTRUCTURA_ORG_ObtenerRegistro(
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                   vlc_Condicion)

            If vlo_Estructura.NUM_EMPLEADO_SUSTITUTO <> 0 _
                                AndAlso vlo_Estructura.FECHA_DESDE_SUSTITUCION <= DateTime.Now _
                                And (vlo_Estructura.FECHA_HASTA_SUSTITUCION >= DateTime.Now _
                                       Or vlo_Estructura.FECHA_HASTA_SUSTITUCION = Utilerias.OrdenesDeTrabajo.Constantes.fechaNula) Then
                Return vlo_Estructura.NUM_EMPLEADO_SUSTITUTO
            Else
                Return vlo_Estructura.NUM_EMPLEADO_JEFE
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Function

    ''' <summary>
    ''' Carga el extensiones permitidas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarExtensionesArchivo() As String
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As Wsr_OT_Catalogos.EntOtmTipoDocumento

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.FOTOGRAFIA))

            Me.IdTipoDocumento = vlo_EntOtmTipoDocumento.IdTipoDocumento

            Return vlo_EntOtmTipoDocumento.FormatosAdmitidos
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga el tamaño maximo permito
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>09/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarTamañoMaximoArchivo() As Integer
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As Wsr_OT_Catalogos.EntOtmTipoDocumento

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.FOTOGRAFIA))

            Return vlo_EntOtmTipoDocumento.TamanioMaximo
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga la ubicacion favorita
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarUbicacionFavorita(pvn_NumEmpleado As Integer) As Wsr_OT_OrdenesDeTrabajo.EntOtfUbicacionFavorita
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTF_UBICACION_FAVORITA_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTF_UBICACION_FAVORITA.NUM_EMPLEADO, pvn_NumEmpleado))
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
    ''' <param name="pvn_IdUbicacion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>15/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarUnidadesUbicaciones(pvn_IdUbicacion As Integer) As Data.DataSet
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_UNIDAD_UBICACION_ListarRegistrosListaPersonalizado(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_UNIDAD_UBICACION.ID_UBICACION, pvn_IdUbicacion),
                String.Empty,
                False,
                0,
                0)
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
    ''' <creationDate>15/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarParametro(pvn_IdParametro As Integer) As Wsr_OT_Catalogos.EntOtpParametroGlobal
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTP_PARAMETRO_GLOBAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_GLOBAL.ID_PARAMETRO, pvn_IdParametro))
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga la categoria de servicio
    ''' </summary>
    ''' <param name="pvn_IdCategoriaServicio"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargarCategoriaServicio(pvn_IdCategoriaServicio As Integer) As Wsr_OT_Catalogos.EntOtmCategoriaServicio
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO, pvn_IdCategoriaServicio))
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga la etapa de la orden
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargarEtapaOrdenTrabajo() As Wsr_OT_Catalogos.EntOtmEtapaOrdenTrabajo
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_ETAPA_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_ETAPA_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.SOLICITUD))
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

#End Region


End Class
