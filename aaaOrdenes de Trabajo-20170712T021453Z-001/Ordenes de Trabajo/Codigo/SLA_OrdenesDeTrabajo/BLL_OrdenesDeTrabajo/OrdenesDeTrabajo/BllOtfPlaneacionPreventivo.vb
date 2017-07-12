Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOtfPlaneacionPreventivo
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
		''' Permite agregar un registro en la tabla OTF_PLANEACION_PREVENTIVO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>09/09/2015 03:12:05 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtfPlaneacionPreventivo) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtfPlaneacionPreventivo As DalOtfPlaneacionPreventivo
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.ConsecutivoPropuesto, pvo_Registro.IdUbicacionAdministra).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("El número de orden ya existe, digite otro.")
                End If

                If ObtenerRegistroPorLugarCategoriaActividad(pvo_Registro.IdLugarTrabajo, pvo_Registro.IdCategoriaServicio, pvo_Registro.IdActividad).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("El edificio o sitio con la categoría de servicio y la actividad ya existen, seleccione otra combinación.")
                End If

                vlo_DalOtfPlaneacionPreventivo = New DalOtfPlaneacionPreventivo(vlo_Conexion)
                vln_Resultado = vlo_DalOtfPlaneacionPreventivo.InsertarRegistro(pvo_Registro)
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
        ''' Permite obtener un registro según el lugar, categoria y actividad
        ''' </summary>
        ''' <param name="pvn_IdLugarTrabajo"></param>
        ''' <param name="pvn_IdCategoriaServicio"></param>
        ''' <param name="pvn_IdActividad"></param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>09/09/2015</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLugarCategoriaActividad(pvn_IdLugarTrabajo As Integer, pvn_IdCategoriaServicio As Integer, pvn_IdActividad As Integer) As EntOtfPlaneacionPreventivo
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfPlaneacionPreventivo As DalOtfPlaneacionPreventivo

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfPlaneacionPreventivo = New DalOtfPlaneacionPreventivo(vlo_Conexion)
                Return vlo_DalOtfPlaneacionPreventivo.ObtenerRegistro(
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = {5}",
                                  Modelo.OTF_PLANEACION_PREVENTIVO.ID_LUGAR_TRABAJO, pvn_IdLugarTrabajo,
                                  Modelo.OTF_PLANEACION_PREVENTIVO.ID_CATEGORIA_SERVICIO, pvn_IdCategoriaServicio,
                                  Modelo.OTF_PLANEACION_PREVENTIVO.ID_ACTIVIDAD, pvn_IdActividad))

            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Permite obtener un registro según su llave primaria
        ''' </summary>
        ''' <param name="pvn_ConsecutivoPropuesto">Consecutivo propuesto en la planificación para la generación de la orden de trabajo</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/09/2015 03:12:05 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_ConsecutivoPropuesto As Integer, pvn_IdUbicacionAdministra As Integer) As EntOtfPlaneacionPreventivo
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfPlaneacionPreventivo As DalOtfPlaneacionPreventivo

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfPlaneacionPreventivo = New DalOtfPlaneacionPreventivo(vlo_Conexion)
                Return vlo_DalOtfPlaneacionPreventivo.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTF_PLANEACION_PREVENTIVO.CONSECUTIVO_PROPUESTO, pvn_ConsecutivoPropuesto, Modelo.OTF_PLANEACION_PREVENTIVO.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra))
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
