Imports Oracle.DataAccess.Client
Imports Utilerias.Genericos
Imports Utilerias.Genericos.Bases
Imports Utilerias.Genericos.Extensiones
Imports Utilerias.BaseDatos.OracleServer
Imports Utilerias.OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo.Modelo
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
	Public Class DalOttDetalleGestionIngr
		Inherits DalBase
#Region "Constructores"
	Public Sub New(ByVal pvc_StrConexion As String)
		MyBase.New(pvc_StrConexion)
	End Sub

	Public Sub New(ByVal pvo_Entidad As EntBase, ByVal pvc_StrConexion As String)
		MyBase.New(pvo_Entidad, pvc_StrConexion)
	End Sub

	Public Sub New(ByVal pvo_Entidad As EntBase, ByVal pvo_Conexion As DbBase)
		MyBase.New(pvo_Entidad, pvo_Conexion)
	End Sub

	Public Sub New(ByVal pvo_Conexion As DbBase)
		MyBase.New(pvo_Conexion)
	End Sub
#End Region

#Region "Metodos"
        Public Sub AdapterOttDetalleGestionIngr(pvo_DataSet As Data.DataSet)
            Dim vlo_Conexion As ConexionOracle
            Dim vlb_DisposeConexion As Boolean

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_Conexion.AdapterCrear(String.Format("INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}) VALUES (:{1}, :{2}, :{3}, :{4}, :{5}, :{6}, :{7}, :{8}, :{9})", Modelo.OTT_DETALLE_GESTION_INGR.Name, Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION, Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO, Modelo.OTT_DETALLE_GESTION_INGR.NUMERO_GESTION, Modelo.OTT_DETALLE_GESTION_INGR.ANNO, Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA, Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA, Modelo.OTT_DETALLE_GESTION_INGR.COSTO_INDIVIDUAL, Modelo.OTT_DETALLE_GESTION_INGR.USUARIO, Modelo.OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR),
                                          String.Format("UPDATE {0} SET {1} = :{1}, {2} = :{2}, {3} = :{3}, {4} = SYSDATE WHERE {5} = :{5} AND {6} = :{6} AND {7} = :{7} AND {8} = :{8} AND {9} = :{9} AND {10} = :{10} AND {11} = :{11}", Modelo.OTT_DETALLE_GESTION_INGR.Name, Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA, Modelo.OTT_DETALLE_GESTION_INGR.COSTO_INDIVIDUAL, Modelo.OTT_DETALLE_GESTION_INGR.USUARIO, Modelo.OTT_DETALLE_GESTION_INGR.TIME_STAMP, Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION, Modelo.OTT_DETALLE_GESTION_INGR.ANNO, Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO, Modelo.OTT_DETALLE_GESTION_INGR.NUMERO_GESTION, Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA, Modelo.OTT_DET_APROVISIONAMIENTO.TIME_STAMP, Modelo.OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR),
                                          String.Format("DELETE FROM {0} WHERE {1} = :{1} AND {2} = :{2} AND {3} = :{3} AND {4} = :{4}", Modelo.OTT_DETALLE_GESTION_INGR.Name, Modelo.OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR, Modelo.OTT_DETALLE_GESTION_INGR.ANNO, Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION, Modelo.OTT_DETALLE_GESTION_INGR.NUMERO_GESTION, Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA, Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO))

                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR), DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION), DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO), DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.NUMERO_GESTION), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.NUMERO_GESTION), DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ANNO), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ANNO), DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA), DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA), DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.COSTO_INDIVIDUAL), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.COSTO_INDIVIDUAL), DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.USUARIO), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.USUARIO), DbType.String, ConexionOracle.TipoParametro.Insert)

                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR), DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION), DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO), DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.NUMERO_GESTION), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.NUMERO_GESTION), DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ANNO), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ANNO), DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA), DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA), DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.COSTO_INDIVIDUAL), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.COSTO_INDIVIDUAL), DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.USUARIO), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.USUARIO), DbType.String, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.TIME_STAMP), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.TIME_STAMP), DbType.Date, ConexionOracle.TipoParametro.Update)

                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_UBICACION), DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO), DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ANNO), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ANNO), DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA), DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR), String.Format("{0}", Modelo.OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR), DbType.Int32, ConexionOracle.TipoParametro.Delete)


                vlo_Conexion.AdapterActualizar(pvo_DataSet, pvo_DataSet.Tables(0).TableName)

            Catch ex As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(ex, "")
                If (vlo_Rethrow) Then
                    Throw
                End If
            End Try
        End Sub
