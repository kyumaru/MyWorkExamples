Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports System.Configuration
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttTrazabilidadProceso

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
        ''' Permite agregar un registro en la tabla OTT_TRAZABILIDAD_PROCESO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/09/2015 11:16:46 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttTrazabilidadProceso) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdTrazabilidadProceso).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
                End If

                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                vln_Resultado = vlo_DalOttTrazabilidadProceso.InsertarRegistro(pvo_Registro)
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
        ''' <param name="pvn_IdTrazabilidadProceso">Llave primaria de la tabla otl_trazabilidad_proceso que se asocia con la secuencia sq_id_trazabilidad_proceso</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/09/2015 11:16:46 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdTrazabilidadProceso As Integer) As EntOttTrazabilidadProceso
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)
                Return vlo_DalOttTrazabilidadProceso.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_TRAZABILIDAD_PROCESO.ID_TRAZABILIDAD_PROCESO, pvn_IdTrazabilidadProceso))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' carga los datos de la vista para lista de trazabilidad con responsable
        ''' </summary>
        ''' <param name="pvc_Condicion"></param>
        ''' <param name="pvc_Orden"></param>
        ''' <param name="pvb_Paginar"></param>
        ''' <param name="pvn_NumeroPagina"></param>
        ''' <param name="pvn_TamanoPagina"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>11/02/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarRegistrosListaPersonalizado(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As Data.DataSet
            Dim vlo_DsDatos As System.Data.DataSet
            Dim vlo_DsOperarioOrdenTrabajo As System.Data.DataSet
            Dim vlo_WsCatalogoVacaciones As WsrCatalogosVacaciones.WsCatalogosVacaciones
            Dim vlo_DalOtmSectorTaller As DalOtmSectorTaller
            Dim vlo_DalOtmCategoriaServicio As DalOtmCategoriaServicio
            Dim vlo_DalOttOperarioOrdenTrab As DalOttOperarioOrdenTrab
            Dim vlo_Estructura As WsrCatalogosVacaciones.PLM_ESTRUCTURA_ORG
            Dim vlc_EncargadoTramite As String = String.Empty
            Dim vlc_NombreJefe As String
            Dim vlc_Condicion As String
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsRegistros As Data.DataSet
            Dim vlo_DalOttTrazabilidadProceso As DalOttTrazabilidadProceso
            Dim vlc_EstadoOrden As String
            Dim vlc_CodigoSIRH As String
            Dim vlc_IdSectorTaller As String
            Dim vln_IdCategoriaServicio As String
            Dim vlc_NombreSolicitante As String
            Dim vlc_ListaUsuariosRoL As String()
            Dim vlo_EntEmpleados As WsrEU_Curriculo.EntEmpleados
            Dim vlb_PrimeraFila As Boolean = True

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttTrazabilidadProceso = New DalOttTrazabilidadProceso(vlo_Conexion)

                vlo_DsRegistros = vlo_DalOttTrazabilidadProceso.ListarRegistrosLista(
                    pvc_Condicion,
                    pvc_Orden,
                    pvb_Paginar,
                    pvn_NumeroPagina,
                    pvn_TamanoPagina)

                For i As Integer = 0 To vlo_DsRegistros.Tables(0).Rows.Count - 2

                    If i = 0 Then

                        vlc_EstadoOrden = vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString
                        vlc_CodigoSIRH = vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.COD_UNIDAD_SIRH).ToString
                        vlc_IdSectorTaller = vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_SECTOR_TALLER).ToString
                        vln_IdCategoriaServicio = CType(vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_CATEGORIA_SERVICIO), Integer)
                        vlc_NombreSolicitante = vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.NOMBRE_SOLICITANTE).ToString

                        ''DIRECTOR DE UNIDAD
                        If vlc_EstadoOrden = EstadoOrden.PENDIENTE_REVISION_DIRECTOR Then

                            vlo_WsCatalogoVacaciones = New WsrCatalogosVacaciones.WsCatalogosVacaciones
                            vlo_WsCatalogoVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
                            vlo_WsCatalogoVacaciones.Timeout = -1
                            vlo_WsCatalogoVacaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_CATALOGOS_VACACIONES)

                            vlc_NombreJefe = vlo_WsCatalogoVacaciones.PLM_ESTRUCTURA_ORG_ObtenerNombreJefeUnidad(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                            CType(vlc_CodigoSIRH, Integer))

                            vlc_Condicion = String.Format("COD_UNIDAD_SIRH = '{0}' AND TIPO = '{1}' AND ESTADO = '{2}'", vlc_CodigoSIRH, "UBC", Utilerias.OrdenesDeTrabajo.Estado.ACTIVO)

                            vlo_Estructura = vlo_WsCatalogoVacaciones.PLM_ESTRUCTURA_ORG_ObtenerRegistro(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                            vlc_Condicion)

                            vlc_EncargadoTramite = String.Format("{0}({1})", vlc_NombreJefe, vlo_Estructura.DESCRIPCION)

                            ''COORDINADOR SECTOR/TALLER
                        ElseIf vlc_EstadoOrden = EstadoOrden.ASIGNADA Or vlc_EstadoOrden = EstadoOrden.EN_PROCESO Or vlc_EstadoOrden = EstadoOrden.EN_ESTUDIO Or
                               vlc_EstadoOrden = EstadoOrden.EN_EVALUACION Or vlc_EstadoOrden = EstadoOrden.PARA_IMPRESION Or
                               vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_REVISION_COORD Or vlc_EstadoOrden = EstadoOrden.NO_CONFORME Or
                               vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_JEFATURA Or vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_DEVUELTO_JEFATURA Or
                               vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_REVISION_COORDINADOR Or
                               vlc_EstadoOrden = EstadoOrden.MATERIAL_PENDIENTE_COMPRA Or vlc_EstadoOrden = EstadoOrden.PARA_RETIRO_MATERIAL Then

                            vlc_Condicion = String.Format("UPPER({0}) = '{1}'", Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, vlc_IdSectorTaller)

                            vlo_DalOtmSectorTaller = New DalOtmSectorTaller(vlo_Conexion)

                            vlo_DsDatos = vlo_DalOtmSectorTaller.ListarRegistrosLista(
                                                    vlc_Condicion,
                                                    String.Empty,
                                                    False,
                                                    0,
                                                    0)

                            vlc_EncargadoTramite = String.Format("{0}({1})", vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE_COORDINADOR), vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE))

                            ''PROFESIONAL ENCARGADO
                        ElseIf vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_PENDIENTE Or vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_EVALUACION Or
                            vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_COORD Or vlc_EstadoOrden = EstadoOrden.CONTRATACION_ACLARACIONES Or
                            vlc_EstadoOrden = EstadoOrden.CONTRATACION_VISITA_TECNICA Or vlc_EstadoOrden = EstadoOrden.EN_ANTEPROYECTO Or
                            vlc_EstadoOrden = EstadoOrden.ANTEPROYECTO_APROBADO_SOLICITANTE Or vlc_EstadoOrden = EstadoOrden.ANTEPROYECTO_DEVUELTO_SOLICITANTE Or
                            vlc_EstadoOrden = EstadoOrden.ELABORACION_PRESUPUESTO Or vlc_EstadoOrden = EstadoOrden.ELABORACION_DE_PLANOS Or
                            vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_DEVUELTO_COORDINADOR Or vlc_EstadoOrden = EstadoOrden.SUPERVISION_OBRA Or
                            vlc_EstadoOrden = EstadoOrden.GESTION_CONTRATACION Then

                            vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)

                            vlc_Condicion = String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'",
                                                          Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION, vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_UBICACION).ToString,
                                                          Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO, vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_ORDEN_TRABAJO).ToString,
                                                          Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO, Cargo.ENCARGADO)

                            vlo_DsOperarioOrdenTrabajo = vlo_DalOttOperarioOrdenTrab.ListarRegistrosLista(vlc_Condicion, String.Format("{0} {1}", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA, Ordenamiento.DESCENDENTE), False, 0, 0)

                            If vlo_DsOperarioOrdenTrabajo.Tables(0) IsNot Nothing AndAlso vlo_DsOperarioOrdenTrabajo.Tables(0).Rows.Count > 0 Then
                                vlc_EncargadoTramite = vlo_DsOperarioOrdenTrabajo.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO).ToString
                            End If

                            ''SOLICITANTE
                        ElseIf vlc_EstadoOrden = EstadoOrden.RECIBIDO_CONFORME_SOLICITANTE Or vlc_EstadoOrden = EstadoOrden.PENDIENTE_DE_ENVIO Or
                            vlc_EstadoOrden = EstadoOrden.ANTEPROYECTO_PENDIENTE_REVISION_SOLICITANTE Or vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_APROBADA_JEFATURA Or
                            vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_APROBADO_JEFATURA Or vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_PENDIENTE_RESPUESTA_SOLICITANTE Or
                            vlc_EstadoOrden = EstadoOrden.PENDIENTE_RESPUESTA_SOLICITANTE Then

                            vlc_EncargadoTramite = vlc_NombreSolicitante

                            ''ENCARGADO DE CONTRATACIONES
                        ElseIf vlc_EstadoOrden = EstadoOrden.CONTRATACION_ADJUDICACION Or vlc_EstadoOrden = EstadoOrden.CONTRATACION_INICIO Or
                            vlc_EstadoOrden = EstadoOrden.CONTRATACION_OFERTAS Or vlc_EstadoOrden = EstadoOrden.CONTRATACION_PUBLICACION_CARTEL Or
                            vlc_EstadoOrden = EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE Or vlc_EstadoOrden = EstadoOrden.CONTRATACION_RECOMENDACION_TECNICA Or
                            vlc_EstadoOrden = EstadoOrden.PENDIENTE_REVISION_CONTRATACIONES Then

                            vlc_ListaUsuariosRoL = CargarUsuariosRol(RolesSistema.OT_ENCARGADO_CONTRATACION)

                            For Each vlc_User In vlc_ListaUsuariosRoL
                                vlo_EntEmpleados = CargarFuncionario(vlc_User)

                                If vlc_EncargadoTramite = String.Empty Then
                                    vlc_EncargadoTramite = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                                Else
                                    vlc_EncargadoTramite = String.Format("{0}, {1} {2} {3}", vlc_EncargadoTramite, vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                                End If

                            Next

                            ''JEFATURA
                        ElseIf vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_APROBADA_COORD Or vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_REVISION_JEFATURA Or
                             vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_APROBADO_COORDINADOR Or vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_REVISION_JEFATURA Or
                             vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_APROBADO_SOLICITANTE Or vlc_EstadoOrden = EstadoOrden.EVALUADA Then

                            vlc_ListaUsuariosRoL = CargarUsuariosRol(RolesSistema.OT_JEFE_SECCION)

                            For Each vlc_User In vlc_ListaUsuariosRoL
                                vlo_EntEmpleados = CargarFuncionario(vlc_User)

                                If vlc_EncargadoTramite = String.Empty Then
                                    vlc_EncargadoTramite = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                                Else
                                    vlc_EncargadoTramite = String.Format("{0}, {1} {2} {3}", vlc_EncargadoTramite, vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                                End If
                            Next

                            ''REQUISICIONES
                        ElseIf vlc_EstadoOrden = EstadoOrden.PENDIENTE_APROBACION_REQUISICION Then

                            vlc_ListaUsuariosRoL = CargarUsuariosRol(RolesSistema.OT_REVISOR_REQUISICIONES)

                            For Each vlc_User In vlc_ListaUsuariosRoL
                                vlo_EntEmpleados = CargarFuncionario(vlc_User)

                                If vlc_EncargadoTramite = String.Empty Then
                                    vlc_EncargadoTramite = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                                Else
                                    vlc_EncargadoTramite = String.Format("{0}, {1} {2} {3}", vlc_EncargadoTramite, vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                                End If
                            Next

                            ''SUPERVISOR
                        ElseIf vlc_EstadoOrden = EstadoOrden.REVISION_PRESUPUESTO_SUPERVISOR Then

                            vlc_Condicion = String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO, vln_IdCategoriaServicio)

                            vlo_DalOtmCategoriaServicio = New DalOtmCategoriaServicio(vlo_Conexion)

                            vlo_DsDatos = vlo_DalOtmCategoriaServicio.ListarRegistrosLista(
                                                    vlc_Condicion,
                                                    String.Empty,
                                                    False,
                                                    0,
                                                    0)

                            vlc_EncargadoTramite = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_CATEGORIA_SERVICIOLST.NOMBRE_EMPLEADO).ToString

                            ''LIQUIDADA
                        ElseIf vlc_EstadoOrden = EstadoOrden.LIQUIDADA Then

                            vlo_EntEmpleados = CargarFuncionario(vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.USUARIO).ToString)

                            If vlo_EntEmpleados.Existe Then
                                vlc_EncargadoTramite = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                            Else
                                vlc_EncargadoTramite = "SISTEMA"
                            End If

                        Else
                            vlc_EncargadoTramite = String.Empty
                        End If

                    Else

                        vlc_EstadoOrden = vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString
                        vlc_CodigoSIRH = vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.COD_UNIDAD_SIRH).ToString
                        vlc_IdSectorTaller = vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_SECTOR_TALLER).ToString
                        vln_IdCategoriaServicio = CType(vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_CATEGORIA_SERVICIO), Integer)
                        vlc_NombreSolicitante = vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.NOMBRE_SOLICITANTE).ToString

                        ''DIRECTOR DE UNIDAD
                        If vlc_EstadoOrden = EstadoOrden.PENDIENTE_REVISION_DIRECTOR Then

                            vlo_WsCatalogoVacaciones = New WsrCatalogosVacaciones.WsCatalogosVacaciones
                            vlo_WsCatalogoVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
                            vlo_WsCatalogoVacaciones.Timeout = -1
                            vlo_WsCatalogoVacaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_CATALOGOS_VACACIONES)

                            vlc_NombreJefe = vlo_WsCatalogoVacaciones.PLM_ESTRUCTURA_ORG_ObtenerNombreJefeUnidad(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                            CType(vlc_CodigoSIRH, Integer))

                            vlc_Condicion = String.Format("COD_UNIDAD_SIRH = '{0}' AND TIPO = '{1}' AND ESTADO = '{2}'", vlc_CodigoSIRH, "UBC", Utilerias.OrdenesDeTrabajo.Estado.ACTIVO)

                            vlo_Estructura = vlo_WsCatalogoVacaciones.PLM_ESTRUCTURA_ORG_ObtenerRegistro(
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                            ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                            vlc_Condicion)

                            vlc_EncargadoTramite = String.Format("{0}({1})", vlc_NombreJefe, vlo_Estructura.DESCRIPCION)

                            ''COORDINADOR SECTOR/TALLER
                        ElseIf vlc_EstadoOrden = EstadoOrden.ASIGNADA Or vlc_EstadoOrden = EstadoOrden.EN_PROCESO Or vlc_EstadoOrden = EstadoOrden.EN_ESTUDIO Or
                               vlc_EstadoOrden = EstadoOrden.EN_EVALUACION Or vlc_EstadoOrden = EstadoOrden.PARA_IMPRESION Or
                               vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_REVISION_COORD Or vlc_EstadoOrden = EstadoOrden.NO_CONFORME Or
                               vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_JEFATURA Or vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_DEVUELTO_JEFATURA Or
                               vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_REVISION_COORDINADOR Or
                               vlc_EstadoOrden = EstadoOrden.MATERIAL_PENDIENTE_COMPRA Or vlc_EstadoOrden = EstadoOrden.PARA_RETIRO_MATERIAL Then

                            vlc_Condicion = String.Format("UPPER({0}) = '{1}'", Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, vlc_IdSectorTaller)

                            vlo_DalOtmSectorTaller = New DalOtmSectorTaller(vlo_Conexion)

                            vlo_DsDatos = vlo_DalOtmSectorTaller.ListarRegistrosLista(
                                                    vlc_Condicion,
                                                    String.Empty,
                                                    False,
                                                    0,
                                                    0)

                            vlc_EncargadoTramite = String.Format("{0}({1})", vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE_COORDINADOR), vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE))

                            ''PROFESIONAL ENCARGADO
                        ElseIf vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_PENDIENTE Or vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_EVALUACION Or
                            vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_DEVUELTA_COORD Or vlc_EstadoOrden = EstadoOrden.CONTRATACION_ACLARACIONES Or
                            vlc_EstadoOrden = EstadoOrden.CONTRATACION_VISITA_TECNICA Or vlc_EstadoOrden = EstadoOrden.EN_ANTEPROYECTO Or
                            vlc_EstadoOrden = EstadoOrden.ANTEPROYECTO_APROBADO_SOLICITANTE Or vlc_EstadoOrden = EstadoOrden.ANTEPROYECTO_DEVUELTO_SOLICITANTE Or
                            vlc_EstadoOrden = EstadoOrden.ELABORACION_PRESUPUESTO Or vlc_EstadoOrden = EstadoOrden.ELABORACION_DE_PLANOS Or
                            vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_DEVUELTO_COORDINADOR Or vlc_EstadoOrden = EstadoOrden.SUPERVISION_OBRA Or
                            vlc_EstadoOrden = EstadoOrden.GESTION_CONTRATACION Then

                            vlo_DalOttOperarioOrdenTrab = New DalOttOperarioOrdenTrab(vlo_Conexion)

                            vlc_Condicion = String.Format("{0} = {1} AND {2} = '{3}' AND {4} = '{5}'",
                                                          Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_UBICACION, vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_UBICACION).ToString,
                                                          Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.ID_ORDEN_TRABAJO, vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_ORDEN_TRABAJO).ToString,
                                                          Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.CARGO, Cargo.ENCARGADO)

                            vlo_DsOperarioOrdenTrabajo = vlo_DalOttOperarioOrdenTrab.ListarRegistrosLista(vlc_Condicion, String.Format("{0} {1}", Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.FECHA_HASTA, Ordenamiento.DESCENDENTE), False, 0, 0)

                            If vlo_DsOperarioOrdenTrabajo.Tables(0) IsNot Nothing AndAlso vlo_DsOperarioOrdenTrabajo.Tables(0).Rows.Count > 0 Then
                                vlc_EncargadoTramite = vlo_DsOperarioOrdenTrabajo.Tables(0).Rows(0)(Modelo.V_OTT_OPERARIO_ORDEN_TRABLST.NOMBRE_EMPLEADO).ToString
                            End If

                            ''SOLICITANTE
                        ElseIf vlc_EstadoOrden = EstadoOrden.RECIBIDO_CONFORME_SOLICITANTE Or vlc_EstadoOrden = EstadoOrden.PENDIENTE_DE_ENVIO Or
                            vlc_EstadoOrden = EstadoOrden.ANTEPROYECTO_PENDIENTE_REVISION_SOLICITANTE Or vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_APROBADA_JEFATURA Or
                            vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_APROBADO_JEFATURA Or vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_PENDIENTE_RESPUESTA_SOLICITANTE Or
                            vlc_EstadoOrden = EstadoOrden.PENDIENTE_RESPUESTA_SOLICITANTE Then

                            vlc_EncargadoTramite = vlc_NombreSolicitante

                            ''ENCARGADO DE CONTRATACIONES
                        ElseIf vlc_EstadoOrden = EstadoOrden.CONTRATACION_ADJUDICACION Or vlc_EstadoOrden = EstadoOrden.CONTRATACION_INICIO Or
                            vlc_EstadoOrden = EstadoOrden.CONTRATACION_OFERTAS Or vlc_EstadoOrden = EstadoOrden.CONTRATACION_PUBLICACION_CARTEL Or
                            vlc_EstadoOrden = EstadoOrden.CONTRATACION_REVISIÓN_EXPEDIENTE Or vlc_EstadoOrden = EstadoOrden.CONTRATACION_RECOMENDACION_TECNICA Or
                            vlc_EstadoOrden = EstadoOrden.PENDIENTE_REVISION_CONTRATACIONES Then

                            vlo_EntEmpleados = CargarFuncionario(vlo_DsRegistros.Tables(0).Rows(i - 1)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.USUARIO).ToString)

                            vlc_EncargadoTramite = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)


                            ''JEFATURA
                        ElseIf vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_APROBADA_COORD Or vlc_EstadoOrden = EstadoOrden.EVALUACION_PRELIMINAR_REVISION_JEFATURA Or
                             vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_APROBADO_COORDINADOR Or vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_REVISION_JEFATURA Or
                             vlc_EstadoOrden = EstadoOrden.PRESUPUESTO_APROBADO_SOLICITANTE Or vlc_EstadoOrden = EstadoOrden.EVALUADA Then

                            vlo_EntEmpleados = CargarFuncionario(vlo_DsRegistros.Tables(0).Rows(i - 1)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.USUARIO).ToString)

                            vlc_EncargadoTramite = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)

                            ''REQUISICIONES
                        ElseIf vlc_EstadoOrden = EstadoOrden.PENDIENTE_APROBACION_REQUISICION Then

                            vlo_EntEmpleados = CargarFuncionario(vlo_DsRegistros.Tables(0).Rows(i - 1)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.USUARIO).ToString)

                            vlc_EncargadoTramite = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)

                            ''SUPERVISOR
                        ElseIf vlc_EstadoOrden = EstadoOrden.REVISION_PRESUPUESTO_SUPERVISOR Then

                            vlc_Condicion = String.Format("{0} = {1}", Modelo.OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO, vln_IdCategoriaServicio)

                            vlo_DalOtmCategoriaServicio = New DalOtmCategoriaServicio(vlo_Conexion)

                            vlo_DsDatos = vlo_DalOtmCategoriaServicio.ListarRegistrosLista(
                                                    vlc_Condicion,
                                                    String.Empty,
                                                    False,
                                                    0,
                                                    0)

                            vlc_EncargadoTramite = vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_CATEGORIA_SERVICIOLST.NOMBRE_EMPLEADO).ToString

                            ''LIQUIDADA
                        ElseIf vlc_EstadoOrden = EstadoOrden.LIQUIDADA Then

                            vlo_EntEmpleados = CargarFuncionario(vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.USUARIO).ToString)

                            If vlo_EntEmpleados.Existe Then
                                vlc_EncargadoTramite = String.Format("{0} {1} {2}", vlo_EntEmpleados.NOMBRE, vlo_EntEmpleados.APELLIDO1, vlo_EntEmpleados.APELLIDO2)
                            Else
                                vlc_EncargadoTramite = "SISTEMA"
                            End If

                        Else
                            vlc_EncargadoTramite = String.Empty
                        End If

                    End If

                    vlo_DsRegistros.Tables(0).Rows(i)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.RESPONSABLE) = vlc_EncargadoTramite
                    vlc_EncargadoTramite = String.Empty
                Next

                vlc_EstadoOrden = vlo_DsRegistros.Tables(0).Rows(vlo_DsRegistros.Tables(0).Rows.Count - 1)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ESTADO_ORDEN_TRABAJO).ToString

                If vlc_EstadoOrden = EstadoOrden.PENDIENTE_DE_ENVIO Then

                    vlo_DsRegistros.Tables(0).Rows(vlo_DsRegistros.Tables(0).Rows.Count - 1)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.RESPONSABLE) = vlo_DsRegistros.Tables(0).Rows(vlo_DsRegistros.Tables(0).Rows.Count - 1)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.NOMBRE_SOLICITANTE).ToString

                ElseIf vlc_EstadoOrden = EstadoOrden.PENDIENTE_REVISION_DIRECTOR Then

                    vlc_CodigoSIRH = vlo_DsRegistros.Tables(0).Rows(vlo_DsRegistros.Tables(0).Rows.Count - 1)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.COD_UNIDAD_SIRH).ToString

                    vlo_WsCatalogoVacaciones = New WsrCatalogosVacaciones.WsCatalogosVacaciones
                    vlo_WsCatalogoVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
                    vlo_WsCatalogoVacaciones.Timeout = -1
                    vlo_WsCatalogoVacaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_CATALOGOS_VACACIONES)

                    vlc_NombreJefe = vlo_WsCatalogoVacaciones.PLM_ESTRUCTURA_ORG_ObtenerNombreJefeUnidad(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    CType(vlc_CodigoSIRH, Integer))

                    vlc_Condicion = String.Format("COD_UNIDAD_SIRH = '{0}' AND TIPO = '{1}' AND ESTADO = '{2}'", vlc_CodigoSIRH, "UBC", Utilerias.OrdenesDeTrabajo.Estado.ACTIVO)

                    vlo_Estructura = vlo_WsCatalogoVacaciones.PLM_ESTRUCTURA_ORG_ObtenerRegistro(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    vlc_Condicion)

                    vlo_DsRegistros.Tables(0).Rows(vlo_DsRegistros.Tables(0).Rows.Count - 1)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.RESPONSABLE) = String.Format("{0}({1})", vlc_NombreJefe, vlo_Estructura.DESCRIPCION)

                ElseIf vlc_EstadoOrden = EstadoOrden.ASIGNADA Then

                    vlc_IdSectorTaller = vlo_DsRegistros.Tables(0).Rows(vlo_DsRegistros.Tables(0).Rows.Count - 1)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.ID_SECTOR_TALLER).ToString

                    vlc_Condicion = String.Format("UPPER({0}) = '{1}'", Modelo.OTM_SECTOR_TALLER.ID_SECTOR_TALLER, vlc_IdSectorTaller)

                    vlo_DalOtmSectorTaller = New DalOtmSectorTaller(vlo_Conexion)

                    vlo_DsDatos = vlo_DalOtmSectorTaller.ListarRegistrosLista(
                                            vlc_Condicion,
                                            String.Empty,
                                            False,
                                            0,
                                            0)

                    vlo_DsRegistros.Tables(0).Rows(vlo_DsRegistros.Tables(0).Rows.Count - 1)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.RESPONSABLE) = String.Format("{0}({1})", vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE_COORDINADOR), vlo_DsDatos.Tables(0).Rows(0).Item(Modelo.V_OTM_SECTOR_TALLERLST.NOMBRE))
                Else
                    vlo_DsRegistros.Tables(0).Rows(vlo_DsRegistros.Tables(0).Rows.Count - 1)(Modelo.V_OTT_TRAZABILIDAD_PROCESOLST.RESPONSABLE) = String.Empty
                End If

                Return vlo_DsRegistros

            Catch vlo_Exc As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Exc, Utilerias.OrdenesDeTrabajo.Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw
                End If
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
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
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>18/05/2016</creationDate>
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

#End Region

    End Class
End Namespace

