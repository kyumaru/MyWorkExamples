Imports Utilerias.OrdenesDeTrabajo
Imports Wsr_OT_Catalogos
Imports System.Data

Partial Class Catalogos_Almacen_Frm_OT_Proveedores
    Inherits System.Web.UI.Page
#Region "Propiedades"
    ''' <summary>
    ''' Propiedad para la ultima expresion de ordenamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
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
    ''' <creationDate>23/05/2016</creationDate>
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
    ''' <creationDate>23/05/2016</creationDate>
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
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property Operacion As eOperacion
        Get
            Return CType(ViewState("Operacion"), eOperacion)
        End Get
        Set(value As eOperacion)
            ViewState("Operacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la operacion a realizar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Identificacion As String
        Get
            Return CType(ViewState("Identificacion"), String)
        End Get
        Set(value As String)
            ViewState("Identificacion") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
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
    ''' Propiedad para el Proveedor a trabajar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>JMCR</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Proveedor As Wsr_OT_Catalogos.EntOtmProveedor
        Get
            Return CType(ViewState("Proveedor"), Wsr_OT_Catalogos.EntOtmProveedor)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmProveedor)
            ViewState("Proveedor") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el Proveedor a trabajar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>JMCR</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property CorreoProveedor As Wsr_OT_Catalogos.EntOtmCorreoProveedor
        Get
            Return CType(ViewState("CorreoProveedor"), Wsr_OT_Catalogos.EntOtmCorreoProveedor)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmCorreoProveedor)
            ViewState("CorreoProveedor") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el Proveedor a trabajar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>JMCR</author>
    ''' <creationDate>05/08/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property TelefonoProveedor As Wsr_OT_Catalogos.EntOtmTelefonoProveedor
        Get
            Return CType(ViewState("TelefonoProveedor"), Wsr_OT_Catalogos.EntOtmTelefonoProveedor)
        End Get
        Set(value As Wsr_OT_Catalogos.EntOtmTelefonoProveedor)
            ViewState("TelefonoProveedor") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property CantidadTelefonos As Integer
        Get
            Return ViewState("CantidadTelefonos")
        End Get
        Set(value As Integer)
            ViewState("CantidadTelefonos") = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property CantidadCorreos As Integer
        Get
            Return ViewState("CantidadCorreos")
        End Get
        Set(value As Integer)
            ViewState("CantidadCorreos") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property DsTelefonos As DataSet
        Get
            Return ViewState("DsTelefonos")
        End Get
        Set(value As DataSet)
            ViewState("DsTelefonos") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property DsCorreos As DataSet
        Get
            Return ViewState("DsCorreos")
        End Get
        Set(value As DataSet)
            ViewState("DsCorreos") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property telefonoModificar As String
        Get
            Return ViewState("telefonoModificar")
        End Get
        Set(value As String)
            ViewState("telefonoModificar") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property CorreoModificar As String
        Get
            Return ViewState("CorreoModificar")
        End Get
        Set(value As String)
            ViewState("CorreoModificar") = value
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
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                InicializarFormulario()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
        WebUtils.RegistrarScript(Me, "BotonBorrar", "AgregarImagenBorrar();")
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click en el boton aceptar para agregar un nuevo registro
    ''' llama a la funcion procesar y muestra un mensaje segun la operacion realizada
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Page.IsValid Then
            Try
                If CantidadCorreos = 0 Then
                    MostrarAlertaError("Debe indicar al menos un correo electrónico")
                Else
                    If CantidadTelefonos = 0 Then
                        MostrarAlertaError("Debe indicar al menos un teléfono")
                    Else
                        Select Case (Me.Operacion)
                            Case Is = eOperacion.Agregar
                                If Aceptar(True) Then
                                    WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarPopupRegistroExitoso();")
                                Else
                                    MostrarAlertaError("No ha sido posible almacenar la información del nuevo registro")
                                End If

                            Case Is = eOperacion.Modificar
                                If Aceptar(False) Then
                                    WebUtils.RegistrarScript(Me.Page, "MostrarAlertaActualizacionExitosa", "mostrarAlertaActualizacionExitosa();")
                                Else
                                    MostrarAlertaError("No ha sido posible actualizar la información del registro")
                                End If
                        End Select
                    End If
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

    ''' <summary>
    ''' Evento que se ejecuta al carar el repeater del listado, asigna atributos a cada elemento del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpTelefono_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpTelefono.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        Dim vlo_IbModificar As ImageButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibBorrarTelefono") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrarTelefono"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If

            If e.Item.FindControl("ibModificarTelefono") IsNot Nothing Then
                vlo_IbModificar = CType(e.Item.FindControl("ibModificarTelefono"), ImageButton)
                vlo_IbModificar.Attributes.Add("data-uniqueid", vlo_IbModificar.UniqueID)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al carar el repeater del listado, asigna atributos a cada elemento del listado
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub rpCorreo_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rpCorreo.ItemDataBound
        Dim vlo_IbBorrar As ImageButton
        Dim vlo_IbModificar As ImageButton

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If e.Item.FindControl("ibBorrarCorreo") IsNot Nothing Then
                vlo_IbBorrar = CType(e.Item.FindControl("ibBorrarCorreo"), ImageButton)
                vlo_IbBorrar.Attributes.Add("data-uniqueid", vlo_IbBorrar.UniqueID)
            End If

            If e.Item.FindControl("ibModificarCorreo") IsNot Nothing Then
                vlo_IbModificar = CType(e.Item.FindControl("ibModificarCorreo"), ImageButton)
                vlo_IbModificar.Attributes.Add("data-uniqueid", vlo_IbModificar.UniqueID)
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
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrarTelefono_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_IbBorrar As ImageButton
        Dim vlc_Identificador As String
        Dim vlc_Telefono As String
        Dim vlc_Cadena As String
        Dim vlo_Lista As String()
        Try
            If CantidadTelefonos = 1 Then
                MostrarAlertaError("El proveedor debe tener al menos un teléfono")
            Else

                vlo_IbBorrar = CType(sender, ImageButton)
                vlc_Cadena = vlo_IbBorrar.CommandArgument
                vlo_Lista = vlc_Cadena.Split("¬")
                vlc_Identificador = vlo_Lista(0)
                vlc_Telefono = vlo_Lista(1)

                If BorrarTelefono(vlc_Telefono) Then
                    LlenarRepeaterTelefono(Utilerias.OrdenesDeTrabajo.Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO)

                    Me.btnAgregaTelefonor.Visible = True
                    Me.btnModificaTelefono.Visible = False
                    Me.btnCancelarTelefono.Visible = False
                Else
                    MostrarAlertaError("El responsable no fue borrado")
                End If
            End If
            WebUtils.RegistrarScript(Me, "BotonBorrar", "AgregarImagenBorrar();")
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
    ''' Evento que se ejecuta cuando se da click sobre la figura de borrar, ubicada en la 
    ''' segunda columna de cada registro del listado, llama a la función borrar y muestra  un mensaje según 
    ''' la operacion efectuada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibModificarTelefono_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_IbModificar As ImageButton
        Dim vlc_Identificador As String
        Dim vlc_Telefono As String
        Dim vlc_Cadena As String
        Dim vlo_Lista As String()
        Try
            vlo_IbModificar = CType(sender, ImageButton)
            vlc_Cadena = vlo_IbModificar.CommandArgument
            vlo_Lista = vlc_Cadena.Split("¬")
            vlc_Identificador = vlo_Lista(0)
            vlc_Telefono = vlo_Lista(1)
            If Not String.IsNullOrWhiteSpace(vlc_Identificador) And Not String.IsNullOrWhiteSpace(vlc_Telefono) Then
                Me.txtTelefono.Text = vlc_Telefono
                Me.telefonoModificar = vlc_Telefono

                Me.btnAgregaTelefonor.Visible = False
                Me.btnModificaTelefono.Visible = True
                Me.btnCancelarTelefono.Visible = True
            Else
                MostrarAlertaError("Los datos no se cargaron correctamente, por favor intentelo nuevamente")
            End If
            LlenarRepeaterTelefono(Utilerias.OrdenesDeTrabajo.Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO)
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

    Protected Sub btnAgregaTelefono_Click(sender As Object, e As EventArgs) Handles btnAgregaTelefonor.Click
        Try
            If Operacion = eOperacion.Agregar Then
                If Not String.IsNullOrWhiteSpace(Me.txtCedula.Text) Then
                    Me.Identificacion = Me.txtCedula.Text
                    If AgregarDsTelefono(Me.Identificacion, txtTelefono.Text) Then
                        'WebUtils.RegistrarScript(Me.Page, "OcultarPopUpUnidad", "javascript:ocultarPopUpFondo();AgregarImagenBorrar();")
                        LlenarRepeaterTelefono(Utilerias.OrdenesDeTrabajo.Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO)
                        Me.txtTelefono.Text = String.Empty

                        Me.btnAgregaTelefonor.Visible = True
                        Me.btnModificaTelefono.Visible = False
                        Me.btnCancelarTelefono.Visible = False
                    Else
                        MostrarAlertaError("El Teléfono no se pudo agregar")
                    End If
                Else
                    MostrarAlertaError("Debe Agregar la cédula")
                End If
            Else
                If AgregarDsTelefono(Me.Identificacion, txtTelefono.Text) Then
                    'WebUtils.RegistrarScript(Me.Page, "OcultarPopUpUnidad", "javascript:ocultarPopUpFondo();AgregarImagenBorrar();")
                    LlenarRepeaterTelefono(Utilerias.OrdenesDeTrabajo.Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO)

                    Me.btnAgregaTelefonor.Visible = True
                    Me.btnModificaTelefono.Visible = False
                    Me.btnCancelarTelefono.Visible = False
                Else
                    MostrarAlertaError("El Teléfono no se pudo agregar")
                End If
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

    Protected Sub btnModificaTelefono_Click(sender As Object, e As EventArgs) Handles btnModificaTelefono.Click
        Try

            If Not String.IsNullOrWhiteSpace(txtCedula.Text) And Not String.IsNullOrWhiteSpace(txtTelefono.Text) And Not String.IsNullOrWhiteSpace(telefonoModificar) Then
                If BorrarTelefono(telefonoModificar) Then
                    If AgregarDsTelefono(Me.txtCedula.Text, txtTelefono.Text) Then
                        Me.txtTelefono.Text = String.Empty

                        Me.btnAgregaTelefonor.Visible = True
                        Me.btnModificaTelefono.Visible = False
                        Me.btnCancelarTelefono.Visible = False
                    Else
                        MostrarAlertaError("El Teléfono no se pudo modificar")
                    End If
                Else
                    MostrarAlertaError("El Teléfono no se pudo modificar")
                End If
            Else
                MostrarAlertaError("Los datos no se cargaron correctamente, por favor intentelo nuevamente")
            End If
            LlenarRepeaterTelefono(Utilerias.OrdenesDeTrabajo.Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO)
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


    Protected Sub btnAgregarCorreo_Click(sender As Object, e As EventArgs) Handles btnAgregarCorreo.Click
        Try
            If Operacion = eOperacion.Agregar Then
                If Not String.IsNullOrWhiteSpace(Me.txtCedula.Text) Then
                    Me.Identificacion = Me.txtCedula.Text
                    If AgregarDsCorreo(Me.txtCedula.Text, txtCorreo.Text, txtNombreCorreo.Text) Then
                        'WebUtils.RegistrarScript(Me.Page, "OcultarPopUpUnidad", "javascript:ocultarPopUpFondo();AgregarImagenBorrar();")
                        LlenarRepeaterCorreos(Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.NOMBRE)
                        Me.txtNombreCorreo.Text = String.Empty
                        Me.txtCorreo.Text = String.Empty

                        Me.btnAgregarCorreo.Visible = True
                        Me.btnModificarCorreo.Visible = False
                        Me.btnCancelarCorreo.Visible = False
                    Else
                        MostrarAlertaError("El Correo no se pudo agregar")
                    End If
                Else
                    MostrarAlertaError("Debe Agregar la cédula")
                End If
            Else
                If AgregarDsCorreo(Me.txtCedula.Text, txtCorreo.Text, txtNombreCorreo.Text) Then
                    'WebUtils.RegistrarScript(Me.Page, "OcultarPopUpUnidad", "javascript:ocultarPopUpFondo();AgregarImagenBorrar();")
                    LlenarRepeaterCorreos(Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.NOMBRE)

                    Me.btnAgregarCorreo.Visible = True
                    Me.btnModificarCorreo.Visible = False
                    Me.btnCancelarCorreo.Visible = False
                Else
                    MostrarAlertaError("El Correo no se pudo agregar")
                End If
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

    Protected Sub btnModificarCorreo_Click(sender As Object, e As EventArgs) Handles btnModificarCorreo.Click
        Try

            If Not String.IsNullOrWhiteSpace(txtCedula.Text) And Not String.IsNullOrWhiteSpace(txtNombreCorreo.Text) And Not String.IsNullOrWhiteSpace(txtCorreo.Text) And Not String.IsNullOrWhiteSpace(CorreoModificar) Then
                If BorrarCorreo(CorreoModificar) Then
                    If AgregarDsCorreo(Me.txtCedula.Text, txtCorreo.Text, txtNombreCorreo.Text) Then
                        Me.txtNombreCorreo.Text = String.Empty
                        Me.txtCorreo.Text = String.Empty

                        Me.btnAgregarCorreo.Visible = True
                        Me.btnModificarCorreo.Visible = False
                        Me.btnCancelarCorreo.Visible = False
                    Else
                        MostrarAlertaError("El Teléfono no se pudo modificar")
                    End If
                Else
                    MostrarAlertaError("El Teléfono no se pudo modificar")
                End If
            Else
                MostrarAlertaError("Los datos no se cargaron correctamente, por favor intentelo nuevamente")
            End If
            LlenarRepeaterCorreos(Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.NOMBRE)
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

    Protected Sub btnCancelarTelefono_Click(sender As Object, e As EventArgs) Handles btnCancelarTelefono.Click
        Me.txtTelefono.Text = String.Empty

        Me.btnAgregaTelefonor.Visible = True
        Me.btnModificaTelefono.Visible = False
        Me.btnCancelarTelefono.Visible = False
    End Sub

    Protected Sub btnCancelarCorreo_Click(sender As Object, e As EventArgs) Handles btnCancelarCorreo.Click
        Me.txtNombreCorreo.Text = String.Empty
        Me.txtCorreo.Text = String.Empty
        Me.btnAgregarCorreo.Visible = True
        Me.btnModificarCorreo.Visible = False
        Me.btnCancelarCorreo.Visible = False
    End Sub


    Protected Sub ibModificarCorreo_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_IbModificar As ImageButton
        Dim vlc_Identificador As String
        Dim vlc_Nombre As String
        Dim vlc_Correo As String
        Dim vlc_Cadena As String
        Dim vlo_Lista As String()
        Try
            vlo_IbModificar = CType(sender, ImageButton)
            vlc_Cadena = vlo_IbModificar.CommandArgument
            vlo_Lista = vlc_Cadena.Split("¬")
            vlc_Identificador = vlo_Lista(0).ToString
            vlc_Correo = vlo_Lista(1)
            vlc_Nombre = vlo_Lista(2)
            If Not String.IsNullOrWhiteSpace(vlc_Identificador) And Not String.IsNullOrWhiteSpace(vlc_Correo) And Not String.IsNullOrWhiteSpace(vlc_Nombre) Then
                Me.txtNombreCorreo.Text = vlc_Nombre
                Me.txtCorreo.Text = vlc_Correo
                Me.CorreoModificar = vlc_Correo

                Me.btnAgregarCorreo.Visible = False
                Me.btnModificarCorreo.Visible = True
                Me.btnCancelarCorreo.Visible = True
            Else
                MostrarAlertaError("Los datos no se cargarón correctamente, por favor intentelo nuevamente")
            End If
            LlenarRepeaterCorreos(Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.NOMBRE)
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

    Protected Sub ibBorrarCorreo_Click(sender As Object, e As ImageClickEventArgs)
        Dim vlo_IbBorrar As ImageButton
        Dim vlc_Identificador As String
        Dim vlc_Correo As String
        Dim vlc_Cadena As String
        Dim vlo_Lista As String()
        Try
            If CantidadCorreos = 1 Then
                MostrarAlertaError("El proveedor debe tener al menos un correo")
            Else

                vlo_IbBorrar = CType(sender, ImageButton)
                vlc_Cadena = vlo_IbBorrar.CommandArgument
                vlo_Lista = vlc_Cadena.Split("¬")
                vlc_Identificador = vlo_Lista(0)
                vlc_Correo = vlo_Lista(1)

                If BorrarCorreo(vlc_Correo) Then
                    LlenarRepeaterCorreos(Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.NOMBRE)
                Else
                    MostrarAlertaError("El Correo no fue borrado")
                End If
            End If
            WebUtils.RegistrarScript(Me, "BotonBorrar", "AgregarImagenBorrar();")
            Me.btnAgregarCorreo.Visible = True
            Me.btnModificarCorreo.Visible = False
            Me.btnCancelarCorreo.Visible = False
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
#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    ''' Método encargado de inicializar el formulario segun la operacion a realizar
    ''' en caso de ser agregar carga el texo con "Agregar", en caso de ser modificar 
    ''' cargar el texto con "Modificar" y llama al método que obtiene los datos de la orden seleccionada segun el id 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub InicializarFormulario()
        Me.DsTelefonos = New DataSet
        Me.DsCorreos = New DataSet
        Me.Operacion = WebUtils.LeerParametro(Of eOperacion)("pvn_Operacion")
        CargarEstado()
        CargarTipoProveedor()
        telefonoModificar = String.Empty
        CorreoModificar = String.Empty
        Me.btnAgregarCorreo.Visible = True
        Me.btnModificarCorreo.Visible = False
        Me.btnCancelarCorreo.Visible = False

        Me.btnAgregaTelefonor.Visible = True
        Me.btnModificaTelefono.Visible = False
        Me.btnCancelarTelefono.Visible = False
        Select Case Me.Operacion
            Case Is = eOperacion.Agregar
                Me.lblAccion.Text = "Agregar Registro"
                Me.trEstado.Visible = False
                CargarEstructuraDs()
            Case Is = eOperacion.Modificar
                Me.lblAccion.Text = "Modificar Registro"
                Me.trEstado.Visible = True
                Try
                    Me.Identificacion = WebUtils.LeerParametro(Of String)("pvn_Identificacion")
                    CargarProveedor(Identificacion)
                Catch ex As Exception
                    Throw
                End Try
        End Select
    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos del registro segun 
    ''' las llaves obtenidas por parametro, muestra un mensaje en caso de ser un identificador no valido o no existente
    ''' </summary>
    ''' <param name="pvn_Identificacion">identificacion del registro</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarProveedor(pvn_Identificacion As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.Proveedor = vlo_Ws_OT_Catalogos.OTM_PROVEEDOR_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1}", Modelo.OTM_PROVEEDOR.IDENTIFICACION, pvn_Identificacion))

            CargarListaTelefonos(String.Format("{0} = {1}", Modelo.OTM_TELEFONO_PROVEEDOR.IDENTIFICACION, pvn_Identificacion), String.Empty, 1)
            CargarListaCorreo(String.Format("{0} = {1}", Modelo.OTM_CORREO_PROVEEDOR.IDENTIFICACION, pvn_Identificacion), String.Empty, 1)
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try

        If Me.Proveedor.Existe Then
            With Me.Proveedor
                Me.txtNombre.Text = .Nombre
                Me.ddlTipoProveedor.SelectedValue = .TipoProveedor
                Me.txtCedula.Text = .Identificacion
                If .SitioWeb <> Constantes.VALOR_DEFECTO_STRING AndAlso .SitioWeb <> String.Empty Then
                    Me.txtSitioWeb.Text = .SitioWeb
                Else
                    Me.txtSitioWeb.Text = String.Empty
                End If
                If .Estado <> Constantes.VALOR_DEFECTO_STRING AndAlso .Estado <> String.Empty Then
                    Me.ddlEstado.SelectedValue = .SitioWeb
                Else
                    Me.ddlEstado.SelectedValue = String.Empty
                End If
                Me.txtDireccion.Text = .Direccion
            End With
        Else
            WebUtils.RegistrarScript(Me.Page, "MostrarAlertaLlaveIncorrecta", "mostrarAlertaLlaveIncorrecta();")
        End If
    End Sub

    ''' <summary>
    ''' carga el combo de estados permitidos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEstado()
        Try
            Me.ddlEstado.Items.Clear()
            Me.ddlEstado.Items.Add(New ListItem("Activo", Estado.ACTIVO))
            Me.ddlEstado.Items.Add(New ListItem("Inactivo", Estado.INACTIVO))
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' carga el combo de estados permitidos
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarTipoProveedor()
        Try
            Me.ddlTipoProveedor.Items.Clear()
            Me.ddlTipoProveedor.Items.Add(New ListItem(Constantes.FORMATO_DDL_SELECCIONE, String.Empty))
            Me.ddlTipoProveedor.Items.Add(New ListItem("Fisico", TipoProveedor.FISICO))
            Me.ddlTipoProveedor.Items.Add(New ListItem("Jurídico", TipoProveedor.JURIDICO))
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvc_Condicion">parámetro con la condicion de búsqueda</param>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaTelefonos(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerExpresionDeOrdenamiento(Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO)
        End If

        Try

            Me.DsTelefonos = vlo_Ws_OT_Catalogos.OTM_TELEFONO_PROVEEDOR_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If DsTelefonos IsNot Nothing AndAlso DsTelefonos.Tables(0).Rows.Count > 0 Then
                With Me.rpTelefono
                    .DataSource = DsTelefonos
                    .DataMember = DsTelefonos.Tables(0).TableName
                    .DataBind()
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End With
                Me.DsTelefonos.Tables(0).PrimaryKey = New DataColumn() {Me.DsTelefonos.Tables(0).Columns(Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO)}
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de Teléfonos: {0}", DsTelefonos.Tables(0).Rows.Count)
                CantidadTelefonos = DsTelefonos.Tables(0).Rows.Count
            Else
                With Me.rpTelefono
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With
                Me.lblCantidadDeRegistros.Visible = False
                CantidadTelefonos = 0
            End If

            If Me.CantidadTelefonos = 0 Then
                Me.txtTelefonoValidacion.Text = String.Empty
            Else
                Me.txtTelefonoValidacion.Text = Me.CantidadTelefonos
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If

            If DsTelefonos IsNot Nothing Then
                DsTelefonos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de cargar la lista que será mostrada en la pantalla de listado de registros
    ''' </summary>
    ''' <param name="pvc_Condicion">parámetro con la condicion de búsqueda</param>
    ''' <param name="pvc_Orden">parámetro con el orden de la búsqueda</param>
    ''' <param name="pvn_NumeroDePagina"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaCorreo(pvc_Condicion As String, pvc_Orden As String, pvn_NumeroDePagina As Integer)
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials


        If String.IsNullOrWhiteSpace(pvc_Orden) Then
            pvc_Orden = ObtenerExpresionDeOrdenamiento(Modelo.OTM_CORREO_PROVEEDOR.IDENTIFICACION)
        End If

        Try

            Me.DsCorreos = vlo_Ws_OT_Catalogos.OTM_CORREO_PROVEEDOR_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                pvc_Condicion,
                pvc_Orden,
                True,
                pvn_NumeroDePagina,
                CType(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ITEMS_POR_PAGINA_EN_LISTADO), Integer))

            If DsCorreos IsNot Nothing AndAlso DsCorreos.Tables(0).Rows.Count > 0 Then
                With Me.rpCorreo
                    .DataSource = DsCorreos
                    .DataMember = DsCorreos.Tables(0).TableName
                    .DataBind()
                    WebUtils.RegistrarScript(Me, "visibilidadPaneles", "mostrarAreaDeListado(); ocultarAreaFiltrosDeBusqueda();")
                End With
                Me.DsCorreos.Tables(0).PrimaryKey = New DataColumn() {Me.DsCorreos.Tables(0).Columns(Modelo.OTM_CORREO_PROVEEDOR.CORREO)}
                Me.lblCantidadRegistros.Visible = True
                Me.lblCantidadRegistros.Text = String.Format("Cantidad de Correos: {0}", DsCorreos.Tables(0).Rows.Count)
                CantidadCorreos = DsCorreos.Tables(0).Rows.Count
            Else
                With Me.rpCorreo
                    .DataSource = Nothing
                    .DataBind()
                    .Visible = False
                End With
                Me.lblCantidadRegistros.Visible = False
                CantidadCorreos = 0
            End If

            If Me.CantidadCorreos = 0 Then
                Me.txtCorreoValidacion.Text = String.Empty
            Else
                Me.txtCorreoValidacion.Text = Me.CantidadCorreos
            End If
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If

            If DsCorreos IsNot Nothing Then
                DsCorreos.Dispose()
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Método que llena el reapeter de Fondos con los datos del dataset DsFondo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Jeannette Chavarría Rojas</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LlenarRepeaterTelefono(pvc_campoOrden As String)
        Try

            If Me.DsTelefonos.Tables.Count > 0 AndAlso DsTelefonos.Tables(0).Rows.Count > 0 Then
                With Me.rpTelefono
                    .DataSource = Me.DsTelefonos
                    .DataMember = Me.DsTelefonos.Tables(0).TableName
                    .DataBind()
                End With
                Me.lblCantidadDeRegistros.Visible = True
                Me.lblCantidadDeRegistros.Text = String.Format("Cantidad de Teléfonos: {0}", Me.CantidadTelefonos)
                If Not String.IsNullOrWhiteSpace(pvc_campoOrden) Then
                    DsTelefonos.Tables(0).DefaultView.Sort = String.Format("{0} {1}", pvc_campoOrden, Ordenamiento.ASCENDENTE)
                End If
            Else
                With Me.rpTelefono
                    .DataSource = Nothing
                    .DataBind()
                End With
                'MostrarAlertaNoHayDatos()
                Me.lblCantidadDeRegistros.Visible = False
            End If
            If Me.CantidadTelefonos = 0 Then
                Me.txtTelefonoValidacion.Text = String.Empty
            Else
                Me.txtTelefonoValidacion.Text = Me.CantidadTelefonos
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Método que llena el reapeter de Fondos con los datos del dataset DsFondo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Jeannette Chavarría Rojas</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LlenarRepeaterCorreos(pvc_campoOrden As String)
        Try

            If Me.DsCorreos.Tables.Count > 0 AndAlso DsCorreos.Tables(0).Rows.Count > 0 Then
                With Me.rpCorreo
                    .DataSource = Me.DsCorreos
                    .DataMember = Me.DsCorreos.Tables(0).TableName
                    .DataBind()
                End With
                Me.lblCantidadRegistros.Visible = True
                Me.lblCantidadRegistros.Text = String.Format("Cantidad de Correos: {0}", Me.CantidadCorreos)
                If Not String.IsNullOrWhiteSpace(pvc_campoOrden) Then
                    DsCorreos.Tables(0).DefaultView.Sort = String.Format("{0} {1}", pvc_campoOrden, Ordenamiento.ASCENDENTE)
                End If
            Else
                With Me.rpTelefono
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.lblCantidadRegistros.Visible = False
            End If

            If Me.CantidadCorreos = 0 Then
                Me.txtCorreoValidacion.Text = String.Empty
            Else
                Me.txtCorreoValidacion.Text = Me.CantidadCorreos
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Carga la estructura de las tablas SFF_USUARIO_FACTURACION,SFF_FONDO_UNIDAD
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Jeannette Chavarría Rojas</author>
    ''' <creationDate>08/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarEstructuraDs()
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            Me.DsTelefonos = vlo_Ws_OT_Catalogos.OTM_TELEFONO_PROVEEDOR_ListarRegistrosLista(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                                String.Format("1 = 0"),
                                String.Empty,
                                False,
                                0,
                                0)
            Me.DsCorreos = vlo_Ws_OT_Catalogos.OTM_CORREO_PROVEEDOR_ListarRegistrosLista(
                           ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                           ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                           String.Format("1 = 0"),
                           String.Empty,
                           False,
                           0,
                           0)

            Me.DsTelefonos.Tables(0).PrimaryKey = New DataColumn() {Me.DsTelefonos.Tables(0).Columns(Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO)}
            Me.DsCorreos.Tables(0).PrimaryKey = New DataColumn() {Me.DsCorreos.Tables(0).Columns(Modelo.OTM_CORREO_PROVEEDOR.CORREO)}
            Me.CantidadTelefonos = 0
            Me.CantidadCorreos = 0
            If Me.CantidadCorreos = 0 Then
                Me.txtCorreoValidacion.Text = String.Empty
            Else
                Me.txtCorreoValidacion.Text = Me.CantidadCorreos
            End If


            If Me.CantidadTelefonos = 0 Then
                Me.txtTelefonoValidacion.Text = String.Empty
            Else
                Me.txtTelefonoValidacion.Text = Me.CantidadTelefonos
            End If
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
    ''' Funcion encargada de contruir el registro
    ''' </summary>
    ''' <returns>la convocatoria</returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirRegistro() As Wsr_OT_Catalogos.EntOtmProveedor
        Dim vlo_EntOtmProveedor As Wsr_OT_Catalogos.EntOtmProveedor

        If Me.Operacion = eOperacion.Agregar Then
            vlo_EntOtmProveedor = New Wsr_OT_Catalogos.EntOtmProveedor
        Else
            vlo_EntOtmProveedor = Me.Proveedor
        End If

        With vlo_EntOtmProveedor
            .Nombre = Me.txtNombre.Text
            .TipoProveedor = Me.ddlTipoProveedor.SelectedValue
            .Identificacion = Me.txtCedula.Text
            .SitioWeb = Me.txtSitioWeb.Text
            .Estado = Me.ddlEstado.SelectedValue
            .Usuario = Me.Usuario.UserName
            .Direccion = Me.txtDireccion.Text
        End With

        Return vlo_EntOtmProveedor
    End Function


    ''' <summary>
    ''' Administra el proceso para agregar un registro
    ''' </summary>
    ''' <returns></returns>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function Aceptar(ByVal pvb_EsAgregar As Boolean) As Boolean
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos
        Dim vlo_EntOtmProveedor As Wsr_OT_Catalogos.EntOtmProveedor

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        vlo_EntOtmProveedor = ConstruirRegistro()

        Try
            Return vlo_Ws_OT_Catalogos.OTM_PROVEEDOR_InsertarModificarRegistroConAsociados(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_EntOtmProveedor, DsTelefonos, DsCorreos, pvb_EsAgregar) > 0
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function


    ''' <summary>
    ''' Permite borrar un Fondo del DataSet DsFondo
    ''' </summary>
    ''' <param name="pvc_Telefono"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Jeannette Chavarría Rojas</author>
    ''' <creationDate>20/11/2015</creationDate>
    ''' <changeLog></changeLog> 
    Private Function BorrarTelefono(pvc_Telefono As String) As Boolean
        Dim vlo_FilaAuxiliar As DataRow

        vlo_FilaAuxiliar = Me.DsTelefonos.Tables(0).Rows.Find(pvc_Telefono)

        If Me.DsTelefonos.Tables(0).Rows.Count > 0 Then

            vlo_FilaAuxiliar.Delete()
            Me.CantidadTelefonos -= 1
            Return True
        End If

        Return False
    End Function

    ''' <summary>
    ''' Permite borrar un Fondo del DataSet DsFondo
    ''' </summary>
    ''' <param name="pvc_Correo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Jeannette Chavarría Rojas</author>
    ''' <creationDate>20/11/2015</creationDate>
    ''' <changeLog></changeLog> 
    Private Function BorrarCorreo(pvc_Correo As String) As Boolean
        Dim vlo_FilaAuxiliar As DataRow

        vlo_FilaAuxiliar = Me.DsCorreos.Tables(0).Rows.Find(pvc_Correo)

        If Me.DsCorreos.Tables(0).Rows.Count > 0 Then

            vlo_FilaAuxiliar.Delete()
            Me.CantidadCorreos -= 1
            Return True
        End If

        Return False
    End Function

    ''' <summary>
    ''' Permite agregar al DsFondos los datos del Fondo Unidad 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Jeannette Chavarría Rojas</author>
    ''' <creationDate>28/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function AgregarDsTelefono(pvc_Identificacion As String, pvc_Telefono As String) As Boolean
        Dim vlo_DrFila As Data.DataRow
        Dim vlo_fila() As Data.DataRow
        Dim vlc_condicion As String
        Dim vlo_Usuario As New UsuarioActual()
        Try

            vlc_condicion = String.Format("{0}='{1}' and {2} = '{3}'", Modelo.OTM_TELEFONO_PROVEEDOR.IDENTIFICACION, pvc_Identificacion, Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO, pvc_Telefono)
            vlo_fila = Me.DsTelefonos.Tables(0).Select(vlc_condicion)
            If vlo_fila.Length > 0 Then
                MostrarAlertaError("El Teléfono ya existe en la Unidad")
                Return False
            Else
                If Me.DsTelefonos.Tables(0).Rows.Count >= 0 Then
                    vlo_DrFila = Me.DsTelefonos.Tables(0).NewRow

                    vlo_DrFila.Item(Me.DsTelefonos.Tables(0).Columns(Modelo.OTM_TELEFONO_PROVEEDOR.IDENTIFICACION)) = pvc_Identificacion
                    vlo_DrFila.Item(Me.DsTelefonos.Tables(0).Columns(Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO)) = pvc_Telefono
                    vlo_DrFila.Item(Me.DsTelefonos.Tables(0).Columns(Modelo.OTM_TELEFONO_PROVEEDOR.USUARIO)) = Me.Usuario.UserName
                    vlo_DrFila.Item(Me.DsTelefonos.Tables(0).Columns(Modelo.OTM_TELEFONO_PROVEEDOR.TIME_STAMP)) = Date.Now
                    Me.DsTelefonos.Tables(0).Rows.Add(vlo_DrFila)
                End If
                Me.CantidadTelefonos += 1
                Return True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Permite agregar al DsFondos los datos del Fondo Unidad 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Jeannette Chavarría Rojas</author>
    ''' <creationDate>28/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function AgregarDsCorreo(pvc_Identificacion As String, pvc_Correo As String, pvc_Nombre As String) As Boolean
        Dim vlo_DrFila As Data.DataRow
        Dim vlo_fila() As Data.DataRow
        Dim vlc_condicion As String
        Dim vlo_Usuario As New UsuarioActual()
        Try

            vlc_condicion = String.Format("{0}='{1}' and {2} = '{3}'", Modelo.OTM_CORREO_PROVEEDOR.IDENTIFICACION, pvc_Identificacion, Modelo.OTM_CORREO_PROVEEDOR.CORREO, pvc_Correo)
            vlo_fila = Me.DsCorreos.Tables(0).Select(vlc_condicion)
            If vlo_fila.Length > 0 Then
                MostrarAlertaError("El Correo ya existe")
                Return False
            Else
                If Me.DsCorreos.Tables(0).Rows.Count >= 0 Then
                    vlo_DrFila = Me.DsCorreos.Tables(0).NewRow

                    vlo_DrFila.Item(Me.DsCorreos.Tables(0).Columns(Modelo.OTM_CORREO_PROVEEDOR.IDENTIFICACION)) = pvc_Identificacion
                    vlo_DrFila.Item(Me.DsCorreos.Tables(0).Columns(Modelo.OTM_CORREO_PROVEEDOR.CORREO)) = pvc_Correo
                    vlo_DrFila.Item(Me.DsCorreos.Tables(0).Columns(Modelo.OTM_CORREO_PROVEEDOR.NOMBRE)) = pvc_Nombre
                    vlo_DrFila.Item(Me.DsCorreos.Tables(0).Columns(Modelo.OTM_CORREO_PROVEEDOR.USUARIO)) = Me.Usuario.UserName
                    vlo_DrFila.Item(Me.DsCorreos.Tables(0).Columns(Modelo.OTM_CORREO_PROVEEDOR.TIME_STAMP)) = Date.Now
                    Me.DsCorreos.Tables(0).Rows.Add(vlo_DrFila)
                End If
                Me.CantidadCorreos += 1
                Return True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' funcion encargada de realizar los oredenamientos segun los valores brindados
    ''' </summary>
    ''' <param name="pvc_Columna"> numero de la columna para el ordenamiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>23/05/2016</creationDate>
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

        UltimoSortExpression = String.Format("{0} {1}", UltimoSortColumn, IIf(UltimoSortDirection = SortDirection.Ascending, Ordenamiento.ASCENDENTE, Ordenamiento.DESCENDENTE))
        Return UltimoSortExpression
    End Function
#End Region


End Class
