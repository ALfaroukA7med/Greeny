document.addEventListener("click", function (e) {
  if (e.target.classList.contains("toggle-password")) {
    const passwordInput = e.target.previousElementSibling;
    if (passwordInput && passwordInput.tagName === "INPUT") {
      const type =
        passwordInput.getAttribute("type") === "password" ? "text" : "password";
      passwordInput.setAttribute("type", type);
      e.target.classList.toggle("fa-eye");
      e.target.classList.toggle("fa-eye-slash");
    }
  }
});

document.addEventListener("DOMContentLoaded", () => {
  let tabSignIn = document.querySelector("#tab-signin");
  let tabRegister = document.querySelector("#tab-register");
  let submitBtn = document.querySelector(".auth-submit-btn");
  let fullName = document.querySelector(".fullName");
  let confirmPassword = document.querySelector(".confirm-pass-group");

  tabSignIn.addEventListener("click", () => {
    tabSignIn.classList.add("active");
    tabRegister.classList.remove("active");
    submitBtn.textContent = "Sign In";
    confirmPassword.classList.add("hide");
    fullName.classList.add("hide");
  });

  tabRegister.addEventListener("click", () => {
    tabRegister.classList.add("active");
    tabSignIn.classList.remove("active");
    submitBtn.textContent = "Register";
    confirmPassword.classList.remove("hide");
    fullName.classList.remove("hide");
  });
  let urlParams = new URLSearchParams(window.location.search);
  let requestedTab = urlParams.get("tab");
  if (requestedTab === "register") {
    tabRegister.click();
  }
});
document.addEventListener("DOMContentLoaded", () => {
  const authForm = document.getElementById("loginForm");

  if (authForm) {
    authForm.addEventListener("submit", function (e) {
      e.preventDefault();

      document.querySelectorAll(".error-message").forEach((el) => el.remove());
      document
        .querySelectorAll(".error-input")
        .forEach((el) => el.classList.remove("error-input"));

      const emailInput = document.getElementById("email");
      const passwordInput = document.getElementById("password");
      const confirmPasswordInput = document.getElementById("confirm-password");

      let isValid = true;

      const showError = (inputElement, message) => {
        isValid = false;
        inputElement.classList.add("error-input");

        const errorSpan = document.createElement("span");
        errorSpan.className = "error-message";
        errorSpan.textContent = message;

        if (inputElement.parentElement.classList.contains("password-wrapper")) {
          inputElement.parentElement.parentElement.appendChild(errorSpan);
        } else {
          inputElement.parentElement.appendChild(errorSpan);
        }
      };

      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

      if (!emailInput.value || !emailRegex.test(emailInput.value)) {
        showError(emailInput, "Please enter a valid email address");
      }

      if (!passwordInput.value || passwordInput.value.length < 8) {
        showError(passwordInput, "Password must be at least 8 characters long");
      }

      if (confirmPasswordInput) {
        if (passwordInput.value !== confirmPasswordInput.value) {
          showError(confirmPasswordInput, "Passwords do not match");
        }
      }

      if (isValid) {
        window.location.href = "index.html";
      }
    });
  }
});
