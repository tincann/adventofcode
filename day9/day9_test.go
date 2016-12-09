package main

import (
	"fmt"
	"io/ioutil"
	"testing"
)

// func Example_1() {
// 	input := `ADVENT`
// 	fmt.Println(explode(string(input)))
// 	// Output: ADVENT
// }

// func Example_2() {
// 	input := `A(1x5)BC`
// 	fmt.Println(explode(string(input)))
// 	// Output: ABBBBBC
// }

// func Example_3() {
// 	input := `(3x3)XYZ`
// 	fmt.Println(explode(string(input)))
// 	// Output: XYZXYZXYZ
// }

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

func Test_1(t *testing.T) {
	input := ``
	compute(input)
}

func Test_Final(t *testing.T) {
	input, _ := ioutil.ReadFile("day9.input")
	output := compute(string(input))
	fmt.Println("FINAL ANSWER P1:", output)
}
