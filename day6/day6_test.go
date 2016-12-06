package main

import "io/ioutil"
import "fmt"
import "testing"

func Example_1() {
	input := `eedadn
drvtee
eandsr
raavrd
atevrs
tsrnev
sdttsa
rasrtv
nssdts
ntnada
svetve
tesnvt
vntsnd
vrdear
dvrsen
enarar`
	fmt.Println(compute(input))
	// Output: easter
}

func Test_Final(t *testing.T) {
	input, _ := ioutil.ReadFile("day6.input")
	fmt.Println(compute(string(input)))
}
