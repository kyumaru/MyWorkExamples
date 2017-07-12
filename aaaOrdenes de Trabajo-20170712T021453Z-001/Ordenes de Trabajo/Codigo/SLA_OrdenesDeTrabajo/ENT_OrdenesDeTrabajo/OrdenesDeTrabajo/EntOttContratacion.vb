Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
	<Serializable()> _
	Public Class EntOttContratacion
		Inherits EntBase
#Region "Atributos"
        Private vgn_IdEtapaContratacion As Integer 'Llave primaria de la tabla otm_etapa_contratacion que se asocia con la secuencia sq_id_etapa_contratacion
        Private vgc_Observaciones As String 'Observaciones
        Private vgn_IdUbicacion As Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgc_IdOrdenTrabajo As String 'Identificador único alfanumérico de la orden de trabajo
        Private vgn_Version As Integer 'Numero de version del proceso de contratación asociado a una orden de trabajo
        Private vgn_IdViaContrato As Integer 'Llave primaria de la tabla otm_via_contrato que se asocia con la secuencia sq_id_via_contrato
        Private vgn_Editable As Integer 'Indicador para marcar la vesión de la contratación como editables o no editable 0: no editable 1: editable, valor por defecto : 1
        Private vgc_NumeroSolicitud As String 'Número de solicitud registrado en geco
        Private vgc_NumeroDecisionInicial As String 'Número de decisión inicial registrado en geco
        Private vgc_NumeroContrato As String 'Número de contrato. ejemplo: 2015cd-000224-osg
        Private vgc_NombreContrato As String 'Nombre del contrato
        Private vgc_Usuario As String 'Usuario
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Llave primaria de la tabla otm_etapa_contratacion que se asocia con la secuencia sq_id_etapa_contratacion
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
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
        ''' Observaciones
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
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
        ''' Llave primaria de la tabla otm_via_contrato que se asocia con la secuencia sq_id_via_contrato
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
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
        ''' Indicador para marcar la vesión de la contratación como editables o no editable 0: no editable 1: editable, valor por defecto : 1
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Editable() As Integer
            Get
                Return vgn_Editable
            End Get
            Set(ByVal value As Integer)
                vgn_Editable = value
            End Set
        End Property

        ''' <summary>
        ''' Número de solicitud registrado en geco
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NumeroSolicitud() As String
            Get
                Return vgc_NumeroSolicitud
            End Get
            Set(ByVal value As String)
                vgc_NumeroSolicitud = value
            End Set
        End Property

        ''' <summary>
        ''' Número de decisión inicial registrado en geco
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NumeroDecisionInicial() As String
            Get
                Return vgc_NumeroDecisionInicial
            End Get
            Set(ByVal value As String)
                vgc_NumeroDecisionInicial = value
            End Set
        End Property

        ''' <summary>
        ''' Número de contrato. ejemplo: 2015cd-000224-osg
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NumeroContrato() As String
            Get
                Return vgc_NumeroContrato
            End Get
            Set(ByVal value As String)
                vgc_NumeroContrato = value
            End Set
        End Property

        ''' <summary>
        ''' Nombre del contrato
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/07/2016 04:50:30 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NombreContrato() As String
            Get
                Return vgc_NombreContrato
            End Get
            Set(ByVal value As String)
                vgc_NombreContrato = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario
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
            vgn_IdEtapaContratacion = 0
            vgc_Observaciones = String.Empty
            vgn_IdUbicacion = 0
            vgc_IdOrdenTrabajo = String.Empty
            vgn_Version = 0
            vgn_IdViaContrato = 0
            vgn_Editable = 0
            vgc_NumeroSolicitud = String.Empty
            vgc_NumeroDecisionInicial = String.Empty
            vgc_NumeroContrato = String.Empty
            vgc_NombreContrato = String.Empty
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
	End Class
End Namespace
