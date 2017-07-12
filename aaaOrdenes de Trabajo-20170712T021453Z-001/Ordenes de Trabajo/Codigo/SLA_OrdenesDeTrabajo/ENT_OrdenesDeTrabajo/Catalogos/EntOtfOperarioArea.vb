Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtfOperarioArea
		Inherits EntBase
#Region "Atributos"
	Private vgn_NumEmpleado AS Double '
	Private vgn_IdSectorTaller AS Integer 'Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller
	Private vgn_IdAreaProfesional AS Integer 'Llave primaria de la tabla otm_area_profesional   que se asocia con la secuencia sq_id_area_profesional
	Private vgc_CategoriaLaboral AS String 'Categoria laboral a la que pertence el funcionario asociado al sector o taller: ope : operario pro: profesional
	Private vgc_Estado AS String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' 
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>27/01/2016 12:12:35 p.m.</creationDate>
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
	''' Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>27/01/2016 12:12:35 p.m.</creationDate>
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
	''' Llave primaria de la tabla otm_area_profesional   que se asocia con la secuencia sq_id_area_profesional
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>27/01/2016 12:12:35 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdAreaProfesional() As Integer
		Get
			Return vgn_IdAreaProfesional
		End Get
		Set(ByVal value As Integer)
			vgn_IdAreaProfesional = value
		End Set
	End Property

	''' <summary>
	''' Categoria laboral a la que pertence el funcionario asociado al sector o taller: ope : operario pro: profesional
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>27/01/2016 12:12:35 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CategoriaLaboral() As String
		Get
			Return vgc_CategoriaLaboral
		End Get
		Set(ByVal value As String)
			vgc_CategoriaLaboral = value
		End Set
	End Property

	''' <summary>
	''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>27/01/2016 12:12:35 p.m.</creationDate>
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
	''' <creationDate>27/01/2016 12:12:35 p.m.</creationDate>
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
	''' <creationDate>27/01/2016 12:12:35 p.m.</creationDate>
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
		vgn_NumEmpleado = 0
		vgn_IdSectorTaller = 0
		vgn_IdAreaProfesional = 0
		vgc_CategoriaLaboral = String.Empty
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
