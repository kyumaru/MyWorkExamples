Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
    Public Class BllOtmLugarTrabajo
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
        ''' Permite agregar un registro en la tabla OTM_LUGAR_TRABAJO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
        ''' <changeLog>
        ''' <autor>Cesar Bermudez G</autor>
        ''' <fecha>14/03/2016</fecha>
        ''' <cambio>Se agrega funcionalidad para agregar datos a la tabla OTM_UNIDAD_ENCARGADA</cambio>
        ''' </changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmLugarTrabajo, pvo_DsUnidades As Data.DataTable) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmLugarTrabajo As DalOtmLugarTrabajo
            Dim vlo_DalOtmUnidadEncargada As DalOtmUnidadEncargada
            Dim vln_Resultado As Integer
            Dim vlo_DsUnidadesEncargadas As Data.DataSet
            Dim vlo_NuevaFila As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                If ObtenerRegistroPorLlaveAlterna(pvo_Registro.Nombre).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede agregar el registro, ya existe un lugar de trabajo con el nombre indicado.")
                End If

                'Se inicializan los objectos de acceso a datos
                vlo_DalOtmUnidadEncargada = New DalOtmUnidadEncargada(vlo_Conexion)
                vlo_DalOtmLugarTrabajo = New DalOtmLugarTrabajo(vlo_Conexion)

                vln_Resultado = vlo_DalOtmLugarTrabajo.InsertarRegistro(pvo_Registro)

                vlo_DsUnidadesEncargadas = vlo_DalOtmUnidadEncargada.ListarRegistros("0=1", String.Empty, False, 0, 0)

                'Si se inserta correctamente
                If vln_Resultado > 0 Then

                    For Each vlo_fila As Data.DataRow In pvo_DsUnidades.Rows
                        vlo_NuevaFila = vlo_DsUnidadesEncargadas.Tables(0).NewRow
                        vlo_NuevaFila.Item(Modelo.V_OTM_UNIDAD_ENCARGADA.COD_UNIDAD_SIRH) = vlo_fila.Item("CODIGO_UBICA")
                        vlo_NuevaFila.Item(Modelo.V_OTM_UNIDAD_ENCARGADA.ID_LUGAR_TRABAJO) = vln_Resultado
                        vlo_NuevaFila.Item(Modelo.V_OTM_UNIDAD_ENCARGADA.USUARIO) = pvo_Registro.Usuario
                        vlo_DsUnidadesEncargadas.Tables(0).Rows.Add(vlo_NuevaFila)
                    Next


                    vlo_DalOtmUnidadEncargada.AdapterUnidadEncargada(vlo_DsUnidadesEncargadas)
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
        ''' Permite modificar un registro en la tabla OTM_LUGAR_TRABAJO, no sin antes aplicar la validación de la llave alterna
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
        ''' <changeLog>
        ''' <autor>Cesar Bermudez G</autor>
        ''' <fecha>14/03/2016</fecha>
        ''' <cambio>Se agrega funcionalidad para agregar datos a la tabla OTM_UNIDAD_ENCARGADA</cambio>
        ''' </changeLog>
        Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmLugarTrabajo, pvo_DsUnidades As Data.DataTable) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmLugarTrabajo As DalOtmLugarTrabajo
            Dim vlo_DalOtmUnidadEncargada As DalOtmUnidadEncargada
            Dim vlo_EntOtmLugarTrabajo As EntOtmLugarTrabajo
            Dim vln_Resultado As Integer
            Dim vlo_DsUnidadesEncargadas As Data.DataSet
            Dim vlo_DsNuevasUnidadesEncargadas As Data.DataSet
            Dim vlo_NuevaFila As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_EntOtmLugarTrabajo = ObtenerRegistroPorLlaveAlterna(pvo_Registro.Nombre)
                If vlo_EntOtmLugarTrabajo.Existe AndAlso vlo_EntOtmLugarTrabajo.IdLugarTrabajo <> pvo_Registro.IdLugarTrabajo Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede modificar el registro, ya existe un lugar de trabajo con el nombre indicado.")
                End If

                vlo_DalOtmLugarTrabajo = New DalOtmLugarTrabajo(vlo_Conexion)
                vlo_DalOtmUnidadEncargada = New DalOtmUnidadEncargada(vlo_Conexion)

                vln_Resultado = vlo_DalOtmLugarTrabajo.ModificarRegistro(pvo_Registro)

                vlo_DsUnidadesEncargadas = vlo_DalOtmUnidadEncargada.ListarRegistros(String.Format("{0} = {1}", Modelo.OTM_UNIDAD_ENCARGADA.ID_LUGAR_TRABAJO, pvo_Registro.IdLugarTrabajo), String.Empty, False, 0, 0)

                For Each vlo_fila In vlo_DsUnidadesEncargadas.Tables(0).Rows
                    vlo_fila.delete()
                Next

                vlo_DalOtmUnidadEncargada.AdapterUnidadEncargada(vlo_DsUnidadesEncargadas)

                vlo_DsNuevasUnidadesEncargadas = vlo_DalOtmUnidadEncargada.ListarRegistros("0=1", String.Empty, False, 0, 0)

                'Si se inserta correctamente
                If vln_Resultado > 0 Then

                    For Each vlo_fila As Data.DataRow In pvo_DsUnidades.Rows
                        vlo_NuevaFila = vlo_DsNuevasUnidadesEncargadas.Tables(0).NewRow
                        vlo_NuevaFila.Item(Modelo.OTM_UNIDAD_ENCARGADA.COD_UNIDAD_SIRH) = vlo_fila.Item("CODIGO_UBICA")
                        vlo_NuevaFila.Item(Modelo.OTM_UNIDAD_ENCARGADA.ID_LUGAR_TRABAJO) = pvo_Registro.IdLugarTrabajo
                        vlo_NuevaFila.Item(Modelo.OTM_UNIDAD_ENCARGADA.USUARIO) = pvo_Registro.Usuario
                        vlo_DsNuevasUnidadesEncargadas.Tables(0).Rows.Add(vlo_NuevaFila)
                    Next

                    vlo_DalOtmUnidadEncargada.AdapterUnidadEncargada(vlo_DsNuevasUnidadesEncargadas)
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
        ''' Permite borrar un registro en la tabla OTM_LUGAR_TRABAJO, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmLugarTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmLugarTrabajo As DalOtmLugarTrabajo
            Dim vlo_DalOtmUnidadEncargada As DalOtmUnidadEncargada
            Dim vlo_DsUnidadesEncargadas As Data.DataSet

            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdLugarTrabajo) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("El registro no puede ser borrado ya que posee información de órdenes de trabajo. Si lo desea, puede cambiar su estado a Inactivo desde la opción Modificar.")
                End If

                vlo_DalOtmUnidadEncargada = New DalOtmUnidadEncargada(vlo_Conexion)

                vlo_DsUnidadesEncargadas = vlo_DalOtmUnidadEncargada.ListarRegistros(String.Format("{0} = {1}", Modelo.OTM_UNIDAD_ENCARGADA.ID_LUGAR_TRABAJO, pvo_Registro.IdLugarTrabajo), String.Empty, False, 0, 0)

                For Each vlo_fila In vlo_DsUnidadesEncargadas.Tables(0).Rows
                    vlo_fila.delete()
                Next

                vlo_DalOtmUnidadEncargada.AdapterUnidadEncargada(vlo_DsUnidadesEncargadas)

                vlo_DalOtmLugarTrabajo = New DalOtmLugarTrabajo(vlo_Conexion)
                vln_Resultado = vlo_DalOtmLugarTrabajo.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvc_Nombre">Nombre del lugar de trabajo</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlaveAlterna(pvc_Nombre As String) As EntOtmLugarTrabajo
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmLugarTrabajo As DalOtmLugarTrabajo

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmLugarTrabajo = New DalOtmLugarTrabajo(vlo_Conexion)
                Return vlo_DalOtmLugarTrabajo.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Modelo.OTM_LUGAR_TRABAJO.NOMBRE, pvc_Nombre.ToUpper()))
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
        ''' <param name="pvn_IdLugarTrabajo">Llave primaria de la tabla otm_lugar_trabajo que se asocia con la secuencia sq_id_lugar_trabajo</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>08/09/2015 01:51:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdLugarTrabajo As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
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

                'Determinar la existencia de registros asociados en la tabla OTF_ORDEN_TRABAJO
                vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                If vlo_DalOttOrdenTrabajo.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_ORDEN_TRABAJO.ID_LUGAR_TRABAJO, pvn_IdLugarTrabajo)).Existe Then
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
