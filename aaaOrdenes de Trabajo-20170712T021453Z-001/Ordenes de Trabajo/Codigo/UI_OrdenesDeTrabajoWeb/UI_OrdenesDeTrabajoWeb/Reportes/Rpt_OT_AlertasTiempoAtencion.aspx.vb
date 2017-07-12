Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo


Partial Class Reportes_Rpt_OT_AlertasTiempoAtencion
    Inherits System.Web.UI.Page
#Region "Propiedades"

    ''' <summary>
    ''' Se usa para mostrarle al usuario los filtros que seleccionó
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Property CondicionUsuario() As String
        Get
            Return CType(ViewState("CondicionUsuario"), String)
        End Get
        Set(ByVal value As String)
            ViewState("CondicionUsuario") = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad para el usuario actual en sesion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/12/2015</creationDate>
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
    ''' Propiedad para cargar la ubiación 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property AutorizadoUbicacion As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion
        Get
            Return CType(ViewState("AutorizadoUbicacion"), Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmAutorizadoUbicacion)
            ViewState("AutorizadoUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Parámetro para indicar las órdenes a generar en el reporte, si son de diseño o de mantenimiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property RequiereFichaTecnica As Boolean
        Get
            Return CType(ViewState("RequiereFichaTecnica"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("RequiereFichaTecnica") = value
        End Set
    End Property

    ''' <summary>
    ''' Para filtrar cuando el supervisor tiene tanto categorias de diseño como de mantenimiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>16/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Property DisYMantenimiento As Boolean
        Get
            Return CType(ViewState("RequiereFichaTecnica"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("RequiereFichaTecnica") = value
        End Set
    End Property

#End Region
#Region "Eventos"
    ''' <summary>
    ''' Evento que inicializa las propiedades y componentes de la página 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                Me.AutorizadoUbicacion = CargarAutorizadoUbicacion(Me.Usuario.NumEmpleado)

                If Me.AutorizadoUbicacion.Existe Then

                    InicializarFormulario()

                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('Usted no posee ninguna ubicación autorizada, contactar con el administrador del sistema.','../Genericos/Frm_MenuPrincipal.aspx');")
                End If


            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Cambia la lista de actividades deacuerdo a la categoría seleccionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlFiltroCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFiltroCategoria.SelectedIndexChanged
        Dim vlc_Condicion As String

        Try
            If Me.ddlFiltroCategoria.SelectedValue <> String.Empty Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_CATEGORIA_SERVICIO, Me.ddlFiltroCategoria.SelectedValue)
                CargarActividad(vlc_Condicion)
            End If
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene los parámetros ingresados y crea una condición de búsqueda
    ''' Después manda a llamar al generador de reportes.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        Try
            'Redirecciona a la pantalla del generador de reportes
            Response.Redirect(String.Format("RPT_OT_ReportePDF.aspx?pvc_Condicion={0}&pvc_Orden={1}&pvc_NombreReporte={2}&pvc_CondicionUsuario={3}&pvb_ReqFicha={4}",
                                            ObtenerCondicionBusqueda(), ObtenerOrden(), Reportes.RPT_OT_REPORTE_ALERTAS_TIEMPO_ATENCION, CondicionUsuario, Me.RequiereFichaTecnica))
        Catch ex As Exception

        End Try
    End Sub

