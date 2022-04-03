package main

import "fmt"

import "io/ioutil"
import "testing"

func Example_1() {
	fmt.Println(execute("5 10 25"))
	// Output: 0
}

func Example_2() {
	fmt.Println(execute("25 10 25"))
	// Output: 1
}

func Test_Final(t *testing.T) {
	input := readInput("day3.input")
	// fmt.Println("FINAL ANSWER:", execute(input))
	fmt.Println("FINAL ANSWER:", executePart2(input))
}

func readInput(path string) string {
	content, _ := ioutil.ReadFile(path)
	return string(content)
}
