package main

import (
	"fmt"
	"io/ioutil"
	"testing"
)

func Test(t *testing.T) {
	input := `value 5 goes to bot 2
bot 2 gives low to bot 1 and high to bot 0
value 3 goes to bot 1
bot 1 gives low to output 1 and high to bot 0
bot 0 gives low to output 2 and high to output 0
value 2 goes to bot 2`
	f := compute(string(input), PART1)
	fmt.Println(f.getValues())
}

func Test_Final(t *testing.T) {
	input, _ := ioutil.ReadFile("day10.input")
	output := compute(string(input), PART1)
	fmt.Println("FINAL ANSWER P1:", output)
}

// part 2
func Example_7() {
	// input := `X(8x2)(3x3)ABCY`
	// fmt.Println(compute(input, PART2))
	// Output: 20
}

func Test_FinalPart2(t *testing.T) {
	input, _ := ioutil.ReadFile("day10.input")
	output := compute(string(input), PART2)
	fmt.Println("FINAL ANSWER P2:", output.outputs[0].highValue*output.outputs[1].highValue*output.outputs[2].highValue)
}
