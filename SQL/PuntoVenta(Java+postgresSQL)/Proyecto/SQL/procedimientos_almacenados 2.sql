CREATE OR REPLACE FUNCTION iniciarcaja(idsucursalp integer, nombrecajap character varying, passadminp integer, montoi numeric, idcajero integer)
  RETURNS integer AS
$BODY$
declare idCajaP integer;
        pass   integer;
        idSuc  integer;
        tipoEmp varchar(1);
        ret     integer;
        idEmp   integer;
        estadoC varchar;

        fechaI timestamp;
        idEmpI integer;
        idCaj  integer;
        
BEGIN

 ret = -1;
 estadoC = null;
 fechaI = null;
 idCajaP = -1;
 idCaj = -1;
 
 select idCaja into idCajaP
        from   Caja
        where  idsucursal = idSucursalP
        and    nombre = nombreCajaP;

        select idEmpleado into idCaj
        from   empleado
        where  idempleado = idCajero;
        
 select estado into estadoC
 from cajaDetalle where (select fechainicio from cajadetalle where idcaja = idCajaP) = (
                        select min(fechainicio) from cajadetalle where idcaja = idCajaP) and estado = 'C';

 if estadoC = 'C' or estadoC = null and idCaj <> -1  and idCajaP <> -1 then

        select idEmpleado into idEmp
        from empleado where contrasenia = passAdminP and tipoempleado = 'A' and idsucursal = idSucursalP;

 update cajaDetalle set montoinicial = montoI, idEmpleadoAdInicia = idEmp, estado = 'A', montoFinal = 0, idCajero = idCaj where idcaja = idCajaP;

        ret = 1;
 
        end if;

 return ret;               
END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
  
  CREATE OR REPLACE FUNCTION cerrarcaja(idsucursalp integer, nombrecajap character varying, passadminp integer, montof numeric)
   RETURNS numeric AS
$BODY$
declare idCajaP integer;
        pass   integer;
        idSuc  integer;
        tipoEmp varchar(1);
        ret numeric;
        idEmp   integer;
        estadoC varchar;
        montoI numeric;
        fechaI timestamp;
        idEmpI integer;
        t record;
        total numeric;
        idCaj integer;
    
BEGIN

 ret = -1;
 estadoC = '';
 idCaj = -1;
 
 select idCaja into idCajaP
        from   Caja
        where  idsucursal = idSucursalP
        and    nombre = nombreCajaP;
        
 select estado into estadoC
 from cajaDetalle where (select fechainicio from cajadetalle where idcaja = idCajaP and estado = 'A') = (
                        select min(fechainicio) from cajadetalle where idcaja = idCajaP and estado = 'A')  and estado = 'A';


 if estadoC = 'A' then

 select fechainicio into fechaI
 from cajaDetalle where (select fechainicio from cajadetalle where idcaja = idCajaP and estado = 'A') = (
                        select min(fechainicio) from cajadetalle where idcaja = idCajaP and estado = 'A')  and estado = 'A';


        select idEmpleado into idEmp
        from empleado e, cajaDetalle cd where contrasenia = passAdminP and tipoempleado = 'A' and idsucursal = idSucursalP;

      select montoinicial into montoI
 from cajaDetalle where (select fechainicio from cajadetalle where idcaja = idCajaP and estado = 'A') = (
                        select min(fechainicio) from cajadetalle where idcaja = idCajaP and estado = 'A')  and estado = 'A';


 --select idEmpleadoAdInicia into idEmpI
 --from cajaDetalle where (select fechainicio from cajadetalle where idcaja = idCajaP and estado = 'A') = (
  --                      select min(fechainicio) from cajadetalle where idcaja = idCajaP and estado = 'A')  and estado = 'A';

 
        end if;

 if estadoC = 'A' then
 
    select sum( monto ) into total
           from   facturacliente 
           where  fecha = current_date
           and    idCaja = idCajaP;

    ret = ( montof - ( montoI + total ) );
 
    update cajaDetalle set montofinal = ret, idEmpleadoAdCierra = idEmp, estado = 'C', fechafinal = now(), idCajero = -1 where fechainicio = fechaI;

 end if;

 return ret;  
END;
$BODY$
  LANGUAGE plpgsql;

  
  
  
  CREATE OR REPLACE FUNCTION actualizarinventario(idproductop character varying, cantidad integer)
  RETURNS void AS
