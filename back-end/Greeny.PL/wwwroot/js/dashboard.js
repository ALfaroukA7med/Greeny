const notificationBtn = document.getElementById("notificationBtn");
const notificationBox = document.getElementById("notificationBox");
const viewAllBtn = document.getElementById("viewAllBtn");

notificationBtn?.addEventListener("click", (e) => {
    e.stopPropagation();
    notificationBox.classList.toggle("show");
});

document.addEventListener("click", (e) => {
    if (
        !notificationBtn.contains(e.target) &&
        !notificationBox.contains(e.target)
    ) {
        notificationBox.classList.remove("show");
    }
});

viewAllBtn?.addEventListener("click", () => {
    window.location.href = "orders.html";
});

document.querySelectorAll(".sidebar-nav a").forEach(link => {
    link.addEventListener("click", (e) => {
        const href = link.getAttribute("href");

        if (href && href !== "#") {
            window.location.href = href;
        }
    });
});

