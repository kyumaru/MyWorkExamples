Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace ORDENES_TRABAJO.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtcEstadoTraslado
		Inherits EntBase
#Region "Atributos"
	Private vgc_EstadoTraslado AS String 'Llave de la tabla otc_estado_traslado
	Private vgc_Descripcion AS String 'Descripción
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave de la tabla otc_estado_traslado
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 08:59:53 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property EstadoTraslado() As String
		Get
			Return vgc_EstadoTraslado
		End Get
		Set(ByVal value As String)
			vgc_EstadoTraslado = value
		End Set
	End Property

	''' <summary>
	''' Descripción
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 08:59:53 a.m.</creationDate>
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
		vgc_EstadoTraslado = String.Empty
		vgc_Descripcion = String.Empty
	End Sub
#End Region
	End Class
End Namespace
