del testing\*.java
CALL tools\antlr4.bat Compiler/Adamant.g4 -o testing -Dlanguage=Java
javac testing\*.java