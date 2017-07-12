Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace ORDENES_TRABAJO.EntidadNegocio.Catalogos
    <Serializable()> _
    Public Class EntOttSolicitudTraslado
        Inherits EntBase
#Region "Atributos"
        Private vgn_Anno As Integer 'Año de la solicitud
        Private vgn_IdSolicitudTraslado As Integer 'Consecutivo de la solicitud. el consecutivo es anual.
        Private vgn_IdUbicacion As Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgn_IdAlmacen As Integer 'Almacén origen
        Private vgn_IdBodega As Integer 'Bodega destino
        Private vgc_EstadoTraslado As String 'Llave de la tabla otc_estado_traslado
        Private vgd_FechaRegistroSolicitud As DateTime 'Fecha de registro de la solicitud
        Private vgd_FechaPropuestaSalida As DateTime 'Fecha propuesta de salida del material
        Private vgc_JornadaRetiro As String 'Jornada del día en que retirará el material: man- mañana, tar- tarde. valor por defecto man.
        Private vgc_Observaciones As String 'Observaciones a la solicitud
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
        Private vgc_NumeroSolicitud As String 'Control de concurrencia - valor por defecto: fecha y hora del sistema
        Private vgc_DescEstadoTraslado As String 'Control de concurrencia - valor por defecto: fecha y hora del sistema
        Private vgc_Encargado As String 'Control de concurrencia - valor por defecto: fecha y hora del sistema
        Private vgc_ObservacionesDevueltas As String 'Control de concurrencia - valor por defecto: fecha y hora del sistema

#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Año de la solicitud
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
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
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
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
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
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
        ''' Almacén origen
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
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
        ''' Bodega destino
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdBodega() As Integer
            Get
                Return vgn_IdBodega
            End Get
            Set(ByVal value As Integer)
                vgn_IdBodega = value
            End Set
        End Property

        ''' <summary>
        ''' Llave de la tabla otc_estado_traslado
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
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
        ''' Fecha de registro de la solicitud
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property FechaRegistroSolicitud() As DateTime
            Get
                Return vgd_FechaRegistroSolicitud
            End Get
            Set(ByVal value As DateTime)
                vgd_FechaRegistroSolicitud = value
            End Set
        End Property

        ''' <summary>
        ''' Fecha propuesta de salida del material
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property FechaPropuestaSalida() As DateTime
            Get
                Return vgd_FechaPropuestaSalida
            End Get
            Set(ByVal value As DateTime)
                vgd_FechaPropuestaSalida = value
            End Set
        End Property

        ''' <summary>
        ''' Jornada del día en que retirará el material: man- mañana, tar- tarde. valor por defecto man.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property JornadaRetiro() As String
            Get
                Return vgc_JornadaRetiro
            End Get
            Set(ByVal value As String)
                vgc_JornadaRetiro = value
            End Set
        End Property

        ''' <summary>
        ''' Observaciones a la solicitud
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Observaciones() As String
            Get
                Return vgc_Observaciones
            End Get
            Set(ByVal value As String)
                vgc_Observaciones = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
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
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
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
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NumeroSolicitud() As String
            Get
                Return vgc_NumeroSolicitud
            End Get
            Set(ByVal value As String)
                vgc_NumeroSolicitud = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property DescEstadoTraslado() As String
            Get
                Return vgc_DescEstadoTraslado
            End Get
            Set(ByVal value As String)
                vgc_DescEstadoTraslado = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Encargado() As String
            Get
                Return vgc_Encargado
            End Get
            Set(ByVal value As String)
                vgc_Encargado = value
            End Set
        End Property


        ''' <summary>
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property ObservacionesDevueltas() As String
            Get
                Return vgc_ObservacionesDevueltas
            End Get
            Set(ByVal value As String)
                vgc_ObservacionesDevueltas = value
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
            vgn_IdBodega = 0
            vgc_EstadoTraslado = String.Empty
            vgd_FechaRegistroSolicitud = DateTime.Now
            vgd_FechaPropuestaSalida = DateTime.Now
            vgc_JornadaRetiro = String.Empty
            vgc_Observaciones = String.Empty
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
            vgc_NumeroSolicitud = String.Empty
            vgc_DescEstadoTraslado = String.Empty
            vgc_Encargado = String.Empty
            vgc_ObservacionesDevueltas = String.Empty
        End Sub
#End Region
    End Class
End Namespace
