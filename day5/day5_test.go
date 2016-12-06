package main

import (
	"fmt"
	"testing"
)

func Test_hash1(t *testing.T) {
	valid, _ := validate("1e0f7f898587b387a20d22ee94f87512")
	if valid {
		t.Fail()
	}
}

func Test_hash2(t *testing.T) {
	valid, letter := validate("00000f898587b387a20d22ee94f87512")
	if valid == false {
		t.Fail()
	}
	if letter != "f" {
		t.Fail()
	}
}

func Test_1(t *testing.T) {
	fmt.Println(computePart2("abc"))
	// Output: 05ace8e3
}

func Test_Final(t *testing.T) {
	fmt.Println("FINAL ANSWER:", compute("ojvtpuvg"))
}

func Test_FinalPart2(t *testing.T) {
	fmt.Println("FINAL ANSWER:", computePart2("ojvtpuvg"))
}
