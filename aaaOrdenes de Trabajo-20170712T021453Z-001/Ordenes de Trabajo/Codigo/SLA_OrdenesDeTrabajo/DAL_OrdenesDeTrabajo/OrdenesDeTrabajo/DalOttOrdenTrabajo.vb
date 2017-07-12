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
    Public Class DalOttOrdenTrabajo
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
        ''' Adapter para las ordenes de tarbajo
        ''' </summary>
        ''' <param name="pvo_DataSet"></param>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>11/09/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Sub AdapterOttOrdenesTrabajo(pvo_DataSet As Data.DataSet)
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

                vlo_Conexion.AdapterCrear(String.Format("INSERT INTO OTT_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO, ANNO, CONSECUTIVO, TIPO_ORDEN_TRABAJO, ESTADO_ORDEN_TRABAJO, NUM_EMPLEADO, ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD, ID_LUGAR_TRABAJO, FECHA_HORA_SOLICITA, COD_UNIDAD_SIRH, SENNAS_EXACTAS, DESCRIPCION_TRABAJO, USUARIO, PARENTESCO) VALUES (:ID_UBICACION, :ID_ORDEN_TRABAJO, :ANNO, :CONSECUTIVO, :TIPO_ORDEN_TRABAJO, :ESTADO_ORDEN_TRABAJO, :NUM_EMPLEADO, :ID_CATEGORIA_SERVICIO, :ID_ACTIVIDAD, :ID_LUGAR_TRABAJO, :FECHA_HORA_SOLICITA, :COD_UNIDAD_SIRH, :SENNAS_EXACTAS, :DESCRIPCION_TRABAJO, :USUARIO, :PARENTESCO)"),
                                          String.Empty,
                                          String.Empty)

                vlo_Conexion.AdapterAgregarParametro(":ID_UBICACION", "ID_UBICACION", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_ORDEN_TRABAJO", "ID_ORDEN_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ANNO", "ANNO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":CONSECUTIVO", "CONSECUTIVO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":TIPO_ORDEN_TRABAJO", "TIPO_ORDEN_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ESTADO_ORDEN_TRABAJO", "ESTADO_ORDEN_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":NUM_EMPLEADO", "NUM_EMPLEADO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_CATEGORIA_SERVICIO", "ID_CATEGORIA_SERVICIO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_ACTIVIDAD", "ID_ACTIVIDAD", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_LUGAR_TRABAJO", "ID_LUGAR_TRABAJO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":FECHA_HORA_SOLICITA", "FECHA_HORA_SOLICITA", DbType.DateTime, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":COD_UNIDAD_SIRH", "COD_UNIDAD_SIRH", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":SENNAS_EXACTAS", "SENNAS_EXACTAS", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":DESCRIPCION_TRABAJO", "DESCRIPCION_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":USUARIO", "USUARIO", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":PARENTESCO", "PARENTESCO", DbType.String, ConexionOracle.TipoParametro.Insert)

                vlo_Conexion.AdapterActualizar(pvo_DataSet, pvo_DataSet.Tables(0).TableName)

            Catch ex As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(ex, "")
                If (vlo_Rethrow) Then
                    Throw
                End If
            End Try
        End Sub

        ''' <summary>
        ''' Adapter para las ordenes de tarbajo, modificacion
        ''' </summary>
        ''' <param name="pvo_DataSet"></param>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>24/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Sub AdapterOttOrdenesTrabajoModificacion(pvo_DataSet As Data.DataSet)
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
                                          String.Format("UPDATE OTT_ORDEN_TRABAJO SET ESTADO_ORDEN_TRABAJO = :ESTADO_ORDEN_TRABAJO WHERE ID_UBICACION = :ID_UBICACION AND ID_ORDEN_TRABAJO = :ID_ORDEN_TRABAJO"),
                                          String.Empty)

                vlo_Conexion.AdapterAgregarParametro(":ESTADO_ORDEN_TRABAJO", "ESTADO_ORDEN_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_UBICACION", "ID_UBICACION", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_ORDEN_TRABAJO", "ID_ORDEN_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Update)

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
        ''' Permite ejecutar el procedimiento PR_OT_ASIG_OT_DISENIO
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/04/2016 03:32:40 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Sub EjecutarPrOtAsigOtDisenio(pvn_IdUbicacion As Integer, pvn_IdPreoOrdentrabajo As Integer, pvn_CodUnidadSirh As Integer, pvc_NombrePersonaContacto As String, pvc_Telefono As String, pvc_SennasExactas As String, pvc_DescripcionTrabajo As String, pvc_Usuario As String, pvn_NumEmpleado As Integer, pvn_IdCategoriaServicio As Integer, pvn_IdActividad As Integer, pvn_IdLugarTrabajo As Integer, pvn_IncluidaEnRecepcion As Integer, pvn_IdUbicacionOrigen As Integer)
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

                vlc_Sentencia = "PR_OT_ASIG_OT_DISENIO"

                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, pvn_IdUbicacion)
                vlo_Conexion.SetParameter("pvn_IdPreOrdenTrabajo", OracleDbType.Int32, pvn_IdPreoOrdentrabajo)
                vlo_Conexion.SetParameter("pvn_CodUnidadSirh", OracleDbType.Int32, pvn_CodUnidadSirh)
                vlo_Conexion.SetParameter("pvc_NombrePersonaContacto", OracleDbType.Varchar2, pvc_NombrePersonaContacto)
                vlo_Conexion.SetParameter("pvc_Telefono", OracleDbType.Varchar2, pvc_Telefono)
                vlo_Conexion.SetParameter("pvc_SennasExactas", OracleDbType.Varchar2, pvc_SennasExactas)
                vlo_Conexion.SetParameter("pvc_DescripcionTrabajo", OracleDbType.Varchar2, pvc_DescripcionTrabajo)
                vlo_Conexion.SetParameter("pvc_Usuario", OracleDbType.Varchar2, pvc_Usuario)
                vlo_Conexion.SetParameter("pvn_NumEmpleado", OracleDbType.Int32, pvn_NumEmpleado)
                vlo_Conexion.SetParameter("pvn_IdCategoriaServicio", OracleDbType.Int32, pvn_IdCategoriaServicio)
                vlo_Conexion.SetParameter("pvn_IdActividad", OracleDbType.Int32, pvn_IdActividad)
                vlo_Conexion.SetParameter("pvn_IdLugarTrabajo", OracleDbType.Int32, pvn_IdLugarTrabajo)
                vlo_Conexion.SetParameter("pvn_IncluidaEnRecepcion", OracleDbType.Int32, pvn_IncluidaEnRecepcion)
                If pvn_IdUbicacionOrigen <> 0 Then
                    vlo_Conexion.SetParameter("pvn_IdUbicacionOrigen", OracleDbType.Int32, pvn_IdUbicacionOrigen)
                End If

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
        ''' Permite ejecutar el procedimiento PR_OT_ASIG_OT_MANTE
        ''' </summary>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>19/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Sub EjecutarPrOtAsigOtMante(pvn_IdUbicacion As Integer, pvn_IdPreoOrdentrabajo As Integer, pvn_CodUnidadSirh As Integer, pvc_NombrePersonaContacto As String, pvc_Telefono As String, pvc_SennasExactas As String, pvc_DescripcionTrabajo As String, pvc_Usuario As String, pvn_NumEmpleado As Integer, pvn_IdCategoriaServicio As Integer, pvn_IdActividad As Integer, pvn_IdLugarTrabajo As Integer, pvn_IncluidaEnRecepcion As Integer, pvn_IdUbicacionOrigen As Integer)
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

                vlc_Sentencia = "PR_OT_ASIG_OT_MANTE"

                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, pvn_IdUbicacion)
                vlo_Conexion.SetParameter("pvn_IdPreOrdenTrabajo", OracleDbType.Int32, pvn_IdPreoOrdentrabajo)
                vlo_Conexion.SetParameter("pvn_CodUnidadSirh", OracleDbType.Int32, pvn_CodUnidadSirh)
                vlo_Conexion.SetParameter("pvc_NombrePersonaContacto", OracleDbType.Varchar2, pvc_NombrePersonaContacto)
                vlo_Conexion.SetParameter("pvc_Telefono", OracleDbType.Varchar2, pvc_Telefono)
                vlo_Conexion.SetParameter("pvc_SennasExactas", OracleDbType.Varchar2, pvc_SennasExactas)
                vlo_Conexion.SetParameter("pvc_DescripcionTrabajo", OracleDbType.Varchar2, pvc_DescripcionTrabajo)
                vlo_Conexion.SetParameter("pvc_Usuario", OracleDbType.Varchar2, pvc_Usuario)
                vlo_Conexion.SetParameter("pvn_NumEmpleado", OracleDbType.Int32, pvn_NumEmpleado)
                vlo_Conexion.SetParameter("pvn_IdCategoriaServicio", OracleDbType.Int32, pvn_IdCategoriaServicio)
                vlo_Conexion.SetParameter("pvn_IdActividad", OracleDbType.Int32, pvn_IdActividad)
                vlo_Conexion.SetParameter("pvn_IdLugarTrabajo", OracleDbType.Int32, pvn_IdLugarTrabajo)
                vlo_Conexion.SetParameter("pvn_IncluidaEnRecepcion", OracleDbType.Int32, pvn_IncluidaEnRecepcion)
                If pvn_IdUbicacionOrigen <> 0 Then
                    vlo_Conexion.SetParameter("pvn_IdUbicacionOrigen", OracleDbType.Int32, pvn_IdUbicacionOrigen)
                End If

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
        ''' Obtiene los registros de la vista V_OT_UNION_PRE_TRANS_HIST según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/04/2016 04:27:52 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtUnionPreTransHist(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_UNION_PRE_TRANS_HIST.Name, V_OT_UNION_PRE_TRANS_HIST.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OT_UNION_PRE_TRANS_HIST según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/04/2016 04:27:52 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtUnionPreTransHist(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OT_UNION_PRE_TRANS_HIST.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OT_UNION_HISTORIC_TRANSAC según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/12/2015 03:42:08 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtUnionHistoricTransac(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_UNION_HISTORIC_TRANSAC.Name, V_OT_UNION_HISTORIC_TRANSAC.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OT_UNION_HISTORIC_TRANSAC según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/12/2015 03:42:08 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtUnionHistoricTransac(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OT_UNION_HISTORIC_TRANSAC.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Permite agregar un registro en la tabla OTT_ORDEN_TRABAJO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function InsertarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOttOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOttOrdenTrabajo)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prI_OTT_ORDEN_TRABAJO"

                vlo_Conexion.SetParameter("pvn_CodUnidadSirh", OracleDbType.Int32, vlo_RegistroInterno.CodUnidadSirh)
                vlo_Conexion.SetParameter("pvc_NombrePersonaContacto", OracleDbType.Varchar2, vlo_RegistroInterno.NombrePersonaContacto)
                vlo_Conexion.SetParameter("pvc_Telefono", OracleDbType.Varchar2, vlo_RegistroInterno.Telefono)
                vlo_Conexion.SetParameter("pvc_SennasExactas", OracleDbType.Varchar2, vlo_RegistroInterno.SennasExactas)
                vlo_Conexion.SetParameter("pvc_DescripcionTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.DescripcionTrabajo)
                vlo_Conexion.SetParameter("pvn_NumeroOrden", OracleDbType.Int32, vlo_RegistroInterno.NumeroOrden)
                vlo_Conexion.SetParameter("pvn_IncluidaEnRecepcion", OracleDbType.Int32, vlo_RegistroInterno.IncluidaEnRecepcion)
                vlo_Conexion.SetParameter("pvc_Parentesco", OracleDbType.Varchar2, vlo_RegistroInterno.Parentesco)
                If vlo_RegistroInterno.IdUbicacionOrigen <> 0 Then
                    vlo_Conexion.SetParameter("pvn_IdUbicacionOrigen", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacionOrigen)
                End If
                vlo_Conexion.SetParameter("pvc_Usuario", OracleDbType.Varchar2, vlo_RegistroInterno.Usuario)
                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
                vlo_Conexion.SetParameter("pvc_IdOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.IdOrdenTrabajo)
                If vlo_RegistroInterno.IdUbicacionMadre <> 0 Then
                    vlo_Conexion.SetParameter("pvn_IdUbicacionMadre", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacionMadre)
                End If
                If vlo_RegistroInterno.IdOrdenTrabajoMadre <> String.Empty Then
                    vlo_Conexion.SetParameter("pvc_IdOrdenTrabajoMadre", OracleDbType.Varchar2, vlo_RegistroInterno.IdOrdenTrabajoMadre)
                End If
                If vlo_RegistroInterno.IdMotivoRechazo <> 0 Then
                    vlo_Conexion.SetParameter("pvn_IdMotivoRechazo", OracleDbType.Int32, vlo_RegistroInterno.IdMotivoRechazo)
                End If
                vlo_Conexion.SetParameter("pvn_Anno", OracleDbType.Int32, vlo_RegistroInterno.Anno)
                vlo_Conexion.SetParameter("pvn_Consecutivo", OracleDbType.Int32, vlo_RegistroInterno.Consecutivo)

                If vlo_RegistroInterno.ConsecutivoHija > 0 Then
                    vlo_Conexion.SetParameter("pvn_ConsecutivoHija", OracleDbType.Int32, vlo_RegistroInterno.ConsecutivoHija)
                End If

                vlo_Conexion.SetParameter("pvc_TipoOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.TipoOrdenTrabajo)
                vlo_Conexion.SetParameter("pvc_EstadoOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.EstadoOrdenTrabajo)
                vlo_Conexion.SetParameter("pvn_NumEmpleado", OracleDbType.Double, vlo_RegistroInterno.NumEmpleado)
                vlo_Conexion.SetParameter("pvn_IdCategoriaServicio", OracleDbType.Int32, vlo_RegistroInterno.IdCategoriaServicio)
                vlo_Conexion.SetParameter("pvn_IdActividad", OracleDbType.Int32, vlo_RegistroInterno.IdActividad)
                vlo_Conexion.SetParameter("pvn_IdLugarTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdLugarTrabajo)
                If vlo_RegistroInterno.IdSectorTaller <> 0 Then
                    vlo_Conexion.SetParameter("pvn_IdSectorTaller", OracleDbType.Int32, vlo_RegistroInterno.IdSectorTaller)
                End If
                vlo_Conexion.SetParameter("pvd_FechaHoraSolicita", OracleDbType.Date, vlo_RegistroInterno.FechaHoraSolicita)
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
        ''' Permite agregar un registro en la tabla OTT_ORDEN_TRABAJO
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
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
        ''' Permite borrar un registro en la tabla OTT_ORDEN_TRABAJO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function BorrarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOttOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOttOrdenTrabajo)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prD_OTT_ORDEN_TRABAJO"

                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
                vlo_Conexion.SetParameter("pvc_IdOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.IdOrdenTrabajo)

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
        ''' Permite borrar un registro en la tabla OTT_ORDEN_TRABAJO
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
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
        ''' Permite modificar un registro en la tabla OTT_ORDEN_TRABAJO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ModificarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOttOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOttOrdenTrabajo)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prU_OTT_ORDEN_TRABAJO"

                vlo_Conexion.SetParameter("pvn_CodUnidadSirh", OracleDbType.Int32, vlo_RegistroInterno.CodUnidadSirh)
                vlo_Conexion.SetParameter("pvc_NombrePersonaContacto", OracleDbType.Varchar2, vlo_RegistroInterno.NombrePersonaContacto)
                vlo_Conexion.SetParameter("pvc_Telefono", OracleDbType.Varchar2, vlo_RegistroInterno.Telefono)
                vlo_Conexion.SetParameter("pvc_SennasExactas", OracleDbType.Varchar2, vlo_RegistroInterno.SennasExactas)
                vlo_Conexion.SetParameter("pvc_DescripcionTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.DescripcionTrabajo)
                vlo_Conexion.SetParameter("pvn_NumeroOrden", OracleDbType.Int32, vlo_RegistroInterno.NumeroOrden)
                vlo_Conexion.SetParameter("pvn_IncluidaEnRecepcion", OracleDbType.Int32, vlo_RegistroInterno.IncluidaEnRecepcion)
                vlo_Conexion.SetParameter("pvc_Parentesco", OracleDbType.Varchar2, vlo_RegistroInterno.Parentesco)
                If vlo_RegistroInterno.IdUbicacionOrigen <> 0 Then
                    vlo_Conexion.SetParameter("pvn_IdUbicacionOrigen", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacionOrigen)
                End If
                vlo_Conexion.SetParameter("pvc_Usuario", OracleDbType.Varchar2, vlo_RegistroInterno.Usuario)
                vlo_Conexion.SetParameter("pvd_TimeStamp", OracleDbType.Date, vlo_RegistroInterno.TimeStamp)
                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
                vlo_Conexion.SetParameter("pvc_IdOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.IdOrdenTrabajo)
                If vlo_RegistroInterno.IdUbicacionMadre <> 0 Then
                    vlo_Conexion.SetParameter("pvn_IdUbicacionMadre", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacionMadre)
                End If
                If vlo_RegistroInterno.IdOrdenTrabajoMadre <> String.Empty And vlo_RegistroInterno.IdOrdenTrabajoMadre <> "-" Then
                    vlo_Conexion.SetParameter("pvc_IdOrdenTrabajoMadre", OracleDbType.Varchar2, vlo_RegistroInterno.IdOrdenTrabajoMadre)
                End If

                If vlo_RegistroInterno.NombreProyecto <> String.Empty And vlo_RegistroInterno.NombreProyecto <> "-" Then
                    vlo_Conexion.SetParameter("pvc_NombreProyecto", OracleDbType.Varchar2, vlo_RegistroInterno.NombreProyecto)
                End If

                If vlo_RegistroInterno.IdMotivoRechazo <> 0 Then
                    vlo_Conexion.SetParameter("pvn_IdMotivoRechazo", OracleDbType.Int32, vlo_RegistroInterno.IdMotivoRechazo)
                End If
                vlo_Conexion.SetParameter("pvn_Anno", OracleDbType.Int32, vlo_RegistroInterno.Anno)
                vlo_Conexion.SetParameter("pvn_Consecutivo", OracleDbType.Int32, vlo_RegistroInterno.Consecutivo)
                vlo_Conexion.SetParameter("pvc_TipoOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.TipoOrdenTrabajo)
                vlo_Conexion.SetParameter("pvc_EstadoOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.EstadoOrdenTrabajo)
                vlo_Conexion.SetParameter("pvn_NumEmpleado", OracleDbType.Double, vlo_RegistroInterno.NumEmpleado)
                vlo_Conexion.SetParameter("pvn_IdCategoriaServicio", OracleDbType.Int32, vlo_RegistroInterno.IdCategoriaServicio)
                vlo_Conexion.SetParameter("pvn_IdActividad", OracleDbType.Int32, vlo_RegistroInterno.IdActividad)
                vlo_Conexion.SetParameter("pvn_IdLugarTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdLugarTrabajo)
                If vlo_RegistroInterno.IdSectorTaller <> 0 Then
                    vlo_Conexion.SetParameter("pvn_IdSectorTaller", OracleDbType.Int32, vlo_RegistroInterno.IdSectorTaller)
                End If
                vlo_Conexion.SetParameter("pvd_FechaHoraSolicita", OracleDbType.Date, vlo_RegistroInterno.FechaHoraSolicita)

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
        ''' Permite modificar un registro en la tabla OTT_ORDEN_TRABAJO
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
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
        ''' Obtiene un registro de la tabla OTT_ORDEN_TRABAJO según el criterio indicado
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ObtenerRegistro(ByVal pvc_Condicion As String) As EntBase
            Dim vlo_MapeoEntidad As List(Of MapeoSimple)
            Dim vlo_DsDatos As DataSet
            Dim vlo_Resultado As New EntOttOrdenTrabajo

            Try
                vlo_DsDatos = ListarRegistros(pvc_Condicion, String.Empty, False, 0, 0)
                If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    vlo_MapeoEntidad = New List(Of MapeoSimple)
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.COD_UNIDAD_SIRH, "CodUnidadSirh"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.NOMBRE_PERSONA_CONTACTO, "NombrePersonaContacto"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.NOMBRE_PROYECTO, "NombreProyecto"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.TELEFONO, "Telefono"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.SENNAS_EXACTAS, "SennasExactas"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.DESCRIPCION_TRABAJO, "DescripcionTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.NUMERO_ORDEN, "NumeroOrden"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.INCLUIDA_EN_RECEPCION, "IncluidaEnRecepcion"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.PARENTESCO, "Parentesco"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_UBICACION_ORIGEN, "IdUbicacionOrigen"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.USUARIO, "Usuario"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.TIME_STAMP, "TimeStamp"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_UBICACION, "IdUbicacion"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, "IdOrdenTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_UBICACION_MADRE, "IdUbicacionMadre"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO_MADRE, "IdOrdenTrabajoMadre"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_MOTIVO_RECHAZO, "IdMotivoRechazo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ANNO, "Anno"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.CONSECUTIVO, "Consecutivo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.TIPO_ORDEN_TRABAJO, "TipoOrdenTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO, "EstadoOrdenTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.NUM_EMPLEADO, "NumEmpleado"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_CATEGORIA_SERVICIO, "IdCategoriaServicio"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_ACTIVIDAD, "IdActividad"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_LUGAR_TRABAJO, "IdLugarTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_SECTOR_TALLER, "IdSectorTaller"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.FECHA_HORA_SOLICITA, "FechaHoraSolicita"))

                    vlo_Resultado = vlo_DsDatos.Tables(0).Rows(0).ToEntity(Of EntOttOrdenTrabajo)(vlo_MapeoEntidad)
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
        ''' Obtiene un registro de la vista V_OTT_ORDEN_TRABAJO_LST según el criterio indicado
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>César Bermudez G</author>
        ''' <creationDate>08/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerRegistroConsulta(ByVal pvc_Condicion As String) As EntBase
            Dim vlo_MapeoEntidad As List(Of MapeoSimple)
            Dim vlo_DsDatos As DataSet
            Dim vlo_Resultado As New EntOttOrdenTrabajo

            Try
                vlo_DsDatos = ListarRegistrosLista(pvc_Condicion, String.Empty, False, 0, 0)
                If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    vlo_MapeoEntidad = New List(Of MapeoSimple)
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.COD_UNIDAD_SIRH, "CodUnidadSirh"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.NOMBRE_PERSONA_CONTACTO, "NombrePersonaContacto"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.NOMBRE_PROYECTO, "NombreProyecto"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.TELEFONO, "Telefono"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.SENNAS_EXACTAS, "SennasExactas"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.DESCRIPCION_TRABAJO, "DescripcionTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.NUMERO_ORDEN, "NumeroOrden"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.INCLUIDA_EN_RECEPCION, "IncluidaEnRecepcion"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.PARENTESCO, "Parentesco"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_UBICACION_ORIGEN, "IdUbicacionOrigen"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.USUARIO, "Usuario"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.TIME_STAMP, "TimeStamp"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_UBICACION, "IdUbicacion"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, "IdOrdenTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_UBICACION_MADRE, "IdUbicacionMadre"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO_MADRE, "IdOrdenTrabajoMadre"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_MOTIVO_RECHAZO, "IdMotivoRechazo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ANNO, "Anno"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.CONSECUTIVO, "Consecutivo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.TIPO_ORDEN_TRABAJO, "TipoOrdenTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO, "EstadoOrdenTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.NUM_EMPLEADO, "NumEmpleado"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_CATEGORIA_SERVICIO, "IdCategoriaServicio"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_ACTIVIDAD, "IdActividad"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_LUGAR_TRABAJO, "IdLugarTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.ID_SECTOR_TALLER, "IdSectorTaller"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.FECHA_HORA_SOLICITA, "FechaHoraSolicita"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.NOMBRE_TALLER, "NombreTaller"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_ORDEN_TRABAJO.COORD_ENCARGADO, "CoordEncargado"))

                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.COD_UNIDAD_SIRH, "CodUnidadSirh"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.NOMBRE_PERSONA_CONTACTO, "NombrePersonaContacto"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.NOMBRE_PROYECTO, "NombreProyecto"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.TELEFONO, "Telefono"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.SENNAS_EXACTAS, "SennasExactas"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.DESCRIPCION_TRABAJO, "DescripcionTrabajo"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.NUMERO_ORDEN, "NumeroOrden"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.INCLUIDA_EN_RECEPCION, "IncluidaEnRecepcion"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.PARENTESCO, "Parentesco"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.ID_UBICACION_ORIGEN, "IdUbicacionOrigen"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.USUARIO, "Usuario"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.TIME_STAMP, "TimeStamp"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.ID_UBICACION, "IdUbicacion"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO, "IdOrdenTrabajo"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.ID_UBICACION_MADRE, "IdUbicacionMadre"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO_MADRE, "IdOrdenTrabajoMadre"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.ID_MOTIVO_RECHAZO, "IdMotivoRechazo"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.ANNO, "Anno"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.CONSECUTIVO, "Consecutivo"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.TIPO_ORDEN_TRABAJO, "TipoOrdenTrabajo"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.ESTADO_ORDEN_TRABAJO, "EstadoOrdenTrabajo"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.NUM_EMPLEADO, "NumEmpleado"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.ID_CATEGORIA_SERVICIO, "IdCategoriaServicio"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.ID_ACTIVIDAD, "IdActividad"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.ID_LUGAR_TRABAJO, "IdLugarTrabajo"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.ID_SECTOR_TALLER, "IdSectorTaller"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.FECHA_HORA_SOLICITA, "FechaHoraSolicita"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.CATEG_REQUIERE_FICHA_TECNICA, "CategRequiereFichaTecnica"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.CONSECUTIVO_HIJA, "ConsecutivoHija"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.DESC_ACTIVIDAD, "DescActividad"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.DESC_ACTIVIDAD_MADRE, "DescActividadMadre"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.DESC_CATEGORIA_SERVICIO, "DescCategoriaServicio"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.DESC_CATEGORIA_SERVICIO_MADRE, "DescCategoriaServicioMadre"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.DESC_CONDICION_ESTADO, "DescCondicionEstado"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.DESC_ESTADO_ORDEN, "DescEstadoOrden"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.DESC_LUGAR_TRABAJO, "DescLugarTrabajo"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.DESC_TIPO_ORDEN, "DescTipoOrden"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.ES_EMERGENCIA, "EsEmergencia"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.FECHA_DESDE, "FechaDesde"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.FECHA_ENVIO_APROBACION, "FechaEnvioAprobacion"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.FECHA_HASTA, "FechaHasta"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.HISTORICO, "Historico"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.ID_PERSONAL, "IdPersonal"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.MOTIVO_NO_CONFORME, "MotivoNoConforme"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.NOMBRE_SOLICITANTE, "NombreSolicitante"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.NUMERO_ORDEN_MADRE_PDAGO, "NumeroOrdenMadrePdago"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.NUM_EMPLEADO_SUPERV_CATEG, "NumEmpleadoSupervCateg"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.NUM_PROF_ENCARGADO, "NumProfEncargado"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.PARA_IMPRESION_RECEPCION, "ParaImpresionRecepcion"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.PARTE_SENNAS_EXACTAS, "ParteSennasExactas"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.POSEE_FICHA_TECNICA, "PoseeFichaTecnica"))
                    'vlo_MapeoEntidad.Add(New MapeoSimple(V_OTT_ORDEN_TRABAJOLST.VIABILIDAD_TECNICA, "ViabilidadTecnica"))


                    vlo_Resultado = vlo_DsDatos.Tables(0).Rows(0).ToEntity(Of EntOttOrdenTrabajo)(vlo_MapeoEntidad)
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
        ''' Obtiene los registros de la vista V_OTT_ORDEN_TRABAJO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_ORDEN_TRABAJO.Name, "V_OTT_ORDEN_TRABAJO", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTT_ORDEN_TRABAJO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOttOrdenTrabajo(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTT_ORDEN_TRABAJO", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OTT_ORDEN_TRABAJOLst según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_ORDEN_TRABAJO.Name, "V_OTT_ORDEN_TRABAJOLst", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTT_ORDEN_TRABAJOLst según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOttOrdenTrabajolst(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTT_ORDEN_TRABAJOLst", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OTTH_ORDEN_TRABAJO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtthOrdenTrabajo(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_ORDEN_TRABAJO.Name, "V_OTTH_ORDEN_TRABAJO", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTTH_ORDEN_TRABAJO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/11/2015 01:43:13 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtthOrdenTrabajo(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTTH_ORDEN_TRABAJO", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Ejecuta la función FN_OT_CONSECUTIVO_ORDEN_HIJA
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>30/11/2015 08:54:17 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerFnOtConsecutivoOrdenHija(pvn_Annio As Integer, pvn_Idubicacion As Integer, pvn_Idcategoria As Integer, pvn_IdOrdenTrabajo As String) As Double
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

                vlc_Sentencia = "SELECT FN_OT_CONSECUTIVO_ORDEN_HIJA(:pvn_Annio, :pvn_Idubicacion, :pvn_Idcategoria, :pvn_IdOrdenTrabajo) AS RESULTADO FROM DUAL"
                vlo_Conexion.SetParameter("pvn_Annio", OracleDbType.Int32, pvn_Annio)
                vlo_Conexion.SetParameter("pvn_Idubicacion", OracleDbType.Int32, pvn_Idubicacion)
                vlo_Conexion.SetParameter("pvn_Idcategoria", OracleDbType.Int32, pvn_Idcategoria)
                vlo_Conexion.SetParameter("pvn_IdOrdenTrabajo", OracleDbType.Varchar2, pvn_Idcategoria)

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
        ''' Ejecuta la función FN_OT_CONSECUTIVO_ORDEN
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/09/2015 09:28:08 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerFnOtConsecutivoOrden(pvn_Annio As Integer, pvn_IdUbicacion As Integer) As Double
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

                vlc_Sentencia = "SELECT FN_OT_CONSECUTIVO_ORDEN(:pvn_Annio, :pvn_IdUbicacion) AS RESULTADO FROM DUAL"
                vlo_Conexion.SetParameter("pvn_Annio", OracleDbType.Int32, pvn_Annio)
                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, pvn_IdUbicacion)

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
        ''' Obtiene los registros de la vista V_OTT_OST_CON_FICHALST según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>27/11/2015 06:23:53 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOttOstConFichalst(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OTT_OST_CON_FICHALST.Name, V_OTT_OST_CON_FICHALST.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OTT_OST_SIN_FICHALST según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>27/11/2015 06:23:53 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOttOstSinFichalst(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OTT_OST_SIN_FICHALST.Name, V_OTT_OST_SIN_FICHALST.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Permite actualizar el estado de una Ot cuando está liquidada y registra el cambio en la trazabilidad
        ''' </summary>
        ''' <param name="pvo_DataSet"></param>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez García</author>
        ''' <creationDate>01/12/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Sub AdapterOTEstadoUpdate(pvo_DataSet As Data.DataSet)
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
                                          String.Format("UPDATE OTT_ORDEN_TRABAJO SET ESTADO_ORDEN_TRABAJO = :ESTADO_ORDEN_TRABAJO WHERE ID_UBICACION = :ID_UBICACION AND ID_ORDEN_TRABAJO = :ID_ORDEN_TRABAJO"),
                                          String.Empty)

                vlo_Conexion.AdapterAgregarParametro(":ESTADO_ORDEN_TRABAJO", "LIQ", DbType.String, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_UBICACION", "NOTA_DISCRIMINADA", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_ORDEN_TRABAJO", "USUARIO", DbType.String, ConexionOracle.TipoParametro.Update)

                vlo_Conexion.AdapterActualizar(pvo_DataSet, "OTT_ORDEN_TRABAJO")

            Catch ex As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(ex, "")
                If (vlo_Rethrow) Then
                    Throw
                End If
            End Try

        End Sub

        ''' <summary>
        ''' Obtiene los registros de la vista V_OT_RECHAZADAS según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/12/2015 03:09:58 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtRechazadas(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_RECHAZADAS.Name, V_OT_RECHAZADAS.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OT_RECHAZADAS según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/12/2015 03:09:58 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtRechazadas(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OT_RECHAZADAS.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Ejecuta la función FN_OT_CONSULTA_LUGAR_TRABAJO
        ''' </summary>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/12/2015 10:55:38 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerFnOtConsultaLugarTrabajo(pvn_PvnIdcategoria As Double, pvn_PvnIdactividad As Double, pvn_PvnIdlugartrabajo As Double) As String
            Dim vlo_Conexion As DbBase
            Dim vlb_DisposeConexion As Boolean
            Dim vlc_Sentencia As String
            Dim vlo_DsDatos As New DataSet
            Dim vlc_Resultado As String

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "SELECT FN_OT_CONSULTA_LUGAR_TRABAJO(:pvn_PvnIdcategoria, :pvn_PvnIdactividad, :pvn_PvnIdlugartrabajo) AS RESULTADO FROM DUAL"
                vlo_Conexion.SetParameter("pvn_PvnIdcategoria", OracleDbType.Double, pvn_PvnIdcategoria)
                vlo_Conexion.SetParameter("pvn_PvnIdactividad", OracleDbType.Double, pvn_PvnIdactividad)
                vlo_Conexion.SetParameter("pvn_PvnIdlugartrabajo", OracleDbType.Double, pvn_PvnIdlugartrabajo)

                vlo_Conexion.EjecutarSentencia(vlc_Sentencia, CommandType.Text, "Resultado", vlo_DsDatos)
                vlc_Resultado = CType(vlo_DsDatos.Tables("Resultado").Rows(0)("RESULTADO"), String)

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

            Return vlc_Resultado
        End Function


        ''' <summary>
        ''' Obtiene los registros de la vista V_OT_HIJAS_RECHAZADAS según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/12/2015 10:44:42 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtHijasRechazadas(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_HIJAS_RECHAZADAS.Name, V_OT_HIJAS_RECHAZADAS.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OT_HIJAS_RECHAZADAS según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/12/2015 10:44:42 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtHijasRechazadas(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OT_HIJAS_RECHAZADAS.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OT_CONSULTA_FECHA_CIERRE según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>07/12/2015 11:19:44 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtConsultaFechaCierre(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_CONSULTA_FECHA_CIERRE.Name, V_OT_CONSULTA_FECHA_CIERRE.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OT_REPORTE_GENERAL según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/12/2015 09:51:06 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtReporteGeneral(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_REPORTE_GENERAL.Name, V_OT_REPORTE_GENERAL.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OT_ALERTAS_TIEMPO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/12/2015 04:08:35 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtAlertasTiempo(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_ALERTAS_TIEMPO.Name, V_OT_ALERTAS_TIEMPO.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OT_RESUMEN_OT según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>22/01/2016 09:19:16 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtResumenOt(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_RESUMEN_OT.Name, V_OT_RESUMEN_OT.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OT_RESPUESTAS_USUARIO según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/02/2016 09:14:11 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarRespuestasSolicitante(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_RESPUESTAS_USUARIO.Name, V_OT_RESPUESTAS_USUARIO.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OT_RESPUESTA_VIABILIDAD según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>26/02/2016 09:14:11 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionRespuestaPendientes(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OT_RESPUESTAS_USUARIO.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OT_UNION_ORDEN_PREORDEN según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/04/2016 01:55:23 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtUnionOrdenPreorden(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_UNION_ORDEN_PREORDEN.Name, V_OT_UNION_ORDEN_PREORDEN.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OT_UNION_ORDEN_PREORDEN según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/04/2016 01:55:23 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtUnionOrdenPreorden(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OT_UNION_ORDEN_PREORDEN.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OTT_PARTIDAS_PERSUP según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>02/06/2016 12:07:56 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOttPartidasPresup(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OTT_PARTIDAS_PRESUP.Name, V_OTT_PARTIDAS_PRESUP.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTT_PARTIDAS_PERSUP según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>02/06/2016 12:07:56 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOttPartidasPresup(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OTT_PARTIDAS_PRESUP.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OT_REPORTE_ORDEN_TRAB según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/07/2016 02:50:35 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtReporteOrdenTrab(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_REPORTE_ORDEN_TRAB.Name, V_OT_REPORTE_ORDEN_TRAB.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OT_REPORTE_ORDEN_TRAB según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/07/2016 02:50:35 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtReporteOrdenTrab(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OT_REPORTE_ORDEN_TRAB.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OT_REVISION_SUPERVISOR según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>20/07/2016 10:57:28 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtRevisionSupervisor(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_REVISION_SUPERVISOR.Name, V_OT_REVISION_SUPERVISOR.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OT_REVISION_SUPERVISOR según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>20/07/2016 10:57:28 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtRevisionSupervisor(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OT_REVISION_SUPERVISOR.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OT_GESTION_SECTOR_TALLER según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>20/07/2016 11:27:42 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtGestionSectorTaller(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_GESTION_SECTOR_TALLER.Name, V_OT_GESTION_SECTOR_TALLER.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OT_GESTION_SECTOR_TALLER según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>20/07/2016 11:27:42 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtGestionSectorTaller(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OT_GESTION_SECTOR_TALLER.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OT_ORDEN_LIQUIDACION según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/08/2016 11:50:38 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtOrdenLiquidacion(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_ORDEN_LIQUIDACION.Name, V_OT_ORDEN_LIQUIDACION.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OT_ORDEN_LIQUIDACION según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/08/2016 11:50:38 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtOrdenLiquidacion(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OT_ORDEN_LIQUIDACION.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
