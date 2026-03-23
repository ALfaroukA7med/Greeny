let hamburger = document.querySelector(".hamburger");
let menu = document.querySelector(".menu");
let nav = document.querySelector("nav").offsetHeight;
let hero = document.querySelector(".hero-section");
hero.style.cssText = `min-height: calc(100vh - ${nav}px);`;
document.body.style.paddingTop = `${nav}px`; // => to make the menu down the navbar
hamburger.onclick = (_) => {
  menu.classList.toggle("show");
};
hamburger.addEventListener("click", (_) => {
  hamburger.classList.toggle("is-active");
});

const addButtons = document.querySelectorAll(".add-btn");
const cartBadge = document.querySelector(".cart-badge");
let totalItems = 0;

addButtons.forEach((btn) => {
  const originalHTML = btn.innerHTML;

  btn.addEventListener("click", function () {
    if (this.classList.contains("is-adding")) return;
    this.classList.add("is-adding");

    totalItems++;
    cartBadge.textContent = totalItems;

    cartBadge.classList.add("bump");
    setTimeout(() => {
      cartBadge.classList.remove("bump");
    }, 200);

    this.innerHTML = '<i class="fa-solid fa-check"></i> Added';
    this.style.backgroundColor = "var(--dark-green)";
    this.style.color = "white";
    this.style.cursor = "default";

    setTimeout(() => {
      this.innerHTML = originalHTML;
      this.style.backgroundColor = "";
      this.style.color = "";
      this.style.cursor = "pointer";
      this.classList.remove("is-adding");
    }, 2000);
  });
});

const likeIcons = document.querySelectorAll(".comm-action i.fa-heart");

likeIcons.forEach((icon) => {
  const actionBtn = icon.parentElement;
  actionBtn.addEventListener("click", function () {
    let countNode = this.lastChild;
    let currentCount = parseInt(countNode.textContent.trim());
    if (icon.classList.contains("fa-solid")) {
      icon.classList.replace("fa-solid", "fa-regular");
      icon.style.color = "";
      currentCount--;
    } else {
      icon.classList.replace("fa-regular", "fa-solid");
      icon.style.color = "#e11d48";
      currentCount++;
    }
    countNode.textContent = ` ${currentCount}`;
  });
});
