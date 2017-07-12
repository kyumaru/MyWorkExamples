Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttOrdnTrabDecInicial
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgc_IdOrdenTrabajo AS String 'Identificador único alfanumérico de la orden de trabajo
	Private vgn_IdTipoObra AS Integer 'Llave primaria de la tabla otm_rubro_desicion_inicia que se asocia con la secuencia sq_rubro_desicion_inicia
	Private vgn_IdRubroDecisionInicia AS Integer 'Llave primaria de la tabla otm_rubro_desicion_inicia que se asocia con la secuencia sq_rubro_desicion_inicia
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
	Private vgc_Valor AS String 'Valor asociado al rubro contemplado para la desición inicial
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/03/2016 08:44:55 a.m.</creationDate>
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
	''' <creationDate>31/03/2016 08:44:55 a.m.</creationDate>
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
	''' Llave primaria de la tabla otm_rubro_desicion_inicia que se asocia con la secuencia sq_rubro_desicion_inicia
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/03/2016 08:44:55 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdTipoObra() As Integer
		Get
			Return vgn_IdTipoObra
		End Get
		Set(ByVal value As Integer)
			vgn_IdTipoObra = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_rubro_desicion_inicia que se asocia con la secuencia sq_rubro_desicion_inicia
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/03/2016 08:44:55 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdRubroDecisionInicia() As Integer
		Get
			Return vgn_IdRubroDecisionInicia
		End Get
		Set(ByVal value As Integer)
			vgn_IdRubroDecisionInicia = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/03/2016 08:44:55 a.m.</creationDate>
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
	''' <creationDate>31/03/2016 08:44:55 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property TimeStamp() As DateTime
		Get
			Return vgd_TimeStamp
		End Get
		Set(ByVal value As DateTime)
			vgd_TimeStamp = value
		End Set
	End Property

	''' <summary>
	''' Valor asociado al rubro contemplado para la desición inicial
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/03/2016 08:44:55 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Valor() As String
		Get
			Return vgc_Valor
		End Get
		Set(ByVal value As String)
			vgc_Valor = value
		End Set
	End Property

#End Region

#Region "Constructores"
	Public Sub New()
		MyBase.New()
		vgn_IdUbicacion = 0
		vgc_IdOrdenTrabajo = String.Empty
		vgn_IdTipoObra = 0
		vgn_IdRubroDecisionInicia = 0
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
		vgc_Valor = String.Empty
	End Sub
#End Region
	End Class
End Namespace
