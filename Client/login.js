async function login() {
    var username = document.getElementById('typeUsername').value;
    var password = document.getElementById('typePassword').value;
    if (isEmpty(username) || isEmpty(password)) {
        alert('Type email and password!');
    }
	else {
        if (password=='password' && username=='user')
			moveCar();
		else
			alert('Wrong creditentials!')
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

