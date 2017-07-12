Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
    <Serializable()> _
    Public Class EntOtfExcepcionPeriodo
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdExcepcionPeriodo As Integer 'Identificador para las excepciones registradas en el sistema de la tabla otf_excepcion_periodo, que se asocia con la secuencia sq_id_excepcion_periodo
        Private vgn_NumEmpleado As Double 'Número de emplerado al cual se le habilita la excepción
        Private vgn_IdUnidadTiempo As Integer 'Llave primaria de la tabla otm_unidad_tiempo que se asocia con la secuencia sq_id_unidad_tiempo
        Private vgn_Vigencia As Integer 'Cantidad de unidades de tiempo que estara vigente la excecpión
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro
        Private vgc_UnidadInterna As String 'unidad interna
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Identificador para las excepciones registradas en el sistema de la tabla otf_excepcion_periodo, que se asocia con la secuencia sq_id_excepcion_periodo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/01/2016 02:03:50 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdExcepcionPeriodo() As Integer
            Get
                Return vgn_IdExcepcionPeriodo
            End Get
            Set(ByVal value As Integer)
                vgn_IdExcepcionPeriodo = value
            End Set
        End Property

        ''' <summary>
        ''' Número de emplerado al cual se le habilita la excepción
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/01/2016 02:03:50 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NumEmpleado() As Double
            Get
                Return vgn_NumEmpleado
            End Get
            Set(ByVal value As Double)
                vgn_NumEmpleado = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_unidad_tiempo que se asocia con la secuencia sq_id_unidad_tiempo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/01/2016 02:03:50 p.m.</creationDate>
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
        ''' Cantidad de unidades de tiempo que estara vigente la excecpión
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/01/2016 02:03:50 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Vigencia() As Integer
            Get
                Return vgn_Vigencia
            End Get
            Set(ByVal value As Integer)
                vgn_Vigencia = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que crea o modifica el registro
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/01/2016 02:03:50 p.m.</creationDate>
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
        ''' Unidad interna
        ''' </summary>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>20/01/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Property UnidadInterna() As String
            Get
                Return vgc_UnidadInterna
            End Get
            Set(ByVal value As String)
                vgc_UnidadInterna = value
            End Set
        End Property

        ''' <summary>
        ''' Control de concurrencia - valor por defecto: fecha y hora del sistema
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/01/2016 02:03:50 p.m.</creationDate>
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
            vgn_IdExcepcionPeriodo = 0
            vgn_NumEmpleado = 0
            vgn_IdUnidadTiempo = 0
            vgn_Vigencia = 0
            vgc_Usuario = String.Empty
            vgc_UnidadInterna = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
    End Class
End Namespace
