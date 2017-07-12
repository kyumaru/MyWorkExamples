Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtcTipoOrdenTrabajo
		Inherits EntBase
#Region "Atributos"
	Private vgc_TipoOrdenTrabajo AS String 'Tipo de orden de trabajo: ordinaria, emergencia, preventivo
	Private vgc_Descripcion AS String 'Descripción de tipo de orden de trabajo
#End Region
#Region "Propiedades"
	''' <summary>
	''' Tipo de orden de trabajo: ordinaria, emergencia, preventivo
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property TipoOrdenTrabajo() As String
		Get
			Return vgc_TipoOrdenTrabajo
		End Get
		Set(ByVal value As String)
			vgc_TipoOrdenTrabajo = value
		End Set
	End Property

	''' <summary>
	''' Descripción de tipo de orden de trabajo
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
		vgc_TipoOrdenTrabajo = String.Empty
		vgc_Descripcion = String.Empty
	End Sub
#End Region
	End Class
End Namespace
