Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmViaCompraContrato
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdViaCompraContrato AS Integer 'Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_contrato
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgc_Descripcion AS String 'Descripción de la vía de contrato.
	Private vgn_TopeEconomico AS Double 'Tope económico establecido para el tipo de vía de contrato
	Private vgc_Estado AS String 'Estado del registro - act: activa, ina: inactiva
	Private vgc_Ambito AS String 'Ámbito que abarca: con - contrataciones, com - compras, amb - ambos
	Private vgc_Usuario AS String 'Usuario
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_contrato
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>27/05/2016 02:21:34 p.m.</creationDate>
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
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>27/05/2016 02:21:34 p.m.</creationDate>
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
	''' Descripción de la vía de contrato.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>27/05/2016 02:21:34 p.m.</creationDate>
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
	''' Tope económico establecido para el tipo de vía de contrato
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>27/05/2016 02:21:34 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property TopeEconomico() As Double
		Get
			Return vgn_TopeEconomico
		End Get
		Set(ByVal value As Double)
			vgn_TopeEconomico = value
		End Set
	End Property

	''' <summary>
	''' Estado del registro - act: activa, ina: inactiva
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>27/05/2016 02:21:34 p.m.</creationDate>
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
	''' Ámbito que abarca: con - contrataciones, com - compras, amb - ambos
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>27/05/2016 02:21:34 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Ambito() As String
		Get
			Return vgc_Ambito
		End Get
		Set(ByVal value As String)
			vgc_Ambito = value
		End Set
	End Property

	''' <summary>
	''' Usuario
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>27/05/2016 02:21:34 p.m.</creationDate>
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
	''' <creationDate>27/05/2016 02:21:34 p.m.</creationDate>
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
		vgn_IdViaCompraContrato = 0
		vgn_IdUbicacion = 0
		vgc_Descripcion = String.Empty
		vgn_TopeEconomico = 0
		vgc_Estado = String.Empty
		vgc_Ambito = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
