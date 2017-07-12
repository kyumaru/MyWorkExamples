Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttLineaAdjudicacion
		Inherits EntBase
#Region "Atributos"
	Private vgn_IdUbicacion AS Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgc_IdOrdenTrabajo As String 'Identificador único alfanumérico de la orden de trabajo
        Private vgn_Version As Integer 'Numero de version del proceso de contratación asociado a una orden de trabajo
        Private vgn_NumeroLinea As Integer 'Número de línea adjudicada
        Private vgn_MontoAdjudicado As Double 'Monto adjudicado en la línea
        Private vgc_Adjudicatario As String 'Nombre del adjudicatario de la obra
        Private vgd_FechaInicioObra As DateTime 'Fecha de inicio de la obra
        Private vgn_PlazoEnDias As Integer 'Plazo en días para finalización de la obra
        Private vgc_FormaCalculoDias As String 'Forma de cálculo para fijación de la fecha de finalización. nat - días naturales, hab - días hábiles. valor por defecto: nat
        Private vgd_FechaFinEstimada As DateTime 'Fecha calculada por el sistema para finalización de la obra. se puede ver afectada por días de cierre institucional.
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
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
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
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
        ''' Numero de version del proceso de contratación asociado a una orden de trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Version() As Integer
            Get
                Return vgn_Version
            End Get
            Set(ByVal value As Integer)
                vgn_Version = value
            End Set
        End Property

        ''' <summary>
        ''' Número de línea adjudicada
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NumeroLinea() As Integer
            Get
                Return vgn_NumeroLinea
            End Get
            Set(ByVal value As Integer)
                vgn_NumeroLinea = value
            End Set
        End Property

        ''' <summary>
        ''' Monto adjudicado en la línea
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property MontoAdjudicado() As Double
            Get
                Return vgn_MontoAdjudicado
            End Get
            Set(ByVal value As Double)
                vgn_MontoAdjudicado = value
            End Set
        End Property

        ''' <summary>
        ''' Nombre del adjudicatario de la obra
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Adjudicatario() As String
            Get
                Return vgc_Adjudicatario
            End Get
            Set(ByVal value As String)
                vgc_Adjudicatario = value
            End Set
        End Property

        ''' <summary>
        ''' Fecha de inicio de la obra
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property FechaInicioObra() As DateTime
            Get
                Return vgd_FechaInicioObra
            End Get
            Set(ByVal value As DateTime)
                vgd_FechaInicioObra = value
            End Set
        End Property

        ''' <summary>
        ''' Plazo en días para finalización de la obra
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property PlazoEnDias() As Integer
            Get
                Return vgn_PlazoEnDias
            End Get
            Set(ByVal value As Integer)
                vgn_PlazoEnDias = value
            End Set
        End Property

        ''' <summary>
        ''' Forma de cálculo para fijación de la fecha de finalización. nat - días naturales, hab - días hábiles. valor por defecto: nat
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property FormaCalculoDias() As String
            Get
                Return vgc_FormaCalculoDias
            End Get
            Set(ByVal value As String)
                vgc_FormaCalculoDias = value
            End Set
        End Property

        ''' <summary>
        ''' Fecha calculada por el sistema para finalización de la obra. se puede ver afectada por días de cierre institucional.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property FechaFinEstimada() As DateTime
            Get
                Return vgd_FechaFinEstimada
            End Get
            Set(ByVal value As DateTime)
                vgd_FechaFinEstimada = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
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
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
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
            vgn_Version = 0
            vgn_NumeroLinea = 0
            vgn_MontoAdjudicado = 0
            vgc_Adjudicatario = String.Empty
            vgd_FechaInicioObra = DateTime.Now
            vgn_PlazoEnDias = 0
            vgc_FormaCalculoDias = String.Empty
            vgd_FechaFinEstimada = DateTime.Now
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
	End Class
End Namespace
