echo off
echo Instalando paquetes esenciales para utilizar el sistema...
call npm i
echo - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
echo Paquetes esenciales instalados exitosamente!
echo Levantando el servidor...
call npm run serve
