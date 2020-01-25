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
    

    
    var message = `Closest prime to ${number} is ${numberEntity.rowKey}. It has been entered ${numberEntity.count} time(s).` 
    messageElement.innerHTML = `<center><p><h1>${message}</p>`

    if (number==numberEntity.rowKey) 
    {
        message = `Bingo! Number entered ${number} is a prime number!. It has been entered ${numberEntity.count} time(s).`
	} else {
      if (number>numberEntity.rowKey) {
          message = `See! Your number ${number} is not too far from the nearest prime number ${numberEntity.rowKey}. <br> Difference is merely ${number - numberEntity.rowKey}.   It has been entered ${numberEntity.count} time(s).`        
	  }
      else {
          message = `See! Your number ${number} is not too far from the nearest prime number ${numberEntity.rowKey}. <br> Difference is merely ${numberEntity.rowKey - number}.   It has been entered ${numberEntity.count} time(s).`    

	  }
	}
        messageElement.innerHTML = `<center><p><h1>${message}</p>`
    
}

