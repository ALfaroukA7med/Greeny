// Toggle Password Visibility 
document.addEventListener("click", function (e) {
  if (e.target.classList.contains("toggle-password")) {
    const passwordInput = e.target.previousElementSibling;
    if (passwordInput && passwordInput.tagName === "INPUT") {
      const type = passwordInput.getAttribute("type") === "password" ? "text" : "password";
      passwordInput.setAttribute("type", type);
      e.target.classList.toggle("fa-eye");
      e.target.classList.toggle("fa-eye-slash");
    }
  }
});

// DOM Loaded 
document.addEventListener("DOMContentLoaded", () => {
  const navElement = document.querySelector("nav");
  const layout = document.querySelector(".auth-layout");

  if (navElement && layout) {
    let navHeight = navElement.offsetHeight;
    layout.style.minHeight = `calc(100vh - ${navHeight}px)`;
    document.body.style.paddingTop = `${navHeight}px`;
  }

  const tabSignIn = document.getElementById("tab-signin");
  const tabRegister = document.getElementById("tab-register");
  const submitBtn = document.querySelector(".auth-submit-btn");
  const authForm = document.getElementById("loginForm");
  
  if (authForm) {
    const formSubmitBtn = authForm.querySelector(".auth-submit-btn");

    const confirmPasswordHTML = `
      <div class="input-group confirm-pass-group">
        <label for="confirm-password">Confirm Password</label>
        <div class="password-wrapper">
          <input
            type="password"
            id="confirm-password"
            placeholder="••••••••"
            required
          />
          <i class="fa-regular fa-eye toggle-password"></i>
        </div>
      </div>
    `;

    if (tabSignIn && tabRegister) {
      tabSignIn.addEventListener("click", () => {
        tabSignIn.classList.add("active");
        tabRegister.classList.remove("active");
        submitBtn.textContent = "Sign In";

        const confirmGroup = document.querySelector(".confirm-pass-group");
        if (confirmGroup) {
          confirmGroup.remove();
        }
      });

      tabRegister.addEventListener("click", () => {
        tabRegister.classList.add("active");
        tabSignIn.classList.remove("active");
        submitBtn.textContent = "Register";

        if (!document.querySelector(".confirm-pass-group")) {
          formSubmitBtn.insertAdjacentHTML("beforebegin", confirmPasswordHTML);
        }
      });
    }
  }
});

