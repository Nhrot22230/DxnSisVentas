function showModalForm(modalId) {
    var modalForm = new bootstrap.Modal(document.getElementById(modalId));
    modalForm.toggle();
}


function validarFormulario() {

    return true;
}

//<asp:BoundField DataField="producto.idProductoCadena" HeaderText="ID Producto" />
//        <asp:BoundField DataField="producto.nombre" HeaderText="Producto" />
//        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
//        <asp:BoundField DataField="producto.precioUnitario" HeaderText="Precio" />
//        <asp:BoundField DataField="subtotal" HeaderText="Subtotal" />
//        <asp:TemplateField HeaderText="Acciones">
//            <ItemTemplate>


function autoUpdate() {
    porcentajeDescuento = document.getElementById('ContentPlaceHolder1_TxtDescuento').value;
    var total = 0;
    var gv = document.getElementById('ContentPlaceHolder1_gvLineasOrdenVenta');
       
    if (gv) {
        for (var i = 1; i < gv.rows.length; i++){
            var subtotalCell = gv.rows[i].cells[4];
            var subtotal = parseFloat(subtotalCell.innerText) || 0;
            total += subtotal;
        }
    }
    total -= (total * porcentajeDescuento / 100);
    document.getElementById('ContentPlaceHolder1_txtTotal').value = total.toFixed(2);
}

function avoidEnterKey(event) {
    if (event.keyCode === 13) {
        return false; // Evita que se propague el evento
    }
    return document.getElementById('ContentPlaceHolder1_TxtDescuento').value;
}