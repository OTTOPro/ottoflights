const volUrl = "/api/VolsAPI";

const connection = new signalR.HubConnectionBuilder().withUrl("/VolHub").build();
connection.start()
    .catch(function (err) { return console.error(err.toString()); });

document.getElementById("savebt").addEventListener("click", function (event) {
    var id = document.getElementById("id").value;
    var datePrevue = document.getElementById("datePrevue").value;
    var dateRevue = datePrevue.slice(0, 11) + document.getElementById("dateRevue").value;
    var numeroVol = document.getElementById("numeroVol").value;
    var agence = document.getElementById("agence").value;
    var provenance = document.getElementById("provenance").value;
    var etat = document.getElementById("etat").value;

    const vol = {
        volId: id,
        datePrevue: datePrevue,
        dateRevue: dateRevue,
        numeroVol: numeroVol,
        agence: agence,
        provenance: provenance,
        etat: etat
    };

    console.log("Voici le nouveau vol :", vol);

    fetch(volUrl + "/" + id, {
        method: "PUT",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(vol)
    })
    .then(response => response.json())
    .then(() => {
        connection.invoke("SendMessage").catch(function (err) {
            return console.error(err.toString());
        });
    })
    .catch(error => {
        console.error("Erreur lors de la requête API :", error);
        alert("Erreur API");
    });

    event.preventDefault();
});
