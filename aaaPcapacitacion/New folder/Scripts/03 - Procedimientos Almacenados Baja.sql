DROP PROCEDURE TRANSPORTES.prD_STM_SERVICIO;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE TRANSPORTES.prD_STM_SERVICIO
(
	pvn_IdServicio IN STM_SERVICIO.ID_SERVICIO%TYPE := NULL
)
IS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			18/5/2017 2:47:14 p. m.
	Descripcion:		Procedimiento para borrar un registro en la tabla STM_SERVICIO
*/
BEGIN
	DELETE FROM STM_SERVICIO WHERE ID_SERVICIO = pvn_IdServicio;
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[TRANSPORTES.prD_STM_SERVICIO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

