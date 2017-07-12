Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttSolicitudReingreso
		Inherits EntBase
#Region "Atributos"
	Private vgn_Anno AS Integer 'Llave primaria de la tabla ott_solicitud_reingreso
	Private vgn_IdSolicitudReingreso AS Integer 'Llave primaria de la tabla ott_solicitud_reingreso. consecutivo anual, por ubicación
	Private vgn_IdUbicacionAdministra AS Integer 'Id de la ubicación a la que corresponde el trámite de reingreso
	Private vgn_IdDetalleMaterial AS Integer 'Llave primaria de la tabla ott_detalle_material que se asocia con la secuencia sq_id_detalle_material
	Private vgn_CantidadReingreso AS Double 'Cantidad de material a reingresar al almacén
	Private vgn_CantidadRecibida AS Double 'Cantidad de material recibida en almacén
	Private vgc_TipoSolicitudReingreso AS String 'Tipo de solicitud de reingreso: cus - material en custodia, dev - devolución de material
	Private vgn_CostoCalculado AS Double 'Costo calculado en el momento de la devolución con base a cantidad y costo promedio por unidad
	Private vgc_Estado AS String 'Estado de la solicitud: pen - pendiente, apr - aprobada, den - denegada
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla ott_solicitud_reingreso
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
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
	''' Llave primaria de la tabla ott_solicitud_reingreso. consecutivo anual, por ubicación
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdSolicitudReingreso() As Integer
		Get
			Return vgn_IdSolicitudReingreso
		End Get
		Set(ByVal value As Integer)
			vgn_IdSolicitudReingreso = value
		End Set
	End Property

	''' <summary>
	''' Id de la ubicación a la que corresponde el trámite de reingreso
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
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
	''' Llave primaria de la tabla ott_detalle_material que se asocia con la secuencia sq_id_detalle_material
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
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
	''' Cantidad de material a reingresar al almacén
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CantidadReingreso() As Double
		Get
			Return vgn_CantidadReingreso
		End Get
		Set(ByVal value As Double)
			vgn_CantidadReingreso = value
		End Set
	End Property

	''' <summary>
	''' Cantidad de material recibida en almacén
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CantidadRecibida() As Double
		Get
			Return vgn_CantidadRecibida
		End Get
		Set(ByVal value As Double)
			vgn_CantidadRecibida = value
		End Set
	End Property

	''' <summary>
	''' Tipo de solicitud de reingreso: cus - material en custodia, dev - devolución de material
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property TipoSolicitudReingreso() As String
		Get
			Return vgc_TipoSolicitudReingreso
		End Get
		Set(ByVal value As String)
			vgc_TipoSolicitudReingreso = value
		End Set
	End Property

	''' <summary>
	''' Costo calculado en el momento de la devolución con base a cantidad y costo promedio por unidad
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CostoCalculado() As Double
		Get
			Return vgn_CostoCalculado
		End Get
		Set(ByVal value As Double)
			vgn_CostoCalculado = value
		End Set
	End Property

	''' <summary>
	''' Estado de la solicitud: pen - pendiente, apr - aprobada, den - denegada
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
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
	''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
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
	''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
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
		vgn_Anno = 0
		vgn_IdSolicitudReingreso = 0
		vgn_IdUbicacionAdministra = 0
		vgn_IdDetalleMaterial = 0
		vgn_CantidadReingreso = 0
		vgn_CantidadRecibida = 0
		vgc_TipoSolicitudReingreso = String.Empty
		vgn_CostoCalculado = 0
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
