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