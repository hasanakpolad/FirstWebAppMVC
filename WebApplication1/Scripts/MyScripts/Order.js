function modal(Id) {
    $.ajax({
        url: "/Orders/Form",
        data: { Id },
        type: 'GET',
        dataType: 'html',
        success: function (data) {
            $('#modalContainer').html(data);
            Validate();
            $('#modalContainer').modal("show");
        }
    });
};

function Validate() {
    $('#orderForm').validate({
        rules: {
            Title: {
                required: true
            },
            Explain: {
                required: true
            },
            Status: {
                required: true
            }
        },
        messages: {
            Title: {
                required: 'Başlık Alanı Boş Geçilemez'
            },
            Explain: {
                required: 'Açıklama Alanı Boş Geçilemez'
            },
            Status: {
                required: 'Durum Alanı Boş Geçilmez'
            }
        }
    });
};

function warn(Id) {
    alertify.confirm('Uyarı', 'Silmek İstediğinizden Emin misiniz?',
        function () {
            $.post('/Orders/Delete', { Id }, function (data, status) {
                if (data) {
                    alertify.success('Silme İşlemi Başarılı')
                    function deleteRow(r) {

                        document.getElementById("Id").deleteRow(Id);
                    }
                    location.reload();

                }
            });
        },
        function () {
            alertify.error('İşlem İptal Edildi.')
        });
};
$(document).ready(function () {
    var table = $('#ordersTable').DataTable({

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
        "serverSide": true,
        "order": [[3, "asc"]],
        "pagingType": "full_numbers",
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        },

        "ajax": {
            "url": "/Orders/GetOrders",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "Title", "name": "Title", "autoWidth": true },
            { "data": "Explain", "name": "Explain", "autoWidth": true },
            { "data": "Status", "name": "Status", "autoWidth": true },
            { "data": "Group", "name": "Group", "autoWidth": true },
            { "data": "User", "name": "User", "autoWidth": true },
            { "data": "CreatorId", "name": "CreatorId", "autoWidth": true },
            {
                "data": "CreateTime", "name": "CreateTime", "autoWidth": true,
                "render": function (value) {
                    if (value === null) return "";

                    var pattern = /Date\(([^)]+)\)/;
                    var results = pattern.exec(value);
                    var dt = new Date(parseFloat(results[1]));

                    return dt.getDate() + "." +( dt.getMonth() + 1) + "." + dt.getFullYear();
                }
            },
           
            {
                "data": "StartTime", "name": "StartTime", "autoWidth": true,
                "render": function (value) {
                    if (value === null) return "";

                    var pattern = /Date\(([^)]+)\)/;
                    var results = pattern.exec(value);
                    var dt = new Date(parseFloat(results[1]));

                    return  dt.getDate()+ "." +(dt.getMonth() + 1)  + "." + dt.getFullYear();
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
        ],



        "fnDrawCallback": function (oSettings) {
            $('#btn1').prependTo($('#ordersTable_filter'));
        },

    });
});

$(function () {
    $(".datepicker").datepicker({
        format: 'dd/mm/yyyy',
    })
});
