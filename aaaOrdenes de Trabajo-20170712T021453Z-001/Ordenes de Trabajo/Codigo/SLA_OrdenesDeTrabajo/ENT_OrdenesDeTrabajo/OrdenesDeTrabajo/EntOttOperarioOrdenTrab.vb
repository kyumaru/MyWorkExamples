Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttOperarioOrdenTrab
		Inherits EntBase
#Region "Atributos"
	Private vgn_NumEmpleado AS Double '
	Private vgn_IdSectorTaller AS Integer 'Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgc_IdOrdenTrabajo AS String 'Identificador único alfanumérico de la orden de trabajo
	Private vgn_IdEtapaOrdenTrabajo AS Integer 'Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo
	Private vgc_Cargo AS String 'Cargo asociado al funcinario segun el proyecto, ope: operario (mantenimiento), col: colaborador, enc: encargado (diseño)
	Private vgd_FechaPropuesta AS DateTime 'Fecha propuesta para que el funcionario realice la ejecución del tiempo que se le asigno.
	Private vgd_FechaEjecuta AS DateTime 'Fecha en la que el funcionario efectua la ejecucion del tiempo que se le asigno par arealizar una tarea.
	Private vgd_FechaDesde AS DateTime 'Fecha desde en la que esta a cargo del proyecto en el caso de las ordenes  de diseño
	Private vgd_FechaHasta AS DateTime 'Fecha hasta en la que esta a cargo del proyetco en el casoi de las ordenes de diseño
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema.
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
	''' Cargo asociado al funcinario segun el proyecto, ope: operario (mantenimiento), col: colaborador, enc: encargado (diseño)
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Cargo() As String
		Get
			Return vgc_Cargo
		End Get
		Set(ByVal value As String)
			vgc_Cargo = value
		End Set
	End Property

	''' <summary>
	''' Fecha propuesta para que el funcionario realice la ejecución del tiempo que se le asigno.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaPropuesta() As DateTime
		Get
			Return vgd_FechaPropuesta
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaPropuesta = value
		End Set
	End Property

	''' <summary>
	''' Fecha en la que el funcionario efectua la ejecucion del tiempo que se le asigno par arealizar una tarea.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaEjecuta() As DateTime
		Get
			Return vgd_FechaEjecuta
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaEjecuta = value
		End Set
	End Property

	''' <summary>
	''' Fecha desde en la que esta a cargo del proyecto en el caso de las ordenes  de diseño
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaDesde() As DateTime
		Get
			Return vgd_FechaDesde
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaDesde = value
		End Set
	End Property

	''' <summary>
	''' Fecha hasta en la que esta a cargo del proyetco en el casoi de las ordenes de diseño
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaHasta() As DateTime
		Get
			Return vgd_FechaHasta
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaHasta = value
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
	''' Control de concurrencia - valor por defecto: fecha y hora del sistema.
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
		vgc_Cargo = String.Empty
		vgd_FechaPropuesta = DateTime.Now
		vgd_FechaEjecuta = DateTime.Now
		vgd_FechaDesde = DateTime.Now
		vgd_FechaHasta = DateTime.Now
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
