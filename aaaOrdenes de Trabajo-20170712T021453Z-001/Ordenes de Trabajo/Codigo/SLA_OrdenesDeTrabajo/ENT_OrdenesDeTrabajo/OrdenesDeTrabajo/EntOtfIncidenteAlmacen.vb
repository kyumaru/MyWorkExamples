Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOtfIncidenteAlmacen
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdIncidenteAlmacen AS Integer 'Llave primaria de la tabla otf_incidente_almacen asociada a la secuencia sq_id_incidente_almacen
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_IdMaterial AS Integer 'Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación
	Private vgn_IdAlmacenBodega AS Integer 'Llave primaria de la tabla otm_almacen_bodega que se asocia con la secuencia sq_id_almacen_bodega
	Private vgn_IdTipoIncidente AS Integer 'Llave primaria de la tabla otm_tipo_incidente asociada a la secuencia  sq_id_tipo_incidente
	Private vgc_Detalle AS String 'Detalle del incidente presentado
	Private vgc_Estado AS String 'Estado del incidente. cre - creado, pen - pendiente, ate - atendido
	Private vgd_FechaInclusion AS DateTime 'Fecha de inclusión del incidente, para priorización.
	Private vgc_ObservacionesRevisor AS String 'Observaciones del revisor del incidente
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otf_incidente_almacen asociada a la secuencia sq_id_incidente_almacen
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdIncidenteAlmacen() As Integer
		Get
			Return vgn_IdIncidenteAlmacen
		End Get
		Set(ByVal value As Integer)
			vgn_IdIncidenteAlmacen = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
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
	''' Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
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
	''' Llave primaria de la tabla otm_almacen_bodega que se asocia con la secuencia sq_id_almacen_bodega
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdAlmacenBodega() As Integer
		Get
			Return vgn_IdAlmacenBodega
		End Get
		Set(ByVal value As Integer)
			vgn_IdAlmacenBodega = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_tipo_incidente asociada a la secuencia  sq_id_tipo_incidente
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdTipoIncidente() As Integer
		Get
			Return vgn_IdTipoIncidente
		End Get
		Set(ByVal value As Integer)
			vgn_IdTipoIncidente = value
		End Set
	End Property

	''' <summary>
	''' Detalle del incidente presentado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Detalle() As String
		Get
			Return vgc_Detalle
		End Get
		Set(ByVal value As String)
			vgc_Detalle = value
		End Set
	End Property

	''' <summary>
	''' Estado del incidente. cre - creado, pen - pendiente, ate - atendido
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
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
	''' Fecha de inclusión del incidente, para priorización.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaInclusion() As DateTime
		Get
			Return vgd_FechaInclusion
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaInclusion = value
		End Set
	End Property

	''' <summary>
	''' Observaciones del revisor del incidente
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property ObservacionesRevisor() As String
		Get
			Return vgc_ObservacionesRevisor
		End Get
		Set(ByVal value As String)
			vgc_ObservacionesRevisor = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
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
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
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
		vgn_IdIncidenteAlmacen = 0
		vgn_IdUbicacion = 0
		vgn_IdMaterial = 0
		vgn_IdAlmacenBodega = 0
		vgn_IdTipoIncidente = 0
		vgc_Detalle = String.Empty
		vgc_Estado = String.Empty
		vgd_FechaInclusion = DateTime.Now
		vgc_ObservacionesRevisor = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
