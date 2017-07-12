
DROP PROCEDURE Reset_Sequence;

DROP PROCEDURE Reset_Sequence_to_Data;

DROP PROCEDURE PR_OT_SUBIR_ESPACIO;

DROP PROCEDURE PR_OT_BAJAR_ESPACIO;

DROP PROCEDURE PR_OT_SUBIR_SUBCOMPONENTE;

DROP PROCEDURE PR_OT_BAJAR_SUBCOMPONENTE;

DROP PROCEDURE PR_OT_SUBIR_REQUERIMIENTO;

DROP PROCEDURE PR_OT_BAJAR_REQUERIMIENTO;

DROP PROCEDURE PR_OT_PASE_HISTORICO_UNICO;

DROP PROCEDURE PR_OT_PASE_HISTORICO_GENERAL;

DROP PROCEDURE PR_OT_INHABILITAR_REQ;

DROP PROCEDURE PR_OT_BAJAR_RUBRO;

DROP PROCEDURE PR_OT_SUBIR_RUBRO;

DROP PROCEDURE PR_OT_ASIG_OT_DISENIO;

DROP PROCEDURE PR_OT_ASIG_OT_MANTE;

DROP PROCEDURE PR_OT_BORRAR_PRE_ORDEN;

DROP PROCEDURE PR_OT_MIGRAR_ORDEN_TRABAJO;

/*------------------------------------------------------------------------------------------------------------------------*/
CREATE OR REPLACE PROCEDURE Reset_Sequence(p_seq_name in varchar2, p_val in number default 0) 
IS  
    /*
    Autor:            Patricia Conejo Altamirano
    Fecha:            28-09-11
    Descripcion:    Procedimiento para alterar una secuencia y definir el siguiente valor.
    */
    l_current number := 0;   
    l_difference number := 0;   
    l_minvalue user_sequences.min_value%type := 0;  
BEGIN    
    select min_value into l_minvalue   
    from user_sequences   
    where sequence_name = p_seq_name;    
    
    execute immediate   'select ' || p_seq_name || '.nextval from dual' INTO l_current;   

     if p_Val < l_minvalue then     
         l_difference := l_minvalue - l_current;   
    else     
        l_difference := p_Val - l_current;   
    end if;    
    
    if l_difference = 0 then     
         return;   
    end if;    

    execute immediate 'alter sequence ' || p_seq_name || ' increment by ' || l_difference || ' minvalue ' || l_minvalue;    
    execute immediate 'select ' || p_seq_name || '.nextval from dual' INTO l_difference;    
    execute immediate 'alter sequence ' || p_seq_name || ' increment by 1 minvalue ' || l_minvalue; 

END Reset_Sequence; 
/*-------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE Reset_Sequence_to_Data(p_TableName varchar2, p_FieldName VARCHAR2, p_seq_name varchar2) 
is  
    /*
    Autor:            Patricia Conejo Altamirano
    Fecha:            28-09-11
    Descripcion:    Procedimiento para alterar una secuencia de una tabla al máximo ID generado para esa tabla.
    */
    l_MaxUsed NUMBER; 
BEGIN    
    execute immediate 'select coalesce(max(' || p_FieldName || '),0) from '|| p_TableName into l_MaxUsed; 
    Reset_Sequence( p_seq_name, l_MaxUsed );  
END Reset_Sequence_to_Data; 
/*-------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE PR_OT_SUBIR_ESPACIO(pvn_IdEspacio number)
IS

vln_Posicion NUMBER;
vln_PosicionAnterior NUMBER;
vln_IdPosicionAnterior NUMBER;

BEGIN
    /*
        Autor: César Bermúdez García
        Fecha: 11/12/2015 10:10 a.m
        Descripcion: Procedimiento para subir el orden de un registro en la tabla OTM_ESPACIO    
		
		CONTROL DE CAMBIOS
		
		Autor: CARLOS GOMEZ ONDOY
        Fecha: 05/01/2016
        Descripcion: MODIFICACION DE EXCEPTION
		
    */    
        
    /* Obtener el numero de orden de la posicion a subir */
    SELECT ORDEN INTO vln_Posicion  FROM OTM_ESPACIO WHERE ID_ESPACIO = pvn_IdEspacio;

    /* Obtener el numero de orden de la posicion a bajar*/
    SELECT MAX(ORDEN) INTO vln_PosicionAnterior FROM OTM_ESPACIO WHERE ORDEN < vln_Posicion ORDER BY ORDEN DESC;
    
    /*Obtener el id de la posicion a bajar*/
    SELECT ID_ESPACIO INTO vln_IdPosicionAnterior FROM OTM_ESPACIO WHERE ORDEN = vln_PosicionAnterior;

    /*Actualizar Posiciones*/
  
    UPDATE OTM_ESPACIO SET ORDEN = vln_PosicionAnterior WHERE ID_ESPACIO = pvn_IdEspacio;
        
    UPDATE OTM_ESPACIO SET ORDEN=vln_Posicion WHERE ID_ESPACIO = vln_IdPosicionAnterior;
  
         IF (SQL%ROWCOUNT = 0) THEN
            RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_SUBIR_ESPACIO]: No se ha actualizado ningún registro.');
        END IF;
        EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_SUBIR_ESPACIO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
            ROLLBACK;

END PR_OT_SUBIR_ESPACIO;

/*------------------------------------------------------------------------------------------------------------------------*/


CREATE OR REPLACE PROCEDURE PR_OT_BAJAR_ESPACIO(pvn_IdEspacio number)
IS

vln_Posicion NUMBER;
vln_PosicionSiguiente NUMBER;
vln_IdPosicionSiguiente NUMBER;

BEGIN
    /*
        Autor: César Bermúdez García
        Fecha: 11/12/2015 10:10 a.m
        Descripcion: Procedimiento para bajar el orden de un registro en la tabla OTM_ESPACIO    
    */    
    
    
    /* Obtener el numero de orden de la posicion a bajar */
    SELECT ORDEN INTO vln_Posicion  FROM OTM_ESPACIO WHERE ID_ESPACIO = pvn_IdEspacio;

    /* Obtener el numero de orden de la posicion a subir*/
    SELECT MIN(ORDEN) INTO vln_PosicionSiguiente FROM OTM_ESPACIO WHERE ORDEN > vln_Posicion ORDER BY ORDEN DESC;
    
    /*Obtener el id de la posicion a subir*/
    SELECT ID_ESPACIO INTO vln_IdPosicionSiguiente FROM OTM_ESPACIO WHERE ORDEN = vln_PosicionSiguiente;

    /*Actualizar Posiciones*/
    BEGIN

        UPDATE OTM_ESPACIO SET ORDEN = vln_PosicionSiguiente WHERE ID_ESPACIO = pvn_IdEspacio;
      
             IF (SQL%ROWCOUNT = 0) THEN
                RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_BAJAR_ESPACIO]: No se ha actualizado ningún registro.');
            END IF;
            EXCEPTION
            WHEN OTHERS THEN
                RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_BAJAR_ESPACIO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
                ROLLBACK;
                
    END;
        
    BEGIN
    
        UPDATE OTM_ESPACIO SET ORDEN=vln_Posicion WHERE ID_ESPACIO = vln_IdPosicionSiguiente;
      
             IF (SQL%ROWCOUNT = 0) THEN
                RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_BAJAR_ESPACIO]: No se ha actualizado ningún registro.');
            END IF;
            EXCEPTION
            WHEN OTHERS THEN
                RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_BAJAR_ESPACIO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
                ROLLBACK;
    END;
            

END;

/*-------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE PR_OT_SUBIR_SUBCOMPONENTE(pvn_IdEspacio number, pvn_IdSubcomponente number)
IS

vln_Posicion NUMBER;
vln_PosicionAnterior NUMBER;
vln_IdPosicionAnterior NUMBER;

BEGIN
    /*
        Autor: César Bermúdez García
        Fecha: 13/11/2015 10:10 a.m
        Descripcion: Procedimiento para subir el orden de un registro en la tabla OTM_SUBCOMPONENTE    
    */    
    
    
    /* Obtener el numero de orden de la posicion a subir */
    SELECT ORDEN INTO vln_Posicion  FROM OTM_SUBCOMPONENTE WHERE ID_SUBCOMPONENTE = pvn_IdSubcomponente;

    /* Obtener el numero de orden de la posicion a bajar*/
    SELECT MAX(ORDEN) INTO vln_PosicionAnterior FROM OTM_SUBCOMPONENTE WHERE ORDEN < vln_Posicion AND ID_ESPACIO = pvn_IdEspacio ORDER BY ORDEN DESC;
    
    /*Obtener el id de la posicion a bajar*/
    SELECT ID_SUBCOMPONENTE INTO vln_IdPosicionAnterior FROM OTM_SUBCOMPONENTE WHERE ORDEN = vln_PosicionAnterior AND ID_ESPACIO = pvn_IdEspacio;

    /*Actualizar Posiciones*/
  
    UPDATE OTM_SUBCOMPONENTE SET ORDEN = vln_PosicionAnterior WHERE ID_SUBCOMPONENTE = pvn_IdSubcomponente;
        
    UPDATE OTM_SUBCOMPONENTE SET ORDEN = vln_Posicion WHERE ID_SUBCOMPONENTE = vln_IdPosicionAnterior;
  
         IF (SQL%ROWCOUNT = 0) THEN
            RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_SUBIR_SUBCOMPONENTE]: No se ha actualizado ningún registro.');
        END IF;
        EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_SUBIR_SUBCOMPONENTE]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
            ROLLBACK;

