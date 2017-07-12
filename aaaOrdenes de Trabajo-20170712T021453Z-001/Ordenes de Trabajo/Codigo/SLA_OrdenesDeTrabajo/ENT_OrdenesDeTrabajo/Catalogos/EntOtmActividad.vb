Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmActividad
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdCategoriaServicio AS Integer 'Llave primaria de la tabla otm_categoria_servicio que se asocia con la secuencia sq_id_categoria_servicio
	Private vgn_IdActividad AS Integer 'Llave primaria de la tabla otm_actividad que se asocia con la secuencia sq_id_actividad
	Private vgn_IdSectorTaller AS Integer 'Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller
	Private vgc_Descripcion AS String 'Descripción de la actividad
	Private vgc_DescripcionAmpliada AS String 'Descripción detallada de la actividad
	Private vgc_Estado AS String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_categoria_servicio que se asocia con la secuencia sq_id_categoria_servicio
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdCategoriaServicio() As Integer
		Get
			Return vgn_IdCategoriaServicio
		End Get
		Set(ByVal value As Integer)
			vgn_IdCategoriaServicio = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_actividad que se asocia con la secuencia sq_id_actividad
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdActividad() As Integer
		Get
			Return vgn_IdActividad
		End Get
		Set(ByVal value As Integer)
			vgn_IdActividad = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
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
	''' Descripción de la actividad
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
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
	''' Descripción detallada de la actividad
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property DescripcionAmpliada() As String
		Get
			Return vgc_DescripcionAmpliada
		End Get
		Set(ByVal value As String)
			vgc_DescripcionAmpliada = value
		End Set
	End Property

	''' <summary>
	''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
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
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
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
		vgn_IdCategoriaServicio = 0
		vgn_IdActividad = 0
		vgn_IdSectorTaller = 0
		vgc_Descripcion = String.Empty
		vgc_DescripcionAmpliada = String.Empty
		vgc_Estado = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
