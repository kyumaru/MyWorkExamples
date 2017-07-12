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
	Public Class DalOttDetAprovisionamiento
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
        ''' <summary>
        ''' Adapter para las lineas de detalle para la gestion de compra por aprovisionamiento
        ''' </summary>
        ''' <param name="pvo_DataSet"></param>
        ''' <remarks></remarks>
        ''' <author>Mauricio Salas Chaves</author>
        ''' <creationDate>23/01/2017</creationDate>
        ''' <changeLog></changeLog>
        Public Sub AdapterOttDetAprovisionamiento(pvo_DataSet As Data.DataSet)
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

                vlo_Conexion.AdapterCrear(String.Format("INSERT INTO OTT_DET_APROVISIONAMIENTO (ID_UBICACION, ANNO, NUMERO_GESTION, ID_MATERIAL, CANTIDAD, OBSERVACIONES, ESTADO, USUARIO) VALUES (:ID_UBICACION, :ANNO, :NUMERO_GESTION, :ID_MATERIAL, :CANTIDAD, :OBSERVACIONES, :ESTADO, :USUARIO)"),
                                          String.Format("UPDATE {0} SET {1} = :{1}, {2} = :{2}, {3} = :{3}, {4} = :{4}, {5} = SYSDATE WHERE {6} = :{6} AND {7} = :{7} AND {8} = :{8} AND {9} = :{9} AND {10} = :{10}", Modelo.OTT_DET_APROVISIONAMIENTO.Name, Modelo.OTT_DET_APROVISIONAMIENTO.CANTIDAD, Modelo.OTT_DET_APROVISIONAMIENTO.OBSERVACIONES, Modelo.OTT_DET_APROVISIONAMIENTO.ESTADO, Modelo.OTT_DET_APROVISIONAMIENTO.USUARIO, Modelo.OTT_DET_APROVISIONAMIENTO.TIME_STAMP, Modelo.OTT_DET_APROVISIONAMIENTO.ID_UBICACION, Modelo.OTT_DET_APROVISIONAMIENTO.NUMERO_GESTION, Modelo.OTT_DET_APROVISIONAMIENTO.ANNO, Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL, Modelo.OTT_DET_APROVISIONAMIENTO.TIME_STAMP),
                                          String.Format("DELETE FROM {0} WHERE {1} = :{1} AND {2} = :{2} AND {3} = :{3} AND {4} = :{4}", Modelo.OTT_DET_APROVISIONAMIENTO.Name, Modelo.OTT_DET_APROVISIONAMIENTO.ID_UBICACION, Modelo.OTT_DET_APROVISIONAMIENTO.ANNO, Modelo.OTT_DET_APROVISIONAMIENTO.NUMERO_GESTION, Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL))

                vlo_Conexion.AdapterAgregarParametro(":ID_UBICACION", "ID_UBICACION", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ANNO", "ANNO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":NUMERO_GESTION", "NUMERO_GESTION", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_MATERIAL", "ID_MATERIAL", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":CANTIDAD", "CANTIDAD", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":OBSERVACIONES", "OBSERVACIONES", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ESTADO", "ESTADO", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":USUARIO", "USUARIO", DbType.String, ConexionOracle.TipoParametro.Insert)

                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DET_APROVISIONAMIENTO.ID_UBICACION), String.Format("{0}", Modelo.OTT_DET_APROVISIONAMIENTO.ID_UBICACION), DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL), String.Format("{0}", Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL), DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DET_APROVISIONAMIENTO.ANNO), String.Format("{0}", Modelo.OTT_DET_APROVISIONAMIENTO.ANNO), DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DET_APROVISIONAMIENTO.NUMERO_GESTION), String.Format("{0}", Modelo.OTT_DET_APROVISIONAMIENTO.NUMERO_GESTION), DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DET_APROVISIONAMIENTO.CANTIDAD), String.Format("{0}", Modelo.OTT_DET_APROVISIONAMIENTO.CANTIDAD), DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DET_APROVISIONAMIENTO.OBSERVACIONES), String.Format("{0}", Modelo.OTT_DET_APROVISIONAMIENTO.OBSERVACIONES), DbType.String, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DET_APROVISIONAMIENTO.ESTADO), String.Format("{0}", Modelo.OTT_DET_APROVISIONAMIENTO.ESTADO), DbType.String, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DET_APROVISIONAMIENTO.USUARIO), String.Format("{0}", Modelo.OTT_DET_APROVISIONAMIENTO.NUMERO_GESTION), DbType.String, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DET_APROVISIONAMIENTO.TIME_STAMP), String.Format("{0}", Modelo.OTT_DET_APROVISIONAMIENTO.TIME_STAMP), DbType.Date, ConexionOracle.TipoParametro.Update)

                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DET_APROVISIONAMIENTO.ID_UBICACION), String.Format("{0}", Modelo.OTT_DET_APROVISIONAMIENTO.ID_UBICACION), DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL), String.Format("{0}", Modelo.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL), DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DET_APROVISIONAMIENTO.ANNO), String.Format("{0}", Modelo.OTT_DET_APROVISIONAMIENTO.ANNO), DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DET_APROVISIONAMIENTO.NUMERO_GESTION), String.Format("{0}", Modelo.OTT_DET_APROVISIONAMIENTO.NUMERO_GESTION), DbType.Int32, ConexionOracle.TipoParametro.Delete)


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
	''' Permite agregar un registro en la tabla OTT_DET_APROVISIONAMIENTO
	''' </summary>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:15 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Overrides Function InsertarRegistro(ByVal pvo_Registro As EntBase) As Integer
		Dim vlo_Conexion As DbBase
		Dim vlc_Sentencia As String
		Dim vlb_DisposeConexion As Boolean
		Dim vlo_RegistroInterno As EntOttDetAprovisionamiento
		Dim vln_Resultado As Integer

		Try
			vln_Resultado = -1
			vlo_RegistroInterno = CType(pvo_Registro, EntOttDetAprovisionamiento)

			If vgo_Conexion Is Nothing Then
				vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
				vlb_DisposeConexion = True
			Else
				vlo_Conexion = vgo_Conexion
				vlb_DisposeConexion = False
			End If

			vlc_Sentencia = "prI_OTT_DET_APROVISIONAMIENTO"

			vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
			vlo_Conexion.SetParameter("pvn_NumeroGestion", OracleDbType.Int32, vlo_RegistroInterno.NumeroGestion)
			vlo_Conexion.SetParameter("pvn_Anno", OracleDbType.Int32, vlo_RegistroInterno.Anno)
			vlo_Conexion.SetParameter("pvn_IdMaterial", OracleDbType.Int32, vlo_RegistroInterno.IdMaterial)
			vlo_Conexion.SetParameter("pvn_Cantidad", OracleDbType.Double, vlo_RegistroInterno.Cantidad)
			vlo_Conexion.SetParameter("pvc_Observaciones", OracleDbType.Varchar2, vlo_RegistroInterno.Observaciones)
			vlo_Conexion.SetParameter("pvc_Estado", OracleDbType.Varchar2, vlo_RegistroInterno.Estado)
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
	''' Permite agregar un registro en la tabla OTT_DET_APROVISIONAMIENTO
	''' </summary>
	''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:15 p.m.</creationDate>
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
	''' Permite borrar un registro en la tabla OTT_DET_APROVISIONAMIENTO
	''' </summary>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:15 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Overrides Function BorrarRegistro(ByVal pvo_Registro As EntBase) As Integer
		Dim vlo_Conexion As DbBase
		Dim vlc_Sentencia As String
		Dim vlb_DisposeConexion As Boolean
		Dim vlo_RegistroInterno As EntOttDetAprovisionamiento
		Dim vln_Resultado As Integer

		Try
			vln_Resultado = -1
			vlo_RegistroInterno = CType(pvo_Registro, EntOttDetAprovisionamiento)

			If vgo_Conexion Is Nothing Then
				vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
				vlb_DisposeConexion = True
			Else
				vlo_Conexion = vgo_Conexion
				vlb_DisposeConexion = False
			End If

			vlc_Sentencia = "prD_OTT_DET_APROVISIONAMIENTO"

			vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
			vlo_Conexion.SetParameter("pvn_NumeroGestion", OracleDbType.Int32, vlo_RegistroInterno.NumeroGestion)
			vlo_Conexion.SetParameter("pvn_Anno", OracleDbType.Int32, vlo_RegistroInterno.Anno)
			vlo_Conexion.SetParameter("pvn_IdMaterial", OracleDbType.Int32, vlo_RegistroInterno.IdMaterial)

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
	''' Permite borrar un registro en la tabla OTT_DET_APROVISIONAMIENTO
	''' </summary>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:15 p.m.</creationDate>
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
	''' Permite modificar un registro en la tabla OTT_DET_APROVISIONAMIENTO
	''' </summary>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:15 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Overrides Function ModificarRegistro(ByVal pvo_Registro As EntBase) As Integer
		Dim vlo_Conexion As DbBase
		Dim vlc_Sentencia As String
		Dim vlb_DisposeConexion As Boolean
		Dim vlo_RegistroInterno As EntOttDetAprovisionamiento
		Dim vln_Resultado As Integer

		Try
			vln_Resultado = -1
			vlo_RegistroInterno = CType(pvo_Registro, EntOttDetAprovisionamiento)

			If vgo_Conexion Is Nothing Then
				vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
				vlb_DisposeConexion = True
			Else
				vlo_Conexion = vgo_Conexion
				vlb_DisposeConexion = False
			End If

			vlc_Sentencia = "prU_OTT_DET_APROVISIONAMIENTO"

			vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
			vlo_Conexion.SetParameter("pvn_NumeroGestion", OracleDbType.Int32, vlo_RegistroInterno.NumeroGestion)
			vlo_Conexion.SetParameter("pvn_Anno", OracleDbType.Int32, vlo_RegistroInterno.Anno)
			vlo_Conexion.SetParameter("pvn_IdMaterial", OracleDbType.Int32, vlo_RegistroInterno.IdMaterial)
			vlo_Conexion.SetParameter("pvn_Cantidad", OracleDbType.Double, vlo_RegistroInterno.Cantidad)
			vlo_Conexion.SetParameter("pvc_Observaciones", OracleDbType.Varchar2, vlo_RegistroInterno.Observaciones)
			vlo_Conexion.SetParameter("pvc_Estado", OracleDbType.Varchar2, vlo_RegistroInterno.Estado)
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
	''' Permite modificar un registro en la tabla OTT_DET_APROVISIONAMIENTO
	''' </summary>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:15 p.m.</creationDate>
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
	''' Obtiene un registro de la tabla OTT_DET_APROVISIONAMIENTO según el criterio indicado
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:15 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Overrides Function ObtenerRegistro(ByVal pvc_Condicion As String) As EntBase
		Dim vlo_MapeoEntidad As List(Of MapeoSimple)
		Dim vlo_DsDatos As DataSet
		Dim vlo_Resultado As New EntOttDetAprovisionamiento

		Try
			vlo_DsDatos = ListarRegistros(pvc_Condicion, String.Empty, False, 0, 0)
			If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
				vlo_MapeoEntidad = New List(Of MapeoSimple)
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DET_APROVISIONAMIENTO.ID_UBICACION, "IdUbicacion"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DET_APROVISIONAMIENTO.NUMERO_GESTION, "NumeroGestion"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DET_APROVISIONAMIENTO.ANNO, "Anno"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DET_APROVISIONAMIENTO.ID_MATERIAL, "IdMaterial"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DET_APROVISIONAMIENTO.CANTIDAD, "Cantidad"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DET_APROVISIONAMIENTO.OBSERVACIONES, "Observaciones"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DET_APROVISIONAMIENTO.ESTADO, "Estado"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DET_APROVISIONAMIENTO.USUARIO, "Usuario"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DET_APROVISIONAMIENTO.TIME_STAMP, "TimeStamp"))

				vlo_Resultado = vlo_DsDatos.Tables(0).Rows(0).ToEntity(Of EntOttDetAprovisionamiento)(vlo_MapeoEntidad)
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
	''' Obtiene los registros de la vista V_OTT_DET_APROVISIONAMIENTO según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
	''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:15 p.m.</creationDate>
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

			vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_DET_APROVISIONAMIENTO.Name, "V_OTT_DET_APROVISIONAMIENTO", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTT_DET_APROVISIONAMIENTO según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:15 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Function ObtenerDatosPaginacionVOttDetAprovisionamiento(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

			vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTT_DET_APROVISIONAMIENTO", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
	''' Obtiene los registros de la vista V_OTT_DET_APROVISIONAMIENTOLst según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
	''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:15 p.m.</creationDate>
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

			vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_DET_APROVISIONAMIENTO.Name, "V_OTT_DET_APROVISIONAMIENTOLst", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTT_DET_APROVISIONAMIENTOLst según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:15 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Function ObtenerDatosPaginacionVOttDetAprovisionamientolst(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

			vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTT_DET_APROVISIONAMIENTOLst", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
	''' Obtiene los registros de la vista V_OTTH_DET_APROVISIONAMIENTO según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
	''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:15 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Function ListarVOtthDetAprovisionamiento(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

			vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_DET_APROVISIONAMIENTO.Name, "V_OTTH_DET_APROVISIONAMIENTO", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTTH_DET_APROVISIONAMIENTO según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>11/01/2017 01:45:15 p.m.</creationDate>
	''' <changeLog></changeLog>
	Public Function ObtenerDatosPaginacionVOtthDetAprovisionamiento(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

			vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTTH_DET_APROVISIONAMIENTO", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
