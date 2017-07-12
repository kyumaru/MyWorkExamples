Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace ORDENES_TRABAJO.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmTelefonoProveedor
		Inherits EntBase
#Region "Atributos"
	Private vgc_Identificacion AS String 'Identificación del proveedor (física o jurídica)
	Private vgc_Telefono AS String 'Teléfono del proveedor
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Identificación del proveedor (física o jurídica)
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:38:48 p.m.</creationDate>
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
	''' Teléfono del proveedor
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:38:48 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Telefono() As String
		Get
			Return vgc_Telefono
		End Get
		Set(ByVal value As String)
			vgc_Telefono = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:38:48 p.m.</creationDate>
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
	''' <creationDate>04/08/2016 03:38:48 p.m.</creationDate>
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
		vgc_Telefono = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
