document.addEventListener('DOMContentLoaded', () => {

    const inputName             = document.getElementById('input-name');
    const inputLastName         = document.getElementById('input-last-name');
    const inputSecondLastName   = document.getElementById('input-second-last-name');
    const inputIdStudent        = document.getElementById('input-id-student');


    const inputs = [inputName, inputLastName, inputSecondLastName];

    inputName.addEventListener('keyup', () => { fValidatorStyle(inputName.id, inputName.type, 'label-' + inputName.id); });
    inputLastName.addEventListener('keyup', () => { fValidatorStyle(inputLastName.id, inputLastName.type, 'label-' + inputLastName.id); });
    inputSecondLastName.addEventListener('keyup', () => { fValidatorStyle(inputSecondLastName.id, inputSecondLastName.type, 'label-' + inputSecondLastName.id); });

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
        const table = $("#table-dataTable").DataTable();
        table.destroy();
        try {
            $.ajax({
                url: "../Students/LoadDataStudents",
                type: "POST",
                data: {},
                beforeSend: () => {
                }, success: (request) => {
                    if (request.Correct == true) {
                        if (request.Html != "") {
                            document.getElementById('body-data').innerHTML = request.Html;
                            setTimeout(() => {
                                fLoadTable();
                            }, 500);
                        }
                    } else {
                        fDynamicAlertsValidations('Error!', request.Error, 'error', 3000, null);
                    }
                   
                }, error: (exception) => {
                    fDynamicAlertsValidations('Error!', exception, 'error', 4000, null);
                }
            });
        } catch (error) {
            fDynamicAlertsValidations('Error!', error, 'error', 4000, null);
        }
    }
    fShowData();

    fShowModal = () => {
        inputIdStudent.value = "";
        fClearInputs(inputs);
        document.getElementById('container-option').innerHTML = ButtonSave();
        $("#modal-students").modal("show");
    }

    fDataSend = () => {
        const dataSend = {
            iIdAlumno: inputIdStudent.value,
            sNombre: inputName.value,
            sApellidoPaterno: inputLastName.value,
            sApellidoMaterno: inputLastName.value
        };
        return dataSend;
    }

    fSaveData = () => {
        try {
            const validate = fValidationsInputs(inputs);
            if (validate) {
                $.ajax({
                    url: '../Students/SaveData',
                    type: 'POST',
                    data: fDataSend(),
                    beforeSend: () => {
                        fAlertLoading();
                    }, success: (request) => {
                        if (request.Correct == true && request.Error == 'none') {
                            $("#modal-students").modal("hide");
                            swal.close();
                            fAlertSuccessSave();
                            setTimeout(() => {
                                fClearInputs(inputs);
                                fShowData();
                            }, 1000);
                        } else {
                            fDynamicAlertsValidations('Error!', request.Error, 'error', 3000, null);
                        }
                    }, error: (exception) => {
                        fDynamicAlertsValidations('Error!', exception, 'error', 4000, null);
                    }
                });
            }
        } catch (error) {
            fDynamicAlertsValidations('Error!', error, 'error', 4000, null);
        }
    }

    

});