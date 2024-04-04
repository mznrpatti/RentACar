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
            if (response.token) {
                localStorage.setItem("token", response.token); // Itt tárold el a tokent helyileg
                window.location.href = "index.html";
            } else {
                alert(response.message);
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
    // Oldalváltás, például egy másik oldalra navigálás
    setTimeout(function() {
        window.location.href = 'index.html'; // Az új oldal URL-je
    }, 2000); // 2 másodperc múlva vált oldalra
    
    // Az auto.png kép mozgatása az autóval együtt
    var carImage = document.querySelector('.car-image');
    carImage.style.right = 'calc(100% + 50px)';
}

