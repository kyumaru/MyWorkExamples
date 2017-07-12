Namespace Utilerias.OrdenesDeTrabajo
	Public Class OrdenesDeTrabajoException
		Inherits Exception
#Region "Constantes"
		Public Const NOMBRE_CLASE As String = "OrdenesDeTrabajoException"
#End Region

#Region "Constructores"
		Public Sub New()
			MyBase.New()
		End Sub

		Public Sub New(ByVal pcv_MensjeError As String)
			MyBase.New(pcv_MensjeError)
		End Sub

		Public Sub New(ByVal pcv_MensjeError As String, ByVal pvo_InnerException As Exception)
			MyBase.New(pcv_MensjeError, pvo_InnerException)
		End Sub

		Public Sub New(ByVal pvo_Info As System.Runtime.Serialization.SerializationInfo, ByVal pvo_Context As System.Runtime.Serialization.StreamingContext)
			MyBase.New(pvo_Info, pvo_Context)
		End Sub
#End Region

#Region "Funciones"
		Public Function GetSoapExceptionDetail() As System.Xml.XmlNode
			Dim vlo_XmlDocument As System.Xml.XmlDocument
			Dim vlo_XmlNodoRaiz As System.Xml.XmlNode
			Dim vlo_XmlAtributo As System.Xml.XmlAttribute

			'crear el xml document
			vlo_XmlDocument = New System.Xml.XmlDocument

			'crear el nodo raiz
			vlo_XmlNodoRaiz = vlo_XmlDocument.CreateNode(System.Xml.XmlNodeType.Element, System.Web.Services.Protocols.SoapException.DetailElementName.Name, System.Web.Services.Protocols.SoapException.DetailElementName.Namespace)

			'crear atributo
			vlo_XmlAtributo = vlo_XmlDocument.CreateAttribute("Message")
			vlo_XmlAtributo.Value = Me.Message

			'agregar atributo al nodo
			vlo_XmlNodoRaiz.Attributes.Append(vlo_XmlAtributo)

			Return vlo_XmlNodoRaiz
		End Function

        Public Shared Function GetFromSoapException(ByVal pvo_Ex As System.Web.Services.Protocols.SoapException) As OrdenesDeTrabajoException
            Return New OrdenesDeTrabajoException(pvo_Ex.Detail.Attributes("Message").Value.Trim)
        End Function
#End Region
	End Class
End Namespace
