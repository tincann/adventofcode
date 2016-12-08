package main

import (
	"fmt"
	"io/ioutil"
	"testing"
)

func Example_1() {
	input := ``
	fmt.Println(compute(string(input)))
	// Output: test
}

func Test_1(t *testing.T) {
	input := ``
	compute(input)
}

func Test_Final(t *testing.T) {
	input, _ := ioutil.ReadFile("day9.input")
	output := compute(string(input))
	fmt.Println("FINAL ANSWER P1:", output)
}
