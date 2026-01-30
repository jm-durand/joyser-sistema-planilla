$(document).ready(function () {
    $('#navMenuCheques').addClass("m-menu__item--open");
    $('#navMenuCheques').addClass("m-menu__item--expanded");
    $('#navSubMenuRegistroCheques').addClass("m-menu__item--active");       
})

$(document).on("keypress", ".onlynumbers", function (e) {
    if (String.fromCharCode(e.keyCode).match(/[^0-9\.]/g)) return false;
})

function ReloadJquery() {
    var script = document.createElement("script");
    script.setAttribute("type", "text/javascript");
    script.setAttribute("src", "/planilla/assets/js/cheques.js");
    document.getElementsByTagName("head")[0].appendChild(script);
    //document.getElementsByClassName("modal-body")[0].appendChild(script);
}

function Mensaje(mensaje, tipo) {
    setTimeout(function () {
        toastr.options = {
            closeButton: true,
            progressBar: true,
            showMethod: 'slideDown',
            timeOut: 5000
        };
        if (tipo == 'success') {
            toastr.success(mensaje);
        } else if (tipo == 'info') {
            toastr.info(mensaje);
        } else if (tipo == 'error') {
            toastr.error(mensaje);
        } else if (tipo == 'warning') {
            toastr.warning(mensaje);
        }

    }, 300);
}

function button_click(objTextBox, objBtnID) {
    if (window.event.keyCode == 13) {
        document.getElementById(objBtnID).focus();
        document.getElementById(objBtnID).click();
    }
}

function abrirModalEmitirCheque() {
    $('#gpConfirmarEmitirCheque').modal('toggle');
}

function abrirModalAnularCheque() {
    $('#gpConfirmarAnularCheque').modal('toggle');
}

function populateTextInput() {
    $(".valorescheckbox").val('');
    var idcheck;
    $(".clickme").each(function () {
        if ($(this).prop('checked')) {
            idcheck = $(this).val();
            $(".valorescheckbox").val($(".valorescheckbox").val() + ',' + ($(this).val()));
        }
    });
}

function CalcularMontoTotal() {
    $(".subtotalimporte").text('0');
    $(".totalpagar").val('0');
    $(".valorescontratos").val('');
    var importe;
    var eachimporte;
    var idimporte;
    $(".montopendientecalcular").each(function () {
        if ($(this).val() == "") { eachimporte = "0"; $(this).val('0'); } else { eachimporte = $(this).val(); };
        idimporte = $(this).attr("id");
        importe = parseFloat(eachimporte);
        $(".subtotalimporte").text(parseFloat($(".subtotalimporte").text()) + importe);
        $(".totalpagar").val(parseFloat($(".totalpagar").val()) + importe);
        $(".valorescontratos").val($(".valorescontratos").val() + ',' + (idimporte + '_' + eachimporte));
        $(".botonmontoletras")[0].click();
    });
}
function validarsolonumero(event) {
    var target = event.target || event.srcElement;
    var id = target.id;
    var code = event.which ? event.which : event.keyCode;
    if (code !== 37 && code !== 39) {
        $('#' + id).val($('#' + id).val().replace(/[^0-9]/ig, function (str) { return ''; }));
    }
}
function RebuildDataTable() {
    $('.m-datatable').mDatatable({
        data: {
            saveState: { cookie: false },
            pageSize: 10,
        },
        layout: {
            minHeight: '100%',
        },
        search: {
            input: $('#generalSearch')
        },
        pagination: true,
        sortable: false,
        columns: [
            {
                field: '#',
                title: '#',
                width: 30,
                textAlign: 'center',
            },
            {
                field: 'CONTRATO',
                title: 'CONTRATO',
                width: 150,
                textAlign: 'left',
            },
            {
                field: 'PROYECTO',
                title: 'PROYECTO',
                width: 180,
                textAlign: 'left',
            },
            {
                field: 'MONTO CONTRATO',
                title: 'MONTO CONTRATO',
                width: 80,
                textAlign: 'left',
            },
            {
                field: 'MONTO PAGADO',
                title: 'MONTO PAGADO',
                width: 80,
                textAlign: 'left',
            },
            {
                field: 'MONTO PENDIENTE',
                title: 'MONTO PENDIENTE',
                width: 80,
                textAlign: 'left',
            },
            {
                field: 'OPCIÓN',
                title: 'OPCIÓN',
                width: 80,
                textAlign: 'center',
            },
        ],
    });
}