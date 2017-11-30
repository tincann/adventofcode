package main

import "unicode"
import "sort"
import "strings"
import "strconv"
import "fmt"

func compute(input string) int {
	rooms := strings.Split(input, "\n")
	sum := 0
	for _, r := range rooms {
		room := parseRoom(r)
		if validateRoom(room) {
			rn, _ := strconv.Atoi(room.roomNumber)
			sum += rn
			secret := decrypt(room)
			if strings.Contains(secret, "north") {
				fmt.Println(room.roomNumber, secret)
			}
		}
	}
	return sum
}

func decrypt(room room) string {
	roomNumber, _ := strconv.Atoi(room.roomNumber)
	shifts := roomNumber % 26

	str := ""
	for _, chr := range room.letters {
		if unicode.IsSpace(chr) {
			str += " "
		} else {
			letter := string(((int(chr) - 97 + shifts) % 26) + 97)
			str += letter
		}
	}
	return str
}

type room struct {
	letters    string
	letterFreq map[string]int
	roomNumber string
	hash       string
}

func parseRoom(input string) room {
	var i = 0
	var r rune
	var room room
	room.letterFreq = make(map[string]int)

	//parse until '[' character
	for ; i < len(input) && string(r) != "["; i++ {
		r = rune(input[i])

		if unicode.IsLetter(r) {
			room.letterFreq[string(r)]++
			room.letters += string(r)
		} else if unicode.IsNumber(r) {
			room.roomNumber += string(r)
		} else if string(r) == "-" {
			room.letters += " "
		}
	}
	//parse hash
	for ; i < len(input); i++ {
		r = rune(input[i])
		if unicode.IsLetter(r) {
			room.hash += string(r)
		}
	}

	return room
}

// Pair represents a character and its frequency
type pair struct {
	key   string
	value int
}

type pairList []pair

func validateRoom(room room) bool {
	pairs := make(pairList, len(room.letterFreq))
	i := 0
	for k, v := range room.letterFreq {
		pairs[i] = pair{k, v}
		i++
	}
	sort.Sort(sort.Reverse(pairs))

	for i := 0; i < len(room.hash); i++ {
		chr := string(room.hash[i])

		if chr != pairs[i].key {
			return false
		}
		i++
	}

	return true
}

func (p pairList) Len() int { return len(p) }
func (p pairList) Less(i, j int) bool {
	return (p[i].value == p[j].value && p[i].key > p[j].key) ||
		p[i].value < p[j].value
}
func (p pairList) Swap(i, j int) { p[i], p[j] = p[j], p[i] }
