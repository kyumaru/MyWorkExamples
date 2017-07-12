Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
    <Serializable()> _
    Public Class EntOttViabilidadTecnica
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdUbicacion As Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgc_IdOrdenTrabajo As String 'Identificador único alfanumérico de la orden de trabajo
        Private vgn_IdUnidadTiempo As Integer 'Llave primaria de la tabla otm_unidad_tiempo que se asocia con la secuencia sq_id_unidad_tiempo
        Private vgn_TiempoRespuesta As Integer 'Indicador para la cantidad de tiempo del que dispone el solicitante para responder sobre el informe de viabilidad tecnica del proyecto
        Private vgn_Viabilidad As Integer 'Indicador para la viabilidad del proyecto 0: no 1:si
        Private vgn_EstimacionPresupuestaria As Double 'Estimación presupuestaria para cubrir los gastos del proyecto.
        Private vgc_Detalle As String 'Texto enriquesido con el detalle de la evaluación de viabilidad tecnica. 
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/02/2016 03:07:35 p.m.</creationDate>
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
        ''' <creationDate>18/02/2016 03:07:35 p.m.</creationDate>
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
        ''' Llave primaria de la tabla otm_unidad_tiempo que se asocia con la secuencia sq_id_unidad_tiempo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/02/2016 03:07:35 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdUnidadTiempo() As Integer
            Get
                Return vgn_IdUnidadTiempo
            End Get
            Set(ByVal value As Integer)
                vgn_IdUnidadTiempo = value
            End Set
        End Property

        ''' <summary>
        ''' Indicador para la cantidad de tiempo del que dispone el solicitante para responder sobre el informe de viabilidad tecnica del proyecto
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/02/2016 03:07:35 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property TiempoRespuesta() As Integer
            Get
                Return vgn_TiempoRespuesta
            End Get
            Set(ByVal value As Integer)
                vgn_TiempoRespuesta = value
            End Set
        End Property

        ''' <summary>
        ''' Indicador para la viabilidad del proyecto 0: no 1:si
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/02/2016 03:07:35 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Viabilidad() As Integer
            Get
                Return vgn_Viabilidad
            End Get
            Set(ByVal value As Integer)
                vgn_Viabilidad = value
            End Set
        End Property

        ''' <summary>
        ''' Estimación presupuestaria para cubrir los gastos del proyecto.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/02/2016 03:07:35 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property EstimacionPresupuestaria() As Double
            Get
                Return vgn_EstimacionPresupuestaria
            End Get
            Set(ByVal value As Double)
                vgn_EstimacionPresupuestaria = value
            End Set
        End Property

        ''' <summary>
        ''' Texto enriquesido con el detalle de la evaluación de viabilidad tecnica. 
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/02/2016 03:07:35 p.m.</creationDate>
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
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/02/2016 03:07:35 p.m.</creationDate>
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
        ''' <creationDate>18/02/2016 03:07:35 p.m.</creationDate>
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
            vgn_IdUnidadTiempo = 0
            vgn_TiempoRespuesta = 0
            vgn_Viabilidad = 0
            vgn_EstimacionPresupuestaria = 0
            vgc_Detalle = String.Empty
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
    End Class
End Namespace
