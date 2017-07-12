Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos

Partial Class Catalogos_Frm_OT_UnidadesPorSede
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
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
    ''' Propiedad para la unidad ubicacion a trabajar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property UnidadUbicacion As Wsr_OT_Catalogos.EntOtmUnidadUbicacion
        Get
            Return CType(ViewState("UnidadUbicacion"), Wsr_OT_Catalogos.EntOtmUnidadUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmUnidadUbicacion)
            ViewState("UnidadUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Prpoiedad para cargar datos la unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property Unidad As WsrCatalogosVacaciones.PLM_ESTRUCTURA_ORG
        Get
            Return CType(ViewState("Unidad"), WsrCatalogosVacaciones.PLM_ESTRUCTURA_ORG)
        End Get
        Set(value As WsrCatalogosVacaciones.PLM_ESTRUCTURA_ORG)
            ViewState("Unidad") = value
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
    ''' <creationDate>05/10/2015</creationDate>
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
    ''' Evento que se ejecuta cuando se da click en el boton aceptar para agregar un nuevo registro
    ''' llama a la funcion procesar y muestra un mensaje segun la operacion realizada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try

                Me.Unidad = CargarUnidad(Me.txtUnidad.Text.Trim)

                If Me.Unidad.Existe Then
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
                    MostrarAlertaError("El código bridado no corresponde a ningúna unidad.")
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
    ''' Método para cargar la lupa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnLimpiarFormulario_Click(sender As Object, e As EventArgs) Handles btnLimpiarFormulario.Click
        WebUtils.RegistrarScript(Me.Page, "CargarLupa", "javascript:cargarLupa();")
    End Sub

    ''' <summary>
    ''' se ejecuta cuando se da click en el link de popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
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
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
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
    ''' <creationDate>05/10/2015</creationDate>
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
    ''' <creationDate>05/10/2015</creationDate>
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
    ''' <creationDate>05/10/2015</creationDate>
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
    ''' <creationDate>05/10/2015</creationDate>
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
                WebUtils.RegistrarScript(Me.Page, "EstablecerControles", "javascript:inicializarScript();")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' carga los datos de la unidad que obtiene por parametro
    ''' </summary>
    ''' <param name="pvn_CodUnidadSirh"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
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
    ''' Busca unidades segun el criterio de búsqueda que obtiene por parametro
    ''' </summary>
    ''' <param name="pvc_CondicionBusquedas"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
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
    ''' limpia los campos de búsqueda y datos del popup
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LimpiarFormulario()
        Me.txtCodigo.Text = String.Empty
        Me.txtDescripcion.Text = String.Empty
        Me.grdUnidades.DataSource = Nothing
        Me.grdUnidades.DataBind()
        Me.txtDescripcion.Focus()
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar el formulario segun la operacion a realizar
    ''' en caso de ser agregar carga el texo con "Agregar", en caso de ser modificar 
    ''' cargar el texto con "Modificar" y llama al método que obtiene los datos de la orden seleccionada segun el id 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        CargarSedes()

        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Unidad por Sede"
            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Unidad por Sede"
                Try
                    CargarUnidadUbicacion(WebUtils.LeerParametro(Of Integer)("pvn_CodUnidadSirh"))
                    Me.txtUnidad.Enabled = False
                    Me.lnkEjecutarBusquedaUnidad.Visible = False
                Catch ex As Exception
                    Throw
                End Try
        End Select
    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la unidad ubicacion segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_CodUnidadSirh">identificacion ubicacion</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarUnidadUbicacion(pvn_CodUnidadSirh As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.UnidadUbicacion = vlo_Ws_OT_Catalogos.OTM_UNIDAD_UBICACION_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_UNIDAD_UBICACION.COD_UNIDAD_SIRH, pvn_CodUnidadSirh))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If Me.UnidadUbicacion.Existe Then
            With Me.UnidadUbicacion
                Me.txtUnidad.Text = .CodUnidadSirh
                Me.ddlSede.SelectedValue = .IdUbicacion
                Me.lblNombreUnidad.Text = CargarUnidad(.CodUnidadSirh).DESCRIPCION
            End With
        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    ''' <summary>
    ''' carga los registros de otm_ubicacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarSedes()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlSede.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODAS, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_UBICACION_ListarRegistros(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} LIKE '%{1}%'", Modelo.OTM_UBICACION.ESTADO, Estado.ACTIVO),
               String.Format("{0} {1}", Modelo.OTM_UBICACION.DESCRIPCION, Ordenamiento.ASCENDENTE),
               False,
               0,
               0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlSede
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_UBICACION.DESCRIPCION
                    .DataValueField = Modelo.OTM_UBICACION.ID_UBICACION
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

#End Region

#Region "Funciones"

    ''' <summary>
    ''' retorna la condicion de busqueda de unidades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
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
    ''' <creationDate>05/10/2015</creationDate>
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
    ''' retorna el dataset de unidades, segun criterio de busqueda
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
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

    ''' <summary>
    ''' Funcion encargada de contruir el registro  de la unidad ubicacion
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_Catalogos.EntOtmUnidadUbicacion
        Dim vlo_EntOtmUnidadUbicacion As Wsr_OT_Catalogos.EntOtmUnidadUbicacion

        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtmUnidadUbicacion = New Wsr_OT_Catalogos.EntOtmUnidadUbicacion
            vlo_EntOtmUnidadUbicacion.CodUnidadSirh = CType(Me.txtUnidad.Text.Trim, Integer)
        Else
            vlo_EntOtmUnidadUbicacion = Me.UnidadUbicacion
        End If

        With vlo_EntOtmUnidadUbicacion
            .IdUbicacion = CType(Me.ddlSede.SelectedValue, Integer)
            .Usuario = Me.Usuario.UserName
        End With

        Return vlo_EntOtmUnidadUbicacion
    End Function

    ''' <summary>
    ''' Administra el proceso para agregar una unidad ubicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmUnidadUbicacion As Wsr_OT_Catalogos.EntOtmUnidadUbicacion

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmUnidadUbicacion = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_UNIDAD_UBICACION_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmUnidadUbicacion) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Administra el proceso para modificar una unidad ubicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>05/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmUnidadUbicacion As Wsr_OT_Catalogos.EntOtmUnidadUbicacion

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmUnidadUbicacion = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_UNIDAD_UBICACION_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmUnidadUbicacion) > 0
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
