Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports OrdenesDeTrabajo.AccesoDatos.Catalogos
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration
Imports System.Data

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllOtfExcepcionPeriodo
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
        ''' carga los datos de la vista para lista de excepciones
        ''' </summary>
        ''' <param name="pvc_Condicion"></param>
        ''' <param name="pvc_Orden"></param>
        ''' <param name="pvb_Paginar"></param>
        ''' <param name="pvn_NumeroPagina"></param>
        ''' <param name="pvn_TamanoPagina"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>20/01/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarRegistrosListaPersonalizado(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As Data.DataSet
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsRegistros As Data.DataSet
            Dim vlo_DalOtfExcepcionPeriodo As DalOtfExcepcionPeriodo

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfExcepcionPeriodo = New DalOtfExcepcionPeriodo(vlo_Conexion)

                vlo_DsRegistros = vlo_DalOtfExcepcionPeriodo.ListarRegistrosLista(
                    pvc_Condicion,
                    pvc_Orden,
                    pvb_Paginar,
                    pvn_NumeroPagina,
                    pvn_TamanoPagina)

                For Each vlo_Fila In vlo_DsRegistros.Tables(0).Rows
                    Dim vln_CantidadDiasTotal As Long = 0
                    Dim vln_DiferenciaDias As Long = 0
                    Dim vln_Unidad As Integer = 0
                    Dim vln_vigencia As Integer = 0

                    vln_Unidad = CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.UNIDAD_UNIDAD_TIEMPO).ToString, Integer)
                    vln_vigencia = CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.VIGENCIA).ToString, Integer)

                    Select Case vln_Unidad
                        Case Unidades.MINUTOS
                            vln_CantidadDiasTotal = (CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer) / 1440) * vln_vigencia
                        Case Unidades.HORAS
                            vln_CantidadDiasTotal = (CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer) / 24) * vln_vigencia
                        Case Unidades.DIAS
                            vln_CantidadDiasTotal = (CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * vln_vigencia
                        Case Unidades.SEMANAS
                            vln_CantidadDiasTotal = (7 * CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * vln_vigencia
                        Case Unidades.MESES
                            vln_CantidadDiasTotal = (30 * CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * vln_vigencia
                        Case Unidades.ANIOS
                            vln_CantidadDiasTotal = (365 * CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * vln_vigencia
                    End Select

                    vln_DiferenciaDias = DateDiff(DateInterval.Day, DateTime.Now, CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.TIME_STAMP).ToString, DateTime).AddDays(vln_CantidadDiasTotal))

                    vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.TIEMPO_RESTANTE) = vln_DiferenciaDias.ToString + " Días"

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
        ''' Se determina cuales son las excepciones de periodo que deben ser eliminadas
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>20/01/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function BorrarExcepcionesPeriodo()
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_DsDatos As Data.DataSet
            Dim vlo_DsRegistrosVistaLista As Data.DataSet
            Dim vlo_DalOtfExcepcionPeriodo As DalOtfExcepcionPeriodo

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_DalOtfExcepcionPeriodo = New DalOtfExcepcionPeriodo(vlo_Conexion)

                vlo_DsDatos = vlo_DalOtfExcepcionPeriodo.ListarRegistrosLista(
                    String.Empty,
                    String.Empty,
                    False,
                    0,
                    0)

                vlo_DsDatos.Tables(0).PrimaryKey = New DataColumn() {vlo_DsDatos.Tables(0).Columns(Modelo.OTF_EXCEPCION_PERIODO.ID_EXCEPCION_PERIODO)}

                vlo_DsRegistrosVistaLista = vlo_DalOtfExcepcionPeriodo.ListarRegistrosLista(
                    String.Empty,
                    String.Empty,
                    False,
                    0,
                    0)

                For Each vlo_Fila In vlo_DsRegistrosVistaLista.Tables(0).Rows
                    Dim vln_CantidadDiasTotal As Long = 0
                    Dim vln_DiferenciaDias As Long = 0
                    Dim vln_Unidad As Integer = 0
                    Dim vln_vigencia As Integer = 0

                    vln_Unidad = CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.UNIDAD_UNIDAD_TIEMPO).ToString, Integer)
                    vln_vigencia = CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.VIGENCIA).ToString, Integer)

                    Select Case vln_Unidad
                        Case Unidades.MINUTOS
                            vln_CantidadDiasTotal = (CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer) / 1440) * vln_vigencia
                        Case Unidades.HORAS
                            vln_CantidadDiasTotal = (CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer) / 24) * vln_vigencia
                        Case Unidades.DIAS
                            vln_CantidadDiasTotal = (CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * vln_vigencia
                        Case Unidades.SEMANAS
                            vln_CantidadDiasTotal = (7 * CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * vln_vigencia
                        Case Unidades.MESES
                            vln_CantidadDiasTotal = (30 * CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * vln_vigencia
                        Case Unidades.ANIOS
                            vln_CantidadDiasTotal = (365 * CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.VALOR_UNIDAD_TIEMPO).ToString, Integer)) * vln_vigencia
                    End Select

                    vln_DiferenciaDias = DateDiff(DateInterval.Day, DateTime.Now, CType(vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.TIME_STAMP).ToString, DateTime).AddDays(vln_CantidadDiasTotal))

                    If vln_DiferenciaDias <= 0 Then
                        vlo_DsDatos.Tables(0).Rows.Find(New Object() {vlo_Fila(Modelo.V_OTF_EXCEPCION_PERIODOLST.ID_EXCEPCION_PERIODO)}).Delete()
                    End If
                Next

                vlo_Conexion.TransaccionBegin()

                vlo_DalOtfExcepcionPeriodo = New DalOtfExcepcionPeriodo(vlo_Conexion)
                vlo_DalOtfExcepcionPeriodo.AdapterOtfExcepcionPeriodo(vlo_DsDatos)

                vlo_Conexion.TransaccionCommit()

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

