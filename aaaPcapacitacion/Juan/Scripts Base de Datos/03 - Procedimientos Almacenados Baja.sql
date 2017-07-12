DROP PROCEDURE TC_CAPACITACION_JUAN.prD_TCM_AUTOR;

DROP PROCEDURE TC_CAPACITACION_JUAN.prD_TCM_AUTOR_POR_LIBRO;

DROP PROCEDURE TC_CAPACITACION_JUAN.prD_TCM_LIBRO;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE TC_CAPACITACION_JUAN.prD_TCM_AUTOR
(
	pvc_IdPersonal IN TCM_AUTOR.ID_PERSONAL%TYPE := NULL
)
IS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Procedimiento para borrar un registro en la tabla TCM_AUTOR
*/
BEGIN
	DELETE FROM TCM_AUTOR WHERE ID_PERSONAL = pvc_IdPersonal;
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[TC_CAPACITACION_JUAN.prD_TCM_AUTOR]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE TC_CAPACITACION_JUAN.prD_TCM_AUTOR_POR_LIBRO
(
	pvc_Isbn IN TCM_AUTOR_POR_LIBRO.ISBN%TYPE := NULL, 
	pvc_IdPersonal IN TCM_AUTOR_POR_LIBRO.ID_PERSONAL%TYPE := NULL
)
IS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Procedimiento para borrar un registro en la tabla TCM_AUTOR_POR_LIBRO
*/
BEGIN
	DELETE FROM TCM_AUTOR_POR_LIBRO WHERE ISBN = pvc_Isbn AND ID_PERSONAL = pvc_IdPersonal;
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[TC_CAPACITACION_JUAN.prD_TCM_AUTOR_POR_LIBRO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE TC_CAPACITACION_JUAN.prD_TCM_LIBRO
(
	pvc_Isbn IN TCM_LIBRO.ISBN%TYPE := NULL
)
IS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Procedimiento para borrar un registro en la tabla TCM_LIBRO
*/
BEGIN
	DELETE FROM TCM_LIBRO WHERE ISBN = pvc_Isbn;
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[TC_CAPACITACION_JUAN.prD_TCM_LIBRO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

