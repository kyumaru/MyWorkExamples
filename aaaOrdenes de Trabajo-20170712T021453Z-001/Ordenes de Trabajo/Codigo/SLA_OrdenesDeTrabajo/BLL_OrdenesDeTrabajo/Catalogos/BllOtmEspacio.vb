Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
    Public Class BllOtmEspacio
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
        ''' Permite agregar un registro en la tabla OTM_ESPACIO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/11/2015 04:54:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmEspacio) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmEspacio As DalOtmEspacio
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdEspacio).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente.")
                End If

                If ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacion, pvo_Registro.Descripcion).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente.")
                End If

                vlo_DalOtmEspacio = New DalOtmEspacio(vlo_Conexion)
                vln_Resultado = vlo_DalOtmEspacio.InsertarRegistro(pvo_Registro)
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
        ''' Permite modificar un registro en la tabla OTM_ESPACIO, no sin antes aplicar la validación de la llave alterna
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/11/2015 04:54:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmEspacio) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmEspacio As DalOtmEspacio
            Dim vlo_EntOtmEspacio As EntOtmEspacio
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_EntOtmEspacio = ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacion, pvo_Registro.Descripcion)
                If vlo_EntOtmEspacio.Existe AndAlso vlo_EntOtmEspacio.IdEspacio <> pvo_Registro.IdEspacio Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente.")
                End If

                vlo_DalOtmEspacio = New DalOtmEspacio(vlo_Conexion)
                vln_Resultado = vlo_DalOtmEspacio.ModificarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTM_ESPACIO, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/11/2015 04:54:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmEspacio) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmEspacio As DalOtmEspacio
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdEspacio) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No puede ser borrado, posee registros asociados.")
                End If

                vlo_DalOtmEspacio = New DalOtmEspacio(vlo_Conexion)
                vln_Resultado = vlo_DalOtmEspacio.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvn_IdEspacio">Llave primaria de la tabla otm_espacio que se asocia con la secuencia sq_id_espacio</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/11/2015 04:54:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdEspacio As Integer) As EntOtmEspacio
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmEspacio As DalOtmEspacio

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmEspacio = New DalOtmEspacio(vlo_Conexion)
                Return vlo_DalOtmEspacio.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_ESPACIO.ID_ESPACIO, pvn_IdEspacio))
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
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvc_Descripcion">Descripción del espacio</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/11/2015 04:54:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlaveAlterna(pvn_IdUbicacion As Integer, pvc_Descripcion As String) As EntOtmEspacio
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmEspacio As DalOtmEspacio

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmEspacio = New DalOtmEspacio(vlo_Conexion)
                Return vlo_DalOtmEspacio.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTM_ESPACIO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTM_ESPACIO.DESCRIPCION, pvc_Descripcion.ToUpper()))
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
        ''' <param name="pvn_IdEspacio">Llave primaria de la tabla otm_espacio que se asocia con la secuencia sq_id_espacio</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/11/2015 04:54:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdEspacio As Integer) As Boolean

            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOtmSubcomponente As DalOtmSubcomponente
            Dim vlo_DalOttFichaTecnicaEspacio As DalOttFichaTecnicaEspacio

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

                'Determinar la existencia de registros asociados en la tabla OTM_SUBCOMPONENTE
                vlo_DalOtmSubcomponente = New DalOtmSubcomponente(vlo_Conexion)
                If vlo_DalOtmSubcomponente.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_SUBCOMPONENTE.ID_ESPACIO, pvn_IdEspacio)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTT_FICHA_TECNICA_ESPACIO
                vlo_DalOttFichaTecnicaEspacio = New DalOttFichaTecnicaEspacio(vlo_Conexion)
                If vlo_DalOttFichaTecnicaEspacio.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_FICHA_TECNICA_ESPACIO.ID_ESPACIO, pvn_IdEspacio)).Existe Then
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
