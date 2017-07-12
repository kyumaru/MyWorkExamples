DROP FUNCTION FN_OT_CONSECUTIVO_ORDEN;
DROP FUNCTION FN_OT_CONSECUTIVO_ORDEN_HIJA;
DROP FUNCTION FN_OT_OBTENER_ULTIMO_ORDEN;
DROP FUNCTION FN_OT_CONSULTA_LUGAR_TRABAJO;
DROP FUNCTION FN_OT_VERSION_ANTEPROYECTO;
DROP FUNCTION FN_OT_ULTIMO_ORDEN_SUB;
DROP FUNCTION FN_OT_ULTIMO_ORDEN_REQ;
DROP FUNCTION FN_OT_ULTIMO_ORDEN_RUBROS;
DROP FUNCTION FN_OT_ID_VIA_SUGERENCIA;
DROP FUNCTION FN_OT_SIGUIENTE_ESTADO_CONT;
DROP FUNCTION FN_OT_CONSECUTIVO_REINGRESO;
DROP FUNCTION FN_OT_CONSECUTIVO_RETIRO;
DROP FUNCTION FN_OT_NUMERO_GESTION_COMPRA;
DROP FUNCTION FN_OT_CANTIDAD_ADJUNTOS_ETAPA;

/*----------------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE FUNCTION FN_OT_VERSION_ANTEPROYECTO(pvn_IdUbicacion IN OTT_ANTEPROYECTO.ID_UBICACION%TYPE := NULL, pvc_IdOrdenTrabajo IN OTT_ANTEPROYECTO.ID_ORDEN_TRABAJO%TYPE := NULL)
RETURN NUMBER IS
/*
    Autor:         CARLOS GOMEZ
    Fecha:         16/03/2016
    Descripcion:   Devuelve el maximo valor de la columna version, para una OT especifica
	
*/ 
VLN_VERSION NUMBER(2);
BEGIN
    SELECT
        MAX(TO_NUMBER(VERSION)) INTO VLN_VERSION
    FROM OTT_ANTEPROYECTO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo;
        
    RETURN NVL(VLN_VERSION, 0);
END;
/*----------------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE FUNCTION FN_OT_CONSECUTIVO_ORDEN(pvn_Annio IN OTT_ORDEN_TRABAJO.ANNO%TYPE := NULL, pvn_IdUbicacion IN OTT_ORDEN_TRABAJO.ID_UBICACION%TYPE := NULL)
RETURN NUMBER IS
/*
    Autor:         CARLOS GOMEZ
    Fecha:         03/09/2015
    Descripcion:   Devuelve el maximo valor de la columna consecutivo
	
	CONTROL DE CAMBIOS
	
	Autor:         CARLOS GOMEZ
    Fecha:         26/11/2015
    Descripcion:   MODIFICACION DE TABLA 
	
*/ 
VLN_CONSECUTIVO NUMBER(10,0);
BEGIN
    SELECT
        MAX(TO_NUMBER(CONSECUTIVO)) INTO VLN_CONSECUTIVO
    FROM OTT_ORDEN_TRABAJO WHERE ANNO = pvn_Annio AND ID_UBICACION = pvn_IdUbicacion;
        
    RETURN NVL(VLN_CONSECUTIVO, 0);
END;
/*----------------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE FUNCTION FN_OT_CONSECUTIVO_ORDEN_HIJA(pvn_Annio IN OTT_ORDEN_TRABAJO.ANNO%TYPE := NULL, pvn_IdUbicacion IN OTT_ORDEN_TRABAJO.ID_UBICACION%TYPE := NULL, pvn_IdCategoria IN OTT_ORDEN_TRABAJO.ID_CATEGORIA_SERVICIO%TYPE := NULL, pvn_IdOrdenTrabajo IN OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO%TYPE := NULL)
RETURN NUMBER IS
/*
    Autor:         CARLOS GOMEZ
    Fecha:         30/11/2015
    Descripcion:   Devuelve el maximo valor de la columna consecutivo PARA ORDENES HIJAS
	
	CONTROL DE CAMBIOS
	
	Autor:         CARLOS GOMEZ
    Fecha:         29/03/2016
    Descripcion:   Se agrega la condición de pvn_IdOrdenTrabajo
    
*/ 
VLN_CONSECUTIVO NUMBER(10,0);
BEGIN
    SELECT
        MAX(TO_NUMBER(CONSECUTIVO_HIJA)) INTO VLN_CONSECUTIVO
    FROM OTT_ORDEN_TRABAJO WHERE ANNO = pvn_Annio AND ID_UBICACION = pvn_IdUbicacion AND ID_CATEGORIA_SERVICIO = pvn_IdCategoria AND PARENTESCO = 'HIJ' AND ID_ORDEN_TRABAJO = pvn_IdOrdenTrabajo;
        
    RETURN NVL(VLN_CONSECUTIVO, 0);
