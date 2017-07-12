Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports WsrGestorNotificaciones
Imports System.Configuration
Imports System.Data

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttGestionCompra
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

#Region "Métodos"
        Public Sub NotificarProveedorFondoTrabajoAprobJefatura(ByVal pvo_EntOttGestionCompra As EntOttGestionCompra)
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlc_CuerpoCorreo As String
            Dim vlc_Destinatarios As String
            Dim vlo_DalOtpParametroUbicacion As DalOtpParametroUbicacion
            Dim vlo_EntOtpParametroUbicacion As EntOtpParametroUbicacion
            Dim vlc_EncargadoRecepcionMateriales As String
            Dim vlc_LugarRecepcionMateriales As String
            Dim vlc_DatosFormaPagoContacto As String
            Dim vlc_Asunto As String
            Dim vlc_Condicion As String
            Dim vlc_Orden As String
            Dim vlo_EntGntDestinatario As EntGNT_DESTINATARIO
            Dim vlo_ListaDestinatarios As List(Of EntGNT_DESTINATARIO)
            Dim vlo_DalOttProveedorCotizacion As DalOttProveedorCotizacion
            Dim vlo_DsAdjudicado As Data.DataSet
            Dim vlc_NombreProveedor As String
            Dim vlo_DsNotificaciones As DataSet
            Dim vlo_DrFilaNotificacion As DataRow
            Dim vlc_Correo As String

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtpParametroUbicacion = New DalOtpParametroUbicacion(vlo_Conexion)
                vlo_DalOttProveedorCotizacion = New DalOttProveedorCotizacion(vlo_Conexion)
                vlo_EntGntDestinatario = New EntGNT_DESTINATARIO
                vlo_ListaDestinatarios = New List(Of EntGNT_DESTINATARIO)

                vlo_DsNotificaciones = New DataSet

                vlo_DsNotificaciones.Tables.Add(New DataTable("NOTIFICACIONES"))

                vlo_DsNotificaciones.Tables(0).Columns.Add(New DataColumn(Utilerias.GestorNotificaciones.Modelo.GNT_NOTIFICACION.ASUNTO, GetType(String)))
                vlo_DsNotificaciones.Tables(0).Columns.Add(New DataColumn(Utilerias.GestorNotificaciones.Modelo.GNT_NOTIFICACION.CUERPO, GetType(String)))
                vlo_DsNotificaciones.Tables(0).Columns.Add(New DataColumn(Utilerias.GestorNotificaciones.Modelo.GNT_NOTIFICACION.ES_HTML, GetType(Integer)))
                vlo_DsNotificaciones.Tables(0).Columns.Add(New DataColumn(Utilerias.GestorNotificaciones.Modelo.GNT_NOTIFICACION.USUARIO_CREA, GetType(String)))
                vlo_DsNotificaciones.Tables(0).Columns.Add(New DataColumn(Utilerias.GestorNotificaciones.Modelo.GNT_ARCHIVO_ADJUNTO.ARCHIVO, GetType(Byte())))
                vlo_DsNotificaciones.Tables(0).Columns.Add(New DataColumn(Utilerias.GestorNotificaciones.Modelo.GNT_DESTINATARIO.DESTINATARIO, GetType(String)))
                vlo_DsNotificaciones.Tables(0).Columns.Add(New DataColumn(Utilerias.GestorNotificaciones.Modelo.GNT_ARCHIVO_ADJUNTO.NOMBRE_ARCHIVO, GetType(String)))
                vlo_DsNotificaciones.Tables(0).Columns(Utilerias.GestorNotificaciones.Modelo.GNT_NOTIFICACION.ASUNTO).DefaultValue = "Adjudicación de la cotización de material"
                vlo_DsNotificaciones.Tables(0).Columns(Utilerias.GestorNotificaciones.Modelo.GNT_NOTIFICACION.ES_HTML).DefaultValue = 1

                vlc_Condicion = String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Utilerias.OrdenesDeTrabajo.Parametros.ENCARGADO_RECEPCION_MATERIALES)
                vlo_EntOtpParametroUbicacion = vlo_DalOtpParametroUbicacion.ObtenerRegistro(vlc_Condicion)
                vlc_EncargadoRecepcionMateriales = vlo_EntOtpParametroUbicacion.Valor

                vlc_Condicion = String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Utilerias.OrdenesDeTrabajo.Parametros.LUGAR_RECEPCION_MATERIALES)
                vlo_EntOtpParametroUbicacion = vlo_DalOtpParametroUbicacion.ObtenerRegistro(vlc_Condicion)
                vlc_LugarRecepcionMateriales = vlo_EntOtpParametroUbicacion.Valor

                vlc_Condicion = String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Utilerias.OrdenesDeTrabajo.Parametros.DATOS_FORMA_PAGO_CONTACTO)
                vlo_EntOtpParametroUbicacion = vlo_DalOtpParametroUbicacion.ObtenerRegistro(vlc_Condicion)
                vlc_DatosFormaPagoContacto = vlo_EntOtpParametroUbicacion.Valor

                'Se obtiene el proveedor adjudicado
                vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = 1", Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.ID_UBICACION, pvo_EntOttGestionCompra.IdUbicacion, Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.ID_VIA_COMPRA_CONTRATO, pvo_EntOttGestionCompra.IdViaCompraContrato, Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.ANNO, pvo_EntOttGestionCompra.Anno, Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.NUMERO_GESTION, pvo_EntOttGestionCompra.NumeroGestion, Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.ADJUDICADO)
                vlo_DsAdjudicado = vlo_DalOttProveedorCotizacion.ListarRegistrosLista(vlc_Condicion, String.Empty, False, 0, 0)

                vlc_NombreProveedor = vlo_DsAdjudicado.Tables(0).Rows(0).Item(Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.NOMBRE_PROVEEDOR)

                vlc_Destinatarios = vlo_DsAdjudicado.Tables(0).Rows(0).Item(Modelo.V_OTT_PROVEEDOR_COTIZACIONLST.CORREO_PROVEDOR)

                vlc_Asunto = "Adjudicación de la cotización de material"

                vlc_CuerpoCorreo = String.Format("<b>Estimado {0}</b><br /><br />Se adjunta la adjudicación de la cotización de material y la proforma aprobada con el fin de que este sea entregado a la brevedad en el Almacén de Materiales y Herramientas de la Sección de Mantenimiento y Construcción de la Oficina de Servicios Generales, Universidad de Costa Rica, agradecemos coordine anticipadamente la entrega del material con {1}<br /><br />Ubicación: {2}<br /><br />{3}<br /><br />Saludos,</b>",
                                                   vlc_NombreProveedor, vlc_EncargadoRecepcionMateriales, vlc_LugarRecepcionMateriales, vlc_DatosFormaPagoContacto)
                vlc_CuerpoCorreo = CorregirCaracteresConAcento(vlc_CuerpoCorreo)

                For Each vlc_Correo In vlc_Destinatarios.Split(",")
                    vlo_DrFilaNotificacion = vlo_DsNotificaciones.Tables(0).NewRow

                    vlo_DrFilaNotificacion(Utilerias.GestorNotificaciones.Modelo.GNT_NOTIFICACION.CUERPO) = vlc_CuerpoCorreo
                    vlo_DrFilaNotificacion(Utilerias.GestorNotificaciones.Modelo.GNT_NOTIFICACION.USUARIO_CREA) = "SISTEMA"
                    vlo_DrFilaNotificacion(Utilerias.GestorNotificaciones.Modelo.GNT_ARCHIVO_ADJUNTO.ARCHIVO) = Nothing
                    vlo_DrFilaNotificacion(Utilerias.GestorNotificaciones.Modelo.GNT_DESTINATARIO.DESTINATARIO) = vlc_Correo.Trim
                    vlo_DrFilaNotificacion(Utilerias.GestorNotificaciones.Modelo.GNT_ARCHIVO_ADJUNTO.NOMBRE_ARCHIVO) = String.Empty


                    vlo_DsNotificaciones.Tables(0).Rows.Add(vlo_DrFilaNotificacion)
                Next

                EnviarMultiplesCorreo(vlo_DsNotificaciones)


            Catch vlo_Exc As Exception

                Throw

            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Sub
#End Region

