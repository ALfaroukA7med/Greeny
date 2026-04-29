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

const upload = document.getElementById("photoUpload");
const fileName = document.getElementById("fileName");

upload.addEventListener("change", function () {
  if (this.files.length > 0) {
    fileName.textContent = this.files[0].name;
  } else {
    fileName.textContent = "No file chosen";
  }
});