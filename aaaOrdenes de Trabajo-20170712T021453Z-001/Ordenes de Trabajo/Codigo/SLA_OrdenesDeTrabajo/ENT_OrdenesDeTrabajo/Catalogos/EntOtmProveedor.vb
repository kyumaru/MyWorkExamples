Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace ORDENES_TRABAJO.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmProveedor
		Inherits EntBase
#Region "Atributos"
	Private vgc_Identificacion AS String 'Identificación del proveedor (física o jurídica)
	Private vgc_TipoProveedor AS String 'Tipo de identificación del proveedor: fis - físico, jur - jurídico
	Private vgc_Nombre AS String 'Nombre del proveedor
	Private vgc_Direccion AS String 'Dirección
	Private vgc_SitioWeb AS String 'Dirección del sitio web
	Private vgc_PersonaContacto AS String 'Nombre completo de la persona contacto
	Private vgc_Observaciones AS String 'Observaciones
	Private vgc_Estado AS String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Identificación del proveedor (física o jurídica)
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
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
	''' Tipo de identificación del proveedor: fis - físico, jur - jurídico
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property TipoProveedor() As String
		Get
			Return vgc_TipoProveedor
		End Get
		Set(ByVal value As String)
			vgc_TipoProveedor = value
		End Set
	End Property

	''' <summary>
	''' Nombre del proveedor
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
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
	''' Dirección
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Direccion() As String
		Get
			Return vgc_Direccion
		End Get
		Set(ByVal value As String)
			vgc_Direccion = value
		End Set
	End Property

	''' <summary>
	''' Dirección del sitio web
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property SitioWeb() As String
		Get
			Return vgc_SitioWeb
		End Get
		Set(ByVal value As String)
			vgc_SitioWeb = value
		End Set
	End Property

	''' <summary>
	''' Nombre completo de la persona contacto
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property PersonaContacto() As String
		Get
			Return vgc_PersonaContacto
		End Get
		Set(ByVal value As String)
			vgc_PersonaContacto = value
		End Set
	End Property

	''' <summary>
	''' Observaciones
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Observaciones() As String
		Get
			Return vgc_Observaciones
		End Get
		Set(ByVal value As String)
			vgc_Observaciones = value
		End Set
	End Property

	''' <summary>
	''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
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
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
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
	''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
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
		vgc_TipoProveedor = String.Empty
		vgc_Nombre = String.Empty
		vgc_Direccion = String.Empty
		vgc_SitioWeb = String.Empty
		vgc_PersonaContacto = String.Empty
		vgc_Observaciones = String.Empty
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
