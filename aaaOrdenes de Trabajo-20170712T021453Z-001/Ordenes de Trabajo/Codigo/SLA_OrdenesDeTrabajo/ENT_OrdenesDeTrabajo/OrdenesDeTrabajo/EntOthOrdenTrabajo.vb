Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOthOrdenTrabajo
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgc_IdOrdenTrabajo AS String 'Identificador único alfanumérico de la orden de trabajo
	Private vgn_IdUbicacionMadre AS Integer 'Id ubicación de la orden de trabajo madre
	Private vgc_IdOrdenTrabajoMadre AS String 'Id de la orden de trabajo madre
	Private vgn_Anno AS Integer 'Año de solicitud de la ot
	Private vgn_Consecutivo AS Integer 'Consecutivo de orden de trabajo, se reinicia cada año.
	Private vgc_TipoOrdenTrabajo AS String 'Tipo de orden de trabajo: ordinaria, emergencia, preventivo
	Private vgc_EstadoOrdenTrabajo AS String 'Llave primaria de la tabla otc_estado_orden_trabajo
	Private vgn_NumEmpleado AS Double 'Número de empleado del usuario que registra la orden de trabajo
	Private vgn_IdCategoriaServicio AS Integer 'Llave primaria de la tabla otm_categoria_servicio que se asocia con la secuencia sq_id_categoria_servicio
	Private vgn_IdActividad AS Integer 'Llave primaria de la tabla otm_actividad que se asocia con la secuencia sq_id_actividad
	Private vgn_IdLugarTrabajo AS Integer 'Llave primaria de la tabla otm_lugar_trabajo que se asocia con la secuencia sq_id_lugar_trabajo
	Private vgn_IdSectorTaller AS Integer 'Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller
	Private vgd_FechaHoraSolicita AS DateTime 'Fecha y hora en que el solicitante registra la orden de trabajo.
	Private vgn_CodUnidadSirh AS Integer 'Código de la unidad académica o administrativa que tramita la autorización de la orden de trabajo
	Private vgc_NombrePersonaContacto AS String 'Nombre completo y apellidos de la persona que podrá ser contactada para evacuar dudas de la orden de trabajo solicitada
	Private vgc_Telefono AS String 'Teléfono mediante el cual se podrá contactar al solicitante o a la persona contacto
	Private vgc_SennasExactas AS String 'Señas exactas del lugar donde se requiere la realización de trabajo
	Private vgc_DescripcionTrabajo AS String 'Descripción detallada del trabajo requerido
	Private vgn_NumeroOrden AS Integer 'Número asignado a la orden de trabajo para seguimiento interno a la sección de mantenimiento y construcción. el número se asigna cuando se inicia la tramitación de la orden.
	Private vgn_IncluidaEnRecepcion AS Integer 'Indicador de si la boleta fue incluida desde la recepción en la sección de mantenimiento y construcción.
	Private vgc_Parentesco AS String 'Parentesco de la boleta. - mad: madre, hij: hija - valor por defecto mad.
	Private vgn_IdMotivoRechazo AS Integer 'Llave primaria de la tabla otm_motivo_rechazo que se asocia con la secuencia sq_id_motivo_rechazo
	Private vgn_IdUbicacionOrigen AS Integer 'Id de la ubicación del jefe administrativo que registra la orden de trabajo
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
	Private vgn_ConsecutivoHija AS Integer '
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
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
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
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
	''' Id ubicación de la orden de trabajo madre
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdUbicacionMadre() As Integer
		Get
			Return vgn_IdUbicacionMadre
		End Get
		Set(ByVal value As Integer)
			vgn_IdUbicacionMadre = value
		End Set
	End Property

	''' <summary>
	''' Id de la orden de trabajo madre
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdOrdenTrabajoMadre() As String
		Get
			Return vgc_IdOrdenTrabajoMadre
		End Get
		Set(ByVal value As String)
			vgc_IdOrdenTrabajoMadre = value
		End Set
	End Property

	''' <summary>
	''' Año de solicitud de la ot
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
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
	''' Consecutivo de orden de trabajo, se reinicia cada año.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Consecutivo() As Integer
		Get
			Return vgn_Consecutivo
		End Get
		Set(ByVal value As Integer)
			vgn_Consecutivo = value
		End Set
	End Property

	''' <summary>
	''' Tipo de orden de trabajo: ordinaria, emergencia, preventivo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property TipoOrdenTrabajo() As String
		Get
			Return vgc_TipoOrdenTrabajo
		End Get
		Set(ByVal value As String)
			vgc_TipoOrdenTrabajo = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otc_estado_orden_trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property EstadoOrdenTrabajo() As String
		Get
			Return vgc_EstadoOrdenTrabajo
		End Get
		Set(ByVal value As String)
			vgc_EstadoOrdenTrabajo = value
		End Set
	End Property

	''' <summary>
	''' Número de empleado del usuario que registra la orden de trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
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
	''' Llave primaria de la tabla otm_categoria_servicio que se asocia con la secuencia sq_id_categoria_servicio
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdCategoriaServicio() As Integer
		Get
			Return vgn_IdCategoriaServicio
		End Get
		Set(ByVal value As Integer)
			vgn_IdCategoriaServicio = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_actividad que se asocia con la secuencia sq_id_actividad
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdActividad() As Integer
		Get
			Return vgn_IdActividad
		End Get
		Set(ByVal value As Integer)
			vgn_IdActividad = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_lugar_trabajo que se asocia con la secuencia sq_id_lugar_trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdLugarTrabajo() As Integer
		Get
			Return vgn_IdLugarTrabajo
		End Get
		Set(ByVal value As Integer)
			vgn_IdLugarTrabajo = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
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
	''' Fecha y hora en que el solicitante registra la orden de trabajo.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaHoraSolicita() As DateTime
		Get
			Return vgd_FechaHoraSolicita
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaHoraSolicita = value
		End Set
	End Property

	''' <summary>
	''' Código de la unidad académica o administrativa que tramita la autorización de la orden de trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CodUnidadSirh() As Integer
		Get
			Return vgn_CodUnidadSirh
		End Get
		Set(ByVal value As Integer)
			vgn_CodUnidadSirh = value
		End Set
	End Property

	''' <summary>
	''' Nombre completo y apellidos de la persona que podrá ser contactada para evacuar dudas de la orden de trabajo solicitada
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NombrePersonaContacto() As String
		Get
			Return vgc_NombrePersonaContacto
		End Get
		Set(ByVal value As String)
			vgc_NombrePersonaContacto = value
		End Set
	End Property

	''' <summary>
	''' Teléfono mediante el cual se podrá contactar al solicitante o a la persona contacto
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Telefono() As String
		Get
			Return vgc_Telefono
		End Get
		Set(ByVal value As String)
			vgc_Telefono = value
		End Set
	End Property

	''' <summary>
	''' Señas exactas del lugar donde se requiere la realización de trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property SennasExactas() As String
		Get
			Return vgc_SennasExactas
		End Get
		Set(ByVal value As String)
			vgc_SennasExactas = value
		End Set
	End Property

	''' <summary>
	''' Descripción detallada del trabajo requerido
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property DescripcionTrabajo() As String
		Get
			Return vgc_DescripcionTrabajo
		End Get
		Set(ByVal value As String)
			vgc_DescripcionTrabajo = value
		End Set
	End Property

	''' <summary>
	''' Número asignado a la orden de trabajo para seguimiento interno a la sección de mantenimiento y construcción. el número se asigna cuando se inicia la tramitación de la orden.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NumeroOrden() As Integer
		Get
			Return vgn_NumeroOrden
		End Get
		Set(ByVal value As Integer)
			vgn_NumeroOrden = value
		End Set
	End Property

	''' <summary>
	''' Indicador de si la boleta fue incluida desde la recepción en la sección de mantenimiento y construcción.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IncluidaEnRecepcion() As Integer
		Get
			Return vgn_IncluidaEnRecepcion
		End Get
		Set(ByVal value As Integer)
			vgn_IncluidaEnRecepcion = value
		End Set
	End Property

	''' <summary>
	''' Parentesco de la boleta. - mad: madre, hij: hija - valor por defecto mad.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Parentesco() As String
		Get
			Return vgc_Parentesco
		End Get
		Set(ByVal value As String)
			vgc_Parentesco = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_motivo_rechazo que se asocia con la secuencia sq_id_motivo_rechazo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
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
	''' Id de la ubicación del jefe administrativo que registra la orden de trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdUbicacionOrigen() As Integer
		Get
			Return vgn_IdUbicacionOrigen
		End Get
		Set(ByVal value As Integer)
			vgn_IdUbicacionOrigen = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
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
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
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
	''' 
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/12/2015 03:34:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property ConsecutivoHija() As Integer
		Get
			Return vgn_ConsecutivoHija
		End Get
		Set(ByVal value As Integer)
			vgn_ConsecutivoHija = value
		End Set
	End Property

