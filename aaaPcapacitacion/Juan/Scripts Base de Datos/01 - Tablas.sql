alter table TCM_AUTOR_POR_LIBRO
   drop constraint FK_TCM_AUTO_TCM_AUTOR_TCM_LIBR;

alter table TCM_AUTOR_POR_LIBRO
   drop constraint FK_TCM_AUTO_TCM_AUTOR_TCM_AUTO;

alter table TCM_LIBRO
   drop constraint FK_TCM_LIBRO_TCC_CONDICION_LIB;

drop table TCC_CONDICION_LIBRO cascade constraints;

drop table TCM_AUTOR cascade constraints;

drop table TCM_AUTOR_POR_LIBRO cascade constraints;

drop table TCM_LIBRO cascade constraints;

/*==============================================================*/
/* Table: TCC_CONDICION_LIBRO                                   */
/*==============================================================*/
create table TCC_CONDICION_LIBRO  (
   CONDICION_LIBRO      VARCHAR2(3)                     not null,
   DESCRIPCION          VARCHAR2(50)                    not null,
   constraint PK_TCC_CONDICION_LIBRO primary key (CONDICION_LIBRO)
);

comment on table TCC_CONDICION_LIBRO is
'Listado de posibles condiciones de un libro al momento de ser adquirido';

comment on column TCC_CONDICION_LIBRO.CONDICION_LIBRO is
'Abreviatura que identifica la condición de un libro';

comment on column TCC_CONDICION_LIBRO.DESCRIPCION is
'Descripción de la condición del libro';

/*==============================================================*/
/* Table: TCM_AUTOR                                             */
/*==============================================================*/
create table TCM_AUTOR  (
   ID_PERSONAL          VARCHAR2(20)                    not null,
   NOMBRE               VARCHAR2(50)                    not null,
   PRIMER_APELLIDO      VARCHAR2(50)                    not null,
   SEGUNDO_APELLIDO     VARCHAR2(50),
   FECHA_HORA_NACIMIENTO DATE                            not null,
   ESTADO               VARCHAR2(3)                    default 'ACT' not null
      constraint CK_ESTADO_AUTOR check (ESTADO in ('ACT','INA') and ESTADO = upper(ESTADO)),
   TIME_STAMP           DATE                           default SYSDATE not null,
   USUARIO              VARCHAR2(256)                   not null,
   constraint PK_TCM_AUTOR primary key (ID_PERSONAL),
   constraint AK_TCM_AUTOR unique (NOMBRE)
);

comment on table TCM_AUTOR is
'Catálogo global de autores';

comment on column TCM_AUTOR.ID_PERSONAL is
'Número de Identificación';

comment on column TCM_AUTOR.NOMBRE is
'Nombre del autor';

comment on column TCM_AUTOR.PRIMER_APELLIDO is
'Primer apellido del autor';

comment on column TCM_AUTOR.SEGUNDO_APELLIDO is
'Segundo apellido del autor';

comment on column TCM_AUTOR.FECHA_HORA_NACIMIENTO is
'Fecha y hora de nacimiento';

comment on column TCM_AUTOR.ESTADO is
'Estado del registro.';

comment on column TCM_AUTOR.TIME_STAMP is
'Control de concurrencia';

comment on column TCM_AUTOR.USUARIO is
'Usuario que manipula la información del registro';

/*==============================================================*/
/* Table: TCM_AUTOR_POR_LIBRO                                   */
/*==============================================================*/
create table TCM_AUTOR_POR_LIBRO  (
   ISBN                 VARCHAR2(13)                    not null,
   ID_PERSONAL          VARCHAR2(20)                    not null,
   constraint PK_TCM_AUTOR_POR_LIBRO primary key (ISBN, ID_PERSONAL)
);

comment on table TCM_AUTOR_POR_LIBRO is
'Catálogo de autores por libro';

comment on column TCM_AUTOR_POR_LIBRO.ISBN is
'Número Estándar Internacional de Libros. Es un identificador único para libros previsto para uso comercial, los hay de 10 y 13 dígitos';

comment on column TCM_AUTOR_POR_LIBRO.ID_PERSONAL is
'Número de Identificación';

/*==============================================================*/
/* Table: TCM_LIBRO                                             */
/*==============================================================*/
create table TCM_LIBRO  (
   ISBN                 VARCHAR2(13)                    not null,
   CONDICION_LIBRO      VARCHAR2(3)                     not null,
   TITULO               VARCHAR2(100)                   not null,
   RESUMEN              VARCHAR2(4000),
   TOTAL_PAGINAS        NUMBER(3,0)                    default 10 not null
      constraint CK_TOTAL_PAGINAS_LIBRO check (TOTAL_PAGINAS between 10 and 100),
   FECHA_HORA_IMPRESION DATE                            not null,
   TIME_STAMP           DATE                           default SYSDATE not null,
   USUARIO              VARCHAR2(256)                   not null,
   constraint PK_TCM_LIBRO primary key (ISBN)
);

comment on table TCM_LIBRO is
'Catálog global de libros';

comment on column TCM_LIBRO.ISBN is
'Número Estándar Internacional de Libros. Es un identificador único para libros previsto para uso comercial, los hay de 10 y 13 dígitos';

comment on column TCM_LIBRO.CONDICION_LIBRO is
'Abreviatura que identifica la condición de un libro';

comment on column TCM_LIBRO.TITULO is
'Título del libro';

comment on column TCM_LIBRO.RESUMEN is
'Resumen del libro';

comment on column TCM_LIBRO.TOTAL_PAGINAS is
'Indica la cantidad de páginas del libro. Solo permite registrar libros con más de 10 páginas hasta un máximo de 100';

comment on column TCM_LIBRO.FECHA_HORA_IMPRESION is
'Fecha y hora en la cual fue impreso el libro';

comment on column TCM_LIBRO.TIME_STAMP is
'Control de concurrencia';

comment on column TCM_LIBRO.USUARIO is
'Usuario que manipula la información del registro';

alter table TCM_AUTOR_POR_LIBRO
   add constraint FK_TCM_AUTO_TCM_AUTOR_TCM_LIBR foreign key (ISBN)
      references TCM_LIBRO (ISBN);

alter table TCM_AUTOR_POR_LIBRO
   add constraint FK_TCM_AUTO_TCM_AUTOR_TCM_AUTO foreign key (ID_PERSONAL)
      references TCM_AUTOR (ID_PERSONAL);

alter table TCM_LIBRO
   add constraint FK_TCM_LIBRO_TCC_CONDICION_LIB foreign key (CONDICION_LIBRO)
      references TCC_CONDICION_LIBRO (CONDICION_LIBRO);
