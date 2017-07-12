Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo

Namespace OrdenesDeTrabajo.LogicaNegocio.Catalogos
    Public Class BllOtmViaCompraContrato
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
        ''' Permite agregar un registro en la tabla OTM_VIA_COMPRA_CONTRATO, no sin antes aplicar la validación de la llave primaria o alterna según corresponda
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>27/05/2016 02:21:34 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function InsertarRegistro(ByVal pvo_Registro As EntOtmViaCompraContrato) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmViaCompraContrato As DalOtmViaCompraContrato
            Dim vln_Resultado As Integer
            Dim vlo_EntOtmViaCompraContrato As EntOtmViaCompraContrato

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If
                vlo_EntOtmViaCompraContrato = ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacion, pvo_Registro.Descripcion)
                If vlo_EntOtmViaCompraContrato.Existe Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Vía de compra contrato ya existente en el listado.")
                End If

                If pvo_Registro.Ambito = Ambito.CONTRATACIONES Then

                    vlo_EntOtmViaCompraContrato = ValidarTopeEconomico(pvo_Registro.TopeEconomico, pvo_Registro.Ambito)
                    If vlo_EntOtmViaCompraContrato.Existe AndAlso vlo_EntOtmViaCompraContrato.IdViaCompraContrato <> pvo_Registro.IdViaCompraContrato Then
                        vln_Resultado = -1
                        Throw New OrdenesDeTrabajoException("El monto ingresado ya existe para otra vía de contrato.")
                    End If

                End If

                vlo_DalOtmViaCompraContrato = New DalOtmViaCompraContrato(vlo_Conexion)
                vln_Resultado = vlo_DalOtmViaCompraContrato.InsertarRegistro(pvo_Registro)
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
        ''' Permite modificar un registro en la tabla OTM_VIA_COMPRA_CONTRATO, no sin antes aplicar la validación de la llave alterna
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>27/05/2016 02:21:34 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ModificarRegistro(ByVal pvo_Registro As EntOtmViaCompraContrato) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmViaCompraContrato As DalOtmViaCompraContrato
            Dim vlo_EntOtmViaCompraContrato As EntOtmViaCompraContrato
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_EntOtmViaCompraContrato = ObtenerRegistroPorLlaveAlterna(pvo_Registro.IdUbicacion, pvo_Registro.Descripcion)
                If vlo_EntOtmViaCompraContrato.Existe AndAlso vlo_EntOtmViaCompraContrato.IdViaCompraContrato <> pvo_Registro.IdViaCompraContrato Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("Vía de compra contrato ya existente en el listado.")
                End If

                If pvo_Registro.Ambito = Ambito.CONTRATACIONES Then

                    vlo_EntOtmViaCompraContrato = ValidarTopeEconomico(pvo_Registro.TopeEconomico, pvo_Registro.Ambito)
                    If vlo_EntOtmViaCompraContrato.Existe AndAlso vlo_EntOtmViaCompraContrato.IdViaCompraContrato <> pvo_Registro.IdViaCompraContrato Then
                        vln_Resultado = -1
                        Throw New OrdenesDeTrabajoException("El monto ingresado ya existe para otra vía de contrato.")
                    End If

                End If

                vlo_DalOtmViaCompraContrato = New DalOtmViaCompraContrato(vlo_Conexion)
                vln_Resultado = vlo_DalOtmViaCompraContrato.ModificarRegistro(pvo_Registro)
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
        ''' Permite borrar un registro en la tabla OTM_VIA_COMPRA_CONTRATO, no sin antes aplicar la validación de dependencia con tablas relacionadas
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>27/05/2016 02:21:34 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarRegistro(ByVal pvo_Registro As EntOtmViaCompraContrato) As Integer
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmViaCompraContrato As DalOtmViaCompraContrato
            Dim vln_Resultado As Integer

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                If PoseeRegistrosAsociados(pvo_Registro.IdViaCompraContrato) Then
                    vln_Resultado = -1
                    Throw New OrdenesDeTrabajoException("La vía de compra contrato posee registros asociados.")
                End If

                vlo_DalOtmViaCompraContrato = New DalOtmViaCompraContrato(vlo_Conexion)
                vln_Resultado = vlo_DalOtmViaCompraContrato.BorrarRegistro(pvo_Registro)
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
        ''' <param name="pvn_IdUbicacion">Llave primaria de la tabla otm_ubicacion que se asocia con la secuencia sq_id_ubicacion</param>
        ''' <param name="pvc_Descripcion">Descripción de la vía de contrato.</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>27/05/2016 02:21:34 p.m.</creationDate>
        ''' <changeLog>
        ''' <cambio>Se modifica el comportamiento del metodo para que evalue el ambito y el monto</cambio>
        ''' <reponsable>César Bermudez G</reponsable>
        ''' <fecha>07/06/2016</fecha>
        ''' </changeLog>
        Private Function ObtenerRegistroPorLlaveAlterna(pvn_IdUbicacion As Integer, pvc_Descripcion As String) As EntOtmViaCompraContrato
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmViaCompraContrato As DalOtmViaCompraContrato

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmViaCompraContrato = New DalOtmViaCompraContrato(vlo_Conexion)
                Return vlo_DalOtmViaCompraContrato.ObtenerRegistro(String.Format("{0} = {1} AND UPPER({2}) = '{3}'",
                                                        Modelo.OTM_VIA_COMPRA_CONTRATO.ID_UBICACION, pvn_IdUbicacion, Modelo.OTM_VIA_COMPRA_CONTRATO.DESCRIPCION, pvc_Descripcion.ToUpper()))
            Catch vlo_Excepcion As Exception
                Throw
            Finally
                If vlb_LiberarConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try
        End Function

        ''' <summary>
        ''' Valida el método para que evalue el ambito y el monto
        ''' </summary>
        ''' <param name="pvn_TopeEconomico"></param>
        ''' <param name="pvc_Ambito"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <reponsable>César Bermudez G</reponsable>
        ''' <fecha>07/06/2016</fecha>
        Private Function ValidarTopeEconomico(pvn_TopeEconomico As Integer, pvc_Ambito As String) As EntOtmViaCompraContrato
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DalOtmViaCompraContrato As DalOtmViaCompraContrato

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmViaCompraContrato = New DalOtmViaCompraContrato(vlo_Conexion)
                Return vlo_DalOtmViaCompraContrato.ObtenerRegistro(String.Format("{0} = {1} AND {2} = '{3}'",
                                                        Modelo.OTM_VIA_COMPRA_CONTRATO.TOPE_ECONOMICO, pvn_TopeEconomico, Modelo.OTM_VIA_COMPRA_CONTRATO.AMBITO, pvc_Ambito))
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
        ''' <param name="pvn_IdViaCompraContrato">Llave primaria de la tabla otm_via_compra_contrato que se asocia con la secuencia sq_id_via_contrato</param>
        ''' <returns>True: posee datos asociados. False: No posee datos asociados</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>27/05/2016 02:21:34 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Private Function PoseeRegistrosAsociados(pvn_IdViaCompraContrato As Integer) As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_PoseeRegistrosAsociados As Boolean
            Dim vlo_DalOtmEtapaViaContrato As DalOtmEtapaViaContrato
            Dim vlo_DalOthContratacion As DalOthContratacion
            Dim vlo_DalOttContratacion As DalOttContratacion
            Dim vlo_DalOttDetalleMaterial As DalOttDetalleMaterial

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

                'Determinar la existencia de registros asociados en la tabla OTM_ETAPA_VIA_CONTRATO
                vlo_DalOtmEtapaViaContrato = New DalOtmEtapaViaContrato(vlo_Conexion)
                If vlo_DalOtmEtapaViaContrato.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTM_ETAPA_VIA_CONTRATO.ID_VIA_CONTRATO, pvn_IdViaCompraContrato)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTH_CONTRATACION
                vlo_DalOthContratacion = New DalOthContratacion(vlo_Conexion)
                If vlo_DalOthContratacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTH_CONTRATACION.ID_VIA_CONTRATO, pvn_IdViaCompraContrato)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTT_CONTRATACION
                vlo_DalOttContratacion = New DalOttContratacion(vlo_Conexion)
                If vlo_DalOttContratacion.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_CONTRATACION.ID_VIA_CONTRATO, pvn_IdViaCompraContrato)).Existe Then
                    Return True
                End If

                'Determinar la existencia de registros asociados en la tabla OTT_DETALLE_MATERIAL
                vlo_DalOttDetalleMaterial = New DalOttDetalleMaterial(vlo_Conexion)
                If vlo_DalOttDetalleMaterial.ObtenerRegistro(String.Format("{0} = {1}", Modelo.OTT_DETALLE_MATERIAL.ID_VIA_COMPRA_CONTRATO, pvn_IdViaCompraContrato)).Existe Then
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
        ''' 'Se coloca una bandera en cada elemento que indica cuando posee registros asociados o no, con el fin de evitar el borrado desde la UI
        ''' </summary>
        ''' <param name="pvc_Condicion"></param>
        ''' <param name="pvc_Orden"></param>
        ''' <param name="pvb_Paginar"></param>
        ''' <param name="pvn_NumeroPagina"></param>
        ''' <param name="pvn_TamanoPagina"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez García</author>
        ''' <creationDate>14/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Function ListarRegistrosLista(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As Data.DataSet
            Dim vlo_DalOtmViaCompraContrato As DalOtmViaCompraContrato
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsDatos As Data.DataSet

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtmViaCompraContrato = New DalOtmViaCompraContrato(vlo_Conexion)

                vlo_DsDatos = vlo_DalOtmViaCompraContrato.ListarRegistrosLista(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

                For Each vlo_fila As Data.DataRow In vlo_DsDatos.Tables(0).Rows
                    If PoseeRegistrosAsociados(vlo_fila(Modelo.V_OTM_VIA_COMPRA_CONTRATOLST.ID_VIA_COMPRA_CONTRATO)) Then
                        vlo_fila(Modelo.V_OTM_VIA_COMPRA_CONTRATOLST.POSEE_REGISTROS_ASOCIADOS) = 1
                    Else
                        vlo_fila(Modelo.V_OTM_VIA_COMPRA_CONTRATOLST.POSEE_REGISTROS_ASOCIADOS) = 0
                    End If

                Next

                Return vlo_DsDatos

            Catch ex As Exception

            End Try
        End Function

#End Region

    End Class
End Namespace
