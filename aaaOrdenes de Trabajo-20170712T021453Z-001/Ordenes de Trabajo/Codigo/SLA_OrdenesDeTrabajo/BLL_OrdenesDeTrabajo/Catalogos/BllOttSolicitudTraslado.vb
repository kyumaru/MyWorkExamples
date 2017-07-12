Imports Utilerias.ORDENES_TRABAJO
Imports ORDENES_TRABAJO.EntidadNegocio.Catalogos
Imports ORDENES_TRABAJO.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Utilerias.OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo.Modelo
Imports System.Data
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos

Namespace ORDENES_TRABAJO.LogicaNegocio.Catalogos
    Public Class BllOttSolicitudTraslado
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
        ''' Permite agregar un registro en la tabla OTT_SOLICITUD_TRASLADO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttSolicitudTraslado) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttSolicitudTraslado As DalOttSolicitudTraslado
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.Anno, pvo_Registro.IdSolicitudTraslado, pvo_Registro.IdUbicacion).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
                End If

                vlo_DalOttSolicitudTraslado = New DalOttSolicitudTraslado(vlo_Conexion)
                vln_Resultado = vlo_DalOttSolicitudTraslado.InsertarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTT_SOLICITUD_TRASLADO, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOttSolicitudTraslado) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttSolicitudTraslado As DalOttSolicitudTraslado
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.Anno, pvo_Registro.IdSolicitudTraslado, pvo_Registro.IdUbicacion) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Hay registro asociados")
                End If

                vlo_DalOttSolicitudTraslado = New DalOttSolicitudTraslado(vlo_Conexion)
                vln_Resultado = vlo_DalOttSolicitudTraslado.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvn_Anno">Año de la solicitud</param>
        ''' <param name="pvn_IdSolicitudTraslado">Consecutivo de la solicitud. el consecutivo es anual.</param>
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_Anno As Integer, pvn_IdSolicitudTraslado As Integer, pvn_IdUbicacion As Integer) As EntOttSolicitudTraslado
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttSolicitudTraslado As DalOttSolicitudTraslado

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttSolicitudTraslado = New DalOttSolicitudTraslado(vlo_Conexion)
                Return vlo_DalOttSolicitudTraslado.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Utilerias.OrdenesDeTrabajo.Modelo.OTT_SOLICITUD_TRASLADO.ANNO, pvn_Anno, Utilerias.OrdenesDeTrabajo.Modelo.OTT_SOLICITUD_TRASLADO.ID_SOLICITUD_TRASLADO, pvn_IdSolicitudTraslado, Utilerias.OrdenesDeTrabajo.Modelo.OTT_SOLICITUD_TRASLADO.ID_UBICACION, pvn_IdUbicacion))
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
        ''' <param name="pvn_Anno">Año de la solicitud</param>
        ''' <param name="pvn_IdSolicitudTraslado">Consecutivo de la solicitud. el consecutivo es anual.</param>
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:18:06 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_Anno As Integer, pvn_IdSolicitudTraslado As Integer, pvn_IdUbicacion As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOttLineaTraslado As DalOttLineaTraslado
            Dim vlo_DalOtlTrazabilSolTraslado As DalOtlTrazabilSolTraslado
            Dim vlo_DalOtlLineaTraslado As DalOtlLineaTraslado

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

                'Determinar la existencia de registros asociados en la tabla OTT_LINEA_TRASLADO
                vlo_DalOttLineaTraslado = New DalOttLineaTraslado(vlo_Conexion)
                If vlo_DalOttLineaTraslado.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Utilerias.OrdenesDeTrabajo.Modelo.OTT_LINEA_TRASLADO.ANNO, pvn_Anno, Utilerias.OrdenesDeTrabajo.Modelo.OTT_LINEA_TRASLADO.ID_SOLICITUD_TRASLADO, pvn_IdSolicitudTraslado, Utilerias.OrdenesDeTrabajo.Modelo.OTT_LINEA_TRASLADO.ID_UBICACION, pvn_IdUbicacion)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTL_TRAZABIL_SOL_TRASLADO
                vlo_DalOtlTrazabilSolTraslado = New DalOtlTrazabilSolTraslado(vlo_Conexion)
                If vlo_DalOtlTrazabilSolTraslado.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Utilerias.OrdenesDeTrabajo.Modelo.OTL_TRAZABIL_SOL_TRASLADO.ANNO, pvn_Anno, Utilerias.OrdenesDeTrabajo.Modelo.OTL_TRAZABIL_SOL_TRASLADO.ID_SOLICITUD_TRASLADO, pvn_IdSolicitudTraslado, Utilerias.OrdenesDeTrabajo.Modelo.OTL_TRAZABIL_SOL_TRASLADO.ID_UBICACION, pvn_IdUbicacion)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTL_LINEA_TRASLADO
                vlo_DalOtlLineaTraslado = New DalOtlLineaTraslado(vlo_Conexion)
                If vlo_DalOtlLineaTraslado.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Utilerias.OrdenesDeTrabajo.Modelo.OTL_LINEA_TRASLADO.ANNO, pvn_Anno, Utilerias.OrdenesDeTrabajo.Modelo.OTL_LINEA_TRASLADO.ID_SOLICITUD_TRASLADO, pvn_IdSolicitudTraslado, Utilerias.OrdenesDeTrabajo.Modelo.OTL_LINEA_TRASLADO.ID_UBICACION, pvn_IdUbicacion)).Existe Then
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
        ''' Permite agregar un registro en la tabla OTM_PROVEEDOR, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarModificarRegistroConAsociados(ByVal pvo_Registro As EntOttSolicitudTraslado, ByVal pvo_DsLineaTraslado As DataSet, ByVal pvb_EsAgregar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttSolicitudTraslado As DalOttSolicitudTraslado
            Dim vlo_DalOtmAlmacenBodega As DalOtmAlmacenBodega
            Dim vlo_EntOtmAlmacenBodega As EntOtmAlmacenBodega
            Dim vln_Resultado As Integer
            Dim vlo_DsDatos As New DataSet
            Dim vln_IdSolicitud As Integer = 0
            Dim vlo_DsLineasBorrados As New DataSet
            Dim vlo_DsLineasAgregados As New DataSet
            Dim vlo_DsSinModificar As New DataSet
            Dim vlc_estado As String = String.Empty

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)

                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttSolicitudTraslado = New DalOttSolicitudTraslado(vlo_Conexion)
                vlo_DalOtmAlmacenBodega = New DalOtmAlmacenBodega(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()
                If pvo_Registro.EstadoTraslado = EstadoTraslado.CREADA Then
                    vlc_estado = EstadoDetalle.PENDIENTE
                ElseIf pvo_Registro.EstadoTraslado = EstadoTraslado.PENDIENTE_APROBACION Then
                    vlc_estado = EstadoDetalle.PENDIENTE_APROBACION
                End If
                If Not String.IsNullOrWhiteSpace(vlc_estado) Then
                    If pvo_DsLineaTraslado.Tables(0).Rows.Count > 0 And Not pvo_DsLineaTraslado.Tables(0) Is Nothing Then
                        If pvb_EsAgregar = True Then
                            vlo_DsDatos = vlo_DalOttSolicitudTraslado.ListarRegistros(String.Empty, String.Format("{0} {1}", OTT_LINEA_TRASLADO.ID_SOLICITUD_TRASLADO, Ordenamiento.DESCENDENTE), False, 0, 0)

                            If vlo_DsDatos.Tables(0).Rows.Count = 0 Then
                                vln_IdSolicitud = 0
                            Else
                                vln_IdSolicitud = CType(vlo_DsDatos.Tables(0).Rows(0).Item(1).ToString, Integer)
                                vln_IdSolicitud = vln_IdSolicitud + 1
                            End If
                            vlo_EntOtmAlmacenBodega = New EntOtmAlmacenBodega
                            vlo_EntOtmAlmacenBodega = vlo_DalOtmAlmacenBodega.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, pvo_Registro.IdUbicacion, OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))
                            pvo_Registro.IdSolicitudTraslado = vln_IdSolicitud
                            pvo_Registro.IdAlmacen = vlo_EntOtmAlmacenBodega.IdAlmacenBodega
                            vln_Resultado = vlo_DalOttSolicitudTraslado.InsertarRegistro(pvo_Registro)
                        Else
                            vln_Resultado = vlo_DalOttSolicitudTraslado.ModificarRegistro(pvo_Registro)
                        End If

                        If vln_Resultado > 0 Then
                            'separa los telefonos que se deben de borrar y los que se deben de agregar
                            vlo_DsLineasBorrados = ObtenerRegistrosPorEstado(pvo_DsLineaTraslado, DataRowState.Deleted)
                            vlo_DsLineasAgregados = ObtenerRegistrosPorEstado(pvo_DsLineaTraslado, DataRowState.Added)
                            vlo_DsSinModificar = ObtenerRegistrosPorEstado(pvo_DsLineaTraslado, DataRowState.Unchanged)

                            'Borra y agrega los registros de telefonos del proveedor
                            AgregarBorrarRegistroDeLineas(vlo_DsLineasBorrados, vlo_DsLineasAgregados, vlo_DsSinModificar, vlo_Conexion, vlc_estado, vln_IdSolicitud)

                        End If
                    Else
                        vln_Resultado = -1
                        Throw New OrdenesDeTrabajoException("Debe Agregar Todos los datos correctamente")
                    End If
                Else
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Debe Agregar Todos los datos correctamente")
                End If
                vlo_Conexion.TransaccionCommit()
            Catch vlo_Excepcion As Exception
                Throw
                vlo_Conexion.TransaccionRollback()
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function


        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="pvo_Ds"></param>
        ''' <param name="pvc_Rowstate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ObtenerRegistrosPorEstado(ByVal pvo_Ds As DataSet, ByVal pvc_Rowstate As DataRowState) As DataSet
            Dim vlo_DsSet As DataSet
            Dim vlo_Row As DataRow
            Dim vlo_tabla As DataTable

            vlo_tabla = pvo_Ds.Tables(0).Clone
            vlo_Row = pvo_Ds.Tables(0).NewRow
            vlo_DsSet = New DataSet
            vlo_DsSet.Tables.Add(vlo_tabla)

            For Each vlo_Row In pvo_Ds.Tables(0).Rows
                If vlo_Row.RowState = pvc_Rowstate Then 'Se cambia el estado de los rows
                    vlo_DsSet.Tables(0).ImportRow(vlo_Row)
                End If
            Next
            Return vlo_DsSet
        End Function

        ''' <summary>
        ''' Borrar los usuarios sin dependencias registrados en una unidad facturadora de la tabla UsuarioFacturacion  y de MembershipProvider 
        ''' </summary>
        ''' <param name="pvo_TelefonoBorrado"></param>
        ''' <param name="pvo_Conexion"></param>
        ''' <remarks></remarks>
        ''' <author>Jeannette Chavarría Rojas</author>
        ''' <creationDate>17/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Private Sub AgregarBorrarRegistroDeLineas(pvo_LineasBorrado As DataSet, pvo_LineasAgregar As DataSet, pvo_SinModificar As DataSet, pvo_Conexion As ConexionOracle, pvc_Estado As String, pvn_IdSolicitud As Integer)
            Dim vlo_DalOttLineaTraslado As DalOttLineaTraslado
            Dim vlo_DalOtfInventario As DalOtfInventario
            Dim vlo_EntOttLineaTraslado As EntOttLineaTraslado
            Dim vlo_EntOtfInventario As EntOtfInventario

            vlo_DalOttLineaTraslado = New DalOttLineaTraslado(pvo_Conexion)
            vlo_DalOtfInventario = New DalOtfInventario(pvo_Conexion)

            Try

                If Not pvo_LineasBorrado.Tables(0) Is Nothing AndAlso pvo_LineasBorrado.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila In pvo_LineasBorrado.Tables(0).Rows
                        vlo_EntOttLineaTraslado = New EntOttLineaTraslado
                        vlo_EntOttLineaTraslado.Anno = vlo_fila(OTT_LINEA_TRASLADO.ANNO, DataRowVersion.Original).ToString
                        vlo_EntOttLineaTraslado.IdSolicitudTraslado = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_SOLICITUD_TRASLADO, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.IdUbicacion = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_UBICACION, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.IdAlmacen = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_ALMACEN, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.IdMaterial = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_MATERIAL, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.CantidadRequerida = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_REQUERIDA, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.CantidadRetirada = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_RETIRADA, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.Detalle = vlo_fila(OTT_LINEA_TRASLADO.DETALLE, DataRowVersion.Original).ToString
                        vlo_EntOttLineaTraslado.Estado = vlo_fila(OTT_LINEA_TRASLADO.ESTADO, DataRowVersion.Original).ToString
                        vlo_EntOttLineaTraslado.Usuario = vlo_fila(OTT_LINEA_TRASLADO.USUARIO, DataRowVersion.Original).ToString
                        vlo_EntOttLineaTraslado.TimeStamp = vlo_fila(OTT_LINEA_TRASLADO.TIME_STAMP, DataRowVersion.Original).ToString

                        vlo_DalOttLineaTraslado.BorrarRegistro(vlo_EntOttLineaTraslado)

                        vlo_EntOtfInventario = New EntOtfInventario
                        vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOttLineaTraslado.IdAlmacen, OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_EntOttLineaTraslado.IdUbicacion, OTF_INVENTARIO.ID_MATERIAL, vlo_EntOttLineaTraslado.IdMaterial))
                        If vlo_EntOtfInventario.Existe Then
                            vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada - vlo_EntOttLineaTraslado.CantidadRequerida
                            vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)

                        End If
                    Next
                ElseIf Not pvo_LineasAgregar.Tables(0) Is Nothing AndAlso pvo_LineasAgregar.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila In pvo_LineasAgregar.Tables(0).Rows
                        vlo_EntOttLineaTraslado = New EntOttLineaTraslado
                        vlo_EntOttLineaTraslado.Anno = CType(vlo_fila(OTT_LINEA_TRASLADO.ANNO), Integer)
                        vlo_EntOttLineaTraslado.IdSolicitudTraslado = CType(pvn_IdSolicitud, Integer)
                        vlo_EntOttLineaTraslado.IdUbicacion = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_UBICACION), Integer)
                        vlo_EntOttLineaTraslado.IdAlmacen = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_ALMACEN), Integer)
                        vlo_EntOttLineaTraslado.IdMaterial = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_MATERIAL), Integer)
                        vlo_EntOttLineaTraslado.CantidadRequerida = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_REQUERIDA), Integer)
                        vlo_EntOttLineaTraslado.CantidadRetirada = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_RETIRADA), Integer)
                        vlo_EntOttLineaTraslado.Detalle = vlo_fila(OTT_LINEA_TRASLADO.DETALLE).ToString
                        vlo_EntOttLineaTraslado.Estado = pvc_Estado
                        vlo_EntOttLineaTraslado.Usuario = vlo_fila(OTT_LINEA_TRASLADO.USUARIO).ToString
                        vlo_EntOttLineaTraslado.TimeStamp = vlo_fila(OTT_LINEA_TRASLADO.TIME_STAMP).ToString
                        vlo_DalOttLineaTraslado.InsertarRegistro(vlo_EntOttLineaTraslado)

                        vlo_EntOtfInventario = New EntOtfInventario
                        vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOttLineaTraslado.IdAlmacen, OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_EntOttLineaTraslado.IdUbicacion, OTF_INVENTARIO.ID_MATERIAL, vlo_EntOttLineaTraslado.IdMaterial))
                        If vlo_EntOtfInventario.Existe Then
                            vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada + vlo_EntOttLineaTraslado.CantidadRequerida
                            vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)
                        End If
                    Next
                ElseIf Not pvo_SinModificar.Tables(0) Is Nothing AndAlso pvo_SinModificar.Tables(0).Rows.Count > 0 Then
                    If pvc_Estado = EstadoDetalle.PENDIENTE_APROBACION Then
                        For Each vlo_fila In pvo_SinModificar.Tables(0).Rows
                            vlo_EntOttLineaTraslado = New EntOttLineaTraslado
                            vlo_EntOttLineaTraslado.Anno = CType(vlo_fila(OTT_LINEA_TRASLADO.ANNO), Integer)
                            vlo_EntOttLineaTraslado.IdSolicitudTraslado = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_SOLICITUD_TRASLADO), Integer)
                            vlo_EntOttLineaTraslado.IdUbicacion = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_UBICACION), Integer)
                            vlo_EntOttLineaTraslado.IdAlmacen = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_ALMACEN), Integer)
                            vlo_EntOttLineaTraslado.IdMaterial = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_MATERIAL), Integer)
                            vlo_EntOttLineaTraslado.CantidadRequerida = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_REQUERIDA), Integer)
                            vlo_EntOttLineaTraslado.CantidadRetirada = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_RETIRADA), Integer)
                            vlo_EntOttLineaTraslado.Detalle = vlo_fila(OTT_LINEA_TRASLADO.DETALLE).ToString
                            vlo_EntOttLineaTraslado.Estado = pvc_Estado
                            vlo_EntOttLineaTraslado.Usuario = vlo_fila(OTT_LINEA_TRASLADO.USUARIO).ToString
                            vlo_EntOttLineaTraslado.TimeStamp = vlo_fila(OTT_LINEA_TRASLADO.TIME_STAMP).ToString

                            vlo_DalOttLineaTraslado.ModificarRegistro(vlo_EntOttLineaTraslado)

                            vlo_EntOtfInventario = New EntOtfInventario
                            vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOttLineaTraslado.IdAlmacen, OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_EntOttLineaTraslado.IdUbicacion, OTF_INVENTARIO.ID_MATERIAL, vlo_EntOttLineaTraslado.IdMaterial))
                            If vlo_EntOtfInventario.Existe Then
                                vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada + vlo_EntOttLineaTraslado.CantidadRequerida
                                vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)
                            End If
                        Next
                    End If
                End If
            Catch vlo_Excepcion As Exception
                Throw
            End Try
        End Sub


        ''' <summary>
        ''' Permite borrar un registro en la tabla OTT_LINEA_TRASLADO
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:16:31 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistrosConjunto(ByVal pvo_Registro As EntOttSolicitudTraslado) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttSolicitudTraslado As DalOttSolicitudTraslado
            Dim vlo_DalOttLineaTraslado As DalOttLineaTraslado
            Dim vlo_EntOttLineaTraslado As EntOttLineaTraslado
            Dim vln_Resultado As Integer
            Dim vlo_DsLineasBorrados As New DataSet
            Dim vlc_Condicion As String = String.Empty
            Dim vlo_DalOtfInventario As DalOtfInventario
            Dim vlo_EntOtfInventario As EntOtfInventario

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)

                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttSolicitudTraslado = New DalOttSolicitudTraslado(vlo_Conexion)
                vlo_DalOttLineaTraslado = New DalOttLineaTraslado(vlo_Conexion)
                vlo_DalOtfInventario = New DalOtfInventario(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()
                vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", OTT_LINEA_TRASLADO.ID_SOLICITUD_TRASLADO, pvo_Registro.IdSolicitudTraslado, OTT_LINEA_TRASLADO.ANNO, pvo_Registro.Anno, OTT_LINEA_TRASLADO.ID_UBICACION, pvo_Registro.IdUbicacion, OTT_LINEA_TRASLADO.ID_ALMACEN, pvo_Registro.IdAlmacen)
                vlo_DsLineasBorrados = vlo_DalOttLineaTraslado.ListarRegistros(vlc_Condicion, String.Empty, False, 0, 0)

                If vlo_DsLineasBorrados.Tables(0).Rows.Count > 0 And Not vlo_DsLineasBorrados.Tables(0) Is Nothing Then
                    For Each vlo_fila In vlo_DsLineasBorrados.Tables(0).Rows
                        vlo_EntOttLineaTraslado = New EntOttLineaTraslado
                        vlo_EntOttLineaTraslado.Anno = CType(vlo_fila(OTT_LINEA_TRASLADO.ANNO), Integer)
                        vlo_EntOttLineaTraslado.IdSolicitudTraslado = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_SOLICITUD_TRASLADO), Integer)
                        vlo_EntOttLineaTraslado.IdUbicacion = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_UBICACION), Integer)
                        vlo_EntOttLineaTraslado.IdAlmacen = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_ALMACEN), Integer)
                        vlo_EntOttLineaTraslado.IdMaterial = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_MATERIAL), Integer)
                        vlo_DalOttLineaTraslado.BorrarRegistro(vlo_EntOttLineaTraslado)

                        vlo_EntOtfInventario = New EntOtfInventario
                        vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOttLineaTraslado.IdAlmacen, OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_EntOttLineaTraslado.IdUbicacion, OTF_INVENTARIO.ID_MATERIAL, vlo_EntOttLineaTraslado.IdMaterial))
                        If vlo_EntOtfInventario.Existe Then
                            vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada - vlo_EntOttLineaTraslado.CantidadRequerida
                            vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)
                        End If

                    Next
                    vln_Resultado = vlo_DalOttSolicitudTraslado.BorrarRegistro(pvo_Registro)
                Else
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Debe Agregar Todos los datos correctamente")
                End If
                vlo_Conexion.TransaccionCommit()
            Catch vlo_Excepcion As Exception
                Throw
                vlo_Conexion.TransaccionRollback()
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function


        ''' <summary>
        ''' Permite agregar un registro en la tabla OTM_PROVEEDOR, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarRegistroConDesicion(ByVal pvo_Registro As EntOttSolicitudTraslado, ByVal pvo_RegistroTrazabilidad As EntOtlTrazabilSolTraslado, ByVal pvo_DsLineaTraslado As DataSet, ByVal pvb_EsAprobar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttSolicitudTraslado As DalOttSolicitudTraslado
            Dim vlo_DalOtlTrazabilSolTraslado As DalOtlTrazabilSolTraslado
            Dim vln_Resultado As Integer
            Dim vlo_DsDatos As New DataSet
            Dim vln_IdSolicitud As Integer = 0
            Dim vlo_DsLineasBorrados As New DataSet
            Dim vlo_DsLineasAgregados As New DataSet
            Dim vlo_DsLineasSinCambios As New DataSet
            Dim vlc_estado As String = String.Empty

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)

                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttSolicitudTraslado = New DalOttSolicitudTraslado(vlo_Conexion)
                vlo_DalOtlTrazabilSolTraslado = New DalOtlTrazabilSolTraslado(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()
                If pvo_DsLineaTraslado.Tables(0).Rows.Count > 0 And Not pvo_DsLineaTraslado.Tables(0) Is Nothing Then
                    If pvb_EsAprobar = True Then
                        vln_Resultado = vlo_DalOttSolicitudTraslado.ModificarRegistro(pvo_Registro)
                        If vln_Resultado > 0 Then
                            'separa los telefonos que se deben de borrar y los que se deben de agregar
                            vlo_DsLineasBorrados = ObtenerRegistrosPorEstado(pvo_DsLineaTraslado, DataRowState.Deleted)
                            vlo_DsLineasAgregados = ObtenerRegistrosPorEstado(pvo_DsLineaTraslado, DataRowState.Added)
                            vlo_DsLineasSinCambios = ObtenerRegistrosPorEstado(pvo_DsLineaTraslado, DataRowState.Unchanged)

                            'Borra y agrega los registros de telefonos del proveedor
                            ModificarBorrarRegistroDeLineasAprobadas(vlo_DsLineasBorrados, vlo_DsLineasAgregados, vlo_DsLineasSinCambios, vlo_Conexion, vln_IdSolicitud)
                            vln_Resultado = vlo_DalOtlTrazabilSolTraslado.InsertarRegistro(pvo_RegistroTrazabilidad)
                        End If
                    Else
                        vln_Resultado = vlo_DalOttSolicitudTraslado.ModificarRegistro(pvo_Registro)
                        If vln_Resultado > 0 Then
                            'separa los telefonos que se deben de borrar y los que se deben de agregar
                            vlo_DsLineasBorrados = ObtenerRegistrosPorEstado(pvo_DsLineaTraslado, DataRowState.Deleted)
                            vlo_DsLineasAgregados = ObtenerRegistrosPorEstado(pvo_DsLineaTraslado, DataRowState.Added)
                            vlo_DsLineasSinCambios = ObtenerRegistrosPorEstado(pvo_DsLineaTraslado, DataRowState.Unchanged)
                            'Borra y agrega los registros de telefonos del proveedor
                            ModificarBorrarRegistroDeLineasDevueltas(vlo_DsLineasBorrados, vlo_DsLineasAgregados, vlo_DsLineasSinCambios, vlo_Conexion, vln_IdSolicitud)
                            vln_Resultado = vlo_DalOtlTrazabilSolTraslado.InsertarRegistro(pvo_RegistroTrazabilidad)
                        End If
                    End If
                Else
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Debe Agregar Todos los datos correctamente")
                End If
                vlo_Conexion.TransaccionCommit()
            Catch vlo_Excepcion As Exception
                Throw
                vlo_Conexion.TransaccionRollback()
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Borrar los usuarios sin dependencias registrados en una unidad facturadora de la tabla UsuarioFacturacion  y de MembershipProvider 
        ''' </summary>
        ''' <param name="pvo_TelefonoBorrado"></param>
        ''' <param name="pvo_Conexion"></param>
        ''' <remarks></remarks>
        ''' <author>Jeannette Chavarría Rojas</author>
        ''' <creationDate>17/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Private Sub ModificarBorrarRegistroDeLineasDevueltas(pvo_LineasBorrado As DataSet, pvo_LineasAgregar As DataSet, pvo_SinCambios As DataSet, pvo_Conexion As ConexionOracle, pvn_IdSolicitud As Integer)
            Dim vlo_DalOttLineaTraslado As DalOttLineaTraslado
            Dim vlo_DalOtfInventario As DalOtfInventario
            Dim vlo_EntOttLineaTraslado As EntOttLineaTraslado
            Dim vlo_EntOtfInventario As EntOtfInventario

            vlo_DalOttLineaTraslado = New DalOttLineaTraslado(pvo_Conexion)
            vlo_DalOtfInventario = New DalOtfInventario(pvo_Conexion)

            Try

                If Not pvo_LineasBorrado.Tables(0) Is Nothing AndAlso pvo_LineasBorrado.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila In pvo_LineasBorrado.Tables(0).Rows
                        vlo_EntOttLineaTraslado = New EntOttLineaTraslado
                        vlo_EntOttLineaTraslado.Anno = vlo_fila(OTT_LINEA_TRASLADO.ANNO, DataRowVersion.Original).ToString
                        vlo_EntOttLineaTraslado.IdSolicitudTraslado = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_SOLICITUD_TRASLADO, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.IdUbicacion = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_UBICACION, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.IdAlmacen = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_ALMACEN, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.IdMaterial = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_MATERIAL, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.CantidadRequerida = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_REQUERIDA, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.CantidadRetirada = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_RETIRADA, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.Detalle = vlo_fila(OTT_LINEA_TRASLADO.DETALLE, DataRowVersion.Original).ToString
                        vlo_EntOttLineaTraslado.Estado = vlo_fila(OTT_LINEA_TRASLADO.ESTADO, DataRowVersion.Original).ToString
                        vlo_EntOttLineaTraslado.Usuario = vlo_fila(OTT_LINEA_TRASLADO.USUARIO, DataRowVersion.Original).ToString
                        vlo_EntOttLineaTraslado.TimeStamp = vlo_fila(OTT_LINEA_TRASLADO.TIME_STAMP, DataRowVersion.Original).ToString

                        vlo_DalOttLineaTraslado.BorrarRegistro(vlo_EntOttLineaTraslado)

                        vlo_EntOtfInventario = New EntOtfInventario
                        vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOttLineaTraslado.IdAlmacen, OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_EntOttLineaTraslado.IdUbicacion, OTF_INVENTARIO.ID_MATERIAL, vlo_EntOttLineaTraslado.IdMaterial))
                        If vlo_EntOtfInventario.Existe Then
                            vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada - vlo_EntOttLineaTraslado.CantidadRequerida
                            vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)
                        End If
                    Next
                ElseIf Not pvo_LineasAgregar.Tables(0) Is Nothing AndAlso pvo_LineasAgregar.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila In pvo_LineasAgregar.Tables(0).Rows
                        vlo_EntOttLineaTraslado = New EntOttLineaTraslado
                        vlo_EntOttLineaTraslado.Anno = CType(vlo_fila(OTT_LINEA_TRASLADO.ANNO), Integer)
                        vlo_EntOttLineaTraslado.IdSolicitudTraslado = CType(pvn_IdSolicitud, Integer)
                        vlo_EntOttLineaTraslado.IdUbicacion = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_UBICACION), Integer)
                        vlo_EntOttLineaTraslado.IdAlmacen = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_ALMACEN), Integer)
                        vlo_EntOttLineaTraslado.IdMaterial = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_MATERIAL), Integer)
                        vlo_EntOttLineaTraslado.CantidadRequerida = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_REQUERIDA), Integer)
                        vlo_EntOttLineaTraslado.CantidadRetirada = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_RETIRADA), Integer)
                        vlo_EntOttLineaTraslado.Detalle = vlo_fila(OTT_LINEA_TRASLADO.DETALLE).ToString
                        vlo_EntOttLineaTraslado.Estado = EstadoDetalle.PENDIENTE
                        vlo_EntOttLineaTraslado.Usuario = vlo_fila(OTT_LINEA_TRASLADO.USUARIO).ToString
                        vlo_EntOttLineaTraslado.TimeStamp = vlo_fila(OTT_LINEA_TRASLADO.TIME_STAMP).ToString

                        vlo_DalOttLineaTraslado.ModificarRegistro(vlo_EntOttLineaTraslado)

                        vlo_EntOtfInventario = New EntOtfInventario
                        vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOttLineaTraslado.IdAlmacen, OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_EntOttLineaTraslado.IdUbicacion, OTF_INVENTARIO.ID_MATERIAL, vlo_EntOttLineaTraslado.IdMaterial))
                        If vlo_EntOtfInventario.Existe Then
                            vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada - vlo_EntOttLineaTraslado.CantidadRequerida
                            vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)
                        End If
                    Next
                ElseIf Not pvo_SinCambios.Tables(0) Is Nothing AndAlso pvo_SinCambios.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila In pvo_SinCambios.Tables(0).Rows
                        vlo_EntOttLineaTraslado = New EntOttLineaTraslado
                        vlo_EntOttLineaTraslado.Anno = CType(vlo_fila(OTT_LINEA_TRASLADO.ANNO), Integer)
                        vlo_EntOttLineaTraslado.IdSolicitudTraslado = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_SOLICITUD_TRASLADO), Integer)
                        vlo_EntOttLineaTraslado.IdUbicacion = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_UBICACION), Integer)
                        vlo_EntOttLineaTraslado.IdAlmacen = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_ALMACEN), Integer)
                        vlo_EntOttLineaTraslado.IdMaterial = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_MATERIAL), Integer)
                        vlo_EntOttLineaTraslado.CantidadRequerida = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_REQUERIDA), Integer)
                        vlo_EntOttLineaTraslado.CantidadRetirada = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_RETIRADA), Integer)
                        vlo_EntOttLineaTraslado.Detalle = vlo_fila(OTT_LINEA_TRASLADO.DETALLE).ToString
                        vlo_EntOttLineaTraslado.Estado = EstadoDetalle.PENDIENTE
                        vlo_EntOttLineaTraslado.Usuario = vlo_fila(OTT_LINEA_TRASLADO.USUARIO).ToString
                        vlo_EntOttLineaTraslado.TimeStamp = vlo_fila(OTT_LINEA_TRASLADO.TIME_STAMP).ToString

                        vlo_EntOtfInventario = New EntOtfInventario
                        vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOttLineaTraslado.IdAlmacen, OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_EntOttLineaTraslado.IdUbicacion, OTF_INVENTARIO.ID_MATERIAL, vlo_EntOttLineaTraslado.IdMaterial))
                        If vlo_EntOtfInventario.Existe Then
                            vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada - vlo_EntOttLineaTraslado.CantidadRequerida
                            vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)
                        End If
                    Next
                End If
            Catch vlo_Excepcion As Exception
                Throw
            End Try
        End Sub


        ''' <summary>
        ''' Borrar los usuarios sin dependencias registrados en una unidad facturadora de la tabla UsuarioFacturacion  y de MembershipProvider 
        ''' </summary>
        ''' <param name="pvo_TelefonoBorrado"></param>
        ''' <param name="pvo_Conexion"></param>
        ''' <remarks></remarks>
        ''' <author>Jeannette Chavarría Rojas</author>
        ''' <creationDate>17/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Private Sub ModificarBorrarRegistroDeLineasAprobadas(pvo_LineasBorrado As DataSet, pvo_LineasAgregar As DataSet, pvo_SinCambios As DataSet, pvo_Conexion As ConexionOracle, pvn_IdSolicitud As Integer)
            Dim vlo_DalOttLineaTraslado As DalOttLineaTraslado
            Dim vlo_DalOtfInventario As DalOtfInventario
            Dim vlo_EntOttLineaTraslado As EntOttLineaTraslado
            Dim vlo_EntOtfInventario As EntOtfInventario

            vlo_DalOttLineaTraslado = New DalOttLineaTraslado(pvo_Conexion)
            vlo_DalOtfInventario = New DalOtfInventario(pvo_Conexion)

            Try

                If Not pvo_LineasBorrado.Tables(0) Is Nothing AndAlso pvo_LineasBorrado.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila In pvo_LineasBorrado.Tables(0).Rows
                        vlo_EntOttLineaTraslado = New EntOttLineaTraslado
                        vlo_EntOttLineaTraslado.Anno = vlo_fila(OTT_LINEA_TRASLADO.ANNO, DataRowVersion.Original).ToString
                        vlo_EntOttLineaTraslado.IdSolicitudTraslado = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_SOLICITUD_TRASLADO, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.IdUbicacion = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_UBICACION, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.IdAlmacen = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_ALMACEN, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.IdMaterial = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_MATERIAL, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.CantidadRequerida = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_REQUERIDA, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.CantidadRetirada = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_RETIRADA, DataRowVersion.Original).ToString, Integer)
                        vlo_EntOttLineaTraslado.Detalle = vlo_fila(OTT_LINEA_TRASLADO.DETALLE, DataRowVersion.Original).ToString
                        vlo_EntOttLineaTraslado.Estado = vlo_fila(OTT_LINEA_TRASLADO.ESTADO, DataRowVersion.Original).ToString
                        vlo_EntOttLineaTraslado.Usuario = vlo_fila(OTT_LINEA_TRASLADO.USUARIO, DataRowVersion.Original).ToString
                        vlo_EntOttLineaTraslado.TimeStamp = vlo_fila(OTT_LINEA_TRASLADO.TIME_STAMP, DataRowVersion.Original).ToString

                        vlo_DalOttLineaTraslado.BorrarRegistro(vlo_EntOttLineaTraslado)

                        vlo_EntOtfInventario = New EntOtfInventario
                        vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOttLineaTraslado.IdAlmacen, OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_EntOttLineaTraslado.IdUbicacion, OTF_INVENTARIO.ID_MATERIAL, vlo_EntOttLineaTraslado.IdMaterial))
                        If vlo_EntOtfInventario.Existe Then
                            vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada - vlo_EntOttLineaTraslado.CantidadRequerida
                            vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)
                        End If
                    Next
                ElseIf Not pvo_LineasAgregar.Tables(0) Is Nothing AndAlso pvo_LineasAgregar.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila In pvo_LineasAgregar.Tables(0).Rows
                        vlo_EntOttLineaTraslado = New EntOttLineaTraslado
                        vlo_EntOttLineaTraslado.Anno = CType(vlo_fila(OTT_LINEA_TRASLADO.ANNO), Integer)
                        vlo_EntOttLineaTraslado.IdSolicitudTraslado = CType(pvn_IdSolicitud, Integer)
                        vlo_EntOttLineaTraslado.IdUbicacion = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_UBICACION), Integer)
                        vlo_EntOttLineaTraslado.IdAlmacen = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_ALMACEN), Integer)
                        vlo_EntOttLineaTraslado.IdMaterial = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_MATERIAL), Integer)
                        vlo_EntOttLineaTraslado.CantidadRequerida = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_REQUERIDA), Integer)
                        vlo_EntOttLineaTraslado.CantidadRetirada = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_RETIRADA), Integer)
                        vlo_EntOttLineaTraslado.Detalle = vlo_fila(OTT_LINEA_TRASLADO.DETALLE).ToString
                        vlo_EntOttLineaTraslado.Estado = EstadoDetalle.APROBADO
                        vlo_EntOttLineaTraslado.Usuario = vlo_fila(OTT_LINEA_TRASLADO.USUARIO).ToString
                        vlo_EntOttLineaTraslado.TimeStamp = vlo_fila(OTT_LINEA_TRASLADO.TIME_STAMP).ToString

                        vlo_DalOttLineaTraslado.InsertarRegistro(vlo_EntOttLineaTraslado)

                        vlo_EntOtfInventario = New EntOtfInventario
                        vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOttLineaTraslado.IdAlmacen, OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_EntOttLineaTraslado.IdUbicacion, OTF_INVENTARIO.ID_MATERIAL, vlo_EntOttLineaTraslado.IdMaterial))
                        If vlo_EntOtfInventario.Existe Then
                            vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada - vlo_EntOttLineaTraslado.CantidadRequerida
                            vlo_EntOtfInventario.CantidadDisponible = vlo_EntOtfInventario.CantidadDisponible - vlo_EntOttLineaTraslado.CantidadRequerida
                            vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)
                        End If
                    Next
                ElseIf Not pvo_SinCambios.Tables(0) Is Nothing AndAlso pvo_SinCambios.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila In pvo_SinCambios.Tables(0).Rows
                        vlo_EntOttLineaTraslado = New EntOttLineaTraslado
                        vlo_EntOttLineaTraslado.Anno = CType(vlo_fila(OTT_LINEA_TRASLADO.ANNO), Integer)
                        vlo_EntOttLineaTraslado.IdSolicitudTraslado = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_SOLICITUD_TRASLADO), Integer)
                        vlo_EntOttLineaTraslado.IdUbicacion = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_UBICACION), Integer)
                        vlo_EntOttLineaTraslado.IdAlmacen = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_ALMACEN), Integer)
                        vlo_EntOttLineaTraslado.IdMaterial = CType(vlo_fila(OTT_LINEA_TRASLADO.ID_MATERIAL), Integer)
                        vlo_EntOttLineaTraslado.CantidadRequerida = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_REQUERIDA), Integer)
                        vlo_EntOttLineaTraslado.CantidadRetirada = CType(vlo_fila(OTT_LINEA_TRASLADO.CANTIDAD_RETIRADA), Integer)
                        vlo_EntOttLineaTraslado.Detalle = vlo_fila(OTT_LINEA_TRASLADO.DETALLE).ToString
                        vlo_EntOttLineaTraslado.Estado = EstadoDetalle.APROBADO
                        vlo_EntOttLineaTraslado.Usuario = vlo_fila(OTT_LINEA_TRASLADO.USUARIO).ToString
                        vlo_EntOttLineaTraslado.TimeStamp = vlo_fila(OTT_LINEA_TRASLADO.TIME_STAMP).ToString

                        vlo_DalOttLineaTraslado.ModificarRegistro(vlo_EntOttLineaTraslado)

                        vlo_EntOtfInventario = New EntOtfInventario
                        vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOttLineaTraslado.IdAlmacen, OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_EntOttLineaTraslado.IdUbicacion, OTF_INVENTARIO.ID_MATERIAL, vlo_EntOttLineaTraslado.IdMaterial))
                        If vlo_EntOtfInventario.Existe Then
                            vlo_EntOtfInventario.CantidadDisponible = vlo_EntOtfInventario.CantidadDisponible - vlo_EntOttLineaTraslado.CantidadRequerida
                            vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada - vlo_EntOttLineaTraslado.CantidadRequerida
                            vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)
                        End If
                    Next
                End If
            Catch vlo_Excepcion As Exception
                Throw
            End Try
        End Sub
#End Region

    End Class
End Namespace
