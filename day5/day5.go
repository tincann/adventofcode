package main

import (
	"crypto/md5"
	"fmt"
	"strconv"
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

func validate(hash string) (valid bool, letter string) {
	if hash[:5] == "00000" {
		return true, string(hash[5])
	}
	return false, ""
}
