function showModalForm() {
  let modalForm = new bootstrap.Modal(document.getElementById("modalForm"));
  modalForm.toggle();
}

let labelNombre = document.getElementById(" < %= TxtNombre % > ");
let errMsgNombre = document.getElementById("nombreErrorMessage");

let labelStock = document.getElementById(" < %= TxtStock % > ");
let errMsgStock = document.getElementById("stockErrorMessage");

let labelPrecio = document.getElementById( " < %= TxtPrecio % > ");
let errMsgPrecio = document.getElementById("precioErrorMessage");

let labelPuntos = document.getElementById(" < %= TxtPuntos % > ");
let errMsgPuntos = document.getElementById("puntosErrorMessage");

let labelCapacidad = document.getElementById(" < %= TxtCapacidad % > ");
let errMsgCapacidad = document.getElementById("capacidadErrorMessage");

function validatePoints() {
  if (labelPuntos.value === "") {
    labelPuntos.classList.add("is-invalid");
    errMsgPuntos.style.display = "block";
    return false;
  }
  if (isNaN(labelPuntos.value)) {
    labelPuntos.classList.add("is-invalid");
    errMsgPuntos.style.display = "block";
    return false;
  }
  if (labelPuntos.value < 0) {
    labelPuntos.classList.add("is-invalid");
    errMsgPuntos.style.display = "block";
    return false;
  }

  labelPuntos.classList.remove("is-invalid");
  errMsgPuntos.style.display = "none";
  return true;
}
function validateCapacidad() {
  if (labelCapacidad.value === "") {
    labelCapacidad.classList.add("is-invalid");
    errMsgCapacidad.style.display = "block";
    return false;
  }
  if (isNaN(labelCapacidad.value)) {
    labelCapacidad.classList.add("is-invalid");
    errMsgCapacidad.style.display = "block";
    return false;
  }
  if (labelCapacidad.value <= 0) {
    labelCapacidad.classList.add("is-invalid");
    errMsgCapacidad.style.display = "block";
    return false;
  }

  labelCapacidad.classList.remove("is-invalid");
  errMsgCapacidad.style.display = "none";
  return true;
}

function validatePrecio() {
  if (labelPrecio.value === "") {
    labelPrecio.classList.add("is-invalid");
    errMsgPrecio.style.display = "block";
    return false;
  }
  if (isNaN(labelPrecio.value)) {
    labelPrecio.classList.add("is-invalid");
    errMsgPrecio.style.display = "block";
    return false;
  }
  if (labelPrecio.value <= 0) {
    labelPrecio.classList.add("is-invalid");
    errMsgPrecio.style.display = "block";
    return false;
  }

  labelPrecio.classList.remove("is-invalid");
  errMsgPrecio.style.display = "none";
  return true;
}

function validateNombre() {
  if (labelNombre.value === "") {
    labelNombre.classList.add("is-invalid");
    errMsgNombre.style.display = "block";
    return false;
  }

  labelNombre.classList.remove("is-invalid");
  errMsgNombre.style.display = "none";
  return true;
}
function validateStock() {
  if (labelStock.value === "") {
    labelStock.classList.add("is-invalid");
    errMsgStock.style.display = "block";
    return false;
  }
  if (isNaN(labelStock.value)) {
    labelStock.classList.add("is-invalid");
    errMsgStock.style.display = "block";
    return false;
  }
  if (labelStock.value <= 0) {
    labelStock.classList.add("is-invalid");
    errMsgStock.style.display = "block";
    return false;
  }

  labelStock.classList.remove("is-invalid");
  errMsgStock.style.display = "none";
  return true;
}

function validarFormulario() {
  // Llama a todas las funciones de validación individual

  let val_puntos = validatePoints();
  let val_capacidad = validateCapacidad();
  let val_precio = validatePrecio();
  let val_nombre = validateNombre();
  let val_stock = validateStock();

  if (val_puntos && val_capacidad && val_precio && val_nombre && val_stock) {
    return true;
  }

  return false;
}
$(document).ready(function () {
  // get elements by id again
    labelNombre = document.getElementById("ContentPlaceHolder1_" + "TxtNombre");
    errMsgNombre = document.getElementById("nombreErrorMessage");

    labelStock = document.getElementById("ContentPlaceHolder1_" + "TxtStock");
    errMsgStock = document.getElementById("stockErrorMessage");

    labelPrecio = document.getElementById("ContentPlaceHolder1_" + "TxtPrecio");
    errMsgPrecio = document.getElementById("precioErrorMessage");

    labelPuntos = document.getElementById("ContentPlaceHolder1_" + "TxtPuntos");
    errMsgPuntos = document.getElementById("puntosErrorMessage");

    labelCapacidad = document.getElementById("ContentPlaceHolder1_" + "TxtCapacidad");
    errMsgCapacidad = document.getElementById("capacidadErrorMessage");
});
