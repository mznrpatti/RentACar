document.addEventListener("DOMContentLoaded", function() {
    getAllRentals();
});

function getAllRentals() {
    fetch(`https://localhost:6200/api/Rental/GetAllRentals`, 
	{
		headers: {
			'Authorization': 'Bearer ' + localStorage.getItem('token')
		}
	})
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to retrieve rentals');
            }
            return response.json();
        })
        .then(data => {
            displayAllRentals(data);
        })
        .catch(error => {
            console.error('Error fetching rentals:', error);
            alert('Failed to retrieve rentals. Please try again later.');
			window.location.href = "index.html";
        });
}

async function displayAllRentals(userRentals) {
    const tableBody = document.querySelector('#all-rentals-table tbody');
    
    tableBody.innerHTML = '';

    const promises = userRentals.map(async rental => {
        const row = document.createElement('tr');

        const carIdCell = document.createElement('td');
        carIdCell.textContent = rental.carId;
		
		const userIdCell = document.createElement('td');
        userIdCell.textContent = rental.userId;

        const fromDateCell = document.createElement('td');
        fromDateCell.textContent = new Date(rental.fromDate).toLocaleDateString();

        const toDateCell = document.createElement('td');
        toDateCell.textContent = new Date(rental.toDate).toLocaleDateString();

        const createdCell = document.createElement('td');
        createdCell.textContent = new Date(rental.created).toLocaleDateString();

        row.appendChild(carIdCell);
		row.appendChild(userIdCell);
        row.appendChild(fromDateCell);
        row.appendChild(toDateCell);
        row.appendChild(createdCell);

        tableBody.appendChild(row);
    });

    try {
        await Promise.all(promises);
    } catch (error) {
        console.error('Error fetching user rentals:', error);
        alert('Failed to retrieve user rentals. Please try again later.');
    }
}
