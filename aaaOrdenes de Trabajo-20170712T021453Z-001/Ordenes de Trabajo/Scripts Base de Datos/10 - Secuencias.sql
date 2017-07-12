create sequence SQ_ID_ACTIVIDAD
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_ADJUNTO_ORDEN_TRABAJO
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_CATEGORIA_SERVICIO
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_REVISION_ORDEN_TRABAJ
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_SECTOR_TALLER
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_TIPO_LUGAR_UBICACION
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_UBICACION
increment by 1
start with 1
nocycle
 nocache
order;


--02-09-2015
create sequence SQ_ID_LUGAR_TRABAJO
increment by 1
start with 1
nocycle
 nocache
order;

--09-09-2015
create sequence SQ_ID_TRAZABILIDAD_PROCESO
increment by 1
start with 1
nocycle
 nocache
order;

--21-09-2015
--se agrega rango el 25-09-2015
create sequence SQ_ID_MOTIVO_RECHAZO
increment by 1
start with 1
 maxvalue 99
 minvalue 1
nocycle
 nocache
order;

---30-10-2015
create sequence SQ_ID_ESPACIO
increment by 1
start with 1
nocycle
 nocache
order;


create sequence SQ_ID_REQUERIMIENTO
increment by 1
start with 1
nocycle
 nocache
order;



create sequence SQ_ID_SUBCOMPONENTE
increment by 1
start with 1
nocycle
 nocache
order;


--02-11-2015
create sequence SQ_ID_PERIODO_CIERRE
increment by 1
start with 1
nocycle
 nocache
order;




create sequence SQ_ID_AREA_PROFESIONAL
increment by 1
start with 1
 nomaxvalue
 minvalue 1
nocycle
 nocache
order;

create sequence SQ_ID_ETAPA_ORDEN_TRABAJO
increment by 1
start with 1
 nomaxvalue
 minvalue 1
cycle
 nocache
order;

create sequence SQ_ID_EXCEPCION_PERIODO
increment by 1
start with 1
 nomaxvalue
 minvalue 1
nocycle
 nocache
order;

create sequence SQ_ID_PERIODO_CIERRE
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_UNIDAD_TIEMPO
increment by 1
start with 1
 nomaxvalue
 minvalue 1
nocycle
 nocache
order;
/*
drop sequence SQ_ID_AREA_PROFESIONAL;
drop sequence SQ_ID_ETAPA_ORDEN_TRABAJO;
drop sequence SQ_ID_EXCEPCION_PERIODO;
drop sequence SQ_ID_PERIODO_CIERRE;
drop sequence SQ_ID_UNIDAD_TIEMPO;
drop sequence SQ_ID_TIPO_DOCUMENTO;
drop sequence SQ_ID_RUBRO_DECISION_INICIA;
drop sequence SQ_ID_TIPO_OBRA;
drop sequence SQ_ID_PRE_ORDEN_TRABAJO;
drop sequence SQ_ID_REVISION_PRE_ORDEN_TRA;
drop sequence SQ_ID_ADJUNTO_GESTION_COMPR;
*/
--15-01-2016

create sequence SQ_ID_AREA_PROFESIONAL
increment by 1
start with 1
 nomaxvalue
 minvalue 1
nocycle
 nocache
order;

create sequence SQ_ID_ETAPA_ORDEN_TRABAJO
increment by 1
start with 1
 nomaxvalue
 minvalue 1
nocycle
 nocache
order;

create sequence SQ_ID_EXCEPCION_PERIODO
increment by 1
start with 1
 nomaxvalue
 minvalue 1
nocycle
 nocache
order;

create sequence SQ_ID_PERIODO_CIERRE
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_UNIDAD_TIEMPO
increment by 1
start with 1
 nomaxvalue
 minvalue 1
nocycle
 nocache
order;


create sequence SQ_ID_TIPO_DOCUMENTO
increment by 1
start with 1
 nomaxvalue
minvalue 1
nocycle
 nocache
noorder;


create sequence SQ_ID_RUBRO_DECISION_INICIA
increment by 1
start with 1
nomaxvalue
nominvalue
nocycle
 nocache
order;

create sequence SQ_ID_TIPO_OBRA
increment by 1
start with 1
nomaxvalue
nominvalue
nocycle
 nocache
order;


create sequence SQ_ID_PRE_ORDEN_TRABAJO
increment by 1
start with 1
 nomaxvalue
 nominvalue
nocycle
 nocache
order;

create sequence SQ_ID_REVISION_PRE_ORDEN_TRA
increment by 1
start with 1
 nomaxvalue
 nominvalue
nocycle
 nocache
order;

/*create sequence SQ_ID_VIA_CONTRATO
increment by 1
start with 1
nocycle
 nocache
order;*/


create sequence SQ_ID_ETAPA_CONTRATACION
increment by 1
start with 1
nocycle
 nocache
order;


--16-05-2016
create sequence SQ_ID_VIA_COMPRA_CONTRATO
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_UNIDAD_MEDIDA
increment by 1
start with 1
nocycle
 nocache
order;


create sequence SQ_ID_ALMACEN_BODEGA
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_SUBCATEGORIA_MATERIAL
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_DETALLE_MATERIAL
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_CATEGORIA_MATERIAL
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_TRAZABILIDAD_SOLICITUD
increment by 1
start with 1
nocycle
 nocache
order;

/*
--se eliminan, el consecutivo es anual
create sequence SQ_ID_SOLICITUD_RETIRO
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_SOLICITUD_REINGRESO
increment by 1
start with 1
nocycle
 nocache
order;*/


create sequence SQ_ID_TRAZABILIDAD_SOL_MAT
increment by 1
start with 1
nocycle
 nocache
order;

--04-08-16
create sequence SQ_ID_ADJUNTO_INCIDENTE
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_INCIDENTE_ALMACEN
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_TIPO_INCIDENTE
increment by 1
start with 1
 nomaxvalue
 minvalue 1
nocycle
 nocache
order;

--09-08-16
create sequence SQ_ID_TRAZABIL_SOL_TRASLADO
increment by 1
start with 1
nocycle
nocache
order;

--17-08-2016
create sequence SQ_ID_LINEA_GESTION_COMPRA
increment by 1
start with 1
nocycle
 nocache
order;

create sequence SQ_ID_TRAZABIL_GESTION_COMP
increment by 1
start with 1
nocycle
 nocache
order;

--25-08-2016
create sequence SQ_ID_GRUPO_GESTION_COMPRA
increment by 1
start with 1
nocycle
 nocache
order;

--09-09-2016
create sequence SQ_ID_DET_APROVISIONAMIENTO
increment by 1
start with 1
nocycle
 nocache
order;


create sequence SQ_ID_ADJUNTO_GESTION_COMPR
	increment by 1
	start with 1
	nocycle
	nocache
	order;

create sequence SQ_ID_TRAZABILIDAD_AJUSTE
	increment by 1
	start with 1
	nocycle
	nocache
	order;

create sequence SQ_ID_TRAZABIL_GESTION_INGR
	increment by 1
	start with 1
	nocycle
	nocache
	order;

create sequence SQ_ID_ADJUNTO_GESTION_INGR
	increment by 1
	start with 1
	nocycle
	nocache
	order;