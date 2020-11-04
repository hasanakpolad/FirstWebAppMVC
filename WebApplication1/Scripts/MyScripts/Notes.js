function modal(Id) {
    $.ajax({
        url: "/Notes/Form",
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
    $('#notesForm').validate({
        rules: {
            Title: {
                required: true
            },
            Note: {
                required: true
            }
        },
        messages: {
            Title: {
                required: 'Başlık Alanı Boş Geçilemez'
            },
            Note: {
                required: 'Notlar Alanı Boş Geçilemez'
            }
        }
    });
};
function warn(Id) {
    alertify.confirm('Uyarı', 'Silmek İstediğinizden Emin misiniz?',
        function () {
            $.post('/Notes/Delete', { Id }, function (data, status) {
                if (data) {
                    alertify.success('Silme İşlemi Başırılı')
                    function deleteRow(r) {

                       /* document.getElementById("Id").*/deleteRow(Id);
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
    var table = $('#notesTable').DataTable({
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
        "order": [0, "asc"],
        "serverSide": true,
        "pagingType": "full_numbers",
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json",

        },

        "ajax": {
            "url": "/Notes/GetNotes",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "Title", "name": "Title", "autoWidth": true },
            { "data": "Note", "name": "Note", "autoWidth": true },
            { "data": "General", "name": "General", "autoWidth": true },
            { "data": "User", "name": "User", "autoWidth": true },
            { "data": "Category", "name": "Category", "autoWidth": true }
        ],

        "fnDrawCallback": function () {
            $('#btn1').prependTo($('#notesTable_filter'));

        }

    });
});