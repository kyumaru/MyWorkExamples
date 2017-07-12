Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Configuration
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttDetalleMaterial
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
        ''' Permite agregar un registro en la tabla OTT_DETALLE_MATERIAL, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttDetalleMaterial) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdDetalleMaterial).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("El material a ingresar ya existe.")
                End If

                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                vln_Resultado = vlo_DalOttDetalleMaterial.InsertarRegistro(pvo_Registro)
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
        ''' Permite agregar un registro en la tabla OTT_DETALLE_MATERIAL, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>19/07/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarDetalleModificarSolicitud(ByVal pvo_Registro As EntOttDetalleMaterial) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vlo_DalOttSolicitudMaterial As DalOttSolicitudMaterial
            Dim vlo_EntOttSolicitudMaterial As EntOttSolicitudMaterial
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdDetalleMaterial).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("El material a ingresar ya existe.")
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttSolicitudMaterial = New DalOttSolicitudMaterial(vlo_Conexion)
                vlo_EntOttSolicitudMaterial = vlo_DalOttSolicitudMaterial.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_SOLICITUD_MATERIAL.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTT_SOLICITUD_MATERIAL.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo))

                vlo_EntOttSolicitudMaterial.EstadoSolMaterial = EstadoSolicitudMaterial.INGRESADO_POR_COORDINADOR
                vlo_EntOttSolicitudMaterial.Usuario = pvo_Registro.Usuario

                vlo_DalOttSolicitudMaterial.ModificarRegistro(vlo_EntOttSolicitudMaterial)

                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                vln_Resultado = vlo_DalOttDetalleMaterial.InsertarRegistro(pvo_Registro)

                vlo_Conexion.TransaccionCommit()

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
        ''' administra el proceso de envio a aprobacion
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso;  Menor a cero: El proceso ha fallado</returns>
        ''' <author>Crlos Gómez Ondoy</author>
        ''' <creationDate>27/06/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function EnviarAprobacion(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvc_Justificacion As String, pvc_UserName As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vln_Resultado As Integer = -1
            Dim vlo_DalOttSolicitudMaterial As DalOttSolicitudMaterial
            Dim vlo_DsOttDetalleMaterial As Data.DataSet
            Dim vlo_EntOttSolicitudMaterial As EntOttSolicitudMaterial

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                vlo_DalOttSolicitudMaterial = New DalOttSolicitudMaterial(vlo_Conexion)

                vlo_DsOttDetalleMaterial = vlo_DalOttDetalleMaterial.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'", Modelo.V_OTT_DETALLE_MATERIAL.ID_UBICACION, pvn_IdUbicacion, Modelo.V_OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo, Modelo.V_OTT_DETALLE_MATERIAL.ESTADO, EstadoRegistro.PENDIENTE_ENVIO), String.Empty, False, 0, 0)
                vlo_EntOttSolicitudMaterial = vlo_DalOttSolicitudMaterial.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_SOLICITUD_MATERIAL.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_SOLICITUD_MATERIAL.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo))

                For Each vlo_FilaOttDetalle In vlo_DsOttDetalleMaterial.Tables(0).Rows
                    vlo_FilaOttDetalle(Modelo.OTT_DETALLE_MATERIAL.ESTADO) = EstadoRegistro.PENDIENTE_APROBACION
                Next

                vlo_DalOttDetalleMaterial.AdapterOttDetalleMaterial(vlo_DsOttDetalleMaterial)

                vlo_EntOttSolicitudMaterial.EstadoSolMaterial = EstadoSolicitudMaterial.INGRESADO_POR_COORDINADOR
                vlo_EntOttSolicitudMaterial.HistorialJustificacion = String.Format("{0}¬{1}: {2}", vlo_EntOttSolicitudMaterial.HistorialJustificacion, DateTime.Now.ToString(Constantes.FORMATO_FECHA_UI), pvc_Justificacion)
                vlo_EntOttSolicitudMaterial.Usuario = pvc_UserName

                vlo_DalOttSolicitudMaterial.ModificarRegistro(vlo_EntOttSolicitudMaterial)

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1

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
        ''' 
        ''' </summary>
        ''' <param name="pvo_EntOttSolicitudMaterial"></param>
        ''' <param name="pvo_EntOttDetalleMaterial"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>21/06/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarSolicitudDetalle(pvo_EntOttSolicitudMaterial As EntOttSolicitudMaterial, pvo_EntOttDetalleMaterial As EntOttDetalleMaterial) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vlo_DalOttSolicitudMaterial As DalOttSolicitudMaterial
            Dim vln_Resultado As Integer = 0

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_EntOttDetalleMaterial.IdDetalleMaterial).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("El material a ingresar ya existe.")
                End If

                If ObtenerRegistroPorLlavePrimariaSolicitud(pvo_EntOttSolicitudMaterial.IdUbicacion, pvo_EntOttSolicitudMaterial.IdOrdenTrabajo).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("La solicitud a ingresar ya existe.")
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttSolicitudMaterial = New DalOttSolicitudMaterial(vlo_Conexion)
                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)

                vlo_DalOttSolicitudMaterial.InsertarRegistro(pvo_EntOttSolicitudMaterial)
                vlo_DalOttDetalleMaterial.InsertarRegistro(pvo_EntOttDetalleMaterial)

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
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
        ''' <param name="pvc_IdOrdenTrabajo">Identificador único alfanumérico de la orden de trabajo</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Carlos gómez Ondoy</author>
        ''' <creationDate>20/06/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimariaSolicitud(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String) As EntOttSolicitudMaterial
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttSolicitudMaterial As DalOttSolicitudMaterial

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttSolicitudMaterial = New DalOttSolicitudMaterial(vlo_Conexion)
                Return vlo_DalOttSolicitudMaterial.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTT_SOLICITUD_MATERIAL.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_SOLICITUD_MATERIAL.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper()))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Permite obtener un registro según su llave primaria
        ''' </summary>
        ''' <param name="pvn_IdDetalleMaterial">Llave primaria de la tabla ott_detalle_material que se asocia con la secuencia sq_id_detalle_material</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdDetalleMaterial As Integer) As EntOttDetalleMaterial
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                Return vlo_DalOttDetalleMaterial.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL, pvn_IdDetalleMaterial))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="pvo_DsMateriales"></param>
        ''' <param name="pvc_idOrdenTrabajo"></param>
        ''' <param name="pvn_idUbicacion"></param>
        ''' <param name="pvb_aprobarSolicitar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>15/6/2016</creationDate>
        ''' <changeLog>Carlos Gómez Ondoy / cambio de la lógica de todo el método</changeLog>
        Public Function RespuestaRevisionRequisiciones(pvo_DsMateriales As DataSet, pvc_idOrdenTrabajo As String, pvn_idUbicacion As Integer, pvc_observaciones As String, pvn_NumEmpleado As Integer, pvb_aprobar As Boolean, pvb_Solicitar As Boolean, pvb_Rechazar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_fila() As DataRow
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_DalOttSolicitudMaterial As DalOttSolicitudMaterial
            Dim vlo_EntOttSolicitudMaterial As EntOttSolicitudMaterial
            Dim vlo_DalOtfInventario As DalOtfInventario
            Dim vlo_EntOtfInventario As EntOtfInventario
            Dim vlo_Usuario As WsrEU_Curriculo.EntEmpleados
            Dim vln_resultado As Integer = 0
            Dim vlo_DsDatos As DataSet

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                vlo_DalOttSolicitudMaterial = New DalOttSolicitudMaterial(vlo_Conexion)
                vlo_DalOtfInventario = New DalOtfInventario(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()

                vlo_EntOttSolicitudMaterial = vlo_DalOttSolicitudMaterial.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'",
                                                    Modelo.OTT_SOLICITUD_MATERIAL.ID_UBICACION, pvn_idUbicacion,
                                                    Modelo.OTT_SOLICITUD_MATERIAL.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo))

                vlo_Usuario = CargarFuncionario(pvn_NumEmpleado)
                vlo_EntOttSolicitudMaterial.Usuario = vlo_Usuario.ID_PERSONAL
                vlo_EntOttSolicitudMaterial.EstadoSolMaterial = IIf(pvb_aprobar, EstadoSolicitudMaterial.APROBADA, EstadoSolicitudMaterial.APROBACION_DE_PRESUPUESTO)
                vlo_DalOttSolicitudMaterial.ModificarRegistro(vlo_EntOttSolicitudMaterial)

                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = '{1}' AND {2} = {3}",
                                                Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo,
                                                Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_idUbicacion))

                vlo_DsDatos = vlo_DalOttDetalleMaterial.ListarRegistros(String.Format("{0} = '{1}' AND {2} = {3} AND {4} = '{5}'",
                                        Modelo.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo,
                                        Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION, pvn_idUbicacion,
                                        Modelo.OTT_DETALLE_MATERIAL.ESTADO, EstadoDetalle.PENDIENTE),
                                        String.Empty, False, 0, 0)

                If pvb_aprobar Then
                    For Each vlo_FilaDetalleMaterial In vlo_DsDatos.Tables(0).Rows
                        If vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.VIA_DESPACHO).ToString <> ViaDespacho.VIACOMPRA Then
                            vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                                                        Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_ALMACEN_BODEGA),
                                                        Modelo.OTF_INVENTARIO.ID_MATERIAL, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL),
                                                        Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA)))

                            If vlo_EntOtfInventario.Existe Then

                                If CType(vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA), Double) = 0 Then

                                    If (vlo_EntOtfInventario.CantidadDisponible - vlo_EntOtfInventario.CantidadReservada) >= CType(vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA), Double) Then
                                        vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada + CType(vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA), Double)
                                        vlo_EntOtfInventario.Usuario = vlo_Usuario.ID_PERSONAL
                                        vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)

                                        vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA) = vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA)
                                    Else
                                        Throw New OrdenesDeTrabajoException("La bodega o almacén seleccionada para alguno de los materiales no posee suficiente material.")
                                    End If
                                End If
                            Else
                                Throw New OrdenesDeTrabajoException("La bodega o almacén seleccionada para alguno de los materiales no posee suficiente material.")
                            End If
                        End If
                    Next
                End If

                If pvb_Solicitar Then
                    For Each vlo_FilaDetalleMaterial In vlo_DsDatos.Tables(0).Rows
                        If vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.VIA_DESPACHO).ToString <> ViaDespacho.VIACOMPRA Then
                            vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                                                        Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_ALMACEN_BODEGA),
                                                        Modelo.OTF_INVENTARIO.ID_MATERIAL, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL),
                                                        Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA)))

                            If vlo_EntOtfInventario.Existe Then

                                If CType(vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA), Double) > 0 Then
                                    vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada - CType(vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA), Double)
                                    vlo_EntOtfInventario.Usuario = vlo_Usuario.ID_PERSONAL
                                    vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)

                                    vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA) = 0
                                End If
                            End If
                        End If
                    Next
                End If

                If pvb_Rechazar Then
                    For Each vlo_FilaDetalleMaterial In vlo_DsDatos.Tables(0).Rows
                        If vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.VIA_DESPACHO).ToString <> ViaDespacho.VIACOMPRA Then
                            vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                                                        Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_ALMACEN_BODEGA),
                                                        Modelo.OTF_INVENTARIO.ID_MATERIAL, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL),
                                                        Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA)))

                            If vlo_EntOtfInventario.Existe Then

                                If CType(vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA), Double) > 0 Then
                                    vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada - CType(vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA), Double)
                                    vlo_EntOtfInventario.Usuario = vlo_Usuario.ID_PERSONAL
                                    vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)

                                    vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA) = 0
                                End If
                            End If
                        End If
                    Next
                End If

                If pvb_aprobar Then
                    vlo_fila = pvo_DsMateriales.Tables(0).Select(String.Format("{0} <> '{1}' AND {0} <> '{2}'", Modelo.V_OTT_DETALLE_MATERIALLST.VIA_DESPACHO, Tipo.ALMACEN, Tipo.BODEGA))

                    If vlo_fila.Length >= 1 Then
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.MATERIAL_PENDIENTE_COMPRA
                    Else
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PARA_RETIRO_MATERIAL
                    End If

                    For Each vlo_filaListado As DataRow In vlo_DsDatos.Tables(0).Rows
                        vlo_filaListado(Modelo.OTT_DETALLE_MATERIAL.ESTADO) = EstadoDetalle.APROBADO
                        vlo_filaListado(Modelo.OTT_DETALLE_MATERIAL.USUARIO) = vlo_Usuario.ID_PERSONAL
                    Next
                End If

                If pvb_Solicitar Then
                    vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.REVISION_PRESUPUESTO_SUPERVISOR

                    For Each vlo_filaListado As DataRow In vlo_DsDatos.Tables(0).Rows
                        vlo_filaListado(Modelo.OTT_DETALLE_MATERIAL.USUARIO) = vlo_Usuario.ID_PERSONAL
                    Next
                End If

                If pvb_Rechazar Then
                    vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EN_EVALUACION

                    For Each vlo_filaListado As DataRow In vlo_DsDatos.Tables(0).Rows
                        vlo_filaListado(Modelo.OTT_DETALLE_MATERIAL.USUARIO) = vlo_Usuario.ID_PERSONAL
                    Next
                End If

                vlo_DalOttDetalleMaterial.AdapterOttDetalleMaterialRequisicion(vlo_DsDatos)

                vlo_EntOttOrdenTrabajo.Usuario = vlo_Usuario.ID_PERSONAL

                vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                If pvb_Solicitar Then
                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = vlo_EntOttOrdenTrabajo.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = vlo_EntOttOrdenTrabajo.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = EstadoOrden.REVISION_PRESUPUESTO_SUPERVISOR
                    vlo_EntOttTrazabilidadProceso.ObservacionesInternas = pvc_observaciones
                    vlo_EntOttTrazabilidadProceso.Usuario = vlo_Usuario.ID_PERSONAL

                    vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                End If

                If pvb_Rechazar Then
                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = vlo_EntOttOrdenTrabajo.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = vlo_EntOttOrdenTrabajo.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = EstadoOrden.RECHAZADA
                    vlo_EntOttTrazabilidadProceso.ObservacionesInternas = pvc_observaciones
                    vlo_EntOttTrazabilidadProceso.Usuario = vlo_Usuario.ID_PERSONAL

                    vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                End If

                vln_resultado = 1
                vlo_Conexion.TransaccionCommit()

                Return vln_resultado
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="pvo_DsMateriales"></param>
        ''' <param name="pvc_idOrdenTrabajo"></param>
        ''' <param name="pvn_idUbicacion"></param>
        ''' <param name="pvb_aprobarSolicitar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>17/6/2016</creationDate>
        ''' <changeLog>Carlos Gómez Ondoy -- 30/09/2016 -- Se cambia la lógica de todo el método</changeLog>
        Public Function RevisionRequisicionesSupervisor(pvc_idOrdenTrabajo As String, pvn_idUbicacion As Integer, pvc_observaciones As String, pvn_NumEmpleado As Integer, pvc_Accion As String, pvb_SolicitudPresupuesto As Boolean, pvo_ArchivoRespuesta As EntOttAdjuntoOrdenTrabajo, pvn_IdArchivoSolicitud As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vlo_DalOtmSectorTaller As DalOtmSectorTaller
            Dim vlo_DalOtpParametroUbicacion As DalOtpParametroUbicacion
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_EntOtmSectorTaller As EntOtmSectorTaller
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vlo_DalOttSolicitudMaterial As DalOttSolicitudMaterial
            Dim vlo_EntOttSolicitudMaterial As EntOttSolicitudMaterial
            Dim vlo_Usuario As WsrEU_Curriculo.EntEmpleados
            Dim vlo_numCoordinador As Integer
            Dim vlc_CorreoAdministrador As String
            Dim vln_resultado As Integer = 0
            Dim vlo_DsParametros As DataSet
            Dim vlo_DsDetalles As DataSet
            Dim vln_IdRespuesta As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vlo_DalOtmSectorTaller = New DalOtmSectorTaller(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New DalOtpParametroUbicacion(vlo_Conexion)
                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                vlo_DalOttSolicitudMaterial = New DalOttSolicitudMaterial(vlo_Conexion)
                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()

                vlo_Usuario = CargarFuncionario(pvn_NumEmpleado)

                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = '{1}' AND {2} = {3}", Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo, Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_idUbicacion))

                If pvb_SolicitudPresupuesto Then

                    'If pvo_ArchivoSolicitud.Archivo IsNot Nothing Then
                    '    vlo_DalOttAdjuntoOrdenTrabajo.InsertarRegistro(pvo_ArchivoSolicitud)
                    'End If

                    If pvo_ArchivoRespuesta.Archivo IsNot Nothing Then
                        vln_IdRespuesta = vlo_DalOttAdjuntoOrdenTrabajo.InsertarRegistro(pvo_ArchivoRespuesta)
                    End If
                End If

                Select Case pvc_Accion
                    Case "0"
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.LIQUIDADA
                    Case "1"
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PENDIENTE_APROBACION_REQUISICION
                    Case "2"
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EN_EVALUACION
                End Select

                vlo_EntOttOrdenTrabajo.Usuario = vlo_Usuario.ID_PERSONAL
                vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                If pvc_Accion = "0" Then
                    'Obtiene el correo del administrador del sistema
                    vlo_DsParametros = vlo_DalOtpParametroUbicacion.ListarRegistrosLista(
                        String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR),
                        String.Empty, False, 0, 0)

                    If vlo_DsParametros.Tables.Count > 0 AndAlso vlo_DsParametros.Tables(0).Rows.Count > 0 Then
                        vlc_CorreoAdministrador = vlo_DsParametros.Tables(0).Rows(0).Item(Modelo.OTP_PARAMETRO_UBICACION.VALOR)
                        vlo_EntOtmSectorTaller = vlo_DalOtmSectorTaller.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, vlo_EntOttOrdenTrabajo.IdSectorTaller))
                        If vlo_EntOtmSectorTaller.Existe Then
                            EnviarNotificacionLiquidada(vlo_EntOttOrdenTrabajo, vlo_EntOtmSectorTaller.NumEmpleadoCoordinador, pvc_observaciones, vlc_CorreoAdministrador)
                        End If
                    End If
                End If

                If pvc_Accion = "1" Then
                    'Obtiene las lineas del detalle material para cambiarles el estado 
                    vlo_DsDetalles = vlo_DalOttDetalleMaterial.ListarRegistros(String.Format("{0} = '{1}' AND {2} = {3}",
                                    Modelo.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo, Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION, pvn_idUbicacion), String.Empty, False, 0, 0)

                    For Each vlo_fila In vlo_DsDetalles.Tables(0).Rows
                        vlo_fila(Modelo.OTT_DETALLE_MATERIAL.ESTADO) = EstadoRegistro.PENDIENTE_APROBACION
                        vlo_fila(Modelo.OTT_DETALLE_MATERIAL.USUARIO) = vlo_Usuario.ID_PERSONAL
                    Next

                    vlo_DalOttDetalleMaterial.AdapterOttDetalleMaterialRequisicion(vlo_DsDetalles)

                End If

                If pvc_Accion = "2" Then

                    'Obtiene las lineas del detalle material para cambiarles el estado 
                    vlo_DsDetalles = vlo_DalOttDetalleMaterial.ListarRegistros(String.Format("{0} = '{1}' AND {2} = {3}",
                                    Modelo.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo, Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION, pvn_idUbicacion), String.Empty, False, 0, 0)

                    For Each vlo_fila In vlo_DsDetalles.Tables(0).Rows
                        vlo_fila(Modelo.OTT_DETALLE_MATERIAL.ESTADO) = EstadoRegistro.PENDIENTE_ENVIO
                        vlo_fila(Modelo.OTT_DETALLE_MATERIAL.USUARIO) = vlo_Usuario.ID_PERSONAL
                    Next

                    vlo_DalOttDetalleMaterial.AdapterOttDetalleMaterialRequisicion(vlo_DsDetalles)

                    'Obtiene el correo del administrador del sistema
                    vlo_DsParametros = vlo_DalOtpParametroUbicacion.ListarRegistrosLista(
                        String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR),
                        String.Empty, False, 0, 0)

                    If vlo_DsParametros.Tables.Count > 0 AndAlso vlo_DsParametros.Tables(0).Rows.Count > 0 Then
                        vlc_CorreoAdministrador = vlo_DsParametros.Tables(0).Rows(0).Item(Modelo.OTP_PARAMETRO_UBICACION.VALOR)
                        vlo_EntOtmSectorTaller = vlo_DalOtmSectorTaller.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, vlo_EntOttOrdenTrabajo.IdSectorTaller))
                        If vlo_EntOtmSectorTaller.Existe Then
                            EnviarNotificacionDevuelta(vlo_EntOttOrdenTrabajo, vlo_EntOtmSectorTaller.NumEmpleadoCoordinador, pvc_observaciones, vlc_CorreoAdministrador)
                        End If
                    End If
                End If

                If vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo <> EstadoOrden.LIQUIDADA Then

                    vlo_EntOttSolicitudMaterial = vlo_DalOttSolicitudMaterial.ObtenerRegistro(
                        String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_SOLICITUD_MATERIAL.ID_UBICACION, pvn_idUbicacion, Modelo.OTT_SOLICITUD_MATERIAL.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo))

                    vlo_EntOttSolicitudMaterial.Usuario = vlo_EntOttOrdenTrabajo.Usuario
                    vlo_EntOttSolicitudMaterial.EstadoSolMaterial = IIf(vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EN_EVALUACION, EstadoSolicitudMaterial.INGRESADO_POR_COORDINADOR, EstadoSolicitudMaterial.APROBADA)

                    vlo_EntOttSolicitudMaterial.IdAdjuntoSolicita = pvn_IdArchivoSolicitud
                    vlo_EntOttSolicitudMaterial.IdAdjuntoRespuesta = vln_IdRespuesta

                    vlo_DalOttSolicitudMaterial.ModificarRegistro(vlo_EntOttSolicitudMaterial)
                End If

                If vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo <> EstadoOrden.EN_EVALUACION Then
                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = vlo_EntOttOrdenTrabajo.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = vlo_EntOttOrdenTrabajo.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.ObservacionesInternas = pvc_observaciones
                    vlo_EntOttTrazabilidadProceso.Usuario = vlo_EntOttOrdenTrabajo.Usuario

                    vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)


                End If

                vln_resultado = 1

                vlo_Conexion.TransaccionCommit()

                Return vln_resultado
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="pvo_DsMateriales"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez G</author>
        ''' <creationDate>27/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Function AjusteMaterial(pvc_UsuarioSession As String, pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vlo_DsDatos As Data.DataSet
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

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                vlo_DalOtfInventario = New DalOtfInventario(vlo_Conexion)

                vlo_DsDatos = vlo_DalOttDetalleMaterial.ListarRegistros(
                    String.Format("{0} = '{1}' AND {2} = {3} AND ({4} = '{5}' OR {4} = '{6}')",
                        Modelo.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo,
                        Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION, pvn_IdUbicacion,
                        Modelo.OTT_DETALLE_MATERIAL.ESTADO, EstadoRegistro.PENDIENTE_APROBACION, EstadoRegistro.DENEGADA),
                    String.Empty, False, 0, 0)

                For Each vlo_FilaDetalleMaterial In vlo_DsDatos.Tables(0).Rows
                    If (vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.VIA_DESPACHO).ToString <> ViaDespacho.VIACOMPRA) Then

                        If vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ESTADO).ToString = EstadoRegistro.PENDIENTE_APROBACION Then

                            vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                                                        Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_ALMACEN_BODEGA),
                                                        Modelo.OTF_INVENTARIO.ID_MATERIAL, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL),
                                                        Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA)))

                            If vlo_EntOtfInventario.Existe Then

                                If CType(vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA), Double) = 0 Then

                                    If (vlo_EntOtfInventario.CantidadDisponible - vlo_EntOtfInventario.CantidadReservada) >= CType(vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA), Double) Then

                                        vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada + CType(vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA), Double)
                                        vlo_EntOtfInventario.Usuario = pvc_UsuarioSession
                                        vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)

                                        vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA) = vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA)
                                        vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ESTADO) = EstadoDetalle.APROBADO
                                        vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.USUARIO) = pvc_UsuarioSession

                                    Else
                                        Throw New OrdenesDeTrabajoException(String.Format("La bodega o almacén seleccionada para el material {0} no posee suficiente material.", vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL).ToString))
                                    End If
                                End If
                            Else
                                Throw New OrdenesDeTrabajoException(String.Format("La bodega o almacén seleccionada para el material {0} no posee suficiente material.", vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL).ToString))
                            End If
                        Else

                            vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                                                        Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_ALMACEN_BODEGA),
                                                        Modelo.OTF_INVENTARIO.ID_MATERIAL, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_MATERIAL),
                                                        Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA)))

                            If vlo_EntOtfInventario.Existe Then

                                If CType(vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA), Double) > 0 Then

                                    vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada - CType(vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA), Double)
                                    vlo_EntOtfInventario.Usuario = pvc_UsuarioSession
                                    vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)

                                    vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA) = 0
                                    vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.USUARIO) = pvc_UsuarioSession

                                End If

                            End If

                        End If

                    Else
                        vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.ESTADO) = EstadoDetalle.APROBADO
                        vlo_FilaDetalleMaterial(Modelo.OTT_DETALLE_MATERIAL.USUARIO) = pvc_UsuarioSession

                    End If
                Next

                vlo_DalOttDetalleMaterial.AdapterOttDetalleMaterialRequisicion(vlo_DsDatos)

                vlo_Conexion.TransaccionCommit()

                Return 1
            Catch ex As Exception
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
        ''' </summary>
        ''' <param name="pvc_idPersonal"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>17/04/2016</creationDate>
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
        ''' Envia el correo electrónico y devuelve un valor mayor a cero si tuvo éxito o menor a cero si ocurrió un fallo
        ''' </summary>
        ''' <param name="pvc_NombreProyecto"></param>
        ''' <param name="pvc_idOrden"></param>
        ''' <param name="pvc_TiempoReal"></param>
        ''' <param name="pvc_CorreoAdministrador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez G</author>
        ''' <creationDate>17/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Function EnviarNotificacionLiquidada(pvo_ordenTrabajo As EntOttOrdenTrabajo, pvn_idCoordinador As Integer, pvc_observaciones As String, pvc_CorreoAdministrador As String) As Integer
            Dim vlo_WsGestorNotificaciones As WsrGestorNotificaciones.wsGestorNotificaciones
            Dim vlo_Sistema As WsrGestorNotificaciones.EntGNM_SISTEMA
            Dim vlo_ListaAdjunto As List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)
            Dim vlo_ListaDestinatario As List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
            Dim vlo_Notificacion As WsrGestorNotificaciones.EntGNT_NOTIFICACION
            Dim vlo_EntGNT_DESTINATARIO As WsrGestorNotificaciones.EntGNT_DESTINATARIO
            Dim vlo_Empleado As WsrEU_Curriculo.EntEmpleados
            Dim vlo_Coordinador As WsrEU_Curriculo.EntEmpleados
            Dim vln_Resultado As Integer


            vlo_WsGestorNotificaciones = New WsrGestorNotificaciones.wsGestorNotificaciones
            vlo_WsGestorNotificaciones.Timeout = -1
            vlo_WsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsGestorNotificaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_GESTOR_NOTIFICACIONES)

            Try
                vln_Resultado = 1
                vlo_Sistema = vlo_WsGestorNotificaciones.GNM_SISTEMA_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    String.Format("NOMBRE_SISTEMA = '{0}'", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_GN)))

                If vlo_Sistema IsNot Nothing AndAlso vlo_Sistema.Existe Then
                    vlo_Notificacion = New WsrGestorNotificaciones.EntGNT_NOTIFICACION()
                    'Carga la información del solicitante
                    vlo_Empleado = CargarFuncionario(pvo_ordenTrabajo.NumEmpleado)
                    vlo_Coordinador = CargarFuncionario(pvn_idCoordinador)
                    If Not String.IsNullOrWhiteSpace(vlo_Empleado.CORREO_INSTITUCIONAL) Then
                        '{0}: Numero de orden de trabajo
                        vlo_Notificacion.ASUNTO = String.Format("Liquidación de la orden de trabajo N° {0}", pvo_ordenTrabajo.IdOrdenTrabajo)

                        '{0}: Nombre del solicitante
                        '{1}: Apellido 1 del solicitante
                        '{2}: Apellido 2 del solicitante
                        '{3}: Descripcion del trabajo
                        '{4}: Observaciones
                        '{5}: Correo del administrador del sistema

                        vlo_Notificacion.CUERPO = String.Format("<b>Señor(a):{0} {1} {2}</b><br /><br /><b>Estimado(a) señor(a)</b><br />Se le notifica que que tras revisar su solicitud para {3}, se ha determinado que no es viable realizar dicho trabajo; motivo por el cual su solicitud ha sido liquidada. <br /><br />Se indican las siguientes observaciones: <br />{4}<br /><hr><i>Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema refiérase a la dirección: {5}</i>",
                                           vlo_Empleado.NOMBRE, vlo_Empleado.APELLIDO1, vlo_Empleado.APELLIDO2, pvo_ordenTrabajo.DescripcionTrabajo, pvc_observaciones, pvc_CorreoAdministrador)
                        vlo_Notificacion.ES_HTML = 1
                        vlo_Notificacion.USUARIO_CREA = System.Environment.UserName.Trim()

                        vlo_ListaDestinatario = New List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
                        vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
                        vlo_EntGNT_DESTINATARIO.DESTINATARIO = vlo_Empleado.CORREO_INSTITUCIONAL
                        vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)

                        vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
                        vlo_EntGNT_DESTINATARIO.DESTINATARIO = vlo_Coordinador.CORREO_INSTITUCIONAL
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

                End If

                Return vln_Resultado
            Catch ex As Exception
                Throw New OrdenesDeTrabajoException("Error en el envío de correos.")
            End Try
        End Function

        ''' <summary>
        ''' Envia el correo electrónico y devuelve un valor mayor a cero si tuvo éxito o menor a cero si ocurrió un fallo
        ''' </summary>
        ''' <param name="pvc_NombreProyecto"></param>
        ''' <param name="pvc_idOrden"></param>
        ''' <param name="pvc_TiempoReal"></param>
        ''' <param name="pvc_CorreoAdministrador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez G</author>
        ''' <creationDate>17/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Function EnviarNotificacionDevuelta(pvo_ordenTrabajo As EntOttOrdenTrabajo, pvn_idCoordinador As Integer, pvc_observaciones As String, pvc_CorreoAdministrador As String) As Integer
            Dim vlo_WsGestorNotificaciones As WsrGestorNotificaciones.wsGestorNotificaciones
            Dim vlo_Sistema As WsrGestorNotificaciones.EntGNM_SISTEMA
            Dim vlo_ListaAdjunto As List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)
            Dim vlo_ListaDestinatario As List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
            Dim vlo_Notificacion As WsrGestorNotificaciones.EntGNT_NOTIFICACION
            Dim vlo_EntGNT_DESTINATARIO As WsrGestorNotificaciones.EntGNT_DESTINATARIO
            Dim vlo_Empleado As WsrEU_Curriculo.EntEmpleados
            Dim vln_Resultado As Integer


            vlo_WsGestorNotificaciones = New WsrGestorNotificaciones.wsGestorNotificaciones
            vlo_WsGestorNotificaciones.Timeout = -1
            vlo_WsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsGestorNotificaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_GESTOR_NOTIFICACIONES)

            Try
                vln_Resultado = 1
                vlo_Sistema = vlo_WsGestorNotificaciones.GNM_SISTEMA_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    String.Format("NOMBRE_SISTEMA = '{0}'", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_GN)))

                If vlo_Sistema IsNot Nothing AndAlso vlo_Sistema.Existe Then
                    vlo_Notificacion = New WsrGestorNotificaciones.EntGNT_NOTIFICACION()
                    'Carga la información del solicitante
                    vlo_Empleado = CargarFuncionario(pvn_idCoordinador)
                    If Not String.IsNullOrWhiteSpace(vlo_Empleado.CORREO_INSTITUCIONAL) Then
                        '{0}: Numero de orden de trabajo
                        vlo_Notificacion.ASUNTO = String.Format("Observaciones a la solicitud de materiales para la orden de trabajo N° {0}", pvo_ordenTrabajo.IdOrdenTrabajo)

                        '{0}: Nombre del coordinador
                        '{1}: Apellido 1 del coordinador
                        '{2}: Apellido 2 del coordinador
                        '{3}: Descripcion del trabajo
                        '{4}: Observaciones
                        '{5}: Correo del administrador del sistema

                        vlo_Notificacion.CUERPO = String.Format("<b>Señor(a):{0} {1} {2}</b><br /><br /><b>Estimado(a) señor(a)</b><br />Se le notifica que tras revisar su solicitud de materiales para {3}, <br />Se le indican las siguientes observaciones: <br />{4}<br /><hr><i>Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema refiérase a la dirección: {5}</i>",
                                           vlo_Empleado.NOMBRE, vlo_Empleado.APELLIDO1, vlo_Empleado.APELLIDO2, pvo_ordenTrabajo.DescripcionTrabajo, pvc_observaciones, pvc_CorreoAdministrador)
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

                End If

                Return vln_Resultado
            Catch ex As Exception
                Throw New OrdenesDeTrabajoException("Error en el envío de correos.")
            End Try
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>19/09/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtDetalleMaterialUnion(pvc_Condicion As String, pvc_Orden As String, pvo_GestionCompra As EntOttGestionCompra) As Data.DataSet
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DsDetallesMaterial As Data.DataSet
            Dim vlo_DsLineasGestion As Data.DataSet
            Dim vlo_NewRow As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)

                vlo_DsDetallesMaterial = vlo_DalOttDetalleMaterial.ListarVOtDetalleMaterial(pvc_Condicion, pvc_Orden, False, 0, 0)
                vlo_DsLineasGestion = vlo_DalOttLineaGestionCompra.ListarRegistrosLista(
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}",
                                  Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION, pvo_GestionCompra.IdUbicacion,
                                  Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvo_GestionCompra.IdViaCompraContrato,
                                  Modelo.OTT_LINEA_GESTION_COMPRA.ANNO, pvo_GestionCompra.Anno,
                                  Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION, pvo_GestionCompra.NumeroGestion), String.Empty, False, 0, 0)

                For Each vlo_Fila In vlo_DsLineasGestion.Tables(0).Rows
                    vlo_NewRow = vlo_DsDetallesMaterial.Tables(0).NewRow

                    vlo_NewRow.Item(vlo_DsDetallesMaterial.Tables(0).Columns(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO)) = "1"
                    vlo_NewRow.Item(vlo_DsDetallesMaterial.Tables(0).Columns(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL)) = vlo_Fila(Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_DETALLE_MATERIAL)
                    vlo_NewRow.Item(vlo_DsDetallesMaterial.Tables(0).Columns(Modelo.V_OT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO)) = vlo_Fila(Modelo.V_OTT_LINEA_GESTION_COMPRALST.NUMERO_OT)
                    vlo_NewRow.Item(vlo_DsDetallesMaterial.Tables(0).Columns(Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL)) = vlo_Fila(Modelo.V_OTT_LINEA_GESTION_COMPRALST.ID_MATERIAL)
                    vlo_NewRow.Item(vlo_DsDetallesMaterial.Tables(0).Columns(Modelo.V_OT_DETALLE_MATERIAL.DESCRIPCION)) = vlo_Fila(Modelo.V_OTT_LINEA_GESTION_COMPRALST.DESCRIPCION)
                    vlo_NewRow.Item(vlo_DsDetallesMaterial.Tables(0).Columns(Modelo.V_OT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA)) = vlo_Fila(Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_SOLICITADA)
                    vlo_NewRow.Item(vlo_DsDetallesMaterial.Tables(0).Columns(Modelo.V_OT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA_MEDIDA)) = vlo_Fila(Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_SOLICITADA_MEDIDA)
                    vlo_NewRow.Item(vlo_DsDetallesMaterial.Tables(0).Columns(Modelo.V_OT_DETALLE_MATERIAL.DETALLE)) = vlo_Fila(Modelo.V_OTT_LINEA_GESTION_COMPRALST.DETALLE)
                    vlo_NewRow.Item(vlo_DsDetallesMaterial.Tables(0).Columns(Modelo.V_OT_DETALLE_MATERIAL.FECHA_ASIGNACION)) = vlo_Fila(Modelo.V_OTT_LINEA_GESTION_COMPRALST.FECHA_ASIGNACION)

                    vlo_DsDetallesMaterial.Tables(0).Rows.Add(vlo_NewRow)
                Next

                Return vlo_DsDetallesMaterial

            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

#End Region

    End Class
End Namespace
