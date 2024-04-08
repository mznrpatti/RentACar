const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const carId = urlParams.get('carId');

const url = `https://localhost:6200/api/Rental/GetRentalAvailability/${carId}`;

fetch(url)
	.then(response => response.json())
    .then(data => {
		const rentalContainer = document.getElementById('rental-dates-container');
		const fromDateSelect = document.getElementById('from-date');
        const toDateSelect = document.getElementById('to-date');
		data.forEach(date => {
			const listItem = document.createElement('li');
			listItem.textContent = date;
			rentalContainer.appendChild(listItem);
			
			// Option elements for select elements
            const option = document.createElement('option');
            option.text = date;
            option.value = date;

            // Append options to both select elements
            fromDateSelect.add(option.cloneNode(true));
            toDateSelect.add(option.cloneNode(true));
        });
    })
	.catch(error => console.error('Hiba történt:', error));


function getUsername(){
	var storedUsername = localStorage.getItem("username");
	if(storedUsername==null) return "";
	return storedUsername;
}

async function rent() {
    var fromdate = document.getElementById('from-date').value;
    var todate = document.getElementById('to-date').value;
	var data = {
            carid: carId,
            username: getUsername(),
			fromdate: fromdate,
			todate: todate
        };
	try {
        const response = await postDataText("rental/rentcar", data, false);
        alert(response);
    } catch (error) {
        console.error("Rental error:", error);
        alert("Rental failed. Please try again later.");
    }
}