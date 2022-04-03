package main

import (
	"fmt"
	"io/ioutil"
	"testing"
)

func Test_1(t *testing.T) {
	input := `rect 3x2
rotate column x=1 by 1
rotate row y=0 by 4
rotate column x=1 by 1`
	fmt.Println(compute(8, 4, input))
}

func Test_Final(t *testing.T) {
	input, _ := ioutil.ReadFile("day8.input")
	fmt.Println("FINAL ANSWER P1:", compute(50, 6, string(input)))
}
