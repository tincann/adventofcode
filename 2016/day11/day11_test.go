package main

import (
	"fmt"
	"strconv"
	"testing"
)

func Example_1() {
	input := `The first floor contains a hydrogen-compatible microchip and a hydrogen generator.
The second floor contains nothing relevant.
The third floor contains nothing relevant.
The fourth floor contains nothing relevant.`
	fmt.Println(compute(input))
	// Output: 4
}

// func Example_2() {
// 	input := `The first floor contains a hydrogen-compatible microchip and a lithium-compatible microchip.
// The second floor contains a hydrogen generator.
// The third floor contains a lithium generator.
// The fourth floor contains nothing relevant.`
// 	fmt.Println(compute(input))
// 	// Output: 11
// }

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
		&Locations{chipLoc: 2, genLoc: 2},
		&Locations{chipLoc: 2, genLoc: 2},
	).isValid() == false {
		t.Error("Should be valid")
	}

	if createState(
		&Locations{chipLoc: 0, genLoc: 2},
		&Locations{chipLoc: 0, genLoc: 0},
	).isValid() == false {
		t.Error("Should be valid")
	}

	if createState(
		&Locations{chipLoc: 0, genLoc: 0},
		&Locations{chipLoc: 0, genLoc: 0},
	).isValid() == false {
		t.Error("Should be valid")
	}

	if createState(
		&Locations{chipLoc: 2, genLoc: 2},
		&Locations{chipLoc: 3, genLoc: 2},
	).isValid() == false {
		t.Error("Should be valid")
	}
}

func Test_InvalidState(t *testing.T) {
	if createState(
		&Locations{chipLoc: 2, genLoc: 2},
		&Locations{chipLoc: 2, genLoc: 3},
	).isValid() == true {
		t.Error("Should be invalid")
	}

	if createState(
		&Locations{chipLoc: 0, genLoc: 3},
		&Locations{chipLoc: 3, genLoc: 0},
		&Locations{chipLoc: 0, genLoc: 3},
	).isValid() == true {
		t.Error("Should be invalid")
	}
}

func Test_Combinations(t *testing.T) {
	state := createState(
		&Locations{chipLoc: 0, genLoc: 0},
	)

	combs := state.generateObjectCombinations(0)
	if len(combs) != 3 {
		t.Error("Should be 3 combinations")
	}
}

// func Test_Final(t *testing.T) {
// 	input, _ := ioutil.ReadFile("day11.input")
// 	output := compute(string(input))
// 	fmt.Println("FINAL ANSWER P1:", output)
// }

// part 2
func Example_7() {
	// input := `X(8x2)(3x3)ABCY`
	// fmt.Println(compute(input, PART2))
	// Output: 20
}
