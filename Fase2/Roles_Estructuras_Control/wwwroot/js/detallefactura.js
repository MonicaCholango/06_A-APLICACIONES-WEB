class DetalleFactura {
  constructor() {}

  listaClientes() {
    var html = "<option value=0>Seleccione un opcion</option>";
    $.get("../../clientes/ListaClientes", (listaclientes) => {
      $.each(listaclientes, (index, valor) => {
        html += `<option value=${valor.id}>${valor.nombre}</option>`;
      });
      $("#listaClientes").html(html);
    });
  }

  unCliente(id) {
    $.get("../../clientes/unCliente?id=" + id, (cliente) => {
      console.log(cliente);
      $("#NombreCliente").val(cliente.nombre);
      $("#DireccionCliente").val(cliente.direccion);
      $("#TelefonoCliente").val(cliente.telefono);
      $("#EmailCliente").val(cliente.email);
    });
  }
  nuevoCliente() {
    $("#NombreCliente").prop("disabled", false);
    $("#DireccionCliente").prop("disabled", false);
    $("#TelefonoCliente").prop("disabled", false);
    $("#EmailCliente").prop("disabled", false);
    $("#listaClientes").prop("disabled", true);
    $("#nuevoCliente").val(1);
    $("#cancelar").css("display", "block");
  }

  listaProductos() {
    var html = "";
    $.get("../../productos/ListaProductos", (listaprodcutos) => {
      $.each(listaprodcutos, (index, stock) => {
        console.log(stock);
        html += `<tr>
                <td>${index + 1} </td>
                <td>${stock["productoModels"]["nombreProducto"]} </td>
                <td>
                    <div class="input-group has-validation">
                        <input type="number" id="stock-${
                          stock.id
                        }" onfocusout="controlarstock(this)" class="form-control" style="width:60px">
                        <div id="validador${
                          stock.id
                        }" style="visibility:hidden">*
                        </div>
                    </div>
                </td>
                <td>${stock.precioVenta}</td>
                <td>
                    <button onclick="cargarProdcutosLista(${stock.id},${
          stock.precioVenta
        },'${
          stock["productoModels"]["nombreProducto"]
        }')" class="btn btn-outline-success">
                    <i class="icon-plus"></i>
                    </button>
                </td>
                </tr>
                `;
      });
      $("#cuerpoproducto").html(html);
    });
  }

  controlarstock(id, cantidad) {
    if (cantidad <= 0 || !cantidad || cantidad == null || cantidad == null)
      return;
    id = id.split("-");

    $.post(
      "../stocks/controlarstock",
      { id: id[1], cantidad: cantidad },
      (dato) => {
        if (!dato) {
          alert("No se encuentra la cantidad en stock");
          $("#btn_agregar").prop("disabled", true);
        } else {
          $("#btn_agregar").prop("disabled", false);
        }
      }
    );
  }

    async cargarProdcutosLista(id, precio, nombre) {
        var cantidad = $(`#stock-${id}`).val();
        if (!cantidad || cantidad <= 0) {
            alert("Ingrese una cantidad valida");
            return;
        }

        let contador = 0;
        $("#cuerpoTabla tr").each(function () {
            contador = parseInt($(this).find("td:eq(0)").text())
        });

      var html = `<tr>
            
            <td>
                ${ contador + 1 }
            </td>
            <td>
                ${cantidad}
            </td>
            <td>
                ${nombre}
            </td>
            <td>
                ${precio}
            </td>
            <td>
                ${cantidad * precio}
            </td>
            <td>
            <button class="btn btn-danger no-print" onclick="eliminarfila(this)">X</button>
            </td>
        </tr>`;
    await $("#cuerpoTabla").append(html);

    await $("#productos").modal("hide");
    actualizarTotal();
  }

  eliminarfila(boton) {
    $(boton).closest("tr").remove();
    actualizarTotal();
  }

  actualizarTotal() {
    let subtotal = 0;
    $("#cuerpoTabla tr").each(function () {
      const valor = parseFloat($(this).find("td:eq(4)").text());
      if (!isNaN(valor)) {
        subtotal += valor;
      }
    });
    let descuentoIngreado = $("#descuentoId").val();
    let descuento = 0;
    if (descuentoIngreado > 0) {
      descuento = (parseFloat(descuentoIngreado) * subtotal) / 100;
    }
    let subtotalcondescuento = subtotal - descuento;
    let iva = subtotalcondescuento * 0.15;
    let total = iva + subtotalcondescuento;

    $("#subtotal").text(subtotal);
    $("#descuento").text(descuento);
    $("#iva").text(iva.toFixed(2));
    $("#total").text(total.toFixed(2));
  }

  limpiarCampos() {
    $("#NombreCliente").prop("disabled", true);
    $("#DireccionCliente").prop("disabled", true);
    $("#TelefonoCliente").prop("disabled", true);
    $("#EmailCliente").prop("disabled", true);
    $("#listaClientes").prop("disabled", false);
    $("#nuevoCliente").val(0);
    $("#cancelar").css("display", "none");
  }

  async guardarFactura() {
    try {
      // Validar que haya productos
      if ($("#cuerpoTabla tr").length === 0) {
        alert("Debe agregar al menos un producto a la factura");
        return;
      }

      // Validar que haya un cliente seleccionado
      let clienteId = $("#listaClientes").val();
      if (!clienteId || clienteId === "0") {
        alert("Debe seleccionar un cliente");
        return;
      }

      let esNuevoCliente = $("#nuevoCliente").val() === "1";
      let cliente = {
        id: clienteId,
        nombre: $("#NombreCliente").val(),
        direccion: $("#DireccionCliente").val(),
        telefono: $("#TelefonoCliente").val(),
        email: $("#EmailCliente").val(),
        esNuevo: esNuevoCliente,
      };

      // Obtener productos de la tabla
      let productos = [];
      $("#cuerpoTabla tr").each(function () {
        let producto = {
          cantidad: parseInt($(this).find("td:eq(1)").text()),
          nombreProducto: $(this).find("td:eq(2)").text().trim(),
          precioUnitario: parseFloat($(this).find("td:eq(3)").text()),
          total: parseFloat($(this).find("td:eq(4)").text()),
        };
        productos.push(producto);
      });

      // Obtener totales
      let totales = {
        subtotal: parseFloat($("#subtotal").text()),
        descuento: parseFloat($("#descuento").text()),
        iva: parseFloat($("#iva").text()),
        total: parseFloat($("#total").text()),
      };

      // Crear objeto factura
      let factura = {
        cliente: cliente,
        productos: productos,
        totales: totales,
      };

      console.log("Datos a enviar:", factura); // Para debugging

      const response = await $.ajax({
        url: "../../DetalleFactura/GuardarFactura",
        type: "POST",
        data: JSON.stringify(factura),
        contentType: "application/json",
      });

        
      if (response.success) {
        alert("Factura guardada exitosamente");
        // Limpiar la tabla y campos
        $("#cuerpoTabla").empty();
        $("#subtotal").text("0.00");
        $("#descuento").text("0.00");
        $("#iva").text("0.00");
        $("#total").text("0.00");
        $("#descuentoId").val("");
        limpiarcajas();
      } else {
        alert("Error al guardar la factura: " + response.message);
      }
    } catch (error) {
      console.error("Error:", error);
      alert("Error al guardar la factura: " + error.message);
    }
  }
}

