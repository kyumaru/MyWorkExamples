Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
    Public Class BllOtmAlmacenBodega
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
        ''' Permite agregar un registro en la tabla OTM_ALMACEN_BODEGA, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>23/05/2016 02:48:06 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmAlmacenBodega) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmAlmacenBodega As DalOtmAlmacenBodega
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacionAdministra, pvo_Registro.Descripcion).Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Descripción ya existente para su ubicación.")
                End If

                If pvo_Registro.Tipo = Tipo.ALMACEN Then
                    If ObtenerRegistroAlmacen(pvo_Registro.IdUbicacionAdministra).Existe Then
                        vln_Resultado = -1
                        Throw New OrdenesDeTrabajoException("La ubicación ya posee un almacén.")
                    End If
                End If

                vlo_DalOtmAlmacenBodega = New DalOtmAlmacenBodega(vlo_Conexion)
                vln_Resultado = vlo_DalOtmAlmacenBodega.InsertarRegistro(pvo_Registro)
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
        ''' Permite modificar un registro en la tabla OTM_ALMACEN_BODEGA, no sin antes aplicar la validación de la llave alterna
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>23/05/2016 02:48:06 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmAlmacenBodega) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmAlmacenBodega As DalOtmAlmacenBodega
            Dim vlo_EntOtmAlmacenBodega As EntOtmAlmacenBodega
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_EntOtmAlmacenBodega = ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacionAdministra, pvo_Registro.Descripcion)
                If vlo_EntOtmAlmacenBodega.Existe AndAlso vlo_EntOtmAlmacenBodega.IdAlmacenBodega <> pvo_Registro.IdAlmacenBodega Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Descripción ya existente para su ubicación.")
                End If

                'If pvo_Registro.Tipo = Tipo.ALMACEN Then
                '    If ObtenerRegistroAlmacen(pvo_Registro.IdUbicacionAdministra).Existe Then
                '        vln_Resultado = -1
                '        Throw New OrdenesDeTrabajoException("La ubicación ya posee un almacén.")
                '    End If
                'End If

                vlo_DalOtmAlmacenBodega = New DalOtmAlmacenBodega(vlo_Conexion)
                vln_Resultado = vlo_DalOtmAlmacenBodega.ModificarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTM_ALMACEN_BODEGA, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>23/05/2016 02:48:06 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmAlmacenBodega) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmAlmacenBodega As DalOtmAlmacenBodega
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdAlmacenBodega) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Hay registro asociados")
                End If

                vlo_DalOtmAlmacenBodega = New DalOtmAlmacenBodega(vlo_Conexion)
                vln_Resultado = vlo_DalOtmAlmacenBodega.BorrarRegistro(pvo_Registro)
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
        ''' Permite obtener un registro según su llave alterna
        ''' </summary>
        ''' <param name="pvn_IdUbicacionAdministra">Id de la ubicación que administra los datos del catálogo</param>
        ''' <param name="pvc_Descripcion">Descripción del almacén</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>23/05/2016 02:48:06 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroPorLlaveAlterna(pvn_IdUbicacionAdministra As Integer, pvc_Descripcion As String) As EntOtmAlmacenBodega
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmAlmacenBodega As DalOtmAlmacenBodega

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmAlmacenBodega = New DalOtmAlmacenBodega(vlo_Conexion)
                Return vlo_DalOtmAlmacenBodega.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'", Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTM_ALMACEN_BODEGA.DESCRIPCION, pvc_Descripcion.ToUpper()))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Permite obtener un registro según la existencia de taller
        ''' </summary>
        ''' <param name="pvn_IdUbicacionAdministra">Id de la ubicación que administra los datos del catálogo</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>24/05/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Function ObtenerRegistroAlmacen(pvn_IdUbicacionAdministra As Integer) As EntOtmAlmacenBodega
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmAlmacenBodega As DalOtmAlmacenBodega

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmAlmacenBodega = New DalOtmAlmacenBodega(vlo_Conexion)
                Return vlo_DalOtmAlmacenBodega.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'", Modelo.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA, pvn_IdUbicacionAdministra, Modelo.OTM_ALMACEN_BODEGA.TIPO, Tipo.ALMACEN))
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
        ''' <param name="pvn_IdAlmacenBodega">Llave primaria de la tabla otm_almacen_bodega que se asocia con la secuencia sq_id_almacen_bodega</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>23/05/2016 02:48:06 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdAlmacenBodega As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOtfInventario As DalOtfInventario

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

                'Determinar la existencia de registros asociados en la tabla OTF_INVENTARIO
                vlo_DalOtfInventario = New DalOtfInventario(vlo_Conexion)
                If vlo_DalOtfInventario.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTF_INVENTARIO.ID_ALMACEN_BODEGA, pvn_IdAlmacenBodega)).Existe Then
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
        ''' Prepara la lista de la union entre via de compra contrato y almacenes y bodegas
        ''' </summary>
        ''' <param name="pvc_Condicion"></param>
        ''' <param name="pvc_Orden"></param>
        ''' <param name="pvb_Paginar"></param>
        ''' <param name="pvn_NumeroPagina"></param>
        ''' <param name="pvn_TamanoPagina"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermudez G</author>
        ''' <creationDate>09/06/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarViaCompraAlmacen(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOtmAlmacenBodega As DalOtmAlmacenBodega
            Dim vlo_DalOtmViaCompraContrato As DalOtmViaCompraContrato
            Dim vlo_dsDatosAlmacen As DataSet
            Dim vlo_dsDatosViaCompra As DataSet
            Dim vlo_dsResultado As DataSet
            Dim vlo_columna As DataColumn
            Dim vlo_NuevaFila As DataRow

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If


                vlo_DalOtmAlmacenBodega = New DalOtmAlmacenBodega(vlo_Conexion)
                vlo_DalOtmViaCompraContrato = New DalOtmViaCompraContrato(vlo_Conexion)

                vlo_dsDatosAlmacen = vlo_DalOtmAlmacenBodega.ListarRegistrosLista(pvc_Condicion, String.Format("{0} {1}", Modelo.OTM_ALMACEN_BODEGA.TIPO, Ordenamiento.ASCENDENTE), pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)
                vlo_dsDatosViaCompra = vlo_DalOtmViaCompraContrato.ListarRegistrosLista(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

                'Se crea un nuevo dataset
                vlo_dsResultado = New DataSet("ViaCompraAlmacen")

                'Se crea una nueva tabla y se agrega al dataset
                vlo_dsResultado.Tables.Add("ViasAlmacenes")

                vlo_columna = New DataColumn()
                'Se asigna un tipo de dato a la columna recientemente creada
                vlo_columna.DataType = System.Type.GetType("System.String")
                'Se le da nombre a esta columna
                vlo_columna.ColumnName = "DESCRIPCION"
                'Se agrega la columna configurada al set de datos
                vlo_dsResultado.Tables(0).Columns.Add(vlo_columna)
                'Se agrega al arreglo de llaves primarias la columna

                vlo_columna = New DataColumn()
                'Se asigna un tipo de dato a la columna recientemente creada
                vlo_columna.DataType = System.Type.GetType("System.String")
                'Se le da nombre a esta columna
                vlo_columna.ColumnName = "ID_AMBITO"
                'Se agrega la columna configurada al set de datos
                vlo_dsResultado.Tables(0).Columns.Add(vlo_columna)
                'Se agrega al arreglo de llaves primarias la columna

                For Each vlo_fila As DataRow In vlo_dsDatosAlmacen.Tables(0).Rows
                    vlo_NuevaFila = vlo_dsResultado.Tables(0).NewRow
                    vlo_NuevaFila("DESCRIPCION") = vlo_fila(Modelo.V_OTM_ALMACEN_BODEGA.DESCRIPCION)
                    vlo_NuevaFila("ID_AMBITO") = String.Format("{0}_{1}", vlo_fila(Modelo.V_OTM_ALMACEN_BODEGA.ID_ALMACEN_BODEGA), vlo_fila(Modelo.V_OTM_ALMACEN_BODEGA.TIPO))

                    vlo_dsResultado.Tables(0).Rows.Add(vlo_NuevaFila)
                Next

                For Each vlo_fila As DataRow In vlo_dsDatosViaCompra.Tables(0).Rows
                    vlo_NuevaFila = vlo_dsResultado.Tables(0).NewRow
                    vlo_NuevaFila("DESCRIPCION") = vlo_fila(Modelo.V_OTM_VIA_COMPRA_CONTRATO.DESCRIPCION)
                    vlo_NuevaFila("ID_AMBITO") = String.Format("{0}_{1}", vlo_fila(Modelo.V_OTM_VIA_COMPRA_CONTRATO.ID_VIA_COMPRA_CONTRATO), ViaDespacho.VIACOMPRA)

                    vlo_dsResultado.Tables(0).Rows.Add(vlo_NuevaFila)
                Next

                Return vlo_dsResultado

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
