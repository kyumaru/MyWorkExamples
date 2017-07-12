Namespace Utilerias.SistemaTransportes
	Public Class Ordenamiento
		Public Const ASCENDENTE As String = "ASC"
		Public Const DESCENDENTE As String = "DESC"
	End Class

	Public Class Estado
		Public Const ACTIVO As String = "ACT"
		Public Const INACTIVO As String = "INA"
	End Class

	Public Class Reportes
		Public Const RUTA_BASE As String = "Reportes_SistemaTransportes" 'Nombre de la carpeta en la cual serán publicados los reportes
	End Class

	Public Class Constantes
		Public Const CONEXION_TRANSPORTES As String = "TRANSPORTES"
		Public Const EHP_TRANSPORTES As String = "EHP_TRANSPORTES"
		Public Const MENSAJE_GENERICO_ERROR As String = "Se ha producido un error en el sistema y la información no ha sido registrada." & vbCrLf & "Por favor inténtelo nuevamente en unos minutos." & vbCrLf & "Si el problema persiste consulte al administrador del sistema"
		Public Const FORMATO_FECHA_UI As String = "dd/MM/yyyy"
		Public Const FORMATO_FECHA_LARGA_UI As String = "dddd dd 'de' MMMM 'del' yyyy"
		Public Const FORMATO_FECHA_HORA_UI As String = "dd/MM/yyyy HH:mm"
		Public Const FORMATO_FECHA_BD As String = "YYYY-MM-DD"
	End Class
End Namespace
