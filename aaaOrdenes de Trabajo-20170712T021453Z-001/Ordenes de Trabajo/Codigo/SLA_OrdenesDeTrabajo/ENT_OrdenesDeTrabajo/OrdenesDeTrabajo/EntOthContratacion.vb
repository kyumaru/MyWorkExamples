Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOthContratacion
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgc_IdOrdenTrabajo AS String 'Identificador único alfanumérico de la orden de trabajo
	Private vgn_Version AS Integer 'Numero de version del proceso de contratación asociado a una orden de trabajo
	Private vgn_IdViaContrato AS Integer 'Llave primaria de la tabla otm_via_contrato que se asocia con la secuencia sq_id_via_contrato
	Private vgn_Editable AS Integer 'Indicador para marcar la vesión de la contratación como editables o no editable 0: no editable 1: editable, valor por defecto : 1
	Private vgn_NumeroSolicitud AS Integer 'Número de solicitud registrado en geco
	Private vgc_NumeroDecisionInicial AS String 'Número de decisión inicial registrado en geco
	Private vgc_NumeroContrato AS String 'Número de contrato. ejemplo: 2015cd-000224-osg
	Private vgc_NombreContrato AS String 'Nombre del contrato
	Private vgc_Usuario AS String 'Usuario
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
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
	''' Identificador único alfanumérico de la orden de trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdOrdenTrabajo() As String
		Get
			Return vgc_IdOrdenTrabajo
		End Get
		Set(ByVal value As String)
			vgc_IdOrdenTrabajo = value
		End Set
	End Property

	''' <summary>
	''' Numero de version del proceso de contratación asociado a una orden de trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Version() As Integer
		Get
			Return vgn_Version
		End Get
		Set(ByVal value As Integer)
			vgn_Version = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_via_contrato que se asocia con la secuencia sq_id_via_contrato
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdViaContrato() As Integer
		Get
			Return vgn_IdViaContrato
		End Get
		Set(ByVal value As Integer)
			vgn_IdViaContrato = value
		End Set
	End Property

	''' <summary>
	''' Indicador para marcar la vesión de la contratación como editables o no editable 0: no editable 1: editable, valor por defecto : 1
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Editable() As Integer
		Get
			Return vgn_Editable
		End Get
		Set(ByVal value As Integer)
			vgn_Editable = value
		End Set
	End Property

	''' <summary>
	''' Número de solicitud registrado en geco
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NumeroSolicitud() As Integer
		Get
			Return vgn_NumeroSolicitud
		End Get
		Set(ByVal value As Integer)
			vgn_NumeroSolicitud = value
		End Set
	End Property

	''' <summary>
	''' Número de decisión inicial registrado en geco
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NumeroDecisionInicial() As String
		Get
			Return vgc_NumeroDecisionInicial
		End Get
		Set(ByVal value As String)
			vgc_NumeroDecisionInicial = value
		End Set
	End Property

	''' <summary>
	''' Número de contrato. ejemplo: 2015cd-000224-osg
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NumeroContrato() As String
		Get
			Return vgc_NumeroContrato
		End Get
		Set(ByVal value As String)
			vgc_NumeroContrato = value
		End Set
	End Property

	''' <summary>
	''' Nombre del contrato
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NombreContrato() As String
		Get
			Return vgc_NombreContrato
		End Get
		Set(ByVal value As String)
			vgc_NombreContrato = value
		End Set
	End Property

	''' <summary>
	''' Usuario
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
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
	''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
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
		vgc_IdOrdenTrabajo = String.Empty
		vgn_Version = 0
		vgn_IdViaContrato = 0
		vgn_Editable = 0
		vgn_NumeroSolicitud = 0
		vgc_NumeroDecisionInicial = String.Empty
		vgc_NumeroContrato = String.Empty
		vgc_NombreContrato = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
