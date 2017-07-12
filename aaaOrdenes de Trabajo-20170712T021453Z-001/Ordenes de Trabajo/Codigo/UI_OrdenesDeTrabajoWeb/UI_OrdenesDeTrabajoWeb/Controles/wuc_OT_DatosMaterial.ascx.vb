
Partial Class Controles_wuc_OT_DatosMaterial
    Inherits System.Web.UI.UserControl

#Region "Métodos"

    Public Sub AsignaDescripcion(pvo_Descripcion As String)
        Me.lblDescripcion.Text = pvo_Descripcion
    End Sub

    Public Sub AsignaCategoria(pvc_Categoria As String)
        Me.lblCategoria.Text = pvc_Categoria
    End Sub

    Public Sub AsignaSubCategoria(pvc_SubCategoria As String)
        Me.lblSubCategoria.Text = pvc_SubCategoria
    End Sub

    Public Sub AsignaCantidad(pvc_SubCantidad As String)
        Me.txtCantidad.Text = pvc_SubCantidad
    End Sub

    Public Sub AsignaMontoPromedio(pvo_MontoPromedio As String)
        If String.IsNullOrWhiteSpace(pvo_MontoPromedio) Then
            Me.lblMontoPromedio.Text = String.Empty
        Else
            Me.lblMontoPromedio.Text = String.Format("₡{0}", pvo_MontoPromedio)
        End If
        hdfMonto.Value = pvo_MontoPromedio
    End Sub

    Public Sub AsignaDetalle(pvc_AsignaDetalle As String)
        Me.txtDetalle.Text = pvc_AsignaDetalle
    End Sub

    Public Sub AsignaUnidadMedida(pvc_UnidadMedida As String)
        Me.lblUnidadMedida.Text = pvc_UnidadMedida
    End Sub

#End Region

#Region "Funciones"

    Public Function RetornaDescripcion() As String
        Return Me.lblDescripcion.Text
    End Function

    Public Function RetornaCategoria() As String
        Return Me.lblCategoria.Text
    End Function

    Public Function RetornaSubCategoria() As String
        Return Me.lblSubCategoria.Text
    End Function

    Public Function RetornaCantidad() As String
        Return Me.txtCantidad.Text
    End Function

    Public Function RetornaMontoPromedio() As String
        Return hdfMonto.Value
    End Function

    Public Function RetornaDetalle() As String
        Return Me.txtDetalle.Text
    End Function

    Public Function RetornaUnidadMedida() As String
        Return Me.lblUnidadMedida.Text
    End Function

#End Region

End Class
