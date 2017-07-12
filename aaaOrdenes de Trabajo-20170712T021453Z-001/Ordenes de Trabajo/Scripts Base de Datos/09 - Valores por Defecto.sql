DECLARE

BEGIN
/*Agrega los tipos de presupuesto*/
prI_OTC_ESTADO_ORDEN_TRABAJO('PRS','Pendiente de revision de supervisor');
END;




/*
	Autor:			Carlos Gómez Ondoy
	Fecha:			01/02/2016
	Descripcion:	Agrega las diferentes etapas identificadas del proceso de las ordenes de trabajo
*/

DECLARE

BEGIN
	INSERT INTO OTM_ETAPA_ORDEN_TRABAJO (ID_ETAPA_ORDEN_TRABAJO, DESCRIPCION, ORDEN, ESTADO, USUARIO) VALUES (2, 'Solicitud', 1, 'ACT', 'SISTEMA');
	INSERT INTO OTM_ETAPA_ORDEN_TRABAJO (ID_ETAPA_ORDEN_TRABAJO, DESCRIPCION, ORDEN, ESTADO, USUARIO) VALUES (3, 'Evaluación', 2, 'ACT', 'SISTEMA');
	INSERT INTO OTM_ETAPA_ORDEN_TRABAJO (ID_ETAPA_ORDEN_TRABAJO, DESCRIPCION, ORDEN, ESTADO, USUARIO) VALUES (4, 'Ejecución', 3, 'ACT', 'SISTEMA');
	INSERT INTO OTM_ETAPA_ORDEN_TRABAJO (ID_ETAPA_ORDEN_TRABAJO, DESCRIPCION, ORDEN, ESTADO, USUARIO) VALUES (5, 'Análisis de Viabilidad Técnica', 4, 'ACT', 'SISTEMA');
	INSERT INTO OTM_ETAPA_ORDEN_TRABAJO (ID_ETAPA_ORDEN_TRABAJO, DESCRIPCION, ORDEN, ESTADO, USUARIO) VALUES (6, 'Informe de Viabilidad Técnica', 5, 'ACT', 'SISTEMA');
	INSERT INTO OTM_ETAPA_ORDEN_TRABAJO (ID_ETAPA_ORDEN_TRABAJO, DESCRIPCION, ORDEN, ESTADO, USUARIO) VALUES (7, 'Respúesta al Informe de Viabilidad Técnica', 6, 'ACT', 'SISTEMA');
	INSERT INTO OTM_ETAPA_ORDEN_TRABAJO (ID_ETAPA_ORDEN_TRABAJO, DESCRIPCION, ORDEN, ESTADO, USUARIO) VALUES (8, 'Anteproyecto', 7, 'ACT', 'SISTEMA');
	INSERT INTO OTM_ETAPA_ORDEN_TRABAJO (ID_ETAPA_ORDEN_TRABAJO, DESCRIPCION, ORDEN, ESTADO, USUARIO) VALUES (9, 'Evaluación Preliminar Informe', 8, 'ACT', 'SISTEMA');
END;



/*
	Autor:			Carlos Gómez Ondoy
	Fecha:			02/02/2016
	Descripcion:	Agrega las tipos de documento de las ordenes de trabajo
*/
DECLARE

BEGIN

	INSERT INTO OTM_TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO, DESCRIPCION, TAMANIO_MAXIMO, FORMATOS_ADMITIDOS, USUARIO) VALUES (1, 'Fotografía', 45, 'PDF,JPG,PNG', 'SISTEMA');
	INSERT INTO OTM_TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO, DESCRIPCION, TAMANIO_MAXIMO, FORMATOS_ADMITIDOS, USUARIO) VALUES (3, 'OFICIO', 45, 'DOC,PDF,ODT', 'SISTEMA');
	INSERT INTO OTM_TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO, DESCRIPCION, TAMANIO_MAXIMO, FORMATOS_ADMITIDOS, USUARIO, PROTEGIDO) VALUES (4, 'AVAL FORESTA', 2, 'PDF', 'SISTEMA', 1);
	INSERT INTO OTM_TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO, DESCRIPCION, TAMANIO_MAXIMO, FORMATOS_ADMITIDOS, USUARIO, PROTEGIDO) VALUES (5, 'AVAL PLANTA FISICA', 2, 'PDF', 'SISTEMA', 1);	

	INSERT INTO OTF_OPERARIO_AREA(NUM_EMPLEADO ,ID_SECTOR_TALLER, ID_AREA_PROFESIONAL,CATEGORIA_LABORAL ,ESTADO, USUARIO,TIME_STAMP )
	SELECT NUM_EMPLEADO, ID_SECTOR_TALLER,NULL,'OPE' ,'ACT', USUARIO,TIME_STAMP FROM OTF_OPERARIO;

END;


/*
	Autor:			Carlos Gómez Ondoy
	Fecha:			08/08/2016
	Descripcion:	Agrega los Estados
*/
DECLARE

