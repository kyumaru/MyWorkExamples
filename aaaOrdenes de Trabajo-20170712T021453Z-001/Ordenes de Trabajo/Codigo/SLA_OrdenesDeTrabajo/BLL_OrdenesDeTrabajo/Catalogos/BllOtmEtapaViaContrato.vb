Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
    Public Class BllOtmEtapaViaContrato
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
        ''' Permite agregar un registro en la tabla OTM_ETAPA_VIA_CONTRATO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmEtapaViaContrato) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmEtapaViaContrato As DalOtmEtapaViaContrato
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdViaContrato, pvo_Registro.IdEtapaContratacion).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Llave primaria repetida")
                End If

                vlo_DalOtmEtapaViaContrato = New DalOtmEtapaViaContrato(vlo_Conexion)
                vln_Resultado = vlo_DalOtmEtapaViaContrato.InsertarRegistro(pvo_Registro)
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
        ''' <param name="pvn_IdViaContrato">Llave primaria de la tabla otm_via_contrato que se asocia con la secuencia sq_id_via_contrato</param>
        ''' <param name="pvn_IdEtapaContratacion">Llave primaria de la tabla otm_etapa_contratacion que se asocia con la secuencia sq_id_etapa_contratacion</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>14/04/2016 09:56:09 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdViaContrato As Integer, pvn_IdEtapaContratacion As Integer) As EntOtmEtapaViaContrato
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmEtapaViaContrato As DalOtmEtapaViaContrato

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmEtapaViaContrato = New DalOtmEtapaViaContrato(vlo_Conexion)
                Return vlo_DalOtmEtapaViaContrato.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_ETAPA_VIA_CONTRATO.ID_VIA_CONTRATO, pvn_IdViaContrato, Modelo.OTM_ETAPA_VIA_CONTRATO.ID_ETAPA_CONTRATACION, pvn_IdEtapaContratacion))
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