END PR_OT_SUBIR_SUBCOMPONENTE;

/*------------------------------------------------------------------------------------------------------------------------*/


CREATE OR REPLACE PROCEDURE PR_OT_BAJAR_SUBCOMPONENTE(pvn_IdEspacio number, pvn_IdSubcomponente number)
IS

vln_Posicion NUMBER;
vln_PosicionSiguiente NUMBER;
vln_IdPosicionSiguiente NUMBER;

BEGIN
    /*
        Autor: César Bermúdez García
        Fecha: 13/11/2015 10:10 a.m
        Descripcion: Procedimiento para bajar el orden de un registro en la tabla OTM_SUBCOMPONENTE    
    */    
    
    
    /* Obtener el numero de orden de la posicion a bajar */
    SELECT ORDEN INTO vln_Posicion  FROM OTM_SUBCOMPONENTE WHERE ID_SUBCOMPONENTE = pvn_IdSubcomponente;

    /* Obtener el numero de orden de la posicion a subir*/
    SELECT MIN(ORDEN) INTO vln_PosicionSiguiente FROM OTM_SUBCOMPONENTE WHERE ORDEN > vln_Posicion AND ID_ESPACIO = pvn_IdEspacio ORDER BY ORDEN DESC;
    
    /*Obtener el id de la posicion a subir*/
    SELECT ID_SUBCOMPONENTE INTO vln_IdPosicionSiguiente FROM OTM_SUBCOMPONENTE WHERE ORDEN = vln_PosicionSiguiente AND ID_ESPACIO = pvn_IdEspacio;

    /*Actualizar Posiciones*/

    UPDATE OTM_SUBCOMPONENTE SET ORDEN = vln_PosicionSiguiente WHERE ID_SUBCOMPONENTE = pvn_IdSubcomponente;
          
    UPDATE OTM_SUBCOMPONENTE SET ORDEN = vln_Posicion WHERE ID_SUBCOMPONENTE = vln_IdPosicionSiguiente;
      
     IF (SQL%ROWCOUNT = 0) THEN
        RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_BAJAR_SUBCOMPONENTE]: No se ha actualizado ningún registro.');
    END IF;
    EXCEPTION
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_BAJAR_SUBCOMPONENTE]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
        ROLLBACK;
              

END PR_OT_BAJAR_SUBCOMPONENTE;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE PR_OT_SUBIR_REQUERIMIENTO(pvn_IdRequerimiento number)
IS

vln_Posicion NUMBER;
vln_PosicionAnterior NUMBER;
vln_IdPosicionAnterior NUMBER;
vln_Nivel NUMBER;

BEGIN
    /*
        Autor: César Bermúdez García
        Fecha: 25/11/2015 9:04 a.m
        Descripcion: Procedimiento para subir el orden de un registro en la tabla OTM_REQUERIMIENTO    
    */    
    
    
    /* Obtener el numero de orden de la posicion a subir */
    SELECT ORDEN INTO vln_Posicion  FROM OTM_REQUERIMIENTO WHERE ID_REQUERIMIENTO = pvn_IdRequerimiento;

    /*Obtener el nivel en el que se está trabajando actualmente */
    SELECT NIVEL INTO vln_Nivel FROM OTM_REQUERIMIENTO WHERE  ID_REQUERIMIENTO = pvn_IdRequerimiento;
    
    /* Obtener el numero de orden de la posicion a bajar*/
    SELECT MAX(ORDEN) INTO vln_PosicionAnterior FROM OTM_REQUERIMIENTO WHERE ORDEN < vln_Posicion ORDER BY ORDEN DESC;
    
    /*Obtener el id de la posicion a bajar*/
    SELECT ID_REQUERIMIENTO INTO vln_IdPosicionAnterior FROM OTM_REQUERIMIENTO WHERE ORDEN = vln_PosicionAnterior AND NIVEL = vln_Nivel;

    /*Actualizar Posiciones*/
  
    UPDATE OTM_REQUERIMIENTO SET ORDEN = vln_PosicionAnterior WHERE ID_REQUERIMIENTO = pvn_IdRequerimiento;
        
    UPDATE OTM_REQUERIMIENTO SET ORDEN=vln_Posicion WHERE ID_REQUERIMIENTO = vln_IdPosicionAnterior;
  
         IF (SQL%ROWCOUNT = 0) THEN
            RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_SUBIR_REQUERIMIENTO]: No se ha actualizado ningún registro.');
        END IF;
        EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_SUBIR_REQUERIMIENTO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
            ROLLBACK;

END PR_OT_SUBIR_REQUERIMIENTO;
/

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE PR_OT_BAJAR_REQUERIMIENTO(pvn_IdRequerimiento number)
IS

vln_Posicion NUMBER;
vln_PosicionSiguiente NUMBER;
vln_IdPosicionSiguiente NUMBER;
vln_Nivel NUMBER;

BEGIN
    /*
        Autor: César Bermúdez García
        Fecha: 25/11/2015 9:04 a.m
        Descripcion: Procedimiento para bajar el orden de un registro en la tabla OTM_REQUERIMIENTO    
    */    
    
    
    /* Obtener el numero de orden de la posicion a bajar */
    SELECT ORDEN INTO vln_Posicion  FROM OTM_REQUERIMIENTO WHERE ID_REQUERIMIENTO = pvn_IdRequerimiento;

    /* Obtener el numero de orden de la posicion a subir*/
    SELECT MIN(ORDEN) INTO vln_PosicionSiguiente FROM OTM_REQUERIMIENTO WHERE ORDEN > vln_Posicion ORDER BY ORDEN DESC;
	
	/*Obtener el nivel en el que se está trabajando actualmente */
    SELECT NIVEL INTO vln_Nivel FROM OTM_REQUERIMIENTO WHERE  ID_REQUERIMIENTO = pvn_IdRequerimiento;
    
    /*Obtener el id de la posicion a subir*/
    SELECT ID_REQUERIMIENTO INTO vln_IdPosicionSiguiente FROM OTM_REQUERIMIENTO WHERE ORDEN = vln_PosicionSiguiente AND NIVEL = vln_Nivel;

    /*Actualizar Posiciones*/

    UPDATE OTM_REQUERIMIENTO SET ORDEN = vln_PosicionSiguiente WHERE ID_REQUERIMIENTO = pvn_IdRequerimiento;
   
    UPDATE OTM_REQUERIMIENTO SET ORDEN=vln_Posicion WHERE ID_REQUERIMIENTO = vln_IdPosicionSiguiente;
  
         IF (SQL%ROWCOUNT = 0) THEN
            RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_BAJAR_REQUERIMIENTO]: No se ha actualizado ningún registro.');
        END IF;
        EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_BAJAR_REQUERIMIENTO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
            ROLLBACK;
              

END PR_OT_BAJAR_REQUERIMIENTO;
/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE PR_OT_PASE_HISTORICO_UNICO
(
	pvn_IdUbicacion IN OTT_ORDEN_TRABAJO.ID_UBICACION%TYPE := NULL, 
	pvc_IdOrdenTrabajo IN OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO%TYPE := NULL
)
IS
/*
	Autor: Carlos Gómez Ondoy
	Fecha: 01/12/2015
	Descripcion: Procedimiento para trasladar una orden y todas sus relaciones de las tablas transccionales a las tablas historicas
*/

vln_CantidadEnTramite NUMBER;

