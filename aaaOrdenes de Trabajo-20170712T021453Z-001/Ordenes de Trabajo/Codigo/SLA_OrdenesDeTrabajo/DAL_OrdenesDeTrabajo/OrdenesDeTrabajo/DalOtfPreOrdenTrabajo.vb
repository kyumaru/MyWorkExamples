Imports Oracle.DataAccess.Client
Imports Utilerias.Genericos
Imports Utilerias.Genericos.Bases
Imports Utilerias.Genericos.Extensiones
Imports Utilerias.BaseDatos.OracleServer
Imports Utilerias.OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo.Modelo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
    Public Class DalOtfPreOrdenTrabajo
        Inherits DalBase
#Region "Constructores"
        Public Sub New(ByVal pvc_StrConexion As String)
            MyBase.New(pvc_StrConexion)
        End Sub

        Public Sub New(ByVal pvo_Entidad As EntBase, ByVal pvc_StrConexion As String)
            MyBase.New(pvo_Entidad, pvc_StrConexion)
        End Sub

        Public Sub New(ByVal pvo_Entidad As EntBase, ByVal pvo_Conexion As DbBase)
            MyBase.New(pvo_Entidad, pvo_Conexion)
        End Sub

        Public Sub New(ByVal pvo_Conexion As DbBase)
            MyBase.New(pvo_Conexion)
        End Sub
#End Region

#Region "Metodos"

#End Region

