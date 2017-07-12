Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
	Public Class BllOtmRubroDecisionInicia
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
		''' Permite borrar un registro en la tabla OTM_RUBRO_DECISION_INICIA, no sin antes aplicar la validación de dependencia con tablas relacionadas
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>16/03/2016 09:48:14 a.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmRubroDecisionInicia) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOtmRubroDecisionInicia As DalOtmRubroDecisionInicia
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If PoseeRegistrosAsociados(pvo_Registro.IdRubroDecisionInicia) Then
					vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Hay registro asociados")
				End If

				vlo_DalOtmRubroDecisionInicia = New DalOtmRubroDecisionInicia(vlo_Conexion)
				vln_Resultado = vlo_DalOtmRubroDecisionInicia.BorrarRegistro(pvo_Registro)
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
		''' <param name="pvn_IdRubroDecisionInicia">Llave primaria de la tabla otm_rubro_desicion_inicia que se asocia con la secuencia sq_rubro_desicion_inicia</param>
		''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>16/03/2016 09:48:14 a.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function PoseeRegistrosAsociados(pvn_IdRubroDecisionInicia As Integer) As Boolean
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_PoseeRegistrosAsociados As Boolean
            'Dim vlo_DalOthOrdnTrabDecInicial As DalOthOrdnTrabDecInicial
            'Dim vlo_DalOttOrdnTrabDecInicial As DalOttOrdnTrabDecInicial

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

				'Determinar la existencia de registros asociados en la tabla OTH_ORDN_TRAB_DEC_INICIAL
                'vlo_DalOthOrdnTrabDecInicial = New DalOthOrdnTrabDecInicial(vlo_Conexion)
                'If vlo_DalOthOrdnTrabDecInicial.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTH_ORDN_TRAB_DEC_INICIAL.ID_RUBRO_DECISION_INICIA, pvn_IdRubroDecisionInicia)).Existe Then
                '	Return True
                'End If

				'Determinar la existencia de registros asociados en la tabla OTT_ORDN_TRAB_DEC_INICIAL
                'vlo_DalOttOrdnTrabDecInicial = New DalOttOrdnTrabDecInicial(vlo_Conexion)
                'If vlo_DalOttOrdnTrabDecInicial.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_ORDN_TRAB_DEC_INICIAL.ID_RUBRO_DECISION_INICIA, pvn_IdRubroDecisionInicia)).Existe Then
                '	Return True
                'End If

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
