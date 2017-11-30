package main

import (
	"regexp"
	"strconv"
)

type PART int

const (
	PART1 PART = iota
	PART2 PART = iota
)

func compute(input string, part PART) int {
	if part == PART1 {
		return len(explode(input))
	}

	return explodePart2(input)
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

func explodePart2(input string) int {

	count := 0
	for len(input) > 0 {
		marker, found := getMarker(input)
		if !found {
			count += len(input)
			break
		}

		count += marker.position
		count += marker.times * explodePart2(input[marker.startIndex:marker.endIndex])
		input = input[marker.endIndex:]
	}

	return count
}

type Marker struct {
	position   int
	startIndex int
	endIndex   int
	times      int
}

func getMarker(input string) (m *Marker, found bool) {
	indexes := r.FindStringSubmatchIndex(input)
	if len(indexes) == 0 {
		return nil, false
	}

	matches := r.FindStringSubmatch(input)
	rng, _ := strconv.Atoi(matches[1])
	times, _ := strconv.Atoi(matches[2])

	startIndex := indexes[0] + len(matches[0])
	endIndex := startIndex + rng
	return &Marker{

		position:   indexes[0],
		startIndex: startIndex,
		endIndex:   endIndex,
		times:      times,
	}, true
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
