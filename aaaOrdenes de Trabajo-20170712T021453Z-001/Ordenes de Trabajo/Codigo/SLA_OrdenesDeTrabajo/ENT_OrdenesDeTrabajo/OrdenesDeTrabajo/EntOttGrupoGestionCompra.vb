Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttGrupoGestionCompra
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_IdViaCompraContrato AS Integer 'Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato
	Private vgn_Anno AS Integer 'Año
	Private vgn_NumeroGestion AS Integer 'Consecutivo de la gestión
	Private vgn_IdGrupoGestionCompra AS Integer 'Llave de la tabla ott_grupo_gestion_compra asociada a la secuencia sq_id_grupo_gestion_compra
	Private vgn_NumeroLinea AS Integer 'Número de línea que agrupa las solicitudes de un mismo material. define el orden.
	Private vgn_IdMaterial AS Integer 'Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación
	Private vgn_CantidadSolicitada AS Double 'Cantidad solicitada
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
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
	''' Llave de la tabla ott_grupo_gestion_compra asociada a la secuencia sq_id_grupo_gestion_compra
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdGrupoGestionCompra() As Integer
		Get
			Return vgn_IdGrupoGestionCompra
		End Get
		Set(ByVal value As Integer)
			vgn_IdGrupoGestionCompra = value
		End Set
	End Property

	''' <summary>
	''' Número de línea que agrupa las solicitudes de un mismo material. define el orden.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
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
	''' Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdMaterial() As Integer
		Get
			Return vgn_IdMaterial
		End Get
		Set(ByVal value As Integer)
			vgn_IdMaterial = value
		End Set
	End Property

	''' <summary>
	''' Cantidad solicitada
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CantidadSolicitada() As Double
		Get
			Return vgn_CantidadSolicitada
		End Get
		Set(ByVal value As Double)
			vgn_CantidadSolicitada = value
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
		vgn_IdUbicacion = 0
		vgn_IdViaCompraContrato = 0
		vgn_Anno = 0
		vgn_NumeroGestion = 0
		vgn_IdGrupoGestionCompra = 0
		vgn_NumeroLinea = 0
		vgn_IdMaterial = 0
		vgn_CantidadSolicitada = 0
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
