Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOtlTrazabilidadAjuste
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdTrazabilidadAjuste AS Integer 'Llave primaria de la tabla, asociada a la secuencia sq_id_trazabilidad_ajuste
	Private vgc_EstadoAjuste AS String 'Llave de la tabla otc_estado_ajuste
	Private vgc_Observaciones AS String 'Observaciones
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla, asociada a la secuencia sq_id_trazabilidad_ajuste
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdTrazabilidadAjuste() As Integer
		Get
			Return vgn_IdTrazabilidadAjuste
		End Get
		Set(ByVal value As Integer)
			vgn_IdTrazabilidadAjuste = value
		End Set
	End Property

	''' <summary>
	''' Llave de la tabla otc_estado_ajuste
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property EstadoAjuste() As String
		Get
			Return vgc_EstadoAjuste
		End Get
		Set(ByVal value As String)
			vgc_EstadoAjuste = value
		End Set
	End Property

	''' <summary>
	''' Observaciones
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Observaciones() As String
		Get
			Return vgc_Observaciones
		End Get
		Set(ByVal value As String)
			vgc_Observaciones = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
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
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
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
		vgn_IdTrazabilidadAjuste = 0
		vgc_EstadoAjuste = String.Empty
		vgc_Observaciones = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
