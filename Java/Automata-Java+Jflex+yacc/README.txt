Este proyecto permite ver el automata del problema 6 para el examen final 
ESPAÑOL->AUTOMATA

La salida es una tabla de transicion con los estados del automata en filas y los simbolos
de entrada como columnas.

Las deficiones que describen al automata se escriben en el archivo --entrada.dat--
linea por linea. Es importante el orden en que se ingresan las clausulas anteriores, el
formato general es:

/////////////////////////////////////////////////
simbolos	[hay un simbolo a] 
.
.
estados	[hay un estado 0] 
.
. 
transiciones entre estados por medio de un simbolos
.
.
estados finales
/////////////////////////////////////////////////


Los simbolos y los estados deben especificarse al inicio en ese orden, las tranciones y
estados finales son opcionales y pueden alternarse (aunque recuerde que todo automata
debe tener al menos 1 estado final).

Los simbolos pueden ser un character cualquiera del rango a-z.

Los numero de estado deben ser un entero i y deben ser una progesion comenzando en 0,
estado 0 el 1er estado, 1 el 2do, etc y en la tabla se ve como qi.  

NO se puede repetir un numero de estado o simbolo.

Puede consultar los archivos prueba-1 y prueba-2 como ejemplos.

Para desplegar el automata utilize el shell de su sistema operativo y cambie su directorio
actual a la carpeta donde tenga el proyecto y ejecute: 
			
						"!#>> java Parser entrada.dat

Puede sustituir entrada.dat por sus propios archivos .dat o los adjuntos de prueba.

El estado inicial siempre se muestra en la segunda celda de la primera columna de la tabla
y corresponde al primer estado establecido.

Problemas encontrados en la implementación: crear una matriz 2d con los arraylist de java
tiene su complejidad, la unica forma de que se tenga un array de cierto tamaño fijo es 
utilizando collections, size != capacidad y el constructor solo especifica capacidad.  

Funcionalidades no implementadas o defectuosas:  n/a, la tabla no se ve tan bonita.
				

											by light wizard kyumaru