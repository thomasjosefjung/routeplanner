async function initialize() 
{
    document.body.textContent = "Nope"

    var response = await fetch('https://localhost:7010/api/Nodes');
    var data = await response.json();
    console.log(data);    
}