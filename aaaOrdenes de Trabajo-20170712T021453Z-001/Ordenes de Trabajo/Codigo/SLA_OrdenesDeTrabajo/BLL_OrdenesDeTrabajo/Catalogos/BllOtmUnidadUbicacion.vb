Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration
Imports System.Data

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
    Public Class BllOtmUnidadUbicacion
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
        ''' Permite agregar un registro en la tabla OTM_UNIDAD_UBICACION, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>05/10/2015 10:18:16 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmUnidadUbicacion) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmUnidadUbicacion As DalOtmUnidadUbicacion
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.CodUnidadSirh).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente.")
                End If

                vlo_DalOtmUnidadUbicacion = New DalOtmUnidadUbicacion(vlo_Conexion)
                vln_Resultado = vlo_DalOtmUnidadUbicacion.InsertarRegistro(pvo_Registro)
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
        ''' <param name="pvn_CodUnidadSirh">Código de la unidad académica o administrativa</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>05/10/2015 10:18:16 a.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_CodUnidadSirh As Integer) As EntOtmUnidadUbicacion
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmUnidadUbicacion As DalOtmUnidadUbicacion

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmUnidadUbicacion = New DalOtmUnidadUbicacion(vlo_Conexion)
                Return vlo_DalOtmUnidadUbicacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_UNIDAD_UBICACION.COD_UNIDAD_SIRH, pvn_CodUnidadSirh))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' carga la vista para lista de unidad ubicacion, ademas carga la descripcion de la unidad segun el COD_UNIDAD_SIRH
        ''' </summary>
        ''' <param name="pvc_Condicion"></param>
        ''' <param name="pvc_Orden"></param>
        ''' <param name="pvb_Paginar"></param>
        ''' <param name="pvn_NumeroPagina"></param>
        ''' <param name="pvn_TamanoPagina"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>05/10/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarRegistrosListaPersonalizado(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As Data.DataSet
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsRegistros As Data.DataSet
            Dim vlo_DalOtmUnidadUbicacion As DalOtmUnidadUbicacion
            Dim vlo_WsSIRH As WsrSIRH.WsSIRH
            Dim vlo_DsDatos As Data.DataSet
            Dim vlo_DrFilaUnidad As Data.DataRow

            vlo_WsSIRH = New WsrSIRH.WsSIRH
            vlo_WsSIRH.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_WsSIRH.Timeout = -1
            vlo_WsSIRH.Url = System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_SIRH).ToString

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DsDatos = vlo_WsSIRH.UBICACION_CargarRegistros(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    DateTime.Now,
                    DateTime.Now,
                    0,
                    1,
                    -1,
                    True)

                vlo_DalOtmUnidadUbicacion = New DalOtmUnidadUbicacion(vlo_Conexion)

                vlo_DsRegistros = vlo_DalOtmUnidadUbicacion.ListarRegistrosLista(
                    pvc_Condicion,
                    pvc_Orden,
                    pvb_Paginar,
                    pvn_NumeroPagina,
                    pvn_TamanoPagina)

                vlo_DsDatos.Tables(0).PrimaryKey = New DataColumn() {vlo_DsDatos.Tables(0).Columns("CODIGO_UBICA")}

                For Each vlo_DrFilaUnidadUbicacion In vlo_DsRegistros.Tables(0).Rows

                    vlo_DrFilaUnidad = vlo_DsDatos.Tables(0).Rows.Find(New Object() {vlo_DrFilaUnidadUbicacion(Modelo.V_OTM_UNIDAD_UBICACIONLST.COD_UNIDAD_SIRH)})

                    If vlo_DrFilaUnidad IsNot Nothing Then
                        vlo_DrFilaUnidadUbicacion(Modelo.V_OTM_UNIDAD_UBICACIONLST.DESC_COD_UNIDAD_SIRH) = vlo_DrFilaUnidad("COD_DESC")
                    End If

                Next

                Return vlo_DsRegistros

            Catch vlo_Exc As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Exc, Utilerias.OrdenesDeTrabajo.Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw
                End If
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
                If vlo_WsSIRH IsNot Nothing Then
                    vlo_WsSIRH.Dispose()
                End If
            End Try
        End Function

#End Region

    End Class
End Namespace
