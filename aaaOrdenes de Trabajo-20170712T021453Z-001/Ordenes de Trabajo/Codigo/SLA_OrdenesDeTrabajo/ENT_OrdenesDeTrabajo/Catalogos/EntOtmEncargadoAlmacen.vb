Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmEncargadoAlmacen
		Inherits EntBase
#Region "Atributos"
	Private vgn_NumEmpleado AS Double 'Número de empleado del encargado
	Private vgn_IdUbicacionAdministra AS Integer 'Id de la ubicación que administra los datos del catálogo
	Private vgc_Rol AS String 'Rol del encargado.  enc -encargado, ali - alistador, des -despachador
	Private vgc_Estado AS String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
#End Region
#Region "Propiedades"
	''' <summary>
	''' Número de empleado del encargado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/06/2016 07:52:43 a.m.</creationDate>
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
	''' Id de la ubicación que administra los datos del catálogo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/06/2016 07:52:43 a.m.</creationDate>
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
	''' Rol del encargado.  enc -encargado, ali - alistador, des -despachador
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/06/2016 07:52:43 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Rol() As String
		Get
			Return vgc_Rol
		End Get
		Set(ByVal value As String)
			vgc_Rol = value
		End Set
	End Property

	''' <summary>
	''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/06/2016 07:52:43 a.m.</creationDate>
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
	''' Control de concurrencia - valor por defecto: fecha y hora del sistema
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/06/2016 07:52:43 a.m.</creationDate>
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
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/06/2016 07:52:43 a.m.</creationDate>
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
		vgn_NumEmpleado = 0
		vgn_IdUbicacionAdministra = 0
		vgc_Rol = String.Empty
		vgc_Estado = String.Empty
		vgd_TimeStamp = DateTime.Now
		vgc_Usuario = String.Empty
	End Sub
#End Region
	End Class
End Namespace
