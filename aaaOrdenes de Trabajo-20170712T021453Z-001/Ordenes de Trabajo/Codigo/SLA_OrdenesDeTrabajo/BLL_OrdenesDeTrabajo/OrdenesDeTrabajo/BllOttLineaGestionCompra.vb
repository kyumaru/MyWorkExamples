Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOttLineaGestionCompra
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
        ''' Permite agregar un registro en la tabla OTT_LINEA_GESTION_COMPRA, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOttLineaGestionCompra) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
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

                If ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacion, pvo_Registro.IdViaCompraContrato, pvo_Registro.Anno, pvo_Registro.NumeroGestion, pvo_Registro.NumeroLinea).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave alterna repetida")
                End If

                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vln_Resultado = vlo_DalOttLineaGestionCompra.InsertarRegistro(pvo_Registro)
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
        ''' Permite modificar un registro en la tabla OTT_LINEA_GESTION_COMPRA, no sin antes aplicar la validación de la llave alterna
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarRegistro(ByVal pvo_Registro As EntOttLineaGestionCompra) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_EntOttLineaGestionCompra As EntOttLineaGestionCompra
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_EntOttLineaGestionCompra = ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacion, pvo_Registro.IdViaCompraContrato, pvo_Registro.Anno, pvo_Registro.NumeroGestion, pvo_Registro.NumeroLinea)
                If vlo_EntOttLineaGestionCompra.Existe AndAlso vlo_EntOttLineaGestionCompra.IdUbicacion <> pvo_Registro.IdUbicacion AndAlso vlo_EntOttLineaGestionCompra.IdViaCompraContrato <> pvo_Registro.IdViaCompraContrato AndAlso vlo_EntOttLineaGestionCompra.Anno <> pvo_Registro.Anno AndAlso vlo_EntOttLineaGestionCompra.NumeroGestion <> pvo_Registro.NumeroGestion AndAlso vlo_EntOttLineaGestionCompra.IdLineaGestionCompra <> pvo_Registro.IdLineaGestionCompra Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("[TODO] Llave alterna repetida")
                End If

                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vln_Resultado = vlo_DalOttLineaGestionCompra.ModificarRegistro(pvo_Registro)
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
        ''' <param name="pvn_NumeroGestion">Consecutivo de la gestión. es anual por ubicación y vía de compra.</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlavePrimaria(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_Anno As Integer, pvn_NumeroGestion As Integer) As EntOttLineaGestionCompra
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                Return vlo_DalOttLineaGestionCompra.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7}", Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_LINEA_GESTION_COMPRA.ANNO, pvn_Anno, Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION, pvn_NumeroGestion))
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
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvn_IdViaCompraContrato">Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_compra_contrato</param>
        ''' <param name="pvn_Anno">Año</param>
        ''' <param name="pvn_NumeroGestion">Consecutivo de la gestión. es anual por ubicación y vía de compra.</param>
        ''' <param name="pvn_NumeroLinea">Número de línea para gestiones por unidad especialidad de compra</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlaveAlterna(pvn_IdUbicacion As Integer, pvn_IdViaCompraContrato As Integer, pvn_Anno As Integer, pvn_NumeroGestion As Integer, pvn_NumeroLinea As Integer) As EntOttLineaGestionCompra
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                Return vlo_DalOttLineaGestionCompra.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.OTT_LINEA_GESTION_COMPRA.ID_UBICACION, pvn_IdUbicacion, Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato, Modelo.OTT_LINEA_GESTION_COMPRA.ANNO, pvn_Anno, Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION, pvn_NumeroGestion, Modelo.OTT_LINEA_GESTION_COMPRA.NUMERO_LINEA, pvn_NumeroLinea))
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
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>18/08/2016 02:41:55 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarRegistrosListaReporte(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_DalOtpParametro As DalOtpParametro
            Dim vlo_EntOtpParametro As EntOtpParametro
            Dim vlc_Condicion As String = String.Empty

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOtpParametro = New DalOtpParametro(vlo_Conexion)
                vlo_EntOtpParametro = New EntOtpParametro
                vlc_Condicion = String.Format("{0} = {1}", Modelo.OTP_PARAMETRO_UBICACION.ID_PARAMETRO, Parametros.VALOR_SECUENCIA_COMPRA_RAPIDA)
                vlo_EntOtpParametro = vlo_DalOttLineaGestionCompra.ObtenerRegistro(vlc_Condicion)
                If vlo_EntOtpParametro.Existe Then
                    pvc_Orden = String.Format("{0} AND {1} = {2}", pvc_Orden, Modelo.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, vlo_EntOtpParametro.Valor.Trim)
                    Return vlo_DalOttLineaGestionCompra.ListarRegistrosLista(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)
                Else
                    Return vlo_DalOttLineaGestionCompra.ListarRegistrosLista(String.Empty, String.Empty, False, 0, 0)
                End If
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
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>26/08/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtLineaGestCompGroupPersonalizado(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As Data.DataSet
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsRegistros As Data.DataSet
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_EntOtmMaterial As EntOtmMaterial
            Dim vlo_EntOtmUnidadMedida As EntOtmUnidadMedida
            Dim vlo_DalOtmMaterial As DalOtmMaterial
            Dim vlo_DalOtmUnidadMedida As DalOtmUnidadMedida
            Dim vlc_Proveedores As String
            Dim vlo_DsOfertaProveedor As DataSet
            Dim vlo_DalOttOfertaProveedor As DalOttOfertaProveedor
            Dim vlo_Row As DataRow
            Dim vlo_EntOttGrupoGestionCompra As EntOttGrupoGestionCompra
            Dim vlo_DalOttGrupoGestionCompra As DalOttGrupoGestionCompra
            Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOtmMaterial = New DalOtmMaterial(vlo_Conexion)
                vlo_DalOtmUnidadMedida = New DalOtmUnidadMedida(vlo_Conexion)
                vlo_DalOttOfertaProveedor = New DalOttOfertaProveedor(vlo_Conexion)
                vlo_DalOttGrupoGestionCompra = New DalOttGrupoGestionCompra(vlo_Conexion)
                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)

                vlo_DsRegistros = vlo_DalOttLineaGestionCompra.ListarVOtLineaGestCompGroup(
                    pvc_Condicion,
                    pvc_Orden,
                    pvb_Paginar,
                    pvn_NumeroPagina,
                    pvn_TamanoPagina)

                For Each vlo_Fila In vlo_DsRegistros.Tables(0).Rows

                    vlo_EntOtmMaterial = vlo_DalOtmMaterial.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_MATERIAL.ID_UBICACION_ADMINISTRA, vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_UBICACION).ToString, Modelo.OTM_MATERIAL.ID_MATERIAL, vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_MATERIAL).ToString))

                    vlo_EntOtmUnidadMedida = vlo_DalOtmUnidadMedida.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_UNIDAD_MEDIDA.ID_UNIDAD_MEDIDA, vlo_EntOtmMaterial.IdUnidadMedida))

                    'Se obtiene el ID_GRUPO_GESTION_COMPRA para obtener la lista de ofertas para esa linea de la gestion de compra
                    vlo_EntOttGrupoGestionCompra = vlo_DalOttGrupoGestionCompra.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.V_OTT_GRUPO_GESTION_COMPRA.ID_UBICACION, vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_UBICACION), Modelo.V_OTT_GRUPO_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO, vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_VIA_COMPRA_CONTRATO), Modelo.V_OTT_GRUPO_GESTION_COMPRA.ANNO, vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.ANNO), Modelo.V_OTT_GRUPO_GESTION_COMPRA.NUMERO_GESTION, vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.NUMERO_GESTION), Modelo.V_OTT_GRUPO_GESTION_COMPRA.ID_MATERIAL, vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_MATERIAL)))

                    If vlo_EntOttGrupoGestionCompra.Existe Then
                        'Se obtiene las ofertas para la linea de gestion de compra
                        vlo_DsOfertaProveedor = vlo_DalOttOfertaProveedor.ListarRegistrosLista(
                        String.Format("{0} = {1} AND {2} = {3} AND {4} = {5} AND {6} = {7} AND {8} = {9}", Modelo.V_OTT_OFERTA_PROVEEDORLST.ID_UBICACION, vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_UBICACION), Modelo.V_OTT_OFERTA_PROVEEDORLST.ID_VIA_COMPRA_CONTRATO, vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.ID_VIA_COMPRA_CONTRATO), Modelo.V_OTT_OFERTA_PROVEEDORLST.ANNO, vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.ANNO), Modelo.V_OTT_OFERTA_PROVEEDORLST.NUMERO_GESTION, vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.NUMERO_GESTION), Modelo.V_OTT_OFERTA_PROVEEDORLST.ID_GRUPO_GESTION_COMPRA, vlo_EntOttGrupoGestionCompra.IdGrupoGestionCompra),
                        String.Empty,
                        False,
                        0,
                        0)

                        If vlo_DsOfertaProveedor IsNot Nothing AndAlso vlo_DsOfertaProveedor.Tables(0).Rows.Count > 0 Then
                            For Each vlo_Row In vlo_DsOfertaProveedor.Tables(0).Rows
                                vlc_Proveedores = String.Format("{0}{1}: {2}&nbsp;&nbsp;&nbsp;&nbsp;", vlc_Proveedores, vlo_Row(Modelo.V_OTT_OFERTA_PROVEEDORLST.NOMBRE_PROVEEDOR), vlo_Row(Modelo.V_OTT_OFERTA_PROVEEDORLST.MONTO))
                            Next
                            vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.PROVEEDORES) = vlc_Proveedores
                        End If
                    End If
                    

                    vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.DESCRIPCION) = vlo_EntOtmMaterial.Descripcion
                    vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.CANTIDAD_SOLICITADA_MEDIDA) = String.Format("{0} {1}", vlo_Fila(Modelo.V_OT_LINEA_GEST_COMP_GROUP.CANTIDAD_SOLICITADA), vlo_EntOtmUnidadMedida.Descripcion)
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
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>26/08/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtLineaGcGroupFondoPersonalizado(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As Data.DataSet
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsRegistros As Data.DataSet
            Dim vlo_DalOttLineaGestionCompra As DalOttLineaGestionCompra
            Dim vlo_EntOtmMaterial As EntOtmMaterial
            Dim vlo_EntOtmUnidadMedida As EntOtmUnidadMedida
            Dim vlo_DalOtmMaterial As DalOtmMaterial
            Dim vlo_DalOtmUnidadMedida As DalOtmUnidadMedida
            Dim vlc_Proveedores As String
            Dim vlo_DsOfertaProveedor As DataSet
            Dim vlo_DalOttOfertaProveedor As DalOttOfertaProveedor
            Dim vlo_Row As DataRow
            Dim vlo_DalOttGrupoGestionCompra As DalOttGrupoGestionCompra
            Dim vlo_EntOttDetalleMaterial As EntOttDetalleMaterial
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOttLineaGestionCompra = New DalOttLineaGestionCompra(vlo_Conexion)
                vlo_DalOtmMaterial = New DalOtmMaterial(vlo_Conexion)
                vlo_DalOtmUnidadMedida = New DalOtmUnidadMedida(vlo_Conexion)
                vlo_DalOttOfertaProveedor = New DalOttOfertaProveedor(vlo_Conexion)
                vlo_DalOttGrupoGestionCompra = New DalOttGrupoGestionCompra(vlo_Conexion)
                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)

                vlo_DsRegistros = vlo_DalOttLineaGestionCompra.ListarVOtLineaGcGroupFondo(
                    pvc_Condicion,
                    pvc_Orden,
                    pvb_Paginar,
                    pvn_NumeroPagina,
                    pvn_TamanoPagina)

                For Each vlo_Fila In vlo_DsRegistros.Tables(0).Rows

                    vlo_EntOtmMaterial = vlo_DalOtmMaterial.ObtenerRegistro(String.Format("{0} = {1} AND {2} = {3}", Modelo.OTM_MATERIAL.ID_UBICACION_ADMINISTRA, vlo_Fila(Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_UBICACION).ToString, Modelo.OTM_MATERIAL.ID_MATERIAL, vlo_Fila(Modelo.V_OT_LINEA_GC_GROUP_FONDO.ID_MATERIAL).ToString))

                    vlo_EntOtmUnidadMedida = vlo_DalOtmUnidadMedida.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_UNIDAD_MEDIDA.ID_UNIDAD_MEDIDA, vlo_EntOtmMaterial.IdUnidadMedida))

                    vlo_Fila(Modelo.V_OT_LINEA_GC_GROUP_FONDO.DESCRIPCION) = vlo_EntOtmMaterial.Descripcion
                    vlo_Fila(Modelo.V_OT_LINEA_GC_GROUP_FONDO.CANTIDAD_SOLICITADA_MEDIDA) = String.Format("{0} {1}", vlo_Fila(Modelo.V_OT_LINEA_GC_GROUP_FONDO.CANTIDAD_SOLICITADA), vlo_EntOtmUnidadMedida.Descripcion)
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
            End Try
        End Function

#End Region

    End Class
End Namespace
