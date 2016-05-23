var cambiarFechaDeEntrada= function(){
    fechaEntrada.style.display = "block";
}, cambiarFechaDeSalida = function () {
    fechaSalida.style.display = "block";
},
iniciar = function () {
    var fechaEntrada=document.getElementById("fechaDeEntradaCalendario");
    fechaEntrada.style.display = "none";
    var fechaSalida = document.getElementById("fechaDeSalidaCalendario");
    fechaSalida.style.display = "none";
}
;
iniciar();
$("#fechaDeEntrada").onclick(cambiarFechaDeEntrada);
$("#fechaDeSalida").onclick(cambiarFechaDeSalida);
