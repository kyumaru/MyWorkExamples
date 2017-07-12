Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttAjusteInventario
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_Anno AS Integer 'Año
	Private vgn_ConsecutivoAjuste AS Integer 'Consecutivo anual del ajuste.
	Private vgn_IdAlmacenBodega AS Integer 'Llave primaria de la tabla otm_almacen_bodega que se asocia con la secuencia sq_id_almacen_bodega
	Private vgc_EstadoAjuste AS String 'Llave de la tabla otc_estado_ajuste
	Private vgc_TipoAjuste AS String 'Tipo de ajuste. ind - inventario individual, gbl - inventario global, exs - existencia 
	Private vgd_FechaRegistroSolicitud AS DateTime 'Fecha de registro de la solicitud
	Private vgc_Observaciones AS String 'Observaciones indicadas por el revisor
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
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
	''' Año
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
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
	''' Consecutivo anual del ajuste.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property ConsecutivoAjuste() As Integer
		Get
			Return vgn_ConsecutivoAjuste
		End Get
		Set(ByVal value As Integer)
			vgn_ConsecutivoAjuste = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_almacen_bodega que se asocia con la secuencia sq_id_almacen_bodega
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdAlmacenBodega() As Integer
		Get
			Return vgn_IdAlmacenBodega
		End Get
		Set(ByVal value As Integer)
			vgn_IdAlmacenBodega = value
		End Set
	End Property

	''' <summary>
	''' Llave de la tabla otc_estado_ajuste
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property EstadoAjuste() As String
		Get
			Return vgc_EstadoAjuste
		End Get
		Set(ByVal value As String)
			vgc_EstadoAjuste = value
		End Set
	End Property

	''' <summary>
	''' Tipo de ajuste. ind - inventario individual, gbl - inventario global, exs - existencia 
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property TipoAjuste() As String
		Get
			Return vgc_TipoAjuste
		End Get
		Set(ByVal value As String)
			vgc_TipoAjuste = value
		End Set
	End Property

	''' <summary>
	''' Fecha de registro de la solicitud
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaRegistroSolicitud() As DateTime
		Get
			Return vgd_FechaRegistroSolicitud
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaRegistroSolicitud = value
		End Set
	End Property

	''' <summary>
	''' Observaciones indicadas por el revisor
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
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
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
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
	''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
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
		vgn_Anno = 0
		vgn_ConsecutivoAjuste = 0
		vgn_IdAlmacenBodega = 0
		vgc_EstadoAjuste = String.Empty
		vgc_TipoAjuste = String.Empty
		vgd_FechaRegistroSolicitud = DateTime.Now
		vgc_Observaciones = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
