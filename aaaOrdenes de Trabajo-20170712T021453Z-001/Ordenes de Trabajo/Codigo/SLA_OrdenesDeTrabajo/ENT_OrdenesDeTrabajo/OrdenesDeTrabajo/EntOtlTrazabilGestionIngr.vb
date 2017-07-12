Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOtlTrazabilGestionIngr
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdTrazabilGestionIngr AS Integer 'Llave primaria de la tabla  otl_trazabil_gestion_ingr asociada a la secuencia  sq_id_trazabil_gestion_ingr
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_IdViaCompraContrato AS Integer 'Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato
	Private vgn_NumeroGestion AS Integer 'Consecutivo de la gestión
	Private vgn_Anno AS Integer 'Año
	Private vgn_Consecutivo AS Integer 'Consecutivo de la gestion
	Private vgc_EstadoGestionIngreso AS String 'Llave de la tabla otc_estado_gestion_ingres
	Private vgc_Observaciones AS String 'Observaciones
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla  otl_trazabil_gestion_ingr asociada a la secuencia  sq_id_trazabil_gestion_ingr
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/02/2017 04:13:16 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdTrazabilGestionIngr() As Integer
		Get
			Return vgn_IdTrazabilGestionIngr
		End Get
		Set(ByVal value As Integer)
			vgn_IdTrazabilGestionIngr = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/02/2017 04:13:16 p.m.</creationDate>
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
	''' <creationDate>20/02/2017 04:13:16 p.m.</creationDate>
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
	''' Consecutivo de la gestión
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/02/2017 04:13:16 p.m.</creationDate>
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
	''' Año
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/02/2017 04:13:16 p.m.</creationDate>
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
	''' Consecutivo de la gestion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/02/2017 04:13:16 p.m.</creationDate>
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
	''' Llave de la tabla otc_estado_gestion_ingres
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/02/2017 04:13:16 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property EstadoGestionIngreso() As String
		Get
			Return vgc_EstadoGestionIngreso
		End Get
		Set(ByVal value As String)
			vgc_EstadoGestionIngreso = value
		End Set
	End Property

	''' <summary>
	''' Observaciones
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>20/02/2017 04:13:16 p.m.</creationDate>
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
	''' <creationDate>20/02/2017 04:13:16 p.m.</creationDate>
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
	''' <creationDate>20/02/2017 04:13:16 p.m.</creationDate>
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
		vgn_IdTrazabilGestionIngr = 0
		vgn_IdUbicacion = 0
		vgn_IdViaCompraContrato = 0
		vgn_NumeroGestion = 0
		vgn_Anno = 0
		vgn_Consecutivo = 0
		vgc_EstadoGestionIngreso = String.Empty
		vgc_Observaciones = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
