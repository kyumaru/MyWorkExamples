Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
	<Serializable()> _
	Public Class EntOtmSectorTaller
		Inherits EntBase
#Region "Atributos"
        Private vgn_IdUbicacionAdministra As Integer
        Private vgn_NumEmpleadoCoordinador As Double 'Número de empleado del coordinador del sector o taller
        Private vgn_NumEmpleadoSustituto As Double 'Número de empleado del sustituto del coordinador
        Private vgn_IdSectorTaller As Integer 'Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller
        Private vgc_Nombre As String 'Nombre del sector
        Private vgc_TipoArea As String 'Tipo de área - sec: sector, tal: taller -  valor por defecto sec
        Private vgc_Estado As String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
        Private vgc_NombreCoodinador As String
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
        ''' Número de empleado del coordinador del sector o taller
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:29:57 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NumEmpleadoCoordinador() As Double
            Get
                Return vgn_NumEmpleadoCoordinador
            End Get
            Set(ByVal value As Double)
                vgn_NumEmpleadoCoordinador = value
            End Set
        End Property

        ''' <summary>
        ''' Número de empleado del sustituto del coordinador
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:29:57 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NumEmpleadoSustituto() As Double
            Get
                Return vgn_NumEmpleadoSustituto
            End Get
            Set(ByVal value As Double)
                vgn_NumEmpleadoSustituto = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:29:57 a.m.</creationDate>
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
        ''' Nombre del sector
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:29:57 a.m.</creationDate>
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
        ''' Tipo de área - sec: sector, tal: taller -  valor por defecto sec
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:29:57 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property TipoArea() As String
            Get
                Return vgc_TipoArea
            End Get
            Set(ByVal value As String)
                vgc_TipoArea = value
            End Set
        End Property

        ''' <summary>
        ''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:29:57 a.m.</creationDate>
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
        ''' Control de concurrencia - valor por defecto: fecha y hora del sistema
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:29:57 a.m.</creationDate>
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
        ''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:29:57 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NombreCoodinador() As String
            Get
                Return vgc_NombreCoodinador
            End Get
            Set(ByVal value As String)
                vgc_NombreCoodinador = value
            End Set
        End Property
#End Region

#Region "Constructores"
	Public Sub New()
            MyBase.New()
            vgn_IdUbicacionAdministra = 0
		vgn_NumEmpleadoCoordinador = 0
		vgn_NumEmpleadoSustituto = 0
		vgn_IdSectorTaller = 0
		vgc_Nombre = String.Empty
		vgc_TipoArea = String.Empty
		vgc_Estado = String.Empty
            vgd_TimeStamp = DateTime.Now
            vgc_NombreCoodinador = String.Empty
	End Sub
#End Region
	End Class
End Namespace
