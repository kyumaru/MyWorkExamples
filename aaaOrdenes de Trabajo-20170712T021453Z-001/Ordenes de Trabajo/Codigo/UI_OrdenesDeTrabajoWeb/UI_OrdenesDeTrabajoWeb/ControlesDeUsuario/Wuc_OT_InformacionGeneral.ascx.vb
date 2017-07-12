Imports Wsr_OT_OrdenesDeTrabajo
Imports System.Data
Imports Utilerias.OrdenesDeTrabajo

''' <summary>
''' Control de usuario para encabezado resumen de órdenes de trabajo
''' </summary>
''' <remarks></remarks>
''' <Autor>César Bermudez Garcia</Autor>
''' <CreationDate>21/01/2016</CreationDate>
Partial Class ControlesDeUsuario_Wuc_OT_InformacionGeneral
    Inherits System.Web.UI.UserControl

#Region "Atributos"
    Private vgc_IdOrdenTrabajo As String
    Private vgc_IdUbicacion As String
    Private vgn_Anno As Integer
    Private vgn_NoContrato As String
    Private vgc_NombreContrato As String
#End Region

#Region "Propiedades"

    Public Property IdOrdenTrabajo As String
        Get
            Return vgc_IdOrdenTrabajo
        End Get
        Set(value As String)
            vgc_IdOrdenTrabajo = value
        End Set
    End Property

    Public Property IdUbicacion As String
        Get
            Return vgc_IdUbicacion
        End Get
        Set(value As String)
            vgc_IdUbicacion = value
        End Set
    End Property

    Public Property Anno As Integer
        Get
            Return vgn_Anno
        End Get
        Set(value As Integer)
            vgn_Anno = value
        End Set
    End Property

    Public Property NumContrato As String
        Get
            Return vgn_NoContrato
        End Get
        Set(value As String)
            vgn_NoContrato = value
        End Set
    End Property

    Public Property NombreContrato As String
        Get
            Return vgc_NombreContrato
        End Get
        Set(value As String)
            vgc_NombreContrato = value
        End Set
    End Property
#End Region

#Region "Eventos"

#End Region

#Region "Metodos"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Inicializar()
        Inicializar(IdOrdenTrabajo, IdUbicacion, Anno, NumContrato, NombreContrato)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pvc_IdOrdenTrabajo"></param>
    ''' <param name="pvc_IdUbicacion"></param>
    ''' <param name="pvc_Anno"></param>
    ''' <remarks></remarks>
    ''' <Autor>César Bermudez Garcia</Autor>
    ''' <CreationDate>21/01/2016</CreationDate>
    Private Sub Inicializar(pvc_IdOrdenTrabajo As String, pvc_IdUbicacion As String, pvc_Anno As Integer, pvn_NumContrato As String, pvc_NombreContrato As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_DsDatos As DataSet


        vlo_Ws_OT_OrdenesDeTrabajo = New Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials

        Try

            vlo_DsDatos = vlo_Ws_OT_OrdenesDeTrabajo.OTT_ORDEN_TRABAJO_ListarVOtResumenOt(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                    String.Format("{0} = '{1}' AND {2} = {3} AND {4} = {5}",
                                  Modelo.V_OT_RESUMEN_OT.ID_ORDEN_TRABAJO,
                                  pvc_IdOrdenTrabajo,
                                  Modelo.V_OT_RESUMEN_OT.ID_UBICACION,
                                  pvc_IdUbicacion,
                                  Modelo.V_OT_RESUMEN_OT.ANNO,
                                  pvc_Anno),
                    String.Empty, False, 0, 0)


            If vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                Dim rows = vlo_DsDatos.Tables(0).Rows(0)
                Me.lblNOrden.Text = rows.Item(Modelo.V_OT_RESUMEN_OT.ID_ORDEN_TRABAJO)

                If rows.Item(Modelo.V_OT_RESUMEN_OT.REQ_FICHA) = FichaTecnica.REQUIERE_FICHA_TECNICA Then
                    Me.trNombreProyecto.Visible = True

                    If vlo_DsDatos.Tables(0).Rows(0)(Modelo.V_OT_RESUMEN_OT.NOMBRE_PROYECTO) Is Nothing Then
                        Me.lblNombreProyecto.Text = String.Empty
                    Else
                        Me.lblNombreProyecto.Text = vlo_DsDatos.Tables(0).Rows(0)(Modelo.V_OT_RESUMEN_OT.NOMBRE_PROYECTO).ToString
                    End If
                Else
                    Me.trNombreProyecto.Visible = False
                End If

                Select Case rows.Item(Modelo.V_OT_RESUMEN_OT.TIPO_ORDEN_TRABAJO)
                    Case TipoOrden.ORDINARIA
                        Me.lblTipoOrden.Text = "Ordinaria"
                    Case TipoOrden.EMERGENCIA
                        Me.lblTipoOrden.Text = "Emergencia"
                    Case TipoOrden.PREVENTIVO
                        Me.lblTipoOrden.Text = "Preventivo"
                    Case TipoOrden.GESTION_EXTERNA
                        Me.lblTipoOrden.Text = "Gestión Externa"
                End Select

                If pvn_NumContrato IsNot Nothing AndAlso pvn_NumContrato <> "-" Then
                    Me.trNumContrato.Visible = True
                Else
                    Me.trNumContrato.Visible = False
                End If

                If pvn_NumContrato IsNot Nothing AndAlso pvc_NombreContrato <> "-" Then
                    Me.trNombreContrato.Visible = True
                Else
                    Me.trNombreContrato.Visible = False
                End If

                Me.lblUnidadSolicitante.Text = rows.Item(Modelo.V_OT_RESUMEN_OT.UNIDAD_SOLICITANTE)
                Me.lblResponsable.Text = rows.Item(Modelo.V_OT_RESUMEN_OT.ENCARGADO)
                Me.lblTaller.Text = rows.Item(Modelo.V_OT_RESUMEN_OT.TALLER)
                Me.lblCategoria.Text = rows.Item(Modelo.V_OT_RESUMEN_OT.CATEGORIA)
                Me.lblNContrato.Text = pvn_NumContrato
                Me.lblNombreContrato.Text = pvc_NombreContrato
                Me.imgDescripcion.Attributes.Add("title", rows.Item(Modelo.V_OT_RESUMEN_OT.DESCRIPCION_TRABAJO))

            End If
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Funciones"

#End Region
End Class
