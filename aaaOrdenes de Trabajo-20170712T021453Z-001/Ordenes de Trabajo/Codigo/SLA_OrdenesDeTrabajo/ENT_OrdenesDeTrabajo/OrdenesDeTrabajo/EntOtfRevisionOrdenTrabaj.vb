Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
    <Serializable()> _
    Public Class EntOtfRevisionOrdenTrabaj
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdRevisionOrdenTrabaj As Integer 'Llave primaria de la tabla otf_revision_orden_trabaj que se asocia con la secuencia sq_id_revision_orden_trabaj
        Private vgn_IdUbicacion As Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgn_Anno As Integer 'Año de solicitud de la ot
        Private vgn_Consecutivo As Integer 'Consecutivo de orden de trabajo, se reinicia cada año.
        Private vgc_Observaciones As String 'Observaciones indicadas por el revisor
        Private vgc_Usuario As String 'Usuario que realiza la revisión
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
        Private vgc_Estado As String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Llave primaria de la tabla otf_revision_orden_trabaj que se asocia con la secuencia sq_id_revision_orden_trabaj
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:25:27 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdRevisionOrdenTrabaj() As Integer
            Get
                Return vgn_IdRevisionOrdenTrabaj
            End Get
            Set(ByVal value As Integer)
                vgn_IdRevisionOrdenTrabaj = value
            End Set
        End Property

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
        ''' <creationDate>31/08/2015 08:25:27 a.m.</creationDate>
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
        ''' <creationDate>31/08/2015 08:25:27 a.m.</creationDate>
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
        ''' Observaciones indicadas por el revisor
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:25:27 a.m.</creationDate>
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
        ''' Usuario que realiza la revisión
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:25:27 a.m.</creationDate>
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
        ''' <creationDate>31/08/2015 08:25:27 a.m.</creationDate>
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
        ''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>31/08/2015 08:25:27 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Estado() As String
            Get
                Return vgc_Estado
            End Get
            Set(ByVal value As String)
                vgc_Estado = value
            End Set
        End Property

#End Region

#Region "Constructores"
        Public Sub New()
            MyBase.New()
            vgn_IdRevisionOrdenTrabaj = 0
            vgn_IdUbicacion = 0
            vgn_Anno = 0
            vgn_Consecutivo = 0
            vgc_Observaciones = String.Empty
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
            vgc_Estado = String.Empty
        End Sub
#End Region
    End Class
End Namespace
