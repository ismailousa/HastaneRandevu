if (!Modernizr.inputtypes.date) {
    $(function () {

        $("#DT").datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy',
            todayHighlight: true,
            clearBtn: true,
            orientation: 'bottom'
        });

    });
}