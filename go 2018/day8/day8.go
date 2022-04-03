package main

import (
	"fmt"
	"regexp"
	"strconv"
	"strings"
)

func compute(width, height int, input string) int {
	screen := NewScreen(width, height)
	for _, line := range strings.Split(input, "\n") {
		screen.executeCommand(line)
	}
	screen.print()
	return screen.countPixels(true)
}

var rectParse = regexp.MustCompile(`rect (\d+)x(\d+)`)
var rotateColumnParse = regexp.MustCompile(`rotate column x=(\d+) by (\d+)`)
var rotateRowParse = regexp.MustCompile(`rotate row y=(\d+) by (\d+)`)

// Screen represents the display
type Screen struct {
	width, height int
	contents      []bool
}

// NewScreen creates a new Screen struct
func NewScreen(width, height int) Screen {
	contents := make([]bool, width*height)
	return Screen{
		width:    width,
		height:   height,
		contents: contents,
	}
}

func (s *Screen) executeCommand(line string) {
	if len(line) == 0 {
		return
	}

	if rectParse.MatchString(line) {
		matches := rectParse.FindStringSubmatch(line)
		width, _ := strconv.Atoi(matches[1])
		height, _ := strconv.Atoi(matches[2])
		s.rect(width, height)
	} else if rotateColumnParse.MatchString(line) {
		matches := rotateColumnParse.FindStringSubmatch(line)
		column, _ := strconv.Atoi(matches[1])
		steps, _ := strconv.Atoi(matches[2])
		s.rotateColumn(column, steps)
	} else if rotateRowParse.MatchString(line) {
		matches := rotateRowParse.FindStringSubmatch(line)
		row, _ := strconv.Atoi(matches[1])
		steps, _ := strconv.Atoi(matches[2])
		s.rotateRow(row, steps)
	} else {
		panic("Can't parse line: " + line)
	}
}

func (s *Screen) setPixel(x, y int, value bool) {
	s.contents[y*s.width+x] = value
}

func (s *Screen) getPixel(x, y int) bool {
	return s.contents[y*s.width+x]
}

func (s *Screen) rect(width, height int) {
	for y := 0; y < height; y++ {
		for x := 0; x < width; x++ {
			s.setPixel(x, y, true)
		}
	}
}

func (s *Screen) rotateColumn(column, steps int) {
	copy := make([]bool, s.height)
	for i := 0; i < s.height; i++ {
		copy[i] = s.getPixel(column, i)
	}

	for i := 0; i < s.height; i++ {
		s.setPixel(column, (i+steps)%s.height, copy[i])
	}
}

func (s *Screen) rotateRow(row, steps int) {
	copy := make([]bool, s.width)
	for i := 0; i < s.width; i++ {
		copy[i] = s.getPixel(i, row)
	}

	for i := 0; i < s.width; i++ {
		s.setPixel((i+steps)%s.width, row, copy[i])
	}
}

func (s *Screen) countPixels(value bool) int {
	counter := 0
	for i := 0; i < len(s.contents); i++ {
		if s.contents[i] == value {
			counter++
		}
	}
	return counter
}

func (s *Screen) print() {

	for i := 0; i < len(s.contents); i++ {
		if s.contents[i] {
			fmt.Print("X")
		} else {
			fmt.Print(".")
		}

		if i%s.width == s.width-1 {
			fmt.Print("\n")
		}
	}
}
