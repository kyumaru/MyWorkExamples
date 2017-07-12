Imports Utilerias.OrdenesDeTrabajo

Partial Class OrdenesDeTrabajo_Frm_OT_MatrizOrdenTrabajo
    Inherits System.Web.UI.Page

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para el usuario en session
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/12/2015</creationDate>
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
    ''' llave de la ot
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdUbicacion As Integer
        Get
            Return CType(ViewState("IdUbicacion"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdUbicacion") = value
        End Set
    End Property

    ''' <summary>
    ''' llave la ot
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property IdOrdenTrabajo As String
        Get
            Return CType(ViewState("IdOrdenTrabajo"), String)
        End Get
        Set(value As String)
            ViewState("IdOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' llave la ot
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property CadenaEspacios As String
        Get
            Return CType(ViewState("CadenaEspacios"), String)
        End Get
        Set(value As String)
            ViewState("CadenaEspacios") = value
        End Set
    End Property

    ''' <summary>
    ''' llave de la ot
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Public Property CantidadDeCeldas As Integer
        Get
            Return CType(ViewState("CantidadDeCeldas"), Integer)
        End Get
        Set(value As Integer)
            ViewState("CantidadDeCeldas") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para las identificaciones de los requerimientos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property ListaRequerimientos() As List(Of Integer)
        Get
            Return CType(ViewState("ListaRequerimientos"), List(Of Integer))
        End Get
        Set(value As List(Of Integer))
            ViewState("ListaRequerimientos") = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para las identificaciones de los subcomponentes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Property ListaSubComponentes() As List(Of String)
        Get
            Return CType(ViewState("ListaSubComponentes"), List(Of String))
        End Get
        Set(value As List(Of String))
            ViewState("ListaSubComponentes") = value
        End Set
    End Property

    ''' <summary>
    ''' data set de detalles existentes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property DsDetalles As Data.DataSet
        Get
            Return CType(ViewState("DsDetalles"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsDetalles") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para la ORDEN DE TRABAJO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property PreOrdenTrabajo As Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo
        Get
            Return CType(ViewState("PreOrdenTrabajo"), Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo)
        End Get
        Set(value As Wsr_OT_OrdenesDeTrabajo.EntOtfPreOrdenTrabajo)
            ViewState("PreOrdenTrabajo") = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property Enviar As Boolean
        Get
            Return CType(ViewState("Enviar"), Boolean)
        End Get
        Set(value As Boolean)
            ViewState("Enviar") = value
        End Set
    End Property

    ''' <summary>
    ''' pantalla de retorno
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>25/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property PantallaRetorno As String
        Get
            Return CType(ViewState("PantallaRetorno"), String)
        End Get
        Set(value As String)
            ViewState("PantallaRetorno") = value
        End Set
    End Property


#End Region

#Region "Eventos"

    ''' <summary>
    ''' Evento que se ejecuta al cargar la página
    ''' </summary>
    ''' <param name="sender">Parámetro propio del evento</param>
    ''' <param name="e">Parámetro propio del evento</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Me.Usuario = New UsuarioActual
                LeerParametrosSession()
                CargarPreOrdenTrabajo(Me.IdUbicacion, Me.IdOrdenTrabajo)
                ConstruirMatriz()
            Catch ex As Exception
                Dim vlo_ControlDeErrores As New ControlDeErrores
                vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' procesa la informacion cargada en la matriz
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>17/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        Try

            Me.Enviar = False
            If ConstruirDatos() Then
                Me.btnProcesar.Enabled = False
                Me.btnProcesarEnviar.Enabled = False
                Me.btnRegresar.Enabled = False
                WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarAlertaActualizacionExitosa();")
            Else
                ConstruirMatriz()
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
    ''' procesa la informacion cargada en la matriz
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnProcesarEnviar_Click(sender As Object, e As EventArgs) Handles btnProcesarEnviar.Click
        Try
            Me.Enviar = True
            If ConstruirDatos() Then
                Me.btnProcesar.Enabled = False
                Me.btnProcesarEnviar.Enabled = False
                Me.btnRegresar.Enabled = False
                WebUtils.RegistrarScript(Me.Page, "MostrarPopupRegistroExitoso", "mostrarAlertaActualizacionExitosa();")
            Else
                ConstruirMatriz()
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
    ''' evento que se ejeuta cuando se da click sobre el boton de regresar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Try
            Me.Session.Add("pvn_IdUbicacion", Me.IdUbicacion)
            Me.Session.Add("pvc_IdOrdenTrabajo", Me.IdOrdenTrabajo)
            Me.Session.Add("pvc_PantallaRetorno", Me.PantallaRetorno)
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
        Response.Redirect(String.Format("Lst_OT_SeleccionEspacios.aspx"), False)
    End Sub

