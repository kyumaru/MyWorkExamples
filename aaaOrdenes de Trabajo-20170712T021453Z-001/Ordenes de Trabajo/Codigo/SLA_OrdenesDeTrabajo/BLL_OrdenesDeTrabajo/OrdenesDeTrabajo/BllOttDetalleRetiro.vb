Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttDetalleRetiro
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
        ''' Permite agregar un registro en la tabla OTT_DETALLE_RETIRO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 02:32:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttDetalleRetiro) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttDetalleRetiro As DalOttDetalleRetiro
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.Anno, pvo_Registro.IdSolicitudRetiro).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
                End If

                vlo_DalOttDetalleRetiro = New DalOttDetalleRetiro(vlo_Conexion)
                vln_Resultado = vlo_DalOttDetalleRetiro.InsertarRegistro(pvo_Registro)
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
        ''' Permite agregar N registros en la tabla OTT_DETALLE_RETIRO, con su respectiva solicitud
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>03/10/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function SolicitudesMaterialCreadas(pvo_DsDatos As Data.DataSet, pvo_Registro As EntOttSolicitudRetiro) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttDetalleRetiro As DalOttDetalleRetiro
            Dim vlo_DalOttSolicitudRetiro As DalOttSolicitudRetiro
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

                vlo_DalOttDetalleRetiro = New DalOttDetalleRetiro(vlo_Conexion)
                vlo_DalOttSolicitudRetiro = New DalOttSolicitudRetiro(vlo_Conexion)

                pvo_Registro.IdSolicitudRetiro = vlo_DalOttSolicitudRetiro.ObtenerFnOtConsecutivoRetiro(pvo_Registro.Anno, pvo_Registro.IdUbicacion) + 1

                vlo_DalOttSolicitudRetiro.InsertarRegistro(pvo_Registro)

                For Each vlo_Fila In pvo_DsDatos.Tables(0).Rows
                    vlo_Fila(Modelo.OTT_DETALLE_RETIRO.ID_SOLICITUD_RETIRO) = pvo_Registro.IdSolicitudRetiro
                Next

                vlo_DalOttDetalleRetiro.AdapterSolicitudRetiro(pvo_DsDatos)

                vlo_Conexion.TransaccionCommit()
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
        ''' Permite obtener un registro según su llave primaria
        ''' </summary>
        ''' <param name="pvn_Anno">Llave primaria de la tabla ott_solicitud_retiro</param>
        ''' <param name="pvn_IdSolicitudRetiro">Llave primaria de la tabla ott_solicitud_retiro</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/06/2016 02:32:03 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_Anno As Integer, pvn_IdSolicitudRetiro As Integer) As EntOttDetalleRetiro
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttDetalleRetiro As DalOttDetalleRetiro

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttDetalleRetiro = New DalOttDetalleRetiro(vlo_Conexion)
                Return vlo_DalOttDetalleRetiro.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTT_DETALLE_RETIRO.ANNO, pvn_Anno, Modelo.OTT_DETALLE_RETIRO.ID_SOLICITUD_RETIRO, pvn_IdSolicitudRetiro))
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
