Imports Utilerias.OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Frm_OT_SelecciónSedeTrabajo
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/10/2015</creationDate>
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
    ''' Propiedad para la ubicacion favorita
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
    ''' data set para los nombramientos del usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsNombramientos As Data.DataSet
        Get
            Return CType(ViewState("DsNombramientos"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsNombramientos") = value
        End Set
    End Property

    ''' <summary>
    ''' data set para los unidad ubicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsUnidadUbicacion As Data.DataSet
        Get
            Return CType(ViewState("DsUnidadUbicacion"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsUnidadUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' data set para las ubicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsUbicacion As Data.DataSet
        Get
            Return CType(ViewState("DsUbicacion"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsUbicacion") = value
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
    ''' <creationDate>07/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click en el boton aceptar 
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try
                If AsignarSedeTrabajo() Then
                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                Else
                    MostrarAlertaError("No se ha podido actualizar la información de las ordenes de trabajo preventivo.")
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

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' inicializa el formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Try

            Me.Usuario = New UsuarioActual
            Me.UbicacionFavorita = CargarUbicacionFavorita(Me.Usuario.NumEmpleado)

            Me.DsNombramientos = CargarNombramientos(Me.Usuario.NumEmpleado)

            If DsNombramientos IsNot Nothing AndAlso DsNombramientos.Tables(0).Rows.Count > 0 Then

                'If Not Me.UbicacionFavorita.Existe Then

                '    ' Me.DsUnidadUbicacion = CargarUnidadesUbicacion(ObtenerCondicionBusquedaUnidadUbicacion)

                '    'If Me.DsUnidadUbicacion IsNot Nothing AndAlso Me.DsUnidadUbicacion.Tables(0).Rows.Count > 0 Then

                '    '  Me.DsUbicacion = CargarUbicaciones(ObtenerCondicionBusquedaUbicacion)
                '    Me.DsUbicacion = CargarUbicaciones(String.Empty)

                '    'Else
                '    'WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Sus unidades aún no poseen ninguna sede asociada en el sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                '    '  End If
                'Else
                '    Me.DsUbicacion = CargarUbicaciones(String.Empty)
                'End If

                Me.DsUbicacion = CargarUbicaciones(String.Empty)

                If Me.DsUbicacion IsNot Nothing AndAlso Me.DsUbicacion.Tables(0).Rows.Count > 0 Then
                    With Me.ddlSede
                        .Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
                        .DataSource = Me.DsUbicacion
                        .DataMember = Me.DsUbicacion.Tables(0).TableName
                        .DataTextField = Modelo.OTM_UBICACION.DESCRIPCION
                        .DataValueField = Modelo.OTM_UBICACION.ID_UBICACION
                        .DataBind()
                    End With
                End If

                If Not Me.UbicacionFavorita.Existe Then
                    DeterminaSugerenciaSede()
                Else
                    Me.ddlSede.SelectedValue = Me.UbicacionFavorita.IdUbicacion
                End If
            Else
                WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no se encuentra asociado a ninguna unidad.','../../Genericos/Frm_MenuPrincipal.aspx');")
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' determina cual sera la sede que se le sugerira como base
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub DeterminaSugerenciaSede()
        Dim vln_CodUnidadSirh As Integer
        Dim vlo_EntOtmUnidadUbicacion As Wsr_OT_Catalogos.EntOtmUnidadUbicacion

        Try
            Dim vlo_Vista As New Data.DataView(Me.DsNombramientos.Tables(0))
            vlo_Vista.Sort = "SUMA_DEDICAC DESC"

            vln_CodUnidadSirh = CType(vlo_Vista.Table.Rows(0)(1), Integer)

            vlo_EntOtmUnidadUbicacion = CargarUnidadUbicacion(vln_CodUnidadSirh)

            If vlo_EntOtmUnidadUbicacion.Existe Then
                Me.ddlSede.SelectedValue = vlo_EntOtmUnidadUbicacion.IdUbicacion
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Funciones"

    ' ''' <summary>
    ' ''' carga el string con la condicion de busqueda de unidad ubicacion
    ' ''' </summary>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    ' ''' <author>Carlos Gómez Ondoy</author>
    ' ''' <creationDate>07/10/2015</creationDate>
    ' ''' <changeLog></changeLog>
    'Private Function ObtenerCondicionBusquedaUnidadUbicacion() As String
    '    Dim vlc_Condicion As String
    '    vlc_Condicion = String.Empty

    '    For Each vlo_Fila In Me.DsNombramientos.Tables(0).Rows
    '        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
    '            vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OTM_UNIDAD_UBICACIONLST.COD_UNIDAD_SIRH, vlo_Fila("CODIGO_UBICA").ToString)
    '        Else
    '            vlc_Condicion = String.Format("{0} OR {1} = {2}", vlc_Condicion, Modelo.V_OTM_UNIDAD_UBICACIONLST.COD_UNIDAD_SIRH, vlo_Fila("CODIGO_UBICA").ToString)
    '        End If
    '    Next

    '    Return vlc_Condicion
    'End Function

    ' ''' <summary>
    ' ''' carga el string con la condicion de busqueda de las ubicaciones
    ' ''' </summary>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    ' ''' <author>Carlos Gómez Ondoy</author>
    ' ''' <creationDate>07/10/2015</creationDate>
    ' ''' <changeLog></changeLog>
    'Private Function ObtenerCondicionBusquedaUbicacion() As String
    '    Dim vlc_Condicion As String
    '    vlc_Condicion = String.Empty

    '    For Each vlo_Fila In Me.DsUnidadUbicacion.Tables(0).Rows
    '        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
    '            vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OTM_UBICACIONLST.ID_UBICACION, vlo_Fila(Modelo.V_OTM_UNIDAD_UBICACIONLST.ID_UBICACION).ToString)
    '        Else
    '            vlc_Condicion = String.Format("{0} OR {1} = {2}", vlc_Condicion, Modelo.V_OTM_UBICACIONLST.ID_UBICACION, vlo_Fila(Modelo.V_OTM_UNIDAD_UBICACIONLST.ID_UBICACION).ToString)
    '        End If
    '    Next

    '    Return vlc_Condicion
    'End Function

    ''' <summary>
    ''' retorna las unidades en las cuales esta nombrado un funcionario
    ''' </summary>
    ''' <param name="pvn_NumeroEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarNombramientos(pvn_NumeroEmpleado As Integer) As Data.DataSet
        Dim vlo_WsSolicitudVacaciones As WsrSolicitudVacaciones.WsSolicitudVacaciones

        vlo_WsSolicitudVacaciones = New WsrSolicitudVacaciones.WsSolicitudVacaciones
        vlo_WsSolicitudVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsSolicitudVacaciones.Timeout = -1

        Try

            Return vlo_WsSolicitudVacaciones.VAH_PERIODOS_DEL_EMPLEADO_ObtenerDedicacionSIRHDatos(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_NumeroEmpleado,
                1)

        Catch ex As Exception
            Throw
        Finally
            If vlo_WsSolicitudVacaciones IsNot Nothing Then
                vlo_WsSolicitudVacaciones.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga un dataset con las unidad ubicacion segun criterio de busqueda que obtiene por parametro
    ''' </summary>
    ''' <param name="pvn_Condicion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarUnidadesUbicacion(pvn_Condicion As String) As Data.DataSet
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_Ws_OT_Catalogos.Timeout = -1

        Try

            Return vlo_Ws_OT_Catalogos.OTM_UNIDAD_UBICACION_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_Condicion,
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
    ''' carga un dataset con las ubicaciones segun criterio de busqueda que obtiene por parametro
    ''' </summary>
    ''' <param name="pvn_Condicion"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarUbicaciones(pvn_Condicion As String) As Data.DataSet
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_Ws_OT_Catalogos.Timeout = -1

        Try

            Return vlo_Ws_OT_Catalogos.OTM_UBICACION_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_Condicion,
                String.Format("{0} {1}", Modelo.V_OTM_UBICACIONLST.DESCRIPCION, Ordenamiento.ASCENDENTE),
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
    ''' asigna la sede de trabajo del funcionario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>07/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function AsignarSedeTrabajo() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOtfUbicacionFavorita As Wsr_OT_OrdenesDeTrabajo.EntOtfUbicacionFavorita

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1

        Try

            If Not Me.UbicacionFavorita.Existe Then
                vlo_EntOtfUbicacionFavorita = New Wsr_OT_OrdenesDeTrabajo.EntOtfUbicacionFavorita
                vlo_EntOtfUbicacionFavorita.NumEmpleado = Me.Usuario.NumEmpleado
                vlo_EntOtfUbicacionFavorita.IdUbicacion = CType(Me.ddlSede.SelectedValue, Integer)

                Return vlo_Ws_OT_OrdenesDeTrabajo.OTF_UBICACION_FAVORITA_InsertarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOtfUbicacionFavorita) > 0
            Else

                Me.UbicacionFavorita.IdUbicacion = CType(Me.ddlSede.SelectedValue, Integer)

                Return vlo_Ws_OT_OrdenesDeTrabajo.OTF_UBICACION_FAVORITA_ModificarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    Me.UbicacionFavorita) > 0

            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
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
    ''' <creationDate>07/10/2015</creationDate>
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
    ''' <param name="pvn_CodUnidadSirh"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>09/10/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarUnidadUbicacion(pvn_CodUnidadSirh As Integer) As Wsr_OT_Catalogos.EntOtmUnidadUbicacion
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_Ws_OT_Catalogos.Timeout = -1

        Try

            Return vlo_Ws_OT_Catalogos.OTM_UNIDAD_UBICACION_ObtenerRegistro(
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
    End Function

#End Region

End Class
