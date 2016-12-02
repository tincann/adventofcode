package main

import (
	"math"
	"strconv"
	"strings"
)

func execute(input string) int {
	instr := parse(input)
	output := move(instr)
	return output
}

type instruction struct {
	dir   int64
	steps int64
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

		instructions[i].steps, _ = strconv.ParseInt(instr[1:], 10, 32)
	}
	return instructions
}

func move(instructions []instruction) int {
	var heading, x, y int64

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
