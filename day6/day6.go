package main

import "strings"

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

func compute(input string) string {
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
		output += table.getHighestFreq()
	}

	return output
}
