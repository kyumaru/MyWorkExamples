Imports Wsr_OT_Catalogos
Imports Utilerias.OrdenesDeTrabajo
Partial Class Catalogos_Frm_OT_ActividadesPorCategoriaServicio
    Inherits System.Web.UI.Page
#Region "Propiedades"
    Private Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    Private Property Actividad As EntOtmActividad
        Get
            Return CType(ViewState("Actividad"), EntOtmActividad)
        End Get
        Set(value As EntOtmActividad)
            ViewState("Actividad") = value
        End Set
    End Property

    Private Property Categoria As EntOtmCategoriaServicio
        Get
            Return CType(ViewState("Categoria"), EntOtmCategoriaServicio)
        End Get
        Set(value As EntOtmCategoriaServicio)
            ViewState("Categoria") = value
        End Set
    End Property

    Private Property UnidadAdministra As Integer
        Get
            Return CType(ViewState("UnidadAdministra"), Integer)
        End Get
        Set(value As Integer)
            ViewState("UnidadAdministra") = value
        End Set
    End Property
#End Region

#Region "Eventos"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Try
                Me.lblParametroIdCategoria.Text = WebUtils.LeerParametro(Of String)("pvc_IdCategoria")
                Me.UnidadAdministra = CType(Session("pvo_UnidadAdministra"), Integer)
                InicializarFormulario()

            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try

        End If
    End Sub

    ''' <summary>
    ''' Evento click del boton aceptar, ingresa o modifica dependiendo de la opcion seleccionada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then 'preguntar cuando hay validadores en el codigo
            Try
                Select Case Me.Operacion
                    Case Is = eOperacion.Agregar
                        If Agregar() Then
                            WebUtils.RegistrarScript(Me, "MostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("No a sido posible registrar la nueva actividad.")
                        End If
                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No a sido posible actualizar la información de la actividad.")
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
#End Region

#Region "Metodos"

    ''' <summary>
    ''' Carga combo de Estado y Pertenece a sede, ademas oculta Estado cuando esta en modo insertar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InicializarFormulario()
        CargarEstadoActividad()
        CargarSector()
        CargarCategoria()
        Me.lblParametroIdCategoria.Attributes.Add("style", "display:none;")
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Actividad"
                'Me.ddlEstado.Attributes.Add("style", "display:none;") 'ocultar etiqueta
                'Me.trEstado.Attributes.Add("style", "display:none;")
                Me.trEstado.Visible = False

            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Actividad"
                Me.trEstado.Visible = True
                ' Me.trEstado.Attributes.Add("style", "display:block;")
                'Me.ddlEstado.Attributes.Add("style", "display:block;") 'mostrar texto
                Try
                    CargarActividad(WebUtils.LeerParametro(Of String)("pvc_IdCategoria"), WebUtils.LeerParametro(Of String)("pvc_IdActividad"))
                Catch ex As Exception
                    Throw
                End Try
        End Select
    End Sub

    ''' <summary>
    ''' Obtiene el registro por medio del parametro pvc_IdUbicacion
    ''' </summary>
    ''' <param name="pvc_IdCategoria"></param>
    ''' <remarks></remarks>
    Private Sub CargarActividad(pvc_IdCategoria As String, pvc_IdActividad As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        '0 columna, 1 valor busqueda
        Try
            Me.Actividad = vlo_Ws_OT_Catalogos.OTM_ACTIVIDAD_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0}) = '{1}' AND UPPER({2}) = '{3}'", Modelo.OTM_ACTIVIDAD.ID_CATEGORIA_SERVICIO, pvc_IdCategoria.Trim.ToUpper, Modelo.OTM_ACTIVIDAD.ID_ACTIVIDAD, pvc_IdActividad.Trim.ToUpper))   'verificar el nombre tambien?
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
        If Me.Actividad.Existe Then
            With Me.Actividad

                Me.txtDescripcion.Text = .Descripcion
                Me.txtResumen.Text = IIf(.DescripcionAmpliada = "-", String.Empty, .DescripcionAmpliada)
                Me.ddlSector.SelectedValue = .IdSectorTaller
                Me.ddlEstado.SelectedValue = .Estado

            End With
        Else
            WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    Private Sub CargarCategoria()
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos



        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
        '0 columna, 1 valor busqueda
        Try
            Me.Categoria = vlo_Ws_OT_Catalogos.OTM_CATEGORIA_SERVICIO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0}) = '{1}'", Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO, Me.lblParametroIdCategoria.Text))   'verificar el nombre tambien?
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        With Me.Categoria

            Me.lblCategoria.Text = .Descripcion

        End With

    End Sub

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    Private Sub CargarEstadoActividad() 'CondicionUbicacion

        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
        Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
        Me.ddlEstado.SelectedValue = Estado.ACTIVO
    End Sub

    Private Sub CargarSector()
        Dim vlo_DsSector As System.Data.DataSet
        Dim vlo_Wsr_OT_Catalogos As Ws_OT_Catalogos
        Dim pvc_Condicion As String

        Try
            vlo_Wsr_OT_Catalogos = New Ws_OT_Catalogos
            vlo_Wsr_OT_Catalogos.Timeout = -1
            vlo_Wsr_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials
            'vlo_Wsr_TC_Catalogos.Url = ConfigurationManager.AppSettings(<ConstanteInterna.MI_SERVICIO_WEB>)

            pvc_Condicion = String.Format("{0} = '{1}' AND {2} = {3}", Modelo.V_OTM_SECTOR_TALLER.TIPO_AREA, Constantes.TIPO_AREA_SEC, Modelo.V_OTM_CATEGORIA_SERVICIO.ID_UBICACION_ADMINISTRA, Me.UnidadAdministra)

            vlo_DsSector = vlo_Wsr_OT_Catalogos.OTM_SECTOR_TALLER_ListarRegistrosLista(
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
            pvc_Condicion,
            String.Empty,
            False,
            0,
            0)
            'primer string.empty es la condicion
            Me.ddlSector.Items.Clear()
            Me.ddlSector.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            If vlo_DsSector.Tables(0) IsNot Nothing AndAlso vlo_DsSector.Tables(0).Rows.Count > 0 Then
                With Me.ddlSector
                    .DataSource = vlo_DsSector
                    .DataMember = vlo_DsSector.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_SECTOR_TALLER.NOMBRE
                    .DataValueField = Modelo.V_OTM_SECTOR_TALLER.ID_SECTOR_TALLER
                    .DataBind()
                End With
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

#End Region

#Region "Funciones"

    ''' <summary>
    ''' Construye registro para insertar a la base de datos con los datos del usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConstruirRegistro() As EntOtmActividad
        Dim vlo_EntOtmActividad As EntOtmActividad



        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtmActividad = New EntOtmActividad
        ElseIf Me.Operacion = eOperacion.Modificar Then
            vlo_EntOtmActividad = Me.Actividad
        End If

        With vlo_EntOtmActividad

            .IdCategoriaServicio = Me.lblParametroIdCategoria.Text
            .Descripcion = Me.txtDescripcion.Text.Trim
            .DescripcionAmpliada = Me.txtResumen.Text.Trim
            If Me.ddlSector.SelectedValue = "" Then
                .IdSectorTaller = 0
            Else
                .IdSectorTaller = Me.ddlSector.SelectedValue
            End If
            .Estado = Me.ddlEstado.SelectedValue
            '.Usuario = New UsuarioActual().UserName 

        End With
        Return vlo_EntOtmActividad
    End Function

    ''' <summary>
    ''' Ingresa nuevo registro en la DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmActividad As EntOtmActividad

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmActividad = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_ACTIVIDAD_InsertarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmActividad) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Modifica registro en la DB
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmActividad As EntOtmActividad

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmActividad = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_ACTIVIDAD_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmActividad) > 0
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
