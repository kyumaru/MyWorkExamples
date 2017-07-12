Imports Utilerias.OrdenesDeTrabajo
Imports ORDENES_TRABAJO.EntidadNegocio.Catalogos
Imports ORDENES_TRABAJO.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data

Namespace ORDENES_TRABAJO.LogicaNegocio.Catalogos
    Public Class BllOtmProveedor
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
        ''' Permite agregar un registro en la tabla OTM_PROVEEDOR, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmProveedor) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmProveedor As DalOtmProveedor
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.Identificacion).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
                End If

                vlo_DalOtmProveedor = New DalOtmProveedor(vlo_Conexion)
                vln_Resultado = vlo_DalOtmProveedor.InsertarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTM_PROVEEDOR, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmProveedor) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmProveedor As DalOtmProveedor
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.Identificacion) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Hay registro asociados")
                End If

                vlo_DalOtmProveedor = New DalOtmProveedor(vlo_Conexion)
                vln_Resultado = vlo_DalOtmProveedor.BorrarRegistro(pvo_Registro)
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
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvc_Identificacion As String) As EntOtmProveedor
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmProveedor As DalOtmProveedor

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmProveedor = New DalOtmProveedor(vlo_Conexion)
                Return vlo_DalOtmProveedor.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Utilerias.OrdenesDeTrabajo.Modelo.OTM_PROVEEDOR.IDENTIFICACION, pvc_Identificacion.ToUpper()))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Verifica si un registro posee datos asociados en las tablas hijas
        ''' </summary>
        ''' <param name="pvc_Identificacion">Identificación del proveedor (física o jurídica)</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvc_Identificacion As String) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOtmTelefonoProveedor As DalOtmTelefonoProveedor
            Dim vlo_DalOtmCorreoProveedor As DalOtmCorreoProveedor

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

                'Determinar la existencia de registros asociados en la tabla OTM_TELEFONO_PROVEEDOR
                vlo_DalOtmTelefonoProveedor = New DalOtmTelefonoProveedor(vlo_Conexion)
                If vlo_DalOtmTelefonoProveedor.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Utilerias.OrdenesDeTrabajo.Modelo.OTM_TELEFONO_PROVEEDOR.IDENTIFICACION, pvc_Identificacion.ToUpper())).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTM_CORREO_PROVEEDOR
                vlo_DalOtmCorreoProveedor = New DalOtmCorreoProveedor(vlo_Conexion)
                If vlo_DalOtmCorreoProveedor.ObtenerRegistro(String.Format("UPPER({0}) = '{1}'", Utilerias.OrdenesDeTrabajo.Modelo.OTM_CORREO_PROVEEDOR.IDENTIFICACION, pvc_Identificacion.ToUpper())).Existe Then
                    Return True
                End If

                Return False
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Permite agregar un registro en la tabla OTM_PROVEEDOR, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>04/08/2016 03:35:36 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarModificarRegistroConAsociados(ByVal pvo_Registro As EntOtmProveedor, ByVal pvo_DsTelefonos As DataSet, ByVal pvo_DsCorreos As DataSet, ByVal pvb_EsAgregar As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmProveedor As DalOtmProveedor
            Dim vln_Resultado As Integer
            Dim vlo_DsTelefonosBorrados As New DataSet
            Dim vlo_DsTelefonosAgregados As New DataSet
            Dim vlo_DsCorreoBorrados As New DataSet
            Dim vlo_DsCorreoAgregados As New DataSet

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)

                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmProveedor = New DalOtmProveedor(vlo_Conexion)

                vlo_Conexion.TransaccionBegin()
                If pvo_DsTelefonos.Tables(0).Rows.Count > 0 And Not pvo_DsTelefonos.Tables(0) Is Nothing And pvo_DsCorreos.Tables(0).Rows.Count > 0 And Not pvo_DsCorreos.Tables(0) Is Nothing Then
                    If pvb_EsAgregar = True Then
                        vln_Resultado = vlo_DalOtmProveedor.InsertarRegistro(pvo_Registro)
                    Else
                        vln_Resultado = vlo_DalOtmProveedor.ModificarRegistro(pvo_Registro)
                    End If

                    If vln_Resultado > 0 Then
                        'separa los telefonos que se deben de borrar y los que se deben de agregar
                        vlo_DsTelefonosBorrados = ObtenerRegistrosPorEstado(pvo_DsTelefonos, DataRowState.Deleted)
                        vlo_DsTelefonosAgregados = ObtenerRegistrosPorEstado(pvo_DsTelefonos, DataRowState.Added)


                        'Borra y agrega los registros de telefonos del proveedor
                        AgregarBorrarRegistroDeTelefono(vlo_DsTelefonosBorrados, vlo_DsTelefonosAgregados, vlo_Conexion)

                        'separa los correos que se deben de borrar y los que se deben de agregar
                        vlo_DsCorreoBorrados = ObtenerRegistrosPorEstado(pvo_DsCorreos, DataRowState.Deleted)
                        vlo_DsCorreoAgregados = ObtenerRegistrosPorEstado(pvo_DsCorreos, DataRowState.Added)

                        'Borra y agrega los registros de correos del proveedor
                        AgregarBorrarRegistroDeCorreo(vlo_DsCorreoBorrados, vlo_DsCorreoAgregados, vlo_Conexion)
                    End If
                Else
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Debe Agregar Todos los datos correctamente")
                End If
                vlo_Conexion.TransaccionCommit()
            Catch vlo_Excepcion As Exception
                Throw
                vlo_Conexion.TransaccionRollback()
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="pvo_Ds"></param>
        ''' <param name="pvc_Rowstate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ObtenerRegistrosPorEstado(ByVal pvo_Ds As DataSet, ByVal pvc_Rowstate As DataRowState) As DataSet
            Dim vlo_DsSet As DataSet
            Dim vlo_Row As DataRow
            Dim vlo_tabla As DataTable

            vlo_tabla = pvo_Ds.Tables(0).Clone
            vlo_Row = pvo_Ds.Tables(0).NewRow
            vlo_DsSet = New DataSet
            vlo_DsSet.Tables.Add(vlo_tabla)

            For Each vlo_Row In pvo_Ds.Tables(0).Rows
                If vlo_Row.RowState = pvc_Rowstate Then 'Se cambia el estado de los rows
                    vlo_DsSet.Tables(0).ImportRow(vlo_Row)
                End If
            Next
            Return vlo_DsSet
        End Function

        ''' <summary>
        ''' Borrar los usuarios sin dependencias registrados en una unidad facturadora de la tabla UsuarioFacturacion  y de MembershipProvider 
        ''' </summary>
        ''' <param name="pvo_TelefonoBorrado"></param>
        ''' <param name="pvo_Conexion"></param>
        ''' <remarks></remarks>
        ''' <author>Jeannette Chavarría Rojas</author>
        ''' <creationDate>17/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Private Sub AgregarBorrarRegistroDeTelefono(pvo_TelefonoBorrado As DataSet, pvo_TelefonoAgregar As DataSet, pvo_Conexion As ConexionOracle)
            Dim vlo_DalOtmTelefonoProveedor As DalOtmTelefonoProveedor
            Dim vlo_EntOtmTelefonoProveedor As EntOtmTelefonoProveedor

            vlo_DalOtmTelefonoProveedor = New DalOtmTelefonoProveedor(pvo_Conexion)

            Try

                If Not pvo_TelefonoBorrado.Tables(0) Is Nothing AndAlso pvo_TelefonoBorrado.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila In pvo_TelefonoBorrado.Tables(0).Rows
                        vlo_EntOtmTelefonoProveedor = New EntOtmTelefonoProveedor
                        vlo_EntOtmTelefonoProveedor.Identificacion = vlo_fila(Modelo.OTM_TELEFONO_PROVEEDOR.IDENTIFICACION, DataRowVersion.Original).ToString
                        vlo_EntOtmTelefonoProveedor.Telefono = vlo_fila(Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO, DataRowVersion.Original).ToString
                        vlo_EntOtmTelefonoProveedor.Usuario = vlo_fila(Modelo.OTM_TELEFONO_PROVEEDOR.USUARIO, DataRowVersion.Original).ToString
                        vlo_EntOtmTelefonoProveedor.TimeStamp = vlo_fila(Modelo.OTM_TELEFONO_PROVEEDOR.TIME_STAMP, DataRowVersion.Original).ToString

                        vlo_DalOtmTelefonoProveedor.BorrarRegistro(vlo_EntOtmTelefonoProveedor)
                    Next
                ElseIf Not pvo_TelefonoAgregar.Tables(0) Is Nothing AndAlso pvo_TelefonoAgregar.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila In pvo_TelefonoAgregar.Tables(0).Rows
                        vlo_EntOtmTelefonoProveedor = New EntOtmTelefonoProveedor
                        vlo_EntOtmTelefonoProveedor.Identificacion = vlo_fila(Modelo.OTM_TELEFONO_PROVEEDOR.IDENTIFICACION).ToString
                        vlo_EntOtmTelefonoProveedor.Telefono = vlo_fila(Modelo.OTM_TELEFONO_PROVEEDOR.TELEFONO).ToString
                        vlo_EntOtmTelefonoProveedor.Usuario = vlo_fila(Modelo.OTM_TELEFONO_PROVEEDOR.USUARIO).ToString
                        vlo_EntOtmTelefonoProveedor.TimeStamp = vlo_fila(Modelo.OTM_TELEFONO_PROVEEDOR.TIME_STAMP).ToString
                        vlo_DalOtmTelefonoProveedor.InsertarRegistro(vlo_EntOtmTelefonoProveedor)
                    Next
                End If
            Catch vlo_Excepcion As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Borrar los usuarios sin dependencias registrados en una unidad facturadora de la tabla UsuarioFacturacion  y de MembershipProvider 
        ''' </summary>
        ''' <param name="pvo_CorreroBorrado"></param>
        ''' <param name="pvo_CorreoAgregar"></param>
        ''' <remarks></remarks>
        ''' <author>Jeannette Chavarría Rojas</author>
        ''' <creationDate>17/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Private Sub AgregarBorrarRegistroDeCorreo(pvo_CorreroBorrado As DataSet, pvo_CorreoAgregar As DataSet, pvo_Conexion As ConexionOracle)
            Dim vlo_DalOtmCorreoProveedor As DalOtmCorreoProveedor
            Dim vlo_EntOtmCorreoProveedor As EntOtmCorreoProveedor

            vlo_DalOtmCorreoProveedor = New DalOtmCorreoProveedor(pvo_Conexion)

            Try

                If Not pvo_CorreroBorrado.Tables(0) Is Nothing AndAlso pvo_CorreroBorrado.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila In pvo_CorreroBorrado.Tables(0).Rows
                        vlo_EntOtmCorreoProveedor = New EntOtmCorreoProveedor
                        vlo_EntOtmCorreoProveedor.Identificacion = vlo_fila(Modelo.OTM_CORREO_PROVEEDOR.IDENTIFICACION, DataRowVersion.Original).ToString
                        vlo_EntOtmCorreoProveedor.Correo = vlo_fila(Modelo.OTM_CORREO_PROVEEDOR.CORREO, DataRowVersion.Original).ToString
                        vlo_EntOtmCorreoProveedor.Nombre = vlo_fila(Modelo.OTM_CORREO_PROVEEDOR.NOMBRE, DataRowVersion.Original).ToString
                        vlo_EntOtmCorreoProveedor.Usuario = vlo_fila(Modelo.OTM_CORREO_PROVEEDOR.USUARIO, DataRowVersion.Original).ToString
                        vlo_EntOtmCorreoProveedor.TimeStamp = vlo_fila(Modelo.OTM_CORREO_PROVEEDOR.TIME_STAMP, DataRowVersion.Original).ToString

                        vlo_DalOtmCorreoProveedor.BorrarRegistro(vlo_EntOtmCorreoProveedor)
                    Next
                ElseIf Not pvo_CorreoAgregar.Tables(0) Is Nothing AndAlso pvo_CorreoAgregar.Tables(0).Rows.Count > 0 Then
                    For Each vlo_fila In pvo_CorreoAgregar.Tables(0).Rows
                        vlo_EntOtmCorreoProveedor = New EntOtmCorreoProveedor
                        vlo_EntOtmCorreoProveedor.Identificacion = vlo_fila(Modelo.OTM_CORREO_PROVEEDOR.IDENTIFICACION).ToString
                        vlo_EntOtmCorreoProveedor.Correo = vlo_fila(Modelo.OTM_CORREO_PROVEEDOR.CORREO).ToString
                        vlo_EntOtmCorreoProveedor.Nombre = vlo_fila(Modelo.OTM_CORREO_PROVEEDOR.NOMBRE).ToString
                        vlo_EntOtmCorreoProveedor.Usuario = vlo_fila(Modelo.OTM_CORREO_PROVEEDOR.USUARIO).ToString
                        vlo_EntOtmCorreoProveedor.TimeStamp = vlo_fila(Modelo.OTM_CORREO_PROVEEDOR.TIME_STAMP).ToString

                        vlo_DalOtmCorreoProveedor.InsertarRegistro(vlo_EntOtmCorreoProveedor)
                    Next
                End If
            Catch vlo_Excepcion As Exception
                Throw
            End Try
        End Sub
#End Region

    End Class
End Namespace
