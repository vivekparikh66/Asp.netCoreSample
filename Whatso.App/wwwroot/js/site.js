(function ($) {

    /* Trigger app shortcut menu on CTRL+Q press */
    $(document).keydown(function (event) {
        // CTRL + Q
        if (event.ctrlKey && event.which === 81)
            $("a[title*=Apps]").trigger("click");
    });

    /* Initialize basic datatable */
    $.fn.DataTableEdit = function ($options) {
        var options = $.extend({
            dom: "<'row mb-3'<'col-sm-12 col-md-6 d-flex align-items-center justify-content-start'f><'col-sm-12 col-md-6 d-flex align-items-center justify-content-end'B>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",
            responsive: true,
            serverSide: true,
            altEditor: true,
            pageLength: 10,
            select: { style: "single" },
            buttons: [
                {
                    extend: 'selected',
                    text: '<i class="las la-times mr-1"></i> Delete',
                    name: 'delete',
                    className: 'btn-danger mr-1'
                },
                {
                    extend: 'selected',
                    text: '<i class="las la-edit mr-1"></i> Edit',
                    name: 'edit',
                    className: 'btn-warning mr-1'
                },
                {
                    text: '<i class="las la-plus mr-1"></i> Add',
                    name: 'add',
                    className: 'btn-info mr-1'
                },
                {
                    text: '<i class="las la-sync mr-1"></i> Synchronize',
                    name: 'refresh',
                    className: 'btn-primary '
                }
            ]
        }, $options);

        return $(this).DataTable(options).on('init.dt', function () {
            $("span[data-role=filter]").off().on("click", function () {
                const search = $(this).data("filter");
                if (table)
                    table.search(search).draw();
            });
        });
    };
}(jQuery));