﻿let labelFechaComprobante, errMsgFechaComprobante;
let labelFechaOrden;
let labelIdOrden, errMsgOrdenAsociada;
labelFechaComprobante = document.getElementById("ContentPlaceHolder1_" + "TxtFechaComprobante");
errMsgFechaComprobante = document.getElementById("fechaComprobanteErrorMessage");
labelFechaOrden = document.getElementById("ContentPlaceHolder1_" + "TxtFechaOrden");
//errMsgFechaOrden = document.getElementById("fechaErrorMessage");
labelIdOrden = document.getElementById("ContentPlaceHolder1_" + "TxtIdOrden");
errMsgOrdenAsociada = document.getElementById("ordenAsociadaErrorMessage");


function HolaMundo() {
    alert('Hola Mundo');
}

function showModalDetallesORV() {
    var modalORV = new bootstrap.Modal(document.getElementById('modalDetallesORV'));
    modalORV.toggle();
}

function showModalDetallesORC() {
    var modalORC = new bootstrap.Modal(document.getElementById('modalDetallesORC'));
    modalORC.toggle();
}

function showModalDetallesCOM() {
    var modalCOM = new bootstrap.Modal(document.getElementById('modalDetallesCOM'));
    modalCOM.toggle();
}

function showModalFormProducto() {
    var modalFormProducto = new bootstrap.Modal(document.getElementById('form-modal-producto'));
    modalFormProducto.toggle();
}

function showModalFormEnviar() {
    var modalFormProducto = new bootstrap.Modal(document.getElementById('form-modal-enviar'));
    modalFormProducto.toggle();
}

function showModalFormOrdenes(){
    var modalFormOrdenes = new bootstrap.Modal(document.getElementById('form-modal-ordenes'));
    modalFormOrdenes.toggle();
}


function formatoFecha(fechaInput) {

    // Verificar que el formato es dd/mm/yyyy usando una expresión regular
    const regex = /^(\d{2})\/(\d{2})\/(\d{4})$/;
    const match = fechaInput.match(regex);
    // Extraer los componentes de la fecha
    const day = match[1];
    const month = match[2];
    const year = match[3];

    // Formatear a yyyy-mm-dd
    const fechaComprobante_formato = `${year}-${month}-${day}`;
    return fechaComprobante_formato;
}
function validateFechaComprobante() {
    if (!labelFechaComprobante.value) {
        labelFechaComprobante.classList.add("is-invalid");
        errMsgFechaComprobante.innerHTML = "El campo Fecha no puede estar vacío";
        errMsgFechaComprobante.style.display = "block";
        return false;
    }
    
    if (labelFechaOrden.value && labelFechaComprobante.value) {

        const fechaComprobante_formato = formatoFecha(labelFechaComprobante.value);
        const fechaOrden_formato = formatoFecha(labelFechaOrden.value);

        const date_comprobante = new Date(fechaComprobante_formato);
        const date_orden = new Date(fechaOrden_formato);

        if (date_comprobante < date_orden) {
            labelFechaComprobante.classList.add("is-invalid");
            errMsgFechaComprobante.innerHTML = "La fecha de comprobante no puede ser menor a la de orden";
            errMsgFechaComprobante.style.display = "block";
            return false;
        }
    }
    
    labelFechaComprobante.classList.remove("is-invalid");
    errMsgFechaComprobante.innerHTML = "";
    errMsgFechaComprobante.style.display = "none";
    return true;
}

function validateOrdenAsociada() {
    if (!labelIdOrden.value) {
        labelIdOrden.classList.add("is-invalid");
        errMsgOrdenAsociada.innerHTML = "La orden asociada no puede estar vacía";
        errMsgOrdenAsociada.style.display = "block";
        return false;
    }


    labelIdOrden.classList.remove("is-invalid");
    errMsgOrdenAsociada.innerHTML = "";
    errMsgOrdenAsociada.style.display = "none";
    return true;
}

function validarFormularioComprobante() {
    let val_fecha = validateFechaComprobante();
    let val_orden = validateOrdenAsociada();
    if (val_fecha && val_orden) {
        return true;
    }

    return false;
}