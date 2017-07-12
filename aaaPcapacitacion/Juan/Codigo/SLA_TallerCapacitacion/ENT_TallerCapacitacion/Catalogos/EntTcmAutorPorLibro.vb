Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace TallerCapacitacion.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntTcmAutorPorLibro
		Inherits EntBase
#Region "Atributos"
	Private vgc_Isbn AS String 'Número estándar internacional de libros. es un identificador único para libros previsto para uso comercial, los hay de 10 y 13 dígitos
	Private vgc_IdPersonal AS String 'Número de identificación
#End Region
#Region "Propiedades"
	''' <summary>
	''' Número estándar internacional de libros. es un identificador único para libros previsto para uso comercial, los hay de 10 y 13 dígitos
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Isbn() As String
		Get
			Return vgc_Isbn
		End Get
		Set(ByVal value As String)
			vgc_Isbn = value
		End Set
	End Property

	''' <summary>
	''' Número de identificación
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdPersonal() As String
		Get
			Return vgc_IdPersonal
		End Get
		Set(ByVal value As String)
			vgc_IdPersonal = value
		End Set
	End Property

#End Region

#Region "Constructores"
	Public Sub New()
		MyBase.New()
		vgc_Isbn = String.Empty
		vgc_IdPersonal = String.Empty
	End Sub
#End Region
	End Class
End Namespace
