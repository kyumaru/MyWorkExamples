Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
    <Serializable()> _
    Public Class EntOttSolicitudRetiro
        Inherits EntBase
#Region "Atributos"
        Private vgn_Anno As Integer 'Llave primaria de la tabla ott_solicitud_retiro
        Private vgn_IdSolicitudRetiro As Integer 'Llave primaria de la tabla ott_solicitud_retiro
        Private vgc_EstadoSolicitudRetiro As String 'Llave primaria de la tabla otc_estado_solicitud_ret
        Private vgn_IdUbicacion As Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgc_IdOrdenTrabajo As String 'Identificador único alfanumérico de la orden de trabajo
        Private vgd_FechaRetiro As DateTime 'Fecha planificada para retirar el material
        Private vgc_JornadaRetiro As String 'Jornada del día en que retirará el material: man- mañana, tar- tarde. valor por defecto man.
        Private vgn_NumeroSalida As Integer 'Número de salida. consecutivo anual.
        Private vgd_FechaHoraImpresion As DateTime 'Fecha y hora de impresión para alistado de materiales
        Private vgd_FechaHoraRetiro As DateTime 'Fecha y hora de retiro de materiales del almacén o bodega
        Private vgc_UsuarioRetira As String 'Usuario que realiza el retiro
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Llave primaria de la tabla ott_solicitud_retiro
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
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
        ''' Llave primaria de la tabla ott_solicitud_retiro
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdSolicitudRetiro() As Integer
            Get
                Return vgn_IdSolicitudRetiro
            End Get
            Set(ByVal value As Integer)
                vgn_IdSolicitudRetiro = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otc_estado_solicitud_ret
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property EstadoSolicitudRetiro() As String
            Get
                Return vgc_EstadoSolicitudRetiro
            End Get
            Set(ByVal value As String)
                vgc_EstadoSolicitudRetiro = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
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
        ''' Identificador único alfanumérico de la orden de trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdOrdenTrabajo() As String
            Get
                Return vgc_IdOrdenTrabajo
            End Get
            Set(ByVal value As String)
                vgc_IdOrdenTrabajo = value
            End Set
        End Property

        ''' <summary>
        ''' Fecha planificada para retirar el material
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property FechaRetiro() As DateTime
            Get
                Return vgd_FechaRetiro
            End Get
            Set(ByVal value As DateTime)
                vgd_FechaRetiro = value
            End Set
        End Property

        ''' <summary>
        ''' Jornada del día en que retirará el material: man- mañana, tar- tarde. valor por defecto man.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
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
        ''' Número de salida. consecutivo anual.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NumeroSalida() As Integer
            Get
                Return vgn_NumeroSalida
            End Get
            Set(ByVal value As Integer)
                vgn_NumeroSalida = value
            End Set
        End Property

        ''' <summary>
        ''' Fecha y hora de impresión para alistado de materiales
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property FechaHoraImpresion() As DateTime
            Get
                Return vgd_FechaHoraImpresion
            End Get
            Set(ByVal value As DateTime)
                vgd_FechaHoraImpresion = value
            End Set
        End Property

        ''' <summary>
        ''' Fecha y hora de retiro de materiales del almacén o bodega
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property FechaHoraRetiro() As DateTime
            Get
                Return vgd_FechaHoraRetiro
            End Get
            Set(ByVal value As DateTime)
                vgd_FechaHoraRetiro = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que realiza el retiro
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property UsuarioRetira() As String
            Get
                Return vgc_UsuarioRetira
            End Get
            Set(ByVal value As String)
                vgc_UsuarioRetira = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
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
        ''' <creationDate>21/06/2016 01:52:19 p.m.</creationDate>
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
            vgn_IdSolicitudRetiro = 0
            vgc_EstadoSolicitudRetiro = String.Empty
            vgn_IdUbicacion = 0
            vgc_IdOrdenTrabajo = String.Empty
            vgd_FechaRetiro = DateTime.Now
            vgc_JornadaRetiro = String.Empty
            vgn_NumeroSalida = 0
            vgd_FechaHoraImpresion = DateTime.Now
            vgd_FechaHoraRetiro = DateTime.Now
            vgc_UsuarioRetira = String.Empty
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
    End Class
End Namespace
