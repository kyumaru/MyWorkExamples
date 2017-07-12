Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmTipoLugarUbicacion
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdTipoLugarUbicacion AS Integer 'Llave primaria de la tabla otm_tipo_lugar_ubicacion que se asocia con la secuencia sq_id_tipo_lugar_ubicacion
	Private vgc_Descripcion AS String 'Descripción del lugar de ubicación
	Private vgc_Estado AS String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_tipo_lugar_ubicacion que se asocia con la secuencia sq_id_tipo_lugar_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdTipoLugarUbicacion() As Integer
		Get
			Return vgn_IdTipoLugarUbicacion
		End Get
		Set(ByVal value As Integer)
			vgn_IdTipoLugarUbicacion = value
		End Set
	End Property

	''' <summary>
	''' Descripción del lugar de ubicación
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
		vgn_IdTipoLugarUbicacion = 0
		vgc_Descripcion = String.Empty
		vgc_Estado = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
