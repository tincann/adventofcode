package main

import (
	"fmt"
	"io/ioutil"
	"testing"
)

func Test(t *testing.T) {

}

func Example_1() {
	input := `The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.
The second floor contains a hydrogen generator.
The third floor contains a lithium generator.
The fourth floor contains nothing relevant.`
	fmt.Println(compute(input))
	// Output: 11
}

func Test_Final(t *testing.T) {
	input, _ := ioutil.ReadFile("day11.input")
	output := compute(string(input))
	fmt.Println("FINAL ANSWER P1:", output)
}

// part 2
func Example_7() {
	// input := `X(8x2)(3x3)ABCY`
	// fmt.Println(compute(input, PART2))
	// Output: 20
}
