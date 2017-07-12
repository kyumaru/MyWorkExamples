Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace ORDENES_TRABAJO.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtlSolicitudTraslado
		Inherits EntBase
#Region "Atributos"
	Private vgn_Anno AS Integer 'Año de la solicitud
	Private vgn_IdSolicitudTraslado AS Integer 'Consecutivo de la solicitud. el consecutivo es anual.
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgd_FechaPropuestaSalida AS DateTime 'Fecha propuesta de salida del material
	Private vgc_JornadaRetiro AS String 'Jornada del día en que retirará el material: man- mañana, tar- tarde. valor por defecto man.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
#End Region
#Region "Propiedades"
	''' <summary>
	''' Año de la solicitud
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:09:43 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Anno() As Integer
		Get
			Return vgn_Anno
		End Get
		Set(ByVal value As Integer)
			vgn_Anno = value
		End Set
	End Property

	''' <summary>
	''' Consecutivo de la solicitud. el consecutivo es anual.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:09:43 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdSolicitudTraslado() As Integer
		Get
			Return vgn_IdSolicitudTraslado
		End Get
		Set(ByVal value As Integer)
			vgn_IdSolicitudTraslado = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:09:43 a.m.</creationDate>
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
	''' Fecha propuesta de salida del material
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:09:43 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaPropuestaSalida() As DateTime
		Get
			Return vgd_FechaPropuestaSalida
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaPropuestaSalida = value
		End Set
	End Property

	''' <summary>
	''' Jornada del día en que retirará el material: man- mañana, tar- tarde. valor por defecto man.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:09:43 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property JornadaRetiro() As String
		Get
			Return vgc_JornadaRetiro
		End Get
		Set(ByVal value As String)
			vgc_JornadaRetiro = value
		End Set
	End Property

	''' <summary>
	''' Control de concurrencia - valor por defecto: fecha y hora del sistema
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:09:43 a.m.</creationDate>
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
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:09:43 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Usuario() As String
		Get
			Return vgc_Usuario
		End Get
		Set(ByVal value As String)
			vgc_Usuario = value
		End Set
	End Property

#End Region

#Region "Constructores"
	Public Sub New()
		MyBase.New()
		vgn_Anno = 0
		vgn_IdSolicitudTraslado = 0
		vgn_IdUbicacion = 0
		vgd_FechaPropuestaSalida = DateTime.Now
		vgc_JornadaRetiro = String.Empty
		vgd_TimeStamp = DateTime.Now
		vgc_Usuario = String.Empty
	End Sub
#End Region
	End Class
End Namespace