END;
/*----------------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE FUNCTION FN_OT_OBTENER_ULTIMO_ORDEN
RETURN NUMBER IS  
    /*
        Autor:          César Bermúdez García
        Fecha:          11/11/2015
        Descripcion:    obtiene el último valor del orden de la tabla OTM_ESPACIO
    */
    MAX_ORDEN NUMBER(2,0);
BEGIN    
    SELECT NVL(MAX(TO_NUMBER(ORDEN)+1),1) INTO MAX_ORDEN FROM OTM_ESPACIO;

    RETURN MAX_ORDEN;
END;


/*----------------------------------------------------------------------------------------------------------------------------------*/
CREATE OR REPLACE FUNCTION FN_OT_ULTIMO_ORDEN_SUB(pvn_IdEspacio IN OTM_SUBCOMPONENTE.ID_ESPACIO%TYPE := NULL)
RETURN NUMBER IS  
    /*
        Autor:          César Bermúdez García
        Fecha:          17/11/2015
        Descripcion:    Obtiene el último valor del orden de la tabla OTM_SUBCOMPONENTE
    */
    MAX_ORDEN NUMBER(2,0);
BEGIN    
    SELECT NVL(MAX(TO_NUMBER(ORDEN)+1),1) INTO MAX_ORDEN FROM OTM_SUBCOMPONENTE WHERE ID_ESPACIO = pvn_IdEspacio;
    
    RETURN MAX_ORDEN;
END;

/*----------------------------------------------------------------------------------------------------------------------------------*/
CREATE OR REPLACE FUNCTION FN_OT_ULTIMO_ORDEN_REQ(pvn_Nivel NUMBER)
RETURN NUMBER IS  
    /*
        Autor:          César Bermúdez García
        Fecha:          24/11/2015
        Descripcion:    Obtiene el último valor del orden de la tabla OTM_REQUERIMIENTO
    */
    MAX_ORDEN NUMBER(2,0);
BEGIN    
    SELECT NVL(MAX(TO_NUMBER(ORDEN)+1),1) INTO MAX_ORDEN FROM OTM_REQUERIMIENTO WHERE NIVEL = 1;
    
    RETURN MAX_ORDEN;
END;
/*----------------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE FUNCTION FN_OT_CONSULTA_LUGAR_TRABAJO(pvn_IdCategoria NUMBER,pvn_IdActividad NUMBER,pvn_IdLugarTrabajo NUMBER)
RETURN VARCHAR2 AS
        
    vln_IdSectorTallerCat OTM_CATEGORIA_SERVICIO.ID_SECTOR_TALLER%TYPE;
    vln_IdSectorTallerAct OTM_ACTIVIDAD.ID_SECTOR_TALLER%TYPE;
    vln_IdSectorTallerLug OTM_LUGAR_TRABAJO.ID_SECTOR_TALLER%TYPE;
    vln_NombreSectorTaller OTM_SECTOR_TALLER.NOMBRE%TYPE;
    
BEGIN
    /*
        Autor:          César Bermúdez García
        Fecha:          3/12/2015
        Descripcion:    Devuelve el nombre de sector o taller que el sistema asignará
    */
        SELECT ID_SECTOR_TALLER INTO vln_IdSectorTallerCat FROM OTM_CATEGORIA_SERVICIO WHERE ID_CATEGORIA_SERVICIO = pvn_IdCategoria;
        SELECT ID_SECTOR_TALLER INTO vln_IdSectorTallerAct FROM OTM_ACTIVIDAD WHERE ID_ACTIVIDAD = pvn_IdActividad;
        SELECT ID_SECTOR_TALLER INTO vln_IdSectorTallerLug FROM OTM_LUGAR_TRABAJO WHERE ID_LUGAR_TRABAJO = pvn_IdLugarTrabajo;
    
        IF vln_IdSectorTallerCat IS NOT NULL THEN
          
        SELECT NOMBRE INTO vln_NombreSectorTaller 
        FROM OTM_SECTOR_TALLER 
        WHERE ID_SECTOR_TALLER = vln_IdSectorTallerCat;

        ELSIF vln_IdSectorTallerAct IS NOT NULL THEN

           SELECT NOMBRE INTO vln_NombreSectorTaller 
           FROM OTM_SECTOR_TALLER 
           WHERE ID_SECTOR_TALLER = vln_IdSectorTallerAct;

        ELSE
           SELECT NOMBRE INTO vln_NombreSectorTaller 
           FROM OTM_SECTOR_TALLER 
           WHERE ID_SECTOR_TALLER = vln_IdSectorTallerLug;

        END IF;
    RETURN vln_NombreSectorTaller;
