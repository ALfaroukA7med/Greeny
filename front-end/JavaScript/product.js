const pdMinusBtn = document.getElementById("pd-minus");
const pdPlusBtn = document.getElementById("pd-plus");
const pdQtyValue = document.getElementById("pd-qty");
const pdWishlistBtn = document.getElementById("pd-wishlist");
const productAddButtons = document.querySelectorAll(".btn-add-to-cart");
if (pdMinusBtn && pdPlusBtn && pdQtyValue) {
  let currentQty = parseInt(pdQtyValue.textContent);

  pdPlusBtn.addEventListener("click", () => {
    currentQty++;
    pdQtyValue.textContent = currentQty;
  });

  pdMinusBtn.addEventListener("click", () => {
    if (currentQty > 1) {
      currentQty--;
      pdQtyValue.textContent = currentQty;
    }
  });
}
let Quantity = document.querySelector(".qty-value");
productAddButtons.forEach((btn) => {
  btn.addEventListener("click", function () {
    cartBadge.innerHTML = `${+cartBadge.innerHTML + +Quantity.innerHTML}`;
    cartBadge.classList.add("bump");
    setTimeout(() => {
      cartBadge.classList.remove("bump");
    }, 200);
  });
});
document.addEventListener("DOMContentLoaded", () => {
  const starInputs = document.querySelectorAll(".star-rating-input i");
  
  starInputs.forEach(star => {
    star.addEventListener("click", function() {
      const rating = this.getAttribute("data-rating");
      
      starInputs.forEach(s => {
        s.classList.remove("fa-solid", "active");
        s.classList.add("fa-regular");
        
        if (s.getAttribute("data-rating") <= rating) {
          s.classList.remove("fa-regular");
          s.classList.add("fa-solid", "active");
        }
      });
    });
  });
});