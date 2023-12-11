const volUrl = "/api/VolsAPI";
let $vols = $("#vols");

getVols();

function formatDate(date) {
    const options = { hour: '2-digit', minute: '2-digit', day: '2-digit', month: '2-digit' };
    return new Date(date).toLocaleDateString("en-US", options);
}

function getVols() {
    fetch(volUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error(`Réponse de l'API non OK: ${response.statusText}`);
            }
            return response.json();
        })
        .then(data => {
            data.forEach(vol => {
                console.log("Données brutes de l'API :", vol);

                let etatClass = "";
                if (vol.etat.toLowerCase() === "annulé") {
                    etatClass = "text-danger"; // Rouge
                } else if (vol.etat.toLowerCase() === "retardé") {
                    etatClass = "text-warning"; // Orange
                } else if (vol.etat.toLowerCase() === "arrivé") {
                    etatClass = "text-success"; // Vert
                }
                let detailsLink = `<a href="/Vols/Details/${vol.volId}" class="btn btn-info"><i class="fas fa-info-circle"></i></a>`;

                let template = `<tr class="${etatClass}">
                    <td>${formatDate(vol.datePrevue)}</td>
                    <td>${formatDate(vol.dateRevue)}</td>
                    <td>${vol.numeroVol}</td>
                    <td>${vol.agence}</td>
                    <td>${vol.provenance}</td>
                    <td>${vol.etat}</td>
                    <td>${detailsLink}</td>
                </tr>`;
                $vols.append($(template));
            });
        })
        .catch(error => {
            console.error("Erreur lors de la récupération des données de l'API :", error);
            alert(`Erreur API: ${error.message}`);
        });
}

const connection = new signalR.HubConnectionBuilder().withUrl("/VolHub").build();
connection.start()
    .catch(function (err) { return console.error(err.toString()); });

connection.on("VolChange", function () {
    $vols.empty();
    getVols();
});