Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttOperarioOrdenTrab
#Region "Atributos"
        Private vgc_CadenaConexion As String
        Private vgo_Conexion As ConexionOracle
#End Region

#Region "Constructores"
        Public Sub New(pvc_CadenaConexion As String)
            vgc_CadenaConexion = pvc_CadenaConexion
        End Sub

        Public Sub New(pvo_Conexion As ConexionOracle)
            vgo_Conexion = pvo_Conexion
        End Sub
#End Region

#Region "Funciones"
        ''' <summary>
        ''' Permite agregar un registro en la tabla OTT_OPERARIO_ORDEN_TRAB, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttOperarioOrdenTrab) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.NumEmpleado, pvo_Registro.IdSectorTaller, pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo, pvo_Registro.IdEtapaOrdenTrabajo).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente, llave primaria repetida.")
                End If

                vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)
                vln_Resultado = vlo_DalOttOperarioOrdenTrab.InsertarRegistro(pvo_Registro)
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Método encargado de procesar los datos d
        ''' </summary>
        ''' <param name="pvc_Usuario"></param>
        ''' <param name="pvc_Clave"></param>
        ''' <param name="pvo_DsOperarioEncargado"></param>
        ''' <param name="pvo_DsOperarioColaborador"></param>
        ''' <param name="pvo_DsTiempoColaborador"></param>
        ''' <param name="pvb_GuardarEnviar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>11/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ProcesarEvaluacion(pvo_OrdenTrabajo As EntOttOrdenTrabajo, pvo_DsOperarioEncargado As Data.DataSet, pvo_DsOperarioColaborador As Data.DataSet, pvo_DsTiempoColaborador As Data.DataSet, pvb_GuardarEnviar As Boolean, pvc_CondicionOperarioEncargado As String, pvc_CondicionOperarioColaborador As String, pvc_CondicionTiempoColaborador As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab
            Dim vlo_DalOttTiempoOperario As DalOttTiempoOperario
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vln_Resultado As Integer = New Integer - 1

            Dim vlo_DsOperarioEncargado As Data.DataSet
            Dim vlo_DsOperarioColaborador As Data.DataSet
            Dim vlo_DsTiempoColaborador As Data.DataSet

            Dim vlo_DrFilaEncargado As Data.DataRow
            Dim vlo_DrFilaColaborador As Data.DataRow
            Dim vlo_DrFilaTiempo As Data.DataRow


            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)
                vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)

                If pvb_GuardarEnviar Then
                    pvo_OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_PENDIENTE
                    vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_OrdenTrabajo)
                End If


                vlo_DsOperarioEncargado = vlo_DalOttOperarioOrdenTrab.ListarRegistros(pvc_CondicionOperarioEncargado, String.Empty, False, 0, 0)
                vlo_DsOperarioColaborador = vlo_DalOttOperarioOrdenTrab.ListarRegistros(pvc_CondicionOperarioColaborador, String.Empty, False, 0, 0)
                vlo_DsTiempoColaborador = vlo_DalOttTiempoOperario.ListarRegistros(pvc_CondicionTiempoColaborador, String.Empty, False, 0, 0)

                For Each vlo_FilaTiempoColaborador In vlo_DsTiempoColaborador.Tables(0).Rows
                    vlo_FilaTiempoColaborador.Delete()
                Next

                For Each vlo_FilaOperarioColaborador In vlo_DsOperarioColaborador.Tables(0).Rows
                    vlo_FilaOperarioColaborador.Delete()
                Next

                For Each vlo_FilaOperarioEncargado In vlo_DsOperarioEncargado.Tables(0).Rows
                    vlo_FilaOperarioEncargado.Delete()
                Next

                vlo_DalOttTiempoOperario.AdapterOttTiempoOperario(vlo_DsTiempoColaborador)
                vlo_DalOttOperarioOrdenTrab.AdapterOttOperarioOrdenTrabajoEvaluacionDisenio(vlo_DsOperarioColaborador)
                vlo_DalOttOperarioOrdenTrab.AdapterOttOperarioOrdenTrabajoEvaluacionDisenio(vlo_DsOperarioEncargado)

                pvo_DsOperarioEncargado.AcceptChanges()
                pvo_DsOperarioColaborador.AcceptChanges()
                pvo_DsTiempoColaborador.AcceptChanges()

                vlo_DsOperarioEncargado = vlo_DalOttOperarioOrdenTrab.ListarRegistros(String.Format("1 = 0"), String.Empty, False, 0, 0)
                vlo_DsOperarioColaborador = vlo_DalOttOperarioOrdenTrab.ListarRegistros(String.Format("1 = 0"), String.Empty, False, 0, 0)
                vlo_DsTiempoColaborador = vlo_DalOttTiempoOperario.ListarRegistros(String.Format("1 = 0"), String.Empty, False, 0, 0)

                For Each vlo_FilaOperarioEncargado In pvo_DsOperarioEncargado.Tables(0).Rows

                    vlo_DrFilaEncargado = vlo_DsOperarioEncargado.Tables(0).NewRow
                    vlo_DrFilaEncargado.Item(vlo_DsOperarioEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO)) = vlo_FilaOperarioEncargado(Modelo.OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO)
                    vlo_DrFilaEncargado.Item(vlo_DsOperarioEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER)) = vlo_FilaOperarioEncargado(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER)
                    vlo_DrFilaEncargado.Item(vlo_DsOperarioEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION)) = vlo_FilaOperarioEncargado(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION)
                    vlo_DrFilaEncargado.Item(vlo_DsOperarioEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO)) = vlo_FilaOperarioEncargado(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO)
                    vlo_DrFilaEncargado.Item(vlo_DsOperarioEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO)) = vlo_FilaOperarioEncargado(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO)
                    vlo_DrFilaEncargado.Item(vlo_DsOperarioEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.CARGO)) = vlo_FilaOperarioEncargado(Modelo.OTT_OPERARIO_ORDEN_TRAB.CARGO)
                    vlo_DrFilaEncargado.Item(vlo_DsOperarioEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_DESDE)) = vlo_FilaOperarioEncargado(Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_DESDE)
                    vlo_DrFilaEncargado.Item(vlo_DsOperarioEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_HASTA)) = vlo_FilaOperarioEncargado(Modelo.OTT_OPERARIO_ORDEN_TRAB.FECHA_HASTA)
                    vlo_DrFilaEncargado.Item(vlo_DsOperarioEncargado.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.USUARIO)) = vlo_FilaOperarioEncargado(Modelo.OTT_OPERARIO_ORDEN_TRAB.USUARIO)

                    vlo_DsOperarioEncargado.Tables(0).Rows.Add(vlo_DrFilaEncargado)

                Next

                For Each vlo_FilaOperarioColaborador In pvo_DsOperarioColaborador.Tables(0).Rows

                    vlo_DrFilaColaborador = vlo_DsOperarioColaborador.Tables(0).NewRow
                    vlo_DrFilaColaborador.Item(vlo_DsOperarioColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO)) = vlo_FilaOperarioColaborador(Modelo.OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO)
                    vlo_DrFilaColaborador.Item(vlo_DsOperarioColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER)) = vlo_FilaOperarioColaborador(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER)
                    vlo_DrFilaColaborador.Item(vlo_DsOperarioColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION)) = vlo_FilaOperarioColaborador(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION)
                    vlo_DrFilaColaborador.Item(vlo_DsOperarioColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO)) = vlo_FilaOperarioColaborador(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO)
                    vlo_DrFilaColaborador.Item(vlo_DsOperarioColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO)) = vlo_FilaOperarioColaborador(Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO)
                    vlo_DrFilaColaborador.Item(vlo_DsOperarioColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.CARGO)) = vlo_FilaOperarioColaborador(Modelo.OTT_OPERARIO_ORDEN_TRAB.CARGO)
                    vlo_DrFilaColaborador.Item(vlo_DsOperarioColaborador.Tables(0).Columns(Modelo.OTT_OPERARIO_ORDEN_TRAB.USUARIO)) = vlo_FilaOperarioColaborador(Modelo.OTT_OPERARIO_ORDEN_TRAB.USUARIO)

                    vlo_DsOperarioColaborador.Tables(0).Rows.Add(vlo_DrFilaColaborador)
                Next

                For Each vlo_FilaTiempoColaborador In pvo_DsTiempoColaborador.Tables(0).Rows

                    vlo_DrFilaTiempo = vlo_DsTiempoColaborador.Tables(0).NewRow

                    vlo_DrFilaTiempo.Item(vlo_DsTiempoColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.NUM_EMPLEADO)) = vlo_FilaTiempoColaborador(Modelo.OTT_TIEMPO_OPERARIO.NUM_EMPLEADO)
                    vlo_DrFilaTiempo.Item(vlo_DsTiempoColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER)) = vlo_FilaTiempoColaborador(Modelo.OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER)
                    vlo_DrFilaTiempo.Item(vlo_DsTiempoColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_UBICACION)) = vlo_FilaTiempoColaborador(Modelo.OTT_TIEMPO_OPERARIO.ID_UBICACION)
                    vlo_DrFilaTiempo.Item(vlo_DsTiempoColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO)) = vlo_FilaTiempoColaborador(Modelo.OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO)
                    vlo_DrFilaTiempo.Item(vlo_DsTiempoColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO)) = vlo_FilaTiempoColaborador(Modelo.OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO)
                    vlo_DrFilaTiempo.Item(vlo_DsTiempoColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.TIEMPO)) = vlo_FilaTiempoColaborador(Modelo.OTT_TIEMPO_OPERARIO.TIEMPO)
                    vlo_DrFilaTiempo.Item(vlo_DsTiempoColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO)) = vlo_FilaTiempoColaborador(Modelo.OTT_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO)
                    vlo_DrFilaTiempo.Item(vlo_DsTiempoColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.CLASIFICACION)) = vlo_FilaTiempoColaborador(Modelo.OTT_TIEMPO_OPERARIO.CLASIFICACION)
                    vlo_DrFilaTiempo.Item(vlo_DsTiempoColaborador.Tables(0).Columns(Modelo.OTT_TIEMPO_OPERARIO.USUARIO)) = vlo_FilaTiempoColaborador(Modelo.OTT_TIEMPO_OPERARIO.USUARIO)

                    vlo_DsTiempoColaborador.Tables(0).Rows.Add(vlo_DrFilaTiempo)

                Next

                vlo_DalOttOperarioOrdenTrab.AdapterOttOperarioOrdenTrabajoEvaluacionDisenio(vlo_DsOperarioEncargado)
                vlo_DalOttOperarioOrdenTrab.AdapterOttOperarioOrdenTrabajoEvaluacionDisenio(vlo_DsOperarioColaborador)
                vlo_DalOttTiempoOperario.AdapterOttTiempoOperario(vlo_DsTiempoColaborador)

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite borrar un registro en la tabla OTT_OPERARIO_ORDEN_TRAB, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOttOperarioOrdenTrab) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.NumEmpleado, pvo_Registro.IdSectorTaller, pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo, pvo_Registro.IdEtapaOrdenTrabajo) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Hay registro asociados, no puede ser borrado.")
                End If

                vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)
                vln_Resultado = vlo_DalOttOperarioOrdenTrab.BorrarRegistro(pvo_Registro)
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite obtener un registro según su llave primaria
        ''' </summary>
        ''' <param name="pvn_NumEmpleado"></param>
        ''' <param name="pvn_IdSectorTaller">Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller</param>
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvc_IdOrdenTrabajo">Identificador único alfanumérico de la orden de trabajo</param>
        ''' <param name="pvn_IdEtapaOrdenTrabajo">Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_NumEmpleado As Double, pvn_IdSectorTaller As Integer, pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_IdEtapaOrdenTrabajo As Integer) As EntOttOperarioOrdenTrab
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)
                Return vlo_DalOttOperarioOrdenTrab.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND UPPER({6}) = '{7}' AND {8} = {9}", Modelo.OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO, pvn_NumEmpleado, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER, pvn_IdSectorTaller, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper(), Modelo.OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO, pvn_IdEtapaOrdenTrabajo))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Verifica si un registro posee datos asociados en las tablas hijas
        ''' </summary>
        ''' <param name="pvn_NumEmpleado"></param>
        ''' <param name="pvn_IdSectorTaller">Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller</param>
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvc_IdOrdenTrabajo">Identificador único alfanumérico de la orden de trabajo</param>
        ''' <param name="pvn_IdEtapaOrdenTrabajo">Llave primaria de la tabla otm_etapa_orden_trabajo que se asocia con la secuencia sq_id_etapa_orden_trabajo</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/02/2016 11:15:36 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_NumEmpleado As Double, pvn_IdSectorTaller As Integer, pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_IdEtapaOrdenTrabajo As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOttTiempoOperario As DalOttTiempoOperario

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                'valor inicial de retorno
                vlo_PoseeRegistrosAsociados = False

                'Determinar la existencia de registros asociados en la tabla OTT_TIEMPO_OPERARIO
                vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
                If vlo_DalOttTiempoOperario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND UPPER({6}) = '{7}' AND {8} = {9}", Modelo.OTT_TIEMPO_OPERARIO.NUM_EMPLEADO, pvn_NumEmpleado, Modelo.OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER, pvn_IdSectorTaller, Modelo.OTT_TIEMPO_OPERARIO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper(), Modelo.OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO, pvn_IdEtapaOrdenTrabajo)).Existe Then
                    Return True
                End If

                Return False
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' envío de correo a las y los encargados de la recepcion de la seccion de mantenimiento, para impresion
        ''' </summary>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>03/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Sub NotificacionRecepcionImpresion(pvc_Correos As String, pvc_Cuerpo As String, pvo_Registro As EntOttOrdenTrabajo)
            Dim vlo_WsGestorNotificaciones As WsrGestorNotificaciones.wsGestorNotificaciones
            Dim vlo_Sistema As WsrGestorNotificaciones.EntGNM_SISTEMA
            Dim vlo_ListaAdjunto As List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)
            Dim vlo_ListaDestinatario As List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
            Dim vlo_Notificacion As WsrGestorNotificaciones.EntGNT_NOTIFICACION
            Dim vlo_EntGNT_DESTINATARIO As WsrGestorNotificaciones.EntGNT_DESTINATARIO
            Dim vlc_Llave As String()

            vlo_WsGestorNotificaciones = New WsrGestorNotificaciones.wsGestorNotificaciones
            vlo_WsGestorNotificaciones.Timeout = -1
            vlo_WsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsGestorNotificaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_GESTOR_NOTIFICACIONES)

            Try

                vlo_Sistema = vlo_WsGestorNotificaciones.GNM_SISTEMA_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    String.Format("NOMBRE_SISTEMA = '{0}'", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_GN)))

                If vlo_Sistema IsNot Nothing AndAlso vlo_Sistema.Existe Then
                    vlo_Notificacion = New WsrGestorNotificaciones.EntGNT_NOTIFICACION()

                    vlo_Notificacion.ASUNTO = String.Format("Notificación de cambio de estado para orden de trabajo N° {0}", pvo_Registro.IdOrdenTrabajo)
                    vlo_Notificacion.CUERPO = pvc_Cuerpo
                    vlo_Notificacion.ES_HTML = 1
                    vlo_Notificacion.USUARIO_CREA = System.Environment.UserName.Trim()

                    vlo_ListaDestinatario = New List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
                    vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()

                    vlc_Llave = pvc_Correos.Split(";")

                    For i = 0 To vlc_Llave.Length - 1
                        vlo_EntGNT_DESTINATARIO.DESTINATARIO = vlc_Llave(i)
                        vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)
                        i = i + 1
                    Next

                    vlo_ListaAdjunto = New List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)

                    vlo_WsGestorNotificaciones.GNT_NOTIFICACION_Registrar(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                        vlo_Sistema,
                        vlo_Notificacion,
                        vlo_ListaAdjunto.ToArray,
                        vlo_ListaDestinatario.ToArray)

                End If
            Catch ex As Exception
                Throw New OrdenesDeTrabajoException("Error en el envío de correos.")
            End Try
        End Sub


        ''' <summary>
        ''' Almacena los recursos que se requieren para poder ejecutar una evaluación
        ''' </summary>
        ''' <param name="pvo_DsFuncionarios">set de datos con los funcionarios a ingresar</param>
        ''' <param name="pvo_DsFuncionariosEjecucion">set de datos con los funcionarios que irán a la ejecución</param>
        ''' <param name="pvn_idSectorTaller">id sector taller de la orden</param>
        ''' <param name="pvn_IdUbicacion">id ubicacion de la orden</param>
        ''' <param name="pvc_idOrden">id de la orden de trabajo</param>
        ''' <param name="pvo_fechaPropuesta">la fecha propuesta para inicio de la evaluación</param>
        ''' <param name="pvo_fechaEfectuo">Fecha en que se efectó la evaluación</param>
        ''' <param name="pvo_fechaEjecuta">Fecha propuesta para el inicio del trabajo</param>
        ''' <param name="pvn_IdUnidadEvaluacion">id unidad de tiempo estimado para la evaluación</param>
        ''' <param name="pvn_TiempoEstimadoEvaluacion">Tiempo estimado para la evaluación</param>
        ''' <param name="pvn_IdUnidadTiempoInvertido">id unidad de tiempo real</param>
        ''' <param name="pvn_tiempoRealInvertido">Tiempo real invertido en la evaluación</param>
        ''' <param name="pvn_UnidadTiempoEstimadoEjecucion">id unidad de tiempo estimado para iniciar el trabajo</param>
        ''' <param name="pvn_TiempoEstimadoEjecucion">tiempo estimado para ejecutar el trabajo</param>
        ''' <param name="pvc_UsuarioEjecuta">usuario actual que ejecuta la acción</param>
        ''' <param name="pvb_EsEjecucion">Indica si la orden se encuentra en ejecución o no</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez García</author>
        ''' <creationDate>03/02/2016 finalizado el 08/02/2016</creationDate>
        ''' <changeLog>Carlos Gómez Ondoy  -- Se agrega la modificación de la OT y el envió de correo a recepción  -- 03/03/2016</changeLog>
        Public Function GuardarEvaluacion(pvo_DsFuncionarios As System.Data.DataTable, pvo_DsFuncionariosReal As Data.DataTable, pvo_DsFuncionariosEjecucion As Data.DataTable, pvn_idSectorTaller As Integer, pvn_IdUbicacion As Integer, pvc_idOrden As String, pvo_fechaPropuesta As Date, pvo_fechaEfectuo As Date, pvo_fechaEjecuta As Date, pvn_IdUnidadEvaluacion As Integer, pvn_TiempoEstimadoEvaluacion As Integer, pvn_IdUnidadTiempoInvertido As Integer, pvn_tiempoRealInvertido As Integer, pvn_UnidadTiempoEstimadoEjecucion As Integer, pvn_TiempoEstimadoEjecucion As Integer, pvc_UsuarioEjecuta As String, pvb_EsEjecucion As Boolean, pvo_OrdenTrabajo As EntOttOrdenTrabajo, pvb_ModificarOrdenTrabajo As Boolean, pvb_NoRequiereMaterial As Boolean, pvc_ObservacionesGeneralesMaterial As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DrNuevaFila As Data.DataRow
            Dim vlc_parametrosLocales() As String
            Dim vlo_obtenerFilas() As Data.DataRow
            Dim vlo_obtenerFilasOpe() As Data.DataRow
            Dim vlo_DsSectorTaller As Data.DataSet
            Dim vlo_DalOtmSectorTaller As DalOtmSectorTaller
            Dim vlo_DalOtpParametro As DalOtpParametroUbicacion
            Dim vlo_EntOtpParametroCorreosRecepcion As EntOtpParametroUbicacion
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab
            Dim vlo_DalOttTiempoOperario As DalOttTiempoOperario
            Dim vlo_DalOttSolicitudMaterial As DalOttSolicitudMaterial
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vlo_DsOttDetalleMaterial As Data.DataSet
            Dim vlo_EntOttSolicitudMaterial As EntOttSolicitudMaterial
            Dim vlo_DalOtmAlmacenBodega As DalOtmAlmacenBodega
            Dim vlo_DalOtfInventario As DalOtfInventario
            Dim vlo_EntOtmAlmacenBodega As EntOtmAlmacenBodega
            Dim vlo_EntOtfInventario As EntOtfInventario
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vln_Resultado As Integer
            Dim vlo_DsDatosOperarioOrden As System.Data.DataSet
            Dim vlo_DsDatosOperarioOrdenNuevo As System.Data.DataSet
            Dim vlo_DsDatosTiempoOperario As System.Data.DataSet
            Dim vlo_DsDatosTiempoOperarioNuevo As System.Data.DataSet
            Dim vlo_Empleado As WsrEU_Curriculo.EntEmpleados
            Dim vlc_Cuerpo As String
            vlc_Cuerpo = String.Empty
            Dim vlb_OrdenCompleta As Boolean = True

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()
                vln_Resultado = 1



                If pvb_ModificarOrdenTrabajo Then

                    '---------------------- Se modifica la OT  ----------------------'
                    vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                    vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_OrdenTrabajo)

                    '----------------------------Se obtiene el sector taller  ----------------------------'
                    vlo_DalOtmSectorTaller = New DalOtmSectorTaller(vlo_Conexion)
                    vlo_DsSectorTaller = vlo_DalOtmSectorTaller.ListarRegistrosLista(String.Format("{0} = {1}", Modelo.V_OTM_SECTOR_TALLERLST.ID_SECTOR_TALLER, pvo_OrdenTrabajo.IdSectorTaller), String.Empty, False, 0, 0)

                    '---------------------- Se envía correo a recepcion, en caso que la OT tenga estado PARA_IMPRESION ----------------------'
                    If pvo_OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PARA_IMPRESION Then
                        vlo_DalOtpParametro = New DalOtpParametroUbicacion(vlo_Conexion)
                        'Se obtiene el parametro de correo de la recepcionista
                        vlo_EntOtpParametroCorreosRecepcion = vlo_DalOtpParametro.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.CORREOS_RECEPCION, Modelo.OTP_PARAMETRO_UBICACION.ID_UBICACION_ADMINISTRA, pvo_OrdenTrabajo.IdUbicacion))

                        vlc_Cuerpo = String.Format("Se le comunica que la orden de trabajo N° {0}, del taller o sector de {1}, a cargo de {2}, se encuentra lista para impresión.", pvo_OrdenTrabajo.IdOrdenTrabajo, vlo_DsSectorTaller.Tables(0).Rows(0)(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE), vlo_DsSectorTaller.Tables(0).Rows(0)(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE_COORDINADOR))
                        NotificacionRecepcionImpresion(vlo_EntOtpParametroCorreosRecepcion.Valor, vlc_Cuerpo, pvo_OrdenTrabajo)
                    End If

                End If


                '__________________________________________________TIEMPO OPERARIO___________________________________________________________
                'Se obtienen los operarios existentes en la base de datos sobre ésta orden de trabajo asignada a esa ubicacion
                'y al sector o taller, si no existe ninguno se procederá a agregarlos.

                vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)
                vlo_DsDatosOperarioOrden = vlo_DalOttOperarioOrdenTrab.ListarRegistrosLista(String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}'",
                                                                Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER,
                                                                pvn_idSectorTaller,
                                                                Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION,
                                                                pvn_IdUbicacion,
                                                                Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO,
                                                                pvc_idOrden), String.Empty, False, 1, 0)

                vlo_DsDatosOperarioOrdenNuevo = vlo_DalOttOperarioOrdenTrab.ListarRegistrosLista("0=1", String.Empty, False, 1, 0)

                'Se obtienen los tiempos estimados en la base de datos sobre ésta orden de trabajo asignada a esa ubicacion
                'y al sector o taller, si no existe ninguno se procederá a agregarlos.

                vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
                vlo_DsDatosTiempoOperario = vlo_DalOttTiempoOperario.ListarRegistros(String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}'",
                                                                Modelo.V_OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER,
                                                                pvn_idSectorTaller,
                                                                Modelo.V_OTT_TIEMPO_OPERARIO.ID_UBICACION,
                                                                pvn_IdUbicacion,
                                                                Modelo.V_OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO,
                                                                pvc_idOrden), String.Empty, False, 1, 0)

                'Se carga la lista de tiempos reales, si exiten, de lo contrario carga la estructura básica
                vlo_DsDatosTiempoOperarioNuevo = vlo_DalOttTiempoOperario.ListarRegistros("0=1", String.Empty, False, 1, 0)

                'Elimina del Dataset cada uno de los registros esto si es evaluacion
                For Each vlo_FilaTiempoOperario In vlo_DsDatosTiempoOperario.Tables(0).Rows
                    vlo_FilaTiempoOperario.Delete()
                Next

                vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)
                'Se llama al adapter para borrar los antiguos registros 
                vlo_DalOttTiempoOperario.AdapterEvaluacionBorrar(vlo_DsDatosTiempoOperario)


                If Not pvb_EsEjecucion Then

                    'Elimina del Dataset cada uno de los registros esto si es evaluacion
                    For Each vlo_FilaTiempoOperario In vlo_DsDatosOperarioOrden.Tables(0).Rows
                        vlo_FilaTiempoOperario.Delete()
                    Next
                    vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
                    'Se llama al adapter para borrar los antiguos registros 
                    vlo_DalOttOperarioOrdenTrab.AdapterEvaluacion(vlo_DsDatosOperarioOrden)

                Else

                    vlo_DsDatosOperarioOrden = vlo_DalOttOperarioOrdenTrab.ListarRegistrosLista(String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}' AND {6} IS NOT NULL",
                                                             Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_SECTOR_TALLER,
                                                             pvn_idSectorTaller,
                                                             Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION,
                                                             pvn_IdUbicacion,
                                                             Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO,
                                                             pvc_idOrden,
                                                              Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_EJECUTA), String.Empty, False, 0, 0)

                    'Elimina del Dataset cada uno de los registros esto si es evaluacion
                    For Each vlo_FilaTiempoOperario In vlo_DsDatosOperarioOrden.Tables(0).Rows
                        vlo_FilaTiempoOperario.Delete()
                    Next
                    vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
                    'Se llama al adapter para borrar los antiguos registros 
                    vlo_DalOttOperarioOrdenTrab.AdapterEvaluacion(vlo_DsDatosOperarioOrden)

                End If


                If pvo_DsFuncionariosReal IsNot Nothing AndAlso pvo_DsFuncionariosReal.Rows.Count > 0 Then
                    For Each vlo_fila As Data.DataRow In pvo_DsFuncionariosReal.Rows
                        vlo_Empleado = CargarFuncionario(vlo_fila(0))
                        'Se almacenan datos del tiempo real invertido en la evaluación
                        vlo_DrNuevaFila = vlo_DsDatosTiempoOperarioNuevo.Tables(0).NewRow
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.NUM_EMPLEADO) = vlo_Empleado.NUM_EMPLEADO
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER) = pvn_idSectorTaller
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_UBICACION) = pvn_IdUbicacion
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO) = pvc_idOrden
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO) = EtapasOrdenTrabajo.EVALUACION
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.TIEMPO) = pvn_tiempoRealInvertido
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO) = pvn_IdUnidadTiempoInvertido
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.CLASIFICACION) = Clasificacion.REAL
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.USUARIO) = pvc_UsuarioEjecuta
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.TIME_STAMP) = Now
                        'Se guarda la fila
                        vlo_DsDatosTiempoOperarioNuevo.Tables(0).Rows.Add(vlo_DrNuevaFila)

                        vlo_DrNuevaFila = vlo_DsDatosTiempoOperarioNuevo.Tables(0).NewRow
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.NUM_EMPLEADO) = vlo_Empleado.NUM_EMPLEADO
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER) = pvn_idSectorTaller
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_UBICACION) = pvn_IdUbicacion
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO) = pvc_idOrden
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO) = EtapasOrdenTrabajo.EVALUACION
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.TIEMPO) = pvn_TiempoEstimadoEvaluacion
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO) = pvn_IdUnidadEvaluacion
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.CLASIFICACION) = Clasificacion.ESTIMADO
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.USUARIO) = pvc_UsuarioEjecuta
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.TIME_STAMP) = Now
                        'Se guarda la fila
                        vlo_DsDatosTiempoOperarioNuevo.Tables(0).Rows.Add(vlo_DrNuevaFila)
                    Next
                Else
                    'For Each vlo_fila As Data.DataRow In pvo_DsFuncionarios.Rows
                    '    vlo_Empleado = CargarFuncionario(vlo_fila(0))
                    '    'Se almacenan datos del tiempo real o del tiempo estimado en OTT_TIEMPO_OPERARIO dependiendo de la etapa en la que se encuentre
                    '    vlo_DrNuevaFila = vlo_DsDatosTiempoOperarioNuevo.Tables(0).NewRow
                    '    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.NUM_EMPLEADO) = vlo_Empleado.NUM_EMPLEADO
                    '    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER) = pvn_idSectorTaller
                    '    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_UBICACION) = pvn_IdUbicacion
                    '    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO) = pvc_idOrden
                    '    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO) = EtapasOrdenTrabajo.EVALUACION
                    '    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.TIEMPO) = IIf(pvb_EsEjecucion, pvn_tiempoRealInvertido, pvn_TiempoEstimadoEvaluacion)
                    '    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO) = IIf(pvb_EsEjecucion, pvn_IdUnidadTiempoInvertido, pvn_IdUnidadEvaluacion)
                    '    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.CLASIFICACION) = IIf(pvb_EsEjecucion, Clasificacion.REAL, Clasificacion.ESTIMADO)
                    '    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.USUARIO) = pvc_UsuarioEjecuta
                    '    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.TIME_STAMP) = Now
                    '    'Se guarda la fila
                    '    vlo_DsDatosTiempoOperarioNuevo.Tables(0).Rows.Add(vlo_DrNuevaFila)
                    'Next
                End If

                If pvb_EsEjecucion Then
                    For Each vlo_fila As Data.DataRow In pvo_DsFuncionariosEjecucion.Rows
                        'Vlo_Fila(0): Cedula
                        'Vlo_Fila(1): Nombre Completo
                        vlo_Empleado = CargarFuncionario(vlo_fila(0))
                        'Se cargan datos para guardar el tiempo estimado en la ejecucion
                        vlo_DrNuevaFila = vlo_DsDatosTiempoOperarioNuevo.Tables(0).NewRow
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.NUM_EMPLEADO) = vlo_Empleado.NUM_EMPLEADO
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER) = pvn_idSectorTaller
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_UBICACION) = pvn_IdUbicacion
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO) = pvc_idOrden
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO) = EtapasOrdenTrabajo.EJECUCION
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.TIEMPO) = pvn_TiempoEstimadoEjecucion
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO) = pvn_UnidadTiempoEstimadoEjecucion
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.CLASIFICACION) = Clasificacion.ESTIMADO
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.USUARIO) = pvc_UsuarioEjecuta
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.TIME_STAMP) = Now
                        'Se guarda la fila
                        vlo_DsDatosTiempoOperarioNuevo.Tables(0).Rows.Add(vlo_DrNuevaFila)
                    Next

                End If


                '________________________________________________OPERARIO ORDEN TRABAJO____________________________________________________________
                'Por cada operario agregado se debe guardar un registro en OTT_OPERARIO_ORDEN_TRAB y otro en OTT_TIEMPO_OPERARIO
                'Esto con el fin de poder corroborar y controlar los tiempos estimados y los tiempos reales de una evaluación.

                If pvb_EsEjecucion Then


                    'Se recorre la lista de operarios proveída por el usuario desde la UI
                    'Esto para agregar los funcionarios que no estén presentes en la base de datos
                    For Each vlo_fila As Data.DataRow In pvo_DsFuncionariosEjecucion.Rows
                        vlo_Empleado = CargarFuncionario(vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA))
                        'Se busca si el operario existe en los datos obtenidos desde la base de datos

                        'Si no existe en la base de datos se procede a agregarlo

                        vlo_DrNuevaFila = vlo_DsDatosOperarioOrdenNuevo.Tables(0).NewRow

                        'Se obtiene el empleado por numero de cédula y se asigna al dataset
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO) = vlo_Empleado.NUM_EMPLEADO
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER) = pvn_idSectorTaller
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION) = pvn_IdUbicacion
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO) = pvc_idOrden
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO) = EtapasOrdenTrabajo.EJECUCION
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.CARGO) = Cargo.OPERARIO
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_PROPUESTA) = pvo_fechaEjecuta
                        'vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_DESDE) = pvo_fechaEfectuo
                        'vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_HASTA) = pvo_fechaEfectuo
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_EJECUTA) = pvo_fechaEfectuo
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.USUARIO) = pvc_UsuarioEjecuta
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.TIME_STAMP) = Now
                        'Se carga la fila con los datos en el dataset que irá para la tabla OTT_OPERARIO_ORDEN_TRAB
                        vlo_DsDatosOperarioOrdenNuevo.Tables(0).Rows.Add(vlo_DrNuevaFila)


                    Next

                    'Se recorre la lista de operarios proveída por el usuario desde la UI
                    'Esto para agregar los funcionarios que no estén presentes en la base de datos
                    For Each vlo_fila As Data.DataRow In pvo_DsFuncionariosReal.Rows
                        vlo_Empleado = CargarFuncionario(vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA))
                        'Se busca si el operario existe en los datos obtenidos desde la base de datos

                        'Si no existe en la base de datos se procede a agregarlo

                        vlo_DrNuevaFila = vlo_DsDatosOperarioOrdenNuevo.Tables(0).NewRow

                        'Se obtiene el empleado por numero de cédula y se asigna al dataset
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO) = vlo_Empleado.NUM_EMPLEADO
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER) = pvn_idSectorTaller
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION) = pvn_IdUbicacion
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO) = pvc_idOrden
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO) = EtapasOrdenTrabajo.EVALUACION
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.CARGO) = Cargo.OPERARIO
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_PROPUESTA) = pvo_fechaEjecuta
                        'vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_DESDE) = pvo_fechaEfectuo
                        'vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_HASTA) = pvo_fechaEfectuo
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_EJECUTA) = pvo_fechaEfectuo
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.USUARIO) = pvc_UsuarioEjecuta
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.TIME_STAMP) = Now
                        'Se carga la fila con los datos en el dataset que irá para la tabla OTT_OPERARIO_ORDEN_TRAB
                        vlo_DsDatosOperarioOrdenNuevo.Tables(0).Rows.Add(vlo_DrNuevaFila)


                    Next

                    'Se recorre la lista de la base de datos para modificar o eliminar los funcionarios no incluídos en la lista
                    If vlo_DsDatosOperarioOrdenNuevo.Tables.Count > 0 AndAlso vlo_DsDatosOperarioOrdenNuevo.Tables(0).Rows.Count > 0 Then

                        'Se insertan o modifican los cambios de los operarios con un adaptador
                        vlo_DalOttOperarioOrdenTrab.AdapterEvaluacion(vlo_DsDatosOperarioOrdenNuevo)
                        'Se agregan los registros de tiempo estimado para estos nuevos operarios
                        vlo_DalOttTiempoOperario.AdapterEvaluacion(vlo_DsDatosTiempoOperarioNuevo)


                    End If
                Else


                    'Se recorre la lista de operarios proveída por el usuario desde la UI
                    'Esto para agregar los funcionarios que no estén presentes en la base de datos
                    For Each vlo_fila As Data.DataRow In pvo_DsFuncionarios.Rows
                        vlo_Empleado = CargarFuncionario(vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA))
                        'Se busca si el operario existe en los datos obtenidos desde la base de datos
                        vlo_obtenerFilas = vlo_DsDatosOperarioOrden.Tables(0).Select(String.Format("{0} = {1}",
                                                        Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA,
                                                        vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA)))
                        'Si no existe en la base de datos se procede a agregarlo
                        If vlo_obtenerFilas.Length <= 0 Then

                            vlo_DrNuevaFila = vlo_DsDatosOperarioOrden.Tables(0).NewRow

                            'Se obtiene el empleado por numero de cédula y se asigna al dataset
                            vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO) = vlo_Empleado.NUM_EMPLEADO
                            vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER) = pvn_idSectorTaller
                            vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION) = pvn_IdUbicacion
                            vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO) = pvc_idOrden
                            vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO) = EtapasOrdenTrabajo.EVALUACION
                            vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.CARGO) = Cargo.OPERARIO
                            vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_PROPUESTA) = pvo_fechaPropuesta
                            vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.USUARIO) = pvc_UsuarioEjecuta
                            vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.TIME_STAMP) = Now
                            'Se carga la fila con los datos en el dataset que irá para la tabla OTT_OPERARIO_ORDEN_TRAB
                            vlo_DsDatosOperarioOrden.Tables(0).Rows.Add(vlo_DrNuevaFila)

                        End If

                    Next

                    'Se recorre la lista de la base de datos para modificar o eliminar los funcionarios no incluídos en la lista
                    If vlo_DsDatosOperarioOrden.Tables.Count > 0 AndAlso vlo_DsDatosOperarioOrden.Tables(0).Rows.Count > 0 Then

                        For Each vlo_fila As Data.DataRow In vlo_DsDatosOperarioOrden.Tables(0).Rows
                            vlo_Empleado = CargarFuncionario(vlo_fila(0))
                            'Si existe un elemento que esté en base de datos y no en las listas que envió el usuario debe eliminarse
                            vlo_obtenerFilas = pvo_DsFuncionarios.Select(String.Format("{0} = '{1}'",
                                                                Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA, vlo_Empleado.ID_PERSONAL))


                            If vlo_obtenerFilas.Length <= 0 Then
                                vlo_fila.Delete()

                            Else
                                'Se modifican los valores para todos los funcionarios presentes en el listado.
                                vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_PROPUESTA) = pvo_fechaPropuesta
                                vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.USUARIO) = pvc_UsuarioEjecuta
                                vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.TIME_STAMP) = Now

                            End If

                        Next

                        'Se insertan o modifican los cambios de los operarios con un adaptador
                        vlo_DalOttOperarioOrdenTrab.AdapterEvaluacion(vlo_DsDatosOperarioOrden)
                        'Se agregan los registros de tiempo estimado para estos nuevos operarios
                        'vlo_DalOttTiempoOperario.AdapterEvaluacion(vlo_DsDatosTiempoOperarioNuevo)


                    End If
                End If

                ' INICIO DE OPERACIONES PARA MATERIALES

                If pvb_EsEjecucion Then

                    If pvb_NoRequiereMaterial Then

                        vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                        vlo_DalOttSolicitudMaterial = New DalOttSolicitudMaterial(vlo_Conexion)

                        vlo_DsOttDetalleMaterial = vlo_DalOttDetalleMaterial.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OTT_DETALLE_MATERIAL.ID_UBICACION, pvn_IdUbicacion, Modelo.V_OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, pvc_idOrden), String.Empty, False, 0, 0)
                        vlo_EntOttSolicitudMaterial = vlo_DalOttSolicitudMaterial.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_SOLICITUD_MATERIAL.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_SOLICITUD_MATERIAL.ID_ORDEN_TRABAJO, pvc_idOrden))

                        If vlo_EntOttSolicitudMaterial.Existe Then

                            For Each vlo_FilaOttDetalle In vlo_DsOttDetalleMaterial.Tables(0).Rows
                                vlo_FilaOttDetalle.Delete()
                            Next

                            vlo_DalOttDetalleMaterial.AdapterOttDetalleMaterial(vlo_DsOttDetalleMaterial)

                            vlo_EntOttSolicitudMaterial.NoRequiereMaterial = 1
                            vlo_EntOttSolicitudMaterial.Observaciones = pvc_ObservacionesGeneralesMaterial
                            vlo_EntOttSolicitudMaterial.EstadoSolMaterial = EstadoSolicitudMaterial.INGRESADO_POR_COORDINADOR

                            vlo_DalOttSolicitudMaterial.ModificarRegistro(vlo_EntOttSolicitudMaterial)
                        Else

                            vlo_EntOttSolicitudMaterial = New EntOttSolicitudMaterial
                            vlo_EntOttSolicitudMaterial.IdUbicacion = pvn_IdUbicacion
                            vlo_EntOttSolicitudMaterial.IdOrdenTrabajo = pvc_idOrden
                            vlo_EntOttSolicitudMaterial.NoRequiereMaterial = 1
                            vlo_EntOttSolicitudMaterial.Observaciones = pvc_ObservacionesGeneralesMaterial
                            vlo_EntOttSolicitudMaterial.Usuario = pvc_UsuarioEjecuta
                            vlo_EntOttSolicitudMaterial.EstadoSolMaterial = EstadoSolicitudMaterial.INGRESADO_POR_COORDINADOR

                            vlo_DalOttSolicitudMaterial.InsertarRegistro(vlo_EntOttSolicitudMaterial)

                        End If

                    Else

                        If pvb_ModificarOrdenTrabajo Then

                            vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                            vlo_DsOttDetalleMaterial = vlo_DalOttDetalleMaterial.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, pvc_idOrden), String.Empty, False, 0, 0)

                            vlo_DalOtmAlmacenBodega = New DalOtmAlmacenBodega(vlo_Conexion)
                            vlo_DalOtfInventario = New DalOtfInventario(vlo_Conexion)

                            For Each vlo_FilaDetalleEnviado In vlo_DsOttDetalleMaterial.Tables(0).Rows
                                vlo_FilaDetalleEnviado(Modelo.OTT_DETALLE_MATERIAL.ESTADO) = EstadoRegistro.PENDIENTE_APROBACION

                                vlo_EntOtmAlmacenBodega = vlo_DalOtmAlmacenBodega.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion, Modelo.OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))
                                vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(
                                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOtmAlmacenBodega.IdAlmacenBodega,
                                  Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion,
                                  Modelo.OTF_INVENTARIO.ID_MATERIAL, vlo_FilaDetalleEnviado(Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL).ToString))

                                If (vlo_EntOtfInventario.CantidadDisponible - vlo_EntOtfInventario.CantidadReservada) < CType(vlo_FilaDetalleEnviado(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA).ToString, Double) Then
                                    vlb_OrdenCompleta = False
                                End If
                            Next

                            If vlb_OrdenCompleta Then
                                For Each vlo_FilaDetalleEnviado In vlo_DsOttDetalleMaterial.Tables(0).Rows

                                    vlo_EntOtmAlmacenBodega = vlo_DalOtmAlmacenBodega.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion, Modelo.OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))
                                    vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(
                                        String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOtmAlmacenBodega.IdAlmacenBodega,
                                      Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion,
                                      Modelo.OTF_INVENTARIO.ID_MATERIAL, vlo_FilaDetalleEnviado(Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL).ToString))

                                    If (vlo_EntOtfInventario.CantidadDisponible - vlo_EntOtfInventario.CantidadReservada) >= CType(vlo_FilaDetalleEnviado(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA).ToString, Double) Then
                                        vlo_FilaDetalleEnviado(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA) = CType(vlo_FilaDetalleEnviado(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA).ToString, Double)
                                    End If

                                    vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada + CType(vlo_FilaDetalleEnviado(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA).ToString, Double)

                                    vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)

                                Next
                            End If

                            vlo_DalOttDetalleMaterial.AdapterOttDetalleMaterial(vlo_DsOttDetalleMaterial)

                            vlo_DalOttSolicitudMaterial = New DalOttSolicitudMaterial(vlo_Conexion)
                            vlo_EntOttSolicitudMaterial = vlo_DalOttSolicitudMaterial.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_SOLICITUD_MATERIAL.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_SOLICITUD_MATERIAL.ID_ORDEN_TRABAJO, pvc_idOrden))
                            vlo_EntOttSolicitudMaterial.Observaciones = pvc_ObservacionesGeneralesMaterial
                            vlo_EntOttSolicitudMaterial.Usuario = pvc_UsuarioEjecuta
                            vlo_EntOttSolicitudMaterial.EstadoSolMaterial = EstadoSolicitudMaterial.APROBACION_DE_REQUISICION

                            vlo_DalOttSolicitudMaterial.ModificarRegistro(vlo_EntOttSolicitudMaterial)

                            vlo_Empleado = CargarFuncionario(pvc_UsuarioEjecuta)

                            vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                            vlo_EntOttTrazabilidadProceso.IdUbicacion = pvn_IdUbicacion
                            vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvc_idOrden
                            vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = vlo_Empleado.NUM_EMPLEADO
                            vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = EstadoOrden.PENDIENTE_APROBACION_REQUISICION
                            'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                            vlo_EntOttTrazabilidadProceso.Observaciones = String.Empty
                            vlo_EntOttTrazabilidadProceso.Usuario = pvc_UsuarioEjecuta

                            vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                            vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                        Else

                            vlo_DalOttSolicitudMaterial = New DalOttSolicitudMaterial(vlo_Conexion)
                            vlo_EntOttSolicitudMaterial = vlo_DalOttSolicitudMaterial.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_SOLICITUD_MATERIAL.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_SOLICITUD_MATERIAL.ID_ORDEN_TRABAJO, pvc_idOrden))

                            If vlo_EntOttSolicitudMaterial.Existe Then

                                vlo_EntOttSolicitudMaterial.Observaciones = pvc_ObservacionesGeneralesMaterial
                                vlo_EntOttSolicitudMaterial.Usuario = pvc_UsuarioEjecuta

                                vlo_DalOttSolicitudMaterial.ModificarRegistro(vlo_EntOttSolicitudMaterial)
                            Else

                                vlo_EntOttSolicitudMaterial.IdUbicacion = pvn_IdUbicacion
                                vlo_EntOttSolicitudMaterial.IdOrdenTrabajo = pvc_idOrden
                                vlo_EntOttSolicitudMaterial.Observaciones = pvc_ObservacionesGeneralesMaterial
                                vlo_EntOttSolicitudMaterial.EstadoSolMaterial = EstadoSolicitudMaterial.INGRESADO_POR_COORDINADOR
                                vlo_EntOttSolicitudMaterial.Usuario = pvc_UsuarioEjecuta

                                vlo_DalOttSolicitudMaterial.InsertarRegistro(vlo_EntOttSolicitudMaterial)
                            End If

                        End If

                    End If

                End If

                ' fIN DE OPERACIONES PARA MATERIALES

                vlo_Conexion.TransaccionCommit()

            Catch ex As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
                If vlo_DsDatosOperarioOrden IsNot Nothing Then
                    vlo_DsDatosOperarioOrden.Dispose()
                End If
                If vlo_DsDatosTiempoOperario IsNot Nothing Then
                    vlo_DsDatosTiempoOperario.Dispose()
                End If
                If vlo_DsDatosTiempoOperarioNuevo IsNot Nothing Then
                    vlo_DsDatosTiempoOperarioNuevo.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
        ''' </summary>
        ''' <param name="pvc_idPersonal"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>03/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function CargarFuncionario(pvc_idPersonal As String) As WsrEU_Curriculo.EntEmpleados
            Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

            vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
            vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsEU_Curriculo.Timeout = -1
            vlo_WsEU_Curriculo.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_EU_CURRICULO)

            Try
                Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    String.Format("ID_PERSONAL = '{0}' OR NUM_EMPLEADO = {0}", pvc_idPersonal))
            Catch ex As Exception
                Throw
            Finally
                If vlo_WsEU_Curriculo IsNot Nothing Then
                    vlo_WsEU_Curriculo.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Almacena en la base de datos registros de profesionales para la valoración en un proyecto
        ''' ademas envia un correo electrónico a cada uno de los funcionarios asociados para notificarles
        ''' sobre la asignación del proyecto.
        ''' </summary>
        ''' <param name="pvo_DsProfesionales"></param>
        ''' <param name="pvn_IdUbicacion"></param>
        ''' <param name="pvc_idOrden"></param>
        ''' <param name="pvn_UnidadTiempoEstimadoEvaluacion"></param>
        ''' <param name="pvn_TiempoEstimadoEvaluacion"></param>
        ''' <param name="pvc_UsuarioEjecuta"></param>
        ''' <param name="pvc_NombreProyecto"></param>
        ''' <param name="pvc_ProfesionalACargo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>11/02/2016</creationDate>
        ''' <changeLog>
        '''    <author>César Bermudez</author>
        '''    <creationDate>17/02/2016</creationDate>
        '''    <change>Se cambia el numero de parámetros enviados y se agregan las unidades de tiempo en el dataset pvo_DsProfesionales</change>
        ''' </changeLog>
        Public Function GuardarProfesionalesValoracion(pvo_DsProfesionales As System.Data.DataTable, pvn_IdUbicacion As Integer, pvc_idOrden As String, pvc_UsuarioEjecuta As String, pvc_NombreProyecto As String, pvc_ProfesionalACargo As Integer, pvb_CambiarEstado As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DrNuevaFila As Data.DataRow
            Dim vlo_obtenerFilas() As Data.DataRow
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab
            Dim vlo_DalDalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttTiempoOperario As DalOttTiempoOperario
            Dim vlo_DalOtpParametroUbicacion As AccesoDatos.Catalogos.DalOtpParametroUbicacion
            Dim vln_Resultado As Integer
            Dim vlo_DsDatosOperarioOrden As System.Data.DataSet
            Dim vlo_DsDatosTiempoOperario As System.Data.DataSet
            Dim vlo_DsDatosTiempoOperarioNuevo As System.Data.DataSet
            Dim vlo_Empleado As WsrEU_Curriculo.EntEmpleados
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlc_TiempoReal As String
            Dim vlc_CorreoAdministrador As String

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()
                vln_Resultado = 1


                '__________________________________________________TIEMPO OPERARIO___________________________________________________________
                'Se obtienen los operarios existentes en la base de datos sobre ésta orden de trabajo asignada a esa ubicacion
                'y al sector o taller, si no existe ninguno se procederá a agregarlos.

                vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New AccesoDatos.Catalogos.DalOtpParametroUbicacion(vlo_Conexion)
                vlo_DsDatosOperarioOrden = vlo_DalOttOperarioOrdenTrab.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}'",
                                                                Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION,
                                                                pvn_IdUbicacion,
                                                                Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO,
                                                                pvc_idOrden), String.Empty, False, 0, 0)

                'Se obtienen los tiempos estimados en la base de datos sobre ésta orden de trabajo asignada a esa ubicacion
                'y al sector o taller, si no existe ninguno se procederá a agregarlos.

                vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
                vlo_DsDatosTiempoOperario = vlo_DalOttTiempoOperario.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'",
                                                                Modelo.V_OTT_TIEMPO_OPERARIO.ID_UBICACION,
                                                                pvn_IdUbicacion,
                                                                Modelo.V_OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO,
                                                                pvc_idOrden,
                                                                Modelo.V_OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO,
                                                                EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA), String.Empty, False, 0, 0)

                'Se carga la estructura básica de la lista de tiempo por operario
                vlo_DsDatosTiempoOperarioNuevo = vlo_DalOttTiempoOperario.ListarRegistros(String.Format("1 = 0"), String.Empty, False, 0, 0)

                For Each vlo_fila As Data.DataRow In pvo_DsProfesionales.Rows
                    vlo_Empleado = CargarFuncionario(vlo_fila(0))
                    'Se almacenan datos es un nuevo dataset para incorporarlos en la tabla OTT_TIEMPO_OPERARIO
                    vlo_DrNuevaFila = vlo_DsDatosTiempoOperarioNuevo.Tables(0).NewRow
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.NUM_EMPLEADO) = vlo_Empleado.NUM_EMPLEADO
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER) = vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_SECTOR_TALLER)
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_UBICACION) = pvn_IdUbicacion
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO) = pvc_idOrden
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO) = EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.TIEMPO) = vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_EST)
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO) = vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_EST)
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.CLASIFICACION) = Clasificacion.ESTIMADO
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.USUARIO) = pvc_UsuarioEjecuta
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.TIME_STAMP) = Now
                    'Se guarda la fila
                    vlo_DsDatosTiempoOperarioNuevo.Tables(0).Rows.Add(vlo_DrNuevaFila)
                Next

                'Elimina del Dataset cada uno de los registros esto si es evaluacion
                For Each vlo_FilaTiempoOperario In vlo_DsDatosTiempoOperario.Tables(0).Rows
                    vlo_FilaTiempoOperario.Delete()
                Next
                'Se llama al adapter para borrar los antiguos registros 
                vlo_DalOttTiempoOperario.AdapterEvaluacionBorrar(vlo_DsDatosTiempoOperario)

                '________________________________________________OPERARIO ORDEN TRABAJO____________________________________________________________
                'Por cada operario agregado se debe guardar un registro en OTT_OPERARIO_ORDEN_TRAB y otro en OTT_TIEMPO_OPERARIO
                'Esto con el fin de poder corroborar y controlar los tiempos estimados y los tiempos reales de una evaluación.

                'Se recorre la lista de profesionales proveída por el usuario desde la UI
                'Esto para agregar los profesionales que no estén presentes en la base de datos
                For Each vlo_fila As Data.DataRow In pvo_DsProfesionales.Rows
                    vlo_Empleado = CargarFuncionario(vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA))
                    'Se busca si el operario existe en los datos obtenidos desde la base de datos
                    vlo_obtenerFilas = vlo_DsDatosOperarioOrden.Tables(0).Select(String.Format("{0} = {1}",
                                                            Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NUM_EMPLEADO,
                                                            vlo_Empleado.NUM_EMPLEADO))
                    'Si no existe en la base de datos se procede a agregarlo
                    If vlo_obtenerFilas.Length <= 0 Then

                        vlo_DrNuevaFila = vlo_DsDatosOperarioOrden.Tables(0).NewRow

                        'Se obtiene el empleado por numero de cédula y se asigna al dataset
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO) = vlo_Empleado.NUM_EMPLEADO
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER) = vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_SECTOR_TALLER)
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION) = pvn_IdUbicacion
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO) = pvc_idOrden
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO) = EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.CARGO) = Cargo.COLABORADOR
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.USUARIO) = pvc_UsuarioEjecuta
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.TIME_STAMP) = Now
                        'Se carga la fila con los datos en el dataset que irá para la tabla OTT_OPERARIO_ORDEN_TRAB
                        vlo_DsDatosOperarioOrden.Tables(0).Rows.Add(vlo_DrNuevaFila)

                    End If

                Next

                'Se recorre la lista de la base de datos para modificar o eliminar los funcionarios no incluídos en la lista
                If vlo_DsDatosOperarioOrden.Tables.Count > 0 AndAlso vlo_DsDatosOperarioOrden.Tables(0).Rows.Count > 0 Then

                    For Each vlo_fila As Data.DataRow In vlo_DsDatosOperarioOrden.Tables(0).Rows
                        vlo_Empleado = CargarFuncionario(vlo_fila(0))
                        'Si existe un elemento que esté en base de datos y no en la lista que envió el usuario debe eliminarse
                        vlo_obtenerFilas = pvo_DsProfesionales.Select(String.Format("{0} = {1}",
                                                            Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA, vlo_Empleado.ID_PERSONAL))
                        If vlo_obtenerFilas.Length <= 0 Then
                            'Si el funcionario no está presente en la lista brindada por el usuario
                            'Se asume que el usuario desde pantalla lo eliminó, y se borrará
                            Dim vlo_EntOttOperarioOrdenTrab = New EntOttOperarioOrdenTrab
                            vlo_EntOttOperarioOrdenTrab.NumEmpleado = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO)
                            vlo_EntOttOperarioOrdenTrab.IdSectorTaller = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER)
                            vlo_EntOttOperarioOrdenTrab.IdUbicacion = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION)
                            vlo_EntOttOperarioOrdenTrab.IdOrdenTrabajo = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO)
                            vlo_EntOttOperarioOrdenTrab.IdEtapaOrdenTrabajo = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO)
                            vlo_DalOttOperarioOrdenTrab.BorrarRegistro(vlo_EntOttOperarioOrdenTrab)

                        Else
                            If vlo_fila.RowState <> Data.DataRowState.Added Then
                                'Se modifican los valores para todos los funcionarios presentes en el listado.
                                vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.USUARIO) = pvc_UsuarioEjecuta
                                vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.TIME_STAMP) = Now
                            End If

                        End If
                    Next

                    'Se insertan o modifican los cambios de los operarios con un adaptador
                    vlo_DalOttOperarioOrdenTrab.AdapterEvaluacion(vlo_DsDatosOperarioOrden)

                    If pvb_CambiarEstado Then
                        'Se cambia el estado de la orden de trabajo
                        vlo_DalDalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)

                        vlo_EntOttOrdenTrabajo = vlo_DalDalOttOrdenTrabajo.ObtenerRegistro(
                            String.Format("{0} = {1} AND {2} = '{3}'",
                            Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION,
                            pvn_IdUbicacion,
                            Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO,
                            pvc_idOrden))

                        If vlo_EntOttOrdenTrabajo.Existe Then
                            vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_EVALUACION
                            vlo_DalDalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                        End If
                    End If


                    'Se agregan los registros de tiempo estimado para estos nuevos operarios
                    vlo_DalOttTiempoOperario.AdapterEvaluacion(vlo_DsDatosTiempoOperarioNuevo)


                End If

                'Enviar una notificacion a cada uno de los profesionales a cargo 

                vlo_DsDatosTiempoOperario.Clear()

                'Para obtener la descripción de el tiempo estimado para realizar la evaluación
                vlo_DsDatosTiempoOperario = vlo_DalOttTiempoOperario.ListarRegistrosLista(
                    String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'",
                        Modelo.V_OTT_TIEMPO_OPERARIO.ID_UBICACION,
                        pvn_IdUbicacion,
                        Modelo.V_OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO,
                        pvc_idOrden,
                        Modelo.V_OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO,
                        EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA), String.Empty, False, 0, 0)

                If vlo_DsDatosTiempoOperario.Tables.Count > 0 AndAlso vlo_DsDatosTiempoOperario.Tables(0).Rows.Count > 0 Then
                    vlc_TiempoReal = vlo_DsDatosTiempoOperario.Tables(0).Rows(0).Item(Modelo.V_OTT_TIEMPO_OPERARIOLST.DESC_TIEMPO_REAL)
                End If

                vlo_DsDatosTiempoOperario.Clear()
                vlo_DsDatosTiempoOperario = vlo_DalOtpParametroUbicacion.ListarRegistrosLista(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR),
                                                                                              String.Empty, False, 0, 0)
                'Obtiene el correo del administrador del sistema
                If vlo_DsDatosTiempoOperario.Tables.Count > 0 AndAlso vlo_DsDatosTiempoOperario.Tables(0).Rows.Count > 0 Then
                    vlc_CorreoAdministrador = vlo_DsDatosTiempoOperario.Tables(0).Rows(0).Item(Modelo.OTP_PARAMETRO_UBICACION.VALOR)
                End If
                vlo_Empleado = CargarFuncionario(pvc_ProfesionalACargo)
                'Se envia un correo a cada uno de los profesionales para notificarles
                EnviarCorreoNotificacion(pvo_DsProfesionales, pvc_NombreProyecto, String.Format("{0} {1} {2}", vlo_Empleado.NOMBRE, vlo_Empleado.APELLIDO1, vlo_Empleado.APELLIDO2), pvc_idOrden, vlc_TiempoReal, vlc_CorreoAdministrador)

                vlo_Conexion.TransaccionCommit()

                Return 1

            Catch ex As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
                If vlo_DsDatosOperarioOrden IsNot Nothing Then
                    vlo_DsDatosOperarioOrden.Dispose()
                End If
                If vlo_DsDatosTiempoOperario IsNot Nothing Then
                    vlo_DsDatosTiempoOperario.Dispose()
                End If
                If vlo_DsDatosTiempoOperarioNuevo IsNot Nothing Then
                    vlo_DsDatosTiempoOperarioNuevo.Dispose()
                End If
            End Try
        End Function


        ''' <summary>
        ''' Envia el correo electrónico y devuelve un valor mayor a cero si tuvo éxito o menor a cero si ocurrió un fallo
        ''' </summary>
        ''' <param name="pvo_DsProfesionales"></param>
        ''' <param name="pvc_NombreProyecto"></param>
        ''' <param name="pvc_ProfesionalACargo"></param>
        ''' <param name="pvn_TiempoEstimadoEvaluacion"></param>
        ''' <param name="pvn_UnidadTiempoEstimadoEvaluacion"></param>
        ''' <param name="pvc_idOrden"></param>
        ''' <param name="pvc_TiempoReal"></param>
        ''' <param name="pvc_CorreoAdministrador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez G</author>
        ''' <creationDate>11/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Function EnviarCorreoNotificacion(pvo_DsProfesionales As Data.DataTable, pvc_NombreProyecto As String, pvc_ProfesionalACargo As String, pvc_idOrden As String, pvc_TiempoReal As String, pvc_CorreoAdministrador As String) As Integer
            Dim vlo_WsGestorNotificaciones As WsrGestorNotificaciones.wsGestorNotificaciones
            Dim vlo_Sistema As WsrGestorNotificaciones.EntGNM_SISTEMA
            Dim vlo_ListaAdjunto As List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)
            Dim vlo_ListaDestinatario As List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
            Dim vlo_Notificacion As WsrGestorNotificaciones.EntGNT_NOTIFICACION
            Dim vlo_EntGNT_DESTINATARIO As WsrGestorNotificaciones.EntGNT_DESTINATARIO
            Dim vlo_CuerpoCorreo As String
            Dim vlo_Empleado As WsrEU_Curriculo.EntEmpleados
            Dim vln_Resultado As Integer


            vlo_WsGestorNotificaciones = New WsrGestorNotificaciones.wsGestorNotificaciones
            vlo_WsGestorNotificaciones.Timeout = -1
            vlo_WsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsGestorNotificaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_GESTOR_NOTIFICACIONES)

            Try
                vln_Resultado = -1
                vlo_Sistema = vlo_WsGestorNotificaciones.GNM_SISTEMA_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    String.Format("NOMBRE_SISTEMA = '{0}'", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_GN)))

                If vlo_Sistema IsNot Nothing AndAlso vlo_Sistema.Existe Then
                    For Each vlo_fila As Data.DataRow In pvo_DsProfesionales.Rows
                        vlo_Notificacion = New WsrGestorNotificaciones.EntGNT_NOTIFICACION()
                        'Obtiene la Cédula del funcionario actual
                        vlo_Empleado = CargarFuncionario(vlo_fila(0))
                        If Not String.IsNullOrWhiteSpace(vlo_Empleado.CORREO_INSTITUCIONAL) Then
                            '{0}: Numero de orden de trabajo
                            vlo_Notificacion.ASUNTO = String.Format("Asignacion como colaborador en proceso de viabilidad tecnica para la orden de trabajo N°{0}", pvc_idOrden)

                            '{0}: Nombre del profesional
                            '{1}: Apellido 1 del profesional
                            '{2}: Apellido 2 del profesional
                            '{3}: Nombre del proyecto
                            '{4}: Profesional encargado del proyecto
                            '{5}: Estimación de tiempo
                            '{6}: Correo del administrador del sistema

                            vlo_Notificacion.CUERPO = String.Format("<b>Señor(a):{0} {1} {2}</b><br /><br /><b>Estimado(a) señor(a): </b><br />Se le notifica que se ha asignado como profesional colaborador en el proyecto:{3}, a cargo del señor(a):{4}, para el cual se requiere su evaluación para dictar un critero conjunto de Viabilidad Técnica.<br />Para realizar dicha evaluación se ha estimado un tiempo de:{5}(S)<hr><i>Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema refiérase a la dirección: {6}</i>",
                                               vlo_Empleado.NOMBRE, vlo_Empleado.APELLIDO1, vlo_Empleado.APELLIDO2, pvc_NombreProyecto, pvc_ProfesionalACargo, pvc_TiempoReal, pvc_CorreoAdministrador)
                            vlo_Notificacion.ES_HTML = 1
                            vlo_Notificacion.USUARIO_CREA = System.Environment.UserName.Trim()

                            vlo_ListaDestinatario = New List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
                            vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
                            vlo_EntGNT_DESTINATARIO.DESTINATARIO = vlo_Empleado.CORREO_INSTITUCIONAL
                            vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)

                            vlo_ListaAdjunto = New List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)

                            vln_Resultado = vlo_WsGestorNotificaciones.GNT_NOTIFICACION_Registrar(
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                                ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                                vlo_Sistema,
                                vlo_Notificacion,
                                vlo_ListaAdjunto.ToArray,
                                vlo_ListaDestinatario.ToArray) > 0
                        End If

                    Next


                End If

                Return vln_Resultado
            Catch ex As Exception
                Throw New OrdenesDeTrabajoException("Error en el envío de correos.")
            End Try
        End Function


        ''' <summary>
        ''' Guarda los datos de la Viabilidad Técnica y actualiza datos de tiempo real
        ''' </summary>
        ''' <param name="pvo_DsEvaluadores"></param>
        ''' <param name="pvo_DsAdjuntosInsert"></param>
        ''' <param name="pvn_idSectorTaller"></param>
        ''' <param name="pvn_IdUbicacion"></param>
        ''' <param name="pvc_idOrden"></param>
        ''' <param name="pvd_FechaEfectuo"></param>
        ''' <param name="pvn_TiempoInvertidoEvaluacion"></param>
        ''' <param name="pvn_UnidadTiempoInvertido"></param>
        ''' <param name="pvb_EsViable"></param>
        ''' <param name="pvn_EstimacionPresupuestaria"></param>
        ''' <param name="pvc_TextoEnriquecido"></param>
        ''' <param name="pvc_UsuarioEjecuta"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez G</author>
        ''' <creationDate>15/02/2016</creationDate>
        ''' <changeLog>
        ''' <author>César Bermúdez G</author>
        ''' <creationDate>15/02/2016</creationDate>
        ''' <change>Modificar el comporamiento de la insersión de datos</change>
        ''' </changeLog>
        Function GuardarViabilidadTecnica(pvo_DsEvaluadores As System.Data.DataTable, pvo_DsAdjuntosInsert As System.Data.DataTable, pvn_IdUbicacion As Integer, pvc_idOrden As String, pvb_EsViable As Boolean, pvn_EstimacionPresupuestaria As Integer, pvc_TextoEnriquecido As String, pvc_UsuarioEjecuta As String, pvb_CambiarEstado As Boolean, pvo_OrdenTrabajo As EntOttOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DrNuevaFila As Data.DataRow
            Dim vlo_obtenerFilas() As Data.DataRow
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab
            Dim vlo_DalOttViabilidadTecnica As DalOttViabilidadTecnica
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttTiempoOperario As DalOttTiempoOperario
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_DalOtpParametroUbicacion As AccesoDatos.Catalogos.DalOtpParametroUbicacion
            Dim vln_Resultado As Integer
            Dim vlo_DsDatosOperarioOrden As System.Data.DataSet
            Dim vlo_DsDatosTiempoOperario As System.Data.DataSet
            Dim vlo_DsDatosTiempoOperarioNuevo As System.Data.DataSet
            Dim vlo_DsActuales As System.Data.DataSet
            Dim vlo_DsNuevos As System.Data.DataSet
            Dim vlo_Empleado As WsrEU_Curriculo.EntEmpleados
            Dim vlo_EntOttViabilidadTecnica As EntOttViabilidadTecnica
            Dim vlc_TiempoReal As String
            Dim vlc_CorreoAdministrador As String

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()
                vln_Resultado = 1


                '__________________________________________________TIEMPO OPERARIO___________________________________________________________
                'Se obtienen los operarios existentes en la base de datos sobre ésta orden de trabajo asignada a esa ubicacion
                'y al sector o taller, si no existe ninguno se procederá a agregarlos.

                vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New AccesoDatos.Catalogos.DalOtpParametroUbicacion(vlo_Conexion)
                vlo_DsDatosOperarioOrden = vlo_DalOttOperarioOrdenTrab.ListarRegistrosLista(String.Format("{0} = {1} AND {2} = '{3}'",
                                                                Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION,
                                                                pvn_IdUbicacion,
                                                                Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO,
                                                                pvc_idOrden), String.Empty, False, 0, 0)

                'Se obtienen los tiempos estimados en la base de datos sobre ésta orden de trabajo asignada a esa ubicacion
                'y al sector o taller, si no existe ninguno se procederá a agregarlos.

                vlo_DalOttTiempoOperario = New DalOttTiempoOperario(vlo_Conexion)
                vlo_DsDatosTiempoOperario = vlo_DalOttTiempoOperario.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'",
                                                                Modelo.V_OTT_TIEMPO_OPERARIO.ID_UBICACION,
                                                                pvn_IdUbicacion,
                                                                Modelo.V_OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO,
                                                                pvc_idOrden,
                                                                Modelo.V_OTT_TIEMPO_OPERARIO.CLASIFICACION,
                                                                Clasificacion.REAL), String.Empty, False, 0, 0)

                'Se carga la estructura básica de la lista de tiempo por operario
                vlo_DsDatosTiempoOperarioNuevo = vlo_DalOttTiempoOperario.ListarRegistros(String.Format("1 = 0"), String.Empty, False, 0, 0)

                Dim vlo_DalOtfOperarioArea As DalOtfOperarioArea
                Dim vlo_EntOtfOperarioAreaTiempo As EntOtfOperarioArea

                vlo_DalOtfOperarioArea = New DalOtfOperarioArea(vlo_Conexion)

                For Each vlo_fila As Data.DataRow In pvo_DsEvaluadores.Rows

                    vlo_Empleado = CargarFuncionario(vlo_fila(0))
                    vlo_EntOtfOperarioAreaTiempo = vlo_DalOtfOperarioArea.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTF_OPERARIO_AREA.NUM_EMPLEADO, vlo_Empleado.NUM_EMPLEADO))
                    'Se almacenan datos es un nuevo dataset para incorporarlos en la tabla OTT_TIEMPO_OPERARIO
                    vlo_DrNuevaFila = vlo_DsDatosTiempoOperarioNuevo.Tables(0).NewRow
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.NUM_EMPLEADO) = vlo_Empleado.NUM_EMPLEADO
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER) = vlo_EntOtfOperarioAreaTiempo.IdSectorTaller
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_UBICACION) = pvn_IdUbicacion
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO) = pvc_idOrden
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO) = EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.TIEMPO) = vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_REAL)
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO) = vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UNIDAD_TIEMPO_REAL)
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.CLASIFICACION) = Clasificacion.REAL
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.USUARIO) = pvc_UsuarioEjecuta
                    vlo_DrNuevaFila.Item(Modelo.V_OTT_TIEMPO_OPERARIO.TIME_STAMP) = Now
                    'Se guarda la fila
                    vlo_DsDatosTiempoOperarioNuevo.Tables(0).Rows.Add(vlo_DrNuevaFila)

                Next

                'Elimina del Dataset cada uno de los registros esto si es evaluacion
                For Each vlo_FilaTiempoOperario In vlo_DsDatosTiempoOperario.Tables(0).Rows
                    vlo_FilaTiempoOperario.Delete()
                Next
                'Se llama al adapter para borrar los antiguos registros 
                vlo_DalOttTiempoOperario.AdapterEvaluacionBorrar(vlo_DsDatosTiempoOperario)


                '________________________________________________OPERARIO ORDEN TRABAJO____________________________________________________________
                'Por cada operario agregado se debe guardar un registro en OTT_OPERARIO_ORDEN_TRAB y otro en OTT_TIEMPO_OPERARIO
                'Esto con el fin de poder corroborar y controlar los tiempos estimados y los tiempos reales de una evaluación.




                Dim vlo_EntOtfOperarioAreaOperario As EntOtfOperarioArea

                'Se recorre la lista de profesionales proveída por el usuario desde la UI
                'Esto para agregar los profesionales que no estén presentes en la base de datos
                For Each vlo_fila As Data.DataRow In pvo_DsEvaluadores.Rows
                    vlo_Empleado = CargarFuncionario(vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA))
                    'Se busca si el operario existe en los datos obtenidos desde la base de datos
                    vlo_obtenerFilas = vlo_DsDatosOperarioOrden.Tables(0).Select(String.Format("{0} = {1}",
                                                            Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NUM_EMPLEADO,
                                                            vlo_Empleado.NUM_EMPLEADO))

                    vlo_EntOtfOperarioAreaOperario = vlo_DalOtfOperarioArea.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTF_OPERARIO_AREA.NUM_EMPLEADO, vlo_Empleado.NUM_EMPLEADO))

                    'Si no existe en la base de datos se procede a agregarlo
                    If vlo_obtenerFilas.Length <= 0 Then

                        vlo_DrNuevaFila = vlo_DsDatosOperarioOrden.Tables(0).NewRow

                        'Se obtiene el empleado por numero de cédula y se asigna al dataset
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO) = vlo_Empleado.NUM_EMPLEADO
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER) = vlo_EntOtfOperarioAreaOperario.IdSectorTaller
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION) = pvn_IdUbicacion
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO) = pvc_idOrden
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO) = EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_PROPUESTA) = vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_EJECUTA)
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_EJECUTA) = vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_EJECUTA)
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.CARGO) = Cargo.COLABORADOR
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.USUARIO) = pvc_UsuarioEjecuta
                        vlo_DrNuevaFila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.TIME_STAMP) = Now
                        'Se carga la fila con los datos en el dataset que irá para la tabla OTT_OPERARIO_ORDEN_TRAB
                        vlo_DsDatosOperarioOrden.Tables(0).Rows.Add(vlo_DrNuevaFila)

                    End If

                Next

                'Se recorre la lista de la base de datos para modificar o eliminar los funcionarios no incluídos en la lista
                If vlo_DsDatosOperarioOrden.Tables.Count > 0 AndAlso vlo_DsDatosOperarioOrden.Tables(0).Rows.Count > 0 Then



                    For Each vlo_fila As Data.DataRow In vlo_DsDatosOperarioOrden.Tables(0).Rows
                        vlo_Empleado = CargarFuncionario(vlo_fila(0))
                        'Si existe un elemento que esté en base de datos y no en la lista que envió el usuario debe eliminarse
                        vlo_obtenerFilas = pvo_DsEvaluadores.Select(String.Format("{0} = {1}",
                                                            Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA, vlo_Empleado.ID_PERSONAL))
                        If vlo_obtenerFilas.Length <= 0 Then
                            'Si el elemento tenia un tiempo estimado no se debe eliminar, 
                            'Si por el contrario tenía solamente un tiempo real debe ser eliminado

                            vlo_obtenerFilas = vlo_DsDatosOperarioOrden.Tables(0).Select(String.Format("{0} = {1} AND {2} = '{3}'",
                                                            Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA, vlo_Empleado.ID_PERSONAL,
                                                            Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.TIEMPO_EST_CLAS, Clasificacion.ESTIMADO))

                            If vlo_obtenerFilas.Length <= 0 Then
                                Dim vlo_EntOttOperarioOrdenTrab = New EntOttOperarioOrdenTrab
                                vlo_EntOttOperarioOrdenTrab.NumEmpleado = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.NUM_EMPLEADO)
                                vlo_EntOttOperarioOrdenTrab.IdSectorTaller = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER)
                                vlo_EntOttOperarioOrdenTrab.IdUbicacion = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION)
                                vlo_EntOttOperarioOrdenTrab.IdOrdenTrabajo = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO)
                                vlo_EntOttOperarioOrdenTrab.IdEtapaOrdenTrabajo = vlo_fila(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO)
                                vlo_DalOttOperarioOrdenTrab.BorrarRegistro(vlo_EntOttOperarioOrdenTrab)
                            Else
                                'Se modifican los valores para todos los funcionarios presentes en el listado.
                                vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_EJECUTA) = vlo_obtenerFilas(0).Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_EJECUTA)
                                vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.USUARIO) = pvc_UsuarioEjecuta
                                vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.TIME_STAMP) = Now
                            End If


                        Else
                            'Se modifican los valores para todos los funcionarios presentes en el listado.
                            vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.FECHA_EJECUTA) = vlo_obtenerFilas(0).Item(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_EJECUTA)
                            vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.USUARIO) = pvc_UsuarioEjecuta
                            vlo_fila.Item(Modelo.V_OTT_OPERARIO_ORDEN_TRAB.TIME_STAMP) = Now

                        End If

                    Next

                    '________________________________________________OTT VIABILIDAD TECNICA____________________________________________________________

                    'Se inserta un objeto con la viabilidad técnica digitada por el usuario
                    vlo_DalOttViabilidadTecnica = New DalOttViabilidadTecnica(vlo_Conexion)

                    vlo_EntOttViabilidadTecnica = vlo_DalOttViabilidadTecnica.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'",
                                  Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion,
                                  Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_idOrden))
                    If vlo_EntOttViabilidadTecnica.Existe Then
                        vlo_EntOttViabilidadTecnica.Viabilidad = IIf(pvb_EsViable, 1, 0)
                        vlo_EntOttViabilidadTecnica.EstimacionPresupuestaria = pvn_EstimacionPresupuestaria
                        vlo_EntOttViabilidadTecnica.Detalle = pvc_TextoEnriquecido
                        vlo_EntOttViabilidadTecnica.Usuario = pvc_UsuarioEjecuta
                        vlo_DalOttViabilidadTecnica.ModificarRegistro(vlo_EntOttViabilidadTecnica)
                    Else
                        vlo_EntOttViabilidadTecnica = New EntOttViabilidadTecnica

                        vlo_EntOttViabilidadTecnica.IdOrdenTrabajo = pvc_idOrden
                        vlo_EntOttViabilidadTecnica.IdUbicacion = pvn_IdUbicacion
                        vlo_EntOttViabilidadTecnica.Viabilidad = IIf(pvb_EsViable, 1, 0)
                        vlo_EntOttViabilidadTecnica.EstimacionPresupuestaria = pvn_EstimacionPresupuestaria
                        vlo_EntOttViabilidadTecnica.Detalle = pvc_TextoEnriquecido
                        vlo_EntOttViabilidadTecnica.Usuario = pvc_UsuarioEjecuta

                        vlo_DalOttViabilidadTecnica.InsertarRegistro(vlo_EntOttViabilidadTecnica)
                    End If


                    If pvb_CambiarEstado Then
                        vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)

                        pvo_OrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_REVISION_COORD

                        vlo_DalOttOrdenTrabajo.ModificarRegistro(pvo_OrdenTrabajo)

                    End If




                    '________________________________________________OTT ARCHIVOS ADJUNTOS____________________________________________________________

                    vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                    'Se obtienen los documentos adjuntos actuales
                    vlo_DsActuales = vlo_DalOttAdjuntoOrdenTrabajo.ListarRegistros(
                        String.Format("{0} = {1} AND {2} = {3} AND {4} = '{5}'",
                                  Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION, pvn_IdUbicacion,
                                  Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA,
                                  Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_idOrden),
                        String.Empty,
                        False,
                        0,
                        0)




                    vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                    vlo_DsNuevos = vlo_DalOttAdjuntoOrdenTrabajo.ListarRegistros(String.Format("1 = 0"), String.Empty, False, 0, 0)


                    For Each vlo_Fila As Data.DataRow In pvo_DsAdjuntosInsert.Rows
                        vlo_DrNuevaFila = vlo_DsNuevos.Tables(0).NewRow
                        vlo_DrNuevaFila.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION)) = pvn_IdUbicacion
                        vlo_DrNuevaFila.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO)) = pvc_idOrden
                        vlo_DrNuevaFila.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO)) = vlo_Fila.Item(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO)
                        vlo_DrNuevaFila.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO)) = EtapasOrdenTrabajo.ANALISIS_VIABILIDAD_TECNICA
                        vlo_DrNuevaFila.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.DESCRIPCION)) = vlo_Fila.Item(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.DESCRIPCION)
                        vlo_DrNuevaFila.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)) = vlo_Fila.Item(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO)
                        vlo_DrNuevaFila.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO)) = vlo_Fila.Item(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO)
                        vlo_DrNuevaFila.Item(vlo_DsNuevos.Tables(0).Columns(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.USUARIO)) = vlo_Fila.Item(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.USUARIO)
                        vlo_DsNuevos.Tables(0).Rows.Add(vlo_DrNuevaFila)
                    Next



                    For Each vlo_Fila In vlo_DsActuales.Tables(0).Rows
                        vlo_Fila.Delete()
                    Next
                    'Se eliminan los registros y se vuelven a insertar, esto para evitar violacion de llaves
                    vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                    vlo_DalOttAdjuntoOrdenTrabajo.AdapterOtTAdjunto(vlo_DsActuales)

                    vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                    'Se agregan los archivos adjuntos
                    vlo_DalOttAdjuntoOrdenTrabajo.AdapterOtTAdjunto(vlo_DsNuevos)

                    'Se insertan o modifican los cambios de los operarios con un adaptador
                    vlo_DalOttOperarioOrdenTrab.AdapterEvaluacion(vlo_DsDatosOperarioOrden)
                    'Se agregan los registros de tiempo estimado para estos nuevos operarios
                    vlo_DalOttTiempoOperario.AdapterEvaluacion(vlo_DsDatosTiempoOperarioNuevo)
                    vlo_Conexion.TransaccionCommit()
                    Return vln_Resultado
                End If

            Catch ex As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
                If vlo_DsDatosOperarioOrden IsNot Nothing Then
                    vlo_DsDatosOperarioOrden.Dispose()
                End If
                If vlo_DsDatosTiempoOperario IsNot Nothing Then
                    vlo_DsDatosTiempoOperario.Dispose()
                End If
                If vlo_DsDatosTiempoOperarioNuevo IsNot Nothing Then
                    vlo_DsDatosTiempoOperarioNuevo.Dispose()
                End If
                If vlo_DsActuales IsNot Nothing Then
                    vlo_DsActuales.Dispose()
                End If
                If vlo_DsNuevos IsNot Nothing Then
                    vlo_DsNuevos.Dispose()
                End If
            End Try
        End Function

#End Region



    End Class
End Namespace
