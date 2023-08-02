document.addEventListener('DOMContentLoaded', () => {

    const inputName = document.getElementById('input-name');
    const inputCost = document.getElementById('input-cost');
    const inputIdSubject = document.getElementById('input-id-student');

    const inputs = [inputName, inputCost];

    inputName.addEventListener('keyup', () => { fValidatorStyle(inputName.id, inputName.type, 'label-' + inputName.id); });
    inputCost.addEventListener('keyup', () => { fValidatorStyle(inputCost.id, inputCost.type, 'label-' + inputCost.id); });

    fLoadTable = () => {
        $("#table-dataTable").DataTable({
            responsive: true,
            "aaSorting": [],
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Spanish.json"
            },
            buttons: [
                {
                    extend: 'excelHtml5',
                    title: 'Materias'
                },
                {
                    extend: 'csvHtml5',
                    title: 'Materias'
                }
            ]
        });
    }

    fShowData = () => {
        const table = $("#table-dataTable").DataTable();
        table.destroy();
        try {
            $.ajax({
                url: "../Subjects/LoadDataSubjects",
                type: "POST",
                data: {},
                beforeSend: () => {
                    fAlertLoading();
                }, success: (request) => {
                    if (request.Correct == true) {
                        swal.close();
                        if (request.Html != "") {
                            document.getElementById('body-data').innerHTML = request.Html;
                            setTimeout(() => {
                                fLoadTable();
                            }, 500);
                        } else {
                            fLoadTable();
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
        inputIdSubject.value = "";
        document.getElementById('label-modal').textContent = "Registrar Materia";
        fClearInputs(inputs);
        document.getElementById('container-option').innerHTML = ButtonSave();
        $("#modal-subjects").modal("show");
    }

    fDataSend = () => {
        const dataSend = {
            iIdMateria: inputIdSubject.value,
            sNombre: inputName.value,
            dCosto: inputCost.value,
        };
        return dataSend;
    }

    fSaveData = () => {
        try {
            const validate = fValidationsInputs(inputs);
            if (validate) {
                $.ajax({
                    url: '../Subjects/SaveData',
                    type: 'POST',
                    data: fDataSend(),
                    beforeSend: () => {
                        fAlertLoading();
                    }, success: (request) => {
                        if (request.Correct == true && request.Error == 'none') {
                            $("#modal-subjects").modal("hide");
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

    fShowDataById = (idSubject) => {
        try {
            $.ajax({
                url: "../Subjects/ShowDataById",
                type: "POST",
                data: { idSubject: parseInt(idSubject) },
                beforeSend: () => {
                    fAlertLoading();
                }, success: (request) => {
                    if (request.Correct) {
                        swal.close();
                        $("#button-save").remove();
                        $("#modal-subjects").modal("show");
                        document.getElementById('label-modal').textContent = "Editar Materia";
                        document.getElementById('container-option').innerHTML = ButtonEdit();

                        inputIdSubject.value = idSubject;
                        inputName.value      = request.Data.sNombre;
                        inputCost.value      = request.Data.dCosto;

                        inputName.style.borderWidth = '2px';
                        inputName.style.borderColor = 'green';
                        inputCost.style.borderWidth = '2px';
                        inputCost.style.borderColor = 'green';

                    } else {
                        fDynamicAlertsValidations('Atención', request.Error, 'error', 3000, null);
                    }
                }, error: (exception) => {
                    fDynamicAlertsValidations('Error!', exception, 'error', 4000, null);
                }
            });

        } catch (error) {
            fDynamicAlertsValidations('Error!', error, 'error', 4000, null);
        }
    }

    fUpdateData = () => {
        try {
            const validate = fValidationsInputs(inputs);
            if (validate) {
                $.ajax({
                    url: '../Subjects/UpdateData',
                    type: 'POST',
                    data: fDataSend(),
                    beforeSend: () => {
                        fAlertLoading();
                    }, success: (request) => {
                        if (request.Correct == true && request.Error == 'none') {
                            $("#modal-subjects").modal("hide");
                            swal.close();
                            fAlertSuccessEdit();
                            fClearInputs(inputs);
                            setTimeout(() => {
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

    fActiveData = (idSubject, status) => {
        try {
            if (parseInt(idSubject) > 0) {
                $.ajax({
                    url: "../Subjects/ActiveInactiveData",
                    type: "POST",
                    data: { idSubject: parseInt(idSubject), status: parseInt(status) },
                    beforeSend: () => {
                        fAlertLoading();
                    }, success: (request) => {
                        if (request.Correct) {
                            swal.close();
                            fDynamicAlertsValidations('Activo', 'La Materia ha sido dado de alta correctamente', 'success', 2000, null);
                            setTimeout(() => {
                                fShowData();
                            }, 2000);
                        } else {
                            fDynamicAlertsValidations('Error!', request.Error, 'error', 3000, null);
                        }
                    }, error: (exception) => {
                        fDynamicAlertsValidations('Error!', exception, 'error', 4000, null);
                    }
                });
            } else {
                fDynamicAlertsValidations('Error', 'Accion inválida', 'error', 2000, null);
                location.reload();
            }
        } catch (error) {
            fDynamicAlertsValidations('Error!', error, 'error', 4000, null);
        }
    }

    fInactiveData = (idSubject, status) => {
        try {
            if (parseInt(idSubject) > 0) {
                $.ajax({
                    url: "../Subjects/ActiveInactiveData",
                    type: "POST",
                    data: { idSubject: parseInt(idSubject), status: parseInt(status) },
                    beforeSend: () => {
                        fAlertLoading();
                    }, success: (request) => {
                        if (request.Correct) {
                            swal.close();
                            fDynamicAlertsValidations('Inactiva', 'La Materia ha sido dado de baja correctamente', 'success', 2000, null);
                            setTimeout(() => {
                                fShowData();
                            }, 2000);
                        } else {
                            fDynamicAlertsValidations('Error!', request.Error, 'error', 3000, null);
                        }
                    }, error: (exception) => {
                        fDynamicAlertsValidations('Error!', exception, 'error', 4000, null);
                    }
                });
            } else {
                fDynamicAlertsValidations('Error', 'Accion inválida', 'error', 2000, null);
                location.reload();
            }
        } catch (error) {
            fDynamicAlertsValidations('Error!', error, 'error', 4000, null);
        }
    }

});