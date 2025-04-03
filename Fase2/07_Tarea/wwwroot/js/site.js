$().ready(

    () => {
        detallefactura()
    }

);

var detallefactura = () => {
    var leerClientes = new DetalleFactura()
    leerClientes.listaClientes()
}

var unCliente = () => {
    var id = $('#listaClientes').val()
    var uncliente = new DetalleFactura()
    uncliente.unCliente(id)
}

var nuevoCliente = () => {
    var nuevoCliente = new DetalleFactura()
    nuevoCliente.nuevoCliente()
}

var limpiarcajas = () => {
    var limpiarcajas = new DetalleFactura()
    limpiarcajas.limpiarCampos()
}

var listaProductos = () => {
    var listaProductos = new DetalleFactura()
    listaProductos.listaProductos()

}

var controlarstock = (caja) => {
    var controlarstock = new DetalleFactura()
    controlarstock.controlarstock(caja.id, caja.value)
}


var cargarProdcutosLista = (id, precio, nombre) => {
    var cargarProdcutosLista = new DetalleFactura()
    cargarProdcutosLista.cargarProdcutosLista(id, precio, nombre);
}

var eliminarfila = (boton) => {
    var eliminarfila = new DetalleFactura()
    eliminarfila.eliminarfila(boton)
}

var actualizarTotal = () => {
    var actualizarTotal = new DetalleFactura()
    actualizarTotal.actualizarTotal()
}


var guardarFactura = () => {
    var guardarFactura = new DetalleFactura();
    guardarFactura.guardarFactura();
};


var imprimirFactura = () => {
    var contenidoOrignal = document.body.innerHTML;

    var imprimir = document.getElementById("imprimir").innerHTML;

    document.body.innerHTML = imprimir;

    window.print();

    document.body.innerHTML = contenidoOrignal;

}