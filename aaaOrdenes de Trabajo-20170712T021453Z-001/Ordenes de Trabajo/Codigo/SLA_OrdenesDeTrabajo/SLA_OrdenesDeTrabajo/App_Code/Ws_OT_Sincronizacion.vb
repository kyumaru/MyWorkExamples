Imports System.Web
Imports System.Data
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Utilerias.Genericos
Imports Utilerias.OrdenesDeTrabajo
Imports OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo

<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://Ordenesdetrabajo.ucr.ac.cr/Sla_Ordenesdetrabajo/Ws_OT_Sincronizacion")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Ws_OT_Sincronizacion
    Inherits System.Web.Services.WebService

#Region "Sincronizacion de Roles"

    ''' <summary>
    ''' Sincronización de roles de director
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>08/09/2015</creationDate>
    ''' <changeLog>
    ''' Autor: Mauricio Salas Chaves
    ''' Descripcion: Ajuste del Ws para enviar la logica al Bll
    ''' </changeLog>
    <WebMethod()> _
    Public Sub SincronizarRolesDirectorUnidad(ByVal pvc_Usuario As String, ByVal pvc_Clave As String)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_ORDENES_DE_TRABAJO).ConnectionString
        Dim vlo_BllSincronizarRoles As New BllSincronizarRoles(vlo_Conexion)

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1

            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.OT_SINCRONIZACION_PROCESAR)

            vlo_BllSincronizarRoles.SincronizarRolesDirectorUnidad()

        Catch vlo_OT_Excepcion As OrdenesDeTrabajoException
            Throw New System.Web.Services.Protocols.SoapException(
                vlo_OT_Excepcion.Message,
                System.Web.Services.Protocols.SoapException.ServerFaultCode,
                OrdenesDeTrabajoException.NOMBRE_CLASE,
                vlo_OT_Excepcion.GetSoapExceptionDetail)

        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Sincronización de roles de jefe administrativo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>09/12/2015</creationDate>
    <WebMethod()> _
    Public Sub SincronizarRolesJefeAdministrativo(ByVal pvc_Usuario As String, ByVal pvc_Clave As String)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_ORDENES_DE_TRABAJO).ConnectionString
        Dim vlo_BllSincronizarRoles As New BllSincronizarRoles(vlo_Conexion)

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1

            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.OT_SINCRONIZACION_PROCESAR)

            vlo_BllSincronizarRoles.SincronizarRolesJefeAdministrativo()

        Catch vlo_OT_Excepcion As OrdenesDeTrabajoException
            Throw New System.Web.Services.Protocols.SoapException(
                vlo_OT_Excepcion.Message,
                System.Web.Services.Protocols.SoapException.ServerFaultCode,
                OrdenesDeTrabajoException.NOMBRE_CLASE,
                vlo_OT_Excepcion.GetSoapExceptionDetail)

        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Eliminación de excepciones de periodo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/01/2016</creationDate>
    ''' <changeLog></changeLog>
    <WebMethod()> _
    Public Sub BorrarExcepcionesPeriodo(ByVal pvc_Usuario As String, ByVal pvc_Clave As String)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_ORDENES_DE_TRABAJO).ConnectionString
        Dim vlo_BllOtfExcepcionPeriodo As New BllOtfExcepcionPeriodo(vlo_Conexion)

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1

            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.OT_SINCRONIZACION_PROCESAR)

            vlo_BllOtfExcepcionPeriodo.BorrarExcepcionesPeriodo()

        Catch vlo_OT_Excepcion As OrdenesDeTrabajoException
            Throw New System.Web.Services.Protocols.SoapException(
                vlo_OT_Excepcion.Message,
                System.Web.Services.Protocols.SoapException.ServerFaultCode,
                OrdenesDeTrabajoException.NOMBRE_CLASE,
                vlo_OT_Excepcion.GetSoapExceptionDetail)

        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' En caso de vencimiento del plazo indicado para la aprobación en el ante proyecto la orden de trabajo debe ser liquidada
    ''' </summary>
    ''' <param name="pvc_Usuario"></param>
    ''' <param name="pvc_Clave"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>11/03/2016</creationDate>
    ''' <changeLog></changeLog>
    <WebMethod()> _
    Public Sub LiquidarOrdenTrabajoFueraDePlazoAprobacionAnteProyeto(ByVal pvc_Usuario As String, ByVal pvc_Clave As String)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_ORDENES_DE_TRABAJO).ConnectionString
        Dim vlo_BllOttOrdenTrabajo As New BllOttOrdenTrabajo(vlo_Conexion)

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1

            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.OT_SINCRONIZACION_PROCESAR)

            vlo_BllOttOrdenTrabajo.LiquidarOrdenTrabajoFueraDePlazoAprobacionAnteProyeto()

        Catch vlo_OT_Excepcion As OrdenesDeTrabajoException
            Throw New System.Web.Services.Protocols.SoapException(
                vlo_OT_Excepcion.Message,
                System.Web.Services.Protocols.SoapException.ServerFaultCode,
                OrdenesDeTrabajoException.NOMBRE_CLASE,
                vlo_OT_Excepcion.GetSoapExceptionDetail)

        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' En caso de vencimiento del plazo indicado para la revision de viabilidad tecnica la orden de trabajo debe ser liquidada
    ''' </summary>
    ''' <param name="pvc_Usuario"></param>
    ''' <param name="pvc_Clave"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>20/10/2016</creationDate>
    ''' <changeLog></changeLog>
    <WebMethod()> _
    Public Sub LiquidarOrdenTrabajoFueraDePlazoRevisionViabilidadTecnica(ByVal pvc_Usuario As String, ByVal pvc_Clave As String)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_ORDENES_DE_TRABAJO).ConnectionString
        Dim vlo_BllOttOrdenTrabajo As New BllOttOrdenTrabajo(vlo_Conexion)

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1

            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.OT_SINCRONIZACION_PROCESAR)

            vlo_BllOttOrdenTrabajo.LiquidarOrdenTrabajoFueraDePlazoRevisionViabilidadTecnica()

        Catch vlo_OT_Excepcion As OrdenesDeTrabajoException
            Throw New System.Web.Services.Protocols.SoapException(
                vlo_OT_Excepcion.Message,
                System.Web.Services.Protocols.SoapException.ServerFaultCode,
                OrdenesDeTrabajoException.NOMBRE_CLASE,
                vlo_OT_Excepcion.GetSoapExceptionDetail)

        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Si la OT llega a 0 Días como plazo es decir se vence el plazo la orden debe ser liquidada y notificar a la jefatura y al usuario 
    ''' Esto debe ejecutarse diariamente. 
    ''' </summary>
    ''' <param name="pvc_Usuario"></param>
    ''' <param name="pvc_Clave"></param>
    ''' <remarks></remarks>
    ''' <author>César Bermudez Garcia</author>
    ''' <creationDate>06/04/2016</creationDate>
    ''' <changeLog></changeLog>
    <WebMethod()> _
    Public Sub LiquidarOrdenTrabajoAprobacionPresupuestoSolicitante(ByVal pvc_Usuario As String, ByVal pvc_Clave As String)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_ORDENES_DE_TRABAJO).ConnectionString
        Dim vlo_BllOttOrdenTrabajo As New BllOttOrdenTrabajo(vlo_Conexion)

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1

            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.OT_SINCRONIZACION_PROCESAR)

            vlo_BllOttOrdenTrabajo.LiquidarOrdenTrabajoAprobacionPresupuestoSolicitante()

        Catch vlo_OT_Excepcion As OrdenesDeTrabajoException
            Throw New System.Web.Services.Protocols.SoapException(
                vlo_OT_Excepcion.Message,
                System.Web.Services.Protocols.SoapException.ServerFaultCode,
                OrdenesDeTrabajoException.NOMBRE_CLASE,
                vlo_OT_Excepcion.GetSoapExceptionDetail)

        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Sub

    ''' <summary>
    ''' En caso de vencimiento del plazo indicado para la aprobación de recibido conforme de solicitante  la orden de trabajo debe ser liquidada
    ''' </summary>
    ''' <param name="pvc_Usuario"></param>
    ''' <param name="pvc_Clave"></param>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>04/08/2016</creationDate>
    ''' <changeLog></changeLog>
    <WebMethod()> _
    Public Sub LiquidarOrdenTrabajoFueraDePlazoRecibidoConformeSolicitante(ByVal pvc_Usuario As String, ByVal pvc_Clave As String)
        Dim vlo_Seguridad As WsrWsSeguridad.WsSeguridad
        Dim vlo_Conexion As String = System.Configuration.ConfigurationManager.ConnectionStrings(Constantes.CONEXION_ORDENES_DE_TRABAJO).ConnectionString
        Dim vlo_BllOttOrdenTrabajo As New BllOttOrdenTrabajo(vlo_Conexion)

        Try
            vlo_Seguridad = New WsrWsSeguridad.WsSeguridad
            vlo_Seguridad.Credentials = System.Net.CredentialCache.DefaultCredentials
            vlo_Seguridad.Timeout = -1

            vlo_Seguridad.EjecucionAutorizada(pvc_Usuario, pvc_Clave, Utilerias.WsSeguridad.Constantes.OT_SINCRONIZACION_PROCESAR)

            vlo_BllOttOrdenTrabajo.LiquidarOrdenTrabajoFueraDePlazoRecibidoConformeSolicitante()

        Catch vlo_OT_Excepcion As OrdenesDeTrabajoException
            Throw New System.Web.Services.Protocols.SoapException(
                vlo_OT_Excepcion.Message,
                System.Web.Services.Protocols.SoapException.ServerFaultCode,
                OrdenesDeTrabajoException.NOMBRE_CLASE,
                vlo_OT_Excepcion.GetSoapExceptionDetail)

        Catch vlo_Excepcion As Exception
            Dim vlo_Rethrow As Boolean = ExceptionPolicy.HandleException(vlo_Excepcion, Constantes.EHP_ORDENES_DE_TRABAJO)
            If (vlo_Rethrow) Then
                Throw ControlDeErrores.DetallarExcepcion(vlo_Excepcion)
            End If
        End Try
    End Sub


#End Region

End Class
