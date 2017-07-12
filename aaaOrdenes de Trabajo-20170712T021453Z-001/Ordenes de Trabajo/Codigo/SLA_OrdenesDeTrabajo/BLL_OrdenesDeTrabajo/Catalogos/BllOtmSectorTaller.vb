Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
    Public Class BllOtmSectorTaller
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
        ''' Permite agregar un registro en la tabla OTM_SECTOR_TALLER, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:29:57 a.m.</creationDate>
        ''' <changeLog>
        '''    <autor>Cesar Bermudez Garcia</autor>
        '''    <fecha>03/02/2016</fecha>
        '''    <cambios>Verificar si el coordinador o el sustituto ya están asignados a un taller o sector</cambios>
        ''' </changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmSectorTaller) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmSectorTaller As DalOtmSectorTaller
            Dim vln_Resultado As Integer
            Dim vlo_EntOtmSectorTaller As EntOtmSectorTaller
            Dim vlb_valido As Boolean = True


            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacionAdministra, pvo_Registro.Nombre).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede agregar el registro, ya existe un Sector o Taller con el nombre indicado.")
                End If

                If pvo_Registro.NumEmpleadoCoordinador = pvo_Registro.NumEmpleadoSustituto Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede agregar el registro, el coordinador y el sustituto deben ser funcionarios diferentes.")
                End If

                If ObtenerRegistroPorCoordinador(pvo_Registro.NumEmpleadoCoordinador).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede agregar el registro, ya existe un Sector o Taller con el coordinador indicado.")
                End If

                vlo_DalOtmSectorTaller = New DalOtmSectorTaller(vlo_Conexion)

                'Se verifica si el empleado coordinador se encuentra asignado a un taller
                '{0}: Columna NUM_EMPLEADO_COORDINADOR
                '{1}: Valor del empleado coordinador proveniente de la UI
                '{2}: NUM_EMPLEADO_SUSTITUTO
                vlo_EntOtmSectorTaller = vlo_DalOtmSectorTaller.ObtenerRegistro(String.Format("{0} = {1} OR {2} = {1}",
                                  Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_COORDINADOR,
                                  pvo_Registro.NumEmpleadoCoordinador,
                                  Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_SUSTITUTO))

                If vlo_EntOtmSectorTaller.Existe Then
                    Throw New OrdenesDeTrabajoException("El coordinador ya está asociado a un taller o sector")
                End If

                If pvo_Registro.NumEmpleadoSustituto <> 0 Then

                    'Se verifica si el empleado sustituto se encuentra asignado a un taller
                    '{0}: Columna NUM_EMPLEADO_COORDINADOR
                    '{1}: Valor del empleado sustituto proveniente de la UI
                    '{2}: NUM_EMPLEADO_SUSTITUTO
                    vlo_EntOtmSectorTaller = vlo_DalOtmSectorTaller.ObtenerRegistro(String.Format("{0} = {1} OR {2} = {1}",
                                      Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_COORDINADOR,
                                      pvo_Registro.NumEmpleadoSustituto,
                                      Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_SUSTITUTO))

                    If vlo_EntOtmSectorTaller.Existe Then
                        Throw New OrdenesDeTrabajoException("El sustituto ya está asociado a un taller o sector")
                    End If
                End If

                'Cuando se verifica que los usuarios están libres de asociaciones se manda a modificar el registro
                vln_Resultado = vlo_DalOtmSectorTaller.InsertarRegistro(pvo_Registro)


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
        ''' Permite modificar un registro en la tabla OTM_SECTOR_TALLER, no sin antes aplicar la validación de la llave alterna
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:29:57 a.m.</creationDate>
        ''' <changeLog>
        '''    <autor>Cesar Bermudez Garcia</autor>
        '''    <fecha>03/02/2016</fecha>
        '''    <cambios>Verificar si el coordinador o el sustituto ya están asignados a un taller o sector</cambios>
        ''' </changeLog>
        Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmSectorTaller) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmSectorTaller As DalOtmSectorTaller
            Dim vlo_EntOtmSectorTaller As EntOtmSectorTaller
            Dim vln_Resultado As Integer
            Dim vlo_SectorTaller As EntOtmSectorTaller

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_EntOtmSectorTaller = ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacionAdministra, pvo_Registro.Nombre)
                If vlo_EntOtmSectorTaller.Existe AndAlso vlo_EntOtmSectorTaller.IdSectorTaller <> pvo_Registro.IdSectorTaller Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede modificar el registro, ya existe un Sector o Taller con el nombre indicado.")
                End If

                If pvo_Registro.NumEmpleadoCoordinador = pvo_Registro.NumEmpleadoSustituto Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede modificar el registro, el coordinador y el sustituto deben ser funcionarios diferentes.")
                End If

                vlo_EntOtmSectorTaller = ObtenerRegistroPorCoordinador(pvo_Registro.NumEmpleadoCoordinador)
                If vlo_EntOtmSectorTaller.Existe AndAlso vlo_EntOtmSectorTaller.IdSectorTaller <> pvo_Registro.IdSectorTaller Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede modificar el registro, ya existe un Sector o Taller con el coordinador indicado.")
                End If

                vlo_DalOtmSectorTaller = New DalOtmSectorTaller(vlo_Conexion)


                'Se obtiene el taller actual para validar los numeros de empleado coordinador y sustituto actuales
                '{0}: Columna NUM_EMPLEADO_COORDINADOR
                '{1}: Valor del empleado coordinador proveniente de la UI
                '{2}: NUM_EMPLEADO_SUSTITUTO
                vlo_SectorTaller = vlo_DalOtmSectorTaller.ObtenerRegistro(String.Format("{0} = {1}",
                                  Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER,
                                  pvo_Registro.IdSectorTaller))

                If vlo_SectorTaller.NumEmpleadoCoordinador <> pvo_Registro.NumEmpleadoCoordinador Then
                    'Se verifica si el empleado coordinador se encuentra asignado a un taller que no sea el mismo que se modificará
                    '{0}: Columna NUM_EMPLEADO_COORDINADOR
                    '{1}: Valor del empleado coordinador proveniente de la UI
                    '{2}: NUM_EMPLEADO_SUSTITUTO
                    vlo_EntOtmSectorTaller = vlo_DalOtmSectorTaller.ObtenerRegistro(String.Format("{0} = {1} OR {1} = {2}",
                                      Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_COORDINADOR,
                                      pvo_Registro.NumEmpleadoCoordinador,
                                      Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_SUSTITUTO,
                                      pvo_Registro.NumEmpleadoCoordinador))

                    If vlo_EntOtmSectorTaller.Existe Then
                        Throw New OrdenesDeTrabajoException("El coordinador ya está asociado a un taller o sector")
                    End If

                End If

                If vlo_SectorTaller.NumEmpleadoSustituto <> pvo_Registro.NumEmpleadoSustituto Then
                    ' si es cero, se modifica ya qu el sustituto puede ser vacio
                    If pvo_Registro.NumEmpleadoSustituto = 0 Then
                        'Cuando se verifica que los usuarios están libres de asociaciones se manda a modificar el registro
                        vln_Resultado = vlo_DalOtmSectorTaller.ModificarRegistro(pvo_Registro)
                    Else

                        'Se verifica si el empleado sustituto se encuentra asignado a un taller que no sea el mismo que se modificará
                        '{0}: Columna NUM_EMPLEADO_COORDINADOR
                        '{1}: Valor del empleado sustituto proveniente de la UI
                        '{2}: NUM_EMPLEADO_SUSTITUTO
                        vlo_EntOtmSectorTaller = vlo_DalOtmSectorTaller.ObtenerRegistro(String.Format("{0} = {1} OR {1} = {2}",
                                          Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_COORDINADOR,
                                          pvo_Registro.NumEmpleadoSustituto,
                                          Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_SUSTITUTO,
                                          pvo_Registro.NumEmpleadoSustituto))


                        If vlo_EntOtmSectorTaller.Existe Then
                            Throw New OrdenesDeTrabajoException("El sustituto ya está asociado a un taller o sector")

                        Else
                            'Cuando se verifica que los usuarios están libres de asociaciones se manda a modificar el registro
                            vln_Resultado = vlo_DalOtmSectorTaller.ModificarRegistro(pvo_Registro)
                        End If
                    End If
                Else
                    vln_Resultado = vlo_DalOtmSectorTaller.ModificarRegistro(pvo_Registro)
                End If

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
        ''' Permite borrar un registro en la tabla OTM_SECTOR_TALLER, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:29:57 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmSectorTaller) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmSectorTaller As DalOtmSectorTaller
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdSectorTaller) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("El registro no puede ser borrado ya que está asociado a órdenes de trabajo. Si lo desea, puede cambiar su estado a Inactivo desde la opción Modificar.")
                End If

                vlo_DalOtmSectorTaller = New DalOtmSectorTaller(vlo_Conexion)
                vln_Resultado = vlo_DalOtmSectorTaller.BorrarRegistro(pvo_Registro)
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
        ''' Permite obtener un registro según su llave alterna
        ''' </summary>
        ''' <param name="pvn_IdUbicacionAdministra">Id de la ubicación que administra los datos del catálogo</param>
        ''' <param name="pvc_Nombre">Nombre del sector</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>23/11/2015 09:01:19 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlaveAlterna(pvn_IdUbicacionAdministra As Integer, pvc_Nombre As String) As EntOtmSectorTaller
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmSectorTaller As DalOtmSectorTaller

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmSectorTaller = New DalOtmSectorTaller(vlo_Conexion)
                Return vlo_DalOtmSectorTaller.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTM_SECTOR_TALLER.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTM_SECTOR_TALLER.NOMBRE, pvc_Nombre.ToUpper()))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Permite comparar si el numero de empleado que se va a registrar ya esta asignado como coordinador de otro sector o taller
        ''' Autor: Mauricio Salas Chaves
        ''' Fecha: 11/09/215
        ''' </summary>
        ''' <param name="pvc_NumEmpleado"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ObtenerRegistroPorCoordinador(pvc_NumEmpleado As String) As EntOtmSectorTaller
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmSectorTaller As DalOtmSectorTaller

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmSectorTaller = New DalOtmSectorTaller(vlo_Conexion)
                Return vlo_DalOtmSectorTaller.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Modelo.OTM_SECTOR_TALLER.NUM_EMPLEADO_COORDINADOR, pvc_NumEmpleado.ToUpper()))
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
        ''' <param name="pvn_IdSectorTaller">Llave primaria de la tabla otm_sector_taller que se asocia con la secuencia sq_id_sector_taller</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>10/09/2015 10:29:57 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdSectorTaller As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOtmActividad As DalOtmActividad
            Dim vlo_DalOtmCategoriaServicio As DalOtmCategoriaServicio
            Dim vlo_DalOtmLugarTrabajo As DalOtmLugarTrabajo
            Dim vlo_DalOtfOperario As DalOtfOperario
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo

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

                'Determinar la existencia de registros asociados en la tabla OTM_ACTIVIDAD
                vlo_DalOtmActividad = New DalOtmActividad(vlo_Conexion)
                If vlo_DalOtmActividad.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_ACTIVIDAD.ID_SECTOR_TALLER, pvn_IdSectorTaller)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTM_CATEGORIA_SERVICIO
                vlo_DalOtmCategoriaServicio = New DalOtmCategoriaServicio(vlo_Conexion)
                If vlo_DalOtmCategoriaServicio.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_SERVICIO.ID_SECTOR_TALLER, pvn_IdSectorTaller)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTM_LUGAR_TRABAJO
                vlo_DalOtmLugarTrabajo = New DalOtmLugarTrabajo(vlo_Conexion)
                If vlo_DalOtmLugarTrabajo.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_LUGAR_TRABAJO.ID_SECTOR_TALLER, pvn_IdSectorTaller)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTF_OPERARIO
                vlo_DalOtfOperario = New DalOtfOperario(vlo_Conexion)
                If vlo_DalOtfOperario.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTF_OPERARIO.ID_SECTOR_TALLER, pvn_IdSectorTaller)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTF_ORDEN_TRABAJO
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                If vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_ORDEN_TRABAJO.ID_SECTOR_TALLER, pvn_IdSectorTaller)).Existe Then
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
