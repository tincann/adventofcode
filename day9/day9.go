package main

import (
	"fmt"
	"regexp"
	"strconv"
	"strings"
)

type PART int

const (
	PART1 PART = iota
	PART2 PART = iota
)

func compute(input string, part PART) int {
	c := 0
	for _, line := range strings.Split(input, "\n") {
		var o string
		if part == PART1 {
			o = explode(line)
		} else {
			// o = explodePart2(line)
		}
		c += len(o)
	}
	return c
}

var r = regexp.MustCompile(`\((\d+)x(\d+)\)`)

func explode(input string) string {
	// fmt.Println(input)

	output := ""
	done := false
	index := 0
	for !done {
		var o string
		var i int

		o, i, done = explodeOnce(input[index:])
		index += i
		output += o
	}

	return output
}

//Explosion represents an explosion command
//for example (2x2)AA
type Explosion struct {
	value    string
	times    int
	children *ExplosionList
}

type ExplosionList []*Explosion

func (e *Explosion) print() {
	fmt.Print("{ ", e.times, "x ")
	if e.isLeaf() {
		fmt.Print("'", e.value, "'")
	} else {
		e.children.print()
	}

	fmt.Print(" }")
}

func (e *ExplosionList) print() {
	fmt.Print("[")
	for _, c := range *e {
		c.print()
		fmt.Print(", ")
	}
	fmt.Print("]")
}
func (e *Explosion) isLeaf() bool {
	return e.children == nil
}

func parse(input string) (e *ExplosionList) {
	indexes := r.FindStringSubmatchIndex(input)
	if len(indexes) == 0 {
		if input == "" {
			return &ExplosionList{}
		}
		e := &Explosion{value: input, times: 1}
		return &ExplosionList{e}
	}

	//parse times
	index := indexes[0]
	matches := r.FindStringSubmatch(input)
	rng, _ := strconv.Atoi(matches[1])
	times, _ := strconv.Atoi(matches[2])

	//length of command string
	skip := len(matches[0])

	exp := Explosion{
		times: times,
	}

	//start and end of string that needs to be repeated
	startIndex := index + skip
	endIndex := startIndex + rng

	before := parse(input[:index])
	exp.children = parse(input[startIndex:endIndex])
	after := parse(input[endIndex:])

	output := append(*before, &exp)
	output = append(output, *after...)

	return &output
}

func explodeOnce(input string) (exploded string, index int, done bool) {
	//parse
	// fmt.Println(input)
	indexes := r.FindStringSubmatchIndex(input)
	if len(indexes) == 0 {
		return input, 0, true
	}
	index = indexes[0]
	matches := r.FindStringSubmatch(input)
	skip := len(matches[0])
	rng, _ := strconv.Atoi(matches[1])
	times, _ := strconv.Atoi(matches[2])

	//explode
	startIndex := index + skip
	endIndex := startIndex + rng
	part := input[startIndex:endIndex]

	output := input[:index]
	for i := 0; i < times; i++ {
		output += part
	}

	return output, endIndex, false
}
