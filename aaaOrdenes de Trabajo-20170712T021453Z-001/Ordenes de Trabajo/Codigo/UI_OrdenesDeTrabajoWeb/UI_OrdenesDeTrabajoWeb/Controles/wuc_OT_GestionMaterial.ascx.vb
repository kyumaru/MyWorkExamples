Imports Utilerias.OrdenesDeTrabajo

Partial Class Controles_wuc_OT_GestionMaterial
    Inherits System.Web.UI.UserControl
#Region "Propiedades"
    ''' <summary>
    ''' año en que se solicit
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property SolicitudRetiro As Integer
        Get
            Return CType(ViewState("SolicitudRetiro"), Integer)
        End Get
        Set(value As Integer)
            ViewState("SolicitudRetiro") = value
        End Set
    End Property

    ''' <summary>
    ''' id de la solicitud de retiro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez G</author>
    ''' <creationDate>20/06/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property Anno As Integer
        Get
            Return CType(ViewState("Anno"), Integer)
        End Get
        Set(value As Integer)
            ViewState("Anno") = value
        End Set
    End Property

    ''' <summary>
    ''' data set para almacer los adjuntos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>20/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Property DsMateriales As Data.DataSet
        Get
            Return CType(ViewState("DsMateriales"), Data.DataSet)
        End Get
        Set(value As Data.DataSet)
            ViewState("DsMateriales") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la identificación del sector o taller a agregar el operario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>20/6/2016</creationDate>
    Private Property MontoTotal As Integer
        Get
            If ViewState("MontoTotal") Is Nothing Then
                Return 0
            End If
            Return CType(ViewState("MontoTotal"), Integer)
        End Get
        Set(value As Integer)
            ViewState("MontoTotal") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la fecha de retiro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>20/6/2016</creationDate>
    Public Property FechaRetiro As Date
        Get
            Return CType(ViewState("FechaRetiro"), Date)
        End Get
        Set(value As Date)
            ViewState("FechaRetiro") = value
        End Set
    End Property

    ''' <summary>
    ''' Almacena la jornada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>6/7/2016</creationDate>
    Public Property JornadaRetiro As String
        Get
            Return CType(ViewState("JornadaRetiro"), String)
        End Get
        Set(value As String)
            ViewState("JornadaRetiro") = value
        End Set
    End Property

#End Region

#Region "Metodos"
    ''' <summary>
    ''' Inicializa la tabla de datos a mostrarle al usuario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>20/6/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarLista()
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vln_costoPromedio As Integer

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            'Listar las solicitudes de retiro con estado SMC
            '{0}: columna ID_SOLICITUD_RETIRO
            '{1}: id de la solicitud de retiro
            '{2}: columna ANNO
            '{3}: año en que se registró la solicitud
            Me.DsMateriales = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_RETIRO_ListarRegistrosLista(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3}", Modelo.OTT_DETALLE_RETIRO.ID_SOLICITUD_RETIRO, Me.SolicitudRetiro,
                    Modelo.OTT_DETALLE_RETIRO.ANNO, Me.Anno), String.Empty, False, 0, 0)

            If Me.DsMateriales IsNot Nothing AndAlso Me.DsMateriales.Tables(0).Rows.Count > 0 Then
                Me.rpPedidos.DataSource = DsMateriales
                Me.rpPedidos.DataMember = Me.DsMateriales.Tables(0).TableName
                Me.rpPedidos.DataBind()
                Me.rpPedidos.Visible = True
                For Each vlo_fila In DsMateriales.Tables(0).Rows
                    vln_costoPromedio = vlo_fila(Modelo.OTT_DETALLE_RETIRO.COSTO_CALCULADO)
                    MontoTotal = MontoTotal + vln_costoPromedio
                Next

                Me.lblMontoTotal.Text = String.Format("Total: ₡{0:n2}", MontoTotal)
            Else
                With Me.rpPedidos
                    .DataSource = Nothing
                    .DataBind()
                End With
                Me.rpPedidos.Visible = False
            End If

            upRp.Update()
        Catch ex As Exception
            Throw
        Finally
            If vlo_Ws_OT_OrdenesDeTrabajo IsNot Nothing Then
                vlo_Ws_OT_OrdenesDeTrabajo.Dispose()
            End If
        End Try

    End Sub

    Public Sub Inicializar()
        CargarLista()

        Me.lblFecha.Text = String.Format("Fecha de Retiro: {0}", FechaRetiro.ToString(Constantes.FORMATO_FECHA_UI))
        If Me.JornadaRetiro = Jornada.MANANA Then
            Me.lblJornada.Text = "Jornada: Mañana"
        Else
            Me.lblJornada.Text = "Jornada: Tarde"
        End If

    End Sub

#End Region


End Class