BEGIN
INSERT INTO OTC_ESTADO_GESTION_COMPRA (ESTADO, DESCRIPCION) VALUES ('CRE', 'Creada');
INSERT INTO OTC_ESTADO_GESTION_COMPRA (ESTADO, DESCRIPCION) VALUES ('SOL', 'Solicitud de Cotizaciones');
INSERT INTO OTC_ESTADO_GESTION_COMPRA (ESTADO, DESCRIPCION) VALUES ('REG', 'Registro de Cotizaciones');
INSERT INTO OTC_ESTADO_GESTION_COMPRA (ESTADO, DESCRIPCION) VALUES ('APS', 'Aprobación del Supervisor');
INSERT INTO OTC_ESTADO_GESTION_COMPRA (ESTADO, DESCRIPCION) VALUES ('APP', 'Aprobación de Presupuesto');
INSERT INTO OTC_ESTADO_GESTION_COMPRA (ESTADO, DESCRIPCION) VALUES ('APJ', 'Aprobación de la Jefatura');
INSERT INTO OTC_ESTADO_GESTION_COMPRA (ESTADO, DESCRIPCION) VALUES ('GCH', 'Gestión de Cheque');
INSERT INTO OTC_ESTADO_GESTION_COMPRA (ESTADO, DESCRIPCION) VALUES ('PIA', 'Pendiente de Ingreso a Almacén');
INSERT INTO OTC_ESTADO_GESTION_COMPRA (ESTADO, DESCRIPCION) VALUES ('ING', 'Registro de Ingresos a Almacén');
INSERT INTO OTC_ESTADO_GESTION_COMPRA (ESTADO, DESCRIPCION) VALUES ('IGG', 'Ingreso Gestión Geco');
INSERT INTO OTC_ESTADO_GESTION_COMPRA (ESTADO, DESCRIPCION) VALUES ('AJA', 'Aprobación del Jefe Administrativo');
INSERT INTO OTC_ESTADO_GESTION_COMPRA (ESTADO, DESCRIPCION) VALUES ('DGI', 'Devuelta a Gestor de Inventario');
END;

/*
	Autor:			JMCR
	Fecha:			11/08/2016
	Descripcion:	Agrega las diferentes estados de estado de traslado
*/

DECLARE

BEGIN
	INSERT INTO OTC_ESTADO_TRASLADO (ESTADO_TRASLADO, DESCRIPCION) VALUES ('CRE','Creada');
	INSERT INTO OTC_ESTADO_TRASLADO (ESTADO_TRASLADO, DESCRIPCION) VALUES ('PAR','Pendiente de Aprobación de Requisiciones');
	INSERT INTO OTC_ESTADO_TRASLADO (ESTADO_TRASLADO, DESCRIPCION) VALUES ('APR','Aprobada');
	INSERT INTO OTC_ESTADO_TRASLADO (ESTADO_TRASLADO, DESCRIPCION) VALUES ('EPM','En Preparación de Materiales');
	INSERT INTO OTC_ESTADO_TRASLADO (ESTADO_TRASLADO, DESCRIPCION) VALUES ('SLR','Solicitud lista para retiro');
	INSERT INTO OTC_ESTADO_TRASLADO (ESTADO_TRASLADO, DESCRIPCION) VALUES ('SME','Solicitud de Materiales Entregada');
	INSERT INTO OTC_ESTADO_TRASLADO (ESTADO_TRASLADO, DESCRIPCION) VALUES ('DEV','Devuelta');
END;

/*
	Autor:			JMCR
	Fecha:			11/08/2016
	Descripcion:	Agrega las diferentes parametros
*/

DECLARE

