Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOtfRevisionPreOrdenTra
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdRevisionPreOrdenTra AS Integer 'Llave primaria de la tabla otf_revision_orden_trabaj que se asocia con la secuencia sq_id_revision_orden_trabaj
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_IdPreOrdenTrabajo AS Integer 'Llave primaria de la tabla otf_pre_orden_trabajo que se asocia con la secuencia sq_id_pre_orden_trabajo
	Private vgc_Observaciones AS String 'Observaciones indicadas por el revisor
	Private vgc_Estado AS String 'Estado del registro - apr: aprueba, dev: devuelve, den: deniega
	Private vgc_Usuario AS String 'Usuario que realiza la revisión
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otf_revision_orden_trabaj que se asocia con la secuencia sq_id_revision_orden_trabaj
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdRevisionPreOrdenTra() As Integer
		Get
			Return vgn_IdRevisionPreOrdenTra
		End Get
		Set(ByVal value As Integer)
			vgn_IdRevisionPreOrdenTra = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
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
	''' Llave primaria de la tabla otf_pre_orden_trabajo que se asocia con la secuencia sq_id_pre_orden_trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdPreOrdenTrabajo() As Integer
		Get
			Return vgn_IdPreOrdenTrabajo
		End Get
		Set(ByVal value As Integer)
			vgn_IdPreOrdenTrabajo = value
		End Set
	End Property

	''' <summary>
	''' Observaciones indicadas por el revisor
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
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
	''' Estado del registro - apr: aprueba, dev: devuelve, den: deniega
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
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
	''' Usuario que realiza la revisión
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
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
	''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
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
		vgn_IdRevisionPreOrdenTra = 0
		vgn_IdUbicacion = 0
		vgn_IdPreOrdenTrabajo = 0
		vgc_Observaciones = String.Empty
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
