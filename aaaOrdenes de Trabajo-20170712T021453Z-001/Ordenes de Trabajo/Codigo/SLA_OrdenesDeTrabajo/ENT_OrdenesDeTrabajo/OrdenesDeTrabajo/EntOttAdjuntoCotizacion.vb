Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttAdjuntoCotizacion
		Inherits EntBase
#Region "Atributos"
	Private vgc_Identificacion AS String 'Identificación del proveedor (física o jurídica)
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_IdViaCompraContrato AS Integer 'Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato
	Private vgn_Anno AS Integer 'Año
	Private vgn_NumeroGestion AS Integer 'Consecutivo de la gestión
	Private vgn_IdTipoDocumento AS Integer 'Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento
	Private vgo_Archivo AS Byte() 'Documento adjunto correspondiente a una lista del equipo que posea la unidad, su estado actual y el equipo que se proyecte adquirir a corto plazo
	Private vgc_NombreArchivo AS String 'Nombre del archivo adjunto
	Private vgc_Descripcion AS String 'Descripción
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
	''' Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
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
	''' Documento adjunto correspondiente a una lista del equipo que posea la unidad, su estado actual y el equipo que se proyecte adquirir a corto plazo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
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
	''' Nombre del archivo adjunto
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
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
	''' Descripción
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
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
		vgn_IdTipoDocumento = 0
		vgo_Archivo = Nothing
		vgc_NombreArchivo = String.Empty
		vgc_Descripcion = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
