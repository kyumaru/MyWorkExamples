Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
    Public Class BllOtfInventario
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
        ''' Permite agregar un registro en la tabla OTF_INVENTARIO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>29/05/2016 02:56:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtfInventario) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfInventario As DalOtfInventario
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdAlmacenBodega, pvo_Registro.IdUbicacionAdministra, pvo_Registro.IdMaterial).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
                End If

                vlo_DalOtfInventario = New DalOtfInventario(vlo_Conexion)
                vln_Resultado = vlo_DalOtfInventario.InsertarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTF_INVENTARIO, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>29/05/2016 02:56:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtfInventario) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfInventario As DalOtfInventario
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdAlmacenBodega, pvo_Registro.IdUbicacionAdministra, pvo_Registro.IdMaterial) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Hay registro asociados")
                End If

                vlo_DalOtfInventario = New DalOtfInventario(vlo_Conexion)
                vln_Resultado = vlo_DalOtfInventario.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvn_IdAlmacenBodega">Llave primaria de la tabla otm_almacen_bodega que se asocia con la secuencia sq_id_almacen_bodega</param>
        ''' <param name="pvn_IdUbicacionAdministra">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvn_IdMaterial">Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>29/05/2016 02:56:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdAlmacenBodega As Integer, pvn_IdUbicacionAdministra As Integer, pvn_IdMaterial As Integer) As EntOtfInventario
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfInventario As DalOtfInventario

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfInventario = New DalOtfInventario(vlo_Conexion)
                Return vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, pvn_IdAlmacenBodega, Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTF_INVENTARIO.ID_MATERIAL, pvn_IdMaterial))
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
        ''' <param name="pvn_IdAlmacenBodega">Llave primaria de la tabla otm_almacen_bodega que se asocia con la secuencia sq_id_almacen_bodega</param>
        ''' <param name="pvn_IdUbicacionAdministra">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvn_IdMaterial">Llave primaria de la tabla otm_material. consecutivo de 1 a n para cada ubicación</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>29/05/2016 02:56:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdAlmacenBodega As Integer, pvn_IdUbicacionAdministra As Integer, pvn_IdMaterial As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            'Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial

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

                'Determinar la existencia de registros asociados en la tabla OTT_DETALLE_MATERIAL
                'vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                'If vlo_DalOttDetalleMaterial.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTT_DETALLE_MATERIAL.ID_ALMACEN_BODEGA, pvn_IdAlmacenBodega, Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL, pvn_IdMaterial)).Existe Then
                '	Return True
                'End If

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
        ''' Procesa la salida de material del inventario
        ''' </summary>
        ''' <param name="pvc_idOrdenTrabajo"></param>
        ''' <param name="pvn_PvnIdalmacenbodega"></param>
        ''' <param name="pvn_PvnIdubicacionadministra"></param>
        ''' <param name="pvn_PvnIdmaterial"></param>
        ''' <param name="pvn_PvnCantidadretiro"></param>
        ''' <param name="pvc_PvcUsuario"></param>
        ''' <param name="pvd_PvdTimestamp"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Cesar Bermudez G</author>
        ''' <creationDate>04/07/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function RetiroMaterial(pvc_idOrdenTrabajo As String, pvn_idDetalleMaterial As Integer, pvn_PvnIdalmacenbodega As Integer, pvn_PvnIdubicacionadministra As Integer, pvn_PvnIdmaterial As Integer, pvn_PvnCantidadretiro As Double, pvc_PvcUsuario As String, pvd_PvdTimestamp As DateTime) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfInventario As DalOtfInventario
            Dim vlo_DalOttOrdenTrabajo As AccesoDatos.OrdenesDeTrabajo.DalOttOrdenTrabajo
            Dim vlo_DalOttDetalleMaterial As AccesoDatos.OrdenesDeTrabajo.DalOttDetalleMaterial
            Dim vlo_EntOttOrdenTrabajo As EntidadNegocio.OrdenesDeTrabajo.EntOttOrdenTrabajo
            Dim vlo_EntOttDetalleMaterial As EntidadNegocio.OrdenesDeTrabajo.EntOttDetalleMaterial
            Dim vlo_resultado As Integer
            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfInventario = New DalOtfInventario(vlo_Conexion)
                vlo_DalOttOrdenTrabajo = New AccesoDatos.OrdenesDeTrabajo.DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttDetalleMaterial = New AccesoDatos.OrdenesDeTrabajo.DalOttDetalleMaterial(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()

                'Metodo que calcula si es posible reservar la cantidad en inventario, de ser así la reserva
                vlo_DalOtfInventario.EjecutarPrOtInventario(pvn_PvnIdalmacenbodega, pvn_PvnIdubicacionadministra, pvn_PvnIdmaterial, pvn_PvnCantidadretiro, pvc_PvcUsuario, pvd_PvdTimestamp)
                vlo_EntOttDetalleMaterial = vlo_DalOttDetalleMaterial.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL, pvn_idDetalleMaterial))
                If vlo_EntOttDetalleMaterial.Existe Then
                    vlo_EntOttDetalleMaterial.CantidadReservada = pvn_PvnCantidadretiro
                    vlo_resultado = vlo_DalOttDetalleMaterial.ModificarRegistro(vlo_EntOttDetalleMaterial)
                    If vlo_resultado > 0 Then

                        vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = '{1}' AND {2} = {3}", Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo, Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_PvnIdubicacionadministra))

                        If vlo_EntOttOrdenTrabajo IsNot Nothing AndAlso vlo_EntOttOrdenTrabajo.Existe Then
                            If vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.MATERIAL_PENDIENTE_COMPRA Then
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EN_PROCESO
                            Else
                                vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.SOLICITUD_MATERIALES_ENTREGADA
                            End If
                            vlo_EntOttOrdenTrabajo.Usuario = pvc_PvcUsuario
                            vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)
                            vlo_resultado = 1
                        End If
                    End If
                End If
                vlo_Conexion.TransaccionCommit()
                Return vlo_resultado
            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Function ProcesoRetiroMaterial(pvc_idOrdenTrabajo As String, pvn_idDetalleMaterial As Integer, pvn_PvnIdalmacenbodega As Integer, pvn_PvnIdubicacionadministra As Integer, pvn_PvnIdmaterial As Integer, pvn_PvnCantidadretiro As Double, pvc_PvcUsuario As String, pvn_PvnAnno As Integer, pvn_PvnIdSolicutRetiro As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfInventario As DalOtfInventario
            Dim vlo_DalOttOrdenTrabajo As AccesoDatos.OrdenesDeTrabajo.DalOttOrdenTrabajo
            Dim vlo_DalOttDetalleMaterial As AccesoDatos.OrdenesDeTrabajo.DalOttDetalleMaterial
            Dim vlo_DalOttDetalleRetiro As AccesoDatos.OrdenesDeTrabajo.DalOttDetalleRetiro
            Dim vlo_EntOttOrdenTrabajo As EntidadNegocio.OrdenesDeTrabajo.EntOttOrdenTrabajo
            Dim vlo_EntOttDetalleMaterial As EntidadNegocio.OrdenesDeTrabajo.EntOttDetalleMaterial
            Dim vlo_EntOttDetalleRetiro As EntidadNegocio.OrdenesDeTrabajo.EntOttDetalleRetiro
            Dim vlo_EntOtfInventario As EntOtfInventario
            Dim vlo_resultado As Integer
            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfInventario = New DalOtfInventario(vlo_Conexion)
                vlo_DalOttOrdenTrabajo = New AccesoDatos.OrdenesDeTrabajo.DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttDetalleMaterial = New AccesoDatos.OrdenesDeTrabajo.DalOttDetalleMaterial(vlo_Conexion)
                vlo_DalOttDetalleRetiro = New AccesoDatos.OrdenesDeTrabajo.DalOttDetalleRetiro(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()

                vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                                  Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, pvn_PvnIdalmacenbodega,
                                  Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, pvn_PvnIdubicacionadministra,
                                  Modelo.OTF_INVENTARIO.ID_MATERIAL, pvn_PvnIdmaterial))

                vlo_EntOttDetalleRetiro = vlo_DalOttDetalleRetiro.ObtenerRegistro(
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                                   Modelo.OTT_DETALLE_RETIRO.ANNO, pvn_PvnAnno,
                                  Modelo.OTT_DETALLE_RETIRO.ID_SOLICITUD_RETIRO, pvn_PvnIdSolicutRetiro,
                                  Modelo.OTT_DETALLE_RETIRO.ID_DETALLE_MATERIAL, pvn_idDetalleMaterial))

                vlo_EntOttDetalleMaterial = vlo_DalOttDetalleMaterial.ObtenerRegistro(
                    String.Format("{0} = {1}", Modelo.OTT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL, pvn_idDetalleMaterial))

                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(
                    String.Format("{0} = {1} AND {2} = '{3}'",
                                  Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_PvnIdubicacionadministra,
                                  Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo))

                If vlo_EntOtfInventario.Existe Then

                    If vlo_EntOttDetalleRetiro.Existe Then

                        If vlo_EntOttDetalleMaterial.Existe Then

                            If (vlo_EntOtfInventario.CantidadDisponible) >= pvn_PvnCantidadretiro Then

                                vlo_EntOtfInventario.CantidadDisponible = vlo_EntOtfInventario.CantidadDisponible - pvn_PvnCantidadretiro
                                vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada - pvn_PvnCantidadretiro
                                vlo_EntOtfInventario.Usuario = pvc_PvcUsuario

                                vlo_EntOttDetalleRetiro.CantidadRetirada = vlo_EntOttDetalleRetiro.CantidadRetirada + pvn_PvnCantidadretiro
                                vlo_EntOttDetalleRetiro.Usuario = pvc_PvcUsuario

                                If vlo_EntOttDetalleRetiro.CantidadSolicitada = vlo_EntOttDetalleRetiro.CantidadRetirada Then
                                    vlo_EntOttDetalleRetiro.Estado = "RET"
                                End If

                                vlo_EntOttDetalleMaterial.CantidadRetirada = vlo_EntOttDetalleMaterial.CantidadRetirada + pvn_PvnCantidadretiro
                                vlo_EntOttDetalleMaterial.Usuario = pvc_PvcUsuario

                                If vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.MATERIAL_PENDIENTE_COMPRA Or vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PARA_RETIRO_MATERIAL Then
                                    vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EN_PROCESO
                                    vlo_EntOttOrdenTrabajo.Usuario = pvc_PvcUsuario

                                    vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)
                                End If

                                vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)
                                vlo_DalOttDetalleRetiro.ModificarRegistro(vlo_EntOttDetalleRetiro)
                                vlo_DalOttDetalleMaterial.ModificarRegistro(vlo_EntOttDetalleMaterial)

                            End If
                        End If
                    End If
                End If

                vlo_Conexion.TransaccionCommit()
                Return vlo_resultado
            Catch ex As Exception
                Throw
            End Try
        End Function



        Public Function ActualizaSolicitudRetiroMaterial(pvc_PvcUsuario As String, pvn_PvnAnno As Integer, pvn_PvnIdSolicutRetiro As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttSolicitudRetiro As AccesoDatos.OrdenesDeTrabajo.DalOttSolicitudRetiro
            Dim vlo_DalOttDetalleRetiro As AccesoDatos.OrdenesDeTrabajo.DalOttDetalleRetiro
            Dim vlo_EntOttSolicitudRetiro As EntidadNegocio.OrdenesDeTrabajo.EntOttSolicitudRetiro
            Dim vlo_DsDetallesRetiro As Data.DataSet
            Dim vlb_Bandera As Boolean = True
           
            Dim vlo_resultado As Integer
            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttSolicitudRetiro = New AccesoDatos.OrdenesDeTrabajo.DalOttSolicitudRetiro(vlo_Conexion)
                vlo_DalOttDetalleRetiro = New AccesoDatos.OrdenesDeTrabajo.DalOttDetalleRetiro(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()

                vlo_EntOttSolicitudRetiro = vlo_DalOttSolicitudRetiro.ObtenerRegistro(
                    String.Format("{0} = {1} AND {2} = {3}",
                                   Modelo.OTT_SOLICITUD_RETIRO.ANNO, pvn_PvnAnno,
                                  Modelo.OTT_SOLICITUD_RETIRO.ID_SOLICITUD_RETIRO, pvn_PvnIdSolicutRetiro))

                vlo_DsDetallesRetiro = vlo_DalOttDetalleRetiro.ListarRegistros(
                    String.Format(String.Format("{0} = {1} AND {2} = {3}",
                                   Modelo.OTT_DETALLE_RETIRO.ANNO, pvn_PvnAnno,
                                  Modelo.OTT_DETALLE_RETIRO.ID_SOLICITUD_RETIRO, pvn_PvnIdSolicutRetiro)),
                    String.Empty, False, 0, 0)

                If vlo_DsDetallesRetiro IsNot Nothing AndAlso vlo_DsDetallesRetiro.Tables(0).Rows.Count > 0 Then

                    For Each vlo_FilaDs In vlo_DsDetallesRetiro.Tables(0).Rows
                        If vlo_FilaDs(Modelo.OTT_DETALLE_RETIRO.CANTIDAD_SOLICITADA).ToString <> vlo_FilaDs(Modelo.OTT_DETALLE_RETIRO.CANTIDAD_RETIRADA).ToString Then
                            vlb_Bandera = False
                        End If
                    Next


                    'If vlo_DsDetallesRetiro.Tables(0).Rows(Modelo.OTT_DETALLE_RETIRO.CANTIDAD_SOLICITADA).ToString <> vlo_DsDetallesRetiro.Tables(0).Rows(Modelo.OTT_DETALLE_RETIRO.CANTIDAD_RETIRADA).ToString Then
                    '    vlb_Bandera = False
                    'End If

                End If

                If vlb_Bandera Then

                    vlo_EntOttSolicitudRetiro.EstadoSolicitudRetiro = EstadoOrden.SOLICITUD_MATERIALES_ENTREGADA
                    vlo_EntOttSolicitudRetiro.Usuario = pvc_PvcUsuario

                    vlo_DalOttSolicitudRetiro.ModificarRegistro(vlo_EntOttSolicitudRetiro)

                End If

                vlo_Conexion.TransaccionCommit()
                Return vlo_resultado
            Catch ex As Exception
                Throw
            End Try
        End Function


#End Region

    End Class
End Namespace
