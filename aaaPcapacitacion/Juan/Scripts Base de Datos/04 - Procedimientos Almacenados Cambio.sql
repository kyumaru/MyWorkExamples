DROP PROCEDURE TC_CAPACITACION_JUAN.prU_TCM_AUTOR;

DROP PROCEDURE TC_CAPACITACION_JUAN.prU_TCM_AUTOR_POR_LIBRO;

DROP PROCEDURE TC_CAPACITACION_JUAN.prU_TCM_LIBRO;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE TC_CAPACITACION_JUAN.prU_TCM_AUTOR
(
	pvc_IdPersonal IN TCM_AUTOR.ID_PERSONAL%TYPE := NULL, 
	pvc_Nombre IN TCM_AUTOR.NOMBRE%TYPE := NULL, 
	pvc_PrimerApellido IN TCM_AUTOR.PRIMER_APELLIDO%TYPE := NULL, 
	pvc_SegundoApellido IN TCM_AUTOR.SEGUNDO_APELLIDO%TYPE := NULL, 
	pvd_FechaHoraNacimiento IN TCM_AUTOR.FECHA_HORA_NACIMIENTO%TYPE := NULL, 
	pvc_Estado IN TCM_AUTOR.ESTADO%TYPE := NULL, 
	pvd_TimeStamp IN TCM_AUTOR.TIME_STAMP%TYPE := NULL, 
	pvc_Usuario IN TCM_AUTOR.USUARIO%TYPE := NULL
)
IS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Procedimiento para modificar un registro en la tabla TCM_AUTOR
*/
BEGIN
	UPDATE TCM_AUTOR SET NOMBRE = pvc_Nombre, PRIMER_APELLIDO = pvc_PrimerApellido, SEGUNDO_APELLIDO = pvc_SegundoApellido, FECHA_HORA_NACIMIENTO = pvd_FechaHoraNacimiento, ESTADO = pvc_Estado, TIME_STAMP = SYSDATE, USUARIO = pvc_Usuario WHERE ID_PERSONAL = pvc_IdPersonal AND TIME_STAMP = pvd_TimeStamp;

	IF (SQL%ROWCOUNT = 0) THEN
		RAISE_APPLICATION_ERROR(-20002, '[TC_CAPACITACION_JUAN.prU_TCM_AUTOR]: No se ha actualizado ningún registro.');
	END IF;
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[TC_CAPACITACION_JUAN.prU_TCM_AUTOR]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE TC_CAPACITACION_JUAN.prU_TCM_AUTOR_POR_LIBRO
(
	pvc_Isbn IN TCM_AUTOR_POR_LIBRO.ISBN%TYPE := NULL, 
	pvc_IdPersonal IN TCM_AUTOR_POR_LIBRO.ID_PERSONAL%TYPE := NULL
)
IS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Procedimiento para modificar un registro en la tabla TCM_AUTOR_POR_LIBRO
*/
BEGIN
	UPDATE TCM_AUTOR_POR_LIBRO SET  WHERE ISBN = pvc_Isbn AND ID_PERSONAL = pvc_IdPersonal;

	IF (SQL%ROWCOUNT = 0) THEN
		RAISE_APPLICATION_ERROR(-20002, '[TC_CAPACITACION_JUAN.prU_TCM_AUTOR_POR_LIBRO]: No se ha actualizado ningún registro.');
	END IF;
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[TC_CAPACITACION_JUAN.prU_TCM_AUTOR_POR_LIBRO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE TC_CAPACITACION_JUAN.prU_TCM_LIBRO
(
	pvc_Isbn IN TCM_LIBRO.ISBN%TYPE := NULL, 
	pvc_CondicionLibro IN TCM_LIBRO.CONDICION_LIBRO%TYPE := NULL, 
	pvc_Titulo IN TCM_LIBRO.TITULO%TYPE := NULL, 
	pvc_Resumen IN TCM_LIBRO.RESUMEN%TYPE := NULL, 
	pvn_TotalPaginas IN TCM_LIBRO.TOTAL_PAGINAS%TYPE := NULL, 
	pvd_FechaHoraImpresion IN TCM_LIBRO.FECHA_HORA_IMPRESION%TYPE := NULL, 
	pvd_TimeStamp IN TCM_LIBRO.TIME_STAMP%TYPE := NULL, 
	pvc_Usuario IN TCM_LIBRO.USUARIO%TYPE := NULL
)
IS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 1:54:32 p. m.
	Descripcion:		Procedimiento para modificar un registro en la tabla TCM_LIBRO
*/
BEGIN
	UPDATE TCM_LIBRO SET CONDICION_LIBRO = pvc_CondicionLibro, TITULO = pvc_Titulo, RESUMEN = pvc_Resumen, TOTAL_PAGINAS = pvn_TotalPaginas, FECHA_HORA_IMPRESION = pvd_FechaHoraImpresion, TIME_STAMP = SYSDATE, USUARIO = pvc_Usuario WHERE ISBN = pvc_Isbn AND TIME_STAMP = pvd_TimeStamp;

	IF (SQL%ROWCOUNT = 0) THEN
		RAISE_APPLICATION_ERROR(-20002, '[TC_CAPACITACION_JUAN.prU_TCM_LIBRO]: No se ha actualizado ningún registro.');
	END IF;
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[TC_CAPACITACION_JUAN.prU_TCM_LIBRO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

