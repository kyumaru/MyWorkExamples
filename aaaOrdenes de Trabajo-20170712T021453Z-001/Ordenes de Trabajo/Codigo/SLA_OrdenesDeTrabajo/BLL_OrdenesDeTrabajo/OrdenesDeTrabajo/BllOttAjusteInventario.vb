Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttAjusteInventario
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
        ''' Permite agregar un registro en la tabla OTT_AJUSTE_INVENTARIO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttAjusteInventario) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAjusteInventario As DalOttAjusteInventario
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.Anno, pvo_Registro.ConsecutivoAjuste).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
                End If

                vlo_DalOttAjusteInventario = New DalOttAjusteInventario(vlo_Conexion)
                vln_Resultado = vlo_DalOttAjusteInventario.InsertarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTT_AJUSTE_INVENTARIO, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOttAjusteInventario) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAjusteInventario As DalOttAjusteInventario
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdUbicacion, pvo_Registro.Anno, pvo_Registro.ConsecutivoAjuste) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Hay registro asociados")
                End If

                vlo_DalOttAjusteInventario = New DalOttAjusteInventario(vlo_Conexion)
                vln_Resultado = vlo_DalOttAjusteInventario.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvn_Anno">Año</param>
        ''' <param name="pvn_ConsecutivoAjuste">Consecutivo anual del ajuste.</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvn_Anno As Integer, pvn_ConsecutivoAjuste As Integer) As EntOttAjusteInventario
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAjusteInventario As DalOttAjusteInventario

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttAjusteInventario = New DalOttAjusteInventario(vlo_Conexion)
                Return vlo_DalOttAjusteInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTT_AJUSTE_INVENTARIO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_AJUSTE_INVENTARIO.ANNO, pvn_Anno, Modelo.OTT_AJUSTE_INVENTARIO.CONSECUTIVO_AJUSTE, pvn_ConsecutivoAjuste))
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
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvn_Anno">Año</param>
        ''' <param name="pvn_ConsecutivoAjuste">Consecutivo anual del ajuste.</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>24/01/2017 02:01:22 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdUbicacion As Integer, pvn_Anno As Integer, pvn_ConsecutivoAjuste As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOttDetalleAjuste As DalOttDetalleAjuste

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

                'Determinar la existencia de registros asociados en la tabla OTT_DETALLE_AJUSTE
                vlo_DalOttDetalleAjuste = New DalOttDetalleAjuste(vlo_Conexion)
                If vlo_DalOttDetalleAjuste.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTT_DETALLE_AJUSTE.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_DETALLE_AJUSTE.ANNO, pvn_Anno, Modelo.OTT_DETALLE_AJUSTE.CONSECUTIVO_AJUSTE, pvn_ConsecutivoAjuste)).Existe Then
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

        Public Function CrearAjusteIndividual(pvn_IdUbicacion As Integer, pvc_IdBodega As Integer, pvn_IdMaterial As Integer, pvc_CantidadAjuste As String, pvc_Observaciones As String, pvc_Tipo As String, pvc_UsuarioCrea As String, pvb_Finalizar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAjusteInventario As DalOttAjusteInventario
            Dim vlo_DalOttDetalleAjuste As DalOttDetalleAjuste
            Dim vlo_DalOtlTrazabilidadAjuste As DalOtlTrazabilidadAjuste
            Dim vlo_EntOtlTrazabilidadAjuste As EntOtlTrazabilidadAjuste
            Dim vlo_EntOttAjusteInventario As EntOttAjusteInventario
            Dim vlo_DsDetalleAjuste As Data.DataSet
            Dim vlc_Resultado As Integer = -1
            Dim vlo_DrFilaDetalleAjuste As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttAjusteInventario = New DalOttAjusteInventario(vlo_Conexion)
                vlo_DalOttDetalleAjuste = New DalOttDetalleAjuste(vlo_Conexion)
                vlo_DalOtlTrazabilidadAjuste = New DalOtlTrazabilidadAjuste(vlo_Conexion)

                'Se ingresa el ajuste del inventario
                vlo_EntOttAjusteInventario = New EntOttAjusteInventario
                vlo_EntOttAjusteInventario.IdUbicacion = pvn_IdUbicacion
                vlo_EntOttAjusteInventario.Anno = DateTime.Now.Year
                vlo_EntOttAjusteInventario.ConsecutivoAjuste = vlo_DalOttAjusteInventario.ObtenerFcOtConsecutivoAjuste(vlo_EntOttAjusteInventario.Anno, vlo_EntOttAjusteInventario.IdUbicacion) + 1
                vlo_EntOttAjusteInventario.IdAlmacenBodega = pvc_IdBodega
                If pvb_Finalizar Then
                    vlo_EntOttAjusteInventario.EstadoAjuste = EstadoAjuste.APROBACION_SUPERVISOR
                Else
                    vlo_EntOttAjusteInventario.EstadoAjuste = EstadoAjuste.CREADO
                End If
                vlo_EntOttAjusteInventario.TipoAjuste = TipoAjuste.INDIVIDUAL
                vlo_EntOttAjusteInventario.FechaRegistroSolicitud = DateTime.Now
                vlo_EntOttAjusteInventario.Observaciones = pvc_Observaciones
                vlo_EntOttAjusteInventario.Usuario = pvc_UsuarioCrea


                vlo_DalOttAjusteInventario.InsertarRegistro(vlo_EntOttAjusteInventario)

                'Se ingresa el detalle del ajuste
                vlo_DsDetalleAjuste = vlo_DalOttDetalleAjuste.ListarRegistros("1 = 0", String.Empty, False, 0, 0)

                vlo_DrFilaDetalleAjuste = vlo_DsDetalleAjuste.Tables(0).NewRow

                vlo_DrFilaDetalleAjuste.Item(Modelo.OTT_DETALLE_AJUSTE.ID_UBICACION) = vlo_EntOttAjusteInventario.IdUbicacion
                vlo_DrFilaDetalleAjuste.Item(Modelo.OTT_DETALLE_AJUSTE.ANNO) = DateTime.Now.Year
                vlo_DrFilaDetalleAjuste.Item(Modelo.OTT_DETALLE_AJUSTE.CONSECUTIVO_AJUSTE) = vlo_EntOttAjusteInventario.ConsecutivoAjuste
                vlo_DrFilaDetalleAjuste.Item(Modelo.OTT_DETALLE_AJUSTE.ID_MATERIAL) = pvn_IdMaterial
                vlo_DrFilaDetalleAjuste.Item(Modelo.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE) = pvc_Tipo
                vlo_DrFilaDetalleAjuste.Item(Modelo.OTT_DETALLE_AJUSTE.CANTIDAD) = CType(pvc_CantidadAjuste, Double)
                vlo_DrFilaDetalleAjuste.Item(Modelo.OTT_DETALLE_AJUSTE.USUARIO) = pvc_UsuarioCrea

                vlo_DsDetalleAjuste.Tables(0).Rows.Add(vlo_DrFilaDetalleAjuste)

                vlo_DalOttDetalleAjuste.AdapterOttDetalleAjuste(vlo_DsDetalleAjuste)

                'Si se selecciono enviar la orden se registra la trazabilidad
                If pvb_Finalizar Then
                    'Se ingresa el registro de trazabilidad
                    vlo_EntOtlTrazabilidadAjuste = New EntOtlTrazabilidadAjuste

                    With vlo_EntOtlTrazabilidadAjuste
                        .EstadoAjuste = vlo_EntOttAjusteInventario.EstadoAjuste
                        .Observaciones = pvc_Observaciones
                        .Usuario = pvc_UsuarioCrea
                    End With

                    vlo_DalOtlTrazabilidadAjuste.InsertarRegistro(vlo_EntOtlTrazabilidadAjuste)
                End If

                vlo_Conexion.TransaccionCommit()

                vlc_Resultado = 1

            Catch vlo_Excepcion As Exception
                vlo_Conexion.TransaccionRollback()
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlc_Resultado
        End Function

        Public Function ModificarAjusteIndividual(pvo_EntOttAjusteInventario As EntOttAjusteInventario, pvc_CantidadAjuste As String, pvc_Observaciones As String, pvc_Tipo As String, pvc_NombreUsuario As String, pvb_Finalizar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAjusteInventario As DalOttAjusteInventario
            Dim vlo_DalOttDetalleAjuste As DalOttDetalleAjuste
            Dim vlo_DalOtlTrazabilidadAjuste As DalOtlTrazabilidadAjuste
            Dim vlo_EntOtlTrazabilidadAjuste As EntOtlTrazabilidadAjuste
            Dim vlo_EntOttDetalleAjuste As EntOttDetalleAjuste
            Dim vln_Resultado As Integer = 0
            Dim vlc_Condicion As String

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttAjusteInventario = New DalOttAjusteInventario(vlo_Conexion)
                vlo_DalOttDetalleAjuste = New DalOttDetalleAjuste(vlo_Conexion)
                vlo_DalOtlTrazabilidadAjuste = New DalOtlTrazabilidadAjuste(vlo_Conexion)

                'Se modifica el ajuste de inventario
                If pvo_EntOttAjusteInventario.Observaciones <> pvc_Observaciones Then
                    pvo_EntOttAjusteInventario.Observaciones = pvc_Observaciones
                End If

                If pvb_Finalizar Then
                    pvo_EntOttAjusteInventario.EstadoAjuste = EstadoAjuste.APROBACION_SUPERVISOR
                Else
                    pvo_EntOttAjusteInventario.EstadoAjuste = EstadoAjuste.CREADO
                End If

                pvo_EntOttAjusteInventario.FechaRegistroSolicitud = DateTime.Now
                pvo_EntOttAjusteInventario.Usuario = pvc_NombreUsuario

                vlo_DalOttAjusteInventario.ModificarRegistro(pvo_EntOttAjusteInventario)

                'Se obtiene el detalle del ajuste para modificar el registro
                vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTT_DETALLE_AJUSTE.ID_UBICACION, pvo_EntOttAjusteInventario.IdUbicacion, Modelo.OTT_DETALLE_AJUSTE.ANNO, pvo_EntOttAjusteInventario.Anno, Modelo.OTT_DETALLE_AJUSTE.CONSECUTIVO_AJUSTE, pvo_EntOttAjusteInventario.ConsecutivoAjuste)

                vlo_EntOttDetalleAjuste = vlo_DalOttDetalleAjuste.ObtenerRegistro(vlc_Condicion)

                If vlo_EntOttDetalleAjuste.DireccionAjuste <> pvc_Tipo Then
                    vlo_EntOttDetalleAjuste.DireccionAjuste = pvc_Tipo
                End If

                If vlo_EntOttDetalleAjuste.Cantidad <> pvc_CantidadAjuste Then
                    vlo_EntOttDetalleAjuste.Cantidad = pvc_CantidadAjuste
                End If

                vlo_EntOttDetalleAjuste.Usuario = pvc_NombreUsuario

                vlo_DalOttDetalleAjuste.ModificarRegistro(vlo_EntOttDetalleAjuste)

                'Si se selecciono finalizar se registra la trazabilidad del movimiento
                If pvb_Finalizar Then
                    'Se ingresa el registro de trazabilidad
                    vlo_EntOtlTrazabilidadAjuste = New EntOtlTrazabilidadAjuste

                    With vlo_EntOtlTrazabilidadAjuste
                        .EstadoAjuste = pvo_EntOttAjusteInventario.EstadoAjuste
                        .Observaciones = pvc_Observaciones
                        .Usuario = pvc_NombreUsuario
                    End With

                    vlo_DalOtlTrazabilidadAjuste.InsertarRegistro(vlo_EntOtlTrazabilidadAjuste)
                End If

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1

                
            Catch vlo_Excepcion As Exception
                vlo_Conexion.TransaccionRollback()
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        Public Function CrearAjusteGlobal(ByVal pvo_DsDetalles As Data.DataSet, pvn_IdUbicacion As Integer, pvc_IdBodega As Integer, pvc_Observaciones As String, pvc_UsuarioCrea As String, pvb_Finalizar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAjusteInventario As DalOttAjusteInventario
            Dim vlo_DalOttDetalleAjuste As DalOttDetalleAjuste
            Dim vlo_DalOtlTrazabilidadAjuste As DalOtlTrazabilidadAjuste
            Dim vlo_EntOtlTrazabilidadAjuste As EntOtlTrazabilidadAjuste
            Dim vlo_EntOttAjusteInventario As EntOttAjusteInventario
            Dim vlo_DsDetalleAjuste As Data.DataSet
            Dim vlc_Resultado As Integer = -1
            Dim vlo_DrFilaDetalleAjuste As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttAjusteInventario = New DalOttAjusteInventario(vlo_Conexion)
                vlo_DalOttDetalleAjuste = New DalOttDetalleAjuste(vlo_Conexion)
                vlo_DalOtlTrazabilidadAjuste = New DalOtlTrazabilidadAjuste(vlo_Conexion)

                'Se ingresa el ajuste del inventario
                vlo_EntOttAjusteInventario = New EntOttAjusteInventario
                vlo_EntOttAjusteInventario.IdUbicacion = pvn_IdUbicacion
                vlo_EntOttAjusteInventario.Anno = DateTime.Now.Year
                vlo_EntOttAjusteInventario.ConsecutivoAjuste = vlo_DalOttAjusteInventario.ObtenerFcOtConsecutivoAjuste(vlo_EntOttAjusteInventario.Anno, vlo_EntOttAjusteInventario.IdUbicacion) + 1
                vlo_EntOttAjusteInventario.IdAlmacenBodega = pvc_IdBodega
                If pvb_Finalizar Then
                    vlo_EntOttAjusteInventario.EstadoAjuste = EstadoAjuste.APROBACION_SUPERVISOR
                Else
                    vlo_EntOttAjusteInventario.EstadoAjuste = EstadoAjuste.CREADO
                End If
                vlo_EntOttAjusteInventario.TipoAjuste = TipoAjuste.GLOBALL
                vlo_EntOttAjusteInventario.FechaRegistroSolicitud = DateTime.Now
                vlo_EntOttAjusteInventario.Observaciones = pvc_Observaciones
                vlo_EntOttAjusteInventario.Usuario = pvc_UsuarioCrea


                vlo_DalOttAjusteInventario.InsertarRegistro(vlo_EntOttAjusteInventario)

                'Se ingresa el detalle del ajuste
                vlo_DsDetalleAjuste = vlo_DalOttDetalleAjuste.ListarRegistros("1 = 0", String.Empty, False, 0, 0)

                For Each vlo_FilaDetalle In pvo_DsDetalles.Tables(0).Rows

                    vlo_DrFilaDetalleAjuste = vlo_DsDetalleAjuste.Tables(0).NewRow

                    vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.ID_UBICACION)) = vlo_EntOttAjusteInventario.IdUbicacion

                    vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.ANNO)) = vlo_EntOttAjusteInventario.Anno
                    vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.CONSECUTIVO_AJUSTE)) = vlo_EntOttAjusteInventario.ConsecutivoAjuste
                    vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.ID_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL)
                    If Not TypeOf vlo_FilaDetalle.Item("CANTIDAD") Is DBNull Then
                        vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.CANTIDAD)) = CType(vlo_FilaDetalle("CANTIDAD"), Double)
                    End If

                    If Not TypeOf vlo_FilaDetalle.Item("TIPO") Is DBNull Then
                        vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE)) = CType(vlo_FilaDetalle("TIPO"), String)
                    End If
                    vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.USUARIO)) = pvc_UsuarioCrea

                    vlo_DsDetalleAjuste.Tables(0).Rows.Add(vlo_DrFilaDetalleAjuste)
                Next

                vlo_DalOttDetalleAjuste.AdapterOttDetalleAjuste(vlo_DsDetalleAjuste)

                'Si se selecciono enviar la orden se registra la trazabilidad
                If pvb_Finalizar Then
                    'Se ingresa el registro de trazabilidad
                    vlo_EntOtlTrazabilidadAjuste = New EntOtlTrazabilidadAjuste

                    With vlo_EntOtlTrazabilidadAjuste
                        .EstadoAjuste = vlo_EntOttAjusteInventario.EstadoAjuste
                        .Observaciones = pvc_Observaciones
                        .Usuario = pvc_UsuarioCrea
                    End With

                    vlo_DalOtlTrazabilidadAjuste.InsertarRegistro(vlo_EntOtlTrazabilidadAjuste)
                End If

                vlo_Conexion.TransaccionCommit()

                vlc_Resultado = 1

            Catch vlo_Excepcion As Exception
                vlo_Conexion.TransaccionRollback()
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlc_Resultado
        End Function

        Public Function ModificarAjusteGlobal(ByVal pvo_DsDetalles As Data.DataSet, ByVal pvo_EntOttAjusteInventario As EntOttAjusteInventario, pvc_Observaciones As String, pvc_UsuarioCrea As String, pvb_Finalizar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAjusteInventario As DalOttAjusteInventario
            Dim vlo_DalOttDetalleAjuste As DalOttDetalleAjuste
            Dim vlo_DalOtlTrazabilidadAjuste As DalOtlTrazabilidadAjuste
            Dim vlo_EntOtlTrazabilidadAjuste As EntOtlTrazabilidadAjuste
            Dim vln_Resultado As Integer = 0
            Dim vlc_Condicion As String
            Dim vlo_DsDetAprovisionamiento As Data.DataSet
            Dim vlc_DetRegistrado As String
            Dim vlo_DrFilaDetalleAjuste As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttAjusteInventario = New DalOttAjusteInventario(vlo_Conexion)
                vlo_DalOttDetalleAjuste = New DalOttDetalleAjuste(vlo_Conexion)
                vlo_DalOtlTrazabilidadAjuste = New DalOtlTrazabilidadAjuste(vlo_Conexion)

                'Se modifica el ajuste de inventario
                If pvo_EntOttAjusteInventario.Observaciones <> pvc_Observaciones Then
                    pvo_EntOttAjusteInventario.Observaciones = pvc_Observaciones
                End If

                If pvb_Finalizar Then
                    pvo_EntOttAjusteInventario.EstadoAjuste = EstadoAjuste.APROBACION_SUPERVISOR
                Else
                    pvo_EntOttAjusteInventario.EstadoAjuste = EstadoAjuste.CREADO
                End If

                pvo_EntOttAjusteInventario.FechaRegistroSolicitud = DateTime.Now
                pvo_EntOttAjusteInventario.Usuario = pvc_UsuarioCrea

                vlo_DalOttAjusteInventario.ModificarRegistro(pvo_EntOttAjusteInventario)

                'Se obtiene el detalle del ajuste para modificar el registro
                vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTT_DETALLE_AJUSTE.ID_UBICACION, pvo_EntOttAjusteInventario.IdUbicacion, Modelo.OTT_DETALLE_AJUSTE.ANNO, pvo_EntOttAjusteInventario.Anno, Modelo.OTT_DETALLE_AJUSTE.CONSECUTIVO_AJUSTE, pvo_EntOttAjusteInventario.ConsecutivoAjuste)

                vlo_DsDetAprovisionamiento = vlo_DalOttDetalleAjuste.ListarRegistros(vlc_Condicion, String.Empty, False, 0, 0)

                vlc_DetRegistrado = String.Empty

                For Each vlo_FilaDetalle In vlo_DsDetAprovisionamiento.Tables(0).Rows
                    For Each vlo_FilaPantalla In pvo_DsDetalles.Tables(0).Rows
                        If vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.ID_MATERIAL).ToString = vlo_FilaPantalla(Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL).ToString Then

                            If vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.CANTIDAD).ToString <> vlo_FilaPantalla("CANTIDAD").ToString Then
                                vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.CANTIDAD) = vlo_FilaPantalla("CANTIDAD")
                            End If

                            If vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE).ToString <> vlo_FilaPantalla("TIPO").ToString Then
                                vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE) = vlo_FilaPantalla("TIPO").ToString
                            End If
                        End If
                    Next
                    vlc_DetRegistrado = String.Format("{0}{1},", vlc_DetRegistrado, vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.ID_MATERIAL).ToString)
                Next

                vlc_DetRegistrado = String.Format(",{0}", vlc_DetRegistrado)

                For Each vlo_FilaDetalle In pvo_DsDetalles.Tables(0).Rows
                    If vlc_DetRegistrado.Contains(vlo_FilaDetalle(Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL).ToString) = False Then
                        vlo_DrFilaDetalleAjuste = vlo_DsDetAprovisionamiento.Tables(0).NewRow

                        vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.ID_UBICACION)) = pvo_EntOttAjusteInventario.IdUbicacion

                        vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.ANNO)) = pvo_EntOttAjusteInventario.Anno
                        vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.CONSECUTIVO_AJUSTE)) = pvo_EntOttAjusteInventario.ConsecutivoAjuste
                        vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.ID_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL)
                        If Not TypeOf vlo_FilaDetalle.Item("CANTIDAD") Is DBNull Then
                            vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.CANTIDAD)) = CType(vlo_FilaDetalle("CANTIDAD"), Double)
                        End If

                        If Not TypeOf vlo_FilaDetalle.Item("TIPO") Is DBNull Then
                            vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE)) = CType(vlo_FilaDetalle("TIPO"), String)
                        End If
                        vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.USUARIO)) = pvc_UsuarioCrea

                        vlo_DsDetAprovisionamiento.Tables(0).Rows.Add(vlo_DrFilaDetalleAjuste)
                    End If

                Next

                vlo_DalOttDetalleAjuste.AdapterOttDetalleAjuste(vlo_DsDetAprovisionamiento)

                'Si se selecciono finalizar se registra la trazabilidad del movimiento
                If pvb_Finalizar Then
                    'Se ingresa el registro de trazabilidad
                    vlo_EntOtlTrazabilidadAjuste = New EntOtlTrazabilidadAjuste

                    With vlo_EntOtlTrazabilidadAjuste
                        .EstadoAjuste = pvo_EntOttAjusteInventario.EstadoAjuste
                        .Observaciones = pvc_Observaciones
                        .Usuario = pvc_UsuarioCrea
                    End With

                    vlo_DalOtlTrazabilidadAjuste.InsertarRegistro(vlo_EntOtlTrazabilidadAjuste)
                End If

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1


            Catch vlo_Excepcion As Exception
                vlo_Conexion.TransaccionRollback()
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        Public Function CrearAjusteExistencia(ByVal pvo_DsDetalles As Data.DataSet, pvn_IdUbicacion As Integer, pvc_IdBodega As Integer, pvc_Observaciones As String, pvc_UsuarioCrea As String, pvb_Finalizar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAjusteInventario As DalOttAjusteInventario
            Dim vlo_DalOttDetalleAjuste As DalOttDetalleAjuste
            Dim vlo_DalOtlTrazabilidadAjuste As DalOtlTrazabilidadAjuste
            Dim vlo_EntOtlTrazabilidadAjuste As EntOtlTrazabilidadAjuste
            Dim vlo_EntOttAjusteInventario As EntOttAjusteInventario
            Dim vlo_DsDetalleAjuste As Data.DataSet
            Dim vlc_Resultado As Integer = -1
            Dim vlo_DrFilaDetalleAjuste As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttAjusteInventario = New DalOttAjusteInventario(vlo_Conexion)
                vlo_DalOttDetalleAjuste = New DalOttDetalleAjuste(vlo_Conexion)
                vlo_DalOtlTrazabilidadAjuste = New DalOtlTrazabilidadAjuste(vlo_Conexion)

                'Se ingresa el ajuste del inventario
                vlo_EntOttAjusteInventario = New EntOttAjusteInventario
                vlo_EntOttAjusteInventario.IdUbicacion = pvn_IdUbicacion
                vlo_EntOttAjusteInventario.Anno = DateTime.Now.Year
                vlo_EntOttAjusteInventario.ConsecutivoAjuste = vlo_DalOttAjusteInventario.ObtenerFcOtConsecutivoAjuste(vlo_EntOttAjusteInventario.Anno, vlo_EntOttAjusteInventario.IdUbicacion) + 1
                vlo_EntOttAjusteInventario.IdAlmacenBodega = pvc_IdBodega
                If pvb_Finalizar Then
                    vlo_EntOttAjusteInventario.EstadoAjuste = EstadoAjuste.APROBACION_SUPERVISOR
                Else
                    vlo_EntOttAjusteInventario.EstadoAjuste = EstadoAjuste.CREADO
                End If
                vlo_EntOttAjusteInventario.TipoAjuste = TipoAjuste.EXISTENCA
                vlo_EntOttAjusteInventario.FechaRegistroSolicitud = DateTime.Now
                vlo_EntOttAjusteInventario.Observaciones = pvc_Observaciones
                vlo_EntOttAjusteInventario.Usuario = pvc_UsuarioCrea


                vlo_DalOttAjusteInventario.InsertarRegistro(vlo_EntOttAjusteInventario)

                'Se ingresa el detalle del ajuste
                vlo_DsDetalleAjuste = vlo_DalOttDetalleAjuste.ListarRegistros("1 = 0", String.Empty, False, 0, 0)

                For Each vlo_FilaDetalle In pvo_DsDetalles.Tables(0).Rows

                    vlo_DrFilaDetalleAjuste = vlo_DsDetalleAjuste.Tables(0).NewRow

                    vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.ID_UBICACION)) = vlo_EntOttAjusteInventario.IdUbicacion

                    vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.ANNO)) = vlo_EntOttAjusteInventario.Anno
                    vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.CONSECUTIVO_AJUSTE)) = vlo_EntOttAjusteInventario.ConsecutivoAjuste
                    vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.ID_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL)

                    If Not TypeOf vlo_FilaDetalle.Item("TIPO") Is DBNull Then
                        If CType(vlo_FilaDetalle("TIPO"), String) = "Incremento" Then
                            vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE)) = TipoMovimiento.INCREMENTO
                            vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.CANTIDAD)) = CType(vlo_FilaDetalle("CANTIDAD"), Double) - vlo_FilaDetalle(Modelo.V_OTF_INVENTARIOLST.CANTIDAD_DISPONIBLE)
                        ElseIf CType(vlo_FilaDetalle("TIPO"), String) = "Decremento" Then
                            vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE)) = TipoMovimiento.DECREMENTO
                            vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.CANTIDAD)) = vlo_FilaDetalle(Modelo.V_OTF_INVENTARIOLST.CANTIDAD_DISPONIBLE) - CType(vlo_FilaDetalle("CANTIDAD"), Double)
                        End If
                    End If

                    vlo_DrFilaDetalleAjuste.Item(vlo_DsDetalleAjuste.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.USUARIO)) = pvc_UsuarioCrea

                    vlo_DsDetalleAjuste.Tables(0).Rows.Add(vlo_DrFilaDetalleAjuste)
                Next

                vlo_DalOttDetalleAjuste.AdapterOttDetalleAjuste(vlo_DsDetalleAjuste)

                'Si se selecciono enviar la orden se registra la trazabilidad
                If pvb_Finalizar Then
                    'Se ingresa el registro de trazabilidad
                    vlo_EntOtlTrazabilidadAjuste = New EntOtlTrazabilidadAjuste

                    With vlo_EntOtlTrazabilidadAjuste
                        .EstadoAjuste = vlo_EntOttAjusteInventario.EstadoAjuste
                        .Observaciones = pvc_Observaciones
                        .Usuario = pvc_UsuarioCrea
                    End With

                    vlo_DalOtlTrazabilidadAjuste.InsertarRegistro(vlo_EntOtlTrazabilidadAjuste)
                End If

                vlo_Conexion.TransaccionCommit()

                vlc_Resultado = 1

            Catch vlo_Excepcion As Exception
                vlo_Conexion.TransaccionRollback()
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlc_Resultado
        End Function

        Public Function ModificarAjusteExistencia(ByVal pvo_DsDetalles As Data.DataSet, ByVal pvo_EntOttAjusteInventario As EntOttAjusteInventario, pvc_Observaciones As String, pvc_UsuarioCrea As String, pvb_Finalizar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAjusteInventario As DalOttAjusteInventario
            Dim vlo_DalOttDetalleAjuste As DalOttDetalleAjuste
            Dim vlo_DalOtlTrazabilidadAjuste As DalOtlTrazabilidadAjuste
            Dim vlo_EntOtlTrazabilidadAjuste As EntOtlTrazabilidadAjuste
            Dim vln_Resultado As Integer = 0
            Dim vlc_Condicion As String
            Dim vlo_DsDetAprovisionamiento As Data.DataSet
            Dim vlc_DetRegistrado As String
            Dim vlo_DrFilaDetalleAjuste As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttAjusteInventario = New DalOttAjusteInventario(vlo_Conexion)
                vlo_DalOttDetalleAjuste = New DalOttDetalleAjuste(vlo_Conexion)
                vlo_DalOtlTrazabilidadAjuste = New DalOtlTrazabilidadAjuste(vlo_Conexion)

                'Se modifica el ajuste de inventario
                If pvo_EntOttAjusteInventario.Observaciones <> pvc_Observaciones Then
                    pvo_EntOttAjusteInventario.Observaciones = pvc_Observaciones
                End If

                If pvb_Finalizar Then
                    pvo_EntOttAjusteInventario.EstadoAjuste = EstadoAjuste.APROBACION_SUPERVISOR
                Else
                    pvo_EntOttAjusteInventario.EstadoAjuste = EstadoAjuste.CREADO
                End If

                pvo_EntOttAjusteInventario.FechaRegistroSolicitud = DateTime.Now
                pvo_EntOttAjusteInventario.Usuario = pvc_UsuarioCrea

                vlo_DalOttAjusteInventario.ModificarRegistro(pvo_EntOttAjusteInventario)

                'Se obtiene el detalle del ajuste para modificar el registro
                vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTT_DETALLE_AJUSTE.ID_UBICACION, pvo_EntOttAjusteInventario.IdUbicacion, Modelo.OTT_DETALLE_AJUSTE.ANNO, pvo_EntOttAjusteInventario.Anno, Modelo.OTT_DETALLE_AJUSTE.CONSECUTIVO_AJUSTE, pvo_EntOttAjusteInventario.ConsecutivoAjuste)

                vlo_DsDetAprovisionamiento = vlo_DalOttDetalleAjuste.ListarRegistros(vlc_Condicion, String.Empty, False, 0, 0)

                vlc_DetRegistrado = String.Empty

                For Each vlo_FilaDetalle In vlo_DsDetAprovisionamiento.Tables(0).Rows
                    For Each vlo_FilaPantalla In pvo_DsDetalles.Tables(0).Rows
                        If vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.ID_MATERIAL).ToString = vlo_FilaPantalla(Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL).ToString Then

                            If CType(vlo_FilaPantalla("TIPO"), String) = "Incremento" Then
                                vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE) = TipoMovimiento.INCREMENTO
                                vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.CANTIDAD) = CType(vlo_FilaPantalla("CANTIDAD"), Double) - vlo_FilaPantalla(Modelo.V_OTF_INVENTARIOLST.CANTIDAD_DISPONIBLE)
                            ElseIf CType(vlo_FilaPantalla("TIPO"), String) = "Decremento" Then
                                vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE) = TipoMovimiento.DECREMENTO
                                vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.CANTIDAD) = vlo_FilaPantalla(Modelo.V_OTF_INVENTARIOLST.CANTIDAD_DISPONIBLE) - CType(vlo_FilaPantalla("CANTIDAD"), Double)
                            End If
                        End If
                    Next
                    vlc_DetRegistrado = String.Format("{0}{1},", vlc_DetRegistrado, vlo_FilaDetalle(Modelo.OTT_DETALLE_AJUSTE.ID_MATERIAL).ToString)
                Next

                vlc_DetRegistrado = String.Format(",{0}", vlc_DetRegistrado)

                For Each vlo_FilaDetalle In pvo_DsDetalles.Tables(0).Rows
                    If vlc_DetRegistrado.Contains(vlo_FilaDetalle(Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL).ToString) = False Then
                        vlo_DrFilaDetalleAjuste = vlo_DsDetAprovisionamiento.Tables(0).NewRow

                        vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.ID_UBICACION)) = pvo_EntOttAjusteInventario.IdUbicacion

                        vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.ANNO)) = pvo_EntOttAjusteInventario.Anno
                        vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.CONSECUTIVO_AJUSTE)) = pvo_EntOttAjusteInventario.ConsecutivoAjuste
                        vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.ID_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OTF_INVENTARIOLST.ID_MATERIAL)

                        If Not TypeOf vlo_FilaDetalle.Item("TIPO") Is DBNull Then
                            If CType(vlo_FilaDetalle("TIPO"), String) = "Incremento" Then
                                vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE)) = TipoMovimiento.INCREMENTO
                                vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.CANTIDAD)) = CType(vlo_FilaDetalle("CANTIDAD"), Double) - vlo_FilaDetalle(Modelo.V_OTF_INVENTARIOLST.CANTIDAD_DISPONIBLE)
                            ElseIf CType(vlo_FilaDetalle("TIPO"), String) = "Decremento" Then
                                vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE)) = TipoMovimiento.DECREMENTO
                                vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.CANTIDAD)) = vlo_FilaDetalle(Modelo.V_OTF_INVENTARIOLST.CANTIDAD_DISPONIBLE) - CType(vlo_FilaDetalle("CANTIDAD"), Double)
                            End If
                        End If

                        vlo_DrFilaDetalleAjuste.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DETALLE_AJUSTE.USUARIO)) = pvc_UsuarioCrea

                        vlo_DsDetAprovisionamiento.Tables(0).Rows.Add(vlo_DrFilaDetalleAjuste)
                    End If

                Next

                vlo_DalOttDetalleAjuste.AdapterOttDetalleAjuste(vlo_DsDetAprovisionamiento)

                'Si se selecciono finalizar se registra la trazabilidad del movimiento
                If pvb_Finalizar Then
                    'Se ingresa el registro de trazabilidad
                    vlo_EntOtlTrazabilidadAjuste = New EntOtlTrazabilidadAjuste

                    With vlo_EntOtlTrazabilidadAjuste
                        .EstadoAjuste = pvo_EntOttAjusteInventario.EstadoAjuste
                        .Observaciones = pvc_Observaciones
                        .Usuario = pvc_UsuarioCrea
                    End With

                    vlo_DalOtlTrazabilidadAjuste.InsertarRegistro(vlo_EntOtlTrazabilidadAjuste)
                End If

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1


            Catch vlo_Excepcion As Exception
                vlo_Conexion.TransaccionRollback()
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        Public Function AprobacionSupervisor(ByVal pvo_Registro As EntOttAjusteInventario, ByVal pvo_Trazabilidad As EntOtlTrazabilidadAjuste) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAjusteInventario As DalOttAjusteInventario
            Dim vlo_DalOtlTrazabilidadAjuste As DalOtlTrazabilidadAjuste
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttAjusteInventario = New DalOttAjusteInventario(vlo_Conexion)
                vlo_DalOtlTrazabilidadAjuste = New DalOtlTrazabilidadAjuste(vlo_Conexion)

                vln_Resultado = vlo_DalOttAjusteInventario.ModificarRegistro(pvo_Registro)

                vlo_DalOtlTrazabilidadAjuste.InsertarRegistro(pvo_Trazabilidad)

                vlo_Conexion.TransaccionCommit()
            Catch vlo_Excepcion As Exception
                vlo_Conexion.TransaccionRollback()
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        Public Function AprobacionJefatura(ByVal pvo_Registro As EntOttAjusteInventario, ByVal pvo_Trazabilidad As EntOtlTrazabilidadAjuste) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAjusteInventario As DalOttAjusteInventario
            Dim vlo_DalOtlTrazabilidadAjuste As DalOtlTrazabilidadAjuste
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttAjusteInventario = New DalOttAjusteInventario(vlo_Conexion)
                vlo_DalOtlTrazabilidadAjuste = New DalOtlTrazabilidadAjuste(vlo_Conexion)

                vln_Resultado = vlo_DalOttAjusteInventario.ModificarRegistro(pvo_Registro)

                vlo_DalOtlTrazabilidadAjuste.InsertarRegistro(pvo_Trazabilidad)

                vlo_Conexion.TransaccionCommit()
            Catch vlo_Excepcion As Exception
                vlo_Conexion.TransaccionRollback()
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

#End Region

    End Class
End Namespace
