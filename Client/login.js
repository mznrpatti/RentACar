async function login() {
    var username = document.getElementById('typeUsername').value;
    var password = document.getElementById('typePassword').value;
    if (isEmpty(username) || isEmpty(password)) {
        alert('Type username and password!');
    } else {
        var data = {
            username: username,
            password: password
        };
        try {
            const response = await postData("auth/login", data, false);
            if (response.username) {
                localStorage.setItem("username", response.username); // store username
                playAudio(); // play audio immediately after login
                startAnimation();
            } else {
                alert("Wrong creditentials!");
            }
        } catch (error) {
            console.error("Login error:", error);
            alert("Login failed. Please try again later.");
        }
    }
}

function playAudio() {
    // play audio immediately after login
    var audio = new Audio('hang.mp3');
    audio.play();
}

function startAnimation() {
    // change page and move auto image
    setTimeout(function() {
        window.location.href = 'index.html'; 
    }, 2000); // time should match the animation duration

    // move auto image
    var carImage = document.querySelector('.car-image');
    carImage.style.transition = 'right 2s ease-in-out'; // Set transition duration
    carImage.style.right = 'calc(100% + 50px)';
}

function isEmpty(str) {
    return (!str || 0 === str.length);
}
