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

function getMaximumDifference() {
    fetch(uri, {
        method: 'GET'           
    })
        .then(response => response.json())
        .then((maxDifferenceEntity) => _displayMaximumDifference(maxDifferenceEntity))
        .catch(error => console.error('Unable to get maximum difference.', error));
}

function _displayClosestPrime(number, numberEntity) {
    const messageElement = document.getElementById('prime-message');
    var message = `Closest prime to ${number} is ${numberEntity.matchedPrime}. It has been entered ${numberEntity.count} times.`
    messageElement.innerHTML = `<p>${message}</p>`;
    getMaximumDifference();
}

function _displayMaximumDifference(maxDifferenceEntity) {
    const maxDiffMessageElement = document.getElementById('maxdiff-message');
    var msg = `Maximum difference so far is ${maxDifferenceEntity.difference} between ${maxDifferenceEntity.number} and ${maxDifferenceEntity.matchedPrime}. It has occured ${maxDifferenceEntity.count} times.`
    maxDiffMessageElement.innerHTML = `<p>${msg}</p>`
}