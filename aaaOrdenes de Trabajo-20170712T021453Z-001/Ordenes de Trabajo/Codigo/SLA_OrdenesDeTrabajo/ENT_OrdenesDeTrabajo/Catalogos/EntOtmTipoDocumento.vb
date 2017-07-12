Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
    <Serializable()> _
    Public Class EntOtmTipoDocumento
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdTipoDocumento As Integer 'Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento
        Private vgc_Descripcion As String 'Descripción del tipo de documento, debe ser unica, por ejemplo; fotos, planos, oficio, reporte 
        Private vgn_TamanioMaximo As Integer 'Tamaño maximo del archivo cargado por el usuario asociado al tipo de documento
        Private vgc_FormatosAdmitidos As String 'Formatos admintidos para el tipo de documento
        Private vgc_Estado As String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
        Private vgn_Protegido As Integer '
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Llave primaria de la tabla otm_tipo_documento que se asocia con la secuencia sq_id_tipo_documento
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/02/2016 11:08:50 a.m.</creationDate>
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
        ''' Descripción del tipo de documento, debe ser unica, por ejemplo; fotos, planos, oficio, reporte 
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/02/2016 11:08:50 a.m.</creationDate>
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
        ''' Tamaño maximo del archivo cargado por el usuario asociado al tipo de documento
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/02/2016 11:08:50 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property TamanioMaximo() As Integer
            Get
                Return vgn_TamanioMaximo
            End Get
            Set(ByVal value As Integer)
                vgn_TamanioMaximo = value
            End Set
        End Property

        ''' <summary>
        ''' Formatos admintidos para el tipo de documento
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/02/2016 11:08:50 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property FormatosAdmitidos() As String
            Get
                Return vgc_FormatosAdmitidos
            End Get
            Set(ByVal value As String)
                vgc_FormatosAdmitidos = value
            End Set
        End Property

        ''' <summary>
        ''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/02/2016 11:08:50 a.m.</creationDate>
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
        ''' <creationDate>28/02/2016 11:08:50 a.m.</creationDate>
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
        ''' <creationDate>28/02/2016 11:08:50 a.m.</creationDate>
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
        ''' 
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/02/2016 11:08:50 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Protegido() As Integer
            Get
                Return vgn_Protegido
            End Get
            Set(ByVal value As Integer)
                vgn_Protegido = value
            End Set
        End Property

#End Region

#Region "Constructores"
        Public Sub New()
            MyBase.New()
            vgn_IdTipoDocumento = 0
            vgc_Descripcion = String.Empty
            vgn_TamanioMaximo = 0
            vgc_FormatosAdmitidos = String.Empty
            vgc_Estado = String.Empty
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
            vgn_Protegido = 0
        End Sub
#End Region
    End Class
End Namespace
