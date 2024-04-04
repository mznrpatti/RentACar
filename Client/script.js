document.addEventListener("DOMContentLoaded", function() {
    fetch('https://localhost:6200/api/car/getcars')
    .then(response => response.json())
    .then(data => {
		setWelcomeMessage();
        const carTableBody = document.querySelector('#car-table tbody');
        const categories = new Set();
        data.forEach(car => {
            const row = document.createElement('tr');
            row.innerHTML = `
				<td>${car.id}</td>
                <td>${car.categoryName}</td>
                <td>${car.brand}</td>
                <td>${car.model}</td>
                <td>${car.dailyPrice}</td>
            `;
            carTableBody.appendChild(row);

            categories.add(car.categoryName);
        });

        // Kateg�ri�k hozz�ad�sa a leg�rd�l� list�hoz
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

    // Kateg�ria szerinti sz�r�s
    categoryFilter.addEventListener('change', function () {
        const selectedCategory = categoryFilter.value;
        fetch(`https://localhost:6200/api/car/getcarsbycategory?category=${selectedCategory}`)
            .then(response => response.json())
            .then(data => {
                carTableBody.innerHTML = ''; // T�bl�zat �r�t�se

                data.forEach(car => {
                    const row = document.createElement('tr');
                    row.innerHTML = `
						<td>${car.id}</td>
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

});

function setWelcomeMessage() {
	var storedUsername = localStorage.getItem("username");
    document.getElementById("welcome").innerText = "Welcome "+storedUsername+"!";
}