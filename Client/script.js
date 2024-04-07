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
        fetch(`https://localhost:6200/api/car/getcarsbycategory?category=${selectedCategory}`)
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
    
    fetch('https://localhost:6200/api/sale/getallsales')
            .then(response => response.json())
            .then(data => {
                const saleTableBody = document.querySelector('#sale-table tbody'); // Az "On Sale" táblázat tbody eleme
                data.forEach(sale => {
                    const row = document.createElement('tr');
                    row.innerHTML = `
                        <td>${sale.carBrand}</td>
                        <td>${sale.carModel}</td>
                        <td>${sale.description}</td>
                        <td>${sale.percentage}</td>
						<td>${sale.changedPrice}</td>
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