#Region "Funciones"
        ''' <summary>
        ''' Permite agregar un registro en la tabla OTT_GESTION_COMPRA, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttGestionCompra) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdViaCompraContrato, pvo_Registro.Anno, pvo_Registro.NumeroGestion).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
                End If

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vln_Resultado = vlo_DalOttGestionCompra.InsertarRegistro(pvo_Registro)
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
        ''' Permite agregar un registro en la tabla OTT_GESTION_COMPRA
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>23/08/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarGestionCompraRapida(pvo_DsDetalles As Data.DataSet, pvc_NombreUsuario As String, pvn_IdUbicacion As Integer) As String
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vlo_EntOttGestionCompra As EntOttGestionCompra
            Dim vlo_DsLineaGestionCompra As Data.DataSet
            Dim vlo_EntOtpParametroUbicacion As EntOtpParametroUbicacion
            Dim vlo_DalOtpParametroUbicacion As DalOtpParametroUbicacion
            Dim vlc_Resultado As String = String.Empty
            Dim vln_NumeroLinea As Integer = 1
            Dim vlo_DrFilaLineaGestionCompra As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New DalOtpParametroUbicacion(vlo_Conexion)

                vlo_EntOtpParametroUbicacion = vlo_DalOtpParametroUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.VALOR_SECUENCIA_COMPRA_RAPIDA))

                vlo_EntOttGestionCompra = New EntOttGestionCompra
                vlo_EntOttGestionCompra.IdUbicacion = pvn_IdUbicacion
                vlo_EntOttGestionCompra.IdViaCompraContrato = vlo_EntOtpParametroUbicacion.Valor
                vlo_EntOttGestionCompra.Anno = DateTime.Now.Year
                vlo_EntOttGestionCompra.NumeroGestion = vlo_DalOttGestionCompra.ObtenerFnOtNumeroGestionCompra(vlo_EntOttGestionCompra.Anno, vlo_EntOttGestionCompra.IdUbicacion, vlo_EntOttGestionCompra.IdViaCompraContrato) + 1
                vlo_EntOttGestionCompra.FechaRegistroSolicitud = DateTime.Now
                vlo_EntOttGestionCompra.Estado = EstadoGestionCompra.CREADA
                vlo_EntOttGestionCompra.Usuario = pvc_NombreUsuario

                vlo_DalOttGestionCompra.InsertarRegistro(vlo_EntOttGestionCompra)

                vlo_DsLineaGestionCompra = vlo_DalOttLineaGestionCompra.ListarRegistros("1 = 0", String.Empty, False, 0, 0)

                For Each vlo_FilaDetalle In pvo_DsDetalles.Tables(0).Rows
                    If vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO).ToString = "1" Then
                        vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ESTADO) = EstadoRegistro.EN_PROCESO_COMPRA

                        vlo_DrFilaLineaGestionCompra = vlo_DsLineaGestionCompra.Tables(0).NewRow

                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION)) = vlo_EntOttGestionCompra.IdUbicacion
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO)) = vlo_EntOttGestionCompra.IdViaCompraContrato
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ANNO)) = vlo_EntOttGestionCompra.Anno
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION)) = vlo_EntOttGestionCompra.NumeroGestion
                        'vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_LINEA)) = vln_NumeroLinea
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_DETALLE_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_SOLICITADA)) = CType(vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA), Double)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.USUARIO)) = pvc_NombreUsuario

                        vlo_DsLineaGestionCompra.Tables(0).Rows.Add(vlo_DrFilaLineaGestionCompra)
                        vln_NumeroLinea = vln_NumeroLinea + 1
                    End If
                Next

                vlo_DalOttDetalleMaterial.AdapterOttDetalleMaterialGestionCompra(pvo_DsDetalles)

                vlo_DalOttLineaGestionCompra.AdapterOttLineaGestionCompra(vlo_DsLineaGestionCompra)

                vlo_Conexion.TransaccionCommit()

                vlc_Resultado = String.Format("{0},{1},{2},{3}", vlo_EntOttGestionCompra.IdUbicacion, vlo_EntOttGestionCompra.IdViaCompraContrato, vlo_EntOttGestionCompra.Anno, vlo_EntOttGestionCompra.NumeroGestion)
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlc_Resultado
        End Function

        ''' <summary>
        ''' Permite agregar un registro en la tabla OTT_GESTION_COMPRA
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>25/08/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarGestionCompraFondoTrabajo(pvo_DsDetalles As Data.DataSet, pvc_NombreUsuario As String, pvn_IdUbicacion As Integer, pvo_Observaciones As String) As String
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_EntOttGestionCompra As EntOttGestionCompra
            Dim vlo_DsLineaGestionCompra As Data.DataSet
            Dim vlo_EntOtpParametroUbicacion As EntOtpParametroUbicacion
            Dim vlo_DalOtpParametroUbicacion As DalOtpParametroUbicacion
            Dim vlc_Resultado As String = String.Empty
            Dim vlo_DrFilaLineaGestionCompra As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New DalOtpParametroUbicacion(vlo_Conexion)

                vlo_EntOtpParametroUbicacion = vlo_DalOtpParametroUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.VALOR_SECUENCIA_FONDO_DE_TRABAJO))

                vlo_EntOttGestionCompra = New EntOttGestionCompra
                vlo_EntOttGestionCompra.IdUbicacion = pvn_IdUbicacion
                vlo_EntOttGestionCompra.IdViaCompraContrato = vlo_EntOtpParametroUbicacion.Valor
                vlo_EntOttGestionCompra.Anno = DateTime.Now.Year
                vlo_EntOttGestionCompra.NumeroGestion = vlo_DalOttGestionCompra.ObtenerFnOtNumeroGestionCompra(vlo_EntOttGestionCompra.Anno, vlo_EntOttGestionCompra.IdUbicacion, vlo_EntOttGestionCompra.IdViaCompraContrato) + 1
                vlo_EntOttGestionCompra.FechaRegistroSolicitud = DateTime.Now
                vlo_EntOttGestionCompra.Estado = EstadoGestionCompra.CREADA
                vlo_EntOttGestionCompra.Usuario = pvc_NombreUsuario
                vlo_EntOttGestionCompra.Observaciones = pvo_Observaciones

                vlo_DalOttGestionCompra.InsertarRegistro(vlo_EntOttGestionCompra)

                vlo_DsLineaGestionCompra = vlo_DalOttLineaGestionCompra.ListarRegistros("1 = 0", String.Empty, False, 0, 0)

                For Each vlo_FilaDetalle In pvo_DsDetalles.Tables(0).Rows
                    If vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO).ToString = "1" Then

                        vlo_DrFilaLineaGestionCompra = vlo_DsLineaGestionCompra.Tables(0).NewRow

                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION)) = vlo_EntOttGestionCompra.IdUbicacion
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO)) = vlo_EntOttGestionCompra.IdViaCompraContrato
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ANNO)) = vlo_EntOttGestionCompra.Anno
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION)) = vlo_EntOttGestionCompra.NumeroGestion
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_DETALLE_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_SOLICITADA)) = CType(vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA), Double)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.USUARIO)) = pvc_NombreUsuario

                        vlo_DsLineaGestionCompra.Tables(0).Rows.Add(vlo_DrFilaLineaGestionCompra)
                    End If
                Next

                vlo_DalOttLineaGestionCompra.AdapterOttLineaGestionCompra(vlo_DsLineaGestionCompra)

                vlo_Conexion.TransaccionCommit()

                vlc_Resultado = String.Format("{0},{1},{2},{3}", vlo_EntOttGestionCompra.IdUbicacion, vlo_EntOttGestionCompra.IdViaCompraContrato, vlo_EntOttGestionCompra.Anno, vlo_EntOttGestionCompra.NumeroGestion)
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlc_Resultado
        End Function

        ''' <summary>
        ''' Permite agregar un registro en la tabla OTT_GESTION_COMPRA
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>07/10/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarGestionUnidadEspecializada(pvo_DsDetalles As Data.DataSet, pvc_NombreUsuario As String, pvn_IdUbicacion As Integer, pvo_Observaciones As String) As String
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_EntOttGestionCompra As EntOttGestionCompra
            Dim vlo_DsLineaGestionCompra As Data.DataSet
            Dim vlo_EntOtpParametroUbicacion As EntOtpParametroUbicacion
            Dim vlo_DalOtpParametroUbicacion As DalOtpParametroUbicacion
            Dim vlo_DalOttGrupoGestionCompra As DalOttGrupoGestionCompra
            Dim vlc_Resultado As String = String.Empty
            Dim vlo_DrFilaLineaGestionCompra As Data.DataRow
            Dim vlo_DsLineas As Data.DataSet
            Dim vlo_DsEstructuraGrupo As Data.DataSet
            Dim vlo_DrFilaGrupo As Data.DataRow
            Dim vln_NumeroLinea As Integer = 1

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New DalOtpParametroUbicacion(vlo_Conexion)
                vlo_DalOttGrupoGestionCompra = New DalOttGrupoGestionCompra(vlo_Conexion)

                vlo_EntOtpParametroUbicacion = vlo_DalOtpParametroUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.VALOR_SECUENCIA_UNIDAD_ESPECIALIZADA_COMPRAS))

                vlo_EntOttGestionCompra = New EntOttGestionCompra
                vlo_EntOttGestionCompra.IdUbicacion = pvn_IdUbicacion
                vlo_EntOttGestionCompra.IdViaCompraContrato = vlo_EntOtpParametroUbicacion.Valor
                vlo_EntOttGestionCompra.Anno = DateTime.Now.Year
                vlo_EntOttGestionCompra.NumeroGestion = vlo_DalOttGestionCompra.ObtenerFnOtNumeroGestionCompra(vlo_EntOttGestionCompra.Anno, vlo_EntOttGestionCompra.IdUbicacion, vlo_EntOttGestionCompra.IdViaCompraContrato) + 1
                vlo_EntOttGestionCompra.FechaRegistroSolicitud = DateTime.Now
                vlo_EntOttGestionCompra.Estado = EstadoGestionCompra.CREADA
                vlo_EntOttGestionCompra.Usuario = pvc_NombreUsuario
                vlo_EntOttGestionCompra.Observaciones = pvo_Observaciones

                vlo_DalOttGestionCompra.InsertarRegistro(vlo_EntOttGestionCompra)

                vlo_DsLineaGestionCompra = vlo_DalOttLineaGestionCompra.ListarRegistros("1 = 0", String.Empty, False, 0, 0)

                For Each vlo_FilaDetalle In pvo_DsDetalles.Tables(0).Rows
                    If vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO).ToString = "1" Then

                        vlo_DrFilaLineaGestionCompra = vlo_DsLineaGestionCompra.Tables(0).NewRow

                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION)) = vlo_EntOttGestionCompra.IdUbicacion
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO)) = vlo_EntOttGestionCompra.IdViaCompraContrato
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ANNO)) = vlo_EntOttGestionCompra.Anno
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION)) = vlo_EntOttGestionCompra.NumeroGestion
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_DETALLE_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_SOLICITADA)) = CType(vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA), Double)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.USUARIO)) = pvc_NombreUsuario

                        vlo_DsLineaGestionCompra.Tables(0).Rows.Add(vlo_DrFilaLineaGestionCompra)
                    End If
                Next

                vlo_DalOttLineaGestionCompra.AdapterOttLineaGestionCompra(vlo_DsLineaGestionCompra)

                vlo_DsLineas = vlo_DalOttLineaGestionCompra.ListarVOtLineaGcGroupUec(
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}",
                                  Modelo.V_OT_LINEA_GC_GROUP_UEC.ID_UBICACION, vlo_EntOttGestionCompra.IdUbicacion,
                                  Modelo.V_OT_LINEA_GC_GROUP_UEC.ID_VIA_COMPRA_CONTRATO, vlo_EntOttGestionCompra.IdViaCompraContrato,
                                  Modelo.V_OT_LINEA_GC_GROUP_UEC.ANNO, vlo_EntOttGestionCompra.Anno,
                                  Modelo.V_OT_LINEA_GC_GROUP_UEC.NUMERO_GESTION, vlo_EntOttGestionCompra.NumeroGestion), String.Empty, False, 0, 0)

                vlo_DsEstructuraGrupo = vlo_DalOttGrupoGestionCompra.ListarRegistros(String.Format("1 = 0"), String.Empty, False, 0, 0)

                For Each vlo_FilaLinea In vlo_DsLineas.Tables(0).Rows

                    vlo_DrFilaGrupo = vlo_DsEstructuraGrupo.Tables(0).NewRow

                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.ID_UBICACION)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_UEC.ID_UBICACION)
                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_UEC.ID_VIA_COMPRA_CONTRATO)
                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.ANNO)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_UEC.ANNO)
                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_GESTION)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_UEC.NUMERO_GESTION)
                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_LINEA)) = vln_NumeroLinea
                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.ID_MATERIAL)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_UEC.ID_MATERIAL)
                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.CANTIDAD_SOLICITADA)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_UEC.CANTIDAD_SOLICITADA)
                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.USUARIO)) = pvc_NombreUsuario

                    vlo_DsEstructuraGrupo.Tables(0).Rows.Add(vlo_DrFilaGrupo)

                    vln_NumeroLinea = vln_NumeroLinea + 1
                Next

                vlo_DalOttGrupoGestionCompra.AdapterGrupoGestionCompra(vlo_DsEstructuraGrupo)

                vlo_Conexion.TransaccionCommit()

                vlc_Resultado = String.Format("{0},{1},{2},{3}", vlo_EntOttGestionCompra.IdUbicacion, vlo_EntOttGestionCompra.IdViaCompraContrato, vlo_EntOttGestionCompra.Anno, vlo_EntOttGestionCompra.NumeroGestion)
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlc_Resultado
        End Function

        ''' <summary>
        ''' Permite agregar un registro en la tabla OTT_GESTION_COMPRA
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Mauricio Salas Chaves</author>
        ''' <creationDate>23/01/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarGestionCompraAprovisionamiento(pvo_DsDetalles As Data.DataSet, pvc_NombreUsuario As String, pvn_IdUbicacion As Integer, pvc_Observaciones As String, pvn_IdViaCompra As String, pvb_Finalizar As Boolean) As String
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAprovisionamiento As DalOttAprovisionamiento
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOttDetAprovisionamiento As DalOttDetAprovisionamiento
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vlo_EntOttAprovisionamiento As EntOttAprovisionamiento
            Dim vlo_DsLineaAprovisionamiento As Data.DataSet
            Dim vlc_Resultado As String = String.Empty
            Dim vlo_DrFilaLineaGestionCompra As Data.DataRow
            Dim vlo_EntOtlTrazabilGestionComp As EntOtlTrazabilGestionComp
            Dim vlo_DalOtlTrazabilGestionComp As DalOtlTrazabilGestionComp

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttAprovisionamiento = New DalOttAprovisionamiento(vlo_Conexion)
                vlo_DalOttDetAprovisionamiento = New DalOttDetAprovisionamiento(vlo_Conexion)
                vlo_DalOtlTrazabilGestionComp = New DalOtlTrazabilGestionComp(vlo_Conexion)
                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)

                vlo_EntOttAprovisionamiento = New EntOttAprovisionamiento
                vlo_EntOttAprovisionamiento.IdUbicacion = pvn_IdUbicacion
                If pvn_IdViaCompra <> String.Empty Then
                    vlo_EntOttAprovisionamiento.IdViaCompraContrato = CType(pvn_IdViaCompra, Integer)
                End If

                vlo_EntOttAprovisionamiento.Anno = DateTime.Now.Year
                vlo_EntOttAprovisionamiento.NumeroGestion = vlo_DalOttGestionCompra.ObtenerFnOtNumeroGestionCompra(vlo_EntOttAprovisionamiento.Anno, vlo_EntOttAprovisionamiento.IdUbicacion, vlo_EntOttAprovisionamiento.IdViaCompraContrato) + 1
                vlo_EntOttAprovisionamiento.FechaRegistroSolicitud = DateTime.Now
                If pvb_Finalizar Then
                    vlo_EntOttAprovisionamiento.Estado = EstadoAprovisionamiento.COMPLETADO
                Else
                    vlo_EntOttAprovisionamiento.Estado = EstadoAprovisionamiento.CREADO
                End If

                vlo_EntOttAprovisionamiento.Usuario = pvc_NombreUsuario

                If pvc_Observaciones <> String.Empty Then
                    vlo_EntOttAprovisionamiento.Observaciones = pvc_Observaciones
                End If

                vlo_DalOttAprovisionamiento.InsertarRegistro(vlo_EntOttAprovisionamiento)

                vlo_DsLineaAprovisionamiento = vlo_DalOttDetAprovisionamiento.ListarRegistros("1 = 0", String.Empty, False, 0, 0)

                For Each vlo_FilaDetalle In pvo_DsDetalles.Tables(0).Rows

                    vlo_DrFilaLineaGestionCompra = vlo_DsLineaAprovisionamiento.Tables(0).NewRow

                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.ID_UBICACION)) = vlo_EntOttAprovisionamiento.IdUbicacion

                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.ANNO)) = vlo_EntOttAprovisionamiento.Anno
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.NUMERO_GESTION)) = vlo_EntOttAprovisionamiento.NumeroGestion
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OTM_MATERIALLST.ID_MATERIAL)
                    If Not TypeOf vlo_FilaDetalle.Item("CANTIDAD") Is DBNull Then
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.CANTIDAD)) = CType(vlo_FilaDetalle("CANTIDAD"), Double)
                    End If

                    If Not TypeOf vlo_FilaDetalle.Item("OBSERVACIONES") Is DBNull Then
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.OBSERVACIONES)) = CType(vlo_FilaDetalle("OBSERVACIONES"), String)
                    End If
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.ESTADO)) = EstadoRegistro.EN_PROCESO_COMPRA
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.USUARIO)) = pvc_NombreUsuario

                    vlo_DsLineaAprovisionamiento.Tables(0).Rows.Add(vlo_DrFilaLineaGestionCompra)
                Next

                vlo_DalOttDetAprovisionamiento.AdapterOttDetAprovisionamiento(vlo_DsLineaAprovisionamiento)

                'Se ingresa el registro de trazabilidad
                'vlo_EntOtlTrazabilGestionComp = New EntOtlTrazabilGestionComp

                'With vlo_EntOtlTrazabilGestionComp
                '    .IdUbicacion = vlo_EntOttAprovisionamiento.IdUbicacion
                '    .IdViaCompraContrato = vlo_EntOttAprovisionamiento.IdViaCompraContrato
                '    .Anno = vlo_EntOttAprovisionamiento.Anno
                '    .NumeroGestion = vlo_EntOttAprovisionamiento.NumeroGestion
                '    .Estado = vlo_EntOttAprovisionamiento.Estado
                '    .Observaciones = vlo_EntOttAprovisionamiento.Observaciones
                '    .Usuario = vlo_EntOttAprovisionamiento.Usuario
                'End With

                'vlo_DalOtlTrazabilGestionComp.InsertarRegistro(vlo_EntOtlTrazabilGestionComp)

                vlo_Conexion.TransaccionCommit()

                vlc_Resultado = String.Format("{0},{1},{2},{3}", vlo_EntOttAprovisionamiento.IdUbicacion, vlo_EntOttAprovisionamiento.IdViaCompraContrato, vlo_EntOttAprovisionamiento.Anno, vlo_EntOttAprovisionamiento.NumeroGestion)

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

        ''' <summary>
        ''' Permite agregar un registro en la tabla OTT_GESTION_COMPRA
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Mauricio Salas Chaves</author>
        ''' <creationDate>09/09/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarGestionCompraAprovisionamiento(pvo_DsDetalles As Data.DataSet, pvo_EntOttAprovisionamiento As EntOttAprovisionamiento, pvc_Observaciones As String, pvc_NombreUsuario As String, pvn_IdViaCompra As Integer, pvb_Finalizar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAprovisionamiento As DalOttAprovisionamiento
            Dim vlo_DalOttDetAprovisionamiento As DalOttDetAprovisionamiento
            Dim vlo_DsDetAprovisionamiento As Data.DataSet
            Dim vln_Resultado As Integer = 0
            Dim vlo_DrFilaLineaGestionCompra As Data.DataRow
            Dim vlb_HuboCambio As Boolean = False
            Dim vlc_Condicion As String
            Dim vlc_DetRegistrado As String

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttAprovisionamiento = New DalOttAprovisionamiento(vlo_Conexion)
                vlo_DalOttDetAprovisionamiento = New DalOttDetAprovisionamiento(vlo_Conexion)

                If pvo_EntOttAprovisionamiento.Observaciones <> pvc_Observaciones Then
                    pvo_EntOttAprovisionamiento.Observaciones = pvc_Observaciones
                    vlb_HuboCambio = True
                End If

                If pvo_EntOttAprovisionamiento.IdViaCompraContrato <> pvn_IdViaCompra Then
                    pvo_EntOttAprovisionamiento.IdViaCompraContrato = CType(pvn_IdViaCompra, Integer)
                    vlb_HuboCambio = True
                End If


                If pvb_Finalizar Then
                    pvo_EntOttAprovisionamiento.Estado = EstadoAprovisionamiento.COMPLETADO
                    vlb_HuboCambio = True
                End If


                If vlb_HuboCambio Then
                    vlo_DalOttAprovisionamiento.ModificarRegistro(pvo_EntOttAprovisionamiento)
                End If

                vlc_Condicion = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}", Modelo.OTT_DET_APROVISIONAMIENTO.ID_UBICACION, pvo_EntOttAprovisionamiento.IdUbicacion, Modelo.OTT_DET_APROVISIONAMIENTO.NUMERO_GESTION, pvo_EntOttAprovisionamiento.NumeroGestion, Modelo.OTT_DET_APROVISIONAMIENTO.ANNO, pvo_EntOttAprovisionamiento.Anno)

                vlo_DsDetAprovisionamiento = vlo_DalOttDetAprovisionamiento.ListarRegistros(vlc_Condicion, String.Empty, False, 0, 0)

                vlc_DetRegistrado = String.Empty

                For Each vlo_FilaDetalle In vlo_DsDetAprovisionamiento.Tables(0).Rows
                    For Each vlo_FilaPantalla In pvo_DsDetalles.Tables(0).Rows
                        If vlo_FilaDetalle(Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL).ToString = vlo_FilaPantalla(Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL).ToString Then

                            If vlo_FilaDetalle(Modelo.OTT_DET_APROVISIONAMIENTO.CANTIDAD).ToString <> vlo_FilaPantalla("CANTIDAD").ToString Then
                                vlo_FilaDetalle(Modelo.OTT_DET_APROVISIONAMIENTO.CANTIDAD) = vlo_FilaPantalla("CANTIDAD")
                            End If

                            If vlo_FilaDetalle(Modelo.OTT_DET_APROVISIONAMIENTO.OBSERVACIONES).ToString <> vlo_FilaPantalla("OBSERVACIONES").ToString Then
                                vlo_FilaDetalle(Modelo.OTT_DET_APROVISIONAMIENTO.OBSERVACIONES) = vlo_FilaPantalla("OBSERVACIONES").ToString
                            End If
                        End If
                    Next
                    vlc_DetRegistrado = String.Format("{0}{1},", vlc_DetRegistrado, vlo_FilaDetalle(Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL).ToString)
                Next

                vlc_DetRegistrado = String.Format(",{0}", vlc_DetRegistrado)

                For Each vlo_FilaDetalle In pvo_DsDetalles.Tables(0).Rows
                    If vlc_DetRegistrado.Contains(vlo_FilaDetalle(Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL).ToString) = False Then
                        vlo_DrFilaLineaGestionCompra = vlo_DsDetAprovisionamiento.Tables(0).NewRow

                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.ID_UBICACION)) = pvo_EntOttAprovisionamiento.IdUbicacion

                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.ANNO)) = pvo_EntOttAprovisionamiento.Anno
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.NUMERO_GESTION)) = pvo_EntOttAprovisionamiento.NumeroGestion
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OTM_MATERIALLST.ID_MATERIAL)
                        If Not TypeOf vlo_FilaDetalle.Item("CANTIDAD") Is DBNull Then
                            vlo_DrFilaLineaGestionCompra.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.CANTIDAD)) = CType(vlo_FilaDetalle("CANTIDAD"), Double)
                        End If

                        If Not TypeOf vlo_FilaDetalle.Item("OBSERVACIONES") Is DBNull Then
                            vlo_DrFilaLineaGestionCompra.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.OBSERVACIONES)) = CType(vlo_FilaDetalle("OBSERVACIONES"), String)
                        End If
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.ESTADO)) = EstadoRegistro.EN_PROCESO_COMPRA
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsDetAprovisionamiento.Tables(0).Columns(Modelo.OTT_DET_APROVISIONAMIENTO.USUARIO)) = pvc_NombreUsuario

                        vlo_DsDetAprovisionamiento.Tables(0).Rows.Add(vlo_DrFilaLineaGestionCompra)
                    End If
                    
                Next

                'vlo_DsDetAprovisionamiento.AcceptChanges()

                vlo_DalOttDetAprovisionamiento.AdapterOttDetAprovisionamiento(vlo_DsDetAprovisionamiento)

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
        ''' Permite agregar un registro en la tabla OTT_GESTION_COMPRA
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Mauricio Salas Chaves</author>
        ''' <creationDate>07/09/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarGestionCompraSuministros(pvo_DsDetalles As Data.DataSet, pvc_NombreUsuario As String, pvn_IdUbicacion As Integer, pvc_Observaciones As String) As String
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vlo_EntOttGestionCompra As EntOttGestionCompra
            Dim vlo_DsLineaGestionCompra As Data.DataSet
            Dim vlo_EntOtpParametroUbicacion As EntOtpParametroUbicacion
            Dim vlo_DalOtpParametroUbicacion As DalOtpParametroUbicacion
            Dim vlc_Resultado As String = String.Empty
            Dim vln_NumeroLinea As Integer = 1
            Dim vlo_DrFilaLineaGestionCompra As Data.DataRow
            Dim vlo_EntOtlTrazabilGestionComp As EntOtlTrazabilGestionComp
            Dim vlo_DalOtlTrazabilGestionComp As DalOtlTrazabilGestionComp

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New DalOtpParametroUbicacion(vlo_Conexion)
                vlo_DalOtlTrazabilGestionComp = New DalOtlTrazabilGestionComp(vlo_Conexion)

                vlo_EntOtpParametroUbicacion = vlo_DalOtpParametroUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.VALOR_SECUENCIA_SUMINISTROS))

                vlo_EntOttGestionCompra = New EntOttGestionCompra
                vlo_EntOttGestionCompra.IdUbicacion = pvn_IdUbicacion
                vlo_EntOttGestionCompra.IdViaCompraContrato = vlo_EntOtpParametroUbicacion.Valor
                vlo_EntOttGestionCompra.Anno = DateTime.Now.Year
                vlo_EntOttGestionCompra.NumeroGestion = vlo_DalOttGestionCompra.ObtenerFnOtNumeroGestionCompra(vlo_EntOttGestionCompra.Anno, vlo_EntOttGestionCompra.IdUbicacion, vlo_EntOttGestionCompra.IdViaCompraContrato) + 1
                vlo_EntOttGestionCompra.FechaRegistroSolicitud = DateTime.Now
                vlo_EntOttGestionCompra.Estado = EstadoGestionCompra.CREADA
                vlo_EntOttGestionCompra.Usuario = pvc_NombreUsuario

                If pvc_Observaciones <> String.Empty Then
                    vlo_EntOttGestionCompra.Observaciones = pvc_Observaciones
                End If

                vlo_DalOttGestionCompra.InsertarRegistro(vlo_EntOttGestionCompra)

                vlo_DsLineaGestionCompra = vlo_DalOttLineaGestionCompra.ListarRegistros("1 = 0", String.Empty, False, 0, 0)

                For Each vlo_FilaDetalle In pvo_DsDetalles.Tables(0).Rows
                    vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ESTADO) = EstadoRegistro.EN_PROCESO_COMPRA

                    vlo_DrFilaLineaGestionCompra = vlo_DsLineaGestionCompra.Tables(0).NewRow

                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION)) = vlo_EntOttGestionCompra.IdUbicacion
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO)) = vlo_EntOttGestionCompra.IdViaCompraContrato
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ANNO)) = vlo_EntOttGestionCompra.Anno
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION)) = vlo_EntOttGestionCompra.NumeroGestion
                    'vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_LINEA)) = vln_NumeroLinea
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL)
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_DETALLE_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL)
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_SOLICITADA)) = CType(vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA), Double)
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.USUARIO)) = pvc_NombreUsuario

                    vlo_DsLineaGestionCompra.Tables(0).Rows.Add(vlo_DrFilaLineaGestionCompra)
                    vln_NumeroLinea = vln_NumeroLinea + 1
                Next

                vlo_DalOttDetalleMaterial.AdapterOttDetalleMaterialGestionCompra(pvo_DsDetalles)

                vlo_DalOttLineaGestionCompra.AdapterOttLineaGestionCompra(vlo_DsLineaGestionCompra)

                'Se ingresa el registro de trazabilidad
                With vlo_EntOtlTrazabilGestionComp
                    .IdUbicacion = vlo_EntOttGestionCompra.IdUbicacion
                    .IdViaCompraContrato = vlo_EntOttGestionCompra.IdViaCompraContrato
                    .Anno = vlo_EntOttGestionCompra.Anno
                    .NumeroGestion = vlo_EntOttGestionCompra.NumeroGestion
                    .Estado = vlo_EntOttGestionCompra.Estado
                    .Observaciones = vlo_EntOttGestionCompra.Observaciones
                    .Usuario = vlo_EntOttGestionCompra.Usuario
                End With

                vlo_DalOtlTrazabilGestionComp.InsertarRegistro(vlo_EntOtlTrazabilGestionComp)

                vlo_Conexion.TransaccionCommit()

                vlc_Resultado = String.Format("{0},{1},{2},{3}", vlo_EntOttGestionCompra.IdUbicacion, vlo_EntOttGestionCompra.IdViaCompraContrato, vlo_EntOttGestionCompra.Anno, vlo_EntOttGestionCompra.NumeroGestion)

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

        Public Function InsertarGestionCompraFinSuministros(pvo_DsDetalles As Data.DataSet, pvc_NombreUsuario As String, pvn_IdUbicacion As Integer, pvc_Observaciones As String) As String
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial
            Dim vlo_EntOttGestionCompra As EntOttGestionCompra
            Dim vlo_DsLineaGestionCompra As Data.DataSet
            Dim vlo_EntOtpParametroUbicacion As EntOtpParametroUbicacion
            Dim vlo_DalOtpParametroUbicacion As DalOtpParametroUbicacion
            Dim vlc_Resultado As String = String.Empty
            Dim vln_NumeroLinea As Integer = 1
            Dim vlo_DrFilaLineaGestionCompra As Data.DataRow
            Dim vlo_EntOtlTrazabilGestionComp As EntOtlTrazabilGestionComp
            Dim vlo_DalOtlTrazabilGestionComp As DalOtlTrazabilGestionComp

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New DalOtpParametroUbicacion(vlo_Conexion)
                vlo_DalOtlTrazabilGestionComp = New DalOtlTrazabilGestionComp(vlo_Conexion)

                vlo_EntOtpParametroUbicacion = vlo_DalOtpParametroUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.VALOR_SECUENCIA_SUMINISTROS))

                vlo_EntOttGestionCompra = New EntOttGestionCompra
                vlo_EntOttGestionCompra.IdUbicacion = pvn_IdUbicacion
                vlo_EntOttGestionCompra.IdViaCompraContrato = vlo_EntOtpParametroUbicacion.Valor
                vlo_EntOttGestionCompra.Anno = DateTime.Now.Year
                vlo_EntOttGestionCompra.NumeroGestion = vlo_DalOttGestionCompra.ObtenerFnOtNumeroGestionCompra(vlo_EntOttGestionCompra.Anno, vlo_EntOttGestionCompra.IdUbicacion, vlo_EntOttGestionCompra.IdViaCompraContrato) + 1
                vlo_EntOttGestionCompra.FechaRegistroSolicitud = DateTime.Now
                vlo_EntOttGestionCompra.Estado = EstadoGestionCompra.INGRESO_GESTION_GECO
                vlo_EntOttGestionCompra.Usuario = pvc_NombreUsuario

                If pvc_Observaciones <> String.Empty Then
                    vlo_EntOttGestionCompra.Observaciones = pvc_Observaciones
                End If

                vlo_DalOttGestionCompra.InsertarRegistro(vlo_EntOttGestionCompra)

                vlo_DsLineaGestionCompra = vlo_DalOttLineaGestionCompra.ListarRegistros("1 = 0", String.Empty, False, 0, 0)

                For Each vlo_FilaDetalle In pvo_DsDetalles.Tables(0).Rows
                    vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ESTADO) = EstadoRegistro.EN_PROCESO_COMPRA

                    vlo_DrFilaLineaGestionCompra = vlo_DsLineaGestionCompra.Tables(0).NewRow

                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION)) = vlo_EntOttGestionCompra.IdUbicacion
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO)) = vlo_EntOttGestionCompra.IdViaCompraContrato
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ANNO)) = vlo_EntOttGestionCompra.Anno
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION)) = vlo_EntOttGestionCompra.NumeroGestion
                    'vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_LINEA)) = vln_NumeroLinea
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL)
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_DETALLE_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL)
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_SOLICITADA)) = CType(vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA), Double)
                    vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompra.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.USUARIO)) = pvc_NombreUsuario

                    vlo_DsLineaGestionCompra.Tables(0).Rows.Add(vlo_DrFilaLineaGestionCompra)
                    vln_NumeroLinea = vln_NumeroLinea + 1
                Next

                vlo_DalOttDetalleMaterial.AdapterOttDetalleMaterialGestionCompra(pvo_DsDetalles)

                vlo_DalOttLineaGestionCompra.AdapterOttLineaGestionCompra(vlo_DsLineaGestionCompra)

                'Se ingresa el registro de trazabilidad
                With vlo_EntOtlTrazabilGestionComp
                    .IdUbicacion = vlo_EntOttGestionCompra.IdUbicacion
                    .IdViaCompraContrato = vlo_EntOttGestionCompra.IdViaCompraContrato
                    .Anno = vlo_EntOttGestionCompra.Anno
                    .NumeroGestion = vlo_EntOttGestionCompra.NumeroGestion
                    .Estado = vlo_EntOttGestionCompra.Estado
                    .Observaciones = vlo_EntOttGestionCompra.Observaciones
                    .Usuario = vlo_EntOttGestionCompra.Usuario
                End With

                vlo_DalOtlTrazabilGestionComp.InsertarRegistro(vlo_EntOtlTrazabilGestionComp)

                vlo_Conexion.TransaccionCommit()

                vlc_Resultado = String.Format("{0},{1},{2},{3}", vlo_EntOttGestionCompra.IdUbicacion, vlo_EntOttGestionCompra.IdViaCompraContrato, vlo_EntOttGestionCompra.Anno, vlo_EntOttGestionCompra.NumeroGestion)

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

        ''' <summary>
        ''' Permite agregar un registro en la tabla OTT_GESTION_COMPRA
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Mauricio Salas Chaves</author>
        ''' <creationDate>09/09/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarGestionCompraSuministros(pvo_DsDetalles As Data.DataSet, pvo_EntOttGestionCompra As EntOttGestionCompra, pvc_Observaciones As String, pvc_NombreUsuario As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DsLineaGestionCompraNuevos As Data.DataSet
            Dim vln_Resultado As Integer = 0
            Dim vlo_DrFilaLineaGestionCompra As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)

                If pvo_EntOttGestionCompra.Observaciones <> pvc_Observaciones Then
                    pvo_EntOttGestionCompra.Observaciones = pvc_Observaciones
                    vlo_DalOttGestionCompra.ModificarRegistro(pvo_EntOttGestionCompra)
                End If


                vlo_DsLineaGestionCompraNuevos = vlo_DalOttLineaGestionCompra.ListarRegistros("1 = 0", String.Empty, False, 0, 0)

                For Each vlo_FilaDetalle In pvo_DsDetalles.Tables(0).Rows
                    If vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.POSEE_GESTION_COMPRA).ToString = "0" Then

                        vlo_DrFilaLineaGestionCompra = vlo_DsLineaGestionCompraNuevos.Tables(0).NewRow

                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION)) = pvo_EntOttGestionCompra.IdUbicacion
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO)) = pvo_EntOttGestionCompra.IdViaCompraContrato
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ANNO)) = pvo_EntOttGestionCompra.Anno
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION)) = pvo_EntOttGestionCompra.NumeroGestion
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_DETALLE_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_SOLICITADA)) = CType(vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA), Double)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.USUARIO)) = pvc_NombreUsuario

                        vlo_DsLineaGestionCompraNuevos.Tables(0).Rows.Add(vlo_DrFilaLineaGestionCompra)
                    End If
                Next

                vlo_DalOttLineaGestionCompra.AdapterOttLineaGestionCompra(vlo_DsLineaGestionCompraNuevos)

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
        ''' Permite agregar un registro en la tabla OTT_GESTION_COMPRA
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>25/08/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarGestionCompraFondoTrabajo(pvo_DsDetalles As Data.DataSet, pvc_NombreUsuario As String, pvn_IdUbicacion As Integer, pvo_EntOttGestionCompra As EntOttGestionCompra) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DsLineaGestionCompraNuevos As Data.DataSet
            Dim vlo_DsLineaGestionCompraViejos As Data.DataSet
            Dim vln_Resultado As Integer = 0
            Dim vlo_DrFilaLineaGestionCompra As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)

                pvo_EntOttGestionCompra.Usuario = pvc_NombreUsuario

                vlo_DalOttGestionCompra.ModificarRegistro(pvo_EntOttGestionCompra)

                vlo_DsLineaGestionCompraNuevos = vlo_DalOttLineaGestionCompra.ListarRegistros("1 = 0", String.Empty, False, 0, 0)
                vlo_DsLineaGestionCompraViejos = vlo_DalOttLineaGestionCompra.ListarRegistros(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION, pvo_EntOttGestionCompra.IdUbicacion, Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvo_EntOttGestionCompra.IdViaCompraContrato, Modelo.OTT_LINEA_GESTION_COMPRA.ANNO, pvo_EntOttGestionCompra.Anno, Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION, pvo_EntOttGestionCompra.NumeroGestion), String.Empty, False, 0, 0)

                For Each vlo_FilaDetalle In pvo_DsDetalles.Tables(0).Rows
                    If vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO).ToString = "1" Then

                        vlo_DrFilaLineaGestionCompra = vlo_DsLineaGestionCompraNuevos.Tables(0).NewRow

                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION)) = pvo_EntOttGestionCompra.IdUbicacion
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO)) = pvo_EntOttGestionCompra.IdViaCompraContrato
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ANNO)) = pvo_EntOttGestionCompra.Anno
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION)) = pvo_EntOttGestionCompra.NumeroGestion
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_DETALLE_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_SOLICITADA)) = CType(vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA), Double)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.USUARIO)) = pvc_NombreUsuario

                        vlo_DsLineaGestionCompraNuevos.Tables(0).Rows.Add(vlo_DrFilaLineaGestionCompra)
                    End If
                Next

                For Each vlo_FilaVieja In vlo_DsLineaGestionCompraViejos.Tables(0).Rows
                    vlo_FilaVieja.Delete()
                Next

                vlo_DalOttLineaGestionCompra.AdapterOttLineaGestionCompra(vlo_DsLineaGestionCompraViejos)
                vlo_DalOttLineaGestionCompra.AdapterOttLineaGestionCompra(vlo_DsLineaGestionCompraNuevos)

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
        ''' Permite agregar un registro en la tabla OTT_GESTION_COMPRA
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>07/10/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarGestionCompraUnidadEspecializada(pvo_DsDetalles As Data.DataSet, pvc_NombreUsuario As String, pvn_IdUbicacion As Integer, pvo_EntOttGestionCompra As EntOttGestionCompra) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DalOttGrupoGestionCompra As DalOttGrupoGestionCompra
            Dim vlo_DsLineaGestionCompraNuevos As Data.DataSet
            Dim vlo_DsLineaGestionCompraViejos As Data.DataSet
            Dim vlo_DsGrupoGestionCompraNuevos As Data.DataSet
            Dim vlo_DsGrupoGestionCompraViejos As Data.DataSet
            Dim vln_Resultado As Integer = 0
            Dim vlo_DrFilaLineaGestionCompra As Data.DataRow
            Dim vlo_DrFilaGrupo As Data.DataRow
            Dim vln_NumeroLinea As Integer = 1
            Dim vlo_DsLineas As Data.DataSet

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOttGrupoGestionCompra = New DalOttGrupoGestionCompra(vlo_Conexion)

                pvo_EntOttGestionCompra.Usuario = pvc_NombreUsuario

                vlo_DalOttGestionCompra.ModificarRegistro(pvo_EntOttGestionCompra)

                vlo_DsGrupoGestionCompraNuevos = vlo_DalOttGrupoGestionCompra.ListarRegistros("1 = 0", String.Empty, False, 0, 0)
                vlo_DsGrupoGestionCompraViejos = vlo_DalOttGrupoGestionCompra.ListarRegistros(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION, pvo_EntOttGestionCompra.IdUbicacion, Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvo_EntOttGestionCompra.IdViaCompraContrato, Modelo.OTT_LINEA_GESTION_COMPRA.ANNO, pvo_EntOttGestionCompra.Anno, Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION, pvo_EntOttGestionCompra.NumeroGestion), String.Empty, False, 0, 0)
                vlo_DsLineaGestionCompraNuevos = vlo_DalOttLineaGestionCompra.ListarRegistros("1 = 0", String.Empty, False, 0, 0)
                vlo_DsLineaGestionCompraViejos = vlo_DalOttLineaGestionCompra.ListarRegistros(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION, pvo_EntOttGestionCompra.IdUbicacion, Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvo_EntOttGestionCompra.IdViaCompraContrato, Modelo.OTT_LINEA_GESTION_COMPRA.ANNO, pvo_EntOttGestionCompra.Anno, Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION, pvo_EntOttGestionCompra.NumeroGestion), String.Empty, False, 0, 0)

                For Each vlo_FilaDetalle In pvo_DsDetalles.Tables(0).Rows
                    If vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.SELECCIONADO).ToString = "1" Then

                        vlo_DrFilaLineaGestionCompra = vlo_DsLineaGestionCompraNuevos.Tables(0).NewRow

                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION)) = pvo_EntOttGestionCompra.IdUbicacion
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO)) = pvo_EntOttGestionCompra.IdViaCompraContrato
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ANNO)) = pvo_EntOttGestionCompra.Anno
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION)) = pvo_EntOttGestionCompra.NumeroGestion
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_MATERIAL)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.ID_DETALLE_MATERIAL)) = vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.CANTIDAD_SOLICITADA)) = CType(vlo_FilaDetalle(Modelo.V_OT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA), Double)
                        vlo_DrFilaLineaGestionCompra.Item(vlo_DsLineaGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_LINEA_GESTION_COMPRA.USUARIO)) = pvc_NombreUsuario

                        vlo_DsLineaGestionCompraNuevos.Tables(0).Rows.Add(vlo_DrFilaLineaGestionCompra)
                    End If
                Next

                For Each vlo_FilaViejaGrupo In vlo_DsGrupoGestionCompraViejos.Tables(0).Rows
                    vlo_FilaViejaGrupo.Delete()
                Next

                For Each vlo_FilaVieja In vlo_DsLineaGestionCompraViejos.Tables(0).Rows
                    vlo_FilaVieja.Delete()
                Next

                vlo_DalOttGrupoGestionCompra.AdapterGrupoGestionCompra(vlo_DsGrupoGestionCompraViejos)
                vlo_DalOttLineaGestionCompra.AdapterOttLineaGestionCompra(vlo_DsLineaGestionCompraViejos)
                vlo_DalOttLineaGestionCompra.AdapterOttLineaGestionCompra(vlo_DsLineaGestionCompraNuevos)

                vlo_DsLineas = vlo_DalOttLineaGestionCompra.ListarVOtLineaGcGroupUec(
                  String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}",
                  Modelo.V_OT_LINEA_GC_GROUP_UEC.ID_UBICACION, pvo_EntOttGestionCompra.IdUbicacion,
                  Modelo.V_OT_LINEA_GC_GROUP_UEC.ID_VIA_COMPRA_CONTRATO, pvo_EntOttGestionCompra.IdViaCompraContrato,
                  Modelo.V_OT_LINEA_GC_GROUP_UEC.ANNO, pvo_EntOttGestionCompra.Anno,
                  Modelo.V_OT_LINEA_GC_GROUP_UEC.NUMERO_GESTION, pvo_EntOttGestionCompra.NumeroGestion), String.Empty, False, 0, 0)

                For Each vlo_FilaLinea In vlo_DsLineas.Tables(0).Rows

                    vlo_DrFilaGrupo = vlo_DsGrupoGestionCompraNuevos.Tables(0).NewRow

                    vlo_DrFilaGrupo.Item(vlo_DsGrupoGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.ID_UBICACION)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_UEC.ID_UBICACION)
                    vlo_DrFilaGrupo.Item(vlo_DsGrupoGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_UEC.ID_VIA_COMPRA_CONTRATO)
                    vlo_DrFilaGrupo.Item(vlo_DsGrupoGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.ANNO)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_UEC.ANNO)
                    vlo_DrFilaGrupo.Item(vlo_DsGrupoGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_GESTION)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_UEC.NUMERO_GESTION)
                    vlo_DrFilaGrupo.Item(vlo_DsGrupoGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_LINEA)) = vln_NumeroLinea
                    vlo_DrFilaGrupo.Item(vlo_DsGrupoGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.ID_MATERIAL)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_UEC.ID_MATERIAL)
                    vlo_DrFilaGrupo.Item(vlo_DsGrupoGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.CANTIDAD_SOLICITADA)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_UEC.CANTIDAD_SOLICITADA)
                    vlo_DrFilaGrupo.Item(vlo_DsGrupoGestionCompraNuevos.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.USUARIO)) = pvc_NombreUsuario

                    vlo_DsGrupoGestionCompraNuevos.Tables(0).Rows.Add(vlo_DrFilaGrupo)

                    vln_NumeroLinea = vln_NumeroLinea + 1
                Next

                vlo_DalOttGrupoGestionCompra.AdapterGrupoGestionCompra(vlo_DsGrupoGestionCompraNuevos)

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
        ''' Permite agregar un registro en la tabla OTT_GESTION_COMPRA
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>29/08/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarGestionAgregarGrupos(pvo_EntOttGestionCompra As EntOttGestionCompra) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DalOttGrupoGestionCompra As DalOttGrupoGestionCompra
            Dim vln_Resultado As Integer = 0
            Dim vlo_DsLineas As Data.DataSet
            Dim vlo_DsEstructuraGrupo As Data.DataSet
            Dim vlo_DrFilaGrupo As Data.DataRow
            Dim vln_NumeroLinea As Integer = 1

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOttGrupoGestionCompra = New DalOttGrupoGestionCompra(vlo_Conexion)

                vlo_DalOttGestionCompra.ModificarRegistro(pvo_EntOttGestionCompra)

                vlo_DsLineas = vlo_DalOttLineaGestionCompra.ListarVOtLineaGcGroupFondo(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_UBICACION, pvo_EntOttGestionCompra.IdUbicacion, Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_VIA_COMPRA_CONTRATO, pvo_EntOttGestionCompra.IdViaCompraContrato, Modelo.V_OT_LINEA_GC_GROUP_FONDO.ANNO, pvo_EntOttGestionCompra.Anno, Modelo.V_OT_LINEA_GC_GROUP_FONDO.NUMERO_GESTION, pvo_EntOttGestionCompra.NumeroGestion), String.Empty, False, 0, 0)
                vlo_DsEstructuraGrupo = vlo_DalOttGrupoGestionCompra.ListarRegistros(String.Format("1 = 0"), String.Empty, False, 0, 0)

                For Each vlo_FilaLinea In vlo_DsLineas.Tables(0).Rows

                    vlo_DrFilaGrupo = vlo_DsEstructuraGrupo.Tables(0).NewRow

                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.ID_UBICACION)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_UBICACION)
                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_VIA_COMPRA_CONTRATO)
                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.ANNO)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_FONDO.ANNO)
                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_GESTION)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_FONDO.NUMERO_GESTION)
                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_LINEA)) = vln_NumeroLinea
                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.ID_MATERIAL)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_MATERIAL)
                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.CANTIDAD_SOLICITADA)) = vlo_FilaLinea(Modelo.V_OT_LINEA_GC_GROUP_FONDO.CANTIDAD_SOLICITADA)
                    vlo_DrFilaGrupo.Item(vlo_DsEstructuraGrupo.Tables(0).Columns(Modelo.OTT_GRUPO_GESTION_COMPRA.USUARIO)) = pvo_EntOttGestionCompra.Usuario

                    vlo_DsEstructuraGrupo.Tables(0).Rows.Add(vlo_DrFilaGrupo)

                    vln_NumeroLinea = vln_NumeroLinea + 1
                Next

                vlo_DalOttGrupoGestionCompra.AdapterGrupoGestionCompra(vlo_DsEstructuraGrupo)

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
        ''' Permite agregar un registro en la tabla OTT_GESTION_COMPRA
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>10/10/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarGestionCompraUnidadEspecializadaEstado(pvo_EntOttGestionCompra As EntOttGestionCompra) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DalOttGrupoGestionCompra As DalOttGrupoGestionCompra
            Dim vln_Resultado As Integer = 0
            Dim vlo_DsLineas As Data.DataSet
            Dim vlo_DsEstructuraGrupo As Data.DataSet
            Dim vlo_DrFilaGrupo As Data.DataRow
            Dim vln_NumeroLinea As Integer = 1

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOttGrupoGestionCompra = New DalOttGrupoGestionCompra(vlo_Conexion)

                vlo_DalOttGestionCompra.ModificarRegistro(pvo_EntOttGestionCompra)

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
        ''' Permite borrar un registro en la tabla OTT_GESTION_COMPRA, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOttGestionCompra) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdUbicacion, pvo_Registro.IdViaCompraContrato, pvo_Registro.Anno, pvo_Registro.NumeroGestion) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Hay registro asociados")
                End If

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vln_Resultado = vlo_DalOttGestionCompra.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvn_Anno">Año</param>
        ''' <param name="pvn_NumeroGestion">Consecutivo de la gestión. es anual, por vía de compra y ubicación.</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_Anno As Integer, pvn_NumeroGestion As Integer) As EntOttGestionCompra
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                Return vlo_DalOttGestionCompra.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_GESTION_COMPRA.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_GESTION_COMPRA.ANNO, pvn_Anno, Modelo.OTT_GESTION_COMPRA.NUMERO_GESTION, pvn_NumeroGestion))
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
        ''' <param name="pvn_Anno">Año</param>
        ''' <param name="pvn_NumeroGestion">Consecutivo de la gestión. es anual, por vía de compra y ubicación.</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_Anno As Integer, pvn_NumeroGestion As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DalOtlTrazabilGestionComp As DalOtlTrazabilGestionComp

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

                'Determinar la existencia de registros asociados en la tabla OTT_LINEA_GESTION_COMPRA
                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                If vlo_DalOttLineaGestionCompra.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_LINEA_GESTION_COMPRA.ANNO, pvn_Anno, Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION, pvn_NumeroGestion)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTL_TRAZABIL_GESTION_COMP
                vlo_DalOtlTrazabilGestionComp = New DalOtlTrazabilGestionComp(vlo_Conexion)
                If vlo_DalOtlTrazabilGestionComp.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTL_TRAZABIL_GESTION_COMP.ID_UBICACION, pvn_IdUbicacion, Modelo.OTL_TRAZABIL_GESTION_COMP.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTL_TRAZABIL_GESTION_COMP.ANNO, pvn_Anno, Modelo.OTL_TRAZABIL_GESTION_COMP.NUMERO_GESTION, pvn_NumeroGestion)).Existe Then
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

        Public Function FondoTrabajoAprobSupervisor(ByVal pvo_Registro As EntOttGestionCompra, ByVal pvo_Trazabilidad As EntOtlTrazabilGestionComp, ByVal pvc_IdentificacionAdjudicado As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOtlTrazabilGestionComp As DalOtlTrazabilGestionComp
            Dim vlo_DalOttProveedorCotizacion As DalOttProveedorCotizacion
            Dim vlo_EntOttProveedorCotizacion As EntOttProveedorCotizacion
            Dim vln_Resultado As Integer
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

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOtlTrazabilGestionComp = New DalOtlTrazabilGestionComp(vlo_Conexion)
                vlo_DalOttProveedorCotizacion = New DalOttProveedorCotizacion(vlo_Conexion)

                'Se cambia el indicador de adjudicado para el proveedor seleccionado
                vlc_Condicion = String.Format("{0} = '{1}' AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_PROVEEDOR_COTIZACION.IDENTIFICACION, pvc_IdentificacionAdjudicado, Modelo.OTT_PROVEEDOR_COTIZACION.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTT_PROVEEDOR_COTIZACION.ID_VIA_COMPRA_CONTRATO, pvo_Registro.IdViaCompraContrato, Modelo.OTT_PROVEEDOR_COTIZACION.ANNO, pvo_Registro.Anno, Modelo.OTT_PROVEEDOR_COTIZACION.NUMERO_GESTION, pvo_Registro.NumeroGestion)
                vlo_EntOttProveedorCotizacion = vlo_DalOttProveedorCotizacion.ObtenerRegistro(vlc_Condicion)

                vlo_EntOttProveedorCotizacion.Adjudicado = eBoolean.Verdadero
                vlo_DalOttProveedorCotizacion.ModificarRegistro(vlo_EntOttProveedorCotizacion)

                vln_Resultado = vlo_DalOttGestionCompra.ModificarRegistro(pvo_Registro)

                vlo_DalOtlTrazabilGestionComp.InsertarRegistro(pvo_Trazabilidad)

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

        Public Function FondoTrabajoAprobacion(ByVal pvo_Registro As EntOttGestionCompra, ByVal pvo_Trazabilidad As EntOtlTrazabilGestionComp) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOtlTrazabilGestionComp As DalOtlTrazabilGestionComp
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

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOtlTrazabilGestionComp = New DalOtlTrazabilGestionComp(vlo_Conexion)

                vln_Resultado = vlo_DalOttGestionCompra.ModificarRegistro(pvo_Registro)

                vlo_DalOtlTrazabilGestionComp.InsertarRegistro(pvo_Trazabilidad)

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

        Public Function FondoTrabajoAprobacionJefatura(ByVal pvo_Registro As EntOttGestionCompra, ByVal pvo_Trazabilidad As EntOtlTrazabilGestionComp, ByVal pvb_EnviarCorreo As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOtlTrazabilGestionComp As DalOtlTrazabilGestionComp
            Dim vln_Resultado As Integer
            Dim vlc_NombreProveedor As String

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOtlTrazabilGestionComp = New DalOtlTrazabilGestionComp(vlo_Conexion)

                vln_Resultado = vlo_DalOttGestionCompra.ModificarRegistro(pvo_Registro)

                vlo_DalOtlTrazabilGestionComp.InsertarRegistro(pvo_Trazabilidad)

                vlo_Conexion.TransaccionCommit()

                If pvb_EnviarCorreo = True Then
                    NotificarProveedorFondoTrabajoAprobJefatura(pvo_Registro)
                End If

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

        Public Function UnidadEspAprobSupervisor(ByVal pvo_Registro As EntOttGestionCompra, ByVal pvo_Trazabilidad As EntOtlTrazabilGestionComp) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOtlTrazabilGestionComp As DalOtlTrazabilGestionComp
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

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOtlTrazabilGestionComp = New DalOtlTrazabilGestionComp(vlo_Conexion)

                vln_Resultado = vlo_DalOttGestionCompra.ModificarRegistro(pvo_Registro)

                vlo_DalOtlTrazabilGestionComp.InsertarRegistro(pvo_Trazabilidad)

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

        Public Function CorregirCaracteresConAcento(pvn_CuerpoCorreo As String) As String

            pvn_CuerpoCorreo = pvn_CuerpoCorreo.Replace("á", "&aacute;")
            pvn_CuerpoCorreo = pvn_CuerpoCorreo.Replace("é", "&eacute;")
            pvn_CuerpoCorreo = pvn_CuerpoCorreo.Replace("í", "&iacute;")
            pvn_CuerpoCorreo = pvn_CuerpoCorreo.Replace("ó", "&oacute;")
            pvn_CuerpoCorreo = pvn_CuerpoCorreo.Replace("ú", "&uacute;")

            pvn_CuerpoCorreo = pvn_CuerpoCorreo.Replace("Á", "&Aacute;")
            pvn_CuerpoCorreo = pvn_CuerpoCorreo.Replace("É", "&Eacute;")
            pvn_CuerpoCorreo = pvn_CuerpoCorreo.Replace("Í", "&Iacute;")
            pvn_CuerpoCorreo = pvn_CuerpoCorreo.Replace("Ó", "&Oacute;")
            pvn_CuerpoCorreo = pvn_CuerpoCorreo.Replace("Ú", "&Uacute;")

            pvn_CuerpoCorreo = pvn_CuerpoCorreo.Replace("ñ", "&ntilde;")

            pvn_CuerpoCorreo = pvn_CuerpoCorreo.Replace(ChrW(8220), "&ldquo;")
            pvn_CuerpoCorreo = pvn_CuerpoCorreo.Replace(ChrW(8221), "&rdquo;")

            Return pvn_CuerpoCorreo
        End Function

        Private Sub EnviarCorreo(ByVal pvc_SubjectCorreo As String, ByVal pvc_Mensaje As String, ByVal pvo_ListaDestinatarios As List(Of EntGNT_DESTINATARIO))
            Dim vlo_WsGestorNotificaciones As wsGestorNotificaciones
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_EntGnmSistema As EntGNM_SISTEMA
            Dim vlo_EntGntNotificacion As EntGNT_NOTIFICACION
            Dim vlo_ListaAdjuntos As List(Of EntGNT_ARCHIVO_ADJUNTO)
            Dim vlc_Condicion As String

            Try

                vlo_WsGestorNotificaciones = New wsGestorNotificaciones
                vlo_WsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
                vlo_WsGestorNotificaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_GESTOR_NOTIFICACIONES)
                vlo_WsGestorNotificaciones.Timeout = -1

                vlc_Condicion = String.Format("NOMBRE_SISTEMA = '{0}'", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_GN))
                vlo_EntGnmSistema = vlo_WsGestorNotificaciones.GNM_SISTEMA_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    vlc_Condicion)

                vlo_EntGntNotificacion = New EntGNT_NOTIFICACION

                With vlo_EntGntNotificacion
                    .ID_SISTEMA = vlo_EntGnmSistema.ID_SISTEMA
                    .ASUNTO = pvc_SubjectCorreo
                    .CUERPO = pvc_Mensaje
                    .ES_HTML = 1
                    .FECHA_CREA = Date.Now
                    .USUARIO_CREA = "SISTEMA"
                End With

                vlo_ListaAdjuntos = New List(Of EntGNT_ARCHIVO_ADJUNTO)

                vlo_WsGestorNotificaciones.GNT_NOTIFICACION_Registrar(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                                                                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN), _
                                                                        vlo_EntGnmSistema, vlo_EntGntNotificacion, vlo_ListaAdjuntos.ToArray, pvo_ListaDestinatarios.ToArray)

            Catch vlo_Exc As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Exc, Utilerias.OrdenesDeTrabajo.Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
                vlo_WsGestorNotificaciones.Dispose()
            End Try
        End Sub

        Public Sub EnviarMultiplesCorreo(ByVal pvo_DsSolicitudes As DataSet)
            Dim vlo_WsGestorNotificaciones As wsGestorNotificaciones
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_EntGnmSistema As EntGNM_SISTEMA
            Dim vlc_Condicion As String



            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vgc_CadenaConexion)
                    vgo_Conexion = vlo_Conexion
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If



                vlo_WsGestorNotificaciones = New wsGestorNotificaciones
                vlo_WsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
                vlo_WsGestorNotificaciones.Url = System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_GESTOR_NOTIFICACIONES)
                vlo_WsGestorNotificaciones.Timeout = -1

                vlc_Condicion = String.Format("NOMBRE_SISTEMA = '{0}'", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_GN))

                vlo_EntGnmSistema = vlo_WsGestorNotificaciones.GNM_SISTEMA_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    vlc_Condicion)

                vlo_WsGestorNotificaciones.GNT_NOTIFICACION_RegistrarNotificaciones(ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                                                                                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                                                                                    vlo_EntGnmSistema.NOMBRE_SISTEMA, pvo_DsSolicitudes)

            Catch vlo_Exc As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Exc, Utilerias.OrdenesDeTrabajo.Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
                vlo_WsGestorNotificaciones.Dispose()
            End Try

        End Sub

#End Region

    End Class
End Namespace
