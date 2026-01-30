//== Class definition

var BootstrapDatepicker = function () {

    //== Private functions
    var demos = function () {
        var arrows;
        if (mUtil.isRTL()) {
            arrows = {
                leftArrow: '<i class="la la-angle-right"></i>',
                rightArrow: '<i class="la la-angle-left"></i>'
            };
        } else {
            arrows = {
                leftArrow: '<i class="la la-angle-left"></i>',
                rightArrow: '<i class="la la-angle-right"></i>'
            };
        }

        $('.m-select2-general').select2({
            placeholder: "Seleccione una opcion",
            width: '100%'
        });
  
        var FechaInicial = $('.fechainicial').val();
        var FechaFinal = $('.fechafinal').val();

        $('.rangofechaplanilla').daterangepicker({
            autoUpdateInput: true,
            buttonClasses: 'm-btn btn',
            applyClass: 'btn-primary',
            cancelClass: 'btn-secondary',
            startDate: FechaInicial,
            endDate: FechaFinal,
            locale: {
                format: 'DD/MM/YYYY',
                applyLabel: 'Aplicar',
                cancelLabel: 'Cancelar',
                daysOfWeek: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sa'],
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Setiembre', 'Octubre', 'Noviembre', 'Diciembre']
            }
        });
        $('.rangofechaplanilla').on('apply.daterangepicker', function (ev, picker) {
            var myString = $('.rangofechaplanilla').val();
            var index = myString.split('-');
            var FechaInicial = index[0]; // Gets the first part
            var FechaFinal = index[1]; //Gets second part  
            $('.fechainicial').val(FechaInicial);
            $('.fechafinal').val(FechaFinal);
            $('.btnbuscarcheque')[0].click();
        });        

        $('.i-checks').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'icheckbox_square-green',
        });

        $('.i-checks').on('ifChanged', function (event) {
            populateTextInput();            
        });

        $('.cbocontratista').on('change', function () {
            $('.m-datatable').mDatatable().search($(this).val().toLowerCase(), 'Contratista');
        });

        $('.cbocontratista').selectpicker();

        $('.datepickers').datepicker({
            rtl: mUtil.isRTL(),
            todayHighlight: true,
            orientation: "bottom left",
            templates: arrows,
            format: 'dd/mm/yyyy',
            language: 'es'
        });

    };

    return {
        // public functions
        init: function () {
            demos();
        }
    };
}();

jQuery(document).ready(function () {    
    BootstrapDatepicker.init();
});

