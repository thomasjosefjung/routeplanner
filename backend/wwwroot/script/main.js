let nodes = [];
let edges = [];
let route = new Object();
route.edges = [];

function writeRoute() {
    let ul = document.getElementById("route_list");
    ul.replaceChildren([]);

    for (let i = 0; i < route.nodes.length; i++) {
        let node = route.nodes[i];
        let listElement = document.createElement("li");
        listElement.innerText = node.name;
        ul.appendChild(listElement);
    }
}

function fillDropdowns() {
    let select_to = document.getElementById('select_to')
    select_to.replaceChildren([]);

    let select_from = document.getElementById('select_from')
    select_from.replaceChildren([]);

    for (let i = 0; i < nodes.length; i++) {
        let obj = nodes[i];

        let newOption = document.createElement('option');
        newOption.value = obj.name;
        let optionText = document.createTextNode(obj.name);
        newOption.appendChild(optionText);

        select_from.appendChild(newOption);

        newOption = document.createElement('option');
        newOption.value = obj.name;
        optionText = document.createTextNode(obj.name);
        newOption.appendChild(optionText);

        select_to.appendChild(newOption);
    }
}

function draw() {
    let canv = document.getElementById("canvas");

    var ctx = canv.getContext("2d");
    ctx.clearRect(0, 0, canv.width, canv.height);

    ctx.strokeStyle = 'lightgray';

    for (let i = 0; i < edges.length; i++) {
        let obj = edges[i];
        ctx.beginPath();

        let coordsFrom = mapCoordinates(obj.coordsFrom.x, obj.coordsFrom.y);
        let coordsTo = mapCoordinates(obj.coordsTo.x, obj.coordsTo.y);

        let [dx, dy] = [coordsTo[0] - coordsFrom[0], coordsTo[1] - coordsFrom[1]];
        let length = Math.sqrt(dx * dx + dy * dy);

        if (length > 100) {
            continue;
        }

        ctx.moveTo(coordsFrom[0], coordsFrom[1]);
        ctx.lineTo(coordsTo[0], coordsTo[1]);
        ctx.stroke();
    }

    ctx.strokeStyle = 'black';

    for (let i = 0; i < nodes.length; i++) {
        let obj = nodes[i];

        drawFilledCircle(ctx, obj.coordinates.x, obj.coordinates.y, 1, 'black');

    }

    ctx.save();
    ctx.strokeStyle = 'blue';
    ctx.lineWidth = 4;

    if (route.edges.length > 0) {
        let edge = route.edges[0];
        let coordsFrom = mapCoordinates(edge.coordsFrom.x, edge.coordsFrom.y);
        let coordsTo = mapCoordinates(edge.coordsTo.x, edge.coordsTo.y);
        ctx.beginPath();
        ctx.moveTo(coordsFrom[0], coordsFrom[1]);

        for (let i = 0; i < route.edges.length; i++) {
            let edge = route.edges[i];
            // let coordsFrom = mapCoordinates(edge.coordsFrom.x, edge.coordsFrom.y); 
            let coordsTo = mapCoordinates(edge.coordsTo.x, edge.coordsTo.y);
            // ctx.beginPath(); 
            ctx.lineTo(coordsTo[0], coordsTo[1]);
        }

        ctx.stroke();
    }

    ctx.restore();
}

async function initialize() {
    let response = await fetch('/api/Edges');
    edges = await response.json();

    response = await fetch('/api/Nodes');
    nodes = await response.json();

    fillDropdowns(); 

    draw();
}

async function trigger_search() {
    let selectFrom = document.getElementById('select_from');
    let selectTo = document.getElementById('select_to');

    let request = new Request(`/api/Route?From=${selectFrom.value}&To=${selectTo.value}`);
    var response = await fetch(request);
    route = await response.json();

    draw();
    writeRoute();
}

function mapCoordinates(x, y) {
    return [85 * (x - 5.0), 1000 - (130 * (y - 47))];
}

function drawFilledCircle(ctx, x, y, size, color) {
    ctx.beginPath(); // Beginnen Sie einen neuen Zeichenpfad

    [x, y] = mapCoordinates(x, y);

    ctx.arc(x, y, size, 0, 2 * Math.PI, false);
    ctx.fillStyle = color;
    ctx.fill(); // Füllen Sie den Kreis mit der zuvor festgelegten Farbe
    ctx.stroke();
}