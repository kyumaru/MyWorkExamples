Imports Oracle.DataAccess.Client
Imports Utilerias.Genericos
Imports Utilerias.Genericos.Bases
Imports Utilerias.Genericos.Extensiones
Imports Utilerias.BaseDatos.OracleServer
Imports Utilerias.OrdenesDeTrabajo
Imports Utilerias.OrdenesDeTrabajo.Modelo
Imports OrdenesDeTrabajo.EntidadNegocio.Catalogos
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.AccesoDatos.Catalogos
	Public Class DalOtcEstadoOrdenTrabajo
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
#End Region

#Region "Funciones"
	''' <summary>
	''' Permite agregar un registro en la tabla OTC_ESTADO_ORDEN_TRABAJO
	''' </summary>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Overrides Function InsertarRegistro(ByVal pvo_Registro As EntBase) As Integer
		Dim vlo_Conexion As DbBase
		Dim vlc_Sentencia As String
		Dim vlb_DisposeConexion As Boolean
		Dim vlo_RegistroInterno As EntOtcEstadoOrdenTrabajo
		Dim vln_Resultado As Integer

		Try
			vln_Resultado = -1
			vlo_RegistroInterno = CType(pvo_Registro, EntOtcEstadoOrdenTrabajo)

			If vgo_Conexion Is Nothing Then
				vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
				vlb_DisposeConexion = True
			Else
				vlo_Conexion = vgo_Conexion
				vlb_DisposeConexion = False
			End If

			vlc_Sentencia = "prI_OTC_ESTADO_ORDEN_TRABAJO"

			vlo_Conexion.SetParameter("pvc_EstadoOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.EstadoOrdenTrabajo)
			vlo_Conexion.SetParameter("pvc_Descripcion", OracleDbType.Varchar2, vlo_RegistroInterno.Descripcion)
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
	''' Permite agregar un registro en la tabla OTC_ESTADO_ORDEN_TRABAJO
	''' </summary>
	''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
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
	''' Permite borrar un registro en la tabla OTC_ESTADO_ORDEN_TRABAJO
	''' </summary>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Overrides Function BorrarRegistro(ByVal pvo_Registro As EntBase) As Integer
		Dim vlo_Conexion As DbBase
		Dim vlc_Sentencia As String
		Dim vlb_DisposeConexion As Boolean
		Dim vlo_RegistroInterno As EntOtcEstadoOrdenTrabajo
		Dim vln_Resultado As Integer

		Try
			vln_Resultado = -1
			vlo_RegistroInterno = CType(pvo_Registro, EntOtcEstadoOrdenTrabajo)

			If vgo_Conexion Is Nothing Then
				vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
				vlb_DisposeConexion = True
			Else
				vlo_Conexion = vgo_Conexion
				vlb_DisposeConexion = False
			End If

			vlc_Sentencia = "prD_OTC_ESTADO_ORDEN_TRABAJO"

			vlo_Conexion.SetParameter("pvc_EstadoOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.EstadoOrdenTrabajo)

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
	''' Permite borrar un registro en la tabla OTC_ESTADO_ORDEN_TRABAJO
	''' </summary>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
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
	''' Permite modificar un registro en la tabla OTC_ESTADO_ORDEN_TRABAJO
	''' </summary>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Overrides Function ModificarRegistro(ByVal pvo_Registro As EntBase) As Integer
		Dim vlo_Conexion As DbBase
		Dim vlc_Sentencia As String
		Dim vlb_DisposeConexion As Boolean
		Dim vlo_RegistroInterno As EntOtcEstadoOrdenTrabajo
		Dim vln_Resultado As Integer

		Try
			vln_Resultado = -1
			vlo_RegistroInterno = CType(pvo_Registro, EntOtcEstadoOrdenTrabajo)

			If vgo_Conexion Is Nothing Then
				vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
				vlb_DisposeConexion = True
			Else
				vlo_Conexion = vgo_Conexion
				vlb_DisposeConexion = False
			End If

			vlc_Sentencia = "prU_OTC_ESTADO_ORDEN_TRABAJO"

			vlo_Conexion.SetParameter("pvc_EstadoOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.EstadoOrdenTrabajo)
			vlo_Conexion.SetParameter("pvc_Descripcion", OracleDbType.Varchar2, vlo_RegistroInterno.Descripcion)

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
	''' Permite modificar un registro en la tabla OTC_ESTADO_ORDEN_TRABAJO
	''' </summary>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
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
	''' Obtiene un registro de la tabla OTC_ESTADO_ORDEN_TRABAJO según el criterio indicado
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Overrides Function ObtenerRegistro(ByVal pvc_Condicion As String) As EntBase
		Dim vlo_MapeoEntidad As List(Of MapeoSimple)
		Dim vlo_DsDatos As DataSet
		Dim vlo_Resultado As New EntOtcEstadoOrdenTrabajo

		Try
			vlo_DsDatos = ListarRegistros(pvc_Condicion, String.Empty, False, 0, 0)
			If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
				vlo_MapeoEntidad = New List(Of MapeoSimple)
				vlo_MapeoEntidad.Add(New MapeoSimple(OTC_ESTADO_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO, "EstadoOrdenTrabajo"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTC_ESTADO_ORDEN_TRABAJO.DESCRIPCION, "Descripcion"))

				vlo_Resultado = vlo_DsDatos.Tables(0).Rows(0).ToEntity(Of EntOtcEstadoOrdenTrabajo)(vlo_MapeoEntidad)
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
	''' Obtiene los registros de la vista V_OTC_ESTADO_ORDEN_TRABAJO según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
	''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
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

			vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTC_ESTADO_ORDEN_TRABAJO.Name, "V_OTC_ESTADO_ORDEN_TRABAJO", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTC_ESTADO_ORDEN_TRABAJO según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Function ObtenerDatosPaginacionVOtcEstadoOrdenTrabajo(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

			vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTC_ESTADO_ORDEN_TRABAJO", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
	''' Obtiene los registros de la vista V_OTC_ESTADO_ORDEN_TRABAJOLst según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
	''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
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

			vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTC_ESTADO_ORDEN_TRABAJO.Name, "V_OTC_ESTADO_ORDEN_TRABAJOLst", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTC_ESTADO_ORDEN_TRABAJOLst según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>31/08/2015 08:20:01 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Function ObtenerDatosPaginacionVOtcEstadoOrdenTrabajolst(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

			vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTC_ESTADO_ORDEN_TRABAJOLst", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
