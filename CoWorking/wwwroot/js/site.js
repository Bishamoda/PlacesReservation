
var today = new Date();
var dd = today.getDate();
var mm = today.getMonth() + 1;
var yyyy = today.getFullYear();

if (dd < 10) {
    dd = '0' + dd;
}

if (mm < 10) {
    mm = '0' + mm;
}

today = yyyy + '-' + mm + '-' + dd;

document.getElementById("MinDate").setAttribute("min", today);

document.getElementById("MaxDate").setAttribute("min", today);


var today = new Date();
var dd = today.getDate();
var mm = today.getMonth() + 2; //January is 0!
var yyyy = today.getFullYear();

if (dd < 10) {
    dd = '0' + dd;
}

if (mm < 10) {
    mm = '0' + mm;
}

today = yyyy + '-' + mm + '-' + dd;
document.getElementById("MinDate").setAttribute("max", today);
document.getElementById("MaxDate").setAttribute("max", today);