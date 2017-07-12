/*===============================================================*/
/*                  TABLAS I ETAPA                                                                                                    */
/*25-08-2015                                                                                                                             */
/*===============================================================*/

/*==============================================================*/
/* Table: OTC_ESTADO_ORDEN_TRABAJO                              */
/*==============================================================*/
/*
Fecha: 02-11-2015
Autor: Patricia Conejo Altamirano
Descripción: Se agrega el campo CONDICION
*/
create table OTT_ANTEPROYECTO_ListarRegistrosLista.OTC_ESTADO_ORDEN_TRABAJO 
(
   ESTADO_ORDEN_TRABAJO VARCHAR2(3)          not null
      constraint CK_ESTADO_ORDEN_TRABAJO_MAYUS check (ESTADO_ORDEN_TRABAJO = upper(ESTADO_ORDEN_TRABAJO)),
   CONDICION            VARCHAR2(3)          default 'ETR' not null
      constraint CK_CONDICION_ESTADO_ORDEN check (CONDICION in ('ETR','TRA')),
   DESCRIPCION          VARCHAR2(50)         not null,
   constraint PK_OTC_ESTADO_ORDEN_TRABAJO primary key (ESTADO_ORDEN_TRABAJO)
);

comment on table OTC_ESTADO_ORDEN_TRABAJO is
'Tabla para registrar los posibles estados de una orden de trabajo';

comment on column OTC_ESTADO_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO is
'Llave primaria de la tabla OTC_ESTADO_ORDEN_TRABAJO';

comment on column OTC_ESTADO_ORDEN_TRABAJO.CONDICION is
'Condición asociada al estado de la orden de trabajo - ETR: En Trámite, TRA: Tramitada - Valor por defecto: ETR';

comment on column OTC_ESTADO_ORDEN_TRABAJO.DESCRIPCION is
'Descripción de la orden de trabajo';

/*==============================================================*/
/* Table: OTC_TIPO_ORDEN_TRABAJO                                */
/*==============================================================*/
create table OTC_TIPO_ORDEN_TRABAJO 
(
   TIPO_ORDEN_TRABAJO   VARCHAR2(3)          not null
      constraint CK_TIPO_ORDEN_TRABAJO check (TIPO_ORDEN_TRABAJO = upper(TIPO_ORDEN_TRABAJO)),
   DESCRIPCION          VARCHAR2(50)         not null,
   constraint PK_OTC_TIPO_ORDEN_TRABAJO primary key (TIPO_ORDEN_TRABAJO)
);

comment on table OTC_TIPO_ORDEN_TRABAJO is
'Tabla para registrar los tipos de ordenes de trabajo: ordinaria, emergencia, preventivo';

comment on column OTC_TIPO_ORDEN_TRABAJO.TIPO_ORDEN_TRABAJO is
'Tipo de orden de trabajo: Ordinaria, Emergencia, Preventivo';

comment on column OTC_TIPO_ORDEN_TRABAJO.DESCRIPCION is
'Descripción de tipo de orden de trabajo';

/*==============================================================*/
/* Table: OTT_ADJUNTO_ORDEN_TRABAJO                             */
/*==============================================================*/
/*
Fecha: 20-10-15
Autor: Patricia Conejo Altamirano
Descripción: Se cambia el nombre de la tabla y la llave en consecuencia con los cambios en OTF_ORDEN_TRABAJO

Fecha: 21-01-2016
Autor:Erick Figueroa 
Descripción: Se agregan los campos correspondientes a ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO,DESCRIPCION 
y se ajusta la llave de la tabla :  constraint PK_OTT_ADJUNTO_ORDEN_TRABAJO primary key (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO)

Script: 
ALTER TABLE OTT_ADJUNTO_ORDEN_TRABAJO
  ADD (   ID_TIPO_DOCUMENTO    NUMBER(10,0)         not null,
		  ID_ETAPA_ORDEN_TRABAJO NUMBER(10,0)       not null,
		  DESCRIPCION          VARCHAR2(4000)       not null);


ALTER TABLE OTT_ADJUNTO_ORDEN_TRABAJO drop constraint PK_OTT_ADJUNTO_ORDEN_TRABAJO;
ALTER TABLE OTT_ADJUNTO_ORDEN_TRABAJO drop constraint PK_OTT_ADJUNTO_ORDEN_TRABAJO primary key (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO);
comment on column OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column OTT_ADJUNTO_ORDEN_TRABAJO.DESCRIPCION is
'Descripción del contenido del archivo adjunto.';

Fecha: 09-03-2016
Autor:Erick Figueroa 
Descripción: Se agregan el campo EXPEDIENTE_TECNICO


Script: 
Alter Table OTT_ADJUNTO_ORDEN_TRABAJO ADD   EXPEDIENTE_TECNICO   NUMBER(1,0)          default 0 not null
     constraint CK_EXPEDIENTE_TECNIC_OTT_ADJU check (EXPEDIENTE_TECNICO between 0 and 1);
      
comment on column OTT_ADJUNTO_ORDEN_TRABAJO.EXPEDIENTE_TECNICO is
'Indicador para el documento asociado a la orden de trabajo, que definira si el documento pertenece o no al expediente tecnico  0: no pertenece 1: si pertenece';


*/

/*==============================================================*/
/* Table: OTT_ADJUNTO_ORDEN_TRABAJO                             */
/*==============================================================*/
create table OTT_ADJUNTO_ORDEN_TRABAJO 
(
   ID_ADJUNTO_ORDEN_TRABAJO NUMBER(10,0)     not null,
   ID_TIPO_DOCUMENTO    NUMBER(10,0)         not null,
   ID_ETAPA_ORDEN_TRABAJO NUMBER(10,0)       not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   NOMBRE_ARCHIVO       VARCHAR2(100)        not null,
   DESCRIPCION          VARCHAR2(4000)       not null,
   ARCHIVO              BLOB                 not null,
   EXPEDIENTE_TECNICO   NUMBER(1,0)          default 0 not null
      constraint CK_EXPEDIENTE_TECNIC_OTT_ADJU check (EXPEDIENTE_TECNICO between 0 and 1),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_ADJUNTO_ORDEN_TRABAJO primary key (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO)
);

comment on table OTT_ADJUNTO_ORDEN_TRABAJO is
'Tabla para registrar los documentos adjuntos, principalmente imágenes, que el solicitante puede incluir en una orden de trabajo.';

comment on column OTT_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO is
'Llave primaria de la tabla OTT_ADJUNTO_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ADJUNTO_ORDEN_TRABAJO';

comment on column OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column OTT_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO is
'Nombre del archivo adjunto.';

comment on column OTT_ADJUNTO_ORDEN_TRABAJO.DESCRIPCION is
'Descripción del contenido del archivo adjunto.';

comment on column OTT_ADJUNTO_ORDEN_TRABAJO.ARCHIVO is
'Documento adjunto.';

comment on column OTT_ADJUNTO_ORDEN_TRABAJO.EXPEDIENTE_TECNICO is
'Indicador para el documento asociado a la orden de trabajo, que definira si el documento pertenece o no al expediente tecnico  0: no pertenece 1: si pertenece';


comment on column OTT_ADJUNTO_ORDEN_TRABAJO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTT_ADJUNTO_ORDEN_TRABAJO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema.';


/*==============================================================*/
/* Table: OTF_ORDEN_TRABAJO                                     */
/*==============================================================*/
/*
Fecha: 09-09-2015
Autor: Patricia Conejo Altamirano
Descripción: Se agrega el campo ID_SECTOR_TALLER
--------------------------------------------------------------
Fecha: 16-09-2015
Autor: Patricia Conejo Altamirano
Descripción: Se agrega el campo INCLUIDA_EN_RECEPCION
--------------------------------------------------------------
Fecha: 21-09-15
Autor: Patricia Conejo Altamirano
Descripción: Se limita el tamaño de la señas exactas y la descripción del trabajo
-----------------------------------------------------------------------
Fecha: 20-10-2015
Autor: Patricia Conejo Altamirano
Descripción: Se cambia el nombre de la tabla y la llave para considerar el nuevo formato de consecutivo por sede solicitado.
-----------------------------------------------------------------------
Fecha: 21-01-2016
Autor: Erick Figueroa
Descripción: Se agrega el campo NOMBRE_PROYECTO
Script especifico: 
ALTER TABLE OTT_ORDEN_TRABAJO  ADD NOMBRE_PROYECTO  VARCHAR2(512);
 
comment on column OTT_ORDEN_TRABAJO.NOMBRE_PROYECTO is
'Nombre del proyecto en caso de tratarse de ordenes de trabajo de diseño.';
*/
/*==============================================================*/
/* Table: OTT_ORDEN_TRABAJO                                     */
/*==============================================================*/
create table OTT_ORDEN_TRABAJO 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ID_UBICACION_MADRE   NUMBER(10,0),
   ID_ORDEN_TRABAJO_MADRE VARCHAR2(18)         default '-',
   ID_MOTIVO_RECHAZO    NUMBER(2,0),
   ANNO                 NUMBER(4,0)          not null
      constraint CK_ANNO_VALOR_MINIMO_TRN check (ANNO >= 0),
   CONSECUTIVO          NUMBER(10,0)         not null
      constraint CK_CONSECUTIVO_MINIMO_TRN check (CONSECUTIVO >= 0),
   NOMBRE_PROYECTO      VARCHAR2(512),
   TIPO_ORDEN_TRABAJO   VARCHAR2(3)          not null
      constraint CK_TIPO_ORDEN_TRABAJO_MAY_TRN check (TIPO_ORDEN_TRABAJO = upper(TIPO_ORDEN_TRABAJO)),
   ESTADO_ORDEN_TRABAJO VARCHAR2(3)          not null
      constraint CK_ESTADO_ORDEN_TRAB_MAY_TRN check (ESTADO_ORDEN_TRABAJO = upper(ESTADO_ORDEN_TRABAJO)),
   NUM_EMPLEADO         INTEGER              not null,
   ID_CATEGORIA_SERVICIO NUMBER(10,0)         not null,
   ID_ACTIVIDAD         NUMBER(10,0)         not null,
   ID_LUGAR_TRABAJO     NUMBER(10,0)         not null,
   ID_SECTOR_TALLER     NUMBER(10,0),
   FECHA_HORA_SOLICITA  DATE                 default SYSDATE not null,
   COD_UNIDAD_SIRH      NUMBER(3,0)          not null,
   NOMBRE_PERSONA_CONTACTO VARCHAR2(200),
   TELEFONO             VARCHAR2(10),
   SENNAS_EXACTAS       VARCHAR2(200)        not null,
   DESCRIPCION_TRABAJO  VARCHAR2(800)        not null,
   NUMERO_ORDEN         NUMBER(10,0)        
      constraint CK_NUMERO_ORDEN_MINIMO_TRN check (NUMERO_ORDEN is null or (NUMERO_ORDEN >= 0)),
   INCLUIDA_EN_RECEPCION NUMBER(1,0)          default 0 not null
      constraint CK_INCLUIDA_EN_RECEPCION_TRN check (INCLUIDA_EN_RECEPCION in (0,1)),
   PARENTESCO           VARCHAR2(3)          default 'MAD' not null
      constraint CK_PARENTESCO_VALOR_TRN check (PARENTESCO in ('MAD','HIJ') and PARENTESCO = upper(PARENTESCO)),
   ID_UBICACION_ORIGEN  NUMBER(10,0),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_ORDEN_TRABAJO primary key (ID_UBICACION, ID_ORDEN_TRABAJO)
);

comment on table OTT_ORDEN_TRABAJO is
'Tabla para llevar el registro de las órdenes de trabajo de mantenimento y construcción que están en trámite.';

comment on column OTT_ORDEN_TRABAJO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_ORDEN_TRABAJO.ID_UBICACION_MADRE is
'Id ubicación de la ot madre';

comment on column OTT_ORDEN_TRABAJO.ID_ORDEN_TRABAJO_MADRE is
'Id de la orden de trabajo madre';

comment on column OTT_ORDEN_TRABAJO.ID_MOTIVO_RECHAZO is
'Llave primaria de la tabla OTM_MOTIVO_RECHAZO que se asocia con la secuencia SQ_ID_MOTIVO_RECHAZO';

comment on column OTT_ORDEN_TRABAJO.ANNO is
'Año de solicitud de la OT';

comment on column OTT_ORDEN_TRABAJO.CONSECUTIVO is
'Consecutivo de orden de trabajo, se reinicia cada año.';

comment on column OTT_ORDEN_TRABAJO.NOMBRE_PROYECTO is
'Nombre del proyecto en caso de tratarse de ordenes de trabajo de diseño.';

comment on column OTT_ORDEN_TRABAJO.TIPO_ORDEN_TRABAJO is
'Tipo de orden de trabajo';

comment on column OTT_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO is
'Llave primaria de la tabla OTC_ESTADO_ORDEN_TRABAJO';

comment on column OTT_ORDEN_TRABAJO.NUM_EMPLEADO is
'Número de empleado del usuario que registra la orden de trabajo';

comment on column OTT_ORDEN_TRABAJO.ID_CATEGORIA_SERVICIO is
'Llave primaria de la tabla OTM_CATEGORIA_SERVICIO que se asocia con la secuencia SQ_ID_CATEGORIA_SERVICIO';

comment on column OTT_ORDEN_TRABAJO.ID_ACTIVIDAD is
'Llave primaria de la tabla OTM_ACTIVIDAD que se asocia con la secuencia SQ_ID_ACTIVIDAD';

comment on column OTT_ORDEN_TRABAJO.ID_LUGAR_TRABAJO is
'Llave primaria de la tabla OTM_LUGAR_TRABAJO que se asocia con la secuencia SQ_ID_LUGAR_TRABAJO';

comment on column OTT_ORDEN_TRABAJO.ID_SECTOR_TALLER is
'Taller o sector asignado para atender la orden de trabajo';

comment on column OTT_ORDEN_TRABAJO.FECHA_HORA_SOLICITA is
'Fecha y hora en que el solicitante registra la orden de trabajo.';

comment on column OTT_ORDEN_TRABAJO.COD_UNIDAD_SIRH is
'Código de la unidad académica o administrativa que tramita la autorización de la orden de trabajo';

comment on column OTT_ORDEN_TRABAJO.NOMBRE_PERSONA_CONTACTO is
'Nombre completo y apellidos de la persona que podrá ser contactada para evacuar dudas de la orden de trabajo solicitada';

comment on column OTT_ORDEN_TRABAJO.TELEFONO is
'Teléfono mediante el cual se podrá contactar al solicitante o a la persona contacto';

comment on column OTT_ORDEN_TRABAJO.SENNAS_EXACTAS is
'Señas exactas del lugar donde se requiere la realización de trabajo';

comment on column OTT_ORDEN_TRABAJO.DESCRIPCION_TRABAJO is
'Descripción detallada del trabajo requerido';

comment on column OTT_ORDEN_TRABAJO.NUMERO_ORDEN is
'Número asignado a la orden de trabajo para seguimiento interno a la sección de Mantenimiento y Construcción. El número se asigna cuando se inicia la tramitación de la orden. Se toma temporalmente del sistema PDAGO.';

comment on column OTT_ORDEN_TRABAJO.INCLUIDA_EN_RECEPCION is
'Indicador de si la boleta fue incluida desde la recepción en la sección de Mantenimiento y Construcción.  - 0: NO, 1: SI - Valor por defecto: 0';

comment on column OTT_ORDEN_TRABAJO.PARENTESCO is
'Parentesco de la boleta. - MAD: Madre, HIJ: Hija - Valor por defecto MAD.';

comment on column OTT_ORDEN_TRABAJO.ID_UBICACION_ORIGEN is
'Id de la ubicación del jefe administrativo que registra la orden de trabajo';

comment on column OTT_ORDEN_TRABAJO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTT_ORDEN_TRABAJO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


/*==============================================================*/
/* Table: OTT_REVISION_ORDEN_TRABAJ                             */
/*==============================================================*/
/*
Fecha: 20-10-2015
Autor: Patricia Conejo Altamirano
Descripción: Se cambia la llave y el nombre de la tabla
*/
create table OTT_REVISION_ORDEN_TRABAJ 
(
   ID_REVISION_ORDEN_TRABAJ NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   OBSERVACIONES        VARCHAR2(2000)       not null,
   ESTADO               VARCHAR2(3)          not null
      constraint CK_ESTADO_REV_VALOR check (ESTADO in ('APR','DEV','DEN') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_REVISION_ORDEN_TRABAJ primary key (ID_REVISION_ORDEN_TRABAJ)
);

comment on table OTT_REVISION_ORDEN_TRABAJ is
'Tabla para registrar las revisiones que realiza un autorizado a la solicitud de orden de trabajo';

comment on column OTT_REVISION_ORDEN_TRABAJ.ID_REVISION_ORDEN_TRABAJ is
'Llave primaria de la tabla OTT_REVISION_ORDEN_TRABAJ que se asocia con la secuencia SQ_ID_REVISION_ORDEN_TRABAJ';

comment on column OTT_REVISION_ORDEN_TRABAJ.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_REVISION_ORDEN_TRABAJ.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_REVISION_ORDEN_TRABAJ.OBSERVACIONES is
'Observaciones indicadas por el revisor';

comment on column OTT_REVISION_ORDEN_TRABAJ.ESTADO is
'Estado del registro - APR: Aprueba, DEV: Devuelve, DEN: Deniega';

comment on column OTT_REVISION_ORDEN_TRABAJ.USUARIO is
'Usuario que realiza la revisión';

comment on column OTT_REVISION_ORDEN_TRABAJ.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_ACTIVIDAD                                         */
/*==============================================================*/
create table OTM_ACTIVIDAD 
(
   ID_CATEGORIA_SERVICIO NUMBER(10,0)         not null,
   ID_ACTIVIDAD         NUMBER(10,0)         not null,
   ID_SECTOR_TALLER     NUMBER(10,0),
   DESCRIPCION          VARCHAR2(100)        not null,
   DESCRIPCION_AMPLIADA VARCHAR2(2000)       not null,
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_ACTIVIDAD check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_ACTIVIDAD primary key (ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD),
   constraint AK_OTM_ACTIVIDAD unique (DESCRIPCION)
);

comment on table OTM_ACTIVIDAD is
'Tabla para registrar las actividades de cada categoría de servicio';

comment on column OTM_ACTIVIDAD.ID_CATEGORIA_SERVICIO is
'Llave primaria de la tabla OTM_CATEGORIA_SERVICIO que se asocia con la secuencia SQ_ID_CATEGORIA_SERVICIO';

comment on column OTM_ACTIVIDAD.ID_ACTIVIDAD is
'Llave primaria de la tabla OTM_ACTIVIDAD que se asocia con la secuencia SQ_ID_ACTIVIDAD';

comment on column OTM_ACTIVIDAD.ID_SECTOR_TALLER is
'Llave primaria de la tabla OTM_SECTOR_TALLER que se asocia con la secuencia SQ_ID_SECTOR_TALLER';

comment on column OTM_ACTIVIDAD.DESCRIPCION is
'Descripción de la actividad';

comment on column OTM_ACTIVIDAD.DESCRIPCION_AMPLIADA is
'Descripción detallada de la actividad';

comment on column OTM_ACTIVIDAD.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_ACTIVIDAD.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_CATEGORIA_SERVICIO                                */
/*==============================================================*/
/*
Fecha: 01-09-2015
Autor: Patricia Conejo Altamirano
Descripción: se agrega el campo ficha técnica
------------------------------------------------------------------------
Fecha: 28-09-2015
Autor: Patricia Conejo Altamirano
Descripción: se agrega el campo ID_UBICACION_ADMINISTRA
------------------------------------------------------------------------
Fecha: 20-10-2015
Autor: Patricia Conejo Altamirano
Descripción: se agrega el campo SIGLAS_ORDEN_TRABAJO_HIJA
------------------------------------------------------------------------
Fecha: 15-01-2016
Responsable: Erick Figueroa
Descripción: se agrega el campo OCULTAR_CATEGORIA
Script:   
ALTER TABLE OTM_CATEGORIA_SERVICIO
  ADD OCULTAR_CATEGORIA    NUMBER(1,0)          default 0 not null
  constraint CK_OCULTAR_CATEGORIA check (OCULTAR_CATEGORIA between 0 and 1);
*/
create table OTM_CATEGORIA_SERVICIO 
(
   ID_CATEGORIA_SERVICIO NUMBER(10,0)         not null,
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
OCULTAR_CATEGORIA    NUMBER(1,0)          default 0 not null
      constraint CK_OCULTAR_CATEGORIA check (OCULTAR_CATEGORIA between 0 and 1),
   NUM_EMPLEADO_SUPERVISOR INTEGER              not null,
   ID_SECTOR_TALLER     NUMBER(10,0),
   REQUIERE_FICHA_TECNICA NUMBER(1,0)          default 0 not null
      constraint CK_REQUIERE_FICHA_TECNICA check (REQUIERE_FICHA_TECNICA in (0,1)),
   DESCRIPCION          VARCHAR2(100)        not null,
      SIGLAS_ORDEN_TRABAJO_HIJA VARCHAR2(3)          default '---' not null
      constraint CK_SIGLAS_ORDEN_TRABAJO check (SIGLAS_ORDEN_TRABAJO_HIJA = upper(SIGLAS_ORDEN_TRABAJO_HIJA)),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_CATEGORIA check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_CATEGORIA_SERVICIO primary key (ID_CATEGORIA_SERVICIO),
   constraint AK_OTM_CATEGORIA_SERVICIO unique (DESCRIPCION)
);

comment on table OTM_CATEGORIA_SERVICIO is
'Tabla para registar las categorías de los servicios que ofrece la sección de Mantenimiento y Construcción para que sean solicitados a través de una orden de trabajo.';

comment on column OTM_CATEGORIA_SERVICIO.ID_CATEGORIA_SERVICIO is
'Llave primaria de la tabla OTM_CATEGORIA_SERVICIO que se asocia con la secuencia SQ_ID_CATEGORIA_SERVICIO';

comment on column OTM_CATEGORIA_SERVICIO.OCULTAR_CATEGORIA is
'Indicador para mostrar o ocultar la categoria a los usuarios externos de la aplicación. - 0: NO, 1: SI - Valor por defecto: 0';

comment on column OTM_CATEGORIA_SERVICIO.ID_UBICACION_ADMINISTRA is
'Id de la ubicación que administra los datos del catálogo';

comment on column OTM_CATEGORIA_SERVICIO.NUM_EMPLEADO_SUPERVISOR is
'Número de empleado supervisor para las boletas de la categoría indicada';

comment on column OTM_CATEGORIA_SERVICIO.ID_SECTOR_TALLER is
'Llave primaria de la tabla OTM_SECTOR_TALLER que se asocia con la secuencia SQ_ID_SECTOR_TALLER';

comment on column OTM_CATEGORIA_SERVICIO.REQUIERE_FICHA_TECNICA is
'Indicador de si requiere ficha técnica - 0: NO, 1: SI - Valor por defecto: 0';

comment on column OTM_CATEGORIA_SERVICIO.DESCRIPCION is
'Descripción de la categoría de servicio';

comment on column OTM_CATEGORIA_SERVICIO.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_CATEGORIA_SERVICIO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_LUGAR_TRABAJO                                     */
/*==============================================================*/
/*
Fecha: 08-09-2015
Autor: Patricia Conejo Altamirano
Descripción: Se agrega el campo ESTADO
_____________________________________________________________
Fecha: 28-09-2015
Autor: Patricia Conejo Altamirano
Descripción: Se agrega el campo ID_UBICACION_ADMINISTRA
se modifica el nombre de ID_UBICACION a ID_UBICACION_PERTENECE
*/
create table OTM_LUGAR_TRABAJO 
(
   ID_LUGAR_TRABAJO     NUMBER(10,0)         not null,
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   ID_UBICACION_PERTENECE NUMBER(10,0)         not null,
   ID_SECTOR_TALLER     NUMBER(10,0)         not null,
   ID_TIPO_LUGAR_UBICACION NUMBER(10,0)         not null,
   NOMBRE               VARCHAR2(150)        not null,
   CLASIFICACION        VARCHAR2(3)          default 'EDI' not null
      constraint CK_CLASIFICACION_VALOR check (CLASIFICACION in ('EDI','SIT') and CLASIFICACION = upper(CLASIFICACION)),
   COD_UNIDAD_SIRH      NUMBER(3,0),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_LUGAR_TRABAJO check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_LUGAR_TRABAJO primary key (ID_LUGAR_TRABAJO),
   constraint AK_OTM_LUGAR_TRABAJO unique (NOMBRE)
)
/

comment on table OTM_LUGAR_TRABAJO is
'Tabla para registrar los lugares de realización de trabajos de mantenimiento y construcción: edificios o sitios.'
/

comment on column OTM_LUGAR_TRABAJO.ID_LUGAR_TRABAJO is
'Llave primaria de la tabla OTM_LUGAR_TRABAJO que se asocia con la secuencia SQ_ID_LUGAR_TRABAJO'
/

comment on column OTM_LUGAR_TRABAJO.ID_UBICACION_ADMINISTRA is
'Id de la ubicación que administra los datos del catálogo'
/

comment on column OTM_LUGAR_TRABAJO.ID_UBICACION_PERTENECE is
'Id de la ubicación a la que pertenece el lugar de trabajo.';


comment on column OTM_LUGAR_TRABAJO.ID_SECTOR_TALLER is
'Llave primaria de la tabla OTM_SECTOR_TALLER que se asocia con la secuencia SQ_ID_SECTOR_TALLER'
/

comment on column OTM_LUGAR_TRABAJO.ID_TIPO_LUGAR_UBICACION is
'Llave primaria de la tabla OTM_TIPO_LUGAR_UBICACION que se asocia con la secuencia SQ_ID_TIPO_LUGAR_UBICACION'
/

comment on column OTM_LUGAR_TRABAJO.NOMBRE is
'Nombre del lugar de trabajo'
/

comment on column OTM_LUGAR_TRABAJO.CLASIFICACION is
'Clasificación del lugar de trabajo - EDI: Edificio, SIT: Sitio - Valor por defecto: EDI'
/

comment on column OTM_LUGAR_TRABAJO.COD_UNIDAD_SIRH is
'Código de la unidad académica o administrativa responsable del lugar'
/

comment on column OTM_LUGAR_TRABAJO.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT'
/

comment on column OTM_LUGAR_TRABAJO.USUARIO is
'Usuario que crea o modifica el registro.'
/

comment on column OTM_LUGAR_TRABAJO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema'
/

/*==============================================================*/
/* Table: OTM_SECTOR_TALLER                                     */
/*==============================================================*/
/*
Fecha: 02-09-2015
Autor: Patricia Conejo Altamirano
Descripción: Se elimina el campo REQUIERE_FICHA_TECNICA
-------------------------------------------------------------------------
Fecha: 09-09-2015
Autor: Patricia Conejo Altamirano
Descripción: Se agrega el coordinador y el sustituto
-------------------------------------------------------------------------
Fecha: 28-09-2015
Autor: Patricia Conejo Altamirano
Descripción: Se agrega el campo ID_UBICACION_ADMINISTRA
-------------------------------------------------------------------------
Fecha: 23-11-2015
Autor: Carlos Gómez Ondoy
Descripción: Se modifica la restricción AK
*/
create table OTM_SECTOR_TALLER 
(
   ID_SECTOR_TALLER     NUMBER(10,0)         not null,
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   NUM_EMPLEADO_COORDINADOR INTEGER              not null,
   NUM_EMPLEADO_SUSTITUTO INTEGER,
   NOMBRE               VARCHAR2(100)        not null,
   TIPO_AREA            VARCHAR2(3)          default 'SEC' not null
      constraint CK_TIPO_AREA check (TIPO_AREA in ('SEC','TAL') and TIPO_AREA = upper(TIPO_AREA)),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_SECTOR check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_SECTOR_TALLER primary key (ID_SECTOR_TALLER),
   constraint AK_OTM_SECTOR unique (NOMBRE,ID_UBICACION_ADMINISTRA)
);

comment on table OTM_SECTOR_TALLER is
'Tabla para registrar los sectores o talleres que corresponden a grupos de trabajo dentro de la sección de Mantenimiento y Construcción que tienen a cargo divisiones o áreas dentro del campus universitario.';

comment on column OTM_SECTOR_TALLER.ID_SECTOR_TALLER is
'Llave primaria de la tabla OTM_SECTOR_TALLER que se asocia con la secuencia SQ_ID_SECTOR_TALLER';

comment on column OTM_SECTOR_TALLER.ID_UBICACION_ADMINISTRA is
'Id de la ubicación que administra los datos del catálogo';

comment on column OTM_SECTOR_TALLER.NUM_EMPLEADO_COORDINADOR is
'Número de empleado del coordinador del sector o taller';

comment on column OTM_SECTOR_TALLER.NUM_EMPLEADO_SUSTITUTO is
'Número de empleado del sustituto del coordinador';

comment on column OTM_SECTOR_TALLER.NOMBRE is
'Nombre del sector';

comment on column OTM_SECTOR_TALLER.TIPO_AREA is
'Tipo de área - SEC: Sector, TAL: Taller -  Valor por defecto SEC';

comment on column OTM_SECTOR_TALLER.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_SECTOR_TALLER.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';



/*==============================================================*/
/* Table: OTM_TIPO_LUGAR_UBICACION                              */
/*==============================================================*/
create table OTM_TIPO_LUGAR_UBICACION 
(
   ID_TIPO_LUGAR_UBICACION NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_TIPO_LUGAR_UBICACION check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_TIPO_LUGAR_UBICACION primary key (ID_TIPO_LUGAR_UBICACION),
   constraint AK_OTM_TIPO_LUGAR_UBICACION unique (DESCRIPCION)
);

comment on table OTM_TIPO_LUGAR_UBICACION is
'Tabla para registrar los tipos de lugar de ubicación: sede, recinto, finca, estación experimental, etc.';

comment on column OTM_TIPO_LUGAR_UBICACION.ID_TIPO_LUGAR_UBICACION is
'Llave primaria de la tabla OTM_TIPO_LUGAR_UBICACION que se asocia con la secuencia SQ_ID_TIPO_LUGAR_UBICACION';

comment on column OTM_TIPO_LUGAR_UBICACION.DESCRIPCION is
'Descripción del lugar de ubicación';

comment on column OTM_TIPO_LUGAR_UBICACION.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_TIPO_LUGAR_UBICACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_UBICACION                                         */
/*==============================================================*/
create table OTM_UBICACION 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   PERTENECE_A_SEDE     NUMBER(1,0)          default 0 not null
      constraint CK_PERTENECE_A_SEDE check (PERTENECE_A_SEDE in (0,1)),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_UBICACION check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_UBICACION primary key (ID_UBICACION),
   constraint AK_OTM_UBICACION unique (DESCRIPCION)
);

comment on table OTM_UBICACION is
'Tabla para registar la ubicación del lugar de realización de un trabajo, en términos de áreas que conforman la universidad: sedes, recintos, estaciones experimentales, otros.';

comment on column OTM_UBICACION.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTM_UBICACION.DESCRIPCION is
'Descripción de la ubicación';

comment on column OTM_UBICACION.PERTENECE_A_SEDE is
'Indicador de si la ubicación pertenece a una sede - 0.NO, 1.SI - Valor por defecto: 0';

comment on column OTM_UBICACION.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_UBICACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

--02-09-2015
/*==============================================================*/
/* Table: OTP_PARAMETRO                                         */
/*==============================================================*/
/*
Fecha:29-09-2015
Autor: Patricia Conejo Altamirano
Descripción: Cambio en el nombre de la tabla, en la llave y el comentario
*/
create table OTP_PARAMETRO_UBICACION 
(
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   ID_PARAMETRO         NUMBER(10,0)           not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   VALOR                VARCHAR2(256)        not null,
   VALOR_DECIMAL        NUMBER(13,2)         not null,
   PROTEGIDO            NUMBER(1,0)          default 0 not null
      constraint CK_PROTEGIDO_VALOR check (PROTEGIDO in (0,1)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTP_PARAMETRO_UBICACION primary key (ID_UBICACION_ADMINISTRA, ID_PARAMETRO),
   constraint AK_OTP_PARAMETRO unique (DESCRIPCION)
);

comment on table OTP_PARAMETRO_UBICACION is
'Tabla para registrar los parámetros del sistema aplicables a cada sede';

comment on column OTP_PARAMETRO_UBICACION.ID_PARAMETRO is
'Llave primaria de la tabla OTP_PARAMETRO';

comment on column OTP_PARAMETRO_UBICACION.DESCRIPCION is
'Descripción del parámetro';

comment on column OTP_PARAMETRO_UBICACION.VALOR is
'Valor del parámetro';

comment on column OTP_PARAMETRO_UBICACION.VALOR_DECIMAL is
'Valor de tipo decimal del parámetro';

comment on column OTP_PARAMETRO_UBICACION.PROTEGIDO is
'Indicador que especifica si el parámetro se encuentra protegido y no se puede modificar desde un mantenimiento - 0: No es protegido, 1: Es protegido - Valor por defecto: 0';

comment on column OTP_PARAMETRO_UBICACION.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTP_PARAMETRO_UBICACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


--09-09-2015
/*==============================================================*/
/* Table: OTF_OPERARIO                                          */
/*==============================================================*/
create table OTF_OPERARIO 
(
   ID_SECTOR_TALLER     NUMBER(10,0)         not null,
   NUM_EMPLEADO         INTEGER              not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTF_OPERARIO primary key (ID_SECTOR_TALLER, NUM_EMPLEADO),
   constraint AK_OTF_OPERARIO unique (NUM_EMPLEADO)
);

comment on table OTF_OPERARIO is
'Tabla para registrar los operarios de un sector o taller';

comment on column OTF_OPERARIO.ID_SECTOR_TALLER is
'Llave primaria de la tabla OTM_SECTOR_TALLER que se asocia con la secuencia SQ_ID_SECTOR_TALLER';

comment on column OTF_OPERARIO.NUM_EMPLEADO is
'Número de empleado del operario.';

comment on column OTF_OPERARIO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTF_OPERARIO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTF_PLANEACION_PREVENTIVO                             */
/*==============================================================*/
/*
Fecha: 29-06-2015
Autor: Patricia Conejo Altamirano
Descripción: se agrega ID_UBICACION_ADMINISTRA y se modifica la llave
*/
create table OTF_PLANEACION_PREVENTIVO 
(
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   CONSECUTIVO_PROPUESTO NUMBER(10,0)         not null,
   ID_CATEGORIA_SERVICIO NUMBER(10,0)         not null,
   ID_ACTIVIDAD         NUMBER(10,0)         not null,
   ID_LUGAR_TRABAJO     NUMBER(10,0)         not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTF_PLANEACION_PREVENTIVO primary key (ID_UBICACION_ADMINISTRA, CONSECUTIVO_PROPUESTO)
);

comment on table OTF_PLANEACION_PREVENTIVO is
'Tabla para registrar la planeación anual de ordenes de trabajo preventivo de la Sección de Mantenimiento y Construcción';

comment on column OTF_PLANEACION_PREVENTIVO.ID_UBICACION_ADMINISTRA is
'Id de la ubicación que administra los datos del catálogo';

comment on column OTF_PLANEACION_PREVENTIVO.CONSECUTIVO_PROPUESTO is
'Consecutivo propuesto en la planificación para la generación de la orden de trabajo';

comment on column OTF_PLANEACION_PREVENTIVO.ID_CATEGORIA_SERVICIO is
'Llave primaria de la tabla OTM_CATEGORIA_SERVICIO que se asocia con la secuencia SQ_ID_CATEGORIA_SERVICIO';

comment on column OTF_PLANEACION_PREVENTIVO.ID_ACTIVIDAD is
'Llave primaria de la tabla OTM_ACTIVIDAD que se asocia con la secuencia SQ_ID_ACTIVIDAD';

comment on column OTF_PLANEACION_PREVENTIVO.ID_LUGAR_TRABAJO is
'Llave primaria de la tabla OTM_LUGAR_TRABAJO que se asocia con la secuencia SQ_ID_LUGAR_TRABAJO';

comment on column OTF_PLANEACION_PREVENTIVO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTF_PLANEACION_PREVENTIVO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTL_TRAZABILIDAD_PROCESO                              */
/*==============================================================*/
/*
Fecha: 21-09-2015
Autor: Patricia Conejo Altamirano
Descripción: Se elimina el campo FECHA_HORA_REGISTRO y se  modifica las observaciones para permitir nulos
--------------------------------------------------------------------------------------------
Fecha: 20-10-2015
Autor: Patricia Conejo Altamirano
Descripción: Se cambia el nombre de la tabla y la llave en consecuencia con los cambios en OTF_ORDEN_TRABAJO
---------------------------------------------------------------------------
Fecha:30-10-2015
Autor:Patricia Conejo Altamirano
Descripción: se agregan los campos ID_MOTIVO_RECHAZO Y OBSERVACIONES_INTERNAS.
*/
/*==============================================================*/
/* Table: OTT_TRAZABILIDAD_PROCESO                              */
/*==============================================================*/
create table OTT_TRAZABILIDAD_PROCESO 
(
   ID_TRAZABILIDAD_PROCESO NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   NUM_EMPLEADO_EJECUTA INTEGER,
   ESTADO_ORDEN_TRABAJO VARCHAR2(3)          not null
      constraint CK_ESTADO_ORDEN_TRAZABIL_TRN check (ESTADO_ORDEN_TRABAJO = upper(ESTADO_ORDEN_TRABAJO)),
   FECHA_HORA_EJECUCION DATE                 not null,
   OBSERVACIONES        VARCHAR2(2000),
   ID_MOTIVO_RECHAZO    NUMBER(2,0),
   OBSERVACIONES_INTERNAS VARCHAR2(2000),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_TRAZABILIDAD_PROCESO primary key (ID_TRAZABILIDAD_PROCESO)
);

comment on table OTT_TRAZABILIDAD_PROCESO is
'Tabla para registrar la trazabilidad del proceso de registro, ejecución y liquidación de una orden de trabajo.';

comment on column OTT_TRAZABILIDAD_PROCESO.ID_TRAZABILIDAD_PROCESO is
'Llave primaria de la tabla OTT_TRAZABILIDAD_PROCESO que se asocia con la secuencia SQ_ID_TRAZABILIDAD_PROCESO';

comment on column OTT_TRAZABILIDAD_PROCESO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_TRAZABILIDAD_PROCESO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_TRAZABILIDAD_PROCESO.NUM_EMPLEADO_EJECUTA is
'Número de empleado responsable de la ejecución';

comment on column OTT_TRAZABILIDAD_PROCESO.ESTADO_ORDEN_TRABAJO is
'Estado asignado a la orden de trabajo en el paso realizado';

comment on column OTT_TRAZABILIDAD_PROCESO.FECHA_HORA_EJECUCION is
'Fecha de ejecución del paso indicado por el responsable (revisión, liquidación, etc.)';

comment on column OTT_TRAZABILIDAD_PROCESO.OBSERVACIONES is
'Observaciones indicadas por el responsable';

comment on column OTT_TRAZABILIDAD_PROCESO.ID_MOTIVO_RECHAZO is
'En caso de rechazo, el motivo indicado en este paso del flujo. Puede haber múltiples rechazos en un flujo.';

comment on column OTT_TRAZABILIDAD_PROCESO.OBSERVACIONES_INTERNAS is
'Observaciones del responsable para procesos internos';

comment on column OTT_TRAZABILIDAD_PROCESO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTT_TRAZABILIDAD_PROCESO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


--21-09-2015
/*==============================================================*/
/* Table: OTM_MOTIVO_RECHAZO                                    */
/*==============================================================*/
create table OTM_MOTIVO_RECHAZO 
(
   ID_MOTIVO_RECHAZO    NUMBER(2,0)          not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_MOTIVO_RECHAZO_VAL check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_MOTIVO_RECHAZO primary key (ID_MOTIVO_RECHAZO),
   constraint AK_OTM_MOTIVO_RECHAZO  unique (DESCRIPCION)
);

comment on table OTM_MOTIVO_RECHAZO is
'Tabla para registrar los posibles motivos por los que una orden de trabajo puede ser rechazada desde un taller o sector';

comment on column OTM_MOTIVO_RECHAZO.ID_MOTIVO_RECHAZO is
'Llave primaria de la tabla OTM_MOTIVO_RECHAZO que se asocia con la secuencia SQ_ID_MOTIVO_RECHAZO';

comment on column OTM_MOTIVO_RECHAZO.DESCRIPCION is
'Descripción del motivo de rechazo';

comment on column OTM_MOTIVO_RECHAZO.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_MOTIVO_RECHAZO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_MOTIVO_RECHAZO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


/*==============================================================*/
/* Table: OTM_AUTORIZADO_DIRECTOR                               */
/*==============================================================*/
create table OTM_AUTORIZADO_DIRECTOR 
(
   COD_UNIDAD_SIRH      NUMBER(3,0)          not null,
   NUM_EMPLEADO         INTEGER              not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_AUTORIZADO_DIRECTOR primary key (NUM_EMPLEADO, COD_UNIDAD_SIRH)
);

comment on table OTM_AUTORIZADO_DIRECTOR is
'Tabla para registrar los funcionarios autorizados a revisar ordenes de trabajo por los directores de cada unidad.';

comment on column OTM_AUTORIZADO_DIRECTOR.COD_UNIDAD_SIRH is
'Código de la unidad donde estará autorizado el funcionario';

comment on column OTM_AUTORIZADO_DIRECTOR.NUM_EMPLEADO is
'Número del empleado autorizado';

comment on column OTM_AUTORIZADO_DIRECTOR.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_AUTORIZADO_DIRECTOR.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

--24-09-2015
/*==============================================================*/
/* Table: OTM_AUTORIZADO_UBICACION                              */
/*==============================================================*/
/*
Fecha: 05-10-15
Autor: Patricia Conejo Altamirano
Descripción: Se cambia el nombre del campo ID_UBICACION a ID_UBICACION_ADMINISTR
*/
create table OTM_AUTORIZADO_UBICACION 
(
   ID_UBICACION_ADMINISTRA         NUMBER(10,0)         not null,
   NUM_EMPLEADO         INTEGER              not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_AUTORIZADO_UBICACION primary key (ID_UBICACION, NUM_EMPLEADO),
   constraint AK_OTM_AUTORIZADO_UBICACION unique (NUM_EMPLEADO)
);

comment on table OTM_AUTORIZADO_UBICACION is
'Tabla para registrar los funcionarios autorizados para gestionar el sistema de ordenes de compra por sede.';

comment on column OTM_AUTORIZADO_UBICACION.ID_UBICACION_ADMINISTRA is
'Id de la ubicación encargada de la administración';

comment on column OTM_AUTORIZADO_UBICACION.NUM_EMPLEADO is
'Número de empleado autorizado en la sede';

comment on column OTM_AUTORIZADO_UBICACION.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_AUTORIZADO_UBICACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

--28-09-2015
/*==============================================================*/
/* Table: OTM_UNIDAD_UBICACION                                  */
/*==============================================================*/
create table OTM_UNIDAD_UBICACION 
(
   COD_UNIDAD_SIRH      NUMBER(3,0)          not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_UNIDAD_UBICACION primary key (COD_UNIDAD_SIRH)
);

comment on table OTM_UNIDAD_UBICACION is
'Tabla para registrar las unidades por ubicación';

comment on column OTM_UNIDAD_UBICACION.COD_UNIDAD_SIRH is
'Código de la unidad académica o administrativa';

comment on column OTM_UNIDAD_UBICACION.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTM_UNIDAD_UBICACION.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_UNIDAD_UBICACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

--29-09-2015
/*==============================================================*/
/* Table: OTP_PARAMETRO_GLOBAL                                  */
/*==============================================================*/
create table OTP_PARAMETRO_GLOBAL 
(
   ID_PARAMETRO         NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   VALOR                VARCHAR2(256)        not null,
   VALOR_DECIMAL        NUMBER(13,2)         not null,
   PROTEGIDO            NUMBER(1,0)          default 0 not null
      constraint CK_PROTEGIDO_PAR_GLOBAL check (PROTEGIDO in (0,1)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTP_PARAMETRO_GLOBAL primary key (ID_PARAMETRO)
);

comment on table OTP_PARAMETRO_GLOBAL is
'Tabla para registar los parámetros del sistema que son globales a todas las sedes.';

comment on column OTP_PARAMETRO_GLOBAL.ID_PARAMETRO is
'Llave primaria de la tabla OTP_PARAMETRO';

comment on column OTP_PARAMETRO_GLOBAL.DESCRIPCION is
'Descripción del parámetro';

comment on column OTP_PARAMETRO_GLOBAL.VALOR is
'Valor del parámetro';

comment on column OTP_PARAMETRO_GLOBAL.VALOR_DECIMAL is
'Valor de tipo decimal del parámetro';

comment on column OTP_PARAMETRO_GLOBAL.PROTEGIDO is
'Indicador que especifica si el parámetro se encuentra protegido y no se puede modificar desde un mantenimiento - 0: No es protegido, 1: Es protegido - Valor por defecto: 0';

comment on column OTP_PARAMETRO_GLOBAL.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTP_PARAMETRO_GLOBAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

--02-10-15

/*==============================================================*/
/* Table: OTF_UBICACION_FAVORITA                                */
/*==============================================================*/
create table OTF_UBICACION_FAVORITA 
(
   NUM_EMPLEADO         INTEGER              not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   constraint PK_OTF_UBICACION_FAVORITA primary key (NUM_EMPLEADO)
);

comment on table OTF_UBICACION_FAVORITA is
'Tabla para registrar la ubicación favorita de un funcionario para el filtrado de información y configuración del sistema';

comment on column OTF_UBICACION_FAVORITA.NUM_EMPLEADO is
'Número de empleado';

comment on column OTF_UBICACION_FAVORITA.ID_UBICACION is
'Id de ubicación de trabajo del empleado';

/*---------------------------------------------------------------------------------------*/
--26-10-2015
/*---------------------------------------------------------------------------------------*/
/*==============================================================*/
/* Table: OTH_ORDEN_TRABAJO                                     */
/*==============================================================*/
/*
-----------------------------------------------------------------------
Fecha: 21-01-2016
Autor: Erick Figueroa
Descripción: Se agrega el campo NOMBRE_PROYECTO
Script especifico: 
ALTER TABLE OTH_ORDEN_TRABAJO  ADD NOMBRE_PROYECTO  VARCHAR2(512);
 
comment on column OTH_ORDEN_TRABAJO.NOMBRE_PROYECTO is
'Nombre del proyecto en caso de tratarse de ordenes de trabajo de diseño.';
*/
create table OTH_ORDEN_TRABAJO 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ID_UBICACION_MADRE   NUMBER(10,0),
   ID_ORDEN_TRABAJO_MADRE VARCHAR2(18)         default '-',
   ANNO                 NUMBER(4,0)          not null
      constraint CK_ANNO_VALOR_HISTORICO check (ANNO >= 0),
   CONSECUTIVO          NUMBER(10,0)         not null,
   NOMBRE_PROYECTO      VARCHAR2(512),
   TIPO_ORDEN_TRABAJO   VARCHAR2(3)          not null
      constraint CK_TIPO_ORDEN_TRABAJO_HIST check (TIPO_ORDEN_TRABAJO = upper(TIPO_ORDEN_TRABAJO)),
   ESTADO_ORDEN_TRABAJO VARCHAR2(3)          not null
      constraint CK_ESTADO_ORDEN_TRABAJO_HIST check (ESTADO_ORDEN_TRABAJO = upper(ESTADO_ORDEN_TRABAJO)),
   NUM_EMPLEADO         INTEGER              not null,
   ID_CATEGORIA_SERVICIO NUMBER(10,0)         not null,
   ID_ACTIVIDAD         NUMBER(10,0)         not null,
   ID_LUGAR_TRABAJO     NUMBER(10,0)         not null,
   ID_SECTOR_TALLER     NUMBER(10,0),
   FECHA_HORA_SOLICITA  DATE                 default SYSDATE not null,
   COD_UNIDAD_SIRH      NUMBER(3,0)          not null,
   NOMBRE_PERSONA_CONTACTO VARCHAR2(200),
   TELEFONO             VARCHAR2(10),
   SENNAS_EXACTAS       VARCHAR2(1000)       not null,
   DESCRIPCION_TRABAJO  VARCHAR2(2000)       not null,
   NUMERO_ORDEN         NUMBER(10,0)        
      constraint CK_NUMERO_ORDEN_VALOR_HIST check (NUMERO_ORDEN is null or (NUMERO_ORDEN >= 0)),
   INCLUIDA_EN_RECEPCION NUMBER(1,0)          default 0 not null,
   PARENTESCO           VARCHAR2(3)          default 'MAD' not null
      constraint CK_PARENTESCO_VALOR_HISTORICO check (PARENTESCO in ('MAD','HIJ') and PARENTESCO = upper(PARENTESCO)),
   ID_MOTIVO_RECHAZO    NUMBER(2,0),
   ID_UBICACION_ORIGEN  NUMBER(10,0),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTH_ORDEN_TRABAJO primary key (ID_UBICACION, ID_ORDEN_TRABAJO)
);

comment on table OTH_ORDEN_TRABAJO is
'Tabla para llevar el registro de las órdenes de trabajo de mantenimento y construcción que están tramitadas (liquidadas).';

comment on column OTH_ORDEN_TRABAJO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_ORDEN_TRABAJO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_ORDEN_TRABAJO.ID_UBICACION_MADRE is
'Id ubicación de la orden de trabajo madre';

comment on column OTH_ORDEN_TRABAJO.ID_ORDEN_TRABAJO_MADRE is
'Id de la orden de trabajo madre';

comment on column OTH_ORDEN_TRABAJO.ANNO is
'Año de solicitud de la OT';

comment on column OTH_ORDEN_TRABAJO.CONSECUTIVO is
'Consecutivo de orden de trabajo, se reinicia cada año.';

comment on column OTH_ORDEN_TRABAJO.NOMBRE_PROYECTO is
'Nombre del proyecto en caso de tratarse de ordenes de trabajo de diseño.';

comment on column OTH_ORDEN_TRABAJO.TIPO_ORDEN_TRABAJO is
'Tipo de orden de trabajo: Ordinaria, Emergencia, Preventivo';

comment on column OTH_ORDEN_TRABAJO.ESTADO_ORDEN_TRABAJO is
'Llave primaria de la tabla OTC_ESTADO_ORDEN_TRABAJO';

comment on column OTH_ORDEN_TRABAJO.NUM_EMPLEADO is
'Número de empleado del usuario que registra la orden de trabajo';

comment on column OTH_ORDEN_TRABAJO.ID_CATEGORIA_SERVICIO is
'Llave primaria de la tabla OTM_CATEGORIA_SERVICIO que se asocia con la secuencia SQ_ID_CATEGORIA_SERVICIO';

comment on column OTH_ORDEN_TRABAJO.ID_ACTIVIDAD is
'Llave primaria de la tabla OTM_ACTIVIDAD que se asocia con la secuencia SQ_ID_ACTIVIDAD';

comment on column OTH_ORDEN_TRABAJO.ID_LUGAR_TRABAJO is
'Llave primaria de la tabla OTM_LUGAR_TRABAJO que se asocia con la secuencia SQ_ID_LUGAR_TRABAJO';

comment on column OTH_ORDEN_TRABAJO.ID_SECTOR_TALLER is
'Llave primaria de la tabla OTM_SECTOR_TALLER que se asocia con la secuencia SQ_ID_SECTOR_TALLER';

comment on column OTH_ORDEN_TRABAJO.FECHA_HORA_SOLICITA is
'Fecha y hora en que el solicitante registra la orden de trabajo.';

comment on column OTH_ORDEN_TRABAJO.COD_UNIDAD_SIRH is
'Código de la unidad académica o administrativa que tramita la autorización de la orden de trabajo';

comment on column OTH_ORDEN_TRABAJO.NOMBRE_PERSONA_CONTACTO is
'Nombre completo y apellidos de la persona que podrá ser contactada para evacuar dudas de la orden de trabajo solicitada';

comment on column OTH_ORDEN_TRABAJO.TELEFONO is
'Teléfono mediante el cual se podrá contactar al solicitante o a la persona contacto';

comment on column OTH_ORDEN_TRABAJO.SENNAS_EXACTAS is
'Señas exactas del lugar donde se requiere la realización de trabajo';

comment on column OTH_ORDEN_TRABAJO.DESCRIPCION_TRABAJO is
'Descripción detallada del trabajo requerido';

comment on column OTH_ORDEN_TRABAJO.NUMERO_ORDEN is
'Número asignado a la orden de trabajo para seguimiento interno a la sección de Mantenimiento y Construcción. El número se asigna cuando se inicia la tramitación de la orden.';

comment on column OTH_ORDEN_TRABAJO.INCLUIDA_EN_RECEPCION is
'Indicador de si la boleta fue incluida desde la recepción en la sección de Mantenimiento y Construcción.';

comment on column OTH_ORDEN_TRABAJO.PARENTESCO is
'Parentesco de la boleta. - MAD: Madre, HIJ: Hija - Valor por defecto MAD.';

comment on column OTH_ORDEN_TRABAJO.ID_MOTIVO_RECHAZO is
'Llave primaria de la tabla OTM_MOTIVO_RECHAZO que se asocia con la secuencia SQ_ID_MOTIVO_RECHAZO';

comment on column OTH_ORDEN_TRABAJO.ID_UBICACION_ORIGEN is
'Id de la ubicación del jefe administrativo que registra la orden de trabajo';

comment on column OTH_ORDEN_TRABAJO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTH_ORDEN_TRABAJO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTH_ADJUNTO_ORDEN_TRABAJO                             */
/*==============================================================*/
/*
Fecha: 21-01-2016
Autor:Erick Figueroa 
Descripción: Se agregan los campos correspondientes a ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO,DESCRIPCION 
y se ajusta la llave de la tabla :  constraint PK_OTT_ADJUNTO_ORDEN_TRABAJO primary key (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO)

Script: 
ALTER TABLE OTT_ADJUNTO_ORDEN_TRABAJO
  ADD (   ID_TIPO_DOCUMENTO    NUMBER(10,0)         not null,
		  ID_ETAPA_ORDEN_TRABAJO NUMBER(10,0)       not null,
		  DESCRIPCION          VARCHAR2(4000)       not null);


ALTER TABLE OTT_ADJUNTO_ORDEN_TRABAJO drop constraint PK_OTT_ADJUNTO_ORDEN_TRABAJO;
ALTER TABLE OTT_ADJUNTO_ORDEN_TRABAJO drop constraint PK_OTT_ADJUNTO_ORDEN_TRABAJO primary key (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO);
comment on column OTT_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column OTT_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column OTT_ADJUNTO_ORDEN_TRABAJO.DESCRIPCION is
'Descripción del contenido del archivo adjunto.';

Fecha: 09-03-2016
Autor:Erick Figueroa 
Descripción: Se agregan el campo EXPEDIENTE_TECNICO


Script: 
 Alter Table OTH_ADJUNTO_ORDEN_TRABAJO ADD EXPEDIENTE_TECNICO   NUMBER(1,0)          default 0 not null
      constraint CK_EXPEDIENTE_TECNIC_OTH_ADJU check (EXPEDIENTE_TECNICO between 0 and 1);
      
      comment on column OTH_ADJUNTO_ORDEN_TRABAJO.EXPEDIENTE_TECNICO is
'Indicador para el documento asociado a la orden de trabajo, que definira si el documento pertenece o no al expediente tecnico  0: no pertenece 1: si pertenece';

*/

/*==============================================================*/
/* Table: OTH_ADJUNTO_ORDEN_TRABAJO                             */
/*==============================================================*/
create table OTH_ADJUNTO_ORDEN_TRABAJO 
(
   ID_ADJUNTO_ORDEN_TRABAJO NUMBER(10,0)         not null,
   ID_TIPO_DOCUMENTO    NUMBER(10,0)         not null,
   ID_ETAPA_ORDEN_TRABAJO NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   NOMBRE_ARCHIVO       VARCHAR2(100)        not null,
   DESCRIPCION          VARCHAR2(4000)       not null,
   ARCHIVO              BLOB                 not null,
   EXPEDIENTE_TECNICO   NUMBER(1,0)          default 0 not null
      constraint CK_EXPEDIENTE_TECNIC_OTH_ADJU check (EXPEDIENTE_TECNICO between 0 and 1),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTH_ADJUNTO_ORDEN_TRABAJO primary key (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO)
);

comment on table OTH_ADJUNTO_ORDEN_TRABAJO is
'Tabla histórica para registrar los documentos adjuntos, principalmente imágenes, que el solicitante pudo haber incluido en una orden de trabajo tramitada';

comment on column OTH_ADJUNTO_ORDEN_TRABAJO.ID_ADJUNTO_ORDEN_TRABAJO is
'Llave primaria de la tabla OTF_ADJUNTO_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ADJUNTO_ORDEN_TRABAJO';

comment on column OTH_ADJUNTO_ORDEN_TRABAJO.ID_TIPO_DOCUMENTO is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column OTH_ADJUNTO_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column OTH_ADJUNTO_ORDEN_TRABAJO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_ADJUNTO_ORDEN_TRABAJO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_ADJUNTO_ORDEN_TRABAJO.NOMBRE_ARCHIVO is
'Nombre del archivo adjunto';

comment on column OTH_ADJUNTO_ORDEN_TRABAJO.DESCRIPCION is
'Descripción del contenido del archivo adjunto.';

comment on column OTH_ADJUNTO_ORDEN_TRABAJO.ARCHIVO is
'Documento adjunto';

comment on column OTH_ADJUNTO_ORDEN_TRABAJO.EXPEDIENTE_TECNICO is
'Indicador para el documento asociado a la orden de trabajo, que definira si el documento pertenece o no al expediente tecnico  0: no pertenece 1: si pertenece';


comment on column OTH_ADJUNTO_ORDEN_TRABAJO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTH_ADJUNTO_ORDEN_TRABAJO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTH_REVISION_ORDEN_TRABAJ                             */
/*==============================================================*/
create table OTH_REVISION_ORDEN_TRABAJ 
(
   ID_REVISION_ORDEN_TRABAJ NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   OBSERVACIONES        VARCHAR2(2000)       not null,
   ESTADO               VARCHAR2(3)          not null
      constraint CK_ESTADO_REVISION_HISTORICO check (ESTADO in ('APR','DEV','DEN') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTH_REVISION_ORDEN_TRABAJ primary key (ID_REVISION_ORDEN_TRABAJ)
);

comment on table OTH_REVISION_ORDEN_TRABAJ is
'Tabla histórica para registrar las revisiones que realizó un autorizado a una solicitud de orden de trabajo';

comment on column OTH_REVISION_ORDEN_TRABAJ.ID_REVISION_ORDEN_TRABAJ is
'Llave primaria de la tabla OTF_REVISION_ORDEN_TRABAJ que se asocia con la secuencia SQ_ID_REVISION_ORDEN_TRABAJ';

comment on column OTH_REVISION_ORDEN_TRABAJ.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_REVISION_ORDEN_TRABAJ.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_REVISION_ORDEN_TRABAJ.OBSERVACIONES is
'Observaciones indicadas por el revisor';

comment on column OTH_REVISION_ORDEN_TRABAJ.ESTADO is
'Estado del registro - APR: Aprueba, DEV: Devuelve, DEN: Deniega';

comment on column OTH_REVISION_ORDEN_TRABAJ.USUARIO is
'Usuario que realiza la revisión';

comment on column OTH_REVISION_ORDEN_TRABAJ.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


/*==============================================================*/
/* Table: OTH_TRAZABILIDAD_PROCESO                              */
/*==============================================================*/
create table OTH_TRAZABILIDAD_PROCESO 
(
   ID_TRAZABILIDAD_PROCESO NUMBER(10,0)         not null,
   ID_MOTIVO_RECHAZO    NUMBER(2,0),
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ESTADO_ORDEN_TRABAJO VARCHAR2(3)          not null
      constraint CK_ESTADO_ORDEN_HISTORICO check (ESTADO_ORDEN_TRABAJO = upper(ESTADO_ORDEN_TRABAJO)),
   NUM_EMPLEADO_EJECUTA INTEGER,
   FECHA_HORA_EJECUCION DATE                 not null,
   OBSERVACIONES        VARCHAR2(2000),
   OBSERVACIONES_INTERNAS VARCHAR2(2000),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTH_TRAZABILIDAD_PROCESO primary key (ID_TRAZABILIDAD_PROCESO)
);

comment on table OTH_TRAZABILIDAD_PROCESO is
'Tabla para registrar la trazabilidad del proceso de registro, ejecución y liquidación de una orden de trabajo.';

comment on column OTH_TRAZABILIDAD_PROCESO.ID_TRAZABILIDAD_PROCESO is
'Llave primaria de la tabla OTL_TRAZABILIDAD_PROCESO que se asocia con la secuencia SQ_ID_TRAZABILIDAD_PROCESO';

comment on column OTH_TRAZABILIDAD_PROCESO.ID_MOTIVO_RECHAZO is
'Llave primaria de la tabla OTM_MOTIVO_RECHAZO que se asocia con la secuencia SQ_ID_MOTIVO_RECHAZO';

comment on column OTH_TRAZABILIDAD_PROCESO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_TRAZABILIDAD_PROCESO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_TRAZABILIDAD_PROCESO.ESTADO_ORDEN_TRABAJO is
'Llave primaria de la tabla OTC_ESTADO_ORDEN_TRABAJO';

comment on column OTH_TRAZABILIDAD_PROCESO.NUM_EMPLEADO_EJECUTA is
'Número de empleado responsable de la ejecución';

comment on column OTH_TRAZABILIDAD_PROCESO.FECHA_HORA_EJECUCION is
'Fecha de ejecución del paso indicado por el responsable (revisión, liquidación, etc.)';

comment on column OTH_TRAZABILIDAD_PROCESO.OBSERVACIONES is
'Observaciones indicadas por el responsable';

comment on column OTH_TRAZABILIDAD_PROCESO.OBSERVACIONES_INTERNAS is
'Observaciones del responsable para procesos internos';

comment on column OTH_TRAZABILIDAD_PROCESO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTH_TRAZABILIDAD_PROCESO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';



/*==============================================================*/
/* Table: OTT_FICHA_TECNICA_GENERAL                             */
/*==============================================================*/
create table OTT_FICHA_TECNICA_GENERAL 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   CONSERVA_MOBILIARIO  NUMBER(1,0)          default 0 not null
      constraint CK_CONSERVA_MOBILIARIO_TRN check (CONSERVA_MOBILIARIO in (0,1)),
   REQUIERE_NUEVO_MOBILIARIO NUMBER(1,0)          default 0 not null
      constraint CK_REQUIERE_NUEVO_MOBILIARIO_T check (REQUIERE_NUEVO_MOBILIARIO in (0,1)),
   OTROS_MOBILIARIO     VARCHAR2(1000),
   OTRO_TIPO_REQUERIMIENTO VARCHAR2(1000),
   NOMBRE_ARCHIVO       VARCHAR2(100),
   ARCHIVO              BLOB,
   CUENTA_CON_ALARMA    NUMBER(1,0)          default 0 not null
      constraint CK_CUENTA_CON_ALARMA_TRN check (CUENTA_CON_ALARMA in (0,1)),
   REQUIERE_ALARMA      NUMBER(1,0)          default 0 not null
      constraint CK_REQUIERE_ALARMA_TRN check (REQUIERE_ALARMA in (0,1)),
   TIME_STAMP           DATE                 default SYSDATE not null,
   USUARIO              VARCHAR2(256)        not null,
   constraint PK_OTT_FICHA_TECNICA_GENERAL primary key (ID_UBICACION, ID_ORDEN_TRABAJO)
);

comment on table OTT_FICHA_TECNICA_GENERAL is
'Tabla para registrar los datos generales de una ficha técnica que complementa los datos de una orden de trabajo de diseño en trámite. Aplica sólo para ordenes de trabajo de diseño.';

comment on column OTT_FICHA_TECNICA_GENERAL.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_FICHA_TECNICA_GENERAL.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_FICHA_TECNICA_GENERAL.CONSERVA_MOBILIARIO is
'Indicador de si conserva mobiliario - 0:No, 1: Sí, Valor defecto: 0:No -';

comment on column OTT_FICHA_TECNICA_GENERAL.REQUIERE_NUEVO_MOBILIARIO is
'Indicador de si requiere nuevo mobiliario - 0:No, 1: Sí, Valor defecto: 0:No -';

comment on column OTT_FICHA_TECNICA_GENERAL.OTROS_MOBILIARIO is
'Otros requerimientos en relación con el mobiliario solicitado';

comment on column OTT_FICHA_TECNICA_GENERAL.OTRO_TIPO_REQUERIMIENTO is
'Detalle de otros tipos de requerimientos';

comment on column OTT_FICHA_TECNICA_GENERAL.NOMBRE_ARCHIVO is
'Nombre del archivo adjunto';

comment on column OTT_FICHA_TECNICA_GENERAL.ARCHIVO is
'Documento adjunto correspondiente a una lista del equipo que posea la unidad, su estado actual y el equipo que se proyecte adquirir a corto plazo';

comment on column OTT_FICHA_TECNICA_GENERAL.CUENTA_CON_ALARMA is
'Indicador de si cuenta con alarma - 0:No, 1: Sí, Valor defecto: 0:No -';

comment on column OTT_FICHA_TECNICA_GENERAL.REQUIERE_ALARMA is
'Indicador de si requiere alarma - 0:No, 1: Sí, Valor defecto: 0:No -';

comment on column OTT_FICHA_TECNICA_GENERAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

comment on column OTT_FICHA_TECNICA_GENERAL.USUARIO is
'Usuario que crea o modifica el registro.';


/*==============================================================*/
/* Table: OTH_FICHA_TECNICA_GENERAL                             */
/*==============================================================*/
create table OTH_FICHA_TECNICA_GENERAL 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   CONSERVA_MOBILIARIO  NUMBER(1,0)          default 0 not null
      constraint CK_CONSERVA_MOBILIAR_HST check (CONSERVA_MOBILIARIO in (0,1)),
   REQUIERE_NUEVO_MOBILIARIO NUMBER(1,0)          default 0 not null
      constraint CK_REQUIERE_NUEVO_HST check (REQUIERE_NUEVO_MOBILIARIO in (0,1)),
   OTROS_MOBILIARIO     VARCHAR2(1000),
   OTRO_TIPO_REQUERIMIENTO VARCHAR2(1000),
   NOMBRE_ARCHIVO       VARCHAR2(100),
   ARCHIVO              BLOB,
   CUENTA_CON_ALARMA    NUMBER(1,0)          default 0 not null
      constraint CK_CUENTA_CON_ALARMA_HST check (CUENTA_CON_ALARMA in (0,1)),
   REQUIERE_ALARMA      NUMBER(1,0)          default 0 not null
      constraint CK_REQUIERE_ALARMA_HST check (REQUIERE_ALARMA in (0,1)),
   TIME_STAMP           DATE                 default SYSDATE not null,
   USUARIO              VARCHAR2(256)        not null,
   constraint PK_OTH_FICHA_TECNICA_GENERAL primary key (ID_UBICACION, ID_ORDEN_TRABAJO)
);

comment on table OTH_FICHA_TECNICA_GENERAL is
'Tabla histórica para registrar los datos generales de una ficha técnica que complementa los datos de una orden de trabajo de diseño en trámite. Aplica sólo para ordenes de trabajo de diseño.';

comment on column OTH_FICHA_TECNICA_GENERAL.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_FICHA_TECNICA_GENERAL.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_FICHA_TECNICA_GENERAL.CONSERVA_MOBILIARIO is
'Indicador de si conserva mobiliario - 0:No, 1: Sí, Valor defecto: 0:No -';

comment on column OTH_FICHA_TECNICA_GENERAL.REQUIERE_NUEVO_MOBILIARIO is
'Indicador de si requiere nuevo mobiliario - 0:No, 1: Sí, Valor defecto: 0:No -';

comment on column OTH_FICHA_TECNICA_GENERAL.OTROS_MOBILIARIO is
'Otros requerimientos en relación con el mobiliario solicitado';

comment on column OTH_FICHA_TECNICA_GENERAL.OTRO_TIPO_REQUERIMIENTO is
'Detalle de otros tipos de requerimientos';

comment on column OTH_FICHA_TECNICA_GENERAL.NOMBRE_ARCHIVO is
'Nombre del archivo adjunto';

comment on column OTH_FICHA_TECNICA_GENERAL.ARCHIVO is
'Documento adjunto correspondiente a una lista del equipo que posea la unidad, su estado actual y el equipo que se proyecte adquirir a corto plazo';

comment on column OTH_FICHA_TECNICA_GENERAL.CUENTA_CON_ALARMA is
'Indicador de si cuenta con alarma - 0:No, 1: Sí, Valor defecto: 0:No -';

comment on column OTH_FICHA_TECNICA_GENERAL.REQUIERE_ALARMA is
'Indicador de si requiere alarma - 0:No, 1: Sí, Valor defecto: 0:No -';

comment on column OTH_FICHA_TECNICA_GENERAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

comment on column OTH_FICHA_TECNICA_GENERAL.USUARIO is
'Usuario que crea o modifica el registro.';


--30-10-2015
/*==============================================================*/
/* Table: OTM_ESPACIO                                           */
/*==============================================================*/
create table OTM_ESPACIO 
(
   ID_ESPACIO           NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   ORDEN                NUMBER(2,0)          not null
      constraint CK_ORDEN_ESPACIO check (ORDEN >= 0),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_ESPACIO check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_ESPACIO primary key (ID_ESPACIO),
   constraint AK_OTM_ESPACIO unique (DESCRIPCION, ID_UBICACION)
);

comment on table OTM_ESPACIO is
'Tabla para registrar los espacios a considerar en el llenado de una ficha técnica de una orden de trabajo de diseño';

comment on column OTM_ESPACIO.ID_ESPACIO is
'Llave primaria de la tabla OTM_ESPACIO que se asocia con la secuencia SQ_ID_ESPACIO';

comment on column OTM_ESPACIO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTM_ESPACIO.DESCRIPCION is
'Descripción del espacio';

comment on column OTM_ESPACIO.ORDEN is
'Orden de visualización';

comment on column OTM_ESPACIO.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_ESPACIO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_ESPACIO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


/*==============================================================*/
/* Table: OTM_SUBCOMPONENTE                                     */
/*==============================================================*/
create table OTM_SUBCOMPONENTE 
(
   ID_ESPACIO           NUMBER(10,0)         not null,
   ID_SUBCOMPONENTE     NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   ORDEN                NUMBER(2,0)          not null
      constraint CK_ORDEN_SUBCOMPONENTE check (ORDEN >= 0),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_SUBCOMPONENTE check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_SUBCOMPONENTE primary key (ID_ESPACIO, ID_SUBCOMPONENTE),
    constraint AK_OTM_SUBCOMPONENTE unique (DESCRIPCION, ID_ESPACIO)
);

comment on table OTM_SUBCOMPONENTE is
'Tabla para registrar los subcomponentes por espacio a considerar en el llenado de una ficha técnica de una orden de trabajo de diseño';

comment on column OTM_SUBCOMPONENTE.ID_ESPACIO is
'Llave primaria de la tabla OTM_ESPACIO que se asocia con la secuencia SQ_ID_ESPACIO';

comment on column OTM_SUBCOMPONENTE.ID_SUBCOMPONENTE is
'Llave primaria de la tabla OTM_SUBCOMPONENTE que se asocia con la secuencia SQ_ID_SUBCOMPONENTE';

comment on column OTM_SUBCOMPONENTE.DESCRIPCION is
'Descripción del subcomponente';

comment on column OTM_SUBCOMPONENTE.ORDEN is
'Orden de visualización';

comment on column OTM_SUBCOMPONENTE.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_SUBCOMPONENTE.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_SUBCOMPONENTE.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_REQUERIMIENTO                                     */
/*==============================================================*/
create table OTM_REQUERIMIENTO 
(
   ID_REQUERIMIENTO     NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_REQUERIMIENTO_PADRE NUMBER(10,0),
   DESCRIPCION          VARCHAR2(100)        not null,
   ORDEN                NUMBER(2,0)          not null
      constraint CK_ORDEN_REQUERIMIENTO check (ORDEN >= 0),
   NIVEL                NUMBER(1,0)          not null
      constraint CK_NIVEL_REQUERIMIENTO check (NIVEL between 1 and 3),
   TIPO_VALOR           VARCHAR2(3)          default 'NUM' not null
      constraint CK_TIPO_VALOR_REQUERIMIENTO check (TIPO_VALOR in ('NUM','CAR','IND') and TIPO_VALOR = upper(TIPO_VALOR)),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_REQUERIMIENTO check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_REQUERIMIENTO primary key (ID_REQUERIMIENTO)
);

comment on table OTM_REQUERIMIENTO is
'Tabla para registrar los requerimientos a llenar en una ficha técnica  de una orden de trabajo de diseño';

comment on column OTM_REQUERIMIENTO.ID_REQUERIMIENTO is
'Llave primaria de la tabla OTM_REQUERIMIENTO que se asocia con la secuencia SQ_ID_REQUERIMIENTO';

comment on column OTM_REQUERIMIENTO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTM_REQUERIMIENTO.ID_REQUERIMIENTO_PADRE is
'Referencia al requeremiento de nivel anterior';

comment on column OTM_REQUERIMIENTO.DESCRIPCION is
'Descripción del requerimiento. Puede repetirse en diferentes niveles. Ejemplo: Cuantos poseen ';

comment on column OTM_REQUERIMIENTO.ORDEN is
'Orden de visualización';

comment on column OTM_REQUERIMIENTO.NIVEL is
'Nivel en que se ubica el requerimiento con un valor de 1 a 3 dado que conforman un encabezado.';

comment on column OTM_REQUERIMIENTO.TIPO_VALOR is
'Tipo de valor a registrar para el requerimiento. - NUM: Numerico, CAR - Caracter - Valor por defecto NUM';

comment on column OTM_REQUERIMIENTO.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_REQUERIMIENTO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_REQUERIMIENTO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_FICHA_TECNICA_ESPACIO                             */
/*==============================================================*/
create table OTT_FICHA_TECNICA_ESPACIO 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         not null,
   ID_ESPACIO           NUMBER(10,0)         not null,
   constraint PK_OTT_FICHA_TECNICA_ESPACIO primary key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO)
);

comment on table OTT_FICHA_TECNICA_ESPACIO is
'Tabla para registrar los espacios que indica el solicitante en la ficha técnica';

comment on column OTT_FICHA_TECNICA_ESPACIO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_FICHA_TECNICA_ESPACIO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_FICHA_TECNICA_ESPACIO.ID_ESPACIO is
'Llave primaria de la tabla OTM_ESPACIO que se asocia con la secuencia SQ_ID_ESPACIO';

/*==============================================================*/
/* Table: OTT_FICHA_TECNICA_SUBCOMP                             */
/*==============================================================*/
create table OTT_FICHA_TECNICA_SUBCOMP 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         not null,
   ID_ESPACIO           NUMBER(10,0)         not null,
   ID_SUBCOMPONENTE     NUMBER(10,0)         not null,
   constraint PK_OTT_FICHA_TECNICA_SUBCOMP primary key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE)
);

comment on table OTT_FICHA_TECNICA_SUBCOMP is
'Tabla para registrar los subcomponentes por espacio que indica el solicitante en la ficha técnica';

comment on column OTT_FICHA_TECNICA_SUBCOMP.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_FICHA_TECNICA_SUBCOMP.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_FICHA_TECNICA_SUBCOMP.ID_ESPACIO is
'Llave primaria de la tabla OTM_ESPACIO que se asocia con la secuencia SQ_ID_ESPACIO';

comment on column OTT_FICHA_TECNICA_SUBCOMP.ID_SUBCOMPONENTE is
'Llave primaria de la tabla OTM_SUBCOMPONENTE que se asocia con la secuencia SQ_ID_SUBCOMPONENTE';

/*==============================================================*/
/* Table: OTT_FICHA_TECNICA_DETALLE                             */
/*==============================================================*/
create table OTT_FICHA_TECNICA_DETALLE 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         not null,
   ID_ESPACIO           NUMBER(10,0)         not null,
   ID_SUBCOMPONENTE     NUMBER(10,0)         not null,
   ID_REQUERIMIENTO     NUMBER(10,0)         not null,
   VALOR                VARCHAR2(10)         not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_FICHA_TECNICA_DETALLE primary key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE, ID_REQUERIMIENTO)
);

comment on table OTT_FICHA_TECNICA_DETALLE is
'Tabla que registra los valores de llenado de la ficha técnica de una orden de trabajo en trámite';

comment on column OTT_FICHA_TECNICA_DETALLE.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_FICHA_TECNICA_DETALLE.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_FICHA_TECNICA_DETALLE.ID_ESPACIO is
'Llave primaria de la tabla OTM_ESPACIO que se asocia con la secuencia SQ_ID_ESPACIO';

comment on column OTT_FICHA_TECNICA_DETALLE.ID_SUBCOMPONENTE is
'Llave primaria de la tabla OTM_SUBCOMPONENTE que se asocia con la secuencia SQ_ID_SUBCOMPONENTE';

comment on column OTT_FICHA_TECNICA_DETALLE.ID_REQUERIMIENTO is
'Llave primaria de la tabla OTM_REQUERIMIENTO que se asocia con la secuencia SQ_ID_REQUERIMIENTO';

comment on column OTT_FICHA_TECNICA_DETALLE.VALOR is
'Valor registrado por el usuario';

comment on column OTT_FICHA_TECNICA_DETALLE.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTT_FICHA_TECNICA_DETALLE.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTH_FICHA_TECNICA_ESPACIO                             */
/*==============================================================*/
create table OTH_FICHA_TECNICA_ESPACIO 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         not null,
   ID_ESPACIO           NUMBER(10,0)         not null,
   constraint PK_OTH_FICHA_TECNICA_ESPACIO primary key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO)
);

comment on table OTH_FICHA_TECNICA_ESPACIO is
'Tabla histórica para registrar los espacios que indica el solicitante en la ficha técnica';

comment on column OTH_FICHA_TECNICA_ESPACIO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_FICHA_TECNICA_ESPACIO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_FICHA_TECNICA_ESPACIO.ID_ESPACIO is
'Llave primaria de la tabla OTM_ESPACIO que se asocia con la secuencia SQ_ID_ESPACIO';

/*==============================================================*/
/* Table: OTH_FICHA_TECNICA_SUBCOMP                             */
/*==============================================================*/
create table OTH_FICHA_TECNICA_SUBCOMP 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         not null,
   ID_ESPACIO           NUMBER(10,0)         not null,
   ID_SUBCOMPONENTE     NUMBER(10,0)         not null,
   constraint PK_OTH_FICHA_TECNICA_SUBCOMP primary key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE)
);

comment on table OTH_FICHA_TECNICA_SUBCOMP is
'Tabla histórica para registrar los subcomponentes por espacio que indica el solicitante en la ficha técnica';

comment on column OTH_FICHA_TECNICA_SUBCOMP.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_FICHA_TECNICA_SUBCOMP.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_FICHA_TECNICA_SUBCOMP.ID_ESPACIO is
'Llave primaria de la tabla OTM_ESPACIO que se asocia con la secuencia SQ_ID_ESPACIO';

comment on column OTH_FICHA_TECNICA_SUBCOMP.ID_SUBCOMPONENTE is
'Llave primaria de la tabla OTM_SUBCOMPONENTE que se asocia con la secuencia SQ_ID_SUBCOMPONENTE';

/*==============================================================*/
/* Table: OTH_FICHA_TECNICA_DETALLE                             */
/*==============================================================*/
create table OTH_FICHA_TECNICA_DETALLE 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         not null,
   ID_ESPACIO           NUMBER(10,0)         not null,
   ID_SUBCOMPONENTE     NUMBER(10,0)         not null,
   ID_REQUERIMIENTO     NUMBER(10,0)         not null,
   VALOR                VARCHAR2(10)         not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   USUARIO              VARCHAR2(256)        not null,
   constraint PK_OTH_FICHA_TECNICA_DETALLE primary key (ID_UBICACION, ID_ESPACIO, ID_SUBCOMPONENTE, ID_REQUERIMIENTO, ID_ORDEN_TRABAJO)
);

comment on table OTH_FICHA_TECNICA_DETALLE is
'Tabla que registra los valores de llenado de la ficha técnica de una orden de trabajo tramitada';

comment on column OTH_FICHA_TECNICA_DETALLE.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_FICHA_TECNICA_DETALLE.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_FICHA_TECNICA_DETALLE.ID_ESPACIO is
'Llave primaria de la tabla OTM_ESPACIO que se asocia con la secuencia SQ_ID_ESPACIO';

comment on column OTH_FICHA_TECNICA_DETALLE.ID_SUBCOMPONENTE is
'Llave primaria de la tabla OTM_SUBCOMPONENTE que se asocia con la secuencia SQ_ID_SUBCOMPONENTE';

comment on column OTH_FICHA_TECNICA_DETALLE.ID_REQUERIMIENTO is
'Llave primaria de la tabla OTM_REQUERIMIENTO que se asocia con la secuencia SQ_ID_REQUERIMIENTO';

comment on column OTH_FICHA_TECNICA_DETALLE.VALOR is
'Valor registrado por el usuario';

comment on column OTH_FICHA_TECNICA_DETALLE.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

comment on column OTH_FICHA_TECNICA_DETALLE.USUARIO is
'Usuario que crea o modifica el registro.';

--02-11-2015
/*==============================================================*/
/* Table: OTF_PERIODO_CIERRE                                    */
/*==============================================================*/
create table OTF_PERIODO_CIERRE 
(
   ID_PERIODO_CIERRE    NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   UNIDAD_CIERRE        VARCHAR2(3)          default 'MNT' not null
      constraint CK_UNIDAD_CIERRE check (UNIDAD_CIERRE in ('MNT','DIS') and UNIDAD_CIERRE = upper(UNIDAD_CIERRE)),
   FECHA_INICIO_CIERRE  DATE                 not null,
   FECHA_FIN_CIERRE     DATE                 not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   USUARIO              VARCHAR2(256)        not null,
   constraint PK_OTF_PERIODO_CIERRE primary key (ID_PERIODO_CIERRE),
   constraint AK_PERIODO_CIERRE unique (UNIDAD_CIERRE, FECHA_INICIO_CIERRE)
);

comment on table OTF_PERIODO_CIERRE is
'Tabla para registrar los períodos de cierre que realiza la sección de mantenimiento y construcción a la recepción de ordenes de trabajo.';

comment on column OTF_PERIODO_CIERRE.ID_PERIODO_CIERRE is
'Llave primaria de la tabla OTF_PERIODO_CIERRE que se asocia con la secuencia SQ_ID_PERIODO_CIERRE';

comment on column OTF_PERIODO_CIERRE.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTF_PERIODO_CIERRE.UNIDAD_CIERRE is
'Unidad de la sección de mantenimiento y construcción que realiza el cierre - MNT: Mantenimiento, DIS: Diseño - Valor defecto: MNT';

comment on column OTF_PERIODO_CIERRE.FECHA_INICIO_CIERRE is
'Fecha de inicio del cierre';

comment on column OTF_PERIODO_CIERRE.FECHA_FIN_CIERRE is
'Fecha de fin de cierre';

comment on column OTF_PERIODO_CIERRE.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

comment on column OTF_PERIODO_CIERRE.USUARIO is
'Usuario que crea o modifica el registro.';

--02-10-15, modificado: 20-10-2015
alter table OTT_ADJUNTO_ORDEN_TRABAJO
   add constraint FK_OTF_ORDEN_TRABAJO_OTT_ADJUN foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTT_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);


--20-10-2015
alter table OTT_ORDEN_TRABAJO
   add constraint FK_OTM_LUGAR_TRABAJO_OTT_ORDEN foreign key (ID_LUGAR_TRABAJO)
      references OTM_LUGAR_TRABAJO (ID_LUGAR_TRABAJO);

alter table OTT_ORDEN_TRABAJO
   add constraint FK_EU_EMPLEADOS_OTT_ORDEN_TRAB foreign key (NUM_EMPLEADO)
      references EU_EMPLEADOS (NUM_EMPLEADO);

alter table OTT_ORDEN_TRABAJO
   add constraint FK_OTC_TIPO_ORD_TRAB_OTT_ORDEN foreign key (TIPO_ORDEN_TRABAJO)
      references OTC_TIPO_ORDEN_TRABAJO (TIPO_ORDEN_TRABAJO);

alter table OTT_ORDEN_TRABAJO
   add constraint FK_OTM_SECTOR_TALLER_OTT_ORDEN foreign key (ID_SECTOR_TALLER)
      references OTM_SECTOR_TALLER (ID_SECTOR_TALLER);

alter table OTT_ORDEN_TRABAJO
   add constraint FK_OTM_UBICACION_OTT_ORDEN_TR foreign key (ID_UBICACION)
      references OTM_UBICACION (ID_UBICACION);

alter table OTT_ORDEN_TRABAJO
   add constraint FK_OTM_MOTIVO_RECHAZO_OTT_ORD foreign key (ID_MOTIVO_RECHAZO)
      references OTM_MOTIVO_RECHAZO (ID_MOTIVO_RECHAZO);

alter table OTT_ORDEN_TRABAJO
   add constraint FK_OTC_ESTADO_OTT_ORDEN_TRABAJ foreign key (ESTADO_ORDEN_TRABAJO)
      references OTC_ESTADO_ORDEN_TRABAJO (ESTADO_ORDEN_TRABAJO);

alter table OTT_ORDEN_TRABAJO
   add constraint FK_OTM_ACTIVIDAD_OTT_ORDEN_TR foreign key (ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD)
      references OTM_ACTIVIDAD (ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD);

alter table OTT_ORDEN_TRABAJO
   add constraint FK_OTT_ORDEN_TRABAJO_OTT_ORDEN foreign key (ID_UBICACION_MADRE, ID_ORDEN_TRABAJO_MADRE)
      references OTT_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTT_ORDEN_TRABAJO
   add constraint FK_OTM_UBICACION_OTT_ORDEN_T foreign key (ID_UBICACION_ORIGEN)
      references OTM_UBICACION (ID_UBICACION);


--modificado: 02-10-15,20-10-2015
alter table OTT_REVISION_ORDEN_TRABAJ
   add constraint FK_OTF_ORDEN_TRABAJO_OTT_REVIS foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTT_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);


alter table OTM_ACTIVIDAD
   add constraint FK_OTM_SECTOR_OTM_ACTIVIDAD foreign key (ID_SECTOR_TALLER)
      references OTM_SECTOR_TALLER (ID_SECTOR_TALLER);

alter table OTM_ACTIVIDAD
   add constraint FK_OTM_CATEGORIA_SERVICIO_OTM foreign key (ID_CATEGORIA_SERVICIO)
      references OTM_CATEGORIA_SERVICIO (ID_CATEGORIA_SERVICIO);

alter table OTM_CATEGORIA_SERVICIO
   add constraint FK_EU_EMPLEADOS_OTM_CATEGORIA foreign key (NUM_EMPLEADO_SUPERVISOR)
      references EU_EMPLEADOS (NUM_EMPLEADO);

alter table OTM_CATEGORIA_SERVICIO
   add constraint FK_OTM_SECTOR_TALLER_OTM_CATEG foreign key (ID_SECTOR_TALLER)
      references OTM_SECTOR_TALLER (ID_SECTOR_TALLER);
      
alter table OTM_CATEGORIA_SERVICIO
   add constraint FK_OTM_UBICACION_OTM_CATEGORIA foreign key (ID_UBICACION_ADMINISTRA)
      references OTM_UBICACION (ID_UBICACION);   

alter table OTM_LUGAR_TRABAJO
   add constraint FK_OTM_SECTOR_OTM_LUGAR_TRABAJ foreign key (ID_SECTOR_TALLER)
      references OTM_SECTOR_TALLER (ID_SECTOR_TALLER);

alter table OTM_LUGAR_TRABAJO
   add constraint FK_OTM_TIPO_LUGAR_OTM_UBICAC foreign key (ID_TIPO_LUGAR_UBICACION)
      references OTM_TIPO_LUGAR_UBICACION (ID_TIPO_LUGAR_UBICACION);
      
alter table OTM_LUGAR_TRABAJO
   add constraint FK_OTM_UBICACION_OTM_LUGAR_TR foreign key (ID_UBICACION_PERTENECE)
      references OTM_UBICACION (ID_UBICACION);

alter table OTM_LUGAR_TRABAJO
   add constraint FK_OTM_UBICACION_OTM_LUGAR_T foreign key (ID_UBICACION_ADMINISTRA)
      references OTM_UBICACION (ID_UBICACION);
      
--09-09-2015
alter table OTF_OPERARIO
   add constraint FK_OTM_SECTOR_TALLER_OTF_OPER foreign key (ID_SECTOR_TALLER)
      references OTM_SECTOR_TALLER (ID_SECTOR_TALLER);

alter table OTF_OPERARIO
   add constraint FK_EU_EMPLEADOS_OTF_OPERARIO foreign key (NUM_EMPLEADO)
      references EU_EMPLEADOS (NUM_EMPLEADO);

alter table OTF_PLANEACION_PREVENTIVO
   add constraint FK_OTM_LUGAR_TRABAJO_OTF_PLAN foreign key (ID_LUGAR_TRABAJO)
      references OTM_LUGAR_TRABAJO (ID_LUGAR_TRABAJO);

alter table OTF_PLANEACION_PREVENTIVO
   add constraint FK_OTM_ACTIVIDAD_OTF_PLANEAC foreign key (ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD)
      references OTM_ACTIVIDAD (ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD);
      
alter table OTF_PLANEACION_PREVENTIVO
   add constraint FK_OTM_UBICACION_OTF_PLANEAC foreign key (ID_UBICACION_ADMINISTRA)
      references OTM_UBICACION (ID_UBICACION);

--02-10-15, modificado 20-10-15
alter table OTT_TRAZABILIDAD_PROCESO
   add constraint FK_OTF_ORDEN_TRABAJO_OTT_TRAZ foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTT_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTT_TRAZABILIDAD_PROCESO
   add constraint FK_EU_EMPLEADOS_OTT_TRAZABIL foreign key (NUM_EMPLEADO_EJECUTA)
      references EU_EMPLEADOS (NUM_EMPLEADO);

alter table OTT_TRAZABILIDAD_PROCESO
   add constraint FK_OTC_ESTADO_ORDEN_TRABAJO_O foreign key (ESTADO_ORDEN_TRABAJO)
      references OTC_ESTADO_ORDEN_TRABAJO (ESTADO_ORDEN_TRABAJO);

alter table OTT_TRAZABILIDAD_PROCESO
   add constraint FK_OTT_TRAZ_RELATIONS_OTM_MOTI foreign key (ID_MOTIVO_RECHAZO)
      references OTM_MOTIVO_RECHAZO (ID_MOTIVO_RECHAZO);


      
alter table OTM_SECTOR_TALLER
   add constraint FK_EU_EMPLEADOS_OTM_SECTOR_TAL foreign key (NUM_EMPLEADO_SUSTITUTO)
      references EU_EMPLEADOS (NUM_EMPLEADO);

alter table OTM_SECTOR_TALLER
   add constraint FK_EU_EMPLEADOS_OTM_SECTOR foreign key (NUM_EMPLEADO_COORDINADOR)
      references EU_EMPLEADOS (NUM_EMPLEADO);
      
alter table OTM_SECTOR_TALLER
   add constraint FK_OTM_UBICACION_OTM_SECTOR foreign key (ID_UBICACION_ADMINISTRA)
      references OTM_UBICACION (ID_UBICACION);
      
--21-09-2015
alter table OTM_AUTORIZADO_DIRECTOR
   add constraint FK_EU_EMPLEADOS_OTM_AUTORIZADO foreign key (NUM_EMPLEADO)
      references EU_EMPLEADOS (NUM_EMPLEADO);
      
--24-09-2015
alter table OTM_AUTORIZADO_UBICACION
   add constraint FK_OTM_AUTO_OTM_AUTOR_OTM_UBIC foreign key (ID_UBICACION_ADMINISTRA)
      references OTM_UBICACION (ID_UBICACION);

alter table OTM_AUTORIZADO_UBICACION
   add constraint FK_OTM_AUTO_OTM_AUTOR_EU_EMPLE foreign key (NUM_EMPLEADO)
      references EU_EMPLEADOS (NUM_EMPLEADO);

--28-09-2015
alter table OTM_UNIDAD_UBICACION
   add constraint FK_OTM_UBICACION_OTM_UNIDAD_UB foreign key (ID_UBICACION)
      references OTM_UBICACION (ID_UBICACION);
      
--02-10-2015

alter table OTF_UBICACION_FAVORITA
   add constraint FK_OTM_UBICACION_OTF_UBICACION foreign key (ID_UBICACION)
      references OTM_UBICACION (ID_UBICACION);

alter table OTF_UBICACION_FAVORITA
   add constraint FK_EU_EMPLEADOS_OTF_UBICACION foreign key (NUM_EMPLEADO)
      references EU_EMPLEADOS (NUM_EMPLEADO);


/*-------------------------------------------------------------------------------------------------*/
--26-10-2015
alter table OTH_ORDEN_TRABAJO
   add constraint FK_OTC_TIPO_ORDEN_OTH_ORDEN_TR foreign key (TIPO_ORDEN_TRABAJO)
      references OTC_TIPO_ORDEN_TRABAJO (TIPO_ORDEN_TRABAJO);

alter table OTH_ORDEN_TRABAJO
   add constraint FK_OTC_ESTADO_OTH_ORDEN_TRABAJ foreign key (ESTADO_ORDEN_TRABAJO)
      references OTC_ESTADO_ORDEN_TRABAJO (ESTADO_ORDEN_TRABAJO);

alter table OTH_ORDEN_TRABAJO
   add constraint FK_OTM_UBICACION_OTH_ORDEN foreign key (ID_UBICACION)
      references OTM_UBICACION (ID_UBICACION);

alter table OTH_ORDEN_TRABAJO
   add constraint FK_OTM_LUGAR_TRABAJO_OTH_ORDEN foreign key (ID_LUGAR_TRABAJO)
      references OTM_LUGAR_TRABAJO (ID_LUGAR_TRABAJO);

alter table OTH_ORDEN_TRABAJO
   add constraint FK_EU_EMPLEADOS_OTH_ORDEN_TRAB foreign key (NUM_EMPLEADO)
      references EU_EMPLEADOS (NUM_EMPLEADO);

alter table OTH_ORDEN_TRABAJO
   add constraint FK_OTM_ACTIVIDAD_OTH_ORDEN foreign key (ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD)
      references OTM_ACTIVIDAD (ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD);

alter table OTH_ORDEN_TRABAJO
   add constraint FK_OTM_SECTOR_TALLER_OTH_ORDEN foreign key (ID_SECTOR_TALLER)
      references OTM_SECTOR_TALLER (ID_SECTOR_TALLER);

alter table OTH_ORDEN_TRABAJO
   add constraint FK_OTM_MOTIVO_RECHAZO_OTH_ORD foreign key (ID_MOTIVO_RECHAZO)
      references OTM_MOTIVO_RECHAZO (ID_MOTIVO_RECHAZO);

alter table OTH_ORDEN_TRABAJO
   add constraint FK_OTH_ORDEN_TRABAJO_OTH_ORDEN foreign key (ID_UBICACION_MADRE, ID_ORDEN_TRABAJO_MADRE)
      references OTH_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);
      
alter table OTH_ORDEN_TRABAJO
   add constraint FK_OTM_UBICACION_OTH_ORDEN_TR foreign key (ID_UBICACION_ORIGEN)
      references OTM_UBICACION (ID_UBICACION);


alter table OTH_ADJUNTO_ORDEN_TRABAJO
   add constraint FK_OTH_ORDEN_TRABAJO_OTH_ADJUN foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTH_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTH_REVISION_ORDEN_TRABAJ
   add constraint FK_OTH_ORDEN_TRABAJO_OTH_REVIS foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTH_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTH_TRAZABILIDAD_PROCESO
   add constraint FK_OTH_ORDEN_TRABAJO_OTH_TRAZ foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTH_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTH_TRAZABILIDAD_PROCESO
   add constraint FK_EU_EMPLEADOS_OTH_TRAZABIL foreign key (NUM_EMPLEADO_EJECUTA)
      references EU_EMPLEADOS (NUM_EMPLEADO);

alter table OTH_TRAZABILIDAD_PROCESO
   add constraint FK_OTC_ESTADO_ORDEN_OTH_TRAZAB foreign key (ESTADO_ORDEN_TRABAJO)
      references OTC_ESTADO_ORDEN_TRABAJO (ESTADO_ORDEN_TRABAJO);

alter table OTH_TRAZABILIDAD_PROCESO
   add constraint FK_OTM_MOTIVO_RECHAZO_OTH_TRAZ foreign key (ID_MOTIVO_RECHAZO)
      references OTM_MOTIVO_RECHAZO (ID_MOTIVO_RECHAZO);


alter table OTT_FICHA_TECNICA_GENERAL
   add constraint FK_OTT_ORDEN_TRABAJO_OTT_FICHA foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTT_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);
      
alter table OTH_FICHA_TECNICA_GENERAL
   add constraint FK_OTT_ORDEN_TRABAJO_OTH_FICHA foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTH_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);
      
--30-10-2015
alter table OTM_ESPACIO
   add constraint FK_OTM_UBICACION_OTM_ESPACIO foreign key (ID_UBICACION)
      references OTM_UBICACION (ID_UBICACION);

alter table OTM_SUBCOMPONENTE
   add constraint FK_OTM_ESPACIO_OTM_SUBCOMPONEN foreign key (ID_ESPACIO)
      references OTM_ESPACIO (ID_ESPACIO);

alter table OTM_REQUERIMIENTO
   add constraint FK_OTM_REQUERIMIENTO_OTM_REQU foreign key (ID_REQUERIMIENTO_PADRE)
      references OTM_REQUERIMIENTO (ID_REQUERIMIENTO);

alter table OTM_REQUERIMIENTO
   add constraint FK_OTM_UBICACION_OTM_REQUERIM foreign key (ID_UBICACION)
      references OTM_UBICACION (ID_UBICACION);

alter table OTT_FICHA_TECNICA_ESPACIO
   add constraint FK_OTT_ORDEN_OTT_FICHA_ESPACIO foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTT_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTT_FICHA_TECNICA_ESPACIO
   add constraint FK_OTM_ESPACIO_OTT_FICHA_ESPAC foreign key (ID_ESPACIO)
      references OTM_ESPACIO (ID_ESPACIO);


alter table OTT_FICHA_TECNICA_SUBCOMP
   add constraint FK_OTT_FICHA_ESPACIO_OTT_FICHA foreign key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO)
      references OTT_FICHA_TECNICA_ESPACIO (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO);

alter table OTT_FICHA_TECNICA_SUBCOMP
   add constraint FK_OTM_SUB_OTT_FICHA_SUBCOMP foreign key (ID_ESPACIO, ID_SUBCOMPONENTE)
      references OTM_SUBCOMPONENTE (ID_ESPACIO, ID_SUBCOMPONENTE);


alter table OTT_FICHA_TECNICA_DETALLE
   add constraint FK_OTT_FICHA_SUB_OTT_FICHA_DET foreign key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE)
      references OTT_FICHA_TECNICA_SUBCOMP (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE);

alter table OTT_FICHA_TECNICA_DETALLE
   add constraint FK_OTM_REQUERIMIENTO_OTT_FICHA foreign key (ID_REQUERIMIENTO)
      references OTM_REQUERIMIENTO (ID_REQUERIMIENTO);

alter table OTH_FICHA_TECNICA_ESPACIO
   add constraint FK_OTH_ORDEN_OTH_FICHA_ESPACIO foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTH_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTH_FICHA_TECNICA_ESPACIO
   add constraint FK_OTM_ESPACIO_OTH_FICHA_ESPAC foreign key (ID_ESPACIO)
      references OTM_ESPACIO (ID_ESPACIO);

alter table OTH_FICHA_TECNICA_SUBCOMP
   add constraint FK_OTH_FICHA_ESPACIO_OTH_SUB foreign key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO)
      references OTH_FICHA_TECNICA_ESPACIO (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO);

alter table OTH_FICHA_TECNICA_SUBCOMP
   add constraint FK_OTM_SUB_OTH_FICHA_SUBCOMP foreign key (ID_ESPACIO, ID_SUBCOMPONENTE)
      references OTM_SUBCOMPONENTE (ID_ESPACIO, ID_SUBCOMPONENTE);

  alter table OTH_FICHA_TECNICA_DETALLE
   add constraint FK_OTH_FICHA_OTH_FICHA_DETALLE foreign key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE)
      references OTH_FICHA_TECNICA_SUBCOMP (ID_UBICACION, ID_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE);

alter table OTH_FICHA_TECNICA_DETALLE
   add constraint FK_OTM_REQUERIMIENTO_OTH_FICHA foreign key (ID_REQUERIMIENTO)
      references OTM_REQUERIMIENTO (ID_REQUERIMIENTO);
      
alter table OTF_PERIODO_CIERRE
   add constraint FK_OTM_UBICACION_OTF_PERIODO foreign key (ID_UBICACION)
      references OTM_UBICACION (ID_UBICACION);


/*===============================================================*/
/*                  TABLAS II ETAPA                              */
/*15-01-2016                                                     */
/*===============================================================*/
/*
/*Drops a constraints*/
alter table OTH_ADJUNTO_ORDEN_TRABAJO
   drop constraint FK_OTM_ETAPA_OTH_ADJ_ORD_TRB;

alter table OTH_ADJUNTO_ORDEN_TRABAJO
   drop constraint FK_OTM_TIP_DOC_OTH_ADJ_ORD_TRB;
   
alter table OTT_ADJUNTO_ORDEN_TRABAJO
   drop constraint FK_OTM_ETAPA_OTT_ADJ_ORD_TRB;

alter table OTT_ADJUNTO_ORDEN_TRABAJO
   drop constraint FK_OTM_TIP_DOC_OTT_ADJ_ORD_TRB;
   
alter table OTF_EXCEPCION_PERIODO
   drop constraint FK_EU_EMPLEADO_OTF_EXC_PER;

alter table OTF_EXCEPCION_PERIODO
   drop constraint FK_OTM_UNID_TIEMP_OTF_EXC_PER;

alter table OTF_OPERARIO_AREA
   drop constraint FK_EU_EMPLEADOS_OTF_OPER_AREA;

alter table OTF_OPERARIO_AREA
   drop constraint FK_OTM_AREA_PROF_OTF_OPER_AREA;

alter table OTF_OPERARIO_AREA
   drop constraint FK_OTM_SEC_TALLR_OTF_OPER_AREA;

alter table OTH_OPERARIO_ORDEN_TRAB
   drop constraint FK_ATH_ORDEN_ATH_OPER_ORD_TRJO;

alter table OTH_OPERARIO_ORDEN_TRAB
   drop constraint FK_OTF_OPR_AREA_OTH_OPR_ORD_TR;

alter table OTH_OPERARIO_ORDEN_TRAB
   drop constraint FK_OTM_TP_RDN_TRJ_OTH_OPER_RDN;

alter table OTH_TIEMPO_OPERARIO
   drop constraint FK_OTM_UNI_TMP_OTH_TMP_OPER;

alter table OTH_TIEMPO_OPERARIO
   drop constraint FK_OTH_OPR_DN_TRJ_OTH_TMP_OPR;

alter table OTH_VIABILIDAD_TECNICA
   drop constraint FK_OTH_RDN_TRJ_OTH_VBL_TEC;

alter table OTH_VIABILIDAD_TECNICA
   drop constraint FK_OTM_UNI_TMP_OTH_VIABLDD_TEC;

alter table OTM_AREA_PROFESIONAL
   drop constraint FK_OTM_UBICACION_OTF_AREA_PROF;

alter table OTT_OPERARIO_ORDEN_TRAB
   drop constraint FK_OTF_OPR_AREA_OTT_OPR_ORD_TR;

alter table OTT_OPERARIO_ORDEN_TRAB
   drop constraint FK_OTM_TP_RDN_TRJ_OTT_OPER_RDN;

alter table OTT_OPERARIO_ORDEN_TRAB
   drop constraint FK_OTT_ORDEN_OTT_OPER_ORD_TRJO;

alter table OTT_TIEMPO_OPERARIO
   drop constraint FK_OTM_UNI_TMP_OTT_TMP_OPER;

alter table OTT_TIEMPO_OPERARIO
   drop constraint FK_OTT_OPR_DN_TRJ_OTT_TMP_OPR;

alter table OTT_VIABILIDAD_TECNICA
   drop constraint FK_OTM_UNI_TMP_OTT_VIABLDD_TEC;

alter table OTT_VIABILIDAD_TECNICA
   drop constraint FK_OTT_RDN_TRJ_OTT_VBL_TEC;

 alter table OTH_ANTEPROYECTO
   drop constraint FK_OTH_ORDEN_TR_OTH_ANTEPROYCT;

alter table OTH_DOCUMENTO_ANTEPROYECT
   drop constraint FK_OTH_ANTEPRO_OTH_DOC_ANTEPRO;

alter table OTT_ANTEPROYECTO
   drop constraint FK_OTT_ORDEN_TR_OTT_ANTEPROYCT;

alter table OTT_DOCUMENTO_ANTEPROYECT
   drop constraint FK_OTT_ANTEPRO_OTT_DOC_ANTEPRO;

alter table OTT_DOCUMENTO_ANTEPROYECT
   drop constraint FK_OTT_ADJ_OT_OTT_DOC_ANTEPRO;

alter table OTT_DOCUMENTO_ANTEPROYECT
   drop constraint FK_OTT_ANTEPRO_OTT_DOC_ANTEPRO;

alter table OTH_DOCUMENTO_ANTEPROYECT
   drop constraint FK_OTH_ADJ_OT_OTH_DOC_ANTEPRO;

alter table OTH_DOCUMENTO_ANTEPROYECT
   drop constraint FK_OTH_ANTEPRO_OTH_DOC_ANTEPRO;

alter table OTM_UNIDAD_ENCARGADA
   drop constraint FK_OTM_LUGAR_TRAB_OTM_UNID_ENC;

alter table OTH_DESICION_INICIAL
   drop constraint FK_OTH_ORDN_TRAB_OTH_DEC_INICI;

alter table OTH_DESICION_INICIAL
   drop constraint FK_OTM_TIPO_OBRA_OTH_DEC_INICIAL;

alter table OTH_INFORME_PRESUPUESTO
   drop constraint FK_OTH_RDN_TRJ_OTH_INF_PRE;

alter table OTH_INFORME_PRESUPUESTO
   drop constraint FK_OTM_UNI_TMP_OTH_INF_PRESUP;

alter table OTH_ORDN_TRAB_DEC_INICIAL
   drop constraint FK_OTH_DEC_INICL_OTH_ORDN_TRB;

alter table OTH_ORDN_TRAB_DEC_INICIAL
   drop constraint FK_OTM_RUBRO_DEC_OTH_ORDN_TRB;

alter table OTT_DESICION_INICIAL
   drop constraint FK_OTM_TIPO_OBRA_OTT_DEC_INICIAL;

alter table OTT_DESICION_INICIAL
   drop constraint FK_OTT_ORDN_TRAB_OTT_DEC_INICI;

alter table OTT_INFORME_PRESUPUESTO
   drop constraint FK_OTM_UNI_TMP_OTT_INF_PRESUP;

alter table OTT_INFORME_PRESUPUESTO
   drop constraint FK_OTT_RDN_TRJ_OTT_INF_PRE;

alter table OTT_ORDN_TRAB_DEC_INICIAL
   drop constraint FK_OTM_RUBRO_DEC_OTT_ORDN_TRB;

alter table OTT_ORDN_TRAB_DEC_INICIAL
   drop constraint FK_OTT_DEC_INICL_OTT_ORDN_TRB;


/*Drops a tablas*/
drop table OTF_EXCEPCION_PERIODO cascade constraints;
drop table OTF_OPERARIO_AREA cascade constraints;
drop table OTH_OPERARIO_ORDEN_TRAB cascade constraints;
drop table OTH_TIEMPO_OPERARIO cascade constraints;
drop table OTH_VIABILIDAD_TECNICA cascade constraints;
drop table OTM_AREA_PROFESIONAL cascade constraints;
drop table OTM_ETAPA_ORDEN_TRABAJO cascade constraints;
drop table OTM_TIPO_DOCUMENTO cascade constraints;
drop table OTM_UNIDAD_TIEMPO cascade constraints;
drop table OTT_OPERARIO_ORDEN_TRAB cascade constraints;
drop table OTT_TIEMPO_OPERARIO cascade constraints;
drop table OTT_VIABILIDAD_TECNICA cascade constraints;
drop table OTT_ANTEPROYECTO cascade constraints;
drop table OTH_ANTEPROYECTO cascade constraints;
drop table OTT_DOCUMENTO_ANTEPROYECT cascade constraints;
drop table OTH_DOCUMENTO_ANTEPROYECT cascade constraints;
drop table OTM_UNIDAD_ENCARGADA cascade constraints;

drop table OTH_DESICION_INICIAL cascade constraints;
drop table OTH_INFORME_PRESUPUESTO cascade constraints;
drop table OTH_ORDN_TRAB_DEC_INICIAL cascade constraints;
drop table OTM_RUBRO_DECISION_INICIA cascade constraints;
drop table OTM_TIPO_OBRA cascade constraints;
drop table OTT_DESICION_INICIAL cascade constraints;
drop table OTT_INFORME_PRESUPUESTO cascade constraints;
drop table OTT_ORDN_TRAB_DEC_INICIAL cascade constraints;
 */

/*Tablas*/


/*==============================================================*/
/* Table: OTF_EXCEPCION_PERIODO                                 */
/*==============================================================*/
create table OTF_EXCEPCION_PERIODO 
(
   ID_EXCEPCION_PERIODO NUMBER(10,0)         not null,
   NUM_EMPLEADO         INTEGER              not null,
   ID_UNIDAD_TIEMPO     NUMBER(10,0)         not null,
   UNIDAD_INTERNA       VARCHAR2(3)          default 'MNT' not null
      constraint CK_UNIDAD_INTERNA_VALOR check (UNIDAD_INTERNA in ('MNT','DIS') and UNIDAD_INTERNA = upper(UNIDAD_INTERNA)),
   VIGENCIA             NUMBER(3,0)          default 1 not null
      constraint CK_VIGENCIA_VALOR_MIN_MAX check (VIGENCIA between 1 and 999),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTF_EXCEPCION_PERIODO primary key (ID_EXCEPCION_PERIODO)
);

comment on table OTF_EXCEPCION_PERIODO is
'Tabla para registrar excepciones para permitie la inclusion de ordenes de trabajo, aun cuando el periodo de cierre para la unidad cierre ya ha finalizado.';

comment on column OTF_EXCEPCION_PERIODO.ID_EXCEPCION_PERIODO is
'Identificador para las excepciones registradas en el sistema de la Tabla OTF_EXCEPCION_PERIODO, que se asocia con la secuencia SQ_ID_EXCEPCION_PERIODO';

comment on column OTF_EXCEPCION_PERIODO.NUM_EMPLEADO is
'Número de emplerado al cual se le habilita la excepción';

comment on column OTF_EXCEPCION_PERIODO.ID_UNIDAD_TIEMPO is
'Llave primaria de la tabla OTM_UNIDAD_TIEMPO que se asocia con la secuencia SQ_ID_UNIDAD_TIEMPO';

comment on column OTF_EXCEPCION_PERIODO.UNIDAD_INTERNA is
'Unidad de la sección de mantenimiento y construcción para la que se registra la excepción - MNT: Mantenimiento, DIS: Diseño - Valor defecto: MNT';

comment on column OTF_EXCEPCION_PERIODO.VIGENCIA is
'Cantidad de unidades de tiempo que estara vigente la excecpión';

comment on column OTF_EXCEPCION_PERIODO.USUARIO is
'Usuario que crea o modifica el registro';

comment on column OTF_EXCEPCION_PERIODO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTF_OPERARIO_AREA                                     */
/*==============================================================*/
create table OTF_OPERARIO_AREA 
(
   NUM_EMPLEADO         INTEGER              not null,
   ID_SECTOR_TALLER     NUMBER(10,0)         not null,
   ID_AREA_PROFESIONAL  NUMBER(10,0),
   CATEGORIA_LABORAL    VARCHAR2(3)          default 'OPE' not null
      constraint CK_CATEGORIA_LABORAL_VALOR check (CATEGORIA_LABORAL in ('OPE','PRO') and CATEGORIA_LABORAL = upper(CATEGORIA_LABORAL)),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_OPERARIO_AREA check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTF_OPERARIO_AREA primary key (NUM_EMPLEADO, ID_SECTOR_TALLER),
   constraint AK_OTM_SECTOR_TALLER unique (NUM_EMPLEADO)
);

comment on table OTF_OPERARIO_AREA is
'Tabla para registrar a los operarios por sector o taller, indicando las respectivas areas profesionales a las que pertencen';

comment on column OTF_OPERARIO_AREA.ID_SECTOR_TALLER is
'Llave primaria de la tabla OTM_SECTOR_TALLER que se asocia con la secuencia SQ_ID_SECTOR_TALLER';

comment on column OTF_OPERARIO_AREA.ID_AREA_PROFESIONAL is
'Llave primaria de la tabla OTM_AREA_PROFESIONAL   que se asocia con la secuencia SQ_ID_AREA_PROFESIONAL';

comment on column OTF_OPERARIO_AREA.CATEGORIA_LABORAL is
'Categoria laboral a la que pertence el funcionario asociado al sector o taller: OPE : OPERARIO PRO: PROFESIONAL';

comment on column OTF_OPERARIO_AREA.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTF_OPERARIO_AREA.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTF_OPERARIO_AREA.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTH_OPERARIO_ORDEN_TRAB                               */
/*==============================================================*/
create table OTH_OPERARIO_ORDEN_TRAB 
(
   NUM_EMPLEADO         INTEGER              not null,
   ID_SECTOR_TALLER     NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ETAPA_ORDEN_TRABAJO NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 not null,
   CARGO                VARCHAR2(3)          default 'OPE' not null
      constraint CK_CARGO_OTH_VALOR_MAY check (CARGO in ('OPE','COL','ENC') and CARGO = upper(CARGO)),
   FECHA_DESDE          DATE,
   FECHA_HASTA          DATE,
   FECHA_PROPUESTA      DATE,
   FECHA_EJECUTA        DATE,
   constraint PK_OTH_OPERARIO_ORDEN_TRAB primary key (NUM_EMPLEADO, ID_SECTOR_TALLER, ID_UBICACION, ID_ETAPA_ORDEN_TRABAJO, ID_ORDEN_TRABAJO)
);

comment on table OTH_OPERARIO_ORDEN_TRAB is
'Tabla para registrar los operarios asociados a una orden de trabajo segun la etapa en la que se haya clasificado. ';

comment on column OTH_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER is
'Llave primaria de la tabla OTM_SECTOR_TALLER que se asocia con la secuencia SQ_ID_SECTOR_TALLER';

comment on column OTH_OPERARIO_ORDEN_TRAB.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column OTH_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_OPERARIO_ORDEN_TRAB.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTH_OPERARIO_ORDEN_TRAB.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema.';

comment on column OTH_OPERARIO_ORDEN_TRAB.CARGO is
'Cargo asociado al funcinario segun el proyecto, OPE: OPERARIO (MANTENIMIENTO), COL: COLABORADOR, ENC: ENCARGADO (DISEÑO)';

comment on column OTH_OPERARIO_ORDEN_TRAB.FECHA_DESDE is
'Fecha desde en la que esta a cargo del proyecto en el caso de las ordenes  de diseño';

comment on column OTH_OPERARIO_ORDEN_TRAB.FECHA_HASTA is
'Fecha hasta en la que esta a cargo del proyetco en el casoi de las ordenes de diseño';

comment on column OTH_OPERARIO_ORDEN_TRAB.FECHA_PROPUESTA is
'Fecha propuesta para que el funcionario realice la ejecución del tiempo que se le asigno.';

comment on column OTH_OPERARIO_ORDEN_TRAB.FECHA_EJECUTA is
'Fecha en la que el funcionario efectua la ejecucion del tiempo que se le asigno par arealizar una tarea.';



/*==============================================================*/
/* Table: OTH_TIEMPO_OPERARIO                                   */
/*==============================================================*/
create table OTH_TIEMPO_OPERARIO 
(
   NUM_EMPLEADO         INTEGER              not null,
   ID_SECTOR_TALLER     NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ETAPA_ORDEN_TRABAJO NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   CLASIFICACION        VARCHAR2(3)          default 'EST' not null
      constraint CK_CLASIFICACION_OTH_VALOR_MAY check (CLASIFICACION in ('EST','RAL') and CLASIFICACION = upper(CLASIFICACION)),
   TIEMPO               NUMBER(8,2)          not null,
   ID_UNIDAD_TIEMPO     NUMBER(10,0)         not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTH_TIEMPO_OPERARIO primary key (NUM_EMPLEADO, ID_SECTOR_TALLER, ID_UBICACION, ID_ETAPA_ORDEN_TRABAJO, ID_ORDEN_TRABAJO, CLASIFICACION)
);

comment on table OTH_TIEMPO_OPERARIO is
'Tabla para registrar los tiempos invertidos por cada uno de los funcionarios asociados a la orden de trabajo.';

comment on column OTH_TIEMPO_OPERARIO.ID_SECTOR_TALLER is
'Llave primaria de la tabla OTM_SECTOR_TALLER que se asocia con la secuencia SQ_ID_SECTOR_TALLER';

comment on column OTH_TIEMPO_OPERARIO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column OTH_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_TIEMPO_OPERARIO.CLASIFICACION is
'Clasificación para el tiempo, EST: ESTIMADO RAL: REAL';

comment on column OTH_TIEMPO_OPERARIO.TIEMPO is
'Cantidad de tiempo invertido ya sea en la estimacion o en la ejecucion del trabajo asignado al operario';

comment on column OTH_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO is
'Llave primaria de la tabla OTM_UNIDAD_TIEMPO que se asocia con la secuencia SQ_ID_UNIDAD_TIEMPO';

comment on column OTH_TIEMPO_OPERARIO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTH_TIEMPO_OPERARIO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTH_VIABILIDAD_TECNICA                                */
/*==============================================================*/
create table OTH_VIABILIDAD_TECNICA 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ID_UNIDAD_TIEMPO     NUMBER(10,0),
   TIEMPO_RESPUESTA     NUMBER(3,0),
   VIABILIDAD           NUMBER(1,0)          default 0 not null
      constraint CK_VIABILIDAD_OTH_VALOR check (VIABILIDAD between 0 and 1),
   ESTIMACION_PRESUPUESTARIA NUMBER(13,2)         not null,
   DETALLE              CLOB                 not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTH_VIABILIDAD_TECNICA primary key (ID_UBICACION, ID_ORDEN_TRABAJO)
);

comment on table OTH_VIABILIDAD_TECNICA is
'Tabla para registrar la viabilidad tecnica posterior al proceso de evaluación efectuado por los profesionales a cargo del proyecto.';

comment on column OTH_VIABILIDAD_TECNICA.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_VIABILIDAD_TECNICA.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_VIABILIDAD_TECNICA.ID_UNIDAD_TIEMPO is
'Llave primaria de la tabla OTM_UNIDAD_TIEMPO que se asocia con la secuencia SQ_ID_UNIDAD_TIEMPO';

comment on column OTH_VIABILIDAD_TECNICA.TIEMPO_RESPUESTA is
'Indicador para la cantidad de tiempo del que dispone el solicitante para responder sobre el informe de viabilidad tecnica del proyecto';

comment on column OTH_VIABILIDAD_TECNICA.VIABILIDAD is
'Indicador para la viabilidad del proyecto 0: No 1:Si';

comment on column OTH_VIABILIDAD_TECNICA.ESTIMACION_PRESUPUESTARIA is
'Estimación presupuestaria para cubrir los gastos del proyecto.';

comment on column OTH_VIABILIDAD_TECNICA.DETALLE is
'Texto enriquesido con el detalle de la evaluación de viabilidad tecnica. ';

comment on column OTH_VIABILIDAD_TECNICA.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTH_VIABILIDAD_TECNICA.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_AREA_PROFESIONAL                                  */
/*==============================================================*/
create table OTM_AREA_PROFESIONAL 
(
   ID_AREA_PROFESIONAL  NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   SUFIJO               VARCHAR2(3)          not null
      constraint CK_SUFIJO_VALOR_MAYUSCULA check (SUFIJO = upper(SUFIJO)),
   DESCRIPCION          VARCHAR2(100)        not null,
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_AREA_PROFESIONAL check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_AREA_PROFESIONAL primary key (ID_AREA_PROFESIONAL)
);

comment on table OTM_AREA_PROFESIONAL is
'Tabla para registar las areas de trabajo, Electrica, Indistrial, Arquietectonica. ';

comment on column OTM_AREA_PROFESIONAL.ID_AREA_PROFESIONAL is
'Llave primaria de la tabla OTM_AREA_PROFESIONAL   que se asocia con la secuencia SQ_ID_AREA_PROFESIONAL';

comment on column OTM_AREA_PROFESIONAL.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTM_AREA_PROFESIONAL.SUFIJO is
'Siglas a asignar al consecutivo de la orden de trabajo madre para crear el consecutivo de la orden de trabajo hija (Exclusivo para ordenes de diseño).';

comment on column OTM_AREA_PROFESIONAL.DESCRIPCION is
'Descricpión del área de servicio. ';

comment on column OTM_AREA_PROFESIONAL.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_AREA_PROFESIONAL.USUARIO is
'Usuario que crea o modifica el registro. ';

comment on column OTM_AREA_PROFESIONAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_ETAPA_ORDEN_TRABAJO                               */
/*==============================================================*/
create table OTM_ETAPA_ORDEN_TRABAJO 
(
   ID_ETAPA_ORDEN_TRABAJO NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   ORDEN                NUMBER(2,0)          default 1 not null
      constraint CKC_ORDEN_OTM_ETAP check (ORDEN between 1 and 99),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_ETAPA_ORDEN_TRAB check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_ETAPA_ORDEN_TRABAJO primary key (ID_ETAPA_ORDEN_TRABAJO)
);

comment on table OTM_ETAPA_ORDEN_TRABAJO is
'Tabla para registrar las posibles estapas en las cuales se puede adjuntar un archivo a la orden de Trabajo';

comment on column OTM_ETAPA_ORDEN_TRABAJO.ID_ETAPA_ORDEN_TRABAJO is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column OTM_ETAPA_ORDEN_TRABAJO.DESCRIPCION is
'Descripción de la etapa a la cual pertenece el registro asociado.';

comment on column OTM_ETAPA_ORDEN_TRABAJO.ORDEN is
'Peso asociado al orden que tendran las etapas indicadas en el catalogo';

comment on column OTM_ETAPA_ORDEN_TRABAJO.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_ETAPA_ORDEN_TRABAJO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_ETAPA_ORDEN_TRABAJO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_TIPO_DOCUMENTO                                    */
/*==============================================================*/
create table OTM_TIPO_DOCUMENTO 
(
   ID_TIPO_DOCUMENTO    NUMBER(10,0)         not null,
   PROTEGIDO            NUMBER(1,0)          default 0 not null
      constraint CKC_PROTEGIDO_OTM_TIPO check (PROTEGIDO between 0 and 1),
   DESCRIPCION          VARCHAR2(256)        not null,
   TAMANIO_MAXIMO       NUMBER(10,0)         not null,
   FORMATOS_ADMITIDOS   VARCHAR2(4000),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_TIPO_DOCUMENTO_VALOR check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_TIPO_DOCUMENTO primary key (ID_TIPO_DOCUMENTO),
   constraint AK_AK_TIPO_DOCUMENTO_OTM_TIPO unique (DESCRIPCION)
);

comment on table OTM_TIPO_DOCUMENTO is
'catalogo para clasificar los tipos de documentacion que se va adjuntando a la orden de trabajo: fotos, oficios, planos en pdf.';

comment on column OTM_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column OTM_TIPO_DOCUMENTO.PROTEGIDO is
'Indicador que especifica si el parámetro se encuentra protegido y no se puede modificar desde un mantenimiento - 0: No es protegido, 1: Es protegido - Valor por defecto: 0';

comment on column OTM_TIPO_DOCUMENTO.DESCRIPCION is
'Descripción del tipo de documento, debe ser unica, por ejemplo; Fotos, Planos, Oficio, Reporte ';

comment on column OTM_TIPO_DOCUMENTO.TAMANIO_MAXIMO is
'Tamaño maximo del archivo cargado por el usuario asociado al tipo de documento';

comment on column OTM_TIPO_DOCUMENTO.FORMATOS_ADMITIDOS is
'Formatos admintidos para el tipo de documento';

comment on column OTM_TIPO_DOCUMENTO.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_TIPO_DOCUMENTO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_TIPO_DOCUMENTO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_UNIDAD_TIEMPO                                     */
/*==============================================================*/
create table OTM_UNIDAD_TIEMPO 
(
   ID_UNIDAD_TIEMPO     NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(50)         not null,
   UNIDAD               NUMBER(2,0)          default 1 not null
      constraint CK_UNIDAD_VALOR_MIN_MAX check (UNIDAD between 1 and 99),
   VALOR                NUMBER(2,0)          default 1 not null
      constraint CK_VALOR_VALOR_MIN_MAX check (VALOR between 1 and 99),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_UNIDAD_TIEMPO check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_UNIDAD_TIEMPO primary key (ID_UNIDAD_TIEMPO),
   constraint AK_OTM_UNIDAD_TIEMPO unique (DESCRIPCION)
);

comment on table OTM_UNIDAD_TIEMPO is
'Tabla para registrar las unidades de tiempo validas en el sistema. ';

comment on column OTM_UNIDAD_TIEMPO.ID_UNIDAD_TIEMPO is
'Llave primaria de la tabla OTM_UNIDAD_TIEMPO que se asocia con la secuencia SQ_ID_UNIDAD_TIEMPO';

comment on column OTM_UNIDAD_TIEMPO.DESCRIPCION is
'Descripción de la unidad de tiempo, debe ser unica, por ejemplo; Semana, Quincena, Mes ';

comment on column OTM_UNIDAD_TIEMPO.UNIDAD is
'Unidad de tiempo que conforma la descripcion indicada';

comment on column OTM_UNIDAD_TIEMPO.VALOR is
'Cantidad de unidades de tiempo que conforman la descripcion indicada';

comment on column OTM_UNIDAD_TIEMPO.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_UNIDAD_TIEMPO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_UNIDAD_TIEMPO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_OPERARIO_ORDEN_TRAB                               */
/*==============================================================*/
create table OTT_OPERARIO_ORDEN_TRAB 
(
   NUM_EMPLEADO         INTEGER              not null,
   ID_SECTOR_TALLER     NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ID_ETAPA_ORDEN_TRABAJO NUMBER(10,0)         not null,
   CARGO                VARCHAR2(3)          default 'OPE' not null
      constraint CK_CARGO_OTT_VALOR_MAY check (CARGO in ('OPE','COL','ENC') and CARGO = upper(CARGO)),
   FECHA_PROPUESTA      DATE,
   FECHA_EJECUTA        DATE,
   FECHA_DESDE          DATE,
   FECHA_HASTA          DATE,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_OPERARIO_ORDEN_TRAB primary key (NUM_EMPLEADO, ID_SECTOR_TALLER, ID_UBICACION, ID_ORDEN_TRABAJO, ID_ETAPA_ORDEN_TRABAJO)
);

comment on table OTT_OPERARIO_ORDEN_TRAB is
'Tabla para registrar los operarios asociados a una orden de trabajo segun la etapa en la que se haya clasificado. ';

comment on column OTT_OPERARIO_ORDEN_TRAB.ID_SECTOR_TALLER is
'Llave primaria de la tabla OTM_SECTOR_TALLER que se asocia con la secuencia SQ_ID_SECTOR_TALLER';

comment on column OTT_OPERARIO_ORDEN_TRAB.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_OPERARIO_ORDEN_TRAB.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_OPERARIO_ORDEN_TRAB.ID_ETAPA_ORDEN_TRABAJO is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column OTT_OPERARIO_ORDEN_TRAB.CARGO is
'Cargo asociado al funcinario segun el proyecto, OPE: OPERARIO (MANTENIMIENTO), COL: COLABORADOR, ENC: ENCARGADO (DISEÑO)';

comment on column OTT_OPERARIO_ORDEN_TRAB.FECHA_PROPUESTA is
'Fecha propuesta para que el funcionario realice la ejecución del tiempo que se le asigno.';

comment on column OTT_OPERARIO_ORDEN_TRAB.FECHA_EJECUTA is
'Fecha en la que el funcionario efectua la ejecucion del tiempo que se le asigno par arealizar una tarea.';

comment on column OTT_OPERARIO_ORDEN_TRAB.FECHA_DESDE is
'Fecha desde en la que esta a cargo del proyecto en el caso de las ordenes  de diseño';

comment on column OTT_OPERARIO_ORDEN_TRAB.FECHA_HASTA is
'Fecha hasta en la que esta a cargo del proyetco en el casoi de las ordenes de diseño';

comment on column OTT_OPERARIO_ORDEN_TRAB.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTT_OPERARIO_ORDEN_TRAB.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema.';

/*==============================================================*/
/* Table: OTT_TIEMPO_OPERARIO                                   */
/*==============================================================*/
create table OTT_TIEMPO_OPERARIO 
(
   NUM_EMPLEADO         INTEGER              not null,
   ID_SECTOR_TALLER     NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ID_ETAPA_ORDEN_TRABAJO NUMBER(10,0)         not null,
   TIEMPO               NUMBER(8,2)          not null,
   ID_UNIDAD_TIEMPO     NUMBER(10,0)         not null,
   CLASIFICACION        VARCHAR2(3)          default 'EST' not null
      constraint CK_CLASIFICACION_OTT_VALOR_MAY check (CLASIFICACION in ('EST','RAL') and CLASIFICACION = upper(CLASIFICACION)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_TIEMPO_OPERARIO primary key (NUM_EMPLEADO, ID_SECTOR_TALLER, ID_UBICACION, ID_ORDEN_TRABAJO, ID_ETAPA_ORDEN_TRABAJO, CLASIFICACION)
);

comment on table OTT_TIEMPO_OPERARIO is
'Tabla para registrar los tiempos invertidos por cada uno de los funcionarios asociados a la orden de trabajo.';

comment on column OTT_TIEMPO_OPERARIO.ID_SECTOR_TALLER is
'Llave primaria de la tabla OTM_SECTOR_TALLER que se asocia con la secuencia SQ_ID_SECTOR_TALLER';

comment on column OTT_TIEMPO_OPERARIO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_TIEMPO_OPERARIO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_TIEMPO_OPERARIO.ID_ETAPA_ORDEN_TRABAJO is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column OTT_TIEMPO_OPERARIO.TIEMPO is
'Cantidad de tiempo invertido ya sea en la estimacion o en la ejecucion del trabajo asignado al operario';

comment on column OTT_TIEMPO_OPERARIO.ID_UNIDAD_TIEMPO is
'Llave primaria de la tabla OTM_UNIDAD_TIEMPO que se asocia con la secuencia SQ_ID_UNIDAD_TIEMPO';

comment on column OTT_TIEMPO_OPERARIO.CLASIFICACION is
'Clasificación para el tiempo, EST: ESTIMADO RAL: REAL';

comment on column OTT_TIEMPO_OPERARIO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTT_TIEMPO_OPERARIO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_VIABILIDAD_TECNICA                                */
/*==============================================================*/
create table OTT_VIABILIDAD_TECNICA 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ID_UNIDAD_TIEMPO     NUMBER(10,0),
   TIEMPO_RESPUESTA     NUMBER(3,0),
   VIABILIDAD           NUMBER(1,0)          default 0 not null
      constraint CK_VIABILIDAD_OTT_VALOR check (VIABILIDAD between 0 and 1),
   ESTIMACION_PRESUPUESTARIA NUMBER(13,2)         not null,
   DETALLE              CLOB                 not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_VIABILIDAD_TECNICA primary key (ID_UBICACION, ID_ORDEN_TRABAJO)
);

comment on table OTT_VIABILIDAD_TECNICA is
'Tabla para registrar la viabilidad tecnica posterior al proceso de evaluación efectuado por los profesionales a cargo del proyecto.';

comment on column OTT_VIABILIDAD_TECNICA.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_VIABILIDAD_TECNICA.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_VIABILIDAD_TECNICA.ID_UNIDAD_TIEMPO is
'Llave primaria de la tabla OTM_UNIDAD_TIEMPO que se asocia con la secuencia SQ_ID_UNIDAD_TIEMPO';

comment on column OTT_VIABILIDAD_TECNICA.TIEMPO_RESPUESTA is
'Indicador para la cantidad de tiempo del que dispone el solicitante para responder sobre el informe de viabilidad tecnica del proyecto';

comment on column OTT_VIABILIDAD_TECNICA.VIABILIDAD is
'Indicador para la viabilidad del proyecto 0: No 1:Si';

comment on column OTT_VIABILIDAD_TECNICA.ESTIMACION_PRESUPUESTARIA is
'Estimación presupuestaria para cubrir los gastos del proyecto.';

comment on column OTT_VIABILIDAD_TECNICA.DETALLE is
'Texto enriquesido con el detalle de la evaluación de viabilidad tecnica. ';

comment on column OTT_VIABILIDAD_TECNICA.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTT_VIABILIDAD_TECNICA.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


/*==============================================================*/
/* Table: OTT_ANTEPROYECTO                                      */
/*
Responsable: Erick Figueroa
Fecha 09/03/2016
Detalle: Se elimina el campo Detalle
Script: Alter Table OTT_ANTEPROYECTO DROP COLUMN DETALLE;
*/
/*==============================================================*/
create table OTT_ANTEPROYECTO 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   VERSION              NUMBER(2,0)          not null,
   EDITABLE             NUMBER(1,0)          not null,
   DESCRIPCION          VARCHAR2(4000)       not null,
   ACTIVIDADES_CONTEMPLADAS VARCHAR2(4000),
   CANTIDAD             NUMBER(5,2)          not null,
   UNIDAD_MEDIDA        VARCHAR2(3)          default 'MTS' not null
      constraint CK_UNIDAD_MEDIDA_OTT_ANTE check (UNIDAD_MEDIDA in ('MTS','MT2','MT3') and UNIDAD_MEDIDA = upper(UNIDAD_MEDIDA)),
   AVAL_PLANTA_FISICA   NUMBER(1,0)          not null,
   AVAL_FORESTA         NUMBER(1,0)          not null,
   TIEMPO_RESPUESTA     NUMBER(3,0),
   ID_UNIDAD_TIEMPO     NUMBER(10,0),
   FECHA_ENVIA          DATE,
   FECHA_RESPONDE       DATE,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_ANTEPROYECTO primary key (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION)
);

comment on table OTT_ANTEPROYECTO is
'Tabla para registrar las diferentes versiones del ante proyecto asociado a una OT de Diseño';

comment on column OTT_ANTEPROYECTO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_ANTEPROYECTO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_ANTEPROYECTO.VERSION is
'Numero de version del anteproyecto asociado a una orden de trabajo';

comment on column OTT_ANTEPROYECTO.EDITABLE is
'Indicador para marcar las vesión del ante proyecto como editables o no editables 0: no editable 1: editable, valor por defecto : 1';

comment on column OTT_ANTEPROYECTO.DESCRIPCION is
'Descripción del trabajo para el cual se elebora el ante proyecto.';

comment on column OTT_ANTEPROYECTO.DETALLE is
'Detalle del trabajo y subtrabajos contemplados en el ante proyecto.';

comment on column OTT_ANTEPROYECTO.ACTIVIDADES_CONTEMPLADAS is
'Listad de actividades contempladas en el ante proyecto, son una guia para generar ordenes de trabajo Hijas.';

comment on column OTT_ANTEPROYECTO.CANTIDAD is
'Cantidad de metros o metros cuadrados que abarcara el proyecto. ';

comment on column OTT_ANTEPROYECTO.UNIDAD_MEDIDA is
'Indicador para metros, metros cuadrados o metros cubicos Valores : MTS, MT2, MT3';

comment on column OTT_ANTEPROYECTO.AVAL_PLANTA_FISICA is
'Indicador para señalar si el proyecto requiere del aval de planta fisica';

comment on column OTT_ANTEPROYECTO.AVAL_FORESTA is
'Indicador para señalar si el proyecto requiere del aval de foresta';

comment on column OTT_ANTEPROYECTO.ID_UNIDAD_TIEMPO is
'Llave primaria de la tabla OTM_UNIDAD_TIEMPO que se asocia con la secuencia SQ_ID_UNIDAD_TIEMPO';


comment on column OTT_ANTEPROYECTO.TIEMPO_RESPUESTA is
'Indicador para la cantidad de tiempo del que dispone el solicitante para responder sobre el informe de viabilidad tecnica del proyecto';

comment on column OTT_ANTEPROYECTO.FECHA_ENVIA is
'Fecha en la que le es enviado al usuario solicitante el anteproyecto para su revisión y aprobación.';

comment on column OTT_ANTEPROYECTO.FECHA_RESPONDE is
'Fecha en la que el usuario solicitante da respuesta a la revisión del ante proyecto';

comment on column OTT_ANTEPROYECTO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTT_ANTEPROYECTO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


/*==============================================================*/
/* Table: OTH_ANTEPROYECTO                                      */
/*
Responsable: Erick Figueroa
Fecha 09/03/2016
Detalle: Se elimina el campo Detalle
Script: Alter Table OTH_ANTEPROYECTO DROP COLUMN DETALLE;
*/
/*==============================================================*/
create table OTH_ANTEPROYECTO 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   VERSION              NUMBER(2,0)          not null,
   EDITABLE             NUMBER(1,0)          not null,
   DESCRIPCION          VARCHAR2(4000)       not null,
   ACTIVIDADES_CONTEMPLADAS VARCHAR2(4000),
   CANTIDAD             NUMBER(5,2)          not null,
   UNIDAD_MEDIDA        VARCHAR2(3)          default 'MTS' not null
      constraint CK_UNIDAD_MEDIDA_OTH_ANTE check (UNIDAD_MEDIDA in ('MTS','MT2','MT3') and UNIDAD_MEDIDA = upper(UNIDAD_MEDIDA)),
   AVAL_PLANTA_FISICA   NUMBER(1,0)          not null,
   AVAL_FORESTA         NUMBER(1,0)          not null,
   TIEMPO_RESPUESTA     NUMBER(3,0),
   ID_UNIDAD_TIEMPO NUMBER(10,0),
   FECHA_ENVIA          DATE,
   FECHA_RESPONDE       DATE,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTH_ANTEPROYECTO primary key (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION)
);

comment on table OTH_ANTEPROYECTO is
'Tabla para registrar las diferentes versiones del ante proyecto asociado a una OT de Diseño';

comment on column OTH_ANTEPROYECTO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_ANTEPROYECTO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_ANTEPROYECTO.VERSION is
'Numero de version del anteproyecto asociado a una orden de trabajo';

comment on column OTH_ANTEPROYECTO.EDITABLE is
'Indicador para marcar las vesión del ante proyecto como editables o no editables 0: no editable 1: editable, valor por defecto : 1';

comment on column OTH_ANTEPROYECTO.DESCRIPCION is
'Descripción del trabajo para el cual se elebora el ante proyecto.';

comment on column OTH_ANTEPROYECTO.DETALLE is
'Detalle del trabajo y subtrabajos contemplados en el ante proyecto.';

comment on column OTH_ANTEPROYECTO.ACTIVIDADES_CONTEMPLADAS is
'Listad de actividades contempladas en el ante proyecto, son una guia para generar ordenes de trabajo Hijas.';

comment on column OTH_ANTEPROYECTO.CANTIDAD is
'Cantidad de metros o metros cuadrados que abarcara el proyecto. ';

comment on column OTH_ANTEPROYECTO.UNIDAD_MEDIDA is
'Indicador para metros, metros cuadrados o metros cubicos Valores : MTS, MT2, MT3';

comment on column OTH_ANTEPROYECTO.AVAL_PLANTA_FISICA is
'Indicador para señalar si el proyecto requiere del aval de planta fisica';

comment on column OTH_ANTEPROYECTO.AVAL_FORESTA is
'Indicador para señalar si el proyecto requiere del aval de foresta';

comment on column OTH_ANTEPROYECTO.TIEMPO_RESPUESTA is
'Indicador para la cantidad de tiempo del que dispone el solicitante para responder sobre el informe de viabilidad tecnica del proyecto';

comment on column OTH_ANTEPROYECTO.ID_UNIDAD_TIEMPO is
'Llave primaria de la tabla OTM_UNIDAD_TIEMPO que se asocia con la secuencia SQ_ID_UNIDAD_TIEMPO';

comment on column OTH_ANTEPROYECTO.FECHA_ENVIA is
'Fecha en la que le es enviado al usuario solicitante el anteproyecto para su revisión y aprobación.';

comment on column OTH_ANTEPROYECTO.FECHA_RESPONDE is
'Fecha en la que el usuario solicitante da respuesta a la revisión del ante proyecto';

comment on column OTH_ANTEPROYECTO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTH_ANTEPROYECTO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';



/*==============================================================*/
/* Table: OTT_DOCUMENTO_ANTEPROYECT                             */
/*==============================================================*/
create table OTT_DOCUMENTO_ANTEPROYECT 
(
   ID_TIPO_DOCUMENTO    NUMBER(10,0)         not null,
   ID_ETAPA_ORDEN_TRABAJO NUMBER(10,0)         not null,
   ID_ADJUNTO_ORDEN_TRABAJO NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   VERSION              NUMBER(2,0)          not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_DOCUMENTO_ANTEPROYECT primary key (ID_UBICACION, ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO, VERSION)
);

comment on table OTT_DOCUMENTO_ANTEPROYECT is
'Tabla para alamacenar la asociacion entre los diferentes documentos y la versión a la cual correspondan ';

comment on column OTT_DOCUMENTO_ANTEPROYECT.ID_TIPO_DOCUMENTO is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column OTT_DOCUMENTO_ANTEPROYECT.ID_ETAPA_ORDEN_TRABAJO is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column OTT_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO is
'Llave primaria de la tabla OTF_ADJUNTO_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ADJUNTO_ORDEN_TRABAJO';

comment on column OTT_DOCUMENTO_ANTEPROYECT.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_DOCUMENTO_ANTEPROYECT.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_DOCUMENTO_ANTEPROYECT.VERSION is
'Numero de version del anteproyecto asociado a una orden de trabajo';

comment on column OTT_DOCUMENTO_ANTEPROYECT.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTT_DOCUMENTO_ANTEPROYECT.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTH_DOCUMENTO_ANTEPROYECT                             */
/*==============================================================*/
create table OTH_DOCUMENTO_ANTEPROYECT 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   VERSION              NUMBER(2,0)          not null,
   ID_TIPO_DOCUMENTO    NUMBER(10,0)         not null,
   ID_ETAPA_ORDEN_TRABAJO NUMBER(10,0)         not null,
   ID_ADJUNTO_ORDEN_TRABAJO NUMBER(10,0)         not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTH_DOCUMENTO_ANTEPROYECT primary key (ID_UBICACION, ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ORDEN_TRABAJO, VERSION, ID_ADJUNTO_ORDEN_TRABAJO)
);

comment on table OTH_DOCUMENTO_ANTEPROYECT is
'Tabla para alamacenar la asociacion entre los diferentes documentos y la versión a la cual correspondan ';

comment on column OTH_DOCUMENTO_ANTEPROYECT.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_DOCUMENTO_ANTEPROYECT.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_DOCUMENTO_ANTEPROYECT.VERSION is
'Numero de version del anteproyecto asociado a una orden de trabajo';

comment on column OTH_DOCUMENTO_ANTEPROYECT.ID_TIPO_DOCUMENTO is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column OTH_DOCUMENTO_ANTEPROYECT.ID_ETAPA_ORDEN_TRABAJO is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column OTH_DOCUMENTO_ANTEPROYECT.ID_ADJUNTO_ORDEN_TRABAJO is
'Llave primaria de la tabla OTF_ADJUNTO_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ADJUNTO_ORDEN_TRABAJO';

comment on column OTH_DOCUMENTO_ANTEPROYECT.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTH_DOCUMENTO_ANTEPROYECT.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';



/*==============================================================*/
/* Table: OTM_UNIDAD_ENCARGADA                                  */
/*==============================================================*/
create table OTM_UNIDAD_ENCARGADA 
(
   ID_LUGAR_TRABAJO     NUMBER(10,0)         not null,
   COD_UNIDAD_SIRH      NUMBER(3,0)          not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_UNIDAD_ENCARGADA primary key (ID_LUGAR_TRABAJO, COD_UNIDAD_SIRH)
);

comment on table OTM_UNIDAD_ENCARGADA is
'Tabla para identificar las unidades encargadas de un determinado edificio o sitio';

comment on column OTM_UNIDAD_ENCARGADA.ID_LUGAR_TRABAJO is
'Llave primaria de la tabla OTM_LUGAR_TRABAJO que se asocia con la secuencia SQ_ID_LUGAR_TRABAJO';

comment on column OTM_UNIDAD_ENCARGADA.COD_UNIDAD_SIRH is
'Código de la unidad académica o administrativa que administra el edificio o sitio y ademas es responsable de la autorización de la orden de trabajo';

comment on column OTM_UNIDAD_ENCARGADA.USUARIO is
'Usuario que crea o modifica el registro. ';

comment on column OTM_UNIDAD_ENCARGADA.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';




/*==============================================================*/
/* Table: OTH_DESICION_INICIAL                                  */
/*==============================================================*/
create table OTH_DESICION_INICIAL 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ID_TIPO_OBRA         NUMBER(10,0)         not null,
   FECHA                DATE                 not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTH_DESICION_INICIAL primary key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_TIPO_OBRA)
);

comment on table OTH_DESICION_INICIAL is
'Tabla para almancenar el encabezado de la decision inicial del proyecto asociado a una orden de trabajo';

comment on column OTH_DESICION_INICIAL.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_DESICION_INICIAL.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_DESICION_INICIAL.ID_TIPO_OBRA is
'Llave primaria de la tabla OTM_RUBRO_DESICION_INICIA que se asocia con la secuencia SQ_RUBRO_DESICION_INICIA';

comment on column OTH_DESICION_INICIAL.FECHA is
'Fecha en la que se Crea la Desición Inicial';

comment on column OTH_DESICION_INICIAL.USUARIO is
'Usuario que crea o modifica el registro. ';

comment on column OTH_DESICION_INICIAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTH_INFORME_PRESUPUESTO                               */
/*==============================================================*/
create table OTH_INFORME_PRESUPUESTO 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ESTIMACION_PRESUPUESTARIA NUMBER(13,2)         not null,
   TIEMPO_RESPUESTA     NUMBER(3,0),
   ID_UNIDAD_TIEMPO     NUMBER(10,0),
   DETALLE              CLOB                 not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTH_INFORME_PRESUPUESTO primary key (ID_UBICACION, ID_ORDEN_TRABAJO)
);

comment on table OTH_INFORME_PRESUPUESTO is
'Tabla para registrar el informe de valoracion presupuestaria elaborado por el profesional de diseño.';

comment on column OTH_INFORME_PRESUPUESTO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_INFORME_PRESUPUESTO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_INFORME_PRESUPUESTO.ESTIMACION_PRESUPUESTARIA is
'presupuesto propuesto para la ejecución del proyecto.';

comment on column OTH_INFORME_PRESUPUESTO.TIEMPO_RESPUESTA is
'Indicador para la cantidad de tiempo del que dispone el solicitante para responder sobre el informe de valoracion presupuestaria del proyecto';

comment on column OTH_INFORME_PRESUPUESTO.ID_UNIDAD_TIEMPO is
'Llave primaria de la tabla OTM_UNIDAD_TIEMPO que se asocia con la secuencia SQ_ID_UNIDAD_TIEMPO';

comment on column OTH_INFORME_PRESUPUESTO.DETALLE is
'Campo para almacenar la redacción referentre a la valoracion presupuestaria indicado por el profesional encargado del proyecto ';

comment on column OTH_INFORME_PRESUPUESTO.USUARIO is
'Usuario que crea o modifica el registro. ';

comment on column OTH_INFORME_PRESUPUESTO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTH_ORDN_TRAB_DEC_INICIAL                             */
/*==============================================================*/
create table OTH_ORDN_TRAB_DEC_INICIAL 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ID_TIPO_OBRA         NUMBER(10,0)         not null,
   ID_RUBRO_DECISION_INICIA NUMBER(10,0)         not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   VALOR                VARCHAR2(4000)       not null,
   constraint PK_OTH_ORDN_TRAB_DEC_INICIAL primary key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_TIPO_OBRA, ID_RUBRO_DECISION_INICIA)
);

comment on table OTH_ORDN_TRAB_DEC_INICIAL is
'Tabla para registrar los rubros de la desicion inicial considerados para una orden de trabajo de diseño ';

comment on column OTH_ORDN_TRAB_DEC_INICIAL.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_ORDN_TRAB_DEC_INICIAL.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_ORDN_TRAB_DEC_INICIAL.ID_TIPO_OBRA is
'Llave primaria de la tabla OTM_RUBRO_DESICION_INICIA que se asocia con la secuencia SQ_RUBRO_DESICION_INICIA';

comment on column OTH_ORDN_TRAB_DEC_INICIAL.ID_RUBRO_DECISION_INICIA is
'Llave primaria de la tabla OTM_RUBRO_DESICION_INICIA que se asocia con la secuencia SQ_RUBRO_DESICION_INICIA';

comment on column OTH_ORDN_TRAB_DEC_INICIAL.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTH_ORDN_TRAB_DEC_INICIAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

comment on column OTH_ORDN_TRAB_DEC_INICIAL.VALOR is
'Valor asociado al rubro contemplado para la desición inicial';

/*==============================================================*/
/* Table: OTM_RUBRO_DECISION_INICIA                             */
/*==============================================================*/
create table OTM_RUBRO_DECISION_INICIA 
(
   ID_RUBRO_DECISION_INICIA NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(4000)       not null,
   ORDEN                NUMBER(3,0)          not null,
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_RUBRO_DEC_INICIAL check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_RUBRO_DECISION_INICIA primary key (ID_RUBRO_DECISION_INICIA)
);

comment on table OTM_RUBRO_DECISION_INICIA is
'Tabla para registrar los diferentes rubros que pueden ser contemplados en la decision inicial del proyecto';

comment on column OTM_RUBRO_DECISION_INICIA.ID_RUBRO_DECISION_INICIA is
'Llave primaria de la tabla OTM_RUBRO_DESICION_INICIA que se asocia con la secuencia SQ_RUBRO_DESICION_INICIA';

comment on column OTM_RUBRO_DECISION_INICIA.DESCRIPCION is
'Descripcipcion del rubro';

comment on column OTM_RUBRO_DECISION_INICIA.ORDEN is
'Peso asociado al orden que tendran las Decisiones indicadas en el catalogo';

comment on column OTM_RUBRO_DECISION_INICIA.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_RUBRO_DECISION_INICIA.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_RUBRO_DECISION_INICIA.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_TIPO_OBRA                                         */
/*==============================================================*/
create table OTM_TIPO_OBRA 
(
   ID_TIPO_OBRA         NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(4000)       not null,
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_TIPO_OBRA check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_TIPO_OBRA primary key (ID_TIPO_OBRA)
);

comment on table OTM_TIPO_OBRA is
'Catalogo para registrar los diferentes tipos de obra en los que se podria catalogar un proyecto para contratación';

comment on column OTM_TIPO_OBRA.ID_TIPO_OBRA is
'Llave primaria de la tabla OTM_RUBRO_DESICION_INICIA que se asocia con la secuencia SQ_RUBRO_DESICION_INICIA';

comment on column OTM_TIPO_OBRA.DESCRIPCION is
'Descripción del tipo de Obra';

comment on column OTM_TIPO_OBRA.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_TIPO_OBRA.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_TIPO_OBRA.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_DESICION_INICIAL                                  */
/*==============================================================*/
create table OTT_DESICION_INICIAL 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ID_TIPO_OBRA         NUMBER(10,0)         not null,
   FECHA                DATE                 not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_DESICION_INICIAL primary key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_TIPO_OBRA)
);

comment on table OTT_DESICION_INICIAL is
'Tabla para almancenar el encabezado de la decision inicial del proyecto asociado a una orden de trabajo';

comment on column OTT_DESICION_INICIAL.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_DESICION_INICIAL.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_DESICION_INICIAL.ID_TIPO_OBRA is
'Llave primaria de la tabla OTM_RUBRO_DESICION_INICIA que se asocia con la secuencia SQ_RUBRO_DESICION_INICIA';

comment on column OTT_DESICION_INICIAL.FECHA is
'Fecha en la que se Crea la Desición Inicial';

comment on column OTT_DESICION_INICIAL.USUARIO is
'Usuario que crea o modifica el registro. ';

comment on column OTT_DESICION_INICIAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_INFORME_PRESUPUESTO                               */
/*==============================================================*/
create table OTT_INFORME_PRESUPUESTO 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ESTIMACION_PRESUPUESTARIA NUMBER(13,2)         not null,
   TIEMPO_RESPUESTA     NUMBER(3,0),
   ID_UNIDAD_TIEMPO     NUMBER(10,0),
   DETALLE              CLOB                 not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_INFORME_PRESUPUESTO primary key (ID_UBICACION, ID_ORDEN_TRABAJO)
);

comment on table OTT_INFORME_PRESUPUESTO is
'Tabla para registrar el informe de valoracion presupuestaria elaborado por el profesional de diseño.';

comment on column OTT_INFORME_PRESUPUESTO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_INFORME_PRESUPUESTO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_INFORME_PRESUPUESTO.ESTIMACION_PRESUPUESTARIA is
'presupuesto propuesto para la ejecución del proyecto.';

comment on column OTT_INFORME_PRESUPUESTO.TIEMPO_RESPUESTA is
'Indicador para la cantidad de tiempo del que dispone el solicitante para responder sobre el informe de valoracion presupuestaria del proyecto';

comment on column OTT_INFORME_PRESUPUESTO.ID_UNIDAD_TIEMPO is
'Llave primaria de la tabla OTM_UNIDAD_TIEMPO que se asocia con la secuencia SQ_ID_UNIDAD_TIEMPO';

comment on column OTT_INFORME_PRESUPUESTO.DETALLE is
'Campo para almacenar la redacción referentre a la valoracion presupuestaria indicado por el profesional encargado del proyecto ';

comment on column OTT_INFORME_PRESUPUESTO.USUARIO is
'Usuario que crea o modifica el registro. ';

comment on column OTT_INFORME_PRESUPUESTO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_ORDN_TRAB_DEC_INICIAL                             */
/*==============================================================*/
create table OTT_ORDN_TRAB_DEC_INICIAL 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ID_TIPO_OBRA         NUMBER(10,0)         not null,
   ID_RUBRO_DECISION_INICIA NUMBER(10,0)         not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   VALOR                VARCHAR2(4000)       not null,
   constraint PK_OTT_ORDN_TRAB_DEC_INICIAL primary key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_TIPO_OBRA, ID_RUBRO_DECISION_INICIA)
);

comment on table OTT_ORDN_TRAB_DEC_INICIAL is
'Tabla para registrar los rubros de la desicion inicial considerados para una orden de trabajo de diseño ';

comment on column OTT_ORDN_TRAB_DEC_INICIAL.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_ORDN_TRAB_DEC_INICIAL.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_ORDN_TRAB_DEC_INICIAL.ID_TIPO_OBRA is
'Llave primaria de la tabla OTM_RUBRO_DESICION_INICIA que se asocia con la secuencia SQ_RUBRO_DESICION_INICIA';

comment on column OTT_ORDN_TRAB_DEC_INICIAL.ID_RUBRO_DECISION_INICIA is
'Llave primaria de la tabla OTM_RUBRO_DESICION_INICIA que se asocia con la secuencia SQ_RUBRO_DESICION_INICIA';

comment on column OTT_ORDN_TRAB_DEC_INICIAL.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTT_ORDN_TRAB_DEC_INICIAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

comment on column OTT_ORDN_TRAB_DEC_INICIAL.VALOR is
'Valor asociado al rubro contemplado para la desición inicial';

alter table OTH_DESICION_INICIAL
   add constraint FK_OTH_ORDN_TRAB_OTH_DEC_INICI foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTH_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTH_DESICION_INICIAL
   add constraint FK_OTM_TIPO_OBRA_OTH_DEC_INIC foreign key (ID_TIPO_OBRA)
      references OTM_TIPO_OBRA (ID_TIPO_OBRA);

alter table OTH_INFORME_PRESUPUESTO
   add constraint FK_OTH_RDN_TRJ_OTH_INF_PRE foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTH_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTH_INFORME_PRESUPUESTO
   add constraint FK_OTM_UNI_TMP_OTH_INF_PRESUP foreign key (ID_UNIDAD_TIEMPO)
      references OTM_UNIDAD_TIEMPO (ID_UNIDAD_TIEMPO);

alter table OTH_ORDN_TRAB_DEC_INICIAL
   add constraint FK_OTH_DEC_INICL_OTH_ORDN_TRB foreign key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_TIPO_OBRA)
      references OTH_DESICION_INICIAL (ID_UBICACION, ID_ORDEN_TRABAJO, ID_TIPO_OBRA);

alter table OTH_ORDN_TRAB_DEC_INICIAL
   add constraint FK_OTM_RUBRO_DEC_OTH_ORDN_TRB foreign key (ID_RUBRO_DECISION_INICIA)
      references OTM_RUBRO_DECISION_INICIA (ID_RUBRO_DECISION_INICIA);

alter table OTT_DESICION_INICIAL
   add constraint FK_OTM_TIPO_OBRA_OTT_DEC_INIC foreign key (ID_TIPO_OBRA)
      references OTM_TIPO_OBRA (ID_TIPO_OBRA);

alter table OTT_DESICION_INICIAL
   add constraint FK_OTT_ORDN_TRAB_OTT_DEC_INICI foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTT_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTT_INFORME_PRESUPUESTO
   add constraint FK_OTM_UNI_TMP_OTT_INF_PRESUP foreign key (ID_UNIDAD_TIEMPO)
      references OTM_UNIDAD_TIEMPO (ID_UNIDAD_TIEMPO);

alter table OTT_INFORME_PRESUPUESTO
   add constraint FK_OTT_RDN_TRJ_OTT_INF_PRE foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTT_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTT_ORDN_TRAB_DEC_INICIAL
   add constraint FK_OTM_RUBRO_DEC_OTT_ORDN_TRB foreign key (ID_RUBRO_DECISION_INICIA)
      references OTM_RUBRO_DECISION_INICIA (ID_RUBRO_DECISION_INICIA);

alter table OTT_ORDN_TRAB_DEC_INICIAL
   add constraint FK_OTT_DEC_INICL_OTT_ORDN_TRB foreign key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_TIPO_OBRA)
      references OTT_DESICION_INICIAL (ID_UBICACION, ID_ORDEN_TRABAJO, ID_TIPO_OBRA);

alter table OTM_UNIDAD_ENCARGADA
   add constraint FK_OTM_LUGAR_TRAB_OTM_UNID_ENC foreign key (ID_LUGAR_TRABAJO)
      references OTM_LUGAR_TRABAJO (ID_LUGAR_TRABAJO);



alter table OTT_DOCUMENTO_ANTEPROYECT
   add constraint FK_OTT_ADJ_OT_OTT_DOC_ANTEPRO foreign key (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO)
      references OTT_ADJUNTO_ORDEN_TRABAJO (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO);

alter table OTT_DOCUMENTO_ANTEPROYECT
   add constraint FK_OTT_ANTEPRO_OTT_DOC_ANTEPRO foreign key (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION)
      references OTT_ANTEPROYECTO (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION);

alter table OTH_DOCUMENTO_ANTEPROYECT
   add constraint FK_OTH_ADJ_OT_OTH_DOC_ANTEPRO foreign key (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO)
      references OTH_ADJUNTO_ORDEN_TRABAJO (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO);

alter table OTH_DOCUMENTO_ANTEPROYECT
   add constraint FK_OTH_ANTEPRO_OTH_DOC_ANTEPRO foreign key (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION)
      references OTH_ANTEPROYECTO(ID_UBICACION, ID_ORDEN_TRABAJO, VERSION);

alter table OTH_ANTEPROYECTO
   add constraint FK_OTH_ORDEN_TR_OTH_ANTEPROYCT foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTH_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTH_ANTEPROYECTO
   add constraint FK_OTH_ANTE_FK_OTM_UN_OTM_UNID foreign key (ID_UNIDAD_TIEMPO)
      references OTM_UNIDAD_TIEMPO (ID_UNIDAD_TIEMPO);

alter table OTT_ANTEPROYECTO
   add constraint FK_OTT_ORDEN_TR_OTT_ANTEPROYCT foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTT_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTT_ANTEPROYECTO
   add constraint FK_OTM_UNI_TMP_OTT_ANTEPROYECT foreign key (ID_UNIDAD_TIEMPO)
      references OTM_UNIDAD_TIEMPO (ID_UNIDAD_TIEMPO);

alter table OTF_EXCEPCION_PERIODO
   add constraint FK_EU_EMPLEADO_OTF_EXC_PER foreign key (NUM_EMPLEADO)
      references EU_EMPLEADOS (NUM_EMPLEADO);

alter table OTF_EXCEPCION_PERIODO
   add constraint FK_OTM_UNID_TIEMP_OTF_EXC_PER foreign key (ID_UNIDAD_TIEMPO)
      references OTM_UNIDAD_TIEMPO (ID_UNIDAD_TIEMPO);

alter table OTF_OPERARIO_AREA
   add constraint FK_EU_EMPLEADOS_OTF_OPER_AREA foreign key (NUM_EMPLEADO)
      references EU_EMPLEADOS (NUM_EMPLEADO);

alter table OTF_OPERARIO_AREA
   add constraint FK_OTM_AREA_PROF_OTF_OPER_AREA foreign key (ID_AREA_PROFESIONAL)
      references OTM_AREA_PROFESIONAL (ID_AREA_PROFESIONAL);

alter table OTF_OPERARIO_AREA
   add constraint FK_OTM_SEC_TALLR_OTF_OPER_AREA foreign key (ID_SECTOR_TALLER)
      references OTM_SECTOR_TALLER (ID_SECTOR_TALLER);

alter table OTH_OPERARIO_ORDEN_TRAB
   add constraint FK_ATH_ORDEN_ATH_OPER_ORD_TRJO foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTH_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTH_OPERARIO_ORDEN_TRAB
   add constraint FK_OTF_OPR_AREA_OTH_OPR_ORD_TR foreign key (NUM_EMPLEADO, ID_SECTOR_TALLER)
      references OTF_OPERARIO_AREA (NUM_EMPLEADO, ID_SECTOR_TALLER);

alter table OTH_OPERARIO_ORDEN_TRAB
   add constraint FK_OTM_TP_RDN_TRJ_OTH_OPER_RDN foreign key (ID_ETAPA_ORDEN_TRABAJO)
      references OTM_ETAPA_ORDEN_TRABAJO (ID_ETAPA_ORDEN_TRABAJO);

alter table OTH_TIEMPO_OPERARIO
   add constraint FK_OTM_UNI_TMP_OTH_TMP_OPER foreign key (ID_UNIDAD_TIEMPO)
      references OTM_UNIDAD_TIEMPO (ID_UNIDAD_TIEMPO);

alter table OTH_TIEMPO_OPERARIO
   add constraint FK_OTH_OPR_DN_TRJ_OTH_TMP_OPR foreign key (NUM_EMPLEADO, ID_SECTOR_TALLER, ID_UBICACION, ID_ETAPA_ORDEN_TRABAJO, ID_ORDEN_TRABAJO)
      references OTH_OPERARIO_ORDEN_TRAB (NUM_EMPLEADO, ID_SECTOR_TALLER, ID_UBICACION, ID_ETAPA_ORDEN_TRABAJO, ID_ORDEN_TRABAJO);

alter table OTH_VIABILIDAD_TECNICA
   add constraint FK_OTH_RDN_TRJ_OTH_VBL_TEC foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTH_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTH_VIABILIDAD_TECNICA
   add constraint FK_OTM_UNI_TMP_OTH_VIABLDD_TEC foreign key (ID_UNIDAD_TIEMPO)
      references OTM_UNIDAD_TIEMPO (ID_UNIDAD_TIEMPO);

alter table OTM_AREA_PROFESIONAL
   add constraint FK_OTM_UBICACION_OTF_AREA_PROF foreign key (ID_UBICACION)
      references OTM_UBICACION (ID_UBICACION);

alter table OTT_OPERARIO_ORDEN_TRAB
   add constraint FK_OTF_OPR_AREA_OTT_OPR_ORD_TR foreign key (NUM_EMPLEADO, ID_SECTOR_TALLER)
      references OTF_OPERARIO_AREA (NUM_EMPLEADO, ID_SECTOR_TALLER);

alter table OTT_OPERARIO_ORDEN_TRAB
   add constraint FK_OTM_TP_RDN_TRJ_OTT_OPER_RDN foreign key (ID_ETAPA_ORDEN_TRABAJO)
      references OTM_ETAPA_ORDEN_TRABAJO (ID_ETAPA_ORDEN_TRABAJO);

alter table OTT_OPERARIO_ORDEN_TRAB
   add constraint FK_OTT_ORDEN_OTT_OPER_ORD_TRJO foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTT_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTT_TIEMPO_OPERARIO
   add constraint FK_OTM_UNI_TMP_OTT_TMP_OPER foreign key (ID_UNIDAD_TIEMPO)
      references OTM_UNIDAD_TIEMPO (ID_UNIDAD_TIEMPO);

alter table OTT_TIEMPO_OPERARIO
   add constraint FK_OTT_OPR_DN_TRJ_OTT_TMP_OPR foreign key (NUM_EMPLEADO, ID_SECTOR_TALLER, ID_UBICACION, ID_ORDEN_TRABAJO, ID_ETAPA_ORDEN_TRABAJO)
      references OTT_OPERARIO_ORDEN_TRAB (NUM_EMPLEADO, ID_SECTOR_TALLER, ID_UBICACION, ID_ORDEN_TRABAJO, ID_ETAPA_ORDEN_TRABAJO);

alter table OTT_VIABILIDAD_TECNICA
   add constraint FK_OTM_UNI_TMP_OTT_VIABLDD_TEC foreign key (ID_UNIDAD_TIEMPO)
      references OTM_UNIDAD_TIEMPO (ID_UNIDAD_TIEMPO);

alter table OTT_VIABILIDAD_TECNICA
   add constraint FK_OTT_RDN_TRJ_OTT_VBL_TEC foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTT_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

/*Llaves foraneas a tablas de adjuntos historicas y transaccionales+*/
	  	  alter table OTT_ADJUNTO_ORDEN_TRABAJO
   add constraint FK_OTM_ETAPA_OTT_ADJ_ORD_TRB foreign key (ID_ETAPA_ORDEN_TRABAJO)
      references OTM_ETAPA_ORDEN_TRABAJO (ID_ETAPA_ORDEN_TRABAJO);

	  alter table OTT_ADJUNTO_ORDEN_TRABAJO
   add constraint FK_OTM_TIP_DOC_OTT_ADJ_ORD_TRB foreign key (ID_TIPO_DOCUMENTO)
      references OTM_TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO);

	  	  alter table OTH_ADJUNTO_ORDEN_TRABAJO
   add constraint FK_OTM_ETAPA_OTH_ADJ_ORD_TRB foreign key (ID_ETAPA_ORDEN_TRABAJO)
      references OTM_ETAPA_ORDEN_TRABAJO (ID_ETAPA_ORDEN_TRABAJO);

	  alter table OTH_ADJUNTO_ORDEN_TRABAJO
   add constraint FK_OTM_TIP_DOC_OTH_ADJ_ORD_TRB foreign key (ID_TIPO_DOCUMENTO)
      references OTM_TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO);

/*===============================================================*/
/*				TABLAS Ajuste Pre Ordenes de Trabajo			 */
/*Fecha: 08-04-2016												 */
/*===============================================================*/
/*drop table OTF_FICHA_TECNICA_DETALLE cascade constraints;

drop table OTF_FICHA_TECNICA_ESPACIO cascade constraints;

drop table OTF_FICHA_TECNICA_GENERAL cascade constraints;

drop table OTF_FICHA_TECNICA_SUBCOMP cascade constraints;

drop table OTF_PRE_ORDEN_TRABAJO cascade constraints;

drop table OTF_REVISION_PRE_ORDEN_TRA cascade constraints;

alter table OTF_FICHA_TECNICA_DETALLE
   drop constraint FK_OTF_FIC_SUB_OTF_FIC_TEC_DET;

alter table OTF_FICHA_TECNICA_DETALLE
   drop constraint FK_OTM_REQ_OTF_FIC_TEC_DET;

alter table OTF_FICHA_TECNICA_ESPACIO
   drop constraint FK_OTF_PRE_OT_OTF_FIC_TEC_ESP;

alter table OTF_FICHA_TECNICA_ESPACIO
   drop constraint FK_OTM_ESPACIO_OTF_FIC_TEC_ESP;

alter table OTF_FICHA_TECNICA_GENERAL
   drop constraint FK_OTF_PRE_OT_OTF_FIC_TEC_GNRL;

alter table OTF_FICHA_TECNICA_SUBCOMP
   drop constraint FK_OTF_FIC_ESP_OTF_FIC_TEC_SUB;

alter table OTF_FICHA_TECNICA_SUBCOMP
   drop constraint FK_OTM_SUBCOMP_OTF_FIC_TEC_SUB;

alter table OTF_PRE_ORDEN_TRABAJO
   drop constraint FK_EU_EMPLEADOS_OTF_PRE_OT;

alter table OTF_PRE_ORDEN_TRABAJO
   drop constraint FK_OMT_ACTIVIDAD_OTF_PRE_OT;

alter table OTF_PRE_ORDEN_TRABAJO
   drop constraint FK_OTM_LUGAR_TRAB_OTF_PRE_OT;

alter table OTF_PRE_ORDEN_TRABAJO
   drop constraint FK_OTM_UBICACION_OTF_PRE_OT;

alter table OTF_REVISION_PRE_ORDEN_TRA
   drop constraint FK_PRE_OT_OTF_REV_OT;
   */


   
/*==============================================================*/
/* Table: OTF_FICHA_TECNICA_DETALLE                             */
/*==============================================================*/
create table OTF_FICHA_TECNICA_DETALLE 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_PRE_ORDEN_TRABAJO NUMBER(10,0)  not null,
   ID_ESPACIO           NUMBER(10,0)         not null,
   ID_SUBCOMPONENTE     NUMBER(10,0)         not null,
   ID_REQUERIMIENTO     NUMBER(10,0)         not null,
   VALOR                VARCHAR2(10)         not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTF_FICHA_TECNICA_DETALLE primary key (ID_UBICACION, ID_PRE_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE, ID_REQUERIMIENTO)
);

comment on table OTF_FICHA_TECNICA_DETALLE is
'Tabla que registra los valores de llenado de la ficha técnica de una orden de trabajo en trámite';

comment on column OTF_FICHA_TECNICA_DETALLE.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTF_FICHA_TECNICA_DETALLE.ID_PRE_ORDEN_TRABAJO is
'Llave primaria de la tabla OTF_PRE_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_PRE_ORDEN_TRABAJO';

comment on column OTF_FICHA_TECNICA_DETALLE.ID_ESPACIO is
'Llave primaria de la tabla OTM_ESPACIO que se asocia con la secuencia SQ_ID_ESPACIO';

comment on column OTF_FICHA_TECNICA_DETALLE.ID_SUBCOMPONENTE is
'Llave primaria de la tabla OTM_SUBCOMPONENTE que se asocia con la secuencia SQ_ID_SUBCOMPONENTE';

comment on column OTF_FICHA_TECNICA_DETALLE.ID_REQUERIMIENTO is
'Llave primaria de la tabla OTM_REQUERIMIENTO que se asocia con la secuencia SQ_ID_REQUERIMIENTO';

comment on column OTF_FICHA_TECNICA_DETALLE.VALOR is
'Valor registrado por el usuario';

comment on column OTF_FICHA_TECNICA_DETALLE.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTF_FICHA_TECNICA_DETALLE.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTF_FICHA_TECNICA_ESPACIO                             */
/*==============================================================*/
create table OTF_FICHA_TECNICA_ESPACIO 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_PRE_ORDEN_TRABAJO NUMBER(10,0)  not null,
   ID_ESPACIO           NUMBER(10,0)         not null,
   constraint PK_OTF_FICHA_TECNICA_ESPACIO primary key (ID_UBICACION, ID_PRE_ORDEN_TRABAJO, ID_ESPACIO)
);

comment on table OTF_FICHA_TECNICA_ESPACIO is
'Tabla para registrar los espacios que indica el solicitante en la ficha técnica';

comment on column OTF_FICHA_TECNICA_ESPACIO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTF_FICHA_TECNICA_ESPACIO.ID_PRE_ORDEN_TRABAJO is
'Llave primaria de la tabla OTF_PRE_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_PRE_ORDEN_TRABAJO';

comment on column OTF_FICHA_TECNICA_ESPACIO.ID_ESPACIO is
'Llave primaria de la tabla OTM_ESPACIO que se asocia con la secuencia SQ_ID_ESPACIO';

/*==============================================================*/
/* Table: OTF_FICHA_TECNICA_GENERAL                             */
/*==============================================================*/
create table OTF_FICHA_TECNICA_GENERAL 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_PRE_ORDEN_TRABAJO NUMBER(10,0)  not null,
   CONSERVA_MOBILIARIO  NUMBER(1,0)          default 0 not null
      constraint CKC_CONSERVA_MOBILIAR_OTF_FICH check (CONSERVA_MOBILIARIO in (0,1)),
   REQUIERE_NUEVO_MOBILIARIO NUMBER(1,0)          default 0 not null
      constraint CKC_REQUIERE_NUEVO_MO_OTF_FICH check (REQUIERE_NUEVO_MOBILIARIO in (0,1)),
   OTROS_MOBILIARIO     VARCHAR2(1000),
   OTRO_TIPO_REQUERIMIENTO VARCHAR2(1000),
   NOMBRE_ARCHIVO       VARCHAR2(100),
   ARCHIVO              BLOB,
   CUENTA_CON_ALARMA    NUMBER(1,0)          default 0 not null
      constraint CKC_CUENTA_CON_ALARMA_OTF_FICH check (CUENTA_CON_ALARMA in (0,1)),
   REQUIERE_ALARMA      NUMBER(1,0)          default 0 not null
      constraint CKC_REQUIERE_ALARMA_OTF_FICH check (REQUIERE_ALARMA in (0,1)),
   TIME_STAMP           DATE                 default SYSDATE not null,
   USUARIO              VARCHAR2(256)        not null,
   constraint PK_OTF_FICHA_TECNICA_GENERAL primary key (ID_UBICACION, ID_PRE_ORDEN_TRABAJO)
);

comment on table OTF_FICHA_TECNICA_GENERAL is
'Tabla para registrar los datos generales de una ficha técnica que complementa los datos de una orden de trabajo de diseño en PreSolicitud. Aplica sólo para ordenes de trabajo de diseño.';

comment on column OTF_FICHA_TECNICA_GENERAL.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTF_FICHA_TECNICA_GENERAL.ID_PRE_ORDEN_TRABAJO is
'Llave primaria de la tabla OTF_PRE_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_PRE_ORDEN_TRABAJO';

comment on column OTF_FICHA_TECNICA_GENERAL.CONSERVA_MOBILIARIO is
'Indicador de si conserva mobiliario - 0:No, 1: Sí, Valor defecto: 0:No -';

comment on column OTF_FICHA_TECNICA_GENERAL.REQUIERE_NUEVO_MOBILIARIO is
'Indicador de si requiere nuevo mobiliario - 0:No, 1: Sí, Valor defecto: 0:No -';

comment on column OTF_FICHA_TECNICA_GENERAL.OTROS_MOBILIARIO is
'Otros requerimientos en relación con el mobiliario solicitado';

comment on column OTF_FICHA_TECNICA_GENERAL.OTRO_TIPO_REQUERIMIENTO is
'Detalle de otros tipos de requerimientos';

comment on column OTF_FICHA_TECNICA_GENERAL.NOMBRE_ARCHIVO is
'Nombre del archivo adjunto';

comment on column OTF_FICHA_TECNICA_GENERAL.ARCHIVO is
'Documento adjunto correspondiente a una lista del equipo que posea la unidad, su estado actual y el equipo que se proyecte adquirir a corto plazo';

comment on column OTF_FICHA_TECNICA_GENERAL.CUENTA_CON_ALARMA is
'Indicador de si cuenta con alarma - 0:No, 1: Sí, Valor defecto: 0:No -';

comment on column OTF_FICHA_TECNICA_GENERAL.REQUIERE_ALARMA is
'Indicador de si requiere alarma - 0:No, 1: Sí, Valor defecto: 0:No -';

comment on column OTF_FICHA_TECNICA_GENERAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

comment on column OTF_FICHA_TECNICA_GENERAL.USUARIO is
'Usuario que crea o modifica el registro.';

/*==============================================================*/
/* Table: OTF_FICHA_TECNICA_SUBCOMP                             */
/*==============================================================*/
create table OTF_FICHA_TECNICA_SUBCOMP 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_PRE_ORDEN_TRABAJO NUMBER(10,0) not null,
   ID_ESPACIO           NUMBER(10,0)         not null,
   ID_SUBCOMPONENTE     NUMBER(10,0)         not null,
   constraint PK_OTF_FICHA_TECNICA_SUBCOMP primary key (ID_UBICACION, ID_PRE_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE)
);

comment on table OTF_FICHA_TECNICA_SUBCOMP is
'Tabla para registrar los subcomponentes por espacio que indica el solicitante en la ficha técnica';

comment on column OTF_FICHA_TECNICA_SUBCOMP.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTF_FICHA_TECNICA_SUBCOMP.ID_PRE_ORDEN_TRABAJO is
'Llave primaria de la tabla OTF_PRE_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_PRE_ORDEN_TRABAJO';

comment on column OTF_FICHA_TECNICA_SUBCOMP.ID_ESPACIO is
'Llave primaria de la tabla OTM_ESPACIO que se asocia con la secuencia SQ_ID_ESPACIO';

comment on column OTF_FICHA_TECNICA_SUBCOMP.ID_SUBCOMPONENTE is
'Llave primaria de la tabla OTM_SUBCOMPONENTE que se asocia con la secuencia SQ_ID_SUBCOMPONENTE';

/*==============================================================*/
/* Table: OTF_PRE_ORDEN_TRABAJO                                 */
/*==============================================================*/
create table OTF_PRE_ORDEN_TRABAJO 
(
   ID_PRE_ORDEN_TRABAJO NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_LUGAR_TRABAJO     NUMBER(10,0)         not null,
   ID_CATEGORIA_SERVICIO NUMBER(10,0)         not null,
   ID_ACTIVIDAD         NUMBER(10,0)         not null,
   NUM_EMPLEADO         INTEGER              not null,
   ANNO                 NUMBER(4,0)          not null
      constraint CKC_ANNO_OTF_PRE_ check (ANNO >= 0),
   FECHA_HORA_SOLICITA  DATE                 default SYSDATE not null,
   COD_UNIDAD_SIRH      NUMBER(3,0)          not null,
   INCLUIDA_EN_RECEPCION NUMBER(1) DEFAULT 0 NOT NULL,
   ID_UBICACION_ORIGEN NUMBER(10,0),
   NOMBRE_PERSONA_CONTACTO VARCHAR2(200),
   TELEFONO             VARCHAR2(10),
   SENNAS_EXACTAS       VARCHAR2(1000)       not null,
   DESCRIPCION_TRABAJO  VARCHAR2(2000)       not null,
   NOMBRE_IMAGEN1       VARCHAR2(100),
   IMAGEN1              BLOB,
   NOMBRE_IMAGEN2       VARCHAR2(100),
   IMAGEN2              BLOB,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTF_PRE_ORDEN_TRABAJO primary key (ID_UBICACION, ID_PRE_ORDEN_TRABAJO)
);

comment on table OTF_PRE_ORDEN_TRABAJO is
'Tabla para registrar las ordenes de trabajo propuestas por los funcionarios de la UCR para gestionar una orden de Trabajo. 
';

comment on column OTF_PRE_ORDEN_TRABAJO.ID_PRE_ORDEN_TRABAJO is
'Llave primaria de la tabla OTF_PRE_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_PRE_ORDEN_TRABAJO';

comment on column OTF_PRE_ORDEN_TRABAJO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTF_PRE_ORDEN_TRABAJO.ID_LUGAR_TRABAJO is
'Llave primaria de la tabla OTM_LUGAR_TRABAJO que se asocia con la secuencia SQ_ID_LUGAR_TRABAJO';

comment on column OTF_PRE_ORDEN_TRABAJO.ID_CATEGORIA_SERVICIO is
'Llave primaria de la tabla OTM_CATEGORIA_SERVICIO que se asocia con la secuencia SQ_ID_CATEGORIA_SERVICIO';

comment on column OTF_PRE_ORDEN_TRABAJO.ID_ACTIVIDAD is
'Llave primaria de la tabla OTM_ACTIVIDAD que se asocia con la secuencia SQ_ID_ACTIVIDAD';

comment on column OTF_PRE_ORDEN_TRABAJO.ANNO is
'Año de solicitud de la OT';

comment on column OTF_PRE_ORDEN_TRABAJO.FECHA_HORA_SOLICITA is
'Fecha y hora en que el solicitante registra la orden de trabajo.';

comment on column OTF_PRE_ORDEN_TRABAJO.COD_UNIDAD_SIRH is
'Código de la unidad académica o administrativa que tramita la autorización de la orden de trabajo';

comment on column OTF_PRE_ORDEN_TRABAJO.NOMBRE_PERSONA_CONTACTO is
'Nombre completo y apellidos de la persona que podrá ser contactada para evacuar dudas de la orden de trabajo solicitada';

comment on column OTF_PRE_ORDEN_TRABAJO.TELEFONO is
'Teléfono mediante el cual se podrá contactar al solicitante o a la persona contacto';

comment on column OTF_PRE_ORDEN_TRABAJO.SENNAS_EXACTAS is
'Señas exactas del lugar donde se requiere la realización de trabajo';

comment on column OTF_PRE_ORDEN_TRABAJO.DESCRIPCION_TRABAJO is
'Descripción detallada del trabajo requerido';

comment on column OTF_PRE_ORDEN_TRABAJO.NOMBRE_IMAGEN1 is
'Campo para almacenar el nombre de un archivo asociado a la solicitud ';

comment on column OTF_PRE_ORDEN_TRABAJO.IMAGEN1 is
' archivo asociado a la solicitud ';

comment on column OTF_PRE_ORDEN_TRABAJO.NOMBRE_IMAGEN2 is
'Campo para almacenar el nombre de un segundo archivo asociado a la solicitud ';

comment on column OTF_PRE_ORDEN_TRABAJO.IMAGEN2 is
'segundo archivo asociado a la solicitud ';

comment on column OTF_PRE_ORDEN_TRABAJO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTF_PRE_ORDEN_TRABAJO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTF_REVISION_PRE_ORDEN_TRA                            */
/*==============================================================*/
create table OTF_REVISION_PRE_ORDEN_TRA 
(
   ID_REVISION_PRE_ORDEN_TRA NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_PRE_ORDEN_TRABAJO NUMBER(10,0)   not null,
   OBSERVACIONES        VARCHAR2(2000)       not null,
   ESTADO               VARCHAR2(3)          not null
      constraint CKC_ESTADO_OTF_REVI check (ESTADO in ('APR','DEV','DEN') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTF_REVISION_PRE_ORDEN_TRA primary key (ID_REVISION_PRE_ORDEN_TRA)
);

comment on table OTF_REVISION_PRE_ORDEN_TRA is
'Tabla para registrar las revisiones que realiza un autorizado a la solicitud de orden de trabajo';

comment on column OTF_REVISION_PRE_ORDEN_TRA.ID_REVISION_PRE_ORDEN_TRA is
'Llave primaria de la tabla OTF_REVISION_ORDEN_TRABAJ que se asocia con la secuencia SQ_ID_REVISION_ORDEN_TRABAJ';

comment on column OTF_REVISION_PRE_ORDEN_TRA.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTF_REVISION_PRE_ORDEN_TRA.ID_PRE_ORDEN_TRABAJO is
'Llave primaria de la tabla OTF_PRE_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_PRE_ORDEN_TRABAJO';

comment on column OTF_REVISION_PRE_ORDEN_TRA.OBSERVACIONES is
'Observaciones indicadas por el revisor';

comment on column OTF_REVISION_PRE_ORDEN_TRA.ESTADO is
'Estado del registro - APR: Aprueba, DEV: Devuelve, DEN: Deniega';

comment on column OTF_REVISION_PRE_ORDEN_TRA.USUARIO is
'Usuario que realiza la revisión';

comment on column OTF_REVISION_PRE_ORDEN_TRA.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

alter table OTF_FICHA_TECNICA_DETALLE
   add constraint FK_OTF_FIC_SUB_OTF_FIC_TEC_DET foreign key (ID_UBICACION, ID_PRE_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE)
      references OTF_FICHA_TECNICA_SUBCOMP (ID_UBICACION, ID_PRE_ORDEN_TRABAJO, ID_ESPACIO, ID_SUBCOMPONENTE);

alter table OTF_FICHA_TECNICA_DETALLE
   add constraint FK_OTM_REQ_OTF_FIC_TEC_DET foreign key (ID_REQUERIMIENTO)
      references OTM_REQUERIMIENTO (ID_REQUERIMIENTO);

alter table OTF_FICHA_TECNICA_ESPACIO
   add constraint FK_OTF_PRE_OT_OTF_FIC_TEC_ESP foreign key (ID_UBICACION, ID_PRE_ORDEN_TRABAJO)
      references OTF_PRE_ORDEN_TRABAJO (ID_UBICACION, ID_PRE_ORDEN_TRABAJO);

alter table OTF_FICHA_TECNICA_ESPACIO
   add constraint FK_OTM_ESPACIO_OTF_FIC_TEC_ESP foreign key (ID_ESPACIO)
      references OTM_ESPACIO (ID_ESPACIO);

alter table OTF_FICHA_TECNICA_GENERAL
   add constraint FK_OTF_PRE_OT_OTF_FIC_TEC_GNRL foreign key (ID_UBICACION, ID_PRE_ORDEN_TRABAJO)
      references OTF_PRE_ORDEN_TRABAJO (ID_UBICACION, ID_PRE_ORDEN_TRABAJO);

alter table OTF_FICHA_TECNICA_SUBCOMP
   add constraint FK_OTF_FIC_ESP_OTF_FIC_TEC_SUB foreign key (ID_UBICACION, ID_PRE_ORDEN_TRABAJO, ID_ESPACIO)
      references OTF_FICHA_TECNICA_ESPACIO (ID_UBICACION, ID_PRE_ORDEN_TRABAJO, ID_ESPACIO);

alter table OTF_FICHA_TECNICA_SUBCOMP
   add constraint FK_OTM_SUBCOMP_OTF_FIC_TEC_SUB foreign key (ID_ESPACIO, ID_SUBCOMPONENTE)
      references OTM_SUBCOMPONENTE (ID_ESPACIO, ID_SUBCOMPONENTE);

alter table OTF_PRE_ORDEN_TRABAJO
   add constraint FK_EU_EMPLEADOS_OTF_PRE_OT foreign key (NUM_EMPLEADO)
      references EU_EMPLEADOS (NUM_EMPLEADO);

alter table OTF_PRE_ORDEN_TRABAJO
   add constraint FK_OMT_ACTIVIDAD_OTF_PRE_OT foreign key (ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD)
      references OTM_ACTIVIDAD (ID_CATEGORIA_SERVICIO, ID_ACTIVIDAD);

alter table OTF_PRE_ORDEN_TRABAJO
   add constraint FK_OTM_LUGAR_TRAB_OTF_PRE_OT foreign key (ID_LUGAR_TRABAJO)
      references OTM_LUGAR_TRABAJO (ID_LUGAR_TRABAJO);

alter table OTF_PRE_ORDEN_TRABAJO
   add constraint FK_OTM_UBICACION_OTF_PRE_OT foreign key (ID_UBICACION)
      references OTM_UBICACION (ID_UBICACION);

alter table OTF_REVISION_PRE_ORDEN_TRA
   add constraint FK_PRE_OT_OTF_REV_OT foreign key (ID_UBICACION, ID_PRE_ORDEN_TRABAJO)
      references OTF_PRE_ORDEN_TRABAJO (ID_UBICACION, ID_PRE_ORDEN_TRABAJO);

/*===============================================================*/
/*				Etapa II Extensión Contrataciones		         */
/*Fecha:	13-04-16													 */
/*===============================================================*/

alter table OTH_CONTRATACION
   drop constraint FK_OTM_VIA_COMP_OTH_CONT;  
   
alter table OTM_ETAPA_VIA_CONTRATO
   drop constraint FK_OTM_VIA_COMP_OTM_ETAPA;

alter table OTM_VIA_COMPRA_CONTRATO
   drop constraint FK_OTM_UBICACION_OTM_VIA_COMP;

alter table OTT_CONTRATACION
   drop constraint FK_OTM_VIA_COMPRA_OTT_CONTR;

drop table OTM_VIA_COMPRA_CONTRATO cascade constraints;

alter table OTM_ENCARGADO_CONTRATO
   drop constraint FK_EU_EMPLEADOS_OTM_ENCARGADO;

alter table OTM_ENCARGADO_CONTRATO
   drop constraint FK_OTM_UBICACION_OTM_ENCARG_CO;

drop table OTM_ENCARGADO_CONTRATO cascade constraints;

alter table OTT_CONTRATACION
   drop constraint FK_OTT_ORDEN_TRABAJO_OTT_CONT;

/*alter table OTT_CONTRATACION
   drop constraint FK_OTM_VIA_CONTRATO_OTT_CONTR;*/

alter table OTT_DOCUMENTO_CONTRATACION
   drop constraint FK_OTT_CONTRATACION_OTT_DOC;

alter table OTT_LINEA_ADJUDICACION
   drop constraint FK_OTT_CONTRATACION_OTT_LINEA;

drop table OTT_CONTRATACION cascade constraints;

alter table OTM_ETAPA_VIA_CONTRATO
   drop constraint FK_OTM_ETAPA_CONT_OTM_ETAPA_V;
   
/*alter table OTM_ETAPA_VIA_CONTRATO
   drop constraint FK_OTM_VIA_COMP_OTM_ETAPA;*/

drop table OTM_ETAPA_VIA_CONTRATO cascade constraints;

alter table OTH_DOCUMENTO_CONTRATACION
   drop constraint FK_OTM_ETAPA_OTH_DOCUMENTO;

alter table OTM_ETAPA_CONTRATACION
   drop constraint FK_OTC_ESTADO_OT_OTM_ETAPA_CON;

/*alter table OTM_ETAPA_VIA_CONTRATO
   drop constraint FK_OTM_ETAPA_CONT_OTM_ETAPA_V;*/

alter table OTT_DOCUMENTO_CONTRATACION
   drop constraint FK_OTM_ETAPA_CONT_OTT_DOCUM;

drop table OTM_ETAPA_CONTRATACION cascade constraints;

alter table OTT_DOCUMENTO_CONTRATACION
   drop constraint FK_OTM_ETAPA_CONT_OTT_DOCUM;

alter table OTT_DOCUMENTO_CONTRATACION
   drop constraint FK_OTT_CONTRATACION_OTT_DOC;

alter table OTT_DOCUMENTO_CONTRATACION
   drop constraint FK_OTT_ADJUNTO_OT_OTT_DOCUMENT;

alter table OTT_DOCUMENTO_CONTRATACION
   drop constraint FK_OTT_LINEA_OTT_DOCUMENTO;

drop table OTT_DOCUMENTO_CONTRATACION cascade constraints;

alter table OTT_DOCUMENTO_CONTRATACION
   drop constraint FK_OTT_LINEA_OTT_DOCUMENTO;

alter table OTT_LINEA_ADJUDICACION
   drop constraint FK_OTT_CONTRATACION_OTT_LINEA;

drop table OTT_LINEA_ADJUDICACION cascade constraints;

alter table OTH_DOCUMENTO_CONTRATACION
   drop constraint FK_OTH_LINEA_AD_OTH_DOCUMENTO;

alter table OTH_LINEA_ADJUDICACION
   drop constraint FK_OHT_CONTRATACION_OTH_LINEA;

drop table OTH_LINEA_ADJUDICACION cascade constraints;

alter table OTH_DOCUMENTO_CONTRATACION
   drop constraint FK_OTH_CONTRATACION_OTH_DOCUM;

alter table OTH_DOCUMENTO_CONTRATACION
   drop constraint FK_OTM_ETAPA_OTH_DOCUMENTO;

alter table OTH_DOCUMENTO_CONTRATACION
   drop constraint FK_OTH_LINEA_AD_OTH_DOCUMENTO;

alter table OTH_DOCUMENTO_CONTRATACION
   drop constraint FK_OTH_ADJUNTO_OTH_DOCUMENTO;

drop table OTH_DOCUMENTO_CONTRATACION cascade constraints;



alter table OTH_CONTRATACION
   drop constraint FK_OTH_ORDEN_TRABAJO_OTH_CON;

alter table OTH_DOCUMENTO_CONTRATACION
   drop constraint FK_OTH_CONTRATACION_OTH_DOCUM;

alter table OTH_LINEA_ADJUDICACION
   drop constraint FK_OHT_CONTRATACION_OTH_LINEA;

drop table OTH_CONTRATACION cascade constraints;

/*==============================================================*/
/* Table: OTM_VIA_CONTRATO : 16-05-16: Se cambia el nombre y se agrega el campo AMBITO    */
/*==============================================================*/
/*==============================================================*/
/* Table: OTM_VIA_COMPRA_CONTRATO                               */
/*==============================================================*/
create table OTM_VIA_COMPRA_CONTRATO 
(
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   TOPE_ECONOMICO       NUMBER(13,2)         not null
      constraint CK_TOPE_ECONOMICO_MINIMO check (TOPE_ECONOMICO >= 0),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_VIA_COMPRA check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   AMBITO               VARCHAR2(3)          default 'CON' not null
      constraint CK_AMBITO check (AMBITO in ('CON','COM','AMB') and AMBITO = upper(AMBITO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_VIA_COMPRA_CONTRATO primary key (ID_VIA_COMPRA_CONTRATO),
   constraint AK_OTM_VIA_COMPRA_CONTRATO unique (DESCRIPCION, ID_UBICACION)
);

comment on table OTM_VIA_COMPRA_CONTRATO is
'Tabla para almacenar los tipos de vías de compras o contratos utilizadas en el proceso de contrataciones';

comment on column OTM_VIA_COMPRA_CONTRATO.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_CONTRATO';

comment on column OTM_VIA_COMPRA_CONTRATO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTM_VIA_COMPRA_CONTRATO.DESCRIPCION is
'Descripción de la vía de contrato.';

comment on column OTM_VIA_COMPRA_CONTRATO.TOPE_ECONOMICO is
'Tope económico establecido para el tipo de vía de contrato';

comment on column OTM_VIA_COMPRA_CONTRATO.ESTADO is
'Estado del registro - ACT: ACTIVA, INA: INACTIVA';

comment on column OTM_VIA_COMPRA_CONTRATO.AMBITO is
'Ámbito que abarca: CON - contrataciones, COM - compras, AMB - ambos';

comment on column OTM_VIA_COMPRA_CONTRATO.USUARIO is
'Usuario';

comment on column OTM_VIA_COMPRA_CONTRATO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


/*==============================================================*/
/* Table: OTM_ENCARGADO_CONTRATO                                */
/*==============================================================*/
create table OTM_ENCARGADO_CONTRATO 
(
   NUM_EMPLEADO         INTEGER              not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_ENCARGADO check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_ENCARGADO_CONTRATO primary key (NUM_EMPLEADO, ID_UBICACION)
);

comment on table OTM_ENCARGADO_CONTRATO is
'Tabla para registrar a los funcionarios encargados del proceso de contrataciones de las ordenes de trabajo de diseño y construcción';

comment on column OTM_ENCARGADO_CONTRATO.NUM_EMPLEADO is
'Número de empleado del funcionario encargado del proceso de contrataciones';

comment on column OTM_ENCARGADO_CONTRATO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTM_ENCARGADO_CONTRATO.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_ENCARGADO_CONTRATO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_ENCARGADO_CONTRATO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


/*==============================================================*/
/* Table: OTT_CONTRATACION                                      */
/*==============================================================*/
create table OTT_CONTRATACION 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   VERSION              NUMBER(2,0)          not null
      constraint CK_VERSION_CONTRATO check (VERSION >= 1),
   ID_VIA_CONTRATO      NUMBER(10,0)         not null,
   EDITABLE             NUMBER(1,0)          default 1 not null
      constraint CK_EDITABLE_CONTRATO check (EDITABLE in (0,1)),
   NUMERO_SOLICITUD     VARCHAR2(15),
   NUMERO_DECISION_INICIAL VARCHAR2(15),
   NUMERO_CONTRATO      VARCHAR2(50),
   NOMBRE_CONTRATO      VARCHAR2(512),
   OBSERVACIONES        VARCHAR2(2000),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_CONTRATACION primary key (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION)
);

comment on table OTT_CONTRATACION is
'Tabla para registrar la información general del proceso de contratación para proyectos de diseño y construcción';

comment on column OTT_CONTRATACION.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_CONTRATACION.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_CONTRATACION.VERSION is
'Numero de version del proceso de contratación asociado a una orden de trabajo';

comment on column OTT_CONTRATACION.ID_VIA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_CONTRATO';

comment on column OTT_CONTRATACION.EDITABLE is
'Indicador para marcar la vesión de la contratación como editables o no editable 0: no editable 1: editable, valor por defecto : 1';

comment on column OTT_CONTRATACION.NUMERO_SOLICITUD is
'Número de solicitud registrado en GECO';

comment on column OTT_CONTRATACION.NUMERO_DECISION_INICIAL is
'Número de decisión inicial registrado en GECO';

comment on column OTT_CONTRATACION.NUMERO_CONTRATO is
'Número de contrato. Ejemplo: 2015CD-000224-OSG';

comment on column OTT_CONTRATACION.NOMBRE_CONTRATO is
'Nombre del contrato';

comment on column ORDENES_TRABAJO.OTT_CONTRATACION.OBSERVACIONES is
'Observaciones';

comment on column OTT_CONTRATACION.USUARIO is
'Usuario';

comment on column OTT_CONTRATACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_ETAPA_VIA_CONTRATO                                */
/*==============================================================*/
create table OTM_ETAPA_VIA_CONTRATO 
(
   ID_VIA_CONTRATO      NUMBER(10,0)         not null,
   ID_ETAPA_CONTRATACION NUMBER(10,0)         not null,
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_ETAPA_VIA check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_ETAPA_VIA_CONTRATO primary key (ID_ETAPA_CONTRATACION, ID_VIA_CONTRATO)
);

comment on table OTM_ETAPA_VIA_CONTRATO is
'Tabla para relacionar las etapas de la contratación con la respectiva vía de contrato';

comment on column OTM_ETAPA_VIA_CONTRATO.ID_VIA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_CONTRATO';

comment on column OTM_ETAPA_VIA_CONTRATO.ID_ETAPA_CONTRATACION is
'Llave primaria de la tabla OTM_ETAPA_CONTRATACION que se asocia con la secuencia SQ_ID_ETAPA_CONTRATACION';

comment on column OTM_ETAPA_VIA_CONTRATO.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_ETAPA_VIA_CONTRATO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_ETAPA_VIA_CONTRATO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_ETAPA_CONTRATACION                                */
/*==============================================================*/
create table OTM_ETAPA_CONTRATACION 
(
   ID_ETAPA_CONTRATACION NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   ESTADO_ORDEN_TRABAJO VARCHAR2(3)          not null
      constraint CK_ESTADO_ORDEN_TRB check (ESTADO_ORDEN_TRABAJO = upper(ESTADO_ORDEN_TRABAJO)),
   ORDEN                NUMBER(2,0)          default 1 not null
      constraint CK_ORDEN_ETAPA_CONT check (ORDEN between 1 and 99),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_ETAPA_CONT check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_ETAPA_CONTRATACION primary key (ID_ETAPA_CONTRATACION)
);

comment on table OTM_ETAPA_CONTRATACION is
'Tabla para registrar las posibles estapas que lleva el proceso de contratación de una orden de trabajo de diseño y construcción';

comment on column OTM_ETAPA_CONTRATACION.ID_ETAPA_CONTRATACION is
'Llave primaria de la tabla OTM_ETAPA_CONTRATACION que se asocia con la secuencia SQ_ID_ETAPA_CONTRATACION';

comment on column OTM_ETAPA_CONTRATACION.DESCRIPCION is
'Descripción de la etapa a la cual pertenece el registro asociado.';

comment on column OTM_ETAPA_CONTRATACION.ESTADO_ORDEN_TRABAJO is
'Llave primaria de la tabla OTC_ESTADO_ORDEN_TRABAJO';

comment on column OTM_ETAPA_CONTRATACION.ORDEN is
'Peso asociado al orden que tendran las etapas indicadas en el catalogo';

comment on column OTM_ETAPA_CONTRATACION.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_ETAPA_CONTRATACION.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_ETAPA_CONTRATACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


/*==============================================================*/
/* Table: OTT_DOCUMENTO_CONTRATACION                            */
/*==============================================================*/
create table OTT_DOCUMENTO_CONTRATACION 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   VERSION              NUMBER(2,0)          not null
      constraint CK_VERSION_DOCUMENTO check (VERSION >= 1),
   ID_TIPO_DOCUMENTO    NUMBER(10,0)         not null,
   ID_ETAPA_ORDEN_TRABAJO NUMBER(10,0)         not null,
   ID_ADJUNTO_ORDEN_TRABAJO NUMBER(10,0)         not null,
   NUMERO_LINEA         NUMBER(2,0),
   ID_ETAPA_CONTRATACION NUMBER(10,0)         not null,
   DOCUMENTO_TRAMITADO  NUMBER(1,0)          default 0 not null
      constraint CK_DOCUMENTO_TRAMITADO check (DOCUMENTO_TRAMITADO in (0,1)),
   ORIGEN               VARCHAR2(3)         
      constraint CK_ORIGEN_DOCUMENTO check (ORIGEN is null or (ORIGEN in ('ENC','PRO') and ORIGEN = upper(ORIGEN))),
   FECHA_HORA_REGISTRO  DATE                 not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_DOCUMENTO_CONTRATACION primary key (ID_UBICACION, ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ORDEN_TRABAJO, VERSION, ID_ADJUNTO_ORDEN_TRABAJO)
);

comment on table OTT_DOCUMENTO_CONTRATACION is
'Tabla para registrar los documentos asociados a una versión de contratación y a cada etapa.';

comment on column OTT_DOCUMENTO_CONTRATACION.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_DOCUMENTO_CONTRATACION.VERSION is
'Numero de version del proceso de contratación asociado a una orden de trabajo';

comment on column OTT_DOCUMENTO_CONTRATACION.ID_TIPO_DOCUMENTO is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column OTT_DOCUMENTO_CONTRATACION.ID_ETAPA_ORDEN_TRABAJO is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column OTT_DOCUMENTO_CONTRATACION.ID_ADJUNTO_ORDEN_TRABAJO is
'Llave primaria de la tabla OTF_ADJUNTO_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ADJUNTO_ORDEN_TRABAJO';

comment on column OTT_DOCUMENTO_CONTRATACION.NUMERO_LINEA is
'Número de línea adjudicada';

comment on column OTT_DOCUMENTO_CONTRATACION.ID_ETAPA_CONTRATACION is
'Llave primaria de la tabla OTM_ETAPA_CONTRATACION que se asocia con la secuencia SQ_ID_ETAPA_CONTRATACION';

comment on column OTT_DOCUMENTO_CONTRATACION.DOCUMENTO_TRAMITADO is
'Indicador de sí el documento adjunto ya fue tramitado por el encargado';

comment on column OTT_DOCUMENTO_CONTRATACION.ORIGEN is
'Indica si el archivo lo registró el encargado o el profesional';

comment on column OTT_DOCUMENTO_CONTRATACION.FECHA_HORA_REGISTRO is
'Fecha y hora de registro del documento';

comment on column OTT_DOCUMENTO_CONTRATACION.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTT_DOCUMENTO_CONTRATACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


/*==============================================================*/
/* Table: OTT_LINEA_ADJUDICACION                                */
/*==============================================================*/
create table OTT_LINEA_ADJUDICACION 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   VERSION              NUMBER(2,0)          not null
      constraint CK_VERSION_LINEA check (VERSION >= 1),
   NUMERO_LINEA         NUMBER(2,0)          not null,
   MONTO_ADJUDICADO     NUMBER(13,2)         not null
      constraint CK_MONTO_ADJUDICADO check (MONTO_ADJUDICADO >= 1),
   ADJUDICATARIO        VARCHAR2(256)        not null,
   FECHA_INICIO_OBRA    DATE                 not null,
   PLAZO_EN_DIAS        NUMBER(4,0)          not null
      constraint CK_PLAZO_EN_DIAS check (PLAZO_EN_DIAS >= 1),
   FORMA_CALCULO_DIAS   VARCHAR2(3)          default 'NAT' not null
      constraint CK_FORMA_CALCULO_DIAS check (FORMA_CALCULO_DIAS in ('NAT','HAB') and FORMA_CALCULO_DIAS = upper(FORMA_CALCULO_DIAS)),
   FECHA_FIN_ESTIMADA   DATE                 not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_LINEA_ADJUDICACION primary key (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION, NUMERO_LINEA)
);

comment on table OTT_LINEA_ADJUDICACION is
'Tabla para registrar las líneas adjudicadas en un proceso de contratación de un proyecto de diseño y construccion.';

comment on column OTT_LINEA_ADJUDICACION.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_LINEA_ADJUDICACION.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_LINEA_ADJUDICACION.VERSION is
'Numero de version del proceso de contratación asociado a una orden de trabajo';

comment on column OTT_LINEA_ADJUDICACION.NUMERO_LINEA is
'Número de línea adjudicada';

comment on column OTT_LINEA_ADJUDICACION.MONTO_ADJUDICADO is
'Monto adjudicado en la línea';

comment on column OTT_LINEA_ADJUDICACION.ADJUDICATARIO is
'Nombre del adjudicatario de la obra';

comment on column OTT_LINEA_ADJUDICACION.FECHA_INICIO_OBRA is
'Fecha de inicio de la obra';

comment on column OTT_LINEA_ADJUDICACION.PLAZO_EN_DIAS is
'Plazo en días para finalización de la obra';

comment on column OTT_LINEA_ADJUDICACION.FORMA_CALCULO_DIAS is
'Forma de cálculo para fijación de la fecha de finalización. NAT - Días naturales, HAB - días hábiles. Valor por defecto: NAT';

comment on column OTT_LINEA_ADJUDICACION.FECHA_FIN_ESTIMADA is
'Fecha calculada por el sistema para finalización de la obra. Se puede ver afectada por días de cierre institucional.';

comment on column OTT_LINEA_ADJUDICACION.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTT_LINEA_ADJUDICACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


/*==============================================================*/
/* Table: OTH_LINEA_ADJUDICACION                                */
/*==============================================================*/
create table OTH_LINEA_ADJUDICACION 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   VERSION              NUMBER(2,0)          not null
      constraint CK_VERSION_LINEA_HIST check (VERSION >= 1),
   NUMERO_LINEA         NUMBER(2,0)          not null,
   MONTO_ADJUDICADO     NUMBER(13,2)         not null
      constraint CK_MONTO_ADJUDICADO_HIST check (MONTO_ADJUDICADO >= 1),
   ADJUDICATARIO        VARCHAR2(256)        not null,
   FECHA_INICIO_OBRA    DATE                 not null,
   PLAZO_EN_DIAS        NUMBER(4,0)          not null
      constraint CK_PLAZO_EN_DIAS_HIST check (PLAZO_EN_DIAS >= 1),
   FORMA_CALCULO_DIAS   VARCHAR2(3)          default 'NAT' not null
      constraint CK_FORMA_CALCULO_HIST check (FORMA_CALCULO_DIAS in ('NAT','HAB') and FORMA_CALCULO_DIAS = upper(FORMA_CALCULO_DIAS)),
   FECHA_FIN_ESTIMADA   DATE                 not null,
   FECHA_FIN_REAL       DATE                 not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTH_LINEA_ADJUDICACION primary key (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION, NUMERO_LINEA)
);

comment on table OTH_LINEA_ADJUDICACION is
'Tabla para registrar la información histórica de las líneas adjudicadas en un proceso de contratación de un proyecto de diseño y construccion.';

comment on column OTH_LINEA_ADJUDICACION.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_LINEA_ADJUDICACION.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_LINEA_ADJUDICACION.VERSION is
'Numero de version del proceso de contratación asociado a una orden de trabajo';

comment on column OTH_LINEA_ADJUDICACION.NUMERO_LINEA is
'Número de línea adjudicada';

comment on column OTH_LINEA_ADJUDICACION.MONTO_ADJUDICADO is
'Monto adjudicado en la línea';

comment on column OTH_LINEA_ADJUDICACION.ADJUDICATARIO is
'Nombre del adjudicatario de la obra';

comment on column OTH_LINEA_ADJUDICACION.FECHA_INICIO_OBRA is
'Fecha de inicio de la obra';

comment on column OTH_LINEA_ADJUDICACION.PLAZO_EN_DIAS is
'Plazo en días para finalización de la obra';

comment on column OTH_LINEA_ADJUDICACION.FORMA_CALCULO_DIAS is
'Forma de cálculo para fijación de la fecha de finalización. NAT - Días naturales, HAB - días hábiles. Valor por defecto: NAT';

comment on column OTH_LINEA_ADJUDICACION.FECHA_FIN_ESTIMADA is
'Fecha calculada por el sistema para finalización de la obra. Se puede ver afectada por días de cierre institucional.';

comment on column OTH_LINEA_ADJUDICACION.FECHA_FIN_REAL is
'Fecha real de finalización de la obra.';

comment on column OTH_LINEA_ADJUDICACION.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTH_LINEA_ADJUDICACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


/*==============================================================*/
/* Table: OTH_DOCUMENTO_CONTRATACION                            */
/*==============================================================*/
create table OTH_DOCUMENTO_CONTRATACION 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   VERSION              NUMBER(2,0)          not null
      constraint CK_VERSION_DOCUM_HIST check (VERSION >= 1),
   ID_TIPO_DOCUMENTO    NUMBER(10,0)         not null,
   ID_ETAPA_ORDEN_TRABAJO NUMBER(10,0)         not null,
   ID_ADJUNTO_ORDEN_TRABAJO NUMBER(10,0)         not null,
   NUMERO_LINEA         NUMBER(2,0),
   ID_ETAPA_CONTRATACION NUMBER(10,0)         not null,
   DOCUMENTO_TRAMITADO  NUMBER(1,0)          default 0 not null
      constraint CK_DOCUMENTO_TRAMITA_HIST check (DOCUMENTO_TRAMITADO in (0,1)),
   ORIGEN               VARCHAR2(3)         
      constraint CK_ORIGEN_HISTORICO check (ORIGEN is null or (ORIGEN in ('ENC','PRO') and ORIGEN = upper(ORIGEN))),
   FECHA_HORA_REGISTRO  DATE                 not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTH_DOCUMENTO_CONTRATACION primary key (ID_UBICACION, ID_ORDEN_TRABAJO, ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, VERSION, ID_ADJUNTO_ORDEN_TRABAJO)
);

comment on table OTH_DOCUMENTO_CONTRATACION is
'Tabla para registrar el historial de los documentos asociados a una versión de contratación y a cada etapa.';

comment on column OTH_DOCUMENTO_CONTRATACION.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_DOCUMENTO_CONTRATACION.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_DOCUMENTO_CONTRATACION.VERSION is
'Numero de version del proceso de contratación asociado a una orden de trabajo';

comment on column OTH_DOCUMENTO_CONTRATACION.ID_TIPO_DOCUMENTO is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column OTH_DOCUMENTO_CONTRATACION.ID_ETAPA_ORDEN_TRABAJO is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column OTH_DOCUMENTO_CONTRATACION.ID_ADJUNTO_ORDEN_TRABAJO is
'Llave primaria de la tabla OTF_ADJUNTO_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ADJUNTO_ORDEN_TRABAJO';

comment on column OTH_DOCUMENTO_CONTRATACION.NUMERO_LINEA is
'Número de línea adjudicada';

comment on column OTH_DOCUMENTO_CONTRATACION.ID_ETAPA_CONTRATACION is
'Llave primaria de la tabla OTM_ETAPA_CONTRATACION que se asocia con la secuencia SQ_ID_ETAPA_CONTRATACION';

comment on column OTH_DOCUMENTO_CONTRATACION.DOCUMENTO_TRAMITADO is
'Indicador de sí el documento adjunto ya fue tramitado por el encargado';

comment on column OTH_DOCUMENTO_CONTRATACION.ORIGEN is
'Indica si el archivo lo registró el encargado o el profesional';

comment on column OTH_DOCUMENTO_CONTRATACION.FECHA_HORA_REGISTRO is
'Fecha y hora de registro del documento';

comment on column OTH_DOCUMENTO_CONTRATACION.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTH_DOCUMENTO_CONTRATACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


/*==============================================================*/
/* Table: OTH_CONTRATACION                                      */
/*==============================================================*/
create table OTH_CONTRATACION 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   VERSION              NUMBER(2,0)          not null
      constraint CK_VERSION_MIN_HIS check (VERSION >= 1),
   ID_VIA_CONTRATO      NUMBER(10,0)         not null,
   EDITABLE             NUMBER(1,0)          default 1 not null
      constraint CK_EDITABLE_CONT_HIST check (EDITABLE in (0,1)),
   NUMERO_SOLICITUD     VARCHAR2(15),
   NUMERO_DECISION_INICIAL VARCHAR2(15),
   NUMERO_CONTRATO      VARCHAR2(50),
   NOMBRE_CONTRATO      VARCHAR2(512),
    OBSERVACIONES        VARCHAR2(2000),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTH_CONTRATACION primary key (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION)
);

comment on table OTH_CONTRATACION is
'Tabla para registrar la información histórica general del proceso de contratación para proyectos de diseño y construcción';

comment on column OTH_CONTRATACION.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTH_CONTRATACION.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTH_CONTRATACION.VERSION is
'Numero de version del proceso de contratación asociado a una orden de trabajo';

comment on column OTH_CONTRATACION.ID_VIA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_CONTRATO';

comment on column OTH_CONTRATACION.EDITABLE is
'Indicador para marcar la vesión de la contratación como editables o no editable 0: no editable 1: editable, valor por defecto : 1';

comment on column OTH_CONTRATACION.NUMERO_SOLICITUD is
'Número de solicitud registrado en GECO';

comment on column OTH_CONTRATACION.NUMERO_DECISION_INICIAL is
'Número de decisión inicial registrado en GECO';

comment on column OTH_CONTRATACION.NUMERO_CONTRATO is
'Número de contrato. Ejemplo: 2015CD-000224-OSG';

comment on column OTH_CONTRATACION.NOMBRE_CONTRATO is
'Nombre del contrato';

comment on column ORDENES_TRABAJO.OTH_CONTRATACION.OBSERVACIONES is
'Observaciones';

comment on column OTH_CONTRATACION.USUARIO is
'Usuario';

comment on column OTH_CONTRATACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

alter table OTM_VIA_COMPRA_CONTRATO
   add constraint FK_OTM_UBICACION_OTM_VIA_COMP foreign key (ID_UBICACION)
      references OTM_UBICACION (ID_UBICACION);

alter table OTM_ENCARGADO_CONTRATO
   add constraint FK_EU_EMPLEADOS_OTM_ENCARGADO foreign key (NUM_EMPLEADO)
      references EU_EMPLEADOS (NUM_EMPLEADO);

alter table OTM_ENCARGADO_CONTRATO
   add constraint FK_OTM_UBICACION_OTM_ENCARG_CO foreign key (ID_UBICACION)
      references OTM_UBICACION (ID_UBICACION);


alter table OTT_CONTRATACION
   add constraint FK_OTT_ORDEN_TRABAJO_OTT_CONT foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTT_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

 alter table OTT_CONTRATACION
   add constraint FK_OTM_VIA_COMPRA_OTT_CONTR foreign key (ID_VIA_CONTRATO)
      references OTM_VIA_COMPRA_CONTRATO (ID_VIA_COMPRA_CONTRATO);
      
alter table OTM_ETAPA_VIA_CONTRATO
   add constraint FK_OTM_ETAPA_CONT_OTM_ETAPA_V foreign key (ID_ETAPA_CONTRATACION)
      references OTM_ETAPA_CONTRATACION (ID_ETAPA_CONTRATACION);
      
alter table OTM_ETAPA_VIA_CONTRATO
   add constraint FK_OTM_VIA_COMP_OTM_ETAPA foreign key (ID_VIA_CONTRATO)
      references OTM_VIA_COMPRA_CONTRATO (ID_VIA_COMPRA_CONTRATO);

/*alter table OTT_CONTRATACION
   add constraint FK_OTM_VIA_CONTRATO_OTT_CONTR foreign key (ID_VIA_CONTRATO)
      references OTM_VIA_COMPRA_CONTRATO (ID_VIA_COMPRA_CONTRATO);*/

alter table OTM_ETAPA_CONTRATACION
   add constraint FK_OTC_ESTADO_OT_OTM_ETAPA_CON foreign key (ESTADO_ORDEN_TRABAJO)
      references OTC_ESTADO_ORDEN_TRABAJO (ESTADO_ORDEN_TRABAJO);

alter table OTT_DOCUMENTO_CONTRATACION
   add constraint FK_OTM_ETAPA_CONT_OTT_DOCUM foreign key (ID_ETAPA_CONTRATACION)
      references OTM_ETAPA_CONTRATACION (ID_ETAPA_CONTRATACION);

alter table OTT_DOCUMENTO_CONTRATACION
   add constraint FK_OTT_CONTRATACION_OTT_DOC foreign key (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION)
      references OTT_CONTRATACION (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION);

alter table OTT_DOCUMENTO_CONTRATACION
   add constraint FK_OTT_ADJUNTO_OT_OTT_DOCUMENT foreign key (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO)
      references OTT_ADJUNTO_ORDEN_TRABAJO (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO);

alter table OTT_DOCUMENTO_CONTRATACION
   add constraint FK_OTT_LINEA_OTT_DOCUMENTO foreign key (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION, NUMERO_LINEA)
      references OTT_LINEA_ADJUDICACION (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION, NUMERO_LINEA);
      
alter table OTT_LINEA_ADJUDICACION
   add constraint FK_OTT_CONTRATACION_OTT_LINEA foreign key (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION)
      references OTT_CONTRATACION (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION);

alter table OTH_LINEA_ADJUDICACION
   add constraint FK_OHT_CONTRATACION_OTH_LINEA foreign key (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION)
      references OTH_CONTRATACION (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION);


alter table OTH_DOCUMENTO_CONTRATACION
   add constraint FK_OTH_CONTRATACION_OTH_DOCUM foreign key (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION)
      references OTH_CONTRATACION (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION);

alter table OTH_DOCUMENTO_CONTRATACION
   add constraint FK_OTM_ETAPA_OTH_DOCUMENTO foreign key (ID_ETAPA_CONTRATACION)
      references OTM_ETAPA_CONTRATACION (ID_ETAPA_CONTRATACION);

alter table OTH_DOCUMENTO_CONTRATACION
   add constraint FK_OTH_LINEA_AD_OTH_DOCUMENTO foreign key (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION, NUMERO_LINEA)
      references OTH_LINEA_ADJUDICACION (ID_UBICACION, ID_ORDEN_TRABAJO, VERSION, NUMERO_LINEA);

alter table OTH_DOCUMENTO_CONTRATACION
   add constraint FK_OTH_ADJUNTO_OTH_DOCUMENTO foreign key (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO)
      references OTH_ADJUNTO_ORDEN_TRABAJO (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO);
    
alter table OTH_CONTRATACION
   add constraint FK_OTM_VIA_COMP_OTH_CONT foreign key (ID_VIA_CONTRATO)
      references OTM_VIA_COMPRA_CONTRATO (ID_VIA_COMPRA_CONTRATO);


alter table OTH_CONTRATACION
   add constraint FK_OTH_ORDEN_TRABAJO_OTH_CON foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references OTH_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

/*===============================================================*/
/*			Etapa III	Gestion de Almacen				         */
/* Fecha:   16-05-2016                                                  */
/*===============================================================*/

alter table ORDENES_TRABAJO.OTF_INVENTARIO
   drop constraint FK_OTM_ALMACEN_OTF_INVENTARIO;

alter table ORDENES_TRABAJO.OTF_INVENTARIO
   drop constraint FK_OTM_MATERIAL_OTF_INVENTARIO;

alter table ORDENES_TRABAJO.OTL_DETALLE_MATERIAL
   drop constraint FK_SOLICITUD_OTL_DETALLE_MAT;

alter table ORDENES_TRABAJO.OTL_DETALLE_MATERIAL
   drop constraint FK_OTM_MATERIAL_OTL_DETALLE;

alter table ORDENES_TRABAJO.OTM_ALMACEN_BODEGA
   drop constraint FK_OTM_UBICACION_OTM_ALMACEN;

alter table ORDENES_TRABAJO.OTM_ALMACEN_BODEGA
   drop constraint FK_OTM_SECTOR_TALLER_OTM_ALMAC;

alter table ORDENES_TRABAJO.OTM_CATEGORIA_MATERIAL
   drop constraint FK_OTM_UBICACION_OTM_FAMILIA;

alter table ORDENES_TRABAJO.OTM_MATERIAL
   drop constraint FK_OTM_UBICACION_OTM_MATERIAL;

alter table ORDENES_TRABAJO.OTM_MATERIAL
   drop constraint FK_OTM_UNIDAD_MED_OTM_MATERIAL;

alter table ORDENES_TRABAJO.OTM_MATERIAL
   drop constraint FK_OTM_CAT_FAM_OTM_MATERIAL;

alter table ORDENES_TRABAJO.OTM_SUBCATEGORIA_CATEGOR
   drop constraint FK_OTM_CATEGORIA_OTM_SUB_CAT;

alter table ORDENES_TRABAJO.OTM_SUBCATEGORIA_CATEGOR
   drop constraint FK_OTM_SUBCAT_MAT_OTM_SUB_CAT;

alter table ORDENES_TRABAJO.OTM_SUBCATEGORIA_MATERIAL
   drop constraint FK_OTM_UBICACION_OTM_CAT_MAT;

alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   drop constraint FK_OTF_INVENTARIO_OTT_DETALLE;

alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   drop constraint FK_OTT_ORDEN_TRAB_OTT_DETALLE;

alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   drop constraint FK_OTM_VIA_CONT_OTT_DETALLE;

alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   drop constraint FK_OTM_MATERIAL_OTT_DETALLE;

alter table ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL
   drop constraint OTT_ORDEN_TRABAJO_OTT_SOL_MAT;

alter table ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL
   drop constraint FK_OTT_ADJUNTO_OTT_SOLICITUD;

alter table ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL
   drop constraint FK_OTT_ADJUNTO_OTT_SOLICITUD_2;

alter table ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL
   drop constraint FK_OTC_ESTADO_OTT_SOL_MAT;
   
  /* alter table ORDENES_TRABAJO.OTL_DETALLE_MATERIAL
   drop constraint FK_SOLICITUD_OTL_DETALLE_MAT;*/

/*alter table ORDENES_TRABAJO.OTL_DETALLE_MATERIAL
   drop constraint FK_OTM_MATERIAL_OTL_DETALLE;*/

/*alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   drop constraint FK_OTF_INVENTARIO_OTT_DETALLE;*/

/*alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   drop constraint FK_OTT_ORDEN_TRAB_OTT_DETALLE;*/

/*alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   drop constraint FK_OTM_MATERIAL_OTT_DETALLE;*/

alter table ORDENES_TRABAJO.OTT_DETALLE_RETIRO
   drop constraint FK_OTT_DETALLE_MAT_OTT_SOL_RET;

alter table ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO
   drop constraint FK_OTT_DET_MAT_OTT_SOL_REING;
   
   alter table ORDENES_TRABAJO.OTT_DETALLE_RETIRO
   drop constraint FK_OTT_DETALLE_MAT_OTT_SOL_RET;

alter table ORDENES_TRABAJO.OTT_DETALLE_RETIRO
   drop constraint FK_OTT_SOLICITUD_RETIRO_OTT_DE;

alter table OTT_SOLICITUD_RETIRO
   drop constraint FK_OTT_SOL_MAT_OTT_SOL_RET;

alter table OTT_SOLICITUD_RETIRO
   drop constraint FK_OTC_ESTADO_OTT_SOLICITUD;
   
   alter table OTM_ENCARGADO_ALMACEN
   drop constraint FK_EU_EMPLEADOS_OTM_ENC_ALMAC;

alter table OTM_ENCARGADO_ALMACEN
   drop constraint FK_OTM_UBICA_OTM_ENCARGADO;
   
   
alter table ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO
   drop constraint FK_OTT_DET_MAT_OTT_SOL_REING;

alter table ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO
   drop constraint FK_OTM_UBICACION_OTT_SOL_REIN;
   
alter table OTL_TRAZABILIDAD_SOL_MAT
   drop constraint FK_OTC_ESTADO_OTL_TRAZABILIDAD;

alter table ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL
   drop constraint FK_OTC_ESTADO_OTT_SOLICITUD;
   
alter table OTL_TRAZABILIDAD_SOL_MAT
   drop constraint FK_OTT_SOLICITUD_MAT_OTL_TRAZ;

alter table OTL_TRAZABILIDAD_SOL_MAT
   drop constraint FK_OTC_ESTADO_OTL_TRAZABILIDAD;

alter table ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE
   drop constraint FK_OTF_INCIDENT_ALM_OTF_ADJUNT;

alter table ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE
   drop constraint FK_OTM_TIPO_DOCUM_OTF_ADJUNTO;

alter table ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN
   drop constraint FK_OTM_TIPO_INCIDENTE_OTF_INC;

alter table ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN
   drop constraint FK_OTM_ALM_BOD_OTF_INCIDENT;

alter table ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN
   drop constraint FK_OTM_MATERIAL_OTF_INCIDENTE;

alter table ORDENES_TRABAJO.OTM_CORREO_PROVEEDOR
   drop constraint FK_OTM_PROVEEDOR_OTM_CORREO;

alter table ORDENES_TRABAJO.OTM_TELEFONO_PROVEEDOR
   drop constraint FK_OTM_PROVEEDOR_OTM_TELEFONO;

alter table OTT_OFERTA_PROVEEDOR
   drop constraint FK_OTT_OFER_OTT_OFERT_OTM_PROV;

--09-08-16
alter table ORDENES_TRABAJO.OTL_LINEA_TRASLADO
   drop constraint FK_OTT_SOL_TRAS_OTL_LINEA_TRAS;

alter table ORDENES_TRABAJO.OTL_LINEA_TRASLADO
   drop constraint FK_OTF_INVENT_OTL_LINEA_TRAS;

alter table ORDENES_TRABAJO.OTL_SOLICITUD_TRASLADO
   drop constraint FK_OTM_UBICA_OTL_SOL_TRASLADO;

alter table ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO
   drop constraint FK_OTC_ESTADO_OTL_TRAZABIL;

alter table ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO
   drop constraint FK_OTT_SOL_OTL_TRAZABIL_SOL;

alter table ORDENES_TRABAJO.OTT_LINEA_TRASLADO
   drop constraint FK_OTF_INVENTARIO_OTT_LINEA;

alter table ORDENES_TRABAJO.OTT_LINEA_TRASLADO
   drop constraint FK_OTT_SOL_TRASL_OTT_LINEA;

alter table ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO
   drop constraint FK_OTM_ALM_BODEGA_OTT_SOLICIT;

alter table ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO
   drop constraint FK_OTM_ALMACEN_BODEGA_OTT_SOL;

alter table ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO
   drop constraint FK_OTC_ESTADO_OTT_SOLIC_TRAS;

alter table ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO
   drop constraint FK_OTM_UBICA_OTT_SOLIC_TRAS;
   
  --17-08-2016
   alter table ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP
   drop constraint FK_OTT_GC_OTL_TRAZABIL_GC;

alter table ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP
   drop constraint FK_OTC_ESTADO_OTL_TRAZABIL_GC;

alter table OTM_FLUJO_GESTION_COMPRA
   drop constraint FK_OTC_ESTADO_OTM_FLUJO;

alter table OTM_FLUJO_GESTION_COMPRA
   drop constraint FK_OTM_VIA_COMPRA_OTM_FLUJO;

alter table ORDENES_TRABAJO.OTT_GESTION_COMPRA
   drop constraint FK_OTM_UBICA_OTT_GESTION_COM;

alter table ORDENES_TRABAJO.OTT_GESTION_COMPRA
   drop constraint FK_OTM_VIA_COMPRA_OTT_GC;

alter table ORDENES_TRABAJO.OTT_GESTION_COMPRA
   drop constraint FK_OTC_ESTADO_OTT_GESTION;

alter table ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA
   drop constraint FK_OTT_GESTION_OTT_LINEA_GC;

alter table ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA
   drop constraint FK_OTT_DET_MAT_OTT_LINEA_GC;

alter table ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA
   drop constraint FK_OTM_MATERIAL_OTT_LINEA;

alter table ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR
   drop constraint FK_OTT_LINEA_GC_OTT_OFERTA;

alter table ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION
   drop constraint FK_OTT_GESTION_OTT_PROV_COT;

--25-08-16
alter table ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION
   drop constraint FK_OTT_PROVE_OTT_ACLARACION;

alter table ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION
   drop constraint FK_OTM_TIPO_DOC_OTT_ADJ_COT;

alter table ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION
   drop constraint FK_OTT_PROV_OTT_ADJUNTO;

alter table ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA
   drop constraint FK_OTT_GESTION_OTT_GRUPO_GES;

alter table ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA
   drop constraint FK_OTM_MATERIAL_OTT_GRUPO;

alter table ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR
   drop constraint FK_OTT_GRUPO_OTT_OFERTA_PROV;

alter table ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR
   drop constraint FK_OTT_PROV_OTT_OFERTA_PROV;

alter table ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION
   drop constraint FK_OTM_PROVEEDOR_OTT_PROV_COT;

--09-09-2016
alter table OTL_DET_APROVISIONAMIENTO
   drop constraint FK_OTT_APROV_OTL_DET_APROV;

alter table OTL_DET_APROVISIONAMIENTO
   drop constraint FK_OTM_MATERIAL_OTL_DET_APROV;

alter table ORDENES_TRABAJO.OTL_TRAZABILIDAD_AJUSTE
   drop constraint FK_OTC_ESTADO_AJ_OTL_TRAZABILI;

alter table ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR
   drop constraint FK_OTT_ESTADO_GESTION_OTL_TRAZ;

alter table ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION
   drop constraint FK_OTT_GEST_OTT_ACLARACION;

alter table ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR
   drop constraint FK_OTT_GESTION_OTT_ADJUNTO;

alter table ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR
   drop constraint FK_OTM_TIPO_DOC_OTT_ADJUNTO_GC;

alter table ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR
   drop constraint FK_OTT_GESTION_INGR_OTT_ADJUNT;

alter table ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR
   drop constraint FK_OTM_TIPO_DOC_OTT_ADJUNTO;

alter table ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO
   drop constraint FK_OTM_UBICA_OTT_AJUSTE_INV;

alter table ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO
   drop constraint FK_OTM_ALMACEN_OTT_AJUSTE_INV;

alter table ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO
   drop constraint FK_OTC_ESTADO_OTT_AJUSTE_INV;

alter table ORDENES_TRABAJO.OTT_APROVISIONAMIENTO
   drop constraint FK_OTM_UBICA_OTT_APROVISIONAM;

alter table ORDENES_TRABAJO.OTT_APROVISIONAMIENTO
   drop constraint FK_OTM_VIA_COMPRA_OTT_APROV;

alter table OTT_DESTINATAR_ACLARACION
   drop constraint FK_OTT_ACLARACION_OTT_DESTIN;

alter table OTT_DESTINATAR_ACLARACION
   drop constraint FK_OTM_PROVEEDOR_OTT_DESTIN;

alter table ORDENES_TRABAJO.OTT_DETALLE_AJUSTE
   drop constraint FK_OTT_AJUSTE_OTT_DETALLE_AJUS;

alter table ORDENES_TRABAJO.OTT_DETALLE_AJUSTE
   drop constraint FK_OTM_MATERIAL_OTT_DETALLE_AJ;

alter table ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR
   drop constraint FK_OTT_ADJUNTO_OTT_DETALLE_GI;

alter table ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR
   drop constraint FK_OTT_LINEA_OTT_DETALLE_GEST;

alter table ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO
   drop constraint FK_OTT_APROV_OTT_DET_APROV;

alter table ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO
   drop constraint FK_OTM_MATERIAL_OTT_DET_APROV;

alter table ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER
   drop constraint FK_OTC_ESTADO_OTT_GESTION_INGR;

alter table ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER
   drop constraint FK_OTT_GESTION_COMPRA_OTT_GEST;

alter table ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER
   drop constraint FK_OTT_PROVEEDOR_OTT_GESTION;

alter table ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA
   drop constraint FK_OTC_ESTADO_OTT_GRUPO_GC;

drop table ORDENES_TRABAJO.OTL_DETALLE_MATERIAL cascade constraints;

drop table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL cascade constraints;

drop table ORDENES_TRABAJO.OTF_INVENTARIO cascade constraints;

--drop table ORDENES_TRABAJO.OTL_DETALLE_MATERIAL cascade constraints;

drop table ORDENES_TRABAJO.OTM_ALMACEN_BODEGA cascade constraints;

drop table ORDENES_TRABAJO.OTM_CATEGORIA_MATERIAL cascade constraints;

drop table ORDENES_TRABAJO.OTM_MATERIAL cascade constraints;

drop table ORDENES_TRABAJO.OTM_SUBCATEGORIA_CATEGOR cascade constraints;

drop table ORDENES_TRABAJO.OTM_SUBCATEGORIA_MATERIAL cascade constraints;

drop table ORDENES_TRABAJO.OTM_UNIDAD_MEDIDA cascade constraints;

--drop table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL cascade constraints;

drop table ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL cascade constraints;

drop table OTC_ESTADO_SOLICITUD_RET cascade constraints;

drop table ORDENES_TRABAJO.OTT_DETALLE_RETIRO cascade constraints;

drop table OTT_SOLICITUD_RETIRO cascade constraints;

drop table OTM_ENCARGADO_ALMACEN cascade constraints;

drop table ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO cascade constraints;

drop table OTC_ESTADO_SOL_MATERIAL cascade constraints;

drop table OTL_TRAZABILIDAD_SOL_MAT cascade constraints;

drop table ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE cascade constraints;

drop table ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN cascade constraints;

drop table ORDENES_TRABAJO.OTM_CORREO_PROVEEDOR cascade constraints;

drop table ORDENES_TRABAJO.OTM_PROVEEDOR cascade constraints;

drop table ORDENES_TRABAJO.OTM_TELEFONO_PROVEEDOR cascade constraints;

drop table ORDENES_TRABAJO.OTM_TIPO_INCIDENTE cascade constraints;

--09-08-16
drop table ORDENES_TRABAJO.OTC_ESTADO_TRASLADO cascade constraints;

drop table ORDENES_TRABAJO.OTL_LINEA_TRASLADO cascade constraints;

drop table ORDENES_TRABAJO.OTL_SOLICITUD_TRASLADO cascade constraints;

drop table ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO cascade constraints;

drop table ORDENES_TRABAJO.OTT_LINEA_TRASLADO cascade constraints;

drop table ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO cascade constraints;

--17-08-2016
drop table ORDENES_TRABAJO.OTC_ESTADO_GESTION_COMPRA cascade constraints;

drop table ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP cascade constraints;

drop table OTM_FLUJO_GESTION_COMPRA cascade constraints;

drop table ORDENES_TRABAJO.OTT_GESTION_COMPRA cascade constraints;

drop table ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA cascade constraints;


--25-08-16
drop table ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION cascade constraints;

drop table ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA cascade constraints;

drop table ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR cascade constraints;

drop table ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION cascade constraints;


--09-09-2016
drop table ORDENES_TRABAJO.OTC_ESTADO_AJUSTE cascade constraints;

drop table ORDENES_TRABAJO.OTC_ESTADO_GESTION_INGRES cascade constraints;

drop table ORDENES_TRABAJO.OTC_ESTADO_LINEA cascade constraints;

drop table OTL_DET_APROVISIONAMIENTO cascade constraints;

drop table ORDENES_TRABAJO.OTL_TRAZABILIDAD_AJUSTE cascade constraints;

drop table ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR cascade constraints;

drop table ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION cascade constraints;

drop table ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR cascade constraints;

drop table ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR cascade constraints;

drop table ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO cascade constraints;

drop table ORDENES_TRABAJO.OTT_APROVISIONAMIENTO cascade constraints;

drop table OTT_DESTINATAR_ACLARACION cascade constraints;

drop table ORDENES_TRABAJO.OTT_DETALLE_AJUSTE cascade constraints;

drop table ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR cascade constraints;

drop table ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO cascade constraints;

drop table ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER cascade constraints;

/*==============================================================*/
/* Table: OTF_INVENTARIO                                        */
/*==============================================================*/
create table ORDENES_TRABAJO.OTF_INVENTARIO 
(
   ID_ALMACEN_BODEGA    NUMBER(10,0)         not null,
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   ID_MATERIAL          NUMBER(10,0)         not null,
   CANTIDAD_DISPONIBLE  NUMBER(10,2)         not null,
   CANTIDAD_RESERVADA   NUMBER(10,2)         not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTF_INVENTARIO primary key (ID_ALMACEN_BODEGA, ID_MATERIAL, ID_UBICACION_ADMINISTRA)
);

comment on table ORDENES_TRABAJO.OTF_INVENTARIO is
'Tabla para llevar el inventario de cada almacén o bodega';

comment on column ORDENES_TRABAJO.OTF_INVENTARIO.ID_ALMACEN_BODEGA is
'Llave primaria de la tabla OTM_ALMACEN_BODEGA que se asocia con la secuencia SQ_ID_ALMACEN_BODEGA';

comment on column ORDENES_TRABAJO.OTF_INVENTARIO.ID_UBICACION_ADMINISTRA is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTF_INVENTARIO.ID_MATERIAL is
'Llave primaria de la tabla OTM_MATERIAL. Consecutivo de 1 a n para cada ubicación';

comment on column ORDENES_TRABAJO.OTF_INVENTARIO.CANTIDAD_DISPONIBLE is
'Cantidad disponible del material en el inventario';

comment on column ORDENES_TRABAJO.OTF_INVENTARIO.CANTIDAD_RESERVADA is
'Cantidad reservada';

comment on column ORDENES_TRABAJO.OTF_INVENTARIO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTF_INVENTARIO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTL_DETALLE_MATERIAL                                  */
/*==============================================================*/
create table ORDENES_TRABAJO.OTL_DETALLE_MATERIAL 
(
   ID_DETALLE_MATERIAL  NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   ID_MATERIAL          NUMBER(10,0)         not null,
   CANTIDAD_SOLICITADA  NUMBER(10,2)         not null
      constraint CK_CANTIDAD_LOG check (CANTIDAD_SOLICITADA >= 0),
   DETALLE              VARCHAR2(500)        not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTL_DETALLE_MATERIAL primary key (ID_DETALLE_MATERIAL)
);

comment on table ORDENES_TRABAJO.OTL_DETALLE_MATERIAL is
'Tabla de bitácora para registrar las modificaciones a la solicitud de material';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.ID_DETALLE_MATERIAL is
'Llave primaria de la tabla OTT_DETALLE_MATERIAL que se asocia con la secuencia SQ_ID_DETALLE_MATERIAL';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA is
'Sede que administra el catálogo de materiales';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.ID_MATERIAL is
'Llave primaria de la tabla OTM_MATERIAL. Consecutivo de 1 a n para cada ubicación';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.CANTIDAD_SOLICITADA is
'Cantidad de material solicitado';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.DETALLE is
'Particularidades del material, ejemplo color';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_ALMACEN_BODEGA                                    */
/*==============================================================*/
create table ORDENES_TRABAJO.OTM_ALMACEN_BODEGA 
(
   ID_ALMACEN_BODEGA    NUMBER(10,0)         not null,
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   TIPO                 VARCHAR2(3)          default 'ALM' not null
      constraint CK_TIPO_ALMACEN check (TIPO in ('ALM','BOD') and TIPO = upper(TIPO)),
   ID_SECTOR_TALLER     NUMBER(10,0),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_ALMACEN check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_ALMACEN_BODEGA primary key (ID_ALMACEN_BODEGA),
   constraint AK_OTM_ALMACEN_BODEGA unique (DESCRIPCION, ID_UBICACION_ADMINISTRA)
);

comment on table ORDENES_TRABAJO.OTM_ALMACEN_BODEGA is
'Tabla para registrar los almacenes y bodegas';

comment on column ORDENES_TRABAJO.OTM_ALMACEN_BODEGA.ID_ALMACEN_BODEGA is
'Llave primaria de la tabla OTM_ALMACEN_BODEGA que se asocia con la secuencia SQ_ID_ALMACEN_BODEGA';

comment on column ORDENES_TRABAJO.OTM_ALMACEN_BODEGA.ID_UBICACION_ADMINISTRA is
'Id de la ubicación que administra los datos del catálogo';

comment on column ORDENES_TRABAJO.OTM_ALMACEN_BODEGA.DESCRIPCION is
'Descripción del almacén';

comment on column ORDENES_TRABAJO.OTM_ALMACEN_BODEGA.TIPO is
'Tipo de almacén: ALM - Almacén Central, Bodega - BOD';

comment on column ORDENES_TRABAJO.OTM_ALMACEN_BODEGA.ID_SECTOR_TALLER is
'En el caso de bodegas, taller o sector que la administra';

comment on column ORDENES_TRABAJO.OTM_ALMACEN_BODEGA.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column ORDENES_TRABAJO.OTM_ALMACEN_BODEGA.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTM_ALMACEN_BODEGA.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_CATEGORIA_MATERIAL                                */
/*==============================================================*/
create table ORDENES_TRABAJO.OTM_CATEGORIA_MATERIAL 
(
   ID_CATEGORIA_MATERIAL NUMBER(10,0)         not null,
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_FAMILIA check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_CATEGORIA_MATERIAL primary key (ID_CATEGORIA_MATERIAL),
   constraint AK_OTM_CATEGORIA_MATERIAL unique (DESCRIPCION, ID_UBICACION_ADMINISTRA)
);

comment on table ORDENES_TRABAJO.OTM_CATEGORIA_MATERIAL is
'Tabla para registrar las categorías de materiales. Ej: Agricolas, Fontanería, Electricidad, etc.';

comment on column ORDENES_TRABAJO.OTM_CATEGORIA_MATERIAL.ID_CATEGORIA_MATERIAL is
'Llave primaria de la tabla OTM_CATEGORIA_MATERIAL que se asocia con la secuencia SQ_ID_CATEGORIA_MATERIAL';

comment on column ORDENES_TRABAJO.OTM_CATEGORIA_MATERIAL.ID_UBICACION_ADMINISTRA is
'Id de la ubicación que administra los datos del catálogo';

comment on column ORDENES_TRABAJO.OTM_CATEGORIA_MATERIAL.DESCRIPCION is
'Descripción de la familia de materiales';

comment on column ORDENES_TRABAJO.OTM_CATEGORIA_MATERIAL.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column ORDENES_TRABAJO.OTM_CATEGORIA_MATERIAL.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTM_CATEGORIA_MATERIAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_MATERIAL                                          */
/*==============================================================*/
create table ORDENES_TRABAJO.OTM_MATERIAL 
(
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   ID_MATERIAL          NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(250)        not null,
   ID_CATEGORIA_MATERIAL NUMBER(10,0)         not null,
   ID_SUBCATEGORIA_MATERIAL NUMBER(10,0)         not null,
   ID_UNIDAD_MEDIDA     NUMBER(10,0)         not null,
   PARTIDA_PRESUPUESTARIA VARCHAR2(15)         not null,
   CLASIFICACION        VARCHAR2(1)          default 'A' not null
      constraint CK_CLASIFICACION_MATERIAL check (CLASIFICACION in ('A','B','C') and CLASIFICACION = upper(CLASIFICACION)),
   PUNTO_REORDEN        NUMBER(4,0)          not null,
   MAXIMO_ALMACEN       NUMBER(4,0)          not null,
   MAXIMO_BODEGA        NUMBER(4,0)          not null,
   COSTO_PROMEDIO       NUMBER(13,2)         not null
      constraint CK_COSTO_PROMEDIO check (COSTO_PROMEDIO >= 0),
   UBICACION_FISICA     VARCHAR2(8)          not null,
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_MATERIAL check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_MATERIAL primary key (ID_UBICACION_ADMINISTRA, ID_MATERIAL),
   constraint AK_OTM_MATERIAL unique (DESCRIPCION, ID_UBICACION_ADMINISTRA, ID_UNIDAD_MEDIDA)
);

comment on table ORDENES_TRABAJO.OTM_MATERIAL is
'Tabla para registrar el catálogo de materiales que pueden estar en el inventario';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.ID_UBICACION_ADMINISTRA is
'Id de la ubicación que administra los datos del catálogo';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.ID_MATERIAL is
'Llave primaria de la tabla OTM_MATERIAL. Consecutivo de 1 a n para cada ubicación';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.DESCRIPCION is
'Descripción del material';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.ID_CATEGORIA_MATERIAL is
'Llave primaria de la tabla OTM_CATEGORIA_MATERIAL que se asocia con la secuencia SQ_ID_CATEGORIA_MATERIAL';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.ID_SUBCATEGORIA_MATERIAL is
'Llave primaria de la tabla OTM_SUBCATEGORIA_MATERIAL que se asocia con la secuencia SQ_ID_SUBCATEGORIA_MATERIAL';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.ID_UNIDAD_MEDIDA is
'Llave primaria de la tabla OTM_UNIDAD_MEDIDA que se asocia con la secuencia SQ_ID_UNIDAD_MEDIDA';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.PARTIDA_PRESUPUESTARIA is
'Partida presupuestaria OPLAU';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.CLASIFICACION is
'Clasificación del material: A- Alta rotación, B- Baja rotación, C- Segunda';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.PUNTO_REORDEN is
'Punto de reorden';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.MAXIMO_ALMACEN is
'Máximo del material en almacén';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.MAXIMO_BODEGA is
'Máximo permitido del material en una bodega para evitar sobreabastecimiento';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.COSTO_PROMEDIO is
'Costo promedio del material';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.UBICACION_FISICA is
'Ubicación física del material conformada por Mueble-Columna-Estante';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTM_MATERIAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_SUBCATEGORIA_CATEGOR                              */
/*==============================================================*/
create table ORDENES_TRABAJO.OTM_SUBCATEGORIA_CATEGOR 
(
   ID_CATEGORIA_MATERIAL NUMBER(10,0)         not null,
   ID_SUBCATEGORIA_MATERIAL NUMBER(10,0)         not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_SUBCATEGORIA_CATEGOR primary key (ID_CATEGORIA_MATERIAL, ID_SUBCATEGORIA_MATERIAL)
);

comment on table ORDENES_TRABAJO.OTM_SUBCATEGORIA_CATEGOR is
'Tabla para registrar las subcategorías de material por categoría';

comment on column ORDENES_TRABAJO.OTM_SUBCATEGORIA_CATEGOR.ID_CATEGORIA_MATERIAL is
'Llave primaria de la tabla OTM_CATEGORIA_MATERIAL que se asocia con la secuencia SQ_ID_CATEGORIA_MATERIAL';

comment on column ORDENES_TRABAJO.OTM_SUBCATEGORIA_CATEGOR.ID_SUBCATEGORIA_MATERIAL is
'Llave primaria de la tabla OTM_SUBCATEGORIA_MATERIAL que se asocia con la secuencia SQ_ID_SUBCATEGORIA_MATERIAL';

comment on column ORDENES_TRABAJO.OTM_SUBCATEGORIA_CATEGOR.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTM_SUBCATEGORIA_CATEGOR.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_SUBCATEGORIA_MATERIAL                             */
/*==============================================================*/
create table ORDENES_TRABAJO.OTM_SUBCATEGORIA_MATERIAL 
(
   ID_SUBCATEGORIA_MATERIAL NUMBER(10,0)         not null,
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_CAT_MATERIAL check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_SUBCATEGORIA_MATERIAL primary key (ID_SUBCATEGORIA_MATERIAL),
   constraint AK_OTM_SUBCATEGORIA_MATERIAL unique (ID_UBICACION_ADMINISTRA, DESCRIPCION)
);

comment on table ORDENES_TRABAJO.OTM_SUBCATEGORIA_MATERIAL is
'Tabla para registrar las diferentes subcategorías que puede tener una categoría de materiales. Ej: Abrasivos, Aceros, Aluminios, etc.';

comment on column ORDENES_TRABAJO.OTM_SUBCATEGORIA_MATERIAL.ID_SUBCATEGORIA_MATERIAL is
'Llave primaria de la tabla OTM_SUBCATEGORIA_MATERIAL que se asocia con la secuencia SQ_ID_SUBCATEGORIA_MATERIAL';

comment on column ORDENES_TRABAJO.OTM_SUBCATEGORIA_MATERIAL.ID_UBICACION_ADMINISTRA is
'Id de la ubicación que administra los datos del catálogo';

comment on column ORDENES_TRABAJO.OTM_SUBCATEGORIA_MATERIAL.DESCRIPCION is
'Descripción de la etapa a la cual pertenece el registro asociado.';

comment on column ORDENES_TRABAJO.OTM_SUBCATEGORIA_MATERIAL.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column ORDENES_TRABAJO.OTM_SUBCATEGORIA_MATERIAL.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTM_SUBCATEGORIA_MATERIAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_UNIDAD_MEDIDA                                     */
/*==============================================================*/
create table ORDENES_TRABAJO.OTM_UNIDAD_MEDIDA 
(
   ID_UNIDAD_MEDIDA     NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(50)         not null,
   ACRONIMO             VARCHAR2(8)          not null,
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_UNI_MEDIDA check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_UNIDAD_MEDIDA primary key (ID_UNIDAD_MEDIDA),
   constraint AK_OTM_UNIDAD_MEDIDA unique (DESCRIPCION)
);

comment on table ORDENES_TRABAJO.OTM_UNIDAD_MEDIDA is
'Tabla para registrar las diferentes unidades de medida requeridas para cuantificar la cantidad existente de materiales';

comment on column ORDENES_TRABAJO.OTM_UNIDAD_MEDIDA.ID_UNIDAD_MEDIDA is
'Llave primaria de la tabla OTM_UNIDAD_MEDIDA que se asocia con la secuencia SQ_ID_UNIDAD_MEDIDA';

comment on column ORDENES_TRABAJO.OTM_UNIDAD_MEDIDA.DESCRIPCION is
'Descripción de unidad de medida. Ej: Litro';

comment on column ORDENES_TRABAJO.OTM_UNIDAD_MEDIDA.ACRONIMO is
'Acrónimo';

comment on column ORDENES_TRABAJO.OTM_UNIDAD_MEDIDA.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo';

comment on column ORDENES_TRABAJO.OTM_UNIDAD_MEDIDA.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTM_UNIDAD_MEDIDA.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_DETALLE_MATERIAL                                  */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL 
(
   ID_DETALLE_MATERIAL  NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   ID_MATERIAL          NUMBER(10,0)         not null,
   CANTIDAD_SOLICITADA  NUMBER(6,2)          not null
      constraint CK_MIN_CANTIDAD_SOL check (CANTIDAD_SOLICITADA >= 0),
   CANTIDAD_RESERVADA   NUMBER(6,2)          not null,
   CANTIDAD_RETIRADA    NUMBER(6,2)          not null,
   DETALLE              VARCHAR2(500),
   VIA_DESPACHO         VARCHAR2(3)          default 'ALM'
      constraint CK_MODO_OBTENCION check (VIA_DESPACHO is null or (VIA_DESPACHO in ('ALM','VCM') and VIA_DESPACHO = upper(VIA_DESPACHO))),
   ID_ALMACEN_BODEGA    NUMBER(10,0),
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0),
    ESTADO               VARCHAR2(3)          default 'PEN'
      constraint CK_ESTADO_DET_MAT check (ESTADO is null or (ESTADO in ('PEV','PEN','APR','DEN','EPC','REB') and ESTADO = upper(ESTADO))),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_DETALLE_MATERIAL primary key (ID_DETALLE_MATERIAL)
);

comment on table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL is
'Tabla para registrar el material solicitado para tramitar una orden de trabajo';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL is
'Llave primaria de la tabla OTT_DETALLE_MATERIAL que se asocia con la secuencia SQ_ID_DETALLE_MATERIAL';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ID_MATERIAL is
'Llave primaria de la tabla OTM_MATERIAL. Consecutivo de 1 a n para cada ubicación';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA is
'Cantidad de material solicitado';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA is
'Cantidad de material reservado';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.CANTIDAD_RETIRADA is
'Cantidad de material retirado';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.DETALLE is
'Particularidades del material, ejemplo color';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.VIA_DESPACHO is
'Modo de obtener el material: almacén, bodega, vía de compra';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ID_ALMACEN_BODEGA is
'Llave primaria de la tabla OTM_ALMACEN_BODEGA que se asocia con la secuencia SQ_ID_ALMACEN_BODEGA';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ESTADO is
'Estado del registro - PEN: Pendiente, APR: Aprobada, DEN: Denegada';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_SOLICITUD_MATERIAL                                */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   NO_REQUIERE_MATERIAL NUMBER(1,0)          default 0 not null
      constraint CK_NO_REQUIERE_MATERIAL check (NO_REQUIERE_MATERIAL in (0,1)),
   OBSERVACIONES        VARCHAR2(1000),
   HISTORIAL_JUSTIFICACION VARCHAR2(4000),
   ID_TIPO_DOCUMENTO_SOLICITA NUMBER(10,0),
   ID_ETAPA_ORDEN_TRABAJO_SOL NUMBER(10,0),
   ID_ADJUNTO_SOLICITA  NUMBER(10,0),
   ID_TIPO_DOCUMENTO_RESPUESTA NUMBER(10,0),
   ID_ETAPA_ORDEN_TRABAJO_RES NUMBER(10,0),
   ID_ADJUNTO_RESPUESTA NUMBER(10,0),
   ESTADO_SOL_MATERIAL  VARCHAR2(3)          default 'ING' not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_SOLICITUD_MATERIAL primary key (ID_UBICACION, ID_ORDEN_TRABAJO)
);

comment on table ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL is
'Tabla para registrar la solicitud de material de una orden de trabajo de mantenimiento.';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL.NO_REQUIERE_MATERIAL is
'Indicador de no requiere material.';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL.OBSERVACIONES is
'Observaciones generales de la solicitud de material';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL.HISTORIAL_JUSTIFICACION is
'Historial de justificaciones para material adicional';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL.ID_TIPO_DOCUMENTO_SOLICITA is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL.ID_ETAPA_ORDEN_TRABAJO_SOL is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL.ID_ADJUNTO_SOLICITA is
'Llave primaria de la tabla OTF_ADJUNTO_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ADJUNTO_ORDEN_TRABAJO';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL.ID_TIPO_DOCUMENTO_RESPUESTA is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL.ID_ETAPA_ORDEN_TRABAJO_RES is
'Llave primaria de la tabla OTM_ETAPA_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ETAPA_ORDEN_TRABAJO';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL.ID_ADJUNTO_RESPUESTA is
'Llave primaria de la tabla OTF_ADJUNTO_ORDEN_TRABAJO que se asocia con la secuencia SQ_ID_ADJUNTO_ORDEN_TRABAJO';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL.ESTADO_SOL_MATERIAL is
'Llave de la tabla OTC_ESTADO_SOL_MATERIAL';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTL_DETALLE_MATERIAL                                  */
/*==============================================================*/
/*create table ORDENES_TRABAJO.OTL_DETALLE_MATERIAL 
(
   ID_DETALLE_MATERIAL  NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   ID_MATERIAL          NUMBER(10,0)         not null,
   CANTIDAD_SOLICITADA  NUMBER(10,2)         not null
      constraint CK_CANTIDAD_LOG check (CANTIDAD_SOLICITADA >= 0),
   DETALLE              VARCHAR2(500)        not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTL_DETALLE_MATERIAL primary key (ID_DETALLE_MATERIAL)
);

comment on table ORDENES_TRABAJO.OTL_DETALLE_MATERIAL is
'Tabla de bitácora para registrar las modificaciones a la solicitud de material';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.ID_DETALLE_MATERIAL is
'Llave primaria de la tabla OTT_DETALLE_MATERIAL que se asocia con la secuencia SQ_ID_DETALLE_MATERIAL';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA is
'Sede que administra el catálogo de materiales';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.ID_MATERIAL is
'Llave primaria de la tabla OTM_MATERIAL. Consecutivo de 1 a n para cada ubicación';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.CANTIDAD_SOLICITADA is
'Cantidad de material solicitado';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.DETALLE is
'Particularidades del material, ejemplo color';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTL_DETALLE_MATERIAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';*/

/*==============================================================*/
/* Table: OTT_DETALLE_MATERIAL                                  */
/*==============================================================*/
/*create table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL 
(
   ID_DETALLE_MATERIAL  NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   ID_MATERIAL          NUMBER(10,0)         not null,
   CANTIDAD_SOLICITADA  NUMBER(6,2)          not null
      constraint CK_MIN_CANTIDAD_SOL check (CANTIDAD_SOLICITADA >= 0),
   CANTIDAD_RESERVADA   NUMBER(6,2)          not null,
   CANTIDAD_RETIRADA    NUMBER(6,2)          not null,
   DETALLE              VARCHAR2(500),
   VIA_DESPACHO         VARCHAR2(3)          default 'ALM'
      constraint CK_MODO_OBTENCION check (VIA_DESPACHO is null or (VIA_DESPACHO in ('ALM','VCM') and VIA_DESPACHO = upper(VIA_DESPACHO))),
   ID_ALMACEN_BODEGA    NUMBER(10,0),
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0),
   ESTADO               VARCHAR2(3)          default 'PEN'
      constraint CK_ESTADO_DET_MAT check (ESTADO is null or (ESTADO in ('PEN','APR','DEN') and ESTADO = upper(ESTADO))),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_DETALLE_MATERIAL primary key (ID_DETALLE_MATERIAL)
);

comment on table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL is
'Tabla para registrar el material solicitado para tramitar una orden de trabajo';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ID_DETALLE_MATERIAL is
'Llave primaria de la tabla OTT_DETALLE_MATERIAL que se asocia con la secuencia SQ_ID_DETALLE_MATERIAL';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ID_UBICACION_ADMINISTRA is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ID_MATERIAL is
'Llave primaria de la tabla OTM_MATERIAL. Consecutivo de 1 a n para cada ubicación';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.CANTIDAD_SOLICITADA is
'Cantidad de material solicitado';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.CANTIDAD_RESERVADA is
'Cantidad de material reservado';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.CANTIDAD_RETIRADA is
'Cantidad de material retirado';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.DETALLE is
'Particularidades del material, ejemplo color';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.VIA_DESPACHO is
'Modo de obtener el material: almacén, bodega, vía de compra';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ID_ALMACEN_BODEGA is
'Llave primaria de la tabla OTM_ALMACEN_BODEGA que se asocia con la secuencia SQ_ID_ALMACEN_BODEGA';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.ESTADO is
'Estado del registro - PEN: Pendiente, APR: Aprobada, DEN: Denegada';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_DETALLE_MATERIAL.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';*/

/*==============================================================*/
/* Table: OTC_ESTADO_SOLICITUD_RET                              */
/*==============================================================*/
create table OTC_ESTADO_SOLICITUD_RET 
(
   ESTADO_SOLICITUD_RETIRO VARCHAR2(3)          not null
      constraint CK_ESTADO_SOL_RET check (ESTADO_SOLICITUD_RETIRO = upper(ESTADO_SOLICITUD_RETIRO)),
   DESCRIPCION          VARCHAR2(100)        not null,
   constraint PK_OTC_ESTADO_SOLICITUD_RET primary key (ESTADO_SOLICITUD_RETIRO)
);

comment on table OTC_ESTADO_SOLICITUD_RET is
'Tabla para registrar los posibles estados de una solicitud de retiro';

comment on column OTC_ESTADO_SOLICITUD_RET.ESTADO_SOLICITUD_RETIRO is
'Llave primaria de la tabla OTC_ESTADO_SOLICITUD_RET';

comment on column OTC_ESTADO_SOLICITUD_RET.DESCRIPCION is
'Descripción de la orden de trabajo';

/*==============================================================*/
/* Table: OTT_DETALLE_RETIRO                                    */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_DETALLE_RETIRO 
(
   ANNO                 NUMBER(4,0)          not null,
   ID_SOLICITUD_RETIRO  NUMBER(10,0)         not null,
   ID_DETALLE_MATERIAL  NUMBER(10,0)         not null,
   CANTIDAD_SOLICITADA  NUMBER(6,2)          not null,
   CANTIDAD_RETIRADA    NUMBER(6,2),
   COSTO_CALCULADO      NUMBER(13,2)         default 0
      constraint CK_COSTO_CALCULADO_RET check (COSTO_CALCULADO is null or (COSTO_CALCULADO >= 0)),
   ESTADO               VARCHAR2(3)          default 'RET' not null
      constraint CK_ESTADO_SOLICITUD_RET check (ESTADO in ('PEN','RET') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_DETALLE_RETIRO primary key (ID_DETALLE_MATERIAL, ANNO, ID_SOLICITUD_RETIRO)
);

comment on table ORDENES_TRABAJO.OTT_DETALLE_RETIRO is
'Tabla para registrar el detalle de las solicitudes de retiro de material del almacén o bodega';

comment on column ORDENES_TRABAJO.OTT_DETALLE_RETIRO.ANNO is
'Llave primaria de la tabla OTT_SOLICITUD_RETIRO';

comment on column ORDENES_TRABAJO.OTT_DETALLE_RETIRO.ID_SOLICITUD_RETIRO is
'Llave primaria de la tabla OTT_SOLICITUD_RETIRO';

comment on column ORDENES_TRABAJO.OTT_DETALLE_RETIRO.ID_DETALLE_MATERIAL is
'Llave primaria de la tabla OTT_DETALLE_MATERIAL que se asocia con la secuencia SQ_ID_DETALLE_MATERIAL';

comment on column ORDENES_TRABAJO.OTT_DETALLE_RETIRO.CANTIDAD_SOLICITADA is
'Cantidad de material a retirar';

comment on column ORDENES_TRABAJO.OTT_DETALLE_RETIRO.CANTIDAD_RETIRADA is
'Cantidad de material efectivamente retirada';

comment on column ORDENES_TRABAJO.OTT_DETALLE_RETIRO.COSTO_CALCULADO is
'Costo calculado con base en cantidad y costo promedio al momento del retiro';

comment on column ORDENES_TRABAJO.OTT_DETALLE_RETIRO.ESTADO is
'Estado de la solicitud: PEN - Pendiente, RET - Retirada';

comment on column ORDENES_TRABAJO.OTT_DETALLE_RETIRO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_DETALLE_RETIRO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_SOLICITUD_RETIRO                                  */
/*==============================================================*/
create table OTT_SOLICITUD_RETIRO 
(
     ANNO                 NUMBER(4,0)          not null,
   ID_SOLICITUD_RETIRO  NUMBER(10,0)         not null,
   ESTADO_SOLICITUD_RETIRO VARCHAR2(3)          not null
      constraint CK_ESTADO_SOLICITUD check (ESTADO_SOLICITUD_RETIRO = upper(ESTADO_SOLICITUD_RETIRO)),
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   FECHA_REGISTRO       DATE                 default SYSDATE not null,
   FECHA_RETIRO         DATE                 not null,
   JORNADA_RETIRO       VARCHAR2(3)          default 'MAN' not null
      constraint CK_JORNADA_RETIRO check (JORNADA_RETIRO in ('MAN','TAR') and JORNADA_RETIRO = upper(JORNADA_RETIRO)),
   NUMERO_SALIDA        NUMBER(10,0),
   FECHA_HORA_IMPRESION DATE,
   FECHA_HORA_ALISTADO  DATE,
   FECHA_HORA_RETIRO    DATE,
   USUARIO_RETIRA       VARCHAR2(256),
   USUARIO              VARCHAR2(256),
   TIME_STAMP           DATE                 default SYSDATE,
   constraint PK_OTT_SOLICITUD_RETIRO primary key (ANNO, ID_SOLICITUD_RETIRO)
);

comment on table OTT_SOLICITUD_RETIRO is
'Tabla para registrar las solicitudes de retiro de material del almacén o bodega';

comment on column OTT_SOLICITUD_RETIRO.ANNO is
'Llave primaria de la tabla OTT_SOLICITUD_RETIRO';

comment on column OTT_SOLICITUD_RETIRO.ID_SOLICITUD_RETIRO is
'Llave primaria de la tabla OTT_SOLICITUD_RETIRO';

comment on column OTT_SOLICITUD_RETIRO.ESTADO_SOLICITUD_RETIRO is
'Llave primaria de la tabla OTC_ESTADO_SOLICITUD_RET';

comment on column OTT_SOLICITUD_RETIRO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_SOLICITUD_RETIRO.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column OTT_SOLICITUD_RETIRO.FECHA_REGISTRO is
'Fecha y hora de registro de la solicitud de retiro';

comment on column OTT_SOLICITUD_RETIRO.FECHA_RETIRO is
'Fecha planificada para retirar el material';

comment on column OTT_SOLICITUD_RETIRO.JORNADA_RETIRO is
'Jornada del día en que retirará el material: MAN- MAÑANA, TAR- TARDE. Valor por defecto MAN.';

comment on column OTT_SOLICITUD_RETIRO.NUMERO_SALIDA is
'Número de salida. Consecutivo anual.';

comment on column OTT_SOLICITUD_RETIRO.FECHA_HORA_IMPRESION is
'Fecha y hora de impresión para alistado de materiales';

comment on column OTT_SOLICITUD_RETIRO.FECHA_HORA_ALISTADO is
'Fecha y hora del alistado del material';

comment on column OTT_SOLICITUD_RETIRO.FECHA_HORA_RETIRO is
'Fecha y hora de retiro de materiales del almacén o bodega';

comment on column OTT_SOLICITUD_RETIRO.USUARIO_RETIRA is
'Usuario que realiza el retiro';

comment on column OTT_SOLICITUD_RETIRO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTT_SOLICITUD_RETIRO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_ENCARGADO_ALMACEN            (23-06-16)                     */
/*==============================================================*/
create table OTM_ENCARGADO_ALMACEN 
(
   NUM_EMPLEADO         INTEGER              not null,
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   ROL                  VARCHAR2(3)          not null
      constraint CK_ROL check (ROL in ('ENC','ALI','DES') and ROL = upper(ROL)),
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_ENCARGADO_ALM check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   TIME_STAMP           DATE                 default SYSDATE not null,
   USUARIO              VARCHAR2(256)        not null,
   constraint PK_OTM_ENCARGADO_ALMACEN primary key (NUM_EMPLEADO)
);

comment on table OTM_ENCARGADO_ALMACEN is
'Tabla para registrar el personal encargado del almacén y los roles correspondientes';

comment on column OTM_ENCARGADO_ALMACEN.NUM_EMPLEADO is
'Número de empleado del encargado';

comment on column OTM_ENCARGADO_ALMACEN.ID_UBICACION_ADMINISTRA is
'Id de la ubicación que administra los datos del catálogo';

comment on column OTM_ENCARGADO_ALMACEN.ROL is
'Rol del encargado.  ENC -Encargado, ALI - Alistador, DES -Despachador';

comment on column OTM_ENCARGADO_ALMACEN.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column OTM_ENCARGADO_ALMACEN.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

comment on column OTM_ENCARGADO_ALMACEN.USUARIO is
'Usuario que crea o modifica el registro.';

/*==============================================================*/
/* Table: OTT_SOLICITUD_REINGRESO                               */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO 
(
   ANNO                 NUMBER(4,0)          not null,
   ID_SOLICITUD_REINGRESO NUMBER(10,0)         not null,
   ID_UBICACION_ADMINISTRA NUMBER(10,0)         not null,
   ID_DETALLE_MATERIAL  NUMBER(10,0)         not null,
   CANTIDAD_REINGRESO   NUMBER(6,2)          not null,
   CANTIDAD_RECIBIDA    NUMBER(6,2)          not null,
   TIPO_SOLICITUD_REINGRESO VARCHAR2(3)          default 'DEV' not null
      constraint CK_TIPO_SOLICITUD_REING check (TIPO_SOLICITUD_REINGRESO in ('CUS','DEV') and TIPO_SOLICITUD_REINGRESO = upper(TIPO_SOLICITUD_REINGRESO)),
   COSTO_CALCULADO      NUMBER(13,2)         default 0 not null
      constraint CK_COSTO_CALCULADO_REING check (COSTO_CALCULADO >= 0),
   ESTADO               VARCHAR2(3)          default 'PEN' not null
      constraint CK_ESTADO_SOL_REING check (ESTADO in ('PEN','APR','DEN') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
  constraint PK_OTT_SOLICITUD_REINGRESO primary key (ID_SOLICITUD_REINGRESO, ANNO, ID_UBICACION_ADMINISTRA)
);

comment on table ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO is
'Tabla para registrar las solicitudes de reingreso de material al almacén';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO.ANNO is
'Llave primaria de la tabla OTT_SOLICITUD_REINGRESO';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO.ID_SOLICITUD_REINGRESO is
'Llave primaria de la tabla OTT_SOLICITUD_REINGRESO. Consecutivo anual, por ubicación';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO.ID_UBICACION_ADMINISTRA is
'Id de la ubicación a la que corresponde el trámite de reingreso';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO.ID_DETALLE_MATERIAL is
'Llave primaria de la tabla OTT_DETALLE_MATERIAL que se asocia con la secuencia SQ_ID_DETALLE_MATERIAL';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO.CANTIDAD_REINGRESO is
'Cantidad de material a reingresar al almacén';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO.CANTIDAD_RECIBIDA is
'Cantidad de material recibida en almacén';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO.TIPO_SOLICITUD_REINGRESO is
'Tipo de solicitud de reingreso: CUS - Material en Custodia, DEV - Devolución de Material';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO.COSTO_CALCULADO is
'Costo calculado en el momento de la devolución con base a cantidad y costo promedio por unidad';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO.ESTADO is
'Estado de la solicitud: PEN - Pendiente, APR - Aprobada, DEN - Denegada';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTC_ESTADO_SOL_MATERIAL                               */
/*==============================================================*/
create table OTC_ESTADO_SOL_MATERIAL 
(
   ESTADO_SOL_MATERIAL  VARCHAR2(3)          not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   constraint PK_OTC_ESTADO_SOL_MATERIAL primary key (ESTADO_SOL_MATERIAL),
   constraint AK_AK_OTC_ESTADO_SOL__OTC_ESTA unique (DESCRIPCION)
);

comment on table OTC_ESTADO_SOL_MATERIAL is
'Tabla para registrar los posibles estados de una solicitud de material';

comment on column OTC_ESTADO_SOL_MATERIAL.ESTADO_SOL_MATERIAL is
'Llave de la tabla OTC_ESTADO_SOL_MATERIAL';

comment on column OTC_ESTADO_SOL_MATERIAL.DESCRIPCION is
'Descripción del estado';

/*==============================================================*/
/* Table: OTL_TRAZABILIDAD_SOL_MAT                              */
/*==============================================================*/
create table ORDENES_TRABAJO.OTL_TRAZABILIDAD_SOL_MAT 
(
   ID_TRAZABILIDAD_SOL_MAT NUMBER(10)           not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ORDEN_TRABAJO     VARCHAR2(18)         default '-' not null,
   ESTADO_SOL_MATERIAL  VARCHAR2(3)          not null,
   OBSERVACIONES        VARCHAR2(2000),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTL_TRAZABILIDAD_SOL_MAT primary key (ID_TRAZABILIDAD_SOL_MAT)
);

comment on table ORDENES_TRABAJO.OTL_TRAZABILIDAD_SOL_MAT is
'Tabla para llevar el registro de la trazabilidad de las solicitudes de material';

comment on column ORDENES_TRABAJO.OTL_TRAZABILIDAD_SOL_MAT.ID_TRAZABILIDAD_SOL_MAT is
'Llave primaria de la tabla, asociada a la secuencia SQ_ID_TRAZABILIDAD_SOL_MAT';

comment on column ORDENES_TRABAJO.OTL_TRAZABILIDAD_SOL_MAT.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTL_TRAZABILIDAD_SOL_MAT.ID_ORDEN_TRABAJO is
'Identificador único alfanumérico de la orden de trabajo';

comment on column ORDENES_TRABAJO.OTL_TRAZABILIDAD_SOL_MAT.ESTADO_SOL_MATERIAL is
'Llave de la tabla OTC_ESTADO_SOL_MATERIAL';

comment on column ORDENES_TRABAJO.OTL_TRAZABILIDAD_SOL_MAT.OBSERVACIONES is
'Observaciones en caso de devolución';

comment on column ORDENES_TRABAJO.OTL_TRAZABILIDAD_SOL_MAT.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTL_TRAZABILIDAD_SOL_MAT.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

--04-08-16
/*==============================================================*/
/* Table: OTF_ADJUNTO_INCIDENTE                                 */
/*==============================================================*/
create table ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE 
(
   ID_ADJUNTO_INCIDENTE NUMBER(10,0)         not null,
   ID_INCIDENTE_ALMACEN NUMBER(10,0)         not null,
   ID_TIPO_DOCUMENTO    NUMBER(10,0)         not null,
   NOMBRE_ARCHIVO       VARCHAR2(100)        not null,
   ARCHIVO              BLOB                 not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   ORIGEN               VARCHAR2(3)          default 'REG' not null
      constraint CK_ORIGEN_ADJUNTO check (ORIGEN in ('REG','REV') and ORIGEN = upper(ORIGEN)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTF_ADJUNTO_INCIDENTE primary key (ID_ADJUNTO_INCIDENTE)
);

comment on table ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE is
'Tabla para registrar los documentos adjuntos a un incidente de almacen';

comment on column ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE.ID_ADJUNTO_INCIDENTE is
'Llave primaria de la tabla  OTF_ADJUNTO_INCIDENTE relacionada a la secuencia SQ_ID_ADJUNTO_INCIDENTE';

comment on column ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE.ID_INCIDENTE_ALMACEN is
'Llave primaria de la tabla OTF_INCIDENTE_ALMACEN asociada a la secuencia SQ_ID_INCIDENTE_ALMACEN';

comment on column ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE.ID_TIPO_DOCUMENTO is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE.NOMBRE_ARCHIVO is
'Nombre del archivo adjunto';

comment on column ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE.ARCHIVO is
'Documento adjunto';

comment on column ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE.DESCRIPCION is
'Breve descripción del archivo adjunto';

comment on column ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE.ORIGEN is
'Origen del archivo: REG - Registrador, REV - Revisor. Valor por defecto REG.';

comment on column ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTF_INCIDENTE_ALMACEN                                 */
/*==============================================================*/
create table ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN 
(
   ID_INCIDENTE_ALMACEN NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0),
   ID_MATERIAL          NUMBER(10,0),
   ID_ALMACEN_BODEGA    NUMBER(10,0)         not null,
   ID_TIPO_INCIDENTE    NUMBER(10,0)         not null,
   DETALLE              VARCHAR2(4000)       not null,
   ESTADO               VARCHAR2(3)          default 'CRE' not null
      constraint CK_ESTADO_INC_ALMACEN check (ESTADO in ('CRE','PEN','ATE') and ESTADO = upper(ESTADO)),
   FECHA_INCLUSION      DATE                 not null,
   OBSERVACIONES_REVISOR VARCHAR2(4000),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTF_INCIDENTE_ALMACEN primary key (ID_INCIDENTE_ALMACEN)
);

comment on table ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN is
'Tabla para registrar los incidentes por almacen';

comment on column ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN.ID_INCIDENTE_ALMACEN is
'Llave primaria de la tabla OTF_INCIDENTE_ALMACEN asociada a la secuencia SQ_ID_INCIDENTE_ALMACEN';

comment on column ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN.ID_MATERIAL is
'Llave primaria de la tabla OTM_MATERIAL. Consecutivo de 1 a n para cada ubicación';

comment on column ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN.ID_ALMACEN_BODEGA is
'Llave primaria de la tabla OTM_ALMACEN_BODEGA que se asocia con la secuencia SQ_ID_ALMACEN_BODEGA';

comment on column ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN.ID_TIPO_INCIDENTE is
'Llave primaria de la tabla OTM_TIPO_INCIDENTE asociada a la secuencia  SQ_ID_TIPO_INCIDENTE';

comment on column ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN.DETALLE is
'Detalle del incidente presentado';

comment on column ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN.ESTADO is
'Estado del incidente. CRE - Creado, PEN - Pendiente, ATE - Atendido';

comment on column ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN.FECHA_INCLUSION is
'Fecha de inclusión del incidente, para priorización.';

comment on column ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN.OBSERVACIONES_REVISOR is
'Observaciones del revisor del incidente';

comment on column ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_CORREO_PROVEEDOR                                  */
/*==============================================================*/
create table ORDENES_TRABAJO.OTM_CORREO_PROVEEDOR 
(
   IDENTIFICACION       VARCHAR2(20)         not null,
   CORREO               VARCHAR2(100)        not null,
   NOMBRE               VARCHAR2(200)        not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_CORREO_PROVEEDOR primary key (IDENTIFICACION, CORREO)
);

comment on table ORDENES_TRABAJO.OTM_CORREO_PROVEEDOR is
'Tabla para registrar los correos de contacto de un proveedor';

comment on column ORDENES_TRABAJO.OTM_CORREO_PROVEEDOR.IDENTIFICACION is
'Identificación del proveedor (física o jurídica)';

comment on column ORDENES_TRABAJO.OTM_CORREO_PROVEEDOR.CORREO is
'Correo del proveedor';

comment on column ORDENES_TRABAJO.OTM_CORREO_PROVEEDOR.NOMBRE is
'Nombre del contacto';

comment on column ORDENES_TRABAJO.OTM_CORREO_PROVEEDOR.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTM_CORREO_PROVEEDOR.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_PROVEEDOR                                         */
/*Fecha: 22-08-16
   Autor: Patricia Conejo
   Descripción: Se elimina el campo PERSONA_CONTACTO
*/
/*==============================================================*/
create table ORDENES_TRABAJO.OTM_PROVEEDOR 
(
   IDENTIFICACION       VARCHAR2(20)         not null,
   TIPO_PROVEEDOR       VARCHAR2(3)          not null
      constraint CK_TIPO_ID_PROVEEDOR check (TIPO_PROVEEDOR in ('FIS','JUR') and TIPO_PROVEEDOR = upper(TIPO_PROVEEDOR)),
   NOMBRE               VARCHAR2(250)        not null,
   DIRECCION            VARCHAR2(1000)       not null,
   SITIO_WEB            VARCHAR2(250),
   OBSERVACIONES        VARCHAR2(2000),
   ESTADO               VARCHAR2(3)          default 'ACT'
      constraint CK_ESTADO_PROVEEDOR check (ESTADO is null or (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO))),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_PROVEEDOR primary key (IDENTIFICACION)
);

comment on table ORDENES_TRABAJO.OTM_PROVEEDOR is
'Tabla para registrar los proveedores por sede';

comment on column ORDENES_TRABAJO.OTM_PROVEEDOR.IDENTIFICACION is
'Identificación del proveedor (física o jurídica)';

comment on column ORDENES_TRABAJO.OTM_PROVEEDOR.TIPO_PROVEEDOR is
'Tipo de identificación del proveedor: FIS - Físico, JUR - Jurídico';

comment on column ORDENES_TRABAJO.OTM_PROVEEDOR.NOMBRE is
'Nombre del proveedor';

comment on column ORDENES_TRABAJO.OTM_PROVEEDOR.DIRECCION is
'Dirección';

comment on column ORDENES_TRABAJO.OTM_PROVEEDOR.SITIO_WEB is
'Dirección del sitio web';

comment on column ORDENES_TRABAJO.OTM_PROVEEDOR.OBSERVACIONES is
'Observaciones';

comment on column ORDENES_TRABAJO.OTM_PROVEEDOR.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column ORDENES_TRABAJO.OTM_PROVEEDOR.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTM_PROVEEDOR.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_TELEFONO_PROVEEDOR                                */
/*==============================================================*/
create table ORDENES_TRABAJO.OTM_TELEFONO_PROVEEDOR 
(
   IDENTIFICACION       VARCHAR2(20)         not null,
   TELEFONO             VARCHAR2(20)         not null,
   USUARIO              VARCHAR2(256),
   TIME_STAMP           DATE                 default SYSDATE,
   constraint PK_OTM_TELEFONO_PROVEEDOR primary key (IDENTIFICACION, TELEFONO)
);

comment on table ORDENES_TRABAJO.OTM_TELEFONO_PROVEEDOR is
'Tabla para registrar los teléfonos de contacto de un proveedor';

comment on column ORDENES_TRABAJO.OTM_TELEFONO_PROVEEDOR.IDENTIFICACION is
'Identificación del proveedor (física o jurídica)';

comment on column ORDENES_TRABAJO.OTM_TELEFONO_PROVEEDOR.TELEFONO is
'Teléfono del proveedor';

comment on column ORDENES_TRABAJO.OTM_TELEFONO_PROVEEDOR.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTM_TELEFONO_PROVEEDOR.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_TIPO_INCIDENTE                                    */
/*==============================================================*/
create table ORDENES_TRABAJO.OTM_TIPO_INCIDENTE 
(
   ID_TIPO_INCIDENTE    NUMBER(10,0)         not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   ESTADO               VARCHAR2(3)          default 'ACT' not null
      constraint CK_ESTADO_INCIDENTE check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_TIPO_INCIDENTE primary key (ID_TIPO_INCIDENTE)
);

comment on table ORDENES_TRABAJO.OTM_TIPO_INCIDENTE is
'Tabla para registrar los tipos de incidentes que se pueden presentar en un almacén.';

comment on column ORDENES_TRABAJO.OTM_TIPO_INCIDENTE.ID_TIPO_INCIDENTE is
'Llave primaria de la tabla OTM_TIPO_INCIDENTE asociada a la secuencia  SQ_ID_TIPO_INCIDENTE';

comment on column ORDENES_TRABAJO.OTM_TIPO_INCIDENTE.DESCRIPCION is
'Descripción del incidente';

comment on column ORDENES_TRABAJO.OTM_TIPO_INCIDENTE.ESTADO is
'Estado del registro - ACT: Activo, INA: Inactivo - Valor por defecto: ACT';

comment on column ORDENES_TRABAJO.OTM_TIPO_INCIDENTE.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTM_TIPO_INCIDENTE.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';
--09-0816

/*==============================================================*/
/* Table: OTC_ESTADO_TRASLADO                                   */
/*==============================================================*/
create table ORDENES_TRABAJO.OTC_ESTADO_TRASLADO 
(
   ESTADO_TRASLADO      VARCHAR2(3)          not null
      constraint CK_ESTADO_TRASLADO check (ESTADO_TRASLADO = upper(ESTADO_TRASLADO)),
   DESCRIPCION          VARCHAR2(100)        not null,
   constraint PK_OTC_ESTADO_TRASLADO primary key (ESTADO_TRASLADO)
);

comment on table ORDENES_TRABAJO.OTC_ESTADO_TRASLADO is
'Tabla para registrar los estados posibles de una solicitud de traslado entre almacén y bodega';

comment on column ORDENES_TRABAJO.OTC_ESTADO_TRASLADO.ESTADO_TRASLADO is
'Llave de la tabla OTC_ESTADO_TRASLADO';

comment on column ORDENES_TRABAJO.OTC_ESTADO_TRASLADO.DESCRIPCION is
'Descripción';

/*==============================================================*/
/* Table: OTL_LINEA_TRASLADO                                    */
/*==============================================================*/
create table ORDENES_TRABAJO.OTL_LINEA_TRASLADO 
(
   ANNO                 NUMBER(4,0)          not null,
   ID_SOLICITUD_TRASLADO NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ALMACEN           NUMBER(10,0)         not null,
   ID_MATERIAL          NUMBER(10,0)         not null,
   CANTIDAD_REQUERIDA   NUMBER(10,2)         not null,
   CANTIDAD_RETIRADA    NUMBER(10,2),
   DETALLE              VARCHAR2(500),
   ESTADO               VARCHAR2(3)          default 'PAP' not null
      constraint CK_ESTADO_LIN_LOG check (ESTADO in ('PEN','PAP','APR') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTL_LINEA_TRASLADO primary key (ID_ALMACEN, ID_MATERIAL, ID_UBICACION, ANNO, ID_SOLICITUD_TRASLADO)
);

comment on table ORDENES_TRABAJO.OTL_LINEA_TRASLADO is
'Tabla de bitácora para registrar los cambios a una solicitud de materiales a trasladar del almacén a bodega.';

comment on column ORDENES_TRABAJO.OTL_LINEA_TRASLADO.ANNO is
'Año de la solicitud';

comment on column ORDENES_TRABAJO.OTL_LINEA_TRASLADO.ID_SOLICITUD_TRASLADO is
'Consecutivo de la solicitud. El consecutivo es anual.';

comment on column ORDENES_TRABAJO.OTL_LINEA_TRASLADO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTL_LINEA_TRASLADO.ID_ALMACEN is
'Llave primaria de la tabla OTM_ALMACEN_BODEGA que se asocia con la secuencia SQ_ID_ALMACEN_BODEGA';

comment on column ORDENES_TRABAJO.OTL_LINEA_TRASLADO.ID_MATERIAL is
'Llave primaria de la tabla OTM_MATERIAL. Consecutivo de 1 a n para cada ubicación';

comment on column ORDENES_TRABAJO.OTL_LINEA_TRASLADO.CANTIDAD_REQUERIDA is
'Cantidad a trasladar de material';

comment on column ORDENES_TRABAJO.OTL_LINEA_TRASLADO.CANTIDAD_RETIRADA is
'Cantidad retirada de material';

comment on column ORDENES_TRABAJO.OTL_LINEA_TRASLADO.DETALLE is
'Detalle';

comment on column ORDENES_TRABAJO.OTL_LINEA_TRASLADO.ESTADO is
'Estado de la línea';

comment on column ORDENES_TRABAJO.OTL_LINEA_TRASLADO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTL_LINEA_TRASLADO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTL_SOLICITUD_TRASLADO                                */
/*==============================================================*/
create table ORDENES_TRABAJO.OTL_SOLICITUD_TRASLADO 
(
   ANNO                 NUMBER(4,0)          not null,
   ID_SOLICITUD_TRASLADO NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   FECHA_PROPUESTA_SALIDA DATE                 not null,
   JORNADA_RETIRO       VARCHAR2(3)          default 'MAN' not null
      constraint CK_JORNADA_RETIRO_TR_L check (JORNADA_RETIRO in ('MAN','TAR') and JORNADA_RETIRO = upper(JORNADA_RETIRO)),
   TIME_STAMP           DATE                 default SYSDATE not null,
   USUARIO              VARCHAR2(256)        not null,
   constraint PK_OTL_SOLICITUD_TRASLADO primary key (ANNO, ID_SOLICITUD_TRASLADO, ID_UBICACION)
);

comment on table ORDENES_TRABAJO.OTL_SOLICITUD_TRASLADO is
'Tabla de bitácora para registrar cambios en las solicitudes de traslado de almacen a bodega';

comment on column ORDENES_TRABAJO.OTL_SOLICITUD_TRASLADO.ANNO is
'Año de la solicitud';

comment on column ORDENES_TRABAJO.OTL_SOLICITUD_TRASLADO.ID_SOLICITUD_TRASLADO is
'Consecutivo de la solicitud. El consecutivo es anual.';

comment on column ORDENES_TRABAJO.OTL_SOLICITUD_TRASLADO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTL_SOLICITUD_TRASLADO.FECHA_PROPUESTA_SALIDA is
'Fecha propuesta de salida del material';

comment on column ORDENES_TRABAJO.OTL_SOLICITUD_TRASLADO.JORNADA_RETIRO is
'Jornada del día en que retirará el material: MAN- MAÑANA, TAR- TARDE. Valor por defecto MAN.';

comment on column ORDENES_TRABAJO.OTL_SOLICITUD_TRASLADO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

comment on column ORDENES_TRABAJO.OTL_SOLICITUD_TRASLADO.USUARIO is
'Usuario que crea o modifica el registro.';

/*==============================================================*/
/* Table: OTL_TRAZABIL_SOL_TRASLADO                             */
/*==============================================================*/
create table ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO 
(
   ID_TRAZABIL_SOL_TRASLADO NUMBER(10)           not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ANNO                 NUMBER(4,0)          not null,
   ID_SOLICITUD_TRASLADO NUMBER(10,0)         not null,
   ESTADO_TRASLADO      VARCHAR2(3)          not null
      constraint CK_ESTADO_TRASLADO_TRAZAB check (ESTADO_TRASLADO = upper(ESTADO_TRASLADO)),
   OBSERVACIONES        VARCHAR2(2000),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTL_TRAZABIL_SOL_TRASLADO primary key (ID_TRAZABIL_SOL_TRASLADO)
);

comment on table ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO is
'Tabla para llevar el registro de la trazabilidad de las solicitudes de traslado de material';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO.ID_TRAZABIL_SOL_TRASLADO is
'Llave primaria de la tabla, asociada a la secuencia SQ_ID_TRAZABIL_SOL_TRASLADO';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO.ANNO is
'Año de la solicitud';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO.ID_SOLICITUD_TRASLADO is
'Consecutivo de la solicitud. El consecutivo es anual.';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO.ESTADO_TRASLADO is
'Llave de la tabla OTC_ESTADO_TRASLADO';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO.OBSERVACIONES is
'Observaciones a la solicitud';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_LINEA_TRASLADO                                    */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_LINEA_TRASLADO 
(
   ANNO                 NUMBER(4,0)          not null,
   ID_SOLICITUD_TRASLADO NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ALMACEN           NUMBER(10,0)         not null,
   ID_MATERIAL          NUMBER(10,0)         not null,
   CANTIDAD_REQUERIDA   NUMBER(10,2)         not null,
   CANTIDAD_RETIRADA    NUMBER(10,2),
   DETALLE              VARCHAR2(500),
   ESTADO               VARCHAR2(3)          default 'PAP' not null
      constraint CK_ESTADO_LINEA check (ESTADO in ('PEN','PAP','APR') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_LINEA_TRASLADO primary key (ID_UBICACION, ID_ALMACEN, ID_MATERIAL, ANNO, ID_SOLICITUD_TRASLADO)
);

comment on table ORDENES_TRABAJO.OTT_LINEA_TRASLADO is
'Tabla para registrar los materiales a trasladar del almacén a bodega.';

comment on column ORDENES_TRABAJO.OTT_LINEA_TRASLADO.ANNO is
'Año de la solicitud';

comment on column ORDENES_TRABAJO.OTT_LINEA_TRASLADO.ID_SOLICITUD_TRASLADO is
'Consecutivo de la solicitud. El consecutivo es anual.';

comment on column ORDENES_TRABAJO.OTT_LINEA_TRASLADO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_LINEA_TRASLADO.ID_ALMACEN is
'Llave primaria de la tabla OTF_INVENTARIO correspondiente al almacén principal';

comment on column ORDENES_TRABAJO.OTT_LINEA_TRASLADO.ID_MATERIAL is
'Llave primaria de la tabla OTF_INVENTARIO correspondiente al material a trasladar';

comment on column ORDENES_TRABAJO.OTT_LINEA_TRASLADO.CANTIDAD_REQUERIDA is
'Cantidad a trasladar de material';

comment on column ORDENES_TRABAJO.OTT_LINEA_TRASLADO.CANTIDAD_RETIRADA is
'Cantidad retirada de material';

comment on column ORDENES_TRABAJO.OTT_LINEA_TRASLADO.DETALLE is
'Detalle';

comment on column ORDENES_TRABAJO.OTT_LINEA_TRASLADO.ESTADO is
'Estado de la línea';

comment on column ORDENES_TRABAJO.OTT_LINEA_TRASLADO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_LINEA_TRASLADO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_SOLICITUD_TRASLADO                                */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO 
(
   ANNO                 NUMBER(4,0)          not null,
   ID_SOLICITUD_TRASLADO NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_ALMACEN           NUMBER(10,0)         not null,
   ID_BODEGA            NUMBER(10,0)         not null,
   ESTADO_TRASLADO      VARCHAR2(3)          not null
      constraint CK_ESTADO_TRASLADO_SOL check (ESTADO_TRASLADO = upper(ESTADO_TRASLADO)),
   FECHA_REGISTRO_SOLICITUD DATE                 not null,
   FECHA_PROPUESTA_SALIDA DATE                 not null,
   JORNADA_RETIRO       VARCHAR2(3)          default 'MAN' not null
      constraint CK_JORNADA_RETIRO_TRAS check (JORNADA_RETIRO in ('MAN','TAR') and JORNADA_RETIRO = upper(JORNADA_RETIRO)),
   OBSERVACIONES        VARCHAR2(4000),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_SOLICITUD_TRASLADO primary key (ID_UBICACION, ANNO, ID_SOLICITUD_TRASLADO)
);

comment on table ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO is
'Tabla para registrar las solicitudes de traslado de almacen a bodega';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO.ANNO is
'Año de la solicitud';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO.ID_SOLICITUD_TRASLADO is
'Consecutivo de la solicitud. El consecutivo es anual.';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO.ID_ALMACEN is
'Almacén origen';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO.ID_BODEGA is
'Bodega destino';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO.ESTADO_TRASLADO is
'Llave de la tabla OTC_ESTADO_TRASLADO';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO.FECHA_REGISTRO_SOLICITUD is
'Fecha de registro de la solicitud';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO.FECHA_PROPUESTA_SALIDA is
'Fecha propuesta de salida del material';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO.JORNADA_RETIRO is
'Jornada del día en que retirará el material: MAN- MAÑANA, TAR- TARDE. Valor por defecto MAN.';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO.OBSERVACIONES is
'Observaciones a la solicitud';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

--17-08-2016
/*==============================================================*/
/* Table: OTC_ESTADO_GESTION_COMPRA                             */
/*==============================================================*/
create table ORDENES_TRABAJO.OTC_ESTADO_GESTION_COMPRA 
(
   ESTADO               VARCHAR2(3)          not null
      constraint CK_ESTADO_GESTION_COMPRA check (ESTADO = upper(ESTADO)),
   DESCRIPCION          VARCHAR2(100)        not null,
   constraint PK_OTC_ESTADO_GESTION_COMPRA primary key (ESTADO)
);

comment on table ORDENES_TRABAJO.OTC_ESTADO_GESTION_COMPRA is
'Tabla para registrar los posibles estados de una gesión de compra';

comment on column ORDENES_TRABAJO.OTC_ESTADO_GESTION_COMPRA.ESTADO is
'Estado';

comment on column ORDENES_TRABAJO.OTC_ESTADO_GESTION_COMPRA.DESCRIPCION is
'Descripción';

/*==============================================================*/
/* Table: OTL_TRAZABIL_GESTION_COMP                             */
/*==============================================================*/
create table ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP 
(
   ID_TRAZABIL_GESTION_COMP NUMBER(10)           not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   ANNO                 NUMBER(4,0)          not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ESTADO               VARCHAR2(3)          not null
      constraint CK_ESTADO_TRAZABIL_GC check (ESTADO = upper(ESTADO)),
   OBSERVACIONES        VARCHAR2(1000),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTL_TRAZABIL_GESTION_COMP primary key (ID_TRAZABIL_GESTION_COMP)
);

comment on table ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP is
'Tabla para llevar el registro de la trazabilidad de las gestiones de compra';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP.ID_TRAZABIL_GESTION_COMP is
'Llave primaria de la tabla, asociada a la secuencia SQ_ID_TRAZABIL_GESTION_COMP';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP.ESTADO is
'Estado';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP.OBSERVACIONES is
'Observaciones';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTM_FLUJO_GESTION_COMPRA                              */
/*==============================================================*/
create table OTM_FLUJO_GESTION_COMPRA 
(
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   ESTADO               VARCHAR2(3)          not null
      constraint CK_ESTADO_FLUJO check (ESTADO = upper(ESTADO)),
   ORDEN                NUMBER(2,0)          default 1 not null
      constraint CK_ORDEN_ESTADO check (ORDEN between 1 and 99),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTM_FLUJO_GESTION_COMPRA primary key (ESTADO, ID_VIA_COMPRA_CONTRATO),
   constraint AK_OTM_FLUJO_GESTION_COMPRA unique (ID_VIA_COMPRA_CONTRATO, ORDEN)
);

comment on table OTM_FLUJO_GESTION_COMPRA is
'Tabla para registrar los estados posibles de una gestión de contratación según la vía de compra.';

comment on column OTM_FLUJO_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column OTM_FLUJO_GESTION_COMPRA.ESTADO is
'Estado';

comment on column OTM_FLUJO_GESTION_COMPRA.ORDEN is
'Peso asociado al orden que tendran las etapas indicadas en el catalogo';

comment on column OTM_FLUJO_GESTION_COMPRA.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTM_FLUJO_GESTION_COMPRA.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_GESTION_COMPRA                                    */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_GESTION_COMPRA 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   ANNO                 NUMBER(4,0)          not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   FECHA_REGISTRO_SOLICITUD DATE                 not null,
   OBSERVACIONES        VARCHAR2(1000),
   NUMERO_CHEQUE        VARCHAR2(30),
   NUMERO_GESTION_GECO  VARCHAR2(20),
   FECHA_HORA_IMPRESION DATE,
   ESTADO               VARCHAR2(3)          not null
      constraint CK_ESTADO_GESTION check (ESTADO = upper(ESTADO)),
       ANNO_REFERENCIA      NUMBER(4,0),
   NUMERO_GESTION_REFERENCIA NUMBER(6,0),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_GESTION_COMPRA primary key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO)
);

comment on table ORDENES_TRABAJO.OTT_GESTION_COMPRA is
'Tabla para registrar las gestiones de compra';

comment on column ORDENES_TRABAJO.OTT_GESTION_COMPRA.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTT_GESTION_COMPRA.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_GESTION_COMPRA.NUMERO_GESTION is
'Consecutivo de la gestión. Es anual, por vía de compra y ubicación.';

comment on column ORDENES_TRABAJO.OTT_GESTION_COMPRA.FECHA_REGISTRO_SOLICITUD is
'Fecha de registro de la solicitud';

comment on column ORDENES_TRABAJO.OTT_GESTION_COMPRA.OBSERVACIONES is
'Observaciones';

comment on column ORDENES_TRABAJO.OTT_GESTION_COMPRA.NUMERO_CHEQUE is
'Número de cheque para pago al proveedor adjudicado en gestiones por fondo de trabajo.';

comment on column ORDENES_TRABAJO.OTT_GESTION_COMPRA.NUMERO_GESTION_GECO is
'Número de gestión de geco. Aplica solo para gestiones de compra por suministros.';

comment on column ORDENES_TRABAJO.OTT_GESTION_COMPRA.FECHA_HORA_IMPRESION is
'Fecha y hora de impresión del reporte en las gestiones de compra rápida';

comment on column ORDENES_TRABAJO.OTT_GESTION_COMPRA.ANNO_REFERENCIA is
'Año de la gestión de referencia. Aplica para subgestiones';

comment on column ORDENES_TRABAJO.OTT_GESTION_COMPRA.NUMERO_GESTION_REFERENCIA is
'Consecutivo de la gestión de referencia. Aplica para subgestiones';


comment on column ORDENES_TRABAJO.OTT_GESTION_COMPRA.ESTADO is
'Estado';

comment on column ORDENES_TRABAJO.OTT_GESTION_COMPRA.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_GESTION_COMPRA.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_LINEA_GESTION_COMPRA                              */
/*Fecha: 29-08-2016
  Autor: Patricia Conejo Altamirano
  Descripción: Se elimina el campo NUMERO_LINEA*/
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   ANNO                 NUMBER(4,0)          not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ID_LINEA_GESTION_COMPRA NUMBER(10,0)         not null,
   ID_MATERIAL          NUMBER(10,0),
   ID_DETALLE_MATERIAL  NUMBER(10,0),
   CANTIDAD_SOLICITADA  NUMBER(6,2),
   CANTIDAD_INGRESA     NUMBER(6,2),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_LINEA_GESTION_COMPRA primary key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, ID_LINEA_GESTION_COMPRA)
);

comment on table ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA is
'Tabla para registrar las líneas de materiales solicitadas en la gestión de compra.';

comment on column ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA.NUMERO_GESTION is
'Consecutivo de la gestión. Es anual por ubicación y vía de compra.';

comment on column ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA.ID_LINEA_GESTION_COMPRA is
'Llave primaria de la tabla OTT_LINEA_GESTION_COMPRA asociada a la secuencia SQ_ID_LINEA_GESTION_COMPRA';

comment on column ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA.ID_MATERIAL is
'Id del material cuando la compra es por aprovisionamiento';

comment on column ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA.ID_DETALLE_MATERIAL is
'Id de la línea de detalle de material de la OT que da origen a la compra.';

comment on column ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA.CANTIDAD_SOLICITADA is
'Cantidad solicitada';

comment on column ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA.CANTIDAD_INGRESA is
'Cantidad comprada del material que ingresa al almacén';

comment on column ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

--25-08-16
/*==============================================================*/
/* Table: OTT_ADJUNTO_COTIZACION                                */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION 
(
   IDENTIFICACION       VARCHAR2(20)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   ANNO                 NUMBER(4,0)          not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ID_TIPO_DOCUMENTO    NUMBER(10,0) not null,
   ARCHIVO              BLOB                 not null,
   NOMBRE_ARCHIVO       VARCHAR2(100)        not null,
   DESCRIPCION          VARCHAR2(500)        not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_ADJUNTO_COTIZACION primary key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, IDENTIFICACION, NUMERO_GESTION, ANNO)
);

comment on table ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION is
'Tabla para registrar el archivo de cotización de cada proveedor';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION.IDENTIFICACION is
'Identificación del proveedor (física o jurídica)';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION.ID_TIPO_DOCUMENTO is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION.ARCHIVO is
'Documento adjunto correspondiente a una lista del equipo que posea la unidad, su estado actual y el equipo que se proyecte adquirir a corto plazo';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION.NOMBRE_ARCHIVO is
'Nombre del archivo adjunto';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION.DESCRIPCION is
'Descripción';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_GRUPO_GESTION_COMPRA                              */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   ANNO                 NUMBER(4,0)          not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ID_GRUPO_GESTION_COMPRA NUMBER(10,0)         not null,
   NUMERO_LINEA         NUMBER(3,0)          not null,
   ID_MATERIAL          NUMBER(10,0)         not null,
   CANTIDAD_SOLICITADA  NUMBER(6,2)          not null,
   USUARIO_ELIMINA      VARCHAR2(256),
   FECHA_HORA_ELIMINA   DATE,
   ESTADO_LINEA         VARCHAR2(3)         
      constraint CK_ESTADO_LINEA check (ESTADO_LINEA is null or (ESTADO_LINEA = upper(ESTADO_LINEA))),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_GRUPO_GESTION_COMPRA primary key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, ID_GRUPO_GESTION_COMPRA),
   constraint AK_OTT_GRUPO_GESTION_COMPRA unique (NUMERO_LINEA)
);

comment on table ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA is
'Tabla para registrar la agrupación de líneas del detalle de la gestión de compra';

comment on column ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA.ID_GRUPO_GESTION_COMPRA is
'Llave de la tabla OTT_GRUPO_GESTION_COMPRA asociada a la secuencia SQ_ID_GRUPO_GESTION_COMPRA';

comment on column ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA.NUMERO_LINEA is
'Número de línea que agrupa las solicitudes de un mismo material. Define el orden.';

comment on column ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA.ID_MATERIAL is
'Llave primaria de la tabla OTM_MATERIAL. Consecutivo de 1 a n para cada ubicación';

comment on column ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA.CANTIDAD_SOLICITADA is
'Cantidad solicitada';

comment on column ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA.USUARIO_ELIMINA is
'Usuario que elimina el registro de la subgestión. Es un borrado lógico, cambia el estado.';

comment on column ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA.FECHA_HORA_ELIMINA is
'Fecha en que se elimina el registro de la subgestión.';

comment on column ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA.ESTADO_LINEA is
'Estado';

comment on column ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_OFERTA_PROVEEDOR                                  */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   ANNO                 NUMBER(4,0)          not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ID_GRUPO_GESTION_COMPRA NUMBER(10,0)         not null,
   IDENTIFICACION       VARCHAR2(20)         not null,
   MONTO                NUMBER(13,2)         not null,
   OBSERVACIONES        VARCHAR2(2000),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_OFERTA_PROVEEDOR primary key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, IDENTIFICACION, NUMERO_GESTION, ANNO, ID_GRUPO_GESTION_COMPRA)
);

comment on table ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR is
'Tabla para registrar las ofertas de cada proveedor por línea';

comment on column ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR.ID_GRUPO_GESTION_COMPRA is
'Llave de la tabla OTT_GRUPO_GESTION_COMPRA asociada a la secuencia SQ_ID_GRUPO_GESTION_COMPRA';

comment on column ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR.IDENTIFICACION is
'Identificación del proveedor (física o jurídica)';

comment on column ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR.MONTO is
'Monto';

comment on column ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR.OBSERVACIONES is
'Observaciones indicadas por el supervisor';

comment on column ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_PROVEEDOR_COTIZACION                              */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION 
(
   IDENTIFICACION       VARCHAR2(20)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   ANNO                 NUMBER(4,0)          not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ESTADO               VARCHAR2(3)          not null
      constraint CK_ESTADO_COTIZACION check (ESTADO in ('PEN','ENV') and ESTADO = upper(ESTADO)),
   ID_NOTIFICACION      NUMBER,
   FECHA_HORA_NOTIFICACION DATE,
   ADJUDICADO           NUMBER(1,0)          default 0
      constraint CK_ADJUDICADO check (ADJUDICADO is null or (ADJUDICADO in (0,1))),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_PROVEEDOR_COTIZACION primary key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, IDENTIFICACION, NUMERO_GESTION, ANNO)
);

comment on table ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION is
'Tabla para registrar los proveedores a los que se soliita cotización para la gestión de compra';

comment on column ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION.IDENTIFICACION is
'Identificación del proveedor (física o jurídica)';

comment on column ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION.ESTADO is
'Estado: PEN - Pendiente de Envío, ENV - Enviado';

comment on column ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION.ID_NOTIFICACION is
'Id de la notificación en el gestor de notificaciones';

comment on column ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION.FECHA_HORA_NOTIFICACION is
'Fecha hora de la notificación';

comment on column ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION.ADJUDICADO is
'Indicador de si el proveedor fue adjudicado';

comment on column ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';


--09-09-216
/*==============================================================*/
/* Table: OTC_ESTADO_AJUSTE                                     */
/*==============================================================*/
create table ORDENES_TRABAJO.OTC_ESTADO_AJUSTE 
(
   ESTADO_AJUSTE        VARCHAR2(3)          not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   constraint PK_OTC_ESTADO_AJUSTE primary key (ESTADO_AJUSTE)
);

comment on table ORDENES_TRABAJO.OTC_ESTADO_AJUSTE is
'Tabla para registrar los posibles estados de un ajuste de inventario';

comment on column ORDENES_TRABAJO.OTC_ESTADO_AJUSTE.ESTADO_AJUSTE is
'Llave de la tabla OTC_ESTADO_AJUSTE';

comment on column ORDENES_TRABAJO.OTC_ESTADO_AJUSTE.DESCRIPCION is
'Descripción de la orden de trabajo';

/*==============================================================*/
/* Table: OTC_ESTADO_GESTION_INGRES                             */
/*==============================================================*/
create table ORDENES_TRABAJO.OTC_ESTADO_GESTION_INGRES 
(
   ESTADO_GESTION_INGRESO VARCHAR2(3)          not null,
   DESCRIPCION          VARCHAR2(100)        not null,
   constraint PK_OTC_ESTADO_GESTION_INGRES primary key (ESTADO_GESTION_INGRESO)
);

comment on table ORDENES_TRABAJO.OTC_ESTADO_GESTION_INGRES is
'Tabla para registrar los posibles estados de una gestión de ingreso de material';

comment on column ORDENES_TRABAJO.OTC_ESTADO_GESTION_INGRES.ESTADO_GESTION_INGRESO is
'Llave de la tabla OTC_ESTADO_GESTION_INGRES';

comment on column ORDENES_TRABAJO.OTC_ESTADO_GESTION_INGRES.DESCRIPCION is
'Descripción';

/*==============================================================*/
/* Table: OTC_ESTADO_LINEA                                      */
/*==============================================================*/
create table ORDENES_TRABAJO.OTC_ESTADO_LINEA 
(
   ESTADO_LINEA         VARCHAR2(3)          not null
      constraint CK_ESTADO_DE_LINEA check (ESTADO_LINEA in ('PEN','DEV','DEN','PDE') and ESTADO_LINEA = upper(ESTADO_LINEA)),
   DESCRIPCION          VARCHAR2(100)        not null,
   constraint PK_OTC_ESTADO_LINEA primary key (ESTADO_LINEA)
);

comment on table ORDENES_TRABAJO.OTC_ESTADO_LINEA is
'Tabla para registrar los posibles estados de una línea';

comment on column ORDENES_TRABAJO.OTC_ESTADO_LINEA.ESTADO_LINEA is
'Estado';

comment on column ORDENES_TRABAJO.OTC_ESTADO_LINEA.DESCRIPCION is
'Descripción de la orden de trabajo';

/*==============================================================*/
/* Table: OTL_DET_APROVISIONAMIENTO                             */
/*==============================================================*/
create table OTL_DET_APROVISIONAMIENTO 
(
   ID_DET_APROVISIONAMIENTO NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ANNO                 NUMBER(4,0)          not null
      constraint CK_ANNO_DET_APROVIS check (ANNO >= 0),
   ID_MATERIAL          NUMBER(10,0)         not null,
   CANTIDAD             NUMBER(10,2)         not null,
   OBSERVACIONES        VARCHAR2(1000),
   ESTADO               VARCHAR2(3)          default 'CRE' not null
      constraint CK_ESTADO_DET_APRV check (ESTADO in ('CRE','COM') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTL_DET_APROVISIONAMIENTO primary key (ID_UBICACION, NUMERO_GESTION, ANNO, ID_MATERIAL, ID_DET_APROVISIONAMIENTO)
);

comment on table OTL_DET_APROVISIONAMIENTO is
'Tabla para registrar las líneas de aprovisionamiento';

comment on column OTL_DET_APROVISIONAMIENTO.ID_DET_APROVISIONAMIENTO is
'Llave primaria de la tabla de bitácora del aprovisionamiento';

comment on column OTL_DET_APROVISIONAMIENTO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTL_DET_APROVISIONAMIENTO.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column OTL_DET_APROVISIONAMIENTO.ANNO is
'Año';

comment on column OTL_DET_APROVISIONAMIENTO.ID_MATERIAL is
'Llave primaria de la tabla OTM_MATERIAL. Consecutivo de 1 a n para cada ubicación';

comment on column OTL_DET_APROVISIONAMIENTO.CANTIDAD is
'Cantidad';

comment on column OTL_DET_APROVISIONAMIENTO.OBSERVACIONES is
'Observaciones';

comment on column OTL_DET_APROVISIONAMIENTO.ESTADO is
'Estado: CRE- Creado, COM - Completado';

comment on column OTL_DET_APROVISIONAMIENTO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column OTL_DET_APROVISIONAMIENTO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTL_TRAZABILIDAD_AJUSTE                               */
/*==============================================================*/
create table ORDENES_TRABAJO.OTL_TRAZABILIDAD_AJUSTE 
(
   ID_TRAZABILIDAD_AJUSTE NUMBER(10)           not null,
   ESTADO_AJUSTE        VARCHAR2(3)          not null,
   OBSERVACIONES        VARCHAR2(2000),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTL_TRAZABILIDAD_AJUSTE primary key (ID_TRAZABILIDAD_AJUSTE)
);

comment on table ORDENES_TRABAJO.OTL_TRAZABILIDAD_AJUSTE is
'Tabla para llevar el registro de la trazabilidad de las gestiones de ajuste de inventario';

comment on column ORDENES_TRABAJO.OTL_TRAZABILIDAD_AJUSTE.ID_TRAZABILIDAD_AJUSTE is
'Llave primaria de la tabla, asociada a la secuencia SQ_ID_TRAZABILIDAD_AJUSTE';

comment on column ORDENES_TRABAJO.OTL_TRAZABILIDAD_AJUSTE.ESTADO_AJUSTE is
'Llave de la tabla OTC_ESTADO_AJUSTE';

comment on column ORDENES_TRABAJO.OTL_TRAZABILIDAD_AJUSTE.OBSERVACIONES is
'Observaciones';

comment on column ORDENES_TRABAJO.OTL_TRAZABILIDAD_AJUSTE.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTL_TRAZABILIDAD_AJUSTE.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/

/* Table: OTL_TRAZABIL_GESTION_INGR                             */

/*==============================================================*/
create table ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR

(
   ID_TRAZABIL_GESTION_INGR NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0),
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0),
   NUMERO_GESTION       NUMBER(6,0),
   ANNO                 NUMBER(4,0),
   CONSECUTIVO          NUMBER(10,0),
   ESTADO_GESTION_INGRESO VARCHAR2(3)          not null,
   OBSERVACIONES        VARCHAR2(1000),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTL_TRAZABIL_GESTION_INGR primary key (ID_TRAZABIL_GESTION_INGR)
);


comment on table ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR is
'Tabla para registrar la trazabilidad de las gestiones de ingreso de material al almacén';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR.ID_TRAZABIL_GESTION_INGR is
'Llave primaria de la tabla  OTL_TRAZABIL_GESTION_INGR asociada a la secuencia  SQ_ID_TRAZABIL_GESTION_INGR';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR.CONSECUTIVO is
'Consecutivo de la gestion';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR.ESTADO_GESTION_INGRESO is
'Llave de la tabla OTC_ESTADO_GESTION_INGRES';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR.OBSERVACIONES is
'Observaciones';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

 

/*==============================================================*/
/* Table: OTT_ACLARACION_COTIZACION                             */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ANNO                 NUMBER(4,0)          not null,
   ID_NOTIFICACION      NUMBER               not null,
   TIPO_NOTIFICACION    VARCHAR2(3)          default 'ACL' not null
      constraint CK_TIPO_NOTIFICACION check (TIPO_NOTIFICACION in ('ACL','SUB','MOD') and TIPO_NOTIFICACION = upper(TIPO_NOTIFICACION)),
   FECHA_ENVIO          DATE                 not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_ACLARACION_COTIZACION primary key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, ID_NOTIFICACION)
);

comment on table ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION is
'Tabla para registrar las aclaraciones enviadas a los proveedores a los que se les cotizó para una gestión de compra';

comment on column ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION.ID_NOTIFICACION is
'Id de la notificación en el gestor de notificaciones';

comment on column ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION.TIPO_NOTIFICACION is
'Tipo de notificación: ACL- Aclaración, MOD - Modificación, SUB - Subsanación';

comment on column ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION.FECHA_ENVIO is
'Fecha de envío de la notificación';

comment on column ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_ADJUNTO_GESTION_COMPR                             */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   ANNO                 NUMBER(4,0)          not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ID_ADJUNTO_GESTION_COMPR NUMBER(10,0)         not null,
   ID_TIPO_DOCUMENTO    NUMBER(10,0)         not null,
   ARCHIVO              BLOB                 not null,
   NOMBRE_ARCHIVO       VARCHAR2(100)        not null,
   DESCRIPCION          VARCHAR2(500),
   NUMERO_ORDEN_COMPRA  VARCHAR2(20),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_ADJUNTO_GESTION_COMPR primary key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, ID_ADJUNTO_GESTION_COMPR)
);

comment on table ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR is
'Tabla para registrar los adjuntos de una gestión de compra: documento GECO, oficio para evaluacion, invitación, etc.';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR.ID_ADJUNTO_GESTION_COMPR is
'Llave de la tabla OTT_ADJUNTO_GESTION_COMPR asociada a la secuencia SQ_ID_ADJUNTO_GESTION_COMPR';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR.ID_TIPO_DOCUMENTO is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR.ARCHIVO is
'Documento adjunto correspondiente a una lista del equipo que posea la unidad, su estado actual y el equipo que se proyecte adquirir a corto plazo';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR.NOMBRE_ARCHIVO is
'Nombre del archivo adjunto';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR.DESCRIPCION is
'Descripción';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR.NUMERO_ORDEN_COMPRA is
'Número de orden de compra';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_ADJUNTO_GESTION_INGR                              */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR

(
   ID_ADJUNTO_GESTION_INGR NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ANNO                 NUMBER(4,0)          not null,
   CONSECUTIVO          NUMBER(10,0)         not null,
   ID_TIPO_DOCUMENTO    NUMBER(10,0)         not null,
   NUMERO_DOCUMENTO     VARCHAR2(20)         not null,
   ARCHIVO              BLOB                 not null,
   NOMBRE_ARCHIVO       VARCHAR2(100)        not null,
   DESCRIPCION          VARCHAR2(100),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_ADJUNTO_GESTION_INGR primary key (ID_ADJUNTO_GESTION_INGR)
);

 
comment on table ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR is
'Tabla para registrar los documentos relacionados a una gestión de ingreso de material: facturas, ordenes de compra, etc.';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR.ID_ADJUNTO_GESTION_INGR is
'Llave primaria de la tabla OTT_ADJUNTO_GESTION_INGR asociada a la secuencia SQ_ID_ADJUNTO_GESTION_INGR';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR.CONSECUTIVO is
'Consecutivo de la gestion';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR.ID_TIPO_DOCUMENTO is
'Llave primaria de la tabla OTM_TIPO_DOCUMENTO que se asocia con la secuencia SQ_ID_TIPO_DOCUMENTO';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR.NUMERO_DOCUMENTO is
'Número de documento: factura, orden de compra, etc.';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR.ARCHIVO is
'Documento adjunto correspondiente a una lista del equipo que posea la unidad, su estado actual y el equipo que se proyecte adquirir a corto plazo';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR.NOMBRE_ARCHIVO is
'Nombre del archivo adjunto';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR.DESCRIPCION is
'Descripción';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_AJUSTE_INVENTARIO                                 */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ANNO                 NUMBER(4,0)          not null
      constraint CK_ANNO_AJUSTE check (ANNO >= 0),
   CONSECUTIVO_AJUSTE   NUMBER(4,0)          not null,
   ID_ALMACEN_BODEGA    NUMBER(10,0)         not null,
   ESTADO_AJUSTE        VARCHAR2(3)          not null,
   TIPO_AJUSTE          VARCHAR2(3)          default 'IND' not null
      constraint CK_TIPO_AJUSTE check (TIPO_AJUSTE in ('IND','GBL','EXS') and TIPO_AJUSTE = upper(TIPO_AJUSTE)),
   FECHA_REGISTRO_SOLICITUD DATE                 not null,
   OBSERVACIONES        VARCHAR2(2000),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_AJUSTE_INVENTARIO primary key (ID_UBICACION, ANNO, CONSECUTIVO_AJUSTE)
);

comment on table ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO is
'Tabla para registrar los ajustes de inventario.';

comment on column ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO.CONSECUTIVO_AJUSTE is
'Consecutivo anual del ajuste.';

comment on column ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO.ID_ALMACEN_BODEGA is
'Llave primaria de la tabla OTM_ALMACEN_BODEGA que se asocia con la secuencia SQ_ID_ALMACEN_BODEGA';

comment on column ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO.ESTADO_AJUSTE is
'Llave de la tabla OTC_ESTADO_AJUSTE';

comment on column ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO.TIPO_AJUSTE is
'Tipo de ajuste. IND - Inventario Individual, GBL - Inventario Global, EXS - Existencia ';

comment on column ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO.FECHA_REGISTRO_SOLICITUD is
'Fecha de registro de la solicitud';

comment on column ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO.OBSERVACIONES is
'Observaciones indicadas por el revisor';

comment on column ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_APROVISIONAMIENTO                                 */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_APROVISIONAMIENTO 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ANNO                 NUMBER(4,0)          not null
      constraint CK_ANNO_APROV check (ANNO >= 0),
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   FECHA_REGISTRO_SOLICITUD DATE                 not null,
   OBSERVACIONES        VARCHAR2(1000),
   ESTADO               VARCHAR2(3)          default 'CRE' not null
      constraint CK_ESTADO_APROV check (ESTADO in ('CRE','COM') and ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_APROVISIONAMIENTO primary key (ID_UBICACION, NUMERO_GESTION, ANNO)
);

comment on table ORDENES_TRABAJO.OTT_APROVISIONAMIENTO is
'Tabla para registrar las gestiones de aprovisionamiento';

comment on column ORDENES_TRABAJO.OTT_APROVISIONAMIENTO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_APROVISIONAMIENTO.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column ORDENES_TRABAJO.OTT_APROVISIONAMIENTO.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_APROVISIONAMIENTO.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTT_APROVISIONAMIENTO.FECHA_REGISTRO_SOLICITUD is
'Fecha de registro de la solicitud';

comment on column ORDENES_TRABAJO.OTT_APROVISIONAMIENTO.OBSERVACIONES is
'Observaciones. Aplica para aprovisionamiento';

comment on column ORDENES_TRABAJO.OTT_APROVISIONAMIENTO.ESTADO is
'Estado: CRE- Creado, COM - Completado';

comment on column ORDENES_TRABAJO.OTT_APROVISIONAMIENTO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_APROVISIONAMIENTO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_DESTINATAR_ACLARACION                             */
/*==============================================================*/
create table OTT_DESTINATAR_ACLARACION 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ANNO                 NUMBER(4,0)          not null,
   ID_NOTIFICACION      NUMBER               not null,
   IDENTIFICACION       VARCHAR2(20)         not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   USUARIO              VARCHAR2(256)        not null,
   constraint PK_OTT_DESTINATAR_ACLARACION primary key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, ID_NOTIFICACION, IDENTIFICACION)
);

comment on table OTT_DESTINATAR_ACLARACION is
'Tabla para registrar los proveedores a quienes se les envía la aclaración';

comment on column OTT_DESTINATAR_ACLARACION.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column OTT_DESTINATAR_ACLARACION.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column OTT_DESTINATAR_ACLARACION.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column OTT_DESTINATAR_ACLARACION.ANNO is
'Año';

comment on column OTT_DESTINATAR_ACLARACION.ID_NOTIFICACION is
'Id de la notificación en el gestor de notificaciones';

comment on column OTT_DESTINATAR_ACLARACION.IDENTIFICACION is
'Identificación del proveedor (física o jurídica)';

comment on column OTT_DESTINATAR_ACLARACION.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

comment on column OTT_DESTINATAR_ACLARACION.USUARIO is
'Usuario que crea o modifica el registro.';

/*==============================================================*/
/* Table: OTT_DETALLE_AJUSTE                                    */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_DETALLE_AJUSTE 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ANNO                 NUMBER(4,0)          not null
      constraint CK_ANNO_DET_AJUSTE check (ANNO >= 0),
   CONSECUTIVO_AJUSTE   NUMBER(4,0)          not null,
   ID_MATERIAL          NUMBER(10,0)         not null,
   DIRECCION_AJUSTE     VARCHAR2(3)          not null,
   CANTIDAD             NUMBER(10,2)         not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_DETALLE_AJUSTE primary key (ID_UBICACION, ANNO, CONSECUTIVO_AJUSTE, ID_MATERIAL)
);

comment on table ORDENES_TRABAJO.OTT_DETALLE_AJUSTE is
'Tabla para registrar el detalle del ajuste al inventario';

comment on column ORDENES_TRABAJO.OTT_DETALLE_AJUSTE.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_DETALLE_AJUSTE.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_DETALLE_AJUSTE.CONSECUTIVO_AJUSTE is
'Consecutivo anual del ajuste.';

comment on column ORDENES_TRABAJO.OTT_DETALLE_AJUSTE.ID_MATERIAL is
'Llave primaria de la tabla OTM_MATERIAL. Consecutivo de 1 a n para cada ubicación';

comment on column ORDENES_TRABAJO.OTT_DETALLE_AJUSTE.DIRECCION_AJUSTE is
'INC - Incremento, DEC - Decremento';

comment on column ORDENES_TRABAJO.OTT_DETALLE_AJUSTE.CANTIDAD is
'Cantidad';

comment on column ORDENES_TRABAJO.OTT_DETALLE_AJUSTE.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_DETALLE_AJUSTE.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_DETALLE_GESTION_INGR                              */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR
(
   ID_ADJUNTO_GESTION_INGR NUMBER(10,0)         not null,
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ANNO                 NUMBER(4,0)          not null,
   ID_LINEA_GESTION_COMPRA NUMBER(10,0)         not null,
   CANTIDAD_INGRESA     NUMBER(6,2)          not null,
   COSTO_INDIVIDUAL     NUMBER(13,2),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_DETALLE_GESTION_INGR primary key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, ID_ADJUNTO_GESTION_INGR, ID_LINEA_GESTION_COMPRA)
);

comment on table ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR is
'Tabla para registrar los materiales recibidos para ingreso a almacén';

comment on column ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR.ID_ADJUNTO_GESTION_INGR is
'Llave primaria de la tabla OTT_ADJUNTO_GESTION_INGR asociada a la secuencia SQ_ID_ADJUNTO_GESTION_INGR';

comment on column ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR.ID_LINEA_GESTION_COMPRA is
'Llave primaria de la tabla OTT_LINEA_GESTION_COMPRA asociada a la secuencia SQ_ID_LINEA_GESTION_COMPRA';

comment on column ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR.CANTIDAD_INGRESA is
'Cantidad de material que ingresa al almacén';

comment on column ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR.COSTO_INDIVIDUAL is
'Costo individual del material que ingresa';

comment on column ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_DET_APROVISIONAMIENTO                             */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ANNO                 NUMBER(4,0)          not null
      constraint CK_ANNO_DET_APROVISIONAM check (ANNO >= 0),
   ID_MATERIAL          NUMBER(10,0)         not null,
   CANTIDAD             NUMBER(10,2)         not null,
   OBSERVACIONES        VARCHAR2(500),
   ESTADO               VARCHAR2(3)          not null
      constraint CK_ESTADO_DET_APROV check (ESTADO = upper(ESTADO)),
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_DET_APROVISIONAMIENTO primary key (ID_UBICACION, NUMERO_GESTION, ANNO, ID_MATERIAL)
);

comment on table ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO is
'Tabla para registrar las líneas de aprovisionamiento';

comment on column ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO.ID_MATERIAL is
'Llave primaria de la tabla OTM_MATERIAL. Consecutivo de 1 a n para cada ubicación';

comment on column ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO.CANTIDAD is
'Cantidad';

comment on column ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO.OBSERVACIONES is
'Observaciones';

comment on column ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO.ESTADO is
'EPR - En proceso,ING - Ingreso en almacen,INF - Infructuosa';

comment on column ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

/*==============================================================*/
/* Table: OTT_GESTION_INGRESO_MATER                             */
/*==============================================================*/
create table ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER 
(
   ID_UBICACION         NUMBER(10,0)         not null,
   ID_VIA_COMPRA_CONTRATO NUMBER(10,0)         not null,
   NUMERO_GESTION       NUMBER(6,0)          not null,
   ANNO                 NUMBER(4,0)          not null,
   CONSECUTIVO          NUMBER(10,0)         not null,
   ESTADO_GESTION_INGRESO VARCHAR2(3)          not null,
   IDENTIFICACION       VARCHAR2(20),
   OBSERVACIONES        VARCHAR2(1000),
   FECHA_INGRESO_REGISTRO DATE                 not null,
   USUARIO              VARCHAR2(256)        not null,
   TIME_STAMP           DATE                 default SYSDATE not null,
   constraint PK_OTT_GESTION_INGRESO_MATER primary key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, CONSECUTIVO)
);

comment on table ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER is
'Tabla para registrar las gestiones de ingreso de material al almacén';

comment on column ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER.ID_UBICACION is
'Llave primaria de la tabla OTM_UBICACION que se asocia con la secuencia SQ_ID_UBICACION';

comment on column ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER.ID_VIA_COMPRA_CONTRATO is
'Llave primaria de la tabla OTM_VIA_COMPRA_CONTRATO que se asocia con la secuencia SQ_ID_VIA_COMPRA_CONTRATO';

comment on column ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER.NUMERO_GESTION is
'Consecutivo de la gestión';

comment on column ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER.ANNO is
'Año';

comment on column ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER.CONSECUTIVO is
'Consecutivo de la gestion';

comment on column ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER.ESTADO_GESTION_INGRESO is
'Llave de la tabla OTC_ESTADO_GESTION_INGRES';

comment on column ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER.IDENTIFICACION is
'Identificación del proveedor (física o jurídica)';

comment on column ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER.OBSERVACIONES is
'Observaciones';

comment on column ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER.FECHA_INGRESO_REGISTRO is
'Fecha de ingreso del registro';

comment on column ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER.USUARIO is
'Usuario que crea o modifica el registro.';

comment on column ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER.TIME_STAMP is
'Control de concurrencia - Valor por defecto: Fecha y hora del sistema';

alter table ORDENES_TRABAJO.OTF_INVENTARIO
   add constraint FK_OTM_ALMACEN_OTF_INVENTARIO foreign key (ID_ALMACEN_BODEGA)
      references ORDENES_TRABAJO.OTM_ALMACEN_BODEGA (ID_ALMACEN_BODEGA);

alter table ORDENES_TRABAJO.OTF_INVENTARIO
   add constraint FK_OTM_MATERIAL_OTF_INVENTARIO foreign key (ID_UBICACION_ADMINISTRA, ID_MATERIAL)
      references ORDENES_TRABAJO.OTM_MATERIAL (ID_UBICACION_ADMINISTRA, ID_MATERIAL);

alter table ORDENES_TRABAJO.OTL_DETALLE_MATERIAL
   add constraint FK_SOLICITUD_OTL_DETALLE_MAT foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table ORDENES_TRABAJO.OTL_DETALLE_MATERIAL
   add constraint FK_OTM_MATERIAL_OTL_DETALLE foreign key (ID_UBICACION_ADMINISTRA, ID_MATERIAL)
      references ORDENES_TRABAJO.OTM_MATERIAL (ID_UBICACION_ADMINISTRA, ID_MATERIAL);

alter table ORDENES_TRABAJO.OTM_ALMACEN_BODEGA
   add constraint FK_OTM_UBICACION_OTM_ALMACEN foreign key (ID_UBICACION_ADMINISTRA)
      references ORDENES_TRABAJO.OTM_UBICACION (ID_UBICACION);

alter table ORDENES_TRABAJO.OTM_ALMACEN_BODEGA
   add constraint FK_OTM_SECTOR_TALLER_OTM_ALMAC foreign key (ID_SECTOR_TALLER)
      references ORDENES_TRABAJO.OTM_SECTOR_TALLER (ID_SECTOR_TALLER);

alter table ORDENES_TRABAJO.OTM_CATEGORIA_MATERIAL
   add constraint FK_OTM_UBICACION_OTM_FAMILIA foreign key (ID_UBICACION_ADMINISTRA)
      references ORDENES_TRABAJO.OTM_UBICACION (ID_UBICACION);

alter table ORDENES_TRABAJO.OTM_MATERIAL
   add constraint FK_OTM_UBICACION_OTM_MATERIAL foreign key (ID_UBICACION_ADMINISTRA)
      references ORDENES_TRABAJO.OTM_UBICACION (ID_UBICACION);

alter table ORDENES_TRABAJO.OTM_MATERIAL
   add constraint FK_OTM_UNIDAD_MED_OTM_MATERIAL foreign key (ID_UNIDAD_MEDIDA)
      references ORDENES_TRABAJO.OTM_UNIDAD_MEDIDA (ID_UNIDAD_MEDIDA);

alter table ORDENES_TRABAJO.OTM_MATERIAL
   add constraint FK_OTM_CAT_FAM_OTM_MATERIAL foreign key (ID_CATEGORIA_MATERIAL, ID_SUBCATEGORIA_MATERIAL)
      references ORDENES_TRABAJO.OTM_SUBCATEGORIA_CATEGOR (ID_CATEGORIA_MATERIAL, ID_SUBCATEGORIA_MATERIAL);

alter table ORDENES_TRABAJO.OTM_SUBCATEGORIA_CATEGOR
   add constraint FK_OTM_CATEGORIA_OTM_SUB_CAT foreign key (ID_CATEGORIA_MATERIAL)
      references ORDENES_TRABAJO.OTM_CATEGORIA_MATERIAL (ID_CATEGORIA_MATERIAL);

alter table ORDENES_TRABAJO.OTM_SUBCATEGORIA_CATEGOR
   add constraint FK_OTM_SUBCAT_MAT_OTM_SUB_CAT foreign key (ID_SUBCATEGORIA_MATERIAL)
      references ORDENES_TRABAJO.OTM_SUBCATEGORIA_MATERIAL (ID_SUBCATEGORIA_MATERIAL);

alter table ORDENES_TRABAJO.OTM_SUBCATEGORIA_MATERIAL
   add constraint FK_OTM_UBICACION_OTM_CAT_MAT foreign key (ID_UBICACION_ADMINISTRA)
      references ORDENES_TRABAJO.OTM_UBICACION (ID_UBICACION);

alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   add constraint FK_OTF_INVENTARIO_OTT_DETALLE foreign key (ID_ALMACEN_BODEGA, ID_MATERIAL, ID_UBICACION_ADMINISTRA)
      references ORDENES_TRABAJO.OTF_INVENTARIO (ID_ALMACEN_BODEGA, ID_MATERIAL, ID_UBICACION_ADMINISTRA);

alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   add constraint FK_OTT_ORDEN_TRAB_OTT_DETALLE foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   add constraint FK_OTM_VIA_CONT_OTT_DETALLE foreign key (ID_VIA_COMPRA_CONTRATO)
      references ORDENES_TRABAJO.OTM_VIA_COMPRA_CONTRATO (ID_VIA_COMPRA_CONTRATO);

alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   add constraint FK_OTM_MATERIAL_OTT_DETALLE foreign key (ID_UBICACION_ADMINISTRA, ID_MATERIAL)
      references ORDENES_TRABAJO.OTM_MATERIAL (ID_UBICACION_ADMINISTRA, ID_MATERIAL);

alter table ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL
   add constraint OTT_ORDEN_TRABAJO_OTT_SOL_MAT foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references ORDENES_TRABAJO.OTT_ORDEN_TRABAJO (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL
   add constraint FK_OTT_ADJUNTO_OTT_SOLICITUD foreign key (ID_TIPO_DOCUMENTO_RESPUESTA, ID_ETAPA_ORDEN_TRABAJO_RES, ID_ADJUNTO_RESPUESTA)
      references ORDENES_TRABAJO.OTT_ADJUNTO_ORDEN_TRABAJO (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO);

alter table ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL
   add constraint FK_OTT_ADJUNTO_OTT_SOLICITUD_2 foreign key (ID_TIPO_DOCUMENTO_SOLICITA, ID_ETAPA_ORDEN_TRABAJO_SOL, ID_ADJUNTO_SOLICITA)
      references ORDENES_TRABAJO.OTT_ADJUNTO_ORDEN_TRABAJO (ID_TIPO_DOCUMENTO, ID_ETAPA_ORDEN_TRABAJO, ID_ADJUNTO_ORDEN_TRABAJO);

alter table ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL
   add constraint FK_OTC_ESTADO_OTT_SOL_MAT foreign key (ESTADO_SOL_MATERIAL)
      references OTC_ESTADO_SOL_MATERIAL (ESTADO_SOL_MATERIAL);

--02-06-16
/*alter table ORDENES_TRABAJO.OTL_DETALLE_MATERIAL
   add constraint FK_SOLICITUD_OTL_DETALLE_MAT foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL (ID_UBICACION, ID_ORDEN_TRABAJO);*/

/*alter table ORDENES_TRABAJO.OTL_DETALLE_MATERIAL
   add constraint FK_OTM_MATERIAL_OTL_DETALLE foreign key (ID_UBICACION_ADMINISTRA, ID_MATERIAL)
      references ORDENES_TRABAJO.OTM_MATERIAL (ID_UBICACION_ADMINISTRA, ID_MATERIAL);*/

/*alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   add constraint FK_OTF_INVENTARIO_OTT_DETALLE foreign key (ID_ALMACEN_BODEGA, ID_MATERIAL, ID_UBICACION_ADMINISTRA)
      references ORDENES_TRABAJO.OTF_INVENTARIO (ID_ALMACEN_BODEGA, ID_MATERIAL, ID_UBICACION_ADMINISTRA);*/

/*alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   add constraint FK_OTT_ORDEN_TRAB_OTT_DETALLE foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL (ID_UBICACION, ID_ORDEN_TRABAJO);*/

/*alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   add constraint FK_OTM_VIA_CONT_OTT_DETALLE foreign key (ID_VIA_COMPRA_CONTRATO)
      references ORDENES_TRABAJO.OTM_VIA_COMPRA_CONTRATO (ID_VIA_COMPRA_CONTRATO);*/

/*alter table ORDENES_TRABAJO.OTT_DETALLE_MATERIAL
   add constraint FK_OTM_MATERIAL_OTT_DETALLE foreign key (ID_UBICACION_ADMINISTRA, ID_MATERIAL)
      references ORDENES_TRABAJO.OTM_MATERIAL (ID_UBICACION_ADMINISTRA, ID_MATERIAL);*/


--09-06-16
alter table ORDENES_TRABAJO.OTT_DETALLE_RETIRO
   add constraint FK_OTT_DETALLE_MAT_OTT_SOL_RET foreign key (ID_DETALLE_MATERIAL)
      references ORDENES_TRABAJO.OTT_DETALLE_MATERIAL (ID_DETALLE_MATERIAL);

alter table ORDENES_TRABAJO.OTT_DETALLE_RETIRO
   add constraint FK_OTT_SOLICITUD_RETIRO_OTT_DE foreign key (ANNO, ID_SOLICITUD_RETIRO)
      references OTT_SOLICITUD_RETIRO (ANNO, ID_SOLICITUD_RETIRO);

alter table OTT_SOLICITUD_RETIRO
   add constraint FK_OTT_SOL_MAT_OTT_SOL_RET foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table OTT_SOLICITUD_RETIRO
   add constraint FK_OTC_ESTADO_OTT_SOLICITUD foreign key (ESTADO_SOLICITUD_RETIRO)
      references OTC_ESTADO_SOLICITUD_RET (ESTADO_SOLICITUD_RETIRO);
      
alter table OTM_ENCARGADO_ALMACEN
   add constraint FK_EU_EMPLEADOS_OTM_ENC_ALMAC foreign key (NUM_EMPLEADO)
      references EU_EMPLEADOS (NUM_EMPLEADO);

alter table OTM_ENCARGADO_ALMACEN
   add constraint FK_OTM_UBICA_OTM_ENCARGADO foreign key (ID_UBICACION_ADMINISTRA)
      references ORDENES_TRABAJO.OTM_UBICACION (ID_UBICACION);
      
alter table ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO
   add constraint FK_OTT_DET_MAT_OTT_SOL_REING foreign key (ID_DETALLE_MATERIAL)
      references ORDENES_TRABAJO.OTT_DETALLE_MATERIAL (ID_DETALLE_MATERIAL);

alter table ORDENES_TRABAJO.OTT_SOLICITUD_REINGRESO
   add constraint FK_OTM_UBICACION_OTT_SOL_REIN foreign key (ID_UBICACION_ADMINISTRA)
      references ORDENES_TRABAJO.OTM_UBICACION (ID_UBICACION);
      
alter table ORDENES_TRABAJO.OTL_TRAZABILIDAD_SOL_MAT
   add constraint FK_OTT_SOLICITUD_MAT_OTL_TRAZ foreign key (ID_UBICACION, ID_ORDEN_TRABAJO)
      references ORDENES_TRABAJO.OTT_SOLICITUD_MATERIAL (ID_UBICACION, ID_ORDEN_TRABAJO);

alter table ORDENES_TRABAJO.OTL_TRAZABILIDAD_SOL_MAT
   add constraint FK_OTC_ESTADO_OTL_TRAZABILIDAD foreign key (ESTADO_SOL_MATERIAL)
      references OTC_ESTADO_SOL_MATERIAL (ESTADO_SOL_MATERIAL);
      
--04-08-16
alter table ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE
   add constraint FK_OTF_INCIDENT_ALM_OTF_ADJUNT foreign key (ID_INCIDENTE_ALMACEN)
      references ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN (ID_INCIDENTE_ALMACEN);

alter table ORDENES_TRABAJO.OTF_ADJUNTO_INCIDENTE
   add constraint FK_OTM_TIPO_DOCUM_OTF_ADJUNTO foreign key (ID_TIPO_DOCUMENTO)
      references ORDENES_TRABAJO.OTM_TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO);

alter table ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN
   add constraint FK_OTM_TIPO_INCIDENTE_OTF_INC foreign key (ID_TIPO_INCIDENTE)
      references ORDENES_TRABAJO.OTM_TIPO_INCIDENTE (ID_TIPO_INCIDENTE);

alter table ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN
   add constraint FK_OTM_ALM_BOD_OTF_INCIDENT foreign key (ID_ALMACEN_BODEGA)
      references ORDENES_TRABAJO.OTM_ALMACEN_BODEGA (ID_ALMACEN_BODEGA);

alter table ORDENES_TRABAJO.OTF_INCIDENTE_ALMACEN
   add constraint FK_OTM_MATERIAL_OTF_INCIDENTE foreign key (ID_UBICACION, ID_MATERIAL)
      references ORDENES_TRABAJO.OTM_MATERIAL (ID_UBICACION_ADMINISTRA, ID_MATERIAL);

alter table ORDENES_TRABAJO.OTM_CORREO_PROVEEDOR
   add constraint FK_OTM_PROVEEDOR_OTM_CORREO foreign key (IDENTIFICACION)
      references ORDENES_TRABAJO.OTM_PROVEEDOR (IDENTIFICACION);

alter table ORDENES_TRABAJO.OTM_TELEFONO_PROVEEDOR
   add constraint FK_OTM_PROVEEDOR_OTM_TELEFONO foreign key (IDENTIFICACION)
      references ORDENES_TRABAJO.OTM_PROVEEDOR (IDENTIFICACION);
--09-08-16
alter table ORDENES_TRABAJO.OTL_LINEA_TRASLADO
   add constraint FK_OTT_SOL_TRAS_OTL_LINEA_TRAS foreign key (ID_UBICACION, ANNO, ID_SOLICITUD_TRASLADO)
      references ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO (ID_UBICACION, ANNO, ID_SOLICITUD_TRASLADO);

alter table ORDENES_TRABAJO.OTL_LINEA_TRASLADO
   add constraint FK_OTF_INVENT_OTL_LINEA_TRAS foreign key (ID_ALMACEN, ID_MATERIAL, ID_UBICACION)
      references ORDENES_TRABAJO.OTF_INVENTARIO (ID_ALMACEN_BODEGA, ID_MATERIAL, ID_UBICACION_ADMINISTRA);

alter table ORDENES_TRABAJO.OTL_SOLICITUD_TRASLADO
   add constraint FK_OTM_UBICA_OTL_SOL_TRASLADO foreign key (ID_UBICACION)
      references ORDENES_TRABAJO.OTM_UBICACION (ID_UBICACION);

alter table ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO
   add constraint FK_OTC_ESTADO_OTL_TRAZABIL foreign key (ESTADO_TRASLADO)
      references ORDENES_TRABAJO.OTC_ESTADO_TRASLADO (ESTADO_TRASLADO);

alter table ORDENES_TRABAJO.OTL_TRAZABIL_SOL_TRASLADO
   add constraint FK_OTT_SOL_OTL_TRAZABIL_SOL foreign key (ID_UBICACION, ANNO, ID_SOLICITUD_TRASLADO)
      references ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO (ID_UBICACION, ANNO, ID_SOLICITUD_TRASLADO);

alter table ORDENES_TRABAJO.OTT_LINEA_TRASLADO
   add constraint FK_OTF_INVENTARIO_OTT_LINEA foreign key (ID_ALMACEN, ID_MATERIAL, ID_UBICACION)
      references ORDENES_TRABAJO.OTF_INVENTARIO (ID_ALMACEN_BODEGA, ID_MATERIAL, ID_UBICACION_ADMINISTRA);

alter table ORDENES_TRABAJO.OTT_LINEA_TRASLADO
   add constraint FK_OTT_SOL_TRASL_OTT_LINEA foreign key (ID_UBICACION, ANNO, ID_SOLICITUD_TRASLADO)
      references ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO (ID_UBICACION, ANNO, ID_SOLICITUD_TRASLADO);

alter table ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO
   add constraint FK_OTM_ALM_BODEGA_OTT_SOLICIT foreign key (ID_ALMACEN)
      references ORDENES_TRABAJO.OTM_ALMACEN_BODEGA (ID_ALMACEN_BODEGA);

alter table ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO
   add constraint FK_OTM_ALMACEN_BODEGA_OTT_SOL foreign key (ID_BODEGA)
      references ORDENES_TRABAJO.OTM_ALMACEN_BODEGA (ID_ALMACEN_BODEGA);

alter table ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO
   add constraint FK_OTC_ESTADO_OTT_SOLIC_TRAS foreign key (ESTADO_TRASLADO)
      references ORDENES_TRABAJO.OTC_ESTADO_TRASLADO (ESTADO_TRASLADO);

alter table ORDENES_TRABAJO.OTT_SOLICITUD_TRASLADO
   add constraint FK_OTM_UBICA_OTT_SOLIC_TRAS foreign key (ID_UBICACION)
      references ORDENES_TRABAJO.OTM_UBICACION (ID_UBICACION);


--17-08-2016
alter table ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP
   add constraint FK_OTT_GC_OTL_TRAZABIL_GC foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO)
      references ORDENES_TRABAJO.OTT_GESTION_COMPRA (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO);

alter table ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_COMP
   add constraint FK_OTC_ESTADO_OTL_TRAZABIL_GC foreign key (ESTADO)
      references ORDENES_TRABAJO.OTC_ESTADO_GESTION_COMPRA (ESTADO);

alter table OTM_FLUJO_GESTION_COMPRA
   add constraint FK_OTC_ESTADO_OTM_FLUJO foreign key (ESTADO)
      references ORDENES_TRABAJO.OTC_ESTADO_GESTION_COMPRA (ESTADO);

alter table OTM_FLUJO_GESTION_COMPRA
   add constraint FK_OTM_VIA_COMPRA_OTM_FLUJO foreign key (ID_VIA_COMPRA_CONTRATO)
      references ORDENES_TRABAJO.OTM_VIA_COMPRA_CONTRATO (ID_VIA_COMPRA_CONTRATO);

alter table ORDENES_TRABAJO.OTT_GESTION_COMPRA
   add constraint FK_OTM_UBICA_OTT_GESTION_COM foreign key (ID_UBICACION)
      references ORDENES_TRABAJO.OTM_UBICACION (ID_UBICACION);

alter table ORDENES_TRABAJO.OTT_GESTION_COMPRA
   add constraint FK_OTM_VIA_COMPRA_OTT_GC foreign key (ID_VIA_COMPRA_CONTRATO)
      references ORDENES_TRABAJO.OTM_VIA_COMPRA_CONTRATO (ID_VIA_COMPRA_CONTRATO);

alter table ORDENES_TRABAJO.OTT_GESTION_COMPRA
   add constraint FK_OTC_ESTADO_OTT_GESTION foreign key (ESTADO, ID_VIA_COMPRA_CONTRATO)
      references OTM_FLUJO_GESTION_COMPRA (ESTADO, ID_VIA_COMPRA_CONTRATO);

alter table ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA
   add constraint FK_OTT_GESTION_OTT_LINEA_GC foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO)
      references ORDENES_TRABAJO.OTT_GESTION_COMPRA (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO);

alter table ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA
   add constraint FK_OTT_DET_MAT_OTT_LINEA_GC foreign key (ID_DETALLE_MATERIAL)
      references ORDENES_TRABAJO.OTT_DETALLE_MATERIAL (ID_DETALLE_MATERIAL);

alter table ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA
   add constraint FK_OTM_MATERIAL_OTT_LINEA foreign key (ID_UBICACION, ID_MATERIAL)
      references ORDENES_TRABAJO.OTM_MATERIAL (ID_UBICACION_ADMINISTRA, ID_MATERIAL);
      
      
--25-08-16
alter table ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION
   add constraint FK_OTM_TIPO_DOC_OTT_ADJ_COT foreign key (ID_TIPO_DOCUMENTO)
      references ORDENES_TRABAJO.OTM_TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO);

alter table ORDENES_TRABAJO.OTT_ADJUNTO_COTIZACION
   add constraint FK_OTT_PROV_OTT_ADJUNTO foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, IDENTIFICACION, NUMERO_GESTION, ANNO)
      references ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, IDENTIFICACION, NUMERO_GESTION, ANNO);

alter table ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA
   add constraint FK_OTT_GESTION_OTT_GRUPO_GES foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO)
      references ORDENES_TRABAJO.OTT_GESTION_COMPRA (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO);

alter table ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA
   add constraint FK_OTM_MATERIAL_OTT_GRUPO foreign key (ID_UBICACION, ID_MATERIAL)
      references ORDENES_TRABAJO.OTM_MATERIAL (ID_UBICACION_ADMINISTRA, ID_MATERIAL);

alter table ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR
   add constraint FK_OTT_GRUPO_OTT_OFERTA_PROV foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, ID_GRUPO_GESTION_COMPRA)
      references ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, ID_GRUPO_GESTION_COMPRA);

alter table ORDENES_TRABAJO.OTT_OFERTA_PROVEEDOR
   add constraint FK_OTT_PROV_OTT_OFERTA_PROV foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, IDENTIFICACION, NUMERO_GESTION, ANNO)
      references ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, IDENTIFICACION, NUMERO_GESTION, ANNO);

alter table ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION
   add constraint FK_OTM_PROVEEDOR_OTT_PROV_COT foreign key (IDENTIFICACION)
      references ORDENES_TRABAJO.OTM_PROVEEDOR (IDENTIFICACION);

alter table ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION
   add constraint FK_OTT_GESTION_OTT_PROV_COT foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO)
      references ORDENES_TRABAJO.OTT_GESTION_COMPRA (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO);

--09-09-2016
alter table OTL_DET_APROVISIONAMIENTO
   add constraint FK_OTT_APROV_OTL_DET_APROV foreign key (ID_UBICACION, NUMERO_GESTION, ANNO)
      references ORDENES_TRABAJO.OTT_APROVISIONAMIENTO (ID_UBICACION, NUMERO_GESTION, ANNO);

alter table OTL_DET_APROVISIONAMIENTO
   add constraint FK_OTM_MATERIAL_OTL_DET_APROV foreign key (ID_UBICACION, ID_MATERIAL)
      references ORDENES_TRABAJO.OTM_MATERIAL (ID_UBICACION_ADMINISTRA, ID_MATERIAL);

alter table ORDENES_TRABAJO.OTL_TRAZABILIDAD_AJUSTE
   add constraint FK_OTC_ESTADO_AJ_OTL_TRAZABILI foreign key (ESTADO_AJUSTE)
      references ORDENES_TRABAJO.OTC_ESTADO_AJUSTE (ESTADO_AJUSTE);

alter table ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR
   add constraint FK_OTT_ESTADO_GESTION_OTL_TRAZ foreign key (ESTADO_GESTION_INGRESO)
      references ORDENES_TRABAJO.OTC_ESTADO_GESTION_INGRES (ESTADO_GESTION_INGRESO);
	  
alter table ORDENES_TRABAJO.OTL_TRAZABIL_GESTION_INGR

   add constraint FK_OTL_TRAZ_RELATIONS_OTT_GEST foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, CONSECUTIVO)

      references ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, CONSECUTIVO);	  

alter table ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION
   add constraint FK_OTT_GEST_OTT_ACLARACION foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO)
      references ORDENES_TRABAJO.OTT_GESTION_COMPRA (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO);

alter table ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR
   add constraint FK_OTT_GESTION_OTT_ADJUNTO foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO)
      references ORDENES_TRABAJO.OTT_GESTION_COMPRA (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO);

alter table ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_COMPR
   add constraint FK_OTM_TIPO_DOC_OTT_ADJUNTO_GC foreign key (ID_TIPO_DOCUMENTO)
      references ORDENES_TRABAJO.OTM_TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO);

alter table ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR
   add constraint FK_OTT_GESTION_INGR_OTT_ADJUNT foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, CONSECUTIVO)
      references ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, CONSECUTIVO);

alter table ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR
   add constraint FK_OTM_TIPO_DOC_OTT_ADJUNTO foreign key (ID_TIPO_DOCUMENTO)
      references ORDENES_TRABAJO.OTM_TIPO_DOCUMENTO (ID_TIPO_DOCUMENTO);

alter table ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO
   add constraint FK_OTM_UBICA_OTT_AJUSTE_INV foreign key (ID_UBICACION)
      references ORDENES_TRABAJO.OTM_UBICACION (ID_UBICACION);

alter table ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO
   add constraint FK_OTM_ALMACEN_OTT_AJUSTE_INV foreign key (ID_ALMACEN_BODEGA)
      references ORDENES_TRABAJO.OTM_ALMACEN_BODEGA (ID_ALMACEN_BODEGA);

alter table ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO
   add constraint FK_OTC_ESTADO_OTT_AJUSTE_INV foreign key (ESTADO_AJUSTE)
      references ORDENES_TRABAJO.OTC_ESTADO_AJUSTE (ESTADO_AJUSTE);

alter table ORDENES_TRABAJO.OTT_APROVISIONAMIENTO
   add constraint FK_OTM_UBICA_OTT_APROVISIONAM foreign key (ID_UBICACION)
      references ORDENES_TRABAJO.OTM_UBICACION (ID_UBICACION);

alter table ORDENES_TRABAJO.OTT_APROVISIONAMIENTO
   add constraint FK_OTM_VIA_COMPRA_OTT_APROV foreign key (ID_VIA_COMPRA_CONTRATO)
      references ORDENES_TRABAJO.OTM_VIA_COMPRA_CONTRATO (ID_VIA_COMPRA_CONTRATO);

alter table OTT_DESTINATAR_ACLARACION
   add constraint FK_OTT_ACLARACION_OTT_DESTIN foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, ID_NOTIFICACION)
      references ORDENES_TRABAJO.OTT_ACLARACION_COTIZACION (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, ID_NOTIFICACION);

alter table OTT_DESTINATAR_ACLARACION
   add constraint FK_OTM_PROVEEDOR_OTT_DESTIN foreign key (IDENTIFICACION)
      references ORDENES_TRABAJO.OTM_PROVEEDOR (IDENTIFICACION);

alter table ORDENES_TRABAJO.OTT_DETALLE_AJUSTE
   add constraint FK_OTT_AJUSTE_OTT_DETALLE_AJUS foreign key (ID_UBICACION, ANNO, CONSECUTIVO_AJUSTE)
      references ORDENES_TRABAJO.OTT_AJUSTE_INVENTARIO (ID_UBICACION, ANNO, CONSECUTIVO_AJUSTE);

alter table ORDENES_TRABAJO.OTT_DETALLE_AJUSTE
   add constraint FK_OTM_MATERIAL_OTT_DETALLE_AJ foreign key (ID_UBICACION, ID_MATERIAL)
      references ORDENES_TRABAJO.OTM_MATERIAL (ID_UBICACION_ADMINISTRA, ID_MATERIAL);

alter table ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR
   add constraint FK_OTT_ADJUNTO_OTT_DETALLE_GI foreign key (ID_ADJUNTO_GESTION_INGR)
      references ORDENES_TRABAJO.OTT_ADJUNTO_GESTION_INGR (ID_ADJUNTO_GESTION_INGR);

alter table ORDENES_TRABAJO.OTT_DETALLE_GESTION_INGR
   add constraint FK_OTT_LINEA_OTT_DETALLE_GEST foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, ID_LINEA_GESTION_COMPRA)
      references ORDENES_TRABAJO.OTT_LINEA_GESTION_COMPRA (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO, ID_LINEA_GESTION_COMPRA);

alter table ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO
   add constraint FK_OTT_APROV_OTT_DET_APROV foreign key (ID_UBICACION, NUMERO_GESTION, ANNO)
      references ORDENES_TRABAJO.OTT_APROVISIONAMIENTO (ID_UBICACION, NUMERO_GESTION, ANNO);

alter table ORDENES_TRABAJO.OTT_DET_APROVISIONAMIENTO
   add constraint FK_OTM_MATERIAL_OTT_DET_APROV foreign key (ID_UBICACION, ID_MATERIAL)
      references ORDENES_TRABAJO.OTM_MATERIAL (ID_UBICACION_ADMINISTRA, ID_MATERIAL);

alter table ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER
   add constraint FK_OTC_ESTADO_OTT_GESTION_INGR foreign key (ESTADO_GESTION_INGRESO)
      references ORDENES_TRABAJO.OTC_ESTADO_GESTION_INGRES (ESTADO_GESTION_INGRESO);

alter table ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER
   add constraint FK_OTT_GESTION_COMPRA_OTT_GEST foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO)
      references ORDENES_TRABAJO.OTT_GESTION_COMPRA (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO);

alter table ORDENES_TRABAJO.OTT_GESTION_INGRESO_MATER
   add constraint FK_OTT_PROVEEDOR_OTT_GESTION foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, IDENTIFICACION, NUMERO_GESTION, ANNO)
      references ORDENES_TRABAJO.OTT_PROVEEDOR_COTIZACION (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, IDENTIFICACION, NUMERO_GESTION, ANNO);
    
alter table ORDENES_TRABAJO.OTT_GESTION_COMPRA
   add constraint FK_OTT_GEST_COMP_OTT_GEST_COMP foreign key (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION_REFERENCIA, ANNO_REFERENCIA)
      references ORDENES_TRABAJO.OTT_GESTION_COMPRA (ID_UBICACION, ID_VIA_COMPRA_CONTRATO, NUMERO_GESTION, ANNO);
      
alter table ORDENES_TRABAJO.OTT_GRUPO_GESTION_COMPRA
   add constraint FK_OTC_ESTADO_OTT_GRUPO_GC foreign key (ESTADO_LINEA)
      references ORDENES_TRABAJO.OTC_ESTADO_LINEA (ESTADO_LINEA);
      

