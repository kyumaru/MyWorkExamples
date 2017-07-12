Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtcEstadoOrdenTrabajo
		Inherits EntBase
#Region "Atributos"
	Private vgc_EstadoOrdenTrabajo AS String 'Llave primaria de la tabla otc_estado_orden_trabajo
	Private vgc_Descripcion AS String 'Descripción de la orden de trabajo
#End Region
#Region "Propiedades"
	''' <summary>
	''' Llave primaria de la tabla otc_estado_orden_trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property EstadoOrdenTrabajo() As String
		Get
			Return vgc_EstadoOrdenTrabajo
		End Get
		Set(ByVal value As String)
			vgc_EstadoOrdenTrabajo = value
		End Set
	End Property

	''' <summary>
	''' Descripción de la orden de trabajo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
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
		vgc_EstadoOrdenTrabajo = String.Empty
		vgc_Descripcion = String.Empty
	End Sub
#End Region
	End Class
End Namespace
