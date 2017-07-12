Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace TallerCapacitacion.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntTccCondicionLibro
		Inherits EntBase
#Region "Atributos"
	Private vgc_CondicionLibro AS String 'Abreviatura que identifica la condición de un libro
	Private vgc_Descripcion AS String 'Descripción de la condición del libro
#End Region
#Region "Propiedades"
	''' <summary>
	''' Abreviatura que identifica la condición de un libro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 4:34:41 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CondicionLibro() As String
		Get
			Return vgc_CondicionLibro
		End Get
		Set(ByVal value As String)
			vgc_CondicionLibro = value
		End Set
	End Property

	''' <summary>
	''' Descripción de la condición del libro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 4:34:41 p. m.</creationDate>
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
		vgc_CondicionLibro = String.Empty
		vgc_Descripcion = String.Empty
	End Sub
#End Region
	End Class
End Namespace
