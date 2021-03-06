﻿function modal(Id) {
    $.ajax({
        url: "/Group/Form",
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
    $('#groupForm').validate({
        rules: {
            GroupName: {
                required: true
            },
            Explain: {
                required: true
            }
        },
        messages: {
            GroupName: {
                required: 'Grup Adı Boş Geçilemez'
            },
            Explain: {
                required: 'Açıklama Boş Geçilemez'
            }
        }
    });
}
function warn(Id) {
    alertify.confirm('Uyarı', 'Silmek istediğinizden Emin misiniz?',
        function () {
            $.post('/Group/Delete', { Id }, function (data, status) {
                if (data) {
                    alertify.success('Silme İşlemi Başarılı')

                    function deleteRow() {

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
    var table = $('#groupsTable').DataTable({
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
            "url": "/Group/GetGroups",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "GroupName", "name": "GroupName", "autoWidth": true },
            { "data": "Explain", "name": "Explain", "autoWidth": true },
        ],
        "fnDrawCallback": function (oSettings) {
            $('#btn1').prependTo($('#groupsTable_filter'));
        },
    });
});