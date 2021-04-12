var fila = '<tr class="selected" id="fila' + contador + '"><td hidden="hidden">' + id_producto + '</td><td><input type="hidden" name="id_producto[]" value="' + id_producto + '" />' + producto + '</td><td>' + cajas + '</td><td>' + precioCompra + '</td><td>' + unidadcaja + '</td><td>' + numlote + '</td><td hidden="hidden">' + enexpoid + '</td> <td>' + vencimiento + '</td><td>' + enexpo + '</td><td hidden="hidden">' + descuento + '</td><td><button type="button" class="btn btn-danger" onclick="Eliminar(' + contador + ');"><span class="fa fa-trash"></span></button></td></tr>';



function Agregar() {
    datosProducto = document.getElementById('cmbProducto').value.split(' ');
    datosLote = document.getElementById('cmbLote').value.split(' ');
    id_producto = datosProducto[0];
    //id_producto = $("#cmbProducto").val();
    producto = $("#cmbProducto option:selected").text();
    cajas = $("#txtcajas").val();
    precioCompra = $("#txtPrecioCompra").val();
    unidadcaja = datosProducto[1];
    //unidadcaja = $("#txtUndcajas").val();
    //precioventa = $("#txtPrecioVenta").val();
    id_lote = datosLote[0];
    numlote = $("#cmbProducto option:selected").val();

    vencimiento = datosLote[1];
    descuento = $("#txtDescuento").val();
    midesc = parseFloat(descuento);

    enexpoid = $("#cmbExpo").val();
    enexpo = $("#cmbExpo option:selected").text();
    //enexpo = document.getElementById("chkIva").checked;//mejor true o false
    if (enexpo) {
        estadochk = "Si"
    }
    else {
        estadochk = "NO"
    }
    //state = document.getElementById("chkIva").checked;//mejor true o false
    stock = unidadcaja * cajas;

    if (id_producto > 0 && cajas > 0 && precioCompra > 0 && unidadcaja > 0 && numlote != "" && vencimiento != "" && descuento >= 0) {
        subtotal[contador] = (cajas * precioCompra);
        total = total + subtotal[contador];
        descTotal[contador] = midesc;

        var fila = '<tr class="selected" id="fila' + contador + '"><td hidden="hidden">' + id_producto + '</td><td><input type="hidden" name="id_producto[]" value="' + id_producto + '" />' + producto + '</td><td>' + cajas + '</td><td>' + precioCompra + '</td><td>' + unidadcaja + '</td><td>' + numlote + '</td><td hidden="hidden">' + enexpoid + '</td> <td>' + vencimiento + '</td><td>' + enexpo + '</td><td hidden="hidden">' + descuento + '</td><td><button type="button" class="btn btn-danger" onclick="Eliminar(' + contador + ');"><span class="fa fa-trash"></span></button></td></tr>';
        var fila2 = '<tr class="selected" id="fila' + contador + '"><td hidden="hidden">' + id_producto + '</td><td><input type="hidden" name="id_producto[]" value="' + id_producto + '" />' + producto + '</td><td>' + precioCompra + '</td><td>' + cajas + '</td><td>' + unidadcaja + '</td><td hidden="hidden">' + id_lote + '</td><td>' + numlote + '</td><td>' + vencimiento + '</td><td hidden="hidden">' + enexpoid + '</td><td>' + enexpo + '</td><td hidden="hidden">' + descuento + '</td><td><button type="button" class="btn btn-danger" onclick="Eliminar(' + contador + ');"><span class="fa fa-trash"></span></button></td></tr>'
        contador++;
        MiSub = MiSub + (precioCompra * cajas);
        Limpiar();

        Midescuento += midesc;
        //$("#Total").html("C$ " + subtotal);
        //$("#Subtotal").html("C$ " + MiSub);

        var EsIva = document.getElementById("chkIva").checked;
        if (EsIva) {
            //Miiva = Miiva + iva;
            Miiva = MiSub * 0.15;
            MiTotal = (MiSub + Miiva - Midescuento);
            $("#Total").html("C$ " + MiTotal);
            $("#iva").html("C$ " + Miiva);
            $("#descuento").html("C$ " + Midescuento);
            $("#subtotal").html("C$ " + MiSub);
            Evaluar();
            $("#tabla_detalle").append(fila);
        } else {
            Miiva = 0;

            MiTotal = (MiSub - Midescuento);
            $("#Total").html("C$ " + MiTotal);
            $("#iva").html("C$ " + Miiva);
            $("#descuento").html("C$ " + Midescuento);
            $("#subtotal").html("C$ " + MiSub);
            Evaluar();
            $("#tabla_detalle").append(fila);
        }

    } else {
        alert("Falta llenar algunos campos obligatorios ")
    }

}
