package main

import "strings"
import "strconv"

func execute(input string) string {
	instructions := strings.Split(input, "\n")
	code := ""
	position := Coordinate{1, 1}
	for _, instr := range instructions {
		position = move(position, strings.TrimSpace(instr))
		number := position.x + position.y*3 + 1
		code += strconv.Itoa(number)
	}
	return code
}

func move(position Coordinate, instructions string) Coordinate {
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
	return position
}

// Direction represents a horizontal and vertical direction. Values can be -1 to 1.
type Direction struct {
	horizontal, vertical int
}

// Coordinate is a coordinate on the keypad
// keypad       coordinates
// 1 2 3  (0,0) (1,0) (2,0)
// 4 5 6  (0,1) (1,1) (2,1)
// 7 8 9  (0,2) (1,2) (2,2)
type Coordinate struct {
	x, y int
}

var directionMap = map[string]*Direction{
	"U": {0, -1},
	"D": {0, 1},
	"L": {-1, 0},
	"R": {1, 0},
}
