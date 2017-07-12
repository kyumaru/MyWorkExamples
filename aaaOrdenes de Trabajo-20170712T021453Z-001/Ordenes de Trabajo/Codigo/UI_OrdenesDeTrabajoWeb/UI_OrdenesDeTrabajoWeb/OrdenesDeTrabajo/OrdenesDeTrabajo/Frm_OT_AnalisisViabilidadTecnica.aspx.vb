Imports System.Data
Imports Wsr_OT_OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos



''' <summary>
''' Clase para manejar el comportamiento interno de la página.
''' </summary>
''' <author>César Bermudez Garcia</author>
''' <creationDate>11/02/2016</creationDate>
''' <changeLog></changeLog>
''' <remarks></remarks>
Partial Class OrdenesDeTrabajo_Frm_OT_AnalisisViabilidadTecnica
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>06/04/2016</creationDate>
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
    ''' Indica a cual página se debe regresar.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property PaginaRegresar As String
        Get
            Return CType(ViewState("PaginaRegresar"), String)
        End Get
        Set(value As String)
            ViewState("PaginaRegresar") = value
        End Set
    End Property

    ''' <summary>
    ''' Extensiones para archivo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property ExtensionesPermitidas As String
        Get
            Return CType(ViewState("ExtensionesPermitidas"), String)
        End Get
        Set(value As String)
            ViewState("ExtensionesPermitidas") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>09/09/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Property TamanoArchivo As Integer
        Get
            Return CType(ViewState("TamanoArchivo"), Integer)
        End Get
        Set(value As Integer)
            ViewState("TamanoArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
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
    ''' Muestra a los evaluadores actualmente ingresados en la tabla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property DsEvaluadores As DataTable
        Get
            Return CType(ViewState("DsEvaluadores"), DataTable)
        End Get
        Set(value As DataTable)
            ViewState("DsEvaluadores") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de tipos de documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsTipoArchivo As Data.DataSet
        Get
            Return CType(ViewState("DsTipoArchivo"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsTipoArchivo") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el dataset de archivos adjuntos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsAdjuntosInsert As Data.DataSet
        Get
            Return CType(ViewState("DsAdjuntosInsert"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsAdjuntosInsert") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la lista de todos los funcionarios de este sector o taller
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsEvaluadoresProf As DataSet
        Get
            Return CType(ViewState("DsEvaluadoresProf"), DataSet)
        End Get
        Set(value As DataSet)
            ViewState("DsEvaluadoresProf") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la lista de evaluadores proveniente de la base de datos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsEvaluadoresActual As DataSet
        Get
            Return CType(ViewState("DsEvaluadoresActual"), DataSet)
        End Get
        Set(value As DataSet)
            ViewState("DsEvaluadoresActual") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property OrdenTrabajo As EntOttOrdenTrabajo
        Get
            Return CType(ViewState("OrdenTrabajo"), EntOttOrdenTrabajo)
        End Get
        Set(value As EntOttOrdenTrabajo)
            ViewState("OrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/02/2016</creationDate>
    Private Property IdSectorTaller As Integer
        Get
            If ViewState("IdSectorTaller") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdSectorTaller"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdSectorTaller") = value
        End Set
    End Property

    ''' <summary>
    ''' Es el identificador base de la Orden de trabajo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/02/2016</creationDate>
    Public Property IdOrdenTrabajo As String
        Get
            If ViewState("IdOrdenTrabajo") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdOrdenTrabajo"), String)
        End Get
        Set(value As String)
            ViewState("IdOrdenTrabajo") = value
        End Set
    End Property


    ''' <summary>
    ''' Guarda la ubicación de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/02/2016</creationDate>
    Public Property IdUbicacion As Integer
        Get
            If ViewState("IdUbicacion") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Guarda la ubicación de la OT
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/02/2016</creationDate>
    Public Property Anno As Integer
        Get
            If ViewState("Anno") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("Anno"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Anno") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena el profesional encargado de la orden
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>15/02/2016</creationDate>
    Public Property ProfesionalEncargado As String
        Get
            If ViewState("ProfesionalEncargado") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("ProfesionalEncargado"), String)
        End Get
        Set(value As String)
            ViewState("ProfesionalEncargado") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena cuando se efectuan cambios o no
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>18/02/2016</creationDate>
    Public Property BanderaCambios As Boolean
        Get
            If ViewState("BanderaCambios") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("BanderaCambios"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("BanderaCambios") = value
        End Set
    End Property

#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento para procesar la carga inicial de datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
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
    ''' Evento que se ejecuta cuando se da click sobre el boton de actualizar (nombre del proyecto)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Try
            Me.OrdenTrabajo.NombreProyecto = Me.txtNombreProyecto.Text.Trim
            If Modificar() Then
                InicializarControlUsuario()
                Me.BanderaCambios = True
                WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarAlertaNombreProyectoExitoso();")
            Else
                MostrarAlertaError("No ha sido posible actualizar la información del nombre del proyecto.")
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
    ''' Cambia el label de area profesional según el profesional seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlFuncionario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFuncionario.SelectedIndexChanged
        Dim vlc_Condicion As String
        Dim vlo_Profesional() As DataRow

        Try
            If Not String.IsNullOrWhiteSpace(Me.ddlFuncionario.SelectedValue) Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OTF_OPERARIO_AREALST.CEDULA, Me.ddlFuncionario.SelectedValue)
                vlo_Profesional = Me.DsEvaluadoresProf.Tables(0).Select(vlc_Condicion)
                Me.lblAreaProfesional.Text = vlo_Profesional(0).Item(Modelo.V_OTF_OPERARIO_AREALST.AREA)
                Me.upAreaProfesional.Update()
                ActivarEvaluador()
            End If

            WebUtils.RegistrarScript(Me.Page, "fechasConfiguracion", "fechasConfiguracion();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Cambia los formatos permitidos dependiendo del tipo de archivo seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ddlTipoArchivo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipoArchivo.SelectedIndexChanged
        Dim vlo_TipoDocumento() As DataRow
        Try
            If Me.ddlTipoArchivo.SelectedValue <> String.Empty Then
                vlo_TipoDocumento = DsTipoArchivo.Tables(0).Select(String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, Me.ddlTipoArchivo.SelectedValue))
                If DsTipoArchivo.Tables.Count > 0 AndAlso vlo_TipoDocumento IsNot Nothing Then
                    Me.ExtensionesPermitidas = vlo_TipoDocumento(0).Item(Modelo.OTM_TIPO_DOCUMENTO.FORMATOS_ADMITIDOS)
                    imgExtensiones.Attributes.Add("title", String.Format("Extensiones permitidas:{0}", ExtensionesPermitidas.ToLower))
                    Me.TamanoArchivo = vlo_TipoDocumento(0).Item(Modelo.OTM_TIPO_DOCUMENTO.TAMANIO_MAXIMO)
                    ActivarEvaluacion()
                    WebUtils.RegistrarScript(Me.Page, "cargarExtensiones", "cargarExtensiones();")
                End If
                Me.trArchivosTipo.Visible = True
            Else
                Me.trArchivosTipo.Visible = False
            End If

            WebUtils.RegistrarScript(Me.Page, "fechasConfiguracion", "fechasConfiguracion();")
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click en el boton de agregar para la evaluacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>29/01/2016</creationDate>
    ''' <changeLog>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>18/02/2016</creationDate>
    ''' <change>Se agrega funcionalidad del update panel para el repeater de funcionarios</change>
    ''' </changeLog>
    Protected Sub btnAgregarFuncionario_Click(sender As Object, e As EventArgs) Handles btnAgregarFuncionario.Click
        Try
            AgregaFuncionariosDataTable()
            Me.upRpEncargados.Update()
            ActivarEvaluador()
            WebUtils.RegistrarScript(Me.Page, "fechasConfiguracion", "fechasConfiguracion();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta cuando se da click sobre el boton de eliminar
    ''' del listado de funcionarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            BorrarFuncionario(CType(sender, ImageButton).CommandName)
            WebUtils.RegistrarScript(Me.Page, "fechasConfiguracion", "fechasConfiguracion();")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' evento que se ejecuta se carga la lista de funcionarios, por cada registro del
    ''' repeater se asigna un identificador unico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpFuncionariosEjecucion_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpEncargados.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al carar el repeater de adjuntos, asigna atributos a cada elemento del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpAdjunto_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpAdjunto.ItemDataBound
        Dim vlo_IbBorrar As ImageButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibBorrar") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrar"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Al dar click en el botón guardar se almacenaran los datos indicados por el coordinador
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    ''' <remarks></remarks>
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Try
            If DatosValidos(1) Then
                If Guardar(False) Then
                    WebUtils.RegistrarScript(Me.Page, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
                Else
                    MostrarAlertaError("No ha sido posible guardar la información del registro")
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Al dar click en Guardar y Finalizar se guardaran los datos y se cambiara el estado para que la OT quede lista para ejecutarse. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnGuardarYFinalizar_Click(sender As Object, e As EventArgs) Handles btnGuardarYFinalizar.Click

        If DatosValidos(2) Then
            'Se cambia el estado de la orden para que quede registrada como evaluacion   Modificar() AndAlso
            'De esta forma se asegura que la proxima vez que entre tendrá que registrar los recursos para la ejecución


            If Guardar(True) Then
                WebUtils.RegistrarScript(Me.Page, "mostrarAlertaGuardadoExitoso", "mostrarAlertaGuardadoExitoso();")
            Else
                MostrarAlertaError("No ha sido posible actualizar la información")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al agregar un archivo adjunto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAgregarArchivo_Click(sender As Object, e As EventArgs) Handles btnAgregarArchivo.Click
        Try
            AgregarArchivo()
            Me.upRpAdjunto.Update()
            ActivarEvaluacion()

            Me.ddlTipoArchivo.SelectedValue = String.Empty
            Me.txtDescripcion.Text = String.Empty
            Me.trArchivosTipo.Visible = False
            ' WebUtils.RegistrarScript(Me.Page, "cargarExtensiones", "cargarExtensiones();")
            WebUtils.RegistrarScript(Me.Page, "fechasConfiguracion", "fechasConfiguracion();")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' descarga el archivo adjunto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnDescargarArchivo_Click(sender As Object, e As ImageClickEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(CType(sender, ImageButton).CommandArgument))(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CType(Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(CType(sender, ImageButton).CommandArgument))(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ARCHIVO), Byte()))
            Response.End()
            WebUtils.RegistrarScript(Me.Page, "fechasConfiguracion", "fechasConfiguracion();")
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar, ubicada en la 
    ''' primera columna de cada registro del listado de adjuntos, llama a la función borrar y muestra  un mensaje según 
    ''' la operacion efectuada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrarAdjunto_Click(sender As Object, e As ImageClickEventArgs)
        Dim vln_Indice As Integer

        Try

            vln_Indice = Convert.ToInt32(CType(sender, ImageButton).CommandName)
            Me.DsAdjuntosInsert.Tables(0).Rows(vln_Indice).Delete()

            If Me.DsAdjuntosInsert IsNot Nothing AndAlso Me.DsAdjuntosInsert.Tables(0).Rows.Count > 0 Then
                Me.rpAdjunto.DataSource = Me.DsAdjuntosInsert
                Me.rpAdjunto.DataMember = Me.DsAdjuntosInsert.Tables(0).TableName
                Me.rpAdjunto.DataBind()
            Else
                With Me.rpAdjunto
                    .DataSource = Nothing
                    .DataBind()
                End With
            End If
            WebUtils.RegistrarScript(Me.Page, "fechasConfiguracion", "fechasConfiguracion();")

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' descarga la imagen adjunta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>04/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub lnkArchivo_Command(sender As Object, e As CommandEventArgs)
        Try
            Response.Clear()
            Response.AppendHeader("content-disposition", "attachment; filename=" + Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.NOMBRE_ARCHIVO).ToString)
            Response.BinaryWrite(CType(Me.DsAdjuntosInsert.Tables(0).Rows(Convert.ToInt32(e.CommandArgument))(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ARCHIVO), Byte()))
            Response.End()
            WebUtils.RegistrarScript(Me.Page, "fechasConfiguracion", "fechasConfiguracion();")
        Catch ex_Descarga As System.Threading.ThreadAbortException
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Redirige al listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>4/03/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Response.Redirect(Me.PaginaRegresar)
    End Sub

#End Region

#Region "Metodos"
    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Activa la pestaña de ficha de evaluacion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ActivarEvaluacion()
        WebUtils.RegistrarScript(Me, "ActivarEvaluacion()", "ActivarEvaluacion();")
    End Sub

    ''' <summary>
    ''' Activa la pestaña de evaluador
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ActivarEvaluador()
        WebUtils.RegistrarScript(Me, "ActivarEvaluadores", "ActivarEvaluadores();")
    End Sub

    ''' <summary>
    ''' Inicializa el formulario y sus componentes
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.PaginaRegresar = "Lst_OT_GestionProfesionalesDisenio.aspx"
        CargarTamañoMaximoArchivo()
        Me.Usuario = New UsuarioActual()
        LeerParametros()
        InicializarControlUsuario()
        CargarOrdenTrabajo(Me.IdUbicacion, Me.IdOrdenTrabajo)
        CargaDsAdjuntos()
        inicializarSetDatos()
        CargarListaFuncionarios(String.Format("{0} = '{1}'", Modelo.OTF_OPERARIO_AREA.CATEGORIA_LABORAL, Area.PROFESIONAL), String.Empty, 1)
        CargarUnidades(String.Format("{0} = '{1}'", Modelo.V_OTM_UNIDAD_TIEMPOLST.ESTADO, Estado.ACTIVO), String.Empty, 1)
        CargarListaTipoArchivo(String.Format("{0} = '{1}' AND {2} = {3}", Modelo.OTM_TIPO_DOCUMENTO.ESTADO, Estado.ACTIVO, Modelo.OTM_TIPO_DOCUMENTO.PROTEGIDO, Proteccion.NO_PROTEGIDO))
        CargarOperariosActuales()
        CargarViabilidadTecnica()
        ActivarEvaluador()

        If Me.Operacion = eOperacion.Consultar Then
            Me.PaginaRegresar = WebUtils.LeerParametro(Of String)("pvn_Regresar")
            WebUtils.RegistrarScript(Me, "deshabilitar", "deshabilitar();")
        End If

    End Sub

    Private Sub LeerParametros()
        Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
        Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
        Me.Anno = WebUtils.LeerParametro(Of Integer)("pvn_Anno")
        Me.ProfesionalEncargado = CargarFuncionario(WebUtils.LeerParametro(Of String)("pvn_IdEncargado")).ID_PERSONAL
        Me.IdSectorTaller = WebUtils.LeerParametro(Of Integer)("pvn_IdSectorTaller")
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")

    End Sub

    ''' <summary>
    ''' Inicializa el control de usuario para mostrar la información general
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/02/2016</creationDate>
    Private Sub InicializarControlUsuario()
        'Asignación de Datos para generar el web user control de la información general


        ctrl_InfoGeneral.Anno = Me.Anno
        ctrl_InfoGeneral.IdOrdenTrabajo = Me.IdOrdenTrabajo
        ctrl_InfoGeneral.IdUbicacion = Me.IdUbicacion

        'Manda  llamar al control de información general de la OT
        Me.ctrl_InfoGeneral.Inicializar()
        Me.upControlOrdenTrabajo.Update()

    End Sub

    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>10/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub inicializarSetDatos()
        Dim vlo_columna As DataColumn
        Dim vlo_llaves(1) As DataColumn


        'Se crea un nuevo datatabla 
        Me.DsEvaluadores = New DataTable

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA
        'Se agrega la columna configurada al set de datos
        DsEvaluadores.Columns.Add(vlo_columna)
        'Se agrega al arreglo de llaves primarias la columna
        vlo_llaves(0) = vlo_columna
        'Y se configura el set de datos para que busque por formatos admitidos como llave primaria.
        Me.DsEvaluadores.PrimaryKey = vlo_llaves

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO
        'Se agrega la columna configurada al set de datos
        DsEvaluadores.Columns.Add(vlo_columna)

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA
        'Se agrega la columna configurada al set de datos
        DsEvaluadores.Columns.Add(vlo_columna)

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.DateTime")
        'Se le da nombre a esta columna        
        vlo_columna.ColumnName = Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_EJECUTA
        'Se agrega la columna configurada al set de datos
        DsEvaluadores.Columns.Add(vlo_columna)

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.String")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_TIEMPO_REAL
        'Se agrega la columna configurada al set de datos
        DsEvaluadores.Columns.Add(vlo_columna)

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.Int32")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_REAL
        'Se agrega la columna configurada al set de datos
        DsEvaluadores.Columns.Add(vlo_columna)

        vlo_columna = New DataColumn()
        'Se asigna un tipo de dato a la columna recientemente creada
        vlo_columna.DataType = System.Type.GetType("System.Int32")
        'Se le da nombre a esta columna
        vlo_columna.ColumnName = Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_REAL
        'Se agrega la columna configurada al set de datos
        DsEvaluadores.Columns.Add(vlo_columna)

    End Sub

    ''' <summary>
    ''' Carga la lista de operarios junto con el coordinador principal y el sustituto(si existe)
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaFuncionarios(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlFuncionario.Items.Clear()
            Me.ddlFuncionario.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            'Se carga la lista de operarios que posee el actual sector o taller
            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTF_OPERARIO_AREA_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then

                For Each vlo_fila As DataRow In vlo_DsDatos.Tables(0).Rows
                    Me.ddlFuncionario.Items.Add(New ListItem(vlo_fila(Modelo.V_OTF_OPERARIO_AREALST.NOMBRE_EMPLEADO), vlo_fila(Modelo.V_OTF_OPERARIO_AREALST.CEDULA)))
                Next

                Me.ddlFuncionario.DataBind()
            Else
                With Me.ddlFuncionario
                    .DataSource = Nothing
                    .DataBind()
                End With

            End If
            Me.DsEvaluadoresProf = vlo_DsDatos
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
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">id de la ubicacion</param>
    ''' <param name="pvn_IdOrdenTrabajo">id de la orden</param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarOrdenTrabajo(pvn_IdUbicacion As Integer, pvn_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.OrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvn_IdOrdenTrabajo))

            Me.txtNombreProyecto.Text = IIf(Me.OrdenTrabajo.NombreProyecto = "-", String.Empty, Me.OrdenTrabajo.NombreProyecto)

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub


    ''' <summary>
    ''' Carga la lista de unidades de tiempo
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <param name="pvc_Orden"></param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>11/02/2016</creationDate>
    Private Sub CargarUnidades(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.ddlUnidadTiempoInvertido.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_UNIDAD_TIEMPO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, pvc_Orden, True, pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                'ddl de unidad de tiempo invertido en la evaluacion
                With Me.ddlUnidadTiempoInvertido
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.V_OTM_UNIDAD_TIEMPOLST.DESCRIPCION
                    .DataValueField = Modelo.V_OTM_UNIDAD_TIEMPOLST.ID_UNIDAD_TIEMPO
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
    ''' Carga la lista de tipos de documento con la condicion especificada.
    ''' </summary>
    ''' <param name="pvc_Condicion"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>12/02/2016</creationDate>
    Private Sub CargarListaTipoArchivo(pvc_Condicion As String)
        Dim vlo_Ws_OT_Catalogos As Ws_OT_Catalogos
        Dim vlo_DsDatos As DataSet

        vlo_Ws_OT_Catalogos = New Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.ddlTipoArchivo.Items.Add(New ListItem(Constantes.FORMATO_DDL_TODOS, String.Empty))

            vlo_DsDatos = vlo_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion, String.Empty, True, 1,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If vlo_DsDatos.Tables.Count > 0 AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                'drop down list de tipos de documento
                With Me.ddlTipoArchivo
                    .DataSource = vlo_DsDatos
                    .DataMember = vlo_DsDatos.Tables(0).TableName
                    .DataTextField = Modelo.OTM_TIPO_DOCUMENTO.DESCRIPCION
                    .DataValueField = Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO
                    .DataBind()
                End With

                Me.DsTipoArchivo = vlo_DsDatos
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
    ''' agrega un funcionario al dataset, estos se encuentran en memoria, y son insertados en la 
    ''' base de datos hasta el final, es decir un vez que se da click sobre el boton de aceptar
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregaFuncionariosDataTable()
        Dim vlo_DrNuevaFila As DataRow

        Try

            Dim vln_Cedula As Integer
            vln_Cedula = CType(Me.ddlFuncionario.SelectedValue, Integer)


            If Me.DsEvaluadores.Rows.Find(New Object() {vln_Cedula}) Is Nothing Then

                'Se recorre la lista de todos los profesionales disponibles para agregar el correspondiente
                For Each vlo_DrProfesional As DataRow In DsEvaluadoresProf.Tables(0).Rows
                    If String.Compare(vlo_DrProfesional(Modelo.V_OTF_OPERARIO_AREALST.CEDULA), Me.ddlFuncionario.SelectedValue) = 0 Then
                        vlo_DrNuevaFila = Me.DsEvaluadores.NewRow

                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA) = vlo_DrProfesional(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO) = vlo_DrProfesional(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA) = vlo_DrProfesional(Modelo.V_OTF_OPERARIO_AREALST.AREA)
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_EJECUTA) = Me.txtDPFechaEvaluacionRealizada.Text
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_TIEMPO_REAL) = String.Format("{0} {1}", Me.txtTiempoInvertidoEvaluacion.Text, Me.ddlUnidadTiempoInvertido.SelectedItem)
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_REAL) = CInt(Me.txtTiempoInvertidoEvaluacion.Text)
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_REAL) = CInt(Me.ddlUnidadTiempoInvertido.SelectedValue)
                        Me.DsEvaluadores.Rows.Add(vlo_DrNuevaFila)
                    End If
                Next

                If Me.DsEvaluadores IsNot Nothing AndAlso Me.DsEvaluadores.Rows.Count > 0 Then
                    Me.rpEncargados.DataSource = DsEvaluadores
                    Me.rpEncargados.DataMember = Me.DsEvaluadores.TableName
                    Me.rpEncargados.DataBind()
                    Me.rpEncargados.Visible = True
                Else
                    With Me.rpEncargados
                        .DataSource = Nothing
                        .DataBind()
                    End With
                    Me.rpEncargados.Visible = False
                End If

                Me.ddlFuncionario.SelectedValue = String.Empty
                Me.lblAreaProfesional.Text = String.Empty
                Me.txtDPFechaEvaluacionRealizada.Text = String.Empty
                Me.txtTiempoInvertidoEvaluacion.Text = String.Empty
                Me.ddlUnidadTiempoInvertido.SelectedValue = String.Empty

            Else
                MostrarAlertaError("El evaluador ya está presente en la lista")
            End If


        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' borra un registro del dataset temporal de profesionales
    ''' </summary>
    ''' <param name="pvc_CommandName"></param>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub BorrarFuncionario(pvc_CommandName As String)
        Try
            'Este if en caso de que se borren funcionarios y la tabla quede vacia
            If DsEvaluadores.Rows.Count <= 0 Then
                Me.rfvddlFuncionario.Enabled = True
                Me.rfvDdlUnidad.Enabled = False
                Me.rfvddlFuncionario.Enabled = False
                Me.rvfTxtTiempoEstimado.Enabled = False
            End If

            Me.DsEvaluadores.Rows.Find(New Object() {pvc_CommandName}).Delete()


            If Me.DsEvaluadores IsNot Nothing AndAlso Me.DsEvaluadores.Rows.Count > 0 Then
                Me.rpEncargados.DataSource = Me.DsEvaluadores
                Me.rpEncargados.DataMember = Me.DsEvaluadores.TableName
                Me.rpEncargados.DataBind()
                Me.rpEncargados.Visible = True
            Else
                With Me.rpEncargados
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpEncargados.Visible = False
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Agrega un nuevo adjunto al dataset 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub AgregarArchivo()
        Dim vlo_DrFila As Data.DataRow

        Try
            If Me.DsAdjuntosInsert.Tables(0).Rows.Count < 2 Then
                vlo_DrFila = Me.DsAdjuntosInsert.Tables(0).NewRow
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.NOMBRE_ARCHIVO)) = Me.ifInfo.FileName
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESCRIPCION)) = Me.txtDescripcion.Text
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ARCHIVO)) = Me.ifInfo.FileBytes
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.USUARIO)) = Me.Usuario.UserName
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_TIPO_DOCUMENTO)) = Me.ddlTipoArchivo.SelectedValue
                vlo_DrFila.Item(Me.DsAdjuntosInsert.Tables(0).Columns(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESC_TIPO_DOCUMENTO)) = Me.ddlTipoArchivo.SelectedItem
                Me.DsAdjuntosInsert.Tables(0).Rows.Add(vlo_DrFila)

                If Me.DsAdjuntosInsert IsNot Nothing AndAlso Me.DsAdjuntosInsert.Tables(0).Rows.Count > 0 Then
                    Me.rpAdjunto.DataSource = Me.DsAdjuntosInsert
                    Me.rpAdjunto.DataMember = Me.DsAdjuntosInsert.Tables(0).TableName
                    Me.rpAdjunto.DataBind()
                Else
                    With Me.rpAdjunto
                        .DataSource = Nothing
                        .DataBind()
                    End With
                End If
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Carga los archivos adjuntos insertados por el usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>12/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargaDsAdjuntos()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Dim condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}'",
                              Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA,
            Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_UBICACION, Me.IdUbicacion,
            Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO, Me.IdOrdenTrabajo)

            Me.DsAdjuntosInsert = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ADJUNTO_ORDEN_TRABAJO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                condicion,
                String.Empty,
                False,
                0,
                0)

            If Me.DsAdjuntosInsert IsNot Nothing AndAlso Me.DsAdjuntosInsert.Tables(0).Rows.Count > 0 Then
                Me.rpAdjunto.DataSource = Me.DsAdjuntosInsert
                Me.rpAdjunto.DataMember = Me.DsAdjuntosInsert.Tables(0).TableName
                Me.rpAdjunto.DataBind()
            Else
                With Me.rpAdjunto
                    .DataSource = Nothing
                    .DataBind()
                End With
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
    ''' Ejecuta la funcion de continuar
    ''' </summary>
    ''' <param name="pvc_Mensaje"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub mostrarPopupConfirmaDeseaContinuar1(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "mostrarPopupConfirmaDeseaContinuar1", String.Format("mostrarPopupConfirmaDeseaContinuar1('{0}');", pvc_Mensaje))
    End Sub

    Private Sub mostrarPopupConfirmaDeseaContinuar2(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "mostrarPopupConfirmaDeseaContinuar2", String.Format("mostrarPopupConfirmaDeseaContinuar2('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Carga el tamaño maximo permito
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>12/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarTamañoMaximoArchivo()
        Dim vlo_Ws_OT_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmTipoDocumento As Wsr_OT_Catalogos.EntOtmTipoDocumento

        vlo_Ws_OT_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_EntOtmTipoDocumento = vlo_Ws_OT_Ws_OT_Catalogos.OTM_TIPO_DOCUMENTO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO, TipoDocumento.FOTOGRAFIA))

            Me.TamanoArchivo = vlo_EntOtmTipoDocumento.TamanioMaximo
            Me.ExtensionesPermitidas = vlo_EntOtmTipoDocumento.FormatosAdmitidos
            imgExtensiones.Attributes.Add("title", ExtensionesPermitidas)

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
    ''' Administra el proceso para modificar una orden de trabajo
    ''' </summary>
    ''' <returns></returns>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Modificar() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ModificarRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.OrdenTrabajo) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Almacena los valores ingresados por el usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez</author>
    ''' <creationDate>11/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Guardar(pvb_CambiarEstado As Boolean) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        'Dim vlo_fechaefectuo As Date
        Dim vlc_textoEnriquecido As String
        Dim vlb_EsViable As Boolean
        Dim vln_Estimacion As Integer
        'Dim vln_TiempoInvertido As Integer


        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlb_EsViable = IIf(Me.rbtnViabilidad.SelectedValue = 1, True, False)
            vlc_textoEnriquecido = String.Empty

            If Me.hdnNicEdit.Value.Trim <> String.Empty Then
                vlc_textoEnriquecido = HttpUtility.HtmlEncode(Me.hdnNicEdit.Value)
            Else
                MostrarAlertaError("El Detalle del campo de texto enriquecido no puede ser vacío.")
                Return False
            End If


            If String.IsNullOrWhiteSpace(Me.txtEstimacionPres.Text.Trim) Then
                vln_Estimacion = Nothing
            Else
                vln_Estimacion = CInt(Me.txtEstimacionPres.Text)
            End If

            Return vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_GuardarViabilidadTecnica(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                Me.DsEvaluadores, Me.DsAdjuntosInsert.Tables(0), IdUbicacion, IdOrdenTrabajo,
                vlb_EsViable, vln_Estimacion, vlc_textoEnriquecido, Usuario.UserName, pvb_CambiarEstado, OrdenTrabajo)

            Return True
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Carga la tabla de evaluadores
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarViabilidadTecnica()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttViabilidadTecnica As EntOttViabilidadTecnica


        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            'Carga la lista de funcionarios actuales en el repeater que tengan tiempo real
            vlo_EntOttViabilidadTecnica = vlo_Ws_OT_OrdenesDeTrabajo.OTT_VIABILIDAD_TECNICA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}'",
                Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION,
                IdUbicacion,
                Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO,
                IdOrdenTrabajo))
            If vlo_EntOttViabilidadTecnica IsNot Nothing Then
                Me.rbtnViabilidad.SelectedValue = vlo_EntOttViabilidadTecnica.Viabilidad
                Me.txtEstimacionPres.Text = vlo_EntOttViabilidadTecnica.EstimacionPresupuestaria
                Me.hdnNicEdit.Value = HttpUtility.HtmlDecode(vlo_EntOttViabilidadTecnica.Detalle)
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
    ''' Carga la tabla de evaluadores
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Cesar Bermudez</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarOperariosActuales() As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlo_filaNueva As DataRow

        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            'Carga la lista de funcionarios actuales en el repeater que tengan tiempo real
            Me.DsEvaluadoresActual = vlo_Ws_OT_OrdenesDeTrabajo.OTT_OPERARIO_ORDEN_TRAB_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'",
                Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION,
                IdUbicacion,
                Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO,
                IdOrdenTrabajo,
                Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_REAL_CLAS,
                Clasificacion.REAL), String.Empty, False, 1, 0)

            If DsEvaluadoresActual.Tables.Count > 0 AndAlso DsEvaluadoresActual.Tables(0).Rows.Count > 0 Then
                'Se actualiza la tabla en memoria con los datos obtenidos

                For Each vlo_fila As DataRow In DsEvaluadoresActual.Tables(0).Rows
                    vlo_filaNueva = Me.DsEvaluadores.NewRow()
                    vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)
                    vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)
                    vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_AREA)
                    vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_TIEMPO_REAL) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.DESC_TIEMPO_REAL)
                    vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_EJECUTA) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_EJECUTA)
                    vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_REAL) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_REAL)
                    vlo_filaNueva(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_REAL) = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_REAL)
                    Me.DsEvaluadores.Rows.Add(vlo_filaNueva)
                Next

                Me.rpEncargados.DataSource = DsEvaluadores
                Me.rpEncargados.DataMember = DsEvaluadores.TableName
                Me.rpEncargados.DataBind()
                Me.rpEncargados.Visible = True

                Me.txtDPFechaEvaluacionRealizada.Text = DsEvaluadores.Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_EJECUTA).ToString
                Return True

            Else
                With Me.rpEncargados
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpEncargados.Visible = False
            End If


            Return False

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
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DatosValidos(pvn_Tipo As Integer) As Boolean
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Ws_OT_OrdenesDeTrabajo
        Dim vlb_esValido As Boolean
        Dim vlo_obtenerFilas() As DataRow


        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
        Try
            If Request("__EVENTARGUMENT") = "true" Then
                Return True
            End If

            If Me.DsEvaluadores.Rows.Find(New Object() {Me.ProfesionalEncargado}) IsNot Nothing Then
                vlb_esValido = True
            Else
                MostrarAlertaError("Debe asignar el tiempo real invertido por el profesional a cargo")
                vlb_esValido = False
            End If

            For Each vlo_fila As DataRow In Me.DsEvaluadoresActual.Tables(0).Rows

                vlo_obtenerFilas = DsEvaluadores.Select(String.Format("{0} = {1}",
                                                            Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA, vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)))
                If vlo_obtenerFilas.Length > 0 AndAlso (vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA) <> Me.ProfesionalEncargado) Then

                    If pvn_Tipo = 0 Then
                        mostrarPopupConfirmaDeseaContinuar1(String.Format("El funcionario {0}, propuesto como acompañante para la evaluación no ha sido incluído como evaluador, ¿Desea Continuar?", vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)))
                    Else
                        mostrarPopupConfirmaDeseaContinuar2(String.Format("El funcionario {0}, propuesto como acompañante para la evaluación no ha sido incluído como evaluador, ¿Desea Continuar?", vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)))
                    End If

                    vlb_esValido = False

                End If
            Next

            For Each vlo_fila As DataRow In Me.DsEvaluadores.Rows
                If DsEvaluadoresActual.Tables.Count > 0 AndAlso DsEvaluadoresActual.Tables(0).Rows.Count > 0 Then
                    vlo_obtenerFilas = DsEvaluadoresActual.Tables(0).Select(String.Format("{0} = {1}",
                                                            Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA, vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)))
                    If vlo_obtenerFilas.Length <= 0 AndAlso (vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA) <> Me.ProfesionalEncargado) Then

                        If pvn_Tipo = 0 Then
                            mostrarPopupConfirmaDeseaContinuar1(String.Format("El funcionario {0}, no había sido propuesto como acompañante para la evaluación, ¿Desea Continuar?", vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)))
                        Else
                            mostrarPopupConfirmaDeseaContinuar2(String.Format("El funcionario {0}, no había sido propuesto como acompañante para la evaluación, ¿Desea Continuar?", vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO)))
                        End If

                        vlb_esValido = False

                    End If
                End If
            Next


            Return vlb_esValido
        Catch ex As Exception

        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>15/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Function CargarFuncionario(pvn_NumEmpleado As String) As WsrEU_Curriculo.EntEmpleados
        Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

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

#End Region

End Class