#End Region

#Region "Métodos"

    ''' <summary>
    ''' Registra el script necesario para mostrar una alerta
    ''' </summary>
    ''' <param name="pvc_Mensaje">Mensaje a mostrar en la alerta</param>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>18/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub MostrarAlertaError(pvc_Mensaje As String)
        WebUtils.RegistrarScript(Me, "alertaError", String.Format("mostrarAlertaError('{0}');", pvc_Mensaje))
    End Sub

    ''' <summary>
    '''  inicializa los componentes necesarios para el funcionamiento de la página
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub ConstruirMatriz()
        Dim vlc_CondicionEspacios As String
        Dim vlo_DsSubComponentes As Data.DataSet
        Dim vlo_DsRequerimientos As Data.DataSet
        Dim vlo_Row As TableRow
        Dim vlo_Row1 As TableRow
        Dim vlo_Row2 As TableRow
        Dim vlo_Row3 As TableRow
        Dim vlo_Cell As TableCell
        Dim vlo_CellEspacio As TableCell
        Dim vlo_CampoLabel As Label
        Dim vlo_CampoTexto As TextBox
        Dim vlo_ddlIndicador As DropDownList
        Dim vlo_ListaTipos As New List(Of String)

        Dim vlo_Condicion As String
        Dim vln_Id As Integer = 1
        Dim vlo_DrFilaDetalle As Data.DataRow

        Try

            vlo_Condicion = String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTF_FICHA_TECNICA_DETALLE.ID_UBICACION, Me.IdUbicacion, Modelo.OTF_FICHA_TECNICA_DETALLE.ID_PRE_ORDEN_TRABAJO, Me.IdOrdenTrabajo)
            DsDetalles = CargarDataSetFichaTecnicaDetalle(vlo_Condicion)

            DsDetalles.Tables(0).PrimaryKey = New Data.DataColumn() {
                DsDetalles.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_DETALLE.ID_ESPACIO),
                DsDetalles.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_DETALLE.ID_SUBCOMPONENTE),
                DsDetalles.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_DETALLE.ID_REQUERIMIENTO)}

            CantidadDeCeldas = 0
            ListaSubComponentes = New List(Of String)
            ListaRequerimientos = New List(Of Integer)
            vlc_CondicionEspacios = ObtenerCondicionDeBusquedaEspacios()
            vlo_DsSubComponentes = CargarDataSetSubComponentes(vlc_CondicionEspacios)
            vlo_DsRequerimientos = CargarDataSetRequerimientos(String.Format("{0} = '{1}' AND {2} = 0", Modelo.V_OTM_REQUERIMIENTOLST.ESTADO, Estado.ACTIVO, Modelo.V_OTM_REQUERIMIENTOLST.CANTIDAD_HIJOS))

            vlo_Row1 = New TableRow
            Me.tContenido.Rows.Add(vlo_Row1)

            vlo_Row2 = New TableRow
            vlo_Row3 = New TableRow

            vlo_Cell = New TableCell

            vlo_CampoLabel = New Label
            vlo_CampoLabel.Text = "Espacio"
            vlo_CampoLabel.Style.Add("font-weight", "bold")
            vlo_Cell.BorderWidth = "1"
            vlo_Cell.BorderColor = System.Drawing.Color.Black
            vlo_Cell.BackColor = System.Drawing.Color.LightSteelBlue
            vlo_Cell.Controls.Add(vlo_CampoLabel)
            vlo_Row3.Cells.Add(vlo_Cell)

            vlo_Cell = New TableCell
            vlo_CampoLabel = New Label
            vlo_CampoLabel.Text = "Sub Componente"
            vlo_CampoLabel.Style.Add("font-weight", "bold")
            vlo_Cell.Controls.Add(vlo_CampoLabel)
            vlo_Cell.BorderWidth = "1"
            vlo_Cell.BorderColor = System.Drawing.Color.Black
            vlo_Cell.BackColor = System.Drawing.Color.LightSteelBlue
            vlo_Row3.Cells.Add(vlo_Cell)

            vlo_Cell = New TableCell
            vlo_Cell.BorderWidth = "1"
            vlo_Cell.BorderColor = System.Drawing.Color.Black
            vlo_Cell.BackColor = System.Drawing.Color.LightSlateGray
            vlo_Row2.Cells.Add(vlo_Cell)
            vlo_Cell = New TableCell
            vlo_Cell.BorderWidth = "1"
            vlo_Cell.BorderColor = System.Drawing.Color.Black
            vlo_Cell.BackColor = System.Drawing.Color.LightSlateGray
            vlo_Row2.Cells.Add(vlo_Cell)
            vlo_Cell = New TableCell
            vlo_Cell.BorderWidth = "1"
            vlo_Cell.BorderColor = System.Drawing.Color.Black
            vlo_Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#505d69")
            vlo_Row1.Cells.Add(vlo_Cell)
            vlo_Cell = New TableCell
            vlo_Cell.BorderWidth = "1"
            vlo_Cell.BorderColor = System.Drawing.Color.Black
            vlo_Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#505d69")
            vlo_Row1.Cells.Add(vlo_Cell)

            For Each vlo_FilaRequerimiento In vlo_DsRequerimientos.Tables(0).Rows
                vlo_Cell = New TableCell
                vlo_CampoLabel = New Label
                vlo_CampoLabel.Text = vlo_FilaRequerimiento(Modelo.V_OTM_REQUERIMIENTOLST.DESCRIPCION).ToString
                vlo_CampoLabel.Style.Add("font-weight", "bold")
                vlo_Cell.Controls.Add(vlo_CampoLabel)
                vlo_Cell.BorderWidth = "1"
                vlo_Cell.BorderColor = System.Drawing.Color.Black
                vlo_Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#41ADE7")
                vlo_Row3.Cells.Add(vlo_Cell)

                vlo_ListaTipos.Add(vlo_FilaRequerimiento(Modelo.V_OTM_REQUERIMIENTOLST.TIPO_VALOR).ToString)
                ListaRequerimientos.Add(CType(vlo_FilaRequerimiento(Modelo.V_OTM_REQUERIMIENTOLST.ID_REQUERIMIENTO), Integer))

                Me.CantidadDeCeldas = Me.CantidadDeCeldas + 1
            Next

            For i = 0 To vlo_DsRequerimientos.Tables(0).Rows.Count - 1
                Dim vlo_ColSpam As Integer = 1
                vlo_Cell = New TableCell
                vlo_CampoLabel = New Label
                vlo_CampoLabel.Text = vlo_DsRequerimientos.Tables(0).Rows(i)(Modelo.V_OTM_REQUERIMIENTOLST.DESCRIPCION_PADRE).ToString
                vlo_CampoLabel.Style.Add("font-weight", "bold")
                vlo_Cell.Controls.Add(vlo_CampoLabel)
                vlo_Cell.BorderWidth = "1"
                vlo_Cell.BorderColor = System.Drawing.Color.Black
                vlo_Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#3D8FCD")

                If i < vlo_DsRequerimientos.Tables(0).Rows.Count - 2 Then
                    While (vlo_DsRequerimientos.Tables(0).Rows(i)(Modelo.V_OTM_REQUERIMIENTOLST.DESCRIPCION_PADRE).ToString = vlo_DsRequerimientos.Tables(0).Rows(i + 1)(Modelo.V_OTM_REQUERIMIENTOLST.DESCRIPCION_PADRE).ToString) And (vlo_DsRequerimientos.Tables(0).Rows(i)(Modelo.V_OTM_REQUERIMIENTOLST.DESCRIPCION_PADRE).ToString <> String.Empty)
                        i = i + 1
                        vlo_ColSpam = vlo_ColSpam + 1
                    End While
                End If

                vlo_Cell.ColumnSpan = vlo_ColSpam.ToString

                vlo_Row2.Cells.Add(vlo_Cell)
            Next

            For i = 0 To vlo_DsRequerimientos.Tables(0).Rows.Count - 1
                Dim vlo_ColSpam As Integer = 1
                vlo_Cell = New TableCell
                vlo_CampoLabel = New Label
                vlo_CampoLabel.Text = vlo_DsRequerimientos.Tables(0).Rows(i)(Modelo.V_OTM_REQUERIMIENTOLST.DESCRIPCION_ABUELO).ToString
                vlo_CampoLabel.Style.Add("font-weight", "bold")
                vlo_Cell.Controls.Add(vlo_CampoLabel)
                vlo_Cell.BorderWidth = "1"
                vlo_Cell.BorderColor = System.Drawing.Color.Black
                vlo_Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#2F6E9D")

                If i < vlo_DsRequerimientos.Tables(0).Rows.Count - 2 Then
                    While (vlo_DsRequerimientos.Tables(0).Rows(i)(Modelo.V_OTM_REQUERIMIENTOLST.DESCRIPCION_ABUELO).ToString = vlo_DsRequerimientos.Tables(0).Rows(i + 1)(Modelo.V_OTM_REQUERIMIENTOLST.DESCRIPCION_ABUELO).ToString) And (vlo_DsRequerimientos.Tables(0).Rows(i)(Modelo.V_OTM_REQUERIMIENTOLST.DESCRIPCION_ABUELO).ToString <> String.Empty)
                        i = i + 1
                        vlo_ColSpam = vlo_ColSpam + 1

                        If i = vlo_DsRequerimientos.Tables(0).Rows.Count - 1 Then
                            Exit While
                        End If

                    End While
                End If

                vlo_Cell.ColumnSpan = vlo_ColSpam.ToString

                vlo_Row1.Cells.Add(vlo_Cell)
            Next

            Me.tContenido.Rows.Add(vlo_Row1)
            Me.tContenido.Rows.Add(vlo_Row2)
            Me.tContenido.Rows.Add(vlo_Row3)

            For k = 0 To vlo_DsSubComponentes.Tables(0).Rows.Count - 1
                vlo_Row = New TableRow

                vlo_CellEspacio = New TableCell
                vlo_CampoLabel = New Label
                vlo_CampoLabel.Text = vlo_DsSubComponentes.Tables(0).Rows(k)(Modelo.V_OTM_SUBCOMPONENTELST.DESCRIPCION_ESPACIO)
                vlo_CampoLabel.Style.Add("font-weight", "bold")
                vlo_CellEspacio.Controls.Add(vlo_CampoLabel)
                vlo_CellEspacio.BorderWidth = "1"
                vlo_CellEspacio.BorderColor = System.Drawing.Color.Black
                vlo_CellEspacio.BackColor = System.Drawing.ColorTranslator.FromHtml("#3D8FCD")
                vlo_Row.Cells.Add(vlo_CellEspacio)

                vlo_Cell = New TableCell
                vlo_CampoLabel = New Label
                vlo_CampoLabel.Text = vlo_DsSubComponentes.Tables(0).Rows(k)(Modelo.V_OTM_SUBCOMPONENTELST.DESCRIPCION)
                vlo_CampoLabel.Style.Add("font-weight", "bold")
                vlo_Cell.Controls.Add(vlo_CampoLabel)
                vlo_Cell.BorderWidth = "1"
                vlo_Cell.BorderColor = System.Drawing.Color.Black
                vlo_Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#41ADE7")
                vlo_Row.Cells.Add(vlo_Cell)

                For i = 0 To Me.CantidadDeCeldas - 1
                    vlo_Cell = New TableCell
                    vlo_Cell.BorderWidth = "1"
                    vlo_Cell.BorderColor = System.Drawing.Color.Black

                    vlo_DrFilaDetalle = DsDetalles.Tables(0).Rows.Find(New Object() {vlo_DsSubComponentes.Tables(0).Rows(k)(Modelo.V_OTM_SUBCOMPONENTELST.ID_ESPACIO), vlo_DsSubComponentes.Tables(0).Rows(k)(Modelo.V_OTM_SUBCOMPONENTELST.ID_SUBCOMPONENTE), Me.ListaRequerimientos.Item(i)})

                    If vlo_ListaTipos(i) <> TipoValor.INDICADOR Then
                        vlo_CampoTexto = New TextBox
                        If vlo_ListaTipos(i) = TipoValor.NUMERICO Then
                            vlo_CampoTexto.Attributes.Add("onkeypress", "return valideKey(event);")
                        End If
                        vlo_CampoTexto.Attributes.Add("title", String.Format("Espacio: {0},  Sub Componente: {1}", vlo_DsSubComponentes.Tables(0).Rows(k)(Modelo.V_OTM_SUBCOMPONENTELST.DESCRIPCION_ESPACIO).ToString, vlo_DsSubComponentes.Tables(0).Rows(k)(Modelo.V_OTM_SUBCOMPONENTELST.DESCRIPCION).ToString))
                        vlo_CampoTexto.Attributes.Add("data-tipo", "tooltip")
                        vlo_CampoTexto.Attributes.Add("id", "Campo" + vln_Id.ToString)
                        vln_Id = vln_Id + 1
                        vlo_CampoTexto.MaxLength = 10
                        vlo_CampoTexto.Style.Add("width", "80px")
                        If vlo_DrFilaDetalle IsNot Nothing Then
                            vlo_CampoTexto.Text = vlo_DrFilaDetalle(Modelo.OTF_FICHA_TECNICA_DETALLE.VALOR)
                        End If
                        vlo_Cell.Controls.Add(vlo_CampoTexto)
                    Else
                        vlo_ddlIndicador = New DropDownList
                        vlo_ddlIndicador.Items.Add(New ListItem(String.Empty, String.Empty))
                        vlo_ddlIndicador.Items.Add(New ListItem("Si", "1"))
                        vlo_ddlIndicador.Items.Add(New ListItem("No", "0"))
                        If vlo_DrFilaDetalle IsNot Nothing Then
                            vlo_ddlIndicador.SelectedValue = vlo_DrFilaDetalle(Modelo.OTF_FICHA_TECNICA_DETALLE.VALOR)
                        End If
                        vlo_ddlIndicador.Attributes.Add("title", String.Format("Espacio: {0},   Sub Componente: {1}", vlo_DsSubComponentes.Tables(0).Rows(k)(Modelo.V_OTM_SUBCOMPONENTELST.DESCRIPCION_ESPACIO).ToString, vlo_DsSubComponentes.Tables(0).Rows(k)(Modelo.V_OTM_SUBCOMPONENTELST.DESCRIPCION).ToString))
                        vlo_ddlIndicador.Attributes.Add("data-tipo", "tooltip")
                        vlo_ddlIndicador.Attributes.Add("id", "Campo" + vln_Id.ToString)
                        vln_Id = vln_Id + 1
                        vlo_ddlIndicador.Style.Add("width", "80px")
                        vlo_Cell.Controls.Add(vlo_ddlIndicador)
                    End If

                    vlo_Row.Cells.Add(vlo_Cell)
                Next

                ListaSubComponentes.Add(String.Format("{0}¬{1}", vlo_DsSubComponentes.Tables(0).Rows(k)(Modelo.V_OTM_SUBCOMPONENTELST.ID_ESPACIO).ToString, vlo_DsSubComponentes.Tables(0).Rows(k)(Modelo.V_OTM_SUBCOMPONENTELST.ID_SUBCOMPONENTE).ToString))

                Me.tContenido.Rows.Add(vlo_Row)
            Next

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' lee y carga los parametros guardados en sesion
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Sub LeerParametrosSession()
        Try
            Me.IdUbicacion = WebUtils.LeerParametro(Of Integer)("pvn_IdUbicacion")
            Me.IdOrdenTrabajo = WebUtils.LeerParametro(Of String)("pvc_IdOrdenTrabajo")
            Me.CadenaEspacios = WebUtils.LeerParametro(Of String)("pvc_CadenaEspacios")
            Me.PantallaRetorno = WebUtils.LeerParametro(Of String)("pvc_PantallaRetorno")
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

    ''' <summary>
    ''' Método encargado de comunicarse con el servicio web , obtener y cargar los datos de la orden de trabajo segun 
    ''' las llaves obtenidas por parametro
    ''' </summary>
    ''' <param name="pvn_IdUbicacion">id de la ubicacion</param>
    ''' <param name="pvn_IdOrdenTrabajo">id de la orden</param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarPreOrdenTrabajo(pvn_IdUbicacion As Integer, pvn_IdOrdenTrabajo As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Me.PreOrdenTrabajo = vlo_Ws_OT_OrdenesDeTrabajo.OTF_PRE_ORDEN_TRABAJO_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_PRE_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTF_PRE_ORDEN_TRABAJO.ID_PRE_ORDEN_TRABAJO, pvn_IdOrdenTrabajo))

        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Sub

