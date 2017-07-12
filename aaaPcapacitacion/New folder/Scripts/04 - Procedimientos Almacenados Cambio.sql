DROP PROCEDURE TRANSPORTES.prU_STM_SERVICIO;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE TRANSPORTES.prU_STM_SERVICIO
(
	pvn_IdServicio IN STM_SERVICIO.ID_SERVICIO%TYPE := NULL, 
	pvc_Descripcion IN STM_SERVICIO.DESCRIPCION%TYPE := NULL, 
	pvc_TipoServicio IN STM_SERVICIO.TIPO_SERVICIO%TYPE := NULL, 
	pvc_Estado IN STM_SERVICIO.ESTADO%TYPE := NULL, 
	pvc_UsuarioCrea IN STM_SERVICIO.USUARIO_CREA%TYPE := NULL, 
	pvd_FechaCrea IN STM_SERVICIO.FECHA_CREA%TYPE := NULL, 
	pvd_TimeStamp IN STM_SERVICIO.TIME_STAMP%TYPE := NULL
)
IS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			18/5/2017 2:47:14 p. m.
	Descripcion:		Procedimiento para modificar un registro en la tabla STM_SERVICIO
*/
BEGIN
	UPDATE STM_SERVICIO SET DESCRIPCION = pvc_Descripcion, TIPO_SERVICIO = pvc_TipoServicio, ESTADO = pvc_Estado, USUARIO_CREA = pvc_UsuarioCrea, FECHA_CREA = pvd_FechaCrea, TIME_STAMP = SYSDATE WHERE ID_SERVICIO = pvn_IdServicio AND TIME_STAMP = pvd_TimeStamp;

	IF (SQL%ROWCOUNT = 0) THEN
		RAISE_APPLICATION_ERROR(-20002, '[TRANSPORTES.prU_STM_SERVICIO]: No se ha actualizado ningún registro.');
	END IF;
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[TRANSPORTES.prU_STM_SERVICIO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

