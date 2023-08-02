document.addEventListener('DOMContentLoaded', () => {

    fLoadTable = () => {
        $("#table-dataTable").DataTable({
            responsive: true,
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Spanish.json"
            },
            buttons: [
                {
                    extend: 'excelHtml5',
                    title: 'Alumnos'
                },
                {
                    extend: 'csvHtml5',
                    title: 'Alumnos'
                }
            ]
        });
    }

    fShowData = () => {
        try {
            $.ajax({
                url: "../Students/LoadDataStudents",
                type: "POST",
                data: {},
                beforeSend: () => {
                }, success: (request) => {
                    if (request.Correct = true) {
                        if (request.Html != "") {
                            document.getElementById('body-data').innerHTML = request.Html;
                            setTimeout(() => {
                                fLoadTable();
                            }, 500);
                        }
                    } else {
                        //Alerta
                    }
                   
                }, error: (exception) => {
                    //Alerta
                }
            });
        } catch (error) {
            if (error instanceof EvalError) {
                console.error('EvalError: ', error.message);
            } else if (error instanceof RangeError) {
                console.error('RangeError: ', error.message);
            } else if (error instanceof TypeError) {
                console.error('TypeError: ', error.message);
            } else {
                console.error('Error: ', error);
            }
        }
    }
    fShowData();

});