END FN_OT_CONSULTA_LUGAR_TRABAJO;
/*----------------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE FUNCTION FN_OT_ULTIMO_ORDEN_RUBROS
RETURN NUMBER IS  
    /*
        Autor:          César Bermúdez García
        Fecha:          16/03/2016
        Descripcion:    Procedimiento para obtener el último valor del orden de la tabla OTM_RUBRO_DECISION_INICIA
    */
    MAX_ORDEN NUMBER(2,0);
BEGIN    
    SELECT NVL(MAX(TO_NUMBER(ORDEN)+1),1) INTO MAX_ORDEN FROM OTM_RUBRO_DECISION_INICIA;
    
    RETURN MAX_ORDEN;
END;
/*----------------------------------------------------------------------------------------------------------------------------------*/


CREATE OR REPLACE FUNCTION FN_OT_ID_VIA_SUGERENCIA(pvn_monto NUMBER)

RETURN NUMBER IS
/*
        Autor:          César Bermúdez García
        Fecha:          18/03/2016
        Descripcion:    funcion para devolver el ID del valor sugerido para la via de contratacion
    */
vlo_resultado NUMBER(2,0);
BEGIN
SELECT ID_VIA_CONTRATO INTO vlo_resultado FROM ORDENES_TRABAJO.OTM_VIA_CONTRATO WHERE TOPE_ECONOMICO = (SELECT MIN(TOPE_ECONOMICO) FROM ORDENES_TRABAJO.OTM_VIA_CONTRATO WHERE TOPE_ECONOMICO >=pvn_monto);
RETURN vlo_resultado;
END;
/*----------------------------------------------------------------------------------------------------------------------------------*/

 CREATE OR REPLACE FUNCTION FN_OT_SIGUIENTE_ESTADO_CONT(pvn_id_etapa_contratacion NUMBER)
 RETURN NUMBER IS
 vln_Resultado NUMBER(2,0);
  BEGIN
 SELECT ID_ETAPA_CONTRATACION INTO vln_Resultado FROM OTM_ETAPA_CONTRATACION WHERE ORDEN = (SELECT ORDEN + 1 FROM OTM_ETAPA_CONTRATACION WHERE ID_ETAPA_CONTRATACION = pvn_id_etapa_contratacion);
 RETURN vln_Resultado;
 END;
/*----------------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE FUNCTION FN_OT_CONSECUTIVO_REINGRESO(pvn_Annio IN OTT_SOLICITUD_REINGRESO.ANNO%TYPE := NULL, pvn_IdUbicacion IN OTT_SOLICITUD_REINGRESO.ID_UBICACION_ADMINISTRA%TYPE := NULL)
RETURN NUMBER IS
/*
    Autor:         CARLOS GOMEZ
    Fecha:         29/07/2016
    Descripcion:   Devuelve el maximo valor de la columna ID_SOLICITUD_REINGRESO
	
*/ 
VLN_CONSECUTIVO NUMBER(10,0);
BEGIN
    SELECT
        MAX(TO_NUMBER(ID_SOLICITUD_REINGRESO)) INTO VLN_CONSECUTIVO
    FROM OTT_SOLICITUD_REINGRESO WHERE ANNO = pvn_Annio AND ID_UBICACION_ADMINISTRA = pvn_IdUbicacion;
        
    RETURN NVL(VLN_CONSECUTIVO, 0);
