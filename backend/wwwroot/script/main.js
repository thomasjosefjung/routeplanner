async function initialize() 
{
    var response = await fetch('https://localhost:7010/api/Nodes');
    var data = await response.json();

    let canv = document.getElementById("canvas"); 
    var ctx = canv.getContext("2d");

    for(let i = 0; i < data.length; i++) {
        let obj = data[i];

        if (obj.name.includes('WILNSDORF'))
            drawFilledCircle(ctx, obj.coordinates.x, obj.coordinates.y, 5, 'red'); 
        else if (obj.name.includes('HERRENBERG'))
            drawFilledCircle(ctx, obj.coordinates.x, obj.coordinates.y,5, 'red'); 
        else 
            drawFilledCircle(ctx, obj.coordinates.x, obj.coordinates.y, 1, 'black'); 
            
    }

    canv.a
}

function drawFilledCircle(ctx, x, y, size, color)
{
    ctx.beginPath(); // Beginnen Sie einen neuen Zeichenpfad

    x = 85 * (x - 5.0); 

    y = 130 * (y - 47); 
    y = 1000 - y; 

    ctx.arc(x, y, size, 0, 2 * Math.PI, false);
    ctx.fillStyle = color; 
    ctx.fill(); // Füllen Sie den Kreis mit der zuvor festgelegten Farbe
    
}