DROP PROCEDURE TC_CAPACITACION_JUAN.prI_TCC_CONDICION_LIBRO;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE TC_CAPACITACION_JUAN.prI_TCC_CONDICION_LIBRO
(
	pvc_CondicionLibro IN TCC_CONDICION_LIBRO.CONDICION_LIBRO%TYPE := NULL, 
	pvc_Descripcion IN TCC_CONDICION_LIBRO.DESCRIPCION%TYPE := NULL
)
IS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			4/5/2017 4:34:41 p. m.
	Descripcion:		Procedimiento para agregar un registro en la tabla TCC_CONDICION_LIBRO
*/
BEGIN
	INSERT INTO TCC_CONDICION_LIBRO (CONDICION_LIBRO, DESCRIPCION) VALUES (pvc_CondicionLibro, pvc_Descripcion);
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[TC_CAPACITACION_JUAN.prI_TCC_CONDICION_LIBRO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

