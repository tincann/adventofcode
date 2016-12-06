package main

import (
	"fmt"
	"io/ioutil"
	"testing"
)

func Test_parseRoom(t *testing.T) {
	room := parseRoom("aaaaa-bbb-z-y-x-123[abxyz]")
	fmt.Println(room)
	assertTrue(t, room.letters["a"] == 5)
	assertTrue(t, room.letters["b"] == 3)
	assertTrue(t, room.letters["x"] == 1)
	assertTrue(t, room.letters["y"] == 1)
	assertTrue(t, room.letters["z"] == 1)
	assertTrue(t, room.roomNumber == "123")
}

func Example_1() {
	room := parseRoom("aaaaa-bbb-z-y-x-123[abxyz]")
	fmt.Println(validateRoom(room))
	// Output: true
}

func Example_2() {
	room := parseRoom("a-b-c-d-e-f-g-h-987[abcde]")
	fmt.Println(validateRoom(room))
	// Output: true
}

func Example_3() {
	room := parseRoom("not-a-real-room-404[oarel]")
	fmt.Println(validateRoom(room))
	// Output: true
}

func Example_4() {
	room := parseRoom("totally-real-room-200[decoy]")
	fmt.Println(validateRoom(room))
	// Output: false
}

func Test_Final(t *testing.T) {
	input, _ := ioutil.ReadFile("day4.input")
	fmt.Println("FINAL ANSWER:", compute(string(input)))
}

func assertTrue(t *testing.T, isTrue bool) {
	if !isTrue {
		t.Fail()
	}
}
