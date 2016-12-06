package main

import (
	"crypto/md5"
	"fmt"
	"strconv"
	"strings"
)

func compute(roomID string) string {
	output := ""
	i := 0
	for x := 0; x < 8; x++ {
		for true {
			candidate := roomID + strconv.Itoa(i)
			hashOutput := fmt.Sprintf("%x", md5.Sum([]byte(candidate)))
			valid, letter := validate(hashOutput)
			i++
			if valid {
				output += letter
				break
			}
		}
	}
	return output
}

func computePart2(roomID string) string {
	output := make([]string, 8)
	filled := 0
	i := 0
	fmt.Println(output)

	for filled < 8 {
		candidate := roomID + strconv.Itoa(i)
		hashOutput := fmt.Sprintf("%x", md5.Sum([]byte(candidate)))
		valid, letter, pos := validatePart2(hashOutput)
		if valid && output[pos] == "" {
			output[pos] = letter
			filled++
			fmt.Println(output)
		}
		i++
	}
	return strings.Join(output, "")
}

func validate(hash string) (valid bool, letter string) {
	if hash[:5] == "00000" {
		return true, string(hash[5])
	}
	return false, ""
}

func validatePart2(hash string) (valid bool, letter string, position int) {
	if hash[:5] == "00000" {
		var err error
		position, err = strconv.Atoi(string(hash[5]))
		if err == nil && position < 8 {
			return true, string(hash[6]), position
		}
	}
	return false, "", -1
}
