Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttDetAprovisionamiento
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_NumeroGestion AS Integer 'Consecutivo de la gestión
	Private vgn_Anno AS Integer 'Año
	Private vgn_IdMaterial AS Integer 'Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación
	Private vgn_Cantidad AS Double 'Cantidad
	Private vgc_Observaciones AS String 'Observaciones
	Private vgc_Estado AS String 'Epr - en proceso,ing - ingreso en almacen,inf - infructuosa
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:14 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdUbicacion() As Integer
		Get
			Return vgn_IdUbicacion
		End Get
		Set(ByVal value As Integer)
			vgn_IdUbicacion = value
		End Set
	End Property

	''' <summary>
	''' Consecutivo de la gestión
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:14 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NumeroGestion() As Integer
		Get
			Return vgn_NumeroGestion
		End Get
		Set(ByVal value As Integer)
			vgn_NumeroGestion = value
		End Set
	End Property

	''' <summary>
	''' Año
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:14 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Anno() As Integer
		Get
			Return vgn_Anno
		End Get
		Set(ByVal value As Integer)
			vgn_Anno = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:14 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdMaterial() As Integer
		Get
			Return vgn_IdMaterial
		End Get
		Set(ByVal value As Integer)
			vgn_IdMaterial = value
		End Set
	End Property

	''' <summary>
	''' Cantidad
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:14 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Cantidad() As Double
		Get
			Return vgn_Cantidad
		End Get
		Set(ByVal value As Double)
			vgn_Cantidad = value
		End Set
	End Property

	''' <summary>
	''' Observaciones
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:14 p.m.</creationDate>
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
	''' Epr - en proceso,ing - ingreso en almacen,inf - infructuosa
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:14 p.m.</creationDate>
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
	''' <creationDate>11/01/2017 01:45:14 p.m.</creationDate>
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
	''' <creationDate>11/01/2017 01:45:14 p.m.</creationDate>
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
		vgn_IdUbicacion = 0
		vgn_NumeroGestion = 0
		vgn_Anno = 0
		vgn_IdMaterial = 0
		vgn_Cantidad = 0
		vgc_Observaciones = String.Empty
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
