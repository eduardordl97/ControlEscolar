document.addEventListener('DOMContentLoaded', () => {

    const InputSubject = document.getElementById("input-subject");
    const InputIdStudent = document.getElementById("input-id-student");

    const contentSubjects = document.getElementById('content-cards');

    let total = 0;

    fAddSubject = () => {
        const id = fValueDataList(InputSubject);
        const attribute = InputSubject.getAttribute('list');
        const cost = document.querySelector('#' + attribute + ' option[value="' + InputSubject.value.trim() + '"]').dataset.cost;
        contentSubjects.innerHTML += `<div class="col-md-12 mb-4 subjects-id" id="${id}">
                                             <div class="card border-left-primary shadow h-100 py-2">
                                                <div class="card-body">
                                                    <div class="row no-gutters align-items-center">
                                                        <div class="col mr-2"  title="Seleccionar para ver detalles">
                                                            <div class="col-md-12 h5 font-weight-bold text-uppercase mb-2"><i class="fa fa-book me-2"></i><span class="h5 mb-0 font-weight-bold text-gray-800">${InputSubject.value}</span></div>
                                                            <div class="col-md-12 mb-1">Costo: $ <span  class="mb-0 cost">${parseFloat(cost).toFixed(2)}</span></div>
                                                        </div>
                                                        <div class="col-auto">
                                                            <button class="btn btn-sm btn-danger animate__animated animate__fadeInRight mr-1" data-action="" onclick="fClearSubject('${InputSubject.value}',${id},${cost});" id="button-inactivedata" title="Desctivar"><i class="fas fa-times-circle"></i></button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

            `
        var elementos = document.getElementsByClassName("subjects-id")
        if (elementos.length > 0) {
            total += parseFloat(cost);
            document.getElementById("span-total").innerText =  parseFloat(total).toFixed(2);
            document.getElementById("container-img").classList.add("d-none")
            document.getElementById("content-cards").classList.remove("d-none")
            document.getElementById('content-savedata').innerHTML = `<button class="btn btn-sm btn-success animate__animated animate__fadeInRight me-2" onclick="fSaveSubjects();" id="button-save"> Inscribir </button>`
        }
        $("#option-" + id + "").remove();
        InputSubject.value = "";
    }

    fClearSubject = (value, id, cost) => {
        var option = `<option id="option-${id}" data-cost="${cost}" data-value="${id}" value="${value}""></option>`
        document.getElementById('dataListSubjects').innerHTML += option;
        $("#" + id + "").remove();
        var elementos = document.getElementsByClassName("subjects-id")
        total = total - parseFloat(cost);
        document.getElementById("span-total").innerText = parseFloat(total).toFixed(2);
        if (elementos.length < 1) {
            document.getElementById("span-total").innerText = parseFloat(0).toFixed(2);
            document.getElementById("content-cards").classList.add("d-none")
            document.getElementById("container-img").classList.remove("d-none")
            document.getElementById('content-savedata').innerHTML = "";
        }
    }

    fSelectSubject = () => {
        document.getElementById('container-data-save').classList.add("d-none");
        document.getElementById('container-data').classList.remove("d-none");
        const validate = fCheckValidValueDataList('dataListSubjects', InputSubject);
        if (validate) {
            fAddSubject();
        }
    }

    fShowResumeSubjects = () => {
        var elementos = document.getElementsByClassName("subjects-id")
        var array = []
        for (var i = 0; i < elementos.length; i++) {
            array.push(elementos[i].id);
        }
        return array
    }

    fSendArray = () => {
        var elementos = document.getElementsByClassName("subjects-id")
        var array = []
        for (var i = 0; i < elementos.length; i++) {
            array.push(elementos[i].id);
        }
        return array
    }

    fLoadInfoSubjects = (element) => {
        document.getElementById(element).innerHTML = '';
        try {

            $.ajax({
                url: "../SubjectsStudent/LoadDataSubjectsStudent",
                type: "POST",
                data: { idStudent: parseInt(InputIdStudent.value)},
                beforeSend: () => {
                }, success: (request) => {
               
                        if (request.Correct == true && request.Error == 'none') {
                            const data = request.Data;
                            const dataLength = request.Data.length;
                            if (dataLength > 0) {
                                Object.entries(data).forEach(([key, value]) => {
                                    document.getElementById(element).innerHTML += `<option class="options-all" id="option-${value.iIdMateria}" data-cost="${value.dCosto}" data-value="${value.iIdMateria}" value="[${value.iIdMateria}] - ${value.sNombre}"  />`;
                                });
                                    
                            } else {
                                fDynamicAlertsValidations('Atención!', 'No existen Materias disponibles para ti', 'warning', 3000, null);
                            }
                        } else {
                            fDynamicAlertsValidations('Error!', 'Ocurrio un fallo interno en la aplicación', 'error', 2000, null);
                        }

                }, error: (exception) => {
                    fDynamicAlertsValidations('Error!', exception, 'error', 4000, null);

                }
            });
        } catch (error) {
            fDynamicAlertsValidations('Error!', error, 'error', 4000, null);
        }
    }

    fLoadInfoSubjects('dataListSubjects');

    fSaveSubjects = () => {
        try {

            const dataSend = fSendArray()
            $.ajax({
                url: "../",
                type: "POST",
                data: { Subjects: dataSend },
                beforeSend: () => {
                    fAlertLoading();
                }, success: (request) => {
                   
                    if (request.Correct == true && request.Error == 'none') {
                        //document.getElementById('container-data-save').innerHTML = request.Cards;
                        //document.getElementById('container-data').classList.add("d-none");
                        //document.getElementById('container-data-save').classList.remove("d-none");
                        //document.getElementById("content-cards").classList.add("d-none")
                        contentSubjects.innerHTML = "";
                        swal.close();
    
                        document.getElementById('button-save').classList.add('d-none');
                    } else {
                        fDynamicAlertsValidations('Error!', 'Ocurrio un fallo interno en la aplicación', 'error', 2000, null);
                    }
                    
                }, error: (exception) => {
                    
                }
            });
          
        } catch (error) {
            
        }
    }




});