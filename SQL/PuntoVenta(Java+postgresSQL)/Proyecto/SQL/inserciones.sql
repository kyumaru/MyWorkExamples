

--NOTA: ejecutar este de primero, luego de crear las tablas para que pueda ingresar como usuario administrador!!!
insert into Persona values ('111610809');
insert into PersonaFisica values ('111610809','Esteban','Noguera','Penaranda','noguera.esteban@gmail.com');
insert into Empleado values ('3333','111610809','Esteban','A','07-06-2014','4321','1000000'); 
insert into telefonos values('111610809', '38273615','CELULAR');
insert into telefonos values('111610809', '38432567','CASA');
insert into Direcciones values('111610809','San Jose','San Jose','Central','del arbol de mango 200mts del perro echado');

--informacion Cliente Contado, crea el usuario cliente contado, este no es cliente frecuente.
insert into Persona values('-2');
insert into PersonaFisica values ('-2','Cliente Contado');	
insert into Cliente values ('0','-2','F');	

--insertar proveedores
insert into Persona values ('11239');
insert into PersonaJuridica values ('11239','DEMASA.SA');
insert into Proveedor values (DEFAULT,'11239','DEMASA.SA','TRUE');	
	
insert into Persona values ('3791');
insert into PersonaJuridica values ('3791','LA LECHERA');
insert into Proveedor values (DEFAULT,'3791','LA LECHERA','TRUE');
	
	
insert into Persona values ('2899');
insert into PersonaJuridica values ('2899','SU CANASTA.SA');
insert into Proveedor values (DEFAULT,'2899','SU CANASTA.SA','TRUE');

insert into Persona values ('3710');
insert into PersonaJuridica values ('3710','CADENA.SA');
insert into Proveedor values (DEFAULT,'3710','CADENA.SA','TRUE');

insert into Persona values ('1292');
insert into PersonaJuridica values ('1292','SABROPAN');
insert into Proveedor values (DEFAULT,'1292','SABROPAN','TRUE');
	
insert into Persona values ('4555');                     	
insert into PersonaJuridica values ('4555','ARSO.SA');
insert into Proveedor values (DEFAULT,'4555','ARSO.SA','TRUE');

insert into Persona values ('3227');                     	
insert into PersonaJuridica values ('3227','LIMPIPRO.SA');
insert into Proveedor values (DEFAULT,'3227','LIMPIPRO.SA','TRUE');

insert into Persona values ('9300');                     	
insert into PersonaJuridica values ('9300','ENCASA.SA');
insert into Proveedor values (DEFAULT,'9300','ENCASA.SA','TRUE');
	
insert into Persona values ('5123');                     	
insert into PersonaJuridica values ('5123','TRIKO.SA');
insert into Proveedor values (DEFAULT,'5123','TRIKO.SA','TRUE');

	
--insertar productos

select insertarProductoNuevo('423','LECHE LITRO',500,11,true,13,1,15,50,50,'U');
select insertarProductoNuevo('411','FRIJOLES NEGROS',725,10,true,13,1,5,15,15,'U');
select insertarProductoNuevo('283','ARROZ 91% ENTERO',815,12,true,13,1,5,15,15,'U');
select insertarProductoNuevo('139','ATUN LOMO ENTERO',1200,15,false,13,1,5,10,10,'U');
select insertarProductoNuevo('28','SPAGUETTI 125g',400,16,true,13,1,5,15,15,'U');
select insertarProductoNuevo('377','PAN CUADRADO GRDE',1100,17,false,13,1,5,10,10,'U');
select insertarProductoNuevo('728','NATILLA 110g',600,18,false,13,1,5,10,10,'U');
select insertarProductoNuevo('902','MANTEQUILLA PEQ',495,13,false,13,1,5,15,15,'U');
select insertarProductoNuevo('578','QUESO CREMA GRDE',1300,13,false,13,1,5,15,15,'U');
select insertarProductoNuevo('14','GALLETAS',125,17,false,13,1,5,10,10,'U');
select insertarProductoNuevo('7','CIGARROS 1',1300,17,false,13,1,5,15,15,'U');
select insertarProductoNuevo('8','CIGARROS 2',1200,17,false,13,1,5,15,15,'U');
select insertarProductoNuevo('9','CIGARROS 3',1000,17,false,13,1,5,15,15,'U');
select insertarProductoNuevo('310','PASTA DIENTES GRDE',915,17,false,13,1,5,15,15,'U');
select insertarProductoNuevo('107','REFRESCO ZARZA',400,14,false,13,1,5,10,10,'U');
select insertarProductoNuevo('108','REFRESCO COLA',400,14,false,13,1,5,15,15,'U');
select insertarProductoNuevo('109','REFRESCO TE',300,14,false,13,1,5,15,15,'U');
select insertarProductoNuevo('197','JABON BANO',605,18,true,13,1,5,15,15,'U');
select insertarProductoNuevo('26','PAPEL HIG',400,18,true,13,1,5,10,10,'U');
select insertarProductoNuevo('20','DETERGENTE 400g',1200,18,false,13,1,5,15,15,'U');
select insertarProductoNuevo('123','AZUCAR KILO',1150,14,true,13,1,5,15,15,'U');
select insertarProductoNuevo('36','CHOCOLATE BARRITA',120,15,false,13,1,5,15,15,'U');
select insertarProductoNuevo('421','SALSA TOMATE LATA',415,16,false,13,1,5,10,10,'U');
select insertarProductoNuevo('839','DESINFECTANTE',300,10,false,13,1,5,15,15,'U');
