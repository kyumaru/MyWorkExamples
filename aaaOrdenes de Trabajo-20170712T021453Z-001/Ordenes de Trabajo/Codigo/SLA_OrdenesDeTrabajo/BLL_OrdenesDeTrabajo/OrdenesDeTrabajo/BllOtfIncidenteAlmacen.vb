Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOtfIncidenteAlmacen
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
        ''' Permite agrefgar un registro en la tabla OTF_INCIDENTE_ALMACEN, y n en los documentos adjuntos de la incidencia
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>08/08/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistroConAdjuntos(ByVal pvo_Registro As EntOtfIncidenteAlmacen, vlo_DsAdjuntos As Data.DataSet) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfIncidenteAlmacen As DalOtfIncidenteAlmacen
            Dim vlo_DalOtfAdjuntoIncidente As DalOtfAdjuntoIncidente
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                'inicia la transaccion
                vlo_Conexion.TransaccionBegin()

                vlo_DalOtfIncidenteAlmacen = New DalOtfIncidenteAlmacen(vlo_Conexion)
                vlo_DalOtfAdjuntoIncidente = New DalOtfAdjuntoIncidente(vlo_Conexion)

                vln_Resultado = vlo_DalOtfIncidenteAlmacen.InsertarRegistro(pvo_Registro)

                For Each vlo_Fila In vlo_DsAdjuntos.Tables(0).Rows
                    vlo_Fila(Modelo.OTF_ADJUNTO_INCIDENTE.ID_INCIDENTE_ALMACEN) = vln_Resultado
                Next

                vlo_DalOtfAdjuntoIncidente.AdapterOtfAdjunto(vlo_DsAdjuntos)

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
        ''' Permite modificar un registro en la tabla OTF_INCIDENTE_ALMACEN, y n en los documentos adjuntos de la incidencia
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>08/08/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarRegistroConAdjuntos(ByVal pvo_Registro As EntOtfIncidenteAlmacen, vlo_DsAdjuntos As Data.DataSet) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfIncidenteAlmacen As DalOtfIncidenteAlmacen
            Dim vlo_DalOtfAdjuntoIncidente As DalOtfAdjuntoIncidente
            Dim vlo_DsExistente As Data.DataSet
            Dim vlo_DsNuevo As Data.DataSet
            Dim vln_Resultado As Integer
            Dim vlo_FilaNueva As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                'inicia la transaccion
                vlo_Conexion.TransaccionBegin()

                vlo_DalOtfIncidenteAlmacen = New DalOtfIncidenteAlmacen(vlo_Conexion)
                vlo_DalOtfAdjuntoIncidente = New DalOtfAdjuntoIncidente(vlo_Conexion)

                vlo_DalOtfIncidenteAlmacen.ModificarRegistro(pvo_Registro)

                vlo_DsExistente = vlo_DalOtfAdjuntoIncidente.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OTF_ADJUNTO_INCIDENTE.ID_INCIDENTE_ALMACEN, pvo_Registro.IdIncidenteAlmacen, Modelo.V_OTF_ADJUNTO_INCIDENTE.ORIGEN, OrigenAdjunto.REGISTRADOR), String.Empty, False, 0, 0)
                vlo_DsNuevo = vlo_DalOtfAdjuntoIncidente.ListarRegistros(String.Format("1 = 0"), String.Empty, False, 0, 0)

                For Each vlo_Fila In vlo_DsAdjuntos.Tables(0).Rows
                    vlo_FilaNueva = vlo_DsNuevo.Tables(0).NewRow

                    vlo_FilaNueva.Item(Modelo.V_OTF_ADJUNTO_INCIDENTE.ID_TIPO_DOCUMENTO) = vlo_Fila(Modelo.V_OTF_ADJUNTO_INCIDENTELST.ID_TIPO_DOCUMENTO)
                    vlo_FilaNueva.Item(Modelo.V_OTF_ADJUNTO_INCIDENTE.ID_INCIDENTE_ALMACEN) = pvo_Registro.IdIncidenteAlmacen
                    vlo_FilaNueva.Item(Modelo.V_OTF_ADJUNTO_INCIDENTE.NOMBRE_ARCHIVO) = vlo_Fila(Modelo.V_OTF_ADJUNTO_INCIDENTELST.NOMBRE_ARCHIVO)
                    vlo_FilaNueva.Item(Modelo.V_OTF_ADJUNTO_INCIDENTE.ARCHIVO) = vlo_Fila(Modelo.V_OTF_ADJUNTO_INCIDENTELST.ARCHIVO)
                    vlo_FilaNueva.Item(Modelo.V_OTF_ADJUNTO_INCIDENTE.DESCRIPCION) = vlo_Fila(Modelo.V_OTF_ADJUNTO_INCIDENTELST.DESCRIPCION)
                    vlo_FilaNueva.Item(Modelo.V_OTF_ADJUNTO_INCIDENTE.ORIGEN) = vlo_Fila(Modelo.V_OTF_ADJUNTO_INCIDENTELST.ORIGEN)
                    vlo_FilaNueva.Item(Modelo.V_OTF_ADJUNTO_INCIDENTE.USUARIO) = vlo_Fila(Modelo.V_OTF_ADJUNTO_INCIDENTELST.USUARIO)

                    vlo_DsNuevo.Tables(0).Rows.Add(vlo_FilaNueva)
                Next

                For Each vlo_Fila In vlo_DsExistente.Tables(0).Rows
                    vlo_Fila.Delete()
                Next

                vlo_DalOtfAdjuntoIncidente.AdapterOtfAdjunto(vlo_DsExistente)

                vlo_DalOtfAdjuntoIncidente.AdapterOtfAdjunto(vlo_DsNuevo)

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
        ''' Permite modificar un registro en la tabla OTF_INCIDENTE_ALMACEN, y n en los documentos adjuntos de la incidencia
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>08/08/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarRegistroConAdjuntosRevisor(ByVal pvo_Registro As EntOtfIncidenteAlmacen, vlo_DsAdjuntos As Data.DataSet) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfIncidenteAlmacen As DalOtfIncidenteAlmacen
            Dim vlo_DalOtfAdjuntoIncidente As DalOtfAdjuntoIncidente
            Dim vlo_DsExistente As Data.DataSet
            Dim vlo_DsNuevo As Data.DataSet
            Dim vln_Resultado As Integer
            Dim vlo_FilaNueva As Data.DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                'inicia la transaccion
                vlo_Conexion.TransaccionBegin()

                vlo_DalOtfIncidenteAlmacen = New DalOtfIncidenteAlmacen(vlo_Conexion)
                vlo_DalOtfAdjuntoIncidente = New DalOtfAdjuntoIncidente(vlo_Conexion)

                vlo_DalOtfIncidenteAlmacen.ModificarRegistro(pvo_Registro)

                vlo_DsExistente = vlo_DalOtfAdjuntoIncidente.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OTF_ADJUNTO_INCIDENTE.ID_INCIDENTE_ALMACEN, pvo_Registro.IdIncidenteAlmacen, Modelo.V_OTF_ADJUNTO_INCIDENTE.ORIGEN, OrigenAdjunto.REVISOR), String.Empty, False, 0, 0)
                vlo_DsNuevo = vlo_DalOtfAdjuntoIncidente.ListarRegistros(String.Format("1 = 0"), String.Empty, False, 0, 0)

                For Each vlo_Fila In vlo_DsAdjuntos.Tables(0).Rows
                    vlo_FilaNueva = vlo_DsNuevo.Tables(0).NewRow

                    vlo_FilaNueva.Item(Modelo.V_OTF_ADJUNTO_INCIDENTE.ID_TIPO_DOCUMENTO) = vlo_Fila(Modelo.V_OTF_ADJUNTO_INCIDENTELST.ID_TIPO_DOCUMENTO)
                    vlo_FilaNueva.Item(Modelo.V_OTF_ADJUNTO_INCIDENTE.ID_INCIDENTE_ALMACEN) = pvo_Registro.IdIncidenteAlmacen
                    vlo_FilaNueva.Item(Modelo.V_OTF_ADJUNTO_INCIDENTE.NOMBRE_ARCHIVO) = vlo_Fila(Modelo.V_OTF_ADJUNTO_INCIDENTELST.NOMBRE_ARCHIVO)
                    vlo_FilaNueva.Item(Modelo.V_OTF_ADJUNTO_INCIDENTE.ARCHIVO) = vlo_Fila(Modelo.V_OTF_ADJUNTO_INCIDENTELST.ARCHIVO)
                    vlo_FilaNueva.Item(Modelo.V_OTF_ADJUNTO_INCIDENTE.DESCRIPCION) = vlo_Fila(Modelo.V_OTF_ADJUNTO_INCIDENTELST.DESCRIPCION)
                    vlo_FilaNueva.Item(Modelo.V_OTF_ADJUNTO_INCIDENTE.ORIGEN) = vlo_Fila(Modelo.V_OTF_ADJUNTO_INCIDENTELST.ORIGEN)
                    vlo_FilaNueva.Item(Modelo.V_OTF_ADJUNTO_INCIDENTE.USUARIO) = vlo_Fila(Modelo.V_OTF_ADJUNTO_INCIDENTELST.USUARIO)

                    vlo_DsNuevo.Tables(0).Rows.Add(vlo_FilaNueva)
                Next

                For Each vlo_Fila In vlo_DsExistente.Tables(0).Rows
                    vlo_Fila.Delete()
                Next

                vlo_DalOtfAdjuntoIncidente.AdapterOtfAdjunto(vlo_DsExistente)

                vlo_DalOtfAdjuntoIncidente.AdapterOtfAdjunto(vlo_DsNuevo)

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
        ''' Permite borrar un registro en la tabla OTF_INCIDENTE_ALMACEN, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtfIncidenteAlmacen) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfIncidenteAlmacen As DalOtfIncidenteAlmacen
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdIncidenteAlmacen) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Hay registro asociados")
                End If

                vlo_DalOtfIncidenteAlmacen = New DalOtfIncidenteAlmacen(vlo_Conexion)
                vln_Resultado = vlo_DalOtfIncidenteAlmacen.BorrarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTF_INCIDENTE_ALMACEN, y sus adjuntos asociados
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>08/08/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistroConAdjuntos(ByVal pvo_Registro As EntOtfIncidenteAlmacen) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfIncidenteAlmacen As DalOtfIncidenteAlmacen
            Dim vlo_DalOtfAdjuntoIncidente As DalOtfAdjuntoIncidente
            Dim vlo_DsExistente As Data.DataSet
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                'inicia la transaccion
                vlo_Conexion.TransaccionBegin()

                vlo_DalOtfAdjuntoIncidente = New DalOtfAdjuntoIncidente(vlo_Conexion)

                vlo_DsExistente = vlo_DalOtfAdjuntoIncidente.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.V_OTF_ADJUNTO_INCIDENTE.ID_INCIDENTE_ALMACEN, pvo_Registro.IdIncidenteAlmacen, Modelo.V_OTF_ADJUNTO_INCIDENTE.ORIGEN, OrigenAdjunto.REGISTRADOR), String.Empty, False, 0, 0)

                For Each vlo_Fila In vlo_DsExistente.Tables(0).Rows
                    vlo_Fila.Delete()
                Next

                vlo_DalOtfAdjuntoIncidente.AdapterOtfAdjunto(vlo_DsExistente)

                vlo_DalOtfIncidenteAlmacen = New DalOtfIncidenteAlmacen(vlo_Conexion)
                vlo_DalOtfIncidenteAlmacen.BorrarRegistro(pvo_Registro)

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
        ''' Verifica si un registro posee datos asociados en las tablas hijas
        ''' </summary>
        ''' <param name="pvn_IdIncidenteAlmacen">Llave primaria de la tabla otf_incidente_almacen asociada a la secuencia sq_id_incidente_almacen</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/08/2016 04:57:26 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdIncidenteAlmacen As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOtfAdjuntoIncidente As DalOtfAdjuntoIncidente

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

                'Determinar la existencia de registros asociados en la tabla OTF_ADJUNTO_INCIDENTE
                vlo_DalOtfAdjuntoIncidente = New DalOtfAdjuntoIncidente(vlo_Conexion)
                If vlo_DalOtfAdjuntoIncidente.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTF_ADJUNTO_INCIDENTE.ID_INCIDENTE_ALMACEN, pvn_IdIncidenteAlmacen)).Existe Then
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
