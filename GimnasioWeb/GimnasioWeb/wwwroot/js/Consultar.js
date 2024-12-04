
$(document).ready(function () {

    $("#example").DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/2.1.8/i18n/es-ES.json',
        },
        columnDefs: [
            { targets: [0, 1, 2, 3, 4], type: "string" }
        ]
    });

});