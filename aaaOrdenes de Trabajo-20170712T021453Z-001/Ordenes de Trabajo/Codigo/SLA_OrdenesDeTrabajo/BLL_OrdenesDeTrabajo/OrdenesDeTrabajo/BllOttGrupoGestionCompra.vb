Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttGrupoGestionCompra
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
        ''' Permite agregar un registro en la tabla OTT_GRUPO_GESTION_COMPRA, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttGrupoGestionCompra) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGrupoGestionCompra As DalOttGrupoGestionCompra
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlavePrimaria(pvo_Registro.IdUbicacion, pvo_Registro.IdViaCompraContrato, pvo_Registro.Anno, pvo_Registro.NumeroGestion).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave primaria repetida")
                End If

                If ObtenerRegistroPorLlaveAlterna(pvo_Registro.NumeroLinea).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave alterna repetida")
                End If

                vlo_DalOttGrupoGestionCompra = New DalOttGrupoGestionCompra(vlo_Conexion)
                vln_Resultado = vlo_DalOttGrupoGestionCompra.InsertarRegistro(pvo_Registro)
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
        ''' Permite modificar un registro en la tabla OTT_GRUPO_GESTION_COMPRA, y un archivo adjunto a la gestion
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>24/10/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function TramitarGestionCompraUnidadEspecializadaGECO(pvo_Registro As EntOttGestionCompra, pvo_EntOttAdjuntoGestionCompr As EntOttAdjuntoGestionCompr) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOttAdjuntoGestionCompr As DalOttAdjuntoGestionCompr
            Dim vlo_EntOttAdjuntoGestionComprAntiguo As EntOttAdjuntoGestionCompr
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

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOttAdjuntoGestionCompr = New DalOttAdjuntoGestionCompr(vlo_Conexion)

                vlo_DalOttGestionCompra.ModificarRegistro(pvo_Registro)

                vlo_EntOttAdjuntoGestionComprAntiguo = vlo_DalOttAdjuntoGestionCompr.ObtenerRegistro(
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = AND {5} = {6} AND {7} = {8}",
                                  Modelo.OTT_ADJUNTO_GESTION_COMPR.ID_UBICACION, pvo_Registro.IdUbicacion,
                                  Modelo.OTT_ADJUNTO_GESTION_COMPR.ID_VIA_COMPRA_CONTRATO, pvo_Registro.IdViaCompraContrato,
                                  Modelo.OTT_ADJUNTO_GESTION_COMPR.ANNO, pvo_Registro.Anno,
                                  Modelo.OTT_ADJUNTO_GESTION_COMPR.NUMERO_GESTION, pvo_Registro.NumeroGestion,
                                  Modelo.OTT_ADJUNTO_GESTION_COMPR.ID_TIPO_DOCUMENTO, TipoDocumento.SOLICITUD_GECO))

                If vlo_EntOttAdjuntoGestionComprAntiguo.Existe Then
                    vlo_DalOttAdjuntoGestionCompr.BorrarRegistro(vlo_EntOttAdjuntoGestionComprAntiguo)
                End If

                pvo_EntOttAdjuntoGestionCompr.IdUbicacion = pvo_Registro.IdUbicacion
                pvo_EntOttAdjuntoGestionCompr.IdViaCompraContrato = pvo_Registro.IdViaCompraContrato
                pvo_EntOttAdjuntoGestionCompr.Anno = pvo_Registro.Anno
                pvo_EntOttAdjuntoGestionCompr.NumeroGestion = pvo_Registro.NumeroGestion
                pvo_EntOttAdjuntoGestionCompr.IdTipoDocumento = TipoDocumento.SOLICITUD_GECO
                vlo_DalOttAdjuntoGestionCompr.InsertarRegistro(pvo_EntOttAdjuntoGestionCompr)

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
        ''' Permite modificar un registro en la tabla OTT_GRUPO_GESTION_COMPRA, y un archivo adjunto a la gestion
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>24/10/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function GuardarGestionCompraUnidadEspecializadaGECO(pvo_Registro As EntOttGestionCompra, pvo_EntOttAdjuntoGestionCompr As EntOttAdjuntoGestionCompr, pvb_PoseeArchivo As Boolean) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGestionCompra As DalOttGestionCompra
            Dim vlo_DalOttAdjuntoGestionCompr As DalOttAdjuntoGestionCompr
            Dim vlo_EntOttAdjuntoGestionComprAntiguo As EntOttAdjuntoGestionCompr
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

                vlo_DalOttGestionCompra = New DalOttGestionCompra(vlo_Conexion)
                vlo_DalOttAdjuntoGestionCompr = New DalOttAdjuntoGestionCompr(vlo_Conexion)

                vlo_DalOttGestionCompra.ModificarRegistro(pvo_Registro)

                vlo_EntOttAdjuntoGestionComprAntiguo = vlo_DalOttAdjuntoGestionCompr.ObtenerRegistro(
                    String.Format("{0} = {1} AND {2} = {3} AND {4} = AND {5} = {6} AND {7} = {8}",
                                  Modelo.OTT_ADJUNTO_GESTION_COMPR.ID_UBICACION, pvo_Registro.IdUbicacion,
                                  Modelo.OTT_ADJUNTO_GESTION_COMPR.ID_VIA_COMPRA_CONTRATO, pvo_Registro.IdViaCompraContrato,
                                  Modelo.OTT_ADJUNTO_GESTION_COMPR.ANNO, pvo_Registro.Anno,
                                  Modelo.OTT_ADJUNTO_GESTION_COMPR.NUMERO_GESTION, pvo_Registro.NumeroGestion,
                                  Modelo.OTT_ADJUNTO_GESTION_COMPR.ID_TIPO_DOCUMENTO, TipoDocumento.SOLICITUD_GECO))

                If vlo_EntOttAdjuntoGestionComprAntiguo.Existe Then
                    vlo_DalOttAdjuntoGestionCompr.BorrarRegistro(vlo_EntOttAdjuntoGestionComprAntiguo)
                End If

                If pvb_PoseeArchivo Then
                    pvo_EntOttAdjuntoGestionCompr.IdUbicacion = pvo_Registro.IdUbicacion
                    pvo_EntOttAdjuntoGestionCompr.IdViaCompraContrato = pvo_Registro.IdViaCompraContrato
                    pvo_EntOttAdjuntoGestionCompr.Anno = pvo_Registro.Anno
                    pvo_EntOttAdjuntoGestionCompr.NumeroGestion = pvo_Registro.NumeroGestion
                    pvo_EntOttAdjuntoGestionCompr.IdTipoDocumento = TipoDocumento.SOLICITUD_GECO
                    vlo_DalOttAdjuntoGestionCompr.InsertarRegistro(pvo_EntOttAdjuntoGestionCompr)
                End If

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
        ''' Permite modificar dos registros en la tabla OTT_GRUPO_GESTION_COMPRA
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>14/10/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function SubirNumeroLinea(pvo_Registro As EntOttGestionCompra, pvn_NumeroLinea As Integer, pvc_UsuarioSession As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGrupoGestionCompra As DalOttGrupoGestionCompra
            Dim vlo_GrupoSubir As EntOttGrupoGestionCompra
            Dim vlo_GrupoBajar As EntOttGrupoGestionCompra
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

                vlo_DalOttGrupoGestionCompra = New DalOttGrupoGestionCompra(vlo_Conexion)

                vlo_GrupoSubir = vlo_DalOttGrupoGestionCompra.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}",
                                      Modelo.OTT_GRUPO_GESTION_COMPRA.ID_UBICACION, pvo_Registro.IdUbicacion,
                                       Modelo.OTT_GRUPO_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvo_Registro.IdViaCompraContrato,
                                        Modelo.OTT_GRUPO_GESTION_COMPRA.ANNO, pvo_Registro.Anno,
                                         Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_GESTION, pvo_Registro.NumeroGestion,
                                          Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_LINEA, pvn_NumeroLinea))

                vlo_GrupoBajar = vlo_DalOttGrupoGestionCompra.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}",
                                      Modelo.OTT_GRUPO_GESTION_COMPRA.ID_UBICACION, pvo_Registro.IdUbicacion,
                                       Modelo.OTT_GRUPO_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvo_Registro.IdViaCompraContrato,
                                        Modelo.OTT_GRUPO_GESTION_COMPRA.ANNO, pvo_Registro.Anno,
                                         Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_GESTION, pvo_Registro.NumeroGestion,
                                          Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_LINEA, (pvn_NumeroLinea - 1)))

                vlo_DalOttGrupoGestionCompra.BorrarRegistro(vlo_GrupoSubir)
                vlo_DalOttGrupoGestionCompra.BorrarRegistro(vlo_GrupoBajar)

                vlo_GrupoSubir.NumeroLinea = (pvn_NumeroLinea - 1)
                vlo_GrupoSubir.Usuario = pvc_UsuarioSession
                vlo_GrupoBajar.NumeroLinea = pvn_NumeroLinea
                vlo_GrupoBajar.Usuario = pvc_UsuarioSession

                vlo_DalOttGrupoGestionCompra.InsertarRegistro(vlo_GrupoSubir)
                vlo_DalOttGrupoGestionCompra.InsertarRegistro(vlo_GrupoBajar)

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
        ''' Permite modificar dos registros en la tabla OTT_GRUPO_GESTION_COMPRA
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>14/10/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function BajarNumeroLinea(pvo_Registro As EntOttGestionCompra, pvn_NumeroLinea As Integer, pvc_UsuarioSession As String) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGrupoGestionCompra As DalOttGrupoGestionCompra
            Dim vlo_GrupoSubir As EntOttGrupoGestionCompra
            Dim vlo_GrupoBajar As EntOttGrupoGestionCompra
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

                vlo_DalOttGrupoGestionCompra = New DalOttGrupoGestionCompra(vlo_Conexion)

                vlo_GrupoBajar = vlo_DalOttGrupoGestionCompra.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}",
                                      Modelo.OTT_GRUPO_GESTION_COMPRA.ID_UBICACION, pvo_Registro.IdUbicacion,
                                       Modelo.OTT_GRUPO_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvo_Registro.IdViaCompraContrato,
                                        Modelo.OTT_GRUPO_GESTION_COMPRA.ANNO, pvo_Registro.Anno,
                                         Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_GESTION, pvo_Registro.NumeroGestion,
                                          Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_LINEA, pvn_NumeroLinea))

                vlo_GrupoSubir = vlo_DalOttGrupoGestionCompra.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}",
                                      Modelo.OTT_GRUPO_GESTION_COMPRA.ID_UBICACION, pvo_Registro.IdUbicacion,
                                       Modelo.OTT_GRUPO_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvo_Registro.IdViaCompraContrato,
                                        Modelo.OTT_GRUPO_GESTION_COMPRA.ANNO, pvo_Registro.Anno,
                                         Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_GESTION, pvo_Registro.NumeroGestion,
                                          Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_LINEA, (pvn_NumeroLinea + 1)))

                vlo_DalOttGrupoGestionCompra.BorrarRegistro(vlo_GrupoBajar)
                vlo_DalOttGrupoGestionCompra.BorrarRegistro(vlo_GrupoSubir)

                vlo_GrupoBajar.NumeroLinea = (pvn_NumeroLinea + 1)
                vlo_GrupoBajar.Usuario = pvc_UsuarioSession
                vlo_GrupoSubir.NumeroLinea = pvn_NumeroLinea
                vlo_GrupoSubir.Usuario = pvc_UsuarioSession

                vlo_DalOttGrupoGestionCompra.InsertarRegistro(vlo_GrupoBajar)
                vlo_DalOttGrupoGestionCompra.InsertarRegistro(vlo_GrupoSubir)

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
        ''' Permite modificar un registro en la tabla OTT_GRUPO_GESTION_COMPRA, no sin antes aplicar la validación de la llave alterna
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarRegistro(ByVal pvo_Registro As EntOttGrupoGestionCompra) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGrupoGestionCompra As DalOttGrupoGestionCompra
            Dim vlo_EntOttGrupoGestionCompra As EntOttGrupoGestionCompra
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_EntOttGrupoGestionCompra = ObtenerRegistroPorLlaveAlterna(pvo_Registro.NumeroLinea)
                If vlo_EntOttGrupoGestionCompra.Existe AndAlso vlo_EntOttGrupoGestionCompra.IdUbicacion <> pvo_Registro.IdUbicacion AndAlso vlo_EntOttGrupoGestionCompra.IdViaCompraContrato <> pvo_Registro.IdViaCompraContrato AndAlso vlo_EntOttGrupoGestionCompra.Anno <> pvo_Registro.Anno AndAlso vlo_EntOttGrupoGestionCompra.NumeroGestion <> pvo_Registro.NumeroGestion AndAlso vlo_EntOttGrupoGestionCompra.IdGrupoGestionCompra <> pvo_Registro.IdGrupoGestionCompra Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave alterna repetida")
                End If

                vlo_DalOttGrupoGestionCompra = New DalOttGrupoGestionCompra(vlo_Conexion)
                vln_Resultado = vlo_DalOttGrupoGestionCompra.ModificarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTT_GRUPO_GESTION_COMPRA, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOttGrupoGestionCompra) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGrupoGestionCompra As DalOttGrupoGestionCompra
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdUbicacion, pvo_Registro.IdViaCompraContrato, pvo_Registro.Anno, pvo_Registro.NumeroGestion, pvo_Registro.IdGrupoGestionCompra) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Hay registro asociados")
                End If

                vlo_DalOttGrupoGestionCompra = New DalOttGrupoGestionCompra(vlo_Conexion)
                vln_Resultado = vlo_DalOttGrupoGestionCompra.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvn_IdViaCompraContrato">Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato</param>
        ''' <param name="pvn_Anno">Año</param>
        ''' <param name="pvn_NumeroGestion">Consecutivo de la gestión</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_Anno As Integer, pvn_NumeroGestion As Integer) As EntOttGrupoGestionCompra
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGrupoGestionCompra As DalOttGrupoGestionCompra

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttGrupoGestionCompra = New DalOttGrupoGestionCompra(vlo_Conexion)
                Return vlo_DalOttGrupoGestionCompra.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_GRUPO_GESTION_COMPRA.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_GRUPO_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_GRUPO_GESTION_COMPRA.ANNO, pvn_Anno, Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_GESTION, pvn_NumeroGestion))
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
        ''' <param name="pvn_NumeroLinea">Número de línea que agrupa las solicitudes de un mismo material. define el orden.</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlaveAlterna(pvn_NumeroLinea As Integer) As EntOttGrupoGestionCompra
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttGrupoGestionCompra As DalOttGrupoGestionCompra

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttGrupoGestionCompra = New DalOttGrupoGestionCompra(vlo_Conexion)
                Return vlo_DalOttGrupoGestionCompra.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_GRUPO_GESTION_COMPRA.NUMERO_LINEA, pvn_NumeroLinea))
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
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvn_IdViaCompraContrato">Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato</param>
        ''' <param name="pvn_Anno">Año</param>
        ''' <param name="pvn_NumeroGestion">Consecutivo de la gestión</param>
        ''' <param name="pvn_IdGrupoGestionCompra">Llave de la tabla ott_grupo_gestion_compra asociada a la secuencia sq_id_grupo_gestion_compra</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>25/08/2016 12:17:45 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_Anno As Integer, pvn_NumeroGestion As Integer, pvn_IdGrupoGestionCompra As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOttOfertaProveedor As DalOttOfertaProveedor

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

                'Determinar la existencia de registros asociados en la tabla OTT_OFERTA_PROVEEDOR
                vlo_DalOttOfertaProveedor = New DalOttOfertaProveedor(vlo_Conexion)
                If vlo_DalOttOfertaProveedor.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_OFERTA_PROVEEDOR.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_OFERTA_PROVEEDOR.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_OFERTA_PROVEEDOR.ANNO, pvn_Anno, Modelo.OTT_OFERTA_PROVEEDOR.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTT_OFERTA_PROVEEDOR.ID_GRUPO_GESTION_COMPRA, pvn_IdGrupoGestionCompra)).Existe Then
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

#End Region

    End Class
End Namespace
