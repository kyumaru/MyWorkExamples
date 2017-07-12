DROP VIEW TC_CAPACITACION_JUAN.V_TCC_CONDICION_LIBRO;

DROP VIEW TC_CAPACITACION_JUAN.V_TCC_CONDICION_LIBROLst;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE VIEW TC_CAPACITACION_JUAN.V_TCC_CONDICION_LIBRO
AS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 4:34:41 p. m.
	Descripcion:		Vista para obtener los registros de la tabla TCC_CONDICION_LIBRO
*/
SELECT
	NVL(CONDICION_LIBRO, '-') AS CONDICION_LIBRO, 
	NVL(DESCRIPCION, '-') AS DESCRIPCION
FROM TCC_CONDICION_LIBRO;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE VIEW TC_CAPACITACION_JUAN.V_TCC_CONDICION_LIBROLst
AS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 4:34:41 p. m.
	Descripcion:		Vista para obtener los registros de la tabla TCC_CONDICION_LIBRO
*/
SELECT
	NVL(CONDICION_LIBRO, '-') AS "CONDICION_LIBRO", 
	NVL(DESCRIPCION, '-') AS "DESCRIPCION"
FROM TCC_CONDICION_LIBRO;

/*------------------------------------------------------------------------------------------------------------------------*/

