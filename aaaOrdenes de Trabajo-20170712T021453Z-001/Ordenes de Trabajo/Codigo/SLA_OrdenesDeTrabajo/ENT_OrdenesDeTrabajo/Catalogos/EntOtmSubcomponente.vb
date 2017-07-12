Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmSubcomponente
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdEspacio AS Integer 'Llave primaria de la tabla otm_espacio que se asocia con la secuencia sq_id_espacio
	Private vgn_IdSubcomponente AS Integer 'Llave primaria de la tabla otm_subcomponente que se asocia con la secuencia sq_id_subcomponente
	Private vgc_Descripcion AS String 'Descripción del subcomponente
	Private vgn_Orden AS Integer 'Orden de visualización
	Private vgc_Estado AS String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_espacio que se asocia con la secuencia sq_id_espacio
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>13/11/2015 01:36:20 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdEspacio() As Integer
		Get
			Return vgn_IdEspacio
		End Get
		Set(ByVal value As Integer)
			vgn_IdEspacio = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_subcomponente que se asocia con la secuencia sq_id_subcomponente
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>13/11/2015 01:36:20 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdSubcomponente() As Integer
		Get
			Return vgn_IdSubcomponente
		End Get
		Set(ByVal value As Integer)
			vgn_IdSubcomponente = value
		End Set
	End Property

	''' <summary>
	''' Descripción del subcomponente
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>13/11/2015 01:36:20 p.m.</creationDate>
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
	''' Orden de visualización
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>13/11/2015 01:36:20 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Orden() As Integer
		Get
			Return vgn_Orden
		End Get
		Set(ByVal value As Integer)
			vgn_Orden = value
		End Set
	End Property

	''' <summary>
	''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>13/11/2015 01:36:20 p.m.</creationDate>
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
	''' <creationDate>13/11/2015 01:36:20 p.m.</creationDate>
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
	''' <creationDate>13/11/2015 01:36:20 p.m.</creationDate>
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
		vgn_IdEspacio = 0
		vgn_IdSubcomponente = 0
		vgc_Descripcion = String.Empty
		vgn_Orden = 0
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
