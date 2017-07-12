﻿Imports System.Data  'para utilizar data set
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo
Imports Wsr_OT_Catalogos

Partial Class Catalogos_Lst_OT_PersonalSectorTaller
    Inherits System.Web.UI.Page
#Region "Propiedades"

    ''' <summary>
    ''' Ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortExpression As String
        Get
            If ViewState("UltimoSortExpression") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortExpression"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortExpression") = value
        End Set
    End Property

    ''' <summary>
    ''' Ultima columna de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortColumn As String
        Get
            If ViewState("UltimoSortColumn") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return String.Empty
            End If
            Return CType(ViewState("UltimoSortColumn"), String)
        End Get
        Set(value As String)
            ViewState("UltimoSortColumn") = value
        End Set
    End Property

    ''' <summary>
    ''' Ultima direccion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property UltimoSortDirection As SortDirection
        Get
            If ViewState("UltimoSortDirection") Is Nothing Then 'maneja estado de los controles entre el cliente y el servidor, se maneja en memoria del cliente
                Return SortDirection.Ascending
            End If
            Return CType(ViewState("UltimoSortDirection"), SortDirection)
        End Get
        Set(value As SortDirection)
            ViewState("UltimoSortDirection") = value
        End Set
    End Property

    Private Property Condicion As String
        Get
            If ViewState("Condicion") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("Condicion"), String)
        End Get
        Set(value As String)
            ViewState("Condicion") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>27/01/2016</creationDate>
    Private Property IdSectorTaller As String
        Get
            If ViewState("IdSectorTaller") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdSectorTaller"), String)
        End Get
        Set(value As String)
            ViewState("IdSectorTaller") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la identificación del coordinador de este sector o taller
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/02/2016</creationDate>
    Private Property CedulaCoordinador As String
        Get
            If ViewState("CedulaCoordinador") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("CedulaCoordinador"), String)
        End Get
        Set(value As String)
            ViewState("CedulaCoordinador") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la identificación del coordinador sustituto, si existe.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>17/02/2016</creationDate>
    Private Property CedulaSustituto As String
        Get
            If ViewState("CedulaSustituto") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("CedulaSustituto"), String)
        End Get
        Set(value As String)
            ViewState("CedulaSustituto") = value
        End Set
    End Property

