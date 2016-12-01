package main

import (
	"fmt"
	"math"
	"strconv"
	"strings"
)

func main() {
	input := "R5, R4, R2, L3, R1, R1, L4, L5, R3, L1, L1, R4, L2, R1, R4, R4, L2, L2, R4, L4, R1, R3, L3, L1, L2, R1, R5, L5, L1, L1, R3, R5, L1, R4, L5, R5, R1, L185, R4, L1, R51, R3, L2, R78, R1, L4, R188, R1, L5, R5, R2, R3, L5, R3, R4, L1, R2, R2, L4, L4, L5, R5, R4, L4, R2, L5, R2, L1, L4, R4, L4, R2, L3, L4, R2, L3, R3, R2, L2, L3, R4, R3, R1, L4, L2, L5, R4, R4, L1, R1, L5, L1, R3, R1, L2, R1, R1, R3, L4, L1, L3, R2, R4, R2, L2, R1, L5, R3, L3, R3, L1, R4, L3, L3, R4, L2, L1, L3, R2, R3, L2, L1, R4, L3, L5, L2, L4, R1, L4, L4, R3, R5, L4, L1, L1, R4, L2, R5, R1, R1, R2, R1, R5, L1, L3, L5, R2"

	instr := parse(input)
	output := calculate(instr)

	fmt.Println(output)
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

func calculate(instructions []instruction) int {
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