BEGIN
	
   /*ETR = EN Trámite*/	
    SELECT COUNT(*) INTO vln_CantidadEnTramite FROM V_OTT_ORDEN_TRABAJOLst WHERE DESC_CONDICION_ESTADO = 'ETR' 
	AND ((ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo) OR (ID_UBICACION_MADRE = pvn_IdUbicacion AND ID_ORDEN_TRABAJO_MADRE = pvc_IdOrdenTrabajo));

	IF vln_CantidadEnTramite = 0 THEN
				
		INSERT INTO OTH_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO, ID_UBICACION_MADRE, ID_ORDEN_TRABAJO_MADRE, ANNO, CONSECUTIVO, CONSECUTIVO_HIJA, TIPO_ORDEN_TRABAJO, ESTADO_ORDEN_TRABAJO, NUM_EMPLEADO, ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD, ID_LUGAR_TRABAJO, ID_SECTOR_TALLER, FECHA_HORA_SOLICITA, COD_UNIDAD_SIRH, NOMBRE_PERSONA_CONTACTO, TELEFONO, SENNAS_EXACTAS, DESCRIPCION_TRABAJO, NUMERO_ORDEN, INCLUIDA_EN_RECEPCION, PARENTESCO, ID_MOTIVO_RECHAZO, ID_UBICACION_ORIGEN, USUARIO)
		SELECT ID_UBICACION, ID_ORDEN_TRABAJO, ID_UBICACION_MADRE, ID_ORDEN_TRABAJO_MADRE, ANNO, CONSECUTIVO, CONSECUTIVO_HIJA, TIPO_ORDEN_TRABAJO, ESTADO_ORDEN_TRABAJO, NUM_EMPLEADO, ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD, ID_LUGAR_TRABAJO, ID_SECTOR_TALLER, FECHA_HORA_SOLICITA, COD_UNIDAD_SIRH, NOMBRE_PERSONA_CONTACTO, TELEFONO, SENNAS_EXACTAS, DESCRIPCION_TRABAJO, NUMERO_ORDEN, INCLUIDA_EN_RECEPCION, PARENTESCO, ID_MOTIVO_RECHAZO, ID_UBICACION_ORIGEN, USUARIO FROM
		OTT_ORDEN_TRABAJO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;
		
		INSERT INTO OTH_ADJUNTO_ORDEN_TRABAJO (ID_ADJUNTO_ORDEN_TRABAJO, ID_UBICACION, ID_ORDEN_TRABAJO, NOMBRE_ARCHIVO, ARCHIVO, USUARIO)
		SELECT ID_ADJUNTO_ORDEN_TRABAJO, ID_UBICACION, ID_ORDEN_TRABAJO, NOMBRE_ARCHIVO, ARCHIVO, USUARIO FROM 
		OTT_ADJUNTO_ORDEN_TRABAJO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;
		
		INSERT INTO OTH_TRAZABILIDAD_PROCESO (ID_TRAZABILIDAD_PROCESO, ID_MOTIVO_RECHAZO, ID_UBICACION, ID_ORDEN_TRABAJO, ESTADO_ORDEN_TRABAJO, NUM_EMPLEADO_EJECUTA, FECHA_HORA_EJECUCION, OBSERVACIONES, OBSERVACIONES_INTERNAS, USUARIO)
		SELECT ID_TRAZABILIDAD_PROCESO, ID_MOTIVO_RECHAZO, ID_UBICACION, ID_ORDEN_TRABAJO, ESTADO_ORDEN_TRABAJO, NUM_EMPLEADO_EJECUTA, FECHA_HORA_EJECUCION, OBSERVACIONES, OBSERVACIONES_INTERNAS, USUARIO FROM 
		OTT_TRAZABILIDAD_PROCESO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;
		
		INSERT INTO OTH_REVISION_ORDEN_TRABAJ (ID_REVISION_ORDEN_TRABAJ, ID_UBICACION, ID_ORDEN_TRABAJO, OBSERVACIONES, ESTADO, USUARIO)
		SELECT ID_REVISION_ORDEN_TRABAJ, ID_UBICACION, ID_ORDEN_TRABAJO, OBSERVACIONES, ESTADO, USUARIO FROM 
		OTT_REVISION_ORDEN_TRABAJ WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;
		
		INSERT INTO OTH_FICHA_TECNICA_GENERAL (ID_UBICACION, ID_ORDEN_TRABAJO, CONSERVA_MOBILIARIO, REQUIERE_NUEVO_MOBILIARIO, OTROS_MOBILIARIO, OTRO_TIPO_REQUERIMIENTO, NOMBRE_ARCHIVO, ARCHIVO, CUENTA_CON_ALARMA, REQUIERE_ALARMA, USUARIO)
		SELECT ID_UBICACION, ID_ORDEN_TRABAJO, CONSERVA_MOBILIARIO, REQUIERE_NUEVO_MOBILIARIO, OTROS_MOBILIARIO, OTRO_TIPO_REQUERIMIENTO, NOMBRE_ARCHIVO, ARCHIVO, CUENTA_CON_ALARMA, REQUIERE_ALARMA, USUARIO FROM
		OTT_FICHA_TECNICA_GENERAL WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;
		
		INSERT INTO OTH_FICHA_TECNICA_ESPACIO (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO)
		SELECT ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO FROM 
		OTT_FICHA_TECNICA_ESPACIO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;
		
		INSERT INTO OTH_FICHA_TECNICA_SUBCOMP (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE)
		SELECT ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE FROM 
		OTT_FICHA_TECNICA_SUBCOMP WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;
		
		INSERT INTO OTH_FICHA_TECNICA_DETALLE (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE, ID_REQUERIMIENTO, VALOR, USUARIO)
		SELECT ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE, ID_REQUERIMIENTO, VALOR, USUARIO FROM
		OTT_FICHA_TECNICA_DETALLE WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;
				
		DELETE FROM OTT_FICHA_TECNICA_DETALLE WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;	
		
		DELETE FROM OTT_FICHA_TECNICA_SUBCOMP WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;	
		
		DELETE FROM OTT_FICHA_TECNICA_ESPACIO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;	
		
		DELETE FROM OTT_FICHA_TECNICA_GENERAL WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;	
		
		DELETE FROM OTT_REVISION_ORDEN_TRABAJ WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;	
		
		DELETE FROM OTT_TRAZABILIDAD_PROCESO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;
		
		DELETE FROM OTT_ADJUNTO_ORDEN_TRABAJO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;
		
		DELETE FROM OTT_ORDEN_TRABAJO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;
		
	END IF;
	
	IF (SQL%ROWCOUNT = 0) THEN
		RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_PASE_HISTORICO_UNICO]: No se ha actualizado ningún registro.');
	END IF;
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_PASE_HISTORICO_UNICO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/


CREATE OR REPLACE PROCEDURE PR_OT_PASE_HISTORICO_GENERAL
IS
/*
	Autor: Carlos Gómez Ondoy
	Fecha: 01/12/2015
	Descripcion: Procedimiento para trasladar todas las orden y todas sus relaciones de las tablas transccionales a las tablas historicas
*/

CURSOR vlo_DsOrdenTrabajo IS
    SELECT ID_UBICACION, ID_ORDEN_TRABAJO FROM OTT_ORDEN_TRABAJO;

vlo_RegistroOrdenTrabajo vlo_DsOrdenTrabajo%ROWTYPE;   
	
BEGIN		
  
	FOR vlo_RegistroOrdenTrabajo IN vlo_DsOrdenTrabajo
    LOOP
         
		PR_OT_PASE_HISTORICO_UNICO(vlo_RegistroOrdenTrabajo.ID_UBICACION, vlo_RegistroOrdenTrabajo.ID_ORDEN_TRABAJO);		 
        
    END LOOP;   
	
	IF (SQL%ROWCOUNT = 0) THEN
		RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_PASE_HISTORICO_GENERAL]: No se ha actualizado ningún registro.');
	END IF;
EXCEPTION
	WHEN OTHERS THEN
		RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.PR_OT_PASE_HISTORICO_GENERAL]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
		ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE PR_OT_INHABILITAR_REQ(pvn_IdRequerimiento number)
IS

CURSOR vlo_Nietos IS SELECT ID_REQUERIMIENTO FROM OTM_REQUERIMIENTO WHERE ID_REQUERIMIENTO_PADRE = pvn_IdRequerimiento;

BEGIN
    /*
        Autor: César Bermúdez García
        Fecha: 1/11/2016
        Descripcion: Procedimiento para inhabilitar un requerimiento junto con sus niveles inferiores  
        
    */    
         
    UPDATE OTM_REQUERIMIENTO SET ESTADO = 'INA' WHERE ID_REQUERIMIENTO = pvn_IdRequerimiento;
    
    UPDATE OTM_REQUERIMIENTO SET ESTADO = 'INA' WHERE ID_REQUERIMIENTO_PADRE = pvn_IdRequerimiento;
    
    FOR nieto IN vlo_Nietos
    LOOP
        UPDATE OTM_REQUERIMIENTO SET ESTADO = 'INA' WHERE ID_REQUERIMIENTO_PADRE = nieto.ID_REQUERIMIENTO;
    END LOOP;
    
