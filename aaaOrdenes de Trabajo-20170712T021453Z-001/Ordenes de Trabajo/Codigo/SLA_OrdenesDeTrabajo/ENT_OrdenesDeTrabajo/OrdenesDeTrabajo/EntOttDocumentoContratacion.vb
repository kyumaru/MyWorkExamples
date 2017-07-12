Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttDocumentoContratacion
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgc_IdOrdenTrabajo AS String 'Identificador único alfanumérico de la orden de trabajo
	Private vgn_Version AS Integer 'Numero de version del proceso de contratación asociado a una orden de trabajo
	Private vgn_IdTipoDocumento AS Integer 'Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento
	Private vgn_IdEtapaOrdenTrabajo AS Integer 'Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo
	Private vgn_IdAdjuntoOrdenTrabajo AS Integer 'Llave primaria de la tabla otf_adjunto_orden_trabajo que se asocia con la secuencia sq_id_adjunto_orden_trabajo
	Private vgn_NumeroLinea AS Integer 'Número de línea adjudicada
	Private vgn_IdEtapaContratacion AS Integer 'Llave primaria de la tabla otm_etapa_contratacion que se asocia con la secuencia sq_id_etapa_contratacion
	Private vgn_DocumentoTramitado AS Integer 'Indicador de sí el documento adjunto ya fue tramitado por el encargado
	Private vgc_Origen AS String 'Indica si el archivo lo registró el encargado o el profesional
	Private vgd_FechaHoraRegistro AS DateTime 'Fecha y hora de registro del documento
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/04/2016 03:32:02 p.m.</creationDate>
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
	''' <creationDate>20/04/2016 03:32:02 p.m.</creationDate>
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
	''' Numero de version del proceso de contratación asociado a una orden de trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/04/2016 03:32:02 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Version() As Integer
		Get
			Return vgn_Version
		End Get
		Set(ByVal value As Integer)
			vgn_Version = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/04/2016 03:32:02 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdTipoDocumento() As Integer
		Get
			Return vgn_IdTipoDocumento
		End Get
		Set(ByVal value As Integer)
			vgn_IdTipoDocumento = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/04/2016 03:32:02 p.m.</creationDate>
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
	''' Llave primaria de la tabla otf_adjunto_orden_trabajo que se asocia con la secuencia sq_id_adjunto_orden_trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/04/2016 03:32:02 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdAdjuntoOrdenTrabajo() As Integer
		Get
			Return vgn_IdAdjuntoOrdenTrabajo
		End Get
		Set(ByVal value As Integer)
			vgn_IdAdjuntoOrdenTrabajo = value
		End Set
	End Property

	''' <summary>
	''' Número de línea adjudicada
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/04/2016 03:32:02 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NumeroLinea() As Integer
		Get
			Return vgn_NumeroLinea
		End Get
		Set(ByVal value As Integer)
			vgn_NumeroLinea = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_etapa_contratacion que se asocia con la secuencia sq_id_etapa_contratacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/04/2016 03:32:02 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdEtapaContratacion() As Integer
		Get
			Return vgn_IdEtapaContratacion
		End Get
		Set(ByVal value As Integer)
			vgn_IdEtapaContratacion = value
		End Set
	End Property

	''' <summary>
	''' Indicador de sí el documento adjunto ya fue tramitado por el encargado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/04/2016 03:32:02 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property DocumentoTramitado() As Integer
		Get
			Return vgn_DocumentoTramitado
		End Get
		Set(ByVal value As Integer)
			vgn_DocumentoTramitado = value
		End Set
	End Property

	''' <summary>
	''' Indica si el archivo lo registró el encargado o el profesional
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/04/2016 03:32:02 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Origen() As String
		Get
			Return vgc_Origen
		End Get
		Set(ByVal value As String)
			vgc_Origen = value
		End Set
	End Property

	''' <summary>
	''' Fecha y hora de registro del documento
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/04/2016 03:32:02 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaHoraRegistro() As DateTime
		Get
			Return vgd_FechaHoraRegistro
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaHoraRegistro = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/04/2016 03:32:02 p.m.</creationDate>
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
	''' <creationDate>20/04/2016 03:32:02 p.m.</creationDate>
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
		vgn_IdUbicacion = 0
		vgc_IdOrdenTrabajo = String.Empty
		vgn_Version = 0
		vgn_IdTipoDocumento = 0
		vgn_IdEtapaOrdenTrabajo = 0
		vgn_IdAdjuntoOrdenTrabajo = 0
		vgn_NumeroLinea = 0
		vgn_IdEtapaContratacion = 0
		vgn_DocumentoTramitado = 0
		vgc_Origen = String.Empty
		vgd_FechaHoraRegistro = DateTime.Now
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
