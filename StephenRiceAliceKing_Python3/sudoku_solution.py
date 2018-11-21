import copy
from pprint import pprint


def parse(foo):
    foo = foo.strip()
    foo = foo.split('\n')
    return [
        [' ' if cell == '.' else cell for cell in row.strip()] 
        for row in foo
    ]


def get_row(grid, row_index):
    return grid[row_index]


def get_col(grid, col_index):
    return [row[col_index] for row in grid]


def get_empty(thing):
    return [
        index
        for (index, cell) in enumerate(thing)
        if cell == ' '
    ]


def is_valid_set(thing):
    cells = [cell for cell in thing if cell != ' ']
    if len(cells) != len(set(cells)):
        return False
    return True


def is_valid(grid):
    # check rows
    for row in grid:
         if not is_valid_set(row):
             return False
    # check columns
    for col_index in range(0, 9):
        col = [row[col_index] for row in grid]
        if not is_valid_set(col):
            return False
        
    # check boxes
    for row_chunk in range(0, 9, 3):
        for col_chunk in range(0, 9, 3):
            square = grid[row_chunk][col_chunk:col_chunk+3] + grid[row_chunk+1][col_chunk:col_chunk+3] + grid[row_chunk+2][col_chunk:col_chunk+3]
            if not is_valid_set(square):
                return False
            
    return True


def is_complete(grid):
    for row in grid:
        if ' ' in row:
            return False
    return True


def get_valid_index_in_row_if_exists(grid, row_index, candidate):
    row = get_row(grid, row_index)
    empty = get_empty(row)
    count = 0
    for index in empty:
        test_grid = copy.deepcopy(grid)
        test_grid[row_index][index] = str(candidate)
        if is_valid(test_grid):
            count += 1
            valid_index = index
    return valid_index if count == 1 else None
        

def get_valid_index_in_col_if_exists(grid, col_index, candidate):
    col = get_col(grid, col_index)
    empty = get_empty(col)
    count = 0
    for index in empty:
        test_grid = copy.deepcopy(grid)
        test_grid[index][col_index] = str(candidate)
        if is_valid(test_grid):
            count += 1
            valid_index = index
    return valid_index if count == 1 else None


def solve_next(grid):
    for index in range(0, 9):
        for candidate in range(1, 10):
            result = get_valid_index_in_row_if_exists(grid, index, candidate)
            if result is not None:
                grid[index][result] = str(candidate)
                return True
            
            result = get_valid_index_in_col_if_exists(grid, index, candidate)
            if result is not None:
                grid[result][index] = str(candidate)
                return True
    return False

def solve(raw_grid):
    print('=' * 50)
    grid = parse(raw_grid)
    while not is_complete(grid):
        pprint(grid)
        if not solve_next(grid):
            print('NO FURTHER MOVES')
            break
    print('DONE')
    pprint(grid)
            

## Tests

empty_grid = [
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '], 
]

invalid_row = [
    ['1','1',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '], 
]

valid_row = [
    ['1','2','3','4','5','6','7','8','9'],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '], 
]

invalid_col = [
    ['1',' ',' ',' ',' ',' ',' ',' ',' '],
    ['1',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '], 
]

valid_col = [
    ['1',' ',' ',' ',' ',' ',' ',' ',' '],
    ['2',' ',' ',' ',' ',' ',' ',' ',' '],
    ['3',' ',' ',' ',' ',' ',' ',' ',' '],
    ['4',' ',' ',' ',' ',' ',' ',' ',' '],
    ['5',' ',' ',' ',' ',' ',' ',' ',' '],
    ['6',' ',' ',' ',' ',' ',' ',' ',' '],
    ['7',' ',' ',' ',' ',' ',' ',' ',' '],
    ['8',' ',' ',' ',' ',' ',' ',' ',' '],
    ['9',' ',' ',' ',' ',' ',' ',' ',' '], 
]


valid_square = [
    ['1','2','3',' ',' ',' ',' ',' ',' '],
    ['4','5','6',' ',' ',' ',' ',' ',' '],
    [' ','8','9',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '], 
]

invalid_square = [
    ['1','2','3',' ',' ',' ',' ',' ',' '],
    ['4','5','1',' ',' ',' ',' ',' ',' '],
    [' ','8','9',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '],
    [' ',' ',' ',' ',' ',' ',' ',' ',' '], 
]

full_invalid_grid = [
    ['1','2','3','4','5','6','7','8','9'],
    ['1','2','3','4','5','6','7','8','9'],
    ['1','2','3','4','5','6','7','8','9'],
    ['1','2','3','4','5','6','7','8','9'],
    ['1','2','3','4','5','6','7','8','9'],
    ['1','2','3','4','5','6','7','8','9'],
    ['1','2','3','4','5','6','7','8','9'],
    ['1','2','3','4','5','6','7','8','9'],
    ['1','2','3','4','5','6','7','8','9'], 
]

partial_grid = """
...75281.
....4...6
2...83..5
..9...283
.8.397.4.
314...7..
1..43...7
4...7....
.95261...
"""

test_parse = parse(partial_grid)
pprint(test_parse)

print(is_valid(empty_grid))
print(not is_complete(empty_grid))
print(not is_valid(invalid_row))
print(not is_complete(invalid_row))
print(is_valid(valid_row))
print(not is_complete(valid_row))
print(is_complete(full_invalid_grid))
print(not is_valid(full_invalid_grid))
print(not is_valid(invalid_col))
print(is_valid(valid_col))
print(is_valid(valid_square))
print(not is_valid(invalid_square))

print(is_valid(parse(partial_grid)))
solve(partial_grid)