END PR_OT_INHABILITAR_REQ;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE PR_OT_BAJAR_RUBRO(pvn_IdRubro number)
AS

vln_Posicion NUMBER;
vln_PosicionSiguiente NUMBER;
vln_IdPosicionSiguiente NUMBER;

BEGIN
    /*
        Autor: César Bermúdez García
        Fecha: 16/03/2016
        Descripcion: Procedimiento para bajar el orden de un registro en la tabla OTM_RUBRO_DECISION_INICIA    
    */    
    
    /* Obtener el numero de orden de la posicion a bajar */
    SELECT ORDEN INTO vln_Posicion  FROM OTM_RUBRO_DECISION_INICIA WHERE ID_RUBRO_DECISION_INICIA = pvn_IdRubro;

    /* Obtener el numero de orden de la posicion a subir*/
    SELECT MIN(ORDEN) INTO vln_PosicionSiguiente FROM OTM_RUBRO_DECISION_INICIA WHERE ORDEN > vln_Posicion ORDER BY ORDEN DESC;
    
    /*Obtener el id de la posicion a subir*/
    SELECT ID_RUBRO_DECISION_INICIA INTO vln_IdPosicionSiguiente FROM OTM_RUBRO_DECISION_INICIA WHERE ORDEN=vln_PosicionSiguiente;

    /*Actualizar Posiciones*/
    BEGIN

        UPDATE OTM_RUBRO_DECISION_INICIA SET ORDEN = vln_PosicionSiguiente WHERE ID_RUBRO_DECISION_INICIA = pvn_IdRubro;
    
        UPDATE OTM_RUBRO_DECISION_INICIA SET ORDEN = vln_Posicion WHERE ID_RUBRO_DECISION_INICIA = vln_IdPosicionSiguiente;
      
             IF (SQL%ROWCOUNT = 0) THEN
                RAISE_APPLICATION_ERROR(-20002, '[PR_OT_BAJAR_RUBRO]: No se ha actualizado ningún registro.');
            END IF;
            EXCEPTION
            WHEN OTHERS THEN
                RAISE_APPLICATION_ERROR(-20002, '[PR_OT_BAJAR_RUBRO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
                ROLLBACK;
    END;
            
END;
/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE PR_OT_SUBIR_RUBRO(pvn_IdRubro number)
IS

vln_Posicion NUMBER;
vln_PosicionAnterior NUMBER;
vln_IdPosicionAnterior NUMBER;

BEGIN
    /*
        Autor: César Bermúdez García
        Fecha: 16/03/2016
        Descripcion: Procedimiento para subir el orden de un registro en la tabla OTM_RUBRO_DECISION_INICIA    
                
    */    
        
    /* Obtener el numero de orden de la posicion a subir */
    SELECT ORDEN INTO vln_Posicion  FROM OTM_RUBRO_DECISION_INICIA WHERE ID_RUBRO_DECISION_INICIA = pvn_IdRubro;

    /* Obtener el numero de orden de la posicion a bajar*/
    SELECT MAX(ORDEN) INTO vln_PosicionAnterior FROM OTM_RUBRO_DECISION_INICIA WHERE ORDEN < vln_Posicion ORDER BY ORDEN DESC;
    
    /*Obtener el id de la posicion a bajar*/
    SELECT ID_RUBRO_DECISION_INICIA INTO vln_IdPosicionAnterior FROM OTM_RUBRO_DECISION_INICIA WHERE ORDEN = vln_PosicionAnterior;

    /*Actualizar Posiciones*/
  
    UPDATE OTM_RUBRO_DECISION_INICIA SET ORDEN = vln_PosicionAnterior WHERE ID_RUBRO_DECISION_INICIA = pvn_IdRubro;
        
    UPDATE OTM_RUBRO_DECISION_INICIA SET ORDEN = vln_Posicion WHERE ID_RUBRO_DECISION_INICIA = vln_IdPosicionAnterior;
  
         IF (SQL%ROWCOUNT = 0) THEN
            RAISE_APPLICATION_ERROR(-20002, '[PR_OT_SUBIR_RUBRO]: No se ha actualizado ningún registro.');
        END IF;
        EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20002, '[PR_OT_SUBIR_RUBRO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
            ROLLBACK;

END;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE PR_OT_ASIG_OT_DISENIO
(
    pvn_IdUbicacion IN OTF_PRE_ORDEN_TRABAJO.ID_UBICACION%TYPE := NULL, 
    pvn_IdPreOrdenTrabajo IN OTF_PRE_ORDEN_TRABAJO.ID_PRE_ORDEN_TRABAJO%TYPE := NULL,
    pvn_CodUnidadSirh IN OTT_ORDEN_TRABAJO.COD_UNIDAD_SIRH%TYPE := NULL, 
    pvc_NombrePersonaContacto IN OTT_ORDEN_TRABAJO.NOMBRE_PERSONA_CONTACTO%TYPE := NULL, 
    pvc_Telefono IN OTT_ORDEN_TRABAJO.TELEFONO%TYPE := NULL, 
    pvc_SennasExactas IN OTT_ORDEN_TRABAJO.SENNAS_EXACTAS%TYPE := NULL, 
    pvc_DescripcionTrabajo IN OTT_ORDEN_TRABAJO.DESCRIPCION_TRABAJO%TYPE := NULL, 
    pvc_Usuario IN OTT_ORDEN_TRABAJO.USUARIO%TYPE := NULL, 
    pvn_NumEmpleado IN OTT_ORDEN_TRABAJO.NUM_EMPLEADO%TYPE := NULL, 
    pvn_IdCategoriaServicio IN OTT_ORDEN_TRABAJO.ID_CATEGORIA_SERVICIO%TYPE := NULL, 
    pvn_IdActividad IN OTT_ORDEN_TRABAJO.ID_ACTIVIDAD%TYPE := NULL, 
    pvn_IdLugarTrabajo IN OTT_ORDEN_TRABAJO.ID_LUGAR_TRABAJO%TYPE := NULL,
	pvn_IncluidaEnRecepcion IN OTT_ORDEN_TRABAJO.INCLUIDA_EN_RECEPCION%TYPE := NULL,
	pvn_IdUbicacionOrigen IN OTT_ORDEN_TRABAJO.ID_UBICACION_ORIGEN%TYPE := NULL
)
IS
/*
    Autor: Carlos Gómez Ondoy
    Fecha: 18/04/2016
    Descripcion: Procedimiento para trasladar una pre orden y todas sus relaciones de las tablas temporales a las tablas transccionales
	
	CONTROL DE CAMBIOS 
	
	Autor: Carlos Gómez Ondoy
    Fecha: 19/10/2016
    Descripcion: Modificación para restar un segundo a fecha
	
*/

vln_Consecutivo NUMBER;
vln_Bandera NUMBER;
vln_Anno NUMBER;
vln_DigitosAnno NUMBER;
vld_FechaEjecucion DATE;

CURSOR vlo_DsRevisionPreOrden IS
    SELECT * FROM OTF_REVISION_PRE_ORDEN_TRA WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo ORDER BY TIME_STAMP ASC;
            
vlo_RegistroRevisionPreOrden vlo_DsRevisionPreOrden%ROWTYPE;
vlo_RegistroPreOrden OTF_PRE_ORDEN_TRABAJO%ROWTYPE;
vlo_RegistroTrazabilidad OTT_TRAZABILIDAD_PROCESO%ROWTYPE;

BEGIN
        
    SELECT TO_NUMBER(TO_CHAR(SYSDATE, 'YYYY')) INTO vln_Anno FROM DUAL;
	vln_DigitosAnno := TO_NUMBER(SUBSTR(TO_CHAR(vln_Anno),2));
    SELECT * INTO vlo_RegistroPreOrden FROM OTF_PRE_ORDEN_TRABAJO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;

    vln_Bandera := 0;
    vln_Consecutivo := FN_OT_CONSECUTIVO_ORDEN(vln_Anno, pvn_IdUbicacion) + 1;

    FOR vlo_RegistroRevisionPreOrden IN vlo_DsRevisionPreOrden
    LOOP   
		vlo_RegistroTrazabilidad := NULL;	
        IF vln_Bandera = 0 THEN  
			IF pvn_IdUbicacionOrigen IS NOT NULL THEN
				INSERT INTO OTT_ORDEN_TRABAJO (COD_UNIDAD_SIRH, NOMBRE_PERSONA_CONTACTO, TELEFONO, SENNAS_EXACTAS, DESCRIPCION_TRABAJO, USUARIO, ID_UBICACION, ID_ORDEN_TRABAJO, ANNO, CONSECUTIVO, TIPO_ORDEN_TRABAJO, ESTADO_ORDEN_TRABAJO, NUM_EMPLEADO, ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD, ID_LUGAR_TRABAJO, FECHA_HORA_SOLICITA, INCLUIDA_EN_RECEPCION, ID_UBICACION_ORIGEN) VALUES (pvn_CodUnidadSirh, pvc_NombrePersonaContacto, pvc_Telefono, pvc_SennasExactas, pvc_DescripcionTrabajo, pvc_Usuario, pvn_IdUbicacion, TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo), vln_Anno, vln_Consecutivo, 'GEX', vlo_RegistroRevisionPreOrden.ESTADO, pvn_NumEmpleado, pvn_IdCategoriaServicio, pvn_IdActividad, pvn_IdLugarTrabajo, vlo_RegistroRevisionPreOrden.TIME_STAMP, pvn_IncluidaEnRecepcion, pvn_IdUbicacionOrigen);                                            
			ELSE
				INSERT INTO OTT_ORDEN_TRABAJO (COD_UNIDAD_SIRH, NOMBRE_PERSONA_CONTACTO, TELEFONO, SENNAS_EXACTAS, DESCRIPCION_TRABAJO, USUARIO, ID_UBICACION, ID_ORDEN_TRABAJO, ANNO, CONSECUTIVO, TIPO_ORDEN_TRABAJO, ESTADO_ORDEN_TRABAJO, NUM_EMPLEADO, ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD, ID_LUGAR_TRABAJO, FECHA_HORA_SOLICITA, INCLUIDA_EN_RECEPCION) VALUES (pvn_CodUnidadSirh, pvc_NombrePersonaContacto, pvc_Telefono, pvc_SennasExactas, pvc_DescripcionTrabajo, pvc_Usuario, pvn_IdUbicacion, TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo), vln_Anno, vln_Consecutivo, 'ORD', vlo_RegistroRevisionPreOrden.ESTADO, pvn_NumEmpleado, pvn_IdCategoriaServicio, pvn_IdActividad, pvn_IdLugarTrabajo, vlo_RegistroRevisionPreOrden.TIME_STAMP, pvn_IncluidaEnRecepcion);                                            
			END IF;		
        ELSE
            UPDATE OTT_ORDEN_TRABAJO SET USUARIO = pvc_Usuario, ESTADO_ORDEN_TRABAJO = vlo_RegistroRevisionPreOrden.ESTADO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO =  TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo);
        END IF;    

        IF vlo_RegistroRevisionPreOrden.ESTADO = 'DEV' OR vlo_RegistroRevisionPreOrden.ESTADO = 'DEN' THEN
            INSERT INTO OTT_TRAZABILIDAD_PROCESO (FECHA_HORA_EJECUCION, OBSERVACIONES, USUARIO, ID_TRAZABILIDAD_PROCESO, ID_UBICACION, ID_ORDEN_TRABAJO, NUM_EMPLEADO_EJECUTA, ESTADO_ORDEN_TRABAJO) VALUES (vlo_RegistroRevisionPreOrden.TIME_STAMP, vlo_RegistroRevisionPreOrden.OBSERVACIONES, pvc_Usuario, SQ_ID_TRAZABILIDAD_PROCESO.NEXTVAL, pvn_IdUbicacion, TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo), pvn_NumEmpleado, vlo_RegistroRevisionPreOrden.ESTADO);
        END IF;
		IF vlo_RegistroRevisionPreOrden.ESTADO = 'PDE' OR vlo_RegistroRevisionPreOrden.ESTADO = 'PEN' THEN
			
			SELECT * INTO vlo_RegistroTrazabilidad FROM (SELECT * FROM OTT_TRAZABILIDAD_PROCESO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo) ORDER BY TIME_STAMP DESC) WHERE ROWNUM = 1;
			
			IF vlo_RegistroRevisionPreOrden.ESTADO = 'PDE' THEN
			
			vld_FechaEjecucion := vlo_RegistroRevisionPreOrden.TIME_STAMP;
			
			vld_FechaEjecucion := vld_FechaEjecucion - INTERVAL '3' SECOND;
			
				UPDATE OTT_TRAZABILIDAD_PROCESO SET FECHA_HORA_EJECUCION = vld_FechaEjecucion WHERE ID_TRAZABILIDAD_PROCESO = vlo_RegistroTrazabilidad.ID_TRAZABILIDAD_PROCESO;
			ELSE
				UPDATE OTT_TRAZABILIDAD_PROCESO SET FECHA_HORA_EJECUCION = vlo_RegistroRevisionPreOrden.TIME_STAMP WHERE ID_TRAZABILIDAD_PROCESO = vlo_RegistroTrazabilidad.ID_TRAZABILIDAD_PROCESO;
			END IF;		
		END IF;	
        vln_Bandera := 1;        
    END LOOP; 

    UPDATE OTT_ORDEN_TRABAJO SET USUARIO = pvc_Usuario, ESTADO_ORDEN_TRABAJO = 'ASG' WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO =  TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo);
    
    IF vlo_RegistroPreOrden.IMAGEN1 IS NOT NULL THEN
        INSERT INTO OTT_ADJUNTO_ORDEN_TRABAJO (ID_ADJUNTO_ORDEN_TRABAJO, ID_UBICACION, ID_ORDEN_TRABAJO, NOMBRE_ARCHIVO, ARCHIVO, USUARIO, ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, DESCRIPCION) VALUES (SQ_ID_ADJUNTO_ORDEN_TRABAJO.NEXTVAL, pvn_IdUbicacion, TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo), vlo_RegistroPreOrden.NOMBRE_IMAGEN1, vlo_RegistroPreOrden.IMAGEN1, pvc_Usuario, 1, 2, 'Fotografía con detalle del trabajo proporcionado por el solicitante');
    END IF;
    
    IF vlo_RegistroPreOrden.IMAGEN2 IS NOT NULL THEN
        INSERT INTO OTT_ADJUNTO_ORDEN_TRABAJO (ID_ADJUNTO_ORDEN_TRABAJO, ID_UBICACION, ID_ORDEN_TRABAJO, NOMBRE_ARCHIVO, ARCHIVO, USUARIO, ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, DESCRIPCION) VALUES (SQ_ID_ADJUNTO_ORDEN_TRABAJO.NEXTVAL, pvn_IdUbicacion, TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo), vlo_RegistroPreOrden.NOMBRE_IMAGEN2, vlo_RegistroPreOrden.IMAGEN2, pvc_Usuario, 1, 2, 'Fotografía con detalle del trabajo proporcionado por el solicitante');
    END IF;
        
    INSERT INTO OTT_FICHA_TECNICA_GENERAL (ID_UBICACION, ID_ORDEN_TRABAJO, CONSERVA_MOBILIARIO, REQUIERE_NUEVO_MOBILIARIO, OTROS_MOBILIARIO, OTRO_TIPO_REQUERIMIENTO, NOMBRE_ARCHIVO, ARCHIVO, CUENTA_CON_ALARMA, REQUIERE_ALARMA, USUARIO, CUENTA_CON_PRESUPUESTO)
    SELECT ID_UBICACION, TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo), CONSERVA_MOBILIARIO, REQUIERE_NUEVO_MOBILIARIO, OTROS_MOBILIARIO, OTRO_TIPO_REQUERIMIENTO, NOMBRE_ARCHIVO, ARCHIVO, CUENTA_CON_ALARMA, REQUIERE_ALARMA, USUARIO, CUENTA_CON_PRESUPUESTO FROM
    OTF_FICHA_TECNICA_GENERAL WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    INSERT INTO OTT_FICHA_TECNICA_ESPACIO (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO)
    SELECT ID_UBICACION,  TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo), ID_ESPACIO FROM 
    OTF_FICHA_TECNICA_ESPACIO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    INSERT INTO OTT_FICHA_TECNICA_SUBCOMP (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE)
    SELECT ID_UBICACION,  TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo), ID_ESPACIO, ID_SUBCOMPONENTE FROM 
    OTF_FICHA_TECNICA_SUBCOMP WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    INSERT INTO OTT_FICHA_TECNICA_DETALLE (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE, ID_REQUERIMIENTO, VALOR, USUARIO)
    SELECT ID_UBICACION,  TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo), ID_ESPACIO, ID_SUBCOMPONENTE, ID_REQUERIMIENTO, VALOR, USUARIO FROM
    OTF_FICHA_TECNICA_DETALLE WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    DELETE FROM OTF_FICHA_TECNICA_DETALLE WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    DELETE FROM OTF_FICHA_TECNICA_SUBCOMP WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    DELETE FROM OTF_FICHA_TECNICA_ESPACIO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    DELETE FROM OTF_FICHA_TECNICA_GENERAL WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    DELETE FROM OTF_REVISION_PRE_ORDEN_TRA WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    DELETE FROM OTF_PRE_ORDEN_TRABAJO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;    
    
    IF (SQL%ROWCOUNT = 0) THEN
        RAISE_APPLICATION_ERROR(-20002, '[PR_OT_ASIG_OT_DISENIO]: No se ha actualizado ningún registro.');
    END IF;
