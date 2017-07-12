Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace ORDENES_TRABAJO.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmCorreoProveedor
		Inherits EntBase
#Region "Atributos"
	Private vgc_Identificacion AS String 'Identificación del proveedor (física o jurídica)
	Private vgc_Correo AS String 'Correo del proveedor
	Private vgc_Nombre AS String 'Nombre del contacto
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Identificación del proveedor (física o jurídica)
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:37:27 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Identificacion() As String
		Get
			Return vgc_Identificacion
		End Get
		Set(ByVal value As String)
			vgc_Identificacion = value
		End Set
	End Property

	''' <summary>
	''' Correo del proveedor
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:37:27 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Correo() As String
		Get
			Return vgc_Correo
		End Get
		Set(ByVal value As String)
			vgc_Correo = value
		End Set
	End Property

	''' <summary>
	''' Nombre del contacto
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:37:27 p.m.</creationDate>
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
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:37:27 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Usuario() As String
		Get
			Return vgc_Usuario
		End Get
		Set(ByVal value As String)
			vgc_Usuario = value
		End Set
	End Property

	''' <summary>
	''' Control de concurrencia - valor por defecto: fecha y hora del sistema
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:37:27 p.m.</creationDate>
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
		vgc_Identificacion = String.Empty
		vgc_Correo = String.Empty
		vgc_Nombre = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
