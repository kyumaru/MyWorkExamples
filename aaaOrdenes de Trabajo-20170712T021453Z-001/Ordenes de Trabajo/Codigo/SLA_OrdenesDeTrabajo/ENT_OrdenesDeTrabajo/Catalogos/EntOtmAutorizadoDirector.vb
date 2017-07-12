Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmAutorizadoDirector
		Inherits EntBase
#Region "Atributos"
	Private vgn_CodUnidadSirh AS Integer 'Código de la unidad donde estará autorizado el funcionario
	Private vgn_NumEmpleado AS Double 'Número del empleado autorizado
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Código de la unidad donde estará autorizado el funcionario
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/09/2015 10:06:27 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CodUnidadSirh() As Integer
		Get
			Return vgn_CodUnidadSirh
		End Get
		Set(ByVal value As Integer)
			vgn_CodUnidadSirh = value
		End Set
	End Property

	''' <summary>
	''' Número del empleado autorizado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/09/2015 10:06:27 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NumEmpleado() As Double
		Get
			Return vgn_NumEmpleado
		End Get
		Set(ByVal value As Double)
			vgn_NumEmpleado = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/09/2015 10:06:27 p.m.</creationDate>
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
	''' <creationDate>24/09/2015 10:06:27 p.m.</creationDate>
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
		vgn_CodUnidadSirh = 0
		vgn_NumEmpleado = 0
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
