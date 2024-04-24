var defaultUrl = "https://localhost:6200/api/";

async function postData(url = "", data = {}, needAuth = true) {
    // Default options are marked with *
    const response = await fetch(defaultUrl + url, {
        method: "POST", // *GET, POST, PUT, DELETE, etc.
        mode: "cors", // no-cors, *cors, same-origin
        cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
        credentials: "same-origin", // include, *same-origin, omit
        headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + (needAuth ? JSON.parse(localStorage.getItem("token")).token : null),
        },
        redirect: "follow", // manual, *follow, error
        referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        body: testJSON(data) ? data : JSON.stringify(data), // body data type must match "Content-Type" header
    });
    if (response.status === 401 || response.status === 403) {
        logout();
    }
    try {
        return await response.json() // parses JSON response into native JavaScript objects
    } catch (error) {
        console.error("Error parsing JSON:", error);
    }
}

async function postDataText(url = "", data = {}, needAuth = true) {
    // Default options are marked with *
    const response = await fetch(defaultUrl + url, {
        method: "POST", // *GET, POST, PUT, DELETE, etc.
        mode: "cors", // no-cors, *cors, same-origin
        cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
        credentials: "same-origin", // include, *same-origin, omit
        headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + localStorage.getItem("token"),
        },
        redirect: "follow", // manual, *follow, error
        referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
        body: testJSON(data) ? data : JSON.stringify(data), // body data type must match "Content-Type" header
    });
    if (response.status === 401 || response.status === 403) {
        logout();
    }
    try {
        return await response.text() // parses JSON response into native JavaScript objects
    } catch (error) {
        console.error("Error parsing JSON:", error);
    }
}

function testJSON(text) {
    if (typeof text !== "string") {
        return false;
    }
    try {
        JSON.parse(text);
        return true;
    } catch (error) {
        return false;
    }
}

function logout() {
    localStorage.clear();
    window.location.href = "login.html";
}