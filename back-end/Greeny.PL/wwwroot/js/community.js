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
<script>
    async function handleVote(postId, voteType) {
    // 1. Instantly target the elements for this specific post
    const scoreElement = document.getElementById(`score-${postId}`);
    const card = scoreElement?.closest('.comm-card');
    if (!scoreElement || !card) return;

    const upvoteBtn = card.querySelector('.vote-btn.upvote');
    const downvoteBtn = card.querySelector('.vote-btn.downvote');

    try {
        // 2. Dispatch background request to matching API route: /api/vote/upvote/{id}
        const response = await fetch(`/api/vote/${voteType}/${postId}`, {
        method: 'POST',
    headers: {
        'Content-Type': 'application/json'
            }
        });

    // Handle expired login session cleanly
    if (response.status === 401) {
        alert("You must be logged in to vote!");
    window.location.href = '/Identity/Account/Login';
    return;
        }

    if (response.ok) {
            const data = await response.json();

    // 3. LIVE UPDATE: Inject the new score number received from the database
    scoreElement.innerText = data.updatedScore;

    // 4. PERSIST VISUAL FEEDBACK: Style arrows according to action states
    if (voteType === 'upvote') {
        toggleArrowState(upvoteBtn, downvoteBtn, '#ff4500', '#878a8c');
            } else if (voteType === 'downvote') {
        toggleArrowState(downvoteBtn, upvoteBtn, '#7193ff', '#878a8c');
            }
        } else {
        console.error("Voting submission rejected by backend service logic.");
        }
    } catch (error) {
        console.error("Network interface connection failure:", error);
    }
}

    // Helper utility to lock UI state changes cleanly
    function toggleArrowState(activeBtn, inactiveBtn, activeColor, defaultColor) {
    const isAlreadyActive = activeBtn.style.color === rgbToHex(activeColor) || activeBtn.dataset.active === "true";

    if (isAlreadyActive) {
        // Dismiss action completed successfully
        activeBtn.style.color = defaultColor;
    activeBtn.dataset.active = "false";
    activeBtn.setAttribute('onmouseout', `this.style.color='${defaultColor}'`);
    } else {
        // Activating state
        activeBtn.style.color = activeColor;
    activeBtn.dataset.active = "true";
    activeBtn.setAttribute('onmouseout', `this.style.color='${activeColor}'`);

    // Turn off opposite arrow 
    inactiveBtn.style.color = defaultColor;
    inactiveBtn.dataset.active = "false";
    inactiveBtn.setAttribute('onmouseout', `this.style.color='${defaultColor}'`);
    }
}

    // Simple helper to catch color property parsing edge cases
    function rgbToHex(color) {
    if (color === '#ff4500') return 'rgb(255, 69, 0)';
    if (color === '#7193ff') return 'rgb(113, 147, 255)';
    return color;
}
</script>

const upload = document.getElementById("photoUpload");
const fileName = document.getElementById("fileName");

if (upload && fileName) {
    upload.addEventListener("change", function () {
        if (this.files.length > 0) {
            fileName.textContent = this.files[0].name;
        } else {
            fileName.textContent = "No file chosen";
        }
    });
}
