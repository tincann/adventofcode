package main

import (
	"fmt"
	"io/ioutil"
	"testing"
)

// func Example_1() {
// 	fmt.Println(isValid("abba[mnop]qrst"))
// 	// Output: true
// }

// func Example_2() {
// 	fmt.Println(isValid("abcd[bddb]xyyx"))
// 	// Output: false
// }

// func Example_3() {
// 	fmt.Println(isValid("aaaa[qwer]tyui"))
// 	// Output: false
// }

// func Example_4() {
// 	fmt.Println(isValid("ioxxoj[asdfgh]zxcvbn"))
// 	// Output: true
// }

func Test_Final(t *testing.T) {
	input, _ := ioutil.ReadFile("day7.input")
	fmt.Println("FINAL ANSWER:", compute(string(input)))
}
