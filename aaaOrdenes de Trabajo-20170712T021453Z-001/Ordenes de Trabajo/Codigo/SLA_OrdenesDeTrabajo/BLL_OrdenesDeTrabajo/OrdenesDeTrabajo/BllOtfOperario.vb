Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports System.Configuration

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOtfOperario
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
        ''' Permite agregar un registro en la tabla OTF_OPERARIO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:50:58 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtfOperario) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfOperario As DalOtfOperario
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdSectorTaller, pvo_Registro.NumEmpleado).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede agregar el registro, la persona indicada ya pertenece a algún sector o taller.")
                End If

                If ObtenerRegistroPorLlaveAlterna(pvo_Registro.NumEmpleado).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede agregar el registro, la persona indicada ya pertenece a algún sector o taller.")
                End If

                vlo_DalOtfOperario = New DalOtfOperario(vlo_Conexion)
                vln_Resultado = vlo_DalOtfOperario.InsertarRegistro(pvo_Registro)
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
        ''' Permite modificar un registro en la tabla OTF_OPERARIO, no sin antes aplicar la validación de la llave alterna
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:50:58 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarRegistro(ByVal pvo_Registro As EntOtfOperario) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfOperario As DalOtfOperario
            Dim vlo_EntOtfOperario As EntOtfOperario
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_EntOtfOperario = ObtenerRegistroPorLlaveAlterna(pvo_Registro.NumEmpleado)
                If vlo_EntOtfOperario.Existe AndAlso vlo_EntOtfOperario.IdSectorTaller <> pvo_Registro.IdSectorTaller AndAlso vlo_EntOtfOperario.NumEmpleado <> pvo_Registro.NumEmpleado Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede modificar el registro, la persona indicada ya pertenece a algún sector o taller.")
                End If

                vlo_DalOtfOperario = New DalOtfOperario(vlo_Conexion)
                vln_Resultado = vlo_DalOtfOperario.ModificarRegistro(pvo_Registro)
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
        ''' Obtiene los registros de la vista V_OTF_OPERARIOLst según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:50:58 a.m.</creationDate>
        ''' <changeLog>
        ''' Autor:Mauricio Salas Chaves
        ''' Fecha: 16/09/15
        ''' Descripcion: Se agrega campo extra de puesto al Ds
        ''' </changeLog>
        Public Function ListarRegistrosLista(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfOperario As DalOtfOperario
            Dim vlo_DsDatos As DataSet
            Dim vlo_Fila As DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If


                vlo_DalOtfOperario = New DalOtfOperario(vlo_Conexion)
                vlo_DsDatos = vlo_DalOtfOperario.ListarRegistrosLista(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

                vlo_DsDatos.Tables(0).Columns.Add("PUESTO")
                'vlo_DsDatos.Tables(0).Columns.Item("PUESTO").MaxLength = 45

                For Each vlo_Fila In vlo_DsDatos.Tables(0).Rows
                    'vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTF_OPERARIOLST.PUESTO) = LlenarPuestoDS(vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTF_OPERARIOLST.NUM_EMPLEADO))
                    vlo_Fila(Modelo.V_OTF_OPERARIOLST.PUESTO) = LlenarPuestoDS(
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                        ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                        vlo_Fila(Modelo.V_OTF_OPERARIOLST.NUM_EMPLEADO))
                Next

                Return vlo_DsDatos
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        Private Function LlenarPuestoDS(pvc_Usuario As String, pvc_Clave As String, pvn_NumeroEmpleado As Integer) As String
            Dim vlo_DsPuesto As System.Data.DataSet
            Dim vlo_WsSolicitudVacaciones As WsrSolicitudVacaciones.WsSolicitudVacaciones
            Dim vlc_Puesto As String
            Dim vlo_Fila As System.Data.DataRow

            Try
                vlo_WsSolicitudVacaciones = New WsrSolicitudVacaciones.WsSolicitudVacaciones
                vlo_WsSolicitudVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
                vlo_WsSolicitudVacaciones.Timeout = -1
                vlo_WsSolicitudVacaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_SOLICITUD_VACACIONES)


                vlo_DsPuesto = vlo_WsSolicitudVacaciones.VAH_PERIODOS_DEL_EMPLEADO_ObtenerDedicacionSIRHConUnidadTODOS(
                    pvc_Usuario,
                    pvc_Clave,
                    pvn_NumeroEmpleado,
                    1)

                If vlo_DsPuesto.Tables(0) IsNot Nothing AndAlso vlo_DsPuesto.Tables(0).Rows.Count > 0 Then
                    If vlo_DsPuesto.Tables(0).Rows.Count > 1 Then
                        For Each vlo_Fila In vlo_DsPuesto.Tables(0).Rows
                            vlc_Puesto = String.Format("{0} {1}", vlc_Puesto, vlo_Fila("DESC_PUESTO").ToString)
                        Next
                    Else
                        vlc_Puesto = vlo_DsPuesto.Tables(0).Rows(0)(4)
                    End If
                Else
                    vlc_Puesto = "No tiene nombramiento actualmente"
                End If

                Return vlc_Puesto

            Catch ex As Exception
                Throw
            Finally
                If vlo_WsSolicitudVacaciones IsNot Nothing Then
                    vlo_WsSolicitudVacaciones.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Permite obtener un registro según su llave primaria
        ''' </summary>
        ''' <param name="pvn_IdSectorTaller">Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller</param>
        ''' <param name="pvn_NumEmpleado"></param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:50:58 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdSectorTaller As Integer, pvn_NumEmpleado As Double) As EntOtfOperario
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfOperario As DalOtfOperario

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfOperario = New DalOtfOperario(vlo_Conexion)
                Return vlo_DalOtfOperario.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_OPERARIO.ID_SECTOR_TALLER, pvn_IdSectorTaller, Modelo.OTF_OPERARIO.NUM_EMPLEADO, pvn_NumEmpleado))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Permite obtener un registro según su llave alterna
        ''' </summary>
        ''' <param name="pvn_NumEmpleado"></param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:50:58 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlaveAlterna(pvn_NumEmpleado As Double) As EntOtfOperario
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfOperario As DalOtfOperario

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfOperario = New DalOtfOperario(vlo_Conexion)
                Return vlo_DalOtfOperario.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTF_OPERARIO.NUM_EMPLEADO, pvn_NumEmpleado))
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
