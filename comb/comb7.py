import math
def Combinations(n,k):
    if k < 0 or k > n:
        return 0
    return math.factorial(n)//(math.factorial(k)*math.factorial(n-k))
def calculate_Cp(p,C_values=None):
    if p==1:
        return 1
    if p%2!=0:
        return 0
    if C_values is None:
        C_values={}
    if p in C_values:
        return C_values[p]
    param=2**Combinations(p-1,2)
    sum=0
    for k in range(1,p):
        sum+=k*Combinations(p,k)*2**Combinations(p-k-1,2)*calculate_Cp(k,C_values)
    result=param-(1/p)*sum
    return result
def to_file(partition, name_of_file = "comb11_07.txt"):
    # partition: список разбиений.
    # name_of_file: имя файла для записи.

    count = 0
    with open(name_of_file, "w") as f:
        f.write(f'All count graphs:{str(partition)}')
n = 8
result = calculate_Cp(n)
file = "comb11_07.txt.txt"
print(result)
to_file(result,file)
