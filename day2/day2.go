package main

import "strings"
import "strconv"

func execute(input string) string {
	instructions := strings.Split(input, "\n")
	code := ""
	var digit string
	// position := Coordinate{1, 1} //part1
	position := Coordinate{0, 2}
	for _, instr := range instructions {
		// position, digit = move(position, strings.TrimSpace(instr)) //part1
		position, digit = movePart2(position, strings.TrimSpace(instr))
		code += digit
	}
	return code
}

// keypad       coordinates
// 1 2 3  (0,0) (1,0) (2,0)
// 4 5 6  (0,1) (1,1) (2,1)
// 7 8 9  (0,2) (1,2) (2,2)
func move(position Coordinate, instructions string) (c Coordinate, digit string) {
	for _, instr := range instructions {
		dir := directionMap[string(instr)]

		position.x += dir.horizontal
		if position.x < 0 {
			position.x = 0
		} else if position.x > 2 {
			position.x = 2
		}

		position.y += dir.vertical
		if position.y < 0 {
			position.y = 0
		} else if position.y > 2 {
			position.y = 2
		}
	}
	digit = strconv.Itoa(position.x + position.y*3 + 1)
	return position, digit
}

//part 2 keypad
var keyPad = []string{
	"  1  ",
	" 234 ",
	"56789",
	" ABC ",
	"  D  ",
}

func movePart2(position Coordinate, instructions string) (c Coordinate, digit string) {
	for _, instr := range instructions {
		dir := directionMap[string(instr)]
		position.x += dir.horizontal
		if position.x < 0 || position.x > 4 || string(keyPad[position.y][position.x]) == " " {
			position.x -= dir.horizontal
		}
		position.y += dir.vertical
		if position.y < 0 || position.y > 4 || string(keyPad[position.y][position.x]) == " " {
			position.y -= dir.vertical
		}
	}
	digit = string(keyPad[position.y][position.x])
	return position, digit
}

// Direction represents a horizontal and vertical direction. Values can be -1 to 1.
type Direction struct {
	horizontal, vertical int
}

// Coordinate is a coordinate on the keypad
type Coordinate struct {
	x, y int
}

var directionMap = map[string]*Direction{
	"U": {0, -1},
	"D": {0, 1},
	"L": {-1, 0},
	"R": {1, 0},
}
