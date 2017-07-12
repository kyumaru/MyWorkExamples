Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
    <Serializable()> _
    Public Class EntOttDetalleRetiro
        Inherits EntBase
#Region "Atributos"
        Private vgn_Anno As Integer 'Llave primaria de la tabla ott_solicitud_retiro
        Private vgn_IdSolicitudRetiro As Integer 'Llave primaria de la tabla ott_solicitud_retiro
        Private vgn_IdDetalleMaterial As Integer 'Llave primaria de la tabla ott_detalle_material que se asocia con la secuencia sq_id_detalle_material
        Private vgn_CantidadSolicitada As Double 'Cantidad de material a retirar
        Private vgn_CantidadRetirada As Double 'Cantidad de material efectivamente retirada
        Private vgn_CostoCalculado As Double 'Costo calculado con base en cantidad y costo promedio al momento del retiro
        Private vgc_Estado As String 'Estado de la solicitud: pen - pendiente, ret - retirada
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Llave primaria de la tabla ott_solicitud_retiro
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 02:32:03 p.m.</creationDate>
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
        ''' <creationDate>21/06/2016 02:32:03 p.m.</creationDate>
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
        ''' Llave primaria de la tabla ott_detalle_material que se asocia con la secuencia sq_id_detalle_material
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 02:32:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdDetalleMaterial() As Integer
            Get
                Return vgn_IdDetalleMaterial
            End Get
            Set(ByVal value As Integer)
                vgn_IdDetalleMaterial = value
            End Set
        End Property

        ''' <summary>
        ''' Cantidad de material a retirar
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 02:32:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property CantidadSolicitada() As Double
            Get
                Return vgn_CantidadSolicitada
            End Get
            Set(ByVal value As Double)
                vgn_CantidadSolicitada = value
            End Set
        End Property

        ''' <summary>
        ''' Cantidad de material efectivamente retirada
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 02:32:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property CantidadRetirada() As Double
            Get
                Return vgn_CantidadRetirada
            End Get
            Set(ByVal value As Double)
                vgn_CantidadRetirada = value
            End Set
        End Property

        ''' <summary>
        ''' Costo calculado con base en cantidad y costo promedio al momento del retiro
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 02:32:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property CostoCalculado() As Double
            Get
                Return vgn_CostoCalculado
            End Get
            Set(ByVal value As Double)
                vgn_CostoCalculado = value
            End Set
        End Property

        ''' <summary>
        ''' Estado de la solicitud: pen - pendiente, ret - retirada
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 02:32:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Estado() As String
            Get
                Return vgc_Estado
            End Get
            Set(ByVal value As String)
                vgc_Estado = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 02:32:03 p.m.</creationDate>
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
        ''' <creationDate>21/06/2016 02:32:03 p.m.</creationDate>
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
            vgn_IdDetalleMaterial = 0
            vgn_CantidadSolicitada = 0
            vgn_CantidadRetirada = 0
            vgn_CostoCalculado = 0
            vgc_Estado = String.Empty
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
    End Class
End Namespace
