Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOtfPreOrdenTrabajo
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
        ''' Inserta la preorden y la revision
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>22/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function EnviarPreOrden(ByVal pvo_Registro As EntOtfPreOrdenTrabajo, pvc_Estado As String, pvn_RequiereFichaTecnica As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_EntOtfRevisionPreOrdenTra As EntOtfRevisionPreOrdenTra
            Dim vlo_DalOtfRevisionPreOrdenTra As DalOtfRevisionPreOrdenTra
            Dim vlo_DalOttOrdenTrabajo As DalOttOrdenTrabajo
            Dim vln_Resultado As Integer = 0

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                If pvc_Estado = EstadoOrden.ASIGNADA Then
                    vlo_DalOttOrdenTrabajo = New DalOttOrdenTrabajo(vlo_Conexion)
                    If pvn_RequiereFichaTecnica = 0 Then
                        vlo_DalOttOrdenTrabajo.EjecutarPrOtAsigOtMante(pvo_Registro.IdUbicacion, pvo_Registro.IdPreOrdenTrabajo, pvo_Registro.CodUnidadSirh, pvo_Registro.NombrePersonaContacto, pvo_Registro.Telefono, pvo_Registro.SennasExactas, pvo_Registro.DescripcionTrabajo, pvo_Registro.Usuario, CType(pvo_Registro.NumEmpleado, Integer), pvo_Registro.IdCategoriaServicio, pvo_Registro.IdActividad, pvo_Registro.IdLugarTrabajo, pvo_Registro.IncluidaEnRecepcion, pvo_Registro.IdUbicacionOrigen)
                    Else
                        vlo_DalOttOrdenTrabajo.EjecutarPrOtAsigOtDisenio(pvo_Registro.IdUbicacion, pvo_Registro.IdPreOrdenTrabajo, pvo_Registro.CodUnidadSirh, pvo_Registro.NombrePersonaContacto, pvo_Registro.Telefono, pvo_Registro.SennasExactas, pvo_Registro.DescripcionTrabajo, pvo_Registro.Usuario, CType(pvo_Registro.NumEmpleado, Integer), pvo_Registro.IdCategoriaServicio, pvo_Registro.IdActividad, pvo_Registro.IdLugarTrabajo, pvo_Registro.IncluidaEnRecepcion, pvo_Registro.IdUbicacionOrigen)
                    End If
                Else
                    vlo_EntOtfRevisionPreOrdenTra = New EntOtfRevisionPreOrdenTra
                    vlo_EntOtfRevisionPreOrdenTra.IdUbicacion = pvo_Registro.IdUbicacion
                    vlo_EntOtfRevisionPreOrdenTra.IdPreOrdenTrabajo = pvo_Registro.IdPreOrdenTrabajo
                    vlo_EntOtfRevisionPreOrdenTra.Estado = pvc_Estado
                    vlo_EntOtfRevisionPreOrdenTra.Usuario = pvo_Registro.Usuario

                    vlo_DalOtfRevisionPreOrdenTra = New DalOtfRevisionPreOrdenTra(vlo_Conexion)
                    vlo_DalOtfRevisionPreOrdenTra.InsertarRegistro(vlo_EntOtfRevisionPreOrdenTra)
                End If

                vlo_Conexion.TransaccionCommit()

                vln_Resultado = 1
                Return vln_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Inserta la preorden y la revision
        ''' </summary>
        ''' <param name="pvo_Registro"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>22/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarPreOrdenCadena(ByVal pvo_Registro As EntOtfPreOrdenTrabajo, pvc_Estado As String) As String
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfPreOrdenTrabajo As DalOtfPreOrdenTrabajo
            Dim vlo_EntOtfRevisionPreOrdenTra As EntOtfRevisionPreOrdenTra
            Dim vlo_DalOtfRevisionPreOrdenTra As DalOtfRevisionPreOrdenTra
            Dim vlc_Resultado As String
            vlc_Resultado = String.Empty
            Dim vln_Consecutivo As Integer = 0

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOtfPreOrdenTrabajo = New DalOtfPreOrdenTrabajo(vlo_Conexion)
                vln_Consecutivo = vlo_DalOtfPreOrdenTrabajo.InsertarRegistro(pvo_Registro)

                vlo_EntOtfRevisionPreOrdenTra = New EntOtfRevisionPreOrdenTra
                vlo_EntOtfRevisionPreOrdenTra.IdUbicacion = pvo_Registro.IdUbicacion
                vlo_EntOtfRevisionPreOrdenTra.IdPreOrdenTrabajo = vln_Consecutivo
                vlo_EntOtfRevisionPreOrdenTra.Estado = pvc_Estado
                vlo_EntOtfRevisionPreOrdenTra.Usuario = pvo_Registro.Usuario

                vlo_DalOtfRevisionPreOrdenTra = New DalOtfRevisionPreOrdenTra(vlo_Conexion)
                vlo_DalOtfRevisionPreOrdenTra.InsertarRegistro(vlo_EntOtfRevisionPreOrdenTra)

                vlo_Conexion.TransaccionCommit()

                vlc_Resultado = String.Format("{0}¬{1}", pvo_Registro.IdUbicacion, vln_Consecutivo)
                Return vlc_Resultado
            Catch vlo_Excepcion As Exception
                Throw New OrdenesDeTrabajoException("Ha ocurrido un error.")
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
            Return vlc_Resultado
        End Function

        ''' <summary>
        ''' Permite agregar un registro en la tabla OTF_PRE_ORDEN_TRABAJO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:07:43 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtfPreOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfPreOrdenTrabajo As DalOtfPreOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfPreOrdenTrabajo = New DalOtfPreOrdenTrabajo(vlo_Conexion)
                vln_Resultado = vlo_DalOtfPreOrdenTrabajo.InsertarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTF_PRE_ORDEN_TRABAJO, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:07:43 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtfPreOrdenTrabajo) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfPreOrdenTrabajo As DalOtfPreOrdenTrabajo
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdPreOrdenTrabajo, pvo_Registro.IdUbicacion) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Hay registro asociados")
                End If

                vlo_DalOtfPreOrdenTrabajo = New DalOtfPreOrdenTrabajo(vlo_Conexion)
                vln_Resultado = vlo_DalOtfPreOrdenTrabajo.BorrarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTF_PRE_ORDEN_TRABAJO,y las dependencias con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>21/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorradoFisico(ByVal pvn_IdUbicacion As Integer, pvn_IdPreOrdenTrabajo As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfPreOrdenTrabajo As DalOtfPreOrdenTrabajo
            Dim vln_Resultado As Integer = 0

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfPreOrdenTrabajo = New DalOtfPreOrdenTrabajo(vlo_Conexion)
                vlo_DalOtfPreOrdenTrabajo.EjecutarPrOtBorrarPreOrden(pvn_IdUbicacion, pvn_IdPreOrdenTrabajo)
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
        ''' Verifica si un registro posee datos asociados en las tablas hijas
        ''' </summary>
        ''' <param name="pvn_IdPreOrdenTrabajo">Llave primaria de la tabla otf_pre_orden_trabajo que se asocia con la secuencia sq_id_pre_orden_trabajo</param>
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 12:07:43 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdPreOrdenTrabajo As Integer, pvn_IdUbicacion As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOtfFichaTecnicaGeneral As DalOtfFichaTecnicaGeneral
            Dim vlo_DalOtfFichaTecnicaEspacio As DalOtfFichaTecnicaEspacio
            Dim vlo_DalOtfRevisionPreOrdenTra As DalOtfRevisionPreOrdenTra

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

                'Determinar la existencia de registros asociados en la tabla OTF_FICHA_TECNICA_GENERAL
                vlo_DalOtfFichaTecnicaGeneral = New DalOtfFichaTecnicaGeneral(vlo_Conexion)
                If vlo_DalOtfFichaTecnicaGeneral.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_FICHA_TECNICA_GENERAL.ID_PRE_ORDEN_TRABAJO, pvn_IdPreOrdenTrabajo, Modelo.OTF_FICHA_TECNICA_GENERAL.ID_UBICACION, pvn_IdUbicacion)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTF_FICHA_TECNICA_ESPACIO
                vlo_DalOtfFichaTecnicaEspacio = New DalOtfFichaTecnicaEspacio(vlo_Conexion)
                If vlo_DalOtfFichaTecnicaEspacio.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_FICHA_TECNICA_ESPACIO.ID_PRE_ORDEN_TRABAJO, pvn_IdPreOrdenTrabajo, Modelo.OTF_FICHA_TECNICA_ESPACIO.ID_UBICACION, pvn_IdUbicacion)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTF_REVISION_PRE_ORDEN_TRA
                vlo_DalOtfRevisionPreOrdenTra = New DalOtfRevisionPreOrdenTra(vlo_Conexion)
                If vlo_DalOtfRevisionPreOrdenTra.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_REVISION_PRE_ORDEN_TRA.ID_PRE_ORDEN_TRABAJO, pvn_IdPreOrdenTrabajo, Modelo.OTF_REVISION_PRE_ORDEN_TRA.ID_UBICACION, pvn_IdUbicacion)).Existe Then
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
