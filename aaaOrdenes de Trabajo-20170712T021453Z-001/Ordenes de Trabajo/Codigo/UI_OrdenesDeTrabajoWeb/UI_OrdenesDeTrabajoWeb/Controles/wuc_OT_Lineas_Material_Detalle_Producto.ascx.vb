Imports Utilerias.OrdenesDeTrabajo
Imports System.Data

Partial Class Controles_wuc_OT_Lineas_Material_Detalle_Producto
    Inherits System.Web.UI.UserControl

    Public Event Recargar(ByVal pvc_HileraMateriales As String)

#Region "Propiedades"

    ''' <summary>
    ''' Propiedad para IdUbicacion de la linea
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property HileraMateriales As String
        Get
            Return CType(ViewState("HileraMateriales"), String)
        End Get
        Set(value As String)
            ViewState("HileraMateriales") = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para IdMaterial de la linea
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Property IdMaterial As Integer
        Get
            Return CType(ViewState("IdMaterial"), Integer)
        End Get
        Set(value As Integer)
            ViewState("IdMaterial") = value
        End Set
    End Property

    Public Property DsMateriales As DataSet
        Get
            Return CType(ViewState("DsMateriales"), DataSet)
        End Get
        Set(value As DataSet)
            ViewState("DsMateriales") = value
        End Set
    End Property

    Public Property GestionCompra As Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra
        Get
            Return CType(ViewState("GestionCompra"), Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra)
        End Get
        Set(value As Wsr_OT_OrdenesDeTrabajo.EntOttGestionCompra)
            ViewState("GestionCompra") = value
        End Set
    End Property



#End Region

#Region "Eventos"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Protected Sub ibBorrar_Click(sender As Object, e As ImageClickEventArgs)
        Try
            BorrarLineaMaterial(CType(CType(sender, ImageButton).CommandArgument, String))
        Catch ex As Exception
            Dim vlo_ControlDeErrores As New ControlDeErrores
            vlo_ControlDeErrores.RegistrarExcepcion(ex, String.Empty)
        End Try
    End Sub

#End Region

#Region "Métodos"
    ''' <summary>
    ''' Llama los métodos que inicializan el formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Sub Inicializar()
        CargarListaLineasMaterial(Me.HileraMateriales, Me.IdMaterial)
    End Sub

    ''' <summary>
    ''' carga la lista de lineas de material, de la gestion
    ''' </summary>
    ''' <param name="pvc_HileraMateriales"></param>
    ''' <param name="pvn_IdMaterial"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Private Sub CargarListaLineasMaterial(pvc_HileraMateriales As String, pvn_IdMaterial As Integer)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1

        Try
            Me.DsMateriales = vlo_Ws_OT_OrdenesDeTrabajo.OTT_DETALLE_MATERIAL_ListarVOtDetalleMaterial(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} IN ({1}) AND {2} = {3}", Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL, pvc_HileraMateriales, Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL, pvn_IdMaterial),
                String.Empty,
                False,
                0,
                0)

            Me.DsMateriales.Tables(0).PrimaryKey = New System.Data.DataColumn() {Me.DsMateriales.Tables(0).Columns(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL)}

            If Me.DsMateriales IsNot Nothing AndAlso Me.DsMateriales.Tables(0).Rows.Count > 0 Then

                Me.rpLineas.DataSource = Me.DsMateriales
                Me.rpLineas.DataMember = Me.DsMateriales.Tables(0).TableName
                Me.rpLineas.DataBind()
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
    ''' 
    ''' </summary>
    ''' <param name="pvn_CommandArgument"></param>
    ''' <remarks></remarks>
    ''' <author>Mauricio Salas Chaves</author>
    ''' <creationDate>08/09/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Sub BorrarLineaMaterial(pvn_CommandArgument As String)
        Dim vlo_Ws_OT_OrdenesDeTrabajo As Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        Dim vlo_EntOttLineaGestionCompra As Wsr_OT_OrdenesDeTrabajo.EntOttLineaGestionCompra
        Dim vlo_ClaveArgument() As Object
        Dim vlo_Clave() As Object
        Dim vlc_NuevaHilera As String

        vlo_Ws_OT_OrdenesDeTrabajo = New Wsr_OT_OrdenesDeTrabajo.Ws_OT_OrdenesDeTrabajo
        vlo_Ws_OT_OrdenesDeTrabajo.Credentials = System.Net.CredentialCache.DefaultCredentials
        vlo_Ws_OT_OrdenesDeTrabajo.Timeout = -1

        Try
            'Se elimina el IdDetalleMaterial de la hilera de seleccionados
            vlc_NuevaHilera = String.Empty
            vlo_Clave = Me.HileraMateriales.Split(",")
            vlo_ClaveArgument = pvn_CommandArgument.Split(",")

            For Each vln_Registro In vlo_Clave
                If vln_Registro <> vlo_ClaveArgument(0) Then
                    vlc_NuevaHilera = String.Format("{0}{1},", vlc_NuevaHilera, vln_Registro)
                End If
            Next

            'Se corta el ultimo caracter de la hilera que es ","
            Me.HileraMateriales = Mid(vlc_NuevaHilera, 1, Len(vlc_NuevaHilera) - 1)

            'Si esta asociado a la gestion de compra se procede a borrar el archivo a nivel de base de datos y se refresca
            If vlo_ClaveArgument(1) = 1 Then
                vlo_EntOttLineaGestionCompra = vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_GESTION_COMPRA_ObtenerRegistro(
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB),
                String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION, Me.GestionCompra.IdUbicacion, Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, Me.GestionCompra.IdViaCompraContrato, Modelo.OTT_LINEA_GESTION_COMPRA.ANNO, Me.GestionCompra.Anno, Modelo.OTT_LINEA_GESTION_COMPRA.ID_DETALLE_MATERIAL, vlo_ClaveArgument(0)))

                vlo_Ws_OT_OrdenesDeTrabajo.OTT_LINEA_GESTION_COMPRA_BorrarRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_WEB),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_WEB), vlo_EntOttLineaGestionCompra)

            End If
            
            'Se eliminar el campo del dataset que se muestra en pantalla
            'vlo_Row = Me.DsMateriales.Tables(0).NewRow

            'Me.DsMateriales.Tables(0).Rows.Find(pvn_IdDetalleMaterial).Delete()


            'ActualizarRpMateriales()


            RaiseEvent Recargar(Me.HileraMateriales)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ActualizarRpMateriales()
        Dim VDsOpciones As DataView = New DataView(Me.DsMateriales.Tables(0))
        Dim vln_Cantidad As Integer

        vln_Cantidad = CantidadRegistros(Me.DsMateriales.Tables(0))
        'VDsOpciones.Sort = Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL

        If VDsOpciones IsNot Nothing AndAlso vln_Cantidad > 0 Then
            Me.rpLineas.DataSource = VDsOpciones
            Me.rpLineas.DataMember = VDsOpciones.Table.TableName
            Me.rpLineas.DataBind()

        Else
            Me.rpLineas.DataSource = Nothing
            Me.rpLineas.DataBind()
        End If

    End Sub

#End Region

#Region "Funciones"
    Private Function CantidadRegistros(ByVal pvo_Dataset As DataTable) As Integer
        Dim vlo_Row As DataRow
        Dim vln_Cantidad As Integer = 0

        vlo_Row = pvo_Dataset.NewRow

        For Each vlo_Row In pvo_Dataset.Rows
            If vlo_Row.RowState <> DataRowState.Deleted Then
                vln_Cantidad = vln_Cantidad + 1
            End If
        Next
        Return vln_Cantidad
    End Function
#End Region

End Class
