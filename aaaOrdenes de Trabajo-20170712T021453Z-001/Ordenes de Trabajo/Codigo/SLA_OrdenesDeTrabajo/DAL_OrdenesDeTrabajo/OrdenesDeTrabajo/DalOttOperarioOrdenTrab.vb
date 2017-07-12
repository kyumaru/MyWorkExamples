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
    Public Class DalOttOperarioOrdenTrab
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
        ''' Adapter para los  operarios de las ordenes de trabajo
        ''' </summary>
        ''' <param name="pvo_DataSet"></param>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>03/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Sub AdapterOttOperarioOrdenTrabajo(pvo_DataSet As Data.DataSet)
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
                                          String.Format("UPDATE OTT_OPERARIO_ORDEN_TRAB SET FECHA_EJECUTA = :FECHA_EJECUTA WHERE NUM_EMPLEADO = :NUM_EMPLEADO AND ID_SECTOR_TALLER = :ID_SECTOR_TALLER AND ID_UBICACION = :ID_UBICACION AND ID_ORDEN_TRABAJO = :ID_ORDEN_TRABAJO AND ID_ETAPA_ORDEN_TRABAJO =:ID_ETAPA_ORDEN_TRABAJO"),
                                          String.Empty)

                vlo_Conexion.AdapterAgregarParametro(":FECHA_EJECUTA", "FECHA_EJECUTA", DbType.Date, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":NUM_EMPLEADO", "NUM_EMPLEADO", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_SECTOR_TALLER", "ID_SECTOR_TALLER", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_UBICACION", "ID_UBICACION", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_ORDEN_TRABAJO", "ID_ORDEN_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_ETAPA_ORDEN_TRABAJO", "ID_ETAPA_ORDEN_TRABAJO", DbType.Int32, ConexionOracle.TipoParametro.Update)

                vlo_Conexion.AdapterActualizar(pvo_DataSet, pvo_DataSet.Tables(0).TableName)

            Catch ex As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(ex, "")
                If (vlo_Rethrow) Then
                    Throw
                End If
            End Try
        End Sub

        ''' <summary>
        ''' Adapter para los  operarios de las ordenes de trabajo
        ''' </summary>
        ''' <param name="pvo_DataSet"></param>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>11/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Sub AdapterOttOperarioOrdenTrabajoEvaluacionDisenio(pvo_DataSet As Data.DataSet)
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

                vlo_Conexion.AdapterCrear(String.Format("INSERT INTO OTT_OPERARIO_ORDEN_TRAB (NUM_EMPLEADO, ID_SECTOR_TALLER, ID_UBICACION, ID_ORDEN_TRABAJO, ID_ETAPA_ORDEN_TRABAJO, CARGO, FECHA_PROPUESTA, FECHA_EJECUTA, FECHA_DESDE, FECHA_HASTA, USUARIO) VALUES (:NUM_EMPLEADO, :ID_SECTOR_TALLER, :ID_UBICACION, :ID_ORDEN_TRABAJO, :ID_ETAPA_ORDEN_TRABAJO, :CARGO, :FECHA_PROPUESTA, :FECHA_EJECUTA, :FECHA_DESDE, :FECHA_HASTA, :USUARIO)"),
                                          String.Format("UPDATE OTT_OPERARIO_ORDEN_TRAB SET FECHA_HASTA = :FECHA_HASTA WHERE NUM_EMPLEADO = :NUM_EMPLEADO AND ID_SECTOR_TALLER = :ID_SECTOR_TALLER AND ID_UBICACION = :ID_UBICACION AND ID_ORDEN_TRABAJO = :ID_ORDEN_TRABAJO AND ID_ETAPA_ORDEN_TRABAJO =:ID_ETAPA_ORDEN_TRABAJO"),
                                          String.Format("DELETE FROM {0} WHERE {1} = :{1} AND {2} = :{2} AND {3} = :{3} AND {4} = :{4} AND {5} = :{5}", Modelo.OTT_OPERARIO_ORDEN_TRAB.Name, Modelo.OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO))

                vlo_Conexion.AdapterAgregarParametro(":NUM_EMPLEADO", "NUM_EMPLEADO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_SECTOR_TALLER", "ID_SECTOR_TALLER", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_UBICACION", "ID_UBICACION", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_ORDEN_TRABAJO", "ID_ORDEN_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_ETAPA_ORDEN_TRABAJO", "ID_ETAPA_ORDEN_TRABAJO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":CARGO", "CARGO", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":FECHA_PROPUESTA", "FECHA_PROPUESTA", DbType.Date, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":FECHA_EJECUTA", "FECHA_EJECUTA", DbType.Date, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":FECHA_DESDE", "FECHA_DESDE", DbType.Date, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":FECHA_HASTA", "FECHA_HASTA", DbType.Date, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":USUARIO", "USUARIO", DbType.String, ConexionOracle.TipoParametro.Insert)

                vlo_Conexion.AdapterAgregarParametro(":FECHA_HASTA", "FECHA_HASTA", DbType.Date, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":NUM_EMPLEADO", "NUM_EMPLEADO", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_SECTOR_TALLER", "ID_SECTOR_TALLER", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_UBICACION", "ID_UBICACION", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_ORDEN_TRABAJO", "ID_ORDEN_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_ETAPA_ORDEN_TRABAJO", "ID_ETAPA_ORDEN_TRABAJO", DbType.Int32, ConexionOracle.TipoParametro.Update)

                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO), String.Format("{0}", Modelo.OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO), DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER), String.Format("{0}", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER), DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION), String.Format("{0}", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION), DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO), String.Format("{0}", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO), DbType.String, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO), String.Format("{0}", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO), DbType.Int32, ConexionOracle.TipoParametro.Delete)

                vlo_Conexion.AdapterActualizar(pvo_DataSet, pvo_DataSet.Tables(0).TableName)

            Catch ex As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(ex, "")
                If (vlo_Rethrow) Then
                    Throw
                End If
            End Try
        End Sub

        ''' <summary>
        ''' Adapter para los  operarios de las ordenes de trabajo
        ''' </summary>
        ''' <param name="pvo_DataSet"></param>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>10/08/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Sub AdapterOttOperarioOrdenTrabajoEliminar(pvo_DataSet As Data.DataSet)
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
                                          String.Empty,
                                          String.Format("DELETE FROM {0} WHERE {1} = :{1} AND {2} = :{2}", Modelo.OTT_OPERARIO_ORDEN_TRAB.Name, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO))

                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION), String.Format("{0}", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION), DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO), String.Format("{0}", Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO), DbType.String, ConexionOracle.TipoParametro.Delete)

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
        ''' Permite agregar un registro en la tabla OTT_OPERARIO_ORDEN_TRAB
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function InsertarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOttOperarioOrdenTrab
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOttOperarioOrdenTrab)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prI_OTT_OPERARIO_ORDEN_TRAB"

                vlo_Conexion.SetParameter("pvn_NumEmpleado", OracleDbType.Double, vlo_RegistroInterno.NumEmpleado)
                vlo_Conexion.SetParameter("pvn_IdSectorTaller", OracleDbType.Int32, vlo_RegistroInterno.IdSectorTaller)
                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
                vlo_Conexion.SetParameter("pvc_IdOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.IdOrdenTrabajo)
                vlo_Conexion.SetParameter("pvn_IdEtapaOrdenTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdEtapaOrdenTrabajo)
                vlo_Conexion.SetParameter("pvc_Cargo", OracleDbType.Varchar2, vlo_RegistroInterno.Cargo)
                If vlo_RegistroInterno.FechaPropuesta <> Nothing Then
                    vlo_Conexion.SetParameter("pvd_FechaPropuesta", OracleDbType.Date, vlo_RegistroInterno.FechaPropuesta)
                End If
                If vlo_RegistroInterno.FechaEjecuta <> Nothing Then
                    vlo_Conexion.SetParameter("pvd_FechaEjecuta", OracleDbType.Date, vlo_RegistroInterno.FechaEjecuta)
                End If
                vlo_Conexion.SetParameter("pvd_FechaDesde", OracleDbType.Date, vlo_RegistroInterno.FechaDesde)
                vlo_Conexion.SetParameter("pvd_FechaHasta", OracleDbType.Date, vlo_RegistroInterno.FechaHasta)
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
        ''' Permite agregar un registro en la tabla OTT_OPERARIO_ORDEN_TRAB
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
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
        ''' Permite borrar un registro en la tabla OTT_OPERARIO_ORDEN_TRAB
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function BorrarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOttOperarioOrdenTrab
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOttOperarioOrdenTrab)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prD_OTT_OPERARIO_ORDEN_TRAB"

                vlo_Conexion.SetParameter("pvn_NumEmpleado", OracleDbType.Double, vlo_RegistroInterno.NumEmpleado)
                vlo_Conexion.SetParameter("pvn_IdSectorTaller", OracleDbType.Int32, vlo_RegistroInterno.IdSectorTaller)
                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
                vlo_Conexion.SetParameter("pvc_IdOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.IdOrdenTrabajo)
                vlo_Conexion.SetParameter("pvn_IdEtapaOrdenTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdEtapaOrdenTrabajo)

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
        ''' Permite borrar un registro en la tabla OTT_OPERARIO_ORDEN_TRAB
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
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
        ''' Permite modificar un registro en la tabla OTT_OPERARIO_ORDEN_TRAB
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ModificarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOttOperarioOrdenTrab
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOttOperarioOrdenTrab)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prU_OTT_OPERARIO_ORDEN_TRAB"

                vlo_Conexion.SetParameter("pvn_NumEmpleado", OracleDbType.Double, vlo_RegistroInterno.NumEmpleado)
                vlo_Conexion.SetParameter("pvn_IdSectorTaller", OracleDbType.Int32, vlo_RegistroInterno.IdSectorTaller)
                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
                vlo_Conexion.SetParameter("pvc_IdOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.IdOrdenTrabajo)
                vlo_Conexion.SetParameter("pvn_IdEtapaOrdenTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdEtapaOrdenTrabajo)
                vlo_Conexion.SetParameter("pvc_Cargo", OracleDbType.Varchar2, vlo_RegistroInterno.Cargo)
                If vlo_RegistroInterno.FechaPropuesta <> Nothing Then
                    vlo_Conexion.SetParameter("pvd_FechaPropuesta", OracleDbType.Date, vlo_RegistroInterno.FechaPropuesta)
                End If

                If vlo_RegistroInterno.FechaEjecuta <> Nothing Then
                    vlo_Conexion.SetParameter("pvd_FechaEjecuta", OracleDbType.Date, vlo_RegistroInterno.FechaEjecuta)
                End If

                vlo_Conexion.SetParameter("pvd_FechaDesde", OracleDbType.Date, vlo_RegistroInterno.FechaDesde)
                vlo_Conexion.SetParameter("pvd_FechaHasta", OracleDbType.Date, vlo_RegistroInterno.FechaHasta)
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
        ''' Permite modificar un registro en la tabla OTT_OPERARIO_ORDEN_TRAB
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
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
        ''' Obtiene un registro de la tabla OTT_OPERARIO_ORDEN_TRAB según el criterio indicado
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ObtenerRegistro(ByVal pvc_Condicion As String) As EntBase
            Dim vlo_MapeoEntidad As List(Of MapeoSimple)
            Dim vlo_DsDatos As DataSet
            Dim vlo_Resultado As New EntOttOperarioOrdenTrab

            Try
                vlo_DsDatos = ListarRegistros(pvc_Condicion, String.Empty, False, 0, 0)
                If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    vlo_MapeoEntidad = New List(Of MapeoSimple)
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO, "NumEmpleado"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER, "IdSectorTaller"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, "IdUbicacion"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO, "IdOrdenTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO, "IdEtapaOrdenTrabajo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_OPERARIO_ORDEN_TRAB.CARGO, "Cargo"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_OPERARIO_ORDEN_TRAB.FECHA_PROPUESTA, "FechaPropuesta"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_OPERARIO_ORDEN_TRAB.FECHA_EJECUTA, "FechaEjecuta"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_OPERARIO_ORDEN_TRAB.FECHA_DESDE, "FechaDesde"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_OPERARIO_ORDEN_TRAB.FECHA_HASTA, "FechaHasta"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_OPERARIO_ORDEN_TRAB.USUARIO, "Usuario"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_OPERARIO_ORDEN_TRAB.TIME_STAMP, "TimeStamp"))

                    vlo_Resultado = vlo_DsDatos.Tables(0).Rows(0).ToEntity(Of EntOttOperarioOrdenTrab)(vlo_MapeoEntidad)
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
        ''' Obtiene los registros de la vista V_OTT_OPERARIO_ORDEN_TRAB según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_OPERARIO_ORDEN_TRAB.Name, "V_OTT_OPERARIO_ORDEN_TRAB", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OTT_OPERARIO_ORDEN_TRABLst según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_OPERARIO_ORDEN_TRAB.Name, "V_OTT_OPERARIO_ORDEN_TRABLst", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OTTH_OPERARIO_ORDEN_TRAB según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtthOperarioOrdenTrab(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_OPERARIO_ORDEN_TRAB.Name, "V_OTTH_OPERARIO_ORDEN_TRAB", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTTH_OPERARIO_ORDEN_TRAB según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtthOperarioOrdenTrab(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTTH_OPERARIO_ORDEN_TRAB", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OTT_OPERARIO_ORDEN_TRAB según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOttOperarioOrdenTrab(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OTT_OPERARIO_ORDEN_TRAB.Name, V_OTT_OPERARIO_ORDEN_TRAB.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTT_OPERARIO_ORDEN_TRAB según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOttOperarioOrdenTrab(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OTT_OPERARIO_ORDEN_TRAB.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Obtiene los registros de la vista V_OTT_OPERARIO_ORDEN_TRABLST según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOttOperarioOrdenTrablst(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OTT_OPERARIO_ORDEN_TRABLST.Name, V_OTT_OPERARIO_ORDEN_TRABLST.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTT_OPERARIO_ORDEN_TRABLST según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOttOperarioOrdenTrablst(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OTT_OPERARIO_ORDEN_TRABLST.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
        ''' Adapter para insertar operarios en la tabla OTT_OPERARIO_ORDEN_TRAB
        ''' Almacena los recursos que se requieren para poder ejecutar la evaluación
        ''' </summary>
        ''' <param name="pvo_DataSet"></param>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez García</author>
        ''' <creationDate>03/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Sub AdapterEvaluacion(pvo_DataSet As Data.DataSet)
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

                vlo_Conexion.AdapterCrear(String.Format("INSERT INTO OTT_OPERARIO_ORDEN_TRAB (NUM_EMPLEADO, ID_SECTOR_TALLER, ID_UBICACION, ID_ORDEN_TRABAJO, ID_ETAPA_ORDEN_TRABAJO, CARGO, FECHA_PROPUESTA, FECHA_EJECUTA, FECHA_DESDE, FECHA_HASTA, USUARIO, TIME_STAMP) VALUES (:NUM_EMPLEADO, :ID_SECTOR_TALLER, :ID_UBICACION, :ID_ORDEN_TRABAJO, :ID_ETAPA_ORDEN_TRABAJO, :CARGO, :FECHA_PROPUESTA, :FECHA_EJECUTA, :FECHA_DESDE, :FECHA_HASTA, :USUARIO, :TIME_STAMP)"),
                                          String.Format("UPDATE OTT_OPERARIO_ORDEN_TRAB SET FECHA_PROPUESTA = :FECHA_PROPUESTA,FECHA_EJECUTA =:FECHA_EJECUTA,FECHA_DESDE =:FECHA_DESDE,FECHA_HASTA =:FECHA_HASTA, USUARIO =:USUARIO,TIME_STAMP =:TIME_STAMP WHERE NUM_EMPLEADO = :NUM_EMPLEADO AND ID_SECTOR_TALLER = :ID_SECTOR_TALLER AND ID_UBICACION = :ID_UBICACION AND ID_ORDEN_TRABAJO = :ID_ORDEN_TRABAJO AND ID_ETAPA_ORDEN_TRABAJO =:ID_ETAPA_ORDEN_TRABAJO"),
                                          String.Format("DELETE FROM OTT_OPERARIO_ORDEN_TRAB WHERE NUM_EMPLEADO = :NUM_EMPLEADO AND ID_SECTOR_TALLER = :ID_SECTOR_TALLER AND ID_UBICACION = :ID_UBICACION AND ID_ORDEN_TRABAJO = :ID_ORDEN_TRABAJO AND ID_ETAPA_ORDEN_TRABAJO = :ID_ETAPA_ORDEN_TRABAJO"))
                'Parámetros para INSERT
                vlo_Conexion.AdapterAgregarParametro(":NUM_EMPLEADO", "NUM_EMPLEADO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_SECTOR_TALLER", "ID_SECTOR_TALLER", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_UBICACION", "ID_UBICACION", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_ORDEN_TRABAJO", "ID_ORDEN_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_ETAPA_ORDEN_TRABAJO", "ID_ETAPA_ORDEN_TRABAJO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":CARGO", "CARGO", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":FECHA_PROPUESTA", "FECHA_PROPUESTA", DbType.Date, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":FECHA_EJECUTA", "FECHA_EJECUTA", DbType.Date, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":FECHA_DESDE", "FECHA_DESDE", DbType.Date, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":FECHA_HASTA", "FECHA_HASTA", DbType.Date, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":USUARIO", "USUARIO", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":TIME_STAMP", "TIME_STAMP", DbType.Date, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":OPERACION", "OPERACION", DbType.Date, ConexionOracle.TipoParametro.Insert)

                'Parámetros para UPDATE
                vlo_Conexion.AdapterAgregarParametro(":NUM_EMPLEADO", "NUM_EMPLEADO", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_SECTOR_TALLER", "ID_SECTOR_TALLER", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_UBICACION", "ID_UBICACION", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_ORDEN_TRABAJO", "ID_ORDEN_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_ETAPA_ORDEN_TRABAJO", "ID_ETAPA_ORDEN_TRABAJO", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":CARGO", "CARGO", DbType.String, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":FECHA_PROPUESTA", "FECHA_PROPUESTA", DbType.Date, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":FECHA_EJECUTA", "FECHA_EJECUTA", DbType.Date, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":FECHA_DESDE", "FECHA_DESDE", DbType.Date, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":FECHA_HASTA", "FECHA_HASTA", DbType.Date, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":USUARIO", "USUARIO", DbType.String, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":TIME_STAMP", "TIME_STAMP", DbType.Date, ConexionOracle.TipoParametro.Update)
                'Parámetros para DELETE
                vlo_Conexion.AdapterAgregarParametro(":NUM_EMPLEADO", "NUM_EMPLEADO", DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(":ID_SECTOR_TALLER", "ID_SECTOR_TALLER", DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(":ID_UBICACION", "ID_UBICACION", DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(":ID_ORDEN_TRABAJO", "ID_ORDEN_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(":ID_ETAPA_ORDEN_TRABAJO", "ID_ETAPA_ORDEN_TRABAJO", DbType.Int32, ConexionOracle.TipoParametro.Delete)



                vlo_Conexion.AdapterActualizar(pvo_DataSet, pvo_DataSet.Tables(0).TableName)
            Catch ex As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(ex, "")
                If (vlo_Rethrow) Then
                    Throw
                End If
            End Try
        End Sub

        ''' <summary>
        ''' Adapter para actualizar operarios en la tabla OTT_OPERARIO_ORDEN_TRAB
        ''' </summary>
        ''' <param name="pvo_DataSet"></param>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez García</author>
        ''' <creationDate>03/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Sub AdapterEvaluacionBorrar(pvo_DataSet As Data.DataSet)
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
                                          String.Empty,
                                          String.Format("DELETE FROM OTT_OPERARIO_ORDEN_TRAB WHERE NUM_EMPLEADO = :NUM_EMPLEADO AND ID_SECTOR_TALLER = :ID_SECTOR_TALLER AND ID_UBICACION = :ID_UBICACION AND ID_ORDEN_TRABAJO = :ID_ORDEN_TRABAJO AND ID_ETAPA_ORDEN_TRABAJO =:ID_ETAPA_ORDEN_TRABAJO"))

                'Parámetros para DELETE
                vlo_Conexion.AdapterAgregarParametro(":NUM_EMPLEADO", "NUM_EMPLEADO", DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(":ID_SECTOR_TALLER", "ID_SECTOR_TALLER", DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(":ID_UBICACION", "ID_UBICACION", DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(":ID_ORDEN_TRABAJO", "ID_ORDEN_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(":ID_ETAPA_ORDEN_TRABAJO", "ID_ETAPA_ORDEN_TRABAJO", DbType.Int32, ConexionOracle.TipoParametro.Delete)

                vlo_Conexion.AdapterActualizar(pvo_DataSet, pvo_DataSet.Tables(0).TableName)

            Catch ex As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(ex, "")
                If (vlo_Rethrow) Then
                    Throw
                End If
            End Try
        End Sub

#End Region
    End Class
End Namespace
