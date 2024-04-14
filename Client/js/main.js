
//URL API Control visitas
const baseURL = "https://localhost:44337";

function openAddModal() {
  // Configura el modal para agregar un nuevo producto
  configureModal("Agregar Visita", {}, "POST");
}

//Funcion para cargar las visitas registradas

function obtenerVisitas(){
    const visitaTable = document.getElementById("visitaTable")
    visitaTable.innerHTML = ''
    fetch(`${baseURL}/api/visitas`)
    .then((response) => response.json())
    .then((visitas) => {
      Object.values(visitas).forEach((visita) => {
        visita.forEach((visit)=>{
          const row = `
                    <tr>
                        <td>${visit.idVisitas}</td>
                        <td>${visit.nombre || "-"}</td>
                        <td>${visit.apellido}</td>
                        <td>${visit.modelo}</td>
                        <td>${visit.color}</td>
                        <td>${visit.estado ? "Activo" : "Inactivo"}</td>
                        <td>
                            <button class="btn btn-primary" onclick="openEditModal(${
                              visit.idVisitas
                            })">Editar</button>
                            <button class="btn btn-danger" onclick="deleteProduct(${
                              visit.idVisitas
                            })">Eliminar</button>
                        </td>
                    </tr>
                `;
        visitaTable.innerHTML += row;
        })
      });
    })
    .catch((error) => console.error("Error al obtener las visitas:", error));

    document.addEventListener("DOMContentLoaded", function () {
        obtenerVisitas();
      });
}

function configureModal(title, visita, method) {
  
  const modalTitle = document.getElementById("visitaModalLabel");
  const nombreInput = document.getElementById("nombreInput");
  const apellidoInput = document.getElementById("apellidoInput");
  const colorInput = document.getElementById("colorInput");
  const estadoInput = document.getElementById("estadoInput");
  const saveVisitaButton = document.getElementById("saveVisitaButton");

  // Se asume que el elemento con el ID 'productIdInput' no es necesario

  modalTitle.textContent = title;

  

  // Solo establece valores si los elementos existen
  if (nombreInput) {
    nombreInput.value = visita.nombre || "";
  }

  if (apellidoInput) {
    apellidoInput.value = visita.apellido || "";
  }

  if (estadoInput) {
    estadoInput.checked = visita.estado || false;
  }

  // Cargar las categorías al abrir el modal
  cargarCasas();

  if (colorInput) {
    colorInput.value = visita.color || false;
  }

  saveVisitaButton.onclick = saveVisita;

  $('#visitaModal').modal('show');
} 

function saveVisita() {
  const modalTitle = document.getElementById("visitaModalLabel");
  const casaId = casaSelect.value; // Obtén el ID de la casa seleccionada

  const visitaData = {
    nombre: nombreInput.value,
    apellido: apellidoInput.value,
    estado: estadoInput.checked,
    idCasa: casaId,
    color: colorInput.value
  };

  const method = modalTitle.textContent.includes("Agregar") ? "POST" : "PUT";
  // Realiza una solicitud POST o PUT a la API para guardar el producto
  fetch(`${baseURL}/api/visitas`, {
    method: method,
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(visitaData),
  })
    .then((response) => response.json())
    .then((savedVisita) => {
      console.log("Visita agregada exitosamente:", saveVisita);
      $("#visitaModal").modal("hide");
      obtenerVisitas();
    })
    .catch((error) => console.error("Error al agendar la visita:", error));
}

function cargarCasas() {
  fetch(`${baseURL}/api/casas`)
    .then((response) => response.json())
    .then((casas) => {
      // Limpiar las opciones actuales
      casaSelect.innerHTML = "";

      // Agregar una opción por cada categoría
      Object.values(casas).forEach((casa) => {
        const option = document.createElement("option");
        option.value = casa.idCasa;
        option.textContent = casa.modelo;
        casaSelect.appendChild(option);
      });
    })
    .catch((error) => console.error("Error al cargar casas:", error));
}