END;
/*----------------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE FUNCTION FN_OT_NUMERO_GESTION_COMPRA(pvn_Annio IN OTT_GESTION_COMPRA.ANNO%TYPE := NULL, pvn_IdUbicacion IN OTT_GESTION_COMPRA.ID_UBICACION%TYPE := NULL, pvn_IdViaCompraContrato IN OTT_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO%TYPE := NULL)
RETURN NUMBER IS
/*
    Autor:         CARLOS GOMEZ
    Fecha:         23/08/2016
    Descripcion:   Devuelve el maximo valor de la columna NUMERO_GESTION
	
*/ 
VLN_NUMERO_GESTION NUMBER(10,0);
BEGIN
    SELECT
        MAX(TO_NUMBER(NUMERO_GESTION)) INTO VLN_NUMERO_GESTION
    FROM OTT_GESTION_COMPRA WHERE ANNO = pvn_Annio AND ID_UBICACION = pvn_IdUbicacion AND ID_VIA_COMPRA_CONTRATO = pvn_IdViaCompraContrato;
        
    RETURN NVL(VLN_NUMERO_GESTION, 0);
END;

/*----------------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE FUNCTION FC_OT_MAT_AGRUPADOS_SUM
(
   PVC_HILERA VARCHAR2
)
RETURN CTP_MAT_SIMINISTROS PIPELINED IS
/*
    Autor:         Mauricio Salas
    Fecha:         06/10/2015
    Descripcion:   Función para obtener los materiales agrupados a la hora de generar una gestion de compra por suministros y llenar el acordeon de la pantalla
    Prueba: SELECT * FROM   TABLE(FC_OT_MAT_AGRUPADOS_SUM('78,101,91,90'));
*/
CURSOR vlo_DsDatos (CPVC_HILERA VARCHAR2) IS
             SELECT
                VISTA.ID_MATERIAL AS "ID_MATERIAL",
                (SELECT DESCRIPCION FROM OTM_MATERIAL WHERE ID_MATERIAL = VISTA.ID_MATERIAL) AS "NOMBRE_MATERIAL",
                SUM(VISTA.CANTIDAD_SOLICITADA)  AS "CANTIDAD_SOLICITADA"
            FROM (SELECT ID_DETALLE_MATERIAL, ID_MATERIAL, CANTIDAD_SOLICITADA FROM OTT_DETALLE_MATERIAL WHERE ID_DETALLE_MATERIAL IN  (SELECT TO_NUMBER(COLUMN_VALUE) FROM TABLE(SPLITSTRING(CPVC_HILERA,',') )) ) VISTA
            GROUP BY VISTA.ID_MATERIAL;

vlo_Registro vlo_DsDatos%ROWTYPE;

BEGIN    


    FOR vlo_Registro IN vlo_DsDatos(PVC_HILERA) 
    LOOP               
         PIPE ROW 
                         (TP_MAT_SIMINISTROS(  
                        vlo_Registro.ID_MATERIAL,
                        vlo_Registro.NOMBRE_MATERIAL,
                        vlo_Registro.CANTIDAD_SOLICITADA
                        ));
    END LOOP;        

    RETURN;
END;