#End Region
#Region "Metodos"

    ''' <summary>
    ''' Inicializa el formulario con las categorias, edificios y actividades, ademas deshabilita los campos que no se deben modificar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()

        Try
            CargarComboEstado()
            CargarCategorias(String.Empty)
            CargarEdificios(String.Format("{0} = {1}",
                                          Modelo.OTM_LUGAR_TRABAJO.ID_UBICACION_PERTENECE,
                                          Me.AutorizadoUbicacion.IdUbicacionAdministra))
            CargarActividad(String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_CATEGORIA_SERVICIO, Me.ddlFiltroCategoria.SelectedValue))
            CargarTiposDeOrden()
            CargarTallerSector()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para cargar las categorias con la condición especificada
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarCategorias(pvc_Condicion As String)
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlFiltroCategoria.Items.Clear()
            Me.ddlFiltroCategoria.Items.Add(New ListItem("[Selecccione el Valor]", String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ListarRegistros(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                            pvc_Condicion,
                            String.Empty,
                            False,
                            0,
                            0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlFiltroCategoria

                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_CATEGORIA_SERVICIO.DESCRIPCION
                    .DataValueField = Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO
                    .SelectedIndex = 1
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
    ''' Los estados válidos para éste reporte son asignada y no conforme.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarComboEstado()

        Try
            Me.ddlFiltroEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
            Me.ddlFiltroEstado.Items.Add(New ListItem("Asignada", Utilerias.OrdenesDeTrabajo.EstadoOrden.ASIGNADA))
            Me.ddlFiltroEstado.Items.Add(New ListItem("No Conforme", Utilerias.OrdenesDeTrabajo.EstadoOrden.NO_CONFORME))

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Si el rol del usuario es coordinador sólo se mostrará el taller o sector correspondiente, 
    ''' Si es supervisor se listarán los talleres o sectores ligados a categorías a su cargo además de la opción “Todos”.
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarTallerSector()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet
        Dim vlo_ReqFichaTec() As System.Data.DataRow

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If Roles.IsUserInRole(Membership.GetUser.UserName, Utilerias.OrdenesDeTrabajo.RolesSistema.OT_SUPERVISOR) Then

                Me.ddlFiltroTallerSector.Items.Add(New ListItem("[Selecccione el Valor]", String.Empty))


                'Carga los talleres o sectores donde el usuario tenga categorias a su cargo
                vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ListarVOtTallerCategoriasSup(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0}={1}", Modelo.V_OT_TALLER_CATEGORIAS_SUP.NUM_EMPLEADO_SUPERVISOR, Me.Usuario.NumEmpleado), String.Empty, False, 1, 0)

                'Verifica si este supervisor posee categorias de mantenimiento y de diseño
                Me.DisYMantenimiento = vlo_DsDatos.Tables(0).Select(String.Format("{0}=1", Modelo.V_OT_TALLER_CATEGORIAS_SUP.REQUIERE_FICHA_TECNICA)).Length > 0 And
                    vlo_DsDatos.Tables(0).Select(String.Format("{0}=0", Modelo.V_OT_TALLER_CATEGORIAS_SUP.REQUIERE_FICHA_TECNICA)).Length > 0


            ElseIf Roles.IsUserInRole(Membership.GetUser.UserName, Utilerias.OrdenesDeTrabajo.RolesSistema.OT_COORDINADOR_MANTENIMIENTO) Or Roles.IsUserInRole(Membership.GetUser.UserName, Utilerias.OrdenesDeTrabajo.RolesSistema.OT_COORDINADOR_DISENIO) Then
                'Si el taller o sector del coordinador está asociado a categorias de mantenimiento
                'se obtienen los de mantenimiento solamente, si está asociado a una de diseño
                'Se obtienen los parámetros de diseño.

                vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ListarVOtTallerCategoriasSup(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0}={1}", Modelo.V_OT_TALLER_CATEGORIAS_SUP.NUM_EMPLEADO_COORDINADOR, Me.Usuario.NumEmpleado), String.Empty, False, 1, 0)

                'Revisa si existen registros que requieran ficha técnica
                vlo_ReqFichaTec = vlo_DsDatos.Tables(0).Select(String.Format("{0}=1", Modelo.V_OT_TALLER_CATEGORIAS_SUP.REQUIERE_FICHA_TECNICA))

                Me.RequiereFichaTecnica = vlo_ReqFichaTec.Count > 0
            End If
            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlFiltroTallerSector
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OT_TALLER_CATEGORIAS_SUP.NOMBRE
                    .DataValueField = Modelo.V_OT_TALLER_CATEGORIAS_SUP.ID_SECTOR_TALLER
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
    ''' Carga todos los tipos de orden de trabajo en la lista para filtros
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarTiposDeOrden()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As System.Data.DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlFiltroTipoOrden.Items.Add(New ListItem("[Selecccione el Valor]", String.Empty))


            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTC_TIPO_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Empty,
                String.Empty,
                True,
                1,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlFiltroTipoOrden
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OTC_TIPO_ORDEN_TRABAJO.DESCRIPCION
                    .DataValueField = Modelo.V_OTC_TIPO_ORDEN_TRABAJO.TIPO_ORDEN_TRABAJO
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
    ''' Carga los edificios con la condicion especificada por parametro
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEdificios(pvc_Condicion As String)
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlFiltroLugarTrabajo.Items.Add(New ListItem("[Selecccione el Valor]", String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_LUGAR_TRABAJO_ListarRegistros(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                pvc_Condicion,
                                String.Empty,
                                False,
                                0,
                                0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlFiltroLugarTrabajo
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_LUGAR_TRABAJO.NOMBRE
                    .DataValueField = Modelo.OTM_LUGAR_TRABAJO.ID_LUGAR_TRABAJO
                    .SelectedValue = String.Empty
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
    ''' Carga una lista de actividades con la condición recibida
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarActividad(pvc_Condicion As String)
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlFiltroActividad.Items.Clear()
            Me.ddlFiltroActividad.Items.Add(New ListItem("[Selecccione el Valor]", String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Ws_OT_Catalogos.OTM_ACTIVIDAD_ListarRegistros(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                            pvc_Condicion,
                            String.Empty,
                            False,
                            0,
                            0)

            If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.ddlFiltroActividad
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

#End Region
#Region "Funciones"

    ''' <summary>
    ''' Carga la ubicacion  de un funcionario por numero de empleado
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/12/2015</creationDate>
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
    ''' Genera la condición de búsqueda basado en la pantalla de filtros.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionBusqueda() As String
        Dim vlc_Condicion As String = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroTallerSector.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_REPORTE_GENERAL.ID_SECTOR_TALLER, Me.ddlFiltroTallerSector.SelectedValue)
                Me.CondicionUsuario = String.Format("Taller o Sector: {0}", Me.ddlFiltroTallerSector.SelectedItem.Text)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_REPORTE_GENERAL.ID_SECTOR_TALLER, Me.ddlFiltroTallerSector.SelectedValue)
                Me.CondicionUsuario = String.Format("{0} Taller o Sector: {1}", "\n", Me.ddlFiltroTallerSector.SelectedItem.Text)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroEstado.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OT_REPORTE_GENERAL.ESTADO_ORDEN_TRABAJO, Me.ddlFiltroEstado.SelectedValue)
                Me.CondicionUsuario = String.Format("Estado: {0}", Me.ddlFiltroEstado.SelectedItem.Text)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OT_REPORTE_GENERAL.ESTADO_ORDEN_TRABAJO, Me.ddlFiltroEstado.SelectedValue)
                Me.CondicionUsuario = String.Format("{0}\n Estado: {1}", Me.CondicionUsuario, Me.ddlFiltroEstado.SelectedItem.Text)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroFechaDesde.Text) And Not String.IsNullOrWhiteSpace(Me.txtFiltroFechaHasta.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} BETWEEN TO_DATE('{1}', 'dd/mm/yyyy') AND TO_DATE('{2}', 'dd/mm/yyyy')", Modelo.V_OT_REPORTE_GENERAL.FECHA_DE_ASIGNACION, Me.txtFiltroFechaDesde.Text, Me.txtFiltroFechaHasta.Text)
                Me.CondicionUsuario = String.Format("Fecha Desde: {0}, Fecha Hasta{1}", Me.txtFiltroFechaDesde.Text, Me.txtFiltroFechaHasta.Text)
            Else
                vlc_Condicion = String.Format("{0} AND {1} BETWEEN TO_DATE('{2}', 'dd/mm/yyyy') AND TO_DATE('{3}', 'dd/mm/yyyy')", vlc_Condicion, Modelo.V_OT_REPORTE_GENERAL.FECHA_DE_ASIGNACION, Me.txtFiltroFechaDesde.Text, Me.txtFiltroFechaHasta.Text)
                Me.CondicionUsuario = String.Format("{0}\n Fecha Desde: {1}, Fecha Hasta: {2}", Me.CondicionUsuario, Me.txtFiltroFechaDesde.Text, Me.txtFiltroFechaHasta.Text)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroCategoria.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_REPORTE_GENERAL.ID_CATEGORIA_SERVICIO, Me.ddlFiltroCategoria.SelectedValue)
                Me.CondicionUsuario = String.Format("Categoria de Servicio: {0}", Me.ddlFiltroCategoria.SelectedItem.Text)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_REPORTE_GENERAL.ID_CATEGORIA_SERVICIO, Me.ddlFiltroCategoria.SelectedValue)
                Me.CondicionUsuario = String.Format("{0}\n Categoria de Servicio: {1}", Me.CondicionUsuario, Me.ddlFiltroCategoria.SelectedItem.Text)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroActividad.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_REPORTE_GENERAL.ID_ACTIVIDAD, Me.ddlFiltroActividad.SelectedValue)
                Me.CondicionUsuario = String.Format("Actividad: {0}", Me.ddlFiltroActividad.SelectedItem.Text)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_REPORTE_GENERAL.ID_ACTIVIDAD, Me.ddlFiltroActividad.SelectedValue)
                Me.CondicionUsuario = String.Format("{0}\n Actividad: {1}", Me.CondicionUsuario, Me.ddlFiltroActividad.SelectedItem.Text)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroTipoOrden.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OT_REPORTE_GENERAL.TIPO_ORDEN_TRABAJO, Me.ddlFiltroTipoOrden.SelectedValue)
                Me.CondicionUsuario = String.Format("Tipo de Orden: {0}", Me.ddlFiltroTipoOrden.SelectedItem.Text)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OT_REPORTE_GENERAL.TIPO_ORDEN_TRABAJO, Me.ddlFiltroTipoOrden.SelectedValue)
                Me.CondicionUsuario = String.Format("{0}\n Tipo de Orden: {1}", Me.CondicionUsuario, Me.ddlFiltroTipoOrden.SelectedItem.Text)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroLugarTrabajo.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OT_REPORTE_GENERAL.ID_LUGAR_TRABAJO, Me.ddlFiltroLugarTrabajo.SelectedValue)
                Me.CondicionUsuario = String.Format("Lugar de Trabajo: {0}", Me.ddlFiltroLugarTrabajo.SelectedItem.Text)
            Else
                vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OT_REPORTE_GENERAL.ID_LUGAR_TRABAJO, Me.ddlFiltroLugarTrabajo.SelectedValue)
                Me.CondicionUsuario = String.Format("{0}\n Lugar de Trabajo: {1}", Me.CondicionUsuario, Me.ddlFiltroLugarTrabajo.SelectedItem.Text)
            End If
        End If

        If Me.rbtMantenimiento.Checked Then
            Me.RequiereFichaTecnica = False
        End If

        If Me.rbtDisenio.Checked Then
            Me.RequiereFichaTecnica = True
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OTT_ORDEN_TRABAJOLST.NUM_EMPLEADO, Me.Usuario.NumEmpleado)
        Else
            vlc_Condicion = String.Format("{0} AND {1} = {2}", vlc_Condicion, Modelo.V_OTT_ORDEN_TRABAJOLST.NUM_EMPLEADO, Me.Usuario.NumEmpleado)
        End If

        Return vlc_Condicion
    End Function

    ''' <summary>
    ''' carga parámetros del sistema
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>10/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarParametro(pvn_IdParametro As Integer, pvn_IdUbicacion As Integer) As Wsr_OT_Catalogos.EntOtpParametroUbicacion
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Return vlo_Ws_OT_Catalogos.OTP_PARAMETRO_UBICACION_ObtenerRegistro(
                 ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, pvn_IdParametro, Modelo.OTP_PARAMETRO_UBICACION.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Obtiene el orden del reporte
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerOrden() As String

        If Me.rbtFecha.Checked Then
            Return String.Format("{0} ASC", Modelo.V_OT_REPORTE_GENERAL.FECHA_DE_ASIGNACION)
        ElseIf Me.rbtNumOt.Checked Then
            Return String.Format("{0} ASC", Modelo.V_OT_REPORTE_GENERAL.N_ORDEN_TRABAJO)
        ElseIf Me.rbtEstado.Checked Then
            Return String.Format("{0} ASC", Modelo.V_OT_REPORTE_GENERAL.ESTADO)
        End If

        Return String.Empty

    End Function

#End Region
End Class
