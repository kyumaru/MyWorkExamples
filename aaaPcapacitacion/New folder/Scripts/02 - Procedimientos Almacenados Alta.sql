DROP PROCEDURE TRANSPORTES.prI_STM_SERVICIO;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE TRANSPORTES.prI_STM_SERVICIO
(
	prn_IdServicio OUT STM_SERVICIO.ID_SERVICIO%TYPE, 
	pvc_Descripcion IN STM_SERVICIO.DESCRIPCION%TYPE := NULL, 
	pvc_TipoServicio IN STM_SERVICIO.TIPO_SERVICIO%TYPE := NULL, 
	pvc_Estado IN STM_SERVICIO.ESTADO%TYPE := NULL, 
	pvc_UsuarioCrea IN STM_SERVICIO.USUARIO_CREA%TYPE := NULL, 
	pvd_FechaCrea IN STM_SERVICIO.FECHA_CREA%TYPE := NULL
)
IS
/*
	Autor:			Generador de Código basado en objetos Oracle
	Fecha:			18/5/2017 2:47:14 p. m.
	Descripcion:		Procedimiento para agregar un registro en la tabla STM_SERVICIO
*/
BEGIN
	INSERT INTO STM_SERVICIO (ID_SERVICIO, DESCRIPCION, TIPO_SERVICIO, ESTADO, USUARIO_CREA, FECHA_CREA) VALUES (SQ_ID_SERVICIO.NEXTVAL, pvc_Descripcion, pvc_TipoServicio, pvc_Estado, pvc_UsuarioCrea, pvd_FechaCrea) RETURNING ID_SERVICIO INTO prn_IdServicio;
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[TRANSPORTES.prI_STM_SERVICIO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

