Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
    <Serializable()> _
    Public Class EntOttSolicitudMaterial
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdUbicacion As Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgc_IdOrdenTrabajo As String 'Identificador único alfanumérico de la orden de trabajo
        Private vgn_NoRequiereMaterial As Integer 'Indicador de no requiere material.
        Private vgc_Observaciones As String 'Observaciones generales de la solicitud de material
        Private vgn_IdTipoDocumentoSolicita As Integer 'Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento
        Private vgn_IdEtapaOrdenTrabajoSol As Integer 'Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo
        Private vgn_IdAdjuntoSolicita As Integer 'Llave primaria de la tabla otf_adjunto_orden_trabajo que se asocia con la secuencia sq_id_adjunto_orden_trabajo
        Private vgn_IdTipoDocumentoRespuesta As Integer 'Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento
        Private vgn_IdEtapaOrdenTrabajoRes As Integer 'Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo
        Private vgn_IdAdjuntoRespuesta As Integer 'Llave primaria de la tabla otf_adjunto_orden_trabajo que se asocia con la secuencia sq_id_adjunto_orden_trabajo
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
        Private vgc_HistorialJustificacion As String
        Private vgc_EstadoSolMaterial As String
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
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
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdOrdenTrabajo() As String
            Get
                Return vgc_IdOrdenTrabajo
            End Get
            Set(ByVal value As String)
                vgc_IdOrdenTrabajo = value
            End Set
        End Property

        Public Property EstadoSolMaterial() As String
            Get
                Return vgc_EstadoSolMaterial
            End Get
            Set(ByVal value As String)
                vgc_EstadoSolMaterial = value
            End Set
        End Property

        Public Property HistorialJustificacion() As String
            Get
                Return vgc_HistorialJustificacion
            End Get
            Set(ByVal value As String)
                vgc_HistorialJustificacion = value
            End Set
        End Property

        ''' <summary>
        ''' Indicador de no requiere material.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NoRequiereMaterial() As Integer
            Get
                Return vgn_NoRequiereMaterial
            End Get
            Set(ByVal value As Integer)
                vgn_NoRequiereMaterial = value
            End Set
        End Property

        ''' <summary>
        ''' Observaciones generales de la solicitud de material
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
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
        ''' Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdTipoDocumentoSolicita() As Integer
            Get
                Return vgn_IdTipoDocumentoSolicita
            End Get
            Set(ByVal value As Integer)
                vgn_IdTipoDocumentoSolicita = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdEtapaOrdenTrabajoSol() As Integer
            Get
                Return vgn_IdEtapaOrdenTrabajoSol
            End Get
            Set(ByVal value As Integer)
                vgn_IdEtapaOrdenTrabajoSol = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otf_adjunto_orden_trabajo que se asocia con la secuencia sq_id_adjunto_orden_trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdAdjuntoSolicita() As Integer
            Get
                Return vgn_IdAdjuntoSolicita
            End Get
            Set(ByVal value As Integer)
                vgn_IdAdjuntoSolicita = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdTipoDocumentoRespuesta() As Integer
            Get
                Return vgn_IdTipoDocumentoRespuesta
            End Get
            Set(ByVal value As Integer)
                vgn_IdTipoDocumentoRespuesta = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdEtapaOrdenTrabajoRes() As Integer
            Get
                Return vgn_IdEtapaOrdenTrabajoRes
            End Get
            Set(ByVal value As Integer)
                vgn_IdEtapaOrdenTrabajoRes = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otf_adjunto_orden_trabajo que se asocia con la secuencia sq_id_adjunto_orden_trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdAdjuntoRespuesta() As Integer
            Get
                Return vgn_IdAdjuntoRespuesta
            End Get
            Set(ByVal value As Integer)
                vgn_IdAdjuntoRespuesta = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
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
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
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
            vgc_IdOrdenTrabajo = String.Empty
            vgn_NoRequiereMaterial = 0
            vgc_Observaciones = String.Empty
            vgn_IdTipoDocumentoSolicita = 0
            vgn_IdEtapaOrdenTrabajoSol = 0
            vgn_IdAdjuntoSolicita = 0
            vgn_IdTipoDocumentoRespuesta = 0
            vgn_IdEtapaOrdenTrabajoRes = 0
            vgn_IdAdjuntoRespuesta = 0
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
            vgc_HistorialJustificacion = String.Empty
            vgc_EstadoSolMaterial = String.Empty
        End Sub
#End Region
    End Class
End Namespace
