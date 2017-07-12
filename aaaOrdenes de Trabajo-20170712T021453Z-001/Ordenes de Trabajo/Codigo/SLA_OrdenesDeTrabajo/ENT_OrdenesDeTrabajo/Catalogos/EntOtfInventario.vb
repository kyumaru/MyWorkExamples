Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtfInventario
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdAlmacenBodega AS Integer 'Llave primaria de la tabla otm_almacen_bodega que se asocia con la secuencia sq_id_almacen_bodega
	Private vgn_IdUbicacionAdministra AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_IdMaterial AS Integer 'Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación
	Private vgn_CantidadDisponible AS Double 'Cantidad disponible del material en el inventario
	Private vgn_CantidadReservada AS Double 'Cantidad reservada
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_almacen_bodega que se asocia con la secuencia sq_id_almacen_bodega
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 02:56:19 p.m.</creationDate>
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
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 02:56:19 p.m.</creationDate>
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
	''' <creationDate>29/05/2016 02:56:19 p.m.</creationDate>
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
	''' Cantidad disponible del material en el inventario
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 02:56:19 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CantidadDisponible() As Double
		Get
			Return vgn_CantidadDisponible
		End Get
		Set(ByVal value As Double)
			vgn_CantidadDisponible = value
		End Set
	End Property

	''' <summary>
	''' Cantidad reservada
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 02:56:19 p.m.</creationDate>
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
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 02:56:19 p.m.</creationDate>
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
	''' <creationDate>29/05/2016 02:56:19 p.m.</creationDate>
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
		vgn_IdAlmacenBodega = 0
		vgn_IdUbicacionAdministra = 0
		vgn_IdMaterial = 0
		vgn_CantidadDisponible = 0
		vgn_CantidadReservada = 0
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