#End Region

#Region "Funciones"
	''' <summary>
	''' Permite agregar un registro en la tabla OTT_DETALLE_GESTION_INGR
	''' </summary>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Overrides Function InsertarRegistro(ByVal pvo_Registro As EntBase) As Integer
		Dim vlo_Conexion As DbBase
		Dim vlc_Sentencia As String
		Dim vlb_DisposeConexion As Boolean
		Dim vlo_RegistroInterno As EntOttDetalleGestionIngr
		Dim vln_Resultado As Integer

		Try
			vln_Resultado = -1
			vlo_RegistroInterno = CType(pvo_Registro, EntOttDetalleGestionIngr)

			If vgo_Conexion Is Nothing Then
				vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
				vlb_DisposeConexion = True
			Else
				vlo_Conexion = vgo_Conexion
				vlb_DisposeConexion = False
			End If

			vlc_Sentencia = "prI_OTT_DETALLE_GESTION_INGR"

			vlo_Conexion.SetParameter("pvn_IdAdjuntoGestionIngr", OracleDbType.Int32, vlo_RegistroInterno.IdAdjuntoGestionIngr)
			vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
			vlo_Conexion.SetParameter("pvn_IdViaCompraContrato", OracleDbType.Int32, vlo_RegistroInterno.IdViaCompraContrato)
			vlo_Conexion.SetParameter("pvn_NumeroGestion", OracleDbType.Int32, vlo_RegistroInterno.NumeroGestion)
			vlo_Conexion.SetParameter("pvn_Anno", OracleDbType.Int32, vlo_RegistroInterno.Anno)
			vlo_Conexion.SetParameter("pvn_IdLineaGestionCompra", OracleDbType.Int32, vlo_RegistroInterno.IdLineaGestionCompra)
			vlo_Conexion.SetParameter("pvn_CantidadIngresa", OracleDbType.Double, vlo_RegistroInterno.CantidadIngresa)
			vlo_Conexion.SetParameter("pvn_CostoIndividual", OracleDbType.Double, vlo_RegistroInterno.CostoIndividual)
			vlo_Conexion.SetParameter("pvc_Usuario", OracleDbType.Varchar2, vlo_RegistroInterno.Usuario)
			vlo_Conexion.EjecutarSentencia(vlc_Sentencia, CommandType.StoredProcedure)
			vln_Resultado = 1
		Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite agregar un registro en la tabla OTT_DETALLE_GESTION_INGR
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function InsertarRegistro() As Integer
            Dim vln_Resultado As Integer

            Try
                If vlb_HayDatos Then
                    vln_Resultado = InsertarRegistro(vlo_Registro)
                Else
                    vln_Resultado = -1
                End If
            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite borrar un registro en la tabla OTT_DETALLE_GESTION_INGR
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function BorrarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOttDetalleGestionIngr
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOttDetalleGestionIngr)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prD_OTT_DETALLE_GESTION_INGR"

                vlo_Conexion.SetParameter("pvn_IdAdjuntoGestionIngr", OracleDbType.Int32, vlo_RegistroInterno.IdAdjuntoGestionIngr)
                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
                vlo_Conexion.SetParameter("pvn_IdViaCompraContrato", OracleDbType.Int32, vlo_RegistroInterno.IdViaCompraContrato)
                vlo_Conexion.SetParameter("pvn_NumeroGestion", OracleDbType.Int32, vlo_RegistroInterno.NumeroGestion)
                vlo_Conexion.SetParameter("pvn_Anno", OracleDbType.Int32, vlo_RegistroInterno.Anno)
                vlo_Conexion.SetParameter("pvn_IdLineaGestionCompra", OracleDbType.Int32, vlo_RegistroInterno.IdLineaGestionCompra)

                vlo_Conexion.EjecutarSentencia(vlc_Sentencia, CommandType.StoredProcedure)
                vln_Resultado = 1
            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite borrar un registro en la tabla OTT_DETALLE_GESTION_INGR
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function BorrarRegistro() As Integer
            Dim vln_Resultado As Integer

            Try
                If vlb_HayDatos Then
                    vln_Resultado = BorrarRegistro(vlo_Registro)
                Else
                    vln_Resultado = -1
                End If
            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite modificar un registro en la tabla OTT_DETALLE_GESTION_INGR
        ''' </summary>
        ''' <param name="pvo_Registro">Entidad a procesar</param>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ModificarRegistro(ByVal pvo_Registro As EntBase) As Integer
            Dim vlo_Conexion As DbBase
            Dim vlc_Sentencia As String
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_RegistroInterno As EntOttDetalleGestionIngr
            Dim vln_Resultado As Integer

            Try
                vln_Resultado = -1
                vlo_RegistroInterno = CType(pvo_Registro, EntOttDetalleGestionIngr)

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlc_Sentencia = "prU_OTT_DETALLE_GESTION_INGR"

                vlo_Conexion.SetParameter("pvn_IdAdjuntoGestionIngr", OracleDbType.Int32, vlo_RegistroInterno.IdAdjuntoGestionIngr)
                vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
                vlo_Conexion.SetParameter("pvn_IdViaCompraContrato", OracleDbType.Int32, vlo_RegistroInterno.IdViaCompraContrato)
                vlo_Conexion.SetParameter("pvn_NumeroGestion", OracleDbType.Int32, vlo_RegistroInterno.NumeroGestion)
                vlo_Conexion.SetParameter("pvn_Anno", OracleDbType.Int32, vlo_RegistroInterno.Anno)
                vlo_Conexion.SetParameter("pvn_IdLineaGestionCompra", OracleDbType.Int32, vlo_RegistroInterno.IdLineaGestionCompra)
                vlo_Conexion.SetParameter("pvn_CantidadIngresa", OracleDbType.Double, vlo_RegistroInterno.CantidadIngresa)
                vlo_Conexion.SetParameter("pvn_CostoIndividual", OracleDbType.Double, vlo_RegistroInterno.CostoIndividual)
                vlo_Conexion.SetParameter("pvc_Usuario", OracleDbType.Varchar2, vlo_RegistroInterno.Usuario)
                vlo_Conexion.SetParameter("pvd_TimeStamp", OracleDbType.Date, vlo_RegistroInterno.TimeStamp)

                vlo_Conexion.EjecutarSentencia(vlc_Sentencia, CommandType.StoredProcedure)
                vln_Resultado = 1
            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Permite modificar un registro en la tabla OTT_DETALLE_GESTION_INGR
        ''' </summary>
        ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ModificarRegistro() As Integer
            Dim vln_Resultado As Integer

            Try
                If vlb_HayDatos Then
                    vln_Resultado = ModificarRegistro(vlo_Registro)
                Else
                    vln_Resultado = -1
                End If
            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            End Try

            Return vln_Resultado
        End Function

        ''' <summary>
        ''' Obtiene un registro de la tabla OTT_DETALLE_GESTION_INGR según el criterio indicado
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ObtenerRegistro(ByVal pvc_Condicion As String) As EntBase
            Dim vlo_MapeoEntidad As List(Of MapeoSimple)
            Dim vlo_DsDatos As DataSet
            Dim vlo_Resultado As New EntOttDetalleGestionIngr

            Try
                vlo_DsDatos = ListarRegistros(pvc_Condicion, String.Empty, False, 0, 0)
                If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    vlo_MapeoEntidad = New List(Of MapeoSimple)
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR, "IdAdjuntoGestionIngr"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DETALLE_GESTION_INGR.ID_UBICACION, "IdUbicacion"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO, "IdViaCompraContrato"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DETALLE_GESTION_INGR.NUMERO_GESTION, "NumeroGestion"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DETALLE_GESTION_INGR.ANNO, "Anno"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA, "IdLineaGestionCompra"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA, "CantidadIngresa"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DETALLE_GESTION_INGR.COSTO_INDIVIDUAL, "CostoIndividual"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DETALLE_GESTION_INGR.USUARIO, "Usuario"))
                    vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DETALLE_GESTION_INGR.TIME_STAMP, "TimeStamp"))

                    vlo_Resultado = vlo_DsDatos.Tables(0).Rows(0).ToEntity(Of EntOttDetalleGestionIngr)(vlo_MapeoEntidad)
                    vlo_Resultado.Existe = True
                End If

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            End Try

            Return vlo_Resultado
        End Function

        ''' <summary>
        ''' Obtiene los registros de la vista V_OTT_DETALLE_GESTION_INGR según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ListarRegistros(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
            Dim vlo_Conexion As DbBase
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_DsDatos As New DataSet

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_DETALLE_GESTION_INGR.Name, "V_OTT_DETALLE_GESTION_INGR", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlo_DsDatos
        End Function

        ''' <summary>
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTT_DETALLE_GESTION_INGR según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOttDetalleGestionIngr(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
            Dim vlo_Conexion As DbBase
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_Resultado As EntDatosPaginacion

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTT_DETALLE_GESTION_INGR", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlo_Resultado
        End Function

        ''' <summary>
        ''' Obtiene los registros de la vista V_OTT_DETALLE_GESTION_INGRLst según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Overrides Function ListarRegistrosLista(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
            Dim vlo_Conexion As DbBase
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_DsDatos As New DataSet

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_DETALLE_GESTION_INGR.Name, "V_OTT_DETALLE_GESTION_INGRLst", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlo_DsDatos
        End Function

        ''' <summary>
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTT_DETALLE_GESTION_INGRLst según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOttDetalleGestionIngrlst(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
            Dim vlo_Conexion As DbBase
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_Resultado As EntDatosPaginacion

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTT_DETALLE_GESTION_INGRLst", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlo_Resultado
        End Function

        ''' <summary>
        ''' Obtiene los registros de la vista V_OTTH_DETALLE_GESTION_INGR según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtthDetalleGestionIngr(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
            Dim vlo_Conexion As DbBase
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_DsDatos As New DataSet

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_DETALLE_GESTION_INGR.Name, "V_OTTH_DETALLE_GESTION_INGR", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlo_DsDatos
        End Function

        ''' <summary>
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTTH_DETALLE_GESTION_INGR según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>01/02/2017 04:28:02 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtthDetalleGestionIngr(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
            Dim vlo_Conexion As DbBase
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_Resultado As EntDatosPaginacion

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTTH_DETALLE_GESTION_INGR", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlo_Resultado
        End Function

        ''' <summary>
        ''' Obtiene los registros de la vista V_OT_DET_GEST_ING_GROUP según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
        ''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/02/2017 03:56:13 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ListarVOtDetGestIngGroup(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
            Dim vlo_Conexion As DbBase
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_DsDatos As New DataSet

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, V_OT_DET_GEST_ING_GROUP.Name, V_OT_DET_GEST_ING_GROUP.Name, pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlo_DsDatos
        End Function

        ''' <summary>
        ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OT_DET_GEST_ING_GROUP según el criterio y orden indicados
        ''' </summary>
        ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
        ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
        ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
        ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
        ''' <author>Generador de Código basado en objetos Oracle</author>
        ''' <creationDate>21/02/2017 03:56:13 p.m.</creationDate>
        ''' <changeLog></changeLog>
        Public Function ObtenerDatosPaginacionVOtDetGestIngGroup(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
            Dim vlo_Conexion As DbBase
            Dim vlb_DisposeConexion As Boolean
            Dim vlo_Resultado As EntDatosPaginacion

            Try
                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
                    vlb_DisposeConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_DisposeConexion = False
                End If

                vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, V_OT_DET_GEST_ING_GROUP.Name, pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

            Catch vlo_Excepcion As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
                If (vlo_Rethrow) Then
                    Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
                End If
            Finally
                If vlb_DisposeConexion Then
                    vlo_Conexion.Dispose()
                End If
            End Try

            Return vlo_Resultado
        End Function
#End Region
	End Class
End Namespace