$BODY$
BEGIN
 
    update inventario set cantidadactual = (cantidadactual - cantidad)
    where idProducto = idProductoP;
	
END;
$BODY$
  LANGUAGE plpgsql;
  
  
  -- Function: actualizarproducto(character varying, character varying, integer, numeric, numeric, integer, integer, integer, boolean)

-- DROP FUNCTION actualizarproducto(character varying, character varying, integer, numeric, numeric, integer, integer, integer, boolean);

CREATE OR REPLACE FUNCTION actualizarproducto(pidproducto character varying, pnombre character varying, pidproveedor integer, ppreciocosto numeric, pporcutil numeric, pcantidadactual integer, pcantidadminima integer, pcantidadmaxima integer, pesexento boolean)
  RETURNS void AS
$BODY$
BEGIN

 update producto set nombre = pnombre , idproveedor = pidproveedor , preciocosto = ppreciocosto , porcentajeutilidad = pporcutil , esexento = pesexento where idproducto = pidproducto;
 update  inventario set cantidadminima = pcantidadminima ,cantidadactual = pcantidadactual ,cantidadmaxima = pcantidadmaxima where idproducto = pidproducto;

END;
$BODY$
  LANGUAGE plpgsql;
  
  
 -- Function: actualizarproveedor(character varying, character varying)

-- DROP FUNCTION actualizarproveedor(character varying, character varying);

CREATE OR REPLACE FUNCTION actualizarproveedor(pcedula character varying, prazonsocial character varying)
  RETURNS void AS
$BODY$
BEGIN
	update PersonaJuridica set  razonsocial = prazonsocial where Cedula = pcedula;
	update Proveedor set razonsocial = prazonsocial where cedula = pcedula;
END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
-- Function: actualizartelefonos(character varying, character varying, character varying)

