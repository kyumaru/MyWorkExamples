Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
    <Serializable()> _
    Public Class EntOtmEtapaViaContrato
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdViaContrato As Integer 'Llave primaria de la tabla otm_via_contrato que se asocia con la secuencia sq_id_via_contrato
        Private vgn_IdEtapaContratacion As Integer 'Llave primaria de la tabla otm_etapa_contratacion que se asocia con la secuencia sq_id_etapa_contratacion
        Private vgc_Estado As String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Llave primaria de la tabla otm_via_contrato que se asocia con la secuencia sq_id_via_contrato
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdViaContrato() As Integer
            Get
                Return vgn_IdViaContrato
            End Get
            Set(ByVal value As Integer)
                vgn_IdViaContrato = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_etapa_contratacion que se asocia con la secuencia sq_id_etapa_contratacion
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdEtapaContratacion() As Integer
            Get
                Return vgn_IdEtapaContratacion
            End Get
            Set(ByVal value As Integer)
                vgn_IdEtapaContratacion = value
            End Set
        End Property

        ''' <summary>
        ''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
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
        ''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
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
        ''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
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
            vgn_IdViaContrato = 0
            vgn_IdEtapaContratacion = 0
            vgc_Estado = String.Empty
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
    End Class
End Namespace
