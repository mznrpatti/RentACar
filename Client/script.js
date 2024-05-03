document.addEventListener("DOMContentLoaded", function() {
    fetch('https://localhost:6200/api/car/getcars', 
	{
		headers: {
			'Authorization': 'Bearer ' + localStorage.getItem('token')
		}
	})
    .then(response => response.json())
    .then(data => {
        setWelcomeMessage();
        const carTableBody = document.querySelector('#car-table tbody');
        const categories = new Set();
        data.forEach(car => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td><a href="rental.html?carId=${car.id}">${car.id}</a></td>
                <td>${car.categoryName}</td>
                <td>${car.brand}</td>
                <td>${car.model}</td>
                <td>${car.dailyPrice}</td>
            `;
            carTableBody.appendChild(row);

            categories.add(car.categoryName);
        });

        // Kategóriák hozzáadása a legördülõ listához
        categories.forEach(category => {
            const option = document.createElement('option');
            option.value = category;
            option.textContent = category;
            categoryFilter.appendChild(option);
        });
    })
    .catch(error => console.error('Error fetching cars:', error));

    const categoryFilter = document.querySelector('#category-filter');
    const carTableBody = document.querySelector('#car-table tbody');

    // Kategória szerinti szûrés
    categoryFilter.addEventListener('change', function () {
        const selectedCategory = categoryFilter.value;
        fetch(`https://localhost:6200/api/car/getcarsbycategory?category=${selectedCategory}`, 
		{
			headers: {
				'Authorization': 'Bearer ' + localStorage.getItem('token')
			}
		})
            .then(response => response.json())
            .then(data => {
                carTableBody.innerHTML = ''; // Táblázat ürítése

                data.forEach(car => {
                    const row = document.createElement('tr');
                    row.innerHTML = `
                        <td><a href="rental.html?carId=${car.id}">${car.id}</a></td>
                        <td>${car.categoryName}</td>
                        <td>${car.brand}</td>
                        <td>${car.model}</td>
                        <td>${car.dailyPrice}</td>
                    `;
                    carTableBody.appendChild(row);
                });
            })
            .catch(error => console.error('Error fetching cars by category:', error));
    });
    
    fetch('https://localhost:6200/api/sale/getallsales', 
	{
		headers: {
			'Authorization': 'Bearer ' + localStorage.getItem('token')
		}
	})
            .then(response => response.json())
            .then(data => {
                const saleTableBody = document.querySelector('#sale-table tbody'); // Az "On Sale" táblázat tbody eleme
                data.forEach(sale => {
                    const row = document.createElement('tr');
					row.id = sale.carId;
					const button = `<button title="Delete" class="btn btn-danger" onclick="deletesale(${sale.id})">X</button>`;
                    row.innerHTML = `
                        <td>${sale.carBrand}</td>
                        <td>${sale.carModel}</td>
                        <td>${sale.description}</td>
                        <td>${sale.percentage}</td>
						<td>${sale.changedPrice}</td>
						<td>${button}</td>
                    `;
                    saleTableBody.appendChild(row);
                });
            })
            .catch(error => console.error('Error fetching sales:', error));
});

function setWelcomeMessage() {
    var storedUsername = localStorage.getItem("username");
    document.getElementById("welcome").innerText = "Welcome "+storedUsername+"!";
}

function redirectToMyRentals() {
    window.location.href = "myRentals.html";
}

function redirectToAllRentals() {
	window.location.href = "allRentals.html";
}

function logout() {
	localStorage.clear();
	window.location.href = "login.html";
}

//---------------------------------------------------
//New sale modal
// Get the modal
var modal = document.getElementById("newSaleModal");

// Get the button that opens the modal
var modalOpener = document.getElementById("openModal");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks on the button, open the modal
modalOpener.onclick = function() {
  modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function() {
  modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function(event) {
  if (event.target == modal) {
    modal.style.display = "none";
  }
}


//Create sale
async function createsale() {
    var carid = document.getElementById('carid').value;
    var description = document.getElementById('description').value;
	var percentage = document.getElementById('percentage').value;
	
	if (!carid || !description || !percentage) {
        alert("Please fill out all fields.");
        return;
    }
	
	var data = {
            CarId: carid,
			Description: description,
            Percentage: percentage
        };
	try {
        const response = await postDataText("sale/createsale", data, true);
		alert(response);
		
		if (!response.ok) {
            if (response.status === 401) {
                throw new Error("Unauthorized");
            } else if (response.status === 403) {
                throw new Error("Forbidden");
            }
        }
		
    } catch (error) {
		 if (error.message === "Unauthorized") {
            alert("You are not authorized to perform this action. Please log in as an admin.");
        } else if (error.message === "Forbidden") {
            alert("You don't have permission to perform this action.");
        } else {
            console.error("Sale creation error:", error);
			alert("Sale creation failed. Please try again later.");
        }
    }
	finally {
		modal.style.display = "none";
        location.reload();
    }
}

//Delete sale
async function deletesale(saleid) {
	try {
        const response = await fetch(`https://localhost:6200/api/Sale/DeleteSale/${saleid}`, {
            method: "DELETE",
            headers: {
                "Authorization": "Bearer " + localStorage.getItem("token")
            }
        });
		
		if (!response.ok) {
            if (response.status === 401) {
                throw new Error("Unauthorized");
            } else if (response.status === 403) {
                throw new Error("Forbidden");
            } else {
                throw new Error("HTTP error, status = " + response.status);
            }
        }
		
        const data = await response.text();
		alert(data);
    } catch (error) {
		 if (error.message === "Unauthorized") {
            alert("You are not authorized to perform this action. Please log in as an admin.");
        } else if (error.message === "Forbidden") {
            alert("You don't have permission to perform this action.");
        } else {
            console.error("Sale deletion error:", error);
            alert("Sale deletion failed. Please try again later.");
        }
    } finally {
        modal.style.display = "none";
        location.reload();
    }
}