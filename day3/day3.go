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
		if validateTriangle(triangle.side1, triangle.side2, triangle.side3) {
			numCorrect++
		}
	}
	return numCorrect
}

func executePart2(input string) int {
	numCorrect := 0
	lines := parse(input)
	var t1, t2, t3 [3]int

	for i := 0; i < len(lines); i++ {
		line := lines[i]
		r := regexp.MustCompile("\\d+")
		sides := r.FindAllString(line, 3)
		if len(sides) != 3 {
			continue
		}
		var tIndex = i % 3
		t1[tIndex] = parseSide(sides[0])
		t2[tIndex] = parseSide(sides[1])
		t3[tIndex] = parseSide(sides[2])

		if i%3 == 2 {
			if validateTriangle(t1[0], t1[1], t1[2]) {
				numCorrect++
			}
			if validateTriangle(t2[0], t2[1], t2[2]) {
				numCorrect++
			}
			if validateTriangle(t3[0], t3[1], t3[2]) {
				numCorrect++
			}
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

func validateTriangle(a int, b int, c int) bool {
	return a+b > c && a+c > b && b+c > a
}
