let hamburger = document.querySelector(".hamburger");
let menu = document.querySelector(".menu");
hamburger.onclick = function () {
  menu.classList.toggle("show");
};
hamburger.addEventListener("click", () => {
  hamburger.classList.toggle("is-active");
});
