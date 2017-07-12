Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmUnidadTiempo
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUnidadTiempo AS Integer 'Llave primaria de la tabla otm_unidad_tiempo que se asocia con la secuencia sq_id_unidad_tiempo
	Private vgc_Descripcion AS String 'Descripción de la unidad de tiempo, debe ser unica, por ejemplo; semana, quincena, mes 
	Private vgn_Unidad AS Integer 'Unidad de tiempo que conforma la descripcion indicada
	Private vgn_Valor AS Integer 'Cantidad de unidades de tiempo que conforman la descripcion indicada
	Private vgc_Estado AS String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_unidad_tiempo que se asocia con la secuencia sq_id_unidad_tiempo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>15/01/2016 02:29:16 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdUnidadTiempo() As Integer
		Get
			Return vgn_IdUnidadTiempo
		End Get
		Set(ByVal value As Integer)
			vgn_IdUnidadTiempo = value
		End Set
	End Property

	''' <summary>
	''' Descripción de la unidad de tiempo, debe ser unica, por ejemplo; semana, quincena, mes 
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>15/01/2016 02:29:16 p.m.</creationDate>
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
	''' Unidad de tiempo que conforma la descripcion indicada
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>15/01/2016 02:29:16 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Unidad() As Integer
		Get
			Return vgn_Unidad
		End Get
		Set(ByVal value As Integer)
			vgn_Unidad = value
		End Set
	End Property

	''' <summary>
	''' Cantidad de unidades de tiempo que conforman la descripcion indicada
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>15/01/2016 02:29:16 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Valor() As Integer
		Get
			Return vgn_Valor
		End Get
		Set(ByVal value As Integer)
			vgn_Valor = value
		End Set
	End Property

	''' <summary>
	''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>15/01/2016 02:29:16 p.m.</creationDate>
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
	''' <creationDate>15/01/2016 02:29:16 p.m.</creationDate>
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
	''' <creationDate>15/01/2016 02:29:16 p.m.</creationDate>
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
		vgn_IdUnidadTiempo = 0
		vgc_Descripcion = String.Empty
		vgn_Unidad = 0
		vgn_Valor = 0
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
