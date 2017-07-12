Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports Utilerias.Genericos
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports SistemaTransportes.EntidadNegocio.Catalogos
Imports SistemaTransportes.AccesoDatos.Catalogos
Imports SistemaTransportes.LogicaNegocio.Catalogos
Imports Utilerias.SistemaTransportes

<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://Sistematransportes.ucr.ac.cr/Sla_Sistematransportes/Ws_STM_Catalogos")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
	Public Class Ws_STM_Catalogos
		Inherits System.Web.Services.WebService
#Region "STM_SERVICIO"
	''' <summary>
	''' Permite agregar un registro en la tabla STM_SERVICIO
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function STM_SERVICIO_InsertarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntStmServicio) As Integer
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TRANSPORTES).ConnectionString
		Dim vlo_BllStmServicio As New BllStmServicio(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.STM_CATALOGOS_INSERTAR)

			Return vlo_BllStmServicio.InsertarRegistro(pvo_Registro)
		Catch vlo_STM_Excepcion As SistemaTransportesException
			Throw New System.Web.Services.Protocols.SoapException(
				vlo_STM_Excepcion.Message,
				System.Web.Services.Protocols.SoapException.ServerFaultCode,
				SistemaTransportesException.NOMBRE_CLASE,
				vlo_STM_Excepcion.GetSoapExceptionDetail)

		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TRANSPORTES)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Permite borrar un registro en la tabla STM_SERVICIO
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function STM_SERVICIO_BorrarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntStmServicio) As Integer
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TRANSPORTES).ConnectionString
		Dim vlo_DalStmServicio As New DalStmServicio(pvo_Registro, vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.STM_CATALOGOS_BORRAR)

			Return vlo_DalStmServicio.BorrarRegistro()
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TRANSPORTES)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Permite borrar un registro en la tabla STM_SERVICIO
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function STM_SERVICIO_ModificarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntStmServicio) As Integer
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TRANSPORTES).ConnectionString
		Dim vlo_BllStmServicio As New BllStmServicio(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.STM_CATALOGOS_MODIFICAR)

			Return vlo_BllStmServicio.ModificarRegistro(pvo_Registro)
		Catch vlo_STM_Excepcion As SistemaTransportesException
			Throw New System.Web.Services.Protocols.SoapException(
				vlo_STM_Excepcion.Message,
				System.Web.Services.Protocols.SoapException.ServerFaultCode,
				SistemaTransportesException.NOMBRE_CLASE,
				vlo_STM_Excepcion.GetSoapExceptionDetail)

		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TRANSPORTES)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene los registros de la vista V_STM_SERVICIO según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
	''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function STM_SERVICIO_ListarRegistros(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TRANSPORTES).ConnectionString
		Dim vlo_DalStmServicio As New DalStmServicio(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.STM_CATALOGOS_CONSULTAR)

			Return vlo_DalStmServicio.ListarRegistros(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TRANSPORTES)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_STM_SERVICIO según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function STM_SERVICIO_ObtenerDatosPaginacionVStmServicio(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TRANSPORTES).ConnectionString
		Dim vlo_DalStmServicio As New DalStmServicio(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.STM_CATALOGOS_CONSULTAR)

			Return vlo_DalStmServicio.ObtenerDatosPaginacionVStmServicio(pvc_Condicion, pvc_Orden, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TRANSPORTES)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene los registros de la vista V_STM_SERVICIOLst según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
	''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function STM_SERVICIO_ListarRegistrosLista(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TRANSPORTES).ConnectionString
		Dim vlo_DalStmServicio As New DalStmServicio(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.STM_CATALOGOS_CONSULTAR)

			Return vlo_DalStmServicio.ListarRegistrosLista(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TRANSPORTES)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_STM_SERVICIOLst según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function STM_SERVICIO_ObtenerDatosPaginacionVStmServiciolst(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TRANSPORTES).ConnectionString
		Dim vlo_DalStmServicio As New DalStmServicio(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.STM_CATALOGOS_CONSULTAR)

			Return vlo_DalStmServicio.ObtenerDatosPaginacionVStmServiciolst(pvc_Condicion, pvc_Orden, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TRANSPORTES)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene un registro de la tabla STM_SERVICIO según el criterio indicado
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>18/5/2017 2:47:15 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function STM_SERVICIO_ObtenerRegistro(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String) As EntStmServicio
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TRANSPORTES).ConnectionString
		Dim vlo_DalStmServicio As New DalStmServicio(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.STM_CATALOGOS_CONSULTAR)

			Return vlo_DalStmServicio.ObtenerRegistro(pvc_Condicion)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TRANSPORTES)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

#End Region

	End Class
