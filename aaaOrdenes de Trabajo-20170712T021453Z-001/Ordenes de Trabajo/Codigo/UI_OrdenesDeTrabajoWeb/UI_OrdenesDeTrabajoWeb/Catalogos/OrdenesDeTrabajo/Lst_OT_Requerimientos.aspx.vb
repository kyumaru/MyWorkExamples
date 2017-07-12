Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Imports System.Data
Imports System.Configuration
Imports AjaxControlToolkit
Imports Utilerias.OrdenesDeTrabajo.Modelo

''' <summary>
''' Listado y mantenimiento de Catálogos por Requerimientos
''' </summary>
''' <remarks></remarks>
''' <author>César Bermúdez García</author>
''' <creationDate>19/11/2015</creationDate>
Partial Class Catalogos_Lst_OT_Requerimientos
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para el requerimiento actual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/11/2015</creationDate>
    Private Property Requerimiento As EntOtmRequerimiento
        Get
            Return CType(ViewState("Requerimiento"), EntOtmRequerimiento)
        End Get
        Set(value As EntOtmRequerimiento)
            ViewState("Requerimiento") = value
        End Set
    End Property

    ''' <summary>
    ''' Operación actual realizada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/11/2015</creationDate
    Private Property operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Para verificar cuando se debe agregar un subnivel o no
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/11/2015</creationDate>
    Private Property agregaSub As Boolean
        Get
            Return CType(ViewState("agregarSubnivel"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("agregarSubnivel") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento para mostrar los elementos inactivos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>26/11/2015</creationDate>
    Protected Sub ckbMostrarInactivos_CheckedChanged(sender As Object, e As EventArgs) Handles ckbMostrarInactivos.CheckedChanged
        Try
            If (Me.ckbMostrarInactivos.Checked) Then
                Buscar(String.Empty, String.Empty)
            Else
                '{0}: Nombre de Columna
                '{1}: Cargar solo requerimientos con Estado Activo
                Buscar(String.Format("{0} = '{1}'", V_OTM_REQUERIMIENTO.ESTADO, Utilerias.OrdenesDeTrabajo.Estado.ACTIVO), String.Empty)
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Evento para expandir el árbol
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>26/11/2015</creationDate>
    Protected Sub expandir_Click(sender As Object, e As EventArgs)
        Me.tvListado.ExpandAll()
    End Sub

    ''' <summary>
    ''' Evento que recoge el árbol de requerimientos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>26/11/2015</creationDate>
    Protected Sub recoger_Click(sender As Object, e As EventArgs)
        Me.tvListado.CollapseAll()
    End Sub

    ''' <summary>
    ''' Carga la lista inicialmente con estado inactivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Try
                Dim vlo_Usuario = New UsuarioActual
                Dim vlo_director = CargarAutorizadoUbicacion(vlo_Usuario.NumEmpleado)
                If vlo_director.Existe Then
                    CargarEstado()
                    '{0}: Nombre de Columna
                    '{1}: Cargar solo requerimientos con Estado Activo
                    Buscar(String.Format("{0} = '{1}'", V_OTM_REQUERIMIENTO.ESTADO, Utilerias.OrdenesDeTrabajo.Estado.ACTIVO), String.Empty)
                    Me.lblNivel.Text = 1
                    Me.frmRequerimiento.Visible = False
                Else
                    WebUtils.RegistrarScript(Me, "MensajePopup", "MensajePopup('No se pudo encontrar ninguna sede a su cargo por lo que no está autorizado a realizar acciones en esta pantalla. Para gestionar los permisos necesarios contacte al administrador del sistema.','../../Genericos/Frm_MenuPrincipal.aspx');")
                End If
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Coloca la operación agregar y modifica el comportamiento del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/11/2015</creationDate>
    Protected Sub Agregar_Click(sender As Object, e As ImageClickEventArgs)
        Me.operacion = eOperacion.Agregar
        FormularioAgregar()
    End Sub

    ''' <summary>
    ''' Decide qué acción tomar dependiendo de las propiedades 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/11/2015</creationDate>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try
                Select Case Me.operacion
                    Case Is = eOperacion.Agregar

                        If Agregar() Then
                            WebUtils.RegistrarScript(Me, "RegistroExitoso", "mostrarPopupRegistroExitoso();")

                        End If

                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me, "ActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No ha sido posible actualizar la información del Requerimiento.")
                        End If
                    Case Is = eOperacion.NoAplica
                        Dim vlo_Nodo = tvListado.SelectedNode
                        If Me.agregaSub Then
                            If AgregaSubnivel() Then
                                WebUtils.RegistrarScript(Me, "RegistroExitoso", "mostrarPopupRegistroExitoso();")
                            End If
                        End If

                        '{0}: Nombre de Columna
                        '{1}: Cargar solo requerimientos con Estado Activo
                        Buscar(String.Format("{0} = '{1}'", V_OTM_REQUERIMIENTO.ESTADO, Utilerias.OrdenesDeTrabajo.Estado.ACTIVO), String.Empty)

                        WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End Select
            Catch ex As Exception
                If TypeOf (ex) Is System.Web.Services.Protocols.SoapException AndAlso
                CType(ex, System.Web.Services.Protocols.SoapException).Actor = OrdenesDeTrabajoException.NOMBRE_CLASE Then
                    Dim vlo_TallerCapacitacionException As OrdenesDeTrabajoException = OrdenesDeTrabajoException.GetFromSoapException(ex)
                    MostrarAlertaError(vlo_TallerCapacitacionException.Message)
                Else
                    Dim vlo_ControlDeErrores As New ControlDeErrores
                    vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
                End If
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Modifica el comportamiento del formulario cuando un nodo de la lista es seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/11/2015</creationDate>
    Protected Sub tvListado_SelectedNodeChanged(sender As Object, e As EventArgs) Handles tvListado.SelectedNodeChanged
        Dim vlo_Nodo = Me.tvListado.SelectedNode
        If vlo_Nodo IsNot Nothing Then
            CargarRequerimiento(vlo_Nodo.Target)
            Me.lblRequerimientoActual.Text = vlo_Nodo.Value
            If vlo_Nodo.ChildNodes.Count > 0 Then
                Me.trTipoValor.Visible = False
                Me.Eliminar.Visible = False
            Else
                Me.trTipoValor.Visible = True
                Me.Eliminar.Visible = True
            End If
            Me.trEstado.Visible = True
            Me.trRNS.Visible = True
            Me.btnAceptar.Text = "Modificar"
            Me.operacion = eOperacion.Modificar
            Me.Ordenar.Visible = True
            Me.AgregarSubnivel.Visible = IIf(Me.Requerimiento.Nivel >= 3, False, True)
            Me.frmRequerimiento.Visible = True
        End If

    End Sub

    ''' <summary>
    ''' Ejecuta la acción de borrar y actualiza la lista
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>23/11/2015</creationDate>
    Protected Sub Eliminar_Click(sender As Object, e As EventArgs)
        Dim vlo_Nodo = tvListado.SelectedNode
        If vlo_Nodo IsNot Nothing Then
            If Borrar(vlo_Nodo.Target) Then
                '{0}: Nombre de Columna
                '{1}: Cargar solo requerimientos con Estado Activo
                Buscar(String.Format("{0} = '{1}'", V_OTM_REQUERIMIENTO.ESTADO,
                                     Utilerias.OrdenesDeTrabajo.Estado.ACTIVO), String.Empty)
                MostrarAlertaRegistroBorrado()
            Else
                MostrarAlertaRegistroNoBorrado()
            End If
        Else
            mostrarAlertaSeleccionarRegistro()
        End If
    End Sub

    ''' <summary>
    ''' Manda a llamar al formulario de ordenar con el requerimiento seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/11/2015</creationDate>
    Protected Sub Ordenar_Click(sender As Object, e As EventArgs)
        Dim vlo_Nodo = tvListado.SelectedNode
        If vlo_Nodo IsNot Nothing And Me.lblNivel.Text <> "1" Then
            Response.Redirect(String.Format("Lst_OT_Ordenar.aspx?pvc_IdRequerimiento={0}", vlo_Nodo.Target))
        Else
            Response.Redirect(String.Format("Lst_OT_Ordenar.aspx?pvc_IdRequerimiento={0}", String.Empty))
        End If
    End Sub

    ''' <summary>
    ''' Prepara el formulario para agregar un subnivel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/11/2015</creationDate>
    Protected Sub AgregarSubnivel_Click(sender As Object, e As EventArgs)
        'Dim ws As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        'ws = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        'ws.Timeout = -1
        'ws.Credentials = System.Net.CredentialCache.DefaultCredentials

        'ws.OTT_ORDEN_TRABAJO_ListarVOttOstConFichalst("SysUsrSlaOrdenesDeTrabajo", "Pd-g!1023f0.a")

        'ws.OTT_ORDEN_TRABAJO_ListarVOttOstConFichalst("SysUsrUiOrdenesDeTrabajoweb", "bfCgMG3hacmA[")

        Me.operacion = eOperacion.NoAplica
        agregaSub = True
        FormularioAgregarSubnivel()
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Inicializa el formulario cuando la acción es agregar un nuevo requerimiento
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/11/2015</creationDate>
    Private Sub FormularioAgregar()
        Me.trEstado.Visible = False
        Me.trRNS.Visible = False
        Me.btnAceptar.Text = "Ingresar"
        Me.Eliminar.Visible = False
        Me.Ordenar.Visible = False
        Me.frmRequerimiento.Visible = True
        Me.txtDescripcion.Text = String.Empty
        Me.AgregarSubnivel.Visible = False
        Me.lblNivel.Text = 1
        WebUtils.RegistrarScript(Me, "OcultarAreaDeFiltros", "ocultarAreaFiltrosDeBusqueda();")
    End Sub

    ''' <summary>
    ''' Inicializa el formulario cuando se desea agregar un sub nivel
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/11/2015</creationDate>
    Private Sub FormularioAgregarSubnivel()
        Me.trEstado.Visible = False
        Me.trRNS.Visible = True
        Me.lblReqNivSup.Text = Me.Requerimiento.Descripcion
        Me.lblNivel.Text = Me.Requerimiento.Nivel + 1
        Me.btnAceptar.Text = "Ingresar Subnivel"
        Me.Eliminar.Visible = False
        Me.Ordenar.Visible = False
        Me.frmRequerimiento.Visible = True
        Me.txtDescripcion.Text = String.Empty
        Me.AgregarSubnivel.Visible = False
        WebUtils.RegistrarScript(Me, "OcultarAreaDeFiltros", "ocultarAreaFiltrosDeBusqueda();")
    End Sub

    ''' <summary>
    ''' Ejecuta el buscar en la base de datos
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>19/11/2015</creationDate>
    Private Sub Buscar(pvc_Condicion As String, pvc_Orden As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EndDatosPaginacion As EntDatosPaginacion
        Dim vlo_DtDatos As DataTable

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_EndDatosPaginacion = vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_ObtenerDatosPaginacionVOtmRequerimientolst(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_EndDatosPaginacion.TotalRegistros > 0 Then
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de Requerimientos: {0}", vlo_EndDatosPaginacion.TotalRegistros)

                '{0} = Nivel del requerimiento
                '{1} = Ordenar Ascendente
                '{2} = Orden del requerimiento
                '{3} = Ordenar Ascendentemente

                vlo_DtDatos = CargarLista(pvc_Condicion, String.Format("{0} {1},{2} {3}",
                            V_OTM_REQUERIMIENTO.NIVEL,
                            Ordenamiento.ASCENDENTE,
                            V_OTM_REQUERIMIENTO.ORDEN,
                            Ordenamiento.ASCENDENTE), 0).Tables(0)

                CargarTreeView(vlo_DtDatos)
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
        End Try
    End Sub

    ''' <summary>
    ''' Carga el treeview en la vista con los datos recibidos del buscar
    ''' </summary>
    ''' <param name="pvo_DtDatos"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>23/11/2015</creationDate>
    Private Sub CargarTreeView(pvo_DtDatos As DataTable)
        Dim vlo_nodoRequerimiento As TreeNode
        Dim vlo_nodoHijo As TreeNode
        Dim vlo_nodoNieto As TreeNode

        Me.tvListado.Nodes.Clear()
        Me.Eliminar.Attributes.Add("data-uniqueid", Me.Eliminar.UniqueID)

        'Obtener Padres
        Dim vlo_Padres() As DataRow
        vlo_Padres = pvo_DtDatos.Select(String.Format("{0}={1}", Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTO.ID_REQUERIMIENTO_PADRE, 0))

        If vlo_Padres.Length = 0 Then
            vlo_Padres = pvo_DtDatos.Select(String.Format(""))
        End If


        For Each rowPadre In vlo_Padres
            vlo_nodoRequerimiento = New TreeNode
            vlo_nodoRequerimiento.Text = rowPadre(V_OTM_REQUERIMIENTO.DESCRIPCION)
            vlo_nodoRequerimiento.Target = rowPadre(V_OTM_REQUERIMIENTO.ID_REQUERIMIENTO).ToString
            vlo_nodoRequerimiento.Value = String.Format("<br/>Descripción:{0}<br/>Req. Nivel Superior:{1}<br/>Estado:{2}<br/>Nivel:{3}<br/>",
                                                        rowPadre(V_OTM_REQUERIMIENTO.DESCRIPCION),
                                                        rowPadre(V_OTM_REQUERIMIENTO.ID_REQUERIMIENTO_PADRE).ToString(),
                                                        rowPadre(V_OTM_REQUERIMIENTO.ESTADO),
                                                        rowPadre(V_OTM_REQUERIMIENTO.NIVEL).ToString())


            'Obtener Hijos
            Dim vlo_Hijos() As DataRow
            vlo_Hijos = pvo_DtDatos.Select(String.Format("{0}={1}", Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTO.ID_REQUERIMIENTO_PADRE,
                                                         CInt(rowPadre(V_OTM_REQUERIMIENTO.ID_REQUERIMIENTO))))

            If vlo_Hijos.Length > 0 Then
                For Each rowHija In vlo_Hijos
                    vlo_nodoHijo = New TreeNode
                    vlo_nodoHijo.Text = rowHija(V_OTM_REQUERIMIENTO.DESCRIPCION)
                    vlo_nodoHijo.Target = rowHija(V_OTM_REQUERIMIENTO.ID_REQUERIMIENTO).ToString
                    vlo_nodoHijo.Value = String.Format("<br/>Descripción:{0}<br/>Req. Nivel Superior:{1}<br/>Estado:{2}<br/>Nivel:{3}<br/>",
                                                       rowHija(V_OTM_REQUERIMIENTO.DESCRIPCION),
                                                       rowHija(V_OTM_REQUERIMIENTO.ID_REQUERIMIENTO_PADRE).ToString(),
                                                       rowHija(V_OTM_REQUERIMIENTO.ESTADO),
                                                       rowHija(V_OTM_REQUERIMIENTO.NIVEL).ToString())

                    'Obtener Nietos
                    Dim vlo_Nietos() As DataRow
                    vlo_Nietos = pvo_DtDatos.Select(String.Format("{0}={1}", Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTO.ID_REQUERIMIENTO_PADRE,
                                                                  CInt(rowHija(V_OTM_REQUERIMIENTO.ID_REQUERIMIENTO))))
                    If vlo_Nietos.Length > 0 Then
                        For Each rowNieto In vlo_Nietos
                            vlo_nodoNieto = New TreeNode
                            vlo_nodoNieto.Text = rowNieto(V_OTM_REQUERIMIENTO.DESCRIPCION)
                            vlo_nodoNieto.Target = rowNieto(V_OTM_REQUERIMIENTO.ID_REQUERIMIENTO).ToString
                            vlo_nodoNieto.Value = String.Format("<br/>Descripción:{0}<br/>Req. Nivel Superior:{1}<br/>Estado:{2}<br/>Nivel:{3}<br/>",
                                                                rowNieto(V_OTM_REQUERIMIENTO.DESCRIPCION),
                                                                rowNieto(V_OTM_REQUERIMIENTO.ID_REQUERIMIENTO_PADRE).ToString(),
                                                                rowNieto(V_OTM_REQUERIMIENTO.ESTADO),
                                                                rowNieto(V_OTM_REQUERIMIENTO.NIVEL).ToString())
                            vlo_nodoHijo.ChildNodes.Add(vlo_nodoNieto)


                        Next
                    End If
                    vlo_nodoRequerimiento.ChildNodes.Add(vlo_nodoHijo)
                Next
            End If

            Me.tvListado.Nodes.Add(vlo_nodoRequerimiento)
            Me.tvListado.NodeStyle.CssClass = "treeNode"
            Me.tvListado.RootNodeStyle.CssClass = "rootNode"
            Me.tvListado.LeafNodeStyle.CssClass = "leafNode"

        Next

    End Sub

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
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("MostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub mostrarAlertaSeleccionarRegistro()
        WebUtils.RegistrarScript(Me, "alertaSeleccionarRegistro", "mostrarAlertaSeleccionarRegistro();")
    End Sub

    Private Sub CargarEstado()
        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
    End Sub

    ''' <summary>
    ''' Carga datos del requerimiento seleccionado, si posee un padre también lo carga para obtener su descripción
    ''' </summary>
    ''' <param name="pvc_IdRequerimiento"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>23/11/2015</creationDate>
    Private Sub CargarRequerimiento(pvc_IdRequerimiento As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_Requerimiento As EntOtmRequerimiento

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0} Nombre de la columna 
            '{1} numero de ID_REQUERIMIENTO

            Me.Requerimiento = vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0})={1}", Modelo.OTM_REQUERIMIENTO.ID_REQUERIMIENTO, pvc_IdRequerimiento.Trim.ToUpper))


            vlo_Requerimiento = vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0})={1}", Modelo.OTM_REQUERIMIENTO.ID_REQUERIMIENTO, Me.Requerimiento.IdRequerimientoPadre))


        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If Me.Requerimiento.Existe Then
            With Me.Requerimiento
                Me.lblReqNivSup.Text = vlo_Requerimiento.Descripcion
                Me.lblNivel.Text = .Nivel
                Me.txtDescripcion.Text = .Descripcion
                Me.ddlEstado.SelectedValue = .Estado
                Me.rbtnlValor.SelectedValue = .TipoValor
            End With
        Else
            WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrrecta", "mostrarAlertaLlaveIncorrrecta();")
        End If
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Construye el registro cuando se desea insertar un nuevo requerimiento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/11/2015</creationDate>
    Private Function ConstruirRegistro() As EntOtmRequerimiento
        Dim vlo_EntOtmRequerimiento As EntOtmRequerimiento

        If Me.operacion = eOperacion.Agregar Then
            vlo_EntOtmRequerimiento = New EntOtmRequerimiento
            vlo_EntOtmRequerimiento.Estado = Estado.ACTIVO
            vlo_EntOtmRequerimiento.Nivel = 1
        ElseIf Me.operacion = eOperacion.Modificar Then
            vlo_EntOtmRequerimiento = Me.Requerimiento
        End If

        Dim vlo_UsuarioActual = New UsuarioActual()
        With vlo_EntOtmRequerimiento
            .Descripcion = Me.txtDescripcion.Text.Trim.ToUpper
            .Estado = IIf(Me.operacion = eOperacion.Modificar, Me.ddlEstado.SelectedValue, Estado.ACTIVO)
            .IdUbicacion = CargarAutorizadoUbicacion(vlo_UsuarioActual.NumEmpleado()).IdUbicacionAdministra
            .Usuario = vlo_UsuarioActual.UserName
            .Orden = IIf(Me.operacion <> eOperacion.Agregar, .Orden, CargarOrden(1))
            .Nivel = vlo_EntOtmRequerimiento.Nivel
            .TipoValor = Me.rbtnlValor.SelectedItem.Value.Trim.ToUpper()
        End With

        Return vlo_EntOtmRequerimiento

    End Function

    ''' <summary>
    ''' Construye el subnivel a insertar en el árbol
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/11/2015</creationDate>
    Private Function ConstruirRegistroSubnivel() As EntOtmRequerimiento
        Dim vlo_EntOtmRequerimiento = New EntOtmRequerimiento

        Dim vlo_UsuarioActual = New UsuarioActual()
        With vlo_EntOtmRequerimiento
            .Descripcion = Me.txtDescripcion.Text.Trim.ToUpper
            .Estado = Estado.ACTIVO
            .IdUbicacion = CargarAutorizadoUbicacion(vlo_UsuarioActual.NumEmpleado()).IdUbicacionAdministra
            .Usuario = vlo_UsuarioActual.UserName
            .Nivel = CInt(Me.lblNivel.Text)
            .Orden = IIf(Me.operacion = eOperacion.Agregar, CargarOrden(.Nivel), .Orden) 'TODO Orden espacializado para ese requerimiento
            .TipoValor = Me.rbtnlValor.SelectedItem.Value.Trim.ToUpper()
            .IdRequerimientoPadre = Me.Requerimiento.IdRequerimiento
        End With

        Return vlo_EntOtmRequerimiento

    End Function

    ''' <summary>
    '''  Manda a llamar a un método que agrega un requerimiento nuevo a la tabla de requerimientos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/11/2015</creationDate>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmRequerimiento As EntOtmRequerimiento

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmRequerimiento = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmRequerimiento) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Inserta un registro de tipo requerimiento en el subnivel deseado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/11/2015</creationDate>
    Private Function AgregaSubnivel() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmRequerimiento As EntOtmRequerimiento

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        'Se construye el objeto a insertar
        vlo_EntOtmRequerimiento = ConstruirRegistroSubnivel()

        Try

            Return vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmRequerimiento) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Manda a llamar a un método que modifica un registro en la tabla de requerimientos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/11/2015</creationDate>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmRequerimiento As EntOtmRequerimiento
        Dim vlo_DtDatos As DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmRequerimiento = ConstruirRegistro()

        Try
            If vlo_EntOtmRequerimiento.Estado = Estado.INACTIVO Then

                'Obtiene los requerimientos hijos
                vlo_DtDatos = CargarLista(String.Format("{0} = {1}", V_OTM_REQUERIMIENTO.ID_REQUERIMIENTO_PADRE, vlo_EntOtmRequerimiento.IdRequerimiento),
                            String.Format("{0} {1},{2} {3}",
                            Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTO.NIVEL,
                            Utilerias.OrdenesDeTrabajo.Ordenamiento.ASCENDENTE,
                            Utilerias.OrdenesDeTrabajo.Modelo.V_OTM_REQUERIMIENTO.ORDEN,
                            Utilerias.OrdenesDeTrabajo.Ordenamiento.ASCENDENTE), 0)

                InactivarRequerimientosHijos(vlo_DtDatos, vlo_EntOtmRequerimiento)
                Return True

            End If

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' inactiva los requerimientos hijos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>18/12/2015</creationDate>
    Private Sub InactivarRequerimientosHijos(pvo_Datos As DataSet, pvo_EntOtmRequerimiento As EntOtmRequerimiento)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        'llama a un adapter para inhabilitar el requerimiento padre y sus hijos
        vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_EjecutarPrOtInhabilitarrequerimiento(
        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
        pvo_EntOtmRequerimiento.IdRequerimiento)

    End Sub

    ''' <summary>
    ''' Manda a llamar a la vista para listar los registros 
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <returns>Un dataset con los registros asociados a las condiciones de la búsqueda</returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>20/11/2015</creationDate>
    Private Function CargarLista(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer) As DataSet
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden, False, pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))
            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                Return vlo_DsDatos
            Else
                MostrarAlertaNoHayDatos()
                Return Nothing
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
    End Function

    ''' <summary>
    ''' Manda a llamar a un método para borrar un registro 
    ''' </summary>
    ''' <param name="pvc_IdRequerimiento"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>25/11/2015</creationDate>
    Private Function Borrar(pvc_IdRequerimiento As String) As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmRequerimiento As EntOtmRequerimiento

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_EntOtmRequerimiento = New EntOtmRequerimiento

        Try
            vlo_EntOtmRequerimiento.IdRequerimiento = CType(pvc_IdRequerimiento, Integer)

            Return vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_BorrarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmRequerimiento) > 0

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga el valor del ultimo orden en nivel 1 para agregar el requerimiento al final
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>24/11/2015</creationDate>
    Private Function CargarOrden(pvn_IdNivel As Integer) As Integer
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_ObtenerFnOtUltimoordenrequerimiento(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvn_IdNivel)
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

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