EXCEPTION
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20002, '[PR_OT_ASIG_OT_DISENIO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
        ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE PR_OT_ASIG_OT_MANTE
(
    pvn_IdUbicacion IN OTF_PRE_ORDEN_TRABAJO.ID_UBICACION%TYPE := NULL, 
    pvn_IdPreOrdenTrabajo IN OTF_PRE_ORDEN_TRABAJO.ID_PRE_ORDEN_TRABAJO%TYPE := NULL,
    pvn_CodUnidadSirh IN OTT_ORDEN_TRABAJO.COD_UNIDAD_SIRH%TYPE := NULL, 
    pvc_NombrePersonaContacto IN OTT_ORDEN_TRABAJO.NOMBRE_PERSONA_CONTACTO%TYPE := NULL, 
    pvc_Telefono IN OTT_ORDEN_TRABAJO.TELEFONO%TYPE := NULL, 
    pvc_SennasExactas IN OTT_ORDEN_TRABAJO.SENNAS_EXACTAS%TYPE := NULL, 
    pvc_DescripcionTrabajo IN OTT_ORDEN_TRABAJO.DESCRIPCION_TRABAJO%TYPE := NULL, 
    pvc_Usuario IN OTT_ORDEN_TRABAJO.USUARIO%TYPE := NULL, 
    pvn_NumEmpleado IN OTT_ORDEN_TRABAJO.NUM_EMPLEADO%TYPE := NULL, 
    pvn_IdCategoriaServicio IN OTT_ORDEN_TRABAJO.ID_CATEGORIA_SERVICIO%TYPE := NULL, 
    pvn_IdActividad IN OTT_ORDEN_TRABAJO.ID_ACTIVIDAD%TYPE := NULL, 
    pvn_IdLugarTrabajo IN OTT_ORDEN_TRABAJO.ID_LUGAR_TRABAJO%TYPE := NULL,
	pvn_IncluidaEnRecepcion IN OTT_ORDEN_TRABAJO.INCLUIDA_EN_RECEPCION%TYPE := NULL,
	pvn_IdUbicacionOrigen IN OTT_ORDEN_TRABAJO.ID_UBICACION_ORIGEN%TYPE := NULL
)
IS
/*
    Autor: Carlos Gómez Ondoy
    Fecha: 19/04/2016
    Descripcion: Procedimiento para trasladar una pre orden (MANTENIMIENTO) y todas sus relaciones de las tablas temporales a las tablas transccionales
*/

