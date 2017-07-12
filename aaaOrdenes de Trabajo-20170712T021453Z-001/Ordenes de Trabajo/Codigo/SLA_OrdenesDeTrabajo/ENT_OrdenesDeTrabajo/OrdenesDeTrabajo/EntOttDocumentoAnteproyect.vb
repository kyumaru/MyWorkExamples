Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttDocumentoAnteproyect
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdTipoDocumento AS Integer 'Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento
	Private vgn_IdEtapaOrdenTrabajo AS Integer 'Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo
	Private vgn_IdAdjuntoOrdenTrabajo AS Integer 'Llave primaria de la tabla otf_adjunto_orden_trabajo que se asocia con la secuencia sq_id_adjunto_orden_trabajo
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgc_IdOrdenTrabajo AS String 'Identificador único alfanumérico de la orden de trabajo
	Private vgn_Version AS Integer 'Numero de version del anteproyecto asociado a una orden de trabajo
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
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
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
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
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
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
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
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
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
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
	''' Numero de version del anteproyecto asociado a una orden de trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
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
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
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
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
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
		vgn_IdTipoDocumento = 0
		vgn_IdEtapaOrdenTrabajo = 0
		vgn_IdAdjuntoOrdenTrabajo = 0
		vgn_IdUbicacion = 0
		vgc_IdOrdenTrabajo = String.Empty
		vgn_Version = 0
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
