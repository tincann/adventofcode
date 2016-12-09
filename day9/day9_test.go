package main

import "fmt"

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

// func Example_4() {
// 	input := `A(2x2)BCD(2x2)EFG`
// 	fmt.Println(explode(string(input)))
// 	// Output: ABCBCDEFEFG
// }

// func Example_5() {
// 	input := `(6x1)(1x3)A`
// 	fmt.Println(explode(string(input)))
// 	// Output: (1x3)A
// }

// func Example_6() {
// 	input := `X(8x2)(3x3)ABCY`
// 	fmt.Println(explode(string(input)))
// 	// Output: X(3x3)ABC(3x3)ABCY
// }

//part 2
// func Example_7() {
// 	input := `X(8x2)(3x3)ABCY`
// 	//X(8x2)(3x3)ABCXY
// 	//1[ X ] 2[ 3[ ABC ] 1[ X ] ] 1[ Y ]
// 	fmt.Println(compute(input, PART2))
// 	// Output: XABCABCABCABCABCABCY
// }

func Example_8() {
	input := `(27x12)(20x12)(13x14)(7x10)(1x12)A`
	fmt.Println(compute(input, PART2))
	// Output: 241920
}

// func Test_Final(t *testing.T) {
// 	input, _ := ioutil.ReadFile("day9.input")
// 	output := compute(string(input), PART1)
// 	fmt.Println("FINAL ANSWER P1:", output)
// }

// func Test_FinalPart2(t *testing.T) {
// 	input, _ := ioutil.ReadFile("day9.input")
// 	output := compute(string(input), PART2)
// 	fmt.Println("FINAL ANSWER P2:", output)
// }
