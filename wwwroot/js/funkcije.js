var data;
function ucitajTabelu() {
    $.ajax({
        url: '/Product/GetAll', // Putanja do akcije u kontroleru
        type: 'GET', // Metoda zahteva (GET, POST, itd.)
        dataType: 'json', // Očekivani tip podataka u odgovoru
        success: function (response) {

            data = response
            if (Array.isArray(data)) {Console.log("jeste niz") }
            data.forEach(function (item) {
                document.write(`
            <tr>
            <td>${item.id}</td>
            <td>${item.name}</td>
            <td>${item.description}</td>
            <td>${item.author}</td>
            <td>${item.price} din.</td>
            <td><img src="${item.imageURL}" style="width:100px;height:80px;"/></td>
            <td>${item.categoryID}</td>
            </tr>
            `)
            });
        },
        error: function (xhr, status, error) {
            // Greška prilikom izvršavanja zahteva
            console.log('Došlo je do greške prilikom izvršavanja zahteva.');
            console.log(xhr.responseText);
        }
    });
}