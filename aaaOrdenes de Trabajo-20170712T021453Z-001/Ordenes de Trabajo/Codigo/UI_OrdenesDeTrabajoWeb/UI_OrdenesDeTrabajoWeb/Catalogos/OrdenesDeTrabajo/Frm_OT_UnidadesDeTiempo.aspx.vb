Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos

''' <summary>
''' Clase para presentar información de las unidades de tiempo, agregarlas y/o modificarlas
''' </summary>
''' <remarks></remarks>
''' <author>César Bermúdez García</author>
''' <creationDate>15/1/2016</creationDate>
Partial Class Catalogos_Frm_OT_UnidadesDeTiempo
    Inherits System.Web.UI.Page
#Region "Propiedades"
    ''' <summary>
    ''' Propiedad que define la operación a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/1/2016</creationDate>
    Private Property operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que almacena el valor actual de la unidad de tiempo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/1/2016</creationDate>
    Private Property UnidadTiempo As EntOtmUnidadTiempo
        Get
            Return CType(ViewState("UnidadTiempo"), EntOtmUnidadTiempo)
        End Get
        Set(value As EntOtmUnidadTiempo)
            ViewState("UnidadTiempo") = value
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
    ''' <creationDate>15/1/2016</creationDate>
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
    ''' Este método se ejecutará al presionar el botón aceptar, dependiendo de la acción modificará o agregará y al final presentará un mensaje
    ''' Si la operación fue exitosa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/1/2016</creationDate>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try
                Select Case Me.operacion
                    Case Is = eOperacion.Agregar

                        If Agregar() Then
                            WebUtils.RegistrarScript(Me, "RegistroExitoso", "mostrarPopupRegistroExitoso();")
                        Else
                            MostrarAlertaError("La Descripción ingresada ya existe.")
                        End If
                    Case Is = eOperacion.Modificar
                        If Modificar() Then
                            WebUtils.RegistrarScript(Me, "ActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                        Else
                            MostrarAlertaError("No ha sido posible actualizar la información de la unidad de tiempo.")
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

#End Region

#Region "Metodos"

    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub


    ''' <summary>
    ''' Carga los estados disponibles para cualquier unidad de tiempo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/1/2016</creationDate>
    Private Sub CargarEstado()
        Me.ddlEstado.Items.Clear()
        Me.ddlEstado.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
        Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
        Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
    End Sub

    ''' <summary>
    ''' Carga la lista de unidades base
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/1/2016</creationDate>
    Private Sub CargarUnidades()
        Me.ddlUnidad.Items.Clear()
        Me.ddlUnidad.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
        Me.ddlUnidad.Items.Add(New ListItem("Minutos", Unidades.MINUTOS))
        Me.ddlUnidad.Items.Add(New ListItem("Horas", Unidades.HORAS))
        Me.ddlUnidad.Items.Add(New ListItem("Días", Unidades.DIAS))
        Me.ddlUnidad.Items.Add(New ListItem("Semanas", Unidades.SEMANAS))
        Me.ddlUnidad.Items.Add(New ListItem("Meses", Unidades.MESES))
        Me.ddlUnidad.Items.Add(New ListItem("Años", Unidades.ANIOS))
    End Sub

    ''' <summary>
    ''' Inicializa el formulario dependiendo de la operación recibida por parámetro
    ''' Tambien carga el drop down list de estados y de unidades
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/1/2016</creationDate>
    Private Sub InicializarFormulario()
        CargarEstado()
        CargarUnidades()

        Me.operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")

        Select Case Me.operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Unidad de tiempo"
                Me.ddlEstado.SelectedValue = Estado.ACTIVO
            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Unidad de tiempo"
                Try
                    CargarUnidad(WebUtils.LeerParametro(Of String)("pvc_IdUnidad"))
                Catch ex As Exception
                    Throw
                End Try

        End Select

    End Sub

    ''' <summary>
    ''' Obtiene el registro desde la base de datos y lo carga en memoria
    ''' </summary>
    ''' <param name="pvc_IdUnidad"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/1/2016</creationDate>
    Private Sub CargarUnidad(pvc_IdUnidad As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            '{0} Nombre de la columna 
            '{1} numero de ID_UNIDAD_TIEMPO

            Me.UnidadTiempo = vlo_Ws_OT_Catalogos.OTM_UNIDAD_TIEMPO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("UPPER({0})={1}", Modelo.OTM_UNIDAD_TIEMPO.ID_UNIDAD_TIEMPO, pvc_IdUnidad.Trim.ToUpper))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If Me.UnidadTiempo.Existe Then
            With Me.UnidadTiempo
                Me.txtDescripcion.Text = .Descripcion
                Me.ddlEstado.SelectedValue = .Estado
                Me.txtValor.Text = .Valor
                Me.ddlUnidad.SelectedValue = .Unidad
            End With
        Else
            WebUtils.RegistrarScript(Me, "MostrarAlertaLlaveIncorrrecta", "mostrarAlertaLlaveIncorrrecta();")
        End If


    End Sub

