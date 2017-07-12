Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttDesicionInicial
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgc_IdOrdenTrabajo AS String 'Identificador único alfanumérico de la orden de trabajo
	Private vgn_IdTipoObra AS Integer 'Llave primaria de la tabla otm_rubro_desicion_inicia que se asocia con la secuencia sq_rubro_desicion_inicia
	Private vgd_Fecha AS DateTime 'Fecha en la que se crea la desición inicial
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro. 
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>17/03/2016 08:50:33 a.m.</creationDate>
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
	''' <creationDate>17/03/2016 08:50:33 a.m.</creationDate>
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
	''' <creationDate>17/03/2016 08:50:33 a.m.</creationDate>
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
	''' Fecha en la que se crea la desición inicial
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>17/03/2016 08:50:33 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Fecha() As DateTime
		Get
			Return vgd_Fecha
		End Get
		Set(ByVal value As DateTime)
			vgd_Fecha = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro. 
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>17/03/2016 08:50:33 a.m.</creationDate>
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
	''' <creationDate>17/03/2016 08:50:33 a.m.</creationDate>
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
		vgn_IdTipoObra = 0
		vgd_Fecha = DateTime.Now
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
