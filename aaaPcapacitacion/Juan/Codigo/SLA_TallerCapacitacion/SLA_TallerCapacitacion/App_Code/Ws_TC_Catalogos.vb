Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports Utilerias.Genericos
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports TallerCapacitacion.EntidadNegocio.Catalogos
Imports TallerCapacitacion.AccesoDatos.Catalogos
Imports TallerCapacitacion.LogicaNegocio.Catalogos
Imports Utilerias.TallerCapacitacion

<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://Tallercapacitacion.ucr.ac.cr/Sla_Tallercapacitacion/Ws_TC_Catalogos")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
	Public Class Ws_TC_Catalogos
    Inherits System.Web.Services.WebService

#Region "TCC_CONDICION_LIBRO"
    ''' <summary>
    ''' Permite agregar un registro en la tabla TCC_CONDICION_LIBRO
    ''' </summary>
    ''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
    ''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
    ''' <param name="pvo_Registro">Entidad a procesar</param>
    ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
    ''' <author>Generador de Código basado en objetos Oracle</author>
    ''' <creationDate>4/5/2017 4:34:41 p. m.</creationDate>
    ''' <changeLog></changeLog>
    <WebMethod()>
    Public Function TCC_CONDICION_LIBRO_InsertarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntTccCondicionLibro) As Integer
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
        Dim vlo_BllTccCondicionLibro As New BllTccCondicionLibro(vlo_Conexion)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1
            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_INSERTAR)

            Return vlo_BllTccCondicionLibro.InsertarRegistro(pvo_Registro)
        Catch vlo_TC_Excepcion As TallerCapacitacionException
            Throw New System.Web.Services.Protocols.SoapException(
                vlo_TC_Excepcion.Message,
                System.Web.Services.Protocols.SoapException.ServerFaultCode,
                TallerCapacitacionException.NOMBRE_CLASE,
                vlo_TC_Excepcion.GetSoapExceptionDetail)

        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Function

    ''' <summary>
    ''' Permite borrar un registro en la tabla TCC_CONDICION_LIBRO
    ''' </summary>
    ''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
    ''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
    ''' <param name="pvo_Registro">Entidad a procesar</param>
    ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
    ''' <author>Generador de Código basado en objetos Oracle</author>
    ''' <creationDate>4/5/2017 4:34:41 p. m.</creationDate>
    ''' <changeLog></changeLog>
    <WebMethod()>
    Public Function TCC_CONDICION_LIBRO_BorrarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntTccCondicionLibro) As Integer
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
        Dim vlo_BllTccCondicionLibro As New BllTccCondicionLibro(vlo_Conexion)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1
            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_BORRAR)

            Return vlo_BllTccCondicionLibro.BorrarRegistro(pvo_Registro)
        Catch vlo_TC_Excepcion As TallerCapacitacionException
            Throw New System.Web.Services.Protocols.SoapException(
                vlo_TC_Excepcion.Message,
                System.Web.Services.Protocols.SoapException.ServerFaultCode,
                TallerCapacitacionException.NOMBRE_CLASE,
                vlo_TC_Excepcion.GetSoapExceptionDetail)

        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Function

    ''' <summary>
    ''' Permite borrar un registro en la tabla TCC_CONDICION_LIBRO
    ''' </summary>
    ''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
    ''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
    ''' <param name="pvo_Registro">Entidad a procesar</param>
    ''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
    ''' <author>Generador de Código basado en objetos Oracle</author>
    ''' <creationDate>4/5/2017 4:34:41 p. m.</creationDate>
    ''' <changeLog></changeLog>
    <WebMethod()>
    Public Function TCC_CONDICION_LIBRO_ModificarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntTccCondicionLibro) As Integer
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
        Dim vlo_DalTccCondicionLibro As New DalTccCondicionLibro(pvo_Registro, vlo_Conexion)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1
            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_MODIFICAR)

            Return vlo_DalTccCondicionLibro.ModificarRegistro()
        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los registros de la vista V_TCC_CONDICION_LIBRO según el criterio y orden indicados
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
    ''' <creationDate>4/5/2017 4:34:41 p. m.</creationDate>
    ''' <changeLog></changeLog>
    <WebMethod()>
    Public Function TCC_CONDICION_LIBRO_ListarRegistros(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
        Dim vlo_DalTccCondicionLibro As New DalTccCondicionLibro(vlo_Conexion)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1
            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

            Return vlo_DalTccCondicionLibro.ListarRegistros(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)
        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_TCC_CONDICION_LIBRO según el criterio y orden indicados
    ''' </summary>
    ''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
    ''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
    ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
    ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
    ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
    ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
    ''' <author>Generador de Código basado en objetos Oracle</author>
    ''' <creationDate>4/5/2017 4:34:41 p. m.</creationDate>
    ''' <changeLog></changeLog>
    <WebMethod()>
    Public Function TCC_CONDICION_LIBRO_ObtenerDatosPaginacionVTccCondicionLibro(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
        Dim vlo_DalTccCondicionLibro As New DalTccCondicionLibro(vlo_Conexion)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1
            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

            Return vlo_DalTccCondicionLibro.ObtenerDatosPaginacionVTccCondicionLibro(pvc_Condicion, pvc_Orden, pvn_TamanoPagina)
        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Function

    ''' <summary>
    ''' Obtiene los registros de la vista V_TCC_CONDICION_LIBROLst según el criterio y orden indicados
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
    ''' <creationDate>4/5/2017 4:34:41 p. m.</creationDate>
    ''' <changeLog></changeLog>
    <WebMethod()>
    Public Function TCC_CONDICION_LIBRO_ListarRegistrosLista(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
        Dim vlo_DalTccCondicionLibro As New DalTccCondicionLibro(vlo_Conexion)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1
            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

            Return vlo_DalTccCondicionLibro.ListarRegistrosLista(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)
        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Function

    ''' <summary>
    ''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_TCC_CONDICION_LIBROLst según el criterio y orden indicados
    ''' </summary>
    ''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
    ''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
    ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
    ''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
    ''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
    ''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
    ''' <author>Generador de Código basado en objetos Oracle</author>
    ''' <creationDate>4/5/2017 4:34:41 p. m.</creationDate>
    ''' <changeLog></changeLog>
    <WebMethod()>
    Public Function TCC_CONDICION_LIBRO_ObtenerDatosPaginacionVTccCondicionLibrolst(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
        Dim vlo_DalTccCondicionLibro As New DalTccCondicionLibro(vlo_Conexion)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1
            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

            Return vlo_DalTccCondicionLibro.ObtenerDatosPaginacionVTccCondicionLibrolst(pvc_Condicion, pvc_Orden, pvn_TamanoPagina)
        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un registro de la tabla TCC_CONDICION_LIBRO según el criterio indicado
    ''' </summary>
    ''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
    ''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
    ''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
    ''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
    ''' <author>Generador de Código basado en objetos Oracle</author>
    ''' <creationDate>4/5/2017 4:34:41 p. m.</creationDate>
    ''' <changeLog></changeLog>
    <WebMethod()>
    Public Function TCC_CONDICION_LIBRO_ObtenerRegistro(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String) As EntTccCondicionLibro
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
        Dim vlo_DalTccCondicionLibro As New DalTccCondicionLibro(vlo_Conexion)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1
            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

            Return vlo_DalTccCondicionLibro.ObtenerRegistro(pvc_Condicion)
        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Function

#End Region

#Region "TCM_AUTOR"
    ''' <summary>
    ''' Permite agregar un registro en la tabla TCM_AUTOR
    ''' </summary>
    ''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
    ''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
    ''' <param name="pvo_Registro">Entidad a procesar</param>
    ''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
    ''' <author>Generador de Código basado en objetos Oracle</author>
    ''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
    ''' <changeLog></changeLog>
    <WebMethod()> _
	Public Function TCM_AUTOR_InsertarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntTcmAutor) As Integer
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_BllTcmAutor As New BllTcmAutor(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_INSERTAR)

			Return vlo_BllTcmAutor.InsertarRegistro(pvo_Registro)
		Catch vlo_TC_Excepcion As TallerCapacitacionException
			Throw New System.Web.Services.Protocols.SoapException(
				vlo_TC_Excepcion.Message,
				System.Web.Services.Protocols.SoapException.ServerFaultCode,
				TallerCapacitacionException.NOMBRE_CLASE,
				vlo_TC_Excepcion.GetSoapExceptionDetail)

		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Permite borrar un registro en la tabla TCM_AUTOR
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_BorrarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntTcmAutor) As Integer
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_BllTcmAutor As New BllTcmAutor(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_BORRAR)

			Return vlo_BllTcmAutor.BorrarRegistro(pvo_Registro)
		Catch vlo_TC_Excepcion As TallerCapacitacionException
			Throw New System.Web.Services.Protocols.SoapException(
				vlo_TC_Excepcion.Message,
				System.Web.Services.Protocols.SoapException.ServerFaultCode,
				TallerCapacitacionException.NOMBRE_CLASE,
				vlo_TC_Excepcion.GetSoapExceptionDetail)

		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Permite borrar un registro en la tabla TCM_AUTOR
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_ModificarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntTcmAutor) As Integer
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_BllTcmAutor As New BllTcmAutor(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_MODIFICAR)

			Return vlo_BllTcmAutor.ModificarRegistro(pvo_Registro)
		Catch vlo_TC_Excepcion As TallerCapacitacionException
			Throw New System.Web.Services.Protocols.SoapException(
				vlo_TC_Excepcion.Message,
				System.Web.Services.Protocols.SoapException.ServerFaultCode,
				TallerCapacitacionException.NOMBRE_CLASE,
				vlo_TC_Excepcion.GetSoapExceptionDetail)

		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene los registros de la vista V_TCM_AUTOR según el criterio y orden indicados
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
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_ListarRegistros(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutor As New DalTcmAutor(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmAutor.ListarRegistros(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_TCM_AUTOR según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_ObtenerDatosPaginacionVTcmAutor(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutor As New DalTcmAutor(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmAutor.ObtenerDatosPaginacionVTcmAutor(pvc_Condicion, pvc_Orden, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene los registros de la vista V_TCM_AUTORLst según el criterio y orden indicados
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
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_ListarRegistrosLista(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutor As New DalTcmAutor(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmAutor.ListarRegistrosLista(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_TCM_AUTORLst según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_ObtenerDatosPaginacionVTcmAutorlst(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutor As New DalTcmAutor(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmAutor.ObtenerDatosPaginacionVTcmAutorlst(pvc_Condicion, pvc_Orden, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene un registro de la tabla TCM_AUTOR según el criterio indicado
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_ObtenerRegistro(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String) As EntTcmAutor
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutor As New DalTcmAutor(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmAutor.ObtenerRegistro(pvc_Condicion)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Ejecuta la función FC_TC_AUTORES_DEL_LIBRO
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_ListarFcTcAutoresDelLibro(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer, pvc_PvcIsbn As String) As DataSet
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutor As New DalTcmAutor(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmAutor.ListarFcTcAutoresDelLibro(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina, pvc_PvcIsbn)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la función FC_TC_AUTORES_DEL_LIBRO según el criterio y orden indicados
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_ObtenerDatosPaginacionFcTcAutoresDelLibro(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer, pvc_PvcIsbn As String) As EntDatosPaginacion
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutor As New DalTcmAutor(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmAutor.ObtenerDatosPaginacionFcTcAutoresDelLibro(pvc_Condicion, pvc_Orden, pvn_TamanoPagina, pvc_PvcIsbn)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Ejecuta la función FC_TC_AUTORES_NO_ASIGNADOS_LIB
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_ListarFcTcAutoresNoAsignadosLib(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer, pvc_PvcIsbn As String) As DataSet
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutor As New DalTcmAutor(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmAutor.ListarFcTcAutoresNoAsignadosLib(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina, pvc_PvcIsbn)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la función FC_TC_AUTORES_NO_ASIGNADOS_LIB según el criterio y orden indicados
	''' </summary>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_ObtenerDatosPaginacionFcTcAutoresNoAsignadosLib(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer, pvc_PvcIsbn As String) As EntDatosPaginacion
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutor As New DalTcmAutor(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmAutor.ObtenerDatosPaginacionFcTcAutoresNoAsignadosLib(pvc_Condicion, pvc_Orden, pvn_TamanoPagina, pvc_PvcIsbn)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

#End Region

#Region "TCM_AUTOR_POR_LIBRO"
	''' <summary>
	''' Permite agregar un registro en la tabla TCM_AUTOR_POR_LIBRO
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_POR_LIBRO_InsertarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntTcmAutorPorLibro) As Integer
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_BllTcmAutorPorLibro As New BllTcmAutorPorLibro(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_INSERTAR)

			Return vlo_BllTcmAutorPorLibro.InsertarRegistro(pvo_Registro)
		Catch vlo_TC_Excepcion As TallerCapacitacionException
			Throw New System.Web.Services.Protocols.SoapException(
				vlo_TC_Excepcion.Message,
				System.Web.Services.Protocols.SoapException.ServerFaultCode,
				TallerCapacitacionException.NOMBRE_CLASE,
				vlo_TC_Excepcion.GetSoapExceptionDetail)

		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Permite borrar un registro en la tabla TCM_AUTOR_POR_LIBRO
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_POR_LIBRO_BorrarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntTcmAutorPorLibro) As Integer
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutorPorLibro As New DalTcmAutorPorLibro(pvo_Registro, vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_BORRAR)

			Return vlo_DalTcmAutorPorLibro.BorrarRegistro()
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Permite borrar un registro en la tabla TCM_AUTOR_POR_LIBRO
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_POR_LIBRO_ModificarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntTcmAutorPorLibro) As Integer
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutorPorLibro As New DalTcmAutorPorLibro(pvo_Registro, vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_MODIFICAR)

			Return vlo_DalTcmAutorPorLibro.ModificarRegistro()
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene los registros de la vista V_TCM_AUTOR_POR_LIBRO según el criterio y orden indicados
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
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_POR_LIBRO_ListarRegistros(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutorPorLibro As New DalTcmAutorPorLibro(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmAutorPorLibro.ListarRegistros(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_TCM_AUTOR_POR_LIBRO según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_POR_LIBRO_ObtenerDatosPaginacionVTcmAutorPorLibro(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutorPorLibro As New DalTcmAutorPorLibro(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmAutorPorLibro.ObtenerDatosPaginacionVTcmAutorPorLibro(pvc_Condicion, pvc_Orden, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene los registros de la vista V_TCM_AUTOR_POR_LIBROLst según el criterio y orden indicados
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
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_POR_LIBRO_ListarRegistrosLista(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutorPorLibro As New DalTcmAutorPorLibro(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmAutorPorLibro.ListarRegistrosLista(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_TCM_AUTOR_POR_LIBROLst según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_POR_LIBRO_ObtenerDatosPaginacionVTcmAutorPorLibrolst(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutorPorLibro As New DalTcmAutorPorLibro(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmAutorPorLibro.ObtenerDatosPaginacionVTcmAutorPorLibrolst(pvc_Condicion, pvc_Orden, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene un registro de la tabla TCM_AUTOR_POR_LIBRO según el criterio indicado
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_AUTOR_POR_LIBRO_ObtenerRegistro(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String) As EntTcmAutorPorLibro
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmAutorPorLibro As New DalTcmAutorPorLibro(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmAutorPorLibro.ObtenerRegistro(pvc_Condicion)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

#End Region

#Region "TCM_LIBRO"
	''' <summary>
	''' Permite agregar un registro en la tabla TCM_LIBRO
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_LIBRO_InsertarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntTcmLibro) As Integer
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_BllTcmLibro As New BllTcmLibro(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_INSERTAR)

			Return vlo_BllTcmLibro.InsertarRegistro(pvo_Registro)
		Catch vlo_TC_Excepcion As TallerCapacitacionException
			Throw New System.Web.Services.Protocols.SoapException(
				vlo_TC_Excepcion.Message,
				System.Web.Services.Protocols.SoapException.ServerFaultCode,
				TallerCapacitacionException.NOMBRE_CLASE,
				vlo_TC_Excepcion.GetSoapExceptionDetail)

		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Permite borrar un registro en la tabla TCM_LIBRO
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_LIBRO_BorrarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntTcmLibro) As Integer
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_BllTcmLibro As New BllTcmLibro(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_BORRAR)

			Return vlo_BllTcmLibro.BorrarRegistro(pvo_Registro)
		Catch vlo_TC_Excepcion As TallerCapacitacionException
			Throw New System.Web.Services.Protocols.SoapException(
				vlo_TC_Excepcion.Message,
				System.Web.Services.Protocols.SoapException.ServerFaultCode,
				TallerCapacitacionException.NOMBRE_CLASE,
				vlo_TC_Excepcion.GetSoapExceptionDetail)

		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Permite borrar un registro en la tabla TCM_LIBRO
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_LIBRO_ModificarRegistro(pvc_Usuario As String, pvc_Clave As String, ByVal pvo_Registro As EntTcmLibro) As Integer
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmLibro As New DalTcmLibro(pvo_Registro, vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_MODIFICAR)

			Return vlo_DalTcmLibro.ModificarRegistro()
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene los registros de la vista V_TCM_LIBRO según el criterio y orden indicados
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
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_LIBRO_ListarRegistros(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmLibro As New DalTcmLibro(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmLibro.ListarRegistros(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_TCM_LIBRO según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_LIBRO_ObtenerDatosPaginacionVTcmLibro(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmLibro As New DalTcmLibro(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmLibro.ObtenerDatosPaginacionVTcmLibro(pvc_Condicion, pvc_Orden, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene los registros de la vista V_TCM_LIBROLst según el criterio y orden indicados
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
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_LIBRO_ListarRegistrosLista(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmLibro As New DalTcmLibro(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmLibro.ListarRegistrosLista(pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_TCM_LIBROLst según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_LIBRO_ObtenerDatosPaginacionVTcmLibrolst(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmLibro As New DalTcmLibro(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmLibro.ObtenerDatosPaginacionVTcmLibrolst(pvc_Condicion, pvc_Orden, pvn_TamanoPagina)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

	''' <summary>
	''' Obtiene un registro de la tabla TCM_LIBRO según el criterio indicado
	''' </summary>
	''' <param name="pvc_Usuario">Nombre de usuario para acceso a servicio web</param>
	''' <param name="pvc_Clave">Clave de usuario para acceso a servicio web</param>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>4/5/2017 1:54:32 p. m.</creationDate>
	''' <changeLog></changeLog>
	<WebMethod()> _
	Public Function TCM_LIBRO_ObtenerRegistro(pvc_Usuario As String, pvc_Clave As String, pvc_Condicion As String) As EntTcmLibro
		Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_TC_CAPACITACION_JUAN).ConnectionString
		Dim vlo_DalTcmLibro As New DalTcmLibro(vlo_Conexion)
		Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad

		Try
			vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
			vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
			vlo_Seguridad.Timeout = -1
			vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.TC_CATALOGOS_CONSULTAR)

			Return vlo_DalTcmLibro.ObtenerRegistro(pvc_Condicion)
		Catch vlo_Excepcion As Exception
			Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_TC_CAPACITACION_JUAN)
			If (vlo_Rethrow) Then
				Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
			End If
		End Try
	End Function

#End Region

	End Class
