DROP VIEW TRANSPORTES.V_STM_SERVICIO;

DROP VIEW TRANSPORTES.V_STM_SERVICIOLst;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE VIEW TRANSPORTES.V_STM_SERVICIO
AS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			18/5/2017 2:47:14 p. m.
	Descripcion:		Vista para obtener los registros de la tabla STM_SERVICIO
*/
SELECT
	NVL(ID_SERVICIO, 0) AS ID_SERVICIO, 
	NVL(DESCRIPCION, '-') AS DESCRIPCION, 
	NVL(TIPO_SERVICIO, '-') AS TIPO_SERVICIO, 
	NVL(ESTADO, '-') AS ESTADO, 
	NVL(USUARIO_CREA, '-') AS USUARIO_CREA, 
	NVL(FECHA_CREA, TO_DATE('01-01-1900','DD-MM-YYYY')) AS FECHA_CREA, 
	NVL(TIME_STAMP, TO_DATE('01-01-1900','DD-MM-YYYY')) AS TIME_STAMP
FROM STM_SERVICIO;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE VIEW TRANSPORTES.V_STM_SERVICIOLst
AS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			18/5/2017 2:47:14 p. m.
	Descripcion:		Vista para obtener los registros de la tabla STM_SERVICIO
*/
SELECT
	NVL(ID_SERVICIO, 0) AS "ID_SERVICIO", 
	NVL(DESCRIPCION, '-') AS "DESCRIPCION", 
	NVL(TIPO_SERVICIO, '-') AS "TIPO_SERVICIO", 
	NVL(ESTADO, '-') AS "ESTADO", 
	NVL(USUARIO_CREA, '-') AS "USUARIO_CREA", 
	NVL(FECHA_CREA, TO_DATE('01-01-1900','DD-MM-YYYY')) AS "FECHA_CREA", 
	NVL(TIME_STAMP, TO_DATE('01-01-1900','DD-MM-YYYY')) AS "TIME_STAMP"
FROM STM_SERVICIO;

/*------------------------------------------------------------------------------------------------------------------------*/

