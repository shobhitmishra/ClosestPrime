# ClosestPrime
Keep track of closest primes to a given number
Prime_number_generator.py is the script to generate prime numbers. This script is taken from [this](https://vict0rs.ch/2019/03/25/generating-hundre-million-primes/ "Generating primes") link. 
We can run the script using the following command:  
python Prime_number_generator.py --n= #number of primes to be generated# --outf= #ouput file path#    
An example is:  
python Prime_number_generator.py --n=1000 --outf='./test.txt'  
This will generate the first 1000 primes and save them in tests.txt

We have generated the first million and ten million primes. They are stored in 1e6_primes.txt and 1e7_primes.txt respectively. 
