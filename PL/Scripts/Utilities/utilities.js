
fValidatorStyle = (id, type, label) => {
    const input = document.getElementById(id);
    const labelId = document.getElementById(label);

    switch (type) {
        case 'text':
            regex = /^[A-Za-zÁÉÍÓÚáéíóúñÑ ]+$/g;
            textLabel = 'Este campo solo puede contener letras';
            break;
        case 'number':
            regex = /^[0-9]+$/;
            textLabel = 'Este campo solo puede contener números';
            break;
        case 'alphanumeric':
            regex = /^[A-Za-zÁÉÍÓÚáéíóúñÑ0-9\s]+$/g;
            textLabel = 'Este campo solo puede contener números y letras';
            break;
    }

    input.style.borderWidth = '2px';

    if (input.value == '') {
        input.style.borderColor = '#4983d9';
        labelId.innerText = '';
        labelId.classList.add('d-none');
    } else if (input.value.match(regex)) {
        input.style.borderColor = 'green';
        labelId.innerText = '';
        labelId.classList.add('d-none');
    } else {
        input.style.borderColor = 'red';
        labelId.innerText = textLabel;
        labelId.style = 'color:red !important;';
        labelId.classList.remove('d-none');
        input.focus();
    }
}

fValidationsInputs = (listInputs) => {
    let validate = true;
    const dataLength = listInputs.length;
    for (let i = 0; i < dataLength; i++) {
        const attribute = listInputs[i].getAttribute('data-placeholder');
        if (listInputs[i].value.trim() == "") {
            fDynamicAlertsValidations('Atención!', 'Completa el campo: ' + attribute, 'warning', 2500, listInputs[i]);
            validate = false;
            setTimeout(() => {
                listInputs[i].focus();
            }, 2000);
            break;
        }
        if (listInputs[i].type == 'text') {
            const regExp = /^[A-Za-zÁÉÍÓÚáéíóúñÑ ]+$/g;
            const validationText = regExp.test(listInputs[i].value);
            if (!validationText) {
                fDynamicAlertsValidations('Atención!', 'El campo solo debe contener letras', 'warning', 2500, listInputs[i]);
                validate = false;
                break;
            }
        }
        if (listInputs[i].getAttribute('minlength') != null) {
            const ilength = listInputs[i].getAttribute('minlength');
            if (listInputs[i].value.length < ilength) {
                fDynamicAlertsValidations('Atención!', attribute + ' no cumple con la longitud mínima: ' + ilength, 'warning', 2500, listInputs[i]);
                validate = false;
                break;
            }
        }
        if (listInputs[i].getAttribute('maxlength') != null) {
            const ilength = listInputs[i].getAttribute('maxlength');
            if (listInputs[i].value.length > ilength) {
                fDynamicAlertsValidations('Atención!', attribute + ' excede la longitud máxima: ' + ilength, 'warning', 2500, listInputs[i]);
                validate = false;
                break;
            }
        }
        
    }
    return validate;
}


fClearInputs = (listInputs) => {
    const dataLength = listInputs.length;
    for (let i = 0; i < dataLength; i++) {
        if (listInputs[i].localName == 'input') {
            listInputs[i].value = '';
            listInputs[i].style.borderColor = '#ced4da';
            listInputs[i].style.borderWidth = '1px';
        }
    }
}

// TITULO, TEXTO, ICONO, TIEMPO DE CIERRE DE MODAL, ELEMENTO AL REALIZAR FOCUS
fDynamicAlertsValidations = (title, text, icon, time, element = null) => {
    Swal.fire({
        title: title,
        text: text,
        icon: icon,
        showConfirmButton: false,
        showClass: {
            popup: 'animate__animated animate__fadeInDown animate__fast'
        },
        hideClass: {
            popup: 'animate__animated animate__fadeOutUp animate__fast'
        },
        timer: time
    });
    if (element != null) {
        setTimeout(() => {
            element.focus();
        }, (time + 2000));
    }
}

fAlertLoading = () => {
    Swal.fire({
        position: 'top-end',
        html: '<div class="spinner-border text-primary" role="status" style="width: 5rem;height: 5rem;margin-top: 1rem; margin-bottom: 36px;"></div><h3>Cargando</h3>',
        showConfirmButton: false,
        allowOutsideClick: false,
        allowEscapeKey: false
    });
}

fAlertSuccessEdit = (time = 2000) => {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: 'Registro Actualizado!',
        showConfirmButton: false,
        timer: time,
        allowOutsideClick: false,
        allowEscapeKey: false
    });
}

fAlertSuccessSave = (time = 2000) => {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: 'Registro Correcto!',
        showConfirmButton: false,
        timer: time,
        allowOutsideClick: false,
        allowEscapeKey: false
    });
}

ButtonSave = () => {
    return `
        <button class='btn btn-sm btn-success' onclick='fSaveData();'> <i class='fas fa-plus-circle mr-2'></i> Guardar </button>
    `;
}

ButtonEdit = () => {
    return `
        <button class='btn btn-sm btn-success' onclick='fSaveData();'> <i class='fas fa-plus-circle mr-2'></i> Guardar </button>
    `;
}