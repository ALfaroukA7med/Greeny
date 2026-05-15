const register = document.querySelector('#tab-register');

register.addEventListener('click', function () {
    window.location.href = '/Account/Register';
});



const togglePassword = document.getElementById("togglePassword");
const passwordInput = document.getElementById("Password");

togglePassword.addEventListener("click", function () {

    if (passwordInput.type === "password") {
        passwordInput.type = "text";
    } else {
        passwordInput.type = "password";
    }

    this.classList.toggle("fa-eye");
    this.classList.toggle("fa-eye-slash");
});