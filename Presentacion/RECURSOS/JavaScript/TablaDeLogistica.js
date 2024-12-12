$(document).ready(function () {
    let buquesDisponibles = [];
    let table; // Variable global para almacenar la instancia de DataTable
    let cambiosPendientes = [];

    // Función para actualizar la tabla y guardar los cambios
    window.updateTableData = function (element) {
        const row = $(element).closest('tr');
        const rowData = table.row(row).data();
        const column = $(element).closest('td').index();
        const newValue = $(element).val();

        // Guardar el cambio en la variable cambiosPendientes
        cambiosPendientes.push({
            rowId: rowData.IdRegistro,
            column: column,
            newValue: newValue,
            rowData: rowData
        });

        // Actualizar el valor en la tabla
        table.cell(row, column).data(newValue).draw();

        // Mostrar los cambios pendientes en la consola
        console.log("Cambios pendientes:", cambiosPendientes);
    };


    // Carga la lista de buques registrados
    $.ajax({
        url: 'Logistica.aspx/ObtenerBuquesRegistrados',
        type: 'POST',
        contentType: 'application/json',
        success: function (response) {
            try {
                buquesDisponibles = JSON.parse(response.d);
                console.log("Buques disponibles:", buquesDisponibles);
                initializeDataTable(); // Inicializar la tabla solo después de cargar los buques
            } catch (err) {
                console.error("Error al parsear la respuesta:", err);
            }
        },
        error: function (error) {
            console.error("Error al obtener los buques registrados:", error);
        }
    });

    // Función para inicializar la tabla DataTable
    function initializeDataTable() {
        table = $('#logisticaTable').DataTable({
            ajax: {
                url: 'Logistica.aspx/ObtenerDatosLogistica',
                type: 'POST',
                contentType: 'application/json',
                dataSrc: function (json) {
                    console.log(JSON.parse(json.d));
                    return JSON.parse(json.d);
                }
            },
            columns: [
                { data: 'IdRegistro', title: 'ID Registro' },
                {
                    data: 'Buque',
                    title: 'Buque',
                    render: function (data, type, row, meta) {
                        const options = buquesDisponibles.map(b =>
                            `<option value="${b}" ${b === data ? "selected" : ""}>${b}</option>`
                        ).join("");
                        return `<select class="form-control buque-dropdown" onchange="updateTableData(data)">${options}</select>`;
                    }
                },
                {
                    data: 'LOA',
                    title: 'LOA',
                    render: function (data, type, row, meta) {
                        return `<input type="number" class="form-control loa-input" value="${data}" onchange="updateTableData(this)" />`;
                    }
                },
                {
                    data: 'Operation Time',
                    title: 'Operation Time',
                    render: function (data, type, row, meta) {
                        const operationTime = data ? data.substring(0, 5) : "00:00";
                        return `
                            <input 
                                type="time" 
                                class="form-control operation-time" 
                                value="${operationTime}" 
                                data-row="${meta.row}" 
                                onchange="updateTableData(this)" />`;
                    }
                },
                {
                    data: 'ETA',
                    title: 'ETA',
                    render: function (data, type, row, meta) {
                        return `<input type="datetime-local" class="form-control eta-input" value="${data}" onchange="updateTableData(this)" />`;
                    }
                },
                {
                    data: 'POB',
                    title: 'POB',
                    render: function (data, type, row, meta) {
                        if (meta.row === 0) {
                            return `
                                <input 
                                    type="datetime-local" 
                                    class="form-control pob-input" 
                                    value="${data}" 
                                    onchange="updatePOB(this, ${meta.row}); updateTableData(this);" 
                                />`;
                        } else {
                            return data || 'Calculado';
                        }
                    }
                },
                {
                    data: 'ETB',
                    title: 'ETB',
                    render: function (data, type, row, meta) {
                        const etb = data ? data : row.POB ? calculateETB(row.POB).toISOString().slice(0, 19).replace("T", " ") : '';
                        return `<input type="datetime-local" class="form-control etb-input" value="${etb}" disabled />`;
                    }
                },
                {
                    data: 'ETC',
                    title: 'ETC',
                    render: function (data, type, row, meta) {
                        const etc = data ? data : row.ETB ? calculateETC(row.ETB).toISOString().slice(0, 19).replace("T", " ") : '';
                        return `<input type="datetime-local" class="form-control etc-input" value="${etc}" disabled />`;
                    }
                },
                {
                    data: 'ETD',
                    title: 'ETD',
                    render: function (data, type, row, meta) {
                        const etd = data ? data : row.ETC ? calculateETD(row.ETC).toISOString().slice(0, 19).replace("T", " ") : '';
                        return `<input type="datetime-local" class="form-control etd-input" value="${etd}" disabled />`;
                    }
                },
                { data: 'Cargo', title: 'Cargo' }
            ],
            language: {
                url: "//cdn.datatables.net/plug-ins/1.13.6/i18n/es-MX.json"
            },
            responsive: true
        });

        // Asignar la función a los elementos editables después de inicializar la tabla
        $('#logisticaTable').on('change', '.buque-dropdown, .loa-input, .operation-time, .eta-input, .pob-input', function () {
            updateTableData(this);
        });
    }

    // Botón para actualizar datos
    $('#btnActualizar').on('click', function () {
        $.ajax({
            url: 'Logistica.aspx/ActualizarDatosLogistica',
            type: 'POST',
            contentType: 'application/json',
            success: function (response) {
                if (response.d) {
                    table.ajax.reload(null, false);
                    alert(response.d);
                } else {
                    alert('No se realizaron cambios.');
                }
            },
            error: function (error) {
                console.error('Error al actualizar los datos:', error);
                alert('Ocurrió un error al actualizar los datos');
            }
        });
    });
});

// Funciones de cálculo
function calculateETB(pob) {
    if (!pob) return '';
    return new Date(new Date(pob).getTime() + 60 * 60 * 1000);
}

function calculateETC(etb) {
    return new Date(new Date(etb).getTime() + 60 * 60 * 1000);
}

function calculateETD(etc) {
    return new Date(new Date(etc).getTime() + 60 * 60 * 1000);
}



// Requerimientos
/* 

 - Dropdown list en la parte de buque que permite seleccionar entre todos los buques registrados.
 - LOA editable desde la tabla.
 - Operation Time editable desde la tabla en formato HH:MM
 - ETA editable desde la tabla con formato de fecha anio/mes/dia/horas y minutos.
 - POB editable exclusivamente en el primer renglon, los demas se calculan mediante ETD del renglon anterior + 1 hora.
 - ETB = POB + 1 HORA
 - ETC = ETB + OPERATION TIME
 - ETD = ETC + 1 HORA

*/
