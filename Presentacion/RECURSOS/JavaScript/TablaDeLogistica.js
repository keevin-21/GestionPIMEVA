///let table = new DataTable('#myTable', {
//    responsive: true
//});

$(document).ready(function () {
    $('#logisticaTable').DataTable({
        ajax: {
            url: 'Logistica.aspx/ObtenerDatosLogistica', // Ruta al WebMethod
            type: 'POST',
            contentType: 'application/json',
            dataSrc: function (json) {
                return JSON.parse(json.d); // Procesar la respuesta del servidor
            }
        },
        columns: [
            { data: 'Buque', title: 'Buque' },
            { data: 'LOA', title: 'LOA' },
            { data: 'OperationTime', title: 'Operation Time' },
            { data: 'ETA', title: 'ETA' },
            { data: 'POB', title: 'POB' },
            { data: 'ETB', title: 'ETB' },
            { data: 'ETC', title: 'ETC' },
            { data: 'ETD', title: 'ETD' },
            { data: 'Cargo', title: 'Cargo' }
        ],
        language: {
            url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/es-MX.json"
        },
        responsive: true
    });
});
