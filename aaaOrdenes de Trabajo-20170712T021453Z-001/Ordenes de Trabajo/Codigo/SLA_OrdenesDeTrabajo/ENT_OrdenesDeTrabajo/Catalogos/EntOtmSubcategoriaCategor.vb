Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmSubcategoriaCategor
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdCategoriaMaterial AS Integer 'Llave primaria de la tabla otm_categoria_material que se asocia con la secuencia sq_id_categoria_material
	Private vgn_IdSubcategoriaMaterial AS Integer 'Llave primaria de la tabla otm_subcategoria_material que se asocia con la secuencia sq_id_subcategoria_material
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otm_categoria_material que se asocia con la secuencia sq_id_categoria_material
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>19/05/2016 05:14:54 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdCategoriaMaterial() As Integer
		Get
			Return vgn_IdCategoriaMaterial
		End Get
		Set(ByVal value As Integer)
			vgn_IdCategoriaMaterial = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_subcategoria_material que se asocia con la secuencia sq_id_subcategoria_material
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>19/05/2016 05:14:54 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdSubcategoriaMaterial() As Integer
		Get
			Return vgn_IdSubcategoriaMaterial
		End Get
		Set(ByVal value As Integer)
			vgn_IdSubcategoriaMaterial = value
		End Set
	End Property

	''' <summary>
	''' Usuario que crea o modifica el registro.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>19/05/2016 05:14:54 p.m.</creationDate>
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
	''' <creationDate>19/05/2016 05:14:54 p.m.</creationDate>
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
		vgn_IdCategoriaMaterial = 0
		vgn_IdSubcategoriaMaterial = 0
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
