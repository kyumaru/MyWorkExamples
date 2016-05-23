CREATE TABLE sucursal
(
  idsucursal serial NOT NULL,
  nombre character varying(30),
  direccion character varying(50),
  CONSTRAINT sucursal_pkey PRIMARY KEY (idsucursal)
);


CREATE TABLE caja
(
  idcaja serial NOT NULL,
  idsucursal integer NOT NULL,
  nombre character varying(50) NOT NULL,
  CONSTRAINT caja_pkey PRIMARY KEY (idcaja),
  CONSTRAINT inventario_idsucursal_fkey FOREIGN KEY (idsucursal)
      REFERENCES sucursal (idsucursal) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE CASCADE
);


CREATE TABLE cajadetalle
(
  idcajadetalle serial NOT NULL,
  idcaja integer NOT NULL,
  montoinicial numeric(10,2) NOT NULL,
  montofinal numeric(10,2),
  fechainicio timestamp without time zone NOT NULL,
  fechafinal timestamp without time zone,
  idempleadoadinicia integer NOT NULL,
  idempleadoadcierra integer,
  estado character varying(1) NOT NULL,
  idcajero integer NOT NULL,
  CONSTRAINT cajadetalle_pkey PRIMARY KEY (idcajadetalle),
  CONSTRAINT id_caja_fkey FOREIGN KEY (idcaja)
      REFERENCES caja (idcaja) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE CASCADE
);


CREATE TABLE nomcaja
(
  nombre character varying(50)
);



CREATE TABLE persona
(
  cedula character varying(40) NOT NULL,
  CONSTRAINT persona_pkey PRIMARY KEY (cedula)
);



CREATE TABLE personafisica
(
  cedula character varying(40) NOT NULL,
  nombre1 character varying(15) NOT NULL,
  apellido1 character varying(15),
  apellido2 character varying(15),
  email character varying(100),
  CONSTRAINT personafisica_pkey PRIMARY KEY (cedula, nombre1),
  CONSTRAINT personafisica_cedula_fkey FOREIGN KEY (cedula)
      REFERENCES persona (cedula) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE NO ACTION
);



CREATE TABLE personajuridica
(
  cedula character varying(40) NOT NULL,
  razonsocial character varying(100) NOT NULL,
  CONSTRAINT personajuridica_pkey PRIMARY KEY (cedula, razonsocial),
  CONSTRAINT personajuridica_cedula_fkey FOREIGN KEY (cedula)
      REFERENCES persona (cedula) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE NO ACTION
);



CREATE TABLE cliente
(
  idcliente serial NOT NULL,
  cedula character varying(40) NOT NULL,
  tipocliente character varying(1) NOT NULL,
  CONSTRAINT cliente_pkey PRIMARY KEY (idcliente),
  CONSTRAINT cliente_cedula_fkey FOREIGN KEY (cedula)
      REFERENCES persona (cedula) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE NO ACTION,
  CONSTRAINT cliente_idcliente_cedula_key UNIQUE (idcliente, cedula)
);




CREATE TABLE cuentaxcobrar
(
  idcuentaxcobrar serial NOT NULL,
  CONSTRAINT cuentaxcobrar_pkey PRIMARY KEY (idcuentaxcobrar)
);




CREATE TABLE cuentaxpagar
(
  idcuentaxpagar serial NOT NULL,
  CONSTRAINT cuentaxpagar_pkey PRIMARY KEY (idcuentaxpagar)
);


CREATE TABLE facturacliente
(
  idfactura serial NOT NULL,
  fecha date NOT NULL,
  idcliente integer NOT NULL,
  monto numeric(10,2),
  idempleado integer NOT NULL,
  idcaja integer,
  CONSTRAINT facturacliente_pkey PRIMARY KEY (idfactura),
  CONSTRAINT facturacliente_idcliente_fkey FOREIGN KEY (idcliente)
      REFERENCES cliente (idcliente) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE CASCADE
);



CREATE TABLE proveedor
(
  idproveedor serial NOT NULL,
  cedula character varying(40) NOT NULL,
  razonsocial character varying(100) NOT NULL,
  "estaActivo" boolean,
  CONSTRAINT proveedor_pkey PRIMARY KEY (idproveedor),
  CONSTRAINT proveedor_cedula_fkey FOREIGN KEY (cedula)
      REFERENCES persona (cedula) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE NO ACTION
);



CREATE TABLE producto
(
  idproducto character varying(40) NOT NULL,
  nombre character varying(50) NOT NULL,
  preciocosto numeric(10,3) NOT NULL,
  idproveedor integer NOT NULL,
  esexento boolean NOT NULL,
  porcentajeutilidad numeric(10,3) NOT NULL,
  tipoventa character varying(1),
  CONSTRAINT producto_pkey PRIMARY KEY (idproducto),
  CONSTRAINT producto_idproveedor_fkey FOREIGN KEY (idproveedor)
      REFERENCES proveedor (idproveedor) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE CASCADE
);


