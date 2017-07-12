Imports Microsoft.VisualBasic

Public Class AdministradorRecursos
#Region "Enumerados"
    Public Enum TAMANO_IMAGEN
        x16
        x24
        x32
        x48
        ESPECIAL
    End Enum

    Public Enum COLOR_IMAGEN
        GRIS
        COLOR
        ESPECIAL
    End Enum
#End Region

#Region "Funciones"
    Public Shared Function ObtenerRutaImagen(pvn_Tamano As TAMANO_IMAGEN, pvn_Color As COLOR_IMAGEN, pvc_Nombre As String) As String
        Dim vlc_CarpetaTamano As String
        Dim vlc_CarpetaColor As String

        Select Case pvn_Tamano
            Case Is = TAMANO_IMAGEN.x16
                vlc_CarpetaTamano = "16x16"

            Case Is = TAMANO_IMAGEN.x24
                vlc_CarpetaTamano = "24x24"

            Case Is = TAMANO_IMAGEN.x32
                vlc_CarpetaTamano = "32x32"

            Case Is = TAMANO_IMAGEN.x48
                vlc_CarpetaTamano = "48x48"

            Case Is = TAMANO_IMAGEN.ESPECIAL
                vlc_CarpetaTamano = "Especial"
        End Select

        If pvn_Tamano <> TAMANO_IMAGEN.ESPECIAL Then
            Select Case pvn_Color
                Case Is = COLOR_IMAGEN.GRIS
                    vlc_CarpetaColor = "Gris/"

                Case Is = COLOR_IMAGEN.COLOR
                    vlc_CarpetaColor = "Color/"

                Case Is = COLOR_IMAGEN.ESPECIAL
                    vlc_CarpetaColor = String.Empty
            End Select
        Else
            vlc_CarpetaColor = String.Empty
        End If

        Return String.Format("{0}/Images/{1}/{2}{3}", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_RUTA_BASE_RECURSOS_WEB_ADS), vlc_CarpetaTamano, vlc_CarpetaColor, pvc_Nombre)
    End Function

    Public Shared Function ObtenerRutaScript(pvc_NombreScript As String) As String
        Return String.Format("{0}/Scripts/{1}", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_RUTA_BASE_RECURSOS_WEB_ADS), pvc_NombreScript)
    End Function

    Public Shared Function ObtenerRutaEstilo(pvc_NombreCSS As String) As String
        Return String.Format("{0}/CSS/{1}", ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_RUTA_BASE_RECURSOS_WEB_ADS), pvc_NombreCSS)
    End Function
#End Region
End Class