#End Region

#Region "Funciones"

    ''' <summary>
    ''' construye las registros necesarios, segun los datos ingresados por el usuario en la matriz 
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>16/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ConstruirDatos() As Integer
        Dim Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlc_Estado As String = String.Empty

        Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Ws_OT_OrdenesDeTrabajo.Timeout = -1
        Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Dim vlc_CadenaValores As String() = Me.hdfCadenaMatriz.Value.Split(",")
            Dim vln_IndiceMatriz As Integer = 0
            Dim vlo_DsFichaTecnicaEspacio As Data.DataSet
            Dim vlo_DsFichaTecnicaSubComp As Data.DataSet
            Dim vlo_DsFichaTecnicaDetalle As Data.DataSet
            Dim vlo_DrFilaEspacio As Data.DataRow
            Dim vlo_DrFilaSubComp As Data.DataRow
            Dim vlo_DrFilaDetalle As Data.DataRow

            vlo_DsFichaTecnicaEspacio = CargarEstructuraFichaTecnicaEspacio()
            vlo_DsFichaTecnicaSubComp = CargarEstructuraFichaTecnicaSubComp()
            vlo_DsFichaTecnicaDetalle = CargarEstructuraFichaTecnicaDetalle()

            For Each vlc_Espacio In Me.CadenaEspacios.Split(",")
                vlo_DrFilaEspacio = vlo_DsFichaTecnicaEspacio.Tables(0).NewRow
                vlo_DrFilaEspacio.Item(vlo_DsFichaTecnicaEspacio.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_ESPACIO.ID_UBICACION)) = Me.IdUbicacion
                vlo_DrFilaEspacio.Item(vlo_DsFichaTecnicaEspacio.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_ESPACIO.ID_PRE_ORDEN_TRABAJO)) = Me.IdOrdenTrabajo
                vlo_DrFilaEspacio.Item(vlo_DsFichaTecnicaEspacio.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_ESPACIO.ID_ESPACIO)) = vlc_Espacio
                vlo_DsFichaTecnicaEspacio.Tables(0).Rows.Add(vlo_DrFilaEspacio)
            Next

            For i = 0 To ListaSubComponentes.Count - 1
                Dim vlc_LlaveSubcomp As String()
                vlc_LlaveSubcomp = ListaSubComponentes(i).Split("¬")

                vlo_DrFilaSubComp = vlo_DsFichaTecnicaSubComp.Tables(0).NewRow
                vlo_DrFilaSubComp.Item(vlo_DsFichaTecnicaSubComp.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_SUBCOMP.ID_UBICACION)) = Me.IdUbicacion
                vlo_DrFilaSubComp.Item(vlo_DsFichaTecnicaSubComp.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_SUBCOMP.ID_PRE_ORDEN_TRABAJO)) = Me.IdOrdenTrabajo
                vlo_DrFilaSubComp.Item(vlo_DsFichaTecnicaSubComp.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_SUBCOMP.ID_ESPACIO)) = vlc_LlaveSubcomp(0)
                vlo_DrFilaSubComp.Item(vlo_DsFichaTecnicaSubComp.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_SUBCOMP.ID_SUBCOMPONENTE)) = vlc_LlaveSubcomp(1)
                vlo_DsFichaTecnicaSubComp.Tables(0).Rows.Add(vlo_DrFilaSubComp)

                For j = 0 To CantidadDeCeldas - 1

                    If (vlc_CadenaValores(vln_IndiceMatriz) <> String.Empty) And (vlc_CadenaValores(vln_IndiceMatriz) <> "¬") Then
                        vlo_DrFilaDetalle = vlo_DsFichaTecnicaDetalle.Tables(0).NewRow
                        vlo_DrFilaDetalle.Item(vlo_DsFichaTecnicaDetalle.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_DETALLE.ID_UBICACION)) = Me.IdUbicacion
                        vlo_DrFilaDetalle.Item(vlo_DsFichaTecnicaDetalle.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_DETALLE.ID_PRE_ORDEN_TRABAJO)) = Me.IdOrdenTrabajo
                        vlo_DrFilaDetalle.Item(vlo_DsFichaTecnicaDetalle.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_DETALLE.ID_ESPACIO)) = vlc_LlaveSubcomp(0)
                        vlo_DrFilaDetalle.Item(vlo_DsFichaTecnicaDetalle.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_DETALLE.ID_SUBCOMPONENTE)) = vlc_LlaveSubcomp(1)
                        vlo_DrFilaDetalle.Item(vlo_DsFichaTecnicaDetalle.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_DETALLE.ID_REQUERIMIENTO)) = ListaRequerimientos(j)
                        vlo_DrFilaDetalle.Item(vlo_DsFichaTecnicaDetalle.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_DETALLE.VALOR)) = vlc_CadenaValores(vln_IndiceMatriz)
                        vlo_DrFilaDetalle.Item(vlo_DsFichaTecnicaDetalle.Tables(0).Columns(Modelo.OTF_FICHA_TECNICA_DETALLE.USUARIO)) = Me.Usuario.UserName
                        vlo_DsFichaTecnicaDetalle.Tables(0).Rows.Add(vlo_DrFilaDetalle)
                    End If

                    vln_IndiceMatriz = vln_IndiceMatriz + 1
                Next
            Next

            If Enviar Then
                If ObtenerDirectorUnidad(CType(Me.PreOrdenTrabajo.CodUnidadSirh, Integer)) = Me.PreOrdenTrabajo.NumEmpleado Then
                    vlc_Estado = EstadoOrden.ASIGNADA
                Else
                    If Not Roles.IsUserInRole(Membership.GetUser.UserName, ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ROL_OT_AUTORIZADOR_SOLICITUD)) Then
                        vlc_Estado = EstadoOrden.PENDIENTE_REVISION_DIRECTOR
                    Else
                        vlc_Estado = EstadoOrden.ASIGNADA
                    End If
                End If
            End If

            Me.PreOrdenTrabajo.Usuario = Me.Usuario.UserName

            If vlo_DsFichaTecnicaDetalle.Tables(0).Rows.Count > 0 Then
                Return Ws_OT_OrdenesDeTrabajo.OTF_FICHA_TECNICA_DETALLE_InsertarDescripcionesRequerimientosEspaciosPrincipales(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                vlo_DsFichaTecnicaEspacio, vlo_DsFichaTecnicaSubComp, vlo_DsFichaTecnicaDetalle, Me.PreOrdenTrabajo, vlc_Estado) > 0
            Else
                WebUtils.RegistrarScript(Me.Page, "mostrarAlertaDatosFaltantes", "mostrarAlertaDatosFaltantes();")
                Return False
            End If

        Catch ex As Exception
            Throw
        Finally
            If Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' crea una condicion de busqueda
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function ObtenerCondicionDeBusquedaEspacios() As String
        Dim vlc_Condicion As String = String.Empty
        Dim vlc_CadenaEspacios As String()

        vlc_CadenaEspacios = Me.CadenaEspacios.Split(",")

        For Each vlc_Caracter In vlc_CadenaEspacios
            If String.IsNullOrWhiteSpace(vlc_Condicion) Then
                vlc_Condicion = String.Format("{0} = {1}", Modelo.V_OTM_SUBCOMPONENTELST.ID_ESPACIO, vlc_Caracter)
            Else
                vlc_Condicion = String.Format("{0} OR {1} = {2}", vlc_Condicion, Modelo.V_OTM_SUBCOMPONENTELST.ID_ESPACIO, vlc_Caracter)
            End If
        Next

        Return vlc_Condicion
    End Function

    ''' <summary>
    ''' Retorna un data set con los datos de los requerimientos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarDataSetRequerimientos(pvc_Condicion As String) As Data.DataSet
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_REQUERIMIENTO_ListarRegistrosLista(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               pvc_Condicion,
               String.Empty,
               False,
               0,
               0)
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' Retorna un data set con los datos de los sub componentes 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarDataSetSubComponentes(pvc_Condicion As String) As Data.DataSet
        Dim vlo_Ws_OT_Catalogos As Wsr_OT_Catalogos.Ws_OT_Catalogos

        vlo_Ws_OT_Catalogos = New Wsr_OT_Catalogos.Ws_OT_Catalogos
        vlo_Ws_OT_Catalogos.Timeout = -1
        vlo_Ws_OT_Catalogos.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return vlo_Ws_OT_Catalogos.OTM_SUBCOMPONENTE_ListarRegistrosLista(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               pvc_Condicion,
               String.Empty,
               False,
               0,
               0)
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_Catalogos IsNot Nothing Then
                vlo_Ws_OT_Catalogos.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga la estructura de la tabla OTT_FICHA_TECNICA_ESPACIO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>17/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarEstructuraFichaTecnicaEspacio() As Data.DataSet
        Dim Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Ws_OT_OrdenesDeTrabajo.Timeout = -1
        Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return Ws_OT_OrdenesDeTrabajo.OTF_FICHA_TECNICA_ESPACIO_ListarRegistros(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("1 = 0"),
               String.Empty,
               False,
               0,
               0)
        Catch ex As Exception
            Throw
        Finally
            If Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga la estructura de la tabla OTT_FICHA_TECNICA_ESPACIO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>17/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarEstructuraFichaTecnicaSubComp() As Data.DataSet
        Dim Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Ws_OT_OrdenesDeTrabajo.Timeout = -1
        Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return Ws_OT_OrdenesDeTrabajo.OTF_FICHA_TECNICA_SUBCOMP_ListarRegistros(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("1 = 0"),
               String.Empty,
               False,
               0,
               0)
        Catch ex As Exception
            Throw
        Finally
            If Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga la estructura de la tabla OTT_FICHA_TECNICA_ESPACIO
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>17/12/2015</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarEstructuraFichaTecnicaDetalle() As Data.DataSet
        Dim Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Ws_OT_OrdenesDeTrabajo.Timeout = -1
        Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return Ws_OT_OrdenesDeTrabajo.OTF_FICHA_TECNICA_DETALLE_ListarRegistros(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               String.Format("1 = 0"),
               String.Empty,
               False,
               0,
               0)
        Catch ex As Exception
            Throw
        Finally
            If Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' carga los datos de la tabla OTT_FICHA_TECNICA_ESPACIO segun la condicion obtenida por parametro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Function CargarDataSetFichaTecnicaDetalle(pvc_Condicion As String) As Data.DataSet
        Dim Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Ws_OT_OrdenesDeTrabajo.Timeout = -1
        Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try
            Return Ws_OT_OrdenesDeTrabajo.OTF_FICHA_TECNICA_DETALLE_ListarRegistros(
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
               ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
               pvc_Condicion,
               String.Empty,
               False,
               0,
               0)
        Catch ex As Exception
            Throw
        Finally
            If Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' retorna  el numero de emplado
    ''' </summary>
    ''' <param name="pvn_CodUnidadSirh"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>14/01/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Function ObtenerDirectorUnidad(ByVal pvn_CodUnidadSirh As Integer) As Integer
        Dim vlo_Estructura As WsrCatalogosVacaciones.PLM_ESTRUCTURA_ORG
        Dim vlo_BLLPlanillas As WsrCatalogosVacaciones.WsCatalogosVacaciones
        Dim vlc_Condicion As String

        Try
            vlo_BLLPlanillas = New WsrCatalogosVacaciones.WsCatalogosVacaciones
            vlo_BLLPlanillas.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_BLLPlanillas.Timeout = -1

            vlc_Condicion = String.Format("COD_UNIDAD_SIRH = {0} AND TIPO = 'UBC' AND ESTADO = '{1}'", pvn_CodUnidadSirh, Estado.ACTIVO)

            vlo_Estructura = vlo_BLLPlanillas.PLM_ESTRUCTURA_ORG_ObtenerRegistro(
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                   ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                   vlc_Condicion)

            If vlo_Estructura.NUM_EMPLEADO_SUSTITUTO <> 0 _
                                AndAlso vlo_Estructura.FECHA_DESDE_SUSTITUCION <= DateTime.Now _
                                And (vlo_Estructura.FECHA_HASTA_SUSTITUCION >= DateTime.Now _
                                       Or vlo_Estructura.FECHA_HASTA_SUSTITUCION = Utilerias.OrdenesDeTrabajo.Constantes.fechaNula) Then
                Return vlo_Estructura.NUM_EMPLEADO_SUSTITUTO
            Else
                Return vlo_Estructura.NUM_EMPLEADO_JEFE
            End If

        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Function

#End Region

End Class
