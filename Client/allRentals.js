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
				if (response.status === 401) {
					throw new Error("Unauthorized");
				} else if (response.status === 403) {
					throw new Error("Forbidden");
				} else {
					throw new Error("HTTP error, status = " + response.status);
				}
            }
            return response.json();
        })
        .then(data => {
            displayAllRentals(data);
        })
        .catch(error => {
			
			if (error.message === "Unauthorized") {
				alert("You are not authorized to perform this action. Please log in as an admin.");
				window.location.href = "index.html";
			} else if (error.message === "Forbidden") {
				alert("You don't have permission to perform this action.");
				window.location.href = "index.html";
			} else {
				console.error('Error fetching rentals:', error);
				alert('Failed to retrieve rentals. Please try again later.');
				window.location.href = "index.html";
        }
			
            
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
