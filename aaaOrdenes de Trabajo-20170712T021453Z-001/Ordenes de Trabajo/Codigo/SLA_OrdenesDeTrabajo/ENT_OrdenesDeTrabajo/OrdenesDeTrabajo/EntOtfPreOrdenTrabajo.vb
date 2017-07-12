Imports System.Xml.Serialization
Imports Utilerias.Genericos.Bases

Namespace OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
    <Serializable()> _
    Public Class EntOtfPreOrdenTrabajo
        Inherits EntBase
#Region "Atributos"
        Private vgn_IdPreOrdenTrabajo As Integer 'Llave primaria de la tabla otf_pre_orden_trabajo que se asocia con la secuencia sq_id_pre_orden_trabajo
        Private vgn_IdUbicacion As Integer 'Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        Private vgn_IdLugarTrabajo As Integer 'Llave primaria de la tabla otm_lugar_trabajo que se asocia con la secuencia sq_id_lugar_trabajo
        Private vgn_IdCategoriaServicio As Integer 'Llave primaria de la tabla otm_categoria_servicio que se asocia con la secuencia sq_id_categoria_servicio
        Private vgn_IdActividad As Integer 'Llave primaria de la tabla otm_actividad que se asocia con la secuencia sq_id_actividad
        Private vgn_NumEmpleado As Double '
        Private vgn_Anno As Integer 'Año de solicitud de la ot
        Private vgd_FechaHoraSolicita As DateTime 'Fecha y hora en que el solicitante registra la orden de trabajo.
        Private vgn_CodUnidadSirh As Integer 'Código de la unidad académica o administrativa que tramita la autorización de la orden de trabajo
        Private vgc_NombrePersonaContacto As String 'Nombre completo y apellidos de la persona que podrá ser contactada para evacuar dudas de la orden de trabajo solicitada
        Private vgc_Telefono As String 'Teléfono mediante el cual se podrá contactar al solicitante o a la persona contacto
        Private vgc_SennasExactas As String 'Señas exactas del lugar donde se requiere la realización de trabajo
        Private vgc_DescripcionTrabajo As String 'Descripción detallada del trabajo requerido
        Private vgc_NombreImagen1 As String 'Campo para almacenar el nombre de un archivo asociado a la solicitud 
        Private vgo_Imagen1 As Byte() ' archivo asociado a la solicitud 
        Private vgc_NombreImagen2 As String 'Campo para almacenar el nombre de un segundo archivo asociado a la solicitud 
        Private vgo_Imagen2 As Byte() 'Segundo archivo asociado a la solicitud 
        Private vgc_Usuario As String 'Usuario que crea o modifica el registro.
        Private vgd_TimeStamp As DateTime 'Control de concurrencia - valor por defecto: fecha y hora del sistema
        Private vgn_IncluidaEnRecepcion As Integer
        Private vgn_IdUbicacionOrigen As Integer

