Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOtcEstadoGestionCompra
		Inherits EntBase
#Region "Atributos"
	Private vgc_Estado AS String 'Estado
	Private vgc_Descripcion AS String 'Descripción
#End Region
#Region "Propiedades"
	''' <summary>
	''' Estado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:54 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Estado() As String
		Get
			Return vgc_Estado
		End Get
		Set(ByVal value As String)
			vgc_Estado = value
		End Set
	End Property

	''' <summary>
	''' Descripción
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:54 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Descripcion() As String
		Get
			Return vgc_Descripcion
		End Get
		Set(ByVal value As String)
			vgc_Descripcion = value
		End Set
	End Property

#End Region

#Region "Constructores"
	Public Sub New()
		MyBase.New()
		vgc_Estado = String.Empty
		vgc_Descripcion = String.Empty
	End Sub
#End Region
	End Class
End Namespace