vln_Consecutivo NUMBER;
vln_Bandera NUMBER;
vln_Anno NUMBER;
vln_DigitosAnno NUMBER;

CURSOR vlo_DsRevisionPreOrden IS
    SELECT * FROM OTF_REVISION_PRE_ORDEN_TRA WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo ORDER BY TIME_STAMP ASC;
            
vlo_RegistroRevisionPreOrden vlo_DsRevisionPreOrden%ROWTYPE;
vlo_RegistroPreOrden OTF_PRE_ORDEN_TRABAJO%ROWTYPE;
vlo_RegistroTrazabilidad OTT_TRAZABILIDAD_PROCESO%ROWTYPE;

BEGIN
        
    SELECT TO_NUMBER(TO_CHAR(SYSDATE, 'YYYY')) INTO vln_Anno FROM DUAL;
	vln_DigitosAnno := TO_NUMBER(SUBSTR(TO_CHAR(vln_Anno),2));
    SELECT * INTO vlo_RegistroPreOrden FROM OTF_PRE_ORDEN_TRABAJO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;

    vln_Bandera := 0;
    vln_Consecutivo := FN_OT_CONSECUTIVO_ORDEN(vln_Anno, pvn_IdUbicacion) + 1;

    FOR vlo_RegistroRevisionPreOrden IN vlo_DsRevisionPreOrden
    LOOP    
		vlo_RegistroTrazabilidad := NULL;		
        IF vln_Bandera = 0 THEN                
            IF pvn_IdUbicacionOrigen IS NOT NULL THEN
				INSERT INTO OTT_ORDEN_TRABAJO (COD_UNIDAD_SIRH, NOMBRE_PERSONA_CONTACTO, TELEFONO, SENNAS_EXACTAS, DESCRIPCION_TRABAJO, USUARIO, ID_UBICACION, ID_ORDEN_TRABAJO, ANNO, CONSECUTIVO, TIPO_ORDEN_TRABAJO, ESTADO_ORDEN_TRABAJO, NUM_EMPLEADO, ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD, ID_LUGAR_TRABAJO, FECHA_HORA_SOLICITA, INCLUIDA_EN_RECEPCION, ID_UBICACION_ORIGEN) VALUES (pvn_CodUnidadSirh, pvc_NombrePersonaContacto, pvc_Telefono, pvc_SennasExactas, pvc_DescripcionTrabajo, pvc_Usuario, pvn_IdUbicacion, TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo), vln_Anno, vln_Consecutivo, 'GEX', vlo_RegistroRevisionPreOrden.ESTADO, pvn_NumEmpleado, pvn_IdCategoriaServicio, pvn_IdActividad, pvn_IdLugarTrabajo, vlo_RegistroRevisionPreOrden.TIME_STAMP, pvn_IncluidaEnRecepcion, pvn_IdUbicacionOrigen);                                            
			ELSE
				INSERT INTO OTT_ORDEN_TRABAJO (COD_UNIDAD_SIRH, NOMBRE_PERSONA_CONTACTO, TELEFONO, SENNAS_EXACTAS, DESCRIPCION_TRABAJO, USUARIO, ID_UBICACION, ID_ORDEN_TRABAJO, ANNO, CONSECUTIVO, TIPO_ORDEN_TRABAJO, ESTADO_ORDEN_TRABAJO, NUM_EMPLEADO, ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD, ID_LUGAR_TRABAJO, FECHA_HORA_SOLICITA, INCLUIDA_EN_RECEPCION) VALUES (pvn_CodUnidadSirh, pvc_NombrePersonaContacto, pvc_Telefono, pvc_SennasExactas, pvc_DescripcionTrabajo, pvc_Usuario, pvn_IdUbicacion, TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo), vln_Anno, vln_Consecutivo, 'ORD', vlo_RegistroRevisionPreOrden.ESTADO, pvn_NumEmpleado, pvn_IdCategoriaServicio, pvn_IdActividad, pvn_IdLugarTrabajo, vlo_RegistroRevisionPreOrden.TIME_STAMP, pvn_IncluidaEnRecepcion);                                            
			END IF;	
        ELSE
            UPDATE OTT_ORDEN_TRABAJO SET USUARIO = pvc_Usuario, ESTADO_ORDEN_TRABAJO = vlo_RegistroRevisionPreOrden.ESTADO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO =  TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo);
        END IF;    

        IF vlo_RegistroRevisionPreOrden.ESTADO = 'DEV' OR vlo_RegistroRevisionPreOrden.ESTADO = 'DEN' THEN
            INSERT INTO OTT_TRAZABILIDAD_PROCESO (FECHA_HORA_EJECUCION, OBSERVACIONES, USUARIO, ID_TRAZABILIDAD_PROCESO, ID_UBICACION, ID_ORDEN_TRABAJO, NUM_EMPLEADO_EJECUTA, ESTADO_ORDEN_TRABAJO) VALUES (vlo_RegistroRevisionPreOrden.TIME_STAMP, vlo_RegistroRevisionPreOrden.OBSERVACIONES, pvc_Usuario, SQ_ID_TRAZABILIDAD_PROCESO.NEXTVAL, pvn_IdUbicacion, TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo), pvn_NumEmpleado, vlo_RegistroRevisionPreOrden.ESTADO);
        END IF;
		IF vlo_RegistroRevisionPreOrden.ESTADO = 'PDE' OR vlo_RegistroRevisionPreOrden.ESTADO = 'PEN' THEN
			
			SELECT * INTO vlo_RegistroTrazabilidad FROM (SELECT * FROM OTT_TRAZABILIDAD_PROCESO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo) ORDER BY TIME_STAMP DESC) WHERE ROWNUM = 1;
			UPDATE OTT_TRAZABILIDAD_PROCESO SET FECHA_HORA_EJECUCION = vlo_RegistroRevisionPreOrden.TIME_STAMP WHERE ID_TRAZABILIDAD_PROCESO = vlo_RegistroTrazabilidad.ID_TRAZABILIDAD_PROCESO;
		END IF;		
        vln_Bandera := 1;        
    END LOOP; 

    UPDATE OTT_ORDEN_TRABAJO SET USUARIO = pvc_Usuario, ESTADO_ORDEN_TRABAJO = 'ASG' WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO =  TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo);
    
    IF vlo_RegistroPreOrden.IMAGEN1 IS NOT NULL THEN
        INSERT INTO OTT_ADJUNTO_ORDEN_TRABAJO (ID_ADJUNTO_ORDEN_TRABAJO, ID_UBICACION, ID_ORDEN_TRABAJO, NOMBRE_ARCHIVO, ARCHIVO, USUARIO, ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, DESCRIPCION) VALUES (SQ_ID_ADJUNTO_ORDEN_TRABAJO.NEXTVAL, pvn_IdUbicacion, TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo), vlo_RegistroPreOrden.NOMBRE_IMAGEN1, vlo_RegistroPreOrden.IMAGEN1, pvc_Usuario, 1, 2, 'Fotografía con detalle del trabajo proporcionado por el solicitante');
    END IF;
    
    IF vlo_RegistroPreOrden.IMAGEN2 IS NOT NULL THEN
        INSERT INTO OTT_ADJUNTO_ORDEN_TRABAJO (ID_ADJUNTO_ORDEN_TRABAJO, ID_UBICACION, ID_ORDEN_TRABAJO, NOMBRE_ARCHIVO, ARCHIVO, USUARIO, ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, DESCRIPCION) VALUES (SQ_ID_ADJUNTO_ORDEN_TRABAJO.NEXTVAL, pvn_IdUbicacion, TO_CHAR(vln_DigitosAnno) || '-' || TO_CHAR(vln_Consecutivo), vlo_RegistroPreOrden.NOMBRE_IMAGEN2, vlo_RegistroPreOrden.IMAGEN2, pvc_Usuario, 1, 2, 'Fotografía con detalle del trabajo proporcionado por el solicitante');
    END IF;
     
    DELETE FROM OTF_REVISION_PRE_ORDEN_TRA WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    DELETE FROM OTF_PRE_ORDEN_TRABAJO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;    
    
    IF (SQL%ROWCOUNT = 0) THEN
        RAISE_APPLICATION_ERROR(-20002, '[PR_OT_ASIG_OT_MANTE]: No se ha actualizado ningún registro.');
    END IF;
