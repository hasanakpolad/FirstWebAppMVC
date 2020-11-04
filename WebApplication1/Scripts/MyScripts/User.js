function modal(Id) {
    $.ajax({
        url: "/Users/Form",
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
    $('#userForm').validate({
        rules: {
            Name: {
                required: true
            },
            Surname: {
                required: true
            },
            Mail: {
                required: true,
                email: true
            },
            Phone: {
                maxlength:11
            }
        },
        messages: {
            Name: {
                required: 'Kullanıcı Adı Boş Geçilemez'
            },
            Surname: {
                required: 'Kullanıcı Soyadı Boş Geçilemez'
            },
            Mail: {
                email: "E-mail Adresi Geçersiz",
                required: 'E-posta Adresi Boş Geçilemez'
            },
            Phone: {
                maxlength:"En fazla 11 karakter girilmelidir."
            }
        }
    });
};

function warn(Id) {
    alertify.confirm('Uyarı', 'Silmek İstediğinizden Emin misiniz?',
        function () {
            $.post("/Users/Delete", { Id }, function (data, status) {
                if (data) {
                    debugger;
                    //Ekranan Ok alert
                    alertify.success('Silme İşlemi Başarılı');
                    //Idli tr yi sileceksin

                    function deleteRow(r) {


                        document.getElementById("Id").deleteRow(Id);
                    };

                    location.reload();
                }
            });

        },
        function () {
            alertify.error('İşlem İptal Edildi.')
        });
};
$(document).ready(function () {
    var table = $('#userTable').DataTable({

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
            {
                targets: 5,
                render: function (data) {
                    if (data == '0') {
                        return 'Kullanıcı'
                    }
                    if (data == '1') {
                        return 'Yetkili'
                    }
                    if (data == '2') {
                        return 'Admin'
                    }
                }
            }
        ],

        "ajax": {
            "url": "/Users/GetUsers",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "Name", "name": "Name", "autoWidth": true },
            { "data": "Surname", "name": "Surname", "autoWidth": true },
            { "data": "Phone", "name": "Phone", "autoWidth": true },
            { "data": "Mail", "name": "Mail", "autoWidth": true },
            { "data": "Rank", "name": "Rank", "autoWidth": true }
        ],
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "Hepsi"]],
        "order": [[1, "asc"]],
        "serverSide": true,
        "pagingType": "full_numbers",
        "language": {
            "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
        },

        "fnDrawCallback": function (oSettings) {
            $('#btn1').prependTo($('#userTable_filter'));
        },

    });

});

/*$(document).ready(function () {
    $('#Rank').selectpicker({
        style: 'btn-info',
        size: 4
    })

});
*/