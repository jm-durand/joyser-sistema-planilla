var DatatablesBasicPaginations = function () {

    var initTable1 = function () {
        var table = $('#m_table_1');

        // begin first table
        table.DataTable({
            responsive: true,
            pageLength: 10,
            pagingType: 'full_numbers'
          
        });
    };

    return {

        //main function to initiate the module
        init: function () {
            initTable1();
        },

    };

}();

jQuery(document).ready(function () {
    DatatablesBasicPaginations.init();
});