﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://localhost/SLA_GestorNotificaciones/wsGestorNotificaciones.asmx")>  _
        Public ReadOnly Property BLL_OrdenesDeTrabajo_WsrGestorNotificaciones_wsGestorNotificaciones() As String
            Get
                Return CType(Me("BLL_OrdenesDeTrabajo_WsrGestorNotificaciones_wsGestorNotificaciones"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://localhost/SLA_ExpUnico/wsEU_Curriculo.asmx")>  _
        Public ReadOnly Property BLL_OrdenesDeTrabajo_WsrEU_Curriculo_wsEU_Curriculo() As String
            Get
                Return CType(Me("BLL_OrdenesDeTrabajo_WsrEU_Curriculo_wsEU_Curriculo"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://10.11.28.15/SLA_ServiciosOrh_SDP_Sirh_Desarrollo/WsSolicitudVacaciones.asm"& _ 
            "x")>  _
        Public ReadOnly Property BLL_OrdenesDeTrabajo_WsrSolicitudVacaciones_WsSolicitudVacaciones() As String
            Get
                Return CType(Me("BLL_OrdenesDeTrabajo_WsrSolicitudVacaciones_WsSolicitudVacaciones"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://10.11.28.15/SLA_ServiciosOrh_SDP_Sirh_Desarrollo/WsCatalogosVacaciones.asm"& _ 
            "x")>  _
        Public ReadOnly Property BLL_OrdenesDeTrabajo_WsrCatalogosVacaciones_WsCatalogosVacaciones() As String
            Get
                Return CType(Me("BLL_OrdenesDeTrabajo_WsrCatalogosVacaciones_WsCatalogosVacaciones"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://localhost/SLA_UtileriasSeguridad/WsOracleRolesProvider.asmx")>  _
        Public ReadOnly Property BLL_OrdenesDeTrabajo_WsrOracleRolesProvider_WsOracleRolesProvider() As String
            Get
                Return CType(Me("BLL_OrdenesDeTrabajo_WsrOracleRolesProvider_WsOracleRolesProvider"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://10.11.28.15/SLA_ServiciosOrh_SDP_Sirh_Desarrollo/WsPlataformaDeServicios.a"& _ 
            "smx")>  _
        Public ReadOnly Property BLL_OrdenesDeTrabajo_WsrPlataformaDeServicios_WsPlataformaDeServicios() As String
            Get
                Return CType(Me("BLL_OrdenesDeTrabajo_WsrPlataformaDeServicios_WsPlataformaDeServicios"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://10.11.28.15/SLA_ServiciosOrh_SDP_Sirh_Desarrollo/WsCatalogosPlanilla.asmx")>  _
        Public ReadOnly Property BLL_OrdenesDeTrabajo_WsrCatalogosPlanilla_WsCatalogosPlanilla() As String
            Get
                Return CType(Me("BLL_OrdenesDeTrabajo_WsrCatalogosPlanilla_WsCatalogosPlanilla"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://10.11.28.15/SLA_ServiciosOrh_SDP_Sirh_Desarrollo/WsSIRH.asmx")>  _
        Public ReadOnly Property BLL_OrdenesDeTrabajo_WsrSIRH_WsSIRH() As String
            Get
                Return CType(Me("BLL_OrdenesDeTrabajo_WsrSIRH_WsSIRH"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl),  _
         Global.System.Configuration.DefaultSettingValueAttribute("http://localhost/SLA_GeneradorDeReportes/Ws_SDP_ReportServer.asmx")>  _
        Public ReadOnly Property BLL_OrdenesDeTrabajo_Wsr_SDP_ReportServer_Ws_SDP_ReportServer() As String
            Get
                Return CType(Me("BLL_OrdenesDeTrabajo_Wsr_SDP_ReportServer_Ws_SDP_ReportServer"),String)
            End Get
        End Property
        
        <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.SpecialSettingAttribute(Global.System.Configuration.SpecialSetting.WebServiceUrl)>  _
        Public ReadOnly Property BLL_OrdenesDeTrabajo_WsrOTServicio_OTServicio() As String
            Get
                Return CType(Me("BLL_OrdenesDeTrabajo_WsrOTServicio_OTServicio"),String)
            End Get
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.My.MySettings
            Get
                Return Global.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace