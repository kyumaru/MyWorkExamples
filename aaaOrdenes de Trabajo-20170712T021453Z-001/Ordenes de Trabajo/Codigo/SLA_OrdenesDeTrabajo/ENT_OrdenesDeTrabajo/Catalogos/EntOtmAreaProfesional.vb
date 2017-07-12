Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmAreaProfesional
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdAreaProfesional AS Integer 'Llave primaria de la tabla otm_area_profesional   que se asocia con la secuencia sq_id_area_profesional
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgc_Sufijo AS String 'Siglas a asignar al consecutivo de la orden de trabajo madre para crear el consecutivo de la orden de trabajo hija (exclusivo para ordenes de diseño).
	Private vgc_Descripcion AS String 'Descricpión del área de servicio. 
	Private vgc_Estado AS String 'Usuario que crea o modifica el registro.
	Private vgc_Usuario AS String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_area_profesional   que se asocia con la secuencia sq_id_area_profesional
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/01/2016 11:22:41 a.m.</creationDate>
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
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/01/2016 11:22:41 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdUbicacion() As Integer
		Get
			Return vgn_IdUbicacion
		End Get
		Set(ByVal value As Integer)
			vgn_IdUbicacion = value
		End Set
	End Property

	''' <summary>
	''' Siglas a asignar al consecutivo de la orden de trabajo madre para crear el consecutivo de la orden de trabajo hija (exclusivo para ordenes de diseño).
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/01/2016 11:22:41 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Sufijo() As String
		Get
			Return vgc_Sufijo
		End Get
		Set(ByVal value As String)
			vgc_Sufijo = value
		End Set
	End Property

	''' <summary>
	''' Descricpión del área de servicio. 
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/01/2016 11:22:41 a.m.</creationDate>
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
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/01/2016 11:22:41 a.m.</creationDate>
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
	''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/01/2016 11:22:41 a.m.</creationDate>
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
	''' <creationDate>20/01/2016 11:22:41 a.m.</creationDate>
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
		vgn_IdAreaProfesional = 0
		vgn_IdUbicacion = 0
		vgc_Sufijo = String.Empty
		vgc_Descripcion = String.Empty
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