#End Region

#Region "Funciones"
    ''' <summary>
    '''  Construye el registro para ser enviado a la base de datos
    ''' </summary>
    ''' <returns>Entidad de Unidade de tiempo</returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/1/2016</creationDate>
    Private Function ConstruirRegistro() As EntOtmUnidadTiempo
        Dim vlo_EntOtmUnidadTiempo As EntOtmUnidadTiempo

        If Me.operacion = eOperacion.Agregar Then
            vlo_EntOtmUnidadTiempo = New EntOtmUnidadTiempo
            vlo_EntOtmUnidadTiempo.Estado = Estado.ACTIVO
        ElseIf Me.operacion = eOperacion.Modificar Then
            vlo_EntOtmUnidadTiempo = Me.UnidadTiempo
        End If

        Dim vlo_UsuarioActual = New UsuarioActual()
        With vlo_EntOtmUnidadTiempo
            .Descripcion = Me.txtDescripcion.Text.Trim.ToUpper
            .Valor = Me.txtValor.Text.Trim
            .Unidad = Me.ddlUnidad.SelectedValue
            .Estado = IIf(Me.operacion = eOperacion.Modificar, Me.ddlEstado.SelectedValue, Estado.ACTIVO)
            .Usuario = vlo_UsuarioActual.UserName

        End With

        Return vlo_EntOtmUnidadTiempo

    End Function

    ''' <summary>
    '''  Agrega una unidad de tiempo nueva a la tabla de unidades de tiempo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/1/2016</creationDate>
    Private Function Agregar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmUnidadTiempo As EntOtmUnidadTiempo
        Dim vlo_EntidadOtmUnidadTiempo As EntOtmUnidadTiempo

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmUnidadTiempo = ConstruirRegistro()

        vlo_EntidadOtmUnidadTiempo = vlo_Ws_OT_Catalogos.OTM_UNIDAD_TIEMPO_ObtenerRegistro(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("{0} = '{1}'", Modelo.OTM_UNIDAD_TIEMPO.DESCRIPCION, vlo_EntOtmUnidadTiempo.Descripcion.ToUpper()))

        If Not vlo_EntidadOtmUnidadTiempo.Existe Then
            Try
                Return vlo_Ws_OT_Catalogos.OTM_UNIDAD_TIEMPO_InsertarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOtmUnidadTiempo) > 0
            Catch ex As Exception
                Throw
            Finally
                If vlo_Ws_OT_Catalogos IsNot Nothing Then
                    vlo_Ws_OT_Catalogos.Dispose()
                End If
            End Try
        End If


    End Function


    ''' <summary>
    ''' Modifica un registro en la tabla de Unidades de tiempo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/1/2016</creationDate>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_EntOtmUnidadTiempo As EntOtmUnidadTiempo

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmUnidadTiempo = ConstruirRegistro()

        Try
  
            Return vlo_Ws_OT_Catalogos.OTM_UNIDAD_TIEMPO_ModificarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    vlo_EntOtmUnidadTiempo) > 0


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
