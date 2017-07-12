Imports System.IO 'Para trabajar con archivos
Imports iTextSharp.text 'Para trabajar con los pdf
Imports iTextSharp.text.pdf
Imports OrdenesDeTrabajo.EntidadNegocio.OrdenesDeTrabajo
Imports Wsr_SDP_ReportServer
Imports OrdenesDeTrabajo.AccesoDatos.OrdenesDeTrabajo
Imports Utilerias.BaseDatos.OracleServer
Imports Utilerias.OrdenesDeTrabajo.Modelo
Imports Utilerias.OrdenesDeTrabajo
Imports System.Configuration
Imports System.Data

Namespace OrdenesDeTrabajo.LogicaNegocio.OrdenesDeTrabajo
    Public Class BllPdfMezcla

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
        ''' <summary>
        ''' Crea el pdf a partir del dataset de reportes
        ''' </summary>
        ''' <param name="pvo_DsReportes">Data Set con reportes</param>
        ''' <returns>Mayor a 0 si fue exitoso</returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>10/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ConcatenarPdf(pvo_DsReportes As Data.DataSet, pvc_NombreReporte As String) As Integer
            Dim vlo_Archivo As Byte()
            Dim vlc_DireccionEscritura As String = String.Format("{0}{1}.pdf", System.Configuration.ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_RUTA_ESCRIBIR_ARCHIVOS).ToString, pvc_NombreReporte)
            Dim Doc As New Document()
            Dim vlo_ArchivoSalida As New FileStream(vlc_DireccionEscritura, FileMode.Create, FileAccess.Write, FileShare.None)
            Dim vlo_ArchivoCopia As New PdfCopy(Doc, vlo_ArchivoSalida)
            Dim vlo_PdfReader As PdfReader
            Dim vln_CantidadPaginas As Integer
            Dim vln_Pagina As Integer = 0
            Try
                Doc.Open() 'Abrimos el documento
                For Each vlo_Row In pvo_DsReportes.Tables(0).Rows 'Dataset De reportes
                    vlo_Archivo = vlo_Row("ARCHIVO")
                    vlo_PdfReader = New PdfReader(vlo_Archivo) 'Lector del pdf
                    vln_CantidadPaginas = vlo_PdfReader.NumberOfPages 'Cantidad de paginas del documento

                    Do While vln_Pagina < vln_CantidadPaginas
                        vln_Pagina += 1
                        vlo_ArchivoCopia.AddPage(vlo_ArchivoCopia.GetImportedPage(vlo_PdfReader, vln_Pagina)) ' vamos armando el pdf general
                    Loop
                    vln_Pagina = 0
                    vlo_ArchivoCopia.FreeReader(vlo_PdfReader)
                    vlo_PdfReader.Close()
                Next
                Return 1
            Catch ex As Exception
                Throw
            Finally
                Doc.Close()
            End Try
        End Function

        ''' <summary>
        ''' Construye el dataset con los reportes
        ''' </summary>
        ''' <param name="pvc_FormatoReporte">Formato del reporte</param>
        ''' <param name="pvc_RutaBase">Ruta Base del reporte</param>
        ''' <param name="pvc_NombreReporte"></param>
        ''' <returns>Archivo del reporte generado</returns>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>10/03/2016</creationDate>
        ''' <changeLog></changeLog>
        Public Function ConstruirReporte(ByVal pvc_Usuario As String, ByVal pvc_Clave As String, pvo_EntOttAnteproyecto As EntOttAnteproyecto, pvc_Condicion As String, pvc_UsuarioEjecuta As String, ByVal pvc_FormatoReporte As String, ByVal pvc_RutaBase As String, ByVal pvc_NombreReporte As String) As Byte()
            Dim vlo_ListaEntParametroReporte As New List(Of EntParametroReporte)
            Dim vlo_EntParametroReporte As EntParametroReporte
            Dim vlb_ExcepcionDescarga As Boolean
            Dim vlo_EntReporte As EntReporte
            Dim vlo_Ws_SDP_ReportServer As Ws_SDP_ReportServer
            Dim vlo_row As DataRow
            Dim vlo_Tabla As DataTable
            Dim vlo_DataSet As DataSet
            Dim vlb_LiberarConexion As Boolean
            Dim vlo_Conexion As ConexionOracle
            Dim vlc_RutaTemporal As String
            Dim vlc_NombreArchivo As String
            Dim vlo_InfoDocumento As FileInfo
            Dim vln_NumeroBytes As Long
            Dim vlo_ArchivoLeer As FileStream
            Dim vlo_LectorBinario As BinaryReader
            Dim vlo_Arreglo As Byte()
            Dim vlo_DalOttDocumentoAnteproyect As DalOttDocumentoAnteproyect
            Dim vlo_DsDocumentoAnteProyecto As Data.DataSet
            Dim vlo_DalOttAdjuntoOrdenTrabajo As DalOttAdjuntoOrdenTrabajo
            Dim vlo_DsAdjuntoOrdenTrabajo As Data.DataSet
            Dim vlc_CondicionAdjuntos As String = String.Empty
            Dim vlc_extensionesFotos As String()
            Dim vlc_Extension As String = ""
            Dim vlc_NombreArchivoAdjunto As String = ""
            Dim vln_IndiceExtension As Integer = 0
            Dim vlc_LimiterExtension As String = "."

            Try

                If vgo_Conexion Is Nothing Then
                    vlo_Conexion = New ConexionOracle(vgc_CadenaConexion)
                    vlb_LiberarConexion = True
                Else
                    vlo_Conexion = vgo_Conexion
                    vlb_LiberarConexion = False
                End If

                vlo_Ws_SDP_ReportServer = New Ws_SDP_ReportServer
                vlo_Ws_SDP_ReportServer.Timeout = -1
                vlo_Ws_SDP_ReportServer.Credentials = System.Net.CredentialCache.DefaultCredentials
                vlo_Ws_SDP_ReportServer.Url = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_WSR_SDP_REPORT_SERVER)

                'configuracion de los reporte
                vlo_EntParametroReporte = New EntParametroReporte
                vlo_EntParametroReporte.Nombre = "pvc_Usuario"
                vlo_EntParametroReporte.Valor = pvc_Usuario
                vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                'configuracion de los reporte
                vlo_EntParametroReporte = New EntParametroReporte
                vlo_EntParametroReporte.Nombre = "pvc_Clave"
                vlo_EntParametroReporte.Valor = pvc_Clave
                vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                'configuracion de los reporte
                vlo_EntParametroReporte = New EntParametroReporte
                vlo_EntParametroReporte.Nombre = "pvc_Condicion"
                vlo_EntParametroReporte.Valor = pvc_Condicion
                vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                vlo_EntParametroReporte = New EntParametroReporte
                vlo_EntParametroReporte.Nombre = "pvc_Orden"
                vlo_EntParametroReporte.Valor = " "
                vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                'configuracion de los reporte
                vlo_EntParametroReporte = New EntParametroReporte
                vlo_EntParametroReporte.Nombre = "pvc_UsuarioEjecuta"
                vlo_EntParametroReporte.Valor = pvc_UsuarioEjecuta
                vlo_ListaEntParametroReporte.Add(vlo_EntParametroReporte)

                vlo_EntReporte = vlo_Ws_SDP_ReportServer.GenerarReporte(pvc_Usuario, pvc_Clave, pvc_FormatoReporte, pvc_RutaBase, pvc_NombreReporte, vlo_ListaEntParametroReporte.ToArray)

                vlo_DalOttDocumentoAnteproyect = New DalOttDocumentoAnteproyect(vlo_Conexion)
                vlo_DsDocumentoAnteProyecto = vlo_DalOttDocumentoAnteproyect.ListarRegistros(String.Format("{0} = {1} AND {2} = '{3}' AND {4} = {5}", Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_UBICACION, pvo_EntOttAnteproyecto.IdUbicacion, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ORDEN_TRABAJO, pvo_EntOttAnteproyecto.IdOrdenTrabajo, Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ETAPA_ORDEN_TRABAJO, EtapasOrdenTrabajo.ANTEPROYECTO), String.Empty, False, 0, 0)

                'Crea la Condicion de busqueda para los adjunto en etapa de anteproyecto
                For Each vlo_FilaDocumentoAnteProyecto In vlo_DsDocumentoAnteProyecto.Tables(0).Rows
                    If String.IsNullOrWhiteSpace(vlc_CondicionAdjuntos) Then
                        vlc_CondicionAdjuntos = String.Format("{0} = {1}", Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO, vlo_FilaDocumentoAnteProyecto(Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO).ToString)
                    Else
                        vlc_CondicionAdjuntos = String.Format("{0} OR {1} = {2}", vlc_CondicionAdjuntos, Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO, vlo_FilaDocumentoAnteProyecto(Modelo.OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO).ToString)
                    End If
                Next

                vlo_DalOttAdjuntoOrdenTrabajo = New DalOttAdjuntoOrdenTrabajo(vlo_Conexion)
                vlo_DsAdjuntoOrdenTrabajo = vlo_DalOttAdjuntoOrdenTrabajo.ListarRegistros(vlc_CondicionAdjuntos, String.Empty, False, 0, 0)

                vlo_Tabla = New DataTable
                vlo_Tabla.Columns.Add("ARCHIVO", Type.GetType("System.Byte[]"))

                vlo_DataSet = New DataSet
                vlo_DataSet.Tables.Add(vlo_Tabla)
                vlo_row = vlo_DataSet.Tables(0).NewRow

                vlo_row("ARCHIVO") = vlo_EntReporte.Reporte
                vlo_DataSet.Tables(0).Rows.Add(vlo_row)

                vlc_extensionesFotos = Constantes.EXTENSIONES_PERMITIDAS_FOTOGRAFIA.Split(",")

                For Each vlo_FilaAdjunto In vlo_DsAdjuntoOrdenTrabajo.Tables(0).Rows
                    vlc_NombreArchivoAdjunto = vlo_FilaAdjunto(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO).ToString
                    vlc_Extension = Path.GetExtension(vlc_NombreArchivoAdjunto).ToUpper

                    If vlc_Extension.ToString = ".PDF" Then
                        vlo_row = vlo_DataSet.Tables(0).NewRow
                        vlo_row("ARCHIVO") = CType(vlo_FilaAdjunto(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO), Byte())
                        vlo_DataSet.Tables(0).Rows.Add(vlo_row)
                    End If

                    Dim vlo_indice = vlc_extensionesFotos.ToList().IndexOf(vlc_Extension.ToString)

                    If vlc_extensionesFotos.ToList().IndexOf(vlc_Extension.ToString) >= 0 Then
                        vlo_row = vlo_DataSet.Tables(0).NewRow
                        vlo_row("ARCHIVO") = ImgPdf(CType(vlo_FilaAdjunto(Modelo.OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO), Byte()), vlc_NombreArchivoAdjunto)
                        vlo_DataSet.Tables(0).Rows.Add(vlo_row)
                    End If

                Next

                ConcatenarPdf(vlo_DataSet, "ReporteConcatenadoAnteProyecto") 'Construye el pdf

                vlc_RutaTemporal = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_RUTA_ESCRIBIR_ARCHIVOS) 'Ruta del documento generado
                vlc_NombreArchivo = "ReporteConcatenadoAnteProyecto.pdf"

                vlo_InfoDocumento = New FileInfo(vlc_RutaTemporal + vlc_NombreArchivo) 'Infomacion del documento
                vln_NumeroBytes = vlo_InfoDocumento.Length 'Tamaño en bytes del documento
                vlo_ArchivoLeer = New FileStream(vlc_RutaTemporal + vlc_NombreArchivo, FileMode.Open, FileAccess.Read)
                vlo_LectorBinario = New BinaryReader(vlo_ArchivoLeer)
                vlo_Arreglo = vlo_LectorBinario.ReadBytes(CInt(vln_NumeroBytes) - 1) 'Arreglo de bytes del pdf

                vlo_LectorBinario.Close()
                vlo_ArchivoLeer.Close()

                If File.Exists(vlc_RutaTemporal + vlc_NombreArchivo) Then
                    File.Delete(vlc_RutaTemporal + vlc_NombreArchivo)
                End If
                If vlo_Arreglo.Length > 0 Then
                    Return vlo_Arreglo
                End If

            Catch ex_Descarga As System.Threading.ThreadAbortException
                vlb_ExcepcionDescarga = True
            Catch ex As Exception
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Convierte una imagen a un PDF
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        ''' <author>Carlos Gómez Ondoy</author>
        ''' <creationDate>08/04/2016</creationDate>
        ''' <changeLog></changeLog>
        Private Function ImgPdf(pvo_imagen As Byte(), pvc_nombreImagen As String) As Byte()
            ''//Ruta temporal donde estamos trabajando
            Dim vlc_RutaTemporal = ConfigurationManager.AppSettings(ConstantesInternas.APP_KEY_RUTA_ESCRIBIR_ARCHIVOS)

            ''//El archivo que estamos creando
            Dim vlc_ArchivoTrabajo = Path.Combine(vlc_RutaTemporal, "Output.pdf")

            Dim vlc_rutaImagen = Path.Combine(vlc_RutaTemporal, pvc_nombreImagen)

            'asigna los bytes a una sección en memoria
            File.WriteAllBytes(vlc_rutaImagen, pvo_imagen)

            ''//Create our file with an exclusive writer lock
            Using vlo_FS As New FileStream(vlc_ArchivoTrabajo, FileMode.Create, FileAccess.Write, FileShare.None)
                ''//Create our PDF document
                Dim vlo_Doc As New Document(PageSize.LETTER)
                ''//Bind our PDF object to the physical file using a PdfWriter

                Dim vlo_Writer = PdfWriter.GetInstance(vlo_Doc, vlo_FS)
                ''//Open our document for writing
                vlo_Doc.Open()

                ''//Insert a blank page
                vlo_Doc.NewPage()

                ''//Add an image to a document. This does not scale the image or anything so if your image is large it might go off the canvas
                vlo_Doc.Add(iTextSharp.text.Image.GetInstance(vlc_rutaImagen))

                vlo_Writer.Flush()
                ''//Close our document
                vlo_Doc.Close()

                Return File.ReadAllBytes(vlc_ArchivoTrabajo)

            End Using

        End Function

#End Region

    End Class
End Namespace