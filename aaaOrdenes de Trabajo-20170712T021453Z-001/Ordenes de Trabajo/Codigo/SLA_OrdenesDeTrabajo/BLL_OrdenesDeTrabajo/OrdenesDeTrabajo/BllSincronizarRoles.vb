Imports System.Data
Imports System.Configuration
Imports Utilerias.BaseDatos.OracleServer
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllSincronizarRoles
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
        Public Function SincronizarRolesDirectorUnidad()
            Dim vlo_WsCatalogosVacaciones As WsrCatalogosVacaciones.WsCatalogosVacaciones
            Dim vlo_WsOracleRolesProvider As WsrOracleRolesProvider.WsOracleRolesProvider
            Dim vlo_DsDatos As New DataSet
            Dim vlc_ListaUsuarios As String()
            Dim vlc_ProviderName As String
            Dim vlc_RoleName As String
            Dim vlc_Usuario As String
            Dim vlo_DrFila As DataRow

            Try

                vlo_WsCatalogosVacaciones = New WsrCatalogosVacaciones.WsCatalogosVacaciones
                vlo_WsCatalogosVacaciones.Credentials = System.Net.CredentialCache.DefaultCredentials
                vlo_WsCatalogosVacaciones.Timeout = -1
                vlo_WsCatalogosVacaciones.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_CATALOGOS_VACACIONES)

                vlo_WsOracleRolesProvider = New WsrOracleRolesProvider.WsOracleRolesProvider
                vlo_WsOracleRolesProvider.Credentials = System.Net.CredentialCache.DefaultCredentials
                vlo_WsOracleRolesProvider.Timeout = -1
                vlo_WsOracleRolesProvider.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_ORACLE_ROLES_PROVIDER)

                vlc_ProviderName = ConfigurationManager.AppSettings("RoleProviderName")
                vlc_RoleName = ConfigurationManager.AppSettings("RoleOT_DirectorUnidad")

                'obtener jefaturas activas en la estructura jerárquica
                vlo_DsDatos = vlo_WsCatalogosVacaciones.PLM_ESTRUCTURA_ORG_ObtenerJefaturasActivas(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN))

                'obtener la lista de usuarios asignados al role DirectorUnidadSeccion        
                vlc_ListaUsuarios = vlo_WsOracleRolesProvider.GetUsersInRole(vlc_ProviderName, vlc_RoleName)

                'quitar el role a aquellos usuarios que no se encuentran en la estructura jerárquica
                If vlc_ListaUsuarios IsNot Nothing AndAlso vlc_ListaUsuarios.Length > 0 Then
                    For Each vlc_Usuario In vlc_ListaUsuarios
                        vlo_WsOracleRolesProvider.RemoveUsersFromRoles(vlc_ProviderName, New String() {vlc_Usuario}, New String() {vlc_RoleName})
                    Next
                End If

                'agregar jefaturas a role
                If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0) IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    For Each vlo_DrFila In vlo_DsDatos.Tables(0).Rows
                        vlo_WsOracleRolesProvider.AddUsersToRoles(vlc_ProviderName, New String() {vlo_DrFila("ID_PERSONAL").ToString.Trim}, New String() {vlc_RoleName})
                    Next
                End If

            Catch ex As Exception
                Throw
            Finally

                If vlo_WsCatalogosVacaciones IsNot Nothing Then
                    vlo_WsCatalogosVacaciones.Dispose()
                End If

                If vlo_WsOracleRolesProvider IsNot Nothing Then
                    vlo_WsOracleRolesProvider.Dispose()
                End If

                If vlo_DsDatos IsNot Nothing Then
                    vlo_DsDatos.Dispose()
                End If
            End Try

        End Function

        ''' <summary>
        ''' sincronizacion de roles de jefe administrativo
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gomez Ondoy</author>
        ''' <creationDate>09/12/2015</creationDate>
        ''' <changeLog></changeLog>
        Public Function SincronizarRolesJefeAdministrativo()
            Dim vlo_WsCatalogosPlanilla As WsrCatalogosPlanilla.WsCatalogosPlanilla
            Dim vlo_WsOracleRolesProvider As WsrOracleRolesProvider.WsOracleRolesProvider
            Dim vlo_DsDatos As New DataSet
            Dim vlc_ListaUsuarios As String()
            Dim vlc_ProviderName As String
            Dim vlc_RoleName As String
            Dim vlc_Usuario As String
            Dim vlo_DrFila As DataRow

            Try
                vlo_WsCatalogosPlanilla = New WsrCatalogosPlanilla.WsCatalogosPlanilla
                vlo_WsCatalogosPlanilla.Credentials = System.Net.CredentialCache.DefaultCredentials
                vlo_WsCatalogosPlanilla.Timeout = -1
                vlo_WsCatalogosPlanilla.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_CATALOGOS_PLANILLA)

                vlo_WsOracleRolesProvider = New WsrOracleRolesProvider.WsOracleRolesProvider
                vlo_WsOracleRolesProvider.Credentials = System.Net.CredentialCache.DefaultCredentials
                vlo_WsOracleRolesProvider.Timeout = -1
                vlo_WsOracleRolesProvider.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_ORACLE_ROLES_PROVIDER)

                vlc_ProviderName = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ROL_PROVIDER_NAME)
                vlc_RoleName = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ROL_OT_JEFE_ADMINISTRATIVO)

                'obtener el listado de jefes administrativos
                vlo_DsDatos = vlo_WsCatalogosPlanilla.PLM_EMPLE_DE_PLANILLA_ObtenerUsuarioPorRol(
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_USUARIO_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_CLAVE_APLICACION_GN),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_NOMBRE_APLICACION_PLANILLAS),
                    ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_ROLE_ASISTENTE_ADMINISTRATIVO))

                'obtener la lista de usuarios asignados al role DirectorUnidadSeccion        
                vlc_ListaUsuarios = vlo_WsOracleRolesProvider.GetUsersInRole(vlc_ProviderName, vlc_RoleName)

                'quitar el role a aquellos usuarios que no se encuentran en la estructura jerárquica
                If vlc_ListaUsuarios IsNot Nothing AndAlso vlc_ListaUsuarios.Length > 0 Then
                    For Each vlc_Usuario In vlc_ListaUsuarios
                        vlo_WsOracleRolesProvider.RemoveUsersFromRoles(vlc_ProviderName, New String() {vlc_Usuario}, New String() {vlc_RoleName})
                    Next
                End If





                'agregar jefaturas a role
                If vlo_DsDatos IsNot Nothing AndAlso vlo_DsDatos.Tables(0) IsNot Nothing AndAlso vlo_DsDatos.Tables(0).Rows.Count > 0 Then
                    For Each vlo_DrFila In vlo_DsDatos.Tables(0).Rows
                        vlo_WsOracleRolesProvider.AddUsersToRoles(vlc_ProviderName, New String() {vlo_DrFila(0).ToString.Trim}, New String() {vlc_RoleName})
                    Next
                End If
            Catch ex As Exception
                Throw
            Finally

                If vlo_WsCatalogosPlanilla IsNot Nothing Then
                    vlo_WsCatalogosPlanilla.Dispose()
                End If

                If vlo_WsOracleRolesProvider IsNot Nothing Then
                    vlo_WsOracleRolesProvider.Dispose()
                End If

                If vlo_DsDatos IsNot Nothing Then
                    vlo_DsDatos.Dispose()
                End If
            End Try
        End Function

#End Region

    End Class
End Namespace

