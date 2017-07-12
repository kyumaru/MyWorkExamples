Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos
Imports System.Data

Partial Class Controles_wuc_OT_Materiales
    Inherits System.Web.UI.UserControl


#Region "Propiedades"
    ''' <summary>
    ''' propiedad para la ubicacion autorizada del usuario que desea registrar ordenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/5/2016</creationDate>
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
    ''' Propiedad para el usuario en sesion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/5/2016</creationDate>
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
    ''' Propiedad para mostrar la partida presupuestaria y el almacen bodega
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>01/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property mostrarAlmacenPartida As Boolean
        Get
            Return CType(ViewState("MaxId"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("MaxId") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>01/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Sub Inicializar()
        Inicializar(mostrarAlmacenPartida)
    End Sub

    Private Sub Inicializar(pvb_mostrarCombos As Boolean)
        Try
            Me.trAlmacenBodega.Visible = pvb_mostrarCombos
            trPartidaPresupuestaria.Visible = pvb_mostrarCombos
            trBuscarFiltro.Visible = pvb_mostrarCombos

            Me.Usuario = New UsuarioActual
            AutorizadoUbicacion = CargarAutorizadoUbicacion(Usuario.NumEmpleado)

            '{0}:ID_UBICACION_ADMINISTRA
            '{1}:id de la ubicacion del usuario actual
            '{2}:ESTADO
            '{3}:Solo categorias con estado activo
            CargarCategorias(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTM_CATEGORIA_MATERIAL.ID_UBICACION_ADMINISTRA, AutorizadoUbicacion.IdUbicacionAdministra,
                                           Modelo.OTM_CATEGORIA_MATERIAL.ESTADO, Estado.ACTIVO))

            '{0}:ID_UBICACION_ADMINISTRA
            '{1}:id de la ubicacion del usuario actual
            '{2}:ESTADO
            '{3}:Solo subcategorias con estado activo
            CargarSubCategorias(String.Format("{0} = {1} AND {2} = '{3}'",
                                              Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.UBICACION_SUBCATEG_MATE, AutorizadoUbicacion.IdUbicacionAdministra,
                                              Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.ESTADO_SUBCATEG_MATE, Estado.ACTIVO))

            '{0}:ID_UBICACION_ADMINISTRA
            '{1}:id de la ubicacion del usuario actual
            '{2}:ESTADO
            '{3}:Solo subcategorias con estado activo
            CargarAlmacenesBodegas(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, AutorizadoUbicacion.IdUbicacionAdministra,
                                              Modelo.OTM_ALMACEN_BODEGA.ESTADO, Estado.ACTIVO))

            CargarPartidaPresupuestaria(String.Empty)


            Me.grdEmpleados.PageSize = CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer)

            LimpiarFormulario()


        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>01/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LimpiarFormulario()
        Me.txtCodigo.Text = String.Empty
        Me.txtDescripcion.Text = String.Empty
        Me.ddlAlmacen.SelectedIndex = 0
        Me.ddlPartidaPresupuestaria.SelectedIndex = 0
        Me.ddlCategoria.SelectedIndex = 0
        Me.ddlSubcategoria.SelectedIndex = 0
        Me.grdEmpleados.DataSource = Nothing
        Me.grdEmpleados.DataBind()
    End Sub

    Private Sub FiltrarPartida()
        CargarPartidaPresupuestaria(Me.txtFiltroPart.Text)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvc_CondicionBusquedas"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>01/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BuscarMateriales(pvc_CondicionBusquedas As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            If String.IsNullOrWhiteSpace(pvc_CondicionBusquedas) Then
                ScriptManager.RegisterStartupScript(Me, GetType(String), "MensajeRetorno", "javascript:alert('Debe indicar algún criterio de búsqueda.');", True)
            Else
                vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ListarRegistrosListaINVENTARIO(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    pvc_CondicionBusquedas, String.Empty, False, 0,
                    CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

                If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    With Me.grdEmpleados
                        .DataSource = vlo_DsDatos
                        .DataMember = vlo_DsDatos.Tables(0).TableName
                        .DataBind()
                    End With
                Else
                    grdEmpleados.DataSource = Nothing
                    grdEmpleados.DataBind()
                End If
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscar.Click
        Try
            BuscarMateriales(ObtenerCondicionDeBusqueda)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub ddlCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategoria.SelectedIndexChanged

        Try

            If Me.ddlCategoria.SelectedValue <> String.Empty Then
                CargarSubCategorias(String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}",
                                              Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.UBICACION_SUBCATEG_MATE, AutorizadoUbicacion.IdUbicacionAdministra,
                                              Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.ESTADO_SUBCATEG_MATE, Estado.ACTIVO,
                                              Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.ID_CATEGORIA_MATERIAL, Me.ddlCategoria.SelectedValue))
            Else
                CargarSubCategorias(String.Format("{0} = {1} AND {2} = '{3}'",
                                              Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.UBICACION_SUBCATEG_MATE, AutorizadoUbicacion.IdUbicacionAdministra,
                                              Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.ESTADO_SUBCATEG_MATE, Estado.ACTIVO))
            End If

        Catch ex As Exception
            Throw
        End Try
        
    End Sub

    Private Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLimpiar.Click
        Try
            LimpiarFormulario()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnBuscarPart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarPart.Click
        Try
            FiltrarPartida()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnLimpiarFiltroPart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLimpiarFiltroPart.Click
        Try
            Me.txtFiltroPart.Text = String.Empty
            FiltrarPartida()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub lnkGrid_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Dim vlc_Llaves() As String
        Try
            'limpiar formulario
            LimpiarFormulario()

            'leer command argument
            vlc_Llaves = e.CommandArgument.ToString.Split("_")

            RaiseEvent Aceptar(CType(vlc_Llaves(0), Integer), CType(vlc_Llaves(1), String), CType(vlc_Llaves(2), Integer), CType(vlc_Llaves(3), Integer), CType(vlc_Llaves(4), Integer), CType(vlc_Llaves(5), Integer))
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Carga las unidades de medida deacuerdo a la condición indicada
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>2/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarPartidaPresupuestaria(pvc_Nombre As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_dsDatos As Data.DataSet
        Dim vlo_PeriodoActual As Integer

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_PeriodoActual = Date.Now.Year

            Me.ddlPartidaPresupuestaria.Items.Clear()


            Me.ddlPartidaPresupuestaria.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_dsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarVOttPartidasPresup(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} LIKE '%{3}%'",
                              Modelo.V_OTT_PARTIDAS_PRESUP.NUM_PERIODO, vlo_PeriodoActual,
                              Modelo.V_OTT_PARTIDAS_PRESUP.NOM_EGRESO, pvc_Nombre.ToUpper()), String.Empty, False, 0, 0)

            If vlo_dsDatos.Tables.Count > 0 AndAlso vlo_dsDatos.Tables(0).Rows.Count <= 0 Then

                vlo_PeriodoActual = vlo_PeriodoActual - 1
                'Se cargan los del periodo anterior en caso de estar vacío
                vlo_dsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarVOttPartidasPresup(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.V_OTT_PARTIDAS_PRESUP.NUM_PERIODO, vlo_PeriodoActual), String.Empty, False, 0, 0)
            End If

            For Each vlo_fila As Data.DataRow In vlo_dsDatos.Tables(0).Rows
                Me.ddlPartidaPresupuestaria.Items.Add(New ListItem(String.Format("({0}) {1}", vlo_fila(Modelo.V_OTT_PARTIDAS_PRESUP.ID_EGRESO), vlo_fila(Modelo.V_OTT_PARTIDAS_PRESUP.NOM_EGRESO)), vlo_fila(Modelo.V_OTT_PARTIDAS_PRESUP.ID_EGRESO)))
            Next


        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

    Public Event Aceptar(pvc_IdMaterial As Integer, pvc_Ubicacion As String, pvn_IdCategoria As Integer, pvn_idSubcategoria As Integer, pvn_CostoPromedio As Integer, pvn_UnidadMedida As Integer)
    Dim vlb_Consultar As Boolean = False

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

    ''' <summary>
    ''' Carga las categorias deacuerdo a la condición indicada
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarCategorias(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_dsDatos As Data.DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlCategoria.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_dsDatos = vlo_Ws_OT_Catalogos.OTM_CATEGORIA_MATERIAL_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, String.Empty, False, 0, 0)

            For Each vlo_fila As Data.DataRow In vlo_dsDatos.Tables(0).Rows
                Me.ddlCategoria.Items.Add(New ListItem(vlo_fila(Modelo.OTM_CATEGORIA_MATERIAL.DESCRIPCION), vlo_fila(Modelo.OTM_CATEGORIA_MATERIAL.ID_CATEGORIA_MATERIAL)))
            Next

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
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>02/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarAlmacenesBodegas(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_dsDatos As Data.DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlAlmacen.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_dsDatos = vlo_Ws_OT_Catalogos.OTM_ALMACEN_BODEGA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, String.Empty, False, 0, 0)

            For Each vlo_fila As Data.DataRow In vlo_dsDatos.Tables(0).Rows
                Me.ddlAlmacen.Items.Add(New ListItem(vlo_fila(Modelo.OTM_ALMACEN_BODEGA.DESCRIPCION), vlo_fila(Modelo.OTM_ALMACEN_BODEGA.ID_ALMACEN_BODEGA)))
            Next


        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Carga las Subcategorias deacuerdo a la condición indicada
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarSubCategorias(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_dsDatos As Data.DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlSubcategoria.Items.Clear()


            Me.ddlSubcategoria.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_dsDatos = vlo_Ws_OT_Catalogos.OTM_SUBCATEGORIA_CATEGOR_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, String.Empty, False, 0, 0)

            With Me.ddlSubcategoria
                .DataSource = vlo_dsDatos
                .DataMember = vlo_dsDatos.Tables(0).TableName
                .DataTextField = Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.DESCRIPCION_SUBCATEG_MATE
                .DataValueField = Modelo.V_OTM_SUBCATEGORIA_CATEGORLST.ID_SUBCATEGORIA_MATERIAL
                .DataBind()
            End With

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Funcion que retorna la condicion de busqueda actual
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>1/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionDeBusqueda() As String
        Dim vlc_Condicion As String = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtCodigo.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor de búsqueda
                vlc_Condicion = String.Format("{0} = {1}",
                                            Modelo.V_OTM_MATERIALLST.ID_MATERIAL,
                                            Me.txtCodigo.Text.Trim.ToUpper)
            Else
                '{0} = Valor original de vlc_Condicion
                '{1} = Nombre de la columna
                '{2} = Valor a buscar
                vlc_Condicion = String.Format("{0} AND {1} = {2}",
                                            vlc_Condicion,
                                            Modelo.V_OTM_MATERIALLST.ID_MATERIAL,
                                            Me.txtCodigo.Text.Trim.ToUpper)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtDescripcion.Text) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor de búsqueda
                vlc_Condicion = String.Format("{0} LIKE '%{1}%'",
                                            Modelo.V_OTM_MATERIALLST.DESCRIPCION,
                                            Me.txtDescripcion.Text.Trim.ToUpper)
            Else
                '{0} = Valor original de vlc_Condicion
                '{1} = Nombre de la columna
                '{2} = Valor a buscar
                vlc_Condicion = String.Format("{0} AND {1} LIKE '%{2}%'",
                                            vlc_Condicion,
                                            Modelo.V_OTM_MATERIALLST.DESCRIPCION,
                                            Me.txtDescripcion.Text.Trim.ToUpper)
            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlCategoria.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor a buscar
                vlc_Condicion = String.Format("{0} = '{1}'",
                                            Modelo.V_OTM_MATERIALLST.ID_CATEGORIA_MATERIAL,
                                            Me.ddlCategoria.SelectedValue)
            Else
                '{0} = Contenido de vlc_Condicion
                '{1} = Nombre de la columna a filtrar
                '{2} = valor de Búsqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'",
                                            vlc_Condicion,
                                            Modelo.V_OTM_MATERIALLST.ID_CATEGORIA_MATERIAL,
                                            Me.ddlCategoria.SelectedValue)

            End If
        End If

        If Not String.IsNullOrWhiteSpace(Me.ddlSubcategoria.SelectedValue) Then
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                '{0} = Nombre de la columna
                '{1} = Valor a buscar
                vlc_Condicion = String.Format("{0} = '{1}'",
                                            Modelo.V_OTM_MATERIALLST.ID_SUBCATEGORIA_MATERIAL,
                                            Me.ddlSubcategoria.SelectedValue)
            Else
                '{0} = Contenido de vlc_Condicion
                '{1} = Nombre de la columna a filtrar
                '{2} = valor de Búsqueda
                vlc_Condicion = String.Format("{0} AND {1} = '{2}'",
                                            vlc_Condicion,
                                            Modelo.V_OTM_MATERIALLST.ID_SUBCATEGORIA_MATERIAL,
                                            Me.ddlSubcategoria.SelectedValue)

            End If
        End If

        If mostrarAlmacenPartida Then
            If Not String.IsNullOrWhiteSpace(Me.ddlAlmacen.SelectedValue) Then
                If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                    '{0} = Nombre de la columna
                    '{1} = Valor a buscar
                    vlc_Condicion = String.Format("{0} = '{1}'",
                                                Modelo.V_OTM_MATERIALLST.ID_ALMACEN_BODEGA,
                                                Me.ddlAlmacen.SelectedValue)
                Else
                    '{0} = Contenido de vlc_Condicion
                    '{1} = Nombre de la columna a filtrar
                    '{2} = valor de Búsqueda
                    vlc_Condicion = String.Format("{0} AND {1} = '{2}'",
                                                vlc_Condicion,
                                                Modelo.V_OTM_MATERIALLST.ID_ALMACEN_BODEGA,
                                                Me.ddlAlmacen.SelectedValue)

                End If
            End If

            If Not String.IsNullOrWhiteSpace(Me.ddlPartidaPresupuestaria.SelectedValue) Then
                If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                    '{0} = Nombre de la columna
                    '{1} = Valor a buscar
                    vlc_Condicion = String.Format("{0} = '{1}'",
                                                Modelo.V_OTM_MATERIALLST.PARTIDA_PRESUPUESTARIA,
                                                Me.ddlPartidaPresupuestaria.SelectedValue)
                Else
                    '{0} = Contenido de vlc_Condicion
                    '{1} = Nombre de la columna a filtrar
                    '{2} = valor de Búsqueda
                    vlc_Condicion = String.Format("{0} AND {1} = '{2}'",
                                                vlc_Condicion,
                                                Modelo.V_OTM_MATERIALLST.PARTIDA_PRESUPUESTARIA,
                                                Me.ddlPartidaPresupuestaria.SelectedValue)

                End If
            End If
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            vlc_Condicion = String.Format("{0} = '{1}'",
                                        Modelo.V_OTM_MATERIALLST.ESTADO,
                                        Estado.ACTIVO)
        Else
            vlc_Condicion = String.Format("{0} AND {1} = '{2}'",
                                        vlc_Condicion,
                                        Modelo.V_OTM_MATERIALLST.ESTADO,
                                        Estado.ACTIVO)
        End If

        If String.IsNullOrWhiteSpace(vlc_Condicion) Then
            MostrarAlertaError("Debe indicar al menos un dato")
        End If

        Return vlc_Condicion

    End Function

    ''' <summary>
    ''' Carga una entidad de tipo autorizado ubicacion
    ''' </summary>
    ''' <param name="pvn_NumEmpleado"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
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

#End Region

End Class
