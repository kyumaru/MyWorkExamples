Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOttLineaAdjudicacion
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
		''' Permite agregar un registro en la tabla OTT_LINEA_ADJUDICACION, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>06/05/2016 10:46:05 a.m.</creationDate>
        ''' <changeLog>
        ''' <author>César Bermudez</author>
        ''' <creationDate>11/05/2016</creationDate>
        ''' </changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttLineaAdjudicacion, pvo_Adjunto As EntOttAdjuntoOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttLineaAdjudicacion As DalOttLineaAdjudicacion
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_DalOttDocumentoContratacion As DalOttDocumentoContratacion
            Dim vlo_EntOttDocumentoContratacion As EntOttDocumentoContratacion
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If pvo_Adjunto.Archivo Is Nothing Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("ERROR: El archivo adjuntado está vacio.")
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo, pvo_Registro.Version, pvo_Registro.NumeroLinea).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("ERROR: Ya existe un registro con el mismo número de linea.")
                End If

                vlo_DalOttLineaAdjudicacion = New DalOttLineaAdjudicacion(vlo_Conexion)
                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DalOttDocumentoContratacion = New DalOttDocumentoContratacion(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()

                vln_Resultado = vlo_DalOttLineaAdjudicacion.InsertarRegistro(pvo_Registro)

                If vln_Resultado > 0 Then
                    vln_Resultado = vlo_DalOttAdjuntoOrdenTrabajo.InsertarRegistro(pvo_Adjunto)

                    If vln_Resultado > 0 Then
                        vlo_EntOttDocumentoContratacion = New EntOttDocumentoContratacion

                        vlo_EntOttDocumentoContratacion.IdAdjuntoOrdenTrabajo = vln_Resultado
                        vlo_EntOttDocumentoContratacion.NumeroLinea = pvo_Registro.NumeroLinea
                        vlo_EntOttDocumentoContratacion.IdEtapaContratacion = EtapaContratacion.ADJUDICACION
                        vlo_EntOttDocumentoContratacion.IdEtapaOrdenTrabajo = EtapasOrdenTrabajo.CONTRATACIONES
                        vlo_EntOttDocumentoContratacion.IdOrdenTrabajo = pvo_Registro.IdOrdenTrabajo
                        vlo_EntOttDocumentoContratacion.IdTipoDocumento = TipoDocumento.OFICIO
                        vlo_EntOttDocumentoContratacion.IdUbicacion = pvo_Registro.IdUbicacion
                        vlo_EntOttDocumentoContratacion.DocumentoTramitado = Documento.NO_TRAMITADO
                        vlo_EntOttDocumentoContratacion.Usuario = pvo_Registro.Usuario
                        vlo_EntOttDocumentoContratacion.Version = pvo_Registro.Version
                        vlo_EntOttDocumentoContratacion.FechaHoraRegistro = Now

                        vln_Resultado = vlo_DalOttDocumentoContratacion.InsertarRegistro(vlo_EntOttDocumentoContratacion)
                    End If
                End If

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
        ''' Modifica la linea de contratación así como su documento adjunto
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <param name="pvo_Adjunto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez</author>
        ''' <creationDate>11/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Function ModificarRegistro(pvo_Registro As EntOttLineaAdjudicacion, pvo_Adjunto As EntOttAdjuntoOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttLineaAdjudicacion As DalOttLineaAdjudicacion
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If pvo_Adjunto.Archivo Is Nothing Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("ERROR: El archivo adjuntado está vacio.")
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo, pvo_Registro.Version, pvo_Registro.NumeroLinea).Existe Then

                    vlo_DalOttLineaAdjudicacion = New DalOttLineaAdjudicacion(vlo_Conexion)
                    vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)

                    vlo_Conexion.TransaccionBegin()

                    Try
                        vln_Resultado = vlo_DalOttLineaAdjudicacion.ModificarRegistro(pvo_Registro)
                    Catch ex As Exception
                    Finally
                        If pvo_Adjunto.Existe Then
                            vln_Resultado = vlo_DalOttAdjuntoOrdenTrabajo.ModificarRegistro(pvo_Adjunto)
                        End If
                    End Try

                End If

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
		''' Permite borrar un registro en la tabla OTT_LINEA_ADJUDICACION, no sin antes aplicar la validación de dependencia con tablas relacionadas
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>06/05/2016 10:46:05 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function BorrarRegistro(ByVal pvo_Registro As EntOttLineaAdjudicacion) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttLineaAdjudicacion As DalOttLineaAdjudicacion
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If PoseeRegistrosAsociados(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo, pvo_Registro.Version, pvo_Registro.NumeroLinea) Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Hay registro asociados")
				End If

				vlo_DalOttLineaAdjudicacion = New DalOttLineaAdjudicacion(vlo_Conexion)
				vln_Resultado = vlo_DalOttLineaAdjudicacion.BorrarRegistro(pvo_Registro)
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
		''' <param name="pvn_Version">Numero de version del proceso de contratación asociado a una orden de trabajo</param>
		''' <param name="pvn_NumeroLinea">Número de línea adjudicada</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>06/05/2016 10:46:05 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_Version As Integer, pvn_NumeroLinea As Integer) As EntOttLineaAdjudicacion
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttLineaAdjudicacion As DalOttLineaAdjudicacion

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOttLineaAdjudicacion = New DalOttLineaAdjudicacion(vlo_Conexion)
				Return vlo_DalOttLineaAdjudicacion.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}' AND {4} = {5} AND {6} = {7}", Modelo.OTT_LINEA_ADJUDICACION.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_LINEA_ADJUDICACION.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper(), Modelo.OTT_LINEA_ADJUDICACION.VERSION, pvn_Version, Modelo.OTT_LINEA_ADJUDICACION.NUMERO_LINEA, pvn_NumeroLinea))
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
		''' <param name="pvc_IdOrdenTrabajo">Identificador único alfanumérico de la orden de trabajo</param>
		''' <param name="pvn_Version">Numero de version del proceso de contratación asociado a una orden de trabajo</param>
		''' <param name="pvn_NumeroLinea">Número de línea adjudicada</param>
		''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>06/05/2016 10:46:05 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function PoseeRegistrosAsociados(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_Version As Integer, pvn_NumeroLinea As Integer) As Boolean
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_PoseeRegistrosAsociados As Boolean
			Dim vlo_DalOttDocumentoContratacion As DalOttDocumentoContratacion

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

				'Determinar la existencia de registros asociados en la tabla OTT_DOCUMENTO_CONTRATACION
				vlo_DalOttDocumentoContratacion = New DalOttDocumentoContratacion(vlo_Conexion)
				If vlo_DalOttDocumentoContratacion.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}' AND {4} = {5} AND {6} = {7}", Modelo.OTT_DOCUMENTO_CONTRATACION.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper(), Modelo.OTT_DOCUMENTO_CONTRATACION.VERSION, pvn_Version, Modelo.OTT_DOCUMENTO_CONTRATACION.NUMERO_LINEA, pvn_NumeroLinea)).Existe Then
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
        ''' Manda a llamar al servicio que suma Días Hábiles
        ''' </summary>
        ''' <param name="pvd_FechaInicio"></param>
        ''' <param name="pvn_Plazo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez G</author>
        ''' <creationDate>06/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Function SumaDiasHabiles(pvd_FechaInicio As Date, pvn_Plazo As Integer) As Date
            Dim vlo_WsPlataformaDeServicios As WsrPlataformaDeServicios.WsPlataformaDeServicios

            vlo_WsPlataformaDeServicios = New WsrPlataformaDeServicios.WsPlataformaDeServicios
            vlo_WsPlataformaDeServicios.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsPlataformaDeServicios.Timeout = -1
            vlo_WsPlataformaDeServicios.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_PLATAFORMA_SERVICIOS)

            Try

                Return vlo_WsPlataformaDeServicios.PST_SOLICITUD_CONSTANCIA_SumaDiasHabiles(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    pvd_FechaInicio, pvn_Plazo)

            Catch ex As Exception
                Throw
            Finally
                If vlo_WsPlataformaDeServicios IsNot Nothing Then
                    vlo_WsPlataformaDeServicios.Dispose()
                End If
            End Try

        End Function

#End Region


	End Class
End Namespace
