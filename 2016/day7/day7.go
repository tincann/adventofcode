package main

import "strings"

func compute(input string) int {
	split := strings.Split(input, "\n")
	count := 0
	for _, line := range split {
		if isValidIPV7(line) {
			count++
		}
	}
	return count
}

func computePart2(input string) int {
	split := strings.Split(input, "\n")
	count := 0
	for _, line := range split {
		if isValidSSL(line) {
			count++
		}
	}
	return count
}

func isValidIPV7(ip string) bool {
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

func isValidSSL(ip string) bool {
	isWithinBrackets := false
	outside := make(map[string]bool)
	inside := make(map[string]bool)

	for i := 0; i < len(ip)-2; i++ {
		if isChar(ip, i, "[") {
			isWithinBrackets = true
		} else if isChar(ip, i, "]") {
			isWithinBrackets = false
		}

		valid, str := isABA(ip, i)
		if valid {
			if isWithinBrackets {
				inside[str] = true
			} else {
				outside[str] = true
			}
		}
	}

	for aba := range inside {
		inverted := invertABA(aba)
		if outside[inverted] {
			return true
		}
	}
	return false
}

func invertABA(s string) string {
	first := string(s[0])
	second := string(s[1])

	return second + first + second
}

func isABA(s string, i int) (valid bool, str string) {
	valid = different(s, i, i+1) &&
		same(s, i, i+2)
	return valid, s[i : i+3]
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
