Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtpParametro
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdParametro AS Integer 'Llave primaria de la tabla otp_parametro
	Private vgc_Descripcion AS String 'Descripción del parámetro
	Private vgc_Valor AS String 'Valor del parámetro
	Private vgn_ValorDecimal AS Double 'Valor de tipo decimal del parámetro
	Private vgn_Protegido AS Integer 'Indicador que especifica si el parámetro se encuentra protegido y no se puede modificar desde un mantenimiento - 0: no es protegido, 1: es protegido - valor por defecto: 0
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otp_parametro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/09/2015 01:54:27 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdParametro() As Integer
		Get
			Return vgn_IdParametro
		End Get
		Set(ByVal value As Integer)
			vgn_IdParametro = value
		End Set
	End Property

	''' <summary>
	''' Descripción del parámetro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/09/2015 01:54:27 p.m.</creationDate>
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
	''' Valor del parámetro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/09/2015 01:54:27 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Valor() As String
		Get
			Return vgc_Valor
		End Get
		Set(ByVal value As String)
			vgc_Valor = value
		End Set
	End Property

	''' <summary>
	''' Valor de tipo decimal del parámetro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/09/2015 01:54:27 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property ValorDecimal() As Double
		Get
			Return vgn_ValorDecimal
		End Get
		Set(ByVal value As Double)
			vgn_ValorDecimal = value
		End Set
	End Property

	''' <summary>
	''' Indicador que especifica si el parámetro se encuentra protegido y no se puede modificar desde un mantenimiento - 0: no es protegido, 1: es protegido - valor por defecto: 0
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/09/2015 01:54:27 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Protegido() As Integer
		Get
			Return vgn_Protegido
		End Get
		Set(ByVal value As Integer)
			vgn_Protegido = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/09/2015 01:54:27 p.m.</creationDate>
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
	''' <creationDate>09/09/2015 01:54:27 p.m.</creationDate>
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
		vgn_IdParametro = 0
		vgc_Descripcion = String.Empty
		vgc_Valor = String.Empty
		vgn_ValorDecimal = 0
		vgn_Protegido = 0
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
