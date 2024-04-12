
//URL API Control visitas
const baseURL = "https://localhost:44337";

//Funcion para cargar las visitas registradas

function obtenerVisitas(){
    const visitaTable = document.getElementById("visitaTable")
    visitaTable.innerHTML = ''
    fetch(`${baseURL}/api/visitas`)
    .then((response) => response.json())
    .then((visitas) => {
      visitas.forEach((visita) => {
        
        const row = `
                    <tr>
                        <td>${visita.IdVisita}</td>
                        <td>${visita.Nombre}</td>
                        <td>${visita.Apellido}</td>
                        <td>${visita.Modelo}</td>
                        <td>${visita.Color}</td>
                        <td>${visita.Estado.value == 1 ? "Activo" : "Inactivo"}</td>
                        <td>
                            <button class="btn btn-primary" onclick="openEditModal(${
                              visita.IdVisita
                            })">Editar</button>
                            <button class="btn btn-danger" onclick="deleteProduct(${
                              visita.IdVisita
                            })">Eliminar</button>
                        </td>
                    </tr>
                `;
            visitaTable.innerHTML += row;
      });
    })
    .catch((error) => console.error("Error al obtener las visitas:", error));
   

    document.addEventListener("DOMContentLoaded", function () {
        obtenerVisitas();
      });
}