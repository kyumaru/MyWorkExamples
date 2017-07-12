Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
    <Serializable()> _
    Public Class EntOttAdjuntoOrdenTrabajo
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdAdjuntoOrdenTrabajo As Integer 'Llave primaria de la tabla ott_adjunto_orden_trabajo que se asocia con la secuencia sq_id_adjunto_orden_trabajo
        Private vgn_IdUbicacion As Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgc_IdOrdenTrabajo As String 'Identificador único alfanumérico de la orden de trabajo
        Private vgc_NombreArchivo As String 'Nombre del archivo adjunto
        Private vgo_Archivo As Byte() 'Documento adjunto
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
        Private vgn_IdTipoDocumento As Integer
        Private vgn_IdEtapaOrdentrabajo As Integer
        Private vgc_Descripcion As String
#End Region
#Region "Propiedades"

        ''' <summary>
        ''' descripcion del documento
        ''' </summary>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>26/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Property Descripcion() As String
            Get
                Return vgc_Descripcion
            End Get
            Set(ByVal value As String)
                vgc_Descripcion = value
            End Set
        End Property
        ''' <summary>
        ''' Id del tipo de documento
        ''' </summary>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>26/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdTipoDocumento() As Integer
            Get
                Return vgn_IdTipoDocumento
            End Get
            Set(ByVal value As Integer)
                vgn_IdTipoDocumento = value
            End Set
        End Property

        ''' <summary>
        ''' Id de la etapa orden trabajo
        ''' </summary>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>26/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdEtapaOrdentrabajo() As Integer
            Get
                Return vgn_IdEtapaOrdentrabajo
            End Get
            Set(ByVal value As Integer)
                vgn_IdEtapaOrdentrabajo = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla ott_adjunto_orden_trabajo que se asocia con la secuencia sq_id_adjunto_orden_trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
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
        ''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
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
        ''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
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
        ''' Nombre del archivo adjunto
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
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
        ''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
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
        ''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
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
        ''' <creationDate>25/11/2015 04:13:29 p.m.</creationDate>
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
            vgn_IdAdjuntoOrdenTrabajo = 0
            vgn_IdTipoDocumento = 0
            vgn_IdEtapaOrdentrabajo = 0
            vgn_IdUbicacion = 0
            vgc_IdOrdenTrabajo = String.Empty
            vgc_NombreArchivo = String.Empty
            vgo_Archivo = Nothing
            vgc_Usuario = String.Empty
            vgc_Descripcion = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
    End Class
End Namespace