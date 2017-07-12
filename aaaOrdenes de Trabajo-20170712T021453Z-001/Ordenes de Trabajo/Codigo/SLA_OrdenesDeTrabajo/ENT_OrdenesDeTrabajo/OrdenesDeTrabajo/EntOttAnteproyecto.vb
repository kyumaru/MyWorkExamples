Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
    <Serializable()> _
    Public Class EntOttAnteproyecto
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdUnidadTiempo As Integer 'Llave primaria de la tabla otm_unidad_tiempo que se asocia con la secuencia sq_id_unidad_tiempo
        Private vgn_IdUbicacion As Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgc_IdOrdenTrabajo As String 'Identificador único alfanumérico de la orden de trabajo
        Private vgn_Version As Integer 'Numero de version del anteproyecto asociado a una orden de trabajo
        Private vgn_Editable As Integer 'Indicador para marcar las vesión del ante proyecto como editables o no editables 0: no editable 1: editable, valor por defecto : 1
        Private vgc_Descripcion As String 'Descripción del trabajo para el cual se elebora el ante proyecto.
        Private vgc_ActividadesContempladas As String 'Listad de actividades contempladas en el ante proyecto, son una guia para generar ordenes de trabajo hijas.
        Private vgn_Cantidad As Double 'Cantidad de metros o metros cuadrados que abarcara el proyecto. 
        Private vgc_UnidadMedida As String 'Indicador para metros, metros cuadrados o metros cubicos valores : mts, mt2, mt3
        Private vgn_AvalPlantaFisica As Integer 'Indicador para señalar si el proyecto requiere del aval de planta fisica
        Private vgn_AvalForesta As Integer 'Indicador para señalar si el proyecto requiere del aval de foresta
        Private vgn_TiempoRespuesta As Integer 'Indicador para la cantidad de tiempo del que dispone el solicitante para responder sobre el informe de viabilidad tecnica del proyecto
        Private vgd_FechaEnvia As DateTime 'Fecha en la que le es enviado al usuario solicitante el anteproyecto para su revisión y aprobación.
        Private vgd_FechaResponde As DateTime 'Fecha en la que el usuario solicitante da respuesta a la revisión del ante proyecto
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
#End Region
#Region "Propiedades"
        ''' <summary>
        ''' Llave primaria de la tabla otm_unidad_tiempo que se asocia con la secuencia sq_id_unidad_tiempo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
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
        ''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
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
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
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
        ''' Numero de version del anteproyecto asociado a una orden de trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
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
        ''' Indicador para marcar las vesión del ante proyecto como editables o no editables 0: no editable 1: editable, valor por defecto : 1
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
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
        ''' Descripción del trabajo para el cual se elebora el ante proyecto.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
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
        ''' Listad de actividades contempladas en el ante proyecto, son una guia para generar ordenes de trabajo hijas.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property ActividadesContempladas() As String
            Get
                Return vgc_ActividadesContempladas
            End Get
            Set(ByVal value As String)
                vgc_ActividadesContempladas = value
            End Set
        End Property

        ''' <summary>
        ''' Cantidad de metros o metros cuadrados que abarcara el proyecto. 
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Cantidad() As Double
            Get
                Return vgn_Cantidad
            End Get
            Set(ByVal value As Double)
                vgn_Cantidad = value
            End Set
        End Property

        ''' <summary>
        ''' Indicador para metros, metros cuadrados o metros cubicos valores : mts, mt2, mt3
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property UnidadMedida() As String
            Get
                Return vgc_UnidadMedida
            End Get
            Set(ByVal value As String)
                vgc_UnidadMedida = value
            End Set
        End Property

        ''' <summary>
        ''' Indicador para señalar si el proyecto requiere del aval de planta fisica
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property AvalPlantaFisica() As Integer
            Get
                Return vgn_AvalPlantaFisica
            End Get
            Set(ByVal value As Integer)
                vgn_AvalPlantaFisica = value
            End Set
        End Property

        ''' <summary>
        ''' Indicador para señalar si el proyecto requiere del aval de foresta
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property AvalForesta() As Integer
            Get
                Return vgn_AvalForesta
            End Get
            Set(ByVal value As Integer)
                vgn_AvalForesta = value
            End Set
        End Property

        ''' <summary>
        ''' Indicador para la cantidad de tiempo del que dispone el solicitante para responder sobre el informe de viabilidad tecnica del proyecto
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property TiempoRespuesta() As Integer
            Get
                Return vgn_TiempoRespuesta
            End Get
            Set(ByVal value As Integer)
                vgn_TiempoRespuesta = value
            End Set
        End Property

        ''' <summary>
        ''' Fecha en la que le es enviado al usuario solicitante el anteproyecto para su revisión y aprobación.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property FechaEnvia() As DateTime
            Get
                Return vgd_FechaEnvia
            End Get
            Set(ByVal value As DateTime)
                vgd_FechaEnvia = value
            End Set
        End Property

        ''' <summary>
        ''' Fecha en la que el usuario solicitante da respuesta a la revisión del ante proyecto
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property FechaResponde() As DateTime
            Get
                Return vgd_FechaResponde
            End Get
            Set(ByVal value As DateTime)
                vgd_FechaResponde = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
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
        ''' <creationDate>07/03/2016 11:51:28 a.m.</creationDate>
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
            vgn_IdUnidadTiempo = 0
            vgn_IdUbicacion = 0
            vgc_IdOrdenTrabajo = String.Empty
            vgn_Version = 0
            vgn_Editable = 0
            vgc_Descripcion = String.Empty
            vgc_ActividadesContempladas = String.Empty
            vgn_Cantidad = 0
            vgc_UnidadMedida = String.Empty
            vgn_AvalPlantaFisica = 0
            vgn_AvalForesta = 0
            vgn_TiempoRespuesta = 0
            vgd_FechaEnvia = DateTime.Now
            vgd_FechaResponde = DateTime.Now
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
    End Class
End Namespace
