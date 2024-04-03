async function login() {
    var email = document.getElementById('typeEmail').value;
    var password = document.getElementById('typePassword').value;
    if (isEmpty(email) || isEmpty(password)) {
        alert('Type email and password!');
    } else if (!validateEmail(email)) {
        alert('Wrong email!');
    } else {
        if (password=='password' && email=='user@gmail.com')
			window.location.href = "index.html";
		else
			alert('Wrong creditentials!')
    }
}

function isEmpty(str) {
    return (!str || 0 === str.length);
}

function validateEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}