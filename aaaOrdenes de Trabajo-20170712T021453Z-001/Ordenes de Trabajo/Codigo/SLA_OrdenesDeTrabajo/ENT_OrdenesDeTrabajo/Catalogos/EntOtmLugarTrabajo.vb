Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
    <Serializable()> _
    Public Class EntOtmLugarTrabajo
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdUbicacionAdministra As Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgc_Estado As String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
        Private vgn_IdLugarTrabajo As Integer 'Llave primaria de la tabla otm_lugar_trabajo que se asocia con la secuencia sq_id_lugar_trabajo
        Private vgn_IdUbicacionPertenece As Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgn_IdSectorTaller As Integer 'Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller
        Private vgn_IdTipoLugarUbicacion As Integer 'Llave primaria de la tabla otm_tipo_lugar_ubicacion que se asocia con la secuencia sq_id_tipo_lugar_ubicacion
        Private vgc_Nombre As String 'Nombre del lugar de trabajo
        Private vgc_Clasificacion As String 'Clasificación del lugar de trabajo - edi: edificio, sit: sitio - valor por defecto: edi
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdUbicacionAdministra() As Integer
            Get
                Return vgn_IdUbicacionAdministra
            End Get
            Set(ByVal value As Integer)
                vgn_IdUbicacionAdministra = value
            End Set
        End Property

        ''' <summary>
        ''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
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
        ''' Llave primaria de la tabla otm_lugar_trabajo que se asocia con la secuencia sq_id_lugar_trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
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
        ''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdUbicacionPertenece() As Integer
            Get
                Return vgn_IdUbicacionPertenece
            End Get
            Set(ByVal value As Integer)
                vgn_IdUbicacionPertenece = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdSectorTaller() As Integer
            Get
                Return vgn_IdSectorTaller
            End Get
            Set(ByVal value As Integer)
                vgn_IdSectorTaller = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_tipo_lugar_ubicacion que se asocia con la secuencia sq_id_tipo_lugar_ubicacion
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdTipoLugarUbicacion() As Integer
            Get
                Return vgn_IdTipoLugarUbicacion
            End Get
            Set(ByVal value As Integer)
                vgn_IdTipoLugarUbicacion = value
            End Set
        End Property

        ''' <summary>
        ''' Nombre del lugar de trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Nombre() As String
            Get
                Return vgc_Nombre
            End Get
            Set(ByVal value As String)
                vgc_Nombre = value
            End Set
        End Property

        ''' <summary>
        ''' Clasificación del lugar de trabajo - edi: edificio, sit: sitio - valor por defecto: edi
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Clasificacion() As String
            Get
                Return vgc_Clasificacion
            End Get
            Set(ByVal value As String)
                vgc_Clasificacion = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
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
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
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
            vgn_IdUbicacionAdministra = 0
            vgc_Estado = String.Empty
            vgn_IdLugarTrabajo = 0
            vgn_IdUbicacionPertenece = 0
            vgn_IdSectorTaller = 0
            vgn_IdTipoLugarUbicacion = 0
            vgc_Nombre = String.Empty
            vgc_Clasificacion = String.Empty
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
    End Class
End Namespace
