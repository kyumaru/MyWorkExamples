Imports Utilerias.ORDENES_TRABAJO
Imports ORDENES_TRABAJO.EntidadNegocio.Catalogos
Imports ORDENES_TRABAJO.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Utilerias.OrdenesDeTrabajo

Namespace ORDENES_TRABAJO.LogicaNegocio.Catalogos
    Public Class BllOttLineaTraslado
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
        ''' Permite agregar un registro en la tabla OTT_LINEA_TRASLADO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:16:31 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttLineaTraslado) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttLineaTraslado As DalOttLineaTraslado
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.Anno, pvo_Registro.IdSolicitudTraslado, pvo_Registro.IdUbicacion, pvo_Registro.IdAlmacen, pvo_Registro.IdMaterial).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
                End If

                vlo_DalOttLineaTraslado = New DalOttLineaTraslado(vlo_Conexion)
                vln_Resultado = vlo_DalOttLineaTraslado.InsertarRegistro(pvo_Registro)
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
        ''' <param name="pvn_Anno">Año de la solicitud</param>
        ''' <param name="pvn_IdSolicitudTraslado">Consecutivo de la solicitud. el consecutivo es anual.</param>
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvn_IdAlmacen">Llave primaria de la tabla otf_inventario correspondiente al almacén principal</param>
        ''' <param name="pvn_IdMaterial">Llave primaria de la tabla otf_inventario correspondiente al material a trasladar</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>09/08/2016 09:16:31 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_Anno As Integer, pvn_IdSolicitudTraslado As Integer, pvn_IdUbicacion As Integer, pvn_IdAlmacen As Integer, pvn_IdMaterial As Integer) As EntOttLineaTraslado
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttLineaTraslado As DalOttLineaTraslado

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttLineaTraslado = New DalOttLineaTraslado(vlo_Conexion)
                Return vlo_DalOttLineaTraslado.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Utilerias.OrdenesDeTrabajo.Modelo.OTT_LINEA_TRASLADO.ANNO, pvn_Anno, Utilerias.OrdenesDeTrabajo.Modelo.OTT_LINEA_TRASLADO.ID_SOLICITUD_TRASLADO, pvn_IdSolicitudTraslado, Utilerias.OrdenesDeTrabajo.Modelo.OTT_LINEA_TRASLADO.ID_UBICACION, pvn_IdUbicacion, Utilerias.OrdenesDeTrabajo.Modelo.OTT_LINEA_TRASLADO.ID_ALMACEN, pvn_IdAlmacen, Utilerias.OrdenesDeTrabajo.Modelo.OTT_LINEA_TRASLADO.ID_MATERIAL, pvn_IdMaterial))
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
