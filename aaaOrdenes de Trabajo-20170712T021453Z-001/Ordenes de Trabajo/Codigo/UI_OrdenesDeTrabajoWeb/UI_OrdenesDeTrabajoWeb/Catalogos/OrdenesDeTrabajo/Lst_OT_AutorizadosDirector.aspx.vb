Imports Utilerias.OrdenesDeTrabajo
Imports WsrEU_Curriculo

Partial Class Catalogos_Lst_OT_AutorizadosDirector
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortExpression As String
        Get
            If ViewState("UltimoSortExpression") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortExpression"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortExpression") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima columna de clasificacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortColumn As String
        Get
            If ViewState("UltimoSortColumn") Is Nothing Then
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortColumn"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortColumn") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ultima direccion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property UltimoSortDirection As SortDirection
        Get
            If ViewState("UltimoSortDirection") Is Nothing Then
                Return SortDirection.Ascending
            End If
            Return CType(ViewState("UltimoSortDirection"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("UltimoSortDirection") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en sesion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property Usuario As UsuarioActual
        Get
            Return CType(ViewState("Usuario"), UsuarioActual)
        End Get
        Set(value As UsuarioActual)
            ViewState("Usuario") = value
        End Set
    End Property

    ' ''' <summary>
    ' ''' Propiedad para el usuario en sesion 
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    ' ''' <author>Carlos Gómez Ondoy</author>
    ' ''' <creationDate>25/09/2015</creationDate>
    ' ''' <changeLog></changeLog>
    'Private Property EntEmpleados As EntEmpleados
    '    Get
    '        Return CType(ViewState("EntEmpleados"), EntEmpleados)
    '    End Get
    '    Set(value As EntEmpleados)
    '        ViewState("EntEmpleados") = value
    '    End Set
    'End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsUnidades As Data.DataSet
        Get
            Return CType(ViewState("DsUnidades"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsUnidades") = value
        End Set
    End Property

    Private Property IdentificacionRole As String
        Get
            Return CType(ViewState("IdentificacionRole"), String)
        End Get
        Set(value As String)
            ViewState("IdentificacionRole") = value
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
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try

                Me.Usuario = New UsuarioActual
                '  Me.EntEmpleados = CargarFuncionario(Me.Usuario.UserName)
                Me.DsUnidades = ObtenerUnidadesAcargoDeFuncionario(Me.Usuario.NumEmpleado)

                If Me.DsUnidades IsNot Nothing AndAlso Me.DsUnidades.Tables(0).Rows.Count > 0 Then
                    Buscar(ObtenerCondicionBusqueda(), String.Empty)
                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee unidades a su cargo.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If

            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
        Me.pnRpAutorizadoDirector.Dibujar()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los encabezados de la tabla del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkRpAutorizadoDirector_Command(sender As Object, e As CommandEventArgs)
        Try
            CargarLista(ObtenerCondicionBusqueda, ObtenerExpresionDeOrdenamiento(e.CommandName), pnRpAutorizadoDirector.PaginaActualLista)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre alguno de los números del paginador
    ''' </summary>
    ''' <param name="pvn_PaginaSeleccionada"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub pnRpAutorizadoDirector_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpAutorizadoDirector.CambioDePagina
        Try
            CargarLista(ObtenerCondicionBusqueda, UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al carar el repeater del listado, asigna atributos a cada elemento del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpAutorizadoDirector_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpAutorizadoDirector.ItemDataBound
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
    ''' segunda columna de cada registro del listado, llama a la función borrar y muestra  un mensaje según 
    ''' la operacion efectuada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_Llave As String()

        Try
            vlo_Llave = CType(sender, ImageButton).CommandArgument.Split("¬")

            If Borrar(vlo_Llave(0), vlo_Llave(1)) Then
                DesAsignarRole(Me.IdentificacionRole)
                MostrarAlertaRegistroBorrado()
                Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)
            Else
                MostrarAlertaRegistroNoBorrado()
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

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre el botón Buscar,
    ''' que se encuentra en el área de filtros.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Buscar(ObtenerCondicionBusqueda, UltimoSortExpression)
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
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaRegistroBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroBorrado", "mostrarAlertaRegistroBorrado();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaRegistroNoBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroNoBorrado", "mostrarAlertaRegistroNoBorrado();")
    End Sub

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvc_Condicion">parámetro con la condicion de búsqueda</param>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_AUTORIZADO_DIRECTOR_ListarRegistrosListaPersonalizado(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpAutorizadoDirector
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End With
            Else
                With Me.rpAutorizadoDirector
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

            If vlo_DsDatos IsNot Nothing Then
                vlo_DsDatos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de realizar la busqueda de registros segun los datos de 
    ''' condicion y orden de búsqueda
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_EntDatosPaginacion As Wsr_OT_Catalogos.EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = String.Format("{0}, {1} ASC", ObtenerExpresionDeOrdenamiento(Modelo.V_OTM_AUTORIZADO_DIRECTORLST.NOMBRE), Modelo.V_OTM_AUTORIZADO_DIRECTORLST.COD_UNIDAD_SIRH)
        End If

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntDatosPaginacion =
                vlo_Ws_OT_Catalogos.OTM_AUTORIZADO_DIRECTOR_ObtenerDatosPaginacionVOtmAutorizadoDirectorlst(
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                     ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                     pvc_Condicion,
                     pvc_Orden,
                     CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpAutorizadoDirector.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                CargarLista(pvc_Condicion, pvc_Orden, 1)
                Me.pnRpAutorizadoDirector.Dibujar()
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de Autorizados {0}", vlo_EntDatosPaginacion.TotalRegistros)
            Else
                Me.lblCantidadDeRegistros.Visible = False
                Me.lblCantidadDeRegistros.Text = String.Empty
                MostrarAlertaNoHayDatos()
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
    ''' Des asigna el Role de autorizado por director
    ''' </summary>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub DesAsignarRole(ByVal pvc_NumIdentificacion As String)
        Try
            Dim vlc_RoleName As String
            vlc_RoleName = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ROL_OT_AUTORIZADOR_SOLICITUD)
            Roles.RemoveUserFromRole(pvc_NumIdentificacion.Trim, vlc_RoleName)
            ' Roles.RemoveUsersFromRole(New String() {pvc_NumIdentificacion.Trim}, vlc_RoleName)
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Función encargada de comunicarse con el  servicio web y proceder a borrar el registro
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <param name="pvn_CodigoUnidad"></param>
    ''' <returns>Si retorna un número mayor a 0 quiere decir que la operacion se realizo con éxito</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function Borrar(pvn_NumEmpleado As Integer, pvn_CodigoUnidad As Integer) As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmAutorizadoDirector As Wsr_OT_Catalogos.EntOtmAutorizadoDirector

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmAutorizadoDirector = New Wsr_OT_Catalogos.EntOtmAutorizadoDirector
        vlo_EntOtmAutorizadoDirector.NumEmpleado = pvn_NumEmpleado
        vlo_EntOtmAutorizadoDirector.CodUnidadSirh = pvn_CodigoUnidad

        Me.IdentificacionRole = CargarFuncionarioNumEmpleado(pvn_NumEmpleado).ID_PERSONAL

        Try
            Return vlo_Ws_OT_Catalogos.OTM_AUTORIZADO_DIRECTOR_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmAutorizadoDirector) > 0
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
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String
        Dim vlc_SubCondicion As String
        vlc_Condicion = String.Empty
        vlc_SubCondicion = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtIdPersonal.Text.Trim) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'", Modelo.V_OTM_AUTORIZADO_DIRECTORLST.ID_PERSONAL, Me.txtIdPersonal.Text.Trim)
            Else
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_Condicion, Modelo.V_OTM_AUTORIZADO_DIRECTORLST.ID_PERSONAL, Me.txtIdPersonal.Text.Trim)
            End If
        End If

        For Each vlo_Fila In Me.DsUnidades.Tables(0).Rows
            If String.IsNullOrWhiteSpace(vlc_SubCondicion) Then
                vlc_SubCondicion = String.Format("{0} = {1}", Modelo.V_OTM_AUTORIZADO_DIRECTORLST.COD_UNIDAD_SIRH, vlo_Fila("COD_UNIDAD_SIRH").ToString)
            Else
                vlc_SubCondicion = String.Format("{0} OR {1} = {2}", vlc_SubCondicion, Modelo.V_OTM_AUTORIZADO_DIRECTORLST.COD_UNIDAD_SIRH, vlo_Fila("COD_UNIDAD_SIRH").ToString)
            End If
        Next

        If Not String.IsNullOrWhiteSpace(vlc_SubCondicion) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = vlc_SubCondicion
            Else
                vlc_Condicion = String.Format("{0} AND ({1})", vlc_Condicion, vlc_SubCondicion)
            End If

        End If

        Return vlc_Condicion
    End Function


    ''' <summary>
    ''' funcion encargada de realizar los oredenamientos segun los valores brindados
    ''' </summary>
    ''' <param name="pvc_Columna"> numero de la columna para el ordenamiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerExpresionDeOrdenamiento(pvc_Columna As String) As String
        If String.IsNullOrWhiteSpace(UltimoSortColumn) OrElse pvc_Columna.CompareTo(UltimoSortColumn) <> 0 Then
            UltimoSortColumn = pvc_Columna
            UltimoSortDirection = SortDirection.Ascending
        Else
            If UltimoSortDirection = SortDirection.Ascending Then
                UltimoSortDirection = SortDirection.Descending
            Else
                UltimoSortDirection = SortDirection.Ascending
            End If
        End If

        UltimoSortExpression = String.Format("{0} {1}, {2} {3}", UltimoSortColumn, IIf(UltimoSortDirection = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE), Modelo.V_OTM_AUTORIZADO_DIRECTORLST.COD_UNIDAD_SIRH, Ordenamiento.ASCENDENTE)
        Return UltimoSortExpression
    End Function

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <param name="pvn_IdPersonal"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionario(pvn_IdPersonal As String) As EntEmpleados
        Dim vlo_WsEU_Curriculo As wsEU_Curriculo

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
    ''' <creationDate>25/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionarioNumEmpleado(pvn_NumEmpleado As String) As EntEmpleados
        Dim vlo_WsEU_Curriculo As wsEU_Curriculo

        vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
        vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_WsEU_Curriculo.Timeout = -1

        Try
            Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("NUM_EMPLEADO = {0}", pvn_NumEmpleado))
        Catch ex As Exception
            Throw
        Finally
            If vlo_WsEU_Curriculo IsNot Nothing Then
                vlo_WsEU_Curriculo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el listado de unidades que estan a cargo del funcionario parametrizado
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>24/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Function ObtenerUnidadesAcargoDeFuncionario(ByVal pvn_NumEmpleado As Integer) As Data.DataSet
        Dim vlo_GestorVacaciones As WsrCatalogosVacaciones.WsCatalogosVacaciones

        Try
            'Inicializa controles
            vlo_GestorVacaciones = New WsrCatalogosVacaciones.WsCatalogosVacaciones
            vlo_GestorVacaciones.Timeout = -1
            vlo_GestorVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials

            Return vlo_GestorVacaciones.PLM_ESTRUCTURA_ORG_ObtenerUbicacionesPorJefesSustitutos(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_NumEmpleado)

        Catch ex As Exception
            Throw
        End Try
    End Function

#End Region

End Class
