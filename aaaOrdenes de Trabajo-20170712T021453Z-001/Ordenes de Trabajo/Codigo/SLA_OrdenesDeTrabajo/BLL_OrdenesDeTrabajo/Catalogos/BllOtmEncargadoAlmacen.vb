Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
	Public Class BllOtmEncargadoAlmacen
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
		''' Permite agregar un registro en la tabla OTM_ENCARGADO_ALMACEN, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>28/06/2016 07:52:43 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmEncargadoAlmacen) As Integer
			Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_EntOtmEncargadoAlmacen As EntOtmEncargadoAlmacen
			Dim vlo_DalOtmEncargadoAlmacen As DalOtmEncargadoAlmacen
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.NumEmpleado).Existe Then
                    'vln_Resultado = -1
                    'Throw New OrdenesDeTrabajoException("Llave primaria repetida")
                    vlo_DalOtmEncargadoAlmacen = New DalOtmEncargadoAlmacen(vlo_Conexion)

                    vlo_EntOtmEncargadoAlmacen = vlo_DalOtmEncargadoAlmacen.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_ENCARGADO_ALMACEN.NUM_EMPLEADO, pvo_Registro.NumEmpleado))

                    vlo_EntOtmEncargadoAlmacen.Rol = pvo_Registro.Rol
                    vln_Resultado = vlo_DalOtmEncargadoAlmacen.ModificarRegistro(vlo_EntOtmEncargadoAlmacen)

                Else


                    vlo_DalOtmEncargadoAlmacen = New DalOtmEncargadoAlmacen(vlo_Conexion)
                    vln_Resultado = vlo_DalOtmEncargadoAlmacen.InsertarRegistro(pvo_Registro)

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
		''' Permite obtener un registro según su llave primaria
		''' </summary>
		''' <param name="pvn_NumEmpleado">Número de empleado del encargado</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>28/06/2016 07:52:43 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_NumEmpleado As Double) As EntOtmEncargadoAlmacen
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmEncargadoAlmacen As DalOtmEncargadoAlmacen

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtmEncargadoAlmacen = New DalOtmEncargadoAlmacen(vlo_Conexion)
				Return vlo_DalOtmEncargadoAlmacen.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_ENCARGADO_ALMACEN.NUM_EMPLEADO, pvn_NumEmpleado))
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
