Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOtfFichaTecnicaSubcomp
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_IdPreOrdenTrabajo AS Integer 'Llave primaria de la tabla otf_pre_orden_trabajo que se asocia con la secuencia sq_id_pre_orden_trabajo
	Private vgn_IdEspacio AS Integer 'Llave primaria de la tabla otm_espacio que se asocia con la secuencia sq_id_espacio
	Private vgn_IdSubcomponente AS Integer 'Llave primaria de la tabla otm_subcomponente que se asocia con la secuencia sq_id_subcomponente
#End Region
#Region "Propiedades"
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
	''' Llave primaria de la tabla otm_espacio que se asocia con la secuencia sq_id_espacio
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
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
	''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdSubcomponente() As Integer
		Get
			Return vgn_IdSubcomponente
		End Get
		Set(ByVal value As Integer)
			vgn_IdSubcomponente = value
		End Set
	End Property

#End Region

#Region "Constructores"
	Public Sub New()
		MyBase.New()
		vgn_IdUbicacion = 0
		vgn_IdPreOrdenTrabajo = 0
		vgn_IdEspacio = 0
		vgn_IdSubcomponente = 0
	End Sub
#End Region
	End Class
End Namespace
