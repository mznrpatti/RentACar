const queryString = window.location.search;
const urlParams = new URLSearchParams(queryString);
const carId = urlParams.get('carId');

const url = `https://localhost:6200/api/Rental/GetRentalAvailability/${carId}`;

fetch(url)
	.then(response => response.json())
    .then(data => {
		const rentalContainer = document.getElementById('rental-dates-container');
		data.forEach(date => {
			const listItem = document.createElement('li');
			listItem.textContent = date;
			rentalContainer.appendChild(listItem);
        });
    })
	.catch(error => console.error('Hiba történt:', error));