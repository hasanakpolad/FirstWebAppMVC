function modal(Id) {
    $.ajax({
        url: "/Daily/Form",
        data: { Id },
        type: 'GET',
        dataType: 'html',
        success: function (data) {
            $('#modalContainer').html(data);
            Validate();
            $('#modalContainer').modal("show");
        }
    });
}

function Validate() {
    $('#dailyForm').validate({
        rules: {
            Note: {
                required: true
            },
            Status: {
                required: true
            }
        },
        messages: {
            Note: {
                required: 'Notlar Alanı Boş Geçilemez'
            },
            Status: {
                required: 'Durum Alanı Boş Geçilemez'
            }
        }
    });
}
function warn(Id) {
    alertify.confirm('Uyarı', 'Silmek İstediğinizden Emin misiniz?',
        function () {
            $.post('/Daily/Delete', { Id }, function (data, status) {
                if (data) {
                    alertify.success('Silme işlemi Başarılı')
                    function deleteRow(r) {

                        document.getElementById("Id").deleteRow(Id);
                    }
                    location.reload();
                }
            });
        },
        function () {
            alertify.error('İşlem İptal Edildi.')
        })
};

$(document).ready(function () {
    var table = $('#dailysTable').DataTable({
        "columnDefs": [
            {
                targets: 0,
                render: function (data) {
                    return '<div class="dropdown">' +
                        '<a class="btn btn-group-sm dropdown-toggle"  id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria - expanded="false" >' +
                        'İşlem' +
                        '</a >' +
                        '<div class="dropdown-menu" aria-labelledby="dropdownMenuButton">' +
                        '<a class="btn btn-group-sm btn btn-primary" onclick="modal(' + data + ')">' +
                        'Düzenle' +
                        '</a>' +
                        '<a class="btn btn-group-sm btn btn-warning" onclick="warn(' + data + ')" style="width:auto">Sil</a>' +
                        '</div>' +
                        '</div >';
                },

            },
        ],
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "Hepsi"]],
        "order": [[3, "asc"]],
        "serverSide": true,
        "pagingType": "full_numbers",
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        },
        "ajax": {
            "url": "/Daily/GetDailys",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "Note", "name": "Note", "autoWidth": true },
            { "data": "Status", "name": "Status", "autoWidth": true },
            {
                "data": "StartTime", "name": "StartTime", "autoWidth": true,

                "render": function (value) {
                    if (value === null) return "";

                    var pattern = /Date\(([^)]+)\)/;
                    var results = pattern.exec(value);
                    var dt = new Date(parseFloat(results[1]));

                    return dt.getDate() + "." + (dt.getMonth() + 1) + "." + dt.getFullYear();
                }

            },
            {
                "data": "FinishTime", "name": "FinishTime", "autoWidth": true,
                "render": function (value) {
                    if (value === null) return "";

                    var pattern = /Date\(([^)]+)\)/;
                    var results = pattern.exec(value);
                    var dt = new Date(parseFloat(results[1]));

                    return dt.getDate() + "." + (dt.getMonth() + 1) + "." + dt.getFullYear();
                }
            },
            { "data": "User", "name": "User", "autoWidth": true }
        ],

        "fnDrawCallback": function (oSettings) {
            $('#btn1').prependTo($('#dailysTable_filter'));
        },
    });
});
$(function () {
    $("#datepicker").datepicker({
        format: 'dd/mm/yyyy',
        defaultDate: Date
    })
});