Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmMaterial
		Inherits EntBase
#Region "Atributos"
	Private vgc_Descripcion AS String 'Descripción del material
	Private vgn_IdCategoriaMaterial AS Integer 'Llave primaria de la tabla otm_categoria_material que se asocia con la secuencia sq_id_categoria_material
	Private vgn_IdSubcategoriaMaterial AS Integer 'Llave primaria de la tabla otm_subcategoria_material que se asocia con la secuencia sq_id_subcategoria_material
	Private vgn_IdUnidadMedida AS Integer 'Llave primaria de la tabla otm_unidad_medida que se asocia con la secuencia sq_id_unidad_medida
	Private vgc_PartidaPresupuestaria AS String 'Partida presupuestaria oplau
	Private vgc_Clasificacion AS String 'Clasificación del material: a- alta rotación, b- baja rotación, c- segunda
	Private vgn_PuntoReorden AS Integer 'Punto de reorden
	Private vgn_MaximoAlmacen AS Integer 'Máximo del material en almacén
	Private vgn_MaximoBodega AS Integer 'Máximo permitido del material en una bodega para evitar sobreabastecimiento
	Private vgn_CostoPromedio AS Double 'Costo promedio del material
	Private vgc_UbicacionFisica AS String 'Ubicación física del material conformada por mueble-columna-estante
	Private vgc_Estado AS String 'Estado del registro - act: activo, ina: inactivo
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
	Private vgn_IdUbicacionAdministra AS Integer 'Id de la ubicación que administra los datos del catálogo
	Private vgn_IdMaterial AS Integer 'Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación
#End Region
#Region "Propiedades"
	''' <summary>
	''' Descripción del material
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
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
	''' Llave primaria de la tabla otm_categoria_material que se asocia con la secuencia sq_id_categoria_material
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdCategoriaMaterial() As Integer
		Get
			Return vgn_IdCategoriaMaterial
		End Get
		Set(ByVal value As Integer)
			vgn_IdCategoriaMaterial = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_subcategoria_material que se asocia con la secuencia sq_id_subcategoria_material
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdSubcategoriaMaterial() As Integer
		Get
			Return vgn_IdSubcategoriaMaterial
		End Get
		Set(ByVal value As Integer)
			vgn_IdSubcategoriaMaterial = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_unidad_medida que se asocia con la secuencia sq_id_unidad_medida
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdUnidadMedida() As Integer
		Get
			Return vgn_IdUnidadMedida
		End Get
		Set(ByVal value As Integer)
			vgn_IdUnidadMedida = value
		End Set
	End Property

	''' <summary>
	''' Partida presupuestaria oplau
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property PartidaPresupuestaria() As String
		Get
			Return vgc_PartidaPresupuestaria
		End Get
		Set(ByVal value As String)
			vgc_PartidaPresupuestaria = value
		End Set
	End Property

	''' <summary>
	''' Clasificación del material: a- alta rotación, b- baja rotación, c- segunda
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Clasificacion() As String
		Get
			Return vgc_Clasificacion
		End Get
		Set(ByVal value As String)
			vgc_Clasificacion = value
		End Set
	End Property

	''' <summary>
	''' Punto de reorden
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property PuntoReorden() As Integer
		Get
			Return vgn_PuntoReorden
		End Get
		Set(ByVal value As Integer)
			vgn_PuntoReorden = value
		End Set
	End Property

	''' <summary>
	''' Máximo del material en almacén
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property MaximoAlmacen() As Integer
		Get
			Return vgn_MaximoAlmacen
		End Get
		Set(ByVal value As Integer)
			vgn_MaximoAlmacen = value
		End Set
	End Property

	''' <summary>
	''' Máximo permitido del material en una bodega para evitar sobreabastecimiento
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property MaximoBodega() As Integer
		Get
			Return vgn_MaximoBodega
		End Get
		Set(ByVal value As Integer)
			vgn_MaximoBodega = value
		End Set
	End Property

	''' <summary>
	''' Costo promedio del material
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CostoPromedio() As Double
		Get
			Return vgn_CostoPromedio
		End Get
		Set(ByVal value As Double)
			vgn_CostoPromedio = value
		End Set
	End Property

	''' <summary>
	''' Ubicación física del material conformada por mueble-columna-estante
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property UbicacionFisica() As String
		Get
			Return vgc_UbicacionFisica
		End Get
		Set(ByVal value As String)
			vgc_UbicacionFisica = value
		End Set
	End Property

	''' <summary>
	''' Estado del registro - act: activo, ina: inactivo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
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
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
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
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
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
	''' Id de la ubicación que administra los datos del catálogo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
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
	''' <creationDate>29/05/2016 11:14:50 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdMaterial() As Integer
		Get
			Return vgn_IdMaterial
		End Get
		Set(ByVal value As Integer)
			vgn_IdMaterial = value
		End Set
	End Property

#End Region

#Region "Constructores"
	Public Sub New()
		MyBase.New()
		vgc_Descripcion = String.Empty
		vgn_IdCategoriaMaterial = 0
		vgn_IdSubcategoriaMaterial = 0
		vgn_IdUnidadMedida = 0
		vgc_PartidaPresupuestaria = String.Empty
		vgc_Clasificacion = String.Empty
		vgn_PuntoReorden = 0
		vgn_MaximoAlmacen = 0
		vgn_MaximoBodega = 0
		vgn_CostoPromedio = 0
		vgc_UbicacionFisica = String.Empty
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
		vgn_IdUbicacionAdministra = 0
		vgn_IdMaterial = 0
	End Sub
#End Region
	End Class
End Namespace