#Region "Funciones"

        ''' <summary>
        ''' Permite ejecutar el procedimiento PR_OT_BORRAR_PRE_ORDEN
        ''' </summary>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>21/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Sub EjecutarPrOtBorrarPreOrden(pvn_IdUbicacion As Integer, pvn_IdPreoOrdentrabajo As Integer)
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "PR_OT_BORRAR_PRE_ORDEN"

                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, pvn_IdUbicacion)
                vlo_Conexion.SetParameter("pvn_IdPreOrdenTrabajo", OracleDbType.Int32, pvn_IdPreoOrdentrabajo)

                vlo_Conexion.EjecutarSentencia(vlc_Sentencia, CommandType.StoredProcedure)
            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

        End Sub

        ''' <summary>
        ''' Permite agregar un registro en la tabla OTF_PRE_ORDEN_TRABAJO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:07:43 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function InsertarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOtfPreOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOtfPreOrdenTrabajo)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prI_OTF_PRE_ORDEN_TRABAJO"

                vlo_Conexion.SetParameter("prn_IdPreOrdenTrabajo", OracleDbType.Int32, vln_Resultado, ParameterDirection.Output)
                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
                vlo_Conexion.SetParameter("pvn_IdLugarTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdLugarTrabajo)
                vlo_Conexion.SetParameter("pvn_IdCategoriaServicio", OracleDbType.Int32, vlo_RegistroInterno.IdCategoriaServicio)
                vlo_Conexion.SetParameter("pvn_IdActividad", OracleDbType.Int32, vlo_RegistroInterno.IdActividad)
                vlo_Conexion.SetParameter("pvn_NumEmpleado", OracleDbType.Double, vlo_RegistroInterno.NumEmpleado)
                vlo_Conexion.SetParameter("pvn_IncluidaEnRecepcion", OracleDbType.Double, vlo_RegistroInterno.IncluidaEnRecepcion)
                vlo_Conexion.SetParameter("pvn_Anno", OracleDbType.Int32, vlo_RegistroInterno.Anno)
                vlo_Conexion.SetParameter("pvd_FechaHoraSolicita", OracleDbType.Date, vlo_RegistroInterno.FechaHoraSolicita)
                vlo_Conexion.SetParameter("pvn_CodUnidadSirh", OracleDbType.Int32, vlo_RegistroInterno.CodUnidadSirh)
                vlo_Conexion.SetParameter("pvc_NombrePersonaContacto", OracleDbType.Varchar2, vlo_RegistroInterno.NombrePersonaContacto)
                vlo_Conexion.SetParameter("pvc_Telefono", OracleDbType.Varchar2, vlo_RegistroInterno.Telefono)
                vlo_Conexion.SetParameter("pvc_SennasExactas", OracleDbType.Varchar2, vlo_RegistroInterno.SennasExactas)
                vlo_Conexion.SetParameter("pvc_DescripcionTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.DescripcionTrabajo)
                vlo_Conexion.SetParameter("pvc_NombreImagen1", OracleDbType.Varchar2, vlo_RegistroInterno.NombreImagen1)
                vlo_Conexion.SetParameterBinary("pvo_Imagen1", vlo_RegistroInterno.Imagen1)
                vlo_Conexion.SetParameter("pvc_NombreImagen2", OracleDbType.Varchar2, vlo_RegistroInterno.NombreImagen2)
                vlo_Conexion.SetParameterBinary("pvo_Imagen2", vlo_RegistroInterno.Imagen2)
                vlo_Conexion.SetParameter("pvc_Usuario", OracleDbType.Varchar2, vlo_RegistroInterno.Usuario)
                If vlo_RegistroInterno.IdUbicacionOrigen <> 0 Then
                    vlo_Conexion.SetParameter("pvn_IdUbicacionOrigen", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacionOrigen)
                End If
                vlo_Conexion.EjecutarSentencia(vlc_Sentencia, CommandType.StoredProcedure)
                vln_Resultado = CType(vlo_Conexion.GetParameterValue("prn_IdPreOrdenTrabajo"), Integer)
            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite agregar un registro en la tabla OTF_PRE_ORDEN_TRABAJO
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:07:43 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function InsertarRegistro() As Integer
            Dim vln_Resultado As Integer

            Try
                If vlb_HayDatos Then
                    vln_Resultado = InsertarRegistro(vlo_Registro)
                Else
                    vln_Resultado = -1
                End If
            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite borrar un registro en la tabla OTF_PRE_ORDEN_TRABAJO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:07:43 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function BorrarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOtfPreOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOtfPreOrdenTrabajo)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prD_OTF_PRE_ORDEN_TRABAJO"

                vlo_Conexion.SetParameter("pvn_IdPreOrdenTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdPreOrdenTrabajo)
                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)

                vlo_Conexion.EjecutarSentencia(vlc_Sentencia, CommandType.StoredProcedure)
                vln_Resultado = 1
            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite borrar un registro en la tabla OTF_PRE_ORDEN_TRABAJO
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:07:43 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function BorrarRegistro() As Integer
            Dim vln_Resultado As Integer

            Try
                If vlb_HayDatos Then
                    vln_Resultado = BorrarRegistro(vlo_Registro)
                Else
                    vln_Resultado = -1
                End If
            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite modificar un registro en la tabla OTF_PRE_ORDEN_TRABAJO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:07:43 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ModificarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOtfPreOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOtfPreOrdenTrabajo)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prU_OTF_PRE_ORDEN_TRABAJO"

                vlo_Conexion.SetParameter("pvn_IdPreOrdenTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdPreOrdenTrabajo)
                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
                vlo_Conexion.SetParameter("pvn_IdLugarTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdLugarTrabajo)
                vlo_Conexion.SetParameter("pvn_IdCategoriaServicio", OracleDbType.Int32, vlo_RegistroInterno.IdCategoriaServicio)
                vlo_Conexion.SetParameter("pvn_IdActividad", OracleDbType.Int32, vlo_RegistroInterno.IdActividad)
                vlo_Conexion.SetParameter("pvn_NumEmpleado", OracleDbType.Double, vlo_RegistroInterno.NumEmpleado)
                vlo_Conexion.SetParameter("pvn_Anno", OracleDbType.Int32, vlo_RegistroInterno.Anno)
                vlo_Conexion.SetParameter("pvd_FechaHoraSolicita", OracleDbType.Date, vlo_RegistroInterno.FechaHoraSolicita)
                vlo_Conexion.SetParameter("pvn_CodUnidadSirh", OracleDbType.Int32, vlo_RegistroInterno.CodUnidadSirh)
                vlo_Conexion.SetParameter("pvc_NombrePersonaContacto", OracleDbType.Varchar2, vlo_RegistroInterno.NombrePersonaContacto)
                vlo_Conexion.SetParameter("pvc_Telefono", OracleDbType.Varchar2, vlo_RegistroInterno.Telefono)
                vlo_Conexion.SetParameter("pvc_SennasExactas", OracleDbType.Varchar2, vlo_RegistroInterno.SennasExactas)
                vlo_Conexion.SetParameter("pvc_DescripcionTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.DescripcionTrabajo)
                vlo_Conexion.SetParameter("pvc_NombreImagen1", OracleDbType.Varchar2, vlo_RegistroInterno.NombreImagen1)
                vlo_Conexion.SetParameterBinary("pvo_Imagen1", vlo_RegistroInterno.Imagen1)
                vlo_Conexion.SetParameter("pvc_NombreImagen2", OracleDbType.Varchar2, vlo_RegistroInterno.NombreImagen2)
                vlo_Conexion.SetParameterBinary("pvo_Imagen2", vlo_RegistroInterno.Imagen2)
                vlo_Conexion.SetParameter("pvc_Usuario", OracleDbType.Varchar2, vlo_RegistroInterno.Usuario)
                vlo_Conexion.SetParameter("pvd_TimeStamp", OracleDbType.Date, vlo_RegistroInterno.TimeStamp)
                If vlo_RegistroInterno.IdUbicacionOrigen <> 0 Then
                    vlo_Conexion.SetParameter("pvn_IdUbicacionOrigen", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacionOrigen)
                End If
                vlo_Conexion.EjecutarSentencia(vlc_Sentencia, CommandType.StoredProcedure)
                vln_Resultado = 1
            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite modificar un registro en la tabla OTF_PRE_ORDEN_TRABAJO
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:07:43 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ModificarRegistro() As Integer
            Dim vln_Resultado As Integer

            Try
                If vlb_HayDatos Then
                    vln_Resultado = ModificarRegistro(vlo_Registro)
                Else
                    vln_Resultado = -1
                End If
            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Obtiene un registro de la tabla OTF_PRE_ORDEN_TRABAJO según el criterio indicado
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:07:43 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ObtenerRegistro(ByVal pvc_Condicion As String) As EntBase
            Dim vlo_MapeoEntidad As List(Of MapeoSimple)
            Dim vlo_DsDatos As DataSet
            Dim vlo_Resultado As New EntOtfPreOrdenTrabajo

            Try
                vlo_DsDatos = ListarRegistros(pvc_Condicion, String.Empty, False, 0, 0)
                If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    vlo_MapeoEntidad = New List(Of MapeoSimple)
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.ID_PRE_ORDEN_TRABAJO, "IdPreOrdenTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.ID_UBICACION, "IdUbicacion"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.ID_LUGAR_TRABAJO, "IdLugarTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.ID_CATEGORIA_SERVICIO, "IdCategoriaServicio"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.ID_ACTIVIDAD, "IdActividad"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.NUM_EMPLEADO, "NumEmpleado"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.ANNO, "Anno"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.FECHA_HORA_SOLICITA, "FechaHoraSolicita"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.COD_UNIDAD_SIRH, "CodUnidadSirh"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.NOMBRE_PERSONA_CONTACTO, "NombrePersonaContacto"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.TELEFONO, "Telefono"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.SENNAS_EXACTAS, "SennasExactas"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.DESCRIPCION_TRABAJO, "DescripcionTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.NOMBRE_IMAGEN1, "NombreImagen1"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.IMAGEN1, "Imagen1"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.NOMBRE_IMAGEN2, "NombreImagen2"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.IMAGEN2, "Imagen2"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.USUARIO, "Usuario"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.TIME_STAMP, "TimeStamp"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.INCLUIDA_EN_RECEPCION, "IncluidaEnRecepcion"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTF_PRE_ORDEN_TRABAJO.ID_UBICACION_ORIGEN, "IdUbicacionOrigen"))

                    vlo_Resultado = vlo_DsDatos.Tables(0).Rows(0).ToEntity(Of EntOtfPreOrdenTrabajo)(vlo_MapeoEntidad)
                    vlo_Resultado.Existe = True
                End If

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            End Try

            Return vlo_Resultado
        End Function

        ''' <summary>
        ''' Obtiene los registros de la vista V_OTF_PRE_ORDEN_TRABAJO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:07:43 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ListarRegistros(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
            Dim vlo_Conexion As DbBase
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_DsDatos As New DataSet

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTF_PRE_ORDEN_TRABAJO.Name, "V_OTF_PRE_ORDEN_TRABAJO", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlo_DsDatos
        End Function

        ''' <summary>
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTF_PRE_ORDEN_TRABAJO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:07:43 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtfPreOrdenTrabajo(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
            Dim vlo_Conexion As DbBase
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_Resultado As EntDatosPaginacion

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTF_PRE_ORDEN_TRABAJO", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlo_Resultado
        End Function

        ''' <summary>
        ''' Obtiene los registros de la vista V_OTF_PRE_ORDEN_TRABAJOLst según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:07:43 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ListarRegistrosLista(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
            Dim vlo_Conexion As DbBase
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_DsDatos As New DataSet

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTF_PRE_ORDEN_TRABAJO.Name, "V_OTF_PRE_ORDEN_TRABAJOLst", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlo_DsDatos
        End Function

        ''' <summary>
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTF_PRE_ORDEN_TRABAJOLst según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:07:43 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtfPreOrdenTrabajolst(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
            Dim vlo_Conexion As DbBase
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_Resultado As EntDatosPaginacion

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTF_PRE_ORDEN_TRABAJOLst", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlo_Resultado
        End Function

#End Region
    End Class
End Namespace
