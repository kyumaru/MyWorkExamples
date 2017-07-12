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
    Public Class DalOttSolicitudReingreso
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


        ''' <summary>
        ''' Adapter para las solicitudes de reingreso
        ''' </summary>
        ''' <param name="pvo_DataSet"></param>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>01/08/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Sub AdapterOttSolicitudReingreso(pvo_DataSet As Data.DataSet)
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_DisposeConexion As Boolean

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_Conexion.AdapterCrear(String.Format("INSERT INTO OTT_SOLICITUD_REINGRESO (ANNO, ID_SOLICITUD_REINGRESO, ID_UBICACION_ADMINISTRA, ID_DETALLE_MATERIAL, CANTIDAD_REINGRESO, CANTIDAD_RECIBIDA, TIPO_SOLICITUD_REINGRESO, COSTO_CALCULADO, ESTADO, USUARIO) VALUES (:ANNO, :ID_SOLICITUD_REINGRESO, :ID_UBICACION_ADMINISTRA, :ID_DETALLE_MATERIAL, :CANTIDAD_REINGRESO, :CANTIDAD_RECIBIDA, :TIPO_SOLICITUD_REINGRESO, :COSTO_CALCULADO, :ESTADO, :USUARIO)"),
                                          String.Empty,
                                          String.Empty)

                vlo_Conexion.AdapterAgregarParametro(":ANNO", "ANNO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_SOLICITUD_REINGRESO", "ID_SOLICITUD_REINGRESO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_UBICACION_ADMINISTRA", "ID_UBICACION_ADMINISTRA", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_DETALLE_MATERIAL", "ID_DETALLE_MATERIAL", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":CANTIDAD_REINGRESO", "CANTIDAD_REINGRESO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":CANTIDAD_RECIBIDA", "CANTIDAD_RECIBIDA", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":TIPO_SOLICITUD_REINGRESO", "TIPO_SOLICITUD_REINGRESO", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":COSTO_CALCULADO", "COSTO_CALCULADO", DbType.Double, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ESTADO", "ESTADO", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":USUARIO", "USUARIO", DbType.String, ConexionOracle.TipoParametro.Insert)

                vlo_Conexion.AdapterActualizar(pvo_DataSet, pvo_DataSet.Tables(0).TableName)

            Catch ex As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(ex, "")
                If (vlo_Rethrow) Then
                    Throw
                End If
            End Try
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="pvo_DataSet"></param>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>03/08/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Sub AdapterOttSolicitudReingresoMod(pvo_DataSet As Data.DataSet)
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_DisposeConexion As Boolean

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_Conexion.AdapterCrear(String.Empty,
                                          String.Format("UPDATE OTT_SOLICITUD_REINGRESO SET ESTADO = :ESTADO, CANTIDAD_RECIBIDA = :CANTIDAD_RECIBIDA WHERE ANNO = :ANNO AND ID_SOLICITUD_REINGRESO = :ID_SOLICITUD_REINGRESO"),
                                          String.Empty)


                vlo_Conexion.AdapterAgregarParametro(":ESTADO", "ESTADO", DbType.String, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":CANTIDAD_RECIBIDA", "CANTIDAD_RECIBIDA", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ANNO", "ANNO", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_SOLICITUD_REINGRESO", "ID_SOLICITUD_REINGRESO", DbType.Int32, ConexionOracle.TipoParametro.Update)

                vlo_Conexion.AdapterActualizar(pvo_DataSet, pvo_DataSet.Tables(0).TableName)

            Catch ex As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(ex, "")
                If (vlo_Rethrow) Then
                    Throw
                End If
            End Try
        End Sub


#End Region

#Region "Funciones"


        ''' <summary>
        ''' Obtiene los registros de la vista V_OT_ORDEN_SOLIC_REING según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/08/2016 06:58:53 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtOrdenSolicReing(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_ORDEN_SOLIC_REING.Name, V_OT_ORDEN_SOLIC_REING.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OT_ORDEN_SOLIC_REING según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/08/2016 06:58:53 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtOrdenSolicReing(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OT_ORDEN_SOLIC_REING.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Permite agregar un registro en la tabla OTT_SOLICITUD_REINGRESO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function InsertarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOttSolicitudReingreso
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOttSolicitudReingreso)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prI_OTT_SOLICITUD_REINGRESO"

                vlo_Conexion.SetParameter("pvn_Anno", OracleDbType.Int32, vlo_RegistroInterno.Anno)
                vlo_Conexion.SetParameter("pvn_IdSolicitudReingreso", OracleDbType.Int32, vlo_RegistroInterno.IdSolicitudReingreso)
                vlo_Conexion.SetParameter("pvn_IdUbicacionAdministra", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacionAdministra)
                vlo_Conexion.SetParameter("pvn_IdDetalleMaterial", OracleDbType.Int32, vlo_RegistroInterno.IdDetalleMaterial)
                vlo_Conexion.SetParameter("pvn_CantidadReingreso", OracleDbType.Double, vlo_RegistroInterno.CantidadReingreso)
                vlo_Conexion.SetParameter("pvn_CantidadRecibida", OracleDbType.Double, vlo_RegistroInterno.CantidadRecibida)
                vlo_Conexion.SetParameter("pvc_TipoSolicitudReingreso", OracleDbType.Varchar2, vlo_RegistroInterno.TipoSolicitudReingreso)
                vlo_Conexion.SetParameter("pvn_CostoCalculado", OracleDbType.Double, vlo_RegistroInterno.CostoCalculado)
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
        ''' Permite agregar un registro en la tabla OTT_SOLICITUD_REINGRESO
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
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
        ''' Permite borrar un registro en la tabla OTT_SOLICITUD_REINGRESO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function BorrarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOttSolicitudReingreso
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOttSolicitudReingreso)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prD_OTT_SOLICITUD_REINGRESO"

                vlo_Conexion.SetParameter("pvn_Anno", OracleDbType.Int32, vlo_RegistroInterno.Anno)
                vlo_Conexion.SetParameter("pvn_IdSolicitudReingreso", OracleDbType.Int32, vlo_RegistroInterno.IdSolicitudReingreso)

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
        ''' Permite borrar un registro en la tabla OTT_SOLICITUD_REINGRESO
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
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
        ''' Permite modificar un registro en la tabla OTT_SOLICITUD_REINGRESO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ModificarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOttSolicitudReingreso
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOttSolicitudReingreso)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prU_OTT_SOLICITUD_REINGRESO"

                vlo_Conexion.SetParameter("pvn_Anno", OracleDbType.Int32, vlo_RegistroInterno.Anno)
                vlo_Conexion.SetParameter("pvn_IdSolicitudReingreso", OracleDbType.Int32, vlo_RegistroInterno.IdSolicitudReingreso)
                vlo_Conexion.SetParameter("pvn_IdUbicacionAdministra", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacionAdministra)
                vlo_Conexion.SetParameter("pvn_IdDetalleMaterial", OracleDbType.Int32, vlo_RegistroInterno.IdDetalleMaterial)
                vlo_Conexion.SetParameter("pvn_CantidadReingreso", OracleDbType.Double, vlo_RegistroInterno.CantidadReingreso)
                vlo_Conexion.SetParameter("pvn_CantidadRecibida", OracleDbType.Double, vlo_RegistroInterno.CantidadRecibida)
                vlo_Conexion.SetParameter("pvc_TipoSolicitudReingreso", OracleDbType.Varchar2, vlo_RegistroInterno.TipoSolicitudReingreso)
                vlo_Conexion.SetParameter("pvn_CostoCalculado", OracleDbType.Double, vlo_RegistroInterno.CostoCalculado)
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
        ''' Permite modificar un registro en la tabla OTT_SOLICITUD_REINGRESO
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
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
        ''' Obtiene un registro de la tabla OTT_SOLICITUD_REINGRESO según el criterio indicado
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ObtenerRegistro(ByVal pvc_Condicion As String) As EntBase
            Dim vlo_MapeoEntidad As List(Of MapeoSimple)
            Dim vlo_DsDatos As DataSet
            Dim vlo_Resultado As New EntOttSolicitudReingreso

            Try
                vlo_DsDatos = ListarRegistros(pvc_Condicion, String.Empty, False, 0, 0)
                If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    vlo_MapeoEntidad = New List(Of MapeoSimple)
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_SOLICITUD_REINGRESO.ANNO, "Anno"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_SOLICITUD_REINGRESO.ID_SOLICITUD_REINGRESO, "IdSolicitudReingreso"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_SOLICITUD_REINGRESO.ID_UBICACION_ADMINISTRA, "IdUbicacionAdministra"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_SOLICITUD_REINGRESO.ID_DETALLE_MATERIAL, "IdDetalleMaterial"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_SOLICITUD_REINGRESO.CANTIDAD_REINGRESO, "CantidadReingreso"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_SOLICITUD_REINGRESO.CANTIDAD_RECIBIDA, "CantidadRecibida"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_SOLICITUD_REINGRESO.TIPO_SOLICITUD_REINGRESO, "TipoSolicitudReingreso"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_SOLICITUD_REINGRESO.COSTO_CALCULADO, "CostoCalculado"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_SOLICITUD_REINGRESO.ESTADO, "Estado"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_SOLICITUD_REINGRESO.USUARIO, "Usuario"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_SOLICITUD_REINGRESO.TIME_STAMP, "TimeStamp"))

                    vlo_Resultado = vlo_DsDatos.Tables(0).Rows(0).ToEntity(Of EntOttSolicitudReingreso)(vlo_MapeoEntidad)
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
        ''' Obtiene los registros de la vista V_OTT_SOLICITUD_REINGRESO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_SOLICITUD_REINGRESO.Name, "V_OTT_SOLICITUD_REINGRESO", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OTT_SOLICITUD_REINGRESOLst según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_SOLICITUD_REINGRESO.Name, "V_OTT_SOLICITUD_REINGRESOLst", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OTTH_SOLICITUD_REINGRESO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtthSolicitudReingreso(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_SOLICITUD_REINGRESO.Name, "V_OTTH_SOLICITUD_REINGRESO", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTTH_SOLICITUD_REINGRESO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtthSolicitudReingreso(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTTH_SOLICITUD_REINGRESO", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OTT_SOLICITUD_REINGRESO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOttSolicitudReingreso(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OTT_SOLICITUD_REINGRESO.Name, V_OTT_SOLICITUD_REINGRESO.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTT_SOLICITUD_REINGRESO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOttSolicitudReingreso(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OTT_SOLICITUD_REINGRESO.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OTT_SOLICITUD_REINGRESOLST según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOttSolicitudReingresolst(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OTT_SOLICITUD_REINGRESOLST.Name, V_OTT_SOLICITUD_REINGRESOLST.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTT_SOLICITUD_REINGRESOLST según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOttSolicitudReingresolst(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OTT_SOLICITUD_REINGRESOLST.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Ejecuta la función FN_OT_CONSECUTIVO_REINGRESO
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>29/07/2016 03:03:47 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerFnOtConsecutivoReingreso(pvn_Annio As Integer, pvn_Idubicacion As Integer) As Double
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

                vlc_Sentencia = "SELECT FN_OT_CONSECUTIVO_REINGRESO(:pvn_Annio, :pvn_Idubicacion) AS RESULTADO FROM DUAL"
                vlo_Conexion.SetParameter("pvn_Annio", OracleDbType.Int32, pvn_Annio)
                vlo_Conexion.SetParameter("pvn_Idubicacion", OracleDbType.Int32, pvn_Idubicacion)

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

#End Region
    End Class
End Namespace
