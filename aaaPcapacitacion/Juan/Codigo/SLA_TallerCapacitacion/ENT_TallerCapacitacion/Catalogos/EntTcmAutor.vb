Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace TallerCapacitacion.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntTcmAutor
		Inherits EntBase
#Region "Atributos"
	Private vgc_IdPersonal AS String 'Número de identificación
	Private vgc_Nombre AS String 'Nombre del autor
	Private vgc_PrimerApellido AS String 'Primer apellido del autor
	Private vgc_SegundoApellido AS String 'Segundo apellido del autor
	Private vgd_FechaHoraNacimiento AS DateTime 'Fecha y hora de nacimiento
	Private vgc_Estado AS String 'Estado del registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia
	Private vgc_Usuario AS String 'Usuario que manipula la información del registro
#End Region
#Region "Propiedades"
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

	''' <summary>
	''' Nombre del autor
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Nombre() As String
		Get
			Return vgc_Nombre
		End Get
		Set(ByVal value As String)
			vgc_Nombre = value
		End Set
	End Property

	''' <summary>
	''' Primer apellido del autor
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property PrimerApellido() As String
		Get
			Return vgc_PrimerApellido
		End Get
		Set(ByVal value As String)
			vgc_PrimerApellido = value
		End Set
	End Property

	''' <summary>
	''' Segundo apellido del autor
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property SegundoApellido() As String
		Get
			Return vgc_SegundoApellido
		End Get
		Set(ByVal value As String)
			vgc_SegundoApellido = value
		End Set
	End Property

	''' <summary>
	''' Fecha y hora de nacimiento
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaHoraNacimiento() As DateTime
		Get
			Return vgd_FechaHoraNacimiento
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaHoraNacimiento = value
		End Set
	End Property

	''' <summary>
	''' Estado del registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
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
		vgc_IdPersonal = String.Empty
		vgc_Nombre = String.Empty
		vgc_PrimerApellido = String.Empty
		vgc_SegundoApellido = String.Empty
		vgd_FechaHoraNacimiento = DateTime.Now
		vgc_Estado = String.Empty
		vgd_TimeStamp = DateTime.Now
		vgc_Usuario = String.Empty
	End Sub
#End Region
	End Class
End Namespace
