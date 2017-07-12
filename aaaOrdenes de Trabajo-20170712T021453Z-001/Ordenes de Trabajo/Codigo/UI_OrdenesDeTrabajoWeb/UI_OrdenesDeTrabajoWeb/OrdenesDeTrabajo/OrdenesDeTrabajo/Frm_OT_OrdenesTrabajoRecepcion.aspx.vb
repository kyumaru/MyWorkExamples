Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Frm_OT_OrdenesTrabajoRecepcion
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' prpopiedad de retorno
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property CadenaLlave As String
        Get
            Return CType(ViewState("CadenaLlave"), String)
        End Get
        Set(value As String)
            ViewState("CadenaLlave") = value
        End Set
    End Property

    ''' <summary>
    ''' id para el tipo de documento permitido
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/02/2016</creationDate>
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
    ''' <creationDate>02/02/2016</creationDate>
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
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' Propiedad para la convocatoria a trabajar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' Propiedad para la PRE ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/04/2016</creationDate>
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
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Property TipoOrden As Integer
        Get
            Return CType(ViewState("TipoOrden"), Integer)
        End Get
        Set(value As Integer)
            ViewState("TipoOrden") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' Propiedad para el dataset de archivos adjuntos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' propiedad para el codigo de unidad de servicios generales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property CodigoUnidad As Integer
        Get
            Return CType(ViewState("CodigoUnidad"), Integer)
        End Get
        Set(value As Integer)
            ViewState("CodigoUnidad") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/09/2015</creationDate>
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
    ''' Prpoiedad para cargar datos del empleado en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/09/2015</creationDate>
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
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>13/10/2015</creationDate>
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
    ''' <creationDate>18/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)
                If Me.AutorizadoUbicacion.Existe Then
                    InicializarFormulario()
                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no se encuentra autorizado para registrar ordenes de trabajo en ninguna sede.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' descarga la imagen adjunta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>09/11/2015</creationDate>
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
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar, ubicada en la 
    ''' primera columna de cada registro del listado de adjuntos, llama a la función borrar y muestra  un mensaje según 
    ''' la operacion efectuada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vln_Indice As Integer

        Try

            Select Case (Me.Operacion)
                Case Is = eOperacion.Agregar
                    vln_Indice = Convert.ToInt32(CType(sender, ImageButton).CommandName)
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
    ''' evento que se ejecuta cuando se cambia el valor del combo de categorias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategoria.SelectedIndexChanged
        Dim vlc_Condicion As String

        Try
            If Me.ddlCategoria.SelectedValue <> String.Empty Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_CATEGORIA_SERVICIO, Me.ddlCategoria.SelectedValue)
                CargarActividad(vlc_Condicion)
            End If
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
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' evento que se ejecuta cuando se da click en el boton de registrar orden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptarOculto_Click(sender As Object, e As EventArgs) Handles btnAceptarOculto.Click
        If Page.IsValid Then
            Try
                Me.Empleado = CargarFuncionario(Me.txtIdSolicitante.Text)

                If Me.Empleado.Existe Then
                    Me.EventoGenerado = 1
                    Select Case (Me.Operacion)
                        Case Is = eOperacion.Agregar
                            If Agregar() Then
                                WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
                            Else
                                MostrarAlertaError("No ha sido posible almacenar la información del nuevo registro")
                            End If

                        Case Is = eOperacion.Modificar
                            If Modificar() Then
                                WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                            Else
                                MostrarAlertaError("No ha sido posible actualizar la información del registro")
                            End If
                    End Select
                Else
                    MostrarAlertaError("El ID del solicitante no corresponde a ningún funcionario.")
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
    ''' evento que se ejecuta cuando se da click en el boton de registrar y enviar orden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptarEnviarOculto_Click(sender As Object, e As EventArgs) Handles btnAceptarEnviarOculto.Click
        If Page.IsValid Then
            Try
                Me.Empleado = CargarFuncionario(Me.txtIdSolicitante.Text)

                If Me.Empleado.Existe Then

                    Me.EventoGenerado = 2

                    If Agregar() Then
                        WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
                    Else
                        MostrarAlertaError("No ha sido posible almacenar la información del nuevo registro")
                    End If


                Else
                    MostrarAlertaError("El ID del solicitante no corresponde a ningún funcionario.")
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
    ''' <creationDate>21/09/2015</creationDate>
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
    ''' <creationDate>21/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkEjecutarBusquedaSolicitante_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusquedaSolicitante.Click
        Try
            Me.wuc_EmpleadosEU.Indicador = 1
            WebUtils.RegistrarScript(Me.Page, "mostrarPopUpBusquedaFuncionario", "javascript:mostrarPopUp('#PopUpBusquedaFuncionario');cargarLupa();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvc_NumeroDeEmpleado"></param>
    ''' <param name="pvc_Identificacion"></param>
    ''' <param name="pvc_NombreCompleto"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub wuc_EmpleadosEU_Aceptar(pvc_NumeroDeEmpleado As Integer, pvc_Identificacion As String, pvc_NombreCompleto As String) Handles wuc_EmpleadosEU.Aceptar
        If Me.wuc_EmpleadosEU.Indicador = 1 Then
            Me.txtIdSolicitante.Text = pvc_Identificacion
            Me.lblNombre.Text = pvc_NombreCompleto
            Me.txtPersonaContacto.Text = pvc_NombreCompleto
            Me.upTxtIdSolicitante.Update()
            Me.upLblNombre.Update()
            Me.upTxtPersonaContacto.Update()
        End If

        WebUtils.RegistrarScript(Me.Page, "InicializarFormulario", "javascript:inicializarFormulario();ocultarFiltroFuncionario();")
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub txtIdSolicitante_TextChanged(sender As Object, e As EventArgs) Handles txtIdSolicitante.TextChanged
        Dim vlo_ServiciosCurriculos As WsrEU_Curriculo.wsEU_Curriculo
        Dim vlo_DsEmpleados As Data.DataSet
        Dim pvc_CondicionBusquedas As String

        Try
            'instanciar y configurar objetos
            vlo_ServiciosCurriculos = New WsrEU_Curriculo.wsEU_Curriculo
            vlo_ServiciosCurriculos.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_ServiciosCurriculos.Timeout = -1

            Me.lblNombre.Text = ""
            Me.txtPersonaContacto.Text = ""

            If Me.txtIdSolicitante.Text <> "" Then
                pvc_CondicionBusquedas = String.Format("{0} = '{1}'", "ID_PERSONAL", Me.txtIdSolicitante.Text)
                vlo_DsEmpleados = vlo_ServiciosCurriculos.Empleados_ListarRegistrosLista(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB), ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), pvc_CondicionBusquedas, "NOMBRE ASC, APELLIDO1 ASC, APELLIDO2 ASC")
                If vlo_DsEmpleados IsNot Nothing AndAlso vlo_DsEmpleados.Tables.Count > 0 AndAlso vlo_DsEmpleados.Tables(0).Rows.Count > 0 Then
                    Me.lblNombre.Text = String.Format("{0} {1} {2}", vlo_DsEmpleados.Tables(0).Rows(0)(4), vlo_DsEmpleados.Tables(0).Rows(0)(5), vlo_DsEmpleados.Tables(0).Rows(0)(6))
                    Me.txtPersonaContacto.Text = String.Format("{0} {1} {2}", vlo_DsEmpleados.Tables(0).Rows(0)(4), vlo_DsEmpleados.Tables(0).Rows(0)(5), vlo_DsEmpleados.Tables(0).Rows(0)(6))
                Else
                    Me.lblNombre.Text = ""
                    Me.txtPersonaContacto.Text = ""
                    Me.txtIdSolicitante.Text = ""
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaNoEncontrado", "javascript:mostrarAlertaNoEncontrado();")

                End If
                WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarFormulario();")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub


    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el boton de 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnActualizarPDAGO_Click(sender As Object, e As EventArgs) Handles btnActualizarPDAGO.Click
        Try
            If ActualizarPDAGO() Then
                WebUtils.RegistrarScript(Me.Page, "mostrarActualizacionExitosa", "mostrarActualizacionExitosa();")
            Else
                MostrarAlertaError("No ha sido posible actualizar el número de orden")
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
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' <creationDate>18/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Dim vlo_DescTipoOrden As String
        Dim vlo_builder = New StringBuilder()

        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        CargarLugarTrabajo(String.Format("{0} LIKE '%{1}%'", Modelo.OTM_LUGAR_TRABAJO.ESTADO, Estado.ACTIVO))
        CargarCategoriaServicio(String.Format("{0} LIKE '%{1}%' AND {2} = 0", Modelo.OTM_CATEGORIA_SERVICIO.ESTADO, Estado.ACTIVO, Modelo.OTM_CATEGORIA_SERVICIO.REQUIERE_FICHA_TECNICA))
        CargarActividad(String.Empty)

        CargarCodigoUnidadServiciosGenerales()

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

                CargarDatosGenerales()

                Me.TipoOrden = WebUtils.LeerParametro(Of Integer)("pvn_TipoOrden")

                vlo_DescTipoOrden = IIf(Me.TipoOrden = 0, "Emergencia", "Ordinaria")
                Me.lblAccion.Text = String.Format("Agregar Orden de Trabajo de {0}", vlo_DescTipoOrden)
                Select Case Me.TipoOrden
                    Case 0
                        Me.btnRegistrar.Visible = False
                        Me.trArchivo.Visible = False
                        Me.trRepeater.Visible = False
                        Me.btnRegistrarEnviar.OnClientClick = "javascript:return validarGuardarEmergencia();"
                    Case 1

                End Select

                Me.txtNumeroOrden.Enabled = False
                Me.btnActualizarPDAGO.Visible = False

            Case Is = eOperacion.Modificar, eOperacion.Consultar

                Me.lblAccion.Text = String.Format("Modificar Orden de Trabajo de Ordinaria")
                Try
                    CargarOrdenTrabajo(WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion"))
                Catch ex As Exception
                    Throw
                End Try
        End Select

        If Me.Operacion = eOperacion.Consultar Then
            CargarCategoriaServicio(String.Format("{0} LIKE '%{1}%'", Modelo.OTM_CATEGORIA_SERVICIO.ESTADO, Estado.ACTIVO))
            Me.lblAccion.Text = String.Format("Consultar Orden de Trabajo de {0}", vlo_DescTipoOrden)
            Me.btnLimpiarFormulario.Enabled = False
            Me.lnkEjecutarBusquedaSolicitante.Visible = False
            WebUtils.RegistrarScript(Me, "deshabilitar", "deshabilitar();")
        End If

    End Sub


    ''' <summary>
    ''' genera el data set de archivos, con respecto a los adjunto a la preorden
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/04/2016</creationDate>
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
    ''' Carga los datos generales del formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarDatosGenerales()
        Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
        Try
            vlo_EntEmpleados = CargarFuncionario(Me.Usuario.UserName)

            Me.lblRegistradaPor.Text = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
            Me.lblFechaRegistro.Text = DateTime.Now.ToString(Constantes.FORMATO_FECHA_UI)

            CargarEstructuraDsAdjunto()

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
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' Carga las catagorias, segun la condicion de busqueda que obtiene por parametro
    ''' </summary>
    ''' <param name="pvc_Condicion">consecutivo de la orden</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarCategoriaServicio(pvc_Condicion As String)
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlCategoria.Items.Clear()
            Me.ddlCategoria.Items.Add(New ListItem("[Seleccione el Valor]", String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ListarRegistros(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                            pvc_Condicion,
                            String.Empty,
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
    ''' <creationDate>18/09/2015</creationDate>
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
                            String.Empty,
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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
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

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_LUGAR_TRABAJO_ListarRegistros(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                pvc_Condicion,
                                String.Format("{0} ASC", Modelo.OTM_LUGAR_TRABAJO.NOMBRE),
                                False,
                                0,
                                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlLugarTrabajo
                    '.Items.Add(New ListItem("[Selecccione el Valor]", String.Empty))
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_LUGAR_TRABAJO.NOMBRE
                    .DataValueField = Modelo.OTM_LUGAR_TRABAJO.ID_LUGAR_TRABAJO
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
    ''' <param name="pvn_IdUbicacion">id ubicacion de la orden</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenTrabajo(pvn_IdUbicacion As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntEmpleadoRegistra As WsrEU_Curriculo.EntEmpleados
        Dim vlo_EntEmpleadoSolicita As WsrEU_Curriculo.EntEmpleados

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            If Me.Operacion = eOperacion.Consultar Then

                If WebUtils.LeerParametro(Of Integer)("pvn_EsPreOrden") = 1 Then
                    Me.PreOrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_ObtenerRegistro(
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                   String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_PRE_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTF_PRE_ORDEN_TRABAJO.ID_PRE_ORDEN_TRABAJO, WebUtils.LeerParametro(Of Integer)("pvn_IdPreOrdenTrabajo")))

                    vlo_EntEmpleadoRegistra = CargarFuncionario(Me.PreOrdenTrabajo.Usuario)
                    vlo_EntEmpleadoSolicita = CargarFuncionarioNumEmpleado(Me.PreOrdenTrabajo.NumEmpleado)

                    If Me.PreOrdenTrabajo.Existe Then
                        With Me.PreOrdenTrabajo
                            Me.lblRegistradaPor.Text = String.Format("{0} {1} {2}", vlo_EntEmpleadoRegistra.NOMBRE, vlo_EntEmpleadoRegistra.APELLIDO1, vlo_EntEmpleadoRegistra.APELLIDO2)
                            Me.lblFechaRegistro.Text = .FechaHoraSolicita.ToString(Constantes.FORMATO_FECHA_HORA_UI)
                            Me.txtIdSolicitante.Text = vlo_EntEmpleadoSolicita.ID_PERSONAL
                            Me.txtPersonaContacto.Text = .NombrePersonaContacto
                            Me.lblNombre.Text = String.Format("{0} {1} {2}", vlo_EntEmpleadoSolicita.NOMBRE, vlo_EntEmpleadoSolicita.APELLIDO1, vlo_EntEmpleadoSolicita.APELLIDO2)
                            Me.txtTelefono.Text = .Telefono
                            Me.ddlLugarTrabajo.SelectedValue = .IdLugarTrabajo
                            Me.txtLugarExacto.Text = .SennasExactas
                            Me.ddlCategoria.SelectedValue = .IdCategoriaServicio
                            Me.ddlActividad.SelectedValue = .IdActividad
                            CargarDescripcionAmpliadaActividad()
                            Me.txtDescTrabajo.Text = .DescripcionTrabajo
                            Me.txtNumeroOrden.Text = ""
                        End With

                        CargaDsAdjuntosModificar()
                        Me.TipoOrden = 1
                        formularioTotal2.Visible = False
                    Else
                        WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
                    End If
                Else

                    Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistroConsulta(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")))
                    If String.IsNullOrWhiteSpace(Me.OrdenTrabajo.NombreTaller) Or String.IsNullOrWhiteSpace(Me.OrdenTrabajo.CoordEncargado) Then
                        Me.trTallerSector.Visible = False
                        Me.trResponsable.Visible = False
                    Else
                        Me.trTallerSector.Visible = True
                        Me.trResponsable.Visible = True
                        Me.lblTallerSector.Text = Me.OrdenTrabajo.NombreTaller
                        Me.lblResponsable.Text = Me.OrdenTrabajo.CoordEncargado
                    End If

                    vlo_EntEmpleadoRegistra = CargarFuncionario(Me.OrdenTrabajo.Usuario)
                    vlo_EntEmpleadoSolicita = CargarFuncionarioNumEmpleado(Me.OrdenTrabajo.NumEmpleado)

                    If Me.OrdenTrabajo.Existe Then
                        With Me.OrdenTrabajo
                            Me.lblRegistradaPor.Text = String.Format("{0} {1} {2}", vlo_EntEmpleadoRegistra.NOMBRE, vlo_EntEmpleadoRegistra.APELLIDO1, vlo_EntEmpleadoRegistra.APELLIDO2)
                            Me.lblFechaRegistro.Text = .FechaHoraSolicita.ToString(Constantes.FORMATO_FECHA_HORA_UI)
                            Me.txtIdSolicitante.Text = vlo_EntEmpleadoSolicita.ID_PERSONAL
                            Me.txtPersonaContacto.Text = .NombrePersonaContacto
                            Me.lblNombre.Text = String.Format("{0} {1} {2}", vlo_EntEmpleadoSolicita.NOMBRE, vlo_EntEmpleadoSolicita.APELLIDO1, vlo_EntEmpleadoSolicita.APELLIDO2)
                            Me.txtTelefono.Text = .Telefono
                            Me.ddlLugarTrabajo.SelectedValue = .IdLugarTrabajo
                            Me.txtLugarExacto.Text = .SennasExactas
                            Me.ddlCategoria.SelectedValue = .IdCategoriaServicio
                            Me.ddlActividad.SelectedValue = .IdActividad
                            CargarDescripcionAmpliadaActividad()
                            Me.txtDescTrabajo.Text = .DescripcionTrabajo
                            Me.txtNumeroOrden.Text = IIf(.NumeroOrden = 0, "", .NumeroOrden)
                        End With

                        CargaDsAdjuntos()

                        Select Case Me.OrdenTrabajo.TipoOrdenTrabajo
                            Case Utilerias.OrdenesDeTrabajo.TipoOrden.EMERGENCIA
                                Me.TipoOrden = 0
                                Me.btnRegistrar.Visible = False
                                Me.trArchivo.Visible = False
                                Me.trRepeater.Visible = False

                            Case Utilerias.OrdenesDeTrabajo.TipoOrden.ORDINARIA
                                Me.TipoOrden = 1
                        End Select
                    Else
                        WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
                    End If

                End If
            Else
                Me.TipoOrden = 1
                formularioTotal2.Visible = False
                btnRegistrarEnviar.Visible = False
                Me.btnRegistrar.OnClientClick = "javascript:return validarGuardarModificar();"

                Me.PreOrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_PRE_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTF_PRE_ORDEN_TRABAJO.ID_PRE_ORDEN_TRABAJO, WebUtils.LeerParametro(Of Integer)("pvn_IdPreOrdenTrabajo")))

                vlo_EntEmpleadoRegistra = CargarFuncionario(Me.PreOrdenTrabajo.Usuario)
                vlo_EntEmpleadoSolicita = CargarFuncionarioNumEmpleado(Me.PreOrdenTrabajo.NumEmpleado)

                If Me.PreOrdenTrabajo.Existe Then
                    With Me.PreOrdenTrabajo
                        Me.lblRegistradaPor.Text = String.Format("{0} {1} {2}", vlo_EntEmpleadoRegistra.NOMBRE, vlo_EntEmpleadoRegistra.APELLIDO1, vlo_EntEmpleadoRegistra.APELLIDO2)
                        Me.lblFechaRegistro.Text = .FechaHoraSolicita.ToString(Constantes.FORMATO_FECHA_HORA_UI)
                        Me.txtIdSolicitante.Text = vlo_EntEmpleadoSolicita.ID_PERSONAL
                        Me.txtPersonaContacto.Text = .NombrePersonaContacto
                        Me.lblNombre.Text = String.Format("{0} {1} {2}", vlo_EntEmpleadoSolicita.NOMBRE, vlo_EntEmpleadoSolicita.APELLIDO1, vlo_EntEmpleadoSolicita.APELLIDO2)
                        Me.txtTelefono.Text = .Telefono
                        Me.ddlLugarTrabajo.SelectedValue = .IdLugarTrabajo
                        Me.txtLugarExacto.Text = .SennasExactas
                        Me.ddlCategoria.SelectedValue = .IdCategoriaServicio
                        Me.ddlActividad.SelectedValue = .IdActividad
                        CargarDescripcionAmpliadaActividad()
                        Me.txtDescTrabajo.Text = .DescripcionTrabajo
                        Me.txtNumeroOrden.Text = ""
                    End With

                    CargaDsAdjuntosModificar()
                Else
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
                End If

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
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' Carga la estructura de la tabla de adjuntos OTF_ADJUNTO_ORDEN_TRABAJO
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarCodigoUnidadServiciosGenerales()
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtpParametro As Wsr_OT_Catalogos.EntOtpParametroGlobal

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtpParametro = vlo_Ws_OT_Ws_OT_Catalogos.OTP_PARAMETRO_GLOBAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_GLOBAL.ID_PARAMETRO, Parametros.CODIGO_UNIDAD_SERVICIOS_GENERALES))

            If vlo_EntOtpParametro.Existe Then
                Me.CodigoUnidad = CType(vlo_EntOtpParametro.Valor, Integer)
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('No se ha sido posible cargar el código de unidad de Servicios Generales.','../../Genericos/Frm_MenuPrincipal.aspx');")
            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion encargada de contruir el registro  de la orden de trabajo
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo
        Dim vlo_EntOttOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo

        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOttOrdenTrabajo = New Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo
            vlo_EntOttOrdenTrabajo.IdUbicacion = Me.AutorizadoUbicacion.IdUbicacionAdministra
            vlo_EntOttOrdenTrabajo.Anno = DateTime.Now.Year
            vlo_EntOttOrdenTrabajo.FechaHoraSolicita = DateTime.Now
        Else
            vlo_EntOttOrdenTrabajo = Me.OrdenTrabajo
        End If

        With vlo_EntOttOrdenTrabajo
            .TipoOrdenTrabajo = IIf(Me.TipoOrden = 0, Utilerias.OrdenesDeTrabajo.TipoOrden.EMERGENCIA, Utilerias.OrdenesDeTrabajo.TipoOrden.ORDINARIA)
            .Parentesco = "MAD"
            .NumEmpleado = Me.Empleado.NUM_EMPLEADO
            .IdCategoriaServicio = Me.ddlCategoria.SelectedValue
            .IdActividad = Me.ddlActividad.SelectedValue
            .IdLugarTrabajo = Me.ddlLugarTrabajo.SelectedValue
            .CodUnidadSirh = Me.CodigoUnidad
            .NombrePersonaContacto = Me.txtPersonaContacto.Text
            .Telefono = Me.txtTelefono.Text
            .SennasExactas = Me.txtLugarExacto.Text
            .DescripcionTrabajo = Me.txtDescTrabajo.Text
            .Usuario = Me.Usuario.UserName
            .IncluidaEnRecepcion = 1
        End With

        Return vlo_EntOttOrdenTrabajo
    End Function

    ''' <summary>
    ''' Funcion encargada de el registro de la pre orden trabajo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistroPreOrden() As Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo
        Dim vlo_EntOtfPreOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo

        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtfPreOrdenTrabajo = New Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo
            vlo_EntOtfPreOrdenTrabajo.IdUbicacion = Me.AutorizadoUbicacion.IdUbicacionAdministra
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
            .CodUnidadSirh = Me.CodigoUnidad
            .NombrePersonaContacto = Me.txtPersonaContacto.Text
            .Telefono = Me.txtTelefono.Text
            .SennasExactas = Me.txtLugarExacto.Text
            .DescripcionTrabajo = Me.txtDescTrabajo.Text
            .Usuario = Me.Usuario.UserName
            .IncluidaEnRecepcion = 1
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
    ''' Administra el proceso para agregar una orden de trabajo incluida desde recepcion
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>21/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo
        Dim vlo_EntOtfPreOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If Me.TipoOrden = 1 Then
                If Roles.IsUserInRole(Membership.GetUser.UserName, ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ROL_OT_AUTORIZADOR_SOLICITUD)) Then
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
                                vlo_EntOttOrdenTrabajo, Me.DsAdjuntosInsert, Me.IdTipoDocumento, Me.EtapaOrdenTrabajo.IdEtapaOrdenTrabajo, DescripcionAdjuntos.ORDENES_RECEPCION)
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

                        vlo_EntOtfPreOrdenTrabajo = ConstruirRegistroPreOrden()

                        CadenaLlave = vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_InsertarPreOrdenCadena(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                vlo_EntOtfPreOrdenTrabajo,
                                EstadoOrden.PENDIENTE_REVISION_DIRECTOR)
                    End If
                End If
            Else
                vlo_EntOttOrdenTrabajo = ConstruirRegistro()
                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.ASIGNADA

                CadenaLlave = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_InsertarOrdenTrabajoConAdjuntosCadena(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                        vlo_EntOttOrdenTrabajo, Me.DsAdjuntosInsert, Me.IdTipoDocumento, Me.EtapaOrdenTrabajo.IdEtapaOrdenTrabajo, DescripcionAdjuntos.ORDENES_RECEPCION)
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
    ''' <creationDate>21/09/2015</creationDate>
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
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>03/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ActualizarPDAGO() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOttOrdenTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOttOrdenTrabajo = Me.OrdenTrabajo

        vlo_EntOttOrdenTrabajo.NumeroOrden = IIf(Me.txtNumeroOrden.Text <> String.Empty, Me.txtNumeroOrden.Text, 0)

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOttOrdenTrabajo) > 0
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
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' carga las ubicaciones sugun el usuario conectado
    ''' </summary>
    ''' <param name="pvn_NumeroEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/09/2015</creationDate>
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
    ''' 
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>13/10/2015</creationDate>
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
    ''' Carga el extensiones permitidas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/02/2016</creationDate>
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
    ''' carga la categoria de servicio
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>02/02/2016</creationDate>
    ''' <changeLog></changeLog>
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
