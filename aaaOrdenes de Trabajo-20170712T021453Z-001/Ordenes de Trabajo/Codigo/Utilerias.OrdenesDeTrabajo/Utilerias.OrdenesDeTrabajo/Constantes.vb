Namespace Utilerias.OrdenesDeTrabajo
    Public Class Ordenamiento
        Public Const ASCENDENTE As String = "ASC"
        Public Const DESCENDENTE As String = "DESC"
    End Class

    'Public Class Siglas
    '    Public Const ARQUITECTONICO As String = "A"
    '    Public Const MECANICO As String = "M"
    '    Public Const ELECTRICO As String = "E"
    '    Public Const ESTRUCTURAL As String = "S"
    'End Class

    Public Class Estado
        Public Const ACTIVO As String = "ACT"
        Public Const INACTIVO As String = "INA"
    End Class

    Public Class Reportes
        Public Const RUTA_BASE As String = "Reportes_OrdenesDeTrabajo" 'Nombre de la carpeta en la cual serán publicados los reportes
        Public Const RPT_OT_BOLETA_ORDEN_TRABAJO As String = "Rpt_OT_BoletaOrdenTrabajo"
        Public Const RPT_OT_BOLETA_ORDEN_TRABAJO_CARTA As String = "Rpt_OT_BoletaOrdenTrabajoCarta" ' Rebuild de Rpt_OT_BoletaOrdenTrabajo

        Public Const RPT_OT_REPORTE_GENERAL_SECTORES_TALLERES As String = "Rpt_OT_ReporteGeneralSectoresTalleres"
        Public Const RPT_OT_REPORTE_ALERTAS_TIEMPO_ATENCION As String = "Rpt_OT_ReporteAlertasTiempoAtencion"
        Public Const RPT_OT_FICHA_TECNICA As String = "Rpt_OT_FichaTecnica"
        Public Const RPT_OT_ANTE_PROYECTO As String = "Rpt_OT_AnteProyecto"
        Public Const RPT_OT_DESICION_INICIAL As String = "Rpt_OT_DesicionInicial"
        Public Const RPT_OT_DESPACHO_MATERIALES As String = "Rpt_OT_DespachoMateriales"
        Public Const RPT_OT_REPORTE_ORDENES_TRABAJO As String = "Rpt_OT_ReporteOrdenesTrabajo"
        Public Const RPT_OT_SECTORES_ORDEN_TRABAJO As String = "Rpt_OT_SectoresOrdenTrabajo"
        Public Const RPT_OT_MATERIALES_UNIDAD_COMPRA_RAPIDA As String = "Rpt_OT_MaterialesUnidadCompraRapida"
        Public Const RPT_OT_SOLICITUD_COTIZACION_MATERIALES_AL_PROVEEDOR As String = "Rpt_OT_SolicitudCotizacionMaterialesAlProveedor"
    End Class

    Public Class Constantes
        Public Const CONEXION_ORDENES_DE_TRABAJO As String = "ORDENES_DE_TRABAJO"
        Public Const EHP_ORDENES_DE_TRABAJO As String = "EHP_ORDENES_DE_TRABAJO"
        Public Const MENSAJE_GENERICO_ERROR As String = "Se ha producido un error en el sistema y la información no ha sido registrada." & vbCrLf & "Por favor inténtelo nuevamente en unos minutos." & vbCrLf & "Si el problema persiste consulte al administrador del sistema"
        Public Const FORMATO_FECHA_UI As String = "dd/MM/yyyy"
        Public Const FORMATO_FECHA_LARGA_UI As String = "dddd dd 'de' MMMM 'del' yyyy"
        Public Const FORMATO_FECHA_HORA_UI As String = "dd/MM/yyyy HH:mm"
        Public Const FORMATO_FECHA_BD As String = "YYYY-MM-DD"
        Public Const FORMATO_DDL_TODOS As String = "[Todos...]"
        Public Const FORMATO_DDL_TODAS As String = "[Todas...]"
        Public Const FORMATO_DDL_SELECCIONE As String = "[Seleccione...]"
        Public Const TIPO_AREA_SEC As String = "SEC"
        Public Const TIPO_AREA_TAL As String = "TAL"
        Public Const CLASIFICACION_EDI As String = "EDI"
        Public Const CLASIFICACION_SIT As String = "SIT"
        Public Const fechaNula As DateTime = #1/1/1900#
        Public Const TAMANNO_CEDULA As Integer = 20
        Public Const TAMANNO_BYTES_A_MEGAS As Integer = 1048576
        Public Const ROL_COORDINADOR As String = "OT_Coordinador"

        Public Const EXTENSIONES_PERMITIDAS_FOTOGRAFIA As String = ".JPG,.JPEG,.PNG,.png,.jpg,.jpeg"

        Public Const APROBAR As Integer = 1
        Public Const DEVOLVER As Integer = 2
        Public Const DENEGAR As Integer = 3
        Public Const VISTO_BUENO As Integer = 4
        Public Const INDICAR_OBSERVACIONES As Integer = 5

        Public Const VISIBLE As Integer = 0
        Public Const OCULTO As Integer = 1

        Public Const SENNAS_EXACTAS As String = "Todo el edificio."
        Public Const DESCRIPCION_TRABAJO As String = "Labores de mantenimiento preventivo."
        Public Const VALOR_DEFECTO_STRING As String = "-"

        Public Const EXPRESION_REGULAR_NUMERO_ORDEN_TRABAJO As String = "'[0-9]+'"
    End Class

    Public Class Cantidad
        Public Const METRO As String = "MTS"
        Public Const METRO_CUADRADO As String = "MT2"
        Public Const METRO_CUBICO As String = "MT3"
    End Class

    Public Class TipoOrden
        Public Const ORDINARIA As String = "ORD"
        Public Const EMERGENCIA As String = "EME"
        Public Const PREVENTIVO As String = "PRE"
        Public Const GESTION_EXTERNA As String = "GEX"
    End Class

    Public Class EstadoOrden
        Public Const PENDIENTE_DE_ENVIO As String = "PEN"
        Public Const DEVUELTA As String = "DEV"
        Public Const PARA_EVALUACION_SOLICITANTE As String = "PES"
        Public Const ASIGNADA As String = "ASG"
        Public Const APROBADA As String = "APR"
        Public Const DENEGADA As String = "DEN"
        Public Const RECHAZADA As String = "REC"
        Public Const EN_TRAMITE As String = "ETR"
        Public Const LIQUIDADA As String = "LIQ"
        Public Const PENDIENTE_REVISION_DIRECTOR As String = "PDE"
        Public Const PENDIENTE_REVISION_SUPERVISOR As String = "PRS"
        Public Const EN_PROCESO As String = "EEJ"
        Public Const BORRADA As String = "BRD"
        Public Const RECIBIDO_CONFORME_SOLICITANTE As String = "PRC"
        Public Const NO_CONFORME As String = "NCF"
        Public Const EN_ESTUDIO As String = "EES"
        Public Const PARA_IMPRESION As String = "PIM"
        Public Const EN_EVALUACION As String = "EEV"
        Public Const EVALUACION_PRELIMINAR_PENDIENTE As String = "EPE"
        Public Const EVALUACION_PRELIMINAR_EVALUACION As String = "EPV"
        Public Const EVALUACION_PRELIMINAR_DEVUELTA_COORD As String = "EDC"
        Public Const EVALUACION_PRELIMINAR_REVISION_COORD As String = "ERC"
        Public Const EVALUACION_PRELIMINAR_APROBADA_COORD As String = "EAC"
        Public Const EVALUACION_PRELIMINAR_REVISION_JEFATURA As String = "ERJ"
        Public Const EVALUACION_PRELIMINAR_APROBADA_JEFATURA As String = "EAJ"
        Public Const EVALUACION_PRELIMINAR_DEVUELTA_JEFATURA As String = "EDJ"
        Public Const EN_ANTEPROYECTO As String = "EAP"
        Public Const ANTEPROYECTO_PENDIENTE_REVISION_SOLICITANTE As String = "APS"
        Public Const ANTEPROYECTO_DEVUELTO_SOLICITANTE As String = "APD"
        Public Const ANTEPROYECTO_APROBADO_SOLICITANTE As String = "APA"
        Public Const EVALUADA As String = "EVA"
        Public Const PENDIENTE_RESPUESTA_SOLICITANTE As String = "PRI"
        Public Const ELABORACION_DE_PLANOS As String = "EPL"
        Public Const ELABORACION_PRESUPUESTO As String = "EDP"
        Public Const PENDIENTE_REVISION_CONTRATACIONES As String = "PRO"
        Public Const GESTION_CONTRATACION As String = "GCT"
        Public Const PRESUPUESTO_DEVUELTO_COORDINADOR As String = "PDC"
        Public Const PRESUPUESTO_REVISION_COORDINADOR As String = "PRV"
        Public Const PRESUPUESTO_REVISION_JEFATURA As String = "PRJ"
        Public Const PRESUPUESTO_APROBADO_JEFATURA As String = "PAJ"
        Public Const PRESUPUESTO_DEVUELTO_JEFATURA As String = "PDJ"
        Public Const PRESUPUESTO_PENDIENTE_RESPUESTA_SOLICITANTE As String = "PPS"
        Public Const PRESUPUESTO_APROBADO_SOLICITANTE As String = "PAF"
        Public Const PRESUPUESTO_APROBADO_COORDINADOR As String = "PAC"
        Public Const CONTRATACION_REVISIÓN_EXPEDIENTE As String = "CRE"
        Public Const CONTRATACION_INICIO As String = "CIN"
        Public Const CONTRATACION_PUBLICACION_CARTEL As String = "CPC"
        Public Const CONTRATACION_VISITA_TECNICA As String = "CVT"
        Public Const CONTRATACION_ACLARACIONES As String = "CAC"
        Public Const CONTRATACION_OFERTAS As String = "COF"
        Public Const CONTRATACION_RECOMENDACION_TECNICA As String = "CRT"
        Public Const CONTRATACION_ADJUDICACION As String = "CAD"
        Public Const SUPERVISION_OBRA As String = "SOB"
        Public Const PENDIENTE_APROBACION_REQUISICION As String = "PAR"
        Public Const PARA_RETIRO_MATERIAL As String = "SDM"
        Public Const MATERIAL_PENDIENTE_COMPRA As String = "MPC"
        Public Const REVISION_PRESUPUESTO_SUPERVISOR As String = "RPS"
        Public Const SOLICITUD_MATERIAL_CREADA As String = "SMC"
        Public Const EN_PREPARACION_MATERIALES As String = "EPM"
        Public Const SOLICITUD_LISTA_PARA_RETIRO As String = "SLR"

        Public Const SOLICITUD_LISTA_RETIRO As String = "SLR"
        Public Const SOLICITUD_MATERIALES_ENTREGADA As String = "SME"

    End Class

    Public Class EstadoRevision
        Public Const DEVUELTA As String = "DEV"
        Public Const APROBADA As String = "APR"
        Public Const DENEGADA As String = "DEN"
    End Class

    Public Class Parametros
        Public Const TAMAÑO_MAXIMO_ARCHIVOS As Integer = 1
        Public Const DIRECCION_CORREO_ADMINISTRADOR As Integer = 2
        Public Const MAXIMO_EJECUCION_OBRAS As Integer = 27
        Public Const CODIGO_UNIDAD_SERVICIOS_GENERALES As Integer = 4
        Public Const PLAZO_LIMITE_RECIBIDO_CONFORME As Integer = 9
        Public Const PLAZO_LIMITE_RECIBIDO_CONFORME_DISENNO As Integer = 10
        Public Const TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_ATENCION As Integer = 5
        Public Const TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_ATRASO As Integer = 6
        Public Const TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_EN_RANGO As Integer = 7
        Public Const TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_ATRASO_DISENNO As Integer = 12
        Public Const TOPE_DIAS_HABILES_GENERAR_ALERTA_OT_ATENCION_DISENNO As Integer = 11
        Public Const SEDE_RODRIGO_FACIO As Integer = 2
        Public Const CORREOS_RECEPCION As Integer = 13
        Public Const VALOR_PARA_HABILITAR_TERCERA_ETAPA_SISTEMA As Integer = 14
        Public Const INICIALES_PERMITIDAS_ARCHIVOS_ELABORACION_PLANOS As Integer = 15
        Public Const VALOR_SECUENCIA_COMPRA_RAPIDA As Integer = 16
        Public Const DIRECCION_TELEFONO_COTIZACION_FONDO_TRABAJO As Integer = 17
        Public Const DIRECCION_CORREO_COTIZACION_FONDO_TRABAJO As Integer = 18
        Public Const TIEMPO_ALERTA_MATERIAL_GESTION_COMPRA_RAPIDA As Integer = 19
        Public Const VALOR_SECUENCIA_SUMINISTROS As Integer = 20
        Public Const VALOR_SECUENCIA_UNIDAD_ESPECIALIZADA_COMPRAS As Integer = 21
        Public Const VALOR_SECUENCIA_FONDO_DE_TRABAJO As Integer = 22
        Public Const GESTIONES_COMPRA_FINALIZADAS As Integer = 23
        Public Const ENCARGADO_RECEPCION_MATERIALES As Integer = 24
        Public Const LUGAR_RECEPCION_MATERIALES As Integer = 25
        Public Const DATOS_FORMA_PAGO_CONTACTO As Integer = 26
        Public Const VALOR_PARA_HABILITAR_WS_PDAGO As Integer = 28
    End Class

    Public Class Unidades
        Public Const MINUTOS As Integer = 1
        Public Const HORAS As Integer = 2
        Public Const DIAS As Integer = 3
        Public Const SEMANAS As Integer = 4
        Public Const MESES As Integer = 5
        Public Const ANIOS As Integer = 6
    End Class

    Public Class RolesSistema
        Public Const OT_DIRECTOR_UNIDAD As String = "OT_DirectorUnidad"
        Public Const OT_AUTORIZADOR_SOLICITUD As String = "OT_Autorizador_Solicitud"
        Public Const OT_SUPERVISOR As String = "OT_Supervisor"
        Public Const OT_COORDINADOR As String = "OT_Coordinador"
        Public Const OT_REGISTRO_SOLICITUD As String = "OT_RegistroSolicitud"
        Public Const OT_COORDINADOR_MANTENIMIENTO As String = "OT_Coordinador_Mantenimiento"
        Public Const OT_PROFESIONAL_DISENIO As String = "OT_Profesional_Disenio"
        Public Const OT_COORDINADOR_DISENIO As String = "OT_Coordinador_Disenio"
        Public Const OT_DELEGADO_JEFE_SECCION As String = "OT_Autorizado_Jefe_Seccion"
        Public Const OT_JEFE_SECCION As String = "OT_Jefe_Seccion"
        Public Const OT_ENCARGADO_CONTRATACION As String = "OT_Encargado_Contratacion"
        Public Const OT_CATALOGOS As String = "OT_Catalogos"
        Public Const OT_ENCARGADO_ALMACEN As String = "OT_Encargado_Almacen"
        Public Const OT_ALISTADOR_ALMACEN As String = "OT_Alistador_Almacen"
        Public Const OT_DESPACHADOR_ALMACEN As String = "OT_Despachador_Almacen"
        Public Const OT_ENCARGADO_INVENTARIO As String = "OT_Encargado_Inventario"
        Public Const OT_REVISOR_REQUISICIONES As String = "OT_Revisor_Requisiciones"
        Public Const OT_ENCARGADO_COMPRA_RAPIDA As String = "OT_Encargado_Compra_Rapida"
        Public Const OT_GESTOR_DE_INVENTARIO As String = "OT_Gestor_De_Inventario"
        Public Const OT_SUPERVISOR_SECCION As String = "OT_Supervisor_Seccion"
        Public Const OT_ENCARGADO_FONDO_TRABAJO As String = "OT_Encargado_Fondo_Trabajo"
        Public Const OT_ENCARGADO_ADMINISTRATIVO As String = "OT_Encargado_Administrativo"
        Public Const OT_UNIDAD_ESPECIALIZADA_COMPRA As String = "OT_Unidad_Especializada_Compra"
        Public Const OT_ASISTENTE_INVENTARIO As String = "OT_Asistente_Inventario"
        Public Const OT_AUTORIZADO_JEFE_SECCION As String = "OT_Autorizado_Jefe_Seccion"
    End Class

    Public Class UnidadCierre
        Public Const MANTENIMIENTO As String = "MNT"
        Public Const DISENIO As String = "DIS"
    End Class

    Public Class CondicionEstado
        Public Const EN_TRAMITE As String = "ETR"
        Public Const TRAMITADA As String = "TRA"
    End Class

    Public Class TipoValor
        Public Const NUMERICO As String = "NUM"
        Public Const CARACTER As String = "CAR"
        Public Const INDICADOR As String = "IND"
    End Class

    Public Class Ambito
        Public Const CONTRATACIONES As String = "CON"
        Public Const COMPRAS As String = "COM"
        Public Const AMBOS As String = "AMB"
    End Class

    Public Class FichaTecnica
        Public Const REQUIERE_FICHA_TECNICA = 1
        Public Const NO_REQUIERE_FICHA_TECNICA = 0
    End Class

    Public Class Proteccion
        Public Const PROTEGIDO = 1
        Public Const NO_PROTEGIDO = 0
    End Class

    Public Class Area
        Public Const OPERARIO = "OPE"
        Public Const PROFESIONAL = "PRO"
    End Class

    Public Class Tipo
        Public Const ALMACEN = "ALM"
        Public Const BODEGA = "BOD"
    End Class

    Public Class Jornada
        Public Const MANANA = "MAN"
        Public Const MANANA_STR As String = "Mañana"
        Public Const TARDE = "TAR"
        Public Const TARDE_STR As String = "Tarde"
    End Class

    Public Class Version
        Public Const EDITABLE As Integer = 1
        Public Const NO_EDITABLE As Integer = 0
    End Class

    Public Class EstadoRegistro
        Public Const PENDIENTE_ENVIO = "PEV"
        Public Const PENDIENTE_APROBACION = "PEN"
        Public Const APROBADA = "APR"
        Public Const DENEGADA = "DEN"
        Public Const EN_PROCESO_COMPRA = "EPC"
        Public Const RECIBIDO_BODEGA = "REB"
    End Class

    Public Class EstadoDetalle
        Public Const PENDIENTE = "PEN"
        Public Const PENDIENTE_ENVIO_STR As String = "Pendiente de Envío"
        Public Const PENDIENTE_APROBACION = "PAP"
        Public Const PENDIENTE_APROBACION_STR As String = "Pendiente de Aprobación"
        Public Const APROBADO = "APR"
        Public Const APROBADO_STR As String = "Aprobado"

    End Class

    Public Class EstadoSolicitudReingreso
        Public Const PENDIENTE = "PEN"
        Public Const APROBADA = "APR"
        Public Const DENEGADA = "DEN"
    End Class

    Public Class EstadoIncidente
        Public Const CREADO = "CRE"
        Public Const PENDIENTE = "PEN"
        Public Const ATENDIDO = "ATE"
    End Class

    Public Class EstadoProveedorCotizacion
        Public Const ENVIADO = "ENV"
        Public Const PENDIENTE_DE_ENVIO = "PEN"
    End Class

    Public Class EstadoGestionCompra
        Public Const CREADA = "CRE"
        Public Const SOLICITUD_DE_COTIZACONES = "SOL"
        Public Const REGISTRO_DE_COTIZACIONES = "REG"
        Public Const APROBACION_DEL_SUPERVISOR = "APS"
        Public Const APROBACION_DE_PRESUPUESTO = "APP"
        Public Const APROBACION_DE_JEFATURA = "APJ"
        Public Const GESTION_DE_CHEQUE = "GCH"
        Public Const PENDIENTE_INGRESO_ALMACEN = "PIA"
        Public Const REGISTRO_DE_INGRESOS_EN_ALMACEN = "ING"
        Public Const INGRESO_GESTION_GECO = "IGG"
        Public Const APROBACION_JEFE_ADMINISTRATIVO = "AJA"
        Public Const APROBACION_JEFATURA_REVISION_TECNICA = "AJR"
        Public Const APROBACION_TECNICA = "APT"
        Public Const GESTION_UNIDAD_ESPECIALIZADA_COTIZACION = "UEC"
        Public Const DEVUELTA_GESTOR_INVENTARIO = "DGI"
        Public Const GESTION_UNIDAD_ESPECIALIZADA_PONDERACION = "UEP"
        Public Const GESTION_UNIDAD_ESPECIALIZADA_REGISTRO_COTIZACION = "UER"
    End Class

    Public Class OrigenAdjunto
        Public Const REGISTRADOR = "REG"
        Public Const REVISOR = "REV"
    End Class

    Public Class EstadoSolicitudRetiro
        Public Const SOLICITUD_MATERIAL_CREADA = "SMC"
        Public Const PREPARACION_MATERIALES = "EPM"
        Public Const SOLICITUD_LISTA_RETIRO = "SLR"
        Public Const SOLICITUD_MATERIALES_ENTREGADA = "SME"
    End Class

    Public Class EstadoSolicitudMaterial
        Public Const INGRESADO_POR_COORDINADOR = "ING"
        Public Const APROBACION_DE_REQUISICION = "APR"
        Public Const APROBACION_DE_PRESUPUESTO = "APP"
        Public Const APROBADA = "APD"
    End Class

    Public Class EstadoTraslado
        Public Const CREADA As String = "CRE"
        Public Const CREADA_STR As String = "Creada"
        Public Const PENDIENTE_APROBACION As String = "PAR"
        Public Const PENDIENTE_APROBACION_STR As String = "Pendiente de Aprobación de Requisiciones"
        Public Const APROBADA As String = "APR"
        Public Const APROBADA_STR As String = "Aprobada"
        Public Const PREPARACION_MATERIAL As String = "EPM"
        Public Const PREPARACION_MATERIAL_STR As String = "En Preparación de Materiales"
        Public Const SOLICITUD_RETIRO As String = "SLR"
        Public Const SOLICITUD_RETIRO_STR As String = "Solicitud lista para retiro"
        Public Const SOLICITUD_ENTREGA As String = "SME"
        Public Const SOLICITUD_ENTREGA_STR As String = "Solicitud de Materiales Entregada"
        Public Const DEVUELTA As String = "DEV"
        Public Const DEVUELTA_STR As String = "Devuelta"
    End Class

    Public Class EstadoAprovisionamiento
        Public Const CREADO As String = "CRE"
        Public Const CREADO_STR As String = "Creado"
        Public Const COMPLETADO As String = "COM"
        Public Const COMPLETADO_STR As String = "Completado"
    End Class

    Public Class TipoProveedor
        Public Const FISICO As String = "FIS"
        Public Const JURIDICO As String = "JUR"
    End Class
    ''' <summary>
    ''' Etapas de ordenes de trabajo
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/02/2016</creationDate>
    ''' <changeLog>
    '''    <author>Cesar Bermudez</author>
    '''    <creationDate>25/02/2016</creationDate>
    '''    <change>se agrega la etapa EVALUACION_PRELIMINAR_INFORME</change>
    ''' </changeLog>
    Public Class EtapasOrdenTrabajo
        Public Const SOLICITUD As Integer = 2
        Public Const EVALUACION As Integer = 3
        Public Const EJECUCION As Integer = 4
        Public Const ANALISIS_VIABILIDAD_TECNICA As Integer = 5
        Public Const INFORME_VIABILIDAD_TECNICA As Integer = 6
        Public Const RESPUESTA_INFORME_VIABILIDAD_TECNICA As Integer = 7
        Public Const ANTEPROYECTO As Integer = 8
        Public Const EVALUACION_PRELIMINAR_INFORME As Integer = 9
        Public Const ELABORACION_PLANOS As Integer = 10
        Public Const PRESUPUESTO As Integer = 11
        Public Const CONTRATACIONES As Integer = 12
    End Class

    ''' <summary>
    ''' Tipos de documento
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>Carlos Gómez Ondoy</author>
    ''' <creationDate>01/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Class TipoDocumento
        Public Const FOTOGRAFIA As Integer = 1
        Public Const OFICIO As Integer = 3
        Public Const AVAL_FORESTA As Integer = 4
        Public Const AVAL_PLANTA_FISICA As Integer = 5
        Public Const CLAUSULA_PENAL As Integer = 6
        Public Const CARTEL As Integer = 8
        Public Const GENERICO As Integer = 9
        Public Const SOLICITUD_GECO As Integer = 10
    End Class

    ''' <summary>
    ''' Cargo asociado al funcinario segun el proyecto
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Class Cargo
        Public Const OPERARIO As String = "OPE"
        Public Const COLABORADOR As String = "COL"
        Public Const ENCARGADO As String = "ENC"
        Public Const PROFESIONAL = "PRO"
    End Class

    Public Class Documento
        Public Const TRAMITADO As Integer = 1
        Public Const NO_TRAMITADO As Integer = 0
    End Class

    ''' <summary>
    ''' Clasificación para el tiempo, EST: ESTIMADO RAL: REAL
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>03/02/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Class Clasificacion
        Public Const ESTIMADO As String = "EST"
        Public Const REAL As String = "RAL"
    End Class

    ''' <summary>
    ''' Clasificación para materiales: A- Alta rotación, B- Baja rotación, C- Segunda'
    ''' </summary>
    ''' <remarks></remarks>
    ''' <author>César Bermúdez García</author>
    ''' <creationDate>29/05/2016</creationDate>
    ''' <changeLog></changeLog>
    Public Class ClasificacionMateriales
        Public Const ALTA_ROTACION As String = "A"
        Public Const BAJA_ROTACION As String = "B"
        Public Const SEGUNDA As String = "C"
    End Class

    Public Class DescripcionAdjuntos
        Public Const ORDENES_USUARIO As String = "Fotografía con detalle del trabajo proporcionado por el solicitante"
        Public Const ORDENES_RECEPCION As String = "Fotografía con detalle del trabajo anexos desde recepcion"
        Public Const ORDENES_DISENIO As String = "Fotografía con detalle del trabajo anexos por encargado de diseño"
        Public Const ORDENES_JEFE_ADMINISTRATIVO As String = "Fotografía con detalle del trabajo anexos por jefe administrativo"
        Public Const ADJUNTOS_OFICIO As String = "Oficio de respuesta al solicitante por parte de la jefatura de la sección de mantenimiento y construcción"
    End Class

    Public Class EtapaContratacion
        Public Const EXPEDIENTE_TECNICO As Integer = 1
        Public Const INICIO As Integer = 2
        Public Const PUBLICACION_CARTEL As Integer = 3
        Public Const VISITA_TECNICA As Integer = 4
        Public Const ACLARACIONES As Integer = 5
        Public Const OFERTAS As Integer = 6
        Public Const RECOMENDACION_TECNICA As Integer = 7
        Public Const ADJUDICACION As Integer = 8

    End Class

    Public Class Dias
        Public Const NATURALES As Integer = 0
        Public Const HABILES As Integer = 1
    End Class

    Public Class FormaDias
        Public Const NATURALES As String = "NAT"
        Public Const HABILES As String = "HAB"
    End Class

    Public Class ViaDespacho
        Public Const ALMACEN As String = "ALM"
        Public Const VIACOMPRA As String = "VCM"
    End Class

    Public Class FiltroPor
        Public Const ARTICULOS_CERO As Integer = 1
        Public Const PUNTO_REORDEN As Integer = 2
        Public Const CATEGORIA As Integer = 3
        Public Const PARTIDA As Integer = 4
        Public Const PALABRA_CLAVE As Integer = 5
        Public Const ARTICULOS_CERO_STR As String = "Artículos en Cero"
        Public Const PUNTO_REORDEN_STR As String = "Punto de Reorden"
        Public Const CATEGORIA_STR As String = "Categoría / Sub Categoría"
        Public Const PARTIDA_STR As String = "Partida"
        Public Const PALABRA_CLAVE_STR As String = "Palabra Clave"
    End Class

    Public Class TipoAjuste
        Public Const INDIVIDUAL = "IND"
        Public Const GLOBALL = "GBL"
        Public Const EXISTENCA = "EXS"
        Public Const INDIVIDUAL_STR = "Individual"
        Public Const GLOBAL_STR = "Global"
        Public Const EXISTENCA_STR = "Existencia"
    End Class

    Public Class EstadoAjuste
        Public Const CREADO = "CRE"
        Public Const APROBACION_SUPERVISOR = "APS"
        Public Const APROBACION_JEFATURA = "APJ"
        Public Const APROBADO = "APR"
        Public Const DEVUELTO_JEFATURA = "DEJ"
        Public Const DEVUELTO_SUPERVISOR = "DES"
    End Class

    Public Class TipoMovimiento
        Public Const INCREMENTO = "INC"
        Public Const DECREMENTO = "DEC"
    End Class

    Public Class EstadoGestionIngr
        Public Const CREADA = "CRE"
        Public Const VALIDACION_MONTOS = "VAM"
        Public Const DEVUELTA = "DEV"
        Public Const TRAMITADA = "TRA"
    End Class
End Namespace
