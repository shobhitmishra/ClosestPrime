import argparse
import time
import itertools as it

def erat3():
    D = { 9: 3, 25: 5 }
    yield 2
    yield 3
    yield 5
    MASK= 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0,
    MODULOS= frozenset( (1, 7, 11, 13, 17, 19, 23, 29) )

    for q in it.compress(
            it.islice(it.count(7), 0, None, 2),
            it.cycle(MASK)):
        p = D.pop(q, None)
        if p is None:
            D[q*q] = q
            yield q
        else:
            x = q + 2*p
            while x in D or (x%30) not in MODULOS:
                x += 2*p
            D[x] = p


if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument(
        "--n", type=int, default=int(1e6), help="number of primes to generate"
    )
    parser.add_argument("--outf", default="", help="output file, with extension")
    opt = parser.parse_args()
    if not opt.outf:
        opt.outf = "./%d_primes.txt" % opt.n

    primes_gen = erat3()
    s = time.time()
    l = [0] * int(1e6)
    for i, p in enumerate(primes_gen):
        j = i % int(1e6)
        l[j] = p
        if j == int(1e6) - 1 or i >= opt.n:
            with open(opt.outf, "a") as f:                
                f.writelines([str(i) + "\n" for i in l if i > 0])
            l = [0] * int(1e6)
            print(f'({int(time.time() - s)}s) wrote at step {i}')
            if i >= opt.n:
                break
    print(f'{opt.n} prime numbers in {time.time() - s}s')