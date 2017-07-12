DROP TYPE CTP_MAT_SIMINISTROS;
DROP TYPE TP_MAT_SIMINISTROS;


CREATE OR REPLACE TYPE TP_MAT_SIMINISTROS AS OBJECT
(
    /*
    Autor:      Mauricio Salas Chaves
    Fecha:        07/09/2016
    Descripcion: Tipo para retornar todos los materiales agrupados partiendo de un criterio sobre la tabla OTT_DETALLE_MATERIAL

    */
    
    ID_MATERIAL    NUMBER(10),
    NOMBRE_MATERIAL                      VARCHAR2(250),
    CANTIDAD_SOLICITADA                   NUMBER(6,2)

    
);
/
--Colección para retornar los registros 
CREATE OR REPLACE TYPE CTP_MAT_SIMINISTROS  AS TABLE OF TP_MAT_SIMINISTROS;