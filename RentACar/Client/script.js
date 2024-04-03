document.addEventListener("DOMContentLoaded", function() {
    fetch('https://localhost:6200/api/car/getcars')
    .then(response => response.json())
    .then(data => {
        const carTableBody = document.querySelector('#car-table tbody');
        data.forEach(car => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${car.categoryName}</td>
                <td>${car.brand}</td>
                <td>${car.model}</td>
                <td>${car.dailyPrice}</td>
            `;
            carTableBody.appendChild(row);
        });
    })
    .catch(error => console.error('Error fetching cars:', error));
});