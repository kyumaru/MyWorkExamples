Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration
Imports System.Data

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
    Public Class BllOtmAutorizadoDirector
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
        ''' Permite agregar un registro en la tabla OTM_AUTORIZADO_DIRECTOR, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>24/09/2015 10:06:27 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmAutorizadoDirector) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmAutorizadoDirector As DalOtmAutorizadoDirector
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.CodUnidadSirh, pvo_Registro.NumEmpleado).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se ha podido agregar el registro, ya existe.")
                End If

                If ObtenerRegistroPorLlaveAlterna(pvo_Registro.CodUnidadSirh, pvo_Registro.NumEmpleado).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("No se ha podido agregar el registro, ya existe.")
                End If

                vlo_DalOtmAutorizadoDirector = New DalOtmAutorizadoDirector(vlo_Conexion)
                vln_Resultado = vlo_DalOtmAutorizadoDirector.InsertarRegistro(pvo_Registro)



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
        ''' Permite modificar un registro en la tabla OTM_AUTORIZADO_DIRECTOR, no sin antes aplicar la validación de la llave alterna
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>24/09/2015 10:06:27 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmAutorizadoDirector) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmAutorizadoDirector As DalOtmAutorizadoDirector
            Dim vlo_EntOtmAutorizadoDirector As EntOtmAutorizadoDirector
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_EntOtmAutorizadoDirector = ObtenerRegistroPorLlaveAlterna(pvo_Registro.CodUnidadSirh, pvo_Registro.NumEmpleado)
                If vlo_EntOtmAutorizadoDirector.Existe AndAlso vlo_EntOtmAutorizadoDirector.CodUnidadSirh <> pvo_Registro.CodUnidadSirh AndAlso vlo_EntOtmAutorizadoDirector.NumEmpleado <> pvo_Registro.NumEmpleado Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Registro ya existente.")
                End If

                vlo_DalOtmAutorizadoDirector = New DalOtmAutorizadoDirector(vlo_Conexion)
                vln_Resultado = vlo_DalOtmAutorizadoDirector.ModificarRegistro(pvo_Registro)
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
        ''' <param name="pvn_CodUnidadSirh">Código de la unidad donde estará autorizado el funcionario</param>
        ''' <param name="pvn_NumEmpleado">Número del empleado autorizado</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>24/09/2015 10:06:27 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_CodUnidadSirh As Integer, pvn_NumEmpleado As Double) As EntOtmAutorizadoDirector
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmAutorizadoDirector As DalOtmAutorizadoDirector

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmAutorizadoDirector = New DalOtmAutorizadoDirector(vlo_Conexion)
                Return vlo_DalOtmAutorizadoDirector.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_AUTORIZADO_DIRECTOR.COD_UNIDAD_SIRH, pvn_CodUnidadSirh, Modelo.OTM_AUTORIZADO_DIRECTOR.NUM_EMPLEADO, pvn_NumEmpleado))
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
        ''' <param name="pvn_CodUnidadSirh">Código de la unidad donde estará autorizado el funcionario</param>
        ''' <param name="pvn_NumEmpleado">Número del empleado autorizado</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>24/09/2015 10:06:27 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlaveAlterna(pvn_CodUnidadSirh As Integer, pvn_NumEmpleado As Double) As EntOtmAutorizadoDirector
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmAutorizadoDirector As DalOtmAutorizadoDirector

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmAutorizadoDirector = New DalOtmAutorizadoDirector(vlo_Conexion)
                Return vlo_DalOtmAutorizadoDirector.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_AUTORIZADO_DIRECTOR.COD_UNIDAD_SIRH, pvn_CodUnidadSirh, Modelo.OTM_AUTORIZADO_DIRECTOR.NUM_EMPLEADO, pvn_NumEmpleado))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="pvc_Condicion"></param>
        ''' <param name="pvc_Orden"></param>
        ''' <param name="pvb_Paginar"></param>
        ''' <param name="pvn_NumeroPagina"></param>
        ''' <param name="pvn_TamanoPagina"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ListarRegistrosListaPersonalizado(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As Data.DataSet
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsRegistros As Data.DataSet
            Dim vlo_DalOtmAutorizadoDirector As DalOtmAutorizadoDirector
            Dim vlo_WsSIRH As WsrSIRH.WsSIRH
            Dim vlo_DsDatos As Data.DataSet
            Dim vlo_Registros As Data.DataSet
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

                vlo_DalOtmAutorizadoDirector = New DalOtmAutorizadoDirector(vlo_Conexion)

                vlo_DsRegistros = vlo_DalOtmAutorizadoDirector.ListarRegistrosLista(
                    pvc_Condicion,
                    pvc_Orden,
                    pvb_Paginar,
                    pvn_NumeroPagina,
                    pvn_TamanoPagina)

                vlo_DsDatos.Tables(0).PrimaryKey = New DataColumn() {vlo_DsDatos.Tables(0).Columns("Codigo_Ubica")}

                For Each vlo_DrFilaAutorizadoDirector In vlo_DsRegistros.Tables(0).Rows

                    vlo_DrFilaUnidad = vlo_DsDatos.Tables(0).Rows.Find(New Object() {vlo_DrFilaAutorizadoDirector(Modelo.V_OTM_AUTORIZADO_DIRECTORLST.COD_UNIDAD_SIRH).ToString})

                    vlo_DrFilaAutorizadoDirector(Modelo.V_OTM_AUTORIZADO_DIRECTORLST.DESCRIPCION_UNIDAD_SIRH) = vlo_DrFilaUnidad("Cod_Desc")

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
