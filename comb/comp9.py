import math
from sympy import *
def combinations(n, k):
    if k < 0 or k > n:
        return 0
    return math.factorial(n) // (math.factorial(k) * math.factorial(n - k))
def wp_x(p, x, degree):
    term1 = (1 / (2 ** p))
    term2 = (1 + x) ** combinations(p, 2)
    sum_term = 0
    for n in range(p + 1):
        sum_term += combinations(p, n) * ((1 - x) / (1 + x)) ** (n * (p - n) / 2)
    poly = term1 * term2 * sum_term
    taylor_series = poly.series(x, 0, degree + 1)  # Разложение в ряд Тейлора до степени degree + 1
    return taylor_series.coeff(x, degree)  # Получаем коэфф при степени degree
p = 8
degree = 13
coeff = int(wp_x(p, symbols('x'), degree))
print(f"Коэффициент перед x^{degree}: {coeff}")