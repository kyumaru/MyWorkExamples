
Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_OrdenesDeTrabajo

Partial Class Catalogos_Frm_OT_Materiales
    Inherits System.Web.UI.Page

#Region "Propiedades"
    ''' <summary>
    ''' Almacena la operación que el usuario desea efectuar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    Private Property operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el valor actual del objeto a insertar/modificar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Material As EntOtmMaterial
        Get
            Return CType(ViewState("Material"), EntOtmMaterial)
        End Get
        Set(value As EntOtmMaterial)
            ViewState("Material") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para la ubicacion autorizada del usuario que desea registrar materiales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Ubicacion As String
        Get
            Return CType(ViewState("Ubicacion"), String)
        End Get
        Set(value As String)
            ViewState("Ubicacion") = value
        End Set
    End Property

    Private Property FiltroBusqueda As String
        Get
            Return CType(ViewState("FiltroBusqueda"), String)
        End Get
        Set(value As String)
            ViewState("FiltroBusqueda") = value
        End Set
    End Property


    ''' <summary>
    ''' propiedad para mostrar/almacenar la cantidad en existencia proveniente desde el listado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property CantidadExistencia As String
        Get
            Return CType(ViewState("CantidadExistencia"), String)
        End Get
        Set(value As String)
            ViewState("CantidadExistencia") = value
        End Set
    End Property


    ''' <summary>
    ''' propiedad para mostrar/guardar el costo promedio proveniente desde el listado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property CostoPromedio As String
        Get
            Return CType(ViewState("CostoPromedio"), String)
        End Get
        Set(value As String)
            ViewState("CostoPromedio") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en sesion 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
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
    ''' propiedad para guardar las partidas presupuestarias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>07/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property PartidasPresupuestarias As Data.DataSet
        Get
            Return CType(ViewState("PartidasPresupuestarias"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("PartidasPresupuestarias") = value
        End Set
    End Property

#End Region

#Region "Eventos"
    ''' <summary>
    ''' Inicializa los componentes del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub txtPartida_TextChanged(sender As Object, e As EventArgs) Handles txtPartida.TextChanged
        WebUtils.RegistrarScript(Me.Page, "cargarLupa", "cargarLupa();")
    End Sub

    ''' <summary>
    ''' Este método se ejecutará al presionar el botón aceptar, dependiendo de la acción modificará o agregará y al final presentará un mensaje
    ''' Si la operación fue exitosa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try
                Select Case Me.operacion
                    Case Is = eOperacion.Agregar

                        If Agregar() Then
                            WebUtils.RegistrarScript(Me, "RegistroExitoso", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("La Descripción o sufijo ingresados ya existen.")
                        End If
                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me, "ActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No ha sido posible actualizar la información del material")
                        End If

                End Select
            Catch ex As Exception
                If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso
                CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                    Dim vlo_TallerCapacitacionException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                    MostrarAlertaError(vlo_TallerCapacitacionException.Message)
                    WebUtils.RegistrarScript(Me, "OcultarAreaDeFiltros", "ocultarAreaFiltrosDeBusqueda();")
                Else
                    Dim vlo_ControlDeErrores As New ControlDeErrores
                    vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
                End If
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta al cambiar el combo de ambitos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategoria.SelectedIndexChanged
        Try
            '{0}:ID_UBICACION_ADMINISTRA
            '{1}:id de la ubicacion del usuario actual
            '{2}:ESTADO
            '{3}:Solo subcategorias con estado activo
            CargarSubCategorias(String.Format("{0} = {1}", Modelo.OTM_SUBCATEGORIA_CATEGOR.ID_CATEGORIA_MATERIAL, ddlCategoria.SelectedValue))

            upSubcategoria.Update()
        Catch ex As Exception
            If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso
                CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                Dim vlo_TallerCapacitacionException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                MostrarAlertaError(vlo_TallerCapacitacionException.Message)
                WebUtils.RegistrarScript(Me, "OcultarAreaDeFiltros", "ocultarAreaFiltrosDeBusqueda();")
            Else
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' muestra el popup de unidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>07/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkEjecutarBusquedaPartida_Click(sender As Object, e As EventArgs) Handles lnkEjecutarBusquedaPartida.Click
        Try
            WebUtils.RegistrarScript(Me.Page, "mostrarPopUpBusquedaPartida", "javascript:mostrarPopUp('#PopUpBusquedaPartida');inicializarScript();")
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
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>07/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkGrid_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
        Dim vlc_partidas() As String

        Try
            vlc_partidas = e.CommandArgument.split("_")
            LimpiarFormulario()
            CargarDatosPartidaPresupuestaria(vlc_partidas(0), vlc_partidas(1))
            WebUtils.RegistrarScript(Me.Page, "Recargar", "inicializarScript();mostrarPopUp('#CerrarPopUpBusquedaPartida');")
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
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>07/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ibBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibBuscar.Click
        Try
            BuscarPartidas(ObtenerCondicionDeBusqueda)
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
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>07/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ibLimpiar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibLimpiar.Click
        LimpiarFormulario()
        WebUtils.RegistrarScript(Me.Page, "Recargar", "inicializarScript();")
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inicializa el formulario dependiendo de la operación recibida por parámetro
    ''' Tambien carga el drop down list de estados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Try

            Me.Usuario = New UsuarioActual
            Me.Material = New EntOtmMaterial
            AutorizadoUbicacion = CargarAutorizadoUbicacion(Usuario.NumEmpleado)
            Me.operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
            Me.FiltroBusqueda = WebUtils.LeerParametro(Of String)("pvc_FiltroBusquedaForm")
            Me.lblCodigo.Text = RetornaMaximoIdMaterial()

            Me.Session.Add("pvc_FiltroBusquedaForm", Me.FiltroBusqueda)

            '{0}:ID_UBICACION_ADMINISTRA
            '{1}:id de la ubicacion del usuario actual
            '{2}:ESTADO
            '{3}:Solo categorias con estado activo
            CargarCategorias(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTM_CATEGORIA_MATERIAL.ID_UBICACION_ADMINISTRA, AutorizadoUbicacion.IdUbicacionAdministra,
                                           Modelo.OTM_CATEGORIA_MATERIAL.ESTADO, Estado.ACTIVO))

            CargarUnidadMedida(String.Format("{0} = '{1}'", Modelo.OTM_UNIDAD_MEDIDA.ESTADO, Estado.ACTIVO))

            CargarPartidaPresupuestaria()

            CargarEstado()
            CargarClasificacion()



            Select Case Me.operacion
                Case Is = eOperacion.Agregar
                    Me.lblAccion.Text = "Agregar Material"
                    Me.ddlEstado.Enabled = False
                    Me.trEstado.Visible = False
                    Me.trCantidadExistencia.Visible = False
                    Me.trCostoPromedio.Visible = False
                Case Is = eOperacion.Modificar
                    Me.lblAccion.Text = "Modificar Material"
                    Me.ddlEstado.Enabled = True
                    Me.CostoPromedio = WebUtils.LeerParametro(Of String)("pvc_CostoPromedio")
                    Me.CantidadExistencia = WebUtils.LeerParametro(Of String)("pvc_Cantidad")

                    Try

                        CargarMaterial(WebUtils.LeerParametro(Of Integer)("pvc_IdMaterial"))

                        '{0}:ID_UBICACION_ADMINISTRA
                        '{1}:id de la ubicacion del usuario actual
                        '{2}:ESTADO
                        '{3}:Solo subcategorias con estado activo
                        CargarSubCategorias(String.Format("{0} = {1}", Modelo.OTM_SUBCATEGORIA_CATEGOR.ID_CATEGORIA_MATERIAL, Me.Material.IdCategoriaMaterial))

                    Catch ex As Exception
                        Throw
                    End Try

            End Select
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' limpia los campos de búsqueda y datos del popup
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>07/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LimpiarFormulario()
        Me.txtCodigoPopup.Text = String.Empty
        Me.txtDescripcionPopup.Text = String.Empty
        Me.grdPartidas.DataSource = Nothing
        Me.grdPartidas.DataBind()
        Me.txtDescripcionPopup.Focus()
    End Sub

    ''' <summary>
    ''' Carga la lista de estados admisibles
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEstado()
        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
    End Sub

    ''' <summary>
    ''' Carga la lista de ambito
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarClasificacion()
        Try
            Me.ddlClasificacion.Items.Clear()
            Me.ddlClasificacion.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
            Me.ddlClasificacion.Items.Add(New ListItem("A - Alta Rotación", ClasificacionMateriales.ALTA_ROTACION))
            Me.ddlClasificacion.Items.Add(New ListItem("B - Baja Rotación", ClasificacionMateriales.BAJA_ROTACION))
            Me.ddlClasificacion.Items.Add(New ListItem("C - Segunda", ClasificacionMateriales.SEGUNDA))
        Catch ex As Exception
            Throw
        Finally
        End Try
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
                pvc_Condicion, String.Format("{0} {1}", Modelo.OTM_CATEGORIA_MATERIAL.DESCRIPCION, Ordenamiento.ASCENDENTE), False, 0, 0)

            With Me.ddlCategoria
                .DataSource = vlo_dsDatos
                .DataMember = vlo_dsDatos.Tables(0).TableName
                .DataTextField = Modelo.OTM_CATEGORIA_MATERIAL.DESCRIPCION
                .DataValueField = Modelo.OTM_CATEGORIA_MATERIAL.ID_CATEGORIA_MATERIAL
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
            Me.ddlSubCategoria.Items.Clear()
            Me.ddlSubCategoria.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_dsDatos = vlo_Ws_OT_Catalogos.OTM_SUBCATEGORIA_CATEGOR_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, String.Empty, False, 0, 0)

            With Me.ddlSubCategoria
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

    ''' <summary>
    ''' Carga las unidades de medida deacuerdo a la condición indicada
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarUnidadMedida(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_dsDatos As Data.DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlUnidadMedida.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_dsDatos = vlo_Ws_OT_Catalogos.OTM_UNIDAD_MEDIDA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, String.Format("{0} {1}", Modelo.OTM_UNIDAD_MEDIDA.DESCRIPCION, Ordenamiento.ASCENDENTE), False, 0, 0)

            For Each vlo_fila As Data.DataRow In vlo_dsDatos.Tables(0).Rows
                Me.ddlUnidadMedida.Items.Add(New ListItem(String.Format("{0} ({1})", vlo_fila(Modelo.OTM_UNIDAD_MEDIDA.DESCRIPCION), vlo_fila(Modelo.OTM_UNIDAD_MEDIDA.ACRONIMO)), vlo_fila(Modelo.OTM_UNIDAD_MEDIDA.ID_UNIDAD_MEDIDA)))
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
    ''' Carga las unidades de medida deacuerdo a la condición indicada
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarPartidaPresupuestaria()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_PeriodoActual As Integer

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_PeriodoActual = Date.Now.Year

            PartidasPresupuestarias = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarVOttPartidasPresup(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.V_OTT_PARTIDAS_PRESUP.NUM_PERIODO, vlo_PeriodoActual), String.Empty, False, 0, 0)

            If PartidasPresupuestarias.Tables.Count > 0 AndAlso PartidasPresupuestarias.Tables(0).Rows.Count <= 0 Then

                vlo_PeriodoActual = vlo_PeriodoActual - 1
                'Se cargan los del periodo anterior en caso de estar vacío
                PartidasPresupuestarias = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarVOttPartidasPresup(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.V_OTT_PARTIDAS_PRESUP.NUM_PERIODO, vlo_PeriodoActual), String.Empty, False, 0, 0)
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
    ''' Obtiene el registro desde la base de datos y lo carga en memoria
    ''' </summary>
    ''' <param name="pvc_IdMaterial"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarMaterial(pvc_IdMaterial As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_UbicacionFisica As String()

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0} Nombre de la columna 
            '{1} numero de ID_VIA_CONTRATO

            Me.Material = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0}={1}", Modelo.OTM_MATERIAL.ID_MATERIAL, pvc_IdMaterial))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
        vlo_UbicacionFisica = Me.Material.UbicacionFisica.Split("-")

        If Me.Material.Existe Then
            With Me.Material
                Me.lblCodigo.Text = pvc_IdMaterial
                Me.txtDescripcion.Text = .Descripcion
                Me.ddlCategoria.SelectedValue = .IdCategoriaMaterial
                Me.ddlSubCategoria.SelectedValue = .IdSubcategoriaMaterial
                Me.ddlUnidadMedida.SelectedValue = .IdUnidadMedida
                Me.txtPartida.Text = .PartidaPresupuestaria
                Me.lblNombrePartida.Text = CargarDescripcionPartidaPresupuestaria(.PartidaPresupuestaria)
                Me.ddlClasificacion.SelectedValue = .Clasificacion
                Me.lblCantidadExistencia.Text = Me.CantidadExistencia
                Me.txtPuntoReorden.Text = .PuntoReorden
                Me.txtMaximoAlmacen.Text = .MaximoAlmacen
                Me.txtMaximoBodegas.Text = .MaximoBodega
                Me.lblCostoPromedio.Text = Me.CostoPromedio
                Me.txtMueble.Text = vlo_UbicacionFisica(0)
                Me.txtColumna.Text = vlo_UbicacionFisica(1)
                Me.txtEstante.Text = vlo_UbicacionFisica(2)
                Me.ddlEstado.SelectedValue = .Estado
            End With
        Else
            WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrrecta", "mostrarAlertaLlaveIncorrrecta();")
        End If


    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvo_idPartidaPresup"></param>
    ''' <remarks></remarks
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>08/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarDatosPartidaPresupuestaria(pvo_idPartidaPresup As String, pvc_NomEgreso As String)
        Me.txtPartida.Text = pvo_idPartidaPresup
        Me.lblNombrePartida.Text = pvc_NomEgreso
        uptxtPartida.Update()
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    '''  Construye el registro para ser enviado a la base de datos
    ''' </summary>
    ''' <returns>Entidad de area profesional</returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>14/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As EntOtmMaterial
        Dim vlo_EntOtmMaterial As EntOtmMaterial

        If Me.operacion = eOperacion.Agregar Then
            vlo_EntOtmMaterial = New EntOtmMaterial
        ElseIf Me.operacion = eOperacion.Modificar Then
            vlo_EntOtmMaterial = Me.Material
        End If
        With vlo_EntOtmMaterial
            .IdMaterial = CInt(Me.lblCodigo.Text)
            .Descripcion = Me.txtDescripcion.Text
            .IdCategoriaMaterial = Me.ddlCategoria.SelectedValue
            .IdSubcategoriaMaterial = Me.ddlSubCategoria.SelectedValue
            .IdUnidadMedida = Me.ddlUnidadMedida.SelectedValue
            .PartidaPresupuestaria = Me.txtPartida.Text
            .Clasificacion = Me.ddlClasificacion.SelectedValue
            .PuntoReorden = Me.txtPuntoReorden.Text
            .MaximoAlmacen = Me.txtMaximoAlmacen.Text
            .MaximoBodega = Me.txtMaximoBodegas.Text
            .UbicacionFisica = String.Format("{0}-{1}-{2}", Me.txtMueble.Text, Me.txtColumna.Text, Me.txtEstante.Text)
            .Estado = Me.ddlEstado.SelectedValue
            .IdUbicacionAdministra = Me.AutorizadoUbicacion.IdUbicacionAdministra
            .Usuario = Me.Usuario.UserName
        End With

        Return vlo_EntOtmMaterial

    End Function

    ''' <summary>
    '''  Agrega un material nuevo a la tabla de materiales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>30/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmMaterial As EntOtmMaterial
        Dim vlo_EntOtmMaterialComparar As EntOtmMaterial

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtmMaterialComparar = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_MATERIAL.ID_MATERIAL, Me.lblCodigo.Text))

            vlo_EntOtmMaterial = ConstruirRegistro()

            If vlo_EntOtmMaterialComparar.Existe Then
                vlo_EntOtmMaterial.IdMaterial = vlo_EntOtmMaterialComparar.IdMaterial + 1
            End If

            Return vlo_Ws_OT_Catalogos.OTM_MATERIAL_InsertarRegistro(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            vlo_EntOtmMaterial) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        Return False
    End Function


    ''' <summary>
    ''' Modifica un registro en la tabla de materiales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmMaterial As EntOtmMaterial

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmMaterial = ConstruirRegistro()

        Try

            Return vlo_Ws_OT_Catalogos.OTM_MATERIAL_ModificarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOtmMaterial)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
        Return False
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

    ''' <summary>
    ''' Busca partidas presupuestarias segun el criterio de búsqueda que obtiene por parametro
    ''' </summary>
    ''' <param name="pvc_CondicionBusquedas"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>07/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BuscarPartidas(pvc_CondicionBusquedas As String)
        Try
            Dim vlo_dataview As Data.DataView
            Dim vlc_CondicionBusqueda As String


            vlo_dataview = New Data.DataView(Me.PartidasPresupuestarias.Tables(0))


            vlc_CondicionBusqueda = String.Format("{0} LIKE '%{1}%'", Modelo.V_OTT_PARTIDAS_PRESUP.ID_EGRESO, txtCodigoPopup.Text)

            If Not String.IsNullOrWhiteSpace(Me.txtDescripcionPopup.Text) Then
                If String.IsNullOrWhiteSpace(vlc_CondicionBusqueda) Then
                    vlc_CondicionBusqueda = String.Format("{0} LIKE '%{1}%'", Modelo.V_OTT_PARTIDAS_PRESUP.NOM_EGRESO, Me.txtDescripcionPopup.Text.Trim)
                Else
                    vlc_CondicionBusqueda = String.Format("{0} AND {1} LIKE '%{2}%'", vlc_CondicionBusqueda, Modelo.V_OTT_PARTIDAS_PRESUP.NOM_EGRESO, Me.txtDescripcionPopup.Text.Trim)
                End If
            End If

            vlo_dataview.RowFilter = vlc_CondicionBusqueda

            grdPartidas.DataSource = vlo_dataview
            grdPartidas.DataBind()

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Function ObtenerCondicionDeBusqueda() As String
        Dim vlc_CondicionBusqueda As String

        vlc_CondicionBusqueda = String.Empty

        If Not String.IsNullOrWhiteSpace(Me.txtCodigoPopup.Text) Then
            vlc_CondicionBusqueda = String.Format("DESCRIPCION LIKE ('%{0}%')", Me.txtCodigoPopup.Text.Trim)
        End If

        If Not String.IsNullOrWhiteSpace(Me.txtDescripcionPopup.Text) Then
            If String.IsNullOrWhiteSpace(vlc_CondicionBusqueda) Then
                vlc_CondicionBusqueda = String.Format("DESCRIPCION LIKE '%{0}%'", Me.txtDescripcionPopup.Text.Trim)
            Else
                vlc_CondicionBusqueda = String.Format("{0} AND DESCRIPCION LIKE '%{1}%'", vlc_CondicionBusqueda, Me.txtDescripcionPopup.Text.Trim)
            End If
        End If

        Return vlc_CondicionBusqueda
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CargarDescripcionPartidaPresupuestaria(pvc_Codigo As String) As String
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarVOttPartidasPresup(
                 ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = '{1}'", Modelo.V_OTT_PARTIDAS_PRESUP.ID_EGRESO, pvc_Codigo), String.Empty, False, 0, 0)


            Return vlo_DsDatos.Tables(0).Rows(0)(Modelo.V_OTT_PARTIDAS_PRESUP.NOM_EGRESO).ToString

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    Private Function RetornaMaximoIdMaterial() As Integer
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As Data.DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_MATERIAL_ListarRegistrosLista(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = {1}", Modelo.OTM_MATERIAL.ID_UBICACION_ADMINISTRA, AutorizadoUbicacion.IdUbicacionAdministra),
               String.Format("{0} {1}", Modelo.OTM_MATERIAL.ID_MATERIAL, Ordenamiento.ASCENDENTE), False, 0, 0)

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                Return (vlo_DsDatos.Tables(0).Rows(vlo_DsDatos.Tables(0).Rows.Count - 1).Item(Modelo.OTM_MATERIAL.ID_MATERIAL)) + 1
            Else
                Return 1
            End If

        Catch ex As Exception
            Throw
        End Try

    End Function

#End Region

End Class
