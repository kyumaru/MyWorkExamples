Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace SistemaTransportes.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntStmServicio
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdServicio AS Integer 'Campo autogenerado que lleva el control de los registros
	Private vgc_Descripcion AS String 'Almacena la descripcion del registro
	Private vgc_TipoServicio AS String 'Indica el tipo de servicio que se puede crear
	Private vgc_Estado AS String 'Almacena el estado del registro
	Private vgc_UsuarioCrea AS String 'Almacena el usuario que crea el registro
	Private vgd_FechaCrea AS DateTime 'Almacena la fecha en que se crea el registro
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia
#End Region
#Region "Propiedades"
	''' <summary>
	''' Campo autogenerado que lleva el control de los registros
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdServicio() As Integer
		Get
			Return vgn_IdServicio
		End Get
		Set(ByVal value As Integer)
			vgn_IdServicio = value
		End Set
	End Property

	''' <summary>
	''' Almacena la descripcion del registro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Descripcion() As String
		Get
			Return vgc_Descripcion
		End Get
		Set(ByVal value As String)
			vgc_Descripcion = value
		End Set
	End Property

	''' <summary>
	''' Indica el tipo de servicio que se puede crear
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property TipoServicio() As String
		Get
			Return vgc_TipoServicio
		End Get
		Set(ByVal value As String)
			vgc_TipoServicio = value
		End Set
	End Property

	''' <summary>
	''' Almacena el estado del registro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
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
	''' Almacena el usuario que crea el registro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property UsuarioCrea() As String
		Get
			Return vgc_UsuarioCrea
		End Get
		Set(ByVal value As String)
			vgc_UsuarioCrea = value
		End Set
	End Property

	''' <summary>
	''' Almacena la fecha en que se crea el registro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaCrea() As DateTime
		Get
			Return vgd_FechaCrea
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaCrea = value
		End Set
	End Property

	''' <summary>
	''' Control de concurrencia
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
	''' <changeLog></changeLog>
	Public Property TimeStamp() As DateTime
		Get
			Return vgd_TimeStamp
		End Get
		Set(ByVal value As DateTime)
			vgd_TimeStamp = value
		End Set
	End Property

#End Region

#Region "Constructores"
	Public Sub New()
		MyBase.New()
		vgn_IdServicio = 0
		vgc_Descripcion = String.Empty
		vgc_TipoServicio = String.Empty
		vgc_Estado = String.Empty
		vgc_UsuarioCrea = String.Empty
		vgd_FechaCrea = DateTime.Now
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
