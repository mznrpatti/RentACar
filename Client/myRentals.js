document.addEventListener("DOMContentLoaded", function() {
    const storedUsername = localStorage.getItem("username");
    if (storedUsername) {
        getUserRentals(storedUsername);
    } else {
        console.error('No username found in localStorage.');
    }
});

function getUserRentals(username) {
    fetch(`https://localhost:6200/api/Rental/GetUserRentals?username=${username}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Failed to retrieve user rentals');
            }
            return response.json();
        })
        .then(data => {
            displayUserRentals(data);
        })
        .catch(error => {
            console.error('Error fetching user rentals:', error);
            alert('Failed to retrieve user rentals. Please try again later.');
        });
}

async function displayUserRentals(userRentals) {
    const tableBody = document.querySelector('#user-rentals-table tbody');
    
    tableBody.innerHTML = '';

    const promises = userRentals.map(async rental => {
        const row = document.createElement('tr');

        const carIdCell = document.createElement('td');
        carIdCell.textContent = rental.carId;
		
		const carNameCell = document.createElement('td');
        carNameCell.textContent = rental.carName;

        const fromDateCell = document.createElement('td');
        fromDateCell.textContent = new Date(rental.fromDate).toLocaleDateString();

        const toDateCell = document.createElement('td');
        toDateCell.textContent = new Date(rental.toDate).toLocaleDateString();

        const createdCell = document.createElement('td');
        createdCell.textContent = new Date(rental.created).toLocaleDateString();

        row.appendChild(carIdCell);
		row.appendChild(carNameCell);
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
