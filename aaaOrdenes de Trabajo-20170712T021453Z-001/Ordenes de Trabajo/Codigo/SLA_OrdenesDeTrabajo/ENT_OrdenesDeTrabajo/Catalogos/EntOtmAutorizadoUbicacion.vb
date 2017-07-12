Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmAutorizadoUbicacion
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUbicacionAdministra AS Integer 'Id de la ubicación encargada de la administración
	Private vgn_NumEmpleado AS Double 'Número de empleado autorizado en la sede
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Id de la ubicación encargada de la administración
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/09/2015 09:44:09 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdUbicacionAdministra() As Integer
		Get
			Return vgn_IdUbicacionAdministra
		End Get
		Set(ByVal value As Integer)
			vgn_IdUbicacionAdministra = value
		End Set
	End Property

	''' <summary>
	''' Número de empleado autorizado en la sede
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/09/2015 09:44:09 a.m.</creationDate>
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
	''' <creationDate>29/09/2015 09:44:09 a.m.</creationDate>
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
	''' <creationDate>29/09/2015 09:44:09 a.m.</creationDate>
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
		vgn_IdUbicacionAdministra = 0
		vgn_NumEmpleado = 0
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
