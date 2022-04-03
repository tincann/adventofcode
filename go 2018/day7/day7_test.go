package main

import (
	"fmt"
	"io/ioutil"
	"testing"
)

//part 1

func Example_1() {
	fmt.Println(isValidIPV7("abba[mnop]qrst"))
	// Output: true
}

func Example_2() {
	fmt.Println(isValidIPV7("abcd[bddb]xyyx"))
	// Output: false
}

func Example_3() {
	fmt.Println(isValidIPV7("aaaa[qwer]tyui"))
	// Output: false
}

func Example_4() {
	fmt.Println(isValidIPV7("ioxxoj[asdfgh]zxcvbn"))
	// Output: true
}

//part 2

func Example_5() {
	fmt.Println(isValidSSL("aba[bab]xyz"))
	// Output: true
}

func Example_6() {
	fmt.Println(isValidSSL("xyx[xyx]xyx"))
	// Output: false
}

func Example_7() {
	fmt.Println(isValidSSL("aaa[kek]eke"))
	// Output: true
}

func Example_8() {
	fmt.Println(isValidSSL("zazbz[bzb]cdb"))
	// Output: true
}

func Test_Final(t *testing.T) {
	input, _ := ioutil.ReadFile("day7.input")
	fmt.Println("FINAL ANSWER P1:", compute(string(input)))
}

func Test_FinalPart2(t *testing.T) {
	input, _ := ioutil.ReadFile("day7.input")
	fmt.Println("FINAL ANSWER P2:", computePart2(string(input)))
}