EXCEPTION
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20002, '[PR_OT_ASIG_OT_MANTE]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
        ROLLBACK;
END;
/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE PR_OT_BORRAR_PRE_ORDEN
(
    pvn_IdUbicacion IN OTF_PRE_ORDEN_TRABAJO.ID_UBICACION%TYPE := NULL, 
    pvn_IdPreOrdenTrabajo IN OTF_PRE_ORDEN_TRABAJO.ID_PRE_ORDEN_TRABAJO%TYPE := NULL
)
IS
/*
    Autor: Carlos Gómez Ondoy
    Fecha: 21/04/2016
    Descripcion: Procedimiento para borrar una pre orden de trabajo y sus relaciones
*/

BEGIN
       
    DELETE FROM OTF_FICHA_TECNICA_DETALLE WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    DELETE FROM OTF_FICHA_TECNICA_SUBCOMP WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    DELETE FROM OTF_FICHA_TECNICA_ESPACIO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    DELETE FROM OTF_FICHA_TECNICA_GENERAL WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    DELETE FROM OTF_REVISION_PRE_ORDEN_TRA WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;
    
    DELETE FROM OTF_PRE_ORDEN_TRABAJO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_PRE_ORDEN_TRABAJO = pvn_IdPreOrdenTrabajo;    
    
    IF (SQL%ROWCOUNT = 0) THEN
        RAISE_APPLICATION_ERROR(-20002, '[PR_OT_BORRAR_PRE_ORDEN]: No se ha actualizado ningún registro.');
    END IF;
EXCEPTION
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20002, '[PR_OT_BORRAR_PRE_ORDEN]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
        ROLLBACK;
END;

/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE PR_OT_MIGRAR_ORDEN_TRABAJO
IS
CURSOR vlo_DsOrdenTrabajo IS SELECT ID_ORDEN_TRABAJO, ID_UBICACION, ID_LUGAR_TRABAJO, ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD, NUM_EMPLEADO, ANNO, FECHA_HORA_SOLICITA, COD_UNIDAD_SIRH, NOMBRE_PERSONA_CONTACTO, TELEFONO, SENNAS_EXACTAS, DESCRIPCION_TRABAJO, USUARIO, TIME_STAMP, INCLUIDA_EN_RECEPCION, ID_UBICACION_ORIGEN,  (SELECT B.NOMBRE_ARCHIVO FROM OTT_ADJUNTO_ORDEN_TRABAJO B WHERE B.ID_ORDEN_TRABAJO = A.ID_ORDEN_TRABAJO AND B.ID_UBICACION = A.ID_UBICACION AND B.ID_ADJUNTO_ORDEN_TRABAJO = (SELECT MAX(C.ID_ADJUNTO_ORDEN_TRABAJO) FROM OTT_ADJUNTO_ORDEN_TRABAJO C WHERE  C.ID_ORDEN_TRABAJO = A.ID_ORDEN_TRABAJO AND C.ID_UBICACION = A.ID_UBICACION)) AS "NOMBRE_IMAGEN1", (SELECT B.ARCHIVO FROM OTT_ADJUNTO_ORDEN_TRABAJO B WHERE B.ID_ORDEN_TRABAJO = A.ID_ORDEN_TRABAJO AND B.ID_UBICACION = A.ID_UBICACION AND B.ID_ADJUNTO_ORDEN_TRABAJO = (SELECT MAX(C.ID_ADJUNTO_ORDEN_TRABAJO) FROM OTT_ADJUNTO_ORDEN_TRABAJO C WHERE  C.ID_ORDEN_TRABAJO = A.ID_ORDEN_TRABAJO AND C.ID_UBICACION = A.ID_UBICACION))  AS "IMAGEN1", (SELECT B.NOMBRE_ARCHIVO FROM OTT_ADJUNTO_ORDEN_TRABAJO B WHERE B.ID_ORDEN_TRABAJO = A.ID_ORDEN_TRABAJO AND B.ID_UBICACION = A.ID_UBICACION AND B.ID_ADJUNTO_ORDEN_TRABAJO = (SELECT MIN(C.ID_ADJUNTO_ORDEN_TRABAJO) FROM OTT_ADJUNTO_ORDEN_TRABAJO C WHERE  C.ID_ORDEN_TRABAJO = A.ID_ORDEN_TRABAJO AND C.ID_UBICACION = A.ID_UBICACION)) AS "NOMBRE_IMAGEN2",   (SELECT B.ARCHIVO FROM OTT_ADJUNTO_ORDEN_TRABAJO B WHERE B.ID_ORDEN_TRABAJO = A.ID_ORDEN_TRABAJO AND B.ID_UBICACION = A.ID_UBICACION AND B.ID_ADJUNTO_ORDEN_TRABAJO = (SELECT MIN(C.ID_ADJUNTO_ORDEN_TRABAJO) FROM OTT_ADJUNTO_ORDEN_TRABAJO C WHERE  C.ID_ORDEN_TRABAJO = A.ID_ORDEN_TRABAJO AND C.ID_UBICACION = A.ID_UBICACION))  AS "IMAGEN2"FROM OTT_ORDEN_TRABAJO A WHERE A.ESTADO_ORDEN_TRABAJO = 'PEN' OR A.ESTADO_ORDEN_TRABAJO = 'DEV' OR A.ESTADO_ORDEN_TRABAJO = 'DEN'  OR A.ESTADO_ORDEN_TRABAJO = 'PDE';
vlo_RegistroOrdenTrabajo vlo_DsOrdenTrabajo%ROWTYPE;
vln_IdPreOrdenTrabajo NUMBER;

