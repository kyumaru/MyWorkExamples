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
	Public Class DalOttDocumentoAnteproyect
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
        ''' Adapter para los documentos adjuntos a la orden de trabajo
        ''' </summary>
        ''' <param name="pvo_DataSet"></param>
        ''' <remarks></remarks>
        ''' <author>César Bermudez Garcia</author>
        ''' <creationDate>07/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Sub AdapterAdjuntosAnteproyecto(pvo_DataSet As Data.DataSet)
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

                vlo_Conexion.AdapterCrear(String.Format("INSERT INTO OTT_DOCUMENTO_ANTEPROYECT (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO, ID_UBICACION, ID_ORDEN_TRABAJO, VERSION, USUARIO) VALUES (:ID_TIPO_DOCUMENTO, :ID_ETAPA_ORDEN_TRABAJO, :ID_ADJUNTO_ORDEN_TRABAJO, :ID_UBICACION, :ID_ORDEN_TRABAJO, :VERSION, :USUARIO)"),
                                          String.Format("UPDATE OTT_DOCUMENTO_ANTEPROYECT SET ID_TIPO_DOCUMENTO=:ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO=:ID_ETAPA_ORDEN_TRABAJO, USUARIO=:USUARIO WHERE AND ID_ORDEN_TRABAJO=:ID_ORDEN_TRABAJO AND ID_UBICACION=:ID_UBICACION AND VERSION=:VERSION"),
                                          String.Empty)

                vlo_Conexion.AdapterAgregarParametro(":ID_TIPO_DOCUMENTO", "ID_TIPO_DOCUMENTO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_ETAPA_ORDEN_TRABAJO", "ID_ETAPA_ORDEN_TRABAJO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_ADJUNTO_ORDEN_TRABAJO", "ID_ADJUNTO_ORDEN_TRABAJO", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_UBICACION", "ID_UBICACION", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":ID_ORDEN_TRABAJO", "ID_ORDEN_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":VERSION", "VERSION", DbType.Int32, ConexionOracle.TipoParametro.Insert)
                vlo_Conexion.AdapterAgregarParametro(":USUARIO", "USUARIO", DbType.String, ConexionOracle.TipoParametro.Insert)

                vlo_Conexion.AdapterAgregarParametro(":ID_TIPO_DOCUMENTO", "ID_TIPO_DOCUMENTO", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_ETAPA_ORDEN_TRABAJO", "ID_ETAPA_ORDEN_TRABAJO", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_ADJUNTO_ORDEN_TRABAJO", "ID_ADJUNTO_ORDEN_TRABAJO", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_UBICACION", "ID_UBICACION", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":ID_ORDEN_TRABAJO", "ID_ORDEN_TRABAJO", DbType.String, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":VERSION", "VERSION", DbType.Int32, ConexionOracle.TipoParametro.Update)
                vlo_Conexion.AdapterAgregarParametro(":USUARIO", "USUARIO", DbType.String, ConexionOracle.TipoParametro.Update)

                vlo_Conexion.AdapterActualizar(pvo_DataSet, pvo_DataSet.Tables(0).TableName)

            Catch ex As Exception
                Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(ex, "")
                If (vlo_Rethrow) Then
                    Throw
                End If
            End Try
        End Sub

        ''' <summary>
        ''' Borra los elementos del dataset de la tabla en base de datos
        ''' </summary>
        ''' <param name="vlo_DsDatosTiempoOperario"></param>
        ''' <remarks></remarks>
        ''' <author>César Bermúdez García</author>
        ''' <creationDate>10/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Sub AdapterEvaluacionBorrar(vlo_DsDatosTiempoOperario As DataSet)
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

                vlo_Conexion.AdapterCrear(String.Empty,
                                          String.Empty,
                                          String.Format("DELETE FROM {0} WHERE {1} = :{1} AND {2} = :{2} AND {3} = :{3} AND {4} = :{4} AND {5} = :{5} AND {6} = :{6}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.Name,
                                                        Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_TIPO_DOCUMENTO, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ETAPA_ORDEN_TRABAJO, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO,
                                                        Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_UBICACION, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ORDEN_TRABAJO, Modelo.OTT_DOCUMENTO_ANTEPROYECT.VERSION))


                'Parámetros para Delete
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_TIPO_DOCUMENTO), String.Format("{0}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_TIPO_DOCUMENTO), DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ETAPA_ORDEN_TRABAJO), String.Format("{0}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ETAPA_ORDEN_TRABAJO), DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO), String.Format("{0}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO), DbType.Int32, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_UBICACION), String.Format("{0}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_UBICACION), DbType.String, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ORDEN_TRABAJO), String.Format("{0}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ORDEN_TRABAJO), DbType.String, ConexionOracle.TipoParametro.Delete)
                vlo_Conexion.AdapterAgregarParametro(String.Format(":{0}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.VERSION), String.Format("{0}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.VERSION), DbType.Int32, ConexionOracle.TipoParametro.Delete)


                vlo_Conexion.AdapterActualizar(vlo_DsDatosTiempoOperario, vlo_DsDatosTiempoOperario.Tables(0).TableName)

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
	''' Permite agregar un registro en la tabla OTT_DOCUMENTO_ANTEPROYECT
	''' </summary>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Overrides Function InsertarRegistro(ByVal pvo_Registro As EntBase) As Integer
		Dim vlo_Conexion As DbBase
		Dim vlc_Sentencia As String
		Dim vlb_DisposeConexion As Boolean
		Dim vlo_RegistroInterno As EntOttDocumentoAnteproyect
		Dim vln_Resultado As Integer

		Try
			vln_Resultado = -1
			vlo_RegistroInterno = CType(pvo_Registro, EntOttDocumentoAnteproyect)

			If vgo_Conexion Is Nothing Then
				vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
				vlb_DisposeConexion = True
			Else
				vlo_Conexion = vgo_Conexion
				vlb_DisposeConexion = False
			End If

			vlc_Sentencia = "prI_OTT_DOCUMENTO_ANTEPROYECT"

			vlo_Conexion.SetParameter("pvn_IdTipoDocumento", OracleDbType.Int32, vlo_RegistroInterno.IdTipoDocumento)
			vlo_Conexion.SetParameter("pvn_IdEtapaOrdenTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdEtapaOrdenTrabajo)
			vlo_Conexion.SetParameter("pvn_IdAdjuntoOrdenTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdAdjuntoOrdenTrabajo)
			vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
			vlo_Conexion.SetParameter("pvc_IdOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.IdOrdenTrabajo)
			vlo_Conexion.SetParameter("pvn_Version", OracleDbType.Int32, vlo_RegistroInterno.Version)
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
	''' Permite agregar un registro en la tabla OTT_DOCUMENTO_ANTEPROYECT
	''' </summary>
	''' <returns>Mayor a cero: El proceso ha sido exitoso; el valor puede corresponder a la llave autogenerada. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
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
	''' Permite borrar un registro en la tabla OTT_DOCUMENTO_ANTEPROYECT
	''' </summary>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Overrides Function BorrarRegistro(ByVal pvo_Registro As EntBase) As Integer
		Dim vlo_Conexion As DbBase
		Dim vlc_Sentencia As String
		Dim vlb_DisposeConexion As Boolean
		Dim vlo_RegistroInterno As EntOttDocumentoAnteproyect
		Dim vln_Resultado As Integer

		Try
			vln_Resultado = -1
			vlo_RegistroInterno = CType(pvo_Registro, EntOttDocumentoAnteproyect)

			If vgo_Conexion Is Nothing Then
				vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
				vlb_DisposeConexion = True
			Else
				vlo_Conexion = vgo_Conexion
				vlb_DisposeConexion = False
			End If

			vlc_Sentencia = "prD_OTT_DOCUMENTO_ANTEPROYECT"

			vlo_Conexion.SetParameter("pvn_IdTipoDocumento", OracleDbType.Int32, vlo_RegistroInterno.IdTipoDocumento)
			vlo_Conexion.SetParameter("pvn_IdEtapaOrdenTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdEtapaOrdenTrabajo)
			vlo_Conexion.SetParameter("pvn_IdAdjuntoOrdenTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdAdjuntoOrdenTrabajo)
			vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
			vlo_Conexion.SetParameter("pvc_IdOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.IdOrdenTrabajo)
			vlo_Conexion.SetParameter("pvn_Version", OracleDbType.Int32, vlo_RegistroInterno.Version)

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
	''' Permite borrar un registro en la tabla OTT_DOCUMENTO_ANTEPROYECT
	''' </summary>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
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
	''' Permite modificar un registro en la tabla OTT_DOCUMENTO_ANTEPROYECT
	''' </summary>
	''' <param name="pvo_Registro">Entidad a procesar</param>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Overrides Function ModificarRegistro(ByVal pvo_Registro As EntBase) As Integer
		Dim vlo_Conexion As DbBase
		Dim vlc_Sentencia As String
		Dim vlb_DisposeConexion As Boolean
		Dim vlo_RegistroInterno As EntOttDocumentoAnteproyect
		Dim vln_Resultado As Integer

		Try
			vln_Resultado = -1
			vlo_RegistroInterno = CType(pvo_Registro, EntOttDocumentoAnteproyect)

			If vgo_Conexion Is Nothing Then
				vlo_Conexion = New ConexionOracle(Me.vlc_StrConexion)
				vlb_DisposeConexion = True
			Else
				vlo_Conexion = vgo_Conexion
				vlb_DisposeConexion = False
			End If

			vlc_Sentencia = "prU_OTT_DOCUMENTO_ANTEPROYECT"

			vlo_Conexion.SetParameter("pvn_IdTipoDocumento", OracleDbType.Int32, vlo_RegistroInterno.IdTipoDocumento)
			vlo_Conexion.SetParameter("pvn_IdEtapaOrdenTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdEtapaOrdenTrabajo)
			vlo_Conexion.SetParameter("pvn_IdAdjuntoOrdenTrabajo", OracleDbType.Int32, vlo_RegistroInterno.IdAdjuntoOrdenTrabajo)
			vlo_Conexion.SetParameter("pvn_IdUbicacion", OracleDbType.Int32, vlo_RegistroInterno.IdUbicacion)
			vlo_Conexion.SetParameter("pvc_IdOrdenTrabajo", OracleDbType.Varchar2, vlo_RegistroInterno.IdOrdenTrabajo)
			vlo_Conexion.SetParameter("pvn_Version", OracleDbType.Int32, vlo_RegistroInterno.Version)
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
	''' Permite modificar un registro en la tabla OTT_DOCUMENTO_ANTEPROYECT
	''' </summary>
	''' <returns>Mayor a cero: El proceso ha sido exitoso. Menor a cero: El proceso ha fallado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
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
	''' Obtiene un registro de la tabla OTT_DOCUMENTO_ANTEPROYECT según el criterio indicado
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <returns>Entidad que representa la información obtenida al ejecutar la consulta</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Overrides Function ObtenerRegistro(ByVal pvc_Condicion As String) As EntBase
		Dim vlo_MapeoEntidad As List(Of MapeoSimple)
		Dim vlo_DsDatos As DataSet
		Dim vlo_Resultado As New EntOttDocumentoAnteproyect

		Try
			vlo_DsDatos = ListarRegistros(pvc_Condicion, String.Empty, False, 0, 0)
			If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
				vlo_MapeoEntidad = New List(Of MapeoSimple)
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DOCUMENTO_ANTEPROYECT.ID_TIPO_DOCUMENTO, "IdTipoDocumento"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DOCUMENTO_ANTEPROYECT.ID_ETAPA_ORDEN_TRABAJO, "IdEtapaOrdenTrabajo"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO, "IdAdjuntoOrdenTrabajo"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DOCUMENTO_ANTEPROYECT.ID_UBICACION, "IdUbicacion"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DOCUMENTO_ANTEPROYECT.ID_ORDEN_TRABAJO, "IdOrdenTrabajo"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DOCUMENTO_ANTEPROYECT.VERSION, "Version"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DOCUMENTO_ANTEPROYECT.USUARIO, "Usuario"))
				vlo_MapeoEntidad.Add(New MapeoSimple(OTT_DOCUMENTO_ANTEPROYECT.TIME_STAMP, "TimeStamp"))

				vlo_Resultado = vlo_DsDatos.Tables(0).Rows(0).ToEntity(Of EntOttDocumentoAnteproyect)(vlo_MapeoEntidad)
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
	''' Obtiene los registros de la vista V_OTT_DOCUMENTO_ANTEPROYECT según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
	''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
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

			vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_DOCUMENTO_ANTEPROYECT.Name, "V_OTT_DOCUMENTO_ANTEPROYECT", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTT_DOCUMENTO_ANTEPROYECT según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Function ObtenerDatosPaginacionVOttDocumentoAnteproyect(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

			vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTT_DOCUMENTO_ANTEPROYECT", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
	''' Obtiene los registros de la vista V_OTT_DOCUMENTO_ANTEPROYECTLst según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
	''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
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

			vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_DOCUMENTO_ANTEPROYECT.Name, "V_OTT_DOCUMENTO_ANTEPROYECTLst", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTT_DOCUMENTO_ANTEPROYECTLst según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Function ObtenerDatosPaginacionVOttDocumentoAnteproyectlst(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

			vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTT_DOCUMENTO_ANTEPROYECTLst", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
	''' Obtiene los registros de la vista V_OTTH_DOCUMENTO_ANTEPROYECT según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvb_Paginar">Indica si debe paginarse el resultado de la consulta</param>
	''' <param name="pvn_NumeroPagina">Indica el número de página a retornar</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>DataSet con la información obtenida según el criterio de búsqueda indicado</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Function ListarVOtthDocumentoAnteproyect(pvc_Condicion As String, pvc_Orden As String, pvb_Paginar As Boolean, pvn_NumeroPagina As Integer, pvn_TamanoPagina As Integer) As DataSet
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

			vlo_DsDatos = Me.EjecutarVista(vlo_Conexion, OTT_DOCUMENTO_ANTEPROYECT.Name, "V_OTTH_DOCUMENTO_ANTEPROYECT", pvc_Condicion, pvc_Orden, pvb_Paginar, pvn_NumeroPagina, pvn_TamanoPagina)

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
	''' Obtiene la cantidad de registros y páginas que retornará la consulta a la vista V_OTTH_DOCUMENTO_ANTEPROYECT según el criterio y orden indicados
	''' </summary>
	''' <param name="pvc_Condicion">Corresponde al detalle de la cláusula WHERE</param>
	''' <param name="pvc_Orden">Corresponde al detalle de la cláusula ORDER BY</param>
	''' <param name="pvn_TamanoPagina">Indica la cantidad máxima de registro a retornar por página</param>
	''' <returns>Entidad que contiene el total de registros de la consulta, el tamaño de la página y la cantidad de páginas requeridas para desplegar la información</returns>
	''' <author>Generador de Código basado en objetos Oracle</author>
	''' <creationDate>29/02/2016 11:26:35 a.m.</creationDate>
	''' <changeLog></changeLog>
	Public Function ObtenerDatosPaginacionVOtthDocumentoAnteproyect(pvc_Condicion As String, pvc_Orden As String, pvn_TamanoPagina As Integer) As EntDatosPaginacion
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

			vlo_Resultado = Me.ObtenerDatosPaginacionVista(vlo_Conexion, "V_OTTH_DOCUMENTO_ANTEPROYECT", pvc_Condicion, pvc_Orden, pvn_TamanoPagina)

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
