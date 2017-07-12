Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.Catalogos
    <Serializable()> _
    Public Class EntOtmCategoriaServicio
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdUbicacionAdministra As Integer
        Private vgn_RequiereFichaTecnica As Integer 'Indicador de si requiere ficha técnica - 0: no, 1: si - valor por defecto: 0
        Private vgn_OcultarCategoria As Integer 'Indicador para mostrar o ocultar la categoria a los usuarios externos de la aplicación. - 0: no, 1: si - valor por defecto: 0
        Private vgn_IdCategoriaServicio As Integer 'Llave primaria de la tabla otm_categoria_servicio que se asocia con la secuencia sq_id_categoria_servicio
        Private vgn_NumEmpleadoSupervisor As Integer 'Número de empleado supervisor para las boletas de la categoría indicada
        Private vgn_IdSectorTaller As Integer 'Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller
        Private vgc_Descripcion As String 'Descripción de la categoría de servicio
        Private vgc_Estado As String 'Estado del registro - act: activo, ina: inactivo - valor por defecto: act
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
        Private vgc_Siglas As String

#End Region
#Region "Propiedades"

        ''' <summary>
        ''' siglas de caetgoria
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>02/09/2015 03:55:24 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Siglas() As String
            Get
                Return vgc_Siglas
            End Get
            Set(ByVal value As String)
                vgc_Siglas = value
            End Set
        End Property

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
        ''' Indicador de si requiere ficha técnica - 0: no, 1: si - valor por defecto: 0
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>02/09/2015 03:55:24 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property RequiereFichaTecnica() As Integer
            Get
                Return vgn_RequiereFichaTecnica
            End Get
            Set(ByVal value As Integer)
                vgn_RequiereFichaTecnica = value
            End Set
        End Property

        ''' <summary>
        ''' Indicador para mostrar o ocultar la categoria a los usuarios externos de la aplicación. - 0: no, 1: si - valor por defecto: 0
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>15/01/2016 11:15:03 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property OcultarCategoria() As Integer
            Get
                Return vgn_OcultarCategoria
            End Get
            Set(ByVal value As Integer)
                vgn_OcultarCategoria = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_categoria_servicio que se asocia con la secuencia sq_id_categoria_servicio
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>02/09/2015 03:55:24 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdCategoriaServicio() As Integer
            Get
                Return vgn_IdCategoriaServicio
            End Get
            Set(ByVal value As Integer)
                vgn_IdCategoriaServicio = value
            End Set
        End Property

        ''' <summary>
        ''' Número de empleado supervisor para las boletas de la categoría indicada
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>02/09/2015 03:55:24 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NumEmpleadoSupervisor() As Integer
            Get
                Return vgn_NumEmpleadoSupervisor
            End Get
            Set(ByVal value As Integer)
                vgn_NumEmpleadoSupervisor = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>02/09/2015 03:55:24 p.m.</creationDate>
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
        ''' Descripción de la categoría de servicio
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>02/09/2015 03:55:24 p.m.</creationDate>
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
        ''' Estado del registro - act: activo, ina: inactivo - valor por defecto: act
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>02/09/2015 03:55:24 p.m.</creationDate>
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
        ''' <creationDate>02/09/2015 03:55:24 p.m.</creationDate>
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
            vgn_RequiereFichaTecnica = 0
            vgn_IdCategoriaServicio = 0
            vgn_NumEmpleadoSupervisor = 0
            vgn_IdSectorTaller = 0
            vgc_Descripcion = String.Empty
            vgc_Siglas = String.Empty
            vgc_Estado = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
    End Class
End Namespace
