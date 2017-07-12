Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmUbicacion
		Inherits EntBase
#Region "Atributos"
	Private vgc_Descripcion AS String 'Descripción de la ubicación
	Private vgn_PerteneceASede AS Integer 'Indicador de si la ubicación pertenece a una sede - 0.no, 1.si - valor por defecto: 0
	Private vgc_Estado AS String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
#End Region
#Region "Propiedades"
	''' <summary>
	''' Descripción de la ubicación
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
	''' Indicador de si la ubicación pertenece a una sede - 0.no, 1.si - valor por defecto: 0
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property PerteneceASede() As Integer
		Get
			Return vgn_PerteneceASede
		End Get
		Set(ByVal value As Integer)
			vgn_PerteneceASede = value
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

	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdUbicacion() As Integer
		Get
			Return vgn_IdUbicacion
		End Get
		Set(ByVal value As Integer)
			vgn_IdUbicacion = value
		End Set
	End Property

#End Region

#Region "Constructores"
	Public Sub New()
		MyBase.New()
		vgc_Descripcion = String.Empty
		vgn_PerteneceASede = 0
		vgc_Estado = String.Empty
		vgd_TimeStamp = DateTime.Now
		vgn_IdUbicacion = 0
	End Sub
#End Region
	End Class
End Namespace
