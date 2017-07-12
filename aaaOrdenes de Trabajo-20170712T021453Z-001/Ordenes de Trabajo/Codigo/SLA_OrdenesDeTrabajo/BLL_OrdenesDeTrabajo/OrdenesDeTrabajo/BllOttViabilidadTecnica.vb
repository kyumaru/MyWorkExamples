Imports Utilerias.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports System.Configuration

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttViabilidadTecnica
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
        ''' Permite agregar un registro en la tabla OTT_VIABILIDAD_TECNICA, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/02/2016 03:07:35 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttViabilidadTecnica) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttViabilidadTecnica As DalOttViabilidadTecnica
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
                End If

                vlo_DalOttViabilidadTecnica = New DalOttViabilidadTecnica(vlo_Conexion)
                vln_Resultado = vlo_DalOttViabilidadTecnica.InsertarRegistro(pvo_Registro)
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
        ''' Permite agregar un registro en la tabla OTT_VIABILIDAD_TECNICA, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>15/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarViabilidad(ByVal pvo_Registro As EntOttViabilidadTecnica, pvb_CambiarEstado As Boolean, pvb_Aprobada As Boolean, pvo_Descripcion As String, pvn_NumEmpleado As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttViabilidadTecnica As DalOttViabilidadTecnica
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_BllOttTrazabilidadProceso As BllOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente")
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo))

                If pvb_CambiarEstado Then
                    If pvb_Aprobada Then

                        vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                        vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                        vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                        vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                        vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_APROBADA_COORD
                        ' vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                        vlo_EntOttTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                        vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                        vlo_BllOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_REVISION_JEFATURA
                        vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)
                    Else

                        vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                        vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                        vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                        vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                        vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_COORD
                        ' vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                        vlo_EntOttTrazabilidadProceso.Observaciones = pvo_Descripcion
                        vlo_EntOttTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                        vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                        vlo_BllOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_COORD
                        vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                    End If

                End If

                vlo_DalOttViabilidadTecnica = New DalOttViabilidadTecnica(vlo_Conexion)
                vln_Resultado = vlo_DalOttViabilidadTecnica.InsertarRegistro(pvo_Registro)

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
        ''' Permite modificar un registro en la tabla OTT_VIABILIDAD_TECNICA, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>15/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarViabilidad(ByVal pvo_Registro As EntOttViabilidadTecnica, pvb_CambiarEstado As Boolean, pvb_Aprobada As Boolean, pvo_Descripcion As String, pvn_NumEmpleado As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttViabilidadTecnica As DalOttViabilidadTecnica
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_BllOttTrazabilidadProceso As BllOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
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

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo))

                If pvb_CambiarEstado Then
                    If pvb_Aprobada Then

                        vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                        vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                        vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                        vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                        vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_APROBADA_COORD
                        ' vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                        vlo_EntOttTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                        vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                        vlo_BllOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_REVISION_JEFATURA
                        vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)
                    Else

                        vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                        vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                        vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                        vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                        vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_COORD
                        'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                        vlo_EntOttTrazabilidadProceso.Observaciones = pvo_Descripcion
                        vlo_EntOttTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                        vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                        vlo_BllOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_COORD
                        vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                    End If

                End If

                vlo_DalOttViabilidadTecnica = New DalOttViabilidadTecnica(vlo_Conexion)
                vln_Resultado = vlo_DalOttViabilidadTecnica.ModificarRegistro(pvo_Registro)

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
        ''' Permite modificar un registro en la tabla OTT_VIABILIDAD_TECNICA, no sin antes aplicar la validación de la llave primaria o alterna según corresponda, ademas inserta un doc tipo oficio
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>26/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarViabilidadOficio(ByVal pvo_Registro As EntOttViabilidadTecnica, pvb_Aprobada As Boolean, pvo_Descripcion As String, pvn_NumEmpleado As Integer, pvo_OficioAdjunto As EntOttAdjuntoOrdenTrabajo, pvb_ExisteArchivo As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttViabilidadTecnica As DalOttViabilidadTecnica
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_DalOtmUnidadTiempo As AccesoDatos.Catalogos.DalOtmUnidadTiempo
            Dim vlo_DalOtpParametroUbicacion As AccesoDatos.Catalogos.DalOtpParametroUbicacion
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_EntOtmUnidadTiempo As EntidadNegocio.Catalogos.EntOtmUnidadTiempo
            Dim vlo_DsParametros As Data.DataSet
            Dim vlc_TiempoRespuesta As String
            Dim vlc_CorreoAdministrador As String

            Dim vlo_BllOttTrazabilidadProceso As BllOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso
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

                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New AccesoDatos.Catalogos.DalOtpParametroUbicacion(vlo_Conexion)
                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvo_Registro.IdUbicacion, Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_Registro.IdOrdenTrabajo))
                vlo_DalOtmUnidadTiempo = New AccesoDatos.Catalogos.DalOtmUnidadTiempo(vlo_Conexion)

                If pvb_Aprobada Then

                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_APROBADA_JEFATURA
                    vlo_EntOttTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                    vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                    vlo_BllOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                    If pvo_Registro.Viabilidad = 1 Then
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.PENDIENTE_RESPUESTA_SOLICITANTE

                        'obtiene la unidad de tiempo para la descripción del restante
                        vlo_EntOtmUnidadTiempo = vlo_DalOtmUnidadTiempo.ObtenerRegistro(
                            String.Format("{0} = {1}", Modelo.OTM_UNIDAD_TIEMPO.ID_UNIDAD_TIEMPO, pvo_Registro.IdUnidadTiempo))

                        If pvo_Registro.TiempoRespuesta = 1 Then
                            vlc_TiempoRespuesta = String.Format("{0} {1}", pvo_Registro.TiempoRespuesta, vlo_EntOtmUnidadTiempo.Descripcion)
                        Else
                            vlc_TiempoRespuesta = String.Format("{0} {1}S", pvo_Registro.TiempoRespuesta, vlo_EntOtmUnidadTiempo.Descripcion)
                        End If

                        vlo_DsParametros = vlo_DalOtpParametroUbicacion.ListarRegistrosLista(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR),
                                                                                          String.Empty, False, 0, 0)
                        'Obtiene el correo del administrador del sistema
                        If vlo_DsParametros.Tables.Count > 0 AndAlso vlo_DsParametros.Tables(0).Rows.Count > 0 Then
                            vlc_CorreoAdministrador = vlo_DsParametros.Tables(0).Rows(0).Item(Modelo.OTP_PARAMETRO_UBICACION.VALOR)
                        End If

                        EnviarCorreoNotificacionAprobada(vlo_EntOttOrdenTrabajo, vlc_TiempoRespuesta, vlc_CorreoAdministrador)

                    Else
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.LIQUIDADA
                    End If

                    vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)
                Else

                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = pvo_Registro.IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = pvn_NumEmpleado
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_JEFATURA
                    vlo_EntOttTrazabilidadProceso.Observaciones = pvo_Descripcion
                    vlo_EntOttTrazabilidadProceso.Usuario = pvo_Registro.Usuario

                    vlo_BllOttTrazabilidadProceso = New BllOttTrazabilidadProceso(vlo_Conexion)
                    vlo_BllOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                    vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_JEFATURA
                    vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                    'COREEO NOTIFICAR, COORDINADOR
                    vlo_DsParametros = vlo_DalOtpParametroUbicacion.ListarRegistrosLista(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR),
                                                                                      String.Empty, False, 0, 0)

                    'Obtiene el correo del administrador del sistema
                    If vlo_DsParametros.Tables.Count > 0 AndAlso vlo_DsParametros.Tables(0).Rows.Count > 0 Then
                        vlc_CorreoAdministrador = vlo_DsParametros.Tables(0).Rows(0).Item(Modelo.OTP_PARAMETRO_UBICACION.VALOR)
                    End If

                    EnviarCorreoNotificacionDevuelta(vlo_EntOttOrdenTrabajo, vlc_CorreoAdministrador)

                End If

                If pvb_Aprobada Then
                    vlo_DalOttViabilidadTecnica = New DalOttViabilidadTecnica(vlo_Conexion)
                    vlo_DalOttViabilidadTecnica.ModificarRegistro(pvo_Registro)
                End If

                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)

                If pvo_OficioAdjunto IsNot Nothing Then
                    If pvo_OficioAdjunto.Archivo.Length > 0 Then

                        If pvb_ExisteArchivo Then
                            vlo_DalOttAdjuntoOrdenTrabajo.ModificarRegistro(pvo_OficioAdjunto)
                        Else
                            vlo_DalOttAdjuntoOrdenTrabajo.InsertarRegistro(pvo_OficioAdjunto)
                        End If

                    End If
                End If

                vln_Resultado = 1
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
        ''' Permite obtener un registro según su llave primaria
        ''' </summary>
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvc_IdOrdenTrabajo">Identificador único alfanumérico de la orden de trabajo</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/02/2016 03:07:35 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String) As EntOttViabilidadTecnica
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttViabilidadTecnica As DalOttViabilidadTecnica

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttViabilidadTecnica = New DalOttViabilidadTecnica(vlo_Conexion)
                Return vlo_DalOttViabilidadTecnica.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTT_VIABILIDAD_TECNICA.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_VIABILIDAD_TECNICA.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper()))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Guarda la respuesta del usuario a la viabilidad tecnica
        ''' </summary>
        ''' <param name="pvb_Presupuesto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez García</author>
        ''' <creationDate>26/02/2016</creationDate>
        ''' <changeLog>Carlos Gomez -  Se modifica la logica general de método ya que generaba mucho errores  27/07/2016 </changeLog>
        Function GuardarRespuestaViabilidadTecnica(pvo_adjunto As EntOttAdjuntoOrdenTrabajo, pvb_Presupuesto As Boolean, pvb_enviar As Boolean, pvn_idSector As Integer, pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String) As Utilerias.Genericos.EntDatosPaginacion
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_DalOtmSectorTaller As AccesoDatos.Catalogos.DalOtmSectorTaller
            Dim vlo_DalOtpParametroUbicacion As AccesoDatos.Catalogos.DalOtpParametroUbicacion
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab
            Dim vlc_Condicion As String
            Dim vlc_Coordinador As String
            Dim vlc_NombreCoordinador As String
            Dim vlc_JefeSeccion As String
            Dim vlc_CorreoAdministrador As String
            Dim vlc_ProfEncargado As String
            Dim vlc_NombreProfEncargado As String
            Dim vlc_ListaUsuariosRoL As String()
            Dim vlc_NombreSolicitante As String
            Dim vlc_CorreoSolicitante As String
            Dim vlo_DsLugar As Data.DataSet
            Dim vlo_DsParametros As Data.DataSet
            Dim vlo_DsOperarioOrdenTrabajo As Data.DataSet
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlo_EntOttTrazabilidadProceso As EntOttTrazabilidadProceso

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New AccesoDatos.Catalogos.DalOtpParametroUbicacion(vlo_Conexion)
                vlo_DalOtmSectorTaller = New AccesoDatos.Catalogos.DalOtmSectorTaller(vlo_Conexion)

                vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}",
                                                Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION,
                                                pvn_IdUbicacion,
                                                Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO,
                                                pvc_IdOrdenTrabajo,
                                                Modelo.OTT_ORDEN_TRABAJO.ID_SECTOR_TALLER,
                                                pvn_idSector))

                vlc_Condicion = String.Format("UPPER({0}) = '{1}'", Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, vlo_EntOttOrdenTrabajo.IdSectorTaller)

                vlo_DsLugar = vlo_DalOtmSectorTaller.ListarRegistrosLista(
                                        vlc_Condicion,
                                        String.Empty,
                                        False,
                                        0,
                                        0)

                vlc_Coordinador = CargarFuncionario(vlo_DsLugar.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NUM_EMPLEADO_COORDINADOR).ToString).CORREO_INSTITUCIONAL
                vlc_NombreCoordinador = vlo_DsLugar.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE_COORDINADOR).ToString
                If pvb_Presupuesto Then

                    vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.EVALUADA
                    vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                    If pvo_adjunto.Archivo.Length > 0 Then
                        vlo_DalOttAdjuntoOrdenTrabajo.InsertarRegistro(pvo_adjunto)
                    End If

                Else

                    vlo_EntEmpleados = CargarFuncionario(pvo_adjunto.Usuario)

                    vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                    vlo_EntOttTrazabilidadProceso = New EntOttTrazabilidadProceso

                    vlo_EntOttTrazabilidadProceso.IdUbicacion = pvn_IdUbicacion
                    vlo_EntOttTrazabilidadProceso.IdOrdenTrabajo = pvc_IdOrdenTrabajo
                    vlo_EntOttTrazabilidadProceso.NumEmpleadoEjecuta = vlo_EntEmpleados.NUM_EMPLEADO
                    vlo_EntOttTrazabilidadProceso.EstadoOrdenTrabajo = EstadoOrden.LIQUIDADA
                    'vlo_EntOttTrazabilidadProceso.FechaHoraEjecucion = DateTime.Now
                    vlo_EntOttTrazabilidadProceso.Observaciones = String.Empty
                    vlo_EntOttTrazabilidadProceso.Usuario = pvo_adjunto.Usuario

                    vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                    vlo_DalOttTrazabilidadProceso.InsertarRegistro(vlo_EntOttTrazabilidadProceso)

                    vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.LIQUIDADA
                    vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                    If pvo_adjunto.Archivo.Length > 0 Then
                        vlo_DalOttAdjuntoOrdenTrabajo.InsertarRegistro(pvo_adjunto)
                    End If


                End If

                If pvb_enviar Then

                    'Se obtiene el correo del profesional encargado
                    vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)

                    vlc_Condicion = String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION, pvn_IdUbicacion,
                                                  Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo, Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO, Cargo.ENCARGADO)

                    vlo_DsOperarioOrdenTrabajo = vlo_DalOttOperarioOrdenTrab.ListarRegistrosLista(vlc_Condicion, String.Format("{0} {1}", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA, Ordenamiento.DESCENDENTE), False, 0, 0)

                    If vlo_DsOperarioOrdenTrabajo.Tables(0) IsNot Nothing AndAlso vlo_DsOperarioOrdenTrabajo.Tables(0).Rows.Count > 0 Then
                        vlc_ProfEncargado = vlo_DsOperarioOrdenTrabajo.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CEDULA).ToString
                    End If

                    vlo_EntEmpleados = CargarFuncionario(vlc_ProfEncargado)

                    vlc_ProfEncargado = vlo_EntEmpleados.CORREO_INSTITUCIONAL

                    vlc_NombreProfEncargado = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)

                    'Se obtiene la lista de cedulas de los jefes de mantenimiento y construcción
                    vlc_ListaUsuariosRoL = CargarUsuariosRol(RolesSistema.OT_JEFE_SECCION)

                    'Se obtiene el nombre y correo del solicitante
                    vlo_EntEmpleados = CargarFuncionario(vlo_EntOttOrdenTrabajo.NumEmpleado)

                    vlc_NombreSolicitante = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                    vlc_CorreoSolicitante = vlo_EntEmpleados.CORREO_INSTITUCIONAL

                    'Obtiene el correo del administrador del sistema
                    vlo_DsParametros = vlo_DalOtpParametroUbicacion.ListarRegistrosLista(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR),
                                                                                      String.Empty, False, 0, 0)

                    If vlo_DsParametros.Tables.Count > 0 AndAlso vlo_DsParametros.Tables(0).Rows.Count > 0 Then
                        vlc_CorreoAdministrador = vlo_DsParametros.Tables(0).Rows(0).Item(Modelo.OTP_PARAMETRO_UBICACION.VALOR)
                    End If

                    EnviarCorreoDisponibilidadAprobacion(vlc_ListaUsuariosRoL, vlc_Coordinador, vlc_NombreCoordinador, vlc_ProfEncargado, vlc_NombreProfEncargado, vlc_NombreSolicitante, vlc_CorreoSolicitante, vlo_EntOttOrdenTrabajo.NombreProyecto, vlo_EntOttOrdenTrabajo.IdOrdenTrabajo, pvb_Presupuesto, vlc_CorreoAdministrador)

                End If

                vlo_Conexion.TransaccionCommit()

            Catch ex As Exception
                Throw
            End Try
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CargarUsuariosRol(pvc_RoleName As String) As String()
            Dim vlo_WsOracleRolesProvider As WsrOracleRolesProvider.WsOracleRolesProvider
            Dim vlc_ProviderName As String

            vlo_WsOracleRolesProvider = New WsrOracleRolesProvider.WsOracleRolesProvider
            vlo_WsOracleRolesProvider.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsOracleRolesProvider.Timeout = -1
            vlo_WsOracleRolesProvider.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_ORACLE_ROLES_PROVIDER)

            Try
                vlc_ProviderName = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ROL_PROVIDER_NAME)

                Return vlo_WsOracleRolesProvider.GetUsersInRole(vlc_ProviderName, pvc_RoleName)

            Catch ex As Exception
                Throw
            Finally
                If vlo_WsOracleRolesProvider IsNot Nothing Then
                    vlo_WsOracleRolesProvider.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
        ''' </summary>
        ''' <param name="pvc_Valor"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Cesar bermudez garcia</author>
        ''' <creationDate>26/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function CargarFuncionario(pvc_Valor As String) As WsrEU_Curriculo.EntEmpleados
            Dim vlo_WsEU_Curriculo As WsrEU_Curriculo.wsEU_Curriculo

            vlo_WsEU_Curriculo = New WsrEU_Curriculo.wsEU_Curriculo
            vlo_WsEU_Curriculo.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsEU_Curriculo.Timeout = -1
            vlo_WsEU_Curriculo.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_EU_CURRICULO)

            Try
                Return vlo_WsEU_Curriculo.Empleados_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    String.Format("ID_PERSONAL = '{0}' OR NUM_EMPLEADO = {0}", pvc_Valor))
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
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez G</author>
        ''' <creationDate>25/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Function EnviarCorreoNotificacionAprobada(pvo_Registro As EntOttOrdenTrabajo, pvc_TiempoReal As String, pvc_CorreoAdministrador As String) As Integer
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
                    'Obtiene la Cédula del funcionario actual
                    vlo_Empleado = CargarFuncionario(pvo_Registro.NumEmpleado)
                    If Not String.IsNullOrWhiteSpace(vlo_Empleado.CORREO_INSTITUCIONAL) Then
                        '{0}: Numero de orden de trabajo
                        vlo_Notificacion.ASUNTO = String.Format("Informe de valoración técnica del proyecto para la orden de trabajo N°{0}", pvo_Registro.IdOrdenTrabajo)

                        '{0}: Nombre del profesional
                        '{1}: Apellido 1 del profesional
                        '{2}: Apellido 2 del profesional
                        '{3}: Nombre del proyecto
                        '{4}: Estimación de tiempo
                        '{5}: Correo del administrador del sistema

                        vlo_Notificacion.CUERPO = String.Format("<b>Señor(a):{0} {1} {2}</b><br /><br /><b>Estimado(a) señor(a)</b><br />Se le notifica que la valoración técnica del proyecto:{3}, ha sido aprobada por la Jefatura de la Sección de Mantenimiento y Construcción. No omito indicarle que la estimación preliminar adjunta se basa en valores por metros cuadrados de proyectos similares y de costos generales pero <b><u>NO es el monto definitivo.</b></u><br /><br />Le solicitamos verificar el oficio adjunto, el cual puede descargar también desde el sistema de Ordenes de Trabajo. Usted dispone de {4} para dar su respuesta mediante sistema, acerca de la disponibilidad presupuestaria de su Unidad para la ejecucioón del proyecto, vencido el plazo su solicitud será liquidada.<br /><br />No debe omitirse el envío físico del oficio a la Unidad.<hr><i>Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema refiérase a la dirección: {5}</i>",
                                           vlo_Empleado.NOMBRE, vlo_Empleado.APELLIDO1, vlo_Empleado.APELLIDO2, pvo_Registro.NombreProyecto, pvc_TiempoReal, pvc_CorreoAdministrador)
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
        ''' Envia el correo electrónico y devuelve un valor mayor a cero si tuvo éxito o menor a cero si ocurrió un fallo
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez G</author>
        ''' <creationDate>25/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Function EnviarCorreoNotificacionDevuelta(pvo_Registro As EntOttOrdenTrabajo, pvc_CorreoAdministrador As String) As Integer
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
                    'Obtiene la Cédula del funcionario actual
                    vlo_Empleado = CargarFuncionario(pvo_Registro.NumEmpleado)
                    If Not String.IsNullOrWhiteSpace(vlo_Empleado.CORREO_INSTITUCIONAL) Then
                        '{0}: Numero de orden de trabajo
                        vlo_Notificacion.ASUNTO = String.Format("Informe de valoración técnica del proyecto para la orden de trabajo N°{0}", pvo_Registro.IdOrdenTrabajo)

                        '{0}: Nombre del profesional
                        '{1}: Apellido 1 del profesional
                        '{2}: Apellido 2 del profesional
                        '{3}: Nombre del proyecto
                        '{4}: Correo del administrador del sistema

                        vlo_Notificacion.CUERPO = String.Format("<b>Señor(a):{0} {1} {2}</b><br /><br /><b>Estimado(a) señor(a)</b><br />Se le notifica que la valoración técnica del proyecto:{3}, ha sido clasificada como no viable. <br /><br />Le solicitamos verificar el oficio adjunto en donde se detallan las razones por las cuales el proyecto NO tiene viabilidad, por lo cual la orden ha sido liquidada.<br /><hr><i>Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema refiérase a la dirección: {4}</i>",
                                           vlo_Empleado.NOMBRE, vlo_Empleado.APELLIDO1, vlo_Empleado.APELLIDO2, pvo_Registro.NombreProyecto, pvc_CorreoAdministrador)
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
        ''' Envia el correo electrónico y devuelve un valor mayor a cero si tuvo éxito o menor a cero si ocurrió un fallo
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez G</author>
        ''' <creationDate>26/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Function EnviarCorreoDisponibilidadAprobacion(pvc_ListaUsuariosRoL As String(), pvc_CorreoCoordinador As String, pvc_NombreCoordinador As String, pvc_CorreoProfesional As String, pvc_NombreProfesional As String, pvc_NombreSolicitante As String, pvc_CorreoSolicitante As String, pvc_NombreProyecto As String, pvc_idOrdenTrabajo As String, pvb_Aprobacion As Boolean, pvc_CorreoAdministrador As String) As Integer
            Dim vlo_WsGestorNotificaciones As WsrGestorNotificaciones.wsGestorNotificaciones
            Dim vlo_Sistema As WsrGestorNotificaciones.EntGNM_SISTEMA
            Dim vlo_ListaAdjunto As List(Of WsrGestorNotificaciones.EntGNT_ARCHIVO_ADJUNTO)
            Dim vlo_ListaDestinatario As List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)
            Dim vlo_Notificacion As WsrGestorNotificaciones.EntGNT_NOTIFICACION
            Dim vlo_EntGNT_DESTINATARIO As WsrGestorNotificaciones.EntGNT_DESTINATARIO
            Dim vlo_Empleado As WsrEU_Curriculo.EntEmpleados
            Dim vlb_bandera As Boolean
            Dim vln_Resultado As Integer


            vlo_WsGestorNotificaciones = New WsrGestorNotificaciones.wsGestorNotificaciones
            vlo_WsGestorNotificaciones.Timeout = -1
            vlo_WsGestorNotificaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsGestorNotificaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_GESTOR_NOTIFICACIONES)

            Try
                vln_Resultado = -1
                vlb_bandera = True
                vlo_Sistema = vlo_WsGestorNotificaciones.GNM_SISTEMA_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    String.Format("NOMBRE_SISTEMA = '{0}'", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_GN)))


                vlo_ListaDestinatario = New List(Of WsrGestorNotificaciones.EntGNT_DESTINATARIO)


                If vlo_Sistema IsNot Nothing AndAlso vlo_Sistema.Existe Then
                    vlo_Notificacion = New WsrGestorNotificaciones.EntGNT_NOTIFICACION()
                    For Each vlo_fila As String In pvc_ListaUsuariosRoL
                        'Obtiene la Cédula del funcionario actual
                        vlo_Empleado = CargarFuncionario(vlo_fila)
                        If Not String.IsNullOrWhiteSpace(vlo_Empleado.CORREO_INSTITUCIONAL) Then
                            vlo_Notificacion.ES_HTML = 1
                            vlo_Notificacion.USUARIO_CREA = System.Environment.UserName.Trim()

                            If pvb_Aprobacion Then
                                '{0}: Numero de orden de trabajo
                                vlo_Notificacion.ASUNTO = String.Format("Respuesta de valoración técnica por parte del solicitante para la orden de trabajo N°{0}", pvc_idOrdenTrabajo)

                                '{0}: Nombre del jefe de mantenimiento
                                '{1}: Apellido 1 del jefe de mantenimiento
                                '{2}: Apellido 2 del jefe de mantenimiento
                                '{3}: Nombre del solicitante
                                '{4}: Nombre del proyecto
                                '{5}: Correo del administrador del sistema

                                vlo_Notificacion.CUERPO = String.Format("<b>Señor(a):{0} {1} {2}</b><br /><br /><b>Estimado(a) señor(a)</b><br />Se le notifica que el señor(a):{3}, ha efectuado la revisión de la valoración preliminar para el proyecto {4}; Sírvase revisar dicha notificacion en el sistema.<br /><hr><i>Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema refiérase a la dirección: {5}</i>",
                                                   vlo_Empleado.NOMBRE, vlo_Empleado.APELLIDO1, vlo_Empleado.APELLIDO2, pvc_NombreSolicitante, pvc_NombreProyecto, pvc_CorreoAdministrador)

                            Else
                                '{0}: Numero de orden de trabajo
                                vlo_Notificacion.ASUNTO = String.Format("Liquidación de la orden de trabajo N°{0}", pvc_idOrdenTrabajo)
                                '{0}: Nombre del solicitante
                                '{1}: Nombre del proyecto
                                '{2}: Correo del administrador del sistema
                                vlo_Notificacion.CUERPO = String.Format("<b>Señor(a):{0}</b><br /><br /><b>Estimado(a) señor(a)</b><br />Se le notifica que el plazo de respuesta del cual disponía para la revisión del informe de valoración técnica del proyecto:{1}, ha vencido; motivo por el cual su solicitud ha sido liquidada automáticamente.<br /><hr><i>Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema refiérase a la dirección: {2}</i>",
                                                pvc_NombreSolicitante, pvc_NombreProyecto, pvc_CorreoAdministrador)

                            End If

                            If vlo_Empleado.CORREO_INSTITUCIONAL <> pvc_CorreoCoordinador AndAlso vlo_Empleado.CORREO_INSTITUCIONAL <> pvc_CorreoProfesional AndAlso vlo_Empleado.CORREO_INSTITUCIONAL <> pvc_CorreoSolicitante Then
                                vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
                                vlo_EntGNT_DESTINATARIO.DESTINATARIO = vlo_Empleado.CORREO_INSTITUCIONAL
                                vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)
                            End If


                            'Solo deben enviarse una vez estos correos, por ello se coloca la bandera
                            If vlb_bandera Then
                                'en el caso de que NO sea aprobación el solicitante debe estar en la lista
                                If Not pvb_Aprobacion Then
                                    If pvc_CorreoSolicitante <> pvc_CorreoCoordinador AndAlso pvc_CorreoSolicitante <> pvc_CorreoProfesional AndAlso pvc_CorreoSolicitante <> vlo_Empleado.CORREO_INSTITUCIONAL Then
                                        vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
                                        vlo_EntGNT_DESTINATARIO.DESTINATARIO = pvc_CorreoSolicitante
                                        vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)
                                    End If

                                End If
                                'Se agregan los correos del profesional y del coordinador del taller
                                If Not String.IsNullOrWhiteSpace(pvc_CorreoCoordinador) Then
                                    If pvc_CorreoCoordinador <> pvc_CorreoSolicitante AndAlso pvc_CorreoCoordinador <> pvc_CorreoProfesional AndAlso pvc_CorreoCoordinador <> vlo_Empleado.CORREO_INSTITUCIONAL Then
                                        vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
                                        vlo_EntGNT_DESTINATARIO.DESTINATARIO = pvc_CorreoCoordinador
                                        vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)
                                    End If
                                End If

                                If Not String.IsNullOrWhiteSpace(pvc_CorreoProfesional) Then
                                    If pvc_CorreoProfesional <> pvc_CorreoSolicitante AndAlso pvc_CorreoProfesional <> pvc_CorreoCoordinador AndAlso pvc_CorreoProfesional <> vlo_Empleado.CORREO_INSTITUCIONAL Then
                                        vlo_EntGNT_DESTINATARIO = New WsrGestorNotificaciones.EntGNT_DESTINATARIO()
                                        vlo_EntGNT_DESTINATARIO.DESTINATARIO = pvc_CorreoProfesional
                                        vlo_ListaDestinatario.Add(vlo_EntGNT_DESTINATARIO)
                                    End If

                                End If
                                vlb_bandera = False
                            End If

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


#End Region
    End Class
End Namespace
