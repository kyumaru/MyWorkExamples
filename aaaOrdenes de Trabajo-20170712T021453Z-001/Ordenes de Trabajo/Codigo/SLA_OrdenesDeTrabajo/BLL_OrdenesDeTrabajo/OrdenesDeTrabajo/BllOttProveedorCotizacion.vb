Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports ORDENES_TRABAJO.AccesoDatos.Catalogos
Imports ORDENES_TRABAJO.EntidadNegocio.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Wsr_SDP_ReportServer
Imports System.Configuration
Imports WsrGestorNotificaciones

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOttProveedorCotizacion
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
		''' Permite agregar un registro en la tabla OTT_PROVEEDOR_COTIZACION, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOttProveedorCotizacion) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttProveedorCotizacion As DalOttProveedorCotizacion
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.Identificacion, pvo_Registro.IdUbicacion, pvo_Registro.IdViaCompraContrato, pvo_Registro.Anno, pvo_Registro.NumeroGestion).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
				End If

				vlo_DalOttProveedorCotizacion = New DalOttProveedorCotizacion(vlo_Conexion)
				vln_Resultado = vlo_DalOttProveedorCotizacion.InsertarRegistro(pvo_Registro)
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
        ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
        ''' </summary>
        ''' <param name="pvc_idPersonal"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>05/09/2016</creationDate>
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
                    String.Format("ID_PERSONAL = '{0}'", pvc_idPersonal))
            Catch ex As Exception
                Throw
            Finally
                If vlo_WsEU_Curriculo IsNot Nothing Then
                    vlo_WsEU_Curriculo.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="pvc_IdentificacionUsuario"></param>
        ''' <param name="pvo_EntOttGestionCompra"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>02/09/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function SolicitudCotizacion(pvc_Usuario As String, pvc_Clave As String, pvc_IdentificacionUsuario As String, pvo_EntOttGestionCompra As EntOttGestionCompra) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vln_Resultado As Integer = New Integer - 1
            Dim vlo_DalOttProveedorCotizacion As DalOttProveedorCotizacion
            Dim vlo_DalOtpParametroUbicacion As DalOtpParametroUbicacion
            Dim vlo_EntOtpParametroUbicacionTelefonos As EntOtpParametroUbicacion
            Dim vlo_EntOtpParametroUbicacionCorreos As EntOtpParametroUbicacion
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DsProveedorCotizacion As Data.DataSet
            Dim vlo_Ws_SDP_ReportServer As Ws_SDP_ReportServer
            Dim vlo_EntReporte As EntReporte
            Dim vlo_ListaEntParametroReporte As New List(Of EntParametroReporte)
            Dim vlo_EntParametroReporte As EntParametroReporte
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
            Dim vlo_DalOtmCorreoProveedor As DalOtmCorreoProveedor
            Dim vlo_DsCorreosProveedor As Data.DataSet
            Dim vlo_wsGestorNotificaciones As wsGestorNotificaciones
            Dim vlo_EntSistema As EntGNM_SISTEMA
            Dim vlo_ListaDestinatarios As List(Of EntGNT_DESTINATARIO)
            Dim vlo_ListaAdjuntos As New List(Of EntGNT_ARCHIVO_ADJUNTO)
            Dim vlo_AdjuntoCorreo As WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO
            Dim vlo_EntCorreo As EntGNT_DESTINATARIO
            Dim vlo_EntNotificacion As EntGNT_NOTIFICACION
            Dim vlo_DalOtmProveedor As DalOtmProveedor
            Dim vlo_EntOtmProveedor As EntOtmProveedor
            Dim vln_IdNotificacion As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Ws_SDP_ReportServer = New Ws_SDP_ReportServer
                vlo_Ws_SDP_ReportServer.Timeout = -1
                vlo_Ws_SDP_ReportServer.Credentials = System.Net.CredentialCache.DefaultCredentials
                vlo_Ws_SDP_ReportServer.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_SDP_REPORT_SERVER)

                vlo_wsGestorNotificaciones = New wsGestorNotificaciones
                vlo_wsGestorNotificaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_GESTOR_NOTIFICACIONES)
                vlo_wsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
                vlo_wsGestorNotificaciones.Timeout = -1

                vlo_Conexion.TransaccionBegin()

                vlo_EntEmpleados = CargarFuncionario(pvc_IdentificacionUsuario)

                vlo_DalOttProveedorCotizacion = New DalOttProveedorCotizacion(vlo_Conexion)
                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New DalOtpParametroUbicacion(vlo_Conexion)
                vlo_DalOtmCorreoProveedor = New DalOtmCorreoProveedor(vlo_Conexion)
                vlo_DalOtmProveedor = New DalOtmProveedor(vlo_Conexion)

                vlo_EntOtpParametroUbicacionTelefonos = vlo_DalOtpParametroUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_TELEFONO_COTIZACION_FONDO_TRABAJO))
                vlo_EntOtpParametroUbicacionCorreos = vlo_DalOtpParametroUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_COTIZACION_FONDO_TRABAJO))

                vlo_DsProveedorCotizacion = vlo_DalOttProveedorCotizacion.ListarRegistros(
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = '{9}'",
                                  Modelo.OTT_PROVEEDOR_COTIZACION.ID_UBICACION, pvo_EntOttGestionCompra.IdUbicacion,
                                  Modelo.OTT_PROVEEDOR_COTIZACION.ID_VIA_COMPRA_CONTRATO, pvo_EntOttGestionCompra.IdViaCompraContrato,
                                  Modelo.OTT_PROVEEDOR_COTIZACION.ANNO, pvo_EntOttGestionCompra.Anno,
                                  Modelo.OTT_PROVEEDOR_COTIZACION.NUMERO_GESTION, pvo_EntOttGestionCompra.NumeroGestion,
                                  Modelo.OTT_PROVEEDOR_COTIZACION.ESTADO, EstadoProveedorCotizacion.PENDIENTE_DE_ENVIO),
                    String.Empty, False, 0, 0)

                vlo_EntSistema = vlo_wsGestorNotificaciones.GNM_SISTEMA_ObtenerPorNombre(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_GN))

                For Each vlo_FilaProveedorCotizacion In vlo_DsProveedorCotizacion.Tables(0).Rows
                    vlo_ListaEntParametroReporte = New List(Of EntParametroReporte)
                    'configuracion de los reporte
                    vlo_EntParametroReporte = New EntParametroReporte
                    vlo_EntParametroReporte.Nombre = "pvc_Usuario"
                    vlo_EntParametroReporte.Valor = pvc_Usuario
                    vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                    'configuracion de los reporte
                    vlo_EntParametroReporte = New EntParametroReporte
                    vlo_EntParametroReporte.Nombre = "pvc_Clave"
                    vlo_EntParametroReporte.Valor = pvc_Clave
                    vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                    'configuracion de los reporte
                    vlo_EntParametroReporte = New EntParametroReporte
                    vlo_EntParametroReporte.Nombre = "pvc_Condicion"
                    vlo_EntParametroReporte.Valor = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}",
                                  Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION, pvo_EntOttGestionCompra.IdUbicacion,
                                  Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvo_EntOttGestionCompra.IdViaCompraContrato,
                                  Modelo.OTT_LINEA_GESTION_COMPRA.ANNO, pvo_EntOttGestionCompra.Anno,
                                  Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION, pvo_EntOttGestionCompra.NumeroGestion)
                    vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                    'configuracion de los reporte
                    vlo_EntParametroReporte = New EntParametroReporte
                    vlo_EntParametroReporte.Nombre = "pvc_UsuarioEjecuta"
                    vlo_EntParametroReporte.Valor = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                    vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                    'configuracion de los reporte
                    vlo_EntParametroReporte = New EntParametroReporte
                    vlo_EntParametroReporte.Nombre = "pvn_IdCorreo"
                    vlo_EntParametroReporte.Valor = vlo_EntOtpParametroUbicacionCorreos.Valor
                    vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                    'configuracion de los reporte
                    vlo_EntParametroReporte = New EntParametroReporte
                    vlo_EntParametroReporte.Nombre = "pvn_IdTelefono"
                    vlo_EntParametroReporte.Valor = vlo_EntOtpParametroUbicacionTelefonos.Valor
                    vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                    'configuracion de los reporte
                    vlo_EntParametroReporte = New EntParametroReporte
                    vlo_EntParametroReporte.Nombre = "pvc_NumeroOT"
                    vlo_EntParametroReporte.Valor = String.Format("{0}-{1}", pvo_EntOttGestionCompra.Anno.ToString.Substring(2, 2), pvo_EntOttGestionCompra.NumeroGestion)
                    vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                    vlo_EntReporte = vlo_Ws_SDP_ReportServer.GenerarReporte(pvc_Usuario, pvc_Clave, "PDF", Utilerias.OrdenesDeTrabajo.Reportes.RUTA_BASE, Utilerias.OrdenesDeTrabajo.Reportes.RPT_OT_SOLICITUD_COTIZACION_MATERIALES_AL_PROVEEDOR, vlo_ListaEntParametroReporte.ToArray)

                    vlo_DsCorreosProveedor = vlo_DalOtmCorreoProveedor.ListarRegistros(
                        String.Format("{0} = '{1}'", Modelo.OTM_CORREO_PROVEEDOR.IDENTIFICACION, vlo_FilaProveedorCotizacion(Modelo.V_OTT_PROVEEDOR_COTIZACION.IDENTIFICACION).ToString),
                        String.Empty, False, 0, 0)

                    vlo_ListaDestinatarios = New List(Of EntGNT_DESTINATARIO)

                    For Each vlo_FilaCorreo In vlo_DsCorreosProveedor.Tables(0).Rows
                        vlo_EntCorreo = New EntGNT_DESTINATARIO()
                        vlo_EntCorreo.DESTINATARIO = vlo_FilaCorreo(Modelo.OTM_CORREO_PROVEEDOR.CORREO)
                        vlo_ListaDestinatarios.Add(vlo_EntCorreo)
                    Next

                    vlo_EntOtmProveedor = vlo_DalOtmProveedor.ObtenerRegistro(String.Format("{0} = '{1}'", Modelo.OTM_PROVEEDOR.IDENTIFICACION, vlo_FilaProveedorCotizacion(Modelo.V_OTT_PROVEEDOR_COTIZACION.IDENTIFICACION).ToString))

                    vlo_EntNotificacion = New EntGNT_NOTIFICACION()
                    vlo_EntNotificacion.ASUNTO = "Solicitud de cotización"
                    vlo_EntNotificacion.CUERPO = String.Format("<b>Estimado {0}:<b><br /><br />Se adjunta solicitud de cotización de material, con el interés de ser adquirido por la Sección de Mantenimiento y Construcción de la Oficina de Servicios Generales, Universidad de Costa Rica,  agradecemos la gestión de su parte, quedamos a la espera de su respuesta.<br />Saludos,<br />Favor no contestar este correo, esta cuenta se utiliza unicamente para notificaciones<br />Por favor dirigir su respuesta al correo: {1}.", vlo_EntOtmProveedor.Nombre, vlo_EntOtpParametroUbicacionCorreos.Valor)
                    vlo_EntNotificacion.ES_HTML = 1
                    vlo_EntNotificacion.USUARIO_CREA = ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN
                    
                    vlo_ListaAdjuntos = New List(Of EntGNT_ARCHIVO_ADJUNTO)

                    vlo_AdjuntoCorreo = New WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO
                    vlo_AdjuntoCorreo.ARCHIVO = vlo_EntReporte.Reporte
                    vlo_AdjuntoCorreo.NOMBRE_ARCHIVO = "Solicitud de cotización de material"

                    vlo_ListaAdjuntos.Add(vlo_AdjuntoCorreo)

                    vln_IdNotificacion = vlo_wsGestorNotificaciones.GNT_NOTIFICACION_Registrar(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                        vlo_EntSistema, vlo_EntNotificacion, vlo_ListaAdjuntos.ToArray(),
                        vlo_ListaDestinatarios.ToArray())

                    vlo_FilaProveedorCotizacion(Modelo.OTT_PROVEEDOR_COTIZACION.ID_NOTIFICACION) = vln_IdNotificacion
                    vlo_FilaProveedorCotizacion(Modelo.OTT_PROVEEDOR_COTIZACION.ESTADO) = EstadoProveedorCotizacion.ENVIADO
                    vlo_FilaProveedorCotizacion(Modelo.OTT_PROVEEDOR_COTIZACION.USUARIO) = pvc_IdentificacionUsuario

                Next

                vlo_DalOttProveedorCotizacion.AdapterProveedorCotizacionModificacion(vlo_DsProveedorCotizacion)

                pvo_EntOttGestionCompra.Estado = EstadoGestionCompra.REGISTRO_DE_COTIZACIONES
                pvo_EntOttGestionCompra.Usuario = pvc_IdentificacionUsuario

                vlo_DalOttGestionCompra.ModificarRegistro(pvo_EntOttGestionCompra)

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
        ''' 
        ''' </summary>
        ''' <param name="pvc_IdentificacionUsuario"></param>
        ''' <param name="pvo_EntOttGestionCompra"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>05/09/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function EnviarProveedor(pvc_Usuario As String, pvc_Clave As String, pvc_IdentificacionUsuario As String, pvo_EntOttGestionCompra As EntOttGestionCompra, pvc_IdentificacionProveedor As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vln_Resultado As Integer = New Integer - 1
            Dim vlo_DalOttProveedorCotizacion As DalOttProveedorCotizacion
            Dim vlo_DalOtpParametroUbicacion As DalOtpParametroUbicacion
            Dim vlo_EntOtpParametroUbicacionTelefonos As EntOtpParametroUbicacion
            Dim vlo_EntOtpParametroUbicacionCorreos As EntOtpParametroUbicacion
            Dim vlo_EntOttProveedorCotizacion As EntOttProveedorCotizacion
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_Ws_SDP_ReportServer As Ws_SDP_ReportServer
            Dim vlo_EntReporte As EntReporte
            Dim vlo_ListaEntParametroReporte As New List(Of EntParametroReporte)
            Dim vlo_EntParametroReporte As EntParametroReporte
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
            Dim vlo_DalOtmCorreoProveedor As DalOtmCorreoProveedor
            Dim vlo_DsCorreosProveedor As Data.DataSet
            Dim vlo_wsGestorNotificaciones As wsGestorNotificaciones
            Dim vlo_EntSistema As EntGNM_SISTEMA
            Dim vlo_ListaDestinatarios As List(Of EntGNT_DESTINATARIO)
            Dim vlo_ListaAdjuntos As New List(Of EntGNT_ARCHIVO_ADJUNTO)
            Dim vlo_AdjuntoCorreo As WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO
            Dim vlo_EntCorreo As EntGNT_DESTINATARIO
            Dim vlo_EntNotificacion As EntGNT_NOTIFICACION
            Dim vlo_DalOtmProveedor As DalOtmProveedor
            Dim vlo_EntOtmProveedor As EntOtmProveedor
            Dim vln_IdNotificacion As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Ws_SDP_ReportServer = New Ws_SDP_ReportServer
                vlo_Ws_SDP_ReportServer.Timeout = -1
                vlo_Ws_SDP_ReportServer.Credentials = System.Net.CredentialCache.DefaultCredentials
                vlo_Ws_SDP_ReportServer.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_SDP_REPORT_SERVER)

                vlo_wsGestorNotificaciones = New wsGestorNotificaciones
                vlo_wsGestorNotificaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_GESTOR_NOTIFICACIONES)
                vlo_wsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
                vlo_wsGestorNotificaciones.Timeout = -1

                vlo_Conexion.TransaccionBegin()


                vlo_EntEmpleados = CargarFuncionario(pvc_IdentificacionUsuario)

                vlo_DalOttProveedorCotizacion = New DalOttProveedorCotizacion(vlo_Conexion)
                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New DalOtpParametroUbicacion(vlo_Conexion)
                vlo_DalOtmCorreoProveedor = New DalOtmCorreoProveedor(vlo_Conexion)
                vlo_DalOtmProveedor = New DalOtmProveedor(vlo_Conexion)

                vlo_EntOtpParametroUbicacionTelefonos = vlo_DalOtpParametroUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_TELEFONO_COTIZACION_FONDO_TRABAJO))
                vlo_EntOtpParametroUbicacionCorreos = vlo_DalOtpParametroUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_COTIZACION_FONDO_TRABAJO))

                vlo_EntOttProveedorCotizacion = vlo_DalOttProveedorCotizacion.ObtenerRegistro(
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = '{9}'",
                                  Modelo.OTT_PROVEEDOR_COTIZACION.ID_UBICACION, pvo_EntOttGestionCompra.IdUbicacion,
                                  Modelo.OTT_PROVEEDOR_COTIZACION.ID_VIA_COMPRA_CONTRATO, pvo_EntOttGestionCompra.IdViaCompraContrato,
                                  Modelo.OTT_PROVEEDOR_COTIZACION.ANNO, pvo_EntOttGestionCompra.Anno,
                                  Modelo.OTT_PROVEEDOR_COTIZACION.NUMERO_GESTION, pvo_EntOttGestionCompra.NumeroGestion,
                                  Modelo.OTT_PROVEEDOR_COTIZACION.IDENTIFICACION, pvc_IdentificacionProveedor))

                vlo_EntSistema = vlo_wsGestorNotificaciones.GNM_SISTEMA_ObtenerPorNombre(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_GN))

                vlo_ListaEntParametroReporte = New List(Of EntParametroReporte)
                'configuracion de los reporte
                vlo_EntParametroReporte = New EntParametroReporte
                vlo_EntParametroReporte.Nombre = "pvc_Usuario"
                vlo_EntParametroReporte.Valor = pvc_Usuario
                vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                'configuracion de los reporte
                vlo_EntParametroReporte = New EntParametroReporte
                vlo_EntParametroReporte.Nombre = "pvc_Clave"
                vlo_EntParametroReporte.Valor = pvc_Clave
                vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                'configuracion de los reporte
                vlo_EntParametroReporte = New EntParametroReporte
                vlo_EntParametroReporte.Nombre = "pvc_Condicion"
                vlo_EntParametroReporte.Valor = String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}",
                              Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION, pvo_EntOttGestionCompra.IdUbicacion,
                              Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvo_EntOttGestionCompra.IdViaCompraContrato,
                              Modelo.OTT_LINEA_GESTION_COMPRA.ANNO, pvo_EntOttGestionCompra.Anno,
                              Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION, pvo_EntOttGestionCompra.NumeroGestion)
                vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                'configuracion de los reporte
                vlo_EntParametroReporte = New EntParametroReporte
                vlo_EntParametroReporte.Nombre = "pvc_UsuarioEjecuta"
                vlo_EntParametroReporte.Valor = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                'configuracion de los reporte
                vlo_EntParametroReporte = New EntParametroReporte
                vlo_EntParametroReporte.Nombre = "pvn_IdCorreo"
                vlo_EntParametroReporte.Valor = vlo_EntOtpParametroUbicacionCorreos.Valor
                vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                'configuracion de los reporte
                vlo_EntParametroReporte = New EntParametroReporte
                vlo_EntParametroReporte.Nombre = "pvn_IdTelefono"
                vlo_EntParametroReporte.Valor = vlo_EntOtpParametroUbicacionTelefonos.Valor
                vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                'configuracion de los reporte
                vlo_EntParametroReporte = New EntParametroReporte
                vlo_EntParametroReporte.Nombre = "pvc_NumeroOT"
                vlo_EntParametroReporte.Valor = String.Format("{0}-{1}", pvo_EntOttGestionCompra.Anno.ToString.Substring(2, 2), pvo_EntOttGestionCompra.NumeroGestion)
                vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                vlo_EntReporte = vlo_Ws_SDP_ReportServer.GenerarReporte(pvc_Usuario, pvc_Clave, "PDF", Utilerias.OrdenesDeTrabajo.Reportes.RUTA_BASE, Utilerias.OrdenesDeTrabajo.Reportes.RPT_OT_SOLICITUD_COTIZACION_MATERIALES_AL_PROVEEDOR, vlo_ListaEntParametroReporte.ToArray)

                vlo_DsCorreosProveedor = vlo_DalOtmCorreoProveedor.ListarRegistros(
                    String.Format("{0} = '{1}'", Modelo.OTM_CORREO_PROVEEDOR.IDENTIFICACION, vlo_EntOttProveedorCotizacion.Identificacion),
                    String.Empty, False, 0, 0)

                vlo_ListaDestinatarios = New List(Of EntGNT_DESTINATARIO)

                For Each vlo_FilaCorreo In vlo_DsCorreosProveedor.Tables(0).Rows
                    vlo_EntCorreo = New EntGNT_DESTINATARIO()
                    vlo_EntCorreo.DESTINATARIO = vlo_FilaCorreo(Modelo.OTM_CORREO_PROVEEDOR.CORREO)
                    vlo_ListaDestinatarios.Add(vlo_EntCorreo)
                Next

                vlo_EntOtmProveedor = vlo_DalOtmProveedor.ObtenerRegistro(String.Format("{0} = '{1}'", Modelo.OTM_PROVEEDOR.IDENTIFICACION, vlo_EntOttProveedorCotizacion.Identificacion))

                vlo_EntNotificacion = New EntGNT_NOTIFICACION()
                vlo_EntNotificacion.ASUNTO = "Solicitud de cotización"
                vlo_EntNotificacion.CUERPO = String.Format("<b>Estimado {0}:<b><br /><br />Se adjunta solicitud de cotización de material, con el interés de ser adquirido por la Sección de Mantenimiento y Construcción de la Oficina de Servicios Generales, Universidad de Costa Rica,  agradecemos la gestión de su parte, quedamos a la espera de su respuesta.<br />Saludos,<br />Favor no contestar este correo, esta cuenta se utiliza unicamente para notificaciones<br />Por favor dirigir su respuesta al correo: {1}.", vlo_EntOtmProveedor.Nombre, vlo_EntOtpParametroUbicacionCorreos.Valor)
                vlo_EntNotificacion.ES_HTML = 1
                vlo_EntNotificacion.USUARIO_CREA = ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN

                vlo_ListaAdjuntos = New List(Of EntGNT_ARCHIVO_ADJUNTO)

                vlo_AdjuntoCorreo = New WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO
                vlo_AdjuntoCorreo.ARCHIVO = vlo_EntReporte.Reporte
                vlo_AdjuntoCorreo.NOMBRE_ARCHIVO = "Solicitud de cotización de material"

                vlo_ListaAdjuntos.Add(vlo_AdjuntoCorreo)

                vln_IdNotificacion = vlo_wsGestorNotificaciones.GNT_NOTIFICACION_Registrar(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    vlo_EntSistema, vlo_EntNotificacion, vlo_ListaAdjuntos.ToArray(),
                    vlo_ListaDestinatarios.ToArray())

                vlo_EntOttProveedorCotizacion.IdNotificacion = vln_IdNotificacion
                vlo_EntOttProveedorCotizacion.Estado = EstadoProveedorCotizacion.ENVIADO
                vlo_EntOttProveedorCotizacion.Usuario = pvc_IdentificacionUsuario

                vlo_DalOttProveedorCotizacion.ModificarRegistro(vlo_EntOttProveedorCotizacion)

                pvo_EntOttGestionCompra.Estado = EstadoGestionCompra.REGISTRO_DE_COTIZACIONES
                pvo_EntOttGestionCompra.Usuario = pvc_IdentificacionUsuario

                vlo_DalOttGestionCompra.ModificarRegistro(pvo_EntOttGestionCompra)

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
		''' Permite borrar un registro en la tabla OTT_PROVEEDOR_COTIZACION, no sin antes aplicar la validación de dependencia con tablas relacionadas
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function BorrarRegistro(ByVal pvo_Registro As EntOttProveedorCotizacion) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttProveedorCotizacion As DalOttProveedorCotizacion
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If PoseeRegistrosAsociados(pvo_Registro.Identificacion, pvo_Registro.IdUbicacion, pvo_Registro.IdViaCompraContrato, pvo_Registro.Anno, pvo_Registro.NumeroGestion) Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Hay registro asociados")
				End If

				vlo_DalOttProveedorCotizacion = New DalOttProveedorCotizacion(vlo_Conexion)
				vln_Resultado = vlo_DalOttProveedorCotizacion.BorrarRegistro(pvo_Registro)
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
		''' <param name="pvc_Identificacion">Identificación del proveedor (física o jurídica)</param>
		''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
		''' <param name="pvn_IdViaCompraContrato">Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato</param>
		''' <param name="pvn_Anno">Año</param>
		''' <param name="pvn_NumeroGestion">Consecutivo de la gestión</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvc_Identificacion As String, pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_Anno As Integer, pvn_NumeroGestion As Integer) As EntOttProveedorCotizacion
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttProveedorCotizacion As DalOttProveedorCotizacion

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOttProveedorCotizacion = New DalOttProveedorCotizacion(vlo_Conexion)
				Return vlo_DalOttProveedorCotizacion.ObtenerRegistro(String.Format("UPPER({0}) = '{1}' AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_PROVEEDOR_COTIZACION.IDENTIFICACION, pvc_Identificacion.ToUpper(), Modelo.OTT_PROVEEDOR_COTIZACION.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_PROVEEDOR_COTIZACION.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_PROVEEDOR_COTIZACION.ANNO, pvn_Anno, Modelo.OTT_PROVEEDOR_COTIZACION.NUMERO_GESTION, pvn_NumeroGestion))
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
		''' <param name="pvc_Identificacion">Identificación del proveedor (física o jurídica)</param>
		''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
		''' <param name="pvn_IdViaCompraContrato">Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato</param>
		''' <param name="pvn_Anno">Año</param>
		''' <param name="pvn_NumeroGestion">Consecutivo de la gestión</param>
		''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function PoseeRegistrosAsociados(pvc_Identificacion As String, pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_Anno As Integer, pvn_NumeroGestion As Integer) As Boolean
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_PoseeRegistrosAsociados As Boolean
			Dim vlo_DalOttAdjuntoCotizacion As DalOttAdjuntoCotizacion
			Dim vlo_DalOttOfertaProveedor As DalOttOfertaProveedor

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

				'Determinar la existencia de registros asociados en la tabla OTT_ADJUNTO_COTIZACION
				vlo_DalOttAdjuntoCotizacion = New DalOttAdjuntoCotizacion(vlo_Conexion)
				If vlo_DalOttAdjuntoCotizacion.ObtenerRegistro(String.Format("UPPER({0}) = '{1}' AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_ADJUNTO_COTIZACION.IDENTIFICACION, pvc_Identificacion.ToUpper(), Modelo.OTT_ADJUNTO_COTIZACION.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ADJUNTO_COTIZACION.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_ADJUNTO_COTIZACION.ANNO, pvn_Anno, Modelo.OTT_ADJUNTO_COTIZACION.NUMERO_GESTION, pvn_NumeroGestion)).Existe Then
					Return True
				End If

				'Determinar la existencia de registros asociados en la tabla OTT_OFERTA_PROVEEDOR
				vlo_DalOttOfertaProveedor = New DalOttOfertaProveedor(vlo_Conexion)
				If vlo_DalOttOfertaProveedor.ObtenerRegistro(String.Format("UPPER({0}) = '{1}' AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_OFERTA_PROVEEDOR.IDENTIFICACION, pvc_Identificacion.ToUpper(), Modelo.OTT_OFERTA_PROVEEDOR.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_OFERTA_PROVEEDOR.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_OFERTA_PROVEEDOR.ANNO, pvn_Anno, Modelo.OTT_OFERTA_PROVEEDOR.NUMERO_GESTION, pvn_NumeroGestion)).Existe Then
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

#End Region

	End Class
End Namespace
