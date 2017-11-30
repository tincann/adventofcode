package main

import (
	"fmt"
	"io/ioutil"
	"testing"
)

func Example_1() {
	input := `ADVENT`
	fmt.Println(explode(string(input)))
	// Output: ADVENT
}

func Example_2() {
	input := `A(1x5)BC`
	fmt.Println(explode(string(input)))
	// Output: ABBBBBC
}

func Example_3() {
	input := `(3x3)XYZ`
	fmt.Println(explode(string(input)))
	// Output: XYZXYZXYZ
}

func Example_4() {
	input := `A(2x2)BCD(2x2)EFG`
	fmt.Println(explode(string(input)))
	// Output: ABCBCDEFEFG
}

func Example_5() {
	input := `(6x1)(1x3)A`
	fmt.Println(explode(string(input)))
	// Output: (1x3)A
}

func Example_6() {
	input := `X(8x2)(3x3)ABCY`
	fmt.Println(explode(string(input)))
	// Output: X(3x3)ABC(3x3)ABCY
}

func Test_Final(t *testing.T) {
	input, _ := ioutil.ReadFile("day9.input")
	output := compute(string(input), PART1)
	fmt.Println("FINAL ANSWER P1:", output)
}

// part 2
func Example_7() {
	input := `X(8x2)(3x3)ABCY`
	fmt.Println(compute(input, PART2))
	// Output: 20
}

func Example_8() {
	input := `(27x12)(20x12)(13x14)(7x10)(1x12)A`
	fmt.Println(compute(input, PART2))
	// Output: 241920
}

func Example_9() {
	input := `(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN`
	fmt.Println(compute(input, PART2))
	// Output: 445
}

func Test_FinalPart2(t *testing.T) {
	input, _ := ioutil.ReadFile("day9.input")
	output := compute(string(input), PART2)
	fmt.Println("FINAL ANSWER P2:", output)
}
