Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
    <Serializable()> _
    Public Class EntOtmUnidadEncargada
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdLugarTrabajo As Integer 'Llave primaria de la tabla otm_lugar_trabajo que se asocia con la secuencia sq_id_lugar_trabajo
        Private vgn_CodUnidadSirh As Integer 'Código de la unidad académica o administrativa que administra el edificio o sitio y ademas es responsable de la autorización de la orden de trabajo
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro. 
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Llave primaria de la tabla otm_lugar_trabajo que se asocia con la secuencia sq_id_lugar_trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/03/2016 12:10:33 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdLugarTrabajo() As Integer
            Get
                Return vgn_IdLugarTrabajo
            End Get
            Set(ByVal value As Integer)
                vgn_IdLugarTrabajo = value
            End Set
        End Property

        ''' <summary>
        ''' Código de la unidad académica o administrativa que administra el edificio o sitio y ademas es responsable de la autorización de la orden de trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/03/2016 12:10:33 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property CodUnidadSirh() As Integer
            Get
                Return vgn_CodUnidadSirh
            End Get
            Set(ByVal value As Integer)
                vgn_CodUnidadSirh = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que crea o modifica el registro. 
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/03/2016 12:10:33 p.m.</creationDate>
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
        ''' <creationDate>14/03/2016 12:10:33 p.m.</creationDate>
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
            vgn_IdLugarTrabajo = 0
            vgn_CodUnidadSirh = 0
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
    End Class
End Namespace
