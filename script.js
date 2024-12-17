const cart = document.querySelector('.cart .fa');
const removeBtn = document.querySelector('.cta-remove');

removeBtn.textContent  = 'Remove from cart';




function addToCart(){
let cartValue = parseInt(cart.getAttribute('value'));
cartValue += 1;
cart.setAttribute('value', cartValue);
//console.log(cartValue);
HideRemoveBtn();
}

function RemoveFromCart(){
 let cartValue = parseInt(cart.getAttribute('value'));
 cartValue = 0;
 cart.setAttribute('value', cartValue);
 //console.log(cartValue);
 HideRemoveBtn();
}

function HideRemoveBtn(){
    if (cart.getAttribute('value') === '0'){
        removeBtn.style.display = 'none'; 
    } else {
        removeBtn.style.display = 'inline-block';
    }
}