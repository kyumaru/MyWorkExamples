Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmAlmacenBodega
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdAlmacenBodega AS Integer 'Llave primaria de la tabla otm_almacen_bodega que se asocia con la secuencia sq_id_almacen_bodega
	Private vgn_IdUbicacionAdministra AS Integer 'Id de la ubicación que administra los datos del catálogo
	Private vgc_Descripcion AS String 'Descripción del almacén
	Private vgc_Tipo AS String 'Tipo de almacén: alm - almacén central, bodega - bod
	Private vgn_IdSectorTaller AS Integer 'En el caso de bodegas, taller o sector que la administra
	Private vgc_Estado AS String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_almacen_bodega que se asocia con la secuencia sq_id_almacen_bodega
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>23/05/2016 02:48:06 p.m.</creationDate>
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
	''' Id de la ubicación que administra los datos del catálogo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>23/05/2016 02:48:06 p.m.</creationDate>
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
	''' Descripción del almacén
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>23/05/2016 02:48:06 p.m.</creationDate>
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
	''' Tipo de almacén: alm - almacén central, bodega - bod
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>23/05/2016 02:48:06 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Tipo() As String
		Get
			Return vgc_Tipo
		End Get
		Set(ByVal value As String)
			vgc_Tipo = value
		End Set
	End Property

	''' <summary>
	''' En el caso de bodegas, taller o sector que la administra
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>23/05/2016 02:48:06 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdSectorTaller() As Integer
		Get
			Return vgn_IdSectorTaller
		End Get
		Set(ByVal value As Integer)
			vgn_IdSectorTaller = value
		End Set
	End Property

	''' <summary>
	''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>23/05/2016 02:48:06 p.m.</creationDate>
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
	''' <creationDate>23/05/2016 02:48:06 p.m.</creationDate>
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
	''' <creationDate>23/05/2016 02:48:06 p.m.</creationDate>
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
		vgc_Descripcion = String.Empty
		vgc_Tipo = String.Empty
		vgn_IdSectorTaller = 0
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
