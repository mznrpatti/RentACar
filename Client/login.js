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
                moveCar();
            } else {
                alert("Wrong creditentials!");
            }
        } catch (error) {
            console.error("Login error:", error);
			alert(error);
            alert("Login failed. Please try again later.");
        }
    }
}

function isEmpty(str) {
    return (!str || 0 === str.length);
}


function moveCar() {
    // change page
    setTimeout(function() {
        window.location.href = 'index.html'; 
    }, 1000); 
    
    //move auto image
    var carImage = document.querySelector('.car-image');
    carImage.style.right = 'calc(100% + 50px)';
}

