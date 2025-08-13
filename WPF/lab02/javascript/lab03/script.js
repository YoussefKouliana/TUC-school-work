const cardPairs = {
    0: 4,
    1: 9,
    2: 8,
    3: 5,
    4: 0,
    5: 3,
    6: 10,
    7: 11,
    8: 2,
    9: 1,
    10: 6,
    11: 7,
};

const timeDropdown = document.getElementById('time');
const timePragraph = document.querySelector('.time span b');
const antalFlips= document.querySelector('.flips span b')
const startButton = document.getElementById('startButton')
const cards = document.querySelectorAll('.card');
let flippedCards = [];
let matchedPairs = 0;
const totalPairs = 6;
let countdown;
let gameStarted = false;


// changing the time text content to the option value
timeDropdown.addEventListener('change',function(){
    const timeSelected = timeDropdown.value;
    timePragraph.textContent = timeSelected;
});

// function or method to the countdown depends to the option value
function startCountdown(){
    clearInterval(countdown);
    let remainingTime = parseInt(timeDropdown.value)
    timePragraph.textContent = remainingTime;
    countdown = setInterval(function(){
        remainingTime --;
        timePragraph.textContent = remainingTime;
        if(remainingTime <= 0){
            clearInterval(countdown);
            alert("Time's up!")
            startButton.textContent = "Starta om";
            startButton.style.display = 'block';

        }
        if (remainingTime >0){
            startButton.style.display = 'none';
        }
    },1000);

}
//calling the countdown function
startButton.addEventListener('click', function(){
    gameStarted = true;
    startCountdown();
    flipAllCards();
});



// method to flipAllcards for 1 seconds to give a hint to the user.
function flipAllCards() {
    const allCards = document.querySelectorAll('.card');
    allCards.forEach(card => {
        const viewFrontCard = card.querySelector('.front-view');
        const viewBackCard = card.querySelector('.back-view');
        viewFrontCard.style.display = 'none';
        viewBackCard.style.display = 'flex';
    });

    // Flip back all cards after 1 second
    setTimeout(function () {
        allCards.forEach(function (card) {
            const viewFrontCard = card.querySelector('.front-view');
            const viewBackCard = card.querySelector('.back-view');
            viewFrontCard.style.display = 'flex';
            viewBackCard.style.display = 'none';
        });
    }, 1000);
}




// //method to flip the card on click
// function flipFrontCard(cardElement){
//   const viewFrontCard = cardElement.querySelector('.front-view');
//   const viewBackCard = cardElement.querySelector('.back-view');
//   if (viewBackCard.style.display === 'none'){
//     viewFrontCard.style.display = 'none';
//     viewBackCard.style.display = 'flex';
//   } else {
//     viewFrontCard.style.display = 'flex';
//     viewBackCard.style.display = 'none';
//   }
// }




//flip a single card
function flipFrontCard(cardElement) {
    if(!gameStarted) return;
    if (flippedCards.length >= 2 || cardElement.classList.contains('matched') || cardElement.classList.contains('flipped')) {
        return;
    }

    // Flip the card
    const frontView = cardElement.querySelector('.front-view');
    const backView = cardElement.querySelector('.back-view');
    frontView.style.display = 'none';
    backView.style.display = 'flex';

    cardElement.classList.add('flipped');
    flippedCards.push(cardElement);

    if (flippedCards.length === 2) {
        checkForMatch();
    }
}



function checkForMatch() {
    const card1 = flippedCards[0];
    const card2 = flippedCards[1];

    const card1Id = card1.getAttribute('data-id');
    const card2Id = card2.getAttribute('data-id');

    //  if condition IDs match based on the cardPairs object
    if (cardPairs[card1Id] == card2Id) {
        card1.classList.add('matched');
        card2.classList.add('matched');
        matchedPairs++;
        antalFlips.textContent = matchedPairs;


        // if condition win
        if (matchedPairs === totalPairs) {
            setTimeout(function () {
                alert('You win!');
                clearInterval(countdown);
                startButton.textContent = "Starta om";
                startButton.style.display = 'block';
                resetGame();
            }, 0);
        }

        // Clear flippedCards array
        flippedCards = [];
    } else {
        // card not matching
        setTimeout(function () {
            flipBack(card1);
            flipBack(card2);
            flippedCards = [];
        }, 1000);
    }
}

//fliping back card
// Flip a card back to its front view
function flipBack(cardElement) {
    const frontView = cardElement.querySelector('.front-view');
    const backView = cardElement.querySelector('.back-view');
    frontView.style.display = 'flex';
    backView.style.display = 'none';

    cardElement.classList.remove('flipped');
}

// Reset the game
function resetGame() {
    flippedCards = [];
    matchedPairs = 0;
    gameStarted = false;
    shuffleCards();

    cards.forEach(function(card) {
        const frontView = card.querySelector('.front-view');
        const backView = card.querySelector('.back-view');
        frontView.style.display = 'flex';
        backView.style.display = 'none';

        card.classList.remove('flipped', 'matched');
    });
    antalFlips.textContent = '0';
}

// Add event listeners to cards
cards.forEach(function(card) {
    card.addEventListener('click', function() {
        flipFrontCard(card);
    });
});

// Start the game
startButton.addEventListener('click', function () {
    startCountdown();
    flipAllCards();
});


//shuffle the cards
function shuffleCards() {
    const parent = document.querySelector('.cards'); 
    const cardsArray = Array.from(parent.querySelectorAll('li.card')); 

    cardsArray.sort(function() {
        return Math.random() - 0.5;
    });

    cardsArray.forEach(function(card) {
        parent.appendChild(card);
    });
}