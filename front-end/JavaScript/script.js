let hamburger = document.querySelector(".hamburger");
let menu = document.querySelector(".menu");
let nav = document.querySelector("nav").offsetHeight;
let hero = document.querySelector('.hero-section');
hero.style.cssText = `min-height: calc(100vh - ${nav}px);`;
document.body.style.paddingTop = `${nav}px`; // => to make the menu down the navbar
hamburger.onclick = (_) => {
  menu.classList.toggle("show");
};
hamburger.addEventListener("click", (_) => {
  hamburger.classList.toggle("is-active");
});
