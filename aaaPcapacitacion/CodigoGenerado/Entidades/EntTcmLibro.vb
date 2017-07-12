Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace TallerCapacitacion.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntTcmLibro
		Inherits EntBase
#Region "Atributos"
	Private vgc_Isbn AS String 'Número estándar internacional de libros. es un identificador único para libros previsto para uso comercial, los hay de 10 y 13 dígitos
	Private vgc_CondicionLibro AS String 'Abreviatura que identifica la condición de un libro
	Private vgc_Titulo AS String 'Título del libro
	Private vgc_Resumen AS String 'Resumen del libro
	Private vgn_TotalPaginas AS Integer 'Indica la cantidad de páginas del libro. solo permite registrar libros con más de 10 páginas hasta un máximo de 100
	Private vgd_FechaHoraImpresion AS DateTime 'Fecha y hora en la cual fue impreso el libro
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia
	Private vgc_Usuario AS String 'Usuario que manipula la información del registro
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
	''' Abreviatura que identifica la condición de un libro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
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
	''' Título del libro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Titulo() As String
		Get
			Return vgc_Titulo
		End Get
		Set(ByVal value As String)
			vgc_Titulo = value
		End Set
	End Property

	''' <summary>
	''' Resumen del libro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Resumen() As String
		Get
			Return vgc_Resumen
		End Get
		Set(ByVal value As String)
			vgc_Resumen = value
		End Set
	End Property

	''' <summary>
	''' Indica la cantidad de páginas del libro. solo permite registrar libros con más de 10 páginas hasta un máximo de 100
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property TotalPaginas() As Integer
		Get
			Return vgn_TotalPaginas
		End Get
		Set(ByVal value As Integer)
			vgn_TotalPaginas = value
		End Set
	End Property

	''' <summary>
	''' Fecha y hora en la cual fue impreso el libro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaHoraImpresion() As DateTime
		Get
			Return vgd_FechaHoraImpresion
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaHoraImpresion = value
		End Set
	End Property

	''' <summary>
	''' Control de concurrencia
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property TimeStamp() As DateTime
		Get
			Return vgd_TimeStamp
		End Get
		Set(ByVal value As DateTime)
			vgd_TimeStamp = value
		End Set
	End Property

	''' <summary>
	''' Usuario que manipula la información del registro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Usuario() As String
		Get
			Return vgc_Usuario
		End Get
		Set(ByVal value As String)
			vgc_Usuario = value
		End Set
	End Property

#End Region

#Region "Constructores"
	Public Sub New()
		MyBase.New()
		vgc_Isbn = String.Empty
		vgc_CondicionLibro = String.Empty
		vgc_Titulo = String.Empty
		vgc_Resumen = String.Empty
		vgn_TotalPaginas = 0
		vgd_FechaHoraImpresion = DateTime.Now
		vgd_TimeStamp = DateTime.Now
		vgc_Usuario = String.Empty
	End Sub
#End Region
	End Class
End Namespace
