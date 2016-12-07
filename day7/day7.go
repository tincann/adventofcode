package main

import "strings"

func compute(input string) int {
	split := strings.Split(input, "\n")
	count := 0
	for _, line := range split {
		if isValid(line) {
			count++
		}
	}
	return count
}

func isValid(ip string) bool {
	isWithinBrackets := false
	valid := false
	for i := 0; i < len(ip)-3; i++ {
		if isChar(ip, i, "[") {
			isWithinBrackets = true
		} else if isChar(ip, i, "]") {
			isWithinBrackets = false
		}

		if isABBA(ip, i) {
			if isWithinBrackets {
				return false
			}
			valid = true
		}

	}
	// fmt.Println("NOT VALID:", ip)
	return valid
}

func isABBA(s string, i int) bool {
	return different(s, i, i+1) &&
		same(s, i+1, i+2) &&
		same(s, i, i+3)
}

func different(s string, index1 int, index2 int) bool {
	return !same(s, index1, index2)
}

func same(s string, index1 int, index2 int) bool {
	return string(s[index1]) == string(s[index2])
}

func isChar(s string, index int, char string) bool {
	return char == string(s[index])
}
