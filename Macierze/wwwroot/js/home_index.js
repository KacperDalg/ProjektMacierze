var i = 1;

function create() {
    var numberOfDivs = parseInt(document.getElementById("numberOfDivs").value);
    var container = document.getElementById("container-divs");
    if (numberOfDivs > 10 || numberOfDivs < 2) {
        alert("Nieprawidłowy rozmiar macierzy (Minimalny rozmiar - 2x2; Maksymalny rozmiar - 10x10)");
    }
    else {
        container.innerHTML = "";
        for (var a = numberOfDivs; a > 0; a--) {
            for (var b = numberOfDivs; b > 0; b--) {
                var div = document.createElement("div");
                div.innerHTML = '<input class="one-number" name="divNum' + i + '" type="number"/>';
                container.appendChild(div);
                i++;
            }
            container.innerHTML += '<div class="linebreak"></div>'
        }
    }
}
document.getElementById("btn").addEventListener("click", create)

function check() {
    const collection = document.getElementsByClassName("one-number");
    for (var c = 0; c < collection.length; c++) {
        if (collection[c].value == "") {
            alert("Wypełnij wszystkie pola!");
            break;
        }
    }
}
document.getElementById("submit").addEventListener("click", check)

function random() {
    document.getElementById("numberOfDivs").value = Math.floor(Math.random() * 9) + 2;
    create();
    const collection = document.getElementsByClassName("one-number");
    for (var c = 0; c < collection.length; c++) {
        collection[c].value = Math.floor(Math.random() * 100);
    }
}
document.getElementById("btn-random").addEventListener("click", random)