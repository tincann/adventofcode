package main

import (
	"fmt"
	"testing"
)

func Example_1() {
	fmt.Println(compute(10, Coordinate{x: 7, y: 4}, false))
	// Output: 11
}

func Test_Final(t *testing.T) {
	fmt.Println("FINAL ANSWER P1:", compute(1358, Coordinate{31, 39}, false))
}

func Test_FinalPart2(t *testing.T) {
	fmt.Println("FINAL ANSWER P2:", compute(1358, Coordinate{31, 39}, true))
}

func Test_zeroSteps(t *testing.T) {
	if compute(10, Coordinate{1, 1}, false) != 0 {
		t.Error("destination equality doesnt work")
	}
}

func Test_Coordinate(t *testing.T) {
	a := Coordinate{5, 3}
	b := Coordinate{5, 3}
	if a != b {
		t.Error("Golang equality is weird")
	}
}

func Test_Binary(t *testing.T) {
	if countOnesInBinary(5) != 2 {
		t.Error("5 (101) should be 2")
	}

	if countOnesInBinary(67909) != 6 {
		t.Error("67909 should be 6")
	}

	if countOnesInBinary(4096) != 1 {
		t.Error("4096 should be 1")
	}
}

func Test_Explore(t *testing.T) {
	m := newMaze(10)
	if m.explore(Tile{1, 1, 0}) != false {
		t.Error("Shouldn't be explored yet")
	}
	if m.explore(Tile{1, 1, 0}) != true {
		t.Error("Should be explored")
	}
}

func Test_isWall(t *testing.T) {
	m := newMaze(10)
	if m.isWall(0, 0) != false {
		t.Error("Should be empty")
	}

	if m.isWall(0, 1) != false {
		t.Error("Should be empty")
	}

	if m.isWall(0, 2) != true {
		t.Error("Should be a wall")
	}

	if m.isWall(1, 0) != true {
		t.Error("Should be a wall")
	}

	if m.isWall(9, 6) != true {
		t.Error("Should be a wall")
	}
}

func Test_Queue(t *testing.T) {
	q := newQueue()
	first := Tile{1, 2, 3}
	q.push(first)
	second := Tile{2, 3, 4}
	q.push(second)
	third := Tile{3, 4, 5}
	q.push(third)

	if first == second || second == third || first == third {
		t.Error("Something wrong with equality")
	}

	if q.empty() {
		t.Error("Should contain 3")
	}

	if q.pop() != first {
		t.Error("Order is wrong")
	}

	if q.pop() != second {
		t.Error("Order is wrong")
	}

	if q.pop() != third {
		t.Error("Order is wrong")
	}

	if q.empty() != true {
		t.Error("Queue is not empty")
	}
}
