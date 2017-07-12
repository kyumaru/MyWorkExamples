Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttDetalleGestionIngr
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdAdjuntoGestionIngr AS Integer 'Llave primaria de la tabla ott_adjunto_gestion_ingr asociada a la secuencia sq_id_adjunto_gestion_ingr
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_IdViaCompraContrato AS Integer 'Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato
	Private vgn_NumeroGestion AS Integer 'Consecutivo de la gestión
	Private vgn_Anno AS Integer 'Año
	Private vgn_IdLineaGestionCompra AS Integer 'Llave primaria de la tabla ott_linea_gestion_compra asociada a la secuencia sq_id_linea_gestion_compra
	Private vgn_CantidadIngresa AS Double 'Cantidad de material que ingresa al almacén
	Private vgn_CostoIndividual AS Double 'Costo individual del material que ingresa
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla ott_adjunto_gestion_ingr asociada a la secuencia sq_id_adjunto_gestion_ingr
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdAdjuntoGestionIngr() As Integer
		Get
			Return vgn_IdAdjuntoGestionIngr
		End Get
		Set(ByVal value As Integer)
			vgn_IdAdjuntoGestionIngr = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
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
	''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
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
	''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
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
	''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
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
	''' Llave primaria de la tabla ott_linea_gestion_compra asociada a la secuencia sq_id_linea_gestion_compra
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
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
	''' Cantidad de material que ingresa al almacén
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
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
	''' Costo individual del material que ingresa
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CostoIndividual() As Double
		Get
			Return vgn_CostoIndividual
		End Get
		Set(ByVal value As Double)
			vgn_CostoIndividual = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
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
	''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
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
		vgn_IdAdjuntoGestionIngr = 0
		vgn_IdUbicacion = 0
		vgn_IdViaCompraContrato = 0
		vgn_NumeroGestion = 0
		vgn_Anno = 0
		vgn_IdLineaGestionCompra = 0
		vgn_CantidadIngresa = 0
		vgn_CostoIndividual = 0
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
