Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace ORDENES_TRABAJO.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtlTrazabilSolTraslado
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdTrazabilSolTraslado AS Integer 'Llave primaria de la tabla, asociada a la secuencia sq_id_trazabil_sol_traslado
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_Anno AS Integer 'Año de la solicitud
	Private vgn_IdSolicitudTraslado AS Integer 'Consecutivo de la solicitud. el consecutivo es anual.
	Private vgc_EstadoTraslado AS String 'Llave de la tabla otc_estado_traslado
	Private vgc_Observaciones AS String 'Observaciones a la solicitud
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla, asociada a la secuencia sq_id_trazabil_sol_traslado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:12:03 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdTrazabilSolTraslado() As Integer
		Get
			Return vgn_IdTrazabilSolTraslado
		End Get
		Set(ByVal value As Integer)
			vgn_IdTrazabilSolTraslado = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:12:03 a.m.</creationDate>
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
	''' Año de la solicitud
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:12:03 a.m.</creationDate>
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
	''' Consecutivo de la solicitud. el consecutivo es anual.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:12:03 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdSolicitudTraslado() As Integer
		Get
			Return vgn_IdSolicitudTraslado
		End Get
		Set(ByVal value As Integer)
			vgn_IdSolicitudTraslado = value
		End Set
	End Property

	''' <summary>
	''' Llave de la tabla otc_estado_traslado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:12:03 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property EstadoTraslado() As String
		Get
			Return vgc_EstadoTraslado
		End Get
		Set(ByVal value As String)
			vgc_EstadoTraslado = value
		End Set
	End Property

	''' <summary>
	''' Observaciones a la solicitud
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:12:03 a.m.</creationDate>
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
	''' <creationDate>09/08/2016 09:12:03 a.m.</creationDate>
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
	''' <creationDate>09/08/2016 09:12:03 a.m.</creationDate>
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
		vgn_IdTrazabilSolTraslado = 0
		vgn_IdUbicacion = 0
		vgn_Anno = 0
		vgn_IdSolicitudTraslado = 0
		vgc_EstadoTraslado = String.Empty
		vgc_Observaciones = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
