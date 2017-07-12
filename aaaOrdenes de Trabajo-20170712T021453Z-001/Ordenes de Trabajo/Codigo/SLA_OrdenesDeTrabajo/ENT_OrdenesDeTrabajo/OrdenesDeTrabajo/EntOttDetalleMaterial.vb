Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttDetalleMaterial
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdDetalleMaterial AS Integer 'Llave primaria de la tabla ott_detalle_material que se asocia con la secuencia sq_id_detalle_material
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgc_IdOrdenTrabajo AS String 'Identificador único alfanumérico de la orden de trabajo
	Private vgn_IdUbicacionAdministra AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_IdMaterial AS Integer 'Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación
	Private vgn_CantidadSolicitada AS Double 'Cantidad de material solicitado
	Private vgn_CantidadReservada AS Double 'Cantidad de material reservado
	Private vgn_CantidadRetirada AS Double 'Cantidad de material retirado
	Private vgc_Detalle AS String 'Particularidades del material, ejemplo color
	Private vgc_ViaDespacho AS String 'Modo de obtener el material: almacén, bodega, vía de compra
	Private vgn_IdAlmacenBodega AS Integer 'Llave primaria de la tabla otm_almacen_bodega que se asocia con la secuencia sq_id_almacen_bodega
	Private vgn_IdViaCompraContrato AS Integer 'Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato
	Private vgc_Estado AS String 'Estado del registro - pen: pendiente, apr: aprobada, den: denegada
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
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
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
	''' Cantidad de material reservado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CantidadReservada() As Double
		Get
			Return vgn_CantidadReservada
		End Get
		Set(ByVal value As Double)
			vgn_CantidadReservada = value
		End Set
	End Property

	''' <summary>
	''' Cantidad de material retirado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CantidadRetirada() As Double
		Get
			Return vgn_CantidadRetirada
		End Get
		Set(ByVal value As Double)
			vgn_CantidadRetirada = value
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
	''' Modo de obtener el material: almacén, bodega, vía de compra
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property ViaDespacho() As String
		Get
			Return vgc_ViaDespacho
		End Get
		Set(ByVal value As String)
			vgc_ViaDespacho = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_almacen_bodega que se asocia con la secuencia sq_id_almacen_bodega
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
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
	''' Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdViaCompraContrato() As Integer
		Get
			Return vgn_IdViaCompraContrato
		End Get
		Set(ByVal value As Integer)
			vgn_IdViaCompraContrato = value
		End Set
	End Property

	''' <summary>
	''' Estado del registro - pen: pendiente, apr: aprobada, den: denegada
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
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
		vgn_CantidadReservada = 0
		vgn_CantidadRetirada = 0
		vgc_Detalle = String.Empty
		vgc_ViaDespacho = String.Empty
		vgn_IdAlmacenBodega = 0
		vgn_IdViaCompraContrato = 0
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
