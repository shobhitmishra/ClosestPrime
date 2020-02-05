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

       if (number==numberEntity.matchedPrime) 
    {
        message = `Bingo! Number entered ${number} is a prime number!.`
	} else {
      if (number>numberEntity.matchedPrime) {
          message = `See! Your number ${number} is not too far from the nearest prime number ${numberEntity.matchedPrime}. <br> Difference is merely ${number - numberEntity.matchedPrime}.`  
                //   message = `See! Your number ${number} is not too far from the nearest prime number ${numberEntity.matchedPrime}. <br> Difference is merely ${number - numberEntity.matchedPrime}.   It has been entered ${numberEntity.count} time(s).`     
	  }
      else {
          message = `See! Your number ${number} is not too far from the nearest prime number ${numberEntity.matchedPrime}. <br> Difference is merely ${numberEntity.matchedPrime - number}.`    

	  }
	}

    messageElement.innerHTML = `<p>${message}</p>`;
    getMaximumDifference();
}

function _displayMaximumDifference(maxDifferenceEntity) {
    const maxDiffMessageElement = document.getElementById('maxdiff-message');

    var msg = `Highest difference found so far is also not that huge.<br> It is just ${maxDifferenceEntity.difference}. <br>  It was discovered when someone entered ${maxDifferenceEntity.number}. Closest prime was ${maxDifferenceEntity.matchedPrime}. `
    //  msg = `Highest difference found so far is also not that huge.<br> It is just ${maxDifferenceEntity.difference}. <br>  It was discovered when someone entered ${maxDifferenceEntity.number}. Closest prime was ${maxDifferenceEntity.matchedPrime}. <br> It has occured ${maxDifferenceEntity.count} times.`
    maxDiffMessageElement.innerHTML = `<p>${msg}</p>`
}