-- DROP FUNCTION actualizartelefonos(character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION actualizartelefonos(pcedula character varying, pnumerotel character varying, ptipo character varying)
  RETURNS void AS
$BODY$ 
BEGIN 
	update Telefonos set numerotel = pnumerotel where cedula = pcedula and tipo = ptipo;
END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
CREATE OR REPLACE FUNCTION borrarcliente(pcedula character varying, tipo character varying)
  RETURNS void AS
$BODY$
BEGIN
  if tipo = 'J' then
    delete from direcciones where Cedula = pcedula;
    delete from telefonos where Cedula = pcedula;
    delete from cliente   where Cedula = pcedula; 
    delete from personaJuridica where Cedula = pcedula;
    delete from persona where Cedula = pcedula;
  elsif tipo = 'F' then 
    delete from direcciones where Cedula = pcedula;
    delete from telefonos where Cedula = pcedula;
    delete from cliente   where Cedula = pcedula; 
    delete from personaFisica where Cedula = pcedula;
    delete from persona where Cedula = pcedula;
  end if;
END;
$BODY$
  LANGUAGE plpgsql;
  
  

 CREATE OR REPLACE FUNCTION borrarempleado(pcedula character varying)
  RETURNS void AS
$BODY$
BEGIN

delete from direcciones where Cedula = pcedula;
delete from telefonos where Cedula = pcedula;
delete from empleado where Cedula = pcedula;
delete from personaFisica where Cedula = pcedula;
delete from persona where Cedula = pcedula;

END;
$BODY$
  LANGUAGE plpgsql;
  

-- Function: borrarproducto(character varying)

-- DROP FUNCTION borrarproducto(character varying);

CREATE OR REPLACE FUNCTION borrarproducto(idproducto1 character varying)
  RETURNS void AS
$BODY$
BEGIN

delete from facturaDetalle fa where  fa.idProducto = idProducto1;
delete from inventario i where  i.idProducto = idProducto1;
delete from pedidos pe where  pe.idProducto = idProducto1;
delete from producto p where p.idProducto = idProducto1;


END;
$BODY$
  LANGUAGE plpgsql;
  
  
-- Function: actualizarclientef(character varying, character varying, character varying, character varying, character varying)

-- DROP FUNCTION actualizarclientef(character varying, character varying, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION actualizarclientef(pcedula character varying, pnombre1 character varying, papellido1 character varying, papellido2 character varying, pemail character varying)
  RETURNS void AS
$BODY$
BEGIN
	update PersonaFisica set nombre1 = pnombre1, apellido1 = papellido1,apellido2 = papellido2, email = pemail where cedula = pcedula;
END;
$BODY$
  LANGUAGE plpgsql;
  
  
 

-- Function: actualizarclientej(character varying, character varying)

-- DROP FUNCTION actualizarclientej(character varying, character varying);

CREATE OR REPLACE FUNCTION actualizarclientej(pcedula character varying, prazonsocial character varying)
  RETURNS void AS
$BODY$
BEGIN
	update PersonaJuridica set razonsocial = prazonsocial where Cedula = pcedula;
END;
$BODY$
  LANGUAGE plpgsql;


-- Function: actualizardirecciones(character varying, character varying, character varying, character varying, character varying)

-- DROP FUNCTION actualizardirecciones(character varying, character varying, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION actualizardirecciones(pcedula character varying, pprovincia character varying, pcanton character varying, pdistrito character varying, psenias character varying)
  RETURNS void AS
$BODY$ 
BEGIN 
	update Direcciones set provincia = pprovincia,canton = pcanton,distrito = pdistrito,senias = psenias where cedula = pcedula;
END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
-- Function: actualizarempleado(character varying, character varying, character varying, character varying, character varying, date, numeric, character varying, integer)

-- DROP FUNCTION actualizarempleado(character varying, character varying, character varying, character varying, character varying, date, numeric, character varying, integer);

CREATE OR REPLACE FUNCTION actualizarempleado(pcedula character varying, pnombre1 character varying, papellido1 character varying, papellido2 character varying, pemail character varying, pfechainicio date, psueldo numeric, ptipoempleado character varying, pcontrasenia integer)
  RETURNS void AS
$BODY$
BEGIN
	update PersonaFisica set nombre1 = pnombre1, apellido1 = papellido1, apellido2 = papellido2, email = pemail where cedula = pcedula;
	update Empleado set nombre1 = pnombre1,tipoempleado = ptipoempleado,sueldo = psueldo where cedula = pcedula; 
END;
$BODY$
  LANGUAGE plpgsql;
  

  
-- Function: actualizarinventario(character varying, integer)

-- DROP FUNCTION actualizarinventario(character varying, integer);

CREATE OR REPLACE FUNCTION actualizarinventario(idproductop character varying, cantidad integer)
  RETURNS void AS
$BODY$
BEGIN
 
    update inventario set cantidadactual = (cantidadactual - cantidad)
    where idProducto = idProductoP;
	
END;
$BODY$
  LANGUAGE plpgsql;

  
  

  
  
  
  
  
-- Function: actualizarproducto(character varying, character varying, integer, numeric, numeric, integer, integer, integer, boolean)

-- DROP FUNCTION actualizarproducto(character varying, character varying, integer, numeric, numeric, integer, integer, integer, boolean);

CREATE OR REPLACE FUNCTION actualizarproducto(pidproducto character varying, pnombre character varying, pidproveedor integer, ppreciocosto numeric, pporcutil numeric, pcantidadactual integer, pcantidadminima integer, pcantidadmaxima integer, pesexento boolean)
  RETURNS void AS
$BODY$
BEGIN

 update producto set nombre = pnombre , idproveedor = pidproveedor , preciocosto = ppreciocosto , porcentajeutilidad = pporcutil , esexento = pesexento where idproducto = pidproducto;
 update  inventario set cantidadminima = pcantidadminima ,cantidadactual = pcantidadactual ,cantidadmaxima = pcantidadmaxima where idproducto = pidproducto;

END;
$BODY$
  LANGUAGE plpgsql;

  
  
  
  
  

  
  
  
  
CREATE OR REPLACE FUNCTION borrarproveedor(pcedula character varying)
  RETURNS void AS
$BODY$
BEGIN
delete from direccion where Cedula = pcedula;
delete from telefonos where Cedula = pcedula;
delete from proveedor where Cedula = pcedula;
delete from personaJuridica where Cedula = pcedula;
delete from persona where Cedula = pcedula;
END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
CREATE OR REPLACE FUNCTION crearcaja(nombrecajap character varying, idsucursalp integer, passadminp integer)
 RETURNS integer AS
$BODY$
declare idCaja integer;
        pass   integer;
        idSuc  integer;
        tipoEmp varchar(1);
        numCajas integer;
        idEmp integer;
        
BEGIN

    pass = -1;
    idSuc = -1;
    tipoEmp = 'n';
    idCaja = -1;
    numCajas = 0;
    
    select contrasenia into pass
    from   empleado 
    where  contrasenia = passAdminP
    and    idsucursal =  idSucursalP;
    
    select idSucursal into idSuc
    from   empleado
    where  contrasenia = passAdminP
    and    idSucursal = idSucursalP;

    select tipoempleado into tipoEmp
    from   empleado
    where  contrasenia = passAdminP
    and    idSucursal = idSucursalP;  

     select count(*) into numCajas from Caja
     where exists(select nombre 
     from Caja where exists(select nombre from Caja 
     where nombre = nombreCajaP and idsucursal = idsucursalP)); 

    select idempleado into idEmp
    from   empleado
    where  contrasenia = passAdminP
    and    idSucursal = idSucursalP; 
        
    
    if pass = passAdminP and pass <> -1 and idSuc <> -1 and idSuc = idSucursalP and tipoEmp = 'A' and numCajas <= 0 then
    insert into   caja(idCaja,idSucursal,nombre) values(DEFAULT,idSucursalP,nombreCajaP);
    select currval('caja_idcaja_seq') into idCaja;
    insert into cajaDetalle(idcajadetalle, idcaja,montoInicial, montoFinal,fechaInicio, fechaFinal, idEmpleadoAdInicia, idEmpleadoAdCierra,estado,idCajero) 
                      values(DEFAULT,idCaja,'0','0',now(),now(),idEmp,idEmp,'C', -1); 
    end if;
    
    return idCaja; 

END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
  
CREATE OR REPLACE FUNCTION crearfacturacliente(fecha date, idcliente integer, monto numeric, passempleado integer)
  RETURNS bigint AS
$BODY$
declare idfac bigint;
        idEmp integer;
 tipoEmp varchar;
 idc integer;
 id_Caja integer;
        
BEGIN
 idEmp = -1;
 idFac = -1;
 tipoEmp = '';
 
 select idEmpleado into idEmp
 from   empleado 
 where  contrasenia = passEmpleado
 and    tipoEmpleado in ('C', 'A'); 



 if idEmp <> -1 then 
 
        select tipoEmpleado into tipoEmp
        from   empleado 
        where  contrasenia = passEmpleado;

  if tipoEmp = 'A' then
  insert into facturaCliente(idFactura,fecha,idcliente, monto, idEmpleado,idCaja) values(DEFAULT,fecha,idCliente,monto,idEmp,0);
  select currval('facturaCliente_idfactura_seq') into idFac;

  end if;

  if tipoEmp = 'C' then-- verifico que el cajero que va a realizar la venta este asignado a la caja 'x'
 
  select idCajero into idc
  from   cajaDetalle 
  where  idCajero = idEmp;

  select idCaja into id_Caja
  from   cajaDetalle 
  where  idCajero = idc;

   if idc = idEmp then

   insert into facturaCliente(idFactura,fecha,idcliente, monto, idEmpleado, idCaja) values(DEFAULT,fecha,idCliente,monto,idEmp,id_Caja);
   select currval('facturaCliente_idfactura_seq') into idFac;

   end if;
 
  end if;
        end if;


 return idFac; 

END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
  
  
CREATE OR REPLACE FUNCTION crearfacturadetalle(idfactura integer, idproducto character varying, precioventaproducto numeric, cantidad integer)
  RETURNS bigint AS
$BODY$

BEGIN
 insert into facturaDetalle(idFactura,idProducto,precioVenta, cantidad,total) 
 values(idFactura,idProducto,precioVentaProducto,cantidad,precioVentaProducto*cantidad);
       return 1; 
END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
  
  
CREATE OR REPLACE FUNCTION decrementarinventario(idproductop character varying, cantidad integer)
  RETURNS void AS
$BODY$
BEGIN

   update inventario set cantidadactual = (cantidadactual - cantidad)
   where idProducto = idProductoP;

END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
-- Function: elimina_cliente(character varying)

-- DROP FUNCTION elimina_cliente(character varying);

CREATE OR REPLACE FUNCTION elimina_cliente(character varying)
  RETURNS void AS
$BODY$
declare 
--vresultado integer;
BEGIN
--elimina en orden de jerarquia referencial
DELETE FROM cliente
WHERE cedula = $1;

DELETE FROM personafisica
WHERE cedula = $1;

DELETE FROM personajuridica
WHERE cedula = $1;

DELETE FROM telefonos
WHERE cedula = $1;

DELETE FROM direcciones
WHERE cedula = $1;

DELETE FROM persona
WHERE cedula = $1;

END
$BODY$
  LANGUAGE plpgsql;

  
  
  
  
-- Function: elimina_x_cedula(character varying)

-- DROP FUNCTION elimina_x_cedula(character varying);

CREATE OR REPLACE FUNCTION elimina_x_cedula(character varying)
  RETURNS void AS
$BODY$
declare 
--vresultado integer;
BEGIN
--elimina en orden de jerarquia referencial
DELETE FROM FACTURACLIENTE
WHERE (select distinct c.cedula from cliente c, facturacliente f where c.idcliente = f.idcliente and c.cedula = $1) = $1;

DELETE FROM cliente
WHERE cedula = $1;

DELETE FROM proveedor
WHERE cedula = $1;

DELETE FROM empleado
WHERE cedula = $1;

DELETE FROM personafisica
WHERE cedula = $1;

DELETE FROM personajuridica
WHERE cedula = $1;

DELETE FROM telefonos
WHERE cedula = $1;

DELETE FROM direcciones
WHERE cedula = $1;

DELETE FROM persona
WHERE cedula = $1;

END
$BODY$
  LANGUAGE plpgsql;

  
  
  
  

 CREATE OR REPLACE FUNCTION eliminarcaja(nombrecajap character varying, idsucursalp integer, passadminp integer)
  RETURNS integer AS
$BODY$
declare retorno integer;
        pass   integer;
        idSuc  integer;
        idC    integer;
        tipoEmp varchar(1);
BEGIN

    pass = -1;
    idSuc = -1;
    tipoEmp = 'n';
    retorno = 0;
    idC = -1;
    
    select contrasenia into pass
    from   empleado 
    where  contrasenia = passAdminP
    and    idsucursal =  idSucursalP;
    
    select idSucursal into idSuc
    from   empleado
    where  contrasenia = passAdminP
    and    idSucursal = idSucursalP;

    select tipoempleado into tipoEmp
    from   empleado
    where  contrasenia = passAdminP
    and    idSucursal = idSucursalP;    

    select idCaja into idC 
    from   Caja
    where  nombre = nombreCajaP 
    and    idsucursal = idSucursalP;  
    
    if pass = passAdminP and pass <> -1 and idSuc <> -1 and idSuc = idSucursalP and tipoEmp = 'A' and idC <> -1 then
    delete from cajadetalle where idcaja = idC; 
    delete from caja where idsucursal = idSucursalP and nombre = nombreCajaP and idcaja = idC;
    retorno = 1;
    end if;
    
    return retorno; 

END;
$BODY$
  LANGUAGE plpgsql;
  
  
  

CREATE OR REPLACE FUNCTION getinformacionproducto(IN idproductop character varying)
  RETURNS TABLE(id character varying, nombre character varying, preciov numeric) AS
$BODY$
BEGIN
 	return query
	select p.idProducto, p.nombre, i.precioVenta 
	from   producto p, inventario i 
	where  i.idProducto = idProductoP 
	and    p.idProducto = idProductoP;
	
END;
$BODY$
  LANGUAGE plpgsql;
  
  

-- Function: insertarclientef(character varying, character varying, character varying, character varying, character varying)

-- DROP FUNCTION insertarclientef(character varying, character varying, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION insertarclientef(pcedula character varying, pnombre1 character varying, papellido1 character varying, papellido2 character varying, pemail character varying)
  RETURNS void AS
$BODY$
BEGIN
	insert into Persona(Cedula) values (pcedula);
	insert into PersonaFisica values(pcedula,pnombre1,papellido1,papellido2,pemail);
	insert into Cliente(Cedula,TipoCliente) values (pcedula,'F');
END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
-- Function: insertarclientej(character varying, character varying)

-- DROP FUNCTION insertarclientej(character varying, character varying);

CREATE OR REPLACE FUNCTION insertarclientej(pcedula character varying, prazonsocial character varying)
  RETURNS void AS
$BODY$
BEGIN
	insert into Persona(Cedula) values (pcedula);
	insert into PersonaJuridica values (pcedula,prazonsocial);
	insert into Cliente(Cedula,TipoCliente) values (pcedula,'J');
END;
$BODY$
  LANGUAGE plpgsql;
  
  

  
-- Function: insertardirecciones(character varying, character varying, character varying, character varying, character varying)

-- DROP FUNCTION insertardirecciones(character varying, character varying, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION insertardirecciones(pcedula character varying, pprovincia character varying, pcanton character varying, pdistrito character varying, psenias character varying)
  RETURNS void AS
$BODY$ 
BEGIN 
	insert into Direcciones values (pcedula,pprovincia,pcanton,pdistrito,psenias);
END;
$BODY$
  LANGUAGE plpgsql;

  
  
-- DROP FUNCTION insertarempleado(character varying, character varying, character varying, character varying, character varying, date, numeric, character varying, integer, integer);

CREATE OR REPLACE FUNCTION insertarempleado(pcedula character varying, pnombre1 character varying, papellido1 character varying, papellido2 character varying, pemail character varying, pfechainicio date, psueldo numeric, ptipoempleado character varying, pcontrasenia integer, pidsucursal integer)
  RETURNS void AS
$BODY$
BEGIN
	insert into Persona(Cedula) values (pcedula);
	insert into PersonaFisica values (pcedula,pnombre1,papellido1,papellido2,pemail);
	insert into Empleado (Cedula,Nombre1,TipoEmpleado,FechaInicio,Contrasenia,Sueldo,idsucursal) values (pcedula,pnombre1,ptipoempleado,pfechainicio,pcontrasenia,psueldo,pidsucursal); 
END;
$BODY$
  LANGUAGE plpgsql;
  

  
CREATE OR REPLACE FUNCTION insertarproducto(pnombre character varying, pcantidad integer, ppreciocosto numeric, pdistrito character varying, psenias character varying)
  RETURNS void AS
$BODY$ 
BEGIN 
	insert into Direcciones values (pcedula,pprovincia,pcanton,pdistrito,psenias);
END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
CREATE OR REPLACE FUNCTION insertarproductonuevo(idproducto character varying, pnombre character varying, ppreciocosto numeric, idproveedor integer, esexento boolean, porcentajeutil numeric, pidsucursal integer, cantidadmin integer, cantidadact integer, cantidadmax integer, tipoventa character varying)
  RETURNS void AS
$BODY$ 
declare precioVenta numeric(10,3);
BEGIN 

	precioVenta := pPrecioCosto + (pPrecioCosto *(porcentajeUtil/100)); 
	insert into producto values (idProducto,pNombre,pPrecioCosto,idProveedor,esExento,porcentajeUtil,tipoVenta);
	insert into inventario values (idProducto,cantidadMin,cantidadAct, cantidadMax,precioVenta,pIdSucursal);
END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
CREATE OR REPLACE FUNCTION insertarproveedor(pcedula character varying, prazonsocial character varying)
  RETURNS void AS
$BODY$
BEGIN
	
	insert into Proveedor(Cedula,Razonsocial,"estaActivo") values (pcedula,prazonsocial,'TRUE');
END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
  
CREATE OR REPLACE FUNCTION insertartelefonos(pcedula character varying, pnumerotel character varying, ptipo character varying)
  RETURNS void AS
$BODY$ 
BEGIN 
	insert into Telefonos values (pcedula,pnumerotel,ptipo);
END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
  
CREATE OR REPLACE FUNCTION precioventaproducto(idproductop character varying)
  RETURNS numeric AS
$BODY$
declare precio numeric(10,3); 
BEGIN
	select precioVenta into precio 
	from inventario  
	where  idproducto = idProductoP;

return precio;
END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
  
  
  
  
CREATE OR REPLACE FUNCTION proveedor(pcedula character varying)
  RETURNS void AS
$BODY$
BEGIN
delete from proveedor where Cedula = pcedula;
END;
$BODY$
  LANGUAGE plpgsql;
  
  

  
CREATE OR REPLACE FUNCTION settotalfacturac(idfact integer, totalfac numeric)
  RETURNS void AS
$BODY$
BEGIN
	
	update facturaCliente set monto = totalFac
    where idfactura = idFact;
	
END;
$BODY$
  LANGUAGE plpgsql;
  
  
  
  
  
  
CREATE OR REPLACE FUNCTION verificausuario(enombre1 character varying, eapellido1 character varying, eapellido2 character varying, econtrasenia integer)
  RETURNS character varying AS
$BODY$
declare 
resultado varchar;
cantidadFilas integer;
nombre varchar;
tipoEmp varchar;
BEGIN
cantidadFilas = 0;
tipoEmp = 'N';
resultado = tipoEmp;

select count(*) into cantidadFilas
from   personaFisica p, empleado e
where  p.nombre1 = eNombre1
and    p.apellido1 = eApellido1
and    p.apellido2 = eApellido2
and    e.contrasenia = eContrasenia
and e.cedula = p.cedula
group by p.nombre1, p.apellido1, p.apellido2, e.nombre1, e.tipoempleado,e.contrasenia; 

if cantidadFilas = 1 then
 select e.tipoempleado into tipoEmp
 from   empleado e
 where  e.contrasenia = eContrasenia
 and nombre1 = enombre1;
end if;

  if cantidadFilas = 1 and tipoEmp = 'A' then 
    resultado = 'A';
  elsif cantidadFilas = 1 and tipoEmp = 'B' then
    resultado = 'B';
  elsif cantidadFilas = 1 and tipoEmp = 'C' then 
    resultado = 'C';    
  end if;    
  
return resultado;  
END;
$BODY$
  LANGUAGE plpgsql;



  
  
  
  
  
-- Function: actualiza_inventario()

-- DROP FUNCTION actualiza_inventario();

CREATE OR REPLACE FUNCTION actualiza_inventario()
  RETURNS trigger AS
$BODY$
 declare vOperacion  char(1);
BEGIN
    IF (TG_OP = 'INSERT') THEN
        voperacion := 'I';
    ELSIF (TG_OP = 'UPDATE') THEN
       voperacion := 'U';

    END IF;   
     IF NEW.cantidadActual < OLD.cantidadActual and NEW.cantidadActual <= NEW.cantidadMinima and vOperacion = 'U' THEN

   IF exists (select idproducto from pedidos where idproducto = NEW.idProducto) THEN
    update pedidos p set cantidad = NEW.cantidadMaxima - New.cantidadActual where p.idProducto = NEW.idProducto;
   ELSE
    insert into pedidos (idProducto, cantidad) values(NEW.idProducto, NEW.cantidadMaxima - New.cantidadActual);
   End if;
           END IF;
    
    IF NEW.cantidadActual > OLD.cantidadActual and vOperacion = 'U' THEN
   IF exists (select idproducto from pedidos where idproducto = NEW.idProducto) THEN
    update pedidos p set cantidad = NEW.cantidadMaxima - New.cantidadActual where p.idProducto = NEW.idProducto;
   ELSE
    insert into pedidos (idProducto, cantidad) values(NEW.idProducto, NEW.cantidadMaxima - New.cantidadActual);
   End if;
           END IF;    
    
   IF NEW.cantidadActual = NEW.cantidadMaxima and vOperacion = 'U' THEN
  delete from pedidos p where p.idProducto = NEW.idProducto ;
    
   END IF;
    

           RETURN NEW;
END;
$BODY$
  LANGUAGE plpgsql;

  
  
  
  
-- Trigger: actinv on inventario

-- DROP TRIGGER actinv ON inventario;

CREATE TRIGGER actinv
  BEFORE UPDATE
  ON inventario
  FOR EACH ROW
  EXECUTE PROCEDURE actualiza_inventario();
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
  
-----Usuario para loguearse.

--NOTA: ejecutar este de primero, luego de crear las tablas para que pueda ingresar como usuario administrador!!!
insert into Persona values ('111610809');
insert into PersonaFisica values ('111610809','Esteban','Noguera','Penaranda','noguera.esteban@gmail.com');
insert into Empleado values ('3333','111610809','Esteban','A','07-06-2014','4321','1000000'); 
insert into telefonos values('111610809', '38273615','CELULAR');
insert into telefonos values('111610809', '38432567','CASA');
insert into Direcciones values('111610809','San Jose','San Jose','Central','del arbol de mango 200mts del perro echado');
