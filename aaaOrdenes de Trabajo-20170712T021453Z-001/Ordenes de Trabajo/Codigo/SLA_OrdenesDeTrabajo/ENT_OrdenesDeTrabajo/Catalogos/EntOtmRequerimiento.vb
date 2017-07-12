Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmRequerimiento
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdRequerimiento AS Integer 'Llave primaria de la tabla otm_requerimiento que se asocia con la secuencia sq_id_requerimiento
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_IdRequerimientoPadre AS Integer 'Referencia al requeremiento de nivel anterior
	Private vgc_Descripcion AS String 'Descripción del requerimiento. puede repetirse en diferentes niveles. ejemplo: cuantos poseen 
	Private vgn_Orden AS Integer 'Orden de visualización
	Private vgn_Nivel AS Integer 'Nivel en que se ubica el requerimiento con un valor de 1 a 3 dado que conforman un encabezado.
	Private vgc_TipoValor AS String 'Tipo de valor a registrar para el requerimiento. - num: numerico, car - caracter - valor por defecto num
	Private vgc_Estado AS String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_requerimiento que se asocia con la secuencia sq_id_requerimiento
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdRequerimiento() As Integer
		Get
			Return vgn_IdRequerimiento
		End Get
		Set(ByVal value As Integer)
			vgn_IdRequerimiento = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
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
	''' Referencia al requeremiento de nivel anterior
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdRequerimientoPadre() As Integer
		Get
			Return vgn_IdRequerimientoPadre
		End Get
		Set(ByVal value As Integer)
			vgn_IdRequerimientoPadre = value
		End Set
	End Property

	''' <summary>
	''' Descripción del requerimiento. puede repetirse en diferentes niveles. ejemplo: cuantos poseen 
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
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
	''' Orden de visualización
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Orden() As Integer
		Get
			Return vgn_Orden
		End Get
		Set(ByVal value As Integer)
			vgn_Orden = value
		End Set
	End Property

	''' <summary>
	''' Nivel en que se ubica el requerimiento con un valor de 1 a 3 dado que conforman un encabezado.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Nivel() As Integer
		Get
			Return vgn_Nivel
		End Get
		Set(ByVal value As Integer)
			vgn_Nivel = value
		End Set
	End Property

	''' <summary>
	''' Tipo de valor a registrar para el requerimiento. - num: numerico, car - caracter - valor por defecto num
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property TipoValor() As String
		Get
			Return vgc_TipoValor
		End Get
		Set(ByVal value As String)
			vgc_TipoValor = value
		End Set
	End Property

	''' <summary>
	''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
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
	''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
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
	''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
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
		vgn_IdRequerimiento = 0
		vgn_IdUbicacion = 0
		vgn_IdRequerimientoPadre = 0
		vgc_Descripcion = String.Empty
		vgn_Orden = 0
		vgn_Nivel = 0
		vgc_TipoValor = String.Empty
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
