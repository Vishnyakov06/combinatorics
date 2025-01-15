import itertools

def int_partitions(n, k):
    # функция генерирует способы разбить число на
    # положительные слагаемые, с учетом их порядка

    # n: исходное число
    # k: количество положительных слагаемых

    # выдает все разбиения в виде кортежа числа n на
    # k положительных частей
    if k == 0:
        if n == 0:
           yield ()
        return
    if k == 1:
        if n > 0:
            yield (n,)
        return
    for i in range(1, n + 1):
            for part in int_partitions(n - i, k-1):
                yield (i,)+part


def generate_cycles(sizes, digits):
    # функция, которая создает всевозможные
    # циклы для заданных размеров

    # sizes: размеры циклов
    # digits: массив цифр

    if not sizes:
        yield []
        return

    first = sizes[0]
    remain = sizes[1:]

    # перебираем все комбинации текущего цикла
    for elements in itertools.combinations(digits, first):

        # оставшиеся цифры, которые не вошли в цикл
        remain_digits = set(digits) - set(elements)

        # перебираем все перестановки и возвращаем список из циклов
        for perm in itertools.permutations(elements):
            cycle = tuple(perm)
            for rest in generate_cycles(remain, remain_digits):
                yield [cycle] + rest


def part_cycles(cycles):
    # функция генерирует подстановку из набора циклов
    # cycles: набор циклов

    # определяем максимальную цифру в цикле
    # и создаем список перестановок
    max_digit = max(max(cycle) for cycle in cycles) if cycles else -1
    perm = list(range(max_digit + 1))

    # каждому элементу цикла присваиваем следующий
    for cycle in cycles:
        for i in range(len(cycle)):
           perm[cycle[i]] = cycle[(i + 1) % len(cycle)]
    return perm


def generate_permutations(digits, count_cycles):
    # функция создает подстановки с заданным
    # количеством независимых циклов

    # digits: массив цифр
    # count_cycles: количество циклов

    all_perm = set()

    # перебираем циклы разных размеров
    for sizes in int_partitions(len(digits), count_cycles):
        for cycles in generate_cycles(sizes, digits):

            # прогнав все циклы для соответствующих размеров,
            # получим перестановки
            permutation = tuple(part_cycles(cycles))
            all_perm.add(permutation)
    return all_perm


def to_file(partition, name_of_file = "comb11_05.txt"):
    # partition: список разбиений.
    # name_of_file: имя файла для записи.

    count = 0
    with open(name_of_file, "w") as f:
        for part in partition:
            count += 1
            f.write(str(part) + "\n")
    print(f'All partitions: {count}')


digits = [0, 1, 2, 3,4,5,6,7,8,9]
cyc = 5
file = "comb11_05.txt.txt"
result = generate_permutations(digits, cyc)
to_file(result, file)