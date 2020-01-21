const uri = 'api/PrimeNumber';
let todos = [];

function getClosestPrime() {
    var number = document.getElementById('input-number').value;
    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: number
    })
        .then(response => response.json())
        .then((closestPrime) => _displayClosestPrime(number, closestPrime))
        .catch(error => console.error('Unable to get prime.', error));
}

function _displayClosestPrime(number, closestPrime) {
    const messageElement = document.getElementById('prime-message');
    var message = `Closest prime to ${number} is ${closestPrime}.`
    messageElement.innerHTML = `<p>${message}</p>`
}

