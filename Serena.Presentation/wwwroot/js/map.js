// js/map.js
function initializeMap(elementId, coordinates, popupText) {
    const mapElement = document.getElementById(elementId);
    if (!mapElement) return;
    
    const map = L.map(elementId).setView(coordinates, 13);
    
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);
    
    L.marker(coordinates).addTo(map)
        .bindPopup(popupText)
        .openPopup();
        
    return map;
}

// Initialize home page map
function initializeHomeMap() {
    return initializeMap('map', [30.81624032, 30.99334552], 'Serena Headquarters');
}

// Initialize contact page map
function initializeContactMap() {
    return initializeMap('map', [30.81624032, 30.99334552], 'Serena Headquarters');
}