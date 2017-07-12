DROP PROCEDURE TC_CAPACITACION_JUAN.prI_TCM_AUTOR;

DROP PROCEDURE TC_CAPACITACION_JUAN.prI_TCM_AUTOR_POR_LIBRO;

DROP PROCEDURE TC_CAPACITACION_JUAN.prI_TCM_LIBRO;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE TC_CAPACITACION_JUAN.prI_TCM_AUTOR
(
	pvc_IdPersonal IN TCM_AUTOR.ID_PERSONAL%TYPE := NULL, 
	pvc_Nombre IN TCM_AUTOR.NOMBRE%TYPE := NULL, 
	pvc_PrimerApellido IN TCM_AUTOR.PRIMER_APELLIDO%TYPE := NULL, 
	pvc_SegundoApellido IN TCM_AUTOR.SEGUNDO_APELLIDO%TYPE := NULL, 
	pvd_FechaHoraNacimiento IN TCM_AUTOR.FECHA_HORA_NACIMIENTO%TYPE := NULL, 
	pvc_Estado IN TCM_AUTOR.ESTADO%TYPE := NULL, 
	pvc_Usuario IN TCM_AUTOR.USUARIO%TYPE := NULL
)
IS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Procedimiento para agregar un registro en la tabla TCM_AUTOR
*/
BEGIN
	INSERT INTO TCM_AUTOR (ID_PERSONAL, NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO, FECHA_HORA_NACIMIENTO, ESTADO, USUARIO) VALUES (pvc_IdPersonal, pvc_Nombre, pvc_PrimerApellido, pvc_SegundoApellido, pvd_FechaHoraNacimiento, pvc_Estado, pvc_Usuario);
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[TC_CAPACITACION_JUAN.prI_TCM_AUTOR]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE TC_CAPACITACION_JUAN.prI_TCM_AUTOR_POR_LIBRO
(
	pvc_Isbn IN TCM_AUTOR_POR_LIBRO.ISBN%TYPE := NULL, 
	pvc_IdPersonal IN TCM_AUTOR_POR_LIBRO.ID_PERSONAL%TYPE := NULL
)
IS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Procedimiento para agregar un registro en la tabla TCM_AUTOR_POR_LIBRO
*/
BEGIN
	INSERT INTO TCM_AUTOR_POR_LIBRO (ISBN, ID_PERSONAL) VALUES (pvc_Isbn, pvc_IdPersonal);
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[TC_CAPACITACION_JUAN.prI_TCM_AUTOR_POR_LIBRO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE TC_CAPACITACION_JUAN.prI_TCM_LIBRO
(
	pvc_Isbn IN TCM_LIBRO.ISBN%TYPE := NULL, 
	pvc_CondicionLibro IN TCM_LIBRO.CONDICION_LIBRO%TYPE := NULL, 
	pvc_Titulo IN TCM_LIBRO.TITULO%TYPE := NULL, 
	pvc_Resumen IN TCM_LIBRO.RESUMEN%TYPE := NULL, 
	pvn_TotalPaginas IN TCM_LIBRO.TOTAL_PAGINAS%TYPE := NULL, 
	pvd_FechaHoraImpresion IN TCM_LIBRO.FECHA_HORA_IMPRESION%TYPE := NULL, 
	pvc_Usuario IN TCM_LIBRO.USUARIO%TYPE := NULL
)
IS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Procedimiento para agregar un registro en la tabla TCM_LIBRO
*/
BEGIN
	INSERT INTO TCM_LIBRO (ISBN, CONDICION_LIBRO, TITULO, RESUMEN, TOTAL_PAGINAS, FECHA_HORA_IMPRESION, USUARIO) VALUES (pvc_Isbn, pvc_CondicionLibro, pvc_Titulo, pvc_Resumen, pvn_TotalPaginas, pvd_FechaHoraImpresion, pvc_Usuario);
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[TC_CAPACITACION_JUAN.prI_TCM_LIBRO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

