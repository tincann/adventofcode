package main

import (
	"fmt"
	"regexp"
	"strings"
)

const MAX_OBJECTS = 10

func compute(input string) int {
	level := createLevel(input)
	fmt.Printf("%+v", level)
	return 5
}

type Level struct {
	floors []*Floor
}

type Floor struct {
	number  int
	objects []*Object
}

type Object struct {
	id  string
	typ string
}

var chipRegex = regexp.MustCompile(`(\w+?)-compatible microchip`)
var generatorRegex = regexp.MustCompile(`(\w+?) generator`)

func createLevel(input string) *Level {
	split := strings.Split(input, "\n")
	level := Level{floors: make([]*Floor, len(split))}

	for i, line := range split {
		floor := Floor{number: i, objects: make([]*Object, 0)}

		chipMatches := chipRegex.FindAllStringSubmatch(line, MAX_OBJECTS)
		if len(chipMatches) > 0 {
			for _, match := range chipMatches {
				floor.objects = append(floor.objects, &Object{id: match[1], typ: "chip"})
			}
		}

		generatorMatches := generatorRegex.FindAllStringSubmatch(line, MAX_OBJECTS)
		if len(generatorMatches) > 0 {
			for _, match := range generatorMatches {
				floor.objects = append(floor.objects, &Object{id: match[1], typ: "generator"})
			}
		}

		level.floors[i] = &floor
	}
	return &level
}
