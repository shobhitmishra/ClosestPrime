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
        .then((numberEntity) => _displayClosestPrime(number, numberEntity))
        .catch(error => console.error('Unable to get prime.', error));
}

function _displayClosestPrime(number, numberEntity) {
    const messageElement = document.getElementById('prime-message');
    var message = `Closest prime to ${number} is ${numberEntity.rowKey}. It has been entered ${numberEntity.count} times.`
    messageElement.innerHTML = `<p>${message}</p>`
}

