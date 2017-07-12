Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
    <Serializable()> _
    Public Class EntOtlTrazabilidadProceso
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdTrazabilidadProceso As Integer 'Llave primaria de la tabla otl_trazabilidad_proceso que se asocia con la secuencia sq_id_trazabilidad_proceso
        Private vgn_IdUbicacion As Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgn_Anno As Integer 'Año de solicitud de la ot
        Private vgn_Consecutivo As Integer 'Consecutivo de orden de trabajo, se reinicia cada año.
        Private vgn_NumEmpleadoEjecuta As Double 'Número de empleado responsable de la ejecución
        Private vgc_EstadoOrdenTrabajo As String 'Estado asignado a la orden de trabajo en el paso realizado
        'Private vgd_FechaHoraRegistro AS DateTime 'Fecha de registro del paso en el proceso
        Private vgd_FechaHoraEjecucion As DateTime 'Fecha de ejecución del paso indicado por el responsable (revisión, liquidación, etc.)
        Private vgc_Observaciones As String 'Observaciones indicadas por el responsable
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Llave primaria de la tabla otl_trazabilidad_proceso que se asocia con la secuencia sq_id_trazabilidad_proceso
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/09/2015 11:16:46 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdTrazabilidadProceso() As Integer
            Get
                Return vgn_IdTrazabilidadProceso
            End Get
            Set(ByVal value As Integer)
                vgn_IdTrazabilidadProceso = value
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
        ''' <creationDate>14/09/2015 11:16:46 a.m.</creationDate>
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
        ''' <creationDate>14/09/2015 11:16:46 a.m.</creationDate>
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
        ''' Número de empleado responsable de la ejecución
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/09/2015 11:16:46 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NumEmpleadoEjecuta() As Double
            Get
                Return vgn_NumEmpleadoEjecuta
            End Get
            Set(ByVal value As Double)
                vgn_NumEmpleadoEjecuta = value
            End Set
        End Property

        ''' <summary>
        ''' Estado asignado a la orden de trabajo en el paso realizado
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/09/2015 11:16:46 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property EstadoOrdenTrabajo() As String
            Get
                Return vgc_EstadoOrdenTrabajo
            End Get
            Set(ByVal value As String)
                vgc_EstadoOrdenTrabajo = value
            End Set
        End Property

        ''' <summary>
        ''' Fecha de ejecución del paso indicado por el responsable (revisión, liquidación, etc.)
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/09/2015 11:16:46 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property FechaHoraEjecucion() As DateTime
            Get
                Return vgd_FechaHoraEjecucion
            End Get
            Set(ByVal value As DateTime)
                vgd_FechaHoraEjecucion = value
            End Set
        End Property

        ''' <summary>
        ''' Observaciones indicadas por el responsable
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/09/2015 11:16:46 a.m.</creationDate>
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
        ''' <creationDate>14/09/2015 11:16:46 a.m.</creationDate>
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
        ''' <creationDate>14/09/2015 11:16:46 a.m.</creationDate>
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
            vgn_IdTrazabilidadProceso = 0
            vgn_IdUbicacion = 0
            vgn_Anno = 0
            vgn_Consecutivo = 0
            vgn_NumEmpleadoEjecuta = 0
            vgc_EstadoOrdenTrabajo = String.Empty
            vgd_FechaHoraEjecucion = DateTime.Now
            vgc_Observaciones = String.Empty
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
    End Class
End Namespace
