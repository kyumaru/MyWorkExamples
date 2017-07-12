Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttAnteproyecto
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
        ''' Permite agregar un registro en la tabla OTT_ANTEPROYECTO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>29/02/2016 11:00:10 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttAnteproyecto) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAnteproyecto As DalOttAnteproyecto
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo, pvo_Registro.Version).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("ERROR: Ya existe una versión registrada para el Anteproyecto")
                End If

                vlo_DalOttAnteproyecto = New DalOttAnteproyecto(vlo_Conexion)
                vln_Resultado = vlo_DalOttAnteproyecto.InsertarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTT_ANTEPROYECTO, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>29/02/2016 11:00:10 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOttAnteproyecto) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAnteproyecto As DalOttAnteproyecto
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdUbicacion, pvo_Registro.IdOrdenTrabajo, pvo_Registro.Version) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("ERROR: No es posible eliminar la información asociada")
                End If

                vlo_DalOttAnteproyecto = New DalOttAnteproyecto(vlo_Conexion)
                vln_Resultado = vlo_DalOttAnteproyecto.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvn_Version">Numero de version del anteproyecto asociado a una orden de trabajo</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>29/02/2016 11:00:10 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_Version As Integer) As EntOttAnteproyecto
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAnteproyecto As DalOttAnteproyecto

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttAnteproyecto = New DalOttAnteproyecto(vlo_Conexion)
                Return vlo_DalOttAnteproyecto.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}' AND {4} = {5}", Modelo.OTT_ANTEPROYECTO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_ANTEPROYECTO.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper(), Modelo.OTT_ANTEPROYECTO.VERSION, pvn_Version))
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
        ''' <param name="pvn_Version">Numero de version del anteproyecto asociado a una orden de trabajo</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>29/02/2016 11:00:10 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdUbicacion As Integer, pvc_IdOrdenTrabajo As String, pvn_Version As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOttDocumentoAnteproyect As DalOttDocumentoAnteproyect

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

                'Determinar la existencia de registros asociados en la tabla OTT_DOCUMENTO_ANTEPROYECT
                vlo_DalOttDocumentoAnteproyect = New DalOttDocumentoAnteproyect(vlo_Conexion)
                If vlo_DalOttDocumentoAnteproyect.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}' AND {4} = {5}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ORDEN_TRABAJO, pvc_IdOrdenTrabajo.ToUpper(), Modelo.OTT_DOCUMENTO_ANTEPROYECT.VERSION, pvn_Version)).Existe Then
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
        ''' Guarda la version del anteproyecto respectiva
        ''' </summary>
        ''' <param name="pvo_DsAdjuntosInsert"></param>
        ''' <param name="pvo_EntOttAnteproyecto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>10/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Function GuardarVersion(pvo_DsAdjuntosInsert As Data.DataSet, pvo_EntOttAnteproyecto As EntOttAnteproyecto, pvo_ArchivoPlantaFisica As EntOttAdjuntoOrdenTrabajo, pvo_ArchivoForesta As EntOttAdjuntoOrdenTrabajo, pvo_ArchivosBorrados As Data.DataSet) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAnteproyecto As DalOttAnteproyecto
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_DalOttDocumentoAnteproyect As DalOttDocumentoAnteproyect
            Dim vlo_EntOttAdjuntoOrdenTrabajo As EntOttAdjuntoOrdenTrabajo
            Dim vlo_EntOttAdjuntoOrdenTrabajoBorrar As EntOttAdjuntoOrdenTrabajo
            Dim vlo_EntOttDocumentoAnteproyectBorrar As EntOttDocumentoAnteproyect
            Dim vlo_DsCantidad As Data.DataSet
            Dim vlo_ArchivoF As EntOttAdjuntoOrdenTrabajo
            Dim vlo_ArchivoP As EntOttAdjuntoOrdenTrabajo
            Dim vlo_EntOtmUnidadTiempo As EntidadNegocio.Catalogos.EntOtmUnidadTiempo
            Dim vlo_DalOtmUnidadTiempo As AccesoDatos.Catalogos.DalOtmUnidadTiempo
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_EntOttOrdenTrabajo As EntOttOrdenTrabajo
            Dim vlo_DalOtpParametroUbicacion As AccesoDatos.Catalogos.DalOtpParametroUbicacion
            Dim vlo_NuevaFila As Data.DataRow
            Dim vlo_DsAdjuntos As Data.DataSet
            Dim vlo_DsDocumentosAnteproyecto As Data.DataSet
            Dim vlo_DsNuevosDocumentosAnteproyecto As Data.DataSet
            Dim vlo_DsArchivos As Data.DataSet
            Dim vlo_DsParametros As Data.DataSet
            Dim vlc_CorreoAdministrador As String
            Dim vlc_TiempoRespuesta As String
            Dim vlo_result As Integer = 1

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
                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DalOttDocumentoAnteproyect = New DalOttDocumentoAnteproyect(vlo_Conexion)
                vlo_DalOtpParametroUbicacion = New AccesoDatos.Catalogos.DalOtpParametroUbicacion(vlo_Conexion)
                vlo_DalOtmUnidadTiempo = New AccesoDatos.Catalogos.DalOtmUnidadTiempo(vlo_Conexion)

                'Se inserta la nueva version o se modifica la ya existente que sea editable

                If pvo_EntOttAnteproyecto.Existe Then
                    vlo_DalOttAnteproyecto = New DalOttAnteproyecto(vlo_Conexion)
                    vlo_DalOttAnteproyecto.ModificarRegistro(pvo_EntOttAnteproyecto)
                Else
                    vlo_result = InsertarRegistro(pvo_EntOttAnteproyecto)
                End If

                'Se buscan los datos de la tabla intermedia de documentos para el anteproyecto
                vlo_DsDocumentosAnteproyecto = vlo_DalOttDocumentoAnteproyect.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5} AND {6} = {7}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_UBICACION,
                                  pvo_EntOttAnteproyecto.IdUbicacion, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ORDEN_TRABAJO,
                                  pvo_EntOttAnteproyecto.IdOrdenTrabajo, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ETAPA_ORDEN_TRABAJO,
                                  EtapasOrdenTrabajo.ANTEPROYECTO, Modelo.OTT_DOCUMENTO_ANTEPROYECT.VERSION, pvo_EntOttAnteproyecto.Version), String.Empty, False, 0, 0)

                'Elimina del Dataset cada uno de los registros existentes
                For Each vlo_FilaTiempoOperario In vlo_DsDocumentosAnteproyecto.Tables(0).Rows
                    vlo_FilaTiempoOperario.Delete()
                Next

                'Se llama al adapter para borrar los antiguos registros 
                vlo_DalOttDocumentoAnteproyect.AdapterEvaluacionBorrar(vlo_DsDocumentosAnteproyecto)

                vlo_DsNuevosDocumentosAnteproyecto = vlo_DalOttDocumentoAnteproyect.ListarRegistros(String.Format("1=0"), String.Empty, False, 0, 0)

                'Se insertan/eliminan los documentos adjuntos respectivos
                If pvo_ArchivoPlantaFisica IsNot Nothing Then
                    If pvo_ArchivoPlantaFisica.Existe Then
                        If pvo_EntOttAnteproyecto.AvalPlantaFisica = 0 Then
                            vlo_result = vlo_DalOttAdjuntoOrdenTrabajo.BorrarRegistro(pvo_ArchivoPlantaFisica)
                        Else
                            If pvo_ArchivoPlantaFisica.Archivo IsNot Nothing Then
                                vlo_ArchivoF = vlo_DalOttAdjuntoOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION, pvo_ArchivoPlantaFisica.IdUbicacion, Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_ArchivoPlantaFisica.IdOrdenTrabajo, Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO, TipoDocumento.AVAL_PLANTA_FISICA))
                                vlo_ArchivoF.Usuario = pvo_EntOttAnteproyecto.Usuario
                                vlo_ArchivoF.Archivo = pvo_ArchivoPlantaFisica.Archivo
                                vlo_ArchivoF.NombreArchivo = pvo_ArchivoPlantaFisica.NombreArchivo

                                vlo_result = vlo_DalOttAdjuntoOrdenTrabajo.ModificarRegistro(vlo_ArchivoF)
                            End If
                        End If
                    Else
                        If pvo_ArchivoPlantaFisica.Archivo IsNot Nothing Then
                            pvo_ArchivoPlantaFisica.IdUbicacion = pvo_EntOttAnteproyecto.IdUbicacion
                            pvo_ArchivoPlantaFisica.IdTipoDocumento = TipoDocumento.AVAL_PLANTA_FISICA
                            pvo_ArchivoPlantaFisica.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.ANTEPROYECTO
                            pvo_ArchivoPlantaFisica.Descripcion = "Nota de solicitud para Aval de Planta Física"
                            pvo_ArchivoPlantaFisica.IdOrdenTrabajo = pvo_EntOttAnteproyecto.IdOrdenTrabajo
                            pvo_ArchivoPlantaFisica.Usuario = pvo_EntOttAnteproyecto.Usuario

                            vlo_result = vlo_DalOttAdjuntoOrdenTrabajo.InsertarRegistro(pvo_ArchivoPlantaFisica)
                        End If
                    End If
                End If

                If pvo_ArchivoForesta IsNot Nothing Then
                    If pvo_ArchivoForesta.Existe Then
                        If pvo_EntOttAnteproyecto.AvalForesta = 0 Then
                            vlo_result = vlo_DalOttAdjuntoOrdenTrabajo.BorrarRegistro(pvo_ArchivoForesta)
                        Else
                            If pvo_ArchivoForesta.Archivo IsNot Nothing Then
                                vlo_ArchivoP = vlo_DalOttAdjuntoOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION, pvo_ArchivoPlantaFisica.IdUbicacion, Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_ArchivoPlantaFisica.IdOrdenTrabajo, Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO, TipoDocumento.AVAL_FORESTA))
                                vlo_ArchivoP.Usuario = pvo_EntOttAnteproyecto.Usuario
                                vlo_ArchivoP.Archivo = pvo_ArchivoForesta.Archivo
                                vlo_ArchivoP.NombreArchivo = pvo_ArchivoForesta.NombreArchivo
                                vlo_result = vlo_DalOttAdjuntoOrdenTrabajo.ModificarRegistro(vlo_ArchivoP)
                            End If
                        End If
                    Else
                        If pvo_ArchivoForesta.Archivo IsNot Nothing Then
                            pvo_ArchivoForesta.IdUbicacion = pvo_EntOttAnteproyecto.IdUbicacion
                            pvo_ArchivoForesta.IdTipoDocumento = TipoDocumento.AVAL_FORESTA
                            pvo_ArchivoForesta.IdEtapaOrdentrabajo = EtapasOrdenTrabajo.ANTEPROYECTO
                            pvo_ArchivoForesta.Descripcion = "Nota de solicitud para Aval de Foresta"
                            pvo_ArchivoForesta.IdOrdenTrabajo = pvo_EntOttAnteproyecto.IdOrdenTrabajo
                            pvo_ArchivoForesta.Usuario = pvo_EntOttAnteproyecto.Usuario
                            vlo_result = vlo_DalOttAdjuntoOrdenTrabajo.InsertarRegistro(pvo_ArchivoForesta)
                        End If
                    End If
                End If

                'Carga la estructura del listado de archivos
                vlo_DsArchivos = vlo_DalOttAdjuntoOrdenTrabajo.ListarRegistrosLista("0=1", String.Empty, False, 0, 0)

                For Each vlo_filaBorrar As Data.DataRow In pvo_ArchivosBorrados.Tables(0).Rows
                    'Verificar que no hallan archivos asociados a la tabla en versiones anteriores
                    vlo_DsDocumentosAnteproyecto = vlo_DalOttDocumentoAnteproyect.ListarRegistros(String.Format("{0} = {1}",
                    Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO, vlo_filaBorrar.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ADJUNTO_ORDEN_TRABAJO)), String.Empty, False, 0, 0)

                    'Si existe al menos un registro relacionado, no se borra
                    'Pero si se eliminará 
                    If vlo_DsDocumentosAnteproyecto.Tables(0).Rows.Count <= 0 Then
                        vlo_EntOttAdjuntoOrdenTrabajo = New EntOttAdjuntoOrdenTrabajo
                        vlo_EntOttAdjuntoOrdenTrabajo.IdAdjuntoOrdenTrabajo = vlo_filaBorrar.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ADJUNTO_ORDEN_TRABAJO)
                        vlo_result = vlo_DalOttAdjuntoOrdenTrabajo.BorrarRegistro(vlo_EntOttAdjuntoOrdenTrabajo)

                    End If

                Next

                For Each vlo_fila As Data.DataRow In pvo_DsAdjuntosInsert.Tables(0).Rows
                    'Si el archivo NO es nulo se ingresa en la tabla
                    If Not TypeOf vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ARCHIVO) Is DBNull Then
                        vlo_NuevaFila = vlo_DsArchivos.Tables(0).NewRow
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESCRIPCION) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.DESCRIPCION)
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ETAPA_ORDEN_TRABAJO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ETAPA_ORDEN_TRABAJO)
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_TIPO_DOCUMENTO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_TIPO_DOCUMENTO)
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_UBICACION) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_UBICACION)
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.NOMBRE_ARCHIVO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.NOMBRE_ARCHIVO)
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ARCHIVO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ARCHIVO)
                        vlo_NuevaFila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.USUARIO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.USUARIO)

                        vlo_DsArchivos.Tables(0).Rows.Add(vlo_NuevaFila)
                    End If
                Next

                'Se insertan los cambios del listado de archivos
                If vlo_DsArchivos.Tables.Count > 0 AndAlso vlo_DsArchivos.Tables(0).Rows.Count > 0 Then
                    vlo_DalOttAdjuntoOrdenTrabajo.AdapterOtTAdjunto(vlo_DsArchivos)
                End If

                'Se obtienen los datos ingresados para obtener el ID_ADJUNTO_ORDEN_TRABAJO
                '{0}: Columna ID_UBICACION
                '{1}: id ubicacion de la OT
                '{2}: columna ID_ORDEN_TRABAJO
                '{3}: valor del id orden trabajo
                '{4}: columna ID_ETAPA_ORDEN_TRABAJO
                '{5}: Etapa anteproyecto
                vlo_DsAdjuntos = vlo_DalOttAdjuntoOrdenTrabajo.ListarRegistros(
                    String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION,
                                  pvo_EntOttAnteproyecto.IdUbicacion, Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO,
                                  pvo_EntOttAnteproyecto.IdOrdenTrabajo, Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ETAPA_ORDEN_TRABAJO,
                                  EtapasOrdenTrabajo.ANTEPROYECTO), String.Empty, False, 0, 0)

                'Cargar la estructura básica de la tabla intermedia
                '{0}: Columna ID_UBICACION
                '{1}: id ubicacion de la OT
                '{2}: columna ID_ORDEN_TRABAJO
                '{3}: valor del id orden trabajo
                '{4}: columna ID_ETAPA_ORDEN_TRABAJO
                '{5}: Etapa anteproyecto
                '{6}: columna version
                '{7}: numero de version del anteproyecto

                'Prepara la tabla con datos a ser insertados
                For Each vlo_fila As Data.DataRow In vlo_DsAdjuntos.Tables(0).Rows
                    vlo_NuevaFila = vlo_DsNuevosDocumentosAnteproyecto.Tables(0).NewRow
                    vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_TIPO_DOCUMENTO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_TIPO_DOCUMENTO)
                    vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ETAPA_ORDEN_TRABAJO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ETAPA_ORDEN_TRABAJO)
                    vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ADJUNTO_ORDEN_TRABAJO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ADJUNTO_ORDEN_TRABAJO)
                    vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_UBICACION) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_UBICACION)
                    vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ORDEN_TRABAJO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)
                    vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.VERSION) = pvo_EntOttAnteproyecto.Version
                    vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.USUARIO) = pvo_EntOttAnteproyecto.Usuario
                    vlo_DsNuevosDocumentosAnteproyecto.Tables(0).Rows.Add(vlo_NuevaFila)
                Next

                'Se ingresan las referencias de documentos para esta version en la tabla intermedia
                vlo_DalOttDocumentoAnteproyect.AdapterAdjuntosAnteproyecto(vlo_DsNuevosDocumentosAnteproyecto)

                For Each vlo_FilaArchivoBorrado In pvo_ArchivosBorrados.Tables(0).Rows
                    vlo_EntOttAdjuntoOrdenTrabajoBorrar = vlo_DalOttAdjuntoOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO, vlo_FilaArchivoBorrado(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO)))
                    vlo_EntOttDocumentoAnteproyectBorrar = vlo_DalOttDocumentoAnteproyect.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO, vlo_FilaArchivoBorrado(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO), Modelo.OTT_DOCUMENTO_ANTEPROYECT.VERSION, pvo_EntOttAnteproyecto.Version))
                    vlo_DsCantidad = vlo_DalOttDocumentoAnteproyect.ListarRegistros(String.Format("{0} = {1}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO, vlo_FilaArchivoBorrado(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO)), String.Empty, False, 0, 0)

                    If vlo_EntOttDocumentoAnteproyectBorrar.Existe Then
                        vlo_DalOttDocumentoAnteproyect.BorrarRegistro(vlo_EntOttDocumentoAnteproyectBorrar)
                    End If

                    If vlo_EntOttAdjuntoOrdenTrabajoBorrar.Existe And (vlo_DsCantidad.Tables(0).Rows.Count = 1) Then
                        vlo_DalOttAdjuntoOrdenTrabajo.BorrarRegistro(vlo_EntOttAdjuntoOrdenTrabajoBorrar)
                    End If
                Next

                'Si no es editable
                If pvo_EntOttAnteproyecto.Editable = 0 Then

                    '{0}: Columna ID_UBICACION
                    '{1}: id ubicacion de la OT
                    '{2}: columna ID_ORDEN_TRABAJO
                    '{3}: valor del id orden trabajo
                    vlo_EntOttOrdenTrabajo = vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'",
                                            Modelo.OTT_ORDEN_TRABAJO.ID_UBICACION, pvo_EntOttAnteproyecto.IdUbicacion,
                                            Modelo.OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO, pvo_EntOttAnteproyecto.IdOrdenTrabajo))
                    If vlo_EntOttOrdenTrabajo.Existe Then
                        vlo_EntOttOrdenTrabajo.EstadoOrdenTrabajo = EstadoOrden.ANTEPROYECTO_PENDIENTE_REVISION_SOLICITANTE
                        vlo_result = vlo_DalOttOrdenTrabajo.ModificarRegistro(vlo_EntOttOrdenTrabajo)

                        'Obtiene el correo del administrador del sistema
                        vlo_DsParametros = vlo_DalOtpParametroUbicacion.ListarRegistrosLista(String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.DIRECCION_CORREO_ADMINISTRADOR),
                                                                                          String.Empty, False, 0, 0)

                        If vlo_DsParametros.Tables.Count > 0 AndAlso vlo_DsParametros.Tables(0).Rows.Count > 0 Then
                            vlc_CorreoAdministrador = vlo_DsParametros.Tables(0).Rows(0).Item(Modelo.OTP_PARAMETRO_UBICACION.VALOR)

                            'obtiene la descripcion para el restante de tiempo de respuesta del solicitante
                            vlo_EntOtmUnidadTiempo = vlo_DalOtmUnidadTiempo.ObtenerRegistro(
                                String.Format("{0} = {1}", Modelo.OTM_UNIDAD_TIEMPO.ID_UNIDAD_TIEMPO, pvo_EntOttAnteproyecto.IdUnidadTiempo))

                            If pvo_EntOttAnteproyecto.TiempoRespuesta = 1 Then
                                vlc_TiempoRespuesta = String.Format("{0} {1}", pvo_EntOttAnteproyecto.TiempoRespuesta, vlo_EntOtmUnidadTiempo.Descripcion)
                            Else
                                vlc_TiempoRespuesta = String.Format("{0} {1}S", pvo_EntOttAnteproyecto.TiempoRespuesta, vlo_EntOtmUnidadTiempo.Descripcion)
                            End If

                            EnviarCorreoNotificacion(vlo_EntOttOrdenTrabajo.NombreProyecto, vlo_EntOttOrdenTrabajo.NumEmpleado, vlo_EntOttOrdenTrabajo.IdOrdenTrabajo, vlc_TiempoRespuesta, vlc_CorreoAdministrador)
                        End If
                    End If
                End If

                vlo_Conexion.TransaccionCommit()

                Return vlo_result

            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
                If vlo_DsAdjuntos IsNot Nothing Then
                    vlo_DsAdjuntos.Dispose()
                End If
                If vlo_DsDocumentosAnteproyecto IsNot Nothing Then
                    vlo_DsDocumentosAnteproyecto.Dispose()
                End If
                If vlo_DsNuevosDocumentosAnteproyecto IsNot Nothing Then
                    vlo_DsNuevosDocumentosAnteproyecto.Dispose()
                End If
                If vlo_DsArchivos IsNot Nothing Then
                    vlo_DsArchivos.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Genera una nueva versión del anteproyecto como una copia del anterior
        ''' </summary>
        ''' <param name="pvo_EntOttAnteproyecto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>10/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function GenerarNuevaVersion(pvo_EntOttAnteproyecto As EntOttAnteproyecto)
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_DalOttDocumentoAnteproyect As DalOttDocumentoAnteproyect
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vlo_NuevaFila As Data.DataRow
            Dim vlo_DsAdjuntos As Data.DataSet
            Dim vlo_DsDocumentosAnteproyecto As Data.DataSet
            Dim vlo_DsNuevosDocumentosAnteproyecto As Data.DataSet
            Dim vlo_DsArchivos As Data.DataSet
            Dim vlo_result As Integer = 1

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
                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DalOttDocumentoAnteproyect = New DalOttDocumentoAnteproyect(vlo_Conexion)

                'Se buscan los datos de la tabla intermedia de documentos para la última version no editable
                vlo_DsDocumentosAnteproyecto = vlo_DalOttDocumentoAnteproyect.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5} AND {6} = {7}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_UBICACION,
                                  pvo_EntOttAnteproyecto.IdUbicacion, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ORDEN_TRABAJO,
                                  pvo_EntOttAnteproyecto.IdOrdenTrabajo, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ETAPA_ORDEN_TRABAJO,
                                  EtapasOrdenTrabajo.ANTEPROYECTO, Modelo.OTT_DOCUMENTO_ANTEPROYECT.VERSION, pvo_EntOttAnteproyecto.Version), String.Empty, False, 0, 0)

                'Estructura del listado
                vlo_DsNuevosDocumentosAnteproyecto = vlo_DalOttDocumentoAnteproyect.ListarRegistros("1 = 0", String.Empty, False, 0, 0)

                'Se inserta la nueva version en modo editable

                pvo_EntOttAnteproyecto.Version = pvo_EntOttAnteproyecto.Version + 1
                pvo_EntOttAnteproyecto.Editable = 1

                InsertarRegistro(pvo_EntOttAnteproyecto)

                For Each vlo_fila As Data.DataRow In vlo_DsDocumentosAnteproyecto.Tables(0).Rows
                    vlo_NuevaFila = vlo_DsNuevosDocumentosAnteproyecto.Tables(0).NewRow
                    vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_TIPO_DOCUMENTO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_TIPO_DOCUMENTO)
                    vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ETAPA_ORDEN_TRABAJO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ETAPA_ORDEN_TRABAJO)
                    vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ADJUNTO_ORDEN_TRABAJO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ADJUNTO_ORDEN_TRABAJO)
                    vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_UBICACION) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_UBICACION)
                    vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.ID_ORDEN_TRABAJO) = vlo_fila.Item(Modelo.V_OTT_ADJUNTO_ORDEN_TRABAJOLST.ID_ORDEN_TRABAJO)
                    vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.VERSION) = pvo_EntOttAnteproyecto.Version
                    vlo_NuevaFila.Item(Modelo.V_OTT_DOCUMENTO_ANTEPROYECTLST.USUARIO) = pvo_EntOttAnteproyecto.Usuario
                    vlo_DsNuevosDocumentosAnteproyecto.Tables(0).Rows.Add(vlo_NuevaFila)
                Next

                'Se ingresan las referencias de documentos para esta version en la tabla intermedia
                vlo_DalOttDocumentoAnteproyect.AdapterAdjuntosAnteproyecto(vlo_DsNuevosDocumentosAnteproyecto)

                vlo_Conexion.TransaccionCommit()

                Return vlo_result

            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
                If vlo_DsAdjuntos IsNot Nothing Then
                    vlo_DsAdjuntos.Dispose()
                End If
                If vlo_DsDocumentosAnteproyecto IsNot Nothing Then
                    vlo_DsDocumentosAnteproyecto.Dispose()
                End If
                If vlo_DsNuevosDocumentosAnteproyecto IsNot Nothing Then
                    vlo_DsNuevosDocumentosAnteproyecto.Dispose()
                End If
                If vlo_DsArchivos IsNot Nothing Then
                    vlo_DsArchivos.Dispose()
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
        ''' <creationDate>10/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Function EnviarCorreoNotificacion(pvc_NombreProyecto As String, pvc_idSolicitante As String, pvc_idOrden As String, pvc_TiempoReal As String, pvc_CorreoAdministrador As String) As Integer
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
                    vlo_Empleado = CargarFuncionario(pvc_idSolicitante)
                    If Not String.IsNullOrWhiteSpace(vlo_Empleado.CORREO_INSTITUCIONAL) Then
                        '{0}: Numero de orden de trabajo
                        vlo_Notificacion.ASUNTO = String.Format("Revisión de Anteproyecto para la orden de trabajo N° {0}", pvc_idOrden)

                        '{0}: Nombre del profesional
                        '{1}: Apellido 1 del profesional
                        '{2}: Apellido 2 del profesional
                        '{3}: Nombre del proyecto
                        '{4}: Estimación de tiempo
                        '{5}: Correo del administrador del sistema

                        vlo_Notificacion.CUERPO = String.Format("<b>Señor(a):{0} {1} {2}</b><br /><br /><b>Estimado(a) señor(a)</b><br />Se le notifica que se ha generado un Anteproyecto asociado al proyecto: {3}, para lo cual se requiere de su revisión.<br /><br />Le solicitamos proceder a revisar el Anteproyecto en el sistema de Órdenes de Trabajo, para lo cual  usted dispone de {4} para dar respuesta, vencido el plazo su solicitud será liquidada.<br /><hr><i>Por favor no responda este correo ya que se utiliza únicamente para el envío de notificaciones. Si desea comunicarse con el administrador del sistema refiérase a la dirección: mantenimientoyconstruccion@ucr.ac.cr</i>",
                                           vlo_Empleado.NOMBRE, vlo_Empleado.APELLIDO1, vlo_Empleado.APELLIDO2, pvc_NombreProyecto, pvc_TiempoReal, pvc_CorreoAdministrador)
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
        ''' Carga el empleado, segun la identificacion personal que obtenga or parametro
        ''' </summary>
        ''' <param name="pvc_idPersonal"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>10/03/2016</creationDate>
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

#End Region


    End Class
End Namespace
