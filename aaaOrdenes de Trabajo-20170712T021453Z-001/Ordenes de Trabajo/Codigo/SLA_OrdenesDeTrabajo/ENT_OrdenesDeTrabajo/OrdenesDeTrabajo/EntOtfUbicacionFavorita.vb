Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOtfUbicacionFavorita
		Inherits EntBase
#Region "Atributos"
	Private vgn_NumEmpleado AS Double 'Número de empleado
	Private vgn_IdUbicacion AS Integer 'Id de ubicación de trabajo del empleado
#End Region
#Region "Propiedades"
	''' <summary>
	''' Número de empleado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>06/10/2015 05:58:38 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property NumEmpleado() As Double
		Get
			Return vgn_NumEmpleado
		End Get
		Set(ByVal value As Double)
			vgn_NumEmpleado = value
		End Set
	End Property

	''' <summary>
	''' Id de ubicación de trabajo del empleado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>06/10/2015 05:58:38 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdUbicacion() As Integer
		Get
			Return vgn_IdUbicacion
		End Get
		Set(ByVal value As Integer)
			vgn_IdUbicacion = value
		End Set
	End Property

#End Region

#Region "Constructores"
	Public Sub New()
		MyBase.New()
		vgn_NumEmpleado = 0
		vgn_IdUbicacion = 0
	End Sub
#End Region
	End Class
End Namespace
