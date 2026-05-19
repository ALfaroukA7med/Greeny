document.addEventListener("DOMContentLoaded", () => {
  const paymentMethods = document.querySelectorAll(".payment-method");
  const checkoutForm = document.getElementById("checkout-form");
  const successModal = document.getElementById("order-success-modal");

  paymentMethods.forEach(method => {
    method.addEventListener("click", function() {
      paymentMethods.forEach(m => m.classList.remove("active"));
      this.classList.add("active");
      const radioBtn = this.querySelector('input[type="radio"]');
      if(radioBtn) radioBtn.checked = true;
    });
  });

  if (checkoutForm) {
    checkoutForm.addEventListener("submit", function(e) {
      e.preventDefault();
      
      if(successModal) {
        successModal.classList.add("show");
        document.body.style.overflow = "hidden";
      }
    });
  }
});