#End Region

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


        If Not IsPostBack Then
            Inicializar()
            Buscar(ObtenerCondicionDeBusqueda, String.Empty)
        End If

        Me.pnRpPersonas.Dibujar()
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try
            Buscar(ObtenerCondicionDeBusqueda, String.Empty)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    Protected Sub rpPersona_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpPersona.ItemDataBound
        Dim vlo_IbBorrar As ImageButton

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            vlo_IbBorrar = e.Item.FindControl("ibBorrar")
            If vlo_IbBorrar IsNot Nothing Then
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub

    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            If Borrar(CType(sender, ImageButton).CommandArgument) Then
                Buscar(ObtenerCondicionDeBusqueda, UltimoSortExpression)
                MostrarAlertaRegistroBorrado()

            Else
                MostrarAlertaRegistroNoBorrado()
            End If
        Catch ex As Exception
            If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                Dim vlo_OrdenesDeTrabajoException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                MostrarAlertaError(vlo_OrdenesDeTrabajoException.Message)
                WebUtils.RegistrarScript(Me, "OcultarAreaFiltrosDeBusqueda", "ocultarAreaFiltrosDeBusqueda();")

            Else
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End If

        End Try
    End Sub

    Protected Sub pnRpPersonas_CambioDePagina(pvn_PaginaSeleccionada As Integer) Handles pnRpPersonas.CambioDePagina
        Try
            CargarLista(ObtenerCondicionDeBusqueda, UltimoSortExpression, pvn_PaginaSeleccionada)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Metodos"

    Private Sub MostrarAlertaNoHayDatos()
        WebUtils.RegistrarScript(Me, "alertaNoHayDatos", "mostrarAlertaNoHayDatos();")
    End Sub

    Private Sub MostrarAlertaRegistroBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroBorrado", "mostrarAlertaRegistroBorrado();")
    End Sub

    Private Sub MostrarAlertaRegistroNoBorrado()
        WebUtils.RegistrarScript(Me, "alertaRegistroNoBorrado", "mostrarAlertaRegistroNoBorrado();")
    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub CargarEstado()
        Me.ddlFiltroEstado.Items.Clear()
        Me.ddlFiltroEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))
        Me.ddlFiltroEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlFiltroEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
    End Sub

    Private Sub CargarArea()
        Me.ddlFiltroArea.Items.Clear()
        Me.ddlFiltroArea.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODAS, String.Empty))
        Me.ddlFiltroArea.Items.Add(New ListItem("Operario", Area.OPERARIO))
        Me.ddlFiltroArea.Items.Add(New ListItem("Profesional", Area.PROFESIONAL))
    End Sub

    Private Sub Inicializar()

        Me.IdSectorTaller = WebUtils.LeerParametro(Of String)("pvc_IdSectorTaller")
        Me.Session.Add("pvc_IdSectorTaller", IdSectorTaller)
        Me.Condicion = String.Format("{0} = {1}", Modelo.V_OTF_OPERARIO.ID_SECTOR_TALLER, IdSectorTaller)
        CargarEstado()
        CargarArea()
        CargarSector()
    End Sub

    Private Sub CargarSector()
        Dim vlo_DsSector As System.Data.DataSet
        Dim vlo_Wsr_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        Try
            vlo_Wsr_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
            vlo_Wsr_OT_Catalogos.Timeout = -1
            vlo_Wsr_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials


            vlo_DsSector = vlo_Wsr_OT_Catalogos.OTM_SECTOR_TALLER_ListarRegistrosLista(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            Me.Condicion,
            String.Empty,
            False,
            0,
            0)


            If vlo_DsSector.Tables(0) IsNot Nothing AndAlso vlo_DsSector.Tables(0).Rows.Count > 0 Then
                Me.lblSector.Text = vlo_DsSector.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE)
                Me.lblCoordinador.Text = vlo_DsSector.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE_COORDINADOR)
                Me.CedulaCoordinador = vlo_DsSector.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.CEDULA_COORDINADOR)
                If Not IsDBNull(vlo_DsSector.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.CEDULA_SUSTITUTO)) Then
                    Me.CedulaSustituto = vlo_DsSector.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.CEDULA_SUSTITUTO)
                Else
                    Me.CedulaSustituto = String.Empty
                End If
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

    ''' <summary>
    ''' Carga la lista del catalogo
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    Private Sub CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTF_OPERARIO_AREA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))


            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                With Me.rpPersona
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataBind()

                End With
                WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
            Else
                With Me.rpPersona
                    .DataSource = Nothing
                    .DataBind()
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
    ''' Metodo para buscar con una condicion enviada por parametro
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntDatosPaginacion As Wsr_OT_Catalogos.EntDatosPaginacion

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerSortExpression(Modelo.V_OTF_OPERARIOLST.NOMBRE_EMPLEADO)
        End If

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EntDatosPaginacion = vlo_Ws_OT_Catalogos.OTF_OPERARIO_AREA_ObtenerDatosPaginacionVOtfOperarioArealst(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer)
                )

            If vlo_EntDatosPaginacion.TotalRegistros > 0 Then
                Me.pnRpPersonas.TotalPaginasLista = vlo_EntDatosPaginacion.TotalPaginas
                Me.pnRpPersonas.Dibujar()
                Me.rpPersona.Visible = True
                Me.pnRpPersonas.Visible = True
                lblCantidadRegistro.Visible = True
                lblCantidadRegistro.Text = String.Format("Cantidad de Personas: {0}", vlo_EntDatosPaginacion.TotalRegistros)
                CargarLista(pvc_Condicion, pvc_Orden, 1) '1 xq la lista siempre carga en esa posicion
            Else
                Me.lblCantidadRegistro.Visible = False
                Me.lblCantidadRegistro.Text = String.Empty
                Me.rpPersona.Visible = False
                Me.pnRpPersonas.Visible = False
                MostrarAlertaNoHayDatos()

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
    ''' Des asigna el Role de autorizado por director
    ''' </summary>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>02/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub QuitarRol(ByVal pvc_NumIdentificacion As String)
        Try
            Dim vlc_RoleName As String
            vlc_RoleName = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ROL_OT_PROFESIONAL_DISENIO)
            Roles.RemoveUserFromRole(pvc_NumIdentificacion.Trim, vlc_RoleName)
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion para eliminar un registro
    ''' </summary>
    ''' <param name="pvc_Id_Actividad"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Borrar(pvc_Id_Actividad As String) As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtfOperarioarea As EntOtfOperarioArea
        Dim vlo_Clave() As Object


        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_EntOtfOperarioarea = New EntOtfOperarioArea
        Try
            vlo_Clave = pvc_Id_Actividad.Split("_")

            vlo_EntOtfOperarioarea.IdSectorTaller = vlo_Clave(0)
            vlo_EntOtfOperarioarea.NumEmpleado = vlo_Clave(1)

            'Quita el rol al usuario ya que será eliminado
            QuitarRol(vlo_Clave(2))

            Return vlo_Ws_OT_Catalogos.OTF_OPERARIO_AREA_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtfOperarioarea) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()

            End If
        End Try
    End Function

    ''' <summary>
    ''' Obtener condicion de busqueda para enviar al metodo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ObtenerCondicionDeBusqueda() As String
        Dim vlc_Condicion As String = Me.Condicion

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroIdentificacion.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0}: Nombre de la columna
                '{1}: Valor del filtro
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTF_OPERARIO_AREALST.CEDULA, Me.txtFiltroIdentificacion.Text.Trim)
            Else
                '{0}: Valor de la condicion
                '{1}: Nombre de la columna
                '{2}: Valor del filtro identificación
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTF_OPERARIO_AREALST.CEDULA, Me.txtFiltroIdentificacion.Text.Trim)

            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtFiltroNombre.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0}: Nombre de la columna
                '{1}: Valor del filtro
                vlc_Condicion = String.Format("UPPER({0}) LIKE '%{1}%'", Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO, Me.txtFiltroNombre.Text.Trim.ToUpper)
            Else
                '{0}: Valor de la condicion
                '{1}: Nombre de la columna
                '{2}: Valor del filtro nombre
                vlc_Condicion = String.Format("{0} AND UPPER({1}) LIKE '%{2}%'", vlc_Condicion, Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO, Me.txtFiltroNombre.Text.Trim.ToUpper)

            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroArea.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0}: Nombre de la columna
                '{1}: Valor del filtro
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTF_OPERARIO_AREALST.CATEGORIA, Me.ddlFiltroArea.SelectedValue)
            Else
                '{0}: Valor de la condicion
                '{1}: Nombre de la columna
                '{2}: Valor del filtro Area
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTF_OPERARIO_AREALST.CATEGORIA, Me.ddlFiltroArea.SelectedValue)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlFiltroEstado.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0}: Nombre de la columna
                '{1}: Valor del filtro
                vlc_Condicion = String.Format("{0} = '{1}'", Modelo.V_OTF_OPERARIO_AREALST.ESTADO, Me.ddlFiltroEstado.SelectedValue)
            Else
                '{0}: Valor de la condicion
                '{1}: Nombre de la columna
                '{2}: Valor del filtro estado
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'", vlc_Condicion, Modelo.V_OTF_OPERARIO_AREALST.ESTADO, Me.ddlFiltroEstado.SelectedValue)
            End If
        End If

        Return vlc_Condicion

    End Function

    ''' <summary>
    ''' Ordenar ascendente o descendente la lista
    ''' </summary>
    ''' <param name="pvc_NombreColumna"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ObtenerSortExpression(pvc_NombreColumna As String) As String
        If String.IsNullOrWhiteSpace(UltimoSortColumn) OrElse pvc_NombreColumna.CompareTo(UltimoSortColumn) <> 0 Then
            UltimoSortColumn = pvc_NombreColumna
            UltimoSortDirection = SortDirection.Ascending
        Else
            If UltimoSortDirection = SortDirection.Ascending Then
                UltimoSortDirection = SortDirection.Descending
            Else
                UltimoSortDirection = SortDirection.Ascending

            End If
        End If
        '0 nombre de la columna y 1 direccion de ordenamiento
        UltimoSortExpression = String.Format("{0} {1}", UltimoSortColumn, IIf(UltimoSortDirection = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))
        Return UltimoSortExpression
    End Function




#End Region
End Class
