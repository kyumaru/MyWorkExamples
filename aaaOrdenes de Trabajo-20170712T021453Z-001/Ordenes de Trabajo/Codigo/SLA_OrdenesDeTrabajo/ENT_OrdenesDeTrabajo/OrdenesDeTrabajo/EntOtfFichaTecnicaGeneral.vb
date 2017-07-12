Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
    <Serializable()> _
    Public Class EntOtfFichaTecnicaGeneral
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdUbicacion As Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgn_IdPreOrdenTrabajo As Integer 'Llave primaria de la tabla otf_pre_orden_trabajo que se asocia con la secuencia sq_id_pre_orden_trabajo
        Private vgn_ConservaMobiliario As Integer 'Indicador de si conserva mobiliario - 0:no, 1: sí, valor defecto: 0:no -
        Private vgn_RequiereNuevoMobiliario As Integer 'Indicador de si requiere nuevo mobiliario - 0:no, 1: sí, valor defecto: 0:no -
        Private vgc_OtrosMobiliario As String 'Otros requerimientos en relación con el mobiliario solicitado
        Private vgc_OtroTipoRequerimiento As String 'Detalle de otros tipos de requerimientos
        Private vgc_NombreArchivo As String 'Nombre del archivo adjunto
        Private vgo_Archivo As Byte() 'Documento adjunto correspondiente a una lista del equipo que posea la unidad, su estado actual y el equipo que se proyecte adquirir a corto plazo
        Private vgn_CuentaConAlarma As Integer 'Indicador de si cuenta con alarma - 0:no, 1: sí, valor defecto: 0:no -
        Private vgn_RequiereAlarma As Integer 'Indicador de si requiere alarma - 0:no, 1: sí, valor defecto: 0:no -
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgn_CuentaConPresupuesto As Integer
#End Region
#Region "Propiedades"

        Public Property CuentaConPresupuesto() As Integer
            Get
                Return vgn_CuentaConPresupuesto
            End Get
            Set(ByVal value As Integer)
                vgn_CuentaConPresupuesto = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
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
        ''' Llave primaria de la tabla otf_pre_orden_trabajo que se asocia con la secuencia sq_id_pre_orden_trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdPreOrdenTrabajo() As Integer
            Get
                Return vgn_IdPreOrdenTrabajo
            End Get
            Set(ByVal value As Integer)
                vgn_IdPreOrdenTrabajo = value
            End Set
        End Property

        ''' <summary>
        ''' Indicador de si conserva mobiliario - 0:no, 1: sí, valor defecto: 0:no -
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property ConservaMobiliario() As Integer
            Get
                Return vgn_ConservaMobiliario
            End Get
            Set(ByVal value As Integer)
                vgn_ConservaMobiliario = value
            End Set
        End Property

        ''' <summary>
        ''' Indicador de si requiere nuevo mobiliario - 0:no, 1: sí, valor defecto: 0:no -
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property RequiereNuevoMobiliario() As Integer
            Get
                Return vgn_RequiereNuevoMobiliario
            End Get
            Set(ByVal value As Integer)
                vgn_RequiereNuevoMobiliario = value
            End Set
        End Property

        ''' <summary>
        ''' Otros requerimientos en relación con el mobiliario solicitado
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property OtrosMobiliario() As String
            Get
                Return vgc_OtrosMobiliario
            End Get
            Set(ByVal value As String)
                vgc_OtrosMobiliario = value
            End Set
        End Property

        ''' <summary>
        ''' Detalle de otros tipos de requerimientos
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property OtroTipoRequerimiento() As String
            Get
                Return vgc_OtroTipoRequerimiento
            End Get
            Set(ByVal value As String)
                vgc_OtroTipoRequerimiento = value
            End Set
        End Property

        ''' <summary>
        ''' Nombre del archivo adjunto
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NombreArchivo() As String
            Get
                Return vgc_NombreArchivo
            End Get
            Set(ByVal value As String)
                vgc_NombreArchivo = value
            End Set
        End Property

        ''' <summary>
        ''' Documento adjunto correspondiente a una lista del equipo que posea la unidad, su estado actual y el equipo que se proyecte adquirir a corto plazo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Archivo() As Byte()
            Get
                Return vgo_Archivo
            End Get
            Set(ByVal value As Byte())
                vgo_Archivo = value
            End Set
        End Property

        ''' <summary>
        ''' Indicador de si cuenta con alarma - 0:no, 1: sí, valor defecto: 0:no -
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property CuentaConAlarma() As Integer
            Get
                Return vgn_CuentaConAlarma
            End Get
            Set(ByVal value As Integer)
                vgn_CuentaConAlarma = value
            End Set
        End Property

        ''' <summary>
        ''' Indicador de si requiere alarma - 0:no, 1: sí, valor defecto: 0:no -
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property RequiereAlarma() As Integer
            Get
                Return vgn_RequiereAlarma
            End Get
            Set(ByVal value As Integer)
                vgn_RequiereAlarma = value
            End Set
        End Property

        ''' <summary>
        ''' Control de concurrencia - valor por defecto: fecha y hora del sistema
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
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
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:20:07 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Usuario() As String
            Get
                Return vgc_Usuario
            End Get
            Set(ByVal value As String)
                vgc_Usuario = value
            End Set
        End Property

#End Region

#Region "Constructores"
        Public Sub New()
            MyBase.New()
            vgn_IdUbicacion = 0
            vgn_CuentaConPresupuesto = 0
            vgn_IdPreOrdenTrabajo = 0
            vgn_ConservaMobiliario = 0
            vgn_RequiereNuevoMobiliario = 0
            vgc_OtrosMobiliario = String.Empty
            vgc_OtroTipoRequerimiento = String.Empty
            vgc_NombreArchivo = String.Empty
            vgo_Archivo = Nothing
            vgn_CuentaConAlarma = 0
            vgn_RequiereAlarma = 0
            vgd_TimeStamp = DateTime.Now
            vgc_Usuario = String.Empty
        End Sub
#End Region
    End Class
End Namespace
