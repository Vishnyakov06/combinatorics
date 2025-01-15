import math
def Combinations(n,k):
    if k < 0 or k > n:
        return 0
    return math.factorial(n)//(math.factorial(k)*math.factorial(n-k))
def calculate_Cp(p,n):
    if p==1:
        return 1
    if p<n:return 0
    sum=0
    for k in range(1,p):
        sum+=k*Combinations(p,k)*2**(k*(n-k))*calculate_Cp(k,n-1)
    return sum/n
def to_file(partition, name_of_file = "comb11_07.txt"):
    # partition: список разбиений.
    # name_of_file: имя файла для записи.

    count = 0
    with open(name_of_file, "w") as f:
        f.write(f'All count graphs:{str(partition)}')
n = 8
k=8
result = calculate_Cp(n,k)
file = "comb11_07.txt.txt"
print(result)
to_file(result,file)
