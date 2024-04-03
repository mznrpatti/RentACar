async function login() {
    var username = document.getElementById('typeUsername').value;
    var password = document.getElementById('typePassword').value;
    if (isEmpty(username) || isEmpty(password)) {
        alert('Type email and password!');
    }
	else {
        if (password=='password' && username=='user')
			window.location.href = "index.html";
		else
			alert('Wrong creditentials!')
    }
}

function isEmpty(str) {
    return (!str || 0 === str.length);
}