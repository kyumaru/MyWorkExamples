Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace TallerCapacitacion.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntTpTcDatosAutor
		Inherits EntBase
#Region "Atributos"
	Private vgc_NombreCompleto AS String
	Private vgc_IdPersonal AS String
	Private vgc_NombreCompleto AS String
	Private vgc_IdPersonal AS String
#End Region
#Region "Propiedades"
	''' <summary>
	''' 
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NombreCompleto() As String
		Get
			Return vgc_NombreCompleto
		End Get
		Set(ByVal value As String)
			vgc_NombreCompleto = value
		End Set
	End Property

	''' <summary>
	''' 
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

	''' <summary>
	''' 
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NombreCompleto() As String
		Get
			Return vgc_NombreCompleto
		End Get
		Set(ByVal value As String)
			vgc_NombreCompleto = value
		End Set
	End Property

	''' <summary>
	''' 
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
		vgc_NombreCompleto = String.Empty
		vgc_IdPersonal = String.Empty
		vgc_NombreCompleto = String.Empty
		vgc_IdPersonal = String.Empty
	End Sub
#End Region
	End Class
End Namespace
