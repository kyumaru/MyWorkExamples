Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOtlDetalleMaterial
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
        ''' Permite agregar un registro en la tabla OTL_DETALLE_MATERIAL, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtlDetalleMaterial) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtlDetalleMaterial As DalOtlDetalleMaterial
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
                    Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
                End If

                vlo_DalOtlDetalleMaterial = New DalOtlDetalleMaterial(vlo_Conexion)
                vln_Resultado = vlo_DalOtlDetalleMaterial.InsertarRegistro(pvo_Registro)
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
        ''' <param name="pvn_IdDetalleMaterial">Llave primaria de la tabla ott_detalle_material que se asocia con la secuencia sq_id_detalle_material</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>03/06/2016 02:42:19 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdDetalleMaterial As Integer) As EntOtlDetalleMaterial
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtlDetalleMaterial As DalOtlDetalleMaterial

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtlDetalleMaterial = New DalOtlDetalleMaterial(vlo_Conexion)
                Return vlo_DalOtlDetalleMaterial.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTL_DETALLE_MATERIAL.ID_DETALLE_MATERIAL, pvn_IdDetalleMaterial))
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
        Private Function RespuestaRevisionRequisiciones(pvo_DsMateriales As DataSet, pvc_idOrdenTrabajo As String, pvn_idUbicacion As Integer, pvc_observaciones As String, pvn_NumEmpleado As Integer, pvb_aprobar As Boolean, pvb_Solicitar As Boolean) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_fila() As DataRow
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vln_resultado As Integer

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

                vlo_Conexion.TransaccionBegin()

                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = '{1}' AND {2} = {3}", Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvc_idOrdenTrabajo, Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvn_idUbicacion))

                If pvb_aprobar Then
                    vlo_fila = pvo_DsMateriales.Tables(0).Select(String.Format("{0} <> {1} AND {0} <> {2}", Modelo.V_OTT_DETALLE_MATERIALLST.VIA_DESPACHO, Tipo.ALMACEN, Tipo.BODEGA))

                    If vlo_fila.Length > 1 Then
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.MATERIAL_PENDIENTE_COMPRA
                    Else
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PARA_RETIRO_MATERIAL
                    End If

                End If

                If pvb_Solicitar Then
                    vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.REVISION_PRESUPUESTO_SUPERVISOR
                End If

                If vln_resultado > 0 Then
                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = vlo_EntOttOrdenTrabajo.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = vlo_EntOttOrdenTrabajo.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo
                    'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                    vlo_EntOttTrazabilidadProceso.Observaciones = pvc_observaciones
                    vlo_EntOttTrazabilidadProceso.Usuario = vlo_EntOttOrdenTrabajo.Usuario

                    vln_resultado = vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)
                End If

                vlo_Conexion.TransaccionCommit()

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
