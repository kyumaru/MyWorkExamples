Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttTrazabilidadProceso
		Inherits EntBase
#Region "Atributos"
        'Private vgd_FechaHoraEjecucion AS DateTime 'Fecha de ejecución del paso indicado por el responsable (revisión, liquidación, etc.)
	Private vgc_Observaciones AS String 'Observaciones indicadas por el responsable
	Private vgn_IdMotivoRechazo AS Integer 'En caso de rechazo, el motivo indicado en este paso del flujo. puede haber múltiples rechazos en un flujo.
	Private vgc_ObservacionesInternas AS String 'Observaciones del responsable para procesos internos
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
	Private vgn_IdTrazabilidadProceso AS Integer 'Llave primaria de la tabla ott_trazabilidad_proceso que se asocia con la secuencia sq_id_trazabilidad_proceso
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgc_IdOrdenTrabajo AS String 'Identificador único alfanumérico de la orden de trabajo
	Private vgn_NumEmpleadoEjecuta AS Double 'Número de empleado responsable de la ejecución
	Private vgc_EstadoOrdenTrabajo AS String 'Estado asignado a la orden de trabajo en el paso realizado
#End Region
#Region "Propiedades"
        ' ''' <summary>
        ' ''' Fecha de ejecución del paso indicado por el responsable (revisión, liquidación, etc.)
        ' ''' </summary>
        ' ''' <author>Generador de Código basado en objetos Oracle</author>
        ' ''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
        ' ''' <changeLog></changeLog>
        'Public Property FechaHoraEjecucion() As DateTime
        '	Get
        '		Return vgd_FechaHoraEjecucion
        '	End Get
        '	Set(ByVal value As DateTime)
        '		vgd_FechaHoraEjecucion = value
        '	End Set
        'End Property

	''' <summary>
	''' Observaciones indicadas por el responsable
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
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
	''' En caso de rechazo, el motivo indicado en este paso del flujo. puede haber múltiples rechazos en un flujo.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdMotivoRechazo() As Integer
		Get
			Return vgn_IdMotivoRechazo
		End Get
		Set(ByVal value As Integer)
			vgn_IdMotivoRechazo = value
		End Set
	End Property

	''' <summary>
	''' Observaciones del responsable para procesos internos
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property ObservacionesInternas() As String
		Get
			Return vgc_ObservacionesInternas
		End Get
		Set(ByVal value As String)
			vgc_ObservacionesInternas = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
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
	''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
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
	''' Llave primaria de la tabla ott_trazabilidad_proceso que se asocia con la secuencia sq_id_trazabilidad_proceso
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdTrazabilidadProceso() As Integer
		Get
			Return vgn_IdTrazabilidadProceso
		End Get
		Set(ByVal value As Integer)
			vgn_IdTrazabilidadProceso = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
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
	''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
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
	''' Número de empleado responsable de la ejecución
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NumEmpleadoEjecuta() As Double
		Get
			Return vgn_NumEmpleadoEjecuta
		End Get
		Set(ByVal value As Double)
			vgn_NumEmpleadoEjecuta = value
		End Set
	End Property

	''' <summary>
	''' Estado asignado a la orden de trabajo en el paso realizado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property EstadoOrdenTrabajo() As String
		Get
			Return vgc_EstadoOrdenTrabajo
		End Get
		Set(ByVal value As String)
			vgc_EstadoOrdenTrabajo = value
		End Set
	End Property

#End Region

#Region "Constructores"
	Public Sub New()
		MyBase.New()
            'vgd_FechaHoraEjecucion = DateTime.Now
		vgc_Observaciones = String.Empty
		vgn_IdMotivoRechazo = 0
		vgc_ObservacionesInternas = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
		vgn_IdTrazabilidadProceso = 0
		vgn_IdUbicacion = 0
		vgc_IdOrdenTrabajo = String.Empty
		vgn_NumEmpleadoEjecuta = 0
		vgc_EstadoOrdenTrabajo = String.Empty
	End Sub
#End Region
	End Class
End Namespace
