@echo off
DEL /F /S /Q /A C:\Documents and Settings\kyu\Desktop\ExamenAutomatas\Yylex.java Yylex.class ParserVal.java ParserVal.class Parser.java Parser.java

set path=%path%;C:\Program Files\Java\jdk1.7.0_51\bin
jFlex.jar  examen.flex
yacc -J examen.y
javac *.java
java Parser prueba-1.dat 
pause