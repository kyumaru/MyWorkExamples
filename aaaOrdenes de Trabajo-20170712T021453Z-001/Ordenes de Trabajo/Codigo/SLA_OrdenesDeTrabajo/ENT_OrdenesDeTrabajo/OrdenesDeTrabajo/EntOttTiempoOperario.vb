Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttTiempoOperario
		Inherits EntBase
#Region "Atributos"
	Private vgn_NumEmpleado AS Double '
	Private vgn_IdSectorTaller AS Integer 'Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgc_IdOrdenTrabajo AS String 'Identificador único alfanumérico de la orden de trabajo
	Private vgn_IdEtapaOrdenTrabajo AS Integer 'Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo
	Private vgn_Tiempo AS Double 'Cantidad de tiempo invertido ya sea en la estimacion o en la ejecucion del trabajo asignado al operario
	Private vgn_IdUnidadTiempo AS Integer 'Llave primaria de la tabla otm_unidad_tiempo que se asocia con la secuencia sq_id_unidad_tiempo
	Private vgc_Clasificacion AS String 'Clasificación para el tiempo, est: estimado ral: real
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' 
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
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
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
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
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
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
	''' Identificador único alfanumérico de la orden de trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdOrdenTrabajo() As String
		Get
			Return vgc_IdOrdenTrabajo
		End Get
		Set(ByVal value As String)
			vgc_IdOrdenTrabajo = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdEtapaOrdenTrabajo() As Integer
		Get
			Return vgn_IdEtapaOrdenTrabajo
		End Get
		Set(ByVal value As Integer)
			vgn_IdEtapaOrdenTrabajo = value
		End Set
	End Property

	''' <summary>
	''' Cantidad de tiempo invertido ya sea en la estimacion o en la ejecucion del trabajo asignado al operario
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Tiempo() As Double
		Get
			Return vgn_Tiempo
		End Get
		Set(ByVal value As Double)
			vgn_Tiempo = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_unidad_tiempo que se asocia con la secuencia sq_id_unidad_tiempo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
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
	''' Clasificación para el tiempo, est: estimado ral: real
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Clasificacion() As String
		Get
			Return vgc_Clasificacion
		End Get
		Set(ByVal value As String)
			vgc_Clasificacion = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
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
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
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
		vgn_IdUbicacion = 0
		vgc_IdOrdenTrabajo = String.Empty
		vgn_IdEtapaOrdenTrabajo = 0
		vgn_Tiempo = 0
		vgn_IdUnidadTiempo = 0
		vgc_Clasificacion = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
