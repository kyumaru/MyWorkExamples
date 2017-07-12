Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOtfAdjuntoIncidente
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdAdjuntoIncidente AS Integer 'Llave primaria de la tabla  otf_adjunto_incidente relacionada a la secuencia sq_id_adjunto_incidente
	Private vgn_IdIncidenteAlmacen AS Integer 'Llave primaria de la tabla otf_incidente_almacen asociada a la secuencia sq_id_incidente_almacen
	Private vgn_IdTipoDocumento AS Integer 'Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento
	Private vgc_NombreArchivo AS String 'Nombre del archivo adjunto
	Private vgo_Archivo AS Byte() 'Documento adjunto
	Private vgc_Descripcion AS String 'Breve descripción del archivo adjunto
	Private vgc_Origen AS String 'Origen del archivo: reg - registrador, rev - revisor. valor por defecto reg.
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla  otf_adjunto_incidente relacionada a la secuencia sq_id_adjunto_incidente
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdAdjuntoIncidente() As Integer
		Get
			Return vgn_IdAdjuntoIncidente
		End Get
		Set(ByVal value As Integer)
			vgn_IdAdjuntoIncidente = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otf_incidente_almacen asociada a la secuencia sq_id_incidente_almacen
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdIncidenteAlmacen() As Integer
		Get
			Return vgn_IdIncidenteAlmacen
		End Get
		Set(ByVal value As Integer)
			vgn_IdIncidenteAlmacen = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
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
	''' Nombre del archivo adjunto
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NombreArchivo() As String
		Get
			Return vgc_NombreArchivo
		End Get
		Set(ByVal value As String)
			vgc_NombreArchivo = value
		End Set
	End Property

	''' <summary>
	''' Documento adjunto
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Archivo() As Byte()
		Get
			Return vgo_Archivo
		End Get
		Set(ByVal value As Byte())
			vgo_Archivo = value
		End Set
	End Property

	''' <summary>
	''' Breve descripción del archivo adjunto
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
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
	''' Origen del archivo: reg - registrador, rev - revisor. valor por defecto reg.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
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
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
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
	''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
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
		vgn_IdAdjuntoIncidente = 0
		vgn_IdIncidenteAlmacen = 0
		vgn_IdTipoDocumento = 0
		vgc_NombreArchivo = String.Empty
		vgo_Archivo = Nothing
		vgc_Descripcion = String.Empty
		vgc_Origen = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