#End Region

#Region "Constructores"
	Public Sub New()
		MyBase.New()
		vgn_IdUbicacion = 0
		vgc_IdOrdenTrabajo = String.Empty
		vgn_IdUbicacionMadre = 0
		vgc_IdOrdenTrabajoMadre = String.Empty
		vgn_Anno = 0
		vgn_Consecutivo = 0
		vgc_TipoOrdenTrabajo = String.Empty
		vgc_EstadoOrdenTrabajo = String.Empty
		vgn_NumEmpleado = 0
		vgn_IdCategoriaServicio = 0
		vgn_IdActividad = 0
		vgn_IdLugarTrabajo = 0
		vgn_IdSectorTaller = 0
		vgd_FechaHoraSolicita = DateTime.Now
		vgn_CodUnidadSirh = 0
		vgc_NombrePersonaContacto = String.Empty
		vgc_Telefono = String.Empty
		vgc_SennasExactas = String.Empty
		vgc_DescripcionTrabajo = String.Empty
		vgn_NumeroOrden = 0
		vgn_IncluidaEnRecepcion = 0
		vgc_Parentesco = String.Empty
		vgn_IdMotivoRechazo = 0
		vgn_IdUbicacionOrigen = 0
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
		vgn_ConsecutivoHija = 0
	End Sub
#End Region
	End Class
End Namespace
