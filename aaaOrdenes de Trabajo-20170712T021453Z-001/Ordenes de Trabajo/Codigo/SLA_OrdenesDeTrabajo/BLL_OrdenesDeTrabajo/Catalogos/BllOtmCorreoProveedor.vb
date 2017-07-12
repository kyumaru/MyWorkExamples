Imports Utilerias.ORDENES_TRABAJO
Imports ORDENES_TRABAJO.EntidadNegocio.Catalogos
Imports ORDENES_TRABAJO.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Utilerias.OrdenesDeTrabajo

Namespace ORDENES_TRABAJO.LogicaNegocio.Catalogos
    Public Class BllOtmCorreoProveedor
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
        ''' Permite agregar un registro en la tabla OTM_CORREO_PROVEEDOR, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/08/2016 03:37:27 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmCorreoProveedor) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmCorreoProveedor As DalOtmCorreoProveedor
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.Identificacion, pvo_Registro.Correo).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
                End If

                vlo_DalOtmCorreoProveedor = New DalOtmCorreoProveedor(vlo_Conexion)
                vln_Resultado = vlo_DalOtmCorreoProveedor.InsertarRegistro(pvo_Registro)
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
        ''' <param name="pvc_Identificacion">Identificación del proveedor (física o jurídica)</param>
        ''' <param name="pvc_Correo">Correo del proveedor</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/08/2016 03:37:27 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvc_Identificacion As String, pvc_Correo As String) As EntOtmCorreoProveedor
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmCorreoProveedor As DalOtmCorreoProveedor

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmCorreoProveedor = New DalOtmCorreoProveedor(vlo_Conexion)
                Return vlo_DalOtmCorreoProveedor.ObtenerRegistro(String.Format("UPPER({0}) = '{1}' AND UPPER({2}) = '{3}'", Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.IDENTIFICACION, pvc_Identificacion.ToUpper(), Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.CORREO, pvc_Correo.ToUpper()))
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
