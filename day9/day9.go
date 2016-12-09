package main

import (
	"regexp"
	"strconv"
	"strings"
)

func compute(input string) int {
	c := 0
	for _, line := range strings.Split(input, "\n") {
		o := explode(line)
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
