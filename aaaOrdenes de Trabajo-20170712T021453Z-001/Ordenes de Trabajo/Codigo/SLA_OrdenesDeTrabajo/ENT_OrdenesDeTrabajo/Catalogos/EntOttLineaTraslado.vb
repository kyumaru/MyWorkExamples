Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace ORDENES_TRABAJO.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOttLineaTraslado
		Inherits EntBase
#Region "Atributos"
	Private vgn_Anno AS Integer 'Año de la solicitud
	Private vgn_IdSolicitudTraslado AS Integer 'Consecutivo de la solicitud. el consecutivo es anual.
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	Private vgn_IdAlmacen AS Integer 'Llave primaria de la tabla otf_inventario correspondiente al almacén principal
	Private vgn_IdMaterial AS Integer 'Llave primaria de la tabla otf_inventario correspondiente al material a trasladar
	Private vgn_CantidadRequerida AS Double 'Cantidad a trasladar de material
	Private vgn_CantidadRetirada AS Double 'Cantidad retirada de material
	Private vgc_Detalle AS String 'Detalle
	Private vgc_Estado AS String 'Estado de la línea
	Private vgc_Usuario AS String 'Usuario que crea o modifica el registro.
	Private vgd_TimeStamp AS DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
	''' <summary>
	''' Año de la solicitud
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:16:31 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Anno() As Integer
		Get
			Return vgn_Anno
		End Get
		Set(ByVal value As Integer)
			vgn_Anno = value
		End Set
	End Property

	''' <summary>
	''' Consecutivo de la solicitud. el consecutivo es anual.
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:16:31 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdSolicitudTraslado() As Integer
		Get
			Return vgn_IdSolicitudTraslado
		End Get
		Set(ByVal value As Integer)
			vgn_IdSolicitudTraslado = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:16:31 a.m.</creationDate>
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
	''' Llave primaria de la tabla otf_inventario correspondiente al almacén principal
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:16:31 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdAlmacen() As Integer
		Get
			Return vgn_IdAlmacen
		End Get
		Set(ByVal value As Integer)
			vgn_IdAlmacen = value
		End Set
	End Property

	''' <summary>
	''' Llave primaria de la tabla otf_inventario correspondiente al material a trasladar
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:16:31 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property IdMaterial() As Integer
		Get
			Return vgn_IdMaterial
		End Get
		Set(ByVal value As Integer)
			vgn_IdMaterial = value
		End Set
	End Property

	''' <summary>
	''' Cantidad a trasladar de material
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:16:31 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CantidadRequerida() As Double
		Get
			Return vgn_CantidadRequerida
		End Get
		Set(ByVal value As Double)
			vgn_CantidadRequerida = value
		End Set
	End Property

	''' <summary>
	''' Cantidad retirada de material
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:16:31 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property CantidadRetirada() As Double
		Get
			Return vgn_CantidadRetirada
		End Get
		Set(ByVal value As Double)
			vgn_CantidadRetirada = value
		End Set
	End Property

	''' <summary>
	''' Detalle
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:16:31 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Property Detalle() As String
		Get
			Return vgc_Detalle
		End Get
		Set(ByVal value As String)
			vgc_Detalle = value
		End Set
	End Property

	''' <summary>
	''' Estado de la línea
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>09/08/2016 09:16:31 a.m.</creationDate>
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
	''' <creationDate>09/08/2016 09:16:31 a.m.</creationDate>
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
	''' <creationDate>09/08/2016 09:16:31 a.m.</creationDate>
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
		vgn_Anno = 0
		vgn_IdSolicitudTraslado = 0
		vgn_IdUbicacion = 0
		vgn_IdAlmacen = 0
		vgn_IdMaterial = 0
		vgn_CantidadRequerida = 0
		vgn_CantidadRetirada = 0
		vgc_Detalle = String.Empty
		vgc_Estado = String.Empty
		vgc_Usuario = String.Empty
		vgd_TimeStamp = DateTime.Now
	End Sub
#End Region
	End Class
End Namespace
