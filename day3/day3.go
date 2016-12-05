package main

import (
	"regexp"
	"strconv"
	"strings"
)

func execute(input string) int {
	triangles := parse(input)
	numCorrect := 0
	for _, sideString := range triangles {
		//parse
		r := regexp.MustCompile("\\d+")
		sides := r.FindAllString(sideString, 3)
		if len(sides) != 3 {
			continue
		}
		triangle := Triangle{
			side1: parseSide(sides[0]),
			side2: parseSide(sides[1]),
			side3: parseSide(sides[2]),
		}

		//validate
		if triangle.validate() {
			numCorrect++
		}
	}
	return numCorrect
}

func parse(input string) []string {
	return strings.Split(input, "\n")
}

func parseSide(side string) int {
	n, _ := strconv.Atoi(side)
	return n
}

// Triangle represents the side lengths of a triangle
type Triangle struct {
	side1, side2, side3 int
}

func (t Triangle) validate() bool {
	return t.side1+t.side2 > t.side3 &&
		t.side1+t.side3 > t.side2 &&
		t.side2+t.side3 > t.side1
}