/*----------------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE FUNCTION FN_OT_CANTIDAD_ADJUNTOS_ETAPA(pvn_IdUbicacion IN OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION%TYPE := NULL, pvc_IdOrdenTrabajo IN OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO%TYPE := NULL, pvn_IdEtapaOrdenTrabajo IN OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO%TYPE := NULL)
RETURN NUMBER IS
/*
    Autor:         CARLOS GOMEZ
    Fecha:         14/09/2016
    Descripcion:   Devuelve la cantidad de adjunto, para una OT especifica y una respectiva etapa
	
*/ 
VLN_CANTIDAD NUMBER(2);
BEGIN
    SELECT
        COUNT(TO_NUMBER(ID_ADJUNTO_ORDEN_TRABAJO)) INTO VLN_CANTIDAD
    FROM OTT_ADJUNTO_ORDEN_TRABAJO WHERE ID_UBICACION = pvn_IdUbicacion AND ID_ORDEN_TRABAJO = pvc_IdOrdenTrabajo AND ID_ETAPA_ORDEN_TRABAJO = pvn_IdEtapaOrdenTrabajo;
        
    RETURN NVL(VLN_CANTIDAD, 0);
END;
/*----------------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE FUNCTION FN_OT_CONSECUTIVO_RETIRO(pvn_Annio IN OTT_SOLICITUD_RETIRO.ANNO%TYPE := NULL, pvn_IdUbicacion IN OTT_SOLICITUD_RETIRO.ID_UBICACION%TYPE := NULL)
RETURN NUMBER IS
/*
    Autor:         CARLOS GOMEZ
    Fecha:         03/10/2016
    Descripcion:   Devuelve el maximo valor de la columna ID_SOLICITUD_RETIRO
	
*/ 
VLN_CONSECUTIVO NUMBER(10,0);
BEGIN
    SELECT
        MAX(TO_NUMBER(ID_SOLICITUD_RETIRO)) INTO VLN_CONSECUTIVO
    FROM OTT_SOLICITUD_RETIRO WHERE ANNO = pvn_Annio AND ID_UBICACION = pvn_IdUbicacion;
        
    RETURN NVL(VLN_CONSECUTIVO, 0);
END;

/*----------------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE FUNCTION FC_OT_CONSECUTIVO_AJUSTE(pvn_Annio IN OTT_AJUSTE_INVENTARIO.ANNO%TYPE := NULL, pvn_IdUbicacion IN OTT_AJUSTE_INVENTARIO.ID_UBICACION%TYPE := NULL)
RETURN NUMBER IS
/*
    Autor:         Mauricio Salas
    Fecha:         26/01/2016
    Descripcion:   Devuelve el maximo valor de la columna CONSECUTIVO_AJUSTE
    
*/ 
VLN_CONSECUTIVO_AJUSTE NUMBER(4,0);
BEGIN
    SELECT
        MAX(TO_NUMBER(CONSECUTIVO_AJUSTE)) INTO VLN_CONSECUTIVO_AJUSTE
    FROM OTT_AJUSTE_INVENTARIO WHERE ANNO = pvn_Annio AND ID_UBICACION = pvn_IdUbicacion;
        
    RETURN NVL(VLN_CONSECUTIVO_AJUSTE, 0);
END;

/*----------------------------------------------------------------------------------------------------------------------------------*/

CREATE OR REPLACE FUNCTION FC_OT_CONSECUTIVO_INGRESO_MAT(pvn_Annio IN OTT_GESTION_INGRESO_MATER.ANNO%TYPE := NULL, pvn_IdUbicacion IN OTT_GESTION_INGRESO_MATER.ID_UBICACION%TYPE := NULL, pvn_NumeroGestion IN OTT_GESTION_INGRESO_MATER.NUMERO_GESTION%TYPE := NULL,  pvn_IdViaCompra IN OTT_GESTION_INGRESO_MATER.ID_VIA_COMPRA_CONTRATO%TYPE := NULL)
RETURN NUMBER IS
/*
    Autor:         Mauricio Salas
    Fecha:         14/02/2017
    Descripcion:   Devuelve el maximo valor de la columna CONSECUTIVO
    
*/ 
VLN_CONSECUTIVO NUMBER(4,0);
BEGIN
    SELECT
        MAX(TO_NUMBER(CONSECUTIVO)) INTO VLN_CONSECUTIVO
    FROM OTT_GESTION_INGRESO_MATER WHERE ANNO = pvn_Annio AND ID_UBICACION = pvn_IdUbicacion AND ID_VIA_COMPRA_CONTRATO = pvn_IdViaCompra AND NUMERO_GESTION = pvn_NumeroGestion;
        
    RETURN NVL(VLN_CONSECUTIVO, 0);
END;