/*FUNCIONES*/
ALTER SESSION SET CURRENT_SCHEMA=TC_CAPACITACION_JUAN;

/*-----------------------------------------------------*/
DROP FUNCTION FC_TC_AUTORES_DEL_LIBRO;
DROP FUNCTION FC_TC_AUTORES_NO_ASIGNADOS_LIB;

/*-----------------------------------------------------*/
CREATE OR REPLACE FUNCTION FC_TC_AUTORES_DEL_LIBRO
(
    /*SE CREO UN TIPO DE DATO APARTE PARA ISBN POR FACILIDAD DE CAMBIOS*/
    PVC_ISBN TCM_LIBRO.ISBN%TYPE
)
RETURN CTP_TC_DATOS_AUTOR PIPELINED IS
/*
    Autor: roberto go
    Fecha: 04/04/2017
    Desc: retorna lista de autores de un libro segun el parametro
*/
/*
	NVL() ORACLE FUNC, IF NULL REPLACE WITH STRING
*/
CURSOR vlo_DsDatos(CPVC_ISBN VARCHAR2) IS
    SELECT 
        A.ID_PERSONAL,
        B.NOMBRE || ' ' || B.PRIMER_APELLIDO || ' ' || NVL(B.SEGUNDO_APELLIDO, '') AS "NOMBRE_COMPLETO"
    FROM TCM_AUTOR_POR_LIBRO A
    INNER JOIN TCM_AUTOR B ON B.ID_PERSONAL=A.ID_PERSONAL
    WHERE A.ISBN=CPVC_ISBN;

vlo_Registro vlo_DsDatos%ROWTYPE;

BEGIN
    FOR vlo_Registro IN vlo_DsDatos(PVC_ISBN)
    LOOP
        PIPE ROW /*COMO NEW AL TP*/
        ( TP_TC_DATOS_AUTOR(
            vlo_Registro.ID_PERSONAL,
            vlo_Registro.NOMBRE_COMPLETO
        ));
    END LOOP;

    RETURN;
END;

/*-----------------CUALES AUTORES NO ASIGNADOS A UN ISBN---------------------------------*/

CREATE OR REPLACE FUNCTION FC_TC_AUTORES_NO_ASIGNADOS_LIB
(
    /*SE CREO UN TIPO DE DATO APARTE PARA ISBN POR FACILIDAD DE CAMBIOS*/
    PVC_ISBN TCM_LIBRO.ISBN%TYPE
)
RETURN CTP_TC_DATOS_AUTOR PIPELINED IS
/*
    Autor: roberto go
    Fecha: 04/04/2017
    Desc: retorna lista de autores NO ASOCIADOS un libro segun el parametro ISBN
*/
/*NVL() ORACLE FUNC*/
CURSOR vlo_DsDatos(CPVC_ISBN VARCHAR2) IS
    SELECT 
        A.ID_PERSONAL,
        A.NOMBRE || ' ' || A.PRIMER_APELLIDO || ' ' || NVL(A.SEGUNDO_APELLIDO, '') AS "NOMBRE_COMPLETO"
    FROM TCM_AUTOR A
    WHERE A.ID_PERSONAL NOT IN (SELECT B.ID_PERSONAL FROM TCM_AUTOR_POR_LIBRO B WHERE B.ISBN = CPVC_ISBN );

vlo_Registro vlo_DsDatos%ROWTYPE;

BEGIN
    FOR vlo_Registro IN vlo_DsDatos(PVC_ISBN)
    LOOP
        PIPE ROW /*COMO NEW AL TP*/
        ( TP_TC_DATOS_AUTOR(
            vlo_Registro.ID_PERSONAL,
            vlo_Registro.NOMBRE_COMPLETO
        ));
    END LOOP;

    RETURN;
END;

/*-----------------------------------------------------*/