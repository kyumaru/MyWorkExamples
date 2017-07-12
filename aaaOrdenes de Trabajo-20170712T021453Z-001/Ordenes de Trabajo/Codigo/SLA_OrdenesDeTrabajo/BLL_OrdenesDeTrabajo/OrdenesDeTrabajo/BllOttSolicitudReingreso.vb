Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
	Public Class BllOttSolicitudReingreso
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
		''' Permite agregar un registro en la tabla OTT_SOLICITUD_REINGRESO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
		''' </summary>
		''' <param name="pvo_Registro">Entidad a procesar</param>
		''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
		''' <changeLog></changeLog>
		Public Function InsertarRegistro(ByVal pvo_Registro As EntOttSolicitudReingreso) As Integer
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttSolicitudReingreso As DalOttSolicitudReingreso
			Dim vln_Resultado As Integer

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				If ObtenerRegistroPorLlavePrimaria(pvo_Registro.Anno, pvo_Registro.IdSolicitudReingreso).Existe Then
					vln_Resultado = -1
					Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
				End If

				vlo_DalOttSolicitudReingreso = New DalOttSolicitudReingreso(vlo_Conexion)
				vln_Resultado = vlo_DalOttSolicitudReingreso.InsertarRegistro(pvo_Registro)
			Catch vlo_Excepcion As Exception
				Throw
			Finally
				If vlb_LiberarConexion Then
					vlo_Conexion.Dispose()
				End If
			End Try

			Return vln_Resultado
        End Function

        Public Function AprobacionSolicitudesReingreso(ByVal pvo_DsSolicitudes As Data.DataSet) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttSolicitudReingreso As DalOttSolicitudReingreso
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

                vlo_DalOttSolicitudReingreso = New DalOttSolicitudReingreso(vlo_Conexion)


                vlo_DalOttSolicitudReingreso.AdapterOttSolicitudReingresoMod(pvo_DsSolicitudes)


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


        Public Function InsertarSolicitudesReingreso(ByVal pvo_DsSolicitudes As Data.DataSet, pvn_IdUbicacion As Integer, pvn_Annio As Integer) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttSolicitudReingreso As DalOttSolicitudReingreso
            Dim vln_Resultado As Integer = 0
            Dim vln_Maximo As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Conexion.TransaccionBegin()

                vlo_DalOttSolicitudReingreso = New DalOttSolicitudReingreso(vlo_Conexion)

                vln_Maximo = vlo_DalOttSolicitudReingreso.ObtenerFnOtConsecutivoReingreso(pvn_Annio, pvn_IdUbicacion)

                For Each vlo_Fila In pvo_DsSolicitudes.Tables(0).Rows
                    vlo_Fila(Modelo.OTT_SOLICITUD_REINGRESO.ID_SOLICITUD_REINGRESO) = vln_Maximo + 1
                    vln_Maximo = vln_Maximo + 1
                Next

                vlo_DalOttSolicitudReingreso.AdapterOttSolicitudReingreso(pvo_DsSolicitudes)



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
		''' Permite obtener un registro según su llave primaria
		''' </summary>
		''' <param name="pvn_Anno">Llave primaria de la tabla ott_solicitud_reingreso</param>
		''' <param name="pvn_IdSolicitudReingreso">Llave primaria de la tabla ott_solicitud_reingreso. consecutivo anual, por ubicación</param>
		''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
		''' <author>Generador de Código basado en objetos Oracle</author>
		''' <creationDate>28/07/2016 03:45:21 p.m.</creationDate>
		''' <changeLog></changeLog>
		Private Function ObtenerRegistroPorLlavePrimaria(pvn_Anno As Integer, pvn_IdSolicitudReingreso As Integer) As EntOttSolicitudReingreso
			Dim vlo_Conexion As ConexionOracle
			Dim vlb_LiberarConexion As Boolean
			Dim vlo_DalOttSolicitudReingreso As DalOttSolicitudReingreso

			Try
				If vgo_Conexion Is Nothing Then
					vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
					vlb_LiberarConexion = True
				Else
					vlo_Conexion = vgo_Conexion
					vlb_LiberarConexion = False
				End If

				vlo_DalOttSolicitudReingreso = New DalOttSolicitudReingreso(vlo_Conexion)
				Return vlo_DalOttSolicitudReingreso.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTT_SOLICITUD_REINGRESO.ANNO, pvn_Anno, Modelo.OTT_SOLICITUD_REINGRESO.ID_SOLICITUD_REINGRESO, pvn_IdSolicitudReingreso))
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
