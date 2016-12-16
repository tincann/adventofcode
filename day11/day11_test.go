package main

import (
	"fmt"
	"io/ioutil"
	"strconv"
	"testing"
)

func Test(t *testing.T) {

}

func Example_1() {
	input := `The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.
The second floor contains a hydrogen generator.
The third floor contains a lithium generator.
The fourth floor contains nothing relevant.`
	fmt.Println(compute(input))
	// Output: 11
}

func createState(locs ...*Locations) *State {
	pairs := make(map[string]*Locations)

	for i, loc := range locs {
		pairs[strconv.Itoa(i)] = loc
	}
	state := State{elevator: 0, numFloors: 4, pairs: pairs}
	return &state
}

func Test_ValidState(t *testing.T) {
	if createState(
		&Locations{chipLocation: 2, generatorLocation: 2},
		&Locations{chipLocation: 2, generatorLocation: 2},
	).isValid() == false {
		t.Error("Should be valid")
	}

	if createState(
		&Locations{chipLocation: 0, generatorLocation: 2},
		&Locations{chipLocation: 0, generatorLocation: 0},
	).isValid() == false {
		t.Error("Should be valid")
	}

	if createState(
		&Locations{chipLocation: 0, generatorLocation: 0},
		&Locations{chipLocation: 0, generatorLocation: 0},
	).isValid() == false {
		t.Error("Should be valid")
	}

	if createState(
		&Locations{chipLocation: 2, generatorLocation: 2},
		&Locations{chipLocation: 3, generatorLocation: 2},
	).isValid() == false {
		t.Error("Should be valid")
	}
}

func Test_InvalidState(t *testing.T) {
	if createState(
		&Locations{chipLocation: 2, generatorLocation: 2},
		&Locations{chipLocation: 2, generatorLocation: 3},
	).isValid() == true {
		t.Error("Should be invalid")
	}

	if createState(
		&Locations{chipLocation: 0, generatorLocation: 3},
		&Locations{chipLocation: 3, generatorLocation: 0},
		&Locations{chipLocation: 0, generatorLocation: 3},
	).isValid() == true {
		t.Error("Should be invalid")
	}
}

func Test_Final(t *testing.T) {
	input, _ := ioutil.ReadFile("day11.input")
	output := compute(string(input))
	fmt.Println("FINAL ANSWER P1:", output)
}

// part 2
func Example_7() {
	// input := `X(8x2)(3x3)ABCY`
	// fmt.Println(compute(input, PART2))
	// Output: 20
}
