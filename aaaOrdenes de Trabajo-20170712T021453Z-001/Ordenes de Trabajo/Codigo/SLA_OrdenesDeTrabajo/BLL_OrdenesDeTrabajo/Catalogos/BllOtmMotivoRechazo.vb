Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
	Public Class BllOtmMotivoRechazo
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
		''' Permite agregar un registro en la tabla OTM_MOTIVO_RECHAZO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>22/09/2015 02:37:24 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmMotivoRechazo) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmMotivoRechazo As DalOtmMotivoRechazo
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion).Existe Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede agregar el registro, ya existe un motivo de rechazo con la descripción indicada.")
				End If

				vlo_DalOtmMotivoRechazo = New DalOtmMotivoRechazo(vlo_Conexion)
				vln_Resultado = vlo_DalOtmMotivoRechazo.InsertarRegistro(pvo_Registro)
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
		''' Permite modificar un registro en la tabla OTM_MOTIVO_RECHAZO, no sin antes aplicar la validación de la llave alterna
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>22/09/2015 02:37:24 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmMotivoRechazo) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmMotivoRechazo As DalOtmMotivoRechazo
			Dim vlo_EntOtmMotivoRechazo As EntOtmMotivoRechazo
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_EntOtmMotivoRechazo = ObtenerRegistroPorLlaveAlterna(pvo_Registro.Descripcion)
				If vlo_EntOtmMotivoRechazo.Existe AndAlso vlo_EntOtmMotivoRechazo.IdMotivoRechazo <> pvo_Registro.IdMotivoRechazo Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se puede modificar el registro, ya existe un motivo de rechazo con la descripción indicada.")
				End If

				vlo_DalOtmMotivoRechazo = New DalOtmMotivoRechazo(vlo_Conexion)
				vln_Resultado = vlo_DalOtmMotivoRechazo.ModificarRegistro(pvo_Registro)
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
		''' <param name="pvc_Descripcion">Descripción del motivo de rechazo</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>22/09/2015 02:37:24 p.m.</creationDate>
        ''' <changeLog>
        ''' <author>Cesar Bermudez</author>
        ''' <creationDate>4/12/2015</creationDate>
        ''' <changeDetail>Cambio de alcance a publico para consultas</changeDetail>
        ''' </changeLog>
        Public Function ObtenerRegistroPorLlaveAlterna(pvc_Descripcion As String) As EntOtmMotivoRechazo
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmMotivoRechazo As DalOtmMotivoRechazo

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmMotivoRechazo = New DalOtmMotivoRechazo(vlo_Conexion)
                Return vlo_DalOtmMotivoRechazo.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Modelo.OTM_MOTIVO_RECHAZO.DESCRIPCION, pvc_Descripcion.ToUpper()))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Permite obtener un registro según su id
        ''' </summary>
        ''' <param name="pvn_IdMotivoRechazo">Llave primaria de la tabla otm_motivo_rechazo que se asocia con la secuencia sq_id_motivo_rechazo</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Cesar Bermudez Garcia</author>
        ''' <creationDate>2/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerRegistroPorLlavePrimaria(pvn_IdMotivoRechazo As Integer) As EntOtmMotivoRechazo
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmMotivoRechazo As DalOtmMotivoRechazo

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmMotivoRechazo = New DalOtmMotivoRechazo(vlo_Conexion)
                Return vlo_DalOtmMotivoRechazo.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_MOTIVO_RECHAZO.ID_MOTIVO_RECHAZO, pvn_IdMotivoRechazo))
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
