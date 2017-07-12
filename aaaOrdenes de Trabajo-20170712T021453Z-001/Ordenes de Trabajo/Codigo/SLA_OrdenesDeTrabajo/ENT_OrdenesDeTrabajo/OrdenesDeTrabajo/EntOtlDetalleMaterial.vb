Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOtlDetalleMaterial
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdDetalleMaterial AS Integer 'Llave primaria de la tabla ott_detalle_material que se asocia con la secuencia sq_id_detalle_material
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgc_IdOrdenTrabajo AS String 'Identificador único alfanumérico de la orden de trabajo
	Private vgn_IdUbicacionAdministra AS Integer 'Sede que administra el catálogo de materiales
	Private vgn_IdMaterial AS Integer 'Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación
	Private vgn_CantidadSolicitada AS Double 'Cantidad de material solicitado
	Private vgc_Detalle AS String 'Particularidades del material, ejemplo color
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla ott_detalle_material que se asocia con la secuencia sq_id_detalle_material
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdDetalleMaterial() As Integer
		Get
			Return vgn_IdDetalleMaterial
		End Get
		Set(ByVal value As Integer)
			vgn_IdDetalleMaterial = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
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
	''' Identificador único alfanumérico de la orden de trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdOrdenTrabajo() As String
		Get
			Return vgc_IdOrdenTrabajo
		End Get
		Set(ByVal value As String)
			vgc_IdOrdenTrabajo = value
		End Set
	End Property

	''' <summary>
	''' Sede que administra el catálogo de materiales
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
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
	''' Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
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
	''' Cantidad de material solicitado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CantidadSolicitada() As Double
		Get
			Return vgn_CantidadSolicitada
		End Get
		Set(ByVal value As Double)
			vgn_CantidadSolicitada = value
		End Set
	End Property

	''' <summary>
	''' Particularidades del material, ejemplo color
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
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
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
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
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
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
		vgn_IdDetalleMaterial = 0
		vgn_IdUbicacion = 0
		vgc_IdOrdenTrabajo = String.Empty
		vgn_IdUbicacionAdministra = 0
		vgn_IdMaterial = 0
		vgn_CantidadSolicitada = 0
		vgc_Detalle = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
