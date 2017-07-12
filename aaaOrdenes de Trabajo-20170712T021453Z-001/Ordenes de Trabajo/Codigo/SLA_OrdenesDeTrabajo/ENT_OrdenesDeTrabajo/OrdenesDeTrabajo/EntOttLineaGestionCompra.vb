Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttLineaGestionCompra
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_IdViaCompraContrato AS Integer 'Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato
	Private vgn_Anno AS Integer 'Año
	Private vgn_NumeroGestion AS Integer 'Consecutivo de la gestión. es anual por ubicación y vía de compra.
	Private vgn_IdLineaGestionCompra AS Integer 'Llave primaria de la tabla ott_linea_gestion_compra asociada a la secuencia sq_id_linea_gestion_compra
	Private vgn_NumeroLinea AS Integer 'Número de línea para gestiones por unidad especialidad de compra
	Private vgn_IdMaterial AS Integer 'Id del material cuando la compra es por aprovisionamiento
	Private vgn_IdDetalleMaterial AS Integer 'Id de la línea de detalle de material de la ot que da origen a la compra.
	Private vgn_CantidadSolicitada AS Double 'Cantidad solicitada
	Private vgn_CantidadIngresa AS Double 'Cantidad comprada del material que ingresa al almacén
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
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
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
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
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
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
	''' Consecutivo de la gestión. es anual por ubicación y vía de compra.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
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
	''' Llave primaria de la tabla ott_linea_gestion_compra asociada a la secuencia sq_id_linea_gestion_compra
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdLineaGestionCompra() As Integer
		Get
			Return vgn_IdLineaGestionCompra
		End Get
		Set(ByVal value As Integer)
			vgn_IdLineaGestionCompra = value
		End Set
	End Property

	''' <summary>
	''' Número de línea para gestiones por unidad especialidad de compra
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
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
	''' Id del material cuando la compra es por aprovisionamiento
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
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
	''' Id de la línea de detalle de material de la ot que da origen a la compra.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdDetalleMaterial() As Integer
		Get
			Return vgn_IdDetalleMaterial
		End Get
		Set(ByVal value As Integer)
			vgn_IdDetalleMaterial = value
		End Set
	End Property

	''' <summary>
	''' Cantidad solicitada
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
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
	''' Cantidad comprada del material que ingresa al almacén
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CantidadIngresa() As Double
		Get
			Return vgn_CantidadIngresa
		End Get
		Set(ByVal value As Double)
			vgn_CantidadIngresa = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
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
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
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
		vgn_IdLineaGestionCompra = 0
		vgn_NumeroLinea = 0
		vgn_IdMaterial = 0
		vgn_IdDetalleMaterial = 0
		vgn_CantidadSolicitada = 0
		vgn_CantidadIngresa = 0
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
