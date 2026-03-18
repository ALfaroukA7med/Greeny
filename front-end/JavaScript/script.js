let hamburger = document.querySelector(".hamburger");
let menu = document.querySelector(".menu");
let nav = document.querySelector("nav").offsetHeight;
// hero.style.cssText = `min-height: calc(100vh - ${nav}px);`;
menu.style.top = `${nav}px`;
document.body.style.paddingTop = `${nav}px`; // => to make the menu down the navbar
hamburger.onclick = (_) => {
  menu.classList.toggle("show");
  if (menu.classList.contains("show"))
    document.body.style.paddingTop = `${nav + menu.offsetHeight}px`; // => to make the menu down the navbar
  else document.body.style.paddingTop = `${nav}px`; // => to make the menu down the navbar
};
hamburger.addEventListener("click", (_) => {
  hamburger.classList.toggle("is-active");
});
