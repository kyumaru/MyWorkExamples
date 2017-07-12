Imports Oracle.DataAccess.Client
Imports Utilerias.Genericos
Imports Utilerias.Genericos.Bases
Imports Utilerias.Genericos.Extensiones
Imports Utilerias.BaseDatos.OracleServer
Imports Utilerias.OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo.Modelo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.AccesoDatos.Catalogos
    Public Class DalOtmRequerimiento
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
        ''' Permite agregar un registro en la tabla OTM_REQUERIMIENTO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function InsertarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOtmRequerimiento
            Dim vln_Resultado As Integer
            Dim vln_null As Nullable(Of Integer)

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOtmRequerimiento)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prI_OTM_REQUERIMIENTO"

                vlo_Conexion.SetParameter("prn_IdRequerimiento", OracleDbType.Int32, vlo_RegistroInterno.IdRequerimiento)
                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
                If vlo_RegistroInterno.IdRequerimientoPadre <> 0 Then
                    vlo_Conexion.SetParameter("pvn_IdRequerimientoPadre", OracleDbType.Int32, vlo_RegistroInterno.IdRequerimientoPadre)
                End If
                vlo_Conexion.SetParameter("pvc_Descripcion", OracleDbType.Varchar2, vlo_RegistroInterno.Descripcion)
                vlo_Conexion.SetParameter("pvn_Orden", OracleDbType.Int32, vlo_RegistroInterno.Orden)
                vlo_Conexion.SetParameter("pvn_Nivel", OracleDbType.Int32, vlo_RegistroInterno.Nivel)
                vlo_Conexion.SetParameter("pvc_TipoValor", OracleDbType.Varchar2, vlo_RegistroInterno.TipoValor)
                vlo_Conexion.SetParameter("pvc_Estado", OracleDbType.Varchar2, vlo_RegistroInterno.Estado)
                vlo_Conexion.SetParameter("pvc_Usuario", OracleDbType.Varchar2, vlo_RegistroInterno.Usuario)

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
        ''' Permite agregar un registro en la tabla OTM_REQUERIMIENTO
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
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
        ''' Permite borrar un registro en la tabla OTM_REQUERIMIENTO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function BorrarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOtmRequerimiento
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOtmRequerimiento)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prD_OTM_REQUERIMIENTO"

                vlo_Conexion.SetParameter("pvn_IdRequerimiento", OracleDbType.Int32, vlo_RegistroInterno.IdRequerimiento)

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
        ''' Permite borrar un registro en la tabla OTM_REQUERIMIENTO
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
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
        ''' Permite modificar un registro en la tabla OTM_REQUERIMIENTO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ModificarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOtmRequerimiento
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOtmRequerimiento)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prU_OTM_REQUERIMIENTO"
                vlo_Conexion.SetParameter("pvn_IdRequerimiento", OracleDbType.Int32, vlo_RegistroInterno.IdRequerimiento)
                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
                If vlo_RegistroInterno.IdRequerimientoPadre <> 0 Then
                    vlo_Conexion.SetParameter("pvn_IdRequerimientoPadre", OracleDbType.Int32, vlo_RegistroInterno.IdRequerimientoPadre)
                End If
                vlo_Conexion.SetParameter("pvc_Descripcion", OracleDbType.Varchar2, vlo_RegistroInterno.Descripcion)
                vlo_Conexion.SetParameter("pvn_Orden", OracleDbType.Int32, vlo_RegistroInterno.Orden)
                vlo_Conexion.SetParameter("pvn_Nivel", OracleDbType.Int32, vlo_RegistroInterno.Nivel)
                vlo_Conexion.SetParameter("pvc_TipoValor", OracleDbType.Varchar2, vlo_RegistroInterno.TipoValor)
                vlo_Conexion.SetParameter("pvc_Estado", OracleDbType.Varchar2, vlo_RegistroInterno.Estado)
                vlo_Conexion.SetParameter("pvc_Usuario", OracleDbType.Varchar2, vlo_RegistroInterno.Usuario)
                vlo_Conexion.SetParameter("pvd_TimeStamp", OracleDbType.Date, vlo_RegistroInterno.TimeStamp)

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
        ''' Permite modificar un registro en la tabla OTM_REQUERIMIENTO
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
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
        ''' Obtiene un registro de la tabla OTM_REQUERIMIENTO según el criterio indicado
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ObtenerRegistro(ByVal pvc_Condicion As String) As EntBase
            Dim vlo_MapeoEntidad As List(Of MapeoSimple)
            Dim vlo_DsDatos As DataSet
            Dim vlo_Resultado As New EntOtmRequerimiento

            Try
                vlo_DsDatos = ListarRegistros(pvc_Condicion, String.Empty, False, 0, 0)
                If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    vlo_MapeoEntidad = New List(Of MapeoSimple)
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTM_REQUERIMIENTO.ID_REQUERIMIENTO, "IdRequerimiento"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTM_REQUERIMIENTO.ID_UBICACION, "IdUbicacion"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTM_REQUERIMIENTO.ID_REQUERIMIENTO_PADRE, "IdRequerimientoPadre"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTM_REQUERIMIENTO.DESCRIPCION, "Descripcion"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTM_REQUERIMIENTO.ORDEN, "Orden"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTM_REQUERIMIENTO.NIVEL, "Nivel"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTM_REQUERIMIENTO.TIPO_VALOR, "TipoValor"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTM_REQUERIMIENTO.ESTADO, "Estado"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTM_REQUERIMIENTO.USUARIO, "Usuario"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTM_REQUERIMIENTO.TIME_STAMP, "TimeStamp"))

                    vlo_Resultado = vlo_DsDatos.Tables(0).Rows(0).ToEntity(Of EntOtmRequerimiento)(vlo_MapeoEntidad)
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
        ''' Obtiene los registros de la vista V_OTM_REQUERIMIENTO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTM_REQUERIMIENTO.Name, "V_OTM_REQUERIMIENTO", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTM_REQUERIMIENTO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtmRequerimiento(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTM_REQUERIMIENTO", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OTM_REQUERIMIENTOLst según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTM_REQUERIMIENTO.Name, "V_OTM_REQUERIMIENTOLst", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTM_REQUERIMIENTOLst según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>19/11/2015 10:05:27 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtmRequerimientolst(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTM_REQUERIMIENTOLst", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Ejecuta la función FN_OT_ULTIMO_ORDEN_REQ
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>25/11/2015 09:37:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerFnOtUltimoordenrequerimiento(pvn_PvnNivel As Double) As Double
            Dim vlo_Conexion As DbBase
            Dim vlb_DisposeConexion As Boolean
            Dim vlc_Sentencia As String
            Dim vlo_DsDatos As New DataSet
            Dim vln_Resultado As Double

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "SELECT FN_OT_ULTIMO_ORDEN_REQ(:pvn_PvnNivel) AS RESULTADO FROM DUAL"
                vlo_Conexion.SetParameter("pvn_PvnNivel", OracleDbType.Double, pvn_PvnNivel)

                vlo_Conexion.EjecutarSentencia(vlc_Sentencia, CommandType.Text, "Resultado", vlo_DsDatos)
                vln_Resultado = CType(vlo_DsDatos.Tables("Resultado").Rows(0)("RESULTADO"), Double)

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
        ''' Permite ejecutar el procedimiento PR_OT_BAJAR_REQUERIMIENTO
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>25/11/2015 09:16:32 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Sub EjecutarPrOtBajarrequerimiento(pvn_PvnIdrequerimiento As Double)
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

                vlc_Sentencia = "PR_OT_BAJAR_REQUERIMIENTO"

                vlo_Conexion.SetParameter("PVN_IDREQUERIMIENTO", OracleDbType.Double, pvn_PvnIdrequerimiento)

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
        ''' Permite ejecutar el procedimiento PR_OT_SUBIR_REQUERIMIENTO
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>25/11/2015 09:16:32 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Sub EjecutarPrOtSubirrequerimiento(pvn_PvnIdrequerimiento As Double)
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

                vlc_Sentencia = "PR_OT_SUBIR_REQUERIMIENTO"

                vlo_Conexion.SetParameter("PVN_IDREQUERIMIENTO", OracleDbType.Double, pvn_PvnIdrequerimiento)

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
        ''' Permite ejecutar el procedimiento PR_OT_INHABILITAR_REQ
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>12/01/2016 08:30:35 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Sub EjecutarPrOtInhabilitarrequerimiento(pvn_PvnIdrequerimiento As Double)
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

                vlc_Sentencia = "PR_OT_INHABILITAR_REQ"

                vlo_Conexion.SetParameter("PVN_IDREQUERIMIENTO", OracleDbType.Double, pvn_PvnIdrequerimiento)

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

#End Region
    End Class
End Namespace
