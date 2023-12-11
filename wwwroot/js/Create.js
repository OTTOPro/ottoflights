const volUrl = "/api/VolsAPI";

const connection = new signalR.HubConnectionBuilder().withUrl("/VolHub").build();
connection.start()
    .catch(function (err) { return console.error(err.toString()); });

document.getElementById("createbt").addEventListener("click", function (event) {
    var datePrevue = document.getElementById("datePrevue").value;
    console.log("Valeur de datePrevue avant assignation : ", datePrevue);
    var dateRevue = datePrevue;
    console.log("Valeur de dateRevue après assignation : ", dateRevue);
    var numeroVol = document.getElementById("numeroVol").value;
    var agence = document.getElementById("agence").value;
    var provenance = document.getElementById("provenance").value;
    var etat = document.getElementById("etat").value;

    const vol = {
        id: 0,
        datePrevue: datePrevue,
        dateRevue: dateRevue,
        numeroVol: numeroVol,
        agence: agence,
        provenance: provenance,
        etat: etat
    };

    console.log("Valeur de dateRevue après assignation : ", dateRevue);

    console.log("Voici le nouveau vol :", vol);

    fetch(volUrl, {
        method: "POST",
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
