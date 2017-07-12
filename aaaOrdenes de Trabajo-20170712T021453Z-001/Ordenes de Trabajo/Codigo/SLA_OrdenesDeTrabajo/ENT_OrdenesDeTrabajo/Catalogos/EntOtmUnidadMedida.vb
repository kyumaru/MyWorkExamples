Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmUnidadMedida
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUnidadMedida AS Integer 'Llave primaria de la tabla otm_unidad_medida que se asocia con la secuencia sq_id_unidad_medida
	Private vgc_Descripcion AS String 'Descripción de unidad de medida. ej: litro
	Private vgc_Acronimo AS String 'Acrónimo
	Private vgc_Estado AS String 'Estado del registro - apr: aprueba, dev: devuelve, den: deniega
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_unidad_medida que se asocia con la secuencia sq_id_unidad_medida
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>23/05/2016 10:24:54 a.m.</creationDate>
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
	''' Descripción de unidad de medida. ej: litro
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>23/05/2016 10:24:54 a.m.</creationDate>
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
	''' Acrónimo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>23/05/2016 10:24:54 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Acronimo() As String
		Get
			Return vgc_Acronimo
		End Get
		Set(ByVal value As String)
			vgc_Acronimo = value
		End Set
	End Property

	''' <summary>
	''' Estado del registro - apr: aprueba, dev: devuelve, den: deniega
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>23/05/2016 10:24:54 a.m.</creationDate>
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
	''' <creationDate>23/05/2016 10:24:54 a.m.</creationDate>
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
	''' <creationDate>23/05/2016 10:24:54 a.m.</creationDate>
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
		vgn_IdUnidadMedida = 0
		vgc_Descripcion = String.Empty
		vgc_Acronimo = String.Empty
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
