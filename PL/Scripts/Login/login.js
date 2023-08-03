document.addEventListener('DOMContentLoaded', () => {

    const inputName     = document.getElementById('input-name');
    const inputLastName = document.getElementById('input-last-name');

    fDataSend = () => {
        const dataSend = {
            sNombre: inputName.value,
            sApellidoPaterno: inputLastName.value,
        };
        return dataSend;
    }

    fSignIn = () => {
        try {
            if (inputName.value != "") {
                if (inputLastName.value != "") {
                    $.ajax({
                        url: "../Login/SignIn",
                        type: "POST",
                        data: fDataSend(),
                        beforeSend: () => {

                        }, success: (request) => {
                            if (request.Correct === true && request.Error === 'none') {
                                //sessionStorage.setItem("userActive", 1);
                                fDynamicAlertsValidations('Inicio de Sesión', 'Se ha iniciado sesión correctamente', 'success', 3000, null);
                                //const data = request.Data;

                                //localStorage.setItem('Data', datas);

                                //id = request.KeyUser;

                                //localStorage.setItem('Id', id);

                                setTimeout(() => {

                                    location.href = "/Home/Index";

                                }, 3000);
                                


                            } else {

                                //sessionStorage.setItem("userActive", 0);

                                fDynamicAlertsValidations('Atención', request.Error, 'info', 1500, null);

                            }

                        }, error: (jqXHR, exception) => {

                            console.error(jqXHR);

                            console.error(exception);

                        }

                    });

                } else {
                    fDynamicAlertsValidations('Atención!', 'Ingresa tu contraseña', 'info', 2000, inputPassword);
                }
            } else {
                fDynamicAlertsValidations('Atención!', 'Ingresa tu correo', 'info', 2000, inputEmail);
            }
        } catch (error) {
            console.log(error);

        }
    };
});