CREATE TABLE facturadetalle
(
  idfactura integer NOT NULL,
  idproducto character varying(40) NOT NULL,
  precioventa numeric(10,3) NOT NULL,
  cantidad integer NOT NULL,
  total numeric(10,2) NOT NULL,
  CONSTRAINT facturadetalle_pkey PRIMARY KEY (idfactura, idproducto),
  CONSTRAINT facturadetalle_idfactura_fkey FOREIGN KEY (idfactura)
      REFERENCES facturacliente (idfactura) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT facturadetalle_idproducto_fkey FOREIGN KEY (idproducto)
      REFERENCES producto (idproducto) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);





CREATE TABLE empleado
(
  idempleado serial NOT NULL,
  cedula character varying(40) NOT NULL,
  nombre1 character varying(15) NOT NULL,
  tipoempleado character varying(1) NOT NULL,
  fechainicio date NOT NULL,
  contrasenia integer NOT NULL,
  sueldo numeric(10,2) NOT NULL,
  idsucursal integer,
  CONSTRAINT empleado_pkey PRIMARY KEY (idempleado, cedula, nombre1),
  CONSTRAINT empleado_cedula_fkey FOREIGN KEY (cedula, nombre1)
      REFERENCES personafisica (cedula, nombre1) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE NO ACTION,
  CONSTRAINT empleado_idsucursal_fkey FOREIGN KEY (idsucursal)
      REFERENCES sucursal (idsucursal) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE CASCADE
);





CREATE TABLE cobro
(
  idfactura serial NOT NULL,
  idcuentaxcobrar serial NOT NULL,
  cedulapersona character varying(40),
  abono numeric(10,2),
  monto numeric(10,2),
  fechaxabono date,
  fechaproxpago date,
  CONSTRAINT cobro_pkey PRIMARY KEY (idfactura, idcuentaxcobrar),
  CONSTRAINT cobro_cedulapersona_fkey FOREIGN KEY (cedulapersona)
      REFERENCES persona (cedula) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE CASCADE,
  CONSTRAINT cobro_idcuentaxcobrar_fkey FOREIGN KEY (idcuentaxcobrar)
      REFERENCES cuentaxcobrar (idcuentaxcobrar) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION
);



CREATE TABLE direcciones
(
  cedula character varying(40) NOT NULL,
  provincia character varying(100),
  canton character varying(100),
  distrito character varying(100),
  senias character varying(100) NOT NULL,
  CONSTRAINT direcciones_pkey PRIMARY KEY (cedula),
  CONSTRAINT direcciones_cedula_fkey FOREIGN KEY (cedula)
      REFERENCES persona (cedula) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE NO ACTION
);





CREATE TABLE inventario
(
  idproducto character varying(40) NOT NULL,
  cantidadminima integer NOT NULL,
  cantidadactual integer NOT NULL,
  cantidadmaxima integer NOT NULL,
  precioventa numeric(10,2) NOT NULL,
  idsucursal integer NOT NULL,
  CONSTRAINT inventario_pkey PRIMARY KEY (idsucursal, idproducto),
  CONSTRAINT inventario_idproducto_fkey FOREIGN KEY (idproducto)
      REFERENCES producto (idproducto) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE NO ACTION,
  CONSTRAINT inventario_idsucursal_fkey FOREIGN KEY (idsucursal)
      REFERENCES sucursal (idsucursal) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE CASCADE
);


  
  
  

CREATE TABLE pago_proveedor
(
  idcuentaxpagar serial NOT NULL,
  idfactura serial NOT NULL,
  idproveedor serial NOT NULL,
  abono numeric(10,2),
  monto numeric(10,2),
  fechaxabono date,
  fechaproxpago date,
  CONSTRAINT pago_proveedor_pkey PRIMARY KEY (idfactura, idcuentaxpagar),
  CONSTRAINT pago_proveedor_idcuentaxpagar_fkey FOREIGN KEY (idcuentaxpagar)
      REFERENCES cuentaxpagar (idcuentaxpagar) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE CASCADE
);




CREATE TABLE pedidos
(
  idproducto character varying(40) NOT NULL,
  cantidad integer NOT NULL,
  CONSTRAINT pedidos_pkey PRIMARY KEY (idproducto),
  CONSTRAINT pedidos_idproducto_fkey FOREIGN KEY (idproducto)
      REFERENCES producto (idproducto) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE NO ACTION
);



CREATE TABLE telefonos
(
  cedula character varying(40) NOT NULL,
  numerotel character varying(100) NOT NULL,
  tipo character varying(30) NOT NULL,
  CONSTRAINT telefonos_pkey PRIMARY KEY (cedula, numerotel),
  CONSTRAINT telefonos_cedula_fkey FOREIGN KEY (cedula)
      REFERENCES persona (cedula) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE NO ACTION
);



CREATE TABLE telefono_sucursal
(
  idsucursal serial NOT NULL,
  telefono character varying(50) NOT NULL,
  CONSTRAINT telefono_sucursal_pkey PRIMARY KEY (idsucursal, telefono),
  CONSTRAINT telefono_sucursal_idsucursal_fkey FOREIGN KEY (idsucursal)
      REFERENCES sucursal (idsucursal) MATCH SIMPLE
      ON UPDATE CASCADE ON DELETE CASCADE
);














