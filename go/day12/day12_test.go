package main

import (
	"fmt"
	"io/ioutil"
	"testing"
)

func Example_1() {
	input := `cpy 41 a
inc a
inc a
dec a
jnz a 2
dec a`
	fmt.Println(compute(input, false))
	// Output: 42
}

func Test_Final(t *testing.T) {
	input, _ := ioutil.ReadFile("day12.input")
	output := compute(string(input), false)
	fmt.Println("FINAL ANSWER P1:", output)
}

func Test_FinalPart2(t *testing.T) {
	input, _ := ioutil.ReadFile("day12.input")
	output := compute(string(input), true)
	fmt.Println("FINAL ANSWER P1:", output)
}
