// Toggle Password Visibility
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

// DOM Loaded
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
// scroll to top
const scrollTopBtn = document.getElementById("scrollTopBtn");
if (scrollTopBtn) {
  window.addEventListener("scroll", () => {
    if (window.scrollY > 300) {
      scrollTopBtn.classList.add("show-btn");
    } else {
      scrollTopBtn.classList.remove("show-btn");
    }
  });
  scrollTopBtn.addEventListener("click", () => {
    window.scrollTo({ top: 0 });
  });
}