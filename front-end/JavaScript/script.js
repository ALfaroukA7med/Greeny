let hamburger = document.querySelector(".hamburger");
let menu = document.querySelector(".menu");
let nav = document.querySelector("nav").offsetHeight;
let hero = document.querySelector(".hero-section");
document.body.style.paddingTop = `${nav}px`;
hamburger.onclick = (_) => {
  menu.classList.toggle("show");
};
hamburger.addEventListener("click", (_) => {
  hamburger.classList.toggle("is-active");
});
let currentUrl = window.location.href;
let links = document.querySelectorAll(".middle-nav a");
let mobLinks = document.querySelectorAll(".menu a");
let curPageLink = function (link) {
  link.classList.remove("active-link");
  if (link.href === currentUrl) {
    if (!currentUrl.includes("log.html")) link.classList.add("active-link");
  }
};
links.forEach(curPageLink);
mobLinks.forEach(curPageLink);

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
const cards = document.querySelectorAll(".product-title");
cards.forEach((card) => {
  card.addEventListener("click", function () {
    window.open("/html/product.html", self);
  });
});
const comUsers = document.querySelectorAll(".comm-user-info");
comUsers.forEach((user) => {
  user.addEventListener("click", function () {
    window.open("/html/profile.html", self);
  });
});
const avatars = document.querySelectorAll(".comm-avatar");
avatars.forEach((avatar) => {
  avatar.addEventListener("click", function () {
    window.open("/html/profile.html", self);
  });
});

let comments = document.querySelectorAll(".fa-comment");
comments.forEach((comment) => {
  comment.addEventListener("click", function () {
      const cardBody = this.closest(".comm-card-body");
      let commentsSection = cardBody.querySelector(".comm-comments-section");
      commentsSection.classList.toggle('show');
  });
});