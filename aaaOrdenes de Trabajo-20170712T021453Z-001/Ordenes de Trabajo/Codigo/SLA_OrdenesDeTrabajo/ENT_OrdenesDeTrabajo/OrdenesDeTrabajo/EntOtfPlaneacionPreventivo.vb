Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
    <Serializable()> _
    Public Class EntOtfPlaneacionPreventivo
        Inherits EntBase
#Region "Atributos"
        Private vgn_ConsecutivoPropuesto As Integer 'Consecutivo propuesto en la planificación para la generación de la orden de trabajo
        Private vgn_IdCategoriaServicio As Integer 'Llave primaria de la tabla otm_categoria_servicio que se asocia con la secuencia sq_id_categoria_servicio
        Private vgn_IdActividad As Integer 'Llave primaria de la tabla otm_actividad que se asocia con la secuencia sq_id_actividad
        Private vgn_IdLugarTrabajo As Integer 'Llave primaria de la tabla otm_lugar_trabajo que se asocia con la secuencia sq_id_lugar_trabajo
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
        Private vgn_IdUbicacionAdministra As Integer 'Id de la ubicación que administra los datos del catálogo
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Consecutivo propuesto en la planificación para la generación de la orden de trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/09/2015 03:12:05 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property ConsecutivoPropuesto() As Integer
            Get
                Return vgn_ConsecutivoPropuesto
            End Get
            Set(ByVal value As Integer)
                vgn_ConsecutivoPropuesto = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_categoria_servicio que se asocia con la secuencia sq_id_categoria_servicio
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/09/2015 03:12:05 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdCategoriaServicio() As Integer
            Get
                Return vgn_IdCategoriaServicio
            End Get
            Set(ByVal value As Integer)
                vgn_IdCategoriaServicio = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_actividad que se asocia con la secuencia sq_id_actividad
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/09/2015 03:12:05 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdActividad() As Integer
            Get
                Return vgn_IdActividad
            End Get
            Set(ByVal value As Integer)
                vgn_IdActividad = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_lugar_trabajo que se asocia con la secuencia sq_id_lugar_trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/09/2015 03:12:05 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdLugarTrabajo() As Integer
            Get
                Return vgn_IdLugarTrabajo
            End Get
            Set(ByVal value As Integer)
                vgn_IdLugarTrabajo = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/09/2015 03:12:05 p.m.</creationDate>
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
        ''' <creationDate>09/09/2015 03:12:05 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property TimeStamp() As DateTime
            Get
                Return vgd_TimeStamp
            End Get
            Set(ByVal value As DateTime)
                vgd_TimeStamp = value
            End Set
        End Property

        ''' <summary>
        ''' Id de la ubicación que administra los datos del catálogo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>06/10/2015 02:30:34 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdUbicacionAdministra() As Integer
            Get
                Return vgn_IdUbicacionAdministra
            End Get
            Set(ByVal value As Integer)
                vgn_IdUbicacionAdministra = value
            End Set
        End Property

#End Region

#Region "Constructores"
        Public Sub New()
            MyBase.New()
            vgn_ConsecutivoPropuesto = 0
            vgn_IdCategoriaServicio = 0
            vgn_IdActividad = 0
            vgn_IdLugarTrabajo = 0
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
            vgn_IdUbicacionAdministra = 0
        End Sub
#End Region
    End Class
End Namespace
