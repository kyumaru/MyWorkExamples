Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
    <Serializable()> _
    Public Class EntOtfAdjuntoOrdenTrabajo
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdUbicacion As Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgn_Anno As Integer 'Año de solicitud de la ot
        Private vgn_Consecutivo As Integer 'Consecutivo de orden de trabajo, se reinicia cada año.
        Private vgn_IdAdjuntoOrdenTrabajo As Integer 'Llave primaria de la tabla otf_adjunto_orden_trabajo que se asocia con la secuencia sq_id_adjunto_orden_trabajo
        Private vgc_NombreArchivo As String 'Nombre del archivo adjunto
        Private vgo_Archivo As Byte() 'Documento adjunto
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"

        ''' <summary>
        ''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/10/2015 11:17:47 a.m.</creationDate>
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
        ''' Año de solicitud de la ot
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:24:17 a.m.</creationDate>
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
        ''' Consecutivo de orden de trabajo, se reinicia cada año.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:24:17 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Consecutivo() As Integer
            Get
                Return vgn_Consecutivo
            End Get
            Set(ByVal value As Integer)
                vgn_Consecutivo = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otf_adjunto_orden_trabajo que se asocia con la secuencia sq_id_adjunto_orden_trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:24:17 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdAdjuntoOrdenTrabajo() As Integer
            Get
                Return vgn_IdAdjuntoOrdenTrabajo
            End Get
            Set(ByVal value As Integer)
                vgn_IdAdjuntoOrdenTrabajo = value
            End Set
        End Property

        ''' <summary>
        ''' Nombre del archivo adjunto
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:24:17 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NombreArchivo() As String
            Get
                Return vgc_NombreArchivo
            End Get
            Set(ByVal value As String)
                vgc_NombreArchivo = value
            End Set
        End Property

        ''' <summary>
        ''' Documento adjunto
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:24:17 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Archivo() As Byte()
            Get
                Return vgo_Archivo
            End Get
            Set(ByVal value As Byte())
                vgo_Archivo = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:24:17 a.m.</creationDate>
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
        ''' <creationDate>31/08/2015 08:24:17 a.m.</creationDate>
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
            vgn_IdUbicacion = 0
            vgn_Anno = 0
            vgn_Consecutivo = 0
            vgn_IdAdjuntoOrdenTrabajo = 0
            vgc_NombreArchivo = String.Empty
            vgo_Archivo = Nothing
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
    End Class
End Namespace