BEGIN

FOR vlo_RegistroOrdenTrabajo IN vlo_DsOrdenTrabajo
    LOOP    

        INSERT INTO OTF_PRE_ORDEN_TRABAJO (
        ID_PRE_ORDEN_TRABAJO, 
        ID_UBICACION, 
        ID_LUGAR_TRABAJO, 
        ID_CATEGORIA_SERVICIO, 
        ID_ACTIVIDAD, 
        NUM_EMPLEADO,
        ANNO,
        FECHA_HORA_SOLICITA, 
        COD_UNIDAD_SIRH, 
        NOMBRE_PERSONA_CONTACTO,
        TELEFONO, 
        SENNAS_EXACTAS, 
        DESCRIPCION_TRABAJO, 
        NOMBRE_IMAGEN1, 
        IMAGEN1, 
        NOMBRE_IMAGEN2, 
        IMAGEN2, 
        USUARIO, 
        INCLUIDA_EN_RECEPCION, 
        ID_UBICACION_ORIGEN) 
        VALUES (
        SQ_ID_PRE_ORDEN_TRABAJO.NEXTVAL, 
        vlo_RegistroOrdenTrabajo.ID_UBICACION, 
        vlo_RegistroOrdenTrabajo.ID_LUGAR_TRABAJO, 
        vlo_RegistroOrdenTrabajo.ID_CATEGORIA_SERVICIO, 
        vlo_RegistroOrdenTrabajo.ID_ACTIVIDAD, 
        vlo_RegistroOrdenTrabajo.NUM_EMPLEADO, 
        vlo_RegistroOrdenTrabajo.ANNO, 
        vlo_RegistroOrdenTrabajo.FECHA_HORA_SOLICITA,
        vlo_RegistroOrdenTrabajo.COD_UNIDAD_SIRH, 
        vlo_RegistroOrdenTrabajo.NOMBRE_PERSONA_CONTACTO, 
        vlo_RegistroOrdenTrabajo.TELEFONO,
        vlo_RegistroOrdenTrabajo.SENNAS_EXACTAS, 
        vlo_RegistroOrdenTrabajo.DESCRIPCION_TRABAJO, 
        vlo_RegistroOrdenTrabajo.NOMBRE_IMAGEN1, 
        vlo_RegistroOrdenTrabajo.IMAGEN2, 
        vlo_RegistroOrdenTrabajo.NOMBRE_IMAGEN2, 
        vlo_RegistroOrdenTrabajo.IMAGEN2, 
        vlo_RegistroOrdenTrabajo.USUARIO, 
        vlo_RegistroOrdenTrabajo.INCLUIDA_EN_RECEPCION, 
        vlo_RegistroOrdenTrabajo.ID_UBICACION_ORIGEN) RETURNING ID_PRE_ORDEN_TRABAJO INTO vln_IdPreOrdenTrabajo;
                
        INSERT INTO OTF_REVISION_PRE_ORDEN_TRA(
		ID_REVISION_PRE_ORDEN_TRA,
        ID_UBICACION,
        ID_PRE_ORDEN_TRABAJO,
        OBSERVACIONES,
        ESTADO,
        USUARIO,
        TIME_STAMP)
        SELECT 
		SQ_ID_REVISION_PRE_ORDEN_TRA.NEXTVAL,
        vlo_RegistroOrdenTrabajo.ID_UBICACION, 
        vln_IdPreOrdenTrabajo,
        OBSERVACIONES,
        ESTADO_ORDEN_TRABAJO,
        USUARIO,
        TIME_STAMP
        FROM    
        OTT_TRAZABILIDAD_PROCESO WHERE ID_ORDEN_TRABAJO = vlo_RegistroOrdenTrabajo.ID_ORDEN_TRABAJO AND ID_UBICACION = vlo_RegistroOrdenTrabajo.ID_UBICACION;
		
		DELETE FROM OTT_TRAZABILIDAD_PROCESO WHERE ID_ORDEN_TRABAJO = vlo_RegistroOrdenTrabajo.ID_ORDEN_TRABAJO AND ID_UBICACION = vlo_RegistroOrdenTrabajo.ID_UBICACION; 

		DELETE FROM OTT_ADJUNTO_ORDEN_TRABAJO WHERE ID_ORDEN_TRABAJO = vlo_RegistroOrdenTrabajo.ID_ORDEN_TRABAJO AND ID_UBICACION = vlo_RegistroOrdenTrabajo.ID_UBICACION; 

		DELETE FROM OTT_REVISION_ORDEN_TRABAJ WHERE ID_ORDEN_TRABAJO = vlo_RegistroOrdenTrabajo.ID_ORDEN_TRABAJO AND ID_UBICACION = vlo_RegistroOrdenTrabajo.ID_UBICACION; 
	        
    END LOOP; 
	
	DELETE FROM OTT_ORDEN_TRABAJO WHERE ESTADO_ORDEN_TRABAJO = 'PEN' OR ESTADO_ORDEN_TRABAJO = 'DEV' OR ESTADO_ORDEN_TRABAJO = 'DEN'  OR ESTADO_ORDEN_TRABAJO = 'PDE';	
    
    IF (SQL%ROWCOUNT = 0) THEN
        RAISE_APPLICATION_ERROR(-20002, '[PR_OT_MIGRAR_ORDEN_TRABAJO]: No se ha actualizado ningún registro.');
    END IF;
EXCEPTION
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20002, '[PR_OT_MIGRAR_ORDEN_TRABAJO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
        ROLLBACK;
END;
/*------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE PROCEDURE PR_OT_INVENTARIO
(
    pvn_IdAlmacenBodega IN OTF_INVENTARIO.ID_ALMACEN_BODEGA%TYPE := NULL, 
    pvn_IdUbicacionAdministra IN OTF_INVENTARIO.ID_UBICACION_ADMINISTRA%TYPE := NULL, 
    pvn_IdMaterial IN OTF_INVENTARIO.ID_MATERIAL%TYPE := NULL, 
    pvn_CantidadRetiro IN NUMBER DEFAULT 0,
    pvc_Usuario IN OTF_INVENTARIO.USUARIO%TYPE := NULL, 
    pvd_TimeStamp IN OTF_INVENTARIO.TIME_STAMP%TYPE := NULL  
)
IS
/*
    Autor:            César Bermúdez G
    Fecha:            4/07/2016 
    Descripcion:     Procedimiento para restar al inventario la cantidad solicitada a retirar.
*/
vln_CantidadDisponible NUMBER;

BEGIN
    /* Obtener la cantidad disponible real del almacen o bodega*/
    SELECT CANTIDAD_DISPONIBLE INTO vln_CantidadDisponible FROM OTF_INVENTARIO WHERE ID_ALMACEN_BODEGA = pvn_IdAlmacenBodega AND ID_UBICACION_ADMINISTRA = pvn_IdUbicacionAdministra AND ID_MATERIAL = pvn_IdMaterial;
    /*Se valida si es posible dispensar dicha cantidad*/
    IF pvn_CantidadRetiro <= vln_CantidadDisponible THEN
        SELECT CANTIDAD_DISPONIBLE - pvn_CantidadRetiro INTO vln_CantidadDisponible FROM OTF_INVENTARIO WHERE ID_ALMACEN_BODEGA = pvn_IdAlmacenBodega AND ID_UBICACION_ADMINISTRA = pvn_IdUbicacionAdministra AND ID_MATERIAL = pvn_IdMaterial;
        UPDATE OTF_INVENTARIO SET CANTIDAD_DISPONIBLE = vln_CantidadDisponible, USUARIO = pvc_Usuario, TIME_STAMP = SYSDATE WHERE ID_ALMACEN_BODEGA = pvn_IdAlmacenBodega AND ID_UBICACION_ADMINISTRA = pvn_IdUbicacionAdministra AND ID_MATERIAL = pvn_IdMaterial;
    ELSE
        RAISE_APPLICATION_ERROR(-1, '[ORDENES_TRABAJO.prU_OTF_INVENTARIO]: Material insuficiente en almacen o bodega.');
    END IF;
    IF (SQL%ROWCOUNT = 0) THEN
        RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.prU_OTF_INVENTARIO]: No se ha actualizado ningún registro.');
    END IF;
EXCEPTION
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20002, '[ORDENES_TRABAJO.prU_OTF_INVENTARIO]: Se ha producido un problema en la ejecución del procedimiento: ('||SQLCODE||') - '||SQLERRM||'.  ');
        ROLLBACK;
END PR_OT_INVENTARIO;
/*------------------------------------------------------------------------------------------------------------------------*/