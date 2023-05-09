let nodes = []; 
let edges = []; 
let route = []; 


function draw()
{
    let canv = document.getElementById("canvas"); 
    var ctx = canv.getContext("2d");

    for(let i = 0; i < nodes.length; i++) {
        let obj = nodes[i];

        drawFilledCircle(ctx, obj.coordinates.x, obj.coordinates.y, 1, 'black'); 

        let newOption = document.createElement('option');
        newOption.value = obj.name; 
        let optionText = document.createTextNode(obj.name); 
        newOption.appendChild(optionText); 
        // newOption.setAttribute('value')

        document.getElementById('select_from').appendChild(newOption); 

        newOption = document.createElement('option');
        newOption.value = obj.name; 
        optionText = document.createTextNode(obj.name); 
        newOption.appendChild(optionText); 

        document.getElementById('select_to').appendChild(newOption); 
    }

    for(let i = 0; i < edges.length; i++) {
        let obj = edges[i];
        ctx.beginPath();

        let coordsFrom = mapCoordinates(obj.coordsFrom.x, obj.coordsFrom.y); 
        let coordsTo = mapCoordinates(obj.coordsTo.x, obj.coordsTo.y); 

        let [dx, dy] = [coordsTo[0] - coordsFrom[0], coordsTo[1] - coordsFrom[1]]; 
        let length = Math.sqrt(dx*dx + dy*dy); 

        if (length > 100)
        {
            continue; 
        }

        ctx.moveTo(coordsFrom[0], coordsFrom[1]); 
        ctx.lineTo(coordsTo[0], coordsTo[1]);
        ctx.stroke();    
    }

}

async function initialize() 
{
    let response = await fetch('/api/Edges');
    edges = await response.json(); 

    response = await fetch('/api/Nodes');
    nodes = await response.json();


    draw(); 
}

async function trigger_search()
{
    let selectFrom = document.getElementById('select_from'); 
    let selectTo = document.getElementById('select_to'); 

    let request = new Request(`/api/Route?From=${selectFrom.value}&To=${selectTo.value}`);
    var response = await fetch(request);
    var data = await response.json();

    console.log(data);
}

function mapCoordinates(x, y)
{
    return [85 * (x - 5.0), 1000 - (130 * (y - 47))]; 
}

function drawFilledCircle(ctx, x, y, size, color)
{
    ctx.beginPath(); // Beginnen Sie einen neuen Zeichenpfad

    [x, y] = mapCoordinates(x, y); 

    ctx.arc(x, y, size, 0, 2 * Math.PI, false);
    ctx.fillStyle = color; 
    ctx.fill(); // Füllen Sie den Kreis mit der zuvor festgelegten Farbe
}