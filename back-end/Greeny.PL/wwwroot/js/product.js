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
