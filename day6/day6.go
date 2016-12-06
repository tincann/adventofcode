package main

import "strings"
import "math"

type freqTable map[string]int

func (m freqTable) getHighestFreq() string {
	highest := 0
	letter := ""
	for k, v := range m {
		if v > highest {
			letter = k
			highest = v
		}
	}
	return letter
}

func (m freqTable) getLowestFreq() string {
	lowest := math.MaxInt64
	letter := ""
	for k, v := range m {
		if v < lowest {
			letter = k
			lowest = v
		}
	}
	return letter
}

type HighLow int

const (
	HIGHEST HighLow = iota
	LOWEST
)

func compute(input string, highLow HighLow) string {
	split := strings.Split(input, "\n")
	width := len(split[0])
	freq := make([]freqTable, width)
	for x := 0; x < width; x++ {
		freq[x] = make(freqTable)
	}

	for y := 0; y < len(split); y++ {
		line := split[y]
		if len(line) == 0 {
			continue
		}
		for x := 0; x < width; x++ {
			chr := string(line[x])
			freq[x][chr]++
		}
	}

	output := ""
	for _, table := range freq {
		if highLow == HIGHEST {
			output += table.getHighestFreq()
		} else {
			output += table.getLowestFreq()
		}
	}

	return output
}
