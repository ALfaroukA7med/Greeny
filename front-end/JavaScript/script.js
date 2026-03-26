// hamburger
let hamburger = document.querySelector(".hamburger");
let menu = document.querySelector(".menu");
let nav = document.querySelector("nav").offsetHeight;
let hero = document.querySelector(".hero-section");
document.body.style.paddingTop = `${nav}px`; // => to make the menu down the navbar
hamburger.onclick = (_) => {
  menu.classList.toggle("show");
};
hamburger.addEventListener("click", (_) => {
  hamburger.classList.toggle("is-active");
});
// currrent opened page
let currentUrl = window.location.href;
let links = document.querySelectorAll(".middle-nav a");
let mobLinks = document.querySelectorAll(".menu a");
let curPageLink = function (link) {
  link.classList.remove("active-link");
  console.log("hi" + " " + currentUrl.substring(currentUrl.length - 8));
  if (link.href === currentUrl) {
    if (!currentUrl.includes("log.html")) link.classList.add("active-link");
  }
};
links.forEach(curPageLink);
mobLinks.forEach(curPageLink);

// Scroll to Top Button Logic
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

document.addEventListener("DOMContentLoaded", () => {
  const tabRegister = document.getElementById("tab-register");
  const urlParams = new URLSearchParams(window.location.search);
  const requestedTab = urlParams.get("tab");
  if (requestedTab === "register") {
    tabRegister.click();
  }
});