BEGIN
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (1,'Tamaño máximo de archivos adjuntos de Mb', '1', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (2,'Dirección de correo electrónico del administrador del sistema', 'mantenimientoyconstruccion@ucr.ac.cr', 0, 0, 'SISTEMA', SYSDATE, 1);
/* El numero 3 no existe*/
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (4,'Código de unidad de la Oficina de Servicios Generales de SIRH', '263', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (5,'Tope de días hábiles para generar alerta de orden de trabajo para atención (Amarillo)', '15', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (6,'Tope de días hábiles para generar alerta de orden de trabajo con atraso (Rojo)', '20', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (7,'Tope de días hábiles para generar alerta de orden de trabajo dentro de rango (Verde)', '10', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (8,'Tope de días hábiles para generar alerta de orden de trabajo con atraso - OTs de Diseño', '20', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (9,'Plazo límite en días hábiles para recibido conforme de órdenes de trabajo', '5', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (10,'Plazo límite en días hábiles para recibido conforme de órdenes de trabajo de diseño', '3', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (11,'Tope de Días hábiles para generar alera de orden de trabajo para atención de OTs Diseño (Amarillo)', '30', 0, 0, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (12,'Tope de días hábiles para generar alerta de orden de trabajo con atraso para OTs de Diseño(Rojo)', '40', 0, 0, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (13,'Correos Recepción', 'erick.guillenfigueroa@ucr.ac.cr', 0, 0, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (14,'Valor para habilitar tercera etapa del sistema', '0', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (15,'Iniciales permitidas para nombres de archivos en elaboración de planos', 'A;M;E;S', 0, 0, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (16,'Valor de la secuencia ID_VIA_COMPRA_CONTRATO para compra rápida', '47', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (17,'Teléfonos para Cotizaciones de Fondo de Trabajo', '83715492;6973232;20167995', 0, 0, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (18,'Correos para Cotizacion de Fondos de Trabajo', 'oficinaserviciosGenerales@gmail.com;oficinaadministracionmaterial@gmail.com;controlgestiondecompras@gmail.com', 0, 0, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (19,'Tiempo de alerta material en gestión de compra rápida', '30', 0, 0, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (20,'Valor de la secuencia ID_VIA_COMPRA_CONTRATO para suministros', '14', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (21,'Valor de la secuencia ID_VIA_COMPRA_CONTRATO para unidad especiaizada de compras', '15', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (22,'Valor de la secuencia ID_VIA_COMPRA_CONTRATO para fondo de trabajo', '45', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (23,'Gestiones de Compra Finalizadas ', '14,15,45', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (24,'Encargado de Recepción de materiales (Fondo de Trabajo)', 'el Sr. Jorge Salazar Sánchez, encargado del Almacén al 2511-5251.', 0, 0, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (25,'Lugar para Recepción de materiales (Fondo de Trabajo)', 'Edificio Saprissa, frente a la Escuela de Estudios Generales, Universidad de Costa Rica, San Pedro de Montes de Oca.', 0, 0, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (26,'Datos para Forma de Pago y Contacto (Fondo de Trabajo)', 'El pagó de esta adquisición se realizará por medio de cheque, la entrega del mismo debe ser coordinado con la Sra, Marianella Rodriguez al 2511-5473, una vez entregado el material.', 0, 0, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (27,'Máximo parametrizado para ejecución de obras.', '200000', 0, 1, 'SISTEMA', SYSDATE, 1);
INSERT INTO OTP_PARAMETRO_UBICACION VALUES (28,'Valor para habilitar WS del sistema PDAGO', '0', 0, 1, 'SISTEMA', SYSDATE, 1);
END;



/*
	Autor:			Carlos Gómez Ondoy
	Fecha:			12/09/2016
	Descripcion:	Actualización de descripciones de los estados
*/

DECLARE

BEGIN

	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Anteproyecto (Aprobado por el Solicitante)' WHERE ESTADO_ORDEN_TRABAJO = 'APA';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Anteproyecto (Devuelto por el Solicitante)' WHERE ESTADO_ORDEN_TRABAJO = 'APD';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Anteproyecto (Pendiente Revision del Solicitante)' WHERE ESTADO_ORDEN_TRABAJO = 'APS';


	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Contratación (Aclaraciones)' WHERE ESTADO_ORDEN_TRABAJO = 'CAC';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Contratación (Adjudicación)' WHERE ESTADO_ORDEN_TRABAJO = 'CAD';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Contratación (Inicio)' WHERE ESTADO_ORDEN_TRABAJO = 'CIN';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Contratación (Ofertas)' WHERE ESTADO_ORDEN_TRABAJO = 'COF';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Contratación (Publicación Cartel)' WHERE ESTADO_ORDEN_TRABAJO = 'CPC';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Contratación (Recomendación Técnica)' WHERE ESTADO_ORDEN_TRABAJO = 'CRT';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Contratación (Revisión Expediente)' WHERE ESTADO_ORDEN_TRABAJO = 'CRE';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Contratación (Visita Técnica)' WHERE ESTADO_ORDEN_TRABAJO = 'CVT';


	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Evaluación Preliminar (Aprobada Jefatura)' WHERE ESTADO_ORDEN_TRABAJO = 'EAJ';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Evaluación Preliminar (Aprobada por Coordinador)' WHERE ESTADO_ORDEN_TRABAJO = 'EAC';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Evaluación Preliminar (Devuelta por Coordinador)' WHERE ESTADO_ORDEN_TRABAJO = 'EDC';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Evaluación Preliminar (Devuelta por Jefatura)' WHERE ESTADO_ORDEN_TRABAJO = 'EDJ';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Evaluación Preliminar (Evaluación)' WHERE ESTADO_ORDEN_TRABAJO = 'EPV';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Evaluación Preliminar (Pendiente)' WHERE ESTADO_ORDEN_TRABAJO = 'EPE';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Evaluación Preliminar (Revisión Coordinador)' WHERE ESTADO_ORDEN_TRABAJO = 'ERC';
	UPDATE OTC_ESTADO_ORDEN_TRABAJO SET DESCRIPCION = 'Evaluación Preliminar (Revisión Jefatura)' WHERE ESTADO_ORDEN_TRABAJO = 'ERJ';

END;