Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttProveedorCotizacion
		Inherits EntBase
#Region "Atributos"
	Private vgc_Identificacion AS String 'Identificación del proveedor (física o jurídica)
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_IdViaCompraContrato AS Integer 'Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato
	Private vgn_Anno AS Integer 'Año
	Private vgn_NumeroGestion AS Integer 'Consecutivo de la gestión
	Private vgc_Estado AS String 'Estado: pen - pendiente de envío, env - enviado
	Private vgn_IdNotificacion AS Double 'Id de la notificación en el gestor de notificaciones
	Private vgd_FechaHoraNotificacion AS DateTime 'Fecha hora de la notificación
	Private vgn_Adjudicado AS Integer 'Indicador de si el proveedor fue adjudicado
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Identificación del proveedor (física o jurídica)
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Identificacion() As String
		Get
			Return vgc_Identificacion
		End Get
		Set(ByVal value As String)
			vgc_Identificacion = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
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
	''' Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdViaCompraContrato() As Integer
		Get
			Return vgn_IdViaCompraContrato
		End Get
		Set(ByVal value As Integer)
			vgn_IdViaCompraContrato = value
		End Set
	End Property

	''' <summary>
	''' Año
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
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
	''' Consecutivo de la gestión
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NumeroGestion() As Integer
		Get
			Return vgn_NumeroGestion
		End Get
		Set(ByVal value As Integer)
			vgn_NumeroGestion = value
		End Set
	End Property

	''' <summary>
	''' Estado: pen - pendiente de envío, env - enviado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
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
	''' Id de la notificación en el gestor de notificaciones
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdNotificacion() As Double
		Get
			Return vgn_IdNotificacion
		End Get
		Set(ByVal value As Double)
			vgn_IdNotificacion = value
		End Set
	End Property

	''' <summary>
	''' Fecha hora de la notificación
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaHoraNotificacion() As DateTime
		Get
			Return vgd_FechaHoraNotificacion
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaHoraNotificacion = value
		End Set
	End Property

	''' <summary>
	''' Indicador de si el proveedor fue adjudicado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Adjudicado() As Integer
		Get
			Return vgn_Adjudicado
		End Get
		Set(ByVal value As Integer)
			vgn_Adjudicado = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
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
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
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
		vgc_Identificacion = String.Empty
		vgn_IdUbicacion = 0
		vgn_IdViaCompraContrato = 0
		vgn_Anno = 0
		vgn_NumeroGestion = 0
		vgc_Estado = String.Empty
		vgn_IdNotificacion = 0
		vgd_FechaHoraNotificacion = DateTime.Now
		vgn_Adjudicado = 0
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
