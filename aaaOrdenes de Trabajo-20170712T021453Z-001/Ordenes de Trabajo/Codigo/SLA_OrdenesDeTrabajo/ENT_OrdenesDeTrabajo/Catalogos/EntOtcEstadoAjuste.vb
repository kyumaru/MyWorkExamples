Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtcEstadoAjuste
		Inherits EntBase
#Region "Atributos"
	Private vgc_EstadoAjuste AS String 'Llave de la tabla otc_estado_ajuste
	Private vgc_Descripcion AS String 'Descripción de la orden de trabajo
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave de la tabla otc_estado_ajuste
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 01:44:49 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property EstadoAjuste() As String
		Get
			Return vgc_EstadoAjuste
		End Get
		Set(ByVal value As String)
			vgc_EstadoAjuste = value
		End Set
	End Property

	''' <summary>
	''' Descripción de la orden de trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>24/01/2017 01:44:49 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Descripcion() As String
		Get
			Return vgc_Descripcion
		End Get
		Set(ByVal value As String)
			vgc_Descripcion = value
		End Set
	End Property

#End Region

#Region "Constructores"
	Public Sub New()
		MyBase.New()
		vgc_EstadoAjuste = String.Empty
		vgc_Descripcion = String.Empty
	End Sub
#End Region
	End Class
End Namespace
