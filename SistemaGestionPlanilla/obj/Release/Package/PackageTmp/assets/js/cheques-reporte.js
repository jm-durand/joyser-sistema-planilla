$(document).ready(function () {
    $('#navMenuReportes').addClass("m-menu__item--open");
    $('#navMenuReportes').addClass("m-menu__item--expanded");
    $('#navSubMenuPlanillaReporteCheques').addClass("m-menu__item--active");

    $('.m-select2-general').select2({
        placeholder: "Seleccione una opcion",
        width: '100%'
    });

    $('.datepickers').datepicker({
        rtl: mUtil.isRTL(),
        todayHighlight: true,
        orientation: "bottom left",
        templates: arrows,
        format: 'dd/mm/yyyy',
        language: 'es'
    });
})

function ReloadJquery() {
    var script = document.createElement("script");
    script.setAttribute("type", "text/javascript");
    script.setAttribute("src", "/planilla/assets/js/cheques-reporte.js");
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