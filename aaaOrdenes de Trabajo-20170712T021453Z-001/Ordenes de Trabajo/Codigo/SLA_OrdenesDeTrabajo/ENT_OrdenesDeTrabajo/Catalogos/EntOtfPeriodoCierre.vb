Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtfPeriodoCierre
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdPeriodoCierre AS Integer 'Llave primaria de la tabla otf_periodo_cierre que se asocia con la secuencia sq_id_periodo_cierre
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgc_UnidadCierre AS String 'Unidad de la sección de mantenimiento y construcción que realiza el cierre - mnt: mantenimiento, dis: diseño - valor defecto: mnt
	Private vgd_FechaInicioCierre AS DateTime 'Fecha de inicio del cierre
	Private vgd_FechaFinCierre AS DateTime 'Fecha de fin de cierre
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otf_periodo_cierre que se asocia con la secuencia sq_id_periodo_cierre
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/01/2016 10:53:17 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdPeriodoCierre() As Integer
		Get
			Return vgn_IdPeriodoCierre
		End Get
		Set(ByVal value As Integer)
			vgn_IdPeriodoCierre = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/01/2016 10:53:17 a.m.</creationDate>
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
	''' Unidad de la sección de mantenimiento y construcción que realiza el cierre - mnt: mantenimiento, dis: diseño - valor defecto: mnt
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/01/2016 10:53:17 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property UnidadCierre() As String
		Get
			Return vgc_UnidadCierre
		End Get
		Set(ByVal value As String)
			vgc_UnidadCierre = value
		End Set
	End Property

	''' <summary>
	''' Fecha de inicio del cierre
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/01/2016 10:53:17 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaInicioCierre() As DateTime
		Get
			Return vgd_FechaInicioCierre
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaInicioCierre = value
		End Set
	End Property

	''' <summary>
	''' Fecha de fin de cierre
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/01/2016 10:53:17 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaFinCierre() As DateTime
		Get
			Return vgd_FechaFinCierre
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaFinCierre = value
		End Set
	End Property

	''' <summary>
	''' Control de concurrencia - valor por defecto: fecha y hora del sistema
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/01/2016 10:53:17 a.m.</creationDate>
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
	''' <creationDate>18/01/2016 10:53:17 a.m.</creationDate>
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
		vgn_IdPeriodoCierre = 0
		vgn_IdUbicacion = 0
		vgc_UnidadCierre = String.Empty
		vgd_FechaInicioCierre = DateTime.Now
		vgd_FechaFinCierre = DateTime.Now
		vgd_TimeStamp = DateTime.Now
		vgc_Usuario = String.Empty
	End Sub
#End Region
	End Class
End Namespace
