Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttGestionIngresoMater
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
        ''' Permite agregar un registro en la tabla OTT_GESTION_INGRESO_MATER, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 02:22:54 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttGestionIngresoMater) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionIngresoMater As DalOttGestionIngresoMater
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdViaCompraContrato, pvo_Registro.NumeroGestion, pvo_Registro.Anno, pvo_Registro.Consecutivo).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
                End If

                vlo_DalOttGestionIngresoMater = New DalOttGestionIngresoMater(vlo_Conexion)
                vln_Resultado = vlo_DalOttGestionIngresoMater.InsertarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTT_GESTION_INGRESO_MATER, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 02:22:54 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOttGestionIngresoMater) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionIngresoMater As DalOttGestionIngresoMater
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdUbicacion, pvo_Registro.IdViaCompraContrato, pvo_Registro.NumeroGestion, pvo_Registro.Anno, pvo_Registro.Consecutivo) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Hay registro asociados")
                End If

                vlo_DalOttGestionIngresoMater = New DalOttGestionIngresoMater(vlo_Conexion)
                vln_Resultado = vlo_DalOttGestionIngresoMater.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvn_IdViaCompraContrato">Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato</param>
        ''' <param name="pvn_NumeroGestion">Consecutivo de la gestión</param>
        ''' <param name="pvn_Anno">Año</param>
        ''' <param name="pvn_Consecutivo">Consecutivo de la gestion</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 02:22:54 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_NumeroGestion As Integer, pvn_Anno As Integer, pvn_Consecutivo As Integer) As EntOttGestionIngresoMater
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionIngresoMater As DalOttGestionIngresoMater

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttGestionIngresoMater = New DalOttGestionIngresoMater(vlo_Conexion)
                Return vlo_DalOttGestionIngresoMater.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_GESTION_INGRESO_MATER.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_GESTION_INGRESO_MATER.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_GESTION_INGRESO_MATER.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTT_GESTION_INGRESO_MATER.ANNO, pvn_Anno, Modelo.OTT_GESTION_INGRESO_MATER.CONSECUTIVO, pvn_Consecutivo))
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
        ''' <param name="pvn_IdViaCompraContrato">Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato</param>
        ''' <param name="pvn_NumeroGestion">Consecutivo de la gestión</param>
        ''' <param name="pvn_Anno">Año</param>
        ''' <param name="pvn_Consecutivo">Consecutivo de la gestion</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 02:22:54 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_NumeroGestion As Integer, pvn_Anno As Integer, pvn_Consecutivo As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOttAdjuntoGestionIngr As DalOttAdjuntoGestionIngr

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

                'Determinar la existencia de registros asociados en la tabla OTT_ADJUNTO_GESTION_INGR
                vlo_DalOttAdjuntoGestionIngr = New DalOttAdjuntoGestionIngr(vlo_Conexion)
                If vlo_DalOttAdjuntoGestionIngr.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_ADJUNTO_GESTION_INGR.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ADJUNTO_GESTION_INGR.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_ADJUNTO_GESTION_INGR.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTT_ADJUNTO_GESTION_INGR.ANNO, pvn_Anno, Modelo.OTT_ADJUNTO_GESTION_INGR.CONSECUTIVO, pvn_Consecutivo)).Existe Then
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

        Public Function CrearIngresoMaterial(pvo_EntOttAdjuntoGestionIngr As EntOttAdjuntoGestionIngr, pvo_DsEnca As DataSet, pvo_DsDetalle As DataSet, pvn_IdUbicacion As Integer, pvn_IdViaCompra As Integer, pvn_Anno As Integer, pvn_NumeroGestion As Integer, pvc_IdProveedor As String, pvc_Observaciones As String, pvc_UsuarioCrea As String, pvb_Finalizar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionIngresoMater As DalOttGestionIngresoMater
            Dim vlo_DalOttDetalleGestionIngr As DalOttDetalleGestionIngr
            Dim vlo_DalOttAdjuntoGestionIngr As DalOttAdjuntoGestionIngr
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOtlTrazabilGestionIngr As DalOtlTrazabilGestionIngr
            Dim vlo_EntOtlTrazabilGestionIngr As EntOtlTrazabilGestionIngr
            Dim vlo_EntOttGestionIngresoMater As EntOttGestionIngresoMater
            Dim vlo_EntOttGestionCompra As EntOttGestionCompra
            Dim vlo_DsDetalleGestionIngr As Data.DataSet
            Dim vlc_Resultado As Integer = -1
            Dim vlo_DrFilaDetalleGestionIngr As Data.DataRow
            Dim vlo_DsLineaGestion As Data.DataSet
            Dim vlc_Condicion As String
            Dim vlb_Completo As Boolean = True
            Dim vln_IdAdjunto As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionIngresoMater = New DalOttGestionIngresoMater(vlo_Conexion)
                vlo_DalOttDetalleGestionIngr = New DalOttDetalleGestionIngr(vlo_Conexion)
                vlo_DalOttAdjuntoGestionIngr = New DalOttAdjuntoGestionIngr(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOtlTrazabilGestionIngr = New DalOtlTrazabilGestionIngr(vlo_Conexion)

                'Se crea el encabezado de ingreso de material
                vlo_EntOttGestionIngresoMater = New EntOttGestionIngresoMater
                vlo_EntOttGestionIngresoMater.IdUbicacion = pvn_IdUbicacion
                vlo_EntOttGestionIngresoMater.Anno = pvn_Anno
                vlo_EntOttGestionIngresoMater.IdViaCompraContrato = pvn_IdViaCompra
                vlo_EntOttGestionIngresoMater.Consecutivo = vlo_DalOttGestionIngresoMater.ObtenerFcOtConsecutivoIngresoMat(pvn_Anno, pvn_IdUbicacion, pvn_NumeroGestion, pvn_IdViaCompra) + 1
                vlo_EntOttGestionIngresoMater.NumeroGestion = pvn_NumeroGestion
                vlo_EntOttGestionIngresoMater.Identificacion = pvc_IdProveedor
                vlo_EntOttGestionIngresoMater.FechaIngresoRegistro = DateTime.Now
                vlo_EntOttGestionIngresoMater.Observaciones = pvc_Observaciones
                vlo_EntOttGestionIngresoMater.Usuario = pvc_UsuarioCrea
                If pvb_Finalizar Then
                    vlo_EntOttGestionIngresoMater.EstadoGestionIngreso = EstadoGestionIngr.VALIDACION_MONTOS
                Else
                    vlo_EntOttGestionIngresoMater.EstadoGestionIngreso = EstadoGestionIngr.CREADA
                End If


                vlo_DalOttGestionIngresoMater.InsertarRegistro(vlo_EntOttGestionIngresoMater)

                'Se ingresa los archivos adjuntos a la gestion de ingreso
                pvo_EntOttAdjuntoGestionIngr.Consecutivo = vlo_EntOttGestionIngresoMater.Consecutivo
                vln_IdAdjunto = vlo_DalOttAdjuntoGestionIngr.InsertarRegistro(pvo_EntOttAdjuntoGestionIngr)

                'Se ingresa el detalle del ingreso de materiales
                vlo_DsDetalleGestionIngr = vlo_DalOttDetalleGestionIngr.ListarRegistros("1 = 0", String.Empty, False, 0, 0)

                For Each vlo_FilaDetalle In pvo_DsDetalle.Tables(0).Rows
                    If Not TypeOf vlo_FilaDetalle.Item("CANTIDAD_ING") Is DBNull AndAlso vlo_FilaDetalle.Item("CANTIDAD_ING").ToString <> String.Empty Then
                        vlo_DrFilaDetalleGestionIngr = vlo_DsDetalleGestionIngr.Tables(0).NewRow

                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR)) = vln_IdAdjunto
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION)) = vlo_EntOttGestionIngresoMater.IdUbicacion
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.ANNO)) = vlo_EntOttGestionIngresoMater.Anno
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO)) = vlo_EntOttGestionIngresoMater.IdViaCompraContrato
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA)) = vlo_FilaDetalle.Item(Modelo.OTT_LINEA_GESTION_COMPRA.ID_LINEA_GESTION_COMPRA)
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.NUMERO_GESTION)) = vlo_EntOttGestionIngresoMater.NumeroGestion
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA)) = vlo_FilaDetalle.Item("CANTIDAD_ING")
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.USUARIO)) = pvc_UsuarioCrea
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.COSTO_INDIVIDUAL)) = 0

                        vlo_DsDetalleGestionIngr.Tables(0).Rows.Add(vlo_DrFilaDetalleGestionIngr)
                    End If


                Next

                vlo_DalOttDetalleGestionIngr.AdapterOttDetalleGestionIngr(vlo_DsDetalleGestionIngr)



                'Si se selecciono finalizar se actualizan las lineas de la gestion de compra, se verifica si se puede cambiar de estado y se registra la trazabilidad del movimiento
                If pvb_Finalizar Then
                    'Se actualiza la cantidad ingresa para cada linea de la gestion de compra
                    vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompra, Modelo.OTT_LINEA_GESTION_COMPRA.ANNO, pvn_Anno, Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION, pvn_NumeroGestion)
                    vlo_DsLineaGestion = vlo_DalOttLineaGestionCompra.ListarRegistros(vlc_Condicion, String.Empty, False, 0, 0)

                    For Each vlo_FilaDetalle In vlo_DsDetalleGestionIngr.Tables(0).Rows
                        For Each vlo_FilaLineaGestion In vlo_DsLineaGestion.Tables(0).Rows
                            If vlo_FilaDetalle(Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA).ToString = vlo_FilaLineaGestion(Modelo.OTT_LINEA_GESTION_COMPRA.ID_LINEA_GESTION_COMPRA).ToString Then

                                If Not TypeOf vlo_FilaLineaGestion.Item(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_INGRESA) Is DBNull AndAlso vlo_FilaLineaGestion.Item(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_INGRESA).ToString <> String.Empty Then
                                    vlo_FilaLineaGestion.Item(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_INGRESA) = vlo_FilaLineaGestion.Item(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_INGRESA) + vlo_FilaDetalle(Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA)
                                Else
                                    vlo_FilaLineaGestion.Item(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_INGRESA) = vlo_FilaDetalle(Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA)
                                End If
                            End If
                        Next
                    Next

                    vlo_DalOttLineaGestionCompra.AdapterOttLineaGestionCompra(vlo_DsLineaGestion)

                    'Se verifica si la gestion esta completa para cambiar su estado
                    For Each vlo_FilaLineaGestion In vlo_DsLineaGestion.Tables(0).Rows
                        If vlo_FilaLineaGestion(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_SOLICITADA).ToString <> vlo_FilaLineaGestion(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_INGRESA).ToString Then
                            vlb_Completo = False
                        End If
                    Next

                    If vlb_Completo Then
                        vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_GESTION_COMPRA.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompra, Modelo.OTT_GESTION_COMPRA.ANNO, pvn_Anno, Modelo.OTT_GESTION_COMPRA.NUMERO_GESTION, pvn_NumeroGestion)
                        vlo_EntOttGestionCompra = vlo_DalOttGestionCompra.ObtenerRegistro(vlc_Condicion)

                        vlo_EntOttGestionCompra.Usuario = pvc_UsuarioCrea
                        vlo_EntOttGestionCompra.Estado = EstadoGestionCompra.REGISTRO_DE_INGRESOS_EN_ALMACEN

                        vlo_DalOttGestionCompra.ModificarRegistro(vlo_EntOttGestionCompra)
                    End If


                End If

                'Se ingresa el registro de trazabilidad
                vlo_EntOtlTrazabilGestionIngr = New EntOtlTrazabilGestionIngr

                With vlo_EntOtlTrazabilGestionIngr
                    .Anno = vlo_EntOttGestionIngresoMater.Anno
                    .Consecutivo = vlo_EntOttGestionIngresoMater.Consecutivo
                    .IdUbicacion = vlo_EntOttGestionIngresoMater.IdUbicacion
                    .IdViaCompraContrato = vlo_EntOttGestionIngresoMater.IdViaCompraContrato
                    .NumeroGestion = vlo_EntOttGestionIngresoMater.NumeroGestion
                    .EstadoGestionIngreso = vlo_EntOttGestionIngresoMater.EstadoGestionIngreso
                    .Observaciones = pvc_Observaciones
                    .Usuario = pvc_UsuarioCrea
                End With

                vlo_DalOtlTrazabilGestionIngr.InsertarRegistro(vlo_EntOtlTrazabilGestionIngr)

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

        Public Function ModificarIngresoMaterial(pvo_EntOttAdjuntoGestionIngr As EntOttAdjuntoGestionIngr, pvo_DsEnca As DataSet, pvo_DsDetalle As DataSet, pvn_IdUbicacion As Integer, pvn_IdViaCompra As Integer, pvn_Anno As Integer, pvn_NumeroGestion As Integer, pvn_Consecutivo As Integer, pvc_IdProveedor As String, pvc_Observaciones As String, pvc_UsuarioCrea As String, pvb_Finalizar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionIngresoMater As DalOttGestionIngresoMater
            Dim vlo_DalOttDetalleGestionIngr As DalOttDetalleGestionIngr
            Dim vlo_DalOttAdjuntoGestionIngr As DalOttAdjuntoGestionIngr
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOtlTrazabilGestionIngr As DalOtlTrazabilGestionIngr
            Dim vlo_EntOtlTrazabilGestionIngr As EntOtlTrazabilGestionIngr
            Dim vlo_EntOttGestionIngresoMater As EntOttGestionIngresoMater
            Dim vlo_EntOttGestionCompra As EntOttGestionCompra
            Dim vln_Resultado As Integer = 0
            Dim vlc_Condicion As String
            Dim vlo_DsDetalleGestionIngr As Data.DataSet
            Dim vlo_DrFilaDetalleGestionIngr As Data.DataRow
            Dim vlc_DetRegistrado As String
            Dim vln_IdAdjunto As Integer
            Dim vlo_AdjuntoAux As EntOttAdjuntoGestionIngr
            Dim vlo_DsLineaGestion As DataSet
            Dim vlb_Completo As Boolean = True

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionIngresoMater = New DalOttGestionIngresoMater(vlo_Conexion)
                vlo_DalOttDetalleGestionIngr = New DalOttDetalleGestionIngr(vlo_Conexion)
                vlo_DalOttAdjuntoGestionIngr = New DalOttAdjuntoGestionIngr(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOtlTrazabilGestionIngr = New DalOtlTrazabilGestionIngr(vlo_Conexion)

                vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_GESTION_INGRESO_MATER.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_GESTION_INGRESO_MATER.ANNO, pvn_Anno, Modelo.OTT_GESTION_INGRESO_MATER.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompra, Modelo.OTT_GESTION_INGRESO_MATER.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTT_GESTION_INGRESO_MATER.CONSECUTIVO, pvn_Consecutivo)
                vlo_EntOttGestionIngresoMater = vlo_DalOttGestionIngresoMater.ObtenerRegistro(vlc_Condicion)

                'Se modifica el ingreso de material
                If vlo_EntOttGestionIngresoMater.Observaciones <> pvc_Observaciones Then
                    vlo_EntOttGestionIngresoMater.Observaciones = pvc_Observaciones
                End If

                If vlo_EntOttGestionIngresoMater.Identificacion = "-" Then
                    vlo_EntOttGestionIngresoMater.Identificacion = String.Empty
                End If

                If vlo_EntOttGestionIngresoMater.Identificacion <> pvc_IdProveedor AndAlso pvc_IdProveedor <> String.Empty Then
                    vlo_EntOttGestionIngresoMater.Identificacion = pvc_IdProveedor
                End If

                If pvb_Finalizar Then
                    vlo_EntOttGestionIngresoMater.EstadoGestionIngreso = EstadoGestionIngr.VALIDACION_MONTOS
                End If

                vlo_EntOttGestionIngresoMater.FechaIngresoRegistro = DateTime.Now
                vlo_EntOttGestionIngresoMater.Usuario = pvc_UsuarioCrea

                vlo_DalOttGestionIngresoMater.ModificarRegistro(vlo_EntOttGestionIngresoMater)

                'Se verifica si es necesario modificar el adjunto
                vln_IdAdjunto = pvo_EntOttAdjuntoGestionIngr.IdAdjuntoGestionIngr

                vlc_Condicion = String.Format("{0} = {1}", Modelo.OTT_ADJUNTO_GESTION_INGR.ID_ADJUNTO_GESTION_INGR, pvo_EntOttAdjuntoGestionIngr.IdAdjuntoGestionIngr)
                vlo_AdjuntoAux = vlo_DalOttAdjuntoGestionIngr.ObtenerRegistro(vlc_Condicion)

                If vlo_AdjuntoAux.NombreArchivo <> pvo_EntOttAdjuntoGestionIngr.NombreArchivo Then
                    vlo_DalOttAdjuntoGestionIngr.ModificarRegistro(pvo_EntOttAdjuntoGestionIngr)
                End If


                'Se obtiene el detalle del ingreso de material
                vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION, vlo_EntOttGestionIngresoMater.IdUbicacion, Modelo.OTT_DETALLE_GESTION_INGR.ANNO, vlo_EntOttGestionIngresoMater.Anno, Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO, vlo_EntOttGestionIngresoMater.IdViaCompraContrato, Modelo.OTT_DETALLE_GESTION_INGR.NUMERO_GESTION, vlo_EntOttGestionIngresoMater.NumeroGestion)

                vlo_DsDetalleGestionIngr = vlo_DalOttDetalleGestionIngr.ListarRegistros(vlc_Condicion, String.Empty, False, 0, 0)

                vlc_DetRegistrado = String.Empty

                For Each vlo_FilaDetalle In vlo_DsDetalleGestionIngr.Tables(0).Rows
                    For Each vlo_FilaPantalla In pvo_DsDetalle.Tables(0).Rows
                        If Not TypeOf vlo_FilaPantalla.Item("CANTIDAD_ING") Is DBNull AndAlso vlo_FilaPantalla.Item("CANTIDAD_ING").ToString <> String.Empty Then
                            If vlo_FilaDetalle(Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA).ToString = vlo_FilaPantalla(Modelo.OTT_LINEA_GESTION_COMPRA.ID_LINEA_GESTION_COMPRA).ToString Then

                                vlo_FilaDetalle(Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA) = vlo_FilaPantalla("CANTIDAD_ING")

                                vlc_DetRegistrado = String.Format("{0}{1},", vlc_DetRegistrado, vlo_FilaDetalle(Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA).ToString)
                            End If
                        End If

                    Next

                Next

                vlc_DetRegistrado = String.Format(",{0}", vlc_DetRegistrado)

                For Each vlo_FilaPantalla In pvo_DsDetalle.Tables(0).Rows
                    If vlc_DetRegistrado.Contains(vlo_FilaPantalla(Modelo.OTT_LINEA_GESTION_COMPRA.ID_LINEA_GESTION_COMPRA).ToString) = False AndAlso Not TypeOf vlo_FilaPantalla.Item("CANTIDAD_ING") Is DBNull AndAlso vlo_FilaPantalla.Item("CANTIDAD_ING").ToString <> String.Empty Then
                        vlo_DrFilaDetalleGestionIngr = vlo_DsDetalleGestionIngr.Tables(0).NewRow

                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR)) = vln_IdAdjunto
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION)) = vlo_EntOttGestionIngresoMater.IdUbicacion
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.ANNO)) = vlo_EntOttGestionIngresoMater.Anno
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO)) = vlo_EntOttGestionIngresoMater.IdViaCompraContrato
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA)) = vlo_FilaPantalla.Item(Modelo.OTT_LINEA_GESTION_COMPRA.ID_LINEA_GESTION_COMPRA)
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.NUMERO_GESTION)) = vlo_EntOttGestionIngresoMater.NumeroGestion
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA)) = vlo_FilaPantalla.Item("CANTIDAD_ING")
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.USUARIO)) = pvc_UsuarioCrea
                        vlo_DrFilaDetalleGestionIngr.Item(vlo_DsDetalleGestionIngr.Tables(0).Columns(Modelo.OTT_DETALLE_GESTION_INGR.COSTO_INDIVIDUAL)) = 0

                        vlo_DsDetalleGestionIngr.Tables(0).Rows.Add(vlo_DrFilaDetalleGestionIngr)
                    End If

                Next

                vlo_DalOttDetalleGestionIngr.AdapterOttDetalleGestionIngr(vlo_DsDetalleGestionIngr)

                'Si se selecciono finalizar se registra la trazabilidad del movimiento
                If pvb_Finalizar Then
                    'Se actualiza la cantidad ingresa para cada linea de la gestion de compra
                    vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompra, Modelo.OTT_LINEA_GESTION_COMPRA.ANNO, pvn_Anno, Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION, pvn_NumeroGestion)
                    vlo_DsLineaGestion = vlo_DalOttLineaGestionCompra.ListarRegistros(vlc_Condicion, String.Empty, False, 0, 0)

                    For Each vlo_FilaDetalle In vlo_DsDetalleGestionIngr.Tables(0).Rows
                        For Each vlo_FilaLineaGestion In vlo_DsLineaGestion.Tables(0).Rows
                            If vlo_FilaDetalle(Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA).ToString = vlo_FilaLineaGestion(Modelo.OTT_LINEA_GESTION_COMPRA.ID_LINEA_GESTION_COMPRA).ToString Then

                                If Not TypeOf vlo_FilaLineaGestion.Item(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_INGRESA) Is DBNull AndAlso vlo_FilaLineaGestion.Item(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_INGRESA).ToString <> String.Empty Then
                                    vlo_FilaLineaGestion.Item(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_INGRESA) = vlo_FilaLineaGestion.Item(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_INGRESA) + vlo_FilaDetalle(Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA)
                                Else
                                    vlo_FilaLineaGestion.Item(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_INGRESA) = vlo_FilaDetalle(Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA)
                                End If
                            End If
                        Next
                    Next

                    vlo_DalOttLineaGestionCompra.AdapterOttLineaGestionCompra(vlo_DsLineaGestion)

                    'Se verifica si la gestion esta completa para cambiar su estado
                    For Each vlo_FilaLineaGestion In vlo_DsLineaGestion.Tables(0).Rows
                        If vlo_FilaLineaGestion(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_SOLICITADA).ToString <> vlo_FilaLineaGestion(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_INGRESA).ToString Then
                            vlb_Completo = False
                        End If
                    Next

                    If vlb_Completo Then
                        vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_GESTION_COMPRA.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompra, Modelo.OTT_GESTION_COMPRA.ANNO, pvn_Anno, Modelo.OTT_GESTION_COMPRA.NUMERO_GESTION, pvn_NumeroGestion)
                        vlo_EntOttGestionCompra = vlo_DalOttGestionCompra.ObtenerRegistro(vlc_Condicion)

                        vlo_EntOttGestionCompra.Usuario = pvc_UsuarioCrea
                        vlo_EntOttGestionCompra.Estado = EstadoGestionCompra.REGISTRO_DE_INGRESOS_EN_ALMACEN

                        vlo_DalOttGestionCompra.ModificarRegistro(vlo_EntOttGestionCompra)
                    End If

                    'Se ingresa el registro de trazabilidad
                    vlo_EntOtlTrazabilGestionIngr = New EntOtlTrazabilGestionIngr

                    With vlo_EntOtlTrazabilGestionIngr
                        .Anno = vlo_EntOttGestionIngresoMater.Anno
                        .Consecutivo = vlo_EntOttGestionIngresoMater.Consecutivo
                        .IdUbicacion = vlo_EntOttGestionIngresoMater.IdUbicacion
                        .IdViaCompraContrato = vlo_EntOttGestionIngresoMater.IdViaCompraContrato
                        .NumeroGestion = vlo_EntOttGestionIngresoMater.NumeroGestion
                        .EstadoGestionIngreso = vlo_EntOttGestionIngresoMater.EstadoGestionIngreso
                        .Observaciones = pvc_Observaciones
                        .Usuario = pvc_UsuarioCrea
                    End With

                    vlo_DalOtlTrazabilGestionIngr.InsertarRegistro(vlo_EntOtlTrazabilGestionIngr)
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

        Public Function DevolverGestionIngresoMatAlmacen(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_NumeroGestion As Integer, pvn_Anno As Integer, pvn_Consecutivo As Integer, pvc_Observaciones As String, pvc_UsuarioCrea As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionIngresoMater As DalOttGestionIngresoMater
            Dim vlo_DalOtlTrazabilGestionIngr As DalOtlTrazabilGestionIngr
            Dim vlo_EntOtlTrazabilGestionIngr As EntOtlTrazabilGestionIngr
            Dim vlo_EntOttGestionIngresoMater As EntOttGestionIngresoMater
            Dim vln_Resultado As Integer = 0
            Dim vlc_Condicion As String
            Dim vlb_Completo As Boolean = True

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionIngresoMater = New DalOttGestionIngresoMater(vlo_Conexion)
                vlo_DalOtlTrazabilGestionIngr = New DalOtlTrazabilGestionIngr(vlo_Conexion)

                vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_GESTION_INGRESO_MATER.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_GESTION_INGRESO_MATER.ANNO, pvn_Anno, Modelo.OTT_GESTION_INGRESO_MATER.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_GESTION_INGRESO_MATER.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTT_GESTION_INGRESO_MATER.CONSECUTIVO, pvn_Consecutivo)
                vlo_EntOttGestionIngresoMater = vlo_DalOttGestionIngresoMater.ObtenerRegistro(vlc_Condicion)

                'Se modifica el ingreso de material
                vlo_EntOttGestionIngresoMater.EstadoGestionIngreso = EstadoGestionIngr.DEVUELTA

                vlo_DalOttGestionIngresoMater.ModificarRegistro(vlo_EntOttGestionIngresoMater)

                'Se ingresa el registro de trazabilidad
                vlo_EntOtlTrazabilGestionIngr = New EntOtlTrazabilGestionIngr

                With vlo_EntOtlTrazabilGestionIngr
                    .Anno = vlo_EntOttGestionIngresoMater.Anno
                    .Consecutivo = vlo_EntOttGestionIngresoMater.Consecutivo
                    .IdUbicacion = vlo_EntOttGestionIngresoMater.IdUbicacion
                    .IdViaCompraContrato = vlo_EntOttGestionIngresoMater.IdViaCompraContrato
                    .NumeroGestion = vlo_EntOttGestionIngresoMater.NumeroGestion
                    .EstadoGestionIngreso = vlo_EntOttGestionIngresoMater.EstadoGestionIngreso
                    .Observaciones = pvc_Observaciones
                    .Usuario = pvc_UsuarioCrea
                End With

                vlo_DalOtlTrazabilGestionIngr.InsertarRegistro(vlo_EntOtlTrazabilGestionIngr)

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

        Public Function GuardarDetalle(pvo_DsDetalle As DataSet) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttDetalleGestionIngr As DalOttDetalleGestionIngr
            Dim vln_Resultado As Integer = 0

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttDetalleGestionIngr = New DalOttDetalleGestionIngr(vlo_Conexion)

                vlo_DalOttDetalleGestionIngr.AdapterOttDetalleGestionIngr(pvo_DsDetalle)

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

        Public Function TramitarValidacionMontos(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_NumeroGestion As Integer, pvn_Anno As Integer, pvn_Consecutivo As Integer, pvc_Observaciones As String, pvc_UsuarioCrea As String, pvo_DsDetalle As DataSet, pvn_IdUbicacionAdministra As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionIngresoMater As DalOttGestionIngresoMater
            Dim vlo_DalOtlTrazabilGestionIngr As DalOtlTrazabilGestionIngr
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vlo_DalOttDetAprovisionamiento As DalOttDetAprovisionamiento
            Dim vlo_DalOtlDetAprovisionamiento As DalOtlDetAprovisionamiento
            Dim vlo_DalOtlDetalleMaterial As DalOtlDetalleMaterial
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DalOtmMaterial As DalOtmMaterial
            Dim vlo_DalOtfInventario As DalOtfInventario
            Dim vlo_DalOtmAlmacenBodega As DalOtmAlmacenBodega
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_EntOttGestionCompra As EntOttGestionCompra
            Dim vlo_EntOtmAlmacenBodega As EntOtmAlmacenBodega
            Dim vlo_EntOtfInventario As EntOtfInventario
            Dim vlo_EntOtmMaterial As EntOtmMaterial
            Dim vlo_EntOttLineaGestionCompra As EntOttLineaGestionCompra
            Dim vlo_EntOttNuevaLineaGestionCompra As EntOttLineaGestionCompra
            Dim vlo_EntOtlDetalleMaterial As EntOtlDetalleMaterial
            Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial
            Dim vlo_EntOttNuevoDetalleMaterial As EntOttDetalleMaterial
            Dim vlo_EntOtlDetAprovisionamiento As EntOtlDetAprovisionamiento
            Dim vlo_EntOttDetAprovisionamiento As EntOttDetAprovisionamiento
            Dim vlo_EntOttNuevoDetAprovisionamiento As EntOttDetAprovisionamiento
            Dim vlo_EntOtlTrazabilGestionIngr As EntOtlTrazabilGestionIngr
            Dim vlo_EntOttGestionIngresoMater As EntOttGestionIngresoMater
            Dim vln_Resultado As Integer = 0
            Dim vlc_Condicion As String
            Dim vlb_Completo As Boolean = True
            Dim vln_NuevoIdDetalle As Integer
            Dim vlo_DsGestionesPendientes As DataSet
            Dim vlc_IdOrdenTrabajo As String = String.Empty

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionIngresoMater = New DalOttGestionIngresoMater(vlo_Conexion)
                vlo_DalOtlTrazabilGestionIngr = New DalOtlTrazabilGestionIngr(vlo_Conexion)
                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                vlo_DalOttDetAprovisionamiento = New DalOttDetAprovisionamiento(vlo_Conexion)
                vlo_DalOtlDetAprovisionamiento = New DalOtlDetAprovisionamiento(vlo_Conexion)
                vlo_DalOtlDetalleMaterial = New DalOtlDetalleMaterial(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOtmMaterial = New DalOtmMaterial(vlo_Conexion)
                vlo_DalOtfInventario = New DalOtfInventario(vlo_Conexion)
                vlo_DalOtmAlmacenBodega = New DalOtmAlmacenBodega(vlo_Conexion)
                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)

                vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_GESTION_INGRESO_MATER.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_GESTION_INGRESO_MATER.ANNO, pvn_Anno, Modelo.OTT_GESTION_INGRESO_MATER.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_GESTION_INGRESO_MATER.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTT_GESTION_INGRESO_MATER.CONSECUTIVO, pvn_Consecutivo)
                vlo_EntOttGestionIngresoMater = vlo_DalOttGestionIngresoMater.ObtenerRegistro(vlc_Condicion)

                'Se modifica el ingreso de material
                vlo_EntOttGestionIngresoMater.EstadoGestionIngreso = EstadoGestionIngr.TRAMITADA

                If vlo_EntOttGestionIngresoMater.Identificacion = "-" Then
                    vlo_EntOttGestionIngresoMater.Identificacion = String.Empty
                End If

                vlo_DalOttGestionIngresoMater.ModificarRegistro(vlo_EntOttGestionIngresoMater)

                vlo_EntOtmAlmacenBodega = vlo_DalOtmAlmacenBodega.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, pvn_IdUbicacion, Modelo.OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))

                For Each vlo_FilaDetalle In pvo_DsDetalle.Tables(0).Rows
                    vlo_EntOttDetalleMaterial = New EntOttDetalleMaterial
                    vlo_EntOttDetAprovisionamiento = New EntOttDetAprovisionamiento

                    'Se obtiene el detalle del material
                    If vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.ID_DETALLE_MATERIAL) = 0 Then
                        vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_DET_APROVISIONAMIENTO.ID_UBICACION, vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.ID_UBICACION), Modelo.OTT_DET_APROVISIONAMIENTO.NUMERO_GESTION, vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.NUMERO_GESTION), Modelo.OTT_DET_APROVISIONAMIENTO.ANNO, vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.ANNO), Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL, vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.ID_MATERIAL_TABLA))
                        vlo_EntOttDetAprovisionamiento = vlo_DalOttDetAprovisionamiento.ObtenerRegistro(vlc_Condicion)
                    Else
                        vlc_Condicion = String.Format("{0} = {1}", Modelo.OTT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL, vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.ID_DETALLE_MATERIAL))
                        vlo_EntOttDetalleMaterial = vlo_DalOttDetalleMaterial.ObtenerRegistro(vlc_Condicion)
                    End If

                    If vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_REQUERIDA).ToString = vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_INGRESA).ToString Then
                        'Si se completa la orden se cambia a recibido bodega para poder ser retirado
                        If vlo_EntOttDetalleMaterial.Existe Then
                            vlc_IdOrdenTrabajo = vlo_EntOttDetalleMaterial.IdOrdenTrabajo

                            vlo_EntOttDetalleMaterial.Estado = EstadoRegistro.RECIBIDO_BODEGA

                            vlo_DalOttDetalleMaterial.ModificarRegistro(vlo_EntOttDetalleMaterial)
                        ElseIf vlo_EntOttDetAprovisionamiento.Existe Then
                            vlo_EntOttDetAprovisionamiento.Estado = EstadoRegistro.RECIBIDO_BODEGA

                            vlo_DalOttDetAprovisionamiento.ModificarRegistro(vlo_EntOttDetAprovisionamiento)
                        End If
                    Else
                        'Se crea otra linea por la cantidad restante y se ajusta la cantidad de la existente, registrando la trazabilidad de los cambios
                        If vlo_EntOttDetalleMaterial.Existe Then
                            vlc_IdOrdenTrabajo = vlo_EntOttDetalleMaterial.IdOrdenTrabajo

                            'Se modifica la cantidad solicitada y el estado para que pueda ser retirada esa parte de los materiales que estan listos
                            vlo_EntOttDetalleMaterial.Estado = EstadoRegistro.RECIBIDO_BODEGA
                            vlo_EntOttDetalleMaterial.CantidadSolicitada = vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_INGRESA)

                            vlo_DalOttDetalleMaterial.ModificarRegistro(vlo_EntOttDetalleMaterial)

                            'Se registra el cambio en la trazabilidad
                            vlo_EntOtlDetalleMaterial = New EntOtlDetalleMaterial
                            vlo_EntOtlDetalleMaterial.CantidadSolicitada = vlo_EntOttDetalleMaterial.CantidadSolicitada
                            vlo_EntOtlDetalleMaterial.Detalle = vlo_EntOttDetalleMaterial.Detalle
                            vlo_EntOtlDetalleMaterial.IdDetalleMaterial = vlo_EntOttDetalleMaterial.IdDetalleMaterial
                            vlo_EntOtlDetalleMaterial.IdMaterial = vlo_EntOttDetalleMaterial.IdMaterial
                            vlo_EntOtlDetalleMaterial.IdOrdenTrabajo = vlo_EntOttDetalleMaterial.IdOrdenTrabajo
                            vlo_EntOtlDetalleMaterial.IdUbicacion = vlo_EntOttDetalleMaterial.IdUbicacion
                            vlo_EntOtlDetalleMaterial.IdUbicacionAdministra = vlo_EntOttDetalleMaterial.IdUbicacionAdministra
                            vlo_EntOtlDetalleMaterial.Usuario = pvc_UsuarioCrea

                            vlo_DalOtlDetalleMaterial.InsertarRegistro(vlo_EntOtlDetalleMaterial)

                            'Se registra un nuevo detalle de material con la cantidad faltante de entregar
                            vlo_EntOttNuevoDetalleMaterial = New EntOttDetalleMaterial

                            vlo_EntOttNuevoDetalleMaterial.CantidadSolicitada = CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_REQUERIDA), Double) - CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_INGRESA), Double)
                            vlo_EntOttNuevoDetalleMaterial.Detalle = vlo_EntOttDetalleMaterial.Detalle
                            vlo_EntOttNuevoDetalleMaterial.IdMaterial = vlo_EntOttDetalleMaterial.IdMaterial
                            vlo_EntOttNuevoDetalleMaterial.IdOrdenTrabajo = vlo_EntOttDetalleMaterial.IdOrdenTrabajo
                            vlo_EntOttNuevoDetalleMaterial.IdUbicacion = vlo_EntOttDetalleMaterial.IdUbicacion
                            vlo_EntOttNuevoDetalleMaterial.IdUbicacionAdministra = vlo_EntOttDetalleMaterial.IdUbicacionAdministra
                            vlo_EntOttNuevoDetalleMaterial.Usuario = pvc_UsuarioCrea
                            vlo_EntOttNuevoDetalleMaterial.ViaDespacho = vlo_EntOttDetalleMaterial.ViaDespacho
                            vlo_EntOttNuevoDetalleMaterial.IdAlmacenBodega = vlo_EntOttDetalleMaterial.IdAlmacenBodega
                            vlo_EntOttNuevoDetalleMaterial.IdViaCompraContrato = vlo_EntOttDetalleMaterial.IdViaCompraContrato
                            vlo_EntOttNuevoDetalleMaterial.Estado = EstadoRegistro.EN_PROCESO_COMPRA

                            vln_NuevoIdDetalle = vlo_DalOttDetalleMaterial.InsertarRegistro(vlo_EntOttNuevoDetalleMaterial)

                            'Se actualiza el costo promedio del material
                            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_MATERIAL.ID_UBICACION_ADMINISTRA, vlo_EntOttNuevoDetalleMaterial.IdUbicacionAdministra, Modelo.OTM_MATERIAL.ID_MATERIAL, vlo_EntOttDetalleMaterial.IdMaterial)
                            vlo_EntOtmMaterial = vlo_DalOtmMaterial.ObtenerRegistro(vlc_Condicion)

                            If vlo_EntOtmMaterial.CostoPromedio = 0 Then
                                vlo_EntOtmMaterial.CostoPromedio = CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.COSTO_INDIVIDUAL), Double)
                            Else
                                vlo_EntOtmMaterial.CostoPromedio = (vlo_EntOtmMaterial.CostoPromedio + CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.COSTO_INDIVIDUAL), Double)) / 2
                            End If

                            vlo_DalOtmMaterial.ModificarRegistro(vlo_EntOtmMaterial)

                            'Se ajusta el inventario

                            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTF_INVENTARIO.ID_MATERIAL, vlo_EntOttDetalleMaterial.IdMaterial, Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOtmAlmacenBodega.IdAlmacenBodega)
                            vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(vlc_Condicion)

                            vlo_EntOtfInventario.CantidadDisponible = vlo_EntOtfInventario.CantidadDisponible + CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_INGRESA), Double)
                            vlo_EntOtfInventario.CantidadReservada = vlo_EntOtfInventario.CantidadReservada + CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_INGRESA), Double)

                            vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)

                            'Se obtiene la linea de la gestion de compra asociada a ese registro para ajustar y crear una nueva linea con el faltante
                            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_LINEA_GESTION_COMPRA.ANNO, pvn_Anno, Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTT_LINEA_GESTION_COMPRA.ID_LINEA_GESTION_COMPRA, vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.ID_LINEA_GESTION_COMPRA))
                            vlo_EntOttLineaGestionCompra = vlo_DalOttLineaGestionCompra.ObtenerRegistro(vlc_Condicion)

                            vlo_EntOttLineaGestionCompra.CantidadSolicitada = vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_INGRESA)

                            vlo_DalOttLineaGestionCompra.ModificarRegistro(vlo_EntOttLineaGestionCompra)

                            vlo_EntOttNuevaLineaGestionCompra = New EntOttLineaGestionCompra

                            vlo_EntOttNuevaLineaGestionCompra.Anno = pvn_Anno
                            vlo_EntOttNuevaLineaGestionCompra.CantidadSolicitada = CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_REQUERIDA), Double) - CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_INGRESA), Double)
                            vlo_EntOttNuevaLineaGestionCompra.IdDetalleMaterial = vln_NuevoIdDetalle
                            vlo_EntOttNuevaLineaGestionCompra.IdMaterial = vlo_EntOttLineaGestionCompra.IdMaterial
                            vlo_EntOttNuevaLineaGestionCompra.IdUbicacion = pvn_IdUbicacion
                            vlo_EntOttNuevaLineaGestionCompra.IdViaCompraContrato = pvn_IdViaCompraContrato
                            vlo_EntOttNuevaLineaGestionCompra.NumeroGestion = pvn_NumeroGestion
                            vlo_EntOttNuevaLineaGestionCompra.Usuario = pvc_UsuarioCrea

                            vlo_DalOttLineaGestionCompra.InsertarRegistro(vlo_EntOttNuevaLineaGestionCompra)

                        ElseIf vlo_EntOttDetAprovisionamiento.Existe Then

                            'Se modifica la cantidad solicitada y el estado para que pueda ser retirada esa parte de los materiales que estan listos
                            vlo_EntOttDetAprovisionamiento.Estado = EstadoRegistro.RECIBIDO_BODEGA
                            vlo_EntOttDetAprovisionamiento.Cantidad = vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_INGRESA)

                            vlo_DalOttDetAprovisionamiento.ModificarRegistro(vlo_EntOttDetAprovisionamiento)

                            'Se registra el cambio en la trazabilidad
                            vlo_EntOtlDetAprovisionamiento = New EntOtlDetAprovisionamiento
                            vlo_EntOtlDetAprovisionamiento.Cantidad = vlo_EntOttDetAprovisionamiento.Cantidad
                            vlo_EntOtlDetAprovisionamiento.NumeroGestion = vlo_EntOttDetAprovisionamiento.NumeroGestion
                            vlo_EntOtlDetAprovisionamiento.Anno = vlo_EntOttDetAprovisionamiento.Anno
                            vlo_EntOtlDetAprovisionamiento.IdMaterial = vlo_EntOttDetAprovisionamiento.IdMaterial
                            vlo_EntOtlDetAprovisionamiento.Observaciones = vlo_EntOttDetAprovisionamiento.Observaciones
                            vlo_EntOtlDetAprovisionamiento.IdUbicacion = vlo_EntOttDetAprovisionamiento.IdUbicacion
                            vlo_EntOtlDetAprovisionamiento.Estado = vlo_EntOttDetAprovisionamiento.Estado
                            vlo_EntOtlDetAprovisionamiento.Usuario = pvc_UsuarioCrea

                            vlo_DalOtlDetAprovisionamiento.InsertarRegistro(vlo_EntOtlDetAprovisionamiento)

                            'Se registra un nuevo detalle de material con la cantidad faltante de entregar
                            vlo_EntOttNuevoDetAprovisionamiento = New EntOttDetAprovisionamiento

                            vlo_EntOttNuevoDetAprovisionamiento.Cantidad = CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_REQUERIDA), Double) - CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_INGRESA), Double)
                            vlo_EntOttNuevoDetAprovisionamiento.IdUbicacion = vlo_EntOttDetAprovisionamiento.IdUbicacion
                            vlo_EntOttNuevoDetAprovisionamiento.NumeroGestion = vlo_EntOttDetAprovisionamiento.NumeroGestion
                            vlo_EntOttNuevoDetAprovisionamiento.Anno = vlo_EntOttDetAprovisionamiento.Anno
                            vlo_EntOttNuevoDetAprovisionamiento.IdMaterial = vlo_EntOttDetAprovisionamiento.IdMaterial
                            vlo_EntOttNuevoDetAprovisionamiento.Observaciones = vlo_EntOttDetAprovisionamiento.Observaciones
                            vlo_EntOttNuevoDetAprovisionamiento.Usuario = pvc_UsuarioCrea
                            vlo_EntOttNuevoDetAprovisionamiento.Estado = EstadoRegistro.EN_PROCESO_COMPRA

                            vln_NuevoIdDetalle = vlo_DalOttDetAprovisionamiento.InsertarRegistro(vlo_EntOttNuevoDetAprovisionamiento)

                            'Se actualiza el costo promedio del material
                            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_MATERIAL.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTM_MATERIAL.ID_MATERIAL, vlo_EntOttDetAprovisionamiento.IdMaterial)
                            vlo_EntOtmMaterial = vlo_DalOtmMaterial.ObtenerRegistro(vlc_Condicion)

                            If vlo_EntOtmMaterial.CostoPromedio = 0 Then
                                vlo_EntOtmMaterial.CostoPromedio = CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.COSTO_INDIVIDUAL), Double)
                            Else
                                vlo_EntOtmMaterial.CostoPromedio = (vlo_EntOtmMaterial.CostoPromedio + CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.COSTO_INDIVIDUAL), Double)) / 2
                            End If

                            vlo_DalOtmMaterial.ModificarRegistro(vlo_EntOtmMaterial)

                            'Se ajusta el inventario

                            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTF_INVENTARIO.ID_MATERIAL, vlo_EntOttDetAprovisionamiento.IdMaterial, Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, vlo_EntOtmAlmacenBodega.IdAlmacenBodega)
                            vlo_EntOtfInventario = vlo_DalOtfInventario.ObtenerRegistro(vlc_Condicion)

                            vlo_EntOtfInventario.CantidadDisponible = vlo_EntOtfInventario.CantidadDisponible + CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_INGRESA), Double)

                            vlo_DalOtfInventario.ModificarRegistro(vlo_EntOtfInventario)

                            'Se obtiene la linea de la solicitud de aprovisionamiento asociada a ese registro para ajustar y crear una nueva linea con el faltante
                            vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_LINEA_GESTION_COMPRA.ANNO, pvn_Anno, Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTT_LINEA_GESTION_COMPRA.ID_LINEA_GESTION_COMPRA, vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.ID_LINEA_GESTION_COMPRA))
                            vlo_EntOttLineaGestionCompra = vlo_DalOttLineaGestionCompra.ObtenerRegistro(vlc_Condicion)

                            vlo_EntOttLineaGestionCompra.CantidadSolicitada = vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_INGRESA)

                            vlo_DalOttLineaGestionCompra.ModificarRegistro(vlo_EntOttLineaGestionCompra)

                            vlo_EntOttNuevaLineaGestionCompra = New EntOttLineaGestionCompra

                            vlo_EntOttNuevaLineaGestionCompra.Anno = pvn_Anno
                            vlo_EntOttNuevaLineaGestionCompra.CantidadSolicitada = CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_REQUERIDA), Double) - CType(vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_INGRESA), Double)
                            vlo_EntOttNuevaLineaGestionCompra.IdDetalleMaterial = vln_NuevoIdDetalle
                            vlo_EntOttNuevaLineaGestionCompra.IdMaterial = vlo_EntOttLineaGestionCompra.IdMaterial
                            vlo_EntOttNuevaLineaGestionCompra.IdUbicacion = pvn_IdUbicacion
                            vlo_EntOttNuevaLineaGestionCompra.IdViaCompraContrato = pvn_IdViaCompraContrato
                            vlo_EntOttNuevaLineaGestionCompra.NumeroGestion = pvn_NumeroGestion
                            vlo_EntOttNuevaLineaGestionCompra.Usuario = pvc_UsuarioCrea

                            vlo_DalOttLineaGestionCompra.InsertarRegistro(vlo_EntOttNuevaLineaGestionCompra)
                        End If

                    End If
                Next

                'Se verifica si ya dio tramite a todos los materiales para cambiar el estado de la gestion de compra
                vlc_Condicion = String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO, vlc_IdOrdenTrabajo, Modelo.OTT_DETALLE_MATERIAL.ESTADO, EstadoRegistro.EN_PROCESO_COMPRA)
                vlo_DsGestionesPendientes = vlo_DalOttDetalleMaterial.ListarRegistros(vlc_Condicion, String.Empty, False, 0, 0)

                If vlo_DsGestionesPendientes.Tables(0).Rows.Count = 0 Then
                    vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_GESTION_COMPRA.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_GESTION_COMPRA.ANNO, pvn_Anno, Modelo.OTT_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_GESTION_COMPRA.NUMERO_GESTION, pvn_NumeroGestion)
                    vlo_EntOttGestionCompra = vlo_DalOttGestionCompra.ObtenerRegistro(vlc_Condicion)

                    vlo_EntOttGestionCompra.Estado = EstadoGestionCompra.REGISTRO_DE_INGRESOS_EN_ALMACEN

                    vlo_DalOttGestionCompra.ModificarRegistro(vlo_EntOttGestionCompra)
                End If

                'Se ingresa el registro de trazabilidad
                vlo_EntOtlTrazabilGestionIngr = New EntOtlTrazabilGestionIngr

                With vlo_EntOtlTrazabilGestionIngr
                    .Anno = vlo_EntOttGestionIngresoMater.Anno
                    .Consecutivo = vlo_EntOttGestionIngresoMater.Consecutivo
                    .IdUbicacion = vlo_EntOttGestionIngresoMater.IdUbicacion
                    .IdViaCompraContrato = vlo_EntOttGestionIngresoMater.IdViaCompraContrato
                    .NumeroGestion = vlo_EntOttGestionIngresoMater.NumeroGestion
                    .EstadoGestionIngreso = vlo_EntOttGestionIngresoMater.EstadoGestionIngreso
                    .Observaciones = pvc_Observaciones
                    .Usuario = pvc_UsuarioCrea
                End With

                vlo_DalOtlTrazabilGestionIngr.InsertarRegistro(vlo_EntOtlTrazabilGestionIngr)

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

        Public Function ExcluirMateriales(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_NumeroGestion As Integer, pvn_Anno As Integer, pvc_UsuarioCrea As String, pvo_DsDetalle As DataSet, pvn_IdUbicacionAdministra As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vlo_DalOtlDetalleMaterial As DalOtlDetalleMaterial
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOttGestionIngresoMater As DalOttGestionIngresoMater
            Dim vlo_EntOtlDetalleMaterial As EntOtlDetalleMaterial
            Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial
            Dim vlo_EntOttNuevoDetalleMaterial As EntOttDetalleMaterial
            Dim vln_Resultado As Integer = 0
            Dim vlc_Condicion As String
            Dim vlb_Completo As Boolean = True
            Dim vln_NuevoIdDetalle As Integer
            Dim vlc_IdOrdenTrabajo As String = String.Empty
            Dim vlo_DsGestionIngr As DataSet
            Dim vln_CantidadRestante As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionIngresoMater = New DalOttGestionIngresoMater(vlo_Conexion)
                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                vlo_DalOtlDetalleMaterial = New DalOtlDetalleMaterial(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)

                For Each vlo_FilaDetalle In pvo_DsDetalle.Tables(0).Rows
                    'Se obtiene el detalle del material
                    vlc_Condicion = String.Format("{0} = {1}", Modelo.OTT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL, vlo_FilaDetalle(Modelo.OTT_LINEA_GESTION_COMPRA.ID_DETALLE_MATERIAL))
                    vlo_EntOttDetalleMaterial = vlo_DalOttDetalleMaterial.ObtenerRegistro(vlc_Condicion)

                    vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_DETALLE_GESTION_INGR.ANNO, pvn_Anno, Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_DETALLE_GESTION_INGR.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA, vlo_FilaDetalle(Modelo.OTT_LINEA_GESTION_COMPRA.ID_LINEA_GESTION_COMPRA))
                    vlo_DsGestionIngr = vlo_DalOttGestionIngresoMater.ListarRegistros(vlc_Condicion, String.Empty, False, 0, 0)


                    If vlo_DsGestionIngr.Tables(0).Rows.Count > 0 Then

                        vln_CantidadRestante = vlo_FilaDetalle(Modelo.V_OTT_LINEA_GESTION_COMPRALST.CANTIDAD_SOLICITADA)

                        For Each vlo_Row In vlo_DsGestionIngr.Tables(0).Rows
                            vln_CantidadRestante = vln_CantidadRestante - vlo_Row(Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA)
                        Next

                        If vln_CantidadRestante > 0 Then
                            'Se modifica la cantidad solicitada con la cntidad ingresada en la solicitud de ingreso de materiales
                            vlo_EntOttDetalleMaterial.CantidadSolicitada = vlo_FilaDetalle(Modelo.V_OTT_DETALLE_GESTION_INGRLST.CANTIDAD_INGRESA) - vln_CantidadRestante

                            vlo_DalOttDetalleMaterial.ModificarRegistro(vlo_EntOttDetalleMaterial)

                            'Se registra un nuevo detalle de material con la cantidad a excluir
                            vlo_EntOttNuevoDetalleMaterial = New EntOttDetalleMaterial

                            vlo_EntOttNuevoDetalleMaterial.CantidadSolicitada = vln_CantidadRestante
                            vlo_EntOttNuevoDetalleMaterial.Detalle = vlo_EntOttDetalleMaterial.Detalle
                            vlo_EntOttNuevoDetalleMaterial.IdMaterial = vlo_EntOttDetalleMaterial.IdMaterial
                            vlo_EntOttNuevoDetalleMaterial.IdOrdenTrabajo = vlo_EntOttDetalleMaterial.IdOrdenTrabajo
                            vlo_EntOttNuevoDetalleMaterial.IdUbicacion = vlo_EntOttDetalleMaterial.IdUbicacion
                            vlo_EntOttNuevoDetalleMaterial.IdUbicacionAdministra = vlo_EntOttDetalleMaterial.IdUbicacionAdministra
                            vlo_EntOttNuevoDetalleMaterial.Usuario = pvc_UsuarioCrea
                            vlo_EntOttNuevoDetalleMaterial.ViaDespacho = vlo_EntOttDetalleMaterial.ViaDespacho
                            vlo_EntOttNuevoDetalleMaterial.IdAlmacenBodega = vlo_EntOttDetalleMaterial.IdAlmacenBodega
                            vlo_EntOttNuevoDetalleMaterial.IdViaCompraContrato = vlo_EntOttDetalleMaterial.IdViaCompraContrato
                            vlo_EntOttNuevoDetalleMaterial.Estado = EstadoRegistro.APROBADA

                            vln_NuevoIdDetalle = vlo_DalOttDetalleMaterial.InsertarRegistro(vlo_EntOttNuevoDetalleMaterial)
                        End If

                        'Se registra el cambio en la trazabilidad
                        'vlo_EntOtlDetalleMaterial = New EntOtlDetalleMaterial
                        'vlo_EntOtlDetalleMaterial.CantidadSolicitada = vlo_EntOttDetalleMaterial.CantidadSolicitada
                        'vlo_EntOtlDetalleMaterial.Detalle = vlo_EntOttDetalleMaterial.Detalle
                        'vlo_EntOtlDetalleMaterial.IdDetalleMaterial = vlo_EntOttDetalleMaterial.IdDetalleMaterial
                        'vlo_EntOtlDetalleMaterial.IdMaterial = vlo_EntOttDetalleMaterial.IdMaterial
                        'vlo_EntOtlDetalleMaterial.IdOrdenTrabajo = vlo_EntOttDetalleMaterial.IdOrdenTrabajo
                        'vlo_EntOtlDetalleMaterial.IdUbicacion = vlo_EntOttDetalleMaterial.IdUbicacion
                        'vlo_EntOtlDetalleMaterial.IdUbicacionAdministra = vlo_EntOttDetalleMaterial.IdUbicacionAdministra
                        'vlo_EntOtlDetalleMaterial.Usuario = pvc_UsuarioCrea

                        'vlo_DalOtlDetalleMaterial.InsertarRegistro(vlo_EntOtlDetalleMaterial)
                    Else
                        vlo_EntOttDetalleMaterial.Estado = EstadoRegistro.APROBADA

                        vlo_DalOttDetalleMaterial.ModificarRegistro(vlo_EntOttDetalleMaterial)
                    End If


                Next



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

#End Region

    End Class
End Namespace
