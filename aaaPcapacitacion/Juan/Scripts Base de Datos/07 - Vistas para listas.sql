DROP VIEW TC_CAPACITACION_JUAN.V_TCM_AUTOR;

DROP VIEW TC_CAPACITACION_JUAN.V_TCM_AUTORLst;

DROP VIEW TC_CAPACITACION_JUAN.V_TCM_AUTOR_POR_LIBRO;

DROP VIEW TC_CAPACITACION_JUAN.V_TCM_AUTOR_POR_LIBROLst;

DROP VIEW TC_CAPACITACION_JUAN.V_TCM_LIBRO;

DROP VIEW TC_CAPACITACION_JUAN.V_TCM_LIBROLst;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE VIEW TC_CAPACITACION_JUAN.V_TCM_AUTOR
AS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Vista para obtener los registros de la tabla TCM_AUTOR
*/
SELECT
	NVL(ID_PERSONAL, '-') AS ID_PERSONAL, 
	NVL(NOMBRE, '-') AS NOMBRE, 
	NVL(PRIMER_APELLIDO, '-') AS PRIMER_APELLIDO, 
	NVL(SEGUNDO_APELLIDO, '-') AS SEGUNDO_APELLIDO, 
	NVL(FECHA_HORA_NACIMIENTO, TO_DATE('01-01-1900','DD-MM-YYYY')) AS FECHA_HORA_NACIMIENTO, 
	NVL(ESTADO, '-') AS ESTADO, 
	NVL(TIME_STAMP, TO_DATE('01-01-1900','DD-MM-YYYY')) AS TIME_STAMP, 
	NVL(USUARIO, '-') AS USUARIO
FROM TCM_AUTOR;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE VIEW TC_CAPACITACION_JUAN.V_TCM_AUTORLst
AS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Vista para obtener los registros de la tabla TCM_AUTOR
*/
SELECT
	NVL(ID_PERSONAL, '-') AS "ID_PERSONAL", 
	NVL(NOMBRE, '-') AS "NOMBRE", 
	NVL(PRIMER_APELLIDO, '-') AS "PRIMER_APELLIDO", 
	NVL(SEGUNDO_APELLIDO, '-') AS "SEGUNDO_APELLIDO", 
	NVL(FECHA_HORA_NACIMIENTO, TO_DATE('01-01-1900','DD-MM-YYYY')) AS "FECHA_HORA_NACIMIENTO", 
	NVL(ESTADO, '-') AS "ESTADO", 
	NVL(TIME_STAMP, TO_DATE('01-01-1900','DD-MM-YYYY')) AS "TIME_STAMP", 
	NVL(USUARIO, '-') AS "USUARIO"
FROM TCM_AUTOR;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE VIEW TC_CAPACITACION_JUAN.V_TCM_AUTOR_POR_LIBRO
AS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Vista para obtener los registros de la tabla TCM_AUTOR_POR_LIBRO
*/
SELECT
	NVL(ISBN, '-') AS ISBN, 
	NVL(ID_PERSONAL, '-') AS ID_PERSONAL
FROM TCM_AUTOR_POR_LIBRO;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE VIEW TC_CAPACITACION_JUAN.V_TCM_AUTOR_POR_LIBROLst
AS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Vista para obtener los registros de la tabla TCM_AUTOR_POR_LIBRO
*/
SELECT
	NVL(ISBN, '-') AS "ISBN", 
	NVL(ID_PERSONAL, '-') AS "ID_PERSONAL"
FROM TCM_AUTOR_POR_LIBRO;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE VIEW TC_CAPACITACION_JUAN.V_TCM_LIBRO
AS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Vista para obtener los registros de la tabla TCM_LIBRO
*/
SELECT
	NVL(ISBN, '-') AS ISBN, 
	NVL(CONDICION_LIBRO, '-') AS CONDICION_LIBRO, 
	NVL(TITULO, '-') AS TITULO, 
	NVL(RESUMEN, '-') AS RESUMEN, 
	NVL(TOTAL_PAGINAS, 0) AS TOTAL_PAGINAS, 
	NVL(FECHA_HORA_IMPRESION, TO_DATE('01-01-1900','DD-MM-YYYY')) AS FECHA_HORA_IMPRESION, 
	NVL(TIME_STAMP, TO_DATE('01-01-1900','DD-MM-YYYY')) AS TIME_STAMP, 
	NVL(USUARIO, '-') AS USUARIO
FROM TCM_LIBRO;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE VIEW TC_CAPACITACION_JUAN.V_TCM_LIBROLst
AS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Vista para obtener los registros de la tabla TCM_LIBRO
*/
SELECT
	NVL(ISBN, '-') AS "ISBN", 
	NVL(CONDICION_LIBRO, '-') AS "CONDICION_LIBRO", 
	NVL(TITULO, '-') AS "TITULO", 
	NVL(RESUMEN, '-') AS "RESUMEN", 
	NVL(TOTAL_PAGINAS, 0) AS "TOTAL_PAGINAS", 
	NVL(FECHA_HORA_IMPRESION, TO_DATE('01-01-1900','DD-MM-YYYY')) AS "FECHA_HORA_IMPRESION", 
	NVL(TIME_STAMP, TO_DATE('01-01-1900','DD-MM-YYYY')) AS "TIME_STAMP", 
	NVL(USUARIO, '-') AS "USUARIO", 
	(SELECT DESCRIPCION FROM TCC_CONDICION_LIBRO WHERE TCC_CONDICION_LIBRO.CONDICION_LIBRO = TCM_LIBRO.CONDICION_LIBRO) AS "DESC_CONDICION_LIBRO"
FROM TCM_LIBRO;

/*------------------------------------------------------------------------------------------------------------------------*/

