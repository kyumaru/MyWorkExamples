Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttGestionCompra
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_IdViaCompraContrato AS Integer 'Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato
	Private vgn_Anno AS Integer 'Año
	Private vgn_NumeroGestion AS Integer 'Consecutivo de la gestión. es anual, por vía de compra y ubicación.
	Private vgd_FechaRegistroSolicitud AS DateTime 'Fecha de registro de la solicitud
	Private vgc_Observaciones AS String 'Observaciones
	Private vgc_NumeroCheque AS String 'Número de cheque para pago al proveedor adjudicado en gestiones por fondo de trabajo.
	Private vgc_NumeroGestionGeco AS String 'Número de gestión de geco. aplica solo para gestiones de compra por suministros.
	Private vgd_FechaHoraImpresion AS DateTime 'Fecha y hora de impresión del reporte en las gestiones de compra rápida
	Private vgc_Estado AS String 'Estado
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
	''' Consecutivo de la gestión. es anual, por vía de compra y ubicación.
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
	''' Fecha de registro de la solicitud
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
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
	''' Observaciones
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
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
	''' Número de cheque para pago al proveedor adjudicado en gestiones por fondo de trabajo.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NumeroCheque() As String
		Get
			Return vgc_NumeroCheque
		End Get
		Set(ByVal value As String)
			vgc_NumeroCheque = value
		End Set
	End Property

	''' <summary>
	''' Número de gestión de geco. aplica solo para gestiones de compra por suministros.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NumeroGestionGeco() As String
		Get
			Return vgc_NumeroGestionGeco
		End Get
		Set(ByVal value As String)
			vgc_NumeroGestionGeco = value
		End Set
	End Property

	''' <summary>
	''' Fecha y hora de impresión del reporte en las gestiones de compra rápida
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property FechaHoraImpresion() As DateTime
		Get
			Return vgd_FechaHoraImpresion
		End Get
		Set(ByVal value As DateTime)
			vgd_FechaHoraImpresion = value
		End Set
	End Property

	''' <summary>
	''' Estado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Estado() As String
		Get
			Return vgc_Estado
		End Get
		Set(ByVal value As String)
			vgc_Estado = value
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
		vgd_FechaRegistroSolicitud = DateTime.Now
		vgc_Observaciones = String.Empty
		vgc_NumeroCheque = String.Empty
		vgc_NumeroGestionGeco = String.Empty
		vgd_FechaHoraImpresion = DateTime.Now
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
