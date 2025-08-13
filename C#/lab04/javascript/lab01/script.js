


function getName(){
let name = prompt("Enter your name: ");
return name.toLocaleUpperCase();
}

function sayHello(){
    let name = getName();
    alert(`Hello Mr ${name}! did you know that your name has ${name.length} letters ? `)
}
