package main

import (
	"math"
	"strconv"
	"strings"
)

func execute(input string) int {
	instr := parse(input)
	output := movePart2(instr)
	return output
}

type instruction struct {
	dir   int
	steps int
}

func parse(input string) []instruction {
	tokens := strings.Split(input, ", ")
	var instructions = make([]instruction, len(tokens))
	for i, instr := range tokens {
		if instr[0] == 'L' {
			instructions[i].dir = -1
		} else {
			instructions[i].dir = 1
		}

		instructions[i].steps, _ = strconv.Atoi(instr[1:])
	}
	return instructions
}

func move(instructions []instruction) int {
	var heading, x, y int

	for _, instr := range instructions {
		heading = (4 + heading + instr.dir) % 4

		if heading == 0 || heading == 2 {
			y += (heading - 1) * instr.steps
		} else {
			x += (heading - 2) * instr.steps
		}

	}

	return int(math.Abs(float64(x))) + int(math.Abs(float64(y)))
}

// Location represents a coordinate on the grid
type Location struct {
	x, y int
}

func (loc Location) getDistance() int {
	return int(math.Abs(float64(loc.x))) + int(math.Abs(float64(loc.y)))
}

func movePart2(instructions []instruction) int {
	var heading int
	var loc Location
	history := make(map[Location]bool)

	for _, instr := range instructions {
		heading = (4 + heading + instr.dir) % 4

		var v *int
		var add int
		if heading == 0 || heading == 2 {
			v = &loc.y
		} else {
			v = &loc.x
		}
		if heading == 1 || heading == 2 {
			add = 1
		} else {
			add = -1
		}

		for i := 0; i < instr.steps; i++ {
			*v += add //cool oneliner, or unreadable mess?

			if history[loc] == false {
				history[loc] = true
			} else {
				return loc.getDistance()
			}
		}
	}
	return loc.getDistance()
}
