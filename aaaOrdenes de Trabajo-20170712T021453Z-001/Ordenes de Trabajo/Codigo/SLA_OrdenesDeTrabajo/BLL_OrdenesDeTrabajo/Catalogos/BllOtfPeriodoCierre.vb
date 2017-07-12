Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
	Public Class BllOtfPeriodoCierre
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
		''' Permite agregar un registro en la tabla OTF_PERIODO_CIERRE, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>18/01/2016 10:53:17 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtfPeriodoCierre) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtfPeriodoCierre As DalOtfPeriodoCierre
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdPeriodoCierre).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente.")
				End If

				If ObtenerRegistroPorLlaveAlterna(pvo_Registro.UnidadCierre, pvo_Registro.FechaInicioCierre).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Ya existe una unidad de este tipo, favor modificar la existente")
				End If

				vlo_DalOtfPeriodoCierre = New DalOtfPeriodoCierre(vlo_Conexion)
				vln_Resultado = vlo_DalOtfPeriodoCierre.InsertarRegistro(pvo_Registro)
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
		''' Permite modificar un registro en la tabla OTF_PERIODO_CIERRE, no sin antes aplicar la validación de la llave alterna
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>18/01/2016 10:53:17 a.m.</creationDate>
        ''' <changeLog>
        '''    <author>César Bermúdez García</author>
        '''    <creationDate>19/1/2016</creationDate>
        ''' </changeLog>
        Public Function ModificarRegistro(ByVal pvo_Registro As EntOtfPeriodoCierre) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtfPeriodoCierre As DalOtfPeriodoCierre
            'Dim vlo_EntOtfPeriodoCierre As EntOtfPeriodoCierre
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                'TODO Se realiza una comprobación por llave alterna para verificar que se está modificando el registro.
                'La condición en este caso queda por hacer ya que no es clara

                'vlo_EntOtfPeriodoCierre = ObtenerRegistroPorLlaveAlterna(pvo_Registro.UnidadCierre, pvo_Registro.FechaInicioCierre)
                'If vlo_EntOtfPeriodoCierre.Existe AndAlso vlo_EntOtfPeriodoCierre.IdPeriodoCierre <> pvo_Registro.IdPeriodoCierre Then
                '	vln_Resultado = -1
                '	Throw New OrdenesDeTrabajoException("Llave alterna repetida")
                'End If

                vlo_DalOtfPeriodoCierre = New DalOtfPeriodoCierre(vlo_Conexion)
                vln_Resultado = vlo_DalOtfPeriodoCierre.ModificarRegistro(pvo_Registro)
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
		''' <param name="pvn_IdPeriodoCierre">Llave primaria de la tabla otf_periodo_cierre que se asocia con la secuencia sq_id_periodo_cierre</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>18/01/2016 10:53:17 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdPeriodoCierre As Integer) As EntOtfPeriodoCierre
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtfPeriodoCierre As DalOtfPeriodoCierre

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtfPeriodoCierre = New DalOtfPeriodoCierre(vlo_Conexion)
				Return vlo_DalOtfPeriodoCierre.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTF_PERIODO_CIERRE.ID_PERIODO_CIERRE, pvn_IdPeriodoCierre))
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
		''' <param name="pvc_UnidadCierre">Unidad de la sección de mantenimiento y construcción que realiza el cierre - mnt: mantenimiento, dis: diseño - valor defecto: mnt</param>
		''' <param name="pvd_FechaInicioCierre">Fecha de inicio del cierre</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>18/01/2016 10:53:17 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlaveAlterna(pvc_UnidadCierre As String, pvd_FechaInicioCierre As DateTime) As EntOtfPeriodoCierre
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtfPeriodoCierre As DalOtfPeriodoCierre

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOtfPeriodoCierre = New DalOtfPeriodoCierre(vlo_Conexion)
                Return vlo_DalOtfPeriodoCierre.ObtenerRegistro(String.Format("UPPER({0}) = '{1}' AND TO_DATE(TO_CHAR({2}, '{3}'), '{3}') = TO_DATE ('{4}', '{3}')",
                                                                             Modelo.OTF_PERIODO_CIERRE.UNIDAD_CIERRE,
                                                                             pvc_UnidadCierre.ToUpper(),
                                                                             Modelo.OTF_PERIODO_CIERRE.FECHA_INICIO_CIERRE,
                                                                             Constantes.FORMATO_FECHA_BD,
                                                                             pvd_FechaInicioCierre.ToString(Constantes.FORMATO_FECHA_BD)))
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
