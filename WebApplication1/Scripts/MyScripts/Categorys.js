function modal(Id) {
    $.ajax({
        url: "/Category/Form",
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
    $('#categoryForm').validate({
        rules: {
            CategoryName: {
                required: true
            }
        },
        messages: {
            CategoryName: {
                required: 'Kategori Adı Boş Geçilemez'
            }
        }
    });
}
function warn(Id) {
    alertify.confirm('Uyarı', 'Silmek İstediğinizden Emin misiniz?',
        function () {
            $.post('/Category/Delete', { Id }, function (data, status) {
                if (data) {
                    function deleteRow(r) {

                        document.getElementById("Id").deleteRow(Id);
                    }
                    alertify.success('Silme işlemi Başarılı')
                    location.reload();
                }

            });
        },
        function () {
            alertify.error('İşlem İptal Edildi.')
        })
}

$(document).ready(function () {
    $('#catTable').DataTable({
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
        "order": [[0, "asc"]],
        "serverSide": true,
        "pagingType": "full_numbers",
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        },
        "ajax": {
            "url": "/Category/GetCategory",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "CategoryName", "name": "CategoryName", "autoWidth": true },
            { "data": "Notes", "name": "Notes", "autoWidth": true }
        ],
        "fnDrawCallback": function (oSettings) {
            $('#btn1').prependTo($('#catTable_filter'));
        },
    });
});