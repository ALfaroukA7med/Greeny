// products adding
const addButtons = document.querySelectorAll(".add-btn");
const cartBadge = document.querySelector(".cart-badge");
addButtons.forEach((btn) => {
  btn.addEventListener("click", function () {
    cartBadge.innerHTML++;
    cartBadge.classList.add("bump");
    setTimeout(() => {
      cartBadge.classList.remove("bump");
    }, 200);
  });
});