#End Region
#Region "Propiedades"

        Public Property IdUbicacionOrigen() As Integer
            Get
                Return vgn_IdUbicacionOrigen
            End Get
            Set(ByVal value As Integer)
                vgn_IdUbicacionOrigen = value
            End Set
        End Property

        Public Property IncluidaEnRecepcion() As Integer
            Get
                Return vgn_IncluidaEnRecepcion
            End Get
            Set(ByVal value As Integer)
                vgn_IncluidaEnRecepcion = value
            End Set
        End Property

        ''' <summary>
        ''' Llave primaria de la tabla otf_pre_orden_trabajo que se asocia con la secuencia sq_id_pre_orden_trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
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
        ''' Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
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
        ''' Llave primaria de la tabla otm_lugar_trabajo que se asocia con la secuencia sq_id_lugar_trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
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
        ''' Llave primaria de la tabla otm_categoria_servicio que se asocia con la secuencia sq_id_categoria_servicio
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
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
        ''' Llave primaria de la tabla otm_actividad que se asocia con la secuencia sq_id_actividad
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property IdActividad() As Integer
            Get
                Return vgn_IdActividad
            End Get
            Set(ByVal value As Integer)
                vgn_IdActividad = value
            End Set
        End Property

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NumEmpleado() As Double
            Get
                Return vgn_NumEmpleado
            End Get
            Set(ByVal value As Double)
                vgn_NumEmpleado = value
            End Set
        End Property

        ''' <summary>
        ''' Año de solicitud de la ot
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
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
        ''' Fecha y hora en que el solicitante registra la orden de trabajo.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property FechaHoraSolicita() As DateTime
            Get
                Return vgd_FechaHoraSolicita
            End Get
            Set(ByVal value As DateTime)
                vgd_FechaHoraSolicita = value
            End Set
        End Property

        ''' <summary>
        ''' Código de la unidad académica o administrativa que tramita la autorización de la orden de trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
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
        ''' Nombre completo y apellidos de la persona que podrá ser contactada para evacuar dudas de la orden de trabajo solicitada
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NombrePersonaContacto() As String
            Get
                Return vgc_NombrePersonaContacto
            End Get
            Set(ByVal value As String)
                vgc_NombrePersonaContacto = value
            End Set
        End Property

        ''' <summary>
        ''' Teléfono mediante el cual se podrá contactar al solicitante o a la persona contacto
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Telefono() As String
            Get
                Return vgc_Telefono
            End Get
            Set(ByVal value As String)
                vgc_Telefono = value
            End Set
        End Property

        ''' <summary>
        ''' Señas exactas del lugar donde se requiere la realización de trabajo
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property SennasExactas() As String
            Get
                Return vgc_SennasExactas
            End Get
            Set(ByVal value As String)
                vgc_SennasExactas = value
            End Set
        End Property

        ''' <summary>
        ''' Descripción detallada del trabajo requerido
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property DescripcionTrabajo() As String
            Get
                Return vgc_DescripcionTrabajo
            End Get
            Set(ByVal value As String)
                vgc_DescripcionTrabajo = value
            End Set
        End Property

        ''' <summary>
        ''' Campo para almacenar el nombre de un archivo asociado a la solicitud 
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NombreImagen1() As String
            Get
                Return vgc_NombreImagen1
            End Get
            Set(ByVal value As String)
                vgc_NombreImagen1 = value
            End Set
        End Property

        ''' <summary>
        '''  archivo asociado a la solicitud 
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Imagen1() As Byte()
            Get
                Return vgo_Imagen1
            End Get
            Set(ByVal value As Byte())
                vgo_Imagen1 = value
            End Set
        End Property

        ''' <summary>
        ''' Campo para almacenar el nombre de un segundo archivo asociado a la solicitud 
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property NombreImagen2() As String
            Get
                Return vgc_NombreImagen2
            End Get
            Set(ByVal value As String)
                vgc_NombreImagen2 = value
            End Set
        End Property

        ''' <summary>
        ''' Segundo archivo asociado a la solicitud 
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Property Imagen2() As Byte()
            Get
                Return vgo_Imagen2
            End Get
            Set(ByVal value As Byte())
                vgo_Imagen2 = value
            End Set
        End Property

        ''' <summary>
        ''' Usuario que crea o modifica el registro.
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
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
        ''' <creationDate>14/04/2016 12:03:03 p.m.</creationDate>
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
            vgn_IdPreOrdenTrabajo = 0
            vgn_IdUbicacionOrigen = 0
            vgn_IdUbicacion = 0
            vgn_IdLugarTrabajo = 0
            vgn_IdCategoriaServicio = 0
            vgn_IdActividad = 0
            vgn_NumEmpleado = 0
            vgn_Anno = 0
            vgn_IncluidaEnRecepcion = 0
            vgd_FechaHoraSolicita = DateTime.Now
            vgn_CodUnidadSirh = 0
            vgc_NombrePersonaContacto = String.Empty
            vgc_Telefono = String.Empty
            vgc_SennasExactas = String.Empty
            vgc_DescripcionTrabajo = String.Empty
            vgc_NombreImagen1 = String.Empty
            vgo_Imagen1 = Nothing
            vgc_NombreImagen2 = String.Empty
            vgo_Imagen2 = Nothing
            vgc_Usuario = String.Empty
            vgd_TimeStamp = DateTime.Now
        End Sub
#End Region
    End Class
